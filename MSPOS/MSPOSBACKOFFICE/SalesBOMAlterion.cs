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
    public partial class SalesBOMAlterion : Form
    {
        public SalesBOMAlterion()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlCommand cmd = null;
        SqlDataAdapter adp = null;
        private void SalesBOMAlterion_Load(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SP_SelectQuery",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "SlectBOM");
            cmd.Parameters.AddWithValue("@itemName","");
            cmd.Parameters.AddWithValue("@ItemCode", "");
            adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstBomName.Items.Add(dt.Rows[i]["BOM_name"].ToString());
                }
            }

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool isChk;
        private void txtBomname_TextChanged(object sender, EventArgs e)
        {
            isChk = false;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType","SelectBOM_name");
            cmd.Parameters.AddWithValue("@itemName", txtBomname.Text);
            cmd.Parameters.AddWithValue("@ItemCode","");
            adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                isChk = true;
                string tempstr = dt.Rows[0]["BOM_name"].ToString();
                for (int k = 0; k < lstBomName.Items.Count; k++)
                {
                    if (tempstr == lstBomName.Items[k].ToString())
                    {
                        lstBomName.SetSelected(k, true);
                        txtBomname.Select();
                        chk = "1";
                        txtBomname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        break;
                    }
                }
            }
            if (isChk == false)
            {
                chk = "1";
                if (txtBomname.Text != "")
                {
                    string name = txtBomname.Text.Remove(txtBomname.Text.Length - 1);
                    txtBomname.Text = name.ToString();
                    txtBomname.Select(txtBomname.Text.Length, 0);
                }
                txtBomname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
            }
            else
            {
                chk = "1";
            }
        }
        string chk;
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

        private void lstBomName_Click(object sender, EventArgs e)
        {
            if (lstBomName.SelectedItems.Count > 0)
            {
                txtBomname.Text = lstBomName.SelectedItem.ToString();
                bomNumber();
            }
        }

        private void txtBomname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lstBomName.SelectedItems.Count > 0)
                {
                    txtBomname.Text = lstBomName.SelectedItem.ToString();
                    bomNumber();
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                if (lstBomName.SelectedIndex < lstBomName.Items.Count - 1)
                {
                    lstBomName.SetSelected(lstBomName.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstBomName.SelectedIndex > 0)
                {
                    lstBomName.SetSelected(lstBomName.SelectedIndex - 1, true);
                }
            }
        }
        string Bom_NO = "";
        public void bomNumber()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType","SelectBomNo");
            cmd.Parameters.AddWithValue("@ItemCode","");
            cmd.Parameters.AddWithValue("@itemName",txtBomname.Text);
            adp = new SqlDataAdapter(cmd);
            DataTable dtchk = new DataTable();
            dtchk.Rows.Clear();
            adp.Fill(dtchk);
            if (dtchk.Rows.Count > 0)
            {
                Bom_NO = dtchk.Rows[0]["BOM_No"].ToString();
                passingvalues passingvalues = new MSPOSBACKOFFICE.passingvalues();
                passingvalues.BOMNO = Bom_NO.ToString();
                SalesBOM frm = new SalesBOM();
                frm.MdiParent = this.ParentForm;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
        }
    }
}
