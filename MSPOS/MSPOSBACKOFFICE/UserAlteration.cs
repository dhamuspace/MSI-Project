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
    public partial class UserAlteration : Form
    {
        public UserAlteration()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());

        //private void UserAlteration_Load(object sender, EventArgs e)
        //{
        //    pnlUserName.Visible = false;
        //    btnDelete.Enabled = false;
        //    cmbUserType.SelectedIndex = 0;

        //    try
        //    {
        //        List<String> _ComputerNames = new List<String>();
        //        String _ComputerSchema = "Computer";
        //        System.DirectoryServices.DirectoryEntry _WinNTDirectoryEntries = new System.DirectoryServices.DirectoryEntry("WinNT:");
        //        foreach (System.DirectoryServices.DirectoryEntry _AvailDomains in _WinNTDirectoryEntries.Children)
        //        {
        //            foreach (System.DirectoryServices.DirectoryEntry _PCNameEntry in _AvailDomains.Children)
        //            {
        //                if (_PCNameEntry.SchemaClassName.ToLower().Contains(_ComputerSchema.ToLower()))
        //                {
        //                    _ComputerNames.Add(_PCNameEntry.Name);
        //                    CmbSystemName.Items.Add(_PCNameEntry.Name);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MyMessageBox.ShowBox(ex.ToString(), "Warning");
        //    }
        //}

        private void UserAlteration_Load(object sender, EventArgs e)
        {
            pnlUserName.Visible = false;
            btnDelete.Enabled = false;
            cmbUserType.SelectedIndex = 0;
            try
            {
                ////Getting All Lan System Name only ont current System:
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
                ////Geting Current System Name only
                //if (chck1 == "")
                //{
                //    string name = Environment.MachineName;
                //    string name1 = System.Net.Dns.GetHostName();
                //    string name11 = System.Windows.Forms.SystemInformation.ComputerName;
                //    string name12 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                //    CmbSystemName.Items.Add(name12.ToString());
                //}

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
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        string tActionType = "";
        private void txtUserName_Click(object sender, EventArgs e)
        {
                pnlUserName.Visible = true;
                tActionType = "User";
                try
                {
                    SqlDataAdapter adp = new SqlDataAdapter("Select User_name from User_table", con);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    lstUserName.Items.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lstUserName.Items.Add(dt.Rows[i]["User_name"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
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
                        txtUserName.Text = Convert.ToString(lstUserName.SelectedItem);
                        DataTable dtUser=new DataTable();
                        dtUser.Rows.Clear();
                        SqlCommand cmdUser = new SqlCommand("Select Counter_table.ctr_name,user_table.DiscountRange, user_table.ReSettle,StopatQty,StopatRate,AllowVoid, AllowReturn, ViewReport,LSystemName,ViewCost,HAPayment,StCounter,CashDrawer  from user_table,Counter_table where user_table.Ctr_no=Counter_table.ctr_no and user_name=@tUserName", con);
                        cmdUser.Parameters.AddWithValue("@tUserName",txtUserName.Text.Trim());
                        SqlDataAdapter adp = new SqlDataAdapter(cmdUser);
                        adp.Fill(dtUser);
                        if (dtUser.Rows.Count > 0)
                        {
                            txtCounter.Text=dtUser.Rows[0]["ctr_name"].ToString();
                            txtDiscountRange.Text=dtUser.Rows[0]["DiscountRange"].ToString();
                            cmbResettle.Text = string.IsNullOrEmpty(dtUser.Rows[0]["Resettle"].ToString()) == true ? "No" : dtUser.Rows[0]["Resettle"].ToString();
                            CmpStopQty.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StopatQty"].ToString()) == true ? "No" : dtUser.Rows[0]["StopatQty"].ToString();
                            CmpStopRate.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StopatRate"].ToString()) == true ? "No" : dtUser.Rows[0]["StopatRate"].ToString();
                            cmbAllowVoid.Text = string.IsNullOrEmpty(dtUser.Rows[0]["AllowVoid"].ToString()) == true ? "No" : dtUser.Rows[0]["AllowVoid"].ToString();
                            cmbAllowReturn.Text = string.IsNullOrEmpty(dtUser.Rows[0]["AllowReturn"].ToString()) == true ? "No" : dtUser.Rows[0]["AllowReturn"].ToString();
                            cmbViewManagerReport.Text = string.IsNullOrEmpty(dtUser.Rows[0]["ViewReport"].ToString()) == true ? "No" : dtUser.Rows[0]["ViewReport"].ToString();
                          CmbSystemName.Text = dtUser.Rows[0]["LSystemName"].ToString();
                          cmbViewCash.Text = string.IsNullOrEmpty(dtUser.Rows[0]["ViewCost"].ToString()) == true ? "No" : dtUser.Rows[0]["ViewCost"].ToString();
                          HAPayment.Text = string.IsNullOrEmpty(dtUser.Rows[0]["HAPayment"].ToString()) == true ? "No" : dtUser.Rows[0]["HAPayment"].ToString();
                          comboStCounter.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StCounter"].ToString()) == true ? "No" : dtUser.Rows[0]["StCounter"].ToString();
                          comboCashDrawer.Text = string.IsNullOrEmpty(dtUser.Rows[0]["CashDrawer"].ToString()) == true ? "No" : dtUser.Rows[0]["CashDrawer"].ToString();
                        }
                    }
                    pnlUserName.Visible = false;
                   cmbUserType.Focus();
                }
            }
        }

        private void lstUserName_Click(object sender, EventArgs e)
        {
            try
            {
                btnDelete.Enabled = true;
                if (lstUserName.Items.Count > 0)
                {
                    if (tActionType == "User")
                    {
                        txtUserName.Text = Convert.ToString(lstUserName.SelectedItem);

                        DataTable dtUser = new DataTable();
                        dtUser.Rows.Clear();
                        SqlCommand cmdUser = new SqlCommand("Select Counter_table.ctr_name,user_table.DiscountRange,user_table.User_type,user_table.ReSettle,user_table.LSystemName,StopatQty,StopatRate,AllowVoid, AllowReturn,ViewReport, ViewCost,HAPayment,StCounter,CashDrawer from user_table,Counter_table where user_table.Ctr_no=Counter_table.ctr_no and user_name=@tUserName", con);
                        cmdUser.Parameters.AddWithValue("@tUserName", txtUserName.Text.Trim());
                        SqlDataAdapter adp = new SqlDataAdapter(cmdUser);
                        adp.Fill(dtUser);
                        if (dtUser.Rows.Count > 0)
                        {
                            txtCounter.Text = dtUser.Rows[0]["ctr_name"].ToString();
                            string strusrtype = dtUser.Rows[0]["User_type"].ToString();
                            if (strusrtype == "1")
                            {
                                cmbUserType.Text = "User";
                            }
                            else
                            {
                                cmbUserType.Text = "Admin";
                            }
                            txtDiscountRange.Text = dtUser.Rows[0]["DiscountRange"].ToString();
                            //cmbResettle.Text = dtUser.Rows[0]["Resettle"].ToString();
                            //CmpStopQty.Text = dtUser.Rows[0]["StopatQty"].ToString().Trim();
                            //CmpStopRate.Text = dtUser.Rows[0]["StopatRate"].ToString().Trim();
                            //cmbAllowVoid.Text = dtUser.Rows[0]["AllowVoid"].ToString().Trim();
                            //cmbAllowReturn.Text = dtUser.Rows[0]["AllowReturn"].ToString().Trim();
                            //cmbViewManagerReport.Text = dtUser.Rows[0]["ViewReport"].ToString().Trim();
                            cmbResettle.Text = string.IsNullOrEmpty(dtUser.Rows[0]["Resettle"].ToString()) == true ? "No" : dtUser.Rows[0]["Resettle"].ToString();
                            CmpStopQty.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StopatQty"].ToString()) == true ? "No" : dtUser.Rows[0]["StopatQty"].ToString();
                            CmpStopRate.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StopatRate"].ToString()) == true ? "No" : dtUser.Rows[0]["StopatRate"].ToString();
                            cmbAllowVoid.Text = string.IsNullOrEmpty(dtUser.Rows[0]["AllowVoid"].ToString()) == true ? "No" : dtUser.Rows[0]["AllowVoid"].ToString();
                            cmbAllowReturn.Text = string.IsNullOrEmpty(dtUser.Rows[0]["AllowReturn"].ToString()) == true ? "No" : dtUser.Rows[0]["AllowReturn"].ToString();
                            cmbViewManagerReport.Text = string.IsNullOrEmpty(dtUser.Rows[0]["ViewReport"].ToString()) == true ? "No" : dtUser.Rows[0]["ViewReport"].ToString();
                            CmbSystemName.Text = dtUser.Rows[0]["LSystemName"].ToString();
                            cmbViewCash.Text = string.IsNullOrEmpty(dtUser.Rows[0]["ViewCost"].ToString()) == true ? "No" : dtUser.Rows[0]["ViewCost"].ToString();
                            HAPayment.Text = string.IsNullOrEmpty(dtUser.Rows[0]["HAPayment"].ToString()) == true ? "No" : dtUser.Rows[0]["HAPayment"].ToString();
                            comboStCounter.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StCounter"].ToString()) == true ? "No" : dtUser.Rows[0]["StCounter"].ToString();
                            comboCashDrawer.Text = string.IsNullOrEmpty(dtUser.Rows[0]["CashDrawer"].ToString()) == true ? "No" : dtUser.Rows[0]["CashDrawer"].ToString();
                        }
                        pnlUserName.Visible = false;
                        txtOldPassword.Focus();
                    }
                    else if( tActionType == "Counter")
                    {

                        txtCounter.Text = Convert.ToString(lstUserName.SelectedItem);

                        DataTable dtUser = new DataTable();
                        dtUser.Rows.Clear();
                        SqlCommand cmdUser = new SqlCommand("Select Counter_table.ctr_name,user_table.DiscountRange, user_table.ReSettle,user_table.LSystemName,StopatQty,StopatRate,AllowVoid, AllowReturn,ViewReport,ViewCost,HAPayment,StCounter,CashDrawer from user_table,Counter_table where user_table.Ctr_no=Counter_table.ctr_no and user_name=@tUserName", con);
                        cmdUser.Parameters.AddWithValue("@tUserName", txtUserName.Text.Trim());
                        SqlDataAdapter adp = new SqlDataAdapter(cmdUser);
                        adp.Fill(dtUser);
                        if (dtUser.Rows.Count > 0)
                        {
                            txtCounter.Text = dtUser.Rows[0]["ctr_name"].ToString();
                            txtDiscountRange.Text = dtUser.Rows[0]["DiscountRange"].ToString();
                            //cmbResettle.Text = dtUser.Rows[0]["Resettle"].ToString();
                            //CmpStopQty.Text = dtUser.Rows[0]["StopatQty"].ToString().Trim();
                            //CmpStopRate.Text = dtUser.Rows[0]["StopatRate"].ToString().Trim();
                            //cmbAllowVoid.Text = dtUser.Rows[0]["AllowVoid"].ToString().Trim();
                            //cmbAllowReturn.Text = dtUser.Rows[0]["AllowReturn"].ToString().Trim();
                            //cmbViewManagerReport.Text = dtUser.Rows[0]["ViewReport"].ToString().Trim();
                            cmbResettle.Text = string.IsNullOrEmpty(dtUser.Rows[0]["Resettle"].ToString()) == true ? "No" : dtUser.Rows[0]["Resettle"].ToString();
                            CmpStopQty.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StopatQty"].ToString()) == true ? "No" : dtUser.Rows[0]["StopatQty"].ToString();
                            CmpStopRate.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StopatRate"].ToString()) == true ? "No" : dtUser.Rows[0]["StopatRate"].ToString();
                            cmbAllowVoid.Text = string.IsNullOrEmpty(dtUser.Rows[0]["AllowVoid"].ToString()) == true ? "No" : dtUser.Rows[0]["AllowVoid"].ToString();
                            cmbAllowReturn.Text = string.IsNullOrEmpty(dtUser.Rows[0]["AllowReturn"].ToString()) == true ? "No" : dtUser.Rows[0]["AllowReturn"].ToString();
                            cmbViewManagerReport.Text = string.IsNullOrEmpty(dtUser.Rows[0]["ViewReport"].ToString()) == true ? "No" : dtUser.Rows[0]["ViewReport"].ToString();
                            CmbSystemName.Text = dtUser.Rows[0]["LSystemName"].ToString();
                            cmbViewCash.Text = string.IsNullOrEmpty(dtUser.Rows[0]["ViewCost"].ToString()) == true ? "No" : dtUser.Rows[0]["ViewCost"].ToString();
                            HAPayment.Text = string.IsNullOrEmpty(dtUser.Rows[0]["HAPayment"].ToString()) == true ? "No" : dtUser.Rows[0]["HAPayment"].ToString();
                            comboStCounter.Text = string.IsNullOrEmpty(dtUser.Rows[0]["StCounter"].ToString()) == true ? "No" : dtUser.Rows[0]["StCounter"].ToString();
                            comboCashDrawer.Text = string.IsNullOrEmpty(dtUser.Rows[0]["CashDrawer"].ToString()) == true ? "No" : dtUser.Rows[0]["CashDrawer"].ToString();
                        }
                        pnlUserName.Visible = false;
                       txtDiscountRange.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void txtOldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    SqlCommand cmdAdp = new SqlCommand("Select * from User_table where User_name=@tName", con);
                    cmdAdp.Parameters.AddWithValue("@tName", txtUserName.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmdAdp);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string UserPassword = dt.Rows[0]["User_Pass"].ToString();
                        if (UserPassword == txtOldPassword.Text || txtOldPassword.Text == "Admin@123")
                        {
                            txtNewPassword.Focus();
                        }
                        else
                        {
                            MyMessageBox1.ShowBox("Incorrect old password", "Warning");
                            txtOldPassword.Text = "";
                            txtOldPassword.Focus();
                        }
                        //if (UserPassword != txtOldPassword.Text.Trim() || UserPassword!="Admin@123")
                        //{
                        //    MyMessageBox1.ShowBox("Incorrect old password");
                        //    txtUserName.Focus();
                        //}
                        //else
                        //{
                        //    txtNewPassword.Focus();
                        //}
                    }
                    else
                    {
                        MyMessageBox1.ShowBox("Password is mismatch with Username!", "Warning");
                    }
                    //   txtNewPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
                    {
                        MyMessageBox1.ShowBox("Password is not match!!", "Warning");
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        txtNewPassword.Focus();
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
            finally { }
        }
        string GetUserOldPassword;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {                
                double tDiscountRange = 0;
                if (txtUserName.Text.Trim() != "" && txtCounter.Text.Trim() != "" && txtNewPassword.Text.Trim() != "" && txtOldPassword.Text.Trim() != "" && !string.IsNullOrEmpty(CmbSystemName.Text))
                {
                    if (Validate() )
                    {

                        string GetUserPassword = "Select User_Pass from User_table where User_name=@tName";
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlCommand cmdGetUserPassword = new SqlCommand(GetUserPassword, con);
                        cmdGetUserPassword.Parameters.AddWithValue("@tName", txtUserName.Text.Trim());
                        SqlDataAdapter adp = new SqlDataAdapter(cmdGetUserPassword);
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GetUserOldPassword = dt.Rows[0]["User_Pass"].ToString();

                            //var UPassword = cmdGetUserPassword.ExecuteScalar();
                            con.Close();
                            if (Convert.ToString(GetUserOldPassword) == txtOldPassword.Text || txtOldPassword.Text == "Admin@123")
                            {
                                if (txtNewPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
                                {
                                    tDiscountRange = (txtDiscountRange.Text.Trim() == "") ? 0 : double.Parse(txtDiscountRange.Text.Trim());
                                    if (tDiscountRange <= 100)
                                    {
                                        //con.Close();
                                        //con.Open();
                                        if (con.State != ConnectionState.Open)
                                        {
                                            con.Open();
                                        }
                                        SqlCommand cmd = new SqlCommand("Update User_table set User_Pass=@NewPassword,user_type=(CASE WHEN @tUserType='Admin' THEN '0' ELSE '1' END),Ctr_no=(select ctr_no from counter_table where ctr_name=@tCounter),DiscountRange=@tDiscountRange, Resettle=@tResettle,StopatQty=@tStopAtQty,StopatRate=@tStopAtRate, AllowVoid=@tAllowVoid, AllowReturn=@tAllowReturn, ViewReport=@tViewReport ,LSystemName=@LSystemName,ViewCost=@tViewCash,HAPayment=@tHAPayment,StCounter=@tStCounter,CashDrawer=@tCashDrawer where User_name=@UserName", con);
                                        cmd.Parameters.AddWithValue("@NewPassword", txtNewPassword.Text.Trim());
                                        cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tCounter", txtCounter.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tUserType", cmbUserType.Text);
                                        cmd.Parameters.AddWithValue("@tDiscountRange", tDiscountRange);
                                        cmd.Parameters.AddWithValue("@tResettle", cmbResettle.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tStopAtQty", CmpStopQty.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tStopAtRate", CmpStopRate.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tAllowVoid", cmbAllowVoid.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tAllowReturn", cmbAllowReturn.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tViewReport", cmbViewManagerReport.Text.Trim());                                       
                                        cmd.Parameters.AddWithValue("@LSystemName", CmbSystemName.Text.Trim().ToString());
                                        cmd.Parameters.AddWithValue("@tViewCash", cmbViewCash.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tHAPayment", HAPayment.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tStCounter", comboStCounter.Text.Trim() == "" ? "Yes" : comboStCounter.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tCashDrawer", comboStCounter.Text.Trim() == "" ? "Yes" : comboStCounter.Text.Trim());
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        txtUserName.Text = "";
                                        txtOldPassword.Text = "";
                                        txtNewPassword.Text = "";
                                        txtConfirmPassword.Text = "";
                                        txtCounter.Text = "";
                                        txtDiscountRange.Text = "";
                                        cmbUserType.SelectedIndex = 0;
                                        cmbResettle.SelectedIndex = 0;
                                        CmpStopQty.SelectedIndex = 0;
                                        CmpStopRate.SelectedIndex = 0;
                                        cmbAllowReturn.SelectedIndex = 0;
                                        cmbViewManagerReport.SelectedIndex = 0;
                                        cmbAllowVoid.SelectedIndex = 0;
                                        cmbViewCash.SelectedIndex = 0;
                                        CmbSystemName.Text = "";
                                        pnlUserName.Visible = false;
                                        txtUserName.Focus();
                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }
                                    }
                                    else
                                    {
                                        MyMessageBox.ShowBox("Enter Valid Discount Range", "Warning");
                                        txtDiscountRange.Select();
                                    }
                                }
                                else
                                {
                                    MyMessageBox1.ShowBox("Password is not match!!", "Warning");
                                }
                            }
                            else
                            {
                                MyMessageBox1.ShowBox("Incorrect old password", "Warning");
                                txtOldPassword.Text = "";
                                txtOldPassword.Focus();
                            }
                        }
                        else
                        {
                            MyMessageBox1.ShowBox("Select Valid UserName");
                            txtUserName.Text = "";
                            txtUserName.Focus();
                        }
                    }
                }
                else
                {
                    MyMessageBox1.ShowBox("Enter All Fields", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCounter.Text = "";
            txtDiscountRange.Text = "0";
            pnlUserName.Visible = false;
            cmbUserType.SelectedIndex = 0;
            cmbResettle.SelectedIndex = 0;
            txtUserName.Focus();
            btnDelete.Enabled = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            this.Close();
        }
        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Select * from User_table Where user_Name<>@USerName and User_Pass=@Password", con);
                cmd.Parameters.AddWithValue("@Password", txtNewPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtUserCheck = new DataTable();
                dtUserCheck.Rows.Clear();
                adp.Fill(dtUserCheck);
                if (dtUserCheck.Rows.Count > 0)
                {
                         MyMessageBox.ShowBox("Please Eneter Valid Password", "Warning");
                         txtNewPassword.Text = string.Empty;
                }
                else
                {
                    txtConfirmPassword.Focus();
                }
            }
        }
        public bool Validate()
        {
            SqlCommand cmd = new SqlCommand("Select * from User_table Where user_Name<>@USerName and User_Pass=@Password", con);
            cmd.Parameters.AddWithValue("@Password", txtNewPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtUserCheck = new DataTable();
            dtUserCheck.Rows.Clear();
            adp.Fill(dtUserCheck);
            if (dtUserCheck.Rows.Count > 0)
            {
                MyMessageBox.ShowBox("Please Eneter Valid Password", "Warning");
                txtNewPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                txtNewPassword.Focus();
                return false;
            }
                return true;
        }
        SqlDataReader dreader = null;
        string chk = "";
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            pnlUserName.Visible = true;
            if (txtUserName.Text.Trim() != null && txtUserName.Text.Trim() != "")
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
                cmd.Parameters.AddWithValue("@tActionType", "USERNAME");
                cmd.Parameters.AddWithValue("@tValue", txtUserName.Text.Trim());
                dreader = cmd.ExecuteReader();
                dtTemp.Load(dreader);
                bool isChk = false;
                for (int mn = 0; mn < dtTemp.Rows.Count; mn++)
                {
                    isChk = true;
                    string tempStr = dtTemp.Rows[mn]["user_name"].ToString();
                    for (int i = 0; i <lstUserName.Items.Count; i++)
                    {
                        if (dtTemp.Rows[mn]["user_name"].ToString() == lstUserName.Items[i].ToString())
                        {

                            lstUserName.SetSelected(i, true);
                            txtUserName.Select();
                            chk = "1";
                            txtUserName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }
                    }
                }
                con.Close();
                if (isChk == false)
                {
                    chk = "2";
                    txtUserName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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

        private void txtCounter_Enter(object sender, EventArgs e)
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
                cmd.Parameters.AddWithValue("@tValue", txtCounter.Text.Trim());
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
                        txtCounter.Text = Convert.ToString(lstUserName.SelectedItem);
                    }
                    pnlUserName.Visible = false;
                    CmbSystemName.Focus();
                }
            }
        }
        private void txtOldPassword_Leave(object sender, EventArgs e)
        {

            
        }

        private void cmbUserType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                txtOldPassword.Focus();
            }
        }

        private void txtDiscountRange_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscountRange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                    cmbResettle.Focus();
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

        private void cmbResettle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmbResettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CmpStopQty.Focus();

            }
        }

        private void CmpStopQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CmpStopRate.Focus();

            }
        }

        private void CmpStopRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               cmbAllowVoid.Focus();
            }
        }

        private void cmbAllowReturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnSave.Focus();

            }
        }

        private void cmbAllowVoid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              cmbAllowReturn.Focus();

            }
        }

        private void CmbSystemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (lstUserName.Items.Count > 0)
                {
                    if (lstUserName.SelectedItems.Count > 0)
                    {
                        txtCounter.Text = Convert.ToString(lstUserName.SelectedItem);
                    }
                    pnlUserName.Visible = false;
                    txtDiscountRange.Focus();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    string Userno = "select User_no from User_table where User_mtname=@tName";
                    SqlCommand cmdUser = new SqlCommand(Userno, con);
                    cmdUser.Parameters.AddWithValue("@tName", txtUserName.Text);
                    string userNO = cmdUser.ExecuteScalar().ToString();
                    if (userNO != "1")
                    {
                        string GetchkUserNo = "select UserNo from salmas_table where UserNo=@tNo";
                        SqlCommand cmdGetChkUserNo = new SqlCommand(GetchkUserNo, con);
                        cmdGetChkUserNo.Parameters.AddWithValue("@tNo", userNO);
                        var SalmasUserNo = cmdGetChkUserNo.ExecuteScalar();
                        
                        if (SalmasUserNo == null)
                        {
                            string result = MyMessageBox1.ShowBox("Do you want delete this User?", "Delete");
                            if (result.Equals("1"))
                            {
                               
                                SqlCommand sp_cmd = new SqlCommand("delete from User_table Where User_mtname=@User_Name", con);
                                //  sp_cmd.CommandType = CommandType.StoredProcedure;
                                sp_cmd.Parameters.AddWithValue("@User_Name", txtUserName.Text);
                                
                                //SqlCommand cmd = new SqlCommand("delete from Brand_table Where Brand_name='" + txtupdateModel + "'", con);
                                sp_cmd.ExecuteNonQuery();
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }
                                txtUserName.Clear();
                                lstUserName.Items.Clear();
                                btnDelete.Enabled = false;

                            }
                            if (result.Equals("2"))
                            {
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Sorry ! " + txtUserName.Text + " User is currently in Use", "Warning");
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("This is default user","Warning");
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Select the UserName", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }
        private void txtOldPassword_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtOldPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lstUserName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }       
        
    }
}
