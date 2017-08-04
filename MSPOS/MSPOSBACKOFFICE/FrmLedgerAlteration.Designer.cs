namespace MSPOSBACKOFFICE
{
    partial class FrmLedgerAlteration
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
            this.lbl_stckbanner = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.txtLPName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnllist = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lstLedgerAlter = new System.Windows.Forms.ListBox();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.pnllist.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_stckbanner);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1018, 50);
            this.Pnl_Header.TabIndex = 33;
            // 
            // lbl_stckbanner
            // 
            this.lbl_stckbanner.AutoSize = true;
            this.lbl_stckbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stckbanner.ForeColor = System.Drawing.Color.White;
            this.lbl_stckbanner.Location = new System.Drawing.Point(3, 13);
            this.lbl_stckbanner.Name = "lbl_stckbanner";
            this.lbl_stckbanner.Size = new System.Drawing.Size(148, 20);
            this.lbl_stckbanner.TabIndex = 0;
            this.lbl_stckbanner.Text = "Ledger Alteration";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 541);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1018, 50);
            this.Pnl_Footer.TabIndex = 34;
            // 
            // txtLPName
            // 
            this.txtLPName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLPName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtLPName.Location = new System.Drawing.Point(261, 91);
            this.txtLPName.Name = "txtLPName";
            this.txtLPName.Size = new System.Drawing.Size(396, 25);
            this.txtLPName.TabIndex = 70;
            this.txtLPName.TextChanged += new System.EventHandler(this.txtLPName_TextChanged);
            this.txtLPName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(207, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 71;
            this.label2.Text = "Name";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExit.Location = new System.Drawing.Point(919, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 36);
            this.btnExit.TabIndex = 72;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnllist
            // 
            this.pnllist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnllist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnllist.Controls.Add(this.label8);
            this.pnllist.Controls.Add(this.lstLedgerAlter);
            this.pnllist.Location = new System.Drawing.Point(11, 131);
            this.pnllist.Name = "pnllist";
            this.pnllist.Size = new System.Drawing.Size(979, 370);
            this.pnllist.TabIndex = 73;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(410, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "Select One";
            // 
            // lstLedgerAlter
            // 
            this.lstLedgerAlter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLedgerAlter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLedgerAlter.FormattingEnabled = true;
            this.lstLedgerAlter.ItemHeight = 20;
            this.lstLedgerAlter.Location = new System.Drawing.Point(6, 38);
            this.lstLedgerAlter.Name = "lstLedgerAlter";
            this.lstLedgerAlter.Size = new System.Drawing.Size(965, 322);
            this.lstLedgerAlter.TabIndex = 0;
            this.lstLedgerAlter.Click += new System.EventHandler(this.lstLedgerAlter_Click);
            // 
            // FrmLedgerAlteration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.pnllist);
            this.Controls.Add(this.txtLPName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLedgerAlteration";
            this.Text = "FrmLedgerAlteration";
            this.Load += new System.EventHandler(this.FrmLedgerAlteration_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.pnllist.ResumeLayout(false);
            this.pnllist.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_stckbanner;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.TextBox txtLPName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnllist;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lstLedgerAlter;
    }
}