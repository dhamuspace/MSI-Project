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


namespace SalesProject
{
    
    public partial class addDiscount : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public event System.EventHandler discnameEventHandler;
        public addDiscount()
        {
            InitializeComponent();
            funConnectionStateCheck();
        }
        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_discountname.Text))
                {
                    chkbox.discountname = txt_discountname.Text;
                    //string strtdate = DateTime.Today.ToShortDateString();
                    //string enddate = DateTime.Today.ToShortDateString();
                    string printedname = txt_discountname.Text.ToUpper();
                    //string creatdisc = "insert into DiscountSetting_Table(Enabled,DiscountName,PrintText,Calculation,Amount,ItemsPerOder,AllowOtherDiscount,StartDate,EndDate,Sunday,Monday,Tuesday,Wednessday,Thursday,Friday,Saturday) values('Yes','" + txt_discountname.Text + "','" + printedname + "','Fixed','0','1','Yes',"+strtdate+","+enddate+",'Yes','Yes','Yes','Yes','Yes','Yes','Yes')";
                    SqlCommand cmd = new SqlCommand("sp_AddDiscount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tDiscountName", txt_discountname.Text);
                    cmd.Parameters.AddWithValue("@tPrintText", printedname);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("inserted successfully");
                    this.Close();
                    if (discnameEventHandler != null)
                    {
                        discnameEventHandler(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_discountname.Text = "";
            this.Close();
        }
    }
}
