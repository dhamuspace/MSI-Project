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
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class frmSalesSummary : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dtDisplay = new DataTable();
     //   double SumofRetail1 =0, SumofWhole1=0, SumofReturn1=0, sumofTotal1=0;
        double SumofRetail2 = 0, SumofWhole2 = 0, SumofReturn2 = 0, sumofTotal2 = 0;

        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        public frmSalesSummary()
        {
            InitializeComponent();
            try
            {
                funConnectionStateCheck();
                grdMonthSummary.Rows.Add(14);
                //grdMonthSummary.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                //grdMonthSummary.RowHeadersDefaultCellStyle.BackColor = Color.Gray;
                grdMonthSummary.Rows[1].HeaderCell.Value = "April";
                grdMonthSummary.Rows[2].HeaderCell.Value = "May";
                grdMonthSummary.Rows[3].HeaderCell.Value = "June";
                grdMonthSummary.Rows[4].HeaderCell.Value = "July";
                grdMonthSummary.Rows[5].HeaderCell.Value = "August";
                grdMonthSummary.Rows[6].HeaderCell.Value = "September";
                grdMonthSummary.Rows[7].HeaderCell.Value = "October";
                grdMonthSummary.Rows[8].HeaderCell.Value = "November";
                grdMonthSummary.Rows[9].HeaderCell.Value = "December";
                grdMonthSummary.Rows[10].HeaderCell.Value = "January";
                grdMonthSummary.Rows[11].HeaderCell.Value = "February";
                grdMonthSummary.Rows[12].HeaderCell.Value = "March";
                // grdMonthSummary.Rows[14].HeaderCell.Value = "TOTAL";
                grdMonthSummary.Columns[0].Width = 350;
                grdMonthSummary.Columns[1].Width = 200;
                grdMonthSummary.Columns[2].Width = 200;
                grdMonthSummary.Columns[3].Width = 200;
                grdMonthSummary.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                Pnl_counter.Visible = false;
                lst_counter.Visible = false;
                lst_ofAmount.Visible = false;
                pnl_Amount.Visible = false;
                grdMonthSummary.Focus();
                grdMonthSummary.CurrentCell = grdMonthSummary.Rows[0].Cells[0];
                txt_reporton.Text = "Gross Amount";

                //foreach (DataGridViewColumn col in grdMonthSummary.Columns)
                //{
                //    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                //    col.HeaderCell.Style.ForeColor = Color.White;
                //}
                //foreach (DataGridViewRow header in grdMonthSummary.Rows)
                //{
                //    header.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //    header.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                //    header.HeaderCell.Style.ForeColor = Color.White;
                //}

                grdMonthSummary.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
                grdMonthSummary.BackgroundColor = Color.White;
                grdMonthSummary.RowHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
                label1.BackColor = Color.CornflowerBlue;

                foreach (DataGridViewColumn col in grdMonthSummary.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                //int year = 2013;
                //string currentmonth = "November";
                //int month = DateTime.Parse("1." + currentmonth + " 2013").Month;
                //DateTime endOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                //string enddate = endOfMonth.ToString("dd/MM/yyyy");
                grdMonthSummary.DefaultCellStyle.Font = new Font("Tahoma", 10);
                grdMonthSummary.RowTemplate.Height = 25;
                numYear.Value = DateTime.Now.Year;
                loadmonthdetails();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }


        string CounterNo;
        public void loadmonthdetails()
        {
            try
            {
                funConnectionStateCheck();
                // double   SumofRetail1 = 0, SumofWhole1 = 0, SumofReturn1 = 0, sumofTotal1 = 0;
                SumofRetail2 = 0; SumofWhole2 = 0; SumofReturn2 = 0; sumofTotal2 = 0;
                //  string CounterNoQry = "select ctr_no from counter_table where ctr_name='" + txt_countername.Text + "'";
                DataTable dtTemp = new DataTable();
                dtTemp.Rows.Clear();
                SqlCommand cmdCounterNo = new SqlCommand("sp_SalesSummarySelectSingle", con);
                cmdCounterNo.CommandType = CommandType.StoredProcedure;
                cmdCounterNo.Parameters.AddWithValue("@tActionType", "COUNTER");
                cmdCounterNo.Parameters.AddWithValue("@tValue", txt_countername.Text.Trim());
                SqlDataAdapter adp101 = new SqlDataAdapter(cmdCounterNo);
                adp101.Fill(dtTemp);
                CounterNo = "";
                if (dtTemp.Rows.Count > 0)
                {
                    CounterNo = dtTemp.Rows[0][0].ToString();
                }


                if (grdMonthSummary.Rows[1].HeaderCell.Value.ToString() == "April")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[1].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);
                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;
                    grdMonthSummary.Rows[1].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[1].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[1].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[1].Cells["S_Total"].Value = fToTotalNovember;



                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 04);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[1].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[1].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[1].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[1].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[1].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }


                }
                if (grdMonthSummary.Rows[2].HeaderCell.Value.ToString() == "May")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[2].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);
                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[2].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[2].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[2].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[2].Cells["S_Total"].Value = fToTotalNovember;



                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 05);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[2].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[2].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[2].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[2].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[2].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }


                }
                if (grdMonthSummary.Rows[3].HeaderCell.Value.ToString() == "June")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[3].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[3].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[3].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[3].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[3].Cells["S_Total"].Value = fToTotalNovember;

                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 06);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[3].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[3].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[3].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[3].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[3].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }


                }

                if (grdMonthSummary.Rows[4].HeaderCell.Value.ToString() == "July")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[4].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[4].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[4].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[4].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[4].Cells["S_Total"].Value = fToTotalNovember;


                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 07);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[4].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[4].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[4].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[4].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[4].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }


                }

                if (grdMonthSummary.Rows[5].HeaderCell.Value.ToString() == "August")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[5].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[5].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[5].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[5].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[5].Cells["S_Total"].Value = fToTotalNovember;


                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 08);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[5].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[5].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[5].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[5].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[5].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }


                }
                if (grdMonthSummary.Rows[6].HeaderCell.Value.ToString() == "September")
                {


                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[6].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());

                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 09);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());
                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);
                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[6].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[6].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[6].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[6].Cells["S_Total"].Value = fToTotalNovember;


                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[6].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[6].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[6].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[6].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[6].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }

                }

                if (grdMonthSummary.Rows[7].HeaderCell.Value.ToString() == "October")
                {

                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[7].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[7].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[7].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[7].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[7].Cells["S_Total"].Value = fToTotalNovember;


                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 10);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[7].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[7].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[7].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[7].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[7].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                }
                if (grdMonthSummary.Rows[8].HeaderCell.Value.ToString() == "November")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[8].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[8].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[8].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[8].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[8].Cells["S_Total"].Value = fToTotalNovember;


                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 11);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[8].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[8].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[8].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[8].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[8].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }

                }

                if (grdMonthSummary.Rows[9].HeaderCell.Value.ToString() == "December")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[9].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[9].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[9].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[9].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[9].Cells["S_Total"].Value = fToTotalNovember;



                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 12);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[9].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[9].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[9].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[9].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[9].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }

                }

                if (grdMonthSummary.Rows[10].HeaderCell.Value.ToString() == "January")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[10].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[10].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[10].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[10].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[10].Cells["S_Total"].Value = fToTotalNovember;



                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 01);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[10].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[10].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[10].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[10].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[10].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }

                }

                if (grdMonthSummary.Rows[11].HeaderCell.Value.ToString() == "February")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[11].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;

                    grdMonthSummary.Rows[11].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[11].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[11].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[11].Cells["S_Total"].Value = fToTotalNovember;



                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 02);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[11].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[11].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[11].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[11].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[11].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }

                }


                if (grdMonthSummary.Rows[12].HeaderCell.Value.ToString() == "March")
                {
                    double RetailAmt, WholeAmt, ReturnAmt;
                    SqlCommand cmdApril = new SqlCommand("sp_MonthSalesSummary", con);
                    cmdApril.CommandType = CommandType.StoredProcedure;
                    cmdApril.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdApril.Parameters.AddWithValue("@tMonth", grdMonthSummary.Rows[12].HeaderCell.Value.ToString());
                    cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
                    cmdApril.Parameters.AddWithValue("@tYearNew", numYear.Value);
                    SqlParameter tempRetailAmt = new SqlParameter("@tRetailAmt", SqlDbType.Float);
                    tempRetailAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempRetailAmt);
                    SqlParameter tempWholeAmt = new SqlParameter("@tWholeAmt", SqlDbType.Float);
                    tempWholeAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempWholeAmt);
                    SqlParameter tempReturnAmt = new SqlParameter("@tReturnAmt", SqlDbType.Float);
                    tempReturnAmt.Direction = ParameterDirection.Output;
                    cmdApril.Parameters.Add(tempReturnAmt);
                    cmdApril.ExecuteNonQuery();
                    RetailAmt = (tempRetailAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempRetailAmt.Value.ToString());
                    WholeAmt = (tempWholeAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempWholeAmt.Value.ToString());
                    ReturnAmt = (tempReturnAmt.Value.ToString().Trim() == "") ? 0 : double.Parse(tempReturnAmt.Value.ToString());


                    double TotalNovember = RetailAmt + WholeAmt - ReturnAmt;
                    string fToTotalNovember = string.Format("{0:0.00}", TotalNovember);
                    string fWholeAmt = string.Format("{0:0.00}", WholeAmt);
                    string fReturnAmt = string.Format("{0:0.00}", ReturnAmt);
                    string fRetailAmt = string.Format("{0:0.00}", RetailAmt);

                    SumofRetail2 = SumofRetail2 + RetailAmt;
                    SumofReturn2 = SumofReturn2 + ReturnAmt;
                    sumofTotal2 = sumofTotal2 + TotalNovember;
                    SumofWhole2 = SumofWhole2 + WholeAmt;


                    grdMonthSummary.Rows[12].Cells["S_Return"].Value = fReturnAmt;
                    grdMonthSummary.Rows[12].Cells["S_Retail"].Value = fRetailAmt;
                    grdMonthSummary.Rows[12].Cells["S_Whole"].Value = fWholeAmt;
                    grdMonthSummary.Rows[12].Cells["S_Total"].Value = fToTotalNovember;




                    //Anbu Alter Code File:
                    SqlCommand cmdtotNetSep = new SqlCommand("SP_MonthlySalesReportAlter", con);
                    cmdtotNetSep.CommandType = CommandType.StoredProcedure;
                    cmdtotNetSep.Parameters.AddWithValue("@CMonth", 03);
                    cmdtotNetSep.Parameters.AddWithValue("@CYear", numYear.Value);
                    cmdtotNetSep.Parameters.AddWithValue("@CounterName", string.IsNullOrEmpty(CounterNo) ? "1" : CounterNo.ToString());
                    cmdtotNetSep.Parameters.AddWithValue("@Types", (txt_reporton.Text == "Gross Amount") ? "Gross Amount" : txt_reporton.Text);
                    SqlParameter CurentDiscount = new SqlParameter("@totalDiscount", SqlDbType.Float);
                    CurentDiscount.Direction = ParameterDirection.Output;
                    cmdtotNetSep.Parameters.Add(CurentDiscount);
                    cmdtotNetSep.ExecuteNonQuery();
                    SqlDataAdapter apdtotNet = new SqlDataAdapter(cmdtotNetSep);
                    DataTable dtTotNet = new DataTable();
                    dtTotNet.Rows.Clear();
                    apdtotNet.Fill(dtTotNet);
                    double totNetSales = 0.00;
                    if (dtTotNet.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtTotNet.Rows[0]["NetSales"].ToString()))
                        {
                            totNetSales = Convert.ToDouble(dtTotNet.Rows[0]["NetSales"].ToString());
                        }
                    }
                    double Discount = 0.00;
                    Discount = string.IsNullOrEmpty(CurentDiscount.Value.ToString()) ? 0.00 : Convert.ToDouble(CurentDiscount.Value.ToString());

                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[12].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[12].Cells["S_Retail"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }
                    grdMonthSummary.Rows[12].Cells["S_Whole"].Value = fWholeAmt;
                    if (txt_reporton.Text == "Gross Amount")
                    {
                        grdMonthSummary.Rows[12].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales);
                    }
                    else
                    {
                        grdMonthSummary.Rows[12].Cells["S_Total"].Value = string.Format("{0:0.00}", totNetSales + Discount);
                    }

                }

                string fsumofTotal = string.Format("{0:0.00}", sumofTotal2);
                string fSumofWhole = string.Format("{0:0.00}", SumofWhole2);
                string fSumofReturn = string.Format("{0:0.00}", SumofReturn2);
                string fSumofRetail = string.Format("{0:0.00}", SumofRetail2);

                grdMonthSummary.Rows[14].Cells["S_Retail"].Value = fSumofRetail;
                grdMonthSummary.Rows[14].Cells["S_Whole"].Value = fSumofWhole;
                grdMonthSummary.Rows[14].Cells["S_Return"].Value = fSumofReturn;
                grdMonthSummary.Rows[14].Cells["S_Total"].Value = fsumofTotal;
                fSumofRetail="";
                fsumofTotal="";
                double totAmtretail = 0.00,totAmtHole=0.00;
                for (int j = 0; j < grdMonthSummary.Rows.Count; j++)
                {
                    if (grdMonthSummary.Rows[j].Cells["S_Total"].Value != null && grdMonthSummary.Rows[j].Cells["S_Total"].Value.ToString() != string.Empty)
                    {
                        if (j < 14)
                        {
                            totAmtretail += Convert.ToDouble(grdMonthSummary.Rows[j].Cells["S_Total"].Value);
                        }
                    }
                   
                }
                grdMonthSummary.Rows[14].Cells["S_Retail"].Value = string.Format("{0:0.00}", totAmtretail.ToString());
                grdMonthSummary.Rows[14].Cells["S_Total"].Value = string.Format("{0:0.00}", totAmtretail.ToString());

                for (int i = 0; i < grdMonthSummary.Columns.Count; i++)
                {
                    grdMonthSummary.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           
        }
        private void grdMonthSummary_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                string Currentmonth = grdMonthSummary.Rows[row].HeaderCell.Value.ToString();

                if (Currentmonth != "")
                {
                    // MessageBox.Show(Currentmonth);
                    chkbox.MonthName = Currentmonth;


                }
                chkbox.MonthName = Currentmonth;
                //frmDailySalesSummary frm = new frmDailySalesSummary();
                //this.Close();
                //frm.Show();

                frmDailySalesSummary frm = new frmDailySalesSummary();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
      
        private void grdMonthSummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                if (row != -1)
                {
                    var tempdate = grdMonthSummary.Rows[row].HeaderCell.Value;
                    if (tempdate != null)
                    {
                        string MonthName = grdMonthSummary.Rows[row].HeaderCell.Value.ToString();
                        // MessageBox.Show(MonthName);
                        chkbox.MonthName = MonthName;
                        chkbox.tCounterName = txt_countername.Text.Trim();
                        chkbox.tYearNew = (numYear.Value.ToString().Trim() == "") ? DateTime.Now.Year : Convert.ToDouble(numYear.Value);
                        //frmDailySalesSummary frm = new frmDailySalesSummary();
                        //this.Close();
                        //frm.Show();

                        frmDailySalesSummary frm = new frmDailySalesSummary();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        this.SendToBack();
                        frm.Show();
                        //this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("empty row is clicked");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_reporton_TextChanged(object sender, EventArgs e)
        {

        }
        string chk;
        SqlDataReader dreader;
        private void txt_countername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_countername.Text.Trim() != null && txt_countername.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dtTemp = new DataTable();
                    dtTemp.Rows.Clear();
                    // SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_name like '" + txt_countername.Text.Trim() + "%'", con);
                    SqlCommand cmd = new SqlCommand("sp_SalesSummarySelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "COUNTERNAME");
                    cmd.Parameters.AddWithValue("@tValue", txt_countername.Text.Trim());
                    SqlDataAdapter adp101 = new SqlDataAdapter(cmd);
                    adp101.Fill(dtTemp);
                    bool isChk = false;
                    for (int mn = 0; mn < dtTemp.Rows.Count; mn++)
                    {
                        isChk = true;
                        string tempStr = dtTemp.Rows[mn]["ctr_name"].ToString();
                        for (int i = 0; i < lst_counter.Items.Count; i++)
                        {
                            if (dtTemp.Rows[mn]["ctr_name"].ToString() == lst_counter.Items[i].ToString())
                            {

                                lst_counter.SetSelected(i, true);
                                txt_countername.Select();
                                chk = "1";
                                txt_countername.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }

                        }
                    }
                    con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_countername.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                }
                else
                {
                    chk = "1";

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;


                }
                else
                {
                    e.Handled = false;

                }
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txt_countername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lst_counter.SelectedIndex < lst_counter.Items.Count - 1)
                    {
                        lst_counter.SetSelected(lst_counter.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lst_counter.SelectedIndex > 0)
                    {
                        lst_counter.SetSelected(lst_counter.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    Pnl_counter.Visible = false;
                    lst_counter.Visible = false;
                    if (lst_counter.Text != "")
                    {
                        txt_countername.Text = lst_counter.SelectedItem.ToString();
                        grdMonthSummary.Focus();
                        CounterNameList();
                        loadmonthdetails();


                    }

                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void CounterNameList()
        {
            try
            {
                // con.Open();
                SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table", con);
                SqlDataAdapter asd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                lst_counter.Items.Clear();
                dt.Rows.Clear();
                asd.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        lst_counter.Items.Add(dt.Rows[k]["ctr_name"].ToString());
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_countername_Enter(object sender, EventArgs e)
        {
            Pnl_counter.Visible = true;
            lst_counter.Visible = true;
            CounterNameList();
        }

        private void txt_reporton_Enter(object sender, EventArgs e)
        {
            pnl_Amount.Visible = true;
            lst_ofAmount.Visible = true;
            lst_ofAmount.Focus();
            lst_ofAmount.SelectedIndex = 0;

            
        }

        private void txt_reporton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnl_Amount.Visible = false;
                lst_ofAmount.Visible = false;
                txt_countername.Focus();
                //loadmonthdetails();
                //string fsumofTotal = string.Format("{0:0.00}", sumofTotal);
                //string fSumofWhole = string.Format("{0:0.00}", SumofWhole);
                //string fSumofReturn = string.Format("{0:0.00}", SumofReturn);
                //string fSumofRetail = string.Format("{0:0.00}", SumofRetail);

                //grdMonthSummary.Rows[13].Cells["S_Retail"].Value = fSumofRetail;
                //grdMonthSummary.Rows[13].Cells["S_Whole"].Value = fSumofWhole;
                //grdMonthSummary.Rows[13].Cells["S_Return"].Value = fSumofReturn;
                //grdMonthSummary.Rows[13].Cells["S_Total"].Value = fsumofTotal;

            }
        }

        private void lst_ofAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_reporton.Text = lst_ofAmount.SelectedItem.ToString();

        }

        private void lst_ofAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnl_Amount.Visible = false;
                lst_ofAmount.Visible = false;
                txt_countername.Focus();

            }
        }

        private void grdMonthSummary_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int t_currentrow = grdMonthSummary.CurrentCell.RowIndex;

                    if (grdMonthSummary.Rows[t_currentrow].HeaderCell.Value != null)
                    {

                        string MonthName = grdMonthSummary.Rows[t_currentrow].HeaderCell.Value.ToString();

                        chkbox.MonthName = MonthName;

                        frmDailySalesSummary frm = new frmDailySalesSummary();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        this.SendToBack();
                        frm.Show();
                        //this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(" Selected Row is Incorrect");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           
        }

        private void frmSalesSummary_Load(object sender, EventArgs e)
        {
           // numYear.Value = DateTime.Now.Year;
            numYear.Select();

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

        }

        private void txt_reporton_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = true;
            Pnl_counter.Visible = false;
        }

        private void txt_countername_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = false;
            Pnl_counter.Visible = true;
        }

        private void btn_Exitss_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_reporton.Select();
            }
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
//        Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceSales = new Microsoft.Reporting.WinForms.ReportDataSource();
        

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Dataset.dsSalesSummary dsSalesSummaryObj = new Dataset.dsSalesSummary();
                for (int i = 0; i < grdMonthSummary.Rows.Count; i++)
                {
                    dsSalesSummaryObj.Tables["DataTable1"].Rows.Add(grdMonthSummary.Rows[i].HeaderCell.Value, Convert.ToString(grdMonthSummary.Rows[i].Cells[0].Value), Convert.ToString(grdMonthSummary.Rows[i].Cells[1].Value), Convert.ToString(grdMonthSummary.Rows[i].Cells[2].Value), Convert.ToString(grdMonthSummary.Rows[i].Cells[3].Value));
                }
                reportViewerSales.Reset();
                //  DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("dsSalesSummary", dsSalesSummaryObj.Tables["DataTable1"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);

                //reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcSalesSummary.rdlc";
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.rdlcSalesSummary.rdlc";
                //Passing Parmetes:
                ReportParameter rpYear = new ReportParameter("Year", Convert.ToString(numYear.Value), false);
                ReportParameter rpReportOn = new ReportParameter("ReportOn", Convert.ToString(txt_reporton.Text), false);
                ReportParameter rpCounter = new ReportParameter("Counter", Convert.ToString(txt_countername.Text), false);
                //ReportParameter rp2 = new ReportParameter("DateTo", "300");
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpYear, rpReportOn, rpCounter });
                dsSalesSummaryObj.Tables["DataTable1"].EndInit();
                reportViewerSales.RefreshReport();
                reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);

               
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
                reportViewerSales.PrintDialog();
                reportViewerSales.Clear();
                reportViewerSales.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}
