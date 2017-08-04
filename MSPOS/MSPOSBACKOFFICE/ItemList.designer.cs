namespace MSPOSBACKOFFICE
{
    partial class ItemList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_opnqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_ndp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_mrsp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_special1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_special2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item_special3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblNoofItems = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_filter = new System.Windows.Forms.Button();
            this.Pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Location = new System.Drawing.Point(2, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 47);
            this.Pnl_Header.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-1, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "List Of Items";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "No Of Items :";
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.White;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_add.ForeColor = System.Drawing.Color.Black;
            this.btn_add.Location = new System.Drawing.Point(788, 5);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(76, 38);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item_code,
            this.Item_name,
            this.nt_opnqty,
            this.Stock_Value,
            this.Item_ndp,
            this.Item_cost,
            this.Item_mrsp,
            this.Item_special1,
            this.Item_special2,
            this.Item_special3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.Location = new System.Drawing.Point(3, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1012, 474);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Item_code
            // 
            this.Item_code.DataPropertyName = "Item_code";
            this.Item_code.HeaderText = "Code";
            this.Item_code.Name = "Item_code";
            this.Item_code.Width = 175;
            // 
            // Item_name
            // 
            this.Item_name.DataPropertyName = "Item_name";
            this.Item_name.HeaderText = "Name";
            this.Item_name.Name = "Item_name";
            this.Item_name.Width = 300;
            // 
            // nt_opnqty
            // 
            this.nt_opnqty.DataPropertyName = "nt_opnqty";
            this.nt_opnqty.HeaderText = "Nt Qty";
            this.nt_opnqty.Name = "nt_opnqty";
            // 
            // Stock_Value
            // 
            this.Stock_Value.DataPropertyName = "Stock_Value";
            this.Stock_Value.HeaderText = "Value";
            this.Stock_Value.Name = "Stock_Value";
            // 
            // Item_ndp
            // 
            this.Item_ndp.DataPropertyName = "Item_ndp";
            this.Item_ndp.HeaderText = "P.Rate";
            this.Item_ndp.Name = "Item_ndp";
            // 
            // Item_cost
            // 
            this.Item_cost.DataPropertyName = "Item_cost";
            this.Item_cost.HeaderText = "Cost";
            this.Item_cost.Name = "Item_cost";
            // 
            // Item_mrsp
            // 
            this.Item_mrsp.DataPropertyName = "Item_mrsp";
            this.Item_mrsp.HeaderText = "Mrp";
            this.Item_mrsp.Name = "Item_mrsp";
            // 
            // Item_special1
            // 
            this.Item_special1.DataPropertyName = "Item_special1";
            this.Item_special1.HeaderText = "Special-1";
            this.Item_special1.Name = "Item_special1";
            // 
            // Item_special2
            // 
            this.Item_special2.DataPropertyName = "Item_special2";
            this.Item_special2.HeaderText = "Special-2";
            this.Item_special2.Name = "Item_special2";
            // 
            // Item_special3
            // 
            this.Item_special3.DataPropertyName = "Item_special3";
            this.Item_special3.HeaderText = "Special-3";
            this.Item_special3.Name = "Item_special3";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Controls.Add(this.lblNoofItems);
            this.Pnl_Footer.Controls.Add(this.btn_add);
            this.Pnl_Footer.Controls.Add(this.label1);
            this.Pnl_Footer.Controls.Add(this.button3);
            this.Pnl_Footer.Controls.Add(this.btn_filter);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 544);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 48);
            this.Pnl_Footer.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(710, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(76, 38);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblNoofItems
            // 
            this.lblNoofItems.AutoSize = true;
            this.lblNoofItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofItems.ForeColor = System.Drawing.Color.White;
            this.lblNoofItems.Location = new System.Drawing.Point(108, 17);
            this.lblNoofItems.Name = "lblNoofItems";
            this.lblNoofItems.Size = new System.Drawing.Size(18, 20);
            this.lblNoofItems.TabIndex = 2;
            this.lblNoofItems.Text = "0";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(940, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 38);
            this.button3.TabIndex = 6;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_filter
            // 
            this.btn_filter.BackColor = System.Drawing.Color.White;
            this.btn_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_filter.ForeColor = System.Drawing.Color.Black;
            this.btn_filter.Location = new System.Drawing.Point(864, 5);
            this.btn_filter.Name = "btn_filter";
            this.btn_filter.Size = new System.Drawing.Size(76, 38);
            this.btn_filter.TabIndex = 5;
            this.btn_filter.Text = "Filter";
            this.btn_filter.UseVisualStyleBackColor = false;
            this.btn_filter.Click += new System.EventHandler(this.btn_filter_Click);
            // 
            // ItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ItemList";
            this.Text = "ItemList";
            this.Load += new System.EventHandler(this.ItemList_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Label lblNoofItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_filter;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_opnqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_ndp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_mrsp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_special1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_special2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item_special3;
        private System.Windows.Forms.Button btnPrint;
    }
}