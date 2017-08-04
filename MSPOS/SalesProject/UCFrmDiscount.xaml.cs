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
using System.Text.RegularExpressions;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for FrmDiscount.xaml
    /// </summary>
    /// 
    public delegate void UCFRMDiscountEvent(object sender,RoutedEventArgs e);
    public partial class UCFrmDiscount : UserControl
    {
        public UCFrmDiscount()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        public event UCFRMDiscountEvent UCFRMDiscountEventEnterClick;
        public event UCFRMDiscountEvent UCFRMDiscountEventCloseClick;
        
        private string DiscountAmt;
        public string StNetAmt;
        
        
        public string Disc
        {
            get { return DiscountAmt; }
            set { DiscountAmt = value; }
        }
        //public string tNetAmt
        //{
        //    get
        //    {
        //        return StNetAmt;
        //    }
        //    set
        //    {
        //        StNetAmt = value;
        //    }
        //}
        
      //  SqlDataReader dr = null;
      //  public event System.EventHandler DiscountCreationEventHandler;
       
        string temp;
        
        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            //txtEnterDiscountValue.Focus();
            //Button btn = (Button)sender;
            //if (txtEnterDiscountValue.Text != "")
            //{
            //    temp = txtEnterDiscountValue.Text;
            //    if (btn.Content.ToString().Trim() != ".")
            //    {
            //        txtEnterDiscountValue.Text = "";
            //        txtEnterDiscountValue.Text = temp + btn.Content.ToString();
            //    }
            //    else
            //    {
            //        if (temp.IndexOf('.') == -1)
            //        {
            //            txtEnterDiscountValue.Text = "";
            //            txtEnterDiscountValue.Text = temp + btn.Content.ToString();
            //        }
            //    }
            //}
            //if (txtEnterDiscountValue.Text == "")
            //{
            //    txtEnterDiscountValue.Text = btn.Content.ToString();
            //}
            //_Class.clsVariables.DiscountType = "Amount";
            //txtEnterDiscountValue.Select(txtEnterDiscountValue.Text.Length, 0);

            txtEnterDiscountValue.Focus();
            Button btn = (Button)sender;
            if (txtEnterDiscountValue.Text != "")
            {
                temp = txtEnterDiscountValue.Text;
                if (btn.Content.ToString().Trim() != ".")
                {
                    txtEnterDiscountValue.Text = "";
                    txtEnterDiscountValue.Text = temp + btn.Content.ToString();
                }
                else
                {
                    if (temp.IndexOf('.') == -1)
                    {
                        txtEnterDiscountValue.Text = "";
                        txtEnterDiscountValue.Text = temp + btn.Content.ToString();
                    }
                }
            }
            if (txtEnterDiscountValue.Text == "")
            {
                if (btn.Content.ToString().Trim() == ".")
                {
                    txtEnterDiscountValue.Text = "0" + btn.Content.ToString();
                }
                else
                {
                    txtEnterDiscountValue.Text = btn.Content.ToString();
                }
            }
            _Class.clsVariables.DiscountType = "Amount";
            txtEnterDiscountValue.Select(txtEnterDiscountValue.Text.Length, 0);
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
           // DiscountAmt = txtEnterDiscountValue.Text.ToString();
            //if (DiscountAmt.Trim() == "")
            //{
            //    DiscountAmt = "0.00";
            //}
            if (UCFRMDiscountEventCloseClick != null)
            {
                txtEnterDiscountValue.Text = string.Empty;
                UCFRMDiscountEventCloseClick(sender,e);
            }
           // this.Close();
            //if (txtEnterValue.Text.Length > 0)
            //{
            //    temp = txtEnterValue.Text;
            //    txtEnterValue.Text = temp.Remove(temp.Length - 1);
            //}
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEnterDiscountValue.Text = string.Empty;
        }
      //  double tEnterAmt=0.00;
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEnterDiscountValue.Text))
                {
                    con.Close();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    DataTable dtNew = new DataTable();
                    dtNew.Rows.Clear();
                    SqlCommand cmdDiscount = new SqlCommand("Select * from user_table where user_no=@tUserNo", con);
                    //  cmdDiscount.Parameters.AddWithValue("@tUserNo", 1);
                    cmdDiscount.Parameters.AddWithValue("@tUserNo", _Class.clsVariables.tUserNo);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdDiscount);
                    adp.Fill(dtNew);
                    double tDiscountRange = 0;
                    bool isChkexec = false;
                    if (dtNew.Rows.Count > 0)
                    {
                        isChkexec = true;
                        tDiscountRange = string.IsNullOrEmpty(Convert.ToString(dtNew.Rows[0]["DiscountRange"])) ? 0 : Convert.ToDouble(Convert.ToString(dtNew.Rows[0]["DiscountRange"]));
                        if (_Class.clsVariables.tDiscountAction.ToUpper() == "MAIN")
                        {
                            if ((tDiscountRange == 100) && (Convert.ToDouble(Convert.ToString(txtEnterDiscountValue.Text)) == Convert.ToDouble(tAmount)))
                            {
                                isChkexec = false;
                            }
                        }
                    }
                    if (isChkexec == true)
                    {
                        double tDiscountValueNew = (tAmount == "") ? 0 : (Convert.ToDouble(tAmount) * (tDiscountRange / 100));
                        if (!string.IsNullOrEmpty(txtEnterDiscountValue.Text) && tDiscountValueNew >= ((txtEnterDiscountValue.Text.Trim() == "") ? 0 : Convert.ToDouble(txtEnterDiscountValue.Text.ToString())))
                        {
                            if (txtEnterDiscountValue.Text.Trim() == "")
                            {
                                //   tEnterAmt = 0.00;
                                DiscountAmt = txtEnterDiscountValue.Text.ToString();
                                //this.Close();
                                if (UCFRMDiscountEventEnterClick != null)
                                {
                                    UCFRMDiscountEventEnterClick(sender, e);
                                }

                            }
                            else
                            {


                                //if (Convert.ToDouble(tAmount) > Convert.ToDouble(Convert.ToString(txtEnterDiscountValue.Text)))//Parthi
                                if (Convert.ToDouble(tAmount) >= Convert.ToDouble(Convert.ToString(txtEnterDiscountValue.Text)))//Now Changed For discount fully
                                {
                                    //if (!string.IsNullOrEmpty(_Class.clsVariables.tSNetAmt) && Convert.ToDouble(_Class.clsVariables.tSNetAmt) > 0)
                                    {
                                        DiscountAmt = txtEnterDiscountValue.Text.ToString();
                                        //this.Close();
                                        if (UCFRMDiscountEventEnterClick != null)
                                        {
                                            UCFRMDiscountEventEnterClick(sender, e);

                                        }
                                    }
                                    //else
                                    //{
                                    //    MyMessageBox.ShowBox("Discount Amount not valid.", "Warning");
                                    //}
                                }
                                else
                                {
                                    MyMessageBox.ShowBox("Discount Amount not valid.", "Warning");
                                }
                                //if (DiscountCreationEventHandler != null)
                                //{
                                //    frm.recDiscount = txtEnterValue.Text.ToString();
                                //    DiscountCreationEventHandler(sender,e);
                                //} 
                                //  frm.recDiscount = Discount1;
                            }
                        }
                        else
                        {
                            MyMessageBox.ShowBox("Enter Valid Discount Amount", "Warning");
                        }
                    }
                    else
                    {
                        //Group Discount Warning
                        MyMessageBox.ShowBox("Enter Valid Discount Amount", "Warning");
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        public string tAmount;
        public string tItemDisAmount;
        string recDiscount1;
        public string Discount1
        {
            get { return recDiscount1; }
            set { recDiscount1 = value; }  
        }

       // string recDiscount1;
        public string DiscountName
        {
            get { return tempDiscName; }
            set { tempDiscName = value; }
        }
        public void funConnectionStateCheck()
        {
            con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        public string tempDiscName = null;
        public string textDiscount;

        public void funLoadDiscount()
        {
            try
            {
                if (_Class.clsVariables.tDiscountLedger == "1")
                {
                    txtEnterDiscountValue.Text = (string.IsNullOrEmpty(Convert.ToString(tItemDisAmount))) ? "" : string.Format("{0:0.00}", Convert.ToString(tItemDisAmount));
                }
                _Class.clsVariables.DiscountType = "Amount";
                funConnectionStateCheck();
                DataTable dtNew = new DataTable();
                dtNew.Rows.Clear();
                // SqlCommand cmd = new SqlCommand(" select Calculation,DiscountName,Amount,convert(varchar,EndDate,103) as EndDate from DiscountSetting_Table where Enddate>(Select EndofDay from EndOfday_table where id=(Select max(id) from EndOfDay))", con);
                SqlCommand cmd = new SqlCommand(@"DECLARE @tQuary4 VARCHAR(max)='Yes';
	DECLARE @tQuary2 VARCHAR(max);
	DECLARE @tQuary1 VARCHAR(max)='select Calculation,DiscountName,Amount,convert(Datetime,EndDate,103) as EndDate from DiscountSetting_Table where ';
	DECLARE @tQuary3 VARCHAR(max)=' AND Enddate>(Select convert(Datetime,EndofDay,108) from EndOfday_table where id=(Select max(id) from EndOfDay_Table))'; 
	select @tQuary4= @tQuary1+(CASE(datename(dw,DATEADD(day,1,EndOfDay)))
	 when 'Monday' THEN 'Monday='
	 when 'Tuesday' THEN 'Tuesday='
	 when 'Wednesday' THEN 'Wednessday='
	 when 'Thursday' THEN 'Thursday='
	 when 'Friday' THEN 'Friday='
	 when 'Saturday' THEN 'Saturday='
	 Else 'Sunday=' End)+'''Yes'''+@tQuary3 from EndOfDay_Table  where id=(Select max(id) from EndOfDay_Table)	
    -- Insert statements for procedure here
	Execute (@tQuary4);", con);
                SqlDataAdapter adpCmd = new SqlDataAdapter(cmd);
                adpCmd.Fill(dtNew);
              //  cmd.CommandType = CommandType.StoredProcedure;
                //dr = cmd.ExecuteReader();
               // dtNew.Load(dr);
                int i = 0;
                pnlDiscountList1.UCPnlItemDisplay.Children.Clear();
                for (int mn = 0; mn < dtNew.Rows.Count; mn++)
                {
                    i += 1;
                    Button newBtn = new Button();
                    TextBlock textBlock = new TextBlock();
                    textBlock.Inlines.Add(dtNew.Rows[mn]["DiscountName"].ToString());
                    textDiscount = "";
                    textDiscount = dtNew.Rows[mn]["DiscountName"].ToString() + "\n";
                    tempDiscName = "";
                    //     tempDiscName = dr["DiscountName"].ToString();
                    textBlock.Inlines.Add(new LineBreak());
                    if (dtNew.Rows[mn]["Calculation"].ToString() == "Fixed")
                    {
                        textBlock.Inlines.Add("FIXED $" + dtNew.Rows[mn]["Amount"].ToString());
                        textDiscount += "FIXED $" + dtNew.Rows[mn]["Amount"].ToString() + "\n";
                    }
                    if (dtNew.Rows[mn]["Calculation"].ToString() == "Percentage")
                    {
                        textBlock.Inlines.Add(dtNew.Rows[mn]["Amount"].ToString() + " PERCENT OFF");
                        textDiscount += dtNew.Rows[mn]["Amount"].ToString() + " PERCENT OFF" + "\n";
                    }
                    textBlock.Inlines.Add(new LineBreak());

                    textBlock.Inlines.Add("EXPIRES " + Convert.ToDateTime(dtNew.Rows[mn]["EndDate"].ToString()).ToString("yyyy-MM-dd"));
                    textDiscount += "EXPIRES " + Convert.ToDateTime(dtNew.Rows[mn]["EndDate"].ToString()).ToShortDateString();
                    textBlock.FontSize = 14;
                    //  newBtn.Content = textBlock;
                    newBtn.Content = textDiscount;
                    newBtn.HorizontalContentAlignment = HorizontalAlignment.Left;
                    newBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    newBtn.VerticalAlignment = VerticalAlignment.Center;
                    //  newBtn.Content = dr["DiscountName"].ToString();
                    newBtn.Name = "Discount" + i;
                    // newBtn.Name ="Discount"+i+dr["DiscountName"].ToString();
                    newBtn.Width = 180;
                    newBtn.Height = 65;
                    newBtn.Margin = new Thickness(1, 1, 1, 1);
                    newBtn.Style = this.Resources["btnGroup"] as Style;
                    newBtn.Click += new RoutedEventHandler(newBtnGroup_Click);
                    // newBtn.Style = "btnnoborder";
                    // newBtn.Template = this.FindResource("btnnoborder") as ControlTemplate;
                    pnlDiscountList1.UCPnlItemDisplay.Children.Add(newBtn);
                 //   pnlDiscountList1.Height = (i * 65) + 50;
                    pnlDiscountList1.Height = 460;
                }
                // con.Close();
                txtEnterDiscountValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            funLoadDiscount();
        }
        private void newBtnGroup_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton=(Button)sender;
          // MessageBox.Show(clickedButton.Content.ToString());
      //   tempDiscName=clickedButton.Content.ToString().Substring(0,tempDiscName.IndexOf("\n"));
   //      MessageBox.Show(tempDiscName);

            DataTable dtDisc1 = new DataTable();
            dtDisc1.Rows.Clear();
            SqlCommand cmd = new SqlCommand(" select * from DiscountSetting_Table where DiscountName='"+clickedButton.Content.ToString().Substring(0,clickedButton.Content.ToString().IndexOf('\n') )+"'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dtDisc1);
          //  con.Close();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
           // dr = cmd.ExecuteReader();
          //  int i = 0;
          
            if(dtDisc1.Rows.Count>0)
          //  while (dr.Read())
            {
                if (dtDisc1.Rows[0]["Calculation"].ToString() == "Fixed")
                {
                    txtEnterDiscountValue.Text = dtDisc1.Rows[0]["Amount"].ToString();
                    _Class.clsVariables.DiscountType = "Amount";
                }
                if (dtDisc1.Rows[0]["Calculation"].ToString() == "Percentage")
                {
                    txtEnterDiscountValue.Text = Math.Round((Convert.ToDouble(Convert.ToString(tAmount)) * (Convert.ToDouble(Convert.ToString(dtDisc1.Rows[0]["Amount"])) / 100)), 2).ToString();
                    _Class.clsVariables.DiscountType = "Percent";
                }
            }
          //  con.Close();
            tempDiscName = null;
        }

        private void clickSVup(object sender, RoutedEventArgs e)
        {
           // svBtn.PageUp();
        }
        private void clickSVdn(object sender, RoutedEventArgs e)
        {
          //  svBtn.PageDown();            
        }
        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
        
        }

        //private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9.]+");
        //    e.Handled = regex.IsMatch(e.Text);
        //}

        private void txtEnterDiscountValue_TextChanged(object sender, TextChangedEventArgs e)
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
