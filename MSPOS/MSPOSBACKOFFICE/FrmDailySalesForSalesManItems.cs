using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Configuration;
using System.Text.RegularExpressions;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;


namespace MSPOSBACKOFFICE
{
    public partial class FrmDailySalesForSalesManItems : Form
    {
        public FrmDailySalesForSalesManItems()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());

        decimal vTotalQty = 0;
        bool ColCreated = false;
        private void SetUpGrid()
        {
            SqlCommand cmdSearch = new SqlCommand("Select distinct Item_table.Item_Code " +
                                                " from salmas_table,stktrn_table,Item_table,Ledger_table,SalRecv_table a where stktrn_table.strn_no=salmas_table.smas_no and stktrn_table.item_no = Item_table.Item_no " +
                                                " and stktrn_table.SmanPer=Ledger_table.Ledger_no and stktrn_table.strn_type='1' and stktrn_table.Strn_Cancel=0 and stktrn_table.nt_qty<>stktrn_table.rnt_qty " +
                                                " and stktrn_table.strn_rtno=0 and Ledger_table.Ledger_groupno='51' " +
                                                " and Smas_Bill=a.SalRecv_Salno and smas_billdate=@FromDate " +
                                                " order by Item_Code ", con);
            cmdSearch.Parameters.AddWithValue("@FromDate", dtpDate.Value.Year + "/" + dtpDate.Value.Month + "/" + dtpDate.Value.Day);
            DataTable dtSearch = new DataTable();
            dtSearch.Rows.Clear();
            SqlDataAdapter adpSearch = new SqlDataAdapter(cmdSearch);
            adpSearch.Fill(dtSearch);
            if (dtSearch.Rows.Count > 0)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                for (int k = 0; k < dtSearch.Rows.Count; k++)
                {
                    DataGridViewColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = (dtSearch.Rows[k]["Item_Code"].ToString()).ToUpper();
                    col.Width = 100;
                    if (k == dtSearch.Rows.Count)
                    {
                        ColCreated = true;
                    }
                    else
                    {
                        int colIndex1 = GridView.Columns.Add(col);
                        ColCreated = true;
                    }
                }
             }

            SqlCommand cmdSalesman = new SqlCommand("select distinct Ledger_table.Ledger_name " +
                                                  " from salmas_table,stktrn_table,Item_table,Ledger_table,SalRecv_table a where stktrn_table.strn_no=salmas_table.smas_no and stktrn_table.item_no = Item_table.Item_no " +
                                                  " and stktrn_table.SmanPer=Ledger_table.Ledger_no and stktrn_table.strn_type='1' and stktrn_table.Strn_Cancel=0 and stktrn_table.nt_qty<>stktrn_table.rnt_qty " +
                                                  " and stktrn_table.strn_rtno=0 and Ledger_table.Ledger_groupno='51' " +
                                                  " and Smas_Bill=a.SalRecv_Salno and smas_billdate=@FromDate " +
                                                  " order by Ledger_table.Ledger_name ", con);
            cmdSalesman.Parameters.AddWithValue("@FromDate", dtpDate.Value.Year + "/" + dtpDate.Value.Month + "/" + dtpDate.Value.Day);
            DataTable dtSalesman = new DataTable();
            dtSalesman.Rows.Clear();
            SqlDataAdapter adpSalesman = new SqlDataAdapter(cmdSalesman);
            adpSalesman.Fill(dtSalesman);
            if (dtSalesman.Rows.Count > 0)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int vSlno = 1;
                for (int r = 0; r < dtSalesman.Rows.Count; r++)
                {
                    GridView.Rows.Add();
                    GridView.Rows[r].Cells[0].Value = vSlno;
                    GridView.Rows[r].Cells[1].Value = (dtSalesman.Rows[r]["Ledger_name"].ToString().Trim()).ToUpper();
                    GridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    vSlno = vSlno + 1;
                }

                GridView.Rows[GridView.Rows.Count - 1].Cells[1].Value = "Total ";
                this.GridView.Rows[GridView.Rows.Count - 1].Cells[1].Style.BackColor = Color.BlueViolet;
                this.GridView.Rows[GridView.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Yellow;
            }
         }

        private void FrmDailySalesForSalesManItems_Load(object sender, EventArgs e)
        {
            ColmCreate();
            SetUpGrid();
            LoadValuesGrid();
            CalculTotQty();
            this.GridView.ColumnHeadersDefaultCellStyle.Font = new Font(GridView.DefaultCellStyle.Font, FontStyle.Bold);

            //For Color settings
            _Class.clsVariables.Sheight_Width();
           // this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            
        }

        public void ColmCreate()
        {
            for (int i = 0; i < 2; i++)
            {
                DataGridViewColumn colm = new DataGridViewTextBoxColumn();
                if (i == 0)
                {
                    colm.HeaderText = "Sl.No";
                    colm.Width = 60;
                }
                else if (i == 1)
                {
                    colm.HeaderText = "SalesMan";
                    colm.Width = 200;
                }
                int colIndex = GridView.Columns.Add(colm);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            GridView.Rows.Clear();
            GridView.Columns.Clear();
            ColmCreate();
            SetUpGrid();
            LoadValuesGrid();
            CalculTotQty();
            this.GridView.ColumnHeadersDefaultCellStyle.Font = new Font(GridView.DefaultCellStyle.Font, FontStyle.Bold);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int Row = 0;
        public void CalculTotQty()
        {
            for (int c = 2; c < GridView.Columns.Count; c++)
            {
                for (int i = 0; i < GridView.Rows.Count - 1; i++)
                {
                    int Value = GridView.Rows[i].Cells[c].Value == null ? 0 : Convert.ToInt32(GridView.Rows[i].Cells[c].Value.ToString());
                    if (Value != 0)
                    {
                        vTotalQty = vTotalQty + Convert.ToDecimal(GridView.Rows[i].Cells[c].Value.ToString());
                    }
                    else
                    {

                    }
                    Row = i;
                }

                GridView.Rows[Row+1].Cells[c].Value = vTotalQty;
                this.GridView.Rows[Row+1].Cells[c].Style.BackColor = Color.BlueViolet;
                this.GridView.Rows[Row+1].Cells[c].Style.ForeColor = Color.Yellow;
                vTotalQty = 0;
            }
        }

        private void LoadValuesGrid()
        {
            string GridItemCode = "";
            string GridLedgername = "";
            string DBItemCode = "";
            string DBLedgername = "";

            SqlCommand cmdRows = new SqlCommand("select Item_table.Item_Code,Sum(stktrn_table.nt_qty- stktrn_table.rnt_qty)  as Qty,Ledger_table.Ledger_name " +
                                                " from salmas_table,stktrn_table,Item_table,Ledger_table,SalRecv_table a where stktrn_table.strn_no=salmas_table.smas_no and stktrn_table.item_no = Item_table.Item_no " +
                                                " and stktrn_table.SmanPer=Ledger_table.Ledger_no and stktrn_table.strn_type='1' and stktrn_table.Strn_Cancel=0 and stktrn_table.nt_qty<>stktrn_table.rnt_qty " +
                                                " and stktrn_table.strn_rtno=0 and Ledger_table.Ledger_groupno='51'" +
                                                " and Smas_Bill=a.SalRecv_Salno and smas_billdate=@FromDate " +
                                                " group by Item_table.Item_Code,Ledger_table.Ledger_name order by Item_Code ", con);
            cmdRows.Parameters.AddWithValue("@FromDate", dtpDate.Value.Year + "/" + dtpDate.Value.Month + "/" + dtpDate.Value.Day);

            DataTable dtLoadValues = new DataTable();
            dtLoadValues.Rows.Clear();
            SqlDataAdapter adpRows = new SqlDataAdapter(cmdRows);
            adpRows.Fill(dtLoadValues);
            if (dtLoadValues.Rows.Count > 0)
            {
                for (int r = 0; r < dtLoadValues.Rows.Count; r++)
                {
                    DBItemCode = dtLoadValues.Rows[r]["Item_Code"].ToString().Trim();
                    DBLedgername = dtLoadValues.Rows[r]["Ledger_name"].ToString().Trim();

                    for (int l = 0; l < GridView.Columns.Count; l++)
                    {
                        GridItemCode = (GridView.Columns[l].HeaderText);
                        if (GridItemCode == DBItemCode)
                        {
                            for (int i = 0; i < GridView.Rows.Count; i++)
                            {
                                GridLedgername = (GridView.Rows[i].Cells[1].Value.ToString());
                                if (GridLedgername == DBLedgername.ToUpper())
                                {
                                    GridView.Rows[i].Cells[l].Value = dtLoadValues.Rows[r]["Qty"].ToString().Trim();
                                    GridView.Columns[l].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    
                                    goto label1;
                                }
                            }
                        }
                    }
                label1: r = r + 0;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range chartRange;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = "SalesMan Item";
            
            int cellRowIndex = 1;
            int cellColumnIndex = 1;

            xlWorkSheet.get_Range("a1", "c1").Merge(false);
            chartRange = xlWorkSheet.get_Range("a1", "c1");

            chartRange.FormulaR1C1 = "Daily Sales For Sales Man - Item";
            chartRange.HorizontalAlignment = 3;
            chartRange.VerticalAlignment = 3;
            chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
            chartRange.Font.Name = "BankGothic Md BT";
            chartRange.Font.Size = 12;
            chartRange.Font.Bold = true;

            cellRowIndex++;
            cellColumnIndex++;

            xlWorkSheet.get_Range("a2", "c2").Merge(false);
            chartRange = xlWorkSheet.get_Range("a2", "c2");

            chartRange.FormulaR1C1 = "Date : " + dtpDate.Text;
            chartRange.HorizontalAlignment = 3;
            chartRange.VerticalAlignment = 3;
            chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
            chartRange.Font.Name = "BankGothic Md BT";
            chartRange.Font.Size = 10;
            chartRange.Font.Bold = true;

            cellRowIndex++;
            cellColumnIndex = 1;
            //Loop through each row and read value from each column. 
            for (int i = 0; i < GridView.Rows.Count + 1; i++)
            {
                for (int j = 0; j < GridView.Columns.Count; j++)
                {
                    // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                    if (cellRowIndex == 3)
                    {
                        xlWorkSheet.Cells[cellRowIndex, cellColumnIndex] = GridView.Columns[j].HeaderText;
                    }
                    else
                    {
                        string GridValue = (GridView.Rows[i - 1].Cells[j].Value == null ? "" : (Convert.ToString(GridView.Rows[i - 1].Cells[j].Value)));
                        if (GridValue != "")
                        {
                            xlWorkSheet.Cells[cellRowIndex, cellColumnIndex] = GridValue;
                        }
                        else
                        {
                            xlWorkSheet.Cells[cellRowIndex, cellColumnIndex] = "";
                        }
                    }
                    cellColumnIndex++;
                }
                cellColumnIndex = 1;
                cellRowIndex++;
            }
            //Getting the location and file name of the excel to save from user. 
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;

            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xlWorkSheet.SaveAs(saveDialog.FileName);
                MessageBox.Show("Export Successful");
            }
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            DataTable FrmGrid = new DataTable();
            foreach (DataGridViewColumn col in GridView.Columns)
            {
                FrmGrid.Columns.Add(col.HeaderText);
            }

            // Data cells
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                DataGridViewRow row = GridView.Rows[i];
                DataRow dr = FrmGrid.NewRow();
                for (int j = 0; j < GridView.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                FrmGrid.Rows.Add(dr);
            }

            ReportDataSource rds = new ReportDataSource();
            ReportViewer1.LocalReport.ReportPath = string.Empty;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            //ReportViewer1.LocalReport.LoadReportDefinition(FrmGrid); 
        }
    }
}
