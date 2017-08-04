using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace MSPOSBACKOFFICE._ExtraForm
{
    public partial class frmQtyaNRate : Form
    {
        public frmQtyaNRate()
        {
            InitializeComponent();
        }
        public string getValueType;

       SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        static frmQtyaNRate newMessageBox;
        public Timer msgTimer;
        static string Button_id;
        int disposeFormTimer;
        private void btnOK_Click(object sender, EventArgs e)
        
        {
            bool isChk = false;
            
            if (txtRate.Enabled == true)
            {
                isChk = true;
            }
            if (txtRate.Text.Trim() == "" && isChk == true)
            {
                txtRate.Select();
            }           
            else
            {
                if (double.Parse(txtRate.Text.Trim()) == 0 && isChk==true)
                {
                    txtRate.Select();                  
                }
                else
                {
                    
                    funQtyInsert();
                }
            }
        }

        public void funQtyInsert()
        {
            
            double tOldQty = 0, tNewQty = 0, tOldRate = 0, tNewRate = 0;
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd1 = new SqlCommand("sp_SalesCreationSelectSingle", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@tValue", _Class.clsVariables.itemName);
            cmd1.Parameters.AddWithValue("@tActionType", "ITEMNAMEWITHUNIT");
            SqlDataAdapter adpCmd1 = new SqlDataAdapter(cmd1);
            adpCmd1.Fill(dtNew);
            // reader = cmd1.ExecuteReader();
            // dtNew.Load(reader);
            if (dtNew.Rows.Count > 0)
            {
                double tUnitDecimals = double.Parse(dtNew.Rows[0]["unit_Decimals"].ToString());

                if (MSPOSBACKOFFICE._Class.clsVariables.itemQty != "")
                {
                    tOldQty = double.Parse(MSPOSBACKOFFICE._Class.clsVariables.itemQty);
                }
                if (txtQty.Text.Trim() != "")
                {
                    tNewQty =(txtQty.Text.Trim()==".")?0:double.Parse(txtQty.Text.Trim());
                }
                if (tUnitDecimals == 0)
                {
                    MSPOSBACKOFFICE._Class.clsVariables.itemQty = (tOldQty + tNewQty).ToString("N0");
                }
                if (tUnitDecimals == 1)
                {
                    MSPOSBACKOFFICE._Class.clsVariables.itemQty = (tOldQty + tNewQty).ToString("N1");
                }
                if (tUnitDecimals == 2)
                {
                    MSPOSBACKOFFICE._Class.clsVariables.itemQty = (tOldQty + tNewQty).ToString("N2");
                }
                if (tUnitDecimals == 3)
                {
                    MSPOSBACKOFFICE._Class.clsVariables.itemQty = (tOldQty + tNewQty).ToString("N3");
                }
                if (tUnitDecimals == 4)
                {
                    MSPOSBACKOFFICE._Class.clsVariables.itemQty = (tOldQty + tNewQty).ToString("N4");
                }
                if (MSPOSBACKOFFICE._Class.clsVariables.itemRate != "")
                {
                    tOldRate = double.Parse(MSPOSBACKOFFICE._Class.clsVariables.itemRate);
                }
                if (txtRate.Text.Trim() != "")
                {
                    tNewRate =(txtRate.Text.Trim()==".")?0:double.Parse(txtRate.Text.Trim());
                }
                MSPOSBACKOFFICE._Class.clsVariables.itemRate = (tNewRate).ToString();
            }
            this.Close();
        }
        string tempStopAtRate, tempStopAtQty;
        private void frmQtyaNRate_Load(object sender, EventArgs e)
        {
            tempStopAtRate = _Class.clsVariables.StopAtRate;
            tempStopAtQty = _Class.clsVariables.StopAtQty;
            if (tempStopAtQty == "1")
            {
                tempStopAtQty = "True";
            }
            if (tempStopAtRate == "1")
            {
                tempStopAtRate = "True";
            }

            if (tempStopAtRate != "True")
            {
                txtRate.Text = _Class.clsVariables.itemRate;
                txtRate.Enabled = false;
            }
            else
            {
                txtRate.Text = _Class.clsVariables.itemRate;
                txtRate.Enabled = true;
                txtRate.Select();
            }
           // txtQty.Select();
           
            if (tempStopAtQty != "True")
            {
               // txtQty.Text = _Class.clsVariables.itemQty;
                txtQty.Enabled = false;
            }
            else
            {
              //  txtQty.Text = _Class.clsVariables.itemQty;
                txtQty.Enabled =true;
                txtQty.Select();
            }
            if (getValueType == "Qty")
            {
                txtQty.Select();
            }
            else
            {
                txtRate.Select();
            }
            //disposeFormTimer = 10;
            //newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            //msgTimer = new Timer();
            //msgTimer.Interval = 1000;
            //msgTimer.Enabled = true;
            //msgTimer.Start();
            //msgTimer.Tick += new System.EventHandler(this.timer_tick);
        }

        private void frmQtyaNRate_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(196, 155, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 56, 96), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            disposeFormTimer--;

            if (disposeFormTimer >= 0)
            {
                newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            }
            else
            {
                newMessageBox.msgTimer.Stop();
                newMessageBox.msgTimer.Dispose();
                newMessageBox.Dispose();
                Button_id = "2";

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            string temp;            
            Button btn = (Button)sender;          

            if (focusedBtn =="Qty")
            {
                if (txtQty.Text != "")
                {
                    temp = txtQty.Text;
                    txtQty.Text = "";
                    txtQty.Text = temp + btn.Text.ToString();
                }
                if (txtQty.Text == "")
                {
                    txtQty.Text = btn.Text.ToString();
                }
                txtQty.Select(txtQty.Text.Length, 0);
            }
            else if (focusedBtn=="Rate")
            {
                if (txtRate.Text != "")
                {
                    temp = txtRate.Text;
                    if (_Class.clsVariables.itemRate != txtRate.Text.Trim())
                    {
                        txtRate.Text = "";
                        txtRate.Text = temp + btn.Text.ToString();
                    }
                    else
                    {
                        txtRate.Text = "";
                        txtRate.Text = btn.Text.ToString();
                    }
                }
                if (txtRate.Text == "")
                {
                   txtRate.Text = btn.Text.ToString();
                }
               txtRate.Select(txtRate.Text.Length, 0);
            }
        }

        private void frmQtyaNRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnOK_Click(sender, e);
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if ( focusedBtn=="Rate")
            {
               txtRate.Clear();
            }
            if (focusedBtn =="Qty")
            {
                txtQty.Clear();
            }
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            string temp;
            if (focusedBtn == "Rate")
            {
                if (txtRate.Text.Length > 0)
                {
                    temp = txtRate.Text;
                    txtRate.Text = temp.Remove(temp.Length - 1);
                }
            }
            if (focusedBtn =="Qty")
            {
                if (txtQty.Text.Length > 0)
                {
                    temp = txtQty.Text;
                    txtQty.Text = temp.Remove(temp.Length - 1);
                }
            }
         
        }
        public string focusedBtn;
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            focusedBtn = "Qty";
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            focusedBtn = "Rate";
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
           focusedBtn = "Qty";
        }

        private void txtRate_Enter(object sender, EventArgs e)
        {
            focusedBtn = "Rate";
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtRate.Enabled == true)
                {
                    txtRate.Select();
                }
                else
                {
                    btnOK.Select();
                }
            }

        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
               btnOK.Select();                
            }
        }

       
      
    }
}
