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
using System.IO;
using System.Configuration;
using System.Data.OleDb;
//using iTextSharp.text;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class ListOfPurchase : Form
    {
        public ListOfPurchase()
        {
            InitializeComponent();
        }
        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
        IFormatProvider culture = new CultureInfo("fr-FR", true);
        // SqlConnection con = new SqlConnection(@"Data Source=ASTRID-PC\SQLEXPRESS;Initial Catalog=Mspos;Persist Security Info=True;User ID=sa;password=!Password123");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataTable dt_cash_table = new DataTable();
        DataTable dt_cash = new DataTable();  
        DataTable dt_lvtype = new DataTable();
        
        private void ListOfPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = txtfromdate;
                foreach (DataGridViewColumn col in DgNormalGrid.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                foreach (DataGridViewColumn col1 in DgDetailGrid.Columns)
                {
                    col1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col1.HeaderCell.Style.Font = new System.Drawing.Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                dt_lvtype.Columns.Add("Types");
                DgNormalGrid.DefaultCellStyle.ForeColor = Color.Black;
                DgNormalGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;                
                DgNormalGrid.BackgroundColor = Color.White;
                DgDetailGrid.DefaultCellStyle.ForeColor = Color.Black;
                DgDetailGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
                DgDetailGrid.BackgroundColor = Color.White;

                DgNormalGrid.ReadOnly = true;
                DgDetailGrid.ReadOnly = true;
                DgDetailGrid.Visible = false;
                Pnl_Footer.Visible = false;
                panel7.Visible = false;
                panel1.Visible = false;
                listboxhide_values();
                listboxpurtype();
                listboxtaxtype(); 
                listviewcounter();
                purchase_entry_values_type();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //SqlCommand cmd = new SqlCommand(@"SELECT distinct purmas_table.pmas_date, purmas_table.pmas_billno, purmas_table.pmas_billdate, purmas_table.pmas_name, Ledger_table.Ledger_name, purmas_table.pmas_netamount, purmas_table.Pmas_sno FROM (purmas_table INNER JOIN stktrn_table ON purmas_table.Pmas_sno = stktrn_table.strn_no) INNER JOIN Ledger_table ON purmas_table.CashLed_no = Ledger_table.Ledger_no", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Actiontype", "SelectPurchaseMasTb");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataTable dt_clone = new DataTable();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string stname = dt.Rows[i]["Ledger_name"].ToString();
                            if (stname.IndexOf("A/c") != -1)
                            {
                                stname = stname.Replace("A/c", " ");
                                dt.Rows[i]["Ledger_name"] = stname.ToString();
                            }
                        }
                        DgNormalGrid.DataSource = dt;
                        DgNormalGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        lbltot_purchase.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                        gridclculation();
                    }
                }
                txtorder.Focus();
                txtorder.SelectAll();
                dt_lvtype.Rows.Clear();
                lvType.Items.Clear();
                dt_lvtype.Rows.Add("Details");
                dt_lvtype.Rows.Add("Normal");
                if (dt_lvtype.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_lvtype.Rows.Count; i++)
                    {
                        lvType.Items.Add(dt_lvtype.Rows[i]["Types"].ToString());
                    }
                }
                DgNormalGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
                DgDetailGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                 Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
                pnlNormal.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        
        public void listboxpurtype()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dt_pur_type = new DataTable();
                // SqlCommand cmd = new SqlCommand("select * from purType_Table", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "PurchaseType");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_pur_type.Rows.Clear();
                adp.Fill(dt_pur_type);
                if (dt_pur_type.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_pur_type.Rows.Count; i++)
                    {
                        lvPurType.Items.Add(dt_pur_type.Rows[i]["PurType_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        public void listboxtaxtype()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dt_tax_type = new DataTable();
                // SqlCommand cmd = new SqlCommand("select * from purType_Table", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "TaxType");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_tax_type.Rows.Clear();
                adp.Fill(dt_tax_type);
                if (dt_tax_type.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tax_type.Rows.Count; i++)
                    {
                         lvtaxtype.Items.Add(dt_tax_type.Rows[i]["TaxType_Name"].ToString());
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }



        DataTable dt_counter_table = new DataTable();
        
        public void listviewcounter()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //SqlCommand cmd = new SqlCommand("select * from counter_table", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "COUNTERTYPE");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_counter_table.Rows.Clear();
                lvcounters.Items.Clear();
                adp.Fill(dt_counter_table);
                if (dt_counter_table.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_counter_table.Rows.Count; i++)
                    {
                        lvcounters.Items.Add(dt_counter_table.Rows[i]["ctr_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }


        public void listboxhide_values()
        {
            // dataGridView2.Visible = false;
            pnlcancel_type.Visible = false;
            pnlcounter.Visible = false;
            pnlinvoice.Visible = false;
            pnlitemspartys.Visible = false;
            pnlOrder.Visible = false;
            pnltype.Visible = false;
            pnlbillType.Visible = false;
            pnlpurtype.Visible = false;
            panel1.Visible = false;
            lvcancel_type.Visible = false;
            lvcounters.Visible = false;
            lvinvoice.Visible = false;
            lvItemsparty.Visible = false;
            lvOrder.Visible = false;
            lvType.Visible = false;
            lvBillType.Visible = false;
            lvPurType.Visible = false;
            //lvItemsparty.Visible = false;
        }
        public void purchase_entry_values_type()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dt_ledger_creation = new DataTable();
                lvItemsparty.Items.Clear();
                // SqlCommand cmd = new SqlCommand("select * from Ledger_table where ledger_type=1", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "LedgerType");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_ledger_creation);
                if (dt_ledger_creation.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_ledger_creation.Rows.Count; i++)
                    {
                        lvItemsparty.Items.Add(dt_ledger_creation.Rows[i]["Ledsel_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void gridclculation()
        {
            try
            {
                double amount = 0.00;
                double grossamount1 = 0.00;
                double taxamount = 0.00;

                for (int i = 0; i < DgNormalGrid.Rows.Count - 1; i++)
                {
                    amount += Convert.ToDouble(DgNormalGrid.Rows[i].Cells["pmas_netamount"].Value);
                    grossamount1 += Convert.ToDouble(DgNormalGrid.Rows[i].Cells["pmas_gross"].Value);
                }

                taxamount  = amount - grossamount1;
                lbltaxamount.Text = taxamount.ToString("0.00"); 
                lbltotal_amount.Text = amount.ToString("0.00");
                //lbltot_purchase.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                lbltot_purchase.Text = Convert.ToInt32(DgNormalGrid.Rows.Count - 1 ).ToString();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string st1;
            try
            {
                if (DgNormalGrid.Rows[e.RowIndex].Cells["pmas_sno"].Value!= null && DgNormalGrid.Rows[e.RowIndex].Cells["pmas_sno"].Value != "")
                {
                    st1 = DgNormalGrid.Rows[e.RowIndex].Cells["pmas_sno"].Value.ToString();
                    PurchaseEntry1 purentry = new PurchaseEntry1(st1);
                    purentry.MdiParent = this.ParentForm;
                    purentry.StartPosition = FormStartPosition.Manual;
                    purentry.WindowState = FormWindowState.Normal;
                    purentry.Location = new Point(0, 80);
                    purentry.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             string st1;
             try
             {
                 if (DgNormalGrid.Rows[e.RowIndex].Cells["pmas_sno"].Value!= null && DgNormalGrid.Rows[e.RowIndex].Cells["pmas_sno"].Value != "")
                 {
                     st1 = DgDetailGrid.Rows[e.RowIndex].Cells["pmas_sno"].Value.ToString();
                     PurchaseEntry1 purentry = new PurchaseEntry1(st1);
                     purentry.MdiParent = this.ParentForm;
                     purentry.StartPosition = FormStartPosition.Manual;
                     purentry.WindowState = FormWindowState.Normal;
                     purentry.Location = new Point(0, 80);
                     purentry.Show();
                 }
             }
             catch
             { }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseEntry1 frm = new PurchaseEntry1("");
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void ListOfPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void lvItemsparty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvItemsparty.Text != "")
            {
                //txtparty_no.Text = lvItemsparty.SelectedItem.ToString();
            }
        }
        private void txtparty_no_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter))
                {
                    int index = lvOrder.FindString(txtparty_no.Text, -1);
                    if (index != 0)
                    {
                        txtparty_no.Text = lvItemsparty.SelectedItem.ToString();
                        txtorder.Focus();
                    }
                    else
                    {
                        txtparty_no.Text = lvItemsparty.Items[index].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string s = "";
        private void txtparty_no_Leave(object sender, EventArgs e)
        {

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txttype.Text == "Normal")
                {
                    NormalGrid();
                    //DgNormalGrid.Visible = true;
                    //DgDetailGrid.Visible = false;
                    //pnlNormal.Visible = true;
                    //pnlDetails.Visible = false;
                }
                else if (txttype.Text == "Details")
                {
                    DetailGrid();
                    //DgNormalGrid.Visible = false;
                    //DgDetailGrid.Visible = true;
                    //pnlDetails.Visible = true;

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void NormalGrid()
        {
            try
            {
                Pnl_Footer.Visible = false;
                pnlNormal.Visible = true;
                DgNormalGrid.Visible = true;
                DgNormalGrid.DataSource = dt;
                lbltot_purchase.Text = Convert.ToInt32(dt.Rows.Count).ToString();
                // string str = "SELECT distinct purmas_table.pmas_no, purmas_table.pmas_date, purmas_table.pmas_billno, purmas_table.pmas_billdate,purmas_table.pmas_netamount,purmas_table.pmas_add1 FROM purmas_table,stktrn_table where";
                string str = "SELECT distinct purmas_table.pmas_date, purmas_table.pmas_billno, purmas_table.pmas_billdate, purmas_table.pmas_name, Ledger_table.Ledger_name, convert(Numeric(18,2),purmas_table.pmas_netamount) as pmas_netamount, convert(Numeric(18,2),purmas_table.pmas_gross) as pmas_gross, purmas_table.Pmas_sno FROM (purmas_table INNER JOIN stktrn_table ON purmas_table.Pmas_sno = stktrn_table.strn_no) INNER JOIN Ledger_table ON purmas_table.CashLed_no = Ledger_table.Ledger_no where ";
                if (txtinvoice.Text == "All" || txtnotcancelled.Text == "All" || txtbill_type.Text == "All")
                {
                    str += " purmas_table.Pmas_sno = stktrn_table.strn_no AND ";
                }
                if (txtinvoice.Text == "Purchase Only")
                {
                    str += " purmas_table.pmas_name='Cash Purchase' AND ";
                }
                
                if (txtinvoice.Text == "Invoice Only")
                {
                    str += " purmas_table.pmas_name<> 'Cash Purchase' AND ";
                }

                if (txttaxtype.Text == "Non GST")
                {
                   
                    str += " purmas_table.taxtype = '3' AND ";
                }

                if (txttaxtype.Text == "Excluding GST")
                {

                    str += " purmas_table.taxtype = '1' AND ";
                }

                if (txttaxtype.Text == "Including GST")
                {

                    str += " purmas_table.taxtype = '2' AND ";
                }

                if (txtbill_type.Text == "Cash")
                {
                    str += " purmas_table.CashLed_no=5 AND ";
                }
                if (txtbill_type.Text == "Credit")
                {
                    str += " purmas_table.CashLed_no=8 AND ";
                }
                if (txtnotcancelled.Text == "Cancelled")
                {
                    str += " purmas_table.pmas_cancel=1 AND ";
                }
                if (txtnotcancelled.Text == "Not Cancelled")
                {
                    str += " purmas_table.pmas_cancel<>1 AND ";
                }
                if (txtremarks.Text.Trim() != "")
                {
                }
               
                if (txtCounter.Text.Trim() != "")
                {

                    //SqlCommand cmd = new SqlCommand("select ctr_no from counter_table where ctr_name='" + txtCounter.Text + "'", con);
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "CounterName");
                    cmd.Parameters.AddWithValue("@itemName", txtCounter.Text);
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    //countername = ""
                    // str += " purmas_table.Ctr_no='" + countername + "' AND ";
                }
                if (billno.Text.Trim() != "")
                {
                    str += " purmas_table.pmas_billno='" + billno.Text + "' AND ";
                }
                string ledgernumber;

                if (txtpurtype.Text.Trim() != "")
                {
                    if (txtpurtype.Text.Trim() != "Primary" && txtpurtype.Text.Trim() != "All")
                    {
                        // SqlCommand cmd = new SqlCommand("select ledger_no from Ledger_table where ledger_name='" + txtparty_no.Text + "'", con);
                        SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "LegderName");
                        cmd.Parameters.AddWithValue("@itemName", txtparty_no.Text);
                        cmd.Parameters.AddWithValue("@ItemCode", "");
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt_party = new DataTable();
                        dt_party.Rows.Clear();
                        adp.Fill(dt_party);
                        if (dt_party.Rows.Count > 0)
                        {
                            ledgernumber = dt_party.Rows[0]["ledger_no"].ToString();
                            str += " purmas_table.party_no='" + ledgernumber + "' AND ";
                        }
                    }
                }

                if (txtparty_no.Text.Trim() != "")
                {
                    str += " purmas_table.pmas_name='" + txtparty_no.Text + "' AND ";
                    //str += ""
                }


                if (txtorder.Text == "Bill Wise")
                {
                    str += " purmas_table.pmas_date>= '" + DateTime.Parse(txtfromdate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and purmas_table.pmas_date<='" + DateTime.Parse(txttodate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' AND ";
                    
                    s = str.Remove(str.Length - 4);
                    s += " Order By purmas_table.pmas_billno ";
                }

                if (txtorder.Text == "Date Wise")
                {
                    str += "  purmas_table.pmas_date>= '" + DateTime.Parse(txtfromdate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and purmas_table.pmas_date<='" + DateTime.Parse(txttodate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' AND ";
                    
                    s = str.Remove(str.Length - 4);
                    s += "Order By purmas_table.pmas_date";
                }


                //    SqlCommand cmd = new SqlCommand("SELECT distinct purmas_table.pmas_no, purmas_table.pmas_date, purmas_table.pmas_billno, purmas_table.pmas_billdate,purmas_table.pmas_netamount,purmas_table.pmas_add1 FROM purmas_table,stktrn_table where purmas_table.Pmas_sno = stktrn_table.strn_no and purmas_table.pmas_date>= '" + DateTime.Parse(txtfromdate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and purmas_table.pmas_date<='" + DateTime.Parse(txttodate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "'", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(s, con);

                dt_cash_table.Rows.Clear();
                adp1.Fill(dt_cash_table);

                if (dt_cash_table.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_cash_table.Rows.Count; i++)
                    {
                        string stname = dt_cash_table.Rows[i]["Ledger_name"].ToString();
                        if (stname.IndexOf("A/c") != -1)
                        {
                            stname = stname.Replace("A/c", " ");
                            dt_cash_table.Rows[i]["Ledger_name"] = stname.ToString();
                        }
                    }
                    DgNormalGrid.DataSource = dt_cash_table;
                }
                gridclculation();
                DgDetailGrid.Visible = false;
                Pnl_Footer.Visible = false;
                DgNormalGrid.Visible = true;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void DetailGrid()
        {
            try
            {
                //string str = " SELECT DISTINCT dbo.Item_table.Item_name, dbo.unit_table.unit_name, dbo.purmas_table.pmas_name,purmas_table.Pmas_sno,dbo.stktrn_table.nt_qty, Convert(Numeric(18,2),dbo.stktrn_table.Rate) as Rate, Convert(Numeric(18,2),dbo.stktrn_table.Amount) as Amount, convert(Numeric(18,2),dbo.stktrn_table.Disc_Per) as Disc_Per, convert(Numeric(18,2),dbo.stktrn_table.Tax_Rate) as Tax_Rate, convert(Numeric(18,2),dbo.stktrn_table.tot_amt) as tot_amt,dbo.purmas_table.pmas_billno, dbo.purmas_table.pmas_date FROM  dbo.purmas_table INNER JOIN dbo.stktrn_table ON dbo.purmas_table.Pmas_sno = dbo.stktrn_table.strn_no INNER JOIN dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no INNER JOIN dbo.unit_table ON dbo.Item_table.Unit_no = dbo.unit_table.unit_no WHERE   ";
                string str = " SELECT DISTINCT dbo.Item_table.Item_name, dbo.unit_table.unit_name, dbo.purmas_table.pmas_name,purmas_table.Pmas_sno,dbo.stktrn_table.nt_qty, Convert(Numeric(18,2),dbo.stktrn_table.Rate) as Rate, Convert(Numeric(18,2),dbo.stktrn_table.Amount) as Amount, convert(Numeric(18,2),dbo.stktrn_table.Disc_Per) as Disc_Per, convert(Numeric(18,2),dbo.stktrn_table.Tax_Rate) as Tax_Rate, convert(Numeric(18,2),dbo.stktrn_table.tot_amt) as tot_amt,dbo.purmas_table.pmas_billno, dbo.purmas_table.pmas_date,stktrn_table.strn_sno FROM  dbo.purmas_table INNER JOIN dbo.stktrn_table ON dbo.purmas_table.Pmas_sno = dbo.stktrn_table.strn_no INNER JOIN dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no INNER JOIN dbo.unit_table ON dbo.Item_table.Unit_no = dbo.unit_table.unit_no WHERE   ";
                if (txtinvoice.Text == "All" || txtnotcancelled.Text == "All" || txtbill_type.Text == "All")
                {
                    str += " purmas_table.Pmas_sno = stktrn_table.strn_no AND ";
                }
                if (txtinvoice.Text == "Purchase Only")
                {
                    str += " purmas_table.pmas_name='Cash Purchase' AND ";
                }
                if (txtinvoice.Text == "Invoice Only")
                {
                    str += " purmas_table.pmas_name<> 'Cash Purchase' AND ";
                }

                if (txtbill_type.Text == "Cash")
                {
                    str += " purmas_table.CashLed_no=5 AND ";
                }
                if (txtbill_type.Text == "Credit")
                {
                    str += " purmas_table.CashLed_no=8 AND ";
                }

                if (txtnotcancelled.Text == "Cancelled")
                {
                    str += " purmas_table.pmas_cancel=1 AND ";
                }
                if (txtnotcancelled.Text == "Not Cancelled")
                {
                    str += " purmas_table.pmas_cancel<>1 AND ";
                }
                if (txtremarks.Text.Trim() != "")
                {

                }
                string countername = "";
                if (txtCounter.Text.Trim() != "")
                {

                    SqlCommand cmd = new SqlCommand("select ctr_no from counter_table where ctr_name=@CtrName", con);
                    cmd.Parameters.AddWithValue("@CtrName", txtCounter.Text);
                    countername = cmd.ExecuteScalar().ToString();
                    // str += " purmas_table.Ctr_no='" + countername + "' AND ";
                }
                if (billno.Text.Trim() != "")
                {
                    str += " purmas_table.pmas_billno='" + billno.Text + "' AND ";
                }
                string ledgernumber;
                if (txtpurtype.Text.Trim() != "")
                {
                    if (txtpurtype.Text.Trim() != "Primary" && txtparty_no.Text.Trim() != "")
                    {
                        SqlCommand cmd = new SqlCommand("select ledger_no from Ledger_table where ledger_name=@ledgerNo", con);
                        cmd.Parameters.AddWithValue("@ledgerNo", txtparty_no.Text);
                        ledgernumber = cmd.ExecuteScalar().ToString();
                        str += " purmas_table.party_no='" + ledgernumber + "' AND ";
                    }
                }
                if (txtparty_no.Text.Trim() != "")
                {
                    str += " purmas_table.pmas_name='" + txtparty_no.Text + "' AND ";
                    //str += ""
                }
                if (txtorder.Text == "Bill Wise")
                {
                    str += " purmas_table.pmas_date>= '" + DateTime.Parse(txtfromdate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and purmas_table.pmas_date<='" + DateTime.Parse(txttodate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' AND ";
                    s = str.Remove(str.Length - 4);
                    if (txttype.Text == "Details")
                    {
                        s += "Order By stktrn_table.strn_sno";
                    }
                    else
                    {
                        s += " Order By purmas_table.pmas_billno ";
                    }
                }
                if (txtorder.Text == "Date Wise")
                {
                    str += "  purmas_table.pmas_date>= '" + DateTime.Parse(txtfromdate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' and purmas_table.pmas_date<='" + DateTime.Parse(txttodate.Value.ToShortDateString()).ToString("yyyy-MM-dd HH:mm:ss:fff") + "' AND ";
                    s = str.Remove(str.Length - 4);
                    s += "Order By purmas_table.pmas_date";
                }
                // SqlCommand cmd = new SqlCommand("SELECT distinct purmas_table.pmas_name,purmas_table.pmas_date, purmas_table.pmas_billno,purmas_table.pmas_add1,stktrn_table.nt_qty,stktrn_table.Rate,stktrn_table.Amount,stktrn_table.Disc_Per,stktrn_table.Tax_Rate,stktrn_table.tot_amt FROM purmas_table,stktrn_table where purmas_table.Pmas_sno = stktrn_table.strn_no and purmas_table.pmas_cancel<>1", con);
                DataTable dt_type_table = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(s, con);
                dt_type_table.Rows.Clear();
                adp.Fill(dt_type_table);
                DgDetailGrid.DataSource = dt_type_table;
                DgDetailGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDetailGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDetailGrid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDetailGrid.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDetailGrid.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDetailGrid.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDetailGrid.Columns["Pmas_sno"].Visible = false;
                double ntqty = 0.00;
                double total_amout = 0.00;
                double nt_amount = 0.00;
                for (int i = 0; i < dt_type_table.Rows.Count; i++)
                {
                    ntqty += Convert.ToDouble(dt_type_table.Rows[i]["nt_qty"].ToString());
                    total_amout += Convert.ToDouble(dt_type_table.Rows[i]["Amount"].ToString());
                    nt_amount += Convert.ToDouble(dt_type_table.Rows[i]["tot_amt"].ToString());
                }
                lblNtQty.Text = ntqty.ToString("0.00");
                lblAmount.Text = total_amout.ToString("0.00");
                lblNetAmount.Text = nt_amount.ToString("0.00");
                Pnl_Footer.Visible = true;

                int datagrid = Convert.ToInt32(dt_type_table.Rows.Count);
                lbl_items_no_grid2.Text = datagrid.ToString();

                DgDetailGrid.Visible = true;
                Pnl_Footer.Visible = true;
                DgNormalGrid.Visible = false;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtbill_type_Enter(object sender, EventArgs e)
        {
            try
            {
                //dataGridView2.Visible = false;
                pnlbillType.Visible = true;
                lvBillType.Visible = true;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                panel7.Visible = false;
                // pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                // lvBillType.Visible = false;
                lvPurType.Visible = false;
                txtbill_type.SelectAll();
                // lvBillType.SetSelected(0, true);
                if (txttype.Text.Trim() == "")
                {
                    if (lvBillType.Items.Count > 0)
                    {
                        lvBillType.SetSelected(0, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txttype_Enter(object sender, EventArgs e)
        {
            try
            {

                txttype.SelectAll();
                panel7.Visible = false;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = true;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = true;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                txttype.SelectAll();
                //  lvType.SetSelected(0, true);

                if (txttype.Text.Trim() == "")
                {
                    if (lvType.Items.Count > 0)
                    {
                        lvType.SetSelected(0, true);
                    }
                }
                if (lvType.Items.Count > 0)
                {
                    if (txttype.Text.Trim() != "")
                    {
                        for (int k = 0; k < dt_lvtype.Rows.Count; k++)
                        {
                            if (txttype.Text == dt_lvtype.Rows[k]["Types"].ToString())
                            {
                                lvType.SetSelected(k, true);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvcancel_type_Enter(object sender, EventArgs e)
        {
            try
            {
                //  dataGridView2.Visible = false;
                pnlcancel_type.Visible = true;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = true;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtorder_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                //  dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = true;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = true;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                if (txtorder.Text.Trim() == "")
                {
                    lvOrder.SetSelected(0, true);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtinvoice_Enter(object sender, EventArgs e)
        {
            try
            {
                txtinvoice.SelectAll();
                panel7.Visible = true;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = true;
                pnlinvoice.Visible = true;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                if (txtinvoice.Text.Trim() == "")
                {
                    lvinvoice.SetSelected(0, true);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtCounter_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = true;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = false;
                lvcounters.Visible = true;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                lvcounters.SetSelected(0, true);
                if (txtCounter.Text.Trim() == "")
                {
                    lvcounters.SetSelected(0, true);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtparty_no_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = true;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = true;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                lvItemsparty.SetSelected(0, true);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        
        private void txtpurtype_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                panel1.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = true;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = true;
                lvtaxtype.Visible = false; 
                txtpurtype.Select();
                //  lvPurType.SetSelected(0, true);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }


        private void txtnotcancelled_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = true;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                lvcancel_type.Visible = true;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                if (txtnotcancelled.Text == "")
                {
                    lvcancel_type.SetSelected(0, true);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtfromdate_Enter(object sender, EventArgs e)
        {
            try
            {
                listboxhide_values();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txttodate_Enter(object sender, EventArgs e)
        {
            try
            {
                listboxhide_values();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtbilltype_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                listboxhide_values();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtremarks_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                listboxhide_values();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void billno_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                listboxhide_values();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvType.SelectedItems.Count>0)
            {
              // txttype.Text = lvType.SelectedItem.ToString();
            }
        }
        private void lvBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBillType.SelectedItems.Count>0)
            {
               // txtbill_type.Text = lvBillType.SelectedItem.ToString();
            }
        }
        private void lvcancel_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvcancel_type.SelectedItems.Count>0)
            {
                // txtnotcancelled.Text = lvcancel_type.SelectedItem.ToString();
            }
        }
        private void lvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvOrder.SelectedItems.Count>0)
            {
               // txtorder.Text = lvOrder.SelectedItem.ToString();
            }
        }
        private void lvinvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvinvoice.SelectedItems.Count>0)
            {
               // txtinvoice.Text = lvinvoice.SelectedItem.ToString();
            }
        }
        private void lvcounters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvcounters.SelectedItems.Count>0)
            {
               // txtCounter.Text = lvcounters.SelectedItem.ToString();
            }
        }
        private void lvPurType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPurType.SelectedItems.Count>0)
            {
               // txtpurtype.Text = lvPurType.SelectedItem.ToString();
            }
        }


        private void txtCounter_KeyUp(object sender, KeyEventArgs e)
        {
            //string s = txtCounter.Text;
            ////LstEmployee.Visible = true;

            //dynamic list = lvcounters.Items.Cast<string>();

            //dynamic query = from item in listwhere item.Length >= s.Length && item.ToLower().Substring(0, s.Length) == s.ToLower()item;

            //if ((query.Count > 0)) {
            //    dynamic newItems = new List<string>();
            //    foreach (object result_loopVariable in query) {
            //        result = result_loopVariable;
            //        newItems.Add(result);
            //    }

            //    LstEmployee.Items.Clear();
            //    foreach (object newItem_loopVariable in newItems) {
            //        newItem = newItem_loopVariable;
            //        LstEmployee.Items.Add(newItem);
            //    }
            //}
        }
        private void txtpurtype_KeyDown(object sender, KeyEventArgs e)
      {
          try
          {
              if (e.KeyCode == Keys.Enter)
              {
                  int index = lvPurType.FindString(txtpurtype.Text, -1);
                  if (index != 0 && index != -1)
                  {
                      txtpurtype.Text = lvPurType.Items[index].ToString();
                      txtparty_no.Focus();
                      txtparty_no.SelectAll();
                  }
                  else
                  {
                      txtpurtype.Text = "All";
                      txtparty_no.Focus();
                  }
              }
          }
          catch (Exception ex)
          {
              MyMessageBox.ShowBox(ex.ToString(), "Warning");
          }
        }
        private void OnTextBoxKeyDown7(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lvItemsparty.SelectedIndex <lvItemsparty.Items.Count - 1)
                {
                   lvItemsparty.SetSelected(lvItemsparty.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lvItemsparty.SelectedIndex > 0)
                {
                  lvItemsparty.SetSelected(lvItemsparty.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
              lvItemsparty.Visible = false;
              pnlitemspartys.Visible = false;
              txtparty_no.Text = lvItemsparty.SelectedItem.ToString();
              DgNormalGrid.Focus();
              txtfromdate.Focus();
              //txtparty_no.Select();
            }
        }
        private void txtparty_no_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtparty_no.Text.Trim() != null && txtparty_no.Text.Trim() != "")
                {
                    bool isChk = true;
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "SpplierName");
                    cmd.Parameters.AddWithValue("@itemName", txtparty_no.Text);
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    //SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@ActionType", "SelectPartyName");
                    //cmd.Parameters.AddWithValue("@itemName", txtparty_no.Text);
                    //cmd.Parameters.AddWithValue("@ItemCode", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt_selectitem = new DataTable();
                    adp.Fill(dt_selectitem);
                    isChk = false;
                    if (dt_selectitem.Rows.Count > 0)
                    {
                        string tempstr = dt_selectitem.Rows[0]["Ledsel_name"].ToString();
                        // string tempstr = dt_selectitem.Rows[0]["Ledger_name"].ToString();
                        for (int k = 0; k < lvItemsparty.Items.Count; k++)
                        {

                            if (tempstr == lvItemsparty.Items[k].ToString())
                            {
                                isChk = true;
                                lvItemsparty.SetSelected(k, true);
                                txtparty_no.Select();
                                chk = "1";
                                txtparty_no.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "1";
                        if (txtparty_no.Text != "")
                        {
                            string name = txtparty_no.Text.Remove(txtparty_no.Text.Length - 1);
                            txtparty_no.Text = name.ToString();
                            txtparty_no.Select(txtpurtype.Text.Length, 0);
                        }
                        txtparty_no.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                        //  chk = "1";
                    }
                    else
                    {
                        chk = "1";
                    }
                }

                //if (txtparty_no.Text.Trim() != null && txtparty_no.Text.Trim() != "")
                //{
                //    bool isChk = true;
                //    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("@ActionType", "SelectPartyName");
                //    cmd.Parameters.AddWithValue("@itemName", txtparty_no.Text);
                //    cmd.Parameters.AddWithValue("@ItemCode", "");
                //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //    DataTable dt_selectitem = new DataTable();
                //    adp.Fill(dt_selectitem);
                //    isChk = false;
                //    if (dt_selectitem.Rows.Count > 0)
                //    {                                            
                //            DgNormalGrid.Refresh();
                //            DgNormalGrid.DataSource = null;
                //            DgNormalGrid.DataSource = dt_selectitem;
                //            //DgNormalGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //            lbltot_purchase.Text = Convert.ToInt32(dt_selectitem.Rows.Count).ToString();
                //            gridclculation();
                        
                //    }                    
                //}
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtnotcancelled_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtnotcancelled.Text.Trim() != null && txtnotcancelled.Text.Trim() != "")
                {

                    for (int i = 0; i < lvType.Items.Count; i++)
                    {
                        chkStr1 = lvcancel_type.Items[i].ToString();
                        if (txtnotcancelled.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtnotcancelled.Text.Length);
                            bool isChk = false;
                            if (txtnotcancelled.Text.Trim() == chkstr2 || txtnotcancelled.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                lvcancel_type.SetSelected(i, true);
                                txtnotcancelled.Select();
                                chk = "1";
                                txtnotcancelled.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtnotcancelled.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtnotcancelled_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int index = lvcancel_type.FindString(txtnotcancelled.Text, -1);
                    if (index != 0)
                    {
                        txtnotcancelled.Text = lvcancel_type.Items[index].ToString();

                        // txtnotcancelled.Text = lvcancel_type.Items.IndexOf(Select(index)).ToString();
                        txtremarks.Focus();
                        txtremarks.SelectAll();
                    }
                    else
                    {
                        txtnotcancelled.Text = lvcancel_type.SelectedItem.ToString();
                        txtremarks.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txttype_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txttype.Text.Trim() != null && txttype.Text.Trim() != "")
                {
                    for (int i = 0; i < lvType.Items.Count; i++)
                    {
                        chkStr1 = lvType.Items[i].ToString();
                        if (txttype.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txttype.Text.Length);
                            bool isChk = false;
                            if (txttype.Text.Trim() == chkstr2 || txttype.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                lvType.SetSelected(i, true);
                                txttype.Select();
                                chk = "1";
                                txttype.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txttype.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txttype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //  txttype.Text = lvType.SelectedItem.ToString();
                    int index = lvType.FindString(txttype.Text, -1);
                    if (index != 0)
                    {
                        txttype.Text = lvType.Items[index].ToString();
                        txtbilltype.Focus();
                        txtbilltype.SelectAll();
                    }
                    else
                    {
                        txttype.Text = lvType.SelectedItem.ToString();
                        txtbilltype.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string chk = "";
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void OnTextBoxKeyDown2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lvcancel_type.SelectedIndex <lvcancel_type.Items.Count - 1)
                {
                   lvcancel_type.SetSelected(lvcancel_type.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (lvcancel_type.SelectedIndex > 0)
                {
                  lvcancel_type.SetSelected(lvcancel_type.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {

               lvcancel_type.Visible = false;
               if (lvcancel_type.SelectedItems.Count > 0)
               {
                   txtnotcancelled.Text = lvcancel_type.SelectedItem.ToString();
               }
               txtremarks.Select();
            }

        }
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lvBillType.SelectedIndex < lvBillType.Items.Count - 1)
                {
                   lvBillType.SetSelected(lvBillType.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lvBillType.SelectedIndex > 0)
                {
                   lvBillType.SetSelected(lvBillType.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
               lvBillType.Visible = false;
               if (lvBillType.SelectedItems.Count > 0)
               {
                   txtbill_type.Text = lvBillType.SelectedItem.ToString();
               }
            txtnotcancelled.Select();
            }
        }
        private void OnTextBoxKeyDown1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lvType.SelectedIndex <lvType.Items.Count - 1)
                {
                   lvType.SetSelected(lvType.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lvType.SelectedIndex > 0)
                {
                  lvType.SetSelected(lvType.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
               lvType.Visible = false;
            txttype.Text =lvType.SelectedItem.ToString();
            txtbilltype.Select();
            }
        }
        string chkStr1, chkstr2 = "";
        private void txtbill_type_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtbill_type.Text.Trim() != null && txtbill_type.Text.Trim() != "")
                {

                    for (int i = 0; i < lvBillType.Items.Count; i++)
                    {
                        chkStr1 = lvBillType.Items[i].ToString();
                        if (txtbill_type.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtbill_type.Text.Length);
                            bool isChk = false;
                            if (txtbill_type.Text.Trim() == chkstr2 || txtbill_type.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                lvBillType.SetSelected(i, true);
                                txtbill_type.Select();
                                chk = "1";
                                txtbill_type.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtbill_type.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtbill_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int index = lvBillType.FindString(txtbill_type.Text, -1);
                if (index != 0)
                {
                    txtbill_type.Text = lvBillType.SelectedItem.ToString();
                    txtnotcancelled.Focus();
                    txtnotcancelled.SelectAll();
                }
                else
                {
                    txtbill_type.Text = "All";
                    txtnotcancelled.Focus();
                }
            }
        }
        private void txttype_Click(object sender, EventArgs e)
        {
            txttype.SelectAll();
        }
        private void txtCounter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int index = lvcounters.FindString(txtCounter.Text, -1);
                if (index != 0)
                {
                    txtCounter.Text = lvcounters.Items[index].ToString();
                    txttype.Focus();
                    txttype.SelectAll();
                }
                else
                {
                  //txtCounter.Text =Convert.ToString( lvcounters.Items.Add(dt_counter_table.Rows[0]["ctr_name"].ToString()));
                    txttype.Focus();
                }
            }
        }
        private void txtorder_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int index = lvOrder.FindString(txtorder.Text, -1);
                    if (index != 0)
                    {
                        txtorder.Text = lvOrder.SelectedItem.ToString();
                        txtinvoice.Focus();
                        txtinvoice.SelectAll();
                    }
                    else
                    {
                        txtorder.Text = "Bill Wise";
                        txtinvoice.Focus();
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    //int index = lvOrder.FindString(txtorder.Text, -1);
                    //if (index != -1)
                    {

                        // MessageBox.Show("hi");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void OnTextBoxKeyDown3(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvOrder.SelectedIndex < lvOrder.Items.Count - 1)
                    {
                        lvOrder.SetSelected(lvOrder.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvOrder.SelectedIndex > 0)
                    {
                        lvOrder.SetSelected(lvOrder.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    lvOrder.Visible = false;
                    if (lvOrder.SelectedItems.Count > 0)
                    {
                        txtorder.Text = lvOrder.SelectedItem.ToString();
                    }
                    txtinvoice.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtorder_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtorder.Text.Trim() != null && txtorder.Text.Trim() != "")
                {
                    for (int i = 0; i < lvOrder.Items.Count; i++)
                    {
                        chkStr1 = lvOrder.Items[i].ToString();
                        if (txtorder.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtorder.Text.Length);
                            bool isChk = false;
                            if (txtorder.Text.Trim() == chkstr2 || txtorder.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                lvOrder.SetSelected(i, true);
                                txtorder.Select();
                                chk = "1";
                                txtorder.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtorder.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void OnTextBoxKeyDown4(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvinvoice.SelectedIndex < lvinvoice.Items.Count - 1)
                    {
                        lvinvoice.SetSelected(lvinvoice.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvinvoice.SelectedIndex > 0)
                    {
                        lvinvoice.SetSelected(lvinvoice.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    lvinvoice.Visible = false;
                    if (lvinvoice.SelectedItems.Count > 0)
                    {
                        txtinvoice.Text = lvinvoice.SelectedItem.ToString();
                    }
                    txtbill_type.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }

        }
        private void txtinvoice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtinvoice.Text.Trim() != null && txtinvoice.Text.Trim() != "")
                {

                    for (int i = 0; i < lvinvoice.Items.Count; i++)
                    {
                        chkStr1 = lvinvoice.Items[i].ToString();
                        if (txtinvoice.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtinvoice.Text.Length);
                            bool isChk = false;
                            if (txtinvoice.Text.Trim() == chkstr2 || txtinvoice.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                lvinvoice.SetSelected(i, true);
                                txtinvoice.Select();
                                chk = "1";
                                txtinvoice.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtinvoice.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void OnTextBoxKeyDown5(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvPurType.SelectedIndex < lvPurType.Items.Count - 1)
                    {
                        lvPurType.SetSelected(lvPurType.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvPurType.SelectedIndex > 0)
                    {
                        lvPurType.SetSelected(lvPurType.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    lvPurType.Visible = false;
                    if (lvPurType.SelectedItems.Count > 0)
                    {
                        txtpurtype.Text = lvPurType.SelectedItem.ToString();
                    }
                    txtparty_no.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtpurtype_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpurtype.Text.Trim() != null && txtpurtype.Text.Trim() != "")
                {
                    bool isChk = true;
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "PurchaseLs");
                    cmd.Parameters.AddWithValue("@itemName", txtpurtype.Text);
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt_purtyp = new DataTable();
                    dt_purtyp.Rows.Clear();
                    adp.Fill(dt_purtyp);
                    isChk = false;
                    if (dt_purtyp.Rows.Count > 0)
                    {
                        isChk = false;
                        string tempstr = dt_purtyp.Rows[0]["PurType_Name"].ToString();
                        for (int k = 0; k < lvPurType.Items.Count; k++)
                        {

                            if (tempstr == lvPurType.Items[k].ToString())
                            {
                                isChk = true;
                                lvPurType.SetSelected(k, true);
                                txtpurtype.Select();
                                chk = "1";
                                txtpurtype.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "1";
                        if (txtpurtype.Text != "")
                        {
                            string name = txtpurtype.Text.Remove(txtpurtype.Text.Length - 1);
                            txtpurtype.Text = name.ToString();
                            txtpurtype.Select(txtpurtype.Text.Length, 0);

                        }
                        txtpurtype.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                        //  chk = "1";
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
            
        }
        private void txtinvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int index = lvinvoice.FindString(txtinvoice.Text, -1);
                    if (index != 0)
                    {
                        txtinvoice.Text = lvinvoice.SelectedItem.ToString();
                        txtbill_type.Focus();
                        txtbill_type.SelectAll();
                    }
                    else
                    {
                        txtinvoice.Text = "All";
                        txtbill_type.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtremarks_TextChanged(object sender, EventArgs e)
        {   
        }
        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCounter.Focus();
            }
        }
        private void OnTextBoxKeyDown6(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvcounters.SelectedIndex < lvcounters.Items.Count - 1)
                    {
                        lvcounters.SetSelected(lvcounters.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvcounters.SelectedIndex > 0)
                    {
                        lvcounters.SetSelected(lvcounters.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    lvcounters.Visible = false;
                    txtCounter.Text = lvcounters.SelectedItem.ToString();
                    txttype.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtCounter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCounter.Text.Trim() != null && txtCounter.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    // SqlCommand cmd = new SqlCommand("Select * from Counter_table where ctr_name like '" +  txtCounter.Text.Trim() + "%'", con);
                    SqlCommand cmd = new SqlCommand("", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "CounterType");
                    cmd.Parameters.AddWithValue("@itemName", txtCounter.Text);
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt_selectitem = new DataTable();

                    bool isChk = true;
                    if (dt_selectitem.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dt_selectitem.Rows[0]["ctr_name"].ToString();
                        for (int k = 0; k < lvcounters.Items.Count; k++)
                        {
                            if (tempstr == lvcounters.Items[k].ToString())
                            {
                                lvcounters.SetSelected(k, true);
                                txtCounter.Select();
                                chk = "1";
                                txtCounter.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtparty_no.Text != "")
                        {
                            string name = txtCounter.Text.Remove(txtCounter.Text.Length - 1);
                            txtCounter.Text = name.ToString();
                            txtCounter.Select(txtCounter.Text.Length, 0);
                        }
                        txtCounter.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtbilltype_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtbilltype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                billno.Focus();
            }
        }

        private void billno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpurtype.Focus();
            }
        }

        private void txtnotcancelled_Click(object sender, EventArgs e)
        {
            txtnotcancelled.SelectAll();
        }

        private void txtpurtype_Click(object sender, EventArgs e)
        {
            txtpurtype.Select();
        }

        private void lvcounters_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvcounters.SelectedItems.Count > 0)
                {
                    txtCounter.Text = lvcounters.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvPurType_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvPurType.SelectedItems.Count > 0)
                {
                    txtpurtype.Text = lvPurType.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void lvinvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvinvoice.SelectedItems.Count > 0)
                {
                    txtinvoice.Text = lvinvoice.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvOrder.SelectedItems.Count > 0)
                {
                    txtorder.Text = lvOrder.SelectedItem.ToString();
                }
            }
             
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvcancel_type_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvcancel_type.SelectedItems.Count > 0)
                {
                    txtnotcancelled.Text = lvcancel_type.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvBillType_Click(object sender, EventArgs e)
        {
            if (lvBillType.SelectedItems.Count > 0)
            {
                txtbill_type.Text = lvBillType.SelectedItem.ToString();
            }
        }
        private void lvType_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvType.SelectedItems.Count > 0)
                {
                    txttype.Text = lvType.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtbill_type_Click(object sender, EventArgs e)
        {
            txtbill_type.SelectAll();
        }

        private void txtparty_no_Click(object sender, EventArgs e)
        {
            txtparty_no.SelectAll();
        }

        private void txtorder_Click(object sender, EventArgs e)
        {
            txtorder.SelectAll();
        }

        private void txtinvoice_Click(object sender, EventArgs e)
        {
            txtinvoice.SelectAll();
        }

        private void txtCounter_Click(object sender, EventArgs e)
        {
            txtCounter.SelectAll();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void btn_Exitss_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Enter)
            {
                txttodate.Focus();
            }
        }
        private void txttodate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Enter)
            {
                txtorder.Focus();
            }
        }

        private void DgNormalGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceSales = new Microsoft.Reporting.WinForms.ReportDataSource();

        private void btn_PRINT_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewerSales.Reset();
                Dataset.DsPurchaseNormal dsobj = new Dataset.DsPurchaseNormal();
                for (int k = 0; k < DgDetailGrid.Rows.Count - 1; k++)
                {
                    string Fdate1 = (Convert.ToDateTime(txtfromdate.Value).Day + "/" + Convert.ToDateTime(txtfromdate.Value).Month + "/" + Convert.ToDateTime(txtfromdate.Value).Year);
                    string Tdate2 = (Convert.ToDateTime(txttodate.Value).Day + "/" + Convert.ToDateTime(txttodate.Value).Month + "/" + Convert.ToDateTime(txttodate.Value).Year);
                    if (!string.IsNullOrEmpty(DgDetailGrid.Rows[k].Cells["items_name"].Value.ToString()))
                    {
                        string Dates = (Convert.ToDateTime(DgDetailGrid.Rows[k].Cells["Column2"].Value).Day + "/" + Convert.ToDateTime(DgDetailGrid.Rows[k].Cells["Column2"].Value).Month + "/" + Convert.ToDateTime(DgDetailGrid.Rows[k].Cells["Column2"].Value).Year);
                        dsobj.Tables["DataTable2"].Rows.Add(Fdate1.ToString(), Tdate2.ToString(), txtorder.Text.Trim(), txtinvoice.Text.Trim(), txtbill_type.Text.Trim(), txtnotcancelled.Text.Trim(), txtremarks.Text.Trim(), txtCounter.Text.Trim(), txttype.Text.Trim(), txtbilltype.Text.Trim(), billno.Text.Trim(), txtpurtype.Text.Trim(), txtparty_no.Text.Trim(), DgDetailGrid.Rows[k].Cells["Column1"].Value.ToString(), Dates, DgDetailGrid.Rows[k].Cells["items_name"].Value.ToString(), DgDetailGrid.Rows[k].Cells["unit_name_unit"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column3"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column6"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column8"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column9"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column10"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column11"].Value.ToString(), DgDetailGrid.Rows[k].Cells["Column12"].Value.ToString());
                    }
                }
                ReportDataSource ds = new ReportDataSource("DsPurchaseNormal", dsobj.Tables["DataTable2"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);
               // reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcPurchaseDetails.rdlc";
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.rdlcPurchaseDetails.rdlc";
                //Passing Parmetes:
                ReportParameter rp = new ReportParameter("Number", "200", false);
                //ReportParameter rp2 = new ReportParameter("DateTo", "300");

                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rp });
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

        private void btnNorprint_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewerSales.Reset();

                Dataset.DsPurchaseNormal dsobj = new Dataset.DsPurchaseNormal();
                for (int k = 0; k < DgNormalGrid.Rows.Count - 1; k++)
                {
                    string Fdate1 = Convert.ToString(Convert.ToDateTime(DgNormalGrid.Rows[k].Cells[0].Value).Day + "/" + Convert.ToDateTime(DgNormalGrid.Rows[k].Cells[0].Value).Month + "/" + Convert.ToDateTime(DgNormalGrid.Rows[k].Cells[0].Value).Year);
                    string Tdate2 = (Convert.ToDateTime(DgNormalGrid.Rows[k].Cells[2].Value).Day + "/" + Convert.ToDateTime(DgNormalGrid.Rows[k].Cells[2].Value).Month + "/" + Convert.ToDateTime(DgNormalGrid.Rows[k].Cells[2].Value).Year);

                    dsobj.Tables["DataTable1"].Rows.Add(Fdate1.ToString(), DgNormalGrid.Rows[k].Cells[1].Value, Tdate2.ToString(), DgNormalGrid.Rows[k].Cells[3].Value, DgNormalGrid.Rows[k].Cells[4].Value, DgNormalGrid.Rows[k].Cells[5].Value, Convert.ToString(Convert.ToDateTime(txtfromdate.Value).Day + "/" + Convert.ToDateTime(txtfromdate.Value).Month + "/" + Convert.ToDateTime(txtfromdate.Value).Year), Convert.ToString(Convert.ToDateTime(txttodate.Value).Day + "/" + Convert.ToDateTime(txttodate.Value).Month + "/" + Convert.ToDateTime(txttodate.Value).Year), txtorder.Text.Trim(), txtinvoice.Text.Trim(), txtbill_type.Text.Trim(), txtnotcancelled.Text.Trim(), txtremarks.Text.Trim(), txtremarks.Text.Trim(), txtCounter.Text.Trim(), txttype.Text.Trim(), txtbilltype.Text.Trim(), billno.Text.Trim(), txtpurtype.Text.Trim(), txtparty_no.Text.Trim());
                }
                ReportDataSource ds = new ReportDataSource("DsPurchaseNormal", dsobj.Tables["DataTable1"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);
               // reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.RdlcPurchaseNormal.rdlc";
                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.RdlcPurchaseNormal.rdlc";
                //Passing Parmetes:
                //Passing Parmetes:
                ReportParameter rp = new ReportParameter("Number", "200", false);
                //ReportParameter rp2 = new ReportParameter("DateTo", "300");

                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rp });
                dt.EndInit();
                reportViewerSales.RefreshReport();
                reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void lvtaxtype_Enter(object sender, EventArgs e)
        {
            //try
            //{
            //    panel7.Visible = false;
            //    // dataGridView2.Visible = false;
            //    pnlcancel_type.Visible = false;
            //    pnlcounter.Visible = false;
            //    pnlinvoice.Visible = false;
            //    pnlitemspartys.Visible = false;
            //    pnlOrder.Visible = false;
            //    pnltype.Visible = false;
            //    pnlbillType.Visible = false;
            //    pnlpurtype.Visible = true;
            //    panel1.Visible = true;
            //    lvcancel_type.Visible = false;
            //    lvcounters.Visible = false;
            //    lvinvoice.Visible = false;
            //    lvItemsparty.Visible = false;
            //    lvOrder.Visible = false;
            //    lvType.Visible = false;
            //    lvBillType.Visible = false;
            //    lvPurType.Visible = false;
            //    lvtaxtype.Visible = true;
            //    txtpurtype.Select();
            //    //  lvPurType.SetSelected(0, true);
            //}
            //catch (Exception ex)
            //{
            //    MyMessageBox.ShowBox(ex.ToString(), "Warning");
            //}

        }

        private void txttaxtype_Enter(object sender, EventArgs e)
        {
            try
            {
                panel7.Visible = false;
                // dataGridView2.Visible = false;
                pnlcancel_type.Visible = false;
                pnlcounter.Visible = false;
                pnlinvoice.Visible = false;
                pnlitemspartys.Visible = false;
                pnlOrder.Visible = false;
                pnltype.Visible = false;
                pnlbillType.Visible = false;
                pnlpurtype.Visible = false;
                panel1.Visible = true;
                lvcancel_type.Visible = false;
                lvcounters.Visible = false;
                lvinvoice.Visible = false;
                lvItemsparty.Visible = false;
                lvOrder.Visible = false;
                lvType.Visible = false;
                lvBillType.Visible = false;
                lvPurType.Visible = false;
                lvtaxtype.Visible = true;
                //txtpurtype.Select();
                txttaxtype.Select();                
                //  lvPurType.SetSelected(0, true);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }


        }

        private void lvtaxtype_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lvType.SelectedItems.Count > 0)
            {
            //  txttaxtype.Text  =   lvtaxtype.SelectedItem.ToString();
            }
        }

        private void lvtaxtype_Click(object sender, EventArgs e)
        {
            try
            {
                if ( lvtaxtype.SelectedItems.Count > 0)
                {
                    txttaxtype.Text = lvtaxtype.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txttaxtype_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txttype.Text == "Normal")
                {
                    NormalGrid();
                    //DgNormalGrid.Visible = true;
                    //DgDetailGrid.Visible = false;
                    //pnlNormal.Visible = true;
                    //pnlDetails.Visible = false;
                }
                else if (txttype.Text == "Details")
                {
                    DetailGrid();
                    //DgNormalGrid.Visible = false;
                    //DgDetailGrid.Visible = true;
                    //pnlDetails.Visible = true;

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
            
        }


        
    }
}

