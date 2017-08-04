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
using System.Text.RegularExpressions;


namespace SalesProject
{
    public partial class FrmTIcketIssue : Form
    {
        public FrmTIcketIssue()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());

        DataTable dtTicket = new DataTable();

        private void Pnl_Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Pnl_Footer_Paint(object sender, PaintEventArgs e)
        {

        }
        string txtAddr = string.Empty;
        bool txtok = false;

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmTicketDisplay.vTicketNo = "";
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        bool FullyTyped = false;
        public void Clear()
        {
            txtAddress.Text = string.Empty;
            txtName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtContactNo.Text = "";
            txtEmailID.Text = "";
            txtJobNo.Text = "";
            txtTNo.Text = "";
            txtServiceBy.Text = "";
            txtSearch.Text = "";
            txtAmount.Text = "";
            txtDeposit.Text = "";
            txtBalance.Text = "";
            txtBillNo.Text = "";
            txtDate.Text = "";
            txtDate.Text = Convert.ToString(dtpModifiedDate.Value.Day + "/" + dtpModifiedDate.Value.Month + "/" + dtpModifiedDate.Value.Year);
            GrdTicketIsue.Rows.Clear();
            txtNotes.Text = string.Empty;
            txtNoteLine1.Text = "";
            txtNoteLine2.Text = "";
            txtNoteLine3.Text = "";
            mainStr = "";
            cmbTicketNo.Items.Clear();
            lblTktNo.Visible = false;
            cmbTicketNo.Visible = false;
            btnSave.Text = "Save";

            btnPrint.Visible = false;
            AutoID();
            txtName.Focus();            
        }
        private void FrmTIcketIssue_Load(object sender, EventArgs e)
        {
            AutoID();
            txtDate.Text= Convert.ToString(dateTimeBox.Value.Day + "/" + dateTimeBox.Value.Month + "/" + dateTimeBox.Value.Year);
            if (FrmTicketDisplay.vTicketNo != "")
            {
                txtSearch.Text = FrmTicketDisplay.vTicketNo;
                SearchLoad();
            }
            ActiveControl = txtName;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode==Keys.Down)
            {
                txtAddress.Focus();
                //txtAddress1.Focus();
            }
            
        }
        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtAddress2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtName.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                txtAddress1.SelectionStart = txtAddress1.Text.Length + 1;
                if (txtAddress1.Text == "")
                {

                    txtName.Focus();
                    strAdd1Len = string.Empty;
                }
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
      {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtAddress3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtAddress1.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                txtAddress2.SelectionStart = txtAddress2.Text.Length + 1;
                if (txtAddress2.Text == "")
                {
                    txtAddress1.Focus();
                    strAdd2Len = string.Empty;
                }
            }
        }

        private void txtAddress3_KeyDown(object sender, KeyEventArgs e)
       {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtEmailID.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtAddress2.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                txtAddress3.SelectionStart = txtAddress3.Text.Length + 1;
                if (txtAddress3.Text == "")
                {
                    txtAddress2.Focus();
                }
            }
        }

        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtContactNo.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                //txtAddress3.Focus();
                txtAddress.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtEmailID.Text == "")
                {
                    //txtAddress3.Focus();
                    txtAddress.Focus();
                }
            }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
               // txtJobNo.Focus();
                txtBillNo.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtEmailID.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtContactNo.Text == "")
                {
                    txtEmailID.Focus();
                }
            }
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtBillNo.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtContactNo.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtJobNo.Text == "")
                {
                    txtContactNo.Focus();
                }
            }
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtServiceBy.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtJobNo.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtBillNo.Text == "")
                {
                    txtJobNo.Focus();
                }
            }
        }

        private void txtServiceBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                //txtDeposit.Focus();
                GrdTicketIsue.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtBillNo.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtServiceBy.Text == "")
                {
                    txtBillNo.Focus();
                }
            }
        }

        private void txtDeposit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                btnSave.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtNotes.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtDeposit.Text != "")
                {
                    txtNotes.Focus();
                }
            }
        }

        private void txtNoteLine1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtNoteLine2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                GrdTicketIsue.Focus();
            }

        }

        private void txtNoteLine2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtNoteLine3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtNoteLine1.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtNoteLine2.Text == "")
                {
                    txtNoteLine1.Focus();
                }
            }
        }

        private void txtNoteLine3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtDeposit.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtNoteLine2.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtNoteLine3.Text == "")
                {
                    txtNoteLine2.Focus();
                }
            }
        }

        public bool txtValidation()
        {
            //if (txtName.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Name", "Warning");
            //    txtName.Focus();
            //    return false;
            //}
            //if (txtAddress1.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Address", "Warning");
            //    txtAddress1.Focus();
            //    return false;
            //}
            //if (txtEmailID.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Email", "Warning");
            //    txtEmailID.Focus();
            //    return false;
            //}
            if (txtContactNo.Text == "")
            {
                MyMessageBox.ShowBox("Enter The ContactNo", "Warning");
                txtContactNo.Focus();
                return false;
            }
            //if (txtJobNo.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The JobNo", "Warning");
            //    txtJobNo.Focus();
            //    return false;
            //}
            //if (txtTNo.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The TNo", "Warning");
            //    txtTNo.Focus();
            //    return false;
            //}
            //if (txtNoteLine1.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Note", "Warning");
            //    txtNoteLine1.Focus();
            //    return false;
            //}
            //if (txtAmount.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Amount", "Warning");
            //    txtAmount.Focus();
            //    return false;
            //}
            //if (txtDeposit.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Deposit", "Warning");
            //    txtDeposit.Focus();
            //    return false;
            //}

            //if (txtBalance.Text == "")
            //{
            //    MyMessageBox.ShowBox("Enter The Balance", "Warning");
            //    txtBalance.Focus();
            //    return false;
            //}
            if (txtServiceBy.Text == "")
            {
                MyMessageBox.ShowBox("Enter The Service By", "Warning");
                txtServiceBy.Focus();
                return false;
            }

            int RowCount = 0;
            if (GrdTicketIsue.Rows.Count <= 1)
            {
                RowCount = GrdTicketIsue.Rows.Count;
            }
            else
            {
                RowCount = GrdTicketIsue.Rows.Count-1;
            }

            for (int i = 0; i < RowCount; i++)
            {
                string strname = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value)) == true) ? "" :Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value);
                if (strname == "" || strname == null)
                {
                    MyMessageBox.ShowBox("Enter the item name", "Warning");
                    //GrdTicketIsue.Rows[i].Cells[0].Selected = true;
                   // GrdTicketIsue.CurrentCell = GrdTicketIsue.Rows[i].Cells[i];
                    return false;
                }

                string vStatus = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[4].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[4].Value);
                if (vStatus == "" || vStatus == null)
                {
                    MyMessageBox.ShowBox("Select The Status ", "Warning");
                    //GrdTicketIsue.Rows[i].Cells[4].Selected = true;
                    //GrdTicketIsue.CurrentCell = GrdTicketIsue.Rows[i].Cells[4];
                    return false;
                }
            }
                return true;
        }

        public bool Exists()
        {
            if (txtName.Text != string.Empty)
            {
                DataTable dtSavechk = new DataTable();
                dtSavechk.Rows.Clear();
                SqlCommand cmdSave = new SqlCommand("select * from T_TicketIssueTable where Name = '" + txtName.Text + "'", con);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmdSave);
                adp.Fill(dtSavechk);

                if (dtSavechk.Rows.Count > 0)
                {
                    MyMessageBox.ShowBox("This Name Already Exists", "Warning");
                    txtName.Text = "";
                    txtName.Focus();
                    return false;
                }
                //SqlCommand cmdSave1 = new SqlCommand("select * from T_TicketIssueTable where JobNo = '" + txtJobNo.Text + "'", con);
                //if (con.State != ConnectionState.Open)
                //{
                //    con.Open();
                //}
                //SqlDataAdapter adp1 = new SqlDataAdapter(cmdSave1);
                //dtSavechk.Clear();
                //adp1.Fill(dtSavechk);

                //if (dtSavechk.Rows.Count > 0)
                //{
                //    MyMessageBox.ShowBox("This Job No. Already Exists", "Warning");
                //    txtJobNo.Text = "";
                //    txtJobNo.Focus();
                //    return false;
                //}

                //SqlCommand cmdSave2 = new SqlCommand("select * from T_TicketIssueTable where BillNo = '" + txtBillNo.Text + "'", con);
                //if (con.State != ConnectionState.Open)
                //{
                //    con.Open();
                //}
                //SqlDataAdapter adp2 = new SqlDataAdapter(cmdSave2);
                //dtSavechk.Clear();
                //adp2.Fill(dtSavechk);

                //if (dtSavechk.Rows.Count > 0)
                //{
                //    MyMessageBox.ShowBox("This Bill No. Already Exists", "Warning");
                //    txtJobNo.Text = "";
                //    txtJobNo.Focus();
                //    return false;
                //}

                con.Close();
            }
            return true;
        }

        int a;
        string val;
        string str1 = "1";
        public void AutoID()
        {

            DataTable dt6 = new DataTable();
            SqlDataAdapter adpt1 = new SqlDataAdapter("select MAX(TicketNo) as TicketNo from T_TicketIssueTable", con);
            adpt1.Fill(dt6);
            //con.Close();
            str1 = Convert.ToString(dt6.Rows[0]["TicketNo"].ToString());
            if (!string.IsNullOrEmpty(str1))
            {
                lblTicketNo.Text = (Convert.ToInt32(str1) + 1).ToString();
                txtTNo.Text = (Convert.ToInt32(str1) + 1).ToString();
            }
            else
            {
                lblTicketNo.Text = "1";
                txtTNo.Text = "1";
            }
        }

        DateTime ModifiedDate = DateTime.Today;
        //DateTime vAssignTime;// = DateTime.Now.ToString("HH:mm:ss tt");
        DateTime vAssignTime = DateTime.Now; 

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    if (txtValidation())
                    {
                        //if (Exists())
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            AutoID();
                            SqlCommand cmdInsert = new SqlCommand("TicketIssueInsert", con);
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            cmdInsert.Connection = con;
                            cmdInsert.Parameters.AddWithValue("@TicketNo", lblTicketNo.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@AddressLine1", txtAddress.Text.Trim());
                            //cmdInsert.Parameters.AddWithValue("@AddressLine1", txtAddress1.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@AddressLine2", txtAddress2.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@AddressLine3", txtAddress3.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@Email", txtEmailID.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@AssignDate", Convert.ToString(dateTimeBox.Value.Month + "/" + dateTimeBox.Value.Day + "/" + dateTimeBox.Value.Year));
                            cmdInsert.Parameters.AddWithValue("@AssignTime", Convert.ToString(vAssignTime.ToShortTimeString()));
                            cmdInsert.Parameters.AddWithValue("@JobNo", txtJobNo.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@TNO", txtTNo.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@BillNo", txtBillNo.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@ServiceMan", txtServiceBy.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@Note1", txtNotes.Text.Trim());
                            //cmdInsert.Parameters.AddWithValue("@Note1", txtNoteLine1.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@Note2", txtNoteLine2.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@Note3", txtNoteLine3.Text.Trim());
                           // cmdInsert.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                            if (txtAmount.Text != "")
                            {
                                cmdInsert.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                            }
                            else
                            {
                                cmdInsert.Parameters.AddWithValue("@Amount", 0);
                            }
                            if (txtDeposit.Text != "")
                            {
                                cmdInsert.Parameters.AddWithValue("@Deposit", txtDeposit.Text.Trim());
                            }
                            else
                            {
                                cmdInsert.Parameters.AddWithValue("@Deposit", 0);
                            }
                            if (txtBalance.Text != "")
                            {
                                cmdInsert.Parameters.AddWithValue("@Balance", txtBalance.Text.Trim());
                            }
                            else
                            {
                                cmdInsert.Parameters.AddWithValue("@Balance", 0);
                            }
                           // cmdInsert.Parameters.AddWithValue("@Balance", txtBalance.Text.Trim());
                            cmdInsert.Parameters.AddWithValue("@ModifiedDate", Convert.ToString(dateTimeBox.Value.Month + "/" + dateTimeBox.Value.Day + "/" + dateTimeBox.Value.Year));
                            cmdInsert.Parameters.AddWithValue("@ModifiedTime", Convert.ToString(vAssignTime.ToShortTimeString()));
                            cmdInsert.ExecuteNonQuery();
                            //con.Close();
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            for (int i = 0; i < GrdTicketIsue.Rows.Count - 1; i++)
                            {
                                SqlCommand cmdsve = new SqlCommand("TicketIssueDetailsInsert", con);
                                cmdsve.CommandType = CommandType.StoredProcedure;
                                cmdsve.Connection = con;
                                //AutoID();
                                double tSalesQty=0.00;
                                //(string.IsNullOrEmpty(Convert.ToString(dtRowFilter1[k]["TotSaleQty"])) == true) ? 0 : Convert.ToDouble(Convert.ToString(dtRowFilter1[k]["TotSaleQty"]));
                                cmdsve.Parameters.AddWithValue("@TicketNo", lblTicketNo.Text.Trim());
                                //cmdsve.Parameters.AddWithValue("@ItemName", (GrdTicketIsue.Rows[i].Cells[0].Value.ToString()));
                                cmdsve.Parameters.AddWithValue("@ItemName", (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value)) == true) ? "" :Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value));
                               // cmdsve.Parameters.AddWithValue("@DescriptionIMEISN", (GrdTicketIsue.Rows[i].Cells[1].Value.ToString()));
                                cmdsve.Parameters.AddWithValue("@DescriptionIMEISN", (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[1].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[1].Value));
                                //cmdsve.Parameters.AddWithValue("@SNo", Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value.ToString()));
                                cmdsve.Parameters.AddWithValue("@SNo", (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value));
                               // cmdsve.Parameters.AddWithValue("@EstimatePrice", Convert.ToDecimal(GrdTicketIsue.Rows[i].Cells[3].Value.ToString()));
                                //tSalesQty = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[3].Value.ToString())) == null) ? 0 : Convert.ToDouble(Convert.ToString(GrdTicketIsue.Rows[i].Cells[3].Value.ToString()));
                                double Estimated = (GrdTicketIsue.Rows[i].Cells[3].Value == null ? 0 : (Convert.ToDouble(GrdTicketIsue.Rows[i].Cells[3].Value)));
                                cmdsve.Parameters.AddWithValue("@EstimatePrice", Estimated);
                                cmdsve.Parameters.AddWithValue("@Status", (GrdTicketIsue.Rows[i].Cells[4].Value.ToString()));
                                cmdsve.ExecuteNonQuery();
                            }
                            MyMessageBox.ShowBox("Record Saved Successfully", "Message");
                            string result1 = MyMessageBox1.ShowBox("Do You Want Print", "Message");
                            if (result1 == "1")
                            {
                                //varPrint = 1;
                                FunPrint();
                                mainStr = "";
                                //rpt.PrintToPrinter(0, true, 1, 0);
                            }


                            GrdTicketIsue.Rows.Clear();
                            txtName.Focus();
                            Clear();
                            con.Close();
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    if (txtValidation())
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlCommand cmdUpdate = new SqlCommand("TicketIssueUpdate", con);
                        cmdUpdate.CommandType = CommandType.StoredProcedure;
                        cmdUpdate.Connection = con;
                        cmdUpdate.Parameters.AddWithValue("@TicketNo", txtTNo.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@AddressLine1", txtAddress.Text.Trim());
                        //cmdUpdate.Parameters.AddWithValue("@AddressLine1", txtAddress1.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@AddressLine2", txtAddress2.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@AddressLine3", txtAddress3.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Email", txtEmailID.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@AssignDate", Convert.ToString(dateTimeBox.Value.Month + "/" + dateTimeBox.Value.Day + "/" + dateTimeBox.Value.Year));
                        cmdUpdate.Parameters.AddWithValue("@AssignTime", Convert.ToString(vAssignTime.ToShortTimeString()));
                        cmdUpdate.Parameters.AddWithValue("@JobNo", txtJobNo.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@TNO", txtTNo.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@BillNo", txtBillNo.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@ServiceMan", txtServiceBy.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Note1", txtNotes.Text.Trim());
                        //cmdUpdate.Parameters.AddWithValue("@Note1", txtNoteLine1.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Note2", txtNoteLine2.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@Note3", txtNoteLine3.Text.Trim());
                        if (txtAmount.Text != "")
                        {
                            cmdUpdate.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                        }
                        else
                        {
                            cmdUpdate.Parameters.AddWithValue("@Amount", 0);
                        }
                        if (txtDeposit.Text != "")
                        {
                            cmdUpdate.Parameters.AddWithValue("@Deposit", txtDeposit.Text.Trim());
                        }
                        else
                        {
                            cmdUpdate.Parameters.AddWithValue("@Deposit", 0);
                        }
                        if (txtBalance.Text != "")
                        {
                            cmdUpdate.Parameters.AddWithValue("@Balance", txtBalance.Text.Trim());
                        }
                        else
                        {
                            cmdUpdate.Parameters.AddWithValue("@Balance", 0);
                        }
                        //cmdUpdate.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                        //cmdUpdate.Parameters.AddWithValue("@Deposit", txtDeposit.Text.Trim());
                        //cmdUpdate.Parameters.AddWithValue("@Balance", txtBalance.Text.Trim());
                        cmdUpdate.Parameters.AddWithValue("@ModifiedDate", Convert.ToString(dtpModifiedDate.Value.Month + "/" + dtpModifiedDate.Value.Day + "/" + dtpModifiedDate.Value.Year));
                        cmdUpdate.Parameters.AddWithValue("@ModifiedTime", Convert.ToString(vAssignTime.ToShortTimeString()));
                        cmdUpdate.ExecuteNonQuery();
                        //con.Close();
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        SqlCommand cmdDelete = new SqlCommand("Delete from T_TicketIssueDetailsTable where TicketNo='" + txtTNo.Text.Trim() + "' ", con);
                        cmdDelete.ExecuteNonQuery();

                        for (int i = 0; i < GrdTicketIsue.Rows.Count - 1; i++)
                        {
                            string vIName=(string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value);
                            string VDesc=(string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[1].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[1].Value);
                            string vSno=(string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value);
                            string vStatus=(GrdTicketIsue.Rows[i].Cells[4].Value.ToString());
                            double EstimatedPrice = (GrdTicketIsue.Rows[i].Cells[3].Value == null ? 0 : (Convert.ToDouble(GrdTicketIsue.Rows[i].Cells[3].Value)));

                            if (vIName != "")
                            {
                                //DataTable dtCheck = new DataTable();
                                //SqlDataAdapter adpt1 = new SqlDataAdapter("select * from T_TicketIssueDetailsTable where TicketNo ='" + txtTNo.Text + "' and ItemName='" + vIName + "'", con);
                                //adpt1.Fill(dtCheck);
                                ////con.Close();
                                //if (dtCheck.Rows.Count > 0)
                                //{
                                //    str1 = Convert.ToString(dtCheck.Rows[0]["TicketNo"].ToString());
                                //}
                                //else
                                //{
                                //    str1 = "";
                                //}
                                //if (!string.IsNullOrEmpty(str1))
                                //{
                                //    SqlCommand cmdUpdate1 = new SqlCommand("Update T_TicketIssueDetailsTable set TicketNo='" + txtTNo.Text.Trim() + "',ItemName='" + vIName + "',DescriptionIMEISN='" + VDesc + "',SNo='" + vSno + "',EstimatePrice='" + EstimatedPrice + "',Status='" + vStatus + "' where TicketNo='" + txtTNo.Text.Trim() + "' and ItemName='" + vIName + "'", con);
                                //    cmdUpdate1.ExecuteNonQuery();
                                //}
                                //else
                                //{
                                    SqlCommand cmdUpdate1 = new SqlCommand("Insert into T_TicketIssueDetailsTable(TicketNo ,ItemName,DescriptionIMEISN,SNo,EstimatePrice,Status) " +
                                                                           " values('" + txtTNo.Text.Trim() + "','" + vIName + "','" + VDesc + "','" + vSno + "','" + EstimatedPrice + "','" + vStatus + "' )", con);
                                    cmdUpdate1.ExecuteNonQuery();
                                //}
                            }

                            //SqlCommand cmdUpdate1 = new SqlCommand("TicketIssueDetailsUpdate", con);
                            //SqlCommand cmdUpdate1 = new SqlCommand("Update T_TicketIssueDetailsTable set TicketNo='" + lblTicketNo.Text.Trim() + "',ItemName=@ItemName,DescriptionIMEISN=@DescriptionIMEISN,SNo=@SNo,EstimatePrice=@EstimatePrice,Status=@Status where TicketNo=@TicketNo and ItemName=@ItemName", con);
                         //   SqlCommand cmdUpdate1 = new SqlCommand("Update T_TicketIssueDetailsTable set TicketNo='" + txtTNo.Text.Trim() + "',ItemName='" + vIName + "',DescriptionIMEISN='" + VDesc + "',SNo='" + vSno + "',EstimatePrice='" + EstimatedPrice + "',Status='" + vStatus + "' where TicketNo='" + txtTNo.Text.Trim() + "' and ItemName='" + vIName + "'", con);

                           // cmdUpdate1.CommandType = CommandType.StoredProcedure;
                           // cmdUpdate1.Connection = con;
                            //AutoID();
                            //cmdUpdate1.Parameters.AddWithValue("@TicketNo", txtTNo.Text.Trim());
                            //cmdUpdate1.Parameters.AddWithValue("@ItemName", (GrdTicketIsue.Rows[i].Cells[0].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@DescriptionIMEISN", (GrdTicketIsue.Rows[i].Cells[1].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@SNo", Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@EstimatePrice", Convert.ToDecimal(GrdTicketIsue.Rows[i].Cells[3].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@Status", (GrdTicketIsue.Rows[i].Cells[4].Value.ToString()));
                            //cmdUpdate1.ExecuteNonQuery();
                            //cmdUpdate1.Parameters.AddWithValue("@TicketNo", lblTicketNo.Text.Trim());
                            ////cmdsve.Parameters.AddWithValue("@ItemName", (GrdTicketIsue.Rows[i].Cells[0].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@ItemName", (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[0].Value));
                            //// cmdsve.Parameters.AddWithValue("@DescriptionIMEISN", (GrdTicketIsue.Rows[i].Cells[1].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@DescriptionIMEISN", (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[1].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[1].Value));
                            ////cmdsve.Parameters.AddWithValue("@SNo", Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value.ToString()));
                            //cmdUpdate1.Parameters.AddWithValue("@SNo", (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[i].Cells[2].Value));
                            //// cmdsve.Parameters.AddWithValue("@EstimatePrice", Convert.ToDecimal(GrdTicketIsue.Rows[i].Cells[3].Value.ToString()));
                            ////tSalesQty = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[i].Cells[3].Value.ToString())) == null) ? 0 : Convert.ToDouble(Convert.ToString(GrdTicketIsue.Rows[i].Cells[3].Value.ToString()));
                            ////cmdUpdate1.Parameters.AddWithValue("@EstimatePrice", EstimatedPrice);
                            //cmdUpdate1.Parameters.AddWithValue("@Status", (GrdTicketIsue.Rows[i].Cells[4].Value.ToString()));
                          //  cmdUpdate1.ExecuteNonQuery();
                        }
                        MyMessageBox.ShowBox("Record Update Successfully", "Message");
                        GrdTicketIsue.Rows.Clear();
                        txtName.Focus();
                        Clear();
                        con.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtEmailID_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (txtEmailID.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmailID.Text))
                {
                    MyMessageBox.ShowBox("Please Enter the Correct Mailid", "Warning");
                    txtEmailID.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtDeposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtDeposit_TextChanged(object sender, EventArgs e)
        {
            decimal vAmount = 0;
            decimal vDeposit = 0;
            decimal vBalance = 0;

            if (txtAmount.Text != "" && txtDeposit.Text!="")
            {
                vAmount = Convert.ToDecimal((txtAmount.Text).ToString());
                vDeposit = Convert.ToDecimal((txtDeposit.Text).ToString());
                vBalance = vAmount - vDeposit;

                txtBalance.Text = vBalance.ToString();
            }
        }

        double tEstimatedPrice = 0.00, tTotEstimatedPrice = 0.00;
        private void GrdTicketIsue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            tEstimatedPrice = 0.00; tTotEstimatedPrice = 0.00;
            if (GrdTicketIsue.CurrentRow != null && e.ColumnIndex == 3)
            {
                if (GrdTicketIsue.Rows[GrdTicketIsue.CurrentCell.RowIndex].Cells[3].Value != null && GrdTicketIsue.Rows[GrdTicketIsue.CurrentCell.RowIndex].Cells[3].Value.ToString() != "")
                {
                    for (int i = 0; i < GrdTicketIsue.Rows.Count-1; i++)
                    {
                        tEstimatedPrice = (GrdTicketIsue.Rows[i].Cells[3].Value == null ? 0 : (Convert.ToDouble(GrdTicketIsue.Rows[i].Cells[3].Value)));
                        tTotEstimatedPrice = (tTotEstimatedPrice + tEstimatedPrice);
                        txtAmount.Text = Convert.ToString(tTotEstimatedPrice);
                    }
                    if (txtAmount.Text != "" && txtDeposit.Text != "")
                    {
                        txtBalance.Text = (Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtDeposit.Text)).ToString();
                    }
                    
                }
            }
        }


        //private string[] SplitByLenght(string s, int split)
        //{
        //    //Like using List because I can just add to it 
        //    List<string> list = new List<string>();

        //    // Integer Division
        //    int TimesThroughTheLoop = s.Length / split;


        //    for (int i = 0; i < TimesThroughTheLoop; i++)
        //    {
        //        list.Add(s.Substring(i * split, split));

        //    }

        //    // Pickup the end of the string
        //    if (TimesThroughTheLoop * split != s.Length)
        //    {
        //        list.Add(s.Substring(TimesThroughTheLoop * split));
        //    }

        //    return list.ToArray();
        //}

        double vTotAmount = 0; int SplittedTextLen = 0; string BalText1 = "";

        private void btnSum_Click(object sender, EventArgs e)
        {
            //string vDesc = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[0].Cells[1].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[0].Cells[1].Value);
            
            //string[] SplittedText = SplitByLenght(vDesc, 40); // Splitting
            //int counter = 0;
            //foreach (string item in SplittedText)
            //{
            //    if (item != "")
            //    {
            //        counter++;

            //    }
            //}
            
            //for (int s = 0; s < counter; s++)
            //{
                
            //    int index1 = SplittedText[s].LastIndexOf(" ");
            //    if (index1 != -1)
            //    {
            //        string[] vSpace = SplittedText[s].Split(' ');
            //        string lastword = vSpace[vSpace.Length - 1];
            //        if (lastword != "")
            //        {
            //            string Firstline = SplittedText[s].Replace(lastword, "");
            //            MessageBox.Show((s+1)+ " Line : " + Firstline);
            //            string BalText = lastword + SplittedText[s+1];

            //            string[] SplittedText1 = SplitByLenght(BalText, 40); // Splitting

            //            //for (int j = 0; j < 2; j++)
            //            //{
            //                int index2 = SplittedText1[0].LastIndexOf(" ");
            //                if (index2 != -1)
            //                {
            //                    string[] vSpace2 = SplittedText1[0].Split(' ');

            //                    string lastword2 = vSpace2[vSpace2.Length - 1];
            //                    if (lastword2 != "")
            //                    {
            //                        string SecondLine = SplittedText1[0].Replace(lastword2, "");
            //                        MessageBox.Show((s+2)+" Line : " + SecondLine);
            //                        BalText1 = lastword2 + SplittedText1[1];
            //                        s = s + 2;
            //                    }
            //                }
            //            //}
            //        }
            //    }
            //}

            //----------------------------------------------///
            // string[] words = SpliceText(FullString, 40);
            //if (txtAmount.Text != "")
            //{
            //vTotAmount = 0;
            //for (int i = 0; i < GrdTicketIsue.Rows.Count - 1; i++)
            //{

            //    vTotAmount = vTotAmount + (GrdTicketIsue.Rows[i].Cells[3].Value == null ? 0 : (Convert.ToDouble(GrdTicketIsue.Rows[i].Cells[3].Value)));
            //    txtAmount.Text = vTotAmount.ToString();
            //    txtDeposit.Focus();
            //}
            //}
  
      }
    
          

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmTicketDisplay tktDisplay = new FrmTicketDisplay();
            //tktDisplay.Left += 170;
            //tktDisplay.Top += 0;
            tktDisplay.Show();
        }

        public void SearchLoad()
        {
            try
            {
                if (txtSearch.Text == "" && cmbTicketNo.Text!="")
                {
                    MyMessageBox.ShowBox("Enter the SearchNo", "Warning");
                    txtSearch.Focus();
                }
                else
                {
                    txtName.Text = "";
                    txtAddress.Text = string.Empty;
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtAddress3.Text = "";
                    txtEmailID.Text = "";
                    txtContactNo.Text = "";
                    txtJobNo.Text = "";
                    txtTNo.Text = "";
                    txtBillNo.Text = "";
                    txtNotes.Text = string.Empty;
                    txtNoteLine1.Text = "";
                    txtNoteLine2.Text = "";
                    txtNoteLine3.Text = "";
                    txtAmount.Text = "";
                    txtDeposit.Text = "";
                    txtBalance.Text = "";
                    GrdTicketIsue.Rows.Clear();

                    int strlen = Convert.ToInt16(txtSearch.Text.Length);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //SqlCommand cmdSearch = new SqlCommand("select TicketNo,Name,AddressLine1, AddressLine2,AddressLine3,Email,ContactNo,AssignDate,TNO,JobNo,ServiceMan,BillNo, " +
                    //                                      " Note1,Note2,Note3,Amount,Deposit,Balance FROM T_TicketIssuetable " +
                    //                                      " WHERE cast(TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  or BillNo='" + txtSearch.Text.Trim() + "' or JobNo='" + txtSearch.Text.Trim() + "' " +
                    //                                      " or ContactNo='" + txtSearch.Text + "' or Name= '" + txtSearch.Text + "' ", con);
                    DataTable dtSearch = new DataTable();

                    if (cmbTicketNo.Text=="")
                    {
                        SqlCommand cmdSearch = new SqlCommand("select TicketNo,Name,AddressLine1, AddressLine2,AddressLine3,Email,ContactNo,AssignDate,TNO,JobNo,ServiceMan,BillNo, " +
                                                              " Note1,Note2,Note3,Amount,Deposit,Balance FROM T_TicketIssueTable " +
                                                              " WHERE cast(TicketNo as nvarchar) ='" + txtSearch.Text.Trim() + "'  " +
                                                              " or ContactNo='" + txtSearch.Text + "' or Name= '" + txtSearch.Text + "' ", con);
                        dtSearch.Rows.Clear();
                        SqlDataAdapter adpSearch = new SqlDataAdapter(cmdSearch);
                        adpSearch.Fill(dtSearch);
                    }
                    else
                    {
                        SqlCommand cmdSearch = new SqlCommand("select TicketNo,Name,AddressLine1, AddressLine2,AddressLine3,Email,ContactNo,AssignDate,TNO,JobNo,ServiceMan,BillNo, " +
                                                              " Note1,Note2,Note3,Amount,Deposit,Balance FROM T_TicketIssueTable " +
                                                              " WHERE TicketNo ='" + cmbTicketNo.Text + "' ", con);
                        dtSearch.Rows.Clear();
                        SqlDataAdapter adpSearch = new SqlDataAdapter(cmdSearch);
                        adpSearch.Fill(dtSearch);
                    }

                    if (dtSearch.Rows.Count > 0)
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        btnPrint.Visible = true;
                        txtName.Text = dtSearch.Rows[0]["Name"].ToString();
                        txtAddress.Text = dtSearch.Rows[0]["Addressline1"].ToString();
                        //txtAddress1.Text = dtSearch.Rows[0]["Addressline1"].ToString();
                        //txtAddress2.Text = dtSearch.Rows[0]["Addressline2"].ToString();
                        //txtAddress3.Text = dtSearch.Rows[0]["Addressline3"].ToString();
                        txtEmailID.Text = dtSearch.Rows[0]["Email"].ToString();
                        txtContactNo.Text = dtSearch.Rows[0]["ContactNo"].ToString();
                        txtJobNo.Text = dtSearch.Rows[0]["JobNo"].ToString();
                        dateTimeBox.Text = dtSearch.Rows[0]["AssignDate"].ToString();
                        txtDate.Text = Convert.ToString(dateTimeBox.Value.Day + "/" + dateTimeBox.Value.Month + "/" + dateTimeBox.Value.Year);
                        txtTNo.Text = dtSearch.Rows[0]["TNo"].ToString();
                        txtBillNo.Text = dtSearch.Rows[0]["Billno"].ToString();
                        txtServiceBy.Text = dtSearch.Rows[0]["ServiceMan"].ToString();
                        txtNotes.Text = dtSearch.Rows[0]["Note1"].ToString();
                        //txtNoteLine1.Text = dtSearch.Rows[0]["Note1"].ToString();
                        //txtNoteLine2.Text = dtSearch.Rows[0]["Note2"].ToString();
                        //txtNoteLine3.Text = dtSearch.Rows[0]["Note3"].ToString();
                        txtAmount.Text = dtSearch.Rows[0]["Amount"].ToString();
                        txtDeposit.Text = dtSearch.Rows[0]["Deposit"].ToString();
                        txtBalance.Text = dtSearch.Rows[0]["Balance"].ToString();

                        btnSave.Text = "Update";

                        SqlCommand cmdSearch1 = new SqlCommand("select ItemName,DescriptionIMEISN,SNo,EstimatePrice,Status FROM T_TicketIssueDetailsTable  WHERE TicketNo ='" + txtTNo.Text.Trim() + "' ", con);
                        DataTable dtSearch1 = new DataTable();
                        dtSearch1.Rows.Clear();
                        SqlDataAdapter adpSearch1 = new SqlDataAdapter(cmdSearch1);
                        adpSearch1.Fill(dtSearch1);
                        if (dtSearch1.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtSearch1.Rows.Count; k++)
                            {
                                GrdTicketIsue.Rows.Add();
                                GrdTicketIsue.Rows[k].Cells[0].Value = dtSearch1.Rows[k]["ItemName"].ToString().Trim();
                                GrdTicketIsue.Rows[k].Cells[1].Value = dtSearch1.Rows[k]["DescriptionIMEISN"].ToString().Trim();
                                GrdTicketIsue.Rows[k].Cells[2].Value = dtSearch1.Rows[k]["SNo"].ToString().Trim();
                                GrdTicketIsue.Rows[k].Cells[3].Value = dtSearch1.Rows[k]["EstimatePrice"].ToString().Trim();
                                GrdTicketIsue.Rows[k].Cells[4].Value = dtSearch1.Rows[k]["Status"].ToString().Trim();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text != "")
                {
                    DataTable dtCombo = new DataTable();
                    int strlen = Convert.ToInt16(txtSearch.Text.Length);
                    if (strlen >= 10)
                    {
                        SqlCommand cmdSearch = new SqlCommand("select TicketNo FROM T_TicketIssueTable WHERE ContactNo ='" + txtSearch.Text + "' ", con);
                        dtCombo.Rows.Clear();
                        SqlDataAdapter adpSearch = new SqlDataAdapter(cmdSearch);
                        adpSearch.Fill(dtCombo);
                        if (dtCombo.Rows.Count > 0)
                        {
                            if (dtCombo.Rows.Count > 1)
                            {
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Open();
                                }

                                lblTktNo.Visible = true;
                                cmbTicketNo.Visible = true;
                                cmbTicketNo.Items.Clear();
                                for (int i = 0; i < dtCombo.Rows.Count; i++)
                                {
                                    cmbTicketNo.Items.Add(dtCombo.Rows[i]["TicketNo"].ToString());
                                    string str = cmbTicketNo.DisplayMember = "TicketNo";

                                }
                                con.Close();
                            }
                            else
                            {
                                SearchLoad();
                                lblTktNo.Visible = false;
                                cmbTicketNo.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        SearchLoad();
                        lblTktNo.Visible = false;
                        cmbTicketNo.Visible = false;
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter the Ticket No", "Warning");
                }
            }
        }

        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;
        string mainStr;
        byte[] byteOut;
        double findCenterPosition;
        DataTable dtPrint = new DataTable();
        public DateTime currentDate;
        decimal vTotAmt;
        bool isChkPrinter = false;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtTNo.Text != "")
            {
                FunPrint();
                mainStr = "";
            }
            else
            {
                MyMessageBox.ShowBox("Please Enter Ticket Number ", "Warning");
                txtSearch.Focus();
            }
        }

        string vDes;
        //kumar add
        public static string SpliceText(string text, int lineLength)
        {
            return Regex.Replace(text, "(.{" + lineLength + "})", "$1" +Environment.NewLine);
            
        }
        public void FunPrint()
        {
            DateTime tTicketDate = new DateTime();
            DateTime tTicketTime = new DateTime();
            string tTicketNo = "", tName = "", tAddress1 = "", tAddress2 = "", tAddress3 = "", tContactNo = "", tNote1 = "", tNote2 = "",tNote3="";
            string tBillType = "";
            string vDescription="";


            DataTable dtAcProcess = new DataTable();
            
            dtTicket.Rows.Clear();
            SqlCommand cmdBillNo = new SqlCommand("select TKT.TicketNo,CONVERT(date,AssignDate,108) as TicketDate,CONVERT(time,Assigntime,103)as tTicketTime,Name,AddressLine1,AddressLine2,AddressLine3,Email,ContactNo,AssignDate,TNO,JobNo,BillNo, " +
                                                  " Note1,Note2,Note3,Amount,Deposit,Balance,ItemName,DescriptionIMEISN,SNo,EstimatePrice,Status " +
                                                  " FROM T_TicketIssuetable TKT, T_TicketIssueDetailsTable TKTD WHERE TKT.TicketNo=TKTD.TicketNo and TKT.TicketNo=@tTicketNo ", con);
            cmdBillNo.Parameters.AddWithValue("@tTicketNo", (txtTNo.Text));
            SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
            adpBillNo.Fill(dtTicket);

            string result = "";
            if (dtTicket.Rows.Count > 0)
            {
                tTicketDate = DateTime.Parse(dtTicket.Rows[0]["TicketDate"].ToString());
                tTicketTime = DateTime.Parse(dtTicket.Rows[0]["tTicketTime"].ToString());
                tTicketNo = dtTicket.Rows[0]["TicketNo"].ToString();
                
                tName = dtTicket.Rows[0]["Name"].ToString();
                tAddress1 = dtTicket.Rows[0]["AddressLine1"].ToString();
                //tAddress1 = dtTicket.Rows[0]["AddressLine1"].ToString();
                //tAddress2 = dtTicket.Rows[0]["AddressLine2"].ToString();
                //tAddress3 = dtTicket.Rows[0]["AddressLine3"].ToString();
                tContactNo = dtTicket.Rows[0]["ContactNo"].ToString();
                //tNote = (dtTicket.Rows[0]["Note1"].ToString() + " " + dtTicket.Rows[0]["Note2"].ToString() + " " + dtTicket.Rows[0]["Note3"].ToString());
                tNote1 = dtTicket.Rows[0]["Note1"].ToString();
                tNote2 = dtTicket.Rows[0]["Note2"].ToString();
                tNote3 = dtTicket.Rows[0]["Note3"].ToString();
                vDescription = dtTicket.Rows[0]["DescriptionIMEISN"].ToString();
                result=SpliceText(vDescription,  40);
                // mainStr += result;
            }
           
            //else
            //{
                charPerLine = _Class.clsVariables.tempGCharactersPerLine;

                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowLogo;

                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }

                string tHeaderAlign = "Yes";

                tHeaderAlign = _Class.clsVariables.tempGReceiptHeaderLeftAlign; //1st Line 
                if (tHeaderAlign == "Yes")
                {
                    ////top design start
                    charPerLine = _Class.clsVariables.tempGCharactersPerLine;

                    if (_Class.clsVariables.tempGPrintTopLine1 == "Yes") //Company Name
                    {
                        topLine1 = _Class.clsVariables.tempGTopLine1;
                        mainStr += topLine1;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine1.Length)), ' ');

                        mainStr += "\n";
                    }
                    // Top Line2
                    // topLine1="";
                    if (_Class.clsVariables.tempGPrintTopLine2 == "Yes") // Address of the Company
                    {
                        topLine2 = _Class.clsVariables.tempGTopLine2;
                        mainStr += topLine2;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine2.Length)), ' ');

                        mainStr += "\n";
                    }
                    // Top Line3
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine3 == "Yes") // Address of the Company
                    {
                        topLine3 = _Class.clsVariables.tempGTopLine3;
                        mainStr += topLine3;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine3.Length)), ' ');

                        mainStr += "\n";
                    }

                    // Top Line4
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine4 == "Yes") // Address of the Company
                    {
                        topLine4 = _Class.clsVariables.tempGTopLine4;
                        mainStr += topLine4;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine4.Length)), ' ');

                        mainStr += "\n";
                    }
                    // Top Line5
                    // topLine1 = "";
                    if (_Class.clsVariables.tempGPrintTopLine5 == "Yes") // E Mail ID of the Company
                    {
                        topLine5 = _Class.clsVariables.tempGTopLine5;
                        mainStr += topLine5;
                        mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - topLine5.Length)), ' ');

                        mainStr += "\n";
                    }
                //}
                
                else
                {
                }
                //header design start
                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }

                string vTktNo= "Ticket No :" + tTicketNo;
                mainStr += vTktNo;
               
                string strvTktNo = vTktNo.ToString();
                int strvTktNolen = Convert.ToInt16(strvTktNo.Length);
                int GetvTktNolen = (24 - strvTktNolen);
                double tTimeCount = GetvTktNolen;
                for (int j = 0; j < tTimeCount; j++)
                {
                    mainStr += " ";
                }
                mainStr += "Date :" + tTicketDate.ToString("dd/MM/yyyy"); // Date Print
                mainStr += "\n";

                string vSerBy = "Service By:" + txtServiceBy.Text.Trim();
                mainStr += vSerBy;

                int strtvSerBy = Convert.ToInt16(vSerBy.Length);
                int GetstrtvSerBylen = (24 - strtvSerBy);
                tTimeCount = GetstrtvSerBylen;
                for (int j = 0; j < tTimeCount; j++)
                {
                    mainStr += " ";
                }
                mainStr += "Time :" + tTicketTime.ToShortTimeString();
                mainStr += "\n";

                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                if (tName != "")
                {
                    mainStr += tName.ToString();
                    mainStr += "\n";
                }

                //if (tAddress1 != "")
                //{
                //    mainStr += tAddress1.ToString();
                //    mainStr += "\n";
                //}
                //if (tAddress2 != "")
                //{
                //    mainStr += tAddress2.ToString();
                //    mainStr += "\n";
                //}
                //if (tAddress3 != "")
                //{
                //    mainStr += tAddress3.ToString();
                //    mainStr += "\n";
                //}

                string sentence = txtAddress.Text;
                string[] words = sentence.Split(' ');
                var parts = new Dictionary<int, string>();
                string part = string.Empty;
                int partCounter = 0;
                foreach (var word in words)
                {
                    if (part.Length + word.Length <= 40)
                    {
                        part += string.IsNullOrEmpty(part) ? word : " " + word;
                    }
                    else
                    {
                        parts.Add(partCounter, part);
                        part = word;
                        partCounter++;
                    }
                }
                parts.Add(partCounter, part);
                StringBuilder builder = new StringBuilder();
                foreach (var item in parts)
                {
                    builder.Append(item.Value);
                    builder.Append(Environment.NewLine);
                }
                //txtAddress.Text = string.Empty;
                // txtAddress.Text = txtAddress.Text.Insert(1, builder.ToString());
                tAddress1 = builder.ToString();

                if (tAddress1 != "")
                {
                    mainStr += tAddress1.ToString();
                    mainStr += "\n";
                }

                if (tContactNo != "")
                {
                    mainStr += "Contact No. " + tContactNo.ToString();
                    mainStr += "\n";
                }
                string temp = "";
                
                //Counter Name
                if (_Class.clsVariables.tempGPrintCounterName == "Yes")
                {
                    temp = _Class.clsVariables.tCounterName;
                    mainStr += temp;
                    mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp.Length)), ' ');
                    mainStr += "\n";
                }

                //UserName
                if (_Class.clsVariables.tempGPrintUserName == "Yes")
                {
                    temp = _Class.clsVariables.tUserName;
                    mainStr += temp;
                    mainStr += "".PadLeft(Convert.ToInt16((double.Parse(charPerLine) - temp.Length)), ' ');

                    mainStr += "\n";
                }

                //Print Line Below Header
                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }

                DataTable dtPrinterItemName = new DataTable();
                string tempStr = null;
                if (_Class.clsVariables.tempGPrintQunatityandRate == "Yes")
                {
                    string tQtyHeading = "";

                    //mainStr += "Sl.No  ";
                    //mainStr += tQtyHeading;
                    ////double chkCount = (double.Parse(charPerLine) - ("Sl.No".Length + 2));
                    ////mainStr += "".PadRight(Convert.ToInt16(chkCount), ' ');

                    mainStr += "Description";
                    double chkCount = (double.Parse(charPerLine) - ("Description".Length + 17));
                    mainStr += "".PadRight(Convert.ToInt16(chkCount), ' ');


                    tQtyHeading += "Estimated Amount";
                    
                    mainStr += tQtyHeading;
                    mainStr += "\n";

                    lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                    if (lineBelowLogo == "No Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                        mainStr += "\n";
                    }
                    if (lineBelowLogo == "Single Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                        mainStr += "\n";
                    }
                    else if (lineBelowLogo == "Double Line")
                    {
                        mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                        mainStr += "\n";
                    }

                    //    }
                    //}
                    int Slno = 0;
                    int Getlen = 0;
                    string VEstAmt = "";
                    for (int mn = 0; mn < dtTicket.Rows.Count; mn++)
                    //foreach (DataRow row in dgsales.Rows)
                    {
                        if (dtTicket.Rows.Count > 0)
                        {
                            //Printing ItemName and SNo ///
                            Slno = Slno + 1;
                            tempStr = dtTicket.Rows[mn]["ItemName"].ToString() + "  : " + dtTicket.Rows[mn]["SNo"].ToString();
                            if (Slno < 2)
                            {                               
                                mainStr += tempStr.PadRight((int)findCenterPosition, ' ');
                                mainStr += "\n";
                            }
                            else
                            {
                                mainStr += tempStr;
                                mainStr += "\n";
                            }
                            tempStr = "";

                            //Printing Description ///
                            string strlen = dtTicket.Rows[mn]["DescriptionIMEISN"].ToString();
                            //result = SpliceText(strlen, 40);
                            string vGridDescription = strlen;
                            string[] words1 = vGridDescription.Split(' ');
                            var parts1 = new Dictionary<int, string>();
                            string part1 = string.Empty;
                            int partCounter1 = 0;
                            foreach (var word in words1)
                            {
                                if (part1.Length + word.Length <= 40)
                                {
                                    part1 += string.IsNullOrEmpty(part1) ? word : " " + word;
                                }
                                else
                                {
                                    parts1.Add(partCounter1, part1);
                                    part1 = word;
                                    partCounter1++;
                                }
                            }
                            parts1.Add(partCounter1, part1);
                            StringBuilder GridDescription = new StringBuilder();
                            foreach (var item in parts1)
                            {
                                GridDescription.Append(item.Value);
                                GridDescription.Append(Environment.NewLine);
                            }
                            result = GridDescription.ToString();

                            mainStr += result;

                            //Printing Estimated Price ///
                            VEstAmt = dtTicket.Rows[mn]["EstimatePrice"].ToString();
                            tTimeCount = 32;
                            mainStr += "\n";
                            for (int j = 0; j < tTimeCount; j++)
                            {
                                mainStr += " ";
                            }
                            mainStr += VEstAmt;
                            mainStr += "\n";
                        }
                    }
                }
                else
                {
                }
                lineBelowLogo = _Class.clsVariables.tempGPrintlineAboveTotal;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
               
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }
                    ////////////////// Total , Deposit, Balance Printing ////////////////////////
                if (_Class.clsVariables.tempGPayThisAmount != "")
                {

                    if (_Class.clsVariables.tempGPrintPayThisAmountRightAlign == "Yes")
                    {
                       
                    }
                    else
                    {
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                           // if (findCenterPosition % 2 == 0)
                            {
                                if (txtAmount.Text != "")
                                {
                                    vTotAmt = Convert.ToDecimal(txtAmount.Text.Trim());
                                }
                                else
                                {
                                    vTotAmt = 0;
                                }
                                string strTotal = string.Format("{0:0.00}", vTotAmt);
                                tTimeCount = 5;
                                mainStr += "                 Total :";                               
                                for (int j = 0; j < tTimeCount; j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += " $ " + strTotal + "".PadRight(Convert.ToInt16(1), ' ');
                                mainStr += "\n";

                                if (txtDeposit.Text != "")
                                {
                                    vTotAmt = Convert.ToDecimal(txtDeposit.Text.Trim());
                                }
                                else
                                {
                                    vTotAmt = 0;
                                }
                                string strDeposit = string.Format("{0:0.00}", vTotAmt);
                                tTimeCount = 3;
                                mainStr += "                 Deposit :";
                                for (int j = 0; j < tTimeCount; j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += " $ " + strDeposit + "".PadRight(Convert.ToInt16(1), ' ');
                                mainStr += "\n";

                                if (txtBalance.Text != "")
                                {
                                    vTotAmt = Convert.ToDecimal(txtBalance.Text.Trim());
                                }
                                else
                                {
                                    vTotAmt = 0;
                                }
                                string strBalance = string.Format("{0:0.00}", vTotAmt);
                                tTimeCount = 3;
                                mainStr += "                 Balance :";
                                for (int j = 0; j < tTimeCount; j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += " $ " + strBalance + "".PadRight(Convert.ToInt16(1), ' ');
                                mainStr += "\n";
                            }
                        }
                    }
                     mainStr += "\n";
                }
                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowTotal;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                   // mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }


                if (_Class.clsVariables.tempGPrintBillType == "Yes")
                {
                    string temp1 = "Payment Mode:" + tBillType;
                    mainStr += temp1;

                    mainStr += "".PadLeft(Convert.ToInt16(Convert.ToDouble(charPerLine) - temp1.Length), ' ');
                    mainStr += "\n";
                    //  break;
                }
                //bottom line
                if (_Class.clsVariables.tempGPrintBottomLine1 == "Yes")
                {

                    topLine1 = _Class.clsVariables.tempGBottomLine1;
                    if (topLine1.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine2 == "Yes")
                {

                    topLine2 = _Class.clsVariables.tempGBottomLine2;
                    if (topLine2.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine2;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }

                mainStr += "\n";
                ////////////////////////////// Notes Printing //////////////////////////////////////////////////////
                //if (tNote1 != "" && tNote2 != "" && tNote3 != "")
                //{
                //    mainStr += "Note : "+"\n" + tNote1;
                //    mainStr += "\n";
                //    mainStr += tNote2;
                //    mainStr += "\n";
                //    mainStr += tNote3;
                //    mainStr += "\n";
                //}
                //else if (tNote1 != "" && tNote2 != "")
                //{
                //    mainStr += "Note : " + "\n" + tNote1;
                //    mainStr += "\n";
                //    mainStr += tNote2;
                //    mainStr += "\n";
                //}
                //else if (tNote2 != "" && tNote3 != "")
                //{
                //    mainStr += "Note : " + "\n" + tNote2;
                //    mainStr += "\n";
                //    mainStr += tNote3;
                //    mainStr += "\n";
                //}
                //else if (tNote1 != "" && tNote3 != "")
                //{
                //    mainStr += "Note : " + "\n" + tNote1;
                //    mainStr += "\n";
                //    mainStr += tNote3;
                //    mainStr += "\n";
                //}
                //else if (tNote1 != "")
                //{
                //    mainStr += "Note : " + "\n" + tNote1;
                //    mainStr += "\n";
                //}
                //else if(tNote2!="")
                //{
                //    mainStr += "Note : " + "\n" + tNote2;
                //    mainStr += "\n";
                //}
                //else if (tNote3 != "")
                //{
                //    mainStr += "Note : " + "\n" + tNote3;
                //    mainStr += "\n";
                //}
                //else
                //{
                //    mainStr += "Note :";
                //    mainStr += "\n";
                //}

                if (tNote1 != "")
                {
                    string sentence1 = txtNotes.Text;
                    string[] words1 = sentence1.Split(' ');
                    var parts1 = new Dictionary<int, string>();
                    string part1 = string.Empty;
                    int partCounter1 = 0;
                    foreach (var word in words1)
                    {
                        if (part1.Length + word.Length <= 40)
                        {
                            part1 += string.IsNullOrEmpty(part1) ? word : " " + word;
                        }
                        else
                        {
                            parts1.Add(partCounter1, part1);
                            part1 = word;
                            partCounter1++;
                        }
                    }
                    parts1.Add(partCounter1, part1);
                    StringBuilder NotesPrint = new StringBuilder();
                    foreach (var item in parts1)
                    {
                        NotesPrint.Append(item.Value);
                        NotesPrint.Append(Environment.NewLine);
                    }
                    //txtAddress.Text = string.Empty;
                    // txtAddress.Text = txtAddress.Text.Insert(1, builder.ToString());
                    tNote1 = NotesPrint.ToString();

                    mainStr += "Note : " + "\n" + tNote1;
                    mainStr += "\n";
                }
               
                    //////////////////////////===========================================================================///////////////////////
               
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }

                if (_Class.clsVariables.tempGPrintBottomLine3 == "Yes")
                {

                    topLine3 = _Class.clsVariables.tempGBottomLine3;
                    if (topLine3.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine3;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine3;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine4 == "Yes")
                {

                    topLine4 = _Class.clsVariables.tempGBottomLine4;
                    if (topLine4.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine4;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine4;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }

                if (_Class.clsVariables.tempGPrintBottomLine5 == "Yes")
                {
                    topLine5 = _Class.clsVariables.tempGBottomLine5;
                    if (topLine5.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine5;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine5;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }
                //Print Line Below Header

                lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowBottomText;
                if (lineBelowLogo == "No Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), ' ');
                    mainStr += "\n";
                }
                if (lineBelowLogo == "Single Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '-');
                    mainStr += "\n";
                }
                else if (lineBelowLogo == "Double Line")
                {
                    mainStr += "".PadLeft(Convert.ToInt16(charPerLine), '=');
                    mainStr += "\n";
                }
                //Print Bottom Time

                if (_Class.clsVariables.tempGPrintBottomTime == "Yes")
                {
                    topLine1 = currentDate.ToString();
                    if (topLine1.Length <= double.Parse(charPerLine))
                    {
                        findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += topLine1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }
                if (_Class.clsVariables.QueueNo == "Yes")
                {
                    string temp1 = "";
                    if (tTicketNo.Length.Equals(2))
                    {
                        temp1 = "Queue No:" + "0" + tTicketNo.Substring(tTicketNo.Length - 3, 3);
                    }
                    else
                    {
                        temp1 = "Queue No:" + tTicketNo.Substring(tTicketNo.Length - 3, 3);
                    }

                    if (temp1.Length <= double.Parse(charPerLine) && temp1.Length > 0)
                    {
                        findCenterPosition = (double.Parse(charPerLine) - temp1.Length);
                        if (findCenterPosition % 2 == 0)
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                            mainStr += temp1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        else
                        {
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                            mainStr += temp1;
                            mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        }
                        mainStr += "\n";
                    }
                }

                string tPrinterType = "";

                if (_Class.clsVariables.tempGEnableThisDevice == "Yes")
                {
                    tPrinterType = "Receipt";
                }

                int tNoPrint = 0;
                
                if (isChkPrinter == false)
                {
                    topLine5 = _Class.clsVariables.tempGPrintCopies;
                    if (topLine5 == "1 Copy")
                    {
                        tNoPrint = 1;
                    }
                    else if (topLine5 == "2 Copy")
                    {
                        tNoPrint = 2;
                    }
                    else if (topLine5 == "3 Copy")
                    {
                        tNoPrint = 3;
                    }
                    else if (topLine5 == "No Copies")
                    {
                        tNoPrint = 0;
                    }

                    for (int i2 = 0; i2 < tNoPrint; i2++)
                    {
                        // RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, mainStr);
                        Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, mainStr));
                        workerThread.Start();
                        bool finished = workerThread.Join(3000);
                        if (!finished)
                        {
                            workerThread.Abort();
                            // CancelPrintJob();
                        }
                        if (_Class.clsVariables.tempGCutPaper == "Yes")
                        {
                            DataTable dtNew = new DataTable();
                            dtNew.Rows.Clear();
                            SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                            cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                            SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                            adp.Fill(dtNew);
                            if (dtNew.Rows.Count > 0)
                            {
                                string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

                                byteOut = new byte[byteStrings.Length];

                                for (int i = 0; i < byteStrings.Length; i++)
                                {
                                    byteOut[i] = Convert.ToByte(byteStrings[i]);
                                }
                                //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                //    }

                                string s1 = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

                                Thread workerThread1 = new Thread(() => RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, s1));
                                workerThread1.Start();
                                finished = workerThread1.Join(3000);
                                if (!finished)
                                {
                                    workerThread1.Abort();
                                    mainStr = "";
                                    //CancelPrintJob();
                                }
                            }
                        }
                    }
                }
            }

        }

        private void GrdTicketIsue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Estimate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (GrdTicketIsue.CurrentCell.ColumnIndex == 3)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) //Only Numbers
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void GrdTicketIsue_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // e.Control.KeyPress += new KeyPressEventHandler(Estimate_KeyPress);
            
                 //if (GrdTicketIsue.Columns[3].Name == "EstimatePrice")
                 //{
                     if (GrdTicketIsue.CurrentCell.ColumnIndex == 3)
                     {
                         TextBox txt = e.Control as TextBox;
                         if (txt != null)
                         {
                             txt.KeyPress += new KeyPressEventHandler(Estimate_KeyPress);
                         }
                     }
                 //}
            
        }

        private void GrdTicketIsue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int col = GrdTicketIsue.CurrentCell.ColumnIndex;
                int row = GrdTicketIsue.CurrentCell.RowIndex;

                if (col < GrdTicketIsue.ColumnCount - 1)
                {
                    col++;
                }
                else
                {
                    col = 0;
                    row++;
                }

                if (row == GrdTicketIsue.RowCount)
                    GrdTicketIsue.Rows.Add();

                GrdTicketIsue.CurrentCell = GrdTicketIsue[col, row];
                e.Handled = true;

                if (col==4)
                {
                     string vIName = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[row].Cells[col].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[row].Cells[col].Value);

                    if (vIName == "")
                    //if (GrdTicketIsue.Rows[e.RowIndex].Cells[0].Value == null)
                    {
                        //txtNoteLine1.Focus();
                        txtNotes.Focus();
                    }
                }

            }
        }
        string strAdd1Len = "", strAdd2Len = "", strAdd3Len="";
        string LastLetter = "";
       private void txtAddress1_KeyPress(object sender, KeyPressEventArgs e)
       {          

         //if (txtAddress1.Text != "")
         //   {
         //       int strlen1 = Convert.ToInt16(txtAddress1.Text.Length);
         //       LastLetter = e.KeyChar.ToString();
                
         //       if (strlen1 == 40)
         //       {
         //          FullyTyped = true;
         //          strAdd1Len = txtAddress1.Text;
         //          int index1 = strAdd1Len.LastIndexOf(" ");
         //          if (index1 != -1)
         //          {
         //              string[] vSpace = strAdd1Len.Split(' ');
         //              string lastword = vSpace[vSpace.Length - 1];
         //              if (lastword != "")
         //              {
         //                  strAdd1Len = strAdd1Len.Remove(index1, 40);
         //                  //strAdd1Len = strAdd1Len.Replace(lastword, "");
         //              }
         //              txtAddress1.Text = strAdd1Len;
         //              txtAddress2.Focus();
         //              txtAddress2.Text = txtAddress2.Text.Insert(txtAddress2.SelectionStart, lastword + LastLetter);
         //              txtAddress2.SelectionStart = txtAddress2.Text.Length + 1;
         //              LastLetter = "";
         //              txtAddress1.Text = strAdd1Len;
         //          }
         //          else
         //          {
         //              txtAddress2.Focus();

         //          }
         //       }
         //       //txtAddress1.Text = strAdd1Len;
         //   }
        }

        private void txtAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {

            //strAdd2Len = txtAddress2.Text;
            if (txtAddress2.Text != "")
            {                
                int strlen = Convert.ToInt16(txtAddress2.Text.Length);
                LastLetter = e.KeyChar.ToString();
                if (strlen == 40)
                {
                    FullyTyped = true;
                    strAdd2Len = txtAddress2.Text;
                    int index1 = strAdd2Len.LastIndexOf(" ");
                    if (index1 != -1)
                    {
                        string[] vSpace = strAdd2Len.Split(' ');
                        string lastword = vSpace[vSpace.Length - 1];
                        if (lastword != "")
                        {
                            strAdd2Len = strAdd2Len.Replace(lastword, "");
                        }
                        txtAddress2.Text = strAdd2Len;
                        txtAddress3.Focus();
                        txtAddress3.Text = txtAddress3.Text.Insert(txtAddress3.SelectionStart, lastword + LastLetter);
                        txtAddress3.SelectionStart = txtAddress3.Text.Length + 1; 
                        //txtAddress2.Text = strAdd2Len;
                        LastLetter = "";
                    }
                    else
                    {
                        txtAddress3.Focus();
                    }
                }
                //txtAddress2.Text = strAdd2Len;
            }
        }

        private void txtAddress3_KeyPress(object sender, KeyPressEventArgs e)
        {

           // strAdd3Len = txtAddress3.Text;
            if (txtAddress3.Text != "")
            {
               
                int strlen = Convert.ToInt16(txtAddress3.Text.Length);
                LastLetter = e.KeyChar.ToString();
                if (strlen == 40)
                {
                    FullyTyped = true;
                    strAdd3Len = txtAddress3.Text;
                    int index1 = strAdd3Len.LastIndexOf(" ");
                    if (index1 != -1)
                    {
                        string[] vSpace = strAdd3Len.Split(' ');
                        string lastword = vSpace[vSpace.Length - 1];
                        if (lastword != "")
                        {
                            strAdd3Len = strAdd3Len.Replace(lastword, "");
                        }
                        //strAdd3Len = strAdd3Len.Replace(lastword, "");
                        txtAddress3.Text = strAdd3Len;
                        txtEmailID.Focus();
                        //txtAddress3.Text = txtAddress3.Text.Insert(txtAddress3.SelectionStart, lastword);
                        //txtAddress3.SelectionStart = txtAddress3.Text.Length + 1; // add some logic if length is 0
                        // txtAddress3.Text = strAdd3Len;
                    }
                    else
                    {
                        txtEmailID.Focus();
                    }
                }
               // txtAddress3.Text = strAdd3Len;
            }
        }
        string strNote1Len = "", strNote2Len = "", strNote3Len="";
        private void txtNoteLine1_KeyPress(object sender, KeyPressEventArgs e)
        {
            strNote1Len = txtNoteLine1.Text;
            if (txtNoteLine1.Text != "")
            {

                int strlen1 = Convert.ToInt16(strNote1Len.Length);
                LastLetter = e.KeyChar.ToString();
                if (strlen1 == 40)
                   {
                       FullyTyped = true;
                    strNote1Len = txtNoteLine1.Text;
                    int strlen = Convert.ToInt16(strNote1Len.Length);

                    if (strlen == 40)
                    {
                        int index1 = strNote1Len.LastIndexOf(" ");
                        if (index1 != -1)
                        {
                            string[] vSpace = strNote1Len.Split(' ');
                            string lastword = vSpace[vSpace.Length - 1];
                            if (lastword != "")
                            {
                                strNote1Len = strNote1Len.Replace(lastword, "");
                            }   
                            txtNoteLine1.Text = strNote1Len;
                            txtNoteLine2.Focus();
                            txtNoteLine2.Text = txtNoteLine2.Text.Insert(txtNoteLine2.SelectionStart, lastword + LastLetter);
                            txtNoteLine2.SelectionStart = txtNoteLine2.Text.Length + 1;
                            txtNoteLine1.Text = strNote1Len;
                        }
                    }
                    txtNoteLine1.Text = strNote1Len;
                }
            }
        }


        private void txtNoteLine2_KeyPress(object sender, KeyPressEventArgs e)
        {
             strNote2Len = txtNoteLine2.Text;
            if (txtNoteLine2.Text != "")
            {

                int strlen1 = Convert.ToInt16(strNote2Len.Length);
                LastLetter = e.KeyChar.ToString();
                if (strlen1 == 40)
                {
                    FullyTyped = true;
                    int index1 = strNote2Len.LastIndexOf(" ");
                    if (index1 != -1)
                    {
                        string[] vSpace = strNote2Len.Split(' ');
                        string lastword = vSpace[vSpace.Length - 1];
                        if (lastword != "")
                        {
                            strNote2Len = strNote2Len.Replace(lastword, "");
                        }
                       
                        txtNoteLine2.Text = strNote2Len;
                        txtNoteLine3.Focus();
                        txtNoteLine3.Text = txtNoteLine3.Text.Insert(txtNoteLine3.SelectionStart, lastword + LastLetter);
                        txtNoteLine3.SelectionStart = txtNoteLine2.Text.Length + 1;
                        txtNoteLine2.Text = strNote2Len;
                    }
                }
            }
        }

        private void txtNoteLine3_KeyPress(object sender, KeyPressEventArgs e)
        {
             strNote3Len = txtNoteLine3.Text;
            if (txtNoteLine3.Text != "")
            {
                int strlen1 = Convert.ToInt16(strNote3Len.Length);
                LastLetter = e.KeyChar.ToString();
                if (strlen1 == 40)
                {
                    FullyTyped = true;
                    int index1 = strNote3Len.LastIndexOf(" ");
                    if (index1 != -1)
                    {
                        string[] vSpace = strNote3Len.Split(' ');
                        string lastword = vSpace[vSpace.Length - 1];
                        if (lastword != "")
                        {
                            strNote3Len = strNote3Len.Replace(lastword, "");
                        }
                        
                        txtNoteLine3.Text = strNote3Len;
                        btnSave.Focus();
                       // txtNoteLine2.Text = txtNoteLine2.Text.Insert(txtNoteLine2.SelectionStart, lastword);
                       // txtNoteLine2.SelectionStart = txtNoteLine2.Text.Length + 1; // add some logic if length is 0
                        //txtNoteLine3.Text = strNote3Len;
                    }
                }
            }
        }

        private void GrdTicketIsue_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //tEstimatedPrice = 0.00; tTotEstimatedPrice = 0.00;
            //if (GrdTicketIsue.CurrentRow != null && e.ColumnIndex == 3)
            //{
                
            //    if (GrdTicketIsue.Rows[GrdTicketIsue.CurrentCell.RowIndex].Cells[3].Value != null && GrdTicketIsue.Rows[GrdTicketIsue.CurrentCell.RowIndex].Cells[3].Value.ToString() != "")
            //    {
            //        for (int i = 0; i < GrdTicketIsue.Rows.Count-1; i++)
            //        {
            //            tEstimatedPrice = (GrdTicketIsue.Rows[i].Cells[3].Value == null ? 0 : (Convert.ToDouble(GrdTicketIsue.Rows[i].Cells[3].Value)));
            //            tTotEstimatedPrice = (tTotEstimatedPrice + tEstimatedPrice);
            //            txtAmount.Text = Convert.ToString(tTotEstimatedPrice);

            //            if (txtAmount.Text != "" && txtDeposit.Text != "")
            //            {
            //                txtBalance.Text = (Convert.ToDecimal(txtAmount.Text) - Convert.ToDecimal(txtDeposit.Text)).ToString();
            //            }
            //        }
            //    }
            //}
        }

        private void GrdTicketIsue_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //   // string vIName = (string.IsNullOrEmpty(Convert.ToString(GrdTicketIsue.Rows[e.RowIndex].Cells[0].Value)) == true) ? "" : Convert.ToString(GrdTicketIsue.Rows[e.RowIndex].Cells[0].Value);

            //    //if (vIName == "")
            //    if(GrdTicketIsue.Rows[e.RowIndex].Cells[0].Value==null)
            //    {
            //        txtNoteLine1.Focus();
            //    }
            //}
        }

        private void GrdTicketIsue_KeyPress(object sender, KeyPressEventArgs e)
        {

        }     

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            strchk = "1";
        }
        private void txtAddress3_Leave(object sender, EventArgs e)
        {
            strchk = "1";
        }
        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress1.Text != "" && strAdd1Len != "" && strchk == "1" && FullyTyped == true)
            {
                if (txtAddress1.Text != strAdd1Len)
                {
                    FullyTyped = false;
                    strchk = "";
                    txtAddress1.Text = txtAddress1.Text.Remove(0, 1);
                }
            }
        }
        string strchk;
        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            strchk = "1";
        }
        private void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress2.Text != "" && strAdd2Len != "" && strchk == "1" && FullyTyped == true)
            {
                if (txtAddress2.Text != strAdd2Len)
                {
                    FullyTyped = false;
                    strchk = "";
                    txtAddress2.Text = txtAddress2.Text.Remove(0, 1);
                }
            }
        }
        private void txtAddress3_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress3.Text != "" && strAdd3Len != "" && strchk == "1" && FullyTyped == true)
            {
                if (txtAddress3.Text != strAdd3Len)
                {
                    FullyTyped = false;
                    strchk = "";
                    txtAddress3.Text = txtAddress3.Text.Remove(0, 1);
                }
            }
        }

        private void txtNoteLine1_Leave(object sender, EventArgs e)
        {
            strchk = "1";
        }

        private void txtNoteLine1_TextChanged(object sender, EventArgs e)
        {
            if (txtNoteLine1.Text != "" && strNote1Len != "" && strchk == "1" && FullyTyped == true)
            {
                if (txtNoteLine1.Text != strNote1Len)
                {
                    FullyTyped = false;
                    strchk = "";
                    strNote1Len = "";
                    txtNoteLine1.Text = txtNoteLine1.Text.Remove(0, 1);
                }
            }
        }

        private void txtNoteLine2_TextChanged(object sender, EventArgs e)
        {
            if (txtNoteLine2.Text != "" && strNote2Len != "" && strchk == "1" && FullyTyped == true)
            {
                if (txtNoteLine2.Text != strNote2Len)
                {
                    FullyTyped = false;
                    strchk = "";
                    strNote2Len = "";
                    txtNoteLine2.Text = txtNoteLine2.Text.Remove(0, 1);
                }
            }
        }

        private void txtNoteLine3_TextChanged(object sender, EventArgs e)
        {
            if (txtNoteLine3.Text != "" && strNote3Len != "" && strchk == "1" && FullyTyped == true)
            {
                if (txtNoteLine3.Text != strNote3Len)
                {
                    FullyTyped = false;
                    strchk = "";
                    strNote3Len = "";
                    txtNoteLine3.Text = txtNoteLine3.Text.Remove(0, 1);
                }
            }
        }

        private void txtNoteLine3_Leave(object sender, EventArgs e)
        {
            strchk = "1";
        }

        private void txtNoteLine2_Leave(object sender, EventArgs e)
        {
            strchk = "1";
        }

        private void cmbTicketNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTicketNo.Text != "")
            {
                SearchLoad();
            }
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtAddress.Lines.Length >= 3 && e.KeyChar == '\r')
            {
                e.Handled = true;
                txtEmailID.Focus();
            }

            //    string sentence = txtAddress.Text;
            //    string[] words = sentence.Split(' ');
            //    var parts = new Dictionary<int, string>();
            //    string part = string.Empty;
            //    int partCounter = 0;
            //    foreach (var word in words)
            //    {
            //        if (part.Length + word.Length <= 40)
            //        {
            //            part += string.IsNullOrEmpty(part) ? word : " " + word;
            //        }
            //        else
            //        {
            //            parts.Add(partCounter, part);
            //            part = word;
            //            partCounter++;
            //        }
            //    }
            //    parts.Add(partCounter, part);
            //    StringBuilder builder = new StringBuilder();
            //    foreach (var item in parts)
            //    {
            //        builder.Append(item.Value);
            //        builder.Append(Environment.NewLine);
            //    }
            //    txtAddress.Text = string.Empty;
            //    // txtAddress.Text = txtAddress.Text.Insert(1, builder.ToString());
            //    txtAddress.Text = builder.ToString();

            //    txtEmailID.Focus();
            //}

            //----------

            //string sentence = txtAddress.Text;
            //string[] words = sentence.Split(' ');
            //var parts = new Dictionary<int, string>();
            //string part = string.Empty;
            //int partCounter = 0;
            //foreach (var word in words)
            //{
            //    if (part.Length + word.Length < 41)
            //    {
            //        part += string.IsNullOrEmpty(part) ? word : " " + word;
            //    }
            //    else
            //    {
            //        parts.Add(partCounter, part);
            //        part = word;
            //        partCounter++;
            //    }
            //}
            //parts.Add(partCounter, part);
            //StringBuilder builder = new StringBuilder();
            //foreach (var item in parts)
            //{
            //    builder.Append(item.Value);
            //    builder.Append(Environment.NewLine);
            //}
            //txtAddress.Text = builder.ToString();
            //txtAddr = txtAddress.Text;
            //txtAddress.Text = txtAddr;
            //txtok = true;

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (txtok == true)
            {
                txtAddress.Text = txtAddr;
                txtok = false;
                txtAddr = "";
            }
            //string sentence = txtAddress.Text;
            //string[] words = sentence.Split(' ');
            //var parts = new Dictionary<int, string>();
            //string part = string.Empty;
            //int partCounter = 0;
            //foreach (var word in words)
            //{
            //    if (part.Length + word.Length < 41)
            //    {
            //        part += string.IsNullOrEmpty(part) ? word : " " + word;
            //    }
            //    else
            //    {
            //        parts.Add(partCounter, part);
            //        part = word;
            //        partCounter++;
            //    }
            //}
            //parts.Add(partCounter, part);
            //StringBuilder builder = new StringBuilder();
            //foreach (var item in parts)
            //{
            //    builder.Append(item.Value);
            //    builder.Append(Environment.NewLine);
            //}
            //txtAddress.Text = builder.ToString();
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtAddress.SelectionStart = txtAddress.Text.Length + 1;
                if (txtAddress.Text == "")
                {
                    txtName.Focus();
                    //strAdd1Len = string.Empty;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                //if (txtAddress.SelectionStart == 0)
                //{
                //    this.ActiveControl = txtName;
                //}
                if (this.txtAddress.Lines.Length == 1)
                {
                    e.Handled = true;
                    this.ActiveControl = txtName;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                //string txtadd = "";
                //txtadd = txtAddress.Text;
                //if (this.txtadd.lin length >= 4)
                //{
                //    txtName.Focus();
                //}
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("Enter");
            }
            //if (this.txtaddress.lines.length >= 4 && e.keych == '\r')
            //{
            //    txtemailid.focus();
            //    e.handled = true;

            //}
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string sentence = txtAddress.Text;
            //    string[] words = sentence.Split(' ');
            //    var parts = new Dictionary<int, string>();
            //    string part = string.Empty;
            //    int partCounter = 0;
            //    foreach (var word in words)
            //    {
            //        if (part.Length + word.Length < 41)
            //        {
            //            part += string.IsNullOrEmpty(part) ? word : " " + word;
            //        }
            //        else
            //        {
            //            parts.Add(partCounter, part);
            //            part = word;
            //            partCounter++;
            //        }
            //    }
            //    parts.Add(partCounter, part);
            //    StringBuilder builder = new StringBuilder();
            //    foreach (var item in parts)
            //    {
            //        builder.Append(item.Value);
            //        builder.Append(Environment.NewLine);
            //    }
            //    txtAddress.Text = "";
            //   // txtAddress.Text = txtAddress.Text.Insert(1, builder.ToString());
            //    txtAddress.Text = builder.ToString();
            //}
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            //string sentence = txtAddress.Text;
            //string[] words = sentence.Split(' ');
            //var parts = new Dictionary<int, string>();
            //string part = string.Empty;
            //int partCounter = 0;
            //foreach (var word in words)
            //{
            //    if (part.Length + word.Length <= 40)
            //    {
            //        part += string.IsNullOrEmpty(part) ? word : " " + word;
            //    }
            //    else
            //    {
            //        parts.Add(partCounter, part);
            //        part = word;
            //        partCounter++;
            //    }
            //}
            //parts.Add(partCounter, part);
            //StringBuilder builder = new StringBuilder();
            //foreach (var item in parts)
            //{
            //    builder.Append(item.Value);
            //    builder.Append(Environment.NewLine);
            //}
            //txtAddress.Text = string.Empty;
            //// txtAddress.Text = txtAddress.Text.Insert(1, builder.ToString());
            //txtAddress.Text = builder.ToString();
        }

        private void txtNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                GrdTicketIsue.Focus();
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (txtNotes.Text == "")
                {
                    GrdTicketIsue.Focus();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtDeposit.Focus();
            }
        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            if (txtok == true)
            {
                txtNotes.Text = txtAddr;
                txtok = false;
                txtAddr = "";
            }
        }

        private void txtNotes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.txtNotes.Lines.Length >= 3 && e.KeyChar == '\r')
            {
                e.Handled = true;
                txtDeposit.Focus();
            }
            //txtAddr = txtNotes.Text;
            //txtNotes.Text = txtAddr;
            //txtNotes.SelectionStart = txtNotes.Text.Length + 1;
            //txtok = true;
        }

        private void txtNotes_Leave(object sender, EventArgs e)
        {

        }
    }
}
