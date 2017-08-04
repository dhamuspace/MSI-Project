namespace MSPOSBACKOFFICE
{
    partial class ItemImport
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
            this.pnl_message = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Ctrbanner = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.bntExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtpathlocation = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.DateGridPanel = new System.Windows.Forms.Panel();
            this.DgItemImport = new DataGridNameSpace.MyDataGrid();
            this.Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblformet = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnbrows = new System.Windows.Forms.Button();
            this.pnl_message.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DateGridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgItemImport)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_message
            // 
            this.pnl_message.BackColor = System.Drawing.Color.Olive;
            this.pnl_message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_message.Controls.Add(this.panel1);
            this.pnl_message.Controls.Add(this.lbl_Ctrbanner);
            this.pnl_message.Location = new System.Drawing.Point(3, 1);
            this.pnl_message.Name = "pnl_message";
            this.pnl_message.Size = new System.Drawing.Size(1001, 50);
            this.pnl_message.TabIndex = 36;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Olive;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 50);
            this.panel1.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Import";
            // 
            // lbl_Ctrbanner
            // 
            this.lbl_Ctrbanner.AutoSize = true;
            this.lbl_Ctrbanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Ctrbanner.ForeColor = System.Drawing.Color.White;
            this.lbl_Ctrbanner.Location = new System.Drawing.Point(12, 8);
            this.lbl_Ctrbanner.Name = "lbl_Ctrbanner";
            this.lbl_Ctrbanner.Size = new System.Drawing.Size(119, 25);
            this.lbl_Ctrbanner.TabIndex = 0;
            this.lbl_Ctrbanner.Text = "Item Import";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Olive;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.bntExit);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(1, 506);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1005, 50);
            this.panel2.TabIndex = 38;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUpdate.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnUpdate.Location = new System.Drawing.Point(780, 8);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(92, 33);
            this.btnUpdate.TabIndex = 44;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnImport.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnImport.Location = new System.Drawing.Point(682, 7);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(92, 34);
            this.btnImport.TabIndex = 43;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // bntExit
            // 
            this.bntExit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bntExit.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.bntExit.Location = new System.Drawing.Point(878, 7);
            this.bntExit.Name = "bntExit";
            this.bntExit.Size = new System.Drawing.Size(92, 34);
            this.bntExit.TabIndex = 42;
            this.bntExit.Text = "Exit";
            this.bntExit.UseVisualStyleBackColor = false;
            this.bntExit.Click += new System.EventHandler(this.bntExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(143, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 39;
            this.label3.Text = "Import File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(22, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 23);
            this.label4.TabIndex = 40;
            this.label4.Text = "Seperated By";
            // 
            // txtpathlocation
            // 
            this.txtpathlocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpathlocation.Location = new System.Drawing.Point(271, 69);
            this.txtpathlocation.Name = "txtpathlocation";
            this.txtpathlocation.Size = new System.Drawing.Size(471, 26);
            this.txtpathlocation.TabIndex = 41;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox1.Controls.Add(this.checkBox6);
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(32, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 383);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox6.Location = new System.Drawing.Point(27, 320);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(72, 20);
            this.checkBox6.TabIndex = 46;
            this.checkBox6.Text = "Others";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox5.Location = new System.Drawing.Point(30, 273);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(74, 22);
            this.checkBox5.TabIndex = 45;
            this.checkBox5.Text = "Space";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox4.Location = new System.Drawing.Point(30, 223);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(106, 20);
            this.checkBox4.TabIndex = 44;
            this.checkBox4.Text = "Semi Colon";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox3.Location = new System.Drawing.Point(30, 173);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(55, 20);
            this.checkBox3.TabIndex = 43;
            this.checkBox3.Text = "Tab";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox2.Location = new System.Drawing.Point(30, 126);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(79, 20);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Comma";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox1.Location = new System.Drawing.Point(30, 82);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 20);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Fixed Width";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // DateGridPanel
            // 
            this.DateGridPanel.BackColor = System.Drawing.Color.DarkGray;
            this.DateGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DateGridPanel.Controls.Add(this.DgItemImport);
            this.DateGridPanel.Controls.Add(this.comboBox1);
            this.DateGridPanel.Controls.Add(this.lblformet);
            this.DateGridPanel.Controls.Add(this.btnSave);
            this.DateGridPanel.Location = new System.Drawing.Point(271, 102);
            this.DateGridPanel.Name = "DateGridPanel";
            this.DateGridPanel.Size = new System.Drawing.Size(701, 383);
            this.DateGridPanel.TabIndex = 43;
            // 
            // DgItemImport
            // 
            this.DgItemImport.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgItemImport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgItemImport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DgItemImport.ColumnHeadersHeight = 38;
            this.DgItemImport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Particulars,
            this.Position});
            this.DgItemImport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DgItemImport.Location = new System.Drawing.Point(-1, 44);
            this.DgItemImport.Name = "DgItemImport";
            this.DgItemImport.RowHeadersVisible = false;
            this.DgItemImport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgItemImport.Size = new System.Drawing.Size(689, 279);
            this.DgItemImport.TabIndex = 47;
            this.DgItemImport.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgItemImport_EditingControlShowing);
            this.DgItemImport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgItemImport_KeyDown);
            this.DgItemImport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridDisplay_KeyPress);
            // 
            // Particulars
            // 
            this.Particulars.DataPropertyName = "Particulars";
            this.Particulars.HeaderText = "Particulars";
            this.Particulars.Name = "Particulars";
            this.Particulars.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Particulars.Width = 300;
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Posistion";
            this.Position.HeaderText = "Position";
            this.Position.Name = "Position";
            this.Position.Width = 175;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(132, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 28);
            this.comboBox1.TabIndex = 46;
            // 
            // lblformet
            // 
            this.lblformet.AutoSize = true;
            this.lblformet.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblformet.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblformet.Location = new System.Drawing.Point(38, 15);
            this.lblformet.Name = "lblformet";
            this.lblformet.Size = new System.Drawing.Size(60, 18);
            this.lblformet.TabIndex = 45;
            this.lblformet.Text = "Format";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSave.Location = new System.Drawing.Point(596, 345);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 35);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnbrows
            // 
            this.btnbrows.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnbrows.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbrows.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnbrows.Location = new System.Drawing.Point(748, 66);
            this.btnbrows.Name = "btnbrows";
            this.btnbrows.Size = new System.Drawing.Size(92, 33);
            this.btnbrows.TabIndex = 45;
            this.btnbrows.Text = "Browse";
            this.btnbrows.UseVisualStyleBackColor = false;
            this.btnbrows.Click += new System.EventHandler(this.btnbrows_Click);
            // 
            // ItemImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1006, 557);
            this.Controls.Add(this.btnbrows);
            this.Controls.Add(this.DateGridPanel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtpathlocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_message);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ItemImport";
            this.Text = "ItemImport";
            this.Load += new System.EventHandler(this.ItemImport_Load);
            this.pnl_message.ResumeLayout(false);
            this.pnl_message.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DateGridPanel.ResumeLayout(false);
            this.DateGridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgItemImport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_message;
        private System.Windows.Forms.Label lbl_Ctrbanner;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bntExit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtpathlocation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel DateGridPanel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblformet;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnbrows;
        private DataGridNameSpace.MyDataGrid DgItemImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
    }
}