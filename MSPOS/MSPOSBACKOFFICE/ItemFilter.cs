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
    public partial class ItemFilter : Form
    {
        public ItemFilter()
        {
            InitializeComponent();
           
        }
        
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
   //     string id_number_item_code;
        string str;
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                loadanother_form();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            } 
        }
        string strDateWiseChk = "", strDateWiseChk_Not = "", strDateWiseChk11="",strn1_salesqty="";

        public void loadanother_form()
        {
            try
            {
                passingvalues.Stock_ItemRport = "";
                string itemgroupmaodel = "";
                str = "";
                //item master view this way->call matholy stock report form:

                if (passingvalues.chckvalues == "1")
                {
                    if (txtStock.Text.Trim() == "Stock Items" || txtStock.Text.Trim() == "All" || txtStock.Text.Trim() == "Negative Stock" || txtStock.Text.Trim() == "Zero Stock" || txtStock.Text.Trim() == "Non Negative Stock")
                    {
                        if (txtStock.Text.Trim() == "All" || txtStock.Text.Trim() == "Negative Stock" || txtStock.Text.Trim() == "Zero Stock"||txtStock.Text.Trim() == "Non Negative Stock")
                        {
                            str = "Select distinct Item_table.Item_code,item_table.Item_name, Convert(numeric(18,2),item_table.nt_cloqty) AS nt_cloqty,Convert(Numeric(18,2),item_table.item_cost) as item_cost,convert(numeric(18,2),ROUND(item_table.nt_cloqty*item_table.item_cost,2)) as tot from item_table Where ";
                        }
                        else if (txtStock.Text.Trim() == "Stock Items")
                        {
                            //strn purchase and opening balance:
                            str = "select item_table.Item_no As Item_code,item_table.Item_name,isnull(sum(nt_qty),0) AS nt_cloqty,item_table.item_cost as item_cost,'0' as tot from stktrn_table,item_table where (stktrn_table.strn_type=0 or stktrn_table.strn_type=3) and ";
                            //only Getting sales qty 
                            strn1_salesqty = "select item_table.Item_no As Item_code,item_table.Item_name,isnull(sum(nt_qty),0) AS nt_cloqty,item_table.Item_cost as item_cost,'0' as tot from stktrn_table,item_table where  stktrn_table.strn_type=1 and stktrn_table.strn_type<>2 and ";
                        }
                    }
                }

                    //else
                //{
                //    if (txtStock.Text.Trim() == "Stock Items")
                //    {
                //        strDateWiseChk = ""; strDateWiseChk11 = "";
                //        // strDateWiseChk = "select  Item_table.Item_code,item_table.Item_name, item_table.nt_cloqty,Convert(Numeric(18,2),item_table.item_cost) as item_cost,convert(numeric(18,2),ROUND(item_table.nt_cloqty*item_table.item_cost,2)) as tot  from Item_table  where ";
                //        strDateWiseChk11 = " select distinct  Item_table.Item_code,item_table.Item_name,sum(stktrn_table.nt_qty) as nt_cloqty,Convert(Numeric(18,2),item_table.item_cost) as item_cost,convert(numeric(18,2),ROUND(sum(stktrn_table.nt_qty)*item_table.item_cost,2)) as tot  from stktrn_table,Item_table where stktrn_table.item_no=Item_table.Item_no and stktrn_table.strn_date=@tStart and ";

                    //        strDateWiseChk_Not = "";
                //        // strDateWiseChk_Not = "select  Item_table.Item_code,item_table.Item_name,sum(stktrn_table.nt_qty*0) as nt_cloqty,Convert(Numeric(18,2),item_table.item_cost) as item_cost,convert(numeric(18,2),ROUND(0*item_table.item_cost,2)) as tot  from stktrn_table,Item_table where stktrn_table.item_no<>Item_table.Item_no  and stktrn_table.strn_date=@tStart and ";
                //        strDateWiseChk_Not = "Select distinct Item_table.Item_code,item_table.Item_name,'0' as nt_cloqty,item_table.item_cost as item_cost,'0.00' as tot from item_table where  ";
                //    }
                //    else
                //    {
                //        str = "Select distinct Item_table.Item_code,item_table.Item_name, Convert(numeric(18,2),item_table.nt_cloqty) AS nt_cloqty,Convert(Numeric(18,2),item_table.item_cost) as item_cost,convert(numeric(18,2),ROUND(item_table.nt_cloqty*item_table.item_cost,2)) as tot from item_table,stktrn_table Where ";
                //    }
                //}
                //item master view this way->call item table
                else
                {
                   // if (txtStock.Text == "Stock Items")
                    //{
                        str = "select item_table.Item_code,item_table.Item_Name,item_table.nt_opnqty,convert(Numeric(18,2),item_table.Stock_Value) as Stock_Value,convert(Numeric(18,2),item_table.Item_ndp) as Item_ndp,Convert(Numeric(18,2),item_table.Item_cost) as Item_cost,Convert(Numeric(18,2),item_table.Item_mrsp) as Item_mrsp,Convert(Numeric(18,2),item_table.item_special1) as item_special1,Convert(Numeric(18,2),item_table.item_special2) as item_special2,convert(Numeric(18,2),item_table.item_special3) as item_special3  from item_table  Where ";
                    //}
                    
                }
                if (txtGroup.Text != "")
                {
                    group_load();
                    str += "item_table.item_Groupno='" + id_group + "' AND ";

                    strn1_salesqty += "item_table.item_Groupno='" + id_group + "' AND ";

                    strDateWiseChk += "item_table.item_Groupno='" + id_group + "' AND ";

                    itemgroupmaodel = "1";
                }
                if (txt_Model.Text != "")
                {
                    modle_Load();
                    str += "  item_table.Model_no = '" + id_modle + "' AND ";

                    strn1_salesqty += "  item_table.Model_no = '" + id_modle + "' AND ";

                    strDateWiseChk += "  item_table.Model_no = '" + id_modle + "' AND ";

                    itemgroupmaodel = "1";
                }
                if (txtBrand.Text != "")
                {
                    btna_load();
                    str += "  item_table.Brand_no ='" + brand_no + "' AND ";


                    strn1_salesqty += "  item_table.Brand_no ='" + brand_no + "' AND ";


                    strDateWiseChk += "  item_table.Brand_no ='" + brand_no + "' AND ";
                    itemgroupmaodel = "1";
                }
                if (txtUnit.Text != "")
                {
                    unit_load();
                    str += "  item_table.Unit_no ='" + uni_no + "' AND ";

                    strn1_salesqty += "  item_table.Unit_no ='" + uni_no + "' AND ";

                    strDateWiseChk += "  item_table.Unit_no ='" + uni_no + "' AND ";

                    itemgroupmaodel = "1";
                }
                if (txtCode.Text != "")
                {
                    item_numbers();
                    str += " item_table.item_code='" + txtCode.Text + "' AND ";

                    strn1_salesqty += " item_table.item_code='" + txtCode.Text + "' AND ";


                    strDateWiseChk += " item_table.item_code='" + txtCode.Text + "' AND ";
                    itemgroupmaodel = "1";
                }
                if (txtAliasName.Text != "")
                {
                    str += "  item_table.item_aliasname='" + txtAliasName.Text + "' AND ";

                    strn1_salesqty += "  item_table.item_aliasname='" + txtAliasName.Text + "' AND ";

                    strDateWiseChk += "  item_table.item_aliasname='" + txtAliasName.Text + "' AND ";

                    itemgroupmaodel = "1";
                }
                if (txtRemark.Text != "")
                {
                    str += " item_table.item_Remarks='" + txtRemark.Text + "' AND ";

                    strn1_salesqty += " item_table.item_Remarks='" + txtRemark.Text + "' AND ";

                    strDateWiseChk += " item_table.item_Remarks='" + txtRemark.Text + "' AND ";
                }
                if (txtName.Text != "")
                {
                    str += " item_table.Item_name='" + txtName.Text.Trim() + "' AND ";

                    strn1_salesqty += " item_table.Item_name='" + txtName.Text.Trim() + "' AND ";

                    strDateWiseChk += "item_table.Item_name='" + txtName.Text.Trim() + "' AND ";
                    itemgroupmaodel = "1";
                }
                if (txtStock.Text == "Stock Items")
                {
                    passingvalues.Stock_ItemRport = "StockReport";
                  //  str += " item_table.nt_cloqty>=0  AND ";

                 //   strn1_salesqty += " item_table.nt_cloqty>=0  AND ";
                    
                }
               // if ((txtStock.Text == "All" || txtLevel.Text == "All" || txtMoement.Text == "All" || txtMoement.Text == "Transact Items") && (txtStock.Text != "Negative Stock" || txtStock.Text != "Stock Items" || txtStock.Text.Trim() != "Non Negative Stock")) 
                {
                   // if (txtStock.Text.Trim() != "Zero Stock" && txtLevel.Text != "Above Avg Sales" && txtLevel.Text != "Above Max Stock" && txtLevel.Text != "Above Min Stock" && txtStock.Text.Trim() != "Non Negative Stock" && txtStock.Text.Trim() != "Negative Stock")
                    if (txtStock.Text.Trim() == "All" && txtLevel.Text.Trim() == "All" && txtMoement.Text.Trim()=="All")
                    {
                        //if ((txtPrice.Text.Trim() == "" || txtPrice.Text.Trim() == "0") && (txtPrice1.Text.Trim() == "" || txtPrice1.Text.Trim() == "0") && (txtCost1.Text.Trim() == "" || txtCost1.Text.Trim() == "0") && (txtCost.Text.Trim() == "" || txtCost.Text.Trim() == "0") && (txtMrp.Text.Trim() == "" || txtMrp.Text.Trim() == "0") && (txtMrp1.Text.Trim() == ""||txtMrp1.Text.Trim() == "0"))
                        if (txtPrice.Text.Trim() == ""  && txtPrice1.Text.Trim() == ""  && txtCost1.Text.Trim() == "" && txtCost.Text.Trim() == "" && txtMrp.Text.Trim() == ""  && txtMrp1.Text.Trim() == "" && itemgroupmaodel .ToString().Trim()=="" )
                        {
                            str += " (item_table.nt_cloqty<=0 or item_table.nt_cloqty>=0) AND ";
                        }
                    }
                    else if (txtStock.Text.Trim() == "Non Negative Stock" )
                    {
                        str += " item_table.nt_cloqty>=0 AND ";

                        strn1_salesqty += " item_table.nt_cloqty>=0 AND ";

                        strDateWiseChk += " item_table.nt_cloqty>=0 AND ";
                    }
                    else if (txtStock.Text.Trim() == "Stock Items")
                    {
                        str += " item_table.nt_cloqty>0 AND ";

                        strn1_salesqty += " item_table.nt_cloqty>0 AND ";

                        strDateWiseChk += " item_table.nt_cloqty>0 AND ";
                    }
                }
                if ((txtLevel.Text == "Above Avg Sales" || txtLevel.Text == "Above Max Stock" || txtLevel.Text == "Above Min Stock") && txtStock.Text != "Negative Stock")
                {
                    str += " Item_table.nt_cloqty>0 AND ";

                    strn1_salesqty += " Item_table.nt_cloqty>0 AND ";

                    strDateWiseChk += " Item_table.nt_cloqty>0 AND ";
                }
                if (txtStock.Text == "Negative Stock")
                {
                    str += " Item_table.nt_cloqty<0  AND ";

                    strn1_salesqty += " Item_table.nt_cloqty<0  AND ";

                    strDateWiseChk += " Item_table.nt_cloqty<0  AND ";
                }
                //if (txtStock.Text == "Stock Items" || txtMoement.Text == "Transact Items" || txtLevel.Text == "Above Max Stock" || txtLevel.Text == "Above Min Stock")
                //{
                //   // str += "item_table.nt_purqty<>0 AND ";
                //}
                if ((txtStock.Text.Trim() == "Zero Stock" || txtMoement.Text.Trim() == "Non Transact Items" || txtLevel.Text.Trim() == "Below Avg Sales") && txtStock.Text != "Negative Stock") 
                {
                    str += " item_table.nt_cloqty=0 AND ";

                    strn1_salesqty += " item_table.nt_cloqty=0 AND ";

                    strDateWiseChk += "item_table.nt_cloqty=0 AND ";
                }
                if (txtLevel.Text.Trim() == "Below Max Stock")
                {
                    str += " item_table.item_maxstock=0  AND ";

                    strn1_salesqty += " item_table.item_maxstock=0  AND ";

                    strDateWiseChk += " item_table.item_maxstock=0  AND ";
                }
                if (txtLevel.Text.Trim() == "Below Mini Stock")
                {
                    str += " item_table.item_minstock=0  AND ";

                    strn1_salesqty += " item_table.item_minstock=0  AND ";

                    strDateWiseChk += " item_table.item_minstock=0  AND ";
                }
                if (txtLevel.Text.Trim() == "Critical Stock")
                {
                    str += " item_table.Item_Critical<>0 AND ";

                    strn1_salesqty += " item_table.Item_Critical<>0 AND ";

                    strDateWiseChk += " item_table.Item_Critical<>0 AND ";
                }
                if (txtMoement.Text.Trim() == "Moving Items")
                {
                    str += " item_table.nt_salqty>=0 AND ";

                    strn1_salesqty += " item_table.nt_salqty>=0 AND ";

                    strDateWiseChk += " item_table.nt_salqty>=0 AND ";
                }
                if (txtMoement.Text.Trim() == "Non Moving Items")
                {
                    str += " item_table.nt_salqty=0 AND ";

                    strn1_salesqty += " item_table.nt_salqty=0 AND ";

                    strDateWiseChk += " item_table.nt_salqty=0 AND ";
                }
                //if (txtPrice.Text.Trim() != "" && txtPrice.Text.Trim() != "0" || (txtCost.Text.Trim() != "" && txtCost.Text.Trim() != "0"))
                if (txtPrice.Text.Trim() != "" )
                {
                    str += " item_table.item_ndp>=" + txtPrice.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_ndp>=" + txtPrice.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_ndp>=" + txtPrice.Text.Trim() + " AND ";
                }
                if (txtPrice1.Text.Trim() != "")
                {
                    str += " item_table.item_ndp<=" + txtPrice1.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_ndp<=" + txtPrice1.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_ndp<=" + txtPrice1.Text.Trim() + " AND ";
                }
                //if (txtPrice1.Text.Trim() != "" && txtPrice1.Text.Trim() != "0" || (txtCost1.Text != "" && txtCost1.Text.Trim() != ""))

                if (txtPrice1.Text.Trim() == "" && txtPrice1.Text.Trim() == "" && txtCost1.Text != "" && txtCost.Text.Trim() != "" && txtMrp.Text.Trim() == "" && txtMrp1.Text.Trim() == "")
                {
                }
                if(txtCost.Text.Trim() != "")
                {
                    str += " item_table.item_cost>=" + txtCost.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_cost>=" + txtCost.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_cost>=" + txtCost.Text.Trim() + " AND ";
                }
                if (txtCost1.Text.Trim() != "")
                {
                    str += " item_table.item_cost<=" + txtCost1.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_cost<=" + txtCost1.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_cost<=" + txtCost1.Text.Trim() + " AND ";
                }
                if (txtMrp.Text.Trim() != "" && txtMrp.Text.Trim() != "")
                {
                    str += " item_table.item_mrsp>=" + txtMrp.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_mrsp>=" + txtMrp.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_mrsp>=" + txtMrp.Text.Trim() + " AND ";
                }
                if (txtMrp1.Text.Trim() != "" && txtMrp1.Text.Trim() != "")
                {
                    str += " item_table.item_mrsp<=" + txtMrp1.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_mrsp<=" + txtMrp1.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_mrsp<=" + txtMrp1.Text.Trim() + " AND ";
                }
                if (txtSpecial1.Text.Trim() != "" && txtSpecial1.Text.Trim() != "")
                {
                    str += " item_table.item_Special1>=" + txtSpecial1.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_Special1>=" + txtSpecial1.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_Special1>=" + txtSpecial1.Text.Trim() + " AND ";
                }
                if (txtSpecial1_one.Text.Trim() != "" && txtSpecial1_one.Text.Trim() != "")
                {
                    str += " item_table.item_Special1<=" + txtSpecial1.Text.Trim() + " AND ";

                    strn1_salesqty += " item_table.item_Special1<=" + txtSpecial1.Text.Trim() + " AND ";

                    strDateWiseChk += " item_table.item_Special1<=" + txtSpecial1.Text.Trim() + " AND ";
                }
                string s = str.Remove(str.Length - 4);
                
                if (passingvalues.chckvalues.ToString() != "0")
                {
                    passingvalues.strDateWiseChkRpt = "";
                    if (strDateWiseChk11!= null && strDateWiseChk11.ToString() != string.Empty)
                    {
                        strDateWiseChk11 += strDateWiseChk;
                        string strDateWiseChk_ = strDateWiseChk11.Remove(strDateWiseChk11.Length - 4);
                        passingvalues.strDateWiseChkRpt = strDateWiseChk_.ToString();

                        strDateWiseChk_Not += strDateWiseChk;

                        passingvalues.strDateWiseNoRcd = "";
                       // string strDatewiseChk2 = strDateWiseChk_Not.Remove(strDateWiseChk_Not.Length);
                        string strDatewiseChk2 = strDateWiseChk_Not.ToString();
                        passingvalues.strDateWiseNoRcd = strDatewiseChk2.ToString();
                    }
                }
                //MessageBox.Show(s);
                passingvalues.passingquery = "";
                passingvalues.Strn1_salesqty = "";
                passingvalues.passingquery = s.ToString();
                if (strn1_salesqty.ToString() != "")
                {
                    string s1 = strn1_salesqty.Remove(strn1_salesqty.Length - 4);
                    passingvalues.Strn1_salesqty = s1.ToString();
                }
                item_creation_load();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string item_code;
        //,uit_no,model_no,goup_no;
        public void item_numbers()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ItemCode");
                cmd.Parameters.AddWithValue("@ItemCode", txtCode.Text);
                cmd.Parameters.AddWithValue("itemName", "");
                //SqlCommand cmd = new SqlCommand("select * from item_table where item_code='"+txtCode.Text+"'",con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    item_code = dt.Rows[0]["item_no"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void item_creation_load()
        {
            try
            {
                string passingvalues_backoffice = passingvalues.chckvalues.ToString();
                if (passingvalues_backoffice == "1")
                {
                    StocReport frm = new StocReport();
                    passingvalues.itemcode = txtCode.Text;
                    passingvalues.numbervaluestoledger = "0";
                    passingvalues.datefrom = dateTimePicker1.Text;
                    passingvalues.dateto = dateTimePicker2.Text;
                    //Parthi
                    passingvalues.tStartDateParthi = Convert.ToDateTime(dateTimePicker1.Value.Year+"/"+dateTimePicker1.Value.Month+"/"+dateTimePicker1.Value.Day);
                    passingvalues.tToDateParthi = Convert.ToDateTime(dateTimePicker2.Value.Year + "/" + dateTimePicker2.Value.Month + "/" + dateTimePicker2.Value.Day);
                    //End Parthi
                    passingvalues.stock = txtStock.Text;
                    passingvalues.leave = txtLevel.Text;
                    passingvalues.movent = txtMoement.Text;
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                }
                else
                {
                    ItemList frm = new ItemList(txtCode.Text);
                    frm.MdiParent = this.ParentForm;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 80);
                    frm.Show();
                    //  frm.Show();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void ItemFilter_Load(object sender, EventArgs e)
        {
            try
            {
                pnllist.Visible = false;
                lvitems.Visible = false;
                btna_load();
                modle_Load();
                unit_load();
                group_load();

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        //brand
        string brand_no;
        public void btna_load()
        {
            try
            {
                DataTable dt_brand_table = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ActionType", "BrandNo");
                //SqlCommand cmd_bran = new SqlCommand("select  Brand_no from Brand_table where Brand_name  ='" + txtBrand.Text + "'", con);
                cmd.Parameters.AddWithValue("@itemName", txtBrand.Text);
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp_brna = new SqlDataAdapter(cmd);
                lvitems.Items.Clear();
                dt_brand_table.Rows.Clear();
                adp_brna.Fill(dt_brand_table);
                if (dt_brand_table.Rows.Count > 0)
                {
                    brand_no = dt_brand_table.Rows[0]["Brand_no"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        //brand_name-select 
        DataTable dt_brand_table = new DataTable();
        
    //group
        string id_group;
        public void group_load()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ItemGroupNo");
                cmd.Parameters.AddWithValue("@itemName", txtGroup.Text);
                cmd.Parameters.AddWithValue("@ItemCode", "");
                DataTable dt_group_nu = new DataTable();
                SqlDataAdapter adp_grou = new SqlDataAdapter(cmd);
                //group number name:
                dt_group_nu.Rows.Clear();
                adp_grou.Fill(dt_group_nu);
                if (dt_group_nu.Rows.Count > 0)
                {
                    adp_grou.Fill(dt_group_nu);
                    id_group = dt_group_nu.Rows[0]["item_groupno"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        //group_name-add_list:
         DataTable dt_group_nu = new DataTable();
//model:
        string id_modle;
        public void modle_Load()
        {
            try
            {
                DataTable dt_model_nu = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ModelNo");
                cmd.Parameters.AddWithValue("@itemName", txt_Model.Text);
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp_num = new SqlDataAdapter(cmd);
                lvitems.Items.Clear();
                dt_model_nu.Rows.Clear();

                adp_num.Fill(dt_model_nu);
                if (dt_model_nu.Rows.Count > 0)
                {
                    id_modle = dt_model_nu.Rows[0]["Model_no"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
//unit:
        string uni_no;
        public void unit_load()
        {
            try
            {
                DataTable dt_unitno = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "Unitnametonumber");
                cmd.Parameters.AddWithValue("@itemName", txtUnit.Text);
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                lvitems.Items.Clear();
                dt_unitno.Rows.Clear();
                adp.Fill(dt_unitno);
                if (dt_unitno.Rows.Count > 0)
                {
                    uni_no = dt_unitno.Rows[0]["unit_no"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        //unit name-add-to list:L
        DataTable dt = new DataTable();
       
        private void txtGroup_Enter(object sender, EventArgs e)
        {
            try
            {
                lbllist.Text = "List Of Group";
                pnllist.Visible = false;
                lvitems.Visible = false;
                pnlStockItems.Visible = false;
                pnllist.Visible = true;
                lvitems.Visible = true;
                lvitems.Visible = true;
                listActionType = "Group";
                txtName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txt_Model.BackColor = Color.White;
                txtGroup.BackColor = Color.LightBlue;
                txtBrand.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtStock.BackColor = Color.White;
                txtLevel.BackColor = Color.White;
                txtMoement.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtAliasName.BackColor = Color.White;
                txtRemark.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtMrp.BackColor = Color.White;
                txtPrice1.BackColor = Color.White;
                txtCost1.BackColor = Color.White;
                txtMrp1.BackColor = Color.White;
                txtSpecial1.BackColor = Color.White;
                txtSpecial1_one.BackColor = Color.White;
                lvitems.Items.Clear();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ItemGroupSelect");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroupname = new DataTable();
                dtGroupname.Rows.Clear();
                lvitems.Items.Clear();
                adp.Fill(dtGroupname);
                lbllist.Text = "List Of Groups";
                if (dtGroupname.Rows.Count > 0)
                {
                    for (int j = 0; j < dtGroupname.Rows.Count; j++)
                    {
                        lvitems.Items.Add(dtGroupname.Rows[j]["item_groupname"].ToString());
                    }
                }
                if (dtGroupname.Rows.Count > 0)
                {
                    for (int i = 0; i < dtGroupname.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(txtGroup.Text))
                        {
                        //    lvitems.SetSelected(0, true);
                        //}
                        //else
                        //{
                            if (txtGroup.Text.Trim() == dtGroupname.Rows[i]["item_groupname"].ToString())
                            {
                                lvitems.SetSelected(i, true);
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
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtName.Focus();
            }
        }
        private void txt_Model_Enter(object sender, EventArgs e)
        {
            try
            {
                lbllist.Text = "List Of Model";
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
              //  load_liv_model();
                pnlStockItems.Visible = false;

                pnllist.Visible = true;
                lvitems.Visible = true;
                listActionType = "Model";
                txtName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txt_Model.BackColor = Color.LightBlue;
                txtGroup.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtStock.BackColor = Color.White;
                txtLevel.BackColor = Color.White;
                txtMoement.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtAliasName.BackColor = Color.White;
                txtRemark.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtMrp.BackColor = Color.White;
                txtPrice1.BackColor = Color.White;
                txtCost1.BackColor = Color.White;
                txtMrp1.BackColor = Color.White;
                txtSpecial1.BackColor = Color.White;
                txtSpecial1_one.BackColor = Color.White;
                lvitems.Items.Clear();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "ModelSelect");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtModelselect = new DataTable();
                dtModelselect.Rows.Clear();
                lvitems.Items.Clear();
                lvitems.Text = "List Model";
                adp.Fill(dtModelselect);
                if (dtModelselect.Rows.Count > 0)
                {
                    for (int j = 0; j < dtModelselect.Rows.Count; j++)
                    {
                        lvitems.Items.Add(dtModelselect.Rows[j]["Model_name"].ToString());
                    }
                }
                if (dtModelselect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtModelselect.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(txt_Model.Text))
                        {
                        //    lvitems.SetSelected(0, true);
                        //}
                        //else
                        //{
                            if (txt_Model.Text.Trim() == dtModelselect.Rows[i]["Model_name"].ToString())
                            {
                                lvitems.SetSelected(i, true);
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtBrand_Enter(object sender, EventArgs e)
        {
            try
            {
                lbllist.Text = "List Of Brand";
                pnllist.Visible = false;
                lvitems.Visible = false;
           //     load_liv_brand();
                pnlStockItems.Visible = false;

                pnllist.Visible = true;
                lvitems.Visible = true;
                listActionType = "Brand";
                txtName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txt_Model.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtBrand.BackColor = Color.LightBlue;
                txtUnit.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtStock.BackColor = Color.White;
                txtLevel.BackColor = Color.White;
                txtMoement.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtAliasName.BackColor = Color.White;
                txtRemark.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtMrp.BackColor = Color.White;
                txtPrice1.BackColor = Color.White;
                txtCost1.BackColor = Color.White;
                txtMrp1.BackColor = Color.White;
                txtSpecial1.BackColor = Color.White;
                txtSpecial1_one.BackColor = Color.White;
                lvitems.Items.Clear();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "BrandSelect");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtBrandSelect = new DataTable();
                dtBrandSelect.Rows.Clear();
                lvitems.Items.Clear();
                lbllist.Text = "List Of Brand";
                adp.Fill(dtBrandSelect);
                if (dtBrandSelect.Rows.Count > 0)
                {
                    for (int j = 0; j < dtBrandSelect.Rows.Count; j++)
                    {
                        lvitems.Items.Add(dtBrandSelect.Rows[j]["Brand_name"].ToString());
                    }
                }
                if (dtBrandSelect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBrandSelect.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(txtBrand.Text))
                        {
                        //    lvitems.SetSelected(0, true);
                        //}
                        //else
                        //{
                            if (txtBrand.Text.Trim() == dtBrandSelect.Rows[i]["Brand_name"].ToString())
                            {
                                lvitems.SetSelected(i, true);
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
        private void txtUnit_Enter(object sender, EventArgs e)
        {
            try
            {
               
                //lbllist.Visible = false;
                listActionType = "Unit";
                pnlStockItems.Visible = false;

                pnllist.Visible = true;
                lvitems.Visible = true;

                // listActionType = "Unit";
                txtName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txt_Model.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtUnit.BackColor = Color.LightBlue;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtStock.BackColor = Color.White;
                txtLevel.BackColor = Color.White;
                txtMoement.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtAliasName.BackColor = Color.White;
                txtRemark.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtMrp.BackColor = Color.White;


                txtPrice1.BackColor = Color.White;
                txtCost1.BackColor = Color.White;
                txtMrp1.BackColor = Color.White;
                txtSpecial1.BackColor = Color.White;
                txtSpecial1_one.BackColor = Color.White;
                lvitems.Visible = true;
                lvitems.Items.Clear();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "UnitSelect");
                cmd.Parameters.AddWithValue("@itemName", "");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                lbllist.Text = "List Of Units";
                DataTable dtunitselect = new DataTable();
                dtunitselect.Rows.Clear();
                adp.Fill(dtunitselect);
                if (dtunitselect.Rows.Count > 0)
                {
                    for (int j = 0; j < dtunitselect.Rows.Count; j++)
                    {
                        lvitems.Items.Add(dtunitselect.Rows[j]["unit_name"].ToString());
                    }
                }
                if (dtunitselect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtunitselect.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(txtUnit.Text))
                        {
                        //    lvitems.SetSelected(0, true);
                        //}
                        //else
                        //{
                            if (txtUnit.Text.Trim() == dtunitselect.Rows[i]["unit_name"].ToString())
                            {
                                lvitems.SetSelected(i, true);
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
        private void OnTextBoxKeyDown2(object sender, KeyEventArgs e)
        {
            try
            {
                pnlStockItems.Visible = true;
                lstboxstock.Visible = true;
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
                if (e.KeyCode == Keys.Down)
                {
                    if (lstboxstock.SelectedIndex < lstboxstock.Items.Count - 1)
                    {
                        lstboxstock.SetSelected(lstboxstock.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lstboxstock.SelectedIndex > 0)
                    {
                        lstboxstock.SetSelected(lstboxstock.SelectedIndex - 1, true);
                    }
                }

                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (listtype == "Stock")
                    {
                        // pnltype.Visible = false;
                        if (lstboxstock.Text != "")
                        {
                            txtStock.Text = lstboxstock.SelectedItem.ToString();
                            txtLevel.Focus();
                        }
                        txtLevel.Focus();
                    }
                    else
                        if (listtype == "Level")
                        {
                            if (lstboxstock.Text != "")
                            {
                                txtLevel.Text = lstboxstock.SelectedItem.ToString();
                                txtMoement.Focus();
                            }
                            txtMoement.Focus();
                        }
                        else
                            if (listtype == "Movement")
                            {
                                if (lstboxstock.Text != "")
                                {
                                    txtMoement.Text = lstboxstock.SelectedItem.ToString();
                                    txtActive.Focus();
                                }
                                txtActive.Focus();
                            }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
       
        private void txtActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCode.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
               // lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAliasName.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }
        private void txtAliasName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRemark.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }
        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPrice.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }
        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPrice1.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
               // lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }
        private void txtPrice1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCost.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCost1.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
                //lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }
        private void txtCost1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtMrp.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
               // lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }
        private void txtMrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtMrp1.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
               // lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }

        private void txtMrp1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtSpecial1.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
              //  lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }

        private void txtSpecial1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtSpecial1_one.Focus();
                pnllist.Visible = false;
                lvitems.Visible = false;
               // lbllist.Visible = false;
                lstboxstock.Visible = false;
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.LightBlue;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
           // txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;
            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            pnllist.Visible = false;
            lvitems.Visible = false;
           // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
        }

        private void txtSpecial1_one_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txt_Model.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtStock.BackColor = Color.White;
                txtLevel.BackColor = Color.White;
                txtMoement.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtAliasName.BackColor = Color.White;
                txtRemark.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtMrp.BackColor = Color.White;


                txtPrice1.BackColor = Color.White;
                txtCost1.BackColor = Color.White;
                txtMrp1.BackColor = Color.White;
                txtSpecial1.BackColor = Color.White;
                txtSpecial1_one.BackColor = Color.White;
                // lbllist.Visible = false;
                lstboxstock.Visible = false;
                pnlStockItems.Visible = false;
            
                btnOK.Focus();
            }
        }
        string listtype;
        DataTable dtStock = new DataTable();
        private void txtStock_Enter(object sender, EventArgs e)
        {
           // lbllist.Visible = true;
            lvitems.Visible = false;
            pnllist.Visible = false;
            pnlStockItems.Visible = true;
            lstboxstock.Visible = true;
            lstboxstock.Items.Clear();
            dtStock.Rows.Clear();
            if(dtStock.Columns.Count==0)
            {
            dtStock.Columns.Add("Stock");
            }
            dtStock.Rows.Add("All");
            dtStock.Rows.Add("Negative Stock");
            dtStock.Rows.Add("Non Negative Stock");
            dtStock.Rows.Add("Stock Items");
            dtStock.Rows.Add("Zero Stock");
            for (int i = 0; i < dtStock.Rows.Count; i++)
            {
                lstboxstock.Items.Add(dtStock.Rows[i][0].ToString());
            }
            if (dtStock.Rows.Count > 0)
            {
                for (int i = 0; i < dtStock.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(txtStock.Text))
                    {
                        if (txtStock.Text.Trim() == dtStock.Rows[i]["Stock"].ToString())
                        {
                            lstboxstock.SetSelected(i, true);
                        }
                    }
                }
            }
            listtype = "Stock";
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.LightBlue;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;
            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
        }

        private void txtCode_Enter(object sender, EventArgs e)
        {
            //lbllist.Visible = false;
            lvitems.Visible = false;
            pnllist.Visible = false;
            pnlStockItems.Visible = false;
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.LightBlue ;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;

            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            passingvalues.DateSelectChanged = string.Empty;
            this.Close();
        }

        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btn_SaveValue.Focus() == true)
                {
                    loadanother_form();
                }
            }
        }

        private void lbllist_Click(object sender, EventArgs e)
        {

        }
        string chk;
        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pnllist.Visible = true;
                lvitems.Visible = true;
                if (txtUnit.Text.Trim() != null && txtUnit.Text.Trim() != "")
                {
                    bool isChk = true;
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "UnitTable");
                    cmd.Parameters.AddWithValue("@itemName", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dtUnit = new DataTable();
                    dtUnit.Rows.Clear();
                    adp.Fill(dtUnit);
                    isChk = false;
                    if (dtUnit.Rows.Count > 0)
                    {
                        isChk = false;
                        string tempstr = dtUnit.Rows[0]["unit_name"].ToString();
                        for (int k = 0; k < lvitems.Items.Count; k++)
                        {
                            if (tempstr == lvitems.Items[k].ToString())
                            {
                                isChk = true;
                                lvitems.SetSelected(k, true);
                                txtUnit.Select();
                                chk = "1";
                                txtUnit.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "1";
                        if (txtUnit.Text != "")
                        {
                            string name = txtUnit.Text.Remove(txtUnit.Text.Length - 1);
                            txtUnit.Text = name.ToString();
                            txtUnit.Select(txtUnit.Text.Length, 0);
                        }
                        txtUnit.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
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
       // }
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
        string listActionType;
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Down)
            {
                if (lvitems.SelectedIndex < lvitems.Items.Count - 1)
                {
                    lvitems.SetSelected(lvitems.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (lvitems.SelectedIndex > 0)
                {
                    lvitems.SetSelected(lvitems.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (listActionType == "Unit")
                {
                    lvitems.Visible = false;
                    if (lvitems.SelectedItems.Count > 0)
                    {
                        //if (!string.IsNullOrEmpty(txtUnit.Text))
                        {
                            txtUnit.Text = lvitems.SelectedItem.ToString();
                        }
                    }
                    txtStock.Select();
                    pnlStockItems.Visible = true;
                    lstboxstock.Visible = true;
                }
                else if (listActionType == "Group")
                {
                    lvitems.Visible = false;
                    if (lvitems.SelectedItems.Count > 0)
                    {
                       // if (!string.IsNullOrEmpty(txtGroup.Text))
                        {
                            txtGroup.Text = lvitems.SelectedItem.ToString();
                        }
                    }
                    txt_Model.Select();
                }
                else if (listActionType == "Model")
                {
                    lvitems.Visible = false;
                    if (lvitems.SelectedItems.Count > 0)
                    {
                       // if (!string.IsNullOrEmpty(txt_Model.Text))
                        {
                            txt_Model.Text = lvitems.SelectedItem.ToString();
                        }
                    }
                    txtBrand.Select();
                }
                else if (listActionType == "Brand")
                {
                    lvitems.Visible = false;
                    if (lvitems.SelectedItems.Count > 0)
                    {
                       // if (!string.IsNullOrEmpty(txtBrand.Text))
                        {
                            txtBrand.Text = lvitems.SelectedItem.ToString();
                        }
                    }
                    txtUnit.Select();
                }
            }
            //if (e.Alt && e.KeyCode == Keys.A)
            //{
 
            //}

        }
        private void textBox2_press_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtGroup_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pnllist.Visible = true;
                if (listActionType != "Over")
                {
                    if (txtGroup.Text.Trim() != null && txtGroup.Text.Trim() != "")
                    {
                        bool isChk = true;
                        SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "ModelGroupTable");
                        cmd.Parameters.AddWithValue("@itemName", txtGroup.Text);
                        cmd.Parameters.AddWithValue("@ItemCode", "");
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);

                        DataTable dtGroup = new DataTable();
                        dtGroup.Rows.Clear();
                        adp.Fill(dtGroup);
                        isChk = false;
                        if (dtGroup.Rows.Count > 0)
                        {
                            isChk = false;
                            string tempstr = dtGroup.Rows[0]["item_groupname"].ToString();
                            for (int k = 0; k < lvitems.Items.Count; k++)
                            {
                                if (tempstr == lvitems.Items[k].ToString())
                                {
                                    isChk = true;
                                    lvitems.SetSelected(k, true);
                                    txtGroup.Select();
                                    chk = "1";
                                    txtGroup.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "1";
                            if (txtGroup.Text != "")
                            {
                                string name = txtGroup.Text.Remove(txtGroup.Text.Length - 1);
                                txtGroup.Text = name.ToString();
                                txtGroup.Select(txtGroup.Text.Length, 0);
                            }
                            txtGroup.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            //  chk = "1";
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
        private void txtBrand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pnllist.Visible = true;
                lvitems.Visible = true;
                if (listActionType != "Over")
                {
                    if (txtBrand.Text.Trim() != null && txtBrand.Text.Trim() != "")
                    {
                        bool isChk = true;
                        SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "BrandTable");
                        cmd.Parameters.AddWithValue("@itemName", txtBrand.Text);
                        cmd.Parameters.AddWithValue("@ItemCode", "");
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dtBrand = new DataTable();
                        dtBrand.Rows.Clear();
                        adp.Fill(dtBrand);
                        isChk = false;
                        if (dtBrand.Rows.Count > 0)
                        {
                            isChk = false;
                            string tempstr = dtBrand.Rows[0]["Brand_name"].ToString();
                            for (int k = 0; k < lvitems.Items.Count; k++)
                            {
                                if (tempstr == lvitems.Items[k].ToString())
                                {
                                    isChk = true;
                                    lvitems.SetSelected(k, true);
                                    txtBrand.Select();
                                    chk = "1";
                                    txtBrand.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "1";
                            if (txtBrand.Text != "")
                            {
                                string name = txtBrand.Text.Remove(txtBrand.Text.Length - 1);
                                txtBrand.Text = name.ToString();
                                txtBrand.Select(txtBrand.Text.Length, 0);
                            }
                            txtBrand.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            //  chk = "1";
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
        private void txt_Model_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pnllist.Visible = true;
                lvitems.Visible = true;
                if (listActionType != "Over")
                {
                    if (txt_Model.Text.Trim() != null && txt_Model.Text.Trim() != "")
                    {
                        bool isChk = true;
                        SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ActionType", "ModelTable");
                        cmd.Parameters.AddWithValue("@itemName", txt_Model.Text);
                        cmd.Parameters.AddWithValue("@ItemCode", "");
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dtUnit = new DataTable();
                        dtUnit.Rows.Clear();
                        adp.Fill(dtUnit);
                        isChk = false;
                        if (dtUnit.Rows.Count > 0)
                        {
                            isChk = false;
                            string tempstr = dtUnit.Rows[0]["Model_name"].ToString();
                            for (int k = 0; k < lvitems.Items.Count; k++)
                            {
                                if (tempstr == lvitems.Items[k].ToString())
                                {
                                    isChk = true;
                                    lvitems.SetSelected(k, true);
                                    txt_Model.Select();
                                    chk = "1";
                                    txt_Model.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "1";
                            if (txt_Model.Text != "")
                            {
                                string name = txt_Model.Text.Remove(txt_Model.Text.Length - 1);
                                txt_Model.Text = name.ToString();
                                txt_Model.Select(txt_Model.Text.Length, 0);
                            }
                            txt_Model.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            //  chk = "1";
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
        DataTable dtLevel = new DataTable();
        private void txtLevel_Enter(object sender, EventArgs e)
        {
            //lbllist.Visible = false;
            lvitems.Visible = false;
            pnllist.Visible = false;
            pnlStockItems.Visible = true;
            lstboxstock.Visible = true;
            lstboxstock.Items.Clear();
            dtLevel.Rows.Clear();
            if (dtLevel.Columns.Count == 0)
            {
                dtLevel.Columns.Add("Level");
            }
            dtLevel.Rows.Add("Above Avg Sales");
            dtLevel.Rows.Add("Above Max Stock");
            dtLevel.Rows.Add("Above Mini Stock");
            dtLevel.Rows.Add("All");
            dtLevel.Rows.Add("Below Avg Sales");
            dtLevel.Rows.Add("Below Max Stock");
            dtLevel.Rows.Add("Below Mini Stock");
            dtLevel.Rows.Add("Critical Stock");
            for (int i = 0; i < dtLevel.Rows.Count; i++)
            {
                lstboxstock.Items.Add(dtLevel.Rows[i]["Level"].ToString());
            }
            if (dtLevel.Rows.Count > 0)
            {
                for (int i = 0; i < dtLevel.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(txtLevel.Text))
                    {
                        //    lvitems.SetSelected(0, true);
                        //}
                        //else
                        //{
                        if (txtLevel.Text.Trim() == dtLevel.Rows[i]["Level"].ToString())
                        {
                            lstboxstock.SetSelected(i, true);
                        }
                    }
                }
            }
            txtLevel.Select();
            listtype = "Level";
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.LightBlue;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
        }
        DataTable dtMovements = new DataTable();
        private void txtMoement_Enter(object sender, EventArgs e)
        {
            // lbllist.Visible = false;
            if (dtMovements.Columns.Count == 0)
            {
                dtMovements.Columns.Add("Movements");
            }
            lvitems.Visible = false;
            pnllist.Visible = false;
            pnlStockItems.Visible = true;
            lstboxstock.Visible = true;
            lstboxstock.Items.Clear();
            dtMovements.Rows.Clear();

            dtMovements.Rows.Add("All");
            dtMovements.Rows.Add("Moving Items");
            dtMovements.Rows.Add("Non Moving Items");
            dtMovements.Rows.Add("Non Transact Items");
            dtMovements.Rows.Add("Transact Items");

            for (int i = 0; i < dtMovements.Rows.Count; i++)
            {
                lstboxstock.Items.Add(dtMovements.Rows[i][0].ToString());
            }
            if (dtMovements.Rows.Count > 0)
            {
                for (int i = 0; i < dtMovements.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(txtMoement.Text))
                    {
                        //    lvitems.SetSelected(0, true);
                        //}
                        //else
                        //{
                        if (txtMoement.Text.Trim() == dtMovements.Rows[i]["Movements"].ToString())
                        {
                            lstboxstock.SetSelected(i, true);
                        }
                    }
                }
            }
            txtMoement.Select();
            listtype = "Movement";
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.LightBlue;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
        }
        private void txtActive_Enter(object sender, EventArgs e)
        {
            pnlStockItems.Visible = false;
           //  lbllist.Visible = false;
            lvitems.Visible = false;
            pnllist.Visible = false;
            lstboxstock.Items.Clear();
            txtActive.Select();

            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.LightBlue;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;



            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
        }
        string chkStr1,chkstr2;
        private void txtStock_TextChanged(object sender, EventArgs e)
       {
            try
            {
                if (txtStock.Text.Trim() != null && txtStock.Text.Trim() != "")
                {
                    //for (int i = 0; i < lstboxstock.Items.Count; i++)
                    //{
                    //    chkStr1 = lstboxstock.Items[i].ToString();
                    //    if (txtStock.Text.Length <= chkStr1.Length)
                    //    {
                    //        chkstr2 = chkStr1.Substring(0, txtStock.Text.Length);
                    //        bool isChk = false;
                    //        if (txtStock.Text.Trim() == chkstr2 || txtStock.Text.Trim() == chkstr2.ToLower())
                    //        {
                    //            isChk = true;
                    //            lstboxstock.SetSelected(i, true);
                    //            txtStock.Select();
                    //            chk = "1";
                    //            txtStock.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);

                    //            break;
                    //        }
                    //        if (isChk == false)
                    //        {
                    //            chk = "2";
                    //            txtStock.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        chk = "1";
                    //    }
                    //}
                    // DataTable dt = dtStock.Rows.Select("Stock LIKE "+txtStock.Text+"%"+"").CopyToDataTable();
                    bool isChk = false;
                    DataRow[] dtRowChk = dtStock.Select("Stock Like '"+txtStock.Text+"%'");
                    for (int i = 0; i < dtRowChk.Length; i++)
                    {
                        string tempstr = dtRowChk[0][0].ToString();
                        for (int k = 0; k < lstboxstock.Items.Count; k++)
                        {
                            isChk = true;
                            if (tempstr == lstboxstock.Items[k].ToString())
                            {
                                lstboxstock.SetSelected(k, true);
                                txtStock.Select();
                                chk = "1";
                                txtStock.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                        
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtStock.Text != "")
                        {
                            string name = txtStock.Text.Remove(txtStock.Text.Length - 1);
                            txtStock.Text = name.ToString();
                            txtStock.Select(txtStock.Text.Length, 0);
                        }
                        txtStock.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                        txtStock.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                    txtName_TextChanged(sender, e);
                }
               
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
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
        private void txtLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtLevel.Text.Trim() != null && txtLevel.Text.Trim() != "")
                {
                    //for (int i = 0; i < lstboxstock.Items.Count; i++)
                    //{
                    //    chkStr1 = lstboxstock.Items[i].ToString();
                    //    if (txtLevel.Text.Length <= chkStr1.Length)
                    //    {
                    //        chkstr2 = chkStr1.Substring(0, txtLevel.Text.Length);
                    //        bool isChk = false;
                    //        if (txtLevel.Text.Trim() == chkstr2 || txtLevel.Text.Trim() == chkstr2.ToLower())
                    //        {
                    //            isChk = true;
                    //            lstboxstock.SetSelected(i, true);
                    //            txtLevel.Select();
                    //            chk = "1";
                    //            txtLevel.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                    //            break;
                    //        }
                    //        if (isChk == false)
                    //        {
                    //            chk = "2";
                    //            txtLevel.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                    //        }
                    //    }
                    //}
                     bool isChk = false;
                    DataRow[] dtRowChk = dtLevel.Select("Level Like '"+txtLevel.Text+"%'");
                    for (int i = 0; i < dtRowChk.Length; i++)
                    {
                        string tempstr = dtRowChk[0][0].ToString();
                        for (int k = 0; k < lstboxstock.Items.Count; k++)
                        {
                            isChk = true;
                            if (tempstr == lstboxstock.Items[k].ToString())
                            {
                                lstboxstock.SetSelected(k, true);
                                txtLevel.Select();
                                chk = "1";
                                txtLevel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                        break;
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtStock.Text != "")
                        {
                            string name = txtLevel.Text.Remove(txtLevel.Text.Length - 1);
                            txtLevel.Text = name.ToString();
                            txtLevel.Select(txtLevel.Text.Length, 0);
                        }
                        txtLevel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                        txtLevel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                    txtName_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtMoement_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMoement.Text.Trim() != null && txtMoement.Text.Trim() != "")
                {
                    //for (int i = 0; i < lstboxstock.Items.Count; i++)
                    //{
                    //    chkStr1 = lstboxstock.Items[i].ToString();
                    //    if (txtMoement.Text.Length <= chkStr1.Length)
                    //    {
                    //        chkstr2 = chkStr1.Substring(0, txtMoement.Text.Length);
                    //        bool isChk = false;
                    //        if (txtMoement.Text.Trim() == chkstr2 || txtMoement.Text.Trim() == chkstr2.ToLower())
                    //        {
                    //            isChk = true;
                    //            lstboxstock.SetSelected(i, true);
                    //            txtMoement.Select();
                    //            chk = "1";
                    //            txtMoement.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);

                    //            break;
                    //        }
                    //        if (isChk == false)
                    //        {
                    //            chk = "2";
                    //            txtMoement.KeyPress += new KeyPressEventHandler(textBox2_press_KeyPress);
                    //        }
                    //    }
                    //}
                    bool isChk = false;
                    DataRow[] dtRowChk = dtMovements.Select("Movements Like '" + txtMoement.Text + "%'");
                    for (int i = 0; i < dtRowChk.Length; i++)
                    {
                        string tempstr = dtRowChk[0][0].ToString();
                        for (int k = 0; k < lstboxstock.Items.Count; k++)
                        {
                            isChk = true;
                            if (tempstr == lstboxstock.Items[k].ToString())
                            {
                                lstboxstock.SetSelected(k, true);
                                txtMoement.Select();
                                chk = "1";
                                txtMoement.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                        break;
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtMoement.Text != "")
                        {
                            string name = txtMoement.Text.Remove(txtMoement.Text.Length - 1);
                            txtMoement.Text = name.ToString();
                            txtMoement.Select(txtMoement.Text.Length, 0);
                        }
                        txtMoement.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                        txtMoement.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                    txtName_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtAliasName_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.LightBlue;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;

            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
        }

        private void txtRemark_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.LightBlue;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;

            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
            pnlStockItems.Visible = false;
        }

        private void txtPrice_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.LightBlue;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
        }
        private void txtPrice1_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.LightBlue;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
        }
        private void txtCost_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.LightBlue;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
           
        }

        private void txtCost1_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.LightBlue;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
     
        }

        private void txtMrp_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.LightBlue;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
    
        }

        private void txtMrp1_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.LightBlue;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
           
        }

        private void txtSpecial1_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.LightBlue;
            txtSpecial1_one.BackColor = Color.White;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
         
        }

        private void txtSpecial1_one_Enter(object sender, EventArgs e)
        {
            txtName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txt_Model.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtLevel.BackColor = Color.White;
            txtMoement.BackColor = Color.White;
            txtActive.BackColor = Color.White;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtAliasName.BackColor = Color.White;
            txtRemark.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtMrp.BackColor = Color.White;


            txtPrice1.BackColor = Color.White;
            txtCost1.BackColor = Color.White;
            txtMrp1.BackColor = Color.White;
            txtSpecial1.BackColor = Color.White;
            txtSpecial1_one.BackColor = Color.LightBlue;
            // lbllist.Visible = false;
            lstboxstock.Visible = false;
            pnlStockItems.Visible = false;
        }
        string DateselectionChanged = "";
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            DateselectionChanged = "1";
            passingvalues.DateSelectChanged = DateselectionChanged;
        }
        private void lvitems_Click(object sender, EventArgs e)
        {
            if(listActionType.Equals("Group"))
            {
                if (lvitems.Items.Count > 0)
                {
                    txtGroup.Text = lvitems.SelectedItem.ToString();
                    txtGroup.Focus();
                }
            }
            else if(listActionType.Equals("Model"))
            {
                if (lvitems.Items.Count > 0)
                {
                    txt_Model.Text = lvitems.SelectedItem.ToString();
                    txt_Model.Focus();
                }
            }
            else if( listActionType.Equals("Brand"))
            {
                if (lvitems.Items.Count > 0)
                {
                    txtBrand.Text = lvitems.SelectedItem.ToString();
                    txtBrand.Focus();
                }
            }
            else if( listActionType.Equals("Unit"))
            {
                if (lvitems.Items.Count > 0)
                {
                    txtUnit.Text = lvitems.SelectedItem.ToString();
                    txtUnit.Focus();
                }
            }
        }
        private void lstboxstock_Click(object sender, EventArgs e)
        {
            if(listtype.Equals("Stock"))
            {
                if (lstboxstock.Items.Count > 0)
                {
                    txtStock.Text = lstboxstock.SelectedItem.ToString();
                    txtStock.Focus();
                }
            }
            else if(listtype.Equals("Level"))
            {
                if (lstboxstock.Items.Count > 0)
                {
                    txtLevel.Text = lstboxstock.SelectedItem.ToString();
                    txtLevel.Focus();
                }
            }
            else if( listtype.Equals("Movement"))
            {
                if (lstboxstock.Items.Count > 0)
                {
                    txtMoement.Text = lstboxstock.SelectedItem.ToString();
                    txtMoement.Focus();
                }
            }
        }
    }
}
