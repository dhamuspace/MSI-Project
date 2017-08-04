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

namespace MSPOSBACKOFFICE
{
    public partial class BranchCreation : Form
    {
        public BranchCreation()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        string txtupdateModel = string.Empty;

        private void newBtnBranch_Click(object sender, EventArgs e)
        {
        
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            Button ClickedButton = (Button)sender;
            txtBranch.Text = ClickedButton.Text.ToString();

            if (txtBranch.Text != "")
            {
                txtupdateModel = ClickedButton.Text.ToString();
            }
            txtBranch.Select();
        }

        public void loadBranch()
        {
            try
            {
                pnl_Counter.Controls.Clear();

                SqlCommand cmd = new SqlCommand(" select Branch_name from Branch_table", con);
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {

                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dr["Branch_name"].ToString();
                    newBtn.Name = "Branch_name" + i;
                    newBtn.Width = 180;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);
                    newBtn.Click += new EventHandler(newBtnBranch_Click);
                    pnl_Counter.Controls.Add(newBtn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBranch.Text != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand("select Branch_no from Branch_table where Branch_name=@tName", con);
                    cmd.Parameters.AddWithValue("@tName", txtBranch.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt=new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if(dt.Rows.Count>0)
                    {
                        MessageBox.Show("Branch name already exists");
                    }
                    else
                    {
                    con.Close();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand sp_cmd = new SqlCommand("sp_Branch_Insert", con);
                    sp_cmd.CommandType = CommandType.StoredProcedure;
                    sp_cmd.Parameters.AddWithValue("@tBranchName", txtBranch.Text.Trim());
                    sp_cmd.ExecuteNonQuery();
                    MyMessageBox.ShowBox("Branch Saved Successfully","Message");
                    con.Close();
                    loadBranch();
                    txtBranch.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Enter Branch name");
                    txtBranch.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BranchCreation_Load(object sender, EventArgs e)
        {
            loadBranch();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;


            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtBranch.Text = "";
            txtBranch.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string a = txtBranch.Text;
                string b = a.ToUpper();

                string mystring = b;
                mystring = mystring.Replace(" ", "");
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand sp_cmd = new SqlCommand("sp_Branch_Update", con);
                sp_cmd.CommandType = CommandType.StoredProcedure;
                sp_cmd.Parameters.AddWithValue("@Branch_Name", txtBranch.Text);
                sp_cmd.Parameters.AddWithValue("@Branch_mtname", mystring);
                sp_cmd.Parameters.AddWithValue("@Branch_Name2", txtupdateModel);
                //SqlCommand cmd = new SqlCommand("update Brand_table set Brand_name='" + txt_Bname.Text + "', Brand_mtname='" + mystring + "' Where Brand_name='" + txtupdateModel + "'", con);
                sp_cmd.ExecuteNonQuery();
                MyMessageBox.ShowBox("Branch Updated Successfully", "Message");
                con.Close();
                // MessageBox.Show("" + txt_Bname.Text + " Brand is Updated", "Update");
                btnSave.Enabled = true;
                txtBranch.Text = string.Empty;
             
                btnUpdate.Enabled = false;
                loadBranch();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }
        public event System.EventHandler CustomerEventHandler;
        private void txtBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CustomerEventHandler != null)
            {
                CustomerEventHandler(sender, e);
            }
           // this.Close();
            if (CustomerEventHandler != null)
            {
                CustomerEventHandler(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBranch.Text != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    string Branchno = "select Branch_no from branch_table where Branch_name=@tBranch";
                    SqlCommand cmdBranch = new SqlCommand(Branchno, con);
                    cmdBranch.Parameters.AddWithValue("@tBranch", txtBranch.Text);
                    string BranchNO = cmdBranch.ExecuteScalar().ToString();
                    if (BranchNO != "1")
                    {
                        con.Close();
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                       // string GetchkBranchNo = "select distinct Branch_no from stktrn_table where Branch_no<>1 and Branch_no=@tBranchNo";
                        string GetchkBranchNo = "select Branch_no from User_table where Branch_no=@tBranchNo";
                        SqlCommand cmdGetChkBranchNo = new SqlCommand(GetchkBranchNo, con);
                        cmdGetChkBranchNo.Parameters.AddWithValue("@tBranchNo", BranchNO);
                        var StkrnBranchNo = cmdGetChkBranchNo.ExecuteScalar();
                        con.Close(); 
                        if (StkrnBranchNo == null)
                        {
                            string result = MyMessageBox1.ShowBox("Do you want delete this Branch?", "Delete");
                            if (result.Equals("1"))
                            {
                                con.Close();
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Open();
                                }
                                SqlCommand sp_cmd = new SqlCommand("delete from Branch_table Where Branch_name=@Branch_Name", con);
                                //  sp_cmd.CommandType = CommandType.StoredProcedure;
                                sp_cmd.Parameters.AddWithValue("@Branch_Name", txtBranch.Text);                                
                                sp_cmd.ExecuteNonQuery();
                                string bno='B'+BranchNO;
                                SqlCommand cmd1 = new SqlCommand("alter table item_table drop column "+bno+"", con);                                
                                cmd1.ExecuteNonQuery();
                                //int brno = Convert.ToInt32(bno) - 1;
                                //SqlCommand cmd2 = new SqlCommand("update number_table set branch_no="+brno+"", con);                                
                                //cmd2.ExecuteNonQuery();

                                con.Close();
                                txtBranch.Clear();
                                loadBranch();
                                btnDelete.Enabled = false;
                            }
                            if (result.Equals("2"))
                            {
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Sorry ! " + txtBranch.Text + " is currently in Use", "Warning");
                            txtBranch.Text = "";
                            txtBranch.Focus();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("This is default Branch", "Warning");
                    }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Select The BranchName", "Warning");
                    }
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void txtBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnSave.Focus();
            }
        }
                
    }
}
