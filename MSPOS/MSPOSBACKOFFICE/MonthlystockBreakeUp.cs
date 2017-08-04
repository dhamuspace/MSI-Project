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


namespace MSPOSBACKOFFICE
{
    public partial class MonthlystockBreakeUp : Form
    {
        public string item_code;
        string id_numbername = "0";
        int value_number2 = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public MonthlystockBreakeUp()
        {

            InitializeComponent();
            foreach (DataGridViewColumn col in DgMonthlyStockRpt.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            pnlitems.Visible = false;
            DgMonthlyStockRpt.Rows.Add(13);
            DgMonthlyStockRpt.Rows[0].HeaderCell.Value = "Opening";
            DgMonthlyStockRpt.Rows[0].InheritedStyle.BackColor = Color.Yellow;
            //dataGridView2.Rows[0].HeaderCell.FormattedValue.Equals(ForeColor.IsKnownColor.Equals(Color.Blue));
            //dataGridView2.Rows[0].HeaderCell.ToString(ForeColor.Equals(Color.Brown));
            DgMonthlyStockRpt.Rows[1].HeaderCell.Value = "January";
            DgMonthlyStockRpt.Rows[2].HeaderCell.Value = "February ";
            DgMonthlyStockRpt.Rows[3].HeaderCell.Value = "March";
            DgMonthlyStockRpt.Rows[4].HeaderCell.Value = "Aprial ";
            DgMonthlyStockRpt.Rows[5].HeaderCell.Value = "May ";
            DgMonthlyStockRpt.Rows[6].HeaderCell.Value = "June";
            DgMonthlyStockRpt.Rows[7].HeaderCell.Value = "Jully";
            DgMonthlyStockRpt.Rows[8].HeaderCell.Value = "August";
            DgMonthlyStockRpt.Rows[9].HeaderCell.Value = "September";
            DgMonthlyStockRpt.Rows[10].HeaderCell.Value = "October ";
            DgMonthlyStockRpt.Rows[11].HeaderCell.Value = "November ";
            DgMonthlyStockRpt.Rows[12].HeaderCell.Value = "December";
            DgMonthlyStockRpt.Rows[13].HeaderCell.Value = "Total:";
            DgMonthlyStockRpt.Rows[13].DefaultCellStyle.BackColor = Color.Yellow;
            DateTime fromdate =new DateTime( );
            fromdate = Convert.ToDateTime(passingvalues.tStartDateParthi.Year+"/"+passingvalues.tStartDateParthi.Month+"/"+passingvalues.tStartDateParthi.Day);
            DateTime enddate =new DateTime ();
            enddate = Convert.ToDateTime(passingvalues.tToDateParthi.Year + "/" + passingvalues.tToDateParthi.Month + "/" + passingvalues.tToDateParthi.Day);
            string id_number = passingvalues.str.ToString();
           // SqlCommand cmd = new SqlCommand("select * from item_table where item_code='" + id_number + "'", con);
            SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ActionType","ItemCode");
                cmd.Parameters.AddWithValue("@ItemCode", id_number);
            cmd.Parameters.AddWithValue("@itemName","");
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                item_code = dt.Rows[0]["Item_no"].ToString();
            }
            string value_number = passingvalues.vaues.ToString();
            value_number2=Convert.ToInt32( passingvalues.vaues.ToString());
            txtitemname.Text = passingvalues.item_name.ToString();
            if (value_number == "1")
            {
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[1].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[1].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[1].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[1].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[1].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "2")
            {
                //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {


                            DgMonthlyStockRpt.Rows[2].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[2].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[2].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[2].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[2].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[2].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "3")
            {
                //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {

                            DgMonthlyStockRpt.Rows[3].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[3].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[3].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[3].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[3].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[3].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "4")
            {
                //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[4].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[4].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[4].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[4].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[4].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[4].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "5")
            {
               // SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[5].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[5].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[5].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[5].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[5].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[5].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "6")
            {
                //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[6].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[6].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[6].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[6].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[6].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[6].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "7")
            {
                //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + id_number.ToString() + "'", con);
              //  SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[7].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[7].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[7].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[7].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[7].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[7].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "8")
            {
               // SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[8].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[8].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[8].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[8].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[8].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[8].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "9")
            {
               // SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[0].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[9].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[9].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[9].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[9].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[9].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[9].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "10")
            {
           //     SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[0].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[10].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[10].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[10].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[10].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[10].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                            double tot = 0.00;
                            tot = Convert.ToDouble(name * amount);
                            DgMonthlyStockRpt.Rows[10].Cells["tot"].Value = tot.ToString();
                        }
                    }
                }
            }
            if (value_number == "11")
            {
              //  SqlDataAdapter adp1 = new SqlDataAdapter("select distinct item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "'  ", con);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                        string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                        if (strn_typeno == "0" && strn_type == "0")
                        {
                            DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                            DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                            double itemcost = 0.00;
                            itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                            double itemqty = 0.00;
                            itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                            itemcost = Convert.ToDouble(itemqty * itemcost);
                            DgMonthlyStockRpt.Rows[0].Cells["tot"].Value = itemcost.ToString();
                        }
                        else
                        {
                            DgMonthlyStockRpt.Rows[11].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[11].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                            DgMonthlyStockRpt.Rows[11].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                            DgMonthlyStockRpt.Rows[11].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                            DgMonthlyStockRpt.Rows[11].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                            double name = 0.00;
                            name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                            double amount = 0.00;
                            if (dt.Rows[0]["nt_cloqty"].ToString() != "")
                            {
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[11].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                }
            }
            if (value_number == "12")
            {

                //DateTime cDate = DateTime.Parse(fromdate);
                //int day = cDate.Day;
                //int month = cDate.Month;
                //int year = cDate.Year;

                //string dateAdded = year + "-" + month + "-" + day + " ";

                //DateTime ctodate = DateTime.Parse(enddate);

                //int year2 = ctodate.Year;
                //int month2 = ctodate.Month;
                //int day2 = ctodate.Day;

                //string dateAdded2 = year2 + "-" + month2 + "-" + day2 + " ";
               // SqlCommand cmdvalue = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
               // SqlDataAdapter adp1 = new SqlDataAdapter(cmdvalue);
                SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    try
                    {

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[0].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[12].Cells["nq_purqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_purval"].Value = dt1.Rows[0]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_prqty"].Value = dt1.Rows[0]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_purRetval"].Value = dt1.Rows[0]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_cloqty"].Value = dt1.Rows[0]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[12].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    catch
                    { }
                }
            }
        }
        private void MonthlystockBreakeUp_Load(object sender, EventArgs e)
        {
            datagridview_calculation();
            DgMonthlyStockRpt.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            
        }
        public void datagridview_calculation()
        {
            if (con.State != ConnectionState.Open)
            {
            }
            double qty = 0.00;
                //, val = 0.00, issue_qty = 0.00, issuse_value = 0.00, closqty = 0.00, closval;
            if (DgMonthlyStockRpt.Rows.Count > 0)
            {
                DgMonthlyStockRpt.Rows[13].Cells["nq_purqty"].Value = "";
                DgMonthlyStockRpt.Rows[13].Cells["nt_purval"].Value = "0.00";
                DgMonthlyStockRpt.Rows[13].Cells["nt_prqty"].Value = "0.00";
                DgMonthlyStockRpt.Rows[13].Cells["nt_purRetval"].Value = "0.00";
                DgMonthlyStockRpt.Rows[13].Cells["nt_cloqty"].Value = "0.00";
                DgMonthlyStockRpt.Rows[13].Cells["tot"].Value = "0.00";
                for (int i = 0; i < DgMonthlyStockRpt.Rows.Count; i++)
                {
                    if (DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value != null)
                    {
                        if (DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value != "" && DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value != null && DgMonthlyStockRpt.Rows[13].Cells["nq_purqty"].RowIndex != 13)
                        {

                            qty += Convert.ToDouble(DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value);
                            DgMonthlyStockRpt.Rows[13].Cells["nq_purqty"].Value = qty.ToString();
                        }
                        if (DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value != null && DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value != "")
                        {
                            DgMonthlyStockRpt.Rows[13].Cells["nq_purqty"].Value = DgMonthlyStockRpt.Rows[i].Cells["nq_purqty"].Value.ToString();
                        }
                        if (DgMonthlyStockRpt.Rows[i].Cells["nt_purval"].Value != null && DgMonthlyStockRpt.Rows[i].Cells["nt_purval"].Value != "")
                        {
                            qty = Convert.ToDouble(DgMonthlyStockRpt.Rows[i].Cells["nt_purval"].Value);
                            DgMonthlyStockRpt.Rows[13].Cells["nt_purval"].Value = qty.ToString();
                        }
                        if (DgMonthlyStockRpt.Rows[i].Cells["nt_prqty"].Value != null && DgMonthlyStockRpt.Rows[i].Cells["nt_prqty"].Value != "")
                        {
                            DgMonthlyStockRpt.Rows[13].Cells["nt_prqty"].Value = DgMonthlyStockRpt.Rows[i].Cells["nt_prqty"].Value.ToString();
                        }
                        if (DgMonthlyStockRpt.Rows[i].Cells["nt_purRetval"].Value != null && DgMonthlyStockRpt.Rows[i].Cells["nt_purRetval"].Value != "")
                        {
                            DgMonthlyStockRpt.Rows[13].Cells["nt_purRetval"].Value = DgMonthlyStockRpt.Rows[i].Cells["nt_purRetval"].Value.ToString();
                        }
                        if (DgMonthlyStockRpt.Rows[i].Cells["nt_cloqty"].Value != null && DgMonthlyStockRpt.Rows[i].Cells["nt_cloqty"].Value != "")
                        {
                            DgMonthlyStockRpt.Rows[13].Cells["nt_cloqty"].Value = DgMonthlyStockRpt.Rows[i].Cells["nt_cloqty"].Value.ToString();
                        }
                        if (DgMonthlyStockRpt.Rows[i].Cells["tot"].Value != null && DgMonthlyStockRpt.Rows[i].Cells["tot"].Value != "")
                        {
                            DgMonthlyStockRpt.Rows[13].Cells["tot"].Value = DgMonthlyStockRpt.Rows[i].Cells["tot"].Value.ToString();
                        }

                    }
                }
            }

            //}
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void dataGridView1_AlternatingRowsDefaultCellStyleChanged(object sender, EventArgs e)
        //{

        //}

        //private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        //{
        //}
        string total;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
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

                if (listview.Text != "")
                {
                    //nameidvalues = "1";
                    txtitemname.Text = listview.SelectedItem.ToString();
                    id_numbername = "0";
                    listbox_values();
                    datagridview_calculation();
                    DgMonthlyStockRpt.Focus();
                }
                //txtitemname.Text = listview.SelectedItem.ToString();
                pnlitems.Visible = false;

            }

        }
        string chk;
        string nameidvalues = "0";
        SqlDataReader dr = null;
        private void txtitemname1_TextChanged(object sender, EventArgs e)
        {
            //if (nameidvalues != "1")
            //{
            //if (listActionType != "Over" && listActionType != null)
            //{
            if (txtitemname.Text.Trim() != null && txtitemname.Text.Trim() != "")
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                bool isChk = false;
                // SqlCommand cmd = new SqlCommand("Select * from item_table where item_name like '" + txtitemname.Text.Trim() + "%'", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ItemNameLike");
                cmd.Parameters.AddWithValue("@itemName", txtitemname.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemCode","");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtItemSelect = new DataTable();
                dtItemSelect.Rows.Clear();

                adp.Fill(dtItemSelect);
                if (dtItemSelect.Rows.Count > 0)
                {
                    isChk = true;
                    string tempstr = dtItemSelect.Rows[0]["item_name"].ToString();
                    for (int k = 0; k < listview.Items.Count; k++)
                    {
                        if (tempstr == listview.Items[k].ToString())
                        {
                            listview.SetSelected(k, true);
                            txtitemname.Select();
                            chk = "1";
                            txtitemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
               {

                    chk = "1";
                    if (txtitemname.Text != "")
                    {
                        string name = txtitemname.Text.Remove(txtitemname.Text.Length - 1);
                        txtitemname.Text = name.ToString();
                        txtitemname.Select(txtitemname.Text.Length, 0);
                    }
                    txtitemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
                else
                {
                    chk = "1";
                }

            }
        }
            //    dr = cmd.ExecuteReader();
            //    bool isChk = false;

            //    while (dr.Read())
            //    {
            //        isChk = true;
            //        string tempStr = dr["item_name"].ToString();
            //        for (int i = 0; i < listview.Items.Count; i++)
            //        {
            //            if (dr["item_name"].ToString() == listview.Items[i].ToString())
            //            {
            //                id_numbername = "1";
            //                listview.SetSelected(i, true);
            //                txtitemname.Select();
            //                chk = "1";
            //                txtitemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
            //                break;
            //            }

            //        }
            //    }

            //    if (isChk == false)
            //    {
            //        chk = "2";
            //        txtitemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
            //    }
            //}
            //else
            //{
            //    chk = "1";

            //}
        

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
        private void txtitemname_Enter(object sender, EventArgs e)
        {
            pnlitems.Visible = true;
            load_listbox();
            datagridview_calculation();
        }
        public void load_listbox()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            if (id_numbername == "0")
            {
                DataTable dt_item_table = new DataTable();
                //SqlCommand cmd = new SqlCommand("select * from item_table ", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SelectItems");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_item_table.Rows.Clear();
                adp.Fill(dt_item_table);
                if (dt_item_table.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_item_table.Rows.Count; i++)
                    {
                        listview.Items.Add(dt_item_table.Rows[i]["Item_name"].ToString());
                    }
                }
            }
        }
        string name_code;
        public void listbox_values()
        {
            DateTime fromdate = new DateTime();
            fromdate = Convert.ToDateTime(passingvalues.tStartDateParthi.Year + "/" + passingvalues.tStartDateParthi.Month + "/" + passingvalues.tStartDateParthi.Day);
            DateTime enddate = new DateTime();
            enddate = Convert.ToDateTime(passingvalues.tToDateParthi.Year + "/" + passingvalues.tToDateParthi.Month + "/" + passingvalues.tToDateParthi.Day);
            if (id_numbername == "0")
            {
                //string fromdate = passingvalues.from_date.ToString();
                //string enddate = passingvalues.end_date.ToString();

               // SqlCommand cmd = new SqlCommand("select * from item_table where item_name='" + txtitemname.Text + "'", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ItemName");
                cmd.Parameters.AddWithValue("@itemName", txtitemname.Text);
                cmd.Parameters.AddWithValue("@ItemCode", "");
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    name_code = dt.Rows[0]["item_code"].ToString();

                    item_code = dt.Rows[0]["item_no"].ToString();
                }
                string value_number = passingvalues.vaues.ToString();
                //  txtitemname.Text = passingvalues.item_name.ToString();
                if (value_number == "1")
                {
                    //  SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                  //  SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[1].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[1].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[1].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[1].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[1].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "2")
                {
                    // SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[2].Cells["nq_purqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[2].Cells["nt_purval"].Value = dt.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[2].Cells["nt_prqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[2].Cells["nt_purRetval"].Value = dt.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[2].Cells["nt_cloqty"].Value = dt.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[2].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "3")
                {
                    //  SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                   // SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[3].Cells["nq_purqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[3].Cells["nt_purval"].Value = dt.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[3].Cells["nt_prqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[3].Cells["nt_purRetval"].Value = dt.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[3].Cells["nt_cloqty"].Value = dt.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[3].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "4")
                {
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                  //  SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[4].Cells["nq_purqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[4].Cells["nt_purval"].Value = dt.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[4].Cells["nt_prqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[4].Cells["nt_purRetval"].Value = dt.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[4].Cells["nt_cloqty"].Value = dt.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[4].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "5")
                {
                    //  SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[5].Cells["nq_purqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[5].Cells["nt_purval"].Value = dt.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[5].Cells["nt_prqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[5].Cells["nt_purRetval"].Value = dt.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[5].Cells["nt_cloqty"].Value = dt.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[5].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "6")
                {
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[6].Cells["nq_purqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[6].Cells["nt_purval"].Value = dt.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[6].Cells["nt_prqty"].Value = dt.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[6].Cells["nt_purRetval"].Value = dt.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[6].Cells["nt_cloqty"].Value = dt.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[6].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "7")
                {
                    // SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[7].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[7].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[7].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[7].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[7].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[7].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "8")
                {
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    dt1.Rows.Clear();
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[8].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[8].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[8].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[8].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[8].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[8].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "9")
                {
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + name_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    dt1.Rows.Clear();
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[9].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[9].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[9].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[9].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[9].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[19].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
                if (value_number == "10")
                {
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + item_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    adp1.Fill(dt1);
                    dt1.Rows.Clear();
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[10].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[10].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[10].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[10].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[10].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[10].Cells["tot"].Value = tot.ToString();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }

                }
                if (value_number == "11")
                {
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval,item_cost from item_table  where item_code='" + item_code.ToString() + "'", con);
                    //SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);

                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["Strnparty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[11].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[11].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[11].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purtqty"].ToString();
                                DgMonthlyStockRpt.Rows[11].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[11].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                if (dt.Rows[0]["nt_cloqty"].ToString() != "")
                                {
                                    amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                    double tot = 0.00;
                                    tot = Convert.ToDouble(name * amount);
                                    DgMonthlyStockRpt.Rows[11].Cells["tot"].Value = tot.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }

                }
                if (value_number == "12")
                {
                    // SqlDataAdapter adp1 = new SqlDataAdapter("select nt_purRetval,nt_prqty,nt_purqty,nt_cloqty,nt_purval from item_table where item_code='" + item_code.ToString() + "'", con);
                   // SqlDataAdapter adp1 = new SqlDataAdapter("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(passingvalues.from_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(passingvalues.end_date1).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                  //  SqlCommand cmd3 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between '" + Convert.ToDateTime(fromdate).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and '" + Convert.ToDateTime(enddate).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' ", con);
                    SqlCommand cmd1 = new SqlCommand("select item_table.nt_purRetval,item_table.nt_purqty,item_table.nt_purqty,item_table.nt_cloqty,item_table.nt_purval,stktrn_table.nt_qty,stktrn_table.Strnparty_no,stktrn_table.Strn_type from item_table,stktrn_table where (item_table.item_no=stktrn_table.item_no)  and  item_table.item_no= '" + item_code.ToString() + "' and stktrn_table.strn_date between @tStart and @tEnd ", con);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(fromdate.Year, fromdate.Month, fromdate.Day));
                    cmd1.Parameters.AddWithValue("@tEnd", new DateTime(enddate.Year, enddate.Month, enddate.Day));
                    DataTable dt1 = new DataTable();
                    dt1.Rows.Clear();
                    adp1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strn_typeno = dt1.Rows[i]["StrnParty_no"].ToString();
                            string strn_type = dt1.Rows[i]["Strn_type"].ToString();
                            if (strn_typeno == "0" && strn_type == "0")
                            {
                                DgMonthlyStockRpt.Rows[0].Cells["nq_purqty"].Value = dt.Rows[0]["nt_opnqty"].ToString();
                                DgMonthlyStockRpt.Rows[0].Cells["nt_cloqty"].Value = dt.Rows[0]["nt_cloqty"].ToString();
                                double itemcost = 0.00;
                                itemcost = Convert.ToDouble(dt.Rows[0]["Item_cost"].ToString());
                                double itemqty = 0.00;
                                itemqty = Convert.ToDouble(dt1.Rows[i]["nt_qty"].ToString());
                                itemcost = Convert.ToDouble(itemqty * itemcost);
                                DgMonthlyStockRpt.Rows[1].Cells["tot"].Value = itemcost.ToString();
                            }
                            else
                            {
                                DgMonthlyStockRpt.Rows[12].Cells["nq_purqty"].Value = dt1.Rows[i]["nt_purqty"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_purval"].Value = dt1.Rows[i]["nt_purval"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_prqty"].Value = dt1.Rows[i]["nt_purtqty"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_purRetval"].Value = dt1.Rows[i]["nt_purRetval"].ToString();
                                DgMonthlyStockRpt.Rows[12].Cells["nt_cloqty"].Value = dt1.Rows[i]["nt_cloqty"].ToString();
                                double name = 0.00;
                                name = Convert.ToDouble(dt.Rows[0]["item_cost"].ToString());
                                double amount = 0.00;
                                if (dt.Rows[0]["nt_cloqty"].ToString() != null && dt.Rows[0]["nt_cloqty"].ToString() != "")
                                {
                                    amount = Convert.ToDouble(dt.Rows[0]["nt_cloqty"].ToString());
                                }
                                double tot = 0.00;
                                tot = Convert.ToDouble(name * amount);
                                DgMonthlyStockRpt.Rows[12].Cells["tot"].Value = tot.ToString();
                                datagridview_calculation();
                            }
                        }
                    }
                    else
                    {
                        altercell();
                    }
                }
            }
        }
        public void altercell()
        {
          //  int value_number1 = 0;

            DgMonthlyStockRpt.Rows[value_number2].Cells["nq_purqty"].Value = "0";
            DgMonthlyStockRpt.Rows[value_number2].Cells["nt_purval"].Value = "0";
            DgMonthlyStockRpt.Rows[value_number2].Cells["nt_prqty"].Value = "0";
            DgMonthlyStockRpt.Rows[value_number2].Cells["nt_purRetval"].Value = "0";
            DgMonthlyStockRpt.Rows[value_number2].Cells["nt_cloqty"].Value = "0";
            DgMonthlyStockRpt.Rows[value_number2].Cells["tot"].Value = "0";
            datagridview_calculation();
        }
        private void txtitemname_Leave(object sender, EventArgs e)
        {
            //  listbox_values();
        }

        private void listview_Click(object sender, EventArgs e)
        {
            if (listview.Text != "")
            {
                string idvalues = listview.SelectedItem.ToString();
                txtitemname.Text = idvalues.ToString();
                id_numbername = "0";
                listbox_values();

                pnlitems.Visible = false;
                // grid_ca
                // listbox_values();
            }
        }
        string itemnumberbassing;
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgMonthlyStockRpt.Rows.Count > 0)
            {
                int rowindex = e.RowIndex;
                if (e.RowIndex != 13)
                {
                    if (DgMonthlyStockRpt.Rows.Count > 0)
                    {

                        for (int kj = 0; kj < DgMonthlyStockRpt.Rows.Count; kj++)
                        {
                            if (DgMonthlyStockRpt.Rows[kj].Cells[0].Value != null)
                            {
                                if (DgMonthlyStockRpt.Rows[kj].Cells["tot"].Value!= null)
                                {
                                    total = DgMonthlyStockRpt.Rows[kj].Cells["tot"].Value.ToString();
                                }
                            }
                        }

                        if (e.RowIndex != 13)
                        {

                            if (e.RowIndex.Equals(0))
                            {
                                if (txtitemname.Text.IndexOf("'") != -1)
                                {
                                    string name = txtitemname.Text.Replace("'", "''");
                                    // SqlCommand cmd = new SqlCommand("select * from item_table where item_name='" + name + "'", con);
                                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ActionType", "ItemName");
                                    cmd.Parameters.AddWithValue("@itemName", name);
                                    cmd.Parameters.AddWithValue("@ItemCode", "");
                                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                    DataTable dt = new DataTable();
                                    dt.Rows.Clear();
                                    adp.Fill(dt);
                                    if (dt.Rows.Count > 0)
                                    {
                                        itemnumberbassing = dt.Rows[0]["item_no"].ToString();
                                        itemcreationformload();
                                    }
                                }
                                else
                                {
                                    // SqlCommand cmd = new SqlCommand("select * from item_table where item_name='" + txtitemname.Text + "'", con);
                                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ActionType", "ItemName");
                                    cmd.Parameters.AddWithValue("@itemName", txtitemname.Text);
                                    cmd.Parameters.AddWithValue("@ItemCode", "");
                                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                    DataTable dt = new DataTable();
                                    dt.Rows.Clear();
                                    adp.Fill(dt);
                                    if (dt.Rows.Count > 0)
                                    {
                                        itemnumberbassing = dt.Rows[0]["item_no"].ToString();
                                        itemcreationformload();
                                    }
                                }
                            }
                            else
                                if (e.RowIndex != 0 != null)
                                {
                                    passingvalues.id_number_item_leder = item_code.ToString();
                                    passingvalues.tot = total.ToString();
                                    Form currentForm = null;//declaring a variable to hold form.
                                    foreach (Form frm1 in this.MdiChildren)//loop in all child forms in mdi
                                    {
                                        if (frm1 is MonthlystockBreakeUp)//if any of the forms type is frmSub
                                        {
                                            currentForm = frm1;//set that form to currentForm variable
                                            break;
                                        }
                                    }
                                    if (currentForm == null)//if form not found
                                    {
                                        passingvalues.tAmountType = "Gross Amount";
                                        ItemLedger frm1 = new ItemLedger();
                                        frm1.MdiParent = this.ParentForm;
                                        frm1.StartPosition = FormStartPosition.Manual;
                                        frm1.WindowState = FormWindowState.Normal;
                                        frm1.Location = new Point(0, 80);
                                        frm1.Show();
                                    }
                                    else//if form is already in child forms
                                    {
                                        currentForm.BringToFront();
                                    }
                                }
                        }
                    }
                }
            }
        }
        public void itemcreationformload()
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm1 in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm1 is MonthlystockBreakeUp)//if any of the forms type is frmSub
                {
                    currentForm = frm1;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                ItemCreations frm1 = new ItemCreations(itemnumberbassing);
                frm1.MdiParent = this.ParentForm;
                frm1.StartPosition = FormStartPosition.Manual;
                frm1.WindowState = FormWindowState.Normal;
                frm1.Location = new Point(0, 80);
                frm1.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }
    }
}

