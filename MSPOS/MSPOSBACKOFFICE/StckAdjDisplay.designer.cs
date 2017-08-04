namespace MSPOSBACKOFFICE
{
    partial class StckAdjDisplay
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_print = new System.Windows.Forms.Button();
            this.lbl_amt = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hideColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unHideColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_countername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.countername = new System.Windows.Forms.Label();
            this.Pnl_Back1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_todate = new System.Windows.Forms.DateTimePicker();
            this.txt_item = new System.Windows.Forms.TextBox();
            this.lbl_stckentrydate = new System.Windows.Forms.Label();
            this.txt_fromdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cancel = new System.Windows.Forms.TextBox();
            this.pnl_Cancel = new System.Windows.Forms.Panel();
            this.lbl_cancel = new System.Windows.Forms.Label();
            this.lst_cancel = new System.Windows.Forms.ListBox();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.lbl_stckbanner = new System.Windows.Forms.Label();
            this.grd_adj_dis = new System.Windows.Forms.DataGridView();
            this.Pnl_Back2 = new System.Windows.Forms.Panel();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.pnl_ctr_name = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_ctrname = new System.Windows.Forms.ListBox();
            this.pnl_item = new System.Windows.Forms.Panel();
            this.s = new System.Windows.Forms.Label();
            this.lst_item = new System.Windows.Forms.ListBox();
            this.contextMenu.SuspendLayout();
            this.Pnl_Back1.SuspendLayout();
            this.pnl_Cancel.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_adj_dis)).BeginInit();
            this.Pnl_Back2.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.pnl_ctr_name.SuspendLayout();
            this.pnl_item.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.White;
            this.btn_print.Location = new System.Drawing.Point(7, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(76, 38);
            this.btn_print.TabIndex = 53;
            this.btn_print.Text = "&Print";
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lbl_amt
            // 
            this.lbl_amt.AutoSize = true;
            this.lbl_amt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_amt.ForeColor = System.Drawing.Color.White;
            this.lbl_amt.Location = new System.Drawing.Point(321, 10);
            this.lbl_amt.Name = "lbl_amt";
            this.lbl_amt.Size = new System.Drawing.Size(38, 24);
            this.lbl_amt.TabIndex = 23;
            this.lbl_amt.Text = "0.0";
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.White;
            this.btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_exit.Location = new System.Drawing.Point(931, 3);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(76, 38);
            this.btn_exit.TabIndex = 52;
            this.btn_exit.Text = "E&xit";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.White;
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_cancel.Location = new System.Drawing.Point(1103, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(76, 38);
            this.btn_cancel.TabIndex = 51;
            this.btn_cancel.Text = "&Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg.ForeColor = System.Drawing.Color.White;
            this.lbl_msg.Location = new System.Drawing.Point(101, 9);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(214, 24);
            this.lbl_msg.TabIndex = 22;
            this.lbl_msg.Text = "Total Adjustment       :";
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.White;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_add.Location = new System.Drawing.Point(856, 4);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(76, 38);
            this.btn_add.TabIndex = 50;
            this.btn_add.Text = "&Add";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideColumnsToolStripMenuItem,
            this.unHideColumnsToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveSettingsToolStripMenuItem,
            this.defaultSettingsToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(166, 98);
            // 
            // hideColumnsToolStripMenuItem
            // 
            this.hideColumnsToolStripMenuItem.Name = "hideColumnsToolStripMenuItem";
            this.hideColumnsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.hideColumnsToolStripMenuItem.Text = "Hide Columns";
            // 
            // unHideColumnsToolStripMenuItem
            // 
            this.unHideColumnsToolStripMenuItem.Name = "unHideColumnsToolStripMenuItem";
            this.unHideColumnsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.unHideColumnsToolStripMenuItem.Text = "UnHide Columns";
            // 
            // defaultSettingsToolStripMenuItem
            // 
            this.defaultSettingsToolStripMenuItem.Name = "defaultSettingsToolStripMenuItem";
            this.defaultSettingsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.defaultSettingsToolStripMenuItem.Text = "Default Settings";
            // 
            // txt_countername
            // 
            this.txt_countername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_countername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_countername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_countername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_countername.Location = new System.Drawing.Point(150, 2);
            this.txt_countername.Name = "txt_countername";
            this.txt_countername.Size = new System.Drawing.Size(240, 22);
            this.txt_countername.TabIndex = 2;
            this.txt_countername.Click += new System.EventHandler(this.txt_countername_Click_1);
            this.txt_countername.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_countername_MouseClick);
            this.txt_countername.TextChanged += new System.EventHandler(this.txt_countername_TextChanged);
            this.txt_countername.Enter += new System.EventHandler(this.txt_countername_Enter);
            this.txt_countername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_countername_KeyDown);
            this.txt_countername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_countername_KeyPress);
            this.txt_countername.Leave += new System.EventHandler(this.txt_countername_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(1, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Cancel             :";
            // 
            // countername
            // 
            this.countername.AutoSize = true;
            this.countername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countername.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.countername.Location = new System.Drawing.Point(1, 5);
            this.countername.Name = "countername";
            this.countername.Size = new System.Drawing.Size(100, 16);
            this.countername.TabIndex = 40;
            this.countername.Text = "Counter Name :";
            // 
            // Pnl_Back1
            // 
            this.Pnl_Back1.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Back1.Controls.Add(this.label1);
            this.Pnl_Back1.Controls.Add(this.txt_todate);
            this.Pnl_Back1.Controls.Add(this.txt_item);
            this.Pnl_Back1.Controls.Add(this.lbl_stckentrydate);
            this.Pnl_Back1.Controls.Add(this.txt_fromdate);
            this.Pnl_Back1.Controls.Add(this.label2);
            this.Pnl_Back1.Controls.Add(this.txt_cancel);
            this.Pnl_Back1.Controls.Add(this.txt_countername);
            this.Pnl_Back1.Controls.Add(this.label3);
            this.Pnl_Back1.Controls.Add(this.countername);
            this.Pnl_Back1.Location = new System.Drawing.Point(1, 45);
            this.Pnl_Back1.Name = "Pnl_Back1";
            this.Pnl_Back1.Size = new System.Drawing.Size(1019, 97);
            this.Pnl_Back1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(406, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "&To";
            // 
            // txt_todate
            // 
            this.txt_todate.CalendarForeColor = System.Drawing.Color.White;
            this.txt_todate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_todate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_todate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_todate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txt_todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_todate.Location = new System.Drawing.Point(463, 42);
            this.txt_todate.Name = "txt_todate";
            this.txt_todate.Size = new System.Drawing.Size(145, 23);
            this.txt_todate.TabIndex = 43;
            this.txt_todate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_todate_KeyDown);
            this.txt_todate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_todate_KeyPress);
            // 
            // txt_item
            // 
            this.txt_item.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_item.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_item.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_item.Location = new System.Drawing.Point(724, 0);
            this.txt_item.Name = "txt_item";
            this.txt_item.Size = new System.Drawing.Size(239, 22);
            this.txt_item.TabIndex = 3;
            this.txt_item.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_item_MouseClick);
            this.txt_item.TextChanged += new System.EventHandler(this.txt_item_TextChanged);
            this.txt_item.Enter += new System.EventHandler(this.txt_item_Enter);
            this.txt_item.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_item_KeyDown);
            this.txt_item.Leave += new System.EventHandler(this.txt_item_Leave);
            // 
            // lbl_stckentrydate
            // 
            this.lbl_stckentrydate.AutoSize = true;
            this.lbl_stckentrydate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stckentrydate.ForeColor = System.Drawing.Color.White;
            this.lbl_stckentrydate.Location = new System.Drawing.Point(406, 6);
            this.lbl_stckentrydate.Name = "lbl_stckentrydate";
            this.lbl_stckentrydate.Size = new System.Drawing.Size(39, 16);
            this.lbl_stckentrydate.TabIndex = 0;
            this.lbl_stckentrydate.Text = "&From";
            // 
            // txt_fromdate
            // 
            this.txt_fromdate.CalendarForeColor = System.Drawing.Color.White;
            this.txt_fromdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_fromdate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_fromdate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_fromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txt_fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_fromdate.Location = new System.Drawing.Point(466, -1);
            this.txt_fromdate.Name = "txt_fromdate";
            this.txt_fromdate.Size = new System.Drawing.Size(145, 23);
            this.txt_fromdate.TabIndex = 42;
            this.txt_fromdate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fromdate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(616, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Item                :";
            // 
            // txt_cancel
            // 
            this.txt_cancel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_cancel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_cancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cancel.Location = new System.Drawing.Point(150, 40);
            this.txt_cancel.Name = "txt_cancel";
            this.txt_cancel.Size = new System.Drawing.Size(240, 22);
            this.txt_cancel.TabIndex = 4;
            this.txt_cancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_cancel_MouseClick);
            this.txt_cancel.TextChanged += new System.EventHandler(this.txt_cancel_TextChanged);
            this.txt_cancel.Enter += new System.EventHandler(this.txt_cancel_Enter);
            this.txt_cancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cancel_KeyDown);
            this.txt_cancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cancel_KeyPress);
            this.txt_cancel.Leave += new System.EventHandler(this.txt_cancel_Leave);
            // 
            // pnl_Cancel
            // 
            this.pnl_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_Cancel.Controls.Add(this.lbl_cancel);
            this.pnl_Cancel.Controls.Add(this.lst_cancel);
            this.pnl_Cancel.Location = new System.Drawing.Point(123, 6);
            this.pnl_Cancel.Name = "pnl_Cancel";
            this.pnl_Cancel.Size = new System.Drawing.Size(275, 282);
            this.pnl_Cancel.TabIndex = 43;
            // 
            // lbl_cancel
            // 
            this.lbl_cancel.AutoSize = true;
            this.lbl_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cancel.ForeColor = System.Drawing.Color.White;
            this.lbl_cancel.Location = new System.Drawing.Point(57, 6);
            this.lbl_cancel.Name = "lbl_cancel";
            this.lbl_cancel.Size = new System.Drawing.Size(153, 20);
            this.lbl_cancel.TabIndex = 36;
            this.lbl_cancel.Text = "Select Cancel Name";
            // 
            // lst_cancel
            // 
            this.lst_cancel.BackColor = System.Drawing.Color.White;
            this.lst_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_cancel.ForeColor = System.Drawing.Color.Black;
            this.lst_cancel.FormattingEnabled = true;
            this.lst_cancel.ItemHeight = 20;
            this.lst_cancel.Items.AddRange(new object[] {
            "All",
            "Cancelled",
            "Not Cancelled"});
            this.lst_cancel.Location = new System.Drawing.Point(6, 33);
            this.lst_cancel.Name = "lst_cancel";
            this.lst_cancel.Size = new System.Drawing.Size(264, 244);
            this.lst_cancel.TabIndex = 10;
            this.lst_cancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_cancel_MouseClick);
            this.lst_cancel.SelectedIndexChanged += new System.EventHandler(this.lst_cancel_SelectedIndexChanged);
            this.lst_cancel.Enter += new System.EventHandler(this.lst_cancel_Enter);
            this.lst_cancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_cancel_KeyPress);
            this.lst_cancel.Leave += new System.EventHandler(this.lst_cancel_Leave);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_stckbanner);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 38);
            this.Pnl_Header.TabIndex = 39;
            // 
            // lbl_stckbanner
            // 
            this.lbl_stckbanner.AutoSize = true;
            this.lbl_stckbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stckbanner.ForeColor = System.Drawing.Color.White;
            this.lbl_stckbanner.Location = new System.Drawing.Point(1, 5);
            this.lbl_stckbanner.Name = "lbl_stckbanner";
            this.lbl_stckbanner.Size = new System.Drawing.Size(197, 25);
            this.lbl_stckbanner.TabIndex = 0;
            this.lbl_stckbanner.Text = "Adjustment Display";
            // 
            // grd_adj_dis
            // 
            this.grd_adj_dis.AllowUserToAddRows = false;
            this.grd_adj_dis.BackgroundColor = System.Drawing.Color.White;
            this.grd_adj_dis.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_adj_dis.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_adj_dis.ColumnHeadersHeight = 50;
            this.grd_adj_dis.ContextMenuStrip = this.contextMenu;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_adj_dis.DefaultCellStyle = dataGridViewCellStyle5;
            this.grd_adj_dis.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grd_adj_dis.Location = new System.Drawing.Point(3, 6);
            this.grd_adj_dis.Name = "grd_adj_dis";
            this.grd_adj_dis.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_adj_dis.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grd_adj_dis.RowHeadersVisible = false;
            this.grd_adj_dis.RowHeadersWidth = 30;
            this.grd_adj_dis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grd_adj_dis.Size = new System.Drawing.Size(1015, 367);
            this.grd_adj_dis.StandardTab = true;
            this.grd_adj_dis.TabIndex = 35;
            this.grd_adj_dis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_adj_dis_CellContentClick);
            this.grd_adj_dis.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_adj_dis_CellDoubleClick);
            this.grd_adj_dis.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grd_adj_dis_CellFormatting);
            this.grd_adj_dis.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grd_adj_dis_DataBindingComplete);
            this.grd_adj_dis.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_adj_dis_RowEnter);
            this.grd_adj_dis.Enter += new System.EventHandler(this.grd_adj_dis_Enter);
            this.grd_adj_dis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grd_adj_dis_KeyDown);
            this.grd_adj_dis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grd_adj_dis_KeyPress);
            // 
            // Pnl_Back2
            // 
            this.Pnl_Back2.Controls.Add(this.Pnl_Footer);
            this.Pnl_Back2.Controls.Add(this.pnl_ctr_name);
            this.Pnl_Back2.Controls.Add(this.pnl_Cancel);
            this.Pnl_Back2.Controls.Add(this.pnl_item);
            this.Pnl_Back2.Controls.Add(this.grd_adj_dis);
            this.Pnl_Back2.Location = new System.Drawing.Point(0, 149);
            this.Pnl_Back2.Name = "Pnl_Back2";
            this.Pnl_Back2.Size = new System.Drawing.Size(1018, 450);
            this.Pnl_Back2.TabIndex = 40;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.lbl_amt);
            this.Pnl_Footer.Controls.Add(this.lbl_msg);
            this.Pnl_Footer.Controls.Add(this.btn_print);
            this.Pnl_Footer.Controls.Add(this.btn_exit);
            this.Pnl_Footer.Controls.Add(this.btn_add);
            this.Pnl_Footer.Controls.Add(this.btn_cancel);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 397);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 43);
            this.Pnl_Footer.TabIndex = 40;
            // 
            // pnl_ctr_name
            // 
            this.pnl_ctr_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_ctr_name.Controls.Add(this.label4);
            this.pnl_ctr_name.Controls.Add(this.lst_ctrname);
            this.pnl_ctr_name.Location = new System.Drawing.Point(121, 5);
            this.pnl_ctr_name.Name = "pnl_ctr_name";
            this.pnl_ctr_name.Size = new System.Drawing.Size(275, 282);
            this.pnl_ctr_name.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(57, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "Select Counter Name";
            // 
            // lst_ctrname
            // 
            this.lst_ctrname.BackColor = System.Drawing.Color.White;
            this.lst_ctrname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_ctrname.ForeColor = System.Drawing.Color.Black;
            this.lst_ctrname.FormattingEnabled = true;
            this.lst_ctrname.ItemHeight = 20;
            this.lst_ctrname.Location = new System.Drawing.Point(6, 33);
            this.lst_ctrname.Name = "lst_ctrname";
            this.lst_ctrname.Size = new System.Drawing.Size(264, 244);
            this.lst_ctrname.TabIndex = 10;
            this.lst_ctrname.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_ctrname_MouseClick);
            this.lst_ctrname.SelectedIndexChanged += new System.EventHandler(this.lst_itemname_SelectedIndexChanged);
            this.lst_ctrname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_itemname_KeyPress);
            this.lst_ctrname.Leave += new System.EventHandler(this.lst_itemname_Leave);
            // 
            // pnl_item
            // 
            this.pnl_item.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_item.Controls.Add(this.s);
            this.pnl_item.Controls.Add(this.lst_item);
            this.pnl_item.Location = new System.Drawing.Point(703, 6);
            this.pnl_item.Name = "pnl_item";
            this.pnl_item.Size = new System.Drawing.Size(275, 282);
            this.pnl_item.TabIndex = 43;
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.s.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s.ForeColor = System.Drawing.Color.White;
            this.s.Location = new System.Drawing.Point(57, 6);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(136, 20);
            this.s.TabIndex = 36;
            this.s.Text = "Select Item Name";
            // 
            // lst_item
            // 
            this.lst_item.BackColor = System.Drawing.Color.White;
            this.lst_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_item.ForeColor = System.Drawing.Color.Black;
            this.lst_item.FormattingEnabled = true;
            this.lst_item.ItemHeight = 20;
            this.lst_item.Location = new System.Drawing.Point(6, 33);
            this.lst_item.Name = "lst_item";
            this.lst_item.Size = new System.Drawing.Size(264, 244);
            this.lst_item.TabIndex = 10;
            this.lst_item.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lst_item_MouseClick);
            this.lst_item.SelectedIndexChanged += new System.EventHandler(this.lst_item_SelectedIndexChanged);
            this.lst_item.Enter += new System.EventHandler(this.lst_item_Enter);
            this.lst_item.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lst_item_KeyPress);
            this.lst_item.Leave += new System.EventHandler(this.lst_item_Leave);
            // 
            // StckAdjDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 592);
            this.Controls.Add(this.Pnl_Back2);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.Pnl_Back1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "StckAdjDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StckAdjDisplay";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.StckAdjDisplay_Load);
            this.contextMenu.ResumeLayout(false);
            this.Pnl_Back1.ResumeLayout(false);
            this.Pnl_Back1.PerformLayout();
            this.pnl_Cancel.ResumeLayout(false);
            this.pnl_Cancel.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_adj_dis)).EndInit();
            this.Pnl_Back2.ResumeLayout(false);
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.pnl_ctr_name.ResumeLayout(false);
            this.pnl_ctr_name.PerformLayout();
            this.pnl_item.ResumeLayout(false);
            this.pnl_item.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label lbl_amt;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem hideColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unHideColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultSettingsToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_countername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label countername;
        private System.Windows.Forms.Panel Pnl_Back1;
        private System.Windows.Forms.Label lbl_stckentrydate;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_stckbanner;
        private System.Windows.Forms.DataGridView grd_adj_dis;
        private System.Windows.Forms.Panel Pnl_Back2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox txt_cancel;
        private System.Windows.Forms.TextBox txt_item;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnl_ctr_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lst_ctrname;
        private System.Windows.Forms.Panel pnl_item;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.ListBox lst_item;
        private System.Windows.Forms.Panel pnl_Cancel;
        private System.Windows.Forms.Label lbl_cancel;
        private System.Windows.Forms.ListBox lst_cancel;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.DateTimePicker txt_todate;
        private System.Windows.Forms.DateTimePicker txt_fromdate;
        private System.Windows.Forms.Panel Pnl_Footer;
    }
}