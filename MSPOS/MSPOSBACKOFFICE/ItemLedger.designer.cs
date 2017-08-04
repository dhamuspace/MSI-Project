namespace MSPOSBACKOFFICE
{
    partial class ItemLedger
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.gridLedger = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssuQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strn_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strn_sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtlederof = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.txtremarks = new System.Windows.Forms.TextBox();
            this.txtlisttype = new System.Windows.Forms.TextBox();
            this.txtcancel = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnllist = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.listview = new System.Windows.Forms.ListBox();
            this.pnltype = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.listtype = new System.Windows.Forms.ListBox();
            this.pnlcancel = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.listcancel = new System.Windows.Forms.ListBox();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.Pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLedger)).BeginInit();
            this.Pnl_Back.SuspendLayout();
            this.pnllist.SuspendLayout();
            this.pnltype.SuspendLayout();
            this.pnlcancel.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 43);
            this.Pnl_Header.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Ledger";
            // 
            // gridLedger
            // 
            this.gridLedger.BackgroundColor = System.Drawing.Color.White;
            this.gridLedger.ColumnHeadersHeight = 40;
            this.gridLedger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Particulars,
            this.Type,
            this.RecQty,
            this.IssuQty,
            this.Value,
            this.strn_no,
            this.strn_sno});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridLedger.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridLedger.Location = new System.Drawing.Point(2, 157);
            this.gridLedger.Name = "gridLedger";
            this.gridLedger.ReadOnly = true;
            this.gridLedger.Size = new System.Drawing.Size(1014, 392);
            this.gridLedger.TabIndex = 1;
            this.gridLedger.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.gridLedger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.gridLedger.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridLedger_CellFormatting);
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Particulars
            // 
            this.Particulars.DataPropertyName = "Particulars";
            this.Particulars.HeaderText = "Particulars";
            this.Particulars.Name = "Particulars";
            this.Particulars.ReadOnly = true;
            this.Particulars.Width = 250;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 150;
            // 
            // RecQty
            // 
            this.RecQty.DataPropertyName = "RecQty";
            this.RecQty.HeaderText = "Rec Qty";
            this.RecQty.Name = "RecQty";
            this.RecQty.ReadOnly = true;
            this.RecQty.Width = 125;
            // 
            // IssuQty
            // 
            this.IssuQty.DataPropertyName = "IssuQty";
            this.IssuQty.HeaderText = "Issu Qty";
            this.IssuQty.Name = "IssuQty";
            this.IssuQty.ReadOnly = true;
            this.IssuQty.Width = 125;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 150;
            // 
            // strn_no
            // 
            this.strn_no.DataPropertyName = "strn_no";
            this.strn_no.HeaderText = "strn_no";
            this.strn_no.Name = "strn_no";
            this.strn_no.ReadOnly = true;
            this.strn_no.Visible = false;
            // 
            // strn_sno
            // 
            this.strn_sno.DataPropertyName = "strn_sno";
            this.strn_sno.HeaderText = "strn_sno";
            this.strn_sno.Name = "strn_sno";
            this.strn_sno.ReadOnly = true;
            this.strn_sno.Visible = false;
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back.Controls.Add(this.comboBox1);
            this.Pnl_Back.Controls.Add(this.label7);
            this.Pnl_Back.Controls.Add(this.label6);
            this.Pnl_Back.Controls.Add(this.label5);
            this.Pnl_Back.Controls.Add(this.txtlederof);
            this.Pnl_Back.Controls.Add(this.label4);
            this.Pnl_Back.Controls.Add(this.label3);
            this.Pnl_Back.Controls.Add(this.label2);
            this.Pnl_Back.Controls.Add(this.dtpTo);
            this.Pnl_Back.Controls.Add(this.dtpFrom);
            this.Pnl_Back.Controls.Add(this.txtremarks);
            this.Pnl_Back.Controls.Add(this.txtlisttype);
            this.Pnl_Back.Controls.Add(this.txtcancel);
            this.Pnl_Back.Location = new System.Drawing.Point(1, 43);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1017, 84);
            this.Pnl_Back.TabIndex = 1;
            this.Pnl_Back.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Item Name or Code",
            "Barcode"});
            this.comboBox1.Location = new System.Drawing.Point(461, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 28);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "Search By";
            this.comboBox1.DropDownClosed += new System.EventHandler(this.comboBox1_DropDownClosed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(6, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Cancel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(666, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ledger Of";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(752, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Remarks";
            // 
            // txtlederof
            // 
            this.txtlederof.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlederof.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlederof.Location = new System.Drawing.Point(752, 6);
            this.txtlederof.Name = "txtlederof";
            this.txtlederof.Size = new System.Drawing.Size(248, 26);
            this.txtlederof.TabIndex = 0;
            this.txtlederof.TextChanged += new System.EventHandler(this.txtlederof_TextChanged);
            this.txtlederof.Enter += new System.EventHandler(this.txtlederof_Enter);
            this.txtlederof.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            this.txtlederof.Leave += new System.EventHandler(this.txtlederof_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(403, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(225, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "From";
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpTo.CalendarForeColor = System.Drawing.Color.White;
            this.dtpTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpTo.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpTo.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(265, 7);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(130, 26);
            this.dtpTo.TabIndex = 7;
            this.dtpTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyDown);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpFrom.CalendarForeColor = System.Drawing.Color.White;
            this.dtpFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpFrom.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpFrom.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(79, 7);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(130, 26);
            this.dtpFrom.TabIndex = 6;
            this.dtpFrom.Enter += new System.EventHandler(this.dateTimePicker1_Enter);
            this.dtpFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // txtremarks
            // 
            this.txtremarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtremarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtremarks.Location = new System.Drawing.Point(833, 46);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(170, 26);
            this.txtremarks.TabIndex = 5;
            this.txtremarks.TextChanged += new System.EventHandler(this.txtremarks_TextChanged);
            this.txtremarks.Enter += new System.EventHandler(this.txtremarks_Enter);
            this.txtremarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtremarks_KeyDown);
            // 
            // txtlisttype
            // 
            this.txtlisttype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlisttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlisttype.Location = new System.Drawing.Point(461, 48);
            this.txtlisttype.Name = "txtlisttype";
            this.txtlisttype.Size = new System.Drawing.Size(285, 26);
            this.txtlisttype.TabIndex = 4;
            this.txtlisttype.TextChanged += new System.EventHandler(this.txtlisttype_TextChanged);
            this.txtlisttype.Enter += new System.EventHandler(this.txtlisttype_Enter);
            this.txtlisttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown1);
            this.txtlisttype.Leave += new System.EventHandler(this.txtlisttype_Leave);
            // 
            // txtcancel
            // 
            this.txtcancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcancel.Location = new System.Drawing.Point(79, 46);
            this.txtcancel.Name = "txtcancel";
            this.txtcancel.Size = new System.Drawing.Size(316, 26);
            this.txtcancel.TabIndex = 1;
            this.txtcancel.Text = "Not Cancelled";
            this.txtcancel.TextChanged += new System.EventHandler(this.txtcancel_TextChanged);
            this.txtcancel.Enter += new System.EventHandler(this.txtcancel_Enter);
            this.txtcancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown2);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(928, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(76, 38);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnllist
            // 
            this.pnllist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnllist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnllist.Controls.Add(this.label8);
            this.pnllist.Controls.Add(this.listview);
            this.pnllist.Location = new System.Drawing.Point(-1, 140);
            this.pnllist.Name = "pnllist";
            this.pnllist.Size = new System.Drawing.Size(617, 362);
            this.pnllist.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(237, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "Select One";
            // 
            // listview
            // 
            this.listview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listview.FormattingEnabled = true;
            this.listview.ItemHeight = 20;
            this.listview.Location = new System.Drawing.Point(6, 32);
            this.listview.Name = "listview";
            this.listview.Size = new System.Drawing.Size(605, 322);
            this.listview.TabIndex = 0;
            this.listview.Click += new System.EventHandler(this.listview_Click);
            // 
            // pnltype
            // 
            this.pnltype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnltype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnltype.Controls.Add(this.label9);
            this.pnltype.Controls.Add(this.pnllist);
            this.pnltype.Controls.Add(this.listtype);
            this.pnltype.Location = new System.Drawing.Point(411, 133);
            this.pnltype.Name = "pnltype";
            this.pnltype.Size = new System.Drawing.Size(345, 167);
            this.pnltype.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(105, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 25);
            this.label9.TabIndex = 1;
            this.label9.Text = "Select One";
            // 
            // listtype
            // 
            this.listtype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listtype.FormattingEnabled = true;
            this.listtype.ItemHeight = 20;
            this.listtype.Items.AddRange(new object[] {
            "Sales"});
            this.listtype.Location = new System.Drawing.Point(7, 37);
            this.listtype.Name = "listtype";
            this.listtype.Size = new System.Drawing.Size(328, 122);
            this.listtype.TabIndex = 0;
            this.listtype.Click += new System.EventHandler(this.listtype_Click);
            // 
            // pnlcancel
            // 
            this.pnlcancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlcancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlcancel.Controls.Add(this.label10);
            this.pnlcancel.Controls.Add(this.listcancel);
            this.pnlcancel.Location = new System.Drawing.Point(77, 133);
            this.pnlcancel.Name = "pnlcancel";
            this.pnlcancel.Size = new System.Drawing.Size(318, 170);
            this.pnlcancel.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(71, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 25);
            this.label10.TabIndex = 1;
            this.label10.Text = "Select One";
            // 
            // listcancel
            // 
            this.listcancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listcancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listcancel.FormattingEnabled = true;
            this.listcancel.ItemHeight = 20;
            this.listcancel.Items.AddRange(new object[] {
            "ALL",
            "Cancelled",
            "Not Cancelled"});
            this.listcancel.Location = new System.Drawing.Point(6, 40);
            this.listcancel.Name = "listcancel";
            this.listcancel.Size = new System.Drawing.Size(304, 122);
            this.listcancel.TabIndex = 0;
            this.listcancel.Click += new System.EventHandler(this.listcancel_Click);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 550);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 42);
            this.Pnl_Footer.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(849, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(76, 38);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ItemLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.pnlcancel);
            this.Controls.Add(this.pnltype);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.gridLedger);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemLedger";
            this.Text = "ItemLedger";
            this.Load += new System.EventHandler(this.ItemLedger_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLedger)).EndInit();
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.pnllist.ResumeLayout(false);
            this.pnllist.PerformLayout();
            this.pnltype.ResumeLayout(false);
            this.pnltype.PerformLayout();
            this.pnlcancel.ResumeLayout(false);
            this.pnlcancel.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridLedger;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.TextBox txtremarks;
        private System.Windows.Forms.TextBox txtlisttype;
        private System.Windows.Forms.TextBox txtcancel;
        private System.Windows.Forms.TextBox txtlederof;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnllist;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listview;
        private System.Windows.Forms.Panel pnltype;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listtype;
        private System.Windows.Forms.Panel pnlcancel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listcancel;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssuQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn strn_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn strn_sno;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnPrint;
    }
}