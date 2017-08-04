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

namespace SalesProject
{
    public partial class ChkingDataGrid : Form
    {
        public ChkingDataGrid()
        {
            InitializeComponent();
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Amt", typeof(string));           
            myDataGrid1.DataSource = dt;
           
        }
        MyDataGridNew myDataGrid1 = new MyDataGridNew();
        DataTable dt = new DataTable();
        System.Windows.Forms.Control cntObject;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        private void myDataGrid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            {
                e.Control.TextChanged += new EventHandler(textbox_TextChanged);
                //  e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OnTextBoxKeyDown); 
                cntObject = (System.Windows.Forms.Control)e.Control;
               
                //e.Control.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextBoxKeyDown);
                //cntObject.KeyDown += OnTextBoxKeyDown;
            }
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {

            DataSet dsNew = new DataSet();
            try
            {
                if (myDataGrid1.CurrentCell.ColumnIndex == 0)
                {
                    if (cntObject.Text.Trim() != null && cntObject.Text.Trim() != "")
                    {
                        // pnlSelect.Visible = true;
                        //  lblPnlTitle.Text = "Select Project Name";
                        SqlDataAdapter cmd = new SqlDataAdapter("Select Item_code+'-'+item_name from Item_table where item_name like '%" + cntObject.Text.Trim() + "%'", con);
                        bool isChk = false;
                        bool isChkNext = false;
                        dsNew.Tables.Clear();
                        cmd.Fill(dsNew, "Find");
                        //listSelect.Items.Clear();
                        if (dsNew.Tables["Find"].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsNew.Tables["Find"].Rows.Count; j++)
                            {
                                isChk = true;
                                string tempStr = dsNew.Tables["Find"].Rows[j][0].ToString();
                                for (int i = 0; i <listBox1.Items.Count; i++)
                                {
                                    string temp2 = listBox1.Items[i].ToString();
                                    if (tempStr == temp2)
                                    {
                                        isChkNext = true;
                                        listBox1.SelectedIndex = i;
                                        cntObject.Select();
                                        cntObject.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                                         chk = "1";
                                        //cntObject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSelectControl_KeyPress);

                                        //  var listBoxItem = (ListBoxItem)listSelect.ItemContainerGenerator.ContainerFromItem(listSelect.SelectedItem);
                                        // listBoxItem.Focus();
                                        //listBoxItem.Focus();
                                        break;
                                    }
                                }
                                if (isChkNext != false)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (listBox1.Items.Count > 0)
                            {
                                // listSelect.SelectedIndex = 0;
                            }
                        }
                        con.Close();
                        if (isChk == false)
                        {
                             chk = "2";
                            cntObject.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                        }
                    }
                    else
                    {
                         chk = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }
            // dgsales.Focus();
        }
        string chk = "2";
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;

                    // chk = "1";

                }
                else
                {
                    e.Handled = false;

                }
            }

        }
        private void myDataGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (myDataGrid1.CurrentCell.ColumnIndex == 0)
            {
                DataSet dsNew = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter("Select Item_code+'-'+item_name from Item_table", con);
                dsNew.Tables.Clear();
                adp.Fill(dsNew, "Find");
              listBox1.Items.Clear();
                for (int i = 0; i < dsNew.Tables["Find"].Rows.Count; i++)
                {
                    listBox1.Items.Add(dsNew.Tables["Find"].Rows[i][0].ToString());
                }
            }
        }

       
       
        private void OnTextBoxKeyDown1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                {
                    listBox1.SetSelected(listBox1.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (listBox1.SelectedIndex > 0)
                {
                    listBox1.SetSelected(listBox1.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                listBox1.Visible = false;
                cntObject.Text = listBox1.SelectedItem.ToString();
             
            }

        }
        private void ChkingDataGrid_Load(object sender, EventArgs e)
        {

        }

       
    }

    public class MyDataGridNew : DataGridView
    {
       
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.Enter)
            //{
            //    base.ProcessTabKey(Keys.Tab );
            //    return true;
            //}

            //return base.ProcessDialogKey(keyData);
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key.  
            if (key == Keys.Enter)
            {

                return this.ProcessTabKey(keyData);
            }
            else if (key == Keys.Down)
            {
                return false;
            }
            else if (key == Keys.Up)
            {
                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key.  
            if (e.KeyCode == Keys.Enter)
            {
                return this.ProcessTabKey(e.KeyData);
            }
            else if (e.KeyCode == Keys.Down)
            {

                return false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                return false;
            }
            return base.ProcessDataGridViewKey(e);
        }
    }
}
