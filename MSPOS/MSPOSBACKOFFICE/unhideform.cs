using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace MSPOSBACKOFFICE
{
    public partial class unhideform : Form
    {
      //  Sqlconection con = new Sqlconection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        chkbox selectname= new chkbox();
        public unhideform()
        {
            InitializeComponent();
       
        }



        private void unhideform_Load(object sender, EventArgs e)
        {
            
            //con.Open();
            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * from stck_adj_table_sri", con);
            //adapter.Fill(ds);
            //this.lst_colHeaders.DataSource = ds.Tables[0];
            //this.lst_colHeaders.DisplayMember = "stckA_Name";
            //con.Close();
            SqlCommand cmd =new SqlCommand( "select * from stckcol_header_tbl");
            cmd.Connection = con;
          
            con.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {

                    ListBox item = new ListBox();
                    item.Text = sdr["columnName"].ToString();
                    item.ValueMember = sdr["colid"].ToString();
               
                    item.SelectedItem = Convert.ToBoolean(sdr["status"]);

                    Chk_colHeader.Items.Add(item.Text, Convert.ToBoolean(sdr["status"]));
                }
            }
            con.Close();
           
        }

        private void Chk_colHeader_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Space)
            {
                Chk_colHeader.SelectedItem = true;
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string sel;
        private void btn_ok_Click(object sender, EventArgs e)
        {

                 unhidevalue();
        }

        public void unhidevalue()
        {

            string tru = "True";
            string flse = "False";


            //Chk_colHeader.CheckedItems.Count - 1

            for (int j = 0; j <= Chk_colHeader.CheckedItems.Count - 1; j++)
            {

                bool checkstatus = Convert.ToBoolean(Chk_colHeader.GetItemCheckState(j));

                if (checkstatus == true)
                {
                    //Chk_colHeader.Text.ToString();
                    //sel = Chk_colHeader.Text.ToString();
                    sel = Chk_colHeader.CheckedItems[j].ToString();
                    if (sel == "Code")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                    if (sel == "Name")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Unit")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }

                    if (sel == "Less Qty")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Add Qty")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Rate")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Amount")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + tru + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }

                else
                {
                  //  sel = Chk_colHeader.Text.ToString();
                     sel = Chk_colHeader.CheckedItems[j].ToString();
                    if (sel == "Code")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Name")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Unit")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }

                    if (sel == "Less Qty")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Add Qty")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Rate")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    if (sel == "Amount")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update stckcol_header_tbl set status='" + flse + "' where columnName='" + sel + "' ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
        }
        
    


              

            

        


        private void Chk_colHeader_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //for (int i = 0; i < Chk_colHeader.Items.Count; i++)
            //{
            //    if (i != e.Index)
            //    {
            //        Chk_colHeader.SetItemChecked(i, false);
            //    }
            //}
        }

        private void unhideform_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void Chk_colHeader_SelectedValueChanged(object sender, EventArgs e)
        {
            //foreach (int j in Chk_colHeader.CheckedIndices )
            //{
                      
            //    MessageBox.Show(Chk_colHeader.Text.ToString());
            //}
            

        }
    }
}
