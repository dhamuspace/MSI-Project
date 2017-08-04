namespace MSPOSBACKOFFICE
{
    partial class SalesBOMAlterion
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
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.txtBomname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lstBomName = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Back.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.Controls.Add(this.Pnl_Footer);
            this.Pnl_Back.Controls.Add(this.Pnl_Header);
            this.Pnl_Back.Controls.Add(this.panel2);
            this.Pnl_Back.Controls.Add(this.label1);
            this.Pnl_Back.Location = new System.Drawing.Point(2, 2);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1019, 625);
            this.Pnl_Back.TabIndex = 0;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 541);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 49);
            this.Pnl_Footer.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(905, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 38);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.txtBomname);
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Location = new System.Drawing.Point(-1, 38);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 53);
            this.Pnl_Header.TabIndex = 1;
            // 
            // txtBomname
            // 
            this.txtBomname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBomname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBomname.Location = new System.Drawing.Point(70, 15);
            this.txtBomname.Name = "txtBomname";
            this.txtBomname.Size = new System.Drawing.Size(329, 23);
            this.txtBomname.TabIndex = 0;
            this.txtBomname.TextChanged += new System.EventHandler(this.txtBomname_TextChanged);
            this.txtBomname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBomname_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(13, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lstBomName);
            this.panel2.Location = new System.Drawing.Point(-1, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 447);
            this.panel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(405, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "List Of BOM\'S";
            // 
            // lstBomName
            // 
            this.lstBomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lstBomName.FormattingEnabled = true;
            this.lstBomName.ItemHeight = 20;
            this.lstBomName.Location = new System.Drawing.Point(6, 32);
            this.lstBomName.Name = "lstBomName";
            this.lstBomName.Size = new System.Drawing.Size(994, 402);
            this.lstBomName.TabIndex = 3;
            this.lstBomName.Click += new System.EventHandler(this.lstBomName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(-2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bom Aletrion";
            // 
            // SalesBOMAlterion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Back);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesBOMAlterion";
            this.Text = "BOMAlterion";
            this.Load += new System.EventHandler(this.SalesBOMAlterion_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lstBomName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtBomname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel Pnl_Footer;
    }
}