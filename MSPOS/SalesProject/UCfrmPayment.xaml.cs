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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using ClosedXML.Excel;
using Microsoft.Reporting.WinForms;
using System.Threading;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for UCfrmPayment.xaml
    /// </summary>
    public delegate void UCVoidEventdelegate();
    public partial class UCfrmPayment : UserControl
    {
        public event UCVoidEventdelegate UCVoidEventdelegate_click;
        public UCfrmPayment()
        {
            InitializeComponent();
        }
        string charPerLine, lineBelowLogo, topLine1, topLine2, topLine3, topLine4, topLine5;
        string mainStr;
        double findCenterPosition;
        int tNoPrint = 0;
        DataTable dtPrint = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void btnClearCls_Click(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            //if (UCVoidEventdelegate_click != null)
            //{
            //    UCVoidEventdelegate_click();
            //}
            
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (UCVoidEventdelegate_click != null)
            {
                UCVoidEventdelegate_click();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            pnlHouseACPayment.Visibility = Visibility.Visible;
            pnlSupplierPayment.Visibility = Visibility.Hidden;
            pnlExpansesDisplay.Visibility = Visibility.Hidden;
            txtHACName.Text = string.Empty;
            txtHACCode.Text = string.Empty;
            txtHACAccountLimit.Text = "0.00";
            txtHACAvailableCredit.Text = "0.00";
            txtHACBalanceDue.Text = "0.00";
            txtHACPaymentAmt.Text = "";
            funLoadCustomerDetails();
            funPaymentAmtDetail(_Class.clsVariables.tEndOfDayDate, _Class.clsVariables.tCounter);
        }    
        SqlDataReader dr = null;
        public void funLoadCustomerDetails()
        {
            //   funConnectionStateCheck();
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd = new SqlCommand("select distinct(SUBSTRING(LTrim(UPPER(Ledger_name)),0,2)) as Card_Name from Ledger_table where Ledger_groupno=32 and Ledger_no<>2", con);
            con.Close();
            con.Open();
            dr = cmd.ExecuteReader();
            dtNew.Load(dr);
            int i = 0;
            string tLoadLetter = "";
            pnlPaymentListLetter.Children.Clear();
            pnlPaymentList.Children.Clear();
            for (int mn = 0; mn < dtNew.Rows.Count; mn++)
            {
                i += 1;
                Button newBtn = new Button();
                newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                tLoadLetter = dtNew.Rows[mn]["Card_Name"].ToString();
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
                newBtn.Name = "HACL" + i;
                //newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                newBtn.Width = 85;
                newBtn.Height = 68;
                newBtn.Margin = new Thickness(1, 1, 1, 1);
                // newBtn.Style = this.Resources["btnGroup"] as Style;
                //Every Button Name Calles As newBtnGroup_Click
                newBtn.Click += new RoutedEventHandler(newBtnGroupCharLetter_Click);
                //newBtn.Style = "btnnoborder";
                //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                pnlPaymentListLetter.Children.Add(newBtn);
                pnlPaymentListLetter.Width = (i * 85) + 50;

            }
            if (tLoadLetter != "")
            {
                funCustomerLoadListAll();
            }
            // con.Close();

        }
        private void newBtnGroupCharLetter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Getting Click Button Name values here:
                Button clickedButton = (Button)sender;
                funlCustomerLoadList(clickedButton.Content.ToString());
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funlCustomerLoadList(string tStartLetter)
        {
            try
            {

                //  funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 and Ledger_name like @tStart order by Ledger_name ASC", con);
                cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlPaymentList.Children.Clear();
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
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnHACCustomerName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;

                }
                // con.Close();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string tLedgerNo = "";
        double tHACAvailableCredit = 0.00;
        private void newBtnHACCustomerName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //SqlCommand cmdTrigger = new SqlCommand("sp_LedgerTrigger", con);
                //cmdTrigger.CommandType = CommandType.StoredProcedure;
                //cmdTrigger.ExecuteNonQuery();

                tHACAvailableCredit = 0.00;
                Button clickedButton = (Button)sender;
                // _Class.clsVariables.funControlSetting();
                DataTable dtLedgerDetails = new DataTable();
                dtLedgerDetails.Rows.Clear();
                SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=32", con);
                cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", clickedButton.Content.ToString());
                SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                adpLedgerDetails.Fill(dtLedgerDetails);

                if (dtLedgerDetails.Rows.Count > 0)
                {
                    tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                    txtHACName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                    txtHACCode.Text = (dtLedgerDetails.Rows[0]["Ledger_Code"].ToString().Trim() == "0") ? "" : dtLedgerDetails.Rows[0]["Ledger_Code"].ToString();
                    txtHACAccountLimit.Text = dtLedgerDetails.Rows[0]["Limit_Amount"].ToString();
                    double tLimitAmt = (dtLedgerDetails.Rows[0]["Limit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Amount"].ToString());
                    double tCreditAmt = (dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString());
                    double tPaidAmt = (dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString());
                    txtHACBalanceDue.Text = string.Format("{0:0.00}", ((tCreditAmt - tPaidAmt) < 0) ? 0.00 : (tCreditAmt - tPaidAmt));
                    txtHACAvailableCredit.Text = string.Format("{0:0.00}", (tLimitAmt - (tCreditAmt - tPaidAmt)));
                    tHACAvailableCredit = txtHACAvailableCredit.Text == "" ? 0.00 : Convert.ToDouble(txtHACAvailableCredit.Text.Trim()) - (txtHACAccountLimit.Text == "" ? 0.00 : Convert.ToDouble(txtHACAccountLimit.Text));
                    //  funPaymentAmtDetail(currentDate, _Class.clsVariables.tCounter);
                }
                else
                {
                    MyMessageBox.ShowBox("House Account Name not Valid", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funCustomerLoadListAll()
        {
            try
            {

                //  funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 order by Ledger_name ASC", con);
                //  cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;

                pnlPaymentList.Children.Clear();
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
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnHACCustomerName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;

                }
                //  con.Close();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnPaymentIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tHACAvailableCredit = 0.00;
                txtHACAvailableCredit.Text = tHACAvailableCredit.ToString();
                lblPaymentTitle.Content = "House Account Payment";
                pnlHouseACPayment.Visibility = Visibility.Visible;
                pnlSupplierPayment.Visibility = Visibility.Hidden;
                pnlExpansesDisplay.Visibility = Visibility.Hidden;
                txtHACName.Text = string.Empty;
                txtHACCode.Text = string.Empty;
                txtHACAccountLimit.Text = "0.00";
                txtHACAvailableCredit.Text = "0.00";
                txtHACBalanceDue.Text = "0.00";
                txtHACPaymentAmt.Text = "";
                funLoadCustomerDetails();
                tLedgerNo = "";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnPaymentOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //lblPaymentTitle.Content = "Payment Out - Supplier";
                pnlHouseACPayment.Visibility = Visibility.Hidden;
                pnlSupplierPayment.Visibility = Visibility.Visible;
                pnlExpansesDisplay.Visibility = Visibility.Hidden;
                txtSupCode.Text = string.Empty;
                txtSupName.Text = string.Empty;
                txtSupplierPaymentAmt.Text = "";
                txtSupplierBalanceDue.Text = "0.00";
                funLoadSupplierDetails();
                tLedgerNo = "";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funLoadSupplierDetails()
        {
            // funConnectionStateCheck();
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd = new SqlCommand("select distinct(SUBSTRING(LTrim(UPPER(Ledger_name)),0,2)) as Card_Name from Ledger_table where Ledger_groupno=31 and Ledger_no<>8", con);
            con.Close();
            con.Open();
            dr = cmd.ExecuteReader();
            dtNew.Load(dr);
            int i = 0;
            string tLoadLetter = "";
            pnlPaymentListLetter.Children.Clear();
            pnlPaymentList.Children.Clear();
            for (int mn = 0; mn < dtNew.Rows.Count; mn++)
            {
                i += 1;
                Button newBtn = new Button();
                newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                tLoadLetter = dtNew.Rows[mn]["Card_Name"].ToString();
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
                newBtn.Name = "HACL" + i;
                //newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                newBtn.Width = 85;
                newBtn.Height = 68;
                newBtn.Margin = new Thickness(1, 1, 1, 1);
                // newBtn.Style = this.Resources["btnGroup"] as Style;
                //Every Button Name Calles As newBtnGroup_Click
                newBtn.Click += new RoutedEventHandler(newBtnSupplierListLetter_Click);
                //newBtn.Style = "btnnoborder";
                //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                pnlPaymentListLetter.Children.Add(newBtn);
                pnlPaymentListLetter.Width = (i * 85) + 50;

            }
            if (tLoadLetter != "")
            {
                funSupplierLoadListAll();
            }
            // con.Close();
        }
        private void newBtnSupplierListLetter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Getting Click Button Name values here:
                Button clickedButton = (Button)sender;
                funlSupplierLoadList(clickedButton.Content.ToString());
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funSupplierLoadListAll()
        {
            try
            {

                //   funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=31 and Ledger_no<>8 order by Ledger_name ASC", con);
                //  cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                con.Close();
                con.Open();
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlPaymentList.Children.Clear();
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
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnSupplierName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;

                }
                //  con.Close();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void newBtnSupplierName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = (Button)sender;
                // _Class.clsVariables.funControlSetting();
                DataTable dtLedgerDetails = new DataTable();
                dtLedgerDetails.Rows.Clear();
                SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=31", con);
                cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", clickedButton.Content.ToString());
                SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                adpLedgerDetails.Fill(dtLedgerDetails);
                txtSupplierPaymentAmt.Text = "";
                if (dtLedgerDetails.Rows.Count > 0)
                {
                    tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                    txtSupName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                    txtSupCode.Text = (dtLedgerDetails.Rows[0]["Ledger_Code"].ToString().Trim() == "0") ? "" : dtLedgerDetails.Rows[0]["Ledger_Code"].ToString();
                    // txtHACAccountLimit.Text = dtLedgerDetails.Rows[0]["Limit_Amount"].ToString();
                    double tLimitAmt = (dtLedgerDetails.Rows[0]["Limit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Amount"].ToString());
                    double tCreditAmt = (dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString());
                    double tPaidAmt = (dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString());
                    txtSupplierBalanceDue.Text = string.Format("{0:0.00}", (tCreditAmt - tPaidAmt));
                    txtSupplierAvailableCredit.Text = string.Format("{0:0.00}", ((tCreditAmt - tPaidAmt)));
                }
                else
                {
                    MyMessageBox.ShowBox("Supplier Account Name not Valid", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funlSupplierLoadList(string tStartLetter)
        {
            try
            {
                // funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=31 and Ledger_no<>8 and Ledger_name like @tStart order by Ledger_name ASC", con);
                cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlPaymentList.Children.Clear();
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
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnSupplierName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;
                }
                // con.Close();

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnExpenses_Click(object sender, RoutedEventArgs e)
        {
           // lblPaymentTitle.Content = "Payment Out - Expenses";
            pnlHouseACPayment.Visibility = Visibility.Hidden;
            pnlSupplierPayment.Visibility = Visibility.Hidden;
            pnlExpansesDisplay.Visibility = Visibility.Visible;
            txtExptAmt.Text = string.Empty;
            txtExpType.Text = string.Empty;
            FunExpenessAllL();
            FunExpencess();
            tLedgerNo = "";
        }
        public void FunExpencess()
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno in (25,26) order by Ledger_name ASC", con);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlPaymentList.Children.Clear();
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                    newBtn.FontSize = 16;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Center;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    newBtn.Name = "HouseACExpeness" + i;
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnHACExpenessrName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;
                }
                //  con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void FunExpenessAllL()
        {
            DataTable dtNew = new DataTable();
            dtNew.Rows.Clear();
            SqlCommand cmd = new SqlCommand("select distinct(SUBSTRING(LTrim(UPPER(Ledger_name)),0,2)) as Card_Name from Ledger_table where Ledger_groupno in (25,26) and Ledger_no<>8", con);
            con.Close();
            con.Open();
            dr = cmd.ExecuteReader();
            dtNew.Load(dr);
            int i = 0;
            string tLoadLetter = "";
            pnlPaymentListLetter.Children.Clear();
            for (int mn = 0; mn < dtNew.Rows.Count; mn++)
            {
                i += 1;
                Button newBtn = new Button();
                newBtn.Content = dtNew.Rows[mn]["Card_Name"].ToString();
                tLoadLetter = dtNew.Rows[mn]["Card_Name"].ToString();
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
                newBtn.Name = "HACLedgerExpeness" + i;
                //newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                newBtn.Width = 85;
                newBtn.Height = 68;
                newBtn.Margin = new Thickness(1, 1, 1, 1);
                // newBtn.Style = this.Resources["btnGroup"] as Style;
                //Every Button Name Calles As newBtnGroup_Click
                newBtn.Click += new RoutedEventHandler(newBtnExpenesesListLetter_Click);
                //newBtn.Style = "btnnoborder";
                //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                pnlPaymentListLetter.Children.Add(newBtn);
                pnlPaymentListLetter.Width = (i * 85) + 50;

            }
        }
        private void newBtnExpenesesListLetter_Click(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            FunExpenesesLAll(ClickedButton.Content.ToString());
        }
        private void FunExpenesesLAll(string tStartLetter)
        {
            try
            {
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno in (25,26)  and Ledger_name like @tStart order by Ledger_name ASC", con);

                cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlPaymentList.Children.Clear();
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
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnHACExpenessrName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void newBtnHACExpenessrName_Click(object sender, EventArgs e)
        {
            try
            {
                Button ClickedButton1 = (Button)sender;
                txtExpType.Text = ClickedButton1.Content.ToString();
                //Getting First Letter Only:
                if (txtExpType.Text.Trim() != string.Empty)
                {
                    SqlCommand cmd_new = new SqlCommand("select * from ledger_table where ledger_name=@LedgerName", con);
                    // cmd_new.CommandType = CommandType.StoredProcedure;
                    cmd_new.Parameters.AddWithValue("@LedgerName", txtExpType.Text.ToString().Trim());
                    tLedgerNo = Convert.ToString(cmd_new.ExecuteScalar().ToString() == string.Empty ? "" : cmd_new.ExecuteScalar().ToString()).ToString();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtHACPaymentAmt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double tLimitAmt = (txtHACAccountLimit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAccountLimit.Text.Trim());
                double tBalanceAmt = (txtHACBalanceDue.Text.Trim() == "") ? 0.00 : double.Parse(txtHACBalanceDue.Text.Trim());
                double tPaymentAmt = 0.00;
                if (txtHACPaymentAmt.Text.Trim() != "" && txtHACName.Text.Trim() != "")
                {

                    if (txtHACPaymentAmt.Text.Trim() != "")
                    {
                        tPaymentAmt = double.Parse(txtHACPaymentAmt.Text.Trim());
                    }
                    txtHACAvailableCredit.Text = string.Format("{0:0.00}", (tLimitAmt + (tPaymentAmt - tBalanceAmt)));
                }
                else
                {
                    tHACAvailableCredit = 0.00;
                    tPaymentAmt = txtHACPaymentAmt.Text.Trim() == "" ? 0.00 : Convert.ToDouble(txtHACPaymentAmt.Text.Trim());
                    txtHACAvailableCredit.Text = string.Format("{0:0.00}", (tLimitAmt + (tPaymentAmt + tHACAvailableCredit - tBalanceAmt)));
                }
            }

            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtHACPaymentAmt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || (e.Text == "." && e.Text.IndexOf('.') > -1)))
            {
                e.Handled = true;
            }
        }

        private void btnHACUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtnHAC.PageUp();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnHACDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtnHAC.PageDown();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnHACPrev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtnHACLetter.PageLeft();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnHACNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                svBtnHACLetter.PageRight();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        DateTime currentDate;
        private void btnCash_Click(object sender, RoutedEventArgs e)
        {            
                try
                {
                    if (pnlHouseACPayment.Visibility == Visibility.Visible)
                    {
                        if (tLedgerNo != "")
                        {
                            SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                            cmdPayment.CommandType = CommandType.StoredProcedure;
                            cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                            cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", "CASH");
                            cmdPayment.Parameters.AddWithValue("@tPayment_Amt", (txtHACPaymentAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtHACPaymentAmt.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tAccount_Limit", (txtHACAccountLimit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAccountLimit.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tBalance_Due", (txtHACBalanceDue.Text.Trim() == "") ? 0.00 : double.Parse(txtHACBalanceDue.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", (txtHACAvailableCredit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAvailableCredit.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                            cmdPayment.ExecuteNonQuery();
                            lblPaymentTitle.Content = "House Account Payment";

                            pnlHouseACPayment.Visibility = Visibility.Visible;
                            txtHACName.Text = string.Empty;
                            txtHACCode.Text = string.Empty;
                            txtHACAccountLimit.Text = "0.00";
                            txtHACAvailableCredit.Text = "0.00";
                            txtHACBalanceDue.Text = "0.00";
                            txtHACPaymentAmt.Text = "";
                            funLoadCustomerDetails();

                        }
                    }

                    if (pnlSupplierPayment.Visibility == Visibility.Visible)
                    {
                        if (tLedgerNo != "")
                        {
                            SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                            cmdPayment.CommandType = CommandType.StoredProcedure;
                            cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                            cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", "CASH");
                            cmdPayment.Parameters.AddWithValue("@tPayment_Amt", (txtSupplierPaymentAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtSupplierPaymentAmt.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tAccount_Limit", "0.00");
                            cmdPayment.Parameters.AddWithValue("@tBalance_Due", (txtSupplierBalanceDue.Text.Trim() == "") ? 0.00 : double.Parse(txtSupplierBalanceDue.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", (txtSupplierAvailableCredit.Text.Trim() == "") ? 0.00 : double.Parse(txtSupplierAvailableCredit.Text.Trim()));
                            cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                            cmdPayment.ExecuteNonQuery();

                            //lblPaymentTitle.Content = "Payment Out - Supplier";
                            pnlSupplierPayment.Visibility = Visibility.Visible;
                            txtSupName.Text = string.Empty;
                            txtSupCode.Text = string.Empty;
                            //txtHACAccountLimit.Text = "0.00";
                            txtSupplierAvailableCredit.Text = "0.00";
                            txtSupplierBalanceDue.Text = "0.00";
                            txtSupplierPaymentAmt.Text = "";
                            funLoadSupplierDetails();
                        }
                    }
                    if (pnlExpansesDisplay.Visibility == Visibility.Visible)
                    {
                        if (tLedgerNo != "")
                        {
                            if (txtExptAmt.Text.Trim() != string.Empty)
                            {
                                SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                                cmdPayment.CommandType = CommandType.StoredProcedure;
                                cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                                cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", "CASH");
                                cmdPayment.Parameters.AddWithValue("@tPayment_Amt", (txtExptAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtExptAmt.Text.Trim()));
                                cmdPayment.Parameters.AddWithValue("@tAccount_Limit", "0.00");
                                cmdPayment.Parameters.AddWithValue("@tBalance_Due", "0.00");
                                cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", "0.0");
                                cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                                cmdPayment.ExecuteNonQuery();
                                txtExpType.Text = string.Empty;
                                txtExptAmt.Text = string.Empty;
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Please Enter Expenses Amount", "Warning");
                                txtExptAmt.Focus();
                            }
                        }
                    }
                    funPaymentAmtDetail(currentDate, _Class.clsVariables.tCounter);
                }
                catch (Exception ex)
                {
                    MyMessageBox.ShowBox(ex.Message, "Warning");
                }
            

        }

        public void funPaymentAmtDetail(DateTime tdate, string tCounterNo)
        {
            try
            {
                SqlCommand cmdTrigger = new SqlCommand("sp_LedgerTrigger", con);
                cmdTrigger.CommandType = CommandType.StoredProcedure;
                con.Close();
                con.Open();
                cmdTrigger.ExecuteNonQuery();

                //If any changes you made here also change Payment Collection button 
                DataTable dtPaymentAmt = new DataTable();
                //dtPaymentAmt.Rows.Clear();
                SqlCommand cmd = new SqlCommand(@"Create Table #TempTable1 (PaymentLedger_No varchar(400),Ledger_groupno1 varchar(400),Payment_Amt Numeric(18,2)) 
INSERT INTO #TempTable1 (PaymentLedger_No,Ledger_groupno1,Payment_Amt) (Select PaymentLedger_No as PaymentLedger_No,Ledger_groupno1 as Ledger_groupno1,(case when Ledger_groupno1=32 then  SUM(Payment_Amt) else -(SUM(Payment_Amt)) EnD) as Payment_Amt  from PaymentDetail_table where EndOfDay=@tDate and Ctr_no=@tCounterNo group by PaymentLedger_No,Ledger_GroupNo1)
Select 'Payment '+Ledger_table.Ledger_name as PaymentMode,SUM(Payment_Amt) as Amount from #TempTable1, Ledger_table where #TempTable1.PaymentLedger_No=Ledger_table.Ledger_no group by Ledger_table.Ledger_name
DROP TABLE #TempTable1", con);
                cmd.Parameters.AddWithValue("@tDate", (_Class.clsVariables.tEndOfDayDate.Year + "/" + _Class.clsVariables.tEndOfDayDate.Month + "/" + _Class.clsVariables.tEndOfDayDate.Day));
                cmd.Parameters.AddWithValue("@tCounterNo", tCounterNo);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtPaymentAmt);
                gridPayment.DataSource = dtPaymentAmt.DefaultView;

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnNETS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pnlHouseACPayment.Visibility == Visibility.Visible)
                {
                    if (tLedgerNo != "")
                    {
                        SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                        cmdPayment.CommandType = CommandType.StoredProcedure;
                        cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                        cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", "NETS");
                        cmdPayment.Parameters.AddWithValue("@tPayment_Amt", (txtHACPaymentAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtHACPaymentAmt.Text.Trim()));
                        cmdPayment.Parameters.AddWithValue("@tAccount_Limit", (txtHACAccountLimit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAccountLimit.Text.Trim()));
                        cmdPayment.Parameters.AddWithValue("@tBalance_Due", (txtHACBalanceDue.Text.Trim() == "") ? 0.00 : double.Parse(txtHACBalanceDue.Text.Trim()));
                        cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", (txtHACAvailableCredit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAvailableCredit.Text.Trim()));
                        cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                        cmdPayment.ExecuteNonQuery();

                        lblPaymentTitle.Content = "House Account Payment";
                        pnlHouseACPayment.Visibility = Visibility.Visible;
                        txtHACName.Text = string.Empty;
                        txtHACCode.Text = string.Empty;
                        txtHACAccountLimit.Text = "0.00";
                        txtHACAvailableCredit.Text = "0.00";
                        txtHACBalanceDue.Text = "0.00";
                        txtHACPaymentAmt.Text = "";
                        funLoadCustomerDetails();

                    }
                }
                funPaymentAmtDetail(currentDate, _Class.clsVariables.tCounter);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnCreditCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pnlPaymentList.Children.Clear();
                if (pnlHouseACPayment.Visibility == Visibility.Visible)
                {
                    if (txtHACName.Text.Trim() != "")
                    {
                        CreditCardHouseAccount();
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Select House Account First", "Warning");
                    }
                    // pnlPaymentList.Children.Clear();
                }
                else if (pnlSupplierPayment.Visibility == Visibility.Visible)
                {
                    CreditCardHouseAccount();
                }
                else if (pnlExpansesDisplay.Visibility == Visibility.Visible)
                {
                    CreditCardHouseAccount();
                }
                // con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void CreditCardHouseAccount()
        {
            try
            {
                //  funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                //  pnlPaymentList.Children.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=5 and Ledger_no<>14 order by Ledger_name ASC", con);
                //  cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                if (dtNew.Rows.Count <= 0)
                {
                    MyMessageBox.ShowBox("Credit Card Details Not Found", "Warning");
                }
                else
                {
                    pnlPaymentList.Children.Clear();
                }
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
                    newBtn.Width = 260;
                    newBtn.Height = 60;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnCreditCardName_Click);
                    pnlPaymentList.Children.Add(newBtn);
                    pnlPaymentList.Height = (i * 60) + 50;

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void newBtnCreditCardName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((txtHACPaymentAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtHACPaymentAmt.Text.Trim())) > 0)
                {
                    if (pnlHouseACPayment.Visibility == Visibility.Visible)
                    {

                        Button clickedButton = (Button)sender;
                        // _Class.clsVariables.funControlSetting();
                        DataTable dtLedgerDetails = new DataTable();
                        dtLedgerDetails.Rows.Clear();
                        SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=5", con);
                        cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", clickedButton.Content.ToString());
                        SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                        adpLedgerDetails.Fill(dtLedgerDetails);

                        if (dtLedgerDetails.Rows.Count > 0)
                        {
                            tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                            txtHACName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                            //if (tLedgerNo != "")
                            //{
                            //    SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                            //    cmdPayment.CommandType = CommandType.StoredProcedure;
                            //    cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                            //    cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", dtLedgerDetails.Rows[0]["Ledger_Name"].ToString());
                            //    cmdPayment.Parameters.AddWithValue("@tPayment_Amt", (txtHACPaymentAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtHACPaymentAmt.Text.Trim()));
                            //    cmdPayment.Parameters.AddWithValue("@tAccount_Limit", (txtHACAccountLimit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAccountLimit.Text.Trim()));
                            //    cmdPayment.Parameters.AddWithValue("@tBalance_Due", (txtHACBalanceDue.Text.Trim() == "") ? 0.00 : double.Parse(txtHACBalanceDue.Text.Trim()));
                            //    cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", (txtHACAvailableCredit.Text.Trim() == "") ? 0.00 : double.Parse(txtHACAvailableCredit.Text.Trim()));
                            //    cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                            //    cmdPayment.ExecuteNonQuery();


                            //    lblPaymentTitle.Content = "House Account Payment";
                            //    pnlHouseACPayment.Visibility = Visibility.Visible;
                            //    txtHACName.Text = string.Empty;
                            //    txtHACCode.Text = string.Empty;
                            //    txtHACAccountLimit.Text = "0.00";
                            //    txtHACAvailableCredit.Text = "0.00";
                            //    txtHACBalanceDue.Text = "0.00";
                            //    txtHACPaymentAmt.Text = "";
                            //    funLoadCustomerDetails();
                            //}
                            if (dtLedgerDetails.Rows.Count > 0)
                            {
                                tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                                txtSupName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                                txtSupCode.Text = (dtLedgerDetails.Rows[0]["Ledger_Code"].ToString().Trim() == "0") ? "" : dtLedgerDetails.Rows[0]["Ledger_Code"].ToString();
                                // txtHACAccountLimit.Text = dtLedgerDetails.Rows[0]["Limit_Amount"].ToString();
                                double tLimitAmt = (dtLedgerDetails.Rows[0]["Limit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Amount"].ToString());
                                double tCreditAmt = (dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString());
                                double tPaidAmt = (dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString());
                                txtSupplierBalanceDue.Text = string.Format("{0:0.00}", (tCreditAmt - tPaidAmt));
                                txtSupplierAvailableCredit.Text = string.Format("{0:0.00}", ((tCreditAmt - tPaidAmt)));
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Supplier Account Name not Valid", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("House Account Name not Valid", "Warning");
                        }

                    }
                    if (pnlSupplierPayment.Visibility == Visibility.Visible)
                    {
                        Button clickedButton = (Button)sender;
                        // _Class.clsVariables.funControlSetting();
                        DataTable dtLedgerDetails = new DataTable();
                        dtLedgerDetails.Rows.Clear();
                        SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=5", con);
                        cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", clickedButton.Content.ToString());
                        SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                        adpLedgerDetails.Fill(dtLedgerDetails);

                        if (dtLedgerDetails.Rows.Count > 0)
                        {
                            // tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                            txtSupName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                            if (tLedgerNo != "")
                            {
                                SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                                cmdPayment.CommandType = CommandType.StoredProcedure;
                                cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                                cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", dtLedgerDetails.Rows[0]["Ledger_Name"].ToString());
                                cmdPayment.Parameters.AddWithValue("@tPayment_Amt", (txtSupplierPaymentAmt.Text.Trim() == "") ? 0.00 : double.Parse(txtSupplierPaymentAmt.Text.Trim()));
                                cmdPayment.Parameters.AddWithValue("@tAccount_Limit", "0.00");
                                cmdPayment.Parameters.AddWithValue("@tBalance_Due", (txtSupplierBalanceDue.Text.Trim() == "") ? 0.00 : double.Parse(txtSupplierBalanceDue.Text.Trim()));
                                cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", (txtSupplierAvailableCredit.Text.Trim() == "") ? 0.00 : double.Parse(txtSupplierAvailableCredit.Text.Trim()));
                                cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                                cmdPayment.ExecuteNonQuery();


                                //lblPaymentTitle.Content = "Payment Out - Supplier";
                                pnlSupplierPayment.Visibility = Visibility.Visible;
                                txtSupName.Text = string.Empty;
                                txtSupCode.Text = string.Empty;
                                //  txtHACAccountLimit.Text = "0.00";
                                txtSupplierAvailableCredit.Text = "0.00";
                                txtSupplierBalanceDue.Text = "0.00";
                                txtSupplierPaymentAmt.Text = "";
                                funLoadSupplierDetails();
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Supplier Name not Valid", "Warning");
                        }
                    }
                    if (pnlExpansesDisplay.Visibility == Visibility.Visible)
                    {
                        // txtSupName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                        if (tLedgerNo != "")
                        {
                            if (txtExpType.Text.Trim() != string.Empty)
                            {
                                SqlCommand cmdPayment = new SqlCommand(@"sp_PaymentDetailHACInsert", con);
                                cmdPayment.CommandType = CommandType.StoredProcedure;
                                cmdPayment.Parameters.AddWithValue("@tLedger_no", tLedgerNo);
                                cmdPayment.Parameters.AddWithValue("@tPaymentLedger_NoType", txtExpType.Text.Trim());
                                cmdPayment.Parameters.AddWithValue("@tPayment_Amt", txtExptAmt.Text == "" ? "0.00" : Convert.ToDouble(txtExptAmt.Text).ToString("0.00"));
                                cmdPayment.Parameters.AddWithValue("@tAccount_Limit", "0.00");
                                cmdPayment.Parameters.AddWithValue("@tBalance_Due", "0.00");
                                cmdPayment.Parameters.AddWithValue("@tAvaliable_Credit", "0.00");
                                cmdPayment.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                                cmdPayment.ExecuteNonQuery();
                                txtExpType.Text = string.Empty;
                                txtExptAmt.Text = string.Empty;

                                FunExpenessAllL();
                                FunExpencess();
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Please Enter Expenses Amount", "Warning");
                            }
                        }
                    }
                    funPaymentAmtDetail(currentDate, _Class.clsVariables.tCounter);
                }
                else
                {
                    MyMessageBox.ShowBox("You should enter Amount", "Warning");
                    txtHACPaymentAmt.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        string temp = "";
        private void btnOne1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pnlHouseACPayment.Visibility == Visibility.Visible)
                {
                    if (txtHACName.Text.Trim() != "")
                    {
                        txtHACPaymentAmt.Focus();
                        Button btn = (Button)sender;
                        if ( txtHACPaymentAmt.Text != "")
                        {
                            temp = txtHACPaymentAmt.Text;
                            txtHACPaymentAmt.Text = "";
                            txtHACPaymentAmt.Text = temp + btn.Content.ToString();
                           // txtHACAvailableCredit.Text = string.Format("{0:0.00}", (Convert.ToDouble(Convert.ToString(txtHACPaymentAmt.Text))+Convert.ToDouble(Convert.ToString(txtHACAvailableCredit.Text))));
                        }
                        if (txtHACPaymentAmt.Text == "")
                        {
                            txtHACPaymentAmt.Text = btn.Content.ToString();
                           // txtHACAvailableCredit.Text = string.Format("{0:0.00}", (Convert.ToDouble(Convert.ToString(txtHACPaymentAmt.Text)) + Convert.ToDouble(Convert.ToString(txtHACAvailableCredit.Text))));
                        }
                        txtHACPaymentAmt.Select(txtHACPaymentAmt.Text.Length, 0);
                    }
                }
                if (pnlSupplierPayment.Visibility == Visibility.Visible)
                {
                    if (txtSupName.Text.Trim() != "")
                    {
                        txtSupplierPaymentAmt.Focus();
                        Button btn = (Button)sender;
                        if (txtSupplierPaymentAmt.Text != "")
                        {
                            temp = txtSupplierPaymentAmt.Text;
                            txtSupplierPaymentAmt.Text = "";
                            txtSupplierPaymentAmt.Text = temp + btn.Content.ToString();
                        }
                        if (txtSupplierPaymentAmt.Text == "")
                        {
                            txtSupplierPaymentAmt.Text = btn.Content.ToString();
                        }
                        txtSupplierPaymentAmt.Select(txtSupplierPaymentAmt.Text.Length, 0);
                    }
                }
                if (pnlExpansesDisplay.Visibility == Visibility.Visible)
                {
                    if (txtExpType.Text.Trim() != "")
                    {
                        txtExptAmt.Focus();
                        Button btn = (Button)sender;
                        if (txtExptAmt.Text != "")
                        {
                            temp = txtExptAmt.Text;
                            txtExptAmt.Text = "";
                            txtExptAmt.Text = temp + btn.Content.ToString();
                        }
                        if (txtExptAmt.Text == "")
                        {
                            txtExptAmt.Text = btn.Content.ToString();
                        }
                        txtExptAmt.Select(txtExptAmt.Text.Length, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnHACEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pnlHouseACPayment.Visibility == Visibility.Visible)
                {
                    funHACCodeSearch();
                }

                if (pnlSupplierPayment.Visibility == Visibility.Visible)
                {
                    funSupplierCodeSearch();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public void funSupplierCodeSearch()
        {
            if (txtSupCode.Text.Trim() != "")
            {
                try
                {
                    DataTable dtLedgerDetails = new DataTable();
                    dtLedgerDetails.Rows.Clear();
                    SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_code=@tLedgerName and Ledger_groupno=31", con);
                    cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", txtSupCode.Text.Trim());
                    SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                    adpLedgerDetails.Fill(dtLedgerDetails);

                    if (dtLedgerDetails.Rows.Count > 0)
                    {
                        tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                        txtSupName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                        txtSupCode.Text = (dtLedgerDetails.Rows[0]["Ledger_Code"].ToString().Trim() == "0") ? "" : dtLedgerDetails.Rows[0]["Ledger_Code"].ToString();
                        // txtHACAccountLimit.Text = dtLedgerDetails.Rows[0]["Limit_Amount"].ToString();
                        double tLimitAmt = (dtLedgerDetails.Rows[0]["Limit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Amount"].ToString());
                        double tCreditAmt = (dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString());
                        double tPaidAmt = (dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString());
                        txtSupplierBalanceDue.Text = string.Format("{0:0.00}", (tCreditAmt - tPaidAmt));
                        txtSupplierAvailableCredit.Text = string.Format("{0:0.00}", (tLimitAmt - (tCreditAmt - tPaidAmt)));
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Supplier Code not Found", "Warning");
                    }

                }
                catch (Exception ex)
                {
                    MyMessageBox.ShowBox(ex.Message, "Warning");
                }
            }
        }
        public void funHACCodeSearch()
        {
            if (txtHACCode.Text.Trim() != "")
            {
                try
                {
                    DataTable dtLedgerDetails = new DataTable();
                    dtLedgerDetails.Rows.Clear();
                    SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_code=@tLedgerName and Ledger_groupno=32", con);
                    cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName", txtHACCode.Text.Trim());
                    SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                    adpLedgerDetails.Fill(dtLedgerDetails);

                    if (dtLedgerDetails.Rows.Count > 0)
                    {
                        tLedgerNo = dtLedgerDetails.Rows[0]["Ledger_No"].ToString();
                        txtHACName.Text = dtLedgerDetails.Rows[0]["Ledger_Name"].ToString();
                        txtHACCode.Text = (dtLedgerDetails.Rows[0]["Ledger_Code"].ToString().Trim() == "0") ? "" : dtLedgerDetails.Rows[0]["Ledger_Code"].ToString();
                        txtHACAccountLimit.Text = dtLedgerDetails.Rows[0]["Limit_Amount"].ToString();
                        double tLimitAmt = (dtLedgerDetails.Rows[0]["Limit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Amount"].ToString());
                        double tCreditAmt = (dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString());
                        double tPaidAmt = (dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString());
                        txtHACBalanceDue.Text = string.Format("{0:0.00}", (tCreditAmt - tPaidAmt));
                        txtHACAvailableCredit.Text = string.Format("{0:0.00}", (tLimitAmt - (tCreditAmt - tPaidAmt)));
                    }
                    else
                    {
                        MyMessageBox.ShowBox("House Account Code not Found", "Warning");
                    }

                }
                catch (Exception ex)
                {
                    MyMessageBox.ShowBox(ex.Message, "Warning");
                }
            }
        }
        DataSet ds = new DataSet();        
        private void btnPaymentPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtHACName.Text.Trim() != "")
                {
                    if (txtHACPaymentAmt.Text.Trim() != "")
                    {

                        funPaymentPrint();
                        //btnPrint_Click(sender, e);

                        if (tPrinterType == "Receipt")
                        {
                            DataTable dtPrinter = new DataTable();
                            dtPrinter.Rows.Clear();
                            SqlDataAdapter adpPrinter = new SqlDataAdapter("select * from CrystalReportPrinterList", con);
                            adpPrinter.Fill(dtPrinter);
                            bool isChkPrinter = false;
                            for (int i = 0; i < dtPrinter.Rows.Count; i++)
                            {
                                string printerName = dtPrinter.Rows[i]["PrinterName"].ToString();
                                isChkPrinter = false;
                                if (_Class.clsVariables.tempGPrinterName == printerName.ToUpper())
                                {
                                    //isChkPrinter = true;
                                    //rptReceiptReport rpt = new rptReceiptReport();
                                    //CrystalDecisions.CrystalReports.Engine.TextObject str1 = ((CrystalDecisions.CrystalReports.Engine.TextObject)rpt.Section2.ReportObjects["Text1"]);
                                    //str1.Text = mainStr;
                                    //rpt.PrintToPrinter(0, true, 1, 0);
                                    //break;

                                    reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcReceipt.rdlc";
                                    ReportParameter rpReportOn = new ReportParameter("ReceiptValue", Convert.ToString(mainStr), false);
                                    this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });
                                    reportViewerSales.RefreshReport();
                                    reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);
                                    break;

                                }
                            }
                            if (isChkPrinter == false)
                            {
                                //for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
                                //{
                                //    if (dtPrint.Rows[k]["Describ"].ToString() == "Print Copies*")
                                //    {
                                topLine5 = _Class.clsVariables.tempGPrintCopies;
                                if (topLine5 == "1 Copy")
                                {
                                    tNoPrint = 1;
                                }
                                else if (topLine5 == "2 Copy")
                                {
                                    tNoPrint = 2;
                                }
                                else if (topLine5 == "3 Copy")
                                {
                                    tNoPrint = 3;
                                }
                                else if (topLine5 == "No Copies")
                                {
                                    tNoPrint = 0;
                                }

                                for (int i2 = 0; i2 < tNoPrint; i2++)
                                {
                                    // RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, mainStr);

                                    Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, mainStr));
                                    workerThread.Start();
                                    bool finished = workerThread.Join(3000);
                                    if (!finished)
                                    {
                                        workerThread.Abort();
                                        // CancelPrintJob();
                                    }


                                    // string s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 29, 86, 66, 0, 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                    // RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);


                                    //for (int i80 = 0; i80 < dtPrint.Rows.Count - 1; i80++)
                                    //{
                                    //    if (dtPrint.Rows[i80]["Describ"].ToString() == "Printer Name*")
                                    //    {

                                    //        for (int i81 = 0; i81 < dtPrint.Rows.Count - 1; i81++)
                                    //        {
                                    //if (dtPrint.Rows[i81]["Describ"].ToString() == "Cut Paper")
                                    //{
                                    if (_Class.clsVariables.tempGCutPaper == "Yes")
                                    {
                                        DataTable dtNew = new DataTable();
                                        dtNew.Rows.Clear();
                                        SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
                                        cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                                        SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
                                        adp.Fill(dtNew);
                                        if (dtNew.Rows.Count > 0)
                                        {
                                            //if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
                                            //{

                                            //  if (dtNew.Rows[0]["Action"].ToString().Trim() == "Cut")
                                            //  {

                                            string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

                                            byteOut = new byte[byteStrings.Length];

                                            for (int i = 0; i < byteStrings.Length; i++)
                                            {
                                                byteOut[i] = Convert.ToByte(byteStrings[i]);
                                            }
                                            //  s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
                                            //    }

                                            string s1 = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

                                            Thread workerThread1 = new Thread(() => RawPrinterHelper.SendStringToPrinter(_Class.clsVariables.tempGPrinterName, s1));
                                            workerThread1.Start();
                                            finished = workerThread1.Join(3000);
                                            if (!finished)
                                            {
                                                workerThread1.Abort();
                                                //CancelPrintJob();
                                            }
                                            // }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Enter Payment Amount", "Warning");
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Select House Account Name", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }
        }
        public void funPaymentPrint()
        {
            try
            {
                if (pnlHouseACPayment.Visibility == Visibility.Visible)
                {
                    DataTable dtPaymentPrint = new DataTable();
                    if (dtPaymentPrint.Columns.Count == 0)
                    {
                        dtPaymentPrint.Columns.Add("Particulars", typeof(string));
                        dtPaymentPrint.Columns.Add("Amount", typeof(string));
                    }
                    dtPaymentPrint.Rows.Clear();

                    if (txtHACName.Text.Trim() != "" && txtHACPaymentAmt.Text.Trim() != "")
                    {
                        dtPaymentPrint.Rows.Add("Name", txtHACName.Text.Trim());
                        if (txtHACCode.Text.Trim() != "")
                        {
                            dtPaymentPrint.Rows.Add("Code", txtHACCode.Text.Trim());
                        }
                        dtPaymentPrint.Rows.Add("Payment Amount", string.IsNullOrEmpty(Convert.ToString(txtHACPaymentAmt.Text)) ? "0.00" : string.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(txtHACPaymentAmt.Text))));
                        dtPaymentPrint.Rows.Add("Account Limit", txtHACAccountLimit.Text.Trim());
                        dtPaymentPrint.Rows.Add("Balance Due", txtHACBalanceDue.Text.Trim());
                        dtPaymentPrint.Rows.Add("Available Credit", txtHACAvailableCredit.Text.Trim());
                        // dtPaymentPrint.Rows.Add("Available Credit", txtHACAvailableCredit.Text.Trim());

                        mainStr = "";
                        funPrintHeaderPart();
                        charPerLine = _Class.clsVariables.tempGCharactersPerLine;
                        topLine1 = "HOUSE ACCOUNT PAYMENT REPORT";
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            mainStr += "\n";
                        }


                        topLine1 = DateTime.Now.ToString("dd/MM/yyyy");
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            mainStr += "\n";
                        }


                        //topLine1 = "Report Date : " + currentDate.Day.ToString() + "/" + currentDate.Month.ToString() + "/" + currentDate.Year.ToString();
                        topLine1 = "Report Date : " + _Class.clsVariables.tEndOfDayDate.Day.ToString() + "/" + _Class.clsVariables.tEndOfDayDate.Month.ToString() + "/" + _Class.clsVariables.tEndOfDayDate.Year.ToString();
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            mainStr += "\n";
                        }

                        topLine1 = txtHACName.Text.Trim();
                        if (topLine1.Length <= double.Parse(charPerLine))
                        {
                            findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                            if (findCenterPosition % 2 == 0)
                            {
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ((findCenterPosition / 2) + 1); j++)
                                {
                                    mainStr += " ";
                                }
                                mainStr += topLine1;
                                for (int j = 0; j < (findCenterPosition / 2); j++)
                                {
                                    mainStr += " ";
                                }
                            }
                            mainStr += "\n";
                        }

                        //Print Line Below Header
                        //for (int i4 = 0; i4 < dtPrint.Rows.Count - 1; i4++)
                        //{
                        //    if (dtPrint.Rows[i4]["Describ"].ToString() == "Characters Per Line*")
                        //    {
                        //        charPerLine = dtPrint.Rows[i4]["Property"].ToString();
                        //    }

                        //    // print lint below logo
                        //    if (dtPrint.Rows[i4]["Describ"].ToString() == "Print Line Below Header")
                        //    {
                        lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                        if (lineBelowLogo == "No Line")
                        {
                            for (int j = 0; j < double.Parse(charPerLine); j++)
                            {
                                mainStr += " ";
                            }
                            mainStr += "\n";
                        }
                        if (lineBelowLogo == "Single Line")
                        {
                            for (int j = 0; j < double.Parse(charPerLine); j++)
                            {
                                mainStr += "-";
                            }
                            mainStr += "\n";
                        }
                        else if (lineBelowLogo == "Double Line")
                        {
                            for (int j = 0; j < double.Parse(charPerLine); j++)
                            {
                                mainStr += "=";
                            }
                            mainStr += "\n";
                        }

                        ds.Tables.Clear();
                        // mainStrSub = "";
                        //   dt_griddiaplay.Rows.Clear();
                        string amount_string = "0.00";

                        findCenterPosition = (double.Parse(charPerLine) - 15);
                        topLine1 = "Payment Amount";
                        string rate = String.Format("{0:0.00}", Convert.ToDouble(Convert.ToString(txtHACPaymentAmt.Text.Trim())));
                        amount_string = txtHACPaymentAmt.Text.Trim();
                        mainStr += topLine1.PadRight((int)findCenterPosition, ' ');
                        if (rate.Length <= 15)
                        {
                            mainStr += rate.PadLeft(15, ' ');
                        }
                        mainStr += "\n";

                        //  dt_griddiaplay.Rows.Add("Payment Amount", amount_string, "", "");

                        findCenterPosition = (double.Parse(charPerLine) - 15);
                        topLine1 = "Account Limit";
                        rate = String.Format("{0:0.00}", (double.Parse(txtHACAccountLimit.Text.ToString())));
                        amount_string = rate.ToString();
                        mainStr += topLine1.PadRight((int)findCenterPosition, ' ');
                        if (rate.Length <= 15)
                        {
                            mainStr += rate.PadLeft(15, ' ');
                        }
                        mainStr += "\n";

                        //  dt_griddiaplay.Rows.Add("Account Limit", String.Format("{0:0.00}", (double.Parse(txtHACAccountLimit.Text.ToString()))), "", "");

                        findCenterPosition = (double.Parse(charPerLine) - 15);
                        topLine1 = "Balance Due";
                        rate = txtHACBalanceDue.Text.ToString().Trim();
                        amount_string = rate.ToString();
                        mainStr += topLine1.PadRight((int)findCenterPosition, ' ');
                        if (rate.Length <= 15)
                        {
                            mainStr += rate.PadLeft(15, ' ');
                        }
                        mainStr += "\n";
                        //break;

                        // dt_griddiaplay.Rows.Add("Balance Due", amount_string, "", "");


                        findCenterPosition = (double.Parse(charPerLine) - 15);
                        topLine1 = "Available Credit";
                        rate = (txtHACAvailableCredit.Text.Trim() != "") ? ((double.Parse(txtHACAvailableCredit.Text.Trim()) > 0) ? txtHACAvailableCredit.Text.Trim() : "0.00") : "0.00";
                        amount_string = rate.ToString();
                        mainStr += topLine1.PadRight((int)findCenterPosition, ' ');
                        if (rate.Length <= 15)
                        {
                            mainStr += rate.PadLeft(15, ' ');
                        }
                        mainStr += "\n";
                        //break;

                        //  dt_griddiaplay.Rows.Add("Available Credit", amount_string, "", "");

                        // lblPrint.Content = mainStr;
                        //Print Line Below Header

                        // print lint below logo

                        lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
                        if (lineBelowLogo == "No Line")
                        {
                            for (int j = 0; j < double.Parse(charPerLine); j++)
                            {
                                mainStr += " ";
                            }
                            mainStr += "\n";
                        }
                        if (lineBelowLogo == "Single Line")
                        {
                            for (int j = 0; j < double.Parse(charPerLine); j++)
                            {
                                mainStr += "-";
                            }
                            mainStr += "\n";
                        }
                        else if (lineBelowLogo == "Double Line")
                        {
                            for (int j = 0; j < double.Parse(charPerLine); j++)
                            {
                                mainStr += "=";
                            }
                            mainStr += "\n";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }
        }
        public void funPrintHeaderPart()
        {
            mainStr = "";
            charPerLine = _Class.clsVariables.tempGCharactersPerLine;
            lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowLogo;

            if (lineBelowLogo == "No Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), ' ');
                mainStr += "\n";
                // break;
            }
            if (lineBelowLogo == "Single Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '-');
                mainStr += "\n";
                // break;
            }
            else if (lineBelowLogo == "Double Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '=');
                mainStr += "\n";
                // break;
            }
            if (_Class.clsVariables.tempGPrintTopLine1 == "Yes")
            {

                topLine1 = _Class.clsVariables.tempGTopLine1;
                if (topLine1.Length <= double.Parse(charPerLine))
                {
                    findCenterPosition = (double.Parse(charPerLine) - topLine1.Length);
                    if (findCenterPosition % 2 == 0)
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        mainStr += topLine1;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    else
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                        mainStr += topLine1;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    mainStr += "\n";
                }
                // break;

            }

            if (_Class.clsVariables.tempGPrintTopLine2 == "Yes")
            {
                topLine2 = _Class.clsVariables.tempGTopLine2;
                if (topLine2.Length <= double.Parse(charPerLine))
                {
                    findCenterPosition = (double.Parse(charPerLine) - topLine2.Length);
                    if (findCenterPosition % 2 == 0)
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        mainStr += topLine2;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    else
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                        mainStr += topLine2;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    mainStr += "\n";
                }
                //  break;
            }

            if (_Class.clsVariables.tempGPrintTopLine3 == "Yes")
            {

                topLine3 = _Class.clsVariables.tempGTopLine3;
                if (topLine3.Length <= double.Parse(charPerLine))
                {
                    findCenterPosition = (double.Parse(charPerLine) - topLine3.Length);
                    if (findCenterPosition % 2 == 0)
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        mainStr += topLine3;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    else
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                        mainStr += topLine3;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    mainStr += "\n";
                    //  break;
                }
            }

            if (_Class.clsVariables.tempGPrintTopLine4 == "Yes")
            {

                topLine4 = _Class.clsVariables.tempGTopLine4;
                if (topLine4.Length <= double.Parse(charPerLine))
                {
                    findCenterPosition = (double.Parse(charPerLine) - topLine4.Length);
                    if (findCenterPosition % 2 == 0)
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        mainStr += topLine4;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    else
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                        mainStr += topLine4;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    mainStr += "\n";
                    //  break;
                }
            }
            if (_Class.clsVariables.tempGPrintTopLine5 == "Yes")
            {
                topLine5 = _Class.clsVariables.tempGTopLine5;
                if (topLine5.Length <= double.Parse(charPerLine))
                {
                    findCenterPosition = (double.Parse(charPerLine) - topLine5.Length);
                    if (findCenterPosition % 2 == 0)
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                        mainStr += topLine5;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    else
                    {
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2) + 1), ' ');
                        mainStr += topLine5;
                        mainStr += "".PadLeft(Convert.ToInt16((findCenterPosition / 2)), ' ');
                    }
                    mainStr += "\n";
                }

            }

            // print lint below logo

            lineBelowLogo = _Class.clsVariables.tempGPrintLineBelowHeader;
            if (lineBelowLogo == "No Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), ' ');
                mainStr += "\n";
            }
            if (lineBelowLogo == "Single Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '-');
                mainStr += "\n";
            }
            else if (lineBelowLogo == "Double Line")
            {
                mainStr += "".PadLeft(Convert.ToInt16(double.Parse(charPerLine)), '=');
                mainStr += "\n";
            }

        }
        DataTable dt_griddiaplay = new DataTable();
        DataTable dt_griddiaplay1 = new DataTable();
        DataTable dtCash = new DataTable();
        DataTable dtNETS = new DataTable();
        DateTime tEndOfDayDate = new DateTime();
        DataTable dtPDF = new DataTable();
        DataTable dtHACPayment = new DataTable();
        DataTable dtDebitCredit = new DataTable();
        DataTable dtTaxReport = new DataTable();
        string tPrinterType = "";
        byte[] byteOut;
        string tPrintingType = "";

        string savefilename;
        public void savedialog()
        {
            try
            {
                System.Windows.Forms.SaveFileDialog savefiledialog = new System.Windows.Forms.SaveFileDialog();
                savefiledialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                savefiledialog.DefaultExt = "Pdf";
                savefiledialog.Filter = "Your extension here (*.pdf)|*.pdf";
                savefiledialog.FilterIndex = 1;
                savefiledialog.RestoreDirectory = true;
                savefiledialog.FileName = "";
                savefiledialog.ShowDialog();
                savefilename = savefiledialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        string saveExcelfilename;
        public void saveExceldialog()
        {
            try
            {
                System.Windows.Forms.SaveFileDialog savefiledialog = new System.Windows.Forms.SaveFileDialog();
                savefiledialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                savefiledialog.DefaultExt = "Xls";
                savefiledialog.Filter = "Your extension here (*.xls)|*.xls";
                savefiledialog.FilterIndex = 1;
                savefiledialog.RestoreDirectory = true;
                savefiledialog.FileName = "";
                savefiledialog.ShowDialog();
                saveExcelfilename = savefiledialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        DataTable dt_selecttable = new DataTable();
        //private void btnPrint_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {

        //        if (tPrintingType == "PDF")
        //        {
        //            if (dtPDF.Rows.Count > 0)
        //            {
        //                try
        //                {
        //                    Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 10, 10);
        //                    savedialog();
        //                    if (savefilename != "")
        //                    {
        //                        PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(savefilename, FileMode.Create));
        //                        doc.Open();
        //                        iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("Item Details \n\n");
        //                        paragraph.IndentationLeft = 250;
        //                        doc.Add(new iTextSharp.text.Paragraph(""));
        //                        int numcolumns = dtPDF.Columns.Count;
        //                        PdfPTable table = new PdfPTable(numcolumns);
        //                        table.DefaultCell.Padding = 2;
        //                        table.WidthPercentage = 80;
        //                        table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //                        //if (numcolumns != dtStudentList.Columns.Count)
        //                        //{
        //                        //    MessageBox.Show("Invalid Columns in datagrid");
        //                        //}
        //                        table.DefaultCell.BorderWidth = 0.5f;
        //                        table.DefaultCell.GrayFill = 0.8f;
        //                        table.DefaultCell.MinimumHeight = 15;
        //                        foreach (DataColumn col in dtPDF.Columns)
        //                        {
        //                            Phrase phrase = new Phrase(col.ColumnName.ToString(), FontFactory.GetFont("Verdana", 7, iTextSharp.text.Font.BOLD));
        //                            table.AddCell(phrase);
        //                        }
        //                        table.HeaderRows = 1;
        //                        for (int i = 0; i < dtPDF.Rows.Count; i++)
        //                        {
        //                            for (int j = 0; j < dtPDF.Columns.Count; j++)
        //                            {
        //                                table.DefaultCell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
        //                                table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
        //                                Phrase pharse = new Phrase(dtPDF.Rows[i][j].ToString(), FontFactory.GetFont("Verdana", 5));
        //                                table.AddCell(pharse);
        //                            }
        //                        }
        //                        doc.Add(paragraph);
        //                        doc.Add(table);
        //                        doc.Add(new iTextSharp.text.Paragraph(""));
        //                        doc.Close();
        //                        // MessageBox.Show("Item PDF is Generated Successfully.." + savefilename);
        //                        savefilename = "";
        //                        tPrintingType = "";
        //                    }

        //                }
        //                catch (Exception ex)
        //                {
        //                    MyMessageBox.ShowBox(ex.Message, "Warning");
        //                }
        //            }
        //        }
        //        else if (tPrintingType == "EODREPORT")
        //        {
        //            saveExceldialog();
        //            if (saveExcelfilename != string.Empty)
        //            {
        //                saveExcelfilename.Replace(".xls", "");
        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    string SaveFile = saveExcelfilename.Replace(".xls", "");
        //                    wb.Worksheets.Add(dt_griddiaplay1, "Customers");
        //                    wb.SaveAs(SaveFile + ".xlsx");

        //                }
        //            }
        //        }
        //        else if (tPrintingType == "ITEMTREPORT")
        //        {
        //            saveExceldialog();

        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                if (saveExcelfilename != string.Empty)
        //                {
        //                    string SaveFile = saveExcelfilename.Replace(".xls", "");
        //                    wb.Worksheets.Add(dt_selecttable, "Customers");

        //                    wb.SaveAs(SaveFile + ".xlsx");
        //                    //wb.SaveAs(folderPath + "DataGridViewExport.xlsx");
        //                }
        //            }

        //        }

        //        else if (tPrintingType == "EXCEL")
        //        {

        //            try
        //            {
        //                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
        //                if (dtPDF.Rows.Count > 0)
        //                {
        //                    saveExceldialog();

        //                    DataColumnCollection dcCollection = dtPDF.Columns;
        //                    // Export Data into EXCEL Sheet

        //                    ExcelApp.Application.Workbooks.Add(Type.Missing);
        //                    // ExcelApp.Cells.CopyFromRecordset(objRS);
        //                    for (int i = 1; i < dtPDF.Rows.Count + 1; i++)
        //                    {
        //                        for (int j = 1; j < dtPDF.Columns.Count + 1; j++)
        //                        {
        //                            if (i == 1)
        //                                ExcelApp.Cells[i, j] = dcCollection[j - 1].ToString();
        //                            else
        //                                ExcelApp.Cells[i, j] = dtPDF.Rows[i - 1][j - 1].ToString();
        //                        }
        //                    }
        //                    ExcelApp.ActiveWorkbook.SaveCopyAs(saveExcelfilename);
        //                    ExcelApp.ActiveWorkbook.Saved = true;
        //                    ExcelApp.Quit();

        //                }
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                //for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
        //                //{
        //                //    if (dtPrint.Rows[i8]["Describ"].ToString() == "Enable This Device*")
        //                //    {
        //                if (_Class.clsVariables.tempGEnableThisDevice == "Yes")
        //                {
        //                    tPrinterType = "Receipt";
        //                    //  break;
        //                }

        //                //    }
        //                //}
        //                int tNoPrint = 0;

        //                DataRow[] dtRowChk = dtPrint.Select("Describ='Printer Name*'");
        //                //for (int i8 = 0; i8 < dtPrint.Rows.Count - 1; i8++)
        //                {
        //                    //   if (dtPrint.Rows[i8]["Describ"].ToString() == "Printer Name*")
        //                    {
        //                        if (tPrinterType == "Receipt")
        //                        {
        //                            DataTable dtPrinter = new DataTable();
        //                            dtPrinter.Rows.Clear();
        //                            SqlDataAdapter adpPrinter = new SqlDataAdapter("select * from CrystalReportPrinterList", con);
        //                            adpPrinter.Fill(dtPrinter);
        //                            bool isChkPrinter = false;
        //                            for (int i = 0; i < dtPrinter.Rows.Count; i++)
        //                            {
        //                                string printerName = dtPrinter.Rows[i]["PrinterName"].ToString();
        //                                isChkPrinter = false;
        //                                if (dtRowChk[0]["Property"].ToString().ToUpper() == printerName.ToUpper())
        //                                {
        //                                    isChkPrinter = true;
        //                                    //rptReceiptReport rpt = new rptReceiptReport();
        //                                    //RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), @"D:\Parthi\" + mainStr + ".pdf");
        //                                    //// RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, @"C:\Test\Sample_PDF_Print.pdf");

        //                                    //CrystalDecisions.CrystalReports.Engine.TextObject str1 = ((CrystalDecisions.CrystalReports.Engine.TextObject)rpt.Section2.ReportObjects["Text1"]);
        //                                    //str1.Text = mainStr;
        //                                    //rpt.PrintToPrinter(0, true, 1, 0);
        //                                    reportViewerSales.LocalReport.ReportEmbeddedResource = "SalesProject.ReportFile.rdlcReceipt.rdlc";
        //                                    ReportParameter rpReportOn = new ReportParameter("ReceiptValue", Convert.ToString(mainStr), false);
        //                                    this.reportViewerSales.LocalReport.SetParameters(new ReportParameter[] { rpReportOn });
        //                                    reportViewerSales.RefreshReport();
        //                                    reportViewerSales.RenderingComplete += new RenderingCompleteEventHandler(PrintSales1);

        //                                    break;
        //                                }
        //                            }

        //                            if (isChkPrinter == false)
        //                            {
        //                                //for (int k = 0; k < dtPrint.Rows.Count - 1; k++)
        //                                //{
        //                                //    if (dtPrint.Rows[k]["Describ"].ToString() == "Print Copies*")
        //                                //    {
        //                                topLine5 = _Class.clsVariables.tempGPrintCopies;

        //                                tNoPrint = 1;

        //                                if (topLine5 == "No Copies")
        //                                {
        //                                    tNoPrint = 0;
        //                                }

        //                                for (int i2 = 0; i2 < tNoPrint; i2++)
        //                                {
        //                                    // RawPrinterHelper.SendStringToPrinter(dtRowChk[0]["Property"].ToString(), mainStr);
        //                                    Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtRowChk[0]["Property"].ToString(), mainStr));
        //                                    workerThread.Start();
        //                                    bool finished = workerThread.Join(3000);
        //                                    if (!finished)
        //                                    {
        //                                        workerThread.Abort();
        //                                    }
        //                                    // string s = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 29, 86, 66, 0, 27, 112, 0, 64, 240 });// device-dependent string, need a FormFeed?
        //                                    // RawPrinterHelper.SendStringToPrinter(dtPrint.Rows[i8]["Property"].ToString(), s);
        //                                }


        //                                DataTable dtNew = new DataTable();
        //                                dtNew.Rows.Clear();
        //                                SqlCommand cmdDrawer = new SqlCommand("Select * from CashDrawerSetting_table where counter=@tCounter", con);
        //                                cmdDrawer.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
        //                                SqlDataAdapter adp = new SqlDataAdapter(cmdDrawer);
        //                                adp.Fill(dtNew);
        //                                if (dtNew.Rows.Count > 0)
        //                                {
        //                                    //  if (dtNew.Rows[0]["Enable"].ToString().Trim() == "Yes")
        //                                    {
        //                                        //PrintDialog pd = new PrintDialog();
        //                                        string s;

        //                                        string[] byteStrings = dtNew.Rows[0]["PaperCut"].ToString().Split(',');

        //                                        byteOut = new byte[byteStrings.Length];

        //                                        for (int i = 0; i < byteStrings.Length; i++)
        //                                        {

        //                                            byteOut[i] = Convert.ToByte(byteStrings[i]);

        //                                        }

        //                                        s = System.Text.ASCIIEncoding.ASCII.GetString(byteOut);// device-dependent string, need a FormFeed?

        //                                        //  RawPrinterHelper.SendStringToPrinter(dtRowChk[0]["Property"].ToString(), s);

        //                                        Thread workerThread = new Thread(() => RawPrinterHelper.SendStringToPrinter(dtRowChk[0]["Property"].ToString(), s));
        //                                        workerThread.Start();
        //                                        bool finished = workerThread.Join(3000);
        //                                        if (!finished)
        //                                        {
        //                                            workerThread.Abort();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                MyMessageBox.ShowBox(ex.ToString(), "Warning");
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MyMessageBox.ShowBox(ex.ToString(), "Warning");
        //    }
        //}

        public void PrintSales1(object sender, RenderingCompleteEventArgs e)
        {
            try
            {
                //reportViewerSales.PrinterSettings.PrinterName = _Class.clsVariables.tPrinterName;
                //  reportViewerSales.PrinterSettings.PrintToFile = true;                
                reportViewerSales.PrintDialog();
                reportViewerSales.Clear();
                reportViewerSales.LocalReport.ReleaseSandboxAppDomain();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        Microsoft.Reporting.WinForms.ReportViewer reportViewerSales = new Microsoft.Reporting.WinForms.ReportViewer();
    }
}
