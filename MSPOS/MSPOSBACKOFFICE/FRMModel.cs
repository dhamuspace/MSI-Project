using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;

namespace MSPOSBACKOFFICE
{
    public partial class Model : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        //SqlConnection con = new SqlConnection(@"Data Source=ASTRID-PC\SQLEXPRESS;Initial Catalog=Mspos;Persist Security Info=True;User ID=sa;password=!Password123");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        string txtupdateModel = string.Empty;
        public Model()
        {
            InitializeComponent();
            loadModel();
            btn_M_Delete.Enabled = false;

            btn_M_Update.Enabled = false;
        }
        public void loadModel()
        {
            try
            {
                Pnl_Modname.Controls.Clear();

                SqlCommand cmd = new SqlCommand("select Model_name from Model_table", con);
                con.Close();
                con.Open();
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {

                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dr["Model_name"].ToString();
                    newBtn.Name = "Model_name" + i;
                    newBtn.Width = 170;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);               
                    newBtn.Click += new EventHandler(newBtnGroupItem_Click);
                    Pnl_Modname.Controls.Add(newBtn);
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
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 56, 96), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);
        }

        private void pnl_rack_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(245, 251, 251), Color.FromArgb(0, 56, 96), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);
        }
        private void newBtnGroupItem_Click(object sender, EventArgs e)
        {
            btn_M_Delete.Enabled = true;
            btn_M_Update.Enabled = true;
            btn_M_save.Enabled = false;
            Button ClickedButton = (Button)sender;
            txt_Mname.Text = ClickedButton.Text.ToString();

            if (txt_Mname.Text != "")
            {
                txtupdateModel = ClickedButton.Text.ToString();


                //DataTable dt = new DataTable();
                //SqlCommand cmd = new SqlCommand(" select * from Model_table where Model_name='" + TxtGroupName.Text + "'", con);
                //con.Close();
                //con.Open();
                //dt.Rows.Clear();
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(dt);

            }
            txt_Mname.Select();
        }
        private void btn_M_save_Click(object sender, EventArgs e)
        {
            funSave();
        }

        void funSave()
        {
            try
            {
                bool result = false;

                if (txt_Mname.Text.Trim() == "")
                {
                MyMessageBox.ShowBox("Please Enter Model Name", "Warning");
                }
                else
                {
                    // SqlCommand cmd1 = new SqlCommand("Select * from model_Table where Model_Name='" +txt_Mname.Text.Trim() + "'", con);
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
                        //string bnoqry = "select max(Model_no)+1 from Numbertable";
                        //SqlCommand bno = new SqlCommand(bnoqry, con);
                        //int b_no = Convert.ToInt32(bno.ExecuteScalar());
                        //con.Close();

                        //con.Open();
                        //string grpos = "select max(GroupPos)+1 from Model_table";
                        //SqlCommand cmdgrpos = new SqlCommand(grpos, con);
                        //var isChk = cmdgrpos.ExecuteScalar().ToString();
                        //if (isChk != "")
                        //{
                        //    grp_pos = Convert.ToInt32(cmdgrpos.ExecuteScalar());
                        //}
                        //else
                        //{
                        //    grp_pos = 1;
                        //}
                       
                        con.Close();

                        //string a = txt_Mname.Text;
                        //string b = a.ToUpper();

                        //string mystring = b;
                        //mystring = mystring.Replace(" ", "");

                        con.Open();
                        SqlCommand sp_cmd = new SqlCommand("sp_Model_Insert", con);
                        sp_cmd.CommandType = CommandType.StoredProcedure;
                        //sp_cmd.Parameters.AddWithValue("@Model_no", b_no);
                        sp_cmd.Parameters.AddWithValue("@tModel_name", txt_Mname.Text);
                        sp_cmd.Parameters.Add("@chk", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        //sp_cmd.Parameters.AddWithValue("@Model_mtname", mystring);
                        //sp_cmd.Parameters.AddWithValue("@Model_level", '0');
                        //sp_cmd.Parameters.AddWithValue("@Model_under", '0');
                        //sp_cmd.Parameters.AddWithValue("@Model_gno", '0');
                        //sp_cmd.Parameters.AddWithValue("@Model_flag", '0');
                        //sp_cmd.Parameters.AddWithValue("@Row_no", '0');
                        //sp_cmd.Parameters.AddWithValue("@Std_Group", '0');
                        //sp_cmd.Parameters.AddWithValue("@GroupPos", grp_pos);

                       // SqlCommand cmd = new SqlCommand("insert into Model_table(Model_no,Model_name,Model_mtname,Model_level,Model_under,Model_gno,Model_flag,Row_no,Std_Group,GroupPos) values('" + b_no + "','" + txt_Mname.Text + "','" + mystring + "', '0','0','0', '0','0','0','" + grp_pos + "')", con);
                        int temp= sp_cmd.ExecuteNonQuery();
                        result = Convert.ToBoolean(sp_cmd.Parameters["@chk"].Value);
                        if (result == true)
                        {
                            MessageBox.Show("Model name already exists");
                            con.Close();
                            
                        }
                        else
                        {
                            txt_Mname.Text = string.Empty;
                            loadModel();
                            con.Close();
                        }
                    // MyMessageBox.ShowBox("Model Saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        //txt_Mname.Text = string.Empty;
                        //loadModel();

                        //SqlCommand cmd11 = new SqlCommand("update NumberTable set Model_no=Model_No+1", con);
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

        private void btn_M_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_M_Update_Click(object sender, EventArgs e)
        {
            try
            {
                string a = txt_Mname.Text;
                string b = a.ToUpper();

                string mystring = b;
                mystring = mystring.Replace(" ", "");
                con.Close();
                con.Open();
                SqlCommand sp_cmd = new SqlCommand("sp_Model_Update", con);
                sp_cmd.CommandType = CommandType.StoredProcedure;
                sp_cmd.Parameters.AddWithValue("@Model_name", txt_Mname.Text);
                sp_cmd.Parameters.AddWithValue("@Model_mtname", mystring);
                sp_cmd.Parameters.AddWithValue("@Model_name2", txtupdateModel);
                //SqlCommand cmd = new SqlCommand("update Model_table set Model_name='" + txt_Mname.Text + "', Model_mtname='" + mystring + "' Where Model_name='" + txtupdateModel + "'", con);
                sp_cmd.ExecuteNonQuery();
                con.Close();
                // MessageBox.Show("" + txt_Mname.Text + " Model is Updated", "Update");
                btn_M_save.Enabled = true;
                txt_Mname.Text = string.Empty;
                btn_M_Delete.Enabled = false;
                btn_M_Update.Enabled = false;
                loadModel();
                txt_Mname.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void btn_M_Delete_Click(object sender, EventArgs e)
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
                    string BrandNoqry = "select Model_no from Model_table where Model_name=@tName";
                    SqlCommand cmdBrand= new SqlCommand(BrandNoqry,con);
                    cmdBrand.Parameters.AddWithValue("@tName", txtupdateModel);
                    string BrandNO=cmdBrand.ExecuteScalar().ToString();
                    con.Close();
                    con.Open();
                    string GetchkBrandCode = " select * from Item_table where Model_no=@tModelNo";
                    SqlCommand cmdGetChkBrandCode = new SqlCommand(GetchkBrandCode, con);
                    cmdGetChkBrandCode.Parameters.AddWithValue("@tModelNo", BrandNO);
                    var UsedBrandNo = cmdGetChkBrandCode.ExecuteScalar();
                    con.Close();
                    if (UsedBrandNo == null)
                    {

                        string result = MyMessageBox1.ShowBox("Do you want delete this Model?", "Delete");
                        if (result.Equals("1"))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand sp_cmd = new SqlCommand("sp_Model_Delete", con);
                            sp_cmd.CommandType = CommandType.StoredProcedure;
                            sp_cmd.Parameters.AddWithValue("@Model_name", txtupdateModel);
                            //SqlCommand cmd = new SqlCommand("delete from Model_table Where Model_name='" + txtupdateModel + "'", con);
                            sp_cmd.ExecuteNonQuery();
                            con.Close();
                            txt_Mname.Clear();

                        }
                        if (result.Equals("2"))
                        {
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Sorry ! " + txtupdateModel + " Model is currently in Use", "Warning");
                    }

                }
                btn_M_save.Enabled = true;
                txt_Mname.Text = string.Empty;
                btn_M_Delete.Enabled = false;
                btn_M_Update.Enabled = false;
                loadModel();
                txt_Mname.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }

        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            btn_M_save.Enabled = true;
            txt_Mname.Text = string.Empty;
            btn_M_Delete.Enabled = false;
            btn_M_Update.Enabled = false;
            loadModel();

        }

        private void Model_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


        private void txt_Mname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (btn_M_save.Enabled == true)
                {
                    btn_M_save.Select();
                }
                else
                {
                    btn_M_Update.Select();
                }
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                btn_M_Exit.Select();
            }
        }

        private void btn_M_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                funSave();
                txt_Mname.Select();
            }

            if (e.KeyCode == Keys.Tab)
            {
                btn_Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txt_Mname.Select();
            }
        }

        private void btn_M_Update_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btn_M_Delete.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txt_Mname.Select();
            }
        }

        private void btn_M_Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btn_Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                btn_M_Update.Select();
            }
        }

        private void btn_Clear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btn_M_Exit.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (btn_M_Update.Enabled == false)
                {
                    btn_M_save.Select();
                }
                else
                {
                    btn_M_Delete.Select();
                }
            }
        }

        private void btn_M_Exit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txt_Mname.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                btn_Clear.Select();
            }
        }

        private void Model_Load(object sender, EventArgs e)
        {
            txt_Mname.Select();
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

    
    }
}
