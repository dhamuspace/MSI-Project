using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Management;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
namespace MSPOSBACKOFFICE
{
    public partial class FrmBarcodePrinter : Form
    {
        public FrmBarcodePrinter()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataAdapter adp = null;
        DataTable dtTable = new System.Data.DataTable();
        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            funItemCode();
        }
        public void funItemCode()
        {
            try
            {
                if (txtItemCode.Text.Trim() != "")
                {
                    dtTable.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "CheckItemcode");
                    cmd.Parameters.AddWithValue("@ItemCode", txtItemCode.Text.Trim() == "" ? txtItemName.Text.Trim() : txtItemCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@ItemName", "");
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtTable);
                    if (dtTable.Rows.Count > 0)
                    {
                        assignValues();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("SP_SelectQuery", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@ActionType", "BarcodePrintSelect");
                        cmd1.Parameters.AddWithValue("@ItemCode", txtItemCode.Text.Trim());
                        cmd1.Parameters.AddWithValue("@ItemName", "");
                        adp = new SqlDataAdapter(cmd1);
                        adp.Fill(dtTable);
                        if (dtTable.Rows.Count > 0)
                        {
                            assignValues();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void assignValues()
        {
            try
            {
                txtItemCode.Text = dtTable.Rows[0]["item_code"].ToString();
                if (tChk != "Exist")
                {
                    tChk = "";
                    txtItemName.Text = dtTable.Rows[0]["item_name"].ToString();
                }
                txtItemPrintName.Text = dtTable.Rows[0]["Item_Printname"].ToString();
                txtPrintRate.Text = string.Format("{0:0.00}", (dtTable.Rows[0]["Item_ndp"].ToString() == "") ? 0.00 : double.Parse(dtTable.Rows[0]["Item_ndp"].ToString()));
                txtCost.Text = string.Format("{0:0.00}", (dtTable.Rows[0]["item_cost"].ToString() == "") ? 0.00 : double.Parse(dtTable.Rows[0]["item_cost"].ToString()));
                txtMrp.Text = string.Format("{0:0.00}", (dtTable.Rows[0]["item_mrsp"].ToString() == "") ? 0.00 : double.Parse(dtTable.Rows[0]["item_mrsp"].ToString()));
                txtSpecial_1.Text = string.Format("{0:0.00}", (dtTable.Rows[0]["item_special1"].ToString() == "") ? 0.00 : double.Parse(dtTable.Rows[0]["item_special1"].ToString()));
                txtUnitName.Text = dtTable.Rows[0]["unit_name"].ToString();
                txtUnitRate.Text = string.Format("{0:0.00}", (dtTable.Rows[0]["item_mrsp"].ToString() == "") ? 0.00 : double.Parse(dtTable.Rows[0]["item_cost"].ToString()));
                panel1.Visible = false;
               // listitems.Visible = false;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void txtPkdDate_Leave(object sender, EventArgs e)
        {
            DateTime date;
            if(!DateTime.TryParseExact("11-Nov-13", new[] { "yyyy-MM-dd", "MM/dd/yy", "dd-MMM-yy", "yyyyMMdd", "MMddyy", "ddMMMyy" }, null, System.Globalization.DateTimeStyles.None, out date))
            {
                MyMessageBox.ShowBox("Not Valid Date","Warning");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoLabels.Text.Trim() != "")
                {
                    if (txtItemName.Text.Trim() != "" && cmbPrinter.Text.Trim() != "")
                    {
                       // string Comp_Name = "", Item_Code = "", Barcode = "", Item_Name = "", Item_Alias = "", Item_Alias1 = "", Item_Alias2 = "", Item_Alias3 = "", ItemPrint_Name = "", ItemPrint_Name1 = "", ItemPrint_Name2 = "", Item_NameNew = "", Item_Name1 = "", Item_Name2 = "", Unit = "", Item_Ndp = "", Item_Cost = "", Item_Mrsp = "", Item_Special1 = "", Item_Special2 = "", Item_Special3 = "", No_Of_Labels = "", Item_Group = "", Item_Model = "", Item_Brand = "", End_of_Column = "", Ndp_Code = "", Cost_Code = "", Mrsp_Code = "", Special1_Code = "", Special2_Code = "", Special3_Code = "", Ndp_Tax_Code = "", Cost_Tax_Code = "", Mrsp_Tax_Code = "", Special1_Tax_Code = "", Special2_Tax_Code = "", Special3_Tax_Code = "", Supplier = "", Ledger_Alias = "", Invoice_No = "", Purchase_Date = "", Ndp_Tax = "", Cost_Tax = "", Mrsp_Tax = "", Special1_Tax = "", Special2_Tax = "", Special3_Tax = "", Month = "", Year = "", Remarks = "", Conv_Unit = "", Conv_Rate = "", Item_Rack = "", PKD_Date = "", EXP_Date = "", Date = "", Time = "";
                        string Comp_Name = "", Item_Code = "", Barcode = "", Item_Name = "", Item_Alias = "", Item_Alias1 = "", Item_Alias2 = "", Item_Alias3 = "", ItemPrint_Name = "", ItemPrint_Name1 = "", ItemPrint_Name2 = "", Item_NameNew = "", Item_Name1 = "", Item_Name2 = "", Unit = "", Item_Ndp = "", Item_Cost = "", Item_Mrsp = "", Item_Special1 = "", Item_Special2 = "", Item_Special3 = "", No_Of_Labels = "", Month = "", Year = "", PKD_Date = "", EXP_Date = "", Date = "", Time = "";
                        string[] arrayCondition ={"&Comp Name&",
"&Item Code&",
"&Barcode&",
"&Item Name&",
"&Item Alias&",
"&Item Alias - 1&",
"&Item Alias - 2&",
"&Item Alias - 3&",
"&ItemPrint Name&",
"&ItemPrint Name - 1&",
"&ItemPrint Name - 2&",
"&Item Name&",
"&Item Name - 1&",
"&Item Name - 2&",
"&Unit&",
"&Item_Ndp&",
"&Item_Cost&",
"&Item_Mrsp&",
"&Item_Special1&",
"&Item_Special2&",
"&Item_Special3&",
"&No Of Labels&",
"&Item Group&",
"&Item Model&",
"&Item Brand&",
"&End of Column&",
"&Ndp_Code&",
"&Cost_Code&",
"&Mrsp_Code&",
"&Special1_Code&",
"&Special2_Code&",
"&Special3_Code&",
"&Ndp+Tax_Code&",
"&Cost+Tax_Code&",
"&Mrsp+Tax_Code&",
"&Special1+Tax_Code&",
"&Special2+Tax_Code&",
"&Special3+Tax_Code&",
"&Supplier&",
"&Ledger Alias&",
"&Invoice No&",
"&Purchase_Date&",
"&Ndp+Tax&",
"&Cost+Tax&",
"&Mrsp+Tax&",
"&Special1+Tax&",
"&Special2+Tax&",
"&Special3+Tax&",
"&Month&",
"&Year&",
"&Remarks&",
"&Conv Unit&",
"&Conv Rate&",
"&Item Rack&",
"&PKD. Date&",
"&EXP. Date&",
"&Date&",
"&Time&"};
                        StringBuilder sb = new StringBuilder();
                        
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }


                        DataTable dtItem = new System.Data.DataTable();
                        dtItem.Rows.Clear();
                        SqlCommand cmdItem = new SqlCommand(@"Select item_table.Item_code,item_table.Item_name,Item_Grouptable.Item_groupname,Model_table.Model_name, Brand_table.Brand_name,Tax_table.Tax_name,Tax_table.tax_percent, 
Item_table.Item_ndp, Item_table.Item_cost, Item_table.Item_mrsp, Item_table.Item_special1,Item_table.Item_special2,Item_table.Item_special3,Item_table.Item_Remarks
from item_table,Item_Grouptable,Model_table,Brand_table, Tax_table where Tax_table.Tax_no=Item_table.Tax_no and Brand_table.Brand_no=Item_table.Brand_no and Model_table.Model_no=Item_table.Model_no and Item_Grouptable.Item_groupno=Item_table.item_Groupno and  Item_table.Item_name=@tItemName", con);
                        cmdItem.Parameters.AddWithValue("@tItemName", txtItemName.Text.Trim());
                        SqlDataAdapter adpItem = new SqlDataAdapter(cmdItem);
                        adpItem.Fill(dtItem);
                        DataTable dtCompany = new System.Data.DataTable();
                        dtCompany.Rows.Clear();
                        SqlCommand cmdCompany = new SqlCommand("Select * from company_table", con);
                        SqlDataAdapter adpCompany = new SqlDataAdapter(cmdCompany);
                        adpCompany.Fill(dtCompany);



                        DataTable dtBarcode = new System.Data.DataTable();
                        dtBarcode.Rows.Clear();
                        SqlCommand cmdBarcode = new SqlCommand("select * from BarCode_table where Item_No=(select Item_No from Item_table where Item_name=@tItemName)", con);
                        cmdBarcode.Parameters.AddWithValue("@tItemName", txtItemName.Text.Trim());
                        SqlDataAdapter adpBarcode = new SqlDataAdapter(cmdBarcode);
                        adpBarcode.Fill(dtBarcode);

                        SqlCommand cmd = new SqlCommand("Select * from Control_table", con);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt = new System.Data.DataTable();
                        adp.Fill(dt);
                        string totalstring = "";
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["BarcodeCoding"].ToString() != "")
                            {
                                totalstring = dt.Rows[0]["BarcodeCoding"].ToString();

                                int countNew = TextTool.CountStringOccurrences(totalstring, "<xpml>");
                                // totalstring = "<Parthi" + totalstring;
                                if (countNew == 0)
                                {
                                    string[] tCodeImage = totalstring.Split('\n');
                                    String tArrayValue = "";
                                    for (int mn = 0; mn < tCodeImage.Length; mn++)
                                    {
                                        if (tCodeImage[mn] != "")
                                        {
                                            bool isChk = false;
                                            for (int k = 0; k < arrayCondition.Length; k++)
                                            {
                                                tArrayValue = "";
                                                int tCondition = tCodeImage[mn].IndexOf(arrayCondition[k].ToString());
                                                if (tCondition != -1)
                                                {
                                                    tArrayValue = arrayCondition[k].ToString();
                                                    isChk = true;
                                                    break;
                                                }

                                            }
                                            // int tCondition = tCodeImage[mn].IndexOf(arrayCondition[k].ToString());
                                            string tCommonStr = "";
                                            // if (tCondition == -1)
                                            if (isChk == false)
                                            {
                                                sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn]));
                                                //break;
                                            }
                                            else
                                            {

                                                if (tArrayValue == "&Comp Name&")
                                                {
                                                    Comp_Name = dtCompany.Rows[0]["Comp_name"].ToString();
                                                    tCommonStr = Comp_Name;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Code&")
                                                {
                                                    Item_Code = dtItem.Rows[0]["Item_Code"].ToString();
                                                    tCommonStr = Item_Code;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Barcode&")
                                                {
                                                    Barcode = dtBarcode.Rows[0]["BarCode"].ToString();
                                                    tCommonStr = Barcode;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name&")
                                                {
                                                    Item_Name = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Name;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias&")
                                                {
                                                    Item_Alias = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias - 1&")
                                                {
                                                    Item_Alias1 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias - 2&")
                                                {
                                                    Item_Alias2 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias - 3&")
                                                {
                                                    Item_Alias3 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias3;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&ItemPrint Name&")
                                                {
                                                    ItemPrint_Name = Item_Alias = txtItemPrintName.Text.Trim();
                                                    tCommonStr = ItemPrint_Name;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&ItemPrint Name - 1&")
                                                {
                                                    ItemPrint_Name1 = Item_Alias = txtItemPrintName.Text.Trim();
                                                    tCommonStr = ItemPrint_Name1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&ItemPrint Name - 2&")
                                                {
                                                    ItemPrint_Name2 = Item_Alias = txtItemPrintName.Text.Trim();
                                                    tCommonStr = ItemPrint_Name2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name&")
                                                {
                                                    Item_NameNew = txtItemName.Text.Trim();
                                                    tCommonStr = Item_NameNew;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name - 1&")
                                                {
                                                    Item_Name1 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Name1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name - 2&")
                                                {
                                                    Item_Name2 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Name2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Unit&")
                                                {
                                                    Unit = txtUnitName.Text.Trim();
                                                    tCommonStr = Unit;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Ndp&")
                                                {
                                                    Item_Ndp = txtPrintRate.Text.Trim();
                                                    tCommonStr = Item_Ndp;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Cost&")
                                                {
                                                    Item_Cost = txtCost.Text.Trim();
                                                    tCommonStr = Item_Cost;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Mrsp&")
                                                {
                                                    Item_Mrsp = txtMrp.Text.Trim();
                                                    tCommonStr = Item_Mrsp;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Special1&")
                                                {
                                                    Item_Special1 = txtSpecial_1.Text.Trim();
                                                    tCommonStr = Item_Special1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Special2&")
                                                {
                                                    Item_Special2 = txtSpecial_1.Text.Trim();
                                                    tCommonStr = Item_Special2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Special3&")
                                                {
                                                    Item_Special3 = txtSpecial_1.Text.Trim();
                                                    tCommonStr = Item_Special3;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&No Of Labels&")
                                                {
                                                    No_Of_Labels = txtNoLabels.Text.Trim();
                                                    tCommonStr = No_Of_Labels;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Group&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_groupname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Model&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Model_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Brand&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Brand_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&End of Column&") { }
                                                if (tArrayValue == "&Ndp_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_ndp"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Cost_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_cost"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Mrsp_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_mrsp"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special1_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special1"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special2_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special2"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special3_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special3"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Ndp+Tax_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_ndp"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Cost+Tax_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_cost"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Mrsp+Tax_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_mrsp"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special1+Tax_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special1"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special2+Tax_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special2"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special3+Tax_Code&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special3"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Supplier&") { }
                                                if (tArrayValue == "&Ledger Alias&") { }
                                                if (tArrayValue == "&Invoice No&") { }
                                                if (tArrayValue == "&Purchase_Date&") { }
                                                if (tArrayValue == "&Ndp+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_ndp"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Cost+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_cost"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }

                                                if (tArrayValue == "&Mrsp+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_mrsp"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special1+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special1"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special2+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special2"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special3+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_special3"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Month&")
                                                {
                                                    Month = DateTime.Now.Month.ToString();
                                                    tCommonStr = Month;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));

                                                }
                                                if (tArrayValue == "&Year&")
                                                {
                                                    Year = DateTime.Now.Year.ToString();
                                                    tCommonStr = Year;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));

                                                }
                                                if (tArrayValue == "&Remarks&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_Remarks"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Conv Unit&") { }
                                                if (tArrayValue == "&Conv Rate&") { }
                                                if (tArrayValue == "&Item Rack&") { }
                                                if (tArrayValue == "&PKD. Date&")
                                                {
                                                    PKD_Date = txtPkdDate.Text.Trim();
                                                    tCommonStr = PKD_Date;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));

                                                }
                                                if (tArrayValue == "&EXP. Date&")
                                                {
                                                    EXP_Date = txtExpDate.Text.Trim();
                                                    tCommonStr = EXP_Date;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Date&")
                                                {
                                                    Date = DateTime.Now.Date.ToString();
                                                    tCommonStr = Date;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));

                                                }
                                                if (tArrayValue == "&Time&")
                                                {
                                                    Time = DateTime.Now.TimeOfDay.ToString();
                                                    tCommonStr = Time;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                // MessageBox.Show(tCodeImage[mn].Replace(arrayCondition[k].ToString(), tCommonStr));
                                                // sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(arrayCondition[k].ToString(), tCommonStr)));
                                                // break;
                                            }
                                        }
                                        //break;
                                    }
                                }

                                for (int ij = 0; ij < countNew; ij++)
                                {

                                    int index1 = totalstring.IndexOf("<xpml>");
                                    int index2 = totalstring.IndexOf("</xpml>");


                                    //  int index1 = totalstring.IndexOf('<');
                                    // int index2 = totalstring.IndexOf('>');
                                    totalstring = totalstring.Remove(index1, index2 + 7);
                                    // tSubString=tSubString.Remove(tSubString);
                                    int index3 = totalstring.IndexOf("<xpml>");
                                    string tSubString2 = "";
                                    if (index3 != -1)
                                    {
                                        tSubString2 = totalstring.Substring(0, index3);
                                    }
                                    else
                                    {
                                        tSubString2 = totalstring;
                                    }
                                    totalstring = totalstring.Remove(0, tSubString2.Length);
                                    string[] tCodeImage = tSubString2.Split('\n');
                                    String tArrayValue = "";
                                    for (int mn = 0; mn < tCodeImage.Length; mn++)
                                    {
                                        if (tCodeImage[mn] != "")
                                        {
                                            bool isChk = false;
                                            for (int k = 0; k < arrayCondition.Length; k++)
                                            {
                                                tArrayValue = "";
                                                int tCondition = tCodeImage[mn].IndexOf(arrayCondition[k].ToString());
                                                if (tCondition != -1)
                                                {
                                                    tArrayValue = arrayCondition[k].ToString();
                                                    isChk = true;
                                                    break;
                                                }

                                            }
                                            // int tCondition = tCodeImage[mn].IndexOf(arrayCondition[k].ToString());
                                            string tCommonStr = "";
                                            // if (tCondition == -1)
                                            if (isChk == false)
                                            {
                                                sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn]));
                                                //break;
                                            }
                                            else
                                            {

                                                if (tArrayValue == "&Comp Name&")
                                                {
                                                    Comp_Name = dtCompany.Rows[0]["Comp_name"].ToString();
                                                    tCommonStr = Comp_Name;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Code&")
                                                {
                                                    Item_Code = dtItem.Rows[0]["Item_Code"].ToString();
                                                    tCommonStr = Item_Code;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Barcode&")
                                                {
                                                    Barcode = dtBarcode.Rows[0]["BarCode"].ToString();
                                                    tCommonStr = Barcode;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name&")
                                                {
                                                    Item_Name = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Name;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias&")
                                                {
                                                    Item_Alias = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias - 1&")
                                                {
                                                    Item_Alias1 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias - 2&")
                                                {
                                                    Item_Alias2 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Alias - 3&")
                                                {
                                                    Item_Alias3 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Alias3;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&ItemPrint Name&")
                                                {
                                                    ItemPrint_Name = Item_Alias = txtItemPrintName.Text.Trim();
                                                    tCommonStr = ItemPrint_Name;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&ItemPrint Name - 1&")
                                                {
                                                    ItemPrint_Name1 = Item_Alias = txtItemPrintName.Text.Trim();
                                                    tCommonStr = ItemPrint_Name1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&ItemPrint Name - 2&")
                                                {
                                                    ItemPrint_Name2 = Item_Alias = txtItemPrintName.Text.Trim();
                                                    tCommonStr = ItemPrint_Name2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name&")
                                                {
                                                    Item_NameNew = txtItemName.Text.Trim();
                                                    tCommonStr = Item_NameNew;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name - 1&")
                                                {
                                                    Item_Name1 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Name1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Name - 2&")
                                                {
                                                    Item_Name2 = txtItemName.Text.Trim();
                                                    tCommonStr = Item_Name2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Unit&")
                                                {
                                                    Unit = txtUnitName.Text.Trim();
                                                    tCommonStr = Unit;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Ndp&")
                                                {
                                                    Item_Ndp = txtPrintRate.Text.Trim();
                                                    tCommonStr = Item_Ndp;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Cost&")
                                                {
                                                    Item_Cost = txtCost.Text.Trim();
                                                    tCommonStr = Item_Cost;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Mrsp&")
                                                {
                                                    Item_Mrsp = txtMrp.Text.Trim();
                                                    tCommonStr = Item_Mrsp;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Special1&")
                                                {
                                                    Item_Special1 = txtSpecial_1.Text.Trim();
                                                    tCommonStr = Item_Special1;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Special2&")
                                                {
                                                    Item_Special2 = txtSpecial_1.Text.Trim();
                                                    tCommonStr = Item_Special2;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item_Special3&")
                                                {
                                                    Item_Special3 = txtSpecial_1.Text.Trim();
                                                    tCommonStr = Item_Special3;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&No Of Labels&")
                                                {
                                                    No_Of_Labels = txtNoLabels.Text.Trim();
                                                    tCommonStr = No_Of_Labels;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Group&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_groupname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Model&") {
                                                    tCommonStr = dtItem.Rows[0]["Model_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Item Brand&") 
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Brand_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&End of Column&") { }
                                                if (tArrayValue == "&Ndp_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_ndp"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Cost_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_cost"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Mrsp_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_mrsp"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special1_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special1"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special2_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special2"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special3_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special3"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Ndp+Tax_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_ndp"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Cost+Tax_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_cost"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Mrsp+Tax_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_mrsp"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special1+Tax_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special1"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special2+Tax_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special2"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special3+Tax_Code&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special3"].ToString() + "+" + dtItem.Rows[0]["Tax_name"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Supplier&") { }
                                                if (tArrayValue == "&Ledger Alias&") { }
                                                if (tArrayValue == "&Invoice No&") { }
                                                if (tArrayValue == "&Purchase_Date&") { }
                                                if (tArrayValue == "&Ndp+Tax&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_ndp"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Cost+Tax&")
                                                {
                                                    tCommonStr = dtItem.Rows[0]["Item_cost"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }

                                                if (tArrayValue == "&Mrsp+Tax&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_mrsp"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special1+Tax&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special1"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special2+Tax&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special2"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Special3+Tax&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_special3"].ToString() + "+" + dtItem.Rows[0]["Tax_tax_percentname"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Month&")
                                                {
                                                   Month = DateTime.Now.Month.ToString();
                                                   tCommonStr = Month;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                
                                                }
                                                if (tArrayValue == "&Year&") {
                                                    Year = DateTime.Now.Year.ToString();
                                                    tCommonStr = Year;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                
                                                }
                                                if (tArrayValue == "&Remarks&") {
                                                    tCommonStr = dtItem.Rows[0]["Item_Remarks"].ToString();
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Conv Unit&") { }
                                                if (tArrayValue == "&Conv Rate&") { }
                                                if (tArrayValue == "&Item Rack&") { }
                                                if (tArrayValue == "&PKD. Date&") {
                                                   PKD_Date =txtPkdDate.Text.Trim();
                                                   tCommonStr = PKD_Date;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                
                                                }
                                                if (tArrayValue == "&EXP. Date&") {
                                                   EXP_Date =txtExpDate.Text.Trim();
                                                   tCommonStr = EXP_Date;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                if (tArrayValue == "&Date&") {
                                                   Date =DateTime.Now.Date.ToString();
                                                   tCommonStr = Date;
                                                   sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                
                                                }
                                                if (tArrayValue == "&Time&") {
                                                  Time = DateTime.Now.TimeOfDay.ToString();
                                                    tCommonStr =Time;
                                                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(tArrayValue, tCommonStr)));
                                                }
                                                // MessageBox.Show(tCodeImage[mn].Replace(arrayCondition[k].ToString(), tCommonStr));
                                                // sb.AppendLine(string.Format(CultureInfo.InvariantCulture, tCodeImage[mn].Replace(arrayCondition[k].ToString(), tCommonStr)));
                                                // break;
                                            }
                                        }
                                        //break;
                                    }
                                }
                                // totalstring = tSubString;
                            }
                            double tNooflabel=0;
                            if (txtNoLabels.Text.Trim() != "")
                            {
                                tNooflabel = (txtNoLabels.Text.Trim() == "") ? 0 : double.Parse(txtNoLabels.Text.Trim());
                            }
                            for (int i = 0; i < tNooflabel; i++)
                            {
                                RawPrinterHelper.SendStringToPrinter(cmbPrinter.Text.Trim(), sb.ToString());
                            }

                            //string input = "test Code, and test <<Item Code>> not testing.  But yes to test";
                            //string pattern = @"\bItem Code\b";
                            //string replace = "text";
                            // string result =  Regex.Replace(input, pattern, replace);
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Enter Item Name or Select Printer", "Warning");
                    }

                }
                else
                {
                    MyMessageBox.ShowBox("Enter number of labels", "Warning");
                }
                       
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrintTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (txtPrintTo.Text.Trim() == "Printer")
                    {
                        txtPrintTo.Text = "File";
                        cmbPrinter.DropDownStyle = ComboBoxStyle.Simple;
                        cmbPrinter.Text = "C:\barcode.txt";
                    }
                    else
                    {
                        txtPrintTo.Text = "Printer";
                        cmbPrinter.Text = "";
                        cmbPrinter.DropDownStyle = ComboBoxStyle.DropDown;
                        cmbPrinter.Items.Clear();
                        foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                        {
                            cmbPrinter.Items.Add(printer.ToString());
                        }
                        
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                  cmbPrinter.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        DataTable dt = new DataTable();
        private void FrmBarcodePrinter_Load(object sender, EventArgs e)
        {
            try
            {
                txtPrintTo.Text = "Printer";
                cmbPrinter.Items.Clear();
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    cmbPrinter.Items.Add(printer.ToString());
                }


                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(" select item_selname from item_seltable  order by item_selname ASC", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                adp.Fill(dt);
                //  int j = 0;
                if (dt.Rows.Count > 0)
                {
                    listitems.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        listitems.Items.Add(dt.Rows[i]["item_selname"].ToString());
                    }
                }

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                //Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
                txtItemCode.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }

        }

        private void txtLbsFormate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (txtLbsFormate.Text.Trim() == "Barcode1")
                    {
                     txtLbsFormate.Text = "Barcode2";                       
                    }
                    else
                    {
                      txtLbsFormate.Text = "Barcode1";                       
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    txtPrintTo.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                funItemCode();
                txtItemName.Select();
                //panel1.Visible = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                panel1.Visible = false;
            }
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txtItemPrintName.Select();
            }
        }

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
                    checkvaluestotextbox();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                //if (listActionType == "Group")
                {
                    panel1.Visible = false;
                }
            }
        }
        string tChk = "";
        public void checkvaluestotextbox()
        {
            try
            {
                if (listitems.Visible == true)
                {
                    tChk = "Exist";
                    string tItemNameChk=listitems.SelectedItem.ToString();
                    DataTable dtChk=new System.Data.DataTable();
                    dtChk.Rows.Clear();
                    SqlCommand cmdItemChk=new SqlCommand("select * from Item_table where Item_no=(select Item_no from Item_seltable where Item_selname=@tItemSelName)",con);
                    cmdItemChk.Parameters.AddWithValue("@tItemSelName",tItemNameChk);
                    SqlDataAdapter adpItemChk=new SqlDataAdapter(cmdItemChk);
                    adpItemChk.Fill(dtChk);
                    if(dtChk.Rows.Count>0)
                    {
                        txtItemName.Text = dtChk.Rows[0]["Item_Name"].ToString();
                    }
                  
                }
                panel1.Visible = false;
              //  listitems.Visible = false;
                txtItemName.Select();
                DataTable dt1 = new DataTable();
                if (txtItemName.Text != null)
                {
                    //if (txtItemName.Text.IndexOf("'") != -1)
                    //{
                    //    string name = txtItemName.Text.Replace("'", "''");

                    //    SqlCommand cmd = new SqlCommand("select distinct item_no from item_seltable where item_selname='" + name + "'", con);
                    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //    dt.Rows.Clear();

                    //    adp.Fill(dt1);
                    //}
                    //else
                    //{
                    //    SqlCommand cmd = new SqlCommand("select distinct item_no from item_seltable where item_selname='" + txtItemName.Text + "'", con);
                    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //    dt.Rows.Clear();

                    //    adp.Fill(dt1);
                    //}
                    dtTable.Rows.Clear();
                    SqlCommand cmd1 = new SqlCommand("SP_SelectQuery", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@ActionType", "BarcodePrintSelectName");
                    cmd1.Parameters.AddWithValue("@ItemCode", "");
                    cmd1.Parameters.AddWithValue("@ItemName", txtItemName.Text);
                    adp = new SqlDataAdapter(cmd1);
                    adp.Fill(dtTable);
                    if (dtTable.Rows.Count > 0)
                    {
                        assignValues();
                    }
                    txtItemPrintName.Select();
                }
            }
            catch
            { }
        }
        private void txtItemPrintName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtPrintRate.Select();
            }
        }

        private void txtPrintRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {             
             txtCost.Select();
            }
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtMrp.Select();
            }
        }

        private void txtMrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtSpecial_1.Select();
            }
        }

        private void txtSpecial_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtUnitName.Select();
            }
        }

        private void txtUnitName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtUnitRate.Select();
            }
        }

        private void txtUnitRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtPkdDate.Select();
            }
        }

        private void txtPkdDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtExpDate.Select();
            }
        }

        private void txtExpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtNoLabels.Select();
            }
        }

        private void txtNoLabels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                 txtLbsFormate.Select();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCost.Text = "";
            txtExpDate.Text = "";
            txtItemCode.Text = "";
            txtItemName.Text = "";
            txtItemPrintName.Text = "";            
            txtMrp.Text = "";
            txtNoLabels.Text = "";
            txtPkdDate.Text = "";
            txtPrintRate.Text = "";
            txtSpecial_1.Text = "";
            txtUnitName.Text = "";
            txtUnitRate.Text = "";
            
        }
        bool isChk = false;
        string chk = "";
        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                panel1.Visible = true;
                label18.Visible = true;
                listitems.Visible = true;
                isChk = false;
                if (listActionType == "Group" && listActionType != null)
                {
                    if (txtItemName.Text.Trim() != null && txtItemName.Text.Trim() != "")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlDataAdapter adp = null;
                        DataTable dt_selectitem = new DataTable();
                        dt_selectitem.Rows.Clear();

                        adp = new SqlDataAdapter("select item_selname from item_seltable  where item_selname like '" + txtItemName.Text + "%'", con);
                        //SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@ActionType", "ItemSelNameChk");
                        //cmd.Parameters.AddWithValue("@ItemCode", "");
                        //cmd.Parameters.AddWithValue("@itemName",txtItemName.Text);
                        //adp = new SqlDataAdapter(cmd);
                        isChk = false;
                        adp.Fill(dt_selectitem);
                        //}
                        if (dt_selectitem.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_selectitem.Rows[0]["item_selname"].ToString().Trim();
                            for (int k = 0; k < listitems.Items.Count; k++)
                            {
                                if (tempstr == listitems.Items[k].ToString().Trim())
                                {
                                    listitems.SetSelected(k, true);
                                    txtItemName.Select();
                                    chk = "1";
                                    txtItemName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "2";
                            if (txtItemName.Text != "")
                            {
                                string name = txtItemName.Text.Remove(txtItemName.Text.Length - 1);
                                txtItemName.Text = name.ToString();
                                txtItemName.Select(txtItemName.Text.Length, 0);
                            }
                            txtItemName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            chk = "1";
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
                MyMessageBox.ShowBox(ex.Message,"Warning");
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
        string listActionType = "";
        private void txtItemName_Enter(object sender, EventArgs e)
        {
            listActionType = "Group";
            panel1.Visible = true;
        }

        private void txtPrintRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrintRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNoLabels_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listitems_Click(object sender, EventArgs e)
        {
            if (listitems.SelectedItems.Count>0)
            {
                if (listActionType == "Group")
                {
                    checkvaluestotextbox();
                }
            }
        }

       
    }

    public static class TextTool
    {
        /// <summary>
        /// Count occurrences of strings.
        /// </summary>
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }
     
}
