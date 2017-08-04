using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace MSPOSBACKOFFICE
{
    public partial class FrmVoucherDisplay : Form
    {
        public FrmVoucherDisplay()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        
        public bool AmountTextBoxCreated = false;
        public bool LedgerTextBoxCreated = false;
        public bool LabelCrDrCreated = false;
        public bool LabelLedgerCreated = false;

        int AmtLblDr = 1;
        int AmtLblCr = 1;
        string vCurrBal = "";
        //public int NoofLedgerBox = 0;
        int inputNumber = 0;

        int x = 165;
        int y = 0;

        int i = 1; int d = 1;

        // Creating DR Amount Text Box //
        public void CreateAmountTextBox()
        {
            if (txtLNameBoxClicked == false)
            {
                if (AmountTextBoxCreated == true)
                {
                    vCrDr = "Cr";
                }
                else
                {
                    vCrDr = "Dr";
                }

                if (vCrDr == "Dr")
                {
                    if (AmountTextBoxCreated == true)
                    {
                        x = x + 40;
                        y = 830;
                        AmountTextBoxCreated = true;
                    }
                    else
                    {
                        x = 10;
                        y = 830;
                        AmountTextBoxCreated = true;
                    }

                    TextBox txtDrAmt = new TextBox();
                    txtDrAmt.KeyDown += new KeyEventHandler(txtDrAmt_KeyDown);

                    if (this.Width != 1152)
                    {
                        txtDrAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtDrAmt.Location = new System.Drawing.Point(y + (0 * 65), x);
                        txtDrAmt.Size = new Size(100, 100);
                        this.PnlLedger.Controls.Add(txtDrAmt);
                        this.ResumeLayout(false);
                        y += 100;
                        txtDrAmt.Name = "txtDrAmount" + d.ToString();
                        txtDrAmt.Text = "";
                        string value = this.PnlLedger.Controls["txtDrAmount" + d].Text;
                        txtDrAmt.TextAlign = HorizontalAlignment.Right;
                        d = d + 1;
                        this.Refresh();

                        txtDrAmt.Text = Convert.ToString(vDrTotal);
                        if (ctamt > vDrTotal)
                        {
                            txtDrAmt.Text = Convert.ToString(ctamt - vDrTotal);
                        }
                        this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Text = "";
                        this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Focus();

                    }
                }
                else
                {
                    if (AmountTextBoxCreated == true)
                    {
                        x = x + 50;
                        y = 990;
                    }
                    else
                    {
                        x = 10;
                        y = 990;
                        AmountTextBoxCreated = true;
                    }

                    TextBox txtAmt = new TextBox();
                    txtAmt.KeyDown += new KeyEventHandler(txtAmt_KeyDown);

                    if (this.Width != 1152)
                    {
                        txtAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtAmt.Location = new System.Drawing.Point(y + (0 * 65), x);
                        txtAmt.Size = new Size(100, 100);
                        this.PnlLedger.Controls.Add(txtAmt);
                        this.ResumeLayout(false);
                        y += 100;
                        txtAmt.Name = "txtAmount" + i.ToString();
                        txtAmt.Text = "";
                        string value = this.PnlLedger.Controls["txtAmount" + i].Text;
                        txtAmt.TextAlign = HorizontalAlignment.Right;
                        i = i + 1;
                        this.Refresh();
                        txtAmt.Text = Convert.ToString(vDrTotal);
                        if (vDrTotal > ctamt)
                        {
                            txtAmt.Text = Convert.ToString(vDrTotal - ctamt);
                        }
                        txtAmt.Focus();
                    }
                }
            }
            else
            {
                this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Text = "";
                this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Focus();
            }
        }

        int NoOfLedgerTextBox = 0;
        int NoOfDRAmtTextBox = 0;
        int NoOfCRAmtTextBox = 0;

        int txxt = 160, tyt = 100;
        int k = 1;
        TextBox DynamictxtBox = new TextBox();

        // Creating Ledger Text Box //

        public void CreateLedgerTextBox()
        {
            if (LedgerTextBoxCreated == true)
            {
                if (LabelLedgerCreated == true)
                {
                    CreateLedgerLabel();
                }
                txxt = txxt + 50;
                tyt = 100;
                LabelLedgerCreated = true;
            }
            else
            {
               // CreateLedgerLabel();
                txxt = 60;
                tyt = 100;
            }
           
            
            TextBox tb = new TextBox();
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            tb.Click += new EventHandler(tb_Click);           
            if (this.Width != 1152)
            {
                tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tb.Location = new System.Drawing.Point(tyt + (0 * 65), txxt);
                tb.Size = new Size(600, 100);

                this.PnlLedger.Controls.Add(tb);
                this.ResumeLayout(false);
                //y += 100;
                tb.Name = "txtDynamic" + k.ToString();
                string Values = this.PnlLedger.Controls["txtDynamic" + (k).ToString()].Text;
                this.Refresh();
                tb.Focus();
                //LstLoad();
                SqlCommand cmdchk = new SqlCommand("Select * from Ledger_table where Ledger_groupno in ('5','6') order by Ledger_groupno ", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmdchk);
                dt.Rows.Clear();
                lstLedgerName.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int s = 0; s < dt.Rows.Count; s++)
                    {
                        lstLedgerName.Items.Add(dt.Rows[s]["Ledger_name"].ToString());
                    }
                }
                pnllist.Visible = true;
                DynamictxtBox.Name = tb.Name.ToString();
                NoOfLedgerTextBox = k;
                k = k + 1;
                tb.BorderStyle = BorderStyle.None;
            }
        }

        int lblxx = 155;
        int lblyy = 97;

        // Passing Current Balance Value //

        public void CurrBalValue()
        {
            string cmd = "";
            if (this.Width != 1152)
            {
                Label lblLedger = new Label();
                lblLedger.Location = new System.Drawing.Point(lblyy, lblxx);
                lblLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblLedger.AutoSize = true;
                lblLedger.ForeColor = Color.Blue;
                {
                    if (vCrDr == "Cr")
                    {
                        if (i == 1)
                        {
                            cmd = " Select ledger_open,climit_amount from Ledger_table where ledger_name ='" + txtLedgerName.Text + "' ";
                        }
                        else
                        {
                            cmd = " Select ledger_open,climit_amount from Ledger_table where ledger_name ='" + this.PnlLedger.Controls["txtDynamic" + (i - 1)].Text + "' ";
                        }
                        //SqlCommand cmdlst = new SqlCommand(" Select ledger_open,climit_amount from Ledger_table where ledger_name ='" + this.PnlLedger.Controls["txtDynamic" + (i)].Text + "' ", con);
                        SqlCommand cmdlst = new SqlCommand(cmd, con);
                        SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                        DataTable dtlst = new DataTable();
                        dtlst.Rows.Clear();
                        con.Close();
                        adaptlst.Fill(dtlst);
                        if (dtlst.Rows.Count > 0)
                        {
                            string ChkVrCr = dtlst.Rows[0]["ledger_open"].ToString();

                            if (Convert.ToDouble(dtlst.Rows[0]["Ledger_open"].ToString().Trim()) < 0)
                            {
                                vCrDr = "Cr";
                            }
                            else
                            {
                                vCrDr = "Dr";
                            }

                            if (ChkVrCr != "")
                            {
                                lblLedger.Text = "Current Balnce : " + ChkVrCr + " " + vCrDr;
                            }
                            else
                            {
                                string Amt = dtlst.Rows[0]["climit_amount"].ToString();
                                if (Amt != "")
                                {
                                    lblLedger.Text = "Current Balnce : " + Amt + " " + vCrDr;
                                }
                            }
                        }


                    }
                    else if (vCrDr == "Dr")
                    {
                        SqlCommand cmdlst = new SqlCommand(" Select ledger_open,climit_amount from Ledger_table where ledger_name ='" + txtLedgerName.Text + "' ", con);
                        SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                        DataTable dtlst = new DataTable();
                        dtlst.Rows.Clear();
                        con.Close();
                        adaptlst.Fill(dtlst);
                        if (dtlst.Rows.Count > 0)
                        {
                            string ChkVrCr = dtlst.Rows[0]["ledger_open"].ToString();

                            if (Convert.ToDouble(dtlst.Rows[0]["Ledger_open"].ToString().Trim()) < 0)
                            {
                                vCrDr = "Cr";
                            }
                            else
                            {
                                vCrDr = "Dr";
                            }


                            string Amt = dtlst.Rows[0]["climit_amount"].ToString();
                            lblLedger.Text = "Current Balnce : " + Amt + " " + vCrDr;
                        }
                    }
                }

                if (txtLNameBoxClicked == false)
                {
                    //if (LabelLedgerCreated == false)
                    //{
                    this.PnlLedger.Controls.Add(lblLedger);
                    lblLedger.Name = "CurrBalLabel" + AmtLblDr.ToString();
                    this.Refresh();
                    vCurrBal = this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr).ToString()].Text;
                    AmtLblDr = AmtLblDr + 1;
                    //}
                }
                else
                {
                    this.PnlLedger.Controls.Add(lblLedger);
                    lblLedger.Name = "CurrBalLabel" + (AmtLblDr-1).ToString();
                    this.Refresh();
                    this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = "";
                    this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = lblLedger.Text;
                }
            }
        }
        
        // Creating Ledger Label  for ( Current Balance ) //
        
        public void CreateLedgerLabel()
        {
            if (txtLNameBoxClicked == false)
            {
                
                if (LabelLedgerCreated == true)
                {
                    lblxx = lblxx + 50;
                    LabelLedgerCreated = true;
                }
                else
                {
                    lblyy = 97;
                    lblxx = 35;
                    LabelLedgerCreated = true;
                }

                CurrBalValue();
            }
            else
            {
                CurrBalValue();
            }
        }

        //gpbCategoria.Controls.Remove(textbox4);
        //gpbCategoria.Controls.Remove(label4);

        // Creating CrDrLabel //

        int lblx = 165;
        int lbly = 20;
        public void CreateCrDrLabel()
        {
            if (LabelCrDrCreated == true)
            {
                lblx = lblx+50;
            }
            else
            {
                lbly = 22;
                lblx = 65;
            }
            
            if (this.Width != 1152)
            {
                Label lblCrDr = new Label();
                lblCrDr.Location = new System.Drawing.Point(lbly + (0 * 65), lblx);
                lblCrDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblCrDr.AutoSize = true;
                
                if (txtVoucherType.Text == "Payment")
                {
                    lblCrDr.Text = "Cr";
                    lblCrDr.ForeColor = Color.Red;
                    lblCrDr.BackColor = Color.Yellow;
                }
                else
                 {
                    lblCrDr.Text = "Dr";
                    lblCrDr.ForeColor = Color.Green;
                    lblCrDr.BackColor = Color.Yellow;
                }
                this.PnlLedger.Controls.Add(lblCrDr);
                this.Refresh();
                LabelCrDrCreated = true;
            }
        }
        string chk4;
        
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstLedgerName.SelectedIndex < lstLedgerName.Items.Count - 1)
                {
                    lstLedgerName.SetSelected(lstLedgerName.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstLedgerName.SelectedIndex > 0)
                {
                    lstLedgerName.SetSelected(lstLedgerName.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lstLedgerName.SelectedItems.Count > 0)
                {
                    DynamictxtBox.Text = lstLedgerName.SelectedItem.ToString();
                    lstLedgerName.Visible = false;
                    this.PnlLedger.Controls["txtDynamic" + (k - 1)].Text = lstLedgerName.SelectedItem.ToString();
                    this.PnlLedger.Controls["txtDynamic" + (k - 1)].BackColor = Color.Honeydew;
                    SqlCommand cmdlst = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_groupno in ('5','6') and Ledger_name ='" + DynamictxtBox.Text + "' order by Ledger_name ", con);
                    SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                    DataTable dtlst = new DataTable();
                    dtlst.Rows.Clear();
                    con.Close();
                    adaptlst.Fill(dtlst);
                    if (dtlst.Rows.Count > 0)
                    {
                        if (DynamictxtBox.Text != "")
                        {
                            if (txtVoucherType.Text == "Payment")
                            {
                                if (ctamt > Convert.ToDecimal(txtDebitTotal.Text))
                                {
                                    vCrDr = "Dr";
                                }
                                else
                                {
                                    vCrDr = "Cr";
                                }
                            }
                            else
                            {
                                if (txtCreditTotal.Text != "")
                                {
                                    if (ctamt > Convert.ToDecimal(txtCreditTotal.Text))
                                    {
                                        vCrDr = "Cr";
                                    }
                                    else
                                    {
                                        vCrDr = "Dr";
                                    }
                                }
                            }
                        }


                        lstLedgerName.Visible = false;
                        pnllist.Visible = false;
                        if (DynamictxtBox.Text != "")
                        {
                          // CreateCrDrLabel();
                           CreateLedgerLabel();
                           CreateAmountTextBox();
                           AmountTextBoxCreated = true;
                        }
                    }
                }
                else
                {
                    
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnllist.Visible = false;
                lstLedgerName.Visible = false;
            }
        }
        decimal vCrTotal = 0, vDrTotal = 0;
        decimal ctamt;
        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string vCrDrAmt = "";
                vCrDrAmt = this.PnlLedger.Controls["txtAmount" + (i - 1)].Text;

                if (vCrDrAmt != "")
                {
                    lstLedgerName.Visible = true;
                    vCrTotal = vCrTotal + Convert.ToDecimal(vCrDrAmt);
                    txtCreditTotal.Text = vCrTotal.ToString();
                    ctamt = Convert.ToDecimal(vCrDrAmt)+ctamt;
                    if (txtVoucherType.Text == "Payment")
                    {
                        if (txtDebitTotal.Text != "")
                        {
                            if (ctamt > Convert.ToDecimal(txtDebitTotal.Text))
                            {
                                vCrDr = "Dr";
                                CreateLedgerTextBox();
                                LedgerTextBoxCreated = true;
                                //LstLoad();

                                SqlCommand cmdchk = new SqlCommand("Select * from Ledger_table where Ledger_groupno in ('5','6') order by Ledger_groupno ", con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdchk);
                                dt.Rows.Clear();
                                lstLedgerName.Items.Clear();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int s = 0; s < dt.Rows.Count; s++)
                                    {
                                        lstLedgerName.Items.Add(dt.Rows[s]["Ledger_name"].ToString());
                                    }
                                }
                                pnllist.Visible = true;
                            }
                            else if (txtCreditTotal.Text == txtDebitTotal.Text)
                            {
                                vCurrBal = this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text;
                                string vSPlitVal = vCurrBal;
                                vSPlitVal = vSPlitVal.Remove(vSPlitVal.Length - 3);
                                var result = vSPlitVal.Length <= 17 ? "" : vSPlitVal.Remove(0, 17);
                                decimal OldVal = 0;
                                decimal NewVal = 0;
                                OldVal = Convert.ToDecimal(result);
                                NewVal = Convert.ToDecimal(vCrDrAmt);
                                string vTot = (Convert.ToDecimal(OldVal) + Convert.ToDecimal(NewVal)).ToString();
                                this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = "Current Balnce : " + vTot + " " + vCrDr;

                                txtNarration.Focus();
                            }
                            else if (txtVoucherType.Text == "Receipt")
                            {
                                CreateLedgerTextBox();
                                LedgerTextBoxCreated = true;
                                // LstLoad();
                                SqlCommand cmdchk = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_groupno in ('32') order by Ledger_name ", con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdchk);
                                dt.Rows.Clear();
                                lstLedgerName.Items.Clear();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int s = 0; s < dt.Rows.Count; s++)
                                    {
                                        lstLedgerName.Items.Add(dt.Rows[s]["Ledger_name"].ToString());
                                    }
                                }
                                pnllist.Visible = true;
                            }
                            else
                            {
                                CreateCrDrLabel();

                                vCurrBal = this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text;
                                string vSPlitVal = vCurrBal;
                                vSPlitVal = vSPlitVal.Remove(vSPlitVal.Length - 3);
                                var result = vSPlitVal.Length <= 17 ? "" : vSPlitVal.Remove(0, 17);
                                decimal OldVal = 0;
                                decimal NewVal = 0;
                                OldVal = Convert.ToDecimal(result);
                                NewVal = Convert.ToDecimal(vCrDrAmt);
                                string vTot = (Convert.ToDecimal(OldVal) + Convert.ToDecimal(NewVal)).ToString();
                                this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = "Current Balnce : " + vTot + " " + vCrDr;

                                LabelLedgerCreated = false;
                                CreateLedgerTextBox();
                                LedgerTextBoxCreated = true;

                                SqlCommand cmdchk = new SqlCommand("Select * from Ledger_table where Ledger_groupno in ('5','6') order by Ledger_groupno ", con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdchk);
                                dt.Rows.Clear();
                                lstLedgerName.Items.Clear();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int s = 0; s < dt.Rows.Count; s++)
                                    {
                                        lstLedgerName.Items.Add(dt.Rows[s]["Ledger_name"].ToString());
                                    }
                                }
                                pnllist.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        CreateCrDrLabel();
                        if (ctamt > Convert.ToDecimal(txtCreditTotal.Text))
                        {
                            vCrDr = "Dr";
                            CreateLedgerTextBox();
                            LedgerTextBoxCreated = true;
                            LstLoad();
                            pnllist.Visible = true;
                        }
                        else if (txtCreditTotal.Text == txtDebitTotal.Text)
                        {
                            vCurrBal = this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text;
                            string vSPlitVal = vCurrBal;
                            vSPlitVal = vSPlitVal.Remove(vSPlitVal.Length - 3);
                            var result = vSPlitVal.Length <= 17 ? "" : vSPlitVal.Remove(0, 17);
                            decimal OldVal = 0;
                            decimal NewVal = 0;
                            OldVal = Convert.ToDecimal(result);
                            NewVal = Convert.ToDecimal(vCrDrAmt);
                            string vTot = (Convert.ToDecimal(OldVal) + Convert.ToDecimal(NewVal)).ToString();
                            this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = "Current Balnce : " + vTot + " " + vCrDr;

                            txtNarration.Focus();
                        }
                        else
                        {
                            CreateLedgerTextBox();
                            LedgerTextBoxCreated = true;
                            LstLoad();
                            pnllist.Visible = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter The Amount", "Warning");
                    this.PnlLedger.Controls["txtAmount" + (i - 1)].Focus();
                }
            }
        }

        // CR Amount Text Box Key_Down Event //

        private void txtDrAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string vCrDrAmt = "";
                vCrDrAmt = this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Text;
                if (vCrDrAmt != "")
                {
                   
                    lstLedgerName.Visible = true;
                    vDrTotal = vDrTotal + Convert.ToDecimal(vCrDrAmt);
                    txtDebitTotal.Text = vDrTotal.ToString();

                         if (txtCreditTotal.Text == txtDebitTotal.Text)
                         {
                             vCurrBal = this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text;
                             string vSPlitVal = vCurrBal;
                             vSPlitVal = vSPlitVal.Remove(vSPlitVal.Length - 3);
                             var result = vSPlitVal.Length <= 17 ? "" : vSPlitVal.Remove(0, 17);
                             decimal OldVal = 0;
                             decimal NewVal = 0;
                             OldVal = Convert.ToDecimal(result);
                             NewVal = Convert.ToDecimal(vCrDrAmt);
                             string vTot = (Convert.ToDecimal(OldVal) + Convert.ToDecimal(NewVal)).ToString();
                             this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = "Current Balnce : " + vTot + " " + vCrDr;

                             txtNarration.Focus();
                         }
                         else
                         {      
                             CreateLedgerTextBox();
                             LedgerTextBoxCreated = true;
                             //LstLoad();
                             pnllist.Visible = true;
                            vCurrBal=this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr-1).ToString()].Text;
                            string vSPlitVal = vCurrBal;
                            vSPlitVal = vSPlitVal.Remove(vSPlitVal.Length - 3);
                            var result = vSPlitVal.Length <= 17 ? "" : vSPlitVal.Remove(0, 17);

                            decimal OldVal = 0;
                            decimal NewVal = 0;
                            OldVal = Convert.ToDecimal(result);
                            NewVal = Convert.ToDecimal(vCrDrAmt);

                            //lblLedger.Text = "Current Balnce : " + vCrDrAmt + " " + vCrDr;
                            string vTot = (Convert.ToDecimal(OldVal) + Convert.ToDecimal(NewVal)).ToString();
                            this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr - 1).ToString()].Text = "Current Balnce : " + vTot + " " + vCrDr;
                            CreateCrDrLabel();
                         }
                    //CreateLedgerTextBox();
                    //LedgerTextBoxCreated = true;
                    //LstLoad();
                    //// pnllist.Location = new Point(txxt + 1, tyt + 30);
                    //pnllist.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please Enter The Amount", "Warning");
                    this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Focus();
                }
            }
            if (e.KeyCode == Keys.Back)
            {
                if (this.PnlLedger.Controls["txtDrAmount" + (d - 1)].Text == "")
                {
                    
                }
            }

        }

        bool isChk = false;
        DataTable dt = new DataTable();

        // Ledger Lst Load //
        public void LstLoad()
        {
            string cmd = "";
            if (txtVoucherType.Text == "Payment")
            {
                cmd = "Select * from Ledger_table where Ledger_groupno in ('28','31','51') order by Ledger_groupno ";
            }
            else if (txtVoucherType.Text == "Receipt")
            {
                cmd = "Select * from Ledger_table where Ledger_groupno in ('32') order by Ledger_name ";
            } 
            else 
            {
                cmd = "Select * from Ledger_table where Ledger_groupno in ('32') order by Ledger_name ";
            }
            SqlCommand cmdchk = new SqlCommand(cmd, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmdchk);
            dt.Rows.Clear();
            lstLedgerName.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstLedgerName.Items.Add(dt.Rows[i]["Ledger_name"].ToString());
                }
            }
            lstLedgerName.Visible = true;
            pnllist.Visible = true;
          //  _Class.clsVariables.Sheight_Width();
        }

        // Counter Load //

        DataTable dtCounter = new DataTable();
        public void LstLoadCounter()
        {
            SqlCommand cmd = new SqlCommand("select distinct ctr_name as Counter from counter_table order by ctr_name ", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dtCounter.Rows.Clear();
            lstCounter.Items.Clear();
            adp.Fill(dtCounter);
            if (dtCounter.Rows.Count > 0)
            {
                for (int i = 0; i < dtCounter.Rows.Count; i++)
                {
                    lstCounter.Items.Add(dtCounter.Rows[i]["Counter"].ToString());
                }
            }
        }

        // Voucher Type Load - " Payment / Receipt " //

        DataTable dtType = new DataTable();
        public void LstLoadType()
        {
            //SqlCommand cmd = new SqlCommand("select distinct a.Vt_name as Type,b.Vch_Type from Vtable a, VoucherNo_Table b where a.Vt_Sno=b.Vch_Type order by a.Vt_name ", con);
            SqlCommand cmd = new SqlCommand("select Vt_name from Vtable where vt_Sno in(1,2)", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dtType.Rows.Clear();
            lstType.Items.Clear();
            adp.Fill(dtType);
            if (dtType.Rows.Count > 0)
            {
                for (int i = 0; i < dtType.Rows.Count; i++)
                {
                    lstType.Items.Add(dtType.Rows[i]["Vt_name"].ToString());
                }
            }
            //_Class.clsVariables.Sheight_Width();
        }

        public void FormLoad()
        {
            LstLoadCounter();
            LstLoadType();
            LstLoad();
            AutoID();
            pnlCounter.Visible = true;
            pnlType.Visible = false;
            pnllist.Visible = false;
            this.ActiveControl = txtCounter;
        }

        bool LedgerShowBox = false;
        bool CrAmtShowBox = false;
        bool DrAmtShowBox = false;
        bool ShowLabelLedgerCreated = false;
        string vDBType = "";
        private void FrmVoucherDisplay_Load(object sender, EventArgs e)
        {
            if (frmDayBook.vVoucherNo != "")
            {
                SqlCommand cmdlst = new SqlCommand("Select t1.VoucherNo,CONVERT(VARCHAR(11), VoucherDate, 103)as Date,CounterNo,VoucherType,DebitTotal,CreditTotal,Narration, " +
                                                   " t2.VoucherNo,LedgerName,DebitAmt,CreditAmt from T_VoucherTable t1,T_VoucherDetailsTable t2 " +
                                                   " where t1.VoucherNo=t2.VoucherNo and t1.VoucherNo='" + frmDayBook.vVoucherNo + "' ", con);
                SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                DataTable dtlst = new DataTable();
                dtlst.Rows.Clear();
                con.Close();
                adaptlst.Fill(dtlst);
                if (dtlst.Rows.Count > 0)
                {
                    lbleAuto.Text = dtlst.Rows[0]["VoucherNo"].ToString();
                    dtpVoucherDate.Text = dtlst.Rows[0]["Date"].ToString();
                    txtCounter.Text = dtlst.Rows[0]["CounterNo"].ToString();
                    txtVoucherType.Text = dtlst.Rows[0]["VoucherType"].ToString();
                    txtDebitTotal.Text = dtlst.Rows[0]["DebitTotal"].ToString();
                    txtCreditTotal.Text = dtlst.Rows[0]["CreditTotal"].ToString();
                    txtNarration.Text = dtlst.Rows[0]["Narration"].ToString();

                    pnlCounter.Visible = false;
                    pnlType.Visible = false;
                    pnllist.Visible = false;

                    for (int i = 0; i < dtlst.Rows.Count; i++)
                    {
                        txtLedgerName.Visible = false;

                        if (LedgerShowBox == true)
                        {
                            txxt = txxt + 50;
                            tyt = 100;
                            LedgerShowBox = true;
                        }
                        else
                        {
                            txxt = 10;
                            tyt = 100;
                            LedgerShowBox = true;
                        }
                        TextBox tbLedgerShowBox = new TextBox();
                        if (this.Width != 1152)
                        {
                            tbLedgerShowBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            tbLedgerShowBox.Location = new System.Drawing.Point(tyt + (0 * 65), txxt);
                            tbLedgerShowBox.Size = new Size(600, 100);

                            this.PnlLedger.Controls.Add(tbLedgerShowBox);
                            this.ResumeLayout(false);
                            //y += 100;
                            tbLedgerShowBox.Name = "tbLedgerShowBox" + i.ToString();
                            string Values = this.PnlLedger.Controls["tbLedgerShowBox" + (i).ToString()].Text;
                            this.PnlLedger.Controls["tbLedgerShowBox" + (i).ToString()].Text = dtlst.Rows[i]["LedgerName"].ToString();
                            tbLedgerShowBox.BorderStyle = BorderStyle.None;
                            tbLedgerShowBox.BackColor = Color.LightYellow;
                            this.Refresh();
                            
                            if (ShowLabelLedgerCreated == true)
                            {
                                lblxx = lblxx + 50;
                                ShowLabelLedgerCreated = true;
                            }
                            else
                            {
                                lblyy = 97;
                                lblxx = 35;
                                ShowLabelLedgerCreated = true;
                            }
                            
                            string cmd="";
                            cmd = " Select ledger_open,climit_amount from Ledger_table where ledger_name ='" + dtlst.Rows[i]["LedgerName"].ToString() + "' ";
                            SqlCommand cmdlstShow = new SqlCommand(cmd, con);
                            SqlDataAdapter adaptlstShow = new SqlDataAdapter(cmdlstShow);
                            DataTable dtlstShow = new DataTable();
                            dtlstShow.Rows.Clear();
                            con.Close();
                            adaptlstShow.Fill(dtlstShow);
                            if (dtlstShow.Rows.Count > 0)
                            {
                                string ChkVrCr = dtlstShow.Rows[0]["ledger_open"].ToString();

                                if (Convert.ToDouble(dtlstShow.Rows[0]["Ledger_open"].ToString().Trim()) < 0)
                                {
                                    vCrDr = "Cr";
                                }
                                else
                                {
                                    vCrDr = "Dr";
                                }

                                Label lblLedger = new Label();
                                lblLedger.Location = new System.Drawing.Point(lblyy, lblxx);
                                lblLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                lblLedger.AutoSize = true;
                                lblLedger.ForeColor = Color.Blue;
                                if (ChkVrCr != "")
                                {
                                    lblLedger.Text = "Current Balnce : " + ChkVrCr + " " + vCrDr;
                                }
                                else
                                {
                                    string Amt = dtlstShow.Rows[0]["climit_amount"].ToString();
                                    if (Amt != "")
                                    {
                                        lblLedger.Text = "Current Balnce : " + Amt + " " + vCrDr;
                                    }
                                }

                                this.PnlLedger.Controls.Add(lblLedger);
                                lblLedger.Name = "CurrBalLabel" + AmtLblDr.ToString();
                                this.Refresh();
                                vCurrBal = this.PnlLedger.Controls["CurrBalLabel" + (AmtLblDr).ToString()].Text;
                                AmtLblDr = AmtLblDr + 1;
                            }
                            decimal DBCrAmt = 0;
                            decimal DBDrAmt = 0;
                            DBCrAmt = Convert.ToDecimal(dtlst.Rows[i]["CreditAmt"].ToString());
                            DBDrAmt = Convert.ToDecimal(dtlst.Rows[i]["DebitAmt"].ToString());
                            

                            if (DBDrAmt == 0)
                            {
                                vDBType = "";
                                vDBType="Cr";
                            }
                            else
                            {
                                vDBType = "";
                                vDBType = "Dr";
                            }
                            
                            if(vDBType=="Cr")
                            {
                                CrDrShowLabel();
                                if (DrAmtShowBox == true)
                                {
                                    x = x + 50;
                                    y = 990;
                                    CrAmtShowBox = true;
                                }

                                else
                                {
                                    if (CrAmtShowBox == true)
                                    {
                                        x = x + 50;
                                        y = 990;
                                        CrAmtShowBox = true;
                                    }
                                    else
                                    {
                                        x = 10;
                                        y = 990;
                                        CrAmtShowBox = true;
                                    }
                                }
                                
                                TextBox tbCrShowBox = new TextBox();
                                if (this.Width != 1152)
                                {
                                    tbCrShowBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    tbCrShowBox.Location = new System.Drawing.Point(y + (0 * 65), x);
                                    tbCrShowBox.Size = new Size(100, 100);
                                    this.PnlLedger.Controls.Add(tbCrShowBox);
                                    this.ResumeLayout(false);
                                    y += 100;
                                    tbCrShowBox.Name = "tbCrShowBox" + i.ToString();
                                    tbCrShowBox.Text = "";
                                    string value = this.PnlLedger.Controls["tbCrShowBox" + i].Text;
                                    tbCrShowBox.TextAlign = HorizontalAlignment.Right;
                                    this.Refresh();
                                    this.PnlLedger.Controls["tbCrShowBox" + (i)].Text = "";
                                    this.PnlLedger.Controls["tbCrShowBox" + (i).ToString()].Text = dtlst.Rows[i]["CreditAmt"].ToString();
                                    tbCrShowBox.BorderStyle = BorderStyle.None;
                                    tbCrShowBox.BackColor = Color.LightYellow;
                                    CrAmtShowBox = true;  
                                }
                            }
                            else if (vDBType=="Dr")
                            {
                                CrDrShowLabel();
                                if (DrAmtShowBox == true)
                                {
                                    x = x + 40;
                                    y = 830;
                                    DrAmtShowBox = true;
                                }
                                else
                                {
                                    x = 10;
                                    y = 830;
                                    DrAmtShowBox = true;
                                }
                                TextBox tbDrShowBox = new TextBox();
                                if (this.Width != 1152)
                                {
                                    tbDrShowBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    tbDrShowBox.Location = new System.Drawing.Point(y + (0 * 65), x);
                                    tbDrShowBox.Size = new Size(100, 100);
                                    this.PnlLedger.Controls.Add(tbDrShowBox);
                                    this.ResumeLayout(false);
                                    y += 100;
                                    tbDrShowBox.Name = "tbDrShowBox" + i.ToString();
                                    tbDrShowBox.Text = "";
                                    string value = this.PnlLedger.Controls["tbDrShowBox" + i].Text;
                                    tbDrShowBox.TextAlign = HorizontalAlignment.Right;
                                    this.Refresh();
                                    this.PnlLedger.Controls["tbDrShowBox" + (i)].Text = "";
                                    this.PnlLedger.Controls["tbDrShowBox" + (i).ToString()].Text = dtlst.Rows[i]["DebitAmt"].ToString();
                                    tbDrShowBox.BorderStyle = BorderStyle.None;
                                    tbDrShowBox.BackColor = Color.LightYellow;
                                    DrAmtShowBox = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                FormLoad();
            }

            this.ActiveControl = txtCounter;
        }

        public bool CrDrShowLabelCreated = false;
        public void CrDrShowLabel()
        {
            if (CrDrShowLabelCreated == true)
            {
                lblx = lblx + 50;
                CrDrShowLabelCreated = true;
            }
            else
            {
                lbly = 15;
                lblx = 10;
                CrDrShowLabelCreated = true;
            }

            if (this.Width != 1152)
            {
                Label lblCrDr = new Label();
                lblCrDr.Location = new System.Drawing.Point(lbly + (0 * 65), lblx);
                lblCrDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblCrDr.AutoSize = true;

                lblCrDr.Text = vDBType;
                if (vDBType == "Cr")
                {
                    lblCrDr.ForeColor = Color.Red;
                    lblCrDr.BackColor = Color.Yellow;
                }
                else if (vDBType == "Dr")
                {
                    lblCrDr.ForeColor = Color.Green;
                    lblCrDr.BackColor = Color.Yellow;
                }

                this.PnlLedger.Controls.Add(lblCrDr);
                this.Refresh();
                LabelCrDrCreated = true;
            }
        }

        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLedgerName.Visible = true;
                txtLedgerName.Focus();
                pnllist.Visible = true;
            }
        }

        private void lstType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pnlType.Visible = false;
            }
        }

        private void lstType_Click(object sender, EventArgs e)
        {
            //if (lstType.SelectedItems.Count > 0)
            //{
            //    txtVoucherType.Text = "";
            //    txtVoucherType.Text = lstType.SelectedItem.ToString();
            //    string Type = lstType.SelectedItem.ToString();
            //    txtVoucherType.Text = Type;
            //    pnlType.Visible = false;
            //}
            if (lstType.Items.Count > 0)
            {
                txtVoucherType.Text = lstType.SelectedItem.ToString();
            }
            pnlType.Visible = false;
            lstType.Visible = false;
        }
        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lstType.SelectedIndex < lstType.Items.Count - 1)
                    {
                        lstType.SetSelected(lstType.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lstType.SelectedIndex > 0)
                    {
                        lstType.SetSelected(lstType.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (lstType.SelectedItems.Count > 0)
                    {
                        txtVoucherType.Text = lstType.SelectedItem.ToString();
                        lstType.Visible = false;

                        SqlCommand cmdlst = new SqlCommand("select distinct a.Vt_name as Type,b.Vch_Type from Vtable a, VoucherNo_Table b where a.Vt_Sno=b.Vch_Type and a.Vt_name ='" + txtVoucherType.Text + "' order by a.Vt_name ", con);
                        SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                        DataTable dtlst = new DataTable();
                        dtlst.Rows.Clear();
                        con.Close();
                        adaptlst.Fill(dtlst);
                        if (dtlst.Rows.Count > 0)
                        {
                            txtVoucherType.Text = dtlst.Rows[0]["Type"].ToString();
                            lstType.Visible = false;
                            pnlType.Visible = false;
                            dtpVoucherDate.Focus();
                        }
                    }
                    else
                    {
                        
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    pnlType.Visible = false;
                    lstType.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }
        string chk2;
        private void txtType_TextChanged(object sender, EventArgs e)
        {
            if (txtVoucherType.Text.Trim() != null && txtVoucherType.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("select a.Vt_name as Type,b.Vch_Type from Vtable a, VoucherNo_Table b where a.Vt_Sno=b.Vch_Type and a.Vt_name like @TypeName  order by a.Vt_name ", con);
                //  SqlCommand cmd = new SqlCommand("Select * from Ledger_table Where Ledger_no>14 and ledger_name like @LedgerName order by Ledger_name", con);
                cmd.Parameters.AddWithValue("@TypeName", txtVoucherType.Text.Trim() + '%');
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroupLedgerSelect = new DataTable();
                dtGroupLedgerSelect.Rows.Clear();
                adp.Fill(dtGroupLedgerSelect);
                isChk = false;
                if (dtGroupLedgerSelect.Rows.Count > 0)
                {
                    string tempstr = dtGroupLedgerSelect.Rows[0]["Type"].ToString().Trim();
                    for (int k = 0; k < lstType.Items.Count; k++)
                    {
                        if (tempstr == lstType.Items[k].ToString().Trim())
                        {
                            isChk = true;
                            lstType.SetSelected(k, true);
                            txtVoucherType.Select();
                            chk2 = "1";
                            txtVoucherType.KeyPress += new KeyPressEventHandler(txtType_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk2 = "2";
                    if (txtVoucherType.Text != "")
                    {
                        //string name = txtVoucherType.Text.Remove(txtVoucherType.Text.Length - 1);
                        //txtVoucherType.Text = name.ToString();
                        //txtVoucherType.Select(txtCounter.Text.Length, 0);
                    }
                    txtVoucherType.KeyPress += new KeyPressEventHandler(txtType_KeyPress);
                    chk2 = "1";
                }
                else
                {
                    chk2 = "1";
                }
            }

        }
       
        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void lstLedgerName_Click(object sender, EventArgs e)
        {
            if (lstLedgerName.SelectedItems.Count > 0)
            {
                txtLedgerName.Text = lstLedgerName.SelectedItem.ToString();
            }
        }
        string chk;
        string chk3;
        private void txtLedger_TextChanged(object sender, EventArgs e)
        {
            if (txtLedgerName.Text.Trim() != null && txtLedgerName.Text.Trim() != "")
            {

                string cmd = "";
                if (txtVoucherType.Text == "Payment")
                {
                    cmd = "Select * from Ledger_table where Ledger_groupno in ('28','31','51') and ledger_name like '"+ txtLedgerName.Text +"%' ";
                }
                else if (txtVoucherType.Text == "Receipt")
                {
                    cmd = "Select * from Ledger_table where Ledger_groupno in ('32')  and ledger_name like '" + txtLedgerName.Text + "%' ";
                }

                //SqlCommand cmd = new SqlCommand("Select ledger_name from Ledger_table where Ledger_no<14 and ledger_name like @CounterName", con);
                ////  SqlCommand cmd = new SqlCommand("Select * from Ledger_table Where Ledger_no>14 and ledger_name like @LedgerName order by Ledger_name", con);
                //cmd.Parameters.AddWithValue("@CounterName", txtLedgerName.Text.Trim() + '%');
                SqlCommand cmdChk = new SqlCommand(cmd,con);

                SqlDataAdapter adp = new SqlDataAdapter(cmdChk);
                DataTable dtGroupLedgerSelect = new DataTable();
                dtGroupLedgerSelect.Rows.Clear();
                adp.Fill(dtGroupLedgerSelect);
                isChk = false;
                if (dtGroupLedgerSelect.Rows.Count > 0)
                {
                    string tempstr = dtGroupLedgerSelect.Rows[0]["ledger_name"].ToString().Trim();
                    for (int k = 0; k < lstLedgerName.Items.Count; k++)
                    {
                        if (tempstr == lstLedgerName.Items[k].ToString().Trim())
                        {
                            isChk = true;
                            lstLedgerName.SetSelected(k, true);
                            txtLedgerName.Select();
                            chk3 = "1";
                            txtLedgerName.KeyPress += new KeyPressEventHandler(txtLedgerControl_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk3 = "2";
                    if (txtLedgerName.Text != "")
                    {
                        string name = txtLedgerName.Text.Remove(txtLedgerName.Text.Length - 1);
                        txtLedgerName.Text = name.ToString();
                        txtLedgerName.Select(txtLedgerName.Text.Length, 0);
                    }
                    txtCounter.KeyPress += new KeyPressEventHandler(txtLedgerControl_KeyPress);
                    chk3 = "1";
                }
                else
                {
                    chk3 = "1";
                }
            }
        }
        private void txtLedgerControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk3 == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }
        string vCrDr = "";
        private void txtLedger_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lstLedgerName.SelectedIndex < lstLedgerName.Items.Count - 1)
                    {
                        lstLedgerName.SetSelected(lstLedgerName.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lstLedgerName.SelectedIndex > 0)
                    {
                        lstLedgerName.SetSelected(lstLedgerName.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (lstLedgerName.SelectedItems.Count > 0)
                    {
                        
                        txtLedgerName.Text = lstLedgerName.SelectedItem.ToString();
                        lstLedgerName.Visible = false;
                        txtLedgerName.BackColor = Color.Honeydew;
                        SqlCommand cmdlst = new SqlCommand(" Select * from Ledger_table where ledger_name ='" + txtLedgerName.Text + "' ", con);
                        SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                        DataTable dtlst = new DataTable();
                        dtlst.Rows.Clear();
                        con.Close();
                        adaptlst.Fill(dtlst);
                        if (dtlst.Rows.Count > 0)
                        {
                             string ChkVrCr = dtlst.Rows[0]["ledger_open"].ToString();

                             if (ChkVrCr != "")
                            {
                                if (ChkVrCr == "0")
                                {
                                    vCrDr = "Dr";
                                }
                                else 
                                {
                                    vCrDr = "Cr";
                                }
                            }

                            lstLedgerName.Visible = false;
                            pnllist.Visible = false;
                            if (txtLedgerName.Text != "")
                            {
                                lblCrDr.Text = vCrDr;
                                CreateLedgerLabel();
                                CreateAmountTextBox();
                                txtLNameBoxClicked = false;
                            }
                        }
                    }
                    else
                    {
                       
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    pnllist.Visible = false;
                    lstLedgerName.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }

        private void txtLedger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void dtpVoucherDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtVoucherType.Text == "Payment")
                {
                    lblCrDr.Text = "Dr";
                    vCrDr = "Dr";
                }
                else if (txtVoucherType.Text == "Receipt")
                {
                    lblCrDr.Text = "Cr";
                    vCrDr = "Cr";
                    lblCrDr.Visible = true;
                }
                LstLoad();
                txtLedgerName.Text = "";
                txtLedgerName.Visible = true;
                txtLedgerName.Focus();
                lblCrDr.Visible = true;
                lstLedgerName.Visible = true;
                pnllist.Visible = true;
            }
        }

        private void dtpVoucherDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtVoucherType.Text == "Payment")
            {
                lblCrDr.Text = "Dr";
                vCrDr = "Dr";
            }
            else if (txtVoucherType.Text == "Receipt")
            {
                lblCrDr.Text = "Cr";
                lblCrDr.Visible = true;
                vCrDr = "Cr";
            }
            LstLoad();
            txtLedgerName.Text = "";
            txtLedgerName.Visible = true;
            txtLedgerName.Focus();
            pnllist.Visible = true;
        }

        private void txtCounter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lstCounter.SelectedIndex < lstCounter.Items.Count - 1)
                    {
                        lstCounter.SetSelected(lstCounter.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lstCounter.SelectedIndex > 0)
                    {
                        lstCounter.SetSelected(lstCounter.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (lstCounter.SelectedItems.Count > 0)
                    {

                        txtCounter.Text = lstCounter.SelectedItem.ToString();
                        lstCounter.Visible = false;
                    }
                    txtVoucherType.Focus();
                    pnlCounter.Visible = false;
                    lstCounter.Visible = false;
                    pnlType.Visible = true;
                    lstType.Visible = true;
                }
                if (e.KeyCode == Keys.Escape)
                {
                    pnlCounter.Visible = false;
                    lstCounter.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txtVoucherType.Focus();
            //    pnlCounter.Visible = false;
            //    lstCounter.Visible = false;
            //    pnlType.Visible = true;
            //    lstType.Visible = true;
            //}
        }

        // Ledger Text Box Click Event //
        private void tb_Click(object sender, EventArgs e)
        {
            SqlCommand cmdchk = new SqlCommand("Select * from Ledger_table where Ledger_groupno in ('5','6') order by Ledger_groupno ", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmdchk);
            dt.Rows.Clear();
            lstLedgerName.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int s = 0; s < dt.Rows.Count; s++)
                {
                    lstLedgerName.Items.Add(dt.Rows[s]["Ledger_name"].ToString());
                }
            }
            pnllist.Visible = true;
            lstLedgerName.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCounter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void txtCounter_Click(object sender, EventArgs e)
        {
            pnlCounter.Visible = true;
            lstCounter.Visible = true;
            LstLoadCounter();
        }

        bool txtLNameBoxClicked = false;
        private void txtLedgerName_Click(object sender, EventArgs e)
        {
            txtLNameBoxClicked = true;
            LstLoad();
        }

        private void lstLedgerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LedgerTextBoxCreated == true)
            {
                this.PnlLedger.Controls["txtDynamic" + (k - 1)].Text = lstLedgerName.SelectedItem.ToString();
            }
            else
            {
                txtLedgerName.Text = lstLedgerName.SelectedItem.ToString();
            }
        }
        string chk1;
        private void txtCounter_TextChanged(object sender, EventArgs e)
        {           
            pnlCounter.Visible=true;
            lstCounter.Visible = true;
            if (txtCounter.Text.Trim() != null && txtCounter.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("select ctr_name from counter_table where ctr_name like @CounterName order by ctr_name", con);
              //  SqlCommand cmd = new SqlCommand("Select * from Ledger_table Where Ledger_no>14 and ledger_name like @LedgerName order by Ledger_name", con);
                cmd.Parameters.AddWithValue("@CounterName", txtCounter.Text.Trim() + '%');
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroupLedgerSelect = new DataTable();
                dtGroupLedgerSelect.Rows.Clear();
                adp.Fill(dtGroupLedgerSelect);
                isChk = false;
                if (dtGroupLedgerSelect.Rows.Count > 0)
                {
                    string tempstr = dtGroupLedgerSelect.Rows[0]["ctr_name"].ToString().Trim();
                    for (int k = 0; k < lstCounter.Items.Count; k++)
                    {
                        if (tempstr == lstCounter.Items[k].ToString().Trim())
                        {
                            isChk = true;
                            lstCounter.SetSelected(k, true);
                            txtCounter.Select();
                            chk1 = "1";
                            txtCounter.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk1 = "2";
                    if (txtCounter.Text != "")
                    {
                        //string name = txtCounter.Text.Remove(txtCounter.Text.Length - 1);
                        //txtCounter.Text = name.ToString();
                        //txtCounter.Select(txtCounter.Text.Length, 0);
                    }
                    txtCounter.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    chk1 = "1";
                }
                else
                {
                    chk1 = "1";
                }
            }
        }
       
        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk1 == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void lstCounter_Click(object sender, EventArgs e)
        {
            if (lstCounter.Items.Count > 0)
            {
                txtCounter.Text = lstCounter.SelectedItem.ToString();
            }
            pnlCounter.Visible = false;
            lstCounter.Visible = false;
            pnlType.Visible = true;
            lstType.Visible = true;
            txtVoucherType.Focus();
        }

        private void txtVoucherType_Click(object sender, EventArgs e)
        {
            pnlType.Visible = true;
            lstType.Visible = true;
            LstLoadType();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AutoID();
            ClearAllTextBox();
            Clear(); 
        }

        public void Clear()
        {
            txtCounter.Text = "";
            txtVoucherType.Text = "";
            pnlCounter.Visible = false;
            pnlType.Visible = false;
            pnllist.Visible = false;
            txtCounter.Focus();
            txtDebitTotal.Text = "";
            txtCreditTotal.Text = "";
            txtNarration.Text = "";
            LedgerTextBoxCreated = false;
            LabelLedgerCreated = false;
            AmountTextBoxCreated = false;
            LabelCrDrCreated = false;
            
            AmtLblDr = 1;

            i = 1;
            d = 1;
            k = 1;

            x = 165;
            y = 0;
            lblxx = 155;
            lblyy = 97;
            txxt = 160;
            tyt = 100;
            lbly = 22;
            lblx = 65;
            ctamt=0;
            vCrTotal = 0;
            vDrTotal = 0;
            
            for (int Led = PnlLedger.Controls.Count; Led >0; Led--)
            {
                PnlLedger.Controls.RemoveAt(Led - 1);
            }

            txtLedgerName.Visible = false;
            lblCrDr.Visible = false;
            FormLoad();
            //this.Controls.Remove(txtLedgerName);
            //this.Controls.Remove(lblCrDr);
            
            //this.PnlLedger.Controls.Count = 0;
        }


        private void ClearAllTextBox()
        {
            //for (int Led = PnlLedger.Controls.Count; Led > 0; Led--)
            //{
            //    PnlLedger.Controls.RemoveAt(Led - 1);
            //}
            //this.Controls.Remove(txtLedgerName);
            //this.Controls.Remove(lblCrDr);
        }  

        public bool txtValidation()
        {
            if (txtCounter.Text == "")
            {
                MyMessageBox.ShowBox("Enter The Counter", "Warning");
                txtCounter.Focus();
                return false;
            }
            if (txtVoucherType.Text == "")
            {
                MyMessageBox.ShowBox("Enter The VoucherType", "Warning");
                txtVoucherType.Focus();
                return false;
            }
            if (txtLedgerName.Text == "")
            {
                MyMessageBox.ShowBox("Enter The LedgerName", "Warning");
                txtLedgerName.Focus();
                return false;
            }
            if (txtNarration.Text == "")
            {
                MyMessageBox.ShowBox("Enter The Narration", "Warning");
                txtNarration.Focus();
                return false;
            }
            return true;
        }

        double DebitAmt = 0.00;
        double CreditAmt = 0.00;
        decimal vCrAmt = 0;
        decimal vDrAmt = 0;
        int VDNO = 0;
        string strCrDr = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    if (txtValidation())
                    {
                        //if (Exists())
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            AutoID();
                            SqlCommand cmdInsert = new SqlCommand("VoucherTableInsert", con);
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            cmdInsert.Connection = con;
                            cmdInsert.Parameters.AddWithValue("@VoucherNo", lbleAuto.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@VoucherDate", Convert.ToString(dtpVoucherDate.Value.Month + "/" + dtpVoucherDate.Value.Day + "/" + dtpVoucherDate.Value.Year));
                            cmdInsert.Parameters.AddWithValue("@CounterNo", txtCounter.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@VoucherType", txtVoucherType.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@DebitTotal", txtDebitTotal.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@CreditTotal", txtCreditTotal.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@Narration", txtNarration.Text.Trim());
                            cmdInsert.ExecuteNonQuery();
                            //con.Close();

                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            for (int i = 0; i <= NoOfLedgerTextBox; i++)
                            {
                                SqlCommand cmdsve = new SqlCommand("VoucherDetailsTableInsert", con);
                                cmdsve.CommandType = CommandType.StoredProcedure;
                                cmdsve.Connection = con;
                                cmdsve.Parameters.AddWithValue("@VoucherNo", lbleAuto.Text.Trim());
                                AutoIncrement();
                                cmdsve.Parameters.AddWithValue("@VDetailsNo", VDNO);

                                string Values = "";
                                if (i != 0)
                                {
                                    Values = this.PnlLedger.Controls["txtDynamic" + (i).ToString()].Text;
                                }
                                else
                                {
                                    Values = txtLedgerName.Text.Trim();
                                }

                                cmdsve.Parameters.AddWithValue("@Ledgername", Values);
                                //if (i != 0)
                                //{
                                //    string Values = this.PnlLedger.Controls["txtDynamic" + (i).ToString()].Text;
                                //    cmdsve.Parameters.AddWithValue("@Ledgername", Values);
                                //}
                                //else
                                //{
                                //    cmdsve.Parameters.AddWithValue("@Ledgername", txtLedgerName.Text.Trim());
                                //}

                                if (i == 0)
                                {
                                    if (lblCrDr.Text == "Cr")
                                    {
                                        strCrDr = "";
                                        strCrDr = this.Controls["txtAmount" + (i + 1)].Text;
                                        vCrAmt = Convert.ToDecimal(strCrDr.ToString());
                                        vDrAmt = 0;
                                    }
                                    else if (lblCrDr.Text == "Dr")
                                    {
                                        strCrDr = "";
                                        //string value = this.Controls["txtDrAmount" + d].Text;
                                        strCrDr = this.PnlLedger.Controls["txtDrAmount" + (i + 1)].Text;
                                        vDrAmt = Convert.ToDecimal(strCrDr.ToString());
                                        vCrAmt = 0;
                                    }
                                }
                                else
                                {
                                    if (vCrDr == "Cr")
                                    {
                                        strCrDr = "";
                                        strCrDr = this.PnlLedger.Controls["txtAmount" + (i)].Text;
                                        vCrAmt = Convert.ToDecimal(strCrDr.ToString());
                                        vDrAmt = 0;
                                    }
                                    else if (vCrDr == "Dr")
                                    {
                                        strCrDr = "";
                                        strCrDr = this.PnlLedger.Controls["txtDrAmount" + (i + 1)].Text;
                                        vDrAmt = Convert.ToDecimal(strCrDr.ToString());
                                        vCrAmt = 0;
                                    }
                                }
                                cmdsve.Parameters.AddWithValue("@DebitAmt", vDrAmt);
                                cmdsve.Parameters.AddWithValue("@CreditAmt", vCrAmt);
                                cmdsve.ExecuteNonQuery();

                                string AmtIn = "";
                                decimal vTotAmt = 0;
                                SqlCommand cmdlst = new SqlCommand(" Select ledger_open from Ledger_table where ledger_name ='" + Values + "' ", con);
                                SqlDataAdapter adaptlst = new SqlDataAdapter(cmdlst);
                                DataTable dtlst = new DataTable();
                                dtlst.Rows.Clear();
                               
                                adaptlst.Fill(dtlst);
                                if (dtlst.Rows.Count > 0)
                                {
                                    AmtIn = "";
                                    AmtIn = dtlst.Rows[0]["ledger_open"].ToString();
                                }

                                vTotAmt = (Convert.ToDecimal(strCrDr) + Convert.ToDecimal(AmtIn));

                                SqlCommand cmdUpdate = new SqlCommand(" Update Ledger_Table set Ledger_Open ='" + vTotAmt + "' where Ledger_name='" + Values + "' ", con);
                                cmdUpdate.ExecuteNonQuery();

                            }
                        }
                        MyMessageBox.ShowBox("Record Saved Successfully", "Message");
                        Clear();

                        txtCounter.Focus();
                        con.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        int a;
        string val;
        string str1 = "1";
        public void AutoID()
        {

            DataTable dt6 = new DataTable();
            SqlDataAdapter adpt1 = new SqlDataAdapter("select MAX(VoucherNo) as VoucherNo from T_VoucherTable", con);
            adpt1.Fill(dt6);
            //con.Close();
            str1 = Convert.ToString(dt6.Rows[0]["VoucherNo"].ToString());
            if (!string.IsNullOrEmpty(str1))
            {
                lbleAuto.Text = (Convert.ToInt32(str1) + 1).ToString();
                //txtTNo.Text = (Convert.ToInt32(str1) + 1).ToString();
            }
            else
            {
                lbleAuto.Text = "1";
                //txtTNo.Text = "1";
            }
        }

        public void AutoIncrement()
        {
            SqlCommand cmd = new SqlCommand("select MAX(VDetailsNo)AS VDetailsNo from T_VoucherDetailsTable", con);
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            adapt.Fill(dt);
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["VDetailsNo"].ToString()))
            {
                VDNO = dt.Rows[0]["VDetailsNo"].ToString() == null ? 1 : Convert.ToInt32(dt.Rows[0]["VDetailsNo"].ToString());
                VDNO = VDNO + 1;
            }
            else
            {
                VDNO = 1;
            }
        }

        public void LedgerShow()
        {
            
        }
    }
}
