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
    public partial class CreditCard : Window
    {
        public CreditCard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        SqlDataReader dr = null;
        string temp;
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
                this.Close();
                SalesProject._Class.clsVariables.tCreditCardName = "";
            }
            catch(Exception ex)
            { }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEnterValue.Text = string.Empty;
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
            { }
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
                    pnlCreditCardList.Children.Add(newBtn);
                    pnlCreditCardList.Height = (i * 65) + 50;
                  
                }
                con.Close();
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void newBtnGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEnterValue.Text.Trim() != "")
                {
                    //Getting Click Button Name values here:
                    Button clickedButton = (Button)sender;
                    SalesProject._Class.clsVariables.tCreditCardName = "";
                    SalesProject._Class.clsVariables.tCreditCardName = clickedButton.Content.ToString();
                    SalesProject._Class.clsVariables.tCreditCardAmt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
                    this.Close();
                }
                else
                {
                    MyMessageBox.ShowBox("You should enter settle amount","Warning");
                    txtEnterValue.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
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

        private void txtEnterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        } 
    }
}
