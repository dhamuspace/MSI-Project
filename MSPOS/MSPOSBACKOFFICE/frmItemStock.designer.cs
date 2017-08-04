namespace MSPOSBACKOFFICE
{
    partial class frmItemStock
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
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lbltot = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbltotcount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DgStockReport = new System.Windows.Forms.DataGridView();
            this.Item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_opnqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_purqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_salqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_cloqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtpTodate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpFromdate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnNotepad = new System.Windows.Forms.Button();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgStockReport)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(1, 3);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1017, 45);
            this.Pnl_Header.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Stock Report";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnNotepad);
            this.Pnl_Footer.Controls.Add(this.btnExcel);
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Controls.Add(this.btnFilter);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.lbltot);
            this.Pnl_Footer.Controls.Add(this.lblqty);
            this.Pnl_Footer.Controls.Add(this.label5);
            this.Pnl_Footer.Controls.Add(this.lbltotcount);
            this.Pnl_Footer.Controls.Add(this.label4);
            this.Pnl_Footer.Location = new System.Drawing.Point(3, 505);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1015, 46);
            this.Pnl_Footer.TabIndex = 2;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(834, 4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(86, 35);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Fi&lter";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(921, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 35);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbltot
            // 
            this.lbltot.AutoSize = true;
            this.lbltot.BackColor = System.Drawing.Color.Olive;
            this.lbltot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot.ForeColor = System.Drawing.Color.White;
            this.lbltot.Location = new System.Drawing.Point(758, 16);
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
            this.lblqty.Location = new System.Drawing.Point(622, 16);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(16, 16);
            this.lblqty.TabIndex = 3;
            this.lblqty.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Olive;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(490, 14);
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
            this.lbltotcount.Location = new System.Drawing.Point(364, 14);
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
            this.label4.Location = new System.Drawing.Point(255, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "No Of items";
            // 
            // DgStockReport
            // 
            this.DgStockReport.ColumnHeadersHeight = 40;
            this.DgStockReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item_code,
            this.Item_name,
            this.nt_opnqty,
            this.nt_purqty,
            this.nt_salqty,
            this.nt_cloqty,
            this.Rate,
            this.Value});
            this.DgStockReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgStockReport.Location = new System.Drawing.Point(1, 105);
            this.DgStockReport.Name = "DgStockReport";
            this.DgStockReport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DgStockReport.RowHeadersVisible = false;
            this.DgStockReport.Size = new System.Drawing.Size(1017, 393);
            this.DgStockReport.TabIndex = 9;
            // 
            // Item_code
            // 
            this.Item_code.DataPropertyName = "Item_code";
            this.Item_code.HeaderText = "Code";
            this.Item_code.Name = "Item_code";
            this.Item_code.Width = 175;
            // 
            // Item_name
            // 
            this.Item_name.DataPropertyName = "Item_name";
            this.Item_name.HeaderText = "Name";
            this.Item_name.Name = "Item_name";
            this.Item_name.Width = 320;
            // 
            // nt_opnqty
            // 
            this.nt_opnqty.DataPropertyName = "nt_opnqty";
            this.nt_opnqty.HeaderText = "OpenQty";
            this.nt_opnqty.Name = "nt_opnqty";
            this.nt_opnqty.Width = 85;
            // 
            // nt_purqty
            // 
            this.nt_purqty.DataPropertyName = "nt_purqty";
            this.nt_purqty.HeaderText = "PurchaseQty";
            this.nt_purqty.Name = "nt_purqty";
            this.nt_purqty.Width = 85;
            // 
            // nt_salqty
            // 
            this.nt_salqty.DataPropertyName = "nt_salqty";
            this.nt_salqty.HeaderText = "SalesQty";
            this.nt_salqty.Name = "nt_salqty";
            this.nt_salqty.Width = 85;
            // 
            // nt_cloqty
            // 
            this.nt_cloqty.DataPropertyName = "nt_cloqty";
            this.nt_cloqty.HeaderText = "CloseQty";
            this.nt_cloqty.Name = "nt_cloqty";
            this.nt_cloqty.Width = 85;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Width = 85;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 85;
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
            this.DtpTodate.Location = new System.Drawing.Point(285, 63);
            this.DtpTodate.Name = "DtpTodate";
            this.DtpTodate.Size = new System.Drawing.Size(112, 23);
            this.DtpTodate.TabIndex = 13;
            this.DtpTodate.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(36, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "&From";
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
            this.DtpFromdate.Location = new System.Drawing.Point(102, 64);
            this.DtpFromdate.Name = "DtpFromdate";
            this.DtpFromdate.Size = new System.Drawing.Size(115, 23);
            this.DtpFromdate.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(235, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "&To";
            this.label3.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(2, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 36);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.White;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExcel.Location = new System.Drawing.Point(92, 5);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(68, 36);
            this.btnExcel.TabIndex = 11;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnNotepad
            // 
            this.btnNotepad.BackColor = System.Drawing.Color.White;
            this.btnNotepad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnNotepad.ForeColor = System.Drawing.Color.Black;
            this.btnNotepad.Location = new System.Drawing.Point(164, 6);
            this.btnNotepad.Name = "btnNotepad";
            this.btnNotepad.Size = new System.Drawing.Size(75, 36);
            this.btnNotepad.TabIndex = 12;
            this.btnNotepad.Text = "Notepad";
            this.btnNotepad.UseVisualStyleBackColor = false;
            this.btnNotepad.Click += new System.EventHandler(this.btnNotepad_Click);
            // 
            // frmItemStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1019, 553);
            this.Controls.Add(this.DtpTodate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DtpFromdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DgStockReport);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmItemStock";
            this.Text = "frmItemStock";
            this.Load += new System.EventHandler(this.frmItemStock_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgStockReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lbltot;
        private System.Windows.Forms.Label lblqty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbltotcount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DgStockReport;
        private System.Windows.Forms.DateTimePicker DtpTodate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpFromdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_opnqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_purqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_salqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_cloqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Button btnNotepad;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPrint;
    }
}