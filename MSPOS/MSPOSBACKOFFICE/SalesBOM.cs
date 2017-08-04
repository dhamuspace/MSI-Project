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
    public partial class SalesBOM : Form
    {
        public SalesBOM()
        {
            InitializeComponent();
            DgBomsEntry.DefaultCellStyle.ForeColor = Color.Black;
            //DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            DgBomsEntry.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            
            DgBomsEntry.BackgroundColor = Color.White;

            foreach (DataGridViewColumn col in DgBomsEntry.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            DgBomsEntry.Columns["Amount"].ReadOnly = true;
            DgBomsEntry.Columns["Type"].ReadOnly = true;
            txtLabourcharge.Text = "0.00";

            dt.Columns.Add("ItemCode");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("BOMSNo");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Type");
            dt.Columns.Add("TaxQty");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Amount");
           // string values = "";
           
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt = new DataTable();
        DataTable dtchk1 = new DataTable();
        private void SalesBOM_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtBomName;
            DgBomsEntry.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["TaxQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgBomsEntry.Columns["Unit"].ReadOnly = true;
            if (passingvalues.BOMNO.ToString() != null && passingvalues.BOMNO.ToString() != "")
            {
                cmd = new SqlCommand("SP_SelectQuery",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SelectBomValues");
                cmd.Parameters.AddWithValue("@itemName",passingvalues.BOMNO.ToString());
                cmd.Parameters.AddWithValue("@ItemCode","");
                DataTable dtselectvalues = new DataTable();
                dtchk1.Rows.Clear();

                dtselectvalues.Rows.Clear();
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dtselectvalues);
                if (dtselectvalues.Rows.Count > 0)
                {
                    dtchk1 = dtselectvalues.Clone();
                    foreach (DataRow drtableOld in dtselectvalues.Rows)
                    {
                        dtchk1.ImportRow(drtableOld);
                    }
                    for (int i = 0; i < dtselectvalues.Rows.Count; i++)
                    {
                        DgBomsEntry.Rows.Add();  
                        DgBomsEntry.Rows[i].Cells["ItemCode"].Value = dtselectvalues.Rows[i]["Item_code"].ToString();
                        DgBomsEntry.Rows[i].Cells["ItemNames"].Value = dtselectvalues.Rows[i]["Item_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["Unit"].Value = dtselectvalues.Rows[i]["unit_name"].ToString();
                        DgBomsEntry.Rows[i].Cells["Type"].Value = dtselectvalues.Rows[i]["Typess"].ToString();
                        DgBomsEntry.Rows[i].Cells["TaxQty"].Value = dtselectvalues.Rows[i]["tx_Qty"].ToString() == "" || dtselectvalues.Rows[i]["tx_Qty"].ToString()==null ? "0.00" : Convert.ToDouble(dtselectvalues.Rows[i]["tx_Qty"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Qty"].Value = dtselectvalues.Rows[i]["nt_qty"].ToString() == "" || dtselectvalues.Rows[i]["nt_qty"].ToString()==null ? "0.00" : Convert.ToDouble(dtselectvalues.Rows[i]["nt_qty"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Rate"].Value = dtselectvalues.Rows[i]["Rate"].ToString() == "" || dtselectvalues.Rows[i]["Rate"].ToString() ==null? "0.00" : Convert.ToDouble(dtselectvalues.Rows[i]["Rate"].ToString()).ToString("0.00");
                        DgBomsEntry.Rows[i].Cells["Amount"].Value = dtselectvalues.Rows[i]["Amount"].ToString() == "" || dtselectvalues.Rows[i]["Amount"].ToString()==null ? "0.00" : Convert.ToDouble(dtselectvalues.Rows[i]["nt_qty"].ToString()).ToString("0.00"); 
                        txtBomName.Text = dtselectvalues.Rows[i]["BOM_name"].ToString();
                        txtLabourcharge.Text = dtselectvalues.Rows[i]["LabourAmount"].ToString();                   
                    }
                }
                QtyCalculation();
            }
            dtchk1.Rows.Clear();

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Header1.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
       // double amounts1 = 0.00, amounts2 = 0.00;
        private void DgBomsEntry_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dtchk1.Rows.Count <= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (DgBomsEntry.CurrentRow != null && e.ColumnIndex == 0)
                    {
                        string itemcode = "";
                        if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemCode"].Value != null)
                        {
                            itemcode = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemCode"].Value.ToString();
                            ItemcodeorItemName(itemcode);
                            if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemCode"].Value != null)
                            {
                                if (dt_items.Rows.Count > 0)
                                {
                                }
                                else
                                {
                                    MyMessageBox1.ShowBox("ItemCode Not Found", "Warning");
                                    int nextindex = Math.Min(this.DgBomsEntry.Columns.Count - 1, this.DgBomsEntry.CurrentCell.ColumnIndex + 1);
                                    SetColumnIndex method = new SetColumnIndex(Mymethod);
                                    this.DgBomsEntry.BeginInvoke(method, nextindex - 1);
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (DgBomsEntry.CurrentRow != null && e.ColumnIndex == 1)
                    {
                        string itemname = "";
                        if (DgBomsEntry.Rows[e.RowIndex].Cells["ItemNames"].Value != null)
                        {
                            itemname = DgBomsEntry.Rows[e.RowIndex].Cells["ItemNames"].Value.ToString();
                            ItemcodeorItemName(itemname);
                            if (itemname != null)
                            {
                                if (dt_items.Rows.Count > 0)
                                {
                                    if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemNames"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemNames"].Value != "")
                                    {
                                        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value = "0.00";
                                        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value = "0.00";
                                        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";
                                        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Type"].Value = "Input";
                                    }
                                }
                                else
                                {
                                    MyMessageBox1.ShowBox("Please Enter Correct ItemName or ItemCode", "Warning");
                                }
                            }
                        }
                    }
                }
               
                else if (DgBomsEntry.CurrentRow != null && e.ColumnIndex == 4)
                {
                 calulation();
                }
                else if (DgBomsEntry.CurrentRow != null && e.ColumnIndex == 5)
                {
                    //if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value != "" && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value != "" && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value != null)
                    {
                       calulation(); 
                    }
                }
                else if (DgBomsEntry.CurrentRow != null && e.ColumnIndex == 6)
                {
                    calulation();
                }
            }
        }
        public void calulation()
        {
            double Amts = 0.00, Amts2 = 0.00;
            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value = "0.00";
            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value == "" || DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value == null ? "0.00" : Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value).ToString("0.00");
            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value == "" || DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value == null ? "0.00" : Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value).ToString("0.00");
            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value = DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value == "" || DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value == null ? "0.00" : Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value).ToString("0.00");
            Amts = Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value);
            Amts2 = Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value);
            double tot=0.00;
            tot= (Amts + Amts2);
            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value=tot.ToString("0.00");
            QtyCalculation();
        }
        public void QtyCalculation()
        {
            double Intputqty = 0.00, OutputQty = 0.00,intputvalues=0.00,outputvalues=0.00;
            lbloutputval.Text = "0.00";
            lbloutputqty.Text = "0.00";
            lblinputval.Text = "0.00";
            lblinputQty.Text = "0.00";

           
            {
                for (int j = 0; j < DgBomsEntry.Rows.Count - 1; j++)
                {
                    if (DgBomsEntry.Rows[j].Cells["Type"].Value != null && DgBomsEntry.Rows[j].Cells["Type"].Value != "")
                    {
                        if (DgBomsEntry.Rows[j].Cells["Type"].Value.ToString().Trim() == "Input")
                        {
                            if (DgBomsEntry.Rows[j].Cells["Qty"].Value != "" && DgBomsEntry.Rows[j].Cells["Qty"].Value != null)
                            {
                                Intputqty += Convert.ToDouble(DgBomsEntry.Rows[j].Cells["Qty"].Value);
                                lblinputQty.Text = Intputqty.ToString("0.00");
                            }
                            if (DgBomsEntry.Rows[j].Cells["Rate"].Value != "" && DgBomsEntry.Rows[j].Cells["Rate"].Value != null)
                            {
                                intputvalues += Convert.ToDouble(DgBomsEntry.Rows[j].Cells["Rate"].Value);
                                lblinputval.Text = intputvalues.ToString("0.00");
                            }
                        }
                        else
                        {
                            if (DgBomsEntry.Rows[j].Cells["Type"].Value.ToString().Trim() == "Output")
                            {
                                if (DgBomsEntry.Rows[j].Cells["Qty"].Value != "" && DgBomsEntry.Rows[j].Cells["Qty"].Value != null)
                                {
                                    OutputQty += Convert.ToDouble(DgBomsEntry.Rows[j].Cells["Qty"].Value);
                                    lbloutputqty.Text = OutputQty.ToString("0.00");
                                }
                                if (DgBomsEntry.Rows[j].Cells["Rate"].Value != "" && DgBomsEntry.Rows[j].Cells["Rate"].Value != null)
                                {
                                    outputvalues += Convert.ToDouble(DgBomsEntry.Rows[j].Cells["Rate"].Value);
                                    lbloutputval.Text = outputvalues.ToString("0.00");
                                }
                            }
                        }

                    }
                }
            }
        }
        public delegate void SetColumnIndex(int i);
        public void Mymethod(int columnIndex)
        {
                this.DgBomsEntry.CurrentCell = this.DgBomsEntry.CurrentRow.Cells[columnIndex];
                this.DgBomsEntry.BeginEdit(true);   
        }
    //    System.Windows.Forms.Control cntObject;
        DataTable dt_items = new DataTable();
        public void ItemcodeorItemName(string itemNamecode)
        {
            SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "Action");
            cmd.Parameters.AddWithValue("@ItemCode", itemNamecode);
            cmd.Parameters.AddWithValue("@itemName", itemNamecode);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            dt_items.Rows.Clear();
            adp.Fill(dt_items);
            if (dt_items.Rows.Count > 0)
            {
                if (dt_items.Rows[0]["Item_code"].ToString().Trim() != "" && dt_items.Rows[0]["Item_code"].ToString() != null)
                {
                    DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemCode"].Value = dt_items.Rows[0]["Item_code"].ToString();
                }
                if (dt_items.Rows[0]["Item_name"].ToString().Trim() != "" && dt_items.Rows[0]["Item_name"].ToString().Trim() != null)
                {
                    DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["ItemNames"].Value = dt_items.Rows[0]["Item_name"].ToString();
                }
                SqlCommand cmd_nostable = new SqlCommand("select * from unit_table where unit_no='" + dt_items.Rows[0]["unit_no"].ToString() + "'", con);
                SqlDataAdapter adp_nostable = new SqlDataAdapter(cmd_nostable);
                DataTable dtnostable = new DataTable();
                dtnostable.Rows.Clear();
                adp_nostable.Fill(dtnostable);
                if (dtnostable.Rows.Count > 0)
                {
                    DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Unit"].Value = dtnostable.Rows[0]["Unit_name"].ToString();
                }
                DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value = Convert.ToDouble(dt_items.Rows[0]["item_ndp"].ToString()).ToString("0.00");
            }
        }
        private void DgBomsEntry_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                if (dt_items.Rows.Count > 0)
                {
                    //int nextindex = Math.Min(this.DgBomsEntry.Columns.Count - 1, this.DgBomsEntry.CurrentCell.ColumnIndex+1 );
                    //SetColumnIndex method = new SetColumnIndex(Mymethod);
                    //this.DgBomsEntry.BeginInvoke(method, DgBomsEntry.CurrentCell.RowIndex + 1);
                }
            }
           
        }
        private void gridDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (DgBomsEntry.CurrentCell.ColumnIndex == 4 || DgBomsEntry.CurrentCell.ColumnIndex == 5 || DgBomsEntry.CurrentCell.ColumnIndex == 6 || DgBomsEntry.CurrentCell.ColumnIndex == 7 || DgBomsEntry.CurrentCell.ColumnIndex == 8)
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
        private void DgBomsEntry_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress += new KeyPressEventHandler(gridDisplay_KeyPress);
                }
                con.Close();
                con.Open();
                SqlCommand namecmd = new SqlCommand("select Item_name,Item_code,Item_mrsp from Item_table   order by Item_name ASC", con);
                DataTable autofind = new DataTable();
                autofind.Rows.Clear();
                SqlDataAdapter nameadp = new SqlDataAdapter(namecmd);
                nameadp.Fill(autofind);
                con.Close();
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["ItemNames"].Index) //Item_name
                {
                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["ItemCode"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["ItemNames"].Index) //Item_name
                {
                    string[] postSource = autofind.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Item_name")).ToArray();

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteCustomSource.AddRange(postSource);
                    te.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Unit"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Qty"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Rate"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }

                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Type"].Index) //Item_name
                {

                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["TaxQty"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });

                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
                if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Amount"].Index) //Item_name
                {
                    TextBox te = e.Control as TextBox;
                    te.AutoCompleteMode = AutoCompleteMode.None;
                    //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                    te.AutoCompleteSource = AutoCompleteSource.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            passingvalues.BOMNO = "";
            this.Close();
        }

        private void DgBomsEntry_KeyDown(object sender, KeyEventArgs e)
        
        {
            if (e.KeyCode == Keys.Space)
            {
               // if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Type"].Index)
                {
                    if (this.DgBomsEntry.CurrentCell.ColumnIndex == this.DgBomsEntry.Columns["Type"].Index)
                    {
                        if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Type"].Value.ToString().Trim() == "Input")
                        {
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Type"].Value = "Output";
                        }
                        else
                        {
                            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Type"].Value = "Input";
                        }
                        QtyCalculation();
                    }
                }
                
            }
            if (DgBomsEntry.Rows[DgBomsEntry.CurrentRow.Index].Cells["ItemCode"].Value == null && DgBomsEntry.Rows[DgBomsEntry.CurrentRow.Index].Cells["ItemNames"].Value == null)
            {
                if (DgBomsEntry.CurrentCell.ColumnIndex == 1)
                {
                    if (this.DgBomsEntry.CurrentCell.ColumnIndex != this.DgBomsEntry.Columns.Count - 1)
                    {
                        if (DgBomsEntry.Rows.Count == 1)
                        {
                            int nextindex = Math.Min(this.DgBomsEntry.Columns.Count - 1, this.DgBomsEntry.CurrentCell.ColumnIndex);
                            SetColumnIndex method = new SetColumnIndex(Mymethod);
                            this.DgBomsEntry.BeginInvoke(method, nextindex - 1);
                            MyMessageBox1.ShowBox("Please Enter Item Name Or Item Code", "Warning");
                            goto end;
                        }
                        else if (DgBomsEntry.Rows.Count > 1)
                        {
                            MyMessageBox1.ShowBox("Please Enter Item Name Or Item Code", "Warning");

                            var selected = DgBomsEntry.SelectedCells;
                            for (int x = 0; x < selected.Count; x++)
                            {
                                DgBomsEntry.ClearSelection();
                            }
                            btnSave.Focus();
                        }
                    }
                }
            }
        end:
            int jki = 0;
        }
        private void DgBomsEntry_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        //    if (DgBomsEntry.Rows[DgBomsEntry.CurrentRow.Index].Cells["ItemCode"].Value == null && DgBomsEntry.Rows[DgBomsEntry.CurrentRow.Index].Cells["ItemNames"].Value == null)
        //    {
        //        if (this.DgBomsEntry.CurrentCell.ColumnIndex != this.DgBomsEntry.Columns.Count - 1)
        //        {
        //            if (DgBomsEntry.Rows.Count == 1)
        //            {
        //                //int nextindex = Math.Min(this.DgBomsEntry.Columns.Count - 1, this.DgBomsEntry.CurrentCell.ColumnIndex);
        //                //SetColumnIndex method = new SetColumnIndex(Mymethod);
        //                //this.DgBomsEntry.BeginInvoke(method, nextindex - 1);
        //                ////MyMessageBox1.ShowBox("Please Enter Item Name Or Item Code", "Warning");
        //                //goto end;
        //            }
        //            else if (DgBomsEntry.Rows.Count > 1)
        //            {
        //                MyMessageBox1.ShowBox("Please Enter Item Name Or Item Code", "Warning");

        //                var selected = DgBomsEntry.SelectedCells;
        //                for (int x = 0; x < selected.Count; x++)
        //                {
        //                    DgBomsEntry.ClearSelection();
        //                }
        //                btnSave.Focus();
        //            }
        //        }
        //    }
        //end:
        //    int jki = 0;
        }
        private bool txtValidate()
        {
            if (txtBomName.Text == "")
            {
                MyMessageBox1.ShowBox("Please Enter BOM Name","Warning");
                txtBomName.Focus();
                return false;
            }
            duplicatefind();
            if (dtdupicate.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        SqlCommand cmd = null;
        SqlDataAdapter adp = null;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtValidate())
            {
                if (DgBomsEntry.Rows.Count > 1)
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    dt.Rows.Clear();
                    int k = 0;
                    int r = 0;
                    if (passingvalues.BOMNO != "" && passingvalues.BOMNO != "")
                    {
                        cmd = new SqlCommand("delete from BOMMas_Table where BOM_No='" + passingvalues.BOMNO.ToString() + "'",con);
                        cmd.ExecuteNonQuery();
                    }
                    for (int i = 0; i < DgBomsEntry.Rows.Count; i++)
                    {
                        if (DgBomsEntry.Rows[i].Cells["ItemNames"].Value != null && DgBomsEntry.Rows[i].Cells["ItemNames"].Value != "")
                        {
                            cmd = new SqlCommand("SP_SelectQuery", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ActionType", "Action");
                            cmd.Parameters.AddWithValue("@ItemCode", "");
                            cmd.Parameters.AddWithValue("@itemName", DgBomsEntry.Rows[i].Cells["ItemNames"].Value.ToString());
                            DataTable dt1 = new DataTable();
                            dt1.Rows.Clear();
                            adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt1);
                            if (dt1.Rows.Count > 0)
                            {
                                string tupes1 = "";
                                string tupes = "";
                                tupes = DgBomsEntry.Rows[i].Cells["Type"].Value.ToString();
                                if (tupes == "Input")
                                {
                                    tupes1 = "False";
                                }
                                else
                                {
                                    tupes1 = "True";
                                }
                                r = ++k;

                                string ITEMCODEEMPTY="";
                                ITEMCODEEMPTY = (DgBomsEntry.Rows[i].Cells["ItemCode"].Value==null) ? "" : (Convert.ToString(DgBomsEntry.Rows[i].Cells["ItemCode"].Value.ToString()));
                                if( ITEMCODEEMPTY.ToString().Trim()!="" &&  ITEMCODEEMPTY.ToString().Trim()!=null)
                                {
                                    dt.Rows.Add(ITEMCODEEMPTY, DgBomsEntry.Rows[i].Cells["ItemNames"].Value, r, DgBomsEntry.Rows[i].Cells["Unit"].Value, tupes1, DgBomsEntry.Rows[i].Cells["TaxQty"].Value, DgBomsEntry.Rows[i].Cells["Qty"].Value, DgBomsEntry.Rows[i].Cells["Rate"].Value, DgBomsEntry.Rows[i].Cells["Amount"].Value);
                                }
                                else
                                {
                                    dt.Rows.Add(ITEMCODEEMPTY, DgBomsEntry.Rows[i].Cells["ItemNames"].Value, r, DgBomsEntry.Rows[i].Cells["Unit"].Value, tupes1, DgBomsEntry.Rows[i].Cells["TaxQty"].Value, DgBomsEntry.Rows[i].Cells["Qty"].Value, DgBomsEntry.Rows[i].Cells["Rate"].Value, DgBomsEntry.Rows[i].Cells["Amount"].Value);
                                } 
                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (txtValidate())
                        {
                            cmd = new SqlCommand("SP_BomCreation", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            string valuess="";
                            valuess=(Convert.ToDouble(txtLabourcharge.Text == "" ? "0.00" : txtLabourcharge.Text).ToString("0.00"));
                            cmd.Parameters.AddWithValue("@labercost",valuess);
                            cmd.Parameters.AddWithValue("@BomName", txtBomName.Text);
                            cmd.Parameters.AddWithValue("@dt_gridload", dt);
                            cmd.ExecuteNonQuery();
                            dt.Rows.Clear();
                            txtBomName.Text = "";
                            txtLabourcharge.Text = "";
                            DgBomsEntry.Rows.Clear();
                            lblinputQty.Text = "0.00";
                            lbloutputqty.Text = "0.00";
                            lbloutputval.Text = "0.00";
                            lblinputval.Text = "0.00";
                        }
                    }
                }
            }
            passingvalues.BOMNO = "";
        }

        private void txtBomName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLabourcharge.Focus();
            }
        }

        private void lblLabourCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DgBomsEntry.Focus();
            }
        }

        private void lblLabourCharge_Enter(object sender, EventArgs e)
        {
            if (txtLabourcharge.Text == "")
            {
                txtLabourcharge.Text = "0.00";
            }
            else
            {
                txtLabourcharge.Text = "";
            }
        }

        private void txtBomName_Leave(object sender, EventArgs e)
        {
            duplicatefind();
        }
        DataTable dtdupicate = new DataTable();
        public void duplicatefind()
        {
            if (txtBomName.Text != "" && txtBomName.Text != null)
            {
                cmd = new SqlCommand("SP_SelectQuery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "DuplicateFind");
                cmd.Parameters.AddWithValue("@ItemCode", "");
                cmd.Parameters.AddWithValue("@itemName", txtBomName.Text);
                adp = new SqlDataAdapter(cmd);
                dtdupicate.Rows.Clear();
                adp.Fill(dtdupicate);
                if (dtdupicate.Rows.Count > 0)
                {
                    MyMessageBox1.ShowBox("Duplicate BOM Name", "Warning");
                }
                else
                { }
            }
        }

        private void DgBomsEntry_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //for (int i = 0; i < DgBomsEntry.Rows.Count - 1; i++)
            //{
            //    double values = 0.00;
            //    if ((DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value != "") && (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value.ToString() != "0" && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value.ToString().ToString() != "0.00"))
            //    {
            //        if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value != "")
            //        {
            //            if (DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value != null && DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value != "")
            //            {
            //                DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value = Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value).ToString("0.00");
            //                values += (Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value));
            //                DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value = values.ToString("0.00");
            //            }
            //            else
            //            {
            //                DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value = "0.00";
            //            }
            //        }
            //        else
            //        {
            //            DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value = "0.00";
            //        }
            //    }
            //    else
            //    {
            //        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["TaxQty"].Value = "0.00";
            //        DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Amount"].Value = (Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Qty"].Value) * Convert.ToDouble(DgBomsEntry.Rows[DgBomsEntry.CurrentCell.RowIndex].Cells["Rate"].Value)).ToString("0.00");

            //    }
            //}
        }

        

           
       
    }
}
