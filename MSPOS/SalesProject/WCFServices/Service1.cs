using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SalesProject.WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        public void btnCashButtonHome(string lblTotAmt, string lblNetAmt, string lblTaxAmt, string tUserNo, string tCounter, DataTable dt, string lblDiscount, string DiscountType, DataTable dtSingleFree, string tSmenNo, string tsmanRemarks, DataTable dtserial)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_btnCashSettleHome", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt));
                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt));
                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt));
                cmd.Parameters.AddWithValue("@tUserno", tUserNo);
                cmd.Parameters.AddWithValue("@tCounter", tCounter);
                double tot = (double.Parse(lblNetAmt) - ((double.Parse(lblTotAmt) + double.Parse(lblTaxAmt)) - double.Parse(lblDiscount)));
                cmd.Parameters.AddWithValue("@RoundValue", tot);
                for (int mnk = 0; mnk < dt.Rows.Count; mnk++)
                {
                    if (dt.Rows[mnk]["Disc"].ToString().Trim() == "")
                    {
                        dt.Rows[mnk]["Disc"] = "0.00";
                    }
                }
                cmd.Parameters.AddWithValue("@tempTable", dt);
                cmd.Parameters.AddWithValue("@tempSerialno", dtserial);
                if (double.Parse(lblDiscount) > 0)
                {
                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount));
                    cmd.Parameters.AddWithValue("@DiscountType", DiscountType);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount));
                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                }
                cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (tSmenNo != "")
                {
                    cmd.Parameters.AddWithValue("@tSmenNo", double.Parse(tSmenNo));
                } 
                else
                {
                    cmd.Parameters.AddWithValue("@tSmenNo", "0");
                }                
                cmd.Parameters.AddWithValue("@SmenRemarks",tsmanRemarks);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void btnNETSButtonHome(string lblTotAmt, string lblNetAmt, string lblTaxAmt, string tUserNo, string tCounter, DataTable dt, string lblDiscount, string DiscountType, DataTable dtSingleFree, string tSmenNo, string tsmanRemarks, DataTable dtserial)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_btnNETSSettleHome", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt));
                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt));
                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt));
                cmd.Parameters.AddWithValue("@tUserno", tUserNo);
                cmd.Parameters.AddWithValue("@tCounter", tCounter);
              //  double tot = ((double.Parse(lblNetAmt) - double.Parse(lblDiscount)) - (double.Parse(lblTotAmt) + double.Parse(lblTaxAmt)));
                double tot = (double.Parse(lblNetAmt) - ((double.Parse(lblTotAmt) + double.Parse(lblTaxAmt)) - double.Parse(lblDiscount)));
                cmd.Parameters.AddWithValue("@RoundValue", tot);
                for (int mnk = 0; mnk < dt.Rows.Count; mnk++)
                {
                    if (dt.Rows[mnk]["Disc"].ToString().Trim() == "")
                    {
                        dt.Rows[mnk]["Disc"] = "0.00";
                    }
                }
                cmd.Parameters.AddWithValue("@tempTable", dt);
                cmd.Parameters.AddWithValue("@tempSerialno", dtserial);
                if (double.Parse(lblDiscount) > 0)
                {
                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount));
                    cmd.Parameters.AddWithValue("@DiscountType", DiscountType);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount));
                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                    
                }
                cmd.Parameters.AddWithValue("@tempFreeItem",_Class.clsVariables.dtSingleFree);
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (tSmenNo != "")
                {
                    cmd.Parameters.AddWithValue("@tSmenNo", double.Parse(tSmenNo));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tSmenNo", "0");
                }
                cmd.Parameters.AddWithValue("@SmenRemarks", tsmanRemarks);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void btnSalesmen(string lblTotAmt, string lblNetAmt, string lblTaxAmt, string tUserNo, string tCounter, DataTable dt, string lblDiscount, string DiscountType, DataTable dtSingleFree)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_funSalesmen", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tGrossAmt", double.Parse(lblTotAmt));
                cmd.Parameters.AddWithValue("@tNetAmt", double.Parse(lblNetAmt));
                //cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount.Content.ToString()));
                cmd.Parameters.AddWithValue("@tTotTax", double.Parse(lblTaxAmt));
                cmd.Parameters.AddWithValue("@tUserno", tUserNo);
                cmd.Parameters.AddWithValue("@tCounter", tCounter);
                double tot = (double.Parse(lblNetAmt) - ((double.Parse(lblTotAmt) + double.Parse(lblTaxAmt)) - double.Parse(lblDiscount)));
                cmd.Parameters.AddWithValue("@RoundValue", tot);
                for (int mnk = 0; mnk < dt.Rows.Count; mnk++)
                {
                    if (dt.Rows[mnk]["Disc"].ToString().Trim() == "")
                    {
                        dt.Rows[mnk]["Disc"] = "0.00";
                    }
                }
                cmd.Parameters.AddWithValue("@tempTable", dt);
                if (double.Parse(lblDiscount) > 0)
                {
                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount));
                    cmd.Parameters.AddWithValue("@DiscountType", DiscountType);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tDiscount", double.Parse(lblDiscount));
                    cmd.Parameters.AddWithValue("@DiscountType", "NoDiscount");
                }
                cmd.Parameters.AddWithValue("@tempFreeItem", _Class.clsVariables.dtSingleFree);
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Parameters.AddWithValue("@tPartyno", _Class.clsVariables.tempsalesmenLedgerNo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}



