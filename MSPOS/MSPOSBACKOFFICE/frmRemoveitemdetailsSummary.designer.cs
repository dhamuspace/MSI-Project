namespace MSPOSBACKOFFICE
{
    partial class frmRemoveitemdetailsSummary
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
            this.Detailsgrid = new System.Windows.Forms.DataGridView();
            this.txtitemname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.From_date = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.To_date = new System.Windows.Forms.DateTimePicker();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.pnlUserName = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.ItemName = new System.Windows.Forms.ListBox();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnprint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Detailsgrid)).BeginInit();
            this.pnlUserName.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(-3, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1020, 43);
            this.Pnl_Header.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remove Item Details Summary";
            // 
            // Detailsgrid
            // 
            this.Detailsgrid.AllowUserToResizeColumns = false;
            this.Detailsgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Detailsgrid.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.Detailsgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Detailsgrid.Location = new System.Drawing.Point(18, 119);
            this.Detailsgrid.Name = "Detailsgrid";
            this.Detailsgrid.RowHeadersVisible = false;
            this.Detailsgrid.Size = new System.Drawing.Size(988, 409);
            this.Detailsgrid.TabIndex = 2;
            // 
            // txtitemname
            // 
            this.txtitemname.Location = new System.Drawing.Point(735, 79);
            this.txtitemname.Name = "txtitemname";
            this.txtitemname.Size = new System.Drawing.Size(268, 20);
            this.txtitemname.TabIndex = 7;
            this.txtitemname.Click += new System.EventHandler(this.txtitemname_Click);
            this.txtitemname.TextChanged += new System.EventHandler(this.txtitemname_TextChanged);
            this.txtitemname.Enter += new System.EventHandler(this.txtitemname_Enter);
            this.txtitemname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtitemname_KeyDown);
            this.txtitemname.Leave += new System.EventHandler(this.txtitemname_Leave);
            this.txtitemname.MouseLeave += new System.EventHandler(this.txtitemname_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(627, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Item Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(28, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "From";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // From_date
            // 
            this.From_date.CalendarTitleBackColor = System.Drawing.Color.Purple;
            this.From_date.CustomFormat = "dd/MM/yyyy";
            this.From_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.From_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.From_date.Location = new System.Drawing.Point(103, 81);
            this.From_date.Name = "From_date";
            this.From_date.Size = new System.Drawing.Size(100, 20);
            this.From_date.TabIndex = 1;
            this.From_date.KeyDown += new System.Windows.Forms.KeyEventHandler(this.From_date_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(229, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "TO";
            // 
            // To_date
            // 
            this.To_date.CustomFormat = "dd/MM/yyyy";
            this.To_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.To_date.Location = new System.Drawing.Point(292, 80);
            this.To_date.Name = "To_date";
            this.To_date.Size = new System.Drawing.Size(101, 20);
            this.To_date.TabIndex = 2;
            this.To_date.CloseUp += new System.EventHandler(this.To_date_CloseUp);
            this.To_date.ValueChanged += new System.EventHandler(this.To_date_ValueChanged);
            this.To_date.Enter += new System.EventHandler(this.To_date_Enter);
            this.To_date.KeyDown += new System.Windows.Forms.KeyEventHandler(this.To_date_KeyDown);
            this.To_date.Leave += new System.EventHandler(this.To_date_Leave);
            this.To_date.MouseEnter += new System.EventHandler(this.To_date_MouseEnter);
            this.To_date.MouseLeave += new System.EventHandler(this.To_date_MouseLeave);
            this.To_date.MouseMove += new System.Windows.Forms.MouseEventHandler(this.To_date_MouseMove);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Exit.Location = new System.Drawing.Point(930, 2);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 38);
            this.btn_Exit.TabIndex = 15;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // pnlUserName
            // 
            this.pnlUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserName.Controls.Add(this.label6);
            this.pnlUserName.Controls.Add(this.ItemName);
            this.pnlUserName.Location = new System.Drawing.Point(735, 104);
            this.pnlUserName.Name = "pnlUserName";
            this.pnlUserName.Size = new System.Drawing.Size(271, 225);
            this.pnlUserName.TabIndex = 38;
            this.pnlUserName.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label6.Location = new System.Drawing.Point(75, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 24);
            this.label6.TabIndex = 36;
            this.label6.Text = "Item Name";
            // 
            // ItemName
            // 
            this.ItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ItemName.FormattingEnabled = true;
            this.ItemName.ItemHeight = 16;
            this.ItemName.Location = new System.Drawing.Point(7, 35);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(254, 178);
            this.ItemName.TabIndex = 35;
            this.ItemName.Click += new System.EventHandler(this.ItemName_Click);
            this.ItemName.SelectedIndexChanged += new System.EventHandler(this.ItemName_SelectedIndexChanged);
            this.ItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ItemName_KeyPress);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnprint);
            this.Pnl_Footer.Controls.Add(this.label5);
            this.Pnl_Footer.Controls.Add(this.btn_Exit);
            this.Pnl_Footer.Location = new System.Drawing.Point(-3, 548);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1020, 43);
            this.Pnl_Footer.TabIndex = 39;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.Cornsilk;
            this.btnprint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.Location = new System.Drawing.Point(839, 3);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(75, 35);
            this.btnprint.TabIndex = 16;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(29, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 25);
            this.label5.TabIndex = 0;
            // 
            // frmRemoveitemdetailsSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1018, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.pnlUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.From_date);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.To_date);
            this.Controls.Add(this.txtitemname);
            this.Controls.Add(this.Detailsgrid);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRemoveitemdetailsSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRemoveitemdetailsSummary";
            this.Load += new System.EventHandler(this.frmRemoveitemdetailsSummary_Load);
            this.Click += new System.EventHandler(this.frmRemoveitemdetailsSummary_Click);
            this.MouseCaptureChanged += new System.EventHandler(this.frmRemoveitemdetailsSummary_MouseCaptureChanged);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Detailsgrid)).EndInit();
            this.pnlUserName.ResumeLayout(false);
            this.pnlUserName.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Detailsgrid;
        private System.Windows.Forms.TextBox txtitemname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker From_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker To_date;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Panel pnlUserName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox ItemName;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnprint;
    }
}