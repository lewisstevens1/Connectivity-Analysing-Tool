using Connectivity_Analysing_Tool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Connectivity_Analysing_Tool
{
    public partial class MainForm : Form
    {

        //  MAIN FORM
        public MainForm()
        {
            InitializeComponent();
        }

        // DECRYPT THE STRING FOR US TO READ
        public static string DecryptString(string cipherText, string passPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(Settings.Default.ENV_VECTOR);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(Settings.Default.ENC_KEYSIDE / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch
            {
                return "NOT ENCRYPTED";
            }
        }

        // MAIN FORM ON LOAD
        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        // MAIN FORM ON CLOSE
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void cat_loadzip_Click(object sender, EventArgs e)
        {
            // CREATE NEW SAVEFILE DIALOGUE TO ONLY ALLOW COMPRESSION TYPES
            OpenFileDialog LOAD_FILE = new OpenFileDialog
            {
                Filter = "Zip Files|*.zip"
            };

            // IF ALL OK, CREATE DIRECTORY WITH PATH
            if(LOAD_FILE.ShowDialog() == DialogResult.OK)
            {
                // DELETES DATA FROM TEMP
                if (Directory.Exists(@Settings.Default.FILE_PATH))
                {
                    Directory.Delete(@Settings.Default.FILE_PATH, true);
                }

                // EXTRACTS INTO TEMP
                ZipFile.ExtractToDirectory(LOAD_FILE.FileName,@Settings.Default.FILE_PATH);

                // SELECTS ALL FILES WITH EXTENSION CTT, UNUSUALLY IT ALSO SEEMS TO TREAT THE END AS A WILDCARD ALSO
                int FileExists = 0;
                var FileArray = Directory.GetFiles(@Settings.Default.FILE_PATH, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".ctta") || s.EndsWith(".cttb") || s.EndsWith(".cttc") || s.EndsWith(".cttd"));

                
                // LOOP THROUGH ALL FILES
                foreach (string file in FileArray)
                {
                    // GET THE FILENAME AND MOVE ALL TO TOP LEVEL DIRECTORY
                    string FILE_NAME = Path.GetFileName(file);
                    string FILE_NEW_LOC = @Settings.Default.FILE_PATH + FILE_NAME;

                    File.Move(file, FILE_NEW_LOC);

                    if (FileExists != 2)
                    {
                        string[] cttlines = File.ReadAllLines(FILE_NEW_LOC);
                        foreach (string ctt_str in cttlines)
                        {
                            var ctt_str_dec = DecryptString(ctt_str, @Settings.Default.ENCRYPTION_PASSWORD);

                            if (ctt_str_dec == "NOT ENCRYPTED")
                            {
                                FileExists = 2;
                                break;
                            }
                            else
                            {
                                FileExists = 1;
                            }
                        }
                    }
                }
                
                // CHECKS IF ANY CTT FILES EXIST NOW
                if (FileExists == 1)
                {
                    loadData();
                    show_disconnections.Enabled = true;
                    show_notifications.Enabled = true;
                    show_successful.Enabled = true;
                    show_others.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Input file is invalid. Please upload zip file that is encrypted.", "Invalid file");
                }
            }
        }


        private void loadData()
        {
            // CLEAR OLD DATA
            cat_datagrid.Rows.Clear();
            cat_datagrid.Refresh();

            ulong LATENCY_COUNT = 0;
            ulong LATENCY_TOTAL = 0;
            ulong LATENCY_AVERAGE = 0;

            int DISCONNECTION_COUNT = 0;            
            int ROW_COUNT = 0;

            // DEFAULT PINGS
            var CTTD_FILES = Directory.GetFiles(@Settings.Default.FILE_PATH, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".cttd"));

            List<ulong> FILE_MATCH_ARRAY = new List<ulong>();
            foreach (string cttd in CTTD_FILES)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("LOADING PINGTEST");
                Console.WriteLine("LOADING: " + Path.GetFileName(cttd));

                string[] cttdlines = File.ReadAllLines(cttd);
                foreach (string cttd_str in cttdlines)
                {
                    var cttd_str_dec = DecryptString(cttd_str, @Settings.Default.ENCRYPTION_PASSWORD);
                    Console.WriteLine("- " + cttd_str_dec);

                    // SPLIT ARRAY VIA SEMICOLON
                    string[] cttd_str_split = cttd_str_dec.Split(';');

                    // FIRST ELEMENT IS DATESTAMP
                    string cttd_datestamp = cttd_str_split[0];

                    // ADD DATESTAMP TO ARRAY FOR MATCHING
                    FILE_MATCH_ARRAY.Add(ulong.Parse(cttd_datestamp));

                    // APPEND TO DATE STRING
                    string cttd_date = createDatestamp(cttd_datestamp);
                    string cttd_ip = cttd_str_split[1];
                    string cttd_time = cttd_str_split[2];

                    cat_datagrid.Rows.Insert(ROW_COUNT, cttd_date, cttd_ip, cttd_time);
                    
                    if (cttd_time == "0")
                    {
                        // CHECK CHECKBOX
                        if (show_disconnections.Checked)
                        {
                            cat_datagrid.Rows[ROW_COUNT].DefaultCellStyle.ForeColor = Color.Red;
                            cat_datagrid.Rows[ROW_COUNT].Visible = true;
                        }
                        else
                        {
                            cat_datagrid.Rows[ROW_COUNT].Visible = false;
                        }

                        DISCONNECTION_COUNT++;
                    }
                    else
                    {
                        // CHECK CHECKBOX
                        if (show_successful.Checked)
                        {
                            cat_datagrid.Rows[ROW_COUNT].DefaultCellStyle.ForeColor = Color.Black;
                            cat_datagrid.Rows[ROW_COUNT].Visible = true;
                        }
                        else
                        {
                            cat_datagrid.Rows[ROW_COUNT].Visible = false;
                        }

                        LATENCY_TOTAL += ulong.Parse(cttd_time);
                        LATENCY_COUNT++;
                    }

                    ROW_COUNT++;
                }
            }

            // RESOURCES
            var CTTA_FILES = Directory.GetFiles(@Settings.Default.FILE_PATH, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".ctta"));

            foreach (string ctta in CTTA_FILES)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("LOADING RESOURCES");
                Console.WriteLine("LOADING: " + Path.GetFileName(ctta));
                string[] cttalines = File.ReadAllLines(ctta);

                foreach (string ctta_str in cttalines)
                {
                    var ctta_str_dec = DecryptString(ctta_str, @Settings.Default.ENCRYPTION_PASSWORD);
                    Console.WriteLine("- " + ctta_str_dec);

                    // SPLIT ARRAY VIA SEMICOLON
                    string[] ctta_str_split = ctta_str_dec.Split(';');

                    // FIRST ELEMENT IS DATESTAMP
                    string ctta_datestamp = ctta_str_split[0];

                    // APPEND TO DATE STRING
                    string ctta_date = createDatestamp(ctta_datestamp);

                    // RAM FREE
                    double ctta_ramfree = Convert.ToDouble(ctta_str_split[1]);

                    // RAM TOTAL
                    double ctta_ramtotal = Convert.ToDouble(ctta_str_split[2]);

                    // CPU USAGE
                    int ctta_cpu = int.Parse(ctta_str_split[3]);

                    // FREE DISK
                    int ctta_disk = int.Parse(ctta_str_split[4]);

                    // ADD TO ARRAY FOR MATCHING
                    int CURRENT_FM_ELEM = 0;
                    int RESULT_EXISTS = 0;

                    foreach (ulong FM_ELEM in FILE_MATCH_ARRAY)
                    {
                        if (FM_ELEM == ulong.Parse(ctta_datestamp))
                        {

                            // FREE RAM
                            cat_datagrid.Rows[CURRENT_FM_ELEM].Cells[3].Value = (ctta_ramfree);
                            if (ctta_ramfree < 0.5)
                            {
                                cat_datagrid.Rows[CURRENT_FM_ELEM].DefaultCellStyle.ForeColor = Color.Red;
                            }

                            // RAM TOTAL
                            cat_datagrid.Rows[CURRENT_FM_ELEM].Cells[4].Value = ctta_ramtotal;

                            // CPU
                            cat_datagrid.Rows[CURRENT_FM_ELEM].Cells[5].Value = ctta_cpu + "%";
                            if (ctta_cpu > 90)
                            {
                                cat_datagrid.Rows[CURRENT_FM_ELEM].DefaultCellStyle.ForeColor = Color.Red;
                            }

                            // DISK C
                            cat_datagrid.Rows[CURRENT_FM_ELEM].Cells[6].Value = (ctta_disk / 1024) + "GB";
                            if (ctta_disk < 2048)
                            {
                                cat_datagrid.Rows[CURRENT_FM_ELEM].DefaultCellStyle.ForeColor = Color.Red;
                            }

                            RESULT_EXISTS = 1;
                        }                     

                        CURRENT_FM_ELEM++;
                    }

                    if (RESULT_EXISTS == 0) {

                        DataGridViewRow newRowValues = new DataGridViewRow();
                        newRowValues.CreateCells(cat_datagrid);
                        newRowValues.Cells[0].Value = ctta_date;

                        // CHECK CHECKBOX
                        if (show_others.Checked)
                        {
                            newRowValues.DefaultCellStyle.ForeColor = Color.Green;
                            newRowValues.Visible = true;
                        }
                        else
                        {
                            newRowValues.Visible = false;
                        }

                        // FREE RAM
                        newRowValues.Cells[3].Value = (ctta_ramfree);
                        if (ctta_ramfree < 0.5)
                        {
                            newRowValues.DefaultCellStyle.ForeColor = Color.Red;
                        }

                        // RAM TOTAL
                        newRowValues.Cells[4].Value = ctta_ramtotal;

                        // CPU
                        newRowValues.Cells[5].Value = ctta_cpu + "%";
                        if (ctta_cpu > 90)
                        {
                            newRowValues.DefaultCellStyle.ForeColor = Color.Red;
                        }

                        // DISK C
                        newRowValues.Cells[6].Value = (ctta_disk / 1024) + "GB";
                        if (ctta_disk < 2048)
                        {
                            newRowValues.DefaultCellStyle.ForeColor = Color.Red;
                        }                       

                        cat_datagrid.Rows.Insert(CURRENT_FM_ELEM, newRowValues);
                    }

                }
            }

            // SPEEDTEST
            var CTTB_FILES = Directory.GetFiles(@Settings.Default.FILE_PATH, "*.*", SearchOption.AllDirectories)
           .Where(s => s.EndsWith(".cttb"));

            foreach (string cttb in CTTB_FILES)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("LOADING SPEEDTEST");
                Console.WriteLine("LOADING: " + Path.GetFileName(cttb));

                string[] cttblines = File.ReadAllLines(cttb);

                foreach (string cttb_str in cttblines)
                {
                    var cttb_str_dec = DecryptString(cttb_str, @Settings.Default.ENCRYPTION_PASSWORD);
                    Console.WriteLine("- " + cttb_str_dec);

                    // SPLIT ARRAY VIA SEMICOLON
                    string[] cttb_str_split = cttb_str_dec.Split(';');

                    // FIRST ELEMENT IS DATESTAMP
                    string cttb_datestamp = cttb_str_split[0];
                    string cttb_date = createDatestamp(cttb_datestamp);

                    // URL ELEMENT
                    string cttb_url = cttb_str_split[1];

                    // LOOP THROUGH CURRENT RESULTS
                    int CURRENT_FM_ELEM = 0;
                    int RESULT_EXISTS = 0;
                    if(cttb_url.Length > 0) {

                        // CHECKS CURRENT RECORDS
                        foreach (ulong FM_ELEM in FILE_MATCH_ARRAY)
                        {
                            if (FM_ELEM == ulong.Parse(cttb_datestamp))
                            {
                                cat_datagrid.Rows[CURRENT_FM_ELEM].Cells[7].Value = cttb_url;
                                RESULT_EXISTS = 1;
                                CURRENT_FM_ELEM++;
                            }
                        }

                        // IF NOT IN CURRENT RECORDS IT CREATES A NEW ONE
                        if (RESULT_EXISTS == 0)
                        {
                            DataGridViewRow newRowValues = new DataGridViewRow();
                            newRowValues.CreateCells(cat_datagrid);

                            // DATA AND URL
                            newRowValues.Cells[0].Value = cttb_date;
                            newRowValues.Cells[7].Value = cttb_url;

                            newRowValues.DefaultCellStyle.ForeColor = Color.Green;

                            if (show_others.Checked)
                            {
                                newRowValues.Visible = true;
                            }
                            else
                            {
                                newRowValues.Visible = false;
                            }

                            cat_datagrid.Rows.Insert(CURRENT_FM_ELEM, newRowValues);
                        }

                    }
                }
            }

            // NOTIFICATIONS
            var CTTC_FILES = Directory.GetFiles(@Settings.Default.FILE_PATH, "*.*", SearchOption.AllDirectories)
          .Where(s => s.EndsWith(".cttc"));

            // LOOP THROUGH EACH CTTC FILE
            foreach (string cttc in CTTA_FILES)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("LOADING NOTIFICATIONS");
                Console.WriteLine("LOADING: " + Path.GetFileName(cttc));
                string[] cttclines = File.ReadAllLines(cttc);
                
                // LOOP THROUGH EACH LINE IN FILE
                foreach (string cttc_str in cttclines)
                {
                    var cttc_str_dec = DecryptString(cttc_str, @Settings.Default.ENCRYPTION_PASSWORD);
                    Console.WriteLine("- " + cttc_str_dec);

                    // SPLIT ARRAY VIA SEMICOLON
                    string[] cttc_str_split = cttc_str_dec.Split(';');

                    // FIRST ELEMENT IS DATESTAMP
                    string cttc_datestamp = cttc_str_split[0];
                    string cttc_date = createDatestamp(cttc_datestamp);

                    DataGridViewRow newRowValues = new DataGridViewRow();
                    newRowValues.CreateCells(cat_datagrid);
                    newRowValues.DefaultCellStyle.ForeColor = Color.Blue;
                    newRowValues.Cells[0].Value = cttc_date;

                    // CHECK CHECKBOX
                    if (show_notifications.Checked)
                    {
                        newRowValues.Visible = true;
                    }
                    else
                    {
                        newRowValues.Visible = false;
                    }

                    cat_datagrid.Rows.Insert((cat_datagrid.Rows.Count - 1), newRowValues);
                }
            }

            LATENCY_AVERAGE = LATENCY_TOTAL / LATENCY_COUNT;

            // LATENCY
            string overviewText = "";
            overviewText += "Disconnection Count: " + DISCONNECTION_COUNT + "  -  ";
            overviewText += "Latency Avg: " + LATENCY_AVERAGE + "ms ";

            overview_textbox.Text = overviewText;
            cat_datagrid.Sort(cat_datagrid.Columns["timestamp"], ListSortDirection.Descending);
        }

        public static string createDatestamp(string ds)
        {
            // SPLIT ALL CHARACTERS INTO ARRAY VALUES
            char[] dsc = ds.ToCharArray();

            // TYPECAST INTO STRING
            string dsc_yr = dsc[0] + "" + dsc[1] + "" + dsc[2] + "" + dsc[3];
            string dsc_m = "" + dsc[4] + dsc[5];
            string dsc_d = "" + dsc[6] + dsc[7];
            string dsc_hr = "" + dsc[8] + dsc[9];
            string dsc_min = "" + dsc[10] + dsc[11];
            string dsc_sec = "" + dsc[12] + dsc[13];

            // APPEND TO DATE STRING
            string dscreturn = dsc_d + "/" + dsc_m + "/" + dsc_yr + " - " + dsc_hr + ":" + dsc_min + ":" + dsc_sec;
            return dscreturn;
        }

        private void show_disconnections_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }
               
        private void show_successful_CheckedChanged_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void show_notifications_CheckedChanged_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void show_others_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
