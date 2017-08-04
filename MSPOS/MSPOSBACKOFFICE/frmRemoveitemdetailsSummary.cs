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
    public partial class frmRemoveitemdetailsSummary : Form
    {
        public frmRemoveitemdetailsSummary()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        private void frmRemoveitemdetailsSummary_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime year = DateTime.Now;
                int Cuurentyear = Convert.ToInt16(year.Year.ToString());


                string currentmonth = chkbox.MonthName;
                int month = DateTime.Parse("1." + currentmonth + Cuurentyear).Month;

                DataTable dtnew = new DataTable();
                dtnew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select CONVERT(DATE,DATEADD(month," + month + "-1,DATEADD(year," + Cuurentyear + "-1900,0)),103) as tStartDate,CONVERT(DATE,DATEADD(day,-1,DATEADD(month," + month + ",DATEADD(year," + Cuurentyear + "-1900,0))),103) as tEndDate", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtnew);
                if (dtnew.Rows.Count > 0)
                {
                    From_date.Value = DateTime.Parse(dtnew.Rows[0]["tStartDate"].ToString());
                    To_date.Value = DateTime.Parse(dtnew.Rows[0]["tEndDate"].ToString());
                }

                DataTable dtdate1 = new DataTable();
                dtdate1.Rows.Clear();
                // SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
                SqlCommand cmddate1 = new SqlCommand("select Item_table.Item_name as ItemName,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty as QTY,RemoveItemDetail_table.Tot_Amt as Amount from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No and  RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
                cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
                cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
                SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
                adpdate1.Fill(dtdate1);
                Detailsgrid.DataSource = dtdate1;

                //DataTable dtitemno = new DataTable();
                //dtitemno.Rows.Clear();
                //SqlCommand cmditem = new SqlCommand("select item_no from  RemoveItemDetail_table", con);
                //SqlDataAdapter adpdate = new SqlDataAdapter(cmditem);
                //adpdate.Fill(dtitemno);
                //for (int i = 0; dtitemno.Rows.Count > i; i++)
                //{
                //    txtitemname.Text = dtitemno.Rows[i]["item_no"].ToString();
                //}

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string chkItemname="";
        string itemnum = "";
        string chk = "";
       // SqlDataReader dreader = null;
        
     
        private void txtitemname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pnlUserName.Visible = true;
                if (txtitemname.Text.Trim() != null && txtitemname.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    DataTable dtdate = new DataTable();
                    dtdate.Rows.Clear();
                    SqlCommand cmddate = new SqlCommand("select Item_table.Item_name as ItemName,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty as QTY,RemoveItemDetail_table.Tot_Amt as Amount from Item_table,RemoveItemDetail_table where  Item_table.Item_no =RemoveItemDetail_table.Item_No and Item_table.Item_name like @titemname ", con);
                    // SqlCommand cmddate = new SqlCommand("Select Item_Name from Item_table where Item_name like '" + txtitemname.Text.Trim() + "%'", con);
                    //SqlCommand cmddate = new SqlCommand("Select Item_Name from Item_table where Item_name like '" + txtitemname.Text.Trim() + "%'", con);
                    cmddate.Parameters.AddWithValue("@titemname", txtitemname.Text + '%');
                    SqlDataAdapter adpdate = new SqlDataAdapter(cmddate);
                    adpdate.Fill(dtdate);

                    chkItemname = txtitemname.Text.Trim();
                    DataTable dtnew = new DataTable();
                    dtnew.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("select item_no from item_table where item_name = @chkitemname", con);
                    cmd.Parameters.AddWithValue("@chkitemname", chkItemname);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtnew);
                    if (dtnew.Rows.Count > 0)
                    {
                        itemnum = dtnew.Rows[0]["item_no"].ToString();
                    }

                    DataTable dtdate2 = new DataTable();
                    dtdate2.Rows.Clear();
                    SqlCommand cmddate2 = new SqlCommand("select Item_table.Item_name as ItemName,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty as QTY,RemoveItemDetail_table.Tot_Amt as Amount from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No and RemoveItemDetail_table.Item_no= @titemnum", con);
                    //SqlCommand cmddate2 = new SqlCommand("select * from RemoveItemDetail_table where Item_no = '" + itemnum + "'", con);
                    cmddate2.Parameters.AddWithValue("@titemnum", itemnum);
                    SqlDataAdapter adpdate2 = new SqlDataAdapter(cmddate2);
                    adpdate2.Fill(dtdate2);
                    Detailsgrid.DataSource = dtdate2;


                    bool isChk = false;
                    for (int mn = 0; mn < dtdate.Rows.Count; mn++)
                    {
                        isChk = true;
                        string tempStr = dtdate.Rows[mn]["ItemName"].ToString();
                        for (int i = 0; i < ItemName.Items.Count; i++)
                        {
                            if (dtdate.Rows[mn]["ItemName"].ToString() == ItemName.Items[i].ToString())
                            {

                                ItemName.SetSelected(i, true);
                                txtitemname.Select();
                                chk = "1";
                                txtitemname.KeyPress += new KeyPressEventHandler(ItemName_KeyPress);
                                break;
                            }
                        }
                    }
                    con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txtitemname.KeyPress += new KeyPressEventHandler(ItemName_KeyPress);
                    }
                }
                else
                {
                    chk = "1";
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {
            //if (txtitemname.Text == "")
            //{
                
            //    DataTable dtdate1 = new DataTable();
            //    dtdate1.Rows.Clear();
            //    //SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
            //    SqlCommand cmddate1 = new SqlCommand("select Item_table.Item_name,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty,RemoveItemDetail_table.Tot_Amt from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No and  RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
            //    cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
            //    cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
            //    SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
            //    adpdate1.Fill(dtdate1);
            //    Detailsgrid.DataSource = dtdate1;
            //}

            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (ItemName.SelectedIndex < ItemName.Items.Count - 1)
                    {
                        ItemName.SetSelected(ItemName.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (ItemName.SelectedIndex > 0)
                    {
                        ItemName.SetSelected(ItemName.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (ItemName.Items.Count > 0)
                    {
                        if (ItemName.SelectedItems.Count > 0)
                        {
                            txtitemname.Text = ItemName.SelectedItem.ToString();
                        }
                        pnlUserName.Visible = false;
                        // btnSave.Focus();
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    pnlUserName.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

            //DataTable dtdate1 = new DataTable();
            //dtdate1.Rows.Clear();
            //SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
            //cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
            //cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
            //SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
            //adpdate1.Fill(dtdate1);
            //Detailsgrid.DataSource = dtdate1;
        }
   
        private void txtitemname_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtitemname.Text == "")
                {

                    DataTable dtdate1 = new DataTable();
                    dtdate1.Rows.Clear();
                    //SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
                    SqlCommand cmddate1 = new SqlCommand("select Item_table.Item_name as ItemName,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty as QTY,RemoveItemDetail_table.Tot_Amt as Amount from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No and  RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
                    cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
                    cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
                    SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
                    adpdate1.Fill(dtdate1);

                    Detailsgrid.DataSource = dtdate1;

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           

            //pnlUserName.Visible = true;
            //try
            //{
            //    SqlDataAdapter adp = new SqlDataAdapter("select Item_table.Item_name,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty,RemoveItemDetail_table.Tot_Amt from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No ", con);
            //   // SqlDataAdapter adp = new SqlDataAdapter("Select Item_name from  Item_table", con);
            //    DataTable dt = new DataTable();
            //    dt.Rows.Clear();
            //    ItemName.Items.Clear();
            //    adp.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            ItemName.Items.Add(dt.Rows[i]["Item_name"]);
            //        }
            //    }
          

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
           
        }
        
        private void txtitemname_Click(object sender, EventArgs e)
        {
            try
            {
                pnlUserName.Visible = true;

                DataTable dtdate1 = new DataTable();
                dtdate1.Rows.Clear();
                ItemName.Items.Clear();
                //SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
                SqlCommand cmddate1 = new SqlCommand("select Item_table.Item_name as ItemName,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty as QTY,RemoveItemDetail_table.Tot_Amt as Amount from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No and  RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
                cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
                cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
                SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
                adpdate1.Fill(dtdate1);

                if (dtdate1.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdate1.Rows.Count; i++)
                    {
                        ItemName.Items.Add(dtdate1.Rows[i]["ItemName"]);
                    }
                }

                //try
                //{
                //    SqlDataAdapter adp = new SqlDataAdapter("select Item_table.Item_name,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty,RemoveItemDetail_table.Tot_Amt from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No", con);
                //    //SqlDataAdapter adp = new SqlDataAdapter("Select Item_name from  Item_table", con);
                //    DataTable dt = new DataTable();
                //    dt.Rows.Clear();
                //    ItemName.Items.Clear();
                //    adp.Fill(dt);
                //    if (dt.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dt.Rows.Count; i++)
                //        {
                //            ItemName.Items.Add(dt.Rows[i]["Item_name"]);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void To_date_Leave(object sender, EventArgs e)
        {

        }

        private void To_date_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void To_date_Enter(object sender, EventArgs e)
        {

        }

        private void To_date_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void To_date_ValueChanged(object sender, EventArgs e)
        {
            //DataTable dtdate1 = new DataTable();
            //dtdate1.Rows.Clear();
            //// SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
            //SqlCommand cmddate1 = new SqlCommand("select Item_table.Item_name as ItemName,RemoveItemDetail_table.Date,RemoveItemDetail_table.Rate,RemoveItemDetail_table.nt_Qty as QTY,RemoveItemDetail_table.Tot_Amt as Amount from Item_table,RemoveItemDetail_table where Item_table.Item_no =RemoveItemDetail_table.Item_No and  RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
            //cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
            //cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
            //SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
            //adpdate1.Fill(dtdate1);
            //Detailsgrid.DataSource = dtdate1;
        }

        private void To_date_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void txtitemname_Leave(object sender, EventArgs e)
        {          
            //DataTable dtdate1 = new DataTable();
            //dtdate1.Rows.Clear();
            //SqlCommand cmddate1 = new SqlCommand("select * from RemoveItemDetail_table where RemoveItemDetail_table.Date between @tfromdate and @ttodate", con);
            //cmddate1.Parameters.AddWithValue("@tfromdate", From_date.Value);
            //cmddate1.Parameters.AddWithValue("@ttodate", To_date.Value);
            //SqlDataAdapter adpdate1 = new SqlDataAdapter(cmddate1);
            //adpdate1.Fill(dtdate1);
            //Detailsgrid.DataSource = dtdate1;
        }

        private void To_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtitemname.Select();
            }
        }

        private void From_date_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                To_date.Select();
               
            }
        }

        private void ItemName_Click(object sender, EventArgs e)
        {
            txtitemname.Text = ItemName.Text.Trim();
           
            pnlUserName.Visible = false;
           
        }

        private void txtitemname_MouseLeave(object sender, EventArgs e)
        {

           
            
        }

        private void frmRemoveitemdetailsSummary_Click(object sender, EventArgs e)
        {
            pnlUserName.Visible = false;
        }

        private void frmRemoveitemdetailsSummary_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        private void To_date_CloseUp(object sender, EventArgs e)
        {

        }

        private void ItemName_SelectedIndexChanged(object sender, EventArgs e)
        {

            

        }

        private void ItemName_KeyPress(object sender, KeyPressEventArgs e)
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

                //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                //{
                //    e.Handled = true;
                //}
                //// allow one decimal point
                //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                //{
                //    e.Handled = true;
                //}
            }
        }
        //RemoveItemDetail_table.ModeType='Return'
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private DataTable getDate()
        {

            DataTable _dt = new DataTable();
            _dt.Columns.Add("ItemName");
            _dt.Columns.Add("Date");
            _dt.Columns.Add("Rate");
            _dt.Columns.Add("QTY");
            _dt.Columns.Add("Amount");
            for (int i = 0; i < Detailsgrid.Rows.Count - 1; i++)
            {
                _dt.Rows.Add(Detailsgrid.Rows[i].Cells["ItemName"].Value.ToString(),(Convert.ToDateTime( Detailsgrid.Rows[i].Cells["Date"].Value).Day+"/"+Convert.ToDateTime( Detailsgrid.Rows[i].Cells["Date"].Value).Month+"/"+Convert.ToDateTime( Detailsgrid.Rows[i].Cells["Date"].Value).Year), Detailsgrid.Rows[i].Cells["Rate"].Value.ToString(), Detailsgrid.Rows[i].Cells["QTY"].Value.ToString(), Detailsgrid.Rows[i].Cells["Amount"].Value.ToString());

            }
            return _dt;
        }

        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceSales = new Microsoft.Reporting.WinForms.ReportDataSource();

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewerSales.Reset();
                DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("RemoveItemDataSet", dt);
                reportViewerSales.LocalReport.DataSources.Add(ds);
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RemoveItemReport.rdlc";
                //Passing Parmetes:
                // ReportParameter rptFrom = new ReportParameter("RptFromDate", Convert.ToString(From_date.Value.Day + "/" + From_date.Value.Day + "/" + From_date.Value.Day+"/"), false);
                // ReportParameter rptTo = new ReportParameter("RptToDate", Convert.ToString(To_date.Value.Day + "/" + To_date.Value.Day + "/" + To_date.Value.Day + "/"), false);

                ReportParameter rptFrom = new ReportParameter("RptFromDate", Convert.ToString(From_date.Text), false);
                ReportParameter rptTo = new ReportParameter("RptToDate", Convert.ToString(To_date.Text), false);
                ReportParameter rptItemName = new ReportParameter("RptItemName", Convert.ToString(txtitemname.Text), false);
                ////ReportParameter rp2 = new ReportParameter("DateTo", "300");
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rptFrom });
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rptTo });
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rptItemName });
                //dt.EndInit();
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
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

     
    }
}
