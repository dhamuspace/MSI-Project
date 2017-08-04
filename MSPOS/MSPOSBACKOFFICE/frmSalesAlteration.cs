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

namespace MSPOSBACKOFFICE
{
    public partial class frmSalesAlteration : Form
    {
        DataTable autofind = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public frmSalesAlteration()
        {
            InitializeComponent();
            try
            {
                pnl_type.Visible = false;
                lst_type.Visible = false;
                pnl_sales.Visible = false;
                pnl_customer.Visible = false;
                lst_ledger.Visible = false;
                foreach (DataGridViewColumn col in grd_SalesRecord.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                loadSalesrecords();
                grd_SalesRecord.Columns[0].Width = 100;
                grd_SalesRecord.Columns[1].Width = 100;
                grd_SalesRecord.Columns[2].Width = 300;
                grd_SalesRecord.Columns[3].Width = 170;
                grd_SalesRecord.Columns[4].Width = 150;
                grd_SalesRecord.Columns[5].Width = 154;
                grd_SalesRecord.Columns[6].Width = 154;
                grd_SalesRecord.Columns[6].Visible = false;
                int a = grd_SalesRecord.Rows.Count;
                lbl_ItemCount.Text = a.ToString();
                funTotalCalculation();

                // con.Open();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                // string tonameqry = "select smas_name from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                SqlCommand cmdtoname = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmdtoname.CommandType = CommandType.StoredProcedure;
                cmdtoname.Parameters.AddWithValue("@tActionType", "CASHTYPE");
                cmdtoname.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmdtoname);
                adp.Fill(dtNew);
                if (dtNew.Rows.Count > 0)
                {
                    txt_to.Text = dtNew.Rows[0]["smas_name"].ToString();
                }

                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlCommand CounterNameqry = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                CounterNameqry.CommandType = CommandType.StoredProcedure;
                CounterNameqry.Parameters.AddWithValue("@tActionType", "COUNTERNAME");
                CounterNameqry.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp1 = new SqlDataAdapter(CounterNameqry);
                adp1.Fill(dtNew1);
                txt_counter.Text = "";
                if (dtNew1.Rows.Count > 0)
                {
                    txt_counter.Text = dtNew1.Rows[0]["ctr_name"].ToString();
                }

                //string counterqry = "select ctr_no from salmas_table where smas_billno='" + chkbox.SalesBillNo + "' ";
                //SqlCommand countercmd = new SqlCommand(counterqry, con);
                //int counterNo = Convert.ToInt16(countercmd.ExecuteScalar());
                //con.Close();
                //con.Open();
                //string CounterNameqry = "select ctr_name from counter_table where ctr_no='" + counterNo + "'";
                //SqlCommand cmdCtnNo = new SqlCommand(CounterNameqry, con);
                //var tempcounter=cmdCtnNo.ExecuteScalar();
                //if (tempcounter == "")
                //{
                //    txt_counter.Text = cmdCtnNo.ExecuteScalar().ToString();
                //}
                //else
                //{
                //    txt_counter.Text = "";
                //}
                //con.Close();
                //con.Open();

                DataTable dtNew2 = new DataTable();
                dtNew2.Rows.Clear();

                SqlCommand saletypecmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                saletypecmd.CommandType = CommandType.StoredProcedure;
                saletypecmd.Parameters.AddWithValue("@tActionType", "SALESNAME");
                saletypecmd.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp2 = new SqlDataAdapter(saletypecmd);
                adp2.Fill(dtNew2);
                if (dtNew2.Rows.Count > 0)
                {
                    txt_sales.Text = dtNew2.Rows[0][0].ToString();
                }

                //string salestypeQry = "select smas_saltype from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                //SqlCommand saletypecmd = new SqlCommand(salestypeQry, con);
                //string salestypesOp = saletypecmd.ExecuteScalar().ToString();
                //con.Close();
                //if (salestypesOp == "True")
                //{
                //    txt_sales.Text = "Whole Sales";
                //}
                //else
                //{
                //    txt_sales.Text = "Retail Sales";
                //}
                //con.Open();

                DataTable dtNew3 = new DataTable();
                dtNew3.Rows.Clear();

                SqlCommand dccmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                dccmd.CommandType = CommandType.StoredProcedure;
                dccmd.Parameters.AddWithValue("@tActionType", "DONO");
                dccmd.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp3 = new SqlDataAdapter(dccmd);
                adp3.Fill(dtNew3);
                if (dtNew3.Rows.Count > 0)
                {
                    txt_dcno.Text = dtNew3.Rows[0]["dc_no"].ToString();
                }
                //string dcnoqry = "select dc_no from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                //SqlCommand dccmd = new SqlCommand(dcnoqry, con);
                //txt_dcno.Text = dccmd.ExecuteScalar().ToString();
                //con.Close();
                //con.Open();

                DataTable dtNew4 = new DataTable();
                dtNew4.Rows.Clear();

                SqlCommand typeqryCmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                typeqryCmd.CommandType = CommandType.StoredProcedure;
                typeqryCmd.Parameters.AddWithValue("@tActionType", "CASHORNETS");
                typeqryCmd.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp4 = new SqlDataAdapter(typeqryCmd);
                adp4.Fill(dtNew4);
                if (dtNew4.Rows.Count > 0)
                {
                    txt_type.Text = dtNew4.Rows[0][0].ToString();
                }
                //string TypesQry = "select smas_cashmode from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                //SqlCommand typeqryCmd = new SqlCommand(salestypeQry, con);
                //string typeqryOp = saletypecmd.ExecuteScalar().ToString();
                //con.Close();
                //if (typeqryOp == "True")
                //{
                //    txt_type.Text = "Cash";
                //}
                //else
                //{
                //    txt_type.Text = "Nett";
                //}

                //con.Open();

                DataTable dtNew5 = new DataTable();
                dtNew5.Rows.Clear();

                SqlCommand OrnerNoqry = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                OrnerNoqry.CommandType = CommandType.StoredProcedure;
                OrnerNoqry.Parameters.AddWithValue("@tActionType", "ORDERNO");
                OrnerNoqry.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp5 = new SqlDataAdapter(OrnerNoqry);
                adp5.Fill(dtNew5);
                if (dtNew5.Rows.Count > 0)
                {
                    txt_order.Text = dtNew5.Rows[0][0].ToString();
                }

                //string OrnerNoqry = "select order_no from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                //SqlCommand cmdorderno = new SqlCommand(OrnerNoqry, con);
                //txt_order.Text = cmdorderno.ExecuteScalar().ToString();
                //con.Close();

                DataTable dtNew6 = new DataTable();
                dtNew6.Rows.Clear();

                SqlCommand OrnerNoqry1 = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                OrnerNoqry1.CommandType = CommandType.StoredProcedure;
                OrnerNoqry1.Parameters.AddWithValue("@tActionType", "DISCOUNTVALUE");
                OrnerNoqry1.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp7 = new SqlDataAdapter(OrnerNoqry1);
                adp7.Fill(dtNew6);
                if (dtNew6.Rows.Count > 0)
                {
                    lblDiscount.Text = dtNew6.Rows[0][0].ToString();
                }

                txt_date.Text = chkbox.DateSalesEntry;
                txt_Billno.Text = chkbox.SalesBillNo;
                grd_SalesRecord.Focus();
                SqlCommand namecmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                namecmd.CommandType = CommandType.StoredProcedure;
                namecmd.Parameters.AddWithValue("@tActionType", "ITEMDETAIL");
                namecmd.Parameters.AddWithValue("@tValue", "1");
                SqlDataAdapter adp6 = new SqlDataAdapter(namecmd);
                adp6.Fill(autofind);


                //SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table where Item_Active=" + 1 + " order by Item_name ASC", con);
                //SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
                //nameadp.Fill(autofind);

                grd_SalesRecord.DefaultCellStyle.Font = new Font("Tahoma", 12);
                grd_SalesRecord.RowTemplate.Height = 25;

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private DataTable AutoNumberedTable(DataTable dtgrdload)
        {
            DataTable ResultTable = new DataTable();
            DataColumn AutoNumberColumn = new DataColumn();
            AutoNumberColumn.ColumnName = "S.no";

            AutoNumberColumn.DataType = typeof(int);

            AutoNumberColumn.AutoIncrement = true;

            AutoNumberColumn.AutoIncrementSeed = 1;

            AutoNumberColumn.AutoIncrementStep = 1;

            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(dtgrdload);

            return ResultTable;

        }
        DataTable dtgrdload = new DataTable();
        int deletedRecNo;
        public void loadSalesrecords()
        {
            try
            {
                DataTable dtNew5 = new DataTable();
                dtNew5.Rows.Clear();

                SqlCommand smasrecordNo = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                smasrecordNo.CommandType = CommandType.StoredProcedure;
                smasrecordNo.Parameters.AddWithValue("@tActionType", "SALMASNOCHANGE");
                smasrecordNo.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp5 = new SqlDataAdapter(smasrecordNo);
                adp5.Fill(dtNew5);
                if (dtNew5.Rows.Count > 0)
                {
                    deletedRecNo = int.Parse(dtNew5.Rows[0][0].ToString());
                }

                DataTable dtNew6 = new DataTable();
                dtNew6.Rows.Clear();

                SqlCommand cmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "ITEMFILL");
                cmd.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp6 = new SqlDataAdapter(cmd);
                adp6.Fill(dtgrdload);
                grd_SalesRecord.DataSource = AutoNumberedTable(dtgrdload);

                //con.Open();
                //string smasrecordNo = "select smas_no from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                //SqlCommand cmdRecordNo = new SqlCommand(smasrecordNo, con);
                //int Billno = Convert.ToInt16(cmdRecordNo.ExecuteScalar());
                //deletedRecNo = Billno;
                //con.Close();
                //con.Open();
                //string querytable = "Select Item_table.Item_code,Item_table.Item_name,stktrn_table.nt_qty , stktrn_table.Rate, stktrn_table.Amount from stktrn_table,Item_table where Item_table.Item_no=stktrn_table.item_no and stktrn_table.strn_no='" + Billno + "'";
                //SqlCommand cmd = new SqlCommand(querytable, con);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(dtgrdload);  
                //grd_SalesRecord.DataSource = AutoNumberedTable(dtgrdload);
                //con.Close();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void grd_SalesRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbox.FormIdentify == "SalesSummary")
                {
                    //frmSalesSummaryDetails frm = new frmSalesSummaryDetails();
                    //frm.BringToFront();
                    //frm.MdiParent = this.ParentForm;
                    //frm.StartPosition = FormStartPosition.Manual;
                    //frm.WindowState = FormWindowState.Normal;
                    //frm.Location = new Point(0, 80);
                    //frm.Show();

                    this.Close();
                }
                if (chkbox.FormIdentify == "ItemLedger")
                {
                    ItemLedger frm = new ItemLedger();
                    frm.BringToFront();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
      //  SqlDataReader dr;
        string chk;
        private void txt_to_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_to.Text.Trim() != null && txt_to.Text.Trim() != "")
                {
                    DataTable dtNew5 = new DataTable();
                    dtNew5.Rows.Clear();

                    SqlCommand cmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "LEDGERLIKE");
                    cmd.Parameters.AddWithValue("@tValue", txt_to.Text.Trim());
                    SqlDataAdapter adp5 = new SqlDataAdapter(cmd);
                    adp5.Fill(dtNew5);
                    bool isChk = false;
                    for (int mn = 0; mn < dtNew5.Rows.Count; mn++)
                    {
                        isChk = true;
                        string tempStr = dtNew5.Rows[mn]["Ledger_name"].ToString();
                        for (int i = 0; i < lst_ledger.Items.Count; i++)
                        {
                            if (dtNew5.Rows[mn]["Ledger_name"].ToString() == lst_ledger.Items[i].ToString())
                            {

                                lst_ledger.SetSelected(i, true);
                                txt_to.Select();
                                chk = "1";
                                txt_to.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    con.Close();
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_to.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                }
                else
                {
                    chk = "1";
                }


                //SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_name like '" + txt_to.Text.Trim() + "%'", con);
                //if (dr != null)
                //{
                //    dr.Close();
                //}
                ////   dr.Close();
                //dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
              
        }

        public void customerDetails()
        {
            try
            {
                //            con.Open();
                SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_gno in (202,31) ", con);
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
                //  con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }


        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_to_Enter(object sender, EventArgs e)
        {
            try
            {
                pnl_customer.Visible = true;
                lst_ledger.Visible = true;
                customerDetails();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_to_KeyDown(object sender, KeyEventArgs e)
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
                    pnl_customer.Visible = false;
                    lst_ledger.Visible = false;
                    if (lst_ledger.Text != "")
                    {
                        txt_to.Text = lst_ledger.SelectedItem.ToString();
                        txt_ToAddress.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        int i;
        private void txt_ToAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    i = i + 1;
                }


                if (i == 5)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        grd_SalesRecord.Focus();
                        grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[0].Cells["Item_code"];
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }

        private void txt_tin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_CST.Focus();
            }
        }

        private void txt_CST_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                grd_SalesRecord.Focus();
                int rows = grd_SalesRecord.CurrentCell.RowIndex;
                int cols = grd_SalesRecord.CurrentCell.ColumnIndex;
                grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[rows].Cells[cols];
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_counter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_counter.Text.Trim() != null && txt_counter.Text.Trim() != "")
                {
                    DataTable dtNew5 = new DataTable();
                    dtNew5.Rows.Clear();

                    SqlCommand cmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "COUNTERNAMELIKE");
                    cmd.Parameters.AddWithValue("@tValue", txt_counter.Text.Trim());
                    SqlDataAdapter adp5 = new SqlDataAdapter(cmd);
                    adp5.Fill(dtNew5);
                    bool isChk = false;
                    for (int mn = 0; mn < dtNew5.Rows.Count; mn++)
                    {

                        // SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table where ctr_name like '" + txt_counter.Text.Trim() + "%'", con);
                        //if (dr != null)
                        //{
                        //    dr.Close();
                        //}
                        ////   dr.Close();
                        //dr = cmd.ExecuteReader();              

                        isChk = true;
                        string tempStr = dtNew5.Rows[mn]["ctr_name"].ToString();
                        for (int i = 0; i < lst_ledger.Items.Count; i++)
                        {
                            if (dtNew5.Rows[mn]["ctr_name"].ToString() == lst_ledger.Items[i].ToString())
                            {

                                lst_ledger.SetSelected(i, true);
                                txt_counter.Select();
                                chk = "1";
                                txt_counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }

                        }
                    }
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

        private void txt_counter_KeyDown(object sender, KeyEventArgs e)
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
                    pnl_customer.Visible = false;
                    lst_ledger.Visible = false;
                    if (lst_ledger.Text != "")
                    {
                        txt_counter.Text = lst_ledger.SelectedItem.ToString();
                        txt_date.Focus();
                    }

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
                pnl_customer.Visible = true;
                lst_ledger.Visible = true;
                counternameload();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void counternameload()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select ctr_name from counter_table", con);
                SqlDataAdapter asd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                lst_ledger.Items.Clear();
                dt.Rows.Clear();
                asd.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        lst_ledger.Items.Add(dt.Rows[k]["ctr_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_type.Focus();
            }
        }



        private void txt_type_Enter(object sender, EventArgs e)
        {
            pnl_type.Visible = true;
            lst_type.Visible = true;
            lst_type.Focus();
            lst_type.SelectedValue = txt_type.Text;

        }

        private void txt_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_counter.Text != "")
                {
                    txt_sales.Focus();
                }
            }
            if (e.KeyCode == Keys.Down)
            {

            }
        }

        private void lst_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_type.Text = lst_type.SelectedItem.ToString();
        }

        private void lst_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_type.Text = lst_type.SelectedItem.ToString();
                pnl_type.Visible = false;
                txt_sales.Focus();
            }
        }

        private void txt_sales_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_sales_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt_sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_sales.Text != "")
                {
                    txt_dcno.Focus();

                }

            }
        }

        private void txt_sales_Enter(object sender, EventArgs e)
        {
            pnl_sales.Visible = true;
            lst_sales.Focus();
        }

        private void lst_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_sales.SelectedItems.Count > 0)
            {
                txt_sales.Text = lst_sales.SelectedItem.ToString();
            }
        }

        private void lst_sales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (lst_sales.SelectedItems.Count > 0)
                    {
                        txt_sales.Text = lst_sales.SelectedItem.ToString();
                    }
                    txt_order.Focus();
                    pnl_sales.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_order_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_dcno.Focus();
            }
        }

        private void txt_dcno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    grd_SalesRecord.Focus();
                    int rows = grd_SalesRecord.CurrentCell.RowIndex;
                    int cols = grd_SalesRecord.CurrentCell.ColumnIndex;
                    grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[rows].Cells[cols];
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        double Tax_amt, amt, Taxvalue,Profit;
        string value;
        int CounterNO;
        int PartyNo;
        double ClosingQty, NetSalVal;
        double TotalamtGrossamt=0,BillAmtTotal=0;
        string TaxValue;
        int getQty = 0;
        string altName = "";
        int altQty = 0, altQty1 = 0, tempQty = 0;
        private void btn_Save_Click(object sender, EventArgs e)
        {

            //if (grd_SalesRecord.Rows.Count > 0)
            //{
            //    if (txt_to.Text.Trim() == "Cash Sales")
            //    {
            //        SqlCommand cmd = new SqlCommand("sp_btnCashSettleHome", con);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lbl_Gross_Amt.Text.ToString()));
            //        cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lbl_Billamt.Text.ToString()));
            //        //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
            //        cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lbl_Gross_Amt.Text.ToString()));

            //        double tot = ((double.Parse(lbl_Billamt.Text.ToString()) - double.Parse("0.00")) - (double.Parse(lbl_Gross_Amt.Text.ToString()) +(double.Parse(lbl_Billamt.Text.ToString())- double.Parse(lbl_Gross_Amt.Text.ToString()))));
            //        cmd.Parameters.AddWithValue("@RoundValue", tot);
            //        cmd.Parameters.AddWithValue("@tempTable", dt);
            //        if (double.Parse(lblDiscount.Content.ToString()) > 0)
            //        {
            //            cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
            //            cmd.Parameters.AddWithValue("@DiscountType", _Class.clsVariables.DiscountType);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
            //            cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
            //        }

            //        con.Close();
            //        con.Open();
            //        cmd.ExecuteNonQuery();

            //        gridItems.DataSource = null;  // Change gridItems.ItemsSource = null;
            //        dt.Clear();
            //        lblNetAmt.Content = "0.00";
            //        lblDiscount.Content = "0.00";
            //        lblTotQty.Content = "0.00";
            //        lblTotAmt.Content = "0.00";
            //        lblTaxAmt.Content = "0.00";
            //        // funPreviousBill();
            //    }
            //}
            //else
            //{

            //}
            // delete the old record and insert the new grdiview values..
            // update the alteration values.
            try
            {
                con.Close();
                con.Open();
                SqlTransaction trans = null;
                trans = con.BeginTransaction();
                for (int ij = 0; ij < dt1.Rows.Count; ij++)
                {
                    if (dt1.Rows[ij]["nt_qty"].ToString() != "" && dt1.Rows[ij]["nt_qty"].ToString() != "0")
                    {
                        double OldQty = Convert.ToDouble(dt1.Rows[ij]["nt_qty"].ToString());

                        string ItemNoqry = "select Item_no from Item_table where Item_name= @tItem_Name";
                        SqlCommand cmdItemNo = new SqlCommand(ItemNoqry, con);
                        cmdItemNo.Parameters.AddWithValue("@tItem_Name", dt1.Rows[ij]["Item_name"].ToString());
                        cmdItemNo.Transaction = trans;
                        
                        int OldItemNO = Convert.ToInt16(cmdItemNo.ExecuteScalar());

                        string getSalQty = "select nt_salqty from Item_table where Item_no='" + OldItemNO + "' ";
                        SqlCommand cmdSalQty = new SqlCommand(getSalQty, con);
                        cmdSalQty.Transaction = trans;
                        double OldSalQty = Convert.ToDouble(cmdSalQty.ExecuteScalar());

                        double CalSalQty = OldSalQty - OldQty;

                        // Get a old sales value:
                        double Old_Sal_val = Convert.ToDouble(dt1.Rows[ij]["Amount"].ToString());

                        string getSalval = "select Nt_Salval from Item_table where Item_no='" + OldItemNO + "' ";
                        SqlCommand cmdSalval = new SqlCommand(getSalval, con);
                        cmdSalval.Transaction = trans;
                        double OldSalval = Convert.ToDouble(cmdSalval.ExecuteScalar());

                        double Ca_Sal_Val = OldSalval - Old_Sal_val;

                        // get a old closing quantity:

                        string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + OldItemNO + "' ";
                        SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                        cmdClosing.Transaction = trans;
                        double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                        double OldClosingQty = ClosingQty + OldQty;

                        SqlCommand cmdUpdate = new SqlCommand("Update Item_table set nt_salqty=" + CalSalQty + ",nt_cloqty=" + OldClosingQty + ",Nt_Salval=" + Ca_Sal_Val + " where Item_no='" + OldItemNO + "' ", con);
                        cmdUpdate.Transaction = trans;
                        cmdUpdate.ExecuteNonQuery();
                      

                        string oldrecord = "select strn_sno from stktrn_table where strn_no='" + deletedRecNo + "' and strn_type=1";
                        SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                        cmdoldRecord.Transaction = trans;
                        SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                            //delete a Old Record in Stkrtn_table:

                            string deleteOldRecord = "Delete from stktrn_table where strn_sno=@strn_sno";
                            SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                            cmddeleteQry.Parameters.AddWithValue("@strn_sno", dt1.Rows[ij]["Id"].ToString());
                            cmddeleteQry.Transaction = trans;
                            cmddeleteQry.ExecuteNonQuery();
                            
                        }
                    }
                }
                for (int ij = 0; ij < grd_SalesRecord.Rows.Count - 1; ij++)
                {
                    if (grd_SalesRecord.Rows[ij].Cells["nt_qty"].Value.ToString() != "" && grd_SalesRecord.Rows[ij].Cells["nt_qty"].Value.ToString() != "0")
                    {
                        // Get a new sales value:
                        double New_L2_Q = Convert.ToDouble(grd_SalesRecord.Rows[ij].Cells["nt_qty"].Value.ToString());
                       
                        string ItemNoqry2 = "select Item_no from Item_table where Item_name=@tItemName";
                        SqlCommand cmdItemNo2 = new SqlCommand(ItemNoqry2, con);
                        cmdItemNo2.Transaction = trans;
                        cmdItemNo2.Parameters.AddWithValue("@tItemName", grd_SalesRecord.Rows[ij].Cells["Item_name"].Value.ToString());
                        int NewItemNo2 = Convert.ToInt16(cmdItemNo2.ExecuteScalar());

                        string getSalQty1 = "select nt_salqty from Item_table where Item_no='" + NewItemNo2 + "' ";
                        SqlCommand cmdSalQty1 = new SqlCommand(getSalQty1, con);
                        cmdSalQty1.Transaction = trans;
                        double NewSalQty = Convert.ToDouble(cmdSalQty1.ExecuteScalar());

                        double Ca_L1_Q = NewSalQty + New_L2_Q;

                        // Get a new sales value:
                        double New_Sal_val = Convert.ToDouble(grd_SalesRecord.Rows[ij].Cells["Amount"].Value.ToString());

                        string getSalval = "select Nt_Salval from Item_table where Item_no='" + NewItemNo2 + "' ";
                        SqlCommand cmdSalval = new SqlCommand(getSalval, con);
                        cmdSalval.Transaction = trans;
                        double NewSalval = Convert.ToDouble(cmdSalval.ExecuteScalar());

                        double Ca_Sal1_Val = NewSalval + New_Sal_val;

                        // get a new closing quantity:                     
                        string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + NewItemNo2 + "' ";
                        SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                        cmdClosing.Transaction = trans;
                        double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                        double NewClosingQty = ClosingQty - New_L2_Q;

                        SqlCommand cmdUpdate1 = new SqlCommand("Update Item_table set nt_salqty=" + Ca_L1_Q + ",nt_cloqty=" + NewClosingQty + ",Nt_Salval=" + Ca_Sal1_Val + " where Item_no='" + NewItemNo2 + "' ", con);
                        cmdUpdate1.Transaction = trans;
                        cmdUpdate1.ExecuteNonQuery();
                       
                        if (dt1.Rows.Count == 0)
                        {
                            string oldrecord = "select strn_sno from stktrn_table where strn_no='" + deletedRecNo + "' and strn_type=1";
                            SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                            cmdoldRecord.Transaction = trans;
                            SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            for (int i = 0; i < dt.Rows.Count;i++)
                            {
                                int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                                //delete a Old Record in Stkrtn_table:
                                
                                string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "'";
                                SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                cmddeleteQry.Transaction = trans;
                                cmddeleteQry.ExecuteNonQuery();
                                
                            }
                        }
                    }
                }
               
                //string oldrecord = "select strn_sno from stktrn_table where strn_no='" + deletedRecNo + "'";
                //SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                //SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                //DataTable dt = new DataTable();
                //adp.Fill(dt);
                for (int i = 0; i < grd_SalesRecord.Rows.Count - 1; i++)
                {
                    if (grd_SalesRecord.Rows[i].Cells["nt_qty"].Value.ToString() != "" && grd_SalesRecord.Rows[i].Cells["nt_qty"].Value.ToString() != "0")
                    {
                        // int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                        // MessageBox.Show("Deleted Record NO" + oldrecordno);

                        // get a Id for Billmas_table:
                        //con.Close();
                        //con.Open();
                        //string BillNo_samas = "select strn_no from stktrn_table where strn_sno='" + oldrecordno + "'";
                        //SqlCommand cmdBillNo = new SqlCommand(BillNo_samas, con);
                        //int olditemno = Convert.ToInt16(cmdBillNo.ExecuteScalar());
                        // con.Close();
                        int olditemno = deletedRecNo;
                        //// get a Max value in Stktrn_table strn_sno:
                        //con.Open();
                        //string newidqry="select Max(strn_sno) from stktrn_table";
                        //SqlCommand cmdnewid=new SqlCommand(newidqry,con);
                        //int Newid = Convert.ToInt16(cmdnewid.ExecuteScalar());
                        //con.Close();

                        // ledger group no:

                        string PartyNoqry = "select Ledger_no from Ledger_table where Ledger_name='" + txt_to.Text + "'";
                        SqlCommand cmdParty = new SqlCommand(PartyNoqry, con);
                        cmdParty.Transaction = trans;
                        PartyNo = Convert.ToInt16(cmdParty.ExecuteScalar());

                        // Counter Number:
                        string counterqry = "select ctr_no from counter_table where ctr_name='" + txt_counter.Text + "'";
                        SqlCommand cmdCounter = new SqlCommand(counterqry, con);
                        cmdCounter.Transaction = trans;
                        CounterNO = Convert.ToInt16(cmdCounter.ExecuteScalar());


                        // insert into stktrn_table:

                        //foreach (DataGridViewRow row in grd_SalesRecord.Rows)
                        //{
                        //    if (!row.IsNewRow)
                        //    {
                        if (grd_SalesRecord.Rows[i].Cells[2].Value != null)
                        {

                            // get a Max value in Stktrn_table strn_sno:

                            string newidqry = "select Max(strnsno)+1 from numbertable";
                            SqlCommand cmdnewid = new SqlCommand(newidqry, con);
                            cmdnewid.Transaction = trans;
                            int Newid = Convert.ToInt16(cmdnewid.ExecuteScalar());


                            // get a Item_Code number from Item_table:

                            string ItemNoqry = "select Item_no from Item_table where  Item_name=@tItemName";
                            SqlCommand cmdItemNo = new SqlCommand(ItemNoqry, con);
                            cmdItemNo.Transaction = trans;
                            cmdItemNo.Parameters.AddWithValue("@tItemName", grd_SalesRecord.Rows[i].Cells[2].Value);
                            int ItemNO = Convert.ToInt16(cmdItemNo.ExecuteScalar());


                            // get a Tax_no number from Item_table:

                            string taxnoqry = "select Tax_no from Item_table where Item_name=@tItemName";
                            SqlCommand cmdtaxno = new SqlCommand(taxnoqry, con);
                            cmdtaxno.Transaction = trans;
                            cmdtaxno.Parameters.AddWithValue("@tItemName", grd_SalesRecord.Rows[i].Cells[2].Value);
                            int TaxNo = Convert.ToInt16(cmdtaxno.ExecuteScalar());

                            // get a taxName by Tax No from Tax_table:

                            string TaxnameQry = "select Nt_percent from Tax_table where Tax_no='" + TaxNo + "'";
                            SqlCommand cmdtaxname = new SqlCommand(TaxnameQry, con);
                            cmdtaxname.Transaction = trans;
                            if (cmdtaxname.ExecuteScalar() != null)
                            {
                                TaxValue = cmdtaxname.ExecuteScalar().ToString();
                            }
                            else
                            {
                                TaxValue = "0";
                            }

                            //get a unitno from Item_name:

                            string ItemUnitqry = "select Unit_no from Item_table where Item_name=@tItemName";
                            SqlCommand cmditemUnit = new SqlCommand(ItemUnitqry, con);
                            cmditemUnit.Transaction = trans;
                            cmditemUnit.Parameters.AddWithValue("@tItemName", grd_SalesRecord.Rows[i].Cells[2].Value);
                            string ItemUnit = cmditemUnit.ExecuteScalar().ToString();

                            //get a unitno from Item_name:
                            //con.Open();
                            //string unitnoqry = "select unit_name from unit_table where unit_name='" + ItemUnit + "'";
                            //SqlCommand cmdunit = new SqlCommand(unitnoqry, con);
                            //int Unitno = Convert.ToInt16(cmdunit.ExecuteScalar());
                            //con.Close();
                            //Tax Value Calculatuion:

                            value = TaxValue;
                            Taxvalue = Double.Parse(value);
                            amt = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[5].Value);
                            Tax_amt = amt * Taxvalue / 100;

                            ////// Gross amt and Bill amount Calculation:
                            ////double singlebillamt = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[5].Value.ToString());
                            ////TotalamtGrossamt = TotalamtGrossamt + singlebillamt;

                            ////// total of Bill amt:
                            ////BillAmtTotal = BillAmtTotal + Tax_amt;

                            //Updation in Item_table:

                            // get Old Sales Quantity:

                            string getSalQty = "select nt_salqty from Item_table where Item_no='" + ItemNO + "' ";
                            SqlCommand cmdSalQty = new SqlCommand(getSalQty, con);
                            cmdSalQty.Transaction = trans;
                            double SalQty = Convert.ToDouble(cmdSalQty.ExecuteScalar());


                            // get a purchase qty:

                            string getpurchaseQty = "select nt_purqty from Item_table where Item_no='" + ItemNO + "' ";
                            SqlCommand cmdPurchaseQty = new SqlCommand(getpurchaseQty, con);
                            cmdPurchaseQty.Transaction = trans;
                            double PurchaseQty = Convert.ToDouble(cmdPurchaseQty.ExecuteScalar());



                            //// get a a opening Quantity:
                            //con.Open();
                            //string OpeningQtyqry = "select nt_opnqty from Item_table where Item_no='" + ItemNO + "' ";
                            //SqlCommand cmdOpenClosing = new SqlCommand(OpeningQtyqry, con);
                            //double OpeningQty = Convert.ToDouble(cmdOpenClosing.ExecuteScalar());
                            //con.Close();

                            //// get a Old entered Quantity:
                            //con.Open();
                            //string OldQtyqry = "select nt_qty from stktrn_table where strn_sno='" + oldrecordno + "'";
                            //SqlCommand cmdOldQty = new SqlCommand(OldQtyqry, con);
                            //double oldQty = Convert.ToDouble(cmdOldQty.ExecuteScalar());
                            //con.Close();
                            //// new Sales Qty: salqty - oldvalue + new vaue:
                            //double newQty = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[3].Value);
                           // double NewsaleQty = 0;

                            //ClosingQty = OpeningQty + PurchaseQty - NewsaleQty;

                            // get Old Sales Value:

                            string getNetsalvalqry = "select Nt_Salval from Item_table where Item_no='" + ItemNO + "' ";
                            SqlCommand cmdSalval = new SqlCommand(getNetsalvalqry, con);
                            cmdSalval.Transaction = trans;
                            DataTable dtNew123 = new DataTable();
                            dtNew123.Rows.Clear();
                            SqlDataAdapter adp123 = new SqlDataAdapter(cmdSalval);
                            adp123.Fill(dtNew123);
                            double oldNetSalVal = 0;
                            if (dtNew123.Rows.Count > 0)
                            {
                                if (dtNew123.Rows[0]["Nt_Salval"].ToString() != null && dtNew123.Rows[0]["Nt_Salval"].ToString() != "")
                                {
                                    oldNetSalVal = double.Parse(dtNew123.Rows[0]["Nt_Salval"].ToString());
                                }
                            }
                            //double oldNetSalVal = Convert.ToDouble(cmdSalval.ExecuteScalar());



                            //// get Net sal Val:
                            //con.Open();
                            //string getoldtotamt = "select tot_amt from stktrn_table where  strn_sno='" + oldrecordno + "'";
                            //SqlCommand cmdoldamt = new SqlCommand(getoldtotamt, con);
                            //double OldTotamt = Convert.ToDouble(cmdoldamt.ExecuteScalar());
                            //con.Close();
                            //double newSalval = Convert.ToDouble(lbl_Billamt.Text);
                            //NetSalVal = oldNetSalVal - OldTotamt + newSalval;


                            //Profit amount:

                            string ItemCostQry = "select Item_cost from Item_table where Item_name=@tItemName";
                            SqlCommand CmdItemCost = new SqlCommand(ItemCostQry, con);
                            CmdItemCost.Transaction = trans;
                            CmdItemCost.Parameters.AddWithValue("@tItemName", grd_SalesRecord.Rows[i].Cells[2].Value);
                            double ItemCost = Convert.ToDouble(CmdItemCost.ExecuteScalar());

                            double salesrate = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[4].Value);
                            double Quantity = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[3].Value);
                            Profit += (salesrate - ItemCost) * Quantity;

                            double Profit1 = 0;
                            Profit1 = (salesrate - ItemCost) * Quantity;


                            SqlCommand cmd = new SqlCommand(@"INSERT INTO stktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate)
                                                                       VALUES(@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8,@c9,@c10,@c11,@c12,@c13,@c14,@c15,@c16,@c17,@c18,@c19,@c20,@c21,@c22,@c23,@c25,@c26,@c27,@c28,@c29,@c30,@c31,@c32,@c33,@c34,@c35,@c36,@c37,@c38,@c39,@c40,@c41,@c42,@c43,@c44,@c45,@c46,@c47,@c48,@c49,@c50,@c51,@c52,@c53,@c54,@c55,@c56,@c57,@c58,@c59,@c60,@c61,@c62,@c63,@c64,@c65,@c66,@c67,@c68,@c69,@c70,@c71,@c72,@c73,@c74,@c75,@c76,@c77,@c78,@c79,@c80,@c81,@c82,@c83,@c84,@c85,@c86,@c87,@c88)", con);
                            {
                                cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C2", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C3", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C4", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C5", SqlDbType.Date));
                                cmd.Parameters.Add(new SqlParameter("@C6", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C7", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C8", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C9", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C10", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C11", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C12", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C13", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C14", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C15", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C16", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C17", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C18", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C19", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C20", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C21", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C22", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C23", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C24", SqlDbType.NVarChar));
                                cmd.Parameters.Add(new SqlParameter("@C25", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C26", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C27", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C28", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C29", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C30", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C31", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C32", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C33", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C34", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C35", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C36", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C37", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C38", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C39", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C40", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C41", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C42", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C43", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C44", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C45", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C46", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C47", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C48", SqlDbType.Float));

                                cmd.Parameters.Add(new SqlParameter("@C49", SqlDbType.Bit));

                                cmd.Parameters.Add(new SqlParameter("@C50", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C51", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C52", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C53", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C54", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C55", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C56", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C57", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C58", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C59", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C60", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C61", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C62", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C63", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C64", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C65", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C66", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C67", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C68", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C69", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C70", SqlDbType.Float));

                                cmd.Parameters.Add(new SqlParameter("@C71", SqlDbType.VarChar));
                                cmd.Parameters.Add(new SqlParameter("@C72", SqlDbType.Bit));
                                cmd.Parameters.Add(new SqlParameter("@C73", SqlDbType.Bit));
                                cmd.Parameters.Add(new SqlParameter("@C74", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C75", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C76", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C77", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C78", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C79", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C80", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C81", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C82", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C83", SqlDbType.Float));

                                cmd.Parameters.Add(new SqlParameter("@C84", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C85", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C86", SqlDbType.Float));
                                cmd.Parameters.Add(new SqlParameter("@C87", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@C88", SqlDbType.Float));

                            }


                            cmd.Parameters["@C1"].Value = Newid;
                            cmd.Parameters["@C2"].Value = deletedRecNo;
                            cmd.Parameters["@C3"].Value = "0";
                            cmd.Parameters["@C4"].Value = "1";          // give a type
                            cmd.Parameters["@C5"].Value = txt_date.Value;
                            cmd.Parameters["@C6"].Value = "0";          // godown billno
                            cmd.Parameters["@C7"].Value = PartyNo;      // ledger group
                            cmd.Parameters["@C8"].Value = "0";          // Grn_no
                            cmd.Parameters["@C9"].Value = "0";          // Order_sno
                            cmd.Parameters["@C10"].Value = "0";         // Dc_no
                            cmd.Parameters["@C11"].Value = ItemNO;      // Item_code
                            cmd.Parameters["@C12"].Value = _Class.clsVariables.tCounter;   // Counter No
                            cmd.Parameters["@C13"].Value = "2";         // Godown no   
                            cmd.Parameters["@C14"].Value = ItemUnit;      // Unit No
                            cmd.Parameters["@C15"].Value = "1";         // Unit ratio
                            cmd.Parameters["@C16"].Value = "0";         // quantityPieces    
                            cmd.Parameters["@C17"].Value = grd_SalesRecord.Rows[i].Cells[3].Value; // nt_quantity
                            cmd.Parameters["@C18"].Value = "0";         //tx_qty
                            cmd.Parameters["@C19"].Value = "0";         //short_qty
                            cmd.Parameters["@C20"].Value = "0";         //rnt_qty
                            cmd.Parameters["@C21"].Value = "0";         //rtx_qty
                            cmd.Parameters["@C22"].Value = "0";         //invnt_qty
                            cmd.Parameters["@C23"].Value = "0";         //invtx_qty
                            cmd.Parameters["@C24"].Value = "";         //qty Datails
                            cmd.Parameters["@C25"].Value = grd_SalesRecord.Rows[i].Cells[4].Value;  //rate
                            cmd.Parameters["@C26"].Value = "0";           //taxrate
                            cmd.Parameters["@C27"].Value = "0";             //currencyno
                            cmd.Parameters["@C28"].Value = "0";             //currency val
                            cmd.Parameters["@C29"].Value = grd_SalesRecord.Rows[i].Cells[5].Value;  //Amount
                            cmd.Parameters["@C30"].Value = TaxNo;                  // Taxno;
                            cmd.Parameters["@C31"].Value = "0";             // Disc Per qty;
                            cmd.Parameters["@C32"].Value = "0";             //Disc Per
                            cmd.Parameters["@C33"].Value = "0";             //Disc amt
                            cmd.Parameters["@C34"].Value = "0";             //AdlDisc_per
                            cmd.Parameters["@C35"].Value = "0";             //AdlDisc Amt
                            cmd.Parameters["@C36"].Value = "0";             //Other Disc Amt
                            cmd.Parameters["@C37"].Value = "0";             //otherPur Disc
                            cmd.Parameters["@C38"].Value = "0";             // Ed Per Qty
                            cmd.Parameters["@C39"].Value = "0";             //Ed_per
                            cmd.Parameters["@C40"].Value = "0";             //Ed_amt
                            cmd.Parameters["@C41"].Value = "0";             //CessPer
                            cmd.Parameters["@C42"].Value = "0";             //Cess amt
                            cmd.Parameters["@C43"].Value = "0";             //SHEcess_Per    
                            cmd.Parameters["@C44"].Value = "0";             //SHEcess_amt    
                            cmd.Parameters["@C45"].Value = "0";             //HL_per
                            cmd.Parameters["@C46"].Value = "0";             //HL_amt    
                            cmd.Parameters["@C47"].Value = "0";             //CST_per
                            cmd.Parameters["@C48"].Value = "0";             //CST_amt    
                            cmd.Parameters["@C49"].Value = false;           //tax_flag
                            cmd.Parameters["@C50"].Value = Taxvalue;                //tax_per
                            cmd.Parameters["@C51"].Value = Tax_amt;             //Taxamt;
                            cmd.Parameters["@C52"].Value = "0";                 //Sur_per
                            cmd.Parameters["@C53"].Value = "0";                 //Sur_amt
                            cmd.Parameters["@C54"].Value = "0";                 //CommiPer
                            cmd.Parameters["@C55"].Value = "0";                 //Commi
                            cmd.Parameters["@C56"].Value = "0";                 //Sman_per
                            cmd.Parameters["@C57"].Value = "0";                 //Sman_amt
                            cmd.Parameters["@C58"].Value = "0";                 //SpeclDiscAmt
                            cmd.Parameters["@C59"].Value = amt + Tax_amt;       //Tot_amt
                            cmd.Parameters["@C60"].Value = "0";                 //alp1    
                            cmd.Parameters["@C61"].Value = "0";                 //alp2
                            cmd.Parameters["@C62"].Value = "0";                 //alp3
                            cmd.Parameters["@C63"].Value = "0";                 //alp4
                            cmd.Parameters["@C64"].Value = "0";                 //ala1
                            cmd.Parameters["@C65"].Value = "0";                 //ala2
                            cmd.Parameters["@C66"].Value = "0";                 //ala3
                            cmd.Parameters["@C67"].Value = "0";                 //ala4
                            cmd.Parameters["@C68"].Value = amt + Tax_amt;       //nET_AMT
                            cmd.Parameters["@C69"].Value = "0";                 //OtherExp
                            cmd.Parameters["@C70"].Value = "0";                 //BillotherExp
                            cmd.Parameters["@C71"].Value = "";                  //strn_remark
                            cmd.Parameters["@C72"].Value = false;                 //strn_cancel
                            cmd.Parameters["@C73"].Value = false;                 //Order_ack
                            cmd.Parameters["@C74"].Value = "0";                 //cost    
                            cmd.Parameters["@C75"].Value = "0";                 //mrsp
                            cmd.Parameters["@C76"].Value = "0";                 //Margin
                            cmd.Parameters["@C77"].Value = "0";                 //Margin_no
                            cmd.Parameters["@C78"].Value = "0";                 //Srate    
                            cmd.Parameters["@C79"].Value = "0";                 //frtx_qty
                            cmd.Parameters["@C80"].Value = "0";                 //rfrnt_qty
                            cmd.Parameters["@C81"].Value = "0";                 //rfrtx_qty
                            cmd.Parameters["@C82"].Value = "0";                 //frnt_qty
                            cmd.Parameters["@C83"].Value = "0";                 //freeqty    
                            cmd.Parameters["@C84"].Value = "0";                 //FreeItemno
                            cmd.Parameters["@C85"].Value = Profit1;             //profit
                            cmd.Parameters["@C86"].Value = "0";                 //itempoint
                            cmd.Parameters["@C87"].Value = "0";                 //Mech no
                            cmd.Parameters["@C88"].Value = "0";                 //Purrate.
                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();

                        }



                        //con.Close();
                        ////Profit amount:
                        //con.Open();
                        //string ItemCostQry = "select Item_cost from Item_table where Item_name='" + grd_SalesRecord.Rows[i].Cells[2].Value + "'";
                        //SqlCommand CmdItemCost = new SqlCommand(ItemCostQry, con);
                        //double ItemCost = Convert.ToDouble(CmdItemCost.ExecuteScalar());
                        //con.Close();
                        //double salesrate = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[4].Value);
                        //double Quantity = Convert.ToDouble(grd_SalesRecord.Rows[i].Cells[5].Value);
                        //Profit += (salesrate - ItemCost) * Quantity;

                        SqlCommand cmdUpdateNum = new SqlCommand("Update numbertable set strnsno=strnsno+1", con);
                        cmdUpdateNum.Transaction = trans;
                        cmdUpdateNum.ExecuteNonQuery();
                    }
                }
                

                bool CashMode;
                //Get a Datecashmode:

                if (txt_type.Text == "Cash")
                {
                    CashMode = true;
                }
                else
                {
                    CashMode = false;
                }

                //for updates in salmas_table
              
                string currentDate = DateTime.Now.ToShortDateString();
                DateTime stdate = Convert.ToDateTime(currentDate);
                string FromsearchDate = stdate.ToString("yyyy-MM-dd");

                string getcurrentTime = DateTime.Now.ToShortTimeString();
                DateTime curTime = Convert.ToDateTime(getcurrentTime);
                string CurrentTime = curTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string SalmasUpdateQry = "Update salmas_table set smas_billdate='" + FromsearchDate + "',smas_billtime='" + CurrentTime + "', dc_no='" + txt_dcno.Text + "',ctr_no='" + _Class.clsVariables.tCounter + "',party_no='" + PartyNo + "',smas_name='" + txt_to.Text + "',smas_cashmode='" + CashMode + "',smas_Gross='" + lbl_Gross_Amt.Text + "',smas_GrossAmount='" + lbl_Gross_Amt.Text + "',smas_NetAmount='" + lbl_Billamt.Text + "',Profit='" + Profit + "' where smas_Billno='" + txt_Billno.Text + "'";
                SqlCommand cmdSalmasUpdate = new SqlCommand(SalmasUpdateQry, con);
                cmdSalmasUpdate.Transaction = trans;
                cmdSalmasUpdate.ExecuteNonQuery();               

                DataTable dtOldVch = new DataTable();
                dtOldVch.Rows.Clear();
                SqlCommand cmdOldVchNo = new SqlCommand("select Vch_Sno from  Vch_table where Vch_No=@tSalesCount and Vch_Party=(select vch_party from Vch_table where Vch_No=@tSalesCount group by vch_party) group by Vch_Sno", con);
                cmdOldVchNo.Parameters.AddWithValue("@tSalesCount", txt_Billno.Text.Trim());
                cmdOldVchNo.Transaction = trans;
                SqlDataAdapter adpOldVchNo = new SqlDataAdapter(cmdOldVchNo);
                adpOldVchNo.Fill(dtOldVch);
                string oldVchNo = "";
                if (dtOldVch.Rows.Count > 0)
                {
                    oldVchNo = dtOldVch.Rows[0][0].ToString();
                }

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmdVch = new SqlCommand("select Tax_Per As Tax,Tax_No from stktrn_table where strn_no=@tStrnNo group by tax_per,Tax_No", con);
                cmdVch.Parameters.AddWithValue("@tStrnNo", deletedRecNo);
                cmdVch.Transaction = trans;
                SqlDataAdapter adpNew = new SqlDataAdapter(cmdVch);
                adpNew.Fill(dtNew);
                SqlCommand spVchDelete = new SqlCommand("sp_vchDelete", con);
               
                spVchDelete.CommandType = CommandType.StoredProcedure;
                spVchDelete.Parameters.AddWithValue("@tSalesCount", txt_Billno.Text.Trim());
                spVchDelete.Parameters.AddWithValue("@tGrossAmt", double.Parse(lbl_Gross_Amt.Text.ToString()));
                spVchDelete.Parameters.AddWithValue("@tNetAmt", double.Parse(lbl_Billamt.Text.ToString()));
                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                spVchDelete.Parameters.AddWithValue("@tTotTax", (double.Parse(lbl_Billamt.Text.ToString()) - double.Parse(lbl_Gross_Amt.Text.ToString())));

                if (double.Parse(lblDiscount.Text.ToString()) > 0)
                {
                    spVchDelete.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Text.ToString()));
                }
                else
                {
                    spVchDelete.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Text.ToString()));
                }
                spVchDelete.Parameters.AddWithValue("@tCurrentDate", txt_date.Value);
                spVchDelete.Parameters.AddWithValue("@CashType", txt_type.Text.Trim());
                 spVchDelete.Transaction = trans;
                spVchDelete.ExecuteNonQuery();
                for (int ij = 0; ij < dtNew.Rows.Count; ij++)
                {
                    SqlCommand spVch = new SqlCommand("sp_vchCreation", con);

                    spVch.CommandType = CommandType.StoredProcedure;
                    spVch.Transaction = trans;
                    spVch.Parameters.AddWithValue("@tTax", (dtNew.Rows[ij]["Tax"].ToString() == "") ? 0 : double.Parse(dtNew.Rows[ij]["Tax"].ToString()));
                    spVch.Parameters.AddWithValue("@tSalesCount", txt_Billno.Text.Trim());
                    spVch.Parameters.AddWithValue("@tCurrentDate", txt_date.Value);
                    spVch.Parameters.AddWithValue("@tTaxNumber", dtNew.Rows[ij]["Tax_no"].ToString());
                    spVch.Parameters.AddWithValue("@CashType", txt_type.Text.Trim());
                    spVch.Parameters.AddWithValue("@OldVchNumber", oldVchNo);
                    spVch.ExecuteNonQuery();

                }
                trans.Commit();
                con.Close();
               
                // for closing the form
                frmSalesSummaryDetails frm = new frmSalesSummaryDetails();
                frm.BringToFront();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();                
                this.Close();
                
            }
            catch (Exception ex)
            {
                //MyMessageBox.ShowBox(ex.Message, "Warning");
                MessageBox.Show(ex.Message, "Warning");
            }            
        }

        private void frmSalesAlteration_Load(object sender, EventArgs e)
        {
            dt1.Columns.Add("S.no");
            dt1.Columns.Add("Item_code");
            dt1.Columns.Add("Item_name");
            dt1.Columns.Add("nt_qty");
            dt1.Columns.Add("Rate");
            dt1.Columns.Add("Amount");
            dt1.Columns.Add("Id");
           // dt1.Columns.Add();
            txt_counter.Text = chkbox.tCounterName;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

        }

        private void grd_SalesRecord_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
                if (grd_SalesRecord.CurrentCell.ColumnIndex == 0) //Item_code
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                    }
                }
                if (this.grd_SalesRecord.CurrentCell.ColumnIndex == this.grd_SalesRecord.Columns[2].Index) //Item_name
                {

                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                if (grd_SalesRecord.CurrentCell.ColumnIndex == 3) //Less Qty
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                    }

                }
                if (grd_SalesRecord.CurrentCell.ColumnIndex == 4) //Add Qty
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                    }
                }
                if (grd_SalesRecord.CurrentCell.ColumnIndex == 5) //Less Qty
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                    }
                }
                if (this.grd_SalesRecord.CurrentCell.ColumnIndex == this.grd_SalesRecord.Columns[1].Index) //Item_code
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    te.AutoCompleteSource = AutoCompleteSource.None;

                }
                if (this.grd_SalesRecord.CurrentCell.ColumnIndex == this.grd_SalesRecord.Columns[3].Index) //less Qty
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    te.AutoCompleteSource = AutoCompleteSource.None;

                }
                if (this.grd_SalesRecord.CurrentCell.ColumnIndex == this.grd_SalesRecord.Columns[4].Index) //Add Qty
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    te.AutoCompleteSource = AutoCompleteSource.None;

                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
       // string itementered;
        private void grd_SalesRecord_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int iRow = grd_SalesRecord.CurrentCell.RowIndex;
                   // double mn = 0.00;

                    //if (grd_SalesRecord.CurrentCell.ColumnIndex == 1)
                    //{
                    //    if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[1].Value.ToString() =="")
                    //    {
                    //        grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[2];

                    //    }
                    //}


                    if (grd_SalesRecord.CurrentCell.ColumnIndex == 5)
                    {

                        DataRow newRow = dtgrdload.NewRow();
                        // Add the row to the rows collection.
                        dtgrdload.Rows.Add(newRow);
                        dtgrdload.AcceptChanges();
                        grd_SalesRecord.AllowUserToAddRows = true;
                        // grd_SalesRecord.Rows.Add();  
                        int a = grd_SalesRecord.Rows.Count - 1;
                        lbl_ItemCount.Text = a.ToString();
                        funTotalCalculation();
                        //grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[iRow+1].Cells[1];
                        //grd_SalesRecord.AllowUserToAddRows = false;

                    }
                    //if (grd_SalesRecord.CurrentCell.ColumnIndex == 4)
                    //{
                    //    grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[5];
                    //}
                    //if (grd_SalesRecord.CurrentCell.ColumnIndex == 3)
                    //{
                    //    grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[4];
                    //}
                    //if (grd_SalesRecord.CurrentCell.ColumnIndex == 2)
                    //{
                    //    itementered = grd_SalesRecord.Rows[iRow].Cells[2].Value.ToString();
                    //    fecthitemnamevalues();

                    //}
                    if (grd_SalesRecord.CurrentCell.ColumnIndex == 2)
                    {
                        if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[1].Value.ToString() == "" && grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[2].Value.ToString() == "")
                        {
                            grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[2].Selected = false;
                            txt_ReceivedAmt.Focus();

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void funTotalCalculation()
        {
            try
            {
                double TotalQty = 0;
                for (int j = 0; j < grd_SalesRecord.Rows.Count; j++)
                {
                    double nt_qty = 0;
                    if (grd_SalesRecord.Rows[j].Cells["nt_qty"].Value == null)
                    {
                        nt_qty = 0;
                    }
                    else
                    {
                        if (grd_SalesRecord.Rows[j].Cells["nt_qty"].Value.ToString().Trim() == "")
                        {
                            nt_qty = 0;
                        }
                        else
                        {
                            nt_qty = Convert.ToDouble(grd_SalesRecord.Rows[j].Cells["nt_qty"].Value.ToString());
                        }

                    }
                    TotalQty = TotalQty + nt_qty;
                    lbl_Qty_count.Text = TotalQty.ToString();
                }
                double GrossAmtTotal = 0;
                for (int j = 0; j < grd_SalesRecord.Rows.Count; j++)
                {
                    double grdAmount = 0;
                    if (grd_SalesRecord.Rows[j].Cells["Amount"].Value == null)
                    {
                        grdAmount = 0;
                    }
                    else
                    {
                        if (grd_SalesRecord.Rows[j].Cells["Amount"].Value.ToString().Trim() == "")
                        {
                            grdAmount = 0;
                        }
                        else
                        {
                            grdAmount = Convert.ToDouble(grd_SalesRecord.Rows[j].Cells["Amount"].Value.ToString());
                        }
                    }
                    GrossAmtTotal = GrossAmtTotal + grdAmount;
                    lbl_Gross_Amt.Text = string.Format("{0:0.00}", GrossAmtTotal);
                }
                double tTaxPercent = 0, tTotAmt = 0;
                for (int j = 0; j < grd_SalesRecord.Rows.Count; j++)
                {
                    if (grd_SalesRecord.Rows[j].Cells["Amount"].Value != null && grd_SalesRecord.Rows[j].Cells["nt_qty"].Value != null && grd_SalesRecord.Rows[j].Cells["Item_name"].Value != null)
                    {
                        DataTable dtTax = new DataTable();
                        dtTax.Rows.Clear();
                        SqlCommand cmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tActionType", "TaxCal");
                        cmd.Parameters.AddWithValue("@tValue", grd_SalesRecord.Rows[j].Cells["Item_name"].Value.ToString().Trim());
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtTax);
                        if (dtTax.Rows.Count > 0)
                        {
                            tTaxPercent = double.Parse(dtTax.Rows[0][0].ToString());
                        }
                        double grdAmount = 0;
                        if (grd_SalesRecord.Rows[j].Cells["Amount"].Value.ToString().Trim() != "")
                        {
                            grdAmount = Convert.ToDouble(grd_SalesRecord.Rows[j].Cells["Amount"].Value.ToString());
                        }
                        else
                        {
                            grdAmount = 0;
                        }
                        tTotAmt += (grdAmount * (tTaxPercent / 100));
                    }
                }
                double billamt = GrossAmtTotal + tTotAmt;
                lbl_Billamt.Text = string.Format("{0:0.00}", billamt);
                lblBalanceAmt.Text = string.Format("{0:0.00}", billamt);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

      //  Double quantity, addcheck, lesscheck, rate;
        public void fecthitemnamevalues(string itemname)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "ITEMDETAILWITHNAME");
                cmd.Parameters.AddWithValue("@tValue", itemname);
                SqlDataAdapter adp5 = new SqlDataAdapter(cmd);
                dtNew5.Rows.Clear();
                adp5.Fill(dtNew5);
                int i = 0;
                if (dtNew5.Rows.Count > 0)
                {

                    //SqlCommand namecmd = new SqlCommand("select Item_code,Item_mrsp from Item_table where Item_name='" + itementered + "'", con);
                    //SqlDataReader dread;
                    //dread = namecmd.ExecuteReader();
                    //int i = 0;
                    //if (dread.Read())
                    //{
                    i = 1;
                    grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[4].Value = dtNew5.Rows[0]["Item_mrsp"].ToString();
                    grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[1].Value = dtNew5.Rows[0]["Item_code"].ToString();
                    int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.grd_SalesRecord.CurrentCell.ColumnIndex + 1);
                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                    this.grd_SalesRecord.BeginInvoke(method, 3);
                }
                if (i == 1)
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd2);
                    adp.Fill(dtNew);
                    //idr2 = cmd2.ExecuteReader();
                    if (dtNew.Rows.Count > 0)
                    {
                        int chkunit = 0;
                        for (int mn = 0; mn < dtNew.Rows.Count;)
                        {
                            chkunit = 1;
                            unit = dtNew.Rows[mn]["unit_name"].ToString();
                            //grd_SalesRecord.Rows[grd_SalesRecord.CurrentRow.Index].Cells["S_Unit"].Value = unit;
                            break;
                        }
                        if (chkunit == 1 && i == 1)
                        {
                            int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.grd_SalesRecord.CurrentCell.ColumnIndex + 1);
                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                            this.grd_SalesRecord.BeginInvoke(method, 3);
                        }
                    }

                    //con.Open();
                    //SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table", con);
                    //SqlDataReader idr2;
                    //idr2 = cmd2.ExecuteReader();
                    //if (idr2.HasRows)
                    //{
                    //    int chkunit = 0;
                    //    while (idr2.Read())
                    //    {
                    //        chkunit = 1;
                    //        unit = idr2["unit_name"].ToString();
                    //       // grd_SalesRecord.Rows[grd_SalesRecord.CurrentRow.Index].Cells[3].Value = unit;
                    //        break;
                    //    }
                    //    con.Close();
                    //    idr2.Dispose();

                    //    if (chkunit == 1 && i == 1)
                    //    {
                    //        grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[4];

                    //    }
                    //}
                }
                else
                {
                    MessageBox.Show("Invalid Item Name");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        string unit = string.Empty;
        int temrowcur;
        DataTable dtNew5 = new DataTable();
        public void getbyid(string id)
        {
            //id = grd_stock.Rows[temrowcur].Cells["Item_code"].Value.ToString();
            // pnl_item_name.Visible = true;
            //lst_itemname.Visible = true;

            try
            {


                SqlCommand cmd = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "ITEMDETAILWITHCODE");
                cmd.Parameters.AddWithValue("@tValue", id);
                SqlDataAdapter adp5 = new SqlDataAdapter(cmd);
                dtNew5.Rows.Clear();
                adp5.Fill(dtNew5);
                int i = 0;
                for (int mn = 0; mn < dtNew5.Rows.Count; )
                {

                    //SqlCommand cmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table where Item_code='" + id + "'", con);
                    //SqlDataReader dr3 = null;
                    //con.Close();
                    //con.Open();
                    //dr3 = cmd.ExecuteReader();
                    //int i = 0;
                    //while (dr3.Read())
                    //{
                    i = 1;
                    string name = dtNew5.Rows[mn]["Item_name"].ToString();

                    grd_SalesRecord.Rows[grd_SalesRecord.CurrentRow.Index].Cells[2].Value = name;
                    grd_SalesRecord.Rows[grd_SalesRecord.CurrentRow.Index].Cells[4].Value = dtNew5.Rows[mn]["Item_mrsp"].ToString();

                    break;
                }

                if (i == 0)
                {
                    // MessageBox.Show("Item code not found in the list");
                    //grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[grd_SalesRecord.CurrentCell.ColumnIndex + 1];
                }
                else
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table", con);
                  //  SqlDataReader idr2;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd2);
                    adp.Fill(dtNew);
                    //idr2 = cmd2.ExecuteReader();
                    if (dtNew.Rows.Count > 0)
                    {
                        int chkunit = 0;
                        for (int mn = 0; mn < dtNew.Rows.Count; )
                        {
                            chkunit = 1;
                            unit = dtNew.Rows[mn]["unit_name"].ToString();
                            //grd_SalesRecord.Rows[grd_SalesRecord.CurrentRow.Index].Cells["S_Unit"].Value = unit;
                            break;
                        }
                        if (chkunit == 1 && i == 1)
                        {
                            int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.grd_SalesRecord.CurrentCell.ColumnIndex + 1);
                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                            this.grd_SalesRecord.BeginInvoke(method, 3);
                            //grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells[3];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
      //  int currentrow;
       // double totalAmt;
        private void grd_SalesRecord_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // currentrow = grd_SalesRecord.CurrentCell.RowIndex;
            // if (grd_SalesRecord.CurrentCell.ColumnIndex == 3)
            // {
                 
            //     quantity = Convert.ToDouble(grd_SalesRecord.Rows[currentrow].Cells[3].Value);
            //     rate = Convert.ToDouble(grd_SalesRecord.Rows[currentrow].Cells[4].Value);

            //     double price = quantity * rate;
            //     grd_SalesRecord.Rows[currentrow].Cells[5].Value = price;
            //     totalAmt += price;
            //     lbl_Gross_Amt.Text = totalAmt.ToString();
            //     grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[currentrow].Cells[5];
            // }           

            //if (grd_SalesRecord.CurrentCell.ColumnIndex == 1)
            //{
            //    grd_SalesRecord.CurrentCell = grd_SalesRecord.Rows[currentrow].Cells[2];
            //    string itemid1 = grd_SalesRecord.Rows[currentrow].Cells[1].Value.ToString();
            //    getbyid(itemid1);
            //}
            
        }

        private void txt_ReceivedAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void btn_Save_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btn_Save_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_to_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_customer.Visible = true;
            pnl_sales.Visible = false;
            pnl_type.Visible = false;
        }

        private void txt_counter_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_customer.Visible = true;
            pnl_sales.Visible = false;
            pnl_type.Visible = false;
        }

        private void txt_sales_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_customer.Visible = false;
            pnl_sales.Visible = true;
            pnl_type.Visible = false;
        }

        private void txt_type_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_customer.Visible = false;
            pnl_sales.Visible = false;
            pnl_type.Visible = true;
        }

        private void lst_ledger_Click(object sender, EventArgs e)
        {
            
        }

        private void txt_date_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ReceivedAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double tReceiverAmt = 0, tBillAmt = 0;
                if (txt_ReceivedAmt.Text.Trim() == "")
                {
                    tReceiverAmt = 0;
                }
                else
                {
                    tReceiverAmt = double.Parse(txt_ReceivedAmt.Text.Trim());
                }
                if (lbl_Billamt.Text.Trim() == "")
                {
                    tBillAmt = 0;
                }
                else
                {
                    tBillAmt = double.Parse(lbl_Billamt.Text.Trim());
                }
                lblBalanceAmt.Text = string.Format("{0:0.00}", tBillAmt - tReceiverAmt);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public delegate void SetColumnIndex(int i);
        public void Mymethod(int columnIndex)
        {
            #region
            //int k=Convert.ToInt32(dgsales.CurrentCell.ColumnIndex);
            //this.dgsales.CurrentCell = this.dgsales.CurrentRow.Cells[k];
            //int o = Convert.ToInt32(dgsales.TabIndex.ToString());
            //this.dgsales.BeginEdit(dgsales.TabIndex.Equals(o-2));
            //dgsales.BeginEdit(true);
            this.grd_SalesRecord.CurrentCell = this.grd_SalesRecord.CurrentRow.Cells[columnIndex];
            this.grd_SalesRecord.BeginEdit(true);
            // System.Windows.Forms.Control cntObject1;
            #endregion
        }

        public void nextcell()
         {
             try
             {
                 if (this.grd_SalesRecord.CurrentCell.ColumnIndex != this.grd_SalesRecord.Columns.Count - 1)
                 {
                     int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.grd_SalesRecord.CurrentCell.ColumnIndex + 1);
                     SetColumnIndex method = new SetColumnIndex(Mymethod);
                     this.grd_SalesRecord.BeginInvoke(method, nextindex);
                 }
             }
             catch (Exception ex)
             {
                 MyMessageBox.ShowBox(ex.Message, "Warning");
             }
         }

        double amount = 0;
        private void grd_SalesRecord_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (grd_SalesRecord.CurrentRow != null && e.ColumnIndex == 1)
                    {
                        string itemcode = "";
                        if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Item_code"].Value != null)
                        {
                            if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Item_code"].Value.ToString().Trim() != "")
                            {

                                itemcode = grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Item_code"].Value.ToString();
                                getbyid(itemcode);
                                if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Item_code"].Value != null)
                                {
                                    if (dtNew5.Rows.Count > 0)
                                    {
                                        nextcell();
                                    }
                                    else
                                    {
                                        MyMessageBox1.ShowBox("Code Not Found", "Warning");
                                        int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.grd_SalesRecord.CurrentCell.ColumnIndex + 1);
                                        SetColumnIndex method = new SetColumnIndex(Mymethod);
                                        this.grd_SalesRecord.BeginInvoke(method, nextindex - 1);
                                    }
                                }
                                else
                                {
                                    //MyMessageBox1.ShowBox("Please Enter Correct ItemCode", "Warning");
                                    //previouscell();  
                                    // grd_SalesRecord.Focus();
                                }
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (grd_SalesRecord.CurrentRow != null && e.ColumnIndex == 2)
                    {
                        string itemname = "";
                        if (grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value != null)
                        {
                            if (grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value.ToString() != "")
                            {
                                string t1 = grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value.ToString();
                                int t2 = e.RowIndex;
                                for (int j = 0; j < grd_SalesRecord.Rows.Count - 1; j++)
                                {
                                    if (t2 != j)
                                    {

                                        if (t1 == grd_SalesRecord.Rows[j].Cells["Item_name"].Value.ToString())
                                        {

                                            MessageBox.Show("Selected item is already entered");
                                            break;
                                        }

                                    }
                                }
                            }

                            itemname = grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value.ToString();
                            fecthitemnamevalues(itemname);
                            if (itemname != null)
                            {
                                if (dtNew5.Rows.Count > 0)
                                {
                                    if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Item_code"].Value != null && grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Item_name"].Value != null)
                                    {
                                        int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.grd_SalesRecord.CurrentCell.ColumnIndex + 1);
                                        SetColumnIndex method = new SetColumnIndex(Mymethod);
                                        this.grd_SalesRecord.BeginInvoke(method, nextindex);
                                    }
                                }
                                else
                                {
                                    MyMessageBox1.ShowBox("Please Enter Correct Name or Code", "Warning");
                                    //int nextindex = Math.Min(this.grd_SalesRecord.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
                                    //SetColumnIndex method = new SetColumnIndex(Mymethod);
                                    //this.myDataGrid1.BeginInvoke(method, nextindex - 1);
                                }
                            }
                        }

                        else
                        {

                        }
                    }
                }

                else if (grd_SalesRecord.CurrentRow != null && e.ColumnIndex == 3)
                {
                    if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value != null && grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value.ToString() != "" && grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value.ToString() != "0")
                    {

                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value)).ToString("0.00");

                        if (grd_SalesRecord.Rows.Count > 0 && grd_SalesRecord.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < grd_SalesRecord.Rows.Count - 1; i++)
                            {
                                if (lbl_Gross_Amt.Text == "")
                                {
                                    if (grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString().Trim() != "")
                                    {
                                        lbl_Gross_Amt.Text = string.Format("{0:0.00}", grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                else
                                {
                                    if (grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    {
                                        amount += double.Parse(grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                lbl_Gross_Amt.Text = string.Format("{0:0.00}", amount);
                            }
                            amount = 0;
                        }
                        funTotalCalculation();
                    }
                    else if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value.ToString() == "" || grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value.ToString() == "0")
                    {                      
                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value)).ToString("0.00");

                        if (grd_SalesRecord.Rows.Count > 0 && grd_SalesRecord.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < grd_SalesRecord.Rows.Count - 1; i++)
                            {
                                if (lbl_Gross_Amt.Text == "")
                                {
                                    if (grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString().Trim() != "")
                                    {
                                        lbl_Gross_Amt.Text = string.Format("{0:0.00}", grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                else
                                {
                                    if (grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    {
                                        amount += double.Parse(grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                lbl_Gross_Amt.Text = string.Format("{0:0.00}", amount);
                            }
                            amount = 0;
                        }
                        funTotalCalculation();
                    }                    

                    double ini_0 = 1, ini2 = 1;
                    if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value == null || grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value.ToString() == "")
                    {
                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
                        ini_0 = 0;

                    }
                    //if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                    //{
                    //    grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Less_Qty"].Value = "0";
                    //    ini2 = 0;
                    //}
                    if (ini_0 != 1 || ini2 != 1)
                    {
                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";

                    }
                }

                else if (grd_SalesRecord.CurrentRow != null && e.ColumnIndex == 4)
                {

                    if (grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value.ToString() != "0" && grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value != null && grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value.ToString() != "")
                    {

                        //grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(grd_SalesRecord.Rows[grd_SalesRecord.CurrentCell.RowIndex].Cells["nt_qty"].Value)).ToString("0");

                        if (grd_SalesRecord.Rows.Count > 0 && grd_SalesRecord.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < grd_SalesRecord.Rows.Count - 1; i++)
                            {

                                if (lbl_Gross_Amt.Text == "")
                                {
                                    if (grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString().Trim() != "")
                                    {
                                        lbl_Gross_Amt.Text = string.Format("{0:0.00}", double.Parse(grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString()));
                                    }
                                }
                                else
                                {
                                    if (grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    {
                                        amount += double.Parse(grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                lbl_Gross_Amt.Text = string.Format("{0:0.00}", amount);
                            }
                            amount = 0;
                        }
                        funTotalCalculation();

                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }
                
        private void grd_SalesRecord_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grd_SalesRecord.CurrentCell.ColumnIndex == 2)
                {
                    if (grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value != null)
                    {
                        if (grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value.ToString() != "")
                        {
                            if (grd_SalesRecord.Rows.Count > 0)
                            {
                                if (grd_SalesRecord.Rows[e.RowIndex].Cells["nt_qty"].Value != null)
                                {
                                    if (grd_SalesRecord.Rows[e.RowIndex].Cells["nt_qty"].Value.ToString().Trim() != "")
                                    {
                                        getQty = Convert.ToInt32(grd_SalesRecord.Rows[e.RowIndex].Cells["nt_qty"].Value.ToString());
                                    }
                                }
                                altName = grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value.ToString();
                                SqlCommand cmd = new SqlCommand("Select nt_salqty from Item_table where Item_name=@altName", con);
                                cmd.Parameters.AddWithValue("@altName", altName);
                                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    tempQty = Convert.ToInt32(dt.Rows[0]["nt_salqty"].ToString());
                                }
                            }
                        }
                    }
                }
                if (grd_SalesRecord.CurrentCell.ColumnIndex == 3)
                {
                   
                }                              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        DataTable dt1 = new DataTable();
        private void grd_SalesRecord_Enter(object sender, EventArgs e)
        {

            try
            {
                //if (dt1.Columns.Count == 0)
                //{
                //    foreach (DataGridViewColumn col in grd_SalesRecord.Columns)
                //    {

                //        dt1.Columns.Add(col.HeaderText);

                //    }
                //}

                for (int i = 0; i < grd_SalesRecord.Rows.Count - 1; i++)
                {
                    if (grd_SalesRecord.Rows[i].Cells["Item_name"].Value.ToString() != "" && grd_SalesRecord.Rows[i].Cells["Item_name"].Value != null)
                    {
                        dt1.Rows.Add(grd_SalesRecord.Rows[i].Cells["S.no"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Item_code"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Item_name"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["nt_qty"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Rate"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Id"].Value.ToString().Trim());
                    }
                }
                //for (int i = dt1.Rows.Count - 1; i >= 0; i += -1)
                //{
                //    DataRow row = dt1.Rows[i];
                //    if (row[0] == null)
                //    {
                //        dt1.Rows.Remove(row);
                //    }
                //    else if (string.IsNullOrEmpty(row[0].ToString()))
                //    {
                //        dt1.Rows.Remove(row);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
          
    }
}
