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
    public partial class ItemLedger : Form
    {
        string id;
        string tAmountType;
        
       public ItemLedger()
        {
            InitializeComponent();

            gridLedger.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

            gridLedger.BackgroundColor = Color.White;
            if (passingvalues.id_number_item_leder == "")
            {
            }
            else
            {
                id = passingvalues.id_number_item_leder;


                DateTime fromdate = new DateTime();
                fromdate = Convert.ToDateTime(passingvalues.tStartDateParthi.Year + "/" + passingvalues.tStartDateParthi.Month + "/" + passingvalues.tStartDateParthi.Day);
                DateTime enddate = new DateTime();
                enddate = Convert.ToDateTime(passingvalues.tToDateParthi.Year + "/" + passingvalues.tToDateParthi.Month + "/" + passingvalues.tToDateParthi.Day);
                // dtpFrom.Value = passingvalues.from_date1;
                dtpFrom.Value = fromdate;
                //dtpTo.Value = passingvalues.end_date1;
                dtpTo.Value = enddate;
                tAmountType = passingvalues.tAmountType;
            }
            dtGrid.Columns.Add("Date", typeof(string));
            dtGrid.Columns.Add("Particulars", typeof(string));
            dtGrid.Columns.Add("Type", typeof(string));
            dtGrid.Columns.Add("RecQty", typeof(string));
            dtGrid.Columns.Add("IssuQty", typeof(string));
            dtGrid.Columns.Add("Value", typeof(string));
            dtGrid.Columns.Add("Strn_no", typeof(string));
        }
        double rcqty = 0.00;
        double issue_qty = 0.00;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void ItemLedger_Load(object sender, EventArgs e)
        {
            dtpFrom.Select();           
            ////if (passingvalues.numbervaluestoledger != "1")
            ////{
            ////    select_query();
            ////    gridcalculation();              
            ////    txtremarks.Focus();
            ////}
            DataTable dtTemp = new DataTable();
            dtTemp.Rows.Clear();
            SqlDataAdapter cmd = new SqlDataAdapter("Select * from item_table where item_no='" + id + "'", con);          
            cmd.Fill(dtTemp);
            if (dtTemp.Rows.Count > 0)
            {
                txtlederof.Text = dtTemp.Rows[0]["Item_Name"].ToString();
            }

            funGridDetails();
            //  gridcalculation();
            pnllist.Visible = false;
            pnltype.Visible = false;
            pnlcancel.Visible = false;
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

        }

        DataTable dtGrid = new DataTable();

        public void funGridDetails()
        {
            if (txtlederof.Text.Trim() != "")
            {
                if (id != null && id.ToString().Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    dtGrid.Rows.Clear();
                    //SqlCommand cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=14) THEN 'NETS' ELSE 'Cash Purchase' END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' when strn_type=2 then 'Sales Ret' ELSE 'Purchase' END) as Type,(CASE WHEN strn_type=0 THEN sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) WHEN strn_type=3 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 THEN sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN sum(Amount) ELSE sum(tot_amt) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ", con);
                    //Parthi Coding Older:
                    // SqlCommand cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' ELSE 'Cash Purchase'  END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' when strn_type=2 then 'Sales Ret' when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' ELSE 'Purchase'  END) as Type,(CASE WHEN strn_type=0 THEN sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 THEN sum(nt_Qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ", con);
                    //Me Alter
                    // SqlCommand cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' When strnParty_no=8 Then 'Cash Purchase'  END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' when strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=0 THEN sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 THEN sum(nt_Qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ", con);
                    SqlCommand cmd = null;
                    if (txtcancel.Text.Trim() == "Cancelled")
                    {
                        cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_Cancel=1) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' When strnParty_no=8 Then 'Cash Purchase' Else  (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no)  END) as Particulars,(CASE WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales' WHEN strn_type=1 and Strn_Cancel=1 THEN 'Cancel' when strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=1 and Strn_Cancel=1 then sum(nt_qty) When StrnParty_no=8 then sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) When strn_type=38 Then sum(nt_qty)  WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty)  When strn_type=37 Then sum(nt_qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and strn_Cancel=1 and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ,Strn_Cancel ", con);
                    }
                    else if (txtcancel.Text.Trim() == "ALL")
                    {
                        // cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strn_Cancel=0 and strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' When strnParty_no=8 Then 'Cash Purchase' Else  (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no)  END) as Particulars,(CASE WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales' WHEN strn_type=1 and Strn_Cancel=1 THEN 'Cancel' when strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=1 and Strn_Cancel=1 then sum(nt_qty) When StrnParty_no=8 then sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) When strn_type=38 Then sum(nt_qty)  WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty)  When strn_type=37 Then sum(nt_qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no,Strn_Cancel ", con);
                        cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strn_Cancel=0 and strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' When strnParty_no=8 Then 'Cash Purchase' Else  (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no)  END) as Particulars,(CASE WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales'WHEN strn_type=1 and Strn_Cancel=1 THEN 'Cancel' WHEN strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=1 and Strn_Cancel=1 then sum(nt_qty) When StrnParty_no=8 then sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) When strn_type=38 Then sum(nt_qty)  WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty)  When strn_type=37 Then sum(nt_qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no,Strn_Cancel ", con);
                    }
                    else if (txtcancel.Text.Trim() == "Not Cancelled")
                    {
                        //cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_Cancel=1) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' When strnParty_no=8 Then 'Cash Purchase' Else  (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no)  END) as Particulars,(CASE WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales'  when strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=1 and Strn_Cancel=1 then sum(nt_qty) When StrnParty_no=8 then sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) When strn_type=38 Then sum(nt_qty)  WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty)  When strn_type=37 Then sum(nt_qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and strn_Cancel=1 and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ,Strn_Cancel ", con);
                        cmd = new SqlCommand(" select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_Cancel=1) AND strnParty_no=2) THEN 'Cash Sales' " +
                                             " WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' " +
                                             " WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' " +
                                             " When strnParty_no=8 Then 'Cash Purchase' Else  (select Ledger_name from Ledger_table " +
                                             " where Ledger_no=stktrn_table.StrnParty_no)  END) as Particulars,(CASE WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales' " +
                                             " when strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' " +
                                             " when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' " +
                                             " When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=1 and Strn_Cancel=1 then sum(nt_qty) " +
                                             " When StrnParty_no=8 then sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) When strn_type=38 Then sum(nt_qty)  WHEN strn_type=3 then sum(nt_Qty)  " +
                                             " WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty)  " +
                                             " When strn_type=37 Then sum(nt_qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty, strn_no  " +
                                             " from stktrn_table where strn_type='1' and strn_Cancel=0 and item_no=@tItemNo " +
                                             " and Strn_date between @tDateFrom and @tDateTo " +
                                             " group by Strn_date,strn_type,strnParty_no,strn_no ,Strn_Cancel ", con);

                    }
                    cmd.Parameters.AddWithValue("@tItemNo", id);
                    cmd.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                    cmd.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                    cmd.Parameters.AddWithValue("@tAmountType", tAmountType);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dp_chke = new DataTable();
                    dp_chke.Rows.Clear();
                    adp.Fill(dp_chke);
                    adp.Fill(dtGrid);
                    double tPurchaseQty = 0;
                    Double tSalesQty = 0;
                    for (int mn = 0; mn < dtGrid.Rows.Count; mn++)
                    {

                        if (dtGrid.Rows[mn]["RecQty"].ToString() != "")
                        {
                            tPurchaseQty += double.Parse(dtGrid.Rows[mn]["RecQty"].ToString());
                        }
                        if (dtGrid.Rows[mn]["IssuQty"].ToString() != "")
                        {
                            tSalesQty += double.Parse(dtGrid.Rows[mn]["IssuQty"].ToString());
                        }

                    }
                    gridLedger.DataSource = dtGrid;
                    dtGrid.Rows.Add("", "", "", "", "", "", "");

                    SqlCommand cmdOpening = new SqlCommand("sp_OpeningQty", con);
                    cmdOpening.CommandType = CommandType.StoredProcedure;
                    cmdOpening.Parameters.AddWithValue("@tActionType", "All");
                    cmdOpening.Parameters.AddWithValue("@tItemNo", id);
                    cmdOpening.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                    cmdOpening.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                    SqlParameter para = new SqlParameter("@tFinalOpenQty", SqlDbType.VarChar, 50);
                    para.Direction = ParameterDirection.Output;
                    cmdOpening.Parameters.Add(para);
                    SqlParameter para1 = new SqlParameter("@tItemCost", SqlDbType.VarChar, 50);
                    para1.Direction = ParameterDirection.Output;
                    cmdOpening.Parameters.Add(para1);

                    //Closing Qty Get:
                    SqlParameter CloingQty = new SqlParameter("@tClosingStock", SqlDbType.VarChar, 50);
                    CloingQty.Direction = ParameterDirection.Output;
                    cmdOpening.Parameters.Add(CloingQty);
                    //Findish
                    cmdOpening.ExecuteNonQuery();
                    double tot5 = 0.00, tot6 = 0.00, totClosingQty = 0.00;
                    tot5 = (para.Value.ToString() == "" ? 0.00 : Convert.ToDouble(para.Value.ToString()));//Opening Qty:
                    tot6 = para1.Value.ToString() == "" ? 0.00 : double.Parse(para1.Value.ToString());//item_cost:
                    totClosingQty = CloingQty.Value.ToString() == "" ? 0.00 : double.Parse(CloingQty.Value.ToString());//Closing Qty

                    string TotalAmt = (tot6 * tot5).ToString("0.00");
                    TotalAmt = TotalAmt.TrimStart('-');

                    SqlCommand cmd_onpenqty = new SqlCommand("select sum(nt_Qty) as Qty from stktrn_table where  strn_type='0' and item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ", con);
                    cmd_onpenqty.Parameters.AddWithValue("@tItemNo", id);
                    cmd_onpenqty.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                    cmd_onpenqty.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                    SqlDataAdapter dp = new SqlDataAdapter(cmd_onpenqty);
                    DataTable dt_opneqty = new DataTable();
                    dt_opneqty.Rows.Clear();
                    double oprnqty = 0.00;
                    dp.Fill(dt_opneqty);
                    if (dt_opneqty.Rows.Count > 0)
                    {
                        oprnqty = Convert.ToDouble(dt_opneqty.Rows[0]["Qty"].ToString());
                    }
                    // dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", para.Value.ToString(), "",(tot5*tot6).ToString(), "");

                    //  else
                    {
                        //this is the orginal process:
                        //dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", para.Value.ToString(), "", (tot5 * tot6).ToString(), "");
                        //dtGrid.Rows.Add("", "                  Total", "", oprnqty, tSalesQty, "", "");
                        //double tClosingQty = 0;
                        //tClosingQty = (tot5 + (tPurchaseQty - tSalesQty));
                        //dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", tClosingQty, "", ((tClosingQty * tot6) == 0) ? "0.00" : string.Format("{0:0.00}", (tClosingQty * tot6)), "");
                        //gridLedger.DataSource = dtGrid;
                        //Me Alter The Process:
                        //string totAmount = (tot5 * tot6).ToString("0.00");
                        //totAmount = totAmount.TrimStart('-');
                        //if (oprnqty != 0.0 && oprnqty.ToString().Trim() != "")
                        //{
                        //    dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", oprnqty.ToString(), "", (totAmount).ToString(), "");
                        //}
                        //else
                        //{
                        //    string TQCostAmt = (oprnqty * tot6).ToString("0.00");
                        //    TQCostAmt = TQCostAmt.TrimStart('-');
                        //    dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", oprnqty.ToString(), "", (TQCostAmt).ToString(), "");
                        //}
                        //dtGrid.Rows.Add("", "                  Total", "", tPurchaseQty, tSalesQty, "", "");
                        double tClosingQty = 0;
                        if (tPurchaseQty > 0)
                        {
                            if (tot5 > 0)
                            {
                                tClosingQty = ((oprnqty + tPurchaseQty) - tSalesQty);
                            }
                            else
                            {
                                tClosingQty = ((tPurchaseQty) - tSalesQty);
                            }
                        }
                        else if (oprnqty >= 0)
                        {
                            tClosingQty = ((oprnqty - tSalesQty));
                        }
                        //getting previous Date Dates:
                        DateTime dt1 = new DateTime();
                        //Finding Last Day OPening and current opening balance:
                        dt1 = Convert.ToDateTime(dtpFrom.Value.AddDays(-1));
                        //Total Qty between to date of sales, purchase and open
                        SqlCommand cmd_TotalBalance = new SqlCommand("Select sum(nt_qty) from stktrn_table Where item_no=@tItemNo and Strn_Cancel<>1 and Strn_date  between @tDateFrom and @tDateTo", con);
                        cmd_TotalBalance.Parameters.AddWithValue("@tItemNo", id);
                        cmd_TotalBalance.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                        cmd_TotalBalance.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                        double cmd_TotalBalance1 = 0.00;
                        DataTable dt_balanceamount = new DataTable();
                        SqlDataAdapter adpbalanceamaount = new SqlDataAdapter(cmd_TotalBalance);
                        dt_balanceamount.Rows.Clear();
                        adpbalanceamaount.Fill(dt_balanceamount);
                        if (dt_balanceamount.Rows.Count > 0)
                        {
                            if (dt_balanceamount.Rows[0][0].ToString() != string.Empty && dt_balanceamount.Rows[0][0].ToString() != null)
                            {
                                cmd_TotalBalance1 = Convert.ToDouble(dt_balanceamount.Rows[0][0].ToString());
                            }
                        }
                        else
                        {
                            cmd_TotalBalance1 = 0.00;
                        }
                        //   cmd_TotalBalance1 = ((Convert.ToString(cmd_TotalBalance.ExecuteScalar().ToString()) == null) || Convert.ToString(cmd_TotalBalance.ExecuteScalar()).ToString() == string.Empty ? 0 : Convert.ToInt32(cmd_TotalBalance.ExecuteScalar().ToString()));
                        //Current date opening  balance have go to Top:
                        SqlCommand cmd_Closingqty = new SqlCommand("Select sum(nt_qty) from stktrn_table Where item_no=@tItemNo and strn_type=0  and Strn_date  between @tDateFrom and @tDateTo", con);
                        cmd_Closingqty.Parameters.AddWithValue("@tItemNo", id);
                        cmd_Closingqty.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                        cmd_Closingqty.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                        // cmd_Closingqty.Parameters.AddWithValue("@Sdate",new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day).AddDays(-1));
                        double OpeningQty1 = 0.00, cmd_Closingqty1 = 0.00;
                        OpeningQty1 = (cmd_Closingqty.ExecuteScalar().ToString() == null || cmd_Closingqty.ExecuteScalar().ToString() == string.Empty ? 0 : Convert.ToInt32(cmd_Closingqty.ExecuteScalar().ToString()));
                        //  cmd_Closingqty1 = Convert.ToDouble((cmd_TotalBalance1) - (OpeningQty1));
                        if (OpeningQty1 != 0.00 && OpeningQty1.ToString() != string.Empty)
                        {
                            DataRow row = dtGrid.NewRow();
                            object[] objrow = new object[] { "", "Openiong Stock", "", OpeningQty1.ToString("0.00"), "", "", "" };

                            row.ItemArray = objrow;
                            dtGrid.Rows.InsertAt(row, 0);
                        }
                        double RecQtytot = 0, totSalQty = 0;
                        for (int km = 0; km < dtGrid.Rows.Count - 1; km++)
                        {
                            RecQtytot += string.IsNullOrEmpty(dtGrid.Rows[km]["RecQty"].ToString()) ? 0.00 : Convert.ToDouble(dtGrid.Rows[km]["RecQty"].ToString());
                            totSalQty += string.IsNullOrEmpty(dtGrid.Rows[km]["IssuQty"].ToString()) ? 0.00 : Convert.ToDouble(dtGrid.Rows[km]["IssuQty"].ToString());
                        }
                        //Previous Date  total Qty Amount:thats called opening balance:
                        SqlCommand cmdPrevoidatetot = new SqlCommand("Select sum(nt_qty) from stktrn_table Where item_no=@tItemNo and Strn_Cancel<>1  and Strn_date=@tDateFrom", con);
                        double prevoiusOpningBalance = 0.00;

                        cmdPrevoidatetot.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day).AddDays(-1));
                        cmdPrevoidatetot.Parameters.AddWithValue("@tItemNo", id);
                        //  prevoiusOpningBalance = (cmdPrevoidatetot.ExecuteScalar().ToString() == null || cmdPrevoidatetot.ExecuteScalar().ToString() == string.Empty ? 0 : Convert.ToInt32(cmdPrevoidatetot.ExecuteScalar().ToString()));
                        SqlDataAdapter adpprevoiusOpningBalance = new SqlDataAdapter(cmdPrevoidatetot);
                        DataTable dtpprevoiusOpningBalance = new DataTable();
                        dtpprevoiusOpningBalance.Rows.Clear();
                        adpprevoiusOpningBalance.Fill(dtpprevoiusOpningBalance);
                        if (dtpprevoiusOpningBalance.Rows.Count > 0)
                        {
                            if (dtpprevoiusOpningBalance.Rows[0][0].ToString() != string.Empty && dtpprevoiusOpningBalance.Rows[0][0].ToString() != null)
                            {
                                prevoiusOpningBalance = Convert.ToDouble(dtpprevoiusOpningBalance.Rows[0][0].ToString());
                            }
                        }
                        else
                        {
                            prevoiusOpningBalance = 0.00;
                        }
                        //Checking:
                        // string dateeeeee=new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day).AddDays(-1).ToString();

                        //Getting Openong Qty:
                        if (!string.IsNullOrEmpty(txtlederof.Text))
                        {
                            SqlCommand tcmdPuSal = new SqlCommand("select distinct (select  Sum(nt_qty) from stktrn_table where strn_type=0 and item_no=@ItemName and strn_date<=@FromDate) As OpenQty,(select  Sum(nt_qty) from stktrn_table where strn_type=3 and item_no=@ItemName and strn_date<=@FromDate ) As PurQty, (select  Convert(numeric(18,2),sum(nt_qty)) from stktrn_table where strn_type=1 and item_no=@ItemName and strn_date<=@FromDate ) As SalQty from stktrn_table where item_no=@ItemName and strn_date<=@FromDate ", con);
                            tcmdPuSal.Parameters.AddWithValue("@FromDate", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                            tcmdPuSal.Parameters.AddWithValue("@ItemName", id);
                            SqlDataAdapter tSPur = new SqlDataAdapter(tcmdPuSal);
                            DataTable dtSalPur = new DataTable();
                            dtSalPur.Rows.Clear();
                            tSPur.Fill(dtSalPur);
                            if (dtSalPur.Rows.Count > 0)
                            {
                            }
                        }
                        string totAmount = (tot5 * tot6).ToString("0.00");
                        totAmount = totAmount.TrimStart('-');
                        if (oprnqty != 0.0 && oprnqty.ToString().Trim() != "")
                        {
                            //dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", 0.00, prevoiusOpningBalance.ToString(), "", "");
                            dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", string.Format("{0:0.00}", (oprnqty)), 0.00, "", "");
                        }
                        else
                        {
                            string TQCostAmt = (oprnqty * tot6).ToString("0.00");
                            TQCostAmt = TQCostAmt.TrimStart('-');
                            // dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", 0.00, prevoiusOpningBalance.ToString(), "", "");
                            dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", string.Format("{0:0.00}", oprnqty), 0.00, "", "");
                        }
                        //  dtGrid.Rows.Add("", "                  Total", "", RecQtytot.ToString("0.00"), totSalQty.ToString("0.00"), "", "");
                        dtGrid.Rows.Add("", "                  Total", "", tPurchaseQty, totSalQty.ToString("0.00"), "", "");
                        string TCStock = (tClosingQty * tot6).ToString("0.00");
                        TCStock = TCStock.TrimStart('-');
                        //dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", RecQtytot - (totSalQty), "", ((tClosingQty * tot6) == 0) ? "0.00" : string.Format("{0:0.00}", (TCStock)), "");
                        if (txtcancel.Text == "ALL")
                        {
                            TCStock = ((oprnqty - tSalesQty) * tot6).ToString("0.00");
                            dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", oprnqty - (tSalesQty), "", ((tClosingQty * tot6) == 0) ? "0.00" : string.Format("{0:0.00}", (TCStock)), "");
                        }
                        else if (txtcancel.Text == "Cancelled")
                        {
                            TCStock = ((oprnqty - tSalesQty) * tot6).ToString("0.00");
                            dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", oprnqty - (tSalesQty), "", ((tClosingQty * tot6) == 0) ? "0.00" : string.Format("{0:0.00}", (TCStock)), "");
                        }
                        else
                        {
                            if (txtcancel.Text == "Not Cancelled")
                            {
                                TCStock = ((oprnqty - tSalesQty) * tot6).ToString("0.00");
                                dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", oprnqty - (tSalesQty), "", ((tClosingQty * tot6) == 0) ? "0.00" : string.Format("{0:0.00}", (TCStock)), "");
                            }

                        }
                        gridLedger.DataSource = dtGrid;
                    }
                }
            }
            else
            {

            }
        }
        public void funGridDetailsSingle()
        {
            if (txtlederof.Text.Trim() != "")
            {
                if (tAmountType == "" || tAmountType == null)
                {
                    tAmountType = "Gross Amount";
                }
                if (txtlisttype.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    dtGrid.Rows.Clear();
                    string tQuery = "";
                    if (txtlisttype.Text.Trim() == "Sales")
                    {
                        if (txtcancel.Text != "Cancelled")
                        {
                            // tQuery = "select convert(varchar,strn_date,103) as Date,(CASE WHEN (((strn_type=1 OR strn_type=2) And strn_Cancel=0) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=14) THEN 'NETS' ELSE (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no and stktrn_table.strn_Cancel=0 ) END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' WHEN strn_type=2 THEN 'Sales Ret' ELSE 'Purchase'  END) as Type,(CASE WHEN strn_type=1 THEN sum(nt_Qty) WHEN strn_type=2 THEN sum(nt_Qty)  ELSE '' END) as RecQty, (CASE WHEN strn_type=1 THEN sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN sum(Amount) ELSE sum(tot_amt) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo and (strn_type=1 OR strn_type=2)  group by Strn_date,strn_type,strnParty_no, strn_no,strn_Cancel ";

                            tQuery = "select (Case When (strn_type=1 or strn_type=2) and Strn_Cancel=0 then convert(Varchar,strn_date,103) End) As Date,(Case When strn_type=1 and Strn_Cancel=0 then (select Ledger_name from Ledger_table where Ledger_no=StrnParty_no ) else 'Cash Sales' End) As Particulars,(Case When strn_type=1  Then 'Sales' When strn_type=2 Then 'Sales Ret' End) As Type, (Case When strn_type=2  and strn_Cancel=0 Then (sum(nt_qty))  else 0.00  End) As RecQty,(Case When strn_type=1 and Strn_Cancel=0 Then (SUM(nt_qty)) else 0.00 End) As IssuQty,(Case When (strn_type=1 or strn_type=2) and Strn_Cancel=0 Then strn_no End) As strn_no,(CASE WHEN @tAmountType='Gross Amount' THEN Convert(Numeric(18,2),sum(Amount)) ELSE Convert(Numeric(18,2),sum(tot_amt)) END) as Value from stktrn_table where (strn_type=1 or strn_type=2)and Strn_Cancel=0  and item_no=@tItemNo and  strn_date between @tDateFrom and @tDateTo Group by strn_type,strn_no,strn_date,StrnParty_no,Strn_Cancel";
                        }
                        else if (txtcancel.Text == "Cancelled")
                        {
                            //tQuery = "select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_Cancel=1) AND strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=14) THEN 'NETS' ELSE (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no  and stktrn_table.strn_Cancel=1 ) END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' WHEN strn_type=2 THEN 'Sales Ret' ELSE 'Purchase'  END) as Type,(CASE WHEN strn_type=1 THEN sum(nt_Qty) WHEN strn_type=2 THEN sum(nt_Qty)  ELSE '' END) as RecQty, (CASE WHEN strn_type=1 THEN sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN sum(Amount) ELSE sum(tot_amt) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo and (strn_type=1 OR strn_type=2)  group by Strn_date,strn_type,strnParty_no, strn_no,strn_Cancel ";
                            // tQuery = "";
                            tQuery = "select Convert(Varchar,Result1.strn_date,103) As Date,Result1.Particulars,Result1.Type,Result1.RecQty,Result1.Value,Result1.strn_no from (select strn_date,item_no,(Case  When StrnParty_no in(1,2) then 'Cash A/C' When StrnParty_no in(14) Then 'NETS' When StrnParty_no not in(1,2,14) then (select ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no  and stktrn_table.strn_Cancel=1 ) End) As Particulars,(Case  When Strn_Cancel=1 and strn_type=1 then 'Sales Cancel' Else '' End) As Type,(Case When Strn_Cancel=1 and strn_type=1 then SUM(nt_qty) Else '' End) As RecQty,(CASE WHEN @tAmountType='Gross Amount' THEN Convert(Numeric(18,2),sum(Amount)) ELSE Convert(Numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and strn_date  between @tDateFrom and @tDateTo group by item_no,Strn_Cancel,StrnParty_no,strn_date,strn_type,strn_no) As Result1 Where Result1.RecQty>0";
                        }
                    }

                    //if (txtlisttype.Text.Trim() == "Cancel")
                    //{
                    //    if (txtcancel.Text != "Cancelled")
                    //    {

                    //        tQuery = "select (case When (strn_type=1 or strn_type=2) and Strn_Cancel=0 then convert(Varchar,strn_date,103) End) As Date,(Case When strn_type=1 and Strn_Cancel=0 then (select Ledger_name from Ledger_table where Ledger_no=StrnParty_no ) else 'Cash Sales' End) As Particulars,(Case When strn_type=1  Then 'Sales' When strn_type=2 Then 'Sales Ret' End) As Type, (Case When strn_type=2  and strn_Cancel=0 Then (sum(nt_qty))  else 0.00  End) As RecQty,(Case When strn_type=1 and Strn_Cancel=0 Then (SUM(nt_qty)) else 0.00 End) As IssuQty,(Case When (strn_type=1 or strn_type=2) and Strn_Cancel=0 Then strn_no End) As strn_no,(CASE WHEN @tAmountType='Gross Amount' THEN Convert(Numeric(18,2),sum(Amount)) ELSE Convert(Numeric(18,2),sum(tot_amt)) END) as Value from stktrn_table where (strn_type=1 or strn_type=2)and Strn_Cancel=0  and item_no=@tItemNo and  strn_date between @tDateFrom and @tDateTo Group by strn_type,strn_no,strn_date,StrnParty_no,Strn_Cancel";
                    //        tQuery = "select (case When (strn_type=1 or strn_type=2) and Strn_Cancel=0 then convert(Varchar,strn_date,103) End) As Date,(Case When strn_type=1 and Strn_Cancel=0 then (select Ledger_name from Ledger_table where Ledger_no=StrnParty_no ) else 'Cash Sales' End) As Particulars,(Case WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales' When strn_type=2 Then 'Sales Ret' End) As Type, (Case When  strn_type=1 and Strn_Cancel=1 then sum(nt_qty)  else 0.00  End) As RecQty,(Case When strn_type=1 and Strn_Cancel=0 Then (SUM(nt_Qty)) else 0.00 End) As IssuQty,(Case When (strn_type=1 or strn_type=2) and Strn_Cancel=0 Then strn_no End) As strn_no,(CASE WHEN @tAmountType='Gross Amount' THEN Convert(Numeric(18,2),sum(Amount)) ELSE Convert(Numeric(18,2),sum(tot_amt)) END) as Value from stktrn_table where (strn_type=1 or strn_type=2)and Strn_Cancel=0  and item_no=@tItemNo and  strn_date between @tDateFrom and @tDateTo Group by strn_type,strn_no,strn_date,StrnParty_no,Strn_Cancel";
                    //        tQuery="select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strn_Cancel=0 and strnParty_no=2) THEN 'Cash Sales' WHEN ((strn_type=3 AND strn_type=8) AND strnParty_no=14) THEN 'NETS' WHEN (strn_type=11) THEN 'Stock Less' WHEN (strn_type=12) THEN 'Stock Add' When strn_type='37' Then 'BOM Receipt' When   strn_type='38' Then 'BOM Issue' When strnParty_no=8 Then 'Cash Purchase' Else  (select Ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no)  END) as Particulars,(CASE WHEN strn_type=1 and Strn_Cancel=0 THEN 'Sales' WHEN strn_type=1 and Strn_Cancel=1 THEN 'Cancel' when strn_type=2 then 'Sales Ret'  when strn_type=11 then 'Stock Less' when strn_type=12 then 'Stock Add' When  strn_type='37' Then 'BOM In' When   strn_type='38' Then 'BOM Out' When strnParty_no=8 and strn_type=3  Then 'Purchase' END) as Type,(CASE WHEN strn_type=1 and Strn_Cancel=1 then sum(nt_qty) When StrnParty_no=8 then sum(nt_Qty) WHEN strn_type=2 then sum(nt_Qty) When strn_type=38 Then sum(nt_qty)  WHEN strn_type=3 then sum(nt_Qty) WHEN strn_type=12 then sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty)  When strn_type=37 Then sum(nt_qty) WHEN strn_type=11 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN convert(numeric(18,2),sum(Amount)) ELSE convert(numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where strn_type<>'0' and item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no,Strn_Cancel ", con);
                    //    }
                    //   else if (txtcancel.Text == "Cancelled")
                    //    {
                    //        tQuery = "select Convert(Varchar,Result1.strn_date,103) As Date,Result1.Particulars,Result1.Type,Result1.RecQty,Result1.Value,Result1.strn_no from (select strn_date,item_no,(Case  When StrnParty_no in(1z,2) then 'Cash A/C' When StrnParty_no in(14) Then 'NETS' When StrnParty_no not in(1,2,14) then (select ledger_name from Ledger_table where Ledger_no=stktrn_table.StrnParty_no  and stktrn_table.strn_Cancel=1 ) End) As Particulars,(Case  When Strn_Cancel=1 and strn_type=1 then 'Cancel' Else '' End) As Type,(Case When strn_type=1 and Strn_Cancel=1 then sum(nt_qty) Else '' End) As RecQty,(CASE WHEN @tAmountType='Gross Amount' THEN Convert(Numeric(18,2),sum(Amount)) ELSE Convert(Numeric(18,2),sum(tot_amt)) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and strn_date  between @tDateFrom and @tDateTo group by item_no,Strn_Cancel,StrnParty_no,strn_date,strn_type,strn_no) As Result1 Where Result1.RecQty>0";
                    //    }

                    //}
                    if (txtlisttype.Text.Trim() == "Purchase")
                    {
                        if (txtcancel.Text != "Cancelled")
                        {
                            tQuery = "select convert(varchar,strn_date,103) as Date,(CASE WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=2 and strn_Cancel=0) THEN 'Cash Sales' WHEN ((strn_type=1 OR strn_type=2) AND strnParty_no=14) THEN 'NETS' ELSE 'Cash Purchase' END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' WHEN strn_type=2 THEN 'Sales Ret' ELSE 'Purchase' END) as Type,(CASE WHEN strn_type=3 THEN sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 and Strn_Cancel=0 then sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN sum(Amount) ELSE sum(tot_amt) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo and strn_type=3 group by Strn_date,strn_type,strnParty_no, strn_no,strn_Cancel";
                        }
                        else { }
                    }
                    if (tQuery != null && tQuery != "")
                    {
                        SqlCommand cmd = new SqlCommand(tQuery, con);
                        cmd.Parameters.AddWithValue("@tItemNo", id);
                        cmd.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                        cmd.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                        cmd.Parameters.AddWithValue("@tAmountType", tAmountType);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtGrid);
                        double tPurchaseQty = 0;
                        Double tSalesQty = 0;
                        for (int mn = 0; mn < dtGrid.Rows.Count; mn++)
                        {

                            if (dtGrid.Rows[mn]["RecQty"].ToString() != "")
                            {
                                tPurchaseQty += double.Parse(dtGrid.Rows[mn]["RecQty"].ToString());
                            }
                            if (dtGrid.Rows[mn]["IssuQty"].ToString() != "")
                            {
                                tSalesQty += double.Parse(dtGrid.Rows[mn]["IssuQty"].ToString());
                            }
                        }
                        gridLedger.DataSource = dtGrid;
                        dtGrid.Rows.Add("", "", "", "", "", "");

                        SqlCommand cmdOpening = new SqlCommand("sp_OpeningQty", con);
                        cmdOpening.CommandType = CommandType.StoredProcedure;
                        if (txtlisttype.Text.Trim() == "Sales")
                        {
                            cmdOpening.Parameters.AddWithValue("@tActionType", "Sales");
                        }
                        if (txtlisttype.Text.Trim() == "Purchase")
                        {
                            cmdOpening.Parameters.AddWithValue("@tActionType", "Purchase");
                        }
                        if (txtlisttype.Text.Trim() == "Void")
                        {
                            cmdOpening.Parameters.AddWithValue("@tActionType", "Cancel");
                        }
                        cmdOpening.Parameters.AddWithValue("@tItemNo", id);
                        cmdOpening.Parameters.AddWithValue("@tClosingStock", 0);
                        cmdOpening.Parameters.AddWithValue("@tDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                        cmdOpening.Parameters.AddWithValue("@tDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                        SqlParameter para = new SqlParameter("@tFinalOpenQty", SqlDbType.VarChar, 50);
                        para.Direction = ParameterDirection.Output;
                        cmdOpening.Parameters.Add(para);
                        SqlParameter para1 = new SqlParameter("@tItemCost", SqlDbType.VarChar, 50);
                        para1.Direction = ParameterDirection.Output;
                        cmdOpening.Parameters.Add(para1);
                        //SqlParameter CloingQty = new SqlParameter("@tClosingStock", SqlDbType.VarChar, 50);
                        //CloingQty.Direction = ParameterDirection.Output;
                        cmdOpening.ExecuteNonQuery();
                        // double parvalues = 0.00;
                        if (para.Value.ToString().Trim() == "" || para.Value == null)
                        {
                            para.Value = "0.00";
                        }
                        if (para1.Value.ToString().Trim() == "" || para1.Value == null)
                        {
                            para1.Value = "0.00";
                        }


                        //new Coding Here
                        //SqlCommand cmdOpeningA = new SqlCommand("select sum(nt_qty) As totQty from stktrn_table Where (strn_type=0 or strn_type=3) and item_no=@ItemNo and strn_date between @tFromDate and @tDateTo", con);
                        SqlCommand cmdOpeningA = new SqlCommand("select Case When ((sum(nt_qty))-(select sum(nt_qty)-(select sum(nt_qty) from stktrn_table where strn_type=2 and strn_rtno<>0 and item_no=@ItemNo and strn_date between @FDateFrom and @TDateTo) from stktrn_table where item_no=@ItemNo and strn_type in (1) and strn_date between @FDateFrom and @TDateTo)) is null Then 0.00 Else ((sum(nt_qty))-(select sum(nt_qty)-(select sum(nt_qty) from stktrn_table where strn_type=2 and strn_rtno<>0 and item_no=@ItemNo and strn_date between @FDateFrom and @TDateTo) from stktrn_table where item_no=@ItemNo and strn_type in (1) and strn_date between @FDateFrom and @TDateTo))  End As TotQty from stktrn_table Where (strn_type=0 or strn_type=3) and item_no=@ItemNo and strn_date between @FDateFrom and @TDateTo", con);
                        cmdOpeningA.Parameters.AddWithValue("@ItemNo", id);
                        cmdOpeningA.Parameters.AddWithValue("@FDateFrom", new DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day));
                        cmdOpeningA.Parameters.AddWithValue("@TDateTo", new DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day));
                        SqlDataAdapter adpA = new SqlDataAdapter(cmdOpeningA);

                        DataTable dtA = new DataTable();
                        dtA.Rows.Clear();
                        adpA.Fill(dtA);
                        if (dtA.Rows.Count > 0)
                        {
                            para.Value = Convert.ToString(Convert.ToDouble(dtA.Rows[0]["totQty"].ToString()).ToString("0.00"));
                        }
                        else
                        {
                            para.Value = "0.00";
                        }
                        dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", para.Value.ToString(), "", (double.Parse(para.Value.ToString()) * double.Parse(para1.Value.ToString())));
                        dtGrid.Rows.Add("", "                  Total", "", tPurchaseQty, tSalesQty, "");
                        double tClosingQty = 0;
                        tClosingQty = (double.Parse(para.Value.ToString()) + (tPurchaseQty - tSalesQty));
                        dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", tClosingQty, "", (tClosingQty * double.Parse(para1.Value.ToString())));
                        gridLedger.DataSource = dtGrid;
                    }
                    else
                    {
                        funGridDetails();
                    }
                }
                else
                {
                    funGridDetails();
                    //string Itemcode = txtlederof.Text.Trim();
                    //// passingvalues.tot = grd_SalesSummary.Rows[row].Cells[4].Value.ToString();
                    //DataTable dtNew4 = new DataTable();
                    //dtNew4.Rows.Clear();
                    //SqlCommand cmdItemno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                    //cmdItemno.CommandType = CommandType.StoredProcedure;
                    //cmdItemno.Parameters.AddWithValue("@tActionType", "ITEMNAME");
                    //cmdItemno.Parameters.AddWithValue("@tValue", Itemcode);
                    //SqlDataAdapter adp4 = new SqlDataAdapter(cmdItemno);
                    //adp4.Fill(dtNew4);
                    //if (dtNew4.Rows.Count > 0)
                    //{
                    //    id = dtNew4.Rows[0][0].ToString();
                    //}
                    //DataTable dtNew = new DataTable();
                    //dtNew.Rows.Clear();
                    //dtGrid.Rows.Clear();
                    //SqlCommand cmd = new SqlCommand("select convert(varchar,strn_date,103) as Date,(CASE WHEN (strn_type=1 AND strnParty_no=2) THEN 'Cash Sales' WHEN (strn_type=1 AND strnParty_no=14) THEN 'NETS' ELSE 'Cash Purchase' END) as Particulars,(CASE WHEN strn_type=1 THEN 'Sales' ELSE 'Purchase' END) as Type,(CASE WHEN strn_type=0 THEN sum(nt_Qty) ELSE '' END) as RecQty, (CASE WHEN strn_type=1 THEN sum(nt_Qty) ELSE '' END) as IssuQty,(CASE WHEN @tAmountType='Gross Amount' THEN sum(Amount) ELSE sum(tot_amt) END) as Value, strn_no  from stktrn_table where item_no=@tItemNo and Strn_date between @tDateFrom and @tDateTo group by Strn_date,strn_type,strnParty_no,strn_no ", con);
                    //cmd.Parameters.AddWithValue("@tItemNo", id);
                    //cmd.Parameters.AddWithValue("@tDateFrom", dtpFrom.Value);
                    //cmd.Parameters.AddWithValue("@tDateTo", dtpTo.Value);
                    //cmd.Parameters.AddWithValue("@tAmountType", tAmountType);
                    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //adp.Fill(dtGrid);
                    //double tPurchaseQty = 0;
                    //Double tSalesQty = 0;
                    //for (int mn = 0; mn < dtGrid.Rows.Count; mn++)
                    //{
                    //    if (dtGrid.Rows[mn]["RecQty"].ToString() != "")
                    //    {
                    //        tPurchaseQty += double.Parse(dtGrid.Rows[mn]["RecQty"].ToString());
                    //    }
                    //    if (dtGrid.Rows[mn]["IssuQty"].ToString() != "")
                    //    {
                    //        tSalesQty += double.Parse(dtGrid.Rows[mn]["IssuQty"].ToString());
                    //    }
                    //}
                    //gridLedger.DataSource = dtGrid;
                    //dtGrid.Rows.Add("", "", "", "", "", "", "");

                    //SqlCommand cmdOpening = new SqlCommand("sp_OpeningQty", con);
                    //cmdOpening.CommandType = CommandType.StoredProcedure;
                    //cmdOpening.Parameters.AddWithValue("@tActionType", "All");
                    //cmdOpening.Parameters.AddWithValue("@tItemNo", id);
                    //cmdOpening.Parameters.AddWithValue("@tDateFrom", dtpFrom.Value);
                    //cmdOpening.Parameters.AddWithValue("@tDateTo", dtpTo.Value);
                    //SqlParameter para = new SqlParameter("@tFinalOpenQty", SqlDbType.VarChar, 50);
                    //para.Direction = ParameterDirection.Output;
                    //cmdOpening.Parameters.Add(para);
                    //SqlParameter para1 = new SqlParameter("@tItemCost", SqlDbType.VarChar, 50);
                    //para1.Direction = ParameterDirection.Output;
                    //cmdOpening.Parameters.Add(para1);
                    //con.Close();
                    //con.Open();
                    //cmdOpening.ExecuteNonQuery();
                    //double parvalues = 0.00;
                    //if (para.Value.ToString().Trim() == "" || para.Value == null)
                    //{
                    //    para.Value = "0.00";
                    //}
                    //if (para1.Value.ToString().Trim() == "" || para1.Value == null)
                    //{
                    //    para1.Value = "0.00";
                    //}

                    //dtGrid.Rows.Add(dtpFrom.Text.ToString(), "         Opening Stock", "", para.Value.ToString(), "", (double.Parse(para.Value.ToString()) * double.Parse(para1.Value.ToString())), "");
                    //dtGrid.Rows.Add("", "                  Total", "", tPurchaseQty, tSalesQty, "", "");
                    //double tClosingQty = 0;
                    //tClosingQty = (double.Parse(para.Value.ToString()) + (tPurchaseQty - tSalesQty));
                    //dtGrid.Rows.Add(dtpTo.Text.ToString(), "         Closing Stock", "", tClosingQty, "", (tClosingQty * double.Parse(para1.Value.ToString())), "");
                    //gridLedger.DataSource = dtGrid;
                }
            }
            else 
            { 
                funGridDetails(); 
            }
        }
        double closingqty = 0.00;
        double openqty = 0.00;
        public void select_query()
        {
            con.Close();
            con.Open();
            gridLedger.Rows.Clear();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt_md = new DataTable();
            //load item name only:
            SqlCommand md = new SqlCommand("select * from item_table where item_no='" + id + "'", con);
            SqlDataAdapter adp_md = new SqlDataAdapter(md);
            dt_md.Rows.Clear();

            adp_md.Fill(dt_md);
            double itemqty = 0.00, itemcost = 0.00;
            if (dt_md.Rows.Count > 0)
            {
                txtlederof.Text = dt_md.Rows[0]["item_name"].ToString();

                itemcost = Convert.ToDouble(dt_md.Rows[0]["Item_cost"].ToString());
                //closing qty
                if (dt_md.Rows[0]["nt_cloqty"].ToString() != "")
                {
                    closingqty = Convert.ToDouble(dt_md.Rows[0]["nt_cloqty"].ToString());
                }

                //txtremarks.Focus();
            }

            //only get id values used to get strn_no for passing to another table
            SqlCommand cmd1 = new SqlCommand("select * from stktrn_table where item_no='" + id + "'", con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            dt1.Rows.Clear();
            con.Close();
            con.Open();
            adp1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                string strn_no = dt1.Rows[0]["strn_no"].ToString();


                //retrive name from stktrn_table used to update process doing
                SqlCommand cmd = new SqlCommand("select * from stktrn_table where strn_no='" + strn_no + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                con.Close();
                con.Open();
                dt.Rows.Clear();
                adp.Fill(dt);
                //
                DataTable dtledgertable = new DataTable();
                if (dt.Rows.Count > 0)
                {
                    gridLedger.AutoGenerateColumns = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gridLedger.Rows.Add();
                        gridLedger.Rows[i].Cells["strn_date"].Value = Convert.ToDateTime(dt.Rows[i]["strn_date"].ToString()).ToShortDateString();//Convert.ToDateTime(dr["EndDate"].ToString()).ToShortDateString();
                        string strn_party_no = dt.Rows[i]["StrnParty_no"].ToString();
                        string strntype = dt.Rows[i]["strn_type"].ToString();
                        gridLedger.Rows[i].Cells["strn_no"].Value = dt.Rows[i]["strn_no"].ToString();
                        gridLedger.Rows[i].Cells["strn_sno"].Value = dt.Rows[i]["strn_sno"].ToString();
                        if (strn_party_no != "0" && strntype != "0")
                        {
                            if (strn_party_no != "")
                            {
                                SqlCommand cmdledger_table = new SqlCommand("select * from ledger_table where ledger_no='" + strn_party_no + "'", con);
                                SqlDataAdapter adpledger = new SqlDataAdapter(cmdledger_table);
                                dtledgertable.Rows.Clear();
                                adpledger.Fill(dtledgertable);
                                if (dtledgertable.Rows.Count > 0)
                                {
                                    gridLedger.Rows[i].Cells["Particulars"].Value = dtledgertable.Rows[0]["Ledger_name"].ToString();
                                }
                            }
                            string strn_type = dt.Rows[i]["strn_type"].ToString();
                            if (strn_type != "")
                            {
                                if (strn_type == "3" && strn_party_no == "8")
                                {
                                    gridLedger.Rows[i].Cells["Type"].Value = "Purchase";
                                    gridLedger.Rows[i].Cells["Rqty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
                                }
                                else if (strn_type == "1" && strn_party_no == "0")
                                {
                                    gridLedger.Rows[i].Cells["Particulars"].Value = "Cash Sales";
                                    gridLedger.Rows[i].Cells["Type"].Value = "Sales";
                                    gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
                                }
                                else if (strn_type == "0" && strn_party_no == "12")
                                {
                                    gridLedger.Rows[i].Cells["Particulars"].Value = "Stock Add";
                                    gridLedger.Rows[i].Cells["Type"].Value = "Stock Add";
                                    gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
                                }
                                else if (strn_type == "0" && strn_party_no == "11")
                                {
                                    gridLedger.Rows[i].Cells["Particulars"].Value = "Stock Less";
                                    gridLedger.Rows[i].Cells["Type"].Value = "Stock Less";
                                    gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
                                }

                                else if (strn_type == "2" && strn_party_no == "2")
                                {
                                    gridLedger.Rows[i].Cells["Type"].Value = "Sales Ret";
                                    gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
                                }
                                else if (strn_type == "4" && strn_party_no == "8")
                                {
                                    gridLedger.Rows[i].Cells["Type"].Value = "Purchase Ret";
                                    gridLedger.Rows[i].Cells["Rqty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
                                }
                                else if (strn_type != "" && strn_party_no != "")
                                {
                                    gridLedger.Rows[i].Cells["Rqty"].Value = dt.Rows[i]["nt_qty"].ToString();
                                    gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();

                                }
                            }
                            itemqty = Convert.ToDouble(dt.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            tot_amount = itemcost;
                            eleparexe = "1";

                        }
                        else if (strn_party_no == "0" && strntype == "0")
                        {
                            if (dt.Rows[i]["nt_qty"].ToString() != "")
                            {
                                openqty = Convert.ToDouble(dt.Rows[i]["nt_qty"].ToString());
                                //gridcalculation();
                            }
                        }
                    }
                }
            }
            else
            {
                gridLedger.Rows.Clear();
                rcqty = 0.00;
                issue_qty = 0.00;
                rcqty_no = 0.00;
                tot_amount = 0.00;
                closingqty = 0.00;
                openqty = 0.00;
                eleparexe = "0";
                // gridcalculation();

            }
        }
        double tot_amount = 0.00, rcqty_no = 0.00;
        string eleparexe = "0";
        public void gridcalculation()
        {
            for (int k = 0; k < gridLedger.Rows.Count - 1; k++)
            {
                if (gridLedger.Rows[k].Cells["Rqty"].Value != null && gridLedger.Rows[k].Cells["Rqty"].Value != "")
                {
                    rcqty += Convert.ToDouble(gridLedger.Rows[k].Cells["Rqty"].Value.ToString());
                }
                if (gridLedger.Rows[k].Cells["IQty"].Value != null && gridLedger.Rows[k].Cells["IQty"].Value != "")
                {
                    issue_qty += Convert.ToDouble(gridLedger.Rows[k].Cells["IQty"].Value);
                }

            }
            if (gridLedger.Rows.Count > 0)
            {
                gridLedger.Rows.Add();
                //oepn qty 172,tot amount line 165
                gridLedger.Rows.Add(dtpFrom.Text.ToString(), "Opening Stock", "", openqty.ToString("0.00"), "", tot_amount.ToString("0.00"), "", "");
                //line 204
                gridLedger.Rows.Add("", "Total", "", rcqty.ToString("0.00"), issue_qty.ToString("0.00"), "", "", "");
                //closing qty 61line define
                gridLedger.Rows.Add(dtpTo.Text.ToString(), "Closing Stock", "", closingqty.ToString("0.00"), "", tot_amount.ToString("0.00"), "", "");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            }
            catch { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            passingvalues.id_number_item_leder = "";
            this.Close();
        }
        string chk;
        SqlDataReader dr = null;
        private void txtlederof_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() != "" && comboBox1.Text.Trim() != "Barcode")
            {
                pnllist.Visible = true;

                if (txtlederof.Text.Trim() != null && txtlederof.Text.Trim() != "")
                {
                    DataTable dtNew4 = new DataTable();
                    dtNew4.Rows.Clear();
                    SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                    cmdCno.CommandType = CommandType.StoredProcedure;
                    cmdCno.Parameters.AddWithValue("@tActionType", "ITEMNAMEALL");
                    cmdCno.Parameters.AddWithValue("@tValue", txtlederof.Text.Trim());
                    SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
                    adp4.Fill(dtNew4);
                    bool isChk = false;
                    if (dtNew4.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dtNew4.Rows[0]["item_selname"].ToString();
                        for (int k = 0; k < listview.Items.Count; k++)
                        {
                            if (tempstr == listview.Items[k].ToString())
                            {
                                listview.SetSelected(k, true);
                                txtlederof.Select();
                                chk = "1";
                                txtlederof.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "1";
                        if (txtlederof.Text != "")
                        {
                            string name = txtlederof.Text.Remove(txtlederof.Text.Length - 1);
                            txtlederof.Text = name.ToString();
                            txtlederof.Select(txtlederof.Text.Length, 0);
                        }
                        txtlederof.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
            else
            {
                if (comboBox1.Text.Trim() == "Barcode")
                {
                    pnllist.Visible = false;
                    if (txtlederof.Text.Trim() != null && txtlederof.Text.Trim() != "")
                    {
                        DataTable dtNew5 = new DataTable();
                        dtNew5.Rows.Clear();
                        SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                        cmdCno.CommandType = CommandType.StoredProcedure;
                        cmdCno.Parameters.AddWithValue("@tActionType", "BARCODECHECKNO");
                        cmdCno.Parameters.AddWithValue("@tValue", txtlederof.Text.Trim());
                        SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
                        adp4.Fill(dtNew5);
                        bool isChk = false;
                        if (dtNew5.Rows.Count > 0)
                        {
                            id = dtNew5.Rows[0]["ITEM_NO"].ToString();
                        }
                    }
                }
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

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                if (listview.SelectedIndex < listview.Items.Count - 1)
                {
                    listview.SetSelected(listview.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (listview.SelectedIndex > 0)
                {
                    listview.SetSelected(listview.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                pnllist.Visible = false;
                if (listview.Text != "")
                {
                    txtlederof.Text = listview.SelectedItem.ToString();
                    // listbox_values();
                }
                txtcancel.Focus();
                txtcancel.SelectAll();
            }
        }
        private void txtlederof_Enter(object sender, EventArgs e)
        {
            //if (passingvalues.numbervaluestoledger != "1")
            //{


            txtlederof.BackColor = Color.LightBlue;
            txtcancel.BackColor = Color.White;
            txtlisttype.BackColor = Color.White;
            txtremarks.BackColor = Color.White;
            pnltype.Visible = false;
            pnlcancel.Visible = false;
            pnllist.Visible = false;
            con.Close();
            con.Open();
            DataTable dt_item_code = new DataTable();

            SqlCommand cmd = new SqlCommand("select * from item_seltable Order by Item_selname", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt_item_code.Rows.Clear();
            listview.Items.Clear();
            adp.Fill(dt_item_code);
            if (dt_item_code.Rows.Count > 0)
            {
                for (int i = 0; i < dt_item_code.Rows.Count; i++)
                {
                    listview.Items.Add(dt_item_code.Rows[i]["item_selname"].ToString());
                }
            }
            con.Close();
            txtlederof.Focus();
            txtlederof.Select();
        }
        // }
        private void txtlederof_Leave(object sender, EventArgs e)
        {
            //if (txtlederof.Text.Trim() != "")
            //{
            //    con.Close();
            //    con.Open();
            //    DataTable dt_itemname = new DataTable();
            //    SqlCommand md = new SqlCommand("select * from item_table where item_name='" + txtlederof.Text + "'", con);
            //    SqlDataAdapter adp = new SqlDataAdapter(md);

            //    adp.Fill(dt_itemname);
            //    if (dt_itemname.Rows.Count > 0)
            //    {
            //        gridLedger.Rows.Clear(); 
            //        id = dt_itemname.Rows[0]["item_no"].ToString();
            //        rcqty = 0.00;
            //        issue_qty = 0.00;
            //        tot_amount = 0.00;
            //        gridLedger.Rows.Clear();
            //        select_query();
            //        eleparexe = "1";

            //    }
            //    if (passingvalues.numbervaluestoledger == "1")
            //    {
            //        if (passingvalues.gridcalculation != "2")
            //        {
            //            if (eleparexe != "0")
            //            {
            //                gridcalculation();
            //            }
            //        }
            //    }
            //    if (passingvalues.gridcalculation == "2")
            //    {

            //            gridcalculation();

            //    }
            //}
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                dtpTo.Focus();
            }
        }
        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtlederof.Focus();
            }
        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if ((comboBox1.Text.Trim() != "Search By" || comboBox1.Text.Trim() != "") || txtlederof.Text.Trim() != "")
                {
                    dtGrid.Rows.Clear();
                    getidnumber();
                    funGridDetailsSingle();
                    gridLedger.Focus();

                    txtlederof.BackColor = Color.White;
                    txtcancel.BackColor = Color.White;
                    txtlisttype.BackColor = Color.White;
                    txtremarks.BackColor = Color.White;
                }
                else
                {
                    MyMessageBox1.ShowBox("Please Select Search Type", "Warning");
                }
            }
        }
        public void getidnumber()
        {
            if (txtlederof.Text != "")
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //  cmd.Parameters.AddWithValue("@ActionType", "ItemName");
                
                if (comboBox1.Text.Trim() == "Barcode")
                {
                    cmd.Parameters.AddWithValue("@ActionType", "BarcodeNoSearch");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ActionType", "BarcodeITemSearch");
                }

                cmd.Parameters.AddWithValue("itemName", txtlederof.Text);
                cmd.Parameters.AddWithValue("ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    id = dt.Rows[0]["item_no"].ToString();
                }
            }
        }
        public void listviablefalse()
        {
            pnllist.Visible = false;
            pnltype.Visible = false;
            pnlcancel.Visible = false;
        }
        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            listviablefalse();
        }

        private void txtremarks_Enter(object sender, EventArgs e)
        {
            txtlederof.BackColor = Color.White;
            txtcancel.BackColor = Color.White;
            txtlisttype.BackColor = Color.White;
            txtremarks.BackColor = Color.LightBlue;

            listviablefalse();
            txtremarks.Select();
        }

        private void txtcancel_Enter(object sender, EventArgs e)
        {

            txtlederof.BackColor = Color.White;
            txtcancel.BackColor = Color.LightBlue;
            txtlisttype.BackColor = Color.White;
            txtremarks.BackColor = Color.White;

            pnllist.Visible = false;
            pnltype.Visible = false;
            pnlcancel.Visible = false;
            txtcancel.SelectAll();
        }
        private void txtlisttype_Enter(object sender, EventArgs e)
        {
            txtlederof.BackColor = Color.White;
            txtcancel.BackColor = Color.White;
            txtlisttype.BackColor = Color.LightBlue;
            txtremarks.BackColor = Color.White;

            pnllist.Visible = false;
            pnltype.Visible = false;
            pnlcancel.Visible = false;
            txtlisttype.SelectAll();
        }
        string chkStr1, chkstr2;
        private void txtlisttype_TextChanged(object sender, EventArgs e)
        {
            pnltype.Visible = true;
            if (txtlisttype.Text.Trim() != null && txtlisttype.Text.Trim() != "")
            {
                for (int i = 0; i < listtype.Items.Count; i++)
                {
                    chkStr1 = listtype.Items[i].ToString();
                    if (txtlisttype.Text.Length <= chkStr1.Length)
                    {
                        chkstr2 = chkStr1.Substring(0, txtlisttype.Text.Length);
                        bool isChk = false;
                        if (txtlisttype.Text.Trim() == chkstr2 || txtlisttype.Text.Trim() == chkstr2.ToLower())
                        {
                            isChk = true;
                            listtype.SetSelected(i, true);
                            txtlisttype.Select();
                            chk = "1";
                            txtlisttype.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);

                            break;
                        }
                        if (isChk == false)
                        {
                            chk = "2";
                            txtlisttype.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                        }
                    }
                }
            }
            else
            {
                chk = "1";
            }
        }
        private void textBox2_press_KeyPress(object sender, KeyPressEventArgs e)
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
        private void OnTextBoxKeyDown1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                if (listtype.SelectedIndex < listtype.Items.Count - 1)
                {
                    listtype.SetSelected(listtype.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (listtype.SelectedIndex > 0)
                {
                    listtype.SetSelected(listtype.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                try
                {
                    if (listtype.Text != "")
                    {
                        txtlisttype.Text = listtype.SelectedItem.ToString();
                        // listbox_values();
                    }
                    pnltype.Visible = false;
                    txtremarks.Focus();
                }
                catch
                { }
            }
        }
        private void txtcancel_TextChanged(object sender, EventArgs e)
        {
            bool isChk = false;
            pnlcancel.Visible = true;
            if (txtcancel.Text.Trim() != null && txtcancel.Text.Trim() != "")
            {
                for (int i = 0; i < listcancel.Items.Count; i++)
                {
                    chkStr1 = listcancel.Items[i].ToString();
                    if (txtcancel.Text.Length <= chkStr1.Length)
                    {
                        chkstr2 = chkStr1.Substring(0, txtcancel.Text.Length);
                        isChk = false;
                        if (txtcancel.Text.Trim().ToLower() == chkstr2.ToLower())
                        {
                            isChk = true;
                            listcancel.SetSelected(i, true);
                            txtcancel.Select();
                            chk = "1";
                            txtcancel.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk != true)
                {
                    // chk = "2";
                    //   txtcancel.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                    chk = "1";
                    if (txtcancel.Text != "")
                    {
                        string name = txtcancel.Text.Remove(txtcancel.Text.Length - 1);
                        txtcancel.Text = name.ToString();
                        txtcancel.Select(txtcancel.Text.Length, 0);
                    }
                    txtcancel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";
            }
        }
        private void OnTextBoxKeyDown2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (listcancel.SelectedIndex < listcancel.Items.Count - 1)
                {
                    listcancel.SetSelected(listcancel.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (listcancel.SelectedIndex > 0)
                {
                    listcancel.SetSelected(listcancel.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                pnltype.Visible = false;
                if (listcancel.Text != "")
                {
                    txtcancel.Text = listcancel.SelectedItem.ToString();
                    txtlisttype.Focus();
                }
                txtlisttype.Focus();
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ItemLedger frm = new ItemLedger();
            frm.Close();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridLedger.Rows[e.RowIndex].Cells["Particulars"].Value.ToString().Trim() == "Opening Stock")
            {
                Trade frm = new Trade(id);
                frm.MdiParent = this.ParentForm;
                // passingvalues.tot = total.ToString();
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else if (gridLedger.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim() == "Sales")
            {
                chkbox.FormIdentify = "ItemLedger";
                int row = e.RowIndex;
                var tempdate = gridLedger.Rows[row].Cells["Strn_no"].Value.ToString();
                if (tempdate != "")
                {
                    // string selectedBillno = gridLedger.Rows[row].Cells["Strn_no"].Value.ToString();
                    // Double selectedbillamt = Convert.ToDouble(gridLedger.Rows[row].Cells["Value"].Value.ToString());
                    //MessageBox.Show(selectedBillno);
                    DataTable dtNew1 = new DataTable();
                    dtNew1.Rows.Clear();
                    SqlDataAdapter adpChk = new SqlDataAdapter("select smas_billno from salmas_table where smas_no='" + gridLedger.Rows[row].Cells["Strn_no"].Value.ToString() + "'", con);
                    adpChk.Fill(dtNew1);
                    if (dtNew1.Rows.Count > 0)
                    {
                        chkbox.SalesBillNo = dtNew1.Rows[0][0].ToString();
                    }

                    chkbox.SalesBillamt = Convert.ToDouble(gridLedger.Rows[row].Cells["Value"].Value.ToString());
                    frmSalesAlteration frm = new frmSalesAlteration();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                    this.Hide();
                }
            }
            else if (gridLedger.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim() == "Purchase")
            {
                if (gridLedger.Rows.Count > 0)
                {
                    int i = e.RowIndex;
                    string strn_number = gridLedger.Rows[i].Cells["strn_no"].Value.ToString();
                    PurchaseEntry1 frm = new PurchaseEntry1(strn_number);
                    frm.MdiParent = this.ParentForm;
                    // passingvalues.tot = total.ToString();
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                }
            }
            //else if (gridLedger.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim() == "Void")
            //{
            //    if (gridLedger.Rows.Count > 0)
            //    {
            //        int i = e.RowIndex;
            //        string strn_number = gridLedger.Rows[i].Cells["strn_no"].Value.ToString();
            //        PurchaseEntry1 frm = new PurchaseEntry1(strn_number);
            //        frm.MdiParent = this.ParentForm;
            //        // passingvalues.tot = total.ToString();
            //        frm.StartPosition = FormStartPosition.Manual;
            //        frm.WindowState = FormWindowState.Normal;
            //        frm.Location = new Point(0, 80);
            //        frm.Show();
            //    }
            //}
            else if (gridLedger.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim().ToUpper() == "BOM IN" || gridLedger.Rows[e.RowIndex].Cells["Type"].Value.ToString().Trim().ToUpper() == "BOM OUT")
            {
                int i = e.RowIndex;
                string strn_number = gridLedger.Rows[i].Cells["strn_no"].Value.ToString();
                SqlCommand cmd = new SqlCommand("select * from BOMMas_Table,BOMissu_Table where BOMMas_Table.BOM_No=BOMissu_Table.BOM_no and BOMIssu_SNo='" + strn_number.ToString() + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtBomName = new DataTable();
                dtBomName.Rows.Clear();
                adp.Fill(dtBomName);
                if (dtBomName.Rows.Count > 0)
                {
                    passingvalues.BomIssueAlterLedger = dtBomName.Rows[0]["BOM_name"].ToString();
                    passingvalues.BomDeleteStkrnValues = strn_number.ToString().Trim();
                    SalesBOMIssueCreation frm = new SalesBOMIssueCreation();
                    frm.MdiParent = this.ParentForm;
                    // passingvalues.tot = total.ToString();
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                }
            }
            //else if (gridLedger.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "Cash Sales" && gridLedger.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "Closing Stock" && gridLedger.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "Total" && gridLedger.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "Opening Stock")
            //{
            //    if (gridLedger.Rows.Count > 0)
            //    {
            //        int i = e.RowIndex;
            //        string strn_number = gridLedger.Rows[i].Cells["strn_no"].Value.ToString();
            //        PurchaseEntry frm = new PurchaseEntry(strn_number);
            //        frm.MdiParent = this.ParentForm;
            //        // passingvalues.tot = total.ToString();
            //        frm.StartPosition = FormStartPosition.Manual;
            //        frm.WindowState = FormWindowState.Normal;
            //        frm.Location = new Point(0, 80);
            //        frm.Show();
            //    }
            //}
        }
        string strn_typename;
        string strn_partyname;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt_md = new DataTable();
        private void txtlisttype_Leave(object sender, EventArgs e)
        {
            //string str = "Select  Item_table.Item_code,item_table.Item_name, item_table.nt_cloqty,item_table.item_cost,ROUND(item_table.nt_cloqty*item_table.item_cost,2) as tot from item_table Where ";
            //string str = "Select stktrn_table.[strn_no],stktrn_table.[strn_sno],stktrn_table.[strn_date],stktrn_table.[StrnParty_no],stktrn_table.[strn_type],stktrn_table.[nt_qty],stktrn_table.[tot_amt] from stktrn_table,purmas_table where stktrn_table.strn_no=purmas_table.pmas_no And ";

            //if (txtcancel.Text != "" && txtlisttype.Text != "" && txtlederof.Text != "")
            //{
            //    if (txtlisttype.Text == "Purchase")
            //    {
            //        str += "stktrn_table.StrnParty_no='8' AND ";
            //        str += "Strn_type='3' AND ";
            //    }
            //    if (txtlisttype.Text == "Purchase Retrun")
            //    {
            //        str += "stktrn_table.StrnParty_no='8' AND ";
            //        str += "stktrn_table.Strn_type='4' AND ";
            //    }
            //    if (txtlisttype.Text == "Sales")
            //    {
            //        str += "stktrn_table.StrnParty_no='2' AND ";
            //        str += "stktrn_table.Strn_type='1' AND ";
            //    }
            //    if (txtlisttype.Text == "Sales Return")
            //    {
            //        str += "stktrn_table.StrnParty_no='2' AND ";
            //        str += "stktrn_table.Strn_type='2' AND ";
            //    }
            //    if (txtcancel.Text == "Cancel")
            //    {
            //        str += "purmas_table.pmas_cancel<>1  AND ";
            //    }
            //    if (txtcancel.Text == "Not Cancelled")
            //    {
            //        str += "purmas_table.pmas_cancel=0 AND ";

            //    }
            //    if (txtcancel.Text == "ALL")
            //    {
            //        str += "purmas_table.pmas_cancel=0 AND ";
            //    }


            //    //list type get values to gridview 
            //    if (con.State != ConnectionState.Open)
            //    {
            //        con.Open();
            //    }

            //    //load item name only:
            //    SqlCommand md = new SqlCommand("select * from item_table where item_no='" + id + "'", con);
            //    SqlDataAdapter adp_md = new SqlDataAdapter(md);
            //    dt_md.Rows.Clear();
            //    if (dr != null)
            //    {
            //        dr.Close();
            //    }
            //    adp_md.Fill(dt_md);
            //    double itemqty = 0.00, itemcost = 0.00;
            //    if (dt_md.Rows.Count > 0)
            //    {
            //        txtlederof.Text = dt_md.Rows[0]["item_name"].ToString();

            //        itemcost = Convert.ToDouble(dt_md.Rows[0]["Item_cost"].ToString());
            //        //closing qty
            //        if (dt_md.Rows[0]["nt_cloqty"].ToString() != "")
            //        {
            //            closingqty = Convert.ToDouble(dt_md.Rows[0]["nt_cloqty"].ToString());
            //        }

            //        //txtremarks.Focus();
            //    }

            //    //only get id values used to get strn_no for passing to another table
            //    SqlCommand cmd1 = new SqlCommand("select * from stktrn_table where item_no='" + id + "'", con);
            //    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            //    dt1.Rows.Clear();
            //    if (dr != null)
            //    {
            //        dr.Close();
            //    }
            //    adp1.Fill(dt1);
            //    if (dt1.Rows.Count > 0)
            //    {
            //        string strn_no = dt1.Rows[0]["strn_no"].ToString();

            //        if (strn_no != "")
            //        {
            //            str += "strn_no='" + strn_no + "' ";
            //        }
            //        MessageBox.Show(str.ToString());
            //        //retrive name from stktrn_table used to update process doing
            //        SqlCommand cmd = new SqlCommand(str, con);

            //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //        if (dr != null)
            //        {
            //            dr.Close();
            //        }
            //        dt.Rows.Clear();
            //        adp.Fill(dt);
            //        //
            //        DataTable dtledgertable = new DataTable();
            //        if (dt.Rows.Count > 0)
            //        {
            //            gridLedger.AutoGenerateColumns = false;
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                gridLedger.Rows.Add();
            //                gridLedger.Rows[i].Cells["strn_date"].Value = Convert.ToDateTime(dt.Rows[i]["strn_date"].ToString()).ToShortDateString();//Convert.ToDateTime(dr["EndDate"].ToString()).ToShortDateString();
            //                string strn_party_no = dt.Rows[i]["StrnParty_no"].ToString();
            //                string strntype = dt.Rows[i]["strn_type"].ToString();
            //                gridLedger.Rows[i].Cells["strn_no"].Value = dt.Rows[i]["strn_no"].ToString();
            //                gridLedger.Rows[i].Cells["strn_sno"].Value = dt.Rows[i]["strn_sno"].ToString();
            //                if (strn_party_no != "0" && strntype != "0")
            //                {
            //                    if (strn_party_no != "")
            //                    {
            //                        SqlCommand cmdledger_table = new SqlCommand("select * from ledger_table where ledger_no='" + strn_party_no + "'", con);
            //                        SqlDataAdapter adpledger = new SqlDataAdapter(cmdledger_table);
            //                        dtledgertable.Rows.Clear();
            //                        adpledger.Fill(dtledgertable);
            //                        if (dtledgertable.Rows.Count > 0)
            //                        {
            //                            gridLedger.Rows[i].Cells["Particulars"].Value = dtledgertable.Rows[0]["Ledger_name"].ToString();
            //                        }
            //                    }
            //                    string strn_type = dt.Rows[i]["strn_type"].ToString();
            //                    if (strn_type != "")
            //                    {
            //                        if (strn_type == "3" && strn_party_no == "8")
            //                        {
            //                            gridLedger.Rows[i].Cells["Type"].Value = "Purchase";
            //                            gridLedger.Rows[i].Cells["Rqty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }
            //                        else if (strn_type == "1" && strn_party_no == "0")
            //                        {
            //                            gridLedger.Rows[i].Cells["Particulars"].Value = "Cash Sales";
            //                            gridLedger.Rows[i].Cells["Type"].Value = "Sales";
            //                            gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }
            //                        else if (strn_type == "0" && strn_party_no == "12")
            //                        {
            //                            gridLedger.Rows[i].Cells["Particulars"].Value = "Stock Add";
            //                            gridLedger.Rows[i].Cells["Type"].Value = "Stock Add";
            //                            gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }
            //                        else if (strn_type == "0" && strn_party_no == "11")
            //                        {
            //                            gridLedger.Rows[i].Cells["Particulars"].Value = "Stock Less";
            //                            gridLedger.Rows[i].Cells["Type"].Value = "Stock Less";
            //                            gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }

            //                        else if (strn_type == "2" && strn_party_no == "2")
            //                        {
            //                            gridLedger.Rows[i].Cells["Type"].Value = "Sales Ret";
            //                            gridLedger.Rows[i].Cells["IQty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }
            //                        else if (strn_type == "4" && strn_party_no == "8")
            //                        {
            //                            gridLedger.Rows[i].Cells["Type"].Value = "Purchase Ret";
            //                            gridLedger.Rows[i].Cells["Rqty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }
            //                        else if (strn_type != "" && strn_party_no != "")
            //                        {
            //                            gridLedger.Rows[i].Cells["Rqty"].Value = dt.Rows[i]["nt_qty"].ToString();
            //                            gridLedger.Rows[i].Cells["tot"].Value = dt.Rows[i]["tot_amt"].ToString();
            //                        }
            //                    }
            //                    itemqty = Convert.ToDouble(dt.Rows[i]["nt_qty"].ToString());
            //                    itemcost = Convert.ToDouble(itemqty * itemcost);
            //                    tot_amount = itemcost;
            //                }
            //                else if (strn_party_no == "0" && strntype == "0")
            //                {
            //                    if (dt.Rows[i]["nt_qty"].ToString() != "")
            //                    {
            //                        openqty = Convert.ToDouble(dt.Rows[i]["nt_qty"].ToString());
            //                    }
            //                }
            //            }
            //            if (dt.Rows.Count > 0)
            //            {
            //                if (passingvalues.gridcalculation == "2")
            //                {
            //                    gridcalculation();
            //                }
            //            }
            //        }
            //        else if (dt.Rows.Count == 0)
            //        {
            //            gridLedger.Rows.Clear();
            //            rcqty = 0.00;
            //            issue_qty = 0.00;
            //            rcqty_no = 0.00;
            //            tot_amount = 0.00;
            //            closingqty = 0.00;
            //            openqty = 0.00;
            //            gridcalculation();
            //        }

            //    }
            //    else
            //    {
            //        gridLedger.Rows.Clear();
            //        rcqty = 0.00;
            //        issue_qty = 0.00;
            //        rcqty_no = 0.00;
            //        tot_amount = 0.00;
            //        closingqty = 0.00;
            //        openqty = 0.00;
            //        gridcalculation();

            //    }
            //}
            //else
            //{
            //    if(dt.Rows.Count>0)
            //    {
            //        gridLedger.Rows.Clear();
            //        gridcalculation();
            //    }
            //}
        }
        private void listtype_Click(object sender, EventArgs e)
        {
            if (listtype.Text != "")
            {
                txtlisttype.Text = listtype.SelectedItem.ToString();
            }
        }
        private void listcancel_Click(object sender, EventArgs e)
        {
            if (listcancel.Text != "")
            {
                txtcancel.Text = listcancel.SelectedItem.ToString();
            }
        }
        private void listview_Click(object sender, EventArgs e)
        {
            if (listview.Text != "")
            {
                txtlederof.Text = listview.SelectedItem.ToString();
            }
        }
        private void txtremarks_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            txtlederof.Text = "";
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        double tCount = 0;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                tCount = 0;
                Dataset.dsSalesSummary dsSalesSummaryObj = new Dataset.dsSalesSummary();
                for (int i = 0; i < gridLedger.Rows.Count; i++)
                {
                    dsSalesSummaryObj.Tables["DataTable5"].Rows.Add(Convert.ToString(gridLedger.Rows[i].Cells[1].Value), Convert.ToString(gridLedger.Rows[i].Cells[2].Value), Convert.ToString(gridLedger.Rows[i].Cells[3].Value), Convert.ToString(gridLedger.Rows[i].Cells[4].Value), Convert.ToString(gridLedger.Rows[i].Cells[5].Value), Convert.ToString(gridLedger.Rows[i].Cells[6].Value), Convert.ToString(dtpFrom.Value.Day + "/" + dtpFrom.Value.Month + "/" + dtpFrom.Value.Year), Convert.ToString(dtpTo.Value.Day + "/" + dtpTo.Value.Month + "/" + dtpTo.Value.Year), Convert.ToString(txtlederof.Text), Convert.ToString(txtcancel.Text), Convert.ToString(txtremarks.Text));
                }
                reportViewerSales.Reset();
                //  DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DataTable5"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);

                //reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcItemLedgerReport.rdlc";
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.rdlcItemLedgerReport.rdlc";
                //Passing Parmetes:
                ReportParameter rpReportOn = new ReportParameter("ListType", Convert.ToString(txtlisttype.Text), false);
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });
                dsSalesSummaryObj.Tables["DataTable5"].EndInit();
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
                if (tCount == 0)
                {
                    reportViewerSales.PrintDialog();
                    tCount++;
                }
                reportViewerSales.Clear();
                reportViewerSales.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void gridLedger_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (gridLedger.Columns[e.ColumnIndex].Name == "Type")
            {

                if (e.RowIndex > -1 && e.ColumnIndex == this.gridLedger.Columns["Type"].Index)
                {
                    if (e.Value != null)
                    {
                        string CNumColour = e.Value.ToString();

                        if (CNumColour == "Cancel")
                        {
                            this.gridLedger.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                        }

                    }
                }
            }
        }
    }

}

    

