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
    public partial class frmItemView : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public frmItemView()
        {
            InitializeComponent();
            
        }

        private void frmItemView_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string itemno;
        private void txt_ItemCode_KeyDown(object sender, KeyEventArgs e)
        {
              
               
             if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {  
                txt_selname.Focus();
            }
        }
        //private void txt_selname_KeyDown(object sender, KeyEventArgs e)
        //{
        //}

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                if (lstgroup.SelectedIndex < lstgroup.Items.Count - 1)
                {
                    lstgroup.SetSelected(lstgroup.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstgroup.SelectedIndex > 0)
                {
                    lstgroup.SetSelected(lstgroup.SelectedIndex - 1, true);
                }
            }

            
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (panel3.Visible == true)
                {
                    if (lstgroup.SelectedIndex != -1)
                    {
                        txt_selname.Text = lstgroup.SelectedItem.ToString();
                    }
                    panel3.Visible = false;
                    txt_ItemCode.Focus();
                }
                txt_ItemCode.Focus();
                txt_ItemCode.Select();
            }

            placeitem_name();

        }
        string chk;
        string itemchckvales = "1";
        private void txt_selname_TextChanged(object sender, EventArgs e)
        {
            panel3.Visible = true;
            if (txt_selname.Text.Trim() != null && txt_selname.Text.Trim() != "")
                {
                    con.Close();
                    con.Open();
                    if (txt_selname.Text.IndexOf("'") != -1)
                    {

                        string name = txt_selname.Text.Replace("'", "''");
                        SqlCommand cmd = new SqlCommand("select item_selname from item_seltable where item_selname Like '" + name + "%'", con);
                        dRead = cmd.ExecuteReader();
                    }
                    else

                    {
                        SqlCommand cmd = new SqlCommand("select item_selname from item_seltable where item_selname Like '" + txt_selname.Text + "%'", con);
                        dRead = cmd.ExecuteReader();
                    }

                   // dRead.Close();

                     
                    bool isChk = false;
                    while (dRead.Read())
                    {
                        isChk = true;
                        string tempStr = dRead["Item_selname"].ToString();
                        for (int i = 0; i < lstgroup.Items.Count; i++)
                        {
                            if (dRead["Item_selname"].ToString() == lstgroup.Items[i].ToString())
                            {
                                itemchckvales = "0";
                                lstgroup.SetSelected(i, true);
                                
                                txt_selname.Select();
                                chk = "1";
                                txt_selname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }

                        }
                    }

                    if (isChk == false)
                    {
                        chk = "2";
                        txt_selname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                }
                else
                {
                    chk = "1";

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



    
        SqlDataReader dRead;
        private void txt_ItemCode_Leave(object sender, EventArgs e)
        
        {
            if (itemchckvales != "0")
            {
                if (txt_ItemCode.Text.Trim() != "")
                {
                    string ItemCode = txt_ItemCode.Text;

                    con.Close();
                    con.Open();

                    string ItemcodeCheck = @"SELECT     dbo.Item_table.Item_code,Item_table.Item_name,Item_table.Item_no, dbo.Item_table.Item_warranty, Brand_table_1.Brand_name, dbo.Model_table.Model_name, dbo.unit_table.unit_name, 
                      dbo.Item_Grouptable.Item_groupname, dbo.Brand_table.Brand_name AS Expr1, dbo.Item_table.Stock_Type, dbo.Item_table.Item_minstock, dbo.Item_table.Item_ndp, 
                      dbo.Item_table.Item_cost, dbo.Item_table.Item_Active, dbo.Item_table.Item_mrsp, dbo.Item_table.Item_special1, dbo.Item_table.Item_special2, 
                      dbo.Item_table.Item_special3, dbo.Item_table.Item_name, dbo.Tax_table.Tax_name
FROM         dbo.Brand_table INNER JOIN
                      dbo.Brand_table AS Brand_table_1 INNER JOIN
                      dbo.Item_table ON Brand_table_1.Brand_no = dbo.Item_table.Brand_no INNER JOIN
                      dbo.Model_table ON dbo.Item_table.Model_no = dbo.Model_table.Model_no INNER JOIN
                      dbo.unit_table ON dbo.Item_table.Unit_no = dbo.unit_table.unit_no INNER JOIN
                      dbo.Item_Grouptable ON dbo.Item_table.item_Groupno = dbo.Item_Grouptable.Item_groupno ON dbo.Brand_table.Brand_no = dbo.Item_table.Brand_no INNER JOIN
                      dbo.Tax_table ON dbo.Item_table.Tax_no = dbo.Tax_table.Tax_no where Item_code='" + ItemCode + "'";
                    SqlCommand cmdcheckcode = new SqlCommand(ItemcodeCheck, con);

                    dRead = cmdcheckcode.ExecuteReader();
                    if (dRead.HasRows)
                    {
                        if (dRead.Read())
                        {
                            txt_ItemName.Text = dRead["Item_name"].ToString();
                            txt_PrintName.Text = dRead["Item_name"].ToString();
                            txt_aliasName.Text = dRead["Item_name"].ToString();

                            txt_Unit.Text = dRead["unit_name"].ToString();
                            txt_groupName.Text = dRead["Item_groupname"].ToString();
                            txt_ModelName.Text = dRead["Model_name"].ToString();
                            txt_BrandName.Text = dRead["Brand_name"].ToString();

                            txt_warranty.Text = dRead["Item_warranty"].ToString();
                          //  
                            //  int Taxno = Convert.ToInt16(dRead["Tax_no"].ToString());
                            txt_Taxname.Text = dRead["Tax_name"].ToString();
                            txt_taxStock.Text = dRead["Stock_Type"].ToString();
                            txt_ntstock.Text = dRead["Item_minstock"].ToString();
                            //txt_Margin.Text = dRead["Margin_no"].ToString();
                            txt_Ndp.Text = dRead["Item_ndp"].ToString();
                            txt_cost.Text = dRead["Item_cost"].ToString();
                            txt_mrp.Text = dRead["Item_mrsp"].ToString();
                            txt_special_1.Text = dRead["Item_special1"].ToString();
                            txt_special_2.Text = dRead["Item_special2"].ToString();
                            txt_special_3.Text = dRead["Item_special3"].ToString();
                            txt_MinPrice.Text = "0.0";
                            txt_MaxPrice.Text = "0.0";
                            txt_status.Text = dRead["Item_Active"].ToString();
                        }
                        if (dRead != null)
                        {
                            dRead.Close();
                        }
                        SqlCommand cmd123 = new SqlCommand("select distinct * from item_table where item_code='" + txt_ItemCode.Text + "'", con);
                        dRead = cmd123.ExecuteReader();
                        while (dRead.HasRows)
                        {
                            if (dRead.Read())
                            {
                                itemno = dRead["item_no"].ToString();
                                break;
                            }
                        }
                        
                        placeitem_name();

                    }
                }
            }
        }
        public void placeitem_name()
        {
            if (itemchckvales != "0")
            {
                DataTable dt1 = new DataTable();
                // dt1.Columns.Add("item_selname", typeof(string));
                if (itemno != null && itemno != "")
                {
                    SqlCommand cmd_seltable = new SqlCommand("select distinct * from Item_seltable where item_no=" + Convert.ToInt64(itemno.ToString()) + "", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd_seltable);
                    if (dRead != null)
                    {
                        dRead.Close();
                    }


                    adp.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        txt_selname.Text = dt1.Rows[0]["Item_selname"].ToString();
                    }
                }
            }
        }
        string item_no;
        private void txt_selname_Leave(object sender, EventArgs e)
        {
            callingmethod();
        }
        public void callingmethod()
        {
            if (txt_selname.Text != "")
            {
                if (dRead != null)
                {
                    dRead.Close();
                }

                DataTable dt = new DataTable();
                if (txt_selname.Text.IndexOf("'") != -1)
                {
                    string name = txt_selname.Text.Replace("'", "''");
                    SqlCommand cmd_seltable = new SqlCommand("select distinct * from Item_seltable where item_selname='" + name+ "'", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd_seltable);
                    adp.Fill(dt);
                }
                else
                {
                    SqlCommand cmd_seltable = new SqlCommand("select distinct * from Item_seltable where item_selname='" + txt_selname.Text + "'", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd_seltable);
                    adp.Fill(dt);
 
                }
                if (dt.Rows.Count > 0)
                {
                    item_no = dt.Rows[0]["item_no"].ToString();
                }
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand(@"SELECT     dbo.Item_table.Item_code, dbo.Item_table.Item_warranty, Brand_table_1.Brand_name, dbo.Model_table.Model_name, dbo.unit_table.unit_name, 
                      dbo.Item_Grouptable.Item_groupname, dbo.Brand_table.Brand_name AS Expr1, dbo.Item_table.Stock_Type, dbo.Item_table.Item_minstock, dbo.Item_table.Item_ndp, 
                      dbo.Item_table.Item_cost, dbo.Item_table.Item_Active, dbo.Item_table.Item_mrsp, dbo.Item_table.Item_special1, dbo.Item_table.Item_special2, 
                      dbo.Item_table.Item_special3, dbo.Item_table.Item_name, dbo.Tax_table.Tax_name
FROM         dbo.Brand_table INNER JOIN
                      dbo.Brand_table AS Brand_table_1 INNER JOIN
                      dbo.Item_table ON Brand_table_1.Brand_no = dbo.Item_table.Brand_no INNER JOIN
                      dbo.Model_table ON dbo.Item_table.Model_no = dbo.Model_table.Model_no INNER JOIN
                      dbo.unit_table ON dbo.Item_table.Unit_no = dbo.unit_table.unit_no INNER JOIN
                      dbo.Item_Grouptable ON dbo.Item_table.item_Groupno = dbo.Item_Grouptable.Item_groupno ON dbo.Brand_table.Brand_no = dbo.Item_table.Brand_no INNER JOIN
                      dbo.Tax_table ON dbo.Item_table.Tax_no = dbo.Tax_table.Tax_no where Item_table.item_no='" + item_no + "'", con);
                SqlDataAdapter adpitemcode = new SqlDataAdapter(cmd1);
                dt.Rows.Clear();
                adpitemcode.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_ItemName.Text = dt.Rows[0]["Item_name"].ToString();
                    txt_PrintName.Text = dt.Rows[0]["Item_name"].ToString();
                    txt_aliasName.Text = dt.Rows[0]["Item_name"].ToString();
                    txt_ItemCode.Text = dt.Rows[0]["item_code"].ToString();
                    txt_Unit.Text = dt.Rows[0]["unit_name"].ToString();
                    txt_groupName.Text = dt.Rows[0]["Item_groupname"].ToString();
                    txt_ModelName.Text = dt.Rows[0]["Model_name"].ToString();
                    txt_BrandName.Text = dt.Rows[0]["Brand_name"].ToString();
                    // int Rackno = Convert.ToInt16(dt.Rows[0]["Rack_no"].ToString());
                    txt_warranty.Text = dt.Rows[0]["Item_warranty"].ToString();
                    itemno = dt.Rows[0]["Item_no"].ToString();
                    txt_Taxname.Text = dt.Rows[0]["Tax_name"].ToString();
                    //int Taxno = Convert.ToInt16(dt.Rows[0]["Tax_no"].ToString());
                    txt_taxStock.Text = dt.Rows[0]["Stock_Type"].ToString();
                    txt_ntstock.Text = dt.Rows[0]["Item_minstock"].ToString();
                    // int Margin_no = Convert.ToInt16(dt.Rows[0]["Margin_no"].ToString());
                    txt_Ndp.Text = dt.Rows[0]["Item_ndp"].ToString();
                    txt_cost.Text = dt.Rows[0]["Item_cost"].ToString();
                    txt_mrp.Text = dt.Rows[0]["Item_mrsp"].ToString();
                    txt_special_1.Text = dt.Rows[0]["Item_special1"].ToString();
                    txt_special_2.Text = dt.Rows[0]["Item_special2"].ToString();
                    txt_special_3.Text = dt.Rows[0]["Item_special3"].ToString();
                    txt_MinPrice.Text = "0.0";
                    txt_MaxPrice.Text = "0.0";
                    txt_status.Text = dt.Rows[0]["Item_Active"].ToString();

                }
                else
                {
                    emptyvalues();
                }

            }
            else
            {
                emptyvalues();

            }
        }
        public void emptyvalues()
        {
            if (dRead != null)
            {
                dRead.Close();
            }
            SqlCommand cmd = new SqlCommand(@"SELECT top 1 dbo.item_table.item_no,dbo.Item_table.Item_code,item_table.Item_warranty, Brand_table_1.Brand_name, dbo.Model_table.Model_name, dbo.unit_table.unit_name, dbo.Item_Grouptable.Item_groupname, 
                      dbo.Brand_table.Brand_name AS Expr1, dbo.Item_table.Stock_Type, dbo.Item_table.Item_minstock, dbo.Item_table.Item_ndp, dbo.Item_table.Item_cost, 
                      dbo.Item_table.Item_Active, dbo.Item_table.Item_mrsp, dbo.Item_table.Item_special1, dbo.Item_table.Item_special2, dbo.Item_table.Item_special3, 
                      dbo.Item_table.Item_name, dbo.Tax_table.Tax_name
FROM         dbo.Brand_table INNER JOIN
                      dbo.Brand_table AS Brand_table_1 INNER JOIN
                      dbo.Item_table ON Brand_table_1.Brand_no = dbo.Item_table.Brand_no INNER JOIN
                      dbo.Model_table ON dbo.Item_table.Model_no = dbo.Model_table.Model_no INNER JOIN
                      dbo.unit_table ON dbo.Item_table.Unit_no = dbo.unit_table.unit_no INNER JOIN
                      dbo.Item_Grouptable ON dbo.Item_table.item_Groupno = dbo.Item_Grouptable.Item_groupno ON dbo.Brand_table.Brand_no = dbo.Item_table.Brand_no INNER JOIN
                      dbo.Tax_table ON dbo.Item_table.Tax_no = dbo.Tax_table.Tax_no order by item_no", con);
            SqlDataAdapter adpitemcode = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adpitemcode.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txt_ItemName.Text = dt.Rows[0]["Item_name"].ToString();
                txt_PrintName.Text = dt.Rows[0]["Item_name"].ToString();
                txt_aliasName.Text = dt.Rows[0]["Item_name"].ToString();
                txt_ItemCode.Text = dt.Rows[0]["item_code"].ToString();
                txt_Unit.Text = dt.Rows[0]["unit_name"].ToString();
                txt_ModelName.Text = dt.Rows[0]["Item_groupname"].ToString();
                txt_ModelName.Text = dt.Rows[0]["Model_name"].ToString();
                txt_BrandName.Text = dt.Rows[0]["Brand_name"].ToString();
                // int Rackno = Convert.ToInt16(dt.Rows[0]["Rack_no"].ToString());
                txt_warranty.Text = dt.Rows[0]["Item_warranty"].ToString();
                itemno = dt.Rows[0]["Item_no"].ToString();
                //int Taxno = Convert.ToInt16(dt.Rows[0]["Tax_no"].ToString());
                txt_taxStock.Text = dt.Rows[0]["Stock_Type"].ToString();
                txt_ntstock.Text = dt.Rows[0]["Item_minstock"].ToString();
                // int Margin_no = Convert.ToInt16(dt.Rows[0]["Margin_no"].ToString());
                txt_Ndp.Text = dt.Rows[0]["Item_ndp"].ToString();
                txt_Taxname.Text = dt.Rows[0]["Tax_name"].ToString();
                txt_cost.Text = dt.Rows[0]["Item_cost"].ToString();
                txt_mrp.Text = dt.Rows[0]["Item_mrsp"].ToString();
                txt_special_1.Text = dt.Rows[0]["Item_special1"].ToString();
                txt_special_2.Text = dt.Rows[0]["Item_special2"].ToString();
                txt_special_3.Text = dt.Rows[0]["Item_special3"].ToString();
                txt_MinPrice.Text = "0.0";
                txt_MaxPrice.Text = "0.0";
                txt_status.Text = dt.Rows[0]["Item_Active"].ToString();


                SqlCommand cmd2 = new SqlCommand("select * from item_table where item_code='" + txt_ItemCode.Text.Trim() + "'", con);
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                dt2.Rows.Clear();
                adp2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    itemno = dt2.Rows[0]["item_no"].ToString();
                   // callingmethod();
                }

            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstgroup_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_ItemCode_Enter(object sender, EventArgs e)
        {
            panel3.Visible = false;
            itemchckvales = "1";
        }

        private void txt_selname_Enter(object sender, EventArgs e)
        {
            panel3.Visible = false;
            listselectevent();
            txt_selname.Focus();
            txt_selname.SelectAll();
            
        }
        public void listselectevent()
        {
            if (itemchckvales != "0")
            {
                con.Close();
                con.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select * from item_seltable", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                lstgroup.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstgroup.Items.Add(dt.Rows[i]["Item_selname"].ToString());
                    }
                    lstgroup.SetSelected(0, true);
                }
            }
        }

        private void lbl_Dt_3_Click(object sender, EventArgs e)
        {

        }
    }
}
