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
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.Reporting.WinForms;
using System.Threading;
using Microsoft.Win32;
namespace SalesProject
{
    /// <summary>
    /// Interaction logic for UCfrmCommission.xaml
    /// </summary>
    public delegate void DUCCommission();
   // public delegate void UCGroupCommission();
    public partial class UCfrmCommission : UserControl
    {
        public UCfrmCommission()
        {
            InitializeComponent();
           
            if (dtGroupCommission.Columns.Count == 0)
            {
                dtGroupCommission.Columns.Add("Particulars",typeof(string));
                dtGroupCommission.Columns.Add("Amount",typeof(string));
            }           
        }
        public event DUCCommission uccommission_click;
        public event DUCCommission ucgroupcommission_click;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCounterUserName.IsEnabled = true;
            DataTable dtUser = new DataTable();
            SqlCommand cmdUser = new SqlCommand("select User_name from User_table", con);
            SqlDataAdapter adpCounter = new SqlDataAdapter(cmdUser);
            adpCounter.Fill(dtUser);
            cmbCounterUserName.Items.Clear();
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                cmbCounterUserName.Items.Add(dtUser.Rows[i]["User_name"]);
            }
            cmbCounterUserName.Items.Add("All User");
            string strCountername = _Class.clsVariables.tCounterName;
            dpFromCommission.SelectedDate = DateTime.Now.Date;
            dpToCommission.SelectedDate = DateTime.Now.Date;
        }        
        string mainStr;
        string tPrintingType;
        double findCenterPosition;
        DataTable dtGroupCommission = new DataTable();
        DataTable dtItemCommission = new DataTable();
        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;        
        DataTable dtPrint = new DataTable();
        DataSet ds = new DataSet();
        private void btnCommissionReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbCounterUserName.Text != null ) 
                {
                    if (dpFromCommission.Text != "" && dpToCommission.Text != "")
                    {
                        if (uccommission_click != null)
                        {
                            uccommission_click();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Select the Date", "Warning");
                    }
                }
                else
                {
                    MyMessageBox1.ShowBox("Select the user", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            // pnlGroupCommissionReport.Visibility = Visibility.Visible;
            //try
            //{
            //    mainStr = "";
            //    tPrintingType = "";
            //    dtGroupCommission.Rows.Clear();
            //    dtItemCommission.Rows.Clear();
            //    string tQuery = "";

            //    //string tCounterName = _Class.clsVariables.tCounterName;
            //    //SqlCommand cmdTcounter = new SqlCommand("Select User_no, User_name, Ctr_no from User_table where Ctr_no=(Select Ctr_no from counter_table where ctr_name='@tCountername')");
            //    //cmdTcounter.Parameters.AddWithValue("@tCountername",tCounterName);
            //    //SqlDataAdapter adpCounter = new SqlDataAdapter(cmdTcounter);


            //    if (cmbCounterUserName.Text == "All Counter")
            //    {
            //        tQuery = "Select User_no, User_name, Ctr_no from User_table";
            //    }
            //    else
            //    {
            //        tQuery = "Select User_no, User_name, Ctr_no from User_table where Ctr_no=(Select Ctr_no from counter_table where ctr_name=@tCounter)";
            //    }
            //    DataTable dtCommission = new DataTable();
            //    dtCommission.Rows.Clear();
            //    DataTable dtUser = new DataTable();
            //    dtUser.Rows.Clear();
            //    SqlCommand cmd = new SqlCommand(tQuery, con);
            //    cmd.Parameters.AddWithValue("@tCounter", Convert.ToString(cmbCounterUserName.SelectedItem));
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    adp.Fill(dtUser);
            //    for (int mn = 0; mn < dtUser.Rows.Count; mn++)
            //    {
            //        funCommission(Convert.ToString(dtUser.Rows[mn]["User_no"]), Convert.ToString(dtUser.Rows[mn]["User_name"]), Convert.ToString(dtUser.Rows[mn]["Ctr_no"]), "ITEM");
            //    }
            //    UCFrmManagerMain frmMain = new UCFrmManagerMain();
            //    frmMain.gridItemsManager.DataSource = null;
            //    frmMain.gridItemsManager.Rows.Clear();
            //    frmMain.gridItemsManager.Refresh();
            //    frmMain.gridItemsManager.DataSource = dtGroupCommission;
            //    frmMain.gridItemsManager.Columns[0].Width = 300;
            //    frmMain.gridItemsManager.Columns[1].Width = 150;
            //    frmMain.gridItemsManager.ColumnHeadersVisible = true;
            //   //gridItemsManager.DataSource = dtGroupCommission;
            //    //gridItemsManager.Columns[0].Width = 300;
            //    //gridItemsManager.Columns[1].Width = 150;
            //    //gridItemsManager.ColumnHeadersVisible = true;
            //    funPrintHeaderPart();

            //    topLine1 = "ITEMWISE COMMISSION REPORT";
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


            //    topLine1 = DateTime.Now.ToString("dd/MM/yyyy");
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
            //    //        break;
            //    //    }

            //    //}


            //    topLine1 = "Report Date : " + dpFromCommission.SelectedDate.Value.Day + "/" + dpFromCommission.SelectedDate.Value.Month + "/" + dpFromCommission.SelectedDate.Value.Year + " - " + dpToCommission.SelectedDate.Value.Day + "/" + dpToCommission.SelectedDate.Value.Month + "/" + dpToCommission.SelectedDate.Value.Year;
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
            //    topLine1 = "Report Counter : " + cmbCounterUserName.Text;
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
            //    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            //    if (lineBelowLogo == "No Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += " ";
            //        }
            //        mainStr += "\n";
            //    }
            //    if (lineBelowLogo == "Single Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "-";
            //        }
            //        mainStr += "\n";
            //    }
            //    else if (lineBelowLogo == "Double Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "=";
            //        }
            //        mainStr += "\n";
            //    }
            //    //        break;
            //    //    }
            //    //}
            //    ds.Tables.Clear();
            //    //  mainStrSub = "";


            //    string tempStr = null;
            //    string tQtyHeading = "";
            //    tQtyHeading = "Particulars";
            //    //  mainStr += tQtyHeading;
            //    double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 8));
            //    for (int j = 0; j < chkCount; j++)
            //    {
            //        tQtyHeading += " ";
            //    }
            //    tQtyHeading += " Amount";
            //    // tQtyHeading += "U/Rate ";
            //    //tQtyHeading += "   Profit";
            //    mainStr += tQtyHeading;
            //    mainStr += "\n";

            //    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            //    if (lineBelowLogo == "No Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += " ";
            //        }
            //        mainStr += "\n";
            //    }
            //    if (lineBelowLogo == "Single Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "-";
            //        }
            //        mainStr += "\n";
            //    }
            //    else if (lineBelowLogo == "Double Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "=";
            //        }
            //        mainStr += "\n";
            //    }
            //    //        break;
            //    //    }
            //    //}
            //    for (int mn = 0; mn < dtGroupCommission.Rows.Count; mn++)
            //    //foreach (DataRow row in dgsales.Rows)
            //    {
            //        // object[] array = dgsales.Rows[mn].;
            //        bool isChk = false;

            //        if (isChk == false)
            //        {
            //            for (int i = 0; i < 2; i++)
            //            {
            //                tempStr = dtGroupCommission.Rows[mn][i].ToString();
            //                //  MessageBox.Show(tempStr.Length.ToString());
            //                findCenterPosition = (double.Parse(charPerLine) - 8);
            //                if (i == 0)
            //                {
            //                    if (tempStr.Length <= (int)findCenterPosition)
            //                    {
            //                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
            //                    }
            //                    else
            //                    {
            //                        string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
            //                        //    MessageBox.Show(temp);
            //                        int chkSpace = temp.LastIndexOf(" ");
            //                        int loc = (temp.Length - temp.LastIndexOf(" "));
            //                        //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
            //                        if (chkSpace != -1)
            //                        {
            //                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
            //                            //   MessageBox.Show(mainStr.ToString());
            //                            for (int j = 0; j < loc + 8; j++)
            //                            {
            //                                mainStr += " ";
            //                            }
            //                            mainStr += "\n";
            //                            string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
            //                            // mainStr += temp1;
            //                            if (temp1.Length <= (int)findCenterPosition)
            //                            {
            //                                mainStr += temp1.PadRight((int)findCenterPosition, ' ');
            //                            }
            //                        }
            //                        else
            //                        {
            //                            mainStr += temp.ToString();
            //                        }

            //                    }
            //                }

            //                if (i == 1)
            //                {
            //                    if (tempStr.Length < 8)
            //                    {
            //                        mainStr += tempStr.PadLeft(7, ' ');
            //                    }
            //                }
            //                if (i == 10)
            //                {
            //                    // mainStr += tempStr.PadRight(7, ' ');
            //                    if (tempStr.Length <= 7)
            //                    {
            //                        mainStr += tempStr.PadLeft(7, ' ');
            //                    }
            //                }
            //                if (i == 4)
            //                {
            //                    if (tempStr.Length <= 10)
            //                    {
            //                        mainStr += tempStr.PadLeft(10, ' ');
            //                    }
            //                }
            //                // tPrintText += tempStr;
            //            }
            //            mainStr += "\n";
            //        }
            //    }

            //    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            //    if (lineBelowLogo == "No Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += " ";
            //        }
            //        mainStr += "\n";
            //    }
            //    if (lineBelowLogo == "Single Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "-";
            //        }
            //        mainStr += "\n";
            //    }
            //    else if (lineBelowLogo == "Double Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "=";
            //        }
            //        mainStr += "\n";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MyMessageBox.ShowBox(ex.Message, "Warning");
            //}
        }       
        private void btnGroupCommissionRpt_Click(object sender, RoutedEventArgs e)
        {
            if (ucgroupcommission_click != null)
            {
                ucgroupcommission_click();
            }

            //try
            //{
            //    mainStr = "";
            //    tPrintingType = "";
            //    dtGroupCommission.Rows.Clear();
            //    dtItemCommission.Rows.Clear();
            //    string tQuery = "";
            //    if (cmbCounterUserName.Text == "All Counter")
            //    {
            //        tQuery = "Select User_no, User_name, Ctr_no from User_table";
            //    }
            //    else
            //    {
            //        tQuery = "Select User_no, User_name, Ctr_no from User_table where Ctr_no=(Select Ctr_no from counter_table where ctr_name=@tCounter)";
            //    }
            //    DataTable dtCommission = new DataTable();
            //    dtCommission.Rows.Clear();
            //    DataTable dtUser = new DataTable();
            //    dtUser.Rows.Clear();
            //    SqlCommand cmd = new SqlCommand(tQuery, con);
            //    cmd.Parameters.AddWithValue("@tCounter", Convert.ToString(cmbCounterUserName.SelectedItem));
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    adp.Fill(dtUser);
            //    for (int mn = 0; mn < dtUser.Rows.Count; mn++)
            //    {
            //        funCommission(Convert.ToString(dtUser.Rows[mn]["User_no"]), Convert.ToString(dtUser.Rows[mn]["User_name"]), Convert.ToString(dtUser.Rows[mn]["Ctr_no"]), "GROUP");
            //    }
            //    UCFrmManagerMain frmMain = new UCFrmManagerMain();
            //    frmMain.gridItemsManager.DataSource = null;
            //    frmMain.gridItemsManager.Rows.Clear();
            //    frmMain.gridItemsManager.Refresh();              
            //    frmMain.gridItemsManager.DataSource = dtGroupCommission;
            //    frmMain.gridItemsManager.Columns[0].Width = 300;
            //    frmMain.gridItemsManager.Columns[1].Width = 150;
            //    frmMain.gridItemsManager.ColumnHeadersVisible = true;
            //    funPrintHeaderPart();
            //    //gridItemsManager.DataSource = dtGroupCommission;
            //    //gridItemsManager.Columns[0].Width = 300;
            //    //gridItemsManager.Columns[1].Width = 150;
            //    //gridItemsManager.ColumnHeadersVisible = true;
            //    //funPrintHeaderPart();

            //    topLine1 = "GROUPWISE COMMISSION REPORT";
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


            //    topLine1 = DateTime.Now.ToString("dd/MM/yyyy");
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
            //    //        break;
            //    //    }

            //    //}




            //    topLine1 = "Report Date : "+ dpFromCommission.SelectedDate.Value.Day + "/" + dpFromCommission.SelectedDate.Value.Month + "/" + dpFromCommission.SelectedDate.Value.Year+ " - "+dpToCommission.SelectedDate.Value.Day+ "/" +dpToCommission.SelectedDate.Value.Month+ "/"+dpToCommission.SelectedDate.Value.Year;
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
            //    topLine1 = "Report Counter : " + cmbCounterUserName.Text;
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
            //    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            //    if (lineBelowLogo == "No Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += " ";
            //        }
            //        mainStr += "\n";
            //    }
            //    if (lineBelowLogo == "Single Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "-";
            //        }
            //        mainStr += "\n";
            //    }
            //    else if (lineBelowLogo == "Double Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "=";
            //        }
            //        mainStr += "\n";
            //    }
            //    //        break;
            //    //    }
            //    //}
            //    ds.Tables.Clear();
            //    //  mainStrSub = "";


            //    string tempStr = null;
            //    string tQtyHeading = "";
            //    tQtyHeading = "Particulars";
            //    //  mainStr += tQtyHeading;
            //    double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 8));
            //    for (int j = 0; j < chkCount; j++)
            //    {
            //        tQtyHeading += " ";
            //    }
            //    tQtyHeading += " Amount";
            //    // tQtyHeading += "U/Rate ";
            //    //tQtyHeading += "   Profit";
            //    mainStr += tQtyHeading;
            //    mainStr += "\n";

            //    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            //    if (lineBelowLogo == "No Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += " ";
            //        }
            //        mainStr += "\n";
            //    }
            //    if (lineBelowLogo == "Single Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "-";
            //        }
            //        mainStr += "\n";
            //    }
            //    else if (lineBelowLogo == "Double Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "=";
            //        }
            //        mainStr += "\n";
            //    }
            //    //        break;
            //    //    }
            //    //}
            //    for (int mn = 0; mn < dtGroupCommission.Rows.Count; mn++)
            //    //foreach (DataRow row in dgsales.Rows)
            //    {
            //        // object[] array = dgsales.Rows[mn].;
            //        bool isChk = false;

            //        if (isChk == false)
            //        {
            //            for (int i = 0; i < 2; i++)
            //            {
            //                tempStr = dtGroupCommission.Rows[mn][i].ToString();
            //                //  MessageBox.Show(tempStr.Length.ToString());
            //                findCenterPosition = (double.Parse(charPerLine) - 8);
            //                if (i == 0)
            //                {
            //                    if (tempStr.Length <= (int)findCenterPosition)
            //                    {
            //                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
            //                    }
            //                    else
            //                    {
            //                        string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
            //                        //    MessageBox.Show(temp);
            //                        int chkSpace = temp.LastIndexOf(" ");
            //                        int loc = (temp.Length - temp.LastIndexOf(" "));
            //                        //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
            //                        if (chkSpace != -1)
            //                        {
            //                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
            //                            //   MessageBox.Show(mainStr.ToString());
            //                            for (int j = 0; j < loc + 8; j++)
            //                            {
            //                                mainStr += " ";
            //                            }
            //                            mainStr += "\n";
            //                            string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
            //                            // mainStr += temp1;
            //                            if (temp1.Length <= (int)findCenterPosition)
            //                            {
            //                                mainStr += temp1.PadRight((int)findCenterPosition, ' ');
            //                            }
            //                        }
            //                        else
            //                        {
            //                            mainStr += temp.ToString();
            //                        }

            //                    }
            //                }

            //                if (i == 1)
            //                {
            //                    if (tempStr.Length < 8)
            //                    {
            //                        mainStr += tempStr.PadLeft(7, ' ');
            //                    }
            //                }
            //                if (i == 10)
            //                {
            //                    // mainStr += tempStr.PadRight(7, ' ');
            //                    if (tempStr.Length <= 7)
            //                    {
            //                        mainStr += tempStr.PadLeft(7, ' ');
            //                    }
            //                }
            //                if (i == 4)
            //                {
            //                    if (tempStr.Length <= 10)
            //                    {
            //                        mainStr += tempStr.PadLeft(10, ' ');
            //                    }
            //                }
            //                // tPrintText += tempStr;
            //            }
            //            mainStr += "\n";
            //        }
            //    }

            //    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            //    if (lineBelowLogo == "No Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += " ";
            //        }
            //        mainStr += "\n";
            //    }
            //    if (lineBelowLogo == "Single Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "-";
            //        }
            //        mainStr += "\n";
            //    }
            //    else if (lineBelowLogo == "Double Line")
            //    {
            //        for (int j = 0; j < double.Parse(charPerLine); j++)
            //        {
            //            mainStr += "=";
            //        }
            //        mainStr += "\n";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MyMessageBox.ShowBox(ex.Message, "Warning");
            //}
        }
        public void funCommission(string tUserNo, string tUserName, string tCtrNo, string tAction)
        {
            try
            {
                //Distict Groupname getting code
                DataTable dtCommissionGroup = new DataTable();
                dtCommissionGroup.Rows.Clear();
                SqlCommand cmdGroupName = new SqlCommand(@"Select distinct(Item_groupName) as Item_GroupName from 
(Select item_no,CONVERT(numeric(18,2),sum(Commi)) as Commission from salmas_table, stktrn_table where salmas_table.smas_no=stktrn_table.strn_no and smas_Cancel=0 and smas_rtno=0 and salmas_table.UserNo=@tUserNo  and Smas_billDate between @tFromDate and @tToDate group by item_no having SUM(Commi)>0) as Result1,
(Select Item_no,Item_name,Item_Grouptable.Item_groupname from Item_table,Item_Grouptable where Item_table.item_Groupno=Item_Grouptable.Item_groupno) as Result2
where Result1.Item_no=Result2.Item_no", con);
                cmdGroupName.Parameters.AddWithValue("@tUserNo", tUserNo);
                cmdGroupName.Parameters.AddWithValue("@tFromDate", Convert.ToDateTime(dpFromCommission.SelectedDate.Value.Year + "/" + dpFromCommission.SelectedDate.Value.Month + "/" + dpFromCommission.SelectedDate.Value.Day));
                cmdGroupName.Parameters.AddWithValue("@tToDate", Convert.ToDateTime(dpToCommission.SelectedDate.Value.Year + "/" + dpToCommission.SelectedDate.Value.Month + "/" + dpToCommission.SelectedDate.Value.Day));
                SqlDataAdapter adpGroupName = new SqlDataAdapter(cmdGroupName);
                adpGroupName.Fill(dtCommissionGroup);




                DataTable dtCommissionAll = new DataTable();
                dtCommissionAll.Rows.Clear();
                SqlCommand cmd = new SqlCommand(@"Select Result1.Item_no,commission,Item_name,Item_groupName from 
(Select item_no,CONVERT(numeric(18,2),sum(Commi)) as Commission from salmas_table, stktrn_table where salmas_table.smas_no=stktrn_table.strn_no and smas_Cancel=0 and smas_rtno=0 and salmas_table.UserNo=@tUserNo and Smas_billDate between @tFromDate and @tToDate group by item_no having SUM(Commi)>0) as Result1,
(Select Item_no,Item_name,Item_Grouptable.Item_groupname from Item_table,Item_Grouptable where Item_table.item_Groupno=Item_Grouptable.Item_groupno) as Result2
where Result1.Item_no=Result2.Item_no", con);
                cmd.Parameters.AddWithValue("@tUserNo", tUserNo);
                cmd.Parameters.AddWithValue("@tFromDate", Convert.ToDateTime(dpFromCommission.SelectedDate.Value.Year + "/" + dpFromCommission.SelectedDate.Value.Month + "/" + dpFromCommission.SelectedDate.Value.Day));
                cmd.Parameters.AddWithValue("@tToDate", Convert.ToDateTime(dpToCommission.SelectedDate.Value.Year + "/" + dpToCommission.SelectedDate.Value.Month + "/" + dpToCommission.SelectedDate.Value.Day));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtCommissionAll);
                string tItemGroupName = "";
                double tCommissionAmt = 0, tTotCommissionAmt = 0;
                if (dtCommissionAll.Rows.Count > 0)
                {

                    if (tAction == "GROUP")
                    {
                        for (int mn = 0; mn < dtCommissionGroup.Rows.Count; mn++)
                        {
                            if (mn == 0)
                            {
                                dtGroupCommission.Rows.Add(Convert.ToString(tUserName), "");
                            }
                            tItemGroupName = Convert.ToString(dtCommissionGroup.Rows[mn]["Item_groupName"].ToString());
                            tItemGroupName = (tItemGroupName.IndexOf("'") == -1) ? tItemGroupName : tItemGroupName.Replace("'", "''");

                            DataRow[] dtCommissionAllRow = dtCommissionAll.Select("Item_groupName='" + tItemGroupName + "'");
                            tCommissionAmt = 0;
                            for (int k = 0; k < dtCommissionAllRow.Length; k++)
                            {
                                tCommissionAmt += string.IsNullOrEmpty(Convert.ToString(dtCommissionAllRow[k]["Commission"])) ? 0.00 : Convert.ToDouble(Convert.ToString(dtCommissionAllRow[k]["Commission"]));

                            }
                            tTotCommissionAmt += tCommissionAmt;
                            dtGroupCommission.Rows.Add(Convert.ToString(dtCommissionGroup.Rows[mn]["Item_groupName"].ToString()), string.Format("{0:0.00}", tCommissionAmt));
                            if (mn == (dtCommissionGroup.Rows.Count - 1))
                            {
                                dtGroupCommission.Rows.Add("Total :", string.Format("{0:0.00}", tTotCommissionAmt));
                                dtGroupCommission.Rows.Add("", "");
                            }
                        }
                    }
                    if (tAction == "ITEM")
                    {
                        for (int mn = 0; mn < dtCommissionGroup.Rows.Count; mn++)
                        {
                            if (mn == 0)
                            {
                                dtGroupCommission.Rows.Add(Convert.ToString(tUserName), "");
                            }
                            tItemGroupName = Convert.ToString(dtCommissionGroup.Rows[mn]["Item_groupName"].ToString());
                            tItemGroupName = (tItemGroupName.IndexOf("'") == -1) ? tItemGroupName : tItemGroupName.Replace("'", "''");

                            DataRow[] dtCommissionAllRow = dtCommissionAll.Select("Item_groupName='" + tItemGroupName + "'");
                            tCommissionAmt = 0;
                            dtGroupCommission.Rows.Add("", "");
                            dtGroupCommission.Rows.Add(Convert.ToString(dtCommissionGroup.Rows[mn]["Item_groupName"].ToString()), "");
                            for (int k = 0; k < dtCommissionAllRow.Length; k++)
                            {
                                tCommissionAmt = string.IsNullOrEmpty(Convert.ToString(dtCommissionAllRow[k]["Commission"])) ? 0.00 : Convert.ToDouble(Convert.ToString(dtCommissionAllRow[k]["Commission"]));
                                tTotCommissionAmt += tCommissionAmt;
                                dtGroupCommission.Rows.Add(Convert.ToString(dtCommissionAllRow[k]["Item_name"].ToString()), string.Format("{0:0.00}", tCommissionAmt));
                            }

                            if (mn == (dtCommissionGroup.Rows.Count - 1))
                            {
                                dtGroupCommission.Rows.Add("Total :", string.Format("{0:0.00}", tTotCommissionAmt));
                                dtGroupCommission.Rows.Add("", "");
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
        public void funPrintHeaderPart()
        {
            mainStr = "";
            charPerLine = _Class.clsVariables.tempGCharactersPerLine;
            lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowLogo;

            if (lineBelowLogo == "No Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), ' ');
                mainStr += "\n";
                // break;
            }
            if (lineBelowLogo == "Single Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '-');
                mainStr += "\n";
                // break;
            }
            else if (lineBelowLogo == "Double Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '=');
                mainStr += "\n";
                // break;
            }
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
                // break;

            }

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
                //  break;
            }

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
                    //  break;
                }
            }

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
                    //  break;
                }
            }
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

            // print lint below logo

            lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            if (lineBelowLogo == "No Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), ' ');
                mainStr += "\n";
            }
            if (lineBelowLogo == "Single Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '-');
                mainStr += "\n";
            }
            else if (lineBelowLogo == "Double Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '=');
                mainStr += "\n";
            }

        }
    }
}
