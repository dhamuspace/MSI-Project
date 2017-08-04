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
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.IO.Ports;


namespace SalesProject
{
    /// <summary>
    /// Interaction logic for frmTableCreation.xaml
    /// </summary>
    /// 
    public delegate void UCMainEvent(object sender, RoutedEventArgs e);
    public delegate void UCMainEvent1();
    public partial class UCMain : UserControl
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        //  SalesProject.RestaurantDBDataContext rdb = new SalesProject.RestaurantDBDataContext();
        public event UCMainEvent UCMainEventBackOfficeClick;
        public event UCMainEvent UCMainEventLogoutClick;
        public event UCMainEvent1 UCMainEventPOSVisibleClick;
        public event UCMainEvent UCMainEventPOSWithPreviousClick;
        public event UCMainEvent1 UCMainEventDineInClick;
        int tPageSize = 30;
        int tPage = 1;
        int tTotalNoOfTable = 0;
        int tSkip = 0;

        public UCMain()
        {
            // _Class.clsVariables cls = new _Class.clsVariables();
            InitializeComponent();

            //  ClsFile.ClsRestaurant.funMasterLoad();
            //  ClsFile.ClsRestaurant.funLoadAfterLogin();
            //  ClsFile.ClsRestaurant.funBeginCashDrawerCheck();

            pnlTableList.Visibility = Visibility.Hidden;
            UCFrmManagerMain.Visibility = Visibility.Hidden;
            UCFrmLogin1.Visibility = Visibility.Visible;

        }

        public void funLoad()
        {
            try
            {

                //  ClsFile.ClsRestaurant.funMasterLoad();
                // ClsFile.ClsRestaurant.funLoadAfterLogin();
                // ClsFile.ClsRestaurant.funBeginCashDrawerCheck();
                pnlTableList.Visibility = Visibility.Hidden;
                UCFrmManagerMain.Visibility = Visibility.Hidden;
                UCFrmLogin1.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }


        private void DineinEvents_OnCancelClick()
        {
            pnlTableList.Visibility = Visibility.Visible;
        }




        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmdUpdate = new SqlCommand("Update User_table set Active='False' where ctr_no=(select ctr_no from User_table where User_no=@tUsername)", con);
                cmdUpdate.Parameters.AddWithValue("@tUsername", SalesProject._Class.clsVariables.tUserNo);
                // cmdUpdate.Parameters.AddWithValue("@tPassword", tPassword);
                cmdUpdate.ExecuteNonQuery();

                // ClsFile.ClsRestaurant.tPOSType = "";

                if (UCMainEventLogoutClick != null)
                {
                    UCMainEventLogoutClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }



        private void btnManager_Click(object sender, RoutedEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmdUpgrade = new SqlCommand("sp_btnUpgradeSales", con);
            //adap.SelectCommand.CommandTimeout = 0;


            cmdUpgrade.CommandType = CommandType.StoredProcedure;
            cmdUpgrade.ExecuteNonQuery();
            UCFrmManagerMain.Visibility = Visibility.Visible;
            pnlGroupItem.Visibility = Visibility.Hidden;
            //   UCFrmLogin1.Visibility = Visibility.Hidden;

            UCFrmManagerMain.funLoadManagerMain(sender, e);
        }

        private void btnBackOffice_Click(object sender, RoutedEventArgs e)
        {
            if (SalesProject._Class.clsVariables.UserType != "1")
            {
                frmBackOffice1 frm = new frmBackOffice1();
                //this.Hide();
                this.Visibility = Visibility.Hidden;


                if (UCMainEventBackOfficeClick != null)
                {
                    UCMainEventBackOfficeClick(sender, e);
                }
                frm.Show();
            }
            else
            {
                SalesProject.MyMessageBox.ShowBox("Please, get user rights to open backoffice!!", "Warning");
            }
        }

        private void btnDineIn_Click(object sender, RoutedEventArgs e)
        {
            // ClsFile.ClsRestaurant.tPOSType = "DINEIN";
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmdLastReset = new SqlCommand("sp_SalesCreationSelectSingle", con);
                cmdLastReset.CommandType = CommandType.StoredProcedure;
                cmdLastReset.Parameters.AddWithValue("@tActionType", "BEGINDRAWERCHECK");
                cmdLastReset.Parameters.AddWithValue("@tValue", _Class.clsVariables.tUserNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmdLastReset);
                adp.Fill(dtNew);
                ////SqlDataAdapter cmdRowChk = new SqlDataAdapter("Select * from EndOFDay_table", con);
                ////cmdRowChk.Fill(ds, "ADP1");
                if (dtNew.Rows.Count == 0)
                {
                    SqlCommand cmdInsertEOD = new SqlCommand("sp_updateFirstEndOfday", con);
                    cmdInsertEOD.CommandType = CommandType.StoredProcedure;
                    cmdInsertEOD.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                    cmdInsertEOD.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    cmdInsertEOD.ExecuteNonQuery();
                }
                _Class.clsVariables.LoadPreviousBill = "LoadOnce";
                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlCommand cmdActive = new SqlCommand("sp_SalesCreationSelectSingle", con);
                cmdActive.CommandType = CommandType.StoredProcedure;
                cmdActive.Parameters.AddWithValue("@tActionType", "ACTIVENEWMAIN");
                cmdActive.Parameters.AddWithValue("@tValue", _Class.clsVariables.tUserNo);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmdActive);
                adp1.Fill(dtNew1);
                if (dtNew1.Rows.Count > 0)
                {
                    DataTable dtNew11 = new DataTable();
                    dtNew11.Rows.Clear();
                    SqlCommand cmdActive1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                    cmdActive1.CommandType = CommandType.StoredProcedure;
                    cmdActive1.Parameters.AddWithValue("@tActionType", "ACTIVENEW");
                    cmdActive1.Parameters.AddWithValue("@tValue", _Class.clsVariables.tUserNo);
                    SqlDataAdapter adp11 = new SqlDataAdapter(cmdActive1);
                    adp11.Fill(dtNew11);
                    if (dtNew11.Rows.Count > 0)
                    {
                        if (dtNew11.Rows[0]["Active"].ToString() != "1")
                        {
                            SalesProject._Class.clsVariables.tNoRead = "Read";
                            // UCSalesCreation frm = new UCSalesCreation();
                            //this.Hide();
                            //this.Visibility = Visibility.Hidden;
                            //frm.CurrentBill.Visibility = Visibility.Visible;
                            //frm.UCFormSettle1.Visibility = Visibility.Hidden;
                            //frm.UCfrmVoid1.Visibility = Visibility.Hidden;

                            //frm.Show();

                            if (UCMainEventDineInClick != null)
                            {
                                UCMainEventDineInClick();
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please Enter Begin Cash Drawer Details First", "Warning");
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Begin Cash Drawer Details First", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
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
        private void btnTakeOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrmTIcketIssue tkt = new FrmTIcketIssue();
                tkt.Show();
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void UCFrmLogin1_UCFrmLoginEvent_loginClick()
        {
            try
            {
                UCFrmLogin1.Visibility = Visibility.Hidden;
                pnlTableMain.Visibility = Visibility.Visible;
                pnlTableList.Visibility = Visibility.Visible;
                UCFrmManagerMain.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void sales_Loaded(object sender, RoutedEventArgs e)
        {
            UCFrmManagerMain.UCFrmManagerMainEvent_Finished += new UCFrmManagerMainEvent(UCFrmManagerMain_UCFrmManagerMainEvent_Finished);
            UCFrmLogin1.UCFrmLoginEvent_loginClick += new UCFrmLoginEvent(UCFrmLogin1_UCFrmLoginEvent_loginClick);

            funMainLoad();
        }

        private void UCFrmLogin1_UCFrmLoginEvent_loginClick(object sender, RoutedEventArgs e)
        {

            funMainLoad();
        }
        private void UCFrmManagerMain_UCFrmManagerMainEvent_Finished()
        {
            pnlTableMain.Visibility = Visibility.Visible;
            pnlTableList.Visibility = Visibility.Visible;
            UCFrmManagerMain.Visibility = Visibility.Hidden;
            UCFrmLogin1.Visibility = Visibility.Hidden;
            pnlGroupItem.Visibility = Visibility.Visible;
            funMainLoad();
            this.gridDisplayMain.BringToFront();
        }

        public void funMainLoad()
        {
            try
            {
                lblcounterName.Content = string.IsNullOrEmpty(Convert.ToString(_Class.clsVariables.tCounterName)) ? "Counter" : Convert.ToString(_Class.clsVariables.tCounterName);
                lblUserName.Content = string.IsNullOrEmpty(Convert.ToString(_Class.clsVariables.tUserName)) ? "Admin" : Convert.ToString(_Class.clsVariables.tUserName);
                lblLocationName.Content = string.IsNullOrEmpty(Convert.ToString(_Class.clsVariables.tBranch)) ? "Branch" : Convert.ToString(_Class.clsVariables.tBranch);
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmdLastReset = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmdLastReset.CommandType = CommandType.StoredProcedure;
                cmdLastReset.Parameters.AddWithValue("@tActionType", "UserActivity");
                SqlDataAdapter adp = new SqlDataAdapter(cmdLastReset);
                adp.Fill(dtNew);
                gridDisplayMain.DataSource = dtNew.DefaultView;
                _Class.clsVariables.tWeightScaleEnable = "No";
                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlCommand cmdLastReset1 = new SqlCommand("Select * from WeightScale_table where counter=@tCounter", con);
                cmdLastReset1.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmdLastReset1);
                adp1.Fill(dtNew1);
                if (dtNew1.Rows.Count > 0)
                {
                    if (dtNew1.Rows[0]["Enable"].ToString().Trim() == "Yes")
                    {
                        _Class.clsVariables.tWeightScaleEnable = dtNew1.Rows[0]["Enable"].ToString();
                        _Class.clsVariables.tPort = dtNew1.Rows[0]["PortName"].ToString();
                        _Class.clsVariables.tBaudRate = int.Parse(dtNew1.Rows[0]["BaudRate"].ToString());
                        _Class.clsVariables.serial.Handshake = System.IO.Ports.Handshake.None;
                        if (dtNew1.Rows[0]["Parity"].ToString() == "None")
                        {
                            _Class.clsVariables.tParity = Parity.None;
                        }
                        else if (dtNew1.Rows[0]["Parity"].ToString() == "Even")
                        {
                            _Class.clsVariables.tParity = Parity.Even;
                        }
                        else if (dtNew1.Rows[0]["Parity"].ToString() == "Odd")
                        {
                            _Class.clsVariables.tParity = Parity.Odd;
                        }
                        else if (dtNew1.Rows[0]["Parity"].ToString() == "Mark")
                        {
                            _Class.clsVariables.tParity = Parity.Mark;
                        }
                        else if (dtNew1.Rows[0]["Parity"].ToString() == "Space")
                        {
                            _Class.clsVariables.tParity = Parity.Space;
                        }

                        _Class.clsVariables obj = new _Class.clsVariables();
                    }
                }
                string tChkCusDisEnable = "No";
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
                }
                //pnlTableMain.Visibility = Visibility.Visible;
                //pnlTableList.Visibility = Visibility.Visible;
                //this.gridDisplayMain.Visible = true;
                //UCFrmManagerMain.Visibility = Visibility.Hidden;
                //UCFrmLogin1.Visibility = Visibility.Hidden;
            }

            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

    }
}
