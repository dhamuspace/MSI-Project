namespace MSPOSBACKOFFICE
{
    partial class frmLedgerReport
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
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.lblTotalAmt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAvbCredit = new System.Windows.Forms.Label();
            this.lblAvlCrdAmt = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_PRINT = new System.Windows.Forms.Button();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.txtfromdate = new System.Windows.Forms.DateTimePicker();
            this.txttodate = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLedgerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCustomers = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.lstLedgerName = new System.Windows.Forms.ListBox();
            this.DtLedger = new System.Windows.Forms.DataGridView();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.Pnl_Back.SuspendLayout();
            this.pnlCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtLedger)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label16);
            this.Pnl_Header.Controls.Add(this.panel2);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 44);
            this.Pnl_Header.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(1, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 25);
            this.label16.TabIndex = 26;
            this.label16.Text = "Ledger Report";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1130, 48);
            this.panel2.TabIndex = 1;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.lblTotalAmt);
            this.Pnl_Footer.Controls.Add(this.label5);
            this.Pnl_Footer.Controls.Add(this.lblAvbCredit);
            this.Pnl_Footer.Controls.Add(this.lblAvlCrdAmt);
            this.Pnl_Footer.Controls.Add(this.btn_close);
            this.Pnl_Footer.Controls.Add(this.btn_PRINT);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 543);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 49);
            this.Pnl_Footer.TabIndex = 44;
            // 
            // lblTotalAmt
            // 
            this.lblTotalAmt.AutoSize = true;
            this.lblTotalAmt.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmt.ForeColor = System.Drawing.Color.White;
            this.lblTotalAmt.Location = new System.Drawing.Point(194, 8);
            this.lblTotalAmt.Name = "lblTotalAmt";
            this.lblTotalAmt.Size = new System.Drawing.Size(63, 31);
            this.lblTotalAmt.TabIndex = 8;
            this.lblTotalAmt.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(38, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Total Amount";
            // 
            // lblAvbCredit
            // 
            this.lblAvbCredit.AutoSize = true;
            this.lblAvbCredit.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvbCredit.ForeColor = System.Drawing.Color.White;
            this.lblAvbCredit.Location = new System.Drawing.Point(771, 8);
            this.lblAvbCredit.Name = "lblAvbCredit";
            this.lblAvbCredit.Size = new System.Drawing.Size(63, 31);
            this.lblAvbCredit.TabIndex = 6;
            this.lblAvbCredit.Text = "0.00";
            // 
            // lblAvlCrdAmt
            // 
            this.lblAvlCrdAmt.AutoSize = true;
            this.lblAvlCrdAmt.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvlCrdAmt.ForeColor = System.Drawing.Color.White;
            this.lblAvlCrdAmt.Location = new System.Drawing.Point(523, 12);
            this.lblAvlCrdAmt.Name = "lblAvlCrdAmt";
            this.lblAvlCrdAmt.Size = new System.Drawing.Size(229, 24);
            this.lblAvlCrdAmt.TabIndex = 5;
            this.lblAvlCrdAmt.Text = "Available Credit Amount";
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.White;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(937, 5);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 38);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "EXIT";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_PRINT
            // 
            this.btn_PRINT.BackColor = System.Drawing.Color.White;
            this.btn_PRINT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PRINT.Location = new System.Drawing.Point(859, 5);
            this.btn_PRINT.Name = "btn_PRINT";
            this.btn_PRINT.Size = new System.Drawing.Size(78, 38);
            this.btn_PRINT.TabIndex = 4;
            this.btn_PRINT.Text = "PRINT";
            this.btn_PRINT.UseVisualStyleBackColor = false;
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Back.Controls.Add(this.txtfromdate);
            this.Pnl_Back.Controls.Add(this.txttodate);
            this.Pnl_Back.Controls.Add(this.label13);
            this.Pnl_Back.Controls.Add(this.txtLedgerName);
            this.Pnl_Back.Controls.Add(this.label3);
            this.Pnl_Back.Controls.Add(this.label1);
            this.Pnl_Back.Location = new System.Drawing.Point(2, 48);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1017, 69);
            this.Pnl_Back.TabIndex = 45;
            // 
            // txtfromdate
            // 
            this.txtfromdate.CalendarForeColor = System.Drawing.Color.White;
            this.txtfromdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtfromdate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtfromdate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtfromdate.CustomFormat = "dd/MM/yyyy";
            this.txtfromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtfromdate.Location = new System.Drawing.Point(65, 9);
            this.txtfromdate.Name = "txtfromdate";
            this.txtfromdate.Size = new System.Drawing.Size(184, 22);
            this.txtfromdate.TabIndex = 1;
            this.txtfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfromdate_KeyDown);
            // 
            // txttodate
            // 
            this.txttodate.CalendarForeColor = System.Drawing.Color.White;
            this.txttodate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txttodate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txttodate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.txttodate.CustomFormat = "dd/MM/yyyy";
            this.txttodate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txttodate.Location = new System.Drawing.Point(355, 9);
            this.txttodate.Name = "txttodate";
            this.txttodate.Size = new System.Drawing.Size(166, 22);
            this.txttodate.TabIndex = 2;
            this.txttodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttodate_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(557, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Party";
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLedgerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLedgerName.Location = new System.Drawing.Point(619, 12);
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(388, 22);
            this.txtLedgerName.TabIndex = 3;
            this.txtLedgerName.TextChanged += new System.EventHandler(this.txtLedgerName_TextChanged);
            this.txtLedgerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(315, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "To ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "From";
            // 
            // pnlCustomers
            // 
            this.pnlCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlCustomers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomers.Controls.Add(this.label18);
            this.pnlCustomers.Controls.Add(this.lstLedgerName);
            this.pnlCustomers.Location = new System.Drawing.Point(618, 96);
            this.pnlCustomers.Name = "pnlCustomers";
            this.pnlCustomers.Size = new System.Drawing.Size(394, 284);
            this.pnlCustomers.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(97, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(200, 25);
            this.label18.TabIndex = 0;
            this.label18.Text = "List Of Customers";
            // 
            // lstLedgerName
            // 
            this.lstLedgerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLedgerName.FormattingEnabled = true;
            this.lstLedgerName.ItemHeight = 20;
            this.lstLedgerName.Location = new System.Drawing.Point(5, 33);
            this.lstLedgerName.Name = "lstLedgerName";
            this.lstLedgerName.Size = new System.Drawing.Size(384, 244);
            this.lstLedgerName.TabIndex = 33;
            this.lstLedgerName.Click += new System.EventHandler(this.lstLedgerName_Click);
            // 
            // DtLedger
            // 
            this.DtLedger.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DtLedger.ColumnHeadersHeight = 50;
            this.DtLedger.Location = new System.Drawing.Point(5, 117);
            this.DtLedger.Name = "DtLedger";
            this.DtLedger.Size = new System.Drawing.Size(1007, 257);
            this.DtLedger.TabIndex = 47;
            // 
            // frmLedgerReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.pnlCustomers);
            this.Controls.Add(this.DtLedger);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmLedgerReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListOfPurchase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLedgerReport_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.pnlCustomers.ResumeLayout(false);
            this.pnlCustomers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtLedger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_PRINT;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.DateTimePicker txtfromdate;
        private System.Windows.Forms.DateTimePicker txttodate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLedgerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCustomers;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox lstLedgerName;
        private System.Windows.Forms.DataGridView DtLedger;
        private System.Windows.Forms.Label lblAvbCredit;
        private System.Windows.Forms.Label lblAvlCrdAmt;
        private System.Windows.Forms.Label lblTotalAmt;
        private System.Windows.Forms.Label label5;
    }
}