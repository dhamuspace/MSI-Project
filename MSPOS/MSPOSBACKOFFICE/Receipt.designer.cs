namespace MSPOSBACKOFFICE
{
    partial class Receipt
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
            this.leftpane = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.btn_ext = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_testprint = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Pnl_Header1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_container = new System.Windows.Forms.Panel();
            this.lbl_selectdesc = new System.Windows.Forms.Label();
            this.pnl_inner = new System.Windows.Forms.Panel();
            this.collapsiblePanel3 = new MSPOSBACKOFFICE.CollapsiblePanel();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prop = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.collapsiblePanel4 = new MSPOSBACKOFFICE.CollapsiblePanel();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.RDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rprop = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.collapsiblePanel2 = new MSPOSBACKOFFICE.CollapsiblePanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.cDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cProp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.leftpane.SuspendLayout();
            this.Pnl_Header.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Pnl_Header1.SuspendLayout();
            this.pnl_container.SuspendLayout();
            this.pnl_inner.SuspendLayout();
            this.collapsiblePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.collapsiblePanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.collapsiblePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.Pnl_Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftpane
            // 
            this.leftpane.BackColor = System.Drawing.Color.DimGray;
            this.leftpane.Controls.Add(this.panel2);
            this.leftpane.Controls.Add(this.comboBox1);
            this.leftpane.Controls.Add(this.label1);
            this.leftpane.Location = new System.Drawing.Point(2, -1);
            this.leftpane.Name = "leftpane";
            this.leftpane.Size = new System.Drawing.Size(209, 546);
            this.leftpane.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 207);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Barcode Scanner",
            "Receipt Printer",
            "Prep Printer",
            "Card Printer",
            "Cash Drawer"});
            this.comboBox1.Location = new System.Drawing.Point(4, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 28);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Device";
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.BackColor = System.Drawing.Color.Transparent;
            this.Pnl_Header.Controls.Add(this.btn_ext);
            this.Pnl_Header.Controls.Add(this.panel4);
            this.Pnl_Header.Controls.Add(this.panel3);
            this.Pnl_Header.Controls.Add(this.btn_testprint);
            this.Pnl_Header.Controls.Add(this.checkBox1);
            this.Pnl_Header.Controls.Add(this.Pnl_Header1);
            this.Pnl_Header.ForeColor = System.Drawing.Color.White;
            this.Pnl_Header.Location = new System.Drawing.Point(206, 1);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1053, 100);
            this.Pnl_Header.TabIndex = 5;
            // 
            // btn_ext
            // 
            this.btn_ext.Location = new System.Drawing.Point(1034, -1);
            this.btn_ext.Name = "btn_ext";
            this.btn_ext.Size = new System.Drawing.Size(20, 24);
            this.btn_ext.TabIndex = 6;
            this.btn_ext.UseVisualStyleBackColor = true;
            this.btn_ext.Visible = false;
            this.btn_ext.Click += new System.EventHandler(this.btn_ext_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Location = new System.Drawing.Point(5, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(94, 90);
            this.panel4.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(8, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(99, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(956, 10);
            this.panel3.TabIndex = 4;
            // 
            // btn_testprint
            // 
            this.btn_testprint.BackColor = System.Drawing.SystemColors.Control;
            this.btn_testprint.ForeColor = System.Drawing.Color.Black;
            this.btn_testprint.Location = new System.Drawing.Point(105, 62);
            this.btn_testprint.Name = "btn_testprint";
            this.btn_testprint.Size = new System.Drawing.Size(78, 23);
            this.btn_testprint.TabIndex = 3;
            this.btn_testprint.Text = "Test Receipt";
            this.btn_testprint.UseVisualStyleBackColor = false;
            this.btn_testprint.Click += new System.EventHandler(this.btn_testprint_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(201, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(225, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Use Default printer when a printer is offline";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Pnl_Header1
            // 
            this.Pnl_Header1.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Header1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header1.Controls.Add(this.label2);
            this.Pnl_Header1.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Header1.Name = "Pnl_Header1";
            this.Pnl_Header1.Size = new System.Drawing.Size(1056, 44);
            this.Pnl_Header1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(117, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Receipt Printer";
            // 
            // pnl_container
            // 
            this.pnl_container.BackColor = System.Drawing.Color.Transparent;
            this.pnl_container.Controls.Add(this.lbl_selectdesc);
            this.pnl_container.Controls.Add(this.pnl_inner);
            this.pnl_container.Controls.Add(this.panel1);
            this.pnl_container.Location = new System.Drawing.Point(212, 98);
            this.pnl_container.Name = "pnl_container";
            this.pnl_container.Size = new System.Drawing.Size(1049, 528);
            this.pnl_container.TabIndex = 6;
            // 
            // lbl_selectdesc
            // 
            this.lbl_selectdesc.AutoSize = true;
            this.lbl_selectdesc.Location = new System.Drawing.Point(22, 456);
            this.lbl_selectdesc.Name = "lbl_selectdesc";
            this.lbl_selectdesc.Size = new System.Drawing.Size(253, 13);
            this.lbl_selectdesc.TabIndex = 0;
            this.lbl_selectdesc.Text = "_________________________________________";
            // 
            // pnl_inner
            // 
            this.pnl_inner.BackColor = System.Drawing.Color.Transparent;
            this.pnl_inner.Controls.Add(this.collapsiblePanel3);
            this.pnl_inner.Controls.Add(this.collapsiblePanel4);
            this.pnl_inner.Controls.Add(this.collapsiblePanel2);
            this.pnl_inner.Location = new System.Drawing.Point(0, 3);
            this.pnl_inner.Name = "pnl_inner";
            this.pnl_inner.Size = new System.Drawing.Size(1044, 450);
            this.pnl_inner.TabIndex = 1;
            // 
            // collapsiblePanel3
            // 
            this.collapsiblePanel3.Controls.Add(this.dataGridView1);
            this.collapsiblePanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.collapsiblePanel3.ExpandedHeight = 146;
            this.collapsiblePanel3.Location = new System.Drawing.Point(0, 293);
            this.collapsiblePanel3.Name = "collapsiblePanel3";
            this.collapsiblePanel3.NextPanel = null;
            this.collapsiblePanel3.PanelTitle = "General Settings";
            this.collapsiblePanel3.Size = new System.Drawing.Size(1044, 166);
            this.collapsiblePanel3.TabIndex = 0;
            // 
            // btn_apply
            // 
            this.btn_apply.BackColor = System.Drawing.Color.White;
            this.btn_apply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_apply.Location = new System.Drawing.Point(852, 3);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(78, 36);
            this.btn_apply.TabIndex = 2;
            this.btn_apply.Text = "&Apply";
            this.btn_apply.UseVisualStyleBackColor = false;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.White;
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(928, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(78, 36);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "E&xit";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Desc,
            this.Prop});
            this.dataGridView1.Location = new System.Drawing.Point(-1, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.Size = new System.Drawing.Size(809, 146);
            this.dataGridView1.TabIndex = 4;
            // 
            // Desc
            // 
            this.Desc.HeaderText = "Description";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            // 
            // Prop
            // 
            this.Prop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Prop.HeaderText = "Properties";
            this.Prop.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.Prop.Name = "Prop";
            this.Prop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // collapsiblePanel4
            // 
            this.collapsiblePanel4.Controls.Add(this.dataGridView3);
            this.collapsiblePanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.collapsiblePanel4.ExpandedHeight = 128;
            this.collapsiblePanel4.Location = new System.Drawing.Point(0, 145);
            this.collapsiblePanel4.Name = "collapsiblePanel4";
            this.collapsiblePanel4.NextPanel = null;
            this.collapsiblePanel4.PanelTitle = "Receipt Settings";
            this.collapsiblePanel4.Size = new System.Drawing.Size(1044, 148);
            this.collapsiblePanel4.TabIndex = 6;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RDesc,
            this.Rprop});
            this.dataGridView3.Location = new System.Drawing.Point(0, 20);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidth = 50;
            this.dataGridView3.Size = new System.Drawing.Size(809, 128);
            this.dataGridView3.TabIndex = 6;
            // 
            // RDesc
            // 
            this.RDesc.HeaderText = "Description";
            this.RDesc.Name = "RDesc";
            this.RDesc.ReadOnly = true;
            // 
            // Rprop
            // 
            this.Rprop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Rprop.HeaderText = "Properties";
            this.Rprop.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.Rprop.Name = "Rprop";
            this.Rprop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Rprop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // collapsiblePanel2
            // 
            this.collapsiblePanel2.Controls.Add(this.dataGridView2);
            this.collapsiblePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.collapsiblePanel2.ExpandedHeight = 125;
            this.collapsiblePanel2.Location = new System.Drawing.Point(0, 0);
            this.collapsiblePanel2.Name = "collapsiblePanel2";
            this.collapsiblePanel2.NextPanel = null;
            this.collapsiblePanel2.PanelTitle = "Custom Text";
            this.collapsiblePanel2.Size = new System.Drawing.Size(1044, 145);
            this.collapsiblePanel2.TabIndex = 5;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDesc,
            this.cProp});
            this.dataGridView2.Location = new System.Drawing.Point(-1, 20);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 50;
            this.dataGridView2.Size = new System.Drawing.Size(809, 125);
            this.dataGridView2.TabIndex = 5;
            // 
            // cDesc
            // 
            this.cDesc.HeaderText = "Description";
            this.cDesc.Name = "cDesc";
            this.cDesc.ReadOnly = true;
            // 
            // cProp
            // 
            this.cProp.HeaderText = "Property";
            this.cProp.Name = "cProp";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(-1, 447);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1044, 0);
            this.panel1.TabIndex = 0;
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.BackColor = System.Drawing.Color.Olive;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btn_cancel);
            this.Pnl_Footer.Controls.Add(this.btn_apply);
            this.Pnl_Footer.Location = new System.Drawing.Point(0, 547);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1019, 44);
            this.Pnl_Footer.TabIndex = 3;
            // 
            // Receipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1019, 592);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.leftpane);
            this.Controls.Add(this.Pnl_Header);
            this.Controls.Add(this.pnl_container);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Receipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "f";
            this.Load += new System.EventHandler(this.Receipt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Receipt_KeyDown);
            this.leftpane.ResumeLayout(false);
            this.leftpane.PerformLayout();
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Pnl_Header1.ResumeLayout(false);
            this.Pnl_Header1.PerformLayout();
            this.pnl_container.ResumeLayout(false);
            this.pnl_container.PerformLayout();
            this.pnl_inner.ResumeLayout(false);
            this.collapsiblePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.collapsiblePanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.collapsiblePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.Pnl_Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel leftpane;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Button btn_ext;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_testprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel pnl_container;
        private System.Windows.Forms.Panel pnl_inner;
        private System.Windows.Forms.Label lbl_selectdesc;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.Button btn_cancel;
        private MSPOSBACKOFFICE.CollapsiblePanel collapsiblePanel3;
        private MSPOSBACKOFFICE.CollapsiblePanel collapsiblePanel4;
        private System.Windows.Forms.DataGridView dataGridView3;
     // private StaffDotNet.CollapsiblePanel.CollapsiblePanel collapsiblePanel2;
        private MSPOSBACKOFFICE.CollapsiblePanel collapsiblePanel2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desc;
        private System.Windows.Forms.DataGridViewComboBoxColumn Prop;
        private System.Windows.Forms.DataGridViewTextBoxColumn RDesc;
        private System.Windows.Forms.DataGridViewComboBoxColumn Rprop;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cProp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Pnl_Header1;
    }
}

