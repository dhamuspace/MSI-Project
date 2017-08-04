using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for frmCashDrawPassword.xaml
    /// </summary>
     public delegate void UCPasswordEvent();
    public partial class frmCashDrawPassword : UserControl
    {
        public frmCashDrawPassword()
        {
            InitializeComponent();
        }
        public event UCPasswordEvent UCPasswordKeyClick;
        public event System.EventHandler SalesCreationEventHandlerPasswordClose;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            //SalesCreationEventHandlerPasswordClose(sender, e);
            txtPasswordCash.Password = "";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //lblText.Content = "Welecome"; 
            txtPasswordCash.Password = "";
            txtPasswordCash.Focus();
        }

        private void BtnKey_Click(object sender, RoutedEventArgs e)
        {
            //UCKeyBoradAct frm = new UCKeyBoradAct();
            //frm.SalesCreationEventHandlerNew += new EventHandler(ClosecashPassword);
            //frm.Visibility = Visibility.Visible;
            //txtPasswordCash.Focus();   
   
            //_Class.clsVariables.tVoidActionType = "KeyPassword";
            ////if (UCPasswordKeyClick != null)
            ////{
            ////    UCPasswordKeyClick();
            ////}
            //frmKeyBoard frm = new frmKeyBoard();
            //frm.SalesCreationEventHandlerNew += new EventHandler(ClosecashPassword);
            //frm.ShowDialog();
            //txtPasswordCash.Focus();
            
        }

        public void ClosecashPassword(object sender, EventArgs e)
        {
            txtPasswordCash.Password= _Class.clsVariables.tVoidValue;           
            txtPasswordCash.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtPassword = new DataTable();
            dtPassword.Clear();
            SqlCommand cmdPassword = new SqlCommand("select User_Pass from User_table where User_type='0' and user_no='1'", con);
            SqlDataAdapter adpPassword = new SqlDataAdapter(cmdPassword);
            adpPassword.Fill(dtPassword);
            string strPassword = "";

            if (dtPassword.Rows.Count > 0)
            {
                strPassword = dtPassword.Rows[0]["User_Pass"].ToString();
            }
            if (txtPasswordCash.Password == strPassword)
            {
                SalesCreationEventHandlerPasswordClose(sender, e);
                this.Visibility = Visibility.Hidden;
                txtPasswordCash.Password = "";
            }
            else
            {
                MyMessageBox1.ShowBox("Wrong Password!", "Warning");
                txtPasswordCash.Password = "";
                txtPasswordCash.Focus();
            }
        }

        private void txtPasswordCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOk.Focus();
            }
        }
        string temp = null;
        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtPasswordCash.Focus();
                System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
                if (txtPasswordCash.Password != "")
                {
                    temp = txtPasswordCash.Password;
                    txtPasswordCash.Password = "";
                    txtPasswordCash.Password = temp + btn.Content.ToString();
                }
                if (txtPasswordCash.Password == "")
                {
                    txtPasswordCash.Password = btn.Content.ToString();
                }

            }
            //txtEnterValue.Select(txtEnterValue.Password.Length, 0);
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}
