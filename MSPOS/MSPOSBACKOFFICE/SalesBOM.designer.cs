namespace MSPOSBACKOFFICE
{
    partial class SalesBOM
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.lbloutputval = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblinputval = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblinputQty = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbloutputqty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.txtBomName = new System.Windows.Forms.TextBox();
            this.txtLabourcharge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DgBomsEntry = new DataGridNameSpace.MyDataGrid();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBomsEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Pnl_Footer);
            this.panel1.Controls.Add(this.Pnl_Header);
            this.panel1.Controls.Add(this.DgBomsEntry);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1263, 624);
            this.panel1.TabIndex = 1;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.lbloutputval);
            this.Pnl_Footer.Controls.Add(this.label4);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Controls.Add(this.lblinputval);
            this.Pnl_Footer.Controls.Add(this.btnSave);
            this.Pnl_Footer.Controls.Add(this.lblinputQty);
            this.Pnl_Footer.Controls.Add(this.label9);
            this.Pnl_Footer.Controls.Add(this.label6);
            this.Pnl_Footer.Controls.Add(this.lbloutputqty);
            this.Pnl_Footer.Controls.Add(this.label8);
            this.Pnl_Footer.Location = new System.Drawing.Point(-24, 537);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1262, 51);
            this.Pnl_Footer.TabIndex = 6;
            // 
            // lbloutputval
            // 
            this.lbloutputval.AutoSize = true;
            this.lbloutputval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbloutputval.ForeColor = System.Drawing.Color.White;
            this.lbloutputval.Location = new System.Drawing.Point(770, 18);
            this.lbloutputval.Name = "lbloutputval";
            this.lbloutputval.Size = new System.Drawing.Size(36, 18);
            this.lbloutputval.TabIndex = 14;
            this.lbloutputval.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(27, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Input Qty";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(948, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(87, 44);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblinputval
            // 
            this.lblinputval.AutoSize = true;
            this.lblinputval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinputval.ForeColor = System.Drawing.Color.White;
            this.lblinputval.Location = new System.Drawing.Point(278, 18);
            this.lblinputval.Name = "lblinputval";
            this.lblinputval.Size = new System.Drawing.Size(36, 18);
            this.lblinputval.TabIndex = 16;
            this.lblinputval.Text = "0.00";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(859, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 44);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblinputQty
            // 
            this.lblinputQty.AutoSize = true;
            this.lblinputQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinputQty.ForeColor = System.Drawing.Color.White;
            this.lblinputQty.Location = new System.Drawing.Point(99, 18);
            this.lblinputQty.Name = "lblinputQty";
            this.lblinputQty.Size = new System.Drawing.Size(36, 18);
            this.lblinputQty.TabIndex = 10;
            this.lblinputQty.Text = "0.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(203, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Input Val";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(459, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Output Qty";
            // 
            // lbloutputqty
            // 
            this.lbloutputqty.AutoSize = true;
            this.lbloutputqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbloutputqty.ForeColor = System.Drawing.Color.White;
            this.lbloutputqty.Location = new System.Drawing.Point(544, 18);
            this.lbloutputqty.Name = "lbloutputqty";
            this.lbloutputqty.Size = new System.Drawing.Size(36, 18);
            this.lbloutputqty.TabIndex = 12;
            this.lbloutputqty.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(686, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "OutPut Val";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.txtBomName);
            this.Pnl_Header.Controls.Add(this.txtLabourcharge);
            this.Pnl_Header.Controls.Add(this.label2);
            this.Pnl_Header.Controls.Add(this.label3);
            this.Pnl_Header.Location = new System.Drawing.Point(0, 36);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1019, 48);
            this.Pnl_Header.TabIndex = 17;
            // 
            // txtBomName
            // 
            this.txtBomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBomName.Location = new System.Drawing.Point(107, 17);
            this.txtBomName.Name = "txtBomName";
            this.txtBomName.Size = new System.Drawing.Size(235, 23);
            this.txtBomName.TabIndex = 1;
            this.txtBomName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBomName_KeyDown);
            this.txtBomName.Leave += new System.EventHandler(this.txtBomName_Leave);
            // 
            // txtLabourcharge
            // 
            this.txtLabourcharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabourcharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLabourcharge.Location = new System.Drawing.Point(486, 17);
            this.txtLabourcharge.Name = "txtLabourcharge";
            this.txtLabourcharge.Size = new System.Drawing.Size(235, 23);
            this.txtLabourcharge.TabIndex = 2;
            this.txtLabourcharge.Enter += new System.EventHandler(this.lblLabourCharge_Enter);
            this.txtLabourcharge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lblLabourCharge_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(356, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Labour Charges";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "BOM Name";
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
            this.ItemCode,
            this.ItemNames,
            this.Unit,
            this.Type,
            this.TaxQty,
            this.Qty,
            this.Rate,
            this.Amount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgBomsEntry.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgBomsEntry.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgBomsEntry.Location = new System.Drawing.Point(7, 89);
            this.DgBomsEntry.Name = "DgBomsEntry";
            this.DgBomsEntry.RowHeadersVisible = false;
            this.DgBomsEntry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgBomsEntry.Size = new System.Drawing.Size(1002, 443);
            this.DgBomsEntry.TabIndex = 6;
            this.DgBomsEntry.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellEndEdit);
            this.DgBomsEntry.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellEnter);
            this.DgBomsEntry.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellLeave);
            this.DgBomsEntry.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBomsEntry_CellValueChanged);
            this.DgBomsEntry.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgBomsEntry_EditingControlShowing);
            this.DgBomsEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgBomsEntry_KeyDown);
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            // 
            // ItemNames
            // 
            this.ItemNames.DataPropertyName = "ItemNames";
            this.ItemNames.HeaderText = "ItemName";
            this.ItemNames.Name = "ItemNames";
            this.ItemNames.Width = 290;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // TaxQty
            // 
            this.TaxQty.DataPropertyName = "TaxQty";
            this.TaxQty.HeaderText = "TaxQty";
            this.TaxQty.Name = "TaxQty";
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.Location = new System.Drawing.Point(-1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "BOM Master Creation";
            // 
            // SalesBOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesBOM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SalesPOM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SalesBOM_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.Pnl_Footer.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBomsEntry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLabourcharge;
        private System.Windows.Forms.TextBox txtBomName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DataGridNameSpace.MyDataGrid DgBomsEntry;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Label lblinputval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbloutputval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbloutputqty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblinputQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}