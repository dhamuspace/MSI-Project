namespace SalesProject._Ledger
{
    partial class LedgerAlteration
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_legername = new System.Windows.Forms.TextBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.pnl_list = new System.Windows.Forms.Panel();
            this.lst_itemName = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnl_list.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.btn_exit);
            this.panel1.Controls.Add(this.txt_legername);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(40, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 142);
            this.panel1.TabIndex = 1;
            // 
            // txt_legername
            // 
            this.txt_legername.Location = new System.Drawing.Point(154, 21);
            this.txt_legername.Name = "txt_legername";
            this.txt_legername.Size = new System.Drawing.Size(556, 20);
            this.txt_legername.TabIndex = 1;
            this.txt_legername.TextChanged += new System.EventHandler(this.txt_legername_TextChanged);
            this.txt_legername.Enter += new System.EventHandler(this.txt_legername_Enter);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(635, 80);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 2;
            this.btn_exit.Text = "E&xit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // pnl_list
            // 
            this.pnl_list.BackColor = System.Drawing.Color.Gray;
            this.pnl_list.Controls.Add(this.lst_itemName);
            this.pnl_list.Controls.Add(this.panel2);
            this.pnl_list.Location = new System.Drawing.Point(40, 197);
            this.pnl_list.Name = "pnl_list";
            this.pnl_list.Size = new System.Drawing.Size(769, 412);
            this.pnl_list.TabIndex = 40;
            // 
            // lst_itemName
            // 
            this.lst_itemName.FormattingEnabled = true;
            this.lst_itemName.Location = new System.Drawing.Point(17, 26);
            this.lst_itemName.Name = "lst_itemName";
            this.lst_itemName.Size = new System.Drawing.Size(736, 368);
            this.lst_itemName.TabIndex = 3;
            this.lst_itemName.SelectedIndexChanged += new System.EventHandler(this.lst_itemName_SelectedIndexChanged);
            this.lst_itemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_itemName_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.label28);
            this.panel2.Location = new System.Drawing.Point(1, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(768, 24);
            this.panel2.TabIndex = 0;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(345, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(119, 22);
            this.label28.TabIndex = 0;
            this.label28.Text = "List of Ledger";
            // 
            // LedgerAlteration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.pnl_list);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LedgerAlteration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LedgerAlteration";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_list.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.TextBox txt_legername;
        private System.Windows.Forms.Panel pnl_list;
        private System.Windows.Forms.ListBox lst_itemName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label28;
    }
}