using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Threading;
using System.Configuration;

namespace SalesProject
{
    public partial class FrmTicketDisplay : Form
    {
        public FrmTicketDisplay()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());

        bool RowInc = false;
        public void GridLoad()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                string Chkcommand = "";
                DataTable dtprnt = new DataTable();
                dtprnt.Rows.Clear();

                if (vKeyEnter == false && cmbStatus.Text == "" && txtSearch.Text == "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                 " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                 " where a.TicketNo = b.TicketNo " +
                                 " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == false && cmbStatus.Text == "All" && txtSearch.Text == "")
                {
                     Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                    " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                    " where a.TicketNo=b.TicketNo " +
                                    " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == false && cmbStatus.Text == "All" && txtSearch.Text != "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                    " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                    " where a.TicketNo=b.TicketNo and cast(a.TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  or a.BillNo='" + txtSearch.Text.Trim() + "' " +
                                    " or a.ContactNo='" + txtSearch.Text + "' or a.Name= '" + txtSearch.Text + "' " +
                                    " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == true && cmbStatus.Text == "" && txtSearch.Text == "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                 " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                 " where a.TicketNo = b.TicketNo " +
                                 " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == true && cmbStatus.Text == "All" && txtSearch.Text == "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                   " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                   " where a.TicketNo=b.TicketNo " +
                                   " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == true && cmbStatus.Text == "All" && txtSearch.Text != "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                    " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                    " where a.TicketNo=b.TicketNo and cast(a.TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  or a.BillNo='" + txtSearch.Text.Trim() + "' " +
                                    " or a.ContactNo='" + txtSearch.Text + "' or a.Name= '" + txtSearch.Text + "' " +
                                    " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == false && cmbStatus.Text != "" && txtSearch.Text == "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                 " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                 " where a.TicketNo = b.TicketNo and b.Status = '"+ cmbStatus.Text +"' " +
                                 " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == false && cmbStatus.Text != "" && txtSearch.Text != "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                     " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                     " where a.TicketNo=b.TicketNo  and b.Status = '" + cmbStatus.Text + "' and cast(a.TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  or a.BillNo='" + txtSearch.Text.Trim() + "' " +
                                     " or a.ContactNo='" + txtSearch.Text + "' or a.Name= '" + txtSearch.Text + "' " +
                                     " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                
                else if (vKeyEnter == true && cmbStatus.Text != "" && txtSearch.Text == "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                   " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                   " where a.TicketNo=b.TicketNo and b.Status = '" + cmbStatus.Text + "' " +
                                   " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == true && cmbStatus.Text != "" && txtSearch.Text != "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                    " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                    " where a.TicketNo=b.TicketNo and b.Status = '" + cmbStatus.Text + "' and cast(a.TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  or a.BillNo='" + txtSearch.Text.Trim() + "' " +
                                    " or a.ContactNo='" + txtSearch.Text + "' or a.Name= '" + txtSearch.Text + "' " +
                                    " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' ";
                }
                else if (vKeyEnter == true && cmbStatus.Text == "" && txtSearch.Text != "")
                {
                    Chkcommand = " Select a.TicketNo as [Ticket No],AssignDate as [Created Date],Name,ItemName as [Item Name],ContactNo,JobNo,ModifiedDate as [Modified Date],Status " +
                                    " from T_TicketIssueTable a,T_TicketIssueDetailsTable b " +
                                    " where a.TicketNo=b.TicketNo " +
                                    " and  AssignDate between '" + dtpFromDate.Value.Year + "/" + dtpFromDate.Value.Month + "/" + dtpFromDate.Value.Day + "' and '" + dtpToDate.Value.Year + "/" + dtpToDate.Value.Month + "/" + dtpToDate.Value.Day + "' " +
                                    " and cast(a.TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  or a.BillNo='" + txtSearch.Text.Trim() + "' " +
                                    " or a.ContactNo = '" + txtSearch.Text + "' or a.Name= '" + txtSearch.Text + "' " ;
                }
                

                SqlCommand cmdprnt = new SqlCommand(Chkcommand, con);
                SqlDataAdapter adptprnt = new SqlDataAdapter(cmdprnt);
                adptprnt.Fill(dtprnt);
                cmdprnt.ExecuteNonQuery();

                if (dtprnt.Rows.Count != 0)
                {
                    MyGridView.Rows.Clear();
                    for (int k = 0; k < dtprnt.Rows.Count; k++)
                    {
                        MyGridView.Rows.Add();
                        MyGridView.Rows[k].Cells[0].Value = dtprnt.Rows[k]["Ticket No"].ToString().Trim();
                        MyGridView.Rows[k].Cells[1].Value = dtprnt.Rows[k]["Created Date"].ToString().Trim();
                        MyGridView.Rows[k].Cells[2].Value = dtprnt.Rows[k]["Name"].ToString().Trim();
                        MyGridView.Rows[k].Cells[4].Value = dtprnt.Rows[k]["ContactNo"].ToString().Trim();
                        MyGridView.Rows[k].Cells[5].Value = dtprnt.Rows[k]["JobNo"].ToString().Trim();
                        MyGridView.Rows[k].Cells[6].Value = dtprnt.Rows[k]["Modified Date"].ToString().Trim();

                        if (vKeyEnter == false)
                        {
                            MyGridView.Rows[k].Cells[3].Value = dtprnt.Rows[k]["Item Name"].ToString().Trim();
                            MyGridView.Rows[k].Cells[7].Value = dtprnt.Rows[k]["Status"].ToString().Trim();
                        }
                        else
                        {
                            SqlCommand cmdSearch1 = new SqlCommand("select ItemName as [Item Name],Status FROM T_TicketIssueDetailsTable  WHERE TicketNo ='" + dtprnt.Rows[k]["Ticket No"].ToString().Trim() + "' ", con);
                            DataTable dtSearch1 = new DataTable();
                            dtSearch1.Rows.Clear();
                            SqlDataAdapter adpSearch1 = new SqlDataAdapter(cmdSearch1);
                            adpSearch1.Fill(dtSearch1);
                            if (dtSearch1.Rows.Count > 0)
                            {
                                for (int s = 0; s < dtSearch1.Rows.Count; s++)
                                {
                                    if (RowInc == false)
                                    {
                                        MyGridView.Rows[k].Cells[3].Value = dtSearch1.Rows[s]["Item Name"].ToString().Trim();
                                        MyGridView.Rows[k].Cells[7].Value = dtSearch1.Rows[s]["Status"].ToString().Trim();
                                    }
                                    else
                                    {
                                        MyGridView.Rows[k].Cells[3].Value = dtSearch1.Rows[s]["Item Name"].ToString().Trim();
                                        MyGridView.Rows[k].Cells[7].Value = dtSearch1.Rows[s]["Status"].ToString().Trim();
                                    }

                                    if (dtprnt.Rows.Count <= dtSearch1.Rows.Count)
                                    {
                                        MyGridView.Rows.Add();
                                        k = k + 1;
                                        RowInc = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MyGridView.Rows.Clear();
                    //MyGridView.DataSource = null;
                    MyMessageBox.ShowBox("Record Not Found", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            vTicketNo = "";
            FrmTIcketIssue frm = new FrmTIcketIssue();
            frm.Show();
        }

        DateTime CurrentDate;
        private void FrmTicketDisplay_Load(object sender, EventArgs e)
        {
            CurrentDate = DateTime.Now;
            dtpFromDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            dtpToDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            GridLoad();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            MyGridView.ReadOnly = false;
            GridLoad();
        }

        private void MyGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (MyGridView.Columns[e.ColumnIndex].Name == "Status")
            {

                {
                    if (e.RowIndex > -1 && e.ColumnIndex == this.MyGridView.Columns["Status"].Index)
                    {
                        if (e.Value != null)
                        {
                            string CNumColour = e.Value.ToString();

                            if (CNumColour == "Completed")
                            {
                                this.MyGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
                            }
                            else if (CNumColour == "Pending")
                            {
                                this.MyGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                            }
                            else if (CNumColour == "Delivered")
                            {
                                this.MyGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Blue;
                            }
                        }
                    }
                }
            }
        }

        bool vKeyEnter = false;
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    MyGridView.Rows.Clear();
                    if (txtSearch.Text != "")
                    {
                        vKeyEnter = true;
                        MyGridView.Rows.Clear();
                        GridLoad();
                        vKeyEnter = false;
                    }
                    else
                    {
                        MyGridView.Rows.Clear();
                        vKeyEnter = false;
                        MyMessageBox.ShowBox("Please enter the value in Search Box", "Warning");
                        txtSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            //MyGridView.DataSource = null;
            cmbStatus.Text ="";

            CurrentDate = DateTime.Now;
            dtpFromDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            dtpToDate.Text = Convert.ToString(CurrentDate.Day + "/" + CurrentDate.Month + "/" + CurrentDate.Year);
            MyGridView.Rows.Clear();
        }

        static string s_infoTxt = "";

        public static string vTicketNo
        {
            get { return s_infoTxt; }
            set { s_infoTxt = value; }
        }

        private void MyGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (MyGridView.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MyMessageBox.ShowBox("Records Not Found", "Warning");
                }
                else if (MyGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim() != "")
                {
                    vTicketNo = MyGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.Close();
                    FrmTIcketIssue tkt = new FrmTIcketIssue();
                    //tkt.Left += 170;
                    //tkt.Top += 10;
                    tkt.Show();
                }
                else
                {
                    MyMessageBox.ShowBox("Records Not Found", "Warning");
                }
            }
        }
    }
}
