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
using System.Collections;
using System.Globalization;

//using iTextSharp.text;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class SalesBOMMasterAltertion : Form
    {
        public SalesBOMMasterAltertion()
        {
            InitializeComponent();
            DgBomsEntry.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            DgBomsEntry.BackgroundColor = Color.White;

            foreach (DataGridViewColumn col in DgBomsEntry.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            txtbomstyle.Text = "Normal";
            hidemethod();
            DgBomsEntry.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["TaxQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlCommand cmd = null;
        SqlDataAdapter adp = null;


        public void hidemethod()
        {
            if (txtbomstyle.Text == "Normal")
            {
                DgBomsEntry.Columns["ItemNames"].Visible = false;
                DgBomsEntry.Columns["Unit"].Visible = false;
                DgBomsEntry.Columns["Type"].Visible = false;
                DgBomsEntry.Columns["Qty"].Visible = false;
                DgBomsEntry.Columns["TaxQty"].Visible = false;
                DgBomsEntry.Columns["Rate"].Visible = false;
                DgBomsEntry.Columns["Amount"].Visible = false;

                for (int i = 0; i < 21; i++)
                {
                    DgBomsEntry.Rows.Add();
                }
                DgBomsEntry.Columns["BomName"].Width = 200;
                normalgridload();
            }
            else
            {
                DgBomsEntry.Rows.Clear();
                DgBomsEntry.Columns["BomName"].Width = 100;
                DgBomsEntry.Columns["ItemNames"].Visible = true;
                DgBomsEntry.Columns["Unit"].Visible = true;
                DgBomsEntry.Columns["Type"].Visible = true;
                DgBomsEntry.Columns["Qty"].Visible = true;
                DgBomsEntry.Columns["TaxQty"].Visible = true;
                DgBomsEntry.Columns["Rate"].Visible = true;
                DgBomsEntry.Columns["Amount"].Visible = true;

                
            }
        }

        private void txtbomstyle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DgBomsEntry.Rows.Clear();
                change();
            }
            if (e.KeyCode == Keys.Enter)
            {
                DgBomsEntry.Rows.Clear();
                hidemethod();    
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtbomstyle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            change();
        }
        public void change()
        {
            if (txtbomstyle.Text.ToString().Trim() == "Normal")
            {
                txtbomstyle.Text = "Detail";
                hidemethod();
                detailsgrid();
            }
            else
            {
                txtbomstyle.Text = "Normal";
                hidemethod();
            }
        }

        public void normalgridload()
        {
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "SlectBOM");
            cmd.Parameters.AddWithValue("@itemName", "");
            cmd.Parameters.AddWithValue("ItemCode","");
            adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    DgBomsEntry.Rows[i].Cells["BomName"].Value = dt.Rows[i]["BOM_name"].ToString();
                    cmd = new SqlCommand("SP_SelectQuery", con);
                    DgBomsEntry.Rows[i].Cells["BOM_No"].Value = dt.Rows[i]["BOM_No"].ToString();

                    if (dt.Rows.Count > 20)
                    {
                        DgBomsEntry.Rows.Add();
                    }
                }
            }
        }
        DataTable dtgridload1 = new DataTable();
      //  bool ischk;
        public void detailsgrid()
        {
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType","SelectGridLoadValues");
            cmd.Parameters.AddWithValue("@itemName", "");
            cmd.Parameters.AddWithValue("ItemCode", "");
            adp = new SqlDataAdapter(cmd);
            dtgridload1.Rows.Clear();
            adp.Fill(dtgridload1);
            DgBomsEntry.Rows.Clear();
            if (dtgridload1.Rows.Count > 0)
            {
                for (int i = 0; i < dtgridload1.Rows.Count; i++)
                {
                        DgBomsEntry.Rows.Add();
                        DgBomsEntry.Rows[i].Cells["BomName"].Value = dtgridload1.Rows[i]["BOM_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["ItemNames"].Value = dtgridload1.Rows[i]["Item_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["Unit"].Value = dtgridload1.Rows[i]["unit_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["Type"].Value = dtgridload1.Rows[i]["Typess"].ToString();
                        DgBomsEntry.Rows[i].Cells["TaxQty"].Value = dtgridload1.Rows[i]["tx_Qty"].ToString() == "" || dtgridload1.Rows[i]["tx_Qty"].ToString() == null ? "0.00" : Convert.ToDouble(dtgridload1.Rows[i]["tx_Qty"].ToString()).ToString("0.00");

                        DgBomsEntry.Rows[i].Cells["Qty"].Value = dtgridload1.Rows[i]["nt_qty"].ToString() == "" || dtgridload1.Rows[i]["nt_qty"].ToString() ==null? "0.00" : Convert.ToDouble(dtgridload1.Rows[i]["nt_qty"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Rate"].Value = dtgridload1.Rows[i]["Rate"].ToString() == ""|| dtgridload1.Rows[i]["Rate"].ToString() ==null ? "0.00" : Convert.ToDouble(dtgridload1.Rows[i]["Rate"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Amount"].Value = dtgridload1.Rows[i]["Amount"].ToString() == ""|| dtgridload1.Rows[i]["Amount"].ToString() ==null ? "0.00" : Convert.ToDouble(dtgridload1.Rows[i]["Amount"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["BOM_No"].Value = dtgridload1.Rows[i]["BOM_No"].ToString();    
                }
            }
        }

        private void DgBomsEntry_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id_BOmNo = "";
             int rowIndex = e.RowIndex;
             if (DgBomsEntry.Rows[rowIndex].Cells[0].Value != null && DgBomsEntry.Rows[rowIndex].Cells[0].Value != "")
             {
                 id_BOmNo = Convert.ToString(DgBomsEntry.Rows[rowIndex].Cells["BOM_No"].Value);
                 pass();  
             }
        }
        string id_BOmNo = "";
        private void DgBomsEntry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //id_BOmNo = "";
            //if (DgBomsEntry.Rows.Count > 0)
            //{
            //    int rowIndex = e.RowIndex;
            //    if (DgBomsEntry.Rows[rowIndex].Cells[0].Value != null)
            //    {
            //        id_BOmNo = Convert.ToString(DgBomsEntry.Rows[rowIndex].Cells["BOM_No"].Value);
            //        pass();
            //    }
            //}
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            if (id_BOmNo != "" && id_BOmNo != null)
            {
                string result = MyMessageBox1.ShowBox("Are You Sure Want to Kill", "Message");
                if (result == "1")
                {
                    cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DeleteSelectedRow");
                    cmd.Parameters.AddWithValue("@itemName", id_BOmNo.ToString());
                    cmd.Parameters.AddWithValue("ItemCode", "");
                    cmd.ExecuteNonQuery();
                    change();
                }
            }
            else
            {
            }
        }
        public void pass()
        {
            if (DgBomsEntry.Rows.Count > 0)
            {

                passingvalues.BOMNO = id_BOmNo.ToString();
                    SalesBOM frm = new SalesBOM();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
               
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SalesBOM purentry = new SalesBOM();
            purentry.MdiParent = this.ParentForm;
            purentry.StartPosition = FormStartPosition.Manual;
            purentry.WindowState = FormWindowState.Normal;
            purentry.Location = new Point(0, 80);
            purentry.Show();
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceSales = new Microsoft.Reporting.WinForms.ReportDataSource();

        private void btnprint_Click(object sender, EventArgs e)
        {
           
            try
            {
                reportViewerSales.Reset();

                Dataset.DsBomissueDisplayMaster dsobj = new Dataset.DsBomissueDisplayMaster();
                for (int k = 0; k < DgBomsEntry.Rows.Count - 1; k++)
                {
                    if (DgBomsEntry.Rows[k].Cells[0].Value != null && !string.IsNullOrEmpty(DgBomsEntry.Rows[k].Cells[0].Value.ToString()))
                    {
                        dsobj.Tables["BomMasterDisplay"].Rows.Add(DgBomsEntry.Rows[k].Cells[0].Value.ToString(), DgBomsEntry.Rows[k].Cells[1].Value.ToString(), DgBomsEntry.Rows[k].Cells[2].Value.ToString(), DgBomsEntry.Rows[k].Cells[3].Value.ToString(), DgBomsEntry.Rows[k].Cells[4].Value.ToString(), DgBomsEntry.Rows[k].Cells[5].Value.ToString(), DgBomsEntry.Rows[k].Cells[6].Value.ToString(), DgBomsEntry.Rows[k].Cells[7].Value.ToString());
                    }
                }
                ReportDataSource ds = new ReportDataSource("DsBomissueDisplayMaster", dsobj.Tables["BomMasterDisplay"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RdlcMasterissuedisplay.rdlc";
                //Passing Parmetes:
                ReportParameter rp = new ReportParameter("Number", "200", false);

                //ReportParameter rp2 = new ReportParameter("DateTo", "300");

                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rp });
                // dt.EndInit();
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

        private void SalesBOMMasterAltertion_Load(object sender, EventArgs e)
        {
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        
    }
}
