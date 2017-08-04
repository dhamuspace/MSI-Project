namespace MSPOSBACKOFFICE
{
    partial class SalesBOMIssueCreation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.lbloutputval = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblinputval = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblinputQty = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbloutputqty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlledgername = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lstledgerName = new System.Windows.Forms.ListBox();
            this.DgBomsEntry = new DataGridNameSpace.MyDataGrid();
            this.Item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Typess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tx_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOM_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabourAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOM_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBOmBillno = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLabour = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtissueQty = new System.Windows.Forms.TextBox();
            this.txtBomName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLabourAmount = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Footer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlledgername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBomsEntry)).BeginInit();
            this.Pnl_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.lbloutputval);
            this.Pnl_Footer.Controls.Add(this.label4);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.lblinputval);
            this.Pnl_Footer.Controls.Add(this.btnSave);
            this.Pnl_Footer.Controls.Add(this.lblinputQty);
            this.Pnl_Footer.Controls.Add(this.label9);
            this.Pnl_Footer.Controls.Add(this.label6);
            this.Pnl_Footer.Controls.Add(this.lbloutputqty);
            this.Pnl_Footer.Controls.Add(this.label8);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 570);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 46);
            this.Pnl_Footer.TabIndex = 6;
            // 
            // lbloutputval
            // 
            this.lbloutputval.AutoSize = true;
            this.lbloutputval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbloutputval.ForeColor = System.Drawing.Color.White;
            this.lbloutputval.Location = new System.Drawing.Point(740, 12);
            this.lbloutputval.Name = "lbloutputval";
            this.lbloutputval.Size = new System.Drawing.Size(36, 18);
            this.lbloutputval.TabIndex = 14;
            this.lbloutputval.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(73, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Input Qty";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(916, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(87, 44);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblinputval
            // 
            this.lblinputval.AutoSize = true;
            this.lblinputval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinputval.ForeColor = System.Drawing.Color.White;
            this.lblinputval.Location = new System.Drawing.Point(339, 12);
            this.lblinputval.Name = "lblinputval";
            this.lblinputval.Size = new System.Drawing.Size(36, 18);
            this.lblinputval.TabIndex = 16;
            this.lblinputval.Text = "0.00";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(828, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 44);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblinputQty
            // 
            this.lblinputQty.AutoSize = true;
            this.lblinputQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinputQty.ForeColor = System.Drawing.Color.White;
            this.lblinputQty.Location = new System.Drawing.Point(145, 12);
            this.lblinputQty.Name = "lblinputQty";
            this.lblinputQty.Size = new System.Drawing.Size(36, 18);
            this.lblinputQty.TabIndex = 10;
            this.lblinputQty.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(270, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Input Val";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(461, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Output Qty";
            // 
            // lbloutputqty
            // 
            this.lbloutputqty.AutoSize = true;
            this.lbloutputqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbloutputqty.ForeColor = System.Drawing.Color.White;
            this.lbloutputqty.Location = new System.Drawing.Point(552, 12);
            this.lbloutputqty.Name = "lbloutputqty";
            this.lbloutputqty.Size = new System.Drawing.Size(36, 18);
            this.lbloutputqty.TabIndex = 12;
            this.lbloutputqty.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(656, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "OutPut Val";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlledgername);
            this.panel1.Controls.Add(this.DgBomsEntry);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.Pnl_Header);
            this.panel1.Controls.Add(this.txtBomName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtLabourAmount);
            this.panel1.Controls.Add(this.txtRemarks);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Pnl_Footer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, -24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1019, 619);
            this.panel1.TabIndex = 2;
            // 
            // pnlledgername
            // 
            this.pnlledgername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pnlledgername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlledgername.Controls.Add(this.label13);
            this.pnlledgername.Controls.Add(this.lstledgerName);
            this.pnlledgername.Location = new System.Drawing.Point(400, 153);
            this.pnlledgername.Name = "pnlledgername";
            this.pnlledgername.Size = new System.Drawing.Size(614, 349);
            this.pnlledgername.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(211, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(164, 29);
            this.label13.TabIndex = 1;
            this.label13.Text = "List Of Ledger";
            // 
            // lstledgerName
            // 
            this.lstledgerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstledgerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstledgerName.FormattingEnabled = true;
            this.lstledgerName.ItemHeight = 16;
            this.lstledgerName.Location = new System.Drawing.Point(7, 34);
            this.lstledgerName.Name = "lstledgerName";
            this.lstledgerName.Size = new System.Drawing.Size(599, 306);
            this.lstledgerName.TabIndex = 0;
            this.lstledgerName.Click += new System.EventHandler(this.lstledgerName_Click);
            // 
            // DgBomsEntry
            // 
            this.DgBomsEntry.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgBomsEntry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgBomsEntry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgBomsEntry.ColumnHeadersHeight = 38;
            this.DgBomsEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item_code,
            this.Item_name,
            this.unit_name,
            this.Typess,
            this.tx_Qty,
            this.nt_qty,
            this.Rate,
            this.Amount,
            this.BOM_No,
            this.LabourAmount,
            this.BOM_name});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgBomsEntry.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgBomsEntry.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgBomsEntry.Location = new System.Drawing.Point(0, 153);
            this.DgBomsEntry.Name = "DgBomsEntry";
            this.DgBomsEntry.RowHeadersVisible = false;
            this.DgBomsEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgBomsEntry.Size = new System.Drawing.Size(1017, 338);
            this.DgBomsEntry.TabIndex = 6;
            this.DgBomsEntry.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellEndEdit);
            this.DgBomsEntry.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgBomsEntry_EditingControlShowing);
            this.DgBomsEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgBomsEntry_KeyDown);
            // 
            // Item_code
            // 
            this.Item_code.DataPropertyName = "Item_code";
            this.Item_code.HeaderText = "ItemCode";
            this.Item_code.Name = "Item_code";
            // 
            // Item_name
            // 
            this.Item_name.DataPropertyName = "Item_name";
            this.Item_name.HeaderText = "ItemName";
            this.Item_name.Name = "Item_name";
            this.Item_name.Width = 310;
            // 
            // unit_name
            // 
            this.unit_name.DataPropertyName = "unit_name";
            this.unit_name.HeaderText = "Unit";
            this.unit_name.Name = "unit_name";
            // 
            // Typess
            // 
            this.Typess.DataPropertyName = "Typess";
            this.Typess.HeaderText = "Type";
            this.Typess.Name = "Typess";
            // 
            // tx_Qty
            // 
            this.tx_Qty.DataPropertyName = "tx_Qty";
            this.tx_Qty.HeaderText = "TaxQty";
            this.tx_Qty.Name = "tx_Qty";
            // 
            // nt_qty
            // 
            this.nt_qty.DataPropertyName = "nt_qty";
            this.nt_qty.HeaderText = "Qty";
            this.nt_qty.Name = "nt_qty";
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // BOM_No
            // 
            this.BOM_No.DataPropertyName = "BOM_No";
            this.BOM_No.HeaderText = "BOM_No";
            this.BOM_No.Name = "BOM_No";
            this.BOM_No.Visible = false;
            // 
            // LabourAmount
            // 
            this.LabourAmount.DataPropertyName = "LabourAmount";
            this.LabourAmount.HeaderText = "LabourAmount";
            this.LabourAmount.Name = "LabourAmount";
            this.LabourAmount.Visible = false;
            // 
            // BOM_name
            // 
            this.BOM_name.DataPropertyName = "BOM_name";
            this.BOM_name.HeaderText = "BOM_name";
            this.BOM_name.Name = "BOM_name";
            this.BOM_name.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 497);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 20);
            this.label12.TabIndex = 29;
            this.label12.Text = "Remarks";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.dtpDate);
            this.Pnl_Header.Controls.Add(this.label7);
            this.Pnl_Header.Controls.Add(this.lblBOmBillno);
            this.Pnl_Header.Controls.Add(this.label5);
            this.Pnl_Header.Controls.Add(this.label10);
            this.Pnl_Header.Controls.Add(this.txtLabour);
            this.Pnl_Header.Controls.Add(this.label11);
            this.Pnl_Header.Controls.Add(this.txtissueQty);
            this.Pnl_Header.Location = new System.Drawing.Point(1, 108);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 46);
            this.Pnl_Header.TabIndex = 28;
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarForeColor = System.Drawing.Color.White;
            this.dtpDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpDate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpDate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(244, 8);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(146, 23);
            this.dtpDate.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(825, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "Lobour";
            // 
            // lblBOmBillno
            // 
            this.lblBOmBillno.AutoSize = true;
            this.lblBOmBillno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblBOmBillno.ForeColor = System.Drawing.Color.White;
            this.lblBOmBillno.Location = new System.Drawing.Point(49, 8);
            this.lblBOmBillno.Name = "lblBOmBillno";
            this.lblBOmBillno.Size = new System.Drawing.Size(29, 20);
            this.lblBOmBillno.TabIndex = 27;
            this.lblBOmBillno.Text = ".....";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(608, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Issue Qty";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(182, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 20);
            this.label10.TabIndex = 25;
            this.label10.Text = "Date";
            // 
            // txtLabour
            // 
            this.txtLabour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabour.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLabour.Location = new System.Drawing.Point(892, 5);
            this.txtLabour.Name = "txtLabour";
            this.txtLabour.Size = new System.Drawing.Size(112, 23);
            this.txtLabour.TabIndex = 21;
            this.txtLabour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLabour.Enter += new System.EventHandler(this.txtissueQty_Enter);
            this.txtLabour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLabour_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 20);
            this.label11.TabIndex = 26;
            this.label11.Text = "No";
            // 
            // txtissueQty
            // 
            this.txtissueQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtissueQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtissueQty.Location = new System.Drawing.Point(704, 5);
            this.txtissueQty.Name = "txtissueQty";
            this.txtissueQty.Size = new System.Drawing.Size(112, 23);
            this.txtissueQty.TabIndex = 20;
            this.txtissueQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtissueQty.TextChanged += new System.EventHandler(this.validateTextInteger);
            this.txtissueQty.Enter += new System.EventHandler(this.txtissueQty_Enter);
            this.txtissueQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtissueQty_KeyDown);
            // 
            // txtBomName
            // 
            this.txtBomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBomName.Location = new System.Drawing.Point(707, 83);
            this.txtBomName.Name = "txtBomName";
            this.txtBomName.Size = new System.Drawing.Size(299, 23);
            this.txtBomName.TabIndex = 19;
            this.txtBomName.TextChanged += new System.EventHandler(this.txtBomName_TextChanged);
            this.txtBomName.Enter += new System.EventHandler(this.txtBomName_Enter);
            this.txtBomName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBomName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(608, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "BOM Name";
            // 
            // txtLabourAmount
            // 
            this.txtLabourAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabourAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLabourAmount.Location = new System.Drawing.Point(707, 54);
            this.txtLabourAmount.Name = "txtLabourAmount";
            this.txtLabourAmount.Size = new System.Drawing.Size(299, 23);
            this.txtLabourAmount.TabIndex = 1;
            this.txtLabourAmount.TextChanged += new System.EventHandler(this.txtLabourAmount_TextChanged);
            this.txtLabourAmount.Enter += new System.EventHandler(this.txtLabourAmount_Enter);
            this.txtLabourAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLabourAmount_KeyDown);
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Location = new System.Drawing.Point(82, 499);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(934, 50);
            this.txtRemarks.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(608, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Labour";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "BOM Issue Creation";
            // 
            // SalesBOMIssueCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesBOMIssueCreation";
            this.Text = "SalesBOMIssueCreation";
            this.Load += new System.EventHandler(this.SalesBOMIssueCreation_Load);
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlledgername.ResumeLayout(false);
            this.pnlledgername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBomsEntry)).EndInit();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Label lbloutputval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblinputval;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblinputQty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbloutputqty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private DataGridNameSpace.MyDataGrid DgBomsEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBOmBillno;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtLabour;
        private System.Windows.Forms.TextBox txtissueQty;
        private System.Windows.Forms.TextBox txtBomName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLabourAmount;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlledgername;
        private System.Windows.Forms.ListBox lstledgerName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Typess;
        private System.Windows.Forms.DataGridViewTextBoxColumn tx_Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOM_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabourAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOM_name;
    }
}