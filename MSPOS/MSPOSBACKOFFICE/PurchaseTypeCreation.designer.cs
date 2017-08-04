namespace MSPOSBACKOFFICE
{
    partial class PurchaseTypeCreation
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
            this.label17 = new System.Windows.Forms.Label();
            this.txtPurUnder = new System.Windows.Forms.TextBox();
            this.txtPurchaseType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlpurtype = new System.Windows.Forms.Panel();
            this.lvPurchase = new System.Windows.Forms.ListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.Pnl_Header.SuspendLayout();
            this.pnlpurtype.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label17);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 45);
            this.Pnl_Header.TabIndex = 76;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(0, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(178, 20);
            this.label17.TabIndex = 0;
            this.label17.Text = "Purchase Type Creation";
            // 
            // txtPurUnder
            // 
            this.txtPurUnder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurUnder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPurUnder.Location = new System.Drawing.Point(240, 120);
            this.txtPurUnder.Name = "txtPurUnder";
            this.txtPurUnder.Size = new System.Drawing.Size(334, 23);
            this.txtPurUnder.TabIndex = 77;
            this.txtPurUnder.TextChanged += new System.EventHandler(this.txtPurUnder_TextChanged);
            this.txtPurUnder.Enter += new System.EventHandler(this.txtPurUnder_Enter);
            this.txtPurUnder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurUnder_KeyDown);
            // 
            // txtPurchaseType
            // 
            this.txtPurchaseType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPurchaseType.Location = new System.Drawing.Point(240, 76);
            this.txtPurchaseType.Name = "txtPurchaseType";
            this.txtPurchaseType.Size = new System.Drawing.Size(334, 23);
            this.txtPurchaseType.TabIndex = 79;
            this.txtPurchaseType.Enter += new System.EventHandler(this.txtPurchaseType_Enter);
            this.txtPurchaseType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseType_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(140, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 16);
            this.label8.TabIndex = 81;
            this.label8.Text = "Under";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(140, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 16);
            this.label7.TabIndex = 80;
            this.label7.Text = "PurType";
            // 
            // pnlpurtype
            // 
            this.pnlpurtype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlpurtype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlpurtype.Controls.Add(this.lvPurchase);
            this.pnlpurtype.Controls.Add(this.label19);
            this.pnlpurtype.Location = new System.Drawing.Point(580, 76);
            this.pnlpurtype.Name = "pnlpurtype";
            this.pnlpurtype.Size = new System.Drawing.Size(349, 309);
            this.pnlpurtype.TabIndex = 82;
            // 
            // lvPurchase
            // 
            this.lvPurchase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lvPurchase.FormattingEnabled = true;
            this.lvPurchase.ItemHeight = 16;
            this.lvPurchase.Location = new System.Drawing.Point(6, 38);
            this.lvPurchase.Name = "lvPurchase";
            this.lvPurchase.Size = new System.Drawing.Size(335, 260);
            this.lvPurchase.TabIndex = 29;
            this.lvPurchase.Click += new System.EventHandler(this.lvPurchase_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.75F);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(94, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 25);
            this.label19.TabIndex = 47;
            this.label19.Text = "Select One";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnSave);
            this.Pnl_Footer.Controls.Add(this.btnCancel);
            this.Pnl_Footer.Controls.Add(this.btn_Exit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 545);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 47);
            this.Pnl_Footer.TabIndex = 77;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(750, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 41);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(834, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 41);
            this.btnCancel.TabIndex = 93;
            this.btnCancel.Text = "Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.White;
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.ForeColor = System.Drawing.Color.Black;
            this.btn_Exit.Location = new System.Drawing.Point(918, 2);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(85, 41);
            this.btn_Exit.TabIndex = 94;
            this.btn_Exit.Text = "E&xit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // PurchaseTypeCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.pnlpurtype);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPurUnder);
            this.Controls.Add(this.txtPurchaseType);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PurchaseTypeCreation";
            this.Text = "PurchaseTypeCreation";
            this.Load += new System.EventHandler(this.PurchaseTypeCreation_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.pnlpurtype.ResumeLayout(false);
            this.pnlpurtype.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPurUnder;
        private System.Windows.Forms.TextBox txtPurchaseType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlpurtype;
        private System.Windows.Forms.ListBox lvPurchase;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btn_Exit;
    }
}