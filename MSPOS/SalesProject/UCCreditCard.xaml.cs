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
    /// Interaction logic for FrmDiscount.xaml
    /// </summary>
    /// 
    public delegate void UCCreditCardEvent(object sender, RoutedEventArgs e);
    
    public partial class UCCreditCard : UserControl
    {
        public UCCreditCard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        SqlDataReader dr = null;
        string temp;
        public event UCCreditCardEvent UCCreditCardEventCardClick;
        public event UCCreditCardEvent UCCreditCardEventCancelClick;
        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtEnterValue.Focus();
                Button btn = (Button)sender;
                if (txtEnterValue.Text != "")
                {
                    temp = txtEnterValue.Text;
                    if (btn.Content.ToString().Trim() != ".")
                    {
                        txtEnterValue.Text = "";
                        txtEnterValue.Text = temp + btn.Content.ToString();
                    }
                    else
                    {
                        if (temp.IndexOf('.') == -1)
                        {
                            txtEnterValue.Text = "";
                            txtEnterValue.Text = temp + btn.Content.ToString();
                        }
                    }
                }
                if (txtEnterValue.Text == "")
                {
                    txtEnterValue.Text = btn.Content.ToString();
                }
                // _Class.clsVariables.DiscountType = "Amount";
                txtEnterValue.Select(txtEnterValue.Text.Length, 0);
            }
            catch (Exception ex)
            { }
        }
        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              //  con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                txtEnterValue.Text = string.Empty;
                this.Visibility = Visibility.Hidden;
                SalesProject._Class.clsVariables.tCreditCardName = "";
                if (UCCreditCardEventCancelClick != null)
                {
                    UCCreditCardEventCancelClick(sender, e);
                }
            }
            catch(Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEnterValue.Text = string.Empty;
        }   
        public void funConnectionStateCheck()
        {
            try
            {
              //  con.Close();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            { }
        }
        public void CreditCardLoad()
        {
            try
            {
                txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tCreditCardAmt);
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                pnlCreditCardList.UCPnlItemDisplay.Children.Clear();

                SqlCommand cmd = new SqlCommand("select Ledger_Name as Card_Name from Ledger_table where Ledger_groupno=5 and Ledger_no<>14", con);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                    newBtn.FontSize = 30;

                    //TextBlock textBlock = new TextBlock();
                    //textBlock.Inlines.Add(dtNew.Rows[mn]["Card_Name"].ToString());
                    //txtCardName = "";
                    //txtCardName = dtNew.Rows[mn]["Card_Name"].ToString();
                    //textBlock.Inlines.Add(new LineBreak());
                    //textBlock.FontSize = 40;
                    ////newBtn.Content = textBlock;
                    //  newBtn.Content = txtCardName;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    newBtn.Background = Brushes.Transparent;
                    //newBtn.Content = dr["DiscountName"].ToString();
                    newBtn.Name = "Discount" + i;
                    //newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                    newBtn.Width = 160;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    // newBtn.Style = this.Resources["btnGroup"] as Style;
                    //Every Button Name Calles As newBtnGroup_Click
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    //newBtn.Style = "btnnoborder";
                    //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                    pnlCreditCardList.UCPnlItemDisplay.Children.Add(newBtn);
                   // pnlCreditCardList.Height = (i * 65) + 50;
                }
              //  con.Close();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
 
        }
        public void funCreditCardLoad()
        {
            try
            {
                txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tCreditCardAmt);
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_Name as Card_Name from Ledger_table where Ledger_groupno=5 and Ledger_no<>14", con);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlCreditCardList.UCPnlItemDisplay.Children.Clear();
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                    newBtn.FontSize = 30;

                    //TextBlock textBlock = new TextBlock();
                    //textBlock.Inlines.Add(dtNew.Rows[mn]["Card_Name"].ToString());
                    //txtCardName = "";
                    //txtCardName = dtNew.Rows[mn]["Card_Name"].ToString();
                    //textBlock.Inlines.Add(new LineBreak());
                    //textBlock.FontSize = 40;
                    ////newBtn.Content = textBlock;
                    //  newBtn.Content = txtCardName;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    //newBtn.Content = dr["DiscountName"].ToString();
                    newBtn.Name = "Discount" + i;
                    newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF004040"));
                    newBtn.Foreground = Brushes.White;
                    newBtn.BorderBrush = Brushes.White;
                    //newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                    newBtn.Width = 162;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    // newBtn.Style = this.Resources["btnGroup"] as Style;
                    //Every Button Name Calles As newBtnGroup_Click
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    //newBtn.Style = "btnnoborder";
                    //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                    pnlCreditCardList.UCPnlItemDisplay.Children.Add(newBtn);
                  //  pnlCreditCardList.Height = (i * 65) + 50;

                }
               // con.Close();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public string txtCardName;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtEnterValue.Text =string.Format("{0:0.00}",SalesProject._Class.clsVariables.tCreditCardAmt);
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_Name as Card_Name from Ledger_table where Ledger_groupno=5 and Ledger_no<>14", con);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                    newBtn.FontSize = 30;
                    
                    //TextBlock textBlock = new TextBlock();
                    //textBlock.Inlines.Add(dtNew.Rows[mn]["Card_Name"].ToString());
                    //txtCardName = "";
                    //txtCardName = dtNew.Rows[mn]["Card_Name"].ToString();
                    //textBlock.Inlines.Add(new LineBreak());
                    //textBlock.FontSize = 40;
                    ////newBtn.Content = textBlock;
                  //  newBtn.Content = txtCardName;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    //newBtn.Content = dr["DiscountName"].ToString();
                    newBtn.Name = "Discount" + i;
                    //newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                    newBtn.Width = 150;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                   // newBtn.Style = this.Resources["btnGroup"] as Style;
                    //Every Button Name Calles As newBtnGroup_Click
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    //newBtn.Style = "btnnoborder";
                    //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                    pnlCreditCardList.UCPnlItemDisplay.Children.Add(newBtn);
                    pnlCreditCardList.Height = (i * 65) + 50;
                  
                }
                con.Close();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
              SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void newBtnGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEnterValue.Text.Trim() != "")
                {
                    //con.Close();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //Getting Click Button Name values here:
                    Button clickedButton = (Button)sender;
                    SalesProject._Class.clsVariables.tCreditCardName = "";
                    SalesProject._Class.clsVariables.tCreditCardName = clickedButton.Content.ToString();
                    SalesProject._Class.clsVariables.tCreditCardAmt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
                   // this.Close();
                    if (UCCreditCardEventCardClick != null)
                    {
                        UCCreditCardEventCardClick(sender, e);
                    }
                }
                else
                {
                   SalesProject. MyMessageBox.ShowBox("You should enter settle amount","Warning");
                    txtEnterValue.Focus();
                }
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void clickSVup(object sender, RoutedEventArgs e)
        {
            try
            {
             //   svBtn.PageUp();
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void clickSVdn(object sender, RoutedEventArgs e)
        {
            try
            {
              //  svBtn.PageDown();
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }   
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtEnterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;     
        } 
    }
}
