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
    public partial class StockAdjustmentAlteration : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        string globalid = chkbox.adjrecno;
        DataTable autofind = new DataTable();
        DataTable dtNew = new DataTable();
        string PartyNO, CtrNO;
        string tempCode = "";
        public int rowno = 0;
        public int initvar = 0;
        public int loopstart = 0;
        public int loopend = 0;
        public int loopstart2 = 0;
        public int loopend2 = 0;
        public int loopstart3 = 0;
        public int loopend3 = 0;
        public int loopstart4 = 0;
        public int loopend4 = 0;
        public int loopstart5 = 0;
        public int loopend5 = 0;
        public int loopstart6 = 0;
        public int loopend6 = 0;
        public int loopstart7 = 0;
        public int loopend7 = 0;
        public int loopstart8 = 0;
        public int loopend8 = 0;
        public int loopstart9 = 0;
        public int loopend9 = 0;
        public int loopstart10 = 0;
        public int loopend10 = 0;
        public int loopstart11 = 0;
        public int loopend11 = 0;
        public int loopstart12 = 0;
        public int loopend12 = 0;
        DataTable dt2_Check = new DataTable();
        string t1, t3;

        public StockAdjustmentAlteration()
        {
            InitializeComponent();

            this.myDataGrid1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            this.myDataGrid1.RowTemplate.Height = 25;


            try
            {
                pnl_comp_name.Visible = false;
                pnl_ctrname.Visible = false;
                button1.Visible = false;

                //txt_date.Format = DateTimePickerFormat.Custom;
                //txt_date.CustomFormat = "dd/MM/yyyy";
                txt_date.Focus();
                txt_date.Select();

                dtNew.Columns.Add("Code", typeof(string));
                dtNew.Columns.Add("Name", typeof(string));
                dtNew.Columns.Add("Unit", typeof(string));
                dtNew.Columns.Add("Less_Qty", typeof(string));
                dtNew.Columns.Add("Add_Qty", typeof(string));
                dtNew.Columns.Add("Rate", typeof(string));
                dtNew.Columns.Add("Amount", typeof(string));
                dtNew.Columns.Add("Stock_Category", typeof(string));
                myDataGrid1.DataSource = dtNew.DefaultView;

                //txt_date.Text = DateTime.Today.Date.ToShortDateString();
                //txt_date.Focus();
                //dt_inv.Format = DateTimePickerFormat.Custom;
                //dt_inv.CustomFormat = "dd/MM/yyyy";
                myDataGrid1.Columns[0].Width = 130;
                myDataGrid1.Columns[1].Width = 330;
                myDataGrid1.Columns[2].Width = 80;
                myDataGrid1.Columns[3].Width = 100;
                myDataGrid1.Columns[4].Width = 100;
                myDataGrid1.Columns[5].Width = 120;
                myDataGrid1.Columns[6].Width = 140;
                foreach (DataGridViewColumn col in myDataGrid1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                if (globalid != null && globalid != "")
                {
                    lbl_stckbanner.Text = "Stock Adjustment Alteration";
                    loadstckalter();
                    lbl_amt.Text = String.Format("{0:0.00}", Convert.ToDouble(FirstTotal.ToString()));
                }
                con.Open();
                string savebtn = "select Cancel from adjmas_table where Adj_Billno='" + globalid + "'";
                SqlCommand canupdat = new SqlCommand(savebtn, con);
                bool a = Convert.ToBoolean(canupdat.ExecuteScalar());
                con.Close();
                if (a == true)
                {
                    btn_save.Enabled = false;
                }
                else
                {
                    btn_save.Enabled = true;
                }

                con.Open();
                SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table where Item_Active=" + 1 + " order by Item_name ASC", con);
                SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
                nameadp.Fill(autofind);
                con.Close();

                lbl_adjust_no.Text = chkbox.adjrecno;
                // get a counter value:
                con.Open();
                string AdjNOQry = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
                SqlCommand CmdAdjustNo = new SqlCommand(AdjNOQry, con);
                string AdjNo = CmdAdjustNo.ExecuteScalar().ToString();
                con.Close();

                // get a Counter Name and company Name :

                string CounterNameqry = "select Distinct(StrnParty_no),strn_date,ctr_no,InvoiceNo,InvoiceDate from stktrn_table where strn_no='" + AdjNo + "'";
                SqlCommand cmdCompany = new SqlCommand(CounterNameqry, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmdCompany);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        CtrNO = dt.Rows[j]["ctr_no"].ToString();
                        PartyNO = dt.Rows[j]["StrnParty_no"].ToString();
                        txt_inv_no.Text = dt.Rows[j]["InvoiceNo"].ToString();
                        dt_inv.Text = dt.Rows[j]["InvoiceDate"].ToString();
                        txt_date.Text = dt.Rows[j]["strn_date"].ToString();
                    }
                }
                con.Close();
                con.Open();
                string Partyqry = "select Ledger_name from Ledger_table where Ledger_no='" + PartyNO + "'";
                SqlCommand cmdParty = new SqlCommand(Partyqry, con);
                var temp = cmdParty.ExecuteScalar();
                if (temp == null)
                {
                    txt_comp_name.Text = "";
                }
                else
                {
                    txt_comp_name.Text = cmdParty.ExecuteScalar().ToString();
                }
                con.Close();

                con.Open();
                string CtrnNoqry = "select ctr_name from counter_table where ctr_no='" + CtrNO + "'";
                SqlCommand cmdCtrnNo = new SqlCommand(CtrnNoqry, con);
                if (cmdCtrnNo.ExecuteScalar() != null)
                {
                    txt_countername.Text = cmdCtrnNo.ExecuteScalar().ToString();
                }
                else
                {
                    txt_countername.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public delegate void SetColumnIndex(int i);
        public void Mymethod(int columnIndex)
        {
            //int k=Convert.ToInt32(dgsales.CurrentCell.ColumnIndex);
            //this.dgsales.CurrentCell = this.dgsales.CurrentRow.Cells[k];
            //int o = Convert.ToInt32(dgsales.TabIndex.ToString());
            //this.dgsales.BeginEdit(dgsales.TabIndex.Equals(o-2));
            //dgsales.BeginEdit(true);
            this.myDataGrid1.CurrentCell = this.myDataGrid1.CurrentRow.Cells[columnIndex];
            this.myDataGrid1.BeginEdit(true);
            // System.Windows.Forms.Control cntObject1;
        }

        private void StockAdjustmentAlteration_Load(object sender, EventArgs e)
        {
            dt11.Columns.Add("Code");
            dt11.Columns.Add("Name");
            dt11.Columns.Add("Unit");
            dt11.Columns.Add("Less_Qty");
            dt11.Columns.Add("Add_Qty");
            dt11.Columns.Add("Rate");
            dt11.Columns.Add("Amount");
            dt11.Columns.Add("Stock_Category");


            foreach (DataGridViewColumn col in myDataGrid1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            myDataGrid1.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            myDataGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            myDataGrid1.BackgroundColor = Color.White;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

        }
        int AdjustNO;
        //, QtyType;
        DataTable dtDisplay = new DataTable();
        double FirstTotal = 0;
        public void loadstckalter()
        {
            try
            {
                string date = "select Adj_No,Adj_Date from adjmas_table where Adj_Billno='" + chkbox.adjrecno + "'";
                SqlCommand datecmd = new SqlCommand(date, con);
                SqlDataAdapter adp = new SqlDataAdapter(datecmd);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DateTime adjNODate = Convert.ToDateTime(dt.Rows[0]["Adj_Date"].ToString());
                    // txt_date.Text = adjNODate.ToString("dd/MM/yyyy");
                    txt_date.Value = adjNODate;
                    AdjustNO = Convert.ToInt16(dt.Rows[0]["Adj_No"].ToString());
                }

                DataSet ds1 = new DataSet();
                ds1.Tables.Clear();
                SqlCommand cmd11 = new SqlCommand("select Item_table.Item_code as Code,Item_table.Item_name as Name, unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Addqty, stktrn_table.Rate as Rate, stktrn_table.Amount as Amount,Item_table.stock_type as Stock_Category from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_no='" + AdjustNO + "' and Strn_type=11 ", con);
                SqlDataAdapter adp11 = new SqlDataAdapter(cmd11);
                adp11.Fill(ds1, "New");
                for (int k = 0; k < ds1.Tables["New"].Rows.Count; k++)
                {
                    dtNew.Rows.Add(ds1.Tables["New"].Rows[k][0].ToString(), ds1.Tables["New"].Rows[k][1].ToString(), ds1.Tables["New"].Rows[k][2].ToString(), ds1.Tables["New"].Rows[k][3].ToString(), "0", string.Format("{0:0.00}", Convert.ToDouble(ds1.Tables["New"].Rows[k][5].ToString())), string.Format("{0:0.00}", Convert.ToDouble(ds1.Tables["New"].Rows[k][6].ToString())), ds1.Tables["New"].Rows[k][7].ToString());
                    FirstTotal = FirstTotal + Convert.ToDouble(ds1.Tables["New"].Rows[k][6].ToString());
                }

                SqlCommand cmd111 = new SqlCommand("select Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Addqty, stktrn_table.Rate as Rate, stktrn_table.Amount as Amount,Item_table.stock_type as Stock_Category from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_no='" + AdjustNO + "' and Strn_type=12 ", con);
                SqlDataAdapter adp111 = new SqlDataAdapter(cmd111);
                adp111.Fill(ds1, "New1");
                for (int k = 0; k < ds1.Tables["New1"].Rows.Count; k++)
                {
                    dtNew.Rows.Add(ds1.Tables["New1"].Rows[k][0].ToString(), ds1.Tables["New1"].Rows[k][1].ToString(), ds1.Tables["New1"].Rows[k][2].ToString(), "0", ds1.Tables["New1"].Rows[k][4].ToString(), string.Format("{0:0.00}", Convert.ToDouble(ds1.Tables["New1"].Rows[k][5].ToString())), string.Format("{0:0.00}", Convert.ToDouble(ds1.Tables["New1"].Rows[k][6].ToString())), ds1.Tables["New1"].Rows[k][7].ToString());
                    FirstTotal = FirstTotal + Convert.ToDouble(ds1.Tables["New1"].Rows[k][6].ToString());
                }
                dtNew.Rows.Add();
                myDataGrid1.DataSource = dtNew.DefaultView;

                //con.Close();
                ////SqlCommand cmd = new SqlCommand("select stck_adj_no,stckA_code,stckA_Name,stckA_Unit,stckA_lesQty,stckA_addQty,stckA_Rate,stckA_Amt from Stockadjmas_table where stckA_code='" + chkbox.ID + "' and stck_adj_no='" + chkbox.adjrecno + "' ", con);
                ////SqlCommand cmd = new SqlCommand("select stck_adj_no,stckA_code,stckA_Name,stckA_Unit,stckA_lesQty,stckA_addQty,stckA_Rate,stckA_Amt from Stockadjmas_table where  stck_adj_no='" + chkbox.adjrecno + "' ", con);
                //SqlCommand cmd = new SqlCommand("select Item_table.Item_code as Code,Item_table.Item_name as Name,unit_table.unit_name as Unit,stktrn_table.nt_qty as Less_Qty,stktrn_table.nt_qty as Addqty, stktrn_table.Rate as Rate, stktrn_table.Amount as Amount from stktrn_table,unit_table,Item_table,adjmas_table where Item_table.Item_no=stktrn_table.item_no and unit_table.unit_no=Item_table.Unit_no and adjmas_table.Adj_No=stktrn_table.strn_no  and stktrn_table.strn_no='" + AdjustNO + "' ", con);
                // con.Open();
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //dt.Rows.Clear();
                //adp.Fill(dt);
                //double totalamt=0;
                //if (dt.Rows.Count > 0)
                //{
                //    //display the Adjust no for Alteration:
                //    lbl_adjust_no.Text = chkbox.adjrecno;
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        con.Close();
                //        con.Open();
                //        string strns_noqry = "select strn_sno from stktrn_table where strn_no='" + AdjustNO + "'";
                //        SqlCommand cmdstrnsno = new SqlCommand(strns_noqry, con);
                //        SqlDataAdapter adpter = new SqlDataAdapter(cmdstrnsno);
                //        DataTable dttable= new DataTable();
                //        adpter.Fill(dttable);
                //        con.Close();
                //         for(int i1=0;i1<dttable.Rows.Count;i1++)
                //         {
                //            con.Close();
                //            con.Open();
                //            string lessqtyqry = "select strn_type from stktrn_table where strn_sno='" + dttable.Rows[i1][0].ToString() + "' ";
                //            SqlCommand cmdlessqty = new SqlCommand(lessqtyqry, con);
                //            QtyType = Convert.ToInt16(cmdlessqty.ExecuteScalar());

                //            if (QtyType == 11)
                //            {
                //                myDataGrid1.Rows.Add();
                //                grd_stock.Rows[i].Cells["Item_code"].Value = dt.Rows[i]["Code"].ToString();
                //                grd_stock.Rows[i].Cells["I_Name"].Value = dt.Rows[i]["Name"].ToString();
                //                grd_stock.Rows[i].Cells["S_Unit"].Value = dt.Rows[i]["Unit"].ToString();
                //                grd_stock.Rows[i].Cells["Lessqty"].Value = dt.Rows[i]["Lessqty"].ToString();
                //                grd_stock.Rows[i].Cells["S_add"].Value = "";
                //                grd_stock.Rows[i].Cells["S_rate"].Value = dt.Rows[i]["Rate"].ToString();
                //                grd_stock.Rows[i].Cells["S_amt"].Value = dt.Rows[i]["Amount"].ToString();

                //                totalamt = totalamt + Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                //            }
                //            else if (QtyType == 12)
                //            {
                //                grd_stock.Rows.Add();
                //                grd_stock.Rows[i].Cells["Item_code"].Value = dt.Rows[i]["Code"].ToString();
                //                grd_stock.Rows[i].Cells["I_Name"].Value = dt.Rows[i]["Name"].ToString();
                //                grd_stock.Rows[i].Cells["S_Unit"].Value = dt.Rows[i]["Unit"].ToString();
                //                grd_stock.Rows[i].Cells["S_less"].Value = "";
                //                grd_stock.Rows[i].Cells["S_add"].Value = dt.Rows[i]["Addqty"].ToString();
                //                grd_stock.Rows[i].Cells["S_rate"].Value = dt.Rows[i]["Rate"].ToString();
                //                grd_stock.Rows[i].Cells["S_amt"].Value = dt.Rows[i]["Amount"].ToString();

                //                totalamt = totalamt + Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                //            }

                //      }
                //        // break;
                //    }
                //}
                //con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

            StckAdjDisplay frm = new StckAdjDisplay();
            this.Close();
            //frm.MdiParent = this.ParentForm;
            //frm.StartPosition = FormStartPosition.Manual;
            //frm.WindowState = FormWindowState.Normal;
            //frm.Location = new Point(0, 80);
            //frm.Show();
            frm.BringToFront();
        }

        private void StockAdjustmentAlteration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                StckAdjDisplay frm = new StckAdjDisplay();
                this.Close();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
        }

        private void txt_date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_countername.Focus();
            }
        }

        private void txt_countername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_ctrname.SelectedIndex < lst_ctrname.Items.Count - 1)
                {
                    lst_ctrname.SetSelected(lst_ctrname.SelectedIndex + 1, true);
                }
                txt_countername.Text = lst_ctrname.SelectedItem.ToString();
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lst_ctrname.SelectedIndex > 0)
                {
                    lst_ctrname.SetSelected(lst_ctrname.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnl_ctrname.Visible = false;
            }


            if (e.KeyCode == Keys.Enter)
            {
                pnl_ctrname.Visible = false;
                //if (lst_ctrname.SelectedItem != string.Empty)
                if (!string.IsNullOrEmpty(lst_ctrname.SelectedItem.ToString()))
                {
                    lst_ctrname.SetSelected(0, true);
                    txt_countername.Text = lst_ctrname.SelectedItem.ToString();
                    pnl_ctrname.Visible = false;
                    txt_inv_no.Focus();
                }
                else
                {

                    pnl_ctrname.Visible = false;
                    txt_inv_no.Focus();
                }


            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    txt_inv_no.Focus();
            //}
        }

        private void txt_inv_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dt_inv.Focus();
            }
        }

        private void dt_inv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_comp_name.Focus();
            }
        }

        private void txt_comp_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_compname.SelectedIndex < lst_compname.Items.Count - 1)
                {
                    lst_compname.SetSelected(lst_compname.SelectedIndex + 1, true);
                }
                txt_comp_name.Text = lst_compname.SelectedItem.ToString();
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lst_compname.SelectedIndex > 0)
                {
                    lst_compname.SetSelected(lst_compname.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnl_comp_name.Visible = false;
            }


            if (e.KeyCode == Keys.Enter)
            {
                pnl_comp_name.Visible = false;
                // if (lst_compname.SelectedItem != string.Empty)
                if (!string.IsNullOrEmpty(lst_compname.SelectedItem.ToString()))
                {
                    lst_compname.SetSelected(0, true);
                    txt_comp_name.Text = lst_compname.SelectedItem.ToString();
                    pnl_comp_name.Visible = false;
                    myDataGrid1.Focus();
                    if (myDataGrid1.Rows.Count > 0)
                    {
                        myDataGrid1.CurrentCell = myDataGrid1.Rows[0].Cells["Code"]; ;
                    }
                    else
                    {
                        txt_comp_name.Focus();
                    }
                }
                else
                {

                    pnl_comp_name.Visible = false;
                    myDataGrid1.Focus();
                    myDataGrid1.CurrentCell = myDataGrid1.Rows[0].Cells["Code"];
                }
            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    grd_stock.Focus();
            //    grd_stock.CurrentCell = grd_stock.Rows[0].Cells["Item_code"];
            //}
        }

        int row, col;
        private void myDataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    int col = myDataGrid1.CurrentCell.ColumnIndex;
                    int row = myDataGrid1.CurrentCell.RowIndex;

                    if (col < myDataGrid1.ColumnCount - 1)
                    {

                        col++;

                    }
                    else
                    {
                        col = 0;
                        row++;
                    }

                    if (row == myDataGrid1.RowCount)
                    {
                        dtNew.Rows.Add();
                        myDataGrid1.AllowUserToAddRows = true;
                        //dtDisplay.Rows.Add();
                        myDataGrid1.AllowUserToAddRows = false;
                    }
                    myDataGrid1.CurrentCell = myDataGrid1[col, row];
                    e.Handled = true;
                }

                int iRow = myDataGrid1.CurrentCell.RowIndex;
                if (myDataGrid1.CurrentCell.ColumnIndex == 4)
                {
                    if (myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value.ToString() != "0")
                    {
                        myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value = "0";
                    }
                }
                if (myDataGrid1.CurrentCell.ColumnIndex == 5)
                {
                    if (myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value.ToString() != "")
                    {
                        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];
                    }
                    else
                    {
                        if (myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString() == "")
                        {
                            string result = MyMessageBox.ShowBox("Empty Quantity", "Warning!");
                            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Add_Qty"];
                        }
                    }
                }
                if (myDataGrid1.CurrentCell.ColumnIndex == 2)
                {
                    if (myDataGrid1.Rows[iRow].Cells["Name"].Value.ToString() != string.Empty && myDataGrid1.Rows[iRow].Cells["Code"].Value.ToString() != string.Empty)
                    {
                        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Less_Qty"];
                    }
                    else
                    {
                        txt_remarks.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        string itemid;
        double templessvalue;
        double tempaddvalue;
        int rowsindex;

        string itementered;
        double Less_qty = 0, Add_qty = 0, price = 0;
        private void myDataGrid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (myDataGrid1.CurrentCell.ColumnIndex == 5)
                //{
                //    double total = 0;
                //    Less_qty = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString().Trim());
                //    Add_qty = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString().Trim());
                //    rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
                //    double price1 = Less_qty * rate;
                //    double price2 = Add_qty * rate;
                //    price = price1 + price2;
                //    myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
                //    for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                //    {
                //        if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                //        {
                //            total += 0;
                //        }
                //        else
                //        {
                //            total += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                //        }
                //    }
                //    lbl_amt.Text = total + ".00";
                //}

                //if (myDataGrid1.CurrentCell.ColumnIndex == 4)
                //{
                //    //myDataGrid1.Select();
                //    //myDataGrid1.CurrentCell.Selected = true;
                //    if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0.00" || myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                //    {
                //        if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value == null)
                //        {
                //            myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"];
                //            myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
                //        }
                //        else
                //        {
                //            if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                //            {
                //                MessageBox.Show("Cannot Entered Both Add and Less");
                //                myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
                //               // myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"];
                //                int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //                SetColumnIndex method = new SetColumnIndex(Mymethod);
                //                myDataGrid1.BeginInvoke(method, 4);
                //            }
                //            else
                //            {
                //                if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value != null && myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "")
                //                {
                //                    double total = 0;
                //                    quantity = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value);
                //                    rate = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value);
                //                    Double price = quantity * rate;
                //                    myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price;

                //                    for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                //                    {
                //                        if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                //                        {
                //                            total += 0;
                //                        }
                //                        else
                //                        {
                //                            total += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                //                        }
                //                    }

                //                    lbl_amt.Text = total.ToString();
                //                    //myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Rate"];
                //                    int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                //                    myDataGrid1.BeginInvoke(method, 5);
                //                }
                //                else
                //                {
                //                    myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
                //                    myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = 0;
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        double total2 = 0;
                //        MessageBox.Show("Quantity field is empty");
                //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = 0;
                //        for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                //        {
                //            if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                //            {
                //                total2 += 0;
                //            }
                //            else
                //            {
                //                total2 += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                //            }
                //        }
                //        lbl_amt.Text = total2 + ".00";
                //        //myDataGrid1.Select();
                //        //myDataGrid1.CurrentCell.Selected = true;
                //    }
                //}

                //if (myDataGrid1.CurrentCell.ColumnIndex == 3)
                //{
                //    ////if (templessvalue != Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_less"].Value.ToString()))
                //    ////{
                //    if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0" || myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0")
                //    {
                //        if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value == null)
                //        {
                //            myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"];
                //            myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
                //        }
                //        else
                //        {
                //            if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "" && myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0")
                //            {
                //                MessageBox.Show("Cannot Entered Both Add and Less");
                //                myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
                //            }
                //            else
                //            {
                //                if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                //                {
                //                    double total = 0;
                //                    quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
                //                    rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);
                //                    Double price = quantity * rate;
                //                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;

                //                    for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                //                    {
                //                        if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                //                        {
                //                            total += 0;
                //                        }
                //                        else
                //                        {
                //                            total += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                //                        }
                //                    }

                //                    lbl_amt.Text = total + ".00";
                //                    int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                //                    myDataGrid1.BeginInvoke(method, 5);
                //                    myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
                //                }
                //                else
                //                {
                //                    myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
                //                    myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = 0;
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        double total2 = 0;
                //        MessageBox.Show("Quantity field is empty");
                //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = 0;
                //        for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                //        {
                //            if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                //            {
                //                total2 += 0;
                //            }
                //            else
                //            {
                //                total2 += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                //            }
                //        }
                //        lbl_amt.Text = total2 + ".00";
                //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //        SetColumnIndex method = new SetColumnIndex(Mymethod);
                //        myDataGrid1.BeginInvoke(method, nextindex-1);
                //    }
                //}

                //if (myDataGrid1.CurrentCell.ColumnIndex == 1)
                //{
                //    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value != null)
                //    {
                //        itementered = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                //        fecthitemnamevalues();
                //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex];
                //    }
                //}

                //if (myDataGrid1.CurrentCell.ColumnIndex == 0)
                //{
                //    rowsindex = myDataGrid1.CurrentRow.Index;
                //    if (myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value != null)
                //    {
                //        itemid = myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                //        //templessvalue = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_less"].Value.ToString());
                //        //tempaddvalue = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_add"].Value.ToString());
                //        getbyid(itemid);
                //        //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = tempCode.ToString();
                //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
                //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //        SetColumnIndex method = new SetColumnIndex(Mymethod);
                //        myDataGrid1.BeginInvoke(method, 3);
                //    }
                //    else
                //    {
                //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //        SetColumnIndex method = new SetColumnIndex(Mymethod);
                //        myDataGrid1.BeginInvoke(method, 5);
                //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[rowsindex].Cells["Name"];
                //    }
                //}

                //**********************

                //if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() == "" || myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() == "0" && myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                //{
                //    // MessageBox.Show("Value Change");
                //    //myDataGrid1.Select();
                //    //myDataGrid1.CurrentCell.Selected = true;
                //    myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
                //    quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
                //    rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);
                //    Double price = quantity * rate;
                //    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;
                //    total += price;
                //    lbl_amt.Text = total + ".00";
                //    myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Rate"];
                //    lesscheck = 1;
                //    addcheck = 0;

                //}
                //// }
                //else if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value != null || myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() == "0")
                //{
                //    MyMessageBox.ShowBox("Cannot Entered Both Add and Less");
                //    myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
                //    myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"];
                //}
                //else
                //{
                //    MyMessageBox.ShowBox("Cannot Entered Both Add and Less");
                //}

                //if (myDataGrid1.CurrentCell.ColumnIndex == 1)
                //{
                //    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value != null)
                //    {
                //        itementered = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                //        fecthitemnamevalues();

                //        myDataGrid1.MultiSelect = false;
                //        myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"];
                //    }
                //}

                //if (myDataGrid1.CurrentCell.ColumnIndex == 0)
                //{
                //     rowsindex =myDataGrid1.CurrentRow.Index;
                //    if (myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value != null)
                //    {
                //        itemid = myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                //         //templessvalue = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_less"].Value.ToString());
                //         //tempaddvalue = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_add"].Value.ToString());
                //        getbyid(itemid);

                //        myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
                //    }
                //    else
                //    {
                //        myDataGrid1.CurrentCell = myDataGrid1.Rows[rowsindex].Cells["Name"];
                //    }

                //}
                /////////////////////////////////
                //if (grd_stock.CurrentCell.ColumnIndex == 0)
                //{
                //    if (grd_stock.Rows[e.RowIndex].Cells["Item_code"].Value != null)
                //    {
                //        itemid = grd_stock.Rows[e.RowIndex].Cells["Item_code"].Value.ToString();
                //        getbyid(itemid);
                //    }
                //    else
                //    {
                //        grd_stock.CurrentCell = grd_stock.Rows[e.RowIndex].Cells["I_Name"];
                //    }
                //}
                //if (grd_stock.CurrentCell.ColumnIndex == 1)
                //{
                //    if (grd_stock.Rows[e.RowIndex].Cells["I_Name"].Value != null)
                //    {
                //        itementered = grd_stock.Rows[e.RowIndex].Cells["I_Name"].Value.ToString();
                //        fecthitemnamevalues();
                //    }
                //}
                //if (grd_stock.CurrentCell.ColumnIndex == 3)
                //{
                //    if (grd_stock.Rows[e.RowIndex].Cells["S_less"].Value != null)
                //    {
                //        grd_stock.Select();
                //        grd_stock.CurrentCell.Selected = true;
                //    }
                //    else
                //    {
                //        grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_add"].Value = 0;
                //        quantity = Convert.ToDouble(grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_less"].Value);
                //        rate = Convert.ToDouble(grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_rate"].Value);

                //        Double price = quantity * rate;
                //        grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_amt"].Value = price;
                //        total += price;
                //        lbl_amt.Text = total.ToString();
                //        grd_stock.CurrentCell = grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_amt"];
                //    }
                //}
                //if (grd_stock.CurrentCell.ColumnIndex == 4)
                //{
                //    //grd_stock.Select();
                //    //grd_stock.CurrentCell.Selected = true;

                //    if (grd_stock.Rows[e.RowIndex].Cells["S_add"].Value == null)
                //    {
                //        grd_stock.CurrentCell = grd_stock.Rows[e.RowIndex].Cells["S_add"];
                //        grd_stock.Rows[e.RowIndex].Cells["S_add"].Value = 0;
                //    }
                //    else
                //    {

                //        grd_stock.Rows[e.RowIndex].Cells["S_less"].Value = 0;
                //        quantity = Convert.ToDouble(grd_stock.Rows[e.RowIndex].Cells["S_add"].Value);
                //        rate = Convert.ToDouble(grd_stock.Rows[e.RowIndex].Cells["S_rate"].Value);

                //        Double price = quantity * rate;
                //        grd_stock.Rows[e.RowIndex].Cells["S_amt"].Value = price;
                //        total += price;
                //        lbl_amt.Text = total.ToString();
                //        grd_stock.CurrentCell = grd_stock.Rows[e.RowIndex].Cells["S_amt"];
                //    }


                //}
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

        }
        string unit = string.Empty;
        public void fecthitemnamevalues()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                SqlCommand namecmd = new SqlCommand("select Item_code,Item_mrsp,stock_type from Item_table where Item_name='" + itementered + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(namecmd);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                int i = 0;
                if (dt.Rows.Count > 0)
                {
                    i = 1;
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = dt.Rows[0]["Item_mrsp"].ToString();
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = dt.Rows[0]["Item_code"].ToString();
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Stock_Category"].Value = dt.Rows[0]["stock_type"].ToString();
                    myDataGrid1.MultiSelect = false;
                    //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
                    //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value = 0;
                    //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = 0;
                    //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = 0;
                }

                if (i == 1)
                {
                    SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table", con);
                    SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    dt2.Rows.Clear();
                    adp2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        int chkunit = 0;
                        for (int j = 0; j < dt2.Rows.Count; )
                        {
                            chkunit = 1;
                            unit = dt2.Rows[j]["unit_name"].ToString();
                            myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Unit"].Value = unit;
                            break;
                        }
                        if (chkunit == 1 && i == 1)
                        {
                            int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                            myDataGrid1.BeginInvoke(method, nextindex + 1);

                            //int nextindex1 = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                            //SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                            //myDataGrid1.BeginInvoke(method1, nextindex1 - 1);
                            //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Invalid Item Name", "Warning");
                    int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                    myDataGrid1.BeginInvoke(method, nextindex - 1);
                    //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex];
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        DataTable dt = new DataTable();
        string Unit_NO;
        public void getbyid(string id, string Name)
        {
            try
            {
                // pnl_item_name.Visible = true;
                //lst_itemname.Visible = true;
                int t_currentrow = myDataGrid1.CurrentRow.Index;
                SqlCommand cmd = new SqlCommand("select Item_name,Item_code,Unit_no,Item_mrsp,stock_type from Item_table where Item_code='" + id + "' or Item_name='" + Name + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                dt.Rows.Clear();
                adp.Fill(dt);
                int i = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; )
                    {
                        i = 1;
                        string name = dt.Rows[j]["Item_name"].ToString();

                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value = name.ToString();
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = dt.Rows[j]["Item_mrsp"].ToString();
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["stock_category"].Value = dt.Rows[j]["stock_type"].ToString();
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value = "0";
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = "0";
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = "0";

                        Unit_NO = dt.Rows[j]["Unit_no"].ToString();
                        break;
                    }
                }

                con.Close();

                if (i == 0)
                {
                    MyMessageBox.ShowBox("Item code not found in the list", "Warning");
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = tempCode.ToString();
                    //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"];
                    //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Selected = true;
                    //myDataGrid1.Select();
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table where unit_no='" + Unit_NO + "'", con);
                    SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    if (dt2.Rows.Count > 0)
                    {
                        int chkunit = 0;
                        for (int j = 0; j < dt2.Rows.Count; )
                        {
                            chkunit = 1;
                            string unit = dt2.Rows[j]["unit_name"].ToString();
                            myDataGrid1.Rows[rowsindex].Cells["Unit"].Value = unit;

                            break;
                        }
                        if (chkunit == 1 && i == 1)
                        {
                            int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                            myDataGrid1.BeginInvoke(method, 3);
                            //myDataGrid1.CurrentCell = myDataGrid1.Rows[rowsindex].Cells["Less_Qty"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void dbcheckforserial()
        {
            try
            {
                SqlDataAdapter adp = null;

                adp = new SqlDataAdapter("select Item_no from serialno_transtbl with (index(Index_serialno)) where Item_no='" + t1 + "' and inout = 1 ", con);
                adp.Fill(dt2_Check);
                if (dt2_Check.Rows.Count > 0)
                {
                    if (dt2_Check.Rows[0][0].ToString().Trim() != "")
                    {
                        MyMessageBox.ShowBox("This Serial No Already Exists in Database", "Warning");
                        dt2_Check.Rows.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void myDataGrid1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                {
                    myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = "0";
                }
            }
            if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value.ToString() == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value = "0";
                }
            }



            if (myDataGrid1.CurrentCell.ColumnIndex == 6)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() == "0" && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value.ToString() == "0")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = "0";
                }
            }
            //if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //{

            //    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "0" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0")
            //    {
            //        //MyMessageBox.ShowBox("Quantity field is empty");
            //        //myDataGrid1.Select();
            //        //myDataGrid1.CurrentCell.Selected = true;
            //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"];
            //        //myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
            //    }
            ////}
            //if (e.ColumnIndex == 1)
            //{
            //    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() == "" && myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString() == "")
            //    {
            //        txt_remarks.Focus();
            //        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex].Selected = false;
            //    }
            //    else
            //    {

            //    }
            //}

            //if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //{
            //    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value != null)
            //    {
            //        itementered = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            //        fecthitemnamevalues();
            //        //int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
            //        //SetColumnIndex method = new SetColumnIndex(Mymethod);
            //        //myDataGrid1.BeginInvoke(method, 5);
            //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex];
            //    }
            //    else
            //    {
            //        txt_remarks.Focus();
            //        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex].Selected = false;
            //    }
            //}

            //if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            //{
            //    ////if (templessvalue != Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_less"].Value.ToString()))
            //    ////{
            //    if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0" || myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0")
            //    {
            //        if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value == null)
            //        {
            //            int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
            //            SetColumnIndex method = new SetColumnIndex(Mymethod);
            //            myDataGrid1.BeginInvoke(method, 3);
            //            myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
            //        }
            //        else
            //        {
            //            if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "" && myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0")
            //            {
            //                MessageBox.Show("Cannot Entered Both Add and Less");
            //                myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
            //            }
            //            else
            //            {
            //                if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
            //                {
            //                    double total = 0;
            //                    quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
            //                    rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);
            //                    Double price = quantity * rate;
            //                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;

            //                    for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
            //                    {
            //                        if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
            //                        {
            //                            total += 0;
            //                        }
            //                        else
            //                        {
            //                            total += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
            //                        }
            //                    }

            //                    lbl_amt.Text = total + ".00";
            //                    int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
            //                    SetColumnIndex method = new SetColumnIndex(Mymethod);
            //                    myDataGrid1.BeginInvoke(method, 4);
            //                    myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
            //                }
            //                else
            //                {
            //                    myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
            //                    myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = 0;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        double total2 = 0;
            //        MessageBox.Show("Quantity field is empty");
            //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = 0;
            //        for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
            //        {
            //            if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
            //            {
            //                total2 += 0;
            //            }
            //            else
            //            {
            //                total2 += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
            //            }
            //        }
            //        lbl_amt.Text = total2 + ".00";
            //        myDataGrid1.Select();
            //        myDataGrid1.CurrentCell.Selected = true;
            //    }
            //}

            //if (e.ColumnIndex == 0)
            //{
            //    //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = tempCode.ToString();
            //}
        }
        //double quantity, rate, total;
        private void myDataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // double totQty = 0.00;
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //{
                //    int iRow = myDataGrid1.CurrentCell.RowIndex;
                //    double mn = 0.00;

                //    if (myDataGrid1.CurrentCell.ColumnIndex == 0)
                //    {
                //        if (myDataGrid1.Rows[grd_stock.CurrentCell.RowIndex].Cells["Item_code1"].Value == null)
                //        {
                //            grd_stock.CurrentCell = grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["I_name"];
                //        }
                //        else
                //        {
                //            string itemid1 = grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["Item_code1"].Value.ToString();
                //            getbyid(itemid1);
                //        }


                //    }
                //    else if (grd_stock.CurrentCell.ColumnIndex == 1)
                //    {
                //        if (grd_stock.Rows[iRow].Cells["I_Name"].Value != null && grd_stock.Rows[iRow].Cells["Item_Code"].Value != null)
                //        {

                //            grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["I_Name"];

                //        }
                //        else
                //        {
                //            txt_remarks.Focus();

                //        }


                //    }
                //    else if (grd_stock.CurrentCell.ColumnIndex == 2)
                //    {
                //        grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_less"];

                //    }
                //    else if (grd_stock.CurrentCell.ColumnIndex == 3)
                //    {

                //        if (grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_less"].Value == null)
                //        {
                //            grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_less"].Value = 0;
                //            grd_stock.CurrentCell = grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_add"];
                //        }
                //        else
                //        {
                //            quantity = Convert.ToDouble(grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_less"].Value);
                //            rate = Convert.ToDouble(grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_rate"].Value);

                //            Double price = quantity * rate;
                //            grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_amt"].Value = price;
                //            total += price;
                //            lbl_amt.Text = total + ".00";
                //            grd_stock.CurrentCell = grd_stock.Rows[grd_stock.CurrentCell.RowIndex].Cells["S_rate"];
                //        }

                //    }
                //    else if (grd_stock.CurrentCell.ColumnIndex == 4)
                //    {

                //        if (grd_stock.Rows[iRow].Cells["S_add"].Value != null)
                //        {
                //            grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_rate"];
                //        }
                //        else
                //        {
                //            string result = MyMessageBox.ShowBox("Empty Quantity", "Warning!");
                //            grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_add"];
                //        }

                //    }
                //    else if (grd_stock.CurrentCell.ColumnIndex == 5)
                //    {

                //        double j = 0;
                //        if (grd_stock.Rows[iRow].Cells["S_rate"] != null)
                //        {
                //            j = Convert.ToDouble(grd_stock.Rows[iRow].Cells["S_rate"].Value);
                //            if (grd_stock.Rows[iRow].Cells["S_less"].Value != null)
                //                //double amount_not=
                //                Convert.ToDouble(grd_stock.Rows[iRow].Cells["S_less"].Value);
                //            double add = Convert.ToDouble(grd_stock.Rows[iRow].Cells["S_less"].Value);
                //            double amount = 0.00;
                //            amount = (j * add);
                //            total += amount;
                //            lbl_amt.Text = total.ToString();
                //            grd_stock.Rows[iRow].Cells["S_amt"].Value = amount.ToString();

                //            grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_amt"];
                //        }
                //        if (grd_stock.Rows[iRow].Cells["S_add"].Value != null)
                //        {
                //            double add = Convert.ToDouble(grd_stock.Rows[iRow].Cells["S_add"].Value);
                //            double amount = 0.00;
                //            amount = (j * add);
                //            total += amount;
                //            lbl_amt.Text = total.ToString();
                //            grd_stock.Rows[iRow].Cells["S_amt"].Value = amount.ToString();
                //            double k = 0.00;
                //            grd_stock.Rows[iRow].Cells["S_less"].Value = k.ToString();
                //            grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_amt"];
                //        }
                //    }
                //    //   grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_amt"];
                //    else if (grd_stock.Rows[iRow].Cells["S_rate"].Value != null)
                //    {
                //        if (grd_stock.Rows[iRow].Cells["S_rate"].Value != null)
                //        {
                //            if (grd_stock.Rows[iRow].Cells["S_less"].Value == null)
                //            {
                //                double k = 0.00;
                //                grd_stock.Rows[iRow].Cells["S_less"].Value = k.ToString();
                //                grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_amt"];
                //            }
                //            if (grd_stock.Rows[iRow].Cells["S_add"].Value == null)
                //            {
                //                double k = 0.00;
                //                grd_stock.Rows[iRow].Cells["S_add"].Value = k.ToString();
                //                grd_stock.CurrentCell = grd_stock.Rows[iRow].Cells["S_amt"];
                //            }
                //        }

                int countofrow = myDataGrid1.Rows.Count;
                if (myDataGrid1.CurrentRow.Index == myDataGrid1.Rows.Count)
                {
                    myDataGrid1.Rows.Add();
                }
                else
                {
                    if (myDataGrid1.CurrentRow.Index == myDataGrid1.Rows.Count)
                    {
                        myDataGrid1.Rows.Add();
                        myDataGrid1.CurrentCell = myDataGrid1.Rows[countofrow + 1].Cells[0];
                    }
                }
            }
        }

        double Tax_amt, amt, Taxvalue;
        //, Profit;
        string value;
        int CounterNO;
        int PartyNo;
        // double ClosingQty, NetSalVal;
        string TaxValue;
        //  double TotalamtGrossamt = 0, BillAmtTotal = 0;
        // int oldrecordno;
        int Newid;
        //, StrnoNo;
        // int olditemno;
        string deletedRecNo;
        // bool tMessage;
        int getQty = 0;
        string altName = "";
        // int altQty = 0, altQty1 = 0,
        int tempQty = 0;
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {

                con.Close();
                int rowcount = myDataGrid1.Rows.Count;
                if (txt_comp_name.Text != "" || rowcount != 0)
                {
                    // Updation in Itemtable
                    if (dt11.Rows.Count > 0)
                    {
                        for (int pq = 0; pq < dt11.Rows.Count; pq++)
                        {
                            if (dt11.Rows[pq]["Add_Qty"].ToString() == "0")
                            {
                                // Get a old sales quantity:
                                double Old_L_Q = Convert.ToDouble(dt11.Rows[pq]["Less_Qty"].ToString());

                                con.Close();
                                con.Open();
                                string ItemNumqry = "select Item_no from Item_table where Item_name='" + dt11.Rows[pq]["Name"].ToString() + "'";
                                SqlCommand cmdItemNum = new SqlCommand(ItemNumqry, con);
                                int OldItemNO = Convert.ToInt16(cmdItemNum.ExecuteScalar());

                                string getSalQty = "select nt_salqty from Item_table where Item_no='" + OldItemNO + "' ";
                                SqlCommand cmdSalQty = new SqlCommand(getSalQty, con);
                                double OldSalQty = Convert.ToDouble(cmdSalQty.ExecuteScalar());

                                double Ca_L_Q = OldSalQty - Old_L_Q;

                                // Get a old sales value:
                                double Old_Sal_val = Convert.ToDouble(dt11.Rows[pq]["Amount"].ToString());

                                string getSalval = "select Nt_Salval from Item_table where Item_no='" + OldItemNO + "' ";
                                SqlCommand cmdSalval = new SqlCommand(getSalval, con);
                                double OldSalval = Convert.ToDouble(cmdSalval.ExecuteScalar());

                                double Ca_Sal_Val = OldSalval - Old_Sal_val;

                                // get a old closing quantity:

                                string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + OldItemNO + "' ";
                                SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                                double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                                double OldClosingQty = ClosingQty + Old_L_Q;

                                SqlCommand cmdUpdate = new SqlCommand("Update Item_table set nt_salqty=" + Ca_L_Q + ",nt_cloqty=" + OldClosingQty + ",Nt_Salval=" + Ca_Sal_Val + " where Item_no='" + OldItemNO + "' ", con);

                                cmdUpdate.ExecuteNonQuery();
                                con.Close();
                                //get a bill number from table:
                                string billNo = lbl_adjust_no.Text;
                                con.Open();
                                string getStrnoqry = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
                                SqlCommand cmddeleteRec = new SqlCommand(getStrnoqry, con);
                                deletedRecNo = cmddeleteRec.ExecuteScalar().ToString();
                                con.Close();
                                // delete the old record and insert the new grdiview values..
                                // update the alteration values.
                                con.Open();
                                string oldrecord = "select strn_sno,strn_type from stktrn_table where strn_no='" + deletedRecNo + "'";
                                SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                                    int oldstrtype = Convert.ToInt16(dt.Rows[i]["strn_type"].ToString());
                                    con.Close();
                                    //delete a Old Record in Stkrtn_table:
                                    con.Open();
                                    string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "' and strn_type='" + oldstrtype + "'";
                                    SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                    cmddeleteQry.ExecuteNonQuery();
                                    con.Close();

                                }
                            }

                            if (dt11.Rows[pq]["Less_Qty"].ToString() == "0")
                            {
                                // Get a old purchase quantity:
                                double Old_A_Q = Convert.ToDouble(dt11.Rows[pq]["Add_Qty"].ToString());

                                con.Close();
                                con.Open();
                                string ItemNumqry = "select Item_no from Item_table where Item_name='" + dt11.Rows[pq]["Name"].ToString() + "'";
                                SqlCommand cmdItemNum = new SqlCommand(ItemNumqry, con);
                                int OldItemNO = Convert.ToInt16(cmdItemNum.ExecuteScalar());

                                string getPurQty = "select nt_purqty from Item_table where Item_no='" + OldItemNO + "' ";
                                SqlCommand cmdPurQty = new SqlCommand(getPurQty, con);
                                double OldPurQty = Convert.ToDouble(cmdPurQty.ExecuteScalar());

                                double Ca_A_Q = OldPurQty - Old_A_Q;

                                // Get a old purchase value:
                                double Old_Pur_val = Convert.ToDouble(dt11.Rows[pq]["Amount"].ToString());

                                string getPurval = "select Nt_PurVal from Item_table where Item_no='" + OldItemNO + "' ";
                                SqlCommand cmdPurval = new SqlCommand(getPurval, con);
                                double OldPurval = Convert.ToDouble(cmdPurval.ExecuteScalar());

                                double Ca_Pur_Val = OldPurval - Old_Pur_val;

                                // Get a old closing quantity:                     
                                string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + OldItemNO + "' ";
                                SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                                double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                                double OldClosingQty = ClosingQty - Old_A_Q;

                                SqlCommand cmdUpdate = new SqlCommand("Update Item_table set nt_purqty=" + Ca_A_Q + ",nt_cloqty=" + OldClosingQty + ",Nt_PurVal=" + Ca_Pur_Val + " where Item_no='" + OldItemNO + "' ", con);
                                cmdUpdate.ExecuteNonQuery();
                                con.Close();

                                //get a bill number from table:
                                string billNo = lbl_adjust_no.Text;
                                con.Open();
                                string getStrnoqry = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
                                SqlCommand cmddeleteRec = new SqlCommand(getStrnoqry, con);
                                deletedRecNo = cmddeleteRec.ExecuteScalar().ToString();
                                con.Close();
                                // delete the old record and insert the new grdiview values..
                                // update the alteration values.
                                con.Open();
                                string oldrecord = "select strn_sno,strn_type from stktrn_table where strn_no='" + deletedRecNo + "'";
                                SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                                    int oldstrtype = Convert.ToInt16(dt.Rows[i]["strn_type"].ToString());
                                    con.Close();
                                    //delete a Old Record in Stkrtn_table:
                                    con.Open();
                                    string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "' and strn_type='" + oldstrtype + "'";
                                    SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                    cmddeleteQry.ExecuteNonQuery();

                                    // Beginning if number of serial number is zero                                  
                                    string mbarcode = "";
                                    mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                    SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                    cmdserial.ExecuteNonQuery();
                                    // Ending if number of serial number is zero
                                    con.Close();
                                }


                            }
                        }
                    }

                    for (int pq = 0; pq < myDataGrid1.Rows.Count - 1; pq++)
                    {
                        if (myDataGrid1.Rows[pq].Cells["Add_qty"].Value.ToString() == "0")
                        {
                            // Get a new sales value:
                            double New_L2_Q = Convert.ToDouble(myDataGrid1.Rows[pq].Cells["Less_Qty"].Value.ToString());
                            con.Close();
                            con.Open();
                            string ItemNoqry2 = "select Item_no from Item_table where Item_name='" + myDataGrid1.Rows[pq].Cells["Name"].Value.ToString() + "'";
                            SqlCommand cmdItemNo2 = new SqlCommand(ItemNoqry2, con);
                            int NewItemNO2 = Convert.ToInt16(cmdItemNo2.ExecuteScalar());

                            string getSalQty1 = "select nt_salqty from Item_table where Item_no='" + NewItemNO2 + "' ";
                            SqlCommand cmdSalQty1 = new SqlCommand(getSalQty1, con);
                            double NewSalQty = Convert.ToDouble(cmdSalQty1.ExecuteScalar());

                            double Ca_L1_Q = NewSalQty + New_L2_Q;

                            // Get a new sales value:
                            double New_Sal_val = Convert.ToDouble(myDataGrid1.Rows[pq].Cells["Amount"].Value.ToString());

                            string getSalval = "select Nt_Salval from Item_table where Item_no='" + NewItemNO2 + "' ";
                            SqlCommand cmdSalval = new SqlCommand(getSalval, con);
                            double NewSalval = Convert.ToDouble(cmdSalval.ExecuteScalar());

                            double Ca_Sal1_Val = NewSalval + New_Sal_val;

                            // get a new closing quantity:                     
                            string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + NewItemNO2 + "' ";
                            SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                            double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                            double NewClosingQty = ClosingQty - New_L2_Q;

                            SqlCommand cmdUpdate1 = new SqlCommand("Update Item_table set nt_salqty=" + Ca_L1_Q + ",nt_cloqty=" + NewClosingQty + ",Nt_Salval=" + Ca_Sal1_Val + " where Item_no='" + NewItemNO2 + "' ", con);
                            cmdUpdate1.ExecuteNonQuery();
                            con.Close();

                            if (dt11.Rows.Count == 0)
                            {
                                //get a bill number from table:
                                string billNo = lbl_adjust_no.Text;
                                con.Open();
                                string getStrnoqry = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
                                SqlCommand cmddeleteRec = new SqlCommand(getStrnoqry, con);
                                deletedRecNo = cmddeleteRec.ExecuteScalar().ToString();
                                con.Close();
                                // delete the old record and insert the new grdiview values..
                                // update the alteration values.
                                con.Open();
                                string oldrecord = "select strn_sno,strn_type from stktrn_table where strn_no='" + deletedRecNo + "'";
                                SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                                    int oldstrtype = Convert.ToInt16(dt.Rows[i]["strn_type"].ToString());
                                    con.Close();
                                    //delete a Old Record in Stkrtn_table:
                                    con.Open();
                                    string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "' and strn_type='" + oldstrtype + "'";
                                    SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                    cmddeleteQry.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }

                        //if (dt11.Rows[pq]["Less_Qty"].ToString() == "0")
                        //{
                        //    // Get a old purchase quantity:
                        //    double Old_A_Q = Convert.ToDouble(dt11.Rows[pq]["Add_Qty"].ToString());

                        //    con.Close();
                        //    con.Open();
                        //    string ItemNumqry = "select Item_no from Item_table where Item_code='" + dt11.Rows[pq]["Code"].ToString() + "' or Item_name='" + dt11.Rows[pq]["Name"].ToString() + "'";
                        //    SqlCommand cmdItemNum = new SqlCommand(ItemNumqry, con);
                        //    int OldItemNO = Convert.ToInt16(cmdItemNum.ExecuteScalar());

                        //    string getPurQty = "select nt_purqty from Item_table where Item_no='" + OldItemNO + "' ";
                        //    SqlCommand cmdPurQty = new SqlCommand(getPurQty, con);
                        //    double OldPurQty = Convert.ToDouble(cmdPurQty.ExecuteScalar());

                        //    double Ca_A_Q = OldPurQty - Old_A_Q;

                        //    // Get a old purchase value:
                        //    double Old_Pur_val = Convert.ToDouble(dt11.Rows[pq]["Amount"].ToString());

                        //    string getPurval = "select Nt_PurVal from Item_table where Item_no='" + OldItemNO + "' ";
                        //    SqlCommand cmdPurval = new SqlCommand(getPurval, con);
                        //    double OldPurval = Convert.ToDouble(cmdPurval.ExecuteScalar());

                        //    double Ca_Pur_Val = OldPurval - Old_Pur_val;

                        //    // Get a old closing quantity:                     
                        //    string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + OldItemNO + "' ";
                        //    SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                        //    double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                        //    double OldClosingQty = ClosingQty - Old_A_Q;

                        //    SqlCommand cmdUpdate = new SqlCommand("Update Item_table set nt_purqty=" + Ca_A_Q + ",nt_cloqty=" + OldClosingQty + ",Nt_PurVal=" + Ca_Pur_Val + " where Item_no='" + OldItemNO + "' ", con);
                        //    cmdUpdate.ExecuteNonQuery();
                        //    con.Close();
                        //}

                        if (myDataGrid1.Rows[pq].Cells["Less_qty"].Value.ToString() == "0")
                        {
                            // Get a new purchase quantity:
                            double New_A_Q = Convert.ToDouble(myDataGrid1.Rows[pq].Cells["Add_Qty"].Value.ToString());
                            con.Close();
                            con.Open();
                            string ItemNoqry1 = "select Item_no from Item_table where Item_name='" + myDataGrid1.Rows[pq].Cells["Name"].Value.ToString() + "'";
                            SqlCommand cmdItemNo1 = new SqlCommand(ItemNoqry1, con);
                            int NewItemNO = Convert.ToInt16(cmdItemNo1.ExecuteScalar());

                            string getPurQty = "select nt_purqty from Item_table where Item_no='" + NewItemNO + "' ";
                            SqlCommand cmdPurQty = new SqlCommand(getPurQty, con);
                            double NewPurQty = Convert.ToDouble(cmdPurQty.ExecuteScalar());

                            double Ca_A1_Q = NewPurQty + New_A_Q;

                            // Get a new purchase value:
                            double New_Pur_val = Convert.ToDouble(myDataGrid1.Rows[pq].Cells["Amount"].Value.ToString());

                            string getPurval = "select Nt_PurVal from Item_table where Item_no='" + NewItemNO + "' ";
                            SqlCommand cmdPurval = new SqlCommand(getPurval, con);
                            double NewPurval = Convert.ToDouble(cmdPurval.ExecuteScalar());

                            double Ca_Pur1_Val = NewPurval + New_Pur_val;

                            // Get a new closing quantity:                     
                            string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + NewItemNO + "' ";
                            SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                            double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                            double NewClosingQty = ClosingQty + New_A_Q;

                            SqlCommand cmdUpdate1 = new SqlCommand("Update Item_table set nt_purqty=" + Ca_A1_Q + ",nt_cloqty=" + NewClosingQty + ",Nt_PurVal=" + Ca_Pur1_Val + " where Item_no='" + NewItemNO + "' ", con);
                            cmdUpdate1.ExecuteNonQuery();
                            con.Close();

                            if (dt11.Rows.Count == 0)
                            {
                                //get a bill number from table:
                                string billNo = lbl_adjust_no.Text;
                                con.Open();
                                string getStrnoqry = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
                                SqlCommand cmddeleteRec = new SqlCommand(getStrnoqry, con);
                                deletedRecNo = cmddeleteRec.ExecuteScalar().ToString();
                                con.Close();
                                // delete the old record and insert the new grdiview values..
                                // update the alteration values.
                                con.Open();
                                string oldrecord = "select strn_sno,strn_type from stktrn_table where strn_no='" + deletedRecNo + "'";
                                SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                                SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                                DataTable dt = new DataTable();
                                adp.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    int oldrecordno = Convert.ToInt16(dt.Rows[i]["strn_sno"].ToString());
                                    int oldstrtype = Convert.ToInt16(dt.Rows[i]["strn_type"].ToString());
                                    con.Close();
                                    //delete a Old Record in Stkrtn_table:
                                    con.Open();
                                    string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "' and strn_type='" + oldstrtype + "'";
                                    SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                    cmddeleteQry.ExecuteNonQuery();
                                    con.Close();
                                }
                            }

                        }
                    }

                    ////get a bill number from table:
                    //string billNo = lbl_adjust_no.Text;
                    con.Close();
                    con.Open();
                    string getStrnoqry1 = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
                    SqlCommand cmddeleteRec1 = new SqlCommand(getStrnoqry1, con);
                    deletedRecNo = cmddeleteRec1.ExecuteScalar().ToString();
                    con.Close();
                    //// delete the old record and insert the new grdiview values..
                    //// update the alteration values.
                    //con.Open();
                    //string oldrecord = "select strn_sno from stktrn_table where strn_no='" + deletedRecNo + "'";
                    //SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
                    //SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
                    //DataTable dt = new DataTable();
                    //adp.Fill(dt);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    for (int i = 0; i < myDataGrid1.Rows.Count; i++)
                    {
                        if (myDataGrid1.Rows[i].Cells["Code"].Value.ToString() == "" && myDataGrid1.Rows[i].Cells["Name"].Value.ToString() == "")
                        {
                            //if (myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString() != "" || myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString() != "")
                            //{
                            MyMessageBox.ShowBox("Stock is Altered Successfully", "Success");

                            dt1.Rows.Clear();
                            dtNew.Rows.Clear();
                            //myDataGrid1.Focus();
                            lbl_amt.Text = "0";
                            //btn_save.BackColor = Color.Transparent;
                            txt_remarks.Text = "";
                            txt_comp_name.Text = "";
                            txt_countername.Text = "";
                            btn_save.Enabled = false;
                            con.Close();
                            con.Open();



                            string UpdateAdmasqry = "update adjmas_table set NetAmount='" + lbl_amt.Text + "' where Adj_No='" + deletedRecNo + "'";
                            SqlCommand cmdAdmasTotal = new SqlCommand(UpdateAdmasqry, con);
                            cmdAdmasTotal.ExecuteNonQuery();
                            con.Close();
                            break;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Quantity field is empty");
                            //}
                        }
                        else
                        {

                            if (myDataGrid1.Rows[i].Cells["Code"].Value.ToString() != "" || myDataGrid1.Rows[i].Cells["Name"].Value.ToString() != "")
                            {
                                if (myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString() != "0" || myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString() != "0")
                                {

                                    // ledger group no:
                                    con.Close();
                                    con.Open();
                                    string PartyNoqry = "select Ledger_no from Ledger_table where Ledger_name='" + txt_comp_name.Text + "'";
                                    SqlCommand cmdParty = new SqlCommand(PartyNoqry, con);
                                    PartyNo = Convert.ToInt16(cmdParty.ExecuteScalar());

                                    // Counter Number:                            
                                    string counterqry = "select ctr_no from counter_table where ctr_name='" + txt_countername.Text + "'";
                                    SqlCommand cmdCounter = new SqlCommand(counterqry, con);
                                    CounterNO = Convert.ToInt16(cmdCounter.ExecuteScalar());
                                    con.Close();

                                    // insert into stktrn_table:

                                    //foreach (DataGridViewRow row in myDataGrid1.Rows)
                                    //{
                                    //    if (!row.IsNewRow)
                                    //    {

                                    // get a Max value in Stktrn_table strn_sno:
                                    con.Close();
                                    con.Open();
                                    string newidqry = "select StrnSno+1 from NumberTable";
                                    SqlCommand cmdnewid = new SqlCommand(newidqry, con);
                                    Newid = Convert.ToInt16(cmdnewid.ExecuteScalar());
                                    //Newid=Newid + 1; 

                                    // update Number Table:                                
                                    string updateqry = "update NumberTable set StrnSno=StrnSno+1";
                                    SqlCommand cmdUpdateStrnSno = new SqlCommand(updateqry, con);
                                    cmdUpdateStrnSno.ExecuteNonQuery();

                                    // get a Item_Code number from Item_table:                                 
                                    string ItemNoqry = "select Item_no from Item_table where Item_name='" + myDataGrid1.Rows[i].Cells[1].Value + "'";
                                    SqlCommand cmdItemNo = new SqlCommand(ItemNoqry, con);
                                    int ItemNO = Convert.ToInt16(cmdItemNo.ExecuteScalar());

                                    // get a Tax_no number from Item_table:                                 
                                    string taxnoqry = "select Tax_no from Item_table where Item_name='" + myDataGrid1.Rows[i].Cells[1].Value + "'";
                                    SqlCommand cmdtaxno = new SqlCommand(taxnoqry, con);
                                    int TaxNo = Convert.ToInt16(cmdtaxno.ExecuteScalar());

                                    // get a taxName by Tax No from Tax_table:                                 
                                    string TaxnameQry = "select Nt_percent from Tax_table where Tax_no='" + TaxNo + "'";
                                    SqlCommand cmdtaxname = new SqlCommand(TaxnameQry, con);
                                    if (cmdtaxname.ExecuteScalar() != null)
                                    {
                                        TaxValue = cmdtaxname.ExecuteScalar().ToString();
                                    }
                                    else
                                    {
                                        TaxValue = "0.00";
                                    }

                                    //get a unitno from Item_name:                                 
                                    string ItemUnitqry = "select Unit_no from unit_table where unit_name='" + myDataGrid1.Rows[i].Cells[2].Value + "'";
                                    SqlCommand cmditemUnit = new SqlCommand(ItemUnitqry, con);
                                    string ItemUnit = cmditemUnit.ExecuteScalar().ToString();
                                    con.Close();
                                    //get a unitno from Item_name:
                                    //con.Open();
                                    //string unitnoqry = "select unit_name from unit_table where unit_name='" + ItemUnit + "'";
                                    //SqlCommand cmdunit = new SqlCommand(unitnoqry, con);
                                    //int Unitno = Convert.ToInt16(cmdunit.ExecuteScalar());
                                    //con.Close();

                                    //Tax Value Calculatuion:

                                    value = TaxValue;
                                    Taxvalue = Double.Parse(value);
                                    amt = Convert.ToDouble(myDataGrid1.Rows[i].Cells[5].Value);
                                    Tax_amt = amt * Taxvalue / 100;

                                    if (myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString() == "0")
                                    {

                                        SqlCommand cmd = new SqlCommand(@"INSERT INTO stktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,InvoiceNo,InvoiceDate)
                                                                       VALUES(@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8,@c9,@c10,@c11,@c12,@c13,@c14,@c15,@c16,@c17,@c18,@c19,@c20,@c21,@c22,@c23,@c25,@c26,@c27,@c28,@c29,@c30,@c31,@c32,@c33,@c34,@c35,@c36,@c37,@c38,@c39,@c40,@c41,@c42,@c43,@c44,@c45,@c46,@c47,@c48,@c49,@c50,@c51,@c52,@c53,@c54,@c55,@c56,@c57,@c58,@c59,@c60,@c61,@c62,@c63,@c64,@c65,@c66,@c67,@c68,@c69,@c70,@c71,@c72,@c73,@c74,@c75,@c76,@c77,@c78,@c79,@c80,@c81,@c82,@c83,@c84,@c85,@c86,@c87,@c88,@c89,@c90)", con);
                                        {
                                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C2", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C3", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C4", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C5", SqlDbType.DateTime));
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
                                            // cmd.Parameters.Add(new SqlParameter("@C24", SqlDbType.NVarChar));
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
                                            cmd.Parameters.Add(new SqlParameter("@C89", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C90", SqlDbType.Date));

                                        }
                                        con.Close();
                                        con.Open();


                                        cmd.Parameters["@C1"].Value = Newid;
                                        cmd.Parameters["@C2"].Value = deletedRecNo;
                                        cmd.Parameters["@C3"].Value = "0";
                                        cmd.Parameters["@C4"].Value = "11";          // give a type
                                        cmd.Parameters["@C5"].Value = txt_date.Text;
                                        cmd.Parameters["@C6"].Value = "0";          // godown billno
                                        cmd.Parameters["@C7"].Value = PartyNo;      // ledger group
                                        cmd.Parameters["@C8"].Value = "0";          // Grn_no
                                        cmd.Parameters["@C9"].Value = "0";          // Order_sno
                                        cmd.Parameters["@C10"].Value = "0";         // Dc_no
                                        cmd.Parameters["@C11"].Value = ItemNO;      // Item_code
                                        cmd.Parameters["@C12"].Value = CounterNO;   // Counter No
                                        cmd.Parameters["@C13"].Value = "2";         // Godown no   
                                        cmd.Parameters["@C14"].Value = ItemUnit;      // Unit No
                                        cmd.Parameters["@C15"].Value = "1";         // Unit ratio
                                        cmd.Parameters["@C16"].Value = "0";         // quantityPieces    
                                        cmd.Parameters["@C17"].Value = myDataGrid1.Rows[i].Cells[3].Value; // nt_quantity
                                        cmd.Parameters["@C18"].Value = "0";         //tx_qty
                                        cmd.Parameters["@C19"].Value = "0";         //short_qty
                                        cmd.Parameters["@C20"].Value = "0";         //rnt_qty
                                        cmd.Parameters["@C21"].Value = "0";         //rtx_qty
                                        cmd.Parameters["@C22"].Value = "0";         //invnt_qty
                                        cmd.Parameters["@C23"].Value = "0";         //invtx_qty
                                        //  cmd.Parameters["@C24"].Value = null;         //qty Datails
                                        cmd.Parameters["@C25"].Value = myDataGrid1.Rows[i].Cells[5].Value;  //rate
                                        cmd.Parameters["@C26"].Value = "0";           //taxrate
                                        cmd.Parameters["@C27"].Value = "0";             //currencyno
                                        cmd.Parameters["@C28"].Value = "0";             //currency val
                                        cmd.Parameters["@C29"].Value = myDataGrid1.Rows[i].Cells[6].Value;  //Amount
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
                                        cmd.Parameters["@C85"].Value = "0";                 //profit
                                        cmd.Parameters["@C86"].Value = "0";                 //itempoint
                                        cmd.Parameters["@C87"].Value = "0";                 //Mech no
                                        cmd.Parameters["@C88"].Value = "0";                 //Purrate.
                                        int tInnNo = 0;
                                        if (txt_inv_no.Text.Trim() == "")
                                        {
                                            tInnNo = 0;
                                        }
                                        else
                                        {
                                            tInnNo = int.Parse(txt_inv_no.Text);
                                        }
                                        cmd.Parameters["@C89"].Value = tInnNo;                 //InvoiceNO
                                        cmd.Parameters["@C90"].Value = dt_inv.Text;
                                        cmd.ExecuteNonQuery();


                                    }
                                    if (myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString() == "0")
                                    {

                                        SqlCommand cmd = new SqlCommand(@"INSERT INTO stktrn_table (strn_sno,strn_no,strn_rtno,strn_type,strn_date,Godown_BillNo,StrnParty_no,Grn_no,OrderSno,Dc_no,item_no,ctr_no,godown_no,Unit_no,Unit_Ratio,QtyInPieces,nt_qty,tx_qty,Short_qty,rnt_qty,rtx_qty,Invnt_qty,Invtx_qty,Rate,Tax_Rate,CurrencyNo,CurrencyValue,Amount,Tax_No,Disc_PerQty,Disc_Per,Disc_Amt,Adldisc_Per,Adldisc_Amt,Othdisc_Amt,OthPurdisc,Ed_PerQty,Ed_Per,Ed_Amt,Cess_Per,Cess_Amt,SHECess_Per,SHECess_Amt,HL_Per,HL_Amt,CST_per,CST_amt,tax_Flag,tax_per,tax_amt,Sur_per,Sur_amt,CommiPer,Commi,SmanPer,SmanAmt,spl_discamt,tot_amt,alp1,alp2,alp3,alp4,ala1,ala2,ala3,ala4,Net_Amt,Other_Exp,BillOther_Exp,strn_remarks,Strn_Cancel,Order_Ack,Cost,Mrsp,Margin,Margin_No,Srate,Frtx_Qty,RFrnt_Qty,RFrtx_Qty,Frnt_Qty,FreeQty,FreeItemNo,Profit,Item_Point,Mech_no,PurRate,InvoiceNo,InvoiceDate)
                                                                       VALUES(@c1,@c2,@c3,@c4,@c5,@c6,@c7,@c8,@c9,@c10,@c11,@c12,@c13,@c14,@c15,@c16,@c17,@c18,@c19,@c20,@c21,@c22,@c23,@c25,@c26,@c27,@c28,@c29,@c30,@c31,@c32,@c33,@c34,@c35,@c36,@c37,@c38,@c39,@c40,@c41,@c42,@c43,@c44,@c45,@c46,@c47,@c48,@c49,@c50,@c51,@c52,@c53,@c54,@c55,@c56,@c57,@c58,@c59,@c60,@c61,@c62,@c63,@c64,@c65,@c66,@c67,@c68,@c69,@c70,@c71,@c72,@c73,@c74,@c75,@c76,@c77,@c78,@c79,@c80,@c81,@c82,@c83,@c84,@c85,@c86,@c87,@c88,@c89,@c90)", con);
                                        {
                                            cmd.Parameters.Add(new SqlParameter("@C1", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C2", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C3", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C4", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C5", SqlDbType.DateTime));
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
                                            // cmd.Parameters.Add(new SqlParameter("@C24", SqlDbType.NVarChar));
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
                                            cmd.Parameters.Add(new SqlParameter("@C89", SqlDbType.Int));
                                            cmd.Parameters.Add(new SqlParameter("@C90", SqlDbType.Date));

                                        }
                                        con.Close();
                                        con.Open();


                                        cmd.Parameters["@C1"].Value = Newid;
                                        cmd.Parameters["@C2"].Value = deletedRecNo;
                                        cmd.Parameters["@C3"].Value = "0";
                                        cmd.Parameters["@C4"].Value = "12";          // give a type
                                        cmd.Parameters["@C5"].Value = txt_date.Text;
                                        cmd.Parameters["@C6"].Value = "0";          // godown billno
                                        cmd.Parameters["@C7"].Value = PartyNo;      // ledger group
                                        cmd.Parameters["@C8"].Value = "0";          // Grn_no
                                        cmd.Parameters["@C9"].Value = "0";          // Order_sno
                                        cmd.Parameters["@C10"].Value = "0";         // Dc_no
                                        cmd.Parameters["@C11"].Value = ItemNO;      // Item_code
                                        cmd.Parameters["@C12"].Value = CounterNO;   // Counter No
                                        cmd.Parameters["@C13"].Value = "2";         // Godown no   
                                        cmd.Parameters["@C14"].Value = ItemUnit;      // Unit No
                                        cmd.Parameters["@C15"].Value = "1";         // Unit ratio
                                        cmd.Parameters["@C16"].Value = "0";         // quantityPieces    
                                        cmd.Parameters["@C17"].Value = myDataGrid1.Rows[i].Cells["Add_Qty"].Value; // nt_quantity
                                        cmd.Parameters["@C18"].Value = "0";         //tx_qty
                                        cmd.Parameters["@C19"].Value = "0";         //short_qty
                                        cmd.Parameters["@C20"].Value = "0";         //rnt_qty
                                        cmd.Parameters["@C21"].Value = "0";         //rtx_qty
                                        cmd.Parameters["@C22"].Value = "0";         //invnt_qty
                                        cmd.Parameters["@C23"].Value = "0";         //invtx_qty
                                        //  cmd.Parameters["@C24"].Value = null;         //qty Datails
                                        cmd.Parameters["@C25"].Value = myDataGrid1.Rows[i].Cells["Rate"].Value;  //rate
                                        cmd.Parameters["@C26"].Value = "0";           //taxrate
                                        cmd.Parameters["@C27"].Value = "0";             //currencyno
                                        cmd.Parameters["@C28"].Value = "0";             //currency val
                                        cmd.Parameters["@C29"].Value = myDataGrid1.Rows[i].Cells["Amount"].Value;  //Amount
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
                                        cmd.Parameters["@C50"].Value = "0";                //tax_per
                                        cmd.Parameters["@C51"].Value = "0";             //Taxamt;
                                        cmd.Parameters["@C52"].Value = "0";                 //Sur_per
                                        cmd.Parameters["@C53"].Value = "0";                 //Sur_amt
                                        cmd.Parameters["@C54"].Value = "0";                 //CommiPer
                                        cmd.Parameters["@C55"].Value = "0";                 //Commi
                                        cmd.Parameters["@C56"].Value = "0";                 //Sman_per
                                        cmd.Parameters["@C57"].Value = "0";                 //Sman_amt
                                        cmd.Parameters["@C58"].Value = "0";                 //SpeclDiscAmt
                                        cmd.Parameters["@C59"].Value = myDataGrid1.Rows[i].Cells["Amount"].Value;        //Tot_amt
                                        cmd.Parameters["@C60"].Value = "0";                 //alp1    
                                        cmd.Parameters["@C61"].Value = "0";                 //alp2
                                        cmd.Parameters["@C62"].Value = "0";                 //alp3
                                        cmd.Parameters["@C63"].Value = "0";                 //alp4
                                        cmd.Parameters["@C64"].Value = "0";                 //ala1
                                        cmd.Parameters["@C65"].Value = "0";                 //ala2
                                        cmd.Parameters["@C66"].Value = "0";                 //ala3
                                        cmd.Parameters["@C67"].Value = "0";                 //ala4
                                        cmd.Parameters["@C68"].Value = myDataGrid1.Rows[i].Cells["Amount"].Value;       //nET_AMT
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
                                        cmd.Parameters["@C85"].Value = "0";                 //profit
                                        cmd.Parameters["@C86"].Value = "0";                 //itempoint
                                        cmd.Parameters["@C87"].Value = "0";                 //Mech no
                                        cmd.Parameters["@C88"].Value = "0";                 //Purrate.
                                        int tInnNo = 0;
                                        if (txt_inv_no.Text.Trim() == "")
                                        {
                                            tInnNo = 0;
                                        }
                                        else
                                        {
                                            tInnNo = int.Parse(txt_inv_no.Text);
                                        }
                                        cmd.Parameters["@C89"].Value = tInnNo;                 //InvoiceNO
                                        cmd.Parameters["@C90"].Value = dt_inv.Text;
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Beginning serial number deletion

                                    string mbarcode = "";
                                    for (int j = 0; j < myDataGridadjstock.Rows.Count - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGridadjstock.Rows[j].Cells["Serialitemcode"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGridadjstock.Rows[j].Cells["SerialNo"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid2.Rows.Count - (myDataGrid2.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid2.Rows[j].Cells["Serialitemcode2"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid2.Rows[j].Cells["SerialNo2"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid3.Rows.Count - (myDataGrid3.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid3.Rows[j].Cells["Serialitemcode3"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid3.Rows[j].Cells["SerialNo3"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid4.Rows.Count - (myDataGrid4.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid4.Rows[j].Cells["Serialitemcode4"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid4.Rows[j].Cells["SerialNo4"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid5.Rows.Count - (myDataGrid5.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid5.Rows[j].Cells["Serialitemcode5"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid5.Rows[j].Cells["SerialNo5"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid6.Rows.Count - (myDataGrid6.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid6.Rows[j].Cells["Serialitemcode6"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid6.Rows[j].Cells["SerialNo6"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid7.Rows.Count - (myDataGrid7.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid7.Rows[j].Cells["Serialitemcode7"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid7.Rows[j].Cells["SerialNo7"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid8.Rows.Count - (myDataGrid8.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid8.Rows[j].Cells["Serialitemcode8"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid8.Rows[j].Cells["SerialNo8"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid9.Rows.Count - (myDataGrid9.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid9.Rows[j].Cells["Serialitemcode9"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid9.Rows[j].Cells["SerialNo9"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid10.Rows.Count - (myDataGrid10.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid10.Rows[j].Cells["Serialitemcode10"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid10.Rows[j].Cells["SerialNo10"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid11.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid11.Rows[j].Cells["Serialitemcode11"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid11.Rows[j].Cells["SerialNo11"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid12.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid12.Rows[j].Cells["Serialitemcode12"].Value.ToString())
                                        {
                                            //SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid12.Rows[j].Cells["SerialNo12"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  pur_sal_ref_no =  '" + lbl_adjust_no.Text.Trim() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }



                                    // Ending Serial number deletion 

                                    // Beginning serial number insertion
                                    for (int j = 0; j < myDataGridadjstock.Rows.Count - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGridadjstock.Rows[j].Cells["Serialitemcode"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGridadjstock.Rows[j].Cells["SerialNo"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid2.Rows.Count - (myDataGrid2.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid2.Rows[j].Cells["Serialitemcode2"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid2.Rows[j].Cells["SerialNo2"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid3.Rows.Count - (myDataGrid3.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid3.Rows[j].Cells["Serialitemcode3"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid3.Rows[j].Cells["SerialNo3"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid4.Rows.Count - (myDataGrid4.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid4.Rows[j].Cells["Serialitemcode4"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid4.Rows[j].Cells["SerialNo4"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid5.Rows.Count - (myDataGrid5.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid5.Rows[j].Cells["Serialitemcode5"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid5.Rows[j].Cells["SerialNo5"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid6.Rows.Count - (myDataGrid6.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid6.Rows[j].Cells["Serialitemcode6"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid6.Rows[j].Cells["SerialNo6"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid7.Rows.Count - (myDataGrid7.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid7.Rows[j].Cells["Serialitemcode7"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid7.Rows[j].Cells["SerialNo7"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid8.Rows.Count - (myDataGrid8.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid8.Rows[j].Cells["Serialitemcode8"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid8.Rows[j].Cells["SerialNo8"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid9.Rows.Count - (myDataGrid9.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid9.Rows[j].Cells["Serialitemcode9"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid9.Rows[j].Cells["SerialNo9"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid10.Rows.Count - (myDataGrid10.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid10.Rows[j].Cells["Serialitemcode10"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid10.Rows[j].Cells["SerialNo10"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid11.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid11.Rows[j].Cells["Serialitemcode11"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid11.Rows[j].Cells["SerialNo11"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid12.Rows.Count - (myDataGrid12.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[i].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[i].Cells["code"].Value.ToString() == myDataGrid12.Rows[j].Cells["Serialitemcode12"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid12.Rows[j].Cells["SerialNo12"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    // Ending serial number insertion
                                }

                                //con.Close();
                                ////delete a Old Record in Stkrtn_table:
                                //con.Open();
                                //string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "'";
                                //SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                //cmddeleteQry.ExecuteNonQuery();
                                //con.Close();

                                //con.Close();
                                ////Profit amount:
                                //con.Open();
                                //string ItemCostQry = "select Item_cost from Item_table where Item_name='" + myDataGrid1.Rows[i].Cells[2].Value + "'";
                                //SqlCommand CmdItemCost = new SqlCommand(ItemCostQry, con);
                                //double ItemCost = Convert.ToDouble(CmdItemCost.ExecuteScalar());
                                //con.Close();
                                //double salesrate = Convert.ToDouble(myDataGrid1.Rows[i].Cells[4].Value);
                                //double Quantity = Convert.ToDouble(myDataGrid1.Rows[i].Cells[5].Value);
                                //Profit += (salesrate - ItemCost) * Quantity;

                            }
                            else
                            {
                                //con.Close();
                                ////delete a Old Record in Stkrtn_table:
                                //con.Open();
                                //string deleteOldRecord = "Delete from stktrn_table where strn_sno='" + oldrecordno + "'";
                                //SqlCommand cmddeleteQry = new SqlCommand(deleteOldRecord, con);
                                //cmddeleteQry.ExecuteNonQuery();
                                //con.Close();

                                //MessageBox.Show("Quantity field is empty");
                                //myDataGrid1.Select();
                                //myDataGrid1.CurrentCell.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("No transaction is Made", "Warning");
                    txt_date.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        public void amountupdate()
        {
            //string time = DateTime.Now.ToString("hh:mm:ss tt");
            // DateTime now = DateTime.Now;
            //TimeSpan time = now.TimeOfDay;
            ////string timeonly = time.ToString("hh:mm:ss tt");
            con.Close();
            con.Open();
            string bnoqry = "select max(Adj_No)+1 from adjmas_table";
            SqlCommand bno = new SqlCommand(bnoqry, con);
            string adj_no = bno.ExecuteScalar().ToString().Trim();
            if (adj_no == "")
            {
                adj_no = "1";
            }
            else
            {
                adj_no = bno.ExecuteScalar().ToString().Trim();
            }
            con.Close();

            con.Open();
            string adj_bilno = "select max(Adj_Billno)+1 from adjmas_table";
            SqlCommand abillno = new SqlCommand(adj_bilno, con);
            string adj_billno = abillno.ExecuteScalar().ToString().Trim();
            if (adj_billno == "")
            {
                adj_billno = "1";
            }
            else
            {
                adj_billno = bno.ExecuteScalar().ToString().Trim();
            }
            con.Close();

            con.Open();
            string c_no = "select ctr_no from counter_table where ctr_name='" + txt_countername.Text + "' ";
            SqlCommand c_noqry = new SqlCommand(c_no, con);
            int ctr_no = Convert.ToInt16(c_noqry.ExecuteScalar().ToString().Trim());
            con.Close();

            //DateTime time = DateTime.Now;
            //string dtime = time.ToShortTimeString();
            //string dtime = time.ToString("hh:mm:ss tt");
            //DateTime dt2= DateTime.ParseExact(dtime, "hh:mm:ss tt", null);
            double totalamt = 0;
            //if (myDataGrid1.Rows[0].Cells[6].Value != null)
            //{
            //    totalamt = float.Parse(myDataGrid1.Rows[0].Cells[6].Value.ToString().Trim());
            //}

            totalamt = Convert.ToDouble(lbl_amt.Text.ToString());
            // string date= txt_date.Text;

            con.Open();
            SqlCommand adj_mastbl = new SqlCommand("insert into adjmas_table(Adj_No,Adj_Billno,Adj_bill,Ctr_No,Adj_Date,Adj_Time,Godown_No,Togodown_No,Transfer_Type,UserNo,NetAmount) values('" + adj_no + "','" + adj_billno + "','" + adj_billno + "','" + ctr_no + "',@C2,@C3,'0','2','0','0','" + totalamt + "')", con);
            adj_mastbl.Parameters.Add(new SqlParameter("@C2", SqlDbType.DateTime));
            adj_mastbl.Parameters.Add(new SqlParameter("@C3", SqlDbType.DateTime));
            adj_mastbl.Parameters["@C2"].Value = txt_date.Text;
            adj_mastbl.Parameters["@C3"].Value = txt_date.Text;
            adj_mastbl.ExecuteNonQuery();
            con.Close();

        }
        public void qtyupdate()
        {
            for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
            {
                //get a oldpurchase qty:
                string iname = myDataGrid1.Rows[i].Cells["Name"].Value.ToString().Trim();
                con.Open();
                string oldpurqry = "select nt_purqty from Item_table where Item_name='" + iname + "'";
                SqlCommand oldpurcmd = new SqlCommand(oldpurqry, con);
                int oldpqty = Convert.ToInt16(oldpurcmd.ExecuteScalar().ToString().Trim());
                con.Close();

                //get a openqty
                con.Open();
                string openqty = "select nt_opnqty from Item_table where Item_name='" + iname + "'";
                SqlCommand cmdopnqty = new SqlCommand(openqty, con);
                int opnqty = Convert.ToInt16(cmdopnqty.ExecuteScalar().ToString().Trim());
                con.Close();

                //update of ntpurva:
                con.Open();
                string getntpurval = "select Nt_PurVal from Item_table where Item_name='" + iname + "'";
                SqlCommand getntpurvalqry = new SqlCommand(getntpurval, con);
                int ntpurval = Convert.ToInt32(getntpurvalqry.ExecuteScalar());
                con.Close();
                //get a old salval:

                con.Open();
                string getntsalval = "select Nt_Salval from Item_table where Item_name='" + iname + "'";
                SqlCommand getntsalvalqry = new SqlCommand(getntsalval, con);
                int newsalval = Convert.ToInt32(getntsalvalqry.ExecuteScalar());
                con.Close();

                //get a old sales qty:
                con.Open();
                string salnewqry = "select nt_salqty from Item_table where Item_name='" + iname + "'";
                SqlCommand sqcmd = new SqlCommand(salnewqry, con);
                int oldsqty = Convert.ToInt16(sqcmd.ExecuteScalar().ToString().Trim());
                con.Close();

                //Update a new purchase qty:
                int newsalqty = Convert.ToInt16(myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString().Trim()) + oldsqty;

                //check the purchase entry:
                if (myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString() == "0")
                {
                    Double stckaddval = Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().Trim()) + ntpurval;
                    con.Open();
                    string amtAddval = "update Item_table set Nt_PurVal=" + stckaddval + " where Item_name='" + iname + "'";
                    SqlCommand update_purval = new SqlCommand(amtAddval, con);
                    update_purval.ExecuteNonQuery();
                    con.Close();

                    //closing stock for addquantity:
                    int purclosestk = opnqty + oldpqty + Convert.ToInt16(myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString().Trim()) - oldsqty;

                    //get a add a new purchase qty:
                    int newpurqty = Convert.ToInt16(myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString().Trim()) + oldpqty;
                    con.Close();
                    con.Open();
                    string updatepurchase = "update Item_table set nt_purqty=" + newpurqty + ",nt_cloqty=" + purclosestk + " where Item_name='" + iname + "'";
                    SqlCommand pupdat = new SqlCommand(updatepurchase, con);
                    pupdat.ExecuteNonQuery();
                    con.Close();


                }


                if (myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString() == "0")
                {
                    Double stcklessval = Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().Trim()) + newsalval;
                    con.Open();
                    string amtlessval = "update Item_table set Nt_Salval=" + stcklessval + " where Item_name='" + iname + "'";
                    SqlCommand update_salval = new SqlCommand(amtlessval, con);
                    update_salval.ExecuteNonQuery();
                    con.Close();

                    //get a add a new sales qty:
                    int newsalsqty = Convert.ToInt16(myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString().Trim());
                    //get a openqty
                    con.Open();
                    string salqty = "select nt_salqty from Item_table where Item_name='" + iname + "'";
                    SqlCommand cmdsalqty = new SqlCommand(salqty, con);
                    int ntsalqty = Convert.ToInt32(cmdsalqty.ExecuteScalar().ToString().Trim()) + newsalsqty;
                    con.Close();


                    //get a sales closstck
                    con.Open();
                    string saleclosqty = "select nt_cloqty from Item_table where Item_name='" + iname + "'";
                    SqlCommand cmdclosqty = new SqlCommand(saleclosqty, con);
                    int clsqty = Convert.ToInt32(cmdclosqty.ExecuteScalar().ToString().Trim());
                    con.Close();

                    //closing stock for lessquantity:

                    int salclosestk = clsqty - newsalsqty;

                    //UPDATE OF CLOSING STOCK:
                    con.Open();
                    string updatepurchase = "update Item_table set nt_salqty=" + ntsalqty + ",nt_cloqty=" + salclosestk + " where Item_name='" + iname + "'";
                    SqlCommand pupdat = new SqlCommand(updatepurchase, con);
                    pupdat.ExecuteNonQuery();
                    con.Close();

                }
            }
        }

        private void myDataGrid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (myDataGrid1.CurrentCell.ColumnIndex == 0) //Item_code
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
            if (this.myDataGrid1.CurrentCell.ColumnIndex == this.myDataGrid1.Columns["Name"].Index) //Item_name
            {
                string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                te.AutoCompleteCustomSource.AddRange(postSource);
                te.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
            }
            if (myDataGrid1.CurrentCell.ColumnIndex == 3) //Less Qty
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }

            }
            if (myDataGrid1.CurrentCell.ColumnIndex == 4) //Add Qty
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
            if (myDataGrid1.CurrentCell.ColumnIndex == 5) //Less Qty
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
            if (this.myDataGrid1.CurrentCell.ColumnIndex == this.myDataGrid1.Columns["Code"].Index) //Item_code
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                te.AutoCompleteSource = AutoCompleteSource.None;

            }
            if (this.myDataGrid1.CurrentCell.ColumnIndex == this.myDataGrid1.Columns["Less_Qty"].Index) //less Qty
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                te.AutoCompleteSource = AutoCompleteSource.None;

            }
            if (this.myDataGrid1.CurrentCell.ColumnIndex == this.myDataGrid1.Columns["Add_Qty"].Index) //Add Qty
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                te.AutoCompleteSource = AutoCompleteSource.None;

            }

        }
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        string chk;
        private void txt_countername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_countername.Text.Trim() != null && ChckCondition == "")
                {
                    if (txt_countername.Text.Trim() != "")
                    {
                        //pnl_ctrname.Visible = true;

                        SqlCommand cmd = new SqlCommand("select ctr_name from counter_table where ctr_name like '" + txt_countername.Text.Trim() + "%'", con);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        adp.Fill(dt);
                        bool isChk = false;
                        for (int m = 0; m < dt.Rows.Count; )
                        {
                            isChk = true;
                            string tempStr = dt.Rows[m]["ctr_name"].ToString();
                            for (int i = 0; i < lst_ctrname.Items.Count; i++)
                            {
                                if (dt.Rows[m]["ctr_name"].ToString() == lst_ctrname.Items[i].ToString())
                                {
                                    lst_ctrname.SetSelected(i, true);
                                    txt_countername.Select();
                                    chk = "1";
                                    txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                                    break;
                                }
                            }
                            break;
                        }

                        //            if (isChk == false)
                        //            {
                        //                chk = "2";
                        //                txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        chk = "1";
                        //    }
                        //}
                        if (isChk == false)
                        {
                            chk = "2";
                            if (txt_countername.Text != "")
                            {
                                string name = txt_countername.Text.Remove(txt_countername.Text.Length - 1);
                                txt_countername.Text = name.ToString();
                                txt_countername.Select(txt_countername.Text.Length, 0);
                            }
                            txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            chk = "1";
                            txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                        }
                        else
                        {
                            chk = "1";
                        }
                        txtName_TextChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txt_countername_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ChckCondition))
            {
                txt_countername.Text = "";
                pnl_comp_name.Visible = false;
                lst_compname.Visible = false;
                //txt_countername.SelectAll();
                countload();
                pnl_ctrname.Visible = true;
                lst_ctrname.Visible = true;
            }
        }

        //string chk1;
        private void txt_comp_name_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_comp_name.Text.Trim() != null)
                {
                    if (txt_comp_name.Text.Trim() != "")
                    {
                        //pnl_ctrname.Visible = true;
                        SqlCommand cmd = new SqlCommand("select Ledger_name from Ledger_table where Ledger_name like '" + txt_comp_name.Text.Trim() + "%'", con);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        adp.Fill(dt);
                        bool isChk = false;
                        for (int m = 0; m < dt.Rows.Count; )
                        {
                            isChk = true;
                            string tempStr = dt.Rows[m]["Ledger_name"].ToString();
                            for (int i = 0; i < lst_compname.Items.Count; i++)
                            {
                                if (dt.Rows[m]["Ledger_name"].ToString() == lst_compname.Items[i].ToString())
                                {
                                    lst_compname.SetSelected(i, true);
                                    txt_comp_name.Select();
                                    chk = "1";
                                    txt_comp_name.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                                    break;
                                }
                            }
                            break;
                        }
                        //        if (isChk == false)
                        //        {
                        //            chk = "2";
                        //            txt_comp_name.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    chk = "1";
                        //}
                        if (isChk == false)
                        {
                            chk = "2";
                            if (txt_comp_name.Text != "")
                            {
                                string name = txt_comp_name.Text.Remove(txt_comp_name.Text.Length - 1);
                                txt_comp_name.Text = name.ToString();
                                txt_comp_name.Select(txt_comp_name.Text.Length, 0);
                            }
                            txt_comp_name.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            chk = "1";
                            txt_comp_name.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                        }
                        else
                        {
                            chk = "1";
                        }
                        txtName_TextChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int curPOS = txt.SelectionStart;
            txt.Text = UppercaseWords(txt.Text);
            txt.Select(curPOS, 0);
        }
        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
        DataTable dt1 = new DataTable();
        public void countload()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select ctr_name from counter_table order by ctr_name ASC", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt1.Rows.Clear();
                lst_ctrname.Items.Clear();
                adp.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        lst_ctrname.Items.Add(dt1.Rows[i]["ctr_name"].ToString());
                        //this.lst_ctrname.SelectedIndex = 0;
                    }
                }
                adp.Dispose();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txt_comp_name_Enter(object sender, EventArgs e)
        {
            //txt_comp_name.Text = "";
            pnl_ctrname.Visible = false;
            lst_ctrname.Visible = false;
            //txt_countername.SelectAll();
            compload();
            pnl_comp_name.Visible = true;
            lst_compname.Visible = true;
            SqlCommand cmd = new SqlCommand("select Ledger_name from Ledger_table where Ledger_gno='201'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adp.Fill(dt);

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (txt_comp_name.Text.Trim() == dt.Rows[m]["Ledger_name"].ToString())
                {
                    lst_compname.SelectedIndex = (m);
                }
            }
        }
        public void compload()
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("select Ledger_name from Ledger_table where Ledger_gno='201'", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable companydt = new DataTable();
                companydt.Rows.Clear();

                lst_compname.Items.Clear();
                adp.Fill(companydt);
                if (companydt.Rows.Count > 0)
                {
                    for (int i = 0; i < companydt.Rows.Count; i++)
                    {
                        lst_compname.Items.Add(companydt.Rows[i]["Ledger_name"].ToString());
                        // txt_comp_name.Text = (companydt.Rows[0]["Ledger_name"].ToString());
                        //this.lst_compname.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void myDataGrid1_Leave(object sender, EventArgs e)
        {
            //if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex].Value.ToString() == "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex].Value.ToString() == "")
            //{
            //   // myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex].Selected = false;
            //}
        }

        private void txt_remarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save.Select();
                btn_save.BackColor = Color.LightBlue;
            }
        }

        private void btn_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save_Click(sender, e);
            }
        }
        string ChckCondition = "";
        private void lst_ctrname_Click(object sender, EventArgs e)
        {
            if (lst_ctrname.Items.Count > 0)
            {
                ChckCondition = "NotEnter";
                txt_countername.Text = lst_ctrname.SelectedItem.ToString();
                txt_countername.Focus();
                ChckCondition = "";
            }
            else { }
        }

        private void lst_compname_Click(object sender, EventArgs e)
        {
            if (lst_compname.Items.Count > 0)
            {
                txt_comp_name.Text = lst_compname.SelectedItem.ToString();
                if (myDataGrid1.Rows.Count > 0)
                {
                    pnl_comp_name.Visible = false;
                    myDataGrid1.Focus();
                }
                else
                {
                    txt_comp_name.Focus();
                }
            }
            else
            { }
        }

        private void myDataGrid1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        public void nextcell()
        {
            if (this.myDataGrid1.CurrentCell.ColumnIndex != this.myDataGrid1.Columns.Count - 1)
            {
                int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
                SetColumnIndex method = new SetColumnIndex(Mymethod);
                this.myDataGrid1.BeginInvoke(method, nextindex + 3);
            }
        }

        DataTable dtName = new DataTable();
        public void fecthitemnamevalues(string itemname)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            SqlCommand namecmd = new SqlCommand("select Item_code,Item_mrsp from Item_table where Item_name='" + itemname + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(namecmd);

            dtName.Rows.Clear();
            adp.Fill(dtName);

            int i = 0;
            if (dtName.Rows.Count > 0)
            {
                for (int j = 0; j < dtName.Rows.Count; j++)
                {
                    i = 1;
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = dtName.Rows[j]["Item_mrsp"].ToString();
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = dtName.Rows[j]["Item_code"].ToString();
                    int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                    myDataGrid1.BeginInvoke(method, 3);
                }
            }

            if (i == 1)
            {
                SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table", con);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd2);
                DataTable dt1 = new DataTable();
                dt1.Rows.Clear();
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    int chkunit = 0;
                    for (int j = 0; j < dt1.Rows.Count; )
                    {
                        chkunit = 1;
                        unit = dt1.Rows[j]["unit_name"].ToString();
                        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Unit"].Value = unit;
                        break;
                    }

                    if (chkunit == 1 && i == 1)
                    {
                        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                        SetColumnIndex method = new SetColumnIndex(Mymethod);
                        myDataGrid1.BeginInvoke(method, 3);
                    }
                }
            }
            else
            {
                MyMessageBox.ShowBox("Invalid Item Name", "Warning");
                int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                SetColumnIndex method = new SetColumnIndex(Mymethod);
                myDataGrid1.BeginInvoke(method, nextindex - 1);
                //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells[myDataGrid1.CurrentCell.ColumnIndex];
            }
        }

        double amount = 0;
        private void myDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 0)
                {
                    string itemcode = "", itemName = "";
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value != null)
                    {
                        if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString() != "")
                        {
                            itemcode = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString();
                            itemName = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value.ToString();
                            getbyid(itemcode, itemName);
                            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    nextcell();
                                }
                                else
                                {
                                    MyMessageBox1.ShowBox("Code Not Found", "Warning");
                                    int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                                    this.myDataGrid1.BeginInvoke(method, nextindex - 1);
                                }
                            }
                            else
                            {
                                //MyMessageBox1.ShowBox("Please Enter Correct ItemCode", "Warning");
                                //previouscell();  
                                // myDataGrid1.Focus();
                            }
                        }
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 1)
                {
                    string itemname = "";
                    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value != null)
                    {
                        if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() != "")
                        {
                            string t1 = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                            int t2 = e.RowIndex;
                            for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
                            {
                                if (t2 != j)
                                {

                                    if (t1 == myDataGrid1.Rows[j].Cells["Name"].Value.ToString())
                                    {

                                        MyMessageBox.ShowBox("Selected item is already entered", "Message");
                                        break;
                                    }

                                }
                            }
                        }

                        itemname = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        fecthitemnamevalues(itemname);
                        if (itemname != null)
                        {
                            if (dtName.Rows.Count > 0)
                            {
                                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value.ToString() != "")
                                {
                                    int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                                    this.myDataGrid1.BeginInvoke(method, nextindex + 1);
                                }
                            }
                            else
                            {
                                MyMessageBox1.ShowBox("Please Enter Correct Name or Code", "Warning");
                                //int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
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

            else if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 3)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value)));

                        if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                            {
                                if (lbl_amt.Text == "")
                                {
                                    lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().ToString()));
                                }
                                else
                                {
                                    if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    {
                                        amount += double.Parse(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(amount.ToString()));
                            }
                            amount = 0;
                        }

                    }
                    else
                    {
                        MyMessageBox.ShowBox("Cannot Enter Both Add and Less");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value = "0";
                    }
                }

                double ini_0 = 1, ini2 = 1;
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value.ToString() == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
                    ini_0 = 0;

                }
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value = "0";
                    ini2 = 0;
                }
                if (ini_0 != 1 || ini2 != 1)
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";

                }
            }

            else if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 4)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "")
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                    {
                        rowno = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);
                        string testvar = "";
                        testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();

                        if (Convert.ToInt32(testvar.ToString()) == 1)
                        {
                            //Beginning First Row    
                            if (rowno == 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGridadjstock.Visible = true;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                myDataGridadjstock.AllowUserToAddRows = false;
                                //this.myDataGridopstock.DefaultCellStyle.ForeColor = Color.Black;                            
                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart = loopend;

                                if (loopstart != 0)
                                {
                                    loopstart = loopend;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend = addqty;

                                    if (loopend < loopstart)
                                    {
                                        loopstart = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGridadjstockrowscount = myDataGridadjstock.Rows.Count;
                                        for (int p = myDataGridadjstockrowscount - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGridadjstock.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend = addqty;
                                }

                                for (int Z = loopstart; Z < loopend; Z++)
                                {
                                    myDataGridadjstock.Rows.Add();
                                    myDataGridadjstock.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGridadjstock.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }

                            }

                            // Ending First Row

                            //Beginning Second Row    
                            if (rowno == 1)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid2.AllowUserToAddRows = false;
                                myDataGrid2.Visible = true;
                                myDataGridadjstock.Visible = true;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart2 = loopend2;

                                if (loopstart2 != 0)
                                {
                                    loopstart2 = loopend2;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend2 = addqty;

                                    if (loopend2 < loopstart2)
                                    {
                                        loopstart2 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid2rowscount = myDataGrid2.Rows.Count;
                                        for (int p = myDataGrid2rowscount - (myDataGrid2.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid2.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend2 = addqty;
                                }

                                for (int Z = loopstart2; Z < loopend2; Z++)
                                {
                                    myDataGrid2.Rows.Add();
                                    myDataGrid2.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend2 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid2.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Second Row

                            //Beginning Third Row    

                            if (rowno == 2)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid3.AllowUserToAddRows = false;
                                myDataGrid3.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart3 = loopend3;

                                if (loopstart3 != 0)
                                {
                                    loopstart3 = loopend3;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend3 = addqty;

                                    if (loopend3 < loopstart3)
                                    {
                                        loopstart3 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid3rowscount = myDataGrid3.Rows.Count;
                                        for (int p = myDataGrid3rowscount - (myDataGrid3.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid3.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend3 = addqty;
                                }

                                for (int Z = loopstart3; Z < loopend3; Z++)
                                {
                                    myDataGrid3.Rows.Add();
                                    myDataGrid3.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend3 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid3.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Third Row

                            //Beginning Fourth Row    
                            if (rowno == 3)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid4.AllowUserToAddRows = false;
                                myDataGrid4.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart4 = loopend4;

                                if (loopstart4 != 0)
                                {
                                    loopstart4 = loopend4;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend4 = addqty;

                                    if (loopend4 < loopstart4)
                                    {
                                        loopstart4 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid4rowscount = myDataGrid4.Rows.Count;
                                        for (int p = myDataGrid4rowscount - (myDataGrid4.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid4.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend4 = addqty;
                                }

                                for (int Z = loopstart4; Z < loopend4; Z++)
                                {
                                    myDataGrid4.Rows.Add();
                                    myDataGrid4.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend4 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid4.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Fourth Row

                            //Beginning Fifth Row    
                            if (rowno == 4)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid5.AllowUserToAddRows = false;
                                myDataGrid5.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart5 = loopend5;

                                if (loopstart5 != 0)
                                {
                                    loopstart5 = loopend5;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend5 = addqty;

                                    if (loopend5 < loopstart5)
                                    {
                                        loopstart5 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid5rowscount = myDataGrid5.Rows.Count;
                                        for (int p = myDataGrid5rowscount - (myDataGrid5.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid5.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend5 = addqty;
                                }

                                for (int Z = loopstart5; Z < loopend5; Z++)
                                {
                                    myDataGrid5.Rows.Add();
                                    myDataGrid5.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend5 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid5.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Fifth Row

                            //Beginning Sixth Row    
                            if (rowno == 5)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid6.AllowUserToAddRows = false;
                                myDataGrid6.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart6 = loopend6;

                                if (loopstart6 != 0)
                                {
                                    loopstart6 = loopend6;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend6 = addqty;

                                    if (loopend6 < loopstart6)
                                    {
                                        loopstart6 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid6rowscount = myDataGrid6.Rows.Count;
                                        for (int p = myDataGrid6rowscount - (myDataGrid6.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid6.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend6 = addqty;
                                }

                                for (int Z = loopstart6; Z < loopend6; Z++)
                                {
                                    myDataGrid6.Rows.Add();
                                    myDataGrid6.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend6 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid6.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Sixth Row

                            //Beginning Seventh Row    
                            if (rowno == 6)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid7.AllowUserToAddRows = false;
                                myDataGrid7.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart7 = loopend7;

                                if (loopstart7 != 0)
                                {
                                    loopstart7 = loopend7;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend7 = addqty;

                                    if (loopend7 < loopstart7)
                                    {
                                        loopstart7 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid7rowscount = myDataGrid7.Rows.Count;
                                        for (int p = myDataGrid7rowscount - (myDataGrid7.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid7.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend7 = addqty;
                                }

                                for (int Z = loopstart7; Z < loopend7; Z++)
                                {
                                    myDataGrid7.Rows.Add();
                                    myDataGrid7.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend7 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid7.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Seventh Row

                            //Beginning Eighth Row    
                            if (rowno == 7)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid8.AllowUserToAddRows = false;
                                myDataGrid8.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart8 = loopend8;

                                if (loopstart8 != 0)
                                {
                                    loopstart8 = loopend8;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend8 = addqty;

                                    if (loopend8 < loopstart8)
                                    {
                                        loopstart8 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid8rowscount = myDataGrid8.Rows.Count;
                                        for (int p = myDataGrid8rowscount - (myDataGrid8.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid8.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend8 = addqty;
                                }

                                for (int Z = loopstart8; Z < loopend8; Z++)
                                {
                                    myDataGrid8.Rows.Add();
                                    myDataGrid8.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend8 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid8.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Eighth Row

                            //Beginning Nineth Row    
                            if (rowno == 8)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid9.AllowUserToAddRows = false;
                                myDataGrid9.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart9 = loopend9;

                                if (loopstart9 != 0)
                                {
                                    loopstart9 = loopend9;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend9 = addqty;

                                    if (loopend9 < loopstart9)
                                    {
                                        loopstart9 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid9rowscount = myDataGrid9.Rows.Count;
                                        for (int p = myDataGrid9rowscount - (myDataGrid9.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid9.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend9 = addqty;
                                }

                                for (int Z = loopstart9; Z < loopend9; Z++)
                                {
                                    myDataGrid9.Rows.Add();
                                    myDataGrid9.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend9 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid9.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Nineth Row

                            //Beginning Tenth Row    
                            if (rowno == 9)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid10.AllowUserToAddRows = false;
                                myDataGrid10.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid11.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart10 = loopend10;

                                if (loopstart10 != 0)
                                {
                                    loopstart10 = loopend10;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend10 = addqty;

                                    if (loopend10 < loopstart10)
                                    {
                                        loopstart10 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid10rowscount = myDataGrid10.Rows.Count;
                                        for (int p = myDataGrid10rowscount - (myDataGrid10.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid10.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend10 = addqty;
                                }

                                for (int Z = loopstart10; Z < loopend10; Z++)
                                {
                                    myDataGrid10.Rows.Add();
                                    myDataGrid10.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend10 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid10.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Tenth Row

                            //Beginning Eleventh Row    
                            if (rowno == 10)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid11.AllowUserToAddRows = false;
                                myDataGrid11.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid12.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart11 = loopend11;

                                if (loopstart11 != 0)
                                {
                                    loopstart11 = loopend11;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend11 = addqty;

                                    if (loopend11 < loopstart11)
                                    {
                                        loopstart11 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid11rowscount = myDataGrid11.Rows.Count;
                                        for (int p = myDataGrid11rowscount - (myDataGrid11.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid11.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend11 = addqty;
                                }

                                for (int Z = loopstart11; Z < loopend11; Z++)
                                {
                                    myDataGrid11.Rows.Add();
                                    myDataGrid11.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend11 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid11.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                            // Ending Eleventh Row

                            //Beginning Twelth Row    
                            if (rowno == 11)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid12.AllowUserToAddRows = false;
                                myDataGrid12.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;
                                myDataGrid9.Visible = false;
                                myDataGrid10.Visible = false;
                                myDataGrid11.Visible = false;

                                int addqty = 0;
                                addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                loopstart12 = loopend12;

                                if (loopstart12 != 0)
                                {
                                    loopstart12 = loopend12;
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend12 = addqty;

                                    if (loopend12 < loopstart12)
                                    {
                                        loopstart12 = 0;
                                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                                        int myDataGrid12rowscount = myDataGrid12.Rows.Count;
                                        for (int p = myDataGrid12rowscount - (myDataGrid12.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid12.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    addqty = Convert.ToInt32(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString());
                                    loopend12 = addqty;
                                }

                                for (int Z = loopstart12; Z < loopend12; Z++)
                                {
                                    myDataGrid12.Rows.Add();
                                    myDataGrid12.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }

                                DataTable datatableserial = new DataTable();
                                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                                datatableserial.Rows.Clear();
                                adpumas.Fill(datatableserial);

                                if (loopend12 >= datatableserial.Rows.Count)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid12.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                    }
                                }
                            }
                        }
                        // Ending Twelth Row                         


                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value)));

                        if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                            {

                                if (lbl_amt.Text == "")
                                {
                                    lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().ToString()));
                                }
                                else
                                {
                                    if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    {
                                        amount += double.Parse(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());
                                    }
                                }
                                lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(amount.ToString()));
                            }
                            amount = 0;
                        }

                    }


                    else
                    {
                        MyMessageBox.ShowBox("Cannot Enter Both Add and Less", "Warning");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = "0";
                    }
                }
                //else if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "0"&&myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" )
                //{
                //    string result = MyMessageBox.ShowBox("Empty Quantity", "Warning!");
                //    myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"];
                ////}

                // if Add_qty is zero
                else
                {
                    rowno = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);

                    if (rowno == 0)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart = 0;
                        loopend = 0;
                        int myDataGridadjstockrowscount = myDataGridadjstock.Rows.Count;
                        for (int p = myDataGridadjstockrowscount - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGridadjstock.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 1)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart2 = 0;
                        loopend2 = 0;
                        int myDataGrid2rowscount = myDataGrid2.Rows.Count;
                        for (int p = myDataGrid2rowscount - (myDataGrid2.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid2.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 2)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart3 = 0;
                        loopend3 = 0;
                        int myDataGrid3rowscount = myDataGrid3.Rows.Count;
                        for (int p = myDataGrid3rowscount - (myDataGrid3.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid3.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 3)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart4 = 0;
                        loopend4 = 0;
                        int myDataGrid4rowscount = myDataGrid4.Rows.Count;
                        for (int p = myDataGrid4rowscount - (myDataGrid4.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid4.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 4)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart5 = 0;
                        loopend5 = 0;
                        int myDataGrid5rowscount = myDataGrid5.Rows.Count;
                        for (int p = myDataGrid5rowscount - (myDataGrid5.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid5.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 5)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart6 = 0;
                        loopend6 = 0;
                        int myDataGrid6rowscount = myDataGrid6.Rows.Count;
                        for (int p = myDataGrid6rowscount - (myDataGrid6.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid6.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 6)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart7 = 0;
                        loopend7 = 0;
                        int myDataGrid7rowscount = myDataGrid7.Rows.Count;
                        for (int p = myDataGrid7rowscount - (myDataGrid7.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid7.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 7)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart8 = 0;
                        loopend8 = 0;
                        int myDataGrid8rowscount = myDataGrid8.Rows.Count;
                        for (int p = myDataGrid8rowscount - (myDataGrid8.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid8.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 8)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart9 = 0;
                        loopend9 = 0;
                        int myDataGrid9rowscount = myDataGrid9.Rows.Count;
                        for (int p = myDataGrid9rowscount - (myDataGrid9.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid9.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 9)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart10 = 0;
                        loopend10 = 0;
                        int myDataGrid10rowscount = myDataGrid10.Rows.Count;
                        for (int p = myDataGrid10rowscount - (myDataGrid10.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid10.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 10)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart11 = 0;
                        loopend11 = 0;
                        int myDataGrid11rowscount = myDataGrid11.Rows.Count;
                        for (int p = myDataGrid11rowscount - (myDataGrid11.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid11.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }

                    if (rowno == 11)
                    {
                        pnl_SerialNo.Visible = false;
                        loopstart12 = 0;
                        loopend12 = 0;
                        int myDataGrid12rowscount = myDataGrid12.Rows.Count;
                        for (int p = myDataGrid12rowscount - (myDataGrid12.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGrid12.Rows.RemoveAt(p - 1);
                        }
                        return;
                    }


                }

                double ini_0 = 1, ini2 = 1;
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
                    ini_0 = 0;

                }
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = "0";
                    ini2 = 0;
                }
                if (ini_0 != 1 || ini2 != 1)
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";

                }
            }

            if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            {
                double total = 0;
                //quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
                //rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);
                //Double price = quantity * rate;
                //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;

                for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                {
                    if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                    {
                        total += 0;
                    }
                    else
                    {
                        total += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                    }
                }

                lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(total.ToString()));
            }

            else if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 6)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value != "0.00")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value)));

                        //gridrows_calculatoin();
                    }
                }

                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value != null)
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value.ToString() != "0.00")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value)));

                        //gridrows_calculatoin();
                    }
                }

                double ini_0 = 1, ini2 = 1;
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
                    ini_0 = 0;
                }
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = "0";
                    ini2 = 0;
                }
                if (ini_0 != 1 || ini2 != 1)
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";

                }
            }

            if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            {
                //if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() != "0")
                //{
                //    double total = 0;
                //    quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
                //    rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);
                //    Double price = quantity * rate;
                //    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;

                //    for (int mn = 0; mn < myDataGrid1.Rows.Count; mn++)
                //    {
                //        if (myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString() == "")
                //        {
                //            total += 0;
                //        }
                //        else
                //        {
                //            total += double.Parse(myDataGrid1.Rows[mn].Cells["Amount"].Value.ToString());
                //        }
                //    }

                //    lbl_amt.Text = total + ".00";
                //}
            }

            //    if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            //    {
            //        rowsindex = myDataGrid1.CurrentRow.Index;
            //        if (myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value != null)
            //        {
            //            itemid = myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            //            //templessvalue = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_less"].Value.ToString());
            //            //tempaddvalue = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["S_add"].Value.ToString());
            //            getbyid(itemid);
            //            //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = tempCode.ToString();
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
            //        }
            //        else
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[rowsindex].Cells["Name"];
            //        }
            //    }

        }

        private void myDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString() == "0.00" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString() == "")
            {
                //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = 0;
            }
            else
            {
                tempCode = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString();
            }
        }

        private void myDataGrid1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void HighlightRows()
        {
            foreach (DataGridViewRow row in myDataGrid1.Rows)
            {

                //row.DefaultCellStyle.BackColor = Color.LightSalmon;
                //row.DefaultCellStyle.SelectionBackColor = Color.Salmon;

            }
        }

        private void myDataGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //this.myDataGrid1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            //this.myDataGrid1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            // HighlightRows();
        }

        private void myDataGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (myDataGrid1.CurrentCell.ColumnIndex == 2)
            {
                if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() == "" && myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString() == "")
                {
                    if (myDataGrid1.Rows.Count > 0)
                    {
                        var selected = myDataGrid1.SelectedCells;
                        for (int x = 0; x < selected.Count; )
                        {
                            myDataGrid1.ClearSelection();
                            MyMessageBox1.ShowBox("Empty Item Code Or Item Name", "Warning");
                            break;
                        }
                        btn_save.Focus();
                    }
                }
            }

            if (myDataGrid1.CurrentCell.ColumnIndex == 5)
            {
                if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() != "" && myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString() != "")
                {
                    if (myDataGrid1.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString()))
                        {
                            getQty = string.IsNullOrEmpty(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString()) ? 0 : Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString());
                            altName = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                            SqlCommand cmd = new SqlCommand("Select nt_purqty from Item_table where Item_name='" + altName + "'", con);
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                tempQty = Convert.ToInt32(dt.Rows[0]["nt_purqty"].ToString());
                            }
                            //altQty = tempQty - getQty;
                            //SqlCommand cmd1 = new SqlCommand("Update Item_table set nt_salqty=@altQty where Item_name='" + altName + "'", con);
                            //cmd1.Parameters.AddWithValue("@altQty", altQty);
                            //con.Close();
                            //con.Open();
                            //cmd1.ExecuteNonQuery();
                            //con.Close();
                        }
                    }
                }
            }

            //if (myDataGrid1.CurrentCell.ColumnIndex == 5)
            //{
            //    if (myDataGrid1.Rows.Count > 0)
            //    {
            //        if (myDataGrid1.Rows.Count > 0)
            //        {
            //            int tempQty = 0;
            //            getQty = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString());
            //            altName = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            //            SqlCommand cmd = new SqlCommand("Select nt_purqty from Item_table where Item_name='" + altName + "'", con);
            //            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //            DataTable dt = new DataTable();
            //            adp.Fill(dt);
            //            if (dt.Rows.Count > 0)
            //            {
            //                tempQty = Convert.ToInt32(dt.Rows[0]["nt_purqty"].ToString());
            //                altQty1 = tempQty + getQty;
            //            }
            //            getQty = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString());
            //            altName = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            //            SqlCommand cmd2 = new SqlCommand("Update Item_table set nt_purqty=@altQty where Item_name='" + altName + "'", con);
            //            cmd2.Parameters.AddWithValue("@altQty", altQty1);
            //            con.Close();
            //            con.Open();
            //            cmd2.ExecuteNonQuery();
            //            con.Close();
            //        }                  

            //    }
            //}

            if (e.ColumnIndex == 4)
            {
                if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                {
                    for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                    {
                        if (lbl_amt.Text == "")
                        {
                            lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().ToString()));
                        }
                        else
                        {
                            if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                            {
                                amount += double.Parse(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());
                            }
                        }
                        lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(amount.ToString()));
                    }
                    amount = 0;
                }
            }
            if (e.ColumnIndex == 5)
            {
                if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                {
                    for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                    {
                        if (lbl_amt.Text == "")
                        {
                            lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().ToString()));
                        }
                        else
                        {
                            if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                            {
                                amount += double.Parse(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());
                            }
                        }
                        lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(amount.ToString()));
                    }
                    amount = 0;
                }
            }
            if (e.ColumnIndex == 6 || e.ColumnIndex == 5)
            {
                if (!string.IsNullOrEmpty(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString()))
                {
                    if (!string.IsNullOrEmpty(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString()))
                    {
                        if (Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString()) > 0)
                        {
                            double qty_Less = 0, Qty_RateLess = 0;
                            qty_Less = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value);
                            Qty_RateLess = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value) <= 0 ? 0.00 : Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value);
                            myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (qty_Less * Qty_RateLess));
                        }
                        else if (!string.IsNullOrEmpty(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString()))
                        {
                            double qty_Add = 0, Qty_RateAdd = 0;
                            qty_Add = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value);
                            Qty_RateAdd = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value) <= 0 ? 0.00 : Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value);
                            myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (qty_Add * Qty_RateAdd));
                        }
                    }
                    else if (!string.IsNullOrEmpty(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString()))
                    {
                        double qty_Add = 0, Qty_RateAdd = 0;
                        qty_Add = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value);
                        Qty_RateAdd = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value) <= 0 ? 0.00 : Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value);
                        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", (qty_Add * Qty_RateAdd));
                    }
                }
            }
        }
        DataTable dt11 = new DataTable();
        private void myDataGrid1_Enter(object sender, EventArgs e)
        {
            for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
            {
                if (myDataGrid1.Rows[i].Cells["Name"].Value.ToString() != "")
                {
                    if (myDataGrid1.Rows[i].Cells["Name"].Value != null)
                    {
                        //string itemnamevalues = "";
                        //itemnamevalues = DgPurchase.Rows[i].Cells["ItemNames"].Value.ToString();
                        //selectchkmethods(itemnamevalues);
                        //if (selectcount != "0")

                        dt11.Rows.Add(myDataGrid1.Rows[i].Cells["Code"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Name"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Unit"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Rate"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().Trim());

                    }
                }
            }

            //foreach (DataGridViewColumn col in myDataGrid1.Columns)
            //{
            //    dt11.Columns.Add(col.HeaderText);
            //}

            //foreach (DataGridViewRow row in myDataGrid1.Rows)
            //{
            //    DataRow dRow = dt11.NewRow();
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        dRow[cell.ColumnIndex] = cell.Value;
            //    }
            //    dt11.Rows.Add(dRow);
            //}

            //for (int i = dt11.Rows.Count - 1; i >= 0; i += -1)
            //{
            //    DataRow row = dt11.Rows[i];
            //    if (row[0] == null)
            //    {
            //        dt11.Rows.Remove(row);
            //    }
            //    else if (string.IsNullOrEmpty(row[0].ToString()))
            //    {
            //        dt11.Rows.Remove(row);
            //    }
            //}

            //     string billNo = lbl_adjust_no.Text;
            //con.Close();
            //con.Open();
            //string getStrnoqry = "select Adj_No from adjmas_table where Adj_Billno='" + lbl_adjust_no.Text + "'";
            //SqlCommand cmddeleteRec = new SqlCommand(getStrnoqry, con);
            //deletedRecNo = cmddeleteRec.ExecuteScalar().ToString();
            //con.Close();
            //// delete the old record and insert the new grdiview values..
            //// update the alteration values.
            //con.Open();
            //string oldrecord = "select strn_sno from stktrn_table where strn_no='" + deletedRecNo + "'";
            //SqlCommand cmdoldRecord = new SqlCommand(oldrecord, con);
            //SqlDataAdapter adp = new SqlDataAdapter(cmdoldRecord);
            //DataTable dt = new DataTable();
            //adp.Fill(dt);
            //for (int ij = 0; ij < dt.Rows.Count; ij++)
            //{
            //}
            //    SqlCommand cmd = new SqlCommand("Select strn_sno from stktrn_table where strn_sno''", con);
        }



        private void myDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt11.Rows.Count; i++)
            {
                if (dt11.Rows[i]["Add_Qty"].ToString() == "0")
                {
                    // Get a old sales quantity:
                    double Old_L_Q = Convert.ToDouble(dt11.Rows[i]["Less_Qty"].ToString());

                    con.Close();
                    con.Open();
                    string ItemNoqry = "select Item_no from Item_table where Item_code='" + dt11.Rows[i]["Code"].ToString() + "' and Item_name='" + dt11.Rows[i]["Name"].ToString() + "'";
                    SqlCommand cmdItemNo = new SqlCommand(ItemNoqry, con);
                    int OldItemNO = Convert.ToInt16(cmdItemNo.ExecuteScalar());

                    string getSalQty = "select nt_salqty from Item_table where Item_no='" + OldItemNO + "' ";
                    SqlCommand cmdSalQty = new SqlCommand(getSalQty, con);
                    double OldSalQty = Convert.ToDouble(cmdSalQty.ExecuteScalar());

                    double Ca_L_Q = OldSalQty - Old_L_Q;

                    // Get a old sales value:
                    double Old_Sal_val = Convert.ToDouble(dt11.Rows[i]["Amount"].ToString());

                    string getSalval = "select Nt_Salval from Item_table where Item_no='" + OldItemNO + "' ";
                    SqlCommand cmdSalval = new SqlCommand(getSalval, con);
                    double OldSalval = Convert.ToDouble(cmdSalval.ExecuteScalar());

                    double Ca_Sal_Val = OldSalval - Old_Sal_val;

                    // get a old closing quantity:

                    string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + OldItemNO + "' ";
                    SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                    double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                    double OldClosingQty = ClosingQty + Old_L_Q;

                    SqlCommand cmdUpdate = new SqlCommand("Update Item_table set nt_salqty=" + Ca_L_Q + ",nt_cloqty=" + OldClosingQty + ",Nt_Salval=" + Ca_Sal_Val + " where Item_no='" + OldItemNO + "' ", con);

                    cmdUpdate.ExecuteNonQuery();
                    con.Close();
                }

                if (myDataGrid1.Rows[i].Cells["Add_qty"].Value.ToString() == "0")
                {
                    // Get a new sales value:
                    double New_L2_Q = Convert.ToDouble(myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString());
                    con.Close();
                    con.Open();
                    string ItemNoqry2 = "select Item_no from Item_table where Item_code='" + myDataGrid1.Rows[i].Cells["Code"].Value.ToString() + "' and Item_name='" + myDataGrid1.Rows[i].Cells["Name"].Value.ToString() + "'";
                    SqlCommand cmdItemNo2 = new SqlCommand(ItemNoqry2, con);
                    int NewItemNO2 = Convert.ToInt16(cmdItemNo2.ExecuteScalar());

                    string getSalQty1 = "select nt_salqty from Item_table where Item_no='" + NewItemNO2 + "' ";
                    SqlCommand cmdSalQty1 = new SqlCommand(getSalQty1, con);
                    double NewSalQty = Convert.ToDouble(cmdSalQty1.ExecuteScalar());

                    double Ca_L1_Q = NewSalQty + New_L2_Q;

                    // Get a new sales value:
                    double New_Sal_val = Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());

                    string getSalval = "select Nt_Salval from Item_table where Item_no='" + NewItemNO2 + "' ";
                    SqlCommand cmdSalval = new SqlCommand(getSalval, con);
                    double NewSalval = Convert.ToDouble(cmdSalval.ExecuteScalar());

                    double Ca_Sal1_Val = NewSalval + New_Sal_val;

                    // get a new closing quantity:                     
                    string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + NewItemNO2 + "' ";
                    SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                    double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                    double NewClosingQty = ClosingQty - New_L2_Q;

                    SqlCommand cmdUpdate1 = new SqlCommand("Update Item_table set nt_salqty=" + Ca_L1_Q + ",nt_cloqty=" + NewClosingQty + ",Nt_Salval=" + Ca_Sal1_Val + " where Item_no='" + NewItemNO2 + "' ", con);
                    cmdUpdate1.ExecuteNonQuery();
                    con.Close();
                }

                if (dt11.Rows[i]["Less_Qty"].ToString() == "0")
                {
                    // Get a old purchase quantity:
                    double Old_A_Q = Convert.ToDouble(dt11.Rows[i]["Add_Qty"].ToString());

                    con.Close();
                    con.Open();
                    string ItemNoqry = "select Item_no from Item_table where Item_code='" + dt11.Rows[i]["Code"].ToString() + "' and Item_name='" + dt11.Rows[i]["Name"].ToString() + "'";
                    SqlCommand cmdItemNo = new SqlCommand(ItemNoqry, con);
                    int OldItemNO = Convert.ToInt16(cmdItemNo.ExecuteScalar());

                    string getPurQty = "select nt_purqty from Item_table where Item_no='" + OldItemNO + "' ";
                    SqlCommand cmdPurQty = new SqlCommand(getPurQty, con);
                    double OldPurQty = Convert.ToDouble(cmdPurQty.ExecuteScalar());

                    double Ca_A_Q = OldPurQty - Old_A_Q;

                    // Get a old purchase value:
                    double Old_Pur_val = Convert.ToDouble(dt11.Rows[i]["Amount"].ToString());

                    string getPurval = "select Nt_PurVal from Item_table where Item_no='" + OldItemNO + "' ";
                    SqlCommand cmdPurval = new SqlCommand(getPurval, con);
                    double OldPurval = Convert.ToDouble(cmdPurval.ExecuteScalar());

                    double Ca_Pur_Val = OldPurval - Old_Pur_val;

                    // Get a old closing quantity:                     
                    string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + OldItemNO + "' ";
                    SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                    double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                    double OldClosingQty = ClosingQty - Old_A_Q;

                    SqlCommand cmdUpdate = new SqlCommand("Update Item_table set nt_purqty=" + Ca_A_Q + ",nt_cloqty=" + OldClosingQty + ",Nt_PurVal=" + Ca_Pur_Val + " where Item_no='" + OldItemNO + "' ", con);
                    cmdUpdate.ExecuteNonQuery();
                    con.Close();
                }

                if (myDataGrid1.Rows[i].Cells["Less_qty"].Value.ToString() == "0")
                {
                    // Get a new purchase quantity:
                    double New_A_Q = Convert.ToDouble(myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString());
                    con.Close();
                    con.Open();
                    string ItemNoqry1 = "select Item_no from Item_table where Item_code='" + myDataGrid1.Rows[i].Cells["Code"].Value.ToString() + "' and Item_name='" + myDataGrid1.Rows[i].Cells["Name"].Value.ToString() + "'";
                    SqlCommand cmdItemNo1 = new SqlCommand(ItemNoqry1, con);
                    int NewItemNO = Convert.ToInt16(cmdItemNo1.ExecuteScalar());

                    string getPurQty = "select nt_purqty from Item_table where Item_no='" + NewItemNO + "' ";
                    SqlCommand cmdPurQty = new SqlCommand(getPurQty, con);
                    double NewPurQty = Convert.ToDouble(cmdPurQty.ExecuteScalar());

                    double Ca_A1_Q = NewPurQty + New_A_Q;

                    // Get a new purchase value:
                    double New_Pur_val = Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());

                    string getPurval = "select Nt_PurVal from Item_table where Item_no='" + NewItemNO + "' ";
                    SqlCommand cmdPurval = new SqlCommand(getPurval, con);
                    double NewPurval = Convert.ToDouble(cmdPurval.ExecuteScalar());

                    double Ca_Pur1_Val = NewPurval + New_Pur_val;

                    // Get a new closing quantity:                     
                    string ClosingQtyqry = "select nt_cloqty from Item_table where Item_no='" + NewItemNO + "' ";
                    SqlCommand cmdClosing = new SqlCommand(ClosingQtyqry, con);
                    double ClosingQty = Convert.ToDouble(cmdClosing.ExecuteScalar());

                    double NewClosingQty = ClosingQty + New_A_Q;

                    SqlCommand cmdUpdate1 = new SqlCommand("Update Item_table set nt_purqty=" + Ca_A1_Q + ",nt_cloqty=" + NewClosingQty + ",Nt_PurVal=" + Ca_Pur1_Val + " where Item_no='" + NewItemNO + "' ", con);
                    cmdUpdate1.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (myDataGridadjstock.Visible == true)
            {
                for (int f = 0; f < myDataGridadjstock.Rows.Count - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGridadjstock.Rows[f].Cells["SerialNo"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid2.Visible == true)
            {
                for (int f = 0; f < myDataGrid2.Rows.Count - (myDataGrid2.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid2.Rows[f].Cells["SerialNo2"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid3.Visible == true)
            {
                for (int f = 0; f < myDataGrid3.Rows.Count - (myDataGrid3.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid3.Rows[f].Cells["SerialNo3"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid4.Visible == true)
            {
                for (int f = 0; f < myDataGrid4.Rows.Count - (myDataGrid4.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid4.Rows[f].Cells["SerialNo4"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid5.Visible == true)
            {
                for (int f = 0; f < myDataGrid5.Rows.Count - (myDataGrid5.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid5.Rows[f].Cells["SerialNo5"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid6.Visible == true)
            {
                for (int f = 0; f < myDataGrid6.Rows.Count - (myDataGrid6.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid6.Rows[f].Cells["SerialNo6"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid7.Visible == true)
            {
                for (int f = 0; f < myDataGrid7.Rows.Count - (myDataGrid7.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid7.Rows[f].Cells["SerialNo7"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid8.Visible == true)
            {
                for (int f = 0; f < myDataGrid8.Rows.Count - (myDataGrid8.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid8.Rows[f].Cells["SerialNo8"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid9.Visible == true)
            {
                for (int f = 0; f < myDataGrid9.Rows.Count - (myDataGrid9.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid9.Rows[f].Cells["SerialNo9"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid10.Visible == true)
            {
                for (int f = 0; f < myDataGrid10.Rows.Count - (myDataGrid10.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid10.Rows[f].Cells["SerialNo10"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid11.Visible == true)
            {
                for (int f = 0; f < myDataGrid11.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid11.Rows[f].Cells["SerialNo11"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }
            }

            if (myDataGrid12.Visible == true)
            {
                for (int f = 0; f < myDataGrid12.Rows.Count - (myDataGrid12.AllowUserToAddRows ? 1 : 0); f++)
                {
                    if ((String)myDataGrid12.Rows[f].Cells["SerialNo12"].Value == null)
                    {
                        MessageBox.Show(" cell is empty");
                        return;
                    }
                }

            }

            pnl_SerialNo.Visible = false;
        }



        private void myDataGrid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 4)
            {
                if (e.ColumnIndex == 4 && myDataGrid1.Rows[e.RowIndex].Cells[4].Value != null && Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value) != 0)
                {

                    rowno = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);
                    string testvar = "";
                    testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();
                    if (Convert.ToInt32(testvar.ToString()) == 1)
                    {
                        //Beginning First Row 
                        if (rowno == 0)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGridadjstock.AllowUserToAddRows = false;

                                if (myDataGridadjstock.Rows.Count != loopend)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGridadjstock.Rows.Add();
                                        myDataGridadjstock.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGridadjstock.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGridadjstock.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGridadjstock.Visible = true;
                            }

                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending First Row

                        //Beginning Second Row 
                        if (rowno == 1)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend2 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid2.AllowUserToAddRows = false;

                                if (myDataGrid2.Rows.Count != loopend2)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid2.Rows.Add();
                                        myDataGrid2.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid2.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid2.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid2.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Second Row

                        //Beginning Third Row 
                        if (rowno == 2)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend3 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid3.AllowUserToAddRows = false;

                                if (myDataGrid3.Rows.Count != loopend3)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid3.Rows.Add();
                                        myDataGrid3.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid3.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid3.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid3.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Third Row

                        //Beginning Fourth Row 
                        if (rowno == 3)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend4 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid4.AllowUserToAddRows = false;

                                if (myDataGrid4.Rows.Count != loopend4)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid4.Rows.Add();
                                        myDataGrid4.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid4.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid4.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid4.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Fourth Row

                        //Beginning Fifth Row 
                        if (rowno == 4)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend5 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid5.AllowUserToAddRows = false;

                                if (myDataGrid5.Rows.Count != loopend5)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid5.Rows.Add();
                                        myDataGrid5.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid5.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid5.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid5.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Fifth Row

                        //Beginning Sixth Row 
                        if (rowno == 5)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend6 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid6.AllowUserToAddRows = false;

                                if (myDataGrid6.Rows.Count != loopend6)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid6.Rows.Add();
                                        myDataGrid6.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid6.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid6.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid6.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Sixth Row

                        //Beginning Seventh Row 
                        if (rowno == 6)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend7 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid6.AllowUserToAddRows = false;

                                if (myDataGrid7.Rows.Count != loopend7)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid7.Rows.Add();
                                        myDataGrid7.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid7.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid7.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid7.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Seventh Row

                        //Beginning Eighth Row 
                        if (rowno == 7)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend8 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid8.AllowUserToAddRows = false;

                                if (myDataGrid8.Rows.Count != loopend8)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid8.Rows.Add();
                                        myDataGrid8.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid8.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid8.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid8.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Eighth Row

                        //Beginning Nineth Row 
                        if (rowno == 8)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend8 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid8.AllowUserToAddRows = false;

                                if (myDataGrid8.Rows.Count != loopend8)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid9.Rows.Add();
                                        myDataGrid9.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid9.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid9.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid9.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Nineth Row

                        //Beginning Tenth Row 
                        if (rowno == 9)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend10 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid10.AllowUserToAddRows = false;

                                if (myDataGrid10.Rows.Count != loopend10)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid10.Rows.Add();
                                        myDataGrid10.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid10.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid10.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid10.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending Tenth Row

                        //Beginning Eleventh Row 
                        if (rowno == 10)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend11 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid11.AllowUserToAddRows = false;

                                if (myDataGrid11.Rows.Count != loopend11)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid11.Rows.Add();
                                        myDataGrid11.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid11.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid11.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid11.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid12.Visible = false;
                        }
                        //Ending ELeventh Row

                        //Beginning Twelth Row 
                        if (rowno == 11)
                        {
                            // Beginning serial number selection from database
                            DataTable datatableserial = new DataTable();
                            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString() + "' and pur_sal_ref_no = '" + lbl_adjust_no.Text.ToString().Trim() + "'", con);
                            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                            datatableserial.Rows.Clear();
                            adpumas.Fill(datatableserial);
                            loopend12 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                            if (datatableserial.Rows.Count > 0)
                            {
                                myDataGrid12.AllowUserToAddRows = false;

                                if (myDataGrid12.Rows.Count != loopend12)
                                {
                                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                                    {
                                        myDataGrid12.Rows.Add();
                                        myDataGrid12.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                                        myDataGrid12.Rows[i].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    }
                                }
                            }
                            // Ending serial number selection from database
                            if (myDataGrid12.Rows.Count > 0)
                            {
                                pnl_SerialNo.Visible = true;
                                myDataGrid12.Visible = true;
                            }

                            myDataGridadjstock.Visible = false;
                            myDataGrid2.Visible = false;
                            myDataGrid3.Visible = false;
                            myDataGrid4.Visible = false;
                            myDataGrid5.Visible = false;
                            myDataGrid6.Visible = false;
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                        }
                        //Ending Twelth Row
                    }

                }
            }
        }

        private void myDataGridadjstock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGridadjstock.Rows[myDataGridadjstock.CurrentCell.RowIndex].Cells["SerialNo"].Value != null)
                {
                    t1 = myDataGridadjstock.Rows[myDataGridadjstock.CurrentRow.Index].Cells["SerialNo"].Value.ToString();
                    t3 = myDataGridadjstock.Rows[myDataGridadjstock.CurrentRow.Index].Cells["serialitemcode"].Value.ToString();
                    int t2 = myDataGridadjstock.CurrentRow.Index;

                    for (int j = 0; j < myDataGridadjstock.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGridadjstock.Rows[j].Cells["SerialNo"].Value != null)
                            {
                                if (t1.ToLower() == myDataGridadjstock.Rows[j].Cells["SerialNo"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid2.Rows[myDataGrid2.CurrentCell.RowIndex].Cells["SerialNo2"].Value != null)
                {
                    t1 = myDataGrid2.Rows[myDataGrid2.CurrentRow.Index].Cells["SerialNo2"].Value.ToString();
                    t3 = myDataGrid2.Rows[myDataGrid2.CurrentRow.Index].Cells["serialitemcode2"].Value.ToString();
                    int t2 = myDataGrid2.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid2.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid2.Rows[j].Cells["SerialNo2"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid2.Rows[j].Cells["SerialNo2"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid3.Rows[myDataGrid3.CurrentCell.RowIndex].Cells["SerialNo3"].Value != null)
                {
                    t1 = myDataGrid3.Rows[myDataGrid3.CurrentRow.Index].Cells["SerialNo3"].Value.ToString();
                    t3 = myDataGrid3.Rows[myDataGrid3.CurrentRow.Index].Cells["serialitemcode3"].Value.ToString();
                    int t2 = myDataGrid3.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid3.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid3.Rows[j].Cells["SerialNo3"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid3.Rows[j].Cells["SerialNo3"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid4.Rows[myDataGrid4.CurrentCell.RowIndex].Cells["SerialNo4"].Value != null)
                {
                    t1 = myDataGrid4.Rows[myDataGrid4.CurrentRow.Index].Cells["SerialNo4"].Value.ToString();
                    t3 = myDataGrid4.Rows[myDataGrid4.CurrentRow.Index].Cells["serialitemcode4"].Value.ToString();
                    int t2 = myDataGrid4.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid4.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid4.Rows[j].Cells["SerialNo4"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid4.Rows[j].Cells["SerialNo4"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid5.Rows[myDataGrid5.CurrentCell.RowIndex].Cells["SerialNo5"].Value != null)
                {
                    t1 = myDataGrid5.Rows[myDataGrid5.CurrentRow.Index].Cells["SerialNo5"].Value.ToString();
                    t3 = myDataGrid5.Rows[myDataGrid5.CurrentRow.Index].Cells["serialitemcode5"].Value.ToString();
                    int t2 = myDataGrid5.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid5.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid5.Rows[j].Cells["SerialNo5"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid5.Rows[j].Cells["SerialNo5"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid6.Rows[myDataGrid6.CurrentCell.RowIndex].Cells["SerialNo6"].Value != null)
                {
                    t1 = myDataGrid6.Rows[myDataGrid6.CurrentRow.Index].Cells["SerialNo6"].Value.ToString();
                    t3 = myDataGrid6.Rows[myDataGrid6.CurrentRow.Index].Cells["serialitemcode6"].Value.ToString();
                    int t2 = myDataGrid6.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid6.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid6.Rows[j].Cells["SerialNo6"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid6.Rows[j].Cells["SerialNo6"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid7_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid7.Rows[myDataGrid7.CurrentCell.RowIndex].Cells["SerialNo7"].Value != null)
                {
                    t1 = myDataGrid7.Rows[myDataGrid7.CurrentRow.Index].Cells["SerialNo7"].Value.ToString();
                    t3 = myDataGrid7.Rows[myDataGrid7.CurrentRow.Index].Cells["serialitemcode7"].Value.ToString();
                    int t2 = myDataGrid7.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid7.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid7.Rows[j].Cells["SerialNo7"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid7.Rows[j].Cells["SerialNo7"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid8_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid8.Rows[myDataGrid8.CurrentCell.RowIndex].Cells["SerialNo8"].Value != null)
                {
                    t1 = myDataGrid8.Rows[myDataGrid8.CurrentRow.Index].Cells["SerialNo8"].Value.ToString();
                    t3 = myDataGrid8.Rows[myDataGrid8.CurrentRow.Index].Cells["serialitemcode8"].Value.ToString();
                    int t2 = myDataGrid8.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid8.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid8.Rows[j].Cells["SerialNo8"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid8.Rows[j].Cells["SerialNo8"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid9_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid9.Rows[myDataGrid9.CurrentCell.RowIndex].Cells["SerialNo9"].Value != null)
                {
                    t1 = myDataGrid9.Rows[myDataGrid9.CurrentRow.Index].Cells["SerialNo9"].Value.ToString();
                    t3 = myDataGrid9.Rows[myDataGrid9.CurrentRow.Index].Cells["serialitemcode9"].Value.ToString();
                    int t2 = myDataGrid9.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid9.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid9.Rows[j].Cells["SerialNo9"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid9.Rows[j].Cells["SerialNo9"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid10_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid10.Rows[myDataGrid10.CurrentCell.RowIndex].Cells["SerialNo10"].Value != null)
                {
                    t1 = myDataGrid10.Rows[myDataGrid10.CurrentRow.Index].Cells["SerialNo10"].Value.ToString();
                    t3 = myDataGrid10.Rows[myDataGrid10.CurrentRow.Index].Cells["serialitemcode10"].Value.ToString();
                    int t2 = myDataGrid10.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid10.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid10.Rows[j].Cells["SerialNo10"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid10.Rows[j].Cells["SerialNo10"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid11_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid11.Rows[myDataGrid11.CurrentCell.RowIndex].Cells["SerialNo11"].Value != null)
                {
                    t1 = myDataGrid11.Rows[myDataGrid11.CurrentRow.Index].Cells["SerialNo11"].Value.ToString();
                    t3 = myDataGrid11.Rows[myDataGrid11.CurrentRow.Index].Cells["serialitemcode11"].Value.ToString();
                    int t2 = myDataGrid11.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid11.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid11.Rows[j].Cells["SerialNo11"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid11.Rows[j].Cells["SerialNo11"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

        private void myDataGrid12_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Beginning 
            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            {

                if (myDataGrid12.Rows[myDataGrid12.CurrentCell.RowIndex].Cells["SerialNo12"].Value != null)
                {
                    t1 = myDataGrid12.Rows[myDataGrid12.CurrentRow.Index].Cells["SerialNo12"].Value.ToString();
                    t3 = myDataGrid12.Rows[myDataGrid12.CurrentRow.Index].Cells["serialitemcode12"].Value.ToString();
                    int t2 = myDataGrid12.CurrentRow.Index;

                    for (int j = 0; j < myDataGrid12.Rows.Count; j++)
                    {
                        if (t2 != j)
                        {
                            if (myDataGrid12.Rows[j].Cells["SerialNo12"].Value != null)
                            {
                                if (t1.ToLower() == myDataGrid12.Rows[j].Cells["SerialNo12"].Value.ToString().ToLower())
                                {
                                    MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                    break;
                                }
                            }
                        }
                    }
                }

                dbcheckforserial();
            }
            //Ending
        }

    }
}
