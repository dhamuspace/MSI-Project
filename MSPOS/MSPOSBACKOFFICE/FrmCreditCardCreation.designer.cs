namespace MSPOSBACKOFFICE
{
    partial class FrmCreditCardCreation
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
            this.PnlLedgerGroup = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.listLedgerGroup = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCreditCardName = new System.Windows.Forms.TextBox();
            this.txtLedgerGroupName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.lbl_Ctrbanner = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.PnlLedgerGroup.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlLedgerGroup
            // 
            this.PnlLedgerGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PnlLedgerGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlLedgerGroup.Controls.Add(this.label18);
            this.PnlLedgerGroup.Controls.Add(this.listLedgerGroup);
            this.PnlLedgerGroup.Location = new System.Drawing.Point(157, 153);
            this.PnlLedgerGroup.Name = "PnlLedgerGroup";
            this.PnlLedgerGroup.Size = new System.Drawing.Size(566, 346);
            this.PnlLedgerGroup.TabIndex = 74;
            this.PnlLedgerGroup.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(203, 5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(148, 26);
            this.label18.TabIndex = 0;
            this.label18.Text = "List Of Ledger";
            // 
            // listLedgerGroup
            // 
            this.listLedgerGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listLedgerGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.listLedgerGroup.FormattingEnabled = true;
            this.listLedgerGroup.ItemHeight = 20;
            this.listLedgerGroup.Location = new System.Drawing.Point(6, 36);
            this.listLedgerGroup.Name = "listLedgerGroup";
            this.listLedgerGroup.Size = new System.Drawing.Size(552, 302);
            this.listLedgerGroup.TabIndex = 3;
            this.listLedgerGroup.Click += new System.EventHandler(this.listLedgerGroup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(153, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 73;
            this.label3.Text = "Card Name";
            // 
            // txtCreditCardName
            // 
            this.txtCreditCardName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditCardName.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtCreditCardName.Location = new System.Drawing.Point(278, 79);
            this.txtCreditCardName.Name = "txtCreditCardName";
            this.txtCreditCardName.Size = new System.Drawing.Size(437, 26);
            this.txtCreditCardName.TabIndex = 1;
            this.txtCreditCardName.Enter += new System.EventHandler(this.txtCreditCardName_Enter);
            this.txtCreditCardName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCreditCardName_KeyDown);
            this.txtCreditCardName.Leave += new System.EventHandler(this.txtCreditCardName_Leave);
            // 
            // txtLedgerGroupName
            // 
            this.txtLedgerGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLedgerGroupName.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtLedgerGroupName.Location = new System.Drawing.Point(278, 111);
            this.txtLedgerGroupName.Name = "txtLedgerGroupName";
            this.txtLedgerGroupName.Size = new System.Drawing.Size(437, 26);
            this.txtLedgerGroupName.TabIndex = 2;
            this.txtLedgerGroupName.TextChanged += new System.EventHandler(this.txtLedgerGroupName_TextChanged);
            this.txtLedgerGroupName.Enter += new System.EventHandler(this.txtLedgerGroupName_Enter);
            this.txtLedgerGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            this.txtLedgerGroupName.Leave += new System.EventHandler(this.txtLedgerGroupName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(153, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 76;
            this.label1.Text = "Bank Ledger";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_Ctrbanner);
            this.Pnl_Header.Location = new System.Drawing.Point(-2, 1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1004, 50);
            this.Pnl_Header.TabIndex = 77;
            // 
            // lbl_Ctrbanner
            // 
            this.lbl_Ctrbanner.AutoSize = true;
            this.lbl_Ctrbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Ctrbanner.ForeColor = System.Drawing.Color.White;
            this.lbl_Ctrbanner.Location = new System.Drawing.Point(6, 14);
            this.lbl_Ctrbanner.Name = "lbl_Ctrbanner";
            this.lbl_Ctrbanner.Size = new System.Drawing.Size(211, 25);
            this.lbl_Ctrbanner.TabIndex = 0;
            this.lbl_Ctrbanner.Text = "Credit Card Creation";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.btnClear);
            this.Pnl_Footer.Controls.Add(this.btnSave);
            this.Pnl_Footer.Location = new System.Drawing.Point(-2, 504);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1011, 50);
            this.Pnl_Footer.TabIndex = 78;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExit.Location = new System.Drawing.Point(931, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 36);
            this.btnExit.TabIndex = 81;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClear.Location = new System.Drawing.Point(862, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 36);
            this.btnClear.TabIndex = 80;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSave.Location = new System.Drawing.Point(793, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 36);
            this.btnSave.TabIndex = 79;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmCreditCardCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1003, 554);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLedgerGroupName);
            this.Controls.Add(this.PnlLedgerGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCreditCardName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCreditCardCreation";
            this.Text = "FrmCreditCardCreation";
            this.Load += new System.EventHandler(this.FrmCreditCardCreation_Load);
            this.PnlLedgerGroup.ResumeLayout(false);
            this.PnlLedgerGroup.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PnlLedgerGroup;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ListBox listLedgerGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCreditCardName;
        private System.Windows.Forms.TextBox txtLedgerGroupName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_Ctrbanner;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
    }
}