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
using System.IO;
using System.Text.RegularExpressions;

namespace MSPOSBACKOFFICE
{
    public partial class Itemalteration : Form
    {
        public Itemalteration()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void txtItem_code_TextChanged(object sender, EventArgs e)
        {
        }
        private void txt_itemname_Enter(object sender, EventArgs e)
        {
            listActionType = "Group";
        }
        private void txtItem_code_Enter(object sender, EventArgs e)
        {
            panel2.Visible = false;
            label1.Visible = false;
            listitems.Visible = false;
        }
        DataTable dt = new DataTable();
        private void Itemalteration_Load(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select item_selname from item_seltable  ORDER BY item_selname DESC", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            //  int j = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Converting Language for ItemName //
                    Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                    Encoding Utf8 = Encoding.UTF8;
                    byte[] originalBytes = Windows1252.GetBytes(dt.Rows[i]["item_selname"].ToString());
                    string goodDecode = "";
                    goodDecode = Utf8.GetString(originalBytes);
                    //MessageBox.Show(goodDecode, "Re-decoded");
                    //listitems.Items.Add(dt.Rows[i]["item_selname"].ToString());
                    listitems.Items.Add(goodDecode);
                }
            }

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        string id_number;
        // string item_name;

        private void txtItem_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                if (txtItem_code.Text != "")
                {

                    DataTable dt_code = new DataTable();
                    //SqlCommand cmd = new SqlCommand("select item_no from item_table where item_code='" + txtItem_code.Text + "'", con);
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ActionType", "ItemCode");
                    cmd.Parameters.AddWithValue("@ItemCode", txtItem_code.Text);
                    cmd.Parameters.AddWithValue("@itemName", "");
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt.Rows.Clear();
                    adp.Fill(dt_code);
                    if (dt_code.Rows.Count > 0)
                    {
                        string id_number = dt_code.Rows[0]["item_no"].ToString();
                        ItemCreations frm = new ItemCreations(id_number);
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                        frm.Show();
                    }
                    else
                    {
                        DataTable dtbarcode = new DataTable();
                        //SqlCommand cmdbarcode = new SqlCommand("select * from barcode_table where barcode='" + txtItem_code.Text + "'", con);
                        SqlCommand cmdbarcode = new SqlCommand("SP_SelectQuery", con);
                        cmdbarcode.CommandType = CommandType.StoredProcedure;
                        cmdbarcode.Parameters.AddWithValue("ActionType", "Barcode");
                        //here put barcode values to itemcode:
                        cmdbarcode.Parameters.AddWithValue("@ItemCode", txtItem_code.Text);
                        cmdbarcode.Parameters.AddWithValue("@itemName", "");
                        SqlDataAdapter adpbarcode = new SqlDataAdapter(cmdbarcode);
                        adpbarcode.Fill(dtbarcode);
                        if (dtbarcode.Rows.Count > 0)
                        {
                            string id_number = dtbarcode.Rows[0]["item_no"].ToString();
                            ItemCreations frm = new ItemCreations(id_number);
                            frm.MdiParent = this.ParentForm;
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.WindowState = FormWindowState.Normal;
                            frm.Location = new Point(0, 80);
                            frm.Show();
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Item Code Not Found");
                        }
                    }
                }
                else
                {
                    txt_itemname.Focus();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isChk = false;
        private void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            panel2.Visible = true;
            label1.Visible = true;
            listitems.Visible = true;
            isChk = false;
            if (listActionType == "Group" && listActionType != null)
            {
                if (txt_itemname.Text.Trim() != null && txt_itemname.Text.Trim() != "")
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = null;
                    DataTable dt_selectitem = new DataTable();
                    dt_selectitem.Rows.Clear();

                    // Converting Language for ItemName //
                    Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                    Encoding Utf8 = Encoding.UTF8;
                    byte[] originalBytes = Windows1252.GetBytes(txt_itemname.Text.Trim());
                    string goodDecode = "";
                    goodDecode = Utf8.GetString(originalBytes);


                    //adp = new SqlDataAdapter("select item_selname from item_seltable  where item_selname like '" + txt_itemname.Text.Trim() + "%' ORDER BY item_selname DESC", con);
                    SqlCommand cmdAlter = new SqlCommand("select item_selname from item_seltable  where item_selname like @ItemName", con);
                    //cmdAlter.Parameters.AddWithValue("@ItemName", txt_itemname.Text.Trim() + "%");
                    cmdAlter.Parameters.AddWithValue("@ItemName", goodDecode + "%");
                    adp = new SqlDataAdapter(cmdAlter);

                    //SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@ActionType", "ItemSelNameChk");
                    //cmd.Parameters.AddWithValue("@ItemCode", "");
                    //cmd.Parameters.AddWithValue("@itemName",txt_itemname.Text);
                    //adp = new SqlDataAdapter(cmd);
                    isChk = false;
                    adp.Fill(dt_selectitem);
                    //}
                    if (dt_selectitem.Rows.Count > 0)
                    {
                        isChk = true;
                        string tempstr = dt_selectitem.Rows[0]["item_selname"].ToString().Trim();
                        for (int k = 0; k < listitems.Items.Count; k++)
                        {
                            if (tempstr == listitems.Items[k].ToString().Trim())
                            {
                                listitems.SetSelected(k, true);
                                txt_itemname.Select();
                                chk = "1";
                                txt_itemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                break;
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txt_itemname.Text != "")
                        {
                            string name = txt_itemname.Text.Remove(txt_itemname.Text.Length - 1);
                            txt_itemname.Text = name.ToString();
                            txt_itemname.Select(txt_itemname.Text.Length, 0);
                        }
                        txt_itemname.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                    }
                    else
                    {
                        chk = "1";
                    }
                }
            }
        }
        string listActionType;
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {

                if (listitems.SelectedIndex < listitems.Items.Count - 1)
                {
                    listitems.SetSelected(listitems.SelectedIndex + 1, true);
                }

            }
            if (e.KeyCode == Keys.Up)
            {
                if (listitems.SelectedIndex > 0)
                {
                    listitems.SetSelected(listitems.SelectedIndex - 1, true);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (listActionType == "Group")
                {
                    checkvaluestotextbox();
                }
            }
        }
        public void checkvaluestotextbox()
        {
            try
            {
                if (listitems.Visible == true)
                {
                    // Converting Language for ItemName //
                    Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                    Encoding Utf8 = Encoding.UTF8;
                    byte[] originalBytes = Windows1252.GetBytes(listitems.SelectedItem.ToString());
                    string goodDecode = "";
                    goodDecode = Utf8.GetString(originalBytes);
                    //MessageBox.Show(goodDecode, "Re-decoded");
                    //txt_itemname.Text = listitems.SelectedItem.ToString();
                    txt_itemname.Text = listitems.SelectedItem.ToString();
                }
                panel2.Visible = false;
                listitems.Visible = false;
                txt_itemname.Select();
                DataTable dt1 = new DataTable();
                if (txt_itemname.Text != null)
                {
                    SqlCommand cmd = new SqlCommand("SP_SelectQuery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "ItemSelName");
                    cmd.Parameters.AddWithValue("@ItemCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", txt_itemname.Text);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    dt.Rows.Clear();
                    adp.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        id_number = dt1.Rows[0]["item_no"].ToString();
                    }
                    if (id_number != "" && id_number != null)
                    {
                        ItemCreations frm = new ItemCreations(id_number);
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                }
            }
            catch
            { }
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
        private void listitems_Click(object sender, EventArgs e)
        {
            if (listitems.Text != "")
            {
                Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                Encoding Utf8 = Encoding.UTF8;
                byte[] originalBytes = Windows1252.GetBytes(listitems.SelectedItem.ToString());
                string goodDecode = "";
                goodDecode = Utf8.GetString(originalBytes);

                txt_itemname.Text = goodDecode;
                //txt_itemname.Text = listitems.SelectedItem.ToString();
                checkvaluestotextbox();
            }
        }

    }
}
