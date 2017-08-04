namespace MSPOSBACKOFFICE
{
    partial class Promotionalteration
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtItem_code = new System.Windows.Forms.TextBox();
            this.txt_itemname = new System.Windows.Forms.TextBox();
            this.listitems = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.txtTypes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgNdGroup = new DataGridNameSpace.MyDataGrid();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtToDate = new System.Windows.Forms.TextBox();
            this.txtFromDate = new System.Windows.Forms.TextBox();
            this.DtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.DtpToDate = new System.Windows.Forms.DateTimePicker();
            this.panel2.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNdGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Offer Item Alteration";
            // 
            // txtItem_code
            // 
            this.txtItem_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItem_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem_code.Location = new System.Drawing.Point(1003, 59);
            this.txtItem_code.Name = "txtItem_code";
            this.txtItem_code.Size = new System.Drawing.Size(10, 29);
            this.txtItem_code.TabIndex = 1;
            this.txtItem_code.Visible = false;
            this.txtItem_code.TextChanged += new System.EventHandler(this.txtItem_code_TextChanged);
            this.txtItem_code.Enter += new System.EventHandler(this.txtItem_code_Enter);
            this.txtItem_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_code_KeyDown);
            // 
            // txt_itemname
            // 
            this.txt_itemname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_itemname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_itemname.Location = new System.Drawing.Point(1003, 51);
            this.txt_itemname.Name = "txt_itemname";
            this.txt_itemname.Size = new System.Drawing.Size(10, 26);
            this.txt_itemname.TabIndex = 3;
            this.txt_itemname.Visible = false;
            this.txt_itemname.TextChanged += new System.EventHandler(this.txt_itemname_TextChanged);
            this.txt_itemname.Enter += new System.EventHandler(this.txt_itemname_Enter);
            this.txt_itemname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            // 
            // listitems
            // 
            this.listitems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listitems.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.listitems.FormattingEnabled = true;
            this.listitems.ItemHeight = 20;
            this.listitems.Location = new System.Drawing.Point(9, 37);
            this.listitems.Name = "listitems";
            this.listitems.Size = new System.Drawing.Size(580, 262);
            this.listitems.TabIndex = 3;
            this.listitems.Click += new System.EventHandler(this.listitems_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.listitems);
            this.panel2.Location = new System.Drawing.Point(867, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 38);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(231, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "List Of items";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(930, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(893, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Code";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(893, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Item Name";
            this.label4.Visible = false;
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Location = new System.Drawing.Point(-1, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 52);
            this.Pnl_Header.TabIndex = 0;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.button1);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 540);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 52);
            this.Pnl_Footer.TabIndex = 2;
            // 
            // txtTypes
            // 
            this.txtTypes.BackColor = System.Drawing.Color.White;
            this.txtTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTypes.Location = new System.Drawing.Point(77, 98);
            this.txtTypes.Name = "txtTypes";
            this.txtTypes.ReadOnly = true;
            this.txtTypes.Size = new System.Drawing.Size(307, 26);
            this.txtTypes.TabIndex = 38;
            this.txtTypes.Text = "Normal";
            this.txtTypes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTypes_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 39;
            this.label5.Text = "Style";
            // 
            // dgNdGroup
            // 
            this.dgNdGroup.ColumnHeadersHeight = 35;
            this.dgNdGroup.Location = new System.Drawing.Point(2, 147);
            this.dgNdGroup.Name = "dgNdGroup";
            this.dgNdGroup.RowHeadersVisible = false;
            this.dgNdGroup.Size = new System.Drawing.Size(1013, 385);
            this.dgNdGroup.TabIndex = 78;
            this.dgNdGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.myDataGrid1_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(442, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 80;
            this.label6.Text = "From Date";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(746, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 16);
            this.label7.TabIndex = 82;
            this.label7.Text = "To Date";
            this.label7.Visible = false;
            // 
            // txtToDate
            // 
            this.txtToDate.BackColor = System.Drawing.Color.White;
            this.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtToDate.Location = new System.Drawing.Point(821, 105);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.ReadOnly = true;
            this.txtToDate.Size = new System.Drawing.Size(164, 23);
            this.txtToDate.TabIndex = 81;
            this.txtToDate.Text = "01/01/2013";
            this.txtToDate.Visible = false;
            // 
            // txtFromDate
            // 
            this.txtFromDate.BackColor = System.Drawing.Color.White;
            this.txtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFromDate.Location = new System.Drawing.Point(532, 102);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.ReadOnly = true;
            this.txtFromDate.Size = new System.Drawing.Size(164, 23);
            this.txtFromDate.TabIndex = 79;
            this.txtFromDate.Text = "01/01/2013";
            this.txtFromDate.Visible = false;
            // 
            // DtpFromDate
            // 
            this.DtpFromDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DtpFromDate.CalendarForeColor = System.Drawing.Color.White;
            this.DtpFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DtpFromDate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DtpFromDate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.DtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.DtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFromDate.Location = new System.Drawing.Point(533, 103);
            this.DtpFromDate.Name = "DtpFromDate";
            this.DtpFromDate.Size = new System.Drawing.Size(162, 20);
            this.DtpFromDate.TabIndex = 94;
            this.DtpFromDate.Visible = false;
            this.DtpFromDate.ValueChanged += new System.EventHandler(this.DtpFromDate_ValueChanged);
            // 
            // DtpToDate
            // 
            this.DtpToDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DtpToDate.CalendarForeColor = System.Drawing.Color.White;
            this.DtpToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DtpToDate.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DtpToDate.CalendarTrailingForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.DtpToDate.CustomFormat = "dd/MM/yyyy";
            this.DtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpToDate.Location = new System.Drawing.Point(822, 106);
            this.DtpToDate.Name = "DtpToDate";
            this.DtpToDate.Size = new System.Drawing.Size(162, 20);
            this.DtpToDate.TabIndex = 95;
            this.DtpToDate.Visible = false;
            this.DtpToDate.ValueChanged += new System.EventHandler(this.DtpToDate_ValueChanged);
            // 
            // Promotionalteration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.DtpToDate);
            this.Controls.Add(this.DtpFromDate);
            this.Controls.Add(this.txtToDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFromDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgNdGroup);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTypes);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_itemname);
            this.Controls.Add(this.txtItem_code);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Promotionalteration";
            this.Text = "Promotionalteration";
            this.Load += new System.EventHandler(this.Itemalteration_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNdGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox txtItem_code;
        private System.Windows.Forms.TextBox txt_itemname;
        private System.Windows.Forms.ListBox listitems;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.TextBox txtTypes;
        private System.Windows.Forms.Label label5;
        private DataGridNameSpace.MyDataGrid dgNdGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtToDate;
        private System.Windows.Forms.TextBox txtFromDate;
        private System.Windows.Forms.DateTimePicker DtpFromDate;
        private System.Windows.Forms.DateTimePicker DtpToDate;
    }
}