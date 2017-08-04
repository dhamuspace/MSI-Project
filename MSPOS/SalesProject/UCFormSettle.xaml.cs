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
using System.Configuration;
using Microsoft.Reporting.WinForms;


namespace SalesProject
{
    /// <summary>
    /// Interaction logic for FormSettle.xaml
    /// </summary>
    /// 
    public delegate void UCFormSettleEvent();
    public partial class UCFormSettle : UserControl
    {
        public static RoutedCommand MyCash = new RoutedCommand();
        public static RoutedCommand MyNETS = new RoutedCommand();
        public static RoutedCommand MyTenderExact1 = new RoutedCommand();
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
        


        public UCFormSettle()
        {
            InitializeComponent();
            MyCash.InputGestures.Add(new KeyGesture(Key.F5, ModifierKeys.None));
            MyNETS.InputGestures.Add(new KeyGesture(Key.F6, ModifierKeys.None));
            MyTenderExact.InputGestures.Add(new KeyGesture(Key.D1, ModifierKeys.Alt));
            MyOne.InputGestures.Add(new KeyGesture(Key.D2, ModifierKeys.Alt));
            MyTwo.InputGestures.Add(new KeyGesture(Key.D3, ModifierKeys.Alt));
            MyFive.InputGestures.Add(new KeyGesture(Key.D4, ModifierKeys.Alt));
            MyTen.InputGestures.Add(new KeyGesture(Key.D5, ModifierKeys.Alt));
            MyFifty.InputGestures.Add(new KeyGesture(Key.D6, ModifierKeys.Alt));
            MyHundred.InputGestures.Add(new KeyGesture(Key.D7, ModifierKeys.Alt));
            MyCreditCard.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Alt));
            MyClear.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            MyHouseAc.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Alt));
            MyVoucher.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Alt));

            MyTenderExact1.InputGestures.Add(new KeyGesture(Key.NumPad1, ModifierKeys.Alt));
            MyOne.InputGestures.Add(new KeyGesture(Key.NumPad2, ModifierKeys.Alt));
            MyTwo.InputGestures.Add(new KeyGesture(Key.NumPad3, ModifierKeys.Alt));
            MyFive.InputGestures.Add(new KeyGesture(Key.NumPad4, ModifierKeys.Alt));
            MyTen.InputGestures.Add(new KeyGesture(Key.NumPad5, ModifierKeys.Alt));
            MyFifty.InputGestures.Add(new KeyGesture(Key.NumPad6, ModifierKeys.Alt));
            MyHundred.InputGestures.Add(new KeyGesture(Key.NumPad7, ModifierKeys.Alt));

            if (dtDisplay.Columns.Count == 0)
            {
                dtDisplay.Columns.Add("Item", typeof(string));
                dtDisplay.Columns.Add("Qty", typeof(string));
                dtDisplay.Columns.Add("Rate", typeof(string));
                dtDisplay.Columns.Add("Amt", typeof(string));
                dtDisplay.Columns.Add("Disc", typeof(string));
                dtDisplay.Columns.Add("SDisc", typeof(string));
                dtDisplay.Columns.Add("Other", typeof(string));
            }
            if (dtPrint.Columns.Count == 0)
            {
                dtPrint.Columns.Add("Describ", typeof(string));
                dtPrint.Columns.Add("Property", typeof(string));
            }

            if (dtSettleVoid.Columns.Count == 0)
            {
                dtSettleVoid.Columns.Add("SalRecLed");
                dtSettleVoid.Columns.Add("SalRecAmt");
                dtSettleVoid.Columns.Add("SalRecRefundAmt");
                dtSettleVoid.Columns.Add("SalRecType");
            }

            if (dtSettle.Columns.Count == 0)
            {
                dtSettle.Columns.Add("SalRecLed");
                dtSettle.Columns.Add("SalRecAmt");
                dtSettle.Columns.Add("SalRecRefundAmt");
                dtSettle.Columns.Add("SalRecType");
            }
        }

        public DataTable dtSettle = new DataTable();
        public DataTable dtDisplay = new DataTable();
        public string tTotQty, tGrossAmt, tNetAmt, tDiscount, tBillNo, tTaxAmt;
        public string chk = null;
        public event UCFormSettleEvent UCFormSettleEvent_ResettleClose;
        public event UCFormSettleEvent UCFormSettleEvent_settleClose;
        public void funFormSettleLoad()
        {
            try
            {
                funConnectionStateCheck();
                if (!string.IsNullOrEmpty(tBillNo))
                {
                    lblBillNo.Content = Convert.ToString(tBillNo);
                    lblTotQty.Content = Convert.ToString(tTotQty);
                    lblDiscount.Content = Convert.ToString(tDiscount);
                    lblTotAmt.Content = Convert.ToString(tGrossAmt);
                    lblNetAmt.Content = Convert.ToString(tNetAmt);
                    lblTaxAmt.Content = Convert.ToString(tTaxAmt);                    
                 //   trans = con.BeginTransaction();
                    int i = 0;
                    for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        dtDisplay.Rows.Add(ds1.Tables[0].Rows[i][0].ToString(), ds1.Tables[0].Rows[i][1].ToString(), ds1.Tables[0].Rows[i][2].ToString(), ds1.Tables[0].Rows[i][3].ToString(), ds1.Tables[0].Rows[i][4].ToString());
                    }
                }

                txtAmount.Text = Convert.ToString(tempBillAmount);

                gridDisplay.DataSource = dtDisplay.DefaultView;

                gridDisplay.Columns[0].Width = 170;
                gridDisplay.Columns[1].Width = 50;
                gridDisplay.Columns[2].Width = 50;
                gridDisplay.Columns[3].Width = 50;

                dtPrint.Rows.Clear();

                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "GSET");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtPrint);


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
                        if (dtPrint.Rows[mn][0].ToString() == "Enable This Device*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Enable_This_Device"].ToString();
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Printer Name*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Printer_Name"].ToString();
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Printer Type*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Printer_Type"].ToString();
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Print Copies*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Print_Copies"].ToString();
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Characters Per Line*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Characters_Per_Line"].ToString();
                        }
                    }
                }
                SqlCommand cmd13 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@tActionType", "RPTSET");
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd13);
                adp1.Fill(dtPrint);

                SqlCommand cmd2 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@tActionType", "CUSTOMTEXT");
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                adp2.Fill(dtPrint);



                //lblBillNo.Content = tBillNo.ToString();
                //lblTotQty.Content = tTotQty.ToString();
                //lblDiscount.Content = tDiscount.ToString();
                //lblTotAmt.Content = tGrossAmt.ToString();
                //lblNetAmt.Content = tNetAmt.ToString();
                //lblTaxAmt.Content = tTaxAmt.ToString();
                //dtDisplay.Columns.Add("Item", typeof(string));
                //dtDisplay.Columns.Add("Qty", typeof(string));
                //dtDisplay.Columns.Add("Rate", typeof(string));
                //dtDisplay.Columns.Add("Amt", typeof(string));
                //dtDisplay.Columns.Add("Disc", typeof(string));
                //txtAmount.Text = tempBillAmount.ToString();
                //int i = 0;
                //for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
                //{
                //    dtDisplay.Rows.Add(ds1.Tables[0].Rows[i][0].ToString(), ds1.Tables[0].Rows[i][1].ToString(), ds1.Tables[0].Rows[i][2].ToString(), ds1.Tables[0].Rows[i][3].ToString(), ds1.Tables[0].Rows[i][4].ToString());
                //}
                //gridDisplay.DataSource = dtDisplay.DefaultView;

                //gridDisplay.Columns[0].Width = 170;                
                //gridDisplay.Columns[1].Width = 50;
                //gridDisplay.Columns[2].Width = 50;
                //gridDisplay.Columns[3].Width = 50;
                chk = "First";


                if (SalesProject._Class.clsVariables.tControlFrom != null)
                {
                    if (SalesProject._Class.clsVariables.tControlFrom.ToString().Trim() == "VOID")
                    {
                        if (dtSettleVoid.Columns.Count == 0)
                        {
                            dtSettleVoid.Columns.Add("SalRecLed");
                            dtSettleVoid.Columns.Add("SalRecAmt");
                            dtSettleVoid.Columns.Add("SalRecRefundAmt");
                            dtSettleVoid.Columns.Add("SalRecType");
                        }
                    }
                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
                //  con.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            funFormSettleLoad();
        }

        public void btnCashMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnCash_Click(sender, e);
            
        }

        public void btnNETSMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnNETS_Click(sender, e);
            
        }

        public void btnTenderExactMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnTotalAmount_Click(sender, e);
        }

        public void btnTenderExactMethod1(Object sender, ExecutedRoutedEventArgs e)
        {
            btnTotalAmount_Click(sender, e);
        }
        public void btnOneMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            //  btnOneDolor_Click(sender, e);
            funOnedolor("1", sender, e);
        }
        public void btnTwoMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("2", sender, e);
        }
        public void btnFiveMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("5", sender, e);
        }
        public void btnTenMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("10", sender, e);
        }

        public void btnFiftyMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("50", sender, e);
        }

        public void btnFifteenMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("15", sender, e);
        }

        public void btnTwentyMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("20", sender, e);
        }

        public void btnHundredMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            funOnedolor("100", sender, e);
        }

        public void btnCreditCardMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnCreditCard_Click(sender, e);
        }
        public void btnClearMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnClear_Click(sender, e);
        }
        public void btnHouseAcMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnHouseAccount_Click(sender, e);
        }
        public void btnVoucherMethod(Object sender, ExecutedRoutedEventArgs e)
        {
            btnVoucher_Click(sender, e);
        }
        
      public  DateTime currentDate;
        private double  refundAmt;
        public double  Refund
        {
            get { return refundAmt; }
            set { refundAmt = value; }
        }

        string temp = null;
        public string tempBillAmount;
        public DataSet ds1 = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
      //  SqlDataReader dr = null;
        DataTable dt = new DataTable("Items");
      //  SqlTransaction trans = null;
        public DataTable dtSettleVoid = new DataTable();
        public string tTenderClose = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            tTenderClose = "Close";
            funBtnClose();
        }

        public void funBtnClose()
        {
            try
            {
                if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                {

                    //DataTable dtNew1 = new DataTable();
                    //dtNew1.Rows.Clear();
                    //SqlCommand cmd = new SqlCommand("select * from TempSalRecv_table where SalRecv_Salno=@tBillNo", con);
                    //// cmd.Transaction = trans;
                    //cmd.Parameters.AddWithValue("@tBillNo", lblBillNo.Content.ToString());
                    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //adp.Fill(dtNew1);
                    if (dtSettle.Rows.Count > 0)
                    {
                       string res = ( MyMessageBox1.ShowBox("Do you want to Close Without Save settle", "Warning"));
                       if (res == "1")
                       {
                           this.Visibility = Visibility.Hidden;
                           if (UCFormSettleEvent_settleClose != null)
                           {
                               UCFormSettleEvent_settleClose();
                           }
                       }
                    }
                    else
                    {
                        //  SalesCreationEventHandlerNew1(sender, e);
                        // this.Close();
                        this.Visibility = Visibility.Hidden;
                        if (UCFormSettleEvent_settleClose != null)
                        {
                            UCFormSettleEvent_settleClose();
                        }
                    }
                }
                else if (SalesProject._Class.clsVariables.tControlFrom.ToString().Trim() == "VOID")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    if (dtSettleVoid.Rows.Count > 0)
                    {
                        double Refunamt = 0.00;
                        //recamt=0.00;
                        for (int j = 0; j < dtSettleVoid.Rows.Count; j++)
                        {
                            Refunamt += dtSettleVoid.Rows[j]["SalRecRefundAmt"].ToString().Trim() == "" ? 0.00 : Convert.ToDouble(dtSettleVoid.Rows[j]["SalRecRefundAmt"].ToString());
                        }
                        if (Convert.ToDouble(lblNetAmt.Content) <= Refunamt)
                        {
                            SqlCommand cmd1 = new SqlCommand("AlterSalRevTable", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            //   cmd1.Transaction = trans;
                            cmd1.Parameters.AddWithValue("@BillNo", lblBillNo.Content.ToString().Trim());
                            cmd1.Parameters.AddWithValue("@NetAmount", lblNetAmt.Content.ToString().Trim());
                            cmd1.Parameters.AddWithValue("@dt_gridload1", dtSettleVoid);
                            cmd1.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                            cmd1.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            cmd1.ExecuteNonQuery();
                            // trans.Commit();
                            // SalesCreationEventHandlerNew1(sender, e);
                            //this.Close();
                            this.Visibility = Visibility.Hidden;
                            if (UCFormSettleEvent_ResettleClose != null)
                            {
                                UCFormSettleEvent_ResettleClose();
                            }
                        }
                        else
                        {

                            string res = (MyMessageBox1.ShowBox("Do you want to Close Without Save Resettle", "Warning"));
                            if (res == "1")
                            {
                                dtSettleVoid.Rows.Clear();
                                //  SalesCreationEventHandlerNew1(sender, e);
                                // this.Close();
                                this.Visibility = Visibility.Hidden;
                                if (UCFormSettleEvent_ResettleClose != null)
                                {
                                    UCFormSettleEvent_ResettleClose();
                                }
                            }
                            else
                            { }
                        }

                    }
                    else
                    {
                        //  SalesCreationEventHandlerNew1(sender, e);
                        // this.Close();
                        this.Visibility = Visibility.Hidden;
                        if (UCFormSettleEvent_ResettleClose != null)
                        {
                            UCFormSettleEvent_ResettleClose();
                        }
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //this.Close();
            }
        }       

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            txtEnterValue.Focus();
            Button btn = (Button)sender;
            if (txtEnterValue.Text != "")
            {
                temp = txtEnterValue.Text;
                if (btn.Content.ToString().Trim() != ".")
                {
                    txtEnterValue.Text = "";
                    txtEnterValue.Text = temp + btn.Content.ToString();
                }
                else
                {
                    if (temp.IndexOf('.') == -1)
                    {
                        txtEnterValue.Text = "";
                        txtEnterValue.Text = temp + btn.Content.ToString();
                    }
                }
            }
            if (txtEnterValue.Text == "")
            {
                if (btn.Content.ToString().Trim() == ".")
                {
                    txtEnterValue.Text ="0"+ btn.Content.ToString();
                }
                else
                {
                    txtEnterValue.Text = btn.Content.ToString();
                }
            }
            txtEnterValue.Select(txtEnterValue.Text.Length, 0);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEnterValue.Text = string.Empty;
            txtEnterValue.Focus();
        }
      //  string disc, netAmt;
        private void btnTotalAmount_Click(object sender, RoutedEventArgs e)        
        {
            try
            {
                if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                {

                    //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                    {
                        SqlCommand cmd = new SqlCommand("sp_funBtnDolor1", con);
                       // cmd.Transaction = trans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                        cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                        //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                        cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                        cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                        cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                       // double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                        double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                        cmd.Parameters.AddWithValue("@RoundValue", tot);
                        for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                        {
                            if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                            {
                                dtDisplay.Rows[mnk]["Disc"] = "0.00";
                            }
                        }
                        cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                        if (double.Parse(lblDiscount.Content.ToString()) > 0)
                        {
                            cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                           // cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                            cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                            cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                        }
                        txtEnterValue.Text = txtAmount.Text.Trim();

                        double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                        if (tSettle >= 0)
                        {
                            dtSettle.Rows.Add("5",txtAmount.Text.Trim(), tSettle, "Cash");
                        }
                        else
                        {
                            dtSettle.Rows.Add("5", txtEnterValue.Text.Trim(), 0, "Cash");
                        }

                        cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                        cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                        cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                        cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                        //cmd.Parameters.AddWithValue("@tSmenNo", _Class.clsVariables.tempsalesmenLedgerNo);
                        cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                        cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                       // cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();
                       // trans.Commit();
                        refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                        // funPrint();
                        //////for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                        //////{
                        //////    if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                        //////    {
                        //////        charPerLine = dtPrint.Rows[i]["Property"].ToString();
                        //////    }
                        //////    if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                        //////    {
                        //////        if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                        //////        {
                        //////            string res = MyMessageBox1.ShowBox("Do you want to print","Warning");
                        //////            if (res == "1")
                        //////            {
                        //////                funPrint();
                        //////            }
                        //////            break;
                        //////        }
                        //////    }
                        //////}
                        gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                        dt.Clear();
                        _Class.clsVariables.dtSingleFree.Rows.Clear();
                        lblNetAmt.Content = "0.00";
                        lblDiscount.Content = "0.00";
                        lblTotQty.Content = "0.00";
                        lblTotAmt.Content = "0.00";
                        lblTaxAmt.Content = "0.00";
                        // SalesCreation frm = new SalesCreation();
                        // this.Close();
                        // _Class.clsVariables.tNoRead = "NOREAD";
                        //// frm.tNoRead = "NOREAD";
                        // frm.Show();
                        funThankYou();
                       // SalesCreationEventHandlerNew(sender, e);
                        _Class.clsVariables.tempCashdrawstringopen = "Yes";
                        SalesCreationEventHandlerNewCash(sender, e);

                        //this.Close();
                        this.Visibility = Visibility.Hidden;
                        //funBtnClose();
                        if (UCFormSettleEvent_settleClose != null)
                        {
                            UCFormSettleEvent_settleClose();
                        }

                    }
                    //else
                    //{
                    //    MyMessageBox.ShowBox("Please Select Product First", "Warning");

                    //}
                    txtEnterValue.Focus();
                }
                else
                {
                    //if (txtAmount.Text.Trim() != "")
                    //{
                    //    dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtAmount.Text.Trim(), "Cash");
                    //    btnClose_Click(sender, e);
                    //}


                    if (SalesProject._Class.clsVariables.tControlFrom.ToString().Trim() == "VOID")
                    {
                        txtEnterValue.Text = txtAmount.Text.Trim();
                        double tAmountNew = (txtEnterValue.Text.Trim() == "") ? 0 : double.Parse(txtEnterValue.Text.Trim());
                        if (tAmountNew >= double.Parse(txtAmount.Text.Trim()))
                        {
                            if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                            {

                                dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                txtAmount.Text =string.Format("{0:0.00}", (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()))); ;
                                txtEnterValue.Text = string.Empty;
                                txtAmount.Text = "0";
                              //  btnClose_Click(sender, e);
                                funBtnClose();
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Please Select Product First", "Warning");
                            }
                            txtEnterValue.Focus();
                        }
                        if (txtEnterValue.Text.ToString().Trim() != "")
                        {
                            if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                            {

                                dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                txtAmount.Text =string.Format("{0:0.00}",(double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                                txtEnterValue.Text = string.Empty;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
              //  trans.Rollback();
               // con.Close();
            }  
        }
        public event System.EventHandler SalesCreationEventHandlerNew;
        public event System.EventHandler SalesCreationEventHandlerNewCash;
        public event System.EventHandler SalesCreationEventHandlerNew1;
        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;
        string mainStr;
        double findCenterPosition;
        DataTable dtPrint = new DataTable();
        
        byte[] byteOut;

        public void PrintSales1(object sender, RenderingCompleteEventArgs e)
        {
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
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        public void funThankYou()
        {
            try
            {
                if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                {
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
        }

        public void funOnedolor(string lastItem, Object sender, RoutedEventArgs e)
        {
            try
            {
             
                txtEnterValue.Text = (lastItem.ToString() == "") ? "0:00" : string.Format("{0:0.00}", double.Parse(lastItem.ToString()));
                txtEnterValue.Select(txtEnterValue.Text.Length, 0);

                if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                {
                    if (double.Parse(txtEnterValue.Text.Trim()) >= double.Parse(txtAmount.Text.Trim()))
                    {

                        //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                        {

                            SqlCommand cmd = new SqlCommand("sp_funBtnDolor1", con);
                           // cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                            //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                            cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                           // double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                            double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                            cmd.Parameters.AddWithValue("@RoundValue", tot);
                            for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                            {
                                if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                                {
                                    dtDisplay.Rows[mnk]["Disc"] = "0.00";
                                }
                            }
                            cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                            if (double.Parse(lblDiscount.Content.ToString()) > 0)
                            {
                                cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                              //  cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                                cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                            }
                            double tSettle=(Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                            if (tSettle >= 0)
                            {
                                dtSettle.Rows.Add("5", txtAmount.Text.Trim(), tSettle, "Cash");
                            }
                            else
                            {
                                dtSettle.Rows.Add("5", txtEnterValue.Text.Trim(), 0, "Cash");
                            }
                            cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                            cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                            cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                            cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                           // cmd.Parameters.AddWithValue("@tSmenNo", _Class.clsVariables.tempsalesmenLedgerNo);
                            cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                            cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                            //cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                          //  trans.Commit();
                            refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                            ////for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                            ////{
                            ////    if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                            ////    {
                            ////        charPerLine = dtPrint.Rows[i]["Property"].ToString();
                            ////    }
                            ////    if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                            ////    {
                            ////        if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                            ////        {
                            ////            string res = MyMessageBox1.ShowBox("Do you want to print","Warning");
                            ////            if (res == "1")
                            ////            {
                            ////                funPrint();
                            ////            }
                            ////            break;
                            ////        }
                            ////    }
                            ////}

                            gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                            dt.Clear();
                            _Class.clsVariables.dtSingleFree.Rows.Clear();
                            lblNetAmt.Content = "0.00";
                            lblDiscount.Content = "0.00";
                            lblTotQty.Content = "0.00";
                            lblTotAmt.Content = "0.00";
                            lblTaxAmt.Content = "0.00";
                            // SalesCreation frm = new SalesCreation();
                            // this.Close();
                            // _Class.clsVariables.tNoRead = "NOREAD";
                            //// frm.tNoRead = "NOREAD";
                            // frm.Show();
                            funThankYou();
                            //  SalesCreationEventHandlerNew(sender, e);
                            _Class.clsVariables.tempCashdrawstringopen = "Yes";
                            SalesCreationEventHandlerNewCash(sender, e);
                           // this.Close();
                            this.Visibility = Visibility.Hidden;
                            if (UCFormSettleEvent_settleClose != null)
                            {
                                UCFormSettleEvent_settleClose();
                            }
                           // funBtnClose();
                        }
                        //else
                        //{
                        //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
                        //}
                        txtEnterValue.Focus();

                    }
                    if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                    {
                       // SqlCommand cmd = new SqlCommand("sp_funBtnDolor2", con);
                       //// cmd.Transaction = trans;
                       // cmd.CommandType = CommandType.StoredProcedure;
                       // cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.ToString());
                       // cmd.ExecuteNonQuery();
                        double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                        if (tSettle >= 0)
                        {
                            dtSettle.Rows.Add("5", txtAmount.Text.Trim(), tSettle, "Cash");
                        }
                        else
                        {
                            dtSettle.Rows.Add("5", txtEnterValue.Text.Trim(), 0, "Cash");
                        }

                        txtAmount.Text =string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                        txtEnterValue.Text = string.Empty;
                    }
                }
                else
                {

                    if (SalesProject._Class.clsVariables.tControlFrom.ToString().Trim() == "VOID")
                    {
                        if (double.Parse(txtEnterValue.Text.Trim()) >= double.Parse(txtAmount.Text.Trim()))
                        {
                            //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                            {

                                dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                txtAmount.Text = string.Format("{0:0.00}", (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString())));
                                txtEnterValue.Text = string.Empty;
                                txtAmount.Text = "0";
                                //btnClose_Click(sender, e);
                                funBtnClose();
                            }
                            //else
                            //{
                            //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
                            //}
                            txtEnterValue.Focus();
                        }
                        if (txtEnterValue.Text.ToString().Trim() != "")
                        {
                            if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                            {

                                dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                txtAmount.Text = string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                                txtEnterValue.Text = string.Empty;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void btnOneDolor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             
                txtEnterValue.Focus();
                Button btn = (Button)sender;
                string myString = Convert.ToString(btn).ToString();
                string[] splitString = myString.Split('$');
                string firstItem = splitString[0];
                string lastItem = splitString[1];
                funOnedolor(lastItem, sender, e);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
              //  trans.Rollback();
              //  this.Close();
                this.Visibility = Visibility.Hidden;
               // funBtnClose();
                tTenderClose = "Close";
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
               // con.Close();
            } 

        }
     

        private void txtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                Int32 selectionStart = textBox.SelectionStart;
                Int32 selectionLength = textBox.SelectionLength;
                String newText = String.Empty;
                int count = 0;
                foreach (Char c in textBox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                    {
                        newText += c;
                        if (c == '.')
                            count += 1;
                    }
                }
                textBox.Text = newText;
                btnTotalAmount.Content = textBox.Text; 
                textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           // btnTotalAmount.Content = txtAmount.Text;
        }
        //int tSalRecv_sno=0;  
        //int tstrn_sno=0;
        //int tItem_no=0;
        //int tstrn_no=0;
        //string  tTax_no;
        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (txtEnterValue.Text != "")
                {
                    if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                    {

                        if (double.Parse(txtEnterValue.Text.Trim()) >= double.Parse(txtAmount.Text.Trim()))
                        {
                            //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                            {
                                SqlCommand cmd = new SqlCommand("sp_funBtnDolor1", con);
                               // cmd.Transaction = trans;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                                //double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                                double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                                cmd.Parameters.AddWithValue("@RoundValue", tot);
                                for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                                {
                                    if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                                    {
                                        dtDisplay.Rows[mnk]["Disc"] = "0.00";
                                    }
                                }
                                cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                                if (double.Parse(lblDiscount.Content.ToString()) > 0)
                                {
                                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                   // cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                                    cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                                }

                                double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                                if (tSettle >= 0)
                                {
                                    dtSettle.Rows.Add("5",txtAmount.Text.Trim(), tSettle, "Cash");
                                }
                                else
                                {
                                    dtSettle.Rows.Add("5", txtEnterValue.Text.Trim(), 0, "Cash");
                                }
                                cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                                cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                                cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                                cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                               // cmd.Parameters.AddWithValue("@tSmenNo", _Class.clsVariables.tempsalesmenLedgerNo);
                                cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                                cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                              //  cmd.Transaction = trans;
                                cmd.ExecuteNonQuery();
                              //  trans.Commit();
                                refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                                // funPrint();
                                ////for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                                ////{
                                ////    if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                                ////    {
                                ////        charPerLine = dtPrint.Rows[i]["Property"].ToString();
                                ////    }
                                ////    if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                                ////    {
                                ////        if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                                ////        {
                                ////            string res=MyMessageBox1.ShowBox("Do you want to print","Warning");
                                ////            if (res == "1")
                                ////            {
                                ////                funPrint();
                                ////            }
                                ////            break;
                                ////        }
                                ////    }
                                ////}

                                gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                                dt.Clear();
                                _Class.clsVariables.dtSingleFree.Rows.Clear();
                                lblNetAmt.Content = "0.00";
                                lblDiscount.Content = "0.00";
                                lblTotQty.Content = "0.00";
                                lblTotAmt.Content = "0.00";
                                lblTaxAmt.Content = "0.00";
                                //  SalesCreation frm = new SalesCreation();
                                //  this.Close();
                                //  _Class.clsVariables.tNoRead = "NOREAD";
                                ////  frm.tNoRead = "NOREAD";
                                //  frm.Show();
                                funThankYou();
                                _Class.clsVariables.tempCashdrawstringopen = "Yes";
                                SalesCreationEventHandlerNewCash(sender, e);
                               // this.Close();
                                this.Visibility = Visibility.Hidden;
                               // funBtnClose();
                                if (UCFormSettleEvent_settleClose != null)
                                {
                                    UCFormSettleEvent_settleClose();
                                }

                            }
                            //else
                            //{
                            //    MyMessageBox.ShowBox("Please Select Product First", "Warning");

                            //}
                            txtEnterValue.Focus();

                        }
                        if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                        {
                         //   SqlCommand cmd = new SqlCommand("sp_funBtnDolor2", con);
                         //   cmd.CommandType = CommandType.StoredProcedure;
                         //   cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.ToString());
                         ////   cmd.Transaction = trans;
                         //   cmd.ExecuteNonQuery();

                            double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                            if (tSettle >= 0)
                            {
                                dtSettle.Rows.Add("5",txtAmount.Text.Trim(), tSettle, "Cash");
                            }
                            else
                            {
                                dtSettle.Rows.Add("5", txtEnterValue.Text.Trim(), 0, "Cash");
                            }
                            txtAmount.Text =string.Format("{0:0.00}",(double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                            txtEnterValue.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (SalesProject._Class.clsVariables.tControlFrom.ToString().Trim() == "VOID")
                        {
                            if (double.Parse(txtEnterValue.Text.Trim()) >= double.Parse(txtAmount.Text.Trim()))
                            {
                                //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                                {
                                   
                                    dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                    txtAmount.Text =string.Format("{0:0.00}",(double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString())));
                                    txtEnterValue.Text = string.Empty;
                                    txtAmount.Text = "0";
                                    //btnClose_Click(sender, e);
                                    funBtnClose();
                                }
                                //else
                                //{
                                //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
                                //}
                                txtEnterValue.Focus();
                            }
                            if (txtEnterValue.Text.ToString().Trim() != "")
                            {
                                if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                                {

                                    dtSettleVoid.Rows.Add("5", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                    txtAmount.Text =string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                                    txtEnterValue.Text = string.Empty;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Settle Amount", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
             //   trans.Rollback();
              //  con.Close();
            } 

        }
    

//        int tempStrnNo;
        
     //   double tNet_amt=0.00;
      //  int tClosingStk = 0;
      //  double tTaxPercent, tTaxAmt1;
        DataSet dsTax = new DataSet();       

        private void btnNETS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEnterValue.Text != "")
                {
                    if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                    {
                        if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                        {
                            SqlCommand cmd = new SqlCommand("sp_SettleNETSProcess1", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                            //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                            cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                            cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                            cmd.Parameters.AddWithValue("@RoundValue", tot);
                            for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                            {
                                if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                                {
                                    dtDisplay.Rows[mnk]["Disc"] = "0.00";
                                }
                            }
                            cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                            if (double.Parse(lblDiscount.Content.ToString()) > 0)
                            {
                                cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                //cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                                cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                            }
                            double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                            if (tSettle >= 0)
                            {
                                dtSettle.Rows.Add("14",txtAmount.Text.Trim(), tSettle, "NETS");
                            }
                            else
                            {
                                dtSettle.Rows.Add("14", txtEnterValue.Text.Trim(), 0, "NETS");
                            }
                            cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                            cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                            cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                            //cmd.Parameters.AddWithValue("@tSmenNo", _Class.clsVariables.tempsalesmenLedgerNo);
                            cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                            cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                          //  cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                           // trans.Commit();
                            refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                            // funPrint();
                            //for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
                            //{
                            //    if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                            //    {
                            //        charPerLine = dtPrint.Rows[i]["Property"].ToString();
                            //    }
                            //    if (dtPrint.Rows[i]["Describ"].ToString().Trim() == "Auto Print")
                            //    {
                            //        if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                            //        {
                            //            string res=MyMessageBox1.ShowBox("Do you want to Print", "Warning");
                            //            if (res == "1")
                            //            {
                            //                funPrint();
                            //            }
                            //            break;
                            //        }
                            //    }
                            //}
                            gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                            dt.Clear();
                            _Class.clsVariables.dtSingleFree.Rows.Clear();
                            lblNetAmt.Content = "0.00";
                            lblDiscount.Content = "0.00";
                            lblTotQty.Content = "0.00";
                            lblTotAmt.Content = "0.00";
                            lblTaxAmt.Content = "0.00";
                            // SalesCreation frm = new SalesCreation();
                            // this.Close();
                            // _Class.clsVariables.tNoRead = "NOREAD";
                            //// frm.tNoRead = "NOREAD";
                            // frm.Show();
                            funThankYou();
                            SalesCreationEventHandlerNew(sender, e);
                         //   this.Close();
                            this.Visibility = Visibility.Hidden;
                           // funBtnClose();
                            if (UCFormSettleEvent_settleClose != null)
                            {
                                UCFormSettleEvent_settleClose();
                            }
                        }
                        if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                        {
                          //  SqlCommand cmd = new SqlCommand("sp_SettleNETSProcess2", con);
                          //  cmd.CommandType = CommandType.StoredProcedure;
                          //  cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.ToString());
                          ////  cmd.Transaction = trans;
                          //  cmd.ExecuteNonQuery();
                            double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                            if (tSettle >= 0)
                            {
                                dtSettle.Rows.Add("14",txtAmount.Text.Trim(), tSettle, "NETS");
                            }
                            else
                            {
                                dtSettle.Rows.Add("14", txtEnterValue.Text.Trim(), 0, "NETS");
                            }
                            txtAmount.Text =string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                            txtEnterValue.Text = string.Empty;
                        }
                        else if (double.Parse(txtEnterValue.Text.Trim()) > double.Parse(txtAmount.Text.Trim()))
                        {
                            MyMessageBox.ShowBox("NETS Value not Exceed then settle Amount", "Warning");
                        }
                    }
                    else
                    {
                        if (SalesProject._Class.clsVariables.tControlFrom == "VOID")
                        {
                             if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                             {
                                dtSettleVoid.Rows.Add("14", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "NETS");
                                refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                                txtEnterValue.Text = string.Empty;
                                txtAmount.Text = "0.00";
                               // btnClose_Click(sender, e);
                                funBtnClose();
                             }
                            if(txtEnterValue.Text.Trim()!="")
                            {
                                if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                                {
                                    dtSettleVoid.Rows.Add("14", txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "NETS");
                                    txtAmount.Text =string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                                    txtEnterValue.Text = string.Empty;
                                }
                                else if (double.Parse(txtEnterValue.Text.Trim()) > double.Parse(txtAmount.Text.Trim()))
                                {
                                    MyMessageBox.ShowBox("NETS Value not Exceed then settle Amount", "Warning");
                                }
                            }
                        }
                    }

                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Settle Amount", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
              //  trans.Rollback();
              //  con.Close();
            } 
        }

        //int tSmas_no = 0;
        //int tVoucherNo = 0;
        //int tSalesVchNo = 0;
        //int tVoucherSno = 0;
        //int tCountNETS = 0;
        //int tCountCash = 0;
        //int tRefno = 0;
    

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtEnterValue.Text.Length > 0)
            {
                temp = txtEnterValue.Text;
                txtEnterValue.Text = temp.Remove(temp.Length - 1);
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

        double discount1, netAmt1;
        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                {
                    //if (txtEnterValue.Text != "")
                    //{

                    //    if (Convert.ToDouble(lblNetAmt.Content.ToString()) != Convert.ToDouble(txtEnterValue.Text.Trim()))
                    //    {
                    //        if (Convert.ToDouble(txtEnterValue.Text.Trim()) ==Convert.ToDouble(txtAmount.Text.Trim()))
                    //        {
                    //            discount1 = (double.Parse(lblDiscount.Content.ToString()) + double.Parse(txtEnterValue.Text.Trim()));
                    //            netAmt1 = (double.Parse(lblNetAmt.Content.ToString()) - double.Parse(txtEnterValue.Text.Trim()));

                    //            //SqlCommand cmd = new SqlCommand("sp_SettleDiscount", con);
                    //            //cmd.CommandType = CommandType.StoredProcedure;
                    //            //cmd.Parameters.AddWithValue("@tDiscount", discount1);
                    //            //cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));                             
                    //            //cmd.ExecuteNonQuery();

                    //            if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                    //            {
                    //                SqlCommand cmd = new SqlCommand("sp_funBtnDolor1", con);
                    //                // cmd.Transaction = trans;
                    //                cmd.CommandType = CommandType.StoredProcedure;
                    //                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                    //                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                    //                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                    //                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                    //                cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                    //                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    //                double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                    //                cmd.Parameters.AddWithValue("@RoundValue", tot);
                    //                for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                    //                {
                    //                    if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                    //                    {
                    //                        dtDisplay.Rows[mnk]["Disc"] = "0.00";
                    //                    }
                    //                }
                    //                cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                    //                if (double.Parse(lblDiscount.Content.ToString()) > 0)
                    //                {
                    //                    cmd.Parameters.AddWithValue("@tDiscount", discount1);
                    //                    cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                    //                }
                    //                else
                    //                {
                    //                    cmd.Parameters.AddWithValue("@tDiscount", discount1);
                    //                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                    //                }
                    //                txtEnterValue.Text = txtAmount.Text.Trim();
                    //                cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                    //                cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                    //                cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                    //                cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                    //                // cmd.Transaction = trans;
                    //                cmd.ExecuteNonQuery();
                    //                // trans.Commit();
                    //                refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));

                    //                gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                    //                dt.Clear();
                    //                _Class.clsVariables.dtSingleFree.Rows.Clear();
                    //                lblNetAmt.Content = "0.00";
                    //                lblDiscount.Content = "0.00";
                    //                lblTotQty.Content = "0.00";
                    //                lblTotAmt.Content = "0.00";
                    //                lblTaxAmt.Content = "0.00";

                    //                funThankYou();











                    //                SalesCreationEventHandlerNew(sender, e);
                    //                //this.Close();
                    //                this.Visibility = Visibility.Hidden;
                    //                //  funBtnClose();
                    //                if (UCFormSettleEvent_settleClose != null)
                    //                {
                    //                    UCFormSettleEvent_settleClose();
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MyMessageBox.ShowBox("Discount amount and Settle Amount not same", "Warning");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MyMessageBox.ShowBox("Please Enter valid discount amount", "Warning");
                    //    }
                    //}
                    //else
                    //{
                    //    MyMessageBox.ShowBox("Please Enter Discount Amount", "Warning");
                    //    txtEnterValue.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
              //  trans.Rollback();
                // con.Close();
            } 
        }

        private void btnPrintBill_Click(object sender, RoutedEventArgs e)
        {
            //funPrint();
            
        }

        public void Method1(Object sender, ExecutedRoutedEventArgs e)
        {
            btnTotalAmount_Click(sender, e);
        }
        public void Method2(Object sender, ExecutedRoutedEventArgs e)
        {
            tChkClose = "";
            funLoad(btnOneDolor);            
            if (tChkClose == "Close")
            {
                SalesCreationEventHandlerNew(sender, e);
               // this.Close();
                this.Visibility = Visibility.Hidden;
                //funBtnClose();
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
            }
        }
        public void Method3(Object sender, ExecutedRoutedEventArgs e)
        {
            tChkClose = "";
            funLoad(btnTwoDolor);
            if (tChkClose == "Close")
            {
                SalesCreationEventHandlerNew(sender, e);
              //  this.Close();
                this.Visibility = Visibility.Hidden;
              //  funBtnClose();
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
            }
        }
        public void Method4(Object sender, ExecutedRoutedEventArgs e)
        {
            tChkClose = "";
            funLoad(btn5);
            if (tChkClose == "Close")
            {
                SalesCreationEventHandlerNew(sender, e);
                //this.Close();
                this.Visibility = Visibility.Hidden;
               // funBtnClose();
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
            }
        }
        public void Method5(Object sender, ExecutedRoutedEventArgs e)
        {
            tChkClose = "";
            funLoad(btn10);
            if (tChkClose == "Close")
            {
                SalesCreationEventHandlerNew(sender, e);
               // this.Close();
                this.Visibility = Visibility.Hidden;
              //  funBtnClose();
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
            }
        }
        public void Method6(Object sender, ExecutedRoutedEventArgs e)
        {
            tChkClose = "";
            funLoad(btn50);
            if (tChkClose == "Close")
            {
                SalesCreationEventHandlerNew(sender, e);
               // this.Close();
                this.Visibility = Visibility.Hidden;
               // funBtnClose();
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
            }
        }
        public void Method7(Object sender, ExecutedRoutedEventArgs e)
        {
            tChkClose = "";
            funLoad(btn100);
            if (tChkClose == "Close")
            {
                SalesCreationEventHandlerNew(sender, e);
               // this.Close();
                this.Visibility = Visibility.Hidden;
               // funBtnClose();
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
            }
        }
        public void Method8(Object sender, ExecutedRoutedEventArgs e)
        {
            btnCreditCard_Click(sender,e);
        }
        public void Method9(Object sender, ExecutedRoutedEventArgs e)
        {
            btnHouseAccount_Click(sender,e);  
        }
        public void Method10(Object sender, ExecutedRoutedEventArgs e)
        {
            btnVoucher_Click(sender,e); 
        }

        private void btnVoucher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHouseAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtAmount.Text != "")
                {
                    if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                    {
                        frmHouseAccount frm = new frmHouseAccount();
                        SalesProject._Class.clsVariables.tHouseACAmt = double.Parse(txtAmount.Text.Trim());
                        frm.ShowDialog();

                        if (!string.IsNullOrEmpty(SalesProject._Class.clsVariables.tHouseACCustomerName))
                        {
                            txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tHouseACAmt);
                        }
                        else
                        {
                            txtEnterValue.Text = "0.00";
                        }

                      //  txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tHouseACAmt);

                        if (SalesProject._Class.clsVariables.tHouseACCustomerName.ToString().Trim() != "")
                        {
                            if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                            {
                                SqlCommand cmd = new SqlCommand("sp_SettleHouseACProcess1", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                                //double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                                double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                                cmd.Parameters.AddWithValue("@RoundValue", tot);
                                for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                                {
                                    if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                                    {
                                        dtDisplay.Rows[mnk]["Disc"] = "0.00";
                                    }
                                }
                                    cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                                if (double.Parse(lblDiscount.Content.ToString()) > 0)
                                {
                                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                 //   cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                                    cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                                }

                            DataTable dtnets = new DataTable();
                            dtnets.Rows.Clear();
                            if (!string.IsNullOrEmpty(_Class.clsVariables.tHouseACCustomerName))
                            {
                                SqlCommand cmdCard = new SqlCommand("select Ledger_no  from Ledger_table where Ledger_name=@tHouseACName", con);
                                cmdCard.Parameters.AddWithValue("@tHouseACName", _Class.clsVariables.tHouseACCustomerName.ToString());
                                // cmd.Transaction = trans;
                                SqlDataAdapter dp = new SqlDataAdapter(cmdCard);
                                dp.Fill(dtnets);
                                if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                                {

                                    double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                                    if (tSettle >= 0)
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtAmount.Text.Trim(), tSettle, _Class.clsVariables.tHouseACCustomerName.ToString());
                                    }
                                    else
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtEnterValue.Text.Trim(), 0, _Class.clsVariables.tHouseACCustomerName.ToString());
                                    }
                                }
                            }
                                cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                                cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                                cmd.Parameters.AddWithValue("@tCreditCardName", SalesProject._Class.clsVariables.tHouseACCustomerName);
                                cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                                //cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                                cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                                cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                                
                              //  cmd.Transaction = trans;
                                cmd.ExecuteNonQuery();
                               // trans.Commit();
                                refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                                
                                gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                                dt.Clear();
                                _Class.clsVariables.dtSingleFree.Rows.Clear();
                                lblNetAmt.Content = "0.00";
                                lblDiscount.Content = "0.00";
                                lblTotQty.Content = "0.00";
                                lblTotAmt.Content = "0.00";
                                lblTaxAmt.Content = "0.00";
                                
                                funThankYou();
                                
                                SalesCreationEventHandlerNew(sender, e);
                               // this.Close();
                                this.Visibility = Visibility.Hidden;
                               // funBtnClose();
                                if (UCFormSettleEvent_settleClose != null)
                                {
                                    UCFormSettleEvent_settleClose();
                                }
                            }
                        }
                        if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                        {
                            //SqlCommand cmd = new SqlCommand("sp_SettleHouseACProcess2", con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.ToString());
                            //cmd.Parameters.AddWithValue("@tCreditCardName", SalesProject._Class.clsVariables.tHouseACCustomerName);                         
                            //cmd.ExecuteNonQuery();

                            DataTable dtnets = new DataTable();
                            dtnets.Rows.Clear();
                            if (!string.IsNullOrEmpty(_Class.clsVariables.tHouseACCustomerName))
                            {
                                SqlCommand cmdCard = new SqlCommand("select Ledger_no  from Ledger_table where Ledger_name=@tHouseACName", con);
                                cmdCard.Parameters.AddWithValue("@tHouseACName", _Class.clsVariables.tHouseACCustomerName.ToString());
                                // cmd.Transaction = trans;
                                SqlDataAdapter dp = new SqlDataAdapter(cmdCard);
                                dp.Fill(dtnets);
                                if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                                {

                                    double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                                    if (tSettle >= 0)
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(),txtAmount.Text.Trim(), tSettle, _Class.clsVariables.tHouseACCustomerName.ToString());
                                    }
                                    else
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtEnterValue.Text.Trim(), 0, _Class.clsVariables.tHouseACCustomerName.ToString());
                                    }
                                }
                            }

                            txtAmount.Text = string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                            txtEnterValue.Text = string.Empty;
                        }
                        else if (double.Parse(txtEnterValue.Text.Trim()) > double.Parse(txtAmount.Text.Trim()))
                        {
                            MyMessageBox.ShowBox("House Account Value not Exceed then settle Amount", "Warning");
                        }
                    }
                    else
                    {
                        if (SalesProject._Class.clsVariables.tControlFrom == "VOID")
                        {
                            frmHouseAccount frm = new frmHouseAccount();
                            SalesProject._Class.clsVariables.tHouseACAmt = double.Parse(txtAmount.Text.Trim());
                            frm.ShowDialog();
                            txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tHouseACAmt);
                            DataTable dtnets = new DataTable();
                            dtnets.Rows.Clear();

                            //if (dtnets.Rows.Count > 0)
                            {
                                if (SalesProject._Class.clsVariables.tHouseACCustomerName != "" && SalesProject._Class.clsVariables.tHouseACCustomerName != null)
                                {
                                    SqlCommand cmd = new SqlCommand("select Ledger_no  from Ledger_table where Ledger_name=@tHouseACName", con);
                                    cmd.Parameters.AddWithValue("@tHouseACName", SalesProject._Class.clsVariables.tHouseACCustomerName.ToString());
                                   // cmd.Transaction = trans;
                                    SqlDataAdapter dp = new SqlDataAdapter(cmd);
                                    dp.Fill(dtnets);
                                    if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                                    {
                                        dtSettleVoid.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtAmount.Text.Trim(), txtAmount.Text.Trim(), "CreditCard");
                                        refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                                        txtAmount.Text = "0.00";
                                        txtEnterValue.Text = string.Empty;
                                       // btnClose_Click(sender, e);
                                        funBtnClose();
                                    }
                                    if (txtEnterValue.Text.Trim() != "")
                                    {
                                        if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                                        {
                                            dtSettleVoid.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                            txtAmount.Text = string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                                            txtEnterValue.Text = string.Empty;
                                        }
                                        else if (double.Parse(txtEnterValue.Text.Trim()) > double.Parse(txtAmount.Text.Trim()))
                                        {
                                            MyMessageBox.ShowBox("House Account Value not Exceed then settle Amount", "Warning");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Settle Amount", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
              //  trans.Rollback();
                //  con.Close();
            }
        }

        private void btnCreditCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtAmount.Text != "")
                {
                    if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                    {
                        CreditCard frm = new CreditCard();
                        SalesProject._Class.clsVariables.tCreditCardAmt = double.Parse(txtAmount.Text.Trim());
                        frm.ShowDialog();

                        txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tCreditCardAmt);

                        if (SalesProject._Class.clsVariables.tCreditCardName != "")
                        {
                            if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                            {
                                SqlCommand cmd = new SqlCommand("sp_SettleCreditCardProcess1", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                                cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                                //double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                                double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                                cmd.Parameters.AddWithValue("@RoundValue", tot);

                                for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                                {
                                    if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                                    {
                                        dtDisplay.Rows[mnk]["Disc"] = "0.00";
                                    }
                                }

                                cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                                if (double.Parse(lblDiscount.Content.ToString()) > 0)
                                {
                                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                   // cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                                    cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                                }

                            DataTable dtnets = new DataTable();
                            dtnets.Rows.Clear();
                            if (!string.IsNullOrEmpty(_Class.clsVariables.tCreditCardName ))
                            {
                                SqlCommand cmdCard = new SqlCommand("select Ledger_no  from Ledger_table where Ledger_name=@tName", con);
                                cmdCard.Parameters.AddWithValue("@tName", _Class.clsVariables.tCreditCardName.ToString());
                                SqlDataAdapter dp = new SqlDataAdapter(cmdCard);
                                dp.Fill(dtnets);
                                if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                                {

                                    double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                                    if (tSettle >= 0)
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(),txtAmount.Text.Trim(), tSettle, _Class.clsVariables.tCreditCardName.ToString());
                                    }
                                    else
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtEnterValue.Text.Trim(), 0, _Class.clsVariables.tCreditCardName.ToString());
                                    }
                                }
                            }
                                cmd.Parameters.AddWithValue("@dt_gridload1", dtSettle);
                                cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                                cmd.Parameters.AddWithValue("@tCreditCardName", SalesProject._Class.clsVariables.tCreditCardName);
                                cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                                //cmd.Parameters.AddWithValue("@tSmenNo", _Class.clsVariables.tempsalesmenLedgerNo);
                                cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                                cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                            //    cmd.Transaction = trans;
                                cmd.ExecuteNonQuery();
                             //   trans.Commit();
                                refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                             
                                gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                                dt.Clear();
                                _Class.clsVariables.dtSingleFree.Rows.Clear();
                                lblNetAmt.Content = "0.00";
                                lblDiscount.Content = "0.00";
                                lblTotQty.Content = "0.00";
                                lblTotAmt.Content = "0.00";
                                lblTaxAmt.Content = "0.00";
                                // SalesCreation frm = new SalesCreation();
                                // this.Close();
                                // _Class.clsVariables.tNoRead = "NOREAD";
                                //// frm.tNoRead = "NOREAD";
                                // frm.Show();
                                funThankYou();
                                SalesCreationEventHandlerNew(sender, e);
                              //  this.Close();
                                this.Visibility = Visibility.Hidden;
                               // funBtnClose();
                                if (UCFormSettleEvent_settleClose != null)
                                {
                                    UCFormSettleEvent_settleClose();
                                }
                            }
                        }
                        if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                        {
                            //SqlCommand cmd = new SqlCommand("sp_SettleCreditCardProcess2", con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.ToString());
                            //cmd.Parameters.AddWithValue("@tCreditCardName", SalesProject._Class.clsVariables.tCreditCardName);
                            //cmd.ExecuteNonQuery();
                            DataTable dtnets = new DataTable();
                            dtnets.Rows.Clear();                           
                            if (!string.IsNullOrEmpty(_Class.clsVariables.tCreditCardName))
                            {
                                SqlCommand cmdCard = new SqlCommand("select Ledger_no  from Ledger_table where Ledger_name=@tName", con);
                                cmdCard.Parameters.AddWithValue("@tName", _Class.clsVariables.tCreditCardName.ToString());
                                SqlDataAdapter dp = new SqlDataAdapter(cmdCard);
                                dp.Fill(dtnets);
                                if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                                {

                                    double tSettle = (Convert.ToDouble(txtEnterValue.Text.ToString()) - Convert.ToDouble(txtAmount.Text.ToString()));
                                    if (tSettle >= 0)
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(),txtAmount.Text.Trim(), tSettle, _Class.clsVariables.tCreditCardName.ToString());
                                    }
                                    else
                                    {
                                        dtSettle.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtEnterValue.Text.Trim(), 0, _Class.clsVariables.tCreditCardName.ToString());
                                    }
                                }
                            }
                            txtAmount.Text = string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                            txtEnterValue.Text = string.Empty;
                        }
                        else if (double.Parse(txtEnterValue.Text.Trim()) > double.Parse(txtAmount.Text.Trim()))
                        {
                            MyMessageBox.ShowBox("Credit Card Value not Exceed then settle Amount", "Warning");
                        }
                    }
                    else
                    {
                        if (SalesProject._Class.clsVariables.tControlFrom == "VOID")
                        {
                            CreditCard frm = new CreditCard();
                            SalesProject._Class.clsVariables.tCreditCardAmt = double.Parse(txtAmount.Text.Trim());
                            frm.ShowDialog();
                            txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tCreditCardAmt);
                            DataTable dtnets = new DataTable();
                            dtnets.Rows.Clear();
                           
                            //if (dtnets.Rows.Count > 0)
                            {
                                if (SalesProject._Class.clsVariables.tCreditCardName != "" && SalesProject._Class.clsVariables.tCreditCardName!=null)
                                {
                                    SqlCommand cmdCard = new SqlCommand("select Ledger_no  from Ledger_table where Ledger_name=@tName", con);
                                    cmdCard.Parameters.AddWithValue("@tName", _Class.clsVariables.tCreditCardName.ToString());
                                    SqlDataAdapter dp = new SqlDataAdapter(cmdCard);
                                    dp.Fill(dtnets);
                                    if (double.Parse(txtEnterValue.Text.Trim()) == double.Parse(txtAmount.Text.Trim()))
                                    {
                                        dtSettleVoid.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtAmount.Text.Trim(), txtAmount.Text.Trim(), "CreditCard");
                                        refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                                        txtAmount.Text = "0.00";
                                        txtEnterValue.Text = string.Empty;
                                      //  btnClose_Click(sender, e);
                                        funBtnClose();
                                    }
                                    if (txtEnterValue.Text.Trim() != "")
                                    {
                                        if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                                        {
                                            dtSettleVoid.Rows.Add(dtnets.Rows[0]["Ledger_no"].ToString(), txtAmount.Text.Trim(), txtEnterValue.Text.Trim(), "Cash");
                                            txtAmount.Text =string.Format("{0:0.00}", (double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                                            txtEnterValue.Text = string.Empty;
                                        }
                                        else if (double.Parse(txtEnterValue.Text.Trim()) > double.Parse(txtAmount.Text.Trim()))
                                        {
                                            MyMessageBox.ShowBox("Credit Card Value not Exceed then settle Amount", "Warning");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Settle Amount", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            //    trans.Rollback();
                //  con.Close();
            }
        }



        public void funLoad(Button btn)
        {
            try
            {
                txtEnterValue.Focus();
               // Button btn = (Button)sender;
                string myString = Convert.ToString(btn).ToString();
                string[] splitString = myString.Split('$');
                string firstItem = splitString[0];
                string lastItem = splitString[1];
                txtEnterValue.Text =(lastItem.ToString()=="")?"0.00":string.Format("{0:0.00}",double.Parse(lastItem.ToString()));
                txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                if (double.Parse(txtEnterValue.Text.Trim()) >= double.Parse(txtAmount.Text.Trim()))
                {

                    //if (lblTotAmt.Content.ToString() != "0.00" && lblTotAmt.Content.ToString() != "0")
                    {
                        SqlCommand cmd = new SqlCommand("sp_funBtnDolor1", con);
                        // cmd.Transaction = trans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                        cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                        //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                        cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt.Content.ToString()));
                        cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                        cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                        //double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                        double tot = (double.Parse(lblNetAmt.Content.ToString()) - ((double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())) - double.Parse(lblDiscount.Content.ToString())));
                        cmd.Parameters.AddWithValue("@RoundValue", tot);
                        for (int mnk = 0; mnk < dtDisplay.Rows.Count; mnk++)
                        {
                            if (dtDisplay.Rows[mnk]["Disc"].ToString().Trim() == "")
                            {
                                dtDisplay.Rows[mnk]["Disc"] = "0.00";
                            }
                        }
                        cmd.Parameters.AddWithValue("@tempTable", dtDisplay);
                        if (double.Parse(lblDiscount.Content.ToString()) > 0)
                        {
                            cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                            //cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
                            cmd.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(_Class.clsVariables.DiscountType) ? "NoDiscount" : _Class.clsVariables.DiscountType);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                            cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                        }
                        cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.Trim());
                        cmd.Parameters.AddWithValue("@tTxtAmount", txtAmount.Text.Trim());
                       // cmd.Parameters.AddWithValue("@tSmenNo", _Class.clsVariables.tempsalesmenLedgerNo);
                        cmd.Parameters.AddWithValue("@tSmenNo", string.IsNullOrEmpty(_Class.clsVariables.tempsalesmenLedgerNo) ? "0" : _Class.clsVariables.tempsalesmenLedgerNo);
                        cmd.Parameters.AddWithValue("@SmenRemarks", _Class.clsVariables.tempsalesmenNote);
                     //   cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();
                    //    trans.Commit();
                        refundAmt = (double.Parse(txtEnterValue.Text.ToString()) - double.Parse(txtAmount.Text.ToString()));
                      
                        gridDisplay.DataSource = null;  // Change gridDisplay.ItemsSource = null;
                        dt.Clear();
                        _Class.clsVariables.dtSingleFree.Rows.Clear();
                        lblNetAmt.Content = "0.00";
                        lblDiscount.Content = "0.00";
                        lblTotQty.Content = "0.00";
                        lblTotAmt.Content = "0.00";
                        lblTaxAmt.Content = "0.00";
                        funThankYou();
                        tChkClose = "Close";
                    }
                    //else
                    //{
                    //    MyMessageBox.ShowBox("Please Select Product First", "Warning");
                    //}
                    txtEnterValue.Focus();

                }
                if (double.Parse(txtEnterValue.Text.Trim()) < double.Parse(txtAmount.Text.Trim()))
                {
                    SqlCommand cmd = new SqlCommand("sp_funBtnDolor2", con);
                  //  cmd.Transaction = trans;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tValue", txtEnterValue.Text.ToString());
                    cmd.ExecuteNonQuery();

                    txtAmount.Text =string.Format("{0:0.00}",(double.Parse(txtAmount.Text.Trim()) - double.Parse(txtEnterValue.Text.Trim())));
                    txtEnterValue.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
              //  trans.Rollback();
               // this.Close();
                this.Visibility = Visibility.Hidden;
               // funBtnClose();
                tTenderClose = "Close";
                if (UCFormSettleEvent_settleClose != null)
                {
                    UCFormSettleEvent_settleClose();
                }
                // con.Close();
            }
        }
        string tChkClose = "";

        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnCash.Focus();
            }
            else
            {
                if (e.Key == Key.Decimal || e.Key==Key.OemPeriod)
                {
                    if (txtEnterValue.Text == "")
                    {
                        txtEnterValue.Text = "0";
                        txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                    }
                }
            }
        }
        private void txtEnterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                Int32 selectionStart = textBox.SelectionStart;
                Int32 selectionLength = textBox.SelectionLength;
                String newText = String.Empty;
                int count = 0;
                foreach (Char c in textBox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                    {
                        newText += c;
                        if (c == '.')
                            count += 1;
                    }
                }
                textBox.Text = newText;              
                textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
