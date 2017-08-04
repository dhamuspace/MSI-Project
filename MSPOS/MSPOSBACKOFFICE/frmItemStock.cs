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
using System.IO;

namespace MSPOSBACKOFFICE
{
    public partial class frmItemStock : Form
    {
        public frmItemStock()
        {
            InitializeComponent();

            DgStockReport.DefaultCellStyle.ForeColor = Color.Black;           
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
        private void frmItemStock_Load(object sender, EventArgs e)
        {
            Gridassign();
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        DataTable dt = new DataTable();
        public void Gridassign()
        {
            dt.Rows.Clear();
            //SqlCommand cmd = new SqlCommand("select Item_no,Item_code,Item_name,nt_opnqty as OpenQty,nt_purqty as PurchaseQty,nt_salqty as SalesQty,nt_cloqty as CloseQty from item_table",con);
            //SqlCommand cmd = new SqlCommand("select Item_code,Item_name,nt_opnqty ,nt_purqty ,nt_salqty ,nt_cloqty from item_table order by Item_name asc", con);
            SqlCommand cmd = new SqlCommand("select Item_code,Item_name,nt_opnqty,nt_purqty,nt_salqty,nt_cloqty, Item_cost as Rate, convert(numeric(18,2),nt_cloqty*Item_cost) as Value from item_table order by Item_name asc", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            DgStockReport.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                DgStockReport.DataSource = dt;
                lbltotcount.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                gridcalculation();                
            }
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
                        //qty += Convert.ToDouble(DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString()) <= 0 ? 0.00 : Convert.ToDouble(DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString());
                        qty += Convert.ToDouble(DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString());
                    }
                    if (DgStockReport.Rows[i].Cells["Rate"].Value != null && DgStockReport.Rows[i].Cells["Rate"].Value != "")
                    {
                        //itemtotal += Convert.ToDouble(DgStockReport.Rows[i].Cells["Rate"].Value.ToString()) <= 0 ? 0.00 : Convert.ToDouble(DgStockReport.Rows[i].Cells["Rate"].Value.ToString());
                        itemtotal += Convert.ToDouble(DgStockReport.Rows[i].Cells["Rate"].Value.ToString());
                    }
                    if (DgStockReport.Rows[i].Cells["Value"].Value != null && DgStockReport.Rows[i].Cells["Value"].Value != "")
                    {
                        //itemvalues += Convert.ToDouble(DgStockReport.Rows[i].Cells["Value"].Value.ToString()) <= 0 ? 0.00 : Convert.ToDouble(DgStockReport.Rows[i].Cells["Value"].Value.ToString());
                        itemvalues += Convert.ToDouble(DgStockReport.Rows[i].Cells["Value"].Value.ToString());
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable getdata()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Code");
            dt1.Columns.Add("Name");
            dt1.Columns.Add("OpenQty");
            dt1.Columns.Add("PurchaseQty");
            dt1.Columns.Add("SalesQty");            
            dt1.Columns.Add("CloseQty");
            // dt1.Rows.Add("1", "raj", "2", "10", "15");
            //dt1.Rows.Add("1", "raj", "2", "10", "15");          
            for (int i = 0; i < DgStockReport.Rows.Count - 1; i++)
            {
                dt1.Rows.Add(DgStockReport.Rows[i].Cells["Item_code"].Value.ToString(), DgStockReport.Rows[i].Cells["Item_name"].Value.ToString(), DgStockReport.Rows[i].Cells["nt_opnqty"].Value.ToString(), DgStockReport.Rows[i].Cells["nt_purqty"].Value.ToString(), DgStockReport.Rows[i].Cells["nt_salqty"].Value.ToString(), DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString());
            }
            return dt1;
        }
        ReportViewer rpt = new ReportViewer();
        Microsoft.Reporting.WinForms.ReportViewer RptviewerStockReport = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource RptdsStockReport = new Microsoft.Reporting.WinForms.ReportDataSource();
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                funprint();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funprint()
        {
            try
            {
                DsItemStock dsSalesSummaryObj = new DsItemStock();
                for (int i = 0; i < DgStockReport.Rows.Count - 1; i++)
                {
                    dsSalesSummaryObj.Tables["DtItemStock"].Rows.Add(Convert.ToString(DgStockReport.Rows[i].Cells["Item_code"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["Item_name"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["nt_opnqty"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["nt_purqty"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["nt_salqty"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["nt_cloqty"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["Rate"].Value.ToString()), Convert.ToString(DgStockReport.Rows[i].Cells["Value"].Value.ToString()));
                }
                rpt.Reset();
                //DataTable dt = getdata();
                ReportDataSource rds = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DtItemStock"]);
                rpt.LocalReport.DataSources.Add(rds);
                rpt.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RptPurchaseAlter.rdlc";
                ReportParameter rptCount = new ReportParameter("Count", Convert.ToString(lbltotcount.Text), false);
                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptCount });
                ReportParameter rptItems = new ReportParameter("Items", Convert.ToString(lblqty.Text), false);
                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptItems });
                ReportParameter rptTot = new ReportParameter("Total", Convert.ToString(lbltot.Text), false);
                this.rpt.LocalReport.SetParameters(new ReportParameter[] { rptTot });
                dsSalesSummaryObj.Tables["DtItemStock"].EndInit();
                rpt.RefreshReport();
                rpt.RenderingComplete += new RenderingCompleteEventHandler(PrintReport);
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
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
        public void PrintReport(object sender, RenderingCompleteEventArgs e)
        {
            rpt.PrintDialog();
            rpt.Clear();
            rpt.LocalReport.ReleaseSandboxAppDomain();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Bind table data to Stream Writer to export data to respective folder
                string text = "ReportXL" + DateTime.Now.ToString("ddMMyyyy hh-mm");
                StreamWriter wr = new StreamWriter(@"C:\Reports\" + text.ToString() + ".xls");
                // Write Columns to excel file
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                }
                wr.WriteLine();
                //write rows to excel file
                for (int i = 0; i < (dt.Rows.Count); i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                        }
                        else
                        {
                            wr.Write("\t");
                        }
                    }
                    wr.WriteLine();
                }
                wr.Close();
                MyMessageBox.ShowBox("Report Saved in C drive Reports folder '" + text.ToString() + "'.xls", "Message");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnNotepad_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "ItemReportNote" + DateTime.Now.ToString("ddMMyyyy hh-mm");
                StreamWriter sW = new StreamWriter("C:\\Reports\\" + text.ToString() + ".txt");

                for (int row = 0; row < DgStockReport.Rows.Count-1; row++)
                {
                    string lines = "";
                    for (int col = 0; col < 8; col++)
                    {
                        lines += (string.IsNullOrEmpty(lines) ? " " : ", ") + DgStockReport.Rows[row].Cells[col].Value.ToString();
                    }

                    sW.WriteLine(lines);
                }

                sW.Close();
                MyMessageBox.ShowBox("Report Saved in C drive Reports folder '"+text.ToString()+"'.txt", "Message");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}
