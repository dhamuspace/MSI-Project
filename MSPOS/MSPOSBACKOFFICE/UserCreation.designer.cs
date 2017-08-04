namespace MSPOSBACKOFFICE
{
    partial class UserCreation
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbUserType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.lbl_stckbanner = new System.Windows.Forms.Label();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlUserName = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lstUserName = new System.Windows.Forms.ListBox();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.txtDiscountRange = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbResettle = new System.Windows.Forms.ComboBox();
            this.CmpStopQty = new System.Windows.Forms.ComboBox();
            this.CmpStopRate = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbAllowVoid = new System.Windows.Forms.ComboBox();
            this.cmbAllowReturn = new System.Windows.Forms.ComboBox();
            this.cmbViewManagerReport = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.CmbSystemName = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbViewCash = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.HAPayment = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.comboStCounter = new System.Windows.Forms.ComboBox();
            this.comboCashDrawer = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.ComboBranch = new System.Windows.Forms.ComboBox();
            this.Pnl_Header.SuspendLayout();
            this.pnlUserName.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(93, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUserName.Location = new System.Drawing.Point(257, 141);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(325, 23);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPassword.Location = new System.Drawing.Point(257, 190);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(325, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(93, 193);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSave.Location = new System.Drawing.Point(777, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 38);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.Location = new System.Drawing.Point(852, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClose.Location = new System.Drawing.Point(927, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 38);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Exit";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbUserType
            // 
            this.cmbUserType.FormattingEnabled = true;
            this.cmbUserType.Items.AddRange(new object[] {
            "Admin",
            "User"});
            this.cmbUserType.Location = new System.Drawing.Point(257, 93);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Size = new System.Drawing.Size(325, 24);
            this.cmbUserType.TabIndex = 1;
            this.cmbUserType.SelectedIndexChanged += new System.EventHandler(this.cmbUserType_SelectedIndexChanged);
            this.cmbUserType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUserType_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label4.Location = new System.Drawing.Point(93, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "User Type";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(257, 239);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(325, 23);
            this.txtConfirmPassword.TabIndex = 4;
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.txtConfirmPassword_TextChanged);
            this.txtConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmPassword_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label5.Location = new System.Drawing.Point(93, 242);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Confirm Password";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_stckbanner);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 47);
            this.Pnl_Header.TabIndex = 33;
            // 
            // lbl_stckbanner
            // 
            this.lbl_stckbanner.AutoSize = true;
            this.lbl_stckbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_stckbanner.ForeColor = System.Drawing.Color.White;
            this.lbl_stckbanner.Location = new System.Drawing.Point(0, 5);
            this.lbl_stckbanner.Name = "lbl_stckbanner";
            this.lbl_stckbanner.Size = new System.Drawing.Size(120, 20);
            this.lbl_stckbanner.TabIndex = 0;
            this.lbl_stckbanner.Text = "User Creation";
            // 
            // txtCounter
            // 
            this.txtCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCounter.Location = new System.Drawing.Point(257, 287);
            this.txtCounter.Margin = new System.Windows.Forms.Padding(4);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(325, 23);
            this.txtCounter.TabIndex = 5;
            this.txtCounter.Click += new System.EventHandler(this.txtCounter_Click);
            this.txtCounter.TextChanged += new System.EventHandler(this.txtCounter_TextChanged);
            this.txtCounter.Enter += new System.EventHandler(this.txtCounter_Click);
            this.txtCounter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCounter_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(93, 293);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "Counter";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // pnlUserName
            // 
            this.pnlUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserName.Controls.Add(this.label6);
            this.pnlUserName.Controls.Add(this.lstUserName);
            this.pnlUserName.Location = new System.Drawing.Point(589, 93);
            this.pnlUserName.Name = "pnlUserName";
            this.pnlUserName.Size = new System.Drawing.Size(271, 225);
            this.pnlUserName.TabIndex = 37;
            this.pnlUserName.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label6.Location = new System.Drawing.Point(75, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 24);
            this.label6.TabIndex = 36;
            this.label6.Text = "Select One";
            // 
            // lstUserName
            // 
            this.lstUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstUserName.FormattingEnabled = true;
            this.lstUserName.ItemHeight = 16;
            this.lstUserName.Location = new System.Drawing.Point(7, 35);
            this.lstUserName.Name = "lstUserName";
            this.lstUserName.Size = new System.Drawing.Size(254, 178);
            this.lstUserName.TabIndex = 35;
            this.lstUserName.Click += new System.EventHandler(this.lstUserName_Click);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnCancel);
            this.Pnl_Footer.Controls.Add(this.btnSave);
            this.Pnl_Footer.Controls.Add(this.btnClose);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 545);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 47);
            this.Pnl_Footer.TabIndex = 34;
            // 
            // txtDiscountRange
            // 
            this.txtDiscountRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscountRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDiscountRange.Location = new System.Drawing.Point(257, 380);
            this.txtDiscountRange.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscountRange.Name = "txtDiscountRange";
            this.txtDiscountRange.Size = new System.Drawing.Size(325, 23);
            this.txtDiscountRange.TabIndex = 7;
            this.txtDiscountRange.TextChanged += new System.EventHandler(this.txtDiscountRange_TextChanged);
            this.txtDiscountRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscountRange_KeyDown);
            this.txtDiscountRange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountRange_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label7.Location = new System.Drawing.Point(93, 383);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 17);
            this.label7.TabIndex = 39;
            this.label7.Text = "Discount Range";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label8.Location = new System.Drawing.Point(93, 430);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 41;
            this.label8.Text = "Allow Re-Settle";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // cmbResettle
            // 
            this.cmbResettle.FormattingEnabled = true;
            this.cmbResettle.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbResettle.Location = new System.Drawing.Point(257, 423);
            this.cmbResettle.Name = "cmbResettle";
            this.cmbResettle.Size = new System.Drawing.Size(118, 24);
            this.cmbResettle.TabIndex = 8;
            this.cmbResettle.Text = "Yes";
            this.cmbResettle.SelectedIndexChanged += new System.EventHandler(this.cmbResettle_SelectedIndexChanged);
            this.cmbResettle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbResettle_KeyDown);
            // 
            // CmpStopQty
            // 
            this.CmpStopQty.FormattingEnabled = true;
            this.CmpStopQty.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.CmpStopQty.Location = new System.Drawing.Point(257, 468);
            this.CmpStopQty.Name = "CmpStopQty";
            this.CmpStopQty.Size = new System.Drawing.Size(118, 24);
            this.CmpStopQty.TabIndex = 10;
            this.CmpStopQty.Text = "Yes";
            this.CmpStopQty.SelectedIndexChanged += new System.EventHandler(this.CmpStopQty_SelectedIndexChanged);
            this.CmpStopQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmpStopQty_KeyDown);
            // 
            // CmpStopRate
            // 
            this.CmpStopRate.FormattingEnabled = true;
            this.CmpStopRate.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.CmpStopRate.Location = new System.Drawing.Point(257, 512);
            this.CmpStopRate.Name = "CmpStopRate";
            this.CmpStopRate.Size = new System.Drawing.Size(118, 24);
            this.CmpStopRate.TabIndex = 12;
            this.CmpStopRate.Text = "Yes";
            this.CmpStopRate.SelectedIndexChanged += new System.EventHandler(this.CmpStopRate_SelectedIndexChanged);
            this.CmpStopRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmpStopRate_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label9.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label9.Location = new System.Drawing.Point(93, 519);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(153, 17);
            this.label9.TabIndex = 44;
            this.label9.Text = "Auto Stop-At-Rate (F4)";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label10.Location = new System.Drawing.Point(93, 473);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 17);
            this.label10.TabIndex = 45;
            this.label10.Text = "Auto Stop-At-Qty (F4)";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label11.Location = new System.Drawing.Point(379, 429);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 17);
            this.label11.TabIndex = 46;
            this.label11.Text = "Allow Void";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label12.Location = new System.Drawing.Point(378, 472);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 17);
            this.label12.TabIndex = 47;
            this.label12.Text = "Allow Return";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // cmbAllowVoid
            // 
            this.cmbAllowVoid.FormattingEnabled = true;
            this.cmbAllowVoid.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbAllowVoid.Location = new System.Drawing.Point(464, 425);
            this.cmbAllowVoid.Name = "cmbAllowVoid";
            this.cmbAllowVoid.Size = new System.Drawing.Size(118, 24);
            this.cmbAllowVoid.TabIndex = 9;
            this.cmbAllowVoid.Text = "Yes";
            this.cmbAllowVoid.SelectedIndexChanged += new System.EventHandler(this.cmbAllowVoid_SelectedIndexChanged);
            this.cmbAllowVoid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAllowVoid_KeyDown);
            // 
            // cmbAllowReturn
            // 
            this.cmbAllowReturn.FormattingEnabled = true;
            this.cmbAllowReturn.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbAllowReturn.Location = new System.Drawing.Point(464, 468);
            this.cmbAllowReturn.Name = "cmbAllowReturn";
            this.cmbAllowReturn.Size = new System.Drawing.Size(118, 24);
            this.cmbAllowReturn.TabIndex = 11;
            this.cmbAllowReturn.Text = "Yes";
            this.cmbAllowReturn.SelectedIndexChanged += new System.EventHandler(this.cmbAllowReturn_SelectedIndexChanged);
            this.cmbAllowReturn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAllowReturn_KeyDown);
            // 
            // cmbViewManagerReport
            // 
            this.cmbViewManagerReport.FormattingEnabled = true;
            this.cmbViewManagerReport.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbViewManagerReport.Location = new System.Drawing.Point(464, 511);
            this.cmbViewManagerReport.Name = "cmbViewManagerReport";
            this.cmbViewManagerReport.Size = new System.Drawing.Size(118, 24);
            this.cmbViewManagerReport.TabIndex = 13;
            this.cmbViewManagerReport.Text = "Yes";
            this.cmbViewManagerReport.SelectedIndexChanged += new System.EventHandler(this.cmbViewManagerReport_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label13.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label13.Location = new System.Drawing.Point(378, 516);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 17);
            this.label13.TabIndex = 52;
            this.label13.Text = "View Report";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // CmbSystemName
            // 
            this.CmbSystemName.FormattingEnabled = true;
            this.CmbSystemName.Location = new System.Drawing.Point(257, 334);
            this.CmbSystemName.Name = "CmbSystemName";
            this.CmbSystemName.Size = new System.Drawing.Size(325, 24);
            this.CmbSystemName.TabIndex = 6;
            this.CmbSystemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbSystemName_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label14.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label14.Location = new System.Drawing.Point(93, 338);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(157, 17);
            this.label14.TabIndex = 55;
            this.label14.Text = "Network System Names";
            // 
            // cmbViewCash
            // 
            this.cmbViewCash.FormattingEnabled = true;
            this.cmbViewCash.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbViewCash.Location = new System.Drawing.Point(825, 512);
            this.cmbViewCash.Name = "cmbViewCash";
            this.cmbViewCash.Size = new System.Drawing.Size(118, 24);
            this.cmbViewCash.TabIndex = 56;
            this.cmbViewCash.Text = "Yes";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label15.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label15.Location = new System.Drawing.Point(594, 514);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 17);
            this.label15.TabIndex = 57;
            this.label15.Text = "View Cash Details";
            // 
            // HAPayment
            // 
            this.HAPayment.FormattingEnabled = true;
            this.HAPayment.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.HAPayment.Location = new System.Drawing.Point(825, 469);
            this.HAPayment.Name = "HAPayment";
            this.HAPayment.Size = new System.Drawing.Size(118, 24);
            this.HAPayment.TabIndex = 61;
            this.HAPayment.Text = "Yes";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label16.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label16.Location = new System.Drawing.Point(594, 471);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(163, 17);
            this.label16.TabIndex = 60;
            this.label16.Text = "House Account Payment";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label17.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label17.Location = new System.Drawing.Point(594, 430);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(192, 17);
            this.label17.TabIndex = 62;
            this.label17.Text = "Settle Counter Status Display";
            // 
            // comboStCounter
            // 
            this.comboStCounter.FormattingEnabled = true;
            this.comboStCounter.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.comboStCounter.Location = new System.Drawing.Point(825, 425);
            this.comboStCounter.Name = "comboStCounter";
            this.comboStCounter.Size = new System.Drawing.Size(118, 24);
            this.comboStCounter.TabIndex = 63;
            this.comboStCounter.Text = "Yes";
            // 
            // comboCashDrawer
            // 
            this.comboCashDrawer.FormattingEnabled = true;
            this.comboCashDrawer.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.comboCashDrawer.Location = new System.Drawing.Point(825, 378);
            this.comboCashDrawer.Name = "comboCashDrawer";
            this.comboCashDrawer.Size = new System.Drawing.Size(118, 24);
            this.comboCashDrawer.TabIndex = 65;
            this.comboCashDrawer.Text = "Yes";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label18.Location = new System.Drawing.Point(594, 383);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(168, 17);
            this.label18.TabIndex = 64;
            this.label18.Text = "Cash Drawer Print Option";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label19.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label19.Location = new System.Drawing.Point(594, 341);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 17);
            this.label19.TabIndex = 66;
            this.label19.Text = "Branch";
            // 
            // ComboBranch
            // 
            this.ComboBranch.FormattingEnabled = true;
            this.ComboBranch.Location = new System.Drawing.Point(679, 335);
            this.ComboBranch.Name = "ComboBranch";
            this.ComboBranch.Size = new System.Drawing.Size(264, 24);
            this.ComboBranch.TabIndex = 67;
            // 
            // UserCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.ComboBranch);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.comboCashDrawer);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.comboStCounter);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.HAPayment);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbViewCash);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.CmbSystemName);
            this.Controls.Add(this.cmbViewManagerReport);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbAllowReturn);
            this.Controls.Add(this.cmbAllowVoid);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CmpStopRate);
            this.Controls.Add(this.CmpStopQty);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbResettle);
            this.Controls.Add(this.txtDiscountRange);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.pnlUserName);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbUserType);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCreation";
            this.Text = "UserCreation";
            this.Load += new System.EventHandler(this.UserCreation_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.pnlUserName.ResumeLayout(false);
            this.pnlUserName.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbUserType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_stckbanner;
        private System.Windows.Forms.TextBox txtCounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstUserName;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.TextBox txtDiscountRange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbResettle;
        private System.Windows.Forms.ComboBox CmpStopQty;
        private System.Windows.Forms.ComboBox CmpStopRate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbAllowVoid;
        private System.Windows.Forms.ComboBox cmbAllowReturn;
        private System.Windows.Forms.ComboBox cmbViewManagerReport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox CmbSystemName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbViewCash;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox HAPayment;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboStCounter;
        private System.Windows.Forms.ComboBox comboCashDrawer;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox ComboBranch;
    }
}