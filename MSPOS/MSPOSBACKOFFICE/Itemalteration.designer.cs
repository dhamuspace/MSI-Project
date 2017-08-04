namespace MSPOSBACKOFFICE
{
    partial class Itemalteration
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
            this.panel2.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Item Alteration";
            // 
            // txtItem_code
            // 
            this.txtItem_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItem_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem_code.Location = new System.Drawing.Point(335, 64);
            this.txtItem_code.Name = "txtItem_code";
            this.txtItem_code.Size = new System.Drawing.Size(160, 29);
            this.txtItem_code.TabIndex = 1;
            this.txtItem_code.TextChanged += new System.EventHandler(this.txtItem_code_TextChanged);
            this.txtItem_code.Enter += new System.EventHandler(this.txtItem_code_Enter);
            this.txtItem_code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_code_KeyDown);
            // 
            // txt_itemname
            // 
            this.txt_itemname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_itemname.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_itemname.Location = new System.Drawing.Point(335, 99);
            this.txt_itemname.Name = "txt_itemname";
            this.txt_itemname.Size = new System.Drawing.Size(334, 29);
            this.txt_itemname.TabIndex = 2;
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
            this.listitems.Location = new System.Drawing.Point(7, 37);
            this.listitems.Name = "listitems";
            this.listitems.Size = new System.Drawing.Size(971, 362);
            this.listitems.TabIndex = 3;
            this.listitems.Click += new System.EventHandler(this.listitems_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.listitems);
            this.panel2.Location = new System.Drawing.Point(25, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(988, 409);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(459, 3);
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
            this.button1.Location = new System.Drawing.Point(927, 5);
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
            this.label3.Location = new System.Drawing.Point(246, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(246, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Name";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Location = new System.Drawing.Point(-1, 1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1014, 52);
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
            // Itemalteration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txt_itemname);
            this.Controls.Add(this.txtItem_code);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Itemalteration";
            this.Text = "Itemalteration";
            this.Load += new System.EventHandler(this.Itemalteration_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
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
    }
}