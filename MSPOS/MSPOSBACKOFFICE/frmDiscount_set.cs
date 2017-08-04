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
using System.Collections;

namespace MSPOSBACKOFFICE
{
    public partial class frmDiscount_set : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        SqlDataReader dr = null;
        string[] eyesno = { "Yes", "No" };
        string[] allyesno = { "Yes", "No" };
        string[] sunyesno = { "Yes", "No" };
        string[] tueyesno = { "Yes", "No" };
        string[] monyesno = { "Yes", "No" };
        string[] wedyesno = { "Yes", "No" };
        string[] thursyesno = { "Yes", "No" };
        string[] friyesno = { "Yes", "No" };
        string[] satyesno = { "Yes", "No" };
        string[] Calc = { "Fixed", "Percentage" };
        public frmDiscount_set()
        {
            InitializeComponent();
            try
            {
                dt_start.Format = DateTimePickerFormat.Custom;
                dt_start.CustomFormat = "dd/MM/yyyy";

                dt_end.Format = DateTimePickerFormat.Custom;
                dt_end.CustomFormat = "dd/MM/yyyy";
                lbl_tooltip.SetToolTip(lbl_enabled, "Select No Disable the Selected Discount");
                lbl_tooltip.SetToolTip(lbl_discName, "Enter the Discount Name ");
                loaddefault();
                loaddicount();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public void loaddicount()
        {
            try
            {
                funConnectionStateCheck();
                pnl_discount.Controls.Clear();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_DiscountSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "LOADDISCOUNT");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dtNew.Rows[mn]["DiscountName"].ToString();
                    newBtn.Name = "DiscountName" + i;
                    newBtn.Width = 180;
                    newBtn.Height = 40;
                    newBtn.ForeColor = Color.White;
                    newBtn.Font = new Font(newBtn.Font.FontFamily, 12, newBtn.Font.Style | FontStyle.Regular);
                    newBtn.Location = new System.Drawing.Point(5, i * 45);
                    //newBtn.Top = 60;
                    //newBtn.Left = 100;
                    newBtn.Click += new EventHandler(newBtnBrandItem_Click);
                    pnl_discount.Controls.Add(newBtn);
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void newBtnBrandItem_Click(object sender, EventArgs e)
        {
            try
            {
                Button ClickedButton = (Button)sender;
                txt_DiscountName.Text = ClickedButton.Text.ToString();

                if (txt_DiscountName.Text != "")
                {
                    txt_DiscountName.Text = ClickedButton.Text.ToString();
                    loadnewselectedfrombtn();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            } 
        }
        
        private void lnk_addnew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                addDiscount addfrm = new addDiscount();
                addfrm.discnameEventHandler += new EventHandler(discountNameEvent);
                addfrm.ShowDialog();
                loaddicount();
                loadnewselected();
                // addfrm.Show();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void discountNameEvent(object sender, EventArgs e)
        {
            // load coding of datagridview:
         
           // loaddicount();
           // loadnewselected();

        }
        private void btn_exIt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string selectionupdate;
        string todeleteselect;
        public void loadnewselectedfrombtn()
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_DiscountName.Text))
                {
                    cb_enabled.Text = "";
                    cb_calc.Text = "";
                    cb_sun.Text = "";
                    cb_Mon.Text = "";
                    cb_tue.Text = "";
                    cb_wed.Text = "";
                    cb_thurs.Text = "";
                    cb_fri.Text = "";
                    cb_sat.Text = "";
                    selectionupdate = txt_DiscountName.Text;
                    todeleteselect = txt_DiscountName.Text;
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("sp_DiscountSelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "DISCOUNTNAME");
                    cmd.Parameters.AddWithValue("@tValue", txt_DiscountName.Text.Trim());

                    //select * from DiscountSetting_Table where DiscountName='" + txt_DiscountName.Text + "'", con);      
                    dr = cmd.ExecuteReader();
                    dtNew.Load(dr);
                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        cb_enabled.SelectedText = dtNew.Rows[mn]["Enabled"].ToString();
                        txt_DiscountName.Text = dtNew.Rows[mn]["DiscountName"].ToString();
                        txt_PrntName.Text = dtNew.Rows[mn]["PrintText"].ToString();
                        cb_calc.SelectedText = dtNew.Rows[mn]["Calculation"].ToString();
                        txt_amt.Text = dtNew.Rows[mn]["Amount"].ToString();
                        txt_itemperorder.Text = dtNew.Rows[mn]["ItemsPerOder"].ToString();
                        dt_start.Text = dtNew.Rows[mn]["StartDate"].ToString();
                        dt_end.Text = dtNew.Rows[mn]["EndDate"].ToString();
                        cb_sun.SelectedText = dtNew.Rows[mn]["Sunday"].ToString();
                        cb_Mon.SelectedText = dtNew.Rows[mn]["Monday"].ToString();
                        cb_tue.SelectedText = dtNew.Rows[mn]["Tuesday"].ToString();
                        cb_wed.SelectedText = dtNew.Rows[mn]["Wednessday"].ToString();
                        cb_thurs.SelectedText = dtNew.Rows[mn]["Thursday"].ToString();
                        cb_fri.SelectedText = dtNew.Rows[mn]["Friday"].ToString();
                        cb_sat.SelectedText = dtNew.Rows[mn]["Saturday"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void loadnewselected()
        {
            try
            {
                cb_enabled.Text = "";
                cb_calc.Text = "";
                cb_sun.Text = "";
                cb_Mon.Text = "";
                cb_tue.Text = "";
                cb_wed.Text = "";
                cb_thurs.Text = "";
                cb_fri.Text = "";
                cb_sat.Text = "";

                if (!string.IsNullOrEmpty(chkbox.discountname))
                {
                    // SqlCommand cmd = new SqlCommand(" select * from DiscountSetting_Table where DiscountName='"+chkbox.discountname+"'", con);
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("sp_DiscountSelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "DISCOUNTNAME");
                    cmd.Parameters.AddWithValue("@tValue", chkbox.discountname);
                    dr = cmd.ExecuteReader();
                    dtNew.Load(dr);
                    for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                    {
                        cb_enabled.SelectedText = dtNew.Rows[mn]["Enabled"].ToString();
                        txt_DiscountName.Text = dtNew.Rows[mn]["DiscountName"].ToString();
                        txt_PrntName.Text = dtNew.Rows[mn]["PrintText"].ToString();
                        cb_calc.SelectedText = dtNew.Rows[mn]["Calculation"].ToString();
                        txt_amt.Text = dtNew.Rows[mn]["Amount"].ToString();
                        txt_itemperorder.Text = dtNew.Rows[mn]["ItemsPerOder"].ToString();
                        dt_start.Text = dtNew.Rows[mn]["StartDate"].ToString();
                        dt_end.Text = dtNew.Rows[mn]["EndDate"].ToString();
                        cb_sun.SelectedText = dtNew.Rows[mn]["Sunday"].ToString();
                        cb_Mon.SelectedText = dtNew.Rows[mn]["Monday"].ToString();
                        cb_tue.SelectedText = dtNew.Rows[mn]["Tuesday"].ToString();
                        cb_wed.SelectedText = dtNew.Rows[mn]["Wednessday"].ToString();
                        cb_thurs.SelectedText = dtNew.Rows[mn]["Thursday"].ToString();
                        cb_fri.SelectedText = dtNew.Rows[mn]["Friday"].ToString();
                        cb_sat.SelectedText = dtNew.Rows[mn]["Saturday"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
          
            
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectionupdate != null)
                {

                    funConnectionStateCheck();
                    SqlCommand cmd = new SqlCommand("sp_DiscountSetting_Table", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tEnabled", cb_enabled.Text);
                    cmd.Parameters.AddWithValue("@tDiscountName", txt_DiscountName.Text.Trim());
                    cmd.Parameters.AddWithValue("@tPrintText", txt_DiscountName.Text.ToUpper().Trim());
                    cmd.Parameters.AddWithValue("@tCalculation", cb_calc.Text);
                    cmd.Parameters.AddWithValue("@tAmount", txt_amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@tItemsPerOder", txt_itemperorder.Text);
                    cmd.Parameters.AddWithValue("@tAllowOtherDiscount", cb_allowOdis.Text);
                    cmd.Parameters.AddWithValue("@tStartDate", Convert.ToDateTime(dt_start.Value));
                    cmd.Parameters.AddWithValue("@tEndDate", Convert.ToDateTime(dt_end.Value));
                    cmd.Parameters.AddWithValue("@tSunday", cb_sun.Text);
                    cmd.Parameters.AddWithValue("@tMonday", cb_Mon.Text);
                    cmd.Parameters.AddWithValue("@tTuesday", cb_tue.Text);
                    cmd.Parameters.AddWithValue("@tWednessday", cb_wed.Text);
                    cmd.Parameters.AddWithValue("@tThursday", cb_thurs.Text);
                    cmd.Parameters.AddWithValue("@tFriday", cb_fri.Text);
                    cmd.Parameters.AddWithValue("@tSaturday", cb_sat.Text);
                    cmd.Parameters.AddWithValue("@tDiscountNameCon", selectionupdate);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Updated Successfully");
                    updatedrecselected();
                    loaddicount();

                }
                else
                {
                    MyMessageBox.ShowBox("Inavlid Attempt To Update", "Warinig");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void updatedrecselected()
        {
            try
            {
                cb_enabled.Text = "";
                cb_calc.Text = "";
                cb_sun.Text = "";
                cb_Mon.Text = "";
                cb_tue.Text = "";
                cb_wed.Text = "";
                cb_thurs.Text = "";
                cb_fri.Text = "";
                cb_sat.Text = "";

                //  SqlCommand cmd = new SqlCommand(" select * from DiscountSetting_Table where DiscountName='" + txt_DiscountName.Text + "'", con);

                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_DiscountSelectSingle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "DISCOUNTNAME");
                cmd.Parameters.AddWithValue("@tValue", txt_DiscountName.Text);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    cb_enabled.SelectedText = dtNew.Rows[mn]["Enabled"].ToString();
                    txt_DiscountName.Text = dtNew.Rows[mn]["DiscountName"].ToString();
                    txt_PrntName.Text = dtNew.Rows[mn]["PrintText"].ToString();
                    cb_calc.SelectedText = dtNew.Rows[mn]["Calculation"].ToString();
                    txt_amt.Text = dtNew.Rows[mn]["Amount"].ToString();
                    txt_itemperorder.Text = dtNew.Rows[mn]["ItemsPerOder"].ToString();
                    dt_start.Text = dtNew.Rows[mn]["StartDate"].ToString();
                    dt_end.Text = dtNew.Rows[mn]["EndDate"].ToString();
                    cb_sun.SelectedText = dtNew.Rows[mn]["Sunday"].ToString();
                    cb_Mon.SelectedText = dtNew.Rows[mn]["Monday"].ToString();
                    cb_tue.SelectedText = dtNew.Rows[mn]["Tuesday"].ToString();
                    cb_wed.SelectedText = dtNew.Rows[mn]["Wednessday"].ToString();
                    cb_thurs.SelectedText = dtNew.Rows[mn]["Thursday"].ToString();
                    cb_fri.SelectedText = dtNew.Rows[mn]["Friday"].ToString();
                    cb_sat.SelectedText = dtNew.Rows[mn]["Saturday"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (todeleteselect != null)
                {
                    funConnectionStateCheck();
                    // SqlCommand cmd = new SqlCommand(" delete from DiscountSetting_Table where DiscountName='" + todeleteselect + "'", con);

                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmd = new SqlCommand("sp_DiscountSelectSingle", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tActionType", "DELETE");
                    cmd.Parameters.AddWithValue("@tValue", todeleteselect);
                    cmd.ExecuteNonQuery();


                    txt_DiscountName.Text = "";

                    txt_PrntName.Text = "";
                    cb_calc.Text = "";
                    txt_itemperorder.Text = "";
                    loaddefault();
                    loaddicount();
                    //cb_allowOdis.Text = "";
                    //cb_enabled.Text = "";
                    //cb_fri.Text = "";
                    //cb_Mon.Text = "";
                    //cb_sat.Text = "";
                    //cb_sun.Text = "";
                    //cb_thurs.Text = "";
                    //cb_tue.Text = "";
                    //cb_wed.Text = "";
                }
                else
                {
                    MyMessageBox.ShowBox("Inavlid Attempt To Delete", "Warinig");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void loaddefault()
        {
            try
            {
                cb_enabled.DataSource = eyesno;
                cb_enabled.Text = "Yes";
                cb_calc.DataSource = Calc;
                cb_calc.Text = "Fixed";
                cb_allowOdis.DataSource = allyesno;
                cb_allowOdis.Text = "Yes";
                cb_sun.DataSource = sunyesno;
                cb_sun.Text = "Yes";
                cb_Mon.DataSource = monyesno;
                cb_Mon.Text = "yes";
                cb_tue.DataSource = tueyesno;
                cb_tue.Text = "Yes";
                cb_wed.DataSource = wedyesno;
                cb_wed.Text = "Yes";
                cb_thurs.DataSource = thursyesno;
                cb_thurs.Text = "Yes";
                cb_fri.DataSource = friyesno;
                cb_fri.Text = "Yes";
                cb_sat.DataSource = satyesno;
                cb_sat.Text = "Yes";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void frmDiscount_set_Load(object sender, EventArgs e)
        {
            //For Color settings
            _Class.clsVariables.Sheight_Width();
           
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
           //pnl_inner.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            
        }
    }
}
