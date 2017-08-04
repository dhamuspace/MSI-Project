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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for UCCustomerList.xaml
    /// </summary>
    public partial class UCCustomerList : UserControl
    {
        public UCCustomerList()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        SqlDataReader dr = null;
        string temp;
        public event System.EventHandler SalesCreationEventHandlerNewCustomerName;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tHouseACAmt);
                funConnectionStateCheck();
                txtCustomerName.Text = "";
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select distinct(SUBSTRING(LTrim(UPPER(Ledger_name)),0,2)) as Card_Name from Ledger_table where Ledger_groupno=32 and Ledger_no<>2", con);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                string tLoadLetter = "";
                pnlCustomerNameList.Children.Clear();
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                    tLoadLetter = dtNew.Rows[mn]["Card_Name"].ToString();
                    newBtn.FontSize = 30;                 
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;                   
                    newBtn.Name = "HACL" + i;                    
                    newBtn.Width = 90;
                    newBtn.Height = 65;
                    newBtn.Foreground = Brushes.White;
                    newBtn.FontSize = 18;
                    newBtn.FontWeight = FontWeights.Black;
                    newBtn.BorderBrush = Brushes.White;
                    newBtn.BorderThickness = new Thickness(1.8);
                    //newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#008080"));
                    newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#551A8B"));
                    newBtn.Margin = new Thickness(2, 1, 1, 2);          
                    newBtn.Click += new RoutedEventHandler(newBtnGroupChar_Click);                    
                    pnlCustomerNameList.Children.Add(newBtn);
                    pnlCustomerNameList.Height = (i * 65) + 50;

                }
                if (tLoadLetter != "")
                {
                    funlLoadListAll();
                }
                con.Close();               
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            } 
        }
        private void newBtnGroupChar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Getting Click Button Name values here:
                Button clickedButton = (Button)sender;
                funlLoadList(clickedButton.Content.ToString());
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funlLoadListAll()
        {
            try
            {
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Customer_Name from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 order by Ledger_name ASC", con);
                //  cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlCustomerList.Children.Clear();
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Customer_Name"].ToString();
                    newBtn.FontSize = 16;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    newBtn.Name = "HouseAC" + i;
                    newBtn.Width = 285;
                    newBtn.Height = 65;
                    newBtn.Foreground = Brushes.White;
                    newBtn.FontSize = 18;
                    newBtn.FontWeight = FontWeights.Black;
                    newBtn.BorderBrush = Brushes.White;
                    newBtn.BorderThickness = new Thickness(1.8);
                    //newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#008080"));
                    newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#551A8B"));
                    newBtn.Margin = new Thickness(2, 1, 1, 2);          
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    pnlCustomerList.Children.Add(newBtn);
                    pnlCustomerList.Height = (i * 65) + 50;

                }
                con.Close();                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void newBtnGroup_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;            
            txtCustomerName.Text = clickedButton.Content.ToString();            
        }
        public void funlLoadList(string tStartLetter)
        {
            try
            {
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 and Ledger_name like @tStart order by Ledger_name ASC", con);
                cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlCustomerList.Children.Clear();
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                    newBtn.FontSize = 16;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    newBtn.Name = "HouseAC" + i;
                    newBtn.Width = 285;
                    newBtn.Height = 65;
                    newBtn.Foreground = Brushes.White;
                    newBtn.FontSize = 18;
                    newBtn.FontWeight = FontWeights.Black;
                    newBtn.BorderBrush = Brushes.White;
                    newBtn.BorderThickness = new Thickness(1.8);
                    //newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#008080"));
                    newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#551A8B"));
                    newBtn.Margin = new Thickness(2, 1, 1, 2);          
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    pnlCustomerList.Children.Add(newBtn);
                    pnlCustomerList.Height = (i * 65) + 50;

                }
                con.Close();                
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funConnectionStateCheck()
        {
            try
            {
                con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            txtCustomerName.Text = "";
        }

        private void clickSVup(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtn.PageUp();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void clickSVdn(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtn.PageDown();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void clickSVup1(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtn1.PageUp();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void clickSVdn1(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtn1.PageDown();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string strLedgerno = "";
        
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtCustomerName.Text != "" || txtCustomerName.Text != null)
            {
                SqlCommand cmdSales = new SqlCommand("select Ledger_no from Ledger_table where Ledger_name='" + txtCustomerName.Text.Trim() + "'", con);
                DataTable dt = new DataTable();
                dt.Rows.Clear();
                SqlDataAdapter adp = new SqlDataAdapter(cmdSales);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    strLedgerno = dt.Rows[0]["Ledger_no"].ToString();
                }                
                _Class.clsVariables.tempCustomerLedgerNo = strLedgerno;

                SalesCreationEventHandlerNewCustomerName(sender, e);
                this.Visibility = Visibility.Hidden;
                txtCustomerName.Text = "";

            }
            else
            {
                MyMessageBox.ShowBox("Select Customer Name","Warning");
                txtCustomerName.Focus();
            }
        }       
    }
}
