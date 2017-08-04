namespace SalesProject
{
    partial class FrmTIcketIssue
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Pnl_Header = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTicketNo = new System.Windows.Forms.Label();
            this.Pnl_Footer = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.cmbTicketNo = new System.Windows.Forms.ComboBox();
            this.lblTktNo = new System.Windows.Forms.Label();
            this.dtpModifiedDate = new System.Windows.Forms.DateTimePicker();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSum = new System.Windows.Forms.Button();
            this.lbleDate = new System.Windows.Forms.Label();
            this.dateTimeBox = new System.Windows.Forms.DateTimePicker();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDeposit = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNoteLine2 = new System.Windows.Forms.TextBox();
            this.txtNoteLine1 = new System.Windows.Forms.TextBox();
            this.txtNoteLine3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtServiceBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJobNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.GrdTicketIsue = new DataGridNameSpace.MyDataGrid();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstimatePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Pnl_Header.SuspendLayout();
            this.Pnl_Footer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdTicketIsue)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Header
            // 
            this.Pnl_Header.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Pnl_Header.BackColor = System.Drawing.Color.White;
            this.Pnl_Header.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Header.Controls.Add(this.label19);
            this.Pnl_Header.Controls.Add(this.label13);
            this.Pnl_Header.Controls.Add(this.btnSearch);
            this.Pnl_Header.Controls.Add(this.txtSearch);
            this.Pnl_Header.Controls.Add(this.lblTicketNo);
            this.Pnl_Header.Location = new System.Drawing.Point(3, 45);
            this.Pnl_Header.Name = "Pnl_Header";
            this.Pnl_Header.Size = new System.Drawing.Size(1020, 43);
            this.Pnl_Header.TabIndex = 50;
            this.Pnl_Header.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_Header_Paint);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(769, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 20);
            this.label19.TabIndex = 97;
            this.label19.Text = "Search";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(402, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 22);
            this.label13.TabIndex = 82;
            this.label13.Text = "Ticket No";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(661, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 38);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Text = "Display";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(837, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(173, 22);
            this.txtSearch.TabIndex = 22;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblTicketNo
            // 
            this.lblTicketNo.AutoSize = true;
            this.lblTicketNo.Font = new System.Drawing.Font("Baskerville Old Face", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketNo.ForeColor = System.Drawing.Color.Black;
            this.lblTicketNo.Location = new System.Drawing.Point(500, 9);
            this.lblTicketNo.Name = "lblTicketNo";
            this.lblTicketNo.Size = new System.Drawing.Size(50, 22);
            this.lblTicketNo.TabIndex = 81;
            this.lblTicketNo.Text = "TNo";
            // 
            // Pnl_Footer
            // 
            this.Pnl_Footer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Pnl_Footer.BackColor = System.Drawing.Color.White;
            this.Pnl_Footer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Footer.Controls.Add(this.btnClear);
            this.Pnl_Footer.Controls.Add(this.btnSave);
            this.Pnl_Footer.Controls.Add(this.btnPrint);
            this.Pnl_Footer.Controls.Add(this.btnExit);
            this.Pnl_Footer.Location = new System.Drawing.Point(3, 719);
            this.Pnl_Footer.Name = "Pnl_Footer";
            this.Pnl_Footer.Size = new System.Drawing.Size(1020, 47);
            this.Pnl_Footer.TabIndex = 51;
            this.Pnl_Footer.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_Footer_Paint);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(681, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 38);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(570, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 38);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.White;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(903, 3);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(102, 38);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(792, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 38);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtNotes);
            this.panel1.Controls.Add(this.txtAddress);
            this.panel1.Controls.Add(this.cmbTicketNo);
            this.panel1.Controls.Add(this.lblTktNo);
            this.panel1.Controls.Add(this.dtpModifiedDate);
            this.panel1.Controls.Add(this.txtDate);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnSum);
            this.panel1.Controls.Add(this.GrdTicketIsue);
            this.panel1.Controls.Add(this.lbleDate);
            this.panel1.Controls.Add(this.dateTimeBox);
            this.panel1.Controls.Add(this.txtBalance);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtDeposit);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtAmount);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtServiceBy);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBillNo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtTNo);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtJobNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtContactNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtEmailID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblAdd);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 629);
            this.panel1.TabIndex = 52;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtAddress1);
            this.panel2.Controls.Add(this.txtAddress3);
            this.panel2.Controls.Add(this.txtAddress2);
            this.panel2.Location = new System.Drawing.Point(184, 284);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(414, 64);
            this.panel2.TabIndex = 110;
            this.panel2.Visible = false;
            // 
            // txtAddress1
            // 
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAddress1.Location = new System.Drawing.Point(3, 4);
            this.txtAddress1.MaxLength = 40;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(402, 16);
            this.txtAddress1.TabIndex = 2356;
            this.txtAddress1.Visible = false;
            this.txtAddress1.WordWrap = false;
            this.txtAddress1.TextChanged += new System.EventHandler(this.txtAddress1_TextChanged);
            this.txtAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress1_KeyDown);
            this.txtAddress1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddress1_KeyPress);
            this.txtAddress1.Leave += new System.EventHandler(this.txtAddress1_Leave);
            // 
            // txtAddress3
            // 
            this.txtAddress3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAddress3.Location = new System.Drawing.Point(3, 40);
            this.txtAddress3.MaxLength = 40;
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(402, 16);
            this.txtAddress3.TabIndex = 5456465;
            this.txtAddress3.Visible = false;
            this.txtAddress3.WordWrap = false;
            this.txtAddress3.TextChanged += new System.EventHandler(this.txtAddress3_TextChanged);
            this.txtAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress3_KeyDown);
            this.txtAddress3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddress3_KeyPress);
            this.txtAddress3.Leave += new System.EventHandler(this.txtAddress3_Leave);
            // 
            // txtAddress2
            // 
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAddress2.Location = new System.Drawing.Point(3, 22);
            this.txtAddress2.MaxLength = 40;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(402, 16);
            this.txtAddress2.TabIndex = 45646;
            this.txtAddress2.Visible = false;
            this.txtAddress2.WordWrap = false;
            this.txtAddress2.TextChanged += new System.EventHandler(this.txtAddress2_TextChanged);
            this.txtAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress2_KeyDown);
            this.txtAddress2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddress2_KeyPress);
            this.txtAddress2.Leave += new System.EventHandler(this.txtAddress2_Leave);
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(111, 525);
            this.txtNotes.MaxLength = 120;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(414, 98);
            this.txtNotes.TabIndex = 8;
            this.txtNotes.TextChanged += new System.EventHandler(this.txtNotes_TextChanged);
            this.txtNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNotes_KeyDown);
            this.txtNotes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNotes_KeyPress);
            this.txtNotes.Leave += new System.EventHandler(this.txtNotes_Leave);
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(111, 36);
            this.txtAddress.MaxLength = 120;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(414, 93);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            this.txtAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddress_KeyPress);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            // 
            // cmbTicketNo
            // 
            this.cmbTicketNo.FormattingEnabled = true;
            this.cmbTicketNo.Location = new System.Drawing.Point(920, 8);
            this.cmbTicketNo.Name = "cmbTicketNo";
            this.cmbTicketNo.Size = new System.Drawing.Size(90, 21);
            this.cmbTicketNo.TabIndex = 109;
            this.cmbTicketNo.Visible = false;
            this.cmbTicketNo.SelectedIndexChanged += new System.EventHandler(this.cmbTicketNo_SelectedIndexChanged);
            // 
            // lblTktNo
            // 
            this.lblTktNo.AutoSize = true;
            this.lblTktNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTktNo.Location = new System.Drawing.Point(835, 9);
            this.lblTktNo.Name = "lblTktNo";
            this.lblTktNo.Size = new System.Drawing.Size(84, 20);
            this.lblTktNo.TabIndex = 108;
            this.lblTktNo.Text = "Ticket No";
            this.lblTktNo.Visible = false;
            // 
            // dtpModifiedDate
            // 
            this.dtpModifiedDate.CustomFormat = "dd/MM/yyyy";
            this.dtpModifiedDate.Enabled = false;
            this.dtpModifiedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpModifiedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpModifiedDate.Location = new System.Drawing.Point(837, 129);
            this.dtpModifiedDate.Name = "dtpModifiedDate";
            this.dtpModifiedDate.Size = new System.Drawing.Size(132, 29);
            this.dtpModifiedDate.TabIndex = 106;
            this.dtpModifiedDate.Visible = false;
            // 
            // txtDate
            // 
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDate.Enabled = false;
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Location = new System.Drawing.Point(662, 8);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(167, 27);
            this.txtDate.TabIndex = 104;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(854, 168);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(115, 16);
            this.label17.TabIndex = 103;
            this.label17.Text = "Must Enter Values";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(9, 602);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 20);
            this.label16.TabIndex = 102;
            this.label16.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(969, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(16, 20);
            this.label14.TabIndex = 100;
            this.label14.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(528, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 20);
            this.label8.TabIndex = 98;
            this.label8.Text = "*";
            // 
            // btnSum
            // 
            this.btnSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSum.BackColor = System.Drawing.Color.White;
            this.btnSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSum.ForeColor = System.Drawing.Color.Black;
            this.btnSum.Location = new System.Drawing.Point(904, 528);
            this.btnSum.Margin = new System.Windows.Forms.Padding(4);
            this.btnSum.Name = "btnSum";
            this.btnSum.Size = new System.Drawing.Size(100, 38);
            this.btnSum.TabIndex = 97;
            this.btnSum.Text = "Sum";
            this.btnSum.UseVisualStyleBackColor = false;
            this.btnSum.Visible = false;
            this.btnSum.Click += new System.EventHandler(this.btnSum_Click);
            // 
            // lbleDate
            // 
            this.lbleDate.AutoSize = true;
            this.lbleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbleDate.Location = new System.Drawing.Point(563, 9);
            this.lbleDate.Name = "lbleDate";
            this.lbleDate.Size = new System.Drawing.Size(48, 20);
            this.lbleDate.TabIndex = 96;
            this.lbleDate.Text = "Date";
            // 
            // dateTimeBox
            // 
            this.dateTimeBox.CustomFormat = "dd/MM/yyyy";
            this.dateTimeBox.Enabled = false;
            this.dateTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeBox.Location = new System.Drawing.Point(762, 130);
            this.dateTimeBox.Name = "dateTimeBox";
            this.dateTimeBox.Size = new System.Drawing.Size(132, 29);
            this.dateTimeBox.TabIndex = 95;
            this.dateTimeBox.Visible = false;
            // 
            // txtBalance
            // 
            this.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalance.Enabled = false;
            this.txtBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalance.Location = new System.Drawing.Point(715, 587);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(177, 22);
            this.txtBalance.TabIndex = 14;
            this.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(628, 592);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.TabIndex = 94;
            this.label10.Text = "Balance";
            // 
            // txtDeposit
            // 
            this.txtDeposit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeposit.Location = new System.Drawing.Point(715, 557);
            this.txtDeposit.Name = "txtDeposit";
            this.txtDeposit.Size = new System.Drawing.Size(177, 22);
            this.txtDeposit.TabIndex = 9;
            this.txtDeposit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeposit.TextChanged += new System.EventHandler(this.txtDeposit_TextChanged);
            this.txtDeposit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeposit_KeyDown);
            this.txtDeposit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeposit_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(628, 562);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 20);
            this.label11.TabIndex = 92;
            this.label11.Text = "Deposit";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Enabled = false;
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(715, 528);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(177, 22);
            this.txtAmount.TabIndex = 12;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(628, 530);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 20);
            this.label12.TabIndex = 90;
            this.label12.Text = "Amount";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtNoteLine2);
            this.panel3.Controls.Add(this.txtNoteLine1);
            this.panel3.Controls.Add(this.txtNoteLine3);
            this.panel3.Location = new System.Drawing.Point(73, 384);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(414, 59);
            this.panel3.TabIndex = 14;
            this.panel3.Visible = false;
            // 
            // txtNoteLine2
            // 
            this.txtNoteLine2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoteLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNoteLine2.Location = new System.Drawing.Point(2, 22);
            this.txtNoteLine2.MaxLength = 40;
            this.txtNoteLine2.Name = "txtNoteLine2";
            this.txtNoteLine2.Size = new System.Drawing.Size(402, 16);
            this.txtNoteLine2.TabIndex = 16;
            this.txtNoteLine2.TextChanged += new System.EventHandler(this.txtNoteLine2_TextChanged);
            this.txtNoteLine2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoteLine2_KeyDown);
            this.txtNoteLine2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoteLine2_KeyPress);
            this.txtNoteLine2.Leave += new System.EventHandler(this.txtNoteLine2_Leave);
            // 
            // txtNoteLine1
            // 
            this.txtNoteLine1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoteLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNoteLine1.Location = new System.Drawing.Point(2, 4);
            this.txtNoteLine1.MaxLength = 40;
            this.txtNoteLine1.Name = "txtNoteLine1";
            this.txtNoteLine1.Size = new System.Drawing.Size(402, 16);
            this.txtNoteLine1.TabIndex = 15;
            this.txtNoteLine1.TextChanged += new System.EventHandler(this.txtNoteLine1_TextChanged);
            this.txtNoteLine1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoteLine1_KeyDown);
            this.txtNoteLine1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoteLine1_KeyPress);
            this.txtNoteLine1.Leave += new System.EventHandler(this.txtNoteLine1_Leave);
            // 
            // txtNoteLine3
            // 
            this.txtNoteLine3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoteLine3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNoteLine3.Location = new System.Drawing.Point(2, 40);
            this.txtNoteLine3.MaxLength = 40;
            this.txtNoteLine3.Name = "txtNoteLine3";
            this.txtNoteLine3.Size = new System.Drawing.Size(402, 16);
            this.txtNoteLine3.TabIndex = 17;
            this.txtNoteLine3.TextChanged += new System.EventHandler(this.txtNoteLine3_TextChanged);
            this.txtNoteLine3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoteLine3_KeyDown);
            this.txtNoteLine3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoteLine3_KeyPress);
            this.txtNoteLine3.Leave += new System.EventHandler(this.txtNoteLine3_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(11, 548);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 87;
            this.label9.Text = "Notes";
            // 
            // txtServiceBy
            // 
            this.txtServiceBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceBy.Location = new System.Drawing.Point(662, 100);
            this.txtServiceBy.Name = "txtServiceBy";
            this.txtServiceBy.Size = new System.Drawing.Size(305, 27);
            this.txtServiceBy.TabIndex = 6;
            this.txtServiceBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtServiceBy_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(563, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 85;
            this.label6.Text = "Service By";
            // 
            // txtBillNo
            // 
            this.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.Location = new System.Drawing.Point(662, 70);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(305, 27);
            this.txtBillNo.TabIndex = 5;
            this.txtBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNo_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(563, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 20);
            this.label7.TabIndex = 83;
            this.label7.Text = "Bill No";
            // 
            // txtTNo
            // 
            this.txtTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTNo.Enabled = false;
            this.txtTNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTNo.Location = new System.Drawing.Point(662, 38);
            this.txtTNo.Name = "txtTNo";
            this.txtTNo.Size = new System.Drawing.Size(305, 27);
            this.txtTNo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(563, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 81;
            this.label4.Text = "T. No";
            // 
            // txtJobNo
            // 
            this.txtJobNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJobNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobNo.Location = new System.Drawing.Point(632, 258);
            this.txtJobNo.Name = "txtJobNo";
            this.txtJobNo.Size = new System.Drawing.Size(305, 27);
            this.txtJobNo.TabIndex = 8;
            this.txtJobNo.Visible = false;
            this.txtJobNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtJobNo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(533, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 79;
            this.label5.Text = "Job No";
            this.label5.Visible = false;
            // 
            // txtContactNo
            // 
            this.txtContactNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNo.Location = new System.Drawing.Point(111, 163);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(414, 27);
            this.txtContactNo.TabIndex = 4;
            this.txtContactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactNo_KeyDown);
            this.txtContactNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(11, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 77;
            this.label3.Text = "Contact No";
            // 
            // txtEmailID
            // 
            this.txtEmailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailID.Location = new System.Drawing.Point(111, 133);
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.Size = new System.Drawing.Size(414, 27);
            this.txtEmailID.TabIndex = 3;
            this.txtEmailID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmailID_KeyDown);
            this.txtEmailID.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmailID_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 75;
            this.label2.Text = "E Mail";
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdd.ForeColor = System.Drawing.Color.Black;
            this.lblAdd.Location = new System.Drawing.Point(11, 61);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(75, 20);
            this.lblAdd.TabIndex = 45;
            this.lblAdd.Text = "Address";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(111, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(414, 27);
            this.txtName.TabIndex = 1;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Name";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label18);
            this.panel4.Location = new System.Drawing.Point(3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1020, 43);
            this.panel4.TabIndex = 53;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Algerian", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(381, 2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(232, 36);
            this.label18.TabIndex = 2;
            this.label18.Text = "Ticket Issue";
            // 
            // GrdTicketIsue
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.GrdTicketIsue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GrdTicketIsue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.GrdTicketIsue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrdTicketIsue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.Description,
            this.SNo,
            this.EstimatePrice,
            this.Status});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Baskerville Old Face", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GrdTicketIsue.DefaultCellStyle = dataGridViewCellStyle6;
            this.GrdTicketIsue.Location = new System.Drawing.Point(3, 200);
            this.GrdTicketIsue.Name = "GrdTicketIsue";
            this.GrdTicketIsue.Size = new System.Drawing.Size(1013, 320);
            this.GrdTicketIsue.TabIndex = 7;
            this.GrdTicketIsue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdTicketIsue_CellContentClick);
            this.GrdTicketIsue.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdTicketIsue_CellLeave);
            this.GrdTicketIsue.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.GrdTicketIsue_CellValidating);
            this.GrdTicketIsue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrdTicketIsue_CellValueChanged);
            this.GrdTicketIsue.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GrdTicketIsue_EditingControlShowing);
            this.GrdTicketIsue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GrdTicketIsue_KeyDown);
            this.GrdTicketIsue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GrdTicketIsue_KeyPress);
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.MaxInputLength = 100;
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 150;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MaxInputLength = 200;
            this.Description.Name = "Description";
            this.Description.Width = 500;
            // 
            // SNo
            // 
            this.SNo.HeaderText = "IMEI";
            this.SNo.MaxInputLength = 20;
            this.SNo.Name = "SNo";
            // 
            // EstimatePrice
            // 
            this.EstimatePrice.HeaderText = "Estimate Price";
            this.EstimatePrice.MaxInputLength = 8;
            this.EstimatePrice.Name = "EstimatePrice";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Items.AddRange(new object[] {
            "Pending",
            "Completed",
            "Delivered",
            "Cannot Repair"});
            this.Status.Name = "Status";
            this.Status.Width = 120;
            // 
            // FrmTIcketIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(1025, 768);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Pnl_Footer);
            this.Controls.Add(this.Pnl_Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTIcketIssue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmTIcketIssue";
            this.Load += new System.EventHandler(this.FrmTIcketIssue_Load);
            this.Pnl_Header.ResumeLayout(false);
            this.Pnl_Header.PerformLayout();
            this.Pnl_Footer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdTicketIsue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Header;
        private System.Windows.Forms.Panel Pnl_Footer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.TextBox txtEmailID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServiceBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJobNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTicketNo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtNoteLine2;
        private System.Windows.Forms.TextBox txtNoteLine1;
        private System.Windows.Forms.TextBox txtNoteLine3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDeposit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbleDate;
        private System.Windows.Forms.DateTimePicker dateTimeBox;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private DataGridNameSpace.MyDataGrid GrdTicketIsue;
        private System.Windows.Forms.Button btnSum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpModifiedDate;
        private System.Windows.Forms.Label lblTktNo;
        private System.Windows.Forms.ComboBox cmbTicketNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstimatePrice;
        private System.Windows.Forms.DataGridViewComboBoxColumn Status;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Panel panel2;
    }
}