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
    public partial class frmDailySalesSummary : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        double SumofRetail = 0, SumofWhole = 0, SumofReturn = 0, sumofTotal = 0;
        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public frmDailySalesSummary()
        {
            InitializeComponent();
            try
            {
                funConnectionStateCheck();
                grdDailySummary.Rows.Add(33);
                grdDailySummary.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
                grdDailySummary.BackgroundColor = Color.White;
                grdDailySummary.RowHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
                label1.BackColor = Color.CornflowerBlue;
                //grdDailySummary.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                //grdDailySummary.RowHeadersDefaultCellStyle.BackColor = Color.Gray;

              //  DateTime year = DateTime.Now;
               // int Cuurentyear = Convert.ToInt16(year.Year.ToString());
                int Cuurentyear = Convert.ToInt16(chkbox.tYearNew);
                txt_counter.Text = chkbox.tCounterName;
                string currentmonth = chkbox.MonthName;
                int month = DateTime.Parse("1." + currentmonth + Cuurentyear).Month;
                DataSet dsTemp = new DataSet();
                dsTemp.Tables.Clear();
                SqlDataAdapter cmd = new SqlDataAdapter("select CONVERT(DATE,DATEADD(month," + month + "-1,DATEADD(year," + Cuurentyear + "-1900,0)),103) as tStartDate,CONVERT(DATE,DATEADD(day,-1,DATEADD(month," + month + ",DATEADD(year," + Cuurentyear + "-1900,0))),103) as tEndDate", con);
                cmd.Fill(dsTemp, "DATES");
                if (dsTemp.Tables["DATES"].Rows.Count > 0)
                {
                    dt_from.Value = DateTime.Parse(dsTemp.Tables["DATES"].Rows[0]["tStartDate"].ToString());
                    dt_to.Value = DateTime.Parse(dsTemp.Tables["DATES"].Rows[0]["tEndDate"].ToString());
                }
                DataTable dtTemp = new DataTable();
                dtTemp.Rows.Clear();
                SqlCommand cmdGetDate = new SqlCommand("getAllDaysBetweenTwoDate", con);
                cmdGetDate.CommandType = CommandType.StoredProcedure;
                cmdGetDate.Parameters.AddWithValue("@FromDate", dt_from.Value.ToString("yyyy-MM-dd"));
                cmdGetDate.Parameters.AddWithValue("@ToDate", dt_to.Value.ToString("yyyy-MM-dd"));
                SqlDataAdapter adp101 = new SqlDataAdapter(cmdGetDate);
                adp101.Fill(dtTemp);
                lst_ofAmount.SelectedIndex = 0;
                for (int mn = 0; mn < dtTemp.Rows.Count; mn++)
                {
                    grdDailySummary.Rows[mn].HeaderCell.Value = dtTemp.Rows[mn][0].ToString().Substring(0, 10);
                    //loadDailydetails(dtTemp.Rows[mn][0].ToString().Substring(0,10), mn);                
                    loadDailydetails(DateTime.Parse(dtTemp.Rows[mn][0].ToString()), mn);

                }

                string fToTotalNovember = string.Format("{0:0.00}", sumofTotal);
                string fWholeAmt = string.Format("{0:0.00}", SumofWhole);
                string fReturnAmt = string.Format("{0:0.00}", SumofReturn);
                string fRetailAmt = string.Format("{0:0.00}", SumofRetail);

                grdDailySummary.Rows[32].Cells["S_Return"].Value = fReturnAmt;
                grdDailySummary.Rows[32].Cells["S_Retail"].Value = fRetailAmt;
                grdDailySummary.Rows[32].Cells["S_Whole"].Value = fWholeAmt;
                grdDailySummary.Rows[32].Cells["S_Total"].Value = fToTotalNovember;

                grdDailySummary.Columns[0].Width = 380;
                grdDailySummary.Columns[1].Width = 160;
                grdDailySummary.Columns[2].Width = 200;
                grdDailySummary.Columns[3].Width = 200;
                grdDailySummary.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                foreach (DataGridViewColumn col in grdDailySummary.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                for (int i = 0; i < grdDailySummary.Columns.Count; i++)
                {
                    grdDailySummary.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                grdDailySummary.DefaultCellStyle.Font = new Font("Tahoma", 10);
                grdDailySummary.RowTemplate.Height = 25;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        string CounterNo;
        public void loadDailydetails(DateTime QueryDate,int row)
        {
            try
            {
                // string sartdate = QueryDate;
                //string enddate1 = enddate;
                double RetailAmt = 0, WholeAmt = 0, ReturnAmt = 0;

                DataTable dtTemp = new DataTable();
                dtTemp.Rows.Clear();
                SqlCommand cmdCounterNo = new SqlCommand("sp_SalesSummarySelectSingle", con);
                cmdCounterNo.CommandType = CommandType.StoredProcedure;
                cmdCounterNo.Parameters.AddWithValue("@tActionType", "COUNTER");
                cmdCounterNo.Parameters.AddWithValue("@tValue", txt_counter.Text.Trim());
                SqlDataAdapter adp101 = new SqlDataAdapter(cmdCounterNo);
                adp101.Fill(dtTemp);

                CounterNo = "";
                if (dtTemp.Rows.Count > 0)
                {
                    CounterNo = dtTemp.Rows[0][0].ToString();
                }

                SqlCommand cmdApril = new SqlCommand("sp_DailySalesSummary", con);
                cmdApril.CommandType = CommandType.StoredProcedure;
                cmdApril.Parameters.AddWithValue("@tActionType", (txt_reportOn.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                cmdApril.Parameters.AddWithValue("@tDate", QueryDate);
                cmdApril.Parameters.AddWithValue("@tCounterNo", CounterNo);
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
                SumofRetail = SumofRetail + RetailAmt;
                SumofReturn = SumofReturn + ReturnAmt;
                sumofTotal = sumofTotal + TotalNovember;
                SumofWhole = SumofWhole + WholeAmt;
                grdDailySummary.Rows[row].Cells["S_Return"].Value = fReturnAmt;
                grdDailySummary.Rows[row].Cells["S_Retail"].Value = fRetailAmt;
                grdDailySummary.Rows[row].Cells["S_Whole"].Value = fWholeAmt;
                grdDailySummary.Rows[row].Cells["S_Total"].Value = fToTotalNovember;
              

               
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
                      
        }

      
        private void grdDailySummary_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;

                if (row != -1)
                {
                    var tempdate = grdDailySummary.Rows[row].HeaderCell.Value;

                    if (tempdate != null)
                    {
                        string SelectedDate = grdDailySummary.Rows[row].HeaderCell.Value.ToString();
                        DateTime DtselectedDate = Convert.ToDateTime(SelectedDate);
                        string selectedAlteredDate = DtselectedDate.ToString("yyyy-MM-dd");
                        //MessageBox.Show(SelectedDate);
                        chkbox.DateSalesEntry = selectedAlteredDate;
                        chkbox.tCounterName = txt_counter.Text.Trim();
                        //frmSalesSummaryDetails frm = new frmSalesSummaryDetails();
                        //this.Close();
                        //frm.Show();


                        frmSalesSummaryDetails frm = new frmSalesSummaryDetails();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Empty Field is Selected");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
     

        private void grdDailySummary_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                var temdate = grdDailySummary.Rows[row].HeaderCell.Value;
                if (temdate != null)
                {
                    string SelectedDate = grdDailySummary.Rows[row].HeaderCell.Value.ToString();
                    //MessageBox.Show(SelectedDate);
                    DateTime DtselectedDate = Convert.ToDateTime(SelectedDate);
                    string selectedAlteredDate = DtselectedDate.ToString("yyyy-MM-dd");
                    chkbox.DateSalesEntry = selectedAlteredDate;
                    chkbox.tCounterName = txt_counter.Text.Trim();
                    frmSalesSummaryDetails frm = new frmSalesSummaryDetails();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Empty row is selected");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           
            //frmDailySalesSummary frm = new frmDailySalesSummary();
            //this.Close();
            //frm.Show();
        }
       
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                frmSalesSummary frm = new frmSalesSummary();
                frm.BringToFront();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_reportOn_Enter(object sender, EventArgs e)
        {
            pnl_Amount.Visible = true;
            lst_ofAmount.Visible = true;
            lst_ofAmount.Focus();
            lst_ofAmount.SelectedIndex = 0;


        }

        private void lst_ofAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_reportOn.Text = lst_ofAmount.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_ofAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pnl_Amount.Visible = false;
                    lst_ofAmount.Visible = false;
                    txt_counter.Focus();

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_counter_Enter(object sender, EventArgs e)
        {
            try
            {
                CounterNameList();
                Pnl_counter.Visible = true;
                lst_counter.Visible = true;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }
        SqlDataReader dreader =null;
        string chk;
        private void txt_counter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_counter.Text.Trim() != null && txt_counter.Text.Trim() != "")
                {
                    funConnectionStateCheck();
                    SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_name like @tCounterName+'%'", con);
                    cmd.Parameters.AddWithValue("@tCounterName", txt_counter.Text.Trim());
                    DataTable dtNew101 = new DataTable();
                    dtNew101.Rows.Clear();
                    SqlDataAdapter adp101 = new SqlDataAdapter(cmd);
                    adp101.Fill(dtNew101);

                    bool isChk = false;
                    for (int mn = 0; mn < dtNew101.Rows.Count; mn++)
                    {
                        isChk = true;
                        string tempStr = dtNew101.Rows[mn]["ctr_name"].ToString();
                        for (int i = 0; i < lst_counter.Items.Count; i++)
                        {
                            if (dtNew101.Rows[mn]["ctr_name"].ToString() == lst_counter.Items[i].ToString())
                            {

                                lst_counter.SetSelected(i, true);
                                txt_counter.Select();
                                chk = "1";
                                txt_counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }

                        }
                    }
                    // con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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
            try
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
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_counter_KeyDown(object sender, KeyEventArgs e)        
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
                    SumofRetail = 0; SumofWhole = 0; SumofReturn = 0; sumofTotal = 0;
                    if (lst_counter.Text != "")
                    {
                        // txt_reportOn.Text
                        txt_counter.Text = lst_counter.SelectedItem.ToString();
                        // CounterNameList();                    
                        grdDailySummary.Focus();
                        string currentstartdate = dt_from.Text;
                        string cuurentenddate = dt_to.Text;
                        funConnectionStateCheck();
                        DataTable dtTemp = new DataTable();
                        dtTemp.Rows.Clear();
                        SqlCommand cmdGetDate = new SqlCommand("getAllDaysBetweenTwoDate", con);
                        cmdGetDate.CommandType = CommandType.StoredProcedure;
                        cmdGetDate.Parameters.AddWithValue("@FromDate", dt_from.Value.ToString("yyyy-MM-dd"));
                        cmdGetDate.Parameters.AddWithValue("@ToDate", dt_to.Value.ToString("yyyy-MM-dd"));
                        dreader = cmdGetDate.ExecuteReader();
                        dtTemp.Load(dreader);
                        // lst_ofAmount.SelectedIndex = 0;
                        grdDailySummary.Rows.Clear();
                        grdDailySummary.Rows.Add(dtTemp.Rows.Count + 1);
                        SumofRetail = 0; SumofWhole = 0; SumofReturn = 0; sumofTotal = 0;
                        for (int mn = 0; mn < dtTemp.Rows.Count; mn++)
                        {
                            grdDailySummary.Rows[mn].HeaderCell.Value = dtTemp.Rows[mn][0].ToString().Substring(0, 10);
                            loadDailydetails(DateTime.Parse(dtTemp.Rows[mn][0].ToString()), mn);
                        }

                        string fToTotalNovember = string.Format("{0:0.00}", sumofTotal);
                        string fWholeAmt = string.Format("{0:0.00}", SumofWhole);
                        string fReturnAmt = string.Format("{0:0.00}", SumofReturn);
                        string fRetailAmt = string.Format("{0:0.00}", SumofRetail);

                        //Changed the code by Anbu:
                        double GrosAmount = 0.00, totalDiscount = 0.00,TotalNetAmt=0.00;
                        if (txt_reportOn.Text.ToString().Trim() == "Gross Amount")
                        {

                            //Alter the Code TotalAmount:
                            SqlCommand cmdGrosAmount = new SqlCommand(@" select convert(numeric(18,2), (Disc1.Disc-Disc2.RDisc)) as NetSales from 
         (Select (case when SUM(Amount) is null then 0 else SUM(Amount) end) as Disc from stktrn_table where strn_type=1 and ctr_no=@CounterNo and strn_rtno=0  and Strn_Cancel=0 and strn_date between @FromDate and @ToDate ) as Disc1,
         (Select (case when SUM(Amount) is null then 0 else SUM(Amount) end) as RDisc from stktrn_table where strn_type=2 and ctr_no=@CounterNo  and strn_rtno<>0 and Strn_Cancel=0 and strn_date between @FromDate and @ToDate) as Disc2", con);
                            cmdGrosAmount.Parameters.AddWithValue("@FromDate", dt_from.Value.Year + "/" + dt_from.Value.Month + "/" + dt_from.Value.Day);
                            cmdGrosAmount.Parameters.AddWithValue("@ToDate", dt_to.Value.Year + "/" + dt_to.Value.Month + "/" + dt_to.Value.Day);
                            cmdGrosAmount.Parameters.AddWithValue("@CounterNo", CounterNo.ToString());
                            GrosAmount = string.IsNullOrEmpty(cmdGrosAmount.ExecuteScalar().ToString()) ? 0.00 : Convert.ToDouble(cmdGrosAmount.ExecuteScalar());

                            //Total Discount:
                            SqlCommand cmdDiscount = new SqlCommand(@"select (convert(numeric(18,2), (Disc1.Disc-Disc2.RDisc))) As TotalDiscount from 
      (Select (case when (SUM(disc_amt+Othdisc_Amt+spl_discamt)) is null then 0 else (SUM(disc_amt+Othdisc_Amt+spl_discamt)) end) as Disc from stktrn_table where strn_type=1 and ctr_no=@CounterNo  and strn_rtno=0 and Strn_Cancel=0 and strn_date between @FromDate and @ToDate ) as Disc1,
      (Select (case when (SUM(disc_amt+Othdisc_Amt+spl_discamt)) is null then 0 else (SUM(disc_amt+Othdisc_Amt+spl_discamt)) end) as RDisc from stktrn_table where strn_type=2 and ctr_no=@CounterNo and strn_rtno<>0 and Strn_Cancel=0 and strn_date between @FromDate and @ToDate ) as Disc2", con);
                            cmdDiscount.Parameters.AddWithValue("@FromDate", dt_from.Value.Year + "/" + dt_from.Value.Month + "/" + dt_from.Value.Day);
                            cmdDiscount.Parameters.AddWithValue("@ToDate", dt_to.Value.Year + "/" + dt_to.Value.Month + "/" + dt_to.Value.Day);
                            cmdDiscount.Parameters.AddWithValue("@CounterNo", CounterNo.ToString());
                            totalDiscount = string.IsNullOrEmpty(cmdDiscount.ExecuteScalar().ToString()) ? 0.00 : Convert.ToDouble(cmdDiscount.ExecuteScalar().ToString());




                            //grdDailySummary.Rows[row].Cells["S_Return"].Value = string.Format("{0:0.00}", GrosAmount + totalDiscount);
                            //grdDailySummary.Rows[row].Cells["S_Total"].Value = string.Format("{0:0.00}", GrosAmount + totalDiscount);
                        }
                        else if (txt_reportOn.Text.ToString().Trim() == "Nett Amount")
                        {
                            SqlCommand cmdNetAmount = new SqlCommand(@"select convert(numeric(18,2), (Disc1.Disc-Disc2.RDisc)) as NetSales from 
		(Select (case when SUM(net_amt) is null then 0 else SUM(net_amt) end) as Disc from stktrn_table where strn_type=1 and strn_rtno=0 and ctr_no=@CounterNo  and Strn_Cancel=0 and strn_date between @FromDate and @ToDate ) as Disc1,
		(Select (case when SUM(net_amt) is null then 0 else SUM(net_amt) end) as RDisc from stktrn_table where strn_type=2 and ctr_no=@CounterNo  and strn_rtno<>0 and Strn_Cancel=0 and strn_date between @FromDate and @ToDate) as Disc2", con);
                            cmdNetAmount.Parameters.AddWithValue("@FromDate", dt_from.Value.Year + "/" + dt_from.Value.Month + "/" + dt_from.Value.Day);
                            cmdNetAmount.Parameters.AddWithValue("@ToDate", dt_to.Value.Year + "/" + dt_to.Value.Month + "/" + dt_to.Value.Day);
                            cmdNetAmount.Parameters.AddWithValue("@CounterNo", CounterNo.ToString());
                            TotalNetAmt = string.IsNullOrEmpty(cmdNetAmount.ExecuteScalar().ToString()) ? 0.00 : Convert.ToDouble(cmdNetAmount.ExecuteScalar().ToString());
                        }

                        //grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Return"].Value = fReturnAmt;
                        //grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Retail"].Value = fRetailAmt;
                        //grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Whole"].Value = fWholeAmt;
                        //grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Total"].Value = fToTotalNovember;


                        grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Return"].Value = fReturnAmt;
                        grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Retail"].Value = GrosAmount > 0 ?   string.Format("{0:0.00}", GrosAmount + totalDiscount): string.Format("{0:0.00}", TotalNetAmt);
                        grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Whole"].Value = fWholeAmt;
                        grdDailySummary.Rows[dtTemp.Rows.Count + 1].Cells["S_Total"].Value = GrosAmount > 0 ? string.Format("{0:0.00}", GrosAmount + totalDiscount) : string.Format("{0:0.00}", TotalNetAmt); 

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
            //con.Close();
            //con.Open();
            try
            {
                funConnectionStateCheck();
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
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
          //  con.Close();
        }

        private void txt_reportOn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pnl_Amount.Visible = false;
                    lst_ofAmount.Visible = false;
                    txt_counter.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_counter_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void grdDailySummary_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int t_currentrow = grdDailySummary.CurrentCell.RowIndex;
                    var temdate = grdDailySummary.Rows[t_currentrow].HeaderCell.Value;
                    if (temdate != null)
                    {
                        string SelectedDate = grdDailySummary.Rows[t_currentrow].HeaderCell.Value.ToString();
                        //MessageBox.Show(SelectedDate);

                        DateTime DtselectedDate = Convert.ToDateTime(SelectedDate);
                        string selectedAlteredDate = DtselectedDate.ToString("yyyy-MM-dd");
                        chkbox.DateSalesEntry = selectedAlteredDate;

                        frmSalesSummaryDetails frm = new frmSalesSummaryDetails();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        this.Hide();

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

        private void txt_reportOn_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = true;
            Pnl_counter.Visible = false;
        }

        private void txt_counter_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = false;
            Pnl_counter.Visible = true;
        }

        private void lst_counter_Click(object sender, EventArgs e)
        {
            txt_counter.Text = lst_counter.SelectedItem.ToString();
        }

        private void lst_ofAmount_Click(object sender, EventArgs e)
        {
            txt_reportOn.Text = lst_ofAmount.SelectedItem.ToString();
        }

        private void dt_from_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dt_to.Select();
            }
        }

        private void dt_to_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              txt_reportOn.Select();
            }
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        private void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                Dataset.dsSalesSummary dsSalesSummaryObj = new Dataset.dsSalesSummary();
                for (int i = 0; i <grdDailySummary.Rows.Count; i++)
                {
                    dsSalesSummaryObj.Tables["DataTable2"].Rows.Add(grdDailySummary.Rows[i].HeaderCell.Value, Convert.ToString(grdDailySummary.Rows[i].Cells[0].Value), Convert.ToString(grdDailySummary.Rows[i].Cells[1].Value), Convert.ToString(grdDailySummary.Rows[i].Cells[2].Value), Convert.ToString(grdDailySummary.Rows[i].Cells[3].Value));
                }
                reportViewerSales.Reset();
                //  DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DataTable2"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);

                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.rdlcDailySalesSummary.rdlc";
                //Passing Parmetes:
              //  ReportParameter rpYear = new ReportParameter("Year", Convert.ToString(numYear.Value), false);
                ReportParameter rpReportOn = new ReportParameter("ReportOn", Convert.ToString(txt_reportOn.Text), false);
                ReportParameter rpCounter = new ReportParameter("Counter", Convert.ToString(txt_counter.Text), false);
                ReportParameter rpFrom = new ReportParameter("From",Convert.ToString(dt_from.Value.Day+"/"+dt_from.Value.Month+"/"+dt_from.Value.Year), false);
                ReportParameter rpTo = new ReportParameter("To", Convert.ToString(dt_to.Value.Day + "/" + dt_to.Value.Month + "/" + dt_to.Value.Year), false);
                //ReportParameter rp2 = new ReportParameter("DateTo", "300");
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn, rpCounter,rpFrom,rpTo });
                dsSalesSummaryObj.Tables["DataTable2"].EndInit();
                reportViewerSales.RefreshReport();
                reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
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
        private void frmDailySalesSummary_Load(object sender, EventArgs e)
        {
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
    }
}
