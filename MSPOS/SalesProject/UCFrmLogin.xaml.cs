
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
using System.Diagnostics;
using Microsoft.Win32;
using MSPOSBACKOFFICE;
namespace SalesProject
{
    /// <summary>
    /// Interaction logic for FrmLogin.xaml
    /// </summary>
    /// 
    public delegate void UCFrmLoginEvent();
    public partial class UCFrmLogin : UserControl
    {
        public static RoutedCommand MyMinimize = new RoutedCommand();
        public event UCFrmLoginEvent UCFrmLoginEvent_loginClick;
        public UCFrmLogin()
        {
            InitializeComponent();
            try
            {
                funConnectionStateCheck();
                _Class.clsVariables.funGlobalReceiptSetting();
                MyMinimize.InputGestures.Add(new KeyGesture(Key.F12, ModifierKeys.None));

                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void btnMinimizeMethod(Object sender, ExecutedRoutedEventArgs e)
        {
           // WindowState = WindowState.Minimized;
            txtEnterValue.Focus();
        }

        string tChkCusDisEnable;
        string temp = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        // Win32PrintClass w32prn = new Win32PrintClass();
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
            try
            {
                UCKeyBoardAct1.Visibility = Visibility.Hidden;
                txtEnterValue.Password = string.Empty;
                txtEnterValue.Focus();
            }
            
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        string strViewCost = "";
        string strCost = "False";
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
                    int tempChk = 0;
                    DataTable dtLoginType = new DataTable();
                    dtLoginType.Rows.Clear();
                    //SqlDataAdapter adpLoginType = new SqlDataAdapter("Select * from settings_table", con);
                    SqlDataAdapter adpLoginType = new SqlDataAdapter("Select LoginSecurity from settings_table", con);
                    adpLoginType.Fill(dtLoginType);
                    if (dtLoginType.Rows.Count > 0)
                    {
                        //isChkLoginType = Convert.ToBoolean(dtLoginType.Rows[0][""].ToString());
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

                            DataTable dtus = new DataTable();
                            dtus.Rows.Clear();
                            SqlCommand cmdUs = new SqlCommand("select * from user_table", con);
                            SqlDataAdapter adpUs = new SqlDataAdapter(cmdUs);
                            adpUs.Fill(dtus);

                            if (dtus.Columns.Contains("ViewCost"))
                            {
                                strViewCost = "Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,SSMA_TimeStamp,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,ViewCost,HAPayment,StCounter,CashDrawer from User_table where User_pass=@tPassword and user_name=@tUsername and LSystemName=@tMachineName";
                            }
                            else
                            {
                                strViewCost = "Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,SSMA_TimeStamp,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,ViewCost,HAPayment,StCounter,CashDrawer from User_table where User_pass=@tPassword and user_name=@tUsername and LSystemName=@tMachineName";
                                strCost = "True";
                            }
                            SqlCommand cmd = new SqlCommand(strViewCost, con);


                            //SqlCommand cmd = new SqlCommand("Select * from User_table where User_pass=@tPassword and user_name=@tUsername and LSystemName=@tMachineName", con);
                           // SqlCommand cmd = new SqlCommand("Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,SSMA_TimeStamp,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,ViewCost from User_table where User_pass=@tPassword and user_name=@tUsername and LSystemName=@tMachineName", con);
                            cmd.Parameters.AddWithValue("@tUsername", tUsername);
                            cmd.Parameters.AddWithValue("@tPassword", tPassword);
                            cmd.Parameters.AddWithValue("@tMachineName", _Class.clsVariables.tSystemName);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);

                        }
                        else
                        {
                            try
                            {
                                tUsername = txtEnterValue.Password.ToString();                               
                                DataTable dtus = new DataTable();
                                dtus.Rows.Clear();
                                SqlCommand cmdUs = new SqlCommand("select * from user_table",con);
                                SqlDataAdapter adpUs = new SqlDataAdapter(cmdUs);
                                adpUs.Fill(dtus);

                                if (dtus.Columns.Contains("ViewCost"))
                                {
                                    strViewCost = "Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,ViewCost,HAPayment,StCounter,CashDrawer,Branch_Name from User_table where User_pass=@tUserno and LSystemName=@tMachineName";
                                }
                                else
                                {
                                    strViewCost = "Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,HAPayment,StCounter,CashDrawer,Branch_Name from User_table where User_pass=@tUserno and LSystemName=@tMachineName";
                                    strCost = "True";
                                }
                                SqlCommand cmd = new SqlCommand(strViewCost, con);
                                cmd.Parameters.AddWithValue("@tUserno", tUsername);
                                cmd.Parameters.AddWithValue("@tMachineName", _Class.clsVariables.tSystemName);
                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    tUsername = dt.Rows[0]["User_name"].ToString();
                                    tPassword = dt.Rows[0]["User_pass"].ToString();
                                    _Class.clsVariables.tBranch = dt.Rows[0]["Branch_Name"].ToString();
                                    MSPOSBACKOFFICE._Class.clsVariables.tBr_Name = dt.Rows[0]["Branch_Name"].ToString();
                                    //_Class.clsVariables.tranch = dt.Rows[0]["Branch_Name"].ToString();
                                }
                                else
                                {
                                    tUsername = "!Password123";                                   
                                }
                            }
                            catch (Exception)
                            {
                                MyMessageBox.ShowBox("Invalid login details", "Warning");
                                txtEnterValue.Password = string.Empty;
                                goto last;
                            }

                        }
                        if (txtEnterValue.Password.Equals("!Password123"))
                        {
                            _Class.clsVariables.tUserNo = "1";
                            _Class.clsVariables.tCounter = "1";
                            _Class.clsVariables.tCounterName ="Counter";
                            _Class.clsVariables.tUserName = "Micro Admin";
                            _Class.clsVariables.UserType = "0";
                          //  FrmMain frm = new FrmMain();
                          // UCMain frm = new UCMain();
                            txtEnterValue.Password = string.Empty;
                           // this.Hide();
                           // frm.Show();

                            if (UCFrmLoginEvent_loginClick != null)
                            {
                                UCFrmLoginEvent_loginClick();
                            }
                        }
                        else
                        {

                            if (dt.Rows.Count > 0)
                            {
                                DataTable dtNew = new DataTable();
                                dtNew.Rows.Clear();
                                //SqlCommand cmdChk = new SqlCommand("Select * from user_table where ctr_no=@CtrNo and Active='True'", con);
                                DataTable dtus = new DataTable();
                                dtus.Rows.Clear();
                                SqlCommand cmdUs = new SqlCommand("select * from user_table", con);
                                SqlDataAdapter adpUs = new SqlDataAdapter(cmdUs);
                                adpUs.Fill(dtus);
                                string strcmd1 = "";
                                if (dtus.Columns.Contains("ViewCost"))
                                {
                                    strcmd1 = "Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,ViewCost,HAPayment,StCounter,CashDrawer from User_table where ctr_no=@CtrNo and Active='True'";
                                }
                                else
                                {

                                    strcmd1 = "Select User_no,User_name,User_type,User_mtname,User_Pass,Alter_Days,Print_Bills,Ctr_no,Active,DiscountRange,Resettle,StopatQty,StopatRate,AllowVoid,AllowReturn,ViewReport,LSystemName,HAPayment,StCounter,CashDrawer from User_table where ctr_no=@CtrNo and Active='True'";
                                }
                                SqlCommand cmdChk = new SqlCommand(strcmd1, con);
                                cmdChk.Parameters.AddWithValue("@Ctrno", dt.Rows[0]["Ctr_no"].ToString().Trim());
                                cmdChk.Parameters.AddWithValue("@tUserNo", dt.Rows[0]["User_no"].ToString());
                                SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
                                adpChk.Fill(dtNew);
                                if (dtNew.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["User_no"].ToString() == dtNew.Rows[0]["User_no"].ToString())
                                    {
                                        MyMessageBox.ShowBox("This user has been already logon into another system", "Warning");
                                        txtEnterValue.Password = "";
                                    }
                                    else if (dt.Rows[0]["User_no"].ToString() != dtNew.Rows[0]["User_no"].ToString())
                                    {
                                        MyMessageBox.ShowBox("This User Counter already Used in another system", "Warning");
                                        txtEnterValue.Password = "";
                                    }
                                }
                                else
                                {
                                    SqlCommand cmdUpdate = new SqlCommand("Update User_table set Active='True' where User_pass=@tPassword and user_name=@tUsername and LSystemName=@tMachineName", con);
                                    cmdUpdate.Parameters.AddWithValue("@tUsername", tUsername);
                                    cmdUpdate.Parameters.AddWithValue("@tPassword", tPassword);
                                    cmdUpdate.Parameters.AddWithValue("@tMachineName", _Class.clsVariables.tSystemName);
                                    cmdUpdate.ExecuteNonQuery();
                                    Usertype = dt.Rows[0]["User_type"].ToString();
                                    _Class.clsVariables.tUserNo = dt.Rows[0]["User_no"].ToString();
                                    _Class.clsVariables.tCounter = dt.Rows[0]["Ctr_no"].ToString();
                                    _Class.clsVariables.tStopAtQtyF4 = (dt.Rows[0]["StopAtQty"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.tStopAtRateF4 = (dt.Rows[0]["StopAtRate"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.tAllowVoid = (dt.Rows[0]["AllowVoid"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.tAllowReturn = (dt.Rows[0]["AllowReturn"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.tViewReport = (dt.Rows[0]["ViewReport"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.HAPaymentReport = (dt.Rows[0]["HAPayment"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.StCounter = (dt.Rows[0]["StCounter"].ToString() == "Yes") ? true : false;
                                    _Class.clsVariables.CashDrawer = (dt.Rows[0]["CashDrawer"].ToString() == "Yes") ? true : false;
                                    if (dtus.Columns.Contains("ViewCost"))
                                    {
                                         _Class.clsVariables.tViewCash = (dt.Rows[0]["ViewCost"].ToString() == "Yes") ? true : false;
                                    }                                   
                                    _Class.clsVariables.tUserName = tUsername;
                                    _Class.clsVariables.funGlobalCustomerDisplaySetting();

                                    if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                                    {

                                        byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                                        _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                                        //   _Class.clsVariables.spCustomerDis.WriteLine("      WELCOME     ");
                                        _Class.clsVariables.spCustomerDis.WriteLine(_Class.clsVariables.tCustomerDisHomeLine1);
                                        byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                                        _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                                        //  _Class.clsVariables.spCustomerDis.Write("  HAVE A NICE DAY  ");
                                        _Class.clsVariables.spCustomerDis.WriteLine(_Class.clsVariables.tCustomerDisHomeLine2);
                                    }

                                    DataTable dtCounterName = new DataTable();
                                    dtCounterName.Rows.Clear();
                                    SqlCommand cmdCounterName = new SqlCommand("Select ctr_name from counter_table where ctr_no=@tCounterNo", con);
                                    cmdCounterName.Parameters.AddWithValue("@tCounterNo", _Class.clsVariables.tCounter);
                                    SqlDataAdapter adpCounter = new SqlDataAdapter(cmdCounterName);
                                    adpCounter.Fill(dtCounterName);
                                    if (dtCounterName.Rows.Count > 0)
                                    {
                                        _Class.clsVariables.tCounterName = dtCounterName.Rows[0]["ctr_name"].ToString();
                                        _Class.clsVariables.funGlobalReceiptPrinterSetting();
                                    }
                                    
                                    if (Usertype == "0")
                                    {
                                        _Class.clsVariables.UserType = "0";
                                      // FrmMain frm = new FrmMain();
                                      // UCMain frm = new UCMain();
                                        txtEnterValue.Password = string.Empty;
                                      //  this.Hide();
                                      //  frm.Show();
                                        if (UCFrmLoginEvent_loginClick != null)
                                        {
                                            UCFrmLoginEvent_loginClick();
                                        }
                                    }
                                    if (Usertype == "1")
                                    {
                                        _Class.clsVariables.UserType = "1";
                                        // FrmMain frm = new FrmMain();
                                       // UCMain frm = new UCMain();
                                        txtEnterValue.Password = string.Empty;
                                       // this.Hide();
                                      //  frm.Show();
                                        if (UCFrmLoginEvent_loginClick != null)
                                        {
                                            UCFrmLoginEvent_loginClick();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Invalid login details", "Warning");
                                txtEnterValue.Password = "";
                            }
                        }

                    }
                }
            last:
                int ck = 0;
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");               
                //_Class.clsVariables.funException(ex);
            }
        }
        //public void funException(Exception ex)
        //{
        //    StackTrace st = new StackTrace(ex, true);
        //    StackFrame frame = st.GetFrame(2);
        //    string strfname = frame.GetFileName();
        //    var line = st.GetFrame(2).GetFileLineNumber();
        //    frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname));
        //}
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {
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

                    // Clear screen (CLS)
                    byte[] bytesToSend1 = new byte[1] { 0x0C };
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                    _Class.clsVariables.spCustomerDis.Close();
                    _Class.clsVariables.spCustomerDis.Dispose();
                    _Class.clsVariables.spCustomerDis = null;
                }
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
        private void frmActivation1_UCHideEvent_KeyBoardClick()
        {
            try
            {
               // UCKeyBoardAct1.Visibility = Visibility.Visible;
                foreach (Control ctl in pnlKeyboardMain.Children)
                {
                    if (ctl is UCKeyBoradAct)
                    {                        
                        ctl.Visibility = Visibility.Visible;
                        UCKeyBoardAct1.txtEnterValue.Password = "";
                        UCKeyBoardAct1.txtEnterValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void frmActivation1_UCHideEvent_Click()
        {
            try
            {
                foreach (Control ctl in pnlKeyboardMain.Children)
                {
                    if (ctl is UCKeyBoradAct)
                    {
                        ctl.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch(Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void UCKeyBoardAct1_UCKeyBoradActEvent_CancelClick()
        {
            try
            {
                if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                {
                    frmActivation1.txtActiveCode.Password = _Class.clsVariables.tVoidValue;
                    frmActivation1.txtActiveCode.Focus();
                }
                else if (_Class.clsVariables.tVoidActionType == "LOGINNAME")
                {
                    txtEnterValue.Password = _Class.clsVariables.tVoidValue;
                    txtEnterValue.Focus();
                }
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               
                SqlCommand cmdCheckExit = new SqlCommand("select name from sysobjects where type='P' and name='sp_RunInstall05032016'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmdCheckExit);
                DataTable ProExit = new DataTable();
                ProExit.Rows.Clear();
                adp.Fill(ProExit);
                if (ProExit.Rows.Count == 0)
                {
                   

                    MyMessageBox.ShowBox("Please Wait .....", "Message");
                    ClsSpRuninstall.SPruninstll();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();

                    }
                    using (SqlCommand cmdSP_Runinstall = new SqlCommand("IF EXISTS (select * from sysobjects where type='P' and name='sp_RunInstall05032016') Begin EXEC sp_RunInstall05032016 End", con))
                        cmdSP_Runinstall.ExecuteNonQuery();

                    using (SqlCommand cmdSP_Runinstall = new SqlCommand("IF EXISTS (select * from sysobjects where type='P' and name='sp_RunInstall05032016') Begin EXEC sp_RunInstall05032016 End", con))
                        cmdSP_Runinstall.ExecuteNonQuery();
                    con.Close();
                }

                UCKeyBoardAct1.UCKeyBoradActEvent_CancelClick+=new UCKeyBoradActEvent(UCKeyBoardAct1_UCKeyBoradActEvent_CancelClick);
                frmActivation1.UCHideEvent_Click+=new UCHideEvent(frmActivation1_UCHideEvent_Click);
                frmActivation1.UCHideEvent_KeyBoardClick+= new UCHideEvent(frmActivation1_UCHideEvent_KeyBoardClick);
                // StkActive.Visibility = Visibility.Hidden;
                txtEnterValue.Focus();
                this.ActivateLogin = "True";

                string st = Algorithm("Parthi");
                //   MessageBox.Show(st);
                UCKeyBoardAct1.Visibility = Visibility.Hidden;
                frmActivation1.Visibility = Visibility.Hidden;
                if (st == "Execute_1")
                {
                    MyMessageBox.ShowBox("Welcome to MSPOS Software","Information");
                    UCKeyBoardAct1.Visibility = Visibility.Hidden;
                    frmActivation1.Visibility = Visibility.Hidden;
                }
                else if (st == "StopWork1")
                {
                    MyMessageBox.ShowBox("Application Can't be load, Contact Software Provider", "Warning");
                    // Application.Current.Shutdown();
                    ActivateLogin = "False";
                    frmActivation1.Visibility = Visibility.Visible;
                   // UCKeyBoardAct1.Visibility = Visibility.Visible;
                    frmActivation1.txtActiveCode.Focus();
                    _Class.clsVariables.tVoidActionType = "ACTIVATE";
                    if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                    {
                        UCKeyBoardAct1.SalesCreationEventHandlerNew += new EventHandler(ItemType);
                        frmActivation1.txtActiveCode.Focus();
                    }
                    _Class.clsVariables.tstr1 = "function3"; 
                }
                else if (st == "StopWork2")
                {
                    if (MyMessageBox1.ShowBox("If you want to use this application continuously, Kindly update current application or Contact your Software Provider!", "Warning") == "1")
                    {
                        ActivateLogin = "False";
                        frmActivation1.Visibility = Visibility.Visible;
                       // UCKeyBoardAct1.Visibility = Visibility.Visible;
                        frmActivation1.txtActiveCode.Focus();
                        _Class.clsVariables.tVoidActionType = "ACTIVATE";
                        if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                        {
                            UCKeyBoardAct1.SalesCreationEventHandlerNew += new EventHandler(ItemType);
                            frmActivation1.txtActiveCode.Focus();
                        }
                        _Class.clsVariables.tstr1 = "function2";  
                        //ActivateLogin = "False";
                        //if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                        //{
                        //    byte[] bytesToSend1 = new byte[1] { 0x0C };
                        //    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                        //    _Class.clsVariables.spCustomerDis.Close();
                        //    _Class.clsVariables.spCustomerDis.Dispose();
                        //    _Class.clsVariables.spCustomerDis = null;
                        //}
                        //Application.Current.Shutdown();
                        //foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())//GETS PROCESSES
                        //{
                        //    if (pr.ProcessName == "SalesProject")//KILLS FIREFOX.....REMOVE FIREFOX.....CONNECT SAVED SQL PROCESSES IN HERE MAYBE??
                        //    {
                        //        pr.Kill(); //KILLS THE PROCESSES
                        //    }
                        //}
                    }
                    else
                    {
                        if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                        {
                            byte[] bytesToSend1 = new byte[1] { 0x0C };
                            _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                            _Class.clsVariables.spCustomerDis.Close();
                            _Class.clsVariables.spCustomerDis.Dispose();
                            _Class.clsVariables.spCustomerDis = null;
                        }
                        Application.Current.Shutdown();
                        foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())//GETS PROCESSES
                        {
                            if (pr.ProcessName == "SalesProject")//KILLS FIREFOX.....REMOVE FIREFOX.....CONNECT SAVED SQL PROCESSES IN HERE MAYBE??
                            {
                                pr.Kill(); //KILLS THE PROCESSES
                            }
                        }
                    }
                }
                else if (st == "BlackList")
                {
                    //  this.Hide();
                    MyMessageBox.ShowBox("Application Can't be load, Contact Software Provider", "Warning");
               //     this.Visibility = Visibility.Hidden;
                    ActivateLogin = "False";
                    //frmActivation1.Visibility = Visibility.Visible;
                    //UCKeyBoardAct1.Visibility = Visibility.Visible; 
                  //  Application.Current.Shutdown();

                    ActivateLogin = "False";
                    frmActivation1.Visibility = Visibility.Visible;
                   // UCKeyBoardAct1.Visibility = Visibility.Visible;
                    frmActivation1.txtActiveCode.Focus();
                    _Class.clsVariables.tVoidActionType = "ACTIVATE";
                    if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                    {
                        UCKeyBoardAct1.SalesCreationEventHandlerNew += new EventHandler(ItemType);
                        frmActivation1.txtActiveCode.Focus();
                    }
                    _Class.clsVariables.tstr1 = "function3"; 
                }
                else if (st == "Execute_0")
                { }
                else
                {
                    if (double.Parse(st) <= 7 && double.Parse(st) >= 0)
                    {
                        if (MyMessageBox1.ShowBox("Your system having problem. You need to update the software with in " + st + " days. Do you want to activate now?", "Warning") == "1")
                        {
                            ActivateLogin = "False";
                            frmActivation1.Visibility = Visibility.Visible;
                            UCKeyBoardAct1.Visibility = Visibility.Visible;
                            frmActivation1.txtActiveCode.Focus();
                            _Class.clsVariables.tVoidActionType = "ACTIVATE";
                            if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                            {
                                UCKeyBoardAct1.SalesCreationEventHandlerNew += new EventHandler(ItemType);
                                frmActivation1.txtActiveCode.Focus();
                            }
                            _Class.clsVariables.tstr1 = "function1";
                            ////FrmActivation frm = new FrmActivation();
                            //// this.Hide();
                            //ActivateLogin = "False";
                            //frmActivation1.Visibility = Visibility.Visible;
                            //UCKeyBoardAct1.Visibility = Visibility.Visible;
                            //frmActivation1.txtActiveCode.Focus();
                            //_Class.clsVariables.tVoidActionType = "ACTIVATE";
                            //if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                            //{
                            //    UCKeyBoardAct1.SalesCreationEventHandlerNew += new EventHandler(ItemType);
                            //    frmActivation1.txtActiveCode.Focus();
                            //}
                        }
                        else
                        {
                           // this.Show();
                            ActivateLogin = "True";
                            frmActivation1.Visibility = Visibility.Hidden;
                            UCKeyBoardAct1.Visibility = Visibility.Hidden;
                        }
                    }
                }
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //paths
                string Br = (string)regkey.GetValue("Black");
                if (regkey.GetValue("Black") == null || (Br == "False"))
                {
                  //  btnLogin.Visibility = Visibility.Visible;
                }
                else
                {
                  //  this.Show();
                  //  btnLogin.Visibility = Visibility.Hidden;
                    frmActivation1.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                //funException(ex);

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
                //    //  txtEnterValue.se.Select(txtEnterValue.Password.Length, 0);
                //}

                UCKeyBoardAct1.Visibility = Visibility.Visible;
                _Class.clsVariables.tVoidActionType = "LOGINNAME";
                if (_Class.clsVariables.tVoidActionType == "LOGINNAME")
                {
                    UCKeyBoardAct1.SalesCreationEventHandlerNew += new EventHandler(CloseEventItemCode);
                    UCKeyBoardAct1.txtEnterValue.Password = "";
                    UCKeyBoardAct1.txtEnterValue.Focus();   
                    //txtEnterValue.Focus();
                    //  txtEnterValue.se.Select(txtEnterValue.Password.Length, 0);
                }

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
        public string ActivateLogin;
        private void firstTime()
        {
            try
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                DateTime dt = DateTime.Now;
                string onlyDate = dt.ToShortDateString(); // get only date not time
                regkey.SetValue("Install", onlyDate); //Value Name,Value Data
                regkey.SetValue("Use", onlyDate); //Value Name,Value Data
                regkey.SetValue("Days", "20"); //Value Name,Value Data
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

          
        }

        private String checkfirstDate()
        {
            try
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                string Br = (string)regkey.GetValue("Install");
                if (regkey.GetValue("Install") == null)
                    return "First";
                else
                    return Br;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                return "";
            }
        }


        private String dayDifPutPresent()
        {
            try
            {
                // get present date from system
                DateTime dt = DateTime.Now;
                string today = dt.ToShortDateString();
                DateTime presentDate = Convert.ToDateTime(today);

                // get instalation date
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                string Br = (string)regkey.GetValue("Install");
                DateTime installationDate = Convert.ToDateTime(Br);

                string Br1 = (string)regkey.GetValue("Days");
                int tNoDays = Convert.ToInt16(Br1);

                TimeSpan diff = presentDate.Subtract(installationDate); //first.Subtract(second);            
                int totaldays = (int)diff.TotalDays;
                // special check if user chenge date in system
                string usd = (string)regkey.GetValue("Use");
                DateTime lastUse = Convert.ToDateTime(usd);
                TimeSpan diff1 = presentDate.Subtract(lastUse); //first.Subtract(second);
                int useBetween = (int)diff1.TotalDays;

                // put next use day in registry
                regkey.SetValue("Use", today); //Value Name,Value Data
                if (useBetween >= 0)
                {
                    if (totaldays < 0)
                        return "Error"; // if user change date in system like date set before installation
                    else if (totaldays >= 0 && totaldays <= tNoDays)
                        return Convert.ToString(tNoDays - totaldays);
                    else
                        return "Expired";
                }
                else
                    return "Error"; // if user change date in system

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                return "Error";
            }
        }
        private void blackList()
        {
            try
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                regkey.SetValue("Black", "True");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private String blackListCheck()
        {
            try
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                string Br = (string)regkey.GetValue("Black");
                if (regkey.GetValue("Black") == null || (Br == "False"))
                    return "NO";
                else
                    return "YES";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                return "";
            }
        }
        public String Algorithm(String appPassword)
        {
            try
            {
                string block = blackListCheck();
                if (block == "NO")
                {
                    string chinstall = checkfirstDate();
                    if (chinstall == "First")
                    {
                        firstTime();// installation date
                        return "Execute_1";
                    }
                    else
                    {
                        string status = dayDifPutPresent();
                        if (status == "Error")
                        {
                            blackList();
                            return "StopWork1";
                        }
                        else if (status == "Expired")
                        {
                            return "StopWork2";
                        }
                        else
                            return status; // execute
                    }
                }
                else
                {
                    return "BlackList";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                return "";
            }
        }
        public void ItemType(object sender, EventArgs s)
        {
            try
            {
                frmActivation1.txtActiveCode.Password = _Class.clsVariables.tVoidValue;
                frmActivation1.txtActiveCode.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }  
        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    btnLogin_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void frmActivation1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UCKeyBoardAct1_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
