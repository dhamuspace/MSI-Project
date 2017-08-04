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
    public partial class FrmLedgerAlteration : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public FrmLedgerAlteration()
        {
            InitializeComponent();
            //frmLedgerCreation obj = new frmLedgerCreation();
            //obj.funLedgerLoadEventClick+=new funLedgerLoadEvent(obj_funLedgerLoadEventClick);
        }

        private void frm_funLedgerLoadEventClick(object sender, EventArgs e)
        {
            try
            {
                txtLPName.Text = "";
                SqlCommand cmd = new SqlCommand("Select * from Ledger_table where Ledger_no>14 order by Ledger_name", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                lstLedgerAlter.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstLedgerAlter.Items.Add(dt.Rows[i]["Ledger_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void txtLPName_TextChanged(object sender, EventArgs e)
        {
            if (txtLPName.Text.Trim() != null && txtLPName.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("Select * from Ledger_table Where Ledger_no>14 and ledger_name like @LedgerName order by Ledger_name", con);
                cmd.Parameters.AddWithValue("@LedgerName", txtLPName.Text.Trim() + '%');
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroupLedgerSelect = new DataTable();
                dtGroupLedgerSelect.Rows.Clear();
                adp.Fill(dtGroupLedgerSelect);
                isChk = false;
                if (dtGroupLedgerSelect.Rows.Count > 0)
                {
                    string tempstr = dtGroupLedgerSelect.Rows[0]["Ledger_name"].ToString().Trim();
                    for (int k = 0; k < lstLedgerAlter.Items.Count; k++)
                    {
                        if (tempstr == lstLedgerAlter.Items[k].ToString().Trim())
                        {
                            isChk = true;
                            lstLedgerAlter.SetSelected(k, true);
                            txtLPName.Select();
                            chk = "1";
                            txtLPName.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk = "2";
                    if (txtLPName.Text != "")
                    {
                        string name = txtLPName.Text.Remove(txtLPName.Text.Length - 1);
                        txtLPName.Text = name.ToString();
                        txtLPName.Select(txtLPName.Text.Length, 0);
                    }
                    txtLPName.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    chk = "1";
                }
                else
                {
                    chk = "1";
                }
            }
        }
        string  chk;
        private void txtSelectControl_KeyPress(object sender, KeyPressEventArgs e)
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
        private void OnTextBoxKeyDown2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstLedgerAlter.SelectedIndex < lstLedgerAlter.Items.Count - 1)
                {
                    lstLedgerAlter.SetSelected(lstLedgerAlter.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstLedgerAlter.SelectedIndex > 0)
                {
                    lstLedgerAlter.SetSelected(lstLedgerAlter.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (lstLedgerAlter.Text != "" && lstLedgerAlter.Text != string.Empty)
                {
                    txtLPName.Text = lstLedgerAlter.SelectedItem.ToString();
                    ShowingForm();
                }
                txtLPName.Focus();
            }
        }
        bool isChk = false;
        DataTable dt = new DataTable();
        private void FrmLedgerAlteration_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Ledger_table where Ledger_no>14 order by Ledger_name", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            lstLedgerAlter.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstLedgerAlter.Items.Add(dt.Rows[i]["Ledger_name"].ToString());
                }
            }

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            txtLPName.Focus();
        }
        public void ShowingForm()
        {
            if (lstLedgerAlter.SelectedItems.Count > 0)
            {
                txtLPName.Text = lstLedgerAlter.SelectedItem.ToString();
            }
            passingvalues.LedgerName = txtLPName.Text.ToString();
            
            txtLPName.Focus();
            frmLedgerCreation frm = new frmLedgerCreation();
            frm.funLedgerLoadEventClick +=new EventHandler(frm_funLedgerLoadEventClick);
            frm.MdiParent = this.ParentForm;
            frm.StartPosition = FormStartPosition.Manual;
            frm.WindowState = FormWindowState.Normal;
            frm.Location = new Point(0, 80);
            frm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstLedgerAlter_Click(object sender, EventArgs e)
        {
                ShowingForm();
        }
    }
}
