namespace MSPOSBACKOFFICE
{
    partial class frmDailySalesSummary
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
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.txt_counter = new System.Windows.Forms.TextBox();
            this.txt_reportOn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_report_on = new System.Windows.Forms.Label();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.dt_to = new System.Windows.Forms.DateTimePicker();
            this.dt_from = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_SummaryBanner = new System.Windows.Forms.Label();
            this.grdDailySummary = new System.Windows.Forms.DataGridView();
            this.S_Retail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Whole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Return = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.pnl_Amount = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lst_ofAmount = new System.Windows.Forms.ListBox();
            this.Pnl_counter = new System.Windows.Forms.Panel();
            this.lbl_counter = new System.Windows.Forms.Label();
            this.lst_counter = new System.Windows.Forms.ListBox();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.Pnl_Back.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDailySummary)).BeginInit();
            this.Pnl_Header.SuspendLayout();
            this.pnl_Amount.SuspendLayout();
            this.Pnl_counter.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(48, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Date";
            // 
            // btn_Print
            // 
            this.btn_Print.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Print.Location = new System.Drawing.Point(862, 3);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(69, 37);
            this.btn_Print.TabIndex = 12;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Exit.Location = new System.Drawing.Point(933, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 38);
            this.btn_Exit.TabIndex = 14;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // txt_counter
            // 
            this.txt_counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_counter.Location = new System.Drawing.Point(802, 16);
            this.txt_counter.Name = "txt_counter";
            this.txt_counter.Size = new System.Drawing.Size(203, 22);
            this.txt_counter.TabIndex = 3;
            this.txt_counter.Click += new System.EventHandler(this.txt_counter_Enter);
            this.txt_counter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_counter_MouseClick);
            this.txt_counter.TextChanged += new System.EventHandler(this.txt_counter_TextChanged);
            this.txt_counter.Enter += new System.EventHandler(this.txt_counter_Enter);
            this.txt_counter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_counter_KeyDown);
            // 
            // txt_reportOn
            // 
            this.txt_reportOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_reportOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_reportOn.Location = new System.Drawing.Point(124, 14);
            this.txt_reportOn.Name = "txt_reportOn";
            this.txt_reportOn.Size = new System.Drawing.Size(238, 22);
            this.txt_reportOn.TabIndex = 2;
            this.txt_reportOn.Click += new System.EventHandler(this.txt_reportOn_Enter);
            this.txt_reportOn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_reportOn_MouseClick);
            this.txt_reportOn.Enter += new System.EventHandler(this.txt_reportOn_Enter);
            this.txt_reportOn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_reportOn_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(738, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Counter";
            // 
            // lbl_report_on
            // 
            this.lbl_report_on.AutoSize = true;
            this.lbl_report_on.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_report_on.ForeColor = System.Drawing.Color.White;
            this.lbl_report_on.Location = new System.Drawing.Point(38, 17);
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
            this.Pnl_Back.Controls.Add(this.txt_counter);
            this.Pnl_Back.Controls.Add(this.label4);
            this.Pnl_Back.Controls.Add(this.txt_reportOn);
            this.Pnl_Back.Controls.Add(this.label3);
            this.Pnl_Back.Controls.Add(this.label2);
            this.Pnl_Back.Controls.Add(this.lbl_report_on);
            this.Pnl_Back.Location = new System.Drawing.Point(1, 46);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1019, 51);
            this.Pnl_Back.TabIndex = 9;
            // 
            // dt_to
            // 
            this.dt_to.CalendarForeColor = System.Drawing.Color.White;
            this.dt_to.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dt_to.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dt_to.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dt_to.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_to.Location = new System.Drawing.Point(607, 14);
            this.dt_to.Name = "dt_to";
            this.dt_to.Size = new System.Drawing.Size(111, 22);
            this.dt_to.TabIndex = 9;
            this.dt_to.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dt_to_KeyDown);
            // 
            // dt_from
            // 
            this.dt_from.CalendarForeColor = System.Drawing.Color.White;
            this.dt_from.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dt_from.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dt_from.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dt_from.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_from.Location = new System.Drawing.Point(436, 14);
            this.dt_from.Name = "dt_from";
            this.dt_from.Size = new System.Drawing.Size(108, 22);
            this.dt_from.TabIndex = 8;
            this.dt_from.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dt_from_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(564, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(378, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "From";
            // 
            // lbl_SummaryBanner
            // 
            this.lbl_SummaryBanner.AutoSize = true;
            this.lbl_SummaryBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SummaryBanner.ForeColor = System.Drawing.Color.White;
            this.lbl_SummaryBanner.Location = new System.Drawing.Point(4, 6);
            this.lbl_SummaryBanner.Name = "lbl_SummaryBanner";
            this.lbl_SummaryBanner.Size = new System.Drawing.Size(199, 25);
            this.lbl_SummaryBanner.TabIndex = 0;
            this.lbl_SummaryBanner.Text = "Daily Sales Summary";
            // 
            // grdDailySummary
            // 
            this.grdDailySummary.BackgroundColor = System.Drawing.Color.White;
            this.grdDailySummary.ColumnHeadersHeight = 40;
            this.grdDailySummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.S_Retail,
            this.S_Whole,
            this.S_Return,
            this.S_Total});
            this.grdDailySummary.Location = new System.Drawing.Point(0, 99);
            this.grdDailySummary.Name = "grdDailySummary";
            this.grdDailySummary.RowHeadersWidth = 200;
            this.grdDailySummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdDailySummary.Size = new System.Drawing.Size(1018, 448);
            this.grdDailySummary.TabIndex = 10;
            this.grdDailySummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDailySummary_CellDoubleClick);
            this.grdDailySummary.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdDailySummary_RowHeaderMouseDoubleClick);
            this.grdDailySummary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdDailySummary_KeyDown);
            // 
            // S_Retail
            // 
            this.S_Retail.HeaderText = "Retail";
            this.S_Retail.Name = "S_Retail";
            this.S_Retail.ReadOnly = true;
            // 
            // S_Whole
            // 
            this.S_Whole.HeaderText = "Whole";
            this.S_Whole.Name = "S_Whole";
            this.S_Whole.ReadOnly = true;
            // 
            // S_Return
            // 
            this.S_Return.HeaderText = "Return";
            this.S_Return.Name = "S_Return";
            this.S_Return.ReadOnly = true;
            // 
            // S_Total
            // 
            this.S_Total.HeaderText = "Total";
            this.S_Total.Name = "S_Total";
            this.S_Total.ReadOnly = true;
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_SummaryBanner);
            this.Pnl_Header.Location = new System.Drawing.Point(-1, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 49);
            this.Pnl_Header.TabIndex = 8;
            // 
            // pnl_Amount
            // 
            this.pnl_Amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_Amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Amount.Controls.Add(this.label5);
            this.pnl_Amount.Controls.Add(this.lst_ofAmount);
            this.pnl_Amount.Location = new System.Drawing.Point(125, 91);
            this.pnl_Amount.Name = "pnl_Amount";
            this.pnl_Amount.Size = new System.Drawing.Size(238, 227);
            this.pnl_Amount.TabIndex = 17;
            this.pnl_Amount.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(72, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Select One";
            // 
            // lst_ofAmount
            // 
            this.lst_ofAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_ofAmount.FormattingEnabled = true;
            this.lst_ofAmount.ItemHeight = 20;
            this.lst_ofAmount.Items.AddRange(new object[] {
            "Gross Amount",
            "Nett Amount"});
            this.lst_ofAmount.Location = new System.Drawing.Point(7, 34);
            this.lst_ofAmount.Name = "lst_ofAmount";
            this.lst_ofAmount.Size = new System.Drawing.Size(225, 184);
            this.lst_ofAmount.TabIndex = 8;
            this.lst_ofAmount.Click += new System.EventHandler(this.lst_ofAmount_Click);
            this.lst_ofAmount.SelectedIndexChanged += new System.EventHandler(this.lst_ofAmount_SelectedIndexChanged);
            this.lst_ofAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_ofAmount_KeyDown);
            // 
            // Pnl_counter
            // 
            this.Pnl_counter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Pnl_counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_counter.Controls.Add(this.lbl_counter);
            this.Pnl_counter.Controls.Add(this.lst_counter);
            this.Pnl_counter.Location = new System.Drawing.Point(720, 103);
            this.Pnl_counter.Name = "Pnl_counter";
            this.Pnl_counter.Size = new System.Drawing.Size(295, 228);
            this.Pnl_counter.TabIndex = 16;
            this.Pnl_counter.Visible = false;
            // 
            // lbl_counter
            // 
            this.lbl_counter.AutoSize = true;
            this.lbl_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_counter.ForeColor = System.Drawing.Color.White;
            this.lbl_counter.Location = new System.Drawing.Point(90, 5);
            this.lbl_counter.Name = "lbl_counter";
            this.lbl_counter.Size = new System.Drawing.Size(124, 20);
            this.lbl_counter.TabIndex = 9;
            this.lbl_counter.Text = "List Of Counter";
            // 
            // lst_counter
            // 
            this.lst_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_counter.FormattingEnabled = true;
            this.lst_counter.ItemHeight = 20;
            this.lst_counter.Location = new System.Drawing.Point(6, 35);
            this.lst_counter.Name = "lst_counter";
            this.lst_counter.Size = new System.Drawing.Size(282, 184);
            this.lst_counter.TabIndex = 8;
            this.lst_counter.Click += new System.EventHandler(this.lst_counter_Click);
            this.lst_counter.SelectedIndexChanged += new System.EventHandler(this.lst_counter_SelectedIndexChanged);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btn_Exit);
            this.Pnl_Footer.Controls.Add(this.btn_Print);
            this.Pnl_Footer.Location = new System.Drawing.Point(1, 547);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 43);
            this.Pnl_Footer.TabIndex = 9;
            // 
            // frmDailySalesSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.pnl_Amount);
            this.Controls.Add(this.Pnl_counter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.grdDailySummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDailySalesSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmDailySalesSummary";
            this.Load += new System.EventHandler(this.frmDailySalesSummary_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDailySummary)).EndInit();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.pnl_Amount.ResumeLayout(false);
            this.pnl_Amount.PerformLayout();
            this.Pnl_counter.ResumeLayout(false);
            this.Pnl_counter.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.TextBox txt_counter;
        private System.Windows.Forms.TextBox txt_reportOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_report_on;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.Label lbl_SummaryBanner;
        private System.Windows.Forms.DataGridView grdDailySummary;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Retail;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Whole;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Return;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Total;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dt_to;
        private System.Windows.Forms.DateTimePicker dt_from;
        private System.Windows.Forms.Panel pnl_Amount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lst_ofAmount;
        private System.Windows.Forms.Panel Pnl_counter;
        private System.Windows.Forms.Label lbl_counter;
        private System.Windows.Forms.ListBox lst_counter;
        private System.Windows.Forms.Panel Pnl_Footer;
    }
}