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
    public partial class Rack : Form
    {
      // SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
   //    SqlConnection con = new SqlConnection(@"Data Source=ASTRID-PC\SQLEXPRESS;Initial Catalog=Mspos;Persist Security Info=True;User ID=sa;password=!Password123");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        string txtupdateModel = string.Empty;
        public Rack()
        {
            InitializeComponent();
            loadRack();
            btn_Rack_Delete.Enabled = false;

            btn_Rack_Update.Enabled = false;
        }
        public void loadRack()
        {
            try
            {
                pnl_rack.Controls.Clear();

                SqlCommand cmd = new SqlCommand(" select Rack_name from Rack_table", con);
                con.Close();
                con.Open();
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {

                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dr["Rack_name"].ToString();
                    newBtn.Name = "Rack_name" + i;
                    newBtn.Width = 170;
                    newBtn.Height = 30;
                   // newBtn.Paint += new PaintEventHandler(newBtn_Pain);
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);                  
                  //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                   // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);

                    newBtn.Click += new EventHandler(newBtnRackItem_Click);
                    pnl_rack.Controls.Add(newBtn);
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
        private void newBtnRackItem_Click(object sender, EventArgs e)
        {
            btn_Rack_Delete.Enabled = true;
            btn_Rack_Update.Enabled = true;
            btn_Rack_save.Enabled = false;
            Button ClickedButton = (Button)sender;
            txt_Rname.Text = ClickedButton.Text.ToString();

            if (txt_Rname.Text != "")
            {
                txtupdateModel = ClickedButton.Text.ToString();          

            }
          txt_Rname.Select();
          
        }

        bool result =false;
        private void btn_Rack_save_Click(object sender, EventArgs e)
        {
            funSave();
        }
        void funSave()
        {
            try
            {
                if (txt_Rname.Text.Trim() == "")
                {
                  MyMessageBox.ShowBox("Please Enter Rack Name", "Warning");
                }
                else
                {
                    // SqlCommand cmd1 = new SqlCommand("Select * from Rack_Table where Rack_Name='" +txt_Rname.Text.Trim() + "'", con);
                    //con.Close();
                    //con.Open();
                    //dr = cmd1.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    MyMessageBox.ShowBox("Rack Name Already Exist", "Warning");
                    //}
                    //else
                    //{
                        //int grp_pos;
                    con.Close();
                    //con.Open();
                    //string bnoqry = "select max(Rack_no)+1 from Numbertable";
                    //SqlCommand bno = new SqlCommand(bnoqry, con);
                    //int b_no = Convert.ToInt32(bno.ExecuteScalar());
                    //con.Close();

                    //con.Open();
                    //string grpos = "select max(GroupPos)+1 from Rack_table";
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
                    //con.Close();

                    //string a = txt_Rname.Text;
                    //string b = a.ToUpper();

                    //string mystring = b;
                    //mystring = mystring.Replace(" ", "");

                    con.Open();
                    SqlCommand sp_cmd = new SqlCommand("sp_Rack_Insert", con);
                    sp_cmd.CommandType = CommandType.StoredProcedure;

                    sp_cmd.Parameters.AddWithValue("@tRackName", txt_Rname.Text);
                        sp_cmd.Parameters.Add("@chk", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //SqlCommand cmd = new SqlCommand("insert into Rack_table(Rack_no,Rack_name,Rack_mtname,Rack_level,Rack_under,Rack_gno,Rack_flag,Std_Group,GroupPos) values('" + b_no + "','" + txt_Rname.Text + "','" + mystring + "','0','0', '0','0','0','" + grp_pos + "')", con);
                   sp_cmd.ExecuteNonQuery();
                   MyMessageBox.Showbox("The RackName Saved Successfully","Message");
                   result = Convert.ToBoolean(sp_cmd.Parameters["@chk"].Value);
                    con.Close();

                    if (result == true)
                    {
                        MessageBox.Show("Track Name already exists");
                        con.Close();
                    }
                    else
                    {
                        txt_Rname.Text = string.Empty;
                        loadRack();
                        con.Close();
                    }

                  //  MessageBox.Show("Rack Saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //SqlCommand cmd11 = new SqlCommand("update NumberTable set Rack_no=Rack_no+1", con);
                    //con.Close();
                    //con.Open();
                    //cmd11.ExecuteNonQuery();
                    //con.Close();
                    }
                //}
            }
            catch (Exception ex)
            {
              MyMessageBox.ShowBox(ex.Message, "Error");
               // this.Close();
            }
        }
        private void btn_Rack__Exit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btn_Rack__Clear_Click(object sender, EventArgs e)
        {
            btn_Rack_save.Enabled = true;
            txt_Rname.Text = string.Empty;
            btn_Rack_Delete.Enabled = false;
            btn_Rack_Update.Enabled = false;
            loadRack();
        }

        private void btn_Rack_Delete_Click(object sender, EventArgs e)
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
                    string RackNoqry="select Rack_no from Rack_table where Rack_name=@tName";
                    SqlCommand cmdRack= new SqlCommand(RackNoqry,con);
                    cmdRack.Parameters.AddWithValue("@tName", txtupdateModel);
                    string RackNO=cmdRack.ExecuteScalar().ToString();
                    con.Close();
                    con.Open();
                    string GetchkBrandCode = " select * from Item_table where Rack_no=@tRackNo";

                    SqlCommand cmdGetChkBrandCode = new SqlCommand(GetchkBrandCode, con);
                    cmdGetChkBrandCode.Parameters.AddWithValue("@tRackNo", RackNO);
                    var UsedRackNO = cmdGetChkBrandCode.ExecuteScalar();
                    con.Close();
                    if (UsedRackNO == null)
                    {

                        string result = MyMessageBox1.ShowBox("Do you want delete this Rack?", "Delete");
                        if (result.Equals("1"))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("delete from Rack_table Where Rack_name=@tName", con);
                            cmd.Parameters.AddWithValue("@tName", txtupdateModel);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            txt_Rname.Clear();

                        }
                        if (result.Equals("2"))
                        {
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Sorry ! " + txtupdateModel + " Rack is currently in Use", "Warning");
                    }
                }
                btn_Rack_save.Enabled = true;
                txt_Rname.Text = string.Empty;
                btn_Rack_Delete.Enabled = false;
                btn_Rack_Update.Enabled = false;
                loadRack();
                txt_Rname.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void btn_Rack_Update_Click(object sender, EventArgs e)
        {
            try
            {
                string a = txt_Rname.Text;
                string b = a.ToUpper();

                string mystring = b;
                mystring = mystring.Replace(" ", "");
                con.Close();
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_Rack_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tRack_name ", txt_Rname.Text);
                cmd.Parameters.AddWithValue("@tRack_mtname", mystring);
                cmd.Parameters.AddWithValue("@tRack_Name2 ", txtupdateModel);

                //SqlCommand cmd = new SqlCommand("update Rack_table set Rack_name='" + txt_Rname.Text + "', Rack_mtname='" + mystring + "' Where Rack_name='" + txtupdateModel + "'", con);
                cmd.ExecuteNonQuery();
                //MyMessageBox.Showbox("The RackName Updated Successfully", "Message");
                con.Close();
                // MessageBox.Show("" + txt_Rname.Text + " Rack is Updated", "Update");
                btn_Rack_save.Enabled = true;
                txt_Rname.Text = string.Empty;
                btn_Rack_Delete.Enabled = false;
                btn_Rack_Update.Enabled = false;
                loadRack();
                txt_Rname.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void Rack_Load(object sender, EventArgs e)
        {
            
            txt_Rname.Select();

           // For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

         private void txt_Rname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab )
            {
                if (btn_Rack_save.Enabled == true)
                {
                    btn_Rack_save.Select();
                }
                else
                {
                    btn_Rack_Update.Select();
                }
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               btn_Rack__Exit.Select();
            }
        }

        private void btn_Rack_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                funSave();
               txt_Rname.Select();
            }
            
            if (e.KeyCode == Keys.Tab)
            {
               btn_Rack__Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
              txt_Rname.Select();
            }
        }

        private void btn_Rack_Update_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
              btn_Rack_Delete.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               txt_Rname.Select();
            }
        }

        private void btn_Rack_Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
               btn_Rack__Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               btn_Rack_Update.Select();
            }
        }

        private void btn_Rack__Clear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
               btn_Rack__Exit.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (btn_Rack_Update.Enabled == false)
                {
                    btn_Rack_save.Select();
                }
                else
                {
                    btn_Rack_Delete.Select();
                }
            }
        }

        private void btn_Rack__Exit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
               txt_Rname.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
               btn_Rack__Clear.Select();
            }
        }

        private void Rack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Rack_Paint(object sender, PaintEventArgs e)
        {
            
        }      

        private void btn_Rack_save_Paint(object sender, PaintEventArgs e)
        {
           
        }    
    }
}
