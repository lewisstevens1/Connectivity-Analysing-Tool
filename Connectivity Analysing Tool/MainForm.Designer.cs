namespace Connectivity_Analysing_Tool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cat_loadzip = new System.Windows.Forms.Button();
            this.show_successful = new System.Windows.Forms.CheckBox();
            this.show_disconnections = new System.Windows.Forms.CheckBox();
            this.show_notifications = new System.Windows.Forms.CheckBox();
            this.cat_datagrid = new System.Windows.Forms.DataGridView();
            this.timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pingaddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pingresponse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ramfree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ramtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cpu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diskc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speedtestid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overview_textbox = new System.Windows.Forms.TextBox();
            this.show_others = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.cat_datagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // cat_loadzip
            // 
            this.cat_loadzip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cat_loadzip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cat_loadzip.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            this.cat_loadzip.Location = new System.Drawing.Point(1251, 447);
            this.cat_loadzip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cat_loadzip.Name = "cat_loadzip";
            this.cat_loadzip.Size = new System.Drawing.Size(130, 32);
            this.cat_loadzip.TabIndex = 0;
            this.cat_loadzip.Text = "LOAD ZIP";
            this.cat_loadzip.UseVisualStyleBackColor = true;
            this.cat_loadzip.Click += new System.EventHandler(this.cat_loadzip_Click);
            // 
            // show_successful
            // 
            this.show_successful.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.show_successful.AutoSize = true;
            this.show_successful.Checked = true;
            this.show_successful.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_successful.Enabled = false;
            this.show_successful.Location = new System.Drawing.Point(14, 455);
            this.show_successful.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.show_successful.Name = "show_successful";
            this.show_successful.Size = new System.Drawing.Size(98, 21);
            this.show_successful.TabIndex = 6;
            this.show_successful.Text = "Successful";
            this.show_successful.UseVisualStyleBackColor = true;
            this.show_successful.CheckedChanged += new System.EventHandler(this.show_successful_CheckedChanged_1);
            // 
            // show_disconnections
            // 
            this.show_disconnections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.show_disconnections.AutoSize = true;
            this.show_disconnections.Checked = true;
            this.show_disconnections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_disconnections.Enabled = false;
            this.show_disconnections.ForeColor = System.Drawing.Color.Red;
            this.show_disconnections.Location = new System.Drawing.Point(231, 455);
            this.show_disconnections.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.show_disconnections.Name = "show_disconnections";
            this.show_disconnections.Size = new System.Drawing.Size(126, 21);
            this.show_disconnections.TabIndex = 2;
            this.show_disconnections.Text = "Disconnections";
            this.show_disconnections.UseVisualStyleBackColor = true;
            this.show_disconnections.CheckedChanged += new System.EventHandler(this.show_disconnections_CheckedChanged);
            // 
            // show_notifications
            // 
            this.show_notifications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.show_notifications.AutoSize = true;
            this.show_notifications.Checked = true;
            this.show_notifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_notifications.Enabled = false;
            this.show_notifications.ForeColor = System.Drawing.Color.Blue;
            this.show_notifications.Location = new System.Drawing.Point(118, 455);
            this.show_notifications.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.show_notifications.Name = "show_notifications";
            this.show_notifications.Size = new System.Drawing.Size(107, 21);
            this.show_notifications.TabIndex = 5;
            this.show_notifications.Text = "Notifications";
            this.show_notifications.UseVisualStyleBackColor = true;
            this.show_notifications.CheckedChanged += new System.EventHandler(this.show_notifications_CheckedChanged_1);
            // 
            // cat_datagrid
            // 
            this.cat_datagrid.AllowUserToAddRows = false;
            this.cat_datagrid.AllowUserToDeleteRows = false;
            this.cat_datagrid.AllowUserToOrderColumns = true;
            this.cat_datagrid.AllowUserToResizeRows = false;
            this.cat_datagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cat_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cat_datagrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.cat_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cat_datagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timestamp,
            this.pingaddress,
            this.pingresponse,
            this.ramfree,
            this.ramtotal,
            this.cpu,
            this.diskc,
            this.speedtestid});
            this.cat_datagrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.cat_datagrid.Location = new System.Drawing.Point(12, 13);
            this.cat_datagrid.Name = "cat_datagrid";
            this.cat_datagrid.ReadOnly = true;
            this.cat_datagrid.RowTemplate.Height = 24;
            this.cat_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cat_datagrid.Size = new System.Drawing.Size(1369, 422);
            this.cat_datagrid.TabIndex = 3;
            // 
            // timestamp
            // 
            this.timestamp.HeaderText = "Timestamp";
            this.timestamp.Name = "timestamp";
            this.timestamp.ReadOnly = true;
            // 
            // pingaddress
            // 
            this.pingaddress.HeaderText = "Ping Address";
            this.pingaddress.Name = "pingaddress";
            this.pingaddress.ReadOnly = true;
            // 
            // pingresponse
            // 
            this.pingresponse.HeaderText = "Ping Response";
            this.pingresponse.Name = "pingresponse";
            this.pingresponse.ReadOnly = true;
            // 
            // ramfree
            // 
            this.ramfree.HeaderText = "RAM Free";
            this.ramfree.Name = "ramfree";
            this.ramfree.ReadOnly = true;
            // 
            // ramtotal
            // 
            this.ramtotal.HeaderText = "RAM Total";
            this.ramtotal.Name = "ramtotal";
            this.ramtotal.ReadOnly = true;
            // 
            // cpu
            // 
            this.cpu.HeaderText = "CPU";
            this.cpu.Name = "cpu";
            this.cpu.ReadOnly = true;
            // 
            // diskc
            // 
            this.diskc.HeaderText = "Disk (C)";
            this.diskc.Name = "diskc";
            this.diskc.ReadOnly = true;
            // 
            // speedtestid
            // 
            this.speedtestid.HeaderText = "Speedtest ID";
            this.speedtestid.Name = "speedtestid";
            this.speedtestid.ReadOnly = true;
            // 
            // overview_textbox
            // 
            this.overview_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overview_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.overview_textbox.Location = new System.Drawing.Point(366, 456);
            this.overview_textbox.Name = "overview_textbox";
            this.overview_textbox.ReadOnly = true;
            this.overview_textbox.Size = new System.Drawing.Size(879, 15);
            this.overview_textbox.TabIndex = 7;
            this.overview_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // show_others
            // 
            this.show_others.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.show_others.AutoSize = true;
            this.show_others.Checked = true;
            this.show_others.CheckState = System.Windows.Forms.CheckState.Checked;
            this.show_others.Enabled = false;
            this.show_others.ForeColor = System.Drawing.Color.Green;
            this.show_others.Location = new System.Drawing.Point(363, 455);
            this.show_others.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.show_others.Name = "show_others";
            this.show_others.Size = new System.Drawing.Size(66, 21);
            this.show_others.TabIndex = 8;
            this.show_others.Text = "Other";
            this.show_others.UseVisualStyleBackColor = true;
            this.show_others.CheckedChanged += new System.EventHandler(this.show_others_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 483);
            this.Controls.Add(this.show_others);
            this.Controls.Add(this.overview_textbox);
            this.Controls.Add(this.cat_loadzip);
            this.Controls.Add(this.show_successful);
            this.Controls.Add(this.cat_datagrid);
            this.Controls.Add(this.show_notifications);
            this.Controls.Add(this.show_disconnections);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1409, 530);
            this.Name = "MainForm";
            this.Text = "CAT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cat_datagrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cat_loadzip;
        private System.Windows.Forms.CheckBox show_disconnections;
        private System.Windows.Forms.CheckBox show_successful;
        private System.Windows.Forms.CheckBox show_notifications;
        private System.Windows.Forms.DataGridView cat_datagrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn pingaddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn pingresponse;
        private System.Windows.Forms.DataGridViewTextBoxColumn ramfree;
        private System.Windows.Forms.DataGridViewTextBoxColumn ramtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn cpu;
        private System.Windows.Forms.DataGridViewTextBoxColumn diskc;
        private System.Windows.Forms.DataGridViewTextBoxColumn speedtestid;
        private System.Windows.Forms.TextBox overview_textbox;
        private System.Windows.Forms.CheckBox show_others;
    }
}

