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
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class StocReport : Form
    {
        public StocReport()
        {
            InitializeComponent();

            DgStockReport.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            DgStockReport.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

            DgStockReport.BackgroundColor = Color.White;

            foreach (DataGridViewColumn col in DgStockReport.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            DgStockReport.ReadOnly = true;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        string query_values = "";
        string query = "";
        private void StocReport_Load(object sender, EventArgs e)
        {
            try
            {
                query = passingvalues.passingquery.ToString();
                string itemname = passingvalues.item_name.ToString();
                string remarks = passingvalues.remarks.ToString();
                string itemcode = passingvalues.itemcode.ToString();
                // string fromdate = passingvalues.datefrom.ToString();
                // string todate = passingvalues.dateto.ToString();
                DateTime fromdate = Convert.ToDateTime(passingvalues.tStartDateParthi.Year+"/"+passingvalues.tStartDateParthi.Month+"/"+passingvalues.tStartDateParthi.Day);
                DateTime todate = Convert.ToDateTime(passingvalues.tToDateParthi.Year+"/"+passingvalues.tToDateParthi.Month+"/"+passingvalues.tToDateParthi.Day);
                DtpFromdate.Value =Convert.ToDateTime(passingvalues.tStartDateParthi.Year + "/" + passingvalues.tStartDateParthi.Month + "/" + passingvalues.tStartDateParthi.Day);
                //DtpTodate.Value = Convert.ToDateTime(passingvalues.tToDateParthi).ToString("dd/MM/yyyy");
                DtpTodate.Value = Convert.ToDateTime(passingvalues.tToDateParthi.Year + "/" + passingvalues.tToDateParthi.Month + "/" + passingvalues.tToDateParthi.Day);
                string stock = passingvalues.stock.ToString();
                string level = passingvalues.leave.ToString();
                string movement = passingvalues.movent.ToString();
                query_values = query.ToString();
                Gridassign();
                DgStockReport.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string remainingquery = "",ReplaceChar,ReplaceChar1;
        public void Gridassign()
        {
            try
            {
                DataTable dtSalesQty = new DataTable();
                remainingquery = query_values.ToString();
                if (passingvalues.Stock_ItemRport == "StockReport")
                {
                    string salesqtyquery = "";
                    salesqtyquery = passingvalues.Strn1_salesqty.ToString();
                      //query_values += " and  Item_table.item_no=stktrn_table.item_no and strn_date between @tStart and @tEnd group by stktrn_table.item_no ,Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost ";
                     //After Dhamu Totled Changed: This Code,Not include in todate.only processing in single date:
                    //query_values += " and  Item_table.item_no=stktrn_table.item_no and strn_date=@tStart  group by stktrn_table.item_no ,Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost ";
                   //query_values += "  and Item_table.Item_no  in (Select Item_no from stktrn_table where item_table.Item_no=stktrn_table.Item_no and stktrn_table.strn_date=@tStart )   group by stktrn_table.item_no ,Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost  ";
                    query_values += " and item_table.Item_no=stktrn_table.Item_no and stktrn_table.strn_date<=@tStart  group by Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost,item_table.Item_no";
                    salesqtyquery += " and stktrn_table.strn_date<=@tStart and item_table.Item_no=stktrn_table.Item_no group by Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost,item_table.Item_no";
                    SqlCommand cmd = new SqlCommand(query_values, con);
                    cmd.Parameters.AddWithValue("@tStart", new DateTime(DtpFromdate.Value.Year, DtpFromdate.Value.Month, DtpFromdate.Value.Day));
                    //cmd.Parameters.AddWithValue("@tEnd", new DateTime(DtpTodate.Value.Year, DtpTodate.Value.Month, DtpTodate.Value.Day));
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt.Rows.Clear();
                    adp.Fill(dt);

                    string chcking = "";
                    SqlCommand cmd_selectsales = new SqlCommand(salesqtyquery, con);
                    cmd_selectsales.Parameters.AddWithValue("@tStart", new DateTime(DtpFromdate.Value.Year, DtpFromdate.Value.Month, DtpFromdate.Value.Day));
                    SqlDataAdapter pad = new SqlDataAdapter(cmd_selectsales);
                    dtSalesQty.Rows.Clear();
                    pad.Fill(dtSalesQty);
                    if (dt.Rows.Count > 0)
                    {
                        if (dtSalesQty.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtSalesQty.Rows.Count; j++)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][1].ToString() == dtSalesQty.Rows[j][1].ToString())
                                    {
                                        chcking = "1";
                                        dt.Rows[i]["nt_cloqty"] = (Convert.ToDouble(dt.Rows[i]["nt_cloqty"]) - (Convert.ToDouble(dtSalesQty.Rows[j]["nt_cloqty"]))).ToString();
                                    }
                                }
                            }
                           for(int k=0;k<dt.Rows.Count;k++)
                           {
                               dt.Rows[k]["tot"] = Convert.ToDouble(Convert.ToDouble(dt.Rows[k]["nt_cloqty"]) * Convert.ToDouble(dt.Rows[k]["item_cost"])).ToString();
                           }
                            DgStockReport.DataSource = dt;
                            lbltotcount.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                            gridcalculation();
                            DgStockReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            DgStockReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        if (chcking == "")
                        {
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                dt.Rows[k]["tot"] = Convert.ToDouble(Convert.ToDouble(dt.Rows[k]["nt_cloqty"]) * Convert.ToDouble(dt.Rows[k]["item_cost"])).ToString();
                            }
                            DgStockReport.DataSource = dt;
                            lbltotcount.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                            gridcalculation();
                            DgStockReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            DgStockReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                    else
                    {
                        //if (dtSalesQty.Rows.Count > 0)
                        //{
                        //    DgStockReport.DataSource = dtSalesQty;
                        //    lbltotcount.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                        //    gridcalculation();
                        //    DgStockReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //    DgStockReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //}
                    }
                }
                else if (passingvalues.Stock_ItemRport != "StockReport" || passingvalues.Stock_ItemRport == "")
                {
                    lbltotcount.Text = "0";
                    lblqty.Text = "0";
                    lbltot.Text = "0";
                    dt.Rows.Clear();
                    string RemainingRecordsfound = "";
                    if (passingvalues.DateSelectChanged == string.Empty )
                    {
                        remainingquery += "  group by Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost ";
                    }
                    else
                    {
                        //remainingquery = "";
                        //if (passingvalues.strDateWiseChkRpt != "")
                        //{
                        //    remainingquery = passingvalues.strDateWiseChkRpt;
                        //    remainingquery += "  group by  Item_table.Item_code,item_table.Item_name,item_table.item_cost,item_table.nt_cloqty";
                        //    RemainingRecordsfound = passingvalues.strDateWiseNoRcd;
                        //    RemainingRecordsfound += "  item_table.Item_no  not in (Select Item_no from stktrn_table where item_table.Item_no=stktrn_table.Item_no and Strn_date=@tStart) group by item_table.item_name,Item_table.Item_code,item_table.Item_cost";
                        //    SqlCommand cmd_select = new SqlCommand(RemainingRecordsfound, con);
                        //    SqlDataAdapter adpnothave = new SqlDataAdapter(cmd_select);
                        //    cmd_select.Parameters.AddWithValue("@tStart", new DateTime(DtpFromdate.Value.Year, DtpFromdate.Value.Month, DtpFromdate.Value.Day));
                        //    dt.Rows.Clear();
                        //    adpnothave.Fill(dt);
                        //}
                        //else
                        //{
                        //   string strquer=  passingvalues.passingquery.ToString();
                        //}
                        remainingquery += "  group by Item_table.Item_name,Item_table.Item_code,item_table.nt_cloqty,Item_table.Item_cost ";
                    }
                    SqlCommand cmd1 = new SqlCommand(remainingquery, con);
                    if (passingvalues.DateSelectChanged == "1")
                    {
                        cmd1.Parameters.AddWithValue("@tStart", new DateTime(DtpFromdate.Value.Year, DtpFromdate.Value.Month, DtpFromdate.Value.Day));
                    }
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DgStockReport.DataSource = dt;
                        lbltotcount.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                        gridcalculation();
                        DgStockReport.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DgStockReport.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            passingvalues.Stock_ItemRport = string.Empty;
            this.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        double qty = 0.00, itemtotal = 0.00, itemvalues = 0.00;
        public void gridcalculation()
        {
            try
            {
                qty = 0.00; itemtotal = 0.00; itemvalues = 0.00;
                for (int i = 0; i < DgStockReport.Rows.Count - 1; i++)
                {
                    if (DgStockReport.Rows[i].Cells["nt_cloqty"].Value != null && DgStockReport.Rows[i].Cells["nt_cloqty"].Value != "")
                    {
                        qty += Convert.ToDouble(DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString())<=0? 0.00 : Convert.ToDouble(DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString());
                    }
                    if (DgStockReport.Rows[i].Cells["Item_cost"].Value != null && DgStockReport.Rows[i].Cells["Item_cost"].Value != "")
                    {
                        itemtotal += Convert.ToDouble(DgStockReport.Rows[i].Cells["Item_cost"].Value.ToString()) <=0? 0.00 : Convert.ToDouble(DgStockReport.Rows[i].Cells["Item_cost"].Value.ToString());
                    }
                    if (DgStockReport.Rows[i].Cells["tot"].Value != null && DgStockReport.Rows[i].Cells["tot"].Value != "")
                    {
                        itemvalues += Convert.ToDouble(DgStockReport.Rows[i].Cells["tot"].Value.ToString())<=0? 0.00 : Convert.ToDouble(DgStockReport.Rows[i].Cells["tot"].Value.ToString());
                    }
                }
                lblqty.Text = Convert.ToDouble(qty).ToString("0.00");
                lbltot.Text = Convert.ToDouble(itemvalues).ToString("0.00");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DgStockReport.Rows.Count > 0)
                {
                    int rowIndex = e.RowIndex;
                    if (DgStockReport.Rows[rowIndex].Cells[0].Value != null)
                    {
                        string id = Convert.ToString(DgStockReport.Rows[rowIndex].Cells["Item_code"].Value);
                        string item_name = Convert.ToString(DgStockReport.Rows[rowIndex].Cells["Item_name"].Value);
                        passingvalues.str = id;
                        string date_mont = DtpFromdate.Value.Month.ToString();
                        passingvalues.vaues = date_mont.ToString();
                        passingvalues.item_name = item_name.ToString();
                        //passingvalues.from_date1 = DtpFromdate.Value;
                        //passingvalues.end_date1 = DtpTodate.Value;
                        passingvalues.tStartDateParthi = Convert.ToDateTime(DtpFromdate.Value.Year+"/"+DtpFromdate.Value.Month+"/"+DtpFromdate.Value.Day);
                        passingvalues.tToDateParthi = Convert.ToDateTime(DtpTodate.Value.Year+"/"+DtpTodate.Value.Month+"/"+DtpTodate.Value.Day);
                        MonthlystockBreakeUp frm = new MonthlystockBreakeUp();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ItemFilter frm = new ItemFilter();
                if (!frm.Visible)
                {
                    StocReport frm1 = new StocReport();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void DtpTodate_CloseUp(object sender, EventArgs e)
        {
            try
            {
                query_values = "";
                query_values = query.ToString();
                Gridassign();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void DtpTodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    query_values = "";
                    query_values = query.ToString();
                    lbltotcount.Text = "0";
                    lblqty.Text = "0";
                    lbltot.Text = "0";
                    Gridassign();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void lblqty_Click(object sender, EventArgs e)
        {

        }

        private void DgStockReport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Y)
            {
                
            }
        }
        private void DgStockReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Y)
            {  
            }
        }
        private DataTable getdata()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Code");
            dt1.Columns.Add("Name");
            dt1.Columns.Add("Qty");
            dt1.Columns.Add("Rate");
            dt1.Columns.Add("Value");
           // dt1.Rows.Add("1", "raj", "2", "10", "15");
            //dt1.Rows.Add("1", "raj", "2", "10", "15");          
            for (int i = 0; i < DgStockReport.Rows.Count - 1; i++)
            {
                dt1.Rows.Add(DgStockReport.Rows[i].Cells["Item_code"].Value.ToString(), DgStockReport.Rows[i].Cells["Item_name"].Value.ToString(), DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString(), DgStockReport.Rows[i].Cells["item_cost"].Value.ToString(), DgStockReport.Rows[i].Cells["tot"].Value.ToString());
            }
            return dt1;
        }
        Microsoft.Reporting.WinForms.ReportViewer RptviewerStockReport = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource RptdsStockReport = new Microsoft.Reporting.WinForms.ReportDataSource();
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                RptviewerStockReport.Reset();
                DataTable dt = getdata();
                ReportDataSource rds = new ReportDataSource("DsStockReport", dt);
                RptviewerStockReport.LocalReport.DataSources.Add(rds);
                RptviewerStockReport.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RptStockReport.rdlc";
                ReportParameter rptfrm = new ReportParameter("RptFrom", Convert.ToString(DtpFromdate.Value.Day + "/" + DtpFromdate.Value.Month + "/" + DtpFromdate.Value.Year), false);
                ReportParameter rptItems = new ReportParameter("RptItems", Convert.ToString(lbltotcount.Text), false);
                ReportParameter rptQty = new ReportParameter("RptQty", Convert.ToString(lblqty.Text), false);
                ReportParameter rptTot = new ReportParameter("RptTot", Convert.ToString(lbltot.Text), false);
                this.RptviewerStockReport.LocalReport.SetParameters(new ReportParameter[] { rptfrm });
                this.RptviewerStockReport.LocalReport.SetParameters(new ReportParameter[] { rptItems });
                this.RptviewerStockReport.LocalReport.SetParameters(new ReportParameter[] { rptQty });
                this.RptviewerStockReport.LocalReport.SetParameters(new ReportParameter[] { rptTot });
                RptviewerStockReport.RefreshReport();
                RptviewerStockReport.RenderingComplete += new RenderingCompleteEventHandler(PrintStockReport);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void PrintStockReport(object sender, RenderingCompleteEventArgs e)
        {
            try
            {
                RptviewerStockReport.PrintDialog();
                RptviewerStockReport.Clear();
                RptviewerStockReport.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    } 
}
