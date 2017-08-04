using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace MSPOSBACKOFFICE
{
    public partial class Brand : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        //SqlConnection con = new SqlConnection(@"Data Source=ASTRID-PC\SQLEXPRESS;Initial Catalog=Mspos;Persist Security Info=True;User ID=sa;password=!Password123");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        string txtupdateModel = string.Empty;
        public Brand()
        {
            InitializeComponent();
            loadBrand();
            btn_Brnd_Delete.Enabled = false;

            btn_Brnd_Update.Enabled = false;
        }

        public void loadBrand()
        {
            try
            {
                pnl_brand.Controls.Clear();

                SqlCommand cmd = new SqlCommand(" select Brand_name from Brand_table", con);
                con.Close();
                con.Open();
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {

                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dr["Brand_name"].ToString();
                    newBtn.Name = "Brand_name" + i;
                    newBtn.Width = 180;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);
                    newBtn.Click += new EventHandler(newBtnBrandItem_Click);
                    pnl_brand.Controls.Add(newBtn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_rack_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newBtnBrandItem_Click(object sender, EventArgs e)
        {
            btn_Brnd_Delete.Enabled = true;
            btn_Brnd_Update.Enabled = true;
            btn_Brnd_save.Enabled = false;
            Button ClickedButton = (Button)sender;
            txt_Bname.Text = ClickedButton.Text.ToString();

            if (txt_Bname.Text != "")
            {
                txtupdateModel = ClickedButton.Text.ToString();
            }
            txt_Bname.Select();
        }
        private void btn_B_save_Click(object sender, EventArgs e)
        {
            funSave();
           
        }
        void funSave()
        {
            try
            {
                bool result = false;

                if (txt_Bname.Text.Trim() == "")
                {
                    MyMessageBox.ShowBox("Please Enter Brand Name", "Warning");
                }
                else
                {
                    //SqlCommand cmd1 = new SqlCommand("Select * from Brand_Table where Brand_Name='" + txt_Bname.Text.Trim() + "'", con);
                    //con.Close();
                    //con.Open();
                    //dr = cmd1.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    MyMessageBox.ShowBox("Brand Name Already Exist", "Warning");
                    //}
                    //else
                    //{
                        //int grp_pos;
                        //con.Close();
                        //con.Open();
                        //string bnoqry = "select max(Brand_no)+1 from Numbertable";
                        //SqlCommand bno = new SqlCommand(bnoqry, con);
                        //int b_no = Convert.ToInt32(bno.ExecuteScalar());
                        //con.Close();

                        //con.Open();
                        //string grpos = "select max(GroupPos)+1 from Brand_table";
                        //SqlCommand cmdgrpos = new SqlCommand(grpos, con);
                        //var isChk = cmdgrpos.ExecuteScalar().ToString();
                        //if (isChk != "" )
                        //{
                        //    grp_pos = Convert.ToInt32(cmdgrpos.ExecuteScalar());
                        //}
                        //else
                        //{
                        //    grp_pos = 1;
                        //}

                    con.Close();

                        //string a = txt_Bname.Text;
                        //string b = a.ToUpper();

                        //string mystring = b;
                        //mystring = mystring.Replace(" ", "");

                        con.Open();
                        SqlCommand sp_cmd = new SqlCommand("sp_Brand_Insert", con);
                        sp_cmd.CommandType = CommandType.StoredProcedure;

                        //sp_cmd.Parameters.AddWithValue("@Brand_No", b_no);

                        sp_cmd.Parameters.AddWithValue("@tBrandName", txt_Bname.Text);
                        sp_cmd.Parameters.Add("@chk", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //SqlParameter result = new SqlParameter("@chk", SqlDbType.Int);
                        //result.Direction = ParameterDirection.Output;
                        //sp_cmd.Parameters.Add(result);
                    

                        //sp_cmd.Parameters.AddWithValue("@Brand_mtname", mystring);
                        //sp_cmd.Parameters.AddWithValue("@Brand_level", '0');
                        //sp_cmd.Parameters.AddWithValue("@Brand_under", '0');
                        //sp_cmd.Parameters.AddWithValue("@Brand_gno", '0');
                        //sp_cmd.Parameters.AddWithValue("@Brand_flag", '0');
                        //sp_cmd.Parameters.AddWithValue("@Std_Group", '0');
                        //sp_cmd.Parameters.AddWithValue("@GroupPos", grp_pos);

//                        SqlCommand cmd = new SqlCommand("insert into Brand_table(Brand_no,Brand_name,Brand_mtname,Brand_level,Brand_under,Brand_gno,Brand_flag,Std_Group,GroupPos) values('" + b_no + "','" + txt_Bname.Text + "','" + mystring + "','0','0', '0','0','0','" + grp_pos + "')", con);
                        sp_cmd.ExecuteNonQuery();
                        result = Convert.ToBoolean(sp_cmd.Parameters["@chk"].Value);
                        //int temp = (int)result.Value;
                        //MessageBox.Show(result.ToString());
                        if (result ==true)
                        {
             
                            MessageBox.Show("Brand Name already exists");
                            con.Close();
                        }
                        else
                        {
                            //  MessageBox.Show("Brand Saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txt_Bname.Text = string.Empty;
                            loadBrand();
                            con.Close();
                        }

                        //SqlCommand cmd11 = new SqlCommand("update NumberTable set Brand_no=Brand_No+1", con);
                        //con.Close();
                        //con.Open();
                        //cmd11.ExecuteNonQuery();
                        //con.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
               // this.Close();
            }
        }
        private void btn_Brnd_Update_Click(object sender, EventArgs e)
        {
            try
            {
                string a = txt_Bname.Text;
                string b = a.ToUpper();

                string mystring = b;
                mystring = mystring.Replace(" ", "");
                con.Close();
                con.Open();
                SqlCommand sp_cmd = new SqlCommand("sp_Brand_Update",con);
                sp_cmd.CommandType = CommandType.StoredProcedure;
                sp_cmd.Parameters.AddWithValue("@Brand_Name", txt_Bname.Text);
                sp_cmd.Parameters.AddWithValue("@Brand_mtname", mystring);
                sp_cmd.Parameters.AddWithValue("@Brand_Name2", txtupdateModel);
                //SqlCommand cmd = new SqlCommand("update Brand_table set Brand_name='" + txt_Bname.Text + "', Brand_mtname='" + mystring + "' Where Brand_name='" + txtupdateModel + "'", con);
                sp_cmd.ExecuteNonQuery();
                con.Close();
                // MessageBox.Show("" + txt_Bname.Text + " Brand is Updated", "Update");
                btn_Brnd_save.Enabled = true;
                txt_Bname.Text = string.Empty;
                btn_Brnd_Delete.Enabled = false;
                btn_Brnd_Update.Enabled = false;
                loadBrand();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void btn_Brnd_Clear_Click(object sender, EventArgs e)
        {
            btn_Brnd_save.Enabled = true;
            txt_Bname.Text = string.Empty;
            btn_Brnd_Delete.Enabled = false;
            btn_Brnd_Update.Enabled = false;
            loadBrand();
        }
        public event System.EventHandler CustomerEventHandler1;
        private void btn_Brnd_Exit_Click(object sender, EventArgs e)
        {
            
            if (CustomerEventHandler1 != null)
            {
                CustomerEventHandler1(sender, e);
                this.Close();
            }
            this.Close();
            this.Close();     
        }
        private void Brand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Brand_Load(object sender, EventArgs e)
        {
            txt_Bname.Select();
           // For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void btn_Brnd_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtupdateModel == "Primary")
                {
                    MyMessageBox.ShowBox("Cannot Delete Standard Group", "Warning");
                }
                else
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    string BrandNoqry = "select Brand_no from Brand_table where Brand_name=@tBrand";
                    SqlCommand cmdBrand= new SqlCommand(BrandNoqry,con);
                    cmdBrand.Parameters.AddWithValue("@tBrand", txtupdateModel);
                    string BrandNO=cmdBrand.ExecuteScalar().ToString();
                    con.Close();
                    con.Open();
                    string GetchkBrandCode = " select * from Item_table where Brand_no=@tBrandNo";
                    SqlCommand cmdGetChkBrandCode = new SqlCommand(GetchkBrandCode, con);
                    cmdGetChkBrandCode.Parameters.AddWithValue("@tBrandNo", BrandNO);
                    var UsedBrandNo = cmdGetChkBrandCode.ExecuteScalar();
                    con.Close();
                    if (UsedBrandNo == null)
                    {
                        string result = MyMessageBox1.ShowBox("Do you want delete this brand?", "Delete");
                        if (result.Equals("1"))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand sp_cmd = new SqlCommand("sp_Brand_Delete", con);
                            sp_cmd.CommandType = CommandType.StoredProcedure;
                            sp_cmd.Parameters.AddWithValue("@Brand_Name", txtupdateModel);

                            //SqlCommand cmd = new SqlCommand("delete from Brand_table Where Brand_name='" + txtupdateModel + "'", con);
                            sp_cmd.ExecuteNonQuery();
                            con.Close();
                            txt_Bname.Clear();

                        }
                        if (result.Equals("2"))
                        {
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Sorry ! "+txtupdateModel+" Brand is currently in Use", "Warning");
                    }
                }
                btn_Brnd_save.Enabled = true;
                txt_Bname.Text = string.Empty;
                btn_Brnd_Delete.Enabled = false;
                btn_Brnd_Update.Enabled = false;
                loadBrand();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void txt_Bname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab )
            {
                if (btn_Brnd_save.Enabled == true)
                {
                    btn_Brnd_save.Select();
                }
                else
                {
                    btn_Brnd_Update.Select();
                }
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               btn_Brnd_Exit.Select();
            }
        }

        private void btn_Brnd_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                funSave();
               txt_Bname.Select();
            }
            
            if (e.KeyCode == Keys.Tab)
            {
               btn_Brnd_Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
              txt_Bname.Select();
            }
        }

        private void btn_Brnd_Update_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
              btn_Brnd_Delete.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               txt_Bname.Select();
            }
        }

        private void btn_Brnd_Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
               btn_Brnd_Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               btn_Brnd_Update.Select();
            }
        }

        private void btn_Brnd_Clear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
               btn_Brnd_Exit.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (btn_Brnd_Update.Enabled == false)
                {
                    btn_Brnd_save.Select();
                }
                else
                {
                    btn_Brnd_Delete.Select();
                }
            }
        }

        private void btn_Brnd_Exit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
               txt_Bname.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               btn_Brnd_Clear.Select();
            }
        }
    }
}
