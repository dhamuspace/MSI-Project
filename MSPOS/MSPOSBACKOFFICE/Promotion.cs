using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;


namespace MSPOSBACKOFFICE
{
    public partial class Promotion : Form
    {
        public Promotion()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dtLoad = new DataTable();
        DataTable dtLoad1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public string tFreeSno = "";
        public string tItemNameNew = "", ItemImage = "";
        public string itemnameexit = "";
        string blackValues = "";
        private void Promotion_Load(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(tFreeSno))

                dpfromdate.Focus();

                lbl_stckbanner.Text = "Special Offer Creation";
                //  dt.Columns.Add("Itemcode", typeof(string));
                dt.Columns.Add("Itemnames", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("Amt", typeof(string));
                dt.Columns.Add("Disc", typeof(string));
                dtLoad.Columns.Add("ItemCode", typeof(string));
                dtLoad.Columns.Add("ItemNames", typeof(string));
                dtLoad.Columns.Add("Free_Qty", typeof(string));
                dtLoad.Columns.Add("Rate", typeof(string));
                dtLoad.Columns.Add("Stock", typeof(string));

                //dtLoad1.Columns.Add("ItemCode", typeof(string));
                //dtLoad1.Columns.Add("ItemNames", typeof(string));
                //dtLoad1.Columns.Add("Qty", typeof(string));
                //dtLoad1.Columns.Add("Rate", typeof(string));
                //dtLoad1.Columns.Add("Amt", typeof(string));
                lbltotSalesQtyto.Visible = false;
                txtSalesQtyTo.Visible = false;

                cmbtype.SelectedIndex = 0;

                // dtLoad1.Rows.Add("","","");
                //  gridItemName.DataSource = dtLoad1;
                // gridItemName.Columns["Rate"].Visible = false;
                // gridItemName.Columns["Amt"].Visible = false;

                if (!string.IsNullOrEmpty(tFreeSno))
                {
                    itemnameexit = "";
                    //gridItemName.DataSource = null;
                    EnterIn = "1";
                    lbl_stckbanner.Text = "Special Offer Alteration";
                    txtitemname.Text = tItemNameNew;
                    DataTable dtDree = new DataTable();
                    dtDree.Rows.Clear();
                    SqlCommand cmdFree = new SqlCommand("Select Sunday,Sturday,Friday,Thursday,Wednesday,Tuesday,Monday,Active,FreeSno,Convert(Date,FromDate,103) As FromDate,Convert(date,ToDate,103) As ToDate,ItemType,OfferName,TotSaleQty,SaleQty, TotSalePrice,FreeType from FreeItemMaster_table where  FreeSnoGroup=@tFreeSno", con);
                    cmdFree.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdFree);
                    adp.Fill(dtDree);
                    dtLoad.Rows.Clear();
                    if (dtDree.Rows.Count > 0)
                    {
                        blackValues = "1";
                        dpfromdate.Value = Convert.ToDateTime(dtDree.Rows[0]["FromDate"].ToString());
                        dptodate.Value = Convert.ToDateTime(dtDree.Rows[0]["ToDate"].ToString());
                        TStpDeleteFreeItemRow.Text = dtDree.Rows[0]["OfferName"].ToString();
                        itemnameexit = dtDree.Rows[0]["OfferName"].ToString();
                        cmbItemType.Text = dtDree.Rows[0]["ItemType"].ToString();
                        if (cmbItemType.Text.Equals("Single") && (cmbtype.Text == "Same Free") ||(cmbItemType.Text.Equals("Single") && (cmbtype.Text.Equals("Price"))))
                        {
                            DataView dataView = new DataView(dtLoad);
                            dgFreeItemList.AllowUserToAddRows = false;
                            gridItemName.AllowUserToAddRows = false;
    
                            //dgFreeItemList.DataSource = dataView;
                        }
                        else if (cmbtype.Text.Equals("Price") && cmbItemType.Text == "Different")
                        {
                            DataView dataView = new DataView(dtLoad);
                            dgFreeItemList.AllowUserToAddRows = true;
                            cmbtype.Enabled = false;
                            lbltotSalesQtyto.Visible = true;
                            txtSalesQtyTo.Visible = true;
                        }
                        else
                        {
                            DataView dataView = new DataView(dtLoad);
                            dgFreeItemList.AllowUserToAddRows = true;
                            lbltotSalesQtyto.Visible = false;
                            txtSalesQtyTo.Visible = false;
                            // dgFreeItemList.DataSource = dataView;
                        }
                        cmbtype.Text = dtDree.Rows[0]["FreeType"].ToString();
                        if (cmbtype.Text == "Price")
                        {
                            txtsalesrate.Enabled = true;
                        }
                        else
                        {
                            txtsalesrate.Enabled = false;
                        }
                        txtActive.Text = dtDree.Rows[0]["Active"].ToString() == "1" ? "ACTIVE" : "NO";
                        txtsalesqtyFrom.Text =Convert.ToDouble(dtDree.Rows[0]["TotSaleQty"].ToString()).ToString("0");
                        txtSalesQtyTo.Text = Convert.ToDouble(dtDree.Rows[0]["SaleQty"].ToString()).ToString("0");
                        txtsalesrate.Text = dtDree.Rows[0]["TotSalePrice"].ToString();
                        Chksunday.Checked = dtDree.Rows[0]["Sunday"].ToString().Trim() == "1" ? true : false;
                        ChkMonday.Checked = dtDree.Rows[0]["Monday"].ToString().Trim() == "1" ? true : false;
                        ChkTuesday.Checked = dtDree.Rows[0]["Tuesday"].ToString().Trim() == "1" ? true : false;
                        ChkWednesDay.Checked = dtDree.Rows[0]["Wednesday"].ToString().Trim() == "1" ? true : false;
                        Chkthursday.Checked = dtDree.Rows[0]["Thursday"].ToString().Trim() == "1" ? true : false;
                        ChkFriday.Checked = dtDree.Rows[0]["Friday"].ToString().Trim() == "1" ? true : false;
                        ChkSturday.Checked = dtDree.Rows[0]["Sturday"].ToString().Trim() == "1" ? true : false;
                        SqlCommand cmd = new SqlCommand("select item_table.item_code AS ItemCode,item_table.Item_name As ItemNames,FreeItemMaster_table.totGQty As Qty from FreeItemMaster_table join Item_table on FreeItemMaster_table.Item_no=Item_table.Item_no where freeItemMaster_table.FreeSnoGroup=@tFreeSno", con);
                        cmd.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                        SqlDataAdapter adpMaster = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        dt1.Rows.Clear();
                        adpMaster.Fill(dt1);
                        dtLoad1.Rows.Clear();
                        for (int k = 0; k < dt1.Rows.Count; k++)
                        {
                            gridItemName.Rows.Add(dt1.Rows[k]["ItemCode"].ToString(), dt1.Rows[k]["ItemNames"].ToString(), dt1.Rows[k]["Qty"].ToString());
                            //dtLoad1.Rows.Add(,"","");
                        }
                        //gridItemName.DataSource = dtLoad1;
                        //panel2.Visible = false;
                    }
                    SqlCommand cmdFreeItem = new SqlCommand("select Item_table.Item_code,Item_table.Item_name,FreeItemDetail_table.FreeQty,FreeItemDetail_table.FreeRate from FreeItemDetail_table join Item_table on FreeItemDetail_table.FreeItem_no=Item_table.Item_no where FreeItemDetail_table.FreeSno=@tFreeSno", con);
                    cmdFreeItem.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                    SqlDataAdapter adpFreeItem = new SqlDataAdapter(cmdFreeItem);
                    DataTable dtFreeItem = new DataTable();
                    dtFreeItem.Rows.Clear();
                    adpFreeItem.Fill(dtFreeItem);
                    if (dtFreeItem.Rows.Count > 0)
                    {
                        dtLoad.Rows.Clear();
                        for (int k = 0; k < dtFreeItem.Rows.Count; k++)
                        {
                            dtLoad.Rows.Add(dtFreeItem.Rows[k]["Item_code"].ToString(), dtFreeItem.Rows[k]["Item_name"].ToString(), dtFreeItem.Rows[k]["FreeQty"].ToString(), "", "");
                        }
                        dgFreeItemList.DataSource = dtLoad;
                    }
                    if (!cmbtype.Text.Equals("Price"))
                    {
                        dgFreeItemList.DataSource = dtLoad;
                        dgFreeItemList.Columns["Rate"].Visible = false;
                        dgFreeItemList.Columns["Stock"].Visible = false;
                    }
                }
                EnterIn = "0";

                gridItemName.Columns[0].DefaultCellStyle.ForeColor = Color.Black;
                gridItemName.Columns[1].DefaultCellStyle.ForeColor = Color.Black;
                gridItemName.Columns[2].DefaultCellStyle.ForeColor = Color.Black;

              //  gridItemName.ReadOnly = true;
                blackValues = "";
                //gridItemName.Rows[3].DefaultCellStyle.ForeColor = Color.Gray;
                if (string.IsNullOrEmpty(txtsalesqtyFrom.Text))
                {
                    gridItemName.ReadOnly = true;
                }

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
                groupBox1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            }
            catch(Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"warning");
            }
        }
        private void Promotion_Paint(object sender, PaintEventArgs e)
        {
            //Graphics mGraphics = e.Graphics;
            //Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            //Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(106, 90, 205), Color.FromArgb(132, 112, 255), LinearGradientMode.Vertical);
            //mGraphics.FillRectangle(LGB, Area1);
            //mGraphics.DrawRectangle(pen1, Area1);


            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 56, 96), Color.FromArgb(245, 251, 251), LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 56, 96), Color.FromArgb(245, 251, 251), LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);

        }

        private void Promotion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Escape)
            {
                this.Close();
            }
        }
        private DataTable AutoNumberedTable(DataTable SourceTable)
        {
            DataTable ResultTable = new DataTable();
            DataColumn AutoNumberColumn = new DataColumn();
            AutoNumberColumn.ColumnName = "sno";

            AutoNumberColumn.DataType = typeof(int);

            AutoNumberColumn.AutoIncrement = true;

            AutoNumberColumn.AutoIncrementSeed = 1;

            AutoNumberColumn.AutoIncrementStep = 1;

            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);

            return ResultTable;
        }
        public event System.EventHandler SalesCreationEventHandlerNew;
        string itemnumber;
        public void nametonumber()
        {
            try
            {
                isChkAgain = true;
                con.Close();
                con.Open();
                if (txtitemname.Text.Trim() != "")
                {
                    DataTable dtitemname = new DataTable();
                    SqlCommand cmd = new SqlCommand("select item_code,item_name,item_no from item_table where Item_Active='1' and item_name=@tItemName", con);
                    cmd.Parameters.AddWithValue("@tItemName", txtitemname.Text.Trim());
                    SqlDataAdapter adp123 = new SqlDataAdapter(cmd);
                    dtitemname.Rows.Clear();
                    adp123.Fill(dtitemname);
                    if (dtitemname.Rows.Count > 0)
                    {
                        TStpDeleteFreeItemRow.Text = dtitemname.Rows[0]["item_code"].ToString();
                        // txtitemname.Text = dtitemname.Rows[0]["item_name"].ToString();
                        itemnumber = dtitemname.Rows[0]["item_no"].ToString();
                    }
                    if (!string.IsNullOrEmpty(itemnumber))
                    {
                        DataTable dtChk = new DataTable();
                        dtChk.Rows.Clear();
                        SqlCommand cmdChk = new SqlCommand("Select * from FreeItem_table where Item_no=@tItemNo and FromDate<=@tFromDate and ToDate>=@tToDate", con);
                        cmdChk.Parameters.AddWithValue("@tItemNo", itemnumber);
                        cmdChk.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                        cmdChk.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                        SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
                        adpChk.Fill(dtChk);
                        if (dtChk.Rows.Count > 0)
                        {
                            MyMessageBox.ShowBox("Same item offer already exists", "Warning");
                        }
                    }
                }
                //cmbtype.Select();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "warning");
            }

        }
        public bool validation()
        {
            if (TStpDeleteFreeItemRow.Text.Trim() == "")
            {
                MessageBox.Show("Enter Itemcode", "Warning");
                TStpDeleteFreeItemRow.Select();
                return false;
            }
            else if (cmbtype.Text.Trim() == "")
            {
                MessageBox.Show("Enter Type", "Warning");
                cmbtype.Select();
                return false;
            }
            else if (txtitemname.Text.Trim() == "")
            {
                MessageBox.Show("Enter ItemName", "Warning");
                txtitemname.Text = "";
                txtitemname.Select();
                return false;
            }

            else if (txtsalesrate.Text == "")
            {
                txtsalesrate.Text = "0.00";
                txtsalesrate.Select();
                return false;
            }
            else if (txtsalesqtyFrom.Text == "")
            {
                txtsalesqtyFrom.Text = "0.00";

                return false;
            }
            else
            {
                return true;
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation())
                {
                    DataRow dr = null;
                    dr = dt.NewRow();
                    dr["sno"] = "";
                    dr["item_code"] = TStpDeleteFreeItemRow.Text;
                    dr["item_name"] = txtitemname.Text;
                    dr["sales_rate"] = txtsalesrate.Text;
                    dr["sales_qty"] = txtsalesqtyFrom.Text;
                    dr["type"] = cmbtype.Text;
                    if (txtitemname.Text.Trim() != "")
                    {
                        SqlCommand cmd = new SqlCommand("select rate from item_table where item_name='" + txtitemname.Text + "'", con);
                        string rate = cmd.ExecuteScalar().ToString();
                        dr["rate"] = rate.ToString();
                    }
                    dt.Rows.Add(dr);
                    TStpDeleteFreeItemRow.Text = "";
                    txtitemname.Text = "";


                    if (dt.Rows.Count == 1)
                    {
                        promotion_no();
                        entry_no();
                        con.Close();
                        con.Open();
                        SqlCommand cmd = new SqlCommand(@"insert into [Promotion_table]([Entry_no],[Promotion_no],[Item_no],[type],[Entry_Date],[From_date],[To_date],[Sales_Qty],[Sales_Rate]) values('" + entry_number + "','" + promotion_number + "','" + itemnumber + "','" + cmbtype.Text + "','" + dtpentryDate.Value.ToString("yyyy-MM-dd") + "','" + dpfromdate.Value.ToString("yyyy-MM-dd") + "','" + dptodate.Value.ToString("yyyy-MM-dd") + "','" + txtsalesqtyFrom.Text + "','" + txtsalesrate.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        int i = Convert.ToInt32(dt.Rows.Count);
                        dr["sno"] = i.ToString();
                        dgFreeItemList.DataSource = dt;
                        dgFreeItemList.Columns["Rate"].Visible = false;
                        dgFreeItemList.Columns["Stock"].Visible = false;
                    }
                    else
                    {
                        DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                        string lasvalues = lastRow["type"].ToString();
                        if (lasvalues == "Any")
                        {
                            DataRow lastRow1 = dt.Rows[dt.Rows.Count - 2];
                            string lastrowss = lastRow1["type"].ToString();
                            if (lastrowss == lasvalues)
                            {
                                promotion_no();
                                entry_no();
                                entry_no();
                                nametonumber();
                                int i = 0;
                                i = Convert.ToInt32(promotion_number);
                                promotion_number = Convert.ToString(Convert.ToInt32(i - 1)).ToString();
                                con.Close();
                                con.Open();
                                SqlCommand cmd = new SqlCommand(@"insert into [Promotion_table]([Entry_no],[Promotion_no],[Item_no],[type],[Entry_Date],[From_date],[To_date],[Sales_Qty],[Sales_Rate]) values('" + entry_number + "','" + promotion_number + "','" + itemnumber + "','" + cmbtype.Text + "','" + dtpentryDate.Value.ToString("yyyy-MM-dd") + "','" + dpfromdate.Value.ToString("yyyy-MM-dd") + "','" + dptodate.Value.ToString("yyyy-MM-dd") + "','" + txtsalesqtyFrom.Text + "','" + txtsalesrate.Text + "')", con);
                                cmd.ExecuteNonQuery();
                                dgFreeItemList.DataSource = dt;
                                dgFreeItemList.Columns["Rate"].Visible = false;
                                dgFreeItemList.Columns["Stock"].Visible = false;
                            }
                            else
                            {
                                string id = "";
                                string ju;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ju = dt.Rows[i]["sno"].ToString();
                                    if (ju != "")
                                    {
                                        id = ju.ToString();
                                    }
                                }
                                promotion_no();
                                con.Close();
                                nametonumber();
                                con.Close();
                                entry_no();
                                con.Open();
                                SqlCommand cmd = new SqlCommand(@"insert into [Promotion_table]([Entry_no],[Promotion_no],[Item_no],[type],[Entry_Date],[From_date],[To_date],[Sales_Qty],[Sales_Rate]) values('" + entry_number + "','" + promotion_number + "','" + itemnumber + "','" + cmbtype.Text + "','" + dtpentryDate.Value.ToString("yyyy-MM-dd") + "','" + dpfromdate.Value.ToString("yyyy-MM-dd") + "','" + dptodate.Value.ToString("yyyy-MM-dd") + "','" + txtsalesqtyFrom.Text + "','" + txtsalesrate.Text + "')", con);
                                cmd.ExecuteNonQuery();
                                dr["sno"] = Convert.ToInt32(id.ToString()) + 1;
                                dgFreeItemList.DataSource = dt;
                                dgFreeItemList.Columns["Rate"].Visible = false;
                                dgFreeItemList.Columns["Stock"].Visible = false;
                            }
                        }
                        else
                        {
                            string id = "";
                            string ju;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                ju = dt.Rows[i]["sno"].ToString();
                                if (ju != "")
                                {
                                    id = ju.ToString();
                                }
                            }
                            dr["sno"] = Convert.ToInt32(id.ToString()) + 1;
                            promotion_no();
                            entry_no();
                            con.Close();
                            nametonumber();
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand(@"insert into [Promotion_table]([Entry_no],[Promotion_no],[Item_no],[type],[Entry_Date],[From_date],[To_date],[Sales_Qty],[Sales_Rate]) values('" + entry_number + "','" + promotion_number + "','" + itemnumber + "','" + cmbtype.Text + "','" + dtpentryDate.Value.ToString("yyyy-MM-dd") + "','" + dpfromdate.Value.ToString("yyyy-MM-dd") + "','" + dptodate.Value.ToString("yyyy-MM-dd") + "','" + txtsalesqtyFrom.Text + "','" + txtsalesrate.Text + "')", con);
                            cmd.ExecuteNonQuery();
                            dgFreeItemList.DataSource = dt;
                            dgFreeItemList.Columns["Rate"].Visible = false;
                            dgFreeItemList.Columns["Stock"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "warning");
            }

        }
        private void txtsalesqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }
        private void txtsalesrate_KeyPress(object sender, KeyPressEventArgs e)
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
        string promotion_number = "";
        public void promotion_no()
        {
            string qry = "select Max(Promotion_no) from Promotion_table";
            promotion_number = auto1(qry);
        }
        string entry_number;
        public void entry_no()
        {
            string qry = "select Max(Entry_no) from Promotion_table";
            entry_number = auto1(qry);
            con.Close();
        }
        public string auto1(string qry)
        {
            if (ConnectionState.Open == con.State)
            {
                con.Close();
            }
            con.Open();
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

        private void txtitemcode_Leave(object sender, EventArgs e)
        {

        }

        private void txtsalesqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtActive.Focus();
            }
        }

        private void cmbtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtsalesqtyFrom.Focus();
                TStpDeleteFreeItemRow.Focus();
            }
        }
        private void txtitemcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //cmbItemType.Select();
                txtsalesqtyFrom.Focus();
            }
        }
        private void txtitemname_Enter(object sender, EventArgs e)
        {
            try
            {
                isChkAgain = false;
                panel2.Visible = false;
                con.Close();
                con.Open();
                DataTable dt_itemname = new DataTable();
                SqlCommand cmd = new SqlCommand("select item_name from item_table", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_itemname.Rows.Clear();
                lstbox.Items.Clear();
                adp.Fill(dt_itemname);
                if (dt_itemname.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_itemname.Rows.Count; i++)
                    {
                        lstbox.Items.Add(dt_itemname.Rows[i]["item_name"].ToString());
                    }
                }
                if (txtitemname.Text.Trim() == "")
                {
                    if (lstbox.Items.Count > 0)
                    {
                        lstbox.SetSelected(0, true);
                    }
                }
                if (lstbox.Items.Count > 0)
                {
                    if (txtitemname.Text.Trim() != "")
                    {
                        for (int k = 0; k < dt_itemname.Rows.Count; k++)
                        {

                            if (txtitemname.Text == dt_itemname.Rows[k]["Item_name"].ToString())
                            {
                                lstbox.SetSelected(k, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "warning");
            }
        }
        private void lstbox_Click(object sender, EventArgs e)
        {
            if (lstbox.SelectedItems.Count > 0)
            {
                txtitemname.Text = lstbox.SelectedItem.ToString();
                txtsalesqtyFrom.Focus();
                txtsalesqtyFrom.Select();
                panel2.Visible = false;
            }
        }
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstbox.SelectedIndex < lstbox.Items.Count - 1)
                {
                    lstbox.SetSelected(lstbox.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstbox.SelectedIndex > 0)
                {
                    lstbox.SetSelected(lstbox.SelectedIndex - 1, true);
                }
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (panel2.Visible == true)
                {
                    if (lstbox.Text != "")
                    {
                        txtitemname.Text = lstbox.SelectedItem.ToString();
                        txtitemname.Select();
                    }
                }
                cmbtype.Select();
            }
        }
        bool isChkAgain = false;
        private void txtitemname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (lstbox.SelectedItems.Count > 0)
                {
                    txtitemname.Text = lstbox.SelectedItem.ToString();
                }
                DataTable dtItemChk = new DataTable();
                dtItemChk.Rows.Clear();
                SqlCommand cmdItemChk = new SqlCommand("Select * from Item_table where item_name=@tItemName", con);
                cmdItemChk.Parameters.AddWithValue("@tItemName", txtitemname.Text.Trim());
                SqlDataAdapter adpItemChk = new SqlDataAdapter(cmdItemChk);
                adpItemChk.Fill(dtItemChk);
                if (dtItemChk.Rows.Count > 0)
                {
                    if (isChkAgain == false)
                    {
                        nametonumber();
                    }
                    cmbtype.Focus();
                }
                else
                {
                    MyMessageBox.ShowBox("Item name not found in the list", "Warning");
                    // txtitemname.Select();
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtitemcode_Enter(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        bool isChk;
        string chk;
        private void txtitemname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                panel2.Visible = true;
                if (text != "1")
                {
                    con.Close();
                    con.Open();
                    DataTable dtitemsearch = new DataTable();
                    SqlCommand cmd = new SqlCommand("select * from item_table where item_name like @tItemName", con);
                    cmd.Parameters.AddWithValue("@tItemName", txtitemname.Text.Trim() + "%");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dtitemsearch.Rows.Clear();
                    adp.Fill(dtitemsearch);
                    isChk = false;
                    if (dtitemsearch.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dtitemsearch.Rows[0]["item_name"].ToString();
                        for (int k = 0; k < lstbox.Items.Count; k++)
                        {
                            if (tempstr == lstbox.Items[k].ToString())
                            {
                                lstbox.SetSelected(k, true);
                                txtitemname.Select();
                                chk = "1";
                                txtitemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {

                        chk = "2";
                        if (txtitemname.Text != "")
                        {
                            string name = txtitemname.Text.Remove(txtitemname.Text.Length - 1);
                            txtitemname.Text = name.ToString();
                            txtitemname.Select(txtitemname.Text.Length, 0);
                        }
                        txtitemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "warning");
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
        string EnterIn = "";
        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtitemname.Text = "";
            //txtitemcode.Text = "";
            //txtsalesqty.Text = "";
            //txtsalesrate.Text = "";
            try
            {
                if (EnterIn != "1")
                {
                    if (cmbtype.SelectedIndex == 0)
                    {
                        txtsalesrate.Enabled = true;
                        dgFreeItemList.DataSource = null;
                        dgFreeItemList.Enabled = false;
                        txtsalesrate.Text = "";
                        txtsalesqtyFrom.Text = string.Empty;
                        txtSalesQtyTo.Text = string.Empty;
                        txtsalesqtyFrom_TextChanged(sender, e);
                        gridItemName.Rows.Clear();
                        if (gridItemName.Rows.Count == 0)
                        {
                            gridItemName.Rows.Add();
                        }
                    }
                    else if (cmbtype.SelectedIndex == 1 && cmbItemType.Text == "Single")
                    {
                        txtsalesrate.Text = "0.00";
                        txtsalesrate.Enabled = false;
                        dgFreeItemList.Enabled = true;
                        dtLoad.Rows.Clear();
                        //dgFreeItemList.DataSource = dtLoad;
                        gridItemName.Columns["Rate"].Visible = true;
                        txtsalesqtyFrom.Text = string.Empty;
                        txtSalesQtyTo.Text = string.Empty;
                        gridItemName.Rows.Clear();
                        if (gridItemName.Rows.Count == 0)
                        {
                            gridItemName.Rows.Add();
                        }
                        //dataGridView1.Columns[0].Width = 150;
                        //dataGridView1.Columns[1].Width = 300;
                        //dataGridView1.Columns[2].Width = 150;
                        //dataGridView1.Columns[3].Width = 150;
                        //dataGridView1.Columns[4].Width = 150;
                        if (dtLoad.Rows.Count == 0)
                        {
                            dtLoad.Rows.Add();
                        }
                        dgFreeItemList.DataSource = dtLoad;
                        dgFreeItemList.Columns["Rate"].Visible = false;
                        dgFreeItemList.Columns["Stock"].Visible = false;
                        lbltotSalesQtyto.Visible = false;
                        txtSalesQtyTo.Visible = false;

                    }
                    else if (cmbItemType.Text == "Different")
                    {
                        txtsalesrate.Enabled = true;
                        dgFreeItemList.DataSource = null;
                        dgFreeItemList.Enabled = false;
                        txtsalesrate.Text = "";
                        txtsalesqtyFrom_TextChanged(sender, e);
                        txtsalesqtyFrom.Text = string.Empty;
                        txtSalesQtyTo.Text = string.Empty;
                        gridItemName.Rows.Clear();
                        if (gridItemName.Rows.Count == 0)
                        {
                            gridItemName.Rows.Add();
                        }
                        lbltotSalesQtyto.Visible = true;
                        txtSalesQtyTo.Visible = true;
                    }
                    else
                    {
                        txtsalesrate.Text = "0.00";
                        txtsalesrate.Enabled = false;
                        dgFreeItemList.Enabled = true;
                        dtLoad.Rows.Clear();
                        dgFreeItemList.DataSource = dtLoad;
                        gridItemName.Rows.Clear();
                        if (gridItemName.Rows.Count == 0)
                        {
                            gridItemName.Rows.Add();
                        }
                        if (dtLoad.Rows.Count == 0)
                        {
                            dtLoad.Rows.Add();
                            dgFreeItemList.DataSource = dtLoad;
                        }

                        dgFreeItemList.Columns["Rate"].Visible = false;
                        dgFreeItemList.Columns["Stock"].Visible = false;
                        lbltotSalesQtyto.Visible = false;
                        txtSalesQtyTo.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                dptodate.Focus();
            }
        }
        private void dptodate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //TStpDeleteFreeItemRow.Focus();
                cmbItemType.Select();
            }
        }
        private void txtActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtActive.Text.Trim() == "ACTIVE")
                {
                    txtActive.Text = "NO";
                }
                else
                {
                    txtActive.Text = "ACTIVE";
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (cmbtype.Text == "Price")
                {
                    txtsalesrate.Focus();
                }
                else
                {
                    gridItemName.Focus();
                }
            }
        }
        private void txtSalesQtyTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtsalesrate.Focus();
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ie = Convert.ToString(e.RowIndex.ToString());
                // DgPurchase.CurrentRow.Cells["Sno"].Value = (Convert.ToInt32(ie) + 1).ToString();
                //  if (enter_emptystring == "1")
                {
                    if (dgFreeItemList.Rows[e.RowIndex].Cells["ItemNames"].Value == null && dgFreeItemList.Rows[e.RowIndex].Cells["ItemCode"].Value == null)
                    {
                        if (dgFreeItemList.CurrentCell.ColumnIndex > 2)
                        {
                            if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value == null)
                            {
                                if (dgFreeItemList.Rows.Count > 1)
                                {
                                    var selected = dgFreeItemList.SelectedCells;
                                    for (int x = 0; x < selected.Count; )
                                    {
                                        dgFreeItemList.ClearSelection();
                                        MyMessageBox1.ShowBox("Please Enter Item Code Or Item Name", "Warning");

                                        break;
                                    }
                                    btnSave.Focus();
                                }
                            }
                        }
                    }
                }
                if (dgFreeItemList.CurrentCell.ColumnIndex == 2)
                {
                    if (!string.IsNullOrEmpty(dgFreeItemList.Rows[e.RowIndex].Cells["ItemNames"].Value.ToString()) && dgFreeItemList.Rows[e.RowIndex].Cells["ItemNames"].Value != null)
                    {
                        if (cmbItemType.Text.Equals("Single") && (cmbtype.Text == "Same Free"))
                        {
                            DataView dataView = new DataView(dtLoad);
                            dgFreeItemList.AllowUserToAddRows = false;
                            //dgFreeItemList.DataSource = dataView;
                        }
                        else
                        {
                            DataView dataView = new DataView(dtLoad);
                            dgFreeItemList.AllowUserToAddRows = true;
                            // dgFreeItemList.DataSource = dataView;
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
            try
            {
                if (this.dgFreeItemList.CurrentCell.ColumnIndex != this.dgFreeItemList.Columns.Count - 1)
                {
                    int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                    SetColumnIndex method = new SetColumnIndex(Mymethod1);
                    this.dgFreeItemList.BeginInvoke(method, nextindex);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "warning");
            }

        }

        public void nextcell1()
        {
            try
            {
                if (this.gridItemName.CurrentCell.ColumnIndex != this.gridItemName.Columns.Count - 1)
                {
                    int nextindex = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 1);
                    SetColumnIndex method1 = new SetColumnIndex(Mymethod1);
                    this.gridItemName.BeginInvoke(method1, nextindex);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "warning");
            }

        }

        public void Mymethod1(int columnIndex)
        {
            if (items_alter != "0")
            {
                if (items_alter != "1")
                {
                    this.gridItemName.CurrentCell = this.gridItemName.CurrentRow.Cells[columnIndex];
                    this.gridItemName.BeginEdit(true);
                }
                else
                {
                    this.gridItemName.CurrentCell = this.gridItemName.CurrentRow.Cells[columnIndex - 1];
                    this.gridItemName.BeginEdit(true);
                }
            }
            else
            {
                this.gridItemName.CurrentCell = this.gridItemName.CurrentRow.Cells[2];
                this.gridItemName.BeginEdit(true);
                items_alter = "1";
            }

        }
        string items_alter = "0";
        public delegate void SetColumnIndex(int i);
        public void Mymethod(int columnIndex)
        {
            if (items_alter != "0")
            {
                this.dgFreeItemList.CurrentCell = this.dgFreeItemList.CurrentRow.Cells[columnIndex];
                this.dgFreeItemList.BeginEdit(true);
            }
            else
            {
                this.dgFreeItemList.CurrentCell = this.dgFreeItemList.CurrentRow.Cells[2];
                this.dgFreeItemList.BeginEdit(true);
                items_alter = "1";
            }

        }
        //public void Mymethod1(int columnIndex)
        //{
        //    if (items_alter != "0")
        //    {
        //        this.dgFreeItemList.CurrentCell = this.dgFreeItemList.CurrentRow.Cells[columnIndex];
        //        this.gridItemName.BeginEdit(true);
        //    }
        //    else
        //    {
        //        this.dgFreeItemList.CurrentCell = this.dgFreeItemList.CurrentRow.Cells[2];
        //        this.dgFreeItemList.BeginEdit(true);
        //        items_alter = "1";
        //    }

        //}

        string enter_emptystring = "";
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value != null && !string.IsNullOrEmpty(dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value.ToString()))
                    {
                        ItemcodeorItemName(dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value.ToString());
                        // gridFreeItemChk();
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value != null && !string.IsNullOrEmpty(dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString()))
                    {
                        ItemcodeorItemName(dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString());
                        gridFreeItemChk1();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void gridFreeItemChk()
        {
            SqlCommand cmd = null;
            if ((cmbItemType.Text == "Single" && cmbtype.Text == "Same Free") || (cmbItemType.Text == "Single" && cmbtype.Text == "Different Free"))
            {
                cmd = new SqlCommand("select * from [FreeItemMasterSingleDifferentView] where FreeItemMasterSingleDifferentView.Active=1 And FreeItemMasterSingleDifferentView.FromDate<=@tFromDate and FreeItemMasterSingleDifferentView.FromDate>=@tToDate", con);
                cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                cmd.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtChkduplicate = new DataTable();
                dtChkduplicate.Rows.Clear();
                adp.Fill(dtChkduplicate);
                if (dtChkduplicate.Rows.Count > 0)
                {
                    for (int j = 0; j < dtChkduplicate.Rows.Count; j++)
                    {
                        // if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value != null)
                        {
                            if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString() == dtChkduplicate.Rows[j]["Item_name"].ToString())
                            {
                                string chSameFree = "", chkSameFreeVal = "";
                                chkSameFreeVal = dtChkduplicate.Rows[j]["FreeType"].ToString();
                                if (cmbtype.Text == "Same Free" && chkSameFreeVal == "Same Free")
                                {
                                    chSameFree = "1";
                                }
                                if (chSameFree != "1")
                                {
                                    MyMessageBox.ShowBox("This Item Already Comes Under Free ItemList", "Warning");
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                                    int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                                    this.gridItemName.BeginInvoke(method1, 2 - 1);
                                    goto End;
                                }
                            }
                        }
                    }
                }
                if ((cmbItemType.Text == "Single" && cmbtype.Text == "Same Free"))
                {
                    for (int i = 0; i < gridItemName.Rows.Count; i++)
                    {
                        for (int k = 0; k < dgFreeItemList.Rows.Count; k++)
                        {
                            if (dgFreeItemList.Rows[k].Cells["ItemCode"].Value != null && dgFreeItemList.Rows[i].Cells["ItemCode"].Value != null)
                            {
                                if (dgFreeItemList.Rows[k].Cells["ItemCode"].Value.ToString() != gridItemName.Rows[i].Cells["ItemCode"].Value.ToString())
                                {
                                    MyMessageBox.ShowBox("Please Enter The Same Item Name", "Warning");
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = "";
                                    int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                                    this.gridItemName.BeginInvoke(method1, 2 - 1);
                                    goto End;
                                }
                            }
                        }
                    }
                }
                if ((cmbItemType.Text == "Single" && cmbtype.Text == "Different Free"))
                {
                    for (int i = 0; i < gridItemName.Rows.Count; i++)
                    {
                        for (int k = 0; k < dgFreeItemList.Rows.Count; k++)
                        {
                            if (dgFreeItemList.Rows[k].Cells["ItemCode"].Value != null && dgFreeItemList.Rows[i].Cells["ItemCode"].Value != null)
                            {
                                if (dgFreeItemList.Rows[k].Cells["ItemCode"].Value.ToString() == gridItemName.Rows[i].Cells["ItemCode"].Value.ToString())
                                {
                                    MyMessageBox.ShowBox("Please Enter The Same Item Name", "Warning");
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = "";
                                    int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                                    this.gridItemName.BeginInvoke(method1, 2 - 1);
                                    goto End;
                                }
                            }
                        }
                    }
                }
            End:
                int ij = 0;
            }
        }
        public void gridFreeItemChk1()
        {
            SqlCommand cmd = null;
            if ((cmbItemType.Text == "Single" && cmbtype.Text == "Same Free") || (cmbItemType.Text == "Single" && cmbtype.Text == "Different Free"))
            {
                cmd = new SqlCommand("select * from [FreeItemMasterSingleDifferentView] where FreeItemMasterSingleDifferentView.Active=1 And FreeItemMasterSingleDifferentView.FromDate<=@tFromDate and FreeItemMasterSingleDifferentView.FromDate>=@tToDate", con);
                cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                cmd.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtChkduplicate = new DataTable();
                dtChkduplicate.Rows.Clear();
                adp.Fill(dtChkduplicate);
                if (dtChkduplicate.Rows.Count > 0)
                {
                    for (int j = 0; j < dtChkduplicate.Rows.Count; j++)
                    {
                        // if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value != null)
                        {
                            string samename = "";
                            samename = dtChkduplicate.Rows[j]["FreeType"].ToString();
                            if (dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString() == dtChkduplicate.Rows[j]["Item_name"].ToString() && samename != cmbtype.Text)
                            {
                                MyMessageBox.ShowBox("This Item Already Comes Under Free ItemList", "Warning");

                                dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";
                                dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = "";
                                int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                                SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                                this.gridItemName.BeginInvoke(method1, 1);
                                goto End;
                            }
                        }
                    }
                }
                if ((cmbItemType.Text == "Single" && cmbtype.Text == "Same Free"))
                {
                    for (int i = 0; i < gridItemName.Rows.Count; i++)
                    {
                        for (int k = 0; k < dgFreeItemList.Rows.Count; k++)
                        {
                            if (dgFreeItemList.Rows[k].Cells["ItemNames"].Value != null && dgFreeItemList.Rows[i].Cells["ItemNames"].Value != null)
                            {
                                if (dgFreeItemList.Rows[k].Cells["ItemNames"].Value.ToString() != gridItemName.Rows[i].Cells["ItemNames"].Value.ToString())
                                {
                                    MyMessageBox.ShowBox("Please Enter The Same Item Name", "Warning");
                                    // dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = "";
                                    int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                                    this.gridItemName.BeginInvoke(method1, 2 - 1);
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;

                                    goto End;
                                }
                            }
                        }
                    }

                }
                if ((cmbItemType.Text == "Single" && cmbtype.Text == "Different Free"))
                {
                    for (int i = 0; i < gridItemName.Rows.Count; i++)
                    {
                        for (int k = 0; k < dgFreeItemList.Rows.Count; k++)
                        {
                            if (dgFreeItemList.Rows[k].Cells["ItemNames"].Value != null && dgFreeItemList.Rows[i].Cells["ItemNames"].Value != null)
                            {
                                if (dgFreeItemList.Rows[k].Cells["ItemNames"].Value.ToString() == gridItemName.Rows[i].Cells["ItemNames"].Value.ToString())
                                {
                                    MyMessageBox.ShowBox("Please Enter The Different Item Name", "Warning");
                                    int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                                    this.gridItemName.BeginInvoke(method1, 2 - 1);
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                                    dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                    goto End;
                                }
                            }
                        }
                    }
                }
            End:
                int jk = 0;
            }
        }
        DataTable dt_items = new DataTable();
        public void ItemcodeorItemName(string itemNamecode)
        {
            try
            {
                if (!string.IsNullOrEmpty(itemNamecode))
                {
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
                            dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = dt_items.Rows[0]["Item_code"].ToString();
                        }
                        else
                        {
                            dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";
                        }
                        dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = dt_items.Rows[0]["Item_name"].ToString();
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Item Code Or Item Name Not Fount", "Warning");
                        int nextindex = Math.Min(this.dgFreeItemList.Columns.Count - 1, this.dgFreeItemList.CurrentCell.ColumnIndex + 1);
                        SetColumnIndex method1 = new SetColumnIndex(Mymethod);
                        dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                        dgFreeItemList.Rows[dgFreeItemList.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                        this.gridItemName.BeginInvoke(method1, 2 - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void gridDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgFreeItemList.CurrentCell.ColumnIndex == 2 || dgFreeItemList.CurrentCell.ColumnIndex == 3 || dgFreeItemList.CurrentCell.ColumnIndex == 4)
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

        private void textbox_TextChanged(object sender, EventArgs e)
        {

        }
        private void textbox1_TextChanged(object sender, EventArgs e)
        {


        }

        //System.Windows.Forms.Control cntObject;
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                con.Close();
                con.Open();
                SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table order by Item_name ASC", con);
                DataTable autofind = new DataTable();
                autofind.Rows.Clear();
                SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
                nameadp.Fill(autofind);
                con.Close();

                if (this.dgFreeItemList.CurrentCell.ColumnIndex == this.dgFreeItemList.Columns["ItemNames"].Index) //Item_name
                {
                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }


                if (this.dgFreeItemList.CurrentCell.ColumnIndex == this.dgFreeItemList.Columns["ItemCode"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.dgFreeItemList.CurrentCell.ColumnIndex == this.dgFreeItemList.Columns["ItemNames"].Index) //Item_name
                {
                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

                if (this.dgFreeItemList.CurrentCell.ColumnIndex == this.dgFreeItemList.Columns["Free_Qty"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.dgFreeItemList.CurrentCell.ColumnIndex == this.dgFreeItemList.Columns["Rate"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.dgFreeItemList.CurrentCell.ColumnIndex == this.dgFreeItemList.Columns["Stock"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        public bool funValidation()
        {
            if (string.IsNullOrEmpty(TStpDeleteFreeItemRow.Text))
            {
                MyMessageBox.ShowBox("Enter Offer name", "Warning");
                TStpDeleteFreeItemRow.Select();
                return false;
            }
            else if (string.IsNullOrEmpty(txtsalesqtyFrom.Text))
            {
                MyMessageBox.ShowBox("Enter Sales Qty", "Warning");
                txtsalesqtyFrom.Select();
                return false;
            }
            else if (string.IsNullOrEmpty(txtSalesQtyTo.Text))
            {

                MyMessageBox.ShowBox("Enter Sales Qty", "Warning");
                txtSalesQtyTo.Select();
                return false;
            }
            else if (string.IsNullOrEmpty(txtsalesrate.Text))
            {
                MyMessageBox.ShowBox("Enter Sales Rate", "Warning");
                txtsalesrate.Select();
                return false;
            }
            else if ((cmbtype.Text == "Free"))
            {
                if (dgFreeItemList.Rows.Count > 1)
                {
                    return true;
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Free Quantity", "Warning");
                    return false;
                }
            }
            else if (itemnameexit != TStpDeleteFreeItemRow.Text)
            {
                //string Vchk = "1";
                con.Close();
                con.Open();
                DataTable dtItemChk = new DataTable();
                dtItemChk.Rows.Clear();
                SqlCommand cmdItemChk = new SqlCommand("Select OfferName from FreeItemMaster_table where Offername=@tItemName", con);
                cmdItemChk.Parameters.AddWithValue("@tItemName", TStpDeleteFreeItemRow.Text.Trim());
                SqlDataAdapter adpItemChk = new SqlDataAdapter(cmdItemChk);
                adpItemChk.Fill(dtItemChk);
                if (dtItemChk.Rows.Count > 0)
                {
                    MyMessageBox.ShowBox("Offer Name Already Exist", "Warning");
                    TStpDeleteFreeItemRow.Select();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        public void Clear()
        {
            if (Chekval == "Yes")
            {
                dtLoad1.Rows.Clear();
                dt.Rows.Clear();
                RetrunNo = "1";
                //gridItemName.DataSource = null;
                gridItemName.Rows.Clear();
                dgFreeItemList.DataSource = null;
                //  dtLoad1.Rows.Add();
                if (gridItemName.Rows.Count == 0)
                {
                    gridItemName.Rows.Add();
                }
                // dtLoad1.Rows.Add("", "", "", "", "");
                // gridItemName.DataSource = dtLoad1;
                RetrunNo = "";
                cmbItemType.Text = "Single";
                cmbtype.Text = "Price";
                txtActive.Text = "ACTIVE";
                TStpDeleteFreeItemRow.Text = "";
                txtsalesqtyFrom.Text = "";
                txtsalesrate.Enabled = true;
                txtSalesQtyTo.Text = "";
                txtsalesrate.Text = "";
                //  gridItemName.Columns["Rate"].Visible = false;
                //  gridItemName.Columns["Amt"].Visible = false;
                if (cmbtype.Text.Equals("Same Free") || cmbtype.Text.Equals("Different Free"))
                {
                    if (dgFreeItemList.Rows.Count == 0)
                    {
                        dgFreeItemList.Rows.Add();
                    }
                }
            }
        }
        string text = "";
        string Chekval = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (funValidation())
                {
                    //    if (gridItemName.Rows.Count > 0)
                    //    {
                    //        string item_nameslist = "";
                    //        item_nameslist = Convert.ToString(gridItemName.Rows[0].Cells[1].Value.ToString() == null || gridItemName.Rows[0].Cells[1].Value.ToString() == "" ? "" : gridItemName.Rows[0].Cells[1].Value.ToString());
                    //        if (!string.IsNullOrEmpty(item_nameslist))
                    //        {
                    //            if (gridItemName.Rows.Count > 0)
                    //            {
                    //                GridQtyCount();
                    //            }

                    //            if (TotQty == Convert.ToDouble(txtsalesqtyFrom.Text.Trim()))
                    //            {
                    //                Dictionary<string, double> dicSum1 = new Dictionary<string, double>();
                    //                foreach (DataRow row in dtLoad1.Rows)
                    //                {
                    //                    string group = row["ItemNames"].ToString();
                    //                    double Qty = (string.IsNullOrEmpty(Convert.ToString(row["Qty"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Qty"]));
                    //                    if (dicSum1.ContainsKey(group))
                    //                        dicSum1[group] += Qty;
                    //                    else
                    //                        dicSum1.Add(group, Qty);
                    //                }
                    //                dtLoad1.Rows.Clear();
                    //                string tMainItem = "";
                    //                foreach (string g in dicSum1.Keys)
                    //                {
                    //                    tMainItem += "'" + g + "',";
                    //                    dtLoad1.Rows.Add("", g, dicSum1[g], "0", "0");
                    //                }
                    //                tMainItem = tMainItem.TrimEnd(',');
                    //                Dictionary<string, double> dicSum = new Dictionary<string, double>();
                    //                dt.Rows.Clear();
                    //                for (int i = 0; i < dgFreeItemList.Rows.Count; i++)
                    //                {
                    //                    if (!string.IsNullOrEmpty(Convert.ToString(dgFreeItemList.Rows[i].Cells["ItemNames"].Value)))
                    //                    {
                    //                        dt.Rows.Add(Convert.ToString(dgFreeItemList.Rows[i].Cells["ItemNames"].Value), string.IsNullOrEmpty(Convert.ToString(dgFreeItemList.Rows[i].Cells["Free_Qty"].Value)) == true ? "1" : Convert.ToString(dgFreeItemList.Rows[i].Cells["Free_Qty"].Value), (string.IsNullOrEmpty(Convert.ToString(dgFreeItemList.Rows[i].Cells["Rate"].Value)) == true) ? "0.00" : Convert.ToString(dgFreeItemList.Rows[i].Cells["Rate"].Value), string.IsNullOrEmpty(Convert.ToString(dgFreeItemList.Rows[i].Cells["Stock"].Value)) == true ? "0" : Convert.ToString(dgFreeItemList.Rows[i].Cells["Stock"].Value), (txtActive.Text.Equals("ACTIVE") ? 1 : 0));
                    //                    }
                    //                }
                    //                foreach (DataRow row in dt.Rows)
                    //                {
                    //                    string group = row["ItemNames"].ToString();
                    //                    double Qty = (string.IsNullOrEmpty(Convert.ToString(row["Qty"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Qty"]));
                    //                    //double Rate = (string.IsNullOrEmpty(Convert.ToString(row["Rate"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Rate"]));
                    //                    //double Stock = (string.IsNullOrEmpty(Convert.ToString(row["Stock"])) == true) ? 1 : Convert.ToDouble(Convert.ToString(row["Stock"]));
                    //                    if (dicSum.ContainsKey(group))
                    //                    {
                    //                        dicSum[group] += Qty;
                    //                        //dicSum[group] = Rate;
                    //                        //dicSum[group] += Stock;
                    //                    }
                    //                    else
                    //                        dicSum.Add(group, Qty);
                    //                }
                    //                dt.Rows.Clear();
                    //                foreach (string g in dicSum.Keys)
                    //                {
                    //                    dt.Rows.Add("", g, dicSum[g], "0.00", "0");
                    //                }
                    //                char[] arr = new char[] { '\'' };
                    //                //Trim Start and end:
                    //                string orginal = tMainItem.TrimStart(arr);
                    //                string endstring = orginal.TrimEnd(arr);

                    //                if (con.State != ConnectionState.Open)
                    //                {
                    //                    con.Open();
                    //                }
                    //                SqlCommand cmd_selectno = new SqlCommand("Select item_no from item_table where item_name in (@ItemName)", con);
                    //                cmd_selectno.Parameters.AddWithValue("@ItemName", endstring.ToString());
                    //                SqlDataAdapter adpselectno = new SqlDataAdapter(cmd_selectno);
                    //                DataTable dtChk = new DataTable();
                    //                string Itemno_ = "";
                    //                DataTable dtselectno = new DataTable();
                    //                dtselectno.Rows.Clear();
                    //                adpselectno.Fill(dtselectno);
                    //                if (dtselectno.Rows.Count > 0)
                    //                {
                    //                    Itemno_ = dtselectno.Rows[0][0].ToString();
                    //                }
                    //                bool isChk = false;
                    //                if (txtActive.Text.Trim().ToUpper() == "ACTIVE")
                    //                {
                    //                    SqlCommand cmdChk = new SqlCommand("Select FreeSnoGroup from FreeItemMaster_table where  Active=1 and   FromDate<=@tFromDate and Todate>=@tToDate and Item_no='" + Itemno_.ToString().Trim() + "'", con);
                    //                    cmdChk.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                    //                    cmdChk.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                    //                    SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
                    //                    dtChk.Rows.Clear();
                    //                    adpChk.Fill(dtChk);

                    //                    Update = ""; Chekval = "";
                    //                    if (dtChk.Rows.Count > 0)
                    //                    {
                    //                        string tRes = MyMessageBox1.ShowBox("Itemname already available in Offer list. do you want to modify existing offer?", "Warning");
                    //                        if (tRes == "1")
                    //                        {
                    //                            isChk = true;
                    //                            Update = "Yes";

                    //                            if (Update == "Yes")
                    //                            {
                    //                                SqlCommand cmdUpdateItem = new SqlCommand("Update FreeItemMaster_table Set Active=0 where item_no='" + Itemno_.ToString() + "' and  offername<>@Offername and  FromDate<=@tFromDate and Todate>=@tToDate", con);
                    //                                cmdUpdateItem.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                    //                                cmdUpdateItem.Parameters.AddWithValue("@Offername", txtitemcode.Text.ToString().Trim());
                    //                                cmdUpdateItem.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                    //                                cmdUpdateItem.ExecuteNonQuery();

                    //                                // SqlCommand cmdUpdateFreeItem = new SqlCommand("update FreeItemDetail_table set FreeItemDetail_table.Active=0 from freeItemMaster_table join FreeItemDetail_table on freeItemMaster_table.Active<> FreeItemDetail_table.Active where freeItemMaster_table.Active=0 and freeItemMaster_table.FreeType='Free,'", con);
                    //                                SqlCommand cmdUpdateFreeItem = new SqlCommand("UPDATE freeItemDetail_table SET ACTIVE=0 WHERE FREESNO=(SELECT FREESNOGROUP FROM freeItemmaster_table WHERE ACTIVE=0 AND  FreeType='Free' )", con);
                    //                                cmdUpdateFreeItem.ExecuteNonQuery();
                    //                                //SqlDataAdapter cmdSelectFreeItem = new SqlDataAdapter("Select FreeSnoGroup from FreeITemMaster Where Active=0", con);
                    //                                //DataTable dtFreeItem = new DataTable();
                    //                                //dtFreeItem.Rows.Clear();
                    //                                //cmdSelectFreeItem.Fill(dtFreeItem);
                    //                                //if (dtFreeItem.Rows.Count > 0)
                    //                                //{
                    //                                //    for (int i = 0; i < dtFreeItem.Rows.Count; i++)
                    //                                //    {
                    //                                //        SqlCommand cmdUpdateDetail = new SqlCommand("Update FreeItemDetail_table Active=0 where FreeSno='" + dtFreeItem.Rows[i]["FreeSnoGroup"].ToString() + "'", con);
                    //                                //        cmdUpdateDetail.ExecuteNonQuery();
                    //                                //    }
                    //                                //}
                    //                            }
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        isChk = true;
                    //                        Chekval = "Yes";
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    isChk = true;
                    //                    Chekval = "Yes";
                    //                }
                    //                if (isChk == true)
                    //                {
                    //                    if (con.State != ConnectionState.Open)
                    //                    {
                    //                        con.Open();
                    //                    }
                    //                    if (lbl_stckbanner.Text == "Special Offer Alteration")
                    //                    {
                    //                        if (!string.IsNullOrEmpty(tFreeSno))
                    //                        {
                    //                            if (con.State != ConnectionState.Open)
                    //                            {
                    //                                con.Open();
                    //                            }
                    //                            SqlCommand cmdDelete = new SqlCommand("Delete  from FreeItemDetail_table where FreeSno=@tFreeSno", con);
                    //                            cmdDelete.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                    //                            cmdDelete.ExecuteNonQuery();

                    //                            SqlCommand cmdDeleteMaster = new SqlCommand("delete from FreeItemMaster_table where FreeSnoGroup=@tFreeSno", con);
                    //                            cmdDeleteMaster.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                    //                            cmdDeleteMaster.ExecuteNonQuery();
                    //                        }
                    //                    }
                    //                    SqlCommand cmd = new SqlCommand("sp_FreeItemInsertNew", con);
                    //                    cmd.CommandType = CommandType.StoredProcedure;
                    //                    // cmd.Parameters.AddWithValue("@tFreeSnoNew", tFreeSno);
                    //                    cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value);
                    //                    cmd.Parameters.AddWithValue("@tToDate", dptodate.Value);
                    //                    cmd.Parameters.AddWithValue("@tOfferName", txtitemcode.Text);
                    //                    cmd.Parameters.AddWithValue("@tItemType", cmbItemType.Text);
                    //                    cmd.Parameters.AddWithValue("@tOfferType", cmbtype.Text);
                    //                    cmd.Parameters.AddWithValue("@tTotSaleQty", (string.IsNullOrEmpty(txtsalesqtyFrom.Text) == true) ? "0" : txtsalesqtyFrom.Text);
                    //                    cmd.Parameters.AddWithValue("@tTotSaleRate", (string.IsNullOrEmpty(txtsalesrate.Text) == true) ? "0.00" : txtsalesrate.Text);
                    //                    cmd.Parameters.AddWithValue("@tFreeTable", dt);
                    //                    cmd.Parameters.AddWithValue("@tActive", (txtActive.Text.ToString().Trim().ToUpper() == ("ACTIVE") ? 1 : 0));
                    //                    cmd.Parameters.AddWithValue("@tItemTable", dtLoad1);
                    //                    cmd.Parameters.AddWithValue("@Sunday", Chksunday.Checked == true ? 1 : 0);
                    //                    cmd.Parameters.AddWithValue("@Monday", ChkMonday.Checked == true ? 1 : 0);
                    //                    cmd.Parameters.AddWithValue("@Tuesday", ChkTuesday.Checked == true ? 1 : 0);
                    //                    cmd.Parameters.AddWithValue("@Wednesday", ChkWednesDay.Checked == true ? 1: 0);
                    //                    cmd.Parameters.AddWithValue("@Thursday", Chkthursday.Checked == true ? 1 : 0);
                    //                    cmd.Parameters.AddWithValue("@Friday", ChkFriday.Checked == true ? 1 : 0);
                    //                    cmd.Parameters.AddWithValue("@Sturday", ChkSturday.Checked == true ? 1 : 0);

                    //                    string tPath = "";
                    //                    if (!string.IsNullOrEmpty(FileName))
                    //                    {
                    //                        conv_photo();
                    //                        if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\OfferImage"))
                    //                        {
                    //                            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\OfferImage");
                    //                        }
                    //                        tPath = System.Windows.Forms.Application.StartupPath + "\\OfferImage\\" + txtitemname.Text.Trim() + ".jpeg";
                    //                        if (!File.Exists(tPath))
                    //                        {
                    //                            System.IO.File.Copy(FileName, tPath);
                    //                        }
                    //                        else
                    //                        {
                    //                            GC.Collect();
                    //                            System.IO.File.Delete(tPath);
                    //                            GC.Collect();
                    //                            System.IO.File.Copy(FileName, tPath);
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        //  tPath = ItemImage.ToString();
                    //                    }
                    //                    cmd.Parameters.AddWithValue("@ItemImage", tPath.ToString() == "" ? "" : "\\OfferImage\\" + txtitemname.Text.Trim() + ".jpeg");
                    //                    cmd.ExecuteNonQuery();
                    //                    Chekval = "Yes";

                    //                }
                    //                Clear();
                    //                if (lbl_stckbanner.Text == "Special Offer Alteration")
                    //                {
                    //                    this.Close();
                    //                    SalesCreationEventHandlerNew(sender, e);                                     
                    //                }
                    //            }
                    //            else
                    //            {
                    //                MyMessageBox.ShowBox("Please Enter Valuable Quantity", "Warning");
                    //                txtsalesqtyFrom.Select();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MyMessageBox.ShowBox("Please Enter Valid Item", "Warning");
                    //        }
                    //    }
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    dt.Columns.Add("ItemCode");
                    dt.Columns.Add("ItemName");
                    dt.Columns.Add("Qty");
                    DataTable dtFReeItem = new DataTable();
                    dtFReeItem.Columns.Add("ItemCode");
                    dtFReeItem.Columns.Add("ItemName");
                    dtFReeItem.Columns.Add("Qty");
                    dtFReeItem.Rows.Clear();
                    //Orginal Grid Item Name
                    for (int i = 0; i < gridItemName.Rows.Count; i++)
                    {
                        if (gridItemName.Rows[i].Cells["ItemNames"].Value != null && (!string.IsNullOrEmpty(gridItemName.Rows[i].Cells["ItemNames"].Value.ToString())) && gridItemName.Rows[i].Cells["Qty1"].Value != null && !string.IsNullOrEmpty(gridItemName.Rows[i].Cells["Qty1"].Value.ToString()))
                        {
                            dt.Rows.Add(gridItemName.Rows[i].Cells["ItemCode"].Value.ToString(), gridItemName.Rows[i].Cells["ItemNames"].Value.ToString(), gridItemName.Rows[i].Cells["Qty1"].Value.ToString());
                        }
                    }
                    //Free Item List
                    {
                        for (int j = 0; j < dgFreeItemList.Rows.Count; j++)
                        {
                            if (dgFreeItemList.Rows[j].Cells["ItemNames"].Value != null && (!string.IsNullOrEmpty(dgFreeItemList.Rows[j].Cells["ItemNames"].Value.ToString())) && dgFreeItemList.Rows[j].Cells["Free_Qty"].Value != null && !string.IsNullOrEmpty(dgFreeItemList.Rows[j].Cells["Free_Qty"].Value.ToString()))
                            {
                                dtFReeItem.Rows.Add(dgFreeItemList.Rows[j].Cells["ItemCode"].Value.ToString(), dgFreeItemList.Rows[j].Cells["ItemNames"].Value.ToString(), dgFreeItemList.Rows[j].Cells["Free_Qty"].Value.ToString());
                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Sp_InsertFreeItem", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value);
                        cmd.Parameters.AddWithValue("@tToDate", dptodate.Value);
                        cmd.Parameters.AddWithValue("@OfferName", TStpDeleteFreeItemRow.Text);
                        cmd.Parameters.AddWithValue("@ItemType", cmbItemType.Text);
                        cmd.Parameters.AddWithValue("@ItemMode", cmbtype.Text);
                        cmd.Parameters.AddWithValue("@ItemTypeMode", cmbtype.Text);
                        cmd.Parameters.AddWithValue("@TotSalesQty", txtsalesqtyFrom.Text);
                        cmd.Parameters.AddWithValue("@SalesQtyTo", txtSalesQtyTo.Text);
                        cmd.Parameters.AddWithValue("@Active", (txtActive.Text.ToString().Trim().ToUpper() == ("ACTIVE") ? 1 : 0));
                        cmd.Parameters.AddWithValue("@OfferPrice", txtsalesrate.Text);
                        cmd.Parameters.AddWithValue("@Sunday", Chksunday.Checked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Monday", ChkMonday.Checked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Tuesday", ChkTuesday.Checked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Wednesday", ChkWednesDay.Checked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Thursday", Chkthursday.Checked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Friday", ChkFriday.Checked == true ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Sturday", ChkSturday.Checked == true ? 1 : 0);


                        if (dt.Rows.Count > 0)
                        {
                            cmd.Parameters.AddWithValue("@dtOferItem", dt);
                        }
                        if (dtFReeItem.Rows.Count > 0)
                        {
                            cmd.Parameters.AddWithValue("@FreeItem", dtFReeItem);
                        }
                        if (cmbtype.Text == "Same Free" || cmbtype.Text == "Different Free")
                        {
                            if (dtFReeItem.Rows.Count > 0)
                            {
                                Chekval = "Yes";
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Please Enter Free Offer Item List", "Warning");
                                Chekval = "No";
                            }
                        }
                        else if (cmbtype.Text == "Price")
                        {
                            Chekval = "Yes";
                        }
                        if (Chekval == "Yes")
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }

                            if (lbl_stckbanner.Text == "Special Offer Alteration")
                            {
                                if (!string.IsNullOrEmpty(tFreeSno))
                                {
                                    SqlCommand cmdDelete = new SqlCommand("Delete  from FreeItemDetail_table where FreeSno=@tFreeSno", con);
                                    cmdDelete.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                                    cmdDelete.ExecuteNonQuery();

                                    SqlCommand cmdDeleteMaster = new SqlCommand("delete from FreeItemMaster_table where FreeSnoGroup=@tFreeSno", con);
                                    cmdDeleteMaster.Parameters.AddWithValue("@tFreeSno", tFreeSno);
                                    cmdDeleteMaster.ExecuteNonQuery();
                                }
                            }
                            cmd.ExecuteNonQuery();
                            Clear();

                            if (lbl_stckbanner.Text == "Special Offer Alteration")
                            {
                               
                                btnExit_Click(sender, e);
                                SalesCreationEventHandlerNew(sender, e);                                     
                            }
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Please Enter Item Name", "Warning");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }

            //try
            //{
            //    con.Close();
            //    con.Open();

            //    DataTable dtItemChk = new DataTable();
            //    dtItemChk.Rows.Clear();
            //    SqlCommand cmdItemChk = new SqlCommand("Select * from Item_table where item_name=@tItemName",con);
            //    cmdItemChk.Parameters.AddWithValue("@tItemName", txtitemname.Text.Trim());
            //    SqlDataAdapter adpItemChk = new SqlDataAdapter(cmdItemChk);
            //    adpItemChk.Fill(dtItemChk);
            //    if (dtItemChk.Rows.Count > 0)
            //    {

            //        if (!string.IsNullOrEmpty(tFreeSno))
            //        {
            //            if (funValidation())
            //            {
            //                DataTable dtChk = new DataTable();
            //                dtChk.Rows.Clear();
            //                SqlCommand cmdChk = new SqlCommand("Select * from FreeItem_table where Active=1 and Item_no=(select item_no from Item_table where Item_name=@tItemName) and FreeSno<>@tFreeSno and FromDate<=@tFromDate and ToDate>=@tToDate", con);
            //                cmdChk.Parameters.AddWithValue("@tItemName", txtitemname.Text);
            //                cmdChk.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
            //                cmdChk.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
            //                cmdChk.Parameters.AddWithValue("@tFreeSno", tFreeSno);
            //                SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
            //                adpChk.Fill(dtChk);
            //                bool isChk = false;
            //                if (dtChk.Rows.Count > 0)
            //                {
            //                    isChk = true;
            //                    // MyMessageBox.ShowBox("Same item offer already exists", "Warning");
            //                }

            //                DataTable dtFreeSno = new DataTable();
            //                dtFreeSno.Rows.Clear();
            //                SqlCommand cmdSelect = new SqlCommand("Select * from stktrn_table where Strn_Cancel=0 and FreeItemNo=@tFreeSno", con);
            //                cmdSelect.Parameters.AddWithValue("@tFreeSno", tFreeSno);
            //                SqlDataAdapter adpFreeSno = new SqlDataAdapter(cmdSelect);
            //                adpFreeSno.Fill(dtFreeSno);
            //                if (dtFreeSno.Rows.Count > 0)
            //                {
            //                    MyMessageBox.ShowBox("This offer could not be update", "Warning");
            //                }
            //                else
            //                {

            //                    dt.Rows.Clear();
            //                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //                    {
            //                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["ItemNames"].Value)))
            //                        {
            //                            dt.Rows.Add(Convert.ToString(dataGridView1.Rows[i].Cells["ItemNames"].Value), string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["Free_Qty"].Value)) == true ? "1" : Convert.ToString(dataGridView1.Rows[i].Cells["Free_Qty"].Value), (string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["Rate"].Value)) == true) ? "0.00" : Convert.ToString(dataGridView1.Rows[i].Cells["Rate"].Value), string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["Stock"].Value)) == true ? "0" : Convert.ToString(dataGridView1.Rows[i].Cells["Stock"].Value), (txtActive.Text.Equals("ACTIVE") ? 1 : 0));
            //                        }
            //                    }
            //                    SqlCommand cmd = new SqlCommand("sp_FreeItemUpdate", con);
            //                    cmd.Parameters.AddWithValue("@tFreeSnoNew", tFreeSno);
            //                    cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value);
            //                    cmd.Parameters.AddWithValue("@tToDate", dptodate.Value);
            //                    cmd.Parameters.AddWithValue("@tItemName", txtitemname.Text);
            //                    cmd.Parameters.AddWithValue("@tOfferType", cmbtype.Text);
            //                    cmd.Parameters.AddWithValue("@tSaleQtyFrom", (string.IsNullOrEmpty(txtsalesqtyFrom.Text) == true) ? "0" : txtsalesqtyFrom.Text);
            //                    cmd.Parameters.AddWithValue("@tSaleQtyTo", (string.IsNullOrEmpty(txtSalesQtyTo.Text) == true) ? "0" : txtSalesQtyTo.Text);
            //                    cmd.Parameters.AddWithValue("@tSalesRate", (string.IsNullOrEmpty(txtsalesrate.Text) == true) ? "0.00" : txtsalesrate.Text);
            //                    cmd.Parameters.AddWithValue("@tActive", (txtActive.Text.Equals("ACTIVE") ? 1 : 0));
            //                    cmd.Parameters.AddWithValue("@tFreeTable", dt);

            //                    //image location Save File:
            //                    string tPath = "";
            //                    if (!string.IsNullOrEmpty(FileName))
            //                    {
            //                        conv_photo();
            //                        if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\OfferImage"))
            //                        {
            //                            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\OfferImage");
            //                        }
            //                        tPath = System.Windows.Forms.Application.StartupPath + "\\OfferImage\\" + txtitemname.Text.Trim() + ".jpeg";
            //                        if (!File.Exists(tPath))
            //                        {
            //                            System.IO.File.Copy(FileName, tPath);
            //                        }
            //                        else
            //                        {
            //                            GC.Collect();
            //                            System.IO.File.Delete(tPath);
            //                            GC.Collect();
            //                            System.IO.File.Copy(FileName, tPath);
            //                        }
            //                    }
            //                    else
            //                    {
            //                        //  tPath = ItemImage.ToString();
            //                    }
            //                    cmd.Parameters.AddWithValue("@ItemImage", tPath.ToString() == "" ? "" : "\\OfferImage\\" + txtitemname.Text.Trim() + ".jpeg");
            //                    string tResult = (isChk == false) ? "3" : MyMessageBox1.ShowBox("Same Item Offer already exist in selected date, Do you want to replace new offer?");
            //                    if (tResult == "1")
            //                    {
            //                        cmd.CommandType = CommandType.StoredProcedure;

            //                        string str = cmd.ExecuteNonQuery().ToString();
            //                        if (str == "-1")
            //                        {
            //                            tFreeSno = "";
            //                            txtitemcode.Text = "";
            //                            txtitemname.Text = "";
            //                            cmbtype.SelectedIndex = 0;
            //                            txtsalesqtyFrom.Text = "";
            //                            txtSalesQtyTo.Text = "";
            //                            txtsalesrate.Text = "";
            //                            txtActive.Text = "ACTIVE";
            //                            dataGridView1.Rows.Clear();
            //                            dtLoad.Rows.Clear();
            //                            dataGridView1.DataSource = dtLoad;
            //                            dataGridView1.Columns[0].Width = 150;
            //                            dataGridView1.Columns[1].Width = 300;
            //                            dataGridView1.Columns[2].Width = 150;
            //                            dataGridView1.Columns[3].Width = 150;
            //                            dataGridView1.Columns[4].Width = 150;
            //                            panel2.Visible = false;
            //                        }
            //                    }
            //                    else if (tResult == "3")
            //                    {
            //                        cmd.CommandType = CommandType.StoredProcedure;

            //                        string str = cmd.ExecuteNonQuery().ToString();
            //                        if (str == "-1")
            //                        {
            //                            tFreeSno = "";
            //                            txtitemcode.Text = "";
            //                            txtitemname.Text = "";
            //                            cmbtype.SelectedIndex = 0;
            //                            txtsalesqtyFrom.Text = "";
            //                            txtSalesQtyTo.Text = "";
            //                            txtsalesrate.Text = "";
            //                            txtActive.Text = "ACTIVE";
            //                            dataGridView1.Rows.Clear();
            //                            dtLoad.Rows.Clear();
            //                            dataGridView1.DataSource = dtLoad;
            //                            dataGridView1.Columns[0].Width = 150;
            //                            dataGridView1.Columns[1].Width = 300;
            //                            dataGridView1.Columns[2].Width = 150;
            //                            dataGridView1.Columns[3].Width = 150;
            //                            dataGridView1.Columns[4].Width = 150;
            //                            panel2.Visible = false;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (funValidation())
            //            {
            //                DataTable dtChk = new DataTable();
            //                dtChk.Rows.Clear();
            //                SqlCommand cmdChk = new SqlCommand("Select * from FreeItem_table where Active=1 and Item_no=@tItemNo and FromDate<=@tFromDate and ToDate>=@tToDate", con);
            //                cmdChk.Parameters.AddWithValue("@tItemNo", itemnumber);
            //                cmdChk.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
            //                cmdChk.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
            //                SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
            //                adpChk.Fill(dtChk);
            //                bool isChk = false;
            //                if (dtChk.Rows.Count > 0)
            //                {
            //                    isChk = true;
            //                    //MyMessageBox.ShowBox("Same item offer already exists", "Warning");
            //                }

            //                dt.Rows.Clear();
            //                for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //                {
            //                    if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["ItemNames"].Value)))
            //                    {
            //                        dt.Rows.Add(Convert.ToString(dataGridView1.Rows[i].Cells["ItemNames"].Value), string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["Free_Qty"].Value)) == true ? "1" : Convert.ToString(dataGridView1.Rows[i].Cells["Free_Qty"].Value), (string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["Rate"].Value)) == true) ? "0.00" : Convert.ToString(dataGridView1.Rows[i].Cells["Rate"].Value), string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["Stock"].Value)) == true ? "0" : Convert.ToString(dataGridView1.Rows[i].Cells["Stock"].Value), (txtActive.Text.Equals("ACTIVE") ? 1 : 0));
            //                    }
            //                }
            //                SqlCommand cmd = new SqlCommand("sp_FreeItemInsert", con);
            //                cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value);
            //                cmd.Parameters.AddWithValue("@tToDate", dptodate.Value);
            //                cmd.Parameters.AddWithValue("@tItemName", txtitemname.Text);
            //                cmd.Parameters.AddWithValue("@tOfferType", cmbtype.Text);
            //                cmd.Parameters.AddWithValue("@tSaleQtyFrom", (string.IsNullOrEmpty(txtsalesqtyFrom.Text) == true) ? "0" : txtsalesqtyFrom.Text);
            //                cmd.Parameters.AddWithValue("@tSaleQtyTo", (string.IsNullOrEmpty(txtSalesQtyTo.Text) == true) ? "0" : txtSalesQtyTo.Text);
            //                cmd.Parameters.AddWithValue("@tSalesRate", (string.IsNullOrEmpty(txtsalesrate.Text) == true) ? "0.00" : txtsalesrate.Text);
            //                cmd.Parameters.AddWithValue("@tActive", (txtActive.Text.Equals("ACTIVE") ? 1 : 0));
            //                cmd.Parameters.AddWithValue("@tFreeTable", dt);
            //                string tPath = "";
            //                if (!string.IsNullOrEmpty(FileName))
            //                {
            //                    conv_photo();
            //                    if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\OfferImage"))
            //                    {
            //                        Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\OfferImage");
            //                    }
            //                    tPath = System.Windows.Forms.Application.StartupPath + "\\OfferImage\\" + txtitemname.Text.Trim() + ".jpeg";
            //                    if (!File.Exists(tPath))
            //                    {
            //                        System.IO.File.Copy(FileName, tPath);
            //                    }
            //                    else
            //                    {
            //                        GC.Collect();
            //                        System.IO.File.Delete(tPath);
            //                        GC.Collect();
            //                        System.IO.File.Copy(FileName, tPath);
            //                    }
            //                }
            //                cmd.Parameters.AddWithValue("@ItemImage", tPath.ToString() == "" ? "" : "\\OfferImage\\" + txtitemname.Text.Trim() + ".jpeg");
            //                string tResult = (isChk == false) ? "3" : MyMessageBox1.ShowBox("Same Item Offer already exist in selected date, Do you want to replace new offer?");
            //                if (tResult == "1")
            //                {
            //                    cmd.CommandType = CommandType.StoredProcedure;
            //                    string str = cmd.ExecuteNonQuery().ToString();
            //                    if (str == "-1")
            //                    {
            //                        txtitemcode.Text = "";
            //                        txtitemname.Text = "";
            //                        cmbtype.SelectedIndex = 0;
            //                        txtsalesqtyFrom.Text = "";
            //                        txtSalesQtyTo.Text = "";
            //                        txtsalesrate.Text = "";
            //                        txtActive.Text = "ACTIVE";

            //                        // dataGridView1.DataSource = null;
            //                        dt.Rows.Clear();
            //                        dataGridView1.DataSource = null;
            //                        dtLoad.Rows.Clear();
            //                        dataGridView1.DataSource = dtLoad;
            //                        dataGridView1.Columns[0].Width = 150;
            //                        dataGridView1.Columns[1].Width = 300;
            //                        dataGridView1.Columns[2].Width = 150;
            //                        dataGridView1.Columns[3].Width = 150;
            //                        dataGridView1.Columns[4].Width = 150;
            //                        panel2.Visible = false;
            //                    }
            //                }
            //                else if (tResult == "3")
            //                {
            //                    cmd.CommandType = CommandType.StoredProcedure;
            //                    string str = cmd.ExecuteNonQuery().ToString();
            //                    if (str == "-1")
            //                    {
            //                        txtitemcode.Text = "";
            //                        txtitemname.Text = "";
            //                        cmbtype.SelectedIndex = 0;
            //                        txtsalesqtyFrom.Text = "";
            //                        txtSalesQtyTo.Text = "";
            //                        txtsalesrate.Text = "";
            //                        txtActive.Text = "ACTIVE";
            //                        dt.Rows.Clear();
            //                        dataGridView1.DataSource = null;
            //                        dtLoad.Rows.Clear();
            //                        dataGridView1.DataSource = dtLoad;
            //                        dataGridView1.Columns[0].Width = 150;
            //                        dataGridView1.Columns[1].Width = 300;
            //                        dataGridView1.Columns[2].Width = 150;
            //                        dataGridView1.Columns[3].Width = 150;
            //                        dataGridView1.Columns[4].Width = 150;
            //                        panel2.Visible = false;
            //                    }
            //                }
            //            }
            //        }

            //    }
            //    else
            //    {
            //        MyMessageBox.ShowBox("Enter item name not valid", "Warning");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MyMessageBox.ShowBox(ex.Message, "Warning");
            //}
        }
        
        byte[] photo_aray = new byte[0];
        byte[] imgByteArr = new byte[0];
        public void conv_photo()
        {
            FileStream fs = new FileStream(@FileName, FileMode.Open, FileAccess.Read);
            //Initialize a byte array with size of stream
            imgByteArr = new byte[fs.Length];
            //Read data from the file stream and put into the byte array
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
            fs.Close();
        }
        private void txtsalesqtyFrom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (blackValues == "")
                {
                    if(cmbItemType.Text=="Single")
                  //  if (cmbtype.Text.Equals("Price") && (cmbItemType.Text.Equals("Single") || cmbtype.Text.Equals("Free") || cmbtype.Text.Equals("Different Free")))
                    {
                        txtSalesQtyTo.Text = txtsalesqtyFrom.Text;
                        txtSalesQtyTo.Visible = false;
                        lbltotSalesQtyto.Visible = false;
                    }
                    //else if (cmbtype.Text == ("Same Free"))
                    //{
                    //    txtSalesQtyTo.Text = txtsalesqtyFrom.Text;
                    //    txtSalesQtyTo.Visible = false;
                    //    lbltotSalesQtyto.Visible = false;
                    //}
                    else
                    {
                        txtSalesQtyTo.Text = txtsalesqtyFrom.Text;
                        txtSalesQtyTo.Visible = true;
                        lbltotSalesQtyto.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void txtSalesQtyTo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cmbtype.Text.Equals("Price"))
                {
                    //txtsalesqtyFrom.Text = txtSalesQtyTo.Text;
                    if (!string.IsNullOrEmpty(txtSalesQtyTo.Text) && Convert.ToDouble(txtSalesQtyTo.Text) > 0)
                    {
                        gridItemName.ReadOnly = false;
                    }
                    else
                    {
                        gridItemName.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string FileName = "";

        public string check = null;
        private void btnBrowes_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\OfferImage"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\OfferImage");
                }
                check = "Image";
                FileName = openFileDialog1.FileName;
            }
        }

        private void lstbox_MouseClick(object sender, MouseEventArgs e)
        {
            lstbox_Click(sender, e);
        }

        private void gridItemName_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                con.Close();
                con.Open();
                SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table Where OpenItem='False'  order by Item_name ASC", con);
                DataTable autofind = new DataTable();
                autofind.Rows.Clear();
                SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
                nameadp.Fill(autofind);
                con.Close();

                if (this.gridItemName.CurrentCell.ColumnIndex == this.gridItemName.Columns["ItemNames"].Index) //Item_name
                {
                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                if (this.gridItemName.CurrentCell.ColumnIndex == this.gridItemName.Columns["ItemCode"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.gridItemName.CurrentCell.ColumnIndex == this.gridItemName.Columns["ItemNames"].Index) //Item_name
                {
                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }

                if (this.gridItemName.CurrentCell.ColumnIndex == this.gridItemName.Columns["Qty1"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        string RetrunNo = "";
        private void gridItemName_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (RetrunNo == "")
                {
                    string ie = Convert.ToString(e.RowIndex.ToString());
                    // DgPurchase.CurrentRow.Cells["Sno"].Value = (Convert.ToInt32(ie) + 1).ToString();
                    //  if (enter_emptystring == "1")
                    {
                        if (gridItemName.Rows[e.RowIndex].Cells["ItemNames"].Value == null && gridItemName.Rows[e.RowIndex].Cells["ItemCode"].Value == null)
                        {
                            if (gridItemName.CurrentCell.ColumnIndex > 2)
                            {
                                if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value == null)
                                {
                                    if (gridItemName.Rows.Count > 1)
                                    {
                                        var selected = gridItemName.SelectedCells;
                                        for (int x = 0; x < selected.Count; )
                                        {
                                            gridItemName.ClearSelection();
                                            MyMessageBox1.ShowBox("Please Enter Item Code Or Item Name", "Warning");

                                            break;
                                        }
                                        // btnSave.Focus();
                                    }
                                }
                            }
                        }
                    }

                    if (gridItemName.CurrentCell.ColumnIndex == 2 && gridItemName.Rows[e.RowIndex].Cells["ItemNames"].Value != null && gridItemName.Rows[e.RowIndex].Cells["ItemCode"].Value != null)
                    {
                        if (!cmbItemType.Text.Equals("Single"))
                        {
                            gridItemName.AllowUserToAddRows = true;
                        }
                        else
                        {
                            gridItemName.AllowUserToAddRows = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string CheckAgain = "";
        string FreeItemChk = "";
        private void gridItemName_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (gridItemName.CurrentRow != null && e.ColumnIndex == 0)
                    {
                        if (gridItemName.Rows[e.RowIndex].Cells["ItemCode"].Value != null && !string.IsNullOrEmpty(gridItemName.Rows[e.RowIndex].Cells["ItemCode"].Value.ToString()))
                        {
                            CheckAgain = "1";
                            GridValuesCheck();
                        }
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if ((gridItemName.CurrentRow != null && e.ColumnIndex == 1))
                    {
                        if (gridItemName.Rows[e.RowIndex].Cells["ItemNames"].Value != null && !string.IsNullOrEmpty(gridItemName.Rows[e.RowIndex].Cells["ItemNames"].Value.ToString()))
                        {
                            string itemname = "", chkdu = "";
                            //if (gridItemName.Rows[e.RowIndex].Cells["ItemNames"].Value != null)
                            //{
                            //    itemname = gridItemName.Rows[e.RowIndex].Cells["ItemNames"].Value.ToString();
                            //    ItemcodeorItemName1(itemname);
                            //    if (!string.IsNullOrEmpty(itemname))
                            //    {
                            //        if (dt_items1.Rows.Count > 0)
                            //        {
                            //            //  if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemCode"].Value != null && DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
                            //            if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
                            //            {
                            //                if (cmbtype.Text == "Free")
                            //                {
                            //                    chkdu = "";
                            //                    SqlCommand cmd = new SqlCommand("select FreeItemMasterView.Item_name As ItemNameMS,FreeItemMasterView.Item_Code As ItemCodeMs,FreeItemMasterDetailView.item_name As FreeItemName,FreeItemMasterDetailView.Item_code As FreeItemCode from FreeItemMasterView join FreeItemMasterDetailView on FreeItemMasterView.FreeSnoGroup=FreeItemMasterDetailView.FreeSno where FreeItemMasterView.FreeType='Free' and FreeItemMasterView.FromDate<=@tFromDate and FreeItemMasterView.ToDate>=@tToDate ", con);
                            //                    cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                            //                    cmd.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                            //                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            //                    DataTable dtChk = new DataTable();
                            //                    dtChk.Rows.Clear();
                            //                    adp.Fill(dtChk);
                            //                    if (dtChk.Rows.Count > 0)
                            //                    {
                            //                        for (int j = 0; j < dtChk.Rows.Count; j++)
                            //                        {
                            //                            if (!string.IsNullOrEmpty(dtChk.Rows[j]["FreeItemName"].ToString()))
                            //                            {
                            //                                if (dtChk.Rows[j]["FreeItemName"].ToString() == dt_items1.Rows[0]["Item_name"].ToString())
                            //                                {
                            //                                    chkdu = "1";
                            //                                    MyMessageBox.ShowBox("This Item Already Comes Under Free ItemList", "Warning");
                            //                                    gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = "";
                            //                                    CheckAgain = "1";
                            //                                    break;
                            //                                }
                            //                            }
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }
                            //        else
                            //        {
                            //            MyMessageBox1.ShowBox("Please Enter Correct ItemName or ItemCode", "Warning");
                            //            int nextindex = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 1);
                            //            SetColumnIndex method = new SetColumnIndex(Mymethod1);
                            //            this.gridItemName.BeginInvoke(method, 2 - 1);
                            //        }
                            //    }
                            //    if (chk == "1")
                            //    {
                            //        int nextindex = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 1);
                            //        SetColumnIndex method = new SetColumnIndex(Mymethod1);
                            //        this.gridItemName.BeginInvoke(method, 3);
                            //    }
                            //}
                            CheckAgain = "";
                            GridValuesCheck();
                        }
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (gridItemName.CurrentRow != null && e.ColumnIndex == 2)
                    {
                        //if (cmbtype.Text == "Price")
                        //{
                        //    if ((!string.IsNullOrEmpty(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["Qty1"].Value.ToString())) && !string.IsNullOrEmpty(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString()))
                        //    {
                        //        string chkdupGrid="1";
                        //        //  SqlCommand cmd = new SqlCommand("select FreeItemMasterView.Item_name As ItemNameMS,FreeItemMasterView.Item_Code As ItemCodeMs,FreeItemMasterDetailView.item_name As FreeItemName,FreeItemMasterDetailView.Item_code As FreeItemCode from FreeItemMasterView join FreeItemMasterDetailView on FreeItemMasterView.FreeSnoGroup=FreeItemMasterDetailView.FreeSno where FreeItemMasterView.FreeType='Price' and FreeItemMasterView.FromDate<=@tFromDate and FreeItemMasterView.ToDate>=@tToDate ", con);
                        //        SqlCommand cmd = new SqlCommand("select FreeItemMasterView.Item_name As ItemNameMS,FreeItemMasterView.Item_Code As ItemCodeMs,TotSaleQty from FreeItemMasterView  where FreeItemMasterView.FreeType='Price' and FreeItemMasterView.FromDate<=@tFromDate and FreeItemMasterView.ToDate>=@tToDate ", con);
                        //        cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                        //        cmd.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);

                        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        //        DataTable dtChk = new DataTable();
                        //        dtChk.Rows.Clear();
                        //        adp.Fill(dtChk);
                        //        if (dtChk.Rows.Count > 0)
                        //        {
                        //            string AnotherName = "", chkEmptname = "";
                        //            double totPurQty = 0.00;
                        //            AnotherName = Convert.ToString(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value).ToString();
                        //            totPurQty = Convert.ToDouble(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["Qty1"].Value);
                        //            for (int l = 0; l < dtChk.Rows.Count; l++)
                        //            {

                        //                if (AnotherName == dtChk.Rows[l]["ItemNameMS"].ToString().Trim() && totPurQty == Convert.ToDouble(dtChk.Rows[l]["TotSaleQty"]))
                        //                {
                        //                    int nextindex = Math.Min(this.gridItemName.Columns.Count, this.gridItemName.CurrentCell.ColumnIndex);
                        //                    SetColumnIndex method = new SetColumnIndex(Mymethod1);
                        //                    this.gridItemName.BeginInvoke(method, 2 - 1);

                        //                    MyMessageBox.ShowBox("This Item AlreadyComes Under the Another PriceList ", "Warning");
                        //                    chkdupGrid = "0";
                        //                    if (gridItemName.CurrentCell.RowIndex != 0)
                        //                    {
                        //                        gridItemName.Rows.RemoveAt(gridItemName.CurrentCell.RowIndex);
                        //                    }
                        //                    else
                        //                    {
                        //                        gridItemName.Rows.RemoveAt(0);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //        if (chkdupGrid.ToString().Trim() == "1")
                        //        { 
                        //        }
                        //    }
                        //}
                        GridValuesCheck();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void GridValuesCheck()
        {
            string itemcode = "";
            if (CheckAgain == "1")
            {
                if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value != null && !string.IsNullOrEmpty(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value.ToString()))
                {
                    itemcode = gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value.ToString();
                }
            }
            else
            {
                if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value != null && !string.IsNullOrEmpty(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString()))
                {
                    itemcode = gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString();
                }
            }
            ItemcodeorItemName1(itemcode);
            FreeItemChk = "";
            if (dt_items1.Rows.Count > 0)
            {
                SqlCommand cmd = null;
                //if ((cmbItemType.Text == "Different" && cmbtype.Text == "Price"))
                //{
                //    cmd = new SqlCommand("select [FreeItemMasterDifferentView].Item_name As ItemNameMS,[FreeItemMasterDifferentView].Item_Code As ItemCodeMs,[FreeItemMasterDifferentView].TotSaleQty,[FreeItemMasterDifferentView].SaleQty,FreeItemMasterDifferentView.FreeType from [FreeItemMasterDifferentView] where FreeItemMasterDifferentView.Active=1 and [FreeItemMasterDifferentView].FromDate<=@tFromDate and [FreeItemMasterDifferentView].ToDate>=@tToDate ", con);
                //}
                //else if (((cmbItemType.Text == "Single" && cmbtype.Text == "Price") || (cmbItemType.Text == "Single" && cmbtype.Text == "Same Free") || (cmbItemType.Text == "Single" && cmbtype.Text == "Different Free")))
                //{
                //    cmd = new SqlCommand("select [FreeItemMasterSingleDifferentView].Item_name As ItemNameMS,[FreeItemMasterSingleDifferentView].Item_Code As ItemCodeMs,[FreeItemMasterSingleDifferentView].TotSaleQty,[FreeItemMasterSingleDifferentView].SaleQty,FreeItemMasterSingleDifferentView.FreeType from [FreeItemMasterSingleDifferentView] where FreeItemMasterSingleDifferentView.Active=1 And [FreeItemMasterSingleDifferentView].FromDate<=@tFromDate and [FreeItemMasterSingleDifferentView].ToDate>=@tToDate ", con);
                //}
                if ((cmbItemType.Text == "Different" && cmbtype.Text == "Price"))
                {
                    if (string.IsNullOrEmpty(tFreeSno))
                    {
                        cmd = new SqlCommand("select [FreeItemMasterDifferentView].Item_name As ItemNameMS,[FreeItemMasterDifferentView].Item_Code As ItemCodeMs,[FreeItemMasterDifferentView].TotSaleQty,[FreeItemMasterDifferentView].SaleQty,FreeItemMasterDifferentView.FreeType from [FreeItemMasterDifferentView] where FreeItemMasterDifferentView.Active=1 and [FreeItemMasterDifferentView].FromDate<=@tFromDate and [FreeItemMasterDifferentView].ToDate>=@tToDate ", con);
                    }
                    else if (!string.IsNullOrEmpty(tFreeSno))
                    {
                        cmd = new SqlCommand("select [FreeItemMasterDifferentView].Item_name As ItemNameMS,[FreeItemMasterDifferentView].Item_Code As ItemCodeMs,[FreeItemMasterDifferentView].TotSaleQty,[FreeItemMasterDifferentView].SaleQty,FreeItemMasterDifferentView.FreeType from [FreeItemMasterDifferentView] where FreeItemMasterDifferentView.Active=1 and [FreeItemMasterDifferentView].freesnoGroup<>@freeSno and [FreeItemMasterDifferentView].FromDate<=@tFromDate and [FreeItemMasterDifferentView].ToDate>=@tToDate ", con);
                        cmd.Parameters.AddWithValue("@freeSno", tFreeSno.ToString());
                    }
                }
                else if (((cmbItemType.Text == "Single" && cmbtype.Text == "Price") || (cmbItemType.Text == "Single" && cmbtype.Text == "Same Free") || (cmbItemType.Text == "Single" && cmbtype.Text == "Different Free")))
                {
                    if (string.IsNullOrEmpty(tFreeSno))
                    {
                        cmd = new SqlCommand("select [FreeItemMasterSingleDifferentView].Item_name As ItemNameMS,[FreeItemMasterSingleDifferentView].Item_Code As ItemCodeMs,[FreeItemMasterSingleDifferentView].TotSaleQty,[FreeItemMasterSingleDifferentView].SaleQty,FreeItemMasterSingleDifferentView.FreeType from [FreeItemMasterSingleDifferentView] where FreeItemMasterSingleDifferentView.Active=1 And [FreeItemMasterSingleDifferentView].FromDate<=@tFromDate and [FreeItemMasterSingleDifferentView].ToDate>=@tToDate ", con);
                    }
                    else
                    {
                        cmd = new SqlCommand("select [FreeItemMasterSingleDifferentView].Item_name As ItemNameMS,[FreeItemMasterSingleDifferentView].Item_Code As ItemCodeMs,[FreeItemMasterSingleDifferentView].TotSaleQty,[FreeItemMasterSingleDifferentView].SaleQty,FreeItemMasterSingleDifferentView.FreeType from [FreeItemMasterSingleDifferentView] where FreeItemMasterSingleDifferentView.Active=1 and FreeItemMasterSingleDifferentView.freesnoGroup<>@freeSno And [FreeItemMasterSingleDifferentView].FromDate<=@tFromDate and [FreeItemMasterSingleDifferentView].ToDate>=@tToDate ", con);
                        cmd.Parameters.AddWithValue("@freeSno", tFreeSno.ToString());
                    }
                }
                cmd.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                cmd.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                DataTable dtChk = new DataTable();
                dtChk.Rows.Clear();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtChk);
                if (dtChk.Rows.Count > 0)
                {
                    double chkTotSaleQty = 0.00, SaleQty = 0.00;
                    string chkagin = "";
                    string FreeTypeValues = "";
                    for (int j = 0; j < dtChk.Rows.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(dtChk.Rows[j]["ItemNameMS"].ToString()))
                        {
                            if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString() != null && !string.IsNullOrEmpty(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString()))
                            {
                                if (dtChk.Rows[j]["ItemNameMS"].ToString().Trim() == gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString())
                                {

                                    chkTotSaleQty = Convert.ToDouble(dtChk.Rows[j]["TotSaleQty"].ToString());
                                    SaleQty = Convert.ToDouble(dtChk.Rows[j]["SaleQty"].ToString());
                                    FreeTypeValues = dtChk.Rows[j]["FreeType"].ToString();
                                    //if ((SaleQty) <= Convert.ToDouble(txtsalesqtyFrom.Text))
                                    if ((SaleQty) == Convert.ToDouble(txtsalesqtyFrom.Text))
                                    {
                                        enter_emptystring = "1";
                                        FreeItemChk = "1";
                                        MyMessageBox.ShowBox("This Item Already Comes Under Another Offer List", "Warning");
                                        dt_items1.Rows.Clear();
                                        //gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                        int nextindex1 = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 2);
                                        SetColumnIndex method1 = new SetColumnIndex(Mymethod1);
                                        this.gridItemName.BeginInvoke(method1, 2 - 1);
                                        gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                                        gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                        dt_items1.Rows.Clear();
                                        //ItemcodeorItemName1("");
                                        enter_emptystring = "";
                                        CheckAgain = "1";
                                        chkagin = "1";
                                        break;
                                    }

                                }
                            }
                        }
                    }
                    if (chkagin == "")
                    {
                        if (SaleQty > 0)
                        {
                            if (((SaleQty) < Convert.ToDouble(txtsalesqtyFrom.Text) || (SaleQty) > Convert.ToDouble(txtsalesqtyFrom.Text)) && cmbtype.Text != FreeTypeValues)
                            {
                                enter_emptystring = "1";
                                FreeItemChk = "1";
                                MyMessageBox.ShowBox("This Item Already Comes Under Another Offer List", "Warning");
                                dt_items1.Rows.Clear();
                                //gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                int nextindex1 = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 2);
                                SetColumnIndex method1 = new SetColumnIndex(Mymethod1);
                                this.gridItemName.BeginInvoke(method1, 2 - 1);
                                gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                                gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                dt_items1.Rows.Clear();
                                //ItemcodeorItemName1("");
                                enter_emptystring = "";
                                CheckAgain = "1";

                            }

                        }
                    }
                }
                if (FreeItemChk == "")
                {
                    //                        SqlCommand cmdFreeItem = new SqlCommand(@"select distinct Item_table.Item_name,freeItemMaster_table.FreeType from  freeitemMaster_table, 
                    //                       FreeItemDetail_table,Item_table where
                    //                       FreeItemDetail_table.FreeSno= freeitemMaster_table.FreeSnoGroup and Item_table.Item_no=FreeItemDetail_table.FreeItem_no and
                    //                       freeitemMaster_table.FreeType='Same Free' or freeitemMaster_table.FreeType='Different Free' and FromDate<=@tFromDate and ToDate>=@tToDate", con);
                    SqlCommand cmdFreeItem = new SqlCommand(@"select distinct Item_table.Item_name,freeItemMaster_table.FreeType from  freeitemMaster_table, 
                                               FreeItemDetail_table,Item_table where
                                               FreeItemDetail_table.FreeSno= freeitemMaster_table.FreeSnoGroup and Item_table.Item_no=FreeItemDetail_table.FreeItem_no and freeItemMaster_table.Active=1
                                               and FromDate<=@tFromDate and ToDate>=@tToDate", con);
                    cmdFreeItem.Parameters.AddWithValue("@tFromDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                    cmdFreeItem.Parameters.AddWithValue("@tToDate", dpfromdate.Value.Year + "/" + dpfromdate.Value.Month + "/" + dpfromdate.Value.Day);
                    SqlDataAdapter adpFree = new SqlDataAdapter(cmdFreeItem);
                    DataTable dtfreeItem = new DataTable();
                    dtfreeItem.Rows.Clear();
                    adpFree.Fill(dtfreeItem);
                    if (dtfreeItem.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtfreeItem.Rows.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(dtfreeItem.Rows[j]["Item_name"].ToString()))
                            {
                                if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString() != null && !string.IsNullOrEmpty(gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString()))
                                {
                                    if (dtfreeItem.Rows[j]["Item_name"].ToString().Trim() == gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value.ToString())
                                    {
                                        string chksameFree = "", chkFreeName = "";
                                        chkFreeName = dtfreeItem.Rows[j]["FreeType"].ToString();
                                        if (cmbtype.Text == "Same Free" && chkFreeName == "Same Free")
                                        {
                                            chksameFree = "1";
                                        }
                                        if (chksameFree != "1")
                                        {
                                            enter_emptystring = "1";
                                            FreeItemChk = "1";
                                            MyMessageBox.ShowBox("This Item Already Comes Under Free Item List", "Warning");
                                            dt_items1.Rows.Clear();
                                            //gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                            int nextindex1 = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 2);
                                            SetColumnIndex method1 = new SetColumnIndex(Mymethod1);
                                            this.gridItemName.BeginInvoke(method1, 2 - 1);
                                            gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value = string.Empty;
                                            gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = string.Empty;
                                            dt_items1.Rows.Clear();
                                            //ItemcodeorItemName1("");
                                            enter_emptystring = "";
                                            CheckAgain = "1";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //   }
                if (CheckAgain != "1")
                {
                    int nextindex = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 2);
                    SetColumnIndex method = new SetColumnIndex(Mymethod1);
                    this.gridItemName.BeginInvoke(method, 3);
                    enter_emptystring = "1";
                }
            }
            else
            {
                //MyMessageBox1.ShowBox("ItemCode Not Found", "Warning");
                //int nextindex = Math.Min(this.gridItemName.Columns.Count - 1, this.gridItemName.CurrentCell.ColumnIndex + 1);
                //SetColumnIndex method = new SetColumnIndex(Mymethod1);
                //this.gridItemName.BeginInvoke(method, 1);
            }
        }
        DataTable dt_items1 = new DataTable();
        public void ItemcodeorItemName1(string itemNamecode)
        {
            try
            {
                if (!string.IsNullOrEmpty(itemNamecode))
                {
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "Action");
                    cmd.Parameters.AddWithValue("@ItemCode", itemNamecode);
                    cmd.Parameters.AddWithValue("@itemName", itemNamecode);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    dt_items1.Rows.Clear();
                    adp.Fill(dt_items1);
                    if (dt_items1.Rows.Count > 0)
                    {
                        // if (enter_emptystring != "1")
                        {
                            if (dt_items1.Rows[0]["Item_code"].ToString().Trim() != "" && dt_items1.Rows[0]["Item_code"].ToString() != null)
                            {
                                gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value = dt_items1.Rows.Count > 0 ? dt_items1.Rows[0]["Item_code"].ToString() : string.Empty;
                            }
                            else
                            {
                                gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemCode"].Value = "";
                            }
                            gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value = dt_items1.Rows.Count > 0 ? dt_items1.Rows[0]["Item_name"].ToString() : string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void cmbItemType_KeyDown(object sender, KeyEventArgs e)        
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Enter)
            {
                cmbtype.Select();
                if (cmbItemType.Text == "Different")
                {
                    TStpDeleteFreeItemRow.Focus();
                }
            }
        }

        private void txtsalesrate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                gridItemName.Focus();
            }
        }

        private void gridItemName_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (exit_close != "1")
                // {
                if (gridItemName.CurrentCell.ColumnIndex == 1 && gridItemName.CurrentRow != null)
                {
                    //string itemnames = Convert.ToString(DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value).ToString();
                    MyMethodCll();
                }
                else if (gridItemName.CurrentCell.ColumnIndex == 0 && gridItemName.CurrentRow != null)
                {
                    MyMethodCll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        public void MyMethodCll()
        {
            if (gridItemName.Rows[gridItemName.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)
            {
                string t1 = gridItemName.Rows[gridItemName.CurrentRow.Index].Cells["ItemNames"].Value.ToString();
                int t2 = gridItemName.CurrentRow.Index;

                for (int j = 0; j < gridItemName.Rows.Count; j++)
                {
                    if (t2 != j)
                    {
                        if (gridItemName.Rows[j].Cells["ItemNames"].Value != null)
                        {
                            if (t1.ToLower() == gridItemName.Rows[j].Cells["ItemNames"].Value.ToString().ToLower())
                            {
                                MyMessageBox1.ShowBox("Item is already Entered");
                                int nextindex = Math.Min(3, this.gridItemName.CurrentCell.ColumnIndex);
                                SetColumnIndex method = new SetColumnIndex(Mymethod1);
                                this.gridItemName.BeginInvoke(method, 2);

                                break;
                            }
                        }
                    }
                }
            }
        }
        double TotQty = 0.00, Qty = 0.00;
        public void GridQtyCount()
        {
            TotQty = 0.00; Qty = 0.00;
            for (int i = 0; i < gridItemName.Rows.Count; i++)
            {
                Qty = (gridItemName.Rows[i].Cells["Qty1"].Value == null || gridItemName.Rows[i].Cells["Qty1"].Value == string.Empty) ? 0.00 : Convert.ToDouble(gridItemName.Rows[i].Cells["Qty1"].Value);
                if (Qty > 0)
                {
                    TotQty += Qty;
                }
            }
        }

        private void cmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blackValues != "1")
            {
                if (cmbItemType.Text == "Different")
                {
                    gridItemName.Rows.Clear();
                    cmbtype.SelectedIndex = 0;
                    cmbtype.Enabled = false;
                    gridItemName.ReadOnly = true;
                    lbltotSalesQtyto.Visible = true;
                    txtSalesQtyTo.Visible = true;
                    //TStpDeleteFreeItemRow.Text = string.Empty;
                    txtsalesqtyFrom.Text = string.Empty;
                    txtSalesQtyTo.Text = string.Empty;
                    txtsalesrate.Text = string.Empty;
                    if (gridItemName.Rows.Count == 0)
                    {
                        gridItemName.Rows.Add();
                    }
                }
                else
                {
                    gridItemName.Rows.Clear();
                    cmbtype.Enabled = true;
                    lbltotSalesQtyto.Visible = false;
                    txtSalesQtyTo.Visible = false;
                    if (gridItemName.Rows.Count == 0)
                    {
                        gridItemName.Rows.Add();
                    }                  
                    //TStpDeleteFreeItemRow.Text = string.Empty;
                    txtsalesqtyFrom.Text = string.Empty;
                    txtSalesQtyTo.Text = string.Empty;
                    txtsalesrate.Text = string.Empty;
                }
            }
        }

        private void CTMenuHidandShow_Click(object sender, EventArgs e)
        {
        //contextMenuStrip1.Visible = true;
            if (gridItemName.Rows.Count > 1)
            {
                string M1 = (MyMessageBox.ShowBox("Are You Sure Want To Delete", "Warning"));
                {
                    if (M1 == "1")
                    {
                        int i = 0;
                        if (gridItemName.Rows.Count > 1)
                        {
                            i = Convert.ToInt16(gridItemName.CurrentCell.RowIndex);
                            gridItemName.Rows.RemoveAt(i);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgFreeItemList.Rows.Count > 1)
            {
                string M1 = (MyMessageBox.ShowBox("Are You Sure Want To Delete", "Warning"));
                {
                    if (M1 == "1")
                    {
                        int i = 0;
                        if (dgFreeItemList.Rows.Count > 1)
                        {
                            i = Convert.ToInt16(dgFreeItemList.CurrentCell.RowIndex);
                            dgFreeItemList.Rows.RemoveAt(i);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
    