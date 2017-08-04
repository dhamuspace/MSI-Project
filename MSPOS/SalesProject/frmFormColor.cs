using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SalesProject
{
    public partial class frmFormColor : Form
    {
        public frmFormColor()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString();
        public void color_check()
        {
           
               // dt2.Columns.Add("Barcode", typeof(string));
                Type colorType = typeof(System.Drawing.Color);
                PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
                foreach (PropertyInfo c in propInfoList)
                {
                    this.cmbBackColor.Items.Add(c.Name);
                    this.cmbUpperColor.Items.Add(c.Name);
                    this.cmbLowerColor.Items.Add(c.Name);
                }
           
        }
        private void cmbBackColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cmbBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbBackColor.Text))
            {
                this.BackColor.Equals(cmbBackColor.Text);
                string color = this.cmbBackColor.SelectedItem.ToString();
                this.BackColor = Color.FromName(color);
            }
        }

        private void cmbBackColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                cmbBackColor.Focus();
            }
        }
        private void cmbUpperColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cmbUpperColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = this.cmbUpperColor.SelectedItem.ToString();
            this.Pnl_Header.BackColor = Color.FromName(color);           
        }

        private void cmbUpperColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                //cmbBackColor.Focus();
                cmbLowerColor.Focus();
            }            
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFormColor_Load(object sender, EventArgs e)
        {
            float height = SystemInformation.VirtualScreen.Height;
            float width = SystemInformation.VirtualScreen.Width;
            double diagonal = Math.Sqrt(width * width + height * height);  
            color_check();
            string Sheigh = this.Height.ToString();
            label1.Visible = false;
            label5.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            lblHeight.Visible = false;
            lblwidth.Visible = false;

            //For Color settings
            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            // Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
        }
        private void cmbLowerColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cmbLowerColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = this.cmbLowerColor.SelectedItem.ToString();
            this.Pnl_Footer.BackColor = Color.FromName(color); 
        }


        SqlCommand cmd = null;
          string totalCounts = "", Update = "";
          int Sheight = 0, SWidth = 0;
        private void btn_save_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
               string update = "select Count(*) As Count from  Colour_table";
                using (cmd = new SqlCommand(update, con))
                {
                    totalCounts = Convert.ToString((int)cmd.ExecuteScalar());
                }
                if (!string.IsNullOrEmpty(totalCounts.ToString()) && Convert.ToDouble(totalCounts.ToString()) > 0)
                {
                  string Update1 = string.Empty;
                    Update1 = "Update Colour_table Set Colour_no=@UpBgColor,colour_name=@BgColor,Colour_mtname=@BtBgColor";
                    using (cmd = new SqlCommand(Update1, con))
                    {
                        cmd.Parameters.AddWithValue("@UpBgColor", string.IsNullOrEmpty(cmbUpperColor.Text.ToString()) ? "Olive" : cmbUpperColor.Text.ToString());
                        cmd.Parameters.AddWithValue("@BgColor", string.IsNullOrEmpty(cmbBackColor.Text.ToString()) ? "InactiveCaptionText" : cmbBackColor.Text.ToString());
                        cmd.Parameters.AddWithValue("@BtBgColor", string.IsNullOrEmpty(cmbLowerColor.Text.ToString()) ? "Olive" : cmbLowerColor.Text.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string Insertnew = "insert into Colour_table(Colour_no,colour_name,Colour_mtname)values(@UpBgColor,@BgColor,@BtBgColor)";
                    using (cmd = new SqlCommand(Insertnew, con))
                    {
                        cmd.Parameters.AddWithValue("@UpBgColor", string.IsNullOrEmpty(cmbUpperColor.Text.ToString()) ? "Olive" : cmbUpperColor.Text.ToString());
                        cmd.Parameters.AddWithValue("@BgColor", string.IsNullOrEmpty(cmbBackColor.Text.ToString()) ? "InactiveCaptionText" : cmbBackColor.Text.ToString());
                        cmd.Parameters.AddWithValue("@BtBgColor", string.IsNullOrEmpty(cmbLowerColor.Text.ToString()) ? "Olive" : cmbLowerColor.Text.ToString());
                        //cmd.Parameters.AddWithValue("@SWidht", string.IsNullOrEmpty(cmbLowerColor.SelectedItem.ToString()) ? "Olive" : cmbLowerColor.SelectedItem.ToString());
                        //cmd.Parameters.AddWithValue("@SHeight", string.IsNullOrEmpty(cmbLowerColor.SelectedItem.ToString()) ? "Olive" : cmbLowerColor.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }
                   
                }
                con.Close();
            }
        }
        private void frmFormColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Right)
            {
              //  Sheight = this.Height;
                SWidth=this.Width;
                this.Width = Width + 1;

                lblwidth.Text = this.Width.ToString();
            }
            if (e.Shift && e.KeyCode == Keys.Left)
            {
                //  Sheight = this.Height;
                SWidth = this.Width;
                this.Width = Width - 1;
                lblwidth.Text = this.Width.ToString();
            }
            if (e.Shift && e.KeyCode == Keys.Down)
            {
                //Sheight = this.Height;
                Sheight = this.Height;
                this.Height = Sheight + 1;
                lblHeight.Text = this.Height.ToString();
            }
            if (e.Shift && e.KeyCode == Keys.Up)
            {
                //Sheight = this.Height;
                Sheight = this.Height;
                this.Height = Sheight - 1;
                lblHeight.Text = this.Height.ToString();
            }
        }
    }
}
