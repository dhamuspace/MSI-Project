namespace MSPOSBACKOFFICE
{
    partial class frmBackup
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
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBackupDatabaseName = new System.Windows.Forms.ComboBox();
            this.cmbBackupServerName = new System.Windows.Forms.ComboBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.txtFolderLocation = new System.Windows.Forms.TextBox();
            this.btnFolderLocation = new System.Windows.Forms.Button();
            this.txtBackUpFileName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.Pnl_Back.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back.Controls.Add(this.label5);
            this.Pnl_Back.Controls.Add(this.label4);
            this.Pnl_Back.Controls.Add(this.label3);
            this.Pnl_Back.Controls.Add(this.label2);
            this.Pnl_Back.Controls.Add(this.cmbBackupDatabaseName);
            this.Pnl_Back.Controls.Add(this.cmbBackupServerName);
            this.Pnl_Back.Controls.Add(this.btnBackup);
            this.Pnl_Back.Controls.Add(this.txtFolderLocation);
            this.Pnl_Back.Controls.Add(this.btnFolderLocation);
            this.Pnl_Back.Controls.Add(this.txtBackUpFileName);
            this.Pnl_Back.Location = new System.Drawing.Point(3, 57);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1012, 479);
            this.Pnl_Back.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(54, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Desination File Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(54, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 20);
            this.label4.TabIndex = 35;
            this.label4.Text = "Backp File Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(54, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 20);
            this.label3.TabIndex = 34;
            this.label3.Text = "Backup Database Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(54, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Server Name";
            // 
            // cmbBackupDatabaseName
            // 
            this.cmbBackupDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBackupDatabaseName.FormattingEnabled = true;
            this.cmbBackupDatabaseName.Location = new System.Drawing.Point(246, 90);
            this.cmbBackupDatabaseName.Name = "cmbBackupDatabaseName";
            this.cmbBackupDatabaseName.Size = new System.Drawing.Size(260, 23);
            this.cmbBackupDatabaseName.TabIndex = 32;
            this.cmbBackupDatabaseName.SelectedIndexChanged += new System.EventHandler(this.cmbBackupDatabaseName_SelectedIndexChanged);
            // 
            // cmbBackupServerName
            // 
            this.cmbBackupServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBackupServerName.FormattingEnabled = true;
            this.cmbBackupServerName.Location = new System.Drawing.Point(246, 47);
            this.cmbBackupServerName.Name = "cmbBackupServerName";
            this.cmbBackupServerName.Size = new System.Drawing.Size(260, 23);
            this.cmbBackupServerName.TabIndex = 30;
            this.cmbBackupServerName.SelectedIndexChanged += new System.EventHandler(this.cmbBackupServerName_SelectedIndexChanged);
            // 
            // btnBackup
            // 
            this.btnBackup.BackColor = System.Drawing.Color.White;
            this.btnBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.Location = new System.Drawing.Point(246, 220);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(160, 42);
            this.btnBackup.TabIndex = 27;
            this.btnBackup.Text = "Database Backup";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // txtFolderLocation
            // 
            this.txtFolderLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolderLocation.Location = new System.Drawing.Point(246, 132);
            this.txtFolderLocation.Name = "txtFolderLocation";
            this.txtFolderLocation.Size = new System.Drawing.Size(260, 21);
            this.txtFolderLocation.TabIndex = 23;
            // 
            // btnFolderLocation
            // 
            this.btnFolderLocation.BackColor = System.Drawing.Color.White;
            this.btnFolderLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolderLocation.ForeColor = System.Drawing.Color.Black;
            this.btnFolderLocation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFolderLocation.Location = new System.Drawing.Point(512, 131);
            this.btnFolderLocation.Name = "btnFolderLocation";
            this.btnFolderLocation.Size = new System.Drawing.Size(38, 23);
            this.btnFolderLocation.TabIndex = 24;
            this.btnFolderLocation.Text = "...";
            this.btnFolderLocation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFolderLocation.UseVisualStyleBackColor = false;
            this.btnFolderLocation.Click += new System.EventHandler(this.btnFolderLocation_Click);
            // 
            // txtBackUpFileName
            // 
            this.txtBackUpFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackUpFileName.Location = new System.Drawing.Point(248, 172);
            this.txtBackUpFileName.Name = "txtBackUpFileName";
            this.txtBackUpFileName.Size = new System.Drawing.Size(260, 21);
            this.txtBackUpFileName.TabIndex = 26;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Location = new System.Drawing.Point(870, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(117, 42);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(3, 5);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1012, 52);
            this.Pnl_Header.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 25);
            this.label1.TabIndex = 36;
            this.label1.Text = "Database Backup";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnClear);
            this.Pnl_Footer.Controls.Add(this.btnClose);
            this.Pnl_Footer.Location = new System.Drawing.Point(3, 536);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1012, 52);
            this.Pnl_Footer.TabIndex = 48;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Location = new System.Drawing.Point(735, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(117, 42);
            this.btnClear.TabIndex = 33;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 590);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBackup";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.ComboBox cmbBackupServerName;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TextBox txtFolderLocation;
        private System.Windows.Forms.Button btnFolderLocation;
        private System.Windows.Forms.TextBox txtBackUpFileName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbBackupDatabaseName;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

