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
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;
using System.Timers;
using System.Drawing.Printing;
using System.Configuration;
//using System.Drawing;
using System.Windows.Controls.Primitives;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Printing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
//using System.Configuration;
using System.IO.Ports;
using System.Threading;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for SalesCreation.xaml
    /// </summary>
    ///
    /// ObservableCollection<ParameterSet> pmset;
    /// 
    public partial class UCSalesCreation : Window
    {
        System.Timers.Timer aTimer;
        string[,] btnItemName = new string[10, 2];
        //  SerialPort serial = new SerialPort("COM5",9600,Parity.None,8,StopBits.One);
        public static RoutedCommand MyCommand = new RoutedCommand();
        public static RoutedCommand MyCash = new RoutedCommand();
        public static RoutedCommand MyNETS = new RoutedCommand();
        public static RoutedCommand MyCashDraw = new RoutedCommand();
        public static RoutedCommand MyPrint = new RoutedCommand();
        public static RoutedCommand MyRemove = new RoutedCommand();
        public static RoutedCommand MyUp = new RoutedCommand();
        public static RoutedCommand MyDown = new RoutedCommand();
        public static RoutedCommand MyVoid = new RoutedCommand();
        public static RoutedCommand MyHold = new RoutedCommand();
        public static RoutedCommand MyStopAtRate = new RoutedCommand();
        public static RoutedCommand MyMinimize = new RoutedCommand();

        public static RoutedCommand MyTenderExact = new RoutedCommand();
        public static RoutedCommand MyOne = new RoutedCommand();
        public static RoutedCommand MyTwo = new RoutedCommand();
        public static RoutedCommand MyFive = new RoutedCommand();
        public static RoutedCommand MyTen = new RoutedCommand();
        public static RoutedCommand MyFifty = new RoutedCommand();
        public static RoutedCommand MyFifteen = new RoutedCommand();
        public static RoutedCommand MyTwenty = new RoutedCommand();
        public static RoutedCommand MyHundred = new RoutedCommand();
        public static RoutedCommand MyCreditCard = new RoutedCommand();
        public static RoutedCommand MyClear = new RoutedCommand();
        public static RoutedCommand MyHouseAc = new RoutedCommand();
        public static RoutedCommand MyVoucher = new RoutedCommand();

        public UCSalesCreation()
        {
            InitializeComponent();
            // Shortcut Key Declaration -Start
            MyCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
            MyCash.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.None));
            MyNETS.InputGestures.Add(new KeyGesture(Key.F6, ModifierKeys.None));
            MyStopAtRate.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.None));
            MyCashDraw.InputGestures.Add(new KeyGesture(Key.F10, ModifierKeys.None));
            MyUp.InputGestures.Add(new KeyGesture(Key.Up, ModifierKeys.None));
            MyDown.InputGestures.Add(new KeyGesture(Key.Down, ModifierKeys.None));
            MyRemove.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.None));
            MyHold.InputGestures.Add(new KeyGesture(Key.F8, ModifierKeys.None));
            MyPrint.InputGestures.Add(new KeyGesture(Key.F9, ModifierKeys.None));
            MyVoid.InputGestures.Add(new KeyGesture(Key.F3, ModifierKeys.None));
            MyMinimize.InputGestures.Add(new KeyGesture(Key.F12, ModifierKeys.None));

            MyTenderExact.InputGestures.Add(new KeyGesture(Key.D1, ModifierKeys.Alt));
            MyOne.InputGestures.Add(new KeyGesture(Key.D2, ModifierKeys.Alt));
            MyTwo.InputGestures.Add(new KeyGesture(Key.D3, ModifierKeys.Alt));
            MyFive.InputGestures.Add(new KeyGesture(Key.D4, ModifierKeys.Alt));
            MyTen.InputGestures.Add(new KeyGesture(Key.D5, ModifierKeys.Alt));
            MyFifty.InputGestures.Add(new KeyGesture(Key.D6, ModifierKeys.Alt));
            MyHundred.InputGestures.Add(new KeyGesture(Key.D7, ModifierKeys.Alt));
            MyFifteen.InputGestures.Add(new KeyGesture(Key.D8, ModifierKeys.Alt));
            MyTwenty.InputGestures.Add(new KeyGesture(Key.D9, ModifierKeys.Alt));
            MyCreditCard.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Alt));
            MyClear.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            MyHouseAc.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Alt));
            MyVoucher.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));

            // Shortcut Key Declaration -End
            //Datatable Column Initialization -Start
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("Amt", typeof(string));
                dt.Columns.Add("Disc", typeof(string));
                dt.Columns.Add("SDisc", typeof(string));
                dt.Columns.Add("Other", typeof(string));
                dt.Columns.Add("Serial", typeof(string));

            }

            if (dtPrint.Columns.Count == 0)
            {
                dtPrint.Columns.Add("Describ", typeof(string));
                dtPrint.Columns.Add("Property", typeof(string));
            }

            if (tempdtSingleFree.Columns.Count == 0)
            {
                tempdtSingleFree.Columns.Add("ItemName", typeof(string));
                tempdtSingleFree.Columns.Add("Qty", typeof(string));
            }

            if (dtFreeBalance.Columns.Count == 0)
            {
                dtFreeBalance.Columns.Add("ItemName", typeof(string));
                dtFreeBalance.Columns.Add("Qty", typeof(string));
            }

            if (dtSettle.Columns.Count == 0)
            {
                dtSettle.Columns.Add("SalRecLed");
                dtSettle.Columns.Add("SalRecAmt");
                dtSettle.Columns.Add("SalRecRefundAmt");
                dtSettle.Columns.Add("SalRecType");
            }
            if (dtserial.Columns.Count == 0)
                dtserial.Columns.Add("Serial_no");


            //Datatable Column Initialization -End
            //Timer Start for Customer Display Msg Showing 
            if (_Class.clsVariables.tNoRead == "Read")
            {
                try
                {
                    //  _Class.clsVariables.tNoRead = "Read";
                    aTimer = new System.Timers.Timer(10000);
                    aTimer.Interval = 1000;
                    aTimer.Enabled = true;

                }
                catch (Exception ex)
                {
                    MyMessageBox.ShowBox(ex.Message, "Warning");
                }
            }
            txtEnterValue.Focus();
        }
        string vPrevBill = "No";
        string vMainTable = "Yes";
        public DataTable dtSettle = new DataTable();
        public DataTable dtserial = new DataTable();
        public void FirstMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Discount Shortcut (Shortcut Key: Alt+D)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnDiscount_Click(sender, e);
            }
            txtEnterValue.Focus();
        }


        public void StopAtRateMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Stop at Rate or Stop at Qty shortcut coding (Shortcut Key: F4)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funF4();
            }
            txtEnterValue.Focus();
        }

        public void btnCashMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Cash Sales Shortcut key coding Start (Shortcut Key:F5)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnCash_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnNETSMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Nets Sales Shortcut key coding Start (Shortcut Key:F6)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnNETS_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnVoidMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Sales Void Screen Shortcut key coding Start (Shortcut Key:F3)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnVoid_Click(sender, e);
            }
            txtEnterValue.Focus();
        }


        public void btnMinimizeMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Sales Screen Minimize Shortcut key coding Start (Shortcut Key:F12)
            WindowState = WindowState.Minimized;
            txtEnterValue.Focus();
        }

        public void btnCashDrawMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Cash Drawer Open Shortcut key coding Start (Shortcut Key:F10)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnCashDraw_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnHoldMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Hold Screen  Shortcut key coding Start (Shortcut Key:F8)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnHold_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnPrintMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Print Previous Sales bill Shortcut key coding Start (Shortcut Key:F9)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnPrint_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnRemoveMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Remove Selected Item from the list of Sales Item Shortcut key coding Start (Shortcut Key:Delete)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnRemove_Click(sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnUpMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Select next Top Item from the list of Current Selected Item Shortcut key coding Start (Shortcut Key:Up Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnUp_Click(sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnDownMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Select next Down Item from the list of Current Selected Item Shortcut key coding Start (Shortcut Key:Down Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnDown_Click(sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnTenderExactMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle Exact bill Amount Shortcut key coding Start (Shortcut Key:Alt+D1 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnCash_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnOneMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 1 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D2 Arrow)            
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("1", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnTwoMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 2 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D3 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("2", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnFiveMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 5 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D4 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("5", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnTenMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 10 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D5 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("10", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnFiftyMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 50 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D6 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("50", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnFifteenMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 15 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D8 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("15", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }

        public void btnTwentyMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 20 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D9 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("20", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnHundredMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Settle 100 dolor bill Amount Shortcut key coding Start (Shortcut Key:Alt+D7 Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                funOnedolor("100", Convert.ToString(lblNetAmt.Content), sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnCreditCardMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Open Tender screen for settle bill amount through Credit Card Shortcut key coding Start (Shortcut Key:Alt+B Arrow)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnTender_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void funClear()
        {
            try
            {
                //Clear or Reset All Sales screen Control Values for next bill
                gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                dt.Clear();
                dtFreeBalance.Rows.Clear();
                _Class.clsVariables.dtSingleFree.Rows.Clear();
                frmDiscountDisplay.Visibility = Visibility.Hidden;
                UCItemDiscount1.Visibility = Visibility.Hidden;
                lblOverAllDiscAmt.Content = "0.00";
                lblSpecialDiscAmt.Content = "0.00";
                lblGroupDiscAmt.Content = "0.00";
                lblNetAmt.Content = "0.00";
                lblDiscount.Content = "0.00";
                lblTotQty.Content = "0.00";
                lblTotAmt.Content = "0.00";
                lblTaxAmt.Content = "0.00";


                //Customer Display function calling Coding -Start
                funThankYou();
                //Customer Display function calling Coding -End


                //Print Previous Bill function calling Coding -Start
                funPreviousBill();
                //Print Previous Bill function calling Coding -Start

                //Customer Display Balance Amount Display function Calling code Start
                funBalanceAmtDisplay();
                //Customer Display Balance Amount Display function Calling code End

                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public void btnClearMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Clear or Reset Sales screen Controls values using Shortcut. (Shortcut Key: Alt+C)
            funClear();
            txtEnterValue.Focus();
        }
        public void btnHouseAcMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Open Tender form for settle House Account Amount Using shortcut (Shortcut Key: Alt+H)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnTender_Click(sender, e);
            }
            txtEnterValue.Focus();
        }
        public void btnVoucherMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //Open Tender form for settle House Account Amount Using shortcut (Shortcut Key: Alt+V)
            if (CurrentBill.Visibility == Visibility.Visible)
            {
                btnTender_Click(sender, e);
            }
            txtEnterValue.Focus();
        }

        public void funOnedolor(string lastItem, string txtAmount, Object sender, RoutedEventArgs e)
        {
            try
            {
                //Cash Settle Coding Start Here

                //txtEnterValue.Text = (lastItem.ToString() == "") ? "0:00" : string.Format("{0:0.00}", double.Parse(lastItem.ToString()));
                // txtEnterValue.Select(txtEnterValue.Text.Length, 0);

                //  if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                {
                    if (double.Parse(lastItem) >= double.Parse(txtAmount.Trim()))
                    {

                        if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                        {

                            SqlCommand cmd = new SqlCommand("sp_funBtnDolor1", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                            //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                            cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                            cmd.Parameters.AddWithValue("@RoundValue", tot);
                            for (int mnk = 0; mnk < dt.Rows.Count; mnk++)
                            {
                                if (dt.Rows[mnk]["Disc"].ToString().Trim() == "")
                                {
                                    dt.Rows[mnk]["Disc"] = "0.00";
                                }
                            }
                            cmd.Parameters.AddWithValue("@tempTable", dt);
                            if (double.Parse(lblDiscount.Content.ToString()) > 0)
                            {
                                cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                            }

                            dtSettle.Rows.Clear();
                            double tSettle = (Convert.ToDouble(lastItem) - Convert.ToDouble(txtAmount.Trim()));
                            if (tSettle >= 0)
                            {
                                dtSettle.Rows.Add("5", txtAmount.Trim(), tSettle, "Cash");
                            }
                            else
                            {
                                dtSettle.Rows.Add("5", txtEnterValue.Text.Trim(), 0, "Cash");
                            }

                            cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                            cmd.Parameters.AddWithValue("@tValue", lastItem);
                            cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Trim());
                            cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);

                            cmd.ExecuteNonQuery();



                            gridItems.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                            dt.Clear();
                            dtFreeBalance.Rows.Clear();
                            _Class.clsVariables.dtSingleFree.Rows.Clear();
                            frmDiscountDisplay.Visibility = Visibility.Hidden;
                            UCItemDiscount1.Visibility = Visibility.Hidden;
                            lblOverAllDiscAmt.Content = "0.00";
                            lblSpecialDiscAmt.Content = "0.00";
                            lblGroupDiscAmt.Content = "0.00";
                            lblNetAmt.Content = "0.00";
                            lblDiscount.Content = "0.00";
                            lblTotQty.Content = "0.00";
                            lblTotAmt.Content = "0.00";
                            lblTaxAmt.Content = "0.00";

                            funThankYou();
                            //  SalesCreationEventHandlerNew(sender, e);
                            CloseEvent2(sender, e);

                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please Select Product First", "Warning");
                        }

                    }
                    else
                    {
                        MyMessageBox.ShowBox("Settle amount must higher than the bill amount", "Warning");
                    }
                    txtEnterValue.Focus();
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        DateTime currentDate;
        public string ParameterName { get; set; }
        public int Value { get; set; }
        string temp = null;
        public string holdString = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        //SqlDataReader dr = null;
        public DataTable dt = new DataTable("Items");

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            // Numeric Keyboard Button codig Start
            txtEnterValue.Focus();
            Button btn = (Button)sender;
            if (txtEnterValue.Text != "")
            {
                temp = txtEnterValue.Text;
                txtEnterValue.Text = "";
                txtEnterValue.Text = temp + btn.Content.ToString();
            }
            if (txtEnterValue.Text == "")
            {
                txtEnterValue.Text = btn.Content.ToString();
            }
            txtEnterValue.Select(txtEnterValue.Text.Length, 0);
            txtEnterValue.Focus();
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            //Delete Last keyed letter 
            if (txtEnterValue.Text.Length > 0)
            {
                temp = txtEnterValue.Text;
                txtEnterValue.Text = temp.Remove(temp.Length - 1);
            }
            txtEnterValue.Focus();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // virtual keyboard open coding

            try
            {
                if (pnlNumeric.Visibility == Visibility.Hidden)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (_Class.clsVariables.tHideKeyboard == true)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        pnlNumeric.Visibility = Visibility.Visible;
                    }
                }
                //open virtual Keybord
                frmKeyBoard frm = new frmKeyBoard();
                _Class.clsVariables.tVoidActionType = "SALESITEMCODE";

                if (_Class.clsVariables.tVoidActionType == "SALESITEMCODE")
                {
                    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEventItemCode);
                    frm.ShowDialog();
                    txtEnterValue.Focus();
                    txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
            txtEnterValue.Focus();
        }

        public void CloseEventItemCode(object sender, EventArgs e)
        {
            //No Need this Event
            //if (_Class.clsVariables.tVoidActionType == "BILLNO")s
            //{
            txtEnterValue.Text = _Class.clsVariables.tVoidValue;
            txtEnterValue.Focus();
            //}
            //else if (_Class.clsVariables.tVoidActionType == "SALESMEN")
            //{
            //    uCSalesmen1.txtNote.Text = _Class.clsVariables.tVoidValue;
            //    uCSalesmen1.txtNote.Focus();
            //}
        }

        public void funConnectionStateCheck()
        {
            //Check Connection State Here
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Sales Screen Exit Button Code here                
                popup.IsOpen = false;
                string tUnload = "";
                vMainTable = "Yes";
                _Class.clsVariables.dtserailno.Rows.Clear();
                listSelect.Items.Clear();
                if (gridItems.Rows.Count > 0)  //Change if (gridItems.Items.Count > 0)
                {

                    //If the list contain Items or Hold form holded something, At the time this msg will display
                    string res = MyMessageBox1.ShowBox("Do you want to delete the hold/current bill details?", "Information");
                    if (res == "1")
                    {
                        // This Proceduce delete holded Values
                        SqlCommand cmd6 = new SqlCommand("sp_HoldTruncate", con);
                        cmd6.CommandType = CommandType.StoredProcedure;
                        funConnectionStateCheck();
                        cmd6.ExecuteNonQuery();



                        dt.Rows.Clear();
                        _Class.clsVariables.dtSingleFree.Rows.Clear();
                        gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                        dtFreeBalance.Rows.Clear();
                        frmDiscountDisplay.Visibility = Visibility.Hidden;
                        UCItemDiscount1.Visibility = Visibility.Hidden;
                        lblOverAllDiscAmt.Content = "0.00";
                        lblSpecialDiscAmt.Content = "0.00";
                        lblGroupDiscAmt.Content = "0.00";

                        funClear();
                        UCFormSettle1.Visibility = Visibility.Hidden;
                        UCfrmVoid1.Visibility = Visibility.Hidden;
                        CurrentBill.Visibility = Visibility.Hidden;
                        UCMain1.Visibility = Visibility.Visible;
                        UCMain1.funMainLoad();
                        tUnload = "Clear";
                    }
                }
                else
                {

                    funClear();
                    UCFormSettle1.Visibility = Visibility.Hidden;
                    UCfrmVoid1.Visibility = Visibility.Hidden;
                    CurrentBill.Visibility = Visibility.Hidden;
                    UCMain1.Visibility = Visibility.Visible;
                    UCMain1.funMainLoad();
                    tUnload = "Clear";
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                // This Procedure move the temp table contain bill details to main table
                SqlCommand cmdUpgrade = new SqlCommand("sp_btnUpgradeSales", con);
                cmdUpgrade.CommandType = CommandType.StoredProcedure;
                cmdUpgrade.ExecuteNonQuery();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public string tempGroupNo;
        public int tempCount;


        public void funScrollGrid()
        {

            //this method helps to focusing item row at runtime
            try
            {
                if (gridItems.Rows.Count > 0)
                {
                    int firstDisplayed = gridItems.SelectedRows[0].Index;
                    int displayed = gridItems.DisplayedRowCount(true);
                    int lastVisible = (firstDisplayed + displayed) - 1;
                    int lastIndex = gridItems.RowCount - 1;

                    if (lastVisible == lastIndex)
                    {
                        if (lastIndex != 0)
                        {
                            gridItems.FirstDisplayedScrollingRowIndex = firstDisplayed + 1;
                        }
                    }
                    else
                    {
                        gridItems.FirstDisplayedScrollingRowIndex = gridItems.SelectedRows[0].Index;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        //DataView view;
        SqlDataReader reader = null;
        int count = 0;
        int rowIndex;
        double totQty, totAmt, totTax, tTax;

        public void funStockDisplay(string productName)
        {
            try
            {
                //this method helps to display selected item Closing stock on top of the Sales Screen
                funConnectionStateCheck();
                SqlCommand cmd = new SqlCommand("sp_SalesCreationStockDisplay", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tItemName", productName);
                cmd.Parameters.AddWithValue("@tBranch_Name", _Class.clsVariables.tBranch.ToString());
                SqlParameter result = new SqlParameter("@tResult", SqlDbType.VarChar, 400);
                result.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(result);
                //DataTable dt = new DataTable();
                //SqlDataAdapter adap = new SqlDataAdapter(cmd);
                //adap.Fill(dt);
                cmd.ExecuteNonQuery();
                lblStock.Content = result.Value.ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        int tTimerCount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //This method helps to display Total bill amount after 5 seconds in Customer Display 
            tTimerCount++;
            if (tTimerCount == 5)
            {
                tempTimer.Stop();

                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {
                    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                    _Class.clsVariables.spCustomerDis.WriteLine("Total Bill Amount");
                    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen

                    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                    _Class.clsVariables.spCustomerDis.Write(lblNetAmt.Content.ToString());

                }
            }
            txtEnterValue.Focus();
        }
        private void timer11_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        int tSelectedRowIndex = 0;
        string rowSelect;

        System.Windows.Forms.Timer tempTimer = new System.Windows.Forms.Timer();
        string tItemNameGlob = "";
        string tShowQty = "";
        double tReadCount = 0;
        //  DataTable dtSingleFree = new DataTable();
        DataTable tempdtSingleFree = new DataTable();
        double tNewValueAmt = 0;
        string strCus = "";
        public void EventCustomerList(object sender, EventArgs e)
        {
            //SqlCommand cmdCus = new SqlCommand("", con);
            strCus = _Class.clsVariables.tempCustomerLedgerNo;
        }

        private void Item_Load(string val)
        {
            try
            {

                DataTable DTM = new DataTable();
                List<string> lstring = new List<string>();
                List<string> rstring = new List<string>();
                //SqlCommand comm = new SqlCommand("select Item_no from  serialno_transtbl where inout=1 and barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name Like '" + txtEnterValue.Text.Trim() + "' OR Item_Code Like '" + txtEnterValue.Text.Trim() + "')", con);
                //SqlCommand comm = new SqlCommand("select Item_no from  serialno_transtbl where inout=1 and barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name Like '%" + txtEnterValue.Text.Trim() + "%' OR Item_Code Like '" + txtEnterValue.Text.Trim() + "')", con);
                //listSelect.SelectedItem.ToString() 
                SqlCommand comm = new SqlCommand("select Item_no from  serialno_transtbl where inout=1 and barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name Like '" + val.Trim() + "')", con);
                SqlDataAdapter adap = new SqlDataAdapter(comm);
                adap.Fill(DTM);
                if (DTM.Rows.Count != 0)
                {
                    listSelect.Items.Clear();
                    if (_Class.clsVariables.dtserailno.Rows.Count == 0)
                    {
                        for (int mn = 0; mn < DTM.Rows.Count; mn++)
                        {
                            listSelect.Items.Add(DTM.Rows[mn]["Item_no"].ToString());
                        }
                    }
                    else
                    {
                        int t = 0;
                        for (int mn = 0; mn < DTM.Rows.Count; mn++)
                        {
                            for (int j = 0; j < _Class.clsVariables.dtserailno.Rows.Count; j++)
                            {
                                if (DTM.Rows[mn]["Item_no"].ToString() != _Class.clsVariables.dtserailno.Rows[j]["Serial_no"].ToString())
                                {
                                    t = 1;
                                }
                                else
                                {
                                    DataRow dr = DTM.Rows[mn];
                                    dr.Delete();
                                    DTM.AcceptChanges();
                                    //break;
                                }
                            }
                        }
                        for (int i = 0; i < DTM.Rows.Count; i++)
                            listSelect.Items.Add(DTM.Rows[i]["Item_no"].ToString());
                    }
                }
                else
                {

                    var bc = new BrushConverter();
                    lblLogo.Foreground = (Brush)bc.ConvertFrom("#FFADF213");
                    funConnectionStateCheck();
                    DataRow dr = null;

                    // Check item Selected or not
                    if (listSelect.SelectedItems.Count > 0 || val.Length > 0)
                    {

                        // DataRow dr = null;
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();

                        // Find '*' Exist or not
                        tempFindStar = val.IndexOf("*");
                        // '*' not in name load below code
                        if (tempFindStar == -1)
                        {
                            //Below code Same as newBtnGroupItem Click.. Refer Button Clicke Event
                            DataTable dtBarcode = new DataTable();
                            dtBarcode.Rows.Clear();
                            SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where BarCode=@tBarCode", con);
                            cmdBarcode.Parameters.AddWithValue("@tBarCode", val.Trim());

                            SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                            adpBarcode.Fill(dtBarcode);
                            //Check keyed word exist in Barcode.
                            if (dtBarcode.Rows.Count > 0)
                            {
                                DataTable dtItem1 = new DataTable();
                                dtItem1.Rows.Clear();
                                SqlCommand cmdItemNew = new SqlCommand("Select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_no=@tItemNo", con);
                                cmdItemNew.Parameters.AddWithValue("@tItemNo", dtBarcode.Rows[0]["Item_no"].ToString());
                                SqlDataAdapter adpCmdNew = new SqlDataAdapter(cmdItemNew);
                                adpCmdNew.Fill(dtItem1);
                                if (dtItem1.Rows.Count > 0)
                                {
                                    funConnectionStateCheck();
                                    DataTable dtNew1 = new DataTable();

                                    //   SqlDataReader dr12 = null;
                                    dtNew1.Rows.Clear();

                                    SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@tValue", dtItem1.Rows[0]["Item_Name"].ToString());

                                    cmd.Parameters.AddWithValue("@tActionType", "TXTBOXVALUE");
                                    SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                                    adpCmd.Fill(dtNew1);
                                    //  dr12 = cmd.ExecuteReader();
                                    // dtNew1.Load(dr12);
                                    int isRecord = 0;
                                    for (int mn = 0; mn < dtNew1.Rows.Count; )
                                    {
                                        isRecord = 1;
                                        rowIndex = 0;
                                        dr = dt.NewRow();
                                        //dtserial.Rows.Add(dtNew1.Rows[mn]["Item_Name"].ToString());
                                        _Class.clsVariables.dtserailno.Rows.Add(dtNew1.Rows[mn]["Item_Name"].ToString());
                                        //   MessageBox.Show(dr12["Item_Name"].ToString());                                        
                                        SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@tValue", dtNew1.Rows[mn]["Item_Name"].ToString());
                                        cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
                                        SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                        adpCmd1.Fill(dtNew);
                                        // reader = cmd1.ExecuteReader();
                                        // dtNew.Load(reader);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            count = 0;
                                            totAmt = 0.00;
                                            totQty = 0.00;
                                            totTax = 0.00;
                                            string tempItemName = dtNew.Rows[mn]["Item_Name"].ToString();
                                            tItemNameGlob = tempItemName;
                                            double tUnitDecimals = double.Parse(dtNew.Rows[mn]["unit_Decimals"].ToString());
                                            string tWeightScale = dtNew.Rows[mn]["WeightScale"].ToString();
                                            double tReadingValue = 0;


                                            DataTable dtItem = new DataTable();
                                            dtItem.Rows.Clear();
                                            SqlCommand cmd12 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                            cmd12.Parameters.AddWithValue("@tItemName", tempItemName);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd12);
                                            adp.Fill(dtItem);
                                            bool isChkOpenItem = false;
                                            bool isChkStopAtRate = false;
                                            //bool isChkStopAtQty = false;
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());
                                                // isChkStopAtQty = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());

                                                if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                {
                                                    isChkOpenItem = true;
                                                }
                                            }

                                            if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                                            {
                                                if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                                                {
                                                ReadAgain:
                                                    try
                                                    {
                                                        tReadCount = 0;
                                                        string data = "";
                                                        data = _Class.clsVariables.serial.ReadExisting();
                                                        //serial.Close();
                                                        if (data.IndexOf("kg") > 0)
                                                        {
                                                            data = data.Substring(0, data.IndexOf("kg"));
                                                            data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                            // if
                                                            tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                        }
                                                        else if (data.IndexOf("k") > 0)
                                                        {
                                                            data = data.Substring(0, data.IndexOf("k"));
                                                            data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                            tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        tReadCount++;
                                                        if (tReadCount < 10)
                                                        {
                                                            goto ReadAgain;
                                                        }
                                                        else
                                                        {
                                                            tShowQty = "";
                                                            MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                                            tShowQty = "Show";

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (isChkStopAtRate == true)
                                                    {
                                                        tReadingValue = 0;
                                                    }
                                                    else
                                                    {
                                                        tReadingValue = 1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (isChkStopAtRate == true)
                                                {
                                                    tReadingValue = 0;
                                                }
                                                else
                                                {
                                                    tReadingValue = 1;
                                                }
                                            }

                                            foreach (DataRow dr1 in dt.Rows)
                                            {
                                                if (dr1["itemName"].ToString() == tempItemName)
                                                {
                                                    if (isChkOpenItem != true)
                                                    {
                                                        count = 1;
                                                        if (tUnitDecimals == 0)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                                        }
                                                        if (tUnitDecimals == 1)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                                        }
                                                        if (tUnitDecimals == 2)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                                        }
                                                        if (tUnitDecimals == 3)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                                        }
                                                        if (tUnitDecimals == 4)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                                        }

                                                        {
                                                            dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                        }
                                                        gridItems.Rows[rowIndex].Selected = true;
                                                        rowSelect = "";
                                                        if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                                                        {
                                                            if (tReadingValue > 0)
                                                            {
                                                                //   tempTimer.Start();

                                                            }
                                                        }
                                                    }
                                                }
                                                rowIndex += 1;
                                            }
                                            if (count == 0)
                                            {
                                                dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString();
                                                if (tUnitDecimals == 0)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N0");
                                                }
                                                if (tUnitDecimals == 1)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N1");
                                                }
                                                if (tUnitDecimals == 2)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N2");
                                                }
                                                if (tUnitDecimals == 3)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N3");
                                                }
                                                if (tUnitDecimals == 4)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N4");
                                                }
                                                // dr["Qty"] = "1";
                                                dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));



                                                {
                                                    dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                                }
                                                dt.Rows.Add(dr);
                                                //   funReplaceFreeItemAmt();
                                                tSelectedRowIndex = dt.Rows.Count;
                                                rowSelect = "Last";

                                                tReadingValueDisplay = tReadingValue;
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dr["Qty"]);
                                                drRateDisplay = Convert.ToString(dr["Rate"]);
                                                drAmtDisplay = Convert.ToString(dr["Amt"]);


                                            }
                                            funStockDisplay(tempItemName);
                                            funDisplayAmount(dt);
                                            if (rowSelect != "")
                                            {
                                                gridItems.DataSource = dt.DefaultView;   // Change gridItems.ItemsSource = dt.DefaultView;
                                                gridItems.Columns[0].Width = 180;
                                                gridItems.Columns[0].ReadOnly = true;
                                                gridItems.Columns[1].Width = 50;
                                                gridItems.Columns[2].Width = 50;
                                                gridItems.Columns[3].Width = 50;
                                                gridItems.Columns[3].ReadOnly = true;
                                                gridItems.RowTemplate.Height = 35;
                                            }
                                            gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                                            funScrollGrid();
                                            funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                            funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                            funRoundCalculate();
                                        }
                                        break;
                                    }
                                    if (isRecord == 0)
                                    {
                                        MyMessageBox.ShowBox("Product Not Found", "Warning");
                                    }
                                    txtEnterValue.Text = "";
                                    txtEnterValue.Focus();
                                }
                            }
                            else
                            {
                                //check keyed text in Itemcode or ItemName
                                funConnectionStateCheck();
                                DataTable dtNew1 = new DataTable();

                                //   SqlDataReader dr12 = null;
                                dtNew1.Rows.Clear();

                                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (listSelect.IsVisible == true)
                                {
                                    if (listSelect.SelectedItems.Count > 0)
                                    {
                                        cmd.Parameters.AddWithValue("@tValue", listSelect.SelectedItem.ToString());
                                    }
                                    else
                                    {
                                        // listSelect.SelectedIndex = 0;
                                        // cmd.Parameters.AddWithValue("@tValue", listSelect.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@tValue", val.Trim());
                                    }
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@tValue", val.Trim());
                                }
                                cmd.Parameters.AddWithValue("@tActionType", "TXTBOXVALUE");
                                SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                                adpCmd.Fill(dtNew1);
                                //  dr12 = cmd.ExecuteReader();
                                // dtNew1.Load(dr12);
                                int isRecord = 0;
                                for (int mn = 0; mn < dtNew1.Rows.Count; )
                                {
                                    isRecord = 1;
                                    rowIndex = 0;
                                    dr = dt.NewRow();
                                    //   MessageBox.Show(dr12["Item_Name"].ToString());
                                    SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@tValue", dtNew1.Rows[mn]["Item_Name"].ToString());
                                    cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
                                    SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                    adpCmd1.Fill(dtNew);
                                    // reader = cmd1.ExecuteReader();
                                    // dtNew.Load(reader);
                                    if (dtNew.Rows.Count > 0)
                                    {
                                        count = 0;
                                        totAmt = 0.00;
                                        totQty = 0.00;
                                        totTax = 0.00;
                                        string tempItemName = dtNew.Rows[mn]["Item_Name"].ToString();
                                        tItemNameGlob = tempItemName;
                                        double tUnitDecimals = double.Parse(dtNew.Rows[mn]["unit_Decimals"].ToString());
                                        string tWeightScale = dtNew.Rows[mn]["WeightScale"].ToString();
                                        double tReadingValue = 0;


                                        DataTable dtItem = new DataTable();
                                        dtItem.Rows.Clear();
                                        SqlCommand cmd12 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                        cmd12.Parameters.AddWithValue("@tItemName", tempItemName);
                                        SqlDataAdapter adp = new SqlDataAdapter(cmd12);
                                        adp.Fill(dtItem);
                                        bool isChkOpenItem = false;
                                        bool isChkStopAtRate = false;
                                        //bool isChkStopAtQty = false;
                                        if (dtItem.Rows.Count > 0)
                                        {
                                            isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());
                                            // isChkStopAtQty = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());

                                            if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                            {
                                                isChkOpenItem = true;
                                            }
                                        }

                                        if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                                        {
                                            if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                                            {
                                            ReadAgain:
                                                try
                                                {
                                                    tReadCount = 0;
                                                    string data = "";
                                                    data = _Class.clsVariables.serial.ReadExisting();
                                                    //serial.Close();
                                                    if (data.IndexOf("kg") > 0)
                                                    {
                                                        data = data.Substring(0, data.IndexOf("kg"));
                                                        data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                        // if
                                                        tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                    }
                                                    else if (data.IndexOf("k") > 0)
                                                    {
                                                        data = data.Substring(0, data.IndexOf("k"));
                                                        data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                        tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    tReadCount++;
                                                    if (tReadCount < 10)
                                                    {
                                                        goto ReadAgain;
                                                    }
                                                    else
                                                    {
                                                        tShowQty = "";
                                                        MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                                        tShowQty = "Show";

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (isChkStopAtRate == true)
                                                {
                                                    tReadingValue = 0;
                                                }
                                                else
                                                {
                                                    tReadingValue = 1;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isChkStopAtRate == true)
                                            {
                                                tReadingValue = 0;
                                            }
                                            else
                                            {
                                                tReadingValue = 1;
                                            }
                                        }

                                        foreach (DataRow dr1 in dt.Rows)
                                        {
                                            if (dr1["itemName"].ToString() == tempItemName)
                                            {
                                                if (isChkOpenItem != true)
                                                {
                                                    count = 1;
                                                    if (tUnitDecimals == 0)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                                    }
                                                    if (tUnitDecimals == 1)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                                    }
                                                    if (tUnitDecimals == 2)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                                    }
                                                    if (tUnitDecimals == 3)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                                    }
                                                    if (tUnitDecimals == 4)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                                    }

                                                    {
                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                    }
                                                    gridItems.Rows[rowIndex].Selected = true;
                                                    rowSelect = "";

                                                    tReadingValueDisplay = tReadingValue;
                                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                    drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                    drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                    drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);

                                                }
                                            }
                                            rowIndex += 1;
                                        }
                                        if (count == 0)
                                        {
                                            dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString();
                                            if (tUnitDecimals == 0)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N0");
                                            }
                                            if (tUnitDecimals == 1)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N1");
                                            }
                                            if (tUnitDecimals == 2)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N2");
                                            }
                                            if (tUnitDecimals == 3)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N3");
                                            }
                                            if (tUnitDecimals == 4)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N4");
                                            }
                                            // dr["Qty"] = "1";
                                            dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                            {
                                                dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                            }
                                            if (dr["Disc"].ToString().Trim() == "")
                                            {
                                                dr["Disc"] = "0.00";
                                            }
                                            dt.Rows.Add(dr);
                                            // funReplaceFreeItemAmt();
                                            tSelectedRowIndex = dt.Rows.Count;
                                            rowSelect = "Last";

                                            tReadingValueDisplay = tReadingValue;
                                            ClickedButtonDisplay = Convert.ToString(tempItemName);
                                            drQtyDisplay = Convert.ToString(dr["Qty"]);
                                            drRateDisplay = Convert.ToString(dr["Rate"]);
                                            drAmtDisplay = Convert.ToString(dr["Amt"]);

                                        }
                                        funStockDisplay(tempItemName);

                                        funDisplayAmount(dt);
                                        if (rowSelect != "")
                                        {
                                            gridItems.DataSource = dt.DefaultView;   // Change gridItems.ItemsSource = dt.DefaultView;
                                            gridItems.Columns[0].Width = 180;
                                            gridItems.Columns[0].ReadOnly = true;
                                            gridItems.Columns[1].Width = 50;
                                            gridItems.Columns[2].Width = 50;
                                            gridItems.Columns[3].Width = 50;
                                            gridItems.Columns[3].ReadOnly = true;
                                            gridItems.RowTemplate.Height = 35;
                                        }
                                        gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                                        funScrollGrid();
                                        funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);


                                        funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                        funRoundCalculate();
                                    }
                                    break;
                                }
                                if (isRecord == 0)
                                {
                                    MyMessageBox.ShowBox("Product Not Found", "Warning");
                                }
                                txtEnterValue.Text = "";
                                txtEnterValue.Focus();
                            }

                        }
                        else
                        {
                            //If '*' Exist executed follow code

                            string tempItemCode = txtEnterValue.Text.Substring(tempFindStar + 1, ((val.Length - 1) - (tempFindStar)));
                            //  MessageBox.Show(txtEnterValue.Text.Substring(tempFindStar+1,((txtEnterValue.Text.Length-1)-(tempFindStar))));
                            string tempQty = txtEnterValue.Text.Substring(0, tempFindStar);
                            //  MessageBox.Show(txtEnterValue.Text.Substring(0, tempFindStar));
                            double num;
                            if (tempQty.Trim() != "" && double.TryParse(tempQty, out num))
                            {
                                if (double.Parse(tempQty) > 0)
                                {
                                    DataTable dtBarcode = new DataTable();
                                    dtBarcode.Rows.Clear();
                                    SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where BarCode=@tBarCode", con);
                                    cmdBarcode.Parameters.AddWithValue("@tBarCode", tempItemCode);

                                    SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                                    adpBarcode.Fill(dtBarcode);

                                    // Check in Barcode or Itemcode
                                    if (dtBarcode.Rows.Count > 0)
                                    {

                                        rowIndex = 0;
                                        // DataRow dr = null;
                                        dr = dt.NewRow();
                                        // MessageBox.Show(ClickedButton.Content.ToString());
                                        SqlCommand cmdItem = new SqlCommand("Select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_no=@tItemNo", con);
                                        cmdItem.Parameters.AddWithValue("@tItemNo", dtBarcode.Rows[0]["Item_no"].ToString());
                                        SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmdItem);
                                        adpCmd1.Fill(dtNew);
                                        //reader = cmd1.ExecuteReader();
                                        //dtNew.Load(reader);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            count = 0;
                                            totAmt = 0.00;
                                            totQty = 0.00;
                                            totTax = 0.00;
                                            string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                            tItemNameGlob = tempItemName;
                                            DataTable dtItem = new DataTable();
                                            dtItem.Rows.Clear();
                                            SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                            cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                            adp.Fill(dtItem);
                                            bool isChkOpenItem = false;
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                {
                                                    isChkOpenItem = true;
                                                }
                                            }
                                            foreach (DataRow dr1 in dt.Rows)
                                            {
                                                if (dr1["itemName"].ToString() == tempItemName)
                                                {
                                                    if (isChkOpenItem != true)
                                                    {
                                                        count = 1;
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                        {
                                                            dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                        }

                                                        tReadingValueDisplay = 1;
                                                        ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                        drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                        drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                        drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                    }
                                                }
                                                rowIndex += 1;


                                            }
                                            if (count == 0)
                                            {
                                                dr["ItemName"] = dtNew.Rows[0]["Item_name"].ToString();
                                                dr["Qty"] = tempQty.ToString();
                                                dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString()));


                                                {
                                                    dr["Amt"] = string.Format("{0:0.00}", (double.Parse(tempQty) * double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString())));
                                                }
                                                dt.Rows.Add(dr);
                                                // funReplaceFreeItemAmt();
                                                tSelectedRowIndex = dt.Rows.Count;

                                                tReadingValueDisplay = 1;
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dr["Qty"]);
                                                drRateDisplay = Convert.ToString(dr["Rate"]);
                                                drAmtDisplay = Convert.ToString(dr["Amt"]);


                                            }
                                            funStockDisplay(tempItemName);
                                            funDisplayAmount(dt);
                                            gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                            gridItems.Columns[0].Width = 180;
                                            gridItems.Columns[0].ReadOnly = true;
                                            gridItems.Columns[1].Width = 50;
                                            gridItems.Columns[2].Width = 50;
                                            gridItems.Columns[3].Width = 50;
                                            gridItems.Columns[3].ReadOnly = true;
                                            gridItems.RowTemplate.Height = 35;
                                            funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                            funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                            funScrollGrid();
                                            funRoundCalculate();
                                        }
                                        else
                                        {
                                            MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                        }
                                    }
                                    else
                                    {
                                        rowIndex = 0;
                                        // DataRow dr = null;
                                        dr = dt.NewRow();
                                        // MessageBox.Show(ClickedButton.Content.ToString());
                                        SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@tValue", tempItemCode);
                                        cmd1.Parameters.AddWithValue("@tActionType", "ITEMCODE");
                                        SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                        adpCmd1.Fill(dtNew);
                                        //reader = cmd1.ExecuteReader();
                                        //dtNew.Load(reader);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            count = 0;
                                            totAmt = 0.00;
                                            totQty = 0.00;
                                            totTax = 0.00;
                                            string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                            tItemNameGlob = tempItemName;
                                            DataTable dtItem = new DataTable();
                                            dtItem.Rows.Clear();
                                            SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                            cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                            adp.Fill(dtItem);
                                            bool isChkOpenItem = false;
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                {
                                                    isChkOpenItem = true;
                                                }
                                            }
                                            foreach (DataRow dr1 in dt.Rows)
                                            {
                                                if (dr1["itemName"].ToString() == tempItemName)
                                                {
                                                    if (isChkOpenItem != true)
                                                    {
                                                        count = 1;
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                        {
                                                            dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                        }

                                                        tReadingValueDisplay = 1;
                                                        ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                        drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                        drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                        drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                    }
                                                }
                                                rowIndex += 1;


                                            }
                                            if (count == 0)
                                            {
                                                dr["ItemName"] = dtNew.Rows[0]["Item_name"].ToString();
                                                dr["Qty"] = tempQty.ToString();
                                                dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString()));


                                                {
                                                    dr["Amt"] = string.Format("{0:0.00}", (double.Parse(tempQty) * double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString())));
                                                }
                                                dt.Rows.Add(dr);
                                                // funReplaceFreeItemAmt();
                                                tSelectedRowIndex = dt.Rows.Count;

                                                tReadingValueDisplay = 1;
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dr["Qty"]);
                                                drRateDisplay = Convert.ToString(dr["Rate"]);
                                                drAmtDisplay = Convert.ToString(dr["Amt"]);




                                            }
                                            funStockDisplay(tempItemName);
                                            funDisplayAmount(dt);
                                            gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                            gridItems.Columns[0].Width = 180;
                                            gridItems.Columns[0].ReadOnly = true;
                                            gridItems.Columns[1].Width = 50;
                                            gridItems.Columns[2].Width = 50;
                                            gridItems.Columns[3].Width = 50;
                                            gridItems.Columns[3].ReadOnly = true;
                                            gridItems.RowTemplate.Height = 35;
                                            funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                            funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                            funScrollGrid();
                                            funRoundCalculate();
                                        }
                                        else
                                        {
                                            MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                        }

                                    }
                                    ////}
                                    ////else
                                    ////{
                                    ////    MyMessageBox.ShowBox("Please Enter Valid Quantity","Warning");
                                    ////}
                                }
                                else if (double.Parse(tempQty) < 0)
                                {
                                    // if item Qty in minus sign execute this block
                                    _Class.clsVariables.funControlSetting();
                                    if (_Class.clsVariables.tSetReturnInSales == false)
                                    {
                                        MyMessageBox.ShowBox("Please Enter Valid Quantity", "Warning");
                                    }
                                    else
                                    {
                                        DataTable dtBarcode = new DataTable();
                                        dtBarcode.Rows.Clear();
                                        SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where BarCode=@tBarCode", con);
                                        cmdBarcode.Parameters.AddWithValue("@tBarCode", tempItemCode);

                                        SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                                        adpBarcode.Fill(dtBarcode);
                                        if (dtBarcode.Rows.Count > 0)
                                        {
                                            rowIndex = 0;
                                            // DataRow dr = null;
                                            dr = dt.NewRow();

                                            SqlCommand cmdItem = new SqlCommand("Select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_no=@tItemNo", con);
                                            cmdItem.Parameters.AddWithValue("@tItemNo", dtBarcode.Rows[0]["Item_no"].ToString());
                                            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmdItem);


                                            adpCmd1.Fill(dtNew);
                                            //reader = cmd1.ExecuteReader();
                                            //dtNew.Load(reader);
                                            if (dtNew.Rows.Count > 0)
                                            {
                                                count = 0;
                                                totAmt = 0.00;
                                                totQty = 0.00;
                                                totTax = 0.00;
                                                string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                                tItemNameGlob = tempItemName;
                                                DataTable dtItem = new DataTable();
                                                dtItem.Rows.Clear();
                                                SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                                cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                                adp.Fill(dtItem);
                                                bool isChkOpenItem = false;
                                                if (dtItem.Rows.Count > 0)
                                                {
                                                    if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                    {
                                                        isChkOpenItem = true;
                                                    }
                                                }
                                                bool isMsgDis = true;
                                                foreach (DataRow dr1 in dt.Rows)
                                                {
                                                    if (dr1["itemName"].ToString() == tempItemName)
                                                    {
                                                        string tItemName = Convert.ToString(tempItemName);
                                                        tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");

                                                        DataRow[] dtRemoveChk = _Class.clsVariables.dtSingleFree.Select("MainItemName='" + tItemName + "'");
                                                        if (dtRemoveChk.Length == 0)
                                                        {
                                                            if (isChkOpenItem != true)
                                                            {
                                                                count = 1;
                                                                if ((double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())) > 0)
                                                                {

                                                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                                    {
                                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                                    }

                                                                    tReadingValueDisplay = 1;
                                                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                                    drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                                    drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                                    drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                                }
                                                                else
                                                                {
                                                                    MyMessageBox.ShowBox("Enter Valid Quantity", "Warning");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            isMsgDis = true;
                                                            MyMessageBox.ShowBox("This Item Could not be Remove from the list");
                                                        }

                                                    }
                                                    rowIndex += 1;


                                                }
                                                if (count == 0 && isMsgDis == false)
                                                {
                                                    MyMessageBox.ShowBox("Item not found in the list", "Warning");
                                                }

                                                funStockDisplay(tempItemName);
                                                funDisplayAmount(dt);
                                                gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                                gridItems.Columns[0].Width = 180;
                                                gridItems.Columns[0].ReadOnly = true;
                                                gridItems.Columns[1].Width = 50;
                                                gridItems.Columns[2].Width = 50;
                                                gridItems.Columns[3].Width = 50;
                                                gridItems.Columns[3].ReadOnly = true;
                                                gridItems.RowTemplate.Height = 35;
                                                funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                                funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                                funScrollGrid();
                                                funRoundCalculate();
                                            }
                                            else
                                            {
                                                MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                            }
                                        }
                                        else
                                        {
                                            rowIndex = 0;
                                            // DataRow dr = null;
                                            dr = dt.NewRow();
                                            // MessageBox.Show(ClickedButton.Content.ToString());
                                            SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                            cmd1.CommandType = CommandType.StoredProcedure;
                                            cmd1.Parameters.AddWithValue("@tValue", tempItemCode);
                                            cmd1.Parameters.AddWithValue("@tActionType", "ITEMCODE");
                                            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                            adpCmd1.Fill(dtNew);
                                            //reader = cmd1.ExecuteReader();
                                            //dtNew.Load(reader);
                                            if (dtNew.Rows.Count > 0)
                                            {
                                                count = 0;
                                                totAmt = 0.00;
                                                totQty = 0.00;
                                                totTax = 0.00;
                                                string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                                tItemNameGlob = tempItemName;
                                                DataTable dtItem = new DataTable();
                                                dtItem.Rows.Clear();
                                                SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                                cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                                adp.Fill(dtItem);
                                                bool isChkOpenItem = false;
                                                if (dtItem.Rows.Count > 0)
                                                {
                                                    if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                    {
                                                        isChkOpenItem = true;
                                                    }
                                                }
                                                bool isMsgDis = false;
                                                foreach (DataRow dr1 in dt.Rows)
                                                {
                                                    if (dr1["itemName"].ToString() == tempItemName)
                                                    {
                                                        string tItemName = Convert.ToString(tempItemName);
                                                        tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");
                                                        DataRow[] dtRemoveChk = _Class.clsVariables.dtSingleFree.Select("MainItemName='" + tItemName + "'");
                                                        if (dtRemoveChk.Length == 0)
                                                        {
                                                            if (isChkOpenItem != true)
                                                            {
                                                                count = 1;
                                                                if ((double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())) > 0)
                                                                {

                                                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                                    {
                                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                                    }

                                                                    tReadingValueDisplay = 1;
                                                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                                    drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                                    drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                                    drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                                }
                                                                else
                                                                {
                                                                    MyMessageBox.ShowBox("Enter Valid Quantity", "Warning");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            isMsgDis = true;
                                                            MyMessageBox.ShowBox("This Item Could not be Remove from the list");

                                                        }
                                                    }
                                                    rowIndex += 1;


                                                }
                                                if (count == 0 && isMsgDis == false)
                                                {
                                                    MyMessageBox.ShowBox("Item not found in the list", "Warning");
                                                }

                                                funStockDisplay(tempItemName);
                                                funDisplayAmount(dt);
                                                gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                                gridItems.Columns[0].Width = 180;
                                                gridItems.Columns[0].ReadOnly = true;
                                                gridItems.Columns[1].Width = 50;
                                                gridItems.Columns[2].Width = 50;
                                                gridItems.Columns[3].Width = 50;
                                                gridItems.Columns[3].ReadOnly = true;
                                                gridItems.RowTemplate.Height = 35;
                                                funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                                funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                                funScrollGrid();
                                                funRoundCalculate();
                                            }
                                            else
                                            {
                                                MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Enter valid Quantity", "Warning");
                            }
                            txtEnterValue.Text = "";
                            txtEnterValue.Focus();
                        }
                    }
                    txtEnterValue.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void newBtnGroupItem_Click(object sender, RoutedEventArgs e)
        {
            //Clicked Item Event Here
            if (pnlNumeric.Visibility == Visibility.Hidden)
            {
                pnlNumeric.Visibility = Visibility.Hidden;
            }
            else
            {
                if (_Class.clsVariables.tHideKeyboard == true)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    pnlNumeric.Visibility = Visibility.Visible;
                }
            }
            var bc = new BrushConverter();
            lblLogo.Foreground = (Brush)bc.ConvertFrom("#FFADF213");

            //Timer start for Customer Display
            tempTimer.Interval = 1000;
            tempTimer.Enabled = false;
            tempTimer.Tick += new EventHandler(timer1_Tick);
            tTimerCount = 0;
            try
            {
                rowSelect = "";
                bool isChkStopAtRate = false;
                if (gridItems.DataSource == null)  // Change if (gridItems.ItemsSource == null)
                {
                    dt.Rows.Clear();
                }
                rowIndex = 0;
                Button ClickedButton = (Button)sender;
                DataRow dr = null;
                dr = dt.NewRow();
                string iname = ClickedButton.Content.ToString();
                SqlCommand cmd12 = new SqlCommand("select * from serialno_transtbl where barcodeno = (select distinct item_code from item_table where item_name like '" + iname + "')", con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd12);
                DataTable dtn = new DataTable();
                adap.Fill(dtn);
                if (dtn.Rows.Count != 0)
                {
                    pnlGroupItem.Visibility = Visibility.Hidden;
                    pnlGroupItem1.Visibility = Visibility.Visible;
                    pnlGroupItem1.Width = 500;
                    pnlGroupItem1.Height = 620;
                    listSelect.FontSize = 25;
                    listSelect.Width = 495;
                    listSelect.Height = 640;
                    Item_Load(iname);
                }
                else
                {

                    //Below method helps,When user click Item button at the time Closing stock displaying on the top of Sales screen
                    funStockDisplay(ClickedButton.Content.ToString());

                    tItemNameGlob = ClickedButton.Content.ToString();

                    UCCustomerList1.SalesCreationEventHandlerNewCustomerName += new EventHandler(EventCustomerList);

                    double tUnitDecimals = 0.00;
                    string tWeightScale = "";
                    SqlParameter result;
                    SqlParameter resultUnit;
                    SqlParameter resultWeightScale;
                    if (strCus == "" || strCus == null)
                    {

                        //Below procedure helps to getting Clicked item Qty No. of Unit Digit (Ex:1.000), Wightscale control values 
                        SqlCommand cmd1 = new SqlCommand("sp_SalesCreationNewBtnGroupItem", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@tItemName", ClickedButton.Content.ToString());
                        // SqlParameter result = new SqlParameter("@tResult", SqlDbType.Float);
                        result = new SqlParameter("@tResult", SqlDbType.Float);
                        result.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(result);

                        // SqlParameter resultUnit = new SqlParameter("@tUnitDigit", SqlDbType.Float);
                        resultUnit = new SqlParameter("@tUnitDigit", SqlDbType.Float);
                        resultUnit.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(resultUnit);

                        //SqlParameter resultWeightScale = new SqlParameter("@tWeightScale", SqlDbType.Float);
                        resultWeightScale = new SqlParameter("@tWeightScale", SqlDbType.Float);
                        resultWeightScale.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(resultWeightScale);
                        cmd1.ExecuteNonQuery();

                        //double tUnitDecimals = double.Parse(resultUnit.Value.ToString());
                        //string tWeightScale = resultWeightScale.Value.ToString();

                        tUnitDecimals = double.Parse(resultUnit.Value.ToString());
                        tWeightScale = resultWeightScale.Value.ToString();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("sp_SalesCreationNewBtnGroupItemCustomer", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@tItemName", ClickedButton.Content.ToString());
                        cmd1.Parameters.AddWithValue("@ledger_no", strCus);
                        // SqlParameter result = new SqlParameter("@tResult", SqlDbType.Float);
                        result = new SqlParameter("@tResult", SqlDbType.Float);
                        result.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(result);

                        // SqlParameter resultUnit = new SqlParameter("@tUnitDigit", SqlDbType.Float);
                        resultUnit = new SqlParameter("@tUnitDigit", SqlDbType.Float);
                        resultUnit.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(resultUnit);

                        //SqlParameter resultWeightScale = new SqlParameter("@tWeightScale", SqlDbType.Float);
                        resultWeightScale = new SqlParameter("@tWeightScale", SqlDbType.Float);
                        resultWeightScale.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(resultWeightScale);
                        cmd1.ExecuteNonQuery();

                        //double tUnitDecimals = double.Parse(resultUnit.Value.ToString());
                        //string tWeightScale = resultWeightScale.Value.ToString();

                        tUnitDecimals = double.Parse(resultUnit.Value.ToString());
                        tWeightScale = resultWeightScale.Value.ToString();
                        _Class.clsVariables.tempCustomerLedgerNo = "";
                        strCus = "";
                    }
                    count = 0;
                    totAmt = 0.00;
                    totQty = 0.00;
                    totTax = 0.00;
                    double tReadingValue = 0;


                    //This below statement helps to find clicked item open item or not
                    DataTable dtItem = new DataTable();
                    dtItem.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                    cmd.Parameters.AddWithValue("@tItemName", ClickedButton.Content.ToString());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtItem);
                    bool isChkOpenItem = false;
                    if (dtItem.Rows.Count > 0)
                    {
                        isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());
                        if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                        {
                            isChkOpenItem = true;
                        }
                    }

                    //Weight Scale Code Start Here
                    // If the Weight scale control is enable, Qty values collecting from weight mechine
                    if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                    {
                        if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                        {
                            tReadCount = 0;
                        ReadAgain:
                            try
                            {
                                string data = "";

                                //This below line helps to reading value from weight mechine. If the weight scale not return anything raise exception
                                data = _Class.clsVariables.serial.ReadExisting();

                                // Our Weight Scale return Alpha numeric values. below code helps to split Weight values and alpha character
                                if (data.IndexOf("kg") > 0)
                                {
                                    data = data.Substring(0, data.IndexOf("kg"));
                                    data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                    // if
                                    tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));
                                }
                                else if (data.IndexOf("k") > 0)
                                {
                                    data = data.Substring(0, data.IndexOf("k"));
                                    data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                    tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));
                                }

                            }
                            catch (Exception)
                            {
                                //this catch block try to read values from weight scale,if value got loop will be exit or try 10 attempts 
                                tReadCount++;
                                if (tReadCount < 10)
                                {
                                    goto ReadAgain;
                                }
                                else
                                {
                                    tShowQty = "";
                                    MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                    tShowQty = "Show";
                                }
                            }
                        }
                        else
                        {
                            if (isChkStopAtRate == true)
                            {
                                tReadingValue = 0;
                            }
                            else
                            {
                                tReadingValue = 1;
                            }
                        }
                    }
                    else
                    {
                        if (isChkStopAtRate == true)
                        {
                            tReadingValue = 0;
                        }
                        else
                        {
                            tReadingValue = 1;
                        }
                    }
                    //Weight Scale Code End Here

                    //Below for loop helps to find clicked item already exist in list or Not
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if (dr1["itemName"].ToString() == ClickedButton.Content.ToString())
                        {
                            if (isChkOpenItem != true)
                            {
                                count = 1;

                                if (tUnitDecimals == 0)
                                {
                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                }
                                if (tUnitDecimals == 1)
                                {
                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                }
                                if (tUnitDecimals == 2)
                                {
                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                }
                                if (tUnitDecimals == 3)
                                {
                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                }
                                if (tUnitDecimals == 4)
                                {
                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                }


                                {
                                    dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", double.Parse(((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())).ToString()));
                                }

                                gridItems.Rows[rowIndex].Selected = true;

                                rowSelect = "";
                                tRowActionType = "ADD";

                                tReadingValueDisplay = tReadingValue;
                                ClickedButtonDisplay = Convert.ToString(ClickedButton.Content);
                                drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                drRateDisplay = Convert.ToString(dr1["Rate"]);
                                drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                            }
                            else
                            {

                            }
                        }
                        rowIndex += 1;
                        tSelectedRowIndex = rowIndex;
                        // totQty += double.Parse(dr1["Qty"].ToString());
                        // totAmt += double.Parse(dr1["Amt"].ToString());
                    }

                    // If clicked item not exist in list below to helps to create to row in grid
                    if (count == 0)
                    {
                        dr["ItemName"] = ClickedButton.Content.ToString();
                        string tOriginalItemName = Convert.ToString(ClickedButton.Content);
                        string tCorrectValue = "0";
                        if (tUnitDecimals == 0)
                        {
                            dr["Qty"] = tReadingValue.ToString("N0");
                            tCorrectValue = tReadingValue.ToString("N0");
                        }
                        if (tUnitDecimals == 1)
                        {
                            dr["Qty"] = tReadingValue.ToString("N1");
                            tCorrectValue = tReadingValue.ToString("N1");
                        }
                        if (tUnitDecimals == 2)
                        {
                            dr["Qty"] = tReadingValue.ToString("N2");
                            tCorrectValue = tReadingValue.ToString("N2");
                        }
                        if (tUnitDecimals == 3)
                        {
                            dr["Qty"] = tReadingValue.ToString("N3");
                            tCorrectValue = tReadingValue.ToString("N3");
                        }
                        if (tUnitDecimals == 4)
                        {
                            dr["Qty"] = tReadingValue.ToString("N4");
                            tCorrectValue = tReadingValue.ToString("N4");
                        }
                        //dr["Qty"] = tReadingValue;

                        dr["Rate"] = string.Format("{0:0.00}", double.Parse(result.Value.ToString()));

                        {

                            dr["Amt"] = string.Format("{0:0.00}", double.Parse(result.Value.ToString()) * double.Parse(tCorrectValue));
                        }

                        if (dr["Disc"].ToString().Trim() == "")
                        {
                            dr["Disc"] = "0.00";
                        }
                        dt.Rows.Add(dr);
                        //  funReplaceFreeItemAmt();
                        tSelectedRowIndex = dt.Rows.Count;
                        rowSelect = "Last";
                        tRowActionType = "DONTADD";

                        tReadingValueDisplay = tReadingValue;
                        ClickedButtonDisplay = Convert.ToString(ClickedButton.Content);
                        drQtyDisplay = Convert.ToString(dr["Qty"]);
                        drRateDisplay = Convert.ToString(dr["Rate"]);
                        drAmtDisplay = Convert.ToString(dr["Amt"]);
                    }
                    // After Adding or Updating row, below method helps to focus currently modified or Inserted rows
                    funScrollGrid();

                    // Below method helps to Calculate Discount, Tax , Gross Amt, Net Amount and Free Item Values
                    funDisplayAmount(dt);

                    gridItems.DataSource = dt.DefaultView;   //Change  gridItems.ItemsSource = dt.DefaultView;
                    gridItems.Columns[0].Width = 180;
                    gridItems.Columns[0].ReadOnly = true;
                    gridItems.Columns[1].Width = 50;
                    gridItems.Columns[2].Width = 50;
                    gridItems.Columns[3].Width = 50;
                    gridItems.Columns[3].ReadOnly = true;
                    gridItems.RowTemplate.Height = 35;
                    if (rowSelect == "Last")
                    {
                        gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                        rowSelect = "";
                    }
                    //below method Helps to Calculate round values 
                    //5Cent -If Unit digit like 8,9,0,1,2 Replace 0    Ex:12.51=>12.50
                    //5Cent -If Unit digit like 3,4,5,6,7 Replace 5    Ex:12.53=>12.55

                    //10Cent-If Unit digit like 8,9,0,1,2 Replace 0    Ex:12.59=>13.00
                    //10Cent-If Unit digit like 3,4,5,6,7 Replace 0    Ex:12.59=>12.00
                    funRoundCalculate();

                    //Below Method Helps to display Stop at Rate or Qty Screen 
                    funStopAtQtyAndRate(ClickedButton.Content.ToString(), tSelectedRowIndex);

                    //Customer Display Method
                    funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);

                    txtEnterValue.Focus();

                    _Class.clsVariables.tempCustomerLedgerNo = "";
                    strCus = "";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        double tReadingValueDisplay = 0;
        string ClickedButtonDisplay = "", drQtyDisplay = "", drRateDisplay = "", drAmtDisplay = "";

        public void funCustomerDisplay(double tReadingValueDisplay, string ClickedButtonDisplay, string drQtyDisplay, string drRateDisplay, string drAmtDisplay)
        {
            try
            {
                // After enable Customer Display  in Preprial Device form, below code will be execute
                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {
                    if (tReadingValueDisplay > 0)
                    {
                        // Timer start for next msg Display
                        tempTimer.Start();

                        byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                        _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                        _Class.clsVariables.spCustomerDis.WriteLine((ClickedButtonDisplay.ToString().Length > 20) ? ClickedButtonDisplay.ToString().Substring(0, 19) : ClickedButtonDisplay.ToString());
                        byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                        _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                        _Class.clsVariables.spCustomerDis.Write(drQtyDisplay + "*" + drRateDisplay + "=" + drAmtDisplay);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        public void funSingleFreeMerge()
        {
            // No need this Method. In future may use
            try
            {
                Dictionary<string, double> dicSum1 = new Dictionary<string, double>();
                foreach (DataRow row in _Class.clsVariables.dtSingleFree.Rows)
                {
                    string group = row["ItemName"].ToString();
                    double Qty = (string.IsNullOrEmpty(Convert.ToString(row["Qty"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Qty"]));
                    if (dicSum1.ContainsKey(group))
                        dicSum1[group] += Qty
;
                    else
                        dicSum1.Add(group, Qty);
                }
                tempdtSingleFree.Rows.Clear();
                foreach (string g in dicSum1.Keys)
                {
                    tempdtSingleFree.Rows.Add(g, dicSum1[g]);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public void funDisplayAmount(DataTable dt)
        {
            try
            {
                double @tTotQty = 0;
                double tTotalChkAmt = 0;
                //below loop helps to assign Default values for Special Disc and Other Discount Column
                for (int tAssignSDisc = 0; tAssignSDisc < dt.Rows.Count; tAssignSDisc++)
                {
                    dt.Rows[tAssignSDisc]["SDisc"] = "0.00";
                    dt.Rows[tAssignSDisc]["Other"] = "0.00";
                    @tTotQty = @tTotQty + (string.IsNullOrEmpty(Convert.ToString(dt.Rows[tAssignSDisc][1])) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[tAssignSDisc][1])));
                    tTotalChkAmt = tTotalChkAmt + (string.IsNullOrEmpty(Convert.ToString(dt.Rows[tAssignSDisc][3])) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[tAssignSDisc][3])));
                }

                //If the user enable allow free Qty option from Backoffice Control Setting form, below coding will be Execute
                if (_Class.clsVariables.tAllowOffer == true)
                {
                    funFreeItem();
                }
                lblGroupDiscAmt.Content = "0.00";
                DataRow[] dtItemGroupRow;
                string tItemName = "";
                double tDiscount = 0, tItemAmt = 0, tGroupDiscPercent = 0, tTotGroupDisc = 0, tSpecialDisc = 0, tOverAllDisc = 0;
                tSpecialDisc = (string.IsNullOrEmpty(Convert.ToString(lblSpecialDiscAmt.Content))) ? 0.00 : Convert.ToDouble(Convert.ToString(lblSpecialDiscAmt.Content));
                tOverAllDisc = (string.IsNullOrEmpty(Convert.ToString(lblOverAllDiscAmt.Content))) ? 0.00 : Convert.ToDouble(Convert.ToString(lblOverAllDiscAmt.Content));

                //If User enable Group discount from BackOffice Control setting Form below coding will be execute
                if (_Class.clsVariables.tMainDiscountType == "Group")
                {
                    //Calculate Groupwise discount for each items and Stored in DISC Column     
                    for (int mn = 0; mn < dt.Rows.Count; mn++)
                    {
                        tDiscount = 0;
                        tItemAmt = 0;
                        tGroupDiscPercent = 0;
                        tItemAmt = (string.IsNullOrEmpty(Convert.ToString(dt.Rows[mn]["Amt"]))) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[mn]["Amt"]));
                        string iname = dt.Rows[mn]["ItemName"].ToString();
                        int ni = iname.IndexOf("-");
                        if (ni != -1)
                            iname = iname.Substring(0, ni);
                        tItemName = Convert.ToString(iname);
                        tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");

                        dtItemGroupRow = _Class.clsVariables.dtItemGroup.Select("Item_name='" + tItemName + "'");
                        for (int k = 0; k < dtItemGroupRow.Length; k++)
                        {
                            tGroupDiscPercent = (string.IsNullOrEmpty(Convert.ToString(dtItemGroupRow[k]["DisPerAmt"]))) ? 0 : Convert.ToDouble(Convert.ToString(dtItemGroupRow[k]["DisPerAmt"]));
                            tDiscount = tItemAmt * (tGroupDiscPercent / 100);
                        }
                        tTotGroupDisc = tTotGroupDisc + tDiscount;
                        dt.Rows[mn]["Disc"] = string.Format("{0:0.00}", tDiscount);
                    }
                    lblGroupDiscAmt.Content = string.Format("{0:0.00}", tTotGroupDisc);
                }
                else
                {
                    tTotGroupDisc = 0;
                    for (int mn = 0; mn < dt.Rows.Count; mn++)
                    {
                        tTotGroupDisc += string.IsNullOrEmpty(Convert.ToString(dt.Rows[mn]["Disc"])) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[mn]["Disc"]));
                    }
                    lblGroupDiscAmt.Content = string.Format("{0:0.00}", tTotGroupDisc);
                }

                double @tNetAmt = 0;
                double @tTotAmt = 0;

                double @tTotTax = 0;
                double @tOtherDisc = 0;
                double tLblNetAmt = 0;
                tLblNetAmt = Convert.ToDouble(tTotalChkAmt - (Convert.ToDouble(Convert.ToString(lblSpecialDiscAmt.Content)) + Convert.ToDouble(Convert.ToString(lblGroupDiscAmt.Content))));
                if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                {
                    tLblNetAmt = tLblNetAmt + Convert.ToDouble(Convert.ToString(lblTaxAmt.Content));
                }
                double tOtherDiscPercent = 0;
                //Split other discount amt to All Item
                if (tOverAllDisc > 0)
                {
                    tOtherDiscPercent = tOverAllDisc / tLblNetAmt;
                    @tOtherDisc = tOverAllDisc / @tTotQty;
                }
                double @Qty = 0;
                double @tTotDiscAmt = 0;
                double @tDisc = 0, @tSDisc = 0, @tODisc = 0;
                double @Amt = 0, @tTax = 0;
                @tTotQty = 0;
                tOverAllDisc = 0;

                // Below loop calculate Tax, Total Qty, Disc, Gross Amt and Net Amount
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    @Qty = 0;
                    @Amt = (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][3])) == true) ? 0 : Convert.ToDouble(dt.Rows[i][3].ToString());
                    @tTax = 0;
                    if (dt.Rows[i][1].ToString() != "")
                    {
                        @Qty = Convert.ToDouble(dt.Rows[i][1].ToString());
                    }
                    //dt.Rows[i]["Other"] = string.Format("{0:0.00}",(@Qty*@tOtherDisc));

                    if (dt.Rows[i]["Disc"].ToString() != "")
                    {
                        @tDisc = Convert.ToDouble(dt.Rows[i]["Disc"].ToString());
                    }
                    if (dt.Rows[i]["SDisc"].ToString() != "")
                    {
                        @tSDisc = Convert.ToDouble(dt.Rows[i]["SDisc"].ToString());
                    }
                    dt.Rows[i]["Other"] = string.Format("{0:0.00}", ((@Amt - (@tDisc + @tSDisc)) * tOtherDiscPercent));
                    if (dt.Rows[i]["Other"].ToString() != "")
                    {
                        @tODisc = Convert.ToDouble(dt.Rows[i]["Other"].ToString());
                        tOverAllDisc = tOverAllDisc + @tODisc;
                    }
                    @tTotDiscAmt = @tTotDiscAmt + @tDisc + @tODisc + @tSDisc;
                    @tTotQty = @tTotQty + @Qty;
                    @tTotAmt = @tTotAmt + @Amt;
                    DataTable stNew = new DataTable();
                    stNew.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("Select Nt_percent from Tax_Table where Tax_no=(Select Tax_no from item_table where Item_Active=1 and Item_name=@ItemName)", con);
                    string iname = dt.Rows[i][0].ToString();
                    int ni = iname.IndexOf("-");
                    if (ni != -1)
                        iname = iname.Substring(0, ni);
                    cmd.Parameters.AddWithValue("@ItemName", iname);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(stNew);
                    if (stNew.Rows.Count > 0)
                    {
                        // Below Setings Enable from Back Office Receipt form
                        if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                        {
                            @tTax = (@Amt - (@tDisc + @tSDisc + @tODisc)) - (((@Amt - (@tDisc + @tSDisc + @tODisc)) * 100) / (100 + Convert.ToDouble(stNew.Rows[0][0].ToString())));
                        }
                        else if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                        {
                            @tTax = ((@Amt - (@tDisc + @tSDisc + @tODisc)) * Convert.ToDouble(stNew.Rows[0][0].ToString())) / 100;
                        }
                        @tTotTax = @tTotTax + @tTax;
                    }
                }

                if (_Class.clsVariables.tMainDiscountType == "Individual" || _Class.clsVariables.tMainDiscountType == "Group")
                {
                    lblGroupDiscAmt.Content = String.Format("{0:0.00}", tTotGroupDisc);
                }
                // if (_Class.clsVariables.tDiscountLedger == "1")
                {
                    lblOverAllDiscAmt.Content = String.Format("{0:0.00}", tOverAllDisc);
                    lblDiscount.Content = String.Format("{0:0.00}", @tTotDiscAmt);
                }
                if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive" || _Class.clsVariables.tempGDisplayTaxType == "NoTax")
                {
                    @tNetAmt = (@tTotAmt) - Convert.ToDouble(lblDiscount.Content.ToString());
                }
                else
                {
                    @tNetAmt = (@tTotTax + @tTotAmt) - Convert.ToDouble(lblDiscount.Content.ToString());
                }
                if (_Class.clsVariables.tempGDisplayTaxType == "NoTax")
                {
                    lblTaxAmt.Content = "0.00";
                }
                else
                {
                    lblTaxAmt.Content = String.Format("{0:0.00}", @tTotTax);
                }
                lblTotQty.Content = @tTotQty.ToString();
                lblTotAmt.Content = String.Format("{0:0.00}", @tTotAmt);
                lblNetAmt.Content = String.Format("{0:0.00}", @tNetAmt);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        public void funStopAtQtyAndRate(string tItemName, int SelectedRowIndex)
        {
            try
            {
                //Stop At Qty and Rate Process Start
                funConnectionStateCheck();
                SqlCommand adpStopAtChk = new SqlCommand("sp_SalesCreation_StopAtQtyAndRate", con);
                adpStopAtChk.CommandType = CommandType.StoredProcedure;
                adpStopAtChk.Parameters.AddWithValue("@tItemName", tItemName);
                SqlParameter resultStopAtQty = new SqlParameter("@tStopAtQty", SqlDbType.VarChar, 100);
                resultStopAtQty.Direction = ParameterDirection.Output;
                adpStopAtChk.Parameters.Add(resultStopAtQty);
                SqlParameter resultStopAtRate = new SqlParameter("@tStopAtRate", SqlDbType.VarChar, 100);
                resultStopAtRate.Direction = ParameterDirection.Output;
                adpStopAtChk.Parameters.Add(resultStopAtRate);
                adpStopAtChk.ExecuteNonQuery();
                if (tShowQty != "Show")
                {
                    tStopAtQty = resultStopAtQty.Value.ToString();
                    tStopAtRate = resultStopAtRate.Value.ToString();
                }
                else
                {
                    tStopAtQty = "True";
                    tStopAtRate = "True";
                }
                bool isChk = false;
                if (tStopAtQty == "True" || tStopAtQty == "1")
                {
                    for (int m = 0; m < gridItems.Rows.Count; m++)
                    {
                        if (gridItems.Rows[m].Cells[0].Value.ToString().Trim().ToUpper() == tItemName.Trim().ToUpper())
                        {
                            gridItems.Rows[m].Cells[1].ReadOnly = false;
                            isChk = true;
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < gridItems.Rows.Count; m++)
                    {
                        if (gridItems.Rows[m].Cells[0].Value.ToString().Trim().ToUpper() == tItemName.Trim().ToUpper())
                        {
                            gridItems.Rows[m].Cells[1].ReadOnly = true;
                        }
                    }
                }

                if (tStopAtRate == "True" || tStopAtRate == "1")
                {
                    for (int m = 0; m < gridItems.Rows.Count; m++)
                    {
                        if (gridItems.Rows[m].Cells[0].Value.ToString().Trim().ToUpper() == tItemName.Trim().ToUpper())
                        {
                            gridItems.Rows[m].Cells[2].ReadOnly = false;
                            isChk = true;
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < gridItems.Rows.Count; m++)
                    {
                        if (gridItems.Rows[m].Cells[0].Value.ToString().Trim().ToUpper() == tItemName.Trim().ToUpper())
                        {
                            gridItems.Rows[m].Cells[2].ReadOnly = true;
                        }
                    }
                }

                if (isChk != false)
                {
                    SalesProject._ExtraForm.frmQtyaNRate frm = new _ExtraForm.frmQtyaNRate();
                    if (dt.Rows.Count > 0)
                    {
                        //  SalesProject._Class.clsVariables.itemIndex = e.RowIndex;
                        SalesProject._Class.clsVariables.itemName = tItemName;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (tItemName == dt.Rows[i][0].ToString())
                            {
                                //  if (tRowActionType == "ADD")
                                // {
                                SalesProject._Class.clsVariables.itemQty = dt.Rows[i]["Qty"].ToString();
                                SalesProject._Class.clsVariables.itemRate = dt.Rows[i]["Rate"].ToString();
                                SalesProject._Class.clsVariables.itemAmt = dt.Rows[i]["Amt"].ToString();
                                //  }

                            }
                        }
                        // DataSet dsTemp = new DataSet();
                        // SqlDataAdapter adpStopAtChk = new SqlDataAdapter("Select * from Item_table where Item_name='" + gridItems.Rows[e.RowIndex].Cells["ItemName"].Value.ToString() + "'", con);

                        // adpStopAtChk.Fill(dsTemp, "STOPAT");
                        // if (dsTemp.Tables["STOPAT"].Rows.Count > 0)
                        // {
                        //    tStopAtQty = dsTemp.Tables["STOPAT"].Rows[0]["StopAtQty"].ToString();
                        //   tStopAtRate = dsTemp.Tables["STOPAT"].Rows[0]["StopAtRate"].ToString();

                        if (tStopAtRate == "True" || tStopAtRate == "1")
                        {
                            _Class.clsVariables.StopAtRate = tStopAtRate;
                            frm.getValueType = "Rate";
                        }
                        else
                        {
                            _Class.clsVariables.StopAtRate = tStopAtRate;
                        }
                        if (tStopAtQty == "True" || tStopAtQty == "1")
                        {
                            _Class.clsVariables.StopAtQty = tStopAtQty;
                            frm.getValueType = "Qty";
                        }
                        else
                        {
                            _Class.clsVariables.StopAtQty = tStopAtQty;
                            //frm.getValueType="Qty";
                        }


                        if ((tStopAtQty == "True" || tStopAtQty == "1") || (tStopAtRate == "True" || tStopAtRate == "1"))
                        {
                            if (pnlNumeric.Visibility == Visibility.Hidden)
                            {
                                pnlNumeric.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                if (_Class.clsVariables.tHideKeyboard == true)
                                {
                                    pnlNumeric.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    pnlNumeric.Visibility = Visibility.Visible;
                                }
                            }
                            frm.ShowDialog();
                            //tempTimer.Stop();
                            //tempTimer.Start();
                        }
                        //    else
                        //    {
                        //        MyMessageBox.ShowBox("This item cannot be change Quantity and Rate", "Warning");
                        //    }
                        //}

                        if (_Class.clsVariables.tempstopatqtyremove == "Yes")
                        {
                            funBtnRemove();
                            txtEnterValue.Focus();
                            _Class.clsVariables.tempstopatqtyremove = "No";
                        }


                        DataTable dtItem = new DataTable();
                        dtItem.Rows.Clear();
                        SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                        cmd.Parameters.AddWithValue("@tItemName", SalesProject._Class.clsVariables.itemName);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtItem);
                        bool isChkOpenItem = false;
                        if (dtItem.Rows.Count > 0)
                        {

                            if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                            {
                                isChkOpenItem = true;
                            }
                        }


                        int tRowIndax = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // 
                            if (isChkOpenItem == true)
                            {
                                if (dt.Rows[i][0].ToString() == SalesProject._Class.clsVariables.itemName && i == SelectedRowIndex - 1)
                                {
                                    tRowIndax = SelectedRowIndex - 1;
                                    DataRow row = dt.NewRow();
                                    row[0] = SalesProject._Class.clsVariables.itemName;
                                    row[1] = SalesProject._Class.clsVariables.itemQty;
                                    row[2] = SalesProject._Class.clsVariables.itemRate;
                                    row[3] = String.Format("{0:0.00}", (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty)));

                                    dt.Rows.RemoveAt(i);
                                    // dt.Rows.InsertAt(row, i);

                                    //Stop at Rate is Zero.. remove item from list - Start

                                    double tRowAmt = (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty));
                                    if (tRowAmt > 0)
                                    {
                                        dt.Rows.InsertAt(row, i);
                                        gridItems.Rows[i].Selected = true;
                                    }
                                    else
                                    {
                                        if (i > 0)
                                        {
                                            gridItems.Rows[i - 1].Selected = true;
                                        }
                                    }

                                    //Stop at Rate is Zero.. remove item from list - End


                                    tReadingValueDisplay = 1;
                                    ClickedButtonDisplay = SalesProject._Class.clsVariables.itemName;
                                    drQtyDisplay = Convert.ToString(row[1]);
                                    drRateDisplay = Convert.ToString(row[2]);
                                    drAmtDisplay = Convert.ToString(row[3]);

                                    //if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                                    //{
                                    //    tempTimer.Start();

                                    //    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                                    //    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                                    //    _Class.clsVariables.spCustomerDis.WriteLine((SalesProject._Class.clsVariables.itemName.Length > 20) ? SalesProject._Class.clsVariables.itemName.Substring(0, 19) : SalesProject._Class.clsVariables.itemName);
                                    //    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                                    //    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                                    //    _Class.clsVariables.spCustomerDis.Write(row[1] + "*" + row[2] + "=" + row[3]);


                                    //}

                                }
                            }
                            else
                            {

                                if (dt.Rows[i][0].ToString() == SalesProject._Class.clsVariables.itemName)
                                {
                                    tRowIndax = SelectedRowIndex - 1;
                                    DataRow row = dt.NewRow();
                                    row[0] = SalesProject._Class.clsVariables.itemName;
                                    row[1] = SalesProject._Class.clsVariables.itemQty;
                                    row[2] = SalesProject._Class.clsVariables.itemRate;
                                    row[3] = String.Format("{0:0.00}", (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty)));
                                    dt.Rows.RemoveAt(i);
                                    //   dt.Rows.InsertAt(row, i);


                                    //Stop at Rate is Zero.. remove item from list - Start

                                    double tRowAmt = (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty));
                                    if (tRowAmt > 0)
                                    {
                                        dt.Rows.InsertAt(row, i);
                                    }

                                    //Stop at Rate is Zero.. remove item from list - End

                                    // dt.Rows[i][1] = dt.Rows[i][1].ToString().Replace("@", SalesProject._Class.clsVariables.itemQty);

                                    tReadingValueDisplay = 1;
                                    ClickedButtonDisplay = SalesProject._Class.clsVariables.itemName;
                                    drQtyDisplay = Convert.ToString(row[1]);
                                    drRateDisplay = Convert.ToString(row[2]);
                                    drAmtDisplay = Convert.ToString(row[3]);

                                    //if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                                    //{
                                    //    tempTimer.Stop();
                                    //    tempTimer.Start();
                                    //    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                                    //    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                                    //    _Class.clsVariables.spCustomerDis.WriteLine((SalesProject._Class.clsVariables.itemName.Length > 20) ? SalesProject._Class.clsVariables.itemName.Substring(0, 19) : SalesProject._Class.clsVariables.itemName);
                                    //    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                                    //    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                                    //    _Class.clsVariables.spCustomerDis.Write(row[1] + "*" + row[2] + "=" + row[3]);


                                    //}

                                }
                            }
                        }

                        try
                        {
                            double tQty = 0, tRate = 0, tAmt = 0;
                            if (gridItems.Rows.Count > 0)
                            {
                                double tRowAmt = (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty));
                                if (tRowAmt > 0)
                                {
                                    if (gridItems.Rows[tRowIndax].Cells[1].Value.ToString().Trim() != "" && gridItems.Rows[tRowIndax].Cells[2].Value.ToString().Trim() != "")
                                    {
                                        tQty = double.Parse(gridItems.Rows[tRowIndax].Cells[1].Value.ToString());
                                        tRate = double.Parse(gridItems.Rows[tRowIndax].Cells[2].Value.ToString());
                                        tAmt = tQty * tRate;
                                        gridItems.Rows[tRowIndax].Cells[2].Value = string.Format("{0:0.00}", tRate);
                                        gridItems.Rows[tRowIndax].Cells[3].Value = string.Format("{0:0.00}", tAmt);
                                    }

                                    funDisplayAmount(dt);
                                    gridItems.CurrentCell = gridItems.Rows[tRowIndax].Cells[0];
                                }
                                funRoundCalculate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MyMessageBox.ShowBox(ex.Message, "Warning");
                        }
                    }
                }
                //Stop at Qty and Rate Coding End
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        string tRowActionType = "";
        DataTable dsRound = new DataTable();
        string tRoundType, firstDecimal, secondDecimal;
        double tRoundValue, tWhole, tDecimal;
        void funRoundCalculate()
        {
            try
            {
                funConnectionStateCheck();
                SqlCommand cmd = new SqlCommand("sp_SalesCreation_RoundCalculate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                adpCmd.Fill(dsRound);
                //  dr = cmd.ExecuteReader();
                // dsRound.Load(dr);
                if (dsRound.Rows.Count > 0)
                {
                    tRoundType = dsRound.Rows[0]["RProp"].ToString();
                    // MessageBox.Show(tRoundType);
                    tRoundValue = Math.Round(double.Parse(lblNetAmt.Content.ToString()), 2);
                    tDecimal = Math.Round(tRoundValue % 1, 2);
                    //  MessageBox.Show(tDecimal.ToString());
                    tWhole = tRoundValue - tDecimal;
                    // MessageBox.Show(tWhole.ToString());
                    //  MessageBox.Show(Convert.ToString( tDecimal).Length.ToString());
                    if (tDecimal.ToString().Length == 1)
                    {
                        firstDecimal = "0";
                        secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1);
                    }
                    else if (tDecimal.ToString().Length == 4)
                    {
                        firstDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1);
                        secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
                    }
                    if (tRoundType == "5cent")
                    {
                        if (tDecimal == 0.99 || tDecimal == 0.98)
                        {
                            tWhole = tWhole + 1;
                            lblNetAmt.Content = String.Format("{0:0.00}", tWhole);
                        }
                        else if (tDecimal >= 0.90 && tDecimal < 0.98)
                        {
                            if (tDecimal.ToString().Length == 4)
                            {

                                switch (tDecimal.ToString().Substring(3, 1))
                                {

                                    case "0":
                                    case "1":
                                    case "2":
                                        {
                                            // secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
                                            lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
                                            break;
                                        }
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                        {
                                            // tWhole = tWhole + 1;
                                            lblNetAmt.Content = string.Format("{0:0.00}", (tWhole + Convert.ToDouble(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "5"))));
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                //  MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }
                        }
                        else
                        {
                            //      MessageBox.Show(tDecimal.ToString().Substring(3, 1));
                            if (tDecimal.ToString().Length == 4)
                            {
                                switch (tDecimal.ToString().Substring(3, 1))
                                {
                                    case "8":
                                    case "9":
                                    case "0":
                                    case "1":
                                    case "2":
                                        {
                                            //  MessageBox.Show(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1));

                                            if (firstDecimal == "9" || firstDecimal == "8")
                                            {
                                                secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                                lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            }
                                            else
                                            {
                                                //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                                //  lblNetAmt.Content = String.Format("{0:0.00}", (tRoundValue.ToString().Replace(secondDecimal.ToString()+firstDecimal.ToString(),secondDecimal+"0")));
                                                lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            }
                                            break;
                                        }
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                        {
                                            //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                            lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + Convert.ToDouble(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "5"))));
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                //   MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }

                        }
                        ////// string tempStr = lblNetAmt.Content.ToString().Trim();
                        //////// int start = tempStr.Length - 1;
                        //////// MessageBox.Show(tempStr.Substring(tempStr.Length - 1, 1));
                        //////                }
                    }
                    if (tRoundType == "10cent")
                    {
                        if (tDecimal <= 0.99 && tDecimal >= 0.95)
                        {
                            tWhole = tWhole + 1;
                            lblNetAmt.Content = String.Format("{0:0.00}", tWhole);
                        }
                        else if (tDecimal >= 0.90 && tDecimal < 0.95)
                        {
                            if (tDecimal.ToString().Length == 4)
                            {

                                switch (tDecimal.ToString().Substring(3, 1))
                                {

                                    case "0":
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                        {
                                            // secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
                                            lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
                                            break;
                                        }

                                }
                            }
                            else
                            {
                                //  MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }
                        }
                        else
                        {
                            //      MessageBox.Show(tDecimal.ToString().Substring(3, 1));
                            if (tDecimal.ToString().Length == 4)
                            {
                                switch (tDecimal.ToString().Substring(3, 1))
                                {
                                    case "0":
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                        {

                                            lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            break;
                                        }
                                    case "5":
                                    case "6":
                                    case "7":
                                    case "8":
                                    case "9":
                                        {
                                            secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                            lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                //   MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Its Helps to Display current system time 
            labelTime.Content = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            CommandManager.InvalidateRequerySuggested();
        }

        DataTable dtOffer = new DataTable();
        DataTable dtOfferSameFree = new DataTable();
        DataTable dtOfferDifferentFree = new DataTable();
        DataTable dtOfferName = new DataTable();
        DataTable dtSingleAllFreeItem = new DataTable();
        DataTable dtDifferent = new DataTable();

        public void funOfferLoad()
        {
            try
            {
                //This method helps to collect Offer Item and Free Item list
                dtFreeBalance.Rows.Clear();
                if (dtOfferDetails.Columns.Count == 0)
                {
                    dtOfferDetails.Columns.Add("ItemName");
                    dtOfferDetails.Columns.Add("OfferName");
                    dtOfferDetails.Columns.Add("OfferCount");
                    dtOfferDetails.Columns.Add("OfferRate");
                    dtOfferDetails.Columns.Add("OfferQty");
                    dtOfferDetails.Columns.Add("OfferTotQty");
                    dtOfferDetails.Columns.Add("OfferTotRate");
                    dtOfferDetails.Columns.Add("RemainQty");
                }
                string tQueryAppend = "";
                tBillDateDay = Convert.ToString(currentDate.DayOfWeek);
                if (tBillDateDay.ToUpper() == "Sunday".ToUpper())
                {
                    tQueryAppend = "Sunday=1 and ";
                }
                else if (tBillDateDay.ToUpper() == "Monday".ToUpper())
                {
                    tQueryAppend = "Monday=1 and ";
                }
                else if (tBillDateDay.ToUpper() == "Tuesday".ToUpper())
                {
                    tQueryAppend = "Tuesday=1 and ";
                }
                else if (tBillDateDay.ToUpper() == "Wednesday".ToUpper())
                {
                    tQueryAppend = "Wednesday=1 and ";
                }
                else if (tBillDateDay.ToUpper() == "Thursday".ToUpper())
                {
                    tQueryAppend = "thursday=1 and ";
                }
                else if (tBillDateDay.ToUpper() == "Friday".ToUpper())
                {
                    tQueryAppend = "friday=1 and ";
                }
                else if (tBillDateDay.ToUpper() == "Saturday".ToUpper())
                {
                    tQueryAppend = "sturday=1 and ";
                }
                else
                {
                    tQueryAppend = "";
                }
                dtOffer.Rows.Clear();

                // Code for getting Single item with Price Offer list
                string tQueryChk = @"Select FreeSno, FreeSnoGroup,OfferName, Item_table.Item_name, TotSaleQty, TotSalePrice, FromDate, ToDate, ItemType,FreeType,Active
from FreeItemMaster_table, Item_table Where " + tQueryAppend + "Item_table.Item_no=FreeItemMaster_table.Item_no and FreeType='Price' and FreeItemMaster_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) order by FreeSnoGroup ASC";
                SqlCommand cmdOffer = new SqlCommand(tQueryChk, con);
                SqlDataAdapter adpOffer = new SqlDataAdapter(cmdOffer);
                adpOffer.Fill(dtOffer);



                dtDifferent.Rows.Clear();
                // Procedure for getting Single item with Different Free list
                SqlCommand cmdDiffer = new SqlCommand("Pro_viewDiffFree", con);
                cmdDiffer.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adpDiffer = new SqlDataAdapter(cmdDiffer);
                adpDiffer.Fill(dtDifferent);


                dtOfferSameFree.Rows.Clear();
                // Code for getting Single item with Same Free Offer list
                tQueryChk = @"Select ViewSameFree.FreeItem_no,ViewSameFree.FreeItem_name,ViewSameFree.FreeQty,ViewSameFree. FreeSno,
ViewSameFree.OfferName,ViewSameFree.TotSaleQty,ViewSameFree.Item_no, Item_table.Item_name from ViewSameFree,Item_table
where " + tQueryAppend + "ViewSameFree.Item_no=Item_table.Item_no";
                cmdOffer = new SqlCommand(tQueryChk, con);
                adpOffer = new SqlDataAdapter(cmdOffer);
                adpOffer.Fill(dtOfferSameFree);


                if (dtOfferName.Columns.Count == 0)
                {
                    dtOfferName.Columns.Add("OfferName", typeof(string));
                    dtOfferName.Columns.Add("Qty", typeof(string));
                }
                // Code for getting Different item with Price Offer list
                DataRow[] dtOfferNameRow = dtOffer.Select("FreeType = 'Price' And ItemType='Different'");
                dtOfferName.Rows.Clear();

                foreach (DataRow row in dtOffer.Select("FreeType = 'Price' And ItemType='Different'"))
                {
                    dtOfferName.Rows.Add(row["OfferName"], "0");
                }

                dtOfferName = dtOfferName.DefaultView.ToTable(true, "OfferName");
                if (dtOfferName.Columns.Count == 1)
                {
                    // dtOfferName.Columns.Add("OfferName", typeof(string));
                    dtOfferName.Columns.Add("Qty", typeof(string));
                }


                dtSingleAllFreeItem.Rows.Clear();
                string tQueryChk1 = @"Select viewSingleFree.FreeItem_no,viewSingleFree.FreeItem_name,viewSingleFree.FreeQty,viewSingleFree. FreeSno,
viewSingleFree.OfferName,viewSingleFree.TotSaleQty,viewSingleFree.Item_no, Item_table.Item_name from viewSingleFree,Item_table
where " + tQueryAppend + " viewSingleFree.Item_no=Item_table.Item_no";
                SqlCommand cmdSingleAllFree = new SqlCommand(tQueryChk1, con);
                SqlDataAdapter adpSingleAllFree = new SqlDataAdapter(cmdSingleAllFree);
                adpSingleAllFree.Fill(dtSingleAllFreeItem);



            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public int tempGroupCount = 1;

        DataTable dtGroup = new DataTable();
        DataTable dtChk = new DataTable();
        //  Win32PrintClass w32prn = new Win32PrintClass();
        string tBillDateDay = "";
        DataTable dtFreeBalance = new DataTable();
        public void UCfrmVoid1_UCfrmVoidEvent_CloseClick()
        {
            //Its Delegate Event its execute from Display form Close button, for cloing Display screen and Open Sales screen

            UCfrmVoid1.Visibility = Visibility.Hidden;
            CurrentBill.Visibility = Visibility.Visible;
            UCFormSettle1.Visibility = Visibility.Hidden;
            UCMain1.Visibility = Visibility.Hidden;
            funSalesCreationLoad();
            txtEnterValue.Focus();
        }
        public void funSalesCreationLoad()
        {
            try
            {
                // Refer form load Event
                if (_Class.clsVariables.LoadPreviousBill != "LoadNot")
                {
                    if (dt.Columns.Count == 0)
                    {
                        dt.Columns.Add("ItemName", typeof(string));
                        dt.Columns.Add("Qty", typeof(string));
                        dt.Columns.Add("Rate", typeof(string));
                        dt.Columns.Add("Amt", typeof(string));
                        dt.Columns.Add("Disc", typeof(string));
                        dt.Columns.Add("SDisc", typeof(string));
                        dt.Columns.Add("Other", typeof(string));
                    }
                    gridItems.DataSource = dt.DefaultView;
                    gridItems.Columns[0].Width = 180;
                    gridItems.Columns[0].ReadOnly = true;

                    gridItems.Columns[1].Width = 50;
                    gridItems.Columns[2].Width = 50;
                    gridItems.Columns[1].ReadOnly = true;
                    gridItems.Columns[2].ReadOnly = true;
                    gridItems.Columns[3].Width = 50;
                    gridItems.Columns[3].ReadOnly = true;
                    gridItems.RowTemplate.Height = 35;
                    for (int i = 0; i < gridItems.Columns.Count; i++)
                    {
                        gridItems.Columns[i].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                    }
                    funConnectionStateCheck();
                    // Below  Procedure Helps to get End Of Day Date
                    SqlCommand cmdLastReset = new SqlCommand("sp_GetEndOfDay", con);
                    cmdLastReset.CommandType = CommandType.StoredProcedure;
                    SqlParameter result = new SqlParameter("@tResult", SqlDbType.DateTime);
                    result.Direction = ParameterDirection.Output;
                    cmdLastReset.Parameters.Add(result);
                    cmdLastReset.ExecuteNonQuery();
                    currentDate = (DateTime)result.Value;
                    SalesProject._Class.clsVariables.tEndOfDayDate = (DateTime)result.Value;
                    _Class.clsVariables.funControlSetting();
                    String searchFolder = System.Windows.Forms.Application.StartupPath + "\\Logo";
                    if (!System.IO.Directory.Exists(searchFolder))
                    {
                        System.IO.Directory.CreateDirectory(searchFolder);
                    }
                    //Below settings helps to load Promotion Details
                    if (_Class.clsVariables.tAllowOffer == true)
                    {
                        funOfferLoad();
                    }

                    DataTable dtCompany = new DataTable();
                    dtCompany.Rows.Clear();
                    SqlCommand cmd21 = new SqlCommand("sp_SalesCreationSelectAll", con);
                    cmd21.CommandType = CommandType.StoredProcedure;
                    cmd21.Parameters.AddWithValue("@tActionType", "COMPANYNAME");
                    SqlDataAdapter adp21 = new SqlDataAdapter(cmd21);
                    adp21.Fill(dtCompany);
                    if (dtCompany.Rows.Count > 0)
                    {
                        lblLogo.Content = dtCompany.Rows[0]["comp_name"].ToString();
                    }

                    //w32prn.SetPrinterName("BIXOLON BCD-1000");
                    // w32prn.SetPrinterName(SalesProject._Class.clsVariables.tCustomerDisplayName);

                    dtPrint.Rows.Clear();
                    if (_Class.clsVariables.dtSingleFree.Columns.Count == 0)
                    {
                        _Class.clsVariables.dtSingleFree.Columns.Add("ItemName", typeof(string));
                        _Class.clsVariables.dtSingleFree.Columns.Add("Qty", typeof(string));
                        _Class.clsVariables.dtSingleFree.Columns.Add("ScannedQty", typeof(string));
                        _Class.clsVariables.dtSingleFree.Columns.Add("MainItemName", typeof(string));
                        _Class.clsVariables.dtSingleFree.Columns.Add("OfferName", typeof(string));
                        _Class.clsVariables.dtSingleFree.Columns.Add("OfferFreeQty", typeof(string));
                        _Class.clsVariables.dtSingleFree.Columns.Add("TotSaleQty", typeof(string));
                    }
                    if (_Class.clsVariables.dtserailno.Columns.Count == 0)
                        _Class.clsVariables.dtserailno.Columns.Add("Serial_no", typeof(string));
                    lblCounterName.Content = _Class.clsVariables.tCounterName;

                    // Below Code Helps to get Receipt Printer Settings

                    SqlCommand cmd2 = new SqlCommand("Select Describ, Property from G_set union Select Rdesc as Describ,Rprop as Property from Rptset Union Select Describ as Describ,prop as Property from Custom_text", con);
                    SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                    adp2.Fill(dtPrint);

                    DataTable dtPrinter = new DataTable();
                    dtPrinter.Rows.Clear();
                    SqlCommand cmdPrinter = new SqlCommand("Select * from ReceiptPrintSettings_table where Counter=@tCounter", con);
                    cmdPrinter.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    SqlDataAdapter adpPrinter = new SqlDataAdapter(cmdPrinter);
                    adpPrinter.Fill(dtPrinter);
                    if (dtPrinter.Rows.Count > 0)
                    {
                        for (int mn = 0; mn < dtPrint.Rows.Count; mn++)
                        {
                            if (dtPrint.Rows[mn][0].ToString().Trim() == "Enable This Device*")
                            {
                                dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Enable_This_Device"].ToString();
                            }
                            if (dtPrint.Rows[mn][0].ToString().Trim() == "Printer Name*")
                            {
                                dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Printer_Name"].ToString();
                                _Class.clsVariables.tPrinterName = dtPrinter.Rows[0]["Printer_Name"].ToString();
                            }
                            if (dtPrint.Rows[mn][0].ToString().Trim() == "Printer Type*")
                            {
                                dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Printer_Type"].ToString();
                            }
                            if (dtPrint.Rows[mn][0].ToString().Trim() == "Print Copies*")
                            {
                                dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Print_Copies"].ToString();
                            }
                            if (dtPrint.Rows[mn][0].ToString().Trim() == "Characters Per Line*")
                            {
                                dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Characters_Per_Line"].ToString();
                            }

                        }
                    }
                    for (int mn = 0; mn < dtPrint.Rows.Count; mn++)
                    {
                        if (dtPrint.Rows[mn][0].ToString().Trim() == "Print Logo")
                        {
                            _Class.clsVariables.tPrintImageEnable = dtPrint.Rows[mn][1].ToString();
                            break;
                        }
                    }
                    DispatcherTimer dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                    dispatcherTimer.Start();
                    lblDate.Content = currentDate.ToShortDateString();
                    tBillDateDay = Convert.ToString(currentDate.DayOfWeek);
                    var bc = new BrushConverter();
                    // gridItems.ItemsSource = dt.DefaultView;
                    if (gridItems.Rows.Count > 0)  // Change if (gridItems.Items.Count > 0)
                    {
                        gridItems.SelectedRows.Equals(gridItems.Rows[0]);  // Change gridItems.SelectedItem = gridItems.Items[0];
                    }


                    //Below code helps to get total number groups exists in the System and Split first 7 and then assign to Group Button
                    // SqlCommand cmd1 = new SqlCommand(" SELECT distinct(Item_table.Item_groupno) FROM Item_table INNER JOIN Item_Grouptable ON item_table.item_Groupno =Item_Grouptable.Item_groupno ", con);
                    SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectAll", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@tActionType", "GROUPCOUNT");
                    // SqlCommand cmd1 = new SqlCommand(" select * from Item_GroupTable order by Grouppos ASC", con);         
                    int i1 = 0, j = 7;

                    SqlDataAdapter adp3 = new SqlDataAdapter(cmd1);
                    adp3.Fill(dtChk);
                    tempGroupCount = 1;
                    for (int mn = 0; mn < dtChk.Rows.Count; mn++)
                    // while (dr.Read())
                    {
                        i1 += 1;
                        if (i1 == j)
                        {
                            tempGroupCount += 1;
                            j = j + 7;
                        }
                    }

                    //  SqlDataAdapter cmd12 = new SqlDataAdapter("SELECT distinct(Item_Grouptable.Item_groupname),Item_Grouptable.GroupPos  FROM Item_table INNER JOIN Item_Grouptable ON item_table.item_Groupno =Item_Grouptable.Item_groupno  ;", con);
                    SqlCommand cmd12 = new SqlCommand("SELECT distinct(Item_Grouptable.Item_groupname),Item_Grouptable.GroupPos,Item_Grouptable.Font_Color,Item_Grouptable.Group_Color,(Case when Item_Grouptable.ImageLocation IS null then '' else Item_Grouptable.ImageLocation END) as ImageLocation, ImageVisibility  FROM Item_table INNER JOIN Item_Grouptable ON item_table.item_Groupno =Item_Grouptable.Item_groupno where item_table.Item_Active=1 and Item_Grouptable.Group_visibility='True' order by GroupPos", con);

                    SqlDataAdapter adp4 = new SqlDataAdapter(cmd12);
                    dtGroup.Rows.Clear();
                    adp4.Fill(dtGroup);
                    // Pass Value and assign group to Button
                    funFillGroup(0, 6);
                    clickCountGroup = 1;
                    txtEnterValue.Focus();

                    // Previous Bill Details Display Method
                    funPreviousBill();

                    // Customer Display Settings getting Procedure
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

                    try
                    {
                        if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                        {

                            byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                            _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);

                        }

                    }
                    catch (Exception ex)
                    {
                        MyMessageBox.ShowBox(ex.Message, "Warning");
                    }

                    //this method helps to display number of bill holded
                    lblHold.Content = funholdLabel();
                    txtEnterValue.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void UCFrmLogin1_UCFrmLoginEvent_loginClick()
        {
            try
            {
                //Delegate event executed from Login form
                UCMain1.lblUserName.Content = _Class.clsVariables.tUserName;
                UCMain1.lblcounterName.Content = _Class.clsVariables.tCounterName;
                UCMain1.lblLocationName.Content = _Class.clsVariables.tBranch;
                UCMain1.UCFrmLogin1.Visibility = Visibility.Hidden;
                UCMain1.pnlTableMain.Visibility = Visibility.Visible;
                UCMain1.pnlTableList.Visibility = Visibility.Visible;
                UCMain1.UCFrmManagerMain.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sales Screen Loading Form and Create object for delgate event
            UCMain1.UCFrmLogin1.UCFrmLoginEvent_loginClick += new UCFrmLoginEvent(UCFrmLogin1_UCFrmLoginEvent_loginClick);
            frmDiscountDisplay.UCFRMDiscountEventCloseClick += new UCFRMDiscountEvent(frmDiscountDisplay_UCFRMDiscountEventCloseClick);
            frmDiscountDisplay.UCFRMDiscountEventEnterClick += new UCFRMDiscountEvent(frmDiscountDisplay_UCFRMDiscountEventEnterClick);
            UCfrmVoid1.UCfrmVoidEvent_CloseClick += new UCfrmVoidEvent(UCfrmVoid1_UCfrmVoidEvent_CloseClick);
            UCfrmVoid1.UCfrmVoidEvent_ResettleClick += new UCfrmVoidEvent(UCfrmVoid1_UCfrmVoidEvent_ResettleClick);
            UCFormSettle1.UCFormSettleEvent_ResettleClose += new UCFormSettleEvent(UCFormSettle1_UCFormSettleEvent_ResettleClose);
            UCFormSettle1.UCFormSettleEvent_settleClose += new UCFormSettleEvent(UCFormSettle1_UCFormSettleEvent_settleClose);
            UCMain1.UCMainEventLogoutClick += new UCMainEvent(UCMain1_UCMainEventLogoutClick);
            UCMain1.UCMainEventDineInClick += new UCMainEvent1(UCMain1_UCMainEventDineInClick);
            UCMain1.UCMainEventBackOfficeClick += new UCMainEvent(UCMain1_UCMainEventBackOfficeClick);

            UCItemDiscount1.UCItemUpdateEventPriceChangeClick += new UCItemUpdateEvent(UCPnlItemUpdateQtyNRate_UCItemUpdateEventPriceChangeClick);
            UCItemDiscount1.UCItemUpdateEventRemoveItemClick += new UCItemUpdateEvent(UCItemDiscount1_UCFrmSplitEventRemoveItem); //+= new UCFrmSplitEvent(UCItemDiscount1_UCFrmSplitEventRemoveItem);
            // UCItemDiscount1.UCItemDicount_CloseClick += new UCFrmSplitEvent1(frmDiscountDisplay_UCFRMDiscountEventCloseClick);
            //UCItemDiscount1.UCItemDicount_EnterClick += new UCFrmSplitEvent1(frmDiscountDisplay_UCFRMDiscountEventEnterClick);
            UCItemDiscount1.UCItemUpdateEventFinishClick += new UCItemUpdateEvent(UCItemDiscount1_UCFrmSplitEventSubmitItem);
            UCItemDiscount1.UCItemUpdateEventShowModifierClick += new UCItemUpdateEvent(UCItemDiscount1_UCItemUpdateEventShowModifierClick);
            frmCashDrawPassword1.UCPasswordKeyClick += new UCPasswordEvent(frmCashDrawPassword1_UCPasswordKeyClick);

            funSalesCreationLoad();
            UCFormSettle1.Visibility = Visibility.Hidden;
            UCfrmVoid1.Visibility = Visibility.Hidden;
            CurrentBill.Visibility = Visibility.Hidden;
            uCSalesmen1.Visibility = Visibility.Hidden;
            UCMain1.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Maximized;
            txtEnterValue.Focus();
            //_Class.clsVariables.tVoidActionType = "SALESITEMCODE";
        }
        private void frmCashDrawPassword1_UCPasswordKeyClick()
        {
            //Delegate Event for visible Discount Form
            try
            {
                frmKeyBoard frm = new frmKeyBoard();
                frm.ShowDialog();

                //uckeyboard1.Visibility = Visibility.Visible;
                //uckeyboard1.txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void UCItemDiscount1_UCItemUpdateEventShowModifierClick()
        {
            //Delegate Event for visible Discount Form
            try
            {

                if (_Class.clsVariables.tMainDiscountType == "Individual")
                {
                    frmDiscountDisplay.Visibility = Visibility.Visible;

                    _Class.clsVariables.tSNetAmt = Convert.ToString(lblNetAmt.Content);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void UCPnlItemUpdateQtyNRate_UCItemUpdateEventPriceChangeClick()
        {
            try
            {
                //Delegate Event for Individual item price ,Qty and Disc Changing form
                UCPriceChange1.lblUserCtlTitle.Content = UCItemDiscount1.UCUpdatelblItemName.Content;
                UCPriceChange1.Visibility = Visibility.Visible;
                UCPriceChange1.txtValue.Text = string.Empty;
                UCPriceChange1.orginalvalues = "";
                UCPriceChange1.txtValue.Focus();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void UCItemDiscount1_UCFrmSplitEventItemCancelClick()
        {
            //Delegate Event for Individual item discount, call from Price Change form
            UCItemDiscount1.Visibility = Visibility.Hidden;
            _Class.clsVariables.tDiscountAction = "";
            txtEnterValue.Focus();
        }
        private void UCItemDiscount1_UCFrmSplitEventSubmitItem()
        {
            try
            {
                //Delegate Event for Item Discount Submit Button Event
                UCItemDiscount1.Visibility = Visibility.Hidden;
                frmDiscountDisplay.Visibility = Visibility.Hidden;
                UCPriceChange1.Visibility = Visibility.Hidden;
                string tItemName = _Class.clsVariables.itemName;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == _Class.clsVariables.itemIndex)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = _Class.clsVariables.itemName;
                        row[1] = _Class.clsVariables.itemQty;
                        row[2] = string.Format("{0:0.00}", Convert.ToDouble(_Class.clsVariables.itemRate));
                        row[3] = string.Format("{0:0.00}", (Convert.ToDouble(_Class.clsVariables.itemRate) * Convert.ToDouble(SalesProject._Class.clsVariables.itemQty)));
                        row[4] = string.Format("{0:0.00}", (Convert.ToDouble(_Class.clsVariables.itemDisc)));
                        dt.Rows.RemoveAt(i);
                        dt.Rows.InsertAt(row, i);

                        //Free Code try to Implementation- Start


                        //Free Code try to Implementation- End
                        break;

                    }
                }
                gridItems.DataSource = dt;
                gridItems.CurrentCell = gridItems.Rows[_Class.clsVariables.itemIndex].Cells[0];
                funDisplayAmount(dt);
                funRoundCalculate();


                _Class.clsVariables.tDiscountAction = "";
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void UCItemDiscount1_UCFrmSplitEventRemoveItem()
        {
            try
            {
                //Delegate Event for remove item from list
                funBtnRemove();
                UCItemDiscount1.Visibility = Visibility.Hidden;
                UCPriceChange1.Visibility = Visibility.Hidden;
                frmDiscountDisplay.Visibility = Visibility.Hidden;
                _Class.clsVariables.tDiscountAction = "";
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void UCMain1_UCMainEventBackOfficeClick(object sender, RoutedEventArgs e)
        {
            //Delegate event for cloing Main screen
            try
            {
                // this.Close();
                this.Hide();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void UCMain1_UCMainEventDineInClick()
        {
            try
            {
                //Delegate Event for POS loading Screen
                funSalesCreationLoad();
                UCFormSettle1.Visibility = Visibility.Hidden;
                UCfrmVoid1.Visibility = Visibility.Hidden;
                CurrentBill.Visibility = Visibility.Visible;
                UCMain1.Visibility = Visibility.Hidden;
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void UCMain1_UCMainEventLogoutClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Delegate event for Main screen logout button
                CurrentBill.Visibility = Visibility.Hidden;




                UCMain1.Visibility = Visibility.Visible;
                UCMain1.funLoad();
                UCMain1.funMainLoad();
                UCMain1.pnlTableMain.Visibility = Visibility.Hidden;
                UCMain1.pnlTableList.Visibility = Visibility.Hidden;
                UCMain1.UCFrmManagerMain.Visibility = Visibility.Hidden;

                UCMain1.UCFrmLogin1.Visibility = Visibility.Visible;
                UCMain1.UCFrmLogin1.txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void UCfrmVoid1_UCfrmVoidEvent_ResettleClick()
        {
            try
            {
                // Delegate event for resettle button from display form
                UCFormSettle1.currentDate = UCfrmVoid1.currentDate;
                UCFormSettle1.txtEnterValue.Text = "";
                UCFormSettle1.dtDisplay.Rows.Clear();
                for (int i = 0; i < UCfrmVoid1.dt.Rows.Count; i++)
                {
                    UCFormSettle1.dtDisplay.Rows.Add(UCfrmVoid1.dt.Rows[i][0].ToString(), UCfrmVoid1.dt.Rows[i][1].ToString(), UCfrmVoid1.dt.Rows[i][2].ToString(), UCfrmVoid1.dt.Rows[i][3].ToString(), UCfrmVoid1.dt.Rows[i][4].ToString());
                }
                UCFormSettle1.lblBillNo.Content = UCfrmVoid1.lblBillNo.Content.ToString();
                UCFormSettle1.lblTotQty.Content = UCfrmVoid1.lblTotQty.Content.ToString();
                UCFormSettle1.lblTotAmt.Content = UCfrmVoid1.lblTotAmt.Content.ToString();
                UCFormSettle1.lblDiscount.Content = UCfrmVoid1.lblDiscount.Content.ToString();
                UCFormSettle1.lblNetAmt.Content = UCfrmVoid1.lblNetAmt.Content.ToString();
                UCFormSettle1.lblTaxAmt.Content = UCfrmVoid1.lblTaxAmt.Content.ToString();
                UCFormSettle1.txtAmount.Text = UCfrmVoid1.lblNetAmt.Content.ToString();
                UCFormSettle1.dtSettleVoid.Rows.Clear();


                UCFormSettle1.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                UCFormSettle1.SalesCreationEventHandlerNew1 += new EventHandler(CloseEvent);

                UCFormSettle1.Visibility = Visibility.Visible;
                UCfrmVoid1.Visibility = Visibility.Hidden;
                CurrentBill.Visibility = Visibility.Hidden;
                UCMain1.Visibility = Visibility.Hidden;

                UCFormSettle1.txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void UCFormSettle1_UCFormSettleEvent_ResettleClose()
        {
            //Delegate Event for Resettle Close
            try
            {
                UCfrmVoid1.funLoad();
                if (UCFormSettle1.tTenderClose != "Close")
                {
                    UCFormSettle1.tTenderClose = "";
                    for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                    {
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                        {
                            charPerLine = dtPrint.Rows[i]["Property"].ToString();
                        }
                        if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                        {
                            if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                            {
                                UCfrmVoid1.funPrevPrint();
                                break;
                            }
                            else if (dtPrint.Rows[i]["Property"].ToString() == "After Confirm")
                            {
                                string res = MyMessageBox1.ShowBox("Do you want to print", "Warning");
                                if (res == "1")
                                {
                                    UCfrmVoid1.funPrevPrint();
                                }
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                _Class.clsVariables.tControlFrom = "";
                CurrentBill.Visibility = Visibility.Hidden;
                UCfrmVoid1.Visibility = Visibility.Visible;
                UCFormSettle1.Visibility = Visibility.Hidden;
                UCMain1.Visibility = Visibility.Hidden;
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void UCFormSettle1_UCFormSettleEvent_settleClose()
        {
            //Delegate event for Tender Screen Close Event
            try
            {
                if (UCFormSettle1.tTenderClose != "Close")
                {
                    UCFormSettle1.tTenderClose = "";
                    for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                    {
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                        {
                            charPerLine = dtPrint.Rows[i]["Property"].ToString();
                        }
                        if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                        {
                            if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                            {
                                funPrevPrint();
                                break;
                            }
                            else if (dtPrint.Rows[i]["Property"].ToString() == "After Confirm")
                            {
                                string res = MyMessageBox1.ShowBox("Do you want to print", "Warning");
                                if (res == "1")
                                {
                                    funPrevPrint();
                                }
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                CurrentBill.Visibility = Visibility.Visible;
                UCfrmVoid1.Visibility = Visibility.Hidden;
                UCFormSettle1.Visibility = Visibility.Hidden;
                UCMain1.Visibility = Visibility.Hidden;
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void frmDiscountDisplay_UCFRMDiscountEventCloseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Delegate Event for Discount Close 
                _Class.clsVariables.tDiscountAction = "";
                UCPriceChange1.Visibility = Visibility.Hidden;
                frmDiscountDisplay.Visibility = Visibility.Hidden;
                UCItemDiscount1.Visibility = Visibility.Hidden;
                funDisplayAmount(dt);
                funRoundCalculate();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        private void frmDiscountDisplay_UCFRMDiscountEventEnterClick(object sender, RoutedEventArgs e)
        {
            //Delegate event for loading Discount Form
            try
            {
                frmDiscountDisplay.Visibility = Visibility.Hidden;
                UCItemDiscount1.Visibility = Visibility.Hidden;

                if (_Class.clsVariables.tDiscountAction.ToUpper() == "MAIN")
                {
                    _Class.clsVariables.tDiscountAction = "";
                    if (frmDiscountDisplay.Disc == null)
                    {
                        lblOverAllDiscAmt.Content = "0.00";
                    }
                    else
                    {
                        lblOverAllDiscAmt.Content = String.Format("{0:0.00}", Convert.ToDouble(frmDiscountDisplay.Disc));
                    }
                }
                if (_Class.clsVariables.tDiscountAction == "ItemDiscount")
                {
                    _Class.clsVariables.tDiscountAction = "";
                    dt.Rows[_Class.clsVariables.itemIndex]["Disc"] = (frmDiscountDisplay.Disc.Trim() == "") ? "0.00" : String.Format("{0:0.00}", Convert.ToDouble(frmDiscountDisplay.Disc));

                    gridItems.DataSource = dt;
                }
                funDisplayAmount(dt);
                funRoundCalculate();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public string recDiscount;
        public string tChkCusDisEnable = "";
        public void funPreviousBill()
        {
            try
            {
                //Its helps to diplay Previous bill record and Display  next bill no
                DataTable dsPrevious = new DataTable();
                dsPrevious.Clear();
                funConnectionStateCheck();
                SqlCommand cmd1 = new SqlCommand("SP_PREVIOUSBILL", con);
                cmd1.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlParameter resultPreviousBill = new SqlParameter("@tRowCount", SqlDbType.Int);
                resultPreviousBill.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultPreviousBill);
                SqlParameter resultNetAmount = new SqlParameter("@tSmas_NetAmount", SqlDbType.Float);
                resultNetAmount.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultNetAmount);
                SqlParameter resultRcvdAmt = new SqlParameter("@tSmas_Rcvdamount", SqlDbType.Float);
                resultRcvdAmt.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultRcvdAmt);
                SqlParameter resultRefundAmt = new SqlParameter("@tRefundAmt", SqlDbType.Float);
                resultRefundAmt.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultRefundAmt);

                SqlParameter resultPreviousBill1 = new SqlParameter("@tRowCount1", SqlDbType.Float);
                resultPreviousBill1.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultPreviousBill1);

                cmd1.ExecuteNonQuery();
                lblPreviosBillNo.Content = (resultPreviousBill.Value.ToString().Trim() == "") ? "0" : resultPreviousBill.Value.ToString();
                lblBillAmt.Content = String.Format("{0:0.00}", (resultNetAmount.Value.ToString().Trim() == "") ? 0 : ((double)resultNetAmount.Value));
                lblRcvdAmt.Content = String.Format("{0:0.00}", (resultRcvdAmt.Value.ToString().Trim() == "") ? 0 : ((double)resultRcvdAmt.Value));
                lblRefundAmt.Content = String.Format("{0:0.00}", (((resultRcvdAmt.Value.ToString().Trim() == "") ? 0 : ((double)resultRcvdAmt.Value)) - ((resultNetAmount.Value.ToString().Trim() == "") ? 0 : ((double)resultNetAmount.Value))));

                double code = double.Parse((resultPreviousBill1.Value.ToString().Trim() == "") ? "0" : resultPreviousBill1.Value.ToString());
                if (code < 9)
                {
                    lblBillNo.Content = ("00" + Convert.ToString(code + 1));
                }
                else if (code < 99)
                {
                    lblBillNo.Content = ("0" + Convert.ToString(code + 1));
                }
                else
                    lblBillNo.Content = (Convert.ToString(code + 1));
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        string tStopAtRate, tStopAtQty;
        string strSales;
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Clear();
            //SqlDataAdapter adp = new SqlDataAdapter("select salesmen from Control_table", con);
            //adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    strSales = dt.Rows[0]["salesmen"].ToString();
            //}
            //if (strSales == "1")
            //{


            //DataTable dtSales = new DataTable();
            //dtSales.Rows.Clear();
            //SqlDataAdapter adpsalesmen = new SqlDataAdapter("select Ledger_Name as cus_Name from Ledger_table where Ledger_groupno='32' and Ledger_no<>14", con);
            //adpsalesmen.Fill(dtSales);
            //if (dtSales.Rows.Count > 0)
            //{

            //    if (UCCustomerList1.Visibility == Visibility.Visible)
            //    {
            //        UCCustomerList1.Visibility = Visibility.Hidden;
            //    }
            //    else
            //    {
            //        UCCustomerList1.Visibility = Visibility.Visible;
            //    }
            //}


            //    else
            //    {
            //        MyMessageBox.ShowBox("There is no Salesmen", "Warning");
            //    }
            //DataTable dtSales = new DataTable();
            //dtSales.Rows.Clear();
            //SqlDataAdapter adpsalesmen = new SqlDataAdapter("select Ledger_Name as Salesmen_Name from Ledger_table where Ledger_groupno=51 and Ledger_no<>14", con);
            //adpsalesmen.Fill(dtSales);
            //if (dtSales.Rows.Count > 0)
            //{

            //    if (uCSalesmen1.Visibility == Visibility.Visible)
            //    {
            //        uCSalesmen1.Visibility = Visibility.Hidden;
            //    }
            //    else
            //    {
            //        uCSalesmen1.Visibility = Visibility.Visible;
            //    }
            //}
            //else
            //{
            //    MyMessageBox.ShowBox("There is no Salesmen", "Warning");
            //}
            //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
            //{
            //    bool isQtyChk = false;
            //    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
            //    {
            //        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
            //        if (tQty == 0)
            //        {
            //            isQtyChk = true;
            //        }
            //    }
            //    if (isQtyChk == false)
            //    {
            //        _Class.clsVariables.tNoRead = "NOREAD";
            //        uCSalesmen1.lblBillNo.Content = lblBillNo.Content.ToString();
            //        uCSalesmen1.lblTotQty.Content = lblTotQty.Content.ToString();
            //        uCSalesmen1.lblTotAmt.Content = lblTotAmt.Content.ToString();
            //        uCSalesmen1.lblDiscount.Content = lblDiscount.Content.ToString();
            //        uCSalesmen1.lblNetAmt.Content = lblNetAmt.Content.ToString();
            //        uCSalesmen1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();
            //        uCSalesmen1.dtDisplay.Rows.Clear();

            //       // uCSalesmen1.SalesCreationEventHandlerNewSalesmen += new EventHandler(CloseEventSalemen);
            //    }
            //    else
            //    {
            //        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
            //    }
            //}
            //else
            //{
            //    uCSalesmen1.Visibility = Visibility.Hidden;
            //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
            //}
            //}
            //else
            //{
            //    MyMessageBox.ShowBox("You not have permission", "warning");
            //}
        }
        public void CloseEventSalemen(object sender, EventArgs e)
        {
            if (strSalesmenSales == "1")
            {
                if (dt.Rows.Count > 0)
                {
                    bool isQtyChk = false;
                    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                    {
                        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                        if (tQty == 0)
                        {
                            isQtyChk = true;
                        }
                    }

                    if (isQtyChk == false)
                    {
                        WCFServices.Service1 objService = new WCFServices.Service1();
                        objService.btnCashButtonHome(lblTotAmt.Content.ToString(), lblNetAmt.Content.ToString(), lblTaxAmt.Content.ToString(), _Class.clsVariables.tUserNo, _Class.clsVariables.tCounter, dt, lblDiscount.Content.ToString(), string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType, _Class.clsVariables.dtSingleFree, _Class.clsVariables.tempsalesmenLedgerNo, _Class.clsVariables.tempsalesmenNote, _Class.clsVariables.dtserailno);

                        gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                        dtFreeBalance.Rows.Clear();
                        dt.Clear();
                        _Class.clsVariables.dtSingleFree.Rows.Clear();
                        frmDiscountDisplay.Visibility = Visibility.Hidden;
                        UCItemDiscount1.Visibility = Visibility.Hidden;
                        lblOverAllDiscAmt.Content = "0.00";
                        lblSpecialDiscAmt.Content = "0.00";
                        lblGroupDiscAmt.Content = "0.00";
                        lblNetAmt.Content = "0.00";
                        lblDiscount.Content = "0.00";
                        lblTotQty.Content = "0.00";
                        lblTotAmt.Content = "0.00";
                        lblTaxAmt.Content = "0.00";
                        funThankYou();
                        funPreviousBill();
                        funBalanceAmtDisplay();
                        funDrawerOpen();

                        for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                        {
                            if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i]["Property"].ToString();
                            }
                            if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                            {
                                if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                                {
                                    funPrevPrint();
                                    break;
                                }
                                else if (dtPrint.Rows[i]["Property"].ToString() == "After Confirm")
                                {
                                    string res = MyMessageBox1.ShowBox("Do you want to print", "Warning");
                                    if (res == "1")
                                    {
                                        funPrevPrint();
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        strSalesmenSales = "";
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                    }

                }
            }
            else if (strSalesmenSales == "2")
            {
                //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                {

                    bool isQtyChk = false;
                    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                    {
                        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                        if (tQty == 0)
                        {
                            isQtyChk = true;
                        }
                    }

                    if (isQtyChk == false)
                    {

                        WCFServices.Service1 objService = new WCFServices.Service1();
                        objService.btnNETSButtonHome(lblTotAmt.Content.ToString(), lblNetAmt.Content.ToString(), lblTaxAmt.Content.ToString(), _Class.clsVariables.tUserNo, _Class.clsVariables.tCounter, dt, lblDiscount.Content.ToString(), string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType, _Class.clsVariables.dtSingleFree, _Class.clsVariables.tempsalesmenLedgerNo, _Class.clsVariables.tempsalesmenNote, _Class.clsVariables.dtserailno);

                        gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                        dtFreeBalance.Rows.Clear();
                        _Class.clsVariables.dtSingleFree.Rows.Clear();
                        frmDiscountDisplay.Visibility = Visibility.Hidden;
                        UCItemDiscount1.Visibility = Visibility.Hidden;
                        lblOverAllDiscAmt.Content = "0.00";
                        lblSpecialDiscAmt.Content = "0.00";
                        lblGroupDiscAmt.Content = "0.00";
                        dt.Clear();
                        lblNetAmt.Content = "0.00";
                        lblDiscount.Content = "0.00";
                        lblTotQty.Content = "0.00";
                        lblTotAmt.Content = "0.00";
                        lblTaxAmt.Content = "0.00";
                        funThankYou();
                        funPreviousBill();
                        funBalanceAmtDisplay();
                        // funDrawerOpen();


                        for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                        {
                            if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i]["Property"].ToString();
                            }
                            if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                            {
                                if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                                {
                                    funPrevPrint();
                                    break;
                                }
                                else if (dtPrint.Rows[i]["Property"].ToString() == "After Confirm")
                                {
                                    string res = MyMessageBox1.ShowBox("Do you want to print", "Warning");
                                    if (res == "1")
                                    {
                                        funPrevPrint();
                                    }
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        strSalesmenSales = "";
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                    }

                }
            }
            else if (strSalesmenSales == "3")
            {
                //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                {
                    bool isQtyChk = false;
                    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                    {
                        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                        if (tQty == 0)
                        {
                            isQtyChk = true;
                        }
                    }

                    if (isQtyChk == false)
                    {
                        _Class.clsVariables.tNoRead = "NOREAD";
                        //  FormSettle frm = new FormSettle(dt);
                        //   frm.tempBillAmount = lblTotAmt.Content.ToString();
                        UCFormSettle1.txtAmount.Text = lblNetAmt.Content.ToString();
                        UCFormSettle1.currentDate = currentDate;
                        UCFormSettle1.txtEnterValue.Text = "";
                        UCFormSettle1.gridDisplay.DataSource = dt.DefaultView;
                        // UCFormSettle1.ds1.Tables.Add(dt.Copy());
                        UCFormSettle1.gridDisplay.Columns[0].Width = 180;
                        UCFormSettle1.gridDisplay.Columns[0].ReadOnly = true;
                        UCFormSettle1.gridDisplay.Columns[1].Width = 50;
                        UCFormSettle1.gridDisplay.Columns[2].Width = 50;
                        UCFormSettle1.gridDisplay.Columns[3].Width = 50;
                        UCFormSettle1.gridDisplay.Columns[3].ReadOnly = true;
                        UCFormSettle1.gridDisplay.RowTemplate.Height = 35;
                        UCFormSettle1.dtSettle.Rows.Clear();
                        UCFormSettle1.lblBillNo.Content = lblBillNo.Content.ToString();
                        UCFormSettle1.lblTotQty.Content = lblTotQty.Content.ToString();
                        UCFormSettle1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                        UCFormSettle1.lblDiscount.Content = lblDiscount.Content.ToString();
                        UCFormSettle1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                        UCFormSettle1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();

                        UCFormSettle1.dtDisplay.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UCFormSettle1.dtDisplay.Rows.Add(Convert.ToString(dt.Rows[i][0]), Convert.ToString(dt.Rows[i][1]), Convert.ToString(dt.Rows[i][2]), Convert.ToString(dt.Rows[i][3]), Convert.ToString(dt.Rows[i][4]), Convert.ToString(dt.Rows[i][5]), Convert.ToString(dt.Rows[i][6]));
                        }

                        UCFormSettle1.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                        UCFormSettle1.SalesCreationEventHandlerNewCash += new EventHandler(CloseEvent2);
                        UCFormSettle1.SalesCreationEventHandlerNew1 += new EventHandler(CloseEvent);

                        UCFormSettle1.Visibility = Visibility.Visible;
                        UCfrmVoid1.Visibility = Visibility.Hidden;
                        CurrentBill.Visibility = Visibility.Hidden;
                        UCMain1.Visibility = Visibility.Hidden;
                        //frm.ShowDialog();
                        //lblRefundAmt.Content = string.Format("{0:0.00}", (frm.Refund.ToString() == "") ? 0.00 : double.Parse(frm.Refund.ToString()));
                        strSalesmenSales = "";
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                    }
                }
            }
        }

        public void funBtnup()
        {
            //Select up record
            try
            {
                if (gridItems.Rows.Count > 0)
                {
                    if (gridItems.SelectedRows[0].Index > 0)
                    {
                        int row = gridItems.SelectedRows[0].Index;// Change gridItems.SelectedIndex--;
                        row--;
                        if (row >= 0)
                        {
                            gridItems.Rows[row].Selected = true;
                        }
                    }
                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            //Select Down record
            funBtnDown();
        }
        public void funBtnDown()
        {
            //Select Down record
            try
            {
                if (gridItems.Rows.Count > 0)
                {
                    int row = gridItems.SelectedRows[0].Index;
                    row++;
                    if (gridItems.Rows.Count > row)
                    {
                        gridItems.Rows[row].Selected = true;
                    }
                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        private void btnQuantity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Remove Item from List
            funBtnRemove();
            txtEnterValue.Focus();
        }

        public void funBtnRemove()
        {
            try
            {
                gridItems.EndEdit();
                //gridItems.CellEndEdit = true;
                if (dt.Rows.Count > 0)
                {

                    if (gridItems.SelectedRows.Count > 0)
                    {
                        //Below code Helps to remove selected item from list
                        int row1 = gridItems.SelectedRows[0].Index;
                        string tItemName = dt.Rows[row1]["ItemName"].ToString();
                        double tQty = double.Parse(dt.Rows[row1]["Qty"].ToString());
                        double tRate = double.Parse(dt.Rows[row1]["Rate"].ToString());
                        dt.Rows.RemoveAt(row1);  // Change dt.Rows.RemoveAt(gridItems.Items.IndexOf(gridItems.SelectedItem));
                        if (gridItems.Rows.Count != 0)
                        {
                            gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                        }

                        // Stored removed item Record in Database
                        SqlCommand cmd = new SqlCommand("sp_RemovedItemInsert", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tItem_NoValue", tItemName);
                        cmd.Parameters.AddWithValue("@tnt_Qty", tQty);
                        cmd.Parameters.AddWithValue("@tRate", tRate);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        //If the Item not selected in the list, last record will be remove
                        int row1 = gridItems.Rows.Count - 1;
                        string tItemName = dt.Rows[row1]["ItemName"].ToString();

                        double tQty = double.Parse(dt.Rows[row1]["Qty"].ToString());
                        double tRate = double.Parse(dt.Rows[row1]["Rate"].ToString());
                        dt.Rows.RemoveAt(row1);  // Change dt.Rows.RemoveAt(gridItems.Items.IndexOf(gridItems.SelectedItem));
                        if (gridItems.Rows.Count != 0)
                        {
                            gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                        }
                        // Stored removed item Record in Database
                        SqlCommand cmd = new SqlCommand("sp_RemovedItemInsert", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tItem_NoValue", tItemName);
                        cmd.Parameters.AddWithValue("@tnt_Qty", tQty);
                        cmd.Parameters.AddWithValue("@tRate", tRate);
                        cmd.ExecuteNonQuery();

                    }
                    UCItemDiscount1.Visibility = Visibility.Hidden;
                    frmDiscountDisplay.Visibility = Visibility.Hidden;
                    UCPriceChange1.Visibility = Visibility.Hidden;
                }
                gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                funDiscountAmtLoad();

                gridItems.Columns[0].Width = 180;
                gridItems.Columns[0].ReadOnly = true;
                gridItems.Columns[1].Width = 50;
                gridItems.Columns[2].Width = 50;
                gridItems.Columns[3].Width = 50;
                gridItems.Columns[3].ReadOnly = true;
                gridItems.RowTemplate.Height = 35;

                funDisplayAmount(dt);
                funRoundCalculate();
                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {

                    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                    _Class.clsVariables.spCustomerDis.WriteLine("Total Bill Amount");
                    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                    _Class.clsVariables.spCustomerDis.Write(lblNetAmt.Content.ToString());

                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        int tempFindStar = 0;
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            //Enter Button 
            if (pnlNumeric.Visibility == Visibility.Hidden)
            {
                if (_Class.clsVariables.tHideKeyboard == true)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (_Class.clsVariables.tHideKeyboard == true)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        pnlNumeric.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                pnlNumeric.Visibility = Visibility.Visible;
            }
            var bc = new BrushConverter();
            lblLogo.Foreground = (Brush)bc.ConvertFrom("#FFADF213");
            tempTimer.Interval = 1000;
            tempTimer.Enabled = false;
            tempTimer.Tick += new EventHandler(timer1_Tick);
            tTimerCount = 0;
            // tempTimer.Start();
            funBtnEnterTextBox();
            //funBtnEnter();
            txtEnterValue.Focus();
        }

        public void funReplaceFreeItemAmt()
        {
            //No Need 
            try
            {
                Dictionary<string, double> dicSum1 = new Dictionary<string, double>();
                foreach (DataRow row in dtFreeBalance.Rows)
                {
                    string group = row["ItemName"].ToString();
                    double Qty = (string.IsNullOrEmpty(Convert.ToString(row["Qty"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Qty"]));
                    if (dicSum1.ContainsKey(group))
                        dicSum1[group] += Qty
;
                    else
                        dicSum1.Add(group, Qty);
                }
                tempdtSingleFree.Rows.Clear();
                foreach (string g in dicSum1.Keys)
                {
                    tempdtSingleFree.Rows.Add(g, dicSum1[g]);
                }
                for (int k = 0; k < tempdtSingleFree.Rows.Count; k++)
                {
                    for (int g = 0; g < dt.Rows.Count; g++)
                    {
                        if (Convert.ToString(dt.Rows[g]["ItemName"]) == Convert.ToString(tempdtSingleFree.Rows[k]["ItemName"]))
                        {
                            double tChkCurrentQty = Convert.ToDouble(dt.Rows[g]["Qty"]);
                            double tChkFreeQty = Convert.ToDouble(tempdtSingleFree.Rows[k]["Qty"]);
                            //  dt.Rows[g]["Amt"] = string.Format("{0:0.00}", tChkCurrentQty * Convert.ToDouble(dt.Rows[g]["Rate"]));
                            if (tChkCurrentQty >= tChkFreeQty)
                            {
                                // dt.Rows[g]["Amt"] = string.Format("{0:0.00}", (tChkCurrentQty - tChkFreeQty) * Convert.ToDouble(dt.Rows[g]["Rate"]));
                                double tSpecialDiscTot = string.IsNullOrEmpty(Convert.ToString(lblSpecialDiscAmt.Content)) ? 0 : Convert.ToDouble(Convert.ToString(lblSpecialDiscAmt.Content));
                                lblSpecialDiscAmt.Content = string.Format("{0:0.00}", (tSpecialDiscTot + (tChkFreeQty * Convert.ToDouble(dt.Rows[g]["Rate"]))));
                            }
                            else
                            {
                                // dt.Rows[g]["Amt"] = "0.00";                               
                                double tSpecialDiscTot = string.IsNullOrEmpty(Convert.ToString(lblSpecialDiscAmt.Content)) ? 0 : Convert.ToDouble(Convert.ToString(lblSpecialDiscAmt.Content));
                                lblSpecialDiscAmt.Content = string.Format("{0:0.00}", (tSpecialDiscTot + (tChkCurrentQty * Convert.ToDouble(dt.Rows[g]["Rate"]))));
                            }
                            // dt.Rows[g]["Amt"] = "0.00";   
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public string tReadScaleAgainState = "";
        double tProcessCurrentQty = 0;
        double tTotAmtOffer = 0.00;
        double tTotAmtOfferPerQty = 0;
        DataTable dtOfferDetails = new DataTable();
        public void funFreeItem()
        {
            try
            {
                //This method help to calculate Promotion datails, Like Special Disc 
                double tSpecialDiscount = 0;
                double tSpecialDiscountSingleFree = 0;
                for (int mnk1 = 0; mnk1 < dt.Rows.Count; mnk1++)
                {
                    dtFreeBalance.Rows.Clear();
                    string tOfferName = "", tFreeItemName = "";
                    double tOfferQty = 0, tRemainingQty = 0, tOfferQtyCount = 0, tOfferRate = 0.0, tFreeQty = 0.0;
                    double tCurrentQty = Convert.ToDouble(dt.Rows[mnk1]["Qty"]);
                    string iname = dt.Rows[mnk1]["ItemName"].ToString();
                    int ni = iname.IndexOf("-");
                    if (ni != -1)
                        iname = iname.Substring(0, ni);
                    string tClickedButton = Convert.ToString(iname);
                    double tCurrentRate = Convert.ToDouble(dt.Rows[mnk1]["Rate"]);
                    tFreeItemName = tClickedButton;
                    tClickedButton = (tClickedButton.IndexOf("'") == -1) ? tClickedButton : tClickedButton.Replace("'", "''");
                    DataRow[] dtOfferRow = dtOffer.Select("FreeType = 'Price' And ItemType='Single' and Item_Name='" + tClickedButton + "'", "TotSaleQty DESC");
                    DataRow[] dtOfferSameFreeRow = dtOfferSameFree.Select("Item_Name='" + tClickedButton + "'", "TotSaleQty DESC");
                    // DataRow[] dtRowSingleFree = dtSingleAllFreeItem.Select("Item_Name='" + tClickedButton + "'");
                    DataRow[] dtRowSingleFree = dtSingleAllFreeItem.Select("Item_Name='" + tClickedButton + "'", "TotSaleQty DESC");
                    DataRow[] dtRowDifferentPrice = dtDifferent.Select("FreeType = 'Price' And ItemType='Different' and Item_name='" + tClickedButton + "'", "TotSaleQty DESC");


                    // Single -Price Code
                    if (dtOfferRow.Length > 0)
                    {
                        tProcessCurrentQty = tCurrentQty;

                        tTotAmtOffer = 0.00;
                        //     dtOfferDetails.Rows.Clear();
                        for (int mn = 0; mn < dtOfferRow.Length; mn++)
                        {
                            tOfferQty = Convert.ToDouble(dtOfferRow[mn]["TotSaleQty"]);
                            tOfferRate = Convert.ToDouble(dtOfferRow[mn]["TotSalePrice"]);
                            tOfferName = Convert.ToString(dtOfferRow[mn]["OfferName"]);
                            while (tOfferQty <= tProcessCurrentQty)
                            {
                                tOfferQtyCount = (int)(tProcessCurrentQty / tOfferQty);
                                tRemainingQty = tProcessCurrentQty - (tOfferQtyCount * tOfferQty);
                                tProcessCurrentQty = tRemainingQty;
                                tTotAmtOffer += (tOfferQtyCount * tOfferRate);
                                //  dtOfferDetails.Rows.Add(tClickedButton, tOfferName, tOfferQtyCount, tOfferRate,tOfferQty,(tOfferQtyCount * tOfferQty), (tOfferQtyCount * tOfferRate), tRemainingQty);                            
                            }
                        }
                        //  dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", (tTotAmtOffer + (tProcessCurrentQty * Convert.ToDouble(tCurrentRate))));
                        double tOfferFinalAmt = (tTotAmtOffer + (tProcessCurrentQty * Convert.ToDouble(tCurrentRate)));
                        dt.Rows[mnk1]["Amt"] = string.Format("{0:0.00}", (tCurrentQty * tCurrentRate));
                        dt.Rows[mnk1]["SDisc"] = string.Format("{0:0.00}", (tCurrentQty * tCurrentRate) - tOfferFinalAmt);
                        tSpecialDiscount += (tCurrentQty * tCurrentRate) - tOfferFinalAmt;
                    }
                    // Single -Same Free Code
                    else
                        if (dtOfferSameFreeRow.Length > 0)
                        {
                            tProcessCurrentQty = tCurrentQty;
                            tTotAmtOffer = 0.00;
                            dtOfferDetails.Rows.Clear();
                            for (int mn = 0; mn < dtOfferSameFreeRow.Length; mn++)
                            {
                                tOfferQty = Convert.ToDouble(dtOfferSameFreeRow[mn]["TotSaleQty"]);
                                tFreeQty = Convert.ToDouble(dtOfferSameFreeRow[mn]["FreeQty"]);
                                tOfferName = Convert.ToString(dtOfferSameFreeRow[mn]["OfferName"]);
                                if (tOfferQty <= tProcessCurrentQty)
                                {
                                    bool isChkExist = false;
                                    // dtFreeBalance.Rows.Clear();
                                    while ((tOfferQty + tFreeQty) <= tProcessCurrentQty)
                                    {

                                        isChkExist = true;
                                        tOfferQtyCount = (int)(tProcessCurrentQty / (tOfferQty + tFreeQty));
                                        tRemainingQty = tProcessCurrentQty - (tOfferQtyCount * (tOfferQty + tFreeQty));
                                        tProcessCurrentQty = tRemainingQty;
                                        tTotAmtOffer += (tOfferQtyCount * tOfferQty) * Convert.ToDouble(tCurrentRate);
                                        dtOfferDetails.Rows.Add(tClickedButton, tOfferName, tOfferQtyCount, tOfferRate, tOfferQty, (tOfferQtyCount * tOfferQty), (tOfferQtyCount * tOfferRate), tRemainingQty);
                                    }
                                }
                            }
                            if (tProcessCurrentQty >= 0)
                            {
                                // dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", (tTotAmtOffer + (tProcessCurrentQty * Convert.ToDouble(tCurrentRate))));
                                double tOfferFinalAmt = (tTotAmtOffer + (tProcessCurrentQty * Convert.ToDouble(tCurrentRate)));
                                tSpecialDiscount += (tCurrentQty * tCurrentRate) - tOfferFinalAmt;
                                dt.Rows[mnk1]["SDisc"] = string.Format("{0:0.00}", (tCurrentQty * tCurrentRate) - tOfferFinalAmt);
                            }
                            else
                            {
                                // dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", tTotAmtOffer);
                                double tOfferFinalAmt = tTotAmtOffer;
                                tSpecialDiscount += (tCurrentQty * tCurrentRate) - tOfferFinalAmt;
                                dt.Rows[mnk1]["SDisc"] = string.Format("{0:0.00}", (tCurrentQty * tCurrentRate) - tOfferFinalAmt);
                            }
                            dt.Rows[mnk1]["Amt"] = string.Format("{0:0.00}", (tCurrentQty * tCurrentRate));
                        }
                        //Single - Different Free
                        else if (dtRowSingleFree.Length > 0)
                        {
                            tSpecialDiscountSingleFree = 0;
                            DataView view = new DataView(dtSingleAllFreeItem);
                            DataTable distinctValues = view.ToTable(true, "Item_name");

                            dtFreeBalance.Rows.Clear();
                            for (int ijk = 0; ijk < distinctValues.Rows.Count; ijk++)
                            {
                                string tItemNameSingle = Convert.ToString(distinctValues.Rows[ijk]["Item_name"]);
                                tItemNameSingle = (tItemNameSingle.IndexOf("'") == -1) ? tItemNameSingle : tItemNameSingle.Replace("'", "''");
                                DataRow[] dtRowChk = dt.Select("ItemName='" + tItemNameSingle + "'");
                                if (dtRowChk.Length > 0)
                                {
                                    double tItemQtyTemp = Convert.ToDouble(Convert.ToString(dtRowChk[0]["Qty"]));
                                    tProcessCurrentQty = tItemQtyTemp;
                                    DataRow[] dtRowSingleFreeTemp = dtSingleAllFreeItem.Select("Item_Name='" + tItemNameSingle + "'", "TotSaleQty DESC");
                                    for (int mn = 0; mn < dtRowSingleFreeTemp.Length; )
                                    {
                                        tOfferQty = Convert.ToDouble(dtRowSingleFreeTemp[mn]["TotSaleQty"]);
                                        tFreeQty = Convert.ToDouble(dtRowSingleFreeTemp[mn]["FreeQty"]);
                                        tOfferName = Convert.ToString(dtRowSingleFreeTemp[mn]["OfferName"]);
                                        if (tOfferQty <= tProcessCurrentQty)
                                        {
                                            while (tOfferQty <= tProcessCurrentQty)
                                            {
                                                tOfferQtyCount = (int)(tProcessCurrentQty / tOfferQty);
                                                tRemainingQty = tProcessCurrentQty - (tOfferQtyCount * tOfferQty);
                                                tProcessCurrentQty = tRemainingQty;

                                                string tOfferNameNew = Convert.ToString(tOfferName);
                                                tOfferNameNew = (tOfferNameNew.IndexOf("'") == -1) ? tOfferNameNew : tOfferNameNew.Replace("'", "''");
                                                DataRow[] dtOfferFreeItemsList = dtSingleAllFreeItem.Select("OfferName='" + tOfferNameNew + "'", "TotSaleQty DESC");
                                                //free item Details load Datatable
                                                for (int z = 0; z < dtOfferFreeItemsList.Length; z++)
                                                {
                                                    dtFreeBalance.Rows.Add(Convert.ToString(dtOfferFreeItemsList[z]["FreeItem_Name"]), (tOfferQtyCount * Convert.ToDouble(dtOfferFreeItemsList[z]["FreeQty"])));
                                                }

                                            }

                                        }
                                        break;
                                    }
                                }
                            }
                            Dictionary<string, double> dicSum1 = new Dictionary<string, double>();
                            foreach (DataRow row in dtFreeBalance.Rows)
                            {
                                string group = row["ItemName"].ToString();
                                double Qty = (string.IsNullOrEmpty(Convert.ToString(row["Qty"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Qty"]));
                                if (dicSum1.ContainsKey(group))
                                    dicSum1[group] += Qty;
                                else
                                    dicSum1.Add(group, Qty);
                            }
                            tempdtSingleFree.Rows.Clear();
                            foreach (string g in dicSum1.Keys)
                            {
                                tempdtSingleFree.Rows.Add(g, dicSum1[g]);
                            }
                            for (int k = 0; k < tempdtSingleFree.Rows.Count; k++)
                            {
                                for (int g = 0; g < dt.Rows.Count; g++)
                                {
                                    if (Convert.ToString(dt.Rows[g]["ItemName"]) == Convert.ToString(tempdtSingleFree.Rows[k]["ItemName"]))
                                    {
                                        double tChkCurrentQty = Convert.ToDouble(dt.Rows[g]["Qty"]);
                                        double tChkFreeQty = Convert.ToDouble(tempdtSingleFree.Rows[k]["Qty"]);
                                        if (tChkCurrentQty >= tChkFreeQty)
                                        {
                                            tSpecialDiscountSingleFree += (tChkFreeQty * Convert.ToDouble(dt.Rows[g]["Rate"]));
                                            dt.Rows[g]["SDisc"] = string.Format("{0:0.00}", (tChkFreeQty * Convert.ToDouble(dt.Rows[g]["Rate"])));
                                        }
                                        else
                                        {
                                            tSpecialDiscountSingleFree += (tChkCurrentQty * Convert.ToDouble(dt.Rows[g]["Rate"]));
                                            dt.Rows[g]["SDisc"] = string.Format("{0:0.00}", (tChkCurrentQty * Convert.ToDouble(dt.Rows[g]["Rate"])));
                                        }

                                    }
                                }
                            }
                        }
                        else
                        {
                            //dt.Rows[mnk1]["Amt"] = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dt.Rows[mnk1]["Qty"])) * Convert.ToDouble(Convert.ToString(dt.Rows[mnk1]["Rate"])));
                        }

                }
                //Different- Price Code- Start
                DataTable dtDifferentTemp = new DataTable();
                if (dtDifferentTemp.Columns.Count == 0)
                {
                    dtDifferentTemp.Columns.Add("ItemName", typeof(string));
                    dtDifferentTemp.Columns.Add("Qty", typeof(string));
                    dtDifferentTemp.Columns.Add("Rate", typeof(string));
                    dtDifferentTemp.Columns.Add("Amt", typeof(string));
                    dtDifferentTemp.Columns.Add("Disc", typeof(string));
                    dtDifferentTemp.Columns.Add("OfferCount", typeof(string));
                    dtDifferentTemp.Columns.Add("OfferQty", typeof(string));
                    dtDifferentTemp.Columns.Add("OfferRate", typeof(string));
                    dtDifferentTemp.Columns.Add("OfferAmt", typeof(string));
                    dtDifferentTemp.Columns.Add("BalanceQty", typeof(string));
                    dtDifferentTemp.Columns.Add("GroupType", typeof(string));
                    dtDifferentTemp.Columns.Add("TempRate", typeof(string));

                }
                double tDiscountPrice = 0;
                dtDifferentTemp.Rows.Clear();
                for (int mnk1 = 0; mnk1 < dt.Rows.Count; mnk1++)
                {
                    double tCurrentQty = Convert.ToDouble(dt.Rows[mnk1]["Qty"]);
                    tProcessCurrentQty = tCurrentQty;
                    string iname = dt.Rows[mnk1]["ItemName"].ToString();
                    int ni = iname.IndexOf("-");
                    if (ni != -1)
                        iname = iname.Substring(0, ni);
                    string tClickedButton = Convert.ToString(iname);
                    double tCurrentRate = Convert.ToDouble(dt.Rows[mnk1]["Rate"]);
                    tClickedButton = (tClickedButton.IndexOf("'") == -1) ? tClickedButton : tClickedButton.Replace("'", "''");
                    DataRow[] dtRowDifferentPrice = dtDifferent.Select("FreeType = 'Price' And ItemType='Different' and Item_name='" + tClickedButton + "'", "TotSaleQty DESC");
                    string tOfferName = "", tFreeItemName = "";
                    tTotAmtOffer = 0;
                    double tOfferQty = 0, tRemainingQty = 0, tOfferQtyCount = 0, tOfferRate = 0.0, tFreeQty = 0.0;

                    //Check single item meet Offer Or Not
                    if (dtRowDifferentPrice.Length > 0)
                    {
                        bool isChkEntry = false;
                        for (int mn = 0; mn < dtRowDifferentPrice.Length; mn++)
                        {
                            tOfferQty = Convert.ToDouble(dtRowDifferentPrice[mn]["TotSaleQty"]);
                            tOfferRate = Convert.ToDouble(dtRowDifferentPrice[mn]["TotSalePrice"]);
                            tOfferName = Convert.ToString(dtRowDifferentPrice[mn]["OfferName"]);
                            while (tOfferQty <= tProcessCurrentQty)
                            {
                                isChkEntry = true;
                                tOfferQtyCount = (int)(tProcessCurrentQty / tOfferQty);
                                tRemainingQty = tProcessCurrentQty - (tOfferQtyCount * tOfferQty);
                                tProcessCurrentQty = tRemainingQty;
                                tTotAmtOffer = (tOfferQtyCount * tOfferRate);
                                // If Meet load data with Single Keyword
                                dtDifferentTemp.Rows.Add(Convert.ToString(dt.Rows[mnk1]["ItemName"]), Convert.ToString(dt.Rows[mnk1]["Qty"]), Convert.ToString(dt.Rows[mnk1]["Rate"]), Convert.ToString(dt.Rows[mnk1]["Amt"]), Convert.ToString(dt.Rows[mnk1]["Disc"]), tOfferQtyCount, tOfferQty, tOfferRate, tTotAmtOffer, 0, "Single", "0");
                            }
                            if (dtRowDifferentPrice.Length - 1 == mn && tRemainingQty > 0)
                            {
                                dtDifferentTemp.Rows.Add(Convert.ToString(dt.Rows[mnk1]["ItemName"]), Convert.ToString(dt.Rows[mnk1]["Qty"]), Convert.ToString(dt.Rows[mnk1]["Rate"]), Convert.ToString(dt.Rows[mnk1]["Amt"]), Convert.ToString(dt.Rows[mnk1]["Disc"]), "0", "0", "0", "0", tRemainingQty, "", "0");
                            }
                        }
                        if (isChkEntry == false)
                        {
                            dtDifferentTemp.Rows.Add(Convert.ToString(dt.Rows[mnk1]["ItemName"]), Convert.ToString(dt.Rows[mnk1]["Qty"]), Convert.ToString(dt.Rows[mnk1]["Rate"]), Convert.ToString(dt.Rows[mnk1]["Amt"]), Convert.ToString(dt.Rows[mnk1]["Disc"]), "0", "0", "0", "0", Convert.ToString(dt.Rows[mnk1]["Qty"]), "", "0");
                        }

                    }
                }
                double tBalanceQty = 0, tOfferCountTemp = 0, tOfferQtyTemp = 0, tOfferAmtTemp = 0, tItemRateTemp = 0, tProcessRate = 0;
                for (int k = 0; k < dtDifferentTemp.Rows.Count; k++)
                {
                    tBalanceQty = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[k]["BalanceQty"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[k]["BalanceQty"]));
                    tProcessRate = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[k]["Rate"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[k]["Rate"]));
                    //tOfferCountTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[k]["OfferCount"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[k]["OfferCount"]));
                    //tOfferQtyTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[k]["OfferQty"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[k]["OfferQty"]));
                    //tOfferAmtTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[k]["OfferAmt"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[k]["OfferAmt"]));
                    //tItemRateTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[k]["Rate"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[k]["Rate"]));
                    // Next Check if grouping item meet Offer or not
                    if (tBalanceQty > 0)
                    {

                        for (int q = 0; q < dtDifferentTemp.Rows.Count; q++)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[q]["GroupType"])))
                            {
                                tProcessCurrentQty = Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[q]["BalanceQty"]));
                                tProcessRate = Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[q]["Rate"]));
                                double tBaseItemRate = Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[q]["Rate"]));
                                double tBaseItemQty = Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[q]["BalanceQty"]));
                                string tItemName = Convert.ToString(dtDifferentTemp.Rows[q]["ItemName"]);
                                string tBaseItem = Convert.ToString(dtDifferentTemp.Rows[q]["ItemName"]);
                                tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");
                                DataRow[] dtRowDifferentPrice = dtDifferent.Select("FreeType = 'Price' And ItemType='Different' and Item_name='" + tItemName + "'", "TotSaleQty DESC");
                                if (dtRowDifferentPrice.Length > 0)
                                {
                                    string tOfferName = "";
                                    double tOfferQty = 0, tRemainingQty = 0, tOfferQtyCount = 0, tOfferRate = 0.0;
                                    //bool isChkOfferNew = false;
                                    for (int t = 0; t < dtRowDifferentPrice.Length; t++)
                                    {
                                        tProcessCurrentQty = tBaseItemQty;
                                        tProcessRate = tBaseItemQty * tBaseItemRate;
                                        tOfferQty = Convert.ToDouble(dtRowDifferentPrice[t]["TotSaleQty"]);
                                        tOfferRate = Convert.ToDouble(dtRowDifferentPrice[t]["TotSalePrice"]);
                                        tOfferName = Convert.ToString(dtRowDifferentPrice[t]["OfferName"]);
                                        string tOfferNameNew = Convert.ToString(dtRowDifferentPrice[t]["OfferName"]);
                                        tOfferNameNew = (tOfferNameNew.IndexOf("'") == -1) ? tOfferNameNew : tOfferNameNew.Replace("'", "''");
                                        DataRow[] dtDiffOfferItemRow = dtDifferent.Select("FreeType = 'Price' And ItemType='Different' and Item_name<>'" + tItemName + "' and Offername='" + tOfferNameNew + "'", "TotSaleQty DESC");


                                        System.Collections.Hashtable hTable = new System.Collections.Hashtable();
                                        System.Collections.ArrayList duplicateList = new System.Collections.ArrayList();

                                        //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                                        //And add duplicate item value in arraylist.
                                        foreach (DataRow drow in dtDiffOfferItemRow)
                                        {
                                            if (hTable.Contains(drow["Item_Name"]))
                                            {
                                                //duplicateList.Add(drow);
                                            }
                                            else
                                            {
                                                hTable.Add(drow["Item_Name"], string.Empty);
                                                duplicateList.Add(drow["Item_Name"]);
                                            }
                                        }
                                        for (int ij = 0; ij < duplicateList.Count; ij++)
                                        {
                                            for (int jk = 0; jk < dtDifferentTemp.Rows.Count; jk++)
                                            {
                                                if (Convert.ToString(duplicateList[ij]) == Convert.ToString(dtDifferentTemp.Rows[jk]["ItemName"]) && Convert.ToString(duplicateList[ij]) != Convert.ToString(tBaseItem) && string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[jk]["GroupType"]))) // tClickedButton)
                                                {
                                                    tProcessCurrentQty += Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[jk]["BalanceQty"]));

                                                    break;
                                                }
                                            }
                                        }
                                        //tProcessRate = tProcessRate / tProcessCurrentQty;
                                        double tAllocatedQty = 0;
                                        tAllocatedQty = tBaseItemQty;

                                        while (tOfferQty <= tProcessCurrentQty)
                                        {

                                            // isChkOfferNew = true;
                                            tOfferQtyCount = (int)(tProcessCurrentQty / tOfferQty);
                                            tRemainingQty = tProcessCurrentQty - (tOfferQtyCount * tOfferQty);
                                            tProcessCurrentQty = tRemainingQty;
                                            tTotAmtOffer = (tOfferQtyCount * tOfferRate);

                                            dtDifferentTemp.Rows[q]["OfferCount"] = Convert.ToString(tOfferQtyCount);
                                            dtDifferentTemp.Rows[q]["OfferQty"] = Convert.ToString(tOfferQty);
                                            dtDifferentTemp.Rows[q]["OfferRate"] = Convert.ToString(tOfferRate);
                                            dtDifferentTemp.Rows[q]["OfferAmt"] = Convert.ToString(tTotAmtOffer);
                                            dtDifferentTemp.Rows[q]["BalanceQty"] = "0";
                                            dtDifferentTemp.Rows[q]["GroupType"] = "Merge";
                                            double tProcessQtyNew = tBaseItemQty;
                                            for (int ij1 = 0; ij1 < duplicateList.Count; ij1++)
                                            {
                                                for (int jk1 = 0; jk1 < dtDifferentTemp.Rows.Count; jk1++)
                                                {
                                                    if (Convert.ToString(duplicateList[ij1]) == Convert.ToString(dtDifferentTemp.Rows[jk1]["ItemName"]) && Convert.ToString(duplicateList[ij1]) != Convert.ToString(tBaseItem) && string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[jk1]["GroupType"]))) // tClickedButton)
                                                    {
                                                        tAllocatedQty += Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[jk1]["BalanceQty"]));
                                                        if (tAllocatedQty <= (tOfferQtyCount * tOfferQty))
                                                        {
                                                            tProcessQtyNew = tAllocatedQty;
                                                            tProcessRate += Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[jk1]["BalanceQty"])) * Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[jk1]["Rate"]));
                                                            dtDifferentTemp.Rows[jk1]["BalanceQty"] = "0";
                                                            dtDifferentTemp.Rows[jk1]["GroupType"] = "Merge";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            dtDifferentTemp.Rows[jk1]["BalanceQty"] = Convert.ToString(tRemainingQty);
                                                            dtDifferentTemp.Rows[jk1]["GroupType"] = "Merge";
                                                            //  dtDifferentTemp.Rows.Add(Convert.ToString(dtDifferentTemp.Rows[jk1]["ItemName"]), Convert.ToString(dtDifferentTemp.Rows[jk1]["Qty"]), Convert.ToString(dtDifferentTemp.Rows[jk1]["Rate"]), Convert.ToString(dtDifferentTemp.Rows[jk1]["Amt"]), Convert.ToString(dtDifferentTemp.Rows[jk1]["Disc"]), "0", "0", "0", "0", tRemainingQty, "","0");
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            tProcessRate = tProcessRate / tProcessQtyNew;
                                            dtDifferentTemp.Rows[q]["TempRate"] = Convert.ToString(tProcessRate);
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                for (int disc = 0; disc < dtDifferentTemp.Rows.Count; disc++)
                {
                    // Final Calculate Disc Amount
                    tBalanceQty = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[disc]["BalanceQty"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[disc]["BalanceQty"]));
                    tOfferCountTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[disc]["OfferCount"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[disc]["OfferCount"]));
                    tOfferQtyTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[disc]["OfferQty"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[disc]["OfferQty"]));
                    tOfferAmtTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[disc]["OfferAmt"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[disc]["OfferAmt"]));


                    if (tBalanceQty == 0)
                    {
                        if (Convert.ToString(dtDifferentTemp.Rows[disc]["GroupType"]) == "Merge")
                        {
                            tItemRateTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[disc]["TempRate"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[disc]["TempRate"]));
                            tSpecialDiscount += ((tOfferCountTemp * tOfferQtyTemp) * tItemRateTemp) - tOfferAmtTemp;
                            for (int tAssign = 0; tAssign < dt.Rows.Count; tAssign++)
                            {
                                if (Convert.ToString(dt.Rows[tAssign]["ItemName"]) == Convert.ToString(dtDifferentTemp.Rows[disc]["ItemName"]))
                                {
                                    double tPreviousDisc = Convert.ToDouble(Convert.ToString(dt.Rows[tAssign]["SDisc"]));
                                    double tCurrentDisc = ((tOfferCountTemp * tOfferQtyTemp) * tItemRateTemp) - tOfferAmtTemp;
                                    dt.Rows[tAssign]["SDisc"] = string.Format("{0:0.00}", tPreviousDisc + tCurrentDisc);
                                }
                            }
                        }
                        else
                        {
                            tItemRateTemp = string.IsNullOrEmpty(Convert.ToString(dtDifferentTemp.Rows[disc]["Rate"])) ? 0 : Convert.ToDouble(Convert.ToString(dtDifferentTemp.Rows[disc]["Rate"]));
                            double tFinalAmtCal = ((tOfferCountTemp * tOfferQtyTemp) * tItemRateTemp) - tOfferAmtTemp;
                            tSpecialDiscount += (tFinalAmtCal < 0) ? 0 : tFinalAmtCal;
                            for (int tAssign = 0; tAssign < dt.Rows.Count; tAssign++)
                            {
                                if (Convert.ToString(dt.Rows[tAssign]["ItemName"]) == Convert.ToString(dtDifferentTemp.Rows[disc]["ItemName"]))
                                {
                                    double tPreviousDisc = Convert.ToDouble(Convert.ToString(dt.Rows[tAssign]["SDisc"]));
                                    double tCurrentDisc = (tFinalAmtCal < 0) ? 0 : tFinalAmtCal;
                                    dt.Rows[tAssign]["SDisc"] = string.Format("{0:0.00}", tPreviousDisc + tCurrentDisc);
                                }
                            }
                        }
                    }
                }
                //Different- Price Code- End
                lblSpecialDiscAmt.Content = string.Format("{0:0.00}", (tSpecialDiscount + tSpecialDiscountSingleFree) < 0 ? 0 : (tSpecialDiscount + tSpecialDiscountSingleFree));
                //funReplaceFreeItemAmt();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }


        public void funBtnEnterTextBox()
        {
            try
            {

                DataTable DTM = new DataTable();
                List<string> lstring = new List<string>();
                List<string> rstring = new List<string>();
                //SqlCommand comm = new SqlCommand("select Item_no from  serialno_transtbl where inout=1 and barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name Like '" + txtEnterValue.Text.Trim() + "' OR Item_Code Like '" + txtEnterValue.Text.Trim() + "')", con);
                //SqlCommand comm = new SqlCommand("select Item_no from  serialno_transtbl where inout=1 and barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name Like '%" + txtEnterValue.Text.Trim() + "%' OR Item_Code Like '" + txtEnterValue.Text.Trim() + "')", con);
                //listSelect.SelectedItem.ToString() 
                SqlCommand comm = new SqlCommand("select Item_no from  serialno_transtbl where inout=1 and barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name Like '" + listSelect.SelectedItem.ToString().Trim() + "' OR Item_Code Like '" + txtEnterValue.Text.Trim() + "')", con);
                SqlDataAdapter adap = new SqlDataAdapter(comm);
                adap.Fill(DTM);
                if (DTM.Rows.Count != 0)
                {
                    listSelect.Items.Clear();
                    if (_Class.clsVariables.dtserailno.Rows.Count == 0)
                    {
                        for (int mn = 0; mn < DTM.Rows.Count; mn++)
                        {
                            listSelect.Items.Add(DTM.Rows[mn]["Item_no"].ToString());
                        }
                    }
                    else
                    {
                        int t = 0;
                        for (int mn = 0; mn < DTM.Rows.Count; mn++)
                        {
                            for (int j = 0; j < _Class.clsVariables.dtserailno.Rows.Count; j++)
                            {
                                if (DTM.Rows[mn]["Item_no"].ToString() != _Class.clsVariables.dtserailno.Rows[j]["Serial_no"].ToString())
                                {
                                    t = 1;
                                }
                                else
                                {
                                    DataRow dr = DTM.Rows[mn];
                                    dr.Delete();
                                    DTM.AcceptChanges();
                                    //break;
                                }
                            }
                        }
                        for (int i = 0; i < DTM.Rows.Count; i++)
                            listSelect.Items.Add(DTM.Rows[i]["Item_no"].ToString());
                    }
                }
                else
                {

                    var bc = new BrushConverter();
                    lblLogo.Foreground = (Brush)bc.ConvertFrom("#FFADF213");
                    funConnectionStateCheck();
                    DataRow dr = null;

                    // Check item Selected or not
                    if (listSelect.SelectedItems.Count > 0 || txtEnterValue.Text.Length > 0)
                    {

                        // DataRow dr = null;
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();

                        // Find '*' Exist or not
                        tempFindStar = txtEnterValue.Text.IndexOf("*");
                        // '*' not in name load below code
                        if (tempFindStar == -1)
                        {
                            //Below code Same as newBtnGroupItem Click.. Refer Button Clicke Event
                            DataTable dtBarcode = new DataTable();
                            dtBarcode.Rows.Clear();
                            SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where BarCode=@tBarCode", con);
                            cmdBarcode.Parameters.AddWithValue("@tBarCode", txtEnterValue.Text.Trim());

                            SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                            adpBarcode.Fill(dtBarcode);
                            //Check keyed word exist in Barcode.
                            if (dtBarcode.Rows.Count > 0)
                            {
                                DataTable dtItem1 = new DataTable();
                                dtItem1.Rows.Clear();
                                SqlCommand cmdItemNew = new SqlCommand("Select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_no=@tItemNo", con);
                                cmdItemNew.Parameters.AddWithValue("@tItemNo", dtBarcode.Rows[0]["Item_no"].ToString());
                                SqlDataAdapter adpCmdNew = new SqlDataAdapter(cmdItemNew);
                                adpCmdNew.Fill(dtItem1);
                                if (dtItem1.Rows.Count > 0)
                                {
                                    funConnectionStateCheck();
                                    DataTable dtNew1 = new DataTable();

                                    //   SqlDataReader dr12 = null;
                                    dtNew1.Rows.Clear();

                                    SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@tValue", dtItem1.Rows[0]["Item_Name"].ToString());

                                    cmd.Parameters.AddWithValue("@tActionType", "TXTBOXVALUE");
                                    SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                                    adpCmd.Fill(dtNew1);
                                    //  dr12 = cmd.ExecuteReader();
                                    // dtNew1.Load(dr12);
                                    int isRecord = 0;
                                    for (int mn = 0; mn < dtNew1.Rows.Count; )
                                    {
                                        isRecord = 1;
                                        rowIndex = 0;
                                        dr = dt.NewRow();
                                        //dtserial.Rows.Add(dtNew1.Rows[mn]["Item_Name"].ToString());
                                        _Class.clsVariables.dtserailno.Rows.Add(dtNew1.Rows[mn]["Item_Name"].ToString());
                                        //   MessageBox.Show(dr12["Item_Name"].ToString());                                        
                                        SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@tValue", dtNew1.Rows[mn]["Item_Name"].ToString());
                                        cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
                                        SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                        adpCmd1.Fill(dtNew);
                                        // reader = cmd1.ExecuteReader();
                                        // dtNew.Load(reader);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            count = 0;
                                            totAmt = 0.00;
                                            totQty = 0.00;
                                            totTax = 0.00;
                                            string tempItemName = dtNew.Rows[mn]["Item_Name"].ToString();
                                            tItemNameGlob = tempItemName;
                                            double tUnitDecimals = double.Parse(dtNew.Rows[mn]["unit_Decimals"].ToString());
                                            string tWeightScale = dtNew.Rows[mn]["WeightScale"].ToString();
                                            double tReadingValue = 0;


                                            DataTable dtItem = new DataTable();
                                            dtItem.Rows.Clear();
                                            SqlCommand cmd12 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                            cmd12.Parameters.AddWithValue("@tItemName", tempItemName);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd12);
                                            adp.Fill(dtItem);
                                            bool isChkOpenItem = false;
                                            bool isChkStopAtRate = false;
                                            //bool isChkStopAtQty = false;
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());
                                                // isChkStopAtQty = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());

                                                if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                {
                                                    isChkOpenItem = true;
                                                }
                                            }

                                            if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                                            {
                                                if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                                                {
                                                ReadAgain:
                                                    try
                                                    {
                                                        tReadCount = 0;
                                                        string data = "";
                                                        data = _Class.clsVariables.serial.ReadExisting();
                                                        //serial.Close();
                                                        if (data.IndexOf("kg") > 0)
                                                        {
                                                            data = data.Substring(0, data.IndexOf("kg"));
                                                            data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                            // if
                                                            tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                        }
                                                        else if (data.IndexOf("k") > 0)
                                                        {
                                                            data = data.Substring(0, data.IndexOf("k"));
                                                            data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                            tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        tReadCount++;
                                                        if (tReadCount < 10)
                                                        {
                                                            goto ReadAgain;
                                                        }
                                                        else
                                                        {
                                                            tShowQty = "";
                                                            MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                                            tShowQty = "Show";

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (isChkStopAtRate == true)
                                                    {
                                                        tReadingValue = 0;
                                                    }
                                                    else
                                                    {
                                                        tReadingValue = 1;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (isChkStopAtRate == true)
                                                {
                                                    tReadingValue = 0;
                                                }
                                                else
                                                {
                                                    tReadingValue = 1;
                                                }
                                            }

                                            foreach (DataRow dr1 in dt.Rows)
                                            {
                                                if (dr1["itemName"].ToString() == tempItemName)
                                                {
                                                    if (isChkOpenItem != true)
                                                    {
                                                        count = 1;
                                                        if (tUnitDecimals == 0)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                                        }
                                                        if (tUnitDecimals == 1)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                                        }
                                                        if (tUnitDecimals == 2)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                                        }
                                                        if (tUnitDecimals == 3)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                                        }
                                                        if (tUnitDecimals == 4)
                                                        {
                                                            dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                                        }

                                                        {
                                                            dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                        }
                                                        gridItems.Rows[rowIndex].Selected = true;
                                                        rowSelect = "";
                                                        if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                                                        {
                                                            if (tReadingValue > 0)
                                                            {
                                                                //   tempTimer.Start();

                                                            }
                                                        }
                                                    }
                                                }
                                                rowIndex += 1;
                                            }
                                            if (count == 0)
                                            {
                                                dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString();
                                                if (tUnitDecimals == 0)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N0");
                                                }
                                                if (tUnitDecimals == 1)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N1");
                                                }
                                                if (tUnitDecimals == 2)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N2");
                                                }
                                                if (tUnitDecimals == 3)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N3");
                                                }
                                                if (tUnitDecimals == 4)
                                                {
                                                    dr["Qty"] = tReadingValue.ToString("N4");
                                                }
                                                // dr["Qty"] = "1";
                                                dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));



                                                {
                                                    dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                                }
                                                dt.Rows.Add(dr);
                                                //   funReplaceFreeItemAmt();
                                                tSelectedRowIndex = dt.Rows.Count;
                                                rowSelect = "Last";

                                                tReadingValueDisplay = tReadingValue;
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dr["Qty"]);
                                                drRateDisplay = Convert.ToString(dr["Rate"]);
                                                drAmtDisplay = Convert.ToString(dr["Amt"]);


                                            }
                                            funStockDisplay(tempItemName);
                                            funDisplayAmount(dt);
                                            if (rowSelect != "")
                                            {
                                                gridItems.DataSource = dt.DefaultView;   // Change gridItems.ItemsSource = dt.DefaultView;
                                                gridItems.Columns[0].Width = 180;
                                                gridItems.Columns[0].ReadOnly = true;
                                                gridItems.Columns[1].Width = 50;
                                                gridItems.Columns[2].Width = 50;
                                                gridItems.Columns[3].Width = 50;
                                                gridItems.Columns[3].ReadOnly = true;
                                                gridItems.RowTemplate.Height = 35;
                                            }
                                            gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                                            funScrollGrid();
                                            funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                            funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                            funRoundCalculate();
                                        }
                                        break;
                                    }
                                    if (isRecord == 0)
                                    {
                                        MyMessageBox.ShowBox("Product Not Found", "Warning");
                                    }
                                    txtEnterValue.Text = "";
                                    txtEnterValue.Focus();
                                }
                            }
                            else
                            {
                                //check keyed text in Itemcode or ItemName
                                funConnectionStateCheck();
                                DataTable dtNew1 = new DataTable();

                                //   SqlDataReader dr12 = null;
                                dtNew1.Rows.Clear();

                                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (listSelect.IsVisible == true)
                                {
                                    if (listSelect.SelectedItems.Count > 0)
                                    {
                                        cmd.Parameters.AddWithValue("@tValue", listSelect.SelectedItem.ToString());
                                    }
                                    else
                                    {
                                        // listSelect.SelectedIndex = 0;
                                        // cmd.Parameters.AddWithValue("@tValue", listSelect.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                                    }
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                                }
                                cmd.Parameters.AddWithValue("@tActionType", "TXTBOXVALUE");
                                SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                                adpCmd.Fill(dtNew1);
                                //  dr12 = cmd.ExecuteReader();
                                // dtNew1.Load(dr12);
                                int isRecord = 0;
                                for (int mn = 0; mn < dtNew1.Rows.Count; )
                                {
                                    isRecord = 1;
                                    rowIndex = 0;
                                    dr = dt.NewRow();
                                    //   MessageBox.Show(dr12["Item_Name"].ToString());
                                    SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@tValue", dtNew1.Rows[mn]["Item_Name"].ToString());
                                    cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
                                    SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                    adpCmd1.Fill(dtNew);
                                    // reader = cmd1.ExecuteReader();
                                    // dtNew.Load(reader);
                                    if (dtNew.Rows.Count > 0)
                                    {
                                        count = 0;
                                        totAmt = 0.00;
                                        totQty = 0.00;
                                        totTax = 0.00;
                                        string tempItemName = dtNew.Rows[mn]["Item_Name"].ToString();
                                        tItemNameGlob = tempItemName;
                                        double tUnitDecimals = double.Parse(dtNew.Rows[mn]["unit_Decimals"].ToString());
                                        string tWeightScale = dtNew.Rows[mn]["WeightScale"].ToString();
                                        double tReadingValue = 0;


                                        DataTable dtItem = new DataTable();
                                        dtItem.Rows.Clear();
                                        SqlCommand cmd12 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                        cmd12.Parameters.AddWithValue("@tItemName", tempItemName);
                                        SqlDataAdapter adp = new SqlDataAdapter(cmd12);
                                        adp.Fill(dtItem);
                                        bool isChkOpenItem = false;
                                        bool isChkStopAtRate = false;
                                        //bool isChkStopAtQty = false;
                                        if (dtItem.Rows.Count > 0)
                                        {
                                            isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());
                                            // isChkStopAtQty = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());

                                            if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                            {
                                                isChkOpenItem = true;
                                            }
                                        }

                                        if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                                        {
                                            if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                                            {
                                            ReadAgain:
                                                try
                                                {
                                                    tReadCount = 0;
                                                    string data = "";
                                                    data = _Class.clsVariables.serial.ReadExisting();
                                                    //serial.Close();
                                                    if (data.IndexOf("kg") > 0)
                                                    {
                                                        data = data.Substring(0, data.IndexOf("kg"));
                                                        data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                        // if
                                                        tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                    }
                                                    else if (data.IndexOf("k") > 0)
                                                    {
                                                        data = data.Substring(0, data.IndexOf("k"));
                                                        data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                        tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    tReadCount++;
                                                    if (tReadCount < 10)
                                                    {
                                                        goto ReadAgain;
                                                    }
                                                    else
                                                    {
                                                        tShowQty = "";
                                                        MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                                        tShowQty = "Show";

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (isChkStopAtRate == true)
                                                {
                                                    tReadingValue = 0;
                                                }
                                                else
                                                {
                                                    tReadingValue = 1;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isChkStopAtRate == true)
                                            {
                                                tReadingValue = 0;
                                            }
                                            else
                                            {
                                                tReadingValue = 1;
                                            }
                                        }

                                        foreach (DataRow dr1 in dt.Rows)
                                        {
                                            if (dr1["itemName"].ToString() == tempItemName)
                                            {
                                                if (isChkOpenItem != true)
                                                {
                                                    count = 1;
                                                    if (tUnitDecimals == 0)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                                    }
                                                    if (tUnitDecimals == 1)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                                    }
                                                    if (tUnitDecimals == 2)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                                    }
                                                    if (tUnitDecimals == 3)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                                    }
                                                    if (tUnitDecimals == 4)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                                    }

                                                    {
                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                    }
                                                    gridItems.Rows[rowIndex].Selected = true;
                                                    rowSelect = "";

                                                    tReadingValueDisplay = tReadingValue;
                                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                    drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                    drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                    drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);

                                                }
                                            }
                                            rowIndex += 1;
                                        }
                                        if (count == 0)
                                        {
                                            dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString();
                                            if (tUnitDecimals == 0)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N0");
                                            }
                                            if (tUnitDecimals == 1)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N1");
                                            }
                                            if (tUnitDecimals == 2)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N2");
                                            }
                                            if (tUnitDecimals == 3)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N3");
                                            }
                                            if (tUnitDecimals == 4)
                                            {
                                                dr["Qty"] = tReadingValue.ToString("N4");
                                            }
                                            // dr["Qty"] = "1";
                                            dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                            {
                                                dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                            }
                                            if (dr["Disc"].ToString().Trim() == "")
                                            {
                                                dr["Disc"] = "0.00";
                                            }
                                            dt.Rows.Add(dr);
                                            // funReplaceFreeItemAmt();
                                            tSelectedRowIndex = dt.Rows.Count;
                                            rowSelect = "Last";

                                            tReadingValueDisplay = tReadingValue;
                                            ClickedButtonDisplay = Convert.ToString(tempItemName);
                                            drQtyDisplay = Convert.ToString(dr["Qty"]);
                                            drRateDisplay = Convert.ToString(dr["Rate"]);
                                            drAmtDisplay = Convert.ToString(dr["Amt"]);

                                        }
                                        funStockDisplay(tempItemName);

                                        funDisplayAmount(dt);
                                        if (rowSelect != "")
                                        {
                                            gridItems.DataSource = dt.DefaultView;   // Change gridItems.ItemsSource = dt.DefaultView;
                                            gridItems.Columns[0].Width = 180;
                                            gridItems.Columns[0].ReadOnly = true;
                                            gridItems.Columns[1].Width = 50;
                                            gridItems.Columns[2].Width = 50;
                                            gridItems.Columns[3].Width = 50;
                                            gridItems.Columns[3].ReadOnly = true;
                                            gridItems.RowTemplate.Height = 35;
                                        }
                                        gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                                        funScrollGrid();
                                        funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);


                                        funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                        funRoundCalculate();
                                    }
                                    break;
                                }
                                if (isRecord == 0)
                                {
                                    MyMessageBox.ShowBox("Product Not Found", "Warning");
                                }
                                txtEnterValue.Text = "";
                                txtEnterValue.Focus();
                            }

                        }
                        else
                        {
                            //If '*' Exist executed follow code

                            string tempItemCode = txtEnterValue.Text.Substring(tempFindStar + 1, ((txtEnterValue.Text.Length - 1) - (tempFindStar)));
                            //  MessageBox.Show(txtEnterValue.Text.Substring(tempFindStar+1,((txtEnterValue.Text.Length-1)-(tempFindStar))));
                            string tempQty = txtEnterValue.Text.Substring(0, tempFindStar);
                            //  MessageBox.Show(txtEnterValue.Text.Substring(0, tempFindStar));
                            double num;
                            if (tempQty.Trim() != "" && double.TryParse(tempQty, out num))
                            {
                                if (double.Parse(tempQty) > 0)
                                {
                                    DataTable dtBarcode = new DataTable();
                                    dtBarcode.Rows.Clear();
                                    SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where BarCode=@tBarCode", con);
                                    cmdBarcode.Parameters.AddWithValue("@tBarCode", tempItemCode);

                                    SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                                    adpBarcode.Fill(dtBarcode);

                                    // Check in Barcode or Itemcode
                                    if (dtBarcode.Rows.Count > 0)
                                    {

                                        rowIndex = 0;
                                        // DataRow dr = null;
                                        dr = dt.NewRow();
                                        // MessageBox.Show(ClickedButton.Content.ToString());
                                        SqlCommand cmdItem = new SqlCommand("Select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_no=@tItemNo", con);
                                        cmdItem.Parameters.AddWithValue("@tItemNo", dtBarcode.Rows[0]["Item_no"].ToString());
                                        SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmdItem);
                                        adpCmd1.Fill(dtNew);
                                        //reader = cmd1.ExecuteReader();
                                        //dtNew.Load(reader);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            count = 0;
                                            totAmt = 0.00;
                                            totQty = 0.00;
                                            totTax = 0.00;
                                            string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                            tItemNameGlob = tempItemName;
                                            DataTable dtItem = new DataTable();
                                            dtItem.Rows.Clear();
                                            SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                            cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                            adp.Fill(dtItem);
                                            bool isChkOpenItem = false;
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                {
                                                    isChkOpenItem = true;
                                                }
                                            }
                                            foreach (DataRow dr1 in dt.Rows)
                                            {
                                                if (dr1["itemName"].ToString() == tempItemName)
                                                {
                                                    if (isChkOpenItem != true)
                                                    {
                                                        count = 1;
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                        {
                                                            dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                        }

                                                        tReadingValueDisplay = 1;
                                                        ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                        drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                        drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                        drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                    }
                                                }
                                                rowIndex += 1;


                                            }
                                            if (count == 0)
                                            {
                                                dr["ItemName"] = dtNew.Rows[0]["Item_name"].ToString();
                                                dr["Qty"] = tempQty.ToString();
                                                dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString()));


                                                {
                                                    dr["Amt"] = string.Format("{0:0.00}", (double.Parse(tempQty) * double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString())));
                                                }
                                                dt.Rows.Add(dr);
                                                // funReplaceFreeItemAmt();
                                                tSelectedRowIndex = dt.Rows.Count;

                                                tReadingValueDisplay = 1;
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dr["Qty"]);
                                                drRateDisplay = Convert.ToString(dr["Rate"]);
                                                drAmtDisplay = Convert.ToString(dr["Amt"]);


                                            }
                                            funStockDisplay(tempItemName);
                                            funDisplayAmount(dt);
                                            gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                            gridItems.Columns[0].Width = 180;
                                            gridItems.Columns[0].ReadOnly = true;
                                            gridItems.Columns[1].Width = 50;
                                            gridItems.Columns[2].Width = 50;
                                            gridItems.Columns[3].Width = 50;
                                            gridItems.Columns[3].ReadOnly = true;
                                            gridItems.RowTemplate.Height = 35;
                                            funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                            funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                            funScrollGrid();
                                            funRoundCalculate();
                                        }
                                        else
                                        {
                                            MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                        }
                                    }
                                    else
                                    {
                                        rowIndex = 0;
                                        // DataRow dr = null;
                                        dr = dt.NewRow();
                                        // MessageBox.Show(ClickedButton.Content.ToString());
                                        SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.AddWithValue("@tValue", tempItemCode);
                                        cmd1.Parameters.AddWithValue("@tActionType", "ITEMCODE");
                                        SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                        adpCmd1.Fill(dtNew);
                                        //reader = cmd1.ExecuteReader();
                                        //dtNew.Load(reader);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            count = 0;
                                            totAmt = 0.00;
                                            totQty = 0.00;
                                            totTax = 0.00;
                                            string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                            tItemNameGlob = tempItemName;
                                            DataTable dtItem = new DataTable();
                                            dtItem.Rows.Clear();
                                            SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                            cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                            adp.Fill(dtItem);
                                            bool isChkOpenItem = false;
                                            if (dtItem.Rows.Count > 0)
                                            {
                                                if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                {
                                                    isChkOpenItem = true;
                                                }
                                            }
                                            foreach (DataRow dr1 in dt.Rows)
                                            {
                                                if (dr1["itemName"].ToString() == tempItemName)
                                                {
                                                    if (isChkOpenItem != true)
                                                    {
                                                        count = 1;
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                        {
                                                            dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                        }

                                                        tReadingValueDisplay = 1;
                                                        ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                        drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                        drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                        drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                    }
                                                }
                                                rowIndex += 1;


                                            }
                                            if (count == 0)
                                            {
                                                dr["ItemName"] = dtNew.Rows[0]["Item_name"].ToString();
                                                dr["Qty"] = tempQty.ToString();
                                                dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString()));


                                                {
                                                    dr["Amt"] = string.Format("{0:0.00}", (double.Parse(tempQty) * double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString())));
                                                }
                                                dt.Rows.Add(dr);
                                                // funReplaceFreeItemAmt();
                                                tSelectedRowIndex = dt.Rows.Count;

                                                tReadingValueDisplay = 1;
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dr["Qty"]);
                                                drRateDisplay = Convert.ToString(dr["Rate"]);
                                                drAmtDisplay = Convert.ToString(dr["Amt"]);




                                            }
                                            funStockDisplay(tempItemName);
                                            funDisplayAmount(dt);
                                            gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                            gridItems.Columns[0].Width = 180;
                                            gridItems.Columns[0].ReadOnly = true;
                                            gridItems.Columns[1].Width = 50;
                                            gridItems.Columns[2].Width = 50;
                                            gridItems.Columns[3].Width = 50;
                                            gridItems.Columns[3].ReadOnly = true;
                                            gridItems.RowTemplate.Height = 35;
                                            funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                            funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                            funScrollGrid();
                                            funRoundCalculate();
                                        }
                                        else
                                        {
                                            MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                        }

                                    }
                                    ////}
                                    ////else
                                    ////{
                                    ////    MyMessageBox.ShowBox("Please Enter Valid Quantity","Warning");
                                    ////}
                                }
                                else if (double.Parse(tempQty) < 0)
                                {
                                    // if item Qty in minus sign execute this block
                                    _Class.clsVariables.funControlSetting();
                                    if (_Class.clsVariables.tSetReturnInSales == false)
                                    {
                                        MyMessageBox.ShowBox("Please Enter Valid Quantity", "Warning");
                                    }
                                    else
                                    {
                                        DataTable dtBarcode = new DataTable();
                                        dtBarcode.Rows.Clear();
                                        SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where BarCode=@tBarCode", con);
                                        cmdBarcode.Parameters.AddWithValue("@tBarCode", tempItemCode);

                                        SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                                        adpBarcode.Fill(dtBarcode);
                                        if (dtBarcode.Rows.Count > 0)
                                        {
                                            rowIndex = 0;
                                            // DataRow dr = null;
                                            dr = dt.NewRow();

                                            SqlCommand cmdItem = new SqlCommand("Select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_no=@tItemNo", con);
                                            cmdItem.Parameters.AddWithValue("@tItemNo", dtBarcode.Rows[0]["Item_no"].ToString());
                                            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmdItem);


                                            adpCmd1.Fill(dtNew);
                                            //reader = cmd1.ExecuteReader();
                                            //dtNew.Load(reader);
                                            if (dtNew.Rows.Count > 0)
                                            {
                                                count = 0;
                                                totAmt = 0.00;
                                                totQty = 0.00;
                                                totTax = 0.00;
                                                string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                                tItemNameGlob = tempItemName;
                                                DataTable dtItem = new DataTable();
                                                dtItem.Rows.Clear();
                                                SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                                cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                                adp.Fill(dtItem);
                                                bool isChkOpenItem = false;
                                                if (dtItem.Rows.Count > 0)
                                                {
                                                    if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                    {
                                                        isChkOpenItem = true;
                                                    }
                                                }
                                                bool isMsgDis = true;
                                                foreach (DataRow dr1 in dt.Rows)
                                                {
                                                    if (dr1["itemName"].ToString() == tempItemName)
                                                    {
                                                        string tItemName = Convert.ToString(tempItemName);
                                                        tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");

                                                        DataRow[] dtRemoveChk = _Class.clsVariables.dtSingleFree.Select("MainItemName='" + tItemName + "'");
                                                        if (dtRemoveChk.Length == 0)
                                                        {
                                                            if (isChkOpenItem != true)
                                                            {
                                                                count = 1;
                                                                if ((double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())) > 0)
                                                                {

                                                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                                    {
                                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                                    }

                                                                    tReadingValueDisplay = 1;
                                                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                                    drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                                    drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                                    drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                                }
                                                                else
                                                                {
                                                                    MyMessageBox.ShowBox("Enter Valid Quantity", "Warning");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            isMsgDis = true;
                                                            MyMessageBox.ShowBox("This Item Could not be Remove from the list");
                                                        }

                                                    }
                                                    rowIndex += 1;


                                                }
                                                if (count == 0 && isMsgDis == false)
                                                {
                                                    MyMessageBox.ShowBox("Item not found in the list", "Warning");
                                                }

                                                funStockDisplay(tempItemName);
                                                funDisplayAmount(dt);
                                                gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                                gridItems.Columns[0].Width = 180;
                                                gridItems.Columns[0].ReadOnly = true;
                                                gridItems.Columns[1].Width = 50;
                                                gridItems.Columns[2].Width = 50;
                                                gridItems.Columns[3].Width = 50;
                                                gridItems.Columns[3].ReadOnly = true;
                                                gridItems.RowTemplate.Height = 35;
                                                funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                                funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                                funScrollGrid();
                                                funRoundCalculate();
                                            }
                                            else
                                            {
                                                MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                            }
                                        }
                                        else
                                        {
                                            rowIndex = 0;
                                            // DataRow dr = null;
                                            dr = dt.NewRow();
                                            // MessageBox.Show(ClickedButton.Content.ToString());
                                            SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                            cmd1.CommandType = CommandType.StoredProcedure;
                                            cmd1.Parameters.AddWithValue("@tValue", tempItemCode);
                                            cmd1.Parameters.AddWithValue("@tActionType", "ITEMCODE");
                                            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                            adpCmd1.Fill(dtNew);
                                            //reader = cmd1.ExecuteReader();
                                            //dtNew.Load(reader);
                                            if (dtNew.Rows.Count > 0)
                                            {
                                                count = 0;
                                                totAmt = 0.00;
                                                totQty = 0.00;
                                                totTax = 0.00;
                                                string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                                tItemNameGlob = tempItemName;
                                                DataTable dtItem = new DataTable();
                                                dtItem.Rows.Clear();
                                                SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                                cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                                adp.Fill(dtItem);
                                                bool isChkOpenItem = false;
                                                if (dtItem.Rows.Count > 0)
                                                {
                                                    if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                                    {
                                                        isChkOpenItem = true;
                                                    }
                                                }
                                                bool isMsgDis = false;
                                                foreach (DataRow dr1 in dt.Rows)
                                                {
                                                    if (dr1["itemName"].ToString() == tempItemName)
                                                    {
                                                        string tItemName = Convert.ToString(tempItemName);
                                                        tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");
                                                        DataRow[] dtRemoveChk = _Class.clsVariables.dtSingleFree.Select("MainItemName='" + tItemName + "'");
                                                        if (dtRemoveChk.Length == 0)
                                                        {
                                                            if (isChkOpenItem != true)
                                                            {
                                                                count = 1;
                                                                if ((double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())) > 0)
                                                                {

                                                                    dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();


                                                                    {
                                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())));
                                                                    }

                                                                    tReadingValueDisplay = 1;
                                                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                                    drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                                    drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                                    drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                                                }
                                                                else
                                                                {
                                                                    MyMessageBox.ShowBox("Enter Valid Quantity", "Warning");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            isMsgDis = true;
                                                            MyMessageBox.ShowBox("This Item Could not be Remove from the list");

                                                        }
                                                    }
                                                    rowIndex += 1;


                                                }
                                                if (count == 0 && isMsgDis == false)
                                                {
                                                    MyMessageBox.ShowBox("Item not found in the list", "Warning");
                                                }

                                                funStockDisplay(tempItemName);
                                                funDisplayAmount(dt);
                                                gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                                gridItems.Columns[0].Width = 180;
                                                gridItems.Columns[0].ReadOnly = true;
                                                gridItems.Columns[1].Width = 50;
                                                gridItems.Columns[2].Width = 50;
                                                gridItems.Columns[3].Width = 50;
                                                gridItems.Columns[3].ReadOnly = true;
                                                gridItems.RowTemplate.Height = 35;
                                                funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                                funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                                funScrollGrid();
                                                funRoundCalculate();
                                            }
                                            else
                                            {
                                                MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Enter valid Quantity", "Warning");
                            }
                            txtEnterValue.Text = "";
                            txtEnterValue.Focus();
                        }
                    }
                    txtEnterValue.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void funBtnEnter()
        {
            try
            {
                //Same as Clicked Button event. Refer newBtnGroupItem
                funConnectionStateCheck();
                DataRow dr = null;
                txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                if (txtEnterValue.Text.Length > 0)
                {

                    // DataRow dr = null;
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    tempFindStar = txtEnterValue.Text.IndexOf("*");
                    if (tempFindStar == -1)
                    {
                        funConnectionStateCheck();
                        DataTable dtNew1 = new DataTable();

                        //  SqlDataReader dr12 = null;
                        dtNew1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                        cmd.Parameters.AddWithValue("@tActionType", "TXTBOXVALUE");
                        // dr12 = cmd.ExecuteReader();
                        SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                        adpCmd.Fill(dtNew1);
                        //dtNew1.Load(dr12);
                        int isRecord = 0;
                        for (int mn = 0; mn < dtNew1.Rows.Count; )
                        {
                            isRecord = 1;
                            rowIndex = 0;
                            dr = dt.NewRow();
                            //   MessageBox.Show(dr12["Item_Name"].ToString());
                            SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@tValue", dtNew1.Rows[mn]["Item_Name"].ToString());
                            cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
                            //  reader = cmd1.ExecuteReader();
                            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                            adpCmd1.Fill(dtNew);
                            //  dtNew.Load(reader);
                            if (dtNew.Rows.Count > 0)
                            {
                                count = 0;
                                totAmt = 0.00;
                                totQty = 0.00;
                                totTax = 0.00;
                                string tempItemName = dtNew.Rows[mn]["Item_Name"].ToString();
                                tItemNameGlob = tempItemName;
                                double tUnitDecimals = double.Parse(dtNew.Rows[mn]["unit_Decimals"].ToString());
                                string tWeightScale = dtNew.Rows[mn]["WeightScale"].ToString();
                                double tReadingValue = 0;
                                bool isChkStopAtRate = false;
                                DataTable dtItem = new DataTable();
                                dtItem.Rows.Clear();
                                SqlCommand cmd12 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                cmd12.Parameters.AddWithValue("@tItemName", tempItemName);
                                SqlDataAdapter adp = new SqlDataAdapter(cmd12);
                                adp.Fill(dtItem);
                                bool isChkOpenItem = false;
                                if (dtItem.Rows.Count > 0)
                                {
                                    isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopAtQty"].ToString());
                                    if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                    {
                                        isChkOpenItem = true;
                                    }
                                }

                                if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                                {
                                ReadAgain:
                                    if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                                    {
                                        try
                                        {
                                            tReadCount = 0;
                                            string data = "";
                                            data = _Class.clsVariables.serial.ReadExisting();
                                            //serial.Close();
                                            if (data.IndexOf("kg") > 0)
                                            {
                                                data = data.Substring(0, data.IndexOf("kg"));
                                                data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                // if
                                                tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                            }
                                            else if (data.IndexOf("k") > 0)
                                            {
                                                data = data.Substring(0, data.IndexOf("k"));
                                                data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                            }
                                        }
                                        catch (Exception)
                                        {
                                            tReadCount++;
                                            if (tReadCount < 10)
                                            {
                                                goto ReadAgain;
                                            }
                                            else
                                            {
                                                tShowQty = "";
                                                MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                                tShowQty = "Show";

                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (isChkStopAtRate == true)
                                        {
                                            tReadingValue = 0;
                                        }
                                        else
                                        {
                                            tReadingValue = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    if (isChkStopAtRate == true)
                                    {
                                        tReadingValue = 0;
                                    }
                                    else
                                    {
                                        tReadingValue = 1;
                                    }
                                }

                                foreach (DataRow dr1 in dt.Rows)
                                {
                                    if (dr1["itemName"].ToString() == tempItemName)
                                    {
                                        if (isChkOpenItem != true)
                                        {
                                            count = 1;

                                            if (tUnitDecimals == 0)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                            }
                                            if (tUnitDecimals == 1)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                            }
                                            if (tUnitDecimals == 2)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                            }
                                            if (tUnitDecimals == 3)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                            }
                                            if (tUnitDecimals == 4)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                            }

                                            {
                                                dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())).ToString());
                                            }
                                            gridItems.Rows[rowIndex].Selected = true;
                                            rowSelect = "";

                                            tReadingValueDisplay = double.Parse(dt.Rows[rowIndex]["Qty"].ToString());
                                            ClickedButtonDisplay = Convert.ToString(tempItemName);
                                            drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                            drRateDisplay = Convert.ToString(dr1["Rate"]);
                                            drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                        }
                                    }
                                    rowIndex += 1;

                                }
                                if (count == 0)
                                {
                                    dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString();
                                    if (tUnitDecimals == 0)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N0");
                                    }
                                    if (tUnitDecimals == 1)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N1");
                                    }
                                    if (tUnitDecimals == 2)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N2");
                                    }
                                    if (tUnitDecimals == 3)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N3");
                                    }
                                    if (tUnitDecimals == 4)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N4");
                                    }
                                    // dr["Qty"] = "1";
                                    dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));


                                    {
                                        dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                    }
                                    dt.Rows.Add(dr);
                                    //  funReplaceFreeItemAmt();
                                    tSelectedRowIndex = dt.Rows.Count;
                                    rowSelect = "Last";

                                    tReadingValueDisplay = tReadingValue;
                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                    drQtyDisplay = Convert.ToString(dr["Qty"]);
                                    drRateDisplay = Convert.ToString(dr["Rate"]);
                                    drAmtDisplay = Convert.ToString(dr["Amt"]);


                                }
                                funStockDisplay(tempItemName);
                                funDisplayAmount(dt);
                                if (rowSelect != "")
                                {
                                    gridItems.DataSource = dt.DefaultView;   // Change gridItems.ItemsSource = dt.DefaultView;
                                    gridItems.Columns[0].Width = 180;
                                    gridItems.Columns[0].ReadOnly = true;
                                    gridItems.Columns[1].Width = 50;
                                    gridItems.Columns[2].Width = 50;
                                    gridItems.Columns[3].Width = 50;
                                    gridItems.Columns[3].ReadOnly = true;
                                    gridItems.RowTemplate.Height = 35;
                                }
                                gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                                funScrollGrid();
                                funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                funRoundCalculate();
                            }
                            break;
                        }
                        if (isRecord == 0)
                        {
                            MyMessageBox.ShowBox("Product Not Found", "Warning");
                        }
                        txtEnterValue.Text = "";
                        txtEnterValue.Focus();
                    }
                    else
                    {
                        string tempItemCode = txtEnterValue.Text.Substring(tempFindStar + 1, ((txtEnterValue.Text.Length - 1) - (tempFindStar)));
                        //  MessageBox.Show(txtEnterValue.Text.Substring(tempFindStar+1,((txtEnterValue.Text.Length-1)-(tempFindStar))));
                        string tempQty = txtEnterValue.Text.Substring(0, tempFindStar);
                        //  MessageBox.Show(txtEnterValue.Text.Substring(0, tempFindStar));
                        if (tempQty.Trim() != "")
                        {
                            if (double.Parse(tempQty) > 0)
                            {



                                rowIndex = 0;
                                // DataRow dr = null;
                                dr = dt.NewRow();
                                // MessageBox.Show(ClickedButton.Content.ToString());
                                SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@tValue", tempItemCode);
                                cmd1.Parameters.AddWithValue("@tActionType", "ITEMCODE");
                                // reader = cmd1.ExecuteReader();
                                SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                                adpCmd1.Fill(dtNew);
                                // dtNew.Load(reader);
                                if (dtNew.Rows.Count > 0)
                                {
                                    count = 0;
                                    totAmt = 0.00;
                                    totQty = 0.00;
                                    totTax = 0.00;
                                    string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();
                                    tItemNameGlob = tempItemName;
                                    DataTable dtItem = new DataTable();
                                    dtItem.Rows.Clear();
                                    SqlCommand cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                    cmd.Parameters.AddWithValue("@tItemName", tempItemName);
                                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                    adp.Fill(dtItem);
                                    bool isChkOpenItem = false;
                                    if (dtItem.Rows.Count > 0)
                                    {
                                        if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                        {
                                            isChkOpenItem = true;
                                        }
                                    }
                                    foreach (DataRow dr1 in dt.Rows)
                                    {
                                        if (dr1["itemName"].ToString() == tempItemName)
                                        {
                                            if (isChkOpenItem != true)
                                            {
                                                count = 1;
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();
                                                dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())).ToString());

                                                tReadingValueDisplay = double.Parse(dt.Rows[rowIndex]["Qty"].ToString());
                                                ClickedButtonDisplay = Convert.ToString(tempItemName);
                                                drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                                drRateDisplay = Convert.ToString(dr1["Rate"]);
                                                drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                            }
                                        }
                                        rowIndex += 1;

                                    }
                                    if (count == 0)
                                    {
                                        dr["ItemName"] = dtNew.Rows[0]["Item_name"].ToString();
                                        dr["Qty"] = tempQty.ToString();
                                        dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString()));
                                        dr["Amt"] = string.Format("{0:0.00}", (double.Parse(tempQty) * double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString())));
                                        dt.Rows.Add(dr);
                                        tSelectedRowIndex = dt.Rows.Count;

                                        tReadingValueDisplay = double.Parse(tempQty);
                                        ClickedButtonDisplay = Convert.ToString(tempItemName);
                                        drQtyDisplay = Convert.ToString(dr["Qty"]);
                                        drRateDisplay = Convert.ToString(dr["Rate"]);
                                        drAmtDisplay = Convert.ToString(dr["Amt"]);



                                    }
                                    funStockDisplay(tempItemName);
                                    funDisplayAmount(dt);
                                    gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                    gridItems.Columns[0].Width = 180;
                                    gridItems.Columns[0].ReadOnly = true;
                                    gridItems.Columns[1].Width = 50;
                                    gridItems.Columns[2].Width = 50;
                                    gridItems.Columns[3].Width = 50;
                                    gridItems.Columns[3].ReadOnly = true;
                                    gridItems.RowTemplate.Height = 35;
                                    funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                    funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                    funScrollGrid();
                                    funRoundCalculate();
                                }
                                else
                                {
                                    MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                }
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Enter Valid Item Quantity", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Enter Quantity", "Warning");
                        }

                        txtEnterValue.Text = "";
                        txtEnterValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void btnStar_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (txtEnterValue.Text.Length > 0)
            {
                if (txtEnterValue.Text != "")
                {
                    temp = txtEnterValue.Text;
                    txtEnterValue.Text = "";
                    txtEnterValue.Text = temp + btn.Content.ToString();
                }
                if (txtEnterValue.Text == "")
                {
                    txtEnterValue.Text = btn.Content.ToString();
                }
            }
            txtEnterValue.Select(txtEnterValue.Text.Length, 0);
            txtEnterValue.Focus();
        }

        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                funConnectionStateCheck();
                if (e.Key == Key.Enter)
                {


                    tempTimer.Interval = 1000;
                    tempTimer.Enabled = false;
                    tempTimer.Tick += new EventHandler(timer1_Tick);
                    tTimerCount = 0;
                    // tempTimer.Start();
                    if (txtEnterValue.Text.Length == 0)
                    {
                        listSelect.SelectedIndex = -1;
                    }
                    funBtnEnterTextBox();
                    //funBtnEnter();

                }
                else
                {
                    funBtnSelect();
                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;
        string mainStr, mainStr2;
        double findCenterPosition;
        DataTable dtPrint = new DataTable();
        // string tSkipDraw = "";
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (pnlNumeric.Visibility == Visibility.Hidden)
            {
                pnlNumeric.Visibility = Visibility.Hidden;
            }
            else
            {
                if (_Class.clsVariables.tHideKeyboard == true)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    pnlNumeric.Visibility = Visibility.Visible;
                }
            }
            if (lblBillNo.Content.ToString().Trim() == "001")
            {
                // funPrint();
                MyMessageBox.ShowBox("There is No bill in the list", "Warning");
            }
            else
            {
                // tSkipDraw = "Skip";
                vPrevBill = "Yes";
                funPrevPrint();
                // tSkipDraw = "";
            }
            txtEnterValue.Focus();
        }
        DataTable dtFreeItem = new DataTable();
        public void funPrevPrint()
        {
            try
            {
                // if (gridItems.Rows.Count > 0)  // Change if (gridItems.Items.Count > 0)
                // {

                DateTime tBillDate = new DateTime();
                DateTime tBillTime = new DateTime();
                string tBillNo = "";
                double @tNetAmt = 0;
                double @tTotAmt = 0;
                double @tTotQty = 0;
                double @tTotTax = 0;
                double @tTotOriginalAmt = 0;
                double @Qty = 0;
                double @Rate = 0;
                double @Amt = 0, @tTaxCalAmt = 0, @tTax = 0;
                double tDiscount = 0.00;
                string tBillType = "";

                //Offer Details Code Start
                string HCLedgerName = "", HCAddress1 = "", HCAddress2 = "", HCAddress3 = "", HCAddress4 = "", HCAddress5 = "";
                DataTable dtAcProcess = new DataTable();


                dtFreeItem.Rows.Clear();

                DataTable dtTempTableChk = new DataTable();
                dtTempTableChk.Rows.Clear();
                SqlCommand cmdTempTableChk = new SqlCommand("Select count(*) from TempSalMas_table where ctr_no=@tCounter", con);
                cmdTempTableChk.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpTempTableChk = new SqlDataAdapter(cmdTempTableChk);
                adpTempTableChk.Fill(dtTempTableChk);
                bool isTempTableChk = false;
                if (dtTempTableChk.Rows.Count > 0)
                {
                    if (Convert.ToDouble(Convert.ToString(dtTempTableChk.Rows[0][0])) > 0)
                    {
                        isTempTableChk = true;
                    }
                    else
                    {
                        isTempTableChk = false;
                    }
                }

                // Load Previous Bill Detail for Printing
                DataTable dtPrviousde = new DataTable();
                dtPrviousde.Rows.Clear();
                SqlCommand cmdPreviousBillDe = new SqlCommand(@"Declare @tRowCount Numeric(18,0)=0;Declare @tCountChk numeric(18,0)=(select count(*) from Tempsalmas_table where ctr_no=@tCounter);
if @tCountChk=0
Select @tRowCount=MAX(smas_billNo) from SalMas_table with (index(IndexSalMas_table)) where ctr_no=@tCounter and smas_rtno=0
else
Select @tRowCount=MAX(smas_billNo) from Tempsalmas_table  where ctr_no=@tCounter and smas_rtno=0
IF @tRowCount is NULL OR @tRowCount=-1
SET @tRowCount=0;
if exists( select * from Tempsalmas_table where ctr_no=@tCounter)
BEGIN
select smas_billno,Smas_NetAmount,Smas_Rcvdamount from Tempsalmas_table where smas_billno=@tRowCount and smas_rtno=0;
END
else
select smas_billno,Smas_NetAmount,Smas_Rcvdamount from salmas_table where smas_billno=@tRowCount and smas_rtno=0;
", con);
                cmdPreviousBillDe.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpPreviousDe = new SqlDataAdapter(cmdPreviousBillDe);
                adpPreviousDe.Fill(dtPrviousde);
                string tlblPreviosBillNoNew = "";
                string tlblBillAmtNew = "";
                string tReceivedAmtNew = "";
                string tRefundAmtNew1 = "";
                if (dtPrviousde.Rows.Count > 0)
                {
                    tlblPreviosBillNoNew = Convert.ToString(dtPrviousde.Rows[0]["smas_billno"]);
                    tlblBillAmtNew = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dtPrviousde.Rows[0]["Smas_NetAmount"])));
                    tReceivedAmtNew = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dtPrviousde.Rows[0]["Smas_Rcvdamount"])));
                    tRefundAmtNew1 = string.Format("{0:0.00}", (Convert.ToDouble(Convert.ToString(dtPrviousde.Rows[0]["Smas_Rcvdamount"])) - Convert.ToDouble(Convert.ToString(dtPrviousde.Rows[0]["Smas_NetAmount"]))));
                }

                DataTable dtPrevBillMas = new DataTable();
                DataTable dtDetail = new DataTable();
                DataTable dtDetail1 = new DataTable();
                DataTable dtDiscount = new DataTable();
                if (isTempTableChk == false)
                {


                    dtPrevBillMas.Rows.Clear();
                    SqlCommand cmdBillNo = new SqlCommand("select Smas_no,CONVERT(date,smas_billdate,108) as BillDate, CONVERT(time,smas_billtime,103)as BillTime,smas_billno, Smas_name from salmas_table with (index(IndexSalMas_table)) where smas_billno=@tBillNo and smas_rtno=0", con);
                    //  cmdBillNo.Parameters.AddWithValue("@tBillNo", (double.Parse(lblPreviosBillNo.Content.ToString())));
                    cmdBillNo.Parameters.AddWithValue("@tBillNo", (double.Parse(tlblPreviosBillNoNew)));
                    SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                    adpBillNo.Fill(dtPrevBillMas);
                    if (dtPrevBillMas.Rows.Count > 0)
                    {
                        tBillDate = DateTime.Parse(dtPrevBillMas.Rows[0]["BillDate"].ToString());
                        tBillTime = DateTime.Parse(dtPrevBillMas.Rows[0]["BillTime"].ToString());
                        tBillType = dtPrevBillMas.Rows[0]["Smas_name"].ToString();
                        double code = double.Parse(dtPrevBillMas.Rows[0]["smas_billno"].ToString());
                        if (code < 9)
                        {
                            tBillNo = ("00" + Convert.ToString(code));
                        }
                        else if (code < 99)
                        {
                            tBillNo = ("0" + Convert.ToString(code));
                        }
                        else
                        {
                            tBillNo = (Convert.ToString(code));
                        }

                        dtDetail.Rows.Clear();
                        dtDetail1.Rows.Clear();

                        //(case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + ss + "+stktrn_table.Serial_No end) as ItemName

                        SqlCommand cmdBillDet1 = new SqlCommand(@"SELECT  (case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+stktrn_table.Serial_No end) as Item_name,stktrn_table.strn_sno,dbo.stktrn_table.nt_qty,stktrn_table.unit_no,unit_table.unit_alias FROM unit_table,  dbo.stktrn_table INNER JOIN dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where unit_table.unit_no=stktrn_table.unit_no and stktrn_table.strn_no=@tBillNo and strn_type=1", con);


                        //                        SqlCommand cmdBillDet1 = new SqlCommand(@"SELECT  dbo.Item_table.Item_name,stktrn_table.strn_sno,dbo.stktrn_table.nt_qty,stktrn_table.unit_no,unit_table.unit_alias
                        //                     FROM unit_table,  dbo.stktrn_table INNER JOIN
                        //                     dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where unit_table.unit_no=stktrn_table.unit_no and stktrn_table.strn_no=@tBillNo and strn_type=1", con);
                        cmdBillDet1.Parameters.AddWithValue("@tBillNo", (double.Parse(dtPrevBillMas.Rows[0]["Smas_no"].ToString())));
                        SqlDataAdapter adpBillDet1 = new SqlDataAdapter(cmdBillDet1);
                        adpBillDet1.Fill(dtDetail1);
                        for (int x = 0; x < dtDetail1.Rows.Count; x++)
                        {
                            if (dtDetail1.Rows[x]["Unit_alias"].ToString() == "True")
                            {
                                SqlCommand cmdBillDet = new SqlCommand(@"SELECT  (case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+stktrn_table.Serial_No end) as Item_name, '1' as nt_qty,convert(numeric(18,2), dbo.stktrn_table.Rate),convert(numeric(18,2),stktrn_table.nt_qty*stktrn_table.Rate) as Amt, convert(numeric(18,2),dbo.stktrn_table.Amount) FROM  dbo.stktrn_table INNER JOIN dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where stktrn_table.strn_sno=@tBillNo and strn_type=1", con);
                                cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtDetail1.Rows[x]["strn_sno"].ToString())));
                                SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                                adpBillDet.Fill(dtDetail);
                            }
                            else
                            {
                                SqlCommand cmdBillDet = new SqlCommand(@"SELECT  (case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+stktrn_table.Serial_No end) as Item_name, dbo.stktrn_table.nt_qty,convert(numeric(18,2), dbo.stktrn_table.Rate),convert(numeric(18,2),stktrn_table.nt_qty*stktrn_table.Rate) as Amt, convert(numeric(18,2),dbo.stktrn_table.Amount) FROM  dbo.stktrn_table INNER JOIN dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where stktrn_table.strn_sno=@tBillNo and strn_type=1", con);
                                cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtDetail1.Rows[x]["strn_sno"].ToString())));
                                SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                                adpBillDet.Fill(dtDetail);
                            }
                        }

                        //                        SqlCommand cmdBillDet = new SqlCommand(@"SELECT  dbo.Item_table.Item_name, dbo.stktrn_table.nt_qty,convert(numeric(18,2), dbo.stktrn_table.Rate),convert(numeric(18,2),stktrn_table.nt_qty*stktrn_table.Rate) as Amt, convert(numeric(18,2),dbo.stktrn_table.Amount)
                        //                     FROM  dbo.stktrn_table INNER JOIN
                        //                     dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where stktrn_table.strn_no=@tBillNo and strn_type=1", con);
                        //                        cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtPrevBillMas.Rows[0]["Smas_no"].ToString())));
                        //                        SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                        //                        adpBillDet.Fill(dtDetail);

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            @Qty = 0;
                            @Amt = double.Parse(dtDetail.Rows[i][3].ToString());
                            @tTaxCalAmt = Convert.ToDouble(Convert.ToString(dtDetail.Rows[i][4]));
                            @tTax = 0;
                            if (dtDetail.Rows[i][1].ToString() != "")
                            {
                                @Qty = double.Parse(dtDetail.Rows[i][1].ToString());
                            }
                            if (dtDetail.Rows[i][2].ToString() != "")
                            {
                                @Rate = double.Parse(dtDetail.Rows[i][2].ToString());
                            }

                            @tTotOriginalAmt = @tTotOriginalAmt + (@Qty * @Rate);
                            @tTotQty = @tTotQty + @Qty;
                            @tTotAmt = @tTotAmt + @Amt;
                            DataTable stNew = new DataTable();
                            stNew.Rows.Clear();
                            string iname = dtDetail.Rows[i][0].ToString();
                            int ni = iname.IndexOf("-");
                            if (ni != -1)
                                iname = iname.Substring(0, ni);
                            SqlCommand cmd = new SqlCommand("Select Nt_percent from Tax_Table where Tax_no=(Select Tax_no from item_table with (index(IndexItem_table)) where Item_Active=1 and Item_name=@ItemName)", con);
                            cmd.Parameters.AddWithValue("@ItemName", iname);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(stNew);
                            if (stNew.Rows.Count > 0)
                            {
                                // @tTax = (@Amt * double.Parse(stNew.Rows[0][0].ToString())) / 100;
                                if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                                {
                                    @tTax = @tTaxCalAmt - ((@tTaxCalAmt * 100) / (100 + Convert.ToDouble(stNew.Rows[0][0].ToString())));
                                }
                                else if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                                {
                                    @tTax = (@tTaxCalAmt * double.Parse(stNew.Rows[0][0].ToString())) / 100;
                                }
                                @tTotTax = @tTotTax + @tTax;
                            }
                        }

                        dtDiscount.Rows.Clear();
                        //  SqlCommand cmdDiscount = new SqlCommand("select * from DiscountDetail_table where Bill_no=@tBillNo", con);
                        SqlCommand cmdDiscount = new SqlCommand(@"Select SUM(Disc_Amt+Othdisc_Amt+spl_discamt) as Amount from stktrn_table where strn_no in (
Select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0 and smas_Cancel=0)", con);
                        cmdDiscount.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adpDiscount = new SqlDataAdapter(cmdDiscount);
                        adpDiscount.Fill(dtDiscount);



                        if (dtDiscount.Rows.Count > 0)
                        {
                            tDiscount = double.Parse(dtDiscount.Rows[0]["Amount"].ToString());
                        }
                        if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive" || _Class.clsVariables.tempGDisplayTaxType == "NoTax")
                        {
                            @tNetAmt = (@tTotAmt) - tDiscount;
                        }
                        else
                        {
                            @tNetAmt = (@tTotTax + @tTotAmt) - tDiscount;
                        }



                    }
                    //Here Getting House accounts Orginal Table values:
                    //                    SqlDataAdapter adpAC = new SqlDataAdapter(@"Select [Ledger_name],[Ledger_Add1]
                    //      ,[Ledger_Add2]
                    //      ,[Ledger_Add3]
                    //      ,[Ledger_Add4]
                    //      ,[Ledger_Add5]
                    //      ,[Ledger_Add6] from Ledger_Table Where Ledger_groupno=32 and Ledger_gno=202 and Ledger_No=(Select Distinct (StrnParty_no) As LedgerNo From stktrn_table Where stktrn_table.strn_no=@tBillNo  and StrnParty_no>15)", con);

                    SqlDataAdapter adpAC = new SqlDataAdapter(@" Select [Ledger_name],[Ledger_Add1]
      ,[Ledger_Add2]
      ,[Ledger_Add3]
      ,[Ledger_Add4]
      ,[Ledger_Add5]
      ,[Ledger_Add6] from Ledger_Table 
      Where Ledger_groupno=32 and Ledger_gno=202 and 
      Ledger_No=(Select Distinct (party_no) As LedgerNo From salmas_table Where salmas_table.smas_billno=@tBillNo  and Party_no>15)", con);
                    adpAC.SelectCommand.Parameters.AddWithValue("@tBillNo", tBillNo);
                    dtAcProcess.Rows.Clear();
                    adpAC.Fill(dtAcProcess);
                }
                else
                {
                    //SqlCommand cmdFreeItem = new SqlCommand("select Item_table.Item_name,(stktrn_table.FreeQty* FreeItem_table. Free_Qty) as Free_Qty from Item_table, FreeItem_table, stktrn_table where Item_table.Item_no=FreeItem_table.FreeItem_no and FreeItem_table.FreeSnoGroup=stktrn_table.FreeItemNo and FreeItem_table.FreeType<>'Price' and stktrn_table.strn_no=(Select smas_no from salmas_table where  salmas_table.smas_billno=@tBillNo)", con);
                    //cmdFreeItem.Parameters.AddWithValue("@tBillNo", (double.Parse(lblPreviosBillNo.Content.ToString())));
                    //SqlDataAdapter adpFreeItem = new SqlDataAdapter(cmdFreeItem);
                    //adpFreeItem.Fill(dtFreeItem);

                    //Offer Details Code End



                    dtPrevBillMas.Rows.Clear();
                    SqlCommand cmdBillNo = new SqlCommand("select Smas_no,CONVERT(date,smas_billdate,108) as BillDate, CONVERT(time,smas_billtime,103)as BillTime,smas_billno, Smas_name from Tempsalmas_table where smas_billno=@tBillNo and smas_rtno=0", con);
                    //cmdBillNo.Parameters.AddWithValue("@tBillNo", (double.Parse(lblPreviosBillNo.Content.ToString())));
                    cmdBillNo.Parameters.AddWithValue("@tBillNo", (double.Parse(tlblPreviosBillNoNew)));
                    SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                    adpBillNo.Fill(dtPrevBillMas);
                    if (dtPrevBillMas.Rows.Count > 0)
                    {
                        tBillDate = DateTime.Parse(dtPrevBillMas.Rows[0]["BillDate"].ToString());
                        tBillTime = DateTime.Parse(dtPrevBillMas.Rows[0]["BillTime"].ToString());
                        tBillType = dtPrevBillMas.Rows[0]["Smas_name"].ToString();
                        double code = double.Parse(dtPrevBillMas.Rows[0]["smas_billno"].ToString());
                        if (code < 9)
                        {
                            tBillNo = ("00" + Convert.ToString(code));
                        }
                        else if (code < 99)
                        {
                            tBillNo = ("0" + Convert.ToString(code));
                        }
                        else
                        {
                            tBillNo = (Convert.ToString(code));
                        }

                        dtDetail.Rows.Clear();
                        dtDetail1.Rows.Clear();

                        SqlCommand cmdBillDet1 = new SqlCommand(@"SELECT  dbo.Item_table.Item_name,Tempstktrn_table.strn_sno,dbo.Tempstktrn_table.nt_qty,Tempstktrn_table.unit_no,unit_table.unit_alias
                     FROM unit_table,  dbo.Tempstktrn_table INNER JOIN
                     dbo.Item_table ON dbo.Tempstktrn_table.item_no = dbo.Item_table.Item_no where unit_table.unit_no=Tempstktrn_table.unit_no and Tempstktrn_table.strn_no=@tBillNo and strn_type=1", con);
                        cmdBillDet1.Parameters.AddWithValue("@tBillNo", (double.Parse(dtPrevBillMas.Rows[0]["Smas_no"].ToString())));
                        SqlDataAdapter adpBillDet1 = new SqlDataAdapter(cmdBillDet1);
                        adpBillDet1.Fill(dtDetail1);
                        for (int x = 0; x < dtDetail1.Rows.Count; x++)
                        {
                            if (dtDetail1.Rows[x]["Unit_alias"].ToString() == "True")
                            {
                                SqlCommand cmdBillDet = new SqlCommand(@"SELECT  (case when len(Tempstktrn_table.Serial_No)=0 OR Tempstktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+Tempstktrn_table.Serial_No end) as Item_name, '1' as nt_qty,convert(numeric(18,2), dbo.Tempstktrn_table.Rate),convert(numeric(18,2),Tempstktrn_table.nt_qty*Tempstktrn_table.Rate) as Amt, convert(numeric(18,2),dbo.Tempstktrn_table.Amount) FROM  dbo.Tempstktrn_table INNER JOIN dbo.Item_table ON dbo.Tempstktrn_table.item_no = dbo.Item_table.Item_no where Tempstktrn_table.strn_sno=@tBillNo and strn_type=1", con);
                                cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtDetail1.Rows[x]["strn_sno"].ToString())));
                                SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                                adpBillDet.Fill(dtDetail);
                            }
                            else
                            {
                                SqlCommand cmdBillDet = new SqlCommand(@"SELECT  (case when len(tempstktrn_table.Serial_No)=0 OR tempstktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+tempstktrn_table.Serial_No end) as Item_name, dbo.Tempstktrn_table.nt_qty,convert(numeric(18,2), dbo.Tempstktrn_table.Rate),convert(numeric(18,2),Tempstktrn_table.nt_qty*Tempstktrn_table.Rate) as Amt, convert(numeric(18,2),dbo.Tempstktrn_table.Amount) FROM  dbo.Tempstktrn_table INNER JOIN dbo.Item_table ON dbo.Tempstktrn_table.item_no = dbo.Item_table.Item_no where Tempstktrn_table.strn_sno=@tBillNo and strn_type=1", con);
                                cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtDetail1.Rows[x]["strn_sno"].ToString())));
                                SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                                adpBillDet.Fill(dtDetail);
                            }
                        }



                        //                        SqlCommand cmdBillDet = new SqlCommand(@"SELECT  dbo.Item_table.Item_name, dbo.Tempstktrn_table.nt_qty,convert(numeric(18,2), dbo.Tempstktrn_table.Rate),convert(numeric(18,2),Tempstktrn_table.nt_qty*Tempstktrn_table.Rate),convert(numeric(18,2),dbo.Tempstktrn_table.Amount)
                        //                     FROM  dbo.Tempstktrn_table INNER JOIN
                        //                     dbo.Item_table ON dbo.tempstktrn_table.item_no = dbo.Item_table.Item_no where tempstktrn_table.strn_no=@tBillNo and strn_type=1", con);
                        //                        cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtPrevBillMas.Rows[0]["Smas_no"].ToString())));
                        //                        SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                        //                        adpBillDet.Fill(dtDetail);

                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            @Qty = 0;
                            @Amt = double.Parse(dtDetail.Rows[i][3].ToString());
                            @tTaxCalAmt = Convert.ToDouble(Convert.ToString(dtDetail.Rows[i][4]));
                            @tTax = 0;
                            if (dtDetail.Rows[i][1].ToString() != "")
                            {
                                @Qty = double.Parse(dtDetail.Rows[i][1].ToString());
                            }
                            if (dtDetail.Rows[i][2].ToString() != "")
                            {
                                @Rate = double.Parse(dtDetail.Rows[i][2].ToString());
                            }
                            @tTotOriginalAmt = @tTotOriginalAmt + (@Qty * @Rate);
                            @tTotQty = @tTotQty + @Qty;
                            @tTotAmt = @tTotAmt + @Amt;
                            DataTable stNew = new DataTable();
                            stNew.Rows.Clear();
                            string iname = dtDetail.Rows[i][0].ToString();
                            int ni = iname.IndexOf("-");
                            if (ni != -1)
                                iname = iname.Substring(0, ni);
                            SqlCommand cmd = new SqlCommand("Select Nt_percent from Tax_Table where Tax_no=(Select Tax_no from item_table with (index(IndexItem_table)) where Item_Active=1 and Item_name=@ItemName)", con);
                            cmd.Parameters.AddWithValue("@ItemName", iname);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(stNew);
                            if (stNew.Rows.Count > 0)
                            {
                                //   @tTax = (@Amt * double.Parse(stNew.Rows[0][0].ToString())) / 100;
                                if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                                {
                                    @tTax = @tTaxCalAmt - ((@tTaxCalAmt * 100) / (100 + Convert.ToDouble(stNew.Rows[0][0].ToString())));
                                }
                                else if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                                {
                                    @tTax = (@tTaxCalAmt * double.Parse(stNew.Rows[0][0].ToString())) / 100;
                                }
                                @tTotTax = @tTotTax + @tTax;
                            }
                        }

                        dtDiscount.Rows.Clear();
                        // SqlCommand cmdDiscount = new SqlCommand("select * from tempDiscountDetail_table where Bill_no=@tBillNo", con);
                        SqlCommand cmdDiscount = new SqlCommand(@"Select SUM(Disc_Amt+Othdisc_Amt+spl_discamt) as Amount from tempstktrn_table where strn_no in (
Select smas_no from tempSalMas_table where smas_billno=@tBillNo and smas_rtno=0 and smas_Cancel=0)", con);
                        cmdDiscount.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adpDiscount = new SqlDataAdapter(cmdDiscount);
                        adpDiscount.Fill(dtDiscount);

                        if (dtDiscount.Rows.Count > 0)
                        {
                            tDiscount = double.Parse(dtDiscount.Rows[0]["Amount"].ToString());
                        }
                        if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive" || _Class.clsVariables.tempGDisplayTaxType == "NoTax")
                        {
                            @tNetAmt = (@tTotAmt) - tDiscount;
                        }
                        else
                        {
                            @tNetAmt = (@tTotTax + @tTotAmt) - tDiscount;
                        }

                    }
                    //Here Getting House accounts TemTable values:
                    //                    SqlDataAdapter adpAC = new SqlDataAdapter(@"Select [Ledger_name],[Ledger_name],[Ledger_Add1]
                    //      ,[Ledger_Add2]
                    //      ,[Ledger_Add3]
                    //      ,[Ledger_Add4]
                    //      ,[Ledger_Add5]
                    //      ,[Ledger_Add6] from Ledger_Table Where Ledger_groupno=32 and Ledger_gno=202 and Ledger_No=(Select Distinct (StrnParty_no) As LedgerNo From tempstktrn_table Where tempstktrn_table.strn_no=@tBillNo  and StrnParty_no>15)", con);

                    SqlDataAdapter adpAC = new SqlDataAdapter(@"Select [Ledger_name],[Ledger_name],[Ledger_Add1]
      ,[Ledger_Add2]
      ,[Ledger_Add3]
      ,[Ledger_Add4]
      ,[Ledger_Add5]
      ,[Ledger_Add6] from Ledger_Table Where Ledger_groupno=32 and Ledger_gno=202 and Ledger_No=(Select Distinct (party_no) As LedgerNo From tempsalmas_table Where tempsalmas_table.smas_billno=@tBillNo  and Party_no>15)", con);
                    adpAC.SelectCommand.Parameters.AddWithValue("@tBillNo", tBillNo);

                    dtAcProcess.Rows.Clear();
                    adpAC.Fill(dtAcProcess);
                }
                mainStr = null;

                mainStr = "";
                if (_Class.clsVariables.tPrintImageEnable.Trim() == "Yes")
                {
                    ImagePrintMain.funImagePrintMain();
                    // mainStr += "\n";
                    //  mainStr += "\n";
                    // mainStr += "\n";
                }

                charPerLine = _Class.clsVariables.tempGCharactersPerLine;

                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowLogo;

                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }




                string tHeaderAlign = "Yes";

                tHeaderAlign = _Class.clsVariables.tempGReceiptHeaderLeftAlign;
                if (tHeaderAlign == "Yes")
                {
                    ////top design start


                    charPerLine = _Class.clsVariables.tempGCharactersPerLine;


                    if (_Class.clsVariables.tempGPrintTopLine1 == "Yes")
                    {
                        topLine1 = _Class.clsVariables.tempGTopLine1;
                        mainStr += topLine1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine1.Length)), ' ');

                        mainStr += "\n";
                    }
                    // Top Line2
                    // topLine1="";
                    if (_Class.clsVariables.tempGPrintTopLine2 == "Yes")
                    {
                        topLine2 = _Class.clsVariables.tempGTopLine2;
                        mainStr += topLine2;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                        mainStr += "\n";
                    }
                    // Top Line3
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine3 == "Yes")
                    {
                        topLine3 = _Class.clsVariables.tempGTopLine3;
                        mainStr += topLine3;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine3.Length)), ' ');

                        mainStr += "\n";
                    }

                    // Top Line4
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine4 == "Yes")
                    {
                        topLine4 = _Class.clsVariables.tempGTopLine4;
                        mainStr += topLine4;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine4.Length)), ' ');

                        mainStr += "\n";
                    }
                    // Top Line5
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine5 == "Yes")
                    {
                        topLine5 = _Class.clsVariables.tempGTopLine5;
                        mainStr += topLine5;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine5.Length)), ' ');

                        mainStr += "\n";
                    }

                }
                else
                {
                    charPerLine = _Class.clsVariables.tempGCharactersPerLine;

                    if (_Class.clsVariables.tempGPrintTopLine1 == "Yes")
                    {
                        topLine1 = _Class.clsVariables.tempGTopLine1;
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            else
                            {


                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            mainStr += "\n";
                        }
                    }
                    // Top Line2
                    // topLine1="";
                    if (_Class.clsVariables.tempGPrintTopLine2 == "Yes")
                    {
                        topLine2 = _Class.clsVariables.tempGTopLine2;
                        if (topLine2.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
                            if (findCenterPosition % 2 == 0)
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine2;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            else
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine2;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            mainStr += "\n";
                        }
                    }
                    // Top Line3
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine3 == "Yes")
                    {
                        topLine3 = _Class.clsVariables.tempGTopLine3;
                        if (topLine3.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
                            if (findCenterPosition % 2 == 0)
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine3;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            else
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine3;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            mainStr += "\n";
                        }
                    }

                    // Top Line4
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine4 == "Yes")
                    {
                        topLine4 = _Class.clsVariables.tempGTopLine4;
                        if (topLine4.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
                            if (findCenterPosition % 2 == 0)
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine4;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            else
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine4;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            mainStr += "\n";
                        }
                    }
                    // Top Line5
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine5 == "Yes")
                    {
                        topLine5 = _Class.clsVariables.tempGTopLine5;
                        if (topLine5.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
                            if (findCenterPosition % 2 == 0)
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine5;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            else
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine5;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            mainStr += "\n";
                        }
                    }

                }


                //header design start
                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }



                if (_Class.clsVariables.tempGPrintDate == "Yes")
                {

                    string tChk = "Bill Date:" + tBillDate.ToString("dd/MM/yyyy");
                    mainStr += "Bill Date:" + tBillTime.ToString("dd/MM/yyyy");
                    double tTimeCount = (double.Parse(charPerLine) - (tChk.Length + 13));
                    for (int j = 0; j < tTimeCount; j++)
                    {
                        mainStr += " ";
                    }


                    if (_Class.clsVariables.tempGPrintTime == "Yes")
                    {
                        mainStr += "Time:" + tBillTime.ToShortTimeString();
                    }
                    else
                    {
                        mainStr += "".PadLeft(13, ' ');

                    }
                    mainStr += "\n";
                    //    }
                    //}
                }




                //receipt No 

                string temp = _Class.clsVariables.tempGReceiptNumber + tBillNo;
                mainStr += temp;
                mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp.Length)), ' ');

                mainStr += "\n";



                //Counter Name
                if (_Class.clsVariables.tempGPrintCounterName == "Yes")
                {
                    temp = _Class.clsVariables.tCounterName;
                    mainStr += temp;
                    mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp.Length)), ' ');

                    mainStr += "\n";

                }


                //UserName
                if (_Class.clsVariables.tempGPrintUserName == "Yes")
                {
                    temp = _Class.clsVariables.tUserName;
                    mainStr += temp;
                    mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp.Length)), ' ');

                    mainStr += "\n";

                }


                //Print Line Below Header
                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }
                if (_Class.clsVariables.HcProcess == "Yes")
                {
                    //Here We Want To Process Here House Accounts:
                    //Getting Ledger Number For Address of Particulare bill no:

                    if (dtAcProcess.Rows.Count > 0)
                    {
                        HCLedgerName = dtAcProcess.Rows[0]["Ledger_Name"].ToString();
                        HCAddress1 = dtAcProcess.Rows[0]["Ledger_Add1"].ToString();
                        HCAddress2 = dtAcProcess.Rows[0]["Ledger_Add2"].ToString();
                        HCAddress3 = dtAcProcess.Rows[0]["Ledger_Add3"].ToString();
                        HCAddress4 = dtAcProcess.Rows[0]["Ledger_Add4"].ToString();
                        HCAddress5 = dtAcProcess.Rows[0]["Ledger_Add5"].ToString();

                        if (!string.IsNullOrEmpty(HCLedgerName))
                        {
                            topLine2 = "Customer Name: " + HCLedgerName;
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                            mainStr += "\n";
                        }
                        if (!string.IsNullOrEmpty(HCAddress1))
                        {
                            topLine2 = HCAddress1;
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                            mainStr += "\n";
                        }
                        if (!string.IsNullOrEmpty(HCAddress2))
                        {
                            topLine2 = HCAddress2;
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                            mainStr += "\n";
                        }
                        if (!string.IsNullOrEmpty(HCAddress3))
                        {
                            topLine2 = HCAddress3;
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                            mainStr += "\n";
                        }
                        if (!string.IsNullOrEmpty(HCAddress4))
                        {
                            topLine2 = HCAddress4;
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                            mainStr += "\n";
                        }
                        if (!string.IsNullOrEmpty(HCAddress5))
                        {
                            topLine2 = HCAddress5;
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                            mainStr += "\n";
                        }
                        lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                        if (lineBelowLogo == "No Line")
                        {
                            mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                            mainStr += "\n";
                        }
                        if (lineBelowLogo == "Single Line")
                        {
                            mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                            mainStr += "\n";
                        }
                        else if (lineBelowLogo == "Double Line")
                        {
                            mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                            mainStr += "\n";
                        }

                    }
                }
                // House Acconts Address Completed :

                //Again Single Line Will Draws:
                //Print Line Below Header



                DataTable dtPrinterItemName = new DataTable();
                string tempStr = null;
                mainStr2 = mainStr;

                if (_Class.clsVariables.tempGPrintQunatityandRate == "Yes" && _Class.clsVariables.tempGPrintURate == "Yes")
                {
                    string tQtyHeading = "";
                    mainStr += "Particulars";
                    // mainStr += tQtyHeading;
                    double chkCount = (double.Parse(charPerLine) - ("Particulars".Length + 22));
                    mainStr += "".PadRight(Convert.ToInt16(chkCount), ' ');

                    tQtyHeading += "Qty   ";
                    tQtyHeading += " U/Rate ";
                    tQtyHeading += " Amount";
                    mainStr += tQtyHeading;
                    mainStr += "\n";

                    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                        mainStr += "\n";
                    }

                    //    }
                    //}

                    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //foreach (DataRow row in dgsales.Rows)
                    {
                        // object[] array = dgsales.Rows[mn].;
                        bool isChk = false;
                        for (int z = 0; z < 4; z++)
                        {
                            if (dtDetail.Rows[mn][z].ToString().Trim() == "")
                            {
                                isChk = true;
                                break;
                            }
                        }
                        if (isChk == false)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                tempStr = dtDetail.Rows[mn][i].ToString();
                                if (i == 0)
                                {
                                    if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                                    {
                                        dtPrinterItemName.Rows.Clear();
                                        SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                        cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                                        SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                                        adpPrinterName.Fill(dtPrinterItemName);

                                        if (dtPrinterItemName.Rows.Count > 0)
                                        {
                                            tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                                        }
                                    }
                                }
                                //  MessageBox.Show(tempStr.Length.ToString());
                                findCenterPosition = (double.Parse(charPerLine) - 22);
                                if (i == 0)
                                {
                                    if (tempStr.Length <= (int)findCenterPosition)
                                    {
                                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                    }
                                    else
                                    {
                                        temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                                        //    MessageBox.Show(temp);
                                        int chkSpace = temp.LastIndexOf(" ");
                                        int loc = (temp.Length - temp.LastIndexOf(" "));
                                        //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                                        if (chkSpace != -1)
                                        {
                                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                                            //   MessageBox.Show(mainStr.ToString());
                                            for (int j = 0; j < loc; j++)
                                            {
                                                mainStr += " ";
                                            }
                                            mainStr += "\n";
                                            string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                                            // mainStr += temp1;
                                            if (temp1.Length <= (int)findCenterPosition)
                                            {
                                                mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                            }
                                            else
                                            {
                                                mainStr += temp1.Substring(0, (int)findCenterPosition);
                                            }
                                        }
                                        else
                                        {
                                            //Without Space Prev Code
                                            mainStr += temp.ToString();
                                            mainStr += "\n";
                                            string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                                            // mainStr += temp1;
                                            if (temp1.Length <= (int)findCenterPosition)
                                            {
                                                mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                            }
                                            else
                                            {
                                                mainStr += temp1.Substring(0, (int)findCenterPosition);
                                            }
                                        }
                                    }
                                }

                                if (i == 1)
                                {
                                    if (tempStr.Length < 8)
                                    {

                                        if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                                        {
                                            findCenterPosition = (7 - tempStr.Length);
                                            if (findCenterPosition % 2 == 0)
                                            {
                                                mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                                                mainStr += tempStr;
                                                mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                                            }
                                            else
                                            {
                                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                                mainStr += tempStr;
                                                mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');
                                            }
                                        }
                                        else
                                        {
                                            mainStr += tempStr.PadRight(7, ' ');
                                        }
                                    }
                                }
                                if (i == 2)
                                {
                                    // mainStr += tempStr.PadRight(7, ' ');
                                    if (tempStr.Length <= 7)
                                    {
                                        mainStr += tempStr.PadLeft(7, ' ');
                                    }
                                }
                                if (i == 3)
                                {
                                    if (tempStr.Length <= 8)
                                    {
                                        mainStr += tempStr.PadLeft(8, ' ');
                                    }
                                }
                                // tPrintText += tempStr;
                            }
                            mainStr += "\n";
                        }
                    }
                }
                else if (_Class.clsVariables.tempGPrintQunatityandRate == "No" && _Class.clsVariables.tempGPrintURate == "No")
                {
                    string tQtyHeading = "";
                    mainStr = "";
                    mainStr = mainStr2;
                    tQtyHeading = "Particulars";
                    mainStr += tQtyHeading;
                    double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));

                    mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                    mainStr += " Qty  ";
                    mainStr += "        ";
                    mainStr += "Amount";
                    mainStr += "\n";

                    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                        mainStr += "\n";
                    }

                    //    }
                    //}



                    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //foreach (DataRow row in dgsales.Rows)
                    {
                        // object[] array = row.ItemArray;

                        for (int i = 0; i < 4; i++)
                        {
                            tempStr = dtDetail.Rows[mn][i].ToString();
                            //  MessageBox.Show(tempStr.Length.ToString());
                            if (i == 0)
                            {
                                if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                                {
                                    dtPrinterItemName.Rows.Clear();
                                    SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                    cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                                    SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                                    adpPrinterName.Fill(dtPrinterItemName);

                                    if (dtPrinterItemName.Rows.Count > 0)
                                    {
                                        tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                                    }
                                }
                            }

                            findCenterPosition = (double.Parse(charPerLine) - 18);
                            if (i == 0)
                            {

                                if (tempStr.Length <= (int)findCenterPosition)
                                {
                                    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                }
                                else
                                {
                                    temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                                    //    MessageBox.Show(temp);
                                    int chkSpace = temp.LastIndexOf(" ");
                                    int loc = (temp.Length - temp.LastIndexOf(" "));
                                    //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                                    if (chkSpace != -1)
                                    {
                                        mainStr += temp.Substring(0, temp.LastIndexOf(" "));

                                        mainStr += "".PadLeft(Convert.ToInt16(loc), ' ');
                                        mainStr += "\n";
                                        string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                                        // mainStr += temp1;
                                        if (temp1.Length <= (int)findCenterPosition)
                                        {
                                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                        }
                                    }
                                    else
                                    {
                                        //Without Space Prev Code
                                        mainStr += temp.ToString();
                                        mainStr += "\n";
                                        string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                                        // mainStr += temp1;
                                        if (temp1.Length <= (int)findCenterPosition)
                                        {
                                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                        }
                                    }
                                }
                            }
                            if (i == 1)
                            {
                                if (tempStr.Length < 8)
                                {

                                    if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                                    {
                                        findCenterPosition = (7 - tempStr.Length);
                                        if (findCenterPosition % 2 == 0)
                                        {
                                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                                            mainStr += tempStr;
                                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                                        }
                                        else
                                        {
                                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                            mainStr += tempStr;
                                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');
                                        }
                                    }
                                    else
                                    {
                                        mainStr += tempStr.PadRight(7, ' ');
                                    }
                                }

                            }
                            if (i == 2)
                            {
                                mainStr += "   ";

                            }
                            if (i == 3)
                            {
                                if (tempStr.Length <= 8)
                                {
                                    mainStr += tempStr.PadLeft(8, ' ');
                                }
                            }
                            // tPrintText += tempStr;
                        }
                        mainStr += "\n";
                    }
                }
                else if (_Class.clsVariables.tempGPrintQunatityandRate == "Yes" && _Class.clsVariables.tempGPrintURate == "No")
                {
                    string tQtyHeading = "";
                    mainStr = "";
                    mainStr = mainStr2;
                    tQtyHeading = "Particulars";
                    mainStr += tQtyHeading;
                    double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));

                    mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                    mainStr += " Qty  ";
                    mainStr += "        ";
                    mainStr += "Amount";
                    mainStr += "\n";

                    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                        mainStr += "\n";
                    }

                    //    }
                    //}



                    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //foreach (DataRow row in dgsales.Rows)
                    {
                        // object[] array = row.ItemArray;

                        for (int i = 0; i < 4; i++)
                        {

                            tempStr = dtDetail.Rows[mn][i].ToString();
                            //  MessageBox.Show(tempStr.Length.ToString());
                            if (i == 0)
                            {
                                if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                                {
                                    dtPrinterItemName.Rows.Clear();
                                    string iname = dtDetail.Rows[mn][i].ToString();
                                    int ni = iname.IndexOf("-");
                                    if (ni != -1)
                                        iname = iname.Substring(0, ni);
                                    tempStr = iname;
                                    SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                    cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                                    SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                                    adpPrinterName.Fill(dtPrinterItemName);

                                    if (dtPrinterItemName.Rows.Count > 0)
                                    {
                                        tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                                    }
                                }
                            }

                            findCenterPosition = (double.Parse(charPerLine) - 18);
                            if (i == 0)
                            {

                                if (tempStr.Length <= (int)findCenterPosition)
                                {
                                    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                }
                                else
                                {
                                    temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                                    //    MessageBox.Show(temp);
                                    int chkSpace = temp.LastIndexOf(" ");
                                    int loc = (temp.Length - temp.LastIndexOf(" "));
                                    //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                                    if (chkSpace != -1)
                                    {
                                        mainStr += temp.Substring(0, temp.LastIndexOf(" "));

                                        mainStr += "".PadLeft(Convert.ToInt16(loc), ' ');
                                        mainStr += "\n";
                                        string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                                        // mainStr += temp1;
                                        if (temp1.Length <= (int)findCenterPosition)
                                        {
                                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                        }
                                    }
                                    else
                                    {
                                        //Without Space Prev Code
                                        mainStr += temp.ToString();
                                        mainStr += "\n";
                                        string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                                        // mainStr += temp1;
                                        if (temp1.Length <= (int)findCenterPosition)
                                        {
                                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                        }
                                    }
                                }
                            }
                            if (i == 1)
                            {
                                if (tempStr.Length < 8)
                                {

                                    if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                                    {
                                        findCenterPosition = (7 - tempStr.Length);
                                        if (findCenterPosition % 2 == 0)
                                        {
                                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                                            mainStr += tempStr;
                                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                                        }
                                        else
                                        {
                                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                            mainStr += tempStr;
                                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');
                                        }
                                    }
                                    else
                                    {
                                        mainStr += tempStr.PadRight(7, ' ');
                                    }
                                }

                            }
                            if (i == 2)
                            {
                                mainStr += "   ";

                            }
                            if (i == 3)
                            {
                                if (tempStr.Length <= 8)
                                {
                                    mainStr += tempStr.PadLeft(8, ' ');
                                }
                            }
                            // tPrintText += tempStr;
                        }
                        mainStr += "\n";
                    }
                }
                else if (_Class.clsVariables.tempGPrintQunatityandRate == "No" && _Class.clsVariables.tempGPrintURate == "Yes")
                {
                    string tQtyHeading = "";
                    tQtyHeading = "Particulars";
                    mainStr += tQtyHeading;
                    double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));

                    mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                    mainStr += "    ";
                    mainStr += "       ";
                    mainStr += "Amount";
                    mainStr += "\n";

                    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                        mainStr += "\n";
                    }

                    //    }
                    //}



                    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //foreach (DataRow row in dgsales.Rows)
                    {
                        // object[] array = row.ItemArray;

                        for (int i = 0; i < 4; i++)
                        {
                            tempStr = dtDetail.Rows[mn][i].ToString();
                            //  MessageBox.Show(tempStr.Length.ToString());
                            if (i == 0)
                            {
                                if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                                {
                                    dtPrinterItemName.Rows.Clear();
                                    string iname = dtDetail.Rows[mn][i].ToString();
                                    int ni = iname.IndexOf("-");
                                    if (ni != -1)
                                        iname = iname.Substring(0, ni);
                                    tempStr = iname;
                                    SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                                    cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                                    SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                                    adpPrinterName.Fill(dtPrinterItemName);

                                    if (dtPrinterItemName.Rows.Count > 0)
                                    {
                                        tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                                    }
                                }
                            }

                            findCenterPosition = (double.Parse(charPerLine) - 18);
                            if (i == 0)
                            {

                                if (tempStr.Length <= (int)findCenterPosition)
                                {
                                    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                }
                                else
                                {
                                    temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                                    //    MessageBox.Show(temp);
                                    int chkSpace = temp.LastIndexOf(" ");
                                    int loc = (temp.Length - temp.LastIndexOf(" "));
                                    //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                                    if (chkSpace != -1)
                                    {
                                        mainStr += temp.Substring(0, temp.LastIndexOf(" "));

                                        mainStr += "".PadLeft(Convert.ToInt16(loc), ' ');
                                        mainStr += "\n";
                                        string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                                        // mainStr += temp1;
                                        if (temp1.Length <= (int)findCenterPosition)
                                        {
                                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                        }
                                    }
                                    else
                                    {
                                        //Without Space Prev Code
                                        mainStr += temp.ToString();
                                        mainStr += "\n";
                                        string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                                        // mainStr += temp1;
                                        if (temp1.Length <= (int)findCenterPosition)
                                        {
                                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                        }
                                    }
                                }
                            }
                            if (i == 1)
                            {
                                mainStr += "   ";

                            }
                            if (i == 2)
                            {
                                mainStr += "       ";

                            }
                            if (i == 3)
                            {
                                if (tempStr.Length <= 8)
                                {
                                    mainStr += tempStr.PadLeft(8, ' ');
                                }
                            }
                            // tPrintText += tempStr;
                        }
                        mainStr += "\n";
                    }
                }



                // if (_Class.clsVariables.tempGPrintQunatityandRate == "Yes")
                // {
                //     string tQtyHeading = "";
                //     mainStr += "Particulars";
                //     // mainStr += tQtyHeading;
                //     double chkCount = (double.Parse(charPerLine) - ("Particulars".Length + 22));
                //     mainStr += "".PadRight(Convert.ToInt16(chkCount), ' ');

                //     tQtyHeading += "  Qty  ";
                //     tQtyHeading += "U/Rate ";
                //     tQtyHeading += " Amount";
                //     mainStr += tQtyHeading;
                //     mainStr += "\n";

                //     lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                //     if (lineBelowLogo == "No Line")
                //     {
                //         mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                //         mainStr += "\n";
                //     }
                //     if (lineBelowLogo == "Single Line")
                //     {
                //         mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                //         mainStr += "\n";
                //     }
                //     else if (lineBelowLogo == "Double Line")
                //     {
                //         mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                //         mainStr += "\n";
                //     }

                //     //    }
                //     //}

                //     for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                //     //foreach (DataRow row in dgsales.Rows)
                //     {
                //         // object[] array = dgsales.Rows[mn].;
                //         bool isChk = false;
                //         for (int z = 0; z < 4; z++)
                //         {
                //             if (dtDetail.Rows[mn][z].ToString().Trim() == "")
                //             {
                //                 isChk = true;
                //                 break;
                //             }
                //         }
                //         if (isChk == false)
                //         {
                //             for (int i = 0; i < 4; i++)
                //             {
                //                 tempStr = dtDetail.Rows[mn][i].ToString();
                //                 if (i == 0)
                //                 {
                //                     if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                //                     {
                //                         dtPrinterItemName.Rows.Clear();
                //                         SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                //                         cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                //                         SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                //                         adpPrinterName.Fill(dtPrinterItemName);

                //                         if (dtPrinterItemName.Rows.Count > 0)
                //                         {
                //                             tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                //                         }
                //                     }
                //                 }
                //                 //  MessageBox.Show(tempStr.Length.ToString());
                //                 findCenterPosition = (double.Parse(charPerLine) - 22);
                //                 if (i == 0)
                //                 {
                //                     if (tempStr.Length <= (int)findCenterPosition)
                //                     {
                //                         mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                //                     }
                //                     else

                //                     {
                //                         temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                //                         //    MessageBox.Show(temp);
                //                         int chkSpace = temp.LastIndexOf(" ");
                //                         int loc = (temp.Length - temp.LastIndexOf(" "));
                //                         //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                //                         if (chkSpace != -1)
                //                         {
                //                             mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                //                             //   MessageBox.Show(mainStr.ToString());
                //                             for (int j = 0; j < loc; j++)
                //                             {
                //                                 mainStr += " ";
                //                             }
                //                             mainStr += "\n";
                //                             string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                //                             // mainStr += temp1;
                //                             if (temp1.Length <= (int)findCenterPosition)
                //                             {
                //                                 mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                             }
                //                             else
                //                             {
                //                                 mainStr += temp1.Substring(0, (int)findCenterPosition);
                //                             }
                //                         }
                //                         else
                //                         {
                //                             //Without Space Prev Code
                //                             mainStr += temp.ToString();
                //                             mainStr += "\n";
                //                             string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                //                             // mainStr += temp1;
                //                             if (temp1.Length <= (int)findCenterPosition)
                //                             {
                //                                 mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                             }
                //                             else
                //                             {
                //                                 mainStr += temp1.Substring(0, (int)findCenterPosition);
                //                             }
                //                         }
                //                     }
                //                 }

                //                 if (i == 1)
                //                 {
                //                     if (tempStr.Length < 8)
                //                     {

                //                         if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                //                         {
                //                             findCenterPosition = (7 - tempStr.Length);
                //                             if (findCenterPosition % 2 == 0)
                //                             {
                //                                 mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                //                                 mainStr += tempStr;
                //                                 mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                //                             }
                //                             else
                //                             {
                //                                 mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                //                                 mainStr += tempStr;
                //                                 mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');
                //                             }
                //                         }
                //                         else
                //                         {
                //                             mainStr += tempStr.PadRight(7, ' ');
                //                         }
                //                     }
                //                 }
                //                 if (i == 2)
                //                 {
                //                     // mainStr += tempStr.PadRight(7, ' ');
                //                     if (tempStr.Length <= 7)
                //                     {
                //                         mainStr += tempStr.PadLeft(7, ' ');
                //                     }
                //                 }
                //                 if (i == 3)
                //                 {
                //                     if (tempStr.Length <= 8)
                //                     {
                //                         mainStr += tempStr.PadLeft(8, ' ');
                //                     }
                //                 }
                //                 // tPrintText += tempStr;
                //             }
                //             mainStr += "\n";
                //         }
                //     }
                // }

                // else
                // {
                //     string tQtyHeading = "";
                //     tQtyHeading = "Particulars";
                //     mainStr += tQtyHeading;
                //     double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));

                //     mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                //     mainStr += "    ";
                //     mainStr += "       ";
                //     mainStr += "Amount";
                //     mainStr += "\n";

                //     lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                //     if (lineBelowLogo == "No Line")
                //     {
                //         mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                //         mainStr += "\n";
                //     }
                //     if (lineBelowLogo == "Single Line")
                //     {
                //         mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                //         mainStr += "\n";
                //     }
                //     else if (lineBelowLogo == "Double Line")
                //     {
                //         mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                //         mainStr += "\n";
                //     }

                //     //    }
                //     //}



                //     for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                //     //foreach (DataRow row in dgsales.Rows)
                //     {
                //         // object[] array = row.ItemArray;

                //         for (int i = 0; i < 4; i++)
                //         {
                //             tempStr = dtDetail.Rows[mn][i].ToString();
                //             //  MessageBox.Show(tempStr.Length.ToString());
                //             if (i == 0)
                //             {
                //                 if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                //                 {
                //                     dtPrinterItemName.Rows.Clear();
                //                     SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                //                     cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                //                     SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                //                     adpPrinterName.Fill(dtPrinterItemName);

                //                     if (dtPrinterItemName.Rows.Count > 0)
                //                     {
                //                         tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                //                     }
                //                 }
                //             }

                //             findCenterPosition = (double.Parse(charPerLine) - 18);
                //             if (i == 0)
                //             {

                //                 if (tempStr.Length <= (int)findCenterPosition)
                //                 {
                //                     mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                //                 }
                //                 else
                //                 {
                //                     temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                //                     //    MessageBox.Show(temp);
                //                     int chkSpace = temp.LastIndexOf(" ");
                //                     int loc = (temp.Length - temp.LastIndexOf(" "));
                //                     //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                //                     if (chkSpace != -1)
                //                     {
                //                         mainStr += temp.Substring(0, temp.LastIndexOf(" "));

                //                         mainStr += "".PadLeft(Convert.ToInt16(loc), ' ');
                //                         mainStr += "\n";
                //                         string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                //                         // mainStr += temp1;
                //                         if (temp1.Length <= (int)findCenterPosition)
                //                         {
                //                             mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                         }
                //                     }
                //                     else
                //                     {
                //                         //Without Space Prev Code
                //                         mainStr += temp.ToString();
                //                         mainStr += "\n";
                //                         string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                //                         // mainStr += temp1;
                //                         if (temp1.Length <= (int)findCenterPosition)
                //                         {
                //                             mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                         }
                //                     }
                //                 }
                //             }
                //             if (i == 1)
                //             {
                //                 mainStr += "   ";

                //             }
                //             if (i == 2)
                //             {
                //                 mainStr += "       ";

                //             }
                //             if (i == 3)
                //             {
                //                 if (tempStr.Length <= 8)
                //                 {
                //                     mainStr += tempStr.PadLeft(8, ' ');
                //                 }
                //             }
                //             // tPrintText += tempStr;
                //         }
                //         mainStr += "\n";
                //     }
                // }

                // //   For print or not URate in sales screen

                // //if(_Class.clsVariables.tempGPrintQunatityandRate != "No")
                //// {
                //     if (_Class.clsVariables.tempGPrintURate == "Yes" && _Class.clsVariables.tempGPrintQunatityandRate == "Yes")
                //     {
                //         string tQtyHeading = "";
                //         mainStr = string.Empty;
                //         mainStr = mainStr2;
                //         mainStr += "Particulars";
                //         // mainStr += tQtyHeading;
                //         double chkCount = (double.Parse(charPerLine) - ("Particulars".Length + 22));
                //         mainStr += "".PadRight(Convert.ToInt16(chkCount), ' ');

                //         tQtyHeading += "  Qty  ";
                //         tQtyHeading += "U/Rate ";
                //         tQtyHeading += " Amount";
                //         mainStr += tQtyHeading;
                //         mainStr += "\n";

                //         lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                //         if (lineBelowLogo == "No Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                //             mainStr += "\n";
                //         }
                //         if (lineBelowLogo == "Single Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                //             mainStr += "\n";
                //         }
                //         else if (lineBelowLogo == "Double Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                //             mainStr += "\n";
                //         }

                //         //    }
                //         //}

                //         for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                //         //foreach (DataRow row in dgsales.Rows)
                //         {
                //             // object[] array = dgsales.Rows[mn].;
                //             bool isChk = false;
                //             for (int z = 0; z < 4; z++)
                //             {
                //                 if (dtDetail.Rows[mn][z].ToString().Trim() == "")
                //                 {
                //                     isChk = true;
                //                     break;
                //                 }
                //             }
                //             if (isChk == false)
                //             {
                //                 for (int i = 0; i < 4; i++)
                //                 {
                //                     tempStr = dtDetail.Rows[mn][i].ToString();
                //                     if (i == 0)
                //                     {
                //                         if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                //                         {
                //                             dtPrinterItemName.Rows.Clear();
                //                             SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                //                             cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                //                             SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                //                             adpPrinterName.Fill(dtPrinterItemName);

                //                             if (dtPrinterItemName.Rows.Count > 0)
                //                             {
                //                                 tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                //                             }
                //                         }
                //                     }
                //                     //  MessageBox.Show(tempStr.Length.ToString());
                //                     findCenterPosition = (double.Parse(charPerLine) - 22);
                //                     if (i == 0)
                //                     {
                //                         if (tempStr.Length <= (int)findCenterPosition)
                //                         {
                //                             mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                //                         }
                //                         else
                //                         {
                //                             temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                //                             //    MessageBox.Show(temp);
                //                             int chkSpace = temp.LastIndexOf(" ");
                //                             int loc = (temp.Length - temp.LastIndexOf(" "));
                //                             //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                //                             if (chkSpace != -1)
                //                             {
                //                                 mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                //                                 //   MessageBox.Show(mainStr.ToString());
                //                                 for (int j = 0; j < loc; j++)
                //                                 {
                //                                     mainStr += " ";
                //                                 }
                //                                 mainStr += "\n";
                //                                 string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                //                                 // mainStr += temp1;
                //                                 if (temp1.Length <= (int)findCenterPosition)
                //                                 {
                //                                     mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                                 }
                //                                 else
                //                                 {
                //                                     mainStr += temp1.Substring(0, (int)findCenterPosition);
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 //Without Space Prev Code
                //                                 mainStr += temp.ToString();
                //                                 mainStr += "\n";
                //                                 string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                //                                 // mainStr += temp1;
                //                                 if (temp1.Length <= (int)findCenterPosition)
                //                                 {
                //                                     mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                                 }
                //                                 else
                //                                 {
                //                                     mainStr += temp1.Substring(0, (int)findCenterPosition);
                //                                 }
                //                             }
                //                         }
                //                     }

                //                     if (i == 1)
                //                     {
                //                         if (tempStr.Length < 8)
                //                         {

                //                             if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                //                             {
                //                                 findCenterPosition = (7 - tempStr.Length);
                //                                 if (findCenterPosition % 2 == 0)
                //                                 {
                //                                     mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                //                                     mainStr += tempStr;
                //                                     mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                //                                 }
                //                                 else
                //                                 {
                //                                     mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                //                                     mainStr += tempStr;
                //                                     mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');
                //                                 }
                //                             }
                //                             else
                //                             {
                //                                 mainStr += tempStr.PadRight(7, ' ');
                //                             }
                //                         }
                //                     }
                //                     if (i == 2)
                //                     {
                //                         // mainStr += tempStr.PadRight(7, ' ');
                //                         if (tempStr.Length <= 7)
                //                         {
                //                             mainStr += tempStr.PadLeft(7, ' ');
                //                         }
                //                     }
                //                     if (i == 3)
                //                     {
                //                         if (tempStr.Length <= 8)
                //                         {
                //                             mainStr += tempStr.PadLeft(8, ' ');
                //                         }
                //                     }
                //                     // tPrintText += tempStr;
                //                 }
                //                 mainStr += "\n";
                //             }
                //         }
                //     }

                //     else if (_Class.clsVariables.tempGPrintURate == "No" && _Class.clsVariables.tempGPrintQunatityandRate == "No")
                //     {
                //         string tQtyHeading = "";
                //         mainStr = "";
                //         mainStr = mainStr2;
                //         tQtyHeading = "Particulars";
                //         mainStr += tQtyHeading;
                //         double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));

                //         mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                //         mainStr += " Qty  ";
                //         mainStr += "        ";
                //         mainStr += "Amount";
                //         mainStr += "\n";

                //         lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                //         if (lineBelowLogo == "No Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                //             mainStr += "\n";
                //         }
                //         if (lineBelowLogo == "Single Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                //             mainStr += "\n";
                //         }
                //         else if (lineBelowLogo == "Double Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                //             mainStr += "\n";
                //         }

                //         //    }
                //         //}



                //         for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                //         //foreach (DataRow row in dgsales.Rows)
                //         {
                //             // object[] array = row.ItemArray;

                //             for (int i = 0; i < 4; i++)
                //             {
                //                 tempStr = dtDetail.Rows[mn][i].ToString();
                //                 //  MessageBox.Show(tempStr.Length.ToString());
                //                 if (i == 0)
                //                 {
                //                     if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                //                     {
                //                         dtPrinterItemName.Rows.Clear();
                //                         SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                //                         cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                //                         SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                //                         adpPrinterName.Fill(dtPrinterItemName);

                //                         if (dtPrinterItemName.Rows.Count > 0)
                //                         {
                //                             tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                //                         }
                //                     }
                //                 }

                //                 findCenterPosition = (double.Parse(charPerLine) - 18);
                //                 if (i == 0)
                //                 {

                //                     if (tempStr.Length <= (int)findCenterPosition)
                //                     {
                //                         mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                //                     }
                //                     else
                //                     {
                //                         temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                //                         //    MessageBox.Show(temp);
                //                         int chkSpace = temp.LastIndexOf(" ");
                //                         int loc = (temp.Length - temp.LastIndexOf(" "));
                //                         //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                //                         if (chkSpace != -1)
                //                         {
                //                             mainStr += temp.Substring(0, temp.LastIndexOf(" "));

                //                             mainStr += "".PadLeft(Convert.ToInt16(loc), ' ');
                //                             mainStr += "\n";
                //                             string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                //                             // mainStr += temp1;
                //                             if (temp1.Length <= (int)findCenterPosition)
                //                             {
                //                                 mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                             }
                //                         }
                //                         else
                //                         {
                //                             //Without Space Prev Code
                //                             mainStr += temp.ToString();
                //                             mainStr += "\n";
                //                             string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                //                             // mainStr += temp1;
                //                             if (temp1.Length <= (int)findCenterPosition)
                //                             {
                //                                 mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                             }
                //                         }
                //                     }
                //                 }
                //                 if (i == 1)
                //                 {
                //                     if (tempStr.Length < 8)
                //                     {

                //                         if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                //                         {
                //                             findCenterPosition = (7 - tempStr.Length);
                //                             if (findCenterPosition % 2 == 0)
                //                             {
                //                                 mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                //                                 mainStr += tempStr;
                //                                 mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');

                //                             }
                //                             else
                //                             {
                //                                 mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                //                                 mainStr += tempStr;
                //                                 mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition / 2), ' ');
                //                             }
                //                         }
                //                         else
                //                         {
                //                             mainStr += tempStr.PadRight(7, ' ');
                //                         }
                //                     }

                //                 }
                //                 if (i == 2)
                //                 {
                //                     mainStr += "   ";

                //                 }
                //                 if (i == 3)
                //                 {
                //                     if (tempStr.Length <= 8)
                //                     {
                //                         mainStr += tempStr.PadLeft(8, ' ');
                //                     }
                //                 }
                //                 // tPrintText += tempStr;
                //             }
                //             mainStr += "\n";
                //         }
                //     }
                //     else
                //     {
                //         string tQtyHeading = "";
                //         tQtyHeading = "Particulars";
                //         mainStr += tQtyHeading;
                //         double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));

                //         mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                //         //tQtyHeading += "  Qty  ";
                //         //mainStr += "    ";
                //         mainStr += "  Qty  ";
                //         mainStr += "       ";
                //         mainStr += "Amount";
                //         mainStr += "\n";

                //         lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                //         if (lineBelowLogo == "No Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                //             mainStr += "\n";
                //         }
                //         if (lineBelowLogo == "Single Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                //             mainStr += "\n";
                //         }
                //         else if (lineBelowLogo == "Double Line")
                //         {
                //             mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                //             mainStr += "\n";
                //         }

                //         //    }
                //         //}



                //         for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                //         //foreach (DataRow row in dgsales.Rows)
                //         {
                //             // object[] array = row.ItemArray;

                //             for (int i = 0; i < 4; i++)
                //             {
                //                 tempStr = dtDetail.Rows[mn][i].ToString();
                //                 //  MessageBox.Show(tempStr.Length.ToString());
                //                 if (i == 0)
                //                 {
                //                     if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                //                     {
                //                         dtPrinterItemName.Rows.Clear();
                //                         SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table with (index(IndexItem_table)) where Item_Active=1 and item_name=@tItemName", con);
                //                         cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                //                         SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                //                         adpPrinterName.Fill(dtPrinterItemName);

                //                         if (dtPrinterItemName.Rows.Count > 0)
                //                         {
                //                             tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                //                         }
                //                     }
                //                 }

                //                 findCenterPosition = (double.Parse(charPerLine) - 18);
                //                 if (i == 0)
                //                 {

                //                     if (tempStr.Length <= (int)findCenterPosition)
                //                     {
                //                         mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                //                     }
                //                     else
                //                     {
                //                         temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                //                         //    MessageBox.Show(temp);
                //                         int chkSpace = temp.LastIndexOf(" ");
                //                         int loc = (temp.Length - temp.LastIndexOf(" "));
                //                         //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                //                         if (chkSpace != -1)
                //                         {
                //                             mainStr += temp.Substring(0, temp.LastIndexOf(" "));

                //                             mainStr += "".PadLeft(Convert.ToInt16(loc), ' ');
                //                             mainStr += "\n";
                //                             string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                //                             // mainStr += temp1;
                //                             if (temp1.Length <= (int)findCenterPosition)
                //                             {
                //                                 mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                             }
                //                         }
                //                         else
                //                         {
                //                             //Without Space Prev Code
                //                             mainStr += temp.ToString();
                //                             mainStr += "\n";
                //                             string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                //                             // mainStr += temp1;
                //                             if (temp1.Length <= (int)findCenterPosition)
                //                             {
                //                                 mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                //                             }
                //                         }
                //                     }
                //                 }
                //                 if (i == 1)
                //                 {
                //                     mainStr += "   ";

                //                 }
                //                 //if (i == 2)
                //                 //{
                //                 //    mainStr += "       ";

                //                 //}
                //                 if (i == 3)
                //                 {
                //                     if (tempStr.Length <= 8)
                //                     {
                //                         mainStr += tempStr.PadLeft(8, ' ');
                //                     }
                //                 }
                //                 // tPrintText += tempStr;
                //             }
                //             mainStr += "\n";
                //         }
                //     }

                // }





                //if(_Class.clsVariables.tempGPrintQunatityandRate=="No")
                //{

                //}



                if (_Class.clsVariables.tempGPrintTax == "Yes" && _Class.clsVariables.tempPrintTaxType == "Top")
                {

                    if (_Class.clsVariables.tempGPayThisAmount != "")
                    {
                        string tTaxType = "NoTax";
                        tTaxType = _Class.clsVariables.tempGDisplayTaxType;

                        if (tTaxType.Trim() == "NoTax")
                        {
                            topLine1 = "";
                        }
                        if (tTaxType.Trim() == "Exclusive")
                        {
                            topLine1 = " Exclusive GST  " + string.Format("{0:0.00}", double.Parse(@tTotTax.ToString())) + "  ";
                        }
                        if (tTaxType.Trim() == "Inclusive")
                        {
                            topLine1 = "  Inclusive GST  " + string.Format("{0:0.00}", double.Parse(@tTotTax.ToString())) + "  ";
                        }
                        // +":$3000.00";
                        {

                            if (true)
                            {
                                lineBelowLogo = "No Line";
                                if (lineBelowLogo == "No Line")
                                {

                                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                                    // mainStr += "\n";
                                }
                            }
                            //topLine1 = _Class.clsVariables.topLine1;
                            //if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                            //{
                            //    findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length));
                            //   // mainStr += "".PadLeft((findCenterPosition), ' ');
                            //  //  mainStr += topLine1;

                            //   // topLine1 = topLine1;
                            //    if (tTaxType.Trim() == "Inclusive")
                            //    {
                            //        mainStr += "".PadLeft((Convert.ToInt16(topLine1.Length)+2), ' ');
                            //    }
                            //    else if (tTaxType.Trim() == "Exclusive")
                            //    {
                            //        mainStr += "".PadLeft((Convert.ToInt16(topLine1.Length) + 4), ' ');
                            //    }
                            //    else
                            //    {
                            //        mainStr += "".PadLeft((Convert.ToInt16(topLine1.Length) + 4), ' ');
                            //    }
                            //    mainStr += topLine1;
                            //}

                            if (topLine1.Length <= (double.Parse(charPerLine)))
                            {
                                findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length) + 10);
                                mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition), ' ');
                                mainStr += topLine1;

                                //topLine1 =  (@tTotAmt.ToString() == "") ? 0.00 : double.Parse(@tTotAmt.ToString());
                                //mainStr += "".PadLeft(Convert.ToInt16((17 - topLine1.Length)), ' ');

                                // mainStr += topLine1;
                                //  +"  3000.00";
                            }
                        }
                    }
                }




                if (_Class.clsVariables.tempGPrintSubtotal == "Yes")
                {
                    if (true)
                    {
                        lineBelowLogo = "No Line";
                        if (lineBelowLogo == "No Line")
                        {

                            mainStr += "".PadRight(Convert.ToInt16(charPerLine), ' ');
                            mainStr += "\n";
                        }
                    }
                    topLine1 = _Class.clsVariables.tempGSubtotal;
                    if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));


                        mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition), ' ');
                        mainStr += topLine1;

                        topLine1 = string.Format("{0:0.00}", (@tTotAmt.ToString() == "") ? 0.00 : double.Parse(@tTotAmt.ToString()));
                        mainStr += "".PadLeft(Convert.ToInt16((9 - topLine1.Length)), ' ');

                        mainStr += topLine1;
                        //  +"  3000.00";
                    }
                    mainStr += "\n";
                }
                if (tDiscount.ToString() != "")
                {
                    if (tDiscount > 0)
                    {
                        topLine1 = "Discount:";
                        if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));
                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition), ' ');
                            mainStr += topLine1;
                            topLine1 = string.Format("{0:0.00}", tDiscount);
                            mainStr += "".PadLeft(Convert.ToInt16((9 - topLine1.Length)), ' ');
                            mainStr += topLine1;
                            //  +"  3000.00";
                        }
                        mainStr += "\n";
                    }
                }
                lineBelowLogo = _Class.clsVariables.tempGPrintlineAboveTotal;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }

                //    }
                //}
                // Pay this amount


                if (_Class.clsVariables.tempGPayThisAmount != "")
                {

                    if (_Class.clsVariables.tempGPrintPayThisAmountRightAlign == "Yes")
                    {
                        //Right Align Code Here
                        // topLine1 = dtPrint.Rows[k]["Property"].ToString();
                        topLine1 = _Class.clsVariables.tempGPayThisAmount;
                        if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));


                            mainStr += "".PadLeft(Convert.ToInt16(findCenterPosition), ' ');
                            mainStr += topLine1;



                            //topLine1 = lblBillAmt.Content.ToString();
                            topLine1 = tlblBillAmtNew;
                            mainStr += "".PadLeft(Convert.ToInt16((9 - topLine1.Length)), ' ');

                            mainStr += topLine1;
                            //  +"  3000.00";
                        }
                    }
                    else
                    {
                        // topLine1 = _Class.clsVariables.tempGPayThisAmount + lblBillAmt.Content.ToString();
                        topLine1 = _Class.clsVariables.tempGPayThisAmount + tlblBillAmtNew;
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {

                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            }
                            else
                            {
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            }
                        }
                    }
                    mainStr += "\n";
                }
                //Tax Print
                if (_Class.clsVariables.tempGPrintTax == "Yes" && _Class.clsVariables.tempPrintTaxType == "Bottom")
                {
                    lineBelowLogo = "No Line";
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (_Class.clsVariables.tempGPayThisAmount != "")
                    {
                        string tTaxType = "NoTax";
                        tTaxType = _Class.clsVariables.tempGDisplayTaxType;

                        if (tTaxType.Trim() == "NoTax")
                        {
                            topLine1 = "";
                        }
                        if (tTaxType.Trim() == "Exclusive")
                        {
                            topLine1 = "[ GST : " + string.Format("{0:0.00}", double.Parse(@tTotTax.ToString())) + " ]";
                        }
                        if (tTaxType.Trim() == "Inclusive")
                        {
                            topLine1 = "[ GST : " + string.Format("{0:0.00}", double.Parse(@tTotTax.ToString())) + " ]";
                        }
                        // +":$3000.00";
                        if (topLine1.Length <= double.Parse(charPerLine) && topLine1.Length > 0)
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            else
                            {

                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                            }
                            mainStr += "\n";
                        }

                    }
                }
                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowTotal;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }


                if (_Class.clsVariables.tempGPrintBillType == "Yes")
                {
                    string temp1 = "Payment Mode:" + tBillType;
                    mainStr += temp1;

                    mainStr += "".PadLeft(Convert.ToInt16(Convert.ToDouble(charPerLine) - temp1.Length), ' ');
                    mainStr += "\n";
                    //  break;
                }

                if (_Class.clsVariables.tempGPrintPaymentMode == "Yes")
                {
                    DataTable dtPayment = new DataTable();
                    dtPayment.Rows.Clear();
                    string tempQuery = "";
                    if (isTempTableChk == false)
                    {
                        tempQuery = "Select Ledger_groupno,Ledger_no,Ledger_name, SUM(SalRecv_Amt) as Amt  from salRecv_table, Ledger_table where  SalRecv_Led=Ledger_no and SalRecv_Salno=@tBillNo group by Ledger_groupno,Ledger_no,Ledger_name";
                    }
                    else
                    {
                        tempQuery = "Select Ledger_groupno,Ledger_no,Ledger_name, SUM(SalRecv_Amt) as Amt  from TempsalRecv_table, Ledger_table where  SalRecv_Led=Ledger_no and SalRecv_Salno=@tBillNo group by Ledger_groupno,Ledger_no,Ledger_name";
                    }
                    SqlCommand cmdPayment = new SqlCommand(tempQuery, con);
                    cmdPayment.Parameters.AddWithValue("@tBillNo", tBillNo);
                    SqlDataAdapter adpPayment = new SqlDataAdapter(cmdPayment);
                    adpPayment.Fill(dtPayment);
                    double tPCashAmt = 0, tPNETSAmt = 0, tPCreditCardAmt = 0, tPHouseACAmt = 0, tPVoucherAmt = 0;
                    for (int mn = 0; mn < dtPayment.Rows.Count; mn++)
                    {
                        if (dtPayment.Rows[mn]["Ledger_no"].ToString().Trim() == "5")
                        {
                            tPCashAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                        }

                        else if (dtPayment.Rows[mn]["Ledger_no"].ToString().Trim() == "14")
                        {
                            tPNETSAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                        }
                        else if (dtPayment.Rows[mn]["Ledger_groupno"].ToString().Trim() == "5" && dtPayment.Rows[mn]["Ledger_no"].ToString().Trim() != "14")
                        {
                            tPCreditCardAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                        }
                        else if (dtPayment.Rows[mn]["Ledger_groupno"].ToString().Trim() == "32")
                        {
                            tPHouseACAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                        }
                        else
                        {
                            tPVoucherAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                        }
                    }
                    if (tPCashAmt > 0)
                    {
                        string temp1 = "Cash      : " + string.Format("{0:0.00}", tPCashAmt);
                        mainStr += temp1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                        mainStr += "\n";
                    }
                    if (tPNETSAmt > 0)
                    {
                        string temp1 = "NETS      : " + string.Format("{0:0.00}", tPNETSAmt);
                        mainStr += temp1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                        mainStr += "\n";
                    }
                    if (tPCreditCardAmt > 0)
                    {
                        string temp1 = "Creditcard: " + string.Format("{0:0.00}", tPCreditCardAmt);
                        mainStr += temp1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                        mainStr += "\n";
                    }

                    for (int mn = 0; mn < dtPayment.Rows.Count; mn++)
                    {
                        if (dtPayment.Rows[mn]["Ledger_groupno"].ToString().Trim() == "5" && dtPayment.Rows[mn]["Ledger_no"].ToString().Trim() != "14")
                        {
                            // tPCreditCardAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                            string temp1 = " >" + dtPayment.Rows[mn]["Ledger_name"].ToString().Trim();
                            // mainStr +=((temp1.Length<(double.Parse(charPerLine)-10))? temp1: temp1.Substring(0,(int)(double.Parse(charPerLine)-11)))+ string.Format("{0:0.00}", (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim())));
                            mainStr += ((temp1.Length < (double.Parse(charPerLine) - 12)) ? temp1 : temp1.Substring(0, (int)(double.Parse(charPerLine) - 13))) + " : " + string.Format("{0:0.00}", (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim())));
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                            mainStr += "\n";
                        }
                    }
                    if (tPHouseACAmt > 0)
                    {
                        string temp1 = "House AC  : " + string.Format("{0:0.00}", tPHouseACAmt);
                        mainStr += temp1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                        mainStr += "\n";
                    }


                    for (int mn = 0; mn < dtPayment.Rows.Count; mn++)
                    {
                        if (dtPayment.Rows[mn]["Ledger_groupno"].ToString().Trim() == "32")
                        {
                            string temp1 = " >" + dtPayment.Rows[mn]["Ledger_name"].ToString().Trim();
                            mainStr += ((temp1.Length < (double.Parse(charPerLine) - 12)) ? temp1 : temp1.Substring(0, (int)(double.Parse(charPerLine) - 13))) + " : " + string.Format("{0:0.00}", (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim())));
                            // mainStr += temp1;
                            mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                            mainStr += "\n";
                        }

                    }
                    if (tPVoucherAmt > 0)
                    {
                        string temp1 = "Voucher   : " + string.Format("{0:0.00}", tPVoucherAmt);
                        mainStr += temp1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp1.Length)), ' ');

                        mainStr += "\n";
                    }





                    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowTotal;
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                        mainStr += "\n";
                    }
                }
                //    }
                string temp12 = _Class.clsVariables.tempGAmountTendered + " " + string.Format("{0:0.00}", tReceivedAmtNew);
                mainStr += temp12;

                mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp12.Length)), ' ');

                mainStr += "\n";
                //temp = "Change : " + string.Format("{0:0.00}", lblRefundAmt.Content);
                temp = "Change : " + string.Format("{0:0.00}", tRefundAmtNew1);
                mainStr += temp;
                mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp.Length)), ' ');
                mainStr += "\n";

                string strsalsno = "";
                SqlCommand cmdsal = new SqlCommand("select salesmen from Control_table", con);
                DataTable dtsal = new DataTable();
                dtsal.Clear();
                SqlDataAdapter adpsal = new SqlDataAdapter(cmdsal);
                adpsal.Fill(dtsal);
                if (dtsal.Rows.Count > 0)
                {
                    strsalsno = dtsal.Rows[0]["salesmen"].ToString();
                }
                if (strsalsno == "1")
                {
                    string strSalname;
                    SqlCommand cmdSalesmanName = new SqlCommand("select Ledger_name from Ledger_table where Ledger_no='" + _Class.clsVariables.tempsalesmenLedgerNo + "'", con);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    SqlDataAdapter adpsalname = new SqlDataAdapter(cmdSalesmanName);
                    adpsalname.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        strSalname = dt.Rows[0]["Ledger_name"].ToString();
                    }
                    else
                    {
                        strSalname = "";
                    }
                    if (_Class.clsVariables.tempsalesmenLedgerNo != "")
                    {
                        mainStr += "Salesmen : " + strSalname;
                        mainStr += "\n";
                    }
                    else
                    {
                        //SqlCommand cmdSalesmanName1 = new SqlCommand("select Ledger_name from Ledger_table where Ledger_no='" + _Class.clsVariables.tempsalesmenLedgerNo + "'", con);
                        SqlCommand cmdSalesmanName1 = new SqlCommand("select distinct a.Ledger_name from Ledger_table a  where a.Ledger_no=(select Smas_SmanNo from salmas_table where smas_billno='" + tBillNo + "')", con);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        SqlDataAdapter adpsalname1 = new SqlDataAdapter(cmdSalesmanName1);
                        adpsalname1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            strSalname = dt1.Rows[0]["Ledger_name"].ToString();
                        }
                        else
                        {
                            strSalname = "";
                        }
                        mainStr += "Salesmen : " + strSalname;
                        mainStr += "\n";
                    }
                    string strsalesmennote = "";
                    if (_Class.clsVariables.tempsalesmenNote != "")
                    {
                        strsalesmennote = _Class.clsVariables.tempsalesmenNote;

                    }
                    else
                    {

                        SqlCommand cmdSalesmanNote1 = new SqlCommand("select smas_remarks from salmas_table where smas_billno='" + tBillNo + "'", con);
                        DataTable dtNote1 = new DataTable();
                        dtNote1.Rows.Clear();
                        SqlDataAdapter adpsalNote1 = new SqlDataAdapter(cmdSalesmanNote1);
                        adpsalNote1.Fill(dtNote1);
                        if (dtNote1.Rows.Count > 0)
                        {

                            strsalesmennote = dtNote1.Rows[0]["smas_remarks"].ToString();
                        }
                        else
                        {
                            strsalesmennote = "";
                        }

                    }
                    int strlenNote = strsalesmennote.Length;
                    if (strlenNote <= 30)
                    {
                        mainStr += "Note : " + _Class.clsVariables.tempsalesmenNote;
                        mainStr += "\n";
                    }
                    else
                    {
                        string sentence1 = strsalesmennote;
                        string[] words1 = sentence1.Split(' ');
                        var parts1 = new Dictionary<int, string>();
                        string part1 = string.Empty;
                        int partCounter1 = 0;
                        foreach (var word in words1)
                        {
                            if (part1.Length + word.Length <= 40)
                            {
                                part1 += string.IsNullOrEmpty(part1) ? word : " " + word;
                            }
                            else
                            {
                                parts1.Add(partCounter1, part1);
                                part1 = word;
                                partCounter1++;
                            }
                        }
                        parts1.Add(partCounter1, part1);
                        StringBuilder NotesPrint = new StringBuilder();
                        foreach (var item in parts1)
                        {
                            NotesPrint.Append(item.Value);
                            NotesPrint.Append(Environment.NewLine);
                        }
                        //txtAddress.Text = string.Empty;
                        // txtAddress.Text = txtAddress.Text.Insert(1, builder.ToString());
                        strsalesmennote = NotesPrint.ToString();
                        mainStr += "Note : " + "\n" + strsalesmennote;
                        //mainStr += "\n";
                    }
                }
                lineBelowLogo = _Class.clsVariables.tempGPrintLineAboveBottomText;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }
                //Free Item Print Coding Start

                if (_Class.clsVariables.tempGPrintSavedAmt == "Yes")
                {
                    if ((@tTotOriginalAmt - @tTotAmt) > 0)
                    {
                        topLine1 = _Class.clsVariables.tempGSavedAmount + ((@tTotOriginalAmt - @tTotAmt) - tDiscount);
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            }
                            else
                            {
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                                mainStr += topLine1;
                                mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            }
                            mainStr += "\n";
                        }
                    }
                }

                //bottom line
                if (_Class.clsVariables.tempGPrintBottomLine1 == "Yes")
                {

                    topLine1 = _Class.clsVariables.tempGBottomLine1;
                    if (topLine1.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                        if (findCenterPosition % 2 == 0)
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine2 == "Yes")
                {

                    topLine2 = _Class.clsVariables.tempGBottomLine2;
                    if (topLine2.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
                        if (findCenterPosition % 2 == 0)
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine3 == "Yes")
                {

                    topLine3 = _Class.clsVariables.tempGBottomLine3;
                    if (topLine3.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
                        if (findCenterPosition % 2 == 0)
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine3;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine3;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine4 == "Yes")
                {

                    topLine4 = _Class.clsVariables.tempGBottomLine4;
                    if (topLine4.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
                        if (findCenterPosition % 2 == 0)
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine4;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine4;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine5 == "Yes")
                {

                    topLine5 = _Class.clsVariables.tempGBottomLine5;
                    if (topLine5.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
                        if (findCenterPosition % 2 == 0)
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine5;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine5;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }
                }
                //Print Line Below Header

                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowBottomText;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }

                //    }
                //}

                //Print Bottom Time

                if (_Class.clsVariables.tempGPrintBottomTime == "Yes")
                {

                    topLine1 = currentDate.ToString();
                    if (topLine1.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                        if (findCenterPosition % 2 == 0)
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }
                }
                if (_Class.clsVariables.QueueNo == "Yes")
                {
                    string temp1 = "";
                    if (tBillNo.Length.Equals(2))
                    {
                        temp1 = "Queue No:" + "0" + tBillNo.Substring(tBillNo.Length - 3, 3);
                    }
                    else
                    {
                        temp1 = "Queue No:" + tBillNo.Substring(tBillNo.Length - 3, 3);
                    }

                    if (temp1.Length <= double.Parse(charPerLine) && temp1.Length > 0)
                    {
                        findCenterPosition = (double.Parse(charPerLine) - temp1.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += temp1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {

                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += temp1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');

                        }
                        mainStr += "\n";
                    }


                }
                ////string ChkText = "";
                ////ChkText  = "040 24 24 24 24                          " + "\n";
                ////ChkText += "06 42 21 77 70                           " + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "STADSTAXI EINDHOVEN                      "+"\n";
                ////ChkText += "A Taxi                                   " + "\n";
                ////ChkText += "Ariana                                   " + "\n";
                ////ChkText += "stadstaxieindhoven@hotmail.com           " + "\n";
                ////ChkText += "www.stadstaxieindhoven.nl                " + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "Name :..................................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "Date :..................................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "To   :..................................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "Price:..................................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "Car  :..................................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "Driver:.................................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "Signature:..............................." + "\n";
                ////ChkText += "                                         " + "\n";
                ////ChkText += "  Not Satisfied? We have arrangement for " + "\n";
                ////ChkText += "              complaint                  " + "\n";
                ////ChkText += "Dispulses Committee: www.taxiklacht.nl   " + "\n";
                ////ChkText += "or cal 0900-437 2445                     " + "\n";
                ////ChkText += "       0900-202 1881                     " + "\n";
                ////ChkText += "Algemene voorwarden KNV te's-Gravenhage  " + "\n";
                ////ChkText += "                                         " + "\n";
                //    }
                //}
                // MessageBox.Show(mainStr);
                string tPrinterType = "";

                if (_Class.clsVariables.tempGEnableThisDevice == "Yes")
                {
                    tPrinterType = "Receipt";
                }
                int tNoPrint = 0;

                if (tPrinterType == "Receipt")
                {
                    DataTable dtPrinter = new DataTable();
                    dtPrinter.Rows.Clear();
                    SqlDataAdapter adpPrinter = new SqlDataAdapter("select * from CrystalReportPrinterList", con);
                    adpPrinter.Fill(dtPrinter);
                    bool isChkPrinter = false;
                    for (int i = 0; i < dtPrinter.Rows.Count; i++)
                    {
                        string printerName = dtPrinter.Rows[i]["PrinterName"].ToString();
                        isChkPrinter = false;
                        if (_Class.clsVariables.tempGPrinterName == printerName.ToUpper())
                        {
                            reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcReceipt.rdlc";
                            ReportParameter rpReportOn = new ReportParameter("ReceiptValue", Convert.ToString(mainStr), false);
                            this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });
                            reportViewerSales.RefreshReport();
                            reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);
                            break;
                        }
                    }
                    if (isChkPrinter == false)
                    {

                        topLine5 = _Class.clsVariables.tempGPrintCopies;
                        if (topLine5 == "1 Copy")
                        {
                            tNoPrint = 1;
                        }
                        else if (topLine5 == "2 Copy")
                        {
                            tNoPrint = 2;
                        }
                        else if (topLine5 == "3 Copy")
                        {
                            tNoPrint = 3;
                        }
                        else if (topLine5 == "No Copies")
                        {
                            tNoPrint = 0;
                        }

                        for (int i2 = 0; i2 < tNoPrint; i2++)
                        {
                            // RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, mainStr);

                            Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, mainStr));
                            workerThread.Start();
                            bool finished = workerThread.Join(3000);
                            if (!finished)
                            {
                                workerThread.Abort();
                                // CancelPrintJob();
                            }
                            if (_Class.clsVariables.tempGCutPaper == "Yes")
                            {
                                DataTable dtNew = new DataTable();
                                dtNew.Rows.Clear();
                                SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                                cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                                adp.Fill(dtNew);
                                if (dtNew.Rows.Count > 0)
                                {
                                    string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

                                    byteOut = new byte[byteStrings.Length];

                                    for (int i = 0; i < byteStrings.Length; i++)
                                    {
                                        byteOut[i] = Convert.ToByte(byteStrings[i]);
                                    }
                                    //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                    //    }

                                    string s1 = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

                                    Thread workerThread1 = new Thread(() => RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, s1));
                                    workerThread1.Start();
                                    finished = workerThread1.Join(3000);
                                    if (!finished)
                                    {
                                        workerThread1.Abort();
                                        //CancelPrintJob();
                                    }
                                    // }
                                }
                            }
                        }
                    }
                }
                else
                {
                    int r = 0;
                    if (SalesProject._Class.clsVariables.PrinterType.ToString().Trim() == "A4")
                    {

                        if (dtDetail.Rows.Count > 0)
                        {
                            string vLedgerName = "";
                            string vInvToAddress = "";
                            string vCompanyName = "";
                            string CompanyAddressLine1 = "";
                            string tLedgerSalesmenName = "";
                            string tLedgerAliasName = "";
                            string tPartyno = "";
                            string tLedgerLimitDays = "";
                            string tRemarks = "";
                            string strtremarks = "";
                            string strNote = "";
                            string strTNote = "";

                            if (vMainTable == "Yes" && vPrevBill == "Yes")
                            {
                                SqlCommand cmdBillNo = new SqlCommand("select * from salmas_table where smas_billno=@tBillNo", con);
                                cmdBillNo.Parameters.AddWithValue("@tBillNo", lblPreviosBillNo.Content);
                                SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                                DataTable dtBillNo = new DataTable();
                                dtBillNo.Rows.Clear();
                                adpBillNo.Fill(dtBillNo);
                                if (dtBillNo.Rows.Count > 0)
                                {
                                    vLedgerName = (dtBillNo.Rows[0]["smas_name"].ToString().Trim());
                                    tPartyno = (dtBillNo.Rows[0]["Smas_SmanNo"].ToString().Trim());
                                    strtremarks = (dtBillNo.Rows[0]["Smas_remarks"].ToString().Trim());
                                    if (strtremarks == "Null")
                                    {
                                        tRemarks = "";
                                    }
                                    else
                                    {
                                        tRemarks = strtremarks;
                                    }
                                }
                                SqlCommand cmdSalName1 = new SqlCommand("select * from Ledger_table where Ledger_No=@tLedgerNo", con);
                                cmdSalName1.Parameters.AddWithValue("@tLedgerNo", tPartyno);
                                SqlDataAdapter adpSalName1 = new SqlDataAdapter(cmdSalName1);
                                DataTable dtSalName1 = new DataTable();
                                dtSalName1.Rows.Clear();
                                adpSalName1.Fill(dtSalName1);
                                if (dtSalName1.Rows.Count > 0)
                                {
                                    tLedgerSalesmenName = (dtSalName1.Rows[0]["Ledger_name"].ToString().Trim());
                                }

                            }
                            else
                            {
                                SqlCommand cmdBillNo = new SqlCommand("select * from tempsalmas_table where smas_billno=@tBillNo", con);
                                cmdBillNo.Parameters.AddWithValue("@tBillNo", lblPreviosBillNo.Content);
                                SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                                DataTable dtBillNo = new DataTable();
                                dtBillNo.Rows.Clear();
                                adpBillNo.Fill(dtBillNo);
                                if (dtBillNo.Rows.Count > 0)
                                {
                                    vLedgerName = (dtBillNo.Rows[0]["smas_name"].ToString().Trim());
                                    strtremarks = (dtBillNo.Rows[0]["Smas_remarks"].ToString().Trim());
                                    if (strtremarks == "Null")
                                    {
                                        tRemarks = "";
                                    }
                                    else
                                    {
                                        tRemarks = strtremarks;
                                    }

                                }
                                SqlCommand cmdSalName = new SqlCommand("select * from Ledger_table where Ledger_No=@tLedgerNo", con);
                                cmdSalName.Parameters.AddWithValue("@tLedgerNo", _Class.clsVariables.tempsalesmenLedgerNo);
                                SqlDataAdapter adpSalName = new SqlDataAdapter(cmdSalName);
                                DataTable dtSalName = new DataTable();
                                dtSalName.Rows.Clear();
                                adpSalName.Fill(dtSalName);
                                if (dtSalName.Rows.Count > 0)
                                {
                                    tLedgerSalesmenName = (dtSalName.Rows[0]["Ledger_name"].ToString().Trim());
                                }


                            }
                            if (vLedgerName != "Cash Sales" && vLedgerName != "NETS")
                            {
                                SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=32", con);
                                cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", vLedgerName);
                                SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                                DataTable dtLedger = new DataTable();
                                dtLedger.Rows.Clear();
                                adpLedgerDetails.Fill(dtLedger);
                                if (dtLedger.Rows.Count > 0)
                                {
                                    vLedgerName = (dtLedger.Rows[0]["Ledger_Name"].ToString().Trim());
                                    vInvToAddress = (dtLedger.Rows[0]["Ledger_Add1"].ToString().Trim()) + Environment.NewLine + (dtLedger.Rows[0]["Ledger_Add2"].ToString().Trim()) + Environment.NewLine + (dtLedger.Rows[0]["Ledger_Add3"].ToString().Trim());
                                    tLedgerAliasName = (dtLedger.Rows[0]["Ledger_mtname"].ToString().Trim());
                                    tLedgerLimitDays = (dtLedger.Rows[0]["Limit_days"].ToString().Trim());
                                }
                                SqlCommand cmdSalName = new SqlCommand("select * from Ledger_table where Ledger_No=@tLedgerNo", con);
                                cmdSalName.Parameters.AddWithValue("@tLedgerNo", _Class.clsVariables.tempsalesmenLedgerNo);
                                SqlDataAdapter adpSalName = new SqlDataAdapter(cmdSalName);
                                DataTable dtSalName = new DataTable();
                                dtSalName.Rows.Clear();
                                adpSalName.Fill(dtSalName);
                                if (dtSalName.Rows.Count > 0)
                                {
                                    tLedgerSalesmenName = (dtSalName.Rows[0]["Ledger_name"].ToString().Trim());
                                }
                            }
                            else
                            {
                                vInvToAddress = "";
                            }

                            SqlDataAdapter adpCompanyAddress = new SqlDataAdapter("Select * from Custom_text", con);
                            DataTable dtcompany = new DataTable();
                            dtcompany.Rows.Clear();
                            adpCompanyAddress.Fill(dtcompany);

                            SqlCommand cmdNote = new SqlCommand("select * from Control_table", con);
                            DataTable dtNote = new DataTable();
                            dtNote.Rows.Clear();
                            SqlDataAdapter adpNote = new SqlDataAdapter(cmdNote);
                            adpNote.Fill(dtNote);
                            if (dtNote.Rows.Count > 0)
                            {
                                strNote = (dtNote.Rows[0]["Note"].ToString().Trim());
                            }
                            if (strNote != "" || strNote != "NULL")
                            {
                                strTNote = strNote;
                            }
                            else
                            {
                                strTNote = "";
                            }
                            DateTime tDueDate = new DateTime();
                            if (tLedgerLimitDays != "")
                            {
                                tDueDate = tBillDate.AddDays(Convert.ToInt16(tLedgerLimitDays));
                            }
                            else
                            {
                                tDueDate = tBillDate;
                            }
                            if (dtcompany.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtcompany.Rows.Count; i++)
                                {
                                    if (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line1")
                                    {
                                        vCompanyName = (dtcompany.Rows[i]["prop"].ToString());
                                    }

                                    if ((dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line2") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line3") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line4") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line5") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line6"))
                                    {
                                        CompanyAddressLine1 = CompanyAddressLine1.ToString() + (dtcompany.Rows[i]["prop"].ToString()) + Environment.NewLine;
                                    }
                                }
                                double tGrandTotal = 0.00;
                                //a4 start
                                //Dataset.DsSalesRpt dsSalesSummaryObj = new Dataset.DsSalesRpt();
                                //DsA4sales dssalessummaryObj1 = new DsA4sales();
                                //for (int i = 0; i < dtDetail.Rows.Count; i++)
                                //{
                                //    dsSalesSummaryObj.Tables["DsSalesRpt"].Rows.Add(dtDetail.Rows[i]["Item_name"].ToString(), dtDetail.Rows[i]["nt_qty"].ToString(), dtDetail.Rows[i]["Column1"], dtDetail.Rows[i]["Column2"].ToString(), "0.00", "0.00", "0.00");
                                //    tGrandTotal += Convert.ToDouble(dtDetail.Rows[i]["Column2"].ToString());
                                //}
                                //a4 end


                                DsA4sales dssalessummaryObj1 = new DsA4sales();
                                for (int i = 0; i < dtDetail.Rows.Count; i++)
                                {
                                    dssalessummaryObj1.Tables["DtA4Sales"].Rows.Add(dtDetail.Rows[i]["Item_name"].ToString(), dtDetail.Rows[i]["nt_qty"].ToString(), dtDetail.Rows[i]["Column1"], dtDetail.Rows[i]["Column2"].ToString(), "0.00", "0.00", "0.00");
                                    tGrandTotal += Convert.ToDouble(dtDetail.Rows[i]["Column2"].ToString());
                                }


                                rpt.Reset();
                                //  DataTable dt = getDate();
                                string dtdate = DateTime.Now.ToString();

                                //A4 start
                                //ReportDataSource ds1 = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DsSalesRpt"]);
                                //rpt.LocalReport.DataSources.Add(ds1);
                                //rpt.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.RptSample.rdlc";
                                //A4 end

                                ReportDataSource ds2 = new ReportDataSource("DataSet1", dssalessummaryObj1.Tables["DtA4Sales"]);
                                rpt.LocalReport.DataSources.Add(ds2);
                                rpt.LocalReport.ReportEmbeddedResource = "SalesProject.RptSalesAfour.rdlc";
                                //Passing Parmetes:

                                //ReportParameter rpReportOnName = new ReportParameter("CompanyName", Convert.ToString(vCompanyName), false);
                                //this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOnName });

                                ReportParameter rptSSS = new ReportParameter("CAddress1new", Convert.ToString(vCompanyName), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptSSS });

                                ReportParameter rpReportOn = new ReportParameter("CAddress", Convert.ToString(CompanyAddressLine1), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });

                                ReportParameter rpReportOn1 = new ReportParameter("BillNo", "FN0" + Convert.ToString(lblPreviosBillNo.Content), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOn1 });

                                ReportParameter rpReportOn2 = new ReportParameter("InvoiceName", Convert.ToString(vLedgerName), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOn2 });

                                ReportParameter rptInvoiceToAddress1 = new ReportParameter("InvToAddress", Convert.ToString(vInvToAddress), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceToAddress1 });

                                ReportParameter rptInvoiceToAddress3 = new ReportParameter("ShipName", Convert.ToString(vLedgerName), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceToAddress3 });

                                ReportParameter rptInvoiceToAddress2 = new ReportParameter("ToShipAddress1", Convert.ToString(vInvToAddress), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceToAddress2 });

                                ReportParameter rptInvoiceDate = new ReportParameter("InvoiceDate1", Convert.ToString(tBillDate.ToString("dd/MM/yyyy")), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceDate });

                                ReportParameter rptTerms = new ReportParameter("PaymentTerms", Convert.ToString(tLedgerLimitDays), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptTerms });

                                ReportParameter rptDueDate = new ReportParameter("DueDate", Convert.ToString(tDueDate.ToString("dd/MM/yyyy")), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptDueDate });

                                ReportParameter rptSalesmen = new ReportParameter("SalesmenName", Convert.ToString(tLedgerSalesmenName), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptSalesmen });

                                ReportParameter rptAliasName = new ReportParameter("AliasName", Convert.ToString(tLedgerAliasName), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptAliasName });

                                //ReportParameter rptDiscount = new ReportParameter("TotDiscount", Convert.ToString(string.Format("{0:0.00}", double.Parse(tDiscount.ToString()))), false);
                                //this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptDiscount });                                

                                ReportParameter rptGrandTotal = new ReportParameter("GrandTotal", Convert.ToString(string.Format("{0:0.00}", double.Parse(tGrandTotal.ToString()))), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptGrandTotal });

                                ReportParameter rptGSTAmount = new ReportParameter("TotGstAmt", Convert.ToString(string.Format("{0:0.00}", double.Parse(@tTotTax.ToString()))), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptGSTAmount });

                                //ReportParameter rptTAmount = new ReportParameter("TotNetAmt", "$" + Convert.ToString(string.Format("{0:0.00}", double.Parse(tlblBillAmtNew.ToString()))), false);
                                //this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptTAmount });

                                ReportParameter rptTAmount = new ReportParameter("TotNetAmt", "$" + Convert.ToString(string.Format("{0:0.00}", double.Parse(tGrandTotal.ToString()) + double.Parse(@tTotTax.ToString()))), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptTAmount });

                                ReportParameter rptCounter = new ReportParameter("CCounter", Convert.ToString(_Class.clsVariables.tCounterName), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptCounter });

                                ReportParameter rptDateTime = new ReportParameter("SysDateTime", Convert.ToString(dtdate), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptDateTime });

                                ReportParameter rptPayment = new ReportParameter("Paymentmode", Convert.ToString(strTNote), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptPayment });

                                ReportParameter rptRemarks = new ReportParameter("Remarks", Convert.ToString(tRemarks), false);
                                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptRemarks });


                                //  dsSalesSummaryObj.Tables["DsSalesRpt"].EndInit();
                                dssalessummaryObj1.Tables["DtA4Sales"].EndInit();
                                rpt.RefreshReport();
                                rpt.RenderingComplete += new RenderingCompleteEventHandler(PrintSales2);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void PrintSales2(object sender, RenderingCompleteEventArgs e)
        {
            try
            {
                rpt.PrintDialog();
                //tCount++;

                rpt.Clear();
                rpt.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
            }
        }
        ReportViewer rpt = new ReportViewer();
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales1 = new Microsoft.Reporting.WinForms.ReportViewer();
        public void PrintRemove(object sender, RenderingCompleteEventArgs e)
        {
            try
            {
                reportViewerSales1.PrintDialog();
                reportViewerSales1.Clear();
                reportViewerSales1.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private DataTable getDate()
        {

            DataTable _dt = new DataTable();
            _dt.Columns.Add("ItemName");
            _dt.Columns.Add("Qty");
            _dt.Columns.Add("Rate");
            _dt.Columns.Add("Amt");
            _dt.Columns.Add("Disc");
            _dt.Columns.Add("SDisc");
            _dt.Columns.Add("Other");


            return _dt;
        }

        public void CancelPrintJob()
        {
            //Checking Coding
            // Variable declarations.
            // bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            System.Management.ManagementObjectSearcher searchPrintJobs;
            System.Management.ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.
                searchPrintJobs = new System.Management.ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.
                prntJobCollection = searchPrintJobs.Get();

                // Look for the job you want to delete/cancel.
                foreach (System.Management.ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.
                    //  if (prntJobID == printJobID)
                    {
                        // Performs a action similar to the cancel
                        // operation of windows print console
                        prntJob.Delete();
                        //   isActionPerformed = true;
                        break;
                    }
                }
                // return isActionPerformed;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                // return false;
            }
            txtEnterValue.Focus();
        }

        public void PrintSales1(object sender, RenderingCompleteEventArgs e)
        {
            //Checking Coding
            try
            {
                //reportViewerSales.PrinterSettings.PrinterName = _Class.clsVariables.tPrinterName;
                //  reportViewerSales.PrinterSettings.PrintToFile = true;                
                reportViewerSales.PrintDialog();
                reportViewerSales.Clear();
                reportViewerSales.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        public void funDrawerOpen1()
        {
            //Check Cash Drawer Setting.. If Drawer enable Open Drawer
            //int tNoPrint = 0;
            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
            {
                if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                    cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);

                    SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
                        {
                            PrintDialog pd = new PrintDialog();
                            string s;
                            // code = null;
                            if (dtNew.Rows[0]["Action"].ToString().Trim() == "Open")
                            {
                                string[] byteStrings = dtNew.Rows[0]["DrawOpen"].ToString().Split(',');
                                byteOut = new byte[byteStrings.Length];
                                for (int i = 0; i < byteStrings.Length; i++)
                                {
                                    byteOut[i] = Convert.ToByte(byteStrings[i]);
                                }
                            }
                            s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

                            //  RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);
                            Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s));
                            workerThread.Start();
                            bool finished = workerThread.Join(3000);
                            if (!finished)
                            {
                                workerThread.Abort();
                            }


                        }
                    }
                }

            }
            txtEnterValue.Focus();
        }


        public void funBalanceAmtDisplay()
        {
            try
            {

                //Customer Display Balance Amt Display coding
                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {
                    tempTimer.Stop();
                    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                    _Class.clsVariables.spCustomerDis.WriteLine("BillNo : " + lblPreviosBillNo.Content);
                    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                    _Class.clsVariables.spCustomerDis.Write(lblRcvdAmt.Content.ToString() + "-" + lblBillAmt.Content.ToString() + "=" + lblRefundAmt.Content.ToString());
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
            txtEnterValue.Focus();
        }
        public void funThankYou()
        {
            //Customer Display Thank you msg display code
            try
            {
                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {
                    tempTimer.Stop();
                    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                    _Class.clsVariables.spCustomerDis.WriteLine("     THANK YOU");
                    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                    _Class.clsVariables.spCustomerDis.Write("     COME AGAIN");
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
            txtEnterValue.Focus();
        }


        string strSalesmenSales = "";
        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            // Bill Amount Cash Settle Code Start
            try
            {
                strSalesmenSales = "1";
                // if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                if (dt.Rows.Count > 0)
                {
                    DataTable dtSalesmen = new DataTable();
                    dtSalesmen.Clear();
                    SqlDataAdapter adp = new SqlDataAdapter("select salesmen from Control_table", con);
                    adp.Fill(dtSalesmen);
                    if (dtSalesmen.Rows.Count > 0)
                    {
                        strSales = dtSalesmen.Rows[0]["salesmen"].ToString();
                    }
                    if (strSales == "1")
                    {
                        DataTable dtSales = new DataTable();
                        dtSales.Rows.Clear();
                        SqlDataAdapter adpsalesmen = new SqlDataAdapter("select Ledger_Name as Salesmen_Name from Ledger_table where Ledger_groupno=51 and Ledger_no<>14", con);
                        adpsalesmen.Fill(dtSales);
                        if (dtSales.Rows.Count > 0)
                        {

                            if (uCSalesmen1.Visibility == Visibility.Visible)
                            {
                                uCSalesmen1.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                uCSalesmen1.Visibility = Visibility.Visible;
                                uCSalesmen1.txtNote.Focus();
                                // if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                                {
                                    bool isQtyChk = false;
                                    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                                    {
                                        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                                        if (tQty == 0)
                                        {
                                            isQtyChk = true;
                                        }
                                    }
                                    if (isQtyChk == false)
                                    {
                                        _Class.clsVariables.tNoRead = "NOREAD";
                                        uCSalesmen1.lblBillNo.Content = lblBillNo.Content.ToString();
                                        uCSalesmen1.lblTotQty.Content = lblTotQty.Content.ToString();
                                        uCSalesmen1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                                        uCSalesmen1.lblDiscount.Content = lblDiscount.Content.ToString();
                                        uCSalesmen1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                                        uCSalesmen1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();
                                        uCSalesmen1.dtDisplay.Rows.Clear();

                                        uCSalesmen1.SalesCreationEventHandlerNewSalesmen += new EventHandler(CloseEventSalemen);
                                    }
                                    else
                                    {
                                        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("There is no Salesmen", "Warning");
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            bool isQtyChk = false;
                            for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                            {
                                double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                                if (tQty == 0)
                                {
                                    isQtyChk = true;
                                }
                            }
                            if (isQtyChk == false)
                            {
                                WCFServices.Service1 objService = new WCFServices.Service1();
                                objService.btnCashButtonHome(lblTotAmt.Content.ToString(), lblNetAmt.Content.ToString(), lblTaxAmt.Content.ToString(), _Class.clsVariables.tUserNo, _Class.clsVariables.tCounter, dt, lblDiscount.Content.ToString(), string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType, _Class.clsVariables.dtSingleFree, _Class.clsVariables.tempsalesmenLedgerNo, _Class.clsVariables.tempsalesmenNote, _Class.clsVariables.dtserailno);

                                gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                                dtFreeBalance.Rows.Clear();
                                dt.Clear();
                                _Class.clsVariables.dtSingleFree.Rows.Clear();
                                frmDiscountDisplay.Visibility = Visibility.Hidden;
                                UCItemDiscount1.Visibility = Visibility.Hidden;
                                lblOverAllDiscAmt.Content = "0.00";
                                lblSpecialDiscAmt.Content = "0.00";
                                lblGroupDiscAmt.Content = "0.00";
                                lblNetAmt.Content = "0.00";
                                lblDiscount.Content = "0.00";
                                lblTotQty.Content = "0.00";
                                lblTotAmt.Content = "0.00";
                                lblTaxAmt.Content = "0.00";
                                funThankYou();
                                funPreviousBill();
                                funBalanceAmtDisplay();
                                funDrawerOpen();

                                for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                                {
                                    if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                                    {
                                        charPerLine = dtPrint.Rows[i]["Property"].ToString();
                                    }
                                    if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                                    {
                                        if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                                        {
                                            funPrevPrint();
                                            break;
                                        }
                                        else if (dtPrint.Rows[i]["Property"].ToString() == "After Confirm")
                                        {
                                            string res = MyMessageBox1.ShowBox("Do you want to print", "Warning");
                                            if (res == "1")
                                            {
                                                funPrevPrint();
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                strSalesmenSales = "";
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please select the product first!");
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please select the product first!");
                }
                vMainTable = "No";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");

            }
            //txtEnterValue.Focus();
            uCSalesmen1.txtNote.Focus();
            //_Class.clsVariables.tVoidActionType = "SALESMEN";
        }
        string stropen = "Yes";

        public void funDrawerOpen()
        {
            //Drawer OPen Code
            try
            {
                for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                {
                    if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                    {

                        // if (tNETS != "NETS")
                        {
                            tNETS = "";
                            DataTable dtNew = new DataTable();
                            dtNew.Rows.Clear();
                            SqlCommand cmd = new SqlCommand("Select * from CashDrawerSetting_table where Counter=@tCounter", con);
                            cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);

                            adp.Fill(dtNew);
                            if (dtNew.Rows.Count > 0)
                            {
                                if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
                                {
                                    PrintDialog pd = new PrintDialog();
                                    string s;
                                    // code = null;

                                    if (dtNew.Rows[0]["Action"].ToString().Trim() == "Open")
                                    {
                                        _Class.clsVariables.tempCashdrawstringopen = "No";
                                        string[] byteStrings = dtNew.Rows[0]["DrawOpen"].ToString().Split(',');
                                        byteOut = new byte[byteStrings.Length];
                                        for (int i = 0; i < byteStrings.Length; i++)
                                        {
                                            byteOut[i] = Convert.ToByte(byteStrings[i]);
                                        }
                                    }
                                    s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

                                    //  RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);

                                    Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s));
                                    workerThread.Start();
                                    bool finished = workerThread.Join(3000);
                                    if (!finished)
                                    {
                                        workerThread.Abort();
                                    }
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        DataSet dsTax = new DataSet();
        private void btnTender_Click(object sender, RoutedEventArgs e)
        {
            //Tender form Load coding and Pass Processing data 
            try
            {
                strSalesmenSales = "3";
                vMainTable = "No";
                UCFormSettle1.tTenderClose = "";
                // if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                if (dt.Rows.Count > 0)
                {
                    DataTable dtSalmen = new DataTable();
                    dtSalmen.Clear();
                    SqlDataAdapter adpSalmen = new SqlDataAdapter("select salesmen from Control_table", con);
                    adpSalmen.Fill(dtSalmen);
                    if (dtSalmen.Rows.Count > 0)
                    {
                        strSales = dtSalmen.Rows[0]["salesmen"].ToString();
                    }
                    if (strSales == "1")
                    {
                        DataTable dtSales = new DataTable();
                        dtSales.Rows.Clear();
                        SqlDataAdapter adpsalesmen = new SqlDataAdapter("select Ledger_Name as Salesmen_Name from Ledger_table where Ledger_groupno=51 and Ledger_no<>14", con);
                        adpsalesmen.Fill(dtSales);
                        if (dtSales.Rows.Count > 0)
                        {

                            if (uCSalesmen1.Visibility == Visibility.Visible)
                            {
                                uCSalesmen1.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                uCSalesmen1.Visibility = Visibility.Visible;
                                //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                                {
                                    bool isQtyChk = false;
                                    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                                    {
                                        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                                        if (tQty == 0)
                                        {
                                            isQtyChk = true;
                                        }
                                    }

                                    if (isQtyChk == false)
                                    {
                                        _Class.clsVariables.tNoRead = "NOREAD";



                                        ////  FormSettle frm = new FormSettle(dt);
                                        // //   frm.tempBillAmount = lblTotAmt.Content.ToString();
                                        //  UCFormSettle1.txtAmount.Text = lblNetAmt.Content.ToString();
                                        //  UCFormSettle1.currentDate = currentDate;
                                        //  UCFormSettle1.txtEnterValue.Text = "";
                                        //  UCFormSettle1.gridDisplay.DataSource = dt.DefaultView;
                                        // // UCFormSettle1.ds1.Tables.Add(dt.Copy());
                                        //  UCFormSettle1.gridDisplay.Columns[0].Width = 180;
                                        //  UCFormSettle1.gridDisplay.Columns[0].ReadOnly = true;
                                        //  UCFormSettle1.gridDisplay.Columns[1].Width = 50;
                                        //  UCFormSettle1.gridDisplay.Columns[2].Width = 50;
                                        //  UCFormSettle1.gridDisplay.Columns[3].Width = 50;
                                        //  UCFormSettle1.gridDisplay.Columns[3].ReadOnly = true;
                                        //  UCFormSettle1.gridDisplay.RowTemplate.Height = 35;
                                        //  UCFormSettle1.dtSettle.Rows.Clear();
                                        //  UCFormSettle1.lblBillNo.Content = lblBillNo.Content.ToString();
                                        //  UCFormSettle1.lblTotQty.Content = lblTotQty.Content.ToString();
                                        //  UCFormSettle1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                                        //  UCFormSettle1.lblDiscount.Content = lblDiscount.Content.ToString();
                                        //  UCFormSettle1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                                        //  UCFormSettle1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();

                                        _Class.clsVariables.tNoRead = "NOREAD";
                                        uCSalesmen1.lblBillNo.Content = lblBillNo.Content.ToString();
                                        uCSalesmen1.lblTotQty.Content = lblTotQty.Content.ToString();
                                        uCSalesmen1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                                        uCSalesmen1.lblDiscount.Content = lblDiscount.Content.ToString();
                                        uCSalesmen1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                                        uCSalesmen1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();
                                        uCSalesmen1.dtDisplay.Rows.Clear();

                                        uCSalesmen1.SalesCreationEventHandlerNewSalesmen += new EventHandler(CloseEventSalemen);

                                        //UCFormSettle1.dtDisplay.Rows.Clear();                        
                                        //for (int i = 0; i < dt.Rows.Count; i++)
                                        //{
                                        //    UCFormSettle1.dtDisplay.Rows.Add(Convert.ToString(dt.Rows[i][0]), Convert.ToString(dt.Rows[i][1]), Convert.ToString(dt.Rows[i][2]), Convert.ToString(dt.Rows[i][3]),Convert.ToString( dt.Rows[i][4]), Convert.ToString(dt.Rows[i][5]), Convert.ToString(dt.Rows[i][6]));
                                        //}

                                        //UCFormSettle1.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                                        //UCFormSettle1.SalesCreationEventHandlerNewCash += new EventHandler(CloseEvent2);
                                        //UCFormSettle1.SalesCreationEventHandlerNew1 += new EventHandler(CloseEvent);

                                        //UCFormSettle1.Visibility = Visibility.Visible;
                                        //UCfrmVoid1.Visibility = Visibility.Hidden;
                                        //CurrentBill.Visibility = Visibility.Hidden;
                                        //UCMain1.Visibility = Visibility.Hidden;
                                        // frm.ShowDialog();

                                        // lblRefundAmt.Content = string.Format("{0:0.00}", (frm.Refund.ToString() == "") ? 0.00 : double.Parse(frm.Refund.ToString()));



                                    }
                                    else
                                    {
                                        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                                    }
                                }
                                //else
                                //{
                                //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
                                //}
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("There is no Salesmen", "Warning");
                        }
                    }
                    else
                    {
                        // if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                        if (dt.Rows.Count > 0)
                        {
                            bool isQtyChk = false;
                            for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                            {
                                double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                                if (tQty == 0)
                                {
                                    isQtyChk = true;
                                }
                            }

                            if (isQtyChk == false)
                            {
                                _Class.clsVariables.tNoRead = "NOREAD";



                                //  FormSettle frm = new FormSettle(dt);
                                //   frm.tempBillAmount = lblTotAmt.Content.ToString();
                                UCFormSettle1.txtAmount.Text = lblNetAmt.Content.ToString();
                                UCFormSettle1.currentDate = currentDate;
                                UCFormSettle1.txtEnterValue.Text = "";
                                UCFormSettle1.gridDisplay.DataSource = dt.DefaultView;
                                // UCFormSettle1.ds1.Tables.Add(dt.Copy());
                                UCFormSettle1.gridDisplay.Columns[0].Width = 180;
                                UCFormSettle1.gridDisplay.Columns[0].ReadOnly = true;
                                UCFormSettle1.gridDisplay.Columns[1].Width = 50;
                                UCFormSettle1.gridDisplay.Columns[2].Width = 50;
                                UCFormSettle1.gridDisplay.Columns[3].Width = 50;
                                UCFormSettle1.gridDisplay.Columns[3].ReadOnly = true;
                                UCFormSettle1.gridDisplay.RowTemplate.Height = 35;
                                UCFormSettle1.dtSettle.Rows.Clear();
                                UCFormSettle1.lblBillNo.Content = lblBillNo.Content.ToString();
                                UCFormSettle1.lblTotQty.Content = lblTotQty.Content.ToString();
                                UCFormSettle1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                                UCFormSettle1.lblDiscount.Content = lblDiscount.Content.ToString();
                                UCFormSettle1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                                UCFormSettle1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();
                                UCFormSettle1.dtDisplay.Rows.Clear();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    UCFormSettle1.dtDisplay.Rows.Add(Convert.ToString(dt.Rows[i][0]), Convert.ToString(dt.Rows[i][1]), Convert.ToString(dt.Rows[i][2]), Convert.ToString(dt.Rows[i][3]), Convert.ToString(dt.Rows[i][4]), Convert.ToString(dt.Rows[i][5]), Convert.ToString(dt.Rows[i][6]));
                                }

                                UCFormSettle1.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                                UCFormSettle1.SalesCreationEventHandlerNewCash += new EventHandler(CloseEvent2);
                                UCFormSettle1.SalesCreationEventHandlerNew1 += new EventHandler(CloseEvent);

                                UCFormSettle1.Visibility = Visibility.Visible;
                                UCfrmVoid1.Visibility = Visibility.Hidden;
                                CurrentBill.Visibility = Visibility.Hidden;
                                UCMain1.Visibility = Visibility.Hidden;
                                // frm.ShowDialog();

                                // lblRefundAmt.Content = string.Format("{0:0.00}", (frm.Refund.ToString() == "") ? 0.00 : double.Parse(frm.Refund.ToString()));



                            }
                            else
                            {
                                MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please Select Product First", "Warning");
                        }
                        UCFormSettle1.txtEnterValue.Focus();
                    }

                }
                else
                {
                    MyMessageBox.ShowBox("Please select the product first!");
                }
                //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                //{
                //     bool isQtyChk = false;
                //    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                //    {
                //        double tQty=(gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "")?0.00:double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                //        if (tQty==0)
                //        {
                //            isQtyChk = true;
                //        }
                //    }

                //    if (isQtyChk == false)
                //    {
                //        _Class.clsVariables.tNoRead = "NOREAD";



                //      ////  FormSettle frm = new FormSettle(dt);
                //      // //   frm.tempBillAmount = lblTotAmt.Content.ToString();
                //      //  UCFormSettle1.txtAmount.Text = lblNetAmt.Content.ToString();
                //      //  UCFormSettle1.currentDate = currentDate;
                //      //  UCFormSettle1.txtEnterValue.Text = "";
                //      //  UCFormSettle1.gridDisplay.DataSource = dt.DefaultView;
                //      // // UCFormSettle1.ds1.Tables.Add(dt.Copy());
                //      //  UCFormSettle1.gridDisplay.Columns[0].Width = 180;
                //      //  UCFormSettle1.gridDisplay.Columns[0].ReadOnly = true;
                //      //  UCFormSettle1.gridDisplay.Columns[1].Width = 50;
                //      //  UCFormSettle1.gridDisplay.Columns[2].Width = 50;
                //      //  UCFormSettle1.gridDisplay.Columns[3].Width = 50;
                //      //  UCFormSettle1.gridDisplay.Columns[3].ReadOnly = true;
                //      //  UCFormSettle1.gridDisplay.RowTemplate.Height = 35;
                //      //  UCFormSettle1.dtSettle.Rows.Clear();
                //      //  UCFormSettle1.lblBillNo.Content = lblBillNo.Content.ToString();
                //      //  UCFormSettle1.lblTotQty.Content = lblTotQty.Content.ToString();
                //      //  UCFormSettle1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                //      //  UCFormSettle1.lblDiscount.Content = lblDiscount.Content.ToString();
                //      //  UCFormSettle1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                //      //  UCFormSettle1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();

                //        _Class.clsVariables.tNoRead = "NOREAD";
                //        uCSalesmen1.lblBillNo.Content = lblBillNo.Content.ToString();
                //        uCSalesmen1.lblTotQty.Content = lblTotQty.Content.ToString();
                //        uCSalesmen1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                //        uCSalesmen1.lblDiscount.Content = lblDiscount.Content.ToString();
                //        uCSalesmen1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                //        uCSalesmen1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();
                //        uCSalesmen1.dtDisplay.Rows.Clear();

                //        uCSalesmen1.SalesCreationEventHandlerNewSalesmen += new EventHandler(CloseEventSalemen);

                //        //UCFormSettle1.dtDisplay.Rows.Clear();                        
                //        //for (int i = 0; i < dt.Rows.Count; i++)
                //        //{
                //        //    UCFormSettle1.dtDisplay.Rows.Add(Convert.ToString(dt.Rows[i][0]), Convert.ToString(dt.Rows[i][1]), Convert.ToString(dt.Rows[i][2]), Convert.ToString(dt.Rows[i][3]),Convert.ToString( dt.Rows[i][4]), Convert.ToString(dt.Rows[i][5]), Convert.ToString(dt.Rows[i][6]));
                //        //}

                //        //UCFormSettle1.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                //        //UCFormSettle1.SalesCreationEventHandlerNewCash += new EventHandler(CloseEvent2);
                //        //UCFormSettle1.SalesCreationEventHandlerNew1 += new EventHandler(CloseEvent);

                //        //UCFormSettle1.Visibility = Visibility.Visible;
                //        //UCfrmVoid1.Visibility = Visibility.Hidden;
                //        //CurrentBill.Visibility = Visibility.Hidden;
                //        //UCMain1.Visibility = Visibility.Hidden;
                //       // frm.ShowDialog();

                //       // lblRefundAmt.Content = string.Format("{0:0.00}", (frm.Refund.ToString() == "") ? 0.00 : double.Parse(frm.Refund.ToString()));



                //    }
                //    else
                //    {
                //        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                //    }
                //}
                //else
                //{
                //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
                //}
                UCFormSettle1.txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }


        private void btnOption_Click(object sender, RoutedEventArgs e)
        {
            if (pnlNumeric.Visibility == Visibility.Hidden)
            {
                pnlNumeric.Visibility = Visibility.Visible;
            }
            else
            {

                pnlNumeric.Visibility = Visibility.Hidden;

            }
            txtEnterValue.Focus();
        }
        public static int clickCountGroup = 1;
        string query = null;
        int startingPosition, endingPosition;
        public static int clickCountGroupItem = 1;
        int tNextRecords = 0;

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //display next 35 group items
            tNextAction = "GroupItem";
            funNext();
            txtEnterValue.Focus();
        }
        string tNextAction = "";
        public void funNext()
        {

            try
            {
                if (pnlNumeric.Visibility == Visibility.Hidden)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (_Class.clsVariables.tHideKeyboard == true)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        pnlNumeric.Visibility = Visibility.Visible;
                    }
                }
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                //display next 7 group Details
                if (tNextAction.Trim() == "Group")
                {

                    if (clickCountGroup < tempGroupCount)
                    {
                        startingPosition = clickCountGroup * 7;
                        endingPosition = startingPosition + 8;
                        funFillGroup(startingPosition, endingPosition);
                        txtEnterValue.Focus();
                        clickCountGroup += 1;
                    }
                }
                //display next 35 group items
                if (tNextAction.Trim() == "GroupItem")
                {
                    if (btnGroupItem1.Content.ToString() != "")
                    {
                        if (clickCountGroupItem < tempGroupItemCount)
                        {
                            dtNew.Rows.Clear();
                            SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tValue", btnGroupItem1.Content.ToString().Trim());
                            cmd.Parameters.AddWithValue("@tActionType", "GROUPITEM");
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(dtNew);
                            //  dr = cmd.ExecuteReader();
                            // dtNew.Load(dr);
                            for (int mn = 0; mn < dtNew.Rows.Count; )
                            {
                                tempGroupNo = dtNew.Rows[mn]["Item_groupNo"].ToString();
                                if (dtNew.Rows[mn]["Group_Color"].ToString() == "")
                                {
                                    tempGroupColor = "DarkBlue";
                                }
                                else
                                {
                                    tempGroupColor = dtNew.Rows[mn]["Group_Color"].ToString();
                                }
                                if (dtNew.Rows[mn]["Font_Color"].ToString() == "")
                                {
                                    tempFontColor = "White";
                                }
                                else
                                {
                                    tempFontColor = dtNew.Rows[mn]["Font_Color"].ToString();
                                }
                                break;
                            }

                            tNextRecords = 0;
                            clickCountGroupItem += 1;
                            if (clickCountGroupItem == 0)
                            {
                                tNextRecords = 35;
                            }
                            else
                            {
                                tNextRecords = clickCountGroupItem * 35;
                            }
                            DataTable dtCmd21 = new DataTable();
                            dtCmd21.Rows.Clear();
                            //  SqlCommand cmd21 = new SqlCommand("select Top 27 * from Item_Table where item_groupNo=@tGroupNo and Item_name not in (Select Top (@tNextRecord) Item_name from Item_Table where item_groupNo=@tGroupNo order by Item_possition ASC) order by Item_possition ASC", con);
                            //SqlCommand cmd21 = new SqlCommand("sp_Next27Records", con);
                            //cmd21.CommandType = CommandType.StoredProcedure;                            
                            string tQuery = "select Top 35 * from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo and Item_name not in (Select Top (CONVERT(int,'" + tNextRecords + "')) Item_name from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo order by Item_possition ASC)  order by Item_possition ASC";
                            SqlCommand cmd21 = new SqlCommand(tQuery, con);

                            cmd21.Parameters.AddWithValue("@tGroupNo", int.Parse(tempGroupNo));
                            cmd21.Parameters.AddWithValue("@tNextRecord", Convert.ToInt32(tNextRecords));
                            SqlDataAdapter adpCmd21 = new SqlDataAdapter(cmd21);
                            adpCmd21.Fill(dtCmd21);
                            // dr = cmd2.ExecuteReader();
                            tempCount = 0;
                            int i = 0;
                            funGroupItemVisibility();
                            for (int mn = 0; mn < dtCmd21.Rows.Count; mn++)
                            {
                                i += 1;
                                funFillGroupItem(i, tempGroupColor, tempFontColor, dtCmd21.Rows[mn]["Item_Name"].ToString());
                            }

                            DataTable dtGroupChk = new DataTable();
                            dtGroupChk.Rows.Clear();
                            SqlCommand cmdChk = new SqlCommand("select (count(*)/35) as GroupItemCount,(count(*)%35) as Remining from item_table where Item_Active=1 and item_groupNo=@tGroupNo", con);
                            cmdChk.Parameters.AddWithValue("@tGroupNo", tempGroupNo.ToString());
                            SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
                            adpChk.Fill(dtGroupChk);
                            double tRemaining = 0;
                            double tGroupItemChk = 0;
                            if (dtGroupChk.Rows.Count > 0)
                            {
                                if (dtGroupChk.Rows[0]["Remining"].ToString() != null)
                                {
                                    tRemaining = double.Parse(dtGroupChk.Rows[0]["Remining"].ToString());
                                }
                                if (dtGroupChk.Rows[0]["GroupItemCount"].ToString() != null)
                                {
                                    tGroupItemChk = double.Parse(dtGroupChk.Rows[0]["GroupItemCount"].ToString());
                                }

                                if (double.Parse(dtGroupChk.Rows[0]["GroupItemCount"].ToString()) >= clickCountGroupItem)
                                {
                                    //clickCountGroupItem += 1;
                                }

                            }
                        }
                    }
                }
                //    txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public int tempGroupItemCount;
        string tempGroupColor, tempFontColor;
        private void btnGroup1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load clicked group containg items
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                pnlGroupItem.Visibility = Visibility.Visible;
                pnlGroupItem1.Visibility = Visibility.Hidden;
                pnlFreeItemDisplay.Visibility = Visibility.Hidden;
                if (pnlNumeric.Visibility == Visibility.Hidden)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (_Class.clsVariables.tHideKeyboard == true)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        pnlNumeric.Visibility = Visibility.Visible;
                    }
                }

                Button clickedButton = (Button)sender;
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tValue", clickedButton.Content.ToString().Trim());
                cmd.Parameters.AddWithValue("@tActionType", "GROUPITEM");
                SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                adpCmd.Fill(dtNew);
                // dr = cmd.ExecuteReader();
                //dtNew.Load(dr);
                for (int mn = 0; mn < dtNew.Rows.Count; )
                {
                    tempGroupNo = dtNew.Rows[mn]["Item_groupNo"].ToString();
                    if (dtNew.Rows[mn]["Group_Color"].ToString() == "")
                    {
                        tempGroupColor = "DarkBlue";
                    }
                    else
                    {
                        tempGroupColor = dtNew.Rows[mn]["Group_Color"].ToString();
                    }
                    if (dtNew.Rows[mn]["Font_Color"].ToString() == "")
                    {
                        tempFontColor = "White";
                    }
                    else
                    {
                        tempFontColor = dtNew.Rows[mn]["Font_Color"].ToString();
                    }
                    break;
                }

                dtNew.Rows.Clear();
                if (dtNew.Columns.Count > 0)
                {
                    dtNew.Columns.Clear();
                }
                SqlCommand cmd3 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@tValue", tempGroupNo.ToString());
                cmd3.Parameters.AddWithValue("@tActionType", "GROUPITEMFIRST");
                // dr = cmd3.ExecuteReader();
                // dtNew.Load(dr);
                SqlDataAdapter adpCmd3 = new SqlDataAdapter(cmd3);
                adpCmd3.Fill(dtNew);
                int i1 = 0, j = 35;
                tempGroupItemCount = 0;
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i1 += 1;
                    if (i1 == j)
                    {
                        tempGroupItemCount += 1;
                        j = j + 35;
                    }
                }




                DataTable dtCmd21 = new DataTable();
                dtCmd21.Rows.Clear();
                tNextRecords = 0;
                string tQuery = "select Top 35 * from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo and Item_name not in (Select Top (CONVERT(int,'" + tNextRecords + "')) Item_name from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo order by Item_possition ASC)  order by Item_possition ASC";
                //  SqlCommand cmd21 = new SqlCommand("sp_Next27Records", con);
                SqlCommand cmd21 = new SqlCommand(tQuery, con);
                // cmd21.CommandType = CommandType.StoredProcedure;                
                cmd21.Parameters.AddWithValue("@tGroupNo", int.Parse(tempGroupNo));
                cmd21.Parameters.AddWithValue("@tNextRecord", Convert.ToInt32(tNextRecords));
                SqlDataAdapter adpCmd21 = new SqlDataAdapter(cmd21);
                adpCmd21.Fill(dtCmd21);
                // dr = cmd2.ExecuteReader();
                tempCount = 0;
                int i = 0;
                funGroupItemVisibility();
                for (int mn = 0; mn < dtCmd21.Rows.Count; mn++)
                {
                    i += 1;
                    // Converting Language for ItemName //
                    Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                    Encoding Utf8 = Encoding.UTF8;
                    byte[] originalBytes = Windows1252.GetBytes(dtCmd21.Rows[mn]["Item_Name"].ToString());
                    string goodDecode = "";
                    goodDecode = Utf8.GetString(originalBytes);
                    //MessageBox.Show(goodDecode, "Re-decoded");
                    // Converting Language for ItemName //

                    funFillGroupItem(i, tempGroupColor, tempFontColor, goodDecode);
                    //funFillGroupItem(i, tempGroupColor, tempFontColor, dtCmd21.Rows[mn]["Item_Name"].ToString());
                }


                clickCountGroupItem = 0;
                // txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public void funFillGroup(int startingPosition, int endingPosition)
        {
            // Load Group Button Settings
            try
            {
                // btnGroup1.Visibility = Visibility.Hidden;
                btnGroup2.Visibility = Visibility.Hidden;
                btnGroup3.Visibility = Visibility.Hidden;
                btnGroup4.Visibility = Visibility.Hidden;
                btnGroup5.Visibility = Visibility.Hidden;
                btnGroup6.Visibility = Visibility.Hidden;
                btnGroup7.Visibility = Visibility.Hidden;
                btnGroup8.Visibility = Visibility.Hidden;
                //  btnGroup9.Visibility = Visibility.Hidden;
                for (int i = (startingPosition); i < dtGroup.Rows.Count && i <= endingPosition; i++)
                {
                    //  DataTable dtImage = new DataTable();
                    //  dtImage.Rows.Clear();
                    //  SqlCommand cmd12 = new SqlCommand("SELECT (Case when Item_Grouptable.ImageLocation IS null then '' else Item_Grouptable.ImageLocation END) as ImageLocation, ImageVisibility  FROM Item_Grouptable where Item_Grouptable.Group_visibility='True' and Item_groupname=@tGroupName", con);
                    //  cmd12.Parameters.AddWithValue("@tGroupName", dtGroup.Rows[i]["Item_groupname"].ToString());
                    ////  cmd12.CommandType = CommandType.StoredProcedure;                 
                    //  SqlDataAdapter adp4 = new SqlDataAdapter(cmd12);
                    //  adp4.Fill(dtImage);

                    if (dtGroup.Rows[i]["Group_Color"].ToString() == "")
                    {
                        tempGroupColor = "Green";
                    }
                    else
                    {
                        tempGroupColor = dtGroup.Rows[i]["Group_Color"].ToString();
                    }
                    if (dtGroup.Rows[i]["Font_Color"].ToString() == "")
                    {
                        tempFontColor = "White";
                    }
                    else
                    {
                        tempFontColor = dtGroup.Rows[i]["Font_Color"].ToString();
                    }

                    var bc = new BrushConverter();
                    //if (i == (startingPosition))
                    //{                      

                    //    btnGroup1.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    //    btnGroup1.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    //    btnGroup1.Visibility = Visibility.Visible;
                    //    btnGroup1.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                    //  //  btnGroup1.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Relative)) };
                    //    if (dtImage.Rows[0]["ImageLocation"].ToString().Trim() != "" && dtImage.Rows[0]["ImageVisibility"].ToString().Trim()=="True")
                    //    {
                    //        string tFileName = System.Windows.Forms.Application.StartupPath + dtImage.Rows[0]["ImageLocation"].ToString();
                    //        if (tFileName != "")
                    //        {
                    //            btnGroup1.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                    //        }
                    //    }
                    //}
                    if (i == (startingPosition))
                    {
                        btnGroup2.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup2.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup2.Visibility = Visibility.Visible;
                        btnGroup2.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();
                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup2.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                        //if (dtImage.Rows[0]["ImageLocation"].ToString().Trim() != "" && dtImage.Rows[0]["ImageVisibility"].ToString().Trim()=="True")
                        //{
                        //    string tFileName = System.Windows.Forms.Application.StartupPath + dtImage.Rows[0]["ImageLocation"].ToString();
                        //    if (tFileName != "")
                        //    {
                        //        if (File.Exists(tFileName))
                        //        {
                        //            btnGroup2.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                        //        }
                        //    }
                        //}
                    }
                    if (i == (startingPosition + 1))
                    {
                        btnGroup3.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup3.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup3.Visibility = Visibility.Visible;
                        btnGroup3.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();
                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup3.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                    }
                    if (i == (startingPosition + 2))
                    {
                        btnGroup4.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup4.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup4.Visibility = Visibility.Visible;
                        btnGroup4.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();
                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup4.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                    }
                    if (i == (startingPosition + 3))
                    {
                        btnGroup5.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup5.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup5.Visibility = Visibility.Visible;
                        btnGroup5.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();
                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup5.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                    }
                    if (i == (startingPosition + 4))
                    {
                        btnGroup6.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup6.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup6.Visibility = Visibility.Visible;
                        btnGroup6.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();

                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup6.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                    }
                    if (i == (startingPosition + 5))
                    {
                        btnGroup7.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup7.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup7.Visibility = Visibility.Visible;
                        btnGroup7.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();
                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup7.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                    }
                    if (i == (startingPosition + 6))
                    {
                        btnGroup8.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                        btnGroup8.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                        btnGroup8.Visibility = Visibility.Visible;
                        btnGroup8.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                        if (dtGroup.Rows[i]["ImageLocation"].ToString().Trim() != "" && dtGroup.Rows[i]["ImageVisibility"].ToString().Trim() == "True")
                        {
                            string tFileName = System.Windows.Forms.Application.StartupPath + dtGroup.Rows[i]["ImageLocation"].ToString();
                            if (tFileName != "")
                            {
                                if (File.Exists(tFileName))
                                {
                                    btnGroup8.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                                }
                            }
                        }
                        break;
                    }
                    //if (i == (startingPosition + 8))
                    //{
                    //    btnGroup9.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    //    btnGroup9.Foreground = (Brush)bc.ConvertFrom(tempFontColor);

                    //    btnGroup9.Visibility = Visibility.Visible;
                    //    btnGroup9.Content = dtGroup.Rows[i]["Item_groupname"].ToString();
                    //    if (dtImage.Rows[0]["ImageLocation"].ToString().Trim() != "" && dtImage.Rows[0]["ImageVisibility"].ToString().Trim()=="True")
                    //    {
                    //        string tFileName = System.Windows.Forms.Application.StartupPath + dtImage.Rows[0]["ImageLocation"].ToString();
                    //        if (tFileName != "")
                    //        {
                    //            btnGroup9.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(tFileName, UriKind.Relative)) };
                    //        }
                    //    }
                    //    break;
                    //}
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            //Load Previous Group Item Details
            tPreviousAction = "GroupItem";
            funPrevious();
            txtEnterValue.Focus();
        }
        string tPreviousAction = "";
        public void funPrevious()
        {
            try
            {
                if (pnlNumeric.Visibility == Visibility.Hidden)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (_Class.clsVariables.tHideKeyboard == true)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        pnlNumeric.Visibility = Visibility.Visible;
                    }
                }
                funConnectionStateCheck();
                //Load Group Details
                if (tPreviousAction.Trim() == "Group")
                {
                    if (clickCountGroup > 1)
                    {
                        clickCountGroup -= 1;
                        endingPosition = clickCountGroup * 7;
                        startingPosition = endingPosition - 7;
                        funFillGroup(startingPosition, endingPosition);
                        txtEnterValue.Focus();

                    }
                }
                //Load Previous Group Item Details
                if (tPreviousAction.Trim() == "GroupItem")
                {
                    if (btnGroupItem1.Visibility == Visibility.Visible)
                    {

                        if (btnGroupItem1.Content.ToString() != "")
                        {

                            if (clickCountGroupItem >= 0)
                            {
                                DataTable dtNew = new DataTable();
                                dtNew.Rows.Clear();
                                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tValue", btnGroupItem1.Content.ToString().Trim());
                                cmd.Parameters.AddWithValue("@tActionType", "GROUPITEM");
                                //  dr = cmd.ExecuteReader();
                                //  dtNew.Load(dr);
                                SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                                adpCmd.Fill(dtNew);
                                for (int mn = 0; mn < dtNew.Rows.Count; )
                                {
                                    tempGroupNo = dtNew.Rows[mn]["Item_groupNo"].ToString();
                                    if (dtNew.Rows[mn]["Group_Color"].ToString() == "")
                                    {
                                        tempGroupColor = "DarkBlue";
                                    }
                                    else
                                    {
                                        tempGroupColor = dtNew.Rows[mn]["Group_Color"].ToString();
                                    }
                                    if (dtNew.Rows[mn]["Font_Color"].ToString() == "")
                                    {
                                        tempFontColor = "White";
                                    }
                                    else
                                    {
                                        tempFontColor = dtNew.Rows[mn]["Font_Color"].ToString();
                                    }
                                    break;
                                }
                                tNextRecords = 0;
                                if (clickCountGroupItem != 0)
                                {
                                    clickCountGroupItem -= 1;
                                }
                                tNextRecords = (clickCountGroupItem) * 35;

                                if (clickCountGroupItem == 0)
                                {
                                    tNextRecords = 0;
                                    // clickCountGroupItem = 0;
                                }
                                if (clickCountGroupItem > 0)
                                {
                                    DataTable dtCmd2 = new DataTable();
                                    dtCmd2.Rows.Clear();
                                    //   SqlCommand cmd2 = new SqlCommand("select Top 27 * from Item_Table where Item_name not in (Select Top " + tNextRecords + " Item_name from Item_Table where item_groupNo='" + tempGroupNo.ToString() + "' order by Item_possition ASC) and item_groupNo='" + tempGroupNo.ToString() + "' order by Item_possition ASC", con);
                                    //SqlCommand cmd2 = new SqlCommand("sp_Next27Records", con);
                                    string tQuery = "select Top 35 * from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo and Item_name not in (Select Top (CONVERT(int,'" + tNextRecords + "')) Item_name from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo order by Item_possition ASC)  order by Item_possition ASC";
                                    SqlCommand cmd2 = new SqlCommand(tQuery, con);

                                    //  cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.AddWithValue("@tNextRecord", tNextRecords);
                                    cmd2.Parameters.AddWithValue("@tGroupNo", int.Parse(tempGroupNo));
                                    SqlDataAdapter adpCmd2 = new SqlDataAdapter(cmd2);
                                    adpCmd2.Fill(dtCmd2);
                                    // dr = cmd2.ExecuteReader();
                                    tempCount = 0;
                                    int i = 0;
                                    funGroupItemVisibility();
                                    for (int mn = 0; mn < dtCmd2.Rows.Count; mn++)
                                    {

                                        i += 1;
                                        funFillGroupItem(i, tempGroupColor, tempFontColor, dtCmd2.Rows[mn]["Item_Name"].ToString());

                                    }
                                }
                                if (clickCountGroupItem == 0)
                                {


                                    DataTable dtCmd2 = new DataTable();
                                    dtCmd2.Rows.Clear();
                                    //   SqlCommand cmd2 = new SqlCommand("select Top 27 * from Item_Table where Item_name not in (Select Top " + tNextRecords + " Item_name from Item_Table where item_groupNo='" + tempGroupNo.ToString() + "' order by Item_possition ASC) and item_groupNo='" + tempGroupNo.ToString() + "' order by Item_possition ASC", con);
                                    // SqlCommand cmd2 = new SqlCommand("sp_Next27Records", con);
                                    // cmd2.CommandType = CommandType.StoredProcedure;
                                    string tQuery = "select Top 35 * from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo and Item_name not in (Select Top (CONVERT(int,'" + tNextRecords + "')) Item_name from Item_Table where Item_Active=1 and item_groupNo=@tGroupNo order by Item_possition ASC)  order by Item_possition ASC";
                                    SqlCommand cmd2 = new SqlCommand(tQuery, con);
                                    tNextRecords = 0;
                                    cmd2.Parameters.AddWithValue("@tNextRecord", tNextRecords);
                                    cmd2.Parameters.AddWithValue("@tGroupNo", int.Parse(tempGroupNo));
                                    SqlDataAdapter adpCmd2 = new SqlDataAdapter(cmd2);
                                    adpCmd2.Fill(dtCmd2);
                                    // dr = cmd2.ExecuteReader();
                                    tempCount = 0;
                                    int i = 0;
                                    funGroupItemVisibility();
                                    for (int mn = 0; mn < dtCmd2.Rows.Count; mn++)
                                    {

                                        i += 1;
                                        funFillGroupItem(i, tempGroupColor, tempFontColor, dtCmd2.Rows[mn]["Item_Name"].ToString());

                                    }
                                    clickCountGroupItem = 0;
                                    txtEnterValue.Focus();
                                }

                            }
                        }
                    }
                }
                // txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public void funGroupItemVisibility()
        {
            btnGroupItem1.Visibility = Visibility.Hidden;
            btnGroupItem2.Visibility = Visibility.Hidden;
            btnGroupItem3.Visibility = Visibility.Hidden;
            btnGroupItem4.Visibility = Visibility.Hidden;
            btnGroupItem5.Visibility = Visibility.Hidden;
            btnGroupItem6.Visibility = Visibility.Hidden;
            btnGroupItem7.Visibility = Visibility.Hidden;
            btnGroupItem8.Visibility = Visibility.Hidden;
            btnGroupItem9.Visibility = Visibility.Hidden;
            btnGroupItem10.Visibility = Visibility.Hidden;
            btnGroupItem11.Visibility = Visibility.Hidden;
            btnGroupItem12.Visibility = Visibility.Hidden;
            btnGroupItem13.Visibility = Visibility.Hidden;
            btnGroupItem14.Visibility = Visibility.Hidden;
            btnGroupItem15.Visibility = Visibility.Hidden;
            btnGroupItem16.Visibility = Visibility.Hidden;
            btnGroupItem17.Visibility = Visibility.Hidden;
            btnGroupItem18.Visibility = Visibility.Hidden;
            btnGroupItem19.Visibility = Visibility.Hidden;
            btnGroupItem20.Visibility = Visibility.Hidden;
            btnGroupItem21.Visibility = Visibility.Hidden;
            btnGroupItem22.Visibility = Visibility.Hidden;
            btnGroupItem23.Visibility = Visibility.Hidden;
            btnGroupItem24.Visibility = Visibility.Hidden;
            btnGroupItem25.Visibility = Visibility.Hidden;
            btnGroupItem26.Visibility = Visibility.Hidden;
            btnGroupItem27.Visibility = Visibility.Hidden;
            btnGroupItem28.Visibility = Visibility.Hidden;
            btnGroupItem29.Visibility = Visibility.Hidden;
            btnGroupItem30.Visibility = Visibility.Hidden;
            btnGroupItem31.Visibility = Visibility.Hidden;
            btnGroupItem32.Visibility = Visibility.Hidden;
            btnGroupItem33.Visibility = Visibility.Hidden;
            btnGroupItem34.Visibility = Visibility.Hidden;
            btnGroupItem35.Visibility = Visibility.Hidden;
            txtEnterValue.Focus();
        }
        public void funFillGroupItem(int i, string tempGroupColor, string tempFontColor, string itemName)
        {
            try
            {
                string titemLocation = "";
                DataTable dtItemImage = new DataTable();
                dtItemImage.Rows.Clear();
                SqlCommand cmd12 = new SqlCommand(@"SELECT Item_table.Item_no,dbo.additionalinfo.items_color, dbo.additionalinfo.font_color, dbo.Item_table.ItemPicture
FROM         dbo.additionalinfo INNER JOIN
                      dbo.Item_table ON dbo.additionalinfo.Item_No = dbo.Item_table.Item_no where Item_table.Item_Active=1 and Item_table.Item_no=(Select Item_no from Item_table where Item_Active=1 and Item_name=@tItemName)", con);
                cmd12.Parameters.AddWithValue("@tItemName", itemName);
                //  cmd12.CommandType = CommandType.StoredProcedure;                 
                SqlDataAdapter adp4 = new SqlDataAdapter(cmd12);
                adp4.Fill(dtItemImage);
                if (dtItemImage.Rows.Count > 0)
                {
                    if (dtItemImage.Rows[0]["items_color"].ToString() == "")
                    {
                        tempGroupColor = "DarkBlue";
                    }
                    else
                    {
                        tempGroupColor = dtItemImage.Rows[0]["items_color"].ToString();
                    }
                    if (dtItemImage.Rows[0]["font_color"].ToString() == "")
                    {
                        tempFontColor = "White";
                    }
                    else
                    {
                        tempFontColor = dtItemImage.Rows[0]["font_color"].ToString();
                    }

                    if (dtItemImage.Rows[0]["ItemPicture"].ToString().Trim() != "")
                    {
                        titemLocation = System.Windows.Forms.Application.StartupPath + dtItemImage.Rows[0]["ItemPicture"].ToString();

                    }
                }

                if (i == 1)
                {
                    var bc = new BrushConverter();
                    btnGroupItem1.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem1.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem1.Visibility = Visibility.Visible;
                    //btnGroupItem1.FontStyle = 
                    btnGroupItem1.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem1.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }

                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 2)
                {
                    var bc = new BrushConverter();
                    btnGroupItem2.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem2.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem2.Visibility = Visibility.Visible;
                    btnGroupItem2.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem2.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 3)
                {
                    var bc = new BrushConverter();
                    btnGroupItem3.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem3.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem3.Visibility = Visibility.Visible;
                    btnGroupItem3.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem3.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 4)
                {
                    var bc = new BrushConverter();
                    btnGroupItem4.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem4.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem4.Visibility = Visibility.Visible;
                    btnGroupItem4.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem4.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 5)
                {
                    var bc = new BrushConverter();
                    btnGroupItem5.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem5.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem5.Visibility = Visibility.Visible;
                    btnGroupItem5.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem5.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 6)
                {
                    var bc = new BrushConverter();
                    btnGroupItem6.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem6.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem6.Visibility = Visibility.Visible;
                    btnGroupItem6.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem6.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 7)
                {
                    var bc = new BrushConverter();
                    btnGroupItem7.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem7.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem7.Visibility = Visibility.Visible;
                    btnGroupItem7.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem7.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 8)
                {
                    var bc = new BrushConverter();
                    btnGroupItem8.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem8.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem8.Visibility = Visibility.Visible;
                    btnGroupItem8.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem8.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 9)
                {
                    var bc = new BrushConverter();
                    btnGroupItem9.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem9.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem9.Visibility = Visibility.Visible;
                    btnGroupItem9.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem9.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 10)
                {
                    var bc = new BrushConverter();
                    btnGroupItem10.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem10.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem10.Visibility = Visibility.Visible;
                    btnGroupItem10.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem10.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 11)
                {
                    var bc = new BrushConverter();
                    btnGroupItem11.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem11.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem11.Visibility = Visibility.Visible;
                    btnGroupItem11.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem11.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 12)
                {
                    var bc = new BrushConverter();
                    btnGroupItem12.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem12.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem12.Visibility = Visibility.Visible;
                    btnGroupItem12.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem12.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 13)
                {
                    var bc = new BrushConverter();
                    btnGroupItem13.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem13.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem13.Visibility = Visibility.Visible;
                    btnGroupItem13.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem13.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 14)
                {
                    var bc = new BrushConverter();
                    btnGroupItem14.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem14.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem14.Visibility = Visibility.Visible;
                    btnGroupItem14.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem14.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 15)
                {
                    var bc = new BrushConverter();
                    btnGroupItem15.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem15.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem15.Visibility = Visibility.Visible;
                    btnGroupItem15.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem15.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 16)
                {
                    var bc = new BrushConverter();
                    btnGroupItem16.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem16.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem16.Visibility = Visibility.Visible;
                    btnGroupItem16.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem16.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 17)
                {
                    var bc = new BrushConverter();
                    btnGroupItem17.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem17.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem17.Visibility = Visibility.Visible;
                    btnGroupItem17.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem17.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 18)
                {
                    var bc = new BrushConverter();
                    btnGroupItem18.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem18.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem18.Visibility = Visibility.Visible;
                    btnGroupItem18.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem18.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 19)
                {
                    var bc = new BrushConverter();
                    btnGroupItem19.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem19.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem19.Visibility = Visibility.Visible;
                    btnGroupItem19.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem19.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 20)
                {
                    var bc = new BrushConverter();
                    btnGroupItem20.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem20.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem20.Visibility = Visibility.Visible;
                    btnGroupItem20.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem20.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 21)
                {
                    var bc = new BrushConverter();
                    btnGroupItem21.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem21.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem21.Visibility = Visibility.Visible;
                    btnGroupItem21.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem21.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 22)
                {
                    var bc = new BrushConverter();
                    btnGroupItem22.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem22.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem22.Visibility = Visibility.Visible;
                    btnGroupItem22.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem22.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 23)
                {
                    var bc = new BrushConverter();
                    btnGroupItem23.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem23.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem23.Visibility = Visibility.Visible;
                    btnGroupItem23.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem23.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 24)
                {
                    var bc = new BrushConverter();
                    btnGroupItem24.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem24.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem24.Visibility = Visibility.Visible;
                    btnGroupItem24.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem24.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 25)
                {
                    var bc = new BrushConverter();
                    btnGroupItem25.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem25.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem25.Visibility = Visibility.Visible;
                    btnGroupItem25.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem25.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 26)
                {
                    var bc = new BrushConverter();
                    btnGroupItem26.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem26.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem26.Visibility = Visibility.Visible;
                    btnGroupItem26.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem26.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (i == 27)
                {
                    var bc = new BrushConverter();
                    btnGroupItem27.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem27.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem27.Visibility = Visibility.Visible;
                    btnGroupItem27.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem27.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 28)
                {
                    var bc = new BrushConverter();
                    btnGroupItem28.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem28.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem28.Visibility = Visibility.Visible;
                    btnGroupItem28.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem28.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 29)
                {
                    var bc = new BrushConverter();
                    btnGroupItem29.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem29.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem29.Visibility = Visibility.Visible;
                    btnGroupItem29.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem29.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 30)
                {
                    var bc = new BrushConverter();
                    btnGroupItem30.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem30.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem30.Visibility = Visibility.Visible;
                    btnGroupItem30.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem30.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 31)
                {
                    var bc = new BrushConverter();
                    btnGroupItem31.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem31.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem31.Visibility = Visibility.Visible;
                    btnGroupItem31.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem31.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 32)
                {
                    var bc = new BrushConverter();
                    btnGroupItem32.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem32.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem32.Visibility = Visibility.Visible;
                    btnGroupItem32.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem32.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 33)
                {
                    var bc = new BrushConverter();
                    btnGroupItem33.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem33.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem33.Visibility = Visibility.Visible;
                    btnGroupItem33.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem33.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 34)
                {
                    var bc = new BrushConverter();
                    btnGroupItem34.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem34.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem34.Visibility = Visibility.Visible;
                    btnGroupItem34.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem34.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                if (i == 35)
                {
                    var bc = new BrushConverter();
                    btnGroupItem35.Background = (Brush)bc.ConvertFrom(tempGroupColor);
                    btnGroupItem35.Foreground = (Brush)bc.ConvertFrom(tempFontColor);
                    btnGroupItem35.Visibility = Visibility.Visible;
                    btnGroupItem35.Content = itemName;
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (File.Exists(titemLocation))
                            {
                                btnGroupItem35.Background = new System.Windows.Media.ImageBrush { ImageSource = new BitmapImage(new Uri(titemLocation, UriKind.Relative)) };
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string tNETS = "";
        private void btnNETS_Click(object sender, RoutedEventArgs e)
        {
            //Settle Bill Amount using NETS
            try
            {
                strSalesmenSales = "2";
                //  if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                if (dt.Rows.Count > 0)
                {
                    DataTable dtSalmen = new DataTable();
                    dtSalmen.Clear();
                    SqlDataAdapter adp = new SqlDataAdapter("select salesmen from Control_table", con);
                    adp.Fill(dtSalmen);
                    if (dtSalmen.Rows.Count > 0)
                    {
                        strSales = dtSalmen.Rows[0]["salesmen"].ToString();
                    }
                    if (strSales == "1")
                    {
                        DataTable dtSales = new DataTable();
                        dtSales.Rows.Clear();
                        SqlDataAdapter adpsalesmen = new SqlDataAdapter("select Ledger_Name as Salesmen_Name from Ledger_table where Ledger_groupno=51 and Ledger_no<>14", con);
                        adpsalesmen.Fill(dtSales);
                        if (dtSales.Rows.Count > 0)
                        {

                            if (uCSalesmen1.Visibility == Visibility.Visible)
                            {
                                uCSalesmen1.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                uCSalesmen1.Visibility = Visibility.Visible;
                                if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                                {
                                    bool isQtyChk = false;
                                    for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                                    {
                                        double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                                        if (tQty == 0)
                                        {
                                            isQtyChk = true;
                                        }
                                    }
                                    if (isQtyChk == false)
                                    {
                                        _Class.clsVariables.tNoRead = "NOREAD";
                                        uCSalesmen1.lblBillNo.Content = lblBillNo.Content.ToString();
                                        uCSalesmen1.lblTotQty.Content = lblTotQty.Content.ToString();
                                        uCSalesmen1.lblTotAmt.Content = lblTotAmt.Content.ToString();
                                        uCSalesmen1.lblDiscount.Content = lblDiscount.Content.ToString();
                                        uCSalesmen1.lblNetAmt.Content = lblNetAmt.Content.ToString();
                                        uCSalesmen1.lblTaxAmt.Content = lblTaxAmt.Content.ToString();
                                        uCSalesmen1.dtDisplay.Rows.Clear();

                                        uCSalesmen1.SalesCreationEventHandlerNewSalesmen += new EventHandler(CloseEventSalemen);

                                    }
                                    else
                                    {
                                        MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("There is no Salesmen", "Warning");
                        }
                    }
                    else
                    {
                        // if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                        if (dt.Rows.Count > 0)
                        {

                            bool isQtyChk = false;
                            for (int mn = 0; mn < gridItems.Rows.Count; mn++)
                            {
                                double tQty = (gridItems.Rows[mn].Cells["Qty"].Value.ToString() == "") ? 0.00 : double.Parse(gridItems.Rows[mn].Cells["Qty"].Value.ToString());
                                if (tQty == 0)
                                {
                                    isQtyChk = true;
                                }
                            }

                            if (isQtyChk == false)
                            {

                                WCFServices.Service1 objService = new WCFServices.Service1();
                                objService.btnNETSButtonHome(lblTotAmt.Content.ToString(), lblNetAmt.Content.ToString(), lblTaxAmt.Content.ToString(), _Class.clsVariables.tUserNo, _Class.clsVariables.tCounter, dt, lblDiscount.Content.ToString(), string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType, _Class.clsVariables.dtSingleFree, _Class.clsVariables.tempsalesmenLedgerNo, _Class.clsVariables.tempsalesmenNote, _Class.clsVariables.dtserailno);

                                gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                                dtFreeBalance.Rows.Clear();
                                _Class.clsVariables.dtSingleFree.Rows.Clear();
                                frmDiscountDisplay.Visibility = Visibility.Hidden;
                                UCItemDiscount1.Visibility = Visibility.Hidden;
                                lblOverAllDiscAmt.Content = "0.00";
                                lblSpecialDiscAmt.Content = "0.00";
                                lblGroupDiscAmt.Content = "0.00";
                                dt.Clear();
                                lblNetAmt.Content = "0.00";
                                lblDiscount.Content = "0.00";
                                lblTotQty.Content = "0.00";
                                lblTotAmt.Content = "0.00";
                                lblTaxAmt.Content = "0.00";
                                funThankYou();
                                funPreviousBill();
                                funBalanceAmtDisplay();
                                // funDrawerOpen();


                                for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                                {
                                    if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                                    {
                                        charPerLine = dtPrint.Rows[i]["Property"].ToString();
                                    }
                                    if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                                    {
                                        if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                                        {
                                            funPrevPrint();
                                            break;
                                        }
                                        else if (dtPrint.Rows[i]["Property"].ToString() == "After Confirm")
                                        {
                                            string res = MyMessageBox1.ShowBox("Do you want to print", "Warning");
                                            if (res == "1")
                                            {
                                                funPrevPrint();
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                strSalesmenSales = "";
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please select the product first!");
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please select the product first!");
                }

                tNETS = "NETS";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            //txtEnterValue.Focus();
            uCSalesmen1.txtNote.Focus();
            //_Class.clsVariables.tVoidActionType = "SALESMEN";
        }
        // FrmNumberBoard frm = new FrmNumberBoard();
        public string funholdLabel()
        {
            try
            {
                // Display Nuo. of Hold Bill
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "GETHOLDNO");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                // dr = cmd.ExecuteReader();
                // dtNew.Load(dr);
                return dtNew.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                return "0";
            }

        }
        private void popup_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                popup.IsOpen = false;
            }
            txtEnterValue.Focus();
        }
        public void CloseEvent(object sender, EventArgs e)
        {
            //No Need
            // tTenderClose = "Close";
            for (int i = 0; i < SalesPagePopup.Children.Count; i++)
            {
                SalesPagePopup.Children.RemoveAt(i);

            }
            popup.IsOpen = false;
            txtEnterValue.Focus();
        }
        int ijk = 0;

        public void CloseEvent2(object sender, EventArgs e)
        {
            //No Need
            // tTenderClose = "Close";
            //if (_Class.clsVariables.tempCashdrawstringopen == "Yes")
            {
                //string result = MyMessageBox1.ShowBox("Are you sure want to open cash draw.?", "Message");
                //  MessageBox.Show("BillNo: " + lblBillNo.Content + "  B.Amt: " + lblNetAmt.Content + " Recvd:" + lblBillNo.Content + " Change: Are you sure want to open cash draw.? ", "Msg");
                //if (result == "1")
                //{
                //    funDrawerOpen();
                //    ijk = ijk + 1;
                //}
                //else
                //{
                //    _Class.clsVariables.tempCashdrawstringopen  = "No";
                //}
            }
            for (int i = 0; i < SalesPagePopup.Children.Count; i++)
            {
                SalesPagePopup.Children.RemoveAt(i);
            }
            popup.IsOpen = false;
            gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;      
            dtFreeBalance.Rows.Clear();
            dt.Clear();
            _Class.clsVariables.dtSingleFree.Rows.Clear();
            frmDiscountDisplay.Visibility = Visibility.Hidden;
            UCItemDiscount1.Visibility = Visibility.Hidden;
            lblOverAllDiscAmt.Content = "0.00";
            lblSpecialDiscAmt.Content = "0.00";
            lblGroupDiscAmt.Content = "0.00";
            lblNetAmt.Content = "0.00";
            lblDiscount.Content = "0.00";
            lblTotQty.Content = "0.00";
            lblTotAmt.Content = "0.00";
            lblTaxAmt.Content = "0.00";
            funPreviousBill();
            funBalanceAmtDisplay();
            if (_Class.clsVariables.tempCashdrawstringopen == "Yes")
            {
                string str1 = "BillNo :  " + lblPreviosBillNo.Content + "            B.Amt :    " + lblBillAmt.Content;
                string str2 = "Recvd : " + lblRcvdAmt.Content;
                string str3 = "Change : " + lblRefundAmt.Content;
                //string result = MyMessageBox2.ShowBox("BillNo : " + lblPreviosBillNo.Content + " B.Amt : " + lblBillAmt.Content + "\nRecvd : " + lblRcvdAmt.Content + " Change : " + lblRefundAmt.Content, "Message");
                string result = MyMessageBox2.ShowBox(str1, str2, str3, "Message");

                if (result == "1")
                {
                    funDrawerOpen();
                    _Class.clsVariables.tempCashdrawstringopen = "No";
                }
                else
                {
                    _Class.clsVariables.tempCashdrawstringopen = "No";
                }
            }
            lblHold.Content = funholdLabel();
            txtEnterValue.Focus();
        }
        public void CloseEvent1(object sender, EventArgs e)
        {
            //No Need
            // tTenderClose = "Close";
            for (int i = 0; i < SalesPagePopup.Children.Count; i++)
            {
                SalesPagePopup.Children.RemoveAt(i);
            }
            popup.IsOpen = false;
            gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;      
            dtFreeBalance.Rows.Clear();
            dt.Clear();
            _Class.clsVariables.dtSingleFree.Rows.Clear();
            frmDiscountDisplay.Visibility = Visibility.Hidden;
            UCItemDiscount1.Visibility = Visibility.Hidden;
            lblOverAllDiscAmt.Content = "0.00";
            lblSpecialDiscAmt.Content = "0.00";
            lblGroupDiscAmt.Content = "0.00";
            lblNetAmt.Content = "0.00";
            lblDiscount.Content = "0.00";
            lblTotQty.Content = "0.00";
            lblTotAmt.Content = "0.00";
            lblTaxAmt.Content = "0.00";
            funPreviousBill();
            funBalanceAmtDisplay();
            lblHold.Content = funholdLabel();
            txtEnterValue.Focus();
        }
        public void HoldInsert_Event(object sender, EventArgs e)
        {
            //Hold Item Save Code start here

            try
            {
                funConnectionStateCheck();
                Button ClickedButton = (Button)sender;
                SqlCommand cmd1 = new SqlCommand("sp_SalesCreationHoldInsert", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@tClickedButton", ClickedButton.Content.ToString());

                for (int mnk = 0; mnk < dt.Rows.Count; mnk++)
                {
                    if (dt.Rows[mnk]["Disc"].ToString().Trim() == "")
                    {
                        dt.Rows[mnk]["Disc"] = "0.00";
                    }
                }
                cmd1.Parameters.AddWithValue("@tempTable", dt);
                cmd1.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                cmd1.Parameters.AddWithValue("@temCtrl_no", _Class.clsVariables.tCounter);
                cmd1.ExecuteNonQuery();
                dt.Rows.Clear();
                _Class.clsVariables.dtSingleFree.Rows.Clear();
                gridItems.DataSource = null;
                dtFreeBalance.Rows.Clear();
                frmDiscountDisplay.Visibility = Visibility.Hidden;
                UCItemDiscount1.Visibility = Visibility.Hidden;
                lblOverAllDiscAmt.Content = "0.00";
                lblSpecialDiscAmt.Content = "0.00";
                lblGroupDiscAmt.Content = "0.00";
                lblTaxAmt.Content = "0.00";
                lblTotQty.Content = "0.00";
                lblTotAmt.Content = "0.00";
                lblNetAmt.Content = "0.00";
                lblDiscount.Content = "0.00";
                lblHold.Content = funholdLabel();
                CloseEvent(sender, e);
                //  txtEnterValue.Focus();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public string NumberFormAction;
        private void btnHold_Click(object sender, RoutedEventArgs e)
        {
            //Load Holded item Details
            // pop Coding Start
            try
            {
                //     if (_Class.clsVariables.tDiscountLedger == "0")
                {
                    if (pnlNumeric.Visibility == Visibility.Hidden)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (_Class.clsVariables.tHideKeyboard == true)
                        {
                            pnlNumeric.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            pnlNumeric.Visibility = Visibility.Visible;
                        }
                    }
                    funConnectionStateCheck();
                    DataTable dsTempNew = new DataTable();
                    //SqlCommand adpHold = new SqlCommand("sp_SalesCreationSelectAll", con);
                    //adpHold.CommandType = CommandType.StoredProcedure;
                    //adpHold.Parameters.AddWithValue("@tActionType", "HOLDCHK");
                    //SqlDataAdapter adpHoldLoad = new SqlDataAdapter(adpHold);
                    //adpHoldLoad.Fill(dsTempNew);

                    //Anbu Change hold Process:
                    SqlCommand adpHold = new SqlCommand("SP_SelectQuery", con);
                    adpHold.CommandType = CommandType.StoredProcedure;
                    adpHold.Parameters.AddWithValue("@ActionType", "HOLDCHK");
                    //Hold Counter no Sent
                    adpHold.Parameters.AddWithValue("@itemName", _Class.clsVariables.tCounter);//Here Given Hold Number:
                    adpHold.Parameters.AddWithValue("@ItemCode", "");
                    SqlDataAdapter adpHoldLoad = new SqlDataAdapter(adpHold);
                    adpHoldLoad.Fill(dsTempNew);
                    //  dr = adpHold.ExecuteReader();
                    //  dsTempNew.Load(dr);
                    if (dsTempNew.Rows.Count <= 0 && gridItems.Rows.Count < 1)  // Change if (dsTempNew.Tables["Temp"].Rows.Count <= 0 && gridItems.Items.Count < 1)
                    {
                        MyMessageBox.ShowBox("No Items in the list", "Warning");
                        txtEnterValue.Focus();
                    }
                    else
                    {
                        FrmHold co = new FrmHold();
                        popup.Height = co.Height;
                        popup.Width = co.Width;
                        popup.IsOpen = true;
                        for (int i = 0; i < SalesPagePopup.Children.Count; i++)
                        {
                            SalesPagePopup.Children.RemoveAt(i);
                        }
                        co.SalesCreationEventHandler += new EventHandler(RefreshSalesCreation);
                        co.HoldInsertEventHandler += new EventHandler(HoldInsert_Event);
                        co.SalesCreationEventHandlerNew += new EventHandler(CloseEvent);
                        SalesPagePopup.Children.Insert(0, co);
                        txtEnterValue.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
            }
            //Popup Codeing End 

            txtEnterValue.Focus();
        }
        public string tHoldId;


        public void RefreshSalesCreation(object sender, EventArgs e)
        {
            try
            {
                bool tAllowResult = true;
                if (dt.Rows.Count > 0)
                {
                    if (MyMessageBox1.ShowBox("If you release holded item, you will lose currently displaying items from the list", "Warning") == "1")
                    {
                        tAllowResult = true;
                    }
                    else
                    {
                        tAllowResult = false;
                    }
                }
                if (tAllowResult == true)
                {
                    funConnectionStateCheck();
                    Button ClickedButton = (Button)sender;
                    dt.Rows.Clear();
                    SqlCommand adp = new SqlCommand("SP_RefreshSalesCreation", con);
                    adp.CommandType = CommandType.StoredProcedure;
                    adp.Parameters.AddWithValue("@tClickedButton", ClickedButton.Content.ToString());
                    adp.Parameters.AddWithValue("@CounterNo", _Class.clsVariables.tCounter);
                    SqlDataAdapter adpLoad = new SqlDataAdapter(adp);
                    adpLoad.Fill(dt);
                    double tOverAllDisc = 0;
                    for (int mn = 0; mn < dt.Rows.Count; mn++)
                    {
                        tOverAllDisc += string.IsNullOrEmpty(Convert.ToString(dt.Rows[mn]["Other"])) ? 0.00 : Convert.ToDouble(Convert.ToString(dt.Rows[mn]["Other"]));
                    }
                    lblOverAllDiscAmt.Content = string.Format("{0:0.00}", tOverAllDisc);
                    SqlCommand adp1 = new SqlCommand(@"Select  ItemName,Qty,ScannedQty,MainItemName,OfferName,OfferFreeQty,TotSaleQty from dtSingleFreeHoldInsert where HoldNo=@tClickedButton and Ctr_no=@CounterNo;
                Delete from dtSingleFreeHoldInsert where Holdno=@tClickedButton and Ctr_no=@CounterNo", con);
                    adp1.Parameters.AddWithValue("@tClickedButton", ClickedButton.Content.ToString());
                    adp1.Parameters.AddWithValue("@CounterNo", _Class.clsVariables.tCounter);
                    SqlDataAdapter adpLoadFree = new SqlDataAdapter(adp1);
                    adpLoadFree.Fill(_Class.clsVariables.dtSingleFree);
                    // dr = adp.ExecuteReader();
                    //dt.Load(dr);
                    gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                    gridItems.Columns[0].Width = 180;
                    gridItems.Columns[0].ReadOnly = true;
                    gridItems.Columns[1].Width = 50;
                    gridItems.Columns[2].Width = 50;
                    gridItems.Columns[3].Width = 50;
                    gridItems.Columns[3].ReadOnly = true;
                    gridItems.RowTemplate.Height = 35;
                    holdString = string.Empty;
                    funScrollGrid();
                    funDisplayAmount(dt);
                    gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;          
                    gridItems.Columns[0].Width = 180;
                    gridItems.Columns[0].ReadOnly = true;
                    gridItems.Columns[1].Width = 50;
                    gridItems.Columns[2].Width = 50;
                    gridItems.Columns[3].Width = 50;
                    gridItems.Columns[3].ReadOnly = true;
                    gridItems.RowTemplate.Height = 35;
                    funRoundCalculate();
                    lblHold.Content = funholdLabel();
                    gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
            }
            txtEnterValue.Focus();
        }
        byte[] byteOut;
        private void btnCashDraw_Click(object sender, RoutedEventArgs e)
        {
            //OPen Cash Drawer Code
            try
            {

                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlCommand cmdDrawer1 = new SqlCommand("Select User_type from User_table where User_No=@tUserno", con);
                cmdDrawer1.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmdDrawer1);
                adp1.Fill(dtNew1);
                if (dtNew1.Rows.Count > 0)
                {
                    if (dtNew1.Rows[0]["User_type"].ToString() == "0")
                    {
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();
                        SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                        cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                        SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                        adp.Fill(dtNew);
                        if (dtNew.Rows.Count > 0)
                        {
                            if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
                            {
                                PrintDialog pd = new PrintDialog();
                                string s;
                                // code = null;
                                if (dtNew.Rows[0]["Action"].ToString().Trim() == "Open")
                                {
                                    //object[] temp = txtDrawerOpen.Text.ToString().Split(',');

                                    //for (int i = 0; i < temp.Length; i++)
                                    //{
                                    //    code[i] =Convert.ToByte(temp[i]);
                                    //}

                                    string[] byteStrings = dtNew.Rows[0]["DrawOpen"].ToString().Split(',');
                                    byteOut = new byte[byteStrings.Length];
                                    for (int i = 0; i < byteStrings.Length; i++)
                                    {
                                        byteOut[i] = Convert.ToByte(byteStrings[i]);
                                    }
                                }
                                if (dtNew.Rows[0]["Action"].ToString().Trim() == "Cut")
                                {

                                    string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

                                    byteOut = new byte[byteStrings.Length];

                                    for (int i = 0; i < byteStrings.Length; i++)
                                    {

                                        byteOut[i] = Convert.ToByte(byteStrings[i]);

                                    }
                                    //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                }
                                if (dtNew.Rows[0]["Action"].ToString().Trim() == "Cut and Open")
                                {
                                    string[] byteStrings = dtNew.Rows[0]["CutAndOpen"].ToString().Split(',');
                                    byteOut = new byte[byteStrings.Length];
                                    for (int i = 0; i < byteStrings.Length; i++)
                                    {
                                        byteOut[i] = Convert.ToByte(byteStrings[i]);
                                    }
                                    //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                }
                                s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?
                                for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                                {
                                    if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                                    {
                                        //RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);

                                        Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s));
                                        workerThread.Start();
                                        bool finished = workerThread.Join(3000);
                                        if (!finished)
                                        {
                                            workerThread.Abort();
                                        }
                                    }
                                }
                            }
                        }
                        //// Allow the user to select a printer.
                        //PrintDialog pd = new PrintDialog();
                        ////  pd.PrinterSettings = new PrinterSettings();
                        //// if (DialogResult.OK == pd.ShowDialog(this))
                        //// {
                        //// Print the file to the printer.
                        ////    RawPrinterHelper.SendFileToPrinter("Zonerich AB-88H","123.txt");
                        ////}
                        ////}
                        //string s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                        //// open code-- 27, 112, 48, 55, 121 
                        //// cut code--29,86,66,0
                        ////cut and open--29 86 66 0 27 112 0 64 240
                        //// Allow the user to select a printer.
                        ////  PrintDialog pd = new PrintDialog();

                        ////pd.PrinterSettings = new PrinterSettings();
                        //// if (DialogResult.OK == pd.ShowDialog(this))
                        //// {
                        //// Send a printer-specific to the printer.
                        //for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                        //{
                        //    if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                        //    {
                        //        RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);                    
                        //    }
                        //}
                        ////    }
                        /// txtEnterValue.Focus();
                    }
                    else
                    {
                        if (frmCashDrawPassword1.Visibility == Visibility.Visible)
                        {

                            frmCashDrawPassword1.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            frmCashDrawPassword1.Visibility = Visibility.Visible;
                            frmCashDrawPassword1.SalesCreationEventHandlerPasswordClose += new EventHandler(CloseEvePasswordChk);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        public void CloseEvePasswordChk(object sender, EventArgs e)
        {
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
            cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
            SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
            adp.Fill(dtNew);
            if (dtNew.Rows.Count > 0)
            {
                if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
                {
                    PrintDialog pd = new PrintDialog();
                    string s;
                    // code = null;
                    if (dtNew.Rows[0]["Action"].ToString().Trim() == "Open")
                    {

                        string[] byteStrings = dtNew.Rows[0]["DrawOpen"].ToString().Split(',');
                        byteOut = new byte[byteStrings.Length];
                        for (int i = 0; i < byteStrings.Length; i++)
                        {
                            byteOut[i] = Convert.ToByte(byteStrings[i]);
                        }
                    }
                    if (dtNew.Rows[0]["Action"].ToString().Trim() == "Cut")
                    {

                        string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

                        byteOut = new byte[byteStrings.Length];

                        for (int i = 0; i < byteStrings.Length; i++)
                        {

                            byteOut[i] = Convert.ToByte(byteStrings[i]);

                        }
                        //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                    }
                    if (dtNew.Rows[0]["Action"].ToString().Trim() == "Cut and Open")
                    {
                        string[] byteStrings = dtNew.Rows[0]["CutAndOpen"].ToString().Split(',');
                        byteOut = new byte[byteStrings.Length];
                        for (int i = 0; i < byteStrings.Length; i++)
                        {
                            byteOut[i] = Convert.ToByte(byteStrings[i]);
                        }
                        //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                    }
                    s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?
                    for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                    {
                        if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                        {
                            //RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);

                            Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s));
                            workerThread.Start();
                            bool finished = workerThread.Join(3000);
                            if (!finished)
                            {
                                workerThread.Abort();
                            }
                        }
                    }
                }
            }

        }

        public string tDiscountAction = "";
        public string dummycode = "";
        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            //Load Over All Bill Discount form
            try
            {
                // if (_Class.clsVariables.tDiscountLedger == "0")
                //  if (Convert.ToDouble(Convert.ToString(lblTaxAmt.Content)) == 0)
                if (true)
                {
                    if (pnlNumeric.Visibility == Visibility.Hidden)
                    {
                        pnlNumeric.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        if (_Class.clsVariables.tHideKeyboard == true)
                        {
                            pnlNumeric.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            pnlNumeric.Visibility = Visibility.Visible;
                        }
                    }
                    if (lblTotAmt.Content.ToString() != "0.00")
                    {
                        // if (_Class.clsVariables.UserType != "1")
                        {
                            _Class.clsVariables.tDiscountAction = "Main";
                            frmDiscountDisplay.funLoadDiscount();
                            frmDiscountDisplay.Visibility = Visibility.Visible;
                            frmDiscountDisplay.tAmount = "";
                            frmDiscountDisplay.txtEnterDiscountValue.Text = "";
                            frmDiscountDisplay.tAmount = Convert.ToString(Convert.ToDouble(Convert.ToString(lblTotAmt.Content)) - (Convert.ToDouble(Convert.ToString(lblSpecialDiscAmt.Content)) + Convert.ToDouble(Convert.ToString(lblGroupDiscAmt.Content))));
                            if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                            {
                                frmDiscountDisplay.tAmount = Convert.ToString(Convert.ToDouble(frmDiscountDisplay.tAmount) + Convert.ToDouble(Convert.ToString(lblTaxAmt.Content)));
                            }


                            // frmDiscountDisplay.tAmount =Convert.ToString(lblNetAmt.Content);
                            _Class.clsVariables.tSNetAmt = lblNetAmt.Content.ToString();
                            //FrmDiscount frm = new FrmDiscount();
                            //frm.tAmount = lblTotAmt.Content.ToString();
                            //// frm.DiscountCreationEventHandler += new EventHandler(DiscountSalesCreation);
                            //frm.ShowDialog();
                            //////tempTimer.Interval = 1000;
                            //////tempTimer.Enabled = false;
                            //////tempTimer.Tick += new EventHandler(timer1_Tick);
                            //////tTimerCount = 0;
                            //////tempTimer.Start();
                            //////frmDiscountDisplay.Disc = "0";
                            //////if (frmDiscountDisplay.Disc == null)
                            //////{
                            //////    lblDiscount.Content = "0.00";
                            //////}
                            //////else
                            //////{
                            //////    lblDiscount.Content = String.Format("{0:0.00}", double.Parse(frmDiscountDisplay.Disc));
                            //////}
                            //////// double.Parse(frm.Disc).ToString("0.##");
                            ////////        Math.Round(double.Parse(frm.Disc),2).ToString();
                            //////if (lblDiscount.Content == null)
                            //////{
                            //////    lblDiscount.Content = "0.00";
                            //////}
                            //////lblNetAmt.Content = String.Format("{0:0.00}", ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString()))).ToString();
                            //////funRoundCalculate();
                        }
                        frmDiscountDisplay.txtEnterDiscountValue.Focus();
                    }
                    else
                    {
                        MyMessageBox.ShowBox("You Should Enter Minimum one Product or Check your Net Amount", "Warning");
                        txtEnterValue.Focus();
                    }

                }
                else
                {
                    //MyMessageBox.ShowBox("Tax item in the list. If you want to give discount use Itemwise or Groupwise Discount","Warning");
                    txtEnterValue.Focus();
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
            }
            txtEnterValue.Focus();
        }

        private void txtEnterValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (pnlGroupItem1.Visibility == Visibility.Visible)
                {
                    if (txtEnterValue.Text.Trim() != "")
                    {
                        if (e.Key == Key.Down)
                        {
                            if (listSelect.SelectedIndex < listSelect.Items.Count - 1)
                            {
                                listSelect.SelectedIndex = listSelect.SelectedIndex + 1;
                                listSelect.ScrollIntoView(listSelect.SelectedIndex);
                            }

                        }
                        if (e.Key == Key.Up)
                        {
                            if (listSelect.SelectedIndex > 0)
                            {
                                listSelect.SelectedIndex = listSelect.SelectedIndex - 1;
                                listSelect.ScrollIntoView(listSelect.SelectedIndex);
                            }
                        }
                    }
                    else
                    {
                        if (e.Key == Key.Up)
                        {
                            funBtnup();
                        }
                        if (e.Key == Key.Down)
                        {
                            funBtnDown();
                        }
                        if (e.Key == Key.Delete)
                        {
                            btnRemove_Click(sender, e);
                            funDisplayAmount(dt);
                            funRoundCalculate();
                        }
                    }
                    funScrollGrid();

                }
                else
                {

                    if (e.Key == Key.Up)
                    {
                        funBtnup();
                    }
                    if (e.Key == Key.Down)
                    {
                        funBtnDown();
                    }
                    if (e.Key == Key.Delete)
                    {
                        btnRemove_Click(sender, e);
                        funDisplayAmount(dt);
                        funRoundCalculate();
                    }
                    funScrollGrid();

                }

                // txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
            }
            txtEnterValue.Focus();
        }

        private void gridItems_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void gridItems_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyData == System.Windows.Forms.Keys.Up)
                {
                    funBtnup();
                }
                if (e.KeyData == System.Windows.Forms.Keys.Down)
                {
                    funBtnDown();
                }
                if (e.KeyData == System.Windows.Forms.Keys.Enter)
                {
                    funBtnSelect();
                    goto End1;
                }
                funScrollGrid();
            // funDisplayAmount(dt);
            End1:
                int ChkValue = 0;
                // txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
            }
            txtEnterValue.Focus();
        }


        public void funBtnSelect()
        {


            if (gridItems.Rows.Count > 0)
            {
                int row = gridItems.SelectedRows[0].Index;

                if (gridItems.Rows[row].Cells[1].ReadOnly == false)
                {
                    gridItems.CurrentCell = gridItems.Rows[row].Cells[1];
                }
                else if (gridItems.Rows[row].Cells[2].ReadOnly == false)
                {
                    gridItems.CurrentCell = gridItems.Rows[row].Cells[2];
                }
            }
            txtEnterValue.Focus();
        }

        private void txtEnterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                funConnectionStateCheck();
                pnlGroupItem1.Visibility = Visibility.Visible;
                pnlGroupItem.Visibility = Visibility.Hidden;
                pnlFreeItemDisplay.Visibility = Visibility.Hidden;
                var bc = new ImageBrush();
                pnlGroupItem1.Children.Clear();
                if (txtEnterValue.Text.Length > 0)
                {
                    int value;
                    if (!int.TryParse(txtEnterValue.Text, out value))
                    {
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();
                        SqlCommand cmd = new SqlCommand("SP_ITEMNAMESEARCH", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tEnterValue", txtEnterValue.Text.Trim());
                        ListBox l1 = new ListBox();
                        listSelect.FontSize = 25;
                        listSelect.Width = 495;
                        listSelect.Height = 640;
                        listSelect.Items.Clear();
                        SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                        adpCmd.Fill(dtNew);
                        //dr = cmd.ExecuteReader();
                        //dtNew.Load(dr);
                        for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                        {
                            listSelect.Items.Add(dtNew.Rows[mn]["Item_Name"].ToString());
                        }
                        //  con.Close();
                        //  listSelect.SelectedIndex = 0;
                        pnlGroupItem1.Children.Add(listSelect);
                    }
                    if (listSelect.Items.Count <= 0)
                    {
                        pnlGroupItem1.Visibility = Visibility.Hidden;
                        pnlFreeItemDisplay.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    pnlGroupItem1.Visibility = Visibility.Hidden;
                    pnlGroupItem.Visibility = Visibility.Visible;
                    pnlFreeItemDisplay.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
            }
            txtEnterValue.Focus();
        }

        private void listSelect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //con.Open();

                //string str = "select Item_no from  serialno_transtbl where barcodeno =(Select item_code from Item_table where Item_Active=1 and Item_name like @value)";
                //SqlCommand comm = new SqlCommand(str, con);
                //comm.CommandType = CommandType.Text;
                //comm.Parameters.AddWithValue("@value", listSelect.SelectedValue.ToString());
                ////comm.Parameters.Add(new SqlParameter("@tValue", SqlDbType.VarChar)).Value = listSelect.SelectedItem.ToString();
                //SqlDataReader reader = comm.ExecuteReader();
                //if (reader.Read())
                //{
                //    listSelect.Items.Add(reader[0].ToString());
                //    while (reader.Read())
                //    {
                //        listSelect.Items.Add(reader[0].ToString());
                //    }
                //    reader.Close();
                //}
                //else
                //{
                bool isChkStopAtRate = false;
                var bc = new BrushConverter();
                lblLogo.Foreground = (Brush)bc.ConvertFrom("#FFADF213");
                funConnectionStateCheck();
                DataRow dr = null;
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                if (listSelect.SelectedItems.Count > 0)
                {
                    tempTimer.Interval = 1000;
                    tempTimer.Enabled = false;
                    tempTimer.Tick += new EventHandler(timer1_Tick);
                    tTimerCount = 0;
                    // tempTimer.Start();
                    DataTable dtNew1 = new DataTable();
                    DataTable dtTable = new DataTable();
                    //  SqlDataReader dr12 = null;
                    dtNew1.Rows.Clear();
                    SqlCommand cmdd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                    cmdd.CommandType = CommandType.StoredProcedure;
                    if (listSelect.IsVisible == true)
                    {
                        cmdd.Parameters.AddWithValue("@tValue", listSelect.SelectedItem.ToString());
                        //dummycode = listSelect.SelectedItem.ToString();
                    }
                    else
                    {
                        cmdd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                    }
                    cmdd.Parameters.AddWithValue("@tActionType", "SERIAL");
                    SqlDataAdapter adpCmd11 = new SqlDataAdapter(cmdd);
                    adpCmd11.Fill(dtTable);
                    if (dtTable.Rows.Count != 0)
                    {
                        listSelect.Items.Clear();

                        if (_Class.clsVariables.dtserailno.Rows.Count == 0)
                        {
                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                listSelect.Items.Add(dtTable.Rows[i]["Item_no"].ToString());
                                dummycode = dtTable.Rows[i]["barcodeno"].ToString();
                            }
                        }
                        else
                        {
                            int t = 0;
                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                for (int j = 0; j < _Class.clsVariables.dtserailno.Rows.Count; j++)
                                {
                                    if (dtTable.Rows[i]["Item_no"].ToString() != _Class.clsVariables.dtserailno.Rows[j]["Serial_no"].ToString())
                                        t = 1;
                                    else
                                    {
                                        DataRow drs = dtTable.Rows[i];
                                        drs.Delete();
                                        dtTable.AcceptChanges();
                                    }
                                }
                            }

                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                listSelect.Items.Add(dtTable.Rows[i]["Item_no"].ToString());
                                dummycode = dtTable.Rows[i]["barcodeno"].ToString();
                            }
                        }
                    }
                    // No serial stock 

                    else
                    {
                        //MessageBox.Show("No Stock");                             
                        string sstr = "-";
                        SqlCommand scmd = new SqlCommand("select * from serialno_transtbl where item_no like '%" + listSelect.SelectedItem.ToString() + "%'", con);
                        scmd.ExecuteNonQuery();
                        SqlDataReader rdr = scmd.ExecuteReader();
                        if (rdr.Read())
                            sstr = sstr + rdr[2].ToString();
                        rdr.Close();

                        string itm = listSelect.SelectedItem.ToString();
                        listSelect.Items.Remove(itm);
                        // MessageBox.Show(itm);
                        _Class.clsVariables.dtserailno.Rows.Add(itm);
                        string temp = "";

                        SqlCommand comd = new SqlCommand("select Item_name from item_table where item_code=(select barcodeno from serialno_transtbl where inout = 1 and item_no='" + itm + "')", con);
                        SqlDataReader reader = comd.ExecuteReader();

                        if (reader.Read())
                        {
                            temp = reader[0].ToString();
                        }
                        //else
                        //{
                        //    MessageBox.Show("No Stock");
                        //    return;
                        //}
                        reader.Close();

                        if (temp != "")
                            itm = temp;
                        //dummycode = "";
                        //SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectSingle", con);
                        //cmd.CommandType = CommandType.StoredProcedure;
                        SqlCommand cmd = new SqlCommand("Select item_name from Item_table where Item_Active=1 and Item_name Like @tValue OR Item_Code Like @tValue", con);
                        cmd.CommandType = CommandType.Text;
                        if (listSelect.IsVisible == true)
                        {
                            cmd.Parameters.AddWithValue("@tValue", itm);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                        }

                        //cmd.Parameters.AddWithValue("@tActionType", "TXTBOXVALUE");
                        SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                        adpCmd.Fill(dtNew1);
                        // dr12 = cmd.ExecuteReader();
                        // dtNew1.Load(dr12);
                        int isRecord = 0;
                        for (int mn = 0; mn < dtNew1.Rows.Count; )
                        {
                            isRecord = 1;
                            rowIndex = 0;
                            dr = dt.NewRow();
                            //   MessageBox.Show(dr12["Item_Name"].ToString());
                            //string iname = dtNew1.Rows[mn]["Item_Name"].ToString();
                            //int ni = iname.IndexOf("-");
                            //iname = iname.Substring(0, ni);
                            int ix = sstr.IndexOf("-");
                            string sstr1 = sstr.Substring(ix + 1, sstr.Length - 1);
                            //SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle2", con);
                            SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.AddWithValue("@tValue", dtNew1.Rows[mn]["Item_Name"].ToString());
                            //cmd1.Parameters.AddWithValue("@tSerial", sstr1);
                            cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
                            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
                            adpCmd1.Fill(dtNew);
                            // reader = cmd1.ExecuteReader();
                            // dtNew.Load(reader);
                            if (dtNew.Rows.Count > 0)
                            {
                                count = 0;
                                totAmt = 0.00;
                                totQty = 0.00;
                                totTax = 0.00;
                                string tempItemName = dtNew.Rows[mn]["Item_Name"].ToString();
                                tItemNameGlob = tempItemName;
                                double tUnitDecimals = double.Parse(dtNew.Rows[mn]["unit_Decimals"].ToString());
                                string tWeightScale = dtNew.Rows[mn]["WeightScale"].ToString();
                                double tReadingValue = 0;

                                DataTable dtItem = new DataTable();
                                dtItem.Rows.Clear();
                                SqlCommand cmd12 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where Item_Active=1  and item_name=@tItemName", con);
                                cmd12.Parameters.AddWithValue("@tItemName", tempItemName);
                                SqlDataAdapter adp = new SqlDataAdapter(cmd12);
                                adp.Fill(dtItem);
                                bool isChkOpenItem = false;
                                if (dtItem.Rows.Count > 0)
                                {
                                    isChkStopAtRate = Convert.ToBoolean(dtItem.Rows[0]["StopatQty"].ToString());
                                    if (dtItem.Rows[0]["OpenItem"].ToString() == "True")
                                    {
                                        isChkOpenItem = true;
                                    }
                                }

                                if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                                {
                                    if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                                    {
                                    ReadAgain:
                                        try
                                        {
                                            tReadCount = 0;
                                            string data = "";
                                            data = _Class.clsVariables.serial.ReadExisting();
                                            //serial.Close();
                                            if (data.IndexOf("kg") > 0)
                                            {
                                                data = data.Substring(0, data.IndexOf("kg"));
                                                data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                // if
                                                tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                            }
                                            else if (data.IndexOf("k") > 0)
                                            {
                                                data = data.Substring(0, data.IndexOf("k"));
                                                data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                                tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                            }
                                        }
                                        catch (Exception)
                                        {
                                            tReadCount++;
                                            if (tReadCount < 10)
                                            {
                                                goto ReadAgain;
                                            }
                                            else
                                            {
                                                tShowQty = "";
                                                MyMessageBox.ShowBox("Weight scale device not ready to use", "Warning");
                                                tShowQty = "Show";

                                            }
                                        }
                                    }
                                    else
                                    {
                                        // tReadingValue = 1;
                                        if (isChkStopAtRate == true)
                                        {
                                            tReadingValue = 0;
                                        }
                                        else
                                        {
                                            //tempTimer.Start();
                                            tReadingValue = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    // tReadingValue = 1;
                                    if (isChkStopAtRate == true)
                                    {
                                        tReadingValue = 0;
                                    }
                                    else
                                    {
                                        //tempTimer.Start();
                                        tReadingValue = 1;
                                    }
                                }

                                foreach (DataRow dr1 in dt.Rows)
                                {
                                    if (dr1["itemName"].ToString() == tempItemName)
                                    {
                                        if (isChkOpenItem != true)
                                        {
                                            count = 1;

                                            if (tUnitDecimals == 0)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N0");
                                            }
                                            if (tUnitDecimals == 1)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N1");
                                            }
                                            if (tUnitDecimals == 2)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N2");
                                            }
                                            if (tUnitDecimals == 3)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N3");
                                            }
                                            if (tUnitDecimals == 4)
                                            {
                                                dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + tReadingValue).ToString("N4");
                                            }


                                            {
                                                dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())).ToString());
                                            }

                                            // dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + 1).ToString();
                                            //dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString())).ToString());
                                            gridItems.Rows[rowIndex].Selected = true;
                                            rowSelect = "";

                                            tReadingValueDisplay = double.Parse(dt.Rows[rowIndex]["Qty"].ToString());
                                            ClickedButtonDisplay = Convert.ToString(tempItemName);
                                            drQtyDisplay = Convert.ToString(dt.Rows[rowIndex]["Qty"]);
                                            drRateDisplay = Convert.ToString(dr1["Rate"]);
                                            drAmtDisplay = Convert.ToString(dt.Rows[rowIndex]["Amt"]);


                                        }
                                    }
                                    rowIndex += 1;
                                }
                                if (count == 0)
                                {
                                    if (sstr.Length > 1)
                                        dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString() + sstr;
                                    else
                                        dr["ItemName"] = dtNew.Rows[mn]["Item_name"].ToString();
                                    dr["Serial"] = sstr1;
                                    if (tUnitDecimals == 0)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N0");
                                    }
                                    if (tUnitDecimals == 1)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N1");
                                    }
                                    if (tUnitDecimals == 2)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N2");
                                    }
                                    if (tUnitDecimals == 3)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N3");
                                    }
                                    if (tUnitDecimals == 4)
                                    {
                                        dr["Qty"] = tReadingValue.ToString("N4");
                                    }

                                    //dr["Qty"] = "1";
                                    dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                    {
                                        dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                    }
                                    //  dr["Amt"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[mn]["Item_mrsp"].ToString()));
                                    dt.Rows.Add(dr);
                                    // funReplaceFreeItemAmt();
                                    rowSelect = "Last";
                                    tSelectedRowIndex = dt.Rows.Count;

                                    tReadingValueDisplay = 1;
                                    ClickedButtonDisplay = Convert.ToString(tempItemName);
                                    drQtyDisplay = Convert.ToString(dr["Qty"]);
                                    drRateDisplay = Convert.ToString(dr["Rate"]);
                                    drAmtDisplay = Convert.ToString(dr["Amt"]);

                                }
                                funStockDisplay(tempItemName);
                                funDisplayAmount(dt);
                                if (rowSelect != "")
                                {
                                    gridItems.DataSource = dt.DefaultView;   // Change gridItems.ItemsSource = dt.DefaultView;
                                    gridItems.Columns[0].Width = 180;
                                    gridItems.Columns[0].ReadOnly = true;
                                    gridItems.Columns[1].Width = 50;
                                    gridItems.Columns[2].Width = 50;
                                    gridItems.Columns[3].Width = 50;
                                    gridItems.Columns[3].ReadOnly = true;
                                    gridItems.RowTemplate.Height = 35;
                                }
                                gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                                funScrollGrid();
                                funStopAtQtyAndRate(tempItemName, tSelectedRowIndex);
                                funCustomerDisplay(tReadingValueDisplay, ClickedButtonDisplay, drQtyDisplay, drRateDisplay, drAmtDisplay);
                                funRoundCalculate();
                            }
                            break;
                        }
                        if (isRecord == 0)
                        {
                            MyMessageBox.ShowBox("Product Not Found", "Warning");
                        }
                        txtEnterValue.Text = "";
                        // txtEnterValue.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");

            }
            txtEnterValue.Focus();
        }

        private void listSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(listSelect.SelectedItem.ToString());
            if (listSelect.SelectedItems.Count > 0)
            {
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
            }
            txtEnterValue.Focus();
        }

        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            if (pnlNumeric.Visibility == Visibility.Hidden)
            {
                pnlNumeric.Visibility = Visibility.Hidden;
            }
            else
            {
                if (_Class.clsVariables.tHideKeyboard == true)
                {
                    pnlNumeric.Visibility = Visibility.Hidden;
                }
                else
                {
                    pnlNumeric.Visibility = Visibility.Visible;
                }
            }
            //if (_Class.clsVariables.UserType != "1")
            //{


            // frmVoid frm = new frmVoid();                
            // frm.currentDate = currentDate;
            SqlCommand cmdUpgrade = new SqlCommand("sp_btnUpgradeSales", con);
            cmdUpgrade.CommandType = CommandType.StoredProcedure;
            cmdUpgrade.ExecuteNonQuery();
            UCfrmVoid1.funVoidLoad();
            CurrentBill.Visibility = Visibility.Hidden;
            UCFormSettle1.Visibility = Visibility.Hidden;
            UCfrmVoid1.Visibility = Visibility.Visible;
            UCMain1.Visibility = Visibility.Hidden;
            UCfrmVoid1.currentDate = currentDate;

            txtEnterValue.Focus();
            vMainTable = "Yes";
        }

        public void CloseEventPassword1(object sender, EventArgs e)
        {
            //No Need
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd = new SqlCommand("Select * from User_table with (index(IndexUser_table)) where User_pass=@tPassword and User_type=0", con);
            cmd.Parameters.AddWithValue("@tPassword", _Class.clsVariables.tVoidValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dtNew);
            if (dtNew.Rows.Count > 0)
            {
                //FrmDiscount frm = new FrmDiscount();
                // frm.ShowDialog();
            }
            else
            {
                MyMessageBox.ShowBox("Invalid Password..Please get user rights to open Void Form!!", "Warning");
            }

            txtEnterValue.Focus();
        }
        public void CloseEventPassword(object sender, EventArgs e)
        {
            //No  Need
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd = new SqlCommand("Select * from User_table with (index(IndexUser_table)) where User_pass=@tPassword and User_type=0", con);
            cmd.Parameters.AddWithValue("@tPassword", _Class.clsVariables.tVoidValue);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dtNew);
            if (dtNew.Rows.Count > 0)
            {
                //frmVoid frm = new frmVoid();
                //frm.ShowDialog();
            }
            else
            {
                MyMessageBox.ShowBox("Invalid Password..Please get user rights to open Void Form!!", "Warning");
            }

            txtEnterValue.Focus();
        }

        public void funDiscountAmtLoad()
        {
            //No need
            try
            {
                double tPrevDisc = 0.00;
                for (int ijk = 0; ijk < gridItems.Rows.Count; ijk++)
                {
                    if (gridItems.Rows[ijk].Cells["Disc"].Value.ToString() != "" && gridItems.Rows[ijk].Cells["Disc"].Value.ToString() != null)
                    {
                        tPrevDisc += double.Parse(gridItems.Rows[ijk].Cells["Disc"].Value.ToString());
                    }
                }
                lblDiscount.Content = String.Format("{0:0.00}", (tPrevDisc));
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }
        private void gridItems_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            // need to change F4 key also
            UCItemDiscount1.Visibility = Visibility.Hidden;
            frmDiscountDisplay.Visibility = Visibility.Hidden;
            UCPriceChange1.Visibility = Visibility.Hidden;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                if (dt.Rows.Count > 0)
                {
                    _Class.clsVariables.tDiscountAction = "ItemDiscount";
                    //if (_Class.clsVariables.tDiscountLedger == "1")

                    {
                        try
                        {
                            if (pnlNumeric.Visibility == Visibility.Hidden)
                            {
                                pnlNumeric.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                if (_Class.clsVariables.tHideKeyboard == true)
                                {
                                    pnlNumeric.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    pnlNumeric.Visibility = Visibility.Visible;
                                }
                            }
                            double tItemAmount = (gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value.ToString().Trim() == "") ? 0.00 : Convert.ToDouble(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value.ToString());

                            _Class.clsVariables.itemIndex = gridItems.CurrentCell.RowIndex;
                            _Class.clsVariables.itemRate = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Rate"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Rate"].Value);
                            _Class.clsVariables.itemQty = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Qty"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Qty"].Value);
                            _Class.clsVariables.itemAmt = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value);
                            _Class.clsVariables.itemName = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["ItemName"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["ItemName"].Value);
                            _Class.clsVariables.itemDisc = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Disc"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Disc"].Value);
                            _Class.clsVariables.itemSDisc = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["SDisc"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["SDisc"].Value);
                            _Class.clsVariables.itemOther = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Other"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Other"].Value);
                            //UCItemDiscount1.txtEven.Text = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Qty"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Qty"].Value);

                            int tCurrentRowIndex = gridItems.CurrentCell.RowIndex;
                            //UCItemDiscount1.funFrmSplitLoad();
                            UCItemDiscount1.Visibility = Visibility.Visible;
                            frmDiscountDisplay.tAmount = "";
                            frmDiscountDisplay.txtEnterDiscountValue.Text = "";
                            double tAmtNew = (Convert.ToDouble(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value))) - ((Convert.ToDouble(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["SDisc"].Value))) + (Convert.ToDouble(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Other"].Value))));
                            //frmDiscountDisplay.tAmount = string.IsNullOrEmpty(Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value)) ? "0.00" : Convert.ToString(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Amt"].Value);
                            frmDiscountDisplay.tAmount = string.IsNullOrEmpty(Convert.ToString(tAmtNew)) ? "0.00" : Convert.ToString(tAmtNew);

                            UCItemDiscount1.tUCUpdateItemNameMain = Convert.ToString(_Class.clsVariables.itemName);
                            UCItemDiscount1.tUCOriginalQtyMain = Convert.ToString(_Class.clsVariables.itemQty);
                            UCItemDiscount1.tUCNewQtyMain = Convert.ToString(_Class.clsVariables.itemQty);
                            UCItemDiscount1.UCLblItemRateMain = Convert.ToString(_Class.clsVariables.itemRate);
                            UCItemDiscount1.UCUpdateSelectedItemNoMain = Convert.ToString(_Class.clsVariables.itemIndex);

                            frmDiscountDisplay.Visibility = Visibility.Hidden;
                            UCPriceChange1.Visibility = Visibility.Hidden;
                            //////if (tItemAmount > 0)
                            //////{
                            //////    // if (_Class.clsVariables.UserType != "1")
                            //////    {
                            //////        FrmDiscount frm = new FrmDiscount();
                            //////        frm.tAmount =string.Format("{0:0.00}",tItemAmount);
                            //////        frm.tItemDisAmount = (gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Disc"].Value.ToString().Trim() == "") ? "0.00" :gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells["Disc"].Value.ToString(); 
                            //////        // frm.DiscountCreationEventHandler += new EventHandler(DiscountSalesCreation);
                            //////        frm.ShowDialog();

                            //////        if (frm.Disc == null)
                            //////        {
                            //////            lblDiscount.Content = "0.00";
                            //////            double tChkDisc = (dt.Rows[tCurrentRowIndex]["Disc"].ToString().Trim() == "") ? 0 : double.Parse(dt.Rows[tCurrentRowIndex]["Disc"].ToString().Trim());
                            //////            if (tChkDisc == 0)
                            //////            {
                            //////                dt.Rows[tCurrentRowIndex]["Disc"] = "0.00";
                            //////            }
                            //////            funDiscountAmtLoad();
                            //////        }
                            //////        else
                            //////        {

                            //////            dt.Rows[tCurrentRowIndex]["Disc"] = (frm.Disc.Trim()=="")?"0.00":String.Format("{0:0.00}",  double.Parse(frm.Disc));
                            //////            gridItems.DataSource = dt;
                            //////            funDiscountAmtLoad();

                            //////        }

                            //////    }                             


                            //////}
                            //////else
                            //////{
                            //////    MyMessageBox.ShowBox("You Should Enter Minimum one Product or Check your Net Amount", "Warning");
                            //////}
                            txtEnterValue.Focus();
                        }
                        catch (Exception ex)
                        {
                            MyMessageBox.ShowBox(ex.Message.ToString(), "Error");
                        }
                    }
                }
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                SalesProject._ExtraForm.frmQtyaNRate frm = new _ExtraForm.frmQtyaNRate();
                if (e.ColumnIndex == 1)
                {
                    frm.getValueType = "Qty";
                }
                else
                {
                    frm.getValueType = "Rate";
                }
                if (gridItems.CurrentCell.ReadOnly == false)
                {

                    if (dt.Rows.Count > 0)
                    {
                        SalesProject._Class.clsVariables.itemIndex = e.RowIndex;
                        SalesProject._Class.clsVariables.itemName = Convert.ToString(gridItems.Rows[e.RowIndex].Cells["ItemName"].Value.ToString());
                        SalesProject._Class.clsVariables.itemQty = Convert.ToString(gridItems.Rows[e.RowIndex].Cells["Qty"].Value.ToString());
                        SalesProject._Class.clsVariables.itemRate = Convert.ToString(gridItems.Rows[e.RowIndex].Cells["Rate"].Value.ToString());
                        SalesProject._Class.clsVariables.itemAmt = Convert.ToString(gridItems.Rows[e.RowIndex].Cells["Amt"].Value.ToString());

                        DataSet dsTemp = new DataSet();
                        SqlDataAdapter adpStopAtChk = new SqlDataAdapter("Select * from Item_table with (index(IndexItem_table)) where Item_Active=1  and Item_name='" + gridItems.Rows[e.RowIndex].Cells["ItemName"].Value.ToString() + "'", con);

                        adpStopAtChk.Fill(dsTemp, "STOPAT");
                        if (dsTemp.Tables["STOPAT"].Rows.Count > 0)
                        {
                            tStopAtQty = dsTemp.Tables["STOPAT"].Rows[0]["StopAtQty"].ToString();
                            tStopAtRate = dsTemp.Tables["STOPAT"].Rows[0]["StopAtRate"].ToString();
                            if (tStopAtQty == "True")
                            {
                                _Class.clsVariables.StopAtQty = tStopAtQty;
                            }
                            else
                            {
                                _Class.clsVariables.StopAtQty = tStopAtQty;
                            }

                            if (tStopAtRate == "True")
                            {
                                _Class.clsVariables.StopAtRate = tStopAtRate;
                            }
                            else
                            {
                                _Class.clsVariables.StopAtRate = tStopAtRate;
                            }
                            if (tStopAtQty == "True" || tStopAtRate == "True")
                            {
                                if (pnlNumeric.Visibility == Visibility.Hidden)
                                {
                                    pnlNumeric.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    if (_Class.clsVariables.tHideKeyboard == true)
                                    {
                                        pnlNumeric.Visibility = Visibility.Hidden;
                                    }
                                    else
                                    {
                                        pnlNumeric.Visibility = Visibility.Visible;
                                    }
                                }
                                frm.ShowDialog();
                                //tempTimer.Stop();
                                //tempTimer.Start();

                            }
                            else
                            {
                                MyMessageBox.ShowBox("This item cannot be change Quantity and Rate", "Warning");
                            }
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == SalesProject._Class.clsVariables.itemIndex)
                            {
                                DataRow row = dt.NewRow();
                                row[0] = SalesProject._Class.clsVariables.itemName;
                                row[1] = SalesProject._Class.clsVariables.itemQty;
                                row[2] = double.Parse(SalesProject._Class.clsVariables.itemRate).ToString("N2");
                                row[3] = String.Format("{0:0.00}", (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty)));
                                dt.Rows.RemoveAt(i);
                                // dt.Rows.InsertAt(row, i);
                                //Stop at Rate is Zero.. remove item from list - Start

                                double tRowAmt = (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty));
                                if (tRowAmt > 0)
                                {
                                    dt.Rows.InsertAt(row, i);
                                }

                                //Stop at Rate is Zero.. remove item from list - End

                                // dt.Rows[i][1] = dt.Rows[i][1].ToString().Replace("@", SalesProject._Class.clsVariables.itemQty);
                            }
                        }

                        try
                        {
                            double tQty = 0, tRate = 0, tAmt = 0;
                            if (gridItems.Rows.Count > 0)
                            {
                                double tRowAmt = (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty));
                                if (tRowAmt > 0)
                                {
                                    if (gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim() != "" && gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[2].Value.ToString().Trim() != "")
                                    {
                                        tQty = double.Parse(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[1].Value.ToString());
                                        tRate = double.Parse(gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[2].Value.ToString());
                                        tAmt = tQty * tRate;
                                        gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[2].Value = string.Format("{0:0.00}", tRate);
                                        gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[3].Value = string.Format("{0:0.00}", tAmt);
                                    }
                                    funDisplayAmount(dt);
                                    gridItems.CurrentCell = gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[0];
                                }
                                funRoundCalculate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MyMessageBox.ShowBox(ex.Message, "Warning");
                        }

                    }
                }
            }
            txtEnterValue.Focus();
        }

        private void listSelect_KeyDown(object sender, KeyEventArgs e)
        {

        }

        public void funF4()
        {
            string tAction = "";
            if (_Class.clsVariables.tStopAtQtyF4 == true)
            {
                tAction = "Qty";
            }
            if (_Class.clsVariables.tStopAtRateF4 == true)
            {
                tAction = "Rate";
            }


            if (tAction == "Rate" || tAction == "Qty")
            {
                SalesProject._ExtraForm.frmQtyaNRate frm = new _ExtraForm.frmQtyaNRate();
                if (tAction == "Qty")
                {
                    frm.getValueType = "Qty";
                }
                else
                {
                    frm.getValueType = "Rate";
                }
                // if (gridItems.CurrentCell.ReadOnly == false)
                {

                    if (dt.Rows.Count > 0)
                    {
                        int tRowIndex = dt.Rows.Count - 1;
                        SalesProject._Class.clsVariables.itemIndex = tRowIndex;
                        SalesProject._Class.clsVariables.itemName = Convert.ToString(gridItems.Rows[tRowIndex].Cells["ItemName"].Value.ToString());
                        SalesProject._Class.clsVariables.itemQty = Convert.ToString(gridItems.Rows[tRowIndex].Cells["Qty"].Value.ToString());
                        SalesProject._Class.clsVariables.itemRate = Convert.ToString(gridItems.Rows[tRowIndex].Cells["Rate"].Value.ToString());
                        SalesProject._Class.clsVariables.itemAmt = Convert.ToString(gridItems.Rows[tRowIndex].Cells["Amt"].Value.ToString());

                        DataSet dsTemp = new DataSet();
                        SqlDataAdapter adpStopAtChk = new SqlDataAdapter("Select * from Item_table with (index(IndexItem_table)) where Item_Active=1 and Item_name='" + gridItems.Rows[tRowIndex].Cells["ItemName"].Value.ToString() + "'", con);

                        adpStopAtChk.Fill(dsTemp, "STOPAT");
                        if (dsTemp.Tables["STOPAT"].Rows.Count > 0)
                        {
                            //tStopAtQty = dsTemp.Tables["STOPAT"].Rows[0]["StopAtQty"].ToString();
                            //tStopAtRate = dsTemp.Tables["STOPAT"].Rows[0]["StopAtRate"].ToString();

                            if (_Class.clsVariables.tStopAtQtyF4 == true)
                            {
                                tStopAtQty = "True";
                            }
                            //tStopAtQty = "True";
                            if (_Class.clsVariables.tStopAtRateF4 == true)
                            {
                                tStopAtRate = "True";
                            }
                            if (tStopAtQty == "True")
                            {
                                _Class.clsVariables.StopAtQty = tStopAtQty;
                            }
                            else
                            {
                                _Class.clsVariables.StopAtQty = tStopAtQty;
                            }

                            if (tStopAtRate == "True")
                            {
                                _Class.clsVariables.StopAtRate = tStopAtRate;
                            }
                            else
                            {
                                _Class.clsVariables.StopAtRate = tStopAtRate;
                            }
                            if (tStopAtQty == "True" || tStopAtRate == "True")
                            {
                                if (pnlNumeric.Visibility == Visibility.Hidden)
                                {
                                    pnlNumeric.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    if (_Class.clsVariables.tHideKeyboard == true)
                                    {
                                        pnlNumeric.Visibility = Visibility.Hidden;
                                    }
                                    else
                                    {
                                        pnlNumeric.Visibility = Visibility.Visible;
                                    }
                                }
                                frm.ShowDialog();
                                //tempTimer.Stop();
                                //tempTimer.Start();
                            }
                            else
                            {
                                MyMessageBox.ShowBox("This item cannot be change Quantity and Rate", "Warning");
                            }
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == SalesProject._Class.clsVariables.itemIndex)
                            {
                                DataRow row = dt.NewRow();
                                row[0] = SalesProject._Class.clsVariables.itemName;
                                row[1] = SalesProject._Class.clsVariables.itemQty;
                                row[2] = double.Parse(SalesProject._Class.clsVariables.itemRate).ToString("N2");
                                row[3] = String.Format("{0:0.00}", (double.Parse(SalesProject._Class.clsVariables.itemRate) * double.Parse(SalesProject._Class.clsVariables.itemQty)));
                                dt.Rows.RemoveAt(i);
                                dt.Rows.InsertAt(row, i);

                                // dt.Rows[i][1] = dt.Rows[i][1].ToString().Replace("@", SalesProject._Class.clsVariables.itemQty);
                            }
                        }

                        try
                        {
                            double tQty = 0, tRate = 0, tAmt = 0;
                            if (gridItems.Rows.Count > 0)
                            {

                                if (gridItems.Rows[tRowIndex].Cells[1].Value.ToString().Trim() != "" && gridItems.Rows[gridItems.CurrentCell.RowIndex].Cells[2].Value.ToString().Trim() != "")
                                {
                                    tQty = double.Parse(gridItems.Rows[tRowIndex].Cells[1].Value.ToString());
                                    tRate = double.Parse(gridItems.Rows[tRowIndex].Cells[2].Value.ToString());
                                    tAmt = tQty * tRate;
                                    gridItems.Rows[tRowIndex].Cells[2].Value = string.Format("{0:0.00}", tRate);
                                    gridItems.Rows[tRowIndex].Cells[3].Value = string.Format("{0:0.00}", tAmt);
                                }
                                funDisplayAmount(dt);
                                gridItems.DataSource = dt;
                                gridItems.CurrentCell = gridItems.Rows[tRowIndex].Cells[0];
                                funRoundCalculate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MyMessageBox.ShowBox(ex.Message, "Warning");
                        }

                    }
                }
            }
            txtEnterValue.Focus();
        }
        private void sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                funF4();
            }
            if (e.Key == Key.F3)
            {
                btnVoid_Click(sender, e);
            }
            else if (e.Key == Key.F10)
            {
                btnCashDraw_Click(sender, e);
            }
            else if (e.Key == Key.F8)
            {
                btnHold_Click(sender, e);
            }
            else if (e.Key == Key.F5)
            {
                btnCash_Click(sender, e);

            }
            else if (e.Key == Key.F6)
            {
                btnNETS_Click(sender, e);
            }
            else if (e.Key == Key.F9)
            {
                btnPrint_Click(sender, e);
            }
            else if (e.Key == Key.Delete)
            {
                btnRemove_Click(sender, e);
            }
            else if (e.Key == Key.Up)
            {
                btnUp_Click(sender, e);
            }
            else if (e.Key == Key.Down)
            {
                btnDown_Click(sender, e);
            }
            else if (e.Key == (Key.LeftAlt & Key.D) || e.Key == (Key.RightAlt & Key.D))
            {
                btnDiscount_Click(sender, e);
            }
            else if (e.Key == Key.Enter)
            {
                txtEnterValue_KeyDown(sender, e);
            }
            txtEnterValue.Focus();
        }
        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            btnDiscount_Click(sender, e);
            txtEnterValue.Focus();
        }



        private void lblLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            funScaleClear();
            txtEnterValue.Focus();
        }
        public void funScaleClear()
        {
            try
            {
                var bc = new BrushConverter();
                lblLogo.Foreground = (Brush)bc.ConvertFrom("White");
                if (tItemNameGlob != "")
                {
                    SqlCommand cmd1 = new SqlCommand("sp_SalesCreationNewBtnGroupItem", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@tItemName", tItemNameGlob);
                    SqlParameter result = new SqlParameter("@tResult", SqlDbType.Float);
                    result.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(result);

                    SqlParameter resultUnit = new SqlParameter("@tUnitDigit", SqlDbType.Float);
                    resultUnit.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(resultUnit);

                    SqlParameter resultWeightScale = new SqlParameter("@tWeightScale", SqlDbType.Float);
                    resultWeightScale.Direction = ParameterDirection.Output;
                    cmd1.Parameters.Add(resultWeightScale);
                    cmd1.ExecuteNonQuery();
                    double tUnitDecimals = double.Parse(resultUnit.Value.ToString());
                    string tWeightScale = resultWeightScale.Value.ToString();
                    count = 0;
                    totAmt = 0.00;
                    totQty = 0.00;
                    totTax = 0.00;
                    double tReadingValue = 0;


                    if (tWeightScale == "1" || tWeightScale.ToUpper() == "TRUE")
                    {
                        if (_Class.clsVariables.tWeightScaleEnable == "Yes")
                        {
                        ReadAgain:
                            try
                            {

                                string data = "";
                                //_Class.clsVariables.serial.ReadExisting();
                                data = _Class.clsVariables.serial.ReadExisting();
                                //serial.Close();
                                if (data.IndexOf("kg") > 0)
                                {
                                    data = data.Substring(0, data.IndexOf("kg"));
                                    data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                    // if
                                    tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                }
                                else if (data.IndexOf("k") > 0)
                                {
                                    data = data.Substring(0, data.IndexOf("k"));
                                    data = data.Substring(data.IndexOf(" "), data.Length - data.IndexOf(" "));
                                    tReadingValue = double.Parse(System.Text.RegularExpressions.Regex.Replace(data, "[^0-9.]", ""));

                                }
                            }
                            catch (Exception)
                            {
                                goto ReadAgain;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        private void btnGroup1_Click_1(object sender, RoutedEventArgs e)
        {
            tPreviousAction = "Group";
            funPrevious();
            txtEnterValue.Focus();
        }

        private void btnGroup9_Click(object sender, RoutedEventArgs e)
        {
            tNextAction = "Group";
            funNext();
            txtEnterValue.Focus();
        }

        private void btnMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
            txtEnterValue.Focus();
        }

        private void lblHold_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Method Helps to Load Hold Record
            try
            {
                funSingleFreeMerge();
                DataRow[] dtRowFilter = null;
                DataRow[] dtRowFilter1 = null;
                double tOriginalQty = 0;
                double tFreeQty = 0;
                listFreeItemDisplay.FontSize = 25;
                listFreeItemDisplay.Width = 495;
                listFreeItemDisplay.Height = 640;
                listFreeItemDisplay.Items.Clear();
                int i = _Class.clsVariables.dtSingleFree.Rows.Count;
                double tTottFreeQty = 0, tTottMainQty = 0, tTottCurrentQty = 0, tSalesQty = 0, tOfferFreeQty = 0, tOfferTotQty = 0;
                for (int m = 0; m < tempdtSingleFree.Rows.Count; m++)
                {
                    tOriginalQty = 0;
                    tTottFreeQty = 0; tTottMainQty = 0; tTottCurrentQty = 0; tSalesQty = 0; tOfferFreeQty = 0; tOfferTotQty = 0;

                    string tItemName = Convert.ToString(tempdtSingleFree.Rows[m]["ItemName"]);
                    tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");
                    dtRowFilter1 = _Class.clsVariables.dtSingleFree.Select("ItemName='" + tItemName + "'");
                    double OfferCount = 0;
                    for (int k = 0; k < dtRowFilter1.Length; k++)
                    {
                        tSalesQty = (string.IsNullOrEmpty(Convert.ToString(dtRowFilter1[k]["TotSaleQty"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(dtRowFilter1[k]["TotSaleQty"]));
                        tOfferFreeQty = (string.IsNullOrEmpty(Convert.ToString(dtRowFilter1[k]["OfferFreeQty"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(dtRowFilter1[k]["OfferFreeQty"]));
                        tOfferTotQty = (string.IsNullOrEmpty(Convert.ToString(dtRowFilter1[k]["Qty"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(dtRowFilter1[k]["Qty"]));
                        OfferCount = (int)tOfferTotQty / tOfferFreeQty;
                        tTottFreeQty += OfferCount * tOfferFreeQty;
                        tTottMainQty += OfferCount * tSalesQty;
                    }


                    dtRowFilter = dt.Select("ItemName='" + tItemName + "'");
                    tFreeQty = (string.IsNullOrEmpty(Convert.ToString(tempdtSingleFree.Rows[m]["Qty"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(tempdtSingleFree.Rows[m]["Qty"]));
                    for (int n = 0; n < dtRowFilter.Length; n++)
                    {
                        tOriginalQty += (string.IsNullOrEmpty(Convert.ToString(dtRowFilter[n]["Qty"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(dtRowFilter[n]["Qty"]));
                    }

                    if (tOriginalQty == 0)
                    {
                        listFreeItemDisplay.Items.Add(Convert.ToString(tempdtSingleFree.Rows[m]["ItemName"]) + " - " + Convert.ToString(tTottFreeQty));
                    }
                    else if ((tTottFreeQty + tTottMainQty) > tOriginalQty && tOriginalQty != 0)
                    {
                        dtRowFilter1 = _Class.clsVariables.dtSingleFree.Select("MainItemName='" + tItemName + "'");
                        if (dtRowFilter1.Length > 0)
                        {
                            listFreeItemDisplay.Items.Add(Convert.ToString(tempdtSingleFree.Rows[m]["ItemName"]) + " - " + Convert.ToString((tTottFreeQty + tTottMainQty) - tOriginalQty));
                        }
                        else
                        {
                            if ((tFreeQty - tOriginalQty) > 0)
                            {
                                listFreeItemDisplay.Items.Add(Convert.ToString(tempdtSingleFree.Rows[m]["ItemName"]) + " - " + Convert.ToString(tFreeQty - tOriginalQty));
                            }
                        }
                    }
                }
                if (listFreeItemDisplay.Items.Count > 0)
                {
                    if (pnlFreeItemDisplay.Visibility == Visibility.Visible)
                    {
                        pnlFreeItemDisplay.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        pnlFreeItemDisplay.Visibility = Visibility.Visible;
                    }
                }
                // txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }

        private void gridItems_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (gridItems.Rows.Count > 0)
                {
                    if (gridItems.SelectedRows[0].Index > 0)
                    {
                        int row = gridItems.SelectedRows[0].Index;// Change gridItems.SelectedIndex--;

                        if (row >= 0)
                        {
                            gridItems.Rows[row].Selected = true;
                        }
                    }
                }
                // txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            txtEnterValue.Focus();
        }



    }
}

