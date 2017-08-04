using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MSPOSBACKOFFICE
{
    public partial class StockAdjustCreate : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        //SqlConnection con = new SqlConnection();
        string id;
        string itemname;
        // Beginning Variable Declaration
        public int rw = 0;
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
        public int rowno = 0;
        public int loadingrowno = 0;
        // Ending Variable Declaration


        //Double total = 0;
        DataTable autofind = new DataTable();
        DataTable companydt = new DataTable();
        DataTable dtNew = new DataTable();
        DataTable dt2_Check = new DataTable();
        string t1, t3;
        public StockAdjustCreate()
        {
            InitializeComponent();
            // DateTime year = DateTime.Now;
            // txt_date.Text = DateTime.Today.Date.ToShortDateString();

            ////txt_date.Format = DateTimePickerFormat.Custom;
            ////txt_date.CustomFormat = "dd/MM/yyyy";
            txt_date.Focus();
            txt_date.Select();
            pnl_comp_name.Visible = false;
            lst_compname.Visible = false;
            lst_itemname.Visible = false;
            pnl_ctrname.Visible = false;
            lst_ctrname.Visible = false;
            pnl_item_name.Visible = false;

            dtNew.Columns.Add("Code", typeof(string));
            dtNew.Columns.Add("Name", typeof(string));
            dtNew.Columns.Add("Unit", typeof(string));
            dtNew.Columns.Add("Less_Qty", typeof(string));
            dtNew.Columns.Add("Add_Qty", typeof(string));
            dtNew.Columns.Add("Rate", typeof(string));
            dtNew.Columns.Add("Amount", typeof(string));
            dtNew.Columns.Add("Stock_Category", typeof(string));

            myDataGrid1.DataSource = dtNew.DefaultView;
            this.myDataGrid1.DefaultCellStyle.Font = new Font("Tahoma", 12);
            this.myDataGrid1.RowTemplate.Height = 25;
            myDataGrid1.Columns[0].Width = 130;
            myDataGrid1.Columns[1].Width = 330;
            myDataGrid1.Columns[2].Width = 80;
            myDataGrid1.Columns[3].Width = 100;
            myDataGrid1.Columns[4].Width = 100;
            myDataGrid1.Columns[5].Width = 120;
            myDataGrid1.Columns[6].Width = 140;
            myDataGrid1.Columns[7].Width = 140;
            // for center alignment of Header File:
            foreach (DataGridViewColumn col in myDataGrid1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            myDataGrid1.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            myDataGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;

            myDataGrid1.BackgroundColor = Color.White;


            // code for cell formatting width:
            //myDataGrid1.CellFormatting += new DataGridViewCellFormattingEventHandler(datagriviewcellformating);
            //myDataGrid1.ColumnHeadersHeight = 210;
            con.Open();
            SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_cost from Item_table where Item_Active=" + 1 + " order by Item_name ASC", con);
            SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
            autofind.Rows.Clear();
            nameadp.Fill(autofind);
        }

        string items_alter = "0";
        #region set a width of all cell indatagridview
        //public void datagriviewcellformating(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    Graphics g = this.CreateGraphics();
        //    myDataGrid1.Columns[e.ColumnIndex].Width = 178;
        //}
        #endregion
        public delegate void SetColumnIndex(int i);
        public void Mymethod(int columnIndex)
        {

            if (items_alter != "0")
            {
                //Top:
                //if (DgPurchase.CurrentRow.Cells[columnIndex].Visible == true)
                //{
                myDataGrid1.CurrentCell = myDataGrid1.CurrentRow.Cells[columnIndex];
                myDataGrid1.BeginEdit(true);
                //goto end;
                //}

                //else
                //{
                //    this.DgPurchase.CurrentCell = this.DgPurchase.CurrentRow.Cells[columnIndex + 1];
                //     goto Top;
                //}
            }
            else
            {
                this.myDataGrid1.CurrentCell = this.myDataGrid1.CurrentRow.Cells[2];
                this.myDataGrid1.BeginEdit(false);
                items_alter = "1";
            }
            //end:
            //int jakend=0;
        }

        // load a counter value in lst_ctrname:

        #region To select a counter name in the listbox
        public void countload()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select ctr_name from counter_table order by ctr_name ASC", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lst_ctrname.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_ctrname.Items.Add(dt.Rows[i]["ctr_name"].ToString());
                    txt_countername.Text = (dt.Rows[0]["ctr_name"].ToString());
                    this.lst_ctrname.SelectedIndex = 0;
                }
            }
            adp.Dispose();

        }
        #endregion


        #region exit button
        private void btn_exit_Click(object sender, EventArgs e)
        {
            pnl_ctrname.Hide();

            this.Close();

        }
        #endregion



        //lstcounter select coding:
        private void lst_ctrname_SelectedIndexChanged(object sender, EventArgs e)
        {

            ////pnl_ctrname.Visible = true;
            ////lst_ctrname.Visible = true;
            ////txt_countername.Text = lst_ctrname.SelectedItem.ToString();
        }



        //load a itemname into Listbox based on id:
        #region Itemname loaded on a Panel list while entered is gridview cell
        public void ItemNameload()
        {
            int iRow = myDataGrid1.CurrentCell.RowIndex;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("select Item_name from Item_table where Item_code='" + id + "' order by Item_name ASC", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lst_itemname.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_itemname.Items.Add(dt.Rows[i]["Item_name"].ToString());
                    itemname = (dt.Rows[0]["Item_name"].ToString());
                    // this.lst_itemname.SelectedIndex = 0;

                }


            }
        }
        #endregion
        //load item name to list box based on name:
        #region Itemname loaded on a Panel list while entered is gridview cell
        public void ItemNameloadbyname()
        {
            SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_cost from Item_table where Item_Active=" + 1 + " order by Item_name ASC", con);
            SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lst_itemname.Items.Clear();
            nameadp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_itemname.Items.Add(dt.Rows[i]["Item_name"].ToString());
                    itemname = (dt.Rows[0]["Item_name"].ToString());
                    // this.lst_itemname.SelectedIndex = 0;
                    //myDataGrid1.Rows[i].Cells["Rate"].Value = (dt.Rows[i]["Item_mrsp"].ToString());
                    //myDataGrid1.Rows[i].Cells["Item_code"].Value = (dt.Rows[i]["Item_code"].ToString());
                }

            }
            nameadp.Dispose();


        }
        #endregion
        // company name load into the listbox:

        // string tActionCtrlEnter;
        // SqlDataReader dreader2 = null;
        #region To get a Company name to Textbox
        public void compload()
        {
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
                    this.lst_compname.SelectedIndex = 0;
                }
            }
        }
        #endregion

        // Txt_counter keypress
        private void txt_countername_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Up))
            {
                txt_countername.Focus();
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_inv_no.Focus();
            }
            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txt_date.Focus();
            }


        }

        #region ContextMenu creation
        private void AddContextMenu()
        {
            ToolStripMenuItem toolStripItem1 = new ToolStripMenuItem();
            toolStripItem1.Text = "Redden";
            toolStripItem1.Click += new EventHandler(toolStripItem1_Click);
            ContextMenuStrip strip = new ContextMenuStrip();
            foreach (DataGridViewColumn column in myDataGrid1.Columns)
            {

                column.ContextMenuStrip = strip;
                column.ContextMenuStrip.Items.Add(toolStripItem1);
            }
        }
        #endregion

        private DataGridViewCellEventArgs mouseLocation;

        // Change the cell's color. 
        private void toolStripItem1_Click(object sender, EventArgs args)
        {
            myDataGrid1.Rows[mouseLocation.RowIndex].Cells[mouseLocation.ColumnIndex].Style.BackColor = Color.Red;
        }

        // Deal with hovering over a cell. 
        private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs location)
        {
            mouseLocation = location;
        }



        private void lst_compname_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txt_comp_name.Text = lst_compname.SelectedItem.ToString();

        }

        private void lst_ctrname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txt_countername.Text = lst_ctrname.SelectedItem.ToString();
            //pnl_ctrname.Visible = true;
            //lst_ctrname.Visible = true;
            //txt_countername.Focus();
        }

        private void txt_comp_name_Leave(object sender, EventArgs e)
        {
            //compload();
            //pnl_comp_name.Visible = false;
            // lst_compname.Visible = false;

            check = true;
        }

        private void txt_comp_name_Enter(object sender, EventArgs e)
        {

            compload();
            pnl_comp_name.Visible = true;
            lst_compname.Visible = true;

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

        string globalid = chkbox.adjrecno;
        public string fromForm;
        // page load event:
        DataTable dt1 = new DataTable();
        private void StockAdjustCreate_Load(object sender, EventArgs e)
        {
            dt1.Columns.Add("Code");
            dt1.Columns.Add("Name");
            dt1.Columns.Add("Unit");
            dt1.Columns.Add("Less_Qty");
            dt1.Columns.Add("Add_Qty");
            dt1.Columns.Add("Rate");
            dt1.Columns.Add("Amount");
            txt_date.Focus();
            // dt_inv.Format = DateTimePickerFormat.Custom;
            // dt_inv.CustomFormat = "dd/MM/yyyy";
            //lst_itemname.Visible = false;
            //pnl_item_name.Visible = false;
            adjno();
            //for (int i = 0; i < 11; i++)
            //{
            //    myDataGrid1.Rows.Add();
            //} 

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

        }

        public void loadstckalter()
        {

            //SqlCommand cmd = new SqlCommand("select stck_adj_no,stckA_code,stckA_Name,stckA_Unit,stckA_lesQty,stckA_addQty,stckA_Rate,stckA_Amt from Stockadjmas_table where stckA_code='" + chkbox.ID + "' and stck_adj_no='" + chkbox.adjrecno + "' ", con);
            SqlCommand cmd = new SqlCommand("select stck_adj_no,stckA_code,stckA_Name,stckA_Unit,stckA_lesQty,stckA_addQty,stckA_Rate,stckA_Amt from Stockadjmas_table where  stck_adj_no='" + chkbox.adjrecno + "' ", con);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adp.Fill(dt);
            double totalamt = 0;
            if (dt.Rows.Count > 0)
            {
                //display the Adjust no for Alteration:
                lbl_adjust_no.Text = chkbox.adjrecno;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    myDataGrid1.Rows.Add();
                    myDataGrid1.Rows[i].Cells["Code"].Value = dt.Rows[i]["stckA_code"].ToString();
                    myDataGrid1.Rows[i].Cells["Name"].Value = dt.Rows[i]["stckA_Name"].ToString();
                    myDataGrid1.Rows[i].Cells["Unit"].Value = dt.Rows[i]["stckA_Unit"].ToString();
                    myDataGrid1.Rows[i].Cells["Less_Qty"].Value = dt.Rows[i]["stckA_lesQty"].ToString();
                    myDataGrid1.Rows[i].Cells["Add_Qty"].Value = dt.Rows[i]["stckA_addQty"].ToString();
                    myDataGrid1.Rows[i].Cells["Rate"].Value = dt.Rows[i]["stckA_Rate"].ToString();
                    myDataGrid1.Rows[i].Cells["Amount"].Value = dt.Rows[i]["stckA_Amt"].ToString();
                    totalamt = totalamt + Convert.ToDouble(dt.Rows[i]["stckA_Amt"].ToString());
                }
                lbl_amt.Text = String.Format("{0:0.00}", Convert.ToDouble(totalamt));
            }
            //DataSet ds = new DataSet();
            //SqlDataReader ad = cmd.ExecuteReader();
            //if (ad.HasRows)
            //{

            //    if (ad.Read())
            //    {
            //        myDataGrid1.Rows.Add();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Code"].Value = ad[0].ToString();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value = ad[1].ToString();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Unit"].Value = ad[2].ToString();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = ad[3].ToString();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value = ad[4].ToString();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Rate"].Value = ad[5].ToString();
            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Amount"].Value = ad[6].ToString();
            //    }
            //}

        }

        #region Adj_no Display in label box
        public void adjno()
        {

            con.Close();

            con.Open();
            string bnoqry = "select max(Adj_Billno)+1 from adjmas_table";
            SqlCommand bno = new SqlCommand(bnoqry, con);
            lbl_adjust_no.Text = bno.ExecuteScalar().ToString().Trim();
            if (lbl_adjust_no.Text == "")
            {
                lbl_adjust_no.Text = "1";
            }
            else
            {
                lbl_adjust_no.Text = bno.ExecuteScalar().ToString().Trim();
            }
            con.Close();


        }
        #endregion

        private void lst_compname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (Char)Keys.Up)
            //{
            //    txt_comp_name.Text = lst_compname.SelectedItem.ToString();
            //}
            txt_comp_name.Text = lst_compname.SelectedItem.ToString();
            pnl_comp_name.Visible = true;
            lst_compname.Visible = true;
            txt_comp_name.Focus();
        }

        double Tax_amt, amt, Taxvalue, Profit;
        string value;
        int CounterNO;
        int PartyNo;
        double ClosingQty, NetSalVal;
        double TotalamtGrossamt = 0, BillAmtTotal = 0;
        bool vStockQtyShown = false;

        #region save buttion click
        string TaxValue;
        int TaxNo;
        int AdjNO;
        SqlTransaction trans = null;
        private void btn_save_Click(object sender, EventArgs e)
        {
            //pnl_item_name.Hide();
            try
            {
                int rowcount = myDataGrid1.Rows.Count;
                if (txt_comp_name.Text != "" || rowcount != 0)
                {

                    if (myDataGrid1.Rows[0].Cells["Code"].Value.ToString() != "" || myDataGrid1.Rows[0].Cells["Name"].Value.ToString() != "")
                    {

                        amountupdate();
                        qtyupdate();

                        con.Close();
                        con.Open();

                        //for (int i = 0; i < myDataGrid1.Rows.Count; i++)
                        //{
                        //    if (myDataGrid1.Rows[i].Cells["Name"].Value != "" && myDataGrid1.Rows[i].Cells["Name"].Value != null)
                        //    {
                        //        //string itemnamevalues = "";
                        //        //itemnamevalues = DgPurchase.Rows[i].Cells["ItemNames"].Value.ToString();
                        //        //selectchkmethods(itemnamevalues);
                        //        //if (selectcount != "0")

                        //            dt1.Rows.Add(myDataGrid1.Rows[i].Cells["Code"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Name"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Unit"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Rate"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().Trim());

                        //    }
                        //}                                                     

                        int tInnNo = 0;
                        if (txt_inv_no.Text.Trim() == "")
                        {
                            tInnNo = 0;
                        }
                        else
                        {
                            tInnNo = int.Parse(txt_inv_no.Text);
                            //Convert.ToString(tInnNo) = Convert.ToString(txt_inv_no.Text);
                        }

                        SqlCommand cmd = new SqlCommand("sp_StockAdjCreate", con);
                        cmd.Transaction = trans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tCtr_name", txt_countername.Text);
                        cmd.Parameters.AddWithValue("@tLedger_name", txt_comp_name.Text);
                        cmd.Parameters.AddWithValue("@tAdjNO", AdjNO);
                        //cmd.Parameters.AddWithValue("@tDate", txt_date.Value);
                        cmd.Parameters.AddWithValue("@tDate", Convert.ToDateTime(txt_date.Value.Year + "/" + txt_date.Value.Month + "/" + txt_date.Value.Day));
                        cmd.Parameters.AddWithValue("@tInnNo", tInnNo);
                        cmd.Parameters.AddWithValue("@temp_Table", dt1);
                        //cmd.Parameters.AddWithValue("@tDt_inv", dt_inv.Value);
                        cmd.Parameters.AddWithValue("@tDt_inv", Convert.ToDateTime(dt_inv.Value.Year + "/" + dt_inv.Value.Month + "/" + dt_inv.Value.Day));
                        cmd.ExecuteNonQuery();
                        con.Close();
                        dt1.Rows.Clear();

                        MyMessageBox.ShowBox("Saved successfully", "Message");
                        //dtNew.Rows.Clear();
                        dt1.Rows.Clear();
                        //myDataGrid1.Focus();
                        lbl_amt.Text = "0.00";
                        //btn_save.BackColor = Color.Transparent;
                        txt_remarks.Text = "";
                        PnlStock.Visible = false;
                        txt_comp_name.Text = "";
                        txt_countername.Text = "";
                        txt_date.Focus();
                        //btn_save.Enabled = false;
                        // adjno();

                        string mbarcode = "";
                        con.Open();

                        for (int irow = 0; irow < myDataGrid1.Rows.Count - 1; irow++)
                        {
                            //int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);

                            if (myDataGrid1.Rows[irow].Cells["Code"].Value.ToString() == "" && myDataGrid1.Rows[irow].Cells["Name"].Value.ToString() == "")
                            {
                            }
                            else
                            {
                                // Beginning serial number insertion

                                // if (m_row_index == 0)
                                // {

                                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value != null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "")
                                {
                                    for (int j = 0; j < myDataGridadjstock.Rows.Count - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGridadjstock.Rows[j].Cells["Serialitemcode"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGridadjstock.Rows[j].Cells["SerialNo"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid2.Rows.Count - (myDataGrid2.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid2.Rows[j].Cells["Serialitemcode2"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid2.Rows[j].Cells["SerialNo2"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid3.Rows.Count - (myDataGrid3.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid3.Rows[j].Cells["Serialitemcode3"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid3.Rows[j].Cells["SerialNo3"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid4.Rows.Count - (myDataGrid4.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid4.Rows[j].Cells["Serialitemcode4"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid4.Rows[j].Cells["SerialNo4"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid5.Rows.Count - (myDataGrid5.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid5.Rows[j].Cells["Serialitemcode5"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid5.Rows[j].Cells["SerialNo5"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid6.Rows.Count - (myDataGrid6.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid6.Rows[j].Cells["Serialitemcode6"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid6.Rows[j].Cells["SerialNo6"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid7.Rows.Count - (myDataGrid7.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid7.Rows[j].Cells["Serialitemcode7"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid7.Rows[j].Cells["SerialNo7"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid8.Rows.Count - (myDataGrid8.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid8.Rows[j].Cells["Serialitemcode8"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid8.Rows[j].Cells["SerialNo8"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid9.Rows.Count - (myDataGrid9.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid9.Rows[j].Cells["Serialitemcode9"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid9.Rows[j].Cells["SerialNo9"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid10.Rows.Count - (myDataGrid10.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid10.Rows[j].Cells["Serialitemcode10"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid10.Rows[j].Cells["SerialNo10"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid11.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid11.Rows[j].Cells["Serialitemcode11"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid11.Rows[j].Cells["SerialNo11"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid12.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();

                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid12.Rows[j].Cells["Serialitemcode12"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("delete serialno_transtbl where  item_no =  '" + myDataGrid12.Rows[j].Cells["SerialNo12"].Value.ToString() + "' and barcodeno =  '" + mbarcode + "' and inout = '1'", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }
                                }

                                //}

                                //if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value != null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "")
                                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "0")
                                {

                                    for (int j = 0; j < myDataGridadjstock.Rows.Count - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGridadjstock.Rows[j].Cells["Serialitemcode"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGridadjstock.Rows[j].Cells["SerialNo"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }


                                    for (int j = 0; j < myDataGrid2.Rows.Count - (myDataGrid2.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid2.Rows[j].Cells["Serialitemcode2"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid2.Rows[j].Cells["SerialNo2"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid3.Rows.Count - (myDataGrid3.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid3.Rows[j].Cells["Serialitemcode3"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid3.Rows[j].Cells["SerialNo3"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid4.Rows.Count - (myDataGrid4.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid4.Rows[j].Cells["Serialitemcode4"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid4.Rows[j].Cells["SerialNo4"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid5.Rows.Count - (myDataGrid5.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid5.Rows[j].Cells["Serialitemcode5"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid5.Rows[j].Cells["SerialNo5"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid6.Rows.Count - (myDataGrid6.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid6.Rows[j].Cells["Serialitemcode6"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid6.Rows[j].Cells["SerialNo6"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid7.Rows.Count - (myDataGrid7.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid7.Rows[j].Cells["Serialitemcode7"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid7.Rows[j].Cells["SerialNo7"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid8.Rows.Count - (myDataGrid8.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid8.Rows[j].Cells["Serialitemcode8"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid8.Rows[j].Cells["SerialNo8"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid9.Rows.Count - (myDataGrid9.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid9.Rows[j].Cells["Serialitemcode9"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid9.Rows[j].Cells["SerialNo9"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid10.Rows.Count - (myDataGrid10.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid10.Rows[j].Cells["Serialitemcode10"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid10.Rows[j].Cells["SerialNo10"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid11.Rows.Count - (myDataGrid11.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid11.Rows[j].Cells["Serialitemcode11"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid11.Rows[j].Cells["SerialNo11"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }

                                    for (int j = 0; j < myDataGrid12.Rows.Count - (myDataGrid12.AllowUserToAddRows ? 1 : 0); j++)
                                    {
                                        mbarcode = myDataGrid1.Rows[irow].Cells["code"].Value.ToString();
                                        if (myDataGrid1.Rows[irow].Cells["code"].Value.ToString() == myDataGrid12.Rows[j].Cells["Serialitemcode12"].Value.ToString())
                                        {
                                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values('" + lbl_adjust_no.Text.Trim() + "','" + myDataGrid12.Rows[j].Cells["SerialNo12"].Value.ToString() + "','" + mbarcode + "','1')", con);
                                            cmdserial.ExecuteNonQuery();
                                        }
                                    }
                                }
                                // Ending Serial Number Insertion                                   
                            }
                        }
                        dtNew.Rows.Clear();
                        con.Close();
                        adjno();
                    }

                    else
                    {
                        MyMessageBox.ShowBox("No transaction is Made", "Warning");
                        txt_date.Focus();
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
                // MessageBox.Show(ex.Message);
                funException(ex);
            }

        }
        public static void funException(Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            //StackFrame frame = st.GetFrame(0);
            StackFrame frame = st.GetFrame(st.FrameCount - 1);
            string strfname1 = frame.GetFileName();
            string strfname = frame.GetMethod().Name;
            var line = st.GetFrame(st.FrameCount - 1).GetFileLineNumber();
            if (strfname1 != null)
            {
                frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname1));
            }
            else
            {
                frmException.ShowBox(ex.Message, "Warning", Convert.ToString(line), Convert.ToString(strfname));
            }

        }
        #endregion
        #region entry of Adj_bill and totalamt
        public void amountupdate()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Amountupdate", con);
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tCtr_name", txt_countername.Text);
            cmd.Parameters.AddWithValue("@tTotalAmt", lbl_amt.Text);
            cmd.Parameters.AddWithValue("@tDate", txt_date.Value);
            cmd.Parameters.Add("@tStrNo", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            AdjNO = Convert.ToInt32(cmd.Parameters["@tStrNo"].Value);
            con.Close();

            //SqlCommand adj_mastbl = new SqlCommand("insert into adjmas_table(Adj_No,Adj_Billprefix,Adj_Billsuffix,Adj_Billno,Adj_bill,Ctr_No,Adj_Date,Adj_Time,Adj_Type,Godown_No,Togodown_No,Transfer_Type,Branch_Godown,UserNo,NetAmount,Adj_Remarks,Export,Cancel,CancelRemarks) values('" + AdjNO + "','','','" + adj_billno + "','" + adj_billno + "','" + ctr_no + "',@C2,@C3,'0','2','0','0','','0','" + totalamt + "','','0','0','')", con);
            //adj_mastbl.Parameters.Add(new SqlParameter("@C2", SqlDbType.DateTime));
            //adj_mastbl.Parameters.Add(new SqlParameter("@C3", SqlDbType.DateTime));
            //adj_mastbl.Parameters["@C2"].Value = txt_date.Text;
            //adj_mastbl.Parameters["@C3"].Value = dtime;
            //adj_mastbl.ExecuteNonQuery();
            //con.Close();

        }
        #endregion

        #region quantity updation
        public void qtyupdate()
        {
            try
            {
                dt1.Rows.Clear();
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

                            dt1.Rows.Add(myDataGrid1.Rows[i].Cells["Code"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Name"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Unit"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Less_Qty"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Add_Qty"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Rate"].Value.ToString().Trim(), myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().Trim());
                        }
                    }
                }

                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Qtyupdate", con);
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@temp_Table", dt1);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion



        bool check = false;
        private void myDataGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            string ie = Convert.ToString(e.RowIndex.ToString());

            //if ((myDataGrid1.CurrentCell.ColumnIndex == 1 || myDataGrid1.CurrentCell.ColumnIndex == 2|| myDataGrid1.CurrentCell.ColumnIndex == 3 || myDataGrid1.CurrentCell.ColumnIndex == 4 || myDataGrid1.CurrentCell.ColumnIndex == 5) && myDataGrid1.CurrentRow != null)
            //{
            //    // if ( myDataGrid1.Rows[e.RowIndex].Cells["ItemCode"].Value == null )
            //    {

            //    }
            //    if (myDataGrid1.CurrentCell.ColumnIndex > 0 && myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() == "")
            //    {
            //if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() == "" && myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString() == "")
            //{
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 2)
            //    {
            //        MyMessageBox1.ShowBox("Please Enter Correct ItemName", "Warning");
            //        int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
            //        SetColumnIndex method = new SetColumnIndex(Mymethod);
            //        this.myDataGrid1.BeginInvoke(method, nextindex - 1);
            //    }
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            //    {
            //        MyMessageBox1.ShowBox("Please Enter Correct ItemName", "Warning");
            //        int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
            //        SetColumnIndex method = new SetColumnIndex(Mymethod);
            //        this.myDataGrid1.BeginInvoke(method, nextindex - 3);
            //    }
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //    {
            //        MyMessageBox1.ShowBox("Please Enter Correct ItemName", "Warning");
            //        int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
            //        SetColumnIndex method = new SetColumnIndex(Mymethod);
            //        this.myDataGrid1.BeginInvoke(method, nextindex - 3);
            //    }

            //}

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
                            //txt_remarks.Focus();
                            break;
                        }
                    }
                }
                else
                {
                    PnlStock.Visible = true;
                    lblStk.Visible = true;
                    lblStockQty.Visible = true;

                    int vRow = myDataGrid1.CurrentRow.Index;

                    DataTable dtStockQty = new DataTable();
                    DataTable dtSalesQty = new DataTable();
                    SqlCommand cmd = new SqlCommand(" Select a1.item_no as item_code,a2.item_name,isnull((nt_cloqty),0) as nt_cloqty from stktrn_table a1,item_table a2 " +
                                                " where a1.item_no=a2.item_no and item_name='" + myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value + "' and a2.item_Code ='" + myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value + "'  " +
                                                " and a1.strn_type in(0,3,2,12) and a1.strn_date<=@tStart " +
                                                " group by  a1.item_no,a2.item_name,a2.item_cost,a2.nt_cloqty ", con);


                    cmd.Parameters.AddWithValue("@tStart", new DateTime(txt_date.Value.Year, txt_date.Value.Month, txt_date.Value.Day));
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dtStockQty.Rows.Clear();
                    adp.Fill(dtStockQty);



                    //SqlCommand cmd1 = new SqlCommand(" Select a1.item_no as item_code,a2.item_name,isnull(sum(nt_cloqty),0) as nt_cloqty from stktrn_table a1,item_table a2 " +
                    //                                " where a1.item_no=a2.item_no and item_name='" + myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value + "' and a2.item_Code ='" + myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value + "'  " +
                    //                                " and a1.strn_type in(1,11) and a1.strn_date<=@tStart " +
                    //                                " group by  a1.item_no,a2.item_name,a2.item_cost ", con);

                    SqlCommand cmd1 = new SqlCommand(" Select a1.item_no as item_code,a2.item_name,isnull((nt_cloqty),0) as nt_cloqty from stktrn_table a1,item_table a2 " +
                                                    " where a1.item_no=a2.item_no and item_name='" + myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value + "' and a2.item_Code ='" + myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value + "'  " +
                                                    " and a1.strn_type in(1,11) and a1.strn_date<=@tStart " +
                                                    " group by  a1.item_no,a2.item_name,a2.item_cost,a2.nt_cloqty ", con);


                    cmd1.Parameters.AddWithValue("@tStart", new DateTime(txt_date.Value.Year, txt_date.Value.Month, txt_date.Value.Day));
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    dtSalesQty.Rows.Clear();
                    adp1.Fill(dtSalesQty);

                    if (dtStockQty.Rows.Count > 0)
                    {
                        if (dtSalesQty.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtSalesQty.Rows.Count; j++)
                            {
                                for (int i = 0; i < dtStockQty.Rows.Count; i++)
                                {
                                    if (dtStockQty.Rows[i][1].ToString() == dtSalesQty.Rows[j][1].ToString())
                                    {

                                        //double CurStockQty = Convert.ToDouble(dtStockQty.Rows[i]["nt_cloqty"]) - (Convert.ToDouble(dtSalesQty.Rows[j]["nt_cloqty"]));
                                        //munies code
                                        double CurStockQty = Convert.ToDouble(dtStockQty.Rows[i]["nt_cloqty"]);
                                        if (vStockQtyShown == false)
                                        {
                                            lblStockQty.Text = "0.00";
                                            lblStockQty.Text = CurStockQty.ToString();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {

                            double CurStockQty = Convert.ToDouble(dtStockQty.Rows[0]["nt_cloqty"]);

                            if (vStockQtyShown == false)
                            {
                                lblStockQty.Text = "0.00";
                                lblStockQty.Text = CurStockQty.ToString();
                            }
                        }
                    }
                    else if (dtStockQty.Rows.Count == 0 && dtSalesQty.Rows.Count == 0)
                    {
                        DataTable dt1StockQty = new DataTable();
                        SqlCommand cmd9 = new SqlCommand(" Select item_code, item_name,isnull((nt_cloqty),0) as nt_cloqty from item_table  " +
                                                    " where item_name='" + myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value + "' and item_Code ='" + myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value + "'", con);
                        //" group by  a1.item_no,a2.item_name,a2.item_cost,a2.nt_cloqty ", con);

                        //cmd.Parameters.AddWithValue("@tStart", new DateTime(txt_date.Value.Year, txt_date.Value.Month, txt_date.Value.Day));
                        SqlDataAdapter adp9 = new SqlDataAdapter(cmd9);
                        dtStockQty.Rows.Clear();
                        adp9.Fill(dt1StockQty);

                        double CurStockQty = Convert.ToDouble(dt1StockQty.Rows[0]["nt_cloqty"]);
                        if (CurStockQty > 0)
                            lblStockQty.Text = CurStockQty.ToString();
                        else
                            lblStockQty.Text = "0.00";

                    }
                    else if (dtStockQty.Rows.Count == 0 && dtSalesQty.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtSalesQty.Rows.Count; i++)
                        {
                            string CurStockQty = dtSalesQty.Rows[i]["nt_cloqty"].ToString();
                            if (vStockQtyShown == false)
                            {
                                lblStockQty.Text = "0.00";
                                lblStockQty.Text = CurStockQty.ToString();
                            }
                        }
                    }
                }
            }

            if (e.ColumnIndex == 3)
            {
                if (myDataGrid1.CurrentCell.ColumnIndex == 3)
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() == "")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = "0";
                    }
                    else
                    {
                        if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "0")
                        {
                            PnlStock.Visible = false;
                            lblStk.Visible = false;
                            lblStockQty.Visible = false;
                        }
                    }
                }
            }

            if (e.ColumnIndex == 4)
            {
                if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "0")
                    {
                        PnlStock.Visible = false;
                        lblStk.Visible = false;
                        lblStockQty.Visible = false;
                    }

                    for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                    {
                        if (lbl_amt.Text == "")
                        {
                            lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString()));
                        }
                        else
                        {
                            //if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                            if (myDataGrid1.Rows[i].Cells["Amount"].Value != null && myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
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
                            lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString()));
                        }
                        else
                        {
                            //if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                            if (myDataGrid1.Rows[i].Cells["Amount"].Value != null && myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                            {
                                amount += double.Parse(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());
                            }
                        }
                        lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(amount.ToString()));
                    }
                    amount = 0;
                }
            }
            if (e.ColumnIndex == 6)
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

            //    }
            //}


            ////back color of each cell:
            //myDataGrid1[e.ColumnIndex, e.RowIndex].Style.SelectionBackColor = Color.Blue;
            ////load a item name into list box and display into gridview:
            //if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //{
            //    //ItemNameload();
            //    ItemNameloadbyname();
            //    pnl_item_name.Visible = true;
            //    lst_itemname.Visible = true;

            //    SqlCommand cmd = new SqlCommand("select Item_name from Item_table where Item_code='" + id + "'", con);
            //    SqlDataReader dr3 = null;
            //    con.Close();
            //    con.Open();
            //    dr3 = cmd.ExecuteReader();
            //    int i = 0;
            //    while (dr3.Read())
            //    {
            //        i = 1;
            //        string name = dr3["Item_name"].ToString();

            //        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value = name;
            //        break;
            //    }
            //    if (i == 0)
            //    {
            //        MessageBox.Show("Item code not found in the list");
            //    }
            //    con.Close();
            //    con.Open();
            //    SqlCommand cmd2 = new SqlCommand("select unit_name from unit_table", con);
            //    SqlDataReader idr2;
            //    idr2 = cmd2.ExecuteReader();
            //    if (idr2.HasRows)
            //    {
            //        int chkunit = 0;
            //        while (idr2.Read())
            //        {
            //            chkunit = 1;
            //            unit = idr2["unit_name"].ToString();
            //            myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Unit"].Value = unit;
            //            break;
            //        }
            //        con.Close();
            //        idr2.Dispose();
            //        if (chkunit == 1)
            //        {

            //        }

            //    }
            //    // for check the item value is already entered:
            //    //if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Item_code"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value != null)
            //    //{
            //        string t1 = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            //        int t2 = e.RowIndex;
            //        for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
            //        {
            //            if (t2 != j)
            //            {
            //                if (t1 == myDataGrid1.Rows[j].Cells["Name"].Value.ToString())
            //                {
            //                    MessageBox.Show("selected item is already entered");

            //                    break;
            //                }
            //            }

            //       // }

            //    }

            //    if (myDataGrid1.CurrentCell.ColumnIndex == 2)
            //    {
            //        pnl_item_name.Visible = false;
            //        lst_itemname.Visible = false;

            //    }


            //}
        }

        string unit = string.Empty;
        int temrowcur;
        #region To fill a datas on datagriid view
        DataTable dt = new DataTable();
        public void getbyid(string id, string name)
        {
            //id = myDataGrid1.Rows[temrowcur].Cells["Item_code"].Value.ToString();
            // pnl_item_name.Visible = true;
            //lst_itemname.Visible = true;
            SqlCommand cmd = new SqlCommand("select Item_name,Item_code,Item_cost from Item_table where Item_code='" + id + "' or Item_name='" + name + "' or Item_no=(select Item_No from BarCode_table where BarCode='" + id + "')", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            adp.Fill(dt);

            int i = 0;
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; )
                {
                    i = 1;
                    string name1 = dt.Rows[j]["Item_name"].ToString();

                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value = name1;
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Rate"].Value = dt.Rows[j]["Item_cost"].ToString();
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = "0";
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value = "0";
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Amount"].Value = "0";
                    break;
                }
            }

            if (i == 0)
            {
                //MessageBox.Show("Item code not found in the list");
                //int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                //SetColumnIndex method = new SetColumnIndex(Mymethod);
                //myDataGrid1.BeginInvoke(method, 3);
            }
            else
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
                        myDataGrid1.BeginInvoke(method, 3);
                    }
                }
            }
        }
        #endregion

        private void txt_date_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_countername.Focus();
            }
        }
        string invoiceno;
        private void txt_inv_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txt_inv_no.Text == "")
                {
                    dt_inv.Focus();
                    invoiceno = "0";
                }
                else
                {
                    dt_inv.Focus();
                    invoiceno = txt_inv_no.Text;
                }
            }
        }

        private void dt_inv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txt_comp_name.Focus();
            }
            if (e.KeyChar == (Char)Keys.Back)
            {
                txt_inv_no.Focus();

            }
        }
        string txtCompanyname;
        private void txt_comp_name_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        DataTable dtName = new DataTable();
        Double quantity, addcheck, lesscheck, rate;
        public void fecthitemnamevalues(string itemname)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                SqlCommand namecmd = new SqlCommand("select Item_code,Item_cost,stock_type from Item_table where Item_name='" + itemname + "'", con);
                SqlDataAdapter adp = new SqlDataAdapter(namecmd);

                dtName.Rows.Clear();
                adp.Fill(dtName);

                int i = 0;
                if (dtName.Rows.Count > 0)
                {
                    for (int j = 0; j < dtName.Rows.Count; j++)
                    {
                        i = 1;
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = dtName.Rows[j]["Item_cost"].ToString();
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value = dtName.Rows[j]["Item_code"].ToString();
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Stock_Category"].Value = dtName.Rows[j]["stock_type"].ToString();
                        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
                        SetColumnIndex method = new SetColumnIndex(Mymethod);
                        myDataGrid1.BeginInvoke(method, 3);
                        //dtNew.Rows.Add("", "", "", "", "", "", "");
                        myDataGrid1.DataSource = dtNew.DefaultView;
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
            catch (Exception ex)
            {
                funException(ex);
            }
        }
        //public void getbyid()
        //{

        //    if (con.State != ConnectionState.Open)
        //    {
        //        con.Open();
        //    }
        //    con.Close();
        //    con.Open();
        //    SqlCommand namecmd = new SqlCommand("select Item_name,Item_mrsp from Item_table where Item_code='" + itemid + "'", con);
        //    SqlDataReader dread;
        //    dread = namecmd.ExecuteReader();
        //    if (dread.Read())
        //    {
        //        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Item_name"].Value = dread["Item_name"].ToString();
        //        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = dread["Item_mrsp"].ToString();
        //       // myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Unit"];
        //    }

        //    con.Close();

        //}

        //string itementered;
        //string itemid;
        //private void myDataGrid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //if (myDataGrid1.CurrentCell.ColumnIndex == 0)
        //{
        //    if (myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString() != "")
        //    {
        //        itemid = myDataGrid1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
        //        getbyid(itemid);
        //    }
        //    else
        //    {
        //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
        //        SetColumnIndex method = new SetColumnIndex(Mymethod);
        //        myDataGrid1.BeginInvoke(method, nextindex-1);
        //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Name"];
        //    }
        //}
        //if (myDataGrid1.CurrentCell.ColumnIndex == 1)
        //{
        //    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() != "")
        //    {
        //        itementered = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
        //        fecthitemnamevalues();
        //    }
        //}
        //if (myDataGrid1.CurrentCell.ColumnIndex == 3)
        //{
        //    if (myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() =="0"||myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString() =="")
        //    {
        //        myDataGrid1.Select();
        //        myDataGrid1.CurrentCell.Selected = true;
        //    }
        //    else
        //    {
        //        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = 0;
        //        quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
        //        rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);

        //        Double price = quantity * rate;
        //        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;
        //        total += price;
        //        //lbl_amt.Text = total.ToString();
        //        lbl_amt.Text = String.Format("{0:0.00}", total);
        //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
        //        SetColumnIndex method = new SetColumnIndex(Mymethod);
        //        myDataGrid1.BeginInvoke(method, 6);
        //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"];
        //    }
        //}
        //if (myDataGrid1.CurrentCell.ColumnIndex == 4)
        //{
        //    //myDataGrid1.Select();
        //    //myDataGrid1.CurrentCell.Selected = true;

        //    if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString() == "")
        //    {
        //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
        //        SetColumnIndex method = new SetColumnIndex(Mymethod);
        //        myDataGrid1.BeginInvoke(method, 4);
        //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"];
        //        myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = 0;
        //    }
        //    else
        //    {

        //        myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = 0;
        //        quantity = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value);
        //        rate = Convert.ToDouble(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value);

        //        Double price = quantity * rate;
        //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price;
        //        total += price;
        //        //lbl_amt.Text = total.ToString();
        //        lbl_amt.Text = String.Format("{0:0.00}", total);
        //        int nextindex = Math.Min(myDataGrid1.Columns.Count - 1, myDataGrid1.CurrentCell.ColumnIndex + 1);
        //        SetColumnIndex method = new SetColumnIndex(Mymethod);
        //        myDataGrid1.BeginInvoke(method, 6);
        //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Amount"];
        //    }
        //}

        //if (myDataGrid1.CurrentCell.ColumnIndex == 5)
        //{

        //    if (addcheck == 0)
        //    {

        //        quantity = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString().Trim());
        //        rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
        //        Double price = quantity * rate;
        //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
        //        total += price;
        //        lbl_amt.Text = total + ".00";
        //    }

        //    if (lesscheck == 0)
        //    {

        //        quantity = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString().Trim());
        //        rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
        //        Double price = quantity * rate;
        //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
        //        total += price;
        //        lbl_amt.Text = total + ".00";
        //    }
        //}
        //}
        //***************
        // int quantity, addcheck, lesscheck, rate;
        //int amtindex = myDataGrid1.CurrentCell.ColumnIndex;
        //int lessqty = myDataGrid1.CurrentCell.ColumnIndex;
        //int addqty = myDataGrid1.CurrentCell.ColumnIndex;
        //int codecheck = myDataGrid1.CurrentCell.ColumnIndex;

        //if (codecheck == 0)
        //{

        //}

        //if (lessqty == 3)
        //{
        //    myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = "0";
        //    addcheck = 0;
        //    lesscheck = 1;

        //    if (myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value != null)
        //    {
        //        quantity = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString().Trim());
        //        rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
        //        int price = quantity * rate;
        //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
        //        total += price;
        //        lbl_amt.Text = total + ".00";
        //        myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Amount"];
        //    }
        //}
        //if (addqty == 4)
        //{

        //    myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value = "0";
        //    lesscheck = 0;
        //    addcheck = 1;
        //    if (myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value != null)
        //    {

        //        quantity = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString().Trim());
        //        rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
        //        int price = quantity * rate;
        //        myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
        //        total += price;
        //        lbl_amt.Text = total + ".00";
        //    }
        //    else
        //    {
        //        double k = 0.00;
        //        myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value = k.ToString("0.00");
        //    }
        //}

        //if (amtindex == 5)
        //{

        //    if (addcheck == 0)
        //    {

        //        //quantity = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Less_Qty"].Value.ToString().Trim());
        //        //rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
        //        //int price = quantity * rate;
        //        //myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
        //        //total += price;
        //        //lbl_amt.Text = total + ".00";
        //    }

        //    if (lesscheck == 0)
        //    {

        //        //quantity = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Add_Qty"].Value.ToString().Trim());
        //        //rate = Convert.ToInt16(myDataGrid1.Rows[e.RowIndex].Cells["Rate"].Value.ToString().Trim());
        //        //int price = quantity * rate;
        //        //myDataGrid1.Rows[e.RowIndex].Cells["Amount"].Value = price.ToString();
        //        //total += price;
        //        //lbl_amt.Text = total + ".00";
        //    }


        //}

        private void hideColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int current = myDataGrid1.CurrentCell.ColumnIndex;
            myDataGrid1.Columns.RemoveAt(current);
            //this.myDataGrid1.Columns["Code"].Visible = false;
            //myDataGrid1.Rows[0].Cells[1].Visible = false;
        }

        private void unHideColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unhideform unhide = new unhideform();
            unhide.Show();
            //int current = myDataGrid1.CurrentCell.ColumnIndex;
            //DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            //col.HeaderText = "Code";
            //myDataGrid1.Columns.Insert(4, col);
        }

        string columnName = string.Empty;
        private void myDataGrid1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //int iColumn = myDataGrid1.CurrentCell.ColumnIndex;
            //int iRow = myDataGrid1.CurrentCell.RowIndex;

            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 5)
            //    {

            //        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];

            //    }
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //    {
            //        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];

            //    }
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            //    { 
            //        string less= myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString();
            //        if (less != "0")
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];

            //        }
            //        else
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Add_Qty"];
            //        }
            //    }
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            //    {
            //        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Name"];
            //    }

            //    if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //    {
            //        string itemname= myDataGrid1.Rows[iRow].Cells["Name"].Value.ToString();
            //        if (itemname!=null)
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Less_Qty"];
            //        }
            //    }

            //    if (myDataGrid1.CurrentCell.ColumnIndex == 6)
            //    {
            //        myDataGrid1.Rows.Add();
            //        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow + 1].Cells["Item_code"];
            //    }

            //}


        }

        private void myDataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (myDataGrid1.CurrentCell.ColumnIndex == 3)
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() == "")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = "0";
                    }
                    else
                    {
                        if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "0")
                        {
                            PnlStock.Visible = false;
                            lblStk.Visible = false;
                            lblStockQty.Visible = false;
                        }
                    }
                }

                //PnlStock.Visible = true;
                //lblStk.Visible = true;
                //lblStockQty.Visible = true;

                //int vRow = myDataGrid1.CurrentRow.Index;

                //DataTable dtStockQty = new DataTable();
                //DataTable dtSalesQty = new DataTable();

                //SqlCommand cmd = new SqlCommand(" Select a1.item_no as item_code,a2.item_name,isnull(sum(nt_qty),0) as nt_cloqty from stktrn_table a1,item_table a2 " +
                //                                " where a1.item_no=a2.item_no and item_name='" + myDataGrid1.Rows[vRow].Cells["Name"].Value + "' and a2.item_Code ='" + myDataGrid1.Rows[0].Cells["Code"].Value + "'  " +
                //                                " and a1.strn_type in(0,3,12,2) and a1.strn_date<=@tStart " +
                //                                " group by  a1.item_no,a2.item_name,a2.item_cost ", con);

                //cmd.Parameters.AddWithValue("@tStart", new DateTime(txt_date.Value.Year, txt_date.Value.Month, txt_date.Value.Day));
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //dtStockQty.Rows.Clear();
                //adp.Fill(dtStockQty);

                //SqlCommand cmd1 = new SqlCommand(" Select a1.item_no as item_code,a2.item_name,isnull(sum(nt_qty),0) as nt_cloqty from stktrn_table a1,item_table a2 " +
                //                                " where a1.item_no=a2.item_no and item_name='" + myDataGrid1.Rows[vRow].Cells["Name"].Value + "' and a2.item_Code ='" + myDataGrid1.Rows[0].Cells["Code"].Value + "'  " +
                //                                " and a1.strn_type in(1,11) and a1.strn_date<=@tStart " +
                //                                " group by  a1.item_no,a2.item_name,a2.item_cost ", con);

                //cmd1.Parameters.AddWithValue("@tStart", new DateTime(txt_date.Value.Year, txt_date.Value.Month, txt_date.Value.Day));
                //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                //dtSalesQty.Rows.Clear();
                //adp1.Fill(dtSalesQty);

                //if (dtStockQty.Rows.Count > 0)
                //{
                //    if (dtSalesQty.Rows.Count > 0)
                //    {
                //        for (int j = 0; j < dtSalesQty.Rows.Count; j++)
                //        {
                //            for (int i = 0; i < dtStockQty.Rows.Count; i++)
                //            {
                //                if (dtStockQty.Rows[i][1].ToString() == dtSalesQty.Rows[j][1].ToString())
                //                {

                //                    lblStockQty.Text = (Convert.ToDouble(dtStockQty.Rows[i]["nt_cloqty"]) - (Convert.ToDouble(dtSalesQty.Rows[j]["nt_cloqty"]))).ToString();
                //                }
                //            }
                //        }
                //    }
                //}
            }
            //if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //{
            //    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "")
            //    {
            //        //myDataGrid1.CurrentCell = myDataGrid1.Rows[e.RowIndex].Cells["Rate"];
            //    }
            //    else
            //    {
            //        if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            //        {
            //            string result = MyMessageBox.ShowBox("Empty Quantity", "Warning!");

            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"];
            //        }
            //    }
            //}                            

            //int iRow = myDataGrid1.CurrentCell.RowIndex;

            ////if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Item_code"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value != null)
            ////{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        if (myDataGrid1.CurrentCell.ColumnIndex == 5)
            //        {

            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];

            //        }
            //        if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];

            //        }
            //        if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            //        {
            //            if (myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value != null)
            //            {

            //                string less = myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString();
            //                if (less != "0")
            //                {
            //                    myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];

            //                }
            //            }
            //            else
            //            {
            //                myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Add_Qty"];
            //            }
            //        }
            //        if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            //        {
            //            pnl_ctrname.Visible = true;
            //            lst_ctrname.Visible = true;
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Name"];
            //        }

            //        if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //        {
            //            string itemname = myDataGrid1.Rows[iRow].Cells["Name"].Value.ToString();
            //            if (itemname != null)
            //            {
            //                myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Less_Qty"];
            //            }
            //            lst_itemname.Visible = false;
            //            pnl_item_name.Visible = false;
            //        }

            //        if (myDataGrid1.CurrentCell.ColumnIndex == 6)
            //        {
            //            myDataGrid1.Rows.Add();
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow + 1].Cells["Item_code"];
            //        }


            //    }
            ////}

            //double mn = 0.00;
            //******************************************
            //double totQty = 0.00;
            //if (e.KeyCode == Keys.Enter)
            //{
            //    int iRow = myDataGrid1.CurrentCell.RowIndex;
            //    double mn = 0.00;

            //    if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            //    {
            //        if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString() == "")
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"];
            //        }
            //        else
            //        {
            //            string itemid1=myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString();
            //            getbyid(itemid1);
            //        }
            //*******************************
            ////if (e.KeyCode == Keys.Enter)
            ////{
            ////    // ItemNameload();

            ////    id = Convert.ToString(myDataGrid1.Rows[iRow].Cells[0].Value);
            ////    DataTable dt_items_name = new DataTable();
            ////    SqlCommand cmd1 = new SqlCommand("select * from Item_table where Item_code='" + id + "' order by Item_name ASC", con);
            ////    SqlDataAdapter adp2 = new SqlDataAdapter(cmd1);
            ////    dt_items_name.Rows.Clear();
            ////    adp2.Fill(dt_items_name);
            ////    if (dt_items_name.Rows.Count > 0)
            ////    {
            ////        myDataGrid1.Rows[iRow].Cells["Name"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
            ////        //myDataGrid1.Rows[iRow].Cells["Unit"].Value = dt_items_name.Rows[0][""].ToString();
            ////        // myDataGrid1.Rows[iRow].Cells["Name"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
            ////        string unit_no = dt_items_name.Rows[0]["Unit_no"].ToString();
            ////        DataTable dt_unit_no1 = new DataTable();
            ////        SqlCommand cmd2 = new SqlCommand("select * from unit_table where unit_no='" + unit_no + "' ", con);
            ////        SqlDataAdapter adp3 = new SqlDataAdapter(cmd2);
            ////        dt_unit_no1.Rows.Clear();
            ////        adp3.Fill(dt_unit_no1);
            ////        myDataGrid1.Rows[iRow].Cells["Unit"].Value = dt_unit_no1.Rows[0]["unit_name"].ToString();



            ////        // myDataGrid1.Rows[iRow].Cells["Unit"].Value = dt_items_name.Rows[0]["Unit_no"].ToString();
            ////        //  myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
            ////        // myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
            ////        myDataGrid1.Rows[iRow].Cells["Rate"].Value = dt_items_name.Rows[0]["Item_cost"].ToString();
            ////        // myDataGrid1.Rows[iRow].Cells["Amount"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
            ////    }
            ////    con.Close();
            ////    //  adp.Dispose();

            ////    myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Name"];
            ////   // ItemNameloadbyname();
            ////   // pnl_item_name.Visible = true;
            ////  //  lst_itemname.Visible = true;
            ////}
            //}
            //***********************************
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //    {
            //        if (myDataGrid1.Rows[iRow].Cells["Name"].Value.ToString() != "" && myDataGrid1.Rows[iRow].Cells["Code"].Value.ToString() != "")
            //        {

            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Name"];
            //            myDataGrid1.Rows[iRow].Selected = true;
            //            //pnl_item_name.Visible = false;
            //            //lst_itemname.Visible = false;
            //        }
            //        else
            //        {
            //            txt_remarks.Focus();

            //        }
            //    }
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 2)
            //    {
            //        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Less_Qty"];
            //        pnl_item_name.Visible = false;
            //        lst_itemname.Visible = false;

            //    }
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            //    {

            //            if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
            //            {
            //                myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value = 0;
            //                myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"];
            //            }
            //            else
            //            {
            //                quantity = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value);
            //                rate = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value);

            //                Double price = quantity * rate;
            //                myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = price;
            //                total += price;
            //                lbl_amt.Text = total + ".00";
            //                myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"];
            //            }

            //    }
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //    {
            //        pnl_item_name.Visible = false;
            //        lst_itemname.Visible = false;
            //        if (myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value.ToString()!="" )
            //        {
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];
            //        }
            //        else
            //        {
            //            string result = MyMessageBox.ShowBox("Empty Quantity", "Warning!");
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Add_Qty"];
            //        }

            //    }
            //    else if (myDataGrid1.CurrentCell.ColumnIndex == 5)
            //    {
            //        pnl_item_name.Visible = false;
            //        lst_itemname.Visible = false;
            //        double j = 0;
            //        if (myDataGrid1.Rows[iRow].Cells["Rate"].Value.ToString() != "")
            //        {
            //            j = Convert.ToDouble(myDataGrid1.Rows[iRow].Cells["Rate"].Value);
            //            if (myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString() != "")
            //                //double amount_not=
            //                Convert.ToDouble(myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value);
            //            double add = Convert.ToDouble(myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value);
            //            double amount = 0.00;
            //            amount = (j * add);
            //            total += amount;
            //            lbl_amt.Text = total.ToString();
            //            myDataGrid1.Rows[iRow].Cells["Amount"].Value = amount.ToString();

            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];
            //        }
            //        if (myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value.ToString() != "")
            //        {
            //            double add = Convert.ToDouble(myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value);
            //            double amount = 0.00;
            //            amount = (j * add);
            //            total += amount;
            //            lbl_amt.Text = total.ToString();
            //            myDataGrid1.Rows[iRow].Cells["Amount"].Value = amount.ToString();
            //            double k = 0.00;
            //            myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value = k.ToString();
            //            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];
            //        }
            //    }
            //    //   myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];
            //    else if (myDataGrid1.Rows[iRow].Cells["Rate"].Value.ToString() != "")
            //    {
            //        if (myDataGrid1.Rows[iRow].Cells["Rate"].Value.ToString() != "")
            //        {
            //            if (myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value.ToString() == "")
            //            {
            //                double k = 0.00;
            //                myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value = k.ToString();
            //                myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];
            //            }
            //            if (myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value.ToString() == "")
            //            {
            //                double k = 0.00;
            //                myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value = k.ToString();
            //                myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Amount"];
            //            }
            //        }

            //        pnl_item_name.Visible = false;
            //        lst_itemname.Visible = false;
            //        dtNew.Rows.Add();
            //        myDataGrid1.AllowUserToAddRows = true;
            //        //dtDisplay.Rows.Add();
            //        myDataGrid1.AllowUserToAddRows = false;
            //        //myDataGrid1.Focus();

            //        //myDataGrid1.Rows.Add();
            //        myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow + 1].Cells[0];
            //    }

            //}
            //if (e.KeyCode == Keys.Down)
            //{
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //    {
            //        lst_itemname.Focus();
            //        //myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value = lst_itemname.SelectedItem.ToString();
            //    }
            //}
            //if (e.KeyCode == Keys.Up)
            //{
            //    if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //    {
            //        lst_itemname.Focus();

            //    }
            //**********************************
            //{
            //    //for (int j = 0; j < myDataGrid1.Rows.Count; j++)
            //    //{
            //    //    if (myDataGrid1.Rows[j].Cells["Qty"].Value != null)
            //    //    {
            //    totQty += Convert.ToDouble(myDataGrid1.Rows[j].Cells[" "].Value);
            //    //    }
            //    ////    lblQtyValues.Text = totQty.ToString();
            //    //}
            //    //myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells["Rate"];
            //}
            //else if (dataGridView1.CurrentCell.ColumnIndex == 4)
            //{
            //    dataGridView1.CurrentCell = dataGridView1.Rows[iRow].Cells["Amount"];
            //}
            //else if (dataGridView1.CurrentCell.ColumnIndex == 5)
            //{
            //    if (dataGridView1.Rows[iRow].Cells["Amount"].Value != null)
            //    {

            //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //        {
            //            mn += (Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value));
            //        }
            //        dataGridView1.Rows.Add();

            //        lblTotalAmount.Text = mn.ToString();
            //        dataGridView1.CurrentCell = dataGridView1.Rows[iRow + 1].Cells["Code"];
            //    }
            //}
            //**************
            // }

            // back space event to previous cell:
            //if (e.KeyCode == Keys.Back)
            //  {
            //      foreach (DataGridViewRow backspace in myDataGrid1.Rows)
            //      {
            //          if (!backspace.IsNewRow)
            //          {
            //              if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            //              {
            //                  if (myDataGrid1.Rows.Count == 0)
            //                  {
            //                      myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Item_code"];
            //                  }
            //                  else
            //                  {
            //                      if (myDataGrid1.Rows.Count > 0)
            //                      {
            //                          myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex-1].Cells["Amount"];
            //                      }
            //                  }

            //              }
            //              else if (myDataGrid1.CurrentCell.ColumnIndex == 1)
            //              {
            //                  myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Item_code"];
            //              }
            //              else if (myDataGrid1.CurrentCell.ColumnIndex == 2)
            //              {
            //                  myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"];
            //              }
            //              else if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            //              {
            //                  myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Unit"];
            //              }
            //              else if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            //              {
            //                  myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"];
            //              }
            //              else if (myDataGrid1.CurrentCell.ColumnIndex == 5)
            //              {
            //                  myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"];
            //              }
            //              else if (myDataGrid1.CurrentCell.ColumnIndex == 6)
            //              {
            //                  myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"];
            //              }
            //          }
            //      }
            //  }
        }


        private void txt_date_Leave(object sender, EventArgs e)
        {
            //txt_countername.Focus();

            // countload();
            // lst_ctrname.Focus();
            //txt_countername.Text = lst_ctrname.SelectedItem.ToString();
        }

        private void txt_countername_Leave(object sender, EventArgs e)
        {

            pnl_ctrname.Visible = false;
            lst_ctrname.Visible = false;
            // txt_inv_no.Focus();
        }

        int Column;
        private void txt_countername_KeyDown(object sender, KeyEventArgs e)
        {
            // lst_ctrname.Focus();
            //txt_countername.Text = lst_ctrname.SelectedItem.ToString();

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
                int count = Convert.ToInt16(myDataGrid1.Rows.Count);
                if (txt_countername.Text != "")
                {

                    //txt_countername.Text = lst_ctrname.SelectedItem.ToString();
                    //pnl_ctrname.Visible = false;
                    //txt_inv_no.Focus();

                    if (count == 0)
                    {
                        dtNew.Rows.Add();
                        myDataGrid1.AllowUserToAddRows = true;
                        myDataGrid1.Focus();
                    }
                    else
                    {
                        // myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"];
                        //if(Column == myDataGrid1.Rows.Count-1)
                        myDataGrid1.Focus();
                    }
                }
                else
                {
                    pnl_ctrname.Visible = false;
                    txt_inv_no.Focus();
                }


            }
        }

        private void dt_inv_Leave(object sender, EventArgs e)
        {
            //compload();
            //lst_compname.Visible = true;
            //pnl_comp_name.Visible = true;
            //lst_compname.Focus();

        }

        private void txt_countername_Enter(object sender, EventArgs e)
        {
            countload();
            pnl_ctrname.Visible = true;
            lst_ctrname.Visible = true;
        }

        private void lst_itemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            //working on firstrow only: 
            int iRow = myDataGrid1.CurrentCell.RowIndex;

            id = Convert.ToString(myDataGrid1.Rows[iRow].Cells[0].Value);
            DataTable dt_items_name = new DataTable();
            id = lst_itemname.SelectedItem.ToString();
            SqlCommand cmd1 = new SqlCommand("select * from Item_table where Item_name='" + id + "' order by Item_name ASC", con);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd1);
            dt_items_name.Rows.Clear();
            adp2.Fill(dt_items_name);
            if (dt_items_name.Rows.Count > 0)
            {
                myDataGrid1.Rows[iRow].Cells["Code"].Value = dt_items_name.Rows[0]["Item_code"].ToString();
                myDataGrid1.Rows[iRow].Cells["Name"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
                //myDataGrid1.Rows[iRow].Cells["Unit"].Value = dt_items_name.Rows[0][""].ToString();
                // myDataGrid1.Rows[iRow].Cells["Name"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
                DataTable dt_unit_no = new DataTable();
                string unit_no = dt_items_name.Rows[0]["Unit_no"].ToString();
                SqlCommand cmd2 = new SqlCommand("select * from unit_table where unit_no='" + unit_no + "' ", con);
                SqlDataAdapter adp3 = new SqlDataAdapter(cmd2);
                dt_unit_no.Rows.Clear();
                adp3.Fill(dt_unit_no);
                myDataGrid1.Rows[iRow].Cells["Unit"].Value = dt_unit_no.Rows[0]["unit_name"].ToString();
                //  myDataGrid1.Rows[iRow].Cells["Less_Qty"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
                // myDataGrid1.Rows[iRow].Cells["Add_Qty"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
                myDataGrid1.Rows[iRow].Cells["Rate"].Value = dt_items_name.Rows[0]["Item_cost"].ToString();
                // myDataGrid1.Rows[iRow].Cells["Amount"].Value = dt_items_name.Rows[0]["Item_name"].ToString();
            }

            //  adp.Dispose();

            //myDataGrid1.Rows[temrowcur].Cells["Name"].Value= lst_itemname.SelectedItem.ToString();
            //myDataGrid1.CurrentCell = myDataGrid1.Rows[temrowcur].Cells["Less_Qty"];

            //pnl_item_name.Visible = false;
            //lst_itemname.Visible = false;
            myDataGrid1.Focus();
            myDataGrid1.CurrentCell = myDataGrid1.Rows[iRow].Cells[2];
        }

        private void myDataGrid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            int vRow = myDataGrid1.RowCount;
            if (myDataGrid1.CurrentCell.ColumnIndex == 0) //Item_code
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    //tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
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

        private void lst_ctrname_Leave(object sender, EventArgs e)
        {
            txt_inv_no.Focus();
        }

        private void txt_inv_no_Enter(object sender, EventArgs e)
        {
            pnl_ctrname.Visible = false;
            lst_ctrname.Visible = false;
        }

        private void lst_itemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            myDataGrid1.Rows[temrowcur].Cells["Name"].Value = lst_itemname.SelectedItem.ToString();
            pnl_item_name.Visible = true;
            lst_itemname.Visible = true;
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                myDataGrid1.CurrentCell = myDataGrid1.Rows[temrowcur].Cells["Less_Qty"];
            }

            //myDataGrid1.CurrentCell = myDataGrid1.Rows[temrowcur].Cells["Unit"];
        }

        private void lst_compname_Leave(object sender, EventArgs e)
        {
            txt_comp_name.Focus();
        }

        private void lst_itemname_Leave(object sender, EventArgs e)
        {
            myDataGrid1.CurrentCell = myDataGrid1.Rows[temrowcur].Cells["Name"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StockAdjustCreate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }

        private void txt_comp_name_MouseClick(object sender, MouseEventArgs e)
        {
            //compload();
            //lst_compname.Visible = true;
            //pnl_comp_name.Visible = true;
            //lst_compname.Focus();
        }

        private void lst_itemname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lst_itemname.Focus();
                lst_itemname.SelectedIndex = 0;
            }
            if (e.KeyCode == Keys.Up)
            {
                lst_itemname.Focus();
                lst_itemname.SelectedIndex = 0;
            }
        }

        private void myDataGrid1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            //if(myDataGrid1.CurrentCell.ColumnIndex==1)
            //{
            //// for check the item value is already entered:
            //    if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value.ToString() != "")
            //    {
            //        string t1 = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            //        int t2 = e.RowIndex;
            //        for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
            //        {
            //            if (t2 != j)
            //            {
            //                if (t1 == myDataGrid1.Rows[j].Cells["Name"].Value.ToString())
            //                {
            //                    MessageBox.Show("selected item is already entered");

            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}

            if (myDataGrid1.CurrentCell.ColumnIndex == 4)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value.ToString() == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value = "0";
                }
            }

            if (myDataGrid1.CurrentCell.ColumnIndex == 3)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() == "")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = "0";
                }
                else
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() != "0")
                    {
                        PnlStock.Visible = false;
                        lblStk.Visible = false;
                        lblStockQty.Visible = false;
                    }
                }
            }

            if (myDataGrid1.CurrentCell.ColumnIndex == 6)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value.ToString() == "0" && myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Add_Qty"].Value.ToString() == "0")
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Less_Qty"].Value = "0";
                }
            }
        }

        //SqlDataReader dreadr3;
        string chk;
        //private void txt_comp_name_TextChanged(object sender, EventArgs e)

        //{
        //    if (txt_comp_name.Text.Trim() != null)
        //    {
        //        if (txt_comp_name.Text.Trim() != "")
        //        {
        //            pnl_comp_name.Visible = true;

        //            SqlCommand cmd = new SqlCommand("select Ledger_name from Ledger_table where Ledger_name like '" + txt_comp_name.Text.Trim() + "%'", con);
        //            SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            dt.Rows.Clear();
        //            adp.Fill(dt);
        //            bool isChk = false;
        //            for (int j = 0; j < dt.Rows.Count;)
        //            {
        //                isChk = true;
        //                string tempStr = dt.Rows[j]["Ledger_name"].ToString();
        //                for (int i = 0; i < lst_compname.Items.Count; i++)
        //                {
        //                    if (dt.Rows[j]["Ledger_name"].ToString() == lst_compname.Items[i].ToString())
        //                    {
        //                        lst_compname.SetSelected(i, true);
        //                        txt_comp_name.Select();
        //                        chk = "1";
        //                        txt_comp_name.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
        //                        break;
        //                    }
        //                }
        //                break;
        //            }

        //            if (isChk == false)
        //            {
        //                chk = "2";
        //                txt_comp_name.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        chk = "1";
        //    }


        //    // Auto Complete Coding:
        //    //AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        //    //con.Close();
        //    //con.Open();
        //    ////SqlCommand cmd = new SqlCommand("select Ledger_name from Ledger_table where Ledger_gno='105' or Ledger_gno='106' or Ledger_gno='201' or Ledger_gno='202' order by Ledger_name ASC", con);
        //    //SqlCommand cmdLedger = new SqlCommand("select Ledger_name from Ledger_table where Ledger_gno='201' order by Ledger_name ASC", con);
        //    //SqlDataReader dReader;
        //    //dReader = cmdLedger.ExecuteReader();

        //    //if (dReader.Read())
        //    //{
        //    //    while (dReader.Read())
        //    //    {
        //    //        namesCollection.Add(dReader["Ledger_name"].ToString());
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Data not found");
        //    //}
        //    //dReader.Close();

        //    //txt_comp_name.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    //txt_comp_name.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    //txt_comp_name.AutoCompleteCustomSource = namesCollection;
        //    //con.Close();


        //}

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
                        for (int j = 0; j < dt.Rows.Count; )
                        {
                            isChk = true;
                            string tempStr = dt.Rows[j]["Ledger_name"].ToString();
                            for (int i = 0; i < lst_compname.Items.Count; i++)
                            {
                                if (dt.Rows[j]["Ledger_name"].ToString() == lst_compname.Items[i].ToString())
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
        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btn_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save_Click(sender, e);
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
                int count = Convert.ToInt16(myDataGrid1.Rows.Count);
                if (txt_comp_name.Text == "")
                {
                    txtCompanyname = txt_comp_name.Text;
                    //myDataGrid1.Rows.Add();
                    //myDataGrid1.Focus();
                    if (count == 0)
                    {
                        dtNew.Rows.Add();
                        myDataGrid1.AllowUserToAddRows = true;
                        //dtDisplay.Rows.Add();
                        myDataGrid1.AllowUserToAddRows = false;
                        myDataGrid1.Focus();
                    }
                    else
                    {
                        //myDataGrid1.CurrentCell = myDataGrid1.CurrentRow.Cells["Item_code"];
                        myDataGrid1.CurrentCell = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"];
                        myDataGrid1.Focus();
                    }
                    pnl_comp_name.Visible = false;
                    if (lst_compname.SelectedItem != string.Empty)
                    {
                        lst_compname.SetSelected(0, true);
                        txt_comp_name.Text = lst_compname.SelectedItem.ToString();
                        pnl_comp_name.Visible = false;
                    }
                    //    }
                    //    else
                    //    {
                    //        //txtCompanyname = txt_comp_name.Text;

                    //        count = Convert.ToInt32(myDataGrid1.Rows.Count);
                    //        //myDataGrid1.Rows.Add();
                    //        //myDataGrid1.Focus();
                    //        if (count == 0)
                    //        {
                    //            dtNew.Rows.Add();
                    //            myDataGrid1.AllowUserToAddRows = true;
                    //            //dtDisplay.Rows.Add();
                    //            myDataGrid1.AllowUserToAddRows = false;
                    //            myDataGrid1.Focus();
                    //        }
                    //        else
                    //        {

                    //            int i = myDataGrid1.CurrentCell.RowIndex;
                    //            myDataGrid1.CurrentCell = myDataGrid1.Rows[i].Cells["Code"];
                    //            myDataGrid1.Focus();
                    //        }
                    //    }

                    //    //txt_comp_name.Text = lst_compname.SelectedItem.ToString();
                    //    pnl_comp_name.Visible = false;
                    //}
                    //if (e.KeyCode == Keys.Back)
                    //{
                    //    dt_inv.Focus();
                    //}
                }
            }
        }

        private void txt_remarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_save.Focus();
                btn_save.BackColor = Color.LightBlue;
            }
        }

        private void txt_countername_TextChanged(object sender, EventArgs e)
        {
            if (txt_countername.Text.Trim() != null)
            {
                if (txt_countername.Text.ToString().Trim() != "")
                {
                    //pnl_ctr_name.Visible = true;

                    SqlCommand cmd = new SqlCommand("select ctr_name from counter_table where ctr_name like '" + txt_countername.Text.Trim() + "%'", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    bool isChk = false;
                    for (int j = 0; j < dt.Rows.Count; )
                    {
                        isChk = true;
                        string tempStr = dt.Rows[j]["ctr_name"].ToString();
                        for (int i = 0; i < lst_ctrname.Items.Count; i++)
                        {
                            if (dt.Rows[j]["ctr_name"].ToString() == lst_ctrname.Items[i].ToString())
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
                    if (isChk == false)
                    {
                        chk = "2";
                        txt_countername.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    }
                }
            }
            else
            {
                chk = "1";
            }
        }

        private void lst_compname_Click(object sender, EventArgs e)
        {
            txt_comp_name.Text = lst_compname.SelectedItem.ToString();
        }

        private void lst_ctrname_Click(object sender, EventArgs e)
        {
            txt_countername.Text = lst_ctrname.SelectedItem.ToString();
        }

        private void btn_Exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void nextcell()
        {
            if (this.myDataGrid1.CurrentCell.ColumnIndex != this.myDataGrid1.Columns.Count - 1)
            {
                int nextindex = Math.Min(this.myDataGrid1.Columns.Count - 1, this.myDataGrid1.CurrentCell.ColumnIndex + 1);
                SetColumnIndex method = new SetColumnIndex(Mymethod);
                this.myDataGrid1.BeginInvoke(method, nextindex + 0);
            }
        }

        double amount = 0;
        // int chkName = 0;
        private void myDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 0)
                {
                    string itemcode = "", itemName = "";
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value != null)
                    {
                        itemcode = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString();
                        itemName = myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value.ToString();
                        getbyid(itemcode, itemName);
                        if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Code"].Value != null)
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

            else if (e.ColumnIndex == 1)
            {
                if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 1)
                {
                    string itemname = "";
                    if (myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value != null && myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString() != "")
                    {

                        string t1 = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        int t2 = e.RowIndex;
                        for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
                        {
                            if (t2 != j)
                            {

                                if (t1 == myDataGrid1.Rows[j].Cells["Name"].Value.ToString())
                                {

                                    if (myDataGrid1.Rows[j].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[j].Cells["Less_Qty"].Value.ToString() != "0")
                                    {
                                        double DBQty = 0;
                                        double GridQty = 0;
                                        double CurrQty = 0;

                                        for (int k = 0; k < myDataGrid1.Rows.Count - 1; k++)
                                        {
                                            if (t1 == myDataGrid1.Rows[k].Cells["Name"].Value.ToString())
                                            {
                                                if (myDataGrid1.Rows[k].Cells["Less_Qty"].Value.ToString() != "")
                                                {
                                                    GridQty = (GridQty) + (Convert.ToDouble(myDataGrid1.Rows[k].Cells["Less_Qty"].Value.ToString()));
                                                }
                                            }
                                        }

                                        DataTable dtDBQty = new DataTable();
                                        SqlCommand cmd = new SqlCommand(" Select a1.item_no as item_code,a2.item_name,isnull(sum(nt_qty),0) as nt_cloqty from stktrn_table a1,item_table a2 " +
                                                                        " where a1.item_no=a2.item_no and item_name='" + myDataGrid1.Rows[j].Cells["Name"].Value + "' and a2.item_Code ='" + myDataGrid1.Rows[j].Cells["Code"].Value + "'  " +
                                                                        " and a1.strn_type in(0,3,12,2) and a1.strn_date<=@tStart " +
                                                                        " group by  a1.item_no,a2.item_name,a2.item_cost ", con);

                                        cmd.Parameters.AddWithValue("@tStart", new DateTime(txt_date.Value.Year, txt_date.Value.Month, txt_date.Value.Day));
                                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                                        dtDBQty.Rows.Clear();
                                        adp.Fill(dtDBQty);
                                        if (dtDBQty.Rows.Count > 0)
                                        {
                                            DBQty = Convert.ToDouble(dtDBQty.Rows[0]["nt_cloqty"]);
                                        }

                                        if (GridQty != 0 && DBQty != 0)
                                        {
                                            CurrQty = (DBQty - GridQty);
                                            lblStockQty.Text = CurrQty.ToString();
                                            vStockQtyShown = true;
                                        }
                                    }
                                    MyMessageBox.ShowBox("Selected item is already entered", "Warning");
                                    PnlStock.Visible = true;
                                    lblStk.Visible = true;
                                    lblStockQty.Visible = true;
                                    break;
                                }
                            }
                        }
                        itemname = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        fecthitemnamevalues(itemname);
                        if (itemname != null)
                        {
                            if (dtName.Rows.Count > 0)
                            {
                                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value != null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Name"].Value.ToString() != "")
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

            else if (e.ColumnIndex == 1)
            {
                // for check the item value is already entered:
                //if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Name"].Value.ToString() != "")
                //{
                //    string t1 = myDataGrid1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                //    int t2 = e.RowIndex;
                //    for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
                //    {
                //        if (t2 != j)
                //        {

                //                if (t1 == myDataGrid1.Rows[j].Cells["Name"].Value.ToString())
                //                {

                //                    MessageBox.Show("Selected item is already entered");
                //                    break;
                //                }

                //        }
                //    }
                //}
            }

            else if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 3)
            {
                string testvar = "";
                testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();
                int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);

                // Beginning Loop 1
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                {
                    // Beginning Loop 2
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "")
                    {
                        // Beginning Serial Number loop

                        //string testvar = "";
                        //testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();                            
                        //int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);                            

                        if (Convert.ToInt32(testvar.ToString()) == 1)
                        {
                            // Beginning First Row
                            if (m_row_index == 0)
                            {
                                myDataGridadjstock.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGridadjstock.Visible = true;
                                myDataGrid2.Visible = false;

                                loopstart = loopend;
                                if (loopstart != 0)
                                {
                                    loopstart = loopend;
                                    loopend = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend <= loopstart)
                                    {
                                        loopstart = 0;
                                        int lessstockrowscount = myDataGridadjstock.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGridadjstock.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }


                                for (int Z = loopstart; Z < loopend; Z++)
                                {
                                    myDataGridadjstock.Rows.Add();
                                    myDataGridadjstock.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }
                            // Ending First Row

                            // Beginning Second Row

                            if (m_row_index == 1)
                            {
                                myDataGrid2.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid2.Visible = true;
                                myDataGridadjstock.Visible = false;

                                loopstart2 = loopend2;
                                if (loopstart2 != 0)
                                {
                                    loopstart2 = loopend2;
                                    loopend2 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend2 < loopstart2)
                                    {
                                        loopstart2 = 0;
                                        int lessstockrowscount = myDataGrid2.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid2.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid2.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend2 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }


                                for (int Z = loopstart2; Z < loopend2; Z++)
                                {
                                    myDataGrid2.Rows.Add();
                                    myDataGrid2.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending Second Row

                            // Beginning Third Row

                            if (m_row_index == 2)
                            {
                                myDataGrid3.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid3.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;

                                loopstart3 = loopend3;
                                if (loopstart3 != 0)
                                {
                                    loopstart3 = loopend3;
                                    loopend3 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend3 < loopstart3)
                                    {
                                        loopstart3 = 0;
                                        int lessstockrowscount = myDataGrid3.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid3.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid3.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend3 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }


                                for (int Z = loopstart3; Z < loopend3; Z++)
                                {
                                    myDataGrid3.Rows.Add();
                                    myDataGrid3.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending Third Row

                            // Beginning Fourth Row

                            if (m_row_index == 3)
                            {
                                myDataGrid4.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid4.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;

                                loopstart4 = loopend4;
                                if (loopstart4 != 0)
                                {
                                    loopstart4 = loopend4;
                                    loopend4 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend4 < loopstart4)
                                    {
                                        loopstart4 = 0;
                                        int lessstockrowscount = myDataGrid4.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid4.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid4.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend4 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }


                                for (int Z = loopstart4; Z < loopend4; Z++)
                                {
                                    myDataGrid4.Rows.Add();
                                    myDataGrid4.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending Fourth Row

                            // Beginning Fifth Row
                            if (m_row_index == 4)
                            {
                                myDataGrid5.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid5.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;

                                loopstart5 = loopend5;
                                if (loopstart5 != 0)
                                {
                                    loopstart5 = loopend5;
                                    loopend5 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend5 < loopstart5)
                                    {
                                        loopstart5 = 0;
                                        int lessstockrowscount = myDataGrid5.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid5.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid5.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend5 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart5; Z < loopend5; Z++)
                                {
                                    myDataGrid5.Rows.Add();
                                    myDataGrid5.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Fifth Row

                            // Beginning Sixth Row
                            if (m_row_index == 5)
                            {
                                myDataGrid6.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid6.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;

                                loopstart6 = loopend6;
                                if (loopstart6 != 0)
                                {
                                    loopstart6 = loopend6;
                                    loopend6 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend6 < loopstart6)
                                    {
                                        loopstart6 = 0;
                                        int lessstockrowscount = myDataGrid6.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid6.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid6.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend6 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart6; Z < loopend6; Z++)
                                {
                                    myDataGrid6.Rows.Add();
                                    myDataGrid6.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Sixth Row

                            // Beginning Seventh Row
                            if (m_row_index == 6)
                            {
                                myDataGrid7.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid7.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;

                                loopstart7 = loopend7;
                                if (loopstart7 != 0)
                                {
                                    loopstart7 = loopend7;
                                    loopend7 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend7 < loopstart7)
                                    {
                                        loopstart7 = 0;
                                        int lessstockrowscount = myDataGrid7.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid7.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid7.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend7 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart7; Z < loopend7; Z++)
                                {
                                    myDataGrid7.Rows.Add();
                                    myDataGrid7.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Seventh Row

                            // Beginning Eighth Row
                            if (m_row_index == 7)
                            {
                                myDataGrid8.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid8.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;

                                loopstart8 = loopend8;
                                if (loopstart8 != 0)
                                {
                                    loopstart8 = loopend8;
                                    loopend8 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend8 < loopstart8)
                                    {
                                        loopstart8 = 0;
                                        int lessstockrowscount = myDataGrid8.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid8.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid8.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend8 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart8; Z < loopend8; Z++)
                                {
                                    myDataGrid8.Rows.Add();
                                    myDataGrid8.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Eighth Row

                            // Beginning Nineth Row
                            if (m_row_index == 8)
                            {
                                myDataGrid9.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid9.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;

                                loopstart9 = loopend9;
                                if (loopstart9 != 0)
                                {
                                    loopstart9 = loopend9;
                                    loopend9 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend9 < loopstart9)
                                    {
                                        loopstart9 = 0;
                                        int lessstockrowscount = myDataGrid9.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid9.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid9.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend9 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart9; Z < loopend9; Z++)
                                {
                                    myDataGrid9.Rows.Add();
                                    myDataGrid9.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Nineth Row

                            // Beginning Tenth Row
                            if (m_row_index == 9)
                            {
                                myDataGrid10.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
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

                                loopstart10 = loopend10;
                                if (loopstart10 != 0)
                                {
                                    loopstart10 = loopend10;
                                    loopend10 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend10 < loopstart10)
                                    {
                                        loopstart10 = 0;
                                        int lessstockrowscount = myDataGrid10.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid10.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid10.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend10 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart10; Z < loopend10; Z++)
                                {
                                    myDataGrid10.Rows.Add();
                                    myDataGrid10.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Tenth Row

                            // Beginning Eleventh Row
                            if (m_row_index == 10)
                            {
                                myDataGrid11.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
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

                                loopstart11 = loopend11;
                                if (loopstart11 != 0)
                                {
                                    loopstart11 = loopend11;
                                    loopend11 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend11 < loopstart11)
                                    {
                                        loopstart11 = 0;
                                        int lessstockrowscount = myDataGrid11.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid11.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid11.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend11 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart11; Z < loopend11; Z++)
                                {
                                    myDataGrid11.Rows.Add();
                                    myDataGrid11.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Eleventh Row

                            // Beginning Twelth Row
                            if (m_row_index == 11)
                            {
                                myDataGrid12.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
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

                                loopstart12 = loopend12;
                                if (loopstart12 != 0)
                                {
                                    loopstart12 = loopend12;
                                    loopend12 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                    if (loopend12 < loopstart12)
                                    {
                                        loopstart12 = 0;
                                        int lessstockrowscount = myDataGrid12.Rows.Count;
                                        for (int p = lessstockrowscount - (myDataGrid12.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid12.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend12 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }

                                for (int Z = loopstart12; Z < loopend12; Z++)
                                {
                                    myDataGrid12.Rows.Add();
                                    myDataGrid12.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Twelth Row


                        }


                        // Ending Serial Number Loop  


                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value)).ToString()));

                        if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < myDataGrid1.Rows.Count; i++)
                            {
                                if (lbl_amt.Text == "")
                                {
                                    lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().ToString()));
                                }
                                else
                                {
                                    //if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    if (myDataGrid1.Rows[i].Cells["Amount"].Value != null && myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
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

                else
                {
                    // Beginning - if less quantity is zero
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "0")
                    {
                        if (m_row_index == 0)
                        {
                            int lessstockrowscount = myDataGridadjstock.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGridadjstock.Rows.RemoveAt(p - 1);
                            }
                            loopstart = 0;
                            loopend = 0;
                        }

                        if (m_row_index == 1)
                        {
                            int lessstockrowscount = myDataGrid2.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid2.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid2.Rows.RemoveAt(p - 1);
                            }
                            loopstart2 = 0;
                            loopend2 = 0;
                        }

                        if (m_row_index == 2)
                        {
                            int lessstockrowscount = myDataGrid3.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid3.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid3.Rows.RemoveAt(p - 1);
                            }
                            loopstart3 = 0;
                            loopend3 = 0;
                        }

                        if (m_row_index == 3)
                        {
                            int lessstockrowscount = myDataGrid4.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid4.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid4.Rows.RemoveAt(p - 1);
                            }
                            loopstart4 = 0;
                            loopend4 = 0;
                        }

                        if (m_row_index == 4)
                        {
                            int lessstockrowscount = myDataGrid5.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid5.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid5.Rows.RemoveAt(p - 1);
                            }
                            loopstart5 = 0;
                            loopend5 = 0;
                        }

                        if (m_row_index == 5)
                        {
                            int lessstockrowscount = myDataGrid6.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid6.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid6.Rows.RemoveAt(p - 1);
                            }
                            loopstart6 = 0;
                            loopend6 = 0;
                        }

                        if (m_row_index == 6)
                        {
                            int lessstockrowscount = myDataGrid7.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid7.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid7.Rows.RemoveAt(p - 1);
                            }
                            loopstart7 = 0;
                            loopend7 = 0;
                        }

                        if (m_row_index == 7)
                        {
                            int lessstockrowscount = myDataGrid8.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid8.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid8.Rows.RemoveAt(p - 1);
                            }
                            loopstart8 = 0;
                            loopend8 = 0;
                        }

                        if (m_row_index == 8)
                        {
                            int lessstockrowscount = myDataGrid9.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid9.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid9.Rows.RemoveAt(p - 1);
                            }
                            loopstart9 = 0;
                            loopend9 = 0;
                        }

                        if (m_row_index == 9)
                        {
                            int lessstockrowscount = myDataGrid10.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid10.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid10.Rows.RemoveAt(p - 1);
                            }
                            loopstart10 = 0;
                            loopend10 = 0;
                        }

                        if (m_row_index == 10)
                        {
                            int lessstockrowscount = myDataGrid11.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid11.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid11.Rows.RemoveAt(p - 1);
                            }
                            loopstart11 = 0;
                            loopend11 = 0;
                        }

                        if (m_row_index == 11)
                        {
                            int lessstockrowscount = myDataGrid12.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid12.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid12.Rows.RemoveAt(p - 1);
                            }
                            loopstart12 = 0;
                            loopend12 = 0;
                        }
                    }

                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() == "")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = "0";
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
                // Begining Loop 1
                string testvar = "";
                testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();

                int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);

                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "0" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value.ToString() != "")
                {
                    // Beginning loop 2
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value == null || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                    {
                        //  double total = 0;                                         
                        // Beginning Serial Number loop

                        //int trw = 0;
                        //string testvar = "";
                        //testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();                          
                        //int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);
                        if (Convert.ToInt32(testvar.ToString()) == 1)
                        {
                            // Beginning First Row
                            if (m_row_index == 0)
                            {
                                myDataGridadjstock.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGridadjstock.Visible = true;
                                myDataGrid2.Visible = false;

                                loopstart = loopend;
                                if (loopstart != 0)
                                {
                                    loopstart = loopend;
                                    loopend = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend < loopstart)
                                    {
                                        loopstart = 0;
                                        int myDataGridadjstockrowscount = myDataGridadjstock.Rows.Count;
                                        for (int p = myDataGridadjstockrowscount - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGridadjstock.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart; Z < loopend; Z++)
                                {
                                    myDataGridadjstock.Rows.Add();
                                    myDataGridadjstock.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending First Row

                            // Beginning Second Row
                            if (m_row_index == 1)
                            {
                                myDataGrid2.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid2.Visible = true;
                                myDataGridadjstock.Visible = false;

                                loopstart2 = loopend2;
                                if (loopstart2 != 0)
                                {
                                    loopstart2 = loopend2;
                                    loopend2 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend2 < loopstart2)
                                    {
                                        loopstart2 = 0;
                                        int myDataGrid2rowscount = myDataGrid2.Rows.Count;
                                        for (int p = myDataGrid2rowscount - (myDataGrid2.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid2.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend2 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart2; Z < loopend2; Z++)
                                {
                                    myDataGrid2.Rows.Add();
                                    myDataGrid2.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending Second Row

                            // Beginning Third Row
                            if (m_row_index == 2)
                            {
                                myDataGrid3.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid3.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;

                                loopstart3 = loopend3;
                                if (loopstart3 != 0)
                                {
                                    loopstart3 = loopend3;
                                    loopend3 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend3 < loopstart3)
                                    {
                                        loopstart3 = 0;
                                        int myDataGrid3rowscount = myDataGrid3.Rows.Count;
                                        for (int p = myDataGrid3rowscount - (myDataGrid3.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid3.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend3 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart3; Z < loopend3; Z++)
                                {
                                    myDataGrid3.Rows.Add();
                                    myDataGrid3.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending Third Row

                            // Beginning Fourth Row
                            if (m_row_index == 3)
                            {
                                myDataGrid4.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid4.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;

                                loopstart4 = loopend4;
                                if (loopstart4 != 0)
                                {
                                    loopstart4 = loopend4;
                                    loopend4 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend4 < loopstart4)
                                    {
                                        loopstart4 = 0;
                                        int myDataGrid4rowscount = myDataGrid4.Rows.Count;
                                        for (int p = myDataGrid4rowscount - (myDataGrid4.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid4.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend4 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart4; Z < loopend4; Z++)
                                {
                                    myDataGrid4.Rows.Add();
                                    myDataGrid4.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }

                            // Ending Fourth Row

                            // Beginning Fifth Row
                            if (m_row_index == 4)
                            {
                                myDataGrid5.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid5.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;

                                loopstart5 = loopend5;
                                if (loopstart5 != 0)
                                {
                                    loopstart5 = loopend5;
                                    loopend5 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend5 < loopstart5)
                                    {
                                        loopstart5 = 0;
                                        int myDataGrid5rowscount = myDataGrid5.Rows.Count;
                                        for (int p = myDataGrid5rowscount - (myDataGrid5.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid5.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend5 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart5; Z < loopend5; Z++)
                                {
                                    myDataGrid5.Rows.Add();
                                    myDataGrid5.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }
                            // Ending Fifth Row

                            // Beginning Sixth Row
                            if (m_row_index == 5)
                            {
                                myDataGrid6.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid6.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;

                                loopstart6 = loopend6;
                                if (loopstart6 != 0)
                                {
                                    loopstart6 = loopend6;
                                    loopend6 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend6 < loopstart6)
                                    {
                                        loopstart6 = 0;
                                        int myDataGrid6rowscount = myDataGrid6.Rows.Count;
                                        for (int p = myDataGrid6rowscount - (myDataGrid6.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid6.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend6 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart6; Z < loopend6; Z++)
                                {
                                    myDataGrid6.Rows.Add();
                                    myDataGrid6.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }
                            // Ending Sixth Row

                            // Beginning Seventh Row
                            if (m_row_index == 6)
                            {
                                myDataGrid7.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid7.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;

                                loopstart7 = loopend7;
                                if (loopstart7 != 0)
                                {
                                    loopstart7 = loopend7;
                                    loopend7 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend7 < loopstart7)
                                    {
                                        loopstart7 = 0;
                                        int myDataGrid7rowscount = myDataGrid7.Rows.Count;
                                        for (int p = myDataGrid7rowscount - (myDataGrid7.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid7.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend7 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }


                                for (int Z = loopstart7; Z < loopend7; Z++)
                                {
                                    myDataGrid7.Rows.Add();
                                    myDataGrid7.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                    //myDataGrid1.Rows[Z].Cells[0].Value = datatableserial.Rows[Z]["item_no"].ToString(); 
                                }
                            }
                            // Ending Seventh Row

                            // Beginning Eighth Row
                            if (m_row_index == 7)
                            {
                                myDataGrid8.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid8.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;

                                loopstart8 = loopend8;
                                if (loopstart8 != 0)
                                {
                                    loopstart8 = loopend8;
                                    loopend8 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend8 < loopstart8)
                                    {
                                        loopstart8 = 0;
                                        int myDataGrid8rowscount = myDataGrid8.Rows.Count;
                                        for (int p = myDataGrid8rowscount - (myDataGrid8.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid8.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend8 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }

                                for (int Z = loopstart8; Z < loopend8; Z++)
                                {
                                    myDataGrid8.Rows.Add();
                                    myDataGrid8.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Eighth Row

                            // Beginning Nineth Row
                            if (m_row_index == 8)
                            {
                                myDataGrid9.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
                                myDataGrid9.Visible = true;
                                myDataGridadjstock.Visible = false;
                                myDataGrid2.Visible = false;
                                myDataGrid3.Visible = false;
                                myDataGrid4.Visible = false;
                                myDataGrid5.Visible = false;
                                myDataGrid6.Visible = false;
                                myDataGrid7.Visible = false;
                                myDataGrid8.Visible = false;

                                loopstart9 = loopend9;
                                if (loopstart9 != 0)
                                {
                                    loopstart9 = loopend9;
                                    loopend9 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend9 < loopstart9)
                                    {
                                        loopstart9 = 0;
                                        int myDataGrid9rowscount = myDataGrid9.Rows.Count;
                                        for (int p = myDataGrid9rowscount - (myDataGrid9.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid9.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend9 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }

                                for (int Z = loopstart9; Z < loopend9; Z++)
                                {
                                    myDataGrid9.Rows.Add();
                                    myDataGrid9.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Nineth Row

                            // Beginning Tenth Row
                            if (m_row_index == 9)
                            {
                                myDataGrid10.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
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

                                loopstart10 = loopend10;
                                if (loopstart10 != 0)
                                {
                                    loopstart10 = loopend10;
                                    loopend10 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend10 < loopstart10)
                                    {
                                        loopstart10 = 0;
                                        int myDataGrid10rowscount = myDataGrid10.Rows.Count;
                                        for (int p = myDataGrid10rowscount - (myDataGrid10.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid10.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend10 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }

                                for (int Z = loopstart10; Z < loopend10; Z++)
                                {
                                    myDataGrid10.Rows.Add();
                                    myDataGrid10.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Tenth Row

                            // Beginning Eleventh Row
                            if (m_row_index == 10)
                            {
                                myDataGrid11.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
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

                                loopstart11 = loopend11;
                                if (loopstart11 != 0)
                                {
                                    loopstart11 = loopend11;
                                    loopend11 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend11 < loopstart11)
                                    {
                                        loopstart11 = 0;
                                        int myDataGrid11rowscount = myDataGrid11.Rows.Count;
                                        for (int p = myDataGrid11rowscount - (myDataGrid11.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid11.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend11 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }

                                for (int Z = loopstart11; Z < loopend11; Z++)
                                {
                                    myDataGrid11.Rows.Add();
                                    myDataGrid11.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Eleventh Row

                            // Beginning Twelth Row
                            if (m_row_index == 11)
                            {
                                myDataGrid12.AllowUserToAddRows = false;
                                pnl_SerialNo.Visible = true;
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

                                loopstart12 = loopend12;
                                if (loopstart12 != 0)
                                {
                                    loopstart12 = loopend12;
                                    loopend12 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                    if (loopend12 < loopstart12)
                                    {
                                        loopstart12 = 0;
                                        int myDataGrid12rowscount = myDataGrid12.Rows.Count;
                                        for (int p = myDataGrid12rowscount - (myDataGrid12.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                                        {
                                            myDataGrid12.Rows.RemoveAt(p - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    loopend12 = Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value.ToString());
                                }

                                for (int Z = loopstart12; Z < loopend12; Z++)
                                {
                                    myDataGrid12.Rows.Add();
                                    myDataGrid12.Rows[Z].Cells[1].Value = myDataGrid1.Rows[e.RowIndex].Cells["code"].Value.ToString();
                                }
                            }
                            // Ending Twelth Row                            

                        }





                        // Ending Serial Number Loop  


                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value)).ToString()));

                        // Beginning loop 3
                        if (myDataGrid1.Rows.Count > 0 && myDataGrid1.CurrentRow.Cells["Amount"].Value.ToString() != "")
                        {
                            for (int i = 0; i < myDataGrid1.Rows.Count; i++)
                            {
                                // Beginning loop 4
                                if (lbl_amt.Text == "")
                                {
                                    lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString().ToString()));
                                }
                                else
                                {
                                    //if (myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    if (myDataGrid1.Rows[i].Cells["Amount"].Value != null && myDataGrid1.Rows[i].Cells["Amount"].Value.ToString() != "")
                                    {
                                        amount += double.Parse(myDataGrid1.Rows[i].Cells["Amount"].Value.ToString());

                                    }
                                }
                                // Ending Loop 4
                                lbl_amt.Text = string.Format("{0:0.00}", Convert.ToDouble(amount.ToString()));

                            }
                            // Ending loop 3
                            amount = 0;
                        }
                        // Ending loop 2
                    }
                    // Ending loop 1

                    else
                    {
                        MyMessageBox.ShowBox("Can not enter both Add and Less");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Add_Qty"].Value = "0";
                    }

                }
                else
                {

                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0")
                    {
                        if (m_row_index == 0)
                        {
                            int lessstockrowscount = myDataGridadjstock.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGridadjstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGridadjstock.Rows.RemoveAt(p - 1);
                            }
                            loopstart = 0;
                            loopend = 0;
                        }

                        if (m_row_index == 1)
                        {
                            int lessstockrowscount = myDataGrid2.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid2.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid2.Rows.RemoveAt(p - 1);
                            }
                            loopstart2 = 0;
                            loopend2 = 0;
                        }

                        if (m_row_index == 2)
                        {
                            int lessstockrowscount = myDataGrid3.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid3.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid3.Rows.RemoveAt(p - 1);
                            }
                            loopstart3 = 0;
                            loopend3 = 0;
                        }

                        if (m_row_index == 3)
                        {
                            int lessstockrowscount = myDataGrid4.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid4.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid4.Rows.RemoveAt(p - 1);
                            }
                            loopstart4 = 0;
                            loopend4 = 0;
                        }

                        if (m_row_index == 4)
                        {
                            int lessstockrowscount = myDataGrid5.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid5.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid5.Rows.RemoveAt(p - 1);
                            }
                            loopstart5 = 0;
                            loopend5 = 0;
                        }

                        if (m_row_index == 5)
                        {
                            int lessstockrowscount = myDataGrid6.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid6.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid6.Rows.RemoveAt(p - 1);
                            }
                            loopstart6 = 0;
                            loopend6 = 0;
                        }

                        if (m_row_index == 6)
                        {
                            int lessstockrowscount = myDataGrid7.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid7.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid7.Rows.RemoveAt(p - 1);
                            }
                            loopstart7 = 0;
                            loopend7 = 0;
                        }

                        if (m_row_index == 7)
                        {
                            int lessstockrowscount = myDataGrid8.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid8.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid8.Rows.RemoveAt(p - 1);
                            }
                            loopstart8 = 0;
                            loopend8 = 0;
                        }

                        if (m_row_index == 8)
                        {
                            int lessstockrowscount = myDataGrid9.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid9.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid9.Rows.RemoveAt(p - 1);
                            }
                            loopstart9 = 0;
                            loopend9 = 0;
                        }

                        if (m_row_index == 9)
                        {
                            int lessstockrowscount = myDataGrid10.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid10.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid10.Rows.RemoveAt(p - 1);
                            }
                            loopstart10 = 0;
                            loopend10 = 0;
                        }

                        if (m_row_index == 10)
                        {
                            int lessstockrowscount = myDataGrid11.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid11.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid11.Rows.RemoveAt(p - 1);
                            }
                            loopstart11 = 0;
                            loopend11 = 0;
                        }

                        if (m_row_index == 11)
                        {
                            int lessstockrowscount = myDataGrid12.Rows.Count;
                            for (int p = lessstockrowscount - (myDataGrid12.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGrid12.Rows.RemoveAt(p - 1);
                            }
                            loopstart12 = 0;
                            loopend12 = 0;
                        }
                    }


                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "0" || myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() == "")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = "0";
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


            else if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 5)
            {

            }

            else if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 6)
            {
                if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value != null && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value.ToString() != "0")
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value != "" && myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value != "0.00")
                    {
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Amount"].Value = string.Format("{0:0.00}", Convert.ToDouble((Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Rate"].Value) * Convert.ToDouble(myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value)).ToString()));

                        //gridrows_calculatoin();
                    }
                }
                else
                {
                    myDataGrid1.Rows[myDataGrid1.CurrentCell.RowIndex].Cells["Less_Qty"].Value = "0";
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

        private void myDataGrid1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                dt_inv.Focus();
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
            pnl_SerialNo.Visible = false;
        }



        private void myDataGrid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
        }



        private void myDataGrid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Less Quantity
            if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 3)
            {
                if (e.ColumnIndex == 3 && myDataGrid1.Rows[e.RowIndex].Cells[3].Value != null && Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[3].Value) != 0)
                {
                    string testvar = "";
                    testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();
                    int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);
                    if (Convert.ToInt32(testvar.ToString()) == 1)
                    {
                        // Beginning First Row
                        if (m_row_index == 0)
                        {
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

                        // Beginning Second Row
                        if (m_row_index == 1)
                        {
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

                        //Beginning Third Row
                        if (m_row_index == 2)
                        {
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

                        // Ending Third Row

                        if (m_row_index == 3)
                        {
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

                        if (m_row_index == 4)
                        {
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

                        if (m_row_index == 5)
                        {
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

                        if (m_row_index == 6)
                        {
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
                            myDataGrid7.Visible = false;
                            myDataGrid8.Visible = false;
                            myDataGrid9.Visible = false;
                            myDataGrid10.Visible = false;
                            myDataGrid11.Visible = false;
                            myDataGrid12.Visible = false;
                        }

                        if (m_row_index == 7)
                        {
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

                        if (m_row_index == 8)
                        {
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

                        if (m_row_index == 9)
                        {
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


                        if (m_row_index == 10)
                        {
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

                        if (m_row_index == 11)
                        {
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

                    }
                }
            }

            // Add Quantity

            if (myDataGrid1.CurrentRow != null && e.ColumnIndex == 4)
            {
                if (e.ColumnIndex == 4 && myDataGrid1.Rows[e.RowIndex].Cells[4].Value != null && Convert.ToInt32(myDataGrid1.Rows[e.RowIndex].Cells[4].Value) != 0)
                {
                    string testvar = "";
                    testvar = myDataGrid1.CurrentRow.Cells["Stock_Category"].Value.ToString();
                    int m_row_index = Convert.ToInt32(myDataGrid1.CurrentCell.RowIndex);
                    if (Convert.ToInt32(testvar.ToString()) == 1)
                    {
                        if (m_row_index == 0)
                        {
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

                        if (m_row_index == 1)
                        {
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

                        if (m_row_index == 2)
                        {
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

                        if (m_row_index == 3)
                        {
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

                        if (m_row_index == 4)
                        {
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

                        if (m_row_index == 5)
                        {
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

                        if (m_row_index == 6)
                        {
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

                        if (m_row_index == 7)
                        {
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

                        if (m_row_index == 8)
                        {
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

                        if (m_row_index == 9)
                        {
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


                        if (m_row_index == 10)
                        {
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

                        if (m_row_index == 11)
                        {
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
                    }
                }
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
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
                // Ending
                dbcheckforserial();
            }
        }
    }
}
