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
using Forms = System.Windows.Forms;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;




namespace MSPOSBACKOFFICE
{
    public partial class FrmDailySalesForSalesMan : Form
    {
        public FrmDailySalesForSalesMan()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());

        decimal vTotalQty = 0;
        private void SetUpGrid()
        {
            for (int i = 0; i < 3; i++)
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
                else if (i == 2)
                {
                    colm.HeaderText = "Amount";
                    colm.Width = 100;
                }

                int colIndex = GridView.Columns.Add(colm);
            }

            SqlCommand cmdSearch = new SqlCommand("select distinct Ledger_table.Ledger_name,sum(a.SalRecv_Amt) as Amount " +
                                                  " from salmas_table,stktrn_table,Item_table,Ledger_table,SalRecv_table a where stktrn_table.strn_no=salmas_table.smas_no and stktrn_table.item_no = Item_table.Item_no " +
                                                  " and stktrn_table.SmanPer=Ledger_table.Ledger_no and stktrn_table.strn_type='1' and stktrn_table.Strn_Cancel=0 and stktrn_table.nt_qty<>stktrn_table.rnt_qty " +
                                                  " and stktrn_table.strn_rtno=0 and Ledger_table.Ledger_groupno='51' " +
                                                  " and Smas_Bill=a.SalRecv_Salno and smas_billdate=@FromDate " +
                                                  " group by Ledger_table.Ledger_name order by Ledger_table.Ledger_name ", con);
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

                int vSlno = 1;
                for (int i = 0; i < dtSearch.Rows.Count; i++)
                {
                    GridView.Rows.Add();
                    GridView.Rows[i].Cells[0].Value = vSlno;
                    GridView.Rows[i].Cells[1].Value = (dtSearch.Rows[i]["Ledger_name"].ToString().Trim()).ToUpper();
                    GridView.Rows[i].Cells[2].Value = dtSearch.Rows[i]["Amount"].ToString().Trim();
                    GridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    vSlno = vSlno + 1;
                }
                
                GridView.Rows[GridView.Rows.Count-1].Cells[1].Value = "Total ";
                this.GridView.Rows[GridView.Rows.Count - 1].Cells[1].Style.BackColor = Color.BlueViolet;
                this.GridView.Rows[GridView.Rows.Count - 1].Cells[1].Style.ForeColor = Color.Yellow;

                for (int t = 0; t < GridView.Rows.Count - 1; t++)
                {
                    vTotalQty = vTotalQty + Convert.ToDecimal(GridView.Rows[t].Cells[2].Value.ToString());
                }

                GridView.Rows[GridView.Rows.Count - 1].Cells[2].Value = vTotalQty;
                this.GridView.Rows[GridView.Rows.Count - 1].Cells[2].Style.BackColor = Color.BlueViolet;
                this.GridView.Rows[GridView.Rows.Count - 1].Cells[2].Style.ForeColor = Color.Yellow;
                lblTotalSales.Text = "Amount  $" + vTotalQty.ToString();
                vTotalQty = 0;
            }
        }

        private void FrmDailySalesForSalesMan_Load(object sender, EventArgs e)
        {
            SetUpGrid();
            this.GridView.ColumnHeadersDefaultCellStyle.Font = new Font(GridView.DefaultCellStyle.Font, FontStyle.Bold);
            //this.reportViewer1.RefreshReport();
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            // this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            GridView.Rows.Clear();
            GridView.Columns.Clear();
            SetUpGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            xlWorkSheet.Name = "SalesMan Amount";

            

                int cellRowIndex = 1;
                int cellColumnIndex = 1;
                //xlWorkSheet.Cells[cellRowIndex, cellColumnIndex] = "Daily Sales For Sales Man - Amount";
                xlWorkSheet.get_Range("a1", "c1").Merge(false);    
                chartRange = xlWorkSheet.get_Range("a1", "c1");
                
                chartRange.FormulaR1C1 = "Daily Sales For Sales Man - Amount";
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

                chartRange.FormulaR1C1 = "Date : " +dtpDate.Text;
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                chartRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                chartRange.Font.Name = "BankGothic Md BT";
                chartRange.Font.Size = 10;
                chartRange.Font.Bold = true;

                cellRowIndex++;
                cellColumnIndex=1;
                //xlWorkSheet.Cells[cellRowIndex, cellColumnIndex] = "Date : " +dtpDate.ToString();

                //Loop through each row and read value from each column. 
                for (int i = 0; i < GridView.Rows.Count + 1; i++)
                {
                    for (int j = 0; j < GridView.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 3)
                        {
                            xlWorkSheet.Cells[cellRowIndex, cellColumnIndex] = GridView.Columns[j].HeaderText;
                            chartRange.Font.Bold = true;
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

        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();

        private void btnSum_Click(object sender, EventArgs e)
        {
           
               try
            {
                //reportViewer1.Visible = true;
                //reportViewer1.Reset();
                //ReportDataSource ds = new ReportDataSource("DataSet1");
                //reportViewer1.LocalReport.DataSources.Add(ds);
                //reportViewer1.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.rdlcDailySalesForSalesManAmount.rdlc";
               
                //reportViewer1.RefreshReport();
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
