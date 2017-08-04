using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;

namespace MSPOSBACKOFFICE
{
    public partial class frmBarcodeSettings : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=MICRO-PC;Initial Catalog=MSPOS;Integrated Security=True");
        //SqlConnection con = new SqlConnection(@"Data Source=ASTRID-PC\SQLEXPRESS;Initial Catalog=Mspos;Persist Security Info=True;User ID=sa;password=!Password123");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        
        public frmBarcodeSettings()
        {
            InitializeComponent();
            con.Close();
            con.Open();
            dtGrid.Columns.Add("Name",typeof(string));
            dtGrid.Columns.Add("Name1", typeof(string));
            gridSelect.DataSource = dtGrid;
            gridSelect.Columns[1].Visible = false;
        }
        

        private void Pnl_Modname_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFormat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtFormat.Text.Trim() == "Format 1")
                {
                    txtFormat.Text = "Format 2";
                }
                else
                {
                    txtFormat.Text = "Format 1";
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtQtySeparator.Select();
            }
        }
        DataTable dtGrid = new DataTable();
        private void frmBarcodeSettings_Load(object sender, EventArgs e)
        {
            try
            {
                dtGrid.Rows.Add("Comp Name", " <<Comp Name>> ");
                dtGrid.Rows.Add("Item Code", " <<Item Code>> ");
                dtGrid.Rows.Add("Barcode", " <<Barcode>> ");
                dtGrid.Rows.Add("Item Name", " <<Item Name>> ");
                dtGrid.Rows.Add("Item Alias", " <<Item Alias>> ");
                dtGrid.Rows.Add("Item Alias - 1", " <<Item Alias - 1>> ");
                dtGrid.Rows.Add("Item Alias - 2", " <<Item Alias - 2>> ");
                dtGrid.Rows.Add("Item Alias - 3", " <<Item Alias - 3>> ");
                dtGrid.Rows.Add("ItemPrint Name", " <<ItemPrint Name>> ");
                dtGrid.Rows.Add("ItemPrint Name - 1", " <<ItemPrint Name - 1>> ");
                dtGrid.Rows.Add("ItemPrint Name - 2", " <<ItemPrint Name - 2>> ");
                dtGrid.Rows.Add("Item Name", " <<Item Name>> ");
                dtGrid.Rows.Add("Item Name - 1", " <<Item Name - 1>> ");
                dtGrid.Rows.Add("Item Name - 2", " <<Item Name - 2>> ");
                dtGrid.Rows.Add("Unit", " <<Unit>> ");
                dtGrid.Rows.Add("Item_Ndp", " <<Item_Ndp>> ");
                dtGrid.Rows.Add("Item_Cost", " <<Item_Cost>> ");
                dtGrid.Rows.Add("Item_Cost", " <<Item_Cost>> ");
                dtGrid.Rows.Add("Item_Mrsp", " <<Item_Mrsp>> ");
                dtGrid.Rows.Add("Item_Special1", " <<Item_Special1>> ");
                dtGrid.Rows.Add("Item_Special2", " <<Item_Special2>> ");
                dtGrid.Rows.Add("Item_Special3", " <<Item_Special3>> ");
                dtGrid.Rows.Add("No Of Labels", " <<No Of Labels>> ");
                dtGrid.Rows.Add("Item Group", " <<Item Group>> ");
                dtGrid.Rows.Add("Item Model", " <<Item Model>> ");
                dtGrid.Rows.Add("Item Brand", " <<Item Brand>> ");
                dtGrid.Rows.Add("End of Column", " <<End of Column>> ");
                dtGrid.Rows.Add("Ndp_Code", " <<Ndp_Code>> ");
                dtGrid.Rows.Add("Cost_Code", " <<Cost_Code>> ");
                dtGrid.Rows.Add("Mrsp_Code", " <<Mrsp_Code>> ");
                dtGrid.Rows.Add("Special1_Code", " <<Special1_Code>> ");
                dtGrid.Rows.Add("Special2_Code", " <<Special2_Code>> ");
                dtGrid.Rows.Add("Special3_Code", " <<Special3_Code>> ");
                dtGrid.Rows.Add("Ndp+Tax_Code", " <<Ndp+Tax_Code>> ");
                dtGrid.Rows.Add("Cost+Tax_Code", " <<Cost+Tax_Code>> ");
                dtGrid.Rows.Add("Mrsp+Tax_Code", " <<Mrsp+Tax_Code>> ");
                dtGrid.Rows.Add("Special1+Tax_Code", " <<Special1+Tax_Code>> ");
                dtGrid.Rows.Add("Special2+Tax_Code", " <<Special2+Tax_Code>> ");
                dtGrid.Rows.Add("Special3+Tax_Code", " <<Special3+Tax_Code>> ");
                dtGrid.Rows.Add("Supplier", " <<Supplier>> ");
                dtGrid.Rows.Add("Ledger Alias", " <<Ledger Alias>> ");
                dtGrid.Rows.Add("Invoice No", " <<Invoice No>> ");
                dtGrid.Rows.Add("Purchase_Date", " <<Purchase_Date>> ");
                dtGrid.Rows.Add("Ndp+Tax", " <<Ndp+Tax>> ");
                dtGrid.Rows.Add("Cost+Tax", " <<Cost+Tax>> ");
                dtGrid.Rows.Add("Mrsp+Tax", " <<Mrsp+Tax>> ");
                dtGrid.Rows.Add("Special1+Tax", " <<Special1+Tax>> ");
                dtGrid.Rows.Add("Special2+Tax", " <<Special2+Tax>> ");
                dtGrid.Rows.Add("Special3+Tax", " <<Special3+Tax>> ");
                dtGrid.Rows.Add("Month", " <<Month>> ");
                dtGrid.Rows.Add("Year", " <<Year>> ");
                dtGrid.Rows.Add("Remarks", " <<Remarks>> ");
                dtGrid.Rows.Add("Conv Unit", " <<Conv Unit>> ");
                dtGrid.Rows.Add("Conv Rate", " <<Conv Rate>> ");
                dtGrid.Rows.Add("Item Rack", " <<Item Rack>> ");
                dtGrid.Rows.Add("PKD. Date", " <<PKD. Date>> ");
                dtGrid.Rows.Add("EXP. Date", " <<EXP. Date>> ");
                dtGrid.Rows.Add("Date", " <<Date>> ");
                dtGrid.Rows.Add("Time", " <<Time>> ");
                gridSelect.DataSource = dtGrid;
                gridSelect.Columns[1].Visible = false;

                DataTable dtNew1 = new DataTable();
                dtNew1.Rows.Clear();
                SqlCommand cmd = new SqlCommand("Select * from Control_table", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtNew1);
                if (dtNew1.Rows.Count > 0)
                {
                    txtBarcode.Text = dtNew1.Rows[0]["BarcodeCoding"].ToString();
                }
                listSelect.SelectedIndex = -1;

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void btn_M_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridSelect_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (txtBarcode.Text.Trim() != "")
            {
                txtBarcode.Text += gridSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void gridSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBarcode.Text += gridSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void txtBefore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtBefore.Text.Trim() == "Before")
                {
                    txtBefore.Text = "After";
                }
                else
                {
                    txtBefore.Text = "Before";
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtCodeStartPos.Select();
            }
        }

        private void btnSaveCode_Click(object sender, EventArgs e)
        {
           try
            {
                SqlCommand cmd = new SqlCommand(@"update Control_table set BarcodeCoding=@tBarcode,BarcodeCoding2=@tBarcode,LabelsPerRow=@tLabelsPerRow,QtySeparator=@tQtySeparator,QtyAfterCode=@tQtyAfterCode,CodePos=@tCodePos,CodeLen=@tCodeLen,RatePos=@tRatePos,RateLen=@tRateLen", con);
                cmd.Parameters.AddWithValue("@tBarcode", txtBarcode.Text.Trim());
                cmd.Parameters.AddWithValue("@tLabelsPerRow", txtLabelPerRow.Text.Trim());
                cmd.Parameters.AddWithValue("@tQtySeparator", txtQtySeparator.Text.Trim());
                cmd.Parameters.AddWithValue("@tQtyAfterCode", (txtBefore.Text.Trim() == "Before") ? false : true);
                cmd.Parameters.AddWithValue("@tCodePos", (txtCodeStartPos.Text.Trim() == "") ? 0 : double.Parse(txtCodeStartPos.Text.Trim()));
                cmd.Parameters.AddWithValue("@tCodeLen", (txtNoOfCharCode.Text.Trim()=="")?0:double.Parse(txtNoOfCharCode.Text.Trim()));
                cmd.Parameters.AddWithValue("@tRatePos", (txtRateStartPos.Text.Trim()=="")?0:double.Parse(txtRateStartPos.Text.Trim()));
                cmd.Parameters.AddWithValue("@tRateLen", (txtNoOfCharRate.Text.Trim() == "") ? 0 : double.Parse(txtNoOfCharRate.Text.Trim()));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void gridSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBarcode.Text += " &" + listSelect.SelectedItem.ToString() + "& ";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void txtLabelPerRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtFormat.Select();
            }
        }

        private void txtQtySeparator_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQtySeparator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtBefore.Select();
            }
        }

        private void txtCodeStartPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtNoOfCharCode.Select();
            }
        }

        private void txtNoOfCharCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtRateStartPos.Select();
            }
        }

        private void txtRateStartPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txtNoOfCharRate.Select();
            }
        }

        private void txtNoOfCharRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               btnSaveCode.Select();
            }
        }

        private void txt0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txt1.Select();
            }
        }

        private void txt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               txt2.Select();
            }
        }

        private void txt2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt3.Select();
            }
        }

        private void txt3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt4.Select();
            }
        }

        private void txt4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt5.Select();
            }
        }

        private void txt5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt6.Select();
            }
        }

        private void txt6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt7.Select();
            }
        }

        private void txt7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt8.Select();
            }
        }

        private void txt8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt9.Select();
            }
        }

        private void txt9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               btn_M_Exit.Select();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

    
    }
}
