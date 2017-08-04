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
using System.Text.RegularExpressions;

namespace MSPOSBACKOFFICE
{
    public partial class frmTaxCreation : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public frmTaxCreation()
        {
            InitializeComponent();
        }
        string TaxName = string.Empty;

        private void txt_taxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_taxName.Text != "")
                {
                    txt_Value.Focus();
                }
                else
                {
                    MyMessageBox.ShowBox("Enter Tax Name","Message");
                }
            }
            else
            {
                //MyMessageBox.ShowBox("Enter Tax Name","Message");
            }
        }

        private void txt_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              txtPurchaseValues.Focus();
            }
        }
        SqlDataReader dr = null;
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_taxName.Text != string.Empty && txt_taxName.Text != "")
                {
                    if (btn_Save.Text == "Save")
                    {
                        SqlCommand cmd1 = new SqlCommand("Select * from Tax_table where Tax_name=@taxname", con);
                        cmd1.Parameters.AddWithValue("@taxname", txt_taxName.Text.Trim());
                        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MyMessageBox.ShowBox("Tax Name Already Exist", "Warning");
                        }
                        else
                        {
                            con.Close();
                            con.Open();
                            string taxQrySelect = "select max(TaxID)+1 from Numbertable";
                            SqlCommand cmdTax = new SqlCommand(taxQrySelect, con);
                            int Tax_no = Convert.ToInt32(cmdTax.ExecuteScalar());
                            con.Close();

                            string a = txt_taxName.Text;
                            string b = a.ToUpper();

                            string mystring = b;
                            mystring = mystring.Replace(" ", "");

                            con.Open();
                            string Taxqry = "insert into Tax_table (Tax_no,Tax_name,Tax_mtname,PEd_percent,PCess_percent,PSHECess_percent,Ed_percent,Cess_percent,SHECess_percent,tax_percent,CST_percent,Ptax_percent,PCST_percent,Sur_percent,PSur_percent,Nt_percent,Ledger_no,RetLedger_no,CSTSales_Ledger,CSTRet_Ledger,CSTTax_Ledger,CSTRetTax_Ledger,CSTPurc_Ledger,CSTPurcRet_Ledger,CSTPurcTax_Ledger,CSTPurcRetTax_Ledger,STPurc_Ledger ,STPurcRet_Ledger,STPurcTax_Ledger,STPurcRetTax_Ledger,NtRetLedger_no,NtLedger_no,SurRetLedger_no,SurLedger_no,Sales_no,NtSales_no,SalesRet_no,NtSalesRet_no,EDLedger_no,EDRetLedger_no,ECessLedger_no,ECessRetLedger_no,SHECessLedger_no,SHECessRetLedger_no) values(@Tax_no,@TaxName,@mystring,'0','0','0','0','0','0','0','0','0','0','0','0',@TaxValue,'0','0','10','10','10','10','10','10','10','10','10','10','10','10','13','13','0','0','0','14','0','14','0','0','0','0','0','0')";
                            SqlCommand cmdTaxInsert = new SqlCommand(Taxqry, con);
                            cmdTaxInsert.Parameters.AddWithValue("@Tax_no", Tax_no);
                            cmdTaxInsert.Parameters.AddWithValue("@TaxName", txt_taxName.Text.Trim());
                            cmdTaxInsert.Parameters.AddWithValue("@mystring", mystring);
                            cmdTaxInsert.Parameters.AddWithValue("@TaxValue", txt_Value.Text.Trim());
                            cmdTaxInsert.ExecuteNonQuery();
                            con.Close();


                            SqlCommand TaxQry = new SqlCommand("update NumberTable set TaxID=TaxID+1", con);
                            con.Close();
                            con.Open();
                            TaxQry.ExecuteNonQuery();
                            con.Close();
                            loadTax();
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            SqlCommand cmd_ = new SqlCommand("Update Tax_table set Ptax_percent='" + txtPurchaseValues.Text.Trim() + "' where tax_name=@taxName", con);
                            cmd_.Parameters.AddWithValue("@taxName", txt_taxName.Text.Trim());

                            cmd_.ExecuteNonQuery();
                            txt_taxName.Text = "";
                            txt_Value.Text = "";
                            txtPurchaseValues.Text = "";                            

                        }
                    }
                    else if (btn_Save.Text == "Update")
                    {
                        if (txt_taxName.Text != string.Empty)
                        {

                            string TaxNo="";
                                string a = txt_taxName.Text.Trim();
                                string b = a.ToUpper();
                                string mystring = b;
                                mystring = mystring.Replace(" ", "");
                                SqlCommand cmd_Select = new SqlCommand("Select Tax_no from Tax_table where Tax_name=@TaxName", con);
                                cmd_Select.Parameters.AddWithValue("@TaxName", TaxName);
                                SqlDataAdapter adp = new SqlDataAdapter(cmd_Select);
                                DataTable dt = new DataTable();
                                dt.Rows.Clear();
                                adp.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    TaxNo = dt.Rows[0]["Tax_no"].ToString();
                                }
                                if (txt_taxName.Text == TaxName && TaxNo!="1")
                                {
                                    con.Close();
                                    con.Open();
                                    SqlCommand cmd = new SqlCommand("Update Tax_table set Tax_name=@TaxName,Tax_mtname=@mystring,Nt_percent=@TaxValue where Tax_no=@TaxNo", con);
                                    cmd.Parameters.AddWithValue("@TaxName", txt_taxName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@mystring", mystring);
                                    cmd.Parameters.AddWithValue("@TaxValue", txt_Value.Text.Trim());
                                    cmd.Parameters.AddWithValue("@TaxNo", TaxNo);
                                    cmd.ExecuteNonQuery();

                                    SqlCommand cmd_ = new SqlCommand("Update Tax_table set Ptax_percent='" + txtPurchaseValues.Text.Trim() + "' where tax_no=@taxName", con);
                                    cmd_.Parameters.AddWithValue("@taxName", TaxNo.ToString().Trim());
                                    cmd_.ExecuteNonQuery();

                                    con.Close();
                                    loadTax();
                                    txt_taxName.Text = "";
                                    txt_Value.Text = "";
                                    txtPurchaseValues.Text = "";
                                    btn_Save.Text = "Save";
                                }

                                else if (txt_taxName.Text != TaxName && TaxNo != "1")
                                {

                                    SqlCommand cmd1 = new SqlCommand("Select * from Tax_table where Tax_name=@Taxname", con);
                                    cmd1.Parameters.AddWithValue("@Taxname", txt_taxName.Text);
                                    SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                                    DataTable dt1 = new DataTable();
                                    dt1.Rows.Clear();
                                    adp1.Fill(dt1);
                                    if (dt1.Rows.Count > 0)
                                    {
                                        MyMessageBox.ShowBox("Tax Name Already Exist", "Warning");
                                    }
                                    else
                                    {
                                        con.Close();
                                        con.Open();
                                        SqlCommand cmd = new SqlCommand("Update Tax_table set Tax_name=@TaxName,Tax_mtname=@mystring,Nt_percent=@TaxValue where Tax_no=@TaxNo", con);
                                        cmd.Parameters.AddWithValue("@TaxName", txt_taxName.Text.Trim());
                                        cmd.Parameters.AddWithValue("@mystring", mystring);
                                        cmd.Parameters.AddWithValue("@TaxValue", txt_Value.Text.Trim());
                                        cmd.Parameters.AddWithValue("@TaxNo", TaxNo);
                                        cmd.ExecuteNonQuery();

                                        SqlCommand cmd_ = new SqlCommand("Update Tax_table set Ptax_percent='" + txtPurchaseValues.Text.Trim() + "' where tax_no=@taxName", con);
                                        cmd_.Parameters.AddWithValue("@taxName", TaxNo.ToString().Trim());
                                        cmd_.ExecuteNonQuery();
                                        con.Close();


                                        loadTax();
                                        txt_taxName.Text = "";
                                        txt_Value.Text = "";
                                        txtPurchaseValues.Text = "";
                                        btn_Save.Text = "Save";
                                    }
                                }
                                else
                                {
                                    MyMessageBox.Showbox("Taxname already exists.","Warning");
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save_Click(sender, e);
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex("[^a0-z9A0-Z9]");
            string RgxItemCode = regex.Replace(txt_Value.Text, "");
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!='.';
        }

        private void newBtnTax_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Save.Text = "Update";
                btnDelete.Enabled = true;

                Button ClickedButton = (Button)sender;

                txt_taxName.Text = ClickedButton.Text.ToString();

                if (txt_taxName.Text != "")
                {
                    TaxName = ClickedButton.Text.ToString();
                }
                SqlCommand cmdselect = new SqlCommand("Select Nt_percent,ptax_percent from Tax_table where Tax_name=@Taxname", con);
                cmdselect.Parameters.AddWithValue("@Taxname", TaxName);
                SqlDataAdapter adp = new SqlDataAdapter(cmdselect);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_Value.Text = dt.Rows[0]["Nt_percent"].ToString();
                    txtPurchaseValues.Text = dt.Rows[0]["ptax_percent"].ToString();
                }
                txt_taxName.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }

        }

        public void loadTax()
        {
            try
            {
                pnl_brand.Controls.Clear();

                SqlCommand cmd = new SqlCommand("select Tax_name from Tax_table", con);
                con.Close();
                con.Open();
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dr["Tax_name"].ToString();
                    newBtn.Name = "Tax_name" + i;
                    newBtn.Width = 180;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);
                    newBtn.Click += new EventHandler(newBtnTax_Click);
                    pnl_brand.Controls.Add(newBtn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void frmTaxCreation_Load(object sender, EventArgs e)
        {
            loadTax();
            btnDelete.Enabled = false;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txt_taxName.Text = "";
            txt_Value.Text = "";
            txtPurchaseValues.Text = "";
            if (btn_Save.Text.Trim() == "Update")
            {
                btn_Save.Text = "Save";
                btnDelete.Enabled = false;
            }
            else
            { }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txt_taxName.Text.Trim() != string.Empty)
                //{
                //    string conform = (MyMessageBox1.ShowBox("Are You Sure Want to Delete", "Warning"));
                //    if (conform == "1")
                //    {
                //        DataTable dtChk = new DataTable();
                //        dtChk.Rows.Clear();
                //        SqlCommand cmdChk = new SqlCommand("Select distinct(Tax_No) from stktrn_table where Tax_No=(Select Tax_No from Tax_table where Tax_name=@tTaxName)", con);
                //        cmdChk.Parameters.AddWithValue("@tTaxName", txt_taxName.Text.Trim());
                //        SqlDataAdapter adpChk = new SqlDataAdapter(cmdChk);
                //        adpChk.Fill(dtChk);
                //        if (dtChk.Rows.Count == 0)
                //        {
                //            if (con.State != ConnectionState.Open)
                //            {
                //                con.Open();
                //            }
                //            SqlCommand cmd = new SqlCommand(@"DELETE tax_table FROM tax_table,item_table WHERE tax_table.tax_no<>item_table.tax_no and tax_table.tax_name=@TaxName", con);

                //            cmd.Parameters.AddWithValue("@TaxName", txt_taxName.Text.Trim());
                //            string Returns = Convert.ToString(cmd.ExecuteNonQuery().ToString());
                //            if (Returns == "1")
                //            {
                //                MyMessageBox1.ShowBox("TaxName Deleted Successfully", "Success");
                //                loadTax();
                //                btnClear_Click(sender, e);

                //            }
                //            else
                //            {
                //                MyMessageBox1.ShowBox("TaxName Already Used by Another Item", "Warning");
                //            }
                //        }
                //        else
                //        {
                //            MyMessageBox.ShowBox("This tax name could not be delete","Warning");
                //        }
                //    }
                //}
                //else
                //{
                //    MyMessageBox1.ShowBox("Please Enter The Tax Name", "Warning");
                //}

                
                    if (txt_taxName.Text.Trim() != string.Empty)
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        string Taxno = "select Tax_no from Tax_table where Tax_name=@tName";
                        SqlCommand cmdUser = new SqlCommand(Taxno, con);
                        cmdUser.Parameters.AddWithValue("@tName", txt_taxName.Text);
                        string TaxNO = cmdUser.ExecuteScalar().ToString();
                        if (TaxNO != "1")
                        {
                            string GetchkTaxNo = "Select distinct(Tax_No) from stktrn_table where Tax_No=@tNo";
                            SqlCommand cmdGetChkTaxNo = new SqlCommand(GetchkTaxNo, con);
                            cmdGetChkTaxNo.Parameters.AddWithValue("@tNo", TaxNO);
                            var salTaxNo = cmdGetChkTaxNo.ExecuteScalar();

                            string GetchkTaxNo1 = "Select distinct(Tax_No) from Item_table where Tax_No=@tNo";
                            SqlCommand cmdGetChkTaxNo1 = new SqlCommand(GetchkTaxNo1, con);
                            cmdGetChkTaxNo1.Parameters.AddWithValue("@tNo", TaxNO);
                            var salTaxNo1 = cmdGetChkTaxNo1.ExecuteScalar();

                            if (salTaxNo == null && salTaxNo1 == null)
                            {
                                string result = MyMessageBox1.ShowBox("Do you want delete this Tax?", "Delete");
                                if (result.Equals("1"))
                                {

                                    SqlCommand sp_cmd = new SqlCommand("delete from Tax_table Where Tax_name=@Tax_Name", con);
                                    sp_cmd.Parameters.AddWithValue("@Tax_Name", txt_taxName.Text);
                                    sp_cmd.ExecuteNonQuery();
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                    MyMessageBox.ShowBox("Deleted Successfully", "Message");
                                    loadTax();
                                    btnClear_Click(sender, e);

                                }
                                if (result.Equals("2"))
                                {
                                }
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Sorry ! " + txt_taxName.Text + " Tax is currently in Use", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("This is default tax");
                        }
                    }                
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.ToString(),"Warning");
            }

        }

        private void txtPurchaseValues_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }
    }
}
