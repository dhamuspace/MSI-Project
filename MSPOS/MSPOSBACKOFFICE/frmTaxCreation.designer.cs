namespace MSPOSBACKOFFICE
{
    partial class frmTaxCreation
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
            this.txtPurchaseValues = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Value = new System.Windows.Forms.TextBox();
            this.txt_taxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_TaxName = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.lbl_TaxBanner = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnl_brand = new System.Windows.Forms.Panel();
            this.lbl_modelname = new System.Windows.Forms.Label();
            this.Pnl_Back.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Pnl_Back.Controls.Add(this.txtPurchaseValues);
            this.Pnl_Back.Controls.Add(this.label1);
            this.Pnl_Back.Controls.Add(this.txt_Value);
            this.Pnl_Back.Controls.Add(this.txt_taxName);
            this.Pnl_Back.Controls.Add(this.label2);
            this.Pnl_Back.Controls.Add(this.lbl_TaxName);
            this.Pnl_Back.Location = new System.Drawing.Point(211, 47);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(804, 496);
            this.Pnl_Back.TabIndex = 1;
            // 
            // txtPurchaseValues
            // 
            this.txtPurchaseValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseValues.Location = new System.Drawing.Point(207, 123);
            this.txtPurchaseValues.MaximumSize = new System.Drawing.Size(215, 25);
            this.txtPurchaseValues.Name = "txtPurchaseValues";
            this.txtPurchaseValues.Size = new System.Drawing.Size(215, 20);
            this.txtPurchaseValues.TabIndex = 3;
            this.txtPurchaseValues.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurchaseValues_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tax Purchase Value (%)";
            // 
            // txt_Value
            // 
            this.txt_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Value.Location = new System.Drawing.Point(208, 79);
            this.txt_Value.MaximumSize = new System.Drawing.Size(215, 25);
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Size = new System.Drawing.Size(215, 20);
            this.txt_Value.TabIndex = 2;
            this.txt_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Value_KeyDown);
            this.txt_Value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Value_KeyPress);
            // 
            // txt_taxName
            // 
            this.txt_taxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_taxName.Location = new System.Drawing.Point(208, 33);
            this.txt_taxName.MaximumSize = new System.Drawing.Size(215, 25);
            this.txt_taxName.Name = "txt_taxName";
            this.txt_taxName.Size = new System.Drawing.Size(215, 20);
            this.txt_taxName.TabIndex = 1;
            this.txt_taxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_taxName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tax Sales Value (%)";
            // 
            // lbl_TaxName
            // 
            this.lbl_TaxName.AutoSize = true;
            this.lbl_TaxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TaxName.ForeColor = System.Drawing.Color.White;
            this.lbl_TaxName.Location = new System.Drawing.Point(18, 33);
            this.lbl_TaxName.Name = "lbl_TaxName";
            this.lbl_TaxName.Size = new System.Drawing.Size(80, 20);
            this.lbl_TaxName.TabIndex = 2;
            this.lbl_TaxName.Text = "Tax Name";
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.White;
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_Exit.Location = new System.Drawing.Point(1178, 7);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(70, 36);
            this.btn_Exit.TabIndex = 7;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.White;
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_Save.Location = new System.Drawing.Point(1032, 7);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 36);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            this.btn_Save.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Save_KeyDown);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.lbl_TaxBanner);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 48);
            this.Pnl_Header.TabIndex = 2;
            // 
            // lbl_TaxBanner
            // 
            this.lbl_TaxBanner.AutoSize = true;
            this.lbl_TaxBanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_TaxBanner.ForeColor = System.Drawing.Color.White;
            this.lbl_TaxBanner.Location = new System.Drawing.Point(-1, 9);
            this.lbl_TaxBanner.Name = "lbl_TaxBanner";
            this.lbl_TaxBanner.Size = new System.Drawing.Size(110, 20);
            this.lbl_TaxBanner.TabIndex = 0;
            this.lbl_TaxBanner.Text = "Tax Creation";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnDelete);
            this.Pnl_Footer.Controls.Add(this.btnClear);
            this.Pnl_Footer.Controls.Add(this.btn_Exit);
            this.Pnl_Footer.Controls.Add(this.btn_Save);
            this.Pnl_Footer.Location = new System.Drawing.Point(-240, 543);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1260, 48);
            this.Pnl_Footer.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDelete.Location = new System.Drawing.Point(960, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 36);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnClear.Location = new System.Drawing.Point(1105, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 36);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Controls.Add(this.pnl_brand);
            this.panel4.Controls.Add(this.lbl_modelname);
            this.panel4.Location = new System.Drawing.Point(1, 47);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(210, 497);
            this.panel4.TabIndex = 6;
            // 
            // pnl_brand
            // 
            this.pnl_brand.AutoScroll = true;
            this.pnl_brand.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pnl_brand.Location = new System.Drawing.Point(-1, 30);
            this.pnl_brand.Name = "pnl_brand";
            this.pnl_brand.Size = new System.Drawing.Size(210, 466);
            this.pnl_brand.TabIndex = 1;
            // 
            // lbl_modelname
            // 
            this.lbl_modelname.AutoSize = true;
            this.lbl_modelname.BackColor = System.Drawing.Color.Transparent;
            this.lbl_modelname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_modelname.ForeColor = System.Drawing.Color.White;
            this.lbl_modelname.Location = new System.Drawing.Point(47, 8);
            this.lbl_modelname.Name = "lbl_modelname";
            this.lbl_modelname.Size = new System.Drawing.Size(72, 17);
            this.lbl_modelname.TabIndex = 0;
            this.lbl_modelname.Text = "Tax Name";
            // 
            // frmTaxCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.Pnl_Back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTaxCreation";
            this.Text = "frmTaxCreation";
            this.Load += new System.EventHandler(this.frmTaxCreation_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.TextBox txt_Value;
        private System.Windows.Forms.TextBox txt_taxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_TaxName;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lbl_TaxBanner;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnl_brand;
        private System.Windows.Forms.Label lbl_modelname;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtPurchaseValues;
        private System.Windows.Forms.Label label1;
    }
}