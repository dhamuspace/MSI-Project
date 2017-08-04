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
    public partial class ControlSettings : Form
    {
        public ControlSettings()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(@"Update Control_table set UpdatePurchaseRate=@PurchaseRate,UpdatePurActualCost=@PurchaseActualCost,
UpdateCost=@PurchaseCost,UpdateMrpRate=@MrpRate,UpdateSpecial1Rate=@SpecialRate1,UpdateSpecial2Rate=@SpecialRate2,
UpdateSpecial3Rate=@SpecialRate3,UpdatePurchaseRateGrn=@PurchaseRageGrn,UpdateCostGrn=@CostGrn,UpdateMrpRateGrn=@MrpRateGrn,
UpdateSpecial1RateGRN=@SpecialRate1Grn,UpdateSpecial2RateGRN=@SpecialRate2Grn,UpdateSpecial3RateGRN=@SpecialRate3Grn", con);
                cmd.Parameters.AddWithValue("@PurchaseRate", txtPurchaseRate.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@PurchaseActualCost", txtActualCost.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@PurchaseCost", txtCost.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@MrpRate", txtmrsp.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@SpecialRate1", txtSpecialRate_1.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@SpecialRate2", txtSpecialRate_2.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@SpecialRate3", txtSpecialRate_3.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@PurchaseRageGrn", txtPurchaseGrnRate.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@CostGrn", txtCostGrn.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@MrpRateGrn", txtMrspGrn.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@SpecialRate1Grn", txtSprcial_1Grn.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@SpecialRate2Grn", txtSprcial_2Grn.Text.Trim() == "No" ? false : true);
                cmd.Parameters.AddWithValue("@SpecialRate3Grn", txtSprcial_3Grn.Text.Trim() == "No" ? false : true);
                
                cmd.ExecuteNonQuery();

                //SqlCommand cmd1 = new SqlCommand("ReturnInSales,hide_keyboard,ctl_creditlimit,Ctl_FreeQty,DiscountLedger", con);
                //cmd1.ExecuteNonQuery();
                string chesales = "";
                SqlCommand cmd1 = new SqlCommand(@"Declare @tCtl_rsRate1 Numeric(18,2);
Select @tCtl_rsRate1=prlist_no from PriceMaster where Prlist_Name=@tCtl_rsRate;
Update Control_table set ReturnInSales=@ReturnInSales,hide_keyboard=@hide_keyboard,ctl_creditlimit=@ctl_creditlimit,Ctl_FreeQty=@Ctl_FreeQty,DiscountLedger=@DiscountLedger,ctl_rsRate=@tCtl_rsRate1, ctl_rstxRate=@tCtl_rsRate1", con);
                cmd1.Parameters.AddWithValue("@ReturnInSales", txtSalesReturn.Text.Trim()=="No"?false:true);
                cmd1.Parameters.AddWithValue("@hide_keyboard",(txtKeyboard.Text.Trim()=="Yes")?"True":"False");
                if (CmbControlCrLimit.Text == "Allow")
                {
                    chesales = "1";
                }
                else if (CmbControlCrLimit.Text == "Warning")
                {
                    chesales = "2";
                }
                else if (CmbControlCrLimit.Text == "Stop")
                {
                    chesales = "3";
                }

                cmd1.Parameters.AddWithValue("@ctl_creditlimit", chesales.ToString());
                cmd1.Parameters.AddWithValue("@Ctl_FreeQty", txtFreeQty.Text.Trim() == "No" ? false : true);
                cmd1.Parameters.AddWithValue("@DiscountLedger", txtDicountAllow.Text.Trim() == "No" ? false : true);
                cmd1.Parameters.AddWithValue("@tCtl_rsRate", cmbCtl_RSrate.Text);
                cmd1.ExecuteNonQuery();

                //Group Discount Settings:
                SqlCommand cmdUpdateControls = new SqlCommand("Update Control_table set GroupDiscounts=@DiscountType",con);
                cmdUpdateControls.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(CmbGroupDiscount.Text.Trim()) ? "None" : CmbGroupDiscount.Text.Trim());
                cmdUpdateControls.ExecuteNonQuery();

                //CommissionCharge Settings:
                SqlCommand cmdUpdatelabourCommiledger = new SqlCommand("Update Control_table set labourCommiledger=@labourCommiledger", con);
                cmdUpdatelabourCommiledger.Parameters.AddWithValue("@labourCommiledger", txtSalesCommission.Text.Trim() == "No" ? false : true);
                cmdUpdatelabourCommiledger.ExecuteNonQuery();

                //sales men settings
                SqlCommand cmdUpdateSalesmen = new SqlCommand("Update Control_table set Salesmen=@Salesmen", con);
                cmdUpdateSalesmen.Parameters.AddWithValue("@Salesmen", txtSalesperson.Text.Trim() == "No" ? false : true);
                cmdUpdateSalesmen.ExecuteNonQuery();
                //Payment note
                SqlCommand cmdNote = new SqlCommand("Update Control_table set Note=@Note", con);
                cmdNote.Parameters.AddWithValue("@Note", txtNote.Text.Trim());
                cmdNote.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.ToString(),"Warning");
            }
        }

        private void ControlSettings_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPrlistName = new DataTable();
                dtPrlistName.Rows.Clear();

                SqlCommand cmdPrice = new SqlCommand("Select prList_name from PriceMaster", con);
                SqlDataAdapter adpPrice = new SqlDataAdapter(cmdPrice);
                adpPrice.Fill(dtPrlistName);
                for (int m = 0; m < dtPrlistName.Rows.Count; m++)
                {
                    cmbCtl_RSrate.Items.Add(dtPrlistName.Rows[m]["prList_name"]);
                }

                SqlCommand cmd = new SqlCommand(@"select UpdatePurchaseRate,UpdatePurActualCost,UpdateCost,UpdateMrpRate,UpdateSpecial1Rate,UpdateSpecial2Rate,UpdateSpecial3Rate,UpdatePurchaseRateGRN,
            UpdateCostGRN,UpdateMrpRateGrn,UpdateSpecial1RateGRN,UpdateSpecial2RateGRN,UpdateSpecial3RateGRN,ReturnInSales,hide_keyboard,Ctl_FreeQty,DiscountLedger,ctl_creditlimit,PriceMaster.prList_name,Control_Table.GroupDiscounts,Control_Table.labourCommiledger,Control_Table.Salesmen,Control_Table.Note from Control_Table,PriceMaster where PriceMaster.PrList_no=Control_Table.Ctl_rsRate", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtPurchaseRate.Text = dt.Rows[0]["UpdatePurchaseRate"].ToString() == "True" ? "Yes" : "No";
                    txtActualCost.Text = dt.Rows[0]["UpdatePurActualCost"].ToString() == "True" ? "Yes" : "No";
                    txtCost.Text = dt.Rows[0]["UpdateCost"].ToString() == "True" ? "Yes" : "No";
                    txtmrsp.Text = dt.Rows[0]["UpdateMrpRate"].ToString() == "True" ? "Yes" : "No";
                    txtSpecialRate_1.Text = dt.Rows[0]["UpdateSpecial1Rate"].ToString() == "True" ? "Yes" : "No";
                    txtSpecialRate_2.Text = dt.Rows[0]["UpdateSpecial2Rate"].ToString() == "True" ? "Yes" : "No";
                    txtSpecialRate_3.Text = dt.Rows[0]["UpdateSpecial3Rate"].ToString() == "True" ? "Yes" : "No";
                    txtPurchaseGrnRate.Text = dt.Rows[0]["UpdatePurchaseRateGRN"].ToString() == "True" ? "Yes" : "No";
                    txtCostGrn.Text = dt.Rows[0]["UpdateCostGRN"].ToString() == "True" ? "Yes" : "No";
                    txtMrspGrn.Text = dt.Rows[0]["UpdateMrpRateGrn"].ToString() == "True" ? "Yes" : "No";
                    txtSprcial_1Grn.Text = dt.Rows[0]["UpdateSpecial1RateGRN"].ToString() == "True" ? "Yes" : "No";
                    txtSprcial_2Grn.Text = dt.Rows[0]["UpdateSpecial2RateGRN"].ToString() == "True" ? "Yes" : "No";
                    txtSprcial_3Grn.Text = dt.Rows[0]["UpdateSpecial3RateGRN"].ToString() == "True" ? "Yes" : "No";

                    txtSalesReturn.Text = dt.Rows[0]["ReturnInSales"].ToString() == "True" ? "Yes" : "No";
                    txtKeyboard.Text = dt.Rows[0]["hide_keyboard"].ToString() == "True" ? "Yes" : "No";
                    txtFreeQty.Text = dt.Rows[0]["Ctl_FreeQty"].ToString() == "True" ? "Yes" : "No";
                    txtDicountAllow.Text = dt.Rows[0]["DiscountLedger"].ToString() == "1" ? "Yes" : "No";
                    txtSalesCommission.Text = (dt.Rows[0]["labourCommiledger"].ToString() == "1" ||dt.Rows[0]["labourCommiledger"].ToString() == "True") ? "Yes" : "No";
                    txtSalesperson.Text = dt.Rows[0]["Salesmen"].ToString() == "1" ? "Yes" : "No";
                    if (dt.Rows[0]["Note"].ToString() == "NuLL" || dt.Rows[0]["Note"].ToString() == "")
                    {
                        txtNote.Text = "No";
                    }
                    else
                    {
                        txtNote.Text = dt.Rows[0]["Note"].ToString();
                    }

                    CmbGroupDiscount.Text = dt.Rows[0]["GroupDiscounts"].ToString();
                    if (dt.Rows[0]["ctl_creditlimit"].ToString() == "1")
                    {
                        CmbControlCrLimit.Text = "Allow";
                    }
                    else if (dt.Rows[0]["ctl_creditlimit"].ToString() == "2")
                    {
                        CmbControlCrLimit.Text = "Warning";
                    }
                    else if (dt.Rows[0]["ctl_creditlimit"].ToString() == "3")
                    {
                        CmbControlCrLimit.Text = "Stop";
                    }
                    cmbCtl_RSrate.Text = Convert.ToString(dt.Rows[0]["prList_name"]);

                    //For Color settings
                    _Class.clsVariables.Sheight_Width();
                    this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                    Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                    Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                    Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);  
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void txtPurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyCode == Keys.Space)
            {

                if (txt.Text == "No")
                {
                    txt.Text = "Yes";
                }
                else
                {
                    txt.Text = "No";
                }
            }
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGroupDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtGroupDiscount.Text.Trim() == "No")
                {
                    txtGroupDiscount.Text = "Yes";
                }
                else
                {
                    txtGroupDiscount.Text = "No";
                }
            }
        }      
        

        private void txtSalesReturn_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

           // if (e.KeyCode == Keys.Space)
            {

                if (txt.Text == "No")
                {
                    txt.Text = "Yes";
                }
                else
                {
                    txt.Text = "No";
                }
            }
        }
        private void txtSalesperson_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyCode == Keys.Space)
            {

                if (txt.Text == "No")
                {
                    txt.Text = "Yes";
                }
                else
                {
                    txt.Text = "No";
                }
            }
        }
        private void txtSalesperson_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            // if (e.KeyCode == Keys.Space)
            {
                if (txt.Text == "No")
                {
                    txt.Text = "Yes";
                }
                else
                {
                    txt.Text = "No";
                }
            }
        }

      

       

      
    }
}
