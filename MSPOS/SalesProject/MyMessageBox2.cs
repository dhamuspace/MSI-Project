using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace SalesProject
{
    public partial class MyMessageBox2 : Form
    {
        static MyMessageBox2 newMessageBox;
        public Timer msgTimer;
        static string Button_id;
        int disposeFormTimer; 
        public MyMessageBox2()
        {
            InitializeComponent();
        }
        public static string ShowBox(string txtMessage, string txtMessage1, string txtMessage2)
        {
            newMessageBox = new MyMessageBox2();
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblMessage1.Text = txtMessage1;
            newMessageBox.lblMessage2.Text = txtMessage2;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        public static string ShowBox(string txtMessage,string txtMessage1,string txtMessage2, string txtTitle)
        {
            newMessageBox = new MyMessageBox2();
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblMessage.Text = txtMessage;
            newMessageBox.lblMessage1.Text = txtMessage1;
            newMessageBox.lblMessage2.Text = txtMessage2;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            //disposeFormTimer = 4;
            //newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            //msgTimer = new Timer();
            //msgTimer.Interval = 1000;
            //msgTimer.Enabled = true;
            //msgTimer.Start();
            //msgTimer.Tick += new System.EventHandler(this.timer_tick);
            btnOK.Focus();
        }

        private void MyMessageBox2_Paint(object sender, PaintEventArgs e)
        {
            //Graphics mGraphics = e.Graphics;
            //Pen pen1 = new Pen(Color.FromArgb(96, 155, 173), 1);

            //Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 56, 96), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            //mGraphics.FillRectangle(LGB, Area1);
            //mGraphics.DrawRectangle(pen1, Area1);

            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(39, 64, 139), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(0, 100, 10), Color.FromArgb(152, 251, 152), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(LGB, Area1);
            mGraphics.DrawRectangle(pen1, Area1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //newMessageBox.msgTimer.Stop();
            //newMessageBox.msgTimer.Dispose();
            Button_id = "1";
            newMessageBox.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //newMessageBox.msgTimer.Stop();
            //newMessageBox.msgTimer.Dispose();
            Button_id = "2";
            newMessageBox.Dispose();
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

        private void lblMessage_Click(object sender, EventArgs e)
        {

        }

       
    }
}
