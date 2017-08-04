namespace MSPOSBACKOFFICE
{
    partial class FrmBarcodePrinter
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
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.lbl_Ctrbanner = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.txtSpecial_1 = new System.Windows.Forms.TextBox();
            this.txtMrp = new System.Windows.Forms.TextBox();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.txtPrintRate = new System.Windows.Forms.TextBox();
            this.txtItemPrintName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUnitRate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPkdDate = new System.Windows.Forms.TextBox();
            this.txtExpDate = new System.Windows.Forms.TextBox();
            this.txtNoLabels = new System.Windows.Forms.TextBox();
            this.txtPrintTo = new System.Windows.Forms.TextBox();
            this.txtLbsFormate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.listitems = new System.Windows.Forms.ListBox();
            this.Pnl_Footer.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnClose);
            this.Pnl_Footer.Controls.Add(this.btnClear);
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 540);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 50);
            this.Pnl_Footer.TabIndex = 34;
            this.Pnl_Footer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClose.Location = new System.Drawing.Point(935, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 36);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClear.Location = new System.Drawing.Point(866, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 36);
            this.btnClear.TabIndex = 34;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPrint.Location = new System.Drawing.Point(796, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 36);
            this.btnPrint.TabIndex = 33;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_Ctrbanner);
            this.Pnl_Header.Location = new System.Drawing.Point(1, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1018, 50);
            this.Pnl_Header.TabIndex = 35;
            // 
            // lbl_Ctrbanner
            // 
            this.lbl_Ctrbanner.AutoSize = true;
            this.lbl_Ctrbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Ctrbanner.ForeColor = System.Drawing.Color.White;
            this.lbl_Ctrbanner.Location = new System.Drawing.Point(12, 8);
            this.lbl_Ctrbanner.Name = "lbl_Ctrbanner";
            this.lbl_Ctrbanner.Size = new System.Drawing.Size(171, 25);
            this.lbl_Ctrbanner.TabIndex = 0;
            this.lbl_Ctrbanner.Text = "Barcode Printing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(143, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 37;
            this.label1.Text = "Item Code";
            // 
            // txtItemCode
            // 
            this.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemCode.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtItemCode.Location = new System.Drawing.Point(322, 56);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(330, 26);
            this.txtItemCode.TabIndex = 36;
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            this.txtItemCode.Leave += new System.EventHandler(this.txtItemCode_Leave);
            // 
            // txtItemName
            // 
            this.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemName.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtItemName.Location = new System.Drawing.Point(322, 88);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(437, 26);
            this.txtItemName.TabIndex = 38;
            this.txtItemName.TextChanged += new System.EventHandler(this.txtItemName_TextChanged);
            this.txtItemName.Enter += new System.EventHandler(this.txtItemName_Enter);
            this.txtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txtUnitName
            // 
            this.txtUnitName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnitName.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtUnitName.Location = new System.Drawing.Point(322, 295);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(177, 26);
            this.txtUnitName.TabIndex = 39;
            this.txtUnitName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitName_KeyDown);
            // 
            // txtSpecial_1
            // 
            this.txtSpecial_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecial_1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtSpecial_1.Location = new System.Drawing.Point(322, 255);
            this.txtSpecial_1.Name = "txtSpecial_1";
            this.txtSpecial_1.Size = new System.Drawing.Size(177, 26);
            this.txtSpecial_1.TabIndex = 41;
            this.txtSpecial_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSpecial_1_KeyDown);
            this.txtSpecial_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrintRate_KeyPress);
            // 
            // txtMrp
            // 
            this.txtMrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMrp.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtMrp.Location = new System.Drawing.Point(322, 223);
            this.txtMrp.Name = "txtMrp";
            this.txtMrp.Size = new System.Drawing.Size(177, 26);
            this.txtMrp.TabIndex = 42;
            this.txtMrp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMrp_KeyDown);
            this.txtMrp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrintRate_KeyPress);
            // 
            // txtCost
            // 
            this.txtCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCost.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtCost.Location = new System.Drawing.Point(322, 191);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(177, 26);
            this.txtCost.TabIndex = 43;
            this.txtCost.TextChanged += new System.EventHandler(this.txtCost_TextChanged);
            this.txtCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCost_KeyDown);
            this.txtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrintRate_KeyPress);
            // 
            // txtPrintRate
            // 
            this.txtPrintRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrintRate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtPrintRate.Location = new System.Drawing.Point(322, 159);
            this.txtPrintRate.Name = "txtPrintRate";
            this.txtPrintRate.Size = new System.Drawing.Size(177, 26);
            this.txtPrintRate.TabIndex = 44;
            this.txtPrintRate.TextChanged += new System.EventHandler(this.txtPrintRate_TextChanged);
            this.txtPrintRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrintRate_KeyDown);
            this.txtPrintRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrintRate_KeyPress);
            // 
            // txtItemPrintName
            // 
            this.txtItemPrintName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemPrintName.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtItemPrintName.Location = new System.Drawing.Point(322, 121);
            this.txtItemPrintName.Name = "txtItemPrintName";
            this.txtItemPrintName.Size = new System.Drawing.Size(437, 26);
            this.txtItemPrintName.TabIndex = 46;
            this.txtItemPrintName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemPrintName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(143, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 47;
            this.label2.Text = "Item Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(143, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 48;
            this.label3.Text = "Item PrintName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label4.Location = new System.Drawing.Point(23, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 49;
            this.label4.Text = "Print Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label5.Location = new System.Drawing.Point(143, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 50;
            this.label5.Text = "P.Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label6.Location = new System.Drawing.Point(143, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 20);
            this.label6.TabIndex = 51;
            this.label6.Text = "Cost";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label7.Location = new System.Drawing.Point(143, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 20);
            this.label7.TabIndex = 52;
            this.label7.Text = "Mrp";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label8.Location = new System.Drawing.Point(143, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 20);
            this.label8.TabIndex = 53;
            this.label8.Text = "Special-1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label9.Location = new System.Drawing.Point(23, 282);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 20);
            this.label9.TabIndex = 54;
            this.label9.Text = "Other Details";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label10.Location = new System.Drawing.Point(143, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 20);
            this.label10.TabIndex = 55;
            this.label10.Text = "Unit Name";
            // 
            // txtUnitRate
            // 
            this.txtUnitRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnitRate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtUnitRate.Location = new System.Drawing.Point(607, 296);
            this.txtUnitRate.Name = "txtUnitRate";
            this.txtUnitRate.Size = new System.Drawing.Size(152, 26);
            this.txtUnitRate.TabIndex = 56;
            this.txtUnitRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitRate_KeyDown);
            this.txtUnitRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrintRate_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label11.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label11.Location = new System.Drawing.Point(516, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 20);
            this.label11.TabIndex = 57;
            this.label11.Text = "Rate";
            // 
            // txtPkdDate
            // 
            this.txtPkdDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPkdDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtPkdDate.Location = new System.Drawing.Point(322, 327);
            this.txtPkdDate.Name = "txtPkdDate";
            this.txtPkdDate.Size = new System.Drawing.Size(437, 26);
            this.txtPkdDate.TabIndex = 58;
            this.txtPkdDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPkdDate_KeyDown);
            this.txtPkdDate.Leave += new System.EventHandler(this.txtPkdDate_Leave);
            // 
            // txtExpDate
            // 
            this.txtExpDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtExpDate.Location = new System.Drawing.Point(322, 359);
            this.txtExpDate.Name = "txtExpDate";
            this.txtExpDate.Size = new System.Drawing.Size(437, 26);
            this.txtExpDate.TabIndex = 59;
            this.txtExpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpDate_KeyDown);
            // 
            // txtNoLabels
            // 
            this.txtNoLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoLabels.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtNoLabels.Location = new System.Drawing.Point(322, 391);
            this.txtNoLabels.Name = "txtNoLabels";
            this.txtNoLabels.Size = new System.Drawing.Size(177, 26);
            this.txtNoLabels.TabIndex = 60;
            this.txtNoLabels.Text = "0";
            this.txtNoLabels.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoLabels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoLabels_KeyDown);
            this.txtNoLabels.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoLabels_KeyPress);
            // 
            // txtPrintTo
            // 
            this.txtPrintTo.BackColor = System.Drawing.Color.White;
            this.txtPrintTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrintTo.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtPrintTo.Location = new System.Drawing.Point(322, 423);
            this.txtPrintTo.Name = "txtPrintTo";
            this.txtPrintTo.ReadOnly = true;
            this.txtPrintTo.Size = new System.Drawing.Size(177, 26);
            this.txtPrintTo.TabIndex = 61;
            this.txtPrintTo.Text = "Printer";
            this.txtPrintTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrintTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrintTo_KeyDown);
            // 
            // txtLbsFormate
            // 
            this.txtLbsFormate.BackColor = System.Drawing.Color.White;
            this.txtLbsFormate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLbsFormate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtLbsFormate.Location = new System.Drawing.Point(607, 391);
            this.txtLbsFormate.Name = "txtLbsFormate";
            this.txtLbsFormate.ReadOnly = true;
            this.txtLbsFormate.Size = new System.Drawing.Size(152, 26);
            this.txtLbsFormate.TabIndex = 63;
            this.txtLbsFormate.Text = "Barcode1";
            this.txtLbsFormate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLbsFormate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLbsFormate_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label12.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label12.Location = new System.Drawing.Point(516, 397);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 20);
            this.label12.TabIndex = 64;
            this.label12.Text = "Fromat";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label13.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label13.Location = new System.Drawing.Point(145, 333);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 65;
            this.label13.Text = "PKD.Date";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label14.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label14.Location = new System.Drawing.Point(145, 365);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 20);
            this.label14.TabIndex = 66;
            this.label14.Text = "EXP.Date";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label15.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label15.Location = new System.Drawing.Point(143, 397);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 20);
            this.label15.TabIndex = 67;
            this.label15.Text = "No Of Labels";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label16.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label16.Location = new System.Drawing.Point(143, 429);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 20);
            this.label16.TabIndex = 68;
            this.label16.Text = "Print To";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label17.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label17.Location = new System.Drawing.Point(143, 461);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 20);
            this.label17.TabIndex = 69;
            this.label17.Text = "Printer";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownWidth = 200;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(322, 460);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(440, 21);
            this.cmbPrinter.TabIndex = 70;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.listitems);
            this.panel1.Location = new System.Drawing.Point(323, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 344);
            this.panel1.TabIndex = 71;
            this.panel1.Visible = false;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(227, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 26);
            this.label18.TabIndex = 0;
            this.label18.Text = "List Of Items";
            // 
            // listitems
            // 
            this.listitems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listitems.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.listitems.FormattingEnabled = true;
            this.listitems.ItemHeight = 20;
            this.listitems.Location = new System.Drawing.Point(5, 36);
            this.listitems.Name = "listitems";
            this.listitems.Size = new System.Drawing.Size(582, 302);
            this.listitems.TabIndex = 3;
            this.listitems.Click += new System.EventHandler(this.listitems_Click);
            // 
            // FrmBarcodePrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbPrinter);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtLbsFormate);
            this.Controls.Add(this.txtPrintTo);
            this.Controls.Add(this.txtNoLabels);
            this.Controls.Add(this.txtExpDate);
            this.Controls.Add(this.txtPkdDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtUnitRate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtItemPrintName);
            this.Controls.Add(this.txtPrintRate);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.txtMrp);
            this.Controls.Add(this.txtSpecial_1);
            this.Controls.Add(this.txtUnitName);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.Pnl_Footer);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBarcodePrinter";
            this.Text = "FrmBarcodePrinter";
            this.Load += new System.EventHandler(this.FrmBarcodePrinter_Load);
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_Ctrbanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.TextBox txtSpecial_1;
        private System.Windows.Forms.TextBox txtMrp;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.TextBox txtPrintRate;
        private System.Windows.Forms.TextBox txtItemPrintName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUnitRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPkdDate;
        private System.Windows.Forms.TextBox txtExpDate;
        private System.Windows.Forms.TextBox txtNoLabels;
        private System.Windows.Forms.TextBox txtPrintTo;
        private System.Windows.Forms.TextBox txtLbsFormate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox listitems;

    }
}