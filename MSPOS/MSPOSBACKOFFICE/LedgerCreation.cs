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

namespace MSPOSBACKOFFICE._Ledger
{
    public partial class LedgerCreation : Form
    {
         //static int remaincount;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public LedgerCreation()
        {
            InitializeComponent();
            pnl_def1.Hide();
            pnl_def2.Hide();
            pnl_def3.Hide();
            pnl_1.Hide();
            pnl_def4.Hide();
            pnl_list.Hide();
            //con.Close();
            //con.Open();
            //SqlCommand cmd = new SqlCommand("select count(Ledger_groupname) from Ledger_Grouptable", con);
            //int lbl_cunt = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            //remaincount = lbl_cunt - 28;
            //con.Close();
        }

        private void LedgerCreation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }



        private void LedgerCreation_Load(object sender, EventArgs e)
        {

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_aliasName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                pnl_list.Show();
                ItemNameloadbyname();
                lst_itemName.Focus();
            }
        }
        string itemname;
        public void ItemNameloadbyname()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            con.Close();
            con.Open();
            SqlCommand namecmd = new SqlCommand("select Ledger_groupname from Ledger_Grouptable order by Ledger_groupname ASC", con);
            SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lst_itemName.Items.Clear();
            nameadp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_itemName.Items.Add(dt.Rows[i]["Ledger_groupname"].ToString());
                    itemname = (dt.Rows[i]["Ledger_groupname"].ToString());
                    this.lst_itemName.SelectedIndex = 0;
                }

            }
            con.Close();
            nameadp.Dispose();


        }

        private void lst_itemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_under.Text = lst_itemName.SelectedItem.ToString();
         
                //this.moveUp.Enabled = this.listBox.SelectedIndex > 0;
                // this.moveDown.Enabled = this.listBox.SelectedIndex > -1 && listBox.SelectedIndex < listBox.Items.Count - 1;
                //lbl_count.Text = remaincount.ToString();
                //if (lst_itemName.SelectedIndex > 28)
                //{
                //    remaincount--;
                //    lbl_count.Text = remaincount.ToString();
                //}
            
  
        }

        private void lst_itemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_under.Text = lst_itemName.SelectedItem.ToString();
                txt_under.Focus();
            }
            
             
        }

        private void txt_under_Enter(object sender, EventArgs e)
        {
            pnl_list.Hide();
            if (txt_under.Text != "")
            {

            }
        }
    }
}
