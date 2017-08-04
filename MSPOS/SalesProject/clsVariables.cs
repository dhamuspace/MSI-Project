using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
namespace SalesProject._Class
{
    class clsVariables
    {

     public static SqlConnection connectionStr = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        public static string itemName;
        public static string itemQty;
        public static string itemRate;
        public static string itemAmt;
        public static string itemDisc;
        public static string itemSDisc;
        public static string itemOther;
        public static int itemIndex;
      //  public static string holdNo;
        public static string holded;
        public static string StopAtRate;
        public static string StopAtQty;
        public static string DiscountType = "NoDiscount";
        public static string tNoRead;
        public static int tBaudRate;
        public static string tPort;
        public static System.IO.Ports.Parity tParity;
        public static SerialPort serial = new SerialPort();


        public static string tWeightScaleEnable;
        public static string tVoidValue;
        public static string tVoidActionType;
        public static string UserType;
        public static string tUserNo="1";
        public static string tCounter="1";
        public static string tBranch="";
       // public static string tCustomerDisplayName;
        public static string tCounterName;
        public static string tUserName;
        public static double tCreditCardAmt;
        public static string tCreditCardName;
        public static DateTime tEndOfDayDate;
        public static string tControlFrom;
        public static bool tSetReturnInSales;
        public static bool tHideKeyboard;
        public static bool tStopAtRateF4= false;
        public static bool tStopAtQtyF4 = false;
        public static string tPrinterName;
       // public static string tImageLocation;
        public static string tPrintImageEnable="No";
        public static bool tAllowVoid = false;
        public static bool tAllowReturn = false;
        public static bool tViewReport = false;
        public static bool HAPaymentReport = false;
        public static bool StCounter = false;
        public static bool CashDrawer = false;
        public static string tMainDiscountType = "None";

        public static double tHouseACAmt;
        public static string tHouseACCustomerName;
        public static string tCtrCreditLimit;
        
        //tDiscountLedger=0 means bill Discout, 1 means Item Discount
        public static string tDiscountLedger="0";

        public static string tempGEnableThisDevice=string.Empty;
        public static string tempGPrinterName = string.Empty;
        public static string tempGPrinterType = string.Empty;
        public static string tempGPrintCopies = string.Empty;
        public static string tempGCharactersPerLine = string.Empty;
        public static string tempGFontsize = string.Empty;

        public static string tempGFontName = string.Empty;
        public static string tempGRound = string.Empty;
        public static string tempGPrintTime = string.Empty;
        public static string tempGPrintDate = string.Empty;
        public static string tempGPrintQunatityandRate = string.Empty;
        public static string tempGCutPaper = string.Empty;
        public static string tempGAutoPrint = string.Empty;

        public static string tempGPrintURate = string.Empty;


        public static string tempGPrintBottomLine1 = string.Empty;
        public static string tempGPrintBottomLine2 = string.Empty;
        public static string tempGPrintBottomLine3 = string.Empty;
        public static string tempGPrintBottomLine4 = string.Empty;
        public static string tempGPrintBottomLine5 = string.Empty;
        public static string tempGPrintBottomTime = string.Empty;
        public static string tempGPrintHeader = string.Empty;
        public static string tempGPrintSubtotal = string.Empty;
        public static string tempGPrintTopLine1 = string.Empty;
        public static string tempGPrintTopLine2 = string.Empty;
        public static string tempGPrintTopLine3 = string.Empty;

        public static string tempGPrintTopLine4 = string.Empty;
        public static string tempGPrintTopLine5 = string.Empty;
        public static string tempGPrintTopLine6 = string.Empty;
        public static string tempGPrintTopLine7 = string.Empty;
        public static string tempGPrintLineBelowLogo = string.Empty;
        public static string tempGPrintLineBelowTopText = string.Empty;
        public static string tempGPrintLineBelowHeader = string.Empty;
        public static string tempGPrintlineAboveTotal = string.Empty;
        public static string tempGPrintLineBelowTotal = string.Empty;
        public static string tempGPrintLineAboveBottomText = string.Empty;
        public static string tempGPrintLineBelowBottomText = string.Empty;
        public static string tempGPrintTax = string.Empty;
        public static string tempGDisplayTaxType = string.Empty;
        public static string tempGPrintCounterName = string.Empty;
        public static string tempGPrintUserName = string.Empty;
        public static string tempGPrintBillType = string.Empty;
        public static string tempGPrintLogo = string.Empty;

        public static string tempGReceiptHeaderLeftAlign = string.Empty;
        public static string tempGPrintPaymentMode = string.Empty;
        public static string tempGPrintPayThisAmountRightAlign = string.Empty;
        public static string tempGPrintPrinterItemName = string.Empty;
        public static string tempGPrintReceiptQtyCenterPosition = string.Empty;
        public static string QueueNo = string.Empty;//Declare Variable:

        public static string tempGReceiptNumber = string.Empty;
        public static string tempGDeliveryChargeText = string.Empty;
        public static string tempGAmountTendered = string.Empty;
        public static string tempGBottomLine1 = string.Empty;
        public static string tempGBottomLine2 = string.Empty;
        public static string tempGBottomLine3 = string.Empty;
        public static string tempGBottomLine4 = string.Empty;
        public static string tempGBottomLine5 = string.Empty;
        public static string tempGChangeDue = string.Empty;
        public static string tempGCustomerCopy = string.Empty;
        public static string tempGCustomerInformation = string.Empty;
        public static string tempGItemCount = string.Empty;
        public static string tempGOrderNumber = string.Empty;
        public static string tempGPayThisAmount = string.Empty;
        public static string tempGSubtotal = string.Empty;
        public static string tempGTopLine1 = string.Empty;
        public static string tempGTopLine2 = string.Empty;
        public static string tempGTopLine3 = string.Empty;
        public static string tempGTopLine4 = string.Empty;
        public static string tempGTopLine5 = string.Empty;
        public static string tempGTotalDue = string.Empty;
        public static string tempGPrintFreeItem = string.Empty;
        public static string tempGPrintSavedAmt = string.Empty;
        public static string tempGSavedAmount = string.Empty;
        public static string tempGManagerAutoPrint = string.Empty;
        public static string tempPrintTaxType = string.Empty;

        public static string tCustomerDisHomeLine1 = string.Empty;
        public static string tCustomerDisHomeLine2 = string.Empty;        

        public static bool tAllowOffer = false;
        public static string tstr1 = string.Empty; 
        public static DataTable dtSingleFree = new DataTable();
        public static string tSNetAmt = string.Empty;
        public static DataTable dtserailno = new DataTable();

        public static  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public static SerialPort spCustomerDis = new SerialPort();

        public static string tCustomerDisplayEnable = "No";

        public static string tSystemName = Environment.MachineName;
        public static DataTable dtItemGroup = new DataTable();
        public static string tDiscountAction = "";

        public static string fColor = string.Empty;
        public static string fPUpColor = string.Empty;
        public static string fPDownColor = string.Empty;
        public static string HcProcess = string.Empty;
        public static string PrinterType = string.Empty;
        //public static string temptopline6 = string.Empty;
        //public static string temptopline7 = string.Empty;

        public static string LoadPreviousBill = string.Empty;
        public static string tempsalesmenLedgerNo = string.Empty;
        public static string tempsalesmenNote = string.Empty;
        public static string tempCustomerLedgerNo = string.Empty;
        public static string tempCashdrawstringopen = string.Empty;
        public static bool tViewCash = false;
        public static string tempstopatqtyremove = "No";
        public clsVariables()
        {
          //  funGlobalCustomerDisplaySetting();
            try
            {

            b1:
                try
                {
                    if (serial.IsOpen == true)
                    {
                        serial.Close();
                    }
                    serial.Dispose();
                }
                catch (Exception)
                {
                    goto b1;
                }
                //Serial Port
                int count = 0;
            a1:
                count++;
                if (count < 150)
                {
                    serial.PortName = tPort;
                    serial.BaudRate = tBaudRate;
                    serial.Parity = tParity;
                    serial.DataBits = 8;
                    serial.StopBits = StopBits.One;
                    serial.RtsEnable = false;
                    serial.DtrEnable = true;
                    serial.Handshake = System.IO.Ports.Handshake.None;
                    //   serial.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);

                    // _serialPort.Open();
                    try
                    {
                        if (!serial.IsOpen)
                        {
                            serial.Open();
                        }
                    }
                    catch (Exception)
                    {
                        if (count < 100)
                        {
                            goto a1;
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Weight Scale Device Not Ready To Use");
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Weight Scale Device Not Ready To Use");
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (!serial.IsOpen)
                    {
                        serial.Open();
                    }
                }
                catch (Exception)
                {
                }
               // MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public static void funGlobalCustomerDisplaySetting()
        {
            try
            {
              //  _Class.clsVariables.tCounter = "1";
                DataTable dtCustomerDisplay = new DataTable();
                dtCustomerDisplay.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from CustomerDisplay_table where Counter=@tCounter",con);
                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtCustomerDisplay);
                if(dtCustomerDisplay.Rows.Count>0)
                {
                    _Class.clsVariables.tCustomerDisplayEnable = (dtCustomerDisplay.Rows[0]["Enable"].ToString().Trim() == "") ? "No" : dtCustomerDisplay.Rows[0]["Enable"].ToString().Trim();
                    if (dtCustomerDisplay.Rows[0]["Enable"].ToString().Trim() == "Yes")
                    {
                        if (spCustomerDis.IsOpen)
                        {
                            spCustomerDis.Close();
                        }
                        spCustomerDis.PortName = dtCustomerDisplay.Rows[0]["PortName"].ToString();
                        spCustomerDis.BaudRate = int.Parse(dtCustomerDisplay.Rows[0]["BaudRate"].ToString());
                        tCustomerDisHomeLine1 =Convert.ToString(dtCustomerDisplay.Rows[0]["HomeLine1"]);
                        tCustomerDisHomeLine2 = Convert.ToString(dtCustomerDisplay.Rows[0]["HomeLine2"]);
                        spCustomerDis.Parity = Parity.None;
                        spCustomerDis.DataBits = 8;
                        spCustomerDis.StopBits = StopBits.One;
                        spCustomerDis.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                tCustomerDisplayEnable = "No";
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //SqlCommand cmdUpdate = new SqlCommand("Update User_table set Active='False' where ctr_no=(select ctr_no from User_table where User_no=@tUsername)", con);
                //cmdUpdate.Parameters.AddWithValue("@tUsername", _Class.clsVariables.tUserNo);
                //// cmdUpdate.Parameters.AddWithValue("@tPassword", tPassword);
                //cmdUpdate.ExecuteNonQuery();
                
               
                MyMessageBox.ShowBox(ex.Message);
            }

        }
        public static void funGlobalReceiptSetting()
        {
            try
            {
                DataTable dtPrint = new DataTable();
                DataTable dtCompany = new DataTable();
                dtCompany.Rows.Clear();
                if (dtPrint.Columns.Count == 0)
                {
                    dtPrint.Columns.Add("Describ", typeof(string));
                    dtPrint.Columns.Add("Property", typeof(string));
                }
                dtPrint.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "GSET");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtPrint);

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

                if (dtPrint.Rows.Count > 0)
                {
                    for (int j = 0; j < dtPrint.Rows.Count; j++)
                    {
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Receipt Number")
                        {
                            tempGReceiptNumber = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Delivery Charge Text")
                        {
                            tempGDeliveryChargeText = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Guest Prefix")
                        //{
                        //  tempgP  temp = dtPrint.Rows[j]["Property"].ToString().Trim();
                        //}
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Guest Suffix")
                        //{

                        //}
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Amount Tendered")
                        {
                            tempGAmountTendered = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Bottom Line 1")
                        {
                            tempGBottomLine1 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Bottom Line 2")
                        {
                            tempGBottomLine2 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Bottom Line 3")
                        {
                            tempGBottomLine3 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Bottom Line 4")
                        {
                            tempGBottomLine4 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Bottom Line 5")
                        {
                            tempGBottomLine5 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Change Due")
                        {
                            tempGChangeDue = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Customer Copy")
                        {
                            tempGCustomerCopy = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Item Count")
                        {
                            tempGItemCount = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Order Amount")
                        //{
                        //    tempGOrderA = dtPrint.Rows[j]["Property"].ToString().Trim();
                        //}
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Pay This Amount")
                        {
                            tempGPayThisAmount = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Subtotal")
                        {
                            tempGSubtotal= dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Top Line1")
                        {
                            tempGTopLine1 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Top Line2")
                        {
                            tempGTopLine2 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Top Line3")
                        {
                            tempGTopLine3 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Top Line4")
                        {
                            tempGTopLine4 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Top Line5")
                        {
                            tempGTopLine5 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Total Due")
                        {
                            tempGTotalDue = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Saved Amount")
                        {
                           tempGSavedAmount = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        


                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Round")
                        {
                            tempGRound = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Qunatity and Rate")
                        {
                            tempGPrintQunatityandRate = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print URate")
                        {
                            tempGPrintURate = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }


                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Date")
                        {
                            tempGPrintDate = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Time")
                        {
                            tempGPrintTime = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }



                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Cut Paper")
                        {
                            tempGCutPaper = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Auto Print")
                        {
                            tempGAutoPrint = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Auto Settle Fractional Balance")
                        //{
                        //    tempGAutoSe = dtPrint.Rows[j]["Property"].ToString().Trim();
                        //}
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Auto Settle Orders With  No Transactions")
                        //{

                        //}
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bitmap Logo")
                        //{
                        //    tempGPrintBit = dtPrint.Rows[j]["Property"].ToString().Trim();
                        //}
                        //if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bitmap Barcode")
                        //{
                        //}
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bottom Line 1")
                        {
                            tempGPrintBottomLine1 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bottom Line 2")
                        {
                            tempGPrintBottomLine2 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bottom Line 3")
                        {

                            tempGPrintBottomLine3 = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bottom Line 4")
                        {
                            tempGPrintBottomLine4 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bottom Line 5")
                        {
                            tempGPrintBottomLine5 = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Bottom Time")
                        {

                            tempGPrintBottomTime = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Header")
                        {

                            tempGPrintHeader = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Subtotal")
                        {

                            tempGPrintSubtotal = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 1")
                        {
                            tempGPrintTopLine1 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 2")
                        {

                            tempGPrintTopLine2 = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 3")
                        {
                            tempGPrintTopLine3 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 4")
                        {
                            tempGPrintTopLine4 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 5")
                        {
                            tempGPrintTopLine5 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 6")
                        {
                            tempGPrintTopLine6 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Top Line 7")
                        {
                            tempGPrintTopLine7 = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Line Below Logo")
                        {
                            tempGPrintLineBelowLogo = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Line Below Top Text")
                        {
                            tempGPrintLineBelowTopText = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Line Below Header")
                        {
                            tempGPrintLineBelowHeader = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print line Above Total")
                        {
                            tempGPrintlineAboveTotal = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Line Below Total")
                        {
                            tempGPrintLineBelowTotal = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Line Above Bottom Text")
                        {
                            tempGPrintLineAboveBottomText = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Line Below Bottom Text")
                        {

                            tempGPrintLineBelowBottomText = dtPrint.Rows[j]["Property"].ToString().Trim();

                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Tax")
                        {
                            tempGPrintTax = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Display Tax Type")
                        {
                            tempGDisplayTaxType = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Counter Name")
                        {
                            tempGPrintCounterName = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print User Name")
                        {
                            tempGPrintUserName = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Bill Type")
                        {
                            tempGPrintBillType = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Logo")
                        {

                            tempGPrintLogo = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Receipt Header Left Align")
                        {

                            tempGReceiptHeaderLeftAlign = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }


                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Payment Mode")
                        {
                            tempGPrintPaymentMode = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }


                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Pay This Amount Right Align")
                        {
                            tempGPrintPayThisAmountRightAlign = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Printer Item Name")
                        {
                            tempGPrintPrinterItemName= dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim().Trim() == "Print Receipt Qty Center Position")
                        {
                            tempGPrintReceiptQtyCenterPosition = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim()== "Enable This Device*")
                        {
                            tempGEnableThisDevice = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim()== "Printer Name*")
                        {
                            tempGPrinterName = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim()== "Printer Type*")
                        {
                            tempGPrinterType = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                         if (dtPrint.Rows[j]["Describ"].ToString().Trim()== "Print Copies*")
                        {
                            tempGPrintCopies = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim()== "Characters Per Line*")
                        {
                            tempGCharactersPerLine = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Free Item")
                        {
                            tempGPrintFreeItem = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Print Saved Amt")
                        {
                          tempGPrintSavedAmt = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }

                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Manager Auto Print")
                        {
                            tempGManagerAutoPrint = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Queue Name")
                        {
                            QueueNo = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "Tax Type")
                        {
                            tempPrintTaxType = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "House A/C Address")
                        {
                            HcProcess = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }
                        if (dtPrint.Rows[j]["Describ"].ToString().Trim() == "PrinterType")
                        {
                            PrinterType = dtPrint.Rows[j]["Property"].ToString().Trim();
                        }                        

                    }

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }


        public static void funGlobalReceiptPrinterSetting()
        {
            try
            {

                DataTable dtPrinter = new DataTable();
                dtPrinter.Rows.Clear();
                SqlCommand cmdPrinter = new SqlCommand("Select * from ReceiptPrintSettings_table where Counter=@tCounter", con);
                cmdPrinter.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpPrinter = new SqlDataAdapter(cmdPrinter);
                adpPrinter.Fill(dtPrinter);
                if (dtPrinter.Rows.Count > 0)
                {
                    tempGEnableThisDevice = dtPrinter.Rows[0][0].ToString();
                    tempGPrinterName = dtPrinter.Rows[0][1].ToString();
                    tempGPrinterType = dtPrinter.Rows[0][2].ToString();
                    tempGPrintCopies = dtPrinter.Rows[0][3].ToString();
                    tempGCharactersPerLine = dtPrinter.Rows[0][4].ToString();
                }                     
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        
        public static void funControlSetting()
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from Control_table", connectionStr);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                tSetReturnInSales = false;
                tCtrCreditLimit = "1";
                if (dtNew.Rows.Count > 0)
                {
                    tSetReturnInSales =(dtNew.Rows[0]["ReturnInSales"].ToString().Trim()=="")?false: Convert.ToBoolean(dtNew.Rows[0]["ReturnInSales"].ToString());
                    tHideKeyboard =(dtNew.Rows[0]["Hide_Keyboard"].ToString().Trim()=="")?true: Convert.ToBoolean(dtNew.Rows[0]["Hide_Keyboard"].ToString());
                    //tCtrCreditLimit = 1. Allow   2. Warn   3. Stop  
                    tCtrCreditLimit = (dtNew.Rows[0]["ctl_CreditLimit"].ToString().Trim() == "") ? "1" : dtNew.Rows[0]["ctl_CreditLimit"].ToString().Trim();
                    tDiscountLedger = (dtNew.Rows[0]["DiscountLedger"].ToString().Trim() == "") ? "0" : dtNew.Rows[0]["DiscountLedger"].ToString().Trim();
                    tAllowOffer = (dtNew.Rows[0]["Ctl_FreeQty"].ToString().Trim() == "") ? Convert.ToBoolean("0") : Convert.ToBoolean(dtNew.Rows[0]["Ctl_FreeQty"].ToString().Trim());
                    tMainDiscountType = (dtNew.Rows[0]["GroupDiscounts"].ToString().Trim() == "") ? "None" : dtNew.Rows[0]["GroupDiscounts"].ToString().Trim();
                }

                dtItemGroup.Rows.Clear();
                SqlCommand cmdItemGroup = new SqlCommand(@"Select Item_no,Item_code,Item_name,Item_Grouptable.Item_groupno,Item_Grouptable.Item_groupname, Item_Grouptable.DisPerAmtType, Item_Grouptable.DisPerAmt from Item_table, Item_Grouptable where Item_Active=1 
and Item_table.item_Groupno=Item_Grouptable.Item_groupno and Group_visibility='True' order by Item_name ", connectionStr);                
                SqlDataAdapter adpItemGroup = new SqlDataAdapter(cmdItemGroup);
                adpItemGroup.Fill(dtItemGroup);

                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlCommand cmd1 = new SqlCommand("Select * from User_table where user_no=@tUserno", connectionStr);
                cmd1.Parameters.AddWithValue("@tUserno",_Class.clsVariables.tUserNo);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(dtNew1);
              //  tSetReturnInSales = false;
                if (dtNew1.Rows.Count > 0)
                {
                    if (dtNew1.Rows[0]["StopAtRate"].ToString().Trim() == "" || dtNew1.Rows[0]["StopAtRate"].ToString().Trim() == "No")
                    {
                        _Class.clsVariables.tStopAtRateF4 = false;
                    }
                    else
                    {
                        _Class.clsVariables.tStopAtRateF4 = true;
                    }

                    if (dtNew1.Rows[0]["StopAtQty"].ToString().Trim() == "" || dtNew1.Rows[0]["StopAtQty"].ToString().Trim() == "No")
                    {
                        _Class.clsVariables.tStopAtQtyF4 = false;
                    }
                    else
                    {
                        _Class.clsVariables.tStopAtQtyF4 = true;
                    }
                   // _Class.clsVariables.tStopAtRateF4 =(dtNew1.Rows[0]["StopAtRate"].ToString().Trim()=="")?false: Convert.ToBoolean(dtNew1.Rows[0]["StopAtRate"].ToString());
                    //_Class.clsVariables.tStopAtQtyF4 = (dtNew1.Rows[0]["StopAtQty"].ToString().Trim()=="")? false:Convert.ToBoolean(dtNew1.Rows[0]["StopAtQty"].ToString());
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        public static DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("SystemName");
            }
            dt.Rows.Clear();
            string chck1 = "";
            List<String> _ComputerNames = new List<String>();
            String _ComputerSchema = "Computer";
            System.DirectoryServices.DirectoryEntry _WinNTDirectoryEntries = new System.DirectoryServices.DirectoryEntry("WinNT:");
            foreach (System.DirectoryServices.DirectoryEntry _AvailDomains in _WinNTDirectoryEntries.Children)
            {
                foreach (System.DirectoryServices.DirectoryEntry _PCNameEntry in _AvailDomains.Children)
                {
                    if (_PCNameEntry.SchemaClassName.ToLower().Contains(_ComputerSchema.ToLower()))
                    {
                        chck1 = "1";
                        _ComputerNames.Add(_PCNameEntry.Name);
                        dt.Rows.Add(_PCNameEntry.Name);
                    }
                }
            }
            if (chck1 == "")
            {
                string name = Environment.MachineName;
                string name1 = System.Net.Dns.GetHostName();
                string name11 = System.Windows.Forms.SystemInformation.ComputerName;
                string name12 = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                dt.Rows.Add(name12.ToString());
            }
            return dt;
        }
        public static void Sheight_Width()
        {
            try
            {
                using (SqlDataAdapter cmd = new SqlDataAdapter("select Colour_no,Colour_name,Colour_mtname,SWidht,Sheight from Colour_table", con))
                {
                    DataTable dtColourValues = new DataTable();
                    dtColourValues.Rows.Clear();
                    cmd.Fill(dtColourValues);
                    if (dtColourValues.Rows.Count > 0)
                    {
                        //fColor = string.Empty;
                        //fPUpColor = string.Empty;
                        //fPDownColor = string.Empty;

                        fColor = dtColourValues.Rows[0]["Colour_name"].ToString();
                        fPUpColor = dtColourValues.Rows[0]["Colour_no"].ToString();
                        fPDownColor = dtColourValues.Rows[0]["Colour_mtname"].ToString();
                    }
                    else
                    {
                        fColor = "InactiveCaptionText";
                        fPUpColor = "Olive";
                        fPDownColor = "Olive";
                    }
                }
            }
            catch
            {

            }
        }
        //public static void funException(Exception ex)
        //{
        //    StackTrace st = new StackTrace(ex, true);
        //    //StackFrame frame = st.GetFrame(0);
        //    StackFrame frame = st.GetFrame(st.FrameCount - 1);
        //    string strfname1 = frame.GetFileName();
        //    string strfname = frame.GetMethod().Name;
        //    var line = st.GetFrame(st.FrameCount - 1).GetFileLineNumber();
        //    if (strfname1 != null)
        //    {
        //        frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname1));
        //    }
        //    else
        //    {
        //        frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname));
        //    }

        //}
    
    }

}


