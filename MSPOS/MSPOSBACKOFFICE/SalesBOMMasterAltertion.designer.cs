namespace MSPOSBACKOFFICE
{
    partial class SalesBOMMasterAltertion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Pnl_Back = new System.Windows.Forms.Panel();
            this.DgBomsEntry = new DataGridNameSpace.MyDataGrid();
            this.BomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOM_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnKill = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbomstyle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Back.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBomsEntry)).BeginInit();
            this.Pnl_Footer.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Back
            // 
            this.Pnl_Back.Controls.Add(this.DgBomsEntry);
            this.Pnl_Back.Controls.Add(this.Pnl_Footer);
            this.Pnl_Back.Controls.Add(this.Pnl_Header);
            this.Pnl_Back.Controls.Add(this.label1);
            this.Pnl_Back.Location = new System.Drawing.Point(0, 3);
            this.Pnl_Back.Name = "Pnl_Back";
            this.Pnl_Back.Size = new System.Drawing.Size(1019, 590);
            this.Pnl_Back.TabIndex = 0;
            // 
            // DgBomsEntry
            // 
            this.DgBomsEntry.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgBomsEntry.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgBomsEntry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgBomsEntry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgBomsEntry.ColumnHeadersHeight = 38;
            this.DgBomsEntry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BomName,
            this.ItemNames,
            this.Unit,
            this.Type,
            this.TaxQty,
            this.Qty,
            this.Rate,
            this.Amount,
            this.BOM_No});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgBomsEntry.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgBomsEntry.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgBomsEntry.Location = new System.Drawing.Point(0, 83);
            this.DgBomsEntry.Name = "DgBomsEntry";
            this.DgBomsEntry.ReadOnly = true;
            this.DgBomsEntry.RowHeadersVisible = false;
            this.DgBomsEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgBomsEntry.Size = new System.Drawing.Size(1018, 455);
            this.DgBomsEntry.TabIndex = 11;
            this.DgBomsEntry.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellClick);
            this.DgBomsEntry.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellDoubleClick);
            // 
            // BomName
            // 
            this.BomName.DataPropertyName = "BomName";
            this.BomName.FillWeight = 64.62037F;
            this.BomName.HeaderText = "BOM Name";
            this.BomName.Name = "BomName";
            this.BomName.ReadOnly = true;
            // 
            // ItemNames
            // 
            this.ItemNames.DataPropertyName = "ItemNames";
            this.ItemNames.FillWeight = 284.3158F;
            this.ItemNames.HeaderText = "ItemName";
            this.ItemNames.Name = "ItemNames";
            this.ItemNames.ReadOnly = true;
            this.ItemNames.Width = 210;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.FillWeight = 59.79155F;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 93;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.FillWeight = 66.64021F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 103;
            // 
            // TaxQty
            // 
            this.TaxQty.DataPropertyName = "TaxQty";
            this.TaxQty.FillWeight = 72.93564F;
            this.TaxQty.HeaderText = "TaxQty";
            this.TaxQty.Name = "TaxQty";
            this.TaxQty.ReadOnly = true;
            this.TaxQty.Width = 112;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.FillWeight = 78.72257F;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 122;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.FillWeight = 84.04206F;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 130;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.FillWeight = 88.93185F;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 138;
            // 
            // BOM_No
            // 
            this.BOM_No.DataPropertyName = "BOM_No";
            this.BOM_No.HeaderText = "BOM_No";
            this.BOM_No.Name = "BOM_No";
            this.BOM_No.ReadOnly = true;
            this.BOM_No.Visible = false;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnprint);
            this.Pnl_Footer.Controls.Add(this.btnKill);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.BtnAdd);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 539);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 49);
            this.Pnl_Footer.TabIndex = 10;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.White;
            this.btnprint.ForeColor = System.Drawing.Color.Black;
            this.btnprint.Location = new System.Drawing.Point(11, 6);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(77, 38);
            this.btnprint.TabIndex = 15;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnKill
            // 
            this.btnKill.BackColor = System.Drawing.Color.White;
            this.btnKill.ForeColor = System.Drawing.Color.Black;
            this.btnKill.Location = new System.Drawing.Point(842, 5);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(77, 38);
            this.btnKill.TabIndex = 14;
            this.btnKill.Text = "Kill";
            this.btnKill.UseVisualStyleBackColor = false;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(919, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(76, 38);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.White;
            this.BtnAdd.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Location = new System.Drawing.Point(765, 5);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(77, 38);
            this.BtnAdd.TabIndex = 12;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Controls.Add(this.txtbomstyle);
            this.Pnl_Header.Location = new System.Drawing.Point(1, 36);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 47);
            this.Pnl_Header.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Style";
            // 
            // txtbomstyle
            // 
            this.txtbomstyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbomstyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtbomstyle.Location = new System.Drawing.Point(54, 14);
            this.txtbomstyle.Name = "txtbomstyle";
            this.txtbomstyle.ReadOnly = true;
            this.txtbomstyle.Size = new System.Drawing.Size(295, 23);
            this.txtbomstyle.TabIndex = 0;
            this.txtbomstyle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbomstyle_KeyDown);
            this.txtbomstyle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtbomstyle_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "BOM Master Display";
            // 
            // SalesBOMMasterAltertion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Back);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesBOMMasterAltertion";
            this.Text = "BOMMasterAltertion";
            this.Load += new System.EventHandler(this.SalesBOMMasterAltertion_Load);
            this.Pnl_Back.ResumeLayout(false);
            this.Pnl_Back.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBomsEntry)).EndInit();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Back;
        private DataGridNameSpace.MyDataGrid DgBomsEntry;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbomstyle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.DataGridViewTextBoxColumn BomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOM_No;
        private System.Windows.Forms.Button btnprint;

    }
}