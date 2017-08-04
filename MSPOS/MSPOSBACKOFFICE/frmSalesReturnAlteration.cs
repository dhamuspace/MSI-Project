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
    public partial class frmSalesReturnAlteration : Form
    {
        DataTable autofind = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dtPrintNew = new DataTable();
        public frmSalesReturnAlteration()
        {
            InitializeComponent();
            try
            {
                pnl_type.Visible = false;
                lst_type.Visible = false;
                pnl_sales.Visible = false;
                pnl_customer.Visible = false;
                lst_ledger.Visible = false;

                dtPrintNew.Columns.Add("S.no", typeof(string));
                dtPrintNew.Columns.Add("Item_code", typeof(string));
                dtPrintNew.Columns.Add("Item_name", typeof(string));
                dtPrintNew.Columns.Add("nt_qty", typeof(string));
                dtPrintNew.Columns.Add("Rate", typeof(string));
                dtPrintNew.Columns.Add("Amount", typeof(string));
                dtPrintNew.Columns.Add("Id", typeof(string));

                foreach (DataGridViewColumn col in grd_SalesRecord.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Pixel);
                }
                grd_SalesRecord.ColumnHeadersHeight = 30;
                grd_SalesRecord.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
                grd_SalesRecord.BackgroundColor = Color.White;
                DataTable dtBillNO = new DataTable();
                dtBillNO.Rows.Clear();
                SqlCommand cmdBillNo = new SqlCommand("select smas_billno from salmas_table where smas_no=(select smas_rtno from salmas_table where smas_billno=@tBillNo and smas_rtno<>0)", con);
                cmdBillNo.Parameters.AddWithValue("@tBillNo", chkbox.SalesBillNo);
                SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                adpBillNo.Fill(dtBillNO);
                if (dtBillNO.Rows.Count > 0)
                {
                    txtBillNo.Text = dtBillNO.Rows[0]["smas_billno"].ToString(); ;
                }

                txt_date.Text = chkbox.DateSalesEntry;
                txtPDate.Text = txt_date.Value.ToShortDateString();
                loadSalesrecords();
                grd_SalesRecord.Columns[0].Width = 100;
                grd_SalesRecord.Columns[1].Width = 150;
                grd_SalesRecord.Columns[2].Width = 300;
                grd_SalesRecord.Columns[3].Width = 150;
                grd_SalesRecord.Columns[4].Width = 150;
                grd_SalesRecord.Columns[5].Width = 150;
                int a = grd_SalesRecord.Rows.Count;
                lbl_ItemCount.Text = a.ToString();
                funTotalCalculation();
                grd_SalesRecord.Columns["S.no"].ReadOnly = true;
                grd_SalesRecord.Columns["Item_code"].ReadOnly = true;
                grd_SalesRecord.Columns["Item_name"].ReadOnly = true;
                grd_SalesRecord.Columns["Amount"].ReadOnly = true;
                grd_SalesRecord.Columns["Id"].Visible = false;
                // grd_SalesRecord.ReadOnly = true;

                DataTable dtNew101 = new DataTable();
                dtNew101.Rows.Clear();
                // string tonameqry = "select smas_name from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                SqlCommand cmdReturnNo = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmdReturnNo.CommandType = CommandType.StoredProcedure;
                cmdReturnNo.Parameters.AddWithValue("@tActionType", "SALESRETURNNO");
                cmdReturnNo.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp101 = new SqlDataAdapter(cmdReturnNo);
                adp101.Fill(dtNew101);
                if (dtNew101.Rows.Count > 0)
                {
                    txtReturnNo.Text = dtNew101.Rows[0]["ReturnNo"].ToString();
                }


                // con.Open();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                // string tonameqry = "select smas_name from salmas_table where smas_billno='" + chkbox.SalesBillNo + "'";
                SqlCommand cmdtoname = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmdtoname.CommandType = CommandType.StoredProcedure;
                cmdtoname.Parameters.AddWithValue("@tActionType", "CASHTYPERETURN");
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
                CounterNameqry.Parameters.AddWithValue("@tActionType", "COUNTERNAMERETURN");
                CounterNameqry.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp1 = new SqlDataAdapter(CounterNameqry);
                adp1.Fill(dtNew1);
                txt_counter.Text = "";
                if (dtNew1.Rows.Count > 0)
                {
                    txt_counter.Text = dtNew1.Rows[0]["ctr_name"].ToString();
                    txtPcounter.Text = dtNew1.Rows[0]["ctr_name"].ToString();
                }





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
                    txtPCashType.Text = dtNew4.Rows[0][0].ToString();
                    txt_type.Text = dtNew4.Rows[0][0].ToString();
                }



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
                txtReturnNo.Text = chkbox.SalesBillNo;
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
                grd_SalesRecord.DefaultCellStyle.Font = new Font("Arial", 10);
                grd_SalesRecord.RowTemplate.Height = 20;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }

        }
        DataTable dtgrdload = new DataTable();
        DataTable dtPreLoad = new DataTable();
        int deletedRecNo;
        public void loadSalesrecords()
        {
            try
            {
                DataTable dtNew5 = new DataTable();
                dtNew5.Rows.Clear();

                SqlCommand smasrecordNo = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                smasrecordNo.CommandType = CommandType.StoredProcedure;
                smasrecordNo.Parameters.AddWithValue("@tActionType", "SALMASNOCHANGERETURN");
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
                cmd.Parameters.AddWithValue("@tActionType", "ITEMFILLRETURN");
                cmd.Parameters.AddWithValue("@tValue", chkbox.SalesBillNo);
                SqlDataAdapter adp6 = new SqlDataAdapter(cmd);
                adp6.Fill(dtgrdload);
                dtPrintNew.Rows.Clear();
                for (int i = 0; i < dtgrdload.Rows.Count; i++)
                {
                    // dtPrintNew.Rows.Add(i + 1, dtgrdload.Rows[i]["Item_code"].ToString(), dtgrdload.Rows[i]["Item_name"].ToString(), ((dtgrdload.Rows[i]["nt_Qty"].ToString() == "0") ? "0" : dtgrdload.Rows[i]["nt_Qty"].ToString()), dtgrdload.Rows[i]["Rate"].ToString(),(( dtgrdload.Rows[i]["Amount"].ToString()=="")?"0.00":dtgrdload.Rows[i]["Amount"].ToString()));
                    dtPrintNew.Rows.Add(i + 1, dtgrdload.Rows[i]["Item_code"].ToString(), dtgrdload.Rows[i]["Item_name"].ToString(), "0", string.Format("{0:0.00}", (dtgrdload.Rows[i]["Rate"].ToString().Trim() == "") ? 0.00 : double.Parse(dtgrdload.Rows[i]["Rate"].ToString())), "0.00", dtgrdload.Rows[i]["Id"].ToString());
                }

                // DataTable dtNew7 = new DataTable();
                dtPreLoad.Rows.Clear();

                SqlCommand cmd7 = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmd7.CommandType = CommandType.StoredProcedure;
                cmd7.Parameters.AddWithValue("@tActionType", "SALESRETURNITEMRETURNQTY");
                cmd7.Parameters.AddWithValue("@tValue", deletedRecNo);
                SqlDataAdapter adp7 = new SqlDataAdapter(cmd7);
                adp7.Fill(dtPreLoad);
                // dtPrintNew.Rows.Clear();
                for (int i = 0; i < dtPreLoad.Rows.Count; i++)
                {
                    for (int j = 0; j < dtPrintNew.Rows.Count; j++)
                    {
                        if (dtPrintNew.Rows[j]["Item_name"].ToString().Trim() == dtPreLoad.Rows[i]["Item_Name"].ToString().Trim() && dtPrintNew.Rows[j]["Id"].ToString().Trim() == dtPreLoad.Rows[i]["Id"].ToString().Trim())
                        {
                            dtPrintNew.Rows[j]["Nt_Qty"] = (dtPreLoad.Rows[i]["Nt_Qty"].ToString().Trim() == "") ? "0" : dtPreLoad.Rows[i]["Nt_Qty"].ToString();
                            dtPrintNew.Rows[j]["Rate"] = (dtPreLoad.Rows[i]["Rate"].ToString().Trim() == "") ? "0.00" : string.Format("{0:0.00}", double.Parse(dtPreLoad.Rows[i]["Rate"].ToString()));
                            dtPrintNew.Rows[j]["Amount"] = (dtPreLoad.Rows[i]["Amount"].ToString().Trim() == "") ? "0.00" : string.Format("{0:0.00}", double.Parse(dtPreLoad.Rows[i]["Amount"].ToString()));
                            break;
                        }
                    }
                    // dtPrintNew.Rows.Add(i + 1, dtgrdload.Rows[i]["Item_code"].ToString(), dtgrdload.Rows[i]["Item_name"].ToString(), ((dtgrdload.Rows[i]["nt_Qty"].ToString() == "0") ? "0" : dtgrdload.Rows[i]["nt_Qty"].ToString()), dtgrdload.Rows[i]["Rate"].ToString(),(( dtgrdload.Rows[i]["Amount"].ToString()=="")?"0.00":dtgrdload.Rows[i]["Amount"].ToString()));
                    //  dtPrintNew.Rows.Add(i + 1, dtgrdload.Rows[i]["Item_code"].ToString(), dtgrdload.Rows[i]["Item_name"].ToString(), "0", dtgrdload.Rows[i]["Rate"].ToString(), "0.00");
                }

                grd_SalesRecord.DataSource = dtPrintNew;
                funTotalCalculation();
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
                funRoundCalculate();
                lblBalanceAmt.Text = string.Format("{0:0.00}", double.Parse(lbl_Billamt.Text));
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
        DataTable dsRound = new DataTable();
        string tRoundType, firstDecimal, secondDecimal;
        double tRoundValue, tWhole, tDecimal;
        void funRoundCalculate()
        {
            try
            {
                funConnectionStateCheck();
                SqlCommand cmd = new SqlCommand("sp_SalesCreation_RoundCalculate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dsRound);
                // dr = cmd.ExecuteReader();
                //dsRound.Load(dr);
                if (dsRound.Rows.Count > 0)
                {
                    tRoundType = dsRound.Rows[0]["RProp"].ToString();
                    // MessageBox.Show(tRoundType);
                    tRoundValue = Math.Round(double.Parse(lbl_Billamt.Text.ToString()), 2);
                    tDecimal = Math.Round(tRoundValue % 1, 2);
                    //  MessageBox.Show(tDecimal.ToString());
                    tWhole = tRoundValue - tDecimal;
                    // MessageBox.Show(tWhole.ToString());
                    //  MessageBox.Show(Convert.ToString( tDecimal).Length.ToString());
                    if (tDecimal.ToString().Length == 1)
                    {
                        firstDecimal = "0";
                        secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1);
                    }
                    else if (tDecimal.ToString().Length == 4)
                    {
                        firstDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1);
                        secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
                    }
                    if (tRoundType == "5cent")
                    {
                        if (tDecimal == 0.99 && tDecimal == 0.98)
                        {
                            tWhole = tWhole + 1;
                            lbl_Billamt.Text = String.Format("{0:0.00}", tWhole);
                        }
                        else if (tDecimal >= 0.90 && tDecimal < 0.98)
                        {
                            if (tDecimal.ToString().Length == 4)
                            {

                                switch (tDecimal.ToString().Substring(3, 1))
                                {

                                    case "0":
                                    case "1":
                                    case "2":
                                        {
                                            // secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
                                            lbl_Billamt.Text = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
                                            break;
                                        }
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                        {
                                            // tWhole = tWhole + 1;
                                            lbl_Billamt.Text = tRoundValue.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "5");
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                //  MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }
                        }
                        else
                        {
                            //      MessageBox.Show(tDecimal.ToString().Substring(3, 1));
                            if (tDecimal.ToString().Length == 4)
                            {
                                switch (tDecimal.ToString().Substring(3, 1))
                                {
                                    case "8":
                                    case "9":
                                    case "0":
                                    case "1":
                                    case "2":
                                        {
                                            //  MessageBox.Show(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1));

                                            if (firstDecimal == "9" || firstDecimal == "8")
                                            {
                                                secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                                lbl_Billamt.Text = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            }
                                            else
                                            {
                                                //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                                //  lblNetAmt.Content = String.Format("{0:0.00}", (tRoundValue.ToString().Replace(secondDecimal.ToString()+firstDecimal.ToString(),secondDecimal+"0")));
                                                lbl_Billamt.Text = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            }
                                            break;
                                        }
                                    case "3":
                                    case "4":
                                    case "5":
                                    case "6":
                                    case "7":
                                        {
                                            //  secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                            lbl_Billamt.Text = String.Format("{0:0.00}", tRoundValue.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "5"));
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                //   MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }

                        }
                        ////// string tempStr = lblNetAmt.Content.ToString().Trim();
                        //////// int start = tempStr.Length - 1;
                        //////// MessageBox.Show(tempStr.Substring(tempStr.Length - 1, 1));
                        //////                }
                    }
                    if (tRoundType == "10cent")
                    {
                        if (tDecimal <= 0.99 && tDecimal >= 0.95)
                        {
                            tWhole = tWhole + 1;
                            lbl_Billamt.Text = String.Format("{0:0.00}", tWhole);
                        }
                        else if (tDecimal >= 0.90 && tDecimal < 0.95)
                        {
                            if (tDecimal.ToString().Length == 4)
                            {

                                switch (tDecimal.ToString().Substring(3, 1))
                                {

                                    case "0":
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                        {
                                            // secondDecimal = tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 1);
                                            lbl_Billamt.Text = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 1, 1), "0"))));
                                            break;
                                        }

                                }
                            }
                            else
                            {
                                //  MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }
                        }
                        else
                        {
                            //      MessageBox.Show(tDecimal.ToString().Substring(3, 1));
                            if (tDecimal.ToString().Length == 4)
                            {
                                switch (tDecimal.ToString().Substring(3, 1))
                                {
                                    case "0":
                                    case "1":
                                    case "2":
                                    case "3":
                                    case "4":
                                        {

                                            lbl_Billamt.Text = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            break;
                                        }
                                    case "5":
                                    case "6":
                                    case "7":
                                    case "8":
                                    case "9":
                                        {
                                            secondDecimal = (double.Parse(secondDecimal) + 1).ToString();
                                            lbl_Billamt.Text = String.Format("{0:0.00}", (tWhole + double.Parse(tDecimal.ToString().Replace(tDecimal.ToString().Substring(tDecimal.ToString().Length - 2, 2), secondDecimal + "0"))));
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                //   MyMessageBox.ShowBox("Error.Check Decimal Point.Contact Programmer", "Warning");
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
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
        string chk = "";
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
                    // txt_sales.Focus();
                    grd_SalesRecord.Focus();
                }
            }
            if (e.KeyCode == Keys.Down)
            {

            }
        }

        private void txt_ReceivedAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
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

        private void lst_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_type.Text = lst_type.SelectedItem.ToString();
        }

        private void txt_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_type.Select();
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

        private void lst_type_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txt_type.Text = lst_type.SelectedItem.ToString();
                    pnl_type.Visible = false;
                    grd_SalesRecord.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void lst_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_sales.SelectedItems.Count > 0)
            {
                txt_counter.Text = lst_sales.SelectedItem.ToString();
            }
        }

        private void txt_counter_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_customer.Visible = true;
            pnl_sales.Visible = false;
            pnl_type.Visible = false;
        }

        private void txt_type_MouseClick(object sender, MouseEventArgs e)
        {
            pnl_customer.Visible = false;
            pnl_sales.Visible = false;
            pnl_type.Visible = true;
        }

        private void frmSalesReturnAlteration_Load(object sender, EventArgs e)
        {
            try
            {
                dtFinal.Columns.Add("ItemName", typeof(string));
                dtFinal.Columns.Add("Qty", typeof(string));
                dtFinal.Columns.Add("Rate", typeof(string));
                dtFinal.Columns.Add("Amt", typeof(string));
                dtFinal.Columns.Add("Id", typeof(string));

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }
        DataTable dtFinal = new DataTable();
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                bool isChk = false;
                dtFinal.Rows.Clear();
                for (int i = 0; i < grd_SalesRecord.Rows.Count; i++)
                {
                    if (grd_SalesRecord.Rows[i].Cells["nt_Qty"].Value.ToString().Trim() != "" || grd_SalesRecord.Rows[i].Cells["Nt_qty"].Value != null)
                    {
                        if (double.Parse(grd_SalesRecord.Rows[i].Cells["nt_Qty"].Value.ToString().Trim()) > 0)
                        {
                            isChk = true;
                            dtFinal.Rows.Add(grd_SalesRecord.Rows[i].Cells["Item_Name"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["nt_Qty"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Rate"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Amount"].Value.ToString().Trim(), grd_SalesRecord.Rows[i].Cells["Id"].Value.ToString().Trim());
                        }
                    }
                }
                if (isChk == true)
                {
                    SqlCommand cmd = new SqlCommand("sp_SalesReturnAlteration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tOldBillNo", deletedRecNo);
                    cmd.Parameters.AddWithValue("@tGrossAmt", (lbl_Gross_Amt.Text.ToString().Trim() == "") ? "0.00" : lbl_Gross_Amt.Text.ToString());// double.Parse(lblTotAmt.Content.ToString()));
                    cmd.Parameters.AddWithValue("@tNetAmt", (lbl_Billamt.Text.ToString().Trim() == "") ? "0.00" : lbl_Billamt.Text.ToString());// double.Parse(lblNetAmt.Content.ToString()));
                    //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                    cmd.Parameters.AddWithValue("@tTotTax", "0.00");// double.Parse(lblTotAmt.Content.ToString()));
                    cmd.Parameters.AddWithValue("@tUserno", _Class.clsVariables.tUserNo);
                    cmd.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                    //double tot = ((double.Parse(lblNetAmt.Content.ToString()) - double.Parse(lblDiscount.Content.ToString())) - (double.Parse(lblTotAmt.Content.ToString()) + double.Parse(lblTaxAmt.Content.ToString())));
                    cmd.Parameters.AddWithValue("@RoundValue", "0.00");
                    cmd.Parameters.AddWithValue("@ReturnDate", txt_date.Value);
                    cmd.Parameters.AddWithValue("@ReturnCounter", txt_counter.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReturnCash", txt_type.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReturnRecieved", (txt_ReceivedAmt.Text.Trim() == "") ? "0.00" : txt_ReceivedAmt.Text.Trim());
                    cmd.Parameters.AddWithValue("@tempTable", dtFinal);
                    con.Close();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    grd_SalesRecord.DataSource = null;  // Change gridItems.ItemsSource = null;
                    dt.Clear();
                    lbl_ItemCount.Text = "0";
                    lbl_Qty_count.Text = "0";
                    lblDiscount.Text = "0.00";
                    lbl_Billamt.Text = "0.00";
                    lbl_Gross_Amt.Text = "0.00";
                    // lbl lblTaxAmt.Content = "0.00";
                    lblBalanceAmt.Text = "0.00";
                    txt_ReceivedAmt.Text = "0.00";
                    txtBillNo.Text = "0.00";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void grd_SalesRecord_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
                {
                    double tNtQty = 0, tRate = 0, tAmount = 0, tPreQty = 0;
                    if (grd_SalesRecord.Rows[e.RowIndex].Cells["Nt_Qty"].Value.ToString().Trim() != "" && grd_SalesRecord.Rows[e.RowIndex].Cells["Nt_Qty"].Value != null)
                    {
                        tNtQty = double.Parse(grd_SalesRecord.Rows[e.RowIndex].Cells["Nt_Qty"].Value.ToString());
                    }
                    if (grd_SalesRecord.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim() != "" && grd_SalesRecord.Rows[e.RowIndex].Cells["Rate"].Value != null)
                    {
                        tRate = double.Parse(grd_SalesRecord.Rows[e.RowIndex].Cells["Rate"].Value.ToString());
                    }

                    for (int i = 0; i < dtPreLoad.Rows.Count; i++)
                    {
                        if (grd_SalesRecord.Rows[e.RowIndex].Cells["Item_name"].Value.ToString() == dtPreLoad.Rows[i]["Item_name"].ToString())
                        {
                            if (dtPreLoad.Rows[i]["Nt_Qty"].ToString().Trim() != "")
                            {
                                tPreQty = double.Parse(dtPreLoad.Rows[i]["Nt_Qty"].ToString());

                            }
                            break;
                        }
                    }

                    DataTable dtChk = new DataTable();
                    dtChk.Rows.Clear();
                    SqlCommand cmd = new SqlCommand(" select * from stktrn_table where strn_sno=(select strn_rtno from stktrn_table where strn_no=@tDeletedNo and strn_type=2 and item_no=(select item_no from Item_table where Item_name=@tName))", con);
                    cmd.Parameters.AddWithValue("@tDeletedNo", deletedRecNo);
                    cmd.Parameters.AddWithValue("@tName", grd_SalesRecord.Rows[e.RowIndex].Cells["Item_Name"].Value.ToString());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtChk);
                    double tNt_qty = 0, trNt_qty = 0, tCurVal = 0;
                    bool isChk = false;
                    if (dtChk.Rows.Count > 0)
                    {
                        if (dtChk.Rows[0]["nt_Qty"].ToString().Trim() != "" || dtChk.Rows[0]["nt_Qty"].ToString() != null)
                        {
                            tNt_qty = double.Parse(dtChk.Rows[0]["Nt_Qty"].ToString());
                        }
                        if (dtChk.Rows[0]["rnt_Qty"].ToString().Trim() != "" || dtChk.Rows[0]["rnt_Qty"].ToString() != null)
                        {
                            trNt_qty = double.Parse(dtChk.Rows[0]["rnt_Qty"].ToString());
                        }

                        if (grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value.ToString().Trim() != "" || grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value.ToString() != null)
                        {
                            tCurVal = double.Parse(grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value.ToString());
                        }
                        if (tNt_qty < ((trNt_qty - tPreQty) + tCurVal))
                        {
                            isChk = true;
                            grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value = "0";
                            grd_SalesRecord.Rows[e.RowIndex].Cells["Amount"].Value = "0.00";
                            MyMessageBox.ShowBox("Too many Quantity", "Warning");

                        }
                    }
                    if (isChk == false)
                    {

                        grd_SalesRecord.Rows[e.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (tNtQty * tRate));
                        if (grd_SalesRecord.Rows[e.RowIndex].Cells["Amount"].Value.ToString().Trim() != "" && grd_SalesRecord.Rows[e.RowIndex].Cells["Amount"].Value != null)
                        {
                            tAmount = double.Parse(grd_SalesRecord.Rows[e.RowIndex].Cells["Amount"].Value.ToString());
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

        private void grd_SalesRecord_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.KeyPress += new KeyPressEventHandler(gridDisplay_KeyPress);
            }
        }

        private void gridDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (gridDisplay.CurrentCell.ColumnIndex == 8 || gridDisplay.CurrentCell.ColumnIndex == 7)
            //{
            //    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //}
            if (grd_SalesRecord.CurrentCell.ColumnIndex == 3 || grd_SalesRecord.CurrentCell.ColumnIndex == 4)
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

        private void lblDiscount_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void grd_SalesRecord_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    DataTable dtChk = new DataTable();
                    dtChk.Rows.Clear();
                    SqlCommand cmd = new SqlCommand(" select * from stktrn_table where strn_sno=(select strn_rtno from stktrn_table where strn_no=@tDeletedNo and strn_type=2 and item_no=(select item_no from Item_table where Item_name=@tName))", con);
                    cmd.Parameters.AddWithValue("@tDeletedNo", deletedRecNo);
                    cmd.Parameters.AddWithValue("@tName", grd_SalesRecord.Rows[e.RowIndex].Cells["Item_Name"].Value.ToString());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtChk);
                    double tNt_qty = 0, trNt_qty = 0, tCurVal = 0;
                   // bool isChk = false;
                    lblSoldQty.Text = "0";
                    lblReturnQty.Text = "0";
                    if (dtChk.Rows.Count > 0)
                    {
                        if (dtChk.Rows[0]["nt_Qty"].ToString().Trim() != "" || dtChk.Rows[0]["nt_Qty"].ToString() != null)
                        {
                            tNt_qty = double.Parse(dtChk.Rows[0]["Nt_Qty"].ToString());
                        }
                        lblSoldQty.Text = tNt_qty.ToString();
                        if (dtChk.Rows[0]["rnt_Qty"].ToString().Trim() != "" || dtChk.Rows[0]["rnt_Qty"].ToString() != null)
                        {
                            trNt_qty = double.Parse(dtChk.Rows[0]["rnt_Qty"].ToString());
                        }
                        lblReturnQty.Text = trNt_qty.ToString();
                        if (grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value.ToString().Trim() != "" || grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value.ToString() != null)
                        {
                            tCurVal = double.Parse(grd_SalesRecord.Rows[e.RowIndex].Cells["nt_Qty"].Value.ToString());
                        }

                    }
                    else
                    {
                        DataTable dtChk1 = new DataTable();
                        dtChk1.Rows.Clear();
                        SqlCommand cmd1 = new SqlCommand(" select * from stktrn_table where strn_no=(select smas_rtno from SalMas_table where smas_no=@tDeletedNo) and strn_type=1 and item_no=(select item_no from Item_table where Item_name=@tName)", con);
                        cmd1.Parameters.AddWithValue("@tDeletedNo", deletedRecNo);
                        cmd1.Parameters.AddWithValue("@tName", grd_SalesRecord.Rows[e.RowIndex].Cells["Item_Name"].Value.ToString());
                        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                        adp1.Fill(dtChk1);
                        if (dtChk1.Rows.Count > 0)
                        {
                            if (dtChk1.Rows[0]["nt_Qty"].ToString().Trim() != "" || dtChk1.Rows[0]["nt_Qty"].ToString() != null)
                            {
                                tNt_qty = double.Parse(dtChk1.Rows[0]["Nt_Qty"].ToString());
                            }

                        }
                        lblSoldQty.Text = tNt_qty.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

    }
}
