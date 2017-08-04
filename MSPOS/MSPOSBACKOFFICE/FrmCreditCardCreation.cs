using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Management;
using System.Configuration;

namespace MSPOSBACKOFFICE
{
    public partial class FrmCreditCardCreation : Form
    {
        public FrmCreditCardCreation()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Pos"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        private void txtLedgerGroupName_Enter(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                PnlLedgerGroup.Visible = Visible;
                SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "LedgerGroupSelection");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@ItemName", "");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                listLedgerGroup.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listLedgerGroup.Items.Add(dt.Rows[i]["Ledger_name"].ToString());
                    }
                }
            }
            catch
            { }
        }
        bool isChk = false;
        private void txtLedgerGroupName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txtLedgerGroupName.Text != string.Empty)
                {
                    PnlLedgerGroup.Visible = Visible;
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "SelectledgerLike");
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", txtLedgerGroupName.Text.Trim());

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt_selectitem = new DataTable();
                    dt_selectitem.Rows.Clear();
                    isChk = false;
                    adp.Fill(dt_selectitem);
                    if (dt_selectitem.Rows.Count > 0)
                    {
                        string tempstr = dt_selectitem.Rows[0]["Ledger_name"].ToString().Trim();
                        for (int k = 0; k < listLedgerGroup.Items.Count; k++)
                        {
                            if (tempstr == listLedgerGroup.Items[k].ToString().Trim())
                            {
                                isChk = true;
                                listLedgerGroup.SetSelected(k, true);
                                txtLedgerGroupName.Select();
                                chk = "1";
                                txtLedgerGroupName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtLedgerGroupName.Text != "")
                        {
                            string name = txtLedgerGroupName.Text.Remove(txtLedgerGroupName.Text.Length - 1);
                            txtLedgerGroupName.Text = name.ToString();
                            txtLedgerGroupName.Select(txtLedgerGroupName.Text.Length, 0);
                        }
                        txtLedgerGroupName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
            catch
            { }
        }
        string chk;
        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch
            { }
        }
        private void txtCreditCardName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
               
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                        if (txtCreditCardName.Text != string.Empty)
                        {
                            duplicate();
                        }
                        else
                        {
                             MyMessageBox.ShowBox("Please Enter Card Name", "Warning");
                            txtCreditCardName.Focus();
                          
                        }
                        if (empty=="")
                        {
                            txtLedgerGroupName.Focus();
                        }
                }
            }
            catch
            { }
        }
        string listActionType;
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (listLedgerGroup.SelectedIndex < listLedgerGroup.Items.Count - 1)
                    {
                        listLedgerGroup.SetSelected(listLedgerGroup.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (listLedgerGroup.SelectedIndex > 0)
                    {
                        listLedgerGroup.SetSelected(listLedgerGroup.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (listLedgerGroup.SelectedItems.Count > 0)
                    {
                        txtLedgerGroupName.Text = listLedgerGroup.SelectedItem.ToString();
                        PnlLedgerGroup.Visible = false;
                    }
                    if (txtLedgerGroupName.Text == string.Empty)
                    {
                        MyMessageBox.ShowBox("Please Enter Ledger Name", "Warning");
                        txtLedgerGroupName.Focus();
                    }
                    else
                    {
                        btnSave.Focus();
                    }
                    
                }
            }
            catch 
            { }
        }
        private void listLedgerGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (listLedgerGroup.SelectedItems.Count > 0)
                {
                    txtLedgerGroupName.Text = listLedgerGroup.SelectedItem.ToString();
                    PnlLedgerGroup.Visible = false;
                }
            }
            catch
            { }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            { }
        }
        private void txtCreditCardName_Enter(object sender, EventArgs e)
        {
            try
            {
                PnlLedgerGroup.Visible = false;
            }
            catch
            { }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (duplicate())
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SP_LedgerGroupIn", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CardName", txtCreditCardName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LedgerGroupName", txtLedgerGroupName.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MyMessageBox.ShowBox("Added Successfully", "Success");
                    btnClear_Click(sender, e);
                }
            }
            catch 
            { }
        }
        string empty = "";
        public bool duplicate()
        {
            empty = "";
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txtCreditCardName.Text == string.Empty)
                {
                   // MyMessageBox.ShowBox("Please Enter Card Name", "Warning");
                    txtCreditCardName.Focus();
                    return false;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DuplicateLedgerName");
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", txtCreditCardName.Text.Trim());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dtSelectDuplicate = new DataTable();
                    adp.Fill(dtSelectDuplicate);
                    if (dtSelectDuplicate.Rows.Count > 0)
                    {
                        MyMessageBox.ShowBox("Duplicate Card Name", "Warning");
                        txtCreditCardName.Focus();
                        empty = "1";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
        }
        private void txtCreditCardName_Leave(object sender, EventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreditCardName.Text = string.Empty;
                txtLedgerGroupName.Text = string.Empty;
                PnlLedgerGroup.Visible = false;
                txtCreditCardName.Focus();
            }
            catch
            { }
        }

        private void txtLedgerGroupName_Leave(object sender, EventArgs e)
        {
            
        }

        private void FrmCreditCardCreation_Load(object sender, EventArgs e)
        {
            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
           // Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
    }
}
