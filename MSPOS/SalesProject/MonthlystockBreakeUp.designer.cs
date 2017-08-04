namespace SalesProject
{
    partial class MonthlystockBreakeUp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DgMonthlyStockRpt = new System.Windows.Forms.DataGridView();
            this.nq_purqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_purval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_prqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_purRetval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nt_cloqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.txtitemname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.listview = new System.Windows.Forms.ListBox();
            this.pnlitems = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgMonthlyStockRpt)).BeginInit();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.pnlitems.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgMonthlyStockRpt
            // 
            this.DgMonthlyStockRpt.BackgroundColor = System.Drawing.Color.White;
            this.DgMonthlyStockRpt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgMonthlyStockRpt.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DgMonthlyStockRpt.ColumnHeadersHeight = 50;
            this.DgMonthlyStockRpt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nq_purqty,
            this.nt_purval,
            this.nt_prqty,
            this.nt_purRetval,
            this.nt_cloqty,
            this.tot});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgMonthlyStockRpt.DefaultCellStyle = dataGridViewCellStyle5;
            this.DgMonthlyStockRpt.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgMonthlyStockRpt.Location = new System.Drawing.Point(23, 53);
            this.DgMonthlyStockRpt.Name = "DgMonthlyStockRpt";
            this.DgMonthlyStockRpt.ReadOnly = true;
            this.DgMonthlyStockRpt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgMonthlyStockRpt.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DgMonthlyStockRpt.RowHeadersWidth = 250;
            this.DgMonthlyStockRpt.Size = new System.Drawing.Size(976, 404);
            this.DgMonthlyStockRpt.TabIndex = 1;
            this.DgMonthlyStockRpt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.DgMonthlyStockRpt.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            // 
            // nq_purqty
            // 
            this.nq_purqty.DataPropertyName = "nq_purqty";
            this.nq_purqty.HeaderText = "Recd Qty";
            this.nq_purqty.Name = "nq_purqty";
            this.nq_purqty.ReadOnly = true;
            this.nq_purqty.Width = 150;
            // 
            // nt_purval
            // 
            this.nt_purval.DataPropertyName = "nt_purval";
            this.nt_purval.HeaderText = "Recd Val";
            this.nt_purval.Name = "nt_purval";
            this.nt_purval.ReadOnly = true;
            this.nt_purval.Width = 150;
            // 
            // nt_prqty
            // 
            this.nt_prqty.DataPropertyName = "nt_prqty";
            this.nt_prqty.HeaderText = "Issue Qty";
            this.nt_prqty.Name = "nt_prqty";
            this.nt_prqty.ReadOnly = true;
            this.nt_prqty.Width = 150;
            // 
            // nt_purRetval
            // 
            this.nt_purRetval.DataPropertyName = "nt_purRetval";
            this.nt_purRetval.HeaderText = "Issue Value";
            this.nt_purRetval.Name = "nt_purRetval";
            this.nt_purRetval.ReadOnly = true;
            this.nt_purRetval.Width = 150;
            // 
            // nt_cloqty
            // 
            this.nt_cloqty.DataPropertyName = "nt_cloqty";
            this.nt_cloqty.HeaderText = "Clos Qty";
            this.nt_cloqty.Name = "nt_cloqty";
            this.nt_cloqty.ReadOnly = true;
            this.nt_cloqty.Width = 150;
            // 
            // tot
            // 
            this.tot.DataPropertyName = "tot";
            this.tot.HeaderText = "Clos Val";
            this.tot.Name = "tot";
            this.tot.ReadOnly = true;
            this.tot.Width = 150;
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.txtitemname);
            this.Pnl_Header.Controls.Add(this.label1);
            this.Pnl_Header.Location = new System.Drawing.Point(-1, 0);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 47);
            this.Pnl_Header.TabIndex = 2;
            // 
            // txtitemname
            // 
            this.txtitemname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtitemname.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitemname.Location = new System.Drawing.Point(318, 10);
            this.txtitemname.Name = "txtitemname";
            this.txtitemname.Size = new System.Drawing.Size(685, 29);
            this.txtitemname.TabIndex = 1;
            this.txtitemname.TextChanged += new System.EventHandler(this.txtitemname1_TextChanged);
            this.txtitemname.Enter += new System.EventHandler(this.txtitemname_Enter);
            this.txtitemname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyDown);
            this.txtitemname.Leave += new System.EventHandler(this.txtitemname_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(58, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Monthly Stock Breake Up Of";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.Exit);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 545);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 45);
            this.Pnl_Footer.TabIndex = 4;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(1097, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(76, 38);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "&Graph";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.White;
            this.Exit.ForeColor = System.Drawing.Color.Black;
            this.Exit.Location = new System.Drawing.Point(922, 3);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(76, 38);
            this.Exit.TabIndex = 5;
            this.Exit.Text = "&Exit";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // listview
            // 
            this.listview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listview.FormattingEnabled = true;
            this.listview.ItemHeight = 20;
            this.listview.Location = new System.Drawing.Point(6, 38);
            this.listview.Name = "listview";
            this.listview.Size = new System.Drawing.Size(691, 304);
            this.listview.TabIndex = 5;
            this.listview.Click += new System.EventHandler(this.listview_Click);
            // 
            // pnlitems
            // 
            this.pnlitems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlitems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlitems.Controls.Add(this.label2);
            this.pnlitems.Controls.Add(this.listview);
            this.pnlitems.Location = new System.Drawing.Point(310, 51);
            this.pnlitems.Name = "pnlitems";
            this.pnlitems.Size = new System.Drawing.Size(703, 350);
            this.pnlitems.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(316, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "List Of Items";
            // 
            // MonthlystockBreakeUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.pnlitems);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.DgMonthlyStockRpt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MonthlystockBreakeUp";
            this.Text = "MonthlystockBreakeUp";
            this.Load += new System.EventHandler(this.MonthlystockBreakeUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgMonthlyStockRpt)).EndInit();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.pnlitems.ResumeLayout(false);
            this.pnlitems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DgMonthlyStockRpt;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.TextBox txtitemname;
        private System.Windows.Forms.Label label1;
       // private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.DataGridViewTextBoxColumn nq_purqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_purval;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_prqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_purRetval;
        private System.Windows.Forms.DataGridViewTextBoxColumn nt_cloqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn tot;
        private System.Windows.Forms.ListBox listview;
        private System.Windows.Forms.Panel pnlitems;
        private System.Windows.Forms.Label label2;
    }
}