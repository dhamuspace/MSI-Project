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
    public partial class frmHouseAccount : Window
    {
        public frmHouseAccount()
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
                if (tKeyType == "Code")
                {
                    txtCustomerCode.Focus();
                    Button btn = (Button)sender;

                    if (txtCustomerCode.Text != "")
                    {
                        temp = txtCustomerCode.Text;
                        if (btn.Content.ToString().Trim() != ".")
                        {
                            txtCustomerCode.Text = "";
                            txtCustomerCode.Text = temp + btn.Content.ToString();
                        }
                        else
                        {
                           // if (temp.IndexOf('.') == -1)
                            {
                                txtCustomerCode.Text = "";
                                txtCustomerCode.Text = temp + btn.Content.ToString();
                            }                            
                        }
                    }
                    if (txtCustomerCode.Text == "")
                    {
                        txtCustomerCode.Text = btn.Content.ToString();
                    }
                    // _Class.clsVariables.DiscountType = "Amount";
                 //   int tCurPos = txtCustomerCode.SelectionStart;
                    txtCustomerCode.Select(txtCustomerCode.Text.Length, 0);
                }
                else
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
                   // int tCurPos = txtCustomerCode.SelectionStart;
                    txtEnterValue.Select(txtEnterValue.Text.Length, 0); 
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
                SalesProject._Class.clsVariables.tHouseACCustomerName = "";
            }
            catch(Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (tKeyType == "Code")
            {
                txtCustomerCode.Text = string.Empty;
            }
            else
            {
                txtEnterValue.Text = string.Empty;
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
        
        public string txtCardName;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtEnterValue.Text =string.Format("{0:0.00}",SalesProject._Class.clsVariables.tHouseACAmt);
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select distinct(SUBSTRING(LTrim(UPPER(Ledger_name)),0,2)) as Card_Name from Ledger_table where Ledger_groupno=32 and Ledger_no<>2", con);
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                string tLoadLetter = "";
                pnlCreditCardHolderList.Children.Clear();
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
                    newBtn.Width = 105;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                   // newBtn.Style = this.Resources["btnGroup"] as Style;
                    //Every Button Name Calles As newBtnGroup_Click
                    newBtn.Click += new RoutedEventHandler(newBtnGroupChar_Click);
                    //newBtn.Style = "btnnoborder";
                    //newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                    pnlCreditCardHolderList.Children.Add(newBtn);
                    pnlCreditCardHolderList.Height = (i * 65) + 50;
                  
                }
                if (tLoadLetter != "")
                {
                    funlLoadListAll();
                }
                con.Close();
              txtCustomerCode.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }           
        }
        public void funlLoadListAll()
        {
            try
            {
                txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tHouseACAmt);
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 order by Ledger_name ASC", con);
              //  cmd.Parameters.AddWithValue("@tStart", tStartLetter + "%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlCreditCardList.Children.Clear();
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
                    newBtn.Width = 200;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    pnlCreditCardList.Children.Add(newBtn);
                    pnlCreditCardList.Height = (i * 65) + 50;

                }
                con.Close();
               txtCustomerCode.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        public void funlLoadList(string tStartLetter)
        {
            try
            {
                
                txtEnterValue.Text = string.Format("{0:0.00}", SalesProject._Class.clsVariables.tHouseACAmt);
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                SqlCommand cmd = new SqlCommand("select Ledger_name as Card_Name from Ledger_table where  Ledger_groupno=32 and Ledger_no<>2 and Ledger_name like @tStart order by Ledger_name ASC", con);
                cmd.Parameters.AddWithValue("@tStart",tStartLetter+"%");
                dr = cmd.ExecuteReader();
                dtNew.Load(dr);
                int i = 0;
                pnlCreditCardList.Children.Clear();
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
                    newBtn.Width = 200;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    pnlCreditCardList.Children.Add(newBtn);
                    pnlCreditCardList.Height = (i * 65) + 50;

                }
                con.Close();
                txtCustomerCode.Focus();
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
               
                Button clickedButton = (Button)sender;

                _Class.clsVariables.funControlSetting();
                DataTable dtLedgerDetails=new DataTable();
                dtLedgerDetails.Rows.Clear();
                SqlCommand cmdLedgerDetails = new SqlCommand("Select * from Ledger_table where Ledger_name=@tLedgerName and Ledger_groupno=32", con);
                cmdLedgerDetails.Parameters.AddWithValue("@tLedgerName",clickedButton.Content.ToString());
                SqlDataAdapter adpLedgerDetails = new SqlDataAdapter(cmdLedgerDetails);
                adpLedgerDetails.Fill(dtLedgerDetails);
                double tLimitAmount = 0.00;
                double tLimitDays = 0.00;
                double tLimitBill = 0.00;
                double tCLimitAmount = 0.00;
                double tPLimitAmount = 0.00;
                double tCLimitDays = 0.00;
                double tCLimitBill = 0.00;
                double tbillamt = 0.00;
                string strLedgerno="";
                if (dtLedgerDetails.Rows.Count > 0)
                {
                    string tRun = "";
                    strLedgerno = dtLedgerDetails.Rows[0]["Ledger_no"].ToString();
                    tLimitAmount = (dtLedgerDetails.Rows[0]["Limit_Amount"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Amount"].ToString().Trim());
                    tLimitDays = (dtLedgerDetails.Rows[0]["Limit_Days"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Days"].ToString().Trim());
                    tLimitBill = (dtLedgerDetails.Rows[0]["Limit_Bills"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["Limit_Bills"].ToString().Trim());
                    tCLimitAmount = (dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Amount"].ToString().Trim());
                    tPLimitAmount = (dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["PLimit_Amount"].ToString().Trim());
                    tCLimitBill = (dtLedgerDetails.Rows[0]["CLimit_Bills"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Bills"].ToString().Trim());
                    tCLimitDays = (dtLedgerDetails.Rows[0]["CLimit_Days"].ToString().Trim() == "") ? 0.00 : double.Parse(dtLedgerDetails.Rows[0]["CLimit_Days"].ToString().Trim());
                    
                    //muniescode
                    tCLimitBill = tCLimitBill + 1;
                    tbillamt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
                    tbillamt = tbillamt + tCLimitAmount;
                    //muniescode end
                    
                    tCLimitAmount += (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());

                    tCLimitAmount = tLimitAmount +(tPLimitAmount-tCLimitAmount);
                 //   tCLimitBill = tLimitBill - tCLimitBill;
                    tCLimitDays = tLimitDays - tCLimitDays;                   
                    if (_Class.clsVariables.tCtrCreditLimit == "3")
                    {
                        if (tCLimitAmount <= 0 && tLimitAmount > 0)
                        {
                            tRun = "";
                            MyMessageBox.ShowBox("Excess Credit Limit- Amount", "Warning");
                        }
                        else if (tCLimitBill > tLimitBill && tLimitBill!=0 )
                        {
                            tRun = "";
                            MyMessageBox.ShowBox("Excess Credit Limit- Bill", "Warning");
                        }
                        else if (tCLimitDays <= 0 && tLimitDays > 0)
                        {
                            tRun = "";
                            MyMessageBox.ShowBox("Excess Credit Limit- Days", "Warning");
                        }
                        else if (tLimitAmount == 0 && tLimitBill == 0 && tLimitDays == 0)
                        {
                            tRun = "";
                            MyMessageBox.ShowBox("Excess Credit Limit Details Not Found", "Warning");
                        }
                        else
                        {
                            tRun = "RUN";
                        }
                        
                    }
                    else if (_Class.clsVariables.tCtrCreditLimit == "2")
                    {
                        if (tCLimitAmount <= 0 && tLimitAmount > 0)
                        {
                            MyMessageBox.ShowBox("Excess Credit Limit- Amount", "Warning");
                        }
                        else if (tCLimitBill > tLimitBill && tLimitBill != 0)
                        {
                            MyMessageBox.ShowBox("Excess Credit Limit- Bill", "Warning");
                        }
                        else if (tCLimitDays <= 0 && tLimitDays > 0)
                        {
                            MyMessageBox.ShowBox("Excess Credit Limit- Days", "Warning");
                        }
                        else if (tLimitAmount == 0 && tLimitBill == 0 && tLimitDays==0)
                        {
                            MyMessageBox.ShowBox("Excess Credit Limit Details Not Found", "Warning");
                        }
                        tRun = "RUN";
                    }
                    else
                    {
                        tRun = "RUN";
                    }
                    if (tRun == "RUN")
                    {
                        //Item Value Zero
                        //if (((txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim())) > 0)
                        {
                            //Getting Click Button Name values here:

                            //munies code
                          
                            //tPLimitAmount= tPLimitAmount+
                            funConnectionStateCheck();



                            SqlCommand cmdupdate = new SqlCommand("update Ledger_table set CLimit_Amount='" + tbillamt + "',CLimit_Bills='" + tCLimitBill + "' where Ledger_no='" + strLedgerno + "' and Ledger_groupno=32", con);
                            cmdupdate.ExecuteNonQuery();

                            SalesProject._Class.clsVariables.tHouseACCustomerName = "";
                            SalesProject._Class.clsVariables.tHouseACCustomerName = clickedButton.Content.ToString();
                            lblCustomerName.Content = clickedButton.Content.ToString();
                            SalesProject._Class.clsVariables.tHouseACAmt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
                            this.Close();
                        }
                        //else
                        //{
                        //    MyMessageBox.ShowBox("You should enter settle amount", "Warning");
                        //    txtCustomerCode.Focus();
                        //}
                    }
                }
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
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
            //try
            //{
            //    if (txtEnterValue.Text.Trim() != "")
            //    {
            //        //Getting Click Button Name values here:
            //        Button clickedButton = (Button)sender;
            //        SalesProject._Class.clsVariables.tCreditCardName = "";
            //        SalesProject._Class.clsVariables.tCreditCardName = clickedButton.Content.ToString();
            //        SalesProject._Class.clsVariables.tCreditCardAmt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
            //        this.Close();
            //    }
            //    else
            //    {
            //        MyMessageBox.ShowBox("You should enter settle amount","Warning");
            //        txtEnterValue.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{ }
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

        string tKeyType = "Amount";
        private void txtCustomerCode_GotFocus(object sender, RoutedEventArgs e)
        {
            tKeyType = "Code";
        }

        private void txtEnterValue_GotFocus(object sender, RoutedEventArgs e)
        {
            tKeyType="Amount";
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                funCodeEnter();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }

        }

        public void funCodeEnter()
        {
            try
            {
                if (tKeyType == "Code")
                {
                    if (txtCustomerCode.Text.Trim() != "")
                    {
                        if (((txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim())) > 0)
                        {
                            DataTable dtNew = new DataTable();
                            dtNew.Rows.Clear();
                            SqlCommand cmd = new SqlCommand("Select * from Ledger_table where Ledger_Code<>0 and Ledger_Code=@tCustomerCode and Ledger_groupno=32", con);
                            cmd.Parameters.AddWithValue("@tCustomerCode", txtCustomerCode.Text.Trim());
                            SqlDataAdapter adp = new SqlDataAdapter(cmd);
                            adp.Fill(dtNew);


                            double tLimitAmount = 0.00;
                            double tLimitDays = 0.00;
                            double tLimitBill = 0.00;
                            double tCLimitAmount = 0.00;
                            double tPLimitAmount = 0.00;
                            double tCLimitDays = 0.00;
                            double tCLimitBill = 0.00;
                            if (dtNew.Rows.Count > 0)
                            {
                                string tRun = "";
                                tLimitAmount = (dtNew.Rows[0]["Limit_Amount"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["Limit_Amount"].ToString().Trim());
                                tLimitDays = (dtNew.Rows[0]["Limit_Days"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["Limit_Days"].ToString().Trim());
                                tLimitBill = (dtNew.Rows[0]["Limit_Bills"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["Limit_Bills"].ToString().Trim());
                                tCLimitAmount = (dtNew.Rows[0]["CLimit_Amount"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["CLimit_Amount"].ToString().Trim());
                                tPLimitAmount = (dtNew.Rows[0]["PLimit_Amount"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["PLimit_Amount"].ToString().Trim());
                                tCLimitBill = (dtNew.Rows[0]["CLimit_Bills"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["CLimit_Bills"].ToString().Trim());
                                tCLimitDays = (dtNew.Rows[0]["CLimit_Days"].ToString().Trim() == "") ? 0.00 : double.Parse(dtNew.Rows[0]["CLimit_Days"].ToString().Trim());


                                tCLimitAmount += (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());

                                tCLimitAmount = tLimitAmount + (tPLimitAmount - tCLimitAmount);
                                //   tCLimitBill = tLimitBill - tCLimitBill;
                                tCLimitDays = tLimitDays - tCLimitDays;
                                if (_Class.clsVariables.tCtrCreditLimit == "3")
                                {
                                    if (tCLimitAmount <= 0 && tLimitAmount > 0)
                                    {
                                        tRun = "";
                                        MyMessageBox.ShowBox("Excess Credit Limit- Amount", "Warning");
                                    }
                                    else if (tCLimitBill >= tLimitBill && tLimitBill != 0)
                                    {
                                        tRun = "";
                                        MyMessageBox.ShowBox("Excess Credit Limit- Bill", "Warning");
                                    }
                                    else if (tCLimitDays <= 0 && tLimitDays > 0)
                                    {
                                        tRun = "";
                                        MyMessageBox.ShowBox("Excess Credit Limit- Days", "Warning");
                                    }
                                    else if (tLimitAmount == 0 && tLimitBill == 0 && tLimitDays == 0)
                                    {
                                        tRun = "";
                                        MyMessageBox.ShowBox("Excess Credit Limit Details Not Found", "Warning");
                                    }
                                    else
                                    {
                                        tRun = "RUN";
                                    }

                                }
                                else if (_Class.clsVariables.tCtrCreditLimit == "2")
                                {
                                    if (tCLimitAmount <= 0 && tLimitAmount > 0)
                                    {
                                        MyMessageBox.ShowBox("Excess Credit Limit- Amount", "Warning");
                                    }
                                    else if (tCLimitBill > tLimitBill && tLimitBill != 0)
                                    {
                                        MyMessageBox.ShowBox("Excess Credit Limit- Bill", "Warning");
                                    }
                                    else if (tCLimitDays <= 0 && tLimitDays > 0)
                                    {
                                        MyMessageBox.ShowBox("Excess Credit Limit- Days", "Warning");
                                    }
                                    else if (tLimitAmount == 0 && tLimitBill == 0 && tLimitDays == 0)
                                    {
                                        MyMessageBox.ShowBox("Excess Credit Limit Details Not Found", "Warning");
                                    }
                                    tRun = "RUN";
                                }
                                else
                                {
                                    tRun = "RUN";
                                }
                                if (tRun == "RUN")
                                {
                                    //Item value zero
                                    //if (((txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim())) > 0)
                                    {
                                        //Getting Click Button Name values here:

                                        SalesProject._Class.clsVariables.tHouseACCustomerName = "";
                                        SalesProject._Class.clsVariables.tHouseACCustomerName = dtNew.Rows[0]["Ledger_name"].ToString();
                                        lblCustomerName.Content = dtNew.Rows[0]["Ledger_name"].ToString();
                                        SalesProject._Class.clsVariables.tHouseACAmt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
                                        this.Close();                                       
                                    }
                                    //else
                                    //{
                                    //    MyMessageBox.ShowBox("You should enter settle amount", "Warning");
                                    //    txtCustomerCode.Focus();
                                    //}
                                }


                                //if (dtNew.Rows.Count > 0)
                                //{
                                //    SalesProject._Class.clsVariables.tHouseACCustomerName = "";
                                //    SalesProject._Class.clsVariables.tHouseACCustomerName = dtNew.Rows[0]["Ledger_name"].ToString();
                                //    lblCustomerName.Content = dtNew.Rows[0]["Ledger_name"].ToString();
                                //    SalesProject._Class.clsVariables.tHouseACAmt = (txtEnterValue.Text.Trim() == "") ? 0.00 : double.Parse(txtEnterValue.Text.Trim());
                                //    this.Close();
                            }
                            else
                            {
                                MyMessageBox.ShowBox("Customer code not found", "Warning");
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Enter Amount", "Warning");
                            txtEnterValue.Focus();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Select House Account Name", "Warning");
                    }

                }
                else
                {
                    MyMessageBox.ShowBox("Select House Account Name", "Warning");
                    // this.Close();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }
        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                funCodeEnter();
            }
        }

        
        private void txtCustomerCode_TextChanged(object sender, TextChangedEventArgs e)
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
                    if (Char.IsDigit(c) || Char.IsControl(c) )
                    {
                        newText += c;                        
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
