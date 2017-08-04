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
using System.Configuration;

namespace MSPOSBACKOFFICE
{
    public partial class SalesBOMIssueCreation : Form
    {
        public SalesBOMIssueCreation()
        {
            InitializeComponent();

            foreach (DataGridViewColumn col in DgBomsEntry.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            DgBomsEntry.Columns["Item_code"].ReadOnly = true;
            DgBomsEntry.Columns["Item_Name"].ReadOnly = true;
            DgBomsEntry.Columns["unit_name"].ReadOnly = true;

        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlCommand cmd = null;
        SqlDataAdapter adp = null;
        string issuenumber = "";
        private void txtLabourAmount_Enter(object sender, EventArgs e)
        {
            valueschages = "";
            pnlledgername.Visible = true;
            
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType","Ledsel_tableSelect");
            cmd.Parameters.AddWithValue("@itemName","");
            cmd.Parameters.AddWithValue("@itemCode","");
            adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lstledgerName.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count>0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                lstledgerName.Items.Add(dt.Rows[i]["Ledsel_name"].ToString());
                }
            }
            valueschages = "1";
        }
        DataTable dt_BomissueCreation = new DataTable();
        private void SalesBOMIssueCreation_Load(object sender, EventArgs e)
        {
            DgBomsEntry.DefaultCellStyle.ForeColor = Color.Black;
           //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            pnlledgername.Visible = false;
            lblBOmBillno.Text = "";
            billno();
            pnlledgername.Visible = false;
            DgBomsEntry.Columns["Typess"].ReadOnly = true;
           // DgBomsEntry.Columns["nt_qty"].DisplayIndex = Right;
            DgBomsEntry.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["tx_Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["nt_Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dt_BomissueCreation.Columns.Add("ItemCode");
            dt_BomissueCreation.Columns.Add("ItemName");
            dt_BomissueCreation.Columns.Add("Unit");
            dt_BomissueCreation.Columns.Add("Type");
            dt_BomissueCreation.Columns.Add("TaxQty");
            dt_BomissueCreation.Columns.Add("Qty");
            dt_BomissueCreation.Columns.Add("Rate");
            dt_BomissueCreation.Columns.Add("Amount");
            dt_BomissueCreation.Columns.Add("BOM_No");
            dt_BomissueCreation.Columns.Add("LabourAmount");
            dt_BomissueCreation.Columns.Add("BOM_name");

           
         //   issuenumber = passingvalues.BomIssueAlterLedger.ToString();
            issuenumber = (passingvalues.BomIssueAlterLedger == null) ? "" : passingvalues.BomIssueAlterLedger.ToString();
            if (issuenumber != "")
            {
                txtBomName.Text = issuenumber.ToString();
                selectvalues();
            }

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        bool isChk;
        string chk;
        private void txtBomName_TextChanged(object sender, EventArgs e)
        {
            isChk = false;
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType","SelectBomLike");
            cmd.Parameters.AddWithValue("@itemName",txtBomName.Text);
            cmd.Parameters.AddWithValue("@ItemCode", "");
            adp = new SqlDataAdapter(cmd);
            DataTable dtselect = new DataTable();
            dtselect.Rows.Clear();
            adp.Fill(dtselect);
            if (dtselect.Rows.Count > 0)
            {
                isChk = true;
                string tempstr = dtselect.Rows[0]["BOM_name"].ToString();
                for (int k = 0; k < lstledgerName.Items.Count; k++)
                {
                    if (tempstr == lstledgerName.Items[k].ToString())
                    {
                        lstledgerName.SetSelected(k, true);
                        txtBomName.Select();
                        chk = "1";
                        txtBomName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        break;
                    }
                }
            }
            if (isChk == false)
            {
                chk = "1";
                if (txtBomName.Text != "")
                {
                    string name = txtBomName.Text.Remove(txtBomName.Text.Length - 1);
                    txtBomName.Text = name.ToString();
                    txtBomName.Select(txtBomName.Text.Length, 0);
                }
                txtBomName.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
            }
            else
            {
                chk = "1";
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

        public void billno()
        {
            cmd = new SqlCommand("SP_SelectQuery_Return", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "MaxBomBillNo");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter VoucherNo = new SqlParameter("@VoucherNo", SqlDbType.VarChar, 50);
            VoucherNo.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(VoucherNo);
            cmd.ExecuteNonQuery();
            lblBOmBillno.Text = (string)cmd.Parameters["@VoucherNo"].Value;
        }
        string valueschages = "";
        private void txtBomName_Enter(object sender, EventArgs e)
        {
            valueschages = "";
            pnlledgername.Visible = true;
            cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "SelectBOmName");
            cmd.Parameters.AddWithValue("@itemName", "");
            cmd.Parameters.AddWithValue("@itemCode", "");
            adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            lstledgerName.Items.Clear();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstledgerName.Items.Add(dt.Rows[i]["BOM_name"].ToString());
                }
            }
            valueschages = "2";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lstledgerName_Click(object sender, EventArgs e)
        {
            listcontentclick();
        }
        public void listcontentclick()
        {
            if (valueschages == "1")
            {
                if (lstledgerName.SelectedItems.Count > 0)
                {
                    txtLabourAmount.Text = lstledgerName.SelectedItem.ToString();
                    // selectvalues();
                    txtBomName.Focus();
                }

            }
            else if (valueschages == "2")
            {
                if (lstledgerName.SelectedItems.Count > 0)
                {
                    txtBomName.Text = lstledgerName.SelectedItem.ToString();

                    selectvalues();
                }
            }
        }
        DataTable dt_ = new DataTable();
        public void selectvalues()
        {
            if (txtBomName.Text != "" && txtBomName.Text != null)
            {
                cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType","BOMIssueselect");
                cmd.Parameters.AddWithValue("@itemName", txtBomName.Text);
                cmd.Parameters.AddWithValue("@ItemCode","");
                adp = new SqlDataAdapter(cmd);
               
                dt_.Rows.Clear();
                adp.Fill(dt_);
                DgBomsEntry.Rows.Clear();
                if (dt_.Rows.Count > 0)
                {
                    txtissueQty.Text = "1";
                    for (int i = 0; i < dt_.Rows.Count; i++)
                    {
                        DgBomsEntry.Rows.Add();
                        DgBomsEntry.Rows[i].Cells["Item_code"].Value = ((dt_.Rows[i]["Item_code"].ToString() == "" || dt_.Rows[i]["Item_code"].ToString().Trim() == null) ? "" : dt_.Rows[i]["Item_code"].ToString());
                        DgBomsEntry.Rows[i].Cells["Item_name"].Value =((dt_.Rows[i]["Item_name"].ToString().Trim()==""||dt_.Rows[i]["Item_name"].ToString() .Trim()==null)?"":dt_.Rows[i]["Item_name"].ToString());
                        DgBomsEntry.Rows[i].Cells["Typess"].Value = dt_.Rows[i]["Typess"].ToString();
                        DgBomsEntry.Rows[i].Cells["unit_name"].Value = dt_.Rows[i]["unit_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["tx_Qty"].Value = dt_.Rows[i]["tx_Qty"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_.Rows[i]["tx_Qty"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["nt_qty"].Value = dt_.Rows[i]["nt_qty"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_.Rows[i]["nt_qty"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Rate"].Value = dt_.Rows[i]["Rate"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_.Rows[i]["Rate"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Amount"].Value = dt_.Rows[i]["Amount"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_.Rows[i]["Amount"].ToString()).ToString("0.00"); 
                        DgBomsEntry.Rows[i].Cells["LabourAmount"].Value = dt_.Rows[i]["LabourAmount"].ToString();
                        txtLabour.Text = dt_.Rows[i]["LabourAmount"].ToString();
                        DgBomsEntry.Rows[i].Cells["BOM_name"].Value = dt_.Rows[i]["BOM_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["BOM_No"].Value = dt_.Rows[i]["BOM_No"].ToString();
                    }
                    pnlledgername.Visible = false;
                    gridcalculation();
                }
            }
        }
        public void gridcalculation()
        {
            lblinputQty.Text = "0.00";
            lblinputval.Text = "0.00";
            lbloutputqty.Text = "0.00";
            lbloutputval.Text = "0.00";
            double inputQty = 0.00,inputval=0.00,ouptputQty=0.00,outputval=0.00;
            for (int i = 0; i < DgBomsEntry.Rows.Count - 1; i++)
            {
                if (DgBomsEntry.Rows[i].Cells["Typess"].Value != null && DgBomsEntry.Rows[i].Cells["Typess"].Value != "")
                {
                    if (DgBomsEntry.Rows[i].Cells["Typess"].Value.ToString().Trim() == "Input")
                    {
                        if (DgBomsEntry.Rows[i].Cells["nt_qty"].Value.ToString().Trim() != null || DgBomsEntry.Rows[i].Cells["nt_qty"].Value.ToString().Trim() != "")
                        {


                            inputQty += Convert.ToDouble(DgBomsEntry.Rows[i].Cells["nt_qty"].Value);
                            lblinputQty.Text = inputQty.ToString("0.00");
                        }
                        if (DgBomsEntry.Rows[i].Cells["Rate"].Value.ToString().Trim() != null || DgBomsEntry.Rows[i].Cells["Rate"].Value.ToString().Trim() != "")
                        {
                            inputval += Convert.ToDouble(DgBomsEntry.Rows[i].Cells["Amount"].Value);
                            lblinputval.Text = inputval.ToString("0.00");
                        }
                    }
                    if (DgBomsEntry.Rows[i].Cells["Typess"].Value.ToString().Trim() == "Output")
                    {
                        if (DgBomsEntry.Rows[i].Cells["nt_qty"].Value.ToString().Trim() != null || DgBomsEntry.Rows[i].Cells["nt_qty"].Value.ToString().Trim() != "")
                        {
                            ouptputQty += Convert.ToDouble(DgBomsEntry.Rows[i].Cells["nt_qty"].Value);
                            lbloutputqty.Text = ouptputQty.ToString("0.00");
                        }
                        if (DgBomsEntry.Rows[i].Cells["Rate"].Value.ToString().Trim() != null || DgBomsEntry.Rows[i].Cells["Rate"].Value.ToString().Trim() != "")
                        {
                            outputval += Convert.ToDouble(DgBomsEntry.Rows[i].Cells["Amount"].Value);
                            lbloutputval.Text = outputval.ToString("0.00");
                        }
                    }
                }
            }
        }
        private void txtLabourAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter||e.KeyCode==Keys.Tab)
            {
                if (valueschages == "1")
                {
                    if (lstledgerName.SelectedItems.Count > 0)
                    {
                        txtLabourAmount.Text = lstledgerName.SelectedItem.ToString();
                        listcontentclick();

                    }

                }
                //else if (valueschages == "2")
                //{
                //    if (lstledgerName.SelectedItems.Count > 0)
                //    {
                //        txtBomName.Text = lstledgerName.SelectedItems.ToString();
                //    }
                //}
                txtBomName.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                if (lstledgerName.SelectedIndex < lstledgerName.Items.Count - 1)
                {
                    lstledgerName.SetSelected(lstledgerName.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstledgerName.SelectedIndex > 0)
                {
                    lstledgerName.SetSelected(lstledgerName.SelectedIndex - 1, true);
                }
            }
        }
        private void txtBomName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (valueschages == "2")
                {
                    if (lstledgerName.SelectedItems.Count > 0)
                    {
                        txtBomName.Text = lstledgerName.SelectedItem.ToString();
                        listcontentclick();
                        txtissueQty.Focus(); 
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (lstledgerName.SelectedIndex < lstledgerName.Items.Count - 1)
                {
                    lstledgerName.SetSelected(lstledgerName.SelectedIndex + 1, true);
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (lstledgerName.SelectedIndex > 0)
                {
                    lstledgerName.SetSelected(lstledgerName.SelectedIndex - 1, true);
                }
            }
            
        }
        private void txtissueQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtLabour.Focus();
                pnlledgername.Visible = false;
               // DgBomsEntry.Rows.Clear();
                if (dt_.Rows.Count > 0)
                {
                    if (txtissueQty.Text == "" || txtissueQty.Text == "0")
                    {
                        txtissueQty.Text = "1";
                    }
                    for (int i = 0; i < dt_.Rows.Count; i++)
                    {
                       // DgBomsEntry.Rows.Add();
                        DgBomsEntry.Rows[i].Cells["Item_code"].Value = dt_.Rows[i]["Item_code"].ToString();
                        DgBomsEntry.Rows[i].Cells["Item_name"].Value = dt_.Rows[i]["Item_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["Typess"].Value = dt_.Rows[i]["Typess"].ToString();
                        DgBomsEntry.Rows[i].Cells["unit_name"].Value = dt_.Rows[i]["unit_name"].ToString();
                        if (DgBomsEntry.Rows[i].Cells["tx_Qty"].Value == null || DgBomsEntry.Rows[i].Cells["tx_Qty"].Value=="")
                        {
                            DgBomsEntry.Rows[i].Cells["tx_Qty"].Value = "0.00";
                        }
                        DgBomsEntry.Rows[i].Cells["tx_Qty"].Value = Convert.ToDouble(DgBomsEntry.Rows[i].Cells["tx_Qty"].Value) > 0 ? Convert.ToDouble(DgBomsEntry.Rows[i].Cells["tx_Qty"].Value).ToString("0.00"): Convert.ToDouble(dt_.Rows[i]["tx_Qty"].ToString()).ToString("0.00");
                       
                       // DgBomsEntry.Rows[i].Cells["tx_Qty"].Value = dt_.Rows[i]["tx_Qty"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_.Rows[i]["tx_Qty"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["nt_qty"].Value = (Convert.ToDouble(dt_.Rows[i]["nt_qty"].ToString()) * Convert.ToDouble(txtissueQty.Text)).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Rate"].Value = dt_.Rows[i]["Rate"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_.Rows[i]["Rate"].ToString()).ToString("0.00");
                        
                        DgBomsEntry.Rows[i].Cells["Amount"].Value =  (Convert.ToDouble(DgBomsEntry.Rows[i].Cells["nt_qty"].Value)*Convert.ToDouble(dt_.Rows[i]["Rate"].ToString())).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["LabourAmount"].Value = dt_.Rows[i]["LabourAmount"].ToString();
                        DgBomsEntry.Rows[i].Cells["BOM_name"].Value = dt_.Rows[i]["BOM_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["BOM_No"].Value = dt_.Rows[i]["BOM_No"].ToString();
                        if (Convert.ToDouble(DgBomsEntry.Rows[i].Cells["tx_Qty"].Value) > 0)
                        {
                            double totamount = 0.00;
                            totamount = (Convert.ToDouble(DgBomsEntry.Rows[i].Cells["tx_Qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[i].Cells["Rate"].Value) * Convert.ToDouble(DgBomsEntry.Rows[i].Cells["nt_Qty"].Value)) / 100;
                            DgBomsEntry.Rows[i].Cells["Amount"].Value = totamount.ToString("0.00"); 
                        }
                    }
                    pnlledgername.Visible = false;
                    gridcalculation();
                }
            }
        }
        private void txtissueQty_Enter(object sender, EventArgs e)
        {
            pnlledgername.Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtissueQty.Text.Trim() == "" || txtissueQty.Text.Trim() == null)
            {
                txtissueQty.Text = "1";
            }
            if (txtLabour.Text.Trim() == "" || txtLabour.Text.Trim() == null)
            {
                txtLabour.Text = "0";
            }
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            if (issuenumber.ToString().Trim() != "")
            {
                //After The Process Delete The Stktrn_table values Before
            }

            dt_BomissueCreation.Rows.Clear();
            cmd = new SqlCommand("SP_Bomissuecreatoin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bom_no",lblBOmBillno.Text);
            cmd.Parameters.AddWithValue("@Labouramount", txtLabour.Text);
            cmd.Parameters.AddWithValue("@Dates",dtpDate.Value);
            cmd.Parameters.AddWithValue("@BomName",txtBomName.Text);
            cmd.Parameters.AddWithValue("@totqty",txtissueQty.Text);
            cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);
            cmd.Parameters.AddWithValue("@labouttypes", txtLabourAmount.Text);
            cmd.Parameters.AddWithValue("@BomNumber", (DgBomsEntry.Rows[0].Cells["BOM_No"].Value.ToString() == "" || DgBomsEntry.Rows[0].Cells["BOM_No"].Value.ToString() == "") ? "" : DgBomsEntry.Rows[0].Cells["BOM_No"].Value.ToString());
            int k = 0;
            for (int i = 0; i < DgBomsEntry.Rows.Count; i++)
            {
                string values = "";
                if (DgBomsEntry.Rows[i].Cells["Item_name"].Value != null && DgBomsEntry.Rows[i].Cells["Item_name"].Value != "")
                {
                    if (DgBomsEntry.Rows[i].Cells["Typess"].Value.ToString().Trim() == "Input")
                    {
                        values = "False";
                        k=++k;
                    }
                    else
                    {
                        values = "True";
                    }
                      //  dt_BomissueCreation.Rows.Add(DgBomsEntry.Rows[i].Cells["Item_code"].Value, DgBomsEntry.Rows[i].Cells["Item_name"].Value, DgBomsEntry.Rows[i].Cells["unit_name"].Value, DgBomsEntry.Rows[i].Cells["Typess"].Value, DgBomsEntry.Rows[i].Cells["tx_Qty"].Value, DgBomsEntry.Rows[i].Cells["nt_qty"].Value, DgBomsEntry.Rows[i].Cells["Rate"].Value, DgBomsEntry.Rows[i].Cells["Amount"].Value, DgBomsEntry.Rows[i].Cells["BOM_No"].Value, DgBomsEntry.Rows[i].Cells["LabourAmount"].Value, DgBomsEntry.Rows[i].Cells["BOM_name"].Value);   
                    dt_BomissueCreation.Rows.Add(DgBomsEntry.Rows[i].Cells["Item_code"].Value, DgBomsEntry.Rows[i].Cells["Item_name"].Value, DgBomsEntry.Rows[i].Cells["unit_name"].Value,values.ToString(), DgBomsEntry.Rows[i].Cells["tx_Qty"].Value, DgBomsEntry.Rows[i].Cells["nt_qty"].Value, DgBomsEntry.Rows[i].Cells["Rate"].Value, DgBomsEntry.Rows[i].Cells["Amount"].Value, DgBomsEntry.Rows[i].Cells["BOM_No"].Value, DgBomsEntry.Rows[i].Cells["LabourAmount"].Value, DgBomsEntry.Rows[i].Cells["BOM_name"].Value);   
                }
            }
            if (dt_BomissueCreation.Rows.Count > 0)
            {
                cmd.Parameters.AddWithValue("@VoucherSnumber", k);
                cmd.Parameters.AddWithValue("@dt_gridload", dt_BomissueCreation);
                cmd.ExecuteNonQuery();
                //update item table missing values enter here :
                cmd = new SqlCommand("SP_BomIssueAlterItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dt_gridload", dt_BomissueCreation);
                cmd.Parameters.AddWithValue("@DeleteStkrnvalue",passingvalues.BomDeleteStkrnValues.ToString().Trim());
                cmd.Parameters.AddWithValue("@Dates", dtpDate.Value);
                cmd.ExecuteNonQuery();
                clear();
            }
            //Clear Coding:
        }
        private void DgBomsEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                // if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Type"].Index)
                {
                    if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Typess"].Index)
                    {
                        if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Typess"].Value.ToString().Trim() == "Input")
                        {
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Typess"].Value = "Output";
                            gridcalculation();
                        }
                        else
                        {
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Typess"].Value = "Input";
                            gridcalculation();
                        }
                       //QtyCalculation();
                    }
                }
            }
        }
        private void txtLabour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DgBomsEntry.Focus();
            }
        }
        public void clear()
        {
            DgBomsEntry.Rows.Clear();
            txtLabourAmount.Text = "";
            txtissueQty.Text = "";
            txtLabour.Text = "0";
        }
        private void validateTextInteger(object sender, EventArgs e)
        {
            Exception X = new Exception();

            TextBox T = (TextBox)sender;

            try
            {
                if (T.Text != "-")
                {
                    int x = int.Parse(T.Text);
                }
            }
            catch (Exception)
            {
                try
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
                catch (Exception) { }
            }
        }

        private void DgBomsEntry_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < DgBomsEntry.Rows.Count - 1; i++)
            {
                double values = 0.00;
                if ((DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value != "") && (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value.ToString() != "0" && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value.ToString().ToString() != "0.00"))
                {
                    if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value != "")
                    {
                        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value = Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value).ToString("0.00");
                        if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value != "")
                        {
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value = Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value).ToString("0.00");

                            values += (Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value));
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value = values.ToString("0.00");
                        }
                        else
                        {
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value == "" || DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value ==null? "0.00" : Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
                        }
                    }
                    else 
                    {
                        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value==""|| DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value==null?"0.00": Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value).ToString("0.00");
                    }
                }
                else
                {
                    DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["tx_Qty"].Value = "0.00";
                    DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value == "" || DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value == null? "0.00" : Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value).ToString("0.00");
                    DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["nt_qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value)).ToString("0.00");

                }
            }
        }

        private void txtLabourAmount_TextChanged(object sender, EventArgs e)
        {
            isChk = false;
            cmd = new SqlCommand("SP_SelectQuery",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "Ledsel_tableSelectLike");
            cmd.Parameters.AddWithValue("@itemName",txtLabourAmount.Text);
            cmd.Parameters.AddWithValue("@ItemCode", "");
            adp = new SqlDataAdapter(cmd);
            DataTable dtLes = new DataTable();
            dtLes.Rows.Clear();
            adp.Fill(dtLes);
            if (dtLes.Rows.Count > 0)
            {
                isChk = true;
                string tempstr = dtLes.Rows[0]["Ledsel_name"].ToString();
                for (int k = 0; k < lstledgerName.Items.Count; k++)
                {
                    if (tempstr == lstledgerName.Items[k].ToString())
                    {
                        lstledgerName.SetSelected(k, true);
                        txtLabourAmount.Select();
                        chk = "1";
                        txtLabourAmount.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        break;
                    }
                }
            }
            if (isChk == false)
            {
                chk = "1";
                if (txtLabourAmount.Text != "")
                {
                    string name = txtLabourAmount.Text.Remove(txtLabourAmount.Text.Length - 1);
                    txtLabourAmount.Text = name.ToString();
                    txtLabourAmount.Select(txtLabourAmount.Text.Length, 0);
                }
                txtLabourAmount.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
            }
            else
            {
                chk = "1";
            }
        }

        private void DgBomsEntry_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress += new KeyPressEventHandler(gridDisplay_KeyPress);
                }
            }
        }
        private void gridDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DgBomsEntry.CurrentCell.ColumnIndex == 4 || DgBomsEntry.CurrentCell.ColumnIndex == 5 || DgBomsEntry.CurrentCell.ColumnIndex == 6 || DgBomsEntry.CurrentCell.ColumnIndex == 7)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                   
                }
                // allow one decimal point
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
