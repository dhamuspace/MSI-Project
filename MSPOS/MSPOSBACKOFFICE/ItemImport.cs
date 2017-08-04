using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.Configuration;



namespace MSPOSBACKOFFICE
{
    public partial class ItemImport : Form
    {
        public ItemImport()
        {
            InitializeComponent();

            csvData.Columns.Add("Itemno");
            csvData.Columns.Add("Item Code");
            csvData.Columns.Add("Item Active");
            csvData.Columns.Add("Item Name");
            csvData.Columns.Add("Alias Name");
            csvData.Columns.Add("Print Name");
            csvData.Columns.Add("Group Name");
            csvData.Columns.Add("Model Name");
           // csvData.Columns.Add("p.unit");
           // csvData.Columns.Add("convertion Radio");
           //  csvData.Columns.Add("Marign");
            csvData.Columns.Add("Brand Name");
            csvData.Columns.Add("Unit Name");
            csvData.Columns.Add("COST");
            csvData.Columns.Add("MRSP");
            csvData.Columns.Add("Remarks");
            csvData.Columns.Add("SPECIAL - 1");
            csvData.Columns.Add("SPECIAL - 2");
          //  csvData.Columns.Add("Min Stock Qty ");
           // csvData.Columns.Add("Max Stock Qty");
           // csvData.Columns.Add("RockName");
           // csvData.Columns.Add("Warrenty");
           // csvData.Columns.Add("Tax Type");
           // csvData.Columns.Add("Remarks");
           // csvData.Columns.Add("Nt Sal Qty");
           // csvData.Columns.Add("Nt open Qty");
            csvData.Columns.Add("SPECIAL - 3");
            csvData.Columns.Add("Tax Type");
      


            //datat Table Values Added At Run Time 
            dtselect.Columns.Add("Values");

            //Add Values to that Particular columns:
            dtselect.Rows.Clear();
            dtselect.Rows.Add("Itemno");
            dtselect.Rows.Add("Item Code");
            dtselect.Rows.Add("Item Active");
            dtselect.Rows.Add("Item Name");
            dtselect.Rows.Add("Alias Name");
            dtselect.Rows.Add("Print Name");
            dtselect.Rows.Add("Group Name");
            dtselect.Rows.Add("Model Name");
            dtselect.Rows.Add("Brand Name");
            dtselect.Rows.Add("Unit Name");
            dtselect.Rows.Add("COST");
            dtselect.Rows.Add("MRSP");
            dtselect.Rows.Add("Remarks");
            dtselect.Rows.Add("SPECIAL - 1");
            dtselect.Rows.Add("SPECIAL - 2");
            dtselect.Rows.Add("SPECIAL - 3");
            dtselect.Rows.Add("Tax Type");
        }
        DataTable dtselect = new DataTable();
        private void bntExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataTable csvData = new DataTable();
        private void ItemImport_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Default";
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //List<string> colors = new List<string>();
            //for (int i = 0; i < DgItemImport.Rows.Count - 1; i++)
            //{
            //    string t2 = Convert.ToString(DgItemImport.Rows[i].Cells["Particulars"].Value.ToString()==""?"":Convert.ToString(DgItemImport.Rows[i].Cells["Particulars"].Value.ToString()));
            //    string t3 = Convert.ToString(DgItemImport.Rows[i].Cells["Position"].Value.ToString() == "" ? "0" : Convert.ToString(DgItemImport.Rows[i].Cells["Position"].Value.ToString()));
            //    int k = Convert.ToInt32(t3);
            //    colors.Insert(i, t2,k);
            //}
            if (txtpathlocation.Text.Trim() != "")
            {
                using (TextFieldParser csvReader = new TextFieldParser(txtpathlocation.Text))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //Read columns from CSV file, remove this line if columns not exits  
                    string[] colFields = csvReader.ReadFields();
                    int c = colFields.Count();
                    //  foreach (string column in colFields)
                    // {
                    //DataColumn datecolumn = new DataColumn(column);
                    //datecolumn.AllowDBNull = true;
                    //csvData.Columns.Add(column);
                    //  }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
                //Empty Columns Remove in Datatable
                foreach (var column in csvData.Columns.Cast<DataColumn>().ToArray())
                {
                    if (csvData.AsEnumerable().All(dr => dr.IsNull(column)))csvData.Columns.Remove(column);
                }
                //Getting All Column Header Name of Datatable:
                string[] columnNames = (from dc in csvData.Columns.Cast<DataColumn>()select dc.ColumnName).ToArray();
                int c1 = Convert.ToInt32(columnNames.Count());
            }
        }
        string filename1 = null, FileName;
        private void btnbrows_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // To list only csv files, we need to add this filter
            openFileDialog.Filter = "|*.csv";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtpathlocation.Text = openFileDialog.FileName;
            }
        }
        private void gridDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DgItemImport.CurrentCell.ColumnIndex == 1)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }
        System.Windows.Forms.Control cntObject;
        private void DgItemImport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.KeyPress += new KeyPressEventHandler(gridDisplay_KeyPress);
            }

            if (this.DgItemImport.CurrentCell.ColumnIndex == this.DgItemImport.Columns["Particulars"].Index) //Item_name
            {
                string[] postSource = dtselect.AsEnumerable().Select<System.Data.DataRow, String>(x => x.Field<String>("Values")).ToArray();
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                te.AutoCompleteCustomSource.AddRange(postSource);
                te.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            if (this.DgItemImport.CurrentCell.ColumnIndex == this.DgItemImport.Columns["Position"].Index) //Item_name
            {
                TextBox te = e.Control as TextBox;
                te.AutoCompleteMode = AutoCompleteMode.None;
                //te.AutoCompleteCustomSource.AddRange(new string[] { "one", "two", "three" });
                te.AutoCompleteSource = AutoCompleteSource.None;
            }
        }
        private void DgItemImport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode ==Keys.Y)
            {
                int Row = DgItemImport.CurrentRow.Index;
              //  DgItemImport.Columns.RemoveAt(Row);
            }
        }
        DataTable dtDuplicate = new DataTable();
        
        private void btnImport_Click(object sender, EventArgs e)
        {
            dtDuplicate.Rows.Clear();
            for (int i = dtDuplicate.Columns.Count - 1; i >= 0; i--)
            {
                dtDuplicate.Columns.RemoveAt(i);
            }
            //Create Datatable Columns Name Here
            for (int j = 0; j < DgItemImport.Rows.Count - 1; j++)
            {
                dtDuplicate.Columns.Add(DgItemImport.Rows[j].Cells["Particulars"].Value.ToString());
            }
            int o = 0;
            for (int i = 0; i < DgItemImport.Rows.Count-1; i++)
            {
                int column = Convert.ToInt32(DgItemImport.Rows[i].Cells["Position"].Value.ToString());
                if (dtDuplicate.Rows.Count <=0)
                {
                    for (int k = 0;k<csvData.Rows.Count; k++)
                    {
                       // 0'th Columns Addedd:
                        dtDuplicate.Rows.Add(csvData.Rows[k][column - 1].ToString());
                    }
                    o = ++o;
                }
                else
                {
                    for (int k = 0; k < csvData.Rows.Count; k++)
                    {
                        // Another Columns Values Added:
                        dtDuplicate.Rows[k][o] = csvData.Rows[k][column - 1].ToString();
                    }
                    o = ++o;
                }
            }
            if (dtDuplicate.Rows.Count > 0)
            {
                for (int i = 0;i< dtDuplicate.Columns.Count; i++)
                {
                    if (dtDuplicate.Columns[i].ToString() == "Brand Name")
                    {
                        for (int k = 0; k < dtDuplicate.Rows.Count; k++)
                        {
                            SqlCommand cmd = new SqlCommand("SPImportItemInsert", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@BrandName", dtDuplicate.Columns[i]["Brand Name"]);
                            cmd.ExecuteNonQuery();
                        } 
                    }
                    if (dtDuplicate.Columns[i].ToString() == "Group Name")
                    {
                        for (int k = 0; k < dtDuplicate.Rows.Count; k++)
                        {
                            //SqlCommand cmd = new SqlCommand("SPImportItemInsert", con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@GroupName", dtDuplicate.Columns[i]["Group Name"]);
                            //cmd.ExecuteNonQuery();
                        }
                    }
                    if (dtDuplicate.Columns[i].ToString() == "Model Name")
                    {
                        for (int k = 0; k < dtDuplicate.Rows.Count; k++)
                        {
                            //SqlCommand cmd = new SqlCommand("SPImportItemInsert", con);
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@ModelName", dtDuplicate.Columns[i]["Model Name"]);
                            //cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            string DtValues = "";
            int i1=0;
            string query = "Insert into item_table( ";
            for (int k = 0; k < DgItemImport.Rows.Count - 1; k++)
            {
                if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Item Code")
                {
                    query += "item_code,";
                    //DtValues += "<"+dtDuplicate.Rows[i1]["Item Code"].ToString()+">" + ",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Item no")
                {
                    query += "item_no,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["Item no"].ToString()+">" + ",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Item Active")
                {
                    query += "item_Active,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["Item no"].ToString() + ">" + ",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Item Name")
                {
                    query += "item_name,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["Item Name"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Alias Name")
                {
                    query += "Item_aliasname,Item_mtaliasname,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["Alias Name"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Print Name")
                {
                    query += "Item_Printname,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["Print Name"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Group Name")
                {

                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Model Name")
                {

                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "P.Unit")
                {
                    query += "Item_ndp,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["P.Unit"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Brand Name")
                {

                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Unit Name")
                {
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "COST")
                {
                    query += "Item_cost,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["COST"].ToString()+">" + ",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "MRSP")
                {
                    query += "Item_mrsp,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["MRSP"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Remarks")
                {
                    query += "Item_Remarks,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["Remarks"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "SPECIAL - 1")
                {
                    query += "Item_Special1,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["SPECIAL - 1"].ToString()+">" + ",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "SPECIAL - 2")
                {
                    query += "Item_Special2,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["SPECIAL - 2"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "SPECIAL - 3")
                {
                    query += "ITem_Special3,";
                    //DtValues += "<" + dtDuplicate.Rows[i1]["SPECIAL - 3"].ToString() + ">"  +",";
                }
                else if (DgItemImport.Rows[k].Cells["Particulars"].Value.ToString().Trim() == "Tax Type")
                {
                  //  DtValues += dtDuplicate.Rows[i1]["Tax Type"].ToString();
                }
            }
            string emptuy = "";
            query = query.TrimEnd(',').ToString();
            emptuy = query + ")" + "Values(" ;
            string emp="";
            string s1 = emptuy.Remove(emptuy.Length - 2);
            for (i1 = 0; i1 < dtDuplicate.Rows.Count; i1++)
            {
                emp = "";
                for (int j = 0; j<dtDuplicate.Columns.Count; j++)
                {
                    emp +="'"+dtDuplicate.Rows[i1][j].ToString()+"'";
                    emp += ",";
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                emptuy += emp.ToString().TrimEnd(',');
                emptuy += ")";  
                //Insert Query Executed Here:
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(emptuy, con);
                cmd.ExecuteNonQuery();
                //Clear The Values:
                emptuy = "";
                emptuy += query + ")" + "Values(";
            }
        }
    }
}
