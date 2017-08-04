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

namespace SalesProject
{
    public partial class PurchaseEntry1 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt_pass_values = new System.Data.DataTable();
        DataTable dtDicounttable = new DataTable();
        DataTable Datta = new DataTable();
        string items_alter = "0";
        string Tessupliers = "";
        string dggridvalues = "";
        public PurchaseEntry1(string id_number)
        {
            InitializeComponent();

            try
            {
                DgPurchase.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["DiscAmt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Qt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["TaxRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Disc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Mrp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Special1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Special2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["Special3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["TotalAmt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //DgPurchase.Columns["TaxName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["TaxPer"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgPurchase.Columns["TaxAmt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //Discount AmountGrid:
                DgDiscount.Columns["DisPerQty_Pr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDiscount.Columns["Percent_Pr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DgDiscount.Columns["DiscountAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
                DataTable dt_unit_table1 = new DataTable();
                DataTable dt_pumas_table = new DataTable();
                DataTable dt_unit_keys = new DataTable();
                
               
                //Insert Process dataTable
                dt_gridload.Columns.Add("itemCode");
                dt_gridload.Columns.Add("ItemName");
                dt_gridload.Columns.Add("Quantity ");
                dt_gridload.Columns.Add("Rate");
                dt_gridload.Columns.Add("Amount");
                dt_gridload.Columns.Add("DicountPerSentage");
                dt_gridload.Columns.Add("DiscountAmount");
                dt_gridload.Columns.Add("MrpRate");
                dt_gridload.Columns.Add("TotalAmount");
                dt_gridload.Columns.Add("Dates", typeof(DateTime));
                dt_gridload.Columns.Add("Counters");
                dt_gridload.Columns.Add("TaxRate");
                dt_gridload.Columns.Add("TaxName");
                dt_gridload.Columns.Add("TaxPer");
                dt_gridload.Columns.Add("TaxAmt");
                dt_gridload.Columns.Add("Special1");
                dt_gridload.Columns.Add("Special2");
                dt_gridload.Columns.Add("Special3");

                id = id_number;

                //Alter Process dataTable
                dtGralter.Columns.Add("itemCode");
                dtGralter.Columns.Add("ItemName");
                dtGralter.Columns.Add("Remarks");
                dtGralter.Columns.Add("Unit");
                dtGralter.Columns.Add("Qty");
                dtGralter.Columns.Add("Rate");
                dtGralter.Columns.Add("TaxRate");
                dtGralter.Columns.Add("Amount");
                dtGralter.Columns.Add("Dic");
                dtGralter.Columns.Add("DicAmount");
                dtGralter.Columns.Add("Mrp");
                dtGralter.Columns.Add("Special1");
                dtGralter.Columns.Add("Special2");
                dtGralter.Columns.Add("Special3");
                dtGralter.Columns.Add("TotalAmount");
                dtGralter.Columns.Add("Exp");
                dtGralter.Columns.Add("StrnNo");
                dtGralter.Columns.Add("StrnSno");

                dtGralter.Columns.Add("TaxName");
                dtGralter.Columns.Add("TaxPer");
                dtGralter.Columns.Add("TaxAmt");

                //item alter table
                dt_gridload1.Columns.Add("strn_sno");
                dt_gridload1.Columns.Add("strn_no");
                dt_gridload1.Columns.Add("item_no");
                dt_gridload1.Columns.Add("nt_qty");
                dt_gridload1.Columns.Add("Amount");

                //Discount Table:
                dtDicounttable.Columns.Add("Type");
                dtDicounttable.Columns.Add("Details");
                dtDicounttable.Columns.Add("PurQty7");
                dtDicounttable.Columns.Add("Percent");
                dtDicounttable.Columns.Add("Amount");
                
                

                foreach (DataGridViewColumn col in DgPurchase.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

               
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "Selectstrn");
                cmd.Parameters.AddWithValue("@itemName", id);
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_pass_values.Rows.Clear();
                adp.Fill(dt_pass_values);
                Datta.Rows.Clear();
                Datta = dt_pass_values.Clone();
                foreach (DataRow drtableOld in dt_pass_values.Rows)
                {
                    Datta.ImportRow(drtableOld);
                }
                if (dt_pass_values.Rows.Count > 0)
                {
                    label17.Text = "Purchase Alteration";
                    items_alter = "0";
                    DataTable dtpumas = new DataTable();
                    SqlCommand cmdpumas = new SqlCommand("select * from purmas_table where Pmas_Sno='" + id + "'", con);
                    
                    SqlDataAdapter adpumas = new SqlDataAdapter(cmdpumas);
                    dtpumas.Rows.Clear();
                    adpumas.Fill(dtpumas);
                    if (dtpumas.Rows.Count > 0)
                    {
                        lblBillNo.Text = dtpumas.Rows[0]["pmas_VoucherNo"].ToString();

                        txtInvoiceNo.Text = dtpumas.Rows[0]["pmas_billno"].ToString();
                        txtOrder_No.Text = dtpumas.Rows[0]["Order_no"].ToString();
                        string name_cash = dtpumas.Rows[0]["CashLed_no"].ToString();
                        if (name_cash == "8")
                        {
                            txtCash.Text = "Cash";
                        }
                        if (name_cash == "5")
                        {
                            txtCash.Text = "Cridit";
                        }
                        txtSuppliers.Text = dtpumas.Rows[0]["Pmas_name"].ToString();
                        SqlCommand nmd = new SqlCommand(@"SELECT PurType_Table.PurType_Name FROM purmas_table INNER JOIN PurType_Table ON purmas_table.PurType = PurType_Table.PurType_No where purmas_table.pmas_Sno='" + id + "'", con);
                        
                        SqlDataAdapter adpr = new SqlDataAdapter(nmd);
                        DataTable dtpur = new DataTable();

                        dtpur.Rows.Clear();
                        adpr.Fill(dtpur);
                        if (dtpur.Rows.Count > 0)
                        {
                            txtPurchaseType.Text = dtpur.Rows[0]["PurType_Name"].ToString();
                        }
                        txtAddress1.Text = (dtpumas.Rows[0]["pmas_add1"].ToString());
                        txtAddress2.Text = (dtpumas.Rows[0]["pmas_add2"].ToString());
                        txtAddress3.Text = (dtpumas.Rows[0]["pmas_add3"].ToString());
                        txtAddress4.Text = (dtpumas.Rows[0]["pmas_add4"].ToString());
                       
                        txtInvalue.Text = dtpumas.Rows[0]["pmas_netamount"].ToString();
                        txtGst.Text = dtpumas.Rows[0]["Pmas_St"].ToString();
                        txtRegno.Text = dtpumas.Rows[0]["Pmas_Cst"].ToString();
                        txtDate.Text = Convert.ToDateTime(dtpumas.Rows[0]["Pmas_date"].ToString()).ToShortDateString();
                        txtIvDate.Text = Convert.ToDateTime(dtpumas.Rows[0]["pmas_billdate"].ToString()).ToShortDateString();
                        
                        DataTable dtledger = new DataTable();
                        string str1 = dtpumas.Rows[0]["party_no"].ToString();


                        SqlCommand nmdadpledger = new SqlCommand("SP_SelectQuery", con);
                        nmdadpledger.CommandType = CommandType.StoredProcedure;
                        nmdadpledger.Parameters.AddWithValue("@ActionType", "LedgerNumber");
                        nmdadpledger.Parameters.AddWithValue("@itemName", str1);
                        nmdadpledger.Parameters.AddWithValue("@ItemCode", "");
                       
                        SqlDataAdapter adpledger = new SqlDataAdapter(nmdadpledger);
                        dtledger.Rows.Clear();
                        adpledger.Fill(dtledger);
                        if (dtledger.Rows.Count > 0)
                        {
                            Tessupliers = "NoGo";
                            txtSuppliers.Text = dtledger.Rows[0]["Ledsel_name"].ToString();
                            Tessupliers = "";
                        }
                        
                        DataTable dtcounter = new DataTable();
                        SqlCommand cmdcounter = new SqlCommand("select * from counter_table where ctr_no='" + dtpumas.Rows[0]["Ctr_no"].ToString() + "'", con);

                        SqlDataAdapter adpcounter = new SqlDataAdapter(cmdcounter);
                        dtcounter.Rows.Clear();
                        adpcounter.Fill(dtcounter);
                        if (dtcounter.Rows.Count > 0)
                        {
                            txtCounter.Text = dtcounter.Rows[0]["ctr_name"].ToString();
                        }
                        //continue:
                        //assign to values to datagridview values:
                        for (int i = 0; i < dt_pass_values.Rows.Count; i++)
                        {
                            DgPurchase.Rows.Add();
                            int iRow = DgPurchase.CurrentCell.RowIndex;
                            //row number gridviews:

                            int m_row_index = Convert.ToInt32(DgPurchase.CurrentCell.RowIndex);
                            DgPurchase.Rows[i].Cells["Sno"].Value = kr + i;

                            //gettting unit number to name:
                            DataTable dtunit = new DataTable();
                            // SqlCommand cmdunion = new SqlCommand("select * from unit_table where unit_no='"+dt_pass_values.Rows[i]["Unit_no"]+"'", con);

                            SqlCommand cmdunion = new SqlCommand("SP_SelectQuery", con);
                            cmdunion.CommandType = CommandType.StoredProcedure;
                            cmdunion.Parameters.AddWithValue("@ActionType", "UnitNo");

                            cmdunion.Parameters.AddWithValue("@itemName", dt_pass_values.Rows[i]["Unit_no"].ToString());
                            cmdunion.Parameters.AddWithValue("@ItemCode", "");
                            //SqlDataAdapter adpcounter = new SqlDataAdapter(cmdcounter);
                            SqlDataAdapter adpunit = new SqlDataAdapter(cmdunion);
                            dtunit.Rows.Clear();
                            adpunit.Fill(dtunit);

                            if (dtunit.Rows.Count > 0)
                            {
                                unitnames = dtunit.Rows[0]["unit_name"].ToString();
                            }
                            //getting values to item_no to item_name and coding from stkrn table values match:
                            DataTable dtitems = new DataTable();
                            // SqlCommand cmditem = new SqlCommand("select * from item_table where item_no='" + dt_pass_values.Rows[i]["item_no"].ToString() + "'", con);
                            SqlCommand cmditem = new SqlCommand("SP_SelectQuery", con);
                            cmditem.CommandType = CommandType.StoredProcedure;
                            cmditem.Parameters.AddWithValue("@ActionType", "ItemNo");
                            cmditem.Parameters.AddWithValue("@itemName", dt_pass_values.Rows[i]["item_no"].ToString());
                            cmditem.Parameters.AddWithValue("@ItemCode", "");

                            SqlDataAdapter adpitem = new SqlDataAdapter(cmditem);
                            adpitem.Fill(dtitems);
                            if (dtitems.Rows.Count > 0)
                            {
                                itemcodeitemtable = dtitems.Rows[0]["item_code"].ToString();
                                itemnameitemtable = dtitems.Rows[0]["item_name"].ToString();
                                //getting values to item table values is completed:

                                string Taxno = dt_pass_values.Rows[i]["Tax_No"].ToString() == "0" ? "1" : dt_pass_values.Rows[i]["Tax_No"].ToString();
                                SqlCommand cmd_taxname = new SqlCommand("select Tax_Name from tax_table where tax_No=@Taxno", con);
                                cmd_taxname.Parameters.AddWithValue("@Taxno", Taxno.ToString().Trim());
                                string TaxName = Convert.ToString(cmd_taxname.ExecuteScalar());

                                DgPurchase.Rows[i].Cells["ItemCode"].Value = itemcodeitemtable.ToString();
                                DgPurchase.Rows[i].Cells["ItemNames"].Value = itemnameitemtable.ToString();
                                DgPurchase.Rows[i].Cells["Unit"].Value = unitnames.ToString();
                                if (dt_pass_values.Rows[i]["nt_qty"].ToString() != null && dt_pass_values.Rows[i]["nt_qty"].ToString() != "")
                                {
                                    DgPurchase.Rows[i].Cells["Qt"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["nt_qty"]).ToString();
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Qt"].Value = "0";
                                }
                                if (dt_pass_values.Rows[i]["Rate"].ToString() != "" && dt_pass_values.Rows[i]["Rate"].ToString() != null)
                                {
                                    DgPurchase.Rows[i].Cells["Rate"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Rate"].ToString()).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Rate"].Value = "0.00";
                                }

                                if (dt_pass_values.Rows[i]["Tax_Rate"].ToString() != "" && dt_pass_values.Rows[i]["Tax_Rate"].ToString() != null)
                                {
                                    DgPurchase.Rows[i].Cells["TaxRate"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Tax_Rate"]).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["TaxRate"].Value = "0.00";
                                }
                                if (dt_pass_values.Rows[i]["Amount"].ToString() != "" && dt_pass_values.Rows[i]["Amount"].ToString() != null)
                                {
                                    DgPurchase.Rows[i].Cells["Amount"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Amount"]).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Amount"].Value = "0.00";
                                }
                                if (dt_pass_values.Rows[i]["Disc_per"].ToString() != "" && dt_pass_values.Rows[i]["Disc_per"].ToString() != null)
                                {
                                    DgPurchase.Rows[i].Cells["Disc"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Disc_per"]).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Disc"].Value = "0.00";
                                }
                                if (dt_pass_values.Rows[i]["Mrsp"].ToString() != "" || dt_pass_values.Rows[i]["Mrsp"].ToString() != null)
                                {
                                    DgPurchase.Rows[i].Cells["DiscAmt"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Disc_Amt"]).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["DiscAmt"].Value = "0.00";
                                }

                                if (dt_pass_values.Rows[i]["Mrsp"].ToString() != "" && dt_pass_values.Rows[i]["Mrsp"].ToString() != null)
                                {
                                    DgPurchase.Rows[i].Cells["Mrp"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Mrsp"]).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Mrp"].Value = "0.00";
                                }
                                if (dtitems.Rows[0]["Item_special1"].ToString() != null && dtitems.Rows[0]["Item_special1"].ToString() != "")
                                {
                                    DgPurchase.Rows[i].Cells["Special1"].Value = Convert.ToDouble(dtitems.Rows[0]["Item_special1"]).ToString("0.00");//only previous values getting from item table
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Special1"].Value = "0.00";
                                }
                                if (dtitems.Rows[0]["Item_special2"].ToString() != null && dtitems.Rows[0]["Item_special2"].ToString() != "")
                                {
                                    DgPurchase.Rows[i].Cells["Special2"].Value = Convert.ToDouble(dtitems.Rows[0]["Item_special2"]).ToString("0.00");//  ""     ""          ""            ""
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Special2"].Value = "0.00";
                                }
                                if (dtitems.Rows[0]["Item_special3"].ToString() != null && dtitems.Rows[0]["Item_special3"].ToString() != "")
                                {
                                    DgPurchase.Rows[i].Cells["Special3"].Value = Convert.ToDouble(dtitems.Rows[0]["Item_special3"]).ToString("0.00");//  ""     ""          ""            ""
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["Special3"].Value = "0.00";
                                }
                                if (dt_pass_values.Rows[i]["tot_amt"].ToString() != null && dt_pass_values.Rows[i]["tot_amt"].ToString() != "")
                                {
                                    DgPurchase.Rows[i].Cells["TotalAmt"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["tot_amt"]).ToString("0.00");
                                }
                                else
                                {
                                    DgPurchase.Rows[i].Cells["TotalAmt"].Value = "0.00";
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dt_pass_values.Rows[i]["Tax_No"])))
                                {
                                    DgPurchase.Rows[i].Cells["TaxName"].Value = TaxName.ToString();
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dt_pass_values.Rows[i]["Disc_Per"])))
                                {
                                    DgPurchase.Rows[i].Cells["Disc"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Disc_Per"]).ToString("0.00");
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dt_pass_values.Rows[i]["Disc_Amt"])))
                                {
                                    DgPurchase.Rows[i].Cells["DiscAmt"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["Disc_Amt"]).ToString("0.00");
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dt_pass_values.Rows[i]["tax_per"])))
                                {
                                    DgPurchase.Rows[i].Cells["TaxPer"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["tax_per"]).ToString("0.00");
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dt_pass_values.Rows[i]["tax_amt"])))
                                {
                                    DgPurchase.Rows[i].Cells["TaxAmt"].Value = Convert.ToDouble(dt_pass_values.Rows[i]["tax_amt"]).ToString("0.00");
                                }
                                
                                DgPurchase.Rows[i].Cells["strn_no"].Value = dt_pass_values.Rows[i]["strn_no"].ToString();//duplication id
                                DgPurchase.Rows[i].Cells["strn_sno"].Value = dt_pass_values.Rows[i]["strn_sno"].ToString();//orginal id
                                DgPurchase.Focus();
                                items_alter = "0";
                            }
                        }
                        //DgPurchase.CurrentCell = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"];
                        double amount = 0.00;
                        double qty = 0.00;
                        double gross_amount = 0.00;
                        for (int i = 0; i < DgPurchase.Rows.Count; i++)
                        {
                            //if (DgPurchase.Rows[i].Cells["Exp"].Value = null)
                            //{
                            amount += Convert.ToDouble(DgPurchase.Rows[i].Cells["Exp"].Value);
                            lblAmount.Text = amount.ToString();
                            // }
                            if (DgPurchase.Rows[i].Cells["Qt"].Value != null)
                            {
                                qty += Convert.ToDouble(DgPurchase.Rows[i].Cells["Qt"].Value);
                            }
                            lbl_Qty.Text = qty.ToString();
                        }
                        for (int j = 0; j < DgPurchase.Rows.Count; j++)
                        {
                            if (DgPurchase.Rows[j].Cells["TotalAmt"].Value != null)
                            {
                                gross_amount += Convert.ToDouble(DgPurchase.Rows[j].Cells["TotalAmt"].Value);
                            }
                            lblAmount.Text = gross_amount.ToString("0.00");
                            lbl_netAmount.Text = gross_amount.ToString("0.00");
                            txtInvalue.Text = gross_amount.ToString("0.00");
                        }
                        DgPurchase.Rows.Add();
                        lblItems.Text = Convert.ToInt32(DgPurchase.Rows.Count - 1).ToString();
                        SqlCommand cmd_dgdiscount = new SqlCommand("Select (case when purDiscount_table.DiscType=1 Then 'Tax' When purDiscount_table.DiscType=2 Then 'Discount' When purDiscount_table.DiscType=3 Then 'Additions' When purDiscount_table.DiscType=0 Then '' End) as Type,Ledsel_table.Ledsel_name As Details ,Convert(Numeric(18,2),purDiscount_table.PerQty) As DisPerQty_Pr ,Convert(Numeric(18,2), purDiscount_table.[Percent]) As Percent_Pr ,Convert(Numeric(18,2), purDiscount_table.Amount) As DiscountAmount from PurDiscount_Table,Ledsel_table where Ledsel_table.Ledger_no= purDiscount_table.ledgerNo and  purDiscount_table.PmasSno='" + id.ToString() + "' order by purDiscount_table.discSno", con);
                        DataTable dtDicountatable = new DataTable();
                        SqlDataAdapter adp_dgdiscount = new SqlDataAdapter(cmd_dgdiscount);
                        adp_dgdiscount.Fill(dtDicountatable);
                        if (dtDicountatable.Rows.Count > 0)
                        {
                            for(int i=0;i<dtDicountatable.Rows.Count;i++)
                            {
                                dggridvalues = "1";
                                DgDiscount.Rows.Add();
                                DgDiscount.Rows[i].Cells[0].Value = dtDicountatable.Rows[i]["Type"].ToString();
                                DgDiscount.Rows[i].Cells[1].Value = dtDicountatable.Rows[i]["Details"].ToString();
                                DgDiscount.Rows[i].Cells[2].Value = dtDicountatable.Rows[i]["DisPerQty_Pr"].ToString();
                                DgDiscount.Rows[i].Cells[3].Value = dtDicountatable.Rows[i]["Percent_Pr"].ToString();
                                DgDiscount.Rows[i].Cells[4].Value = dtDicountatable.Rows[i]["DiscountAmount"].ToString();
                                tDiscountGridCalculation();
                                dggridvalues = "";
                            }
                        }
                        if (dtDicountatable.Rows.Count > 0)
                        {
                            txtInvalue.Text = Convert.ToDouble(lblDiscountNetAmt.Text).ToString("0.00");
                        }
                        else
                        {
                        }

                    }
                }
                Datta.Rows.Clear();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string id;
        string unitnames;
        string itemcodeitemtable;
        string itemnameitemtable;
        int kr = 1;
        object[] array1 = new object[200];
        DataTable dt_gridload = new DataTable();
        DataTable dtGralter = new DataTable();
        DataTable dt_gridload1 = new DataTable();
        private void frmTaxCreation_Load(object sender, EventArgs e)
        {
            try
            {
                pnlHideUnhide.Visible = false;
                DgPurchase.Columns[2].Width = 400;
                DgPurchase.Columns[0].Width = 40;
                DgPurchase.Columns[4].Width = 60;
                pnDiscountPanel.Visible = false;
                Pnl_Back.Visible = false;

                for (int i = 0; i < 11; i++)
                {
                    DgPurchase.Rows.Add();
                }

                //if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value == null)
                {
                    if (DgPurchase.Rows.Count > 1)
                    {
                        var selected = DgPurchase.SelectedCells;
                        for (int x = 0; x < selected.Count;)
                        {
                            DgPurchase.ClearSelection();
                            break;
                        }
                        txtInvoiceNo.Focus();
                    }
                }
                DgPurchase.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

                DgPurchase.BackgroundColor = Color.White;
                if (id == "" || id == "0")
                {
                    load_check_box_values();
                    autonumner();
                    autonumner1();
                    auto_number_gen();
                    pnlHideUnhide.Visible = false;
                    txtCash.Text = "Cash";
                    lvItems_items_add();
                    lvItems.Visible = false;
                   
                    comman_listview();
                    txtInvoiceNo.Focus();
                    suppliers_entry();
                    listcommodity();
                    purchaseType();
                    counterType();
                    // voucher_no();
                    txtInvoiceNo.Focus();
                    txtInvoiceNo.Select();
                    txtDate.Text = DtpPurchaseDate.Text;
                    txtIvDate.Text = DtpInvoiceDate.Text;
                }
                else
                {
                    load_check_box_values();
                    autonumner();
                    autonumner1();
                    // auto_number_gen();
                    pnlHideUnhide.Visible = false;
                    txtCash.Text = "Cash";
                    lvItems_items_add();
                    lvItems.Visible = false;
                    // DgPurchase.AutoGenerateColumns = false;
                    comman_listview();
                    txtInvoiceNo.Focus();
                    suppliers_entry();
                    listcommodity();
                    purchaseType();
                    counterType();
                    txtInvoiceNo.Focus();
                    txtInvoiceNo.Select();



                }
                //load_check_box_values();

                //Datagridview row Hight increase:
                DgPurchase.RowTemplate.Height = 80;

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }

        }
        public void suppliers_entry()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "LedgerType");

                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ItemName", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt1.Rows.Clear();
                lvSuppliers.Items.Clear();
                adp.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        lvSuppliers.Items.Add(dt1.Rows[i]["Ledsel_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void listcommodity()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "Commodity");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ItemName", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        DataTable dtpurchase1_ = new DataTable();
        public void purchaseType()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "PurchaseType");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ItemName", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dtpurchase1_.Rows.Clear();
                lvPurchase.Items.Clear();
                adp.Fill(dtpurchase1_);
                if (dtpurchase1_.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpurchase1_.Rows.Count; i++)
                    {
                        lvPurchase.Items.Add(dtpurchase1_.Rows[i]["PurType_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        // string conter_no;
        DataTable dt = new DataTable();
        public void counterType()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //  SqlCommand cmd = new SqlCommand("select ctr_name  from counter_table order by ctr_name  ASC", con);
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "COUNTERTYPE");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ItemName", "");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                lvCounters.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // lvSuppliers.Text = "(Demo) List Models";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lvCounters.Items.Add(dt.Rows[i]["ctr_name"].ToString());
                    }
                    if (lvCounters.Items.Count > 0)
                    {
                        if (txtCounter.Text.Trim() == "")
                        {
                            txtCounter.Text = dt.Rows[0]["ctr_name"].ToString();
                        }
                        else
                        {
                            if (txtCounter.Text.Trim() != "")
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (txtCounter.Text.Trim() == dt.Rows[j]["ctr_name"].ToString())
                                    {
                                        lvCounters.SelectedIndex = j;
                                    }
                                }
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
        public void lvItems_items_add()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SelectItems");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ItemName", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_items.Clear();
                adp.Fill(dt_items);
                if (dt_items.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_items.Rows.Count; i++)
                    {
                        lvItems.Items.Add((dt_items.Rows[i]["Item_name"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void auto_number_gen()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SP_SelectQuery_Return", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "VoucherNo");
                SqlParameter VoucherNo = new SqlParameter("@VoucherNo", SqlDbType.VarChar, 50);
                VoucherNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(VoucherNo);
                cmd.ExecuteNonQuery();
                lblBillNo.Text = (string)cmd.Parameters["@VoucherNo"].Value;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        string number_serious;
        string number_seroius_strn_Sno;
        public void autonumner()
        {
            try
            {
                //select querey auto number:
                string qry = "select max(StrnNo) from NumberTable";
                string StrnNo = auto1(qry);
                number_serious = StrnNo;
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void autonumner1()//auto increment number every times:
        {
            try
            {
                //select querey auto number:
                string qry = "select max(StrnSno) from NumberTable";
                string Strnsno = auto1(qry);
                number_seroius_strn_Sno = Strnsno;
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public string auto1(string qry)
        {
           
                if (ConnectionState.Open == con.State)
                {
                    con.Close();
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(qry, con);
                    int no = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (no < 9)
                    {
                        return (Convert.ToString(no + 1));
                    }
                    else if (no < 99)
                    {
                        return (Convert.ToString(no + 1));
                    }
                    else if (no < 999)
                    {
                        return (Convert.ToString(no + 1));
                    }
                    else
                    {
                        return (Convert.ToString(no + 1));
                    }
                }
                catch
                {
                    return ("1");
                }
                con.Close();
            
        }

        private void DgPurchase_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ie = Convert.ToString(e.RowIndex.ToString());
                DgPurchase.CurrentRow.Cells["Sno"].Value = (Convert.ToInt32(ie) + 1).ToString();
                if (enter_emptystring == "1")
                {
                    if (DgPurchase.Rows[e.RowIndex].Cells["ItemNames"].Value == null && DgPurchase.Rows[e.RowIndex].Cells["ItemCode"].Value == null)
                    {
                        if (DgPurchase.CurrentCell.ColumnIndex > 2)
                        {
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value == null)
                            {
                                if (DgPurchase.Rows.Count > 1)
                                {
                                    var selected = DgPurchase.SelectedCells;
                                    for (int x = 0; x < selected.Count;)
                                    {
                                        DgPurchase.ClearSelection();
                                        MyMessageBox1.ShowBox("Please Enter Item Code Or Item Name", "Warning");
                                        break;
                                    }
                                    btnSave.Focus();
                                }
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
        public void nextcell()
        {
            if (this.DgPurchase.CurrentCell.ColumnIndex != this.DgPurchase.Columns.Count - 1)
            {
                int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex + 1);
                SetColumnIndex method = new SetColumnIndex(Mymethod);
                this.DgPurchase.BeginInvoke(method, nextindex+3);
            }
        }
        public void previouscell()
        {
            if (this.DgPurchase.CurrentCell.ColumnIndex != this.DgPurchase.Columns.Count - 1)
            {
                int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex + 1);
                SetColumnIndex method = new SetColumnIndex(Mymethod);
                this.DgPurchase.BeginInvoke(method, nextindex - 1);
            }
        }
        public delegate void SetColumnIndex(int i);
        public void Mymethod(int columnIndex)
        {
            if (items_alter!="0")
            {
                    this.DgPurchase.CurrentCell = this.DgPurchase.CurrentRow.Cells[columnIndex];
                    this.DgPurchase.BeginEdit(true);
            }
            else
            {
                this.DgPurchase.CurrentCell = this.DgPurchase.CurrentRow.Cells[2];
                this.DgPurchase.BeginEdit(true);
                items_alter = "1";
            }
        }
        private void gridDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DgPurchase.CurrentCell.ColumnIndex == 5|| DgPurchase.CurrentCell.ColumnIndex == 6 || DgPurchase.CurrentCell.ColumnIndex == 8 || DgPurchase.CurrentCell.ColumnIndex == 7 || DgPurchase.CurrentCell.ColumnIndex ==9 || DgPurchase.CurrentCell.ColumnIndex == 10 || DgPurchase.CurrentCell.ColumnIndex == 11 || DgPurchase.CurrentCell.ColumnIndex == 12 || DgPurchase.CurrentCell.ColumnIndex == 13 || DgPurchase.CurrentCell.ColumnIndex == 14 )
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;

                }
                // allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
        }
        System.Windows.Forms.Control cntObject;
        private void DgPurchase_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (DgPurchase.CurrentCell.ColumnIndex == 0)
                {
                    e.Control.TextChanged += new EventHandler(textbox_TextChanged);
                    //  e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnTextBoxKeyDown); 
                    cntObject = (System.Windows.Forms.Control)e.Control;
                    cntObject.TextChanged += textbox_TextChanged;
                    //e.Control.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextBoxKeyDown);
                    //cntObject.KeyDown += OnTextBoxKeyDown;
                }
                if (DgPurchase.CurrentCell.ColumnIndex == 1)
                {
                    e.Control.TextChanged += new EventHandler(textbox1_TextChanged);
                    // e.Control.KeyPress += new System.Windows.Forms.KeyEventHandler(OnTextBoxKeyDown); 
                    cntObject = (System.Windows.Forms.Control)e.Control;
                    cntObject.TextChanged += textbox1_TextChanged;
                    //TextBox textBox = edit.Control as TextBox;
                    //textBox.TextChanged += new EventHandler(textBox_TextChanged);
                }
                //if (DgPurchase.CurrentCell.ColumnIndex == 2)
                //{
                //    cntObject.Leave += new EventArgs(textbox1_Text2);
                //}
                {
                    TextBox txt = e.Control as TextBox;
                    if (txt != null)
                    {
                        txt.KeyPress += new KeyPressEventHandler(gridDisplay_KeyPress);
                    }
                }
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table order by Item_name ASC", con);
                DataTable autofind = new DataTable();
                autofind.Rows.Clear();
                SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
                nameadp.Fill(autofind);
                con.Close();
                string[] postSource=null;
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["ItemNames"].Index) //Item_name
                {
                    postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Sno"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }

                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["ItemCode"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["ItemNames"].Index) //Item_name
                {
                    postSource = null;
                    postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Remarks"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Unit"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Qt"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Rate"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["TaxRate"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Amount"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Disc"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["DiscAmt"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Mrp"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Special1"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Special2"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["Special3"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["TotalAmt"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
               
                if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["TaxName"].Index)
                {
                    TextBox autoText = e.Control as TextBox;
                    if (autoText != null)
                    {
                        autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                        autoText.AutoCompleteSource =  AutoCompleteSource.CustomSource;
                        AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                        addItems(DataCollection);
                        autoText.AutoCompleteCustomSource = DataCollection;
                    }    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        public void addItems(AutoCompleteStringCollection col)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand namecmd1 = new SqlCommand("select Tax_name from Tax_Table order by Tax_name ASC", con);
            DataTable autofind1 = new DataTable();
            autofind1.Rows.Clear();
            SqlDataAdapter nameadp1 = new SqlDataAdapter(namecmd1);
            nameadp1.Fill(autofind1);
            for (int i = 0; i < autofind1.Rows.Count; i++)
            {
                col.Add(autofind1.Rows[i]["Tax_name"].ToString());
            }
        }
        DataTable dt_items = new DataTable();
        double tax_percentage = 0.00;
        string Ttaxname = "";
        public void ItemcodeorItemName(string itemNamecode)        
        {
            try
            {
                tax_percentage = 0.00;
                Ttaxname = "";
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "Action");
                cmd.Parameters.AddWithValue("@ItemCode", itemNamecode);
                cmd.Parameters.AddWithValue("@itemName", itemNamecode);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                dt_items.Rows.Clear();
                adp.Fill(dt_items);
                if (dt_items.Rows.Count > 0)
                {
                    if (dt_items.Rows[0]["Item_code"].ToString().Trim() != "" && dt_items.Rows[0]["Item_code"].ToString() != null)
                    {
                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value = dt_items.Rows[0]["Item_code"].ToString();
                    }
                    else
                    {
                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";
                    }
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value = dt_items.Rows[0]["Item_name"].ToString();
                    SqlCommand cmd_nostable = new SqlCommand("select * from unit_table where unit_no='" + dt_items.Rows[0]["unit_no"].ToString() + "'", con);
                    SqlDataAdapter adp_nostable = new SqlDataAdapter(cmd_nostable);
                    DataTable dtnostable = new DataTable();
                    dtnostable.Rows.Clear();
                    adp_nostable.Fill(dtnostable);
                    if (dtnostable.Rows.Count > 0)
                    {
                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Unit"].Value = dtnostable.Rows[0]["Unit_name"].ToString();
                    }
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Mrp"].Value = dt_items.Rows[0]["SmanAmt"].ToString() == null || dt_items.Rows[0]["SmanAmt"].ToString() == "" ? "0.00" : dt_items.Rows[0]["SmanAmt"].ToString();
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = dt_items.Rows[0]["item_ndp"].ToString() == "" || dt_items.Rows[0]["item_ndp"].ToString() == null ? "0.00" : Convert.ToDouble(dt_items.Rows[0]["item_ndp"].ToString()).ToString("0.00");
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special1"].Value = dt_items.Rows[0]["Item_special1"].ToString() == null || dt_items.Rows[0]["Item_special1"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_items.Rows[0]["Item_special1"].ToString()).ToString("0.00");
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value = dt_items.Rows[0]["Item_special2"].ToString() == null || dt_items.Rows[0]["Item_special2"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_items.Rows[0]["Item_special2"].ToString()).ToString("0.00");
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special3"].Value = dt_items.Rows[0]["Item_special3"].ToString() == null || dt_items.Rows[0]["Item_special3"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_items.Rows[0]["Item_special3"].ToString()).ToString("0.00");
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value = "0.00";
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = "0.00";
                   
                    
                    //getting Taxname:
                   // Ttaxname = dt_items.Rows[0]["Tax_name"].ToString();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlCommand cmd_Tax = new SqlCommand("Select Ptax_percent from Tax_Table where Tax_no=@TaxNo", con);
                    cmd_Tax.Parameters.AddWithValue("@TaxNo",dt_items.Rows[0]["Tax_no"].ToString());
                    SqlDataAdapter adp_tax = new SqlDataAdapter(cmd_Tax);
                    DataTable dtTaxname = new DataTable();
                    dtTaxname.Rows.Clear();
                    adp_tax.Fill(dtTaxname);
                    if (dtTaxname.Rows.Count > 0)
                    {
                        tax_percentage = Convert.ToDouble(dtTaxname.Rows[0]["Ptax_percent"].ToString());
                    }
                    else
                    {
                        tax_percentage = 0.00;
                    }

                    SqlCommand cmd_Ttaxname = new SqlCommand("Select tax_name from Tax_Table where Tax_no=@TaxNo", con);
                    cmd_Ttaxname.Parameters.AddWithValue("@TaxNo", dt_items.Rows[0]["Tax_no"].ToString());
                    SqlDataAdapter adpTaxname = new SqlDataAdapter(cmd_Ttaxname);
                    DataTable dt_taxname = new DataTable();
                    dt_taxname.Rows.Clear();
                    adpTaxname.Fill(dt_taxname);
                    if (dt_taxname.Rows.Count > 0)
                    {
                        Ttaxname = (dt_taxname.Rows[0]["tax_name"].ToString() == "" ? "" :  dt_taxname.Rows[0]["tax_name"].ToString());
                    }
                    else
                    {
                        Ttaxname = "Nill";
                    }

                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value =Ttaxname;
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = "0.00";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string Chk = "1";
        public void ItemcodeorItemName1(string itemNamecode)
        {
            try
            {
                if (Chk == "1")
                {
                    DataTable dt_taxC = new DataTable();
                    tax_percentage = 0.00;
                    Ttaxname = "";
                    SqlCommand cmd_Tx = new SqlCommand("select Tax_No from item_table where Item_table.Item_name=@itemName", con);
                    cmd_Tx.Parameters.AddWithValue("@itemName", itemNamecode);
                    dt_taxC.Rows.Clear();
                    SqlDataAdapter adp_tax = new SqlDataAdapter(cmd_Tx);
                    adp_tax.Fill(dt_taxC);
                    if (dt_taxC.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("select Ptax_percent,Tax_name from Tax_table where Tax_no='" + dt_taxC.Rows[0]["Tax_No"] + "'", con);
                        cmd.Parameters.AddWithValue("@itemName", itemNamecode);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        dt_items.Rows.Clear();
                        adp.Fill(dt_items);
                        if (dt_items.Rows.Count > 0)
                        {
                            //string TtaxNo = "";
                            //TtaxNo = dt_items.Rows[0]["Tax_no"].ToString();
                            //SqlCommand cmd_Ttaxname = new SqlCommand("Select tax_name from Tax_Table where Tax_no=@TaxNo", con);
                            //cmd_Ttaxname.Parameters.AddWithValue("@TaxNo", TtaxNo.ToString());
                            tax_percentage = Convert.ToDouble(dt_items.Rows[0]["Ptax_percent"]);
                            Ttaxname = Convert.ToString(dt_items.Rows[0]["Tax_name"]);
                        }
                    }
                }

                else
                {
                    SqlCommand cmd = new SqlCommand("select Ptax_percent,Tax_name from Tax_table where Tax_Name=@itemName", con);
                    cmd.Parameters.AddWithValue("@itemName", itemNamecode);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt_items.Rows.Clear();
                    adp.Fill(dt_items);
                    if (dt_items.Rows.Count > 0)
                    {
                        //string TtaxNo = "";
                        //TtaxNo = dt_items.Rows[0]["Tax_no"].ToString();
                        //SqlCommand cmd_Ttaxname = new SqlCommand("Select tax_name from Tax_Table where Tax_no=@TaxNo", con);
                        //cmd_Ttaxname.Parameters.AddWithValue("@TaxNo", TtaxNo.ToString());
                        tax_percentage = Convert.ToDouble(dt_items.Rows[0]["Ptax_percent"]);
                        Ttaxname = Convert.ToString(dt_items.Rows[0]["Tax_name"]);
                    }
                }
            }
            catch
            {
            }
        }
        string chk = "";
        private void textbox_TextChanged(object sender, EventArgs e)
        {   
        }
        private void textbox1_TextChanged(object sender, EventArgs e)
        {   
        }
        public void load_check_box_values()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dt_purtabel = new System.Data.DataTable();
                SqlCommand cmd = new SqlCommand("select * from Pur_HideUnhide_Table", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_purtabel.Rows.Clear();
                adp.Fill(dt_purtabel);

                if (dt_purtabel.Rows.Count > 0)
                {
                    string sno_no = dt_purtabel.Rows[0]["sno"].ToString();
                    if (sno_no == "1")
                    {
                        int j = 0;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Sno"].Visible = true;
                    }
                    if (sno_no == "0")
                    {
                        int j = 0;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Sno"].Visible = false;
                    }
                    string codes_entry = dt_purtabel.Rows[0]["Code"].ToString();
                    if (codes_entry == "1")
                    {

                        int j = 1;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["ItemCode"].Visible = true;
                    }
                    if (codes_entry == "0")
                    {

                        int j = 1;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["ItemCode"].Visible = false;
                    }
                    string Name_entry = dt_purtabel.Rows[0]["Name"].ToString();
                    if (Name_entry == "1")
                    {

                        int j = 2;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["ItemNames"].Visible = true;

                    }
                    if (Name_entry == "0")
                    {
                        int j = 2;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["ItemNames"].Visible = false;

                    }
                    string remarks_entry = dt_purtabel.Rows[0]["Remarks"].ToString();
                    if (remarks_entry == "1")
                    {
                        int j = 3;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Remarks"].Visible = true;
                    }
                    if (remarks_entry == "0")
                    {
                        int j = 3;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Remarks"].Visible = false;
                    }
                    string Unit_entry = dt_purtabel.Rows[0]["Unit"].ToString();
                    if (Unit_entry == "1")
                    {
                        int j = 4;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Unit"].Visible = true;
                    }
                    if (Unit_entry == "0")
                    {
                        int j = 4;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Unit"].Visible = false;
                    }
                    string qty_entry = dt_purtabel.Rows[0]["Qty"].ToString();
                    if (qty_entry == "1")
                    {
                        int j = 5;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Qt"].Visible = true;
                    }
                    if (qty_entry == "0")
                    {
                        int j = 5;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Qt"].Visible = false;
                    }
                    string rate_entry = dt_purtabel.Rows[0]["Rate"].ToString();
                    if (rate_entry == "1")
                    {
                        int j = 6;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 6;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Rate"].Visible = true;
                    }
                    if (rate_entry == "0")
                    {
                        int j = 6;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 6;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Rate"].Visible = false;
                    }
                    string Taxrate_entry = dt_purtabel.Rows[0]["TaxRate"].ToString();
                    if (Taxrate_entry == "1")
                    {
                        int j = 7;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 7;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["TaxRate"].Visible = true;
                    }
                    if (Taxrate_entry == "0")
                    {
                        int j = 7;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 7;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["TaxRate"].Visible = false;
                    }
                    string Amount_entry = dt_purtabel.Rows[0]["Amount"].ToString();
                    if (Amount_entry == "1")
                    {
                        int j = 8;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 8;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Amount"].Visible = true;
                    }
                    if (Amount_entry == "0")
                    {
                        int j = 8;
                        //    int j = 17;
                        //    int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //    j = i - 8;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Amount"].Visible = false;
                    }
                    string disc_entry = dt_purtabel.Rows[0]["Disc"].ToString();
                    if (disc_entry == "1")
                    {
                        int j = 9;

                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 9;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Disc"].Visible = true;
                    }
                    if (disc_entry == "0")
                    {
                        int j = 9;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 9;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Disc"].Visible = false;
                    }
                    string DisCount_amount = dt_purtabel.Rows[0]["DiscAmount"].ToString();
                    if (DisCount_amount == "1")
                    {
                        int j = 10;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 10;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["DiscAmt"].Visible = true;
                    }
                    if (DisCount_amount == "0")
                    {
                        int j = 10;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 10;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["DiscAmt"].Visible = false;
                    }
                    string Mrp_entry = dt_purtabel.Rows[0]["Mrp"].ToString();
                    if (Mrp_entry == "1")
                    {
                        int j = 11;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 11;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Mrp"].Visible = true;
                    }
                    if (Mrp_entry == "0")
                    {
                        int j = 11;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 11;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Mrp"].Visible = false;
                    }
                    string Special1_entry = dt_purtabel.Rows[0]["Special_1"].ToString(); ;
                    if (Special1_entry == "1")
                    {
                        int j = 12;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 12;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Special1"].Visible = true;
                    }
                    if (Special1_entry == "0")
                    {
                        int j = 12;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 12;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Special1"].Visible = false;
                    }

                    string special2_entry = dt_purtabel.Rows[0]["Special_2"].ToString();
                    if (special2_entry == "1")
                    {
                        int j = 13;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Special2"].Visible = true;
                    }
                    if (special2_entry == "0")
                    {
                        int j = 13;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Special3"].Visible = false;
                    }

                    string special3_entry = dt_purtabel.Rows[0]["Special_3"].ToString();
                    if (special3_entry == "1")
                    {
                        int j = 14;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Special3"].Visible = true;
                    }
                    if (special3_entry == "0")
                    {
                        int j = 14;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Special1"].Visible = false;
                    }
                    string totalAmount_entry = dt_purtabel.Rows[0]["TotalAmount"].ToString();

                    if (totalAmount_entry == "1")
                    {
                        int j = 15;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["TotalAmt"].Visible = true;
                    }
                    if (totalAmount_entry == "0")
                    {
                        int j = 15;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["TotalAmt"].Visible = false;
                    }
                    string exp_date_entry = dt_purtabel.Rows[0]["exp"].ToString();
                    if (exp_date_entry == "1")
                    {
                        int j = 16;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["Exp"].Visible = true;
                    }
                    if (exp_date_entry == "0")
                    {
                        int j = 16;

                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["Exp"].Visible = false;
                    }
                    string TtaxsName = dt_purtabel.Rows[0]["TaxName"].ToString();
                    if (TtaxsName == "1")
                    {
                        int j = 17;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["TaxName"].Visible = true;
                    }
                    if (TtaxsName == "0")
                    {
                        int j = 17;

                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["TaxName"].Visible = false;
                    }
                    string TtaxsPer = dt_purtabel.Rows[0]["TaxPer"].ToString();
                    if (TtaxsPer == "1")
                    {
                        int j = 18;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["TaxPer"].Visible = true;
                    }
                    if (TtaxsPer == "0")
                    {
                        int j = 18;

                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["TaxPer"].Visible = false;
                    }
                    string TtaxsAmt = dt_purtabel.Rows[0]["TaxAmt"].ToString();
                    if (TtaxsAmt == "1")
                    {
                        int j = 19;
                        //int j = 17;
                        //int i = Convert.ToInt32(Chk_colHeader.Items.Count);
                        //j = i - 13;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Unchecked);
                        DgPurchase.Columns["TaxAmt"].Visible = true;
                    }
                    if (TtaxsAmt == "0")
                    {
                        int j = 19;
                        Chk_colHeader.SetItemCheckState(j, CheckState.Checked);
                        DgPurchase.Columns["TaxAmt"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void hideColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlHideUnhide.Visible = true;
        }
        string _Exp = "1"; string _total_amount = "1"; string _colum_name = "1"; string _code = "1"; string _Special3 = "1"; string _Special2 = "1"; string _Special1 = "1"; string _Mrp = "1"; string _DiscAmount = "1"; string _disc = "1"; string _TaxRate = "1"; string _amount = "1"; string _rate = "1"; string _qty = "1"; string _serialno = "1"; string _Remarks = "1"; string _Unit = "1"; string _TaxName = "1"; string _TaxPer = "1"; string _TaxAmt = "1";
        private void btnHideUnHideOk_Click(object sender, EventArgs e)
        {
            try
            {

                _Exp = "1"; _total_amount = "1"; _colum_name = "1"; _code = "1"; _Special3 = "1"; _Special2 = "1"; _Special1 = "1"; _Mrp = "1"; _DiscAmount = "1"; _disc = "1"; _TaxRate = "1"; _amount = "1"; _rate = "1"; _qty = "1"; _serialno = "1"; _Remarks = "1"; _Unit = "1"; _TaxName = "1"; _TaxPer = "1"; _TaxAmt = "1"; _Exp = "1";
                foreach (var item in Chk_colHeader.CheckedItems)
                {
                    int r = Convert.ToInt32(Chk_colHeader.SelectedIndex);

                    if (item == "ItemName")
                    {
                        _colum_name = "0";
                        DgPurchase.Columns["ItemNames"].Visible = false;
                    }
                    if (item == "ItemCode")
                    {
                        _code = "0";
                        DgPurchase.Columns["ItemCode"].Visible = false;
                    }
                    if (item == "S.No")
                    {
                        _serialno = "0";
                        DgPurchase.Columns["Sno"].Visible = false;
                    }
                    if (item == "Remarks")
                    {
                        _Remarks = "0";
                        DgPurchase.Columns["Remarks"].Visible = false;
                    }
                    if (item == "Unit")
                    {
                        _Unit = "0";
                        DgPurchase.Columns["Unit"].Visible = false;
                    }
                    if (item == "Qty")
                    {
                        _qty = "0";
                        DgPurchase.Columns["Qt"].Visible = false;
                    }
                    if (item == "Rate")
                    {
                        _rate = "0";
                        DgPurchase.Columns["Rate"].Visible = false;
                    }
                    if (item == "Tax Rate")
                    {
                        _TaxRate = "0";
                        DgPurchase.Columns["TaxRate"].Visible = false;
                    }
                    if (item == "Amount")
                    {
                        _amount = "0";
                        DgPurchase.Columns["Amount"].Visible = false;
                    }
                    if (item == "Disc")
                    {
                        _disc = "0";
                        DgPurchase.Columns["Disc"].Visible = false;
                    }
                    if (item == "Discount Amount")
                    {
                        _DiscAmount = "0";
                        DgPurchase.Columns["DiscAmt"].Visible = false;
                    }
                    if (item == "Mrp")
                    {
                        _Mrp = "0";

                        DgPurchase.Columns["Mrp"].Visible = false;
                    }
                    if (item == "Special-1")
                    {
                        _Special1 = "0";
                        DgPurchase.Columns["Special1"].Visible = false;
                    }
                    if (item == "Special-2")
                    {
                        _Special2 = "0";
                        DgPurchase.Columns["Special2"].Visible = false;
                    }
                    if (item == "Special-3")
                    {
                        _Special3 = "0";
                        DgPurchase.Columns["Special3"].Visible = false;
                    }
                    if (item == "Total Amount")
                    {
                        _total_amount = "0";
                        DgPurchase.Columns["TotalAmt"].Visible = false;
                    }
                    if (item == "Exp")
                    {
                        _Exp = "0";
                        DgPurchase.Columns["Exp"].Visible = false;
                    }
                    if (item == "Tax Name")
                    {
                        _TaxName = "0";
                        DgPurchase.Columns["TaxName"].Visible = false;
                    }
                    if (item == "Tax Per")
                    {
                        _TaxPer = "0";
                        DgPurchase.Columns["TaxPer"].Visible = false;
                    }
                    if (item == "Tax Amt")
                    {
                        _TaxAmt = "0";
                        DgPurchase.Columns["TaxAmt"].Visible = false;
                    }
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                }

                pnlHideUnhide.Visible = false;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        string enter_emptystring = "";
        private void DgPurchase_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Datta.Rows.Count <= 0)
                {
                    if (e.ColumnIndex == 1)
                    {
                        if (DgPurchase.CurrentRow != null && e.ColumnIndex == 1)
                        {
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value != "")
                            {
                                string itemcode = "";
                                if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value != null)
                                {
                                    itemcode = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value.ToString();
                                    ItemcodeorItemName(itemcode);
                                    if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value != null)
                                    {
                                        if (dt_items.Rows.Count > 0)
                                        {
                                            nextcell();
                                            enter_emptystring = "1";
                                        }
                                        else
                                        {
                                            MyMessageBox1.ShowBox("ItemCode Not Found", "Warning");
                                            int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex + 1);
                                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                                            this.DgPurchase.BeginInvoke(method, 1);
                                        }
                                    }
                                    else
                                    {
                                        //MyMessageBox1.ShowBox("Please Enter Correct ItemCode", "Warning");
                                        //previouscell();  
                                        // DgPurchase.Focus();
                                    }
                                }
                            }
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        if (DgPurchase.CurrentRow != null && e.ColumnIndex == 2)
                        {
                            string itemname = "";
                            if (DgPurchase.Rows[e.RowIndex].Cells["ItemNames"].Value != null)
                            {
                                itemname = DgPurchase.Rows[e.RowIndex].Cells["ItemNames"].Value.ToString();
                                ItemcodeorItemName(itemname);
                                if (itemname != null)
                                {
                                    if (dt_items.Rows.Count > 0)
                                    {
                                        //  if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
                                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
                                        {
                                            int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex + 1);
                                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                                            this.DgPurchase.BeginInvoke(method, nextindex + 2);
                                            enter_emptystring = "1";
                                        }
                                    }
                                    else
                                    {
                                        MyMessageBox1.ShowBox("Please Enter Correct ItemName or ItemCode", "Warning");
                                        int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex + 1);
                                        SetColumnIndex method = new SetColumnIndex(Mymethod);
                                        this.DgPurchase.BeginInvoke(method, 2 - 1);
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 4)
                    {

                    }
                    else 
                        if (DgPurchase.CurrentRow != null && e.ColumnIndex == 5)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value != "" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value != "0.00" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != "0.00" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != "")
                        {
                            double Dist1 = 0.00;
                            discount = 0.00;
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value)).ToString("0.00");
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value).ToString("0.00");
                            Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                            if (Dist1 > 0)
                            {
                                discount = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                            {
                                discount = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                            }

                            gridrows_calculatoin();
                            AmtFinal = 0.00;
                            AmtFinal = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value));
                            TaxAmtFinal = (AmtFinal * tax_percentage / 100);
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = (TaxAmtFinal).ToString("0.00");

                            TotTaxAmt = 0.00;
                            TotTaxAmt = Convert.ToDouble(AmtFinal + TaxAmtFinal - discount);
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = TotTaxAmt.ToString("0.00");
                          //  DgDiscount_CellValueChanged(sender,e);

                            DgDiscountCalculations();
                        }
                        double ini_0 = 1, ini2 = 1;
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
                            ini_0 = 0;

                        }
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value = "0";
                            ini2 = 0;
                        }
                        if (ini_0 != 1 || ini2 != 1)
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = "0.00";
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 6)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value != "" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value != "0.00" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != "0.00" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != "")
                        {
                            double TaxperAmt = 0.00, discountAmt=0.00;
                            if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value)) && (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value)>0))
                            {
                              //  DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value).ToString("0.00");
                                TaxperAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value) * 100 / (100 + tax_percentage));
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = TaxperAmt.ToString("0.00");
                            }
                            else
                            {
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                            }
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value)).ToString("0.00");
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value).ToString("0.00");
                         
                            gridrows_calculatoin();
                            AmtFinal = 0.00;
                            tFAmount = 0.00;
                            tFAmount = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value));
                            TaxAmtFinal = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value));
                            TaxAmtFinal = (TaxAmtFinal * tax_percentage / 100);

                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = (TaxAmtFinal).ToString("0.00");

                            discount = 0.00;
                            double Dist1=0.00;
                            Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                            if(Dist1>0)
                            {
                            discount = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                            {
                                discount = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                            }
                            TotTaxAmt = 0.00;
                            TotTaxAmt = Convert.ToDouble(tFAmount + TaxAmtFinal - discount);
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = TotTaxAmt.ToString("0.00");

                         //   TaxCalculationGrid();
                            
                        }
                        double ini_0 = 1, ini2 = 1;
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
                            ini_0 = 0;

                        }
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value = "0";
                            ini2 = 0;
                        }
                        if (ini_0 != 1 || ini2 != 1)
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = "0.00";
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 7)
                      {
                            string itemcode = "";
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
                            {
                                chk = "1";
                                itemcode = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString();
                                if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value)))
                                {
                                    ItemcodeorItemName1(itemcode);
                                }
                                else
                                {
                                    chk = "0";
                                    ItemcodeorItemName1(itemcode);
                                    chk = "1";
                                }
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value = Ttaxname.ToString();
                               // TaxCalculationGrid();
                            }
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value = "0.00";
                            if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value)))
                            {

                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = "0.00";
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = "0.00";
                            }
                            
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value)) && Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value) != 0.00)
                            {
                                double TaxperAmt = 0.00, discountAmt = 0.00,TtRate=0.00;
                                double Dist1 = 0.00;
                                if (!string.IsNullOrEmpty(tax_percentage.ToString().Trim()) && tax_percentage != 0.00)
                                {

                                   
                                   
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value).ToString("0.00");
                                    TaxperAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value) * 100 / (100 + tax_percentage));
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = TaxperAmt.ToString("0.00");
                                    AmtFinal = 0.00;
                                    AmtFinal = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value));
                                    TaxAmtFinal = ((AmtFinal * tax_percentage / 100));
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = (TaxAmtFinal).ToString("0.00");
                                    
                                    TotTaxAmt = 0.00;
                                    Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                                    if (Dist1 > 0)
                                    {

                                        discountAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                                    {
                                        discountAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                                    }
                                    TotTaxAmt = Convert.ToDouble(AmtFinal + TaxAmtFinal - discountAmt);
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = TotTaxAmt.ToString("0.00");
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value)))
                                    {
                                       
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value).ToString("0.00");
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value).ToString("0.00");
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                                        double TAmt=0.00,FTotAmt=0.00;
                                        TAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
                                        FTotAmt = (TAmt * tax_percentage / 100);
                                        //FTotAmt += Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = FTotAmt.ToString("0.00");


                                        Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                                        if (Dist1 > 0)
                                        {
                                            discountAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                                        }
                                        if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                                        {
                                            discountAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                                        }
                                        TotTaxAmt=0.00;
                                        TotTaxAmt = Convert.ToDouble(FTotAmt + TAmt - discountAmt);
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = TotTaxAmt.ToString("0.00");
                                    }
                                }
                            }
                            else
                            {
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value).ToString("0.00");
                                if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value)) && Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value) != 0.00)
                                {
                                    double discountAmt = 0.00;
                                    double Dist1 = 0.00;
                                    
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value).ToString("0.00");

                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                                    double TAmt = 0.00, FTotAmt = 0.00;
                                    TAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
                                    FTotAmt = (TAmt * tax_percentage / 100);
                                    //FTotAmt += Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = FTotAmt.ToString("0.00");


                                    Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                                    if (Dist1 > 0)
                                    {
                                        discountAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                                    {
                                        discountAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                                    }
                                    TotTaxAmt = 0.00;
                                    TotTaxAmt = Convert.ToDouble(FTotAmt + TAmt - discountAmt);
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = TotTaxAmt.ToString("0.00");
                                }
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                            }
                        }
                        gridrows_calculatoin();
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 8)
                    {

                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = "0.00";
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 9)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value != "" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value != "0.00" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value != "")
                        {
                            double tTaxPerAmt = 0.00;

                            discount = 0.00;
                            tTaxPerAmt = 0.00;
                            discount = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                            double discountminis = 0.00;
                            discountminis = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value) - (Convert.ToDouble(discount)));
                            tTaxPerAmt = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value);
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = (discountminis + tTaxPerAmt).ToString("0.00");
                            gridrows_calculatoin();
                        }
                        else
                        {
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == "")
                            {
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value = "0.00";
                            }
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == "")
                            {
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = "0.00";
                            }
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value).ToString("0.00");

                            gridrows_calculatoin();

                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 10)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value != "" && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value != "0.00")
                        {
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value != "")
                            {
                               // TaxCalculationGrid();
                              //  DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = (Convert.ToDouble((DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) - Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)).ToString("0.00");
                                if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value)))
                                {
                                    Chk = "0";
                                    double tTFinale = 0.00, tTTotalPer = 0.00, DscountAmtTot = 0.00;
                                    Ttaxname = Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value);
                                    ItemcodeorItemName1(Ttaxname);
                                    if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value)))
                                    {
                                        // DscountAmtTot = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value);
                                    }

                                    double discountAmt = 0.00;
                                    double Dist1 = 0.00;
                                    Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                                    if (Dist1 > 0)
                                    {
                                        discountAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                                        DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                                    }
                                    if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                                    {
                                        discountAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                                    }

                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value = Ttaxname.ToString();
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                                    tTFinale = 0.00;
                                    tTFinale = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
                                    tTTotalPer = (tTFinale * tax_percentage / 100);
                                    tTFinale += tTTotalPer - discountAmt;
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = tTTotalPer.ToString("0.00");
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = "0.00";
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = tTFinale.ToString("0.00");
                                }
                                gridrows_calculatoin();
                            }
                        }
                        else
                        {
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == "")
                            {
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = "0.00";
                            }
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == "")
                            {
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = "0.00";
                            }
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 11)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Mrp"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Mrp"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Mrp"].Value = "0.00";
                        }
                        else
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Mrp"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Mrp"].Value).ToString("0.00");
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 12)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special1"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special1"].Value = "0.00";
                        }
                        else
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special1"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special1"].Value).ToString("0.00");
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 13)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value = "0.00";
                        }
                        else
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special2"].Value).ToString("0.00");
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 14)
                    {
                        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special3"].Value == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special3"].Value == "")
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special3"].Value = "0.00";
                        }
                        else
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special3"].Value = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Special3"].Value).ToString("0.00");
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 15 )
                    {
                        if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["TaxName"].Index)
                        {

                            if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value)))
                            {
                                Chk = "0";
                                double tTFinale = 0.00, tTTotalPer = 0.00,DscountAmtTot=0.00;
                                Ttaxname = Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value);
                                ItemcodeorItemName1(Ttaxname);
                                if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value)))
                                {
                                   // DscountAmtTot = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value);
                                }

                                double  discountAmt = 0.00;
                                double Dist1 = 0.00;
                                Dist1 = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value));
                                if (Dist1 > 0)
                                {
                                    discountAmt = (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value) * (Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value)) / 100);
                                    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value = discount.ToString("0.00");
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value)))
                                {
                                    discountAmt = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
                                }

                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxName"].Value = Ttaxname.ToString();
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString("0.00");
                                tTFinale = 0.00;
                                tTFinale = Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
                                tTTotalPer = (tTFinale * tax_percentage / 100);
                                tTFinale += tTTotalPer - discountAmt;
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = tTTotalPer.ToString("0.00");
                                DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = tTFinale.ToString("0.00");


                            }
                            gridrows_calculatoin();
                        }
                    }
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 16)
                    {
                        if (this.DgPurchase.CurrentCell.ColumnIndex == this.DgPurchase.Columns["TaxPer"].Index)
                        {
                            DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value = tax_percentage.ToString();
                        }
                    }   
                    else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 17)
                    {
                       // DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value = TaxAmtFinal.ToString("0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        //public void TaxCalculationGrid()
        //{
        //    double tTaxRateAmt=0.00,tTaxRcost=0.00,tDicountPer = 0.00,tDicountPerTot=0.00, tDiscountAmt = 0.00,tDisountAmtTot=0.00, tTaxPerAmt = 0.00,tTaxPerAmtTot=0.00, tTaxPer = 0.00,tOrginalTot=0.00,TotAmountA=0.00;

        //    tTaxRateAmt = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxRate"].Value);
        //    TotAmountA=DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Amount"].Value);
        //    tDicountPer = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Disc"].Value);
        //    tDiscountAmt = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["DiscAmt"].Value);
        //    tTaxPer = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxPer"].Value);
        //    tTaxPerAmt = DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value == null ? 0.00 : Convert.ToDouble(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TaxAmt"].Value);

        //    //TaxRate Inclusive calcualtion:And Also assign to 
        //    tTaxRcost = (tTaxRateAmt * 100 / 100 + tax_percentage);
        //    //This All Percentage calculation process:
        //    tDicountPerTot =( tDicountPer * TotAmountA / 100);
        //    tDisountAmtTot = (TotAmountA - tDiscountAmt);
        //    tTaxPerAmtTot = (TotAmountA * tax_percentage / 100);
        //    tOrginalTot = (tDicountPerTot + tDisountAmtTot + tTaxPerAmtTot);
        //    DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["TotalAmt"].Value = tOrginalTot.ToString("0.00");
        //    if (tax_percentage > 0)
        //    {
        //      //  DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Rate"].Value = tTaxRcost.ToString("0.00");
        //    }
        //    else
        //    {
  
        //    }
        //}
        double discount = 0.00;
        double TaxAmtFinal = 0.00, AmtFinal = 0.00,TotTaxAmt=0.00,tFAmount=0.00;
        private void DgPurchase_CellLeave(object sender, DataGridViewCellEventArgs e)
        { 
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            pnlHideUnhide.Visible = false;
        }
        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_select = new System.Data.DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("select * from Pur_HideUnhide_Table", con);
                dt_select.Rows.Clear();
                adp.Fill(dt_select);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (dt_select.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("update Pur_HideUnhide_Table set sno='" + _serialno + "',Code='" + _code + "',Name='" + _colum_name + "',Remarks='" + _Remarks + "',Unit='" + _Unit + "',Qty='" + _qty + "',Rate='" + _rate + "',TaxRate='" + _TaxRate + "',Amount='" + _amount + "',Disc='" + _disc + "',DiscAmount='" + _DiscAmount + "',Mrp='" + _Mrp + "',Special_1='" + _Special1 + "',Special_2='" + _Special2 + "',Special_3='" + _Special3 + "',TotalAmount='" + _total_amount + "',exp='" + _Exp + "'", con);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd_update = new SqlCommand("Update Pur_HideUnhide_Table set TaxName='" + _TaxName + "',TaxPer='" + _TaxPer + "',TaxAmt='"+_TaxAmt+"'", con);
                    cmd_update.ExecuteNonQuery();
                }
                else
                {
                    //SqlCommand cmd = new SqlCommand("insert into Pur_HideUnhide_Table(sno,Code,Name,Remarks,Unit,Qty,Rate,TaxRate,Amount,Disc,DiscAmount,Mrp,Special_1,Special_2,Special_3,TotalAmount,exp) values('" + _serialno + "','" + _code + "','" + _colum_name + "','" + _Remarks + "','" + _Unit + "','" + _qty + "','" + _rate + "','" + _TaxRate + "','" + _amount + "','" + _disc + "','" + _DiscAmount + "','" + _Mrp + "','" + _Special1 + "','" + _Special2 + "','" + _Special3 + "','" + _total_amount + "','" + _Exp + "')", con); ;
                    //here remarks unhide
                    SqlCommand cmd = new SqlCommand("insert into Pur_HideUnhide_Table(sno,Code,Name,Unit,Qty,Rate,TaxRate,Amount,Disc,DiscAmount,Mrp,Special_1,Special_2,Special_3,TotalAmount,exp) values('" + _serialno + "','" + _code + "','" + _colum_name + "','" + _Unit + "','" + _qty + "','" + _rate + "','" + _TaxRate + "','" + _amount + "','" + _disc + "','" + _DiscAmount + "','" + _Mrp + "','" + _Special1 + "','" + _Special2 + "','" + _Special3 + "','" + _total_amount + "','" + _Exp + "')", con); ;
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd_update = new SqlCommand("Update Pur_HideUnhide_Table set TaxName='" + _TaxName + "',TaxPer='" + _TaxPer + "',TaxAmt='" + _TaxAmt + "'", con);
                    cmd_update.ExecuteNonQuery();
                }
                load_check_box_values();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtSuppliers_Click(object sender, EventArgs e)
        {
            txtSuppliers.SelectAll();
        }

        private void txtSuppliers_Enter(object sender, EventArgs e)
        {
            try
            {
                txtDate.BackColor = Color.White;
                txtSuppliers.BackColor = Color.LightBlue;
                txtAddress1.BackColor = Color.White;
                txtAddress2.BackColor = Color.White;
                txtAddress3.BackColor = Color.White;
                txtAddress4.BackColor = Color.White;
                txtInvoiceNo.BackColor = Color.White;
                txtPurchaseType.BackColor = Color.White;
                txtInvalue.BackColor = Color.White;
                txtOrder_No.BackColor = Color.White;
                txtIvDate.BackColor = Color.White;
                txtCounter.BackColor = Color.White;
                txtCash.BackColor = Color.White;
                pnlpurchasetype.Visible = false;
                pnlcounter.Visible = false;
                pnllvledger.Visible = true;
                lvSuppliers.Visible = true;
                suppliers_entry();
                if (lvSuppliers.Items.Count > 0)
                {
                    if (txtSuppliers.Text.Trim() == "")
                    {
                        lvSuppliers.SetSelected(0, true);
                    }
                    else
                    {
                        if (txtSuppliers.Text.Trim() != "")
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (txtSuppliers.Text.Trim() == dt1.Rows[i]["Ledsel_name"].ToString().Trim())
                                {
                                    lvSuppliers.SelectedIndex=i;
                                    break;
                                }
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
        DataTable dt_supplier = new DataTable();
        string suppliers_number;
        private void txtSuppliers_Leave(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                //SqlDataReader dr = null;
                //    dr.Close();
                SqlCommand cmd = new SqlCommand("select * from Item_table where Item_name='" + txtSuppliers.Text + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_supplier.Rows.Clear();

                adp.Fill(dt_supplier);
                if (dt_supplier.Rows.Count > 0)
                {
                    // suppliers_number = dt.Rows[0][""].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtSuppliers_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvSuppliers.SelectedIndex < lvSuppliers.Items.Count - 1)
                    {
                        lvSuppliers.SetSelected(lvSuppliers.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvSuppliers.SelectedIndex > 0)
                    {
                        lvSuppliers.SetSelected(lvSuppliers.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (lvSuppliers.Items.Count > 0)
                    {
                        if (txtSuppliers.Text == "")
                        {
                            if (lvSuppliers.SelectedItems.Count > 0)
                            {
                                txtSuppliers.Text = lvSuppliers.SelectedItem.ToString();
                            }
                            else
                            {
                                lvSuppliers.SetSelected(0, true);
                                if (lvSuppliers.Items.Count > 0)
                                {
                                    txtSuppliers.Text = lvSuppliers.SelectedItem.ToString();
                                }
                            }

                        }
                        else if (lvSuppliers.Items.Count > 0)
                        {
                            txtSuppliers.Text = lvSuppliers.SelectedItem.ToString();
                        }
                    }
                    // txtAddress.Select();
                    pnllvledger.Visible = false;
                    txtAddress1.Focus();
                }
                if (e.Alt && e.KeyCode == Keys.A)
                {
                    //if (accetion_type == "Unit_Name")
                    {
                        frmLedgerCreation frm = new frmLedgerCreation();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtSuppliers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Tessupliers == "")
                {
                    bool isChk = false;
                    if (txtSuppliers.Text.Trim() != null && txtSuppliers.Text.Trim() != "")
                    {
                        DataTable dt_unitTable = new DataTable();
                        dt_unitTable.Rows.Clear();
                        SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "SpplierName");
                        //Here Put Suppliuer Name Means ItemName 
                        cmd.Parameters.AddWithValue("ItemName", txtSuppliers.Text);
                        cmd.Parameters.AddWithValue("ItemCode", "");
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt_unitTable);
                        if (dt_unitTable.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_unitTable.Rows[0]["Ledsel_name"].ToString();
                            for (int k = 0; k < lvSuppliers.Items.Count; k++)
                            {
                                if (tempstr == lvSuppliers.Items[k].ToString())
                                {
                                    lvSuppliers.SetSelected(k, true);
                                    txtSuppliers.Select();
                                    chk = "1";
                                    txtSuppliers.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "1";
                            if (txtSuppliers.Text != "")
                            {
                                string name = txtSuppliers.Text.Remove(txtSuppliers.Text.Length - 1);
                                txtSuppliers.Text = name.ToString();
                                txtSuppliers.Select(txtSuppliers.Text.Length, 0);
                            }
                            txtSuppliers.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        }
                        else
                        {
                            chk = "1";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        
        }
        private void txtAddress_Enter(object sender, EventArgs e)
        {
            comman_listview();
        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // txtGst.Focus();
                txtPurchaseType.Focus();
            }
        }
        public void comman_listview()
        {
            panel6.Visible = false;
            pnlpurchasetype.Visible = false;
            pnllvledger.Visible = false;
            pnlcounter.Visible = false;
           // lvCommodity.Visible = false;
            lvCounters.Visible = false;
            lvPurchase.Visible = false;
            lvSuppliers.Visible = false;
        }
        private void txtInvoiceNo_Enter(object sender, EventArgs e)
        {
            comman_listview();
            listbohide();
            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.LightBlue;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }
        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (txtInvoiceNo.Text != "")
                    {
                        txtIvDate.Focus();
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Invoice No Empty", "Warning");
                        txtInvoiceNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtGst_Enter(object sender, EventArgs e)
        {
            comman_listview();
        }
        private void txtGst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRegno.Focus();
            }
        }

        private void txtRegno_Enter(object sender, EventArgs e)
        {
            comman_listview();
        }

        private void txtRegno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPurchaseType.Focus();
            }
        }

        private void txtPurchaseType_Click(object sender, EventArgs e)
        {
            txtPurchaseType.SelectAll();
        }
        string itemname = "1";
        private void txtPurchaseType_Enter(object sender, EventArgs e)
        {
            try
            {
                itemname = "0";
                pnlcounter.Visible = false;
                pnllvledger.Visible = false;
                pnlpurchasetype.Visible = true;
                lvSuppliers.Visible = false;
                // lvCommodity.Visible = false;
                lvCounters.Visible = false;
                lvPurchase.Visible = true;
                purchaseType();

                txtDate.BackColor = Color.White;
                txtSuppliers.BackColor = Color.White;
                txtAddress1.BackColor = Color.White;
                txtAddress2.BackColor = Color.White;
                txtAddress3.BackColor = Color.White;
                txtAddress4.BackColor = Color.White;
                txtInvoiceNo.BackColor = Color.White;
                txtPurchaseType.BackColor = Color.LightBlue;
                txtInvalue.BackColor = Color.White;
                txtOrder_No.BackColor = Color.White;
                txtIvDate.BackColor = Color.White;
                txtCounter.BackColor = Color.White;
                txtCash.BackColor = Color.White;
                if (lvPurchase.Items.Count > 0)
                {
                    if (txtPurchaseType.Text.Trim() == "")
                    {
                        lvPurchase.SelectedIndex = (1);
                    }
                    else
                    {
                        if (txtPurchaseType.Text.Trim() != "")
                        {
                            for (int ik = 0; ik < dtpurchase1_.Rows.Count; ik++)
                            {
                                if (txtPurchaseType.Text.Trim() == dtpurchase1_.Rows[ik]["PurType_Name"].ToString().Trim())
                                {
                                    lvPurchase.SelectedIndex = ik;
                                }
                            }
                        }
                    }
                }
                txtPurchaseType.Focus();
                txtPurchaseType.SelectAll();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
            // txtPurchaseType.Text = dt2.Rows[0]["PurType_Name"].ToString();
        }
        string purchase_type_number;
        private void txtPurchaseType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dt_purchase_type = new DataTable();
                SqlCommand cmd = new SqlCommand("select  * from PurType_Table where PurType_Name='" + txtPurchaseType.Text + "' ", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_purchase_type.Rows.Clear();

                adp.Fill(dt_purchase_type);
                if (dt_purchase_type.Rows.Count > 0)
                {
                    if (txtPurchaseType.Text != "")
                    {
                        purchase_type_number = dt_purchase_type.Rows[0]["PurType_No"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void OnTextBoxKeyDown1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvPurchase.SelectedIndex < lvPurchase.Items.Count - 1)
                    {
                        lvPurchase.SetSelected(lvPurchase.SelectedIndex + 1, true);
                    }

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvPurchase.SelectedIndex > 0)
                    {
                        lvPurchase.SetSelected(lvPurchase.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (lvPurchase.SelectedItems.Count>0)
                    {
                        txtPurchaseType.Text = lvPurchase.SelectedItem.ToString();
                    }
                    pnlpurchasetype.Visible = false;
                    txtInvalue.Focus();
                }
                if (e.Alt && e.KeyCode == Keys.A)
                {

                    MSPOSBACKOFFICE.PurchaseTypeCreation frm = new MSPOSBACKOFFICE.PurchaseTypeCreation();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtPurchaseType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool isChk = false;
                if (txtPurchaseType.Text.Trim() != null && txtPurchaseType.Text.Trim() != "")
                {
                    DataTable dt_unitTable = new DataTable();
                    dt_unitTable.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "PurchaseLs");
                    //Here Put Purchase Name Means ItemName 
                    cmd.Parameters.AddWithValue("ItemName", txtPurchaseType.Text);
                    cmd.Parameters.AddWithValue("ItemCode", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt_unitTable);
                    if (dt_unitTable.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dt_unitTable.Rows[0]["PurType_Name"].ToString();
                        for (int k = 0; k < lvPurchase.Items.Count; k++)
                        {
                            if (tempstr == lvPurchase.Items[k].ToString())
                            {
                                lvPurchase.SetSelected(k, true);
                                txtPurchaseType.Select();
                                chk = "1";
                                txtPurchaseType.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "1";
                        if (txtSuppliers.Text != "")
                        {
                            string name = txtPurchaseType.Text.Remove(txtPurchaseType.Text.Length - 1);
                            txtPurchaseType.Text = name.ToString();
                            txtPurchaseType.Select(txtPurchaseType.Text.Length, 0);
                        }
                        txtPurchaseType.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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
        private void txtInvalue_Enter(object sender, EventArgs e)
        {
            comman_listview();
            listbohide();

            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.LightBlue;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
        }
        private void txtInvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtInvalue.Text == "")
                {
                    MyMessageBox.ShowBox("Empty Invoice no", "Warning");
                    txtCounter.Focus();
                }
                txtCounter.Focus();
            }
        }
        private void txtCounter_Enter(object sender, EventArgs e)
        {
            try
            {
                pnlcounter.Visible = true;
                pnllvledger.Visible = false;
                pnlpurchasetype.Visible = false;
                lvSuppliers.Visible = false;
                //lvCommodity.Visible = false;
                lvCounters.Visible = true;
                lvPurchase.Visible = false;
                counterType();
               // listbohide();

                txtDate.BackColor = Color.White;
                txtSuppliers.BackColor = Color.White;
                txtAddress1.BackColor = Color.White;
                txtAddress2.BackColor = Color.White;
                txtAddress3.BackColor = Color.White;
                txtAddress4.BackColor = Color.White;
                txtInvoiceNo.BackColor = Color.White;
                txtPurchaseType.BackColor = Color.White;
                txtInvalue.BackColor = Color.White;
                txtOrder_No.BackColor = Color.White;
                txtIvDate.BackColor = Color.White;
                txtCounter.BackColor = Color.LightBlue;
                txtCash.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtCounter_Click(object sender, EventArgs e)
        {
            txtCounter.SelectAll();
        }
        private void OnTextBoxKeyDown2(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Down)
                {
                    if (lvCounters.SelectedIndex < lvCounters.Items.Count - 1)
                    {
                        lvCounters.SetSelected(lvCounters.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvCounters.SelectedIndex > 0)
                    {
                        lvCounters.SetSelected(lvCounters.SelectedIndex - 1, true);
                    }
                }

                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (lvCounters.SelectedItems.Count > 0)
                    {
                        txtCounter.Text = lvCounters.SelectedItem.ToString();
                        
                    }
                    pnlcounter.Visible = false;
                    txtCash.Focus();
                    //  txtInvalue.Focus();
                }
                if (e.Alt && e.KeyCode == Keys.A)
                {

                    MSPOSBACKOFFICE.CounterCreation frm = new MSPOSBACKOFFICE.CounterCreation();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string Counter_number;
        private void txtCounter_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCounter.Text != "")
                {
                    con.Close();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dt_counter_number = new System.Data.DataTable();
                    SqlCommand cmd = new SqlCommand("select * from counter_table where ctr_name='" + txtCounter.Text + "' ", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt_counter_number.Rows.Clear();
                    dt_counter_number.Rows.Clear();
                    adp.Fill(dt);
                    if (dt_counter_number.Rows.Count > 0)
                    {
                        // lvSuppliers.Text = "(Demo) List Models";
                        for (int i = 0; i < dt_counter_number.Rows.Count; i++)
                        {
                            Counter_number = (dt_counter_number.Rows[i]["ctr_no"].ToString());
                        }
                    }
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
                if (txtPurchaseType.Text.Trim() != null && txtPurchaseType.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    bool isChk = false;
                    DataTable dt_unitTable = new DataTable();
                    dt_unitTable.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "CounterType");
                    //Here Put Counter Name Means ItemName 
                    cmd.Parameters.AddWithValue("ItemName", txtCounter.Text);
                    cmd.Parameters.AddWithValue("ItemCode", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt_unitTable);
                    if (dt_unitTable.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dt_unitTable.Rows[0]["ctr_name"].ToString();
                        for (int k = 0; k < lvCounters.Items.Count; k++)
                        {
                            if (tempstr == lvCounters.Items[k].ToString())
                            {
                                lvCounters.SetSelected(k, true);
                                txtCounter.Select();
                                chk = "1";
                                txtCounter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "1";
                        if (txtSuppliers.Text != "")
                        {
                            string name = txtCounter.Text.Remove(txtCounter.Text.Length - 1);
                            txtCounter.Text = name.ToString();
                            txtCounter.Select(txtSuppliers.Text.Length, 0);
                        }
                        txtCounter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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

        private void txtCash_Click(object sender, EventArgs e)
        {
            
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            comman_listview();
            if (lvCounters.Items.Count > 0)
            {
                lvCounters.SetSelected(0, true);
            }
            listbohide();

            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.LightBlue;
            listbohide();
        }

        private void txtCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrder_No.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (txtCash.Text == "Cash")
                {
                    txtCash.Text = "Credit";
                }
                else
                {
                    txtCash.Text = "Cash";
                }
            }
        }
        private void txtOrder_No_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int count = Convert.ToInt32(DgPurchase.Rows.Count);
                    if (count == 0)
                    {
                        //  DgPurchase.Rows.Add();
                        DgPurchase.Select();
                    }
                    else
                    {
                        int iRow = DgPurchase.CurrentCell.RowIndex;
                        DgPurchase.CurrentCell = DgPurchase.Rows[iRow].Cells["ItemCode"];
                        DgPurchase.Focus();
                    }
                    txtDate.BackColor = Color.White;
                    txtSuppliers.BackColor = Color.White;
                    txtAddress1.BackColor = Color.White;
                    txtAddress2.BackColor = Color.White;
                    txtAddress3.BackColor = Color.White;
                    txtAddress4.BackColor = Color.White;
                    txtInvoiceNo.BackColor = Color.White;
                    txtPurchaseType.BackColor = Color.White;
                    txtInvalue.BackColor = Color.White;
                    txtOrder_No.BackColor = Color.White;
                    txtIvDate.BackColor = Color.White;
                    txtCounter.BackColor = Color.White;
                    txtCash.BackColor = Color.White;
                    listbohide();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (DgPurchase.Rows.Count > 0)
                {
                    for (int i = 0; i < DgPurchase.Rows.Count; i++)
                    {
                        DgPurchase.Rows.Clear();
                        // DgPurchase.Rows.RemoveAt(i);
                        dt2.Rows.Clear();
                    }
                    clear();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void clear()
        {
            try
            {
                txtSuppliers.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
                txtGst.Text = string.Empty;
                txtRegno.Text = string.Empty;
                txtPurchaseType.Text = string.Empty;
                txtInvalue.Text = string.Empty;
                txtCounter.Text = string.Empty;
                txtCash.Text = string.Empty;
                txtOrder_No.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtAddress3.Text = string.Empty;
                txtAddress4.Text = string.Empty;

                Pnl_Back.Visible = false;
                pnDiscountPanel.Visible = false;

                lblItems.Text = "0";
                lbl_Qty.Text = "0";
                dt2.Rows.Clear();
                int km = 0;
                km = Convert.ToInt32(DgPurchase.Rows.Count);
                if (DgPurchase.Rows.Count > 0)
                {
                    for (int i = 0; i < DgPurchase.Rows.Count; i++)
                    {
                        DgPurchase.Rows.Clear();
                        dt2.Rows.Clear();
                        dt_pass_values.Clear();
                    }
                    // clear();
                }
                lblAmount.Text = "0";
                lbl_netAmount.Text = "0";
                txtPurchaseType.Text = "";
                txtSuppliers.Text = "";
                autonumner();
                autonumner1();
                auto_number_gen();
                pnlHideUnhide.Visible = false;
                DgPurchase.Columns[2].Width = 400;
                DgPurchase.Columns[0].Width = 40;
                DgPurchase.Columns[4].Width = 60;
                for (int i = 0; i < 45; i++)
                {
                    DgPurchase.Rows.Add();
                }
                DgDiscount.Rows.Clear();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string exit_close = "";
        private void button1_Click(object sender, EventArgs e)
        {
            exit_close = "1";
            this.Close();
            exit_close = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                save_clik_enter();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
       // string counternumber;
       // string itemvalues;
       // string unig_nametablevalues;
        string chck_values = "false";
        public void save_clik_enter()
        {
            try
            {
                if (txtInvoiceNo.Text != "")
                {
                    if (txtInvalue.Text != "")
                    {
                        if (txtSuppliers.Text.Trim() != "")
                        {
                            if (txtPurchaseType.Text.Trim() != "")
                            {
                                if (double.Parse(txtInvalue.Text) == double.Parse(lbl_netAmount.Text))
                                {
                                    double amounts = 0.00;
                                    amounts = (Convert.ToDouble(txtInvalue.Text));
                                    txtInvalue.Text = amounts.ToString("0.00");
                                    double amount12 = 0.00;
                                    amount12 = Convert.ToDouble(lbl_netAmount.Text);
                                    lbl_netAmount.Text = amount12.ToString("0.00");
                                    txtInvalue.Text = amount12.ToString("0.00");
                                    //string result = "";
                                    if (dt_pass_values.Rows.Count > 0)
                                    {
                                        SqlCommand cmd = new SqlCommand("SP_PurchaseAlter", con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Counter", txtCounter.Text);
                                        cmd.Parameters.AddWithValue("@Cash", txtCash.Text);
                                        cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                                        cmd.Parameters.AddWithValue("@OrderNumber", txtOrder_No.Text);
                                        cmd.Parameters.AddWithValue("@SupplierName", txtSuppliers.Text);
                                        cmd.Parameters.AddWithValue("@CurrentDate", DtpPurchaseDate.Value);
                                        cmd.Parameters.AddWithValue("@Gst", txtGst.Text);
                                        cmd.Parameters.AddWithValue("@RegistationNo", txtRegno.Text);
                                        cmd.Parameters.AddWithValue("@GrossAmount", lbl_netAmount.Text);
                                        cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
                                        cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
                                        cmd.Parameters.AddWithValue("@Address3", txtAddress3.Text);
                                        cmd.Parameters.AddWithValue("@Address4", txtAddress4.Text);
                                        cmd.Parameters.AddWithValue("@Dates", DtpPurchaseDate.Value);
                                        cmd.Parameters.AddWithValue("@InvoiceDate", DtpInvoiceDate.Value);
                                        cmd.Parameters.AddWithValue("@VoucherNo", id);
                                        
                                        DataTable DataTable1 = new DataTable();
                                        dt_gridload1.Rows.Clear();
                                        dtGralter.Rows.Clear();
                                        DataTable1.Rows.Clear();
                                        for (int i = 0; i < DgPurchase.Rows.Count - 1; i++)
                                        {
                                            if (DgPurchase.Rows[i].Cells["ItemNames"].Value != "" && DgPurchase.Rows[i].Cells["ItemNames"].Value != null)
                                            {
                                                //string itemnamevalues = "";
                                                //itemnamevalues = DgPurchase.Rows[i].Cells["ItemNames"].Value.ToString();
                                                //selectchkmethods(itemnamevalues);
                                                //if (selectcount != "0")
                                                {
                                                    dtGralter.Rows.Add(DgPurchase.Rows[i].Cells["ItemCode"].Value, DgPurchase.Rows[i].Cells["ItemNames"].Value, DgPurchase.Rows[i].Cells["Remarks"].Value, DgPurchase.Rows[i].Cells["Unit"].Value, DgPurchase.Rows[i].Cells["Qt"].Value, DgPurchase.Rows[i].Cells["Rate"].Value, DgPurchase.Rows[i].Cells["TaxRate"].Value, DgPurchase.Rows[i].Cells["Amount"].Value, DgPurchase.Rows[i].Cells["Disc"].Value, DgPurchase.Rows[i].Cells["DiscAmt"].Value, DgPurchase.Rows[i].Cells["Mrp"].Value, DgPurchase.Rows[i].Cells["Special1"].Value, DgPurchase.Rows[i].Cells["Special2"].Value, DgPurchase.Rows[i].Cells["Special3"].Value, DgPurchase.Rows[i].Cells["TotalAmt"].Value, DgPurchase.Rows[i].Cells["exp"].Value, DgPurchase.Rows[i].Cells["Strn_no"].Value, DgPurchase.Rows[i].Cells["Strn_sno"].Value, DgPurchase.Rows[i].Cells["TaxName"].Value, DgPurchase.Rows[i].Cells["TaxPer"].Value, DgPurchase.Rows[i].Cells["TaxAmt"].Value);
                                                }
                                            }
                                        }
                                        DataTable1 = dtGralter.Clone();
                                        foreach (DataRow drtableOld in dtGralter.Rows)
                                        {
                                            DataTable1.ImportRow(drtableOld);
                                        }
                                        for (int im = 0; im < dt_pass_values.Rows.Count; im++)
                                        {
                                            dt_gridload1.Rows.Add(dt_pass_values.Rows[im]["strn_sno"].ToString(), dt_pass_values.Rows[im]["strn_no"].ToString(), dt_pass_values.Rows[im]["item_no"].ToString(), dt_pass_values.Rows[im]["nt_qty"].ToString(), dt_pass_values.Rows[im]["tot_amt"].ToString());
                                        }
                                        cmd.Parameters.AddWithValue("@dt_gridload", dt_gridload1);
                                        cmd.Parameters.AddWithValue("@gload1", DataTable1);

                                        SqlParameter retu1 = new SqlParameter("@ReturnResult1", SqlDbType.VarChar, 50);
                                        retu1.Direction = ParameterDirection.Output;
                                        cmd.Parameters.Add(retu1);
                                        if (con.State != ConnectionState.Open)
                                        {
                                            con.Open();
                                        }

                                        //Discount Table values:
                                        string typne_names = "0";
                                        dtDicounttable.Rows.Clear();
                                        for (int i = 0; i < DgDiscount.Rows.Count - 1; i++)
                                        {
                                            if ((!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[i].Cells["Details"].Value))))
                                            {
                                                if ((!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[i].Cells["DiscountAmount"].Value))))
                                                {
                                                    if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[i].Cells["Type"].Value)))
                                                    {
                                                        if (DgDiscount.Rows[i].Cells["Type"].Value.ToString().Trim() == "Tax")
                                                        {
                                                            typne_names = "1";
                                                        }
                                                        else if (DgDiscount.Rows[i].Cells["Type"].Value.ToString().Trim() == "Discount" && DgDiscount.Rows[i].Cells["Type"].Value != null)
                                                        {
                                                            typne_names = "2";
                                                        }
                                                        else if (DgDiscount.Rows[i].Cells["Type"].Value.ToString().Trim() == "Additions" && DgDiscount.Rows[i].Cells["Type"].Value != null)
                                                        {
                                                            typne_names = "3";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        typne_names = "0";
                                                    }
                                                    double tpercent = 0.00;
                                                    tpercent = DgDiscount.Rows[i].Cells["Percent_Pr"].Value == "" ? 0.00 : Convert.ToDouble(DgDiscount.Rows[i].Cells["Percent_Pr"].Value);
                                                    double tdicountqt = 0.00;
                                                    string Tdetails = "";
                                                    Tdetails = DgDiscount.Rows[i].Cells["Details"].Value == "" ? "" : Convert.ToString(DgDiscount.Rows[i].Cells["Details"].Value);
                                                    tdicountqt = DgDiscount.Rows[i].Cells["DisPerQty_Pr"].Value == "" ? 0.00 : Convert.ToDouble(DgDiscount.Rows[i].Cells["DisPerQty_Pr"].Value);
                                                    dtDicounttable.Rows.Add(typne_names, Tdetails.ToString(), tdicountqt, tpercent, DgDiscount.Rows[i].Cells["DiscountAmount"].Value);
                                                }
                                            }
                                        }
                                        
                                        cmd.Parameters.AddWithValue("@DgDiscount_Table", dtDicounttable);
                                        cmd.ExecuteNonQuery();

                                        dt_pass_values.Rows.Clear();
                                        DataTable1.Rows.Clear();
                                        clear();
                                        this.Close();
                                    }
                                    else
                                    {
                                        string cash_number;
                                        //values getting to textbox:
                                        if (txtCash.Text == "Cash")
                                        {
                                            cash_number = "5";
                                        }
                                        else
                                        {
                                            cash_number = "8";
                                        }
                                        SqlCommand cmd = new SqlCommand("SP_PurchaseEntry", con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@SupplierName", txtSuppliers.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Count_values", txtCounter.Text.Trim());
                                        cmd.Parameters.AddWithValue("@BillNo", lblBillNo.Text.Trim());
                                        cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Dates", DtpPurchaseDate.Value);
                                        cmd.Parameters.AddWithValue("@InvoiceDate", DtpInvoiceDate.Value);
                                        cmd.Parameters.AddWithValue("@PurchaseType", txtPurchaseType.Text.Trim());
                                        cmd.Parameters.AddWithValue("@OrderNo", txtOrder_No.Text.Trim());
                                        cmd.Parameters.AddWithValue("@CashNumber", cash_number.Trim());
                                        cmd.Parameters.AddWithValue("@Total", lblAmount.Text.Trim());
                                        cmd.Parameters.AddWithValue("@NetAmount", lbl_netAmount.Text.Trim());
                                        cmd.Parameters.AddWithValue("@InvoiceAmount", txtInvalue.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text.Trim() == "" ? "" : txtAddress1.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text.Trim() == "" ? "" : txtAddress2.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Address3", txtAddress3.Text.Trim() == "" ? "" : txtAddress3.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Address4", txtAddress4.Text.Trim() == "" ? "" : txtAddress4.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tTotalQty", lbl_Qty.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Additions", lblDiscountAdditions.Text.Trim() == "" ? "0.00" : lblDiscountAdditions.Text.Trim());
                                        cmd.Parameters.AddWithValue("@tDiscountAmt", lblDiscountDis.Text.Trim() == "" ? "0.00" : lblDiscountDis.Text.Trim());
                                        dt_gridload.Rows.Clear();
                                        string TaxRate="";
                                        for (int i = 0; i < DgPurchase.Rows.Count - 1; i++)
                                        {
                                            if (DgPurchase.Rows[i].Cells["ItemNames"].Value != null && DgPurchase.Rows[i].Cells["ItemNames"].Value != "" && DgPurchase.Rows[i].Cells["Qt"].Value != "" && DgPurchase.Rows[i].Cells["Qt"].Value != null)
                                            {
                                                TaxRate="0";
                                                if( DgPurchase.Rows[i].Cells["TaxRate"].Value!=null &&  DgPurchase.Rows[i].Cells["TaxRate"].Value.ToString()!="")
                                                {
                                                    TaxRate=DgPurchase.Rows[i].Cells["TaxRate"].Value==null || DgPurchase.Rows[i].Cells["TaxRate"].Value.ToString().Trim()==string.Empty?"0":Convert.ToString(DgPurchase.Rows[i].Cells["TaxRate"].Value.ToString().Trim()); 
                                                }
                                                dt_gridload.Rows.Add(DgPurchase.Rows[i].Cells["ItemCode"].Value.ToString(), DgPurchase.Rows[i].Cells["ItemNames"].Value.ToString(), DgPurchase.Rows[i].Cells["Qt"].Value, DgPurchase.Rows[i].Cells["Rate"].Value, DgPurchase.Rows[i].Cells["Amount"].Value, DgPurchase.Rows[i].Cells["Disc"].Value, DgPurchase.Rows[i].Cells["DiscAmt"].Value, DgPurchase.Rows[i].Cells["Mrp"].Value, DgPurchase.Rows[i].Cells["TotalAmt"].Value, DtpPurchaseDate.Value, txtCounter.Text, TaxRate, DgPurchase.Rows[i].Cells["TaxName"].Value, DgPurchase.Rows[i].Cells["TaxPer"].Value, DgPurchase.Rows[i].Cells["TaxAmt"].Value,DgPurchase.Rows[i].Cells["Special1"].Value, DgPurchase.Rows[i].Cells["Special2"].Value, DgPurchase.Rows[i].Cells["Special3"].Value);
                                                chck_values = "true";
                                            }
                                        }
                                        if (dt_gridload.Rows.Count > 0)
                                        {
                                            cmd.Parameters.AddWithValue("@dt_gridload", dt_gridload);
                                        }
                                        //Discount Table values:
                                        string typne_names = "0";
                                        dtDicounttable.Rows.Clear();
                                        for (int i = 0; i < DgDiscount.Rows.Count - 1; i++)
                                        {
                                            if ((!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[i].Cells["Details"].Value))))
                                            {
                                                if ((!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[i].Cells["DiscountAmount"].Value))) )
                                                {
                                                    if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[i].Cells["Type"].Value)))
                                                    {
                                                        if (DgDiscount.Rows[i].Cells["Type"].Value.ToString().Trim() == "Tax")
                                                        {
                                                            typne_names = "1";
                                                        }
                                                        else if (DgDiscount.Rows[i].Cells["Type"].Value.ToString().Trim() == "Discount" && DgDiscount.Rows[i].Cells["Type"].Value != null)
                                                        {
                                                            typne_names = "2";
                                                        }
                                                        else if (DgDiscount.Rows[i].Cells["Type"].Value.ToString().Trim() == "Additions" && DgDiscount.Rows[i].Cells["Type"].Value != null)
                                                        {
                                                            typne_names = "3";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        typne_names = "0";
                                                    }
                                                double tpercent = 0.00;
                                                tpercent =DgDiscount.Rows[i].Cells["Percent_Pr"].Value == "" ? 0.00 : Convert.ToDouble(DgDiscount.Rows[i].Cells["Percent_Pr"].Value);
                                                double tdicountqt = 0.00;
                                                string Tdetails = "";
                                                Tdetails = DgDiscount.Rows[i].Cells["Details"].Value == "" ? "" : Convert.ToString(DgDiscount.Rows[i].Cells["Details"].Value);
                                                tdicountqt = DgDiscount.Rows[i].Cells["DisPerQty_Pr"].Value == "" ? 0.00 : Convert.ToDouble(DgDiscount.Rows[i].Cells["DisPerQty_Pr"].Value);
                                                dtDicounttable.Rows.Add(typne_names,Tdetails.ToString(), tdicountqt, tpercent, DgDiscount.Rows[i].Cells["DiscountAmount"].Value);
                                                }
                                            }
                                        }
                                        if (dtDicounttable.Rows.Count > 0)
                                        {
                                            cmd.Parameters.AddWithValue("@DgDiscount_Table", dtDicounttable);
                                        }
                                        SqlParameter retu1 = new SqlParameter("@Return", SqlDbType.VarChar, 50);
                                        retu1.Direction = ParameterDirection.Output;
                                        cmd.Parameters.Add(retu1);
                                        if (con.State != ConnectionState.Open)
                                        {
                                            con.Open();
                                        }
                                        cmd.ExecuteNonQuery();
                                        if (chck_values == "true")
                                        {
                                            MyMessageBox.ShowBox("Added Successfully");
                                            //btnSave.BackColor = Color.Transparent;
                                            lbl_netAmount.Text = "";
                                            lblAmount.Text = "";
                                            clear();
                                        }
                                        dt_pass_values.Rows.Clear();
                                        clear();
                                    }
                                }
                                else
                                {
                                    MyMessageBox.ShowBox("InCorrect invoice value", "Warning");
                                    txtInvalue.Focus();
                                }
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Please Enter Purchase Type", "Warning");
                                txtPurchaseType.Focus();
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Please Enter Supplier Name", "Warning");
                            txtSuppliers.Focus();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Empty  Invoice Value", "Warning");
                        txtInvalue.Focus();
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Empty  Bill No", "Warning");
                    txtInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
       string selectcount="0";
        public void selectchkmethods(string itemnamevalues)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ItemName");
                cmd.Parameters.AddWithValue("@itemName", itemnamevalues);
                cmd.Parameters.AddWithValue("@Itemcode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtapdfill = new DataTable();
                dtapdfill.Rows.Clear();
                adp.Fill(dtapdfill);
                if (dtapdfill.Rows.Count > 0)
                {
                    selectcount = "1";
                }
                else
                {
                    selectcount = "0";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void gridrows_calculatoin()
        {
            try
            {
                double gross_amount1 = 0.00;
                double qty = 0;
                int increment = 0, values = 0;
                for (int gr_cal = 0; gr_cal < DgPurchase.Rows.Count; gr_cal++)
                {
                    gross_amount1 += Convert.ToDouble(DgPurchase.Rows[gr_cal].Cells["TotalAmt"].Value);
                    if (DgPurchase.Rows[gr_cal].Cells["Qt"].Value != null)
                    {
                        qty += Convert.ToDouble(DgPurchase.Rows[gr_cal].Cells["Qt"].Value);
                        values = ++increment;
                    }
                    lblItems.Text = values.ToString();
                    lbl_Qty.Text = qty.ToString();
                }
                lblAmount.Text = gross_amount1.ToString("0.00");
                lbl_netAmount.Text = (Convert.ToDouble(lblDiscountNetAmt.Text) + gross_amount1).ToString("0.00");
                txtInvalue.Text = (Convert.ToDouble(lblDiscountNetAmt.Text) + gross_amount1).ToString("0.00");
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void DgPurchase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                try
                {
                    if (exit_close != "1")
                    {
                         if (DgPurchase.CurrentCell.ColumnIndex == 2 && DgPurchase.CurrentRow != null)
                        {
                            //string itemnames = Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value).ToString();
                            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
                            {
                                string t1 = DgPurchase.Rows[DgPurchase.CurrentRow.Index].Cells["ItemNames"].Value.ToString();
                                int t2 = DgPurchase.CurrentRow.Index;

                                for (int j = 0; j < DgPurchase.Rows.Count; j++)
                                {
                                    if (t2 != j)
                                    {
                                        if (DgPurchase.Rows[j].Cells["ItemNames"].Value != null)
                                        {
                                            if (t1.ToLower() == DgPurchase.Rows[j].Cells["ItemNames"].Value.ToString().ToLower())
                                            {
                                                MyMessageBox1.ShowBox("Item is already Entered");
                                                int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex);
                                                SetColumnIndex method = new SetColumnIndex(Mymethod);
                                                this.DgPurchase.BeginInvoke(method, 5);

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error");
                }   
        }
        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
        }
        private void DtpInvoiceDate_DropDown(object sender, EventArgs e)
        {  
        }
        private void DtpPurchaseDate_ValueChanged(object sender, EventArgs e)
        {
            txtDate.Text = DtpPurchaseDate.Text;
            txtSuppliers.Focus();
        }

        private void DtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            txtIvDate.Text = DtpInvoiceDate.Text;
            txtSuppliers.Focus();
        }

        private void lvSuppliers_Click(object sender, EventArgs e)
        {
            if (lvSuppliers.SelectedItems.Count>0)
            {
                txtSuppliers.Text = lvSuppliers.SelectedItem.ToString();
                pnllvledger.Visible = false;
               
            }
        }
        private void lvPurchase_Click(object sender, EventArgs e)
        {
            if (lvPurchase.SelectedItems.Count>0)
            {
                txtPurchaseType.Text = lvPurchase.SelectedItem.ToString();
                pnlpurchasetype.Visible = false;
            }
        }
        private void lvCounters_Click(object sender, EventArgs e)
        {
            if (lvCounters.SelectedItems.Count > 0)
            {
                txtCounter.Text = lvCounters.SelectedItem.ToString();
            }
        }
        private void Chk_colHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void txtCash_DoubleClick(object sender, EventArgs e)
        {
            if (txtCash.Text != "Cash")
            {
                txtCash.Text = "Cash";
            }
            else
            {
                txtCash.Text = "Credit";
            }
        }

        private void DgPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    try
            //    {
            //        if (DgPurchase.CurrentCell.ColumnIndex == 2 && DgPurchase.CurrentRow != null)
            //        {
            //            //string itemnames = Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value).ToString();
            //            if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
            //            {
            //                string t1 = DgPurchase.Rows[DgPurchase.CurrentRow.Index].Cells["ItemNames"].Value.ToString();
            //                int t2 = DgPurchase.CurrentRow.Index;

            //                for (int j = 0; j < DgPurchase.Rows.Count; j++)
            //                {
            //                    if (t2 != j)
            //                    {
            //                        if (DgPurchase.Rows[j].Cells["ItemNames"].Value != null)
            //                        {
            //                            if (t1.ToLower() == DgPurchase.Rows[j].Cells["ItemNames"].Value.ToString().ToLower())
            //                            {
            //                                MyMessageBox1.ShowBox("Item is already Entered");

            //                                //   MessageBox.Show("selected item is already entered", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //                                //  lstItemname.SelectedIndex = -1;
            //                                int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex + 1);
            //                                SetColumnIndex method = new SetColumnIndex(Mymethod);
            //                                this.DgPurchase.BeginInvoke(method, nextindex + 1);

            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString(), "Error");
            //    }
            //}
        }

        private void DgPurchase_KeyPress(object sender, KeyPressEventArgs e)
        {   
        }
        private void DgPurchase_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
             //if (DgPurchase.CurrentCell.ColumnIndex == 6)
             //{
             //    if (DgPurchase.Rows[DgPurchase.CurrentRow.Index].Cells["ItemNames"].Value != null)
             //    {
             //        //string itemnames = Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value).ToString();
             //        if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value.ToString().Trim() == null || DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["Qt"].Value.ToString().Trim() == "0")
             //        {
             //            MyMessageBox1.ShowBox("Please Enter Qty Rate", "Warning");
             //            int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex);
             //            SetColumnIndex method = new SetColumnIndex(Mymethod);
             //            this.DgPurchase.BeginInvoke(method, 5);
             //        }
             //    }
             //}
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAddress2.Focus();
            }
        }
        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                 txtAddress4.Focus();
            }
        }
        private void txtAddress4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPurchaseType.Focus();
            }
        }
        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAddress3.Focus();
            }
        }
        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
        }
        private void txtAddress1_Enter(object sender, EventArgs e)
        {
            pnllvledger.Visible = false;

            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.LightBlue;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }

        private void txtIvDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtSuppliers.Focus();
            }
        }
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab)
            {
                txtSuppliers.Focus();
            }
        }
        private void txtDate_Enter(object sender, EventArgs e)
        {
            txtDate.BackColor = Color.LightBlue;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }
        public void listbohide()
        {
            pnlpurchasetype.Visible = false;
            pnlcounter.Visible = false;
            pnllvledger.Visible = false;
        }
        private void txtAddress2_Enter(object sender, EventArgs e)
        {
            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.LightBlue;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }
        private void txtAddress3_Enter(object sender, EventArgs e)
        {
            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.LightBlue;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }
        private void txtAddress4_Enter(object sender, EventArgs e)
        {
            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.LightBlue;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }
        private void txtOrder_No_Enter(object sender, EventArgs e)
        {
            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.LightBlue;
            txtIvDate.BackColor = Color.White;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }
        private void txtIvDate_Enter(object sender, EventArgs e)
        {
            txtDate.BackColor = Color.White;
            txtSuppliers.BackColor = Color.White;
            txtAddress1.BackColor = Color.White;
            txtAddress2.BackColor = Color.White;
            txtAddress3.BackColor = Color.White;
            txtAddress4.BackColor = Color.White;
            txtInvoiceNo.BackColor = Color.White;
            txtPurchaseType.BackColor = Color.White;
            txtInvalue.BackColor = Color.White;
            txtOrder_No.BackColor = Color.White;
            txtIvDate.BackColor = Color.LightBlue;
            txtCounter.BackColor = Color.White;
            txtCash.BackColor = Color.White;
            listbohide();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (Pnl_Back.Visible == true)
            {
                Pnl_Back.Visible = false;

                lblDiscountAmt .Text= Convert.ToDouble(lblAmount.Text).ToString("0.00");
                pnDiscountPanel.Visible = false;
                
            }
            else
            {
                lblDiscountAmt.Text = Convert.ToDouble(lblAmount.Text).ToString("0.00");
                Pnl_Back.Visible = true;
                pnDiscountPanel.Visible = true;
                
            }
        }
        //DgDiscount Entry Form Validation and entrys:

        private void DgDiscount_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
          //  string Columnsname = Convert.ToString(DgPurchase.Name.ToString());
            if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["Type"].Index)
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    DataCollection.Add("Additions");
                    DataCollection.Add("Discount");
                    DataCollection.Add("Tax");
                   // DgaddItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
            }
            if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["Details"].Index) //Item_name
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                te.AutoCompleteSource = AutoCompleteSource.None;
            }
            if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["DisPerQty_Pr"].Index) //Item_name
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                te.AutoCompleteSource = AutoCompleteSource.None;
            }

            if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["Percent_Pr"].Index)
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                te.AutoCompleteSource = AutoCompleteSource.None;
            }
            if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["DiscountAmount"].Index)
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                te.AutoCompleteSource = AutoCompleteSource.None;
            }

          
            if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["Details"].Index) //Item_name
            {
                
              //  con.Close();
              //  con.Open();
              //  SqlCommand namecmd = new SqlCommand("select Ledsel_name from Ledsel_table order by Ledsel_name ASC", con);
              //  //Dgautofind.Rows.Clear();
              //  DataTable autofind = new DataTable();
              //  autofind.Rows.Clear();
              //  SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
              //  nameadp.Fill(autofind);
              //  con.Close();
              // // string[] postSource = null;
              ////  postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x1 => x1.Field<String>("Ledsel_name")).ToArray();

              //  TextBox te = e.Control as TextBox;
              //  //te.Text = null;
              //  AutoCompleteStringCollection col =new AutoCompleteStringCollection();
              //  //te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
              //  ////te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
              //  //te.AutoCompleteCustomSource.AddRange(postSource);
              //  //te.AutoCompleteSource = AutoCompleteSource.CustomSource;
              //  for (int i = 0; i < autofind.Rows.Count; i++)
              //  {
              //      col.Add(autofind.Rows[i]["Ledsel_name"].ToString());
              //  }
              //  te.AutoCompleteCustomSource = col;



                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems1(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }  
            }
        }
        public void addItems1(AutoCompleteStringCollection col)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand namecmd1 = new SqlCommand("select Ledsel_name from Ledsel_table order by Ledsel_name ASC", con);
            DataTable autofind1 = new DataTable();
            autofind1.Rows.Clear();
            SqlDataAdapter nameadp1 = new SqlDataAdapter(namecmd1);
            nameadp1.Fill(autofind1);
            for (int i = 0; i < autofind1.Rows.Count; i++)
            {
                col.Add(autofind1.Rows[i]["Ledsel_name"].ToString());
            }
        }
        //public void DgaddItems(AutoCompleteStringCollection col)
        //{
        //    DataTable Dgautofind = new DataTable();
        //    Dgautofind.Rows.Clear();
        //    Dgautofind.Columns.Add("Title");
        //    Dgautofind.Rows.Add("Additions");
        //    Dgautofind.Rows.Add("Discount");
        //    Dgautofind.Rows.Add("Tax");
        //    for (int i = 0; i < Dgautofind.Rows.Count; i++)
        //    {
        //        col.Add(Dgautofind.Rows[i]["Title"].ToString());
        //    }
        //}
        double DPerqty = 0.00, DgPurchaseNetAmount = 0.00, TDisountPertot = 0.00, DPerqty2=0.00;
        private void DgDiscount_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dggridvalues != "1")
            {
                if (DgDiscount.CurrentRow != null && e.ColumnIndex == 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[0].Value)))
                    {
                        DgDiscountTpe = Convert.ToString(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[0].Value);
                    }

                    if (DgDiscountTpe != string.Empty)
                    {
                        for (int k1 = 0; k1 < DgDiscount.Rows.Count - 1; k1++)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[k1].Cells[0].Value)))
                            {
                                if (DgDiscountTpe != DgDiscount.Rows[k1].Cells[0].Value.ToString() && DgDiscount.Rows[k1].Cells[0].Value.ToString() != "Additions")
                                {
                                    if (DgDiscountTpe != "Additions")
                                    {
                                        MyMessageBox1.ShowBox(DgDiscountTpe + " Should Be Enter Before " + Convert.ToString(DgDiscount.Rows[k1].Cells[0].Value + "_Additions"), "Warning");
                                        int nextindex = Math.Min(this.DgDiscount.Columns.Count - 1, this.DgDiscount.CurrentCell.ColumnIndex);
                                        SetColumnIndex method = new SetColumnIndex(Mymethod1);
                                        this.DgDiscount.BeginInvoke(method, 0);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                else if (DgDiscount.CurrentRow != null && e.ColumnIndex == 1)
                {


                }
                else if (DgDiscount.CurrentRow != null && e.ColumnIndex == 2)
                {
                    if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns["DisPerQty_Pr"].Index)
                    //   if(!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[DgDiscount.CurrentCell.ColumnIndex].Cells["PerQty"].Value)))
                    {
                        DPerqty2 = 0.00;
                        double DPerqtt = 0.00;
                        if (DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value != null)
                        {

                            DPerqtt = Convert.ToDouble(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value);
                            DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value = DPerqtt.ToString("0.00");
                            DPerqty2 = (DPerqtt * Convert.ToDouble(lbl_Qty.Text));
                            //DgDiscount.Rows[DgDiscount.CurrentCell.ColumnIndex].Cells[0].Value = "0.00";
                            DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells["Percent_Pr"].Value = null;

                            DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells["DiscountAmount"].Value = DPerqty2.ToString("0.00");
                            DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value = "";
                            tDiscountGridCalculation();
                        }
                    }

                }

                else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 3)
                {
                    if (DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value != null && Convert.ToString(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value) != "")
                    {

                        if (DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value == null || DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value == "")
                        {

                        }
                        else
                        {
                            DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[4].Value = 0.00;
                            tDiscountGridCalculation();
                            if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value)))
                            {
                                if (Convert.ToDouble(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value) > 0)
                                {
                                    DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value = string.Empty;
                                    DPerqty = DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value == null ? 0.00 : Convert.ToDouble(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[2].Value);
                                    DPerqty = (DPerqty * Convert.ToDouble(lbl_Qty.Text));
                                    DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[4].Value = DPerqty.ToString("0.00");
                                    tDiscountGridCalculation();
                                }
                            }
                            else
                            {
                                double TDisountPertot2 = 0.00;
                                TDisountPertot = 0.00;
                                //TDiscountPerAmount = 0.00;
                                DgPurchaseNetAmount = 0.00;
                                TDisountPertot2 = DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value == null || DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value == "" ? 0.00 : Convert.ToDouble(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value);
                                DgPurchaseNetAmount = Convert.ToDouble(lblAmount.Text);
                                DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[3].Value = TDisountPertot2.ToString("0.00");
                                TDisountPertot2 = (((TDiscountPerAmount + DgPurchaseNetAmount) * TDisountPertot2) / 100);
                                DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells["DiscountAmount"].Value = TDisountPertot2.ToString("0.00");
                                tDiscountGridCalculation();
                            }
                        }
                    }
                }
                else if (DgPurchase.CurrentRow != null && e.ColumnIndex == 4)
                { }
            }
        }
        double tDTax = 0.00, tDDiscount = 0.00, tDiscountAdditions = 0.00, TDiscountPerAmount = 0.00;
        public void tDiscountGridCalculation()
        {
            tDTax = 0.00; tDDiscount = 0.00; tDiscountAdditions = 0.00; TDiscountPerAmount = 0.00;
            lblDiscountAmt.Text = lblAmount.Text;
            DPerqty = 0.00;
          
            for (int j = 0; j <= DgDiscount.Rows.Count - 1; j++)
            {
                DPerqty = DgDiscount.Rows[j].Cells[2].Value == null ? 0.00 : Convert.ToDouble(DgDiscount.Rows[j].Cells[2].Value);
                DPerqty = (DPerqty * Convert.ToDouble(lbl_Qty.Text));
                if (Convert.ToString(DgDiscount.Rows[j].Cells["Type"].Value) == "Tax")
                {
                    if( !string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[j].Cells["DiscountAmount"].Value)))
                    {
                        tDTax += Convert.ToDouble(DgDiscount.Rows[j].Cells["DiscountAmount"].Value);
                    }
                }
                else if (Convert.ToString(DgDiscount.Rows[j].Cells["Type"].Value) == "Discount")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[j].Cells["DiscountAmount"].Value)))
                    {
                        tDDiscount += Convert.ToDouble(DgDiscount.Rows[j].Cells["DiscountAmount"].Value);
                    }
                }
                else if (Convert.ToString(DgDiscount.Rows[j].Cells["Type"].Value) == "Additions")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[j].Cells["DiscountAmount"].Value)))
                    {
                        tDiscountAdditions += Convert.ToDouble(DgDiscount.Rows[j].Cells["DiscountAmount"].Value);
                    }
                }
                else
                {
                   
                }
            }
            //Columns Seconds Row values nedds
            for (int kr = 0; kr < DgDiscount.Rows.Count - 1; kr++)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[kr].Cells["DiscountAmount"].Value)))
                {
                    if ((!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[kr].Cells["DiscountAmount"].Value))) && Convert.ToDouble(DgDiscount.Rows[kr].Cells["DiscountAmount"].Value)>0) 
                    {
                        TDiscountPerAmount += Convert.ToDouble(DgDiscount.Rows[kr].Cells["DiscountAmount"].Value);
                    }
                }
            }
            lblDiscountDis.Text = tDDiscount.ToString("0.00");
            txtDiscountTax.Text = tDTax.ToString("0.00");
            lblDiscountAdditions.Text = tDiscountAdditions.ToString("0.00");
            lblDiscountNetAmt.Text = ((tDTax + tDiscountAdditions + Convert.ToDouble(lblAmount.Text)) - tDDiscount).ToString("0.00");
            lbl_netAmount.Text = Convert.ToDouble(lblDiscountNetAmt.Text).ToString("0.00");
            txtInvalue.Text = Convert.ToDouble(lblDiscountNetAmt.Text).ToString("0.00");
        }
        string DgDiscountTpe = "", DgDiscountTpe1 = "", jCheck = "0";
                       
        private void DgDiscount_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 1)
            {
                if (DgDiscount.Rows.Count < 1)
                {
                    if (DgDiscount.Rows[0].Cells[0].Value != string.Empty)
                    {
                        DgDiscountTpe = "";
                        DgDiscountTpe = DgDiscount.Rows[0].Cells[0].Value.ToString();
                       
                    }
                }
                else
                {
                    if (DgDiscount.Rows.Count > 1)
                    {
                        if (jCheck == "0")
                        {
                            DgDiscountTpe1 = DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[0].Value == null ? "" : DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[0].Value.ToString();
                            //if (this.DgDiscount.CurrentCell.ColumnIndex == this.DgDiscount.Columns[1].Index)
                            //{
                            //    if (string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[DgDiscount.CurrentCell.RowIndex].Cells[0].Value)))
                            //    {
                            //        //MyMessageBox1.ShowBox(DgDiscountTpe1 + " Should Be Enter before " + "  " + DgDiscountTpe, "Warning");
                            //        PnlDiscount.Visible = false;
                            //        btnSave.Focus();
                            //        //int nextindex = Math.Min(this.DgDiscount.Columns.Count - 1, this.DgDiscount.CurrentCell.ColumnIndex + 0);
                            //        //SetColumnIndex method = new SetColumnIndex(Mymethod);
                            //        //this.DgDiscount.BeginInvoke(method, 0);
                            //        jCheck = "1";
                            //    }
                            //}

                        }
                        else
                        {
                            jCheck = "0";
                        }
                    }
                
                }
            }
           
        }
        public void DgDiscountCalculations()
        {
            if (DgDiscount.Rows.Count > 1)
            {
                for (int j = 0; j < DgDiscount.Rows.Count - 1; j++)
                {
                    //Discount Amount for quentity:
                    if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[j].Cells[2].Value)) && Convert.ToDouble(DgDiscount.Rows[j].Cells[2].Value) > 0)
                    {
                        DPerqty = 0.00;
                        DPerqty = Convert.ToDouble(DgDiscount.Rows[j].Cells[2].Value);
                        DPerqty = (DPerqty * Convert.ToDouble(lbl_Qty.Text));
                        DgDiscount.Rows[j].Cells[4].Value = DPerqty.ToString();
                        tDiscountGridCalculation();
                    }
                    //Discount percentage:
                  else if (!string.IsNullOrEmpty(Convert.ToString(DgDiscount.Rows[j].Cells[3].Value)) && Convert.ToDouble(DgDiscount.Rows[j].Cells[3].Value) > 0)
                    {
                        TDisountPertot = DgDiscount.Rows[j].Cells[3].Value == null || DgDiscount.Rows[j].Cells[3].Value == "" ? 0.00 : Convert.ToDouble(DgDiscount.Rows[j].Cells[3].Value);
                        DgPurchaseNetAmount = Convert.ToDouble(lblAmount.Text);
                        DgDiscount.Rows[j].Cells[3].Value = TDisountPertot.ToString("0.00");
                        TDisountPertot = (((TDiscountPerAmount + DgPurchaseNetAmount) * TDisountPertot) / 100);
                        DgDiscount.Rows[j].Cells["DiscountAmount"].Value = TDisountPertot.ToString("0.00");
                        tDiscountGridCalculation();
                    }
                }
            }
        }
        public void Mymethod1(int columnIndex)
        {
            this.DgDiscount.CurrentCell = this.DgDiscount.CurrentRow.Cells[columnIndex];
            this.DgDiscount.BeginEdit(true);
        }
    }
}
