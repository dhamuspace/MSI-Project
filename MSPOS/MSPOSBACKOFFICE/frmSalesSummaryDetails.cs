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
   
    public partial class frmSalesSummaryDetails : Form
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public frmSalesSummaryDetails()
        {
            InitializeComponent();

            try
            {
                foreach (DataGridViewColumn col in grd_SalesDetails.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                dtTemp1.Columns.Add("Bill_No", typeof(string));
                dtTemp1.Columns.Add("Bill_Date", typeof(string));
                dtTemp1.Columns.Add("Particulars", typeof(string));
                dtTemp1.Columns.Add("Cash_Recd", typeof(string));
                dtTemp1.Columns.Add("Amount", typeof(string));
                dtTemp1.Columns.Add("Type", typeof(string));
                txt_sales.Text = "All";
                txt_cash.Text = "All";
                txt_reporton.Text = "Gross Amount";
                //   grd_SalesDetails.DataSource = dtTemp1;
                // Grdloadbysearch();
                grd_SalesDetails.DefaultCellStyle.Font = new Font("Tahoma", 12);
                grd_SalesDetails.RowTemplate.Height = 25;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }

        }
        public void loadSalesDetails()
        {
            try
            {
                con.Open();
                DateTime globaldate = Convert.ToDateTime(chkbox.DateSalesEntry);
                string date2 = globaldate.ToString("yyyy-MM-dd");
                //string queryMonth = "Select smas_billno as Bill_No,smas_billdate as Bill_Date ,smas_name As Particulars, smas_name as Cash_Recd,smas_Gross as Amount from salmas_table where smas_billdate ='" + date2 + "' ";
                string queryMonth = "Select smas_billno as Bill_No,smas_billdate as Bill_Date ,smas_name As Particulars, smas_name as Cash_Recd,(CASE WHEN @tActionType='GROSSAMT' THEN smas_Gross ELSE smas_NetAmount END) as Amount,(CASE WHEN smas_rtno=0 THEN 'NORETURN' ELSE 'RETURN' END) as Type from salmas_table where smas_billdate like @tDate+'%'";
                SqlCommand cmd = new SqlCommand(queryMonth, con);
                cmd.Parameters.AddWithValue("@tDate", date2);
                cmd.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);


                double tot = 0, tReturnValue = 0, tSalesValue = 0;
                for (int mn = 0; mn < dt.Rows.Count; mn++)
                {
                    if (dt.Rows[mn]["Type"].ToString().Trim() == "RETURN")
                    {
                        tReturnValue += (double.Parse(dt.Rows[mn]["Amount"].ToString()));
                    }
                    if (dt.Rows[mn]["Type"].ToString().Trim() == "NORETURN")
                    {
                        tSalesValue += (double.Parse(dt.Rows[mn]["Amount"].ToString()));
                    }
                    tot += (double.Parse(dt.Rows[mn]["Amount"].ToString()));
                }
                grd_SalesDetails.Rows.Add((dt.Rows.Count + 3));
                //  dtTemp1.Rows.Add(1);
                for (int mn = 0; mn < (dt.Rows.Count + 2); mn++)
                {
                    if (dt.Rows.Count > mn)
                    {
                        grd_SalesDetails.Rows[mn].Cells[0].Value = dt.Rows[mn][0].ToString();
                        grd_SalesDetails.Rows[mn].Cells[1].Value = dt.Rows[mn][1].ToString();
                        grd_SalesDetails.Rows[mn].Cells[2].Value = dt.Rows[mn][2].ToString();
                        grd_SalesDetails.Rows[mn].Cells[3].Value = dt.Rows[mn][3].ToString();
                        grd_SalesDetails.Rows[mn].Cells[4].Value = dt.Rows[mn][4].ToString();
                    }
                    if (mn == (dt.Rows.Count))
                    {
                        grd_SalesDetails.Rows[mn].Cells[0].Value = "";
                        grd_SalesDetails.Rows[mn].Cells[1].Value = "Total :";
                        grd_SalesDetails.Rows[mn].Cells[2].Value = "";
                        grd_SalesDetails.Rows[mn].Cells[3].Value = "";
                        grd_SalesDetails.Rows[mn].Cells[4].Value = string.Format("{0:0.00}", (tSalesValue - tReturnValue));
                    }
                }

                grd_SalesDetails.DataSource = dt;
                //grd_SalesDetails.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }

        private void grd_SalesDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                chkbox.FormIdentify = "SalesSummary";
                int row = e.RowIndex;
                var tempdate = grd_SalesDetails.Rows[row].Cells["Bill_No"].Value.ToString();
                if (tempdate != "")
                {
                    string selectedBillno = grd_SalesDetails.Rows[row].Cells["Bill_No"].Value.ToString();
                    Double selectedbillamt = Convert.ToDouble(grd_SalesDetails.Rows[row].Cells["Amount"].Value.ToString());
                    //MessageBox.Show(selectedBillno);
                    chkbox.SalesBillNo = selectedBillno;
                    chkbox.SalesBillamt = selectedbillamt;
                    chkbox.tCounterName = txt_counter.Text.Trim();
                    if (grd_SalesDetails.Rows[row].Cells["Type"].Value.ToString().Trim() == "NORETURN")
                    {
                        DataTable dtNewChkingNew = new DataTable();
                        dtNewChkingNew.Rows.Clear();
                        SqlCommand cmdChkExisting = new SqlCommand("Select * from salMas_table where smas_rtno=@tBillNo", con);
                        cmdChkExisting.Parameters.AddWithValue("@tBillNo", tempdate);
                        SqlDataAdapter adpChkExist = new SqlDataAdapter(cmdChkExisting);
                        adpChkExist.Fill(dtNewChkingNew);
                        if (dtNewChkingNew.Rows.Count > 0)
                        {
                            MyMessageBox.ShowBox("Sales has return", "Warning");
                        }
                        frmSalesAlteration frm = new frmSalesAlteration();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        frmSalesReturnAlteration frm = new frmSalesReturnAlteration();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Empty Field is Selected");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                frmDailySalesSummary frm = new frmDailySalesSummary();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
                //frm.BringToFront();
                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        SqlDataReader dreader=null;
        string chk;
        private void txt_ledger_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_ledger.Text.Trim() != null && txt_ledger.Text.Trim() != "")
                {
                    funConnectionStateCheck();
                    DataTable dtNew1 = new DataTable();
                    dtNew1.Rows.Clear();
                    // SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_name like '" + txt_ledger.Text.Trim() + "%'", con);
                    SqlCommand cmd = new SqlCommand("sp_SalesSummarySelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "LEDGERNAME");
                    cmd.Parameters.AddWithValue("@tValue", txt_ledger.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew1);
                    // dreader = cmd.ExecuteReader();
                    bool isChk = false;
                    for (int mn = 0; mn < dtNew1.Rows.Count; mn++)
                    {
                        isChk = true;
                        string tempStr = dtNew1.Rows[mn]["Ledger_name"].ToString();
                        for (int i = 0; i < lst_ledger.Items.Count; i++)
                        {
                            if (dtNew1.Rows[mn]["Ledger_name"].ToString() == lst_ledger.Items[i].ToString())
                            {

                                lst_ledger.SetSelected(i, true);
                                txt_ledger.Select();
                                chk = "1";
                                txt_ledger.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }

                        }
                    }
                    con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_ledger.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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

        private void txt_ledger_Enter(object sender, EventArgs e)
        {
            try
            {
                LedgerDetails();
                pnl_ledger.Visible = true;
                lst_ledger.Visible = true;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
          
        }
        public void LedgerDetails()
        {
            try
            {
                //con.Open();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table ", con);
                SqlDataAdapter asd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                lst_ledger.Items.Clear();
                dt.Rows.Clear();
                asd.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        lst_ledger.Items.Add(dt.Rows[k]["Ledger_name"].ToString());
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_ledger_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lst_ledger.SelectedIndex < lst_ledger.Items.Count - 1)
                    {
                        lst_ledger.SetSelected(lst_ledger.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lst_ledger.SelectedIndex > 0)
                    {
                        lst_ledger.SetSelected(lst_ledger.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    pnl_ledger.Visible = false;
                    lst_ledger.Visible = false;
                    if (lst_ledger.Text != "")
                    {
                        txt_ledger.Text = lst_ledger.SelectedItem.ToString();
                        txt_counter.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
      //  SqlDataReader dreader1=null;
        private void txt_counter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_counter.Text.Trim() != null && txt_counter.Text.Trim() != "")
                {
                    funConnectionStateCheck();
                    DataTable dtNew1 = new DataTable();
                    dtNew1.Rows.Clear();
                    // SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_name like '" + txt_ledger.Text.Trim() + "%'", con);
                    SqlCommand cmd = new SqlCommand("sp_SalesSummarySelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "COUNTERNAME");
                    cmd.Parameters.AddWithValue("@tValue", txt_counter.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtNew1);
                    // dreader = cmd.ExecuteReader();
                    bool isChk = false;
                    for (int mn = 0; mn < dtNew1.Rows.Count; mn++)
                    {

                        // SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_name like '" + txt_counter.Text.Trim() + "%'", con);


                        isChk = true;
                        string tempStr = dtNew1.Rows[mn]["ctr_name"].ToString();
                        for (int i = 0; i < lst_counter.Items.Count; i++)
                        {
                            if (dtNew1.Rows[mn]["ctr_name"].ToString() == lst_counter.Items[i].ToString())
                            {

                                lst_counter.SetSelected(i, true);
                                txt_counter.Select();
                                chk = "1";
                                txt_counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }

                        }
                    }
                    con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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

        private void txt_counter_Enter(object sender, EventArgs e)
        {
            try
            {
                CounterNameLoad();
                pnl_counter.Visible = true;
                lst_counter.Visible = true;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
          
        }
        public void CounterNameLoad()
        {
            try
            {
                funConnectionStateCheck();
                SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table", con);
                SqlDataAdapter asd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                lst_counter.Items.Clear();
                dt.Rows.Clear();
                asd.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        lst_counter.Items.Add(dt.Rows[k]["ctr_name"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           
        }
        private void txt_counter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lst_counter.SelectedIndex < lst_counter.Items.Count - 1)
                    {
                        lst_counter.SetSelected(lst_counter.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lst_counter.SelectedIndex > 0)
                    {
                        lst_counter.SetSelected(lst_counter.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    pnl_counter.Visible = false;
                    lst_counter.Visible = false;
                    if (lst_counter.SelectedItems.Count > 0)
                    {

                        txt_counter.Text = lst_counter.SelectedItem.ToString();

                    }
                    txt_sales.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_sales_Enter(object sender, EventArgs e)
        {
            try
            {
                lst_sales.Visible = true;
                pnl_sales.Visible = true;
                lst_sales.Focus();
                lst_sales.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_sales.Text = lst_sales.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_sales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pnl_sales.Visible = false;
                    lst_sales.Visible = false;
                    txt_sales.Text = lst_sales.SelectedItem.ToString();
                    txt_cash.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_cash_Enter(object sender, EventArgs e)
        {
            try
            {
                pnl_cash.Visible = true;
                lst_cash.Visible = true;
                lst_cash.Focus();
                lst_cash.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            
        }

        private void lst_cash_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_cash.Text = lst_cash.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_cash_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pnl_cash.Visible = false;
                    lst_cash.Visible = false;
                    txt_cash.Text = lst_cash.SelectedItem.ToString();
                    txt_reporton.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_reporton_Enter(object sender, EventArgs e)
        {
            try
            {
                pnl_Amount.Visible = true;
                lst_ofAmount.Visible = true;
                lst_ofAmount.Focus();
                lst_ofAmount.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_ofAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_reporton.Text = lst_ofAmount.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_ofAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pnl_Amount.Visible = false;
                    lst_ofAmount.Visible = false;
                    txt_reporton.Text = lst_ofAmount.SelectedItem.ToString();
                    //gridload();
                    Grdloadbysearch();
                    grd_SalesDetails.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        DataTable dtTemp1 = new DataTable();
        public void Grdloadbysearch()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Rows.Clear();
                SqlCommand cmdGetDate = new SqlCommand("getAllDaysBetweenTwoDate", con);
                cmdGetDate.CommandType = CommandType.StoredProcedure;
                cmdGetDate.Parameters.AddWithValue("@FromDate", dt_from.Value.ToString("yyyy-MM-dd"));
                cmdGetDate.Parameters.AddWithValue("@ToDate", dt_to.Value.ToString("yyyy-MM-dd"));
                SqlDataAdapter adp = new SqlDataAdapter(cmdGetDate);
                adp.Fill(dtTemp);
                //dreader = cmdGetDate.ExecuteReader();
                //dtTemp.Load(dreader);
                // lst_ofAmount.SelectedIndex = 0;

                dtTemp1.Rows.Clear();
                string tCmd = "";
                DataTable dtDiscountAmt = new DataTable();
                double tDiscountAmt = 0.00;
                for (int mn = 0; mn < dtTemp.Rows.Count; mn++)
                {

                    SqlCommand cmdGetDate1 = new SqlCommand("sp_SalesSummaryDetail", con);
                    cmdGetDate1.CommandType = CommandType.StoredProcedure;
                    cmdGetDate1.Parameters.AddWithValue("@tActionType", (txt_reporton.Text == "Gross Amount") ? "GROSSAMT" : "NETAMT");
                    cmdGetDate1.Parameters.AddWithValue("@tCounterName", (txt_counter.Text.Trim() == "") ? "All" : txt_counter.Text.Trim());
                    cmdGetDate1.Parameters.AddWithValue("@tPartyName", (txt_ledger.Text.Trim() == "") ? "All" : txt_ledger.Text.Trim());
                    cmdGetDate1.Parameters.AddWithValue("@tSalesType", txt_sales.Text.Trim());
                    cmdGetDate1.Parameters.AddWithValue("@tCashType", txt_cash.Text.Trim());
                    cmdGetDate1.Parameters.AddWithValue("@tDate", DateTime.Parse(dtTemp.Rows[mn][0].ToString()).ToString("yyyy-MM-dd"));
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmdGetDate1);
                    adp1.Fill(dtTemp1);
                
                }
                double tot = 0, tReturnValue = 0, tSalesValue = 0;
                string tBillNoCollection = "";
                for (int mn = 0; mn < dtTemp1.Rows.Count; mn++)
                {
                    if (dtTemp1.Rows[mn]["Type"].ToString().Trim() == "RETURN")
                    {
                        tReturnValue += (double.Parse(dtTemp1.Rows[mn]["Amount"].ToString()));
                    }
                    if (dtTemp1.Rows[mn]["Type"].ToString().Trim() == "NORETURN")
                    {
                        tBillNoCollection += dtTemp1.Rows[mn]["Bill_No"].ToString().Trim() + ",";
                        tSalesValue += (double.Parse(dtTemp1.Rows[mn]["Amount"].ToString()));
                    }
                    DateTime str = DateTime.Parse(dtTemp1.Rows[mn]["Bill_Date"].ToString());
                    dtTemp1.Rows[mn]["Bill_Date"] = str.ToShortDateString();
                    tot += (double.Parse(dtTemp1.Rows[mn]["Amount"].ToString()));
                }
                tBillNoCollection += tBillNoCollection.TrimEnd(',');
                dtTemp1.Rows.Add("", "", "", "", "");
                
                dtTemp1.Rows.Add("", "", "Total:", "", string.Format("{0:0.00}", (tSalesValue - tReturnValue)));

                if (txt_counter.Text.Trim() == "All")
                {
                    tCmd = "Select sum(Amount) as DiscountAmt from DiscountDetail_table  where Bill_no in ("+tBillNoCollection+")";
                }
                else
                {
                    tCmd = "Select sum(Amount) as DiscountAmt from DiscountDetail_table  where Bill_no in (" + tBillNoCollection + ")";
                }

                dtDiscountAmt.Rows.Clear();
                SqlCommand cmdDiscount = new SqlCommand(tCmd, con);
                cmdDiscount.Parameters.AddWithValue("@tDate", dt_from.Value);
                cmdDiscount.Parameters.AddWithValue("@tCounter", txt_counter.Text);
                SqlDataAdapter adpDiscount = new SqlDataAdapter(cmdDiscount);
                adpDiscount.Fill(dtDiscountAmt);

                if (dtDiscountAmt.Rows.Count > 0)
                {
                    tDiscountAmt += (string.IsNullOrEmpty(Convert.ToString(dtDiscountAmt.Rows[0]["DiscountAmt"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(dtDiscountAmt.Rows[0]["DiscountAmt"]));
                    //  dtTemp1.Rows.Add("", "", "Discount Amount", string.Format("{0:0.00}", tDiscountAmt), string.Format("{0:0.00}", ((tSalesValue - tReturnValue) - tDiscountAmt)));

                }
                if (tDiscountAmt > 0)
                {
                    dtTemp1.Rows.Add("", "", "Discount Amount", string.Format("{0:0.00}", tDiscountAmt), string.Format("{0:0.00}", ((tSalesValue - tReturnValue) - tDiscountAmt)));

                }

                grd_SalesDetails.DataSource = dtTemp1.DefaultView;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
          
        }

        private void txt_ledger_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible=false;
            pnl_cash.Visible = false;
            pnl_counter.Visible = false;
            pnl_sales.Visible = false;
            pnl_ledger.Visible = true;
        }

        private void txt_counter_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = false;
            pnl_cash.Visible = false;
            pnl_counter.Visible = true;
            pnl_sales.Visible = false;
            pnl_ledger.Visible = false;
        }

        private void txt_sales_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = false;
            pnl_cash.Visible = false;
            pnl_counter.Visible = false;
            pnl_sales.Visible = true;
            pnl_ledger.Visible = false;
        }

        private void txt_cash_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = false;
            pnl_cash.Visible = true;
            pnl_counter.Visible = false;
            pnl_sales.Visible = false;
            pnl_ledger.Visible = false;
        }

        private void txt_reporton_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_Amount.Visible = true;
            pnl_cash.Visible = false;
            pnl_counter.Visible = false;
            pnl_sales.Visible = false;
            pnl_ledger.Visible = false;
        }

        private void frmSalesSummaryDetails_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn col in grd_SalesDetails.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);

                }

                dt_from.Text = chkbox.DateSalesEntry;
                dt_to.Text = chkbox.DateSalesEntry;
                txt_counter.Text = chkbox.tCounterName;
                //loadSalesDetails();
                Grdloadbysearch();
                grd_SalesDetails.Columns[0].Width = 120;
                grd_SalesDetails.Columns[1].Width = 115;
                grd_SalesDetails.Columns[2].Width = 350;
                grd_SalesDetails.Columns[3].Width = 230;
                grd_SalesDetails.Columns[4].Width = 200;
                grd_SalesDetails.Columns["Type"].Visible = false;
                grd_SalesDetails.ReadOnly = true;
                pnl_Amount.Visible = false;
                lst_ofAmount.Visible = false;
                pnl_cash.Visible = false;
                lst_cash.Visible = false;
                pnl_counter.Visible = false;
                lst_counter.Visible = false;

                pnl_ledger.Visible = false;
                lst_ledger.Visible = false;

                pnl_sales.Visible = false;
                lst_sales.Visible = false;

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
            
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        private void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                Dataset.dsSalesSummary dsSalesSummaryObj = new Dataset.dsSalesSummary();
                for (int i = 0; i <grd_SalesDetails.Rows.Count; i++)
                {
                    dsSalesSummaryObj.Tables["DataTable3"].Rows.Add( Convert.ToString(grd_SalesDetails.Rows[i].Cells[0].Value), Convert.ToString(grd_SalesDetails.Rows[i].Cells[1].Value), Convert.ToString(grd_SalesDetails.Rows[i].Cells[2].Value), Convert.ToString(grd_SalesDetails.Rows[i].Cells[3].Value), Convert.ToString(grd_SalesDetails.Rows[i].Cells[4].Value), Convert.ToString(txt_sales.Text), Convert.ToString(txt_cash.Text), Convert.ToString(txt_ledger.Text));
                }
                reportViewerSales.Reset();
                //  DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DataTable3"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);

                reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcSalesSummaryDetail.rdlc";
                //Passing Parmetes:
                ReportParameter rpReportOn = new ReportParameter("ReportOn", Convert.ToString(txt_reporton.Text), false);
                ReportParameter rpCounter = new ReportParameter("Counter", Convert.ToString(txt_counter.Text), false);
                ReportParameter rpFrom = new ReportParameter("From", Convert.ToString(dt_from.Value.Day + "/" + dt_from.Value.Month + "/" + dt_from.Value.Year), false);
                ReportParameter rpTo = new ReportParameter("To", Convert.ToString(dt_to.Value.Day + "/" + dt_to.Value.Month + "/" + dt_to.Value.Year), false);
                //ReportParameter rpCash = new ReportParameter("Cash", Convert.ToString(txt_cash.Text), false);
                //ReportParameter rpSalesType = new ReportParameter("SalesType", Convert.ToString(txt_sales.Text), false);
                //ReportParameter rpParty = new ReportParameter("Party", Convert.ToString(txt_ledger.Text), false);
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn, rpCounter, rpFrom, rpTo});
                //, rpCash, rpSalesType, rpParty });
                dsSalesSummaryObj.Tables["DataTable3"].EndInit();
                reportViewerSales.RefreshReport();
                reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
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
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}
