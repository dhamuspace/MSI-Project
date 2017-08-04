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
    public partial class Unit : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
     //   SqlDataReader dr = null;
        string txtupdateModel = string.Empty;
        public Unit()
        {
            InitializeComponent();
            loadUnit();
            btn_unit_Delete.Enabled = false;
            btn_unit_Update.Enabled = false;
            txtDecimals.Text = "0";
        }

        public void loadUnit()
        {
            try
            {
                pnl_Unit.Controls.Clear();

                SqlCommand cmd = new SqlCommand(" select unit_name from unit_table", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                adp.Fill(dt);
                int i = 0;
                for (int j = 0; j < dt.Rows.Count;j++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dt.Rows[j]["unit_name"].ToString();
                    newBtn.Name = "unit_name" + i;
                    newBtn.Width = 170;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);
                    newBtn.Click += new EventHandler(newBtnunitItem_Click);
                    pnl_Unit.Controls.Add(newBtn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }
        public event System.EventHandler CustomerEventHandler;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_rack_Paint(object sender, PaintEventArgs e)
        {

        }
        private void newBtnunitItem_Click(object sender, EventArgs e)
        {
            try
            {
                btn_unit_Delete.Enabled = true;
                btn_unit_Update.Enabled = true;
                btn_unit_save.Enabled = false;
                Button ClickedButton = (Button)sender;
                ClickedButton.Font.Size.Equals(26);
                txt_Uname.Text = ClickedButton.Text.ToString();
                if (txt_Uname.Text != "")
                {
                    txtupdateModel = ClickedButton.Text.ToString();
                    SqlCommand cmd = new SqlCommand("Select * from unit_table where unit_name=@tName", con);
                    cmd.Parameters.AddWithValue("@tName", ClickedButton.Text.ToString().Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtDecimals.Text = dt.Rows[0]["unit_decimals"].ToString();
                        chkRoundQty.Checked = Convert.ToBoolean(dt.Rows[0]["unit_alias"].ToString());
                        chkWeightScale.Checked = Convert.ToBoolean(dt.Rows[0]["WeightScale"].ToString());
                    }
                    con.Close();
                }
                txt_Uname.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        bool weightScale = false;
        bool RoundQty = false;
        private void btn_unit_save_Click(object sender, EventArgs e)
        {
            funSave();
        }
        void funSave()
        {
            
            try
            {
                bool result = false;

                if (txt_Uname.Text.Trim() == "")
                {
                    MyMessageBox.ShowBox("Please Enter Unit Name", "Warning");
                }
                else
                {
                    int num = 0;
                    if (txtDecimals.Text != "")
                    {
                        num = Convert.ToInt16(txtDecimals.Text.ToString());
                    }
                    if (num < 5)
                    {

                        //SqlCommand cmd1 = new SqlCommand("Select * from Unit_Table where Unit_Name='" + txt_Uname.Text.Trim() + "'", con);
                        //con.Close();
                        //con.Open();
                        //dr = cmd1.ExecuteReader();
                        //if (dr.Read())
                        //{
                        //    MyMessageBox.ShowBox("Unit Name Already Exist", "Warning");
                        //}
                        //else
                        //{
                        con.Close();

                        //con.Open();
                        //string bnoqry = "select max(UnitID)+1 from Numbertable";
                        //SqlCommand bno = new SqlCommand(bnoqry, con);
                        //int b_no = Convert.ToInt32(bno.ExecuteScalar());
                        //con.Close();



                        //string a = txt_Uname.Text;
                        //string b = a.ToUpper();

                        //string mystring = b;
                        //mystring = mystring.Replace(" ", "");

                        con.Open();
                        //string temp;
                        //if (txtDecimals.Text.Trim() == "")
                        //{
                        //    temp = "0";
                        //}
                        //else
                        //{
                        //    temp = txtDecimals.Text.Trim();
                        //}

                        weightScale = chkWeightScale.Checked;
                        RoundQty = chkRoundQty.Checked;
                        SqlCommand sp_cmd = new SqlCommand("sp_Unit_Insert", con);
                        sp_cmd.CommandType = CommandType.StoredProcedure;
                        //sp_cmd.Parameters.AddWithValue("@unit_no", b_no);
                        sp_cmd.Parameters.AddWithValue("@tunit_name ", txt_Uname.Text);
                        //sp_cmd.Parameters.AddWithValue("@unit_printname ", mystring);
                        //sp_cmd.Parameters.AddWithValue("@unit_mtname ",mystring );
                        sp_cmd.Parameters.AddWithValue("@unit_alias", RoundQty);
                        //sp_cmd.Parameters.AddWithValue("@unit_flag ", '0');
                        sp_cmd.Parameters.AddWithValue("@tudecimal ", txtDecimals.Text.Trim());
                        sp_cmd.Parameters.AddWithValue("@tWeightScale ", weightScale);
                        sp_cmd.Parameters.Add("@chk", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //SqlCommand cmd = new SqlCommand("insert into unit_table(unit_no,unit_name,unit_printname,unit_mtname,unit_alias,unit_flag,unit_Decimals) values('" + b_no + "','" + txt_Uname.Text + "','" + mystring + "','" + mystring + "','0','0','"+temp+"')", con);
                        sp_cmd.ExecuteNonQuery();
                        result = Convert.ToBoolean(sp_cmd.Parameters["@chk"].Value);
                        con.Close();
                        //   MessageBox.Show("Unit Saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MyMessageBox1.ShowBox("Unit Name Saved Successfully", "Message");
                        if (result == true)
                        {
                            MessageBox.Show("Unit Name already exists");
                            con.Close();
                        }
                        else
                        {
                            txt_Uname.Text = string.Empty;
                            txtDecimals.Text = string.Empty;
                            chkWeightScale.Checked = false;
                            chkRoundQty.Checked = false;
                            loadUnit();
                            con.Close();
                        }
                        //SqlCommand cmd11 = new SqlCommand("update NumberTable set UnitId=UnitID+1", con);
                        //con.Close();
                        //con.Open();
                        //cmd11.ExecuteNonQuery();
                        //con.Close();
                    }
                    //}
                    else
                    {
                        MessageBox.Show("Can't save! Number of decimals are only 0-5");
                        txtDecimals.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }
        private void btn_Unit_Update_Click(object sender, EventArgs e)
        {
            try
            {
                    int num = 0;
                    if (txtDecimals.Text != "")
                    {
                        num = Convert.ToInt16(txtDecimals.Text.ToString());
                    }
                    if (num < 5)
                    {
                        string a = txt_Uname.Text;
                        string b = a.ToUpper();

                        string mystring = b;
                        mystring = mystring.Replace(" ", "");
                        con.Close();
                        con.Open();
                        string temp;
                        if (txtDecimals.Text.Trim() == "")
                        {
                            temp = "0";
                        }
                        else
                        {
                            temp = txtDecimals.Text.Trim();
                        }
                        weightScale = chkWeightScale.Checked;
                        RoundQty = chkRoundQty.Checked;
                        SqlCommand sp_cmd = new SqlCommand("sp_Unit_Update", con);
                        sp_cmd.CommandType = CommandType.StoredProcedure;
                        sp_cmd.Parameters.AddWithValue("@unit_name", txt_Uname.Text);
                        sp_cmd.Parameters.AddWithValue("@unit_mtname", mystring);
                        sp_cmd.Parameters.AddWithValue("@unit_Decimals", temp);
                        sp_cmd.Parameters.AddWithValue("@unit_alias ", RoundQty);
                        sp_cmd.Parameters.AddWithValue("@tWeightScale ", weightScale);
                        sp_cmd.Parameters.AddWithValue("@unit_Name2", txtupdateModel);
                        //SqlCommand cmd = new SqlCommand("update unit_table set unit_name='" + txt_Uname.Text + "', unit_mtname='" + mystring + "',unit_Decimals='"+temp+"' Where unit_name='" + txtupdateModel + "'", con);
                        sp_cmd.ExecuteNonQuery();
                        con.Close();
                        // MessageBox.Show("" + txt_Uname.Text + " Unit is Updated", "Update");
                        btn_unit_save.Enabled = true;
                        txt_Uname.Text = string.Empty;
                        txtDecimals.Text = string.Empty;
                        btn_unit_Delete.Enabled = false;
                        btn_unit_Update.Enabled = false;
                        chkWeightScale.Checked = false;
                        chkRoundQty.Checked = false;
                        loadUnit();
                        txt_Uname.Select();
                    }
                    else
                    {
                        MessageBox.Show("Can't update! Number of decimals are only 0-5");
                    }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void btn_unit_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtupdateModel == "Nos")
                {
                    MyMessageBox.ShowBox("Cannot Delete Standard Group", "Warning");
                }
                else
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    string BrandNoqry = "select unit_no from Unit_table where unit_name=@tName";
                    SqlCommand cmdBrand= new SqlCommand(BrandNoqry,con);
                    cmdBrand.Parameters.AddWithValue("@tName", txtupdateModel);
                    string BrandNO=cmdBrand.ExecuteScalar().ToString();
                    con.Close();
                    con.Open();
                    string GetchkBrandCode = " select * from Item_table where Unit_no=@tBrandNo";
                    SqlCommand cmdGetChkBrandCode = new SqlCommand(GetchkBrandCode, con);
                    cmdGetChkBrandCode.Parameters.AddWithValue("@tBrandNo", BrandNO);
                    var UsedBrandNo = cmdGetChkBrandCode.ExecuteScalar();
                    con.Close();
                    if (UsedBrandNo == null)
                    {

                        string result = MyMessageBox1.ShowBox("Do you want delete this Unit?", "Delete");
                        if (result.Equals("1"))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("delete from Unit_table Where Unit_name=@tName", con);
                            cmd.Parameters.AddWithValue("@tName", txtupdateModel);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            txt_Uname.Clear();

                        }
                        if (result.Equals("2"))
                        {
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Sorry ! " + txtupdateModel + " Unit is currently in Use", "Warning");
                    }
                }
                btn_unit_save.Enabled = true;
                txt_Uname.Text = string.Empty;
                txtDecimals.Text = string.Empty;
                btn_unit_Delete.Enabled = false;
                btn_unit_Update.Enabled = false;
                chkWeightScale.Checked=false;
                loadUnit();
                txt_Uname.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void btn_unit_Clear_Click(object sender, EventArgs e)
        {
            btn_unit_save.Enabled = true;
            txt_Uname.Text = string.Empty;
            txtDecimals.Text = string.Empty;
            btn_unit_Delete.Enabled = false;
            btn_unit_Update.Enabled = false;
            chkWeightScale.Checked = false;
            chkRoundQty.Checked = false;
            loadUnit();
        }

        private void btn_unit_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Unit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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
            txt_Uname.Select();
        }

       
        private void txt_Uname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDecimals.Focus();

                //if (btn_unit_save.Enabled == true)
                //{
                //    btn_unit_save.Select();
                //}
                //else
                //{
                //    btn_unit_Update.Select();
                //}
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                btn_unit_Exit.Select();
            }
        }

        private void btn_unit_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                funSave();
                txt_Uname.Select();
            }

            if (e.KeyCode == Keys.Tab)
            {
                btn_unit_Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txt_Uname.Select();
            }
        }

        private void btn_Unit_Update_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btn_unit_Delete.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                txt_Uname.Select();
            }
        }

        private void btn_unit_Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btn_unit_Clear.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                btn_unit_Update.Select();
            }
        }

        private void btn_unit_Clear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btn_unit_Exit.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                if (btn_unit_Update.Enabled == false)
                {
                    btn_unit_save.Select();
                }
                else
                {
                    btn_unit_Delete.Select();
                }
            }
        }

        private void btn_unit_Exit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txt_Uname.Select();
            }
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                btn_unit_Clear.Select();
            }
        }

        private void Unit_Load(object sender, EventArgs e)
        {
            txt_Uname.Select();

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void Unit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CustomerEventHandler != null)
            {
                CustomerEventHandler(sender, e);
            }
            this.Close();
            if (CustomerEventHandler != null)
            {
                CustomerEventHandler(sender, e);
            }
        }

        private void txtDecimals_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                chkWeightScale.Focus();
            }
        }

        private void txtDecimals_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtDecimals.MaxLength = 1;
            if (!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar)&& e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtDecimals_Click(object sender, EventArgs e)
        {
            if (txtDecimals.Text == "0")
            {
                txtDecimals.Text = "";
            }
        }

        private void txtDecimals_Leave(object sender, EventArgs e)
        {
            if (txtDecimals.Text == "")
            {
                txtDecimals.Text = "0";
            }
        }

        private void chkWeightScale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (btn_unit_save.Enabled == true)
                {
                    btn_unit_save.Select();
                }
                else
                {
                    btn_unit_Update.Select();
                }
            }
        }
    }
}
