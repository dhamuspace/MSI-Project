
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MSPOSBACKOFFICE
{
    /// <summary>
    /// Interaction logic for FrmLogin.xaml
    /// </summary>
    public partial class FrmLogin : Window
    {
        public static RoutedCommand MyMinimize = new RoutedCommand();
        public void btnMinimizeMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            txtEnterValue.Focus();
        }
        public FrmLogin()
        {
            InitializeComponent();
            MyMinimize.InputGestures.Add(new KeyGesture(Key.F12, ModifierKeys.None));
            try
            {
                funConnectionStateCheck();

                
                DataTable dtCusDis = new DataTable();
                dtCusDis.Rows.Clear();
                SqlCommand cmdCusDis = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmdCusDis.CommandType = CommandType.StoredProcedure;
                cmdCusDis.Parameters.AddWithValue("@tActionType", "CUSTOMERDISPLAY");
                SqlDataAdapter adpCusDis = new SqlDataAdapter(cmdCusDis);
                adpCusDis.Fill(dtCusDis);
                if (dtCusDis.Rows.Count > 0)
                {
                  tChkCusDisEnable = dtCusDis.Rows[0]["Enable"].ToString();
                  MSPOSBACKOFFICE._Class.clsVariables.tCustomerDisplayName = dtCusDis.Rows[0]["DeviceName"].ToString();
                }
             //   w32prn.SetPrinterName(MSPOSBACKOFFICE._Class.clsVariables.tCustomerDisplayName);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string tChkCusDisEnable;
        string temp = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
      //  Win32PrintClass w32prn = new Win32PrintClass();
        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtEnterValue.Focus();
                // SecureString passdat =txtEnterValue.SecurePassword;
                Button btn = (Button)sender;
                string password = txtEnterValue.Password;
                if (password != "")
                {
                    temp = password;
                    txtEnterValue.Password = "";
                    txtEnterValue.Password = temp + btn.Content.ToString();
                }
                if (txtEnterValue.Password == "")
                {
                    txtEnterValue.Password = btn.Content.ToString();
                }
                // txtEnterValue.Select(txtEnterValue.Password.Length , 0);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEnterValue.Password  = string.Empty;
        }
        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //if (txtEnterValue.Password .Trim() == "2")
            //{
            //    FrmMain frm = new FrmMain();
            //    txtEnterValue.Password = string.Empty;
            //    this.Hide();
            //    frm.Show();
            //}

            try
            {
                funConnectionStateCheck();
                if (txtEnterValue.Password.Trim() != "")
                {
                    bool isChkLoginType = false;
                    int tempChk=0;
                    DataTable dtLoginType = new DataTable();
                    dtLoginType.Rows.Clear();
                    SqlDataAdapter adpLoginType = new SqlDataAdapter("Select * from settings_table", con);
                    adpLoginType.Fill(dtLoginType);
                    if (dtLoginType.Rows.Count > 0)
                    {
                        isChkLoginType = Convert.ToBoolean(dtLoginType.Rows[0]["LoginSecurity"].ToString());
                        if (isChkLoginType == true)
                        {
                            tempChk = txtEnterValue.Password.IndexOf('@');
                        }
                        else
                        {
                            tempChk = 1;
                        }
                    }
                    
                    if (tempChk == -1)
                    {
                        MyMessageBox.ShowBox("Enter Valid Username and Password", "Warning");
                        txtEnterValue.Password = "";
                        txtEnterValue.Focus();
                    }
                    else
                    {
                        string tUsername = "";
                        string tPassword = "";
                        string Usertype;
                            //, temp2;
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        if (isChkLoginType == true)
                        {
                            string tLogin = txtEnterValue.Password;                           
                            tUsername = tLogin.Substring(0, txtEnterValue.Password.IndexOf('@'));
                            tPassword = tLogin.Substring(txtEnterValue.Password.IndexOf('@') + 1, (tLogin.Length - tUsername.Length - 1));
                            
                            SqlCommand cmd = new SqlCommand("Select * from User_table where User_pass=@tPassword and user_name=@tUsername", con);
                            cmd.Parameters.AddWithValue("@tUsername", tUsername);
                            cmd.Parameters.AddWithValue("@tPassword", tPassword);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);
                            
                        }
                        else
                        {
                            try
                            {
                                tUsername = txtEnterValue.Password.ToString();
                                SqlCommand cmd = new SqlCommand("Select * from User_table where User_pass=@tUserno", con);
                                cmd.Parameters.AddWithValue("@tUserno", tUsername);
                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    tUsername=dt.Rows[0]["User_name"].ToString();
                                    tPassword = dt.Rows[0]["User_pass"].ToString();
                                }

                            }
                            catch (Exception)
                            {
                                MyMessageBox.ShowBox("Invalid login details","Warning");
                                txtEnterValue.Password = string.Empty;
                                goto last;
                            }

                        }

                        if (dt.Rows.Count > 0)
                        {
                            //DataTable dtNew = new DataTable();
                            //dtNew.Rows.Clear();
                            //SqlCommand cmdChk = new SqlCommand("Select * from user_table where ctr_no=@CtrNo and Active='True'", con);
                            //cmdChk.Parameters.AddWithValue("@Ctrno", dt.Rows[0]["Ctr_no"].ToString().Trim());
                            //cmdChk.Parameters.AddWithValue("@tUserNo", dt.Rows[0]["User_no"].ToString());
                            //SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
                            //adpChk.Fill(dtNew);
                            //if (dtNew.Rows.Count > 0)
                            //{
                            //    if (dt.Rows[0]["User_no"].ToString() == dtNew.Rows[0]["User_no"].ToString())
                            //    {
                            //       // MyMessageBox.ShowBox("This user has been already logon into another system", "Warning");
                            //        txtEnterValue.Password = "";
                            //    }
                            //    else if (dt.Rows[0]["User_no"].ToString() != dtNew.Rows[0]["User_no"].ToString())
                            //    {
                            //      //  MyMessageBox.ShowBox("This User Counter already Used in another system", "Warning");
                            //        txtEnterValue.Password = "";
                            //    }
                            //}
                            //else
                            //{
                                //SqlCommand cmdUpdate = new SqlCommand("Update User_table set Active='True' where User_pass=@tPassword and user_name=@tUsername", con);
                                //cmdUpdate.Parameters.AddWithValue("@tUsername", tUsername);
                                //cmdUpdate.Parameters.AddWithValue("@tPassword", tPassword);
                                //cmdUpdate.ExecuteNonQuery();
                                Usertype = dt.Rows[0]["User_type"].ToString();
                                _Class.clsVariables.tUserNo = dt.Rows[0]["User_no"].ToString();
                                _Class.clsVariables.tCounter = dt.Rows[0]["Ctr_no"].ToString();
                                _Class.clsVariables.tStopAtQtyF4 = (dt.Rows[0]["StopAtQty"].ToString()=="Yes")?true:false;
                                _Class.clsVariables.tStopAtRateF4 = (dt.Rows[0]["StopAtRate"].ToString() == "Yes") ? true : false;
                                _Class.clsVariables.tUserName=tUsername;

                                DataTable dtCounterName = new DataTable();
                                dtCounterName.Rows.Clear();
                                SqlCommand cmdCounterName = new SqlCommand("Select ctr_name from counter_table where ctr_no=@tCounterNo", con);
                                cmdCounterName.Parameters.AddWithValue("@tCounterNo", _Class.clsVariables.tCounter);
                                SqlDataAdapter adpCounter = new SqlDataAdapter(cmdCounterName);
                                adpCounter.Fill(dtCounterName);
                                if (dtCounterName.Rows.Count > 0)
                                {
                                    _Class.clsVariables.tCounterName = dtCounterName.Rows[0]["ctr_name"].ToString();
                                }

                                if (Usertype == "0")
                                {
                                    _Class.clsVariables.UserType = "0";
                                    frmBackOffice1 frm = new frmBackOffice1();
                                    txtEnterValue.Password = string.Empty;
                                    this.Hide();
                                    frm.Show();
                                }
                                if (Usertype == "1")
                                {
                                    _Class.clsVariables.UserType = "1";
                                    frmBackOffice1 frm = new frmBackOffice1();
                                    txtEnterValue.Password = string.Empty;
                                    this.Hide();
                                    frm.Show();
                                }
                           // }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Invalid login details", "Warning");
                            txtEnterValue.Password = "";
                        }

                    }
                }
            last:
                int ck = 0;
            txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                //if (tChkCusDisEnable == "Yes")
                //{
                //    // Clear 2st line
                //    w32prn.SetDeviceFont(7, "FontControl", false, false);
                //    // Clear screen (CLS)
                //    w32prn.PrintText("a");
                //    // Clear 1st line (CAN for 1st line)
                //    //  w32prn.PrintText("o");
                //    w32prn.SetDeviceFont(7, "BCD 1st Line", false, false);
                //    w32prn.PrintText("");

                //    w32prn.SetDeviceFont(7, "BCD 2nd Line", false, false);
                //    w32prn.PrintText("");
                //    w32prn.SetDeviceFont(7, "FontControl", false, false);
                //    w32prn.EndDoc();
                //}               
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           // this.Close();
            Application.Current.Shutdown();
            foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())//GETS PROCESSES
            {
                if (pr.ProcessName == "SalesProject")//KILLS FIREFOX.....REMOVE FIREFOX.....CONNECT SAVED SQL PROCESSES IN HERE MAYBE??
                {
                    pr.Kill(); //KILLS THE PROCESSES
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (tChkCusDisEnable == "Yes")
                //{
                //    // Clear 2st line
                //    w32prn.SetDeviceFont(7, "FontControl", false, false);
                //    // Clear screen (CLS)
                //    w32prn.PrintText("a");
                //    // Clear 1st line (CAN for 1st line)
                //    //  w32prn.PrintText("o");
                //    w32prn.SetDeviceFont(7, "BCD 1st Line", false, false);
                //    w32prn.PrintText("      WELCOME");

                //    w32prn.SetDeviceFont(7, "BCD 2nd Line", false, false);
                //    w32prn.PrintText("  HAVE A NICE DAY");
                //    w32prn.SetDeviceFont(7, "FontControl", false, false);
                //    w32prn.EndDoc();
                //}
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //frmKeyBoard frm = new frmKeyBoard();
                //_Class.clsVariables.tVoidActionType = "LOGINNAME";
                //if (_Class.clsVariables.tVoidActionType == "LOGINNAME")
                //{
                //    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEventItemCode);
                //    frm.ShowDialog();
                //    txtEnterValue.Focus();
                //  //  txtEnterValue.se.Select(txtEnterValue.Password.Length, 0);
                //}
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        public void CloseEventItemCode(object sender, EventArgs e)
        {
            try
            {
                txtEnterValue.Password = _Class.clsVariables.tVoidValue;
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }       
    }
}
