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
using System.Collections;


namespace MSPOSBACKOFFICE
{
    public partial class Reorganize : Form
    {
        public Reorganize()
        {
            InitializeComponent();
            GbDateWiseDelete.Visible = false;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        SqlCommand cmd = null;
        SqlDataAdapter adp = null;

        private void BtnDeletesalesbill_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Before delete do you want to make sales backup...?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Transaction = "DatabaseBackup";
                    panel4.Show();
                    pnlTransation.Show();
                    txtpassword.Select();
                }
                else
                {
                    if (MessageBox.Show("Are you sure to delete the sales...?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Transaction = "DELETE SALES ONELY";
                        panel4.Show();
                        pnlTransation.Show();
                        txtpassword.Select();
                    }
                }                
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnDeleteVoucher_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Truncate table Vch_table", con);
            cmd.ExecuteNonQuery();
        }
        private void btnSalesbillDateWise_Click(object sender, EventArgs e)
        {
            //GbDateWiseDelete.Visible = true;
        }

        private void btnDateDeleteSales_Click(object sender, EventArgs e)
        {
            DataTable dtstk = new DataTable();
            if (txtDate.Text != "")
            {
                if (ValidateDate(txtDate.Text) == true)
                {
                    dtstk.Rows.Clear();
                    DateTime d2 = new DateTime();
                    d2 = (DateTime.ParseExact(txtDate.Text, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                    //cmd = new SqlCommand("select * from stktrn_table where strn_date between '" + Convert.ToDateTime(d2).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' and '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' and strn_type=1", con);
                    cmd = new SqlCommand("select * from stktrn_table where strn_date<='" + Convert.ToDateTime(d2).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' and strn_type=1", con);
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtstk);
                    if (MyMessageBox1.ShowBox("Are sure want to delete", "Warnig") == "1")
                    {
                        if (dtstk.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtstk.Rows.Count; i++)
                            {
                                cmd = new SqlCommand("delete from stktrn_table where strn_sno=''", con);
                                cmd.ExecuteNonQuery();
                                cmd = new SqlCommand("delete from salmas_table where smas_no=''", con);
                                cmd.ExecuteNonQuery();
                            }

                            GbDateWiseDelete.Visible = false;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                   // MyMessageBox1.ShowBox("Date is not valid", "Warning");
                    ValidateDate1(txtDate.Text);
                }
            }
        }

        private bool ValidateDate(string date)
        {
            string[] dateParts;
            try
            {
                // for US, alter to suit if splitting on hyphen, comma, etc.
                dateParts = date.Split('/');

                // create new date from the parts; if this does not fail
                // the method will return true and the date is valid
                DateTime testDate = new
                    DateTime(Convert.ToInt32(dateParts[2]),
                    Convert.ToInt32(dateParts[0]),
                    Convert.ToInt32(dateParts[1]));
                return true;
            }
            catch
            {
                // if a test date cannot be created, the
                // method will return false
                return false;
            }
        }

        private bool ValidateDate1(string stringDateValue)
        {
            try
            {
                System.Globalization.CultureInfo CultureInfoDateCulture = new System.Globalization.CultureInfo("fr-FR");
                DateTime d = DateTime.ParseExact(stringDateValue, "dd/MM/yyyy", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Example 2 - Validate Date for the format MM/DD/YYYY 
        private bool ValidateDate2(string stringDateValue)
        {
            try
            {
                System.Globalization.CultureInfo CultureInfoDateCulture = new System.Globalization.CultureInfo("en-US");
                DateTime d = DateTime.ParseExact(stringDateValue, "MM/dd/yyyy", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Example 3 - Validate Date for the format YYYY/MM/DD 
        private bool ValidateDate3(string stringDateValue)
        {
            try
            {
                System.Globalization.CultureInfo CultureInfoDateCulture = new System.Globalization.CultureInfo("ja-JP");
                DateTime d = DateTime.ParseExact(stringDateValue, "yyyy/MM/dd", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Example 4 - Validate Date for the format DDMMYYYY 
        private bool ValidateDate4(string stringDateValue)
        {
            try
            {
                System.Globalization.CultureInfo CultureInfoDateCulture = new System.Globalization.CultureInfo("fr-FR");
                DateTime d = DateTime.ParseExact(stringDateValue, "ddMMyyyy", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Example 5 - Validate Date for the format MMDDYYYY 
        private bool ValidateDate5(string stringDateValue)
        {
            try
            {
                System.Globalization.CultureInfo CultureInfoDateCulture = new System.Globalization.CultureInfo("en-US");
                DateTime d = DateTime.ParseExact(stringDateValue, "MMddyyyy", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Example 6 - Validate Date for the format MMDDYYYYHHMMSS 
        private bool ValidateDate6(string stringDateValue)
        {
            try
            {
                System.Globalization.CultureInfo CultureInfoDateCulture = new System.Globalization.CultureInfo("en-US");
                DateTime d = DateTime.ParseExact(stringDateValue, "MMddyyyyHHmmss", CultureInfoDateCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GbDateWiseDelete.Visible = false;
        }
        private void Reorganize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
        }
        private void bntExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string Transaction = "";
        private void btnDeleteAllTransaction_Click(object sender, EventArgs e)
        {
            Transaction = "TRANSACTION";
            panel4.Show();
            pnlTransation.Show();
           // btnlogin_Click(sender, e);
            txtpassword.Select();
        }
        private void RemoveAllProcess_Click(object sender, EventArgs e)
        {
            Transaction = "REMOVEALL";
            panel4.Show();
            pnlTransation.Show();
            txtpassword.Select(); 
        } 
        private void Reorganize_Load(object sender, EventArgs e)
        {
            pnlTransation.Hide();
            panel4.Hide();

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
             Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
           // Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        string messagebox = "";
        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txtpassword.Text == "")
                {
                    MyMessageBox1.ShowBox("Please Enter password", "Warning");
                    txtpassword.Select();
                }
                else if (txtpassword.Text.Trim().ToUpper() == "!PASSWORD123" && Transaction != "LICENCE")
                {
                    if (Transaction.ToString().Trim() == "TRANSACTION")
                    {
                        SqlCommand cmd = new SqlCommand("sp_DeleteAllTransaction", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        pnlTransation.Hide();
                        panel4.Hide();
                        messagebox = MyMessageBox1.ShowBox("Are You sure Want to Delete", "Message");
                        if (messagebox == "1")
                        {
                            cmd.ExecuteNonQuery();
                            MyMessageBox.ShowBox("Deleted AllTransaction", "Success");
                            txtpassword.Text = "";
                        }
                    }
                    else if (Transaction.ToString().Trim() == "REMOVEALL")
                    {
                        SqlCommand cmd1 = new SqlCommand("[sp_RemoveAll]", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        pnlTransation.Hide();
                        panel4.Hide();
                        messagebox = MyMessageBox1.ShowBox("Are You sure Want to Delete", "Message");
                        if (messagebox == "1")
                        {
                            cmd1.ExecuteNonQuery();
                            txtpassword.Text = "";
                        }
                        txtpassword.Text = "";
                    }
                    else if (Transaction.ToString().Trim() == "DELETEPURCHASEONLY")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlCommand cmd_delete = new SqlCommand("SP_DeleteTotalPurchase", con);
                        messagebox = MyMessageBox1.ShowBox("Are You sure Want to Delete All Purchase Entrys", "Message");
                        if (messagebox == "1")
                        {
                            pnlTransation.Hide();
                            panel4.Hide();
                            cmd_delete.ExecuteNonQuery();
                            MyMessageBox.ShowBox("Deleted All Purchases Entrys", "Success");
                            txtpassword.Text = "";
                        }
                    }
                    else if (Transaction.ToString().Trim() == "DELETESALESONLY")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlCommand cmd_delete = new SqlCommand("SP_DeleteTotalSales", con);
                        messagebox = MyMessageBox1.ShowBox("Are You sure Want to Delete All Sales Entrys", "Message");
                        if (messagebox == "1")
                        {
                            pnlTransation.Hide();
                            panel4.Hide();
                            cmd_delete.ExecuteNonQuery();
                            MyMessageBox.ShowBox("Deleted All Sales Entrys", "Success");
                            txtpassword.Text = "";
                        }
                    }
                    else if (Transaction.ToString().Trim() == "CLEAROPENINGQTY")
                    {
                        SqlCommand cmd_DeleteOpnQty = new SqlCommand("DeleteOpeningQty", con);
                        messagebox = MyMessageBox1.ShowBox("Are You sure Want to Delete Opening Quantity", "Warning");
                        if (messagebox == "1")
                        {
                            pnlTransation.Hide();
                            panel4.Hide();
                            cmd_DeleteOpnQty.ExecuteNonQuery();
                            MyMessageBox.ShowBox("Opening Stock Clear", "Success");
                            txtpassword.Text = "";
                        }
                    }
                    else if (Transaction.ToString().Trim() == "ROLLBACKENDOFDAY")
                    {
                        DataTable dtNew1 = new DataTable();
                        dtNew1.Rows.Clear();
                        SqlDataAdapter adp = new SqlDataAdapter("select COUNT(*) from SalMas_table where Smas_billDate=(select CONVERT(DATE,DateAdd(day,1,endofDay),103) from EndofDay_table where ID=(Select EndofDayId from numberTable))", con);
                        adp.Fill(dtNew1);
                        if (dtNew1.Rows[0][0].ToString() == "0")
                        {
                            SqlCommand cmdRollback = new SqlCommand("sp_RollBackEndOfDay", con);
                            cmdRollback.CommandType = CommandType.StoredProcedure;
                            messagebox = MyMessageBox1.ShowBox("Do you Want to Rollback last reset Endofday", "Warning");
                            if (messagebox == "1")
                            {
                                pnlTransation.Hide();
                                panel4.Hide();

                                DataTable dtNew = new DataTable();
                                dtNew.Rows.Clear();
                                SqlDataAdapter Asp = new SqlDataAdapter(cmdRollback);
                                Asp.Fill(dtNew);
                                //                            cmdRollback.ExecuteNonQuery();
                                //     MyMessageBox.ShowBox(dtNew.Rows[0][0].ToString(), "Success");
                                txtpassword.Text = "";
                                MyMessageBox.ShowBox("Operation Executed Successfully", "Warning");
                            }

                        }
                        else
                        {
                            MyMessageBox.ShowBox("Could Not Run this Operation", "Warning");
                            pnlTransation.Hide();
                            panel4.Hide();

                        }
                    }
                    //else if (Transaction.ToString().Trim().Equals("DELETE SALES ONELY"))
                    //{
                    //    SqlCommand cmdDeleteSales = new SqlCommand("DeleteSalesOnly", con);
                    //    cmdDeleteSales.CommandType = CommandType.StoredProcedure;
                    //    messagebox = MyMessageBox1.ShowBox("Do Sure you Want to Delete Total Sales", "Warning");
                    //    if (messagebox == "1")
                    //    {
                    //        if (con.State != ConnectionState.Open)
                    //        {
                    //            con.Open();
                    //        }
                    //        cmdDeleteSales.ExecuteNonQuery();
                    //        txtpassword.Text = "";
                    //        MyMessageBox.ShowBox("Operation Executed Successfully", "Warning");

                    //    }
                    //}
                }
                else if (txtpassword.Text.Trim() == "AdminActivate@123" && Transaction == "LICENCE")
                {
                    Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser;
                    regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                    regkey.SetValue("Black", "False");
                    firstTime();
                    txtpassword.Text = "";
                    pnlTransation.Hide();
                    panel4.Hide();

                }
                else if (txtpassword.Text.Trim() == "66612345" && Transaction.ToString().Trim().Equals("DELETE SALES ONELY"))
                {
                    SqlCommand cmdDeleteSales = new SqlCommand("DeleteSalesOnly", con);
                    cmdDeleteSales.CommandType = CommandType.StoredProcedure;
                    messagebox = MyMessageBox1.ShowBox("Do Sure you Want to Delete Total Sales", "Warning");
                    if (messagebox == "1")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmdDeleteSales.ExecuteNonQuery();
                        txtpassword.Text = "";
                        MyMessageBox.ShowBox("Operation Executed Successfully", "Warning");
                    }
                }
                else if (txtpassword.Text.Trim() == "Admin@123" && Transaction == "DatabaseBackup")
                {
                    frmBackup frm = new frmBackup();
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.WindowState = FormWindowState.Normal;
                    frm.Location = new Point(0, 135);
                    frm.Show();
                    pnlTransation.Hide();
                    panel4.Hide();
                    txtpassword.Text = "";
                }
                else if (txtpassword.Text.Trim().ToUpper() != "!PASSWORD123")
                {
                    MyMessageBox.ShowBox("Please Enter Valid Password");
                    txtpassword.Text = "";
                }
                else if (txtpassword.Text.Trim().ToUpper() != "AdminActivate@123")
                {
                    MyMessageBox.ShowBox("Please Enter Valid Activation Code");
                    txtpassword.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void firstTime()
        {
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser;
            regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
            DateTime dt = DateTime.Now;
            string onlyDate = dt.ToShortDateString(); // get only date not time
            regkey.SetValue("Install", onlyDate); //Value Name,Value Data
            regkey.SetValue("Use", onlyDate); //Value Name,Value Data
            regkey.SetValue("Days", "365"); //Value Name,Value Data
        } 
        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnlogin.Focus();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pnlTransation.Hide();
            panel4.Hide();
            txtpassword.Text = "";
            
        }
        string strnItemNo = "";
        string strn_type = "";
        string StrnParty_no = "";
        private void btnDeleteSales_Click(object sender, EventArgs e)
        {
            
            Transaction = "DELETESALESONLY";
            panel4.Show();
            pnlTransation.Show();
            //btnlogin_Click(sender,e);
        }
        private void btnDeletePurchase_Click(object sender, EventArgs e)
        {
           
            Transaction = "DELETEPURCHASEONLY";
            panel4.Show();
            pnlTransation.Show();
           // btnlogin_Click(sender,e);
        }

        private void btnClearOpnQty_Click(object sender, EventArgs e)
        {
            
            Transaction = "CLEAROPENINGQTY";
            panel4.Show();
            pnlTransation.Show();
            //btnlogin_Click(sender, e);

            
        }

        private void btnRollBackLastEndOfDay_Click(object sender, EventArgs e)
        {
            try
            {
                Transaction = "ROLLBACKENDOFDAY";
                panel4.Show();
                pnlTransation.Show();
                txtpassword.Select(); 
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Transaction = "LICENCE";
                panel4.Show();
                pnlTransation.Show();
                txtpassword.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void Pnl_Back_Paint(object sender, PaintEventArgs e)
        {

        }

      
    }
}
