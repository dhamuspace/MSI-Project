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
using System.IO;
using System.Text.RegularExpressions;

namespace MSPOSBACKOFFICE
{
    public partial class Promotionalteration : Form
    {
        public Promotionalteration()
        {
            InitializeComponent();

            //  dtDetails1.Rows.Clear();
            //dtDetails1.Columns.Add("FreeSno");
            //dtDetails1.Columns.Add("FromDate");
            //dtDetails1.Columns.Add("ToDate");
            //dtDetails1.Columns.Add("Item_Code");
            //dtDetails1.Columns.Add("Item_Name");
            //dtDetails1.Columns.Add("OfferName");
            //dtDetails1.Columns.Add("ItemType");
            //dtDetails1.Columns.Add("FreeType");
            //dtDetails1.Columns.Add("totSaleQty");
            //dtDetails1.Columns.Add("TotSalePrice");

            //dtDetails1.Columns.Add("SaleQty");
            //dtDetails1.Columns.Add("SaleRate");
            //dtDetails1.Columns.Add("SaleAmt");
            //dtDetails1.Columns.Add("TotFreeQty");
            //dtDetails1.Columns.Add("TempTotFreeQty");
            //dgNdGroup.DataSource = dtDetails1.DefaultView;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());

        DataTable dtDetails1 = new DataTable();
        private void txtItem_code_TextChanged(object sender, EventArgs e)
        {
        }
        private void txt_itemname_Enter(object sender, EventArgs e)
        {
            listActionType = "Group";
        }
        private void txtItem_code_Enter(object sender, EventArgs e)
        {
            panel2.Visible = false;
            label1.Visible = false;
            listitems.Visible = false;
        }
        
        DataTable dt = new DataTable();
        private void Itemalteration_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtTypes;
            Normal();
            
          //  SqlCommand cmd = new SqlCommand("Select distinct(Item_table.Item_name) as Item_name from freeItem_table, Item_table where Item_table.Item_no=freeItem_table.Item_no order by Item_table.Item_name ASC", con);
          //  SqlDataAdapter adp = new SqlDataAdapter(cmd);
          //  adp.Fill(dt);
          ////  int j = 0;
          //  if (dt.Rows.Count > 0)
          //  {
          //      for (int i = 0; i < dt.Rows.Count; i++)
          //      {

          //          listitems.Items.Add(dt.Rows[i]["Item_name"].ToString());
                   
          //      }
          //  }
          //txt_itemname.Select();


            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            txtTypes.Select();

        }

        public void Normal()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select distinct OfferName,FreeSnoGroup from FreeItemMaster_table", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtOffername = new DataTable();
                dtOffername.Rows.Clear();
                adp.Fill(dtOffername);
                if (dtOffername.Rows.Count > 0)
                {
                    dgNdGroup.DataSource = null;
                    dgNdGroup.DataSource = dtOffername.DefaultView;
                    dgNdGroup.Columns["OfferName"].Width = 600;
                    dgNdGroup.Columns["FreeSnoGroup"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string id_number;
        //string item_name;
        private void txtItem_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txtItem_code.Text != "")
                {

                    DataTable dt_code = new DataTable();
                    //SqlCommand cmd = new SqlCommand("select item_no from item_table where item_code='" + txtItem_code.Text + "'", con);
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", "ItemCode");
                    cmd.Parameters.AddWithValue("@ItemCode",txtItem_code.Text);
                    cmd.Parameters.AddWithValue("@itemName", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt.Rows.Clear();
                    adp.Fill(dt_code);
                    if (dt_code.Rows.Count > 0)
                    {
                        string id_number = dt_code.Rows[0]["item_no"].ToString();
                        ItemCreations frm = new ItemCreations(id_number);
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        frm.Show();
                    }
                    else
                    {
                        DataTable dtbarcode = new DataTable();
                       // SqlCommand cmdbarcode = new SqlCommand("select * from barcode_table where barcode='" + txtItem_code.Text + "'", con);

                        SqlCommand cmdbarcode = new SqlCommand("SP_SelectQuery", con);
                        cmdbarcode.CommandType = CommandType.StoredProcedure;
                        cmdbarcode.Parameters.AddWithValue("ActionType", "Barcode");
                        //here put barcode values to itemcode:
                        cmdbarcode.Parameters.AddWithValue("@ItemCode", txtItem_code.Text);
                        cmdbarcode.Parameters.AddWithValue("@itemName", "");
                        SqlDataAdapter adpbarcode = new SqlDataAdapter(cmdbarcode);
                        adpbarcode.Fill(dtbarcode);
                        if (dtbarcode.Rows.Count > 0)
                        {
                            string id_number = dtbarcode.Rows[0]["item_no"].ToString();
                            ItemCreations frm = new ItemCreations(id_number);
                            frm.MdiParent = this.ParentForm;
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.WindowState = FormWindowState.Normal;
                            frm.Location = new Point(0, 80);
                            frm.Show();
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Item Code Not Found");
                        }
                    }
                }
                else
                {
                    txt_itemname.Focus();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool  isChk = false;
        private void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            panel2.Visible = true;
            label1.Visible = true;
            listitems.Visible = true;
            isChk = false;
            if (listActionType == "Group" && listActionType != null)
            {
                if (txt_itemname.Text.Trim() != null && txt_itemname.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = null;
                    DataTable dt_selectitem = new DataTable();
                    dt_selectitem.Rows.Clear();

                    SqlCommand cmdAlter = new SqlCommand("Select distinct(Item_table.Item_name) as Item_name from freeItem_table, Item_table where Item_table.Item_no=freeItem_table.Item_no and item_table.item_name like @ItemName order by item_table.item_name ASC", con);
                    cmdAlter.Parameters.AddWithValue("@ItemName",txt_itemname.Text.Trim()+"%");
                     adp = new SqlDataAdapter(cmdAlter);
                    
                    //SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@ActionType", "ItemSelNameChk");
                    //cmd.Parameters.AddWithValue("@ItemCode", "");
                    //cmd.Parameters.AddWithValue("@itemName",txt_itemname.Text);
                    //adp = new SqlDataAdapter(cmd);
                    isChk = false;
                    adp.Fill(dt_selectitem);
                    //}
                    if (dt_selectitem.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dt_selectitem.Rows[0]["item_name"].ToString().Trim();
                        for (int k = 0; k < listitems.Items.Count; k++)
                        {
                            if (tempstr == listitems.Items[k].ToString().Trim())
                            {
                                listitems.SetSelected(k, true);
                                txt_itemname.Select();
                                chk = "1";
                                txt_itemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txt_itemname.Text != "")
                        {
                            string name = txt_itemname.Text.Remove(txt_itemname.Text.Length - 1);
                            txt_itemname.Text = name.ToString();
                            txt_itemname.Select(txt_itemname.Text.Length, 0);
                        }
                        txt_itemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
        }
        string listActionType;
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
           

            if (e.KeyCode == Keys.Down)
            {

                if (listitems.SelectedIndex < listitems.Items.Count - 1)
                {
                    listitems.SetSelected(listitems.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (listitems.SelectedIndex > 0)
                {
                    listitems.SetSelected(listitems.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (listActionType == "Group")
                {
                    //checkvaluestotextbox();
                }

                if (listitems.SelectedItems.Count > 0)
                {
                    txt_itemname.Text = listitems.SelectedItem.ToString();
                    DataTable dtFree = new DataTable();
                    dtFree.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("Select FreeSno,FromDate,ToDate,SaleQtyfrom,SaleQtyTo, FreeType,FreeSnoGroup, Item_no from FreeItem_table where FreeSno in (Select distinct(FreeSnoGroup) from FreeItem_table) and item_no=(Select Item_no from Item_table where item_name=@tItemName) ", con);
                    cmd.Parameters.AddWithValue("@tItemName", txt_itemname.Text.Trim());
                    //  cmd.Parameters.AddWithValue("@tDateFrom",dpfromdate.Value);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtFree);
                    dgNdGroup.DataSource = dtFree;
                    dgNdGroup.Columns["FreeSno"].Visible = false;
                    dgNdGroup.Columns["FreeSnoGroup"].Visible = false;
                    dgNdGroup.Columns["Item_no"].Visible = false;
                }
                else
                {
                   
                       // txt_itemname.Text = listitems.SelectedItem.ToString();
                        DataTable dtFree = new DataTable();
                        dtFree.Rows.Clear();
                        SqlCommand cmd = new SqlCommand("Select FreeSno,FromDate,ToDate,SaleQtyfrom,SaleQtyTo, FreeType,FreeSnoGroup, Item_no from FreeItem_table where FreeSno in (Select distinct(FreeSnoGroup) from FreeItem_table)", con);
                        //cmd.Parameters.AddWithValue("@tItemName", txt_itemname.Text.Trim());
                        //  cmd.Parameters.AddWithValue("@tDateFrom",dpfromdate.Value);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtFree);
                        dgNdGroup.DataSource = dtFree;
                        dgNdGroup.Columns["FreeSno"].Visible = false;
                        dgNdGroup.Columns["FreeSnoGroup"].Visible = false;
                        dgNdGroup.Columns["Item_no"].Visible = false;
                    
                }

                panel2.Visible = false;
                listitems.Visible = false;
            }
        }
        public void checkvaluestotextbox()
        {
            try
            {
                if (listitems.Visible == true)
                {
                    txt_itemname.Text = listitems.SelectedItem.ToString();
                }
                panel2.Visible = false;
                listitems.Visible = false;
                txt_itemname.Select();
                DataTable dt1 = new DataTable();
                if (txt_itemname.Text != null)
                {
                    //if (txt_itemname.Text.IndexOf("'") != -1)
                    //{
                    //    string name = txt_itemname.Text.Replace("'", "''");

                    //    SqlCommand cmd = new SqlCommand("select distinct item_no from item_seltable where item_selname='" + name + "'", con);
                    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //    dt.Rows.Clear();
                        
                    //    adp.Fill(dt1);
                    //}
                    //else
                    //{
                    //    SqlCommand cmd = new SqlCommand("select distinct item_no from item_seltable where item_selname='" + txt_itemname.Text + "'", con);
                    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //    dt.Rows.Clear();
                        
                    //    adp.Fill(dt1);
                    //}
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "ItemSelName");
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", txt_itemname.Text);
                    SqlDataAdapter  adp = new SqlDataAdapter(cmd);
                    dt.Rows.Clear();
                    adp.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        id_number = dt1.Rows[0]["item_no"].ToString();
                    }
                    if (id_number != "" && id_number != null)
                    {
                        Promotion frm = new Promotion();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        

                    }
                }
            }
            catch
            { }
        }
        string chk;
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

        private void listitems_Click(object sender, EventArgs e)
        {
            if (listitems.Text != "")
            {
                txt_itemname.Text = listitems.SelectedItem.ToString();
                //checkvaluestotextbox();
                if (listitems.SelectedItems.Count > 0)
                {
                    txt_itemname.Text = listitems.SelectedItem.ToString();
                    DataTable dtFree = new DataTable();
                    dtFree.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("Select FreeSno,FromDate,ToDate,SaleQtyfrom,SaleQtyTo, FreeType,FreeSnoGroup, Item_no from FreeItem_table where FreeSno in (Select distinct(FreeSnoGroup) from FreeItem_table) and item_no=(Select Item_no from Item_table where item_name=@tItemName) ", con);
                    cmd.Parameters.AddWithValue("@tItemName", txt_itemname.Text.Trim());
                    //  cmd.Parameters.AddWithValue("@tDateFrom",dpfromdate.Value);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtFree);
                    dgNdGroup.DataSource = dtFree;
                    dgNdGroup.Columns["FreeSno"].Visible = false;
                    dgNdGroup.Columns["FreeSnoGroup"].Visible = false;
                    dgNdGroup.Columns["Item_no"].Visible = false;
                }
                listitems.Visible = false;
            }
        }

        private void dptodate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txt_itemname.Select();
            }
        }

        private void dptodate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                 txt_itemname.Select();
            }
        }
        private void myDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(dgNdGroup.Rows[e.RowIndex].Cells["FreeSnoGroup"].Value.ToString()))
                {
                    panel2.Visible = false;
                    listitems.Visible = false;
                    txt_itemname.Select();
                    if (txt_itemname.Text != null)
                    {
                      //  if (id_number != "" && id_number != null)
                        {
                            Promotion frm = new Promotion();
                            frm.tItemNameNew = txt_itemname.Text;
                            frm.tFreeSno = dgNdGroup.Rows[e.RowIndex].Cells["FreeSnoGroup"].Value.ToString();
                            frm.MdiParent = this.ParentForm;
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.WindowState = FormWindowState.Normal;
                            frm.Location = new Point(0, 80);
                            frm.SalesCreationEventHandlerNew += new EventHandler(CloseEvent);
                            frm.Show();
                            
                            //this.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void CloseEvent(object sender, EventArgs e)
        {
            Normal();
        }
        private void txtTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtTypes.Text == "Normal")
                {
                    txtTypes.Text = "Detail";
                    Details();
                }
                else
                {
                    txtTypes.Text = "Normal";
                    Normal();
                }
            }
        }
        DataTable dtduplicate = new DataTable();
        public void Details()
        {
            DataTable dtDetails = new DataTable();
            dtDetails.Rows.Clear();
            dtduplicate.Rows.Clear();
            dtDetails1.Rows.Clear();
            //SqlCommand cmd = new SqlCommand(@"select FreeItemMaster_table.FreeSno,FreeItemMaster_table.FromDate,FreeItemMaster_table.ToDate,FreeItemMaster_table.OfferName,FreeItemMaster_table.ItemType,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.TotSalePrice,FreeItemMaster_table.Item_no,FreeItemMaster_table.SaleQty,FreeItemMaster_table.SaleRate,FreeItemMaster_table.SaleAmt,
              //                                  FreeItemMaster_table.TotFreeQty,FreeItemMaster_table.TempTotFreeQty,item_table.Item_Name,item_table.Item_Code from FreeItemMaster_table join item_table on FreeItemMaster_table.item_no=item_table.item_no order by FreeSno", con);

            SqlCommand cmd = new SqlCommand(" select distinct FreeSnoGroup from FreeItemMaster_table Where Active=1", con);
            //cmd.Parameters.AddWithValue("@Fdate",DtpFromDate.Value.Day+"/"+DtpFromDate.Value.Month+"/"+DtpFromDate.Value.Year);
            SqlDataAdapter adpDetails = new SqlDataAdapter(cmd);
            dtduplicate.Rows.Clear();
            adpDetails.Fill(dtduplicate);
            if (dtduplicate.Rows.Count > 0)
            {
                dtDetails.Columns.Add("FreeSno");
                dtDetails.Columns.Add("FromDate");
                dtDetails.Columns.Add("ToDate");
                dtDetails.Columns.Add("ItemCode");
                dtDetails.Columns.Add("ItemName");
                dtDetails.Columns.Add("OfferName");
                dtDetails.Columns.Add("SalesQty");
                dtDetails.Columns.Add("FreeQty");
                //string chk="True";
                for (int i = 0; i < dtduplicate.Rows.Count; i++)
                {
                    SqlCommand cmd1 = new SqlCommand(@"select FreeItemMaster_table.FreeSnoGroup,Convert(Varchar,FreeItemMaster_table.FromDate,103) as FromDate,Convert(Varchar,FreeItemMaster_table.ToDate,103) as ToDate,FreeItemMaster_table.OfferName,FreeItemMaster_table.ItemType,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.TotSalePrice,FreeItemMaster_table.Item_no,FreeItemMaster_table.SaleQty,FreeItemMaster_table.SaleRate,FreeItemMaster_table.SaleAmt,
                                                FreeItemMaster_table.TotFreeQty,FreeItemMaster_table.TempTotFreeQty,item_table.Item_Name,item_table.Item_Code from FreeItemMaster_table join item_table on FreeItemMaster_table.item_no=item_table.item_no  where FreeItemMaster_table.FreeSnoGroup='" + dtduplicate.Rows[i]["FreeSnoGroup"].ToString() + "' and  FreeItemMaster_table.Active=1", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                    dtDetails1.Rows.Clear();
                    adp.Fill(dtDetails1);
                    if (dtDetails1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtDetails1.Rows.Count; j++)
                        {
                            if (j == 0)
                            {
                                dtDetails.Rows.Add(dtDetails1.Rows[j]["FreeSnoGroup"].ToString(), dtDetails1.Rows[j]["FromDate"].ToString(), dtDetails1.Rows[j]["ToDate"].ToString(), "", "", "", "", "");

                            }
                            dtDetails.Rows.Add(dtDetails1.Rows[j]["FreeSnoGroup"].ToString(), "", "", dtDetails1.Rows[j]["Item_Code"].ToString(), dtDetails1.Rows[j]["Item_Name"].ToString(), dtDetails1.Rows[j]["OfferName"].ToString(), dtDetails1.Rows[j]["SaleQty"].ToString(), "");
                        }
                    }
                    SqlCommand cmdfreeItem = new SqlCommand("select FreeItemDetail_table.FreeSno,FreeItemDetail_table.FreeItem_no,FreeItemDetail_table.FreeQty,FreeItemDetail_table.FreeQty,Item_table.Item_Code,Item_table.Item_Name from FreeItemDetail_table join item_table on FreeItemDetail_table.FreeItem_no=Item_table.Item_no where FreeItemDetail_table.FreeSno='" + dtDetails1.Rows[0]["FreeSnoGroup"].ToString().Trim() + "' and FreeItemDetail_table.Active=1", con);
                    SqlDataAdapter adpfreeitem = new SqlDataAdapter(cmdfreeItem);
                    DataTable dtFredditem = new DataTable();
                    adpfreeitem.Fill(dtFredditem);
                    if (dtFredditem.Rows.Count > 0)
                    {
                        for(int l=0;l<dtFredditem.Rows.Count;l++)
                        {
                            dtDetails.Rows.Add(dtFredditem.Rows[l]["FreeSno"].ToString(), "", "", dtFredditem.Rows[l]["Item_Code"].ToString(), dtFredditem.Rows[l]["Item_Name"].ToString(), "Free", "",dtFredditem.Rows[l]["FreeQty"].ToString());
                        }
                    }
                }
                dgNdGroup.DataSource = dtDetails.DefaultView;
                dgNdGroup.Columns["FreeSno"].Visible = false;
                dgNdGroup.Columns["ItemCode"].Width = 200;
                dgNdGroup.Columns["ItemName"].Width = 300;
                dgNdGroup.Columns["FromDate"].Width = 100;
                dgNdGroup.Columns["ToDate"].Width = 100;
                dgNdGroup.Columns["OfferName"].Width = 175;
            }
        }
        public void selectionMethod()
        {
            if (txtTypes.Text == "Normal")
            {
                txtTypes.Text = "Detail";
                Details();
            }
            else
            {
                txtTypes.Text = "Normal";
                Normal();
            }
        }
        private void DtpFromDate_ValueChanged(object sender, EventArgs e)
        {   
            txtFromDate.Text = DtpFromDate.Value.Day + "/" + DtpFromDate.Value.Month + "/" + DtpFromDate.Value.Year;
        }
        private void DtpToDate_ValueChanged(object sender, EventArgs e)
        {
            txtToDate.Text = DtpToDate.Value.Day + "/" + DtpToDate.Value.Month + "/" + DtpToDate.Value.Year;
        }
    }
}
