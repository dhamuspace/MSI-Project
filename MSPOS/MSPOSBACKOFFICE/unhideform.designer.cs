namespace MSPOSBACKOFFICE
{
    partial class unhideform
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
            this.lbl_banner = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Chk_colHeader = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // lbl_banner
            // 
            this.lbl_banner.AutoSize = true;
            this.lbl_banner.BackColor = System.Drawing.Color.Black;
            this.lbl_banner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_banner.ForeColor = System.Drawing.Color.White;
            this.lbl_banner.Location = new System.Drawing.Point(75, 9);
            this.lbl_banner.Name = "lbl_banner";
            this.lbl_banner.Size = new System.Drawing.Size(220, 17);
            this.lbl_banner.TabIndex = 6;
            this.lbl_banner.Text = "Hide / UnHide Column Option";
            // 
            // btn_ok
            // 
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok.Location = new System.Drawing.Point(26, 250);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "&Ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(270, 250);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "&Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "    Use SpaceBar to \r\n    Select / Unselect";
            // 
            // Chk_colHeader
            // 
            this.Chk_colHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Chk_colHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_colHeader.FormattingEnabled = true;
            this.Chk_colHeader.Location = new System.Drawing.Point(26, 46);
            this.Chk_colHeader.Name = "Chk_colHeader";
            this.Chk_colHeader.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Chk_colHeader.Size = new System.Drawing.Size(319, 170);
            this.Chk_colHeader.TabIndex = 0;
            this.Chk_colHeader.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Chk_colHeader_ItemCheck);
            this.Chk_colHeader.SelectedValueChanged += new System.EventHandler(this.Chk_colHeader_SelectedValueChanged);
            this.Chk_colHeader.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Chk_colHeader_KeyPress);
            // 
            // unhideform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 297);
            this.Controls.Add(this.Chk_colHeader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lbl_banner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "unhideform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "unhideform";
            this.Load += new System.EventHandler(this.unhideform_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unhideform_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_banner;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox Chk_colHeader;
    }
}