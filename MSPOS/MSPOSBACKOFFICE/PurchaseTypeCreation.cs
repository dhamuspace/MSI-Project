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
using System.Globalization;


namespace MSPOSBACKOFFICE
{
    
    public partial class PurchaseTypeCreation : Form
    {
        public PurchaseTypeCreation()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlCommand cmd = null;
        SqlDataAdapter adp = null;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        public void clear()
        {
            txtPurchaseType.Text = string.Empty;
            txtPurUnder.Text = string.Empty;
            values = "0";
        }

        private void txtPurchaseType_Enter(object sender, EventArgs e)
        {
            pnlpurtype.Visible = false;

        }
        private void PurchaseTypeCreation_Load(object sender, EventArgs e)
        {
            pnlpurtype.Visible = false;
            pnlistload();

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //  Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
           // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        
        private void txtPurUnder_Enter(object sender, EventArgs e)
        {
            pnlpurtype.Visible = true;
        }
       // string itemnumber = "";
        private void txtPurUnder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lvPurchase.SelectedIndex < lvPurchase.Items.Count - 1)
                {
                    lvPurchase.SetSelected(lvPurchase.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (lvPurchase.SelectedIndex > 0)
                {
                    lvPurchase.SetSelected(lvPurchase.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (lvPurchase.SelectedItems.Count > 0)
                {
                    txtPurUnder.Text = lvPurchase.SelectedItem.ToString();

                    selectledgNo();

                }
                btnSave.Focus();
                pnlpurtype.Visible = false;
            }
        }
        string purUnder = "",PurtypeGno="",purTypeno="";
       // string cmdnew = "";
        int purLevel_type = 0;
        public void selectledgNo()
        {
            if (txtPurUnder.Text != string.Empty)
            {
                purUnder = "";
                PurtypeGno = ""; purTypeno = "";
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType","SedTypeNos");
                cmd.Parameters.AddWithValue("@itemName",txtPurUnder.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemCode", "");
                dt.Rows.Clear();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    purUnder = dt.Rows[0]["PurType_Under"].ToString();
                    PurtypeGno = dt.Rows[0]["PurType_Gno"].ToString();
                    purTypeno = dt.Rows[0]["PurType_No"].ToString();
                    purLevel_type = Convert.ToInt32(dt.Rows[0]["PurType_level"].ToString());
                    purLevel_type =purLevel_type+1;
                }
            }
        }
        string values = "0";
        private void btnSave_Click(object sender, EventArgs e)
        {
            values = "1";
            if (validate())
            {
                //getting number table max values purtype:
                cmd = new SqlCommand("Select (Max(purtype_no)+1) from numbertable", con);
                string purtype = "";
                purtype = Convert.ToString(cmd.ExecuteScalar()).ToString();

                //geting maxvalues from purtype_table:
                cmd = new SqlCommand("Select (Max(GroupPos)+1) from PurType_table", con);
                string purgroupno = "";
                purgroupno = Convert.ToString(cmd.ExecuteScalar().ToString());


                //update number_table purtype_no:
                cmd = new SqlCommand("Update NumberTable set purtype_no='" + purtype.ToString() + "'", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("insert into PurType_Table(PurType_No,PurType_Name,PurType_MTName,PurType_level,PurType_Under,PurType_Gno,PurType_flag,Std_Group,GroupPos) values(@PurtypeNo,@PurType_Name,@PurType_MTName,@PurType_level,@PurType_Under,@PurType_Gno,@PurType_flag,@Std_Group,@GroupPos)", con);
                
                cmd.Parameters.AddWithValue("@PurtypeNo", purtype.ToString());
                cmd.Parameters.AddWithValue("@PurType_Name", txtPurchaseType.Text);
                cmd.Parameters.AddWithValue("@PurType_MTName", txtPurchaseType.Text.ToUpper().ToString());
                cmd.Parameters.AddWithValue("@PurType_level", purLevel_type.ToString());
                cmd.Parameters.AddWithValue("@PurType_Under", purTypeno.ToString());
                cmd.Parameters.AddWithValue("@PurType_Gno", PurtypeGno.ToString());
                cmd.Parameters.AddWithValue("@PurType_flag", 0);
                cmd.Parameters.AddWithValue("@Std_Group", 0);
                cmd.Parameters.AddWithValue("@GroupPos", purgroupno);
                cmd.ExecuteNonQuery();
                clear();
            }
        }
        DataTable dt = new DataTable();
        public void pnlistload()
        {
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "SelectLedType");
            cmd.Parameters.AddWithValue("@itemName", "");
            cmd.Parameters.AddWithValue("@ItemCode", "");
            adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            lvPurchase.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lvPurchase.Items.Add(dt.Rows[i]["PurType_Name"].ToString());
                }
            }
        }
        string chk = "";
        private void txtPurUnder_TextChanged(object sender, EventArgs e)
        {

            bool isChk = false;
            if (txtPurUnder.Text.Trim() != null && txtPurUnder.Text.Trim() != "")
            {
                DataTable dt_unitTable = new DataTable();
                dt_unitTable.Rows.Clear();
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "Selectlikepurtype");
                //Here Put Suppliuer Name Means ItemName 
                cmd.Parameters.AddWithValue("ItemName", txtPurUnder.Text);
                cmd.Parameters.AddWithValue("ItemCode", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_unitTable.Rows.Clear();
                adp.Fill(dt_unitTable);
                if (dt_unitTable.Rows.Count > 0)
                {
                    isChk = true;
                    string tempstr = dt_unitTable.Rows[0]["PurType_Name"].ToString();
                    for (int k = 0; k < lvPurchase.Items.Count; k++)
                    {
                        if (tempstr == lvPurchase.Items[k].ToString())
                        {
                            lvPurchase.SetSelected(k, true);
                            txtPurUnder.Select();
                            chk = "1";
                            txtPurUnder.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk = "1";
                    if (txtPurUnder.Text != "")
                    {
                        string name = txtPurUnder.Text.Remove(txtPurUnder.Text.Length - 1);
                        txtPurUnder.Text = name.ToString();
                        txtPurUnder.Select(txtPurUnder.Text.Length, 0);
                    }
                    txtPurUnder.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                }
                else
                {
                    chk = "1";
                }
            }
        }
        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }
        private void lvPurchase_Click(object sender, EventArgs e)
        {
            if (lvPurchase.SelectedItems.Count > 0)
            {
                txtPurUnder.Text = lvPurchase.SelectedItem.ToString();
                selectledgNo();
            } 
        }
        private void txtPurchaseType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPurUnder.Focus();
                validate();
            }
        }
        private bool validate()
        {
            if (txtPurchaseType.Text.Trim() != "" || txtPurchaseType.Text != string.Empty)
            {
                //Duplicate Find:
               
                cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SelectLedTypeNos");

                cmd.Parameters.AddWithValue("itemName", txtPurchaseType.Text.Trim());
                cmd.Parameters.AddWithValue("ItemCode", "");
                adp = new SqlDataAdapter(cmd);
                DataTable dt_duplicate = new DataTable();
                dt_duplicate.Rows.Clear();
                adp.Fill(dt_duplicate);
                if (dt_duplicate.Rows.Count > 0)
                {
                    MyMessageBox.ShowBox("Duplicate GroupName", "Warning");
                    txtPurchaseType.Focus();
                    return false;
                }
            }
                if (values == "1")
                {
                    if (txtPurUnder.Text == string.Empty || txtPurUnder.Text == "")
                    {
                        MyMessageBox.ShowBox("Empty Under", "Warning");
                        txtPurUnder.Focus();
                        return false;
                    }
                }
            return true;
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
