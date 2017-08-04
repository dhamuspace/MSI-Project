namespace MSPOSBACKOFFICE
{
    partial class Brand
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
            this.pnl_brand = new System.Windows.Forms.Panel();
            this.lbl_modelname = new System.Windows.Forms.Label();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.txt_Bname = new System.Windows.Forms.TextBox();
            this.lbl_brandnew = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Brnd_Clear = new System.Windows.Forms.Button();
            this.btn_Brnd_Exit = new System.Windows.Forms.Button();
            this.btn_Brnd_Delete = new System.Windows.Forms.Button();
            this.btn_Brnd_Update = new System.Windows.Forms.Button();
            this.btn_Brnd_save = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.Pnl_Back.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_brand
            // 
            this.pnl_brand.AutoScroll = true;
            this.pnl_brand.BackColor = System.Drawing.Color.DimGray;
            this.pnl_brand.Location = new System.Drawing.Point(0, 31);
            this.pnl_brand.Name = "pnl_brand";
            this.pnl_brand.Size = new System.Drawing.Size(207, 473);
            this.pnl_brand.TabIndex = 1;
            // 
            // lbl_modelname
            // 
            this.lbl_modelname.AutoSize = true;
            this.lbl_modelname.BackColor = System.Drawing.Color.Transparent;
            this.lbl_modelname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lbl_modelname.ForeColor = System.Drawing.Color.White;
            this.lbl_modelname.Location = new System.Drawing.Point(48, 5);
            this.lbl_modelname.Name = "lbl_modelname";
            this.lbl_modelname.Size = new System.Drawing.Size(91, 18);
            this.lbl_modelname.TabIndex = 0;
            this.lbl_modelname.Text = "Brand Name";
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Back.Controls.Add(this.txt_Bname);
            this.Pnl_Back.Controls.Add(this.lbl_brandnew);
            this.Pnl_Back.Location = new System.Drawing.Point(208, 44);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(808, 504);
            this.Pnl_Back.TabIndex = 6;
            // 
            // txt_Bname
            // 
            this.txt_Bname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Bname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Bname.Location = new System.Drawing.Point(105, 77);
            this.txt_Bname.Name = "txt_Bname";
            this.txt_Bname.Size = new System.Drawing.Size(322, 22);
            this.txt_Bname.TabIndex = 1;
            this.txt_Bname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Bname_KeyDown);
            // 
            // lbl_brandnew
            // 
            this.lbl_brandnew.AutoSize = true;
            this.lbl_brandnew.BackColor = System.Drawing.Color.Transparent;
            this.lbl_brandnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_brandnew.ForeColor = System.Drawing.Color.White;
            this.lbl_brandnew.Location = new System.Drawing.Point(3, 78);
            this.lbl_brandnew.Name = "lbl_brandnew";
            this.lbl_brandnew.Size = new System.Drawing.Size(87, 17);
            this.lbl_brandnew.TabIndex = 0;
            this.lbl_brandnew.Text = "Brand Name";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 42);
            this.Pnl_Header.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Brand";
            // 
            // btn_Brnd_Clear
            // 
            this.btn_Brnd_Clear.BackColor = System.Drawing.Color.White;
            this.btn_Brnd_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Brnd_Clear.Location = new System.Drawing.Point(870, 3);
            this.btn_Brnd_Clear.Name = "btn_Brnd_Clear";
            this.btn_Brnd_Clear.Size = new System.Drawing.Size(70, 36);
            this.btn_Brnd_Clear.TabIndex = 6;
            this.btn_Brnd_Clear.Text = "&Clear";
            this.btn_Brnd_Clear.UseVisualStyleBackColor = false;
            this.btn_Brnd_Clear.Click += new System.EventHandler(this.btn_Brnd_Clear_Click);
            this.btn_Brnd_Clear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Brnd_Clear_KeyDown);
            // 
            // btn_Brnd_Exit
            // 
            this.btn_Brnd_Exit.BackColor = System.Drawing.Color.White;
            this.btn_Brnd_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Brnd_Exit.Location = new System.Drawing.Point(939, 3);
            this.btn_Brnd_Exit.Name = "btn_Brnd_Exit";
            this.btn_Brnd_Exit.Size = new System.Drawing.Size(70, 36);
            this.btn_Brnd_Exit.TabIndex = 5;
            this.btn_Brnd_Exit.Text = "&Exit";
            this.btn_Brnd_Exit.UseVisualStyleBackColor = false;
            this.btn_Brnd_Exit.Click += new System.EventHandler(this.btn_Brnd_Exit_Click);
            this.btn_Brnd_Exit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Brnd_Exit_KeyDown);
            // 
            // btn_Brnd_Delete
            // 
            this.btn_Brnd_Delete.BackColor = System.Drawing.Color.White;
            this.btn_Brnd_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Brnd_Delete.Location = new System.Drawing.Point(801, 3);
            this.btn_Brnd_Delete.Name = "btn_Brnd_Delete";
            this.btn_Brnd_Delete.Size = new System.Drawing.Size(70, 36);
            this.btn_Brnd_Delete.TabIndex = 4;
            this.btn_Brnd_Delete.Text = "&Delete";
            this.btn_Brnd_Delete.UseVisualStyleBackColor = false;
            this.btn_Brnd_Delete.Click += new System.EventHandler(this.btn_Brnd_Delete_Click);
            this.btn_Brnd_Delete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Brnd_Delete_KeyDown);
            // 
            // btn_Brnd_Update
            // 
            this.btn_Brnd_Update.BackColor = System.Drawing.Color.White;
            this.btn_Brnd_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Brnd_Update.Location = new System.Drawing.Point(732, 3);
            this.btn_Brnd_Update.Name = "btn_Brnd_Update";
            this.btn_Brnd_Update.Size = new System.Drawing.Size(70, 36);
            this.btn_Brnd_Update.TabIndex = 3;
            this.btn_Brnd_Update.Text = "&Update";
            this.btn_Brnd_Update.UseVisualStyleBackColor = false;
            this.btn_Brnd_Update.Click += new System.EventHandler(this.btn_Brnd_Update_Click);
            this.btn_Brnd_Update.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Brnd_Update_KeyDown);
            // 
            // btn_Brnd_save
            // 
            this.btn_Brnd_save.BackColor = System.Drawing.Color.White;
            this.btn_Brnd_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Brnd_save.Location = new System.Drawing.Point(662, 3);
            this.btn_Brnd_save.Name = "btn_Brnd_save";
            this.btn_Brnd_save.Size = new System.Drawing.Size(70, 36);
            this.btn_Brnd_save.TabIndex = 2;
            this.btn_Brnd_save.Text = "&Save";
            this.btn_Brnd_save.UseVisualStyleBackColor = false;
            this.btn_Brnd_save.Click += new System.EventHandler(this.btn_B_save_Click);
            this.btn_Brnd_save.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Brnd_save_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.pnl_brand);
            this.panel1.Controls.Add(this.lbl_modelname);
            this.panel1.Location = new System.Drawing.Point(1, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 505);
            this.panel1.TabIndex = 5;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btn_Brnd_Clear);
            this.Pnl_Footer.Controls.Add(this.btn_Brnd_Exit);
            this.Pnl_Footer.Controls.Add(this.btn_Brnd_Update);
            this.Pnl_Footer.Controls.Add(this.btn_Brnd_Delete);
            this.Pnl_Footer.Controls.Add(this.btn_Brnd_save);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 548);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 43);
            this.Pnl_Footer.TabIndex = 10;
            // 
            // Brand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Brand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Brand";
            this.Load += new System.EventHandler(this.Brand_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Brand_KeyDown);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_brand;
        private System.Windows.Forms.Label lbl_modelname;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Brnd_Clear;
        private System.Windows.Forms.Button btn_Brnd_Exit;
        private System.Windows.Forms.Button btn_Brnd_Delete;
        private System.Windows.Forms.Button btn_Brnd_Update;
        private System.Windows.Forms.Button btn_Brnd_save;
        private System.Windows.Forms.TextBox txt_Bname;
        private System.Windows.Forms.Label lbl_brandnew;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel Pnl_Footer;
    }
}