namespace MSPOSBACKOFFICE
{
    partial class frmSalesSummary
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
            this.lbl_SummaryBanner = new System.Windows.Forms.Label();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_countername = new System.Windows.Forms.TextBox();
            this.txt_reporton = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_report_on = new System.Windows.Forms.Label();
            this.grdMonthSummary = new System.Windows.Forms.DataGridView();
            this.S_Retail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Whole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Return = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.S_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Print = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lst_counter = new System.Windows.Forms.ListBox();
            this.Pnl_counter = new System.Windows.Forms.Panel();
            this.lbl_counter = new System.Windows.Forms.Label();
            this.pnl_Amount = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lst_ofAmount = new System.Windows.Forms.ListBox();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btn_Exitss = new System.Windows.Forms.Button();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Back.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMonthSummary)).BeginInit();
            this.Pnl_counter.SuspendLayout();
            this.pnl_Amount.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_SummaryBanner);
            this.Pnl_Header.Location = new System.Drawing.Point(0, -1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1017, 43);
            this.Pnl_Header.TabIndex = 0;
            // 
            // lbl_SummaryBanner
            // 
            this.lbl_SummaryBanner.AutoSize = true;
            this.lbl_SummaryBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SummaryBanner.ForeColor = System.Drawing.Color.White;
            this.lbl_SummaryBanner.Location = new System.Drawing.Point(0, 8);
            this.lbl_SummaryBanner.Name = "lbl_SummaryBanner";
            this.lbl_SummaryBanner.Size = new System.Drawing.Size(225, 25);
            this.lbl_SummaryBanner.TabIndex = 0;
            this.lbl_SummaryBanner.Text = "Monthly Sales Summary";
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back.Controls.Add(this.numYear);
            this.Pnl_Back.Controls.Add(this.label4);
            this.Pnl_Back.Controls.Add(this.txt_countername);
            this.Pnl_Back.Controls.Add(this.txt_reporton);
            this.Pnl_Back.Controls.Add(this.label2);
            this.Pnl_Back.Controls.Add(this.lbl_report_on);
            this.Pnl_Back.Location = new System.Drawing.Point(0, 42);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1017, 65);
            this.Pnl_Back.TabIndex = 1;
            // 
            // numYear
            // 
            this.numYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numYear.Location = new System.Drawing.Point(53, 16);
            this.numYear.Maximum = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.numYear.Minimum = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(102, 26);
            this.numYear.TabIndex = 1;
            this.numYear.Value = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            this.numYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numYear_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Year";
            // 
            // txt_countername
            // 
            this.txt_countername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_countername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_countername.Location = new System.Drawing.Point(646, 20);
            this.txt_countername.Name = "txt_countername";
            this.txt_countername.Size = new System.Drawing.Size(295, 22);
            this.txt_countername.TabIndex = 3;
            this.txt_countername.Click += new System.EventHandler(this.txt_countername_Enter);
            this.txt_countername.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_countername_MouseClick);
            this.txt_countername.TextChanged += new System.EventHandler(this.txt_countername_TextChanged);
            this.txt_countername.Enter += new System.EventHandler(this.txt_countername_Enter);
            this.txt_countername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_countername_KeyDown);
            // 
            // txt_reporton
            // 
            this.txt_reporton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_reporton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_reporton.Location = new System.Drawing.Point(250, 19);
            this.txt_reporton.Name = "txt_reporton";
            this.txt_reporton.Size = new System.Drawing.Size(295, 22);
            this.txt_reporton.TabIndex = 2;
            this.txt_reporton.Click += new System.EventHandler(this.txt_reporton_Enter);
            this.txt_reporton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_reporton_MouseClick);
            this.txt_reporton.TextChanged += new System.EventHandler(this.txt_reporton_TextChanged);
            this.txt_reporton.Enter += new System.EventHandler(this.txt_reporton_Enter);
            this.txt_reporton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_reporton_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(574, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Counter";
            // 
            // lbl_report_on
            // 
            this.lbl_report_on.AutoSize = true;
            this.lbl_report_on.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_report_on.ForeColor = System.Drawing.Color.White;
            this.lbl_report_on.Location = new System.Drawing.Point(159, 20);
            this.lbl_report_on.Name = "lbl_report_on";
            this.lbl_report_on.Size = new System.Drawing.Size(83, 20);
            this.lbl_report_on.TabIndex = 0;
            this.lbl_report_on.Text = "Report On";
            // 
            // grdMonthSummary
            // 
            this.grdMonthSummary.BackgroundColor = System.Drawing.Color.White;
            this.grdMonthSummary.ColumnHeadersHeight = 40;
            this.grdMonthSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.S_Retail,
            this.S_Whole,
            this.S_Return,
            this.S_Total});
            this.grdMonthSummary.Location = new System.Drawing.Point(0, 111);
            this.grdMonthSummary.Name = "grdMonthSummary";
            this.grdMonthSummary.RowHeadersWidth = 300;
            this.grdMonthSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdMonthSummary.Size = new System.Drawing.Size(1018, 434);
            this.grdMonthSummary.TabIndex = 0;
            this.grdMonthSummary.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMonthSummary_CellDoubleClick);
            this.grdMonthSummary.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdMonthSummary_RowHeaderMouseDoubleClick);
            this.grdMonthSummary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdMonthSummary_KeyDown);
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
            // btn_Print
            // 
            this.btn_Print.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Print.Location = new System.Drawing.Point(1281, -250);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 35);
            this.btn_Print.TabIndex = 4;
            this.btn_Print.Text = "Print";
            this.btn_Print.UseVisualStyleBackColor = false;
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Exit.Location = new System.Drawing.Point(940, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 35);
            this.btn_Exit.TabIndex = 4;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(37, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Month";
            // 
            // lst_counter
            // 
            this.lst_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_counter.FormattingEnabled = true;
            this.lst_counter.ItemHeight = 20;
            this.lst_counter.Location = new System.Drawing.Point(7, 31);
            this.lst_counter.Name = "lst_counter";
            this.lst_counter.Size = new System.Drawing.Size(279, 184);
            this.lst_counter.TabIndex = 8;
            this.lst_counter.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Pnl_counter
            // 
            this.Pnl_counter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Pnl_counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_counter.Controls.Add(this.lbl_counter);
            this.Pnl_counter.Controls.Add(this.lst_counter);
            this.Pnl_counter.Location = new System.Drawing.Point(646, 93);
            this.Pnl_counter.Name = "Pnl_counter";
            this.Pnl_counter.Size = new System.Drawing.Size(295, 225);
            this.Pnl_counter.TabIndex = 9;
            // 
            // lbl_counter
            // 
            this.lbl_counter.AutoSize = true;
            this.lbl_counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_counter.ForeColor = System.Drawing.Color.White;
            this.lbl_counter.Location = new System.Drawing.Point(91, 6);
            this.lbl_counter.Name = "lbl_counter";
            this.lbl_counter.Size = new System.Drawing.Size(113, 20);
            this.lbl_counter.TabIndex = 9;
            this.lbl_counter.Text = "List of Counter";
            this.lbl_counter.Click += new System.EventHandler(this.label3_Click);
            // 
            // pnl_Amount
            // 
            this.pnl_Amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_Amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Amount.Controls.Add(this.label3);
            this.pnl_Amount.Controls.Add(this.lst_ofAmount);
            this.pnl_Amount.Location = new System.Drawing.Point(250, 94);
            this.pnl_Amount.Name = "pnl_Amount";
            this.pnl_Amount.Size = new System.Drawing.Size(290, 225);
            this.pnl_Amount.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(84, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select One";
            // 
            // lst_ofAmount
            // 
            this.lst_ofAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_ofAmount.FormattingEnabled = true;
            this.lst_ofAmount.ItemHeight = 20;
            this.lst_ofAmount.Items.AddRange(new object[] {
            "Gross Amount",
            "Nett Amount"});
            this.lst_ofAmount.Location = new System.Drawing.Point(8, 32);
            this.lst_ofAmount.Name = "lst_ofAmount";
            this.lst_ofAmount.Size = new System.Drawing.Size(273, 184);
            this.lst_ofAmount.TabIndex = 8;
            this.lst_ofAmount.SelectedIndexChanged += new System.EventHandler(this.lst_ofAmount_SelectedIndexChanged);
            this.lst_ofAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_ofAmount_KeyDown);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Controls.Add(this.btn_Exitss);
            this.Pnl_Footer.Controls.Add(this.btn_Exit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 546);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1017, 43);
            this.Pnl_Footer.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.Location = new System.Drawing.Point(857, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 35);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btn_Exitss
            // 
            this.btn_Exitss.BackColor = System.Drawing.Color.White;
            this.btn_Exitss.Location = new System.Drawing.Point(1183, 0);
            this.btn_Exitss.Name = "btn_Exitss";
            this.btn_Exitss.Size = new System.Drawing.Size(75, 40);
            this.btn_Exitss.TabIndex = 1;
            this.btn_Exitss.Text = "Exit";
            this.btn_Exitss.UseVisualStyleBackColor = false;
            this.btn_Exitss.Click += new System.EventHandler(this.btn_Exitss_Click);
            // 
            // frmSalesSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 592);
            this.Controls.Add(this.pnl_Amount);
            this.Controls.Add(this.Pnl_counter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.grdMonthSummary);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.Pnl_Footer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSalesSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmSalesSummary";
            this.Load += new System.EventHandler(this.frmSalesSummary_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMonthSummary)).EndInit();
            this.Pnl_counter.ResumeLayout(false);
            this.Pnl_counter.PerformLayout();
            this.pnl_Amount.ResumeLayout(false);
            this.pnl_Amount.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_SummaryBanner;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.TextBox txt_reporton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_report_on;
        private System.Windows.Forms.TextBox txt_countername;
        private System.Windows.Forms.DataGridView grdMonthSummary;
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Retail;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Whole;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Return;
        private System.Windows.Forms.DataGridViewTextBoxColumn S_Total;
        private System.Windows.Forms.ListBox lst_counter;
        private System.Windows.Forms.Panel Pnl_counter;
        private System.Windows.Forms.Label lbl_counter;
        private System.Windows.Forms.Panel pnl_Amount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lst_ofAmount;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btn_Exitss;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPrint;
    }
}