namespace MSPOSBACKOFFICE
{
    partial class frmItemWiseSalesSummary
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
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Back1 = new System.Windows.Forms.Panel();
            this.txt_to = new System.Windows.Forms.DateTimePicker();
            this.txt_from = new System.Windows.Forms.DateTimePicker();
            this.lbl_customer = new System.Windows.Forms.Label();
            this.txt_customer = new System.Windows.Forms.TextBox();
            this.Todate = new System.Windows.Forms.Label();
            this.lbl_Fromdate = new System.Windows.Forms.Label();
            this.grd_SalesSummary = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalAmt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.lbl_total = new System.Windows.Forms.Label();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_option = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.Pnl_Back2 = new System.Windows.Forms.Panel();
            this.Pnllistselect = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.listSelect = new System.Windows.Forms.ListBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.txt_model = new System.Windows.Forms.TextBox();
            this.txt_salestypes = new System.Windows.Forms.TextBox();
            this.txt_Counter = new System.Windows.Forms.TextBox();
            this.txt_Brand = new System.Windows.Forms.TextBox();
            this.txt_Group = new System.Windows.Forms.TextBox();
            this.txt_OrderBy = new System.Windows.Forms.TextBox();
            this.txt_ReportOn = new System.Windows.Forms.TextBox();
            this.lbl_Model = new System.Windows.Forms.Label();
            this.lbl_sales = new System.Windows.Forms.Label();
            this.lbl_counter = new System.Windows.Forms.Label();
            this.lbl_brand = new System.Windows.Forms.Label();
            this.lbl_group = new System.Windows.Forms.Label();
            this.lbl_orderby = new System.Windows.Forms.Label();
            this.lbl_reportOn = new System.Windows.Forms.Label();
            this.lst_Boxitem = new System.Windows.Forms.ListBox();
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Back1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_SalesSummary)).BeginInit();
            this.Pnl_Back2.SuspendLayout();
            this.Pnllistselect.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(0, -1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 43);
            this.Pnl_Header.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sales Summary";
            // 
            // Pnl_Back1
            // 
            this.Pnl_Back1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back1.Controls.Add(this.txt_to);
            this.Pnl_Back1.Controls.Add(this.txt_from);
            this.Pnl_Back1.Controls.Add(this.lbl_customer);
            this.Pnl_Back1.Controls.Add(this.txt_customer);
            this.Pnl_Back1.Controls.Add(this.Todate);
            this.Pnl_Back1.Controls.Add(this.lbl_Fromdate);
            this.Pnl_Back1.Location = new System.Drawing.Point(0, 48);
            this.Pnl_Back1.Name = "Pnl_Back1";
            this.Pnl_Back1.Size = new System.Drawing.Size(1019, 36);
            this.Pnl_Back1.TabIndex = 1;
            // 
            // txt_to
            // 
            this.txt_to.CalendarForeColor = System.Drawing.Color.White;
            this.txt_to.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_to.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_to.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_to.CustomFormat = "dd/MM/yyyy";
            this.txt_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_to.Location = new System.Drawing.Point(311, 7);
            this.txt_to.Name = "txt_to";
            this.txt_to.Size = new System.Drawing.Size(181, 26);
            this.txt_to.TabIndex = 15;
            this.txt_to.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_to_KeyDown);
            // 
            // txt_from
            // 
            this.txt_from.CalendarForeColor = System.Drawing.Color.White;
            this.txt_from.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_from.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_from.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_from.CustomFormat = "dd/MM/yyyy";
            this.txt_from.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_from.Location = new System.Drawing.Point(71, 7);
            this.txt_from.Name = "txt_from";
            this.txt_from.Size = new System.Drawing.Size(177, 26);
            this.txt_from.TabIndex = 15;
            this.txt_from.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_from_KeyDown);
            this.txt_from.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_from_KeyPress);
            // 
            // lbl_customer
            // 
            this.lbl_customer.AutoSize = true;
            this.lbl_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_customer.ForeColor = System.Drawing.Color.White;
            this.lbl_customer.Location = new System.Drawing.Point(532, 11);
            this.lbl_customer.Name = "lbl_customer";
            this.lbl_customer.Size = new System.Drawing.Size(78, 20);
            this.lbl_customer.TabIndex = 5;
            this.lbl_customer.Text = "Customer";
            // 
            // txt_customer
            // 
            this.txt_customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txt_customer.Location = new System.Drawing.Point(624, 6);
            this.txt_customer.Name = "txt_customer";
            this.txt_customer.Size = new System.Drawing.Size(269, 23);
            this.txt_customer.TabIndex = 4;
            this.txt_customer.Click += new System.EventHandler(this.txt_customer_Enter);
            this.txt_customer.TextChanged += new System.EventHandler(this.txt_customer_TextChanged_1);
            this.txt_customer.Enter += new System.EventHandler(this.txt_customer_Enter);
            this.txt_customer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnCustomerKeyDown);
            // 
            // Todate
            // 
            this.Todate.AutoSize = true;
            this.Todate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Todate.ForeColor = System.Drawing.Color.White;
            this.Todate.Location = new System.Drawing.Point(275, 11);
            this.Todate.Name = "Todate";
            this.Todate.Size = new System.Drawing.Size(27, 20);
            this.Todate.TabIndex = 1;
            this.Todate.Text = "To";
            // 
            // lbl_Fromdate
            // 
            this.lbl_Fromdate.AutoSize = true;
            this.lbl_Fromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Fromdate.ForeColor = System.Drawing.Color.White;
            this.lbl_Fromdate.Location = new System.Drawing.Point(23, 10);
            this.lbl_Fromdate.Name = "lbl_Fromdate";
            this.lbl_Fromdate.Size = new System.Drawing.Size(50, 20);
            this.lbl_Fromdate.TabIndex = 0;
            this.lbl_Fromdate.Text = "From ";
            // 
            // grd_SalesSummary
            // 
            this.grd_SalesSummary.AllowUserToAddRows = false;
            this.grd_SalesSummary.BackgroundColor = System.Drawing.Color.White;
            this.grd_SalesSummary.ColumnHeadersHeight = 45;
            this.grd_SalesSummary.Location = new System.Drawing.Point(0, 90);
            this.grd_SalesSummary.Name = "grd_SalesSummary";
            this.grd_SalesSummary.RowHeadersVisible = false;
            this.grd_SalesSummary.Size = new System.Drawing.Size(1019, 419);
            this.grd_SalesSummary.TabIndex = 2;
            this.grd_SalesSummary.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grd_SalesSummary_CellMouseDoubleClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panel3.Location = new System.Drawing.Point(0, 535);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1019, 10);
            this.panel3.TabIndex = 3;
            // 
            // lblTotalAmt
            // 
            this.lblTotalAmt.AutoSize = true;
            this.lblTotalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmt.ForeColor = System.Drawing.Color.White;
            this.lblTotalAmt.Location = new System.Drawing.Point(404, 12);
            this.lblTotalAmt.Name = "lblTotalAmt";
            this.lblTotalAmt.Size = new System.Drawing.Size(40, 20);
            this.lblTotalAmt.TabIndex = 3;
            this.lblTotalAmt.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(281, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total Amount :";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.ForeColor = System.Drawing.Color.White;
            this.lblTotalQty.Location = new System.Drawing.Point(201, 12);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(40, 20);
            this.lblTotalQty.TabIndex = 1;
            this.lblTotalQty.Text = "0.00";
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total.ForeColor = System.Drawing.Color.White;
            this.lbl_total.Location = new System.Drawing.Point(119, 12);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(84, 20);
            this.lbl_total.TabIndex = 0;
            this.lbl_total.Text = "Total Qty  :";
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.White;
            this.btn_print.Location = new System.Drawing.Point(17, 2);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(76, 38);
            this.btn_print.TabIndex = 4;
            this.btn_print.Text = "Print";
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_option
            // 
            this.btn_option.BackColor = System.Drawing.Color.White;
            this.btn_option.Location = new System.Drawing.Point(857, 1);
            this.btn_option.Name = "btn_option";
            this.btn_option.Size = new System.Drawing.Size(76, 38);
            this.btn_option.TabIndex = 5;
            this.btn_option.Text = "Option";
            this.btn_option.UseVisualStyleBackColor = false;
            this.btn_option.Click += new System.EventHandler(this.btn_option_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.White;
            this.btn_exit.Location = new System.Drawing.Point(933, 1);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(76, 38);
            this.btn_exit.TabIndex = 6;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // Pnl_Back2
            // 
            this.Pnl_Back2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back2.Controls.Add(this.Pnllistselect);
            this.Pnl_Back2.Controls.Add(this.txtType);
            this.Pnl_Back2.Controls.Add(this.label2);
            this.Pnl_Back2.Controls.Add(this.btn_ok);
            this.Pnl_Back2.Controls.Add(this.txt_model);
            this.Pnl_Back2.Controls.Add(this.txt_salestypes);
            this.Pnl_Back2.Controls.Add(this.txt_Counter);
            this.Pnl_Back2.Controls.Add(this.txt_Brand);
            this.Pnl_Back2.Controls.Add(this.txt_Group);
            this.Pnl_Back2.Controls.Add(this.txt_OrderBy);
            this.Pnl_Back2.Controls.Add(this.txt_ReportOn);
            this.Pnl_Back2.Controls.Add(this.lbl_Model);
            this.Pnl_Back2.Controls.Add(this.lbl_sales);
            this.Pnl_Back2.Controls.Add(this.lbl_counter);
            this.Pnl_Back2.Controls.Add(this.lbl_brand);
            this.Pnl_Back2.Controls.Add(this.lbl_group);
            this.Pnl_Back2.Controls.Add(this.lbl_orderby);
            this.Pnl_Back2.Controls.Add(this.lbl_reportOn);
            this.Pnl_Back2.Location = new System.Drawing.Point(0, 91);
            this.Pnl_Back2.Name = "Pnl_Back2";
            this.Pnl_Back2.Size = new System.Drawing.Size(1019, 451);
            this.Pnl_Back2.TabIndex = 7;
            // 
            // Pnllistselect
            // 
            this.Pnllistselect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Pnllistselect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnllistselect.Controls.Add(this.label5);
            this.Pnllistselect.Controls.Add(this.listSelect);
            this.Pnllistselect.Location = new System.Drawing.Point(567, 33);
            this.Pnllistselect.Name = "Pnllistselect";
            this.Pnllistselect.Size = new System.Drawing.Size(287, 328);
            this.Pnllistselect.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(88, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Select One";
            // 
            // listSelect
            // 
            this.listSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listSelect.FormattingEnabled = true;
            this.listSelect.ItemHeight = 20;
            this.listSelect.Location = new System.Drawing.Point(7, 36);
            this.listSelect.Name = "listSelect";
            this.listSelect.Size = new System.Drawing.Size(272, 284);
            this.listSelect.TabIndex = 15;
            this.listSelect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listSelect_MouseClick);
            // 
            // txtType
            // 
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txtType.Location = new System.Drawing.Point(215, 333);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(334, 24);
            this.txtType.TabIndex = 17;
            this.txtType.Click += new System.EventHandler(this.txtType_Click);
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            this.txtType.Enter += new System.EventHandler(this.txtType_Click);
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(81, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Type";
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.White;
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok.Location = new System.Drawing.Point(278, 380);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(76, 38);
            this.btn_ok.TabIndex = 14;
            this.btn_ok.Text = "Ok";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // txt_model
            // 
            this.txt_model.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_model.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_model.Location = new System.Drawing.Point(216, 290);
            this.txt_model.Name = "txt_model";
            this.txt_model.Size = new System.Drawing.Size(334, 24);
            this.txt_model.TabIndex = 13;
            this.txt_model.Click += new System.EventHandler(this.txt_model_Click);
            this.txt_model.TextChanged += new System.EventHandler(this.txt_model_TextChanged);
            this.txt_model.Enter += new System.EventHandler(this.txt_model_Click);
            this.txt_model.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txt_salestypes
            // 
            this.txt_salestypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_salestypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_salestypes.Location = new System.Drawing.Point(216, 245);
            this.txt_salestypes.Name = "txt_salestypes";
            this.txt_salestypes.Size = new System.Drawing.Size(334, 24);
            this.txt_salestypes.TabIndex = 12;
            this.txt_salestypes.Click += new System.EventHandler(this.txt_salestypes_Click);
            this.txt_salestypes.TextChanged += new System.EventHandler(this.txt_salestypes_TextChanged);
            this.txt_salestypes.Enter += new System.EventHandler(this.txt_salestypes_Click);
            this.txt_salestypes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txt_Counter
            // 
            this.txt_Counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_Counter.Location = new System.Drawing.Point(216, 202);
            this.txt_Counter.Name = "txt_Counter";
            this.txt_Counter.Size = new System.Drawing.Size(334, 24);
            this.txt_Counter.TabIndex = 11;
            this.txt_Counter.Click += new System.EventHandler(this.txt_Counter_Enter);
            this.txt_Counter.TextChanged += new System.EventHandler(this.txt_Counter_TextChanged);
            this.txt_Counter.Enter += new System.EventHandler(this.txt_Counter_Enter);
            this.txt_Counter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txt_Brand
            // 
            this.txt_Brand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Brand.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_Brand.Location = new System.Drawing.Point(215, 161);
            this.txt_Brand.Name = "txt_Brand";
            this.txt_Brand.Size = new System.Drawing.Size(334, 24);
            this.txt_Brand.TabIndex = 10;
            this.txt_Brand.Click += new System.EventHandler(this.txt_Brand_Click);
            this.txt_Brand.TextChanged += new System.EventHandler(this.txt_Brand_TextChanged);
            this.txt_Brand.Enter += new System.EventHandler(this.txt_Brand_Click);
            this.txt_Brand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txt_Group
            // 
            this.txt_Group.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_Group.Location = new System.Drawing.Point(216, 117);
            this.txt_Group.Name = "txt_Group";
            this.txt_Group.Size = new System.Drawing.Size(334, 24);
            this.txt_Group.TabIndex = 9;
            this.txt_Group.Click += new System.EventHandler(this.txt_Group_Enter);
            this.txt_Group.TextChanged += new System.EventHandler(this.txt_Group_TextChanged);
            this.txt_Group.Enter += new System.EventHandler(this.txt_Group_Enter);
            this.txt_Group.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txt_OrderBy
            // 
            this.txt_OrderBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_OrderBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_OrderBy.Location = new System.Drawing.Point(216, 75);
            this.txt_OrderBy.Name = "txt_OrderBy";
            this.txt_OrderBy.Size = new System.Drawing.Size(334, 24);
            this.txt_OrderBy.TabIndex = 8;
            this.txt_OrderBy.Click += new System.EventHandler(this.txt_OrderBy_Click);
            this.txt_OrderBy.TextChanged += new System.EventHandler(this.txt_OrderBy_TextChanged);
            this.txt_OrderBy.Enter += new System.EventHandler(this.txt_OrderBy_Click);
            this.txt_OrderBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // txt_ReportOn
            // 
            this.txt_ReportOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ReportOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.txt_ReportOn.Location = new System.Drawing.Point(216, 33);
            this.txt_ReportOn.Name = "txt_ReportOn";
            this.txt_ReportOn.Size = new System.Drawing.Size(334, 24);
            this.txt_ReportOn.TabIndex = 7;
            this.txt_ReportOn.Click += new System.EventHandler(this.txt_ReportOn_Click);
            this.txt_ReportOn.TextChanged += new System.EventHandler(this.txt_ReportOn_TextChanged);
            this.txt_ReportOn.Enter += new System.EventHandler(this.txt_ReportOn_Click);
            this.txt_ReportOn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // lbl_Model
            // 
            this.lbl_Model.AutoSize = true;
            this.lbl_Model.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Model.ForeColor = System.Drawing.Color.White;
            this.lbl_Model.Location = new System.Drawing.Point(82, 292);
            this.lbl_Model.Name = "lbl_Model";
            this.lbl_Model.Size = new System.Drawing.Size(52, 20);
            this.lbl_Model.TabIndex = 6;
            this.lbl_Model.Text = "Model";
            // 
            // lbl_sales
            // 
            this.lbl_sales.AutoSize = true;
            this.lbl_sales.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sales.ForeColor = System.Drawing.Color.White;
            this.lbl_sales.Location = new System.Drawing.Point(82, 250);
            this.lbl_sales.Name = "lbl_sales";
            this.lbl_sales.Size = new System.Drawing.Size(95, 20);
            this.lbl_sales.TabIndex = 5;
            this.lbl_sales.Text = "Sales Types";
            // 
            // lbl_counter
            // 
            this.lbl_counter.AutoSize = true;
            this.lbl_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_counter.ForeColor = System.Drawing.Color.White;
            this.lbl_counter.Location = new System.Drawing.Point(82, 207);
            this.lbl_counter.Name = "lbl_counter";
            this.lbl_counter.Size = new System.Drawing.Size(66, 20);
            this.lbl_counter.TabIndex = 4;
            this.lbl_counter.Text = "Counter";
            // 
            // lbl_brand
            // 
            this.lbl_brand.AutoSize = true;
            this.lbl_brand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_brand.ForeColor = System.Drawing.Color.White;
            this.lbl_brand.Location = new System.Drawing.Point(82, 166);
            this.lbl_brand.Name = "lbl_brand";
            this.lbl_brand.Size = new System.Drawing.Size(52, 20);
            this.lbl_brand.TabIndex = 3;
            this.lbl_brand.Text = "Brand";
            // 
            // lbl_group
            // 
            this.lbl_group.AutoSize = true;
            this.lbl_group.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_group.ForeColor = System.Drawing.Color.White;
            this.lbl_group.Location = new System.Drawing.Point(82, 122);
            this.lbl_group.Name = "lbl_group";
            this.lbl_group.Size = new System.Drawing.Size(54, 20);
            this.lbl_group.TabIndex = 2;
            this.lbl_group.Text = "Group";
            // 
            // lbl_orderby
            // 
            this.lbl_orderby.AutoSize = true;
            this.lbl_orderby.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_orderby.ForeColor = System.Drawing.Color.White;
            this.lbl_orderby.Location = new System.Drawing.Point(82, 77);
            this.lbl_orderby.Name = "lbl_orderby";
            this.lbl_orderby.Size = new System.Drawing.Size(71, 20);
            this.lbl_orderby.TabIndex = 1;
            this.lbl_orderby.Text = "Order By";
            // 
            // lbl_reportOn
            // 
            this.lbl_reportOn.AutoSize = true;
            this.lbl_reportOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_reportOn.ForeColor = System.Drawing.Color.White;
            this.lbl_reportOn.Location = new System.Drawing.Point(82, 38);
            this.lbl_reportOn.Name = "lbl_reportOn";
            this.lbl_reportOn.Size = new System.Drawing.Size(83, 20);
            this.lbl_reportOn.TabIndex = 0;
            this.lbl_reportOn.Text = "Report On";
            // 
            // lst_Boxitem
            // 
            this.lst_Boxitem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_Boxitem.FormattingEnabled = true;
            this.lst_Boxitem.ItemHeight = 20;
            this.lst_Boxitem.Location = new System.Drawing.Point(8, 33);
            this.lst_Boxitem.Name = "lst_Boxitem";
            this.lst_Boxitem.Size = new System.Drawing.Size(276, 284);
            this.lst_Boxitem.TabIndex = 15;
            this.lst_Boxitem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_Boxitem_MouseClick);
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomer.Controls.Add(this.label4);
            this.pnlCustomer.Controls.Add(this.lst_Boxitem);
            this.pnlCustomer.Location = new System.Drawing.Point(614, 110);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(293, 326);
            this.pnlCustomer.TabIndex = 8;
            this.pnlCustomer.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(97, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 24);
            this.label4.TabIndex = 16;
            this.label4.Text = "Select One";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.lblTotalAmt);
            this.Pnl_Footer.Controls.Add(this.label3);
            this.Pnl_Footer.Controls.Add(this.btn_print);
            this.Pnl_Footer.Controls.Add(this.lblTotalQty);
            this.Pnl_Footer.Controls.Add(this.btn_option);
            this.Pnl_Footer.Controls.Add(this.lbl_total);
            this.Pnl_Footer.Controls.Add(this.btn_exit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 548);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 42);
            this.Pnl_Footer.TabIndex = 1;
            // 
            // frmItemWiseSalesSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Back2);
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Pnl_Back1);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.grd_SalesSummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmItemWiseSalesSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmItemWiseSalesSummary";
            this.Load += new System.EventHandler(this.frmItemWiseSalesSummary_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Back1.ResumeLayout(false);
            this.Pnl_Back1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_SalesSummary)).EndInit();
            this.Pnl_Back2.ResumeLayout(false);
            this.Pnl_Back2.PerformLayout();
            this.Pnllistselect.ResumeLayout(false);
            this.Pnllistselect.PerformLayout();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Back1;
        private System.Windows.Forms.Label lbl_customer;
        private System.Windows.Forms.TextBox txt_customer;
        private System.Windows.Forms.Label Todate;
        private System.Windows.Forms.Label lbl_Fromdate;
        private System.Windows.Forms.DataGridView grd_SalesSummary;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_total;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_option;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Panel Pnl_Back2;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TextBox txt_model;
        private System.Windows.Forms.TextBox txt_salestypes;
        private System.Windows.Forms.TextBox txt_Counter;
        private System.Windows.Forms.TextBox txt_Brand;
        private System.Windows.Forms.TextBox txt_Group;
        private System.Windows.Forms.TextBox txt_OrderBy;
        private System.Windows.Forms.TextBox txt_ReportOn;
        private System.Windows.Forms.Label lbl_Model;
        private System.Windows.Forms.Label lbl_sales;
        private System.Windows.Forms.Label lbl_counter;
        private System.Windows.Forms.Label lbl_brand;
        private System.Windows.Forms.Label lbl_group;
        private System.Windows.Forms.Label lbl_orderby;
        private System.Windows.Forms.Label lbl_reportOn;
        private System.Windows.Forms.ListBox lst_Boxitem;
        private System.Windows.Forms.DateTimePicker txt_to;
        private System.Windows.Forms.DateTimePicker txt_from;
        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.ListBox listSelect;
        private System.Windows.Forms.Label lblTotalAmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Panel Pnllistselect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}