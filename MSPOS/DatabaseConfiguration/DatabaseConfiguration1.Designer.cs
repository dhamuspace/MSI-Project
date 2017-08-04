namespace DatabaseConfiguration
{
    partial class DatabaseConfiguration1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseConfiguration1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreateNew = new System.Windows.Forms.Button();
            this.linkBtnBackup = new System.Windows.Forms.Button();
            this.btnLoadSettings = new System.Windows.Forms.Button();
            this.linkBtnConfiguration = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.TabStripDBConfiguration = new System.Windows.Forms.TabControl();
            this.tabConfiguration = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCustomConnectionString = new System.Windows.Forms.TextBox();
            this.chkCustomConnectionString = new System.Windows.Forms.CheckBox();
            this.cmbTimeOut = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.radioServerAuthentication = new System.Windows.Forms.RadioButton();
            this.radioWindowsAuthentication = new System.Windows.Forms.RadioButton();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDatabaseType = new System.Windows.Forms.ComboBox();
            this.tabBackUp = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtBackupDatabaseName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBackup = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolderLocation = new System.Windows.Forms.TextBox();
            this.btnFolderLocation = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBackUpFileName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbBackupServerName = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSqlPassWord = new System.Windows.Forms.TextBox();
            this.txtSqlLogin = new System.Windows.Forms.TextBox();
            this.btnMakeNewDB = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNewDBName = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.TabStripDBConfiguration.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabBackUp.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnCreateNew);
            this.panel1.Controls.Add(this.linkBtnBackup);
            this.panel1.Controls.Add(this.btnLoadSettings);
            this.panel1.Controls.Add(this.linkBtnConfiguration);
            this.panel1.Controls.Add(this.btnSaveSettings);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 403);
            this.panel1.TabIndex = 0;
            // 
            // btnCreateNew
            // 
            this.btnCreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateNew.Location = new System.Drawing.Point(5, 84);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(115, 28);
            this.btnCreateNew.TabIndex = 3;
            this.btnCreateNew.Text = "Create New DB";
            this.btnCreateNew.UseVisualStyleBackColor = true;
            this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
            // 
            // linkBtnBackup
            // 
            this.linkBtnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.linkBtnBackup.Location = new System.Drawing.Point(6, 50);
            this.linkBtnBackup.Name = "linkBtnBackup";
            this.linkBtnBackup.Size = new System.Drawing.Size(115, 28);
            this.linkBtnBackup.TabIndex = 2;
            this.linkBtnBackup.Text = "Backup";
            this.linkBtnBackup.UseVisualStyleBackColor = true;
            this.linkBtnBackup.Click += new System.EventHandler(this.linkBtnBackup_Click);
            // 
            // btnLoadSettings
            // 
            this.btnLoadSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadSettings.Location = new System.Drawing.Point(6, 366);
            this.btnLoadSettings.Name = "btnLoadSettings";
            this.btnLoadSettings.Size = new System.Drawing.Size(115, 28);
            this.btnLoadSettings.TabIndex = 5;
            this.btnLoadSettings.Text = "Load Setting";
            this.btnLoadSettings.UseVisualStyleBackColor = true;
            this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
            // 
            // linkBtnConfiguration
            // 
            this.linkBtnConfiguration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.linkBtnConfiguration.Location = new System.Drawing.Point(6, 16);
            this.linkBtnConfiguration.Name = "linkBtnConfiguration";
            this.linkBtnConfiguration.Size = new System.Drawing.Size(115, 28);
            this.linkBtnConfiguration.TabIndex = 1;
            this.linkBtnConfiguration.Text = "Configuration";
            this.linkBtnConfiguration.UseVisualStyleBackColor = true;
            this.linkBtnConfiguration.Click += new System.EventHandler(this.linkBtnConfiguration_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Location = new System.Drawing.Point(6, 332);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(115, 28);
            this.btnSaveSettings.TabIndex = 4;
            this.btnSaveSettings.Text = "Save Setting";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // TabStripDBConfiguration
            // 
            this.TabStripDBConfiguration.Controls.Add(this.tabConfiguration);
            this.TabStripDBConfiguration.Controls.Add(this.tabBackUp);
            this.TabStripDBConfiguration.Location = new System.Drawing.Point(135, 1);
            this.TabStripDBConfiguration.Name = "TabStripDBConfiguration";
            this.TabStripDBConfiguration.SelectedIndex = 0;
            this.TabStripDBConfiguration.Size = new System.Drawing.Size(534, 407);
            this.TabStripDBConfiguration.TabIndex = 1;
            this.TabStripDBConfiguration.SelectedIndexChanged += new System.EventHandler(this.TabStripDBConfiguration_SelectedIndexChanged);
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.Controls.Add(this.groupBox2);
            this.tabConfiguration.Controls.Add(this.groupBox1);
            this.tabConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguration.Size = new System.Drawing.Size(526, 381);
            this.tabConfiguration.TabIndex = 0;
            this.tabConfiguration.Text = "        Configuration         ";
            this.tabConfiguration.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCustomConnectionString);
            this.groupBox2.Controls.Add(this.chkCustomConnectionString);
            this.groupBox2.Controls.Add(this.cmbTimeOut);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnTestConnection);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.radioServerAuthentication);
            this.groupBox2.Controls.Add(this.radioWindowsAuthentication);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtLogin);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDatabaseName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbServerName);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(27, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 293);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL Server Setting";
            // 
            // txtCustomConnectionString
            // 
            this.txtCustomConnectionString.Location = new System.Drawing.Point(22, 119);
            this.txtCustomConnectionString.Multiline = true;
            this.txtCustomConnectionString.Name = "txtCustomConnectionString";
            this.txtCustomConnectionString.Size = new System.Drawing.Size(447, 129);
            this.txtCustomConnectionString.TabIndex = 15;
            this.txtCustomConnectionString.Visible = false;
            // 
            // chkCustomConnectionString
            // 
            this.chkCustomConnectionString.AutoSize = true;
            this.chkCustomConnectionString.Location = new System.Drawing.Point(148, 55);
            this.chkCustomConnectionString.Name = "chkCustomConnectionString";
            this.chkCustomConnectionString.Size = new System.Drawing.Size(177, 20);
            this.chkCustomConnectionString.TabIndex = 14;
            this.chkCustomConnectionString.Text = "Custom Connection string";
            this.chkCustomConnectionString.UseVisualStyleBackColor = true;
            this.chkCustomConnectionString.CheckedChanged += new System.EventHandler(this.chkCustomConnectionString_CheckedChanged);
            this.chkCustomConnectionString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkCustomConnectionString_KeyDown);
            // 
            // cmbTimeOut
            // 
            this.cmbTimeOut.DropDownWidth = 10;
            this.cmbTimeOut.FormattingEnabled = true;
            this.cmbTimeOut.Items.AddRange(new object[] {
            "Microsoft SQL Server"});
            this.cmbTimeOut.Location = new System.Drawing.Point(148, 257);
            this.cmbTimeOut.Name = "cmbTimeOut";
            this.cmbTimeOut.Size = new System.Drawing.Size(150, 24);
            this.cmbTimeOut.TabIndex = 8;
            this.cmbTimeOut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTimeOut_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "Timeout";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(315, 254);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(144, 28);
            this.btnTestConnection.TabIndex = 9;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Security Type";
            // 
            // radioServerAuthentication
            // 
            this.radioServerAuthentication.AutoSize = true;
            this.radioServerAuthentication.Location = new System.Drawing.Point(150, 143);
            this.radioServerAuthentication.Name = "radioServerAuthentication";
            this.radioServerAuthentication.Size = new System.Drawing.Size(181, 20);
            this.radioServerAuthentication.TabIndex = 5;
            this.radioServerAuthentication.Text = "SQL Server Authentication";
            this.radioServerAuthentication.UseVisualStyleBackColor = true;
            this.radioServerAuthentication.CheckedChanged += new System.EventHandler(this.radioServerAuthentication_CheckedChanged);
            this.radioServerAuthentication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioWindowsAuthentication_KeyDown);
            // 
            // radioWindowsAuthentication
            // 
            this.radioWindowsAuthentication.AutoSize = true;
            this.radioWindowsAuthentication.Checked = true;
            this.radioWindowsAuthentication.Location = new System.Drawing.Point(150, 119);
            this.radioWindowsAuthentication.Name = "radioWindowsAuthentication";
            this.radioWindowsAuthentication.Size = new System.Drawing.Size(214, 20);
            this.radioWindowsAuthentication.TabIndex = 4;
            this.radioWindowsAuthentication.TabStop = true;
            this.radioWindowsAuthentication.Text = "Windows Authentication ( SPPI )";
            this.radioWindowsAuthentication.UseVisualStyleBackColor = true;
            this.radioWindowsAuthentication.CheckedChanged += new System.EventHandler(this.radioWindowsAuthentication_CheckedChanged);
            this.radioWindowsAuthentication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.radioWindowsAuthentication_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(148, 220);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(309, 22);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(148, 181);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(311, 22);
            this.txtLogin.TabIndex = 6;
            this.txtLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLogin_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Login";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(148, 83);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(311, 22);
            this.txtDatabaseName.TabIndex = 3;
            this.txtDatabaseName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDatabaseName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Name";
            // 
            // cmbServerName
            // 
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(148, 21);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(311, 24);
            this.cmbServerName.TabIndex = 2;
            this.cmbServerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbServerName_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbDatabaseType);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Database Type";
            // 
            // cmbDatabaseType
            // 
            this.cmbDatabaseType.FormattingEnabled = true;
            this.cmbDatabaseType.Items.AddRange(new object[] {
            "Microsoft SQL Server"});
            this.cmbDatabaseType.Location = new System.Drawing.Point(148, 22);
            this.cmbDatabaseType.Name = "cmbDatabaseType";
            this.cmbDatabaseType.Size = new System.Drawing.Size(311, 24);
            this.cmbDatabaseType.TabIndex = 1;
            this.cmbDatabaseType.Text = "Microsoft SQL Server";
            this.cmbDatabaseType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDatabaseType_KeyDown);
            // 
            // tabBackUp
            // 
            this.tabBackUp.Controls.Add(this.groupBox4);
            this.tabBackUp.Controls.Add(this.groupBox3);
            this.tabBackUp.Controls.Add(this.groupBox5);
            this.tabBackUp.Location = new System.Drawing.Point(4, 22);
            this.tabBackUp.Name = "tabBackUp";
            this.tabBackUp.Size = new System.Drawing.Size(526, 381);
            this.tabBackUp.TabIndex = 1;
            this.tabBackUp.Text = "         Backup           ";
            this.tabBackUp.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtBackupDatabaseName);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.btnBackup);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtFolderLocation);
            this.groupBox4.Controls.Add(this.btnFolderLocation);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtBackUpFileName);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(24, 224);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(475, 153);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Backup Database Details";
            // 
            // txtBackupDatabaseName
            // 
            this.txtBackupDatabaseName.Location = new System.Drawing.Point(169, 22);
            this.txtBackupDatabaseName.Name = "txtBackupDatabaseName";
            this.txtBackupDatabaseName.Size = new System.Drawing.Size(260, 22);
            this.txtBackupDatabaseName.TabIndex = 21;
            this.txtBackupDatabaseName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBackupDatabaseName_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 16);
            this.label11.TabIndex = 20;
            this.label11.Text = "Backup Data Source";
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(169, 121);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(260, 27);
            this.btnBackup.TabIndex = 17;
            this.btnBackup.Text = "Database Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Backup File Location";
            // 
            // txtFolderLocation
            // 
            this.txtFolderLocation.Location = new System.Drawing.Point(169, 56);
            this.txtFolderLocation.Name = "txtFolderLocation";
            this.txtFolderLocation.Size = new System.Drawing.Size(260, 22);
            this.txtFolderLocation.TabIndex = 13;
            this.txtFolderLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolderLocation_KeyDown);
            // 
            // btnFolderLocation
            // 
            this.btnFolderLocation.Location = new System.Drawing.Point(435, 55);
            this.btnFolderLocation.Name = "btnFolderLocation";
            this.btnFolderLocation.Size = new System.Drawing.Size(35, 23);
            this.btnFolderLocation.TabIndex = 14;
            this.btnFolderLocation.Text = "...";
            this.btnFolderLocation.UseVisualStyleBackColor = true;
            this.btnFolderLocation.Click += new System.EventHandler(this.btnFolderLocation_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "Destination File Name";
            // 
            // txtBackUpFileName
            // 
            this.txtBackUpFileName.Location = new System.Drawing.Point(170, 89);
            this.txtBackUpFileName.Name = "txtBackUpFileName";
            this.txtBackUpFileName.Size = new System.Drawing.Size(260, 22);
            this.txtBackUpFileName.TabIndex = 16;
            this.txtBackUpFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBackUpFileName_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cmbBackupServerName);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(23, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(475, 56);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Server Details";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Server Name";
            // 
            // cmbBackupServerName
            // 
            this.cmbBackupServerName.FormattingEnabled = true;
            this.cmbBackupServerName.Location = new System.Drawing.Point(169, 22);
            this.cmbBackupServerName.Name = "cmbBackupServerName";
            this.cmbBackupServerName.Size = new System.Drawing.Size(260, 24);
            this.cmbBackupServerName.TabIndex = 1;
            this.cmbBackupServerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBackupServerName_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txtSqlPassWord);
            this.groupBox5.Controls.Add(this.txtSqlLogin);
            this.groupBox5.Controls.Add(this.btnMakeNewDB);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.txtNewDBName);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(23, 78);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(475, 144);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Create Database Details";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 89);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "Passowrd";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 16);
            this.label12.TabIndex = 20;
            this.label12.Text = "Login";
            // 
            // txtSqlPassWord
            // 
            this.txtSqlPassWord.Location = new System.Drawing.Point(169, 84);
            this.txtSqlPassWord.Name = "txtSqlPassWord";
            this.txtSqlPassWord.PasswordChar = '*';
            this.txtSqlPassWord.Size = new System.Drawing.Size(260, 22);
            this.txtSqlPassWord.TabIndex = 4;
            // 
            // txtSqlLogin
            // 
            this.txtSqlLogin.Location = new System.Drawing.Point(169, 54);
            this.txtSqlLogin.Name = "txtSqlLogin";
            this.txtSqlLogin.Size = new System.Drawing.Size(260, 22);
            this.txtSqlLogin.TabIndex = 3;
            // 
            // btnMakeNewDB
            // 
            this.btnMakeNewDB.Location = new System.Drawing.Point(169, 112);
            this.btnMakeNewDB.Name = "btnMakeNewDB";
            this.btnMakeNewDB.Size = new System.Drawing.Size(260, 27);
            this.btnMakeNewDB.TabIndex = 5;
            this.btnMakeNewDB.Text = "Make New Database";
            this.btnMakeNewDB.UseVisualStyleBackColor = true;
            this.btnMakeNewDB.Click += new System.EventHandler(this.btnMakeNewDB_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 16);
            this.label13.TabIndex = 15;
            this.label13.Text = "New Database Name";
            // 
            // txtNewDBName
            // 
            this.txtNewDBName.Location = new System.Drawing.Point(170, 25);
            this.txtNewDBName.Name = "txtNewDBName";
            this.txtNewDBName.Size = new System.Drawing.Size(260, 22);
            this.txtNewDBName.TabIndex = 2;
            this.txtNewDBName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewDBName_KeyDown);
            // 
            // DatabaseConfiguration1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 411);
            this.Controls.Add(this.TabStripDBConfiguration);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DatabaseConfiguration1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Configuration Utility";
            this.Load += new System.EventHandler(this.DatabaseConfiguration1_Load);
            this.panel1.ResumeLayout(false);
            this.TabStripDBConfiguration.ResumeLayout(false);
            this.tabConfiguration.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabBackUp.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl TabStripDBConfiguration;
        private System.Windows.Forms.TabPage tabConfiguration;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioServerAuthentication;
        private System.Windows.Forms.RadioButton radioWindowsAuthentication;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDatabaseType;
        private System.Windows.Forms.Button linkBtnConfiguration;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.ComboBox cmbTimeOut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkCustomConnectionString;
        private System.Windows.Forms.TextBox txtCustomConnectionString;
        private System.Windows.Forms.Button btnLoadSettings;
        private System.Windows.Forms.Button linkBtnBackup;
        private System.Windows.Forms.TabPage tabBackUp;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBackUpFileName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnFolderLocation;
        private System.Windows.Forms.TextBox txtFolderLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TextBox txtBackupDatabaseName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbBackupServerName;
        private System.Windows.Forms.Button btnCreateNew;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnMakeNewDB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNewDBName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSqlPassWord;
        private System.Windows.Forms.TextBox txtSqlLogin;
    }
}

