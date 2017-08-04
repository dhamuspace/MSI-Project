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
//using System.Windows.Forms;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for UCTableCreate.xaml
    /// </summary>
    /// 

    public delegate void UCPriceChargeEvents();
    public partial class UCPriceChange : UserControl   
    {
        public UCPriceChange()
        {
            InitializeComponent();
            txtValue.Text = string.Empty;
            txtValue.Focus();
        }
        public event UCPriceChargeEvents OnCancelClick;
        public event UCPriceChargeEvents OnNextClick;
        public string lblUserCtlTitleMain
        {
            get
            {
                return Convert.ToString(lblUserCtlTitle.Content);
            }
            set
            {
                lblUserCtlTitle.Content = value;

            }
        }
        private string tUserCtlStatus = "";
        public string tUserCtlStatusMain
        {
            get
            {
                return Convert.ToString(tUserCtlStatus);
            }
            set
            {
                tUserCtlStatus = value;

            }

        }

        private string tUserCtlStatusValue = "";
        public string tUserCtlStatusValueMain
        {
            get
            {
                return Convert.ToString(tUserCtlStatusValue);
            }
            set
            {
                tUserCtlStatusValue = value;
            }

        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtValue.Text = string.Empty;
                orginalvalues = string.Empty;
                txtValue.Focus();
               // pnlErrorDisplay.Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                //NewWPFproject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnZero_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtValue.Text))
                {
                   // pnlErrorDisplay.Visibility = Visibility.Hidden;
                    Button btn = (Button)sender;
                    string tPreviosValue = txtValue.Text.Trim();
                    txtValue.Text = tPreviosValue + btn.Content;
                    txtValue.Select(txtValue.Text.Length, 0);
                }
                else
                {
                  //  pnlErrorDisplay.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                SalesProject.MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tUserCtlStatus = "Cancel";
                this.Visibility = Visibility.Hidden;
                if (OnCancelClick != null)
                {
                    OnCancelClick();
                }
                
            }
            catch (Exception ex)
            {
               SalesProject. MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((string.IsNullOrEmpty(Convert.ToString(txtValue.Text)) == true ? 0 : Convert.ToDouble(Convert.ToString(txtValue.Text).Trim())) > -1)
                {
                   // pnlErrorDisplay.Visibility = Visibility.Hidden;
                    tUserCtlStatus = "Next";
                    tUserCtlStatusValue = txtValue.Text.Trim();
                    _Class.clsVariables.itemRate = txtValue.Text.Trim();
                   // ClsFile.ClsRestaurant.tGuest = string.IsNullOrEmpty(txtValue.Text.Trim()) ? "1" : txtValue.Text;
                    this.Visibility = Visibility.Hidden;
                    if (OnNextClick != null)
                    {
                        OnNextClick();
                    }
                }
                else
                {
                   // pnlErrorDisplay.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
              SalesProject. MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        double strTemp;
        private void txtValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           // e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);        

        }
        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return false;
        }
        private bool IsDelOrBackspaceOrTabKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab;
        }
        public string orginalvalues="";
        private void btnNine_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string tPreviosValue = txtValue.Text.Trim();
            txtValue.Text = tPreviosValue + btn.Content;
            txtValue.Select(txtValue.Text.Length, 0);
            string fristvalues = "", secondValues = "";
            orginalvalues += btn.Content;

            txtValue.Text = "";

            char[] chararrinput = orginalvalues.ToCharArray();

            if (chararrinput.Length == 2)
            {

                txtValue.Text = "00." + orginalvalues.ToString();
            }

            else if (chararrinput.Length == 3)
            {
                fristvalues = orginalvalues.Substring(0, orginalvalues.Length - 2);
                secondValues = orginalvalues.Substring(orginalvalues.Length - 2, 2);
                txtValue.Text = "0" + fristvalues + "." + secondValues.ToString();
            }
            else if (chararrinput.Length == 4)
            {

                fristvalues = orginalvalues.Substring(0, orginalvalues.Length - 2);
                int count = Convert.ToInt32(orginalvalues.Length);
                secondValues = orginalvalues.Substring(orginalvalues.Length - 2, 2);
                txtValue.Text = (fristvalues + "." + secondValues).ToString();
            }
            else if (chararrinput.Length >= 5)
            {

                fristvalues = orginalvalues.Substring(0, orginalvalues.Length - 2);
                int count = Convert.ToInt32(orginalvalues.Length);
                secondValues = orginalvalues.Substring(orginalvalues.Length - 2, 2);
                txtValue.Text = (fristvalues + "." + secondValues).ToString();
            }
            else
            {
                txtValue.Text = "00.0" + orginalvalues.ToString();
            }            
        }
       
        private void btnZero_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.Key == Key.NumPad9)
            {
                strTemp = 9;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad8)
            {                
                strTemp = 8;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad7)
            {               
                strTemp = 7;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad6)
            {               
                strTemp = 6;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad5)
            {                
                strTemp = 5;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad4)
            {               
                strTemp = 4;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad3)
            {               
                strTemp = 3;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad2)
            {                
                strTemp = 2;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad1)
            {                
                strTemp = 1;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad0)
            {                
                strTemp = 0;
                funKeyDownpress();
            }
               
        }
        void funKeyDownpress()
        {
             
                string tPreviosValue = txtValue.Text.Trim();
                txtValue.Text = tPreviosValue + strTemp;
                txtValue.Select(txtValue.Text.Length, 0);
                string fristvalues = "", secondValues = "";
                orginalvalues += strTemp;

                txtValue.Text = "";
                
                char[] chararrinput = orginalvalues.ToCharArray();

                if (chararrinput.Length == 2)
                 {
                    
                     txtValue.Text = "00." + orginalvalues.ToString();
                 }

                else if (chararrinput.Length==3)
                 {                     
                     fristvalues = orginalvalues.Substring(0, orginalvalues.Length - 2);
                     secondValues = orginalvalues.Substring(orginalvalues.Length-2 , 2);
                     txtValue.Text = "0"+fristvalues + "."+secondValues.ToString();
                 }
                else if (chararrinput.Length == 4)
                {
                  
                    fristvalues = orginalvalues.Substring(0, orginalvalues.Length - 2);
                    int count = Convert.ToInt32(orginalvalues.Length);                    
                    secondValues = orginalvalues.Substring(orginalvalues.Length-2, 2);
                    txtValue.Text=(fristvalues+"."+secondValues).ToString();
                }
                else if (chararrinput.Length >=5)
                {
                    
                    fristvalues = orginalvalues.Substring(0, orginalvalues.Length - 2);
                    int count = Convert.ToInt32(orginalvalues.Length);                    
                    secondValues = orginalvalues.Substring(orginalvalues.Length - 2, 2);
                    txtValue.Text = (fristvalues + "." + secondValues).ToString();
                }
                else
                {
                    txtValue.Text = "00.0" + orginalvalues.ToString();
                }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceOrTabKey(e.Key);   
            //e.Handled = !char.IsControl(e.Key) && !char.IsDigit(e.KeyChar);
            if (e.Key == Key.NumPad9)
            {
                strTemp = 9;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad8)
            {                
                strTemp = 8;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad7)
            {                
                strTemp = 7;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad6)
            {                
                strTemp = 6;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad5)
            {                
                strTemp = 5;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad4)
            {                
                strTemp = 4;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad3)
            {                
                strTemp = 3;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad2)
            {               
                strTemp = 2;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad1)
            {                
                strTemp = 1;
                funKeyDownpress();
            }
            if (e.Key == Key.NumPad0)
            {                
                strTemp = 0;
                funKeyDownpress();
            }
        }

        private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
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
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }        
       
    }
}
