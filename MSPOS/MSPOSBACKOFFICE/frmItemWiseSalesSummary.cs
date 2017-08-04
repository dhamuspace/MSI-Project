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
    public partial class frmItemWiseSalesSummary : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
      
        public frmItemWiseSalesSummary()
        {
            InitializeComponent();
            Pnl_Back2.Visible = false;
            pnlCustomer.Visible = false;
            lst_Boxitem.Visible = false;
            txt_from.Format = DateTimePickerFormat.Custom;
            txt_from.CustomFormat = "dd/MM/yyyy";
            txt_from.Select();
            txt_from.Focus();

            txt_to.Format = DateTimePickerFormat.Custom;
            txt_to.CustomFormat = "dd/MM/yyyy";
          //  txt_from.Text = DateTime.Now.ToShortDateString();
          //  txt_to.Text = DateTime.Now.ToShortDateString();
            grd_SalesSummary.ReadOnly = true;
            chkbox.FormIdentify = "ItemWise";
            con.Close();
            con.Open();
            dtDisplay.Columns.Add("Sno", typeof(string));
            dtDisplay.Columns.Add("Item_code", typeof(string));
            dtDisplay.Columns.Add("Item_name", typeof(string));
            dtDisplay.Columns.Add("nt_Qty", typeof(string));
            dtDisplay.Columns.Add("Amount", typeof(string));

            //row height
           grd_SalesSummary.DefaultCellStyle.Font = new Font("Tahoma", 10);
           grd_SalesSummary.RowTemplate.Height = 25;
        }
        DataTable dtDisplay = new DataTable();
      

        private void frmItemWiseSalesSummary_Load(object sender, EventArgs e)
        {
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void btn_option_Click(object sender, EventArgs e)
        {
            Pnl_Back2.Visible = true;
            txt_ReportOn.Text = "Gross Amount";
            txt_salestypes.Text = "All";
            txt_OrderBy.Text = "Item";
            if (txt_OrderBy.Text == "Item")
            {
                txtType.ReadOnly = true;
                txtType.Text = "Detail";
            }
            txt_ReportOn.Select();
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                if (listSelect.SelectedIndex < listSelect.Items.Count - 1)
                {
                    listSelect.SetSelected(listSelect.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (listSelect.SelectedIndex > 0)
                {
                    listSelect.SetSelected(listSelect.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              // pnlCustomer.Visible = false;
                Pnllistselect.Visible = false;
                
                //if (listSelect.Text != "")
                //{

                if (tFocusActionType == "TYPE")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txtType.Text = listSelect.SelectedItem.ToString();
                       // listbox_values();
                        btn_ok.Select();
                    }
                    else
                    {
                        btn_ok.Select();
                    }
                }

                if (tFocusActionType == "MODEL")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txt_model.Text = listSelect.SelectedItem.ToString();
                        listbox_values();
                        if (txt_OrderBy.Text == "Item")
                        {
                            btn_ok.Select();
                        }
                        else
                        {
                            txtType.Select();
                        }
                    }
                    else
                    {
                        if (txt_OrderBy.Text == "Item")
                        {
                            btn_ok.Select();
                        }
                        else
                        {
                            txtType.Select();
                        }
                       // btn_ok.Select();
                    }
                }
                if (tFocusActionType == "SALESTYPE")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txt_salestypes.Text = listSelect.SelectedItem.ToString();
                        txt_model.Select();
                    }
                    else
                    {
                        txt_model.Select();
                    }
                }
            
                if (tFocusActionType == "COUNTER")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txt_Counter.Text = listSelect.SelectedItem.ToString();
                        listbox_values();
                        txt_salestypes.Select();
                    }
                    else
                    {
                        txt_salestypes.Select();
                    }
                }
                if (tFocusActionType == "BRAND")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txt_Brand.Text = listSelect.SelectedItem.ToString();
                        listbox_values();
                        txt_Counter.Select();
                    }
                    else
                    {
                        txt_Counter.Select();
                    }
                }
                if (tFocusActionType == "GROUP")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txt_Group.Text = listSelect.SelectedItem.ToString();
                        listbox_values();
                        txt_Brand.Select();
                    }
                    else
                    {
                        txt_Brand.Select();
                    }
                }        

              
       
               
                if (tFocusActionType == "ORDER")
                {
                    if (listSelect.SelectedItems.Count > 0)
                    {
                        txt_OrderBy.Text = listSelect.SelectedItem.ToString();
                        if (txt_OrderBy.Text == "Item")
                        {
                            txtType.ReadOnly = true;
                            txtType.Text = "Detail";
                        }
                        else
                        {
                            txtType.ReadOnly = false;
                            txtType.Text = "Summary";
                        }
                        txt_Group.Select();
                    }
                    else
                    {
                        txt_Group.Select();
                    }
                }
               
                    if (tFocusActionType == "REPORT")
                    {
                        if (listSelect.SelectedItems.Count > 0)
                        {
                            txt_ReportOn.Text = listSelect.SelectedItem.ToString();
                            txt_OrderBy.Select();
                        }
                        else
                        {
                            txt_OrderBy.Select();
                        }
                    }
                    
                    
              //  }

            }

        }
       
        string chk;
        
        //SqlDataReader dr = null;
        private void txt_Group_TextChanged(object sender, EventArgs e)
        {
            //if (listActionType != "Over" && listActionType != null)
            //{
            if (txt_Group.Text.Trim() != null && txt_Group.Text.Trim() != "")
            {
             DataTable dtNew4 = new DataTable();
            dtNew4.Rows.Clear();
            SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
            cmdCno.CommandType = CommandType.StoredProcedure;
            cmdCno.Parameters.AddWithValue("@tActionType", "GROUP");
            cmdCno.Parameters.AddWithValue("@tValue",txt_Group.Text.Trim());
            SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
            adp4.Fill(dtNew4);
            bool isChk = false;
            for(int mn=0;mn<dtNew4.Rows.Count; mn++)
            {              

              //  SqlCommand cmd = new SqlCommand("Select Brand_name from Brand_table where Brand_name like '" + txt_Group.Text.Trim() + "%'", con);
                //if (dr != null)
                //{
                //    dr.Close();
                //}
                ////   dr.Close();
                //dr = cmd.ExecuteReader();
                //bool isChk = false;
                //while (dr.Read())
                //{
                    isChk = true;
                    string tempStr = dtNew4.Rows[mn][0].ToString();
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        if (dtNew4.Rows[mn][0].ToString() ==listSelect.Items[i].ToString())
                        {
                            listSelect.SetSelected(i, true);
                            txt_Group.Select();
                            chk = "1";
                            txt_Group.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }

                    }
                }               
                if (isChk == false)
                {
                    chk = "2";
                    txt_Group.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";

            }
            // }
        }

        public void listbox_values()
        {
            string tQuery = "";
            if (tFocusActionType == "MODEL")
            {
                tQuery = "select Model_name from Model_table";
            }
            if (tFocusActionType == "COUNTER")
            {
                tQuery = "select ctr_name from counter_table";
            }
            if (tFocusActionType == "BRAND")
            {
                tQuery = "Select Brand_name from Brand_table";
            }
            if (tFocusActionType == "GROUP")
            {
                tQuery = "Select Item_Groupname from Item_Grouptable";
            }           
           
           
           // SqlCommand cmd = new SqlCommand(tQuery, con);
            SqlDataAdapter asd = new SqlDataAdapter(tQuery, con);
            DataTable dt=new DataTable ();
            listSelect.Items.Clear();
            dt.Rows.Clear();
            asd.Fill(dt);
            if(dt.Rows.Count>0)
            {
                for(int k=0;k<dt.Rows.Count;k++)
                {
                    listSelect.Items.Add(dt.Rows[k][0].ToString());
                }
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
        string tFocusActionType = "";
        private void txt_Group_Enter(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            tFocusActionType = "GROUP";
            listbox_values();
        }

        private void txt_customer_Enter(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            lst_Boxitem.Visible = true;
            customerDetails();
        }

        public void customerDetails()
        {
           // con.Open();
            SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_gno in (202,31) ", con);
            SqlDataAdapter asd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            lst_Boxitem.Items.Clear();
            dt.Rows.Clear();
            asd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    lst_Boxitem.Items.Add(dt.Rows[k]["Ledger_name"].ToString());
                }
            }
           // con.Close();
        }

        private void OnCustomerKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lst_Boxitem.SelectedIndex < lst_Boxitem.Items.Count - 1)
                {
                    lst_Boxitem.SetSelected(lst_Boxitem.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lst_Boxitem.SelectedIndex > 0)
                {
                    lst_Boxitem.SetSelected(lst_Boxitem.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                passingvalues.tAmountType = "Gross Amount";
                pnlCustomer.Visible = false;
                lst_Boxitem.Visible = false;
                if (lst_Boxitem.Text != "")
                {                          
                    txt_customer.Text = lst_Boxitem.SelectedItem.ToString();
                    grd_SalesSummary.Show();
                    CustomerInfo();
                }
            }

        }

      

        SqlDataReader dread = null;
        private void txt_customer_TextChanged_1(object sender, EventArgs e)
        {
           pnlCustomer.Visible = true;
           lst_Boxitem.Visible = true;
            if (txt_customer.Text.Trim() != null && txt_customer.Text.Trim() != "")
            {
             
             DataTable dtNew4 = new DataTable();
             dtNew4.Rows.Clear();
             SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
             cmdCno.CommandType = CommandType.StoredProcedure;
             cmdCno.Parameters.AddWithValue("@tActionType", "LEDGERLIKE");
             cmdCno.Parameters.AddWithValue("@tValue", txt_customer.Text.Trim());
             SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
             adp4.Fill(dtNew4);
             bool isChk = false;
             for(int mn=0;mn<dtNew4.Rows.Count; mn++)
             {
              
                //SqlCommand cmd = new SqlCommand("Select Ledger_name from Ledger_table where Ledger_name like '" + txt_customer.Text.Trim() + "%'", con);
                //if (dread != null)
                //{
                //    dread.Close();
                //}
                ////   dr.Close();
                //dread = cmd.ExecuteReader();
                //bool isChk = false;
                //while (dread.Read())
                //{
                   isChk = true;
                    string tempStr = dtNew4.Rows[mn]["Ledger_name"].ToString();
                    for (int i = 0; i < lst_Boxitem.Items.Count; i++)
                    {
                        if (dtNew4.Rows[mn]["Ledger_name"].ToString() == lst_Boxitem.Items[i].ToString())
                        {

                            lst_Boxitem.SetSelected(i, true);
                            txt_customer.Select();
                            chk = "1";
                            txt_customer.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }

                    }
                }               
                if (isChk == false)
                {
                    chk = "2";
                    txt_customer.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";
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
        int CustomerNo;

        public void CustomerInfo()
        {
            DataTable dtNew4 = new DataTable();
            dtNew4.Rows.Clear();
            SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
            cmdCno.CommandType = CommandType.StoredProcedure;
            cmdCno.Parameters.AddWithValue("@tActionType", "SEARCHLEDGERNO");
            cmdCno.Parameters.AddWithValue("@tValue", txt_customer.Text.Trim());
      
            SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
            adp4.Fill(dtNew4);
            if (dtNew4.Rows.Count > 0)
            {
            //string customerno = "select Distinct(Ledger_no) from Ledger_table where Ledger_name='" + txt_customer.Text + "' and  ";
            //SqlCommand cmdCno = new SqlCommand(customerno, con);
            //var tempCno = cmdCno.ExecuteScalar();
            //if (tempCno != null)
            //{
            //    CustomerNo = Convert.ToInt16(cmdCno.ExecuteScalar());
                loadgridDataBycustomer(int.Parse(dtNew4.Rows[0][0].ToString()));
            }
            else
            {
                MessageBox.Show(" Invalid Customer Name");
                grd_SalesSummary.DataSource = null;
            }
            con.Close();
        }

        public void loadgridDataBycustomer(int CusNO)
        {
            DataTable dtNew4 = new DataTable();
            dtNew4.Rows.Clear();
            SqlCommand cmdgrid = new SqlCommand("sp_ItemwiseSelect", con);
            cmdgrid.CommandType = CommandType.StoredProcedure;
            cmdgrid.Parameters.AddWithValue("@tActionType", "ItemWise");
            cmdgrid.Parameters.AddWithValue("@tValue", CusNO);
            cmdgrid.Parameters.AddWithValue("@tDateFrom",txt_from.Value);
            cmdgrid.Parameters.AddWithValue("@tDateTo",txt_to.Value);
            SqlDataAdapter adp4 = new SqlDataAdapter(cmdgrid);
            adp4.Fill(dtNew4);
            
            if (dtNew4.Rows.Count > 0)
            {             

            //string queryselect = "Select  Item_table.Item_code,Item_table.Item_name, sum(stktrn_table.nt_qty)as nt_qty ,sum(stktrn_table.Amount) as Amount from stktrn_table,Item_table where Item_table.Item_no=stktrn_table.item_no and stktrn_table.StrnParty_no=" + CusNO + " group by Item_table.Item_no,Item_table.Item_code,Item_table.Item_name ";
            //SqlCommand cmdgrid = new SqlCommand(queryselect, con);
            //SqlDataAdapter adp = new SqlDataAdapter(cmdgrid);
            //DataTable dt2 = new DataTable();
            //adp.Fill(dt2);
            //if (dt2.Rows.Count > 0)
            //{
                grd_SalesSummary.DataSource = AutoNumberedTable(dtNew4);
                foreach (DataGridViewColumn col in grd_SalesSummary.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                grd_SalesSummary.Columns[0].Width = 200;
                grd_SalesSummary.Columns[1].Width = 200;
                grd_SalesSummary.Columns[2].Width = 350;
                grd_SalesSummary.Columns[3].Width = 250;
                grd_SalesSummary.Columns[4].Width = 250;
            }
            else
            {
                
                MyMessageBox.ShowBox("No Entries Made in  '" + txt_customer.Text + "'");
                grd_SalesSummary.Show();
                grd_SalesSummary.DataSource = null;
            }
           // con.Close();
            pnlCustomer.Visible = false;


            double TotOfTotalQty = 0, TotofTotalVal = 0;
            for (int i = 0; i < grd_SalesSummary.Rows.Count; i++)
            {
                double TotVal = Convert.ToDouble(grd_SalesSummary.Rows[i].Cells["Amount"].Value);
                double TotQty = Convert.ToDouble(grd_SalesSummary.Rows[i].Cells["Nt_Qty"].Value);
                TotOfTotalQty = TotOfTotalQty + TotQty;
                TotofTotalVal = TotofTotalVal + TotVal;
            }

            lblTotalQty.Text = TotOfTotalQty.ToString();
            lblTotalAmt.Text =string.Format("{0:0.00}",TotofTotalVal);

            //dt2.Rows.Add("", "Total  :", TotOfTotalQty, TotofTotalVal);
         

     

           

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grd_SalesSummary_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = e.RowIndex;

            if (grd_SalesSummary.Rows[row].ReadOnly == true)
            {
                var tempdate = grd_SalesSummary.Rows[row].Cells[1].Value;
                if (tempdate != null)
                {
                    string Itemcode = grd_SalesSummary.Rows[row].Cells[2].Value.ToString();
                    passingvalues.tot = grd_SalesSummary.Rows[row].Cells[4].Value.ToString();
                    DataTable dtNew4 = new DataTable();
                    dtNew4.Rows.Clear();
                    SqlCommand cmdItemno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                    cmdItemno.CommandType = CommandType.StoredProcedure;
                    cmdItemno.Parameters.AddWithValue("@tActionType", "ITEMNAME");
                    cmdItemno.Parameters.AddWithValue("@tValue", Itemcode);
                    SqlDataAdapter adp4 = new SqlDataAdapter(cmdItemno);
                    adp4.Fill(dtNew4);
                    if (dtNew4.Rows.Count > 0)
                    {
                        passingvalues.id_number_item_leder = dtNew4.Rows[0][0].ToString();
                        passingvalues.from_date1 = txt_from.Value;
                        passingvalues.end_date1 = txt_to.Value;
                        passingvalues.tStartDateParthi = txt_from.Value;
                        passingvalues.tToDateParthi = txt_to.Value;
                    }
                    //string queryItemNO = "select Item_no from Item_table where Item_code='" + Itemcode + "' ";
                    //SqlCommand cmdItemno = new SqlCommand(queryItemNO,con);
                    //string ItemNO = cmdItemno.ExecuteScalar().ToString();

                    ////MessageBox.Show(ItemNO);
                    //passingvalues.id_number_item_leder = ItemNO;

                    //ItemLedger frm = new ItemLedger();
                    //this.Close();
                    //frm.Show();

                    ItemLedger frm = new ItemLedger();
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    this.SendToBack();
                   
                    frm.Show();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("empty row is clicked");
                }
            }
        }

        private void txt_from_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txt_from_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_to.Focus();
            }
        }

        private void txt_to_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_customer.Focus();
            }
        }
        string tQuery = "";
        private void btn_ok_Click(object sender, EventArgs e)
        {
            tTotQty = 0;
            tTotAmt = 0;
            
            
            //if (txt_ReportOn.Text.Trim() == "Gross Amount")
            //{

            tQuery = "SELECT dbo.Item_table.Item_code, dbo.Item_table.Item_name,SUM(dbo.stktrn_table.nt_qty) as nt_qty,(CASE WHEN @tReportOn='Gross Amount' THEN SUM( dbo.stktrn_table.Amount) ELSE sum(dbo.stktrn_table.Net_amt) END) as Amount  FROM dbo.Item_table INNER JOIN dbo.stktrn_table ON dbo.Item_table.Item_no = dbo.stktrn_table.item_no and stktrn_table.strn_date between @tDateFrom and @tDateTo and ";
            if (txt_Group.Text.Trim() != "")
            {
                tQuery += "Item_table.item_Groupno=(Select item_Groupno from  Item_Grouptable where Item_groupname='" + txt_Group.Text.Trim() + "') and ";
            }
            if (txt_Brand.Text.Trim() != "")
            {
                tQuery += "Item_table.Brand_no=(Select Brand_no from  brand_table where Brand_name='" + txt_Brand.Text.Trim() + "') and ";
            }
            if (txt_model.Text.Trim() != "")
            {
                tQuery += "Item_table.Model_no=(Select Model_no from  Model_table where Model_name='" + txt_model.Text.Trim() + "') and ";
            }

            if (txt_Counter.Text.Trim() != "")
            {
                tQuery += "stktrn_table.ctr_no=(Select ctr_no from  counter_table where ctr_name='" + txt_Counter.Text.Trim() + "') and ";
            }
            if (txt_salestypes.Text.Trim() == "Cash")
            {
                tQuery += "stktrn_table.StrnParty_no=2 and ";
            }
            if (txt_salestypes.Text.Trim() == "Credit")
            {
                tQuery += "stktrn_table.StrnParty_no=14 and ";
            }
            tQuery = tQuery.Remove(tQuery.Length - 4);
            tQuery += "group by dbo.Item_table.Item_code,dbo.Item_table.Item_name";
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd = new SqlCommand(tQuery, con);
            cmd.Parameters.AddWithValue("@tReportOn", txt_ReportOn.Text.Trim());

            passingvalues.tAmountType = txt_ReportOn.Text.Trim();

            cmd.Parameters.AddWithValue("@tDateFrom", txt_from.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@tDateTo", txt_to.Value.ToString("yyyy-MM-dd"));
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dtNew);
            funTotal(dtNew);
            grd_SalesSummary.DataSource = AutoNumberedTable(dtNew);

            if (txt_OrderBy.Text.Trim() == "Item")
            {

            }
            else if (txt_OrderBy.Text.Trim() == "Group")
            {                
                DataTable dtGroupItem = new DataTable();
                if (dtGroupItem.Columns.Count == 0)
                {
                    dtGroupItem.Columns.Add("GroupName", typeof(string));
                    dtGroupItem.Columns.Add("ItemName", typeof(string));
                }
                DataTable dtGroupNew = new DataTable();
                if (dtGroupNew.Columns.Count == 0)
                {
                    dtGroupNew.Columns.Add("GroupName", typeof(string));
                }
                dtGroupItem.Rows.Clear();
                dtGroupNew.Rows.Clear();
                DataTable dtGroup = new DataTable();

                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    string itemName = dtNew.Rows[mn]["Item_name"].ToString();
                    SqlCommand cmd1 = new SqlCommand("select item_groupname from Item_Grouptable where Item_groupno=(select item_Groupno from Item_table where Item_name=@tItemName)", con);
                    cmd1.Parameters.AddWithValue("@tItemName", itemName);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    dtGroup.Rows.Clear();
                    adp1.Fill(dtGroup);
                    if (dtGroup.Rows.Count > 0)
                    {
                        dtGroupItem.Rows.Add(dtGroup.Rows[0][0].ToString(), dtNew.Rows[mn]["Item_name"].ToString());
                        dtGroupNew.Rows.Add(dtGroup.Rows[0][0].ToString());
                    }
                }

                DataTable distinctTable = new DataTable();
                if (distinctTable.Columns.Count == 0)
                {
                    distinctTable.Columns.Add("GroupName", typeof(string));
                }
                for (int hi = 0; hi < dtGroupNew.Rows.Count; hi++)
                {
                    if (hi == 0)
                    {
                        distinctTable.Rows.Add(dtGroupNew.Rows[hi][0].ToString());
                    }
                    else
                    {
                        string tGroupname = dtGroupNew.Rows[hi][0].ToString();
                        bool isChk = false;
                        for (int ab = 0; ab < distinctTable.Rows.Count; ab++)
                        {
                            if (distinctTable.Rows[ab][0].ToString() == tGroupname)
                            {
                                isChk = true;
                            }
                        }
                        if (isChk == false)
                        {
                            distinctTable.Rows.Add(dtGroupNew.Rows[hi][0].ToString());
                        }
                    }
                }
                // DataTable distinctTable = dtGroupNew.DefaultView.ToTable( /*distinct*/ true);
                dtDisplay.Rows.Clear();
                //  DataTable distinctTable = dtGroupNew.DefaultView.ToTable(true);
                double tGroupTotQty = 0, tGroupTotAmt = 0;
                int tRowNo = 0;
                List<int> list = new List<int>();
                list.Clear();

                for (int ij = 0; ij < distinctTable.Rows.Count; ij++)
                {
                    tRowNo = dtDisplay.Rows.Count;
                    list.Add(tRowNo);
                    dtDisplay.Rows.Add(dtDisplay.Rows.Count + 1, "", distinctTable.Rows[ij][0].ToString(), "", "");
                    tGroupTotQty = 0;
                    tGroupTotAmt = 0;
                    for (int kl = 0; kl < dtGroupItem.Rows.Count; kl++)
                    {

                        if (distinctTable.Rows[ij][0].ToString() == dtGroupItem.Rows[kl][0].ToString())
                        {
                            for (int x = 0; x < dtNew.Rows.Count; x++)
                            {
                                if (dtGroupItem.Rows[kl][1].ToString() == dtNew.Rows[x]["Item_name"].ToString())
                                {
                                    tGroupTotQty += double.Parse(dtNew.Rows[x]["nt_qty"].ToString());
                                    tGroupTotAmt += double.Parse(dtNew.Rows[x]["Amount"].ToString());
                                    tTotAmt += double.Parse(dtNew.Rows[x]["Amount"].ToString());
                                    tTotQty += double.Parse(dtNew.Rows[x]["nt_qty"].ToString());
                                    if (txtType.Text.Trim() == "Detail")
                                    {
                                        dtDisplay.Rows.Add(dtDisplay.Rows.Count + 1, dtNew.Rows[x]["Item_code"].ToString(), dtNew.Rows[x]["Item_name"].ToString(), dtNew.Rows[x]["nt_qty"].ToString(), string.Format("{0:0.00}",double.Parse(dtNew.Rows[x]["Amount"].ToString())));
                                    }
                                }
                            }
                        }
                    }
                    dtDisplay.Rows[tRowNo]["nt_Qty"] = tGroupTotQty;
                    dtDisplay.Rows[tRowNo]["Amount"] = string.Format("{0:0.00}", tGroupTotAmt);
                }
                int[] rowNumber = list.ToArray();
                grd_SalesSummary.DataSource = dtDisplay;
                grd_SalesSummary.ReadOnly = false;
                int isChkRow = 0;
                for (int bc = 0; bc < grd_SalesSummary.Rows.Count; bc++)
                {
                    if (bc == rowNumber[isChkRow])
                    {
                        grd_SalesSummary.Rows[bc].ReadOnly = false;
                        grd_SalesSummary.Rows[bc].DefaultCellStyle.ForeColor = Color.Blue;
                        if (isChkRow < rowNumber.Length - 1)
                        {
                            isChkRow += 1;
                        }
                    }
                    else
                    {
                        grd_SalesSummary.Rows[bc].ReadOnly = true;
                    }
                }

            }

            else if (txt_OrderBy.Text.Trim() == "Brand")
            {
                DataTable dtGroupItem = new DataTable();
                if (dtGroupItem.Columns.Count == 0)
                {
                    dtGroupItem.Columns.Add("GroupName", typeof(string));
                    dtGroupItem.Columns.Add("ItemName", typeof(string));
                }
                DataTable dtGroupNew = new DataTable();
                if (dtGroupNew.Columns.Count == 0)
                {
                    dtGroupNew.Columns.Add("GroupName", typeof(string));
                }
                dtGroupItem.Rows.Clear();
                dtGroupNew.Rows.Clear();
                DataTable dtGroup = new DataTable();

                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    string itemName = dtNew.Rows[mn]["Item_name"].ToString();
                    SqlCommand cmd1 = new SqlCommand("select Brand_name from  Brand_table where Brand_no=(select Brand_no from Item_table where Item_name=@tItemName)", con);
                    cmd1.Parameters.AddWithValue("@tItemName", itemName);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    dtGroup.Rows.Clear();
                    adp1.Fill(dtGroup);
                    if (dtGroup.Rows.Count > 0)
                    {
                        dtGroupItem.Rows.Add(dtGroup.Rows[0][0].ToString(), dtNew.Rows[mn]["Item_name"].ToString());
                        dtGroupNew.Rows.Add(dtGroup.Rows[0][0].ToString());
                    }
                }

                DataTable distinctTable = new DataTable();
                if (distinctTable.Columns.Count == 0)
                {
                    distinctTable.Columns.Add("GroupName", typeof(string));
                }
                for (int hi = 0; hi < dtGroupNew.Rows.Count; hi++)
                {
                    if (hi == 0)
                    {
                        distinctTable.Rows.Add(dtGroupNew.Rows[hi][0].ToString());
                    }
                    else
                    {
                        string tGroupname = dtGroupNew.Rows[hi][0].ToString();
                        bool isChk = false;
                        for (int ab = 0; ab < distinctTable.Rows.Count; ab++)
                        {
                            if (distinctTable.Rows[ab][0].ToString() == tGroupname)
                            {
                                isChk = true;
                            }
                        }
                        if (isChk == false)
                        {
                            distinctTable.Rows.Add(dtGroupNew.Rows[hi][0].ToString());
                        }
                    }
                }
                // DataTable distinctTable = dtGroupNew.DefaultView.ToTable( /*distinct*/ true);
                dtDisplay.Rows.Clear();
                //  DataTable distinctTable = dtGroupNew.DefaultView.ToTable(true);
                double tGroupTotQty = 0, tGroupTotAmt = 0;
                int tRowNo = 0;
                List<int> list = new List<int>();
                list.Clear();
                for (int ij = 0; ij < distinctTable.Rows.Count; ij++)
                {
                    tRowNo = dtDisplay.Rows.Count;
                    list.Add(tRowNo);
                    dtDisplay.Rows.Add(dtDisplay.Rows.Count + 1, "", distinctTable.Rows[ij][0].ToString(), "", "");
                    tGroupTotQty = 0;
                    tGroupTotAmt = 0;
                    for (int kl = 0; kl < dtGroupItem.Rows.Count; kl++)
                    {

                        if (distinctTable.Rows[ij][0].ToString() == dtGroupItem.Rows[kl][0].ToString())
                        {
                            for (int x = 0; x < dtNew.Rows.Count; x++)
                            {
                                if (dtGroupItem.Rows[kl][1].ToString() == dtNew.Rows[x]["Item_name"].ToString())
                                {
                                    tGroupTotQty += double.Parse(dtNew.Rows[x]["nt_qty"].ToString());
                                    tGroupTotAmt += double.Parse(dtNew.Rows[x]["Amount"].ToString());
                                    tTotAmt += double.Parse(dtNew.Rows[x]["Amount"].ToString());
                                    tTotQty += double.Parse(dtNew.Rows[x]["nt_qty"].ToString());
                                    if (txtType.Text.Trim() == "Detail")
                                    {
                                        dtDisplay.Rows.Add(dtDisplay.Rows.Count + 1, dtNew.Rows[x]["Item_code"].ToString(), dtNew.Rows[x]["Item_name"].ToString(), dtNew.Rows[x]["nt_qty"].ToString(), string.Format("{0:0.00}", double.Parse(dtNew.Rows[x]["Amount"].ToString())));
                                    }
                                }
                            }
                        }
                    }
                    dtDisplay.Rows[tRowNo]["nt_Qty"] = tGroupTotQty;
                    dtDisplay.Rows[tRowNo]["Amount"] = string.Format("{0:0.00}", tGroupTotAmt);
                }
                int[] rowNumber = list.ToArray();
                grd_SalesSummary.DataSource = dtDisplay;
                grd_SalesSummary.ReadOnly = false;
                int isChkRow = 0;
                for (int bc = 0; bc < grd_SalesSummary.Rows.Count; bc++)
                {
                    if (bc == rowNumber[isChkRow])
                    {
                        grd_SalesSummary.Rows[bc].ReadOnly = false;
                        grd_SalesSummary.Rows[bc].DefaultCellStyle.ForeColor = Color.Blue;
                        if (isChkRow < rowNumber.Length - 1)
                        {
                            isChkRow += 1;
                        }
                    }
                    else
                    {
                        grd_SalesSummary.Rows[bc].ReadOnly = true;
                    }
                }

            }

            else if (txt_OrderBy.Text.Trim() == "Model")
            {
                DataTable dtGroupItem = new DataTable();
                if (dtGroupItem.Columns.Count == 0)
                {
                    dtGroupItem.Columns.Add("GroupName", typeof(string));
                    dtGroupItem.Columns.Add("ItemName", typeof(string));
                }
                DataTable dtGroupNew = new DataTable();
                if (dtGroupNew.Columns.Count == 0)
                {
                    dtGroupNew.Columns.Add("GroupName", typeof(string));
                }
                dtGroupItem.Rows.Clear();
                dtGroupNew.Rows.Clear();
                DataTable dtGroup = new DataTable();

                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    string itemName = dtNew.Rows[mn]["Item_name"].ToString();
                    SqlCommand cmd1 = new SqlCommand("select Model_name from Model_table where Model_no=(select Model_no from Item_table where Item_name=@tItemName)", con);
                    cmd1.Parameters.AddWithValue("@tItemName", itemName);
                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                    dtGroup.Rows.Clear();
                    adp1.Fill(dtGroup);
                    if (dtGroup.Rows.Count > 0)
                    {
                        dtGroupItem.Rows.Add(dtGroup.Rows[0][0].ToString(), dtNew.Rows[mn]["Item_name"].ToString());
                        dtGroupNew.Rows.Add(dtGroup.Rows[0][0].ToString());
                    }
                }

                DataTable distinctTable = new DataTable();
                if (distinctTable.Columns.Count == 0)
                {
                    distinctTable.Columns.Add("GroupName", typeof(string));
                }
                for (int hi = 0; hi < dtGroupNew.Rows.Count; hi++)
                {
                    if (hi == 0)
                    {
                        distinctTable.Rows.Add(dtGroupNew.Rows[hi][0].ToString());
                    }
                    else
                    {
                        string tGroupname = dtGroupNew.Rows[hi][0].ToString();
                        bool isChk = false;
                        for (int ab = 0; ab < distinctTable.Rows.Count; ab++)
                        {
                            if (distinctTable.Rows[ab][0].ToString() == tGroupname)
                            {
                                isChk = true;
                            }
                        }
                        if (isChk == false)
                        {
                            distinctTable.Rows.Add(dtGroupNew.Rows[hi][0].ToString());
                        }
                    }
                }
                // DataTable distinctTable = dtGroupNew.DefaultView.ToTable( /*distinct*/ true);
                dtDisplay.Rows.Clear();
                //  DataTable distinctTable = dtGroupNew.DefaultView.ToTable(true);
                double tGroupTotQty = 0, tGroupTotAmt = 0;
                int tRowNo = 0;
                List<int> list = new List<int>();
                list.Clear();

                for (int ij = 0; ij < distinctTable.Rows.Count; ij++)
                {
                    tRowNo = dtDisplay.Rows.Count;
                    list.Add(tRowNo);
                    dtDisplay.Rows.Add(dtDisplay.Rows.Count + 1, "", distinctTable.Rows[ij][0].ToString(), "", "");
                    tGroupTotQty = 0;
                    tGroupTotAmt = 0;
                    for (int kl = 0; kl < dtGroupItem.Rows.Count; kl++)
                    {

                        if (distinctTable.Rows[ij][0].ToString() == dtGroupItem.Rows[kl][0].ToString())
                        {
                            for (int x = 0; x < dtNew.Rows.Count; x++)
                            {
                                if (dtGroupItem.Rows[kl][1].ToString() == dtNew.Rows[x]["Item_name"].ToString())
                                {
                                    tGroupTotQty += double.Parse(dtNew.Rows[x]["nt_qty"].ToString());
                                    tGroupTotAmt += double.Parse(dtNew.Rows[x]["Amount"].ToString());
                                    tTotAmt += double.Parse(dtNew.Rows[x]["Amount"].ToString());
                                    tTotQty += double.Parse(dtNew.Rows[x]["nt_qty"].ToString());
                                    if (txtType.Text.Trim() == "Detail")
                                    {
                                        dtDisplay.Rows.Add(dtDisplay.Rows.Count + 1, dtNew.Rows[x]["Item_code"].ToString(), dtNew.Rows[x]["Item_name"].ToString(), dtNew.Rows[x]["nt_qty"].ToString(),string.Format("{0:0.00}",double.Parse(dtNew.Rows[x]["Amount"].ToString())));
                                    }
                                }
                            }
                        }
                    }
                    dtDisplay.Rows[tRowNo]["nt_Qty"] = tGroupTotQty;
                    dtDisplay.Rows[tRowNo]["Amount"] = string.Format("{0:0.00}", tGroupTotAmt);
                }
                int[] rowNumber = list.ToArray();
                grd_SalesSummary.DataSource = dtDisplay;
                grd_SalesSummary.ReadOnly = false;
                int isChkRow = 0;
                for (int bc = 0; bc < grd_SalesSummary.Rows.Count; bc++)
                {
                    if (bc == rowNumber[isChkRow])
                    {
                        grd_SalesSummary.Rows[bc].ReadOnly = false;
                        grd_SalesSummary.Rows[bc].DefaultCellStyle.ForeColor = Color.Blue;
                        if (isChkRow < rowNumber.Length - 1)
                        {
                            isChkRow += 1;
                        }
                    }
                    else
                    {
                        grd_SalesSummary.Rows[bc].ReadOnly = true;
                    }
                }

            }
            if (txt_OrderBy.Text != "Item")
            {
                lblTotalAmt.Text = string.Format("{0:0.00}", tTotAmt / 2);
                lblTotalQty.Text = string.Format("{0:0.00}", tTotQty / 2);
            }
            else
            {
                lblTotalAmt.Text = string.Format("{0:0.00}", tTotAmt);
                lblTotalQty.Text = string.Format("{0:0.00}", tTotQty);
            }
                foreach (DataGridViewColumn col in grd_SalesSummary.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                grd_SalesSummary.Columns[0].Width = 200;
                grd_SalesSummary.Columns[1].Width = 200;
                grd_SalesSummary.Columns[2].Width = 350;
                grd_SalesSummary.Columns[3].Width = 250;
                grd_SalesSummary.Columns[4].Width = 250;


                Pnl_Back2.Visible = false;

        }
        double tTotQty, tTotAmt;
        private void funTotal(DataTable dtChk)
        {
            tTotQty=0;
            tTotAmt=0;
            for (int mn = 0; mn < dtChk.Rows.Count; mn++)
            {
                tTotQty += double.Parse(dtChk.Rows[mn]["nt_Qty"].ToString());
                tTotAmt += double.Parse(dtChk.Rows[mn]["Amount"].ToString());
            }
            lblTotalQty.Text = string.Format("{0:0.00}", tTotQty);
            lblTotalAmt.Text = string.Format("{0:0.00}", tTotAmt);
        }
        private void txt_ReportOn_Click(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            listSelect.Items.Clear();
            tFocusActionType="REPORT";
            listSelect.Items.Add("Gross Amount");
            listSelect.Items.Add("Nett Amount");
            listSelect.SetSelected(0, true);
            txt_ReportOn.Text = listSelect.SelectedItem.ToString();
        }

        private void txt_OrderBy_Click(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            listSelect.Items.Clear();
            tFocusActionType="ORDER";
            listSelect.Items.Add("Brand");
            listSelect.Items.Add("Group");
            listSelect.Items.Add("Item");
            listSelect.Items.Add("Model");
            listSelect.SetSelected(2, true);
            txt_OrderBy.Text = listSelect.SelectedItem.ToString();
        }

        private void txt_salestypes_Click(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            listSelect.Items.Clear();
           tFocusActionType="SALESTYPE";
            listSelect.Items.Add("All");
            listSelect.Items.Add("Cash");
            listSelect.Items.Add("Credit");
           // lst_Boxitem.Items.Add("Model");
            listSelect.SetSelected(0, true);
            txt_salestypes.Text = listSelect.SelectedItem.ToString();
        }

        private void txt_Brand_Click(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            tFocusActionType = "BRAND";
            listbox_values();
        }

        private void txt_Brand_TextChanged(object sender, EventArgs e)
        {
            if (txt_Brand.Text.Trim() != null &&txt_Brand.Text.Trim() != "")
            {
                DataTable dtNew4 = new DataTable();
                dtNew4.Rows.Clear();
                SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmdCno.CommandType = CommandType.StoredProcedure;
                cmdCno.Parameters.AddWithValue("@tActionType", "BRAND");
                cmdCno.Parameters.AddWithValue("@tValue", txt_Brand.Text.Trim());
                SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
                adp4.Fill(dtNew4);
                bool isChk = false;
                for (int mn = 0; mn < dtNew4.Rows.Count; mn++)
                {
                    isChk = true;
                    string tempStr = dtNew4.Rows[mn][0].ToString();
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        if (dtNew4.Rows[mn][0].ToString() == listSelect.Items[i].ToString())
                        {

                            listSelect.SetSelected(i, true);
                            txt_Brand.Select();
                            chk = "1";
                            txt_Brand.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }

                    }
                }
                if (isChk == false)
                {
                    chk = "2";
                    txt_Brand.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";

            }
        }

        private void txt_Counter_TextChanged(object sender, EventArgs e)
        {
           if (txt_Counter.Text.Trim() != null && txt_Counter.Text.Trim() != "")
            {
                DataTable dtNew4 = new DataTable();
                dtNew4.Rows.Clear();
                SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmdCno.CommandType = CommandType.StoredProcedure;
                cmdCno.Parameters.AddWithValue("@tActionType", "COUNTER");
                cmdCno.Parameters.AddWithValue("@tValue", txt_Counter.Text.Trim());
                SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
                adp4.Fill(dtNew4);
                bool isChk = false;
                for (int mn = 0; mn < dtNew4.Rows.Count; mn++)
                {
                    isChk = true;
                    string tempStr = dtNew4.Rows[mn][0].ToString();
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        if (dtNew4.Rows[mn][0].ToString() == listSelect.Items[i].ToString())
                        {
                            listSelect.SetSelected(i, true);
                            txt_Counter.Select();
                            chk = "1";
                            txt_Counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk = "2";
                    txt_Counter.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";
            }
        }

        private void txt_Counter_Enter(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            tFocusActionType = "COUNTER";
            listbox_values();
        }

        private void txt_model_Click(object sender, EventArgs e)
        {
            Pnllistselect.Visible = true;
            tFocusActionType = "MODEL";
            listbox_values();
        }

        private void txt_model_TextChanged(object sender, EventArgs e)
        {
            if (txt_model.Text.Trim() != null && txt_model.Text.Trim() != "")
            {
                DataTable dtNew4 = new DataTable();
                dtNew4.Rows.Clear();
                SqlCommand cmdCno = new SqlCommand("sp_SalesAlterationSelectSingle", con);
                cmdCno.CommandType = CommandType.StoredProcedure;
                cmdCno.Parameters.AddWithValue("@tActionType", "MODEL");
                cmdCno.Parameters.AddWithValue("@tValue", txt_model.Text.Trim());
                SqlDataAdapter adp4 = new SqlDataAdapter(cmdCno);
                adp4.Fill(dtNew4);
                bool isChk = false;
                for (int mn = 0; mn < dtNew4.Rows.Count; mn++)
                {
                    isChk = true;
                    string tempStr = dtNew4.Rows[mn][0].ToString();
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        if (dtNew4.Rows[mn][0].ToString() == listSelect.Items[i].ToString())
                        {
                            listSelect.SetSelected(i, true);
                            txt_model.Select();
                            chk = "1";
                            txt_model.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk = "2";
                    txt_model.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
            }
            else
            {
                chk = "1";
            }
        }

        private void txt_ReportOn_TextChanged(object sender, EventArgs e)
        {
            if (tStop != true)
            {
                tStop = false;
                if (txt_ReportOn.Text.Trim() != null && txt_ReportOn.Text.Trim() != "")
                {
                    string chkStr1 = "", chkstr2 = "";
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txt_ReportOn.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txt_ReportOn.Text.Length);
                            bool isChk = false;
                            if (txt_ReportOn.Text.Trim() == chkstr2 || txt_ReportOn.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                listSelect.SetSelected(i, true);
                                txt_ReportOn.Select();
                                chk = "1";
                                txt_ReportOn.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txt_ReportOn.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            }
                        }
                    }

                }
                else
                {
                    chk = "1";
                }
            }

        }

        private void txt_OrderBy_TextChanged(object sender, EventArgs e)
        {
            if (tStop != true)
            {
                tStop = false;
                if (txt_OrderBy.Text.Trim() != null && txt_OrderBy.Text.Trim() != "")
                {
                    string chkStr1 = "", chkstr2 = "";
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txt_OrderBy.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txt_OrderBy.Text.Length);
                            bool isChk = false;
                            if (txt_OrderBy.Text.Trim() == chkstr2 || txt_OrderBy.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                listSelect.SetSelected(i, true);
                                txt_OrderBy.Select();
                                chk = "1";
                                txt_OrderBy.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txt_OrderBy.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            }
                        }
                    }
                }
                else
                {
                    chk = "1";
                }
            }
        }

        private void txt_salestypes_TextChanged(object sender, EventArgs e)
        {
            if (tStop != true)
            {
                tStop = false;
                if (txt_salestypes.Text.Trim() != null && txt_salestypes.Text.Trim() != "")
                {
                    string chkStr1 = "", chkstr2 = "";
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txt_salestypes.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txt_salestypes.Text.Length);
                            bool isChk = false;
                            if (txt_salestypes.Text.Trim() == chkstr2 || txt_salestypes.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                listSelect.SetSelected(i, true);
                                txt_salestypes.Select();
                                chk = "1";
                                txt_salestypes.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txt_salestypes.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            }
                        }
                    }

                }
                else
                {
                    chk = "1";
                }
            }
        }

        private void txtType_Click(object sender, EventArgs e)
        {
            if (txt_OrderBy.Text != "Item")
            {
                txtType.ReadOnly = false;
                Pnllistselect.Visible = true;
                listSelect.Items.Clear();
                tFocusActionType = "TYPE";
                listSelect.Items.Add("Summary");
                listSelect.Items.Add("Detail");
                // lst_Boxitem.Items.Add("Model");
                listSelect.SetSelected(0, true);
                txtType.Text = listSelect.SelectedItem.ToString();
            }
            else
            {
                txtType.ReadOnly = true;
            }
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            if (tStop != true)
            {
                tStop = false;
                if (txtType.Text.Trim() != null && txtType.Text.Trim() != "")
                {
                    string chkStr1 = "", chkstr2 = "";
                    for (int i = 0; i < listSelect.Items.Count; i++)
                    {
                        chkStr1 = listSelect.Items[i].ToString();
                        if (txtType.Text.Length <= chkStr1.Length)
                        {
                            chkstr2 = chkStr1.Substring(0, txtType.Text.Length);
                            bool isChk = false;
                            if (txtType.Text.Trim() == chkstr2 || txtType.Text.Trim() == chkstr2.ToLower())
                            {
                                isChk = true;
                                listSelect.SetSelected(i, true);
                                txtType.Select();
                                chk = "1";
                                txtType.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);

                                break;
                            }
                            if (isChk == false)
                            {
                                chk = "2";
                                txtType.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            }
                        }
                    }

                }
                else
                {
                    chk = "1";
                }
            }
        }
        private void txt_OrderByNew_TextChanged(object sender, EventArgs e)
        {
        }
        bool tStop = false;
        private void listSelect_MouseClick(object sender, MouseEventArgs e)
        {
            

            //if (listSelect.Text != "")
            //{

            if (tFocusActionType == "TYPE")
            {
                tStop = true;
                if (listSelect.SelectedItems.Count > 0)
                {
                    txtType.Text = listSelect.SelectedItem.ToString();
                    // listbox_values();
                    btn_ok.Select();
                }
                else
                {
                    btn_ok.Select();
                }
            }

            if (tFocusActionType == "MODEL")
            {
                if (listSelect.SelectedItems.Count > 0)
                {
                    txt_model.Text = listSelect.SelectedItem.ToString();
                    listbox_values();
                    if (txt_OrderBy.Text == "Item")
                    {
                        btn_ok.Select();
                    }
                    else
                    {
                        txtType.Select();
                    }
                }
                else
                {
                    if (txt_OrderBy.Text == "Item")
                    {
                        btn_ok.Select();
                    }
                    else
                    {
                        txtType.Select();
                    }
                    // btn_ok.Select();
                }
            }
            if (tFocusActionType == "SALESTYPE")
            {
                tStop = true;
                if (listSelect.SelectedItems.Count > 0)
                {
                    txt_salestypes.Text = listSelect.SelectedItem.ToString();
                    txt_model.Select();
                }
                else
                {
                    txt_model.Select();
                }
            }

            if (tFocusActionType == "COUNTER")
            {
                
                if (listSelect.SelectedItems.Count > 0)
                {
                    txt_Counter.Text = listSelect.SelectedItem.ToString();
                    listbox_values();
                    txt_salestypes.Select();
                }
                else
                {
                    txt_salestypes.Select();
                }
            }
            if (tFocusActionType == "BRAND")
            {
                if (listSelect.SelectedItems.Count > 0)
                {
                    txt_Brand.Text = listSelect.SelectedItem.ToString();
                    listbox_values();
                    txt_Counter.Select();
                }
                else
                {
                    txt_Counter.Select();
                }
            }
            if (tFocusActionType == "GROUP")
            {
                if (listSelect.SelectedItems.Count > 0)
                {
                    txt_Group.Text = listSelect.SelectedItem.ToString();
                    listbox_values();
                    txt_Brand.Select();
                }
                else
                {
                    txt_Brand.Select();
                }
            }

            if (tFocusActionType == "ORDER")
            {
                if (listSelect.SelectedItems.Count > 0)
                {
                    tStop = true;
                   // txt_OrderBy.TextChanged+= new EventHandler(txt_OrderByNew_TextChanged);
                    txt_OrderBy.Text = listSelect.SelectedItem.ToString();
                    if (txt_OrderBy.Text == "Item")
                    {
                        txtType.ReadOnly = true;
                        txtType.Text = "Detail";
                    }
                    else
                    {
                        txtType.ReadOnly = false;
                        txtType.Text = "Summary";
                    }
                    txt_Group.Select();
                }
                else
                {
                    txt_Group.Select();
                }
            }

            if (tFocusActionType == "REPORT")
            {
                tStop = true;
                if (listSelect.SelectedItems.Count > 0)
                {
                    txt_ReportOn.Text = listSelect.SelectedItem.ToString();
                    txt_OrderBy.Select();
                }
                else
                {
                    txt_OrderBy.Select();
                }
            }
            Pnllistselect.Visible = false;
                    
        }

        private void lst_Boxitem_MouseClick(object sender, MouseEventArgs e)
        {
            if (lst_Boxitem.SelectedItems.Count > 0)
            {
                passingvalues.tAmountType = "Gross Amount";
                txt_customer.Text = lst_Boxitem.SelectedItem.ToString();
                grd_SalesSummary.Show();
                CustomerInfo();
                pnlCustomer.Visible = false;
                lst_Boxitem.Visible = false;
            }
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                Dataset.dsSalesSummary dsSalesSummaryObj = new Dataset.dsSalesSummary();
                for (int i = 0; i < grd_SalesSummary.Rows.Count; i++)
                {
                    dsSalesSummaryObj.Tables["DataTable4"].Rows.Add(Convert.ToString(grd_SalesSummary.Rows[i].Cells[0].Value), Convert.ToString(grd_SalesSummary.Rows[i].Cells[1].Value), Convert.ToString(grd_SalesSummary.Rows[i].Cells[2].Value), Convert.ToString(grd_SalesSummary.Rows[i].Cells[3].Value), Convert.ToString(grd_SalesSummary.Rows[i].Cells[4].Value), Convert.ToString(txt_from.Value.Day + "/" + txt_from.Value.Month + "/" + txt_from.Value.Year), Convert.ToString(txt_to.Value.Day + "/" + txt_to.Value.Month + "/" + txt_to.Value.Year), Convert.ToString(txt_customer.Text));
                }
                reportViewerSales.Reset();
                //  DataTable dt = getDate();
                ReportDataSource ds = new ReportDataSource("DataSet1", dsSalesSummaryObj.Tables["DataTable4"]);
                reportViewerSales.LocalReport.DataSources.Add(ds);

                reportViewerSales.LocalReport.ReportEmbeddedResource = "MSPOSBACKOFFICE.ReportFile.rdlcItemwiseSalesSummary.rdlc";
                //Passing Parmetes:
                ReportParameter rpReportOn = new ReportParameter("TotalQty", Convert.ToString(lblTotalQty.Text), false);
                ReportParameter rpCounter = new ReportParameter("TotalAmt", Convert.ToString(lblTotalAmt.Text), false);
                //ReportParameter rpFrom = new ReportParameter("From", Convert.ToString(dt_from.Value.Day + "/" + dt_from.Value.Month + "/" + dt_from.Value.Year), false);
                //ReportParameter rpTo = new ReportParameter("To", Convert.ToString(dt_to.Value.Day + "/" + dt_to.Value.Month + "/" + dt_to.Value.Year), false);
                ////ReportParameter rpCash = new ReportParameter("Cash", Convert.ToString(txt_cash.Text), false);
                //ReportParameter rpSalesType = new ReportParameter("SalesType", Convert.ToString(txt_sales.Text), false);
                //ReportParameter rpParty = new ReportParameter("Party", Convert.ToString(txt_ledger.Text), false);
                this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn, rpCounter});
                //, rpCash, rpSalesType, rpParty });
                dsSalesSummaryObj.Tables["DataTable4"].EndInit();
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
