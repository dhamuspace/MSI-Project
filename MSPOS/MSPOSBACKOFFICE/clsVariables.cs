using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace MSPOSBACKOFFICE._Class
{
   public class clsVariables
    {
        public static string itemName;
        public static string itemQty;
        public static string itemRate;
        public static string itemAmt;
        public static int itemIndex;
        public static string holdNo;
        public static string holded;
        public static string StopAtRate;
        public static string StopAtQty;
        public static string DiscountType;
        public static string tNoRead;
        public static int tBaudRate;
        public static string tPort;
        public static System.IO.Ports.Parity tParity;
        public static SerialPort serial = new SerialPort();
        public static string tWeightScaleEnable;
        public static string tVoidValue;
        public static string tVoidActionType;
        public static string UserType;
        public static string tUserNo;
        public static string tCounter;
        public static string tCustomerDisplayName;
        public static string tCounterName;
        public static string tUserName;
        public static double tCreditCardAmt;
        public static string tCreditCardName;
        public static DateTime tEndOfDayDate;
        public static string tControlFrom;
        public static Boolean tStopAtQtyF4;
        public static Boolean tStopAtRateF4;
        public static SerialPort spCustomerDis = new SerialPort();
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public static string tCustomerDisHomeLine1 = string.Empty;
        public static string tCustomerDisHomeLine2 = string.Empty;
        public static string tCustomerDisplayEnable = "No";
        public static string tBr_Name = "";
        //for color
        public static string fColor = string.Empty;
        public static string fPUpColor = string.Empty;
        public static string fPDownColor = string.Empty;

        public static void funGlobalCustomerDisplaySetting()
        {
            try
            {
                //  _Class.clsVariables.tCounter = "1";
                DataTable dtCustomerDisplay = new DataTable();
                dtCustomerDisplay.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from CustomerDisplay_table where Counter=@tCounter", con);
                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtCustomerDisplay);
                if (dtCustomerDisplay.Rows.Count > 0)
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
                        tCustomerDisHomeLine1 = Convert.ToString(dtCustomerDisplay.Rows[0]["HomeLine1"]);
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
        public clsVariables()
        {

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
                MyMessageBox.ShowBox(ex.Message, "Warning");
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
        public static void funException(Exception ex)
        {
            StackTrace st = new StackTrace(ex,true);
            //StackFrame frame = st.GetFrame(0);
            StackFrame frame = st.GetFrame(st.FrameCount - 1);
            string strfname1 = frame.GetFileName();
            string strfname = frame.GetMethod().Name;
            var line = st.GetFrame(st.FrameCount - 1).GetFileLineNumber();
            if (strfname1 != null)
            {
                frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname1));
            }
            else
            {
                frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname));
            }

        }

        public static object tBranch { get; set; }
    }
}
