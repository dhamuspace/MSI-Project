using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Configuration;

using System.IO;
using System.Threading;

namespace MSPOSBACKOFFICE
{
    #region
    public partial class frmPeripheralSettings : Form
    {

        public frmPeripheralSettings()
        {
            InitializeComponent();
            PeripheralSettings.SelectedIndex = 0;
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
      //  Win32PrintClass w32prn = new Win32PrintClass();
        string FileName;
        byte[] imgByteArr = new byte[0];

        private void tabPageScale_Click(object sender, EventArgs e)
        {



        }

       
        List<string> allTheSheets = new List<string>();
        private void PeripheralSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string CtrNo = "";
                //SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                //cmd1.Parameters.AddWithValue("@tCtrName", cmbCtrLCD.Text);
                //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                //DataTable dt = new DataTable();
                //dt.Rows.Clear();
                //adp1.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                //}


                if (PeripheralSettings.SelectedTab.Name == "tabPageScale")
                // if (PeripheralSettings.Equals("tabPageScale"))
                {
                    string[] ports = SerialPort.GetPortNames();
                    cmbSerialPort.Items.Clear();
                    foreach (string port in ports)
                    {
                        cmbSerialPort.Items.Add(port);
                    }
                    cmbBaud.Items.Clear();
                    int[] baud = { 75, 110, 134, 150, 300, 600, 1200, 1800, 2400, 4800, 7200, 9600, 14400, 19200, 38400, 57600, 115200, 128000 };
                    for (int i = 0; i < baud.Length; i++)
                    {
                        cmbBaud.Items.Add(baud[i]);
                    }
                    foreach (string s in Enum.GetNames(typeof(Parity)))
                    {
                        cmbParity.Items.Add(s);
                    }

                    string CtrNo = "";
                    SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    cmd1.Parameters.AddWithValue("@tCtrName", _Class.clsVariables.tCounter);
                    SqlDataAdapter adpCtrName = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adpCtrName.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    }

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdScale = new SqlCommand("Select * from WeightScale_table where counter=@tCounter", con);
                    cmdScale.Parameters.AddWithValue("@tCounter", CtrNo);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdScale);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table ctr_no=@CtrNo", con);
                        cmd.Parameters.AddWithValue("@CtrNo", dtNew.Rows[0]["Counter"].ToString());
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            cmbCounter.Text = dt1.Rows[0]["ctr_name"].ToString();
                        }

                        //cmbCounter.Text = dtNew.Rows[0]["Counter"].ToString();
                        cmbEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                        cmbParity.Text = dtNew.Rows[0]["Parity"].ToString();
                        cmbSerialPort.Text = dtNew.Rows[0]["PortName"].ToString();
                        cmbBaud.Text = dtNew.Rows[0]["BaudRate"].ToString();
                    }
                }
                if (PeripheralSettings.SelectedTab.Name == "tabPageCashDrawer")
                // if (PeripheralSettings.Equals("tabPageScale"))
                {
                    //string CtrNo = "";
                    //SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    //cmd1.Parameters.AddWithValue("@tCtrName", );
                    //SqlDataAdapter adpCtrName = new SqlDataAdapter(cmd1);
                    //DataTable dt = new DataTable();
                    //dt.Rows.Clear();
                    //adpCtrName.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    //}

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                    cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_no=@CtrNo", con);
                        cmd.Parameters.AddWithValue("@CtrNo", dtNew.Rows[0]["Counter"].ToString());
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            cmbCounter2.Text = dt1.Rows[0]["ctr_name"].ToString();
                        }

                        //cmbCounter2.Text = dtNew.Rows[0]["Counter"].ToString();
                        cmbDrawerEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                        cmbDrawerInterface.Text = dtNew.Rows[0]["Interface"].ToString();
                        cmbDrawerAction.Text = dtNew.Rows[0]["Action"].ToString();
                        txtDrawerCut.Text = dtNew.Rows[0]["PaperCut"].ToString();
                        txtDrawerOpen.Text = dtNew.Rows[0]["DrawOpen"].ToString();
                        txtCutandOpen.Text = dtNew.Rows[0]["CutAndOpen"].ToString();
                    }
                }
                if (PeripheralSettings.SelectedTab.Name == "tabCustomerDisplay")
                // if (PeripheralSettings.Equals("tabPageScale"))
                {
                    cmbCustomerDisDevice.Items.Clear();
                    foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                    {
                        cmbCustomerDisDevice.Items.Add(printer.ToString());
                    }

                    string[] ports = SerialPort.GetPortNames();
                    cmbCusDisplayPort.Items.Clear();
                    foreach (string port in ports)
                    {
                        cmbCusDisplayPort.Items.Add(port);
                    }

                    cmbCusDisplayBaudRate.Items.Clear();
                    int[] baud = { 75, 110, 134, 150, 300, 600, 1200, 1800, 2400, 4800, 7200, 9600, 14400, 19200, 38400, 57600, 115200, 128000 };
                    for (int i = 0; i < baud.Length; i++)
                    {
                        cmbCusDisplayBaudRate.Items.Add(baud[i]);
                    }

                    //string CtrNo = "";
                    //SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    //cmd1.Parameters.AddWithValue("@tCtrName", );
                    //SqlDataAdapter adpCtrName = new SqlDataAdapter(cmd1);
                    //DataTable dt = new DataTable();
                    //dt.Rows.Clear();
                    //adpCtrName.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    //}

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdCustomer = new SqlCommand("Select * from CustomerDisplay_table where counter=@tCounter", con);
                    cmdCustomer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdCustomer);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_no=@CtrNo", con);
                        cmd.Parameters.AddWithValue("@CtrNo", dtNew.Rows[0]["Counter"].ToString());
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            cmbCounter3.Text = dt1.Rows[0]["ctr_name"].ToString();
                        }

                        //cmbCounter3.Text = dtNew.Rows[0]["Counter"].ToString();
                        cmbCusDisplayEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                        cmbCusDisplayPort.Text = dtNew.Rows[0]["PortName"].ToString();
                        cmbCusDisplayBaudRate.Text = dtNew.Rows[0]["BaudRate"].ToString();
                        cmbCustomerDisDevice.Text = dtNew.Rows[0]["DeviceName"].ToString();
                        txtCusDisplay1line.Text = dtNew.Rows[0]["HomeLine1"].ToString();
                        txtCusDisplay2.Text = dtNew.Rows[0]["HomeLine2"].ToString();
                    }
                }
                if (PeripheralSettings.SelectedTab.Name == "tabLCDDisplay")
                {
                    //string CtrNo = "";
                    //SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    //cmd1.Parameters.AddWithValue("@tCtrName", _Class.clsVariables.tCounter);
                    //SqlDataAdapter adpCtrName = new SqlDataAdapter(cmd1);
                    //DataTable dt = new DataTable();
                    //dt.Rows.Clear();
                    //adpCtrName.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    //}

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdLCD = new SqlCommand("Select * from LCDDisplay_table where counter=@tCounter", con);
                    cmdLCD.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdLCD);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_no=@CtrNo", con);
                        cmd.Parameters.AddWithValue("@CtrNo", dtNew.Rows[0]["Counter"].ToString());
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        adp1.Fill(dt1);
                        if(dt1.Rows.Count>0)
                        {
                            cmbCtrLCD.Text = dt1.Rows[0]["ctr_name"].ToString();
                        }

                        //cmbCtrLCD.Text = dtNew.Rows[0]["Counter"].ToString();
                        cmdDeviceLCD.Text = dtNew.Rows[0]["Enable"].ToString();
                        cmbLogoNameLCD.Text = dtNew.Rows[0]["LogoName"].ToString();
                        txtCustomNameLCD.Text = dtNew.Rows[0]["Name"].ToString();
                        txtAddr1LCD.Text = dtNew.Rows[0]["AddrLine1"].ToString();
                        txtAddr2LCD.Text = dtNew.Rows[0]["AddrLine2"].ToString();
                        chkRateLCD.Checked = Convert.ToBoolean(dtNew.Rows[0]["EnableRate"].ToString());
                        chkAmountLCD.Checked = Convert.ToBoolean(dtNew.Rows[0]["EnableAmount"].ToString());
                    }
                }
                if (PeripheralSettings.SelectedTab.Name == "tabPrinterSettings")
                {
                    //string CtrNo = "";
                    //SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    //cmd1.Parameters.AddWithValue("@tCtrName", _Class.clsVariables.tCounter);
                    //SqlDataAdapter adpCtrName = new SqlDataAdapter(cmd1);
                    //DataTable dt = new DataTable();
                    //dt.Rows.Clear();
                    //adpCtrName.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    //}


                    cmbPrinterName.Items.Clear();
                    foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                    {
                        cmbPrinterName.Items.Add(printer.ToString());
                    }

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdLCD = new SqlCommand("Select * from ReceiptPrintSettings_table where counter=@tCounter", con);
                    cmdLCD.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdLCD);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_no=@CtrNo", con);
                        cmd.Parameters.AddWithValue("@CtrNo", dtNew.Rows[0]["Counter"].ToString());
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        adp1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            cmbCtrPrint.Text = dt1.Rows[0]["ctr_name"].ToString();
                        }

                        //cmbCtrLCD.Text = dtNew.Rows[0]["Counter"].ToString();
                        cmbEnablePrint.Text = dtNew.Rows[0]["Enable_This_Device"].ToString();
                        cmbPrinterName.Text = dtNew.Rows[0]["Printer_Name"].ToString();
                        cmbPrinterType.Text = dtNew.Rows[0]["Printer_Type"].ToString();
                        cmbPrintCopies.Text = dtNew.Rows[0]["Print_Copies"].ToString();
                        cmbCharPerLine.Text = dtNew.Rows[0]["Characters_Per_Line"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        double tReadingValue;
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        // The bulk of the clean-up code is implemented in Dispose(bool)
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        // free managed resources
        //        if (serial != null)
        //        {
        //            serial.Dispose();
        //            serial = null;
        //        }
        //    }
        //    // free native resources if there are any.
        //}
        private Queue<byte> recievedData = new Queue<byte>();
        private void btnTestScale_Click(object sender, EventArgs e)
        {
            try
            {
                lblWeightDisplay.Text = "0.00";
                if (cmbEnable.Text.Trim() == "Yes")
                {
                    int count = 0;
                ReadAgain:
                    count++;
                    try
                    {

                        ////if (_Class.clsVariables.serial.IsOpen)
                        ////{
                        ////    _Class.clsVariables.serial.BaseStream.Dispose();
                        ////    _Class.clsVariables.serial.Close();
                        ////    _Class.clsVariables.serial.Open();
                        ////}
                        Control.CheckForIllegalCrossThreadCalls = false;
                        //int b = _Class.clsVariables.serial.Read(bf, 0, 36);
                        _Class.clsVariables.serial.DtrEnable = true;
                        Thread.Sleep(30);
                        string data = _Class.clsVariables.serial.ReadExisting() + _Class.clsVariables.serial.ReadExisting();
                       // byte[] data1 = new byte[_Class.clsVariables.serial.BytesToRead];
                       // _Class.clsVariables.serial.Read(data1, 0, data1.Length);
                      //  data1.ToList().ForEach(b => recievedData.Enqueue(b));
               
                       
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
                        lblWeightDisplay.Text = tReadingValue.ToString();


                    }
                    catch (Exception)
                    {
                        if (count < 100)
                        {
                            goto ReadAgain;

                        }
                        else
                        {
                            MyMessageBox.ShowBox("Weight Scale not ready to use","Warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBaud.Text.Trim() != "" && cmbEnable.Text.Trim() != "" && cmbParity.Text.Trim() != "" && cmbSerialPort.Text.Trim() != "")
                {
                    string CtrNo = "";
                    SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    cmd1.Parameters.AddWithValue("@tCtrName", cmbCounter.Text.Trim());
                    SqlDataAdapter adpCtrNo = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adpCtrNo.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    }

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdScale = new SqlCommand("Select * from WeightScale_table where counter=@tCounter", con);
                    cmdScale.Parameters.AddWithValue("@tCounter", CtrNo);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdScale);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO [WeightScale_table]([Enable],[PortName],[BaudRate],[Parity],Counter) VALUES (@tEnable,@tPort,@tBaud,@tParity,@tCounter)", con);
                        cmd.Parameters.AddWithValue("@tEnable", cmbEnable.Text.Trim());
                        cmd.Parameters.AddWithValue("@tPort", cmbSerialPort.Text.Trim());
                        cmd.Parameters.AddWithValue("@tBaud", cmbBaud.Text.Trim());
                        cmd.Parameters.AddWithValue("@tParity", cmbParity.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update [WeightScale_table] Set Enable=@tEnable,PortName=@tPort,BaudRate=@tBaud,Parity=@tParity where Counter=@tCounter", con);
                        cmd.Parameters.AddWithValue("@tEnable", cmbEnable.Text.Trim());
                        cmd.Parameters.AddWithValue("@tPort", cmbSerialPort.Text.Trim());
                        cmd.Parameters.AddWithValue("@tBaud", cmbBaud.Text.Trim());
                        cmd.Parameters.AddWithValue("@tParity", cmbParity.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Enter All Fields", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        DataTable dtPrint = new DataTable();
        private void frmPeripheralSettings_Load(object sender, EventArgs e)
        {
            try
            {
                //string CtrNo = "";
                //SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                //cmd1.Parameters.AddWithValue("@tCtrName",);
                //SqlDataAdapter adpCtrName = new SqlDataAdapter(cmd1);
                //DataTable dt = new DataTable();
                //dt.Rows.Clear();
                //adpCtrName.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                //}

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmdScale = new SqlCommand("Select * from WeightScale_table where counter=@tCounter", con);
                cmdScale.Parameters.AddWithValue("@tCounter",  _Class.clsVariables.tCounter);
                SqlDataAdapter adp0 = new SqlDataAdapter(cmdScale);
                adp0.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_no=@CtrNo", con);
                    cmd.Parameters.AddWithValue("@CtrNo", dtNew.Rows[0]["Counter"].ToString());
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        cmbCounter.Text = dt1.Rows[0]["ctr_name"].ToString();
                    }
                    //cmbCounter.Text = dtNew.Rows[0]["Counter"].ToString();
                    cmbEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                    cmbParity.Text = dtNew.Rows[0]["Parity"].ToString();
                    cmbSerialPort.Text = dtNew.Rows[0]["PortName"].ToString();
                    cmbBaud.Text = dtNew.Rows[0]["BaudRate"].ToString();
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("Select ctr_name from counter_table where ctr_no=@tCtrNo", con);
                    cmd1.Parameters.AddWithValue("@tCtrNo", _Class.clsVariables.tCounter);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cmbCounter.Text = dt.Rows[0]["ctr_name"].ToString();
                    }
                }

                DataTable dtCtr = new DataTable();
                dtCtr.Rows.Clear();
                SqlDataAdapter adpCtr = new SqlDataAdapter("Select ctr_name from counter_table", con);
                adpCtr.Fill(dtCtr);
                if (dtCtr.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCtr.Rows.Count; i++)
                    {
                        cmbCounter.Items.Add(dtCtr.Rows[i]["ctr_name"].ToString());
                        cmbCounter2.Items.Add(dtCtr.Rows[i]["ctr_name"].ToString());
                        cmbCounter3.Items.Add(dtCtr.Rows[i]["ctr_name"].ToString());
                        cmbCtrLCD.Items.Add(dtCtr.Rows[i]["ctr_name"].ToString());
                        cmbCtrPrint.Items.Add(dtCtr.Rows[i]["ctr_name"].ToString());
                    }
                }

                dtPrint.Columns.Add("Describ", typeof(string));
                dtPrint.Columns.Add("Property", typeof(string));
                dtPrint.Rows.Clear();
                SqlCommand cmd11 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd11.CommandType = CommandType.StoredProcedure;
                cmd11.Parameters.AddWithValue("@tActionType", "GSET");
                SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
                adp11.Fill(dtPrint);
                // funConnectionStateCheck();
                //dr = cmd.ExecuteReader();
                //dtPrint.Load(dr);
                //while (dr.Read())
                //{
                //    dtPrint.Rows.Add(dr["Describ"].ToString(), dr["Property"].ToString());
                //}
                //con.Close();
                SqlCommand cmd13 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@tActionType", "RPTSET");
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd13);
                adp2.Fill(dtPrint);
                //dr = cmd13.ExecuteReader();
                //dtPrint.Load(dr);
                //while (dr.Read())
                //{
                //    dtPrint.Rows.Add(dr["RDesc"].ToString(), dr["RProp"].ToString());
                //}
                //con.Close();
                SqlCommand cmd2 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@tActionType", "CUSTOMTEXT");
                SqlDataAdapter adp3 = new SqlDataAdapter(cmd2);
                adp3.Fill(dtPrint);

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }
        byte[] byteOut;
        private void btnTestDrawer_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDrawerEnable.Text.Trim() == "Yes")
                {
                    PrintDialog pd = new PrintDialog();
                    string s;
                    // code = null;
                    if (cmbDrawerAction.Text == "Open")
                    {
                        //object[] temp = txtDrawerOpen.Text.ToString().Split(',');

                        //for (int i = 0; i < temp.Length; i++)
                        //{
                        //    code[i] =Convert.ToByte(temp[i]);
                        //}

                        if (txtDrawerOpen.Text.Trim() != "")
                        {
                            string[] byteStrings = txtDrawerOpen.Text.ToString().Split(',');

                            byteOut = new byte[byteStrings.Length];

                            for (int i = 0; i < byteStrings.Length; i++)
                            {
                                byteOut[i] = Convert.ToByte(byteStrings[i]);
                            }
                        }
                    }
                    if (cmbDrawerAction.Text == "Cut")
                    {
                        if (txtDrawerCut.Text.Trim() != "")
                        {
                            string[] byteStrings = txtDrawerCut.Text.ToString().Split(',');

                            byteOut = new byte[byteStrings.Length];

                            for (int i = 0; i < byteStrings.Length; i++)
                            {

                                byteOut[i] = Convert.ToByte(byteStrings[i]);

                            }
                        }
                        //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                    }
                    if (cmbDrawerAction.Text == "Cut and Open")
                    {
                        if (txtCutandOpen.Text.Trim() != "")
                        {
                            string[] byteStrings = txtCutandOpen.Text.ToString().Split(',');
                            byteOut = new byte[byteStrings.Length];
                            for (int i = 0; i < byteStrings.Length; i++)
                            {
                                byteOut[i] = Convert.ToByte(byteStrings[i]);
                            }
                        }
                        //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                    }
                    s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?
                    for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                    {
                        if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                        {
                           // RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);
                            System.Threading.Thread workerThread = new System.Threading.Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s));
                            workerThread.Start();
                            bool finished = workerThread.Join(3000);
                            if (!finished)
                            {
                                workerThread.Abort();
                                // CancelPrintJob();
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

        private void btnDrawerSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDrawerAction.Text.Trim() != "" && cmbDrawerEnable.Text.Trim() != "" && cmbDrawerInterface.Text.Trim() != "" && txtCutandOpen.Text.Trim() != "" && txtDrawerCut.Text.Trim() != "" && txtDrawerOpen.Text.Trim() != "")
                {
                    string CtrNo = "";
                    SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    cmd1.Parameters.AddWithValue("@tCtrName", cmbCounter2.Text.Trim());
                    SqlDataAdapter adpCtrNo = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adpCtrNo.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    }

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                    cmdDrawer.Parameters.AddWithValue("@tCounter", CtrNo);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO [CashDrawerSetting_table]([Enable],[Interface],[Action],[PaperCut],[DrawOpen],[CutAndOpen],[Counter]) VALUES (@tEnable,@tInterface,@tAction,@tPaperCut,@tDrawOpen,@tCutAndOpen,@tCounter)", con);
                        cmd.Parameters.AddWithValue("@tEnable", cmbDrawerEnable.Text.Trim());
                        cmd.Parameters.AddWithValue("@tInterface", cmbDrawerInterface.Text.Trim());
                        cmd.Parameters.AddWithValue("@tAction", cmbDrawerAction.Text.Trim());
                        cmd.Parameters.AddWithValue("@tPaperCut", txtDrawerCut.Text.Trim());
                        cmd.Parameters.AddWithValue("@tDrawOpen", txtDrawerOpen.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCutAndOpen", txtCutandOpen.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update [CashDrawerSetting_table] Set Enable=@tEnable,Interface = @tInterface,Action = @tAction,PaperCut = @tPaperCut,DrawOpen = @tDrawOpen,CutAndOpen = @tCutAndOpen where Counter=@tCounter", con);
                        cmd.Parameters.AddWithValue("@tEnable", cmbDrawerEnable.Text.Trim());
                        cmd.Parameters.AddWithValue("@tInterface", cmbDrawerInterface.Text.Trim());
                        cmd.Parameters.AddWithValue("@tAction", cmbDrawerAction.Text.Trim());
                        cmd.Parameters.AddWithValue("@tPaperCut", txtDrawerCut.Text.Trim());
                        cmd.Parameters.AddWithValue("@tDrawOpen", txtDrawerOpen.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCutAndOpen", txtCutandOpen.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Enter All Fields", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtDrawerOpen.Text = "27,112,48,55,121";
            txtDrawerCut.Text = "29,86,66,0";
            txtCutandOpen.Text = "29,86,66,0,27,112,0,64,240";
            cmbDrawerEnable.SelectedIndex = 1;
            cmbDrawerAction.SelectedIndex = 1;
            cmbDrawerInterface.SelectedIndex = 1;
        }

        private void btnCusDisplaySave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCusDisplayEnable.Text.Trim() != "" && cmbCusDisplayPort.Text.Trim() != "" && cmbCusDisplayBaudRate.Text.Trim() != "" )
                {
                     string CtrNo = "";
                SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                cmd1.Parameters.AddWithValue("@tCtrName", cmbCounter3.Text);
                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                }

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd2 = new SqlCommand("Select * from CustomerDisplay_table where Counter=@tCounter", con);
                    cmd2.Parameters.AddWithValue("@tCounter", CtrNo);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd2);
                    adp1.Fill(dtNew);
                    if (dtNew.Rows.Count == 0)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO CustomerDisplay_table(Enable,PortName,BaudRate,HomeLine1,HomeLine2,DeviceName,Counter) VALUES (@tEnable,@tPortName,@tBaudRate,@tInterface,@tAction,@tDeviceName,@tCounter)", con);
                        cmd.Parameters.AddWithValue("@tEnable", cmbCusDisplayEnable.Text.Trim());

                        cmd.Parameters.AddWithValue("@tPortName",cmbCusDisplayPort.Text.Trim());
                        cmd.Parameters.AddWithValue("@tBaudRate",cmbCusDisplayBaudRate.Text.Trim());

                        cmd.Parameters.AddWithValue("@tInterface", txtCusDisplay1line.Text);
                        cmd.Parameters.AddWithValue("@tAction", txtCusDisplay2.Text);
                        cmd.Parameters.AddWithValue("@tDeviceName", cmbCustomerDisDevice.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update CustomerDisplay_table Set Enable=@tEnable,PortName=@tPortName,BaudRate=@tBaudRate,HomeLine1 = @tInterface,HomeLine2 = @tAction,DeviceName=@tDeviceName where Counter=@tCounter", con);
                        cmd.Parameters.AddWithValue("@tEnable", cmbCusDisplayEnable.Text.Trim());

                        cmd.Parameters.AddWithValue("@tPortName", cmbCusDisplayPort.Text.Trim());
                        cmd.Parameters.AddWithValue("@tBaudRate", cmbCusDisplayBaudRate.Text.Trim());

                        cmd.Parameters.AddWithValue("@tInterface", txtCusDisplay1line.Text);
                        cmd.Parameters.AddWithValue("@tAction", txtCusDisplay2.Text);
                        cmd.Parameters.AddWithValue("@tDeviceName", cmbCustomerDisDevice.Text.Trim());
                        cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Enter All Fields", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnTestDisplay_Click(object sender, EventArgs e)
        {
            if (cmbCusDisplayEnable.Text.Trim() == "Yes" && cmbCusDisplayBaudRate.Text.Trim()!="" && cmbCusDisplayPort.Text.Trim()!="")
            {
               // w32prn.SetPrinterName(cmbCustomerDisDevice.Text.Trim());
                try
                {
                    _Class.clsVariables.funGlobalCustomerDisplaySetting();
                   // SerialPort sp = new SerialPort();

                    //sp.PortName =cmbCusDisplayPort.Text.Trim();
                    //sp.BaudRate = int.Parse(cmbCusDisplayBaudRate.Text.Trim());
                    //sp.Parity = Parity.None;
                    //sp.DataBits = 8;
                    //sp.StopBits = StopBits.One;
                    //sp.Open();
                    // sp.WriteLine("                                        ");
                    byte[] bytesToSend1 = new byte[1] { 0x0C }; // send hex code 0C to clear screen
                   _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                   _Class.clsVariables.spCustomerDis.WriteLine(txtCusDisplay1line.Text);
                    byte[] bytesToSend = new byte[1] { 0x0D }; // send hex code 0C to clear screen
                    _Class.clsVariables.spCustomerDis.Write(bytesToSend, 0, 1);
                    _Class.clsVariables.spCustomerDis.Write(txtCusDisplay2.Text);
                    

                    //w32prn.PrintText("a");
                    //w32prn.SetDeviceFont(7, "FontControl", false, false);

                    //w32prn.PrintText("b");
                    //w32prn.PrintText("c");
                    //for (int i = 0; i < 1; i++)
                    //{
                    //    w32prn.SetDeviceFont(7, "BCD 1st Line", false, false);
                    //    w32prn.PrintText(txtCusDisplay1line.Text);
                    //    w32prn.EndDoc();
                    //    w32prn.SetDeviceFont(7, "FontControl", false, false);
                    //    //w32prn.PrintText("d");
                    //    //w32prn.PrintText("n");

                    //    w32prn.SetDeviceFont(7, "BCD 2nd Line", false, false);
                    //    w32prn.PrintText(txtCusDisplay2.Text);
                    //    w32prn.EndDoc();
                    //}

                }
                catch (Exception ex)
                {
                    MyMessageBox.ShowBox(ex.Message, "Warning");
                }
            }
        }

        private void btn_exIt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CtrNo = "";
                SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                cmd1.Parameters.AddWithValue("@tCtrName", cmbCounter.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                }

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from WeightScale_table where Counter=@tCounter", con);
                cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                SqlDataAdapter adp0 = new SqlDataAdapter(cmd);
                adp0.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    //cmbCounter.Text = dtNew.Rows[0]["Counter"].ToString();
                    cmbEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                    cmbParity.Text = dtNew.Rows[0]["Parity"].ToString();
                    cmbSerialPort.Text = dtNew.Rows[0]["PortName"].ToString();
                    cmbBaud.Text = dtNew.Rows[0]["BaudRate"].ToString();
                }
                else
                {
                    //cmbEnable.Text = "";
                    //cmbParity.Text = "";
                    //cmbSerialPort.Text = "";
                    //cmbBaud.Text = "";
                    //cmbParity.Items.Add(dtNew.Rows[0]["Parity"].ToString());
                    //cmbSerialPort.Items.Add(dtNew.Rows[0]["PortName"].ToString());
                    //cmbBaud.Items.Add(dtNew.Rows[0]["BaudRate"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCounter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CtrNo = "";
                SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                cmd1.Parameters.AddWithValue("@tCtrName", cmbCounter2.Text);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp1.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                }

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from CashDrawerSetting_table where Counter=@tCounter", con);
                cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    //cmbCounter2.Text = dtNew.Rows[0]["Counter"].ToString();
                    cmbDrawerEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                    cmbDrawerInterface.Text = dtNew.Rows[0]["Interface"].ToString();
                    cmbDrawerAction.Text = dtNew.Rows[0]["Action"].ToString();
                    txtDrawerCut.Text = dtNew.Rows[0]["PaperCut"].ToString();
                    txtDrawerOpen.Text = dtNew.Rows[0]["DrawOpen"].ToString();
                    txtCutandOpen.Text = dtNew.Rows[0]["CutAndOpen"].ToString();
                }
                else
                {
                    //cmbDrawerEnable.Text = "";
                    //cmbDrawerInterface.Text = "";
                    //cmbDrawerAction.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCounter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CtrNo = "";
                SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                cmd1.Parameters.AddWithValue("@tCtrName", cmbCounter3.Text);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp1.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CtrNo = dt.Rows[0]["ctr_no"].ToString();
                }

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from CustomerDisplay_table where Counter=@tCounter", con);
                cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    // cmbCounter3.Text = dtNew.Rows[0]["Counter"].ToString();
                    cmbCusDisplayEnable.Text = dtNew.Rows[0]["Enable"].ToString();
                    cmbCustomerDisDevice.Text = dtNew.Rows[0]["DeviceName"].ToString();
                    txtCusDisplay1line.Text = dtNew.Rows[0]["HomeLine1"].ToString();
                    txtCusDisplay2.Text = dtNew.Rows[0]["HomeLine2"].ToString();
                }
                else
                {
                    //cmbCusDisplayEnable.Text = "";
                    //cmbCustomerDisDevice.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        MemoryStream ms;
        byte[] photo_aray = new byte[0];
        public void conv_photo()
        {
            FileStream fs = new FileStream(@FileName, FileMode.Open, FileAccess.Read);
            //Initialize a byte array with size of stream
            imgByteArr = new byte[fs.Length];
            //Read data from the file stream and put into the byte array
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
            fs.Close();
        }

        
        private void btnSaveLCD_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCtrLCD.Text.Trim() != "" && cmdDeviceLCD.Text.Trim() != "" && cmbLogoNameLCD.Text.Trim() != "")
                {
                    string CtrNo = "";
                    SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                    cmd1.Parameters.AddWithValue("@tCtrName", cmbCtrLCD.Text);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    }

                    if (check != null)
                    {

                        SqlCommand cmdCtrName = new SqlCommand("Select * from LCDDisplay_table where Counter=@tCtrNo", con);
                        cmdCtrName.Parameters.AddWithValue("@tCtrNo", CtrNo);
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmdCtrName);
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();
                        adp1.Fill(dtNew);
                        if (dtNew.Rows.Count == 0)
                        {
                            conv_photo();
                            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\CustomerLogo\\" + cmbCtrLCD.Text.Trim() + ".jpeg"))
                            {
                                System.IO.File.Copy(FileName, System.Windows.Forms.Application.StartupPath + "\\CustomerLogo\\" + cmbCtrLCD.Text.Trim() + ".jpeg");
                            }
                            string tPath = System.Windows.Forms.Application.StartupPath + "\\CustomerLogo\\" + cmbCtrLCD.Text.Trim() + ".jpeg";

                            SqlCommand cmd = new SqlCommand("INSERT INTO [LCDDisplay_table]([Enable],[LogoName],[Logo],[Name],[AddrLine1],[AddrLine2],[Counter],[EnableRate],[EnableAmount],[ImgLocation]) VALUES (@tEnable,@tLogoName,@tLogo,@tName,@tAddr1,@tAddr2,@tCounter,@tRate,@tAmount,@tImgLocation)", con);
                            cmd.Parameters.AddWithValue("@tEnable", cmdDeviceLCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tLogoName", cmbLogoNameLCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tLogo", imgByteArr);
                            cmd.Parameters.AddWithValue("@tName", (txtCustomNameLCD.Text.Trim() == "") ? "" : txtCustomNameLCD.Text);
                            cmd.Parameters.AddWithValue("@tAddr1", txtAddr1LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tAddr2", txtAddr2LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                            cmd.Parameters.AddWithValue("@tRate", (chkRateLCD.Checked == true) ? "True" : "False");
                            cmd.Parameters.AddWithValue("@tAmount", (chkAmountLCD.Checked == true) ? "True" : "False");
                            cmd.Parameters.AddWithValue("@tImgLocation", "\\CustomerLogo\\" + cmbCtrLCD.Text.Trim() + ".jpeg");
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            conv_photo();
                            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\CustomerLogo"))
                            {
                                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\CustomerLogo");
                            }
                            string tPath = System.Windows.Forms.Application.StartupPath + "\\CustomerLogo\\" + cmbCtrLCD.Text.Trim() + ".jpeg";
                            if (!File.Exists(tPath))
                            {
                                System.IO.File.Copy(FileName, tPath);
                            }
                            else
                            {
                                try
                                {
                                    GC.Collect();
                                    System.IO.File.Delete(tPath);
                                    GC.Collect();
                                    System.IO.File.Copy(FileName, tPath);
                                }
                                catch (Exception ex)
                                {
                                    MyMessageBox.ShowBox(ex.Message);
                                }
                            }

                            SqlCommand cmd = new SqlCommand("Update [LCDDisplay_table] Set Enable=@tEnable,LogoName=@tLogoName,Logo=@tLogo,Name=@tName,AddrLine1=@tAddr1,AddrLine2=@tAddr2,EnableRate=@tRate,EnableAmount=@tAmount,ImgLocation=@tImgLocation where Counter=@tCounter", con);
                            cmd.Parameters.AddWithValue("@tEnable", cmdDeviceLCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tLogoName", cmbLogoNameLCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tLogo", imgByteArr);
                            cmd.Parameters.AddWithValue("@tName", (txtCustomNameLCD.Text.Trim() == "") ? "" : txtCustomNameLCD.Text);
                            cmd.Parameters.AddWithValue("@tAddr1", txtAddr1LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tAddr2", txtAddr2LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                            cmd.Parameters.AddWithValue("@tRate", (chkRateLCD.Checked == true) ? "True" : "False");
                            cmd.Parameters.AddWithValue("@tAmount", (chkAmountLCD.Checked == true) ? "True" : "False");
                            cmd.Parameters.AddWithValue("@tImgLocation", "\\CustomerLogo\\" + cmbCtrLCD.Text.Trim() + ".jpeg");
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        SqlCommand cmdCtrName = new SqlCommand("Select * from LCDDisplay_table where Counter=@tCtrNo", con);
                        cmdCtrName.Parameters.AddWithValue("@tCtrNo", CtrNo);
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmdCtrName);
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();
                        adp1.Fill(dtNew);
                        if (dtNew.Rows.Count == 0)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO [LCDDisplay_table]([Enable],[LogoName],[Name],[AddrLine1],[AddrLine2],[Counter],[EnableRate],[EnableAmount]) VALUES (@tEnable,@tLogoName,@tName,@tAddr1,@tAddr2,@tCounter,@tRate,@tAmount)", con);
                            cmd.Parameters.AddWithValue("@tEnable", cmdDeviceLCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tLogoName", cmbLogoNameLCD.Text.Trim());
                            //cmd.Parameters.AddWithValue("@tLogo", null);
                            cmd.Parameters.AddWithValue("@tName", (txtCustomNameLCD.Text.Trim() == "") ? "" : txtCustomNameLCD.Text);
                            cmd.Parameters.AddWithValue("@tAddr1", txtAddr1LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tAddr2", txtAddr2LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                            cmd.Parameters.AddWithValue("@tRate", (chkRateLCD.Checked == true) ? "True" : "False");
                            cmd.Parameters.AddWithValue("@tAmount", (chkAmountLCD.Checked == true) ? "True" : "False");
                            //cmd.Parameters.AddWithValue("@tImgLocation", null);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {

                            SqlCommand cmd = new SqlCommand("Update [LCDDisplay_table] Set Enable=@tEnable,LogoName=@tLogoName,Name=@tName,AddrLine1=@tAddr1,AddrLine2=@tAddr2,EnableRate=@tRate,EnableAmount=@tAmount where Counter=@tCounter", con);
                            cmd.Parameters.AddWithValue("@tEnable", cmdDeviceLCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tLogoName", cmbLogoNameLCD.Text.Trim());
                            //cmd.Parameters.AddWithValue("@tLogo", null);
                            cmd.Parameters.AddWithValue("@tName", (txtCustomNameLCD.Text.Trim() == "") ? "" : txtCustomNameLCD.Text);
                            cmd.Parameters.AddWithValue("@tAddr1", txtAddr1LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tAddr2", txtAddr2LCD.Text.Trim());
                            cmd.Parameters.AddWithValue("@tCounter", CtrNo);
                            cmd.Parameters.AddWithValue("@tRate", (chkRateLCD.Checked == true) ? "True" : "False");
                            cmd.Parameters.AddWithValue("@tAmount", (chkAmountLCD.Checked == true) ? "True" : "False");
                            //cmd.Parameters.AddWithValue("@tImgLocation", null);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    check = null;
                }
                else
                {
                    MyMessageBox.ShowBox("Enter All Fields", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbLogoNameLCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLogoNameLCD.Text == "Logo")
            {
                txtCustomNameLCD.Enabled = false;
                btnImgLCD.Enabled = true;
                txtCustomNameLCD.Text = "";
            }
            else if (cmbLogoNameLCD.Text == "Name")
            {
                btnImgLCD.Enabled = false;
                txtCustomNameLCD.Enabled = true;
            }
        }

        public string check = null;
        private void btnImgLCD_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\CustomerLogo"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\CustomerLogo");
                }
                check = "Image";
                FileName = openFileDialog1.FileName;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCtrLCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CtrNo1 = "";
                SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                cmd1.Parameters.AddWithValue("@tCtrName", cmbCtrLCD.Text);
                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CtrNo1 = dt.Rows[0]["ctr_no"].ToString();
                }

                SqlCommand cmd = new SqlCommand("Select * from LCDDisplay_table where Counter=@tCounter", con);
                cmd.Parameters.AddWithValue("@tCounter", CtrNo1); 
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    //cmbCtrLCD.Text = dt.Rows[0]["Counter"].ToString();
                    cmdDeviceLCD.Text = dt1.Rows[0]["Enable"].ToString();
                    cmbLogoNameLCD.Text = dt1.Rows[0]["LogoName"].ToString();
                    txtCustomNameLCD.Text = dt1.Rows[0]["Name"].ToString();
                    txtAddr1LCD.Text = dt1.Rows[0]["AddrLine1"].ToString();
                    txtAddr2LCD.Text = dt1.Rows[0]["AddrLine2"].ToString();
                    chkRateLCD.Checked = Convert.ToBoolean(dt1.Rows[0]["EnableRate"].ToString());
                    chkAmountLCD.Checked = Convert.ToBoolean(dt1.Rows[0]["EnableAmount"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTestLCD_Click(object sender, EventArgs e)
        {

        }

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCtrPrint.Text != "" && cmbEnablePrint.Text != "" && cmbPrinterName.Text != "" && cmbPrinterType.Text != "" && cmbPrintCopies.Text != "" && cmbCharPerLine.Text != "")
                {
                    string CtrNo = "";
                    SqlCommand cmd = new SqlCommand("Select ctr_no from counter_table where ctr_name=@tCounter", con);
                    cmd.Parameters.AddWithValue("@tCounter", cmbCtrPrint.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        CtrNo = dt.Rows[0]["ctr_no"].ToString();
                    }

                    SqlCommand cmdCtrValues = new SqlCommand("Select * from ReceiptPrintSettings_table where Counter=@tCtrNo", con);
                    cmdCtrValues.Parameters.AddWithValue("@tCtrNo", CtrNo);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmdCtrValues);
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    adp1.Fill(dtNew);
                    if (dtNew.Rows.Count == 0)
                    {
                        SqlCommand cmdInsert = new SqlCommand("Insert into ReceiptPrintSettings_table(Enable_This_Device,Printer_Name,Printer_Type,Print_Copies,Characters_Per_Line,Counter) values(@Enable_This_Device,@Printer_Name,@Printer_Type,@Print_Copies,@Characters_Per_Line,@Counter)", con);
                        cmdInsert.Parameters.AddWithValue("@Enable_This_Device", cmbEnablePrint.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Printer_Name", cmbPrinterName.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Printer_Type", cmbPrinterType.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Print_Copies", cmbPrintCopies.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Characters_Per_Line", cmbCharPerLine.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Counter", CtrNo);
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdUpdate = new SqlCommand("Update ReceiptPrintSettings_table set Enable_This_Device=@Enable_This_Device,Printer_Name=@Printer_Name,Printer_Type=@Printer_Type,Print_Copies=@Print_Copies,Characters_Per_Line=@Characters_Per_Line where Counter=@Counter", con);
                        cmdUpdate.Parameters.AddWithValue("@Enable_This_Device", cmbEnablePrint.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Printer_Name", cmbPrinterName.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Printer_Type", cmbPrinterType.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Print_Copies", cmbPrintCopies.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Characters_Per_Line", cmbCharPerLine.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Counter", CtrNo);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                else
                {
                    MyMessageBox1.ShowBox("Empty Field");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCtrPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CtrNo1 = "";
                SqlCommand cmd1 = new SqlCommand("Select ctr_no from counter_table where ctr_name =@tCtrName", con);
                cmd1.Parameters.AddWithValue("@tCtrName", cmbCtrPrint.Text);
                SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    CtrNo1 = dt.Rows[0]["ctr_no"].ToString();
                }

                SqlCommand cmd = new SqlCommand("Select * from ReceiptPrintSettings_table where Counter=@tCounter", con);
                cmd.Parameters.AddWithValue("@tCounter", CtrNo1);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    //cmbCtrLCD.Text = dt.Rows[0]["Counter"].ToString();
                    cmbEnablePrint.Text = dt1.Rows[0]["Enable_This_Device"].ToString();
                    cmbPrinterName.Text = dt1.Rows[0]["Printer_Name"].ToString();
                    cmbPrinterType.Text = dt1.Rows[0]["Printer_Type"].ToString();
                    cmbPrintCopies.Text = dt1.Rows[0]["Print_Copies"].ToString();
                    cmbCharPerLine.Text = dt1.Rows[0]["Characters_Per_Line"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExitPrinter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCusDisplayBaudRate_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
    #endregion
}
