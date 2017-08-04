namespace SalesProject
{
    partial class StocReport
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DgStockReport = new System.Windows.Forms.DataGridView();
            this.Item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_cloqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lbltot = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbltotcount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpTodate = new System.Windows.Forms.DateTimePicker();
            this.DtpFromdate = new System.Windows.Forms.DateTimePicker();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DgStockReport)).BeginInit();
            this.Pnl_Footer.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stock Report";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "&From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(220, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "&To";
            this.label3.Visible = false;
            // 
            // DgStockReport
            // 
            this.DgStockReport.ColumnHeadersHeight = 40;
            this.DgStockReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item_code,
            this.Item_name,
            this.nt_cloqty,
            this.item_cost,
            this.tot});
            this.DgStockReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgStockReport.Location = new System.Drawing.Point(-1, 96);
            this.DgStockReport.Name = "DgStockReport";
            this.DgStockReport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DgStockReport.RowHeadersVisible = false;
            this.DgStockReport.Size = new System.Drawing.Size(1017, 443);
            this.DgStockReport.TabIndex = 8;
            this.DgStockReport.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.DgStockReport.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.DgStockReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgStockReport_KeyDown);
            this.DgStockReport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DgStockReport_KeyPress);
            // 
            // Item_code
            // 
            this.Item_code.DataPropertyName = "Item_code";
            this.Item_code.HeaderText = "Code";
            this.Item_code.Name = "Item_code";
            this.Item_code.Width = 150;
            // 
            // Item_name
            // 
            this.Item_name.DataPropertyName = "Item_name";
            this.Item_name.HeaderText = "Name";
            this.Item_name.Name = "Item_name";
            this.Item_name.Width = 380;
            // 
            // nt_cloqty
            // 
            this.nt_cloqty.DataPropertyName = "nt_cloqty";
            this.nt_cloqty.HeaderText = "Qty";
            this.nt_cloqty.Name = "nt_cloqty";
            this.nt_cloqty.Width = 150;
            // 
            // item_cost
            // 
            this.item_cost.DataPropertyName = "item_cost";
            this.item_cost.HeaderText = "Rate";
            this.item_cost.Name = "item_cost";
            this.item_cost.Width = 150;
            // 
            // tot
            // 
            this.tot.DataPropertyName = "tot";
            this.tot.HeaderText = "Value";
            this.tot.Name = "tot";
            this.tot.Width = 150;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(902, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 35);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(816, 4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(86, 35);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Fi&lter";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Controls.Add(this.btnFilter);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.lbltot);
            this.Pnl_Footer.Controls.Add(this.lblqty);
            this.Pnl_Footer.Controls.Add(this.label5);
            this.Pnl_Footer.Controls.Add(this.lbltotcount);
            this.Pnl_Footer.Controls.Add(this.label4);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 545);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 46);
            this.Pnl_Footer.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(17, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 36);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lbltot
            // 
            this.lbltot.AutoSize = true;
            this.lbltot.BackColor = System.Drawing.Color.Olive;
            this.lbltot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot.ForeColor = System.Drawing.Color.White;
            this.lbltot.Location = new System.Drawing.Point(661, 16);
            this.lbltot.Name = "lbltot";
            this.lbltot.Size = new System.Drawing.Size(16, 16);
            this.lbltot.TabIndex = 4;
            this.lbltot.Text = "0";
            // 
            // lblqty
            // 
            this.lblqty.AutoSize = true;
            this.lblqty.BackColor = System.Drawing.Color.Olive;
            this.lblqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblqty.ForeColor = System.Drawing.Color.White;
            this.lblqty.Location = new System.Drawing.Point(525, 16);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(16, 16);
            this.lblqty.TabIndex = 3;
            this.lblqty.Text = "0";
            this.lblqty.Click += new System.EventHandler(this.lblqty_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Olive;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(360, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total";
            // 
            // lbltotcount
            // 
            this.lbltotcount.AutoSize = true;
            this.lbltotcount.BackColor = System.Drawing.Color.Olive;
            this.lbltotcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotcount.ForeColor = System.Drawing.Color.White;
            this.lbltotcount.Location = new System.Drawing.Point(234, 14);
            this.lbltotcount.Name = "lbltotcount";
            this.lbltotcount.Size = new System.Drawing.Size(16, 16);
            this.lbltotcount.TabIndex = 1;
            this.lbltotcount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Olive;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(125, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "No Of items";
            // 
            // DtpTodate
            // 
            this.DtpTodate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DtpTodate.CalendarForeColor = System.Drawing.Color.White;
            this.DtpTodate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DtpTodate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DtpTodate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.DtpTodate.CustomFormat = "dd/MM/yyyy";
            this.DtpTodate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTodate.Location = new System.Drawing.Point(270, 61);
            this.DtpTodate.Name = "DtpTodate";
            this.DtpTodate.Size = new System.Drawing.Size(112, 23);
            this.DtpTodate.TabIndex = 7;
            this.DtpTodate.Visible = false;
            this.DtpTodate.CloseUp += new System.EventHandler(this.DtpTodate_CloseUp);
            this.DtpTodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpTodate_KeyDown);
            // 
            // DtpFromdate
            // 
            this.DtpFromdate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DtpFromdate.CalendarForeColor = System.Drawing.Color.White;
            this.DtpFromdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DtpFromdate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DtpFromdate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.DtpFromdate.CustomFormat = "dd/MM/yyyy";
            this.DtpFromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFromdate.Location = new System.Drawing.Point(87, 62);
            this.DtpFromdate.Name = "DtpFromdate";
            this.DtpFromdate.Size = new System.Drawing.Size(115, 23);
            this.DtpFromdate.TabIndex = 6;
            this.DtpFromdate.CloseUp += new System.EventHandler(this.DtpTodate_CloseUp);
            this.DtpFromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpTodate_KeyDown);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(1, 1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 45);
            this.Pnl_Header.TabIndex = 0;
            // 
            // StocReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.DtpTodate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.DtpFromdate);
            this.Controls.Add(this.DgStockReport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StocReport";
            this.Text = "StocReport";
            this.Load += new System.EventHandler(this.StocReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgStockReport)).EndInit();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DgStockReport;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbltotcount;
        private System.Windows.Forms.DateTimePicker DtpTodate;
        private System.Windows.Forms.DateTimePicker DtpFromdate;
        private System.Windows.Forms.Label lbltot;
        private System.Windows.Forms.Label lblqty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_cloqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn tot;
        private System.Windows.Forms.Button btnPrint;
    }
}