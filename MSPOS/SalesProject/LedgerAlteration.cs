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

namespace SalesProject._Ledger
{
    public partial class LedgerAlteration : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public LedgerAlteration()
        {
            InitializeComponent();
          
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
            SqlCommand namecmd = new SqlCommand("select Ledger_name from Ledger_table order by Ledger_name ASC", con);
            SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lst_itemName.Items.Clear();
            nameadp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lst_itemName.Items.Add(dt.Rows[i]["Ledger_name"].ToString());
                    itemname = (dt.Rows[i]["Ledger_name"].ToString());
                    this.lst_itemName.SelectedIndex = 0;
                }

            }
            con.Close();
            nameadp.Dispose();


        }

        private void txt_legername_Enter(object sender, EventArgs e)
        {
            ItemNameloadbyname();
            pnl_list.Show();
            lst_itemName.Focus();
            txt_legername.Text = lst_itemName.SelectedItem.ToString();
        }

        private void lst_itemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_legername.Text = lst_itemName.SelectedItem.ToString();
           


        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lst_itemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_legername.Text = lst_itemName.SelectedItem.ToString();
                pnl_list.Hide();
            }
        }

        private void txt_legername_TextChanged(object sender, EventArgs e)
        {

            //AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            //con.Close();
            //con.Open();
            //SqlCommand cmd = new SqlCommand("select Ledger_name from Ledger_table order by Ledger_name ASC", con);

            //SqlDataReader dReader;
            //dReader = cmd.ExecuteReader();

            //if (dReader.Read())
            //{
            //    while (dReader.Read())
            //        namesCollection.Add(dReader["Ledger_name"].ToString());
            //}
            //else
            //{
            //    MessageBox.Show("Data not found");
            //}
            //dReader.Close();

            //txt_legername.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txt_legername.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txt_legername.AutoCompleteCustomSource = namesCollection;
            //con.Close();

        }


    }

}
