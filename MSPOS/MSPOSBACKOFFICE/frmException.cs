using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MSPOSBACKOFFICE
{
    public partial class frmException : Form
    {

        static frmException newMessageBox;
        public Timer msgTimer;
        static string Button_id;
        int disposeFormTimer; 

        public frmException()
        {
            InitializeComponent();
        }
        public static string ShowBox(string txtMessage, string txtLno, string txtfilename)
        {
            newMessageBox = new frmException();
            newMessageBox.lblDetail.Text = txtMessage;
            newMessageBox.lblMsgFormName.Text = txtfilename; 
            newMessageBox.lblMsgLineNo.Text = txtLno;
            newMessageBox.ShowDialog();
            return Button_id;
        }
        public static string ShowBox(string txtMessage, string txtTitle, string txtLno, string txtfilename)
        {
            newMessageBox = new frmException();
            newMessageBox.lblTittle.Text = txtTitle;
            newMessageBox.lblMsgFormName.Text = txtfilename;         
            newMessageBox.lblMsgDetail.Text = txtMessage;
            newMessageBox.lblMsgLineNo.Text =Convert.ToString(txtLno);
           
            newMessageBox.ShowDialog();
            return Button_id;
        }

        private void frmException_Load(object sender, EventArgs e)
        {
            disposeFormTimer = 2;
            newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            msgTimer = new Timer();
            msgTimer.Interval = 2000;
            msgTimer.Enabled = true;
            msgTimer.Start();
            msgTimer.Tick += new System.EventHandler(this.timer_tick); 
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
                Button_id = "1";
            }
        }
        private void frmException_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 56, 96), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            newMessageBox.msgTimer.Stop();
            newMessageBox.msgTimer.Dispose();
            Button_id = "1";
            newMessageBox.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            newMessageBox.msgTimer.Stop();
            newMessageBox.msgTimer.Dispose();
            Button_id = "2";
            newMessageBox.Dispose();
        }
        
    }
}
