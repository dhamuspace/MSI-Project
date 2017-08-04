namespace MSPOSBACKOFFICE
{
    partial class SalesBomIssueDisplay
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
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.pnlcanceltype = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lstcanceltype = new System.Windows.Forms.ListBox();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtcancel = new System.Windows.Forms.TextBox();
            this.dtpTodate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromdate = new System.Windows.Forms.DateTimePicker();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnprint = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgIssueDisplay = new MSPOSBACKOFFICE.MyDataGridNew();
            this.Pnl_Back.SuspendLayout();
            this.pnlcanceltype.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIssueDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.Controls.Add(this.pnlcanceltype);
            this.Pnl_Back.Controls.Add(this.Pnl_Header);
            this.Pnl_Back.Controls.Add(this.Pnl_Footer);
            this.Pnl_Back.Controls.Add(this.dgIssueDisplay);
            this.Pnl_Back.Controls.Add(this.label1);
            this.Pnl_Back.Location = new System.Drawing.Point(1, 1);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1019, 725);
            this.Pnl_Back.TabIndex = 0;
            // 
            // pnlcanceltype
            // 
            this.pnlcanceltype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlcanceltype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlcanceltype.Controls.Add(this.label6);
            this.pnlcanceltype.Controls.Add(this.lstcanceltype);
            this.pnlcanceltype.Location = new System.Drawing.Point(546, 86);
            this.pnlcanceltype.Name = "pnlcanceltype";
            this.pnlcanceltype.Size = new System.Drawing.Size(264, 142);
            this.pnlcanceltype.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label6.Location = new System.Drawing.Point(71, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "Select One";
            // 
            // lstcanceltype
            // 
            this.lstcanceltype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstcanceltype.FormattingEnabled = true;
            this.lstcanceltype.ItemHeight = 16;
            this.lstcanceltype.Items.AddRange(new object[] {
            "ALL",
            "Not Cancel"});
            this.lstcanceltype.Location = new System.Drawing.Point(4, 35);
            this.lstcanceltype.Name = "lstcanceltype";
            this.lstcanceltype.Size = new System.Drawing.Size(254, 98);
            this.lstcanceltype.TabIndex = 0;
            this.lstcanceltype.Click += new System.EventHandler(this.lstcanceltype_Click);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label5);
            this.Pnl_Header.Controls.Add(this.label4);
            this.Pnl_Header.Controls.Add(this.label3);
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Controls.Add(this.txtType);
            this.Pnl_Header.Controls.Add(this.txtcancel);
            this.Pnl_Header.Controls.Add(this.dtpTodate);
            this.Pnl_Header.Controls.Add(this.dtpFromdate);
            this.Pnl_Header.Location = new System.Drawing.Point(-1, 37);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 50);
            this.Pnl_Header.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(514, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cancel        :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(752, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Type      :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(7, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "From :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(268, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "To :";
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.White;
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtType.Location = new System.Drawing.Point(827, 12);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(183, 26);
            this.txtType.TabIndex = 3;
            this.txtType.Text = "Normal";
            this.txtType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtType_KeyDown);
            // 
            // txtcancel
            // 
            this.txtcancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtcancel.Location = new System.Drawing.Point(615, 12);
            this.txtcancel.Name = "txtcancel";
            this.txtcancel.Size = new System.Drawing.Size(127, 26);
            this.txtcancel.TabIndex = 2;
            this.txtcancel.Text = "ALL";
            this.txtcancel.Enter += new System.EventHandler(this.txtcancel_Enter);
            this.txtcancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcancel_KeyDown);
            // 
            // dtpTodate
            // 
            this.dtpTodate.CalendarForeColor = System.Drawing.Color.White;
            this.dtpTodate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpTodate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dtpTodate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.dtpTodate.CustomFormat = "dd/MM/yyyy";
            this.dtpTodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTodate.Location = new System.Drawing.Point(306, 13);
            this.dtpTodate.Name = "dtpTodate";
            this.dtpTodate.Size = new System.Drawing.Size(200, 23);
            this.dtpTodate.TabIndex = 1;
            this.dtpTodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpTodate_KeyDown);
            // 
            // dtpFromdate
            // 
            this.dtpFromdate.CalendarForeColor = System.Drawing.Color.White;
            this.dtpFromdate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpFromdate.CalendarTitleBackColor = System.Drawing.Color.White;
            this.dtpFromdate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dtpFromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromdate.Location = new System.Drawing.Point(65, 14);
            this.dtpFromdate.Name = "dtpFromdate";
            this.dtpFromdate.Size = new System.Drawing.Size(200, 23);
            this.dtpFromdate.TabIndex = 0;
            this.dtpFromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromdate_KeyDown);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnprint);
            this.Pnl_Footer.Controls.Add(this.BtnAdd);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 540);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 51);
            this.Pnl_Footer.TabIndex = 2;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.White;
            this.btnprint.ForeColor = System.Drawing.Color.Black;
            this.btnprint.Location = new System.Drawing.Point(2, 3);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(82, 43);
            this.btnprint.TabIndex = 10;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.White;
            this.BtnAdd.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Location = new System.Drawing.Point(853, 3);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(82, 43);
            this.BtnAdd.TabIndex = 7;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(934, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(79, 43);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(1, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "BOM Issue Display";
            // 
            // dgIssueDisplay
            // 
            this.dgIssueDisplay.ColumnHeadersHeight = 30;
            this.dgIssueDisplay.Location = new System.Drawing.Point(0, 86);
            this.dgIssueDisplay.Name = "dgIssueDisplay";
            this.dgIssueDisplay.RowHeadersVisible = false;
            this.dgIssueDisplay.Size = new System.Drawing.Size(1015, 440);
            this.dgIssueDisplay.TabIndex = 3;
            this.dgIssueDisplay.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgIssueDisplay_CellDoubleClick);
            // 
            // SalesBomIssueDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Back);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SalesBomIssueDisplay";
            this.Text = "SalesBomIssueDisplay";
            this.Load += new System.EventHandler(this.SalesBomIssueDisplay_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.pnlcanceltype.ResumeLayout(false);
            this.pnlcanceltype.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIssueDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Back;
        private MyDataGridNew dgIssueDisplay;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtcancel;
        private System.Windows.Forms.DateTimePicker dtpTodate;
        private System.Windows.Forms.DateTimePicker dtpFromdate;
        private System.Windows.Forms.Panel pnlcanceltype;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstcanceltype;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button btnprint;
    }
}