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

namespace MSPOSBACKOFFICE
{
    public partial class UserCreation : Form
    {
        public UserCreation()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public Boolean Validate()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select * from User_table Where user_Name<>@USerName and User_Pass=@Password", con);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtUserCheck = new DataTable();
            dtUserCheck.Rows.Clear();
            adp.Fill(dtUserCheck);
            if (dtUserCheck.Rows.Count > 0)
            {
                MyMessageBox.ShowBox("Please Eneter Valid Password", "Warning");
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                txtPassword.Focus();
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
          {
            try
            {
                double tDiscountRange=0;
                if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "" && txtCounter.Text.Trim() != "" && !string.IsNullOrEmpty(CmbSystemName.Text))
                {
                    if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
                    {
                        MessageBox.Show("Password is not match!!");
                    }
                    else
                    {
                        tDiscountRange = (txtDiscountRange.Text.Trim() == "") ? 0 : double.Parse(txtDiscountRange.Text.Trim());
                        if (tDiscountRange <= 100)
                        {
                            if (Validate())
                            {
                                con.Close();
                                con.Open();
                                DataTable dtNew = new DataTable();
                                dtNew.Rows.Clear();
                                SqlCommand cmd = new SqlCommand("Select * from user_table where user_name=@tUsername", con);
                                cmd.Parameters.AddWithValue("@tUsername", txtUserName.Text.Trim());
                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                adp.Fill(dtNew);
                                if (dtNew.Rows.Count > 0)
                                {
                                    MyMessageBox.ShowBox("User Name Already Exist", "Warning");
                                    txtUserName.Focus();
                                }
                                else
                                {
                                    DataTable dtNew1 = new DataTable();
                                    dtNew1.Rows.Clear();
                                    SqlCommand cmd1 = new SqlCommand("Select * from user_table where user_pass=@tUsername", con);
                                    cmd1.Parameters.AddWithValue("@tUsername", txtPassword.Text.Trim());
                                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                                    adp1.Fill(dtNew1);
                                    if (dtNew1.Rows.Count > 0)
                                    {
                                        MyMessageBox.ShowBox("User password Already Exist", "Warning");
                                        txtPassword.Text = string.Empty;
                                        txtConfirmPassword.Text = string.Empty;
                                        txtPassword.Focus();
                                    }
                                    else
                                    {
                                        SqlCommand sp_cmd = new SqlCommand("sp_User_Insert", con);
                                        sp_cmd.CommandType = CommandType.StoredProcedure;
                                        sp_cmd.Parameters.AddWithValue("@tUserName", txtUserName.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tUserType", cmbUserType.Text);
                                        sp_cmd.Parameters.AddWithValue("@tPassword", txtPassword.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tCounter", txtCounter.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tDiscountRange", tDiscountRange);
                                        sp_cmd.Parameters.AddWithValue("@tResettle", cmbResettle.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tStopAtQty", CmpStopQty.Text.Trim() == "" ? "Yes" : CmpStopQty.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tStopAtRate", CmpStopRate.Text.Trim() == "" ? "Yes" : CmpStopRate.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tAllowVoid", cmbAllowVoid.Text.Trim() == "" ? "Yes" : cmbAllowVoid.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tAllowReturn", cmbAllowReturn.Text.Trim() == "" ? "Yes" : cmbAllowReturn.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tViewReport", cmbViewManagerReport.Text.Trim() == "" ? "Yes" : cmbViewManagerReport.Text.Trim());                                        
                                        sp_cmd.Parameters.AddWithValue("@LSystemName", CmbSystemName.Text.Trim().ToString());
                                        sp_cmd.Parameters.AddWithValue("@tViewCash", cmbViewCash.Text.Trim() == "" ? "Yes" : cmbViewCash.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tHAPayment", HAPayment.Text.Trim() == "" ? "Yes" : HAPayment.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tStCounter", comboStCounter.Text.Trim() == "" ? "Yes" : comboStCounter.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tCashDrawer", comboCashDrawer.Text.Trim() == "" ? "Yes" : comboCashDrawer.Text.Trim());
                                        sp_cmd.Parameters.AddWithValue("@tbr_name", ComboBranch.SelectedItem.ToString());
                                        sp_cmd.ExecuteNonQuery();
                                        MyMessageBox.ShowBox("Username Saved Successfully","Message");
                                        con.Close();
                                        txtUserName.Text = "";
                                        txtPassword.Text = "";
                                        txtConfirmPassword.Text = "";
                                        txtCounter.Text = "";
                                        txtDiscountRange.Text = "";
                                        pnlUserName.Visible = false;
                                        cmbUserType.SelectedIndex = 0;
                                        cmbResettle.SelectedIndex = 0;
                                        cmbAllowReturn.SelectedIndex = 0;
                                        cmbAllowVoid.SelectedIndex = 0;
                                        CmpStopRate.SelectedIndex = 0;
                                        CmpStopQty.SelectedIndex = 0;
                                        cmbViewManagerReport.SelectedIndex = 0;
                                        HAPayment.SelectedIndex = 0;
                                        cmbUserType.Focus();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Enter Valid Discount Range", "Warning");
                            txtDiscountRange.Select();
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Enter all fields", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from User_table Where user_Name<>@USerName and User_Pass=@Password", con);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtUserCheck = new DataTable();
                dtUserCheck.Rows.Clear();
                adp.Fill(dtUserCheck);
                if (dtUserCheck.Rows.Count > 0)
                {
                    MyMessageBox.ShowBox("Please Eneter Valid Password", "Warning");
                    txtPassword.Text = string.Empty;
                }
                else
                {
                    txtConfirmPassword.Focus();
                }
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
                    {
                        MessageBox.Show("Password is not match!!");
                        txtPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        txtPassword.Focus();
                    }
                    else
                    {
                        //DataTable dt = _Class.clsVariables.GetDataTable();
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{

                        //    CmbSystemName.Items.Add(dt.Rows[i][0].ToString());
                        //}
                        txtCounter.Focus();
                    }
                }
                
            }
            catch
            { }
            finally
            { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtDiscountRange.Text = "";
            cmbUserType.SelectedIndex = 0;
            cmbResettle.SelectedIndex = 0;
            pnlUserName.Visible = false;
            cmbUserType.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                    txtPassword.Focus();
                }
            }
        }

        private void UserCreation_Load(object sender, EventArgs e)
        {
            cmbUserType.SelectedIndex = 0;
            cmbUserType.Focus();

            try
            {
                NetworkBrowser nb = new NetworkBrowser();
                foreach (string pc in nb.getNetworkComputers())
                {
                    CmbSystemName.Items.Add(pc);
                }

                SqlCommand cmd = new SqlCommand("select branch_name from branch_table ", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ComboBranch.Items.Add(reader[0].ToString());
                }
                reader.Close();


                //string chck1 = "";
                //List<String> _ComputerNames = new List<String>();
                //String _ComputerSchema = "Computer";
                //System.DirectoryServices.DirectoryEntry _WinNTDirectoryEntries = new System.DirectoryServices.DirectoryEntry("WinNT:");
                //foreach (System.DirectoryServices.DirectoryEntry _AvailDomains in _WinNTDirectoryEntries.Children)
                //{
                //    foreach (System.DirectoryServices.DirectoryEntry _PCNameEntry in _AvailDomains.Children)
                //    {
                //        if (_PCNameEntry.SchemaClassName.ToLower().Contains(_ComputerSchema.ToLower()))
                //        {
                //            chck1 = "1";
                //            _ComputerNames.Add(_PCNameEntry.Name);
                //            CmbSystemName.Items.Add(_PCNameEntry.Name);
                //        }
                //    }
                //}
                //if (chck1 == "")
                //{
                //    string name = Environment.MachineName;
                //    string name1 = System.Net.Dns.GetHostName();
                //    string name11 = System.Windows.Forms.SystemInformation.ComputerName;
                //    string name12 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                //    CmbSystemName.Items.Add(name12.ToString());
                //}
               // string hostName = System.Net.Dns.GetHostName();


                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(),"Warning");
            }
        }
        string chk = "";
        SqlDataReader dreader = null;
        private void txtCounter_TextChanged(object sender, EventArgs e)
        {
            pnlUserName.Visible = true;
            if (txtCounter.Text.Trim() != null && txtCounter.Text.Trim() != "")
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dtTemp = new DataTable();
                dtTemp.Rows.Clear();
                // SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_name like '" + txt_countername.Text.Trim() + "%'", con);
                SqlCommand cmd = new SqlCommand("sp_SalesSummarySelectSingle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "COUNTERNAME");
                cmd.Parameters.AddWithValue("@tValue",txtCounter.Text.Trim());
                dreader = cmd.ExecuteReader();
                dtTemp.Load(dreader);
                bool isChk = false;
                for (int mn = 0; mn < dtTemp.Rows.Count; mn++)
                {
                    isChk = true;
                    string tempStr = dtTemp.Rows[mn]["ctr_name"].ToString();
                    for (int i = 0; i < lstUserName.Items.Count; i++)
                    {
                        if (dtTemp.Rows[mn]["ctr_name"].ToString() == lstUserName.Items[i].ToString())
                        {
                           lstUserName.SetSelected(i, true);
                           txtCounter.Select();
                           chk = "1";
                          txtCounter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }
                    }
                }
                con.Close();
                if (isChk == false)
                {
                    chk = "2";
                    txtCounter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";
            }
        }
        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtCounter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstUserName.SelectedIndex < lstUserName.Items.Count - 1)
                {
                    lstUserName.SetSelected(lstUserName.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstUserName.SelectedIndex > 0)
                {
                    lstUserName.SetSelected(lstUserName.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (lstUserName.Items.Count > 0)
                {
                    if (lstUserName.SelectedItems.Count > 0)
                    {
                        txtCounter.Text = lstUserName.SelectedItem.ToString();
                    }
                    pnlUserName.Visible = false;
                    CmbSystemName.Focus();
                }
            }
        }
        private void txtCounter_Click(object sender, EventArgs e)
        {
            pnlUserName.Visible = true;
            tActionType = "Counter";
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select ctr_name from  counter_table", con);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                lstUserName.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstUserName.Items.Add(dt.Rows[i]["ctr_name"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbUserType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUserName.Focus();
            }
        }
        private void txtDiscountRange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                    cmbResettle.Focus();
                }
            }
        }
        private void txtDiscountRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDiscountRange_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserType.Text.Trim() == "Admin")
            {
                cmbResettle.SelectedIndex = 0;
                CmpStopQty.SelectedIndex = 0;
                CmpStopRate.SelectedIndex = 0;
                cmbAllowReturn.SelectedIndex = 0;
                cmbViewManagerReport.SelectedIndex = 0;
                cmbAllowVoid.SelectedIndex = 0;
                cmbViewCash.SelectedIndex = 0;
            }
            else
            {
                cmbResettle.SelectedIndex = 1;
                CmpStopQty.SelectedIndex = 1;
                CmpStopRate.SelectedIndex = 1;
                cmbAllowReturn.SelectedIndex = 1;
                cmbAllowVoid.SelectedIndex = 1;
                cmbViewManagerReport.SelectedIndex = 1;
                cmbViewCash.SelectedIndex = 1;
            }
        }

        private void cmbResettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                    CmpStopQty.Focus();
                }
            }
        }

        private void CmpStopQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                    CmpStopRate.Focus();
                }
            }
        }

        private void CmpStopRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                   cmbAllowVoid.Focus();
                }
            }
        }

        private void cmbAllowVoid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                   cmbAllowReturn.Focus();
                }
            }
        }

        private void cmbAllowReturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text != string.Empty)
                {
                    cmbViewManagerReport.Focus();
                }
            }
        }

        private void cmbAllowVoid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmbResettle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbViewManagerReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbAllowReturn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void CmpStopQty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void CmpStopRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void CmbSystemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (lstUserName.Items.Count > 0)
                {
                    if (lstUserName.SelectedItems.Count > 0)
                    {
                        txtCounter.Text = lstUserName.SelectedItem.ToString();
                    }
                    pnlUserName.Visible = false;
                    txtDiscountRange.Focus();
                }
            }
        }
        string tActionType = "";
        private void lstUserName_Click(object sender, EventArgs e)
        {
            if (tActionType == "User")
            {
                txtUserName.Text = Convert.ToString(lstUserName.SelectedItem);
            }
            if (tActionType == "Counter")
            {
                txtCounter.Text = Convert.ToString(lstUserName.SelectedItem);
            }
        }
    }
}
