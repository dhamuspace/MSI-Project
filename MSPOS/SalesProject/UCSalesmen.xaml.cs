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
    /// Interaction logic for UCSalesmen.xaml
    /// </summary>
    public partial class UCSalesmen : UserControl
    {
        public UCSalesmen()
        {
            InitializeComponent();

            if (dtDisplay.Columns.Count == 0)
            {
                dtDisplay.Columns.Add("Item", typeof(string));
                dtDisplay.Columns.Add("Qty", typeof(string));
                dtDisplay.Columns.Add("Rate", typeof(string));
                dtDisplay.Columns.Add("Amt", typeof(string));
                dtDisplay.Columns.Add("Disc", typeof(string));
                dtDisplay.Columns.Add("SDisc", typeof(string));
                dtDisplay.Columns.Add("Other", typeof(string));
            }
            if (dtSalesmen.Columns.Count == 0)
            {
                dtSalesmen.Columns.Add("SalRecLed");
                dtSalesmen.Columns.Add("SalRecAmt");
                dtSalesmen.Columns.Add("SalRecRefundAmt");
                dtSalesmen.Columns.Add("SalRecType");
            }
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            funsalesmenLoad();
        }
        string strlblBillNo, strlblTotQty, strlblTotAmt, strlblDiscount, strlblNetAmt, strlblTaxAmt;
        public void funsalesmenLoad()
        {
            _Class.clsVariables.tVoidActionType = "SALESMEN";
            txtNote.Text = "";
            txtSalesmen.Text = "";
            pnlSalesmenList.UCPnlItemDisplay.Children.Clear();
            pnlSalesmenList.UCPnlItemDisplay.Width = 460;
            SqlCommand cmd = new SqlCommand("select Ledger_Name as Salesmen_Name from Ledger_table where Ledger_groupno=51 and Ledger_no<>14",con);
            //SqlCommand cmd = new SqlCommand("select Ledger_Name as Salesmen_Name from Ledger_table where  Ledger_no<>14", con);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                for (int x = 0; dt.Rows.Count > x; x++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dt.Rows[x]["Salesmen_Name"].ToString();
                    newBtn.FontSize = 16;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    newBtn.Name = "Salesmen" + i;
                    newBtn.Width = 420;
                    newBtn.Height = 65;
                    newBtn.Foreground = Brushes.White;
                    newBtn.FontSize = 18;
                    newBtn.FontWeight = FontWeights.Black;
                    newBtn.BorderBrush = Brushes.White;
                    newBtn.BorderThickness = new Thickness(1.8);
                    //newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#008080"));
                    newBtn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#551A8B"));
                    newBtn.Margin = new Thickness(2, 1, 1, 2);
                    newBtn.Focusable = false;                   
                    
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    pnlSalesmenList.UCPnlItemDisplay.Children.Add(newBtn);
                   
                }
            }
            txtNote.Focus();
        }
        private void newBtnGroup_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton=(Button)sender;
            txtSalesmen.Text = clickedButton.Content.ToString();
            //frmKeyBoard frm = new frmKeyBoard();
            //frm.SalesCreationEventHandlerNew += new EventHandler(ClosesalesmenNote);
            //frm.ShowDialog();
            //txtNote.Focus();

            //_Class.clsVariables.tVoidActionType = "salmen";
        }
        public void ClosesalesmenNote(object sender, EventArgs e)
        {
            txtNote.Text = _Class.clsVariables.tVoidValue;
            txtNote.Focus();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            txtSalesmen.Text = "";
            txtNote.Text = "";
            _Class.clsVariables.tVoidActionType = "SALESITEMCODE";
        }
        public DataTable dtSalesmen = new DataTable();
        public event System.EventHandler SalesCreationEventHandlerNewSalesmen;
        public DataTable dtDisplay = new DataTable();
        string strLedgerno;
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if ((txtSalesmen.Text == "") && (string.IsNullOrEmpty(txtSalesmen.Text.Trim())))
            {
                MyMessageBox.ShowBox("Select the Salesmen","Warning");
                txtSalesmen.Focus();
            }
            else
            {
                if (SalesProject._Class.clsVariables.tControlFrom != "VOID")
                {
                    _Class.clsVariables.tempsalesmenLedgerNo = "";
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlCommand cmdSales = new SqlCommand("select Ledger_no from Ledger_table where Ledger_name='" + txtSalesmen.Text.Trim() + "'", con);
                    DataTable dt = new DataTable();
                    dt.Rows.Clear();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSales);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        strLedgerno = dt.Rows[0]["Ledger_no"].ToString();
                    }
                    _Class.clsVariables.tempsalesmenLedgerNo = strLedgerno;
                    if (txtNote.Text != "")
                    {
                        _Class.clsVariables.tempsalesmenNote = txtNote.Text.Trim();
                    }
                    else
                    {
                        _Class.clsVariables.tempsalesmenNote = "Null";
                    }                   
                    SalesCreationEventHandlerNewSalesmen(sender ,e);

                    this.Visibility = Visibility.Hidden;
                    txtNote.Text = "";
                    txtSalesmen.Text = "";
                }
                
            }
        }

        private void BtnKey_Click(object sender, RoutedEventArgs e)
        {
            frmKeyBoard frm = new frmKeyBoard();
            frm.SalesCreationEventHandlerNew += new EventHandler(ClosesalesmenNote);
            frm.ShowDialog();
            txtNote.Focus();
        }

          
      
    }
}
