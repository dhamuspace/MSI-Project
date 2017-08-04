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
    /// Interaction logic for frmVoid.xaml
    /// </summary>
    /// 
    public delegate void UCfrmVoidEvent();
    public partial class UCfrmVoid : UserControl
    {
        public event UCfrmVoidEvent UCfrmVoidEvent_CloseClick;
        public event UCfrmVoidEvent UCfrmVoidEvent_ResettleClick;
        public UCfrmVoid()
        {
            InitializeComponent();
            try
            {
                //con.Close();
                //if (con.State != ConnectionState.Open)
                //{
                //    con.Open();
                //}
                funConnectionStateCheck();
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("ItemName", typeof(string));
                    dt.Columns.Add("Qty", typeof(string));
                    dt.Columns.Add("Rate", typeof(string));
                    dt.Columns.Add("Amt", typeof(string));
                    dt.Columns.Add("Id", typeof(string));
                    dt.Columns.Add("Disc", typeof(string));
                    dt.Columns.Add("SDisc", typeof(string));
                    dt.Columns.Add("Other", typeof(string));
                    dt.Columns.Add("Serial", typeof(string));
                }
                if (dtRemove.Columns.Count == 0)
                {
                    dtRemove.Columns.Add("ItemName", typeof(string));
                    dtRemove.Columns.Add("Qty", typeof(string));
                    dtRemove.Columns.Add("Rate", typeof(string));
                    dtRemove.Columns.Add("Amt", typeof(string));
                    dtRemove.Columns.Add("Id", typeof(string));
                    dtRemove.Columns.Add("Disc", typeof(string));
                    dtRemove.Columns.Add("SDisc", typeof(string));
                    dtRemove.Columns.Add("Other", typeof(string));
                }
                if (dtFinal.Columns.Count == 0)
                {
                    dtFinal.Columns.Add("ItemName", typeof(string));
                    dtFinal.Columns.Add("Qty", typeof(string));
                    dtFinal.Columns.Add("Rate", typeof(string));
                    dtFinal.Columns.Add("Amt", typeof(string));
                    dtFinal.Columns.Add("Id", typeof(string));
                    dtFinal.Columns.Add("Disc", typeof(string));
                    dtFinal.Columns.Add("SDisc", typeof(string));
                    dtFinal.Columns.Add("Other", typeof(string));
                }
                if (dtPrint.Columns.Count == 0)
                {
                    dtPrint.Columns.Add("Describ", typeof(string));
                    dtPrint.Columns.Add("Property", typeof(string));
                }
                gridItems.DataSource = dt.DefaultView;


            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }


        public void funLoad()
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel,smas_no from salmas_table where Ctr_no=@tCounter and smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                //cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();

                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);

                DataTable dtReturnVal = new DataTable();

                dtReturnVal.Rows.Clear();
                SqlCommand cmdReturn = new SqlCommand("select smas_rtno,SUM(smas_NetAmount) as returnAmt from salmas_table where Ctr_no=@tCounter and smas_rtno<>0 and smas_rtno in (select smas_no from SalMas_table where  Ctr_no=@tCounter and smas_rtno=0 and smas_billno in (select smas_billno from salmas_table where Ctr_no=@tCounter and smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)))) group by smas_rtno", con);
                //  cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                cmdReturn.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                adpReturn.Fill(dtReturnVal);
                string tSmasNo = "";
                double tRetValue = 0.0, tNtAmt = 0.0;
                for (int ij = 0; ij < dtReturnVal.Rows.Count; ij++)
                {
                    tRetValue = 0.0;
                    tNtAmt = 0.0;
                    tSmasNo = "";

                    tSmasNo = dtReturnVal.Rows[ij]["smas_rtno"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dtReturnVal.Rows[ij]["returnAmt"])))
                    {
                        tRetValue = (dtReturnVal.Rows[ij]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[ij]["returnAmt"].ToString());
                    }
                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        if (Convert.ToString(dtReturnVal.Rows[ij]["smas_rtno"]) == Convert.ToString(dtNew.Rows[mn]["smas_no"]))
                        {
                            //tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                            tNtAmt = (string.IsNullOrEmpty(Convert.ToString(dtNew.Rows[mn]["NetAmount"]))) ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                            dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                        }
                    }
                }

                gridDisplay.DataSource = dtNew.DefaultView;
                gridDisplay.RowTemplate.Height = 35;
                gridDisplay.Columns["Cancel"].Visible = false;
                gridDisplay.Columns["smas_no"].Visible = false;
                for (int j = 0; j < gridDisplay.Rows.Count; j++)
                {
                    if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                    {
                        gridDisplay.Rows[j].ReadOnly = true;
                        // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                funCalculate("LOAD");
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }
        }

        //public void funLoad()
        //{
        //    try
        //    {

        //        DataTable dtNew = new DataTable();
        //        dtNew.Rows.Clear();
        //        SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
        //        //cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();
        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //        adp.Fill(dtNew);

        //        DataTable dtReturnVal = new DataTable();

        //        for (int mn = 0; mn < dtNew.Rows.Count; mn++)
        //        {
        //            dtReturnVal.Rows.Clear();
        //            SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_rtno<>0", con);
        //            cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
        //            SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
        //            adpReturn.Fill(dtReturnVal);
        //            double tRetValue = 0.0, tNtAmt = 0.0;
        //            if (dtReturnVal.Rows.Count > 0)
        //            {
        //              //  if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
        //                if (!string.IsNullOrEmpty(Convert.ToString(dtReturnVal.Rows[0]["returnAmt"])))
        //                {
        //                    tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
        //                }
        //            }
        //            //tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
        //            tNtAmt = (string.IsNullOrEmpty(Convert.ToString(dtNew.Rows[mn]["NetAmount"]))) ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
        //            dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
        //        }

        //        gridDisplay.DataSource = dtNew.DefaultView;

        //        gridDisplay.RowTemplate.Height = 35;
        //        gridDisplay.Columns["Cancel"].Visible = false;
        //        for (int j = 0; j < gridDisplay.Rows.Count; j++)
        //        {
        //            if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
        //            {
        //                gridDisplay.Rows[j].ReadOnly = true;
        //                // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
        //                gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
        //            }
        //        }
        //        funCalculate();
        //    }
        //    catch (Exception ex)
        //    {
        //        MyMessageBox1.ShowBox(ex.Message, "Warning");
        //    }
        //}
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        public DataTable dt = new DataTable();
        public DateTime currentDate;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UCfrmPayment1.UCVoidEventdelegate_click += new UCVoidEventdelegate(UCVoidEventdelegate_click_click);
            funVoidLoad();
        }

        private void UCVoidEventdelegate_click_click()
        {
            windowsFormsHost1.Visibility = Visibility.Visible;
            windowsFormsHost2.Visibility = Visibility.Visible;
        }


        public void funVoidLoad()
        {
            try
            {
                lblBillNo.Content = "0001";
                lblDate.Content = "DD/MM/YYYY";
                lblDiscount.Content = "0.00";
                lblTotAmt.Content = "0.00";
                lblTotQty.Content = "0.00";
                lblTaxAmt.Content = "0.00";
                lblRefund.Content = "0.00";
                lblNetAmt.Content = "0.00";
                txtBillNo.Text = "";
                txtEnterValue.Text = "";
                dt.Rows.Clear();
                gridItems.DataSource = dt.DefaultView;
                if (_Class.clsVariables.tAllowOffer == true)
                {
                    funOfferLoad();
                }
                dtpFrom.SelectedDate = SalesProject._Class.clsVariables.tEndOfDayDate;
                dtpTo.SelectedDate = SalesProject._Class.clsVariables.tEndOfDayDate;
                funLoad();


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

                DataTable dtPrinter = new DataTable();
                dtPrinter.Rows.Clear();
                SqlCommand cmdPrinter = new SqlCommand("Select * from ReceiptPrintSettings_table where Counter=@tCounter", con);
                cmdPrinter.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpPrinter = new SqlDataAdapter(cmdPrinter);
                adpPrinter.Fill(dtPrinter);
                if (dtPrinter.Rows.Count > 0)
                {
                    int tCount = 0;
                    for (int mn = 0; mn < dtPrint.Rows.Count; mn++)
                    {
                        if (dtPrint.Rows[mn][0].ToString() == "Enable This Device*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Enable_This_Device"].ToString();
                            tCount++;
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Printer Name*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Printer_Name"].ToString();
                            tCount++;
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Printer Type*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Printer_Type"].ToString();
                            tCount++;
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Print Copies*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Print_Copies"].ToString();
                            tCount++;
                        }
                        if (dtPrint.Rows[mn][0].ToString() == "Characters Per Line*")
                        {
                            dtPrint.Rows[mn][1] = dtPrinter.Rows[0]["Characters_Per_Line"].ToString();
                            tCount++;
                        }
                        // Loop Exist
                        if (tCount == 10)
                        {
                            break;
                        }
                    }
                    btnLoad.Focus();
                }

                SqlCommand cmd13 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@tActionType", "RPTSET");
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd13);
                adp1.Fill(dtPrint);
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
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                adp2.Fill(dtPrint);

                _Class.clsVariables.tVoidActionType = "BILLNO";

                lblDate.Content = DateTime.Now.ToShortDateString();
                txtBillNo.Select(txtBillNo.Text.Length, 0);
                _Class.clsVariables.LoadPreviousBill = "LoadNot";

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                // SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,smas_billDate as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_name='Cash Sales' and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel, smas_no from salmas_table where Ctr_no=@tCounter and smas_rtno=0 and smas_name='Cash Sales' and smas_billdate between convert(date,@tFromDate,108) AND convert(date,@tToDate,108) order by smas_billno DESC", con);
                cmd.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value);
                cmd.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value);
                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                //cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                DataTable dtReturnVal = new DataTable();

                dtReturnVal.Rows.Clear();
                SqlCommand cmdReturn = new SqlCommand("select smas_rtno,SUM(smas_NetAmount) as returnAmt from salmas_table where ctr_no=@tCounter and smas_rtno<>0 and smas_name='Cash Sales' and smas_rtno in (select smas_no from SalMas_table where ctr_no=@tCounter and smas_rtno=0 and smas_billno in (select smas_billno from salmas_table where ctr_no=@tCounter and smas_rtno=0 and smas_name='Cash Sales' and smas_billdate between @tFromDate and @tToDate)) group by smas_rtno", con);
                cmdReturn.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value);
                cmdReturn.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value);
                cmdReturn.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                adpReturn.Fill(dtReturnVal);
                string tSmasNo = "";
                double tRetValue = 0.0, tNtAmt = 0.0;
                for (int ij = 0; ij < dtReturnVal.Rows.Count; ij++)
                {
                    tRetValue = 0.0;
                    tNtAmt = 0.0;
                    tSmasNo = "";

                    tSmasNo = dtReturnVal.Rows[ij]["smas_rtno"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dtReturnVal.Rows[ij]["returnAmt"])))
                    {
                        tRetValue = (dtReturnVal.Rows[ij]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[ij]["returnAmt"].ToString());
                    }
                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        if (Convert.ToString(dtReturnVal.Rows[ij]["smas_rtno"]) == Convert.ToString(dtNew.Rows[mn]["smas_no"]))
                        {
                            //tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                            tNtAmt = (string.IsNullOrEmpty(Convert.ToString(dtNew.Rows[mn]["NetAmount"]))) ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                            dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                        }
                    }
                }

                gridDisplay.DataSource = dtNew.DefaultView;
                gridDisplay.RowTemplate.Height = 35;
                gridDisplay.Columns["Cancel"].Visible = false;
                gridDisplay.Columns["smas_no"].Visible = false;


                //for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                //{
                //    dtReturnVal.Rows.Clear();
                //    SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_rtno<>0 and smas_name='Cash Sales'", con);
                //    cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                //    SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                //    adpReturn.Fill(dtReturnVal);
                //    double tRetValue = 0.0, tNtAmt = 0.0;
                //    if (dtReturnVal.Rows.Count > 0)
                //    {
                //        if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                //        {
                //            tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                //        }
                //    }
                //    tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                //    dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                //}

                //gridDisplay.DataSource = dtNew.DefaultView;
                //gridDisplay.Columns["Cancel"].Visible = false;
                for (int j = 0; j < gridDisplay.Rows.Count; j++)
                {
                    if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                    {
                        gridDisplay.Rows[j].ReadOnly = true;
                        // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                funCalculate("CASH");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void btnNETS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                // SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,smas_billDate as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_name='NETS' and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel, smas_no from salmas_table where ctr_no=@tCounter and smas_rtno=0 and smas_name='NETS' AND smas_billdate between convert(date,@tFromDate,108) AND convert(date, @tToDate,108) order by smas_billno DESC", con);
                cmd.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value);
                cmd.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value);
                cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                //cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);

                DataTable dtReturnVal = new DataTable();

                dtReturnVal.Rows.Clear();
                SqlCommand cmdReturn = new SqlCommand("select smas_rtno,SUM(smas_NetAmount) as returnAmt from salmas_table where ctr_no=@tCounter and smas_rtno<>0 and smas_name='NETS' and smas_rtno in (select smas_no from SalMas_table where ctr_no=@tCounter and smas_rtno=0 and smas_billno in (select smas_billno from salmas_table where ctr_no=@tCounter and smas_rtno=0 and smas_name='NETS' and smas_billdate between @tFromDate and @tToDate)) group by smas_rtno", con);
                cmdReturn.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value);
                cmdReturn.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value);
                cmdReturn.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                adpReturn.Fill(dtReturnVal);
                string tSmasNo = "";
                double tRetValue = 0.0, tNtAmt = 0.0;
                for (int ij = 0; ij < dtReturnVal.Rows.Count; ij++)
                {
                    tRetValue = 0.0;
                    tNtAmt = 0.0;
                    tSmasNo = "";

                    tSmasNo = dtReturnVal.Rows[ij]["smas_rtno"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(dtReturnVal.Rows[ij]["returnAmt"])))
                    {
                        tRetValue = (dtReturnVal.Rows[ij]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[ij]["returnAmt"].ToString());
                    }
                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        if (Convert.ToString(dtReturnVal.Rows[ij]["smas_rtno"]) == Convert.ToString(dtNew.Rows[mn]["smas_no"]))
                        {
                            //tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                            tNtAmt = (string.IsNullOrEmpty(Convert.ToString(dtNew.Rows[mn]["NetAmount"]))) ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                            dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                        }
                    }
                }

                gridDisplay.DataSource = dtNew.DefaultView;
                gridDisplay.RowTemplate.Height = 35;
                gridDisplay.Columns["Cancel"].Visible = false;
                gridDisplay.Columns["smas_no"].Visible = false;


                //for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                //{
                //    dtReturnVal.Rows.Clear();
                //    SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_name='NETS' and smas_rtno<>0", con);
                //    cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                //    SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                //    adpReturn.Fill(dtReturnVal);
                //    double tRetValue = 0.0, tNtAmt = 0.0;
                //    if (dtReturnVal.Rows.Count > 0)
                //    {
                //        if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                //        {
                //            tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                //        }
                //    }
                //    tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                //    dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                //}
                //gridDisplay.DataSource = dtNew.DefaultView;
                //gridDisplay.Columns["Cancel"].Visible = false;
                for (int j = 0; j < gridDisplay.Rows.Count; j++)
                {
                    if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                    {
                        gridDisplay.Rows[j].ReadOnly = true;
                        // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                funCalculate("NETS");

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        public void funCalculate(string btnName)
        {
            try
            {
                txtTotal.Text = "0.00";
                double tCalculate = 0;
                for (int j = 0; j < gridDisplay.Rows.Count; j++)
                {
                    if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() != "True")
                    {
                        tCalculate += ((gridDisplay.Rows[j].Cells["NetAmount"].Value.ToString() == "") ? 0 : double.Parse(gridDisplay.Rows[j].Cells["NetAmount"].Value.ToString()));
                    }
                }
                DataTable dtVoid = new DataTable();
                dtVoid.Rows.Clear();
                SqlCommand cmdVoid = new SqlCommand("select party_no, smas_rtno,convert(numeric(18,2),SUM(smas_NetAmount)) as Amount from salmas_table where  Smas_Cancel<>1 and smas_rtno<>0 and ctr_no=@tCounter and Smas_billdate between @tFromDate and @tToDate group by smas_rtno, party_no", con);
                cmdVoid.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                cmdVoid.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day);
                cmdVoid.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day);
                SqlDataAdapter adpVoid = new SqlDataAdapter(cmdVoid);
                adpVoid.Fill(dtVoid);
                double tCashReturnValue = 0.00;
                double tNETSReturnValue = 0.00;
                for (int i = 0; i < dtVoid.Rows.Count; i++)
                {
                    if (dtVoid.Rows[i]["party_no"].ToString() == "2")
                    {
                        tCashReturnValue += (dtVoid.Rows[i]["Amount"].ToString().Trim() == "") ? 0 : double.Parse(dtVoid.Rows[i]["Amount"].ToString());
                    }
                    else if (dtVoid.Rows[i]["party_no"].ToString() == "14")
                    {
                        tNETSReturnValue += (dtVoid.Rows[i]["Amount"].ToString().Trim() == "") ? 0 : double.Parse(dtVoid.Rows[i]["Amount"].ToString());
                    }
                }

                //Her Coding Changed Here by Anbu:
                int NetSCount = 0, CashSCount = 0;

                SqlCommand adp31 = new SqlCommand("select Distinct (Strn_no) from stktrn_table,salmas_table where stktrn_table.strn_type=1 and  stktrn_table.strn_rtno<>1 and salmas_table.ctr_no=@tCounter and stktrn_table.strn_no=salmas_table.smas_no and stktrn_table.strn_date between @tFromDate and @tToDate", con);
                adp31.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                adp31.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day);
                adp31.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day);
                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlDataAdapter adp11 = new SqlDataAdapter(adp31);
                adp11.Fill(dtNew1);
                //  lblCNETSSalesTotal.Content = "0.00";
                if (dtNew1.Rows.Count > 0)
                {
                    double CashAmt = 0.00, NetsAmt = 0.00;
                    // for (int i = 0; i < dtNew1.Rows.Count; i++)
                    {
                        //SqlCommand cmd = new SqlCommand(@"select (Case When SalRecv_table.SalRecv_Led=14  Then Convert(Numeric(18,2),SUM(SalRecv_table.SalRecv_Amt))  End) as NETs, (Case When SalRecv_table.SalRecv_Led=5 Then Convert(Numeric(18,2),SUM(SalRecv_table.SalRecv_Amt))  End) As Cash from salmas_table,SalRecv_table  where SalMas_Table.Smas_cancel='0' and  SalRecv_table.SalRecv_Salno='" + dtNew1.Rows[i]["Strn_no"].ToString() + "' and salmas_table.smas_rtno<>1 and salmas_table.smas_billno=SalRecv_table.salRecv_Salno group by SalRecv_table.SalRecv_Amt,SalRecv_table.SalRecv_Led ", con);
                        SqlCommand cmd = new SqlCommand(@"select (Case When SalRecv_table.SalRecv_Led=14  Then Convert(Numeric(18,2),SUM(SalRecv_table.SalRecv_Amt))  End) as NETs, (Case When SalRecv_table.SalRecv_Led=5 Then Convert(Numeric(18,2),SUM(SalRecv_table.SalRecv_Amt))  End) As Cash from salmas_table,SalRecv_table  where SalMas_Table.Smas_cancel='0' and SalMas_Table.smas_rtno=0 and salmas_table.smas_billno=SalRecv_table.salRecv_Salno and salmas_table.ctr_no=@tCounter  and salmas_table.smas_rtno<>1  and  smas_billdate  between @tFromDate and @tToDate group by SalRecv_table.SalRecv_Amt,SalRecv_table.SalRecv_Led", con);
                        cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                        cmd.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day);
                        cmd.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day);
                        SqlDataAdapter aadp1 = new SqlDataAdapter(cmd);
                        DataTable SelNetsAndCash = new DataTable();
                        SelNetsAndCash.Rows.Clear();
                        aadp1.Fill(SelNetsAndCash);
                        if (SelNetsAndCash.Rows.Count > 0)
                        {
                            for (int j = 0; j < SelNetsAndCash.Rows.Count; j++)
                            {
                                if (SelNetsAndCash.Rows[j]["Cash"].ToString().Trim() != "" && SelNetsAndCash.Rows[j]["Cash"].ToString().Trim() != null)
                                {
                                    CashAmt += Convert.ToDouble(SelNetsAndCash.Rows[j]["Cash"].ToString());
                                    CashSCount = ++CashSCount;
                                }
                                //  if (SelNetsAndCash.Rows.Count > 1)
                                {

                                    if (SelNetsAndCash.Rows[j]["NETs"].ToString().Trim() != "" && SelNetsAndCash.Rows[j]["NETs"].ToString().Trim() != null)
                                    {
                                        NetsAmt += Convert.ToDouble(SelNetsAndCash.Rows[j]["NETs"].ToString());
                                        NetSCount = ++NetSCount;
                                    }
                                }
                            }
                        }
                    }
                    // double TotalSalesAmt = 0.00;
                    if (btnName.ToString().ToUpper() == "LOAD")
                    {
                        //txtTotal.Text = string.Format("{0:0.00}", tCalculate);
                        if (_Class.clsVariables.tViewCash == true)
                        {
                            txtTotal.Text = string.Format("{0:0.00}", tCalculate);
                        }
                        else
                        {
                            txtTotal.Text = "0.00";
                        }
                    }
                    if (btnName.ToString().ToUpper() == "CASH")
                    {
                        //txtTotal.Text =string.Format("{0:0.00}",(CashAmt - tCashReturnValue));
                        if (_Class.clsVariables.tViewCash == true)
                        {
                            txtTotal.Text = string.Format("{0:0.00}", (CashAmt - tCashReturnValue));
                        }
                        else
                        {
                            txtTotal.Text = "0.00";
                        }
                    }
                    if (btnName.ToString().ToUpper() == "NETS")
                    {
                        txtTotal.Text = string.Format("{0:0.00}", (NetsAmt - tNETSReturnValue));
                    }
                    DataTable dtCreditcard1 = new DataTable();
                    if (btnName.ToString().ToUpper() == "CREDITCARD")
                    {
                        DataTable dtLedgerNoCredit = new DataTable();
                        dtLedgerNoCredit.Rows.Clear();
                        DataTable dtCredit = new DataTable();


                        SqlCommand cmdLedgerCredit = new SqlCommand("Select * from Ledger_table where Ledger_groupno=5 and Ledger_no<>14", con);
                        SqlDataAdapter adpLedgerCredit = new SqlDataAdapter(cmdLedgerCredit);
                        adpLedgerCredit.Fill(dtLedgerNoCredit);
                        double totamountCredit = 0.00;
                        for (int mn = 0; mn < dtLedgerNoCredit.Rows.Count; mn++)
                        {
                            SqlCommand cmdHAC1 = new SqlCommand(@"select Convert(Numeric(18,2),SUM(SalRecv_table.SalRecv_Amt)) as Amount  from salmas_table,SalRecv_table  where SalRecv_table.SalRecv_Led=@tPartyNo and salmas_table.smas_billno=SalRecv_table.SalRecv_Salno and salmas_table.Smas_rtno=0 and salmas_table.ctr_no=@tCounter and smas_Cancel<>1 and salmas_table.smas_rtno<>1 and salmas_table.smas_billno=SalRecv_table.salRecv_Salno and smas_billdate between @tFromDate and @tToDate  group by SalRecv_table.SalRecv_Amt,SalRecv_table.SalRecv_Led", con);
                            cmdHAC1.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            cmdHAC1.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day);
                            cmdHAC1.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day);
                            cmdHAC1.Parameters.AddWithValue("@tPartyNo", dtLedgerNoCredit.Rows[mn]["Ledger_no"].ToString());
                            SqlDataAdapter aadpHAC1 = new SqlDataAdapter(cmdHAC1);
                            dtCreditcard1.Rows.Clear();
                            // dt_griddiaplay1.Rows.Clear();
                            aadpHAC1.Fill(dtCreditcard1);
                            if (dtCreditcard1.Rows.Count > 0)
                            {
                                //gridItems.DataSource = dtCreditcard1.DefaultView;
                                for (int i = 0; i < dtCreditcard1.Rows.Count; i++)
                                {
                                    totamountCredit += Convert.ToDouble(dtCreditcard1.Rows[i]["Amount"].ToString());
                                }
                            }
                        }
                        txtTotal.Text = string.Format("{0:0.00}", totamountCredit);

                    }
                    if (btnName.ToString().ToUpper() == "HOUSEAC")
                    {

                        DataTable dtLedgerNo = new DataTable();
                        dtLedgerNo.Rows.Clear();
                        DataTable dtHAC = new DataTable();


                        SqlCommand cmdLedger = new SqlCommand("Select * from Ledger_table where Ledger_groupno=32 and Ledger_no<>2", con);
                        SqlDataAdapter adpLedger = new SqlDataAdapter(cmdLedger);
                        adpLedger.Fill(dtLedgerNo);
                        double totamountHAC = 0.00;
                        for (int mn = 0; mn < dtLedgerNo.Rows.Count; mn++)
                        {
                            dtHAC.Rows.Clear();
                            SqlCommand cmdHAC1 = new SqlCommand(@"select Convert(Numeric(18,2),SUM(SalRecv_table.SalRecv_Amt)) as Amount  from salmas_table,SalRecv_table  where SalRecv_table.SalRecv_Led=@tPartyNo and salmas_table.smas_billno=SalRecv_table.SalRecv_Salno and salmas_table.Smas_rtno=0 and salmas_table.ctr_no=@tCounter and smas_Cancel<>1  and salmas_table.smas_rtno<>1 and salmas_table.smas_billno=SalRecv_table.salRecv_Salno and smas_billdate between @tFromDate and @tToDate group by SalRecv_table.SalRecv_Amt,SalRecv_table.SalRecv_Led", con);
                            cmdHAC1.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            cmdHAC1.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day);
                            cmdHAC1.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day);

                            cmdHAC1.Parameters.AddWithValue("@tPartyNo", dtLedgerNo.Rows[mn]["Ledger_no"].ToString());
                            SqlDataAdapter aadpHAC1 = new SqlDataAdapter(cmdHAC1);
                            dtCreditcard1.Rows.Clear();
                            // dt_griddiaplay1.Rows.Clear();
                            aadpHAC1.Fill(dtCreditcard1);
                            if (dtCreditcard1.Rows.Count > 0)
                            {
                                //gridItems.DataSource = dtCreditcard1.DefaultView;
                                for (int i = 0; i < dtCreditcard1.Rows.Count; i++)
                                {
                                    totamountHAC += Convert.ToDouble(dtCreditcard1.Rows[i]["Amount"].ToString());
                                }
                            }
                        }
                        txtTotal.Text = string.Format("{0:0.00}", totamountHAC);

                    }


                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        public void funVoid()
        {
            try
            {
                if (tBillNo != "")
                {
                    DataTable dtNewChkingNew = new DataTable();
                    dtNewChkingNew.Rows.Clear();
                    SqlCommand cmdChkExisting = new SqlCommand("Select * from salMas_table where smas_rtno=@tBillNo", con);
                    cmdChkExisting.Parameters.AddWithValue("@tBillNo", tBillNo);
                    SqlDataAdapter adpChkExist = new SqlDataAdapter(cmdChkExisting);
                    adpChkExist.Fill(dtNewChkingNew);
                    if (dtNewChkingNew.Rows.Count > 0)
                    {
                        MyMessageBox.ShowBox("Sales has return", "Warning");
                    }
                    else
                    {
                        string result = MyMessageBox1.ShowBox("Are you sure to cancel this billno", "Warning");
                        if (result == "1")
                        {
                            SqlCommand cmd = new SqlCommand("sp_Void", con);
                            cmd.Parameters.AddWithValue("@tBillNo", tBillNo);
                            cmd.Parameters.AddWithValue("@tReason", txtReason.Text.Trim());
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();

                            DataTable dtNew = new DataTable();
                            dtNew.Rows.Clear();
                            SqlCommand cmd1 = new SqlCommand("select smas_billno as BillNo,smas_billDate as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2), smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                            //cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();
                            SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                            adp.Fill(dtNew);
                            gridDisplay.DataSource = dtNew.DefaultView;
                            gridDisplay.Columns["Cancel"].Visible = false;
                            for (int j = 0; j < gridDisplay.Rows.Count; j++)
                            {
                                if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                                {
                                    gridDisplay.Rows[j].ReadOnly = true;
                                    // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                                    gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            gridItems.DataSource = null;
                            txtBillNo.Text = "";
                            txtEnterValue.Text = "";
                            txtReason.Text = "";
                            txtTotal.Text = "";
                            funCalculate("LOAD");

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void btnVoid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Class.clsVariables.UserType != "1")
                {
                    funVoid();
                }
                else
                {
                    try
                    {
                        if (_Class.clsVariables.tAllowVoid == true)
                        {
                            funVoid();
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please, get user rights to Cancel this Bill!!", "Warning");
                        }
                        //frmKeyBoard frm = new frmKeyBoard();
                        //_Class.clsVariables.tVoidActionType = "PASSWORD";
                        //if (_Class.clsVariables.tVoidActionType == "PASSWORD")
                        //{
                        //    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEventPassword);
                        //    frm.ShowDialog();
                        //    txtEnterValue.Focus();
                        //    txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                        //}
                    }
                    catch (Exception ex)
                    {
                        MyMessageBox.ShowBox(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }


        public void CloseEventPassword(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from User_table where User_pass=@tPassword and User_type=0", con);
                cmd.Parameters.AddWithValue("@tPassword", _Class.clsVariables.tVoidValue);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    funVoid();
                }
                else
                {
                    MyMessageBox.ShowBox("Invalid Password..Please get user rights to open Void Form!!", "Warning");
                }

                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void CloseEventPassword1(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from User_table where User_pass=@tPassword and User_type=0", con);
                cmd.Parameters.AddWithValue("@tPassword", _Class.clsVariables.tVoidValue);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    funReturn();
                    for (int j = 0; j < gridDisplay.Rows.Count; j++)
                    {
                        if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                        {
                            gridDisplay.Rows[j].ReadOnly = true;
                            // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                            gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    funCalculate("LOAD");
                }
                else
                {
                    MyMessageBox.ShowBox("Invalid Password..Please get user rights to open Void Form!!", "Warning");
                }

                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }


        private void windowsFormsHost1_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
        string tBillNo = "";
        string isCancel = "";
        string isReturn = "";
        private void gridDisplay_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                btnGo.IsEnabled = true;
                btnRemove.IsEnabled = true;
                btnSave.IsEnabled = true;
                txtBillNo.IsEnabled = true;
                txtEnterValue.IsEnabled = true;
                isCancel = "";
                isReturn = "";
                if (e.RowIndex != -1)
                {
                    if (gridDisplay.Rows[e.RowIndex].Cells["Cancel"].Value.ToString().Trim() == "False")
                    {
                        tBillNo = gridDisplay.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                        tPrintDate = DateTime.Parse(gridDisplay.Rows[e.RowIndex].Cells["Date"].Value.ToString());
                        tPrintTime = gridDisplay.Rows[e.RowIndex].Cells["Time"].Value.ToString();
                        DataTable dtNewChkingNew = new DataTable();
                        dtNewChkingNew.Rows.Clear();
                        SqlCommand cmdChkExisting = new SqlCommand("Select * from salMas_table where smas_rtno= (select smas_no from salMas_table where smas_billno=@tBillNo and smas_rtno=0)", con);
                        cmdChkExisting.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adpChkExist = new SqlDataAdapter(cmdChkExisting);
                        adpChkExist.Fill(dtNewChkingNew);
                        if (dtNewChkingNew.Rows.Count > 0)
                        {
                            isReturn = "Return";
                            MyMessageBox.ShowBox("Sales has return", "Warning");
                        }

                        txtBillNo.Text = tBillNo.ToString();
                        funLoadOldBill(tBillNo);
                        funDisplayAmount(dt);
                        funRoundCalculate();
                        txtEnterValue.Focus();

                    }
                    else
                    {
                        btnGo.IsEnabled = false;
                        btnRemove.IsEnabled = false;
                        btnSave.IsEnabled = false;
                        txtBillNo.IsEnabled = false;
                        txtEnterValue.IsEnabled = false;
                        isCancel = "Cancel";
                        tBillNo = gridDisplay.Rows[e.RowIndex].Cells["BillNo"].Value.ToString();
                        tPrintDate = DateTime.Parse(gridDisplay.Rows[e.RowIndex].Cells["Date"].Value.ToString());
                        tPrintTime = gridDisplay.Rows[e.RowIndex].Cells["Time"].Value.ToString();
                        DataTable dtNewChkingNew = new DataTable();
                        dtNewChkingNew.Rows.Clear();
                        SqlCommand cmdChkExisting = new SqlCommand("Select * from salMas_table where smas_rtno= (select smas_no from salMas_table where smas_billno=@tBillNo and smas_rtno=0)", con);
                        cmdChkExisting.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adpChkExist = new SqlDataAdapter(cmdChkExisting);
                        adpChkExist.Fill(dtNewChkingNew);
                        if (dtNewChkingNew.Rows.Count > 0)
                        {
                            //  MyMessageBox.ShowBox("Sales has return", "Warning");
                        }

                        txtBillNo.Text = tBillNo.ToString();
                        funLoadOldBill(tBillNo);
                        funDisplayAmount(dt);
                        funRoundCalculate();
                        txtEnterValue.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }
        DataTable dtChkGrid = new DataTable();
        DataTable dtReturn = new DataTable();
        DateTime tPrintDate;
        string tPrintTime;
        public void funLoadOldBill(string tBillNo)
        {
            try
            {
                DataTable dtChk = new DataTable();
                dtChk.Rows.Clear();
                SqlCommand chkBill = new SqlCommand("select * from salmas_table where smas_billno=@tBillNo and smas_rtno=0", con);
                chkBill.Parameters.AddWithValue("@tBillNo", tBillNo);
                SqlDataAdapter adpChk = new SqlDataAdapter(chkBill);
                adpChk.Fill(dtChk);
                if (dtChk.Rows.Count > 0)
                {
                    DataTable dtNew = new DataTable();
                    dtChkGrid.Rows.Clear();
                    dtRemove.Rows.Clear();
                    dtNew.Rows.Clear();


                    SqlCommand cmd = new SqlCommand("Select smas_billno,CONVERT(date,smas_billdate,103) as Date,smas_Gross,smas_NetAmount,DiscountDetail_table.Amount from salmas_table, DiscountDetail_table where DiscountDetail_table.Bill_no=salmas_table.smas_billno and salmas_table.smas_billno=@tBillNo and salmas_table.smas_rtno=0", con);
                    cmd.Parameters.AddWithValue("@tBillNo", tBillNo);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew);
                    if (dtNew.Rows.Count > 0)
                    {
                        lblBillNo.Content = dtNew.Rows[0]["smas_billno"].ToString();
                        //lblDiscount.Content = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Amount"].ToString()));
                        lblNetAmt.Content = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["smas_netAmount"].ToString()));
                        //lblTotQty.Content=dtNew.Rows[0][].ToString();
                        //lblTaxAmt.Content=dtNew.Rows[0][].ToString();
                        lblTotAmt.Content = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["smas_Gross"].ToString()));
                        lblDate.Content = dtNew.Rows[0]["Date"].ToString();

                        dt.Rows.Clear();

                        dtReturn.Rows.Clear();
                        //SqlCommand cmdReturn = new SqlCommand("select Item_table.Item_name as ItemName,(stktrn_table.rnt_qty)  as Qty,convert(numeric(18,2),stktrn_table.Rate) as Rate,convert(numeric(18,2),((stktrn_table.rnt_qty)*stktrn_table.Rate)) as Amt, stktrn_table.strn_sno as Id from stktrn_table,Item_table where stktrn_table.item_no=Item_table.Item_no and stktrn_table.strn_no=(select smas_no from salmas_table where smas_billno=@tBillNo and Smas_rtno=0) and stktrn_table.rnt_qty>0", con);
                        SqlCommand cmdReturn = new SqlCommand("select Item_table.Item_name as ItemName, convert(numeric(18,2),(tot_amt)) as Amt,CONVERT(numeric(18,2),(Disc_Amt)),spl_discamt,Othdisc_Amt as Disc from stktrn_table,Item_table where stktrn_table.item_no=Item_table.Item_no and stktrn_table.strn_no in(Select smas_no from SalMas_table where smas_rtno= (select smas_no from salmas_table where smas_billno=@tBillNo and Smas_rtno=0 and smas_Cancel=0))", con);
                        cmdReturn.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                        adpReturn.Fill(dtReturn);
                        double tReturnTotValue = 0, tReturnDiscAmt = 0;
                        for (int mn = 0; mn < dtReturn.Rows.Count; mn++)
                        {
                            tReturnTotValue += Convert.ToDouble(dtReturn.Rows[mn]["Amt"].ToString());
                            tReturnDiscAmt += Convert.ToDouble(dtReturn.Rows[mn]["Disc"].ToString());
                        }
                        lblDiscount.Content = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Amount"].ToString()) - tReturnDiscAmt);
                        lblNetAmt.Content = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["smas_netAmount"].ToString()) - tReturnTotValue);
                        lblTotAmt.Content = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["smas_Gross"].ToString()) - tReturnTotValue);
                        string ss = "'-'";
                        SqlCommand cmd1 = new SqlCommand("select (case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + ss + "+stktrn_table.Serial_No end) as ItemName,(stktrn_table.nt_qty-stktrn_table.rnt_qty)  as Qty,convert(numeric(18,2),stktrn_table.Rate) as Rate,convert(numeric(18,2),((stktrn_table.nt_qty-stktrn_table.rnt_qty)*stktrn_table.Rate)) as Amt, stktrn_table.strn_sno as Id, stktrn_table.Disc_Amt as Disc,stktrn_table.spl_DiscAmt as SDisc,convert(numeric(18,2),((stktrn_table.OthDisc_Amt)/(stktrn_table.nt_qty))*(stktrn_table.nt_qty-stktrn_table.rnt_qty)) as Other,stktrn_table.Serial_No as Serial from stktrn_table,Item_table where stktrn_table.item_no=Item_table.Item_no and stktrn_table.strn_no=(select smas_no from salmas_table where smas_billno=@tBillNo and Smas_rtno=0) and stktrn_table.nt_qty<>stktrn_table.rnt_qty", con);
                        cmd1.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                        adp1.Fill(dt);
                        adp1.Fill(dtChkGrid);

                        DataTable dtReturnTax = new DataTable();
                        dtReturnTax.Rows.Clear();
                        SqlCommand cmdReturnTax = new SqlCommand("select Item_table.Item_name as ItemName,convert(numeric(18,2),stktrn_table.Rate) as Rate,convert(numeric(18,2),(stktrn_table.Amount )) as Amt from stktrn_table,Item_table where stktrn_table.Item_no=Item_table.Item_no and strn_rtno<>0 and strn_no in( Select smas_no from salMas_table where smas_rtno= (select smas_no from salMas_table where smas_billno=@tBillNo and smas_rtno=0))", con);
                        cmdReturnTax.Parameters.AddWithValue("@tBillNo", tBillNo);
                        SqlDataAdapter adpReturnTax = new SqlDataAdapter(cmdReturnTax);
                        adpReturnTax.Fill(dtReturnTax);
                        //for (int mn = 0; mn < dtReturnTax.Rows.Count; mn++)
                        //{
                        //     string tItemName = Convert.ToString(dtReturnTax.Rows[mn]["ItemName"]);
                        //     tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");

                        //     DataRow[] dtRow = dt.Select("ItemName='" + tItemName + "' AND Rate='" + Convert.ToString(dtReturnTax.Rows[mn]["Rate"]) + "'");
                        //     if (dtRow.Length > 0)
                        //     {
                        //         for (int ij = 0; ij < dt.Rows.Count; ij++)
                        //         {
                        //             if (tItemName == Convert.ToString(dt.Rows[ij]["ItemName"]) && Convert.ToString(dtReturnTax.Rows[mn]["Rate"]) == Convert.ToString(dt.Rows[ij]["Rate"]))
                        //             {
                        //                dt.Rows[ij]["Amt"] = Convert.ToDouble(dt.Rows[ij]["Amt"]) - Convert.ToDouble(dtReturnTax.Rows[mn]["Amt"]);
                        //             }
                        //         }
                        //     }
                        //}

                        gridItems.DataSource = dt;
                        gridItems.Columns[0].ReadOnly = true;
                        gridItems.Columns["Itemname"].Width = 230;

                        gridItems.Columns[1].Width = 40;
                        gridItems.Columns[2].Width = 40;
                        gridItems.Columns[3].Width = 40;
                        gridItems.Columns[3].ReadOnly = true;
                        gridItems.Columns[4].Visible = false;
                        gridItems.RowTemplate.Height = 35;

                        //for(int mn=0;mn<dt.Rows.Count;mn++)
                        // {
                        //   DataRow r1=dtChkGrid.NewRow();                      
                        //   r1["ItemName"] = dt.Rows[mn]["ItemName"].ToString();
                        //   r1["Qty"] = dt.Rows[mn]["Qty"].ToString();
                        //   r1["Rate"] = dt.Rows[mn]["Rate"].ToString();
                        //   r1["Amt"] = dt.Rows[mn]["Amt"].ToString();
                        //   dtChkGrid.Rows.Add(r1);
                        //   //  dtChkGrid.Rows.Add(dt.Rows[mn]["ItemName"].ToString(),dt.Rows[mn]["Qty"].ToString(),dt.Rows[mn]["Rate"].ToString(),dt.Rows[mn]["Amt"].ToString());
                        // }
                        //  dtChkGrid.Clone(dt.Rows);                       
                        double tQty = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tQty += double.Parse(dt.Rows[i]["Qty"].ToString());
                        }
                        lblTotQty.Content = tQty.ToString();
                        funDisplayAmount(dt);

                    }
                    else
                    {
                        MyMessageBox.ShowBox("Enter Valid Bill Number", "Warning");
                        txtBillNo.Focus();
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Enter Valid Bill Number", "Warning");
                    txtBillNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }
        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;
        string mainStr, mainStr1;
        double findCenterPosition;
        DataTable dtPrint = new DataTable();
        //private void btnPrint_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (gridItems.Rows.Count > 0)  // Change if (gridItems.Items.Count > 0)
        //        {

        //            //new printing coding start
        //            mainStr = null;
        //            for (int i1 = 0; i1 < dtPrint.Rows.Count - 1; i1++)
        //            {
        //                if (dtPrint.Rows[i1]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i1]["Property"].ToString();
        //                }

        //                // print lint below logo
        //                if (dtPrint.Rows[i1]["Describ"].ToString() == "Print Line Below Logo")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i1]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                }
        //            }

        //            //top design start
        //            for (int i = 0; i < dtPrint.Rows.Count - 1; i++)
        //            {
        //                if (dtPrint.Rows[i]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i]["Property"].ToString();
        //                }

        //                // Top Line1
        //                //  topLine1="";
        //                if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 1")
        //                {
        //                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line1")
        //                            {
        //                                topLine1 = dtPrint.Rows[k]["Property"].ToString();


        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (double.Parse(charPerLine) - topLine1.Length); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += "\n";
        //                                //////if (topLine1.Length <= double.Parse(charPerLine))
        //                                //////{
        //                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
        //                                //////    if (findCenterPosition % 2 == 0)
        //                                //////    {
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine1;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    else
        //                                //////    {
        //                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine1;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    mainStr += "\n";
        //                                //////}
        //                            }
        //                        }
        //                    }
        //                }

        //                // Top Line2
        //                // topLine1="";
        //                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 2")
        //                {
        //                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line2")
        //                            {
        //                                topLine2 = dtPrint.Rows[k]["Property"].ToString();
        //                                mainStr += topLine2;
        //                                for (int j = 0; j < (double.Parse(charPerLine) - topLine2.Length); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += "\n";
        //                                //////if (topLine2.Length <= double.Parse(charPerLine))
        //                                //////{
        //                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
        //                                //////    if (findCenterPosition % 2 == 0)
        //                                //////    {
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine2;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    else
        //                                //////    {
        //                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine2;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    mainStr += "\n";
        //                                //////}
        //                            }
        //                        }
        //                    }
        //                }

        //                // Top Line3
        //                // topLine1 = "";
        //                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 3")
        //                {
        //                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line3")
        //                            {
        //                                topLine3 = dtPrint.Rows[k]["Property"].ToString();
        //                                mainStr += topLine3;
        //                                for (int j = 0; j < (double.Parse(charPerLine) - topLine3.Length); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += "\n";
        //                                //////if (topLine3.Length <= double.Parse(charPerLine))
        //                                //////{
        //                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
        //                                //////    if (findCenterPosition % 2 == 0)
        //                                //////    {
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine3;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    else
        //                                //////    {
        //                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine3;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    mainStr += "\n";
        //                                //////}
        //                            }
        //                        }
        //                    }
        //                }


        //                // Top Line4
        //                //topLine1 = "";
        //                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 4")
        //                {
        //                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line4")
        //                            {
        //                                topLine4 = dtPrint.Rows[k]["Property"].ToString();
        //                                mainStr += topLine4;
        //                                for (int j = 0; j < (double.Parse(charPerLine) - topLine4.Length); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += "\n";
        //                                ////if (topLine4.Length <= double.Parse(charPerLine))
        //                                ////{
        //                                ////    findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
        //                                ////    if (findCenterPosition % 2 == 0)
        //                                ////    {
        //                                ////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                ////        {
        //                                ////            mainStr += " ";
        //                                ////        }
        //                                ////        mainStr += topLine4;
        //                                ////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                ////        {
        //                                ////            mainStr += " ";
        //                                ////        }
        //                                ////    }
        //                                ////    else
        //                                ////    {
        //                                ////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                ////        {
        //                                ////            mainStr += " ";
        //                                ////        }
        //                                ////        mainStr += topLine4;
        //                                ////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                ////        {
        //                                ////            mainStr += " ";
        //                                ////        }
        //                                ////    }
        //                                ////    mainStr += "\n";
        //                                ////}
        //                            }
        //                        }
        //                    }
        //                }

        //               // Top Line5
        //                // topLine1 = "";
        //                else if (dtPrint.Rows[i]["Describ"].ToString() == "Print Top Line 5")
        //                {
        //                    if (dtPrint.Rows[i]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Top Line5")
        //                            {
        //                                topLine5 = dtPrint.Rows[k]["Property"].ToString();
        //                                mainStr += topLine5;
        //                                for (int j = 0; j < (double.Parse(charPerLine) - topLine5.Length); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += "\n";
        //                                //////if (topLine5.Length <= double.Parse(charPerLine))
        //                                //////{
        //                                //////    findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
        //                                //////    if (findCenterPosition % 2 == 0)
        //                                //////    {
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine5;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    else
        //                                //////    {
        //                                //////        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////        mainStr += topLine5;
        //                                //////        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                //////        {
        //                                //////            mainStr += " ";
        //                                //////        }
        //                                //////    }
        //                                //////    mainStr += "\n";
        //                                //////}
        //                            }
        //                        }
        //                    }
        //                }



        //            }
        //            //header design start
        //            for (int i2 = 0; i2 < dtPrint.Rows.Count - 1; i2++)
        //            {
        //                if (dtPrint.Rows[i2]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i2]["Property"].ToString();
        //                }

        //                // print lint below logo
        //                if (dtPrint.Rows[i2]["Describ"].ToString() == "Print Line Below Header")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i2]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                }
        //            }


        //            for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
        //            {
        //                if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i3]["Property"].ToString();
        //                }


        //                if (dtPrint.Rows[i3]["Describ"].ToString() == "Print Date")
        //                {
        //                    if (dtPrint.Rows[i3]["Property"].ToString() == "Yes")
        //                    {
        //                        string tChk = "Bill Date:" +tPrintDate.ToString("dd/MM/yyyy");
        //                        mainStr += "Bill Date:" + tPrintDate.ToString("dd/MM/yyyy");
        //                        double tTimeCount = (double.Parse(charPerLine) - (tChk.Length + 13));
        //                        for (int j = 0; j < tTimeCount; j++)
        //                        {
        //                            mainStr += " ";
        //                        }

        //                        for (int ii3 = 0; ii3 < dtPrint.Rows.Count - 1; ii3++)
        //                        {
        //                            if (dtPrint.Rows[ii3]["Describ"].ToString() == "Print Time")
        //                            {
        //                                if (dtPrint.Rows[ii3]["Property"].ToString() == "Yes")
        //                                {
        //                                    mainStr += "Time:" +tPrintTime;
        //                                }
        //                                else
        //                                {
        //                                    for (int j = 0; j < 13; j++)
        //                                    {
        //                                        mainStr += " ";
        //                                    }
        //                                }
        //                                mainStr += "\n";
        //                            }
        //                        }
        //                    }
        //                }

        //            }



        //            //receipt No 
        //            for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
        //            {
        //                if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i3]["Property"].ToString();
        //                }


        //                if (dtPrint.Rows[i3]["Describ"].ToString() == "Receipt Number")
        //                {
        //                    if (dtPrint.Rows[i3]["Property"].ToString() != "")
        //                    {
        //                        string temp = dtPrint.Rows[i3]["Property"].ToString() + lblBillNo.Content.ToString();
        //                        mainStr += temp;
        //                        for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";

        //                    }
        //                }
        //            }


        //            //Print Line Below Header
        //            for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
        //            {
        //                if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i4]["Property"].ToString();
        //                }

        //                // print lint below logo
        //                if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                }
        //            }


        //            for (int i5 = 0; i5 < dtPrint.Rows.Count - 1; i5++)
        //            {
        //                if (dtPrint.Rows[i5]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i5]["Property"].ToString();
        //                }

        //                double location = 0.00;
        //                string tempStr = null;
        //                if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Qunatity and Rate")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        string tQtyHeading = "";
        //                        tQtyHeading = "Particulars";
        //                        //  mainStr += tQtyHeading;
        //                        double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 22));
        //                        for (int j = 0; j < chkCount; j++)
        //                        {
        //                            tQtyHeading += " ";
        //                        }
        //                        tQtyHeading += "  Qty  ";
        //                        tQtyHeading += "U/Rate ";
        //                        tQtyHeading += " Amount";
        //                        mainStr += tQtyHeading;
        //                        mainStr += "\n";
        //                        for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
        //                        {
        //                            if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
        //                            {
        //                                charPerLine = dtPrint.Rows[i4]["Property"].ToString();
        //                            }

        //                            // print lint below logo
        //                            if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
        //                            {
        //                                lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
        //                                if (lineBelowLogo == "No Line")
        //                                {
        //                                    for (int j = 0; j < double.Parse(charPerLine); j++)
        //                                    {
        //                                        mainStr += " ";
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                                if (lineBelowLogo == "Single Line")
        //                                {
        //                                    for (int j = 0; j < double.Parse(charPerLine); j++)
        //                                    {
        //                                        mainStr += "-";
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                                else if (lineBelowLogo == "Double Line")
        //                                {
        //                                    for (int j = 0; j < double.Parse(charPerLine); j++)
        //                                    {
        //                                        mainStr += "=";
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                            }
        //                        }
        //                        for (int mn = 0; mn < gridItems.Rows.Count; mn++)
        //                        //foreach (DataRow row in dgsales.Rows)
        //                        {
        //                            // object[] array = dgsales.Rows[mn].;
        //                            bool isChk = false;
        //                            for (int z = 0; z < 4; z++)
        //                            {
        //                                if (gridItems.Rows[mn].Cells[z].Value.ToString().Trim() == "")
        //                                {
        //                                    isChk = true;
        //                                    break;
        //                                }
        //                            }
        //                            if (isChk == false)
        //                            {
        //                                for (int i = 0; i < 4; i++)
        //                                {
        //                                    tempStr = gridItems.Rows[mn].Cells[i].Value.ToString();
        //                                    //  MessageBox.Show(tempStr.Length.ToString());
        //                                    findCenterPosition = (double.Parse(charPerLine) - 22);
        //                                    if (i == 0)
        //                                    {
        //                                        if (tempStr.Length <= (int)findCenterPosition)
        //                                        {
        //                                            mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
        //                                        }
        //                                        else
        //                                        {
        //                                            string temp = tempStr.Substring(0,(((int)findCenterPosition + 4)<tempStr.Length)?(int)(findCenterPosition + 4):tempStr.Length);
        //                                            //    MessageBox.Show(temp);
        //                                            int chkSpace = temp.LastIndexOf(" ");
        //                                            int loc = (temp.Length - temp.LastIndexOf(" "));
        //                                            //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
        //                                            if (chkSpace != -1)
        //                                            {
        //                                                mainStr += temp.Substring(0, temp.LastIndexOf(" "));
        //                                                //   MessageBox.Show(mainStr.ToString());
        //                                                for (int j = 0; j < loc + 18; j++)
        //                                                {
        //                                                    mainStr += " ";
        //                                                }
        //                                                mainStr += "\n";
        //                                                string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
        //                                                // mainStr += temp1;
        //                                                if (temp1.Length <= (int)findCenterPosition)
        //                                                {
        //                                                    mainStr += temp1.PadRight((int)findCenterPosition, ' ');
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                mainStr += temp.ToString();
        //                                            }

        //                                        }
        //                                    }

        //                                    if (i == 1)
        //                                    {
        //                                        if (tempStr.Length < 8)
        //                                        {
        //                                            mainStr += tempStr.PadRight(7, ' ');
        //                                        }
        //                                    }
        //                                    if (i == 2)
        //                                    {
        //                                        // mainStr += tempStr.PadRight(7, ' ');
        //                                        if (tempStr.Length <= 7)
        //                                        {
        //                                            mainStr += tempStr.PadLeft(7, ' ');
        //                                        }
        //                                    }
        //                                    if (i == 3)
        //                                    {
        //                                        if (tempStr.Length <= 8)
        //                                        {
        //                                            mainStr += tempStr.PadLeft(8, ' ');
        //                                        }
        //                                    }
        //                                    // tPrintText += tempStr;
        //                                }
        //                                mainStr += "\n";
        //                            }
        //                        }

        //                    }

        //                    else
        //                    {
        //                        string tQtyHeading = "";
        //                        tQtyHeading = "Particulars";
        //                        mainStr += tQtyHeading;
        //                        double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));
        //                        for (int j = 0; j < tQtyCount; j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "    ";
        //                        mainStr += "       ";
        //                        mainStr += "Amount";
        //                        mainStr += "\n";

        //                        for (int mn = 0; mn < gridItems.Rows.Count; mn++)
        //                        //foreach (DataRow row in dgsales.Rows)
        //                        {
        //                            // object[] array = row.ItemArray;

        //                            for (int i = 0; i < 4; i++)
        //                            {
        //                                tempStr = gridItems.Rows[mn].Cells[i].Value.ToString();
        //                                //  MessageBox.Show(tempStr.Length.ToString());
        //                                findCenterPosition = (double.Parse(charPerLine) - 18);
        //                                if (i == 0)
        //                                {
        //                                    if (tempStr.Length <= (int)findCenterPosition)
        //                                    {
        //                                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
        //                                    }
        //                                    else
        //                                    {
        //                                        string temp = tempStr.Substring(0, (int)findCenterPosition);
        //                                        int loc = (temp.Length - temp.LastIndexOf(" "));
        //                                        mainStr += temp.Substring(0, temp.LastIndexOf(" "));
        //                                        for (int j = 0; j < loc + 18; j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += "\n";
        //                                        string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
        //                                        mainStr += temp1;
        //                                        if (temp1.Length <= (int)findCenterPosition)
        //                                        {
        //                                            mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
        //                                        }
        //                                    }
        //                                    //if (tempStr.Length <= (int)findCenterPosition)
        //                                    //{
        //                                    //    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
        //                                    //}
        //                                }
        //                                if (i == 1)
        //                                {
        //                                    mainStr += "   ";
        //                                    //if (tempStr.Length < 4)
        //                                    //{
        //                                    //    mainStr += tempStr.PadRight(3, ' ');
        //                                    //}
        //                                }
        //                                if (i == 2)
        //                                {
        //                                    mainStr += "       ";
        //                                    //if (tempStr.Length <= 7)
        //                                    //{
        //                                    //    mainStr += tempStr.PadLeft(7, ' ');
        //                                    //}
        //                                }
        //                                if (i == 3)
        //                                {
        //                                    if (tempStr.Length <= 8)
        //                                    {
        //                                        mainStr += tempStr.PadLeft(8, ' ');
        //                                    }
        //                                }
        //                                // tPrintText += tempStr;
        //                            }
        //                            mainStr += "\n";
        //                        }
        //                    }
        //                }
        //                if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Subtotal")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Subtotal")
        //                            {
        //                                topLine1 = dtPrint.Rows[k]["Property"].ToString();
        //                                if (topLine1.Length <= (double.Parse(charPerLine) - 9))
        //                                {
        //                                    findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));

        //                                    for (int j = 0; j < (findCenterPosition); j++)
        //                                    {
        //                                        mainStr += " ";
        //                                    }
        //                                    mainStr += topLine1;
        //                                    topLine1 =string.Format("{0:0.00}",(lblTotAmt.Content.ToString()=="")?0.00:double.Parse(lblTotAmt.Content.ToString()));
        //                                    for (int j = 0; j < 9 - topLine1.Length; j++)
        //                                    {
        //                                        mainStr += " ";
        //                                    }
        //                                    mainStr += topLine1;
        //                                    //  +"  3000.00";

        //                                }

        //                                mainStr += "\n";
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (lblDiscount.Content.ToString() != "")
        //            {
        //                if (double.Parse(lblDiscount.Content.ToString()) > 0)
        //                {
        //                    topLine1 = "Discount:";
        //                    if (topLine1.Length <= (double.Parse(charPerLine) - 9))
        //                    {
        //                        findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));

        //                        for (int j = 0; j < (findCenterPosition); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += topLine1;
        //                        topLine1 =string.Format("{0:0.00}",(lblDiscount.Content.ToString()=="")?0.00:double.Parse(lblDiscount.Content.ToString()));
        //                        for (int j = 0; j < 9 - topLine1.Length; j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += topLine1;
        //                        //  +"  3000.00";
        //                    }

        //                    mainStr += "\n";
        //                    //
        //                    //
        //                    //Print Products List-End

        //                }
        //            }



        //            //Print line Above Total
        //            for (int i10 = 0; i10 < dtPrint.Rows.Count - 1; i10++)
        //            {
        //                if (dtPrint.Rows[i10]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i10]["Property"].ToString();
        //                }


        //                if (dtPrint.Rows[i10]["Describ"].ToString() == "Print line Above Total")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i10]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                }
        //            }
        //            // Pay this amount

        //            //receipt No 
        //            for (int i9 = 0; i9 < dtPrint.Rows.Count - 1; i9++)
        //            {
        //                if (dtPrint.Rows[i9]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i9]["Property"].ToString();
        //                }


        //                if (dtPrint.Rows[i9]["Describ"].ToString() == "Pay This Amount")
        //                {
        //                    if (dtPrint.Rows[i9]["Property"].ToString() != "")
        //                    {
        //                        topLine1 = "Total Amount : "+string.Format("{0:0.00}",double.Parse(lblNetAmt.Content.ToString()));

        //                        // +":$3000.00";
        //                        if (topLine1.Length <= double.Parse(charPerLine))
        //                        {
        //                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
        //                            if (findCenterPosition % 2 == 0)
        //                            {
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                            }
        //                            mainStr += "\n";
        //                        }

        //                    }
        //                }
        //            }



        //            //Print Line Below Total
        //            for (int i10 = 0; i10 < dtPrint.Rows.Count - 1; i10++)
        //            {
        //                if (dtPrint.Rows[i10]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i10]["Property"].ToString();
        //                }

        //                // print lint below logo
        //                if (dtPrint.Rows[i10]["Describ"].ToString() == "Print Line Below Total")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i10]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                }
        //            }
        //            // Your Order Number

        //            //receipt No 
        //            for (int i9 = 0; i9 < dtPrint.Rows.Count - 1; i9++)
        //            {
        //                if (dtPrint.Rows[i9]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i9]["Property"].ToString();
        //                }


        //                if (dtPrint.Rows[i9]["Describ"].ToString() == "Order Number")
        //                {
        //                    if (dtPrint.Rows[i9]["Property"].ToString() != "")
        //                    {
        //                        topLine1 = dtPrint.Rows[i9]["Property"].ToString() + lblBillNo.Content.ToString();
        //                        if (topLine1.Length <= double.Parse(charPerLine))
        //                        {
        //                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
        //                            if (findCenterPosition % 2 == 0)
        //                            {
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                            }
        //                            mainStr += "\n";
        //                        }

        //                    }
        //                }
        //            }

        //            //Print Line Above Bottom Text
        //            for (int i7 = 0; i7 < dtPrint.Rows.Count - 1; i7++)
        //            {
        //                if (dtPrint.Rows[i7]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i7]["Property"].ToString();
        //                }


        //                if (dtPrint.Rows[i7]["Describ"].ToString() == "Print Line Above Bottom Text")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i7]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";

        //                    }
        //                }
        //            }

        //            //bottom line
        //            for (int i5 = 0; i5 < dtPrint.Rows.Count - 1; i5++)
        //            {
        //                if (dtPrint.Rows[i5]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i5]["Property"].ToString();
        //                }

        //                // Print Bottom Line 1
        //                //  topLine1="";
        //                if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 1")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 1")
        //                            {
        //                                topLine1 = dtPrint.Rows[k]["Property"].ToString();
        //                                if (topLine1.Length <= double.Parse(charPerLine))
        //                                {
        //                                    findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
        //                                    if (findCenterPosition % 2 == 0)
        //                                    {
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine1;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine1;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                // Print Bottom Line 2
        //                // topLine1="";
        //                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 2")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 2")
        //                            {
        //                                topLine2 = dtPrint.Rows[k]["Property"].ToString();
        //                                if (topLine2.Length <= double.Parse(charPerLine))
        //                                {
        //                                    findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
        //                                    if (findCenterPosition % 2 == 0)
        //                                    {
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine2;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine2;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                // Print Bottom Line 3
        //                // topLine1 = "";
        //                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 3")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 3")
        //                            {
        //                                topLine3 = dtPrint.Rows[k]["Property"].ToString();
        //                                if (topLine3.Length <= double.Parse(charPerLine))
        //                                {
        //                                    findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
        //                                    if (findCenterPosition % 2 == 0)
        //                                    {
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine3;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine3;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }


        //                // Print Bottom Line 4
        //                //topLine1 = "";
        //                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 4")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 4")
        //                            {
        //                                topLine4 = dtPrint.Rows[k]["Property"].ToString();
        //                                if (topLine4.Length <= double.Parse(charPerLine))
        //                                {
        //                                    findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
        //                                    if (findCenterPosition % 2 == 0)
        //                                    {
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine4;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine4;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //               //Print Bottom Line 5
        //                // topLine1 = "";
        //                else if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Bottom Line 5")
        //                {
        //                    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
        //                    {
        //                        for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                        {
        //                            if (dtPrint.Rows[k]["Describ"].ToString() == "Bottom Line 5")
        //                            {
        //                                topLine5 = dtPrint.Rows[k]["Property"].ToString();
        //                                if (topLine5.Length <= double.Parse(charPerLine))
        //                                {
        //                                    findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
        //                                    if (findCenterPosition % 2 == 0)
        //                                    {
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine5;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                        mainStr += topLine5;
        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                        {
        //                                            mainStr += " ";
        //                                        }
        //                                    }
        //                                    mainStr += "\n";
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            //Print Line Below Header
        //            for (int i6 = 0; i6 < dtPrint.Rows.Count - 1; i6++)
        //            {
        //                if (dtPrint.Rows[i6]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i6]["Property"].ToString();
        //                }

        //                // print lint below logo
        //                if (dtPrint.Rows[i6]["Describ"].ToString() == "Print Line Below Bottom Text")
        //                {
        //                    lineBelowLogo = dtPrint.Rows[i6]["Property"].ToString();
        //                    if (lineBelowLogo == "No Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += " ";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    if (lineBelowLogo == "Single Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "-";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                    else if (lineBelowLogo == "Double Line")
        //                    {
        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
        //                        {
        //                            mainStr += "=";
        //                        }
        //                        mainStr += "\n";
        //                    }
        //                }
        //            }

        //            //Print Bottom Time
        //            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
        //            {
        //                if (dtPrint.Rows[i8]["Describ"].ToString() == "Characters Per Line*")
        //                {
        //                    charPerLine = dtPrint.Rows[i8]["Property"].ToString();
        //                }

        //                // Top Line1
        //                //  topLine1="";
        //                if (dtPrint.Rows[i8]["Describ"].ToString() == "Print Bottom Time")
        //                {
        //                    if (dtPrint.Rows[i8]["Property"].ToString() == "Yes")
        //                    {

        //                        topLine1 = currentDate.ToString();
        //                        if (topLine1.Length <= double.Parse(charPerLine))
        //                        {
        //                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
        //                            if (findCenterPosition % 2 == 0)
        //                            {
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                                mainStr += topLine1;
        //                                for (int j = 0; j < (findCenterPosition / 2); j++)
        //                                {
        //                                    mainStr += " ";
        //                                }
        //                            }
        //                            mainStr += "\n";
        //                        }
        //                    }
        //                }
        //            }
        //            // MessageBox.Show(mainStr);
        //            string tPrinterType = "";
        //            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
        //            {
        //                if (dtPrint.Rows[i8]["Describ"].ToString() == "Enable This Device*")
        //                {
        //                    if (dtPrint.Rows[i8]["Property"].ToString() == "Yes")
        //                    {
        //                        tPrinterType = "Receipt";
        //                    }

        //                }
        //            }

        //            int tNoPrint = 0;
        //            for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
        //            {
        //                if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
        //                {
        //                    if (tPrinterType == "Receipt")
        //                    {
        //                        DataTable dtPrinter = new DataTable();
        //                        dtPrinter.Rows.Clear();
        //                        SqlDataAdapter adpPrinter = new SqlDataAdapter("select * from CrystalReportPrinterList", con);
        //                        adpPrinter.Fill(dtPrinter);
        //                        bool isChkPrinter = false;
        //                        for (int i = 0; i < dtPrinter.Rows.Count; i++)
        //                        {
        //                            string printerName = dtPrinter.Rows[i]["PrinterName"].ToString();
        //                            isChkPrinter = false;
        //                            if (dtPrint.Rows[i8]["Property"].ToString().ToUpper() == printerName.ToUpper())
        //                            {
        //                                isChkPrinter = true;
        //                                rptReceiptReport rpt = new rptReceiptReport();
        //                                CrystalDecisions.CrystalReports.Engine.TextObject str1 = ((CrystalDecisions.CrystalReports.Engine.TextObject)rpt.Section2.ReportObjects["Text1"]);
        //                                str1.Text = mainStr;
        //                                rpt.PrintToPrinter(0, true, 1, 0);
        //                                break;
        //                            }
        //                        }
        //                        if (isChkPrinter == false)
        //                        {
        //                            for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                            {
        //                                if (dtPrint.Rows[k]["Describ"].ToString() == "Print Copies*")
        //                                {
        //                                    topLine5 = dtPrint.Rows[k]["Property"].ToString();
        //                                    if (topLine5 == "1 Copy")
        //                                    {
        //                                        tNoPrint = 1;
        //                                    }
        //                                    else if (topLine5 == "2 Copy")
        //                                    {
        //                                        tNoPrint = 2;
        //                                    }
        //                                    else if (topLine5 == "3 Copy")
        //                                    {
        //                                        tNoPrint = 3;
        //                                    }
        //                                    else if (topLine5 == "No Copies")
        //                                    {
        //                                        tNoPrint = 0;
        //                                    }

        //                                    for (int i2 = 0; i2 < tNoPrint; i2++)
        //                                    {
        //                                        RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), mainStr);
        //                                        //string s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 29, 86, 66, 0, 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
        //                                        //RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MyMessageBox.ShowBox("Enter Product", "Warning");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MyMessageBox.ShowBox(ex.Message, "Warning");
        //    }
        //}

        private void txtBillNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    funLoadOldBill(txtBillNo.Text.Trim());
                    txtEnterValue.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_Class.clsVariables.tVoidActionType == "BILLNO")
                {
                    try
                    {
                        funLoadOldBill(txtBillNo.Text.Trim());
                        txtEnterValue.Focus();
                    }
                    catch (Exception ex)
                    {
                        MyMessageBox.ShowBox(ex.Message, "Warning");
                    }
                }
                else if (_Class.clsVariables.tVoidActionType == "ITEMCODE")
                {
                    funLoadItem();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            funBtnup();
        }

        public void funBtnup()
        {
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
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            funBtnDown();
        }
        public void funBtnDown()
        {
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
        }
        DataTable dtRemove = new DataTable();
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                funOfferLoad();
                _Class.clsVariables.LoadPreviousBill = "LoadNot";
                if (dt.Rows.Count > 1)
                {
                    if (gridItems.SelectedRows.Count > 0)
                    {
                        int row1 = gridItems.SelectedRows[0].Index;
                        string iname = Convert.ToString(dt.Rows[row1][0]);
                        int ni = iname.IndexOf("-");
                        if (ni != -1)
                            iname = iname.Substring(0, ni);
                        string tItemName = iname;
                        tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");
                        DataRow[] dtRowSingleFree = dtSingleAllFreeItem.Select("Item_Name='" + tItemName + "'");
                        DataRow[] dtRowDifferentPrice = dtDifferent.Select("FreeType = 'Price' And ItemType='Different' and Item_name='" + tItemName + "'", "TotSaleQty DESC");
                        DataRow[] dtOfferRow = dtOffer.Select("FreeType = 'Price' And ItemType='Single' and Item_Name='" + tItemName + "'", "TotSaleQty DESC");
                        DataRow[] dtOfferSameFreeRow = dtOfferSameFree.Select("Item_Name='" + tItemName + "'", "TotSaleQty DESC");

                        if (dtRowSingleFree.Length == 0 && dtRowDifferentPrice.Length == 0 && dtOfferRow.Length == 0 && dtOfferSameFreeRow.Length == 0)
                        {
                            dtRemove.Rows.Add(dt.Rows[row1][0].ToString(), "0", dt.Rows[row1][2].ToString(), dt.Rows[row1][3].ToString(), dt.Rows[row1][4].ToString(), Convert.ToString(dt.Rows[row1][5]), Convert.ToString(dt.Rows[row1][6]), Convert.ToString(dt.Rows[row1][7]));
                            dt.Rows.RemoveAt(row1);  // Change dt.Rows.RemoveAt(gridItems.Items.IndexOf(gridItems.SelectedItem));
                            if (gridItems.Rows.Count != 0)
                            {
                                gridItems.Rows[gridItems.Rows.Count - 1].Selected = true;
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Selected item could not be remove from the list", "Warning");
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count == 1)
                    {
                        MyMessageBox.ShowBox("Single Item could not be remove from the list");
                    }
                }
                gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                funDisplayAmount(dt);
                funRoundCalculate();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        int tempFindStar;
        SqlDataReader reader = null;
        int count = 0;
        int rowIndex;
        double totQty, totAmt, totTax, tTax;
        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    funOfferLoad();
                    funLoadItem();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funLoadItem()
        {
            try
            {
                if (gridItems.Rows.Count > 0)
                {
                    if (txtEnterValue.Text.Trim() != "")
                    {
                        if (txtEnterValue.Text.ToString().Substring(0, 1) == "-")
                        {
                            // funConnectionStateCheck();
                            DataRow dr = null;
                            //  txtEnterValue.Select(txtEnterValue.Text.Length, 0);

                            // if (listSelect.SelectedItems.Count > 0 || txtEnterValue.Text.Length > 0)
                            //  {

                            // DataRow dr = null;
                            DataTable dtNew = new DataTable();
                            dtNew.Rows.Clear();
                            // dtRemove.Rows.Clear();

                            tempFindStar = txtEnterValue.Text.IndexOf("*");
                            if (tempFindStar != -1 && tempFindStar != 0)
                            {
                                string tempItemCode = txtEnterValue.Text.Substring(tempFindStar + 1, ((txtEnterValue.Text.Length - 1) - (tempFindStar)));
                                //  MessageBox.Show(txtEnterValue.Text.Substring(tempFindStar+1,((txtEnterValue.Text.Length-1)-(tempFindStar))));
                                string tempQty = txtEnterValue.Text.Substring(0, tempFindStar);
                                //  MessageBox.Show(txtEnterValue.Text.Substring(0, tempFindStar));
                                rowIndex = 0;
                                // DataRow dr = null;
                                dr = dt.NewRow();
                                // MessageBox.Show(ClickedButton.Content.ToString());
                                SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.AddWithValue("@tValue", tempItemCode);
                                cmd1.Parameters.AddWithValue("@tActionType", "ITEMCODE");
                                reader = cmd1.ExecuteReader();
                                dtNew.Load(reader);
                                if (dtNew.Rows.Count > 0)
                                {
                                    count = 0;
                                    totAmt = 0.00;
                                    totQty = 0.00;
                                    totTax = 0.00;
                                    string tempItemName = dtNew.Rows[0]["Item_Name"].ToString();

                                    string tItemName = Convert.ToString(tempItemName);
                                    tItemName = (tItemName.IndexOf("'") == -1) ? tItemName : tItemName.Replace("'", "''");
                                    DataRow[] dtRowSingleFree = dtSingleAllFreeItem.Select("Item_Name='" + tItemName + "'");
                                    DataRow[] dtRowDifferentPrice = dtDifferent.Select("FreeType = 'Price' And ItemType='Different' and Item_name='" + tItemName + "'", "TotSaleQty DESC");
                                    DataRow[] dtOfferRow = dtOffer.Select("FreeType = 'Price' And ItemType='Single' and Item_Name='" + tItemName + "'", "TotSaleQty DESC");
                                    DataRow[] dtOfferSameFreeRow = dtOfferSameFree.Select("Item_Name='" + tItemName + "'", "TotSaleQty DESC");

                                    if (dtRowSingleFree.Length == 0 && dtRowDifferentPrice.Length == 0 && dtOfferRow.Length == 0 && dtOfferSameFreeRow.Length == 0)
                                    {
                                        double tCurrentQty = 0, tOtherDisc = 0;
                                        foreach (DataRow dr1 in dt.Rows)
                                        {
                                            if (dr1["itemName"].ToString() == tempItemName)
                                            {
                                                tCurrentQty = Convert.ToDouble(Convert.ToString(dr1["Qty"]));
                                                tOtherDisc = Convert.ToDouble(Convert.ToString(dr1["Other"]));
                                                tOtherDisc = tOtherDisc / tCurrentQty;
                                                count = 1;
                                                if (double.Parse(dr1["Qty"].ToString()) >= double.Parse(tempQty.ToString()))
                                                {
                                                    if ((double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())) >= 0 && dt.Rows.Count > 1)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();
                                                        double tAmt = ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString()));
                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", tAmt);
                                                        dt.Rows[rowIndex]["Other"] = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dr1["Qty"])) * tOtherDisc);
                                                    }
                                                    else if ((double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())) > 0 && dt.Rows.Count == 1)
                                                    {
                                                        dt.Rows[rowIndex]["Qty"] = (double.Parse(dr1["Qty"].ToString()) + double.Parse(tempQty.ToString())).ToString();
                                                        double tAmt = ((double.Parse(dr1["Qty"].ToString())) * double.Parse(dr1["Rate"].ToString()));
                                                        dt.Rows[rowIndex]["Amt"] = string.Format("{0:0.00}", tAmt);
                                                        dt.Rows[rowIndex]["Other"] = string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(dr1["Qty"])) * tOtherDisc);
                                                    }
                                                    else
                                                    {
                                                        MyMessageBox.ShowBox("Please Enter Valid Return Qty", "Warning");
                                                    }
                                                }
                                                else
                                                {
                                                    MyMessageBox.ShowBox("Enter Valid Return Qty", "Warning");
                                                }
                                            }
                                            rowIndex += 1;

                                        }
                                        if (count == 0)
                                        {
                                            //dr["ItemName"] = dtNew.Rows[0]["Item_name"].ToString();
                                            //dr["Qty"] = tempQty.ToString();
                                            //dr["Rate"] = string.Format("{0:0.00}", double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString()));
                                            //dr["Amt"] = string.Format("{0:0.00}", (double.Parse(tempQty) * double.Parse(dtNew.Rows[0]["Item_mrsp"].ToString())));
                                            //dt.Rows.Add(dr);
                                            MyMessageBox.ShowBox("Kindly enter current bill sold item code", "Warning");
                                        }
                                    }
                                    else
                                    {
                                        MyMessageBox.ShowBox("This item quantity could not be change", "warning");
                                    }
                                    // funStockDisplay(tempItemName);
                                    funDisplayAmount(dt);
                                    gridItems.DataSource = dt.DefaultView; // Change gridItems.ItemsSource = dt.DefaultView;
                                    // funStopAtQtyAndRate(tempItemName);
                                    funScrollGrid();
                                    funRoundCalculate();
                                }
                                else
                                {
                                    MyMessageBox.ShowBox("Item Code Not Found", "Warning");
                                }
                                txtEnterValue.Text = "";
                                txtEnterValue.Focus();
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
        public void funScrollGrid()
        {
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
        }
        public void funDisplayAmount(DataTable dt)
        {
            try
            {

                double @tTotQty = 0;

                DataRow[] dtItemGroupRow;
                string tItemName = "";
                double tDiscount = 0, tItemAmt = 0, tGroupDiscPercent = 0, tTotGroupDisc = 0, tSpecialDisc = 0, tOverAllDisc = 0;
                if (_Class.clsVariables.tMainDiscountType == "Group")
                {

                    for (int mn = 0; mn < dt.Rows.Count; mn++)
                    {
                        tDiscount = 0;
                        tItemAmt = 0;
                        tGroupDiscPercent = 0;
                        tItemAmt = (string.IsNullOrEmpty(Convert.ToString(dt.Rows[mn]["Amt"]))) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[mn]["Amt"]));
                        string iname = Convert.ToString(dt.Rows[mn]["ItemName"]);
                        int ni = iname.IndexOf("-");
                        if (ni != -1)
                            iname = iname.Substring(0, ni);
                        tItemName = iname;
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

                }
                double @tNetAmt = 0;
                double @tTotAmt = 0;

                double @tTotTax = 0;

                double @Qty = 0;
                double @tTotDiscAmt = 0;
                double @tDisc = 0, @tSDisc = 0, @tODisc = 0;
                double @Amt = 0, @tTax = 0;
                @tTotQty = 0;
                tOverAllDisc = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    @Qty = 0;
                    @Amt = (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][3])) == true) ? 0 : Convert.ToDouble(dt.Rows[i][3].ToString());
                    @tTax = 0;
                    if (dt.Rows[i][1].ToString() != "")
                    {
                        @Qty = Convert.ToDouble(dt.Rows[i][1].ToString());
                    }

                    if (dt.Rows[i]["Disc"].ToString() != "")
                    {
                        @tDisc = Convert.ToDouble(dt.Rows[i]["Disc"].ToString());
                    }
                    if (dt.Rows[i]["SDisc"].ToString() != "")
                    {
                        @tSDisc = Convert.ToDouble(dt.Rows[i]["SDisc"].ToString());
                    }
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
                    string iname = dt.Rows[i][0].ToString();
                    int ni = iname.IndexOf("-");
                    if (ni != -1)
                        iname = iname.Substring(0, ni);
                    SqlCommand cmd = new SqlCommand("Select Nt_percent from Tax_Table where Tax_no=(Select Tax_no from item_table where Item_Active=1 and Item_name=@ItemName)", con);
                    cmd.Parameters.AddWithValue("@ItemName", iname);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(stNew);
                    if (stNew.Rows.Count > 0)
                    {
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

                {
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


                //double @tNetAmt = 0;
                //double @tTotAmt = 0;
                //double @tTotQty = 0;
                //double @tTotTax = 0;
                //double @Qty = 0;
                //double @Amt = 0, @tTax = 0, @Disc=0, @SDisc=0;
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    @Qty = 0;
                //    @Amt = double.Parse(dt.Rows[i][3].ToString());
                //    @tTax = 0;
                //    if (dt.Rows[i][1].ToString() != "")
                //    {
                //        @Qty =Convert.ToDouble(Convert.ToString(dt.Rows[i][1]));
                //    }
                //    @Disc = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][5])) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[i][5]));
                //    @SDisc = string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][6])) ? 0 : Convert.ToDouble(Convert.ToString(dt.Rows[i][6]));
                //    @tTotQty = @tTotQty + @Qty;
                //    @tTotAmt = @tTotAmt + @Amt;
                //    DataTable stNew = new DataTable();
                //    stNew.Rows.Clear();
                //    if (_Class.clsVariables.tempGDisplayTaxType != "NoTax")
                //    {
                //        SqlCommand cmd = new SqlCommand("Select Nt_percent from Tax_Table where Tax_no=(Select Tax_no from item_table where Item_name=@ItemName)", con);
                //        cmd.Parameters.AddWithValue("@ItemName", dt.Rows[i][0].ToString());
                //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //        adp.Fill(stNew);
                //        if (stNew.Rows.Count > 0)
                //        {
                //           // @tTax = (@Amt * double.Parse(stNew.Rows[0][0].ToString())) / 100;
                //            if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                //            {
                //                @tTax = @Amt - ((@Amt * 100) / (100 + Convert.ToDouble(stNew.Rows[0][0].ToString())));
                //            }
                //            else if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                //            {
                //                @tTax = (double.Parse(dt.Rows[i][2].ToString()) * double.Parse(stNew.Rows[0][0].ToString())) / 100;
                //                @tTax = @Qty * @tTax;
                //            }
                //            @tTotTax = @tTotTax + @tTax;
                //        }
                //    }

                //}

                //@tNetAmt = (@tTotTax + @tTotAmt) - double.Parse(lblDiscount.Content.ToString());
                //if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                //{
                //    @tNetAmt = ( @tTotAmt) - double.Parse(lblDiscount.Content.ToString());
                //}
                //lblTaxAmt.Content = String.Format("{0:0.00}", @tTotTax);
                //lblTotQty.Content = @tTotQty.ToString();
                //lblTotAmt.Content = String.Format("{0:0.00}", @tTotAmt);
                //lblNetAmt.Content = String.Format("{0:0.00}", @tNetAmt);
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

        DataTable dtOffer = new DataTable();
        DataTable dtOfferSameFree = new DataTable();
        DataTable dtOfferDifferentFree = new DataTable();
        DataTable dtOfferName = new DataTable();
        DataTable dtSingleAllFreeItem = new DataTable();
        DataTable dtDifferent = new DataTable();
        DataTable dtOfferDetails = new DataTable();
        string tBillDateDay = "";
        public void funOfferLoad()
        {
            try
            {
                //  dtFreeBalance.Rows.Clear();
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
                string tQueryChk = @"Select FreeSno, FreeSnoGroup,OfferName, Item_table.Item_name, TotSaleQty, TotSalePrice, FromDate, ToDate, ItemType,FreeType,Active
from FreeItemMaster_table, Item_table Where " + tQueryAppend + "Item_table.Item_no=FreeItemMaster_table.Item_no and FreeType='Price' and FreeItemMaster_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) order by FreeSnoGroup ASC";
                SqlCommand cmdOffer = new SqlCommand(tQueryChk, con);
                SqlDataAdapter adpOffer = new SqlDataAdapter(cmdOffer);
                adpOffer.Fill(dtOffer);

                dtDifferent.Rows.Clear();
                SqlCommand cmdDiffer = new SqlCommand("Pro_viewDiffFree", con);
                cmdDiffer.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adpDiffer = new SqlDataAdapter(cmdDiffer);
                adpDiffer.Fill(dtDifferent);


                dtOfferSameFree.Rows.Clear();
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
        }

        void funRoundCalculate1()
        {
            try
            {
                funConnectionStateCheck();
                SqlCommand cmd = new SqlCommand("sp_SalesCreation_RoundCalculate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                dsRound.Load(dr);
                if (dsRound.Rows.Count > 0)
                {
                    tRoundType = dsRound.Rows[0]["RProp"].ToString();
                    // MessageBox.Show(tRoundType);
                    tRoundValue = Math.Round(double.Parse(lblRefund.Content.ToString()), 2);
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
                        if (tDecimal == 0.99 && tDecimal == 0.98)
                        {
                            tWhole = tWhole + 1;
                            lblRefund.Content = String.Format("{0:0.00}", tWhole);
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
                                            lblRefund.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
                                            break;
                                        }
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                        {
                                            // tWhole = tWhole + 1;
                                            lblRefund.Content = tRoundValue.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "5");
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
                                                lblRefund.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            }
                                            else
                                            {
                                                //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                                //  lblNetAmt.Content = String.Format("{0:0.00}", (tRoundValue.ToString().Replace(secondDecimal.ToString()+firstDecimal.ToString(),secondDecimal+"0")));
                                                lblRefund.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
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
                                            lblRefund.Content = String.Format("{0:0.00}", tRoundValue.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "5"));
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
                            lblRefund.Content = String.Format("{0:0.00}", tWhole);
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
                                            lblRefund.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
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

                                            lblRefund.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            break;
                                        }
                                    case "5":
                                    case "6":
                                    case "7":
                                    case "8":
                                    case "9":
                                        {
                                            secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                            lblRefund.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
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
        }
        DataTable dsRound = new DataTable();
        string tRoundType, firstDecimal, secondDecimal;
        double tRoundValue, tWhole, tDecimal;
        SqlDataReader dr = null;
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
        }
        //void funRoundCalculate()
        //{
        //    try
        //    {
        //        funConnectionStateCheck();
        //        SqlCommand cmd = new SqlCommand("sp_SalesCreation_RoundCalculate", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        dr = cmd.ExecuteReader();
        //        dsRound.Load(dr);
        //        if (dsRound.Rows.Count > 0)
        //        {
        //            tRoundType = dsRound.Rows[0]["RProp"].ToString();
        //            // MessageBox.Show(tRoundType);
        //            tRoundValue = Math.Round(double.Parse(lblNetAmt.Content.ToString()), 2);
        //            tDecimal = Math.Round(tRoundValue % 1, 2);
        //            //  MessageBox.Show(tDecimal.ToString());
        //            tWhole = tRoundValue - tDecimal;
        //            // MessageBox.Show(tWhole.ToString());
        //            //  MessageBox.Show(Convert.ToString( tDecimal).Length.ToString());
        //            if (tDecimal.ToString().Length == 1)
        //            {
        //                firstDecimal = "0";
        //                secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1);
        //            }
        //            else if (tDecimal.ToString().Length == 4)
        //            {
        //                firstDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1);
        //                secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
        //            }
        //            if (tRoundType == "5cent")
        //            {
        //                if (tDecimal == 0.99 || tDecimal == 0.98)
        //                {
        //                    tWhole = tWhole + 1;
        //                    lblNetAmt.Content = String.Format("{0:0.00}", tWhole);
        //                }
        //                else if (tDecimal >= 0.90 && tDecimal < 0.98)
        //                {
        //                    if (tDecimal.ToString().Length == 4)
        //                    {

        //                        switch (tDecimal.ToString().Substring(3, 1))
        //                        {

        //                            case "0":
        //                            case "1":
        //                            case "2":
        //                                {
        //                                    // secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
        //                                    lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
        //                                    break;
        //                                }
        //                            case "3":
        //                            case "4":
        //                            case "5":
        //                            case "6":
        //                            case "7":
        //                                {
        //                                    // tWhole = tWhole + 1;
        //                                    lblNetAmt.Content = tRoundValue.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "5");
        //                                    break;
        //                                }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //  MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
        //                    }
        //                }
        //                else
        //                {
        //                    //      MessageBox.Show(tDecimal.ToString().Substring(3, 1));
        //                    if (tDecimal.ToString().Length == 4)
        //                    {
        //                        switch (tDecimal.ToString().Substring(3, 1))
        //                        {
        //                            case "8":
        //                            case "9":
        //                            case "0":
        //                            case "1":
        //                            case "2":
        //                                {
        //                                    //  MessageBox.Show(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1));

        //                                    if (firstDecimal == "9" || firstDecimal == "8")
        //                                    {
        //                                        secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
        //                                        lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
        //                                    }
        //                                    else
        //                                    {
        //                                        //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
        //                                        //  lblNetAmt.Content = String.Format("{0:0.00}", (tRoundValue.ToString().Replace(secondDecimal.ToString()+firstDecimal.ToString(),secondDecimal+"0")));
        //                                        lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
        //                                    }
        //                                    break;
        //                                }
        //                            case "3":
        //                            case "4":
        //                            case "5":
        //                            case "6":
        //                            case "7":
        //                                {
        //                                    //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
        //                                    lblNetAmt.Content = String.Format("{0:0.00}", tRoundValue.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "5"));
        //                                    break;
        //                                }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //   MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
        //                    }

        //                }
        //                ////// string tempStr = lblNetAmt.Content.ToString().Trim();
        //                //////// int start = tempStr.Length - 1;
        //                //////// MessageBox.Show(tempStr.Substring(tempStr.Length - 1, 1));
        //                //////                }
        //            }
        //            if (tRoundType == "10cent")
        //            {
        //                if (tDecimal <= 0.99 && tDecimal >= 0.95)
        //                {
        //                    tWhole = tWhole + 1;
        //                    lblNetAmt.Content = String.Format("{0:0.00}", tWhole);
        //                }
        //                else if (tDecimal >= 0.90 && tDecimal < 0.95)
        //                {
        //                    if (tDecimal.ToString().Length == 4)
        //                    {

        //                        switch (tDecimal.ToString().Substring(3, 1))
        //                        {

        //                            case "0":
        //                            case "1":
        //                            case "2":
        //                            case "3":
        //                            case "4":
        //                                {
        //                                    // secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
        //                                    lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
        //                                    break;
        //                                }

        //                        }
        //                    }
        //                    else
        //                    {
        //                        //  MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
        //                    }
        //                }
        //                else
        //                {
        //                    //      MessageBox.Show(tDecimal.ToString().Substring(3, 1));
        //                    if (tDecimal.ToString().Length == 4)
        //                    {
        //                        switch (tDecimal.ToString().Substring(3, 1))
        //                        {
        //                            case "0":
        //                            case "1":
        //                            case "2":
        //                            case "3":
        //                            case "4":
        //                                {

        //                                    lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
        //                                    break;
        //                                }
        //                            case "5":
        //                            case "6":
        //                            case "7":
        //                            case "8":
        //                            case "9":
        //                                {
        //                                    secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
        //                                    lblNetAmt.Content = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
        //                                    break;
        //                                }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //   MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MyMessageBox.ShowBox(ex.Message, "Warning");
        //    }
        //}
        DataTable dtFinal = new DataTable();
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (_Class.clsVariables.UserType != "1")
                {
                    funReturn();
                    for (int j = 0; j < gridDisplay.Rows.Count; j++)
                    {
                        if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                        {
                            gridDisplay.Rows[j].ReadOnly = true;
                            // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                            gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    funCalculate("LOAD");
                }
                else
                {
                    try
                    {
                        if (_Class.clsVariables.tAllowReturn == true)
                        {
                            funReturn();
                            for (int j = 0; j < gridDisplay.Rows.Count; j++)
                            {
                                if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                                {
                                    gridDisplay.Rows[j].ReadOnly = true;
                                    // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                                    gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                                }
                            }

                            funCalculate("LOAD");
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please, get user rights to Return this Item!!", "Warning");
                        }
                        //frmKeyBoard frm = new frmKeyBoard();
                        //_Class.clsVariables.tVoidActionType = "PASSWORD";
                        //if (_Class.clsVariables.tVoidActionType == "PASSWORD")
                        //{
                        //    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEventPassword1);
                        //    frm.ShowDialog();
                        //    txtEnterValue.Focus();
                        //    txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                        //}
                    }
                    catch (Exception ex)
                    {
                        MyMessageBox.ShowBox(ex.Message);
                    }
                }
                funLoad();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }


        }
        public void funReturn()
        {
            try
            {
                // if (gridItems.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("sp_SalesReturn", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tOldBillNo", lblBillNo.Content);
                    cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt.Content.ToString()));
                    cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt.Content.ToString()));
                    //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                    cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTotAmt.Content.ToString()));
                    cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                    cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    double tot = 0;
                    if (_Class.clsVariables.tempGDisplayTaxType == "NoTax" || _Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                    {
                        tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString())));
                    }
                    else
                    {
                        tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                    }
                    cmd.Parameters.AddWithValue("@RoundValue", tot);
                    dtFinal.Rows.Clear();
                    if (dtChkGrid.Rows.Count != dt.Rows.Count)
                    {
                        //bool isRowChk = false;
                        for (int i = 0; i < dtRemove.Rows.Count; i++)
                        {
                            string iname = dtRemove.Rows[i]["ItemName"].ToString().Trim();
                            int ni = iname.IndexOf("-");
                            if (ni != -1)
                                iname = iname.Substring(0, ni);
                            dtFinal.Rows.Add(iname.Trim(), "0", dtRemove.Rows[i]["Rate"].ToString().Trim(), "0.00", dtRemove.Rows[i]["Id"].ToString().Trim(), Convert.ToString(dtRemove.Rows[i]["Disc"]), Convert.ToString(dtRemove.Rows[i]["SDisc"]), Convert.ToString(dtRemove.Rows[i]["Other"]));
                        }
                    }
                    for (int i = 0; i < dtChkGrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string iname = dtChkGrid.Rows[i]["ItemName"].ToString();
                            int ni = iname.IndexOf("-");
                            if (ni != -1)
                                iname = iname.Substring(0, ni);

                            string jname = dt.Rows[j]["ItemName"].ToString();
                            int nj = jname.IndexOf("-");
                            if (nj != -1)
                                jname = jname.Substring(0, nj);

                            if (iname.Trim() == jname.Trim() && dtChkGrid.Rows[i]["Id"].ToString().Trim() == dt.Rows[j]["Id"].ToString().Trim())
                            {
                                if (dtChkGrid.Rows[i]["Qty"].ToString().Trim() != dt.Rows[j]["Qty"].ToString().Trim() || dtChkGrid.Rows[i]["Rate"].ToString().Trim() != dt.Rows[j]["Rate"].ToString().Trim() || dtChkGrid.Rows[i]["Amt"].ToString().Trim() != dt.Rows[j]["Amt"].ToString().Trim())
                                {
                                    string xname = dt.Rows[j]["ItemName"].ToString();
                                    int nx = xname.IndexOf("-");
                                    if (nx != -1)
                                        xname = xname.Substring(0, ni);
                                    dtFinal.Rows.Add(xname.Trim(), dt.Rows[j]["Qty"].ToString().Trim(), dt.Rows[j]["Rate"].ToString().Trim(), dt.Rows[j]["Amt"].ToString().Trim(), dt.Rows[j]["Id"].ToString().Trim(), "0.00", "0.00", string.Format("{0:0.00}", (Convert.ToDouble(Convert.ToString(dtChkGrid.Rows[i]["Other"])) - Convert.ToDouble(Convert.ToString(dt.Rows[j]["Other"])))));
                                    break;

                                }
                            }
                        }
                    }
                    cmd.Parameters.AddWithValue("@tempTable", dtFinal);
                    if (dtFinal.Rows.Count > 0)
                    {
                        cmd.ExecuteNonQuery();
                        _Class.clsVariables.LoadPreviousBill = "LoadOnce";
                    }
                    gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
                    dt.Clear();
                    lblNetAmt.Content = "0.00";
                    lblDiscount.Content = "0.00";
                    lblTotQty.Content = "0.00";
                    lblTotAmt.Content = "0.00";
                    lblTaxAmt.Content = "0.00";
                    txtBillNo.Text = "";

                    funLoadValues();
                    //DataTable dtNew = new DataTable();
                    //dtNew.Rows.Clear();
                    //SqlCommand cmd11 = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                    ////cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();
                    //SqlDataAdapter adp = new SqlDataAdapter(cmd11);
                    //adp.Fill(dtNew);

                    //DataTable dtReturnVal = new DataTable();

                    //for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    //{
                    //    dtReturnVal.Rows.Clear();
                    //    SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0)", con);
                    //    cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                    //    SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                    //    adpReturn.Fill(dtReturnVal);
                    //    double tRetValue = 0.0, tNtAmt = 0.0;
                    //    if (dtReturnVal.Rows.Count > 0)
                    //    {
                    //        if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                    //        {
                    //            tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                    //        }
                    //    }
                    //    tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                    //    dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                    //}

                    //gridDisplay.DataSource = dtNew.DefaultView;

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }
        private void btnKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frmKeyBoard frm = new frmKeyBoard();
                if (_Class.clsVariables.tVoidActionType == "ITEMCODE")
                {
                    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                    // frm.ShowDialog();
                    txtBillNo.Focus();
                    txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                }
                if (_Class.clsVariables.tVoidActionType == "BILLNO")
                {
                    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEvent);
                    txtBillNo.Focus();
                    txtBillNo.Select(txtBillNo.Text.Length, 0);
                }
                if (_Class.clsVariables.tVoidActionType == "REMARK")
                {
                    frm.SalesCreationEventHandlerNew += new EventHandler(CloseEvent2);
                    // frm.ShowDialog();
                    txtBillNo.Focus();
                    txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                }

                frm.ShowDialog();
                if (_Class.clsVariables.tVoidActionType == "ITEMCODE")
                {
                    txtEnterValue.Focus();
                    txtEnterValue.Select(txtEnterValue.Text.Length, 0);
                }
                if (_Class.clsVariables.tVoidActionType == "BILLNO")
                {
                    txtBillNo.Focus();
                    txtBillNo.Select(txtBillNo.Text.Length, 0);
                }
                if (_Class.clsVariables.tVoidActionType == "REMARK")
                {
                    txtReason.Focus();
                    txtReason.Select(txtBillNo.Text.Length, 0);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }


        private void CloseEvent2(object sender, EventArgs e)
        {
            txtReason.Text = SalesProject._Class.clsVariables.tVoidValue;
            txtReason.Focus();
            txtReason.Select(txtEnterValue.Text.Length, 0);
        }
        private void CloseEvent1(object sender, EventArgs e)
        {
            txtEnterValue.Text = SalesProject._Class.clsVariables.tVoidValue;
            txtEnterValue.Focus();
            txtEnterValue.Select(txtEnterValue.Text.Length, 0);
        }
        private void CloseEvent(object sender, EventArgs e)
        {
            tTenderClose = "Close";
            txtBillNo.Text = SalesProject._Class.clsVariables.tVoidValue;
            txtBillNo.Focus();
            txtBillNo.Select(txtBillNo.Text.Length, 0);
        }
        private void txtBillNo_LostFocus(object sender, RoutedEventArgs e)
        {
            _Class.clsVariables.tVoidActionType = "BILLNO";
        }

        private void txtEnterValue_LostFocus(object sender, RoutedEventArgs e)
        {
            _Class.clsVariables.tVoidActionType = "ITEMCODE";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // this.Close();
            this.Visibility = Visibility.Hidden;
            if (UCfrmVoidEvent_CloseClick != null)
            {
                UCfrmVoidEvent_CloseClick();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                funPrevPrint();
                mainStr = "";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox("Warning!");
            }
        }


        ReportViewer rpt = new ReportViewer();
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales1 = new Microsoft.Reporting.WinForms.ReportViewer();

        public void funPrevPrint()
        {
            try
            {

                DateTime tBillDate = new DateTime();
                DateTime tBillTime = new DateTime();
                string tBillNo = "";
                double @tNetAmt = 0;
                double @tTotOriginalAmt = 0;
                Double @Rate = 0;
                double @tTotAmt = 0;
                double @tTotQty = 0;
                double @tTotTax = 0;
                double @Qty = 0;
                double @Amt = 0, @tTaxCalAmt = 0, @tTax = 0;
                double tDiscount = 0.00;
                double tRefund = 0.00;
                double tTotal = 0.00;
                string tBillType = "";
                string tCounterNameNew = "";
                string tUserNameNew = "";


                string HCLedgerName = "", HCAddress1 = "", HCAddress2 = "", HCAddress3 = "", HCAddress4 = "", HCAddress5 = "";
                DataTable dtAcProcess = new DataTable();


                DataTable dtPrevBillMas = new DataTable();
                DataTable dtDetail = new DataTable();
                DataTable dtDetail1 = new DataTable();

                dtPrevBillMas.Rows.Clear();
                SqlCommand cmdBillNo = new SqlCommand("select Smas_no,CONVERT(date,smas_billdate,108) as BillDate, CONVERT(time,smas_billtime,103)as BillTime,smas_billno, Smas_name, Ctr_name, User_name from salmas_table, Counter_table, User_table where user_table.user_no=salMas_table.Userno and counter_table.ctr_no=salmas_table.ctr_no and smas_billno=@tBillNo and smas_rtno=0", con);
                cmdBillNo.Parameters.AddWithValue("@tBillNo", (txtBillNo.Text.Trim()));
                SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                adpBillNo.Fill(dtPrevBillMas);
                if (dtPrevBillMas.Rows.Count > 0)
                {
                    tBillDate = DateTime.Parse(dtPrevBillMas.Rows[0]["BillDate"].ToString());
                    tBillTime = DateTime.Parse(dtPrevBillMas.Rows[0]["BillTime"].ToString());
                    tBillType = dtPrevBillMas.Rows[0]["Smas_name"].ToString();
                    double code = double.Parse(dtPrevBillMas.Rows[0]["smas_billno"].ToString());
                    tCounterNameNew = Convert.ToString(dtPrevBillMas.Rows[0]["Ctr_name"]);
                    tUserNameNew = Convert.ToString(dtPrevBillMas.Rows[0]["User_name"]);
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

                    SqlCommand cmdBillDet1 = new SqlCommand(@"SELECT  (case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+stktrn_table.Serial_No end) as Item_name,stktrn_table.strn_sno,dbo.stktrn_table.nt_qty,stktrn_table.unit_no,unit_table.unit_alias FROM unit_table,  dbo.stktrn_table INNER JOIN dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where unit_table.unit_no=stktrn_table.unit_no and stktrn_table.strn_no=@tBillNo and strn_type=1", con);


                    //                    SqlCommand cmdBillDet1 = new SqlCommand(@"SELECT  dbo.Item_table.Item_name,stktrn_table.strn_sno,dbo.stktrn_table.nt_qty,stktrn_table.unit_no,unit_table.unit_alias
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

                    //                    SqlCommand cmdBillDet = new SqlCommand(@"SELECT  dbo.Item_table.Item_name, (dbo.stktrn_table.nt_qty-dbo.stktrn_table.rnt_qty) as nt_qty,convert(numeric(18,2), dbo.stktrn_table.Rate),convert(numeric(18,2),((dbo.stktrn_table.nt_qty-dbo.stktrn_table.rnt_qty)*stktrn_table.Rate)) as Amt, convert(numeric(18,2),dbo.stktrn_table.Amount)  
                    //                     FROM  dbo.stktrn_table INNER JOIN
                    //                     dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where stktrn_table.strn_no=@tBillNo and strn_type=1", con);
                    //                    cmdBillDet.Parameters.AddWithValue("@tBillNo", dtPrevBillMas.Rows[0]["smas_no"].ToString());
                    //                    SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                    //                    adpBillDet.Fill(dtDetail);


                    dtDetail.Rows.Clear();
                    SqlCommand cmd2 = new SqlCommand(@"select (case when len(stktrn_table.Serial_No)=0 OR stktrn_table.Serial_No is NULL then Item_table.Item_name else Item_table.Item_name+" + "'-'" + "+stktrn_table.Serial_No end) as Item_name,(stktrn_table.nt_qty-stktrn_table.rnt_qty)  as nt_qty,convert(numeric(18,2),stktrn_table.Rate) ,convert(numeric(18,2),((stktrn_table.nt_qty-stktrn_table.rnt_qty)*stktrn_table.Rate)) as Amt, convert(numeric(18,2),stktrn_table.Rate) from stktrn_table,Item_table where stktrn_table.item_no=Item_table.Item_no and  stktrn_table.strn_no=(select smas_no from salmas_table where smas_billno=@tBillNo and Smas_rtno=0)   and stktrn_table.nt_qty<>stktrn_table.rnt_qty", con);
                    cmd2.Parameters.AddWithValue("@tBillNo", txtBillNo.Text.Trim());
                    SqlDataAdapter adap = new SqlDataAdapter(cmd2);
                    adap.Fill(dtDetail);


                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        @Qty = 0;
                        @Rate = 0;
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
                        if (@Qty > 0)
                        {
                            @tTotOriginalAmt = @tTotOriginalAmt + (@Qty * @Rate);
                            @tTotQty = @tTotQty + @Qty;
                            @tTotAmt = @tTotAmt + @Amt;
                            DataTable stNew = new DataTable();
                            stNew.Rows.Clear();
                            SqlCommand cmd = new SqlCommand("Select Nt_percent from Tax_Table where Tax_no=(Select Tax_no from item_table where Item_name=@ItemName)", con);
                            cmd.Parameters.AddWithValue("@ItemName", dtDetail.Rows[i][0].ToString());
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(stNew);
                            if (stNew.Rows.Count > 0)
                            {
                                if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                                {
                                    @tTax = @tTaxCalAmt - ((@tTaxCalAmt * 100) / (100 + Convert.ToDouble(stNew.Rows[0][0].ToString())));
                                }
                                else if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                                {
                                    @tTax = (@tTaxCalAmt * Convert.ToDouble(stNew.Rows[0][0].ToString())) / 100;
                                }
                                else
                                {
                                    @tTax = 0;
                                    //  @tTax = (@Amt * double.Parse(stNew.Rows[0][0].ToString())) / 100;                                  
                                }
                                @tTotTax = @tTotTax + @tTax;
                            }
                        }
                    }
                    DataTable dtDiscount = new DataTable();
                    dtDiscount.Rows.Clear();
                    SqlCommand cmdDiscount = new SqlCommand(@"Select SUM(Disc_Amt+Othdisc_Amt+spl_discamt) as Amount from stktrn_table where strn_no in (
Select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0 and smas_Cancel=0)", con);
                    cmdDiscount.Parameters.AddWithValue("@tBillNo", code);
                    SqlDataAdapter adpDiscount = new SqlDataAdapter(cmdDiscount);
                    adpDiscount.Fill(dtDiscount);
                    if (dtDiscount.Rows.Count > 0)
                    {
                        if (!DBNull.Value.Equals(dtDiscount.Rows[0]["Amount"].ToString()))
                        {
                            tDiscount = 0;
                        }
                        else
                        {
                            tDiscount = double.Parse(dtDiscount.Rows[0]["Amount"].ToString());
                        }
                    }
                    if (_Class.clsVariables.tempGDisplayTaxType == "Inclusive")
                    {
                        @tNetAmt = (@tTotAmt) - tDiscount;
                    }
                    else if (_Class.clsVariables.tempGDisplayTaxType == "Exclusive")
                    {
                        @tNetAmt = (@tTotTax + @tTotAmt) - tDiscount;
                    }
                    else
                    {
                        @tNetAmt = (@tTotTax + @tTotAmt) - tDiscount;
                    }
                    lblRefund.Content = string.Format("{0:0.00}", @tNetAmt);
                    funRoundCalculate1();
                    DataTable dtRefund = new DataTable();
                    dtRefund.Rows.Clear();
                    SqlCommand cmdRefund = new SqlCommand("select convert(numeric(18,2), (case WHEN (sum(SalRecv_Amt+SalRecv_Refund) is Null) THEN '0.00' ELSE sum(SalRecv_Amt+SalRecv_Refund) END)) as Amount from SalRecv_table where SalRecv_salno=@tBillNo", con);
                    cmdRefund.Parameters.AddWithValue("@tBillNo", dtPrevBillMas.Rows[0]["smas_Billno"].ToString());
                    SqlDataAdapter adpRefund = new SqlDataAdapter(cmdRefund);
                    adpRefund.Fill(dtRefund);
                    if (dtRefund.Rows.Count > 0)
                    {
                        tRefund = double.Parse(dtRefund.Rows[0]["Amount"].ToString());
                    }
                    //  lblRefund.Content =string.Format("{0:0.00}",tRefund)
                    tTotal = tRefund - double.Parse(lblNetAmt.Content.ToString());
                }
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

                //new printing coding start

                DataTable dtPrinterItemName = new DataTable();
                mainStr = null;

                if (dtDetail.Rows.Count > 0)
                {
                    if (_Class.clsVariables.tPrintImageEnable.Trim() == "Yes")
                    {
                        ImagePrintMain.funImagePrintMain();
                    }
                    if (isCancel == "Cancel")
                    {
                        for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                        {
                            if (dtPrint.Rows[i8]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i8]["Property"].ToString();

                                topLine1 = "*** VOID ***";
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
                                break;
                            }

                        }
                    }

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
                            break;
                        }
                    }

                    //top design start
                    string tHeaderAlign = "Yes";
                    for (int i1 = 0; i1 < dtPrint.Rows.Count - 1; i1++)
                    {
                        if (dtPrint.Rows[i1]["Describ"].ToString().Trim() == "Receipt Header Left Align")
                        {
                            tHeaderAlign = dtPrint.Rows[i1]["Property"].ToString();
                            break;
                        }
                    }
                    if (tHeaderAlign == "Yes")
                    {
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
                                        }
                                    }
                                }
                            }



                        }
                    }
                    else
                    {
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
                                            break;
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
                                            break;
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
                                                break;
                                            }
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
                                                break;
                                            }
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
                                            break;
                                        }
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
                            break;
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

                                string tChk = "Bill Date:" + tBillDate.ToString("dd/MM/yyyy");
                                mainStr += "Bill Date:" + tBillDate.ToString("dd/MM/yyyy");
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
                                            mainStr += "Time:" + tBillTime.ToShortTimeString();
                                        }
                                        else
                                        {
                                            for (int j = 0; j < 13; j++)
                                            {
                                                mainStr += " ";
                                            }
                                        }
                                        mainStr += "\n";
                                        break;
                                    }
                                }
                                break;
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
                                string temp = dtPrint.Rows[i3]["Property"].ToString() + tBillNo;
                                mainStr += temp;
                                for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                break;
                            }
                        }
                    }

                    //Counter Name
                    //for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
                    //{
                    //    if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                    //    {
                    //        charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                    //    }


                    //    if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Print Counter Name")
                    //    {
                    //        if (dtPrint.Rows[i3]["Property"].ToString() != "Yes")
                    //        {
                    //            //  string temp = _Class.clsVariables.tCounterName;
                    //            string temp = tCounterNameNew;
                    //            mainStr += temp;
                    //            for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
                    //            {
                    //                mainStr += " ";
                    //            }
                    //            mainStr += "\n";
                    //            break;
                    //        }
                    //    }
                    //}

                    //UserName
                    //for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
                    //{
                    //    if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                    //    {
                    //        charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                    //    }


                    //    if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Print User Name")
                    //    {
                    //        if (dtPrint.Rows[i3]["Property"].ToString() != "Yes")
                    //        {
                    //            // string temp = _Class.clsVariables.tUserName;
                    //            string temp = tUserNameNew;
                    //            mainStr += temp;
                    //            for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
                    //            {
                    //                mainStr += " ";
                    //            }
                    //            mainStr += "\n";
                    //            break;
                    //        }
                    //    }
                    //}

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
                            break;
                        }
                    }
                    // For House Acc Address munies code

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


                    //for (int i5 = 0; i5 < dtPrint.Rows.Count - 1; i5++)
                    //{
                    //if (dtPrint.Rows[i5]["Describ"].ToString() == "Characters Per Line*")
                    //{
                    //    charPerLine = dtPrint.Rows[i5]["Property"].ToString();
                    //}

                    // double location = 0.00;

                    string tempStr = null;

                    mainStr1 = mainStr;

                    if (_Class.clsVariables.tempGPrintQunatityandRate == "Yes" && _Class.clsVariables.tempGPrintURate == "Yes")
                    {
                        string tQtyHeading = "";
                        tQtyHeading = "Particulars";
                        //  mainStr += tQtyHeading;
                        double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 22));
                        for (int j = 0; j < chkCount; j++)
                        {
                            tQtyHeading += " ";
                        }
                        tQtyHeading += "Qty    ";
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
                                            SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
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
                                            string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
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



                                            //string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
                                            ////    MessageBox.Show(temp);
                                            //int chkSpace = temp.LastIndexOf(" ");
                                            //int loc = (temp.Length - temp.LastIndexOf(" "));
                                            ////   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                                            //if (chkSpace != -1)
                                            //{
                                            //    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                                            //    //   MessageBox.Show(mainStr.ToString());
                                            //    for (int j = 0; j < loc + 18; j++)
                                            //    {
                                            //        mainStr += " ";
                                            //    }
                                            //    mainStr += "\n";
                                            //    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                                            //    // mainStr += temp1;
                                            //    if (temp1.Length <= (int)findCenterPosition)
                                            //    {
                                            //        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    //Without Space Prev Code
                                            //    mainStr += temp.ToString();
                                            //    mainStr += "\n";
                                            //    string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                                            //    // mainStr += temp1;
                                            //    if (temp1.Length <= (int)findCenterPosition)
                                            //    {
                                            //        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                                            //    }
                                            //}
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
                                                    for (int j = 0; j < (findCenterPosition / 2); j++)
                                                    {
                                                        mainStr += " ";
                                                    }
                                                    mainStr += tempStr;
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
                                                    mainStr += tempStr;
                                                    for (int j = 0; j < (findCenterPosition / 2); j++)
                                                    {
                                                        mainStr += " ";
                                                    }
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
                        //break;
                    }
                    else if (_Class.clsVariables.tempGPrintQunatityandRate == "No" && _Class.clsVariables.tempGPrintURate == "Yes")
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



                        for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                        //foreach (DataRow row in dgsales.Rows)
                        {
                            // object[] array = row.ItemArray;

                            for (int i = 0; i < 4; i++)
                            {
                                tempStr = dtDetail.Rows[mn][i].ToString();
                                if (i == 0)
                                {
                                    if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                                    {
                                        dtPrinterItemName.Rows.Clear();
                                        SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
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
                                findCenterPosition = (double.Parse(charPerLine) - 18);
                                if (i == 0)
                                {

                                    if (tempStr.Length <= (int)findCenterPosition)
                                    {
                                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                    }
                                    else
                                    {
                                        string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
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
                                    //if (tempStr.Length < 4)
                                    //{
                                    //    mainStr += tempStr.PadRight(3, ' ');
                                    //}
                                }
                                if (i == 2)
                                {
                                    mainStr += "       ";
                                    //if (tempStr.Length <= 7)
                                    //{
                                    //    mainStr += tempStr.PadLeft(7, ' ');
                                    //}
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
                        // break;
                    }
                    else if (_Class.clsVariables.tempGPrintQunatityandRate == "No" && _Class.clsVariables.tempGPrintURate == "No" || _Class.clsVariables.tempGPrintQunatityandRate == "Yes" && _Class.clsVariables.tempGPrintURate == "No")
                    {
                        string tQtyHeading = "";
                        tQtyHeading = "Particulars";
                        mainStr += tQtyHeading;
                        double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));
                        for (int j = 0; j < tQtyCount; j++)
                        {
                            mainStr += " ";
                        }
                        //mainStr += "    ";
                        //mainStr += "       ";
                        //mainStr += "Amount";
                        //mainStr += "\n";

                        //mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                        mainStr += " Qty  ";
                        mainStr += "     ";
                        mainStr += "Amount";
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



                        for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                        //foreach (DataRow row in dgsales.Rows)
                        {
                            // object[] array = row.ItemArray;

                            for (int i = 0; i < 4; i++)
                            {
                                tempStr = dtDetail.Rows[mn][i].ToString();
                                if (i == 0)
                                {
                                    if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                                    {
                                        dtPrinterItemName.Rows.Clear();
                                        SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
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
                                findCenterPosition = (double.Parse(charPerLine) - 18);
                                if (i == 0)
                                {

                                    if (tempStr.Length <= (int)findCenterPosition)
                                    {
                                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                    }
                                    else
                                    {
                                        string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
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
                                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                                {
                                                    mainStr += " ";
                                                }
                                                mainStr += tempStr;
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
                                                mainStr += tempStr;
                                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                                {
                                                    mainStr += " ";
                                                }
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
                                    //if (tempStr.Length <= 7)
                                    //{
                                    //    mainStr += tempStr.PadLeft(7, ' ');
                                    //}
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


                    // if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Qunatity and Rate")
                    // {
                    //if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    //{
                    //    string tQtyHeading = "";
                    //    tQtyHeading = "Particulars";
                    //    //  mainStr += tQtyHeading;
                    //    double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 22));
                    //    for (int j = 0; j < chkCount; j++)
                    //    {
                    //        tQtyHeading += " ";
                    //    }
                    //    tQtyHeading += "  Qty  ";
                    //    tQtyHeading += "U/Rate ";
                    //    tQtyHeading += " Amount";
                    //    mainStr += tQtyHeading;
                    //    mainStr += "\n";
                    //    for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                    //    {
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                    //        {
                    //            charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                    //        }

                    //        // print lint below logo
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                    //        {
                    //            lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    //            if (lineBelowLogo == "No Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += " ";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            if (lineBelowLogo == "Single Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "-";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            else if (lineBelowLogo == "Double Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "=";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //        }
                    //    }

                    //    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //    //foreach (DataRow row in dgsales.Rows)
                    //    {
                    //        // object[] array = dgsales.Rows[mn].;
                    //        bool isChk = false;
                    //        for (int z = 0; z < 4; z++)
                    //        {
                    //            if (dtDetail.Rows[mn][z].ToString().Trim() == "")
                    //            {
                    //                isChk = true;
                    //                break;
                    //            }
                    //        }
                    //        if (isChk == false)
                    //        {
                    //            for (int i = 0; i < 4; i++)
                    //            {
                    //                tempStr = dtDetail.Rows[mn][i].ToString();

                    //                if (i == 0)
                    //                {
                    //                    if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                    //                    {
                    //                        dtPrinterItemName.Rows.Clear();
                    //                        SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
                    //                        cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                    //                        SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                    //                        adpPrinterName.Fill(dtPrinterItemName);

                    //                        if (dtPrinterItemName.Rows.Count > 0)
                    //                        {
                    //                            tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                    //                        }
                    //                    }
                    //                }
                    //                //  MessageBox.Show(tempStr.Length.ToString());
                    //                findCenterPosition = (double.Parse(charPerLine) - 22);
                    //                if (i == 0)
                    //                {
                    //                    if (tempStr.Length <= (int)findCenterPosition)
                    //                    {
                    //                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                    }
                    //                    else
                    //                    {
                    //                        string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                    //                        //    MessageBox.Show(temp);
                    //                        int chkSpace = temp.LastIndexOf(" ");
                    //                        int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                        //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //                        if (chkSpace != -1)
                    //                        {
                    //                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                            //   MessageBox.Show(mainStr.ToString());
                    //                            for (int j = 0; j < loc; j++)
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
                    //                            else
                    //                            {
                    //                                mainStr += temp1.Substring(0, (int)findCenterPosition);
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            //Without Space Prev Code
                    //                            mainStr += temp.ToString();
                    //                            mainStr += "\n";
                    //                            string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                    //                            // mainStr += temp1;
                    //                            if (temp1.Length <= (int)findCenterPosition)
                    //                            {
                    //                                mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                            }
                    //                            else
                    //                            {
                    //                                mainStr += temp1.Substring(0, (int)findCenterPosition);
                    //                            }
                    //                        }



                    //                        //string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
                    //                        ////    MessageBox.Show(temp);
                    //                        //int chkSpace = temp.LastIndexOf(" ");
                    //                        //int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                        ////   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //                        //if (chkSpace != -1)
                    //                        //{
                    //                        //    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                        //    //   MessageBox.Show(mainStr.ToString());
                    //                        //    for (int j = 0; j < loc + 18; j++)
                    //                        //    {
                    //                        //        mainStr += " ";
                    //                        //    }
                    //                        //    mainStr += "\n";
                    //                        //    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //                        //    // mainStr += temp1;
                    //                        //    if (temp1.Length <= (int)findCenterPosition)
                    //                        //    {
                    //                        //        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        //    }
                    //                        //}
                    //                        //else
                    //                        //{
                    //                        //    //Without Space Prev Code
                    //                        //    mainStr += temp.ToString();
                    //                        //    mainStr += "\n";
                    //                        //    string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                    //                        //    // mainStr += temp1;
                    //                        //    if (temp1.Length <= (int)findCenterPosition)
                    //                        //    {
                    //                        //        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        //    }
                    //                        //}
                    //                    }
                    //                }

                    //                if (i == 1)
                    //                {
                    //                    if (tempStr.Length < 8)
                    //                    {
                    //                        if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                    //                        {
                    //                            findCenterPosition = (7 - tempStr.Length);
                    //                            if (findCenterPosition % 2 == 0)
                    //                            {
                    //                                for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                                mainStr += tempStr;
                    //                                for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                            }
                    //                            else
                    //                            {
                    //                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                                mainStr += tempStr;
                    //                                for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            mainStr += tempStr.PadRight(7, ' ');
                    //                        }
                    //                    }
                    //                }
                    //                if (i == 2)
                    //                {
                    //                    // mainStr += tempStr.PadRight(7, ' ');
                    //                    if (tempStr.Length <= 7)
                    //                    {
                    //                        mainStr += tempStr.PadLeft(7, ' ');
                    //                    }
                    //                }
                    //                if (i == 3)
                    //                {
                    //                    if (tempStr.Length <= 8)
                    //                    {
                    //                        mainStr += tempStr.PadLeft(8, ' ');
                    //                    }
                    //                }
                    //                // tPrintText += tempStr;
                    //            }
                    //            mainStr += "\n";
                    //        }
                    //    }
                    //    break;
                    //}
                    //else
                    //{
                    //    string tQtyHeading = "";
                    //    tQtyHeading = "Particulars";
                    //    mainStr += tQtyHeading;
                    //    double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));
                    //    for (int j = 0; j < tQtyCount; j++)
                    //    {
                    //        mainStr += " ";
                    //    }
                    //    mainStr += "    ";
                    //    mainStr += "       ";
                    //    mainStr += "Amount";
                    //    mainStr += "\n";
                    //    for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                    //    {
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                    //        {
                    //            charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                    //        }

                    //        // print lint below logo
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                    //        {
                    //            lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    //            if (lineBelowLogo == "No Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += " ";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            if (lineBelowLogo == "Single Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "-";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            else if (lineBelowLogo == "Double Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "=";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //        }
                    //    }



                    //    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //    //foreach (DataRow row in dgsales.Rows)
                    //    {
                    //        // object[] array = row.ItemArray;

                    //        for (int i = 0; i < 4; i++)
                    //        {
                    //            tempStr = dtDetail.Rows[mn][i].ToString();
                    //            if (i == 0)
                    //            {
                    //                if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                    //                {
                    //                    dtPrinterItemName.Rows.Clear();
                    //                    SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
                    //                    cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                    //                    SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                    //                    adpPrinterName.Fill(dtPrinterItemName);

                    //                    if (dtPrinterItemName.Rows.Count > 0)
                    //                    {
                    //                        tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                    //                    }
                    //                }
                    //            }
                    //            //  MessageBox.Show(tempStr.Length.ToString());
                    //            findCenterPosition = (double.Parse(charPerLine) - 18);
                    //            if (i == 0)
                    //            {

                    //                if (tempStr.Length <= (int)findCenterPosition)
                    //                {
                    //                    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                }
                    //                else
                    //                {
                    //                    string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                    //                    //    MessageBox.Show(temp);
                    //                    int chkSpace = temp.LastIndexOf(" ");
                    //                    int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                    //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //                    if (chkSpace != -1)
                    //                    {
                    //                        mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                        //   MessageBox.Show(mainStr.ToString());
                    //                        for (int j = 0; j < loc; j++)
                    //                        {
                    //                            mainStr += " ";
                    //                        }
                    //                        mainStr += "\n";
                    //                        string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //                        // mainStr += temp1;
                    //                        if (temp1.Length <= (int)findCenterPosition)
                    //                        {
                    //                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        //Without Space Prev Code
                    //                        mainStr += temp.ToString();
                    //                        mainStr += "\n";
                    //                        string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                    //                        // mainStr += temp1;
                    //                        if (temp1.Length <= (int)findCenterPosition)
                    //                        {
                    //                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        }
                    //                    }


                    //                }

                    //                //////if (tempStr.Length <= (int)findCenterPosition)
                    //                //////{
                    //                //////    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                //////}
                    //                //////else
                    //                //////{
                    //                //////    string temp = tempStr.Substring(0, (int)findCenterPosition);
                    //                //////    int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                //////    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                //////    for (int j = 0; j < loc + 18; j++)
                    //                //////    {
                    //                //////        mainStr += " ";
                    //                //////    }
                    //                //////    mainStr += "\n";
                    //                //////    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //                //////    mainStr += temp1;
                    //                //////    if (temp1.Length <= (int)findCenterPosition)
                    //                //////    {
                    //                //////        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                //////    }
                    //                //////}
                    //                //if (tempStr.Length <= (int)findCenterPosition)
                    //                //{
                    //                //    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                //}
                    //            }
                    //            if (i == 1)
                    //            {
                    //                mainStr += "   ";
                    //                //if (tempStr.Length < 4)
                    //                //{
                    //                //    mainStr += tempStr.PadRight(3, ' ');
                    //                //}
                    //            }
                    //            if (i == 2)
                    //            {
                    //                mainStr += "       ";
                    //                //if (tempStr.Length <= 7)
                    //                //{
                    //                //    mainStr += tempStr.PadLeft(7, ' ');
                    //                //}
                    //            }
                    //            if (i == 3)
                    //            {
                    //                if (tempStr.Length <= 8)
                    //                {
                    //                    mainStr += tempStr.PadLeft(8, ' ');
                    //                }
                    //            }
                    //            // tPrintText += tempStr;
                    //        }
                    //        mainStr += "\n";
                    //    }
                    //    break;
                    //}
                    //  }




                    // To print on or off Unit Rate

                    // mainStr = "";
                    //  mainStr = mainStr1;
                    //if ( == "Yes")                
                    //  if (dtPrint.Rows[i5]["Describ"].ToString() == "Print URate")
                    //  {
                    //if (_Class.clsVariables.tempGPrintURate == "Yes")
                    //{
                    //    string tQtyHeading = "";
                    //    tQtyHeading = "Particulars";
                    //    //  mainStr += tQtyHeading;
                    //    double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 22));
                    //    for (int j = 0; j < chkCount; j++)
                    //    {
                    //        tQtyHeading += " ";
                    //    }
                    //    tQtyHeading += "  Qty  ";
                    //    tQtyHeading += "U/Rate ";
                    //    tQtyHeading += " Amount";
                    //    mainStr += tQtyHeading;
                    //    mainStr += "\n";
                    //    for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                    //    {
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                    //        {
                    //            charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                    //        }

                    //        // print lint below logo
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                    //        {
                    //            lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    //            if (lineBelowLogo == "No Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += " ";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            if (lineBelowLogo == "Single Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "-";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            else if (lineBelowLogo == "Double Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "=";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //        }
                    //    }

                    //    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //    //foreach (DataRow row in dgsales.Rows)
                    //    {
                    //        // object[] array = dgsales.Rows[mn].;
                    //        bool isChk = false;
                    //        for (int z = 0; z < 4; z++)
                    //        {
                    //            if (dtDetail.Rows[mn][z].ToString().Trim() == "")
                    //            {
                    //                isChk = true;
                    //                break;
                    //            }
                    //        }
                    //        if (isChk == false)
                    //        {
                    //            for (int i = 0; i < 4; i++)
                    //            {
                    //                tempStr = dtDetail.Rows[mn][i].ToString();

                    //                if (i == 0)
                    //                {
                    //                    if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                    //                    {
                    //                        dtPrinterItemName.Rows.Clear();
                    //                        SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
                    //                        cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                    //                        SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                    //                        adpPrinterName.Fill(dtPrinterItemName);

                    //                        if (dtPrinterItemName.Rows.Count > 0)
                    //                        {
                    //                            tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                    //                        }
                    //                    }
                    //                }
                    //                //  MessageBox.Show(tempStr.Length.ToString());
                    //                findCenterPosition = (double.Parse(charPerLine) - 22);
                    //                if (i == 0)
                    //                {
                    //                    if (tempStr.Length <= (int)findCenterPosition)
                    //                    {
                    //                        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                    }
                    //                    else
                    //                    {
                    //                        string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                    //                        //    MessageBox.Show(temp);
                    //                        int chkSpace = temp.LastIndexOf(" ");
                    //                        int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                        //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //                        if (chkSpace != -1)
                    //                        {
                    //                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                            //   MessageBox.Show(mainStr.ToString());
                    //                            for (int j = 0; j < loc; j++)
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
                    //                            else
                    //                            {
                    //                                mainStr += temp1.Substring(0, (int)findCenterPosition);
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            //Without Space Prev Code
                    //                            mainStr += temp.ToString();
                    //                            mainStr += "\n";
                    //                            string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                    //                            // mainStr += temp1;
                    //                            if (temp1.Length <= (int)findCenterPosition)
                    //                            {
                    //                                mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                            }
                    //                            else
                    //                            {
                    //                                mainStr += temp1.Substring(0, (int)findCenterPosition);
                    //                            }
                    //                        }



                    //                        //string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
                    //                        ////    MessageBox.Show(temp);
                    //                        //int chkSpace = temp.LastIndexOf(" ");
                    //                        //int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                        ////   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //                        //if (chkSpace != -1)
                    //                        //{
                    //                        //    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                        //    //   MessageBox.Show(mainStr.ToString());
                    //                        //    for (int j = 0; j < loc + 18; j++)
                    //                        //    {
                    //                        //        mainStr += " ";
                    //                        //    }
                    //                        //    mainStr += "\n";
                    //                        //    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //                        //    // mainStr += temp1;
                    //                        //    if (temp1.Length <= (int)findCenterPosition)
                    //                        //    {
                    //                        //        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        //    }
                    //                        //}
                    //                        //else
                    //                        //{
                    //                        //    //Without Space Prev Code
                    //                        //    mainStr += temp.ToString();
                    //                        //    mainStr += "\n";
                    //                        //    string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                    //                        //    // mainStr += temp1;
                    //                        //    if (temp1.Length <= (int)findCenterPosition)
                    //                        //    {
                    //                        //        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        //    }
                    //                        //}
                    //                    }
                    //                }

                    //                if (i == 1)
                    //                {
                    //                    if (tempStr.Length < 8)
                    //                    {
                    //                        if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                    //                        {
                    //                            findCenterPosition = (7 - tempStr.Length);
                    //                            if (findCenterPosition % 2 == 0)
                    //                            {
                    //                                for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                                mainStr += tempStr;
                    //                                for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                            }
                    //                            else
                    //                            {
                    //                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                                mainStr += tempStr;
                    //                                for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                                {
                    //                                    mainStr += " ";
                    //                                }
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            mainStr += tempStr.PadRight(7, ' ');
                    //                        }
                    //                    }
                    //                }
                    //                if (i == 2)
                    //                {
                    //                    // mainStr += tempStr.PadRight(7, ' ');
                    //                    if (tempStr.Length <= 7)
                    //                    {
                    //                        mainStr += tempStr.PadLeft(7, ' ');
                    //                    }
                    //                }
                    //                if (i == 3)
                    //                {
                    //                    if (tempStr.Length <= 8)
                    //                    {
                    //                        mainStr += tempStr.PadLeft(8, ' ');
                    //                    }
                    //                }
                    //                // tPrintText += tempStr;
                    //            }
                    //            mainStr += "\n";
                    //        }
                    //    }
                    //    break;
                    //}
                    //else
                    //{
                    //    string tQtyHeading = "";
                    //    tQtyHeading = "Particulars";
                    //    mainStr += tQtyHeading;
                    //    double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));
                    //    for (int j = 0; j < tQtyCount; j++)
                    //    {
                    //        mainStr += " ";
                    //    }
                    //    //mainStr += "    ";
                    //    //mainStr += "       ";
                    //    //mainStr += "Amount";
                    //    //mainStr += "\n";

                    //    //mainStr += "".PadLeft(Convert.ToInt16(tQtyCount), ' ');
                    //    mainStr += " Qty  ";
                    //    mainStr += "     ";
                    //    mainStr += "Amount";
                    //    mainStr += "\n";



                    //    for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                    //    {
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                    //        {
                    //            charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                    //        }

                    //        // print lint below logo
                    //        if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                    //        {
                    //            lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    //            if (lineBelowLogo == "No Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += " ";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            if (lineBelowLogo == "Single Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "-";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //            else if (lineBelowLogo == "Double Line")
                    //            {
                    //                for (int j = 0; j < double.Parse(charPerLine); j++)
                    //                {
                    //                    mainStr += "=";
                    //                }
                    //                mainStr += "\n";
                    //            }
                    //        }
                    //    }



                    //    for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //    //foreach (DataRow row in dgsales.Rows)
                    //    {
                    //        // object[] array = row.ItemArray;

                    //        for (int i = 0; i < 4; i++)
                    //        {
                    //            tempStr = dtDetail.Rows[mn][i].ToString();
                    //            if (i == 0)
                    //            {
                    //                if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                    //                {
                    //                    dtPrinterItemName.Rows.Clear();
                    //                    SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
                    //                    cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                    //                    SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                    //                    adpPrinterName.Fill(dtPrinterItemName);

                    //                    if (dtPrinterItemName.Rows.Count > 0)
                    //                    {
                    //                        tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                    //                    }
                    //                }
                    //            }
                    //            //  MessageBox.Show(tempStr.Length.ToString());
                    //            findCenterPosition = (double.Parse(charPerLine) - 18);
                    //            if (i == 0)
                    //            {

                    //                if (tempStr.Length <= (int)findCenterPosition)
                    //                {
                    //                    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                }
                    //                else
                    //                {
                    //                    string temp = tempStr.Substring(0, (((int)double.Parse(charPerLine)) < tempStr.Length) ? (int)(double.Parse(charPerLine)) : tempStr.Length);
                    //                    //    MessageBox.Show(temp);
                    //                    int chkSpace = temp.LastIndexOf(" ");
                    //                    int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                    //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //                    if (chkSpace != -1)
                    //                    {
                    //                        mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                        //   MessageBox.Show(mainStr.ToString());
                    //                        for (int j = 0; j < loc; j++)
                    //                        {
                    //                            mainStr += " ";
                    //                        }
                    //                        mainStr += "\n";
                    //                        string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //                        // mainStr += temp1;
                    //                        if (temp1.Length <= (int)findCenterPosition)
                    //                        {
                    //                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        //Without Space Prev Code
                    //                        mainStr += temp.ToString();
                    //                        mainStr += "\n";
                    //                        string temp1 = tempStr.Substring((temp.Length), ((tempStr.Length - temp.Length) - 1));
                    //                        // mainStr += temp1;
                    //                        if (temp1.Length <= (int)findCenterPosition)
                    //                        {
                    //                            mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //                        }
                    //                    }


                    //                }

                    //                //////if (tempStr.Length <= (int)findCenterPosition)
                    //                //////{
                    //                //////    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                //////}
                    //                //////else
                    //                //////{
                    //                //////    string temp = tempStr.Substring(0, (int)findCenterPosition);
                    //                //////    int loc = (temp.Length - temp.LastIndexOf(" "));
                    //                //////    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //                //////    for (int j = 0; j < loc + 18; j++)
                    //                //////    {
                    //                //////        mainStr += " ";
                    //                //////    }
                    //                //////    mainStr += "\n";
                    //                //////    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //                //////    mainStr += temp1;
                    //                //////    if (temp1.Length <= (int)findCenterPosition)
                    //                //////    {
                    //                //////        mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                //////    }
                    //                //////}
                    //                //if (tempStr.Length <= (int)findCenterPosition)
                    //                //{
                    //                //    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //                //}
                    //            }
                    //            if (i == 1)
                    //            {
                    //                //mainStr += "   ";
                    //                ////if (tempStr.Length < 4)
                    //                ////{
                    //                ////    mainStr += tempStr.PadRight(3, ' ');
                    //                ////}


                    //                if (tempStr.Length < 8)
                    //                {
                    //                    if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                    //                    {
                    //                        findCenterPosition = (7 - tempStr.Length);
                    //                        if (findCenterPosition % 2 == 0)
                    //                        {
                    //                            for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                            {
                    //                                mainStr += " ";
                    //                            }
                    //                            mainStr += tempStr;
                    //                            for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                            {
                    //                                mainStr += " ";
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                    //                            {
                    //                                mainStr += " ";
                    //                            }
                    //                            mainStr += tempStr;
                    //                            for (int j = 0; j < (findCenterPosition / 2); j++)
                    //                            {
                    //                                mainStr += " ";
                    //                            }
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        mainStr += tempStr.PadRight(7, ' ');
                    //                    }
                    //                }



                    //            }
                    //            if (i == 2)
                    //            {
                    //                mainStr += "   ";
                    //                //if (tempStr.Length <= 7)
                    //                //{
                    //                //    mainStr += tempStr.PadLeft(7, ' ');
                    //                //}
                    //            }
                    //            if (i == 3)
                    //            {
                    //                if (tempStr.Length <= 8)
                    //                {
                    //                    mainStr += tempStr.PadLeft(8, ' ');
                    //                }
                    //            }
                    //            // tPrintText += tempStr;
                    //        }
                    //        mainStr += "\n";
                    //    }
                    //    break;
                    //}






                    //////string tempStr = null;
                    //////if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Qunatity and Rate")
                    //////{
                    //////    if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                    //////    {
                    //////        string tQtyHeading = "";
                    //////        tQtyHeading = "Particulars";
                    //////        //  mainStr += tQtyHeading;
                    //////        double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 22));
                    //////        for (int j = 0; j < chkCount; j++)
                    //////        {
                    //////            tQtyHeading += " ";
                    //////        }
                    //////        tQtyHeading += "  Qty  ";
                    //////        tQtyHeading += "U/Rate ";
                    //////        tQtyHeading += " Amount";
                    //////        mainStr += tQtyHeading;
                    //////        mainStr += "\n";
                    //////        for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                    //////        {
                    //////            if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                    //////            {
                    //////                charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                    //////            }

                    //////            // print lint below logo
                    //////            if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                    //////            {
                    //////                lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    //////                if (lineBelowLogo == "No Line")
                    //////                {
                    //////                    for (int j = 0; j < double.Parse(charPerLine); j++)
                    //////                    {
                    //////                        mainStr += " ";
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////                if (lineBelowLogo == "Single Line")
                    //////                {
                    //////                    for (int j = 0; j < double.Parse(charPerLine); j++)
                    //////                    {
                    //////                        mainStr += "-";
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////                else if (lineBelowLogo == "Double Line")
                    //////                {
                    //////                    for (int j = 0; j < double.Parse(charPerLine); j++)
                    //////                    {
                    //////                        mainStr += "=";
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////            }
                    //////        }

                    //////        for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //////        //foreach (DataRow row in dgsales.Rows)
                    //////        {
                    //////            // object[] array = dgsales.Rows[mn].;
                    //////            double tRemoveid = 0;
                    //////            if (dtDetail.Rows[mn]["nt_qty"].ToString() != "")
                    //////            {
                    //////                tRemoveid = (dtDetail.Rows[mn]["nt_qty"].ToString() == "") ? 0 : double.Parse(dtDetail.Rows[mn]["nt_qty"].ToString());
                    //////            }
                    //////            if (tRemoveid > 0)
                    //////            {
                    //////                bool isChk = false;
                    //////                for (int z = 0; z < 4; z++)
                    //////                {
                    //////                    if (dtDetail.Rows[mn][z].ToString().Trim() == "")
                    //////                    {
                    //////                        isChk = true;
                    //////                        break;
                    //////                    }
                    //////                }
                    //////                if (isChk == false)
                    //////                {
                    //////                    for (int i = 0; i < 4; i++)
                    //////                    {
                    //////                        tempStr = dtDetail.Rows[mn][i].ToString();
                    //////                        //  MessageBox.Show(tempStr.Length.ToString());
                    //////                        findCenterPosition = (double.Parse(charPerLine) - 22);
                    //////                        if (i == 0)
                    //////                        {
                    //////                            if (tempStr.Length <= (int)findCenterPosition)
                    //////                            {
                    //////                                mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //////                            }
                    //////                            else
                    //////                            {
                    //////                                string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
                    //////                                //    MessageBox.Show(temp);
                    //////                                int chkSpace = temp.LastIndexOf(" ");
                    //////                                int loc = (temp.Length - temp.LastIndexOf(" "));
                    //////                                //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                    //////                                if (chkSpace != -1)
                    //////                                {
                    //////                                    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //////                                    //   MessageBox.Show(mainStr.ToString());
                    //////                                    for (int j = 0; j < loc + 18; j++)
                    //////                                    {
                    //////                                        mainStr += " ";
                    //////                                    }
                    //////                                    mainStr += "\n";
                    //////                                    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //////                                    // mainStr += temp1;
                    //////                                    if (temp1.Length <= (int)findCenterPosition)
                    //////                                    {
                    //////                                        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                    //////                                    }
                    //////                                }
                    //////                                else
                    //////                                {
                    //////                                    mainStr += temp.ToString();
                    //////                                }

                    //////                            }
                    //////                        }

                    //////                        if (i == 1)
                    //////                        {
                    //////                            if (tempStr.Length < 8)
                    //////                            {
                    //////                                mainStr += tempStr.PadRight(7, ' ');
                    //////                            }
                    //////                        }
                    //////                        if (i == 2)
                    //////                        {
                    //////                            // mainStr += tempStr.PadRight(7, ' ');
                    //////                            if (tempStr.Length <= 7)
                    //////                            {
                    //////                                mainStr += tempStr.PadLeft(7, ' ');
                    //////                            }
                    //////                        }
                    //////                        if (i == 3)
                    //////                        {
                    //////                            if (tempStr.Length <= 8)
                    //////                            {
                    //////                                mainStr += tempStr.PadLeft(8, ' ');
                    //////                            }
                    //////                        }
                    //////                        // tPrintText += tempStr;
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////            }
                    //////        }

                    //////    }

                    //////    else
                    //////    {
                    //////        string tQtyHeading = "";
                    //////        tQtyHeading = "Particulars";
                    //////        mainStr += tQtyHeading;
                    //////        double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length + 18));
                    //////        for (int j = 0; j < tQtyCount; j++)
                    //////        {
                    //////            mainStr += " ";
                    //////        }
                    //////        mainStr += "    ";
                    //////        mainStr += "       ";
                    //////        mainStr += "Amount";
                    //////        mainStr += "\n";
                    //////        for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                    //////        {
                    //////            if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                    //////            {
                    //////                charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                    //////            }

                    //////            // print lint below logo
                    //////            if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                    //////            {
                    //////                lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                    //////                if (lineBelowLogo == "No Line")
                    //////                {
                    //////                    for (int j = 0; j < double.Parse(charPerLine); j++)
                    //////                    {
                    //////                        mainStr += " ";
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////                if (lineBelowLogo == "Single Line")
                    //////                {
                    //////                    for (int j = 0; j < double.Parse(charPerLine); j++)
                    //////                    {
                    //////                        mainStr += "-";
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////                else if (lineBelowLogo == "Double Line")
                    //////                {
                    //////                    for (int j = 0; j < double.Parse(charPerLine); j++)
                    //////                    {
                    //////                        mainStr += "=";
                    //////                    }
                    //////                    mainStr += "\n";
                    //////                }
                    //////            }
                    //////        }



                    //////        for (int mn = 0; mn < dtDetail.Rows.Count; mn++)
                    //////        //foreach (DataRow row in dgsales.Rows)
                    //////        {
                    //////            // object[] array = row.ItemArray;
                    //////            double tRemoveid = 0;
                    //////            if (dtDetail.Rows[mn]["nt_qty"].ToString() != "")
                    //////            {
                    //////                tRemoveid = (dtDetail.Rows[mn]["nt_qty"].ToString() == "") ? 0 : double.Parse(dtDetail.Rows[mn]["nt_qty"].ToString());
                    //////            }
                    //////            if (tRemoveid > 0)
                    //////            {
                    //////                for (int i = 0; i < 4; i++)
                    //////                {
                    //////                    tempStr = dtDetail.Rows[mn][i].ToString();
                    //////                    //  MessageBox.Show(tempStr.Length.ToString());
                    //////                    findCenterPosition = (double.Parse(charPerLine) - 18);
                    //////                    if (i == 0)
                    //////                    {
                    //////                        if (tempStr.Length <= (int)findCenterPosition)
                    //////                        {
                    //////                            mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //////                        }
                    //////                        else
                    //////                        {
                    //////                            string temp = tempStr.Substring(0, (int)findCenterPosition);
                    //////                            int loc = (temp.Length - temp.LastIndexOf(" "));
                    //////                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                    //////                            for (int j = 0; j < loc + 18; j++)
                    //////                            {
                    //////                                mainStr += " ";
                    //////                            }
                    //////                            mainStr += "\n";
                    //////                            string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                    //////                            mainStr += temp1;
                    //////                            if (temp1.Length <= (int)findCenterPosition)
                    //////                            {
                    //////                                mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //////                            }
                    //////                        }
                    //////                        //if (tempStr.Length <= (int)findCenterPosition)
                    //////                        //{
                    //////                        //    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                    //////                        //}
                    //////                    }
                    //////                    if (i == 1)
                    //////                    {
                    //////                        mainStr += "   ";
                    //////                        //if (tempStr.Length < 4)
                    //////                        //{
                    //////                        //    mainStr += tempStr.PadRight(3, ' ');
                    //////                        //}
                    //////                    }
                    //////                    if (i == 2)
                    //////                    {
                    //////                        mainStr += "       ";
                    //////                        //if (tempStr.Length <= 7)
                    //////                        //{
                    //////                        //    mainStr += tempStr.PadLeft(7, ' ');
                    //////                        //}
                    //////                    }
                    //////                    if (i == 3)
                    //////                    {
                    //////                        if (tempStr.Length <= 8)
                    //////                        {
                    //////                            mainStr += tempStr.PadLeft(8, ' ');
                    //////                        }
                    //////                    }
                    //////                    // tPrintText += tempStr;
                    //////                }
                    //////                mainStr += "\n";
                    //////            }
                    //////        }
                    //////    }
                    //////}

                    //}

                    //  if (_Class.clsVariables.tempGPrintSubtotal== "Print Subtotal")
                    {
                        if (_Class.clsVariables.tempGPrintSubtotal == "Yes")
                        {
                            //for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                            //{
                            //    if (dtPrint.Rows[k]["Describ"].ToString() == "Subtotal")
                            //    {
                            if (true)
                            {
                                lineBelowLogo = "No Line";
                                if (lineBelowLogo == "No Line")
                                {
                                    for (int j = 0; j < double.Parse(charPerLine); j++)
                                    {
                                        mainStr += " ";
                                    }
                                    mainStr += "\n";
                                }
                            }
                            topLine1 = _Class.clsVariables.tempGSubtotal;
                            if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                            {
                                findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));

                                for (int j = 0; j < (findCenterPosition); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;



                                topLine1 = string.Format("{0:0.00}", (@tTotAmt.ToString() == "") ? 0.00 : double.Parse(@tTotAmt.ToString()));
                                for (int j = 0; j < 9 - topLine1.Length; j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                //  +"  3000.00";

                            }

                            mainStr += "\n";
                        }
                        //    }
                        //    //break;
                        //}
                    }


                    if (tDiscount.ToString() != "")
                    {
                        if (tDiscount > 0)
                        {
                            topLine1 = "Discount:";
                            if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                            {
                                findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));

                                for (int j = 0; j < (findCenterPosition); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                topLine1 = string.Format("{0:0.00}", tDiscount);
                                for (int j = 0; j < 9 - topLine1.Length; j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                //  +"  3000.00";
                            }

                            mainStr += "\n";
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
                            break;
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
                                if (_Class.clsVariables.tempGPrintPayThisAmountRightAlign == "Yes")
                                {
                                    //Right Align Code Here
                                    // topLine1 = dtPrint.Rows[k]["Property"].ToString();
                                    topLine1 = dtPrint.Rows[i9]["Property"].ToString();
                                    if (topLine1.Length <= (double.Parse(charPerLine) - 9))
                                    {
                                        findCenterPosition = (double.Parse(charPerLine) - (topLine1.Length + 9));

                                        for (int j = 0; j < (findCenterPosition); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine1;



                                        topLine1 = string.Format("{0:0.00}", double.Parse(lblRefund.Content.ToString()));
                                        for (int j = 0; j < 9 - topLine1.Length; j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += topLine1;
                                        //  +"  3000.00";

                                    }



                                }
                                else
                                {
                                    topLine1 = dtPrint.Rows[i9]["Property"].ToString() + string.Format("{0:0.00}", double.Parse(lblRefund.Content.ToString()));
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
                                    }
                                }
                                mainStr += "\n";
                                break;
                            }
                        }
                    }

                    //Tax Print
                    //  string tPrintTax = "";
                    for (int i101 = 0; i101 < dtPrint.Rows.Count - 1; i101++)
                    {
                        if (dtPrint.Rows[i101]["Describ"].ToString() == "Print Tax")
                        {

                            if (dtPrint.Rows[i101]["Property"].ToString() == "Yes")
                            {
                                for (int i10 = 0; i10 < dtPrint.Rows.Count - 1; i10++)
                                {
                                    if (dtPrint.Rows[i10]["Describ"].ToString() == "Characters Per Line*")
                                    {
                                        charPerLine = dtPrint.Rows[i10]["Property"].ToString();
                                    }

                                    // print lint below logo
                                    if (dtPrint.Rows[i10]["Describ"].ToString() == "Print Line Below Total")
                                    {
                                        lineBelowLogo = "No Line";
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
                                        break;
                                    }
                                }

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
                                            string tTaxType = "NoTax";
                                            for (int mn = 0; mn < dtPrint.Rows.Count; mn++)
                                            {
                                                if (dtPrint.Rows[mn]["Describ"].ToString() == "Display Tax Type")
                                                {
                                                    tTaxType = dtPrint.Rows[mn]["Property"].ToString();
                                                    break;
                                                }

                                            }
                                            if (tTaxType.Trim() == "NoTax")
                                            {
                                                topLine1 = "[ GST : 0.00 ]";
                                            }
                                            if (tTaxType.Trim() == "Exclusive")
                                            {
                                                topLine1 = "[ GST : " + string.Format("{0:0.00}", double.Parse(@tTotTax.ToString())) + " ]";
                                            }
                                            if (tTaxType.Trim() == "Inclusive")
                                            {
                                                //double tTaxPrev = @tTotAmt;

                                                //double tPercent = 0.0;
                                                //double a = 7, b = 100;
                                                //tPercent = Convert.ToDouble(a) / Convert.ToDouble(b);
                                                //double tTaxFind = (tTaxPrev * tPercent);
                                                //tTaxFind = (tTaxPrev - tTaxFind);
                                                //topLine1 = "[ GST 7%: " + string.Format("{0:0.00}", tTaxFind * tPercent) + " ]";


                                                topLine1 = "[ GST : " + string.Format("{0:0.00}", double.Parse(@tTotTax.ToString())) + " ]";

                                                //  topLine1 = "[ GST 7%: " + string.Format("{0:0.00}", double.Parse(@totTax.ToString())) + " ]";
                                            }
                                            // +":$3000.00";
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
                                        break;
                                    }
                                }
                            }
                            break;
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
                            break;
                        }
                    }
                    // Your Order Number



                    for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
                    {
                        if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                        {
                            charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                        }

                        if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Print Bill Type")
                        {
                            if (dtPrint.Rows[i3]["Property"].ToString().Trim() == "Yes")
                            {
                                string temp1 = "Payment Mode:" + tBillType;
                                mainStr += temp1;
                                for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                break;
                            }
                        }
                    }
                    if (isCancel != "Cancel" && isReturn != "Return")
                    {
                        for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
                        {
                            if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                            }

                            if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Print Payment Mode")
                            {
                                if (dtPrint.Rows[i3]["Property"].ToString().Trim() == "Yes")
                                {
                                    DataTable dtPayment = new DataTable();
                                    dtPayment.Rows.Clear();
                                    SqlCommand cmdPayment = new SqlCommand("Select Ledger_groupno,Ledger_no,Ledger_name, SUM(SalRecv_Amt) as Amt  from salRecv_table, Ledger_table where  SalRecv_Led=Ledger_no and SalRecv_Salno=@tBillNo group by Ledger_groupno,Ledger_no, Ledger_name", con);
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
                                        for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += "\n";
                                    }
                                    if (tPNETSAmt > 0)
                                    {
                                        string temp1 = "NETS      : " + string.Format("{0:0.00}", tPNETSAmt);
                                        mainStr += temp1;
                                        for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += "\n";
                                    }
                                    if (tPCreditCardAmt > 0)
                                    {
                                        string temp1 = "Creditcard: " + string.Format("{0:0.00}", tPCreditCardAmt);
                                        mainStr += temp1;
                                        for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                        {
                                            mainStr += " ";
                                        }
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
                                            for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                            {
                                                mainStr += " ";
                                            }
                                            mainStr += "\n";
                                        }
                                    }
                                    if (tPHouseACAmt > 0)
                                    {
                                        string temp1 = "House AC  : " + string.Format("{0:0.00}", tPHouseACAmt);
                                        mainStr += temp1;
                                        for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += "\n";
                                    }

                                    for (int mn = 0; mn < dtPayment.Rows.Count; mn++)
                                    {
                                        if (dtPayment.Rows[mn]["Ledger_groupno"].ToString().Trim() == "32")
                                        {
                                            // tPCreditCardAmt += (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim()));
                                            //   string temp1 = " >" + dtPayment.Rows[mn]["Ledger_name"].ToString().Trim() +" : "+ string.Format("{0:0.00}", (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim())));
                                            string temp1 = " >" + dtPayment.Rows[mn]["Ledger_name"].ToString().Trim();
                                            mainStr += ((temp1.Length < (double.Parse(charPerLine) - 12)) ? temp1 : temp1.Substring(0, (int)(double.Parse(charPerLine) - 13))) + " : " + string.Format("{0:0.00}", (dtPayment.Rows[mn]["Amt"].ToString().Trim() == "" ? 0.00 : double.Parse(dtPayment.Rows[mn]["Amt"].ToString().Trim())));
                                            // mainStr += temp1;
                                            for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                            {
                                                mainStr += " ";
                                            }
                                            mainStr += "\n";
                                        }

                                    }

                                    if (tPVoucherAmt > 0)
                                    {
                                        string temp1 = "Voucher   : " + string.Format("{0:0.00}", tPVoucherAmt);
                                        mainStr += temp1;
                                        for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                        {
                                            mainStr += " ";
                                        }
                                        mainStr += "\n";
                                    }
                                    for (int i10 = 0; i10 < dtPrint.Rows.Count - 1; i10++)
                                    {
                                        //if (dtPrint.Rows[i10]["Describ"].ToString() == "Characters Per Line*")
                                        //{
                                        //    charPerLine = dtPrint.Rows[i10]["Property"].ToString();
                                        //}

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
                                            break;
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    //receipt No 
                    //  if (double.Parse(tRefund.ToString()) > 0)
                    {


                        for (int i3 = 0; i3 < dtPrint.Rows.Count - 1; i3++)
                        {
                            if (dtPrint.Rows[i3]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i3]["Property"].ToString();
                            }


                            if (dtPrint.Rows[i3]["Describ"].ToString().Trim() == "Amount Tendered")
                            {
                                // if (dtPrint.Rows[i3]["Property"].ToString() != "")

                                string temp1 = dtPrint.Rows[i3]["Property"].ToString() + " " + string.Format("{0:0.00}", tRefund);
                                mainStr += temp1;
                                for (int j = 0; j < (double.Parse(charPerLine) - temp1.Length); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += "\n";
                                //   lblRefund.Content = string.Format("{0:0.00}", tTotal);
                                //  funRoundCalculate1();
                                string temp = "Change : " + string.Format("{0:0.00}", tRefund - double.Parse(lblRefund.Content.ToString()));
                                mainStr += temp;
                                for (int j = 0; j < (double.Parse(charPerLine) - temp.Length); j++)
                                {
                                    mainStr += " ";
                                }
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
                                        break;
                                    }
                                }

                            }
                        }
                    }
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
                                        break;
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
                                        break;
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
                                        break;
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
                                        break;
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
                                        break;
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
                            break;
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

                                topLine1 = currentDate.ToString();
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
                                break;
                            }

                        }
                    }


                    if (isCancel == "Cancel")
                    {

                        for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                        {
                            if (dtPrint.Rows[i8]["Describ"].ToString() == "Characters Per Line*")
                            {
                                charPerLine = dtPrint.Rows[i8]["Property"].ToString();

                                topLine1 = "*** VOID ***";
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
                                break;
                            }
                        }
                    }
                    // string mainstr2 = "";
                    //mainstr2 = mainStr;
                    if (isReturn == "Return")
                    {
                        //for (int i5 = 0; i5 < dtPrint.Rows.Count - 1; i5++)
                        //{
                        //    if (dtPrint.Rows[i5]["Describ"].ToString() == "Characters Per Line*")
                        //    {
                        //        charPerLine = dtPrint.Rows[i5]["Property"].ToString();
                        //    }

                        //    //  double location = 0.00;
                        //    string tempStr = null;

                        //    if (dtPrint.Rows[i5]["Describ"].ToString() == "Print Qunatity and Rate")
                        //    {
                        //        if (dtPrint.Rows[i5]["Property"].ToString() == "Yes")
                        //        {
                        //            string tQtyHeading = "";
                        //            tQtyHeading = "Returned Product Detail";
                        //            //  mainStr += tQtyHeading;
                        //            double chkCount = (double.Parse(charPerLine) - (tQtyHeading.Length));
                        //            for (int j = 0; j < chkCount; j++)
                        //            {
                        //                tQtyHeading += " ";
                        //            }

                        //            mainStr += tQtyHeading;
                        //            mainStr += "\n";
                        //            for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                        //            {
                        //                if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                        //                {
                        //                    charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                        //                }

                        //                // print lint below logo
                        //                if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                        //                {
                        //                    lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                        //                    if (lineBelowLogo == "No Line")
                        //                    {
                        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        //                        {
                        //                            mainStr += " ";
                        //                        }
                        //                        mainStr += "\n";
                        //                    }
                        //                    if (lineBelowLogo == "Single Line")
                        //                    {
                        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        //                        {
                        //                            mainStr += "-";
                        //                        }
                        //                        mainStr += "\n";
                        //                    }
                        //                    else if (lineBelowLogo == "Double Line")
                        //                    {
                        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        //                        {
                        //                            mainStr += "=";
                        //                        }
                        //                        mainStr += "\n";
                        //                    }
                        //                    break;
                        //                }
                        //            }

                        //            for (int mn = 0; mn < dtReturn.Rows.Count; mn++)
                        //            //foreach (DataRow row in dgsales.Rows)
                        //            {
                        //                // object[] array = dgsales.Rows[mn].;
                        //                bool isChk = false;
                        //                for (int z = 0; z < 4; z++)
                        //                {
                        //                    if (dtReturn.Rows[mn][z].ToString().Trim() == "")
                        //                    {
                        //                        isChk = true;
                        //                        break;
                        //                    }
                        //                }
                        //                if (isChk == false)
                        //                {
                        //                    for (int i = 0; i < 4; i++)
                        //                    {
                        //                        tempStr = dtReturn.Rows[mn][i].ToString();

                        //                        if (i == 0)
                        //                        {
                        //                            if (_Class.clsVariables.tempGPrintPrinterItemName == "Yes")
                        //                            {
                        //                                dtPrinterItemName.Rows.Clear();
                        //                                SqlCommand cmdPrinterName = new SqlCommand("Select Item_PrintName from Item_table where item_name=@tItemName", con);
                        //                                cmdPrinterName.Parameters.AddWithValue("@tItemName", tempStr);
                        //                                SqlDataAdapter adpPrinterName = new SqlDataAdapter(cmdPrinterName);
                        //                                adpPrinterName.Fill(dtPrinterItemName);

                        //                                if (dtPrinterItemName.Rows.Count > 0)
                        //                                {
                        //                                    tempStr = dtPrinterItemName.Rows[0]["Item_PrintName"].ToString();
                        //                                }
                        //                            }
                        //                        }
                        //                        //  MessageBox.Show(tempStr.Length.ToString());
                        //                        findCenterPosition = (double.Parse(charPerLine) - 22);
                        //                        if (i == 0)
                        //                        {
                        //                            if (tempStr.Length <= (int)findCenterPosition)
                        //                            {
                        //                                mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                        //                            }
                        //                            else
                        //                            {
                        //                                string temp = tempStr.Substring(0, (((int)findCenterPosition) < tempStr.Length) ? (int)(findCenterPosition) : tempStr.Length);
                        //                                //    MessageBox.Show(temp);
                        //                                int chkSpace = temp.LastIndexOf(" ");
                        //                                int loc = (temp.Length - temp.LastIndexOf(" "));
                        //                                //   MessageBox.Show(temp.LastIndexOf(" ").ToString());
                        //                                if (chkSpace != -1)
                        //                                {
                        //                                    mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                        //                                    //   MessageBox.Show(mainStr.ToString());
                        //                                    for (int j = 0; j < loc + 18; j++)
                        //                                    {
                        //                                        mainStr += " ";
                        //                                    }
                        //                                    mainStr += "\n";
                        //                                    string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                        //                                    // mainStr += temp1;
                        //                                    if (temp1.Length <= (int)findCenterPosition)
                        //                                    {
                        //                                        mainStr += temp1.PadRight((int)findCenterPosition, ' ');
                        //                                    }
                        //                                }
                        //                                else
                        //                                {
                        //                                    mainStr += temp.ToString();
                        //                                }

                        //                            }
                        //                        }

                        //                        if (i == 1)
                        //                        {
                        //                            if (tempStr.Length < 8)
                        //                            {
                        //                                if (_Class.clsVariables.tempGPrintReceiptQtyCenterPosition == "Yes")
                        //                                {
                        //                                    findCenterPosition = (7 - tempStr.Length);
                        //                                    if (findCenterPosition % 2 == 0)
                        //                                    {
                        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                        //                                        {
                        //                                            mainStr += " ";
                        //                                        }
                        //                                        mainStr += tempStr;
                        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                        //                                        {
                        //                                            mainStr += " ";
                        //                                        }
                        //                                    }
                        //                                    else
                        //                                    {
                        //                                        for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                        //                                        {
                        //                                            mainStr += " ";
                        //                                        }
                        //                                        mainStr += tempStr;
                        //                                        for (int j = 0; j < (findCenterPosition / 2); j++)
                        //                                        {
                        //                                            mainStr += " ";
                        //                                        }
                        //                                    }
                        //                                }
                        //                                else
                        //                                {
                        //                                    mainStr += tempStr.PadRight(7, ' ');
                        //                                }
                        //                            }
                        //                        }
                        //                        if (i == 2)
                        //                        {
                        //                            // mainStr += tempStr.PadRight(7, ' ');
                        //                            if (tempStr.Length <= 7)
                        //                            {
                        //                                mainStr += tempStr.PadLeft(7, ' ');
                        //                            }
                        //                        }
                        //                        if (i == 3)
                        //                        {
                        //                            if (tempStr.Length <= 8)
                        //                            {
                        //                                mainStr += tempStr.PadLeft(8, ' ');
                        //                            }
                        //                        }
                        //                        // tPrintText += tempStr;
                        //                    }
                        //                    mainStr += "\n";
                        //                }
                        //            }

                        //        }

                        //        else
                        //        {
                        //            string tQtyHeading = "";
                        //            tQtyHeading = "Returnted Product Detail";
                        //            mainStr += tQtyHeading;
                        //            double tQtyCount = (double.Parse(charPerLine) - (tQtyHeading.Length));
                        //            for (int j = 0; j < tQtyCount; j++)
                        //            {
                        //                mainStr += " ";
                        //            }
                        //            mainStr += "\n";
                        //            for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                        //            {
                        //                if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                        //                {
                        //                    charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                        //                }

                        //                // print lint below logo
                        //                if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                        //                {
                        //                    lineBelowLogo = dtPrint.Rows[i4]["Property"].ToString();
                        //                    if (lineBelowLogo == "No Line")
                        //                    {
                        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        //                        {
                        //                            mainStr += " ";
                        //                        }
                        //                        mainStr += "\n";
                        //                    }
                        //                    if (lineBelowLogo == "Single Line")
                        //                    {
                        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        //                        {
                        //                            mainStr += "-";
                        //                        }
                        //                        mainStr += "\n";
                        //                    }
                        //                    else if (lineBelowLogo == "Double Line")
                        //                    {
                        //                        for (int j = 0; j < double.Parse(charPerLine); j++)
                        //                        {
                        //                            mainStr += "=";
                        //                        }
                        //                        mainStr += "\n";
                        //                    }
                        //                    break;
                        //                }
                        //            }



                        //            for (int mn = 0; mn < dtReturn.Rows.Count; mn++)
                        //            //foreach (DataRow row in dgsales.Rows)
                        //            {
                        //                // object[] array = row.ItemArray;

                        //                for (int i = 0; i < 4; i++)
                        //                {
                        //                    tempStr = dtReturn.Rows[mn][i].ToString();
                        //                    //  MessageBox.Show(tempStr.Length.ToString());
                        //                    findCenterPosition = (double.Parse(charPerLine) - 18);
                        //                    if (i == 0)
                        //                    {
                        //                        if (tempStr.Length <= (int)findCenterPosition)
                        //                        {
                        //                            mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                        //                        }
                        //                        else
                        //                        {
                        //                            string temp = tempStr.Substring(0, (int)findCenterPosition);
                        //                            int loc = (temp.Length - temp.LastIndexOf(" "));
                        //                            mainStr += temp.Substring(0, temp.LastIndexOf(" "));
                        //                            for (int j = 0; j < loc + 18; j++)
                        //                            {
                        //                                mainStr += " ";
                        //                            }
                        //                            mainStr += "\n";
                        //                            string temp1 = tempStr.Substring((temp.LastIndexOf(" ") + 1), ((tempStr.Length - temp.LastIndexOf(" ")) - 1));
                        //                            mainStr += temp1;
                        //                            if (temp1.Length <= (int)findCenterPosition)
                        //                            {
                        //                                mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                        //                            }
                        //                        }
                        //                        //if (tempStr.Length <= (int)findCenterPosition)
                        //                        //{
                        //                        //    mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                        //                        //}
                        //                    }
                        //                    if (i == 1)
                        //                    {
                        //                        mainStr += "   ";
                        //                        //if (tempStr.Length < 4)
                        //                        //{
                        //                        //    mainStr += tempStr.PadRight(3, ' ');
                        //                        //}
                        //                    }
                        //                    if (i == 2)
                        //                    {
                        //                        mainStr += "       ";
                        //                        //if (tempStr.Length <= 7)
                        //                        //{
                        //                        //    mainStr += tempStr.PadLeft(7, ' ');
                        //                        //}
                        //                    }
                        //                    if (i == 3)
                        //                    {
                        //                        if (tempStr.Length <= 8)
                        //                        {
                        //                            mainStr += tempStr.PadLeft(8, ' ');
                        //                        }
                        //                    }
                        //                    // tPrintText += tempStr;
                        //                }
                        //                mainStr += "\n";
                        //            }
                        //         //   break;
                        //        }

                        //    }          


                        //}

                    }




                    // MessageBox.Show(mainStr);
                    //string tPrinterType = "";
                    //for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                    //{
                    //    if (dtPrint.Rows[i8]["Describ"].ToString() == "Enable This Device*")
                    //    {
                    //        if (dtPrint.Rows[i8]["Property"].ToString() == "Yes")
                    //        {
                    //            tPrinterType = "Receipt";
                    //        }
                    //    }
                    //}

                    //int tNoPrint = 0;
                    //for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                    //{
                    //    if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                    //    {
                    //        if (tPrinterType == "Receipt")
                    //        {
                    //            DataTable dtPrinter = new DataTable();
                    //            dtPrinter.Rows.Clear();
                    //            SqlDataAdapter adpPrinter = new SqlDataAdapter("select * from CrystalReportPrinterList", con);
                    //            adpPrinter.Fill(dtPrinter);
                    //            bool isChkPrinter = false;
                    //            for (int i = 0; i < dtPrinter.Rows.Count; i++)
                    //            {
                    //                string printerName = dtPrinter.Rows[i]["PrinterName"].ToString();
                    //                isChkPrinter = false;
                    //                if (dtPrint.Rows[i8]["Property"].ToString().ToUpper() == printerName.ToUpper())
                    //                {
                    //                    isChkPrinter = true;
                    //                    //rptReceiptReport rpt = new rptReceiptReport();
                    //                    //CrystalDecisions.CrystalReports.Engine.TextObject str1 = ((CrystalDecisions.CrystalReports.Engine.TextObject)rpt.Section2.ReportObjects["Text1"]);
                    //                    //str1.Text = mainStr;
                    //                    //rpt.PrintToPrinter(0, true, 1, 0);
                    //                    reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcReceipt.rdlc";
                    //                    ReportParameter rpReportOn = new ReportParameter("ReceiptValue", Convert.ToString(mainStr), false);
                    //                    this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });
                    //                    reportViewerSales.RefreshReport();
                    //                    reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);

                    //                    break;
                    //                }
                    //            }
                    //            if (isChkPrinter == false)
                    //            {
                    //                for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                    //                {
                    //                    if (dtPrint.Rows[k]["Describ"].ToString() == "Print Copies*")
                    //                    {
                    //                        topLine5 = dtPrint.Rows[k]["Property"].ToString();
                    //                        if (topLine5 == "1 Copy")
                    //                        {
                    //                            tNoPrint = 1;
                    //                        }
                    //                        else if (topLine5 == "2 Copy")
                    //                        {
                    //                            tNoPrint = 2;
                    //                        }
                    //                        else if (topLine5 == "3 Copy")
                    //                        {
                    //                            tNoPrint = 3;
                    //                        }
                    //                        else if (topLine5 == "No Copies")
                    //                        {
                    //                            tNoPrint = 0;
                    //                        }

                    //                        for (int i2 = 0; i2 < tNoPrint; i2++)
                    //                        {
                    //                            //  RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), mainStr);
                    //                            Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), mainStr));
                    //                            workerThread.Start();
                    //                            bool finished = workerThread.Join(3000);
                    //                            if (!finished)
                    //                            {
                    //                                workerThread.Abort();
                    //                            }

                    //                            // string s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 29, 86, 66, 0, 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                    //                            // RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);
                    //                        }
                    //                    }
                    //                }
                    //            }



                    //        }
                    //        break;
                    //    }
                    //}
                    //for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
                    //{
                    //    if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
                    //    {

                    //        for (int i81 = 0; i81 < dtPrint.Rows.Count - 1; i81++)
                    //        {
                    //            if (dtPrint.Rows[i81]["Describ"].ToString() == "Cut Paper")
                    //            {
                    //                DataTable dtNew = new DataTable();
                    //                dtNew.Rows.Clear();
                    //                SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                    //                cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    //                SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                    //                adp.Fill(dtNew);
                    //                if (dtNew.Rows.Count > 0)
                    //                {
                    //                    //if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
                    //                    //{

                    //                    ////if (dtNew.Rows[0]["Action"].ToString().Trim() == "Cut")
                    //                    ////{

                    //                    string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

                    //                    byteOut = new byte[byteStrings.Length];

                    //                    for (int i = 0; i < byteStrings.Length; i++)
                    //                    {

                    //                        byteOut[i] = Convert.ToByte(byteStrings[i]);

                    //                    }
                    //                    //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                    //                    //     }

                    //                    string s1 = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

                    //                    //  RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s1);

                    //                    Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s1));
                    //                    workerThread.Start();
                    //                    bool finished = workerThread.Join(3000);
                    //                    if (!finished)
                    //                    {
                    //                        workerThread.Abort();
                    //                    }

                    //                }
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}


                    ////}
                    ////else
                    ////{
                    ////    MyMessageBox.ShowBox("Enter Product", "Warning");
                    ////}







                    string tPrinterType1 = "";

                    if (_Class.clsVariables.tempGEnableThisDevice == "Yes")
                    {
                        tPrinterType1 = "Receipt";
                    }
                    // mainStr = "";
                    int tNoPrint1 = 0;

                    if (tPrinterType1 == "Receipt")
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
                                tNoPrint1 = 1;
                            }
                            else if (topLine5 == "2 Copy")
                            {
                                tNoPrint1 = 2;
                            }
                            else if (topLine5 == "3 Copy")
                            {
                                tNoPrint1 = 3;
                            }
                            else if (topLine5 == "No Copies")
                            {
                                tNoPrint1 = 0;
                            }

                            for (int i2 = 0; i2 < tNoPrint1; i2++)
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

                            //if (dtDetail.Rows.Count > 0)
                            //{
                            //    string vLedgerName = "";
                            //    string vInvToAddress = "";
                            //    string vCompanyName = "";
                            //    string CompanyAddressLine1 = "";                          

                            //        SqlCommand cmdBillNo1 = new SqlCommand("select * from tempsalmas_table where smas_billno=@tBillNo", con);
                            //        cmdBillNo1.Parameters.AddWithValue("@tBillNo", txtBillNo.Text);
                            //        SqlDataAdapter adpBillNo1 = new SqlDataAdapter(cmdBillNo1);
                            //        DataTable dtBillNo1 = new DataTable();
                            //        dtBillNo1.Rows.Clear();
                            //        adpBillNo1.Fill(dtBillNo1);
                            //        if (dtBillNo1.Rows.Count > 0)
                            //        {
                            //            vLedgerName = (dtBillNo1.Rows[0]["smas_name"].ToString().Trim());
                            //        }


                            //    if (vLedgerName != "Cash Sales" && vLedgerName != "NETS")
                            //    {
                            //        SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=32", con);
                            //        cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", vLedgerName);
                            //        SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                            //        DataTable dtLedger = new DataTable();
                            //        adpLedgerDetails.Fill(dtLedger);
                            //        if (dtLedger.Rows.Count > 0)
                            //        {
                            //            vLedgerName = (dtLedger.Rows[0]["Ledger_Name"].ToString().Trim());
                            //            vInvToAddress = (dtLedger.Rows[0]["Ledger_Add1"].ToString().Trim()) + Environment.NewLine + (dtLedger.Rows[0]["Ledger_Add2"].ToString().Trim()) + Environment.NewLine + (dtLedger.Rows[0]["Ledger_Add3"].ToString().Trim());
                            //        }
                            //    }
                            //    else
                            //    {
                            //        vInvToAddress = "";
                            //    }

                            //    SqlDataAdapter adpCompanyAddress = new SqlDataAdapter("Select * from Custom_text", con);
                            //    DataTable dtcompany = new DataTable();
                            //    adpCompanyAddress.Fill(dtcompany);
                            //    if (dtcompany.Rows.Count > 0)
                            //    {
                            //        for (int i = 0; i < dtcompany.Rows.Count; i++)
                            //        {
                            //            if (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line1")
                            //            {
                            //                vCompanyName = (dtcompany.Rows[i]["prop"].ToString());
                            //            }

                            //            if ((dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line2") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line3") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line4") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line5"))
                            //            {
                            //                CompanyAddressLine1 = CompanyAddressLine1.ToString() + (dtcompany.Rows[i]["prop"].ToString()) + Environment.NewLine;
                            //            }

                            //            //if ((dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line1") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line2") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line3") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line4") || (dtcompany.Rows[i]["Describ"].ToString().Trim() == "Top Line5"))
                            //            //{
                            //            //    CompanyAddressLine1 = CompanyAddressLine1.ToString() + (dtcompany.Rows[i]["prop"].ToString()) + Environment.NewLine;
                            //            //}
                            //        }
                            //        Dataset.DsSalesRpt dsSalesSummaryObj = new Dataset.DsSalesRpt();
                            //        for (int i = 0; i < dtDetail.Rows.Count; i++)
                            //        {
                            //            dsSalesSummaryObj.Tables["DsSalesRpt"].Rows.Add(dtDetail.Rows[i]["Item_name"].ToString(), dtDetail.Rows[i]["nt_qty"].ToString(), dtDetail.Rows[i]["Column1"], dtDetail.Rows[i]["Column2"].ToString(), "0.00", "0.00", "0.00");
                            //        }
                            //        rpt.Reset();
                            //        //  DataTable dt = getDate();
                            //        ReportDataSource ds1 = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DsSalesRpt"]);
                            //        rpt.LocalReport.DataSources.Add(ds1);

                            //        rpt.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.RptSample.rdlc";
                            //        //Passing Parmetes:

                            //        //ReportParameter rpReportOnName = new ReportParameter("CompanyName", Convert.ToString(vCompanyName), false);
                            //        //this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOnName });

                            //        ReportParameter rptSSS = new ReportParameter("CAddress1", Convert.ToString(vCompanyName), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptSSS });

                            //        ReportParameter rpReportOn = new ReportParameter("CAddress", Convert.ToString(CompanyAddressLine1), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });


                            //        ReportParameter rpReportOn1 = new ReportParameter("BillNo", Convert.ToString(txtBillNo.Text), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOn1 });

                            //        ReportParameter rpReportOn2 = new ReportParameter("InvoiceName", Convert.ToString(vLedgerName), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rpReportOn2 });

                            //        ReportParameter rptInvoiceToAddress1 = new ReportParameter("InvToAddress", Convert.ToString(vInvToAddress), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceToAddress1 });

                            //        ReportParameter rptInvoiceToAddress3 = new ReportParameter("ShipName", Convert.ToString(vLedgerName), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceToAddress3 });

                            //        ReportParameter rptInvoiceToAddress2 = new ReportParameter("ToShipAddress1", Convert.ToString(vInvToAddress), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceToAddress2 });

                            //        ReportParameter rptInvoiceDate = new ReportParameter("InvoiceDate1", Convert.ToString(tBillDate.ToString("dd/MM/yyyy")), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptInvoiceDate });

                            //        //if (tDiscount != 0)
                            //        //{
                            //        ReportParameter rptDiscount = new ReportParameter("TotDiscount", Convert.ToString(string.Format("{0:0.00}", double.Parse(tDiscount.ToString()))), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptDiscount });
                            //        //}

                            //        ReportParameter rptGSTAmount = new ReportParameter("TotGstAmt", Convert.ToString(string.Format("{0:0.00}", double.Parse(@tTotTax.ToString()))), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptGSTAmount });

                            //        ReportParameter rptTAmount = new ReportParameter("TotNetAmt", Convert.ToString(string.Format("{0:0.00}", double.Parse(lblNetAmt.Content.ToString()))), false);
                            //        this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptTAmount });


                            //        dsSalesSummaryObj.Tables["DsSalesRpt"].EndInit();
                            //        rpt.RefreshReport();
                            //        rpt.RenderingComplete += new RenderingCompleteEventHandler(PrintSalesNew);                                
                            //    }
                            //}

                            //ForsalesPrint

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

                                //if (vMainTable == "Yes" && vPrevBill == "Yes")
                                if (lblBillNo.Content != "")
                                {
                                    SqlCommand cmdBillNo1 = new SqlCommand("select * from salmas_table where smas_billno=@tBillNo", con);
                                    cmdBillNo1.Parameters.AddWithValue("@tBillNo", lblBillNo.Content);
                                    SqlDataAdapter adpBillNo1 = new SqlDataAdapter(cmdBillNo1);
                                    DataTable dtBillNo1 = new DataTable();
                                    dtBillNo1.Rows.Clear();
                                    adpBillNo1.Fill(dtBillNo1);
                                    if (dtBillNo1.Rows.Count > 0)
                                    {
                                        vLedgerName = (dtBillNo1.Rows[0]["smas_name"].ToString().Trim());
                                        tPartyno = (dtBillNo1.Rows[0]["Smas_SmanNo"].ToString().Trim());
                                        strtremarks = (dtBillNo1.Rows[0]["Smas_remarks"].ToString().Trim());
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
                                //else
                                //{
                                //    SqlCommand cmdBillNo = new SqlCommand("select * from tempsalmas_table where smas_billno=@tBillNo", con);
                                //    cmdBillNo.Parameters.AddWithValue("@tBillNo", lblPreviosBillNo.Content);
                                //    SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                                //    DataTable dtBillNo = new DataTable();
                                //    dtBillNo.Rows.Clear();
                                //    adpBillNo.Fill(dtBillNo);
                                //    if (dtBillNo.Rows.Count > 0)
                                //    {
                                //        vLedgerName = (dtBillNo.Rows[0]["smas_name"].ToString().Trim());
                                //        strtremarks = (dtBillNo.Rows[0]["Smas_remarks"].ToString().Trim());
                                //        if (strtremarks == "Null")
                                //        {
                                //            tRemarks = "";
                                //        }
                                //        else
                                //        {
                                //            tRemarks = strtremarks;
                                //        }

                                //    }
                                //    SqlCommand cmdSalName = new SqlCommand("select * from Ledger_table where Ledger_No=@tLedgerNo", con);
                                //    cmdSalName.Parameters.AddWithValue("@tLedgerNo", _Class.clsVariables.tempsalesmenLedgerNo);
                                //    SqlDataAdapter adpSalName = new SqlDataAdapter(cmdSalName);
                                //    DataTable dtSalName = new DataTable();
                                //    dtSalName.Rows.Clear();
                                //    adpSalName.Fill(dtSalName);
                                //    if (dtSalName.Rows.Count > 0)
                                //    {
                                //        tLedgerSalesmenName = (dtSalName.Rows[0]["Ledger_name"].ToString().Trim());
                                //    }


                                //}
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
                                    //A4
                                    //Dataset.DsSalesRpt dsSalesSummaryObj = new Dataset.DsSalesRpt();
                                    //for (int i = 0; i < dtDetail.Rows.Count; i++)
                                    //{
                                    //    dsSalesSummaryObj.Tables["DsSalesRpt"].Rows.Add(dtDetail.Rows[i]["Item_name"].ToString(), dtDetail.Rows[i]["nt_qty"].ToString(), dtDetail.Rows[i]["Column1"], dtDetail.Rows[i]["Column2"].ToString(), "0.00", "0.00", "0.00");
                                    //    tGrandTotal += Convert.ToDouble(dtDetail.Rows[i]["Column2"].ToString());
                                    //}

                                    //thermal
                                    DsA4sales dssalessummaryObj1 = new DsA4sales();
                                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                                    {
                                        dssalessummaryObj1.Tables["DtA4Sales"].Rows.Add(dtDetail.Rows[i]["Item_name"].ToString(), dtDetail.Rows[i]["nt_qty"].ToString(), dtDetail.Rows[i]["Column1"], dtDetail.Rows[i]["Column2"].ToString(), "0.00", "0.00", "0.00");
                                        tGrandTotal += Convert.ToDouble(dtDetail.Rows[i]["Column2"].ToString());
                                    }

                                    rpt.Reset();
                                    //  DataTable dt = getDate();
                                    string dtdate = DateTime.Now.ToString();

                                    //ReportDataSource ds1 = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DsSalesRpt"]);
                                    //rpt.LocalReport.DataSources.Add(ds1);
                                    //rpt.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.RptSample.rdlc";


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

                                    ReportParameter rpReportOn1 = new ReportParameter("BillNo", "FN0" + Convert.ToString(lblBillNo.Content), false);
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

                                    //ReportParameter rptTAmount = new ReportParameter("TotNetAmt", "$" + Convert.ToString(string.Format("{0:0.00}", double.Parse(lblNetAmt.Content.ToString()))), false);
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


                                    // dsSalesSummaryObj.Tables["DsSalesRpt"].EndInit();
                                    dssalessummaryObj1.Tables["DtA4Sales"].EndInit();
                                    rpt.RefreshReport();
                                    rpt.RenderingComplete += new RenderingCompleteEventHandler(PrintSales2);

                                }
                            }
                        }
                    }

                    //txtEnterValue.Focus();
                }
                else
                {
                    MyMessageBox.ShowBox("Records Not Found", "Warning");
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

                rpt.Clear();
                rpt.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
            }
        }
        public void PrintSalesNew(object sender, RenderingCompleteEventArgs e)
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

        private void txtReason_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtReason_LostFocus(object sender, RoutedEventArgs e)
        {
            _Class.clsVariables.tVoidActionType = "REMARK";
        }

        public void funLoadValues()
        {
            try
            {
                if (dtpTo.Text != null && dtpTo.Text != "")
                {
                    if (dtpFrom.SelectedDate.Value != null && dtpTo.SelectedDate.Value != null)
                    {
                        DataTable dtNew = new DataTable();
                        dtNew.Rows.Clear();
                        //SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                        SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel,smas_no from salmas_table where ctr_no=@tCounter and smas_rtno=0 and smas_billdate between @tFromDate AND @tToDate order by smas_billno DESC", con);
                        cmd.Parameters.AddWithValue("@tFromDate", Convert.ToDateTime(dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day));
                        cmd.Parameters.AddWithValue("@tToDate", Convert.ToDateTime(dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day));
                        cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtNew);

                        DataTable dtReturnVal = new DataTable();
                        dtReturnVal.Rows.Clear();
                        SqlCommand cmdReturn = new SqlCommand("select smas_rtno,SUM(smas_NetAmount) as returnAmt from salmas_table where ctr_no=@tCounter and smas_rtno<>0 and smas_rtno in (select smas_no from SalMas_table where Ctr_no=@tCounter and smas_rtno=0 and smas_billno in (select smas_billno from salmas_table where Ctr_no=@tCounter and smas_rtno=0 and smas_billdate between @tFromDate and @tToDate)) group by smas_rtno", con);
                        cmdReturn.Parameters.AddWithValue("@tFromDate", Convert.ToDateTime(dtpFrom.SelectedDate.Value.Year + "/" + dtpFrom.SelectedDate.Value.Month + "/" + dtpFrom.SelectedDate.Value.Day));
                        cmdReturn.Parameters.AddWithValue("@tToDate", Convert.ToDateTime(dtpTo.SelectedDate.Value.Year + "/" + dtpTo.SelectedDate.Value.Month + "/" + dtpTo.SelectedDate.Value.Day));
                        cmdReturn.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                        SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                        adpReturn.Fill(dtReturnVal);
                        string tSmasNo = "";
                        double tRetValue = 0.0, tNtAmt = 0.0;
                        for (int ij = 0; ij < dtReturnVal.Rows.Count; ij++)
                        {
                            tRetValue = 0.0;
                            tNtAmt = 0.0;
                            tSmasNo = "";

                            tSmasNo = dtReturnVal.Rows[ij]["smas_rtno"].ToString();
                            if (!string.IsNullOrEmpty(Convert.ToString(dtReturnVal.Rows[ij]["returnAmt"])))
                            {
                                tRetValue = (dtReturnVal.Rows[ij]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[ij]["returnAmt"].ToString());
                            }
                            for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                            {
                                if (Convert.ToString(dtReturnVal.Rows[ij]["smas_rtno"]) == Convert.ToString(dtNew.Rows[mn]["smas_no"]))
                                {
                                    //tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                                    tNtAmt = (string.IsNullOrEmpty(Convert.ToString(dtNew.Rows[mn]["NetAmount"]))) ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                                    dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                                }
                            }
                        }

                        gridDisplay.DataSource = dtNew.DefaultView;
                        gridDisplay.RowTemplate.Height = 35;
                        gridDisplay.Columns["Cancel"].Visible = false;
                        gridDisplay.Columns["smas_no"].Visible = false;


                        //for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                        //{
                        //    dtReturnVal.Rows.Clear();
                        //    SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_rtno<>0", con);
                        //    cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                        //    SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                        //    adpReturn.Fill(dtReturnVal);
                        //    double tRetValue = 0.0, tNtAmt = 0.0;
                        //    if (dtReturnVal.Rows.Count > 0)
                        //    {
                        //        if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                        //        {
                        //            tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                        //        }
                        //    }
                        //    tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                        //    dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                        //}

                        //gridDisplay.DataSource = dtNew.DefaultView;

                        //gridDisplay.RowTemplate.Height = 35;
                        //gridDisplay.Columns["Cancel"].Visible = false;
                        for (int j = 0; j < gridDisplay.Rows.Count; j++)
                        {
                            if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                            {
                                gridDisplay.Rows[j].ReadOnly = true;
                                // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                                gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        funCalculate("LOAD");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void dtpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // funLoadValues();
        }
        public string tTenderClose = "";
        private void btnReSettle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if(gridDisplay.Rows[].Cells["Cancel"].Value.ToString()=="True")
                if (isCancel != "Cancel" && isReturn != "Return")
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("Select * from User_table where User_no=@tUserNo", con);
                    cmd.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew);
                    string tResettleState = "No";
                    if (dtNew.Rows.Count > 0)
                    {
                        tResettleState = dtNew.Rows[0]["Resettle"].ToString();
                    }
                    if (tResettleState == "Yes")
                    {

                        SalesProject._Class.clsVariables.tControlFrom = "VOID";
                        tTenderClose = "";

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
                                if (UCfrmVoidEvent_ResettleClick != null)
                                {
                                    UCfrmVoidEvent_ResettleClick();

                                    ////UCFormSettle frm = new UCFormSettle();                                
                                    ////frm.tempBillAmount = lblNetAmt.Content.ToString();
                                    ////frm.currentDate = currentDate;
                                    ////frm.ds1.Tables.Add(dt.Copy());
                                    ////frm.tBillNo = lblBillNo.Content.ToString();
                                    ////frm.tTotQty = lblTotQty.Content.ToString();
                                    ////frm.tGrossAmt = lblTotAmt.Content.ToString();
                                    ////frm.tDiscount = lblDiscount.Content.ToString();
                                    ////frm.tNetAmt = lblNetAmt.Content.ToString();
                                    ////frm.tTaxAmt = lblTaxAmt.Content.ToString();                      
                                    ////frm.SalesCreationEventHandlerNew += new EventHandler(CloseEvent1);
                                    ////frm.SalesCreationEventHandlerNew1 += new EventHandler(CloseEvent);                                    
                                    ////frm.ShowDialog();
                                }
                                txtEnterValue.Focus();
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Item Quantity not in Zero", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please Select Bill First", "Warning");
                        }
                        // SalesProject._Class.clsVariables.tControlFrom = "";
                        txtEnterValue.Focus();

                    }
                }
                else
                {
                    MyMessageBox.ShowBox("This bill Could not be Re-settle", "Warning");
                }
            }
            catch (Exception ex)
            {
                SalesProject._Class.clsVariables.tControlFrom = "";
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void GridDisplaysettle()
        {
            try
            {
                if (dtpFrom.SelectedDate.Value != null && dtpTo.SelectedDate.Value != null)
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    //SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                    SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 order by smas_billno DESC", con);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew);

                    DataTable dtReturnVal = new DataTable();
                    SqlCommand cmdReturn;
                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        dtReturnVal.Rows.Clear();
                        cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_rtno<>0", con);
                        cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                        SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                        adpReturn.Fill(dtReturnVal);
                        double tRetValue = 0.0, tNtAmt = 0.0;
                        if (dtReturnVal.Rows.Count > 0)
                        {
                            if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                            {
                                tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                            }
                        }
                        tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                        dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                    }

                    gridDisplay.DataSource = dtNew.DefaultView;

                    gridDisplay.RowTemplate.Height = 35;
                    gridDisplay.Columns["Cancel"].Visible = false;
                    for (int j = 0; j < gridDisplay.Rows.Count; j++)
                    {
                        if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                        {
                            gridDisplay.Rows[j].ReadOnly = true;
                            // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                            gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    funCalculate("LOAD");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnCreditCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtCard = new DataTable();
                dtCard.Rows.Clear();
                SqlCommand cmdSelect = new SqlCommand("Select Ledger_name as Card_Name from Ledger_Table where Ledger_groupno=5 and Ledger_no<>14 order by Ledger_no asc", con);
                SqlDataAdapter adpSelect = new SqlDataAdapter(cmdSelect);
                adpSelect.Fill(dtCard);
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                for (int ij = 0; ij < dtCard.Rows.Count; ij++)
                {
                    // SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,smas_billDate as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_name='Cash Sales' and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                    SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where ctr_no=@tCounter and smas_rtno=0 and smas_name=@tCardName and smas_billdate between convert(date,@tFromDate,108) AND convert(date,@tToDate,108) order by smas_billno DESC", con);
                    cmd.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("@tCardName", dtCard.Rows[ij]["Card_Name"].ToString());
                    cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew);
                    DataTable dtReturnVal = new DataTable();

                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        dtReturnVal.Rows.Clear();
                        SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_rtno<>0 and  smas_name<>'Cash Sales' and smas_name<>'NETS'", con);
                        cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                        SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                        adpReturn.Fill(dtReturnVal);
                        double tRetValue = 0.0, tNtAmt = 0.0;
                        if (dtReturnVal.Rows.Count > 0)
                        {
                            if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                            {
                                tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                            }
                        }
                        tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                        dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                    }
                }
                gridDisplay.DataSource = dtNew.DefaultView;
                if (dtNew.Rows.Count > 0)
                {
                    gridDisplay.Columns["Cancel"].Visible = false;
                }


                for (int j = 0; j < gridDisplay.Rows.Count; j++)
                {
                    if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                    {
                        gridDisplay.Rows[j].ReadOnly = true;
                        // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                // funCalculate("CREDITCARD");
                funCalculate("CREDITCARD");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }

        }

        private void btnHouseAC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtHAC = new DataTable();
                dtHAC.Rows.Clear();

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmdHAC = new SqlCommand("select Ledger_name  from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 order by Ledger_name ASC", con);
                SqlDataAdapter adpHAC = new SqlDataAdapter(cmdHAC);
                adpHAC.Fill(dtHAC);
                for (int ij = 0; ij < dtHAC.Rows.Count; ij++)
                {
                    // SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,smas_billDate as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where smas_rtno=0 and smas_name='Cash Sales' and smas_billdate=(SELECT CONVERT(date,DATEADD(day,1,endofday),103) FROM EndOFday_Table where Id=(select EndOfDayId from NumberTable)) order by smas_billno DESC", con);
                    SqlCommand cmd = new SqlCommand("select smas_billno as BillNo,Convert(date,smas_billDate,103) as Date,CONVERT(time,smas_billtime,108) as Time,smas_name as Type,convert(numeric(18,2),smas_NetAmount) as NetAmount,smas_cancel as Cancel from salmas_table where ctr_no=@tCounter and smas_rtno=0 and smas_name=@tCashName and smas_billdate between convert(date,@tFromDate,108) AND convert(date,@tToDate,108) order by smas_billno DESC", con);
                    cmd.Parameters.AddWithValue("@tFromDate", dtpFrom.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("@tToDate", dtpTo.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("@tCashName", dtHAC.Rows[ij]["Ledger_name"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    //cmd.Parameters.AddWithValue("@tDate",(DateTime)result.Value.ToString();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew);
                    DataTable dtReturnVal = new DataTable();

                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        dtReturnVal.Rows.Clear();
                        SqlCommand cmdReturn = new SqlCommand("select SUM(smas_NetAmount) as returnAmt from salmas_table where smas_rtno=(select smas_no from SalMas_table where smas_billno=@tBillNo and smas_rtno=0) and smas_rtno<>0 and smas_name='Cash Sales'", con);
                        cmdReturn.Parameters.AddWithValue("@tBillNo", dtNew.Rows[mn]["BillNo"].ToString());
                        SqlDataAdapter adpReturn = new SqlDataAdapter(cmdReturn);
                        adpReturn.Fill(dtReturnVal);
                        double tRetValue = 0.0, tNtAmt = 0.0;
                        if (dtReturnVal.Rows.Count > 0)
                        {
                            if (dtReturnVal.Rows[0]["returnAmt"].ToString() != "")
                            {
                                tRetValue = (dtReturnVal.Rows[0]["returnAmt"].ToString() == "") ? 0 : double.Parse(dtReturnVal.Rows[0]["returnAmt"].ToString());
                            }
                        }
                        tNtAmt = (dtNew.Rows[mn]["NetAmount"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[mn]["NetAmount"].ToString());
                        dtNew.Rows[mn]["NetAmount"] = string.Format("{0:0.00}", (tNtAmt - tRetValue));
                    }
                }
                gridDisplay.DataSource = null;
                gridDisplay.DataSource = dtNew.DefaultView;
                if (dtNew.Rows.Count > 0)
                {
                    gridDisplay.Columns["Cancel"].Visible = false;
                }
                for (int j = 0; j < gridDisplay.Rows.Count; j++)
                {
                    if (gridDisplay.Rows[j].Cells["Cancel"].Value.ToString() == "True")
                    {
                        gridDisplay.Rows[j].ReadOnly = true;
                        // gridDisplay.Rows[j].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        gridDisplay.Rows[j].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                // funCalculate("HOUSEAC");
                funCalculate("HOUSEAC");


            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            funLoadValues();
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UCfrmPayment1.Visibility = Visibility.Visible;
                windowsFormsHost1.Visibility = Visibility.Hidden;
                windowsFormsHost2.Visibility = Visibility.Hidden;
                //  UCfrmPayment frmpay = new UCfrmPayment();               
                UCfrmPayment1.funPaymentAmtDetail(_Class.clsVariables.tEndOfDayDate, _Class.clsVariables.tCounter);
                //UCfrmPayment1.funPaymentAmtDetail();          
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }
        }




    }
}
