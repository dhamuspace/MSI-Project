using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Configuration;
using System.Globalization;

namespace SalesProject
{
    public partial class frmLedgerCreation : Form
    {
        public frmLedgerCreation()
        {
            InitializeComponent();
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            listLoad();
            pnlUnderName.Visible = false;
            txtCrDr.Visible = false;
        }
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        int LedgerNo;
        string DOB = "";
        private void listLoad()
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(@"Select Ledger_groupname from Ledger_Grouptable Order by Ledger_groupname ASC", con);
                dt.Rows.Clear();
                adp.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstUnderName.Items.Add(dt.Rows[i]["Ledger_groupname"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Clear()
        {
            txtLName.Text = "";
            txtLPName.Text = "";
            txtLAName.Text = "";
            txtUnder.Text = "";
            txtOpenBalance.Text = "";
            txtCrDr.Text = "Cr";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtDelAddress1.Text = "";
            txtDelAddress2.Text = "";
            txtDelAddress3.Text = "";
            txtArea.Text = "";
            txtDOB.Text = "";
            txtOffice.Text = "";
            txtMobile.Text = "";
            txtFax.Text = "";
            txtEMail.Text = "";
            txtPOC.Text = "";
            txtCSTNo.Text = "";
            txtRemarks.Text = "";
            txtLedgerCode.Text = "";
            txtAmount.Text = "";
            txtDays.Text = "";
            txtBills.Text = "";
            txtPAddLess.Text = "";
            txtSAddLess.Text = "";
            txtLName.Focus();
        }
        string ledgerGetName = "",LedgerNumber="";
        string tChkDuplicateLedgerName = "";
        string tChkDuplicateLedgerCode = "";
        private void frmLedgerCreation_Load(object sender, EventArgs e)
        {
            try
            {
                AlterLe = "True";
                if (passingvalues.LedgerName != "")
                {
                    tChkDuplicateLedgerName = passingvalues.LedgerName;
                    AlterLe = "False";
                    ledgerGetName = ""; LedgerNumber = "";
                    // SqlCommand cmd = new SqlCommand(@"SELECT dbo.Ledger_Grouptable.Ledger_groupname, dbo.Ledger_table.* FROM  dbo.Ledger_Grouptable INNER JOIN dbo.Ledger_table ON dbo.Ledger_Grouptable.Ledger_groupno = dbo.Ledger_table.Ledger_groupno Where ledger_name=@LedgerName", con);
                    //SqlCommand cmd = new SqlCommand(@"SELECT  Ledger_table.*,Ledger_Grouptable.Ledger_groupname FROM  Ledger_Grouptable,Ledger_table   Where Ledger_table.ledger_name=@LedgerName and Ledger_Grouptable.Ledger_groupgno=Ledger_table.Ledger_gno", con);
                    SqlCommand cmd = new SqlCommand(@"SELECT  Ledger_table.*,Ledger_Grouptable.Ledger_groupname FROM  Ledger_Grouptable,Ledger_table   Where Ledger_table.ledger_name=@LedgerName and Ledger_Grouptable.ledger_groupno=Ledger_table.ledger_groupno ", con);
                    
                    cmd.Parameters.AddWithValue("@LedgerName", passingvalues.LedgerName.ToString());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dtLedger = new DataTable();
                    adp.Fill(dtLedger);
                    if (dtLedger.Rows.Count > 0)
                    {
                        ledgerGetName = dtLedger.Rows[0]["Ledger_name"].ToString();
                        LedgerNumber = dtLedger.Rows[0]["Ledger_no"].ToString();

                        SqlCommand cmdChkSelect = new SqlCommand("Select * from SalRecv_table where SalRecv_led='" + LedgerNumber + "'", con);
                        SqlDataAdapter aqp = new SqlDataAdapter(cmdChkSelect);
                        DataTable dtChk = new DataTable();
                        dtChk.Rows.Clear();
                        aqp.Fill(dtChk);
                        if (dtChk.Rows.Count > 0)
                        {
                            txtUnder.Text = dtLedger.Rows[0]["Ledger_groupname"].ToString();
                            if (txtUnder.Text == "Sales men")
                            {
                                txtUnder.Enabled = false;
                            }
                            txtUnder.Enabled = false;
                        }
                        else
                        {
                            txtUnder.Enabled = true;
                            txtUnder.Text = dtLedger.Rows[0]["Ledger_groupname"].ToString();
                            if (txtUnder.Text == "Sales men")
                            {
                                txtUnder.Enabled = false;
                            }
                        }
                        txtLedgerCode.Text = dtLedger.Rows[0]["Ledger_code"].ToString();
                        txtLName.Text = dtLedger.Rows[0]["Ledger_name"].ToString();
                        txtLPName.Text = dtLedger.Rows[0]["Ledger_Printname"].ToString();
                        txtLAName.Text = dtLedger.Rows[0]["ledger_mtname"].ToString();
                       // txtUnder.Text = dtLedger.Rows[0]["Ledger_groupname"].ToString();
                        string indexvalues = string.Empty;
                        if (txtOpenBalance.Text.IndexOf("-") == -1)
                        {
                            txtOpenBalance.Text = dtLedger.Rows[0]["Ledger_open"].ToString().Replace("-", "");
                        }
                        else
                        {
                            txtOpenBalance.Text = dtLedger.Rows[0]["Ledger_open"].ToString();
                        }
                        tChkDuplicateLedgerName = passingvalues.LedgerName;
                        tChkDuplicateLedgerCode = dtLedger.Rows[0]["Ledger_code"].ToString();
                        if (Convert.ToDouble(dtLedger.Rows[0]["Ledger_open"].ToString().Trim()) <0)
                        {
                            txtCrDr.Text = "Cr";
                        }
                        else
                        {
                            txtCrDr.Text = "Dr";
                        }
                        txtAddress1.Text = dtLedger.Rows[0]["Ledger_Add1"].ToString();
                        txtAddress2.Text = dtLedger.Rows[0]["Ledger_Add2"].ToString();
                        txtAddress3.Text = dtLedger.Rows[0]["Ledger_Add3"].ToString();
                        txtDelAddress1.Text = dtLedger.Rows[0]["Ledger_Add4"].ToString();
                        txtDelAddress2.Text = dtLedger.Rows[0]["Ledger_Add5"].ToString();
                        txtDelAddress3.Text = dtLedger.Rows[0]["Ledger_Add6"].ToString();
                        txtArea.Text = dtLedger.Rows[0]["Area_no"].ToString();
                        string strDOB = Convert.ToDateTime(dtLedger.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
                        if (strDOB != "01/01/9999")
                        {                           
                            
                            txtDOB.Text = strDOB;
                        }
                        else
                        {
                            txtDOB.Text = "";
                        }
                        //txtDOB.Text = Convert.ToDateTime(dtLedger.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyyy");
                        txtOffice.Text = dtLedger.Rows[0]["Ledger_Offphone"].ToString();
                        txtMobile.Text = dtLedger.Rows[0]["Ledger_Cellphone"].ToString();
                        txtFax.Text = dtLedger.Rows[0]["Ledger_Resiphone"].ToString();
                        txtEMail.Text = dtLedger.Rows[0]["Ledger_Email"].ToString();
                        txtPOC.Text = dtLedger.Rows[0]["Ledger_St"].ToString();
                        txtCSTNo.Text = dtLedger.Rows[0]["Ledger_Cst"].ToString();
                        txtRemarks.Text = dtLedger.Rows[0]["Ledger_Remarks"].ToString();

                        txtAmount.Text = dtLedger.Rows[0]["Limit_Amount"].ToString();
                        txtBills.Text = dtLedger.Rows[0]["Limit_Bills"].ToString();
                        txtDays.Text = dtLedger.Rows[0]["Limit_Days"].ToString();
                        txtPAddLess.Text = dtLedger.Rows[0]["ledger_paddless"].ToString();
                        txtSAddLess.Text = dtLedger.Rows[0]["ledger_saddless"].ToString();
                        type_ = dtLedger.Rows[0]["Ledger_Type"].ToString();
                        if (type_ == "1")
                        {
                            txtType.Text = "Purchase";
                        }
                        else if (type_ == "2")
                        {
                            txtType.Text = "Sales";

                        }
                        else if (type_ == "3")
                        {
                            txtType.Text = "Default";
                        }

                        PurchaseRate = dtLedger.Rows[0]["Ledger_pcost"].ToString();
                        if (PurchaseRate.ToString().Trim() == "21")
                        {
                            txtPurchaseRate.Text = "Cost";

                        }
                        if (PurchaseRate == "24")
                        {
                            txtPurchaseRate.Text = "Default";

                        }
                        if (PurchaseRate == "22")
                        {
                            txtPurchaseRate.Text = "Mrp";

                        }
                        if (PurchaseRate == "20")
                        {
                            txtPurchaseRate.Text = "P.Rate";

                        }
                        if (PurchaseRate.ToString().Trim() == "23")
                        {
                            txtPurchaseRate.Text = "Special - 1";

                        }
                        if (PurchaseRate.ToString().Trim() == "18")
                        {
                            txtPurchaseRate.Text = "Special - 2";

                        }
                        if (PurchaseRate.ToString().Trim() == "19")
                        {
                            txtPurchaseRate.Text = "Special - 3";
                        }

                        SalesRate = dtLedger.Rows[0]["Ledger_scost"].ToString(); 

                        if (SalesRate.ToString().Trim() == "21")
                        {
                            txtSalesRate.Text = "Cost";

                        }
                        if (SalesRate == "24")
                        {
                            txtSalesRate.Text = "Default";

                        }
                        if (SalesRate == "22")
                        {
                            txtSalesRate.Text = "Mrp";

                        }
                        if (SalesRate == "20")
                        {
                            txtSalesRate.Text = "P.Rate";

                        }
                        if (SalesRate.ToString().Trim() == "23")
                        {
                            txtSalesRate.Text = "Special - 1";

                        }
                        if (SalesRate.ToString().Trim() == "18")
                        {
                            txtSalesRate.Text = "Special - 2";

                        }
                        if (SalesRate.ToString().Trim() == "19")
                        {
                            txtSalesRate.Text = "Special - 3";
                        }
                        CashType = dtLedger.Rows[0]["Cash_Type"].ToString();
                        if (CashType.ToString().Trim() == "0")
                        {
                            txtCashMode.Text = "Credit";
                        }
                        else
                        {
                            CashType = "Debit";
                        }
                        if (dtLedger.Rows[0]["Ledger_groupname"].ToString().Trim() == "Customer")
                        {
                            pnlCreditLimit.Visible = true;
                            pnlPriceDetails.Visible = true;
                            pnlSupplier.Visible = true;
                        }
                        else
                        {
                            pnlCreditLimit.Visible = false;
                            pnlPriceDetails.Visible = false;
                            pnlSupplier.Visible = false;
                        }
                        pnlListSelect.Visible = false;
                        txtLName.Focus();
                        if (txtUnder.Text == "Sales Men")
                        {
                            txtUnder.Enabled = false;
                        }
                    }
                    passingvalues.LedgerName = string.Empty;
                    if (txtUnder.Text == " ")
                    {
                        txtUnder.Enabled = false;
                    }
                }
                else
                {

                    pnlCreditLimit.Visible = false;
                    pnlPriceDetails.Visible = false;
                }
                pnlUnderName.Visible = false;
                AlterLe = "False";
                txtLName.Focus();
                AlterLe = "True";

                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //panel6.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
                if (txtUnder.Text == " ")
                {
                    txtUnder.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        //private void Validating()
        //{
        //    if (txtLName.Text == string.Empty)
        //    {
        //        MyMessageBox1.ShowBox("Enter Ledger Name, Please..");
        //    }
        //}

        private void GetLedgerNo()
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_no from Ledger_table where Ledger_name='" + txtLName.Text.Trim() + "'", con);
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    LedgerNo = Convert.ToInt16(dt.Rows[0]["Ledger_no"].ToString());
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
       
        string dupicate = "";
        private void txtLName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtLName.Text != string.Empty)
                    {
                        if (txtLName.Text.Trim() != ledgerGetName.ToString().Trim() && ledgerGetName.ToString().Trim()!="")
                        {
                            GetLedgerNo();
                            SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_name from Ledger_table where Ledger_no ='" + LedgerNo + "'", con);
                            dt.Rows.Clear();
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MyMessageBox1.ShowBox("Duplicate Ledger Name", "Warning");
                                dupicate = "YES";
                            }
                            else
                            {
                                txtLedgerCode.Focus();
                                dupicate = "NO";
                            }
                        }
                        else
                        {
                            txtLedgerCode.Focus();
                            dupicate = "NO";
                        }
                    }
                    else
                    {
                        MyMessageBox1.ShowBox("Enter Ledger Name", "Warning");
                        txtLName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtLPName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLAName.Focus();
            }
        }

        private void txtLAName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnder.Focus();
            }
        }

        private void txtUnder_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lstUnderName.SelectedIndex < lstUnderName.Items.Count - 1)
                    {
                        lstUnderName.SetSelected(lstUnderName.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lstUnderName.SelectedIndex > 0)
                    {
                        lstUnderName.SetSelected(lstUnderName.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (lstUnderName.SelectedIndex > -1)
                    {
                        txtUnder.Text = lstUnderName.SelectedItem.ToString();
                        string tUnder = lstUnderName.SelectedItem.ToString();
                        pnlUnderName.Visible = false;
                        if (tUnder.Trim() == "Supplier")
                        {
                            pnlSupplier.Visible = true;
                            pnlCreditLimit.Visible = true;
                            pnlPriceDetails.Visible = true;
                            pnlListSelect.Visible = false;
                            txtType.Text = "Purchase";
                        }
                        else if (tUnder.Trim() == "Customer")
                        {
                            pnlSupplier.Visible = true;
                            pnlCreditLimit.Visible = true;
                            pnlPriceDetails.Visible = true;
                            pnlListSelect.Visible = false;
                            pnlUnderName.Visible = false;
                            txtType.Text = "Sales";
                            txtCrDr.Visible = Visible;
                            txtAmount.Focus();
                        }
                        else
                        {
                            pnlSupplier.Visible = false;
                            pnlCreditLimit.Visible = false;
                            pnlPriceDetails.Visible = false;
                            pnlListSelect.Visible = false;
                        }
                    }
                    if (txtUnder.Text != string.Empty)
                    {
                        txtOpenBalance.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtUnder_Click(object sender, EventArgs e)
        {
            pnlUnderName.Visible = true;
        }

        private void lstUnderName_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUnderName.SelectedIndex > 0)
                {
                    if (txtUnder.Text == "Bank Accounts")
                    {
                        txtUnder.Text = lstUnderName.SelectedItem.ToString();
                        pnlUnderName.Visible = false;
                        pnlSupplier.Visible = false;
                    }
                    if (txtUnder.Text == "Supplier")
                    {
                        txtUnder.Text = lstUnderName.SelectedItem.ToString();
                        pnlUnderName.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnLedExit_Click(object sender, EventArgs e)
        {
            btnLedExit.BackColor = Color.LightCoral;
            this.Close();
        }

        private void btnLedExit_Enter(object sender, EventArgs e)
        {
            btnLedExit.BackColor = Color.Coral;
        }

        private void txtOpenBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtUnder.Text == "Supplier")
                {
                    if (txtOpenBalance.Text == string.Empty || txtOpenBalance.Text == "0.00")
                    {
                        txtCrDr.Focus();
                    }
                    else
                    {
                        txtCrDr.Focus();
                    }
                }
                else if (txtUnder.Text == "Bank Accounts")
                {
                    if (txtOpenBalance.Text == string.Empty || txtOpenBalance.Text == "0.00")
                    {
                        txtOpenBalance.Text = "0.00";
                        btnLSave.Focus();
                    }
                    else
                    {
                        btnLSave.Focus();
                    }
                }
                else if (txtUnder.Text.Trim() == "Customer")
                {
                    txtAddress1.Focus();
                }
            }
        }

        private void txtOpenBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void txtOpenBalance_Leave(object sender, EventArgs e)
        {
            txtDelAddress2_Leave(sender, e);


            if (txtOpenBalance.Text == string.Empty)
            {
                txtOpenBalance.Text = "0.00";
            }
        }

        private void txtOpenBalance_Click(object sender, EventArgs e)
        {
            txtOpenBalance.Text = "";
        }
        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtAddress1.Text == string.Empty || txtAddress1.Text == txtLName.Text.ToString())
                {
                    txtAddress1.Text = txtLName.Text.ToString();
                    txtAddress2.Focus();
                }
                else
                {
                    txtAddress2.Focus();
                }
            }
        }
        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress3.Focus();
            }
        }
        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDelAddress1.Focus();
            }
        }
        private void txtDelAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDelAddress2.Focus();
            }
        }
        private void txtDelAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDelAddress3.Focus();
            }
        }
        private void txtDelAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtArea.Focus();
            }
        }

        private void txtDOB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string vDate1 = txtDOB.Text;
                    string temp = DateTime.Now.ToString("dd/MM/yyyy");
                    string temp2 = temp.Substring(6);
                    DateTime parsed;
                    bool valid = DateTime.TryParseExact(vDate1, "dd/MM/yyyy",
                                                        CultureInfo.InvariantCulture,
                                                        DateTimeStyles.None,
                                                        out parsed);
                    if (valid == true)
                    {
                        string vDate2 = vDate1.Substring(6);
                        if (vDate2 != temp2)
                        {
                            DateTime Date1 = Convert.ToDateTime(vDate1);
                            DOB = Date1.ToString("dd/MM/yyyy");
                            txtOffice.Focus();
                        }
                        else
                        {
                            MyMessageBox1.ShowBox("Invalid date of birth", "Warning");
                            txtOffice.Focus();
                        }
                    }
                    else
                    {
                        MyMessageBox1.ShowBox("Invalid date of birth", "Warning");
                        txtOffice.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDOB.Focus();
            }
        }
        private void txtOffice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFax.Focus();
            }
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPOC.Focus();
            }
        }

        //private void txtCSTNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtRemark1.Focus();
        //    }
        //}
        //private void txtRemark1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtRemark2.Focus();
        //    }
        //}
        //private void txtRemarks2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtRemarks3.Focus();
        //    }
        //}
        private void txtRemarks3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmount.Focus();
            }
        }

        private void txtPOC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCSTNo.Focus();
            }
        }
        string type_ = "", PurchaseRate = "", SalesRate="",CashType="";
        private void btnLSave_Click(object sender, EventArgs e)        
        {
            try
            {
                btnLSave.BackColor = Color.Coral;
                int vLedgNo = 0, vGpgno = 0, vGpno = 0;
                try
                {
                    if (txtLName.Text != string.Empty)
                    {
                        if (CheckDuplicateLedgerName())
                        {
                            if (CheckDuplicateLedgerCode())
                            {
                                // if (dupicate.ToString().Trim() != "YES")
                                {
                                    if (LedgerNumber.ToString().Trim() == "")
                                    {
                                        if (txtUnder.Text.Trim() != "")
                                        {
                                            if (txtUnder.Text == "Supplier")
                                            {
                                                SqlCommand cmd = new SqlCommand("select max(LedgerNo)+1 from Numbertable", con);

                                                // con.Close();
                                                //   con.Open();
                                                if (cmd.ExecuteScalar() != DBNull.Value)
                                                {
                                                    vLedgNo = Convert.ToInt32(cmd.ExecuteScalar());
                                                }

                                                //  con.Close();
                                                if (txtLedgerCode.Text != "" && txtLedgerCode.Text != string.Empty)
                                                {
                                                    SqlCommand cmdUpdate = new SqlCommand("Update numbertable set Customer_No=Customer_No+1", con);
                                                    cmdUpdate.ExecuteNonQuery();
                                                }
                                                SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_groupgno,Ledger_groupno from Ledger_Grouptable where Ledger_groupname='" + txtUnder.Text.Trim() + "' ", con);
                                                dt.Rows.Clear();
                                                adp.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    vGpgno = Convert.ToInt16(dt.Rows[0]["Ledger_groupgno"].ToString());
                                                    vGpno = Convert.ToInt32(dt.Rows[0]["Ledger_groupno"].ToString());
                                                }

                                                string vLName, vPLName, vALName;
                                                vLName = txtLName.Text.ToUpper();
                                                vPLName = txtLPName.Text.ToUpper();
                                                vALName = txtLAName.Text.ToUpper();

                                                SqlCommand cmd_Insert = new SqlCommand(@"Insert into DBO.Ledger_table(Ledger_no,Ledger_gno,Prty_Prefix,Prty_No,Prty_Suffix,Prty_Number,Prty_MtNumber,Ledger_Code,Ledger_name,Ledger_mtname,Ledger_Printname,Ledger_mtPrintname,Alias_name,Alias_mtname,Ledger_groupno,Area_no,Ledger_baltype,Ledger_Add1,Ledger_Add2,Ledger_Add3,Ledger_Add4,Ledger_Add5,Ledger_Add6,Ledger_Resiphone,Ledger_Offphone,Ledger_Cellphone,Ledger_Email,DOB,Mechanic_Commi,Sman_Commi,Mechanic_CommiTx,Ledger_Cst,Ledger_St,Ledger_VAT,Tax_Open,ledger_open,ledger_openOrg,ledger_openSus,Ledger_clos,Ledger_closOrg,Ledger_closSus,ledger_Type,ledger_pcost,ledger_scost,ledger_paddless,ledger_saddless,Limit_Days,Limit_Bills,Limit_Amount,ref_no,ref_type,Update_flag,Ledger_flag,Cash_Type,Ledger_Pos,ExciseRange,ExciseDivision,ExciseCollectrate,ExciseRegnNo,PanNo,ECCNo,Ledger_Remarks,Ledger_BillLimits,Ledger_TransPort,Ledger_Courier,Ledger_Photo) VALUES (@vLedgNo,@vGpgno,'','0','','','','0',@LName,@vLName,@PLName,@vPLName,@ALName,@vALName,@vGpno,'1','true',@LAddr1,@LAddr2,@LAddr3,@DLAddr1,@DLAddr2,@DLAddr3,@Fax,@OffPhone,@Mobile,@EMail,@DOB,'0','0','0','','','0','0',@OpenBal,@OpenBal,'0','0','0','0','1','24','24','0','0','0','0','0','0','','0','0','0','0','','','','','','','','0','','','')", con);
                                                SqlCommand cmd2_Insert = new SqlCommand(@"Insert into dbo.Ledsel_table(Ledger_no,Ledsel_name,Ledsel_mtname,Ledger_Gno,Ledger_Type) values(@vLedgNo,@LName,@vLName,@vGpgno,'1')", con);
                                                SqlCommand cmd2 = new SqlCommand("Update NumberTable set LedgerNo=LedgerNo+1", con);
                                                {
                                                    cmd_Insert.Parameters.AddWithValue("@vLedgNo", vLedgNo);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpno", vGpno);
                                                    cmd_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@PLName", txtLPName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ALName", txtLAName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@vLName", vLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vPLName", vPLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vALName", vALName);
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr1", txtAddress1.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr2", (txtAddress2.Text.Trim() == "") ? "" : txtAddress2.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr3", (txtAddress3.Text.Trim() == "") ? "" : txtAddress3.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DLAddr1", (txtDelAddress1.Text.Trim() == "") ? "" : txtDelAddress1.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DLAddr2", (txtDelAddress2.Text.Trim() == "") ? "" : txtDelAddress2.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DLAddr3", (txtDelAddress3.Text.Trim() == "") ? "" : txtDelAddress3.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@Fax", (txtFax.Text.Trim() == "") ? "" : txtFax.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@OffPhone", (txtOffice.Text.Trim() == "") ? "" : txtOffice.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@Mobile", (txtMobile.Text.Trim() == "") ? "" : txtMobile.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@EMail", (txtEMail.Text.Trim() == "") ? "" : txtEMail.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "01/01/9999" : DOB);
                                                    //cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "" : DOB);


                                                    SqlCommand cmdVchInsert = new SqlCommand(@"INSERT INTO Vch_table([Sno],[Vch_Sno],[Vch_Pre],[Vch_NoLong],[Vch_Suf],[Vch_No],[Vch_MtNo],[Ctr_no],[UserNo],[RepNo],[Vch_Party],[ref_no],[ref_det],[Vch_Date],[Vch_type],[ledger_no],[ledger_no1],[Dr_amount],[Cr_amount],[Vch_Remarks],[Vch_IndRemarks],[Vch_Cancel],[Vch_CRemarks],[Vch_flag])   VALUES
           (0,0,'',0,'','','',@tCtr_no,@tUserNo,0,0,0,0,'2014-03-31',255,@tledger_no,0,@tDr_amount,@tCr_amount,'' ,'',0,'',0)", con);
                                                    cmdVchInsert.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                    cmdVchInsert.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                    cmdVchInsert.Parameters.AddWithValue("@tledger_no", vLedgNo);
                                                       
                                                   // cmd_Insert.Parameters.AddWithValue("@OpenBal", (txtOpenBalance.Text.Trim() == "") ? "" : txtOpenBalance.Text);
                                                    if (txtCrDr.Text == "Dr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount",txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount",0);
                                                       
                                                    }
                                                    else if (txtCrDr.Text == "Cr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : -Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", 0);
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount",  txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        

                                                    }
                                                    cmd2_Insert.Parameters.AddWithValue("@vLedgNo", vLedgNo);
                                                    cmd2_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd2_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd2_Insert.Parameters.AddWithValue("@vLName", vLName);

                                                    //con.Close();
                                                    //con.Open();
                                                    cmd_Insert.ExecuteNonQuery();
                                                    cmd2_Insert.ExecuteNonQuery();
                                                    cmd2.ExecuteNonQuery();
                                                    MyMessageBox.ShowBox("LedgerName Saved Successfully","Message");
                                                    if ((txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text)) > 0)
                                                    {
                                                        cmdVchInsert.ExecuteNonQuery();
                                                    }
                                                    // con.Close();
                                                    Clear();
                                                }
                                            }
                                            else if (txtUnder.Text == "Sales Men")
                                            {
                                                SqlCommand cmd = new SqlCommand("select max(LedgerNo)+1 from Numbertable", con);                                                
                                                if (cmd.ExecuteScalar() != DBNull.Value)
                                                {
                                                    vLedgNo = Convert.ToInt32(cmd.ExecuteScalar());
                                                }
                                                if (txtLedgerCode.Text != "" && txtLedgerCode.Text != string.Empty)
                                                {
                                                    SqlCommand cmdUpdate = new SqlCommand("Update numbertable set Customer_No=Customer_No+1", con);
                                                    cmdUpdate.ExecuteNonQuery();
                                                }
                                                SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_groupgno,Ledger_groupno from Ledger_Grouptable where Ledger_groupname='" + txtUnder.Text.Trim() + "' ", con);
                                                dt.Rows.Clear();
                                                adp.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    vGpgno = Convert.ToInt16(dt.Rows[0]["Ledger_groupgno"].ToString());
                                                    vGpno = Convert.ToInt32(dt.Rows[0]["Ledger_groupno"].ToString());
                                                }
                                                
                                                string vLName, vPLName, vALName;
                                                vLName = txtLName.Text.ToUpper();
                                                vPLName = txtLPName.Text.ToUpper();
                                                vALName = txtLAName.Text.ToUpper();

                                                SqlCommand cmd_Insert = new SqlCommand(@"Insert into DBO.Ledger_table(Ledger_no,Ledger_gno,Prty_Prefix,Prty_No,Prty_Suffix,Prty_Number,Prty_MtNumber,Ledger_Code,Ledger_name,Ledger_mtname,Ledger_Printname,Ledger_mtPrintname,Alias_name,Alias_mtname,Ledger_groupno,Area_no,Ledger_baltype,Ledger_Add1,Ledger_Add2,Ledger_Add3,Ledger_Add4,Ledger_Add5,Ledger_Add6,Ledger_Resiphone,Ledger_Offphone,Ledger_Cellphone,Ledger_Email,DOB,Mechanic_Commi,Sman_Commi,Mechanic_CommiTx,Ledger_Cst,Ledger_St,Ledger_VAT,Tax_Open,ledger_open,ledger_openOrg,ledger_openSus,Ledger_clos,Ledger_closOrg,Ledger_closSus,ledger_Type,ledger_pcost,ledger_scost,ledger_paddless,ledger_saddless,Limit_Days,Limit_Bills,Limit_Amount,ref_no,ref_type,Update_flag,Ledger_flag,Cash_Type,Ledger_Pos,ExciseRange,ExciseDivision,ExciseCollectrate,ExciseRegnNo,PanNo,ECCNo,Ledger_Remarks,Ledger_BillLimits,Ledger_TransPort,Ledger_Courier,Ledger_Photo) VALUES (@vLedgNo,@vGpgno,'','0','','','','0',@LName,@vLName,@PLName,@vPLName,@ALName,@vALName,@vGpno,'1','true',@LAddr1,@LAddr2,@LAddr3,@DLAddr1,@DLAddr2,@DLAddr3,@Fax,@OffPhone,@Mobile,@EMail,@DOB,'0','0','0','','','0','0',@OpenBal,@OpenBal,'0','0','0','0','1','24','24','0','0','0','0','0','0','','0','0','0','0','','','','','','','','0','','','')", con);
                                                SqlCommand cmd2_Insert = new SqlCommand(@"Insert into dbo.Ledsel_table(Ledger_no,Ledsel_name,Ledsel_mtname,Ledger_Gno,Ledger_Type) values(@vLedgNo,@LName,@vLName,@vGpgno,'1')", con);
                                                SqlCommand cmd2 = new SqlCommand("Update NumberTable set LedgerNo=LedgerNo+1", con);
                                                {
                                                    cmd_Insert.Parameters.AddWithValue("@vLedgNo", vLedgNo);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpno", vGpno);
                                                    cmd_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@PLName", txtLPName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ALName", txtLAName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@vLName", vLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vPLName", vPLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vALName", vALName);
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr1", txtAddress1.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr2", (txtAddress2.Text.Trim() == "") ? "" : txtAddress2.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr3", (txtAddress3.Text.Trim() == "") ? "" : txtAddress3.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DLAddr1", (txtDelAddress1.Text.Trim() == "") ? "" : txtDelAddress1.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DLAddr2", (txtDelAddress2.Text.Trim() == "") ? "" : txtDelAddress2.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DLAddr3", (txtDelAddress3.Text.Trim() == "") ? "" : txtDelAddress3.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@Fax", (txtFax.Text.Trim() == "") ? "" : txtFax.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@OffPhone", (txtOffice.Text.Trim() == "") ? "" : txtOffice.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@Mobile", (txtMobile.Text.Trim() == "") ? "" : txtMobile.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@EMail", (txtEMail.Text.Trim() == "") ? "" : txtEMail.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "01/01/9999" : DOB);
                                                    //cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "" : DOB);


                                                    SqlCommand cmdVchInsert = new SqlCommand(@"INSERT INTO Vch_table([Sno],[Vch_Sno],[Vch_Pre],[Vch_NoLong],[Vch_Suf],[Vch_No],[Vch_MtNo],[Ctr_no],[UserNo],[RepNo],[Vch_Party],[ref_no],[ref_det],[Vch_Date],[Vch_type],[ledger_no],[ledger_no1],[Dr_amount],[Cr_amount],[Vch_Remarks],[Vch_IndRemarks],[Vch_Cancel],[Vch_CRemarks],[Vch_flag])   VALUES
           (0,0,'',0,'','','',@tCtr_no,@tUserNo,0,0,0,0,'2014-03-31',255,@tledger_no,0,@tDr_amount,@tCr_amount,'' ,'',0,'',0)", con);
                                                    cmdVchInsert.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                    cmdVchInsert.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                    cmdVchInsert.Parameters.AddWithValue("@tledger_no", vLedgNo);
                                                       
                                                   // cmd_Insert.Parameters.AddWithValue("@OpenBal", (txtOpenBalance.Text.Trim() == "") ? "" : txtOpenBalance.Text);
                                                    if (txtCrDr.Text == "Dr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount",txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount",0);
                                                       
                                                    }
                                                    else if (txtCrDr.Text == "Cr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : -Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", 0);
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount",  txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        

                                                    }
                                                    cmd2_Insert.Parameters.AddWithValue("@vLedgNo", vLedgNo);
                                                    cmd2_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd2_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd2_Insert.Parameters.AddWithValue("@vLName", vLName);

                                                    //con.Close();
                                                    //con.Open();
                                                    cmd_Insert.ExecuteNonQuery();
                                                    cmd2_Insert.ExecuteNonQuery();
                                                    cmd2.ExecuteNonQuery();
                                                    MyMessageBox.ShowBox("LedgerName Saved Successfully", "Message");
                                                    if ((txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text)) > 0)
                                                    {
                                                        cmdVchInsert.ExecuteNonQuery();
                                                    }
                                                    // con.Close();
                                                    Clear();
                                                }
                                                
                                            }
                                            else if (txtUnder.Text == "Bank Accounts")
                                            {
                                                int LedgNo = 0;
                                                SqlCommand cmd = new SqlCommand("Select LedgerNo+1 from NumberTable", con);
                                                // con.Close();
                                                // con.Open();
                                                if (cmd.ExecuteScalar() != DBNull.Value)
                                                {
                                                    LedgNo = Convert.ToInt32(cmd.ExecuteScalar());
                                                }

                                                //  con.Close();

                                                if (txtLedgerCode.Text != "" && txtLedgerCode.Text != string.Empty)
                                                {
                                                    SqlCommand cmdUpdate = new SqlCommand("Update numbertable set Customer_No=Customer_No+1", con);
                                                    cmdUpdate.ExecuteNonQuery();
                                                }

                                                SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_groupgno,Ledger_groupno from Ledger_Grouptable where Ledger_groupname='" + txtUnder.Text.Trim() + "' ", con);
                                                dt.Rows.Clear();
                                                adp.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    vGpgno = Convert.ToInt16(dt.Rows[0]["Ledger_groupgno"].ToString());
                                                    vGpno = Convert.ToInt32(dt.Rows[0]["Ledger_groupno"].ToString());
                                                }

                                                string vLName, vPLName, vALName;
                                                vLName = txtLName.Text.ToUpper();
                                                vPLName = txtLPName.Text.ToUpper();
                                                vALName = txtLAName.Text.ToUpper();
                                                SqlCommand cmd_Insert = new SqlCommand(@"Insert into DBO.Ledger_table(Ledger_no,Ledger_gno,Prty_Prefix,Prty_No,Prty_Suffix,Prty_Number,Prty_MtNumber,Ledger_Code,Ledger_name,Ledger_mtname,Ledger_Printname,Ledger_mtPrintname,Alias_name,Alias_mtname,Ledger_groupno,Area_no,Ledger_baltype,Ledger_Add1,Ledger_Add2,Ledger_Add3,Ledger_Add4,Ledger_Add5,Ledger_Add6,Ledger_Resiphone,Ledger_Offphone,Ledger_Cellphone,Ledger_Email,DOB,Mechanic_Commi,Sman_Commi,Mechanic_CommiTx,Ledger_Cst,Ledger_St,Ledger_VAT,Tax_Open,ledger_open,ledger_openOrg,ledger_openSus,Ledger_clos,Ledger_closOrg,Ledger_closSus,ledger_Type,ledger_pcost,ledger_scost,ledger_paddless,ledger_saddless,Limit_Days,Limit_Bills,Limit_Amount,ref_no,ref_type,Update_flag,Ledger_flag,Cash_Type,Ledger_Pos,ExciseRange,ExciseDivision,ExciseCollectrate,ExciseRegnNo,PanNo,ECCNo,Ledger_Remarks,Ledger_BillLimits,Ledger_TransPort,Ledger_Courier,Ledger_Photo) 
                                                        VALUES(@vLedgNo,@vGpgno,'','0','','','','0',@LName,@vLName,@PLName,@vPLName,@ALName,@vALName,@vGpno,'1','true',@LAddr1,'','','','','','','','','',@DOB,'0','0','0','','','0','0',@OpenBal,@OpenBal,'0','0','0','0','0','24','24','0','0','0','0','0','0','','0','0','0','0','','','','','','','','0','','','')", con);
                                                SqlCommand cmd2_Insert = new SqlCommand(@"Insert into dbo.Ledsel_table(Ledger_no,Ledsel_name,Ledsel_mtname,Ledger_Gno,Ledger_Type) values(@vLedgNo,@LName,@vLName,@vGpgno,'0')", con);
                                                SqlCommand cmd2 = new SqlCommand("Update NumberTable set LedgerNo=LedgerNo+1", con);
                                                {
                                                    cmd_Insert.Parameters.AddWithValue("@vLedgNo", LedgNo);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpno", vGpno);
                                                    cmd_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@PLName", txtLPName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ALName", txtLAName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@vLName", vLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vPLName", vPLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vALName", vALName);
                                                    cmd_Insert.Parameters.AddWithValue("@LAddr1", txtLName.Text.Trim());
                                                    //cmd_Insert.Parameters.AddWithValue("@OpenBal", (Convert.ToDouble(txtOpenBalance.Text.Trim()) == 0) ? 0 :-Convert.ToDouble(txtOpenBalance.Text));

                                                    SqlCommand cmdVchInsert = new SqlCommand(@"INSERT INTO Vch_table([Sno],[Vch_Sno],[Vch_Pre],[Vch_NoLong],[Vch_Suf],[Vch_No],[Vch_MtNo],[Ctr_no],[UserNo],[RepNo],[Vch_Party],[ref_no],[ref_det],[Vch_Date],[Vch_type],[ledger_no],[ledger_no1],[Dr_amount],[Cr_amount],[Vch_Remarks],[Vch_IndRemarks],[Vch_Cancel],[Vch_CRemarks],[Vch_flag])   VALUES
           (0,0,'',0,'','','',@tCtr_no,@tUserNo,0,0,0,0,'2014-03-31',255,@tledger_no,0,@tDr_amount,@tCr_amount,'' ,'',0,'',0)", con);
                                                    cmdVchInsert.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                    cmdVchInsert.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                    cmdVchInsert.Parameters.AddWithValue("@tledger_no", LedgNo);

                                                    if (txtCrDr.Text == "Dr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount", 0);

                                                    }
                                                    else if (txtCrDr.Text == "Cr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : -Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", 0);
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                    }
                                                    cmd_Insert.Parameters.AddWithValue("@DOB", "01/01/9999");
                                                    cmd2_Insert.Parameters.AddWithValue("@vLedgNo", LedgNo);
                                                    cmd2_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd2_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd2_Insert.Parameters.AddWithValue("@vLName", vLName);
                                                    // con.Close();
                                                    //con.Open();
                                                    cmd_Insert.ExecuteNonQuery();
                                                    cmd2_Insert.ExecuteNonQuery();
                                                    cmd2.ExecuteNonQuery();
                                                    MyMessageBox.ShowBox("LedgerName Saved Successfully", "Message");
                                                    if ((txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text)) > 0)
                                                    {
                                                        cmdVchInsert.ExecuteNonQuery();
                                                    }
                                                    // con.Close();
                                                    Clear();
                                                }
                                            }
                                            else if (txtUnder.Text.Trim() == "Customer")
                                            {
                                                PurchaseType();
                                                if (txtLedgerCode.Text != "" && txtLedgerCode.Text != string.Empty)
                                                {
                                                    SqlCommand cmdUpdate = new SqlCommand("Update numbertable set Customer_No=Customer_No+1 ", con);
                                                    cmdUpdate.ExecuteNonQuery();
                                                }
                                                int LedgNo = 0;
                                                SqlCommand cmd = new SqlCommand("Select LedgerNo+1 from NumberTable", con);
                                                // con.Close();
                                                //   con.Open();
                                                if (cmd.ExecuteScalar() != DBNull.Value)
                                                {
                                                    LedgNo = Convert.ToInt32(cmd.ExecuteScalar());
                                                }
                                                // con.Close();
                                                SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_groupgno,Ledger_groupno from Ledger_Grouptable where Ledger_groupname='" + txtUnder.Text.Trim() + "' ", con);
                                                dt.Rows.Clear();
                                                adp.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    vGpgno = Convert.ToInt16(dt.Rows[0]["Ledger_groupgno"].ToString());
                                                    vGpno = Convert.ToInt32(dt.Rows[0]["Ledger_groupno"].ToString());
                                                }
                                                string vLName, vPLName, vALName;
                                                vLName = txtLName.Text.ToUpper();
                                                vPLName = txtLPName.Text.ToUpper();
                                                vALName = txtLAName.Text.ToUpper();

                                                SqlCommand cmd_Insert = new SqlCommand("insert into Ledger_table(Ledger_no,Ledger_gno,Prty_Prefix,Prty_No,Prty_Suffix,Prty_Number,Prty_MtNumber,Ledger_Code,Ledger_name,Ledger_mtname,Ledger_Printname,Ledger_mtPrintname,Alias_name,Alias_mtname,Ledger_groupno,Area_no,Ledger_baltype,Ledger_Add1,Ledger_Add2,Ledger_Add3,Ledger_Add4,Ledger_Add5,Ledger_Add6,Ledger_Resiphone,Ledger_Offphone,ledger_cellphone,ledger_Email,Dob,Mechanic_Commi,Sman_Commi,Mechanic_CommiTx,ledger_Cst,Ledger_St,Ledger_VAT,Tax_Open,ledger_open,ledger_openOrg,ledger_openSus,ledger_clos,Ledger_closOrg,Ledger_closSus,ledger_Type,ledger_pcost,ledger_scost,ledger_paddless,ledger_saddless,Limit_Days,Limit_Bills,Limit_Amount,ref_no,ref_type,Update_flag,Ledger_flag,Cash_Type,Ledger_Pos,ExciseRange,ExciseDivision,ExciseCollectrate,ExciseRegnNo,PanNo,ECCNo,Ledger_Remarks,Ledger_BillLimits,ledger_transPort,Ledger_Courier,Ledger_Photo) values(@vLedgNo,@vGpgno,'','0','','','',@LedgerCode,@LName,@PLName,@PLName,@vPLName,@ALName,@vALName,@vGpno,1,1,@AddressLine1,@AddressLine2,@AddressLine3,@AddressLine4,@AddressLine5,@AddressLine6,@Regsi,@OfficePhone,@CellPhoneNo,@Email,@DOB,0,0,0,@LedgerCst,@LedgerSt,0,0,@OpenBal,@OpenBal,0,0,0,0,@ledger_type,@ledger_pcost,@ledger_scost,@ledger_paddless,@ledger_saddless,@limit_Days,@limit_Bills,@limit_Amount,0,'',0,0,@Cash_Type,0,'','','','','','','',0,'','','')", con);
                                                SqlCommand cmd2_Insert = new SqlCommand(@"Insert into dbo.Ledsel_table(Ledger_no,Ledsel_name,Ledsel_mtname,Ledger_Gno,Ledger_Type) values(@vLedgNo,@LName,@vLName,@vGpgno,'0')", con);
                                                SqlCommand cmd2 = new SqlCommand("Update NumberTable set LedgerNo=LedgerNo+1", con);
                                                {
                                                    cmd_Insert.Parameters.AddWithValue("@vLedgNo", LedgNo);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpno", vGpno);
                                                    cmd_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@PLName", txtLPName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ALName", txtLAName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@vLName", vLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vPLName", vPLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vALName", vALName);
                                                    //cmd_Insert.Parameters.AddWithValue("@LAddr1", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine1", txtAddress1.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine2", txtAddress2.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine3", txtAddress3.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine4", txtDelAddress1.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine5", txtDelAddress2.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine6", txtDelAddress3.Text.Trim());

                                                    SqlCommand cmdVchInsert = new SqlCommand(@"INSERT INTO Vch_table([Sno],[Vch_Sno],[Vch_Pre],[Vch_NoLong],[Vch_Suf],[Vch_No],[Vch_MtNo],[Ctr_no],[UserNo],[RepNo],[Vch_Party],[ref_no],[ref_det],[Vch_Date],[Vch_type],[ledger_no],[ledger_no1],[Dr_amount],[Cr_amount],[Vch_Remarks],[Vch_IndRemarks],[Vch_Cancel],[Vch_CRemarks],[Vch_flag])   VALUES
           (0,0,'',0,'','','',@tCtr_no,@tUserNo,0,0,0,0,'2014-03-31',255,@tledger_no,0,@tDr_amount,@tCr_amount,'' ,'',0,'',0)", con);
                                                    cmdVchInsert.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                    cmdVchInsert.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                    cmdVchInsert.Parameters.AddWithValue("@tledger_no", LedgNo);

                                                    if (txtCrDr.Text == "Dr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount", 0);

                                                    }
                                                    else if (txtCrDr.Text == "Cr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : -Convert.ToDouble(txtOpenBalance.Text));

                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", 0);
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                    }
                                                    cmd_Insert.Parameters.AddWithValue("@LedgerCode", txtLedgerCode.Text.ToString().Trim() == "" ? "" : txtLedgerCode.Text.ToString().Trim());

                                                    cmd_Insert.Parameters.AddWithValue("@Regsi", (txtFax.Text.Trim() == "") ? "" : txtFax.Text);

                                                    cmd_Insert.Parameters.AddWithValue("@Mobile", (txtMobile.Text.Trim() == "") ? "" : txtMobile.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@EMail", (txtEMail.Text.Trim() == "") ? "" : txtEMail.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "01/01/9999" : DOB);
                                                    //cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "" : DOB);

                                                    cmd_Insert.Parameters.AddWithValue("@LedgerCst", txtCSTNo.Text.ToString().Trim() == "" ? "" : txtCSTNo.Text.ToString().Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@LedgerSt", txtPOC.Text == "" ? "" : txtPOC.Text.ToString().Trim());


                                                    //Price Details List:
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_type", type_.ToString().Trim() == "" ? "0" : type_.ToString().Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_pcost", PurchaseRate.ToString().Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_scost", SalesRate.ToString().Trim() == "" ? "0" : SalesRate.ToString().Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_paddless", txtPAddLess.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_saddless", txtSAddLess.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@Cash_Type", CashType.ToString().Trim() == "" ? "0" : CashType.ToString().Trim());
                                                    //Credit Limit:
                                                    cmd_Insert.Parameters.AddWithValue("@limit_Amount", txtAmount.Text.Trim() == "" ? "0" : txtAmount.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@limit_Bills", txtBills.Text.ToString().Trim() == "" ? "0" : txtBills.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@limit_Days", txtDays.Text.ToString().Trim() == "" ? "0" : txtDays.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@OfficePhone", txtOffice.Text.ToString().Trim() == "" ? "" : txtOffice.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@CellPhoneNo", txtMobile.Text.ToString().Trim() == "" ? "" : txtMobile.Text.Trim());
                                                    // cmd_Insert.Parameters.AddWithValue("@Email", txtEMail.Text.ToString().Trim() == "" ? "" : txtEMail.Text.Trim());

                                                    cmd2_Insert.Parameters.AddWithValue("@vLedgNo", LedgNo.ToString().Trim() == "" ? "" : LedgNo.ToString());
                                                    cmd2_Insert.Parameters.AddWithValue("@vGpgno", vGpgno.ToString().Trim() == "" ? "" : vGpgno.ToString());
                                                    cmd2_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim().Trim() == "" ? "" : txtLName.Text.Trim());
                                                    cmd2_Insert.Parameters.AddWithValue("@vLName", vLName.ToString().Trim() == "" ? "" : vLName.ToString());
                                                    //   con.Close();
                                                    //  con.Open();
                                                    cmd_Insert.ExecuteNonQuery();
                                                    cmd2_Insert.ExecuteNonQuery();
                                                    cmd2.ExecuteNonQuery();
                                                    MyMessageBox.ShowBox("LedgerName Saved Successfully", "Message");
                                                    if ((txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text)) > 0)
                                                    {
                                                        cmdVchInsert.ExecuteNonQuery();
                                                    }
                                                    //    con.Close();
                                                    Clear();
                                                }
                                            }
                                            else if (txtUnder.Text.Trim() == "Expenses(Mfg / Trading)" || txtUnder.Text.Trim() == "Expenses(Admin)")
                                            {
                                                if (txtLedgerCode.Text != "" && txtLedgerCode.Text != string.Empty)
                                                {
                                                    SqlCommand cmdUpdate = new SqlCommand("Update numbertable set Customer_No=Customer_No+1 ", con);
                                                    cmdUpdate.ExecuteNonQuery();
                                                }
                                                int LedgNo = 0;
                                                SqlCommand cmd = new SqlCommand("Select LedgerNo+1 from NumberTable", con);
                                                // con.Close();
                                                //   con.Open();
                                                if (cmd.ExecuteScalar() != DBNull.Value)
                                                {
                                                    LedgNo = Convert.ToInt32(cmd.ExecuteScalar());
                                                }
                                                // con.Close();
                                                SqlDataAdapter adp = new SqlDataAdapter("Select Ledger_groupgno,Ledger_groupno from Ledger_Grouptable where Ledger_groupname='" + txtUnder.Text.Trim() + "' ", con);
                                                dt.Rows.Clear();
                                                adp.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    vGpgno = Convert.ToInt16(dt.Rows[0]["Ledger_groupgno"].ToString());
                                                    vGpno = Convert.ToInt32(dt.Rows[0]["Ledger_groupno"].ToString());
                                                }
                                                string vLName, vPLName, vALName;
                                                vLName = txtLName.Text.ToUpper();
                                                vPLName = txtLPName.Text.ToUpper();
                                                vALName = txtLAName.Text.ToUpper();
                                                //New Alter 
                                                SqlCommand cmd_Insert = new SqlCommand("insert into Ledger_table(Ledger_no,Ledger_gno,Prty_Prefix,Prty_No,Prty_Suffix,Prty_Number,Prty_MtNumber,Ledger_Code,Ledger_name,Ledger_mtname,Ledger_Printname,Ledger_mtPrintname,Alias_name,Alias_mtname,Ledger_groupno,Area_no,Ledger_baltype,Ledger_Add1,Ledger_Add2,Ledger_Add3,Ledger_Add4,Ledger_Add5,Ledger_Add6,Ledger_Resiphone,Ledger_Offphone,ledger_cellphone,ledger_Email,Dob,Mechanic_Commi,Sman_Commi,Mechanic_CommiTx,ledger_Cst,Ledger_St,Ledger_VAT,Tax_Open,ledger_open,ledger_openOrg,ledger_openSus,ledger_clos,Ledger_closOrg,Ledger_closSus,ledger_Type,ledger_pcost,ledger_scost,ledger_paddless,ledger_saddless,Limit_Days,Limit_Bills,Limit_Amount,ref_no,ref_type,Update_flag,Ledger_flag,Cash_Type,Ledger_Pos,ExciseRange,ExciseDivision,ExciseCollectrate,ExciseRegnNo,PanNo,ECCNo,Ledger_Remarks,Ledger_BillLimits,ledger_transPort,Ledger_Courier,Ledger_Photo) values(@vLedgNo,@vGpgno,'','0','','','',@LedgerCode,@LName,@PLName,@PLName,@vPLName,@ALName,@vALName,@vGpno,1,1,@AddressLine1,@AddressLine2,@AddressLine3,@AddressLine4,@AddressLine5,@AddressLine6,@Regsi,@OfficePhone,@CellPhoneNo,@Email,@DOB,0,0,0,@LedgerCst,@LedgerSt,0,0,@OpenBal,@OpenBal,0,0,0,0,@ledger_type,@ledger_pcost,@ledger_scost,@ledger_paddless,@ledger_saddless,@limit_Days,@limit_Bills,@limit_Amount,0,'',0,0,@Cash_Type,0,'','','','','','','',0,'','','')", con);
                                                SqlCommand cmd2_Insert = new SqlCommand(@"Insert into dbo.Ledsel_table(Ledger_no,Ledsel_name,Ledsel_mtname,Ledger_Gno,Ledger_Type) values(@vLedgNo,@LName,@vLName,@vGpgno,'0')", con);
                                                SqlCommand cmd2 = new SqlCommand("Update NumberTable set LedgerNo=LedgerNo+1", con);
                                                {
                                                    cmd_Insert.Parameters.AddWithValue("@vLedgNo", LedgNo);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpgno", vGpgno);
                                                    cmd_Insert.Parameters.AddWithValue("@vGpno", vGpno);
                                                    cmd_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@PLName", txtLPName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ALName", txtLAName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@vLName", vLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vPLName", vPLName);
                                                    cmd_Insert.Parameters.AddWithValue("@vALName", vALName);
                                                    //cmd_Insert.Parameters.AddWithValue("@LAddr1", txtLName.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine1", txtAddress1.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine2", txtAddress2.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine3", txtAddress3.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine4", txtDelAddress1.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine5", txtDelAddress2.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@AddressLine6", txtDelAddress3.Text.Trim());


                                                    SqlCommand cmdVchInsert = new SqlCommand(@"INSERT INTO Vch_table([Sno],[Vch_Sno],[Vch_Pre],[Vch_NoLong],[Vch_Suf],[Vch_No],[Vch_MtNo],[Ctr_no],[UserNo],[RepNo],[Vch_Party],[ref_no],[ref_det],[Vch_Date],[Vch_type],[ledger_no],[ledger_no1],[Dr_amount],[Cr_amount],[Vch_Remarks],[Vch_IndRemarks],[Vch_Cancel],[Vch_CRemarks],[Vch_flag])   VALUES
           (0,0,'',0,'','','',@tCtr_no,@tUserNo,0,0,0,0,'2014-03-31',255,@tledger_no,0,@tDr_amount,@tCr_amount,'' ,'',0,'',0)", con);
                                                    cmdVchInsert.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                    cmdVchInsert.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                    cmdVchInsert.Parameters.AddWithValue("@tledger_no", vLedgNo);

                                                    if (txtCrDr.Text == "Dr")
                                                    {
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));


                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount", 0);


                                                    }
                                                    else if (txtCrDr.Text == "Cr")
                                                    {
                                                        double OpeninCreditBakance = 0.00;
                                                        OpeninCreditBakance = txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text);
                                                        cmd_Insert.Parameters.AddWithValue("@OpenBal", -OpeninCreditBakance);


                                                        cmdVchInsert.Parameters.AddWithValue("@tDr_amount", 0);
                                                        cmdVchInsert.Parameters.AddWithValue("@tCr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                    }
                                                    cmd_Insert.Parameters.AddWithValue("@LedgerCode", txtLedgerCode.Text.ToString().Trim() == "" ? "" : txtLedgerCode.Text.ToString().Trim());

                                                    cmd_Insert.Parameters.AddWithValue("@Regsi", (txtFax.Text.Trim() == "") ? "" : txtFax.Text);

                                                    cmd_Insert.Parameters.AddWithValue("@Mobile", (txtMobile.Text.Trim() == "") ? "" : txtMobile.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@EMail", (txtEMail.Text.Trim() == "") ? "" : txtEMail.Text);
                                                    cmd_Insert.Parameters.AddWithValue("@DOB", (DOB == "") ? "01/01/9999" : DOB);

                                                    cmd_Insert.Parameters.AddWithValue("@LedgerCst", txtCSTNo.Text.ToString().Trim() == "" ? "" : txtCSTNo.Text.ToString().Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@LedgerSt", txtPOC.Text == "" ? "" : txtPOC.Text.ToString().Trim());


                                                    //Price Details List:
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_type", type_.ToString().Trim() == "" ? "0" : "0");
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_pcost", PurchaseRate.ToString().Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_scost", SalesRate.ToString().Trim() == "" ? "0" : "0");
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_paddless", "");
                                                    cmd_Insert.Parameters.AddWithValue("@ledger_saddless", "");
                                                    cmd_Insert.Parameters.AddWithValue("@Cash_Type", "0");
                                                    //Credit Limit:
                                                    cmd_Insert.Parameters.AddWithValue("@limit_Amount", txtAmount.Text.Trim() == "" ? "0" : "0");
                                                    cmd_Insert.Parameters.AddWithValue("@limit_Bills", txtBills.Text.ToString().Trim() == "" ? "0" : "0");
                                                    cmd_Insert.Parameters.AddWithValue("@limit_Days", txtDays.Text.ToString().Trim() == "" ? "0" : "0");
                                                    cmd_Insert.Parameters.AddWithValue("@OfficePhone", txtOffice.Text.ToString().Trim() == "" ? "" : txtOffice.Text.Trim());
                                                    cmd_Insert.Parameters.AddWithValue("@CellPhoneNo", txtMobile.Text.ToString().Trim() == "" ? "" : txtMobile.Text.Trim());
                                                    // cmd_Insert.Parameters.AddWithValue("@Email", txtEMail.Text.ToString().Trim() == "" ? "" : txtEMail.Text.Trim());

                                                    cmd2_Insert.Parameters.AddWithValue("@vLedgNo", LedgNo.ToString().Trim() == "" ? "" : LedgNo.ToString());
                                                    cmd2_Insert.Parameters.AddWithValue("@vGpgno", vGpgno.ToString().Trim() == "" ? "" : vGpgno.ToString());
                                                    cmd2_Insert.Parameters.AddWithValue("@LName", txtLName.Text.Trim().Trim() == "" ? "" : txtLName.Text.Trim());
                                                    cmd2_Insert.Parameters.AddWithValue("@vLName", vLName.ToString().Trim() == "" ? "" : vLName.ToString());
                                                    //   con.Close();
                                                    //  con.Open();
                                                    cmd_Insert.ExecuteNonQuery();
                                                    cmd2_Insert.ExecuteNonQuery();
                                                    cmd2.ExecuteNonQuery();
                                                    MyMessageBox.ShowBox("LedgerName Saved Successfully", "Message");
                                                    if ((txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text)) > 0)
                                                    {
                                                        cmdVchInsert.ExecuteNonQuery();
                                                    }
                                                    //    con.Close();
                                                    Clear();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MyMessageBox.ShowBox("Select Ledger Under Value", "Warning");
                                            txtUnder.Focus();

                                        }
                                    }
                                   
                                    else if (LedgerNumber.ToString().Trim() != string.Empty)
                                    {
                                        string tExexute = "";
                                        SqlDataAdapter adpDup = new SqlDataAdapter("Select Ledger_no from Ledger_table where Ledger_name='" + txtLName.Text.Trim() + "'", con);
                                        dt.Rows.Clear();
                                        adpDup.Fill(dt);
                                        if (dt.Rows.Count > 0)
                                        {
                                            if (tChkDuplicateLedgerName != txtLName.Text.Trim())
                                            {
                                                MyMessageBox1.ShowBox("Duplicate Ledger Name", "Warning");
                                                tExexute = "";
                                                txtLName.Focus();
                                            }
                                            else
                                            {
                                                tExexute = "Execute";
                                            }
                                        }
                                        else
                                        {
                                            tExexute = "Execute";
                                        }
                                        if (tExexute == "Execute")
                                        {
                                            //if (con.State != ConnectionState.Open)
                                            //{
                                            //    con.Open();
                                            //}
                                            SqlCommand adp = new SqlCommand("Select Ledger_groupgno,Ledger_groupno from Ledger_Grouptable where Ledger_groupname=@LedgerName", con);
                                            adp.Parameters.AddWithValue("@LedgerName", txtUnder.Text.Trim());
                                            SqlDataAdapter apd1 = new SqlDataAdapter(adp);
                                            DataTable dt1 = new DataTable();
                                            dt1.Rows.Clear();
                                            apd1.Fill(dt1);
                                            if (dt1.Rows.Count > 0)
                                            {
                                                vGpgno = Convert.ToInt16(dt1.Rows[0]["Ledger_groupgno"].ToString());
                                                vGpno = Convert.ToInt32(dt1.Rows[0]["Ledger_groupno"].ToString());
                                            }
                                            SqlCommand cmdUpdateLedger = new SqlCommand("Update Ledger_table set Ledger_gno='" + vGpgno + "',prty_prefix='',Prty_no=0,prty_suffix='',prty_number='',prty_MtNumber='',ledger_code=@LedgerCode,Ledger_name=@LedgerName,Ledger_Mtname=Upper(@LedgerName),Ledger_Printname=@PrinterName,Ledger_Mtprintname=Upper(@PrinterName),Alias_name=@AliesName,Alias_Mtname=Upper(@AliesName),Ledger_groupno=@LGroupNO,Ledger_Add1=@Address1,Ledger_Add2=@Address2,Ledger_Add3=@Address3,Ledger_Add4=@Address4,Ledger_Add5=@Address5,Ledger_Add6=@Address6,Ledger_Resiphone=@RegsiPhone,Ledger_OffPhone=@office,Ledger_cellPhone=@CellPhone,Ledger_Email=@EmailName,DOB=@DOB,Mechanic_Commi=0,Sman_Commi=0,Mechanic_CommiTx=0,Ledger_Cst=@LedgerCst,Ledger_St=@LedgerPOC,Ledger_Vat=0,Tax_Open=0,Ledger_open=@LedgerOpenBal,Ledger_openOrg=@LedgerOpenBal,Ledger_openSus=0,Ledger_clos=0,Ledger_closOrg=0,Ledger_closSus=0,ledger_pcost=@ledger_pcost,ledger_scost=@ledger_scost,ledger_type=@ledger_type,ledger_paddless=@ledger_paddless,ledger_saddless=@ledger_saddless,limit_Amount=@limit_Amount,limit_Bills=@limit_Bills,limit_Days=@limit_Days,ref_no=0,ref_type='',update_flag=0,ledger_flag=0,Cash_Type=@Cash_Type Where Ledger_no=@LedgerNo", con);
                                            cmdUpdateLedger.Parameters.AddWithValue("@LedgerCode", txtLedgerCode.Text.ToString().Trim() == "" ? "0" : txtLedgerCode.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@LedgerName", txtLName.Text.ToString().Trim() == "" ? "" : txtLName.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@PrinterName", txtLPName.Text.ToString().Trim() == "" ? "" : txtLPName.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@AliesName", txtLAName.Text.ToString().Trim() == "" ? "" : txtLAName.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@LGroupNO", vGpno.ToString());
                                            cmdUpdateLedger.Parameters.AddWithValue("@Address1", txtAddress1.Text.ToString().Trim() == "" ? "" : txtAddress1.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@Address2", txtAddress2.Text.ToString().Trim() == "" ? "" : txtAddress2.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@Address3", txtAddress3.Text.ToString().Trim() == "" ? "" : txtAddress3.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@Address4", txtDelAddress1.Text.ToString().Trim() == "" ? "" : txtDelAddress1.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@Address5", txtDelAddress2.Text.ToString().Trim() == "" ? "" : txtDelAddress2.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@Address6", txtDelAddress3.Text.ToString().Trim() == "" ? "" : txtDelAddress3.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@DOB", txtDOB.Text.ToString().Trim() == "" ? "" : txtDOB.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@RegsiPhone", txtFax.Text.ToString().Trim() == "" ? "" : txtFax.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@office", txtOffice.Text.ToString().Trim() == "" ? "" : txtOffice.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@CellPhone", txtMobile.Text.ToString().Trim() == "" ? "" : txtMobile.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@EmailName", txtEMail.Text.ToString().Trim() == "" ? "" : txtEMail.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@LedgerCst", txtCSTNo.Text.ToString().Trim() == "" ? "" : txtCSTNo.Text.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@LedgerPOC", txtPOC.Text.ToString().Trim() == "" ? "" : txtPOC.Text.ToString().Trim());
                                           // cmdUpdateLedger.Parameters.AddWithValue("@LedgerOpenBal", txtOpenBalance.Text.Trim() == "" ? "0" : txtOpenBalance.Text.ToString().Trim());


                                            DataTable dtSelect = new DataTable();
                                            dtSelect.Rows.Clear();
                                            SqlCommand cmdSelect = new SqlCommand("Select * from Vch_table where ledger_no=@tledger_no and sno=0 and vch_type=255",con);
                                            cmdSelect.Parameters.AddWithValue("@tledger_no", LedgerNumber);
                                            SqlDataAdapter adpSelect = new SqlDataAdapter(cmdSelect);
                                            adpSelect.Fill(dtSelect);
                                            if (dtSelect.Rows.Count > 0)
                                            {
                                                SqlCommand cmdVchUpdate = new SqlCommand(@"Update Vch_table SET [Ctr_no]=@tCtr_no,[UserNo]=@tUserNo,[Dr_amount]=@tDr_amount,[Cr_amount]=@tCr_amount where ledger_no=@tledger_no and sno=0 and vch_type=255", con);
                                                cmdVchUpdate.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                cmdVchUpdate.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                cmdVchUpdate.Parameters.AddWithValue("@tledger_no", LedgerNumber);


                                                if (txtCrDr.Text == "Dr")
                                                {
                                                    cmdUpdateLedger.Parameters.AddWithValue("@LedgerOpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                    cmdVchUpdate.Parameters.AddWithValue("@tDr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                    cmdVchUpdate.Parameters.AddWithValue("@tCr_amount", 0);

                                                }
                                                else if (txtCrDr.Text == "Cr")
                                                {
                                                    double OpeninCreditBakance = 0.00;
                                                    OpeninCreditBakance = txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text);
                                                    cmdUpdateLedger.Parameters.AddWithValue("@LedgerOpenBal", -OpeninCreditBakance);

                                                    cmdVchUpdate.Parameters.AddWithValue("@tDr_amount", 0);
                                                    cmdVchUpdate.Parameters.AddWithValue("@tCr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                }
                                                cmdVchUpdate.ExecuteNonQuery();

                                            }
                                            else
                                            {

                                                SqlCommand cmdVchInsert = new SqlCommand(@"INSERT INTO Vch_table([Sno],[Vch_Sno],[Vch_Pre],[Vch_NoLong],[Vch_Suf],[Vch_No],[Vch_MtNo],[Ctr_no],[UserNo],[RepNo],[Vch_Party],[ref_no],[ref_det],[Vch_Date],[Vch_type],[ledger_no],[ledger_no1],[Dr_amount],[Cr_amount],[Vch_Remarks],[Vch_IndRemarks],[Vch_Cancel],[Vch_CRemarks],[Vch_flag])   VALUES
           (0,0,'',0,'','','',@tCtr_no,@tUserNo,0,0,0,0,'2014-03-31',255,@tledger_no,0,@tDr_amount,@tCr_amount,'' ,'',0,'',0)", con);
                                                cmdVchInsert.Parameters.AddWithValue("@tCtr_no", (_Class.clsVariables.tCounter == "") ? "1" : _Class.clsVariables.tCounter);
                                                cmdVchInsert.Parameters.AddWithValue("@tUserNo", (_Class.clsVariables.tUserNo == "") ? "0" : _Class.clsVariables.tUserNo);
                                                cmdVchInsert.Parameters.AddWithValue("@tledger_no", LedgerNumber);
                                                if (txtCrDr.Text == "Dr")
                                                {
                                                    cmdUpdateLedger.Parameters.AddWithValue("@LedgerOpenBal", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));

                                                    cmdVchInsert.Parameters.AddWithValue("@tDr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                    cmdVchInsert.Parameters.AddWithValue("@tCr_amount", 0);
                                                }
                                                else if (txtCrDr.Text == "Cr")
                                                {
                                                    double OpeninCreditBakance = 0.00;
                                                    OpeninCreditBakance = txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text);
                                                    cmdUpdateLedger.Parameters.AddWithValue("@LedgerOpenBal", -OpeninCreditBakance);


                                                    cmdVchInsert.Parameters.AddWithValue("@tDr_amount", 0);
                                                    cmdVchInsert.Parameters.AddWithValue("@tCr_amount", txtOpenBalance.Text.ToString().Trim() == "" ? 0 : Convert.ToDouble(txtOpenBalance.Text));
                                                }
                                                cmdVchInsert.ExecuteNonQuery();
                                            }
                                            PurchaseType();
                                            cmdUpdateLedger.Parameters.AddWithValue("@ledger_pcost", PurchaseRate.ToString().Trim() == "" ? "0" : PurchaseRate.ToString().Trim());
                                            
                                            cmdUpdateLedger.Parameters.AddWithValue("@ledger_scost", SalesRate.ToString().Trim() == "" ? "0" : SalesRate.ToString().Trim());
                                            if (txtType.Text.Trim() == "Purchase")
                                            {
                                                type_ = "1";
                                            }
                                            else if (txtType.Text.Trim() == "Sales")
                                            {
                                                type_ = "2";
                                            }
                                            else
                                            {
                                                type_ = "3";
                                            }
                                            cmdUpdateLedger.Parameters.AddWithValue("@ledger_type", type_.ToString().Trim() == "" ? "0" : type_.ToString().Trim());

                                            cmdUpdateLedger.Parameters.AddWithValue("@ledger_paddless", txtPAddLess.Text.Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@ledger_saddless", txtSAddLess.Text.Trim());

                                            cmdUpdateLedger.Parameters.AddWithValue("@limit_Amount", txtAmount.Text.Trim() == "" ? "0" : txtAmount.Text.Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@limit_Bills", txtBills.Text.ToString().Trim() == "" ? "0" : txtBills.Text.Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@limit_Days", txtDays.Text.ToString().Trim() == "" ? "0" : txtDays.Text.Trim());

                                            cmdUpdateLedger.Parameters.AddWithValue("@Cash_Type", CashType.ToString().Trim() == "" ? "0" : CashType.ToString().Trim());
                                            cmdUpdateLedger.Parameters.AddWithValue("@LedgerNo", LedgerNumber);
                                            cmdUpdateLedger.ExecuteNonQuery();

                                            MyMessageBox.ShowBox("LedgerName Updated Successfully", "Message");
                                            
                                            this.Close();
                                        }
                                    }
                                }
                                //else
                                //{
                                //    MyMessageBox1.ShowBox("Duplicate Ledger Name", "Warning");
                                //    txtLName.Focus();
                                //}
                            }
                        }
                    }
                    else
                    {
                        MyMessageBox1.ShowBox("Enter ledger name", "Warning");
                        txtLName.Focus();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void PurchaseType()
        {
            //Purchase Type 
            type_ = ""; PurchaseRate = ""; SalesRate = "";
            if (txtType.Text.Trim() == "Purchase")
            {
                type_ = "1";
            }
            else if (txtType.Text.Trim() == "Sales")
            {
                type_ = "2";
            }
            else
            {
                type_ = "3";
            }
            if (txtPurchaseRate.Text.Trim() == "Cost")
            {
                PurchaseRate = "21";
            }
            if (txtPurchaseRate.Text.Trim() == "Default")
            {
                PurchaseRate = "24";
            }
            if (txtPurchaseRate.Text.Trim() == "Mrp")
            {
                PurchaseRate = "22";
            }
            if (txtPurchaseRate.Text.Trim() == "P.Rate")
            {
                PurchaseRate = "20";
            }
            if (txtPurchaseRate.Text.Trim() == "Special - 1")
            {
                PurchaseRate = "23";
            }
            if (txtPurchaseRate.Text.Trim() == "Special - 2")
            {
                PurchaseRate = "18";
            }
            if (txtPurchaseRate.Text.Trim() == "Special - 3")
            {
                PurchaseRate = "19";
            }

            //Sales Rate 
            if (txtSalesRate.Text.Trim() == "Cost")
            {
                SalesRate = "21";
            }
            if (txtSalesRate.Text.Trim() == "Default")
            {
                SalesRate = "24";
            }
            if (txtSalesRate.Text.Trim() == "Mrp")
            {
                SalesRate = "22";
            }
            if (txtSalesRate.Text.Trim() == "P.Rate")
            {
                SalesRate = "20";
            }
            if (txtSalesRate.Text.Trim() == "Special - 1")
            {
                SalesRate = "23";
            }
            if (txtSalesRate.Text.Trim() == "Special - 2")
            {
                SalesRate = "18";
            }
            if (txtSalesRate.Text.Trim() == "Special - 3")
            {
                SalesRate = "19";
            }
            if (txtCashMode.Text.Trim() == "Credit")
            {
                CashType = "0";
            }
            else
            {
                CashType = "1";
            }
        }
        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            txtDOB.Text = dtpDOB.Value.ToString("dd/MM/yyyy");
            txtDOB.Focus();
        }
        string AlterLe = "";
        private void txtUnder_Enter(object sender, EventArgs e)
        
        {
            if (AlterLe != "False")
            {
                txtAddress2_Enter(sender, e);

                pnlUnderName.Visible = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (txtUnder.Text.ToString().Trim() == dt.Rows[i]["Ledger_groupname"].ToString())
                    {
                        lstUnderName.SetSelected(i, true);
                    }
                }
            }
            
        }

        private void txtUnder_Leave(object sender, EventArgs e)
        {
            txtDelAddress2_Leave(sender,e);
            pnlUnderName.Visible = false;
        }
        private void txtOpenBalance_TextChanged(object sender, EventArgs e)
        {
            if (txtOpenBalance.Text != "0.00" && txtOpenBalance.Text != "")
            {
                txtCrDr.Visible = true;
            }
        }

        private void txtCrDr_DoubleClick(object sender, EventArgs e)
        {
            if (txtCrDr.Text == "Cr")
            {
                txtCrDr.Text = "Dr";
            }
            else
            {
                txtCrDr.Text = "Cr";
            }
        }

        string chk1, chk;
        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
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
        bool isChk = false;
        private void txtUnder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (AlterLe.ToString().Trim() != "False")
                {
                    if (txtUnder.Text.Trim() != null && txtUnder.Text.Trim() != "")
                    {
                        pnlUnderName.Visible = true;
                        lstUnderName.Visible = true;
                        SqlCommand cmd = new SqlCommand("Select Ledger_groupname from Ledger_Grouptable where Ledger_groupname like @LedgerGroupName", con);
                        cmd.Parameters.AddWithValue("@LedgerGroupName", txtUnder.Text.Trim() + '%');
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dtGroupLedgerSelect = new DataTable();
                        dtGroupLedgerSelect.Rows.Clear();
                        adp.Fill(dtGroupLedgerSelect);
                        isChk = false;
                        if (dtGroupLedgerSelect.Rows.Count > 0)
                        {
                            string tempstr = dtGroupLedgerSelect.Rows[0]["Ledger_groupname"].ToString().Trim();
                            for (int k = 0; k < lstUnderName.Items.Count; k++)
                            {
                                if (tempstr == lstUnderName.Items[k].ToString().Trim())
                                {
                                    isChk = true;
                                    lstUnderName.SetSelected(k, true);
                                    txtUnder.Select();
                                    chk = "1";
                                    txtUnder.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "2";
                            if (txtUnder.Text != "")
                            {
                                string name = txtUnder.Text.Remove(txtUnder.Text.Length - 1);
                                txtUnder.Text = name.ToString();
                                txtUnder.Select(txtUnder.Text.Length, 0);
                            }
                            txtUnder.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            chk = "1";
                        }
                        else
                        {
                            chk = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void btnLSave_MouseHover(object sender, EventArgs e)
        {
            btnLSave.BackColor = Color.LightBlue;
        }

        private void btnLedExit_MouseHover(object sender, EventArgs e)
        {
            btnLedExit.BackColor = Color.LightBlue;
        }

        private void btnLSave_MouseLeave(object sender, EventArgs e)
        {
            btnLSave.BackColor = SystemColors.ButtonFace;
        }

        private void btnLedExit_MouseLeave(object sender, EventArgs e)
        {
            btnLedExit.BackColor = SystemColors.ButtonFace;
        }

        private void txtCrDr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress1.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (txtCrDr.Text.ToString().Trim() == "Cr")
                {
                    txtCrDr.Text = "Dr";
                }
                else
                {
                    txtCrDr.Text = "Cr";
                }
            }
        }
        private void txtType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (AlterLe.ToString().Trim() != "False")
                {
                    if (txtType.Text.Trim() != null && txtType.Text.Trim() != "")
                    {
                        for (int i = 0; i < listSelect.Items.Count; i++)
                        {
                            chkStr1 = listSelect.Items[i].ToString();
                            if (txtType.Text.Length <= chkStr1.Length)
                            {
                                chkstr2 = chkStr1.Substring(0, txtType.Text.Length);
                                bool isChk = false;
                                if (txtType.Text.Trim() == chkstr2 || txtType.Text.Trim() == chkstr2.ToLower())
                                {
                                    isChk = true;
                                    listSelect.SetSelected(i, true);
                                    txtType.Select();
                                    chk = "1";
                                    txtType.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);

                                    break;
                                }
                                if (isChk == false)
                                {
                                    chk = "2";
                                    txtType.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                }
                            }
                        }
                    }
                    else
                    {
                        chk = "1";
                    }
                    txtPurchaseRate.Enabled = false;
                    txtPAddLess.Enabled = false;
                    txtSalesRate.Enabled = false;
                    txtSAddLess.Enabled = false;
                    txtCashMode.Enabled = false;
                    if (txtType.Text == "Both")
                    {
                        txtPurchaseRate.Enabled = true;
                        txtPAddLess.Enabled = true;
                        txtSalesRate.Enabled = true;
                        txtSAddLess.Enabled = true;
                        txtCashMode.Enabled = true;
                        txtPurchaseRate.Text = "Default";
                        txtSalesRate.Text = "Default";
                        txtCashMode.Text = "Credit";
                    }
                    else if (txtType.Text == "Sales")
                    {
                        txtSalesRate.Enabled = true;
                        txtSAddLess.Enabled = true;
                        txtCashMode.Enabled = true;
                        txtSalesRate.Text = "Default";
                        txtCashMode.Text = "Credit";
                    }
                    else if (txtType.Text == "Purchase")
                    {
                        txtPurchaseRate.Enabled = true;
                        txtPAddLess.Enabled = true;
                        //txtPurchaseRate.Text = "Default";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtType_Enter(object sender, EventArgs e)        
        {

            txtAddress2_Enter(sender, e);
            tListSelectType = "Type";
            funListLoad();
        }

        string tListSelectType = "";
        public void funListLoad()
        {
            try
            {
                pnlListSelect.Visible = true;
                listSelect.Items.Clear();
                if (tListSelectType == "Type")
                {
                    listSelect.Items.Add("Both");
                    listSelect.Items.Add("Purchase");
                    listSelect.Items.Add("Sales");
                    listSelect.SelectedItem = "Both";
                }
                else if (tListSelectType == "Purchase" || tListSelectType == "Sales")
                {
                    listSelect.Items.Add("Cost");
                    listSelect.Items.Add("Default");
                    listSelect.Items.Add("Mrsp");
                    listSelect.Items.Add("Ndp");
                    listSelect.Items.Add("Special - 1");
                    listSelect.Items.Add("Special - 2");
                    listSelect.Items.Add("Special - 3");
                    listSelect.SelectedItem = "Default";
                }
                else if (tListSelectType == "Cash Mode")
                {
                    listSelect.Items.Add("Cash");
                    listSelect.Items.Add("Credit");
                    listSelect.SelectedItem = "Credit";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void txtPurchaseRate_Enter(object sender, EventArgs e)
        {
            try
            {
                txtAddress2_Enter(sender, e);
                tListSelectType = "Purchase";
                funListLoad();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtSalesRate_Enter(object sender, EventArgs e)
        {
            try
            {
                txtAddress2_Enter(sender, e);
                tListSelectType = "Sales";
                funListLoad();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string chkStr1 = "", chkstr2="";
        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPurchaseRate.Text.Trim() != null && txtPurchaseRate.Text.Trim() != "")
                {
                    for (int i = 0; i <listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txtPurchaseRate.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0,txtPurchaseRate.Text.Length);
                            bool isChk = false;
                            if (txtPurchaseRate.Text.Trim() == chkstr2 || txtPurchaseRate.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                               listSelect.SetSelected(i, true);
                                txtPurchaseRate.Select();
                                chk = "1";
                                txtPurchaseRate.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtPurchaseRate.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        //string chk = "";
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar))
                {
                    if (chk == "2")
                    {
                        e.Handled = true;
                        // chk = "1";
                    }
                    else
                    {
                        e.Handled = false;

                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtPurchaseRate_KeyDown(object sender, KeyEventArgs e)        
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (listSelect.SelectedIndex < listSelect.Items.Count - 1)
                    {
                        listSelect.SetSelected(listSelect.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (listSelect.SelectedIndex > 0)
                    {
                        listSelect.SetSelected(listSelect.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (tListSelectType == "Type")
                    {
                        pnlListSelect.Visible = false;
                        if (listSelect.SelectedItems.Count > 0)
                        {
                           txtType.Text = listSelect.SelectedItem.ToString();
                        }
                       txtPurchaseRate.Select();
                    }
                    if (tListSelectType == "Purchase")
                    {
                        pnlListSelect.Visible = false;
                        if (listSelect.SelectedItems.Count > 0)
                        {
                            txtPurchaseRate.Text = listSelect.SelectedItem.ToString();
                        }
                        txtPAddLess.Select();
                    }
                    else if (tListSelectType == "Sales")
                    {
                        pnlListSelect.Visible = false;
                        if (listSelect.SelectedItems.Count > 0)
                        {
                           txtSalesRate.Text = listSelect.SelectedItem.ToString();
                        }
                        txtSAddLess.Select();
                    }
                    else if (tListSelectType == "Cash Mode")
                    {
                        pnlListSelect.Visible = false;
                        if (listSelect.SelectedItems.Count > 0)
                        {
                           txtCashMode.Text = listSelect.SelectedItem.ToString();
                        }
                       btnLSave.Select();
                    }
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            try
           {
                if (e.KeyCode == Keys.Down)
                {
                    if (listSelect.SelectedIndex < listSelect.Items.Count - 1)
                    {
                        listSelect.SetSelected(listSelect.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (listSelect.SelectedIndex > 0)
                    {
                        listSelect.SetSelected(listSelect.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    pnlListSelect.Visible = false;
                    if (listSelect.SelectedItems.Count > 0)
                    {
                      txtType.Text = listSelect.SelectedItem.ToString();
                    }
                 //   txtPurchaseRatetxtPurchaseRate.Select();
                    if (txtType.Text == "Both")
                    {
                        txtPAddLess.Focus();
                        txtPurchaseRate.ReadOnly = true;
                        txtPAddLess.Focus();
                    }
                    else if (txtType.Text == "Sales")
                    {
                        txtSalesRate.Focus();
                    }
                    else if (txtType.Text == "Purchase")
                    {
                        txtPurchaseRate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void txtSalesRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSalesRate.Text.Trim() != null && txtSalesRate.Text.Trim() != "")
                {
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txtSalesRate.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtSalesRate.Text.Length);
                            bool isChk = false;
                            if (txtSalesRate.Text.Trim() == chkstr2 || txtSalesRate.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                listSelect.SetSelected(i, true);
                                txtSalesRate.Select();
                                chk = "1";
                                txtSalesRate.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtSalesRate.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtCashMode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCashMode.Text.Trim() != null && txtCashMode.Text.Trim() != "")
                {
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txtCashMode.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtCashMode.Text.Length);
                            bool isChk = false;
                            if (txtCashMode.Text.Trim() == chkstr2 || txtCashMode.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                listSelect.SetSelected(i, true);
                                txtCashMode.Select();
                                chk = "1";
                                txtCashMode.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtCashMode.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtCashMode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCashMode_Enter(object sender, EventArgs e)
        {
            try
            {
                txtAddress2_Enter(sender, e);
                tListSelectType = "Cash Mode";
                funListLoad();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBills.Focus();
            }
        }

        private void txtBills_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDays.Focus();
            }
        }

        private void txtDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtType.Focus();
            }
        }

        private void txtPAddLess_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtAddress2_Enter(sender, e);
                    txtSalesRate.ReadOnly = true;
                    txtSAddLess.Focus();
                    if (txtType.Text == "Purchase")
                    {
                        btnLSave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtSAddLess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLSave.Focus();
            }
        }
        private void txtCSTNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmount.Focus();
            }
        }
        string CustomerNumber = "";
        private void txtLedgerCode_Enter(object sender, EventArgs e)
        {

            try
            {
                if (passingvalues.LedgerName != "")
                {
                    CustomerNumber = "";
                    //if (con.State != ConnectionState.Open)
                    //{
                    //    con.Open();
                    //}
                    SqlCommand cmd = new SqlCommand("select Customer_No+1 from numbertable", con);
                    CustomerNumber = Convert.ToString(cmd.ExecuteScalar()).ToString();
                    txtLedgerCode.Text = CustomerNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }



        }
        private void txtLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtLedgerCode.Text != string.Empty && txtLedgerCode.Text != "0" && txtLedgerCode.Text != "0.00")
                {
                    CheckDuplicateLedgerCode();
                }
                else
                {
                    txtLPName.Focus();
                }
            }
        }


        private bool CheckDuplicateLedgerName()
        {
            
            if (txtLName.Text.Trim() != "")
            {
                SqlDataAdapter adpDup = new SqlDataAdapter("Select Ledger_no from Ledger_table where Ledger_name='" + txtLName.Text.Trim() + "'", con);
                dt.Rows.Clear();
                adpDup.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (tChkDuplicateLedgerName != txtLName.Text.Trim())
                    {
                        MyMessageBox1.ShowBox("Duplicate Ledger Name", "Warning");
                        txtLName.Focus();
                        return false;
                        
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        private bool CheckDuplicateLedgerCode()
        {
            if (txtLedgerCode.Text.ToString().Trim() != "" && txtLedgerCode.Text != "0" && txtLedgerCode.Text != "0.00")
            {
                if (tChkDuplicateLedgerCode != txtLedgerCode.Text.Trim())
                {
                    DataTable dt_code = new DataTable();
                    dt_code.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("select Ledger_Code from Ledger_table where ledger_Code=@LedgerCode", con);
                    cmd.Parameters.AddWithValue("@LedgerCode", txtLedgerCode.Text.ToString().Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt_code);
                    if (dt_code.Rows.Count > 0)
                    {
                        MyMessageBox.ShowBox("Already Exit This Code", "Warning");
                        txtLedgerCode.Focus();
                        return false;
                    }
                    else
                    {
                        txtLPName.Focus();
                        return true;
                    }
                }
            }
            else
            {
                txtLPName.Focus();
            }
            return true;
        }

        private void txtOpenBalance_Enter(object sender, EventArgs e)
        {
            try
            {
                txtAddress2_Enter(sender, e);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }


            pnlUnderName.Visible = false;
        }
        
        //All Textbox clear
        private void ClearTextBoxes(Control.ControlCollection cc)
        {
            //foreach (Control ctrl in cc)
            //{
            //    TextBox tb = ctrl as TextBox;
            //   // if (tb != null)
            //        tb.text="";
            //    //else
            //    //    ClearTextBoxes(ctrl.Controls);
            //}
        }
        private void txtAddress2_Enter(object sender, EventArgs e)
        {
            try
            {
                //if (txtName.Focus() == true)
                {
                  //  ClearTextBoxes(this.Controls);

                    TextBox txtBox = (TextBox)sender;
                    txtBox.Focus();
                    txtBox.BackColor = Color.LightBlue; 
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
       
        private void txtDelAddress2_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            txtBox.BackColor = Color.White;
        }

        private void frmLedgerCreation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }      

    }
}
