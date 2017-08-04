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
using System.IO;
using System.Configuration;
using System.Data.OleDb;
//using iTextSharp.text;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;

namespace MSPOSBACKOFFICE
{
    public partial class frmLedgerReport : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public frmLedgerReport()
        {
            InitializeComponent();

            try
            {
                foreach (DataGridViewColumn col in DtLedger.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                //dt.Columns.Add("Date", typeof(string));
                //dt.Columns.Add("Particulars", typeof(string));
                //dt.Columns.Add("BillNo", typeof(string));
                //dt.Columns.Add("Debit", typeof(string));
                //dt.Columns.Add("Amount", typeof(string));            

                DtLedger.DefaultCellStyle.Font = new Font("Tahoma", 12);
                DtLedger.RowTemplate.Height = 25;

                //DtLedger.Columns[0].Width = 150;
                //DtLedger.Columns[1].Width = 300;
                //DtLedger.Columns[2].Width = 150;
                //DtLedger.Columns[3].Width = 150;
                //DtLedger.Columns[4].Width = 200;
                //DtLedger.Columns["Type"].Visible = false;
                DtLedger.ReadOnly = true;

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_PRINT_Click(object sender, EventArgs e)
        {

        }

        private void txtfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txttodate.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Showbox(ex.Message, "Warning");
            }
        }

        private void txttodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtLedgerName.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Showbox(ex.Message, "Warning");
            }
        }

        private void txtparty_no_Click(object sender, EventArgs e)
        {

        }

        private void txtparty_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtparty_no_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown7(object sender, KeyEventArgs e)
        {

        }

        private void txtparty_no_Leave(object sender, EventArgs e)
        {

        }

        private void txtpurtype_Click(object sender, EventArgs e)
        {

        }

        private void txtpurtype_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpurtype_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown5(object sender, KeyEventArgs e)
        {

        }

        private void txtCounter_Click(object sender, EventArgs e)
        {

        }

        private void txtCounter_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCounter_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown6(object sender, KeyEventArgs e)
        {

        }

        private void txtinvoice_Click(object sender, EventArgs e)
        {

        }

        private void txtinvoice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtinvoice_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown4(object sender, KeyEventArgs e)
        {

        }

        private void billno_Enter(object sender, EventArgs e)
        {

        }

        private void billno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtremarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtremarks_Enter(object sender, EventArgs e)
        {

        }

        private void txtremarks_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtorder_Click(object sender, EventArgs e)
        {

        }

        private void txtorder_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtorder_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown3(object sender, KeyEventArgs e)
        {

        }

        private void txtbilltype_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbilltype_Enter(object sender, EventArgs e)
        {

        }

        private void txtbilltype_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtnotcancelled_Click(object sender, EventArgs e)
        {

        }

        private void txtnotcancelled_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnotcancelled_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown2(object sender, KeyEventArgs e)
        {

        }

        private void txttype_Click(object sender, EventArgs e)
        {

        }

        private void txttype_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttype_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown1(object sender, KeyEventArgs e)
        {

        }

        private void txtbill_type_Click(object sender, EventArgs e)
        {

        }

        private void txtbill_type_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbill_type_Enter(object sender, EventArgs e)
        {

        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvItemsparty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        DataTable dt = new DataTable();
        private void frmLedgerReport_Load(object sender, EventArgs e)
        {
            pnlCustomers.Visible = false;           
            SqlCommand cmd = new SqlCommand("Select * from Ledger_table where Ledger_no>14 order by Ledger_name", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt.Rows.Clear();
            lstLedgerName.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstLedgerName.Items.Add(dt.Rows[i]["Ledger_name"].ToString());
                }
            }


            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            //pnlNormal.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }

        private void txtLedgerName_TextChanged(object sender, EventArgs e)
        {
            if (txtLedgerName.Text.Trim() != null && txtLedgerName.Text.Trim() != "")
            {
                pnlCustomers.Visible = true;
                SqlCommand cmd = new SqlCommand("Select * from Ledger_table Where Ledger_no>14 and ledger_name like @LedgerName order by Ledger_name", con);
                cmd.Parameters.AddWithValue("@LedgerName", txtLedgerName.Text.Trim() + '%');
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtGroupLedgerSelect = new DataTable();
                dtGroupLedgerSelect.Rows.Clear();
                adp.Fill(dtGroupLedgerSelect);
                isChk = false;
                if (dtGroupLedgerSelect.Rows.Count > 0)
                {
                    string tempstr = dtGroupLedgerSelect.Rows[0]["Ledger_name"].ToString().Trim();
                    for (int k = 0; k < lstLedgerName.Items.Count; k++)
                    {
                        if (tempstr == lstLedgerName.Items[k].ToString().Trim())
                        {
                            isChk = true;
                            lstLedgerName.SetSelected(k, true);
                            txtLedgerName.Select();
                            chk = "1";
                            txtLedgerName.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                            break;
                        }
                    }
                }
                if (isChk == false)
                {
                    chk = "2";
                    if (txtLedgerName.Text != "")
                    {
                        string name = txtLedgerName.Text.Remove(txtLedgerName.Text.Length - 1);
                        txtLedgerName.Text = name.ToString();
                        txtLedgerName.Select(txtLedgerName.Text.Length, 0);
                    }
                    txtLedgerName.KeyPress += new KeyPressEventHandler(txtSelectControl_KeyPress);
                    chk = "1";
                }
                else
                {
                    chk = "1";
                }
            }
        }
        string chk;
        bool isChk = false;
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

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lstLedgerName.SelectedIndex < lstLedgerName.Items.Count - 1)
                    {
                        lstLedgerName.SetSelected(lstLedgerName.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lstLedgerName.SelectedIndex > 0)
                    {
                        lstLedgerName.SetSelected(lstLedgerName.SelectedIndex - 1, true);
                    }
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (lstLedgerName.Text != "" && lstLedgerName.Text != string.Empty)
                    {
                        txtLedgerName.Text = lstLedgerName.SelectedItem.ToString();
                        //ShowingForm();
                        funLoad();
                        pnlCustomers.Visible = false;
                    }                    
                }
                if(e.KeyCode==Keys.Escape)
                {
                    pnlCustomers.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Showbox(ex.Message, "Warning");
            }
        }

        private void lstLedgerName_Click(object sender, EventArgs e)
        {
            if (lstLedgerName.SelectedIndex > 0)
            {              
              txtLedgerName.Text = lstLedgerName.SelectedItem.ToString();
              pnlCustomers.Visible = false;
            }
        }
        double Tot = 0.00;
        public void funLoad()
        {
            try
            {
                lblTotalAmt.Text = "0.00";
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select smas_billdate as Date,smas_name as Particulars, smas_billno as BillNo,Smas_NetAmount as Debit from Salmas_table where smas_name ='" + txtLedgerName.Text.Trim() + "' and smas_billdate between @FromDate and @ToDate", con);
                cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ToDate", txttodate.Value.ToString("yyyy-MM-dd"));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Tot = 0.00;
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {                        
                        Tot += Convert.ToDouble(dt.Rows[i]["Debit"].ToString());
                    }
                    DtLedger.DataSource = null;
                    DtLedger.DataSource = dt.DefaultView;
                    lblTotalAmt.Text = Convert.ToString(Tot);
                }
                DataTable dt1 = new DataTable();
                //SqlCommand cmd1 = new SqlCommand("select Paymentdetail_table.EndOfday as Date,Ledger_table.Ledger_name as Particulars,Paymentdetail_table.Avaliable_Credit as Amt from Ledger_table,Paymentdetail_table where PaymentDetail_Id=(select MAX(PaymentDetail_Id) from Paymentdetail_table where Paymentdetail_table.EndOfDay between @FromDate and @ToDate and Ledger_no=(select Ledger_no from Ledger_table where Ledger_name='" + txtLedgerName.Text.Trim() + "')) and Ledger_table.Ledger_no=Paymentdetail_table.Ledger_no", con);
                SqlCommand cmd1 = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=32", con);
                cmd1.Parameters.AddWithValue("@tLedgerName", txtLedgerName.Text.Trim());
                //cmd1.Parameters.AddWithValue("@FromDate", txtfromdate.Value.ToString("yyyy-MM-dd"));
                //cmd1.Parameters.AddWithValue("@ToDate", txttodate.Value.ToString("yyyy-MM-dd"));
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    double tLimitAmt = (dt1.Rows[0]["Limit_Amount"].ToString() == "") ? 0.00 : double.Parse(dt1.Rows[0]["Limit_Amount"].ToString());
                    double tCreditAmt = (dt1.Rows[0]["CLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dt1.Rows[0]["CLimit_Amount"].ToString());
                    double tPaidAmt = (dt1.Rows[0]["PLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dt1.Rows[0]["PLimit_Amount"].ToString());
                    //txtHACBalanceDue.Text = string.Format("{0:0.00}", ((tCreditAmt - tPaidAmt) < 0) ? 0.00 : (tCreditAmt - tPaidAmt));
                    lblAvbCredit.Text = string.Format("{0:0.00}", (tLimitAmt - (tCreditAmt - tPaidAmt)));
                    //lblAvbCredit.Text = dt1.Rows[0]["Amt"].ToString();
                }
                else
                {
                    lblAvbCredit.Text = "0.00";
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.Showbox(ex.Message, "Warning");
            }
        }        
    }
}