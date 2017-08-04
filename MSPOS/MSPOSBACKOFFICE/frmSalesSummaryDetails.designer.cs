namespace MSPOSBACKOFFICE
{
    partial class frmSalesSummaryDetails
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
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.txt_counter = new System.Windows.Forms.TextBox();
            this.txt_reporton = new System.Windows.Forms.TextBox();
            this.lbl_Countername = new System.Windows.Forms.Label();
            this.lbl_report_on = new System.Windows.Forms.Label();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.dt_to = new System.Windows.Forms.DateTimePicker();
            this.dt_from = new System.Windows.Forms.DateTimePicker();
            this.txt_sales = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ledger = new System.Windows.Forms.TextBox();
            this.lbl_PartyNo = new System.Windows.Forms.Label();
            this.txt_cash = new System.Windows.Forms.TextBox();
            this.lbl_cash = new System.Windows.Forms.Label();
            this.lbl_SummaryBanner = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.grd_SalesDetails = new System.Windows.Forms.DataGridView();
            this.Bill_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bill_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cash_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_Amount = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lst_ofAmount = new System.Windows.Forms.ListBox();
            this.pnl_ledger = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lst_ledger = new System.Windows.Forms.ListBox();
            this.pnl_counter = new System.Windows.Forms.Panel();
            this.lst_counter = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnl_sales = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lst_sales = new System.Windows.Forms.ListBox();
            this.pnl_cash = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lst_cash = new System.Windows.Forms.ListBox();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.Pnl_Back.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_SalesDetails)).BeginInit();
            this.pnl_Amount.SuspendLayout();
            this.pnl_ledger.SuspendLayout();
            this.pnl_counter.SuspendLayout();
            this.pnl_sales.SuspendLayout();
            this.pnl_cash.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Print
            // 
            this.btn_Print.BackColor = System.Drawing.Color.White;
            this.btn_Print.Location = new System.Drawing.Point(869, 4);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(68, 37);
            this.btn_Print.TabIndex = 19;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.White;
            this.btn_Exit.Location = new System.Drawing.Point(943, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(68, 38);
            this.btn_Exit.TabIndex = 21;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // txt_counter
            // 
            this.txt_counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counter.Location = new System.Drawing.Point(690, 23);
            this.txt_counter.Name = "txt_counter";
            this.txt_counter.Size = new System.Drawing.Size(244, 22);
            this.txt_counter.TabIndex = 3;
            this.txt_counter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_counter_MouseClick);
            this.txt_counter.TextChanged += new System.EventHandler(this.txt_counter_TextChanged);
            this.txt_counter.Enter += new System.EventHandler(this.txt_counter_Enter);
            this.txt_counter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_counter_KeyDown);
            // 
            // txt_reporton
            // 
            this.txt_reporton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_reporton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_reporton.Location = new System.Drawing.Point(892, 73);
            this.txt_reporton.Name = "txt_reporton";
            this.txt_reporton.Size = new System.Drawing.Size(113, 22);
            this.txt_reporton.TabIndex = 2;
            this.txt_reporton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_reporton_MouseClick);
            this.txt_reporton.Enter += new System.EventHandler(this.txt_reporton_Enter);
            // 
            // lbl_Countername
            // 
            this.lbl_Countername.AutoSize = true;
            this.lbl_Countername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Countername.ForeColor = System.Drawing.Color.White;
            this.lbl_Countername.Location = new System.Drawing.Point(620, 26);
            this.lbl_Countername.Name = "lbl_Countername";
            this.lbl_Countername.Size = new System.Drawing.Size(54, 16);
            this.lbl_Countername.TabIndex = 1;
            this.lbl_Countername.Text = "Counter";
            // 
            // lbl_report_on
            // 
            this.lbl_report_on.AutoSize = true;
            this.lbl_report_on.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_report_on.ForeColor = System.Drawing.Color.White;
            this.lbl_report_on.Location = new System.Drawing.Point(817, 77);
            this.lbl_report_on.Name = "lbl_report_on";
            this.lbl_report_on.Size = new System.Drawing.Size(69, 16);
            this.lbl_report_on.TabIndex = 0;
            this.lbl_report_on.Text = "Report On";
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back.Controls.Add(this.dt_to);
            this.Pnl_Back.Controls.Add(this.dt_from);
            this.Pnl_Back.Controls.Add(this.txt_sales);
            this.Pnl_Back.Controls.Add(this.label4);
            this.Pnl_Back.Controls.Add(this.label2);
            this.Pnl_Back.Controls.Add(this.label3);
            this.Pnl_Back.Controls.Add(this.txt_ledger);
            this.Pnl_Back.Controls.Add(this.lbl_PartyNo);
            this.Pnl_Back.Controls.Add(this.txt_cash);
            this.Pnl_Back.Controls.Add(this.lbl_cash);
            this.Pnl_Back.Controls.Add(this.txt_counter);
            this.Pnl_Back.Controls.Add(this.txt_reporton);
            this.Pnl_Back.Controls.Add(this.lbl_Countername);
            this.Pnl_Back.Controls.Add(this.lbl_report_on);
            this.Pnl_Back.Location = new System.Drawing.Point(1, 46);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1015, 104);
            this.Pnl_Back.TabIndex = 17;
            // 
            // dt_to
            // 
            this.dt_to.CalendarForeColor = System.Drawing.Color.White;
            this.dt_to.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dt_to.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dt_to.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dt_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_to.Location = new System.Drawing.Point(464, 79);
            this.dt_to.Name = "dt_to";
            this.dt_to.Size = new System.Drawing.Size(132, 22);
            this.dt_to.TabIndex = 11;
            // 
            // dt_from
            // 
            this.dt_from.CalendarForeColor = System.Drawing.Color.White;
            this.dt_from.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dt_from.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dt_from.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dt_from.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_from.Location = new System.Drawing.Point(464, 27);
            this.dt_from.Name = "dt_from";
            this.dt_from.Size = new System.Drawing.Size(129, 22);
            this.dt_from.TabIndex = 10;
            // 
            // txt_sales
            // 
            this.txt_sales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_sales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sales.Location = new System.Drawing.Point(118, 73);
            this.txt_sales.Name = "txt_sales";
            this.txt_sales.Size = new System.Drawing.Size(249, 22);
            this.txt_sales.TabIndex = 9;
            this.txt_sales.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_sales_MouseClick);
            this.txt_sales.Enter += new System.EventHandler(this.txt_sales_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(386, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "To  :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sales Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(383, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "From  :";
            // 
            // txt_ledger
            // 
            this.txt_ledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ledger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ledger.Location = new System.Drawing.Point(118, 27);
            this.txt_ledger.Name = "txt_ledger";
            this.txt_ledger.Size = new System.Drawing.Size(249, 22);
            this.txt_ledger.TabIndex = 7;
            this.txt_ledger.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_ledger_MouseClick);
            this.txt_ledger.TextChanged += new System.EventHandler(this.txt_ledger_TextChanged);
            this.txt_ledger.Enter += new System.EventHandler(this.txt_ledger_Enter);
            this.txt_ledger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ledger_KeyDown);
            // 
            // lbl_PartyNo
            // 
            this.lbl_PartyNo.AutoSize = true;
            this.lbl_PartyNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PartyNo.ForeColor = System.Drawing.Color.White;
            this.lbl_PartyNo.Location = new System.Drawing.Point(22, 29);
            this.lbl_PartyNo.Name = "lbl_PartyNo";
            this.lbl_PartyNo.Size = new System.Drawing.Size(60, 16);
            this.lbl_PartyNo.TabIndex = 6;
            this.lbl_PartyNo.Text = "Party No";
            // 
            // txt_cash
            // 
            this.txt_cash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cash.Location = new System.Drawing.Point(690, 74);
            this.txt_cash.Name = "txt_cash";
            this.txt_cash.Size = new System.Drawing.Size(120, 22);
            this.txt_cash.TabIndex = 5;
            this.txt_cash.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_cash_MouseClick);
            this.txt_cash.Enter += new System.EventHandler(this.txt_cash_Enter);
            // 
            // lbl_cash
            // 
            this.lbl_cash.AutoSize = true;
            this.lbl_cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cash.ForeColor = System.Drawing.Color.White;
            this.lbl_cash.Location = new System.Drawing.Point(621, 77);
            this.lbl_cash.Name = "lbl_cash";
            this.lbl_cash.Size = new System.Drawing.Size(39, 16);
            this.lbl_cash.TabIndex = 4;
            this.lbl_cash.Text = "Cash";
            // 
            // lbl_SummaryBanner
            // 
            this.lbl_SummaryBanner.AutoSize = true;
            this.lbl_SummaryBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SummaryBanner.ForeColor = System.Drawing.Color.White;
            this.lbl_SummaryBanner.Location = new System.Drawing.Point(3, 7);
            this.lbl_SummaryBanner.Name = "lbl_SummaryBanner";
            this.lbl_SummaryBanner.Size = new System.Drawing.Size(215, 25);
            this.lbl_SummaryBanner.TabIndex = 0;
            this.lbl_SummaryBanner.Text = "Sales Summary Details";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_SummaryBanner);
            this.Pnl_Header.Location = new System.Drawing.Point(1, 2);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1015, 47);
            this.Pnl_Header.TabIndex = 16;
            // 
            // grd_SalesDetails
            // 
            this.grd_SalesDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grd_SalesDetails.ColumnHeadersHeight = 50;
            this.grd_SalesDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Bill_No,
            this.Bill_Date,
            this.Particulars,
            this.Cash_Type,
            this.Amount});
            this.grd_SalesDetails.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.grd_SalesDetails.Location = new System.Drawing.Point(0, 156);
            this.grd_SalesDetails.Name = "grd_SalesDetails";
            this.grd_SalesDetails.RowHeadersVisible = false;
            this.grd_SalesDetails.Size = new System.Drawing.Size(1018, 385);
            this.grd_SalesDetails.TabIndex = 22;
            this.grd_SalesDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_SalesDetails_CellDoubleClick);
            // 
            // Bill_No
            // 
            this.Bill_No.DataPropertyName = "Bill_no";
            this.Bill_No.HeaderText = "Bill_No";
            this.Bill_No.Name = "Bill_No";
            // 
            // Bill_Date
            // 
            this.Bill_Date.DataPropertyName = "Bill_Date";
            this.Bill_Date.HeaderText = "Bill_Date";
            this.Bill_Date.Name = "Bill_Date";
            // 
            // Particulars
            // 
            this.Particulars.DataPropertyName = "Particulars";
            this.Particulars.HeaderText = "Particulars";
            this.Particulars.Name = "Particulars";
            // 
            // Cash_Type
            // 
            this.Cash_Type.DataPropertyName = "Cash_Recd";
            this.Cash_Type.HeaderText = "Cash_Type";
            this.Cash_Type.Name = "Cash_Type";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // pnl_Amount
            // 
            this.pnl_Amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_Amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Amount.Controls.Add(this.label5);
            this.pnl_Amount.Controls.Add(this.lst_ofAmount);
            this.pnl_Amount.Location = new System.Drawing.Point(1051, 167);
            this.pnl_Amount.Name = "pnl_Amount";
            this.pnl_Amount.Size = new System.Drawing.Size(203, 191);
            this.pnl_Amount.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(46, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Select One";
            // 
            // lst_ofAmount
            // 
            this.lst_ofAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_ofAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_ofAmount.FormattingEnabled = true;
            this.lst_ofAmount.ItemHeight = 18;
            this.lst_ofAmount.Items.AddRange(new object[] {
            "Gross Amount",
            "Nett Amount"});
            this.lst_ofAmount.Location = new System.Drawing.Point(9, 36);
            this.lst_ofAmount.Name = "lst_ofAmount";
            this.lst_ofAmount.Size = new System.Drawing.Size(185, 146);
            this.lst_ofAmount.TabIndex = 8;
            this.lst_ofAmount.SelectedIndexChanged += new System.EventHandler(this.lst_ofAmount_SelectedIndexChanged);
            this.lst_ofAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_ofAmount_KeyDown);
            // 
            // pnl_ledger
            // 
            this.pnl_ledger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_ledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ledger.Controls.Add(this.label1);
            this.pnl_ledger.Controls.Add(this.lst_ledger);
            this.pnl_ledger.Location = new System.Drawing.Point(69, 158);
            this.pnl_ledger.Name = "pnl_ledger";
            this.pnl_ledger.Size = new System.Drawing.Size(286, 190);
            this.pnl_ledger.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(63, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "List of Ledger";
            // 
            // lst_ledger
            // 
            this.lst_ledger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_ledger.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_ledger.FormattingEnabled = true;
            this.lst_ledger.ItemHeight = 18;
            this.lst_ledger.Location = new System.Drawing.Point(8, 33);
            this.lst_ledger.Name = "lst_ledger";
            this.lst_ledger.Size = new System.Drawing.Size(268, 146);
            this.lst_ledger.TabIndex = 8;
            // 
            // pnl_counter
            // 
            this.pnl_counter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_counter.Controls.Add(this.lst_counter);
            this.pnl_counter.Controls.Add(this.label6);
            this.pnl_counter.Location = new System.Drawing.Point(634, 150);
            this.pnl_counter.Name = "pnl_counter";
            this.pnl_counter.Size = new System.Drawing.Size(272, 188);
            this.pnl_counter.TabIndex = 20;
            // 
            // lst_counter
            // 
            this.lst_counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_counter.FormattingEnabled = true;
            this.lst_counter.ItemHeight = 18;
            this.lst_counter.Location = new System.Drawing.Point(3, 34);
            this.lst_counter.Name = "lst_counter";
            this.lst_counter.Size = new System.Drawing.Size(259, 146);
            this.lst_counter.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(50, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "List of Counter";
            // 
            // pnl_sales
            // 
            this.pnl_sales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_sales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_sales.Controls.Add(this.label7);
            this.pnl_sales.Controls.Add(this.lst_sales);
            this.pnl_sales.Location = new System.Drawing.Point(361, 156);
            this.pnl_sales.Name = "pnl_sales";
            this.pnl_sales.Size = new System.Drawing.Size(278, 191);
            this.pnl_sales.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(86, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Select one";
            // 
            // lst_sales
            // 
            this.lst_sales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_sales.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_sales.FormattingEnabled = true;
            this.lst_sales.ItemHeight = 18;
            this.lst_sales.Items.AddRange(new object[] {
            "All",
            "Retail Sales",
            "Whole Sales"});
            this.lst_sales.Location = new System.Drawing.Point(10, 36);
            this.lst_sales.Name = "lst_sales";
            this.lst_sales.Size = new System.Drawing.Size(256, 146);
            this.lst_sales.TabIndex = 8;
            this.lst_sales.SelectedIndexChanged += new System.EventHandler(this.lst_sales_SelectedIndexChanged);
            this.lst_sales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_sales_KeyDown);
            // 
            // pnl_cash
            // 
            this.pnl_cash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_cash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_cash.Controls.Add(this.label8);
            this.pnl_cash.Controls.Add(this.lst_cash);
            this.pnl_cash.Location = new System.Drawing.Point(653, 157);
            this.pnl_cash.Name = "pnl_cash";
            this.pnl_cash.Size = new System.Drawing.Size(207, 189);
            this.pnl_cash.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(68, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Select One";
            // 
            // lst_cash
            // 
            this.lst_cash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_cash.FormattingEnabled = true;
            this.lst_cash.ItemHeight = 18;
            this.lst_cash.Items.AddRange(new object[] {
            "All",
            "Cash",
            "Credit"});
            this.lst_cash.Location = new System.Drawing.Point(11, 34);
            this.lst_cash.Name = "lst_cash";
            this.lst_cash.Size = new System.Drawing.Size(184, 146);
            this.lst_cash.TabIndex = 8;
            this.lst_cash.SelectedIndexChanged += new System.EventHandler(this.lst_cash_SelectedIndexChanged);
            this.lst_cash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_cash_KeyDown);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btn_Print);
            this.Pnl_Footer.Controls.Add(this.btn_Exit);
            this.Pnl_Footer.Location = new System.Drawing.Point(1, 543);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1016, 44);
            this.Pnl_Footer.TabIndex = 17;
            // 
            // frmSalesSummaryDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 592);
            this.Controls.Add(this.pnl_counter);
            this.Controls.Add(this.pnl_sales);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.pnl_cash);
            this.Controls.Add(this.pnl_ledger);
            this.Controls.Add(this.pnl_Amount);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.grd_SalesDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSalesSummaryDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmSalesSummaryDetails";
            this.Load += new System.EventHandler(this.frmSalesSummaryDetails_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_SalesDetails)).EndInit();
            this.pnl_Amount.ResumeLayout(false);
            this.pnl_Amount.PerformLayout();
            this.pnl_ledger.ResumeLayout(false);
            this.pnl_ledger.PerformLayout();
            this.pnl_counter.ResumeLayout(false);
            this.pnl_counter.PerformLayout();
            this.pnl_sales.ResumeLayout(false);
            this.pnl_sales.PerformLayout();
            this.pnl_cash.ResumeLayout(false);
            this.pnl_cash.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.TextBox txt_counter;
        private System.Windows.Forms.TextBox txt_reporton;
        private System.Windows.Forms.Label lbl_Countername;
        private System.Windows.Forms.Label lbl_report_on;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.Label lbl_SummaryBanner;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView grd_SalesDetails;
        private System.Windows.Forms.TextBox txt_cash;
        private System.Windows.Forms.Label lbl_cash;
        private System.Windows.Forms.TextBox txt_sales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ledger;
        private System.Windows.Forms.Label lbl_PartyNo;
        private System.Windows.Forms.Panel pnl_Amount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lst_ofAmount;
        private System.Windows.Forms.Panel pnl_ledger;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lst_ledger;
        private System.Windows.Forms.Panel pnl_counter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lst_counter;
        private System.Windows.Forms.Panel pnl_sales;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lst_sales;
        private System.Windows.Forms.Panel pnl_cash;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lst_cash;
        private System.Windows.Forms.DateTimePicker dt_to;
        private System.Windows.Forms.DateTimePicker dt_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bill_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bill_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cash_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.Panel Pnl_Footer;
    }
}