namespace MSPOSBACKOFFICE
{
    partial class Rack
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
            this.pnl_rack = new System.Windows.Forms.Panel();
            this.lbl_rackname = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.lbl_racknew = new System.Windows.Forms.Label();
            this.txt_Rname = new System.Windows.Forms.TextBox();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Rack__Clear = new System.Windows.Forms.Button();
            this.btn_Rack__Exit = new System.Windows.Forms.Button();
            this.btn_Rack_Update = new System.Windows.Forms.Button();
            this.btn_Rack_Delete = new System.Windows.Forms.Button();
            this.btn_Rack_save = new System.Windows.Forms.Button();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.Pnl_Back.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_rack
            // 
            this.pnl_rack.AutoScroll = true;
            this.pnl_rack.BackColor = System.Drawing.Color.DimGray;
            this.pnl_rack.Location = new System.Drawing.Point(1, 33);
            this.pnl_rack.Name = "pnl_rack";
            this.pnl_rack.Size = new System.Drawing.Size(209, 465);
            this.pnl_rack.TabIndex = 1;
            // 
            // lbl_rackname
            // 
            this.lbl_rackname.AutoSize = true;
            this.lbl_rackname.BackColor = System.Drawing.Color.Transparent;
            this.lbl_rackname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_rackname.ForeColor = System.Drawing.Color.White;
            this.lbl_rackname.Location = new System.Drawing.Point(52, 7);
            this.lbl_rackname.Name = "lbl_rackname";
            this.lbl_rackname.Size = new System.Drawing.Size(81, 17);
            this.lbl_rackname.TabIndex = 0;
            this.lbl_rackname.Text = "Rack Name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.pnl_rack);
            this.panel1.Controls.Add(this.lbl_rackname);
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 499);
            this.panel1.TabIndex = 5;
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Back.Controls.Add(this.lbl_racknew);
            this.Pnl_Back.Controls.Add(this.txt_Rname);
            this.Pnl_Back.Location = new System.Drawing.Point(210, 45);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(805, 499);
            this.Pnl_Back.TabIndex = 2;
            // 
            // lbl_racknew
            // 
            this.lbl_racknew.AutoSize = true;
            this.lbl_racknew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_racknew.ForeColor = System.Drawing.Color.White;
            this.lbl_racknew.Location = new System.Drawing.Point(25, 69);
            this.lbl_racknew.Name = "lbl_racknew";
            this.lbl_racknew.Size = new System.Drawing.Size(81, 17);
            this.lbl_racknew.TabIndex = 0;
            this.lbl_racknew.Text = "Rack Name";
            // 
            // txt_Rname
            // 
            this.txt_Rname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Rname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txt_Rname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_Rname.Location = new System.Drawing.Point(121, 66);
            this.txt_Rname.Name = "txt_Rname";
            this.txt_Rname.Size = new System.Drawing.Size(286, 23);
            this.txt_Rname.TabIndex = 1;
            this.txt_Rname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Rname_KeyDown);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label3);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 45);
            this.Pnl_Header.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Rack";
            // 
            // btn_Rack__Clear
            // 
            this.btn_Rack__Clear.BackColor = System.Drawing.Color.White;
            this.btn_Rack__Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rack__Clear.Location = new System.Drawing.Point(861, 4);
            this.btn_Rack__Clear.Name = "btn_Rack__Clear";
            this.btn_Rack__Clear.Size = new System.Drawing.Size(70, 36);
            this.btn_Rack__Clear.TabIndex = 6;
            this.btn_Rack__Clear.Text = "Clear";
            this.btn_Rack__Clear.UseVisualStyleBackColor = false;
            this.btn_Rack__Clear.Click += new System.EventHandler(this.btn_Rack__Clear_Click);
            this.btn_Rack__Clear.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Rack_save_Paint);
            this.btn_Rack__Clear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Rack__Clear_KeyDown);
            // 
            // btn_Rack__Exit
            // 
            this.btn_Rack__Exit.BackColor = System.Drawing.Color.White;
            this.btn_Rack__Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rack__Exit.Location = new System.Drawing.Point(931, 4);
            this.btn_Rack__Exit.Name = "btn_Rack__Exit";
            this.btn_Rack__Exit.Size = new System.Drawing.Size(70, 36);
            this.btn_Rack__Exit.TabIndex = 5;
            this.btn_Rack__Exit.Text = "Exit";
            this.btn_Rack__Exit.UseVisualStyleBackColor = false;
            this.btn_Rack__Exit.Click += new System.EventHandler(this.btn_Rack__Exit_Click);
            this.btn_Rack__Exit.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Rack_save_Paint);
            this.btn_Rack__Exit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Rack__Exit_KeyDown);
            // 
            // btn_Rack_Update
            // 
            this.btn_Rack_Update.BackColor = System.Drawing.Color.White;
            this.btn_Rack_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rack_Update.Location = new System.Drawing.Point(722, 5);
            this.btn_Rack_Update.Name = "btn_Rack_Update";
            this.btn_Rack_Update.Size = new System.Drawing.Size(70, 36);
            this.btn_Rack_Update.TabIndex = 3;
            this.btn_Rack_Update.Text = "Update";
            this.btn_Rack_Update.UseVisualStyleBackColor = false;
            this.btn_Rack_Update.Click += new System.EventHandler(this.btn_Rack_Update_Click);
            this.btn_Rack_Update.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Rack_save_Paint);
            this.btn_Rack_Update.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Rack_Update_KeyDown);
            // 
            // btn_Rack_Delete
            // 
            this.btn_Rack_Delete.BackColor = System.Drawing.Color.White;
            this.btn_Rack_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rack_Delete.Location = new System.Drawing.Point(792, 4);
            this.btn_Rack_Delete.Name = "btn_Rack_Delete";
            this.btn_Rack_Delete.Size = new System.Drawing.Size(70, 36);
            this.btn_Rack_Delete.TabIndex = 4;
            this.btn_Rack_Delete.Text = "Delete";
            this.btn_Rack_Delete.UseVisualStyleBackColor = false;
            this.btn_Rack_Delete.Click += new System.EventHandler(this.btn_Rack_Delete_Click);
            this.btn_Rack_Delete.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Rack_save_Paint);
            this.btn_Rack_Delete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Rack_Delete_KeyDown);
            // 
            // btn_Rack_save
            // 
            this.btn_Rack_save.BackColor = System.Drawing.Color.White;
            this.btn_Rack_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rack_save.Location = new System.Drawing.Point(652, 5);
            this.btn_Rack_save.Name = "btn_Rack_save";
            this.btn_Rack_save.Size = new System.Drawing.Size(70, 36);
            this.btn_Rack_save.TabIndex = 2;
            this.btn_Rack_save.Text = "Save";
            this.btn_Rack_save.UseVisualStyleBackColor = false;
            this.btn_Rack_save.Click += new System.EventHandler(this.btn_Rack_save_Click);
            this.btn_Rack_save.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Rack_save_Paint);
            this.btn_Rack_save.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_Rack_save_KeyDown);
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btn_Rack__Clear);
            this.Pnl_Footer.Controls.Add(this.btn_Rack_Update);
            this.Pnl_Footer.Controls.Add(this.btn_Rack__Exit);
            this.Pnl_Footer.Controls.Add(this.btn_Rack_Delete);
            this.Pnl_Footer.Controls.Add(this.btn_Rack_save);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 544);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 45);
            this.Pnl_Footer.TabIndex = 12;
            // 
            // Rack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 589);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.Pnl_Back);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Rack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rack";
            this.Load += new System.EventHandler(this.Rack_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Rack_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Rack_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_rack;
        private System.Windows.Forms.Label lbl_rackname;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Pnl_Back;
        private System.Windows.Forms.Button btn_Rack__Clear;
        private System.Windows.Forms.Button btn_Rack__Exit;
        private System.Windows.Forms.Button btn_Rack_Delete;
        private System.Windows.Forms.Button btn_Rack_Update;
        private System.Windows.Forms.Button btn_Rack_save;
        private System.Windows.Forms.TextBox txt_Rname;
        private System.Windows.Forms.Label lbl_racknew;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel Pnl_Footer;
    }
}