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
using System.Drawing.Printing;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{

    public partial class Receipt : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=srigates_workout;Integrated Security=True;Pooling=False");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        List<string> allTheSheets = new List<string>();
        List<string> fonts = new List<string>();
        List<string> Numeric = new List<string>();
        List<string> charperline = new List<string>();

        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
 {
    List<String> filesFound = new List<String>();
    var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
    foreach (var filter in filters)
    {
        filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
    }
    
    return filesFound.ToArray();
 }


        string CounterNo = "";
        string counternumber = "";
        public Receipt()
        {
            InitializeComponent();
           
            dataGridView1.Columns[0].Width = 390;
            dataGridView1.Columns[1].Width = 390;
            dataGridView2.Columns[0].Width = 390;
            dataGridView2.Columns[1].Width = 390;
            dataGridView3.Columns[0].Width = 390;
            dataGridView3.Columns[1].Width = 390;
            //dataGridView1.AutoSizeColumnsMode.Equals("Fill");
            //dataGridView2.AutoSizeColumnsMode.Equals("Fill");
            //dataGridView3.AutoSizeColumnsMode.Equals("Fill");
            funConnectionStateCheck();

            CounterNo = _Class.clsVariables.tCounter.ToString();

            // fetch  a value from Db g_setting:
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                allTheSheets.Add(printer.ToString());
            }
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);
            }
            for (int i = 30; i <= 60; i++)
            {
                Numeric.Add(i.ToString());
                charperline.Add(i.ToString());
            }

            string[] copies = { "No Copies", "1 Copy", "2 Copy", "3 Copy" };
            string[] DiscRound = { "5cent", "10cent" };
            string[] AutoPrint = { "Yes", "No","After Confirm"};
            string[] ChargeType = { "None","Amount","Percent" };
            string[] PurchaseRateType = { "Before GST", "GST"};
            string[] ItemCostType = { "Purchase", "Purchase and Tax", "Purchase and Additions", "Purchase, Additions and Tax" };
            DataTable dtPrint = new DataTable();
            dtPrint.Rows.Clear();
            SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectAll", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tActionType", "GSET");
            dr = cmd.ExecuteReader();
            dtPrint.Load(dr);
            if (dtPrint.Rows.Count > 0)
            {
                SqlCommand cmdCounter = new SqlCommand("SP_SelectQuery", con);
                cmdCounter.CommandType=CommandType.StoredProcedure;
                cmdCounter.Parameters.AddWithValue("@ActionType", "COUNTERSETEL");
                cmdCounter.Parameters.AddWithValue("@itemName",CounterNo.ToString().Trim());
                cmdCounter.Parameters.AddWithValue("@ItemCode","");
                dr=cmdCounter.ExecuteReader();
                DataTable dtCounter=new DataTable ();
                dtCounter.Rows.Clear();
                dtCounter.Load(dr);
                if (dtCounter.Rows.Count > 0)
                {
                    counternumber = "1";
                    for (int i = 0; i < dtPrint.Rows.Count; i++)
                    {
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Font Name*")
                        {
                        }
                        else if (dtPrint.Rows[i]["Describ"].ToString() == "Font size*")
                        {
                        }
                        else
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells["Desc"].Value = dtPrint.Rows[i]["Describ"].ToString();
                        }
                        // dataGridView1.Rows[i].Cells["Prop"].Value = dtPrint.Rows[i]["Property"].ToString();

                        if (dtPrint.Rows[i]["Describ"].ToString() == "Enable This Device*")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = dtCounter.Rows[0]["Enable_This_Device"].ToString();
                        }
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Printer Name*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = allTheSheets;
                           // dataGridView1.Rows[i].Cells[1].Value = dtCounter.Rows[0]["Printer_Name"].ToString();
                            bool isChk = false;
                            for (int i1 = 0; i1 < allTheSheets.Count; i1++)
                            {
                                if (dtCounter.Rows[0]["Printer_Name"].ToString() == allTheSheets[i1])
                                {
                                    isChk = true;
                                    dataGridView1.Rows[i].Cells[1].Value = dtCounter.Rows[0]["Printer_Name"].ToString();
                                    break;
                                }
                            }
                            if (isChk == false)
                            {
                                dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(allTheSheets[0]);
                            }
                                
                        }
                        string[] printtype = { "Windows Drivers" };
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Printer Type*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = printtype;
                            dataGridView1.Rows[i].Cells[1].Value = dtCounter.Rows[0]["Printer_Type"].ToString();
                        }
                        //if (dtPrint.Rows[i]["Describ"].ToString() == "Font Name*")
                        //{
                        //    (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = fonts;
                        //    dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        //}
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Print Copies*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = copies;
                            dataGridView1.Rows[i].Cells[1].Value = dtCounter.Rows[0]["Print_Copies"].ToString();
                        }
                        //if (dtPrint.Rows[i]["Describ"].ToString() == "Font size*")
                        //{
                        //    (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = Numeric;
                        //    dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        //}
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = charperline;
                            dataGridView1.Rows[i].Cells[1].Value = dtCounter.Rows[0]["Characters_Per_Line"].ToString();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dtPrint.Rows.Count; i++)
                    {
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Font Name*")
                        {
                        }
                        else if (dtPrint.Rows[i]["Describ"].ToString() == "Font size*")
                        {
                        }
                        else
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells["Desc"].Value = dtPrint.Rows[i]["Describ"].ToString();
                        }
                        // dataGridView1.Rows[i].Cells["Prop"].Value = dtPrint.Rows[i]["Property"].ToString();

                        if (dtPrint.Rows[i]["Describ"].ToString() == "Enable This Device*")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        }
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Printer Name*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = allTheSheets;
                            dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        }
                        string[] printtype = { "Windows Drivers" };
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Printer Type*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = printtype;
                            dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        }
                        //if (dtPrint.Rows[i]["Describ"].ToString() == "Font Name*")
                        //{
                        //    (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = fonts;
                        //    dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        //}
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Print Copies*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = copies;
                            dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        }

                        //if (dtPrint.Rows[i]["Describ"].ToString() == "Font size*")
                        //{
                        //    (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = Numeric;
                        //    dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        //}
                        if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                        {
                            (dataGridView1.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = charperline;
                            dataGridView1.Rows[i].Cells[1].Value = dtPrint.Rows[i]["Property"].ToString();
                        }
                    }
                }
            }
            // fetch  a value from Db custom_text:
            DataTable cusdt = new DataTable();
            cusdt.Rows.Clear();
            SqlCommand cmd2 = new SqlCommand("sp_SalesCreationSelectAll", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@tActionType", "CUSTOMTEXT");
            dr = cmd2.ExecuteReader();
            cusdt.Load(dr);
            if (cusdt.Rows.Count > 0)
            {
                for (int i = 0; i < cusdt.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells["cDesc"].Value = cusdt.Rows[i]["Describ"].ToString();
                    dataGridView2.Rows[i].Cells["cProp"].Value = cusdt.Rows[i]["property"].ToString();
                }
            }
            // fetch  a value from DbRptset:
            string[] prlinebelowlogo = { "No Line", "Single Line", "Double Line" };
            string[] prlinebelowtoptext = { "No Line", "Single Line", "Double Line" };
            string[] prlinebelowheader = { "No Line", "Single Line", "Double Line" };
            string[] prlinebelowtotal = { "No Line", "Single Line", "Double Line" };
            string[] prlineabovetotal = { "No Line", "Single Line", "Double Line" };
            string[] prlineabovebottomtext = { "No Line", "Single Line", "Double Line" };
            string[] prlinebelowbottomtext = { "No Line", "Single Line", "Double Line" };
            string[] prlineTaxType = { "NoTax", "Inclusive", "Exclusive" };
           

            var filters = new String[] { "jpg", "jpeg", "png", "gif", "bmp" };
            String searchFolder =Application.StartupPath+"\\Logo";
            if (!System.IO.Directory.Exists(searchFolder))
            {
                System.IO.Directory.CreateDirectory(searchFolder);
            }



             //string[] printImage=GetFilesFrom(searchFolder, filters, false);
             //for (int mn = 0; mn < printImage.Length; mn++)
             //{
             //    string tPrintImage=printImage[mn].ToString().Trim();
             //    printImage[mn] = tPrintImage.Substring(tPrintImage.IndexOf("\\Logo") + 5, tPrintImage.Length - (tPrintImage.IndexOf("\\Logo") + 5));
             //}
           //  printImage. = "None";
            SqlCommand Rptcmd = new SqlCommand("select Rdesc,Rprop from Rptset ", con);

            SqlDataAdapter rptadp = new SqlDataAdapter(Rptcmd);
            DataTable rptdt = new DataTable();
            rptdt.Rows.Clear();

            rptadp.Fill(rptdt);
            if (rptdt.Rows.Count > 0)
            {
                for (int i = 0; i < rptdt.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells["RDesc"].Value = rptdt.Rows[i]["Rdesc"].ToString();

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Round")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = DiscRound;                        
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Auto Settle Fractional Balance")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Qunatity and Rate")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Date")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Time")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Auto Print")
                    {
//                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = AutoPrint;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Cut Paper")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Auto Settle Orders With  No Transactions")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bitmap Logo")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bitmap Barcode")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bottom Line 1")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bottom Line 2")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bottom Line 3")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bottom Line 4")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bottom Line 5")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Bottom Time")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Header")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Subtotal")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Top Line 1")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Top Line 2")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Top Line 3")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Top Line 4")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Top Line 5")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Line Below Logo")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlinebelowlogo;

                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Line Below Top Text")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlinebelowtoptext;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Line Below Header")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlinebelowheader;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print line Above Total")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlineabovetotal;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Line Below Total")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlinebelowtotal;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Line Above Bottom Text")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlineabovebottomtext;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Print Line Below Bottom Text")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlinebelowbottomtext;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Tax")
                    {                      
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Display Tax Type")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = prlineTaxType;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Counter Name")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print User Name")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Bill Type")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Logo")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Receipt Header Left Align")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Payment Mode")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Pay This Amount Right Align")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Printer Item Name")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Receipt Qty Center Position")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Service Charge")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Bank Charge")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Creditcard Charge")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Service Charge Type")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = ChargeType;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Bank Charge Type")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = ChargeType;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Creditcard Charge Type")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = ChargeType;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Charges Calculate from Net Amt")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Purchase Rate Calculation")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource =PurchaseRateType;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString() == "Item Cost Calculation")
                    {
                        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = ItemCostType;
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();

                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Free Item")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Print Saved Amt")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }

                    if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Manager Auto Print")
                    {
                        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    }
                    //if (rptdt.Rows[i]["Rdesc"].ToString().Trim() == "Logo Name")
                    //{
                    //    try
                    //    {
                    //        (dataGridView3.Rows[i].Cells[1] as DataGridViewComboBoxCell).DataSource = (object)printImage;
                    //        dataGridView3.Rows[i].Cells[1].Value = rptdt.Rows[i]["Rprop"].ToString();
                    //    }
                    //    catch (Exception)
                    //    {
                    //        dataGridView3.Rows[i].Cells[1].Value = "None";
                    //    }
                    //}
                }
            }

           
           
        }

        private void btn_ext_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            genset();
            rptset();
            customtext();
        }

        public void customtext()
        {
            //  //insert a value into Custom_text:
            for (int j = 0; j < dataGridView2.Rows.Count; j++)
            {
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Receipt Number")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));                      
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Delivery Charge Text")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Print Guest Prefix")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Print Guest Suffix")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Amount Tendered")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bottom Line 1")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bottom Line 2")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bottom Line 3")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bottom Line 4")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bottom Line 5")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Change Due")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Customer Copy")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Item Count")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Order Amount")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Pay This Amount")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Top Line1")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Top Line2")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Top Line3")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Top Line4")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Top Line5")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Total Due")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Service Charge Name")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bank Charge Name")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Creditcard Charge Name")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Service Charge Value")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Bank Charge Value")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Creditcard Charge Value")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() == "Saved Amount")
                {

                    SqlCommand cmd = new SqlCommand("update Custom_text set prop=@C1 where Describ='" + Convert.ToString(dataGridView2.Rows[j].Cells["cDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView2.Rows[j].Cells["cProp"].Value;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void rptset()
        {
            //insert a value into report set:
            for (int j = 0; j < dataGridView3.Rows.Count; j++)
            {
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Round")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Qunatity and Rate")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Date")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Time")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }



                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Cut Paper")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Auto Print")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Auto Settle Fractional Balance")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Auto Settle Orders With  No Transactions")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bitmap Logo")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bitmap Barcode")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bottom Line 1")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bottom Line 2")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bottom Line 3")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bottom Line 4")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bottom Line 5")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bottom Time")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Header")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Subtotal")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Top Line 1")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Top Line 2")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Top Line 3")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Top Line 4")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Top Line 5")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Line Below Logo")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Line Below Top Text")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Line Below Header")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print line Above Total")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Line Below Total")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Line Above Bottom Text")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }
                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Line Below Bottom Text")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Tax")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Display Tax Type")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();

                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Counter Name")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print User Name")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bill Type")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Logo")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Receipt Header Left Align")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }


                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Payment Mode")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }


                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Pay This Amount Right Align")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Printer Item Name")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Receipt Qty Center Position")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Service Charge")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Bank Charge")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Creditcard Charge")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Service Charge Type")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Bank Charge Type")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Creditcard Charge Type")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Charges Calculate from Net Amt")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Purchase Rate Calculation")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Item Cost Calculation")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Free Item")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Print Saved Amt")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }

                if (Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() == "Manager Auto Print")
                {

                    SqlCommand cmd = new SqlCommand("update Rptset set Rprop=@C1 where Rdesc='" + Convert.ToString(dataGridView3.Rows[j].Cells["RDesc"].Value).Trim() + "'", con);
                    {

                        cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                    }
                    cmd.Parameters["@C1"].Value = dataGridView3.Rows[j].Cells["Rprop"].Value;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void genset()
        {
            if (counternumber == "1")
            {
                // insert a value into G_set Table:
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    if (dataGridView1.Rows[j].Cells["Desc"].Value.ToString() == "Enable This Device*")
                    {
                        SqlCommand cmd = new SqlCommand("update ReceiptPrintSettings_table set Enable_This_Device=@C1 where Counter='" + CounterNo + "'", con);
                        {

                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));

                        }
                        cmd.Parameters["@C1"].Value = dataGridView1.Rows[j].Cells["Prop"].Value;
                        cmd.ExecuteNonQuery();
                    }
                    if (dataGridView1.Rows[j].Cells["Desc"].Value.ToString() == "Printer Name*")
                    {

                        SqlCommand cmd = new SqlCommand("update ReceiptPrintSettings_table set Printer_Name=@C1 where Counter='" + CounterNo + "'", con);
                        {

                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));


                        }
                        cmd.Parameters["@C1"].Value = dataGridView1.Rows[j].Cells["Prop"].Value;
                        cmd.ExecuteNonQuery();

                    }
                    if (dataGridView1.Rows[j].Cells["Desc"].Value.ToString() == "Printer Type*")
                    {

                        SqlCommand cmd = new SqlCommand("update ReceiptPrintSettings_table set Printer_Type=@C1 where Counter='" + CounterNo + "'", con);
                        {

                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));

                        }
                        cmd.Parameters["@C1"].Value = dataGridView1.Rows[j].Cells["Prop"].Value;
                        cmd.ExecuteNonQuery();

                    }
                    if (dataGridView1.Rows[j].Cells["Desc"].Value.ToString() == "Print Copies*")
                    {

                        SqlCommand cmd = new SqlCommand("update ReceiptPrintSettings_table set Print_Copies=@C1 where Counter='" + CounterNo + "'", con);
                        {

                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                        }
                        cmd.Parameters["@C1"].Value = dataGridView1.Rows[j].Cells["Prop"].Value;
                        cmd.ExecuteNonQuery();

                    }
                    if (dataGridView1.Rows[j].Cells["Desc"].Value.ToString() == "Characters Per Line*")
                    {

                        //SqlCommand cmd = new SqlCommand("update G_set set Property=@C1 where Describ='" + dataGridView1.Rows[j].Cells["Desc"].Value + "'", con);
                        SqlCommand cmd = new SqlCommand("update ReceiptPrintSettings_table set Characters_Per_Line=@C1 where Counter='" + CounterNo + "'", con);
                        {

                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.VarChar));
                        }
                        cmd.Parameters["@C1"].Value = dataGridView1.Rows[j].Cells["Prop"].Value;
                        cmd.ExecuteNonQuery();

                    }
                }
               
            }
            else
            {
                SqlCommand cmdCounter = new SqlCommand("SP_SelectQuery", con);
                cmdCounter.CommandType=CommandType.StoredProcedure;
                cmdCounter.Parameters.AddWithValue("@ActionType", "COUNTERSETEL");
                cmdCounter.Parameters.AddWithValue("@itemName",CounterNo.ToString().Trim());
                cmdCounter.Parameters.AddWithValue("@ItemCode","");
                dr=cmdCounter.ExecuteReader();
                DataTable dtCounter=new DataTable ();
                dtCounter.Rows.Clear();
                dtCounter.Load(dr);
                if (dtCounter.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("Update  ReceiptPrintSettings_table set Enable_This_Device=@Enableprinterdevice,Printer_Name=@printer_name,Printer_Type=@PrinterType,Print_Copies=@PrinterCopy,Characters_Per_Line=@Characters_Per_Line,Counter=@Counter", con);
                    cmd1.Parameters.AddWithValue("@Enableprinterdevice", dataGridView1.Rows[0].Cells["Prop"].Value);
                    cmd1.Parameters.AddWithValue("@printer_name", dataGridView1.Rows[1].Cells["Prop"].Value);
                    cmd1.Parameters.AddWithValue("@PrinterType", dataGridView1.Rows[2].Cells["Prop"].Value);
                    cmd1.Parameters.AddWithValue("@PrinterCopy", dataGridView1.Rows[3].Cells["Prop"].Value);
                    cmd1.Parameters.AddWithValue("@Characters_Per_Line", dataGridView1.Rows[4].Cells["Prop"].Value);
                    cmd1.Parameters.AddWithValue("@Counter", CounterNo.ToString().Trim());
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into  ReceiptPrintSettings_table(Enable_This_Device,Printer_Name,Printer_Type,Print_Copies,Characters_Per_Line,Counter) values(@Enable_This_Device,@Printer_Name,@Printer_Type,@Print_Copies,@Characters_Per_Line,@Counter)", con);
                    cmd.Parameters.AddWithValue("@Enable_This_Device", dataGridView1.Rows[0].Cells["Prop"].Value);
                    cmd.Parameters.AddWithValue("@Printer_Name", dataGridView1.Rows[1].Cells["Prop"].Value);
                    cmd.Parameters.AddWithValue("@Printer_Type", dataGridView1.Rows[2].Cells["Prop"].Value);
                    cmd.Parameters.AddWithValue("@Print_Copies", dataGridView1.Rows[3].Cells["Prop"].Value);
                    cmd.Parameters.AddWithValue("@Characters_Per_Line", dataGridView1.Rows[4].Cells["Prop"].Value);
                    cmd.Parameters.AddWithValue("@Counter", CounterNo.ToString().Trim());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Receipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataTable dtPrint = new DataTable();
        private void Receipt_Load(object sender, EventArgs e)
        {
            //dtPrint.Columns.Add("Describ", typeof(string));
            //dtPrint.Columns.Add("Property", typeof(string));
            //dtPrint.Rows.Clear();
            //SqlCommand cmd = new SqlCommand("Select Describ,Property from G_set", con);
            //con.Close();
            //con.Open();
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    dtPrint.Rows.Add(dr["Describ"].ToString(), dr["Property"].ToString());
            //}
            //con.Close();
            //SqlCommand cmd1 = new SqlCommand("Select RDesc,RProp from rptset", con);
            //con.Close();
            //con.Open();
            //dr = cmd1.ExecuteReader();
            //while (dr.Read())
            //{
            //    dtPrint.Rows.Add(dr["RDesc"].ToString(), dr["RProp"].ToString());
            //}
            //con.Close();
            //SqlCommand cmd2 = new SqlCommand("Select Describ,Prop from Custom_Text", con);
            //con.Close();
            //con.Open();
            //dr = cmd2.ExecuteReader();
            //while (dr.Read())
            //{
            //    dtPrint.Rows.Add(dr["Describ"].ToString(), dr["Prop"].ToString());
            //}
            //con.Close();


            funReceiptLoad();
            //dataGridView3.RowTemplate.Height = 50;
            //dataGridView2.RowTemplate.Height = 50;
            //dataGridView1.RowTemplate.Height = 50;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 35;
                row.DefaultCellStyle.Font = new Font("Arial", 13F, FontStyle.Regular);
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Height = 35;
                row.DefaultCellStyle.Font = new Font("Arial", 13F, FontStyle.Regular);
            }

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                row.Height = 35;
                row.DefaultCellStyle.Font = new Font("Arial", 13F, FontStyle.Regular);
            }

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            btn_apply.Focus();
        }
        public void funReceiptLoad()
        {
            try
            {
                if (dtPrint.Columns.Count == 0)
                {
                    dtPrint.Columns.Add("Describ", typeof(string));
                    dtPrint.Columns.Add("Property", typeof(string));
                }
                dtPrint.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "GSET");
                dr = cmd.ExecuteReader();
                dtPrint.Load(dr);

                SqlCommand cmd13 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@tActionType", "RPTSET");
                dr = cmd13.ExecuteReader();
                dtPrint.Load(dr);

                SqlCommand cmd2 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@tActionType", "CUSTOMTEXT");
                dr = cmd2.ExecuteReader();
                dtPrint.Load(dr);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
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

        SqlDataReader dr = null;
        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;
        string mainStr;
        double findCenterPosition;
        private void btn_testprint_Click(object sender, EventArgs e)
        {
            funReceiptLoad();
            mainStr = null;
            for (int i1 = 0; i1 < dtPrint.Rows.Count - 1; i1++)
            {
                if (dtPrint.Rows[i1]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i1]["Property"].ToString();
                }

                // print lint below logo
                if (dtPrint.Rows[i1]["Describ"].ToString() == "Print Line Below Logo")
                {
                    lineBelowLogo = dtPrint.Rows[i1]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";
                    }
                }
            }

            //top design start
            for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
            {
                if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i]["Property"].ToString();
                }

                // Top Line1
                //  topLine1="";
                if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 1")
                {
                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line1")
                            {
                                topLine1 = dtPrint.Rows[k]["Property"].ToString();


                                mainStr += topLine1;
                                for (int j = 0; j < (double.Parse(charPerLine) - topLine1.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                //////if (topLine1.Length <= double.Parse(charPerLine))
                                //////{
                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                                //////    if (findCenterPosition % 2 == 0)
                                //////    {
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine1;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    else
                                //////    {
                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine1;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    mainStr += "\n";
                                //////}
                            }
                        }
                    }
                }

                // Top Line2
                // topLine1="";
                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 2")
                {
                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line2")
                            {
                                topLine2 = dtPrint.Rows[k]["Property"].ToString();
                                mainStr += topLine2;
                                for (int j = 0; j < (double.Parse(charPerLine) - topLine2.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                //////if (topLine2.Length <= double.Parse(charPerLine))
                                //////{
                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
                                //////    if (findCenterPosition % 2 == 0)
                                //////    {
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine2;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    else
                                //////    {
                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine2;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    mainStr += "\n";
                                //////}
                            }
                        }
                    }
                }

                // Top Line3
                // topLine1 = "";
                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 3")
                {
                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line3")
                            {
                                topLine3 = dtPrint.Rows[k]["Property"].ToString();
                                mainStr += topLine3;
                                for (int j = 0; j < (double.Parse(charPerLine) - topLine3.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                //////if (topLine3.Length <= double.Parse(charPerLine))
                                //////{
                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
                                //////    if (findCenterPosition % 2 == 0)
                                //////    {
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine3;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    else
                                //////    {
                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine3;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    mainStr += "\n";
                                //////}
                            }
                        }
                    }
                }


                // Top Line4
                //topLine1 = "";
                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 4")
                {
                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line4")
                            {
                                topLine4 = dtPrint.Rows[k]["Property"].ToString();
                                mainStr += topLine4;
                                for (int j = 0; j < (double.Parse(charPerLine) - topLine4.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                ////if (topLine4.Length <= double.Parse(charPerLine))
                                ////{
                                ////    findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
                                ////    if (findCenterPosition % 2 == 0)
                                ////    {
                                ////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                ////        {
                                ////            mainStr += " ";
                                ////        }
                                ////        mainStr += topLine4;
                                ////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                ////        {
                                ////            mainStr += " ";
                                ////        }
                                ////    }
                                ////    else
                                ////    {
                                ////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                ////        {
                                ////            mainStr += " ";
                                ////        }
                                ////        mainStr += topLine4;
                                ////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                ////        {
                                ////            mainStr += " ";
                                ////        }
                                ////    }
                                ////    mainStr += "\n";
                                ////}
                            }
                        }
                    }
                }

               // Top Line5
                // topLine1 = "";
                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 5")
                {
                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line5")
                            {
                                topLine5 = dtPrint.Rows[k]["Property"].ToString();
                                mainStr += topLine5;
                                for (int j = 0; j < (double.Parse(charPerLine) - topLine5.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                //////if (topLine5.Length <= double.Parse(charPerLine))
                                //////{
                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
                                //////    if (findCenterPosition % 2 == 0)
                                //////    {
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine5;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    else
                                //////    {
                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////        mainStr += topLine5;
                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
                                //////        {
                                //////            mainStr += " ";
                                //////        }
                                //////    }
                                //////    mainStr += "\n";
                                //////}
                            }
                        }
                    }
                }



            }
            //header design start
            for (int i2 = 0; i2 < dtPrint.Rows.Count - 1; i2++)
            {
                if (dtPrint.Rows[i2]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i2]["Property"].ToString();
                }

                // print lint below logo
                if (dtPrint.Rows[i2]["Describ"].ToString() == "Print Line Below Header")
                {
                    lineBelowLogo = dtPrint.Rows[i2]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";
                    }
                }
            }


            for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
            {
                if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                }


                if (dtPrint.Rows[i3]["Describ"].ToString() == "Print Date")
                {
                    if (dtPrint.Rows[i3]["Property"].ToString() == "Yes")
                    {
                        string tChk = "Bill Date:" + DateTime.Now.ToString("dd/MM/yyyy");
                        mainStr += "Bill Date:" + DateTime.Now.ToString("dd/MM/yyyy");
                        double tTimeCount = (double.Parse(charPerLine) - (tChk.Length + 13));
                        for (int j = 0; j < tTimeCount; j++)
                        {
                            mainStr += " ";
                        }

                        for (int ii3 = 0; ii3 < dtPrint.Rows.Count - 1; ii3++)
                        {
                            if (dtPrint.Rows[ii3]["Describ"].ToString() == "Print Time")
                            {
                                if (dtPrint.Rows[ii3]["Property"].ToString() == "Yes")
                                {
                                    mainStr += "Time:" + DateTime.Now.ToShortTimeString();

                                }
                                else
                                {
                                    for (int j = 0; j < 13; j++)
                                    {
                                        mainStr += " ";
                                    }
                                }
                                mainStr += "\n";
                            }
                        }
                    }
                }
            }

            //receipt No 
            for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
            {
                if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                }


                if (dtPrint.Rows[i3]["Describ"].ToString() == "Receipt Number")
                {
                    if (dtPrint.Rows[i3]["Property"].ToString() != "")
                    {
                        string tChk = dtPrint.Rows[i3]["Property"].ToString() + "123";
                        mainStr +=dtPrint.Rows[i3]["Property"].ToString()+"123";
                        for (int j = 0; j < (double.Parse(charPerLine) - tChk.Length); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";

                    }
                }
            }


            //Counter Name
            for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
            {
                if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                }


                if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Print Counter Name")
                {
                    if (dtPrint.Rows[i3]["Property"].ToString() != "Yes")
                    {
                        string temp = _Class.clsVariables.tCounterName;
                        mainStr += temp;
                        for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";

                    }
                }
            }

            //UserName
            for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
            {
                if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                }


                if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Print User Name")
                {
                    if (dtPrint.Rows[i3]["Property"].ToString() != "Yes")
                    {
                        string temp = _Class.clsVariables.tUserName;
                        mainStr += temp;
                        for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";

                    }
                }
            }
           
            //Print Line Below Header
            for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
            {
                if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                }

                // print lint below logo
                if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                {
                    lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";
                    }
                }
            }

            //Print Products List-Starts
            //
            //
            for (int i5 = 0; i5 < dtPrint.Rows.Count - 1; i5++)
            {
                if (dtPrint.Rows[i5]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i5]["Property"].ToString();
                }

                // Print Bottom Line 1
                //  topLine1="";
                double location = 0.00;
                if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Qunatity and Rate")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        string tQtyHeading = "";
                        tQtyHeading = "Particulars";
                        //  mainStr += tQtyHeading;
                        double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 22));
                        for (int j = 0; j < chkCount; j++)
                        {
                            tQtyHeading += " ";
                        }
                        tQtyHeading += "  Qty  ";
                        tQtyHeading += "U/Rate ";
                        tQtyHeading += " Amount";
                        mainStr += tQtyHeading;
                        mainStr += "\n";
                        for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                        {
                            if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                            }

                            // print lint below logo
                            if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                            {
                                lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                                if (lineBelowLogo == "No Line")
                                {
                                    for (int j = 0; j < double.Parse(charPerLine); j++)
                                    {
                                        mainStr += " ";
                                    }
                                    mainStr += "\n";
                                }
                                if (lineBelowLogo == "Single Line")
                                {
                                    for (int j = 0; j < double.Parse(charPerLine); j++)
                                    {
                                        mainStr += "-";
                                    }
                                    mainStr += "\n";
                                }
                                else if (lineBelowLogo == "Double Line")
                                {
                                    for (int j = 0; j < double.Parse(charPerLine); j++)
                                    {
                                        mainStr += "=";
                                    }
                                    mainStr += "\n";
                                }
                            }
                        }



                        topLine1 = "ItemName1";
                        location = double.Parse(charPerLine) - 19;
                        if (topLine1.Length <= location)
                        {
                            findCenterPosition = (location - topLine1.Length);

                            mainStr += topLine1;
                            for (int j = 0; j < (findCenterPosition); j++)
                            {
                                mainStr += " ";
                            }
                            mainStr += " 10";
                            mainStr += " 100.00";
                            mainStr += " 1000.00";
                            mainStr += "\n";
                        }
                        topLine1 = "ItemName2";
                        location = double.Parse(charPerLine) - 19;
                        if (topLine1.Length <= location)
                        {
                            findCenterPosition = (location - topLine1.Length);

                            mainStr += topLine1;
                            for (int j = 0; j < (findCenterPosition); j++)
                            {
                                mainStr += " ";
                            }
                            mainStr += " 20";
                            mainStr += " 100.00";
                            mainStr += " 2000.00";
                            mainStr += "\n";
                        }

                    }

                    else
                    {
                        string tQtyHeading = "";
                        tQtyHeading = "Particulars";
                        mainStr += tQtyHeading;
                        double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));
                        for (int j = 0; j < tQtyCount; j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "    ";
                        mainStr += "       ";
                        mainStr += "Amount";
                        mainStr += "\n";
                               

                        topLine1 = "ItemName1";
                        location = double.Parse(charPerLine) - 8;
                        if (topLine1.Length <= location)
                        {
                            findCenterPosition = (location - topLine1.Length);

                            mainStr += topLine1;
                            for (int j = 0; j < (findCenterPosition); j++)
                            {
                                mainStr += " ";
                            }
                            //   mainStr += " 10";
                            //   mainStr += " 100.00";
                            mainStr += " 1000.00";
                            mainStr += "\n";
                        }
                        topLine1 = "ItemName2";
                        location = double.Parse(charPerLine) - 8;
                        if (topLine1.Length <= location)
                        {
                            findCenterPosition = (location - topLine1.Length);

                            mainStr += topLine1;
                            for (int j = 0; j < (findCenterPosition); j++)
                            {
                                mainStr += " ";
                            }
                            // mainStr += " 20";
                            // mainStr += " 100.00";
                            mainStr += " 2000.00";
                            mainStr += "\n";
                        }

                    }
                }
                if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Subtotal")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Subtotal")
                            {
                                topLine1 = dtPrint.Rows[k]["Property"].ToString();
                                if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                                {
                                    findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));

                                    for (int j = 0; j < (findCenterPosition); j++)
                                    {
                                        mainStr += " ";
                                    }
                                    mainStr += topLine1 + " 3000.00";

                                }

                                mainStr += "\n";
                            }
                        }
                    }
                }
            }

            //
            //
            //Print Products List-End





            //Print line Above Total
            for (int i10 = 0; i10 < dtPrint.Rows.Count - 1; i10++)
            {
                if (dtPrint.Rows[i10]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i10]["Property"].ToString();
                }


                if (dtPrint.Rows[i10]["Describ"].ToString() == "Print line Above Total")
                {
                    lineBelowLogo = dtPrint.Rows[i10]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";
                    }
                }
            }
            // Pay this amount

            //receipt No 
            for (int i9 = 0; i9 < dtPrint.Rows.Count - 1; i9++)
            {
                if (dtPrint.Rows[i9]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i9]["Property"].ToString();
                }


                if (dtPrint.Rows[i9]["Describ"].ToString() == "Pay This Amount")
                {
                    if (dtPrint.Rows[i9]["Property"].ToString() != "")
                    {
                        topLine1 = dtPrint.Rows[i9]["Property"].ToString() + ":$3000.00";
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            mainStr += "\n";
                        }

                    }
                }
            }



            //Print Line Below Total
            for (int i10 = 0; i10 < dtPrint.Rows.Count - 1; i10++)
            {
                if (dtPrint.Rows[i10]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i10]["Property"].ToString();
                }

                // print lint below logo
                if (dtPrint.Rows[i10]["Describ"].ToString() == "Print Line Below Total")
                {
                    lineBelowLogo = dtPrint.Rows[i10]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";
                    }
                }
            }
            // Your Order Number

            //receipt No 
            for (int i9 = 0; i9 < dtPrint.Rows.Count - 1; i9++)
            {
                if (dtPrint.Rows[i9]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i9]["Property"].ToString();
                }


                if (dtPrint.Rows[i9]["Describ"].ToString() == "Order Number")
                {
                    //if (dtPrint.Rows[i9]["Property"].ToString() != "")
                    //{
                    //    topLine1 = dtPrint.Rows[i9]["Property"].ToString() + "123";
                    //    if (topLine1.Length <= double.Parse(charPerLine))
                    //    {
                    //        findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                    //        if (findCenterPosition % 2 == 0)
                    //        {
                    //            for (int j = 0; j < (findCenterPosition / 2); j++)
                    //            {
                    //                mainStr += " ";
                    //            }
                    //            mainStr += topLine1;
                    //            for (int j = 0; j < (findCenterPosition / 2); j++)
                    //            {
                    //                mainStr += " ";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                    //            {
                    //                mainStr += " ";
                    //            }
                    //            mainStr += topLine1;
                    //            for (int j = 0; j < (findCenterPosition / 2); j++)
                    //            {
                    //                mainStr += " ";
                    //            }
                    //        }
                    //        mainStr += "\n";
                    //    }

                    //}
                }
            }

            //Print Line Above Bottom Text
            for (int i7 = 0; i7 < dtPrint.Rows.Count - 1; i7++)
            {
                if (dtPrint.Rows[i7]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i7]["Property"].ToString();
                }


                if (dtPrint.Rows[i7]["Describ"].ToString() == "Print Line Above Bottom Text")
                {
                    lineBelowLogo = dtPrint.Rows[i7]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";

                    }
                }
            }

            //bottom line
            for (int i5 = 0; i5 < dtPrint.Rows.Count - 1; i5++)
            {
                if (dtPrint.Rows[i5]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i5]["Property"].ToString();
                }

                // Print Bottom Line 1
                //  topLine1="";
                if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 1")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 1")
                            {
                                topLine1 = dtPrint.Rows[k]["Property"].ToString();
                                if (topLine1.Length <= double.Parse(charPerLine))
                                {
                                    findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                                    if (findCenterPosition % 2 == 0)
                                    {
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine1;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine1;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    mainStr += "\n";
                                }
                            }
                        }
                    }
                }

                // Print Bottom Line 2
                // topLine1="";
                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 2")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 2")
                            {
                                topLine2 = dtPrint.Rows[k]["Property"].ToString();
                                if (topLine2.Length <= double.Parse(charPerLine))
                                {
                                    findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
                                    if (findCenterPosition % 2 == 0)
                                    {
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine2;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine2;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    mainStr += "\n";
                                }
                            }
                        }
                    }
                }

                // Print Bottom Line 3
                // topLine1 = "";
                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 3")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 3")
                            {
                                topLine3 = dtPrint.Rows[k]["Property"].ToString();
                                if (topLine3.Length <= double.Parse(charPerLine))
                                {
                                    findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
                                    if (findCenterPosition % 2 == 0)
                                    {
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine3;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine3;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    mainStr += "\n";
                                }
                            }
                        }
                    }
                }


                // Print Bottom Line 4
                //topLine1 = "";
                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 4")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 4")
                            {
                                topLine4 = dtPrint.Rows[k]["Property"].ToString();
                                if (topLine4.Length <= double.Parse(charPerLine))
                                {
                                    findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
                                    if (findCenterPosition % 2 == 0)
                                    {
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine4;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine4;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    mainStr += "\n";
                                }
                            }
                        }
                    }
                }

               //Print Bottom Line 5
                // topLine1 = "";
                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 5")
                {
                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    {
                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                        {
                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 5")
                            {
                                topLine5 = dtPrint.Rows[k]["Property"].ToString();
                                if (topLine5.Length <= double.Parse(charPerLine))
                                {
                                    findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
                                    if (findCenterPosition % 2 == 0)
                                    {
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine5;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine5;
                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                                        {
                                            mainStr += " ";
                                        }
                                    }
                                    mainStr += "\n";
                                }
                            }
                        }
                    }
                }

            }

            //Print Line Below Header
            for (int i6 = 0; i6 < dtPrint.Rows.Count - 1; i6++)
            {
                if (dtPrint.Rows[i6]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i6]["Property"].ToString();
                }

                // print lint below logo
                if (dtPrint.Rows[i6]["Describ"].ToString() == "Print Line Below Bottom Text")
                {
                    lineBelowLogo = dtPrint.Rows[i6]["Property"].ToString();
                    if (lineBelowLogo == "No Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += " ";
                        }
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "-";
                        }
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        {
                            mainStr += "=";
                        }
                        mainStr += "\n";
                    }
                }
            }

            //Print Bottom Time
            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
            {
                if (dtPrint.Rows[i8]["Describ"].ToString() == "Characters Per Line*")
                {
                    charPerLine = dtPrint.Rows[i8]["Property"].ToString();
                }

                // Top Line1
                //  topLine1="";
                if (dtPrint.Rows[i8]["Describ"].ToString() == "Print Bottom Time")
                {
                    if (dtPrint.Rows[i8]["Property"].ToString() == "Yes")
                    {

                        topLine1 = DateTime.Now.ToString();
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            mainStr += "\n";
                        }
                    }
                }
            }
            // MessageBox.Show(mainStr);
           
            string tPrinterType = "";
            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
            {
                if (dtPrint.Rows[i8]["Describ"].ToString() == "Enable This Device*")
                {
                    if (dtPrint.Rows[i8]["Property"].ToString() == "Yes")
                    {
                        tPrinterType = "Receipt";
                    }
                }
            }

            int tNoPrint = 0;
            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
            {
                if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                {
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
                            if (dtPrint.Rows[i8]["Property"].ToString().ToUpper() == printerName.ToUpper())
                            {
                                isChkPrinter = true;
                                //rptReceiptReport rpt = new rptReceiptReport();
                                //CrystalDecisions.CrystalReports.Engine.TextObject str1 = ((CrystalDecisions.CrystalReports.Engine.TextObject)rpt.Section2.ReportObjects["Text1"]);
                                //str1.Text = mainStr;
                                //rpt.PrintToPrinter(0, true, 1, 0);
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
                            for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                            {
                                if (dtPrint.Rows[k]["Describ"].ToString() == "Print Copies*")
                                {
                                    topLine5 = dtPrint.Rows[k]["Property"].ToString();
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
                                       // RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), mainStr);

                                        System.Threading.Thread workerThread = new System.Threading.Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), mainStr));
                                        workerThread.Start();
                                        bool finished = workerThread.Join(3000);
                                        if (!finished)
                                        {
                                            workerThread.Abort();
                                            // CancelPrintJob();
                                        }
                                        // string s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 29, 86, 66, 0, 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                        // RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);
                                        byte[] byteOut;
                                        for (int i12 = 0; i12 < dtPrint.Rows.Count - 1; i12++)
                                        {
                                            if (dtPrint.Rows[i12]["Describ"].ToString() == "Cut Paper")
                                            {
                                                if (dtPrint.Rows[i12]["Property"].ToString() == "Yes")
                                                {

                                                    DataTable dtNew = new DataTable();
                                                    dtNew.Rows.Clear();
                                                    
                                                    SqlDataAdapter adp = new SqlDataAdapter("Select * from CashDrawerSetting_table", con);
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
                                                       // s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?
                                                      //  RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), System.Text.ASCIIEncoding.ASCII.GetString(byteOut));
                                                        System.Threading.Thread workerThread1 = new System.Threading.Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), System.Text.ASCIIEncoding.ASCII.GetString(byteOut)));
                                                        workerThread1.Start();
                                                        bool finished1 = workerThread1.Join(3000);
                                                        if (!finished1)
                                                        {
                                                            workerThread1.Abort();
                                                            // CancelPrintJob();
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

