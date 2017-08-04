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
    public partial class CounterCreation : Form
    {
        public CounterCreation()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        string txtupdateModel = string.Empty;

        private void newBtnCounter_Click(object sender, EventArgs e)
        {
        
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            Button ClickedButton = (Button)sender;
            txtCounter.Text = ClickedButton.Text.ToString();

            if (txtCounter.Text != "")
            {
                txtupdateModel = ClickedButton.Text.ToString();
            }
            txtCounter.Select();
        }

        public void loadCounter()
        {
            try
            {
                pnl_Counter.Controls.Clear();

                SqlCommand cmd = new SqlCommand(" select ctr_name from counter_table", con);
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
                    newBtn.Text = dr["ctr_name"].ToString();
                    newBtn.Name = "ctr_name" + i;
                    newBtn.Width = 180;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);
                    newBtn.Click += new EventHandler(newBtnCounter_Click);
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
                if (txtCounter.Text != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand("select ctr_no from counter_table where ctr_name=@tName", con);
                    cmd.Parameters.AddWithValue("@tName", txtCounter.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt=new DataTable();
                    dt.Rows.Clear();
                    adp.Fill(dt);
                    if(dt.Rows.Count>0)
                    {
                        MessageBox.Show("Counter name already exists");
                    }
                    else
                    {
                    con.Close();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand sp_cmd = new SqlCommand("sp_Counter_Insert", con);
                    sp_cmd.CommandType = CommandType.StoredProcedure;
                    sp_cmd.Parameters.AddWithValue("@tCtrName", txtCounter.Text.Trim());
                    sp_cmd.ExecuteNonQuery();
                    MyMessageBox.ShowBox("Counter Saved Successfully","Message");
                    con.Close();
                    loadCounter();
                    txtCounter.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Enter counter name");
                    txtCounter.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CounterCreation_Load(object sender, EventArgs e)
        {
            loadCounter();
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
            txtCounter.Text = "";
            txtCounter.Focus();
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
                string a = txtCounter.Text;
                string b = a.ToUpper();

                string mystring = b;
                mystring = mystring.Replace(" ", "");
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand sp_cmd = new SqlCommand("sp_Counter_Update", con);
                sp_cmd.CommandType = CommandType.StoredProcedure;
                sp_cmd.Parameters.AddWithValue("@Counter_Name", txtCounter.Text);
                sp_cmd.Parameters.AddWithValue("@Counter_mtname", mystring);
                sp_cmd.Parameters.AddWithValue("@Counter_Name2", txtupdateModel);
                //SqlCommand cmd = new SqlCommand("update Brand_table set Brand_name='" + txt_Bname.Text + "', Brand_mtname='" + mystring + "' Where Brand_name='" + txtupdateModel + "'", con);
                sp_cmd.ExecuteNonQuery();
                MyMessageBox.ShowBox("Counter Updated Successfully", "Message");
                con.Close();
                // MessageBox.Show("" + txt_Bname.Text + " Brand is Updated", "Update");
                btnSave.Enabled = true;
                txtCounter.Text = string.Empty;
             
                btnUpdate.Enabled = false;
                loadCounter();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }
        public event System.EventHandler CustomerEventHandler;
        private void txtCounter_KeyPress(object sender, KeyPressEventArgs e)
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
                if (txtCounter.Text != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    string Counterno = "select ctr_no from counter_table where ctr_name=@tCounter";
                    SqlCommand cmdCounter = new SqlCommand(Counterno, con);
                    cmdCounter.Parameters.AddWithValue("@tCounter", txtCounter.Text);
                    string counterNO = cmdCounter.ExecuteScalar().ToString();
                    if (counterNO != "1")
                    {
                        con.Close();
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                       // string GetchkCounterNo = "select distinct ctr_no from stktrn_table where ctr_no<>1 and ctr_no=@tCounterNo";
                        string GetchkCounterNo = "select ctr_no from User_table where ctr_no=@tCounterNo";
                        SqlCommand cmdGetChkCounterNo = new SqlCommand(GetchkCounterNo, con);
                        cmdGetChkCounterNo.Parameters.AddWithValue("@tCounterNo", counterNO);
                        var StkrnCounterNo = cmdGetChkCounterNo.ExecuteScalar();
                        con.Close(); 
                        if (StkrnCounterNo == null)
                        {
                            string result = MyMessageBox1.ShowBox("Do you want delete this User?", "Delete");
                            if (result.Equals("1"))
                            {
                                con.Close();
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Open();
                                }
                                SqlCommand sp_cmd = new SqlCommand("delete from counter_table Where ctr_name=@Counter_Name", con);
                                //  sp_cmd.CommandType = CommandType.StoredProcedure;
                                sp_cmd.Parameters.AddWithValue("@Counter_Name", txtCounter.Text);                                
                                sp_cmd.ExecuteNonQuery();
                                con.Close();
                                txtCounter.Clear();
                                loadCounter();
                                btnDelete.Enabled = false;
                            }
                            if (result.Equals("2"))
                            {
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Sorry ! " + txtCounter.Text + " is currently in Use", "Warning");
                            txtCounter.Text = "";
                            txtCounter.Focus();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("This is default counter", "Warning");
                    }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Select The CounterName", "Warning");
                    }
                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
        }

        private void txtCounter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnSave.Focus();
            }
        }
                
    }
}
