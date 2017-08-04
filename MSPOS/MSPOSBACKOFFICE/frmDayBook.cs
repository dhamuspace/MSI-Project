using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class frmDayBook : Form
    {
        public frmDayBook()
        {
            InitializeComponent();
            dtNew.Columns.Add("Date", typeof(string));
            dtNew.Columns.Add("Party", typeof(string));
            dtNew.Columns.Add("Type", typeof(string));
            dtNew.Columns.Add("Debit", typeof(string));
            dtNew.Columns.Add("Credit", typeof(string));

            dtNormal.Columns.Add("Date", typeof(string));
            dtNormal.Columns.Add("Party", typeof(string));
            dtNormal.Columns.Add("Type", typeof(string));
            dtNormal.Columns.Add("Narration", typeof(string));
            dtNormal.Columns.Add("Amount", typeof(string));
            dtNormal.Columns.Add("invno", typeof(string));

            dtPreview.Columns.Add("Date", typeof(string));
            dtPreview.Columns.Add("Party", typeof(string));
            dtPreview.Columns.Add("Debit", typeof(string));
            dtPreview.Columns.Add("Credit", typeof(string));

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt_Style = new DataTable();
        DataTable dt_Cancel = new DataTable();
        DataTable dt_Voucher = new DataTable();
        DataTable dtsales = new DataTable();
        DataTable dtNormal = new DataTable();
        DataTable dtPreview = new DataTable();
        string listActionType;
        string chk;
        DataTable dtprnt = new DataTable();
        string cmd = "";
        string chkStr1, chkstr2;
        static string s_infoTxt = "";

        public static string vVoucherNo
        {
            get { return s_infoTxt; }
            set { s_infoTxt = value; }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            lblTot.Text = "";
            lblTotItems.Text = "";
            myDataGrid1.DataSource = null;
            myDataGrid1.Rows.Clear();
            dtNew.Rows.Clear();
            dtNormal.Rows.Clear();
            dtprnt.Rows.Clear();
            pRowCreated = false;
            sRowCreated = false;
            if (txtStyle.Text == "Details")
            {
                dg2gridload();
            }
            else if (txtStyle.Text == "Normal")
            {
                txtAmfrom.Visible = true;
                txtAmtTo.Visible = true;
                lblAmtFrom.Visible = true;
                lblAmtTo.Visible = true;
                myDataGrid1.Visible = true;
                int r = myDataGrid1.Rows.Count;
                gridload();
            }
            else if (txtStyle.Text == "Preview")
            {
                txtAmfrom.Visible = true;
                txtAmtTo.Visible = true;
                lblAmtFrom.Visible = true;
                lblAmtTo.Visible = true;
                myDataGrid1.Visible = true;
                int r = myDataGrid1.Rows.Count;
                dg2gridload();
            }
            else
            {
                gridload();
            }
        }

        DataTable dtdetails = new DataTable();
        DataTable dtNew = new DataTable();
        int Row = 0;
        decimal vCreditTotal = 0, vDebitTotal = 0;

        public void dg2gridload()
        {
            if (txtStyle.Text == "Details")
            {
                myDataGrid1.Visible = false;
                GridDetail.Visible = true;
                txtAmfrom.Visible = false;
                txtAmtTo.Visible = false;
                lblAmtFrom.Visible = false;
                lblAmtTo.Visible = false;

                dtdetails.Rows.Clear();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmddetails = new SqlCommand("select CONVERT(VARCHAR(11), pmas_date, 103)as Date ,a2.pmas_name as Party,a1.PurType_Name as Type,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'", con);
                SqlDataAdapter adptdetails = new SqlDataAdapter(cmddetails);
                adptdetails.Fill(dtdetails);
                con.Close();
                dtNew.Rows.Clear();
                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        dtNew.Rows.Add();
                        dtNew.Rows[i]["Date"] = dtdetails.Rows[i]["Date"].ToString();
                        dtNew.Rows[i]["Type"] = dtdetails.Rows[i]["Type"].ToString();
                        dtNew.Rows[i]["Debit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        dtNew.Rows[i + 1]["Party"] = dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[i + 1]["Credit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        string vParty = "";
                        vParty = dtdetails.Rows[i]["invno"].ToString() + " , " + dtdetails.Rows[i]["Date"].ToString() + " , " + dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[i + 2]["Party"] = vParty;
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        vCreditTotal = vCreditTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                        vCreditTotal = vDebitTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());

                    }
                    else
                    {
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        dtNew.Rows[Row - 1]["Date"] = dtdetails.Rows[i]["Date"].ToString();
                        dtNew.Rows[Row - 1]["Type"] = dtdetails.Rows[i]["Type"].ToString();
                        dtNew.Rows[Row - 1]["Debit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        dtNew.Rows[Row - 1]["Party"] = dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[Row - 1]["Credit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        string vParty = "";
                        vParty = dtdetails.Rows[i]["invno"].ToString() + " , " + dtdetails.Rows[i]["Date"].ToString() + "," + dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[Row - 1]["Party"] = vParty;
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        vCreditTotal = vCreditTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                        vCreditTotal = vDebitTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                    }
                }
                myDataGrid1.DataSource = null;
                myDataGrid1.Rows.Clear();
                GridDetail.DataSource = null;
                GridDetail.Rows.Clear();
                GridDetail.DataSource = dtNew;

                lblTotItems.Text = dtdetails.Rows.Count.ToString();

                this.GridDetail.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridDetail.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridDetail.DefaultCellStyle.Font = new Font(GridDetail.DefaultCellStyle.Font, FontStyle.Bold);

                this.GridDetail.Columns[0].Width = 100;
                this.GridDetail.Columns[1].Width = 400;
                if (txtStyle.Text == "Preview")
                {
                    this.myDataGrid1.Columns[2].Visible = false;
                }
                else
                {
                    this.GridDetail.Columns[2].Visible = true;
                    this.GridDetail.Columns[2].Width = 130;
                }
                this.GridDetail.Columns[3].Width = 100;
                this.GridDetail.Columns[4].Width = 100;
            }
            else if (txtStyle.Text == "Normal")
            {
                txtAmfrom.Visible = true;
                txtAmtTo.Visible = true;
                GridDetail.Visible = false;
                myDataGrid1.Visible = true;
                gridload();

            }
            else if (txtStyle.Text == "Preview")
            {
                myDataGrid1.Visible = false;
                GridDetail.Visible = true;
                txtAmfrom.Visible = false;
                txtAmtTo.Visible = false;
                lblAmtFrom.Visible = false;
                lblAmtTo.Visible = false;

                dtdetails.Rows.Clear();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmddetails = new SqlCommand("select CONVERT(VARCHAR(11), pmas_date, 103)as Date ,a2.pmas_name as Party,a1.PurType_Name as Type,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'", con);
                SqlDataAdapter adptdetails = new SqlDataAdapter(cmddetails);
                adptdetails.Fill(dtdetails);
                con.Close();
                dtNew.Rows.Clear();
                for (int i = 0; i < dtdetails.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        dtNew.Rows.Add();
                        dtNew.Rows[i]["Date"] = dtdetails.Rows[i]["Date"].ToString();
                        dtNew.Rows[i]["Type"] = dtdetails.Rows[i]["Type"].ToString();
                        dtNew.Rows[i]["Debit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        dtNew.Rows[i + 1]["Party"] = dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[i + 1]["Credit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        string vParty = "";
                        vParty = dtdetails.Rows[i]["invno"].ToString() + " , " + dtdetails.Rows[i]["Date"].ToString() + " , " + dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[i + 2]["Party"] = vParty;
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        vCreditTotal = vCreditTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                        vCreditTotal = vDebitTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                    }
                    else
                    {
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        dtNew.Rows[Row - 1]["Date"] = dtdetails.Rows[i]["Date"].ToString();
                        dtNew.Rows[Row - 1]["Type"] = dtdetails.Rows[i]["Type"].ToString();
                        dtNew.Rows[Row - 1]["Debit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        dtNew.Rows[Row - 1]["Party"] = dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[Row - 1]["Credit"] = dtdetails.Rows[i]["Amount"].ToString();
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        string vParty = "";
                        vParty = dtdetails.Rows[i]["invno"].ToString() + " , " + dtdetails.Rows[i]["Date"].ToString() + "," + dtdetails.Rows[i]["Party"].ToString();
                        dtNew.Rows[Row - 1]["Party"] = vParty;
                        dtNew.Rows.Add();
                        Row = dtNew.Rows.Count;
                        vCreditTotal = vCreditTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                        vCreditTotal = vDebitTotal + Convert.ToDecimal(dtdetails.Rows[i]["Amount"].ToString());
                    }
                }
                myDataGrid1.DataSource = null;
                myDataGrid1.Rows.Clear();
                GridDetail.DataSource = null;
                GridDetail.Rows.Clear();
                GridDetail.DataSource = dtNew;

                lblTotItems.Text = dtdetails.Rows.Count.ToString();

                this.GridDetail.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridDetail.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.GridDetail.DefaultCellStyle.Font = new Font(GridDetail.DefaultCellStyle.Font, FontStyle.Bold);

                this.GridDetail.Columns[0].Width = 100;
                this.GridDetail.Columns[1].Width = 400;
                if (txtStyle.Text == "Preview")
                {
                    this.GridDetail.Columns[2].Visible = false;
                }
                else
                {
                    this.myDataGrid1.Columns[2].Visible = true;
                    this.myDataGrid1.Columns[2].Width = 130;
                }
                this.GridDetail.Columns[3].Width = 100;
                this.GridDetail.Columns[4].Width = 100;
            }
            this.GridDetail.ReadOnly = true;
        }
        string @Debottot;
        public void PreviewDebitTotal()
        {
            for (int i = 0; i < GridDetail.Rows.Count; i++)
            {
                if (GridDetail.Rows[i].Cells[2].Value != null && GridDetail.Rows[i].Cells[2].Value != "")
                {
                    @Debottot += Convert.ToDouble(GridDetail.Rows[i].Cells[2].Value == null ? "0.00" : GridDetail.Rows[i].Cells[2].Value.ToString());
                    lblTotItems.Text = Convert.ToInt32(GridDetail.Rows.Count - 1).ToString();
                    this.GridDetail.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                lblTot.Text = Convert.ToString(@Debottot);
            }
        }
        string @Credittot;
        public void PreviewCreditTotal()
        {
            for (int i = 0; i < GridDetail.Rows.Count; i++)
            {
                if (GridDetail.Rows[i].Cells[3].Value != null && GridDetail.Rows[i].Cells[3].Value != "")
                {
                    @Credittot += Convert.ToDouble(GridDetail.Rows[i].Cells[2].Value == null ? "0.00" : GridDetail.Rows[i].Cells[3].Value.ToString());
                    lblTotItems.Text = Convert.ToInt32(GridDetail.Rows.Count - 1).ToString();
                    this.GridDetail.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                lblTot.Text = Convert.ToString(@Credittot);
            }
        }

        bool sRowCreated = false;
        bool pRowCreated = false;
        bool PaymentRowCreated = false;
        public void gridload()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            dtprnt.Rows.Clear();
            dtNormal.Rows.Clear();
            if (txtCash.Text != "" && txtCash.Text != null && txtAmfrom.Text != "" && txtAmfrom.Text != null && txtAmtTo.Text != "" && txtAmtTo.Text != null)
            {
                cmd = "select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type ,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                      " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                      " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                      " and a2.pmas_name='" + txtCash.Text + "' and a2.pmas_netamount between '" + txtAmfrom.Text + "' and '" + txtAmtTo.Text + "' " +
                      " Union All " +
                      " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                      " from  salmas_table a1  " +
                      " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                      " and a1.smas_name='" + txtCash.Text + "' and a1.smas_NetAmount between '" + txtAmfrom.Text + "' and '" + txtAmtTo.Text + "'" +
                      " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  " ;

            }

            else if (txtAmfrom.Text != "" && txtAmfrom.Text != null && txtAmtTo.Text != "" && txtAmtTo.Text != null)
            {
                cmd = "select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type ,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                       " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                       " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                       " and a2.pmas_netamount between '" + txtAmfrom.Text + "' and '" + txtAmtTo.Text + "' " +
                       " Union All " +
                       " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                       " from  salmas_table a1  " +
                       " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                       " and a1.smas_NetAmount between '" + txtAmfrom.Text + "' and '" + txtAmtTo.Text + "' "+
                       " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  ";
            }
            else if (txtCash.Text != "" && txtCash.Text != null)
            {
                cmd = "select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type ,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                       " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                       " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                       " and a2.pmas_name ='" + txtCash.Text + "' " +
                       " Union All " +
                       " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                       " from  salmas_table a1  " +
                       " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                       " and a1.smas_name ='" + txtCash.Text + "' "+
                       " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  ";

            }
            else if (txtCash.Text == "" && txtCash.Text == null && txtAmfrom.Text == "" && txtAmfrom.Text == null && txtAmtTo.Text == "" && txtAmtTo.Text == null)
            {
                cmd = "select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type ,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                       " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                       " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                       " Union All " +
                       " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                       " from  salmas_table a1  " +
                       " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' "+
                       " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  ";
            }
            else if (txtAmfrom.Text == "" && txtAmfrom.Text == null && txtAmtTo.Text == "" && txtAmtTo.Text == null)
            {
                cmd = "Select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type,a2.Pmas_sno as invno ,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                      " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                      " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                      " Union All " +
                      " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                      " from  salmas_table a1  " +
                      " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' "+
                      " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  ";
            }
            else if (txtCash.Text == "" && txtCash.Text == null)
            {
                cmd = "Select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type,a2.Pmas_sno as invno ,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                      " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                      " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                      " Union All " +
                      " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                      " from  salmas_table a1  " +
                      " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'"+
                      " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  ";

            }
            else
            {
                cmd = "Select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type,a2.Pmas_sno as invno ,Convert(numeric(18,2),a2.pmas_netamount) as Amount " +
                      " from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No " +
                      " and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' " +
                      " Union All " +
                      " select CONVERT(VARCHAR(11), a1.smas_billdate, 103)as Date,a1.smas_name as Party,a1.smas_name as Type,smas_billno, Convert(numeric(18,2),a1.smas_NetAmount) as Amount " +
                      " from  salmas_table a1  " +
                      " where a1.smas_billdate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "' "+
                      " Union all " +
                      " Select CONVERT(VARCHAR(11), VoucherDate, 103)as Date,LedgerName as Party,VoucherType as Type,t1.VoucherNo as invno ,Convert(numeric(18,2),t2.DebitAmt) as Amount " +
                      " from T_VoucherTable t1,T_VoucherDetailsTable t2 where t1.VoucherNo= t2.VoucherNo and t2.DebitAmt<>'0' " +
                      " and t1.VoucherDate between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'  ";

            }
            SqlCommand cmdload = new SqlCommand(cmd, con);
            SqlDataAdapter adaptload = new SqlDataAdapter(cmdload);
            adaptload.Fill(dtprnt);
            //myDataGrid1.DataSource = dtprnt;
            string vType = "";
            decimal vTotalAmt = 0;
            if (dtprnt.Rows.Count != 0)
            {
                for (int i = 0; i < dtprnt.Rows.Count; i++)
                {
                    vType = dtprnt.Rows[i]["Type"].ToString();

                    if (vType == "Local Purchase")
                    {
                        dtNormal.Rows.Add();
                        if (pRowCreated == false || dtNormal.Rows.Count == 1)
                        {
                            dtNormal.Rows[i]["Party"] = "PURCHASE";
                            dtNormal.Rows.Add();
                            pRowCreated = true;
                        }
                        dtNormal.Rows[i + 1]["Date"] = dtprnt.Rows[i]["Date"].ToString();
                        dtNormal.Rows[i + 1]["Party"] = dtprnt.Rows[i]["Party"].ToString();
                        dtNormal.Rows[i + 1]["Type"] = dtprnt.Rows[i]["Type"].ToString();
                        dtNormal.Rows[i + 1]["Amount"] = dtprnt.Rows[i]["Amount"].ToString();
                        string vNarration = "";
                        vNarration = dtprnt.Rows[i]["invno"].ToString() + " , " + dtprnt.Rows[i]["Date"].ToString();
                        dtNormal.Rows[i + 1]["Narration"] = vNarration;
                        vTotalAmt = vTotalAmt + Convert.ToDecimal(dtprnt.Rows[i]["Amount"].ToString());
                    }
                    else if (vType == "Payment")
                    {
                        dtNormal.Rows.Add();
                        if (PaymentRowCreated == false)
                        {
                            if (PaymentRowCreated == false || dtNormal.Rows.Count == 1)
                            {
                                dtNormal.Rows[i]["Party"] = "PAYMENT";
                                dtNormal.Rows.Add();
                                PaymentRowCreated = true;

                                dtNormal.Rows[i + 1]["Date"] = dtprnt.Rows[i]["Date"].ToString();
                                dtNormal.Rows[i + 1]["Party"] = dtprnt.Rows[i]["Party"].ToString();
                                dtNormal.Rows[i + 1]["Type"] = dtprnt.Rows[i]["Type"].ToString();
                                dtNormal.Rows[i + 1]["Amount"] = dtprnt.Rows[i]["Amount"].ToString();
                                dtNormal.Rows[i + 1]["invno"] = dtprnt.Rows[i]["invno"].ToString();
                                string vNarration = "";
                                vNarration = dtprnt.Rows[i]["invno"].ToString() + " , " + dtprnt.Rows[i]["Date"].ToString();
                                dtNormal.Rows[i + 1]["Narration"] = vNarration;
                                vTotalAmt = vTotalAmt + Convert.ToDecimal(dtprnt.Rows[i]["Amount"].ToString());
                            }
                            else
                            {
                                dtNormal.Rows[i + 2][1] = "PAYMENT";
                                dtNormal.Rows.Add();
                                PaymentRowCreated = true;

                                dtNormal.Rows[i + 3]["Date"] = dtprnt.Rows[i]["Date"].ToString();
                                dtNormal.Rows[i + 3]["Party"] = dtprnt.Rows[i]["Party"].ToString();
                                dtNormal.Rows[i + 3]["Type"] = dtprnt.Rows[i]["Type"].ToString();
                                dtNormal.Rows[i + 3]["Amount"] = dtprnt.Rows[i]["Amount"].ToString();
                                dtNormal.Rows[i + 3]["ID"] = dtprnt.Rows[i]["invno"].ToString();
                                string vNarration = "";
                                vNarration = dtprnt.Rows[i]["invno"].ToString() + " , " + dtprnt.Rows[i]["Date"].ToString();
                                dtNormal.Rows[i + 3]["Narration"] = vNarration;
                                vTotalAmt = vTotalAmt + Convert.ToDecimal(dtprnt.Rows[i]["Amount"].ToString());
                            }
                        }
                       

                    }
                    else
                    {
                        dtNormal.Rows.Add();
                        if (sRowCreated == false)
                        {
                            if (i != 0)
                            {
                                dtNormal.Rows[i + 1][1] = "SALES";
                                dtNormal.Rows.Add();
                                sRowCreated = true;
                            }
                            else
                            {
                                dtNormal.Rows[i][1] = "SALES";
                                dtNormal.Rows.Add();
                                sRowCreated = true;
                            }
                        }

                        if (i != 0)
                        {
                            dtNormal.Rows[i + 2]["Date"] = dtprnt.Rows[i]["Date"].ToString();
                            dtNormal.Rows[i + 2]["Party"] = dtprnt.Rows[i]["Party"].ToString();
                            dtNormal.Rows[i + 2]["Type"] = dtprnt.Rows[i]["Type"].ToString();
                            dtNormal.Rows[i + 2]["Amount"] = dtprnt.Rows[i]["Amount"].ToString();
                        }
                        else
                        {
                            dtNormal.Rows[i + 1]["Date"] = dtprnt.Rows[i]["Date"].ToString();
                            dtNormal.Rows[i + 1]["Party"] = dtprnt.Rows[i]["Party"].ToString();
                            dtNormal.Rows[i + 1]["Type"] = dtprnt.Rows[i]["Type"].ToString();
                            dtNormal.Rows[i + 1]["Amount"] = dtprnt.Rows[i]["Amount"].ToString();
                        }

                        string vNarration = "";
                        vNarration = dtprnt.Rows[i]["invno"].ToString() + " , " + dtprnt.Rows[i]["Date"].ToString();
                        if (i != 0)
                        {
                            dtNormal.Rows[i + 2]["Narration"] = vNarration;
                        }
                        else
                        {
                            dtNormal.Rows[i + 1]["Narration"] = vNarration;
                        }
                        vTotalAmt = vTotalAmt + Convert.ToDecimal(dtprnt.Rows[i]["Amount"].ToString());
                    }
                }
                int r = dtNormal.Rows.Count;
                dtNormal.Rows.Add();
                dtNormal.Rows.Add();
                dtNormal.Rows[r + 1]["Narration"] = "Total : ";
                dtNormal.Rows[r + 1]["Amount"] = vTotalAmt;
                //myDataGrid1.DataSource = dtNormal;
                myDataGrid1.DataSource = null;
                myDataGrid1.Rows.Clear();

                for (int k = 0; k < dtNormal.Rows.Count; k++)
                {
                    myDataGrid1.Rows.Add();
                    if (dtNormal.Rows[k]["Party"] == "PURCHASE" || dtNormal.Rows[k]["Party"] == "SALES" || dtNormal.Rows[k]["Party"] == "PAYMENT")
                    {
                        this.myDataGrid1.DefaultCellStyle.Font = new Font(myDataGrid1.DefaultCellStyle.Font, FontStyle.Bold);
                        myDataGrid1.Rows[k].DefaultCellStyle.BackColor = Color.Yellow;
                        myDataGrid1.Rows[k].Cells[0].Value = dtNormal.Rows[k]["Date"];
                    }
                    else
                    {
                        myDataGrid1.Rows[k].Cells[0].Value = dtNormal.Rows[k]["Date"];
                    }

                    myDataGrid1.Rows[k].Cells[1].Value = dtNormal.Rows[k]["Party"];
                    myDataGrid1.Rows[k].Cells[2].Value = dtNormal.Rows[k]["Type"];
                    myDataGrid1.Rows[k].Cells[5].Value = dtNormal.Rows[k]["invno"];
                    if (dtNormal.Rows[k]["Narration"] == "Total : ")
                    {
                        myDataGrid1.Rows[k].DefaultCellStyle.BackColor = Color.YellowGreen;
                        myDataGrid1.Rows[k].Cells[3].Value = dtNormal.Rows[k]["Narration"];
                    }
                    else
                    {
                        myDataGrid1.Rows[k].Cells[3].Value = dtNormal.Rows[k]["Narration"];
                    }

                    myDataGrid1.Rows[k].Cells[4].Value = dtNormal.Rows[k]["Amount"];
                   // myDataGrid1.Rows[k].Cells[5].Value = dtNormal.Rows[k]["ID"];
                }

                // myDataGrid1.DataSource = dtNormal;

                lblTotItems.Text = dtprnt.Rows.Count.ToString();

                this.myDataGrid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.myDataGrid1.DefaultCellStyle.Font = new Font(myDataGrid1.DefaultCellStyle.Font, FontStyle.Bold);

                this.myDataGrid1.Columns[0].Width = 100;
                this.myDataGrid1.Columns[1].Width = 300;
                this.myDataGrid1.Columns[2].Width = 200;
                this.myDataGrid1.Columns[3].Width = 200;
                this.myDataGrid1.Columns[4].Width = 100;
                this.myDataGrid1.ReadOnly = true;

            }
            con.Close();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DateTime CurrentDate;
        private void frmDayBook_Load(object sender, EventArgs e)
        {

            GridDetail.Visible = false;
            lstCancel.Visible = false;
            lstStyle.Visible = false;
            lstVoucher.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            pnllstcash.Visible = false;
            lstCash.Visible = false;
            pnllstcounter.Visible = false;
            lstCounter.Visible = false;
            CurrentDate = DateTime.Now;
            daybkFromDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            DaybkToDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            myDataGrid1.Visible = true;
            myDataGrid1.Rows.Clear();
            pRowCreated = false;
            sRowCreated = false;
            gridload();

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }


        private void lstStyle_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    if (lstStyle.Items.Count > 0)
                    {
                        txtStyle.Text = lstStyle.SelectedItem.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            panel2.Visible = false;
            lstStyle.Visible = false;
        }
        public void lstStyleLoad()
        {
            lstStyle.Items.Clear();
            lstStyle.Items.Add("Details");
            lstStyle.Items.Add("Normal");
            lstStyle.Items.Add("Preview");
        }
        private void lstStyle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCancel.Items.Count > 0)
                {
                    txtCancel.Text = lstCancel.SelectedItem.ToString();
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            panel3.Visible = false;
            lstCancel.Visible = false;

        }
        public void lstCancelLoad()
        {
            lstCancel.Items.Clear();
            lstCancel.Items.Add("All");
            lstCancel.Items.Add("Cancelled");
            lstCancel.Items.Add("Not Cancelled");

        }


        private void OnKeyDownVoucher(object sender, KeyEventArgs e)
        {

        }

        private void lstVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstVoucher.Items.Count > 0)
                {
                    txtVoucher.Text = lstVoucher.SelectedItem.ToString();
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            panel4.Visible = false;
            lstVoucher.Visible = false;
        }

        public void lstVoucherLoad()
        {

            lstVoucher.Items.Clear();
            lstVoucher.Items.Add("All");
            lstVoucher.Items.Add("Contra");
            lstVoucher.Items.Add("Cr Note");

        }

        private void txtStyle_Click(object sender, EventArgs e)
        {
            lstStyleLoad();
            panel2.Visible = true;
            lstStyle.Visible = true;
        }

        private void txtCancel_Click(object sender, EventArgs e)
        {
            lstCancelLoad();
            panel3.Visible = true;
            lstCancel.Visible = true;
        }

        private void txtVoucher_Click(object sender, EventArgs e)
        {
            lstVoucherLoad();
            panel4.Visible = true;
            lstVoucher.Visible = true;
        }
        string selectedItemsName = "";
        private void txtStyle_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                if (lstStyle.SelectedIndex < lstStyle.Items.Count - 1)
                {
                    lstStyle.SetSelected(lstStyle.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstStyle.SelectedIndex > 0)
                {
                    lstStyle.SetSelected(lstStyle.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                lstStyle.Visible = false;
                panel2.Visible = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lstStyle.SelectedItems.Count > 0)
                {

                    if (string.IsNullOrEmpty(txtStyle.Text.ToString().Trim()))
                    {
                        txtStyle.Text = lstStyle.SelectedItem.ToString();
                        selectedItemsName = lstStyle.SelectedItem.ToString();
                        pRowCreated = false;
                        lstStyleLoad();

                    }
                    else if (lstStyle.SelectedItem.ToString() == selectedItemsName.ToString().Trim())
                    {
                        // txtSupplierName.Text = listDetails.SelectedItem.ToString();
                        //  ListSelectionChanged();
                    }
                    else if (lstStyle.SelectedItem.ToString().Trim() != selectedItemsName.ToString().Trim())
                    {
                        txtStyle.Text = lstStyle.SelectedItem.ToString();
                        selectedItemsName = lstStyle.SelectedItem.ToString();
                        pRowCreated = false;
                        lstStyleLoad();
                    }
                    panel2.Visible = false;
                    lstStyle.Visible = false;
                    txtCancel.Focus();

                }
                else
                {
                    txtCancel.Focus();
                }
                if (txtStyle.Text == "Details")
                {
                    txtAmfrom.Visible = false;
                    txtAmtTo.Visible = false;
                    myDataGrid1.Visible = false;
                    GridDetail.Visible = true;
                    lblAmtFrom.Visible = false;
                    lblAmtTo.Visible = false;

                }
                else if (txtStyle.Text == "Normal")
                {
                    gridload();
                    txtAmfrom.Visible = true;
                    txtAmtTo.Visible = true;
                    myDataGrid1.Visible = true;
                    GridDetail.Visible = false;
                    lblAmtFrom.Visible = true;
                    lblAmtTo.Visible = true;

                }
                else if (txtStyle.Text == "Preview")
                {
                    txtAmfrom.Visible = false;
                    txtAmtTo.Visible = false;
                    myDataGrid1.Visible = false;
                    GridDetail.Visible = true;
                    lblAmtFrom.Visible = false;
                    lblAmtTo.Visible = false;

                }
            }
        }

        private void txtCancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstCancel.SelectedIndex < lstCancel.Items.Count - 1)
                {
                    lstCancel.SetSelected(lstCancel.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstCancel.SelectedIndex > 0)
                {
                    lstCancel.SetSelected(lstCancel.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                lstCancel.Visible = false;
                panel3.Visible = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lstCancel.SelectedItems.Count > 0)
                {

                    if (string.IsNullOrEmpty(txtCancel.Text.ToString().Trim()))
                    {
                        txtCancel.Text = lstCancel.SelectedItem.ToString();
                        selectedItemsName = lstCancel.SelectedItem.ToString();
                        lstCancelLoad();
                    }
                    else if (lstCancel.SelectedItem.ToString() == selectedItemsName.ToString().Trim())
                    {
                        // txtSupplierName.Text = listDetails.SelectedItem.ToString();
                        //  ListSelectionChanged();
                    }
                    else if (lstCancel.SelectedItem.ToString().Trim() != selectedItemsName.ToString().Trim())
                    {
                        txtCancel.Text = lstCancel.SelectedItem.ToString();
                        selectedItemsName = lstCancel.SelectedItem.ToString();
                        lstCancelLoad();
                    }
                    panel3.Visible = false;
                    lstCancel.Visible = false;
                    txtCash.Focus();
                }
                else
                {
                    txtCash.Focus();
                }
            }
        }


        private void txtVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstVoucher.SelectedIndex < lstVoucher.Items.Count - 1)
                {
                    lstVoucher.SetSelected(lstVoucher.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstVoucher.SelectedIndex > 0)
                {
                    lstVoucher.SetSelected(lstVoucher.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                lstVoucher.Visible = false;
                panel4.Visible = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lstVoucher.SelectedItems.Count > 0)
                {

                    if (string.IsNullOrEmpty(txtVoucher.Text.ToString().Trim()))
                    {
                        txtVoucher.Text = lstVoucher.SelectedItem.ToString();
                        selectedItemsName = lstVoucher.SelectedItem.ToString();
                        lstVoucherLoad();
                    }
                    else if (lstVoucher.SelectedItem.ToString() == selectedItemsName.ToString().Trim())
                    {
                        // txtSupplierName.Text = listDetails.SelectedItem.ToString();
                        //  ListSelectionChanged();
                    }
                    else if (lstVoucher.SelectedItem.ToString().Trim() != selectedItemsName.ToString().Trim())
                    {
                        txtVoucher.Text = lstVoucher.SelectedItem.ToString();
                        selectedItemsName = lstVoucher.SelectedItem.ToString();
                        lstVoucherLoad();
                    }
                    panel4.Visible = false;
                    lstVoucher.Visible = false;
                    txtStyle.Focus();
                }
                else
                {
                    txtStyle.Focus();
                }
            }
        }

        private void daybkFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DaybkToDate.Focus();
            }
        }

        private void DaybkToDate_Enter(object sender, EventArgs e)
        {

        }

        private void DaybkToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCounter.Focus();
            }
        }

        private void txtCounter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstCounter.SelectedIndex < lstCounter.Items.Count - 1)
                {
                    lstCounter.SetSelected(lstCounter.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstCounter.SelectedIndex > 0)
                {
                    lstCounter.SetSelected(lstCounter.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                lstCounter.Visible = false;
                pnllstcounter.Visible = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lstCounter.SelectedItems.Count > 0)
                {

                    if (string.IsNullOrEmpty(txtCounter.Text.ToString().Trim()))
                    {
                        txtCounter.Text = lstCounter.SelectedItem.ToString();
                        selectedItemsName = lstCounter.SelectedItem.ToString();
                        lstCounterLoad();
                    }
                    else if (lstCounter.SelectedItem.ToString() == selectedItemsName.ToString().Trim())
                    {
                        // txtSupplierName.Text = listDetails.SelectedItem.ToString();
                        //  ListSelectionChanged();
                    }
                    else if (lstCounter.SelectedItem.ToString().Trim() != selectedItemsName.ToString().Trim())
                    {
                        txtCounter.Text = lstCounter.SelectedItem.ToString();
                        selectedItemsName = lstCounter.SelectedItem.ToString();
                        lstCounterLoad();
                    }
                    pnllstcounter.Visible = false;
                    lstCounter.Visible = false;
                    txtVoucher.Focus();
                }
                else
                {
                    txtVoucher.Focus();
                }
            }
        }

        private void txtAmfrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmtTo.Focus();
            }
        }

        private void txtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstCash.SelectedIndex < lstCash.Items.Count - 1)
                {
                    lstCash.SetSelected(lstCash.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstCash.SelectedIndex > 0)
                {
                    lstCash.SetSelected(lstCash.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                lstCash.Visible = false;
                pnllstcash.Visible = false;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (lstCash.SelectedItems.Count > 0)
                {

                    if (string.IsNullOrEmpty(txtCash.Text.ToString().Trim()))
                    {
                        txtCash.Text = lstCash.SelectedItem.ToString();
                        selectedItemsName = lstCash.SelectedItem.ToString();
                        lstCashLoad();
                    }
                    else if (lstCash.SelectedItem.ToString() == selectedItemsName.ToString().Trim())
                    {
                        // txtSupplierName.Text = listDetails.SelectedItem.ToString();
                        //  ListSelectionChanged();
                    }
                    else if (lstCash.SelectedItem.ToString().Trim() != selectedItemsName.ToString().Trim())
                    {
                        txtCash.Text = lstCash.SelectedItem.ToString();
                        selectedItemsName = lstCash.SelectedItem.ToString();
                        lstCashLoad();
                    }
                    pnllstcash.Visible = false;
                    lstCash.Visible = false;
                    txtAmfrom.Focus();
                }
                else
                {
                    txtAmfrom.Focus();
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleardata();
        }
        public void cleardata()
        {
            dtNormal.Rows.Clear();
            dtdetails.Rows.Clear();
            dtNew.Rows.Clear();
            dtPreview.Rows.Clear();
            dtprnt.Rows.Clear();

            this.GridDetail.DataSource = null;
            this.myDataGrid1.DataSource = null;
            myDataGrid1.Rows.Clear();
            GridDetail.Rows.Clear();
            this.GridDetail.Visible = false;
            this.myDataGrid1.Visible = true;


            daybkFromDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            DaybkToDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);

            txtAmtTo.Text = "";
            txtCancel.Text = "";
            txtCash.Text = "";
            txtStyle.Text = "";
            txtVoucher.Text = "";
            txtCounter.Text = "";
            txtAmfrom.Text = "";

            txtAmfrom.Visible = true;
            txtAmtTo.Visible = true;
            lblAmtFrom.Visible = true;
            lblAmtTo.Visible = true;

            lblTotItems.Text = "";
            lblTot.Text = "";
            pRowCreated = false;
            sRowCreated = false;

            lstCancel.Items.Clear();
            lstStyle.Items.Clear();
            lstVoucher.Items.Clear();
            lstCash.Items.Clear();
            lstCounter.Items.Clear();

            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            pnllstcash.Visible = false;
            pnllstcounter.Visible = false;
            gridload();

        }
        Microsoft.Reporting.WinForms.ReportViewer rptviewer = new Microsoft.Reporting.WinForms.ReportViewer();
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DayBookNormal();
            DayBookDetails();
        }
        public void DayBookDetails()
        {
            try
            {
                DateTime tFromdate = new DateTime();
                DateTime tTodate = new DateTime();

                tFromdate = Convert.ToDateTime(daybkFromDate.Text.ToString());
                tTodate = Convert.ToDateTime(DaybkToDate.Text.ToString());
                string strHeader1 = "";
                if (txtStyle.Text == "Details")
                {
                    strHeader1 = "DayBook Detailed Report";
                    DsDayData dsDaydetails = new DsDayData();
                    for (int i = 0; i < GridDetail.Rows.Count; i++)
                    {
                        dsDaydetails.Tables["DtDayDetails"].Rows.Add(GridDetail.Rows[i].Cells[0].Value, GridDetail.Rows[i].Cells[1].Value, GridDetail.Rows[i].Cells[2].Value, GridDetail.Rows[i].Cells[3].Value, GridDetail.Rows[i].Cells[4].Value);
                    }
                    rptviewer.Reset();
                    ReportDataSource ds = new ReportDataSource("DataSet1", dsDaydetails.Tables["DtDayDetails"]);
                    rptviewer.LocalReport.DataSources.Add(ds);
                    rptviewer.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.RdlcDayDetails.rdlc";

                    ReportParameter rptFromdate = new ReportParameter("rptFrom", Convert.ToString(tFromdate.ToString("dd/MM/yyyy")), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptFromdate });

                    ReportParameter rptTodate = new ReportParameter("rptTo", Convert.ToString(tTodate.ToString("dd/MM/yyyy")), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptTodate });
                    DateTime dt = DateTime.Now;
                    string CDate = dt.ToString("dd/MM/yyy");
                    ReportParameter rptCDate = new ReportParameter("rptDate", Convert.ToString(CDate), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptCDate });
                    ReportParameter rptHeader1 = new ReportParameter("rptHeader", Convert.ToString(strHeader1), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptHeader1 });

                    dsDaydetails.Tables["DtDayDetails"].EndInit();
                    rptviewer.RefreshReport();
                    rptviewer.RenderingComplete += new RenderingCompleteEventHandler(PrintSales2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }

        }
        public void DayBookNormal()
        {
            try
            {
                DateTime tFromdate = new DateTime();
                DateTime tTodate = new DateTime();

                tFromdate = Convert.ToDateTime(daybkFromDate.Text.ToString());
                tTodate = Convert.ToDateTime(DaybkToDate.Text.ToString());

                DsDayData dssummary = new DsDayData();
                string strHeader = "";
                if (txtStyle.Text == "Normal" || txtStyle.Text == "")
                {
                    strHeader = "DayBook Report";

                    for (int i = 0; i < myDataGrid1.Rows.Count; i++)
                    {
                        dssummary.Tables["DtDayData"].Rows.Add(myDataGrid1.Rows[i].Cells[0].Value, myDataGrid1.Rows[i].Cells[1].Value, myDataGrid1.Rows[i].Cells[2].Value, myDataGrid1.Rows[i].Cells[3].Value, myDataGrid1.Rows[i].Cells[4].Value);
                    }
                    rptviewer.Reset();
                    ReportDataSource ds = new ReportDataSource("DataSet1", dssummary.Tables["DtDayData"]);
                    rptviewer.LocalReport.DataSources.Add(ds);
                    rptviewer.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.rdlcday.rdlc";

                    ReportParameter rptFromdate = new ReportParameter("rptFrom", Convert.ToString(tFromdate.ToString("dd/MM/yyyy")), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptFromdate });

                    ReportParameter rptTodate = new ReportParameter("rptTo", Convert.ToString(tTodate.ToString("dd/MM/yyyy")), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptTodate });

                    DateTime dt = DateTime.Now;
                    string CDate = dt.ToString("dd/MM/yyy");
                    ReportParameter rptCDate = new ReportParameter("rptDate", Convert.ToString(CDate), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptCDate });

                    ReportParameter rptHeader1 = new ReportParameter("rptHeader", Convert.ToString(strHeader), false);
                    this.rptviewer.LocalReport.SetParameters(new ReportParameter[] { rptHeader1 });

                    dssummary.Tables["DtDayData"].EndInit();
                    rptviewer.RefreshReport();
                    rptviewer.RenderingComplete += new RenderingCompleteEventHandler(PrintSales2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }

        }

        public void PrintSales2(object sender, RenderingCompleteEventArgs e)
        {
            try
            {
                rptviewer.PrintDialog();
                rptviewer.Clear();
                rptviewer.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            myDataGrid1.DataSource = null;
            myDataGrid1.Rows.Clear();
        }

        private void lstCash_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    if (lstCash.Items.Count > 0)
                    {
                        txtCash.Text = lstCash.SelectedItem.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            pnllstcash.Visible = false;
            lstCash.Visible = false;
        }

        public void lstCashLoad()
        {
            lstCash.Items.Clear();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            DataTable dtcash = new DataTable();
            SqlCommand lstcashcmd = new SqlCommand("select Ledger_name from Ledger_table where ledger_no>14  order by Ledger_name ", con);
            SqlDataAdapter adaptcash = new SqlDataAdapter(lstcashcmd);
            adaptcash.Fill(dtcash);
            lstcashcmd.ExecuteNonQuery();
            con.Close();
            if (dtcash.Rows.Count > 0)
            {
                for (int i = 0; i < dtcash.Rows.Count; i++)
                {
                    lstCash.Items.Add(dtcash.Rows[i]["Ledger_name"].ToString());
                }
            }

        }


        private void txtCash_Click(object sender, EventArgs e)
        {

            lstCashLoad();
            pnllstcash.Visible = true;
            lstCash.Visible = true;
        }

        private void daybkFromDate_ValueChanged(object sender, EventArgs e)
        {
            //gridload();
            //@tTotal = 0;
        }

        private void DaybkToDate_ValueChanged(object sender, EventArgs e)
        {
            //gridload();
            //@tTotal = 0;
        }

        private void lstCounter_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    if (lstCounter.Items.Count > 0)
                    {
                        txtCounter.Text = lstCounter.SelectedItem.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            pnllstcounter.Visible = false;
            lstCounter.Visible = false;
        }
        public void lstCounterLoad()
        {
            lstCounter.Items.Clear();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            DataTable dtcounter = new DataTable();
            SqlCommand lstcountercmd = new SqlCommand("select ctr_name from counter_table order by ctr_name", con);
            SqlDataAdapter adaptcash = new SqlDataAdapter(lstcountercmd);
            adaptcash.Fill(dtcounter);
            lstcountercmd.ExecuteNonQuery();
            con.Close();
            if (dtcounter.Rows.Count > 0)
            {
                for (int i = 0; i < dtcounter.Rows.Count; i++)
                {
                    lstCounter.Items.Add(dtcounter.Rows[i]["ctr_name"].ToString());
                }
            }

        }

        private void txtCounter_Click(object sender, EventArgs e)
        {
            lstCounterLoad();
            pnllstcounter.Visible = true;
            lstCounter.Visible = true;
        }

        private void txtAmtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dtenter = new DataTable();
                dtNormal.Rows.Clear();
                decimal vTotalAmt = 0;
                SqlCommand cmdenter = new SqlCommand("select CONVERT(VARCHAR(11), pmas_date, 103)as Date,a2.pmas_name as Party,a1.PurType_Name as Type ,a2.Pmas_sno as invno,Convert(numeric(18,2),a2.pmas_netamount) as Amount from purmas_table a2,purtype_table a1  where a2.PurType=a1.PurType_No and a2.pmas_date between '" + daybkFromDate.Value.Year + "/" + daybkFromDate.Value.Month + "/" + daybkFromDate.Value.Day + "' and '" + DaybkToDate.Value.Year + "/" + DaybkToDate.Value.Month + "/" + DaybkToDate.Value.Day + "'and a2.pmas_netamount between '" + txtAmfrom.Text + "' and '" + txtAmtTo.Text + "'", con);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adtenter = new SqlDataAdapter(cmdenter);
                cmdenter.ExecuteNonQuery();
                con.Close();
                adtenter.Fill(dtenter);

                for (int i = 0; i < dtenter.Rows.Count; i++)
                {
                    dtNormal.Rows.Add();
                    dtNormal.Rows[i]["Date"] = dtenter.Rows[i]["Date"].ToString();
                    dtNormal.Rows[i]["Party"] = dtenter.Rows[i]["Party"].ToString();
                    dtNormal.Rows[i]["Type"] = dtenter.Rows[i]["Type"].ToString();
                    dtNormal.Rows[i]["Amount"] = dtenter.Rows[i]["Amount"].ToString();
                    dtNormal.Rows[i]["invno"] = dtenter.Rows[i]["invno"].ToString();
                    string vNarration = "";
                    vNarration = dtenter.Rows[i]["invno"].ToString() + " , " + dtenter.Rows[i]["Date"].ToString();
                    dtNormal.Rows[i]["Narration"] = vNarration;
                    vTotalAmt = vTotalAmt + Convert.ToDecimal(dtenter.Rows[i]["Amount"].ToString());
                }

                int r = dtNormal.Rows.Count;
                dtNormal.Rows.Add();
                dtNormal.Rows.Add();
                dtNormal.Rows[r + 1]["Narration"] = "Total : ";
                dtNormal.Rows[r + 1]["Amount"] = vTotalAmt;
                myDataGrid1.DataSource = dtNormal;
                lblTotItems.Text = dtenter.Rows.Count.ToString();
                this.myDataGrid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.myDataGrid1.Columns[0].Width = 100;
                this.myDataGrid1.Columns[1].Width = 300;
                this.myDataGrid1.Columns[2].Width = 130;
                this.myDataGrid1.Columns[3].Width = 200;
                this.myDataGrid1.Columns[4].Width = 100;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        bool isChk = false;
        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            if (txtCash.Text.Trim() != null && txtCash.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("Select * from Ledger_table Where Ledger_no>14 and ledger_name like @LedgerName order by Ledger_name", con);
                cmd.Parameters.AddWithValue("@LedgerName", txtCash.Text.Trim() + '%');
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroupLedgerSelect = new DataTable();
                dtGroupLedgerSelect.Rows.Clear();
                adp.Fill(dtGroupLedgerSelect);
                isChk = false;
                if (dtGroupLedgerSelect.Rows.Count > 0)
                {
                    string tempstr = dtGroupLedgerSelect.Rows[0]["Ledger_name"].ToString().Trim();
                    for (int k = 0; k < lstCash.Items.Count; k++)
                    {
                        if (tempstr == lstCash.Items[k].ToString().Trim())
                        {
                            isChk = true;
                            lstCash.SetSelected(k, true);
                            txtCash.Select();
                            chk = "1";
                            txtCash.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk = "2";
                    if (txtCash.Text != "")
                    {
                        string name = txtCash.Text.Remove(txtCash.Text.Length - 1);
                        txtCash.Text = name.ToString();
                        txtCash.Select(txtCash.Text.Length, 0);
                    }
                    txtCash.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    chk = "1";
                }
                else
                {
                    chk = "1";
                }
            }
        }

        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
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
        bool isChk1 = false;
        private void txtCounter_TextChanged(object sender, EventArgs e)
        {
            if (txtCounter.Text.Trim() != null && txtCounter.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("select ctr_name from counter_table where ctr_name like @ctr_name order by ctr_name", con);
                cmd.Parameters.AddWithValue("@ctr_name", txtCounter.Text.Trim() + '%');
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroup = new DataTable();
                dtGroup.Rows.Clear();
                adp.Fill(dtGroup);
                isChk1 = false;
                if (dtGroup.Rows.Count > 0)
                {
                    string tempstr = dtGroup.Rows[0]["ctr_name"].ToString().Trim();
                    for (int k = 0; k < lstCounter.Items.Count; k++)
                    {
                        if (tempstr == lstCounter.Items[k].ToString().Trim())
                        {
                            isChk1 = true;
                            lstCounter.SetSelected(k, true);
                            txtCounter.Select();
                            chk = "1";
                            txtCounter.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk1 == false)
                {
                    chk = "2";
                    if (txtCounter.Text != "")
                    {
                        string name = txtCounter.Text.Remove(txtCounter.Text.Length - 1);
                        txtCounter.Text = name.ToString();
                        txtCounter.Select(txtCounter.Text.Length, 0);
                    }
                    txtCounter.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    chk = "1";
                }
                else
                {
                    chk = "1";
                }
            }
        }

        DataTable dtstyle1 = new DataTable();

        private void txtStyle_TextChanged(object sender, EventArgs e)
        {

        }

        private void daybkFromDate_Leave(object sender, EventArgs e)
        {
            pRowCreated = false;
            sRowCreated = false;
            gridload();
        }

        private void DaybkToDate_Leave(object sender, EventArgs e)
        {
            pRowCreated = false;
            sRowCreated = false;
            gridload();
        }

        private void myDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtStyle_Leave(object sender, EventArgs e)
        {
            lstStyle.Visible = false;
            panel2.Visible = false;
        }

        private void txtCancel_Leave(object sender, EventArgs e)
        {
            lstCancel.Visible = false;
            panel3.Visible = false;
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            lstCash.Visible = false;
            pnllstcash.Visible = false;
        }

        private void txtCounter_Leave(object sender, EventArgs e)
        {
            lstCounter.Visible = false;
            pnllstcounter.Visible = false;
        }

        private void txtVoucher_Leave(object sender, EventArgs e)
        {
            lstVoucher.Visible = false;
            panel4.Visible = false;
        }

        private void myDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // if (myDataGrid1.Rows[e.RowIndex].Cells["Particulars"].Value.ToString().Trim() == "Opening Stock")
                //{

                //MSPOSBACKOFFICE.ItemCreations frm = new MSPOSBACKOFFICE.ItemCreations();
                //frm.MdiParent = this.ParentForm;
                //// passingvalues.tot = total.ToString();
                //frm.StartPosition = FormStartPosition.Manual;
                //frm.WindowState = FormWindowState.Normal;
                //frm.Location = new Point(0, 80);
                //frm.Show();
                //}
                if (myDataGrid1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() == "Cash Sales")
                {
                    chkbox.FormIdentify = "frmDayBook";
                    int row = e.RowIndex;
                    var tempdate = myDataGrid1.Rows[row].Cells["invno"].Value.ToString();
                    if (tempdate != "")
                    {
                        // string selectedBillno = gridLedger.Rows[row].Cells["Strn_no"].Value.ToString();
                        // Double selectedbillamt = Convert.ToDouble(gridLedger.Rows[row].Cells["Value"].Value.ToString());
                        //MessageBox.Show(selectedBillno);
                        DataTable dtNew1 = new DataTable();
                        dtNew1.Rows.Clear();
                        SqlDataAdapter adpChk = new SqlDataAdapter("select Pmas_sno as invno from purmas_table where Pmas_sno='" + myDataGrid1.Rows[row].Cells["invno"].Value.ToString() + "'", con);
                        adpChk.Fill(dtNew1);
                        if (dtNew1.Rows.Count > 0)
                        {
                            chkbox.SalesBillNo = dtNew1.Rows[0][0].ToString();
                        }

                        chkbox.SalesBillamt = Convert.ToDouble(myDataGrid1.Rows[row].Cells["invno"].Value.ToString());
                        frmSalesAlteration frm = new frmSalesAlteration();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                       // this.Hide(); 
                    }
                }
                else if (myDataGrid1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() == "Local Purchase")
                {
                    if (myDataGrid1.Rows.Count > 0)
                    {
                        int i = e.RowIndex;
                        string strn_number = myDataGrid1.Rows[i].Cells["invno"].Value.ToString();
                        PurchaseEntry1 frm = new PurchaseEntry1(strn_number);
                        frm.MdiParent = this.ParentForm;
                        // passingvalues.tot = total.ToString();
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                }
            }
           catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }
    }
}
