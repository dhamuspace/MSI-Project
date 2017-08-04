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
using System.Globalization;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class StckAdjDisplay : Form
    {
        // SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
      //  string itemname;
        DataTable dt = new DataTable();
        string FromsearchDate;
        string ToSearchDate;
        public StckAdjDisplay()
        {

            InitializeComponent();
            this.grd_adj_dis.DefaultCellStyle.Font = new Font("Tahoma", 12);
            this.grd_adj_dis.RowTemplate.Height = 25;
            
            // txt_fromdate.Format = DateTimePickerFormat.Custom;
            //txt_fromdate.CustomFormat = "dd/MM/yyyy";

            //txt_todate.Format = DateTimePickerFormat.Custom;
            // txt_todate.CustomFormat = "dd/MM/yyyy";

            pnl_ctr_name.Visible = false;
            string FromDate = txt_fromdate.Value.ToShortDateString();
            DateTime stdate = Convert.ToDateTime(FromDate);
            FromsearchDate = stdate.ToString("yyyy-MM-dd");

            string ToDate = txt_todate.Value.ToShortDateString();
            DateTime enddate = Convert.ToDateTime(FromDate);
            //string inputFormat = "dd/MM/yyyy";
            ToSearchDate = enddate.ToString("yyyy-MM-dd");

            // lstCancel:
            //txt_cancel.Text = "All";

            //DateTime fromDateconvert = DateTime.ParseExact(FromDate, inputFormat, CultureInfo.InvariantCulture);
            //DateTime toDateConvert = DateTime.ParseExact(FromDate, inputFormat, CultureInfo.InvariantCulture);
            ////DateTime date = DateTime.ParseExact(txt_fromdate.Text.ToString(), , CultureInfo.InvariantCulture);
            //FromsearchDate = fromDateconvert.ToString("yyyy-MM-dd hh:mm:ss.fff");
            //ToSearchDate = toDateConvert.ToString("yyyy-MM-dd hh:mm:ss.fff");
            dtDisplay.Columns.Add("AdjNo", typeof(string));
            dtDisplay.Columns.Add("Date", typeof(string));
            dtDisplay.Columns.Add("Code", typeof(string));
            dtDisplay.Columns.Add("Name", typeof(string));
            dtDisplay.Columns.Add("Unit", typeof(string));
            dtDisplay.Columns.Add("Less_Qty", typeof(string));
            dtDisplay.Columns.Add("Add_Qty", typeof(string));
            dtDisplay.Columns.Add("Rate", typeof(string));
            dtDisplay.Columns.Add("AddAmount", typeof(string));
            dtDisplay.Columns.Add("LessAmount", typeof(string));
            gridload();
            totalrecords();
            //grd_adj_dis.CurrentCell = grd_adj_dis.Rows[0].Cells[0];

            grd_adj_dis.Columns[0].Width = 55;
            grd_adj_dis.Columns[1].Width = 95;
            grd_adj_dis.Columns[2].Width = 100;
            grd_adj_dis.Columns[3].Width = 220;
            grd_adj_dis.Columns[4].Width = 60;
            grd_adj_dis.Columns[5].Width = 90;
            grd_adj_dis.Columns[6].Width = 90;
            grd_adj_dis.Columns[7].Width = 100;
            grd_adj_dis.Columns[8].Width = 100;
            grd_adj_dis.Columns[9].Width = 100;
            grd_adj_dis.Focus();
            // grd_adj_dis.CurrentCell = grd_adj_dis.Rows[0].Cells[0];

            foreach (DataGridViewColumn col in grd_adj_dis.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

        }
        public void totalrecords()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(Adj_no) from adjmas_table", con);
            int count_adj = Convert.ToInt16(cmd.ExecuteScalar());
            lbl_amt.Text = count_adj.ToString();
            con.Close();
        }

        DataSet chkDs = new DataSet();
        DataSet chkDs1 = new DataSet();
        public void quantitytype(string Number)
        {
            //transaction Type:
            //here write code for ntqty, lessqty:

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            chkDs1.Tables.Clear();
            string lessqtyqry = "select strn_type from stktrn_table where strn_sno='" + Number + "'";
            SqlDataAdapter adp = new SqlDataAdapter(lessqtyqry, con);
            adp.Fill(chkDs1, "Chk");
            for (int i = 0; i < chkDs1.Tables["Chk"].Rows.Count;)
            {
                QtyType = Convert.ToInt16(chkDs1.Tables["Chk"].Rows[0][0].ToString());
                break;
            }

        }

        DataTable dtDisplay = new DataTable();
        DataTable dt1 = new DataTable();
       // SqlDataReader dr4 = null;
        int QtyType;
        string Adjustnumber;

        public void gridload()
        {

            //if (txt_cancel.Text == "All" && txt_item.Text !="" && txt_countername.Text !="")
            //{
            //    con.Close();
            //    con.Open();
            //    DataTable dtusers = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter("select stck_adj_no as AdjNo,stck_date as Date,stckA_code as Code,stckA_Name as Name,stckA_Unit as Unit,stck_lessTXqty as LessTaxqty,stckA_lesQty as Less_Qty,stck_lessTXqty as AddTaxQty,stckA_addQty as Add_Qty,stckA_Rate as Rate,stck_addamt as AddAmount,stck_lessamt as LessAmount from Stockadjmas_table where stckA_Name='"+txt_item.Text+"' and stckA_CtrName='"+txt_countername.Text+"'", con);
            //    //SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            //    da.Fill(dtusers);
            //    grd_adj_dis.DataSource = dtusers;
            //    grd_adj_dis.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //    con.Close();
            //    da.Dispose();
            //}
            //if (txt_cancel.Text == "Cancelled" && txt_countername.Text !="" && txt_item.Text !="")
            //{
            //    con.Close();
            //    con.Open();
            //    DataTable dtusers = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter("select stck_adj_no as AdjNo,stck_date as Date,stckA_code as Code,stckA_Name as Name,stckA_Unit as Unit,stck_lessTXqty as LessTaxqty,stckA_lesQty as Less_Qty,stck_lessTXqty as AddTaxQty,stckA_addQty as Add_Qty,stckA_Rate as Rate,stck_addamt as AddAmount,stck_lessamt as LessAmount from Stockadjmas_table  where stck_cancel='True' and stckA_Name='" + txt_item.Text + "'and stckA_CtrName='"+txt_countername.Text+"' ", con);
            //    SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            //    da.Fill(dtusers);
            //    grd_adj_dis.DataSource = dtusers;
            //    grd_adj_dis.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //    con.Close();
            //    da.Dispose();
            //}
            //if (txt_cancel.Text == "Not Cancelled" && txt_item.Text != "" && txt_countername.Text != "")
            //{
            //    con.Close();
            //    con.Open();
            //    DataTable dtusers = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter("select stck_adj_no as AdjNo,stck_date as Date,stckA_code as Code,stckA_Name as Name,stckA_Unit as Unit,stck_lessTXqty as LessTaxqty,stckA_lesQty as Less_Qty,stck_lessTXqty as AddTaxQty,stckA_addQty as Add_Qty,stckA_Rate as Rate,stck_addamt as AddAmount,stck_lessamt as LessAmount from Stockadjmas_table where stck_cancel='False' and stckA_Name='" + txt_item.Text + "'and stckA_CtrName='"+txt_countername.Text+"'", con);
            //    SqlCommandBuilder cmd = new SqlCommandBuilder(da);
            //    da.Fill(dtusers);
            //    grd_adj_dis.DataSource = dtusers;
            //    grd_adj_dis.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //    con.Close();
            //    da.Dispose();
            //}


            //else
            //{
            if (grd_adj_dis.Rows.Count > 0)
            {
                do
                {
                    foreach (DataGridViewRow row in grd_adj_dis.Rows)
                    {
                        try
                        {
                            grd_adj_dis.Rows.Remove(row);
                        }
                        catch (Exception) { }
                    }
                } while (grd_adj_dis.Rows.Count > 0);
            }
            dt1.Clear();

            SqlDataAdapter cmd1 = new SqlDataAdapter("Select strn_sno from stktrn_table where strn_type='11' or strn_type='12'  order by strn_no ASC ", con);
            cmd1.Fill(dt1);
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                Adjustnumber = dt1.Rows[j][0].ToString();

                chkDs.Tables.Clear();

                //SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' ", con);
                SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no and stktrn_table.strn_date between '" + txt_fromdate.Value.Year + "/" + txt_fromdate.Value.Month + "/" + txt_fromdate.Value.Day + "' and '" + txt_todate.Value.Year + "/" + txt_todate.Value.Month + "/" + txt_todate.Value.Day + "' and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' ", con);
                cmd.Fill(chkDs, "Chk1");

                int i = 0;
                for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                {

                    quantitytype(Adjustnumber);

                    i = i + 1;
                    if (QtyType == 11)
                    {
                        if (i == 1)
                        {

                            dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                        }
                        else
                        {
                            string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                            for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                            {
                                quantitytype(temp);
                                if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                {
                                    dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                }
                            }
                        }
                    }
                    if (QtyType == 12)
                    {
                        if (i == 1)
                        {

                            dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                        }
                        else
                        {
                            string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                            for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                            {
                                quantitytype(temp);
                                if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                {

                                    dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                }

                            }
                        }

                    }

                }
                con.Close();
            }
            grd_adj_dis.DataSource = dtDisplay;

            // }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StckAdjDisplay_Load(object sender, EventArgs e)
        {
            txt_countername.Select();
            pnl_ctr_name.Visible = false;
            pnl_item.Visible = false;
            pnl_Cancel.Visible = false;
            lst_cancel.Visible = false;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void txt_countername_Enter(object sender, EventArgs e)
        {

            //if (txt_countername.TextLength > 0)
            //{
            //    txt_countername.Select(0, txt_countername.TextLength+1);
            //}
            // txt_countername.Text = "";
            pnl_Cancel.Visible = false;
            pnl_item.Visible = false;            
            //txt_countername.SelectAll();

        }

        private void txt_countername_Leave(object sender, EventArgs e)
        {
            //pnl_ctr_name.Visible = false;
            //lst_ctrname.Visible = false;
            //txt_item.Focus();
            //item name:
            // FetchByCounterName();



        }
        public void countload()
        {

            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select ctr_name from counter_table order by ctr_name ASC", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            lst_ctrname.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_ctrname.Items.Add(dt.Rows[i]["ctr_name"].ToString());
                    //txt_countername.Text = lst_ctrname.SelectedItem.ToString();
                    this.lst_ctrname.SelectedIndex = 0;
                }
            }
            con.Close();
            adp.Dispose();

        }
        public void ItemNameload()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Item_name from Item_table order by Item_name ASC", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            lst_item.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_item.Items.Add(dt.Rows[i]["Item_name"].ToString());
                    //itemname = (dt.Rows[0]["Item_name"].ToString());
                    this.lst_item.SelectedIndex = 0;
                }
            }
            con.Close();
            adp.Dispose();
        }
        private void txt_fromdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_todate.Focus();
            }


            if (e.KeyChar == (Char)Keys.Back)
            {

                txt_fromdate.Focus();

            }
        }

        public void FetchByCounterName()
        {
            string FromDate = txt_fromdate.Text;
            DateTime stdate = Convert.ToDateTime(FromDate);
            FromsearchDate = stdate.ToString("yyyy-MM-dd");

            string Todate = txt_todate.Text;
            DateTime endDate = Convert.ToDateTime(Todate);
            ToSearchDate = endDate.ToString("yyyy-MM-dd");
            dt1.Clear();


            con.Open();
            string CounterNoqry = "select ctr_no from counter_table where ctr_name='" + txt_countername.Text.Trim() + "'";
            SqlCommand cmdCounterNO = new SqlCommand(CounterNoqry, con);
            var temp1 = cmdCounterNO.ExecuteScalar();
            if (temp1 != null)
            {
                string CounterNo = cmdCounterNO.ExecuteScalar().ToString();
                con.Close();
                if (grd_adj_dis.Rows.Count > 0)
                {
                    do
                    {
                        foreach (DataGridViewRow row in grd_adj_dis.Rows)
                        {
                            try
                            {
                                grd_adj_dis.Rows.Remove(row);
                            }
                            catch (Exception) { }
                        }
                    } while (grd_adj_dis.Rows.Count > 0);

                }
                dt1.Clear();

                SqlDataAdapter cmd1 = new SqlDataAdapter("Select strn_sno from stktrn_table where strn_type='11' or strn_type='12' and ctr_no='" + CounterNo + "'  ", con);
                cmd1.Fill(dt1);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Adjustnumber = dt1.Rows[j][0].ToString();

                    chkDs.Tables.Clear();

                    //SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' and stktrn_table.ctr_no='" + CounterNo + "' ", con);
                    SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no and stktrn_table.strn_date between '" + FromsearchDate + "' and '" + ToSearchDate + "'  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' and stktrn_table.ctr_no='" + CounterNo + "' ", con);
                    cmd.Fill(chkDs, "Chk1");



                    int i = 0;
                    for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                    {

                        quantitytype(Adjustnumber);

                        i = i + 1;
                        if (QtyType == 11)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {
                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                    }
                                }
                            }
                        }
                        if (QtyType == 12)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {

                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                    }

                                }
                            }

                        }

                    }
                    con.Close();

                }
                grd_adj_dis.DataSource = dtDisplay;

            }

        }

        public void getgridloadbydate()
        {

            string FromDate = txt_fromdate.Text;
            DateTime stdate = Convert.ToDateTime(FromDate);
            FromsearchDate = stdate.ToString("yyyy-MM-dd");

            string Todate = txt_todate.Text;
            DateTime endDate = Convert.ToDateTime(Todate);
            ToSearchDate = endDate.ToString("yyyy-MM-dd");
            dt1.Clear();
            // dr4.Dispose();
            // SqlDataAdapter cmd1 = new SqlDataAdapter("Select strn_sno from stktrn_table where strn_type='11' or strn_type='12' and strn_date between'" + FromsearchDate + "'  AND  '" + ToSearchDate + "'", con);
            // cmd1.Fill(dt1);

            if (grd_adj_dis.Rows.Count > 0)
            {
                do
                {
                    foreach (DataGridViewRow row in grd_adj_dis.Rows)
                    {
                        try
                        {
                            grd_adj_dis.Rows.Remove(row);
                        }
                        catch (Exception) { }
                    }
                } while (grd_adj_dis.Rows.Count > 0);
            }
            dt1.Clear();

            SqlDataAdapter cmd1 = new SqlDataAdapter("Select strn_sno from stktrn_table where strn_type='11' or strn_type='12' and strn_date between'" + FromsearchDate + "'  AND  '" + ToSearchDate + "'", con);
            cmd1.Fill(dt1);
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                Adjustnumber = dt1.Rows[j][0].ToString();

                chkDs.Tables.Clear();

                SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' and stktrn_table.strn_date between '" + FromsearchDate + "' and '" + ToSearchDate + "' ", con);
                cmd.Fill(chkDs, "Chk1");



                int i = 0;
                for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                {

                    quantitytype(Adjustnumber);

                    i = i + 1;
                    if (QtyType == 11)
                    {
                        if (i == 1)
                        {

                            dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                        }
                        else
                        {
                            string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                            for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                            {
                                quantitytype(temp);
                                if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                {
                                    dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                }
                            }
                        }
                    }
                    if (QtyType == 12)
                    {
                        if (i == 1)
                        {

                            dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                        }
                        else
                        {
                            string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                            for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                            {
                                quantitytype(temp);
                                if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                {

                                    dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                }

                            }
                        }

                    }

                }
                con.Close();

            }
            grd_adj_dis.DataSource = dtDisplay;

        }

        // get by textbox cancelled name:
        public void getbycancelledtext()
        {

            string FromDate = txt_fromdate.Text;
            DateTime stdate = Convert.ToDateTime(FromDate);
            FromsearchDate = stdate.ToString("yyyy-MM-dd");

            string Todate = txt_todate.Text;
            DateTime endDate = Convert.ToDateTime(Todate);
            ToSearchDate = endDate.ToString("yyyy-MM-dd");

            dt1.Clear();
            // dr4.Dispose();
            if (txt_cancel.Text == "All")
            {
                if (grd_adj_dis.Rows.Count > 0)
                {
                    do
                    {
                        foreach (DataGridViewRow row in grd_adj_dis.Rows)
                        {
                            try
                            {
                                grd_adj_dis.Rows.Remove(row);
                            }
                            catch (Exception) { }
                        }
                    } while (grd_adj_dis.Rows.Count > 0);
                }
                dt1.Clear();

                SqlDataAdapter cmd1 = new SqlDataAdapter("Select Distinct(strn_sno) from stktrn_table where strn_type='11' or strn_type='12'", con);
                cmd1.Fill(dt1);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Adjustnumber = dt1.Rows[j][0].ToString();

                    chkDs.Tables.Clear();

                    //SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "'", con);
                    SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no and stktrn_table.strn_date between '"+FromsearchDate+"' and '"+ToSearchDate+"'  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "'", con);
                    cmd.Fill(chkDs, "Chk1");

                    int i = 0;
                    for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                    {

                        quantitytype(Adjustnumber);

                        i = i + 1;
                        if (QtyType == 11)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {
                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                    }
                                }
                            }
                        }
                        if (QtyType == 12)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {

                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                    }

                                }
                            }

                        }

                    }
                    con.Close();

                }
                grd_adj_dis.DataSource = dtDisplay;
                
            }
            else if (txt_cancel.Text == "Cancelled")
            {
                dt1.Clear();
                // dr4.Dispose();

                if (grd_adj_dis.Rows.Count > 0)
                {
                    do
                    {
                        foreach (DataGridViewRow row in grd_adj_dis.Rows)
                        {
                            try
                            {
                                grd_adj_dis.Rows.Remove(row);
                            }
                            catch (Exception) { }
                        }
                    } while (grd_adj_dis.Rows.Count > 0);
                }
                dt1.Clear();

                SqlDataAdapter cmd1 = new SqlDataAdapter("Select Distinct(strn_sno) from stktrn_table where Strn_Cancel='" + true + "'", con);
                cmd1.Fill(dt1);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Adjustnumber = dt1.Rows[j][0].ToString();

                    chkDs.Tables.Clear();

                    SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no and stktrn_table.strn_date between '" + FromsearchDate + "' and '" + ToSearchDate + "'  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' and stktrn_table.Strn_Cancel='" + true + "'", con);
                    cmd.Fill(chkDs, "Chk1");

                    int i = 0;
                    for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                    {

                        quantitytype(Adjustnumber);

                        i = i + 1;
                        if (QtyType == 11)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {
                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                    }
                                }
                            }
                        }
                        if (QtyType == 12)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {

                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                    }

                                }
                            }

                        }

                    }
                    con.Close();

                }
                grd_adj_dis.DataSource = dtDisplay;
             
            }
            else if (txt_cancel.Text == "Not Cancelled")
            {

                dt1.Clear();
                // dr4.Dispose();

                if (grd_adj_dis.Rows.Count > 0)
                {
                    do
                    {
                        foreach (DataGridViewRow row in grd_adj_dis.Rows)
                        {
                            try
                            {
                                grd_adj_dis.Rows.Remove(row);
                            }
                            catch (Exception) { }
                        }
                    } while (grd_adj_dis.Rows.Count > 0);
                }
                dt1.Clear();

                SqlDataAdapter cmd1 = new SqlDataAdapter("Select Distinct(strn_sno) from stktrn_table where Strn_Cancel='" + false + "'", con);
                cmd1.Fill(dt1);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    Adjustnumber = dt1.Rows[j][0].ToString();

                    chkDs.Tables.Clear();

                    SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no and stktrn_table.strn_date between '" + FromsearchDate + "' and '" + ToSearchDate + "'  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' and stktrn_table.Strn_Cancel='" + false + "'", con);
                    cmd.Fill(chkDs, "Chk1");

                    int i = 0;
                    for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                    {

                        quantitytype(Adjustnumber);

                        i = i + 1;
                        if (QtyType == 11)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {
                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                    }
                                }
                            }
                        }
                        if (QtyType == 12)
                        {
                            if (i == 1)
                            {

                                dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                            }
                            else
                            {
                                string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                                for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                                {
                                    quantitytype(temp);
                                    if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                    {

                                        dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                    }

                                }
                            }

                        }

                    }
                    con.Close();

                }
                grd_adj_dis.DataSource = dtDisplay;
                
            }

            else
            {
                MyMessageBox.ShowBox("No Record Found", "Warning");
                do
                {
                    foreach (DataGridViewRow row in grd_adj_dis.Rows)
                    {
                        try
                        {
                            grd_adj_dis.Rows.Remove(row);
                        }
                        catch (Exception) { }
                    }
                } while (grd_adj_dis.Rows.Count > 0);
            }
            grd_adj_dis.DataSource = dtDisplay;

        }



        private void txt_todate_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void lst_itemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txt_countername.Text = lst_ctrname.SelectedItem.ToString();
        }

        private void lst_itemname_Leave(object sender, EventArgs e)
        {

            //txt_item.Focus();
            //txt_countername.Text = lst_ctrname.SelectedItem.ToString();


            ////load item in listbox:
            //lst_item.Focus();
            //ItemNameload();
            //pnl_item.Visible = true;
            //lst_item.Visible = true;

        }

        private void txt_cancel_opt_Enter(object sender, EventArgs e)
        {
            pnl_ctr_name.Visible = false;
            lst_ctrname.Visible = false;
        }

        private void txt_countername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_ctrname.SelectedIndex < lst_ctrname.Items.Count - 1)
                {
                    lst_ctrname.SetSelected(lst_ctrname.SelectedIndex + 1, true);
                }
                txt_countername.Text = lst_ctrname.SelectedItem.ToString();
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lst_ctrname.SelectedIndex > 0)
                {
                    lst_ctrname.SetSelected(lst_ctrname.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnl_ctr_name.Visible = false;
            }


            if (e.KeyCode == Keys.Enter)
            {
                pnl_ctr_name.Visible = false;
                if (txt_countername.Text != "")
                {

                    txt_countername.Text = lst_ctrname.SelectedItem.ToString();
                    FetchByCounterName();
                    pnl_ctr_name.Visible = false;
                    txt_item.Focus();
                }
                else
                {

                    FetchByCounterName();
                    pnl_ctr_name.Visible = false;
                    txt_item.Focus();
                }


            }


            ////if (e.KeyCode == Keys.Enter)
            ////{
            ////    if (txt_countername.Text != "")
            ////    {
            ////        FetchByCounterName();
            ////        //txt_countername.Text = lst_ctrname.SelectedItem.ToString();
            ////    }

            ////        txt_item.Focus();

            ////}

        }

        private void lst_itemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txt_countername.Text  = lst_ctrname.SelectedItem.ToString();
            pnl_ctr_name.Visible = true;
            lst_ctrname.Visible = true;
            //txt_countername.Focus();


        }

        private void txt_countername_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    txt_countername.Focus();
            //}

            //if (e.KeyChar == Convert.ToChar(Keys.Back))
            //{
            //    txt_item.Focus();
            //}
        }

        private void txt_todate_Leave(object sender, EventArgs e)
        {

            //countload();
            //pnl_ctr_name.Visible = true;
            //lst_ctrname.Visible = true;
            //lst_ctrname.Focus();

        }

        private void lst_item_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lst_item_Leave(object sender, EventArgs e)
        {
            //txt_item.Text = lst_item.SelectedItem.ToString();

            //lst_item.Visible = false;
            //pnl_item.Visible = false;


            //lst_cancel.Focus();
        }

        private void txt_item_Leave(object sender, EventArgs e)
        {

            // txt_cancel.Text = lst_cancel.SelectedItem.ToString();
        }



        private void btn_add_Click(object sender, EventArgs e)
        {
            StockAdjustCreate frm = new StockAdjustCreate();
            frm.MdiParent = this.ParentForm;
            frm.StartPosition = FormStartPosition.Manual;
            frm.WindowState = FormWindowState.Normal;
            frm.Location = new Point(0, 80);
            frm.Show();
            this.Close();
        }

        private void grd_adj_dis_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    int t_currentrow = grd_adj_dis.CurrentCell.RowIndex - 1;
            //    //DataGridViewSelectedRowCollection rows = grd_adj_dis.SelectedRows;
            //    //string val =grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["Code"].Value.ToString();

            //    if (grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString() != "")
            //    {
            //        //int t_currentrow = grd_adj_dis.CurrentCell.RowIndex-1;
            //        grd_adj_dis.CurrentCell = grd_adj_dis.Rows[t_currentrow].Cells["code"];

            //        MessageBox.Show(grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString());
            //        //MessageBox.Show(val);
            //        // double id = Convert.ToDouble(val);
            //        //int adj_rec_no = Convert.ToInt16(grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString());

            //        double id = Convert.ToDouble(grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString());
            //        string adj_rec_no = grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString();
            //        chkbox.ID = id;
            //        chkbox.adjrecno = adj_rec_no;
            //        StockAdjustCreate stckalter = new StockAdjustCreate();
            //        this.Hide();
            //        stckalter.Show();
            //    }
            //    else
            //    {

            //        while (grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString() == null)
            //        {
            //            MessageBox.Show("Please Select Valid Cell");
            //        }
            //    }
            //}

        }

        private void grd_adj_dis_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (grd_adj_dis.SelectedRows.Count > 0)
            {
                DataGridViewRow currentRow = grd_adj_dis.SelectedRows[0];
                string id = currentRow.Cells[0].Value.ToString();
                chkbox.ID = id;
                StockAdjustCreate frm = new StockAdjustCreate();
                this.Close();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();

            }
        }

        private void grd_adj_dis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                int t_currentrow = grd_adj_dis.CurrentCell.RowIndex - 1;
                //DataGridViewSelectedRowCollection rows = grd_adj_dis.SelectedRows;
                //string val =grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["Code"].Value.ToString();
                if (t_currentrow >= 0)
                {
                    if (grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString() != "")
                    {

                        //int t_currentrow = grd_adj_dis.CurrentCell.RowIndex-1;
                        grd_adj_dis.CurrentCell = grd_adj_dis.Rows[t_currentrow].Cells["code"];
                        // grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells[grd_adj_dis.CurrentCell.ColumnIndex].Selected = false;

                        ////////  MessageBox.Show(grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString());
                        //MessageBox.Show(val);
                        // double id = Convert.ToDouble(val);
                        //int adj_rec_no = Convert.ToInt16(grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString());

                        string id = grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString();
                        string adj_rec_no = grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString();
                        chkbox.ID = id;
                        chkbox.adjrecno = adj_rec_no;
                        StockAdjustmentAlteration frm = new StockAdjustmentAlteration();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        // this.Close();
                        this.SendToBack();
                        frm.Show();

                    }
                    else if (grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString() == "")
                    {
                        //    if (t_currentrow > 1)
                        //    {
                        for (int itemfinder = t_currentrow; t_currentrow >= itemfinder; itemfinder--)
                        {
                            if (grd_adj_dis.Rows[itemfinder].Cells["AdjNo"].Value.ToString() != "")
                            {
                                grd_adj_dis.CurrentCell = grd_adj_dis.Rows[itemfinder].Cells["code"];
                                // grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells[grd_adj_dis.CurrentCell.ColumnIndex].Selected = false;
                                /////    MessageBox.Show(grd_adj_dis.Rows[itemfinder].Cells["Code"].Value.ToString());
                                //MessageBox.Show(val);
                                // double id = Convert.ToDouble(val);
                                //int adj_rec_no = Convert.ToInt16(grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString());

                                string id = grd_adj_dis.Rows[itemfinder].Cells["Code"].Value.ToString();
                                string adj_rec_no = grd_adj_dis.Rows[itemfinder].Cells["AdjNo"].Value.ToString();
                                chkbox.ID = id;
                                chkbox.adjrecno = adj_rec_no;
                                StockAdjustmentAlteration frm = new StockAdjustmentAlteration();
                                //this.Close();
                                this.SendToBack();
                                frm.MdiParent = this.ParentForm;
                                frm.StartPosition = FormStartPosition.Manual;
                                frm.WindowState = FormWindowState.Normal;
                                frm.Location = new Point(0, 80);
                                frm.Show();
                                if (itemfinder == 0 || adj_rec_no != "")
                                {
                                    break;
                                }
                                // }
                            }
                        }
                    }
                }
            }
        }

        private void txt_cancel_Leave(object sender, EventArgs e)
        {
            //pnl_Cancel.Visible = false;
            //lst_cancel.Visible = false;
            //gridload();
        }

        private void txt_cancel_KeyPress(object sender, KeyPressEventArgs e)
        {

        }



        private void txt_cancel_KeyDown(object sender, KeyEventArgs e)
        {
            // lst_cancel.Focus();

            if (e.KeyCode == Keys.Down)
            {
                if (lst_cancel.SelectedIndex < lst_cancel.Items.Count - 1)
                {
                    lst_cancel.SetSelected(lst_cancel.SelectedIndex + 1, true);
                }
                txt_cancel.Text = lst_cancel.SelectedItem.ToString();
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lst_cancel.SelectedIndex > 0)
                {
                    lst_cancel.SetSelected(lst_cancel.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnl_Cancel.Visible = false;
            }


            if (e.KeyCode == Keys.Enter)
            {
                pnl_Cancel.Visible = false;
                if (txt_cancel.Text != "")
                {

                    //txt_cancel.Text = lst_ctrname.SelectedItem.ToString();
                    getbycancelledtext();
                    pnl_Cancel.Visible = false;
                    //grd_adj_dis.Focus();

                }
                else
                {

                    getbycancelledtext();
                    pnl_Cancel.Visible = false;
                    //grd_adj_dis.Focus();
                }


            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    if(txt_cancel.Text!="")
            //    {
            //        getbycancelledtext();
            //    }
            //    grd_adj_dis.Focus();
            //}
        }

        private void lst_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_cancel.Text = lst_cancel.SelectedItem.ToString();
            //txt_cancel.Focus();
        }

        private void lst_cancel_Leave(object sender, EventArgs e)
        {
            //txt_cancel.Text = lst_cancel.SelectedItem.ToString();
            //gridload();
            grd_adj_dis.Focus();
            // grd_adj_dis.CurrentCell = grd_adj_dis.Rows[0].Cells[0];

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            int canpurqty;
            int t_currentrow = grd_adj_dis.CurrentCell.RowIndex;

            // MessageBox.Show(grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString());
            string itemcode = grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString();
            string adjno = grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString();

            // cancelling updation process : for n_purqty-add_nt_qty;
            var temp = grd_adj_dis.Rows[t_currentrow].Cells["Add_Qty"].Value.ToString();
            if (temp == " ")
            {
                canpurqty = 0;
            }
            else
            {
                canpurqty = Convert.ToInt16(grd_adj_dis.Rows[t_currentrow].Cells["Add_Qty"].Value);
            }
            con.Open();
            string pur_qty = "select nt_purqty from Item_table where Item_code='" + itemcode + "'";
            SqlCommand nt_purqty = new SqlCommand(pur_qty, con);
            string NtPurQty = nt_purqty.ExecuteScalar().ToString();
            con.Close();

            //check for already cancel:
            con.Open();
            string already = "select Cancel from adjmas_table where Adj_Billno='" + adjno + "'";
            SqlCommand supdat = new SqlCommand(already, con);
            bool checkcl = Convert.ToBoolean(supdat.ExecuteScalar());
            con.Close();

            //get a StrnNo based AdjBillNO on Adjmas_table:
            con.Open();
            string Adjmas_noqry = "select Adj_No from adjmas_table where Adj_Billno='" + adjno + "'";
            SqlCommand cmdstktrn = new SqlCommand(Adjmas_noqry, con);
            string Adjmasno = cmdstktrn.ExecuteScalar().ToString();
            con.Close();

            if (MessageBox.Show("Are You sure you want to Cancel the Item", "Warning Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (checkcl == false)
                {
                    // updation for cancel the adjustment in Adjtable:
                    con.Open();
                    string updatesales = "update adjmas_table set Cancel='" + true + "' where Adj_Billno='" + adjno + "'";
                    SqlCommand canupdat = new SqlCommand(updatesales, con);
                    canupdat.ExecuteNonQuery();
                    con.Close();

                    // updation for cancel the adjustment in Stockadjmas_table: 
                    con.Open();
                    string canadjqry = "update stktrn_table set Strn_Cancel='" + true + "' where strn_no='" + Adjmasno + "'";
                    SqlCommand canadj = new SqlCommand(canadjqry, con);
                    canadj.ExecuteNonQuery();
                    con.Close();

                    gridload();

                    if (grd_adj_dis.Rows[t_currentrow].Cells["Less_Qty"].Value == "")
                    {
                        //cancel the less qty into item table:
                        int canAddqty = Convert.ToInt16(grd_adj_dis.Rows[t_currentrow].Cells["Add_Qty"].Value.ToString());
                        con.Close();
                        con.Open();
                        string qry_oldadd_qty = "select nt_purqty from Item_table where Item_code='" + itemcode + "'";
                        SqlCommand cmd_oldadd_qty = new SqlCommand(qry_oldadd_qty, con);
                        int oldpurqty = Convert.ToInt32(cmd_oldadd_qty.ExecuteScalar().ToString());
                        con.Close();

                        int updatepurqty = oldpurqty - canAddqty;


                        //cancel the less qty closeqty into item table:


                        con.Open();
                        string qry_oldclo_qty = "select nt_cloqty from Item_table where Item_code='" + itemcode + "'";
                        SqlCommand cmd_oldcls_qty = new SqlCommand(qry_oldclo_qty, con);
                        int oldclsqty = Convert.ToInt32(cmd_oldcls_qty.ExecuteScalar().ToString());
                        con.Close();

                        int updatecloqty = oldclsqty - canAddqty;

                        //cancel the less qty salesval into item table:

                        double canPurval = Convert.ToDouble(grd_adj_dis.Rows[t_currentrow].Cells["AddAmount"].Value.ToString());
                        con.Open();
                        string qry_oldPurval = "select Nt_PurVal from Item_table where Item_code='" + itemcode + "'";
                        SqlCommand cmd_oldPurval = new SqlCommand(qry_oldPurval, con);
                        double oldPurval = Convert.ToDouble(cmd_oldPurval.ExecuteScalar().ToString());
                        con.Close();

                        double updatePurval = oldPurval - canPurval;

                        //updates sales qty  lessqty,closqty,salval:
                        con.Open();
                        string updatePurqty = "update Item_table set nt_purqty=" + updatepurqty + ",nt_cloqty=" + updatecloqty + ",Nt_PurVal=" + updatePurval + " where Item_code='" + itemcode + "'";
                        SqlCommand cmdupdatPurqty = new SqlCommand(updatePurqty, con);
                        cmdupdatPurqty.ExecuteNonQuery();
                        con.Close();
                    }
                    if (grd_adj_dis.Rows[t_currentrow].Cells["Add_Qty"].Value.ToString() == " ")
                    {
                        //cancel the less qty into item table:
                        int canlessqty = Convert.ToInt32(grd_adj_dis.Rows[t_currentrow].Cells["Less_Qty"].Value.ToString());
                        con.Close();
                        con.Open();
                        string qry_oldless_qty = "select nt_salqty from Item_table where Item_code='" + itemcode + "'";
                        SqlCommand cmd_oldless_qty = new SqlCommand(qry_oldless_qty, con);
                        int oldsalqty = Convert.ToInt32(cmd_oldless_qty.ExecuteScalar().ToString());
                        con.Close();

                        int updatesalqty = oldsalqty - canlessqty;


                        //cancel the less qty closeqty into item table:


                        con.Open();
                        string qry_oldclo_qty = "select nt_cloqty from Item_table where Item_code='" + itemcode + "'";
                        SqlCommand cmd_oldcls_qty = new SqlCommand(qry_oldclo_qty, con);
                        int oldclsqty = Convert.ToInt32(cmd_oldcls_qty.ExecuteScalar().ToString());
                        con.Close();

                        int updatecloqty = oldclsqty + canlessqty;

                        //cancel the less qty salesval into item table:

                        Double cansalval = Convert.ToDouble(grd_adj_dis.Rows[t_currentrow].Cells["LessAmount"].Value.ToString());
                        con.Open();
                        string qry_oldsalval = "select Nt_Salval from Item_table where Item_code='" + itemcode + "'";
                        SqlCommand cmd_oldsalval = new SqlCommand(qry_oldsalval, con);
                        int oldsalval = Convert.ToInt32(cmd_oldsalval.ExecuteScalar().ToString());
                        con.Close();

                        Double updatesalval = oldsalval - cansalval;

                        //updates sales qty  lessqty,closqty,salval:
                        con.Open();
                        string updatesalesqty = "update Item_table set nt_salqty=" + updatesalqty + ",nt_cloqty=" + updatecloqty + ",Nt_Salval=" + updatesalval + " where Item_code='" + itemcode + "'";
                        SqlCommand cmdupdatsalesqty = new SqlCommand(updatesalesqty, con);
                        cmdupdatsalesqty.ExecuteNonQuery();
                        con.Close();
                    }
                }

                else
                {
                    string CancelMessage = MyMessageBox.ShowBox("Already Records in Cancel ", "Warning!");
                }
            }

        }

        private void grd_adj_dis_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //int t_currentrow = grd_adj_dis.CurrentCell.RowIndex;
            //int adjno = Convert.ToInt32(grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString());
            //foreach (DataGridViewRow row in grd_adj_dis.Rows)
            //{
            //    con.Close();
            //    con.Open();
            //    SqlCommand selcan = new SqlCommand("select Cancel from adjmas_table where adj_no=" + adjno + " ", con);
            //    bool selcanid = Convert.ToBoolean(selcan.ExecuteScalar());
            //    con.Close();
            //    ////if (row.Cells["Cancel"].Value.ToString() == "True") //**Object reference not set to an instance of an object**
            //    ////{
            //    if (selcanid == true) //**Object reference not set to an instance of an object**
            //    {
            //        //string CNumColour = grd_adj_dis.CurrentRow.Cells[0].FormattedValue.ToString();

            //        foreach (DataGridViewCell cells in row.Cells)
            //        {
            //            if (grd_adj_dis.CurrentCell.ColumnIndex == 0)
            //            {
            //                cells.Style.ForeColor = Color.Red;

            //            }
            //        }
            //        //row.DefaultCellStyle.ForeColor = Color.Red;  //then change row color to red
            //    }
            //}
        }

        private void lst_cancel_KeyPress(object sender, KeyPressEventArgs e)
        {

            txt_cancel.Focus();
            pnl_Cancel.Visible = true;
            lst_cancel.Visible = true;
            if (lst_cancel.SelectedItem != null)
            {
                txt_cancel.Text = lst_cancel.SelectedItem.ToString();
            }

            pnl_item.Visible = false;
            lst_item.Visible = false;
            grd_adj_dis.Focus();
            grd_adj_dis.CurrentCell = grd_adj_dis.Rows[0].Cells[0];

        }

        private void lst_item_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_item.Text = lst_item.SelectedItem.ToString();

            pnl_item.Visible = true;
            lst_item.Visible = true;

            pnl_Cancel.Visible = true;
            lst_cancel.Visible = true;

            lst_cancel.Focus();

        }

        private void lst_item_Enter(object sender, EventArgs e)
        {

        }

        private void lst_cancel_Enter(object sender, EventArgs e)
        {
            pnl_item.Visible = false;
            lst_item.Visible = false;

        }

        private void grd_adj_dis_Enter(object sender, EventArgs e)
        {
            pnl_Cancel.Visible = false;
            lst_cancel.Visible = false;
        }

        private void txt_countername_MouseClick(object sender, MouseEventArgs e)
        {
            countload();
            pnl_ctr_name.Visible = true;
            lst_ctrname.Visible = true;
        }

        private void lst_ctrname_MouseClick(object sender, MouseEventArgs e)
        {
            txt_countername.Text = lst_ctrname.SelectedItem.ToString();
            pnl_ctr_name.Visible = false;
        }

        private void lst_cancel_MouseClick(object sender, MouseEventArgs e)
        {

            txt_cancel.Text = lst_cancel.SelectedItem.ToString();
            pnl_Cancel.Visible = false;
            if (txt_cancel.Text != "")
            {

                //txt_cancel.Text = lst_ctrname.SelectedItem.ToString();
                getbycancelledtext();
                pnl_Cancel.Visible = false;
                //grd_adj_dis.Focus();

            }
            else
            {

                getbycancelledtext();
                pnl_Cancel.Visible = false;
                //grd_adj_dis.Focus();
            }
        }

        private void lst_item_MouseClick(object sender, MouseEventArgs e)
        {
            txt_item.Text = lst_item.SelectedItem.ToString();
        }

        private void txt_item_MouseClick(object sender, MouseEventArgs e)
        {
            ItemNameload();
            pnl_item.Visible = true;
            lst_item.Visible = true;

        }

        private void txt_cancel_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Cancel.Visible = true;
            lst_cancel.Visible = true;

        }

        private void grd_adj_dis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SqlDataReader dreadr3;
        string chk;
        private void txt_countername_TextChanged(object sender, EventArgs e)
        {
            if (txt_countername.Text.Trim() == null && txt_countername.Text.Trim() == "")
            {
                countload();
                pnl_ctr_name.Visible = true;
                lst_ctrname.Visible = true;
                if (txt_countername.Text.Trim() != null && txt_countername.Text.Trim() != "")
                {
                    //pnl_ctr_name.Visible = true;

                    SqlCommand cmd = new SqlCommand("select ctr_name from counter_table where ctr_name like '" + txt_countername.Text.Trim() + "%'", con);
                    con.Close();
                    con.Open();
                    dreadr3 = cmd.ExecuteReader();
                    bool isChk = false;
                    while (dreadr3.Read())
                    {
                        isChk = true;
                        string tempStr = dreadr3["ctr_name"].ToString();
                        for (int i = 0; i < lst_ctrname.Items.Count; i++)
                        {
                            if (dreadr3["ctr_name"].ToString() == lst_ctrname.Items[i].ToString())
                            {
                                lst_ctrname.SetSelected(i, true);
                                txt_countername.Select();
                                chk = "1";
                                txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                                break;
                            }
                        }
                        break;
                    }
                    con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    }
                }
                else
                {
                    chk = "1";
                }





                //AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
                //con.Close();
                //con.Open();
                //SqlCommand cmd = new SqlCommand("select ctr_name from counter_table", con);

                //SqlDataReader dReader;
                //dReader = cmd.ExecuteReader();

                //if (dReader.Read())
                //{
                //    while (dReader.Read())
                //        namesCollection.Add(dReader["ctr_name"].ToString());
                //}
                //else
                //{
                //    MessageBox.Show("Data not found");
                //}
                //dReader.Close();

                //txt_countername.AutoCompleteMode = AutoCompleteMode.Suggest;
                //txt_countername.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txt_countername.AutoCompleteCustomSource = namesCollection;
                //con.Close();
            }
        }

        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;

                    // chk = "1";

                }
                else
                {
                    e.Handled = false;
                }
            }

        }
        private void txt_item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_item.SelectedIndex < lst_item.Items.Count - 1)
                {
                    lst_item.SetSelected(lst_item.SelectedIndex + 1, true);
                }
                txt_item.Text = lst_item.SelectedItem.ToString();
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lst_item.SelectedIndex > 0)
                {
                    lst_item.SetSelected(lst_item.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnl_item.Visible = false;
            }


            if (e.KeyCode == Keys.Enter)
            {
                pnl_item.Visible = false;
                if (txt_item.Text != "")
                {

                    txt_item.Text = lst_item.SelectedItem.ToString();
                    FetchByItemName();
                    pnl_item.Visible = false;
                    txt_cancel.Focus();
                }
                else
                {

                    FetchByItemName();
                    pnl_item.Visible = false;
                    txt_cancel.Focus();
                }


            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (txt_item.Text != "")
            //    {
            //        FetchByItemName();
            //    }

            //        txt_cancel.Focus();

            //}

        }
        public void FetchByItemName()
        {
            string FromDate = txt_fromdate.Text;
            DateTime stdate = Convert.ToDateTime(FromDate);
            FromsearchDate = stdate.ToString("yyyy-MM-dd");

            string Todate = txt_fromdate.Text;
            DateTime endDate = Convert.ToDateTime(Todate);
            ToSearchDate = endDate.ToString("yyyy-MM-dd");
            string ItemNumber;
            con.Close();
            con.Open();
            string Itemnameqry = "select Item_no from Item_table where Item_name='" + txt_item.Text + "'";
            SqlCommand cmdItemName = new SqlCommand(Itemnameqry, con);
            var Temp = cmdItemName.ExecuteScalar();
            if (Temp == null)
            {
                ItemNumber = "";
            }
            else
            {
                ItemNumber = cmdItemName.ExecuteScalar().ToString();
            }

            if (grd_adj_dis.Rows.Count > 0)
            {
                do
                {
                    foreach (DataGridViewRow row in grd_adj_dis.Rows)
                    {
                        try
                        {
                            grd_adj_dis.Rows.Remove(row);
                        }
                        catch (Exception) { }
                    }
                } while (grd_adj_dis.Rows.Count > 0);
            }
            dt1.Clear();

            SqlDataAdapter cmd1 = new SqlDataAdapter("Select strn_sno from stktrn_table where strn_type='11' or strn_type='12' and item_no ='" + ItemNumber + "'", con);
            cmd1.Fill(dt1);
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                Adjustnumber = dt1.Rows[j][0].ToString();

                chkDs.Tables.Clear();

                SqlDataAdapter cmd = new SqlDataAdapter("Select adjmas_table.Adj_Bill as AdjNo,stktrn_table.strn_date as Date,Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Add_Qty,stktrn_table.Rate as Rate, stktrn_table.Amount as Less_Amount,stktrn_table.Amount as Add_Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_sno='" + dt1.Rows[j][0].ToString() + "' and stktrn_table.item_no= '" + ItemNumber + "' ", con);
                cmd.Fill(chkDs, "Chk1");



                int i = 0;
                for (int i1 = 0; i1 < chkDs.Tables["Chk1"].Rows.Count; i1++)
                {

                    quantitytype(Adjustnumber);

                    i = i + 1;
                    if (QtyType == 11)
                    {
                        if (i == 1)
                        {

                            dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                        }
                        else
                        {
                            string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                            for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                            {
                                quantitytype(temp);
                                if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                {
                                    dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), chkDs.Tables["Chk1"].Rows[i1][5].ToString(), " ", chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());
                                }
                            }
                        }
                    }
                    if (QtyType == 12)
                    {
                        if (i == 1)
                        {

                            dtDisplay.Rows.Add(chkDs.Tables["Chk1"].Rows[i1][0].ToString(), chkDs.Tables["Chk1"].Rows[i1][1].ToString().Substring(0, 10), chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), chkDs.Tables["Chk1"].Rows[i1][8].ToString(), "");
                        }
                        else
                        {
                            string temp = chkDs.Tables["Chk1"].Rows[i1][0].ToString();

                            for (int i2 = 0; i2 < dtDisplay.Rows.Count; i2++)
                            {
                                quantitytype(temp);
                                if (temp == dtDisplay.Rows[i2]["Adjno"].ToString())
                                {

                                    dtDisplay.Rows.Add("", "", chkDs.Tables["Chk1"].Rows[i1][2].ToString(), chkDs.Tables["Chk1"].Rows[i1][3].ToString(), chkDs.Tables["Chk1"].Rows[i1][4].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][5].ToString(), chkDs.Tables["Chk1"].Rows[i1][7].ToString(), "", chkDs.Tables["Chk1"].Rows[i1][8].ToString());

                                }

                            }
                        }

                    }

                }
                con.Close();

            }
            grd_adj_dis.DataSource = dtDisplay;


        }

        SqlDataReader dreadr4;
        string chk1;
        private void txt_item_TextChanged(object sender, EventArgs e)
        {
            ItemNameload();
            pnl_item.Visible = true;
            lst_item.Visible = true;
            if (txt_item.Text.Trim() != null && txt_item.Text.Trim() != "")
            {
                // pnl_item.Visible = true;

                SqlCommand cmd = new SqlCommand("select Item_name from Item_table where Item_name like '" + txt_item.Text.Trim() + "%'", con);
                con.Close();
                con.Open();
                dreadr4 = cmd.ExecuteReader();
                bool isChk = false;
                while (dreadr4.Read())
                {
                    isChk = true;
                    string tempStr = dreadr4["Item_name"].ToString();
                    for (int i = 0; i < lst_item.Items.Count; i++)
                    {
                        if (dreadr4["Item_name"].ToString() == lst_item.Items[i].ToString())
                        {
                            lst_item.SetSelected(i, true);
                            txt_item.Select();
                            chk1 = "1";
                            txt_item.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            break;
                        }
                    }
                    break;
                }
                con.Close();
                if (isChk == false)
                {
                    chk1 = "2";
                    txt_item.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                }
            }
            else
            {
                chk1 = "1";
            }

            //AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            //con.Close();
            //con.Open();
            //SqlCommand cmd = new SqlCommand("select Item_name from Item_table order by Item_name ASC", con);

            //SqlDataReader dReader;
            //dReader = cmd.ExecuteReader();

            //if (dReader.Read())
            //{
            //    while (dReader.Read())
            //        namesCollection.Add(dReader["Item_name"].ToString());
            //}
            //else
            //{
            //    MessageBox.Show("Data not found");
            //}
            //dReader.Close();

            //txt_item.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txt_item.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txt_item.AutoCompleteCustomSource = namesCollection;
            //con.Close();
        }
        string chkStr1, chkstr2;
        private void txt_cancel_TextChanged(object sender, EventArgs e)
        {
            if (txt_cancel.Text.Trim() != null && txt_cancel.Text.Trim() != "")
            {

                for (int i = 0; i < lst_cancel.Items.Count; i++)
                {
                    chkStr1 = lst_cancel.Items[i].ToString();
                    if (txt_cancel.Text.Length <= chkStr1.Length)
                    {
                        chkstr2 = chkStr1.Substring(0, txt_cancel.Text.Length);
                        bool isChk = false;
                        if (txt_cancel.Text.Trim() == chkstr2 || txt_cancel.Text.Trim() == chkstr2.ToLower())
                        {
                            isChk = true;
                            lst_cancel.SetSelected(i, true);
                            txt_cancel.Select();
                            chk = "1";
                            txt_cancel.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);

                            break;
                        }
                        if (isChk == false)
                        {
                            chk = "2";
                            txt_cancel.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                        }
                    }


                }

            }
            else
            {
                chk = "1";
            }


            //AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            //string[] array={"ALL","Cancelled","Not Cancelled"};
            //txt_cancel.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txt_cancel.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txt_cancel.AutoCompleteCustomSource.AddRange(array);
        }

        private void grd_adj_dis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int t_currentrow = e.RowIndex;
            //DataGridViewSelectedRowCollection rows = grd_adj_dis.SelectedRows;
            //string val =grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["Code"].Value.ToString();

            if (grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString() != "")
            {

                //int t_currentrow = grd_adj_dis.CurrentCell.RowIndex-1;
                grd_adj_dis.CurrentCell = grd_adj_dis.Rows[t_currentrow].Cells["code"];
                // grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells[grd_adj_dis.CurrentCell.ColumnIndex].Selected = false;

                ////////  MessageBox.Show(grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString());
                //MessageBox.Show(val);
                // double id = Convert.ToDouble(val);
                //int adj_rec_no = Convert.ToInt16(grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString());

                string id = grd_adj_dis.Rows[t_currentrow].Cells["Code"].Value.ToString();
                string adj_rec_no = grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString();

                chkbox.ID = id;
                chkbox.adjrecno = adj_rec_no;
                StockAdjustmentAlteration frm = new StockAdjustmentAlteration();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                // this.Close();
                this.SendToBack();
                frm.Show();

            }
            else if (grd_adj_dis.Rows[t_currentrow].Cells["AdjNo"].Value.ToString() == "")
            {
                //    if (t_currentrow > 1)
                //    {
                for (int itemfinder = t_currentrow; t_currentrow >= itemfinder; itemfinder--)
                {
                    if (grd_adj_dis.Rows[itemfinder].Cells["AdjNo"].Value.ToString() != "")
                    {
                        grd_adj_dis.CurrentCell = grd_adj_dis.Rows[itemfinder].Cells["code"];
                        // grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells[grd_adj_dis.CurrentCell.ColumnIndex].Selected = false;
                        /////    MessageBox.Show(grd_adj_dis.Rows[itemfinder].Cells["Code"].Value.ToString());
                        //MessageBox.Show(val);
                        // double id = Convert.ToDouble(val);
                        //int adj_rec_no = Convert.ToInt16(grd_adj_dis.Rows[grd_adj_dis.CurrentCell.RowIndex].Cells["AdjNo"].Value.ToString());

                        string id = grd_adj_dis.Rows[itemfinder].Cells["Code"].Value.ToString();
                        string adj_rec_no = grd_adj_dis.Rows[itemfinder].Cells["AdjNo"].Value.ToString();
                        chkbox.ID = id;
                        chkbox.adjrecno = adj_rec_no;
                        StockAdjustmentAlteration frm = new StockAdjustmentAlteration();
                        // this.Close();
                        this.SendToBack();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        if (itemfinder == 0 || adj_rec_no != "")
                        {
                            break;
                        }

                        // }
                    }
                }
            }

        }

        private void txt_item_Enter(object sender, EventArgs e)
        {
            // txt_item.Text = "";
            pnl_ctr_name.Visible = false;
            pnl_Cancel.Visible = false;
            // txt_item.SelectAll();
            //ItemNameload();
            //pnl_item.Visible = true;
            //lst_item.Visible = true;

        }

        private void txt_cancel_Enter(object sender, EventArgs e)
        {
            txt_cancel.Text = "";
            pnl_ctr_name.Visible = false;
            pnl_item.Visible = false;
            txt_cancel.SelectAll();
            pnl_Cancel.Visible = true;
            lst_cancel.Visible = true;
            lst_cancel.SelectedIndex = 0;
            txt_cancel.Text = lst_cancel.SelectedItem.ToString();

        }

        private void txt_todate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                getgridloadbydate();
                //txt_countername.Focus();
            }


            if (e.KeyCode == Keys.Back)
            {

                txt_todate.Focus();

            }
        }

        private void grd_adj_dis_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //this.grd_adj_dis.RowsDefaultCellStyle.BackColor = Color.Bisque;
            //this.grd_adj_dis.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
        }

        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceSales = new Microsoft.Reporting.WinForms.ReportDataSource();

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewerSales.Reset();
                Dataset.DsBomissueDisplayMaster dsobj = new Dataset.DsBomissueDisplayMaster();
                for (int k = 0; k < grd_adj_dis.Rows.Count; k++)
                {
                    dsobj.Tables["SalesAdjusmentDispalay"].Rows.Add(grd_adj_dis.Rows[k].Cells[0].Value.ToString(), grd_adj_dis.Rows[k].Cells[1].Value.ToString(), grd_adj_dis.Rows[k].Cells[2].Value.ToString(), grd_adj_dis.Rows[k].Cells[3].Value.ToString(), grd_adj_dis.Rows[k].Cells[4].Value.ToString(), grd_adj_dis.Rows[k].Cells[5].Value.ToString(), grd_adj_dis.Rows[k].Cells[6].Value.ToString(), grd_adj_dis.Rows[k].Cells[7].Value.ToString(), grd_adj_dis.Rows[k].Cells[8].Value.ToString(), grd_adj_dis.Rows[k].Cells[9].Value.ToString());
                }
                ReportDataSource ds = new ReportDataSource("DsBomissueDisplayMaster", dsobj.Tables["SalesAdjusmentDispalay"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RdlcAdjDisplay.rdlc";
                //Passing Parmetes:
                string DateF = (Convert.ToString(txt_fromdate.Value.Day) + "/" + (txt_fromdate.Value.Month) + "/" + (txt_fromdate.Value.Year));
                string DateFT = (Convert.ToString(txt_todate.Value.Day) + "/" + (txt_todate.Value.Month) + "/" + (txt_todate.Value.Year));
                ReportParameter rp = new ReportParameter("CounterName", txt_countername.Text, false);
                ReportParameter FDate = new ReportParameter("FDate", DateF, false);
                ReportParameter ToDate = new ReportParameter("ToDate", DateFT, false);
                ReportParameter Cancel = new ReportParameter("Cancel", txt_cancel.Text.Trim(), false);
                ReportParameter Type = new ReportParameter("ItemName", txt_item.Text.Trim(), false);
                //ReportParameter rp2 = new ReportParameter("DateTo", "300");

                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rp, FDate, ToDate, Cancel, Type });
                dt.EndInit();
                reportViewerSales.RefreshReport();
                reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
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
            }
        }

        private void txt_countername_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_ctr_name.Visible = true;
                lst_ctrname.Visible = true;
                countload();
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_countername_Click_1(object sender, EventArgs e)
        {

        }

    }
}


