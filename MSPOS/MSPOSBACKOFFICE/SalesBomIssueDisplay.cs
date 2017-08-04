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
using System.Configuration;


//using iTextSharp.text;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;


namespace MSPOSBACKOFFICE
{
    public partial class SalesBomIssueDisplay : Form
    {
        public SalesBomIssueDisplay()
        {
            InitializeComponent();
            dgIssueDisplay.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            dgIssueDisplay.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

            dgIssueDisplay.BackgroundColor = Color.White;
            foreach (DataGridViewColumn col in dgIssueDisplay.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            dgIssueDisplay.ReadOnly = true;
            pnlcanceltype.Visible = false;
            //dtissueorder.Rows[i]["No"].ToString(), dtissueorder.Rows[i]["Date"].ToString(),BOMMas_Table.BOM_name ,dtissueorder.Rows[i]["Code"].ToString(), dtissueorder.Rows[i]["Name"].ToString(), dtissueorder.Rows[i]["Unit"].ToString(), dtissueorder.Rows[i]["Tax Qty"].ToString(), dtissueorder.Rows[0]["Nt Qty"].ToString(), dtissueorder.Rows[i]["Rate"].ToString(), dtissueorder.Rows[0]["Amount"].ToString(), "", "", dtissueorder.Rows[i]["Nt Qty"].ToString(), dtissueorder.Rows[i]["Amount"].ToString());


            dt.Columns.Add("No", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Bom Name", typeof(string));
            dt.Columns.Add("Issue Qty", typeof(string));

            //dtissueorder.Rows[i]["No"].ToString(), dtissueorder.Rows[i]["Date"].ToString(),BOMMas_Table.BOM_name ,dtissueorder.Rows[i]["Code"].ToString(), dtissueorder.Rows[i]["Name"].ToString(), dtissueorder.Rows[i]["Unit"].ToString(), dtissueorder.Rows[i]["Tax Qty"].ToString(), dtissueorder.Rows[0]["Nt Qty"].ToString(), dtissueorder.Rows[i]["Rate"].ToString(), dtissueorder.Rows[0]["Amount"].ToString(), "", "", dtissueorder.Rows[i]["Nt Qty"].ToString(), dtissueorder.Rows[i]["Amount"].ToString());
            dtDetails.Columns.Add("No", typeof(string));
            dtDetails.Columns.Add("Date", typeof(string));
            dtDetails.Columns.Add("BomName", typeof(string));
            dtDetails.Columns.Add("Code", typeof(string));
            dtDetails.Columns.Add("Name", typeof(string));
            dtDetails.Columns.Add("Unit", typeof(string));
            dtDetails.Columns.Add("Tax Qty", typeof(string));
            dtDetails.Columns.Add("Issued Qty", typeof(string));
            dtDetails.Columns.Add("Rate", typeof(string));
            dtDetails.Columns.Add("Amount", typeof(string));
            dtDetails.Columns.Add("In Qty", typeof(string));
            dtDetails.Columns.Add("In Amt", typeof(string));
            dtDetails.Columns.Add("Out Qty", typeof(string));
            dtDetails.Columns.Add("Out Amt", typeof(string));

        }
        DataTable dtDetails = new DataTable();
        SqlCommand cmd = null;
        SqlDataAdapter adp = null;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SalesBomIssueDisplay_Load(object sender, EventArgs e)
        {
            selectquery();

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        DataTable dt = new DataTable();
        
        public void selectquery()
        {
            if (txtType.Text.Trim() == "Normal")
            {
                cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ActionType", "BOmissuedisplay");
                adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    int k = 0;
                    k = Convert.ToInt16(dt.Rows.Count);
                    dt.Rows.Add("", "", "", "");
                    double totalqty = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][3].ToString() != "" && dt.Rows[i][3].ToString() != null)
                        {
                            totalqty += Convert.ToDouble(dt.Rows[i][3].ToString());
                        }
                    }
                    dt.Rows.Add("", "", "Total" + "(" + k.ToString() + ")", totalqty);
                    dgIssueDisplay.DataSource = dt.DefaultView;
                    dgIssueDisplay.Columns[0].Width = 100;
                    dgIssueDisplay.Columns[1].Width=200;
                    dgIssueDisplay.Columns[2].Width=800;
                    dgIssueDisplay.Columns[2].Width=175;
                    dgIssueDisplay.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    foreach (DataGridViewColumn col1 in dgIssueDisplay.Columns)
                    {
                        col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col1.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                    }
                }
            }
            else
            {
                SqlCommand cmd_bomissues = new SqlCommand("select * from BOMissu_Table where  bom_date between @sDate and @tDate", con);
                cmd_bomissues.Parameters.Add("@sDate", new DateTime(dtpFromdate.Value.Year, dtpFromdate.Value.Month, dtpFromdate.Value.Day));
                cmd_bomissues.Parameters.Add("@tDate", new DateTime(dtpTodate.Value.Year, dtpTodate.Value.Month, dtpTodate.Value.Day));
                SqlDataAdapter adp_bomissues = new SqlDataAdapter(cmd_bomissues);
                DataTable dt_issue = new DataTable();
                dt_issue.Rows.Clear();
                dtDetails.Rows.Clear();
                dt.Rows.Clear();
                adp_bomissues.Fill(dt_issue);
                if (dt_issue.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_issue.Rows.Count; i++)
                    {
                        //here only display issued qty:
                        SqlCommand cmd_selecqurey = new SqlCommand("select BOMissu_Table.BOM_BillNo As No,Convert(Varchar(10),BOMissu_Table.BOM_Date,111) As [Date],BOMMas_Table.BOM_name As [Bom Name],Item_table.Item_code As [Code],Item_table.Item_name as [Name],unit_table.unit_name As [Unit],(Convert(Numeric(18,2),bomMas_table.tx_Qty)) As [Tax Qty],bomMas_table.nt_qty As [Nt Qty],(Convert(Numeric(18,2),bomMas_table.Rate)) As [Rate],convert(Numeric(18,2),bomMas_table.Rate*bomMas_table.nt_qty) as [Amount] from BOMissu_Table,bomMas_table,Item_table,unit_table where BOMissu_Table.bom_no=bomMas_table.bom_no and Item_table.Item_no=bomMas_table.Item_No and unit_table.unit_no=bomMas_table.Unit_No and bomMas_table.Bom_No='" + dt_issue.Rows[i]["Bom_No"].ToString() + "' and bomMas_table .Type='1' and BOMissu_Table.BOM_BillNo='" + dt_issue.Rows[i]["BOM_BillNo"].ToString() + "'", con);
                        DataTable dtissueorder = new DataTable();
                        dtissueorder.Rows.Clear();
                        SqlDataAdapter adpissuedisplay = new SqlDataAdapter(cmd_selecqurey);
                        adpissuedisplay.Fill(dtissueorder);
                        if (dtissueorder.Rows.Count > 0)
                        {
                            //only issue qty:
                            dtDetails.Rows.Add(dtissueorder.Rows[0]["No"].ToString(), dtissueorder.Rows[0]["Date"].ToString(), dtissueorder.Rows[0]["Bom Name"].ToString(), dtissueorder.Rows[0]["Code"].ToString(), dtissueorder.Rows[0]["Name"].ToString(), dtissueorder.Rows[0]["Unit"].ToString(), dtissueorder.Rows[0]["Tax Qty"].ToString(), dtissueorder.Rows[0]["Nt Qty"].ToString(), dtissueorder.Rows[0]["Rate"].ToString(), dtissueorder.Rows[0]["Amount"].ToString(), "", "", dtissueorder.Rows[0]["Nt Qty"].ToString(), dtissueorder.Rows[0]["Amount"].ToString());
                        }
                        if (dtissueorder.Rows.Count > 0)
                        {
                            //here put this quere :   
                            SqlCommand cmd_ = new SqlCommand("select BOMissu_Table.BOM_BillNo As No,Convert(Varchar(10),BOMissu_Table.BOM_Date,111) As [Date],BOMMas_Table.BOM_name As [Bom Name],Item_table.Item_code As  [Code],Item_table.Item_name as [Name],unit_table.unit_name As [Unit],(Convert(Numeric(18,2),bomMas_table.tx_Qty)) As [Tax Qty],bomMas_table.nt_qty As [Nt Qty],(Convert(Numeric(18,2),bomMas_table.Rate)) As [Rate],convert(Numeric(18,2),bomMas_table.Rate*bomMas_table.nt_qty) as [Amount] from BOMissu_Table,bomMas_table,Item_table,unit_table where BOMissu_Table.bom_no=bomMas_table.bom_no and Item_table.Item_no=bomMas_table.Item_No and unit_table.unit_no=bomMas_table.Unit_No and bomMas_table.Bom_No='" + dt_issue.Rows[i]["Bom_No"].ToString() + "' and bomMas_table .Type<>1 and BOMissu_Table.BOM_BillNo='" + dt_issue.Rows[i]["BOM_BillNo"].ToString() + "'", con);
                            SqlDataAdapter adp_ = new SqlDataAdapter(cmd_);
                            // Issue Qty Remaining values added here 
                            DataTable dt_ = new DataTable();
                            dt_.Rows.Clear();
                            adp_.Fill(dt_);
                            if (dt_.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt_.Rows.Count; j++)
                                {
                                    dtDetails.Rows.Add("","", "", dt_.Rows[j]["Code"].ToString(), dt_.Rows[j]["Name"].ToString(), dt_.Rows[j]["Unit"].ToString(), dt_.Rows[j]["Tax Qty"].ToString(), dt_.Rows[j]["Nt Qty"].ToString(), dt_.Rows[j]["Rate"].ToString(), dt_.Rows[j]["Amount"].ToString(), dt_.Rows[j]["Nt Qty"].ToString(), dt_.Rows[j]["Amount"].ToString(), "", "");
                                }
                            }
                        }
                        else
                        {
                            SqlCommand cmd_ = new SqlCommand("select BOMissu_Table.BOM_BillNo As No,Convert(Varchar(10),BOMissu_Table.BOM_Date,111) As [Date],BOMMas_Table.BOM_name As [Bom Name],Item_table.Item_code As  [Code],Item_table.Item_name as [Name],unit_table.unit_name As [Unit],(Convert(Numeric(18,2),bomMas_table.tx_Qty)) As [Tax Qty],bomMas_table.nt_qty As [Nt Qty],(Convert(Numeric(18,2),bomMas_table.Rate)) As [Rate],convert(Numeric(18,2),bomMas_table.Rate*bomMas_table.nt_qty) as [Amount] from BOMissu_Table,bomMas_table,Item_table,unit_table where BOMissu_Table.bom_no=bomMas_table.bom_no and Item_table.Item_no=bomMas_table.Item_No and unit_table.unit_no=bomMas_table.Unit_No and bomMas_table.Bom_No='" + dt_issue.Rows[i]["Bom_No"].ToString() + "' and bomMas_table .Type<>1 and BOMissu_Table.BOM_BillNo='" + dt_issue.Rows[i]["BOM_BillNo"].ToString() + "'", con);
                            SqlDataAdapter adp_ = new SqlDataAdapter(cmd_);
                            // Issue Qty Remaining values added here 
                            DataTable dt_ = new DataTable();
                            dt_.Rows.Clear();
                            adp_.Fill(dt_);
                            if (dt_.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt_.Rows.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        dtDetails.Rows.Add(dt_.Rows[j]["No"].ToString(), dt_.Rows[j]["Date"].ToString(), dt_.Rows[j]["BOM Name"].ToString(), dt_.Rows[j]["Code"].ToString(), dt_.Rows[j]["Name"].ToString(), dt_.Rows[j]["Unit"].ToString(), dt_.Rows[j]["Tax Qty"].ToString(), dt_.Rows[j]["Nt Qty"].ToString(), dt_.Rows[j]["Rate"].ToString(), dt_.Rows[j]["Amount"].ToString(), dt_.Rows[j]["Nt Qty"].ToString(), dt_.Rows[j]["Amount"].ToString(), "", "");
                                    }
                                    else
                                    {
                                        dtDetails.Rows.Add("", "", "", dt_.Rows[j]["Code"].ToString(), dt_.Rows[j]["Name"].ToString(), dt_.Rows[j]["Unit"].ToString(), dt_.Rows[j]["Tax Qty"].ToString(), dt_.Rows[j]["Nt Qty"].ToString(), dt_.Rows[j]["Rate"].ToString(), dt_.Rows[j]["Amount"].ToString(), dt_.Rows[j]["Nt Qty"].ToString(), dt_.Rows[j]["Amount"].ToString(), "", "");
                                    }
                                }
                            }
                        }
                    }
                    //Calcuation Values Addedd Here :
                    // dtDetails.Rows.Add("", "", "", "", "", "", "", "", "", "Calculate_nt_qty", "Calculate_nt_Amount", "Calculate_out_qty", "Calculate_out_amount");
                    
                    dtDetails.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "");

                    double In_qty=0.00,In_Amt=0.00,Out_Aty=0.00,Out_Amt=0.00;
                    for (int k = 0; k < dtDetails.Rows.Count; k++)
                    {
                        if ((dtDetails.Rows[k][10].ToString() != null) && (dtDetails.Rows[k][10].ToString() != ""))
                        {
                            In_Amt += Convert.ToDouble(dtDetails.Rows[k][10].ToString());
                        }
                        if ((dtDetails.Rows[k][11].ToString() != null) && (dtDetails.Rows[k][11].ToString() != ""))
                        {
                            In_qty += Convert.ToDouble(dtDetails.Rows[k][11].ToString());
                        }
                        if ((dtDetails.Rows[k][12].ToString() != null) && (dtDetails.Rows[k][12].ToString() != ""))
                        {
                            Out_Aty += Convert.ToDouble(dtDetails.Rows[k][12].ToString());
                        }
                        if ((dtDetails.Rows[k][13].ToString() != null) && (dtDetails.Rows[k][13].ToString() != ""))
                        {
                            Out_Amt += Convert.ToDouble(dtDetails.Rows[k][13].ToString());
                        }
                    }
                    dtDetails.Rows.Add("", "", "", "", "", "Total" +"("+Convert.ToInt16(dt_issue.Rows.Count)+")", "", "", "", "", In_Amt.ToString("0.00"), In_qty.ToString("0.00"), Out_Aty.ToString("0.00"), Out_Amt.ToString("0.00"));
                    dgIssueDisplay.DataSource = dtDetails.DefaultView;
                    dgIssueDisplay.Columns[3].Width = 150;
                    dgIssueDisplay.Columns[4].Width = 200;
                    dgIssueDisplay.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgIssueDisplay.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    foreach (DataGridViewColumn col1 in dgIssueDisplay.Columns)
                    {
                        col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col1.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                    }
                }
            }
        }
        private void txtcancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab)
            {
                if (lstcanceltype.SelectedItems.Count > 0)
                {
                    txtcancel.Text = lstcanceltype.SelectedItem.ToString();
                }
                txtType.Focus();
                pnlcanceltype.Visible = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                if (lstcanceltype.SelectedIndex < lstcanceltype.Items.Count - 1)
                {
                    lstcanceltype.SetSelected(lstcanceltype.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstcanceltype.SelectedIndex > 0)
                {
                    lstcanceltype.SetSelected(lstcanceltype.SelectedIndex - 1, true);
                }
            }
        }
        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtType.Text == "Normal")
                {
                    txtType.Text = "Detail";
                }
                else
                {
                    txtType.Text = "Normal";
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                dgIssueDisplay.Select();
                pnlcanceltype.Visible = false;
                selectquery();
                txtType.Focus();
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            SalesBOMIssueCreation purentry = new SalesBOMIssueCreation();
            purentry.MdiParent = this.ParentForm;
            purentry.StartPosition = FormStartPosition.Manual;
            purentry.WindowState = FormWindowState.Normal;
            purentry.Location = new Point(0, 80);
            purentry.Show();
        }

        private void txtcancel_Enter(object sender, EventArgs e)
        {
            pnlcanceltype.Visible = true;
        }

        private void lstcanceltype_Click(object sender, EventArgs e)
        {
            if (lstcanceltype.SelectedItems.Count > 0)
            {
                txtcancel.Text = lstcanceltype.SelectedItem.ToString();
            }
        }

        private void dgIssueDisplay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dgIssueDisplay.Rows.Count > 0)
            {
                string st1 = "";
                if (txtType.Text == "Normal")
                {
                    if (dgIssueDisplay.Rows[e.RowIndex].Cells["Bom Name"].Value != null && dgIssueDisplay.Rows[e.RowIndex].Cells["Bom Name"].Value != "")
                    {
                        st1 = dgIssueDisplay.Rows[e.RowIndex].Cells[0].Value.ToString();

                       
                    }
                }
                else if (dgIssueDisplay.Rows[e.RowIndex].Cells["Name"].Value != null && dgIssueDisplay.Rows[e.RowIndex].Cells["Name"].Value != "")
                {
                    int i = e.RowIndex;
                    
                    for (int j = 0;i>=0; j++)
                    {
                        if (dgIssueDisplay.Rows[i].Cells["No"].Value != null && dgIssueDisplay.Rows[i].Cells["No"].Value != "")
                        {
                            st1 = dgIssueDisplay.Rows[i].Cells["No"].Value.ToString();
                            break;
                        }
                        i=--i;
                    }
                }
                pass(st1);
            }
        }
        public void pass(string str2)
        {
            if (str2.ToString().Trim()!="")
            {
                passingvalues.BOMNO = str2.ToString();
                SalesBOM frm = new SalesBOM();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceSales = new Microsoft.Reporting.WinForms.ReportDataSource();
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewerSales.Reset();
                Dataset.DsPurchaseNormal dsobj = new Dataset.DsPurchaseNormal();
                for (int k = 0; k < dgIssueDisplay.Rows.Count - 1; k++)
                {
                    dsobj.Tables["BomIssueDispalay"].Rows.Add(dgIssueDisplay.Rows[k].Cells[0].Value.ToString(), dgIssueDisplay.Rows[k].Cells[1].Value.ToString(), dgIssueDisplay.Rows[k].Cells[2].Value.ToString(), dgIssueDisplay.Rows[k].Cells[3].Value.ToString(), dgIssueDisplay.Rows[k].Cells[4].Value.ToString(), dgIssueDisplay.Rows[k].Cells[5].Value.ToString(), dgIssueDisplay.Rows[k].Cells[6].Value.ToString(), dgIssueDisplay.Rows[k].Cells[7].Value.ToString(), dgIssueDisplay.Rows[k].Cells[8].Value.ToString(), dgIssueDisplay.Rows[k].Cells[9].Value.ToString(), dgIssueDisplay.Rows[k].Cells[10].Value.ToString(), dgIssueDisplay.Rows[k].Cells[11].Value.ToString(), dgIssueDisplay.Rows[k].Cells[12].Value.ToString(), dgIssueDisplay.Rows[k].Cells[13].Value.ToString());
                }
                ReportDataSource ds = new ReportDataSource("DsPurchaseNormal", dsobj.Tables["BomIssueDispalay"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RdlcIssueDisplay.rdlc";
                //Passing Parmetes:
                string DateF = (Convert.ToString(dtpFromdate.Value.Day) + "/" + (dtpFromdate.Value.Month) + "/" + (dtpFromdate.Value.Year));
                string DateFT = (Convert.ToString(dtpTodate.Value.Day) + "/" + (dtpTodate.Value.Month) + "/" + (dtpTodate.Value.Year));
                ReportParameter rp = new ReportParameter("Number", "200", false);
                ReportParameter FDate = new ReportParameter("FDate", DateF, false);
                ReportParameter ToDate = new ReportParameter("ToDate", DateFT, false);
                ReportParameter Cancel = new ReportParameter("Cancel", txtcancel.Text.Trim(), false);
                ReportParameter Type = new ReportParameter("Type", txtType.Text.Trim(), false);
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

        private void dtpFromdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpTodate.Focus();
            }
        }

        private void dtpTodate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcancel.Focus();
            }
        }
    }
}
