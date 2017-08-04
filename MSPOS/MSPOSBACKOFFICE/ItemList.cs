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
    public partial class ItemList : Form
    {
        string code_number;
        public ItemList(string number)
        {
            InitializeComponent();
            code_number = number;

            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

            dataGridView1.BackgroundColor = Color.White;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void ItemList_Load(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                con.Close();
                con.Open();
                DataTable dt = new DataTable();
                string query = passingvalues.passingquery;

                //SqlCommand cmd = new SqlCommand("select Item_code,Item_Name,nt_opnqty,Stock_Value,Item_ndp,Item_cost,Item_mrsp,item_special1,item_special2,item_special3 from item_table where item_code='" + code_number + "'", con);
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                if (dr != null)
                {
                    dr.Close();
                }
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    int i = Convert.ToInt32(dt.Rows.Count);
                    lblNoofItems.Text = i.ToString();
                }

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
              //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ItemCreations frm = new ItemCreations("");
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
                // frm.Show();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string code_numbers;
    

        private void btn_filter_Click(object sender, EventArgs e)
        {
            ItemFilter frm = new ItemFilter();
            if (!frm.Visible)
            {
                ItemList frm1 = new ItemList("");
                this.Close();
            }   
        }
        SqlDataReader dr = null;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    int i = e.RowIndex;
                    con.Close();
                    con.Open();
                    string j = Convert.ToString(dataGridView1.Rows[i].Cells["Item_name"].Value).ToString();

                    SqlCommand cmd = new SqlCommand("select * from item_table where Item_name='" + j + "'", con);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    DataTable dt=new DataTable ();
                    dt.Rows.Clear();
                    if(dr!=null)
                    {
                        dr.Close();
                    }
                    adp.Fill(dt); 
                    if(dt.Rows.Count>0)
                    {
                        code_numbers = dt.Rows[0]["item_no"].ToString();
                    }
                    ItemCreations frm = new ItemCreations(code_numbers);
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();

                }
            }
            catch(Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private DataTable getDate()
        {

            DataTable _dt = new DataTable();
            _dt.Columns.Add("Code");
            _dt.Columns.Add("Name");
            _dt.Columns.Add("NtQty");
            _dt.Columns.Add("Value");
            _dt.Columns.Add("PRate");
            _dt.Columns.Add("Cost");
            _dt.Columns.Add("Mrp");
            _dt.Columns.Add("Special-1");
            _dt.Columns.Add("Special-2");
            _dt.Columns.Add("Special-3");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                _dt.Rows.Add(dataGridView1.Rows[i].Cells["Item_code"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_name"].Value.ToString(), dataGridView1.Rows[i].Cells["nt_opnqty"].Value.ToString(), dataGridView1.Rows[i].Cells["Stock_Value"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_ndp"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_Cost"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_mrsp"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_Special1"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_Special2"].Value.ToString(), dataGridView1.Rows[i].Cells["Item_Special3"].Value.ToString());
            }
            return _dt;
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewerSales.Reset();
                DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("DsItemList", dt);
                reportViewerSales.LocalReport.DataSources.Add(ds);
               // reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.RptItemList.rdlc";
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RptItemList.rdlc";
                //Passing Parmetes:
                // ReportParameter rptFrom = new ReportParameter("RptFromDate", Convert.ToString(From_date.Value.Day + "/" + From_date.Value.Day + "/" + From_date.Value.Day+"/"), false);
                // ReportParameter rptTo = new ReportParameter("RptToDate", Convert.ToString(To_date.Value.Day + "/" + To_date.Value.Day + "/" + To_date.Value.Day + "/"), false);

                ReportParameter rptNoItems = new ReportParameter("RptNumItemparameter", Convert.ToString(lblNoofItems.Text), false);

                //ReportParameter rp2 = new ReportParameter("DateTo", "300");
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rptNoItems });

                dt.EndInit();
                reportViewerSales.RefreshReport();
                reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintRemove);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void PrintRemove(object sender, RenderingCompleteEventArgs e)
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
