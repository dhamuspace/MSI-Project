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
using WpfPageTransitions;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LCDDisplay
{
    /// <summary>
    /// Interaction logic for LCDDisplayScreen.xaml
    /// </summary>
    public partial class LCDDisplayScreen : Window
    {
        public LCDDisplayScreen()
        {
            InitializeComponent();
        }
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
         string tBillNo = ""; 
        protected void funPreviousBill()
        {

            try
            {
                DateTime tBillDate = new DateTime();
                DateTime tBillTime = new DateTime();                             
                double @tTotAmt = 0;
                double @tTotQty = 0;                
                double @Qty = 0;
                double @Amt = 0;                
                string tBillType = "";
                DataTable dtPrevBillMas = new DataTable();
                DataTable dtDetail = new DataTable();
                dtPrevBillMas.Rows.Clear();
               // SqlCommand cmd = new SqlCommand("Select max(smas_billNo) as BillNo from SalMas_table where smas_rtno=0 and smas_Cancel=0 ", con);
                SqlCommand cmdBillNo = new SqlCommand("select Smas_no,CONVERT(date,smas_billdate,108) as BillDate, CONVERT(time,smas_billtime,103)as BillTime,smas_billno, Smas_name from salmas_table where smas_billno=(Select max(smas_billNo) as BillNo from SalMas_table where smas_rtno=0 and smas_Cancel=0) and smas_rtno=0", con);
               // cmdBillNo.Parameters.AddWithValue("@tBillNo", (double.Parse(lblPreviosBillNo.Content.ToString())));
                SqlDataAdapter adpBillNo = new SqlDataAdapter(cmdBillNo);
                adpBillNo.Fill(dtPrevBillMas);
                if (dtPrevBillMas.Rows.Count > 0)
                {
                    tBillDate = DateTime.Parse(dtPrevBillMas.Rows[0]["BillDate"].ToString());
                    tBillTime = DateTime.Parse(dtPrevBillMas.Rows[0]["BillTime"].ToString());
                    tBillType = dtPrevBillMas.Rows[0]["Smas_name"].ToString();
                    double code = double.Parse(dtPrevBillMas.Rows[0]["smas_billno"].ToString());
                    if (code < 9)
                    {
                        tBillNo = ("00" + Convert.ToString(code));
                    }
                    else if (code < 99)
                    {
                        tBillNo = ("0" + Convert.ToString(code));
                    }
                    else
                    {
                        tBillNo = (Convert.ToString(code));
                    }
                    tCurrentBillNo = tBillNo;
                    dtDetail.Rows.Clear();
                    SqlCommand cmdBillDet = new SqlCommand(@"SELECT  dbo.Item_table.Item_name, dbo.stktrn_table.nt_qty,convert(numeric(18,2), dbo.stktrn_table.Rate),convert(numeric(18,2),dbo.stktrn_table.Amount)
                     FROM  dbo.stktrn_table INNER JOIN
                     dbo.Item_table ON dbo.stktrn_table.item_no = dbo.Item_table.Item_no where stktrn_table.strn_no=@tBillNo and strn_type=1", con);
                    cmdBillDet.Parameters.AddWithValue("@tBillNo", (double.Parse(dtPrevBillMas.Rows[0]["Smas_no"].ToString())));
                    SqlDataAdapter adpBillDet = new SqlDataAdapter(cmdBillDet);
                    adpBillDet.Fill(dtDetail);

                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        @Qty = 0;
                        @Amt = double.Parse(dtDetail.Rows[i][3].ToString());                     
                        if (dtDetail.Rows[i][1].ToString() != "")
                        {
                            @Qty = double.Parse(dtDetail.Rows[i][1].ToString());
                        }
                        @tTotQty = @tTotQty + @Qty;
                        @tTotAmt = @tTotAmt + @Amt;
                        lblTotQty.Content = @tTotQty;
                    }
                    gridItems.DataSource = dtDetail;
                    if (tLCDEnableRate.Trim() == "True" && tLCDEnableAmt.Trim()=="True")                    
                    {
                        lblGridTitle.Content = "Name                        Qty   Rate  Amt";
                        gridItems.Columns[0].Width = 190;
                        gridItems.Columns[1].Width = 50;
                        gridItems.Columns[2].Width = 50;
                        gridItems.Columns[3].Width = 70;
                        gridItems.Columns[2].Visible = true;
                        gridItems.Columns[3].Visible = true;
                    }
                    else if (tLCDEnableRate.Trim()!= "True" && tLCDEnableAmt.Trim() == "True")
                    {
                        lblGridTitle.Content = "Name                         Qty      Amt";
                        gridItems.Columns[0].Width = 210;
                        gridItems.Columns[1].Width = 70;
                        gridItems.Columns[2].Width = 50;
                        gridItems.Columns[2].Width = 70;
                        gridItems.Columns[2].Visible = false;
                        
                        gridItems.Columns[3].Visible = true;
                    }
                    else if (tLCDEnableRate.Trim() == "True" && tLCDEnableAmt.Trim() != "True")
                    {
                        lblGridTitle.Content = "Name                         Qty     Rate";
                        gridItems.Columns[0].Width = 210;
                        gridItems.Columns[1].Width = 70;
                        gridItems.Columns[2].Width = 70;
                        gridItems.Columns[3].Visible = false;
                        gridItems.Columns[2].Visible = true;
                        
                    }
                    else
                    {
                        lblGridTitle.Content = "Name                           Qty";
                        gridItems.Columns[0].Width = 250;
                        gridItems.Columns[1].Width = 100;
                        gridItems.Columns[2].Width = 50;
                        gridItems.Columns[2].Visible = false;
                        gridItems.Columns[3].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Warning");
            }
        }
        public string tChkCusDisEnable = "";
        public string tPreviousBillNo = "";
        public string tCurrentBillNo = "";
        public void funPreviousBillAmount()
        {
            try
            {
                //            SqlCommand cmd1 = new SqlCommand("select max(SalRecv_SalNo) from SalRecv_table", con);
                DataTable dsPrevious = new DataTable();
                dsPrevious.Clear();
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SP_PREVIOUSBILL", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                SqlParameter resultPreviousBill = new SqlParameter("@tRowCount", SqlDbType.Int);
                resultPreviousBill.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultPreviousBill);
                SqlParameter resultNetAmount = new SqlParameter("@tSmas_NetAmount", SqlDbType.Float);
                resultNetAmount.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultNetAmount);
                SqlParameter resultRcvdAmt = new SqlParameter("@tSmas_Rcvdamount", SqlDbType.Float);
                resultRcvdAmt.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultRcvdAmt);
                SqlParameter resultRefundAmt = new SqlParameter("@tRefundAmt", SqlDbType.Float);
                resultRefundAmt.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultRefundAmt);
                SqlParameter resultPreviousBill1 = new SqlParameter("@tRowCount1", SqlDbType.Int);
                resultPreviousBill1.Direction = ParameterDirection.Output;
                cmd1.Parameters.Add(resultPreviousBill1);
                cmd1.ExecuteNonQuery();
             //   lblPreviosBillNo.Content = (resultPreviousBill.Value.ToString().Trim() == "") ? "0" : resultPreviousBill.Value.ToString();
                lblBillAmt.Content = String.Format("{0:0.00}", (resultNetAmount.Value.ToString().Trim() == "") ? 0 : ((double)resultNetAmount.Value));
                lblTender.Content ="Tendered : "+ String.Format("{0:0.00}", (resultRcvdAmt.Value.ToString().Trim() == "") ? 0 : ((double)resultRcvdAmt.Value));
                lblRefund.Content ="Change Due : "+String.Format("{0:0.00}", (((resultRcvdAmt.Value.ToString().Trim() == "") ? 0 : ((double)resultRcvdAmt.Value)) - ((resultNetAmount.Value.ToString().Trim() == "") ? 0 : ((double)resultNetAmount.Value))));               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }
        int tTimerCount = 0;
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            tTimerCount++;
            if (tTimerCount == 3)
            {
                funPreviousBill();
                if (tPreviousBillNo != tCurrentBillNo)
                {
                    funPreviousBillAmount();
                }
            }
            if (tTimerCount == 10)
            {
                tTimerCount = 0;
              //  tempTimer.Stop();
                tRowCount--;
                if (tRowCount == -1)
                {
                    tRowCount = dtOffer.Rows.Count-1;
                }
               if (tRowCount>-1)
                {
                    int tMarqueeTxtLength = dtOffer.Rows[tRowCount][0].ToString().Length;
                    if (tMarqueeTxtLength > 40)
                    {
                        tbmarquee.FontSize = 14;
                    }
                    else
                    {
                        tbmarquee.FontSize = 20;
                    }
                    tbmarquee.Content = dtOffer.Rows[tRowCount][0].ToString();
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.From = -tbmarquee.ActualWidth;
                    doubleAnimation.To = canMain.ActualWidth;
                   // doubleAnimation.RepeatBehavior = null;
                    doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:10"));
                    tbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);
                   // break;

                    Page1 newPage = new Page1();

                    string titemLocation = "";
//                    DataTable dtItemImage = new DataTable();
//                    dtItemImage.Rows.Clear();
//                    SqlCommand cmd12 = new SqlCommand(@"SELECT Item_table.Item_no,dbo.additionalinfo.items_color, dbo.additionalinfo.font_color, dbo.Item_table.ItemPicture
//FROM         dbo.additionalinfo INNER JOIN
//                      dbo.Item_table ON dbo.additionalinfo.Item_No = dbo.Item_table.Item_no where Item_table.Item_no=@tItemName", con);
//                    cmd12.Parameters.AddWithValue("@tItemName", dtOffer.Rows[tRowCount]["Item_no"].ToString());
//                    //  cmd12.CommandType = CommandType.StoredProcedure;                 
//                    SqlDataAdapter adp4 = new SqlDataAdapter(cmd12);
//                    adp4.Fill(dtItemImage);
                   // if (dtItemImage.Rows.Count > 0)
                    {
                        if (dtOffer.Rows[tRowCount]["ItemImage"].ToString().Trim() != "")
                        {
                            titemLocation = System.Windows.Forms.Application.StartupPath + dtOffer.Rows[tRowCount]["ItemImage"].ToString();

                        }
                    }
                    try
                    {
                        if (titemLocation != "")
                        {
                            if (System.IO.File.Exists(titemLocation))
                            {
                                newPage.imgOffer.Source = new BitmapImage(new Uri(titemLocation)); ;
                            }

                        }
                        else
                        {
                            // if (System.IO.File.Exists(titemLocation))
                            //{
                            //    imgOffer.Source = new BitmapImage(new Uri(titemLocation)); ;
                            //}
                        }
                    }
                    catch (Exception)
                    {
                    }

                    
                    newPage.lblGridTitle.Text = dtOffer.Rows[tRowCount][0].ToString(); 
                    pageTransitionControl.ShowPage(newPage);
                }
            }
        }
        int tRowCount=0;
        DataTable dtOffer = new DataTable();
        System.Windows.Forms.Timer tempTimer = new System.Windows.Forms.Timer();
        string tLCDEnable = "No";
        string tLCDLogoLocation = "";
        string tLCDLogoType="", tLCDName = "", tLCDAddress1 = "", tLCDAddress2 = "", tLCFCounterNo = "1", tLCDEnableRate = "True", tLCDEnableAmt="True";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            DataTable dtLCD = new DataTable();
            dtLCD.Rows.Clear();
            SqlCommand cmdLcd = new SqlCommand("Select * from LCDDisplay_table", con);
            SqlDataAdapter adpLCD = new SqlDataAdapter(cmdLcd);
            adpLCD.Fill(dtLCD);
            if (dtLCD.Rows.Count > 0)
            {
                tLCDLogoType = Convert.ToString(dtLCD.Rows[0]["LogoName"]);
                tLCDEnable = Convert.ToString(dtLCD.Rows[0]["Enable"]);
                //tLCDLogoLocation=Convert.ToString( dtLCD.Rows[0]["imgLocation"]);
                tLCDLogoLocation = System.Windows.Forms.Application.StartupPath + Convert.ToString(dtLCD.Rows[0]["imgLocation"]);
                tLCDAddress1 = Convert.ToString(dtLCD.Rows[0]["AddrLine1"]);
                tLCDAddress2 = Convert.ToString(dtLCD.Rows[0]["AddrLine2"]);
                tLCFCounterNo = Convert.ToString(dtLCD.Rows[0]["Counter"]);
                tLCDEnableRate = Convert.ToString(dtLCD.Rows[0]["EnableRate"]);
                tLCDEnableAmt = Convert.ToString(dtLCD.Rows[0]["EnableAmount"]);
                tLCDName = Convert.ToString(dtLCD.Rows[0]["Name"]);
                if (tLCDLogoLocation != "")
                {
                    if (System.IO.File.Exists(tLCDLogoLocation))
                    {
                        imgCustomerLogo.Source = new BitmapImage(new Uri(tLCDLogoLocation)); ;
                    }
                }
                else
                {
                    lblCustomerName.Content = tLCDName;
                }
            }
            if (tLCDEnable == "Yes")
            {
                tempTimer.Interval = 1000;
                tempTimer.Enabled = false;
                tempTimer.Tick += new EventHandler(timer1_Tick);
                tTimerCount = 0;
                tempTimer.Start();

                imgCustomerLogo.Visibility = Visibility.Collapsed;
                lblCustomerName.Visibility = Visibility.Visible;
                lblAddress1.Visibility = Visibility.Visible;
                lblAddress2.Visibility = Visibility.Visible;
                if (!string.IsNullOrEmpty(tLCDLogoType) && tLCDLogoType == "Logo")
                {
                    imgCustomerLogo.Visibility = Visibility.Visible;
                    lblCustomerName.Visibility = Visibility.Collapsed;
                    // lblAddress1.Content = tLCDAddress1;
                }
                else
                {
                    lblCustomerName.Content = tLCDName;
                }
                if (!string.IsNullOrEmpty(tLCDAddress1))
                {
                    lblAddress1.Content = tLCDAddress1;
                }
                if (!string.IsNullOrEmpty(tLCDAddress2))
                {
                    lblAddress2.Content = tLCDAddress2;
                }
                funPreviousBill();
                tPreviousBillNo = tBillNo;
                funPreviousBillAmount();
                if (dtOffer.Columns.Count == 0)
                {
                    dtOffer.Columns.Add("OfferList", typeof(string));
                    dtOffer.Columns.Add("Item_no", typeof(string));
                    dtOffer.Columns.Add("ItemImage", typeof(string));
                }
                dtOffer.Rows.Clear();
                //parthi Coding..
                // SqlCommand cmdOffer = new SqlCommand("Select 'Buy '+CONVERT(varchar,SaleQtyFrom)+' '+Item_table.Item_name+' $'+CONVERT(varchar,convert(numeric(18,2), Rate)) as OfferList,FreeItem_table.Item_no,ItemImage from FreeItem_table, Item_table where item_table.Item_no = FreeItem_table.Item_no and FreeType='Item Price' and Active=1 and FromDate<=(Select CONVERT(date,EndOfDay,103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table))", con);
                //Anbu Coding Item Price
                SqlCommand cmdOffer = new SqlCommand("Select 'Buy '+CONVERT(varchar,SaleQty)+' '+Item_table.Item_name+' $'+CONVERT(varchar,convert(numeric(18,2), SaleAmt)) as OfferList,FreeItemMaster_table.Item_no,ItemImage from FreeItemMaster_table, Item_table where item_table.Item_no = FreeItemMaster_table.Item_no and FreeType='Item Price' and Active=1 and FromDate<=(Select CONVERT(date,EndOfDay,103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table))", con);
                SqlDataAdapter adpOffer = new SqlDataAdapter(cmdOffer);
                adpOffer.Fill(dtOffer);

                // DataTable dtSno = new DataTable();
                // dtSno.Rows.Clear();
                // //SqlCommand cmdOfferSno = new SqlCommand("Select distinct(FreeSnoGroup) as FreeSnoGroup,Item_table.item_no  from tempView, Item_table where Item_table.Item_no=tempView.FreeItem_no and Active=1 and FreeType<>'Item Price'", con);
                // //ParthiCoding:
                // //SqlCommand cmdOfferSno = new SqlCommand("Select FreeSnoGroup,Item_table.Item_no, ItemImage from FreeItem_table,Item_table where Item_table.Item_no=FreeItem_table.Item_no and FreeSno in (Select distinct(FreeSnoGroup) as FreeSnoGroup  from FreeItem_table where  Active=1 and FreeType<>'Item Price' and FromDate<=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table)))", con);
                // //AnbuCoding:


                // SqlCommand cmdOfferSno = new SqlCommand("select Item_table.Item_name,Item_table.Item_code,FreeItemMaster_table.OfferName,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.SaleQty,FreeItemMaster_table.SaleAmt,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.ItemImage from FreeItemMaster_table join  Item_table on FreeItemMaster_table.Item_no=Item_table.Item_no where Active=1 and FreeType<>'Item Price' and FreeItemMaster_table.FromDate<=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table)) and FreeItemMaster_table.ToDate>=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table))", con);
                // SqlDataAdapter adpOfferSno = new SqlDataAdapter(cmdOfferSno);
                // adpOfferSno.Fill(dtSno);
                //DataTable dtSnoDetail = new DataTable();
                //dtSnoDetail.Rows.Clear();

                // SqlCommand cmdOfferSnoDetail = new SqlCommand("Select FreeSno, Item_table.Item_name as FreeItemName, tempView.Item_name,Item_table.Item_no, SaleQtyFrom, SaleQtyTo, Free_Qty,Rate, Disc_amt, Disc_Per, Date, FromDate, ToDate, FreeItem_Stock, FreeItem_TempStock, FreeType,Active, FreeSnoGroup from tempView, Item_table where Item_table.Item_no=tempView.FreeItem_no and FreeSnoGroup in (Select distinct(FreeSnoGroup) as FreeSnoGroup  from FreeItem_table where  Active=1 and FreeType<>'Item Price' and FromDate<=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table)))", con);
                //// for(int il=0)
                //SqlCommand cmdOfferSnoDetail = new SqlCommand("select Item_table.Item_name,Item_table.Item_code,FreeItemMaster_table.Date,FreeItemMaster_table.FromDate,FreeItemMaster_table.ToDate,FreeItemMaster_table.TotFreeQty,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.ItemImage,FreeItemMaster_table.SaleQty,FreeItemMaster_table.TotFreeQty,FreeItemMaster_table.TotSaleQty from FreeItemMaster_table join Item_table on Item_table.Item_no=freeitemMaster_table.item_no where Active=1 and ItemType='Single' and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table))) ", con);
                //SqlDataAdapter adpOfferSnoDetail = new SqlDataAdapter(cmdOfferSnoDetail);
                //adpOfferSnoDetail.Fill(dtSnoDetail);

                //string tOfferName = "";
                //string tSno = "";
                //for (int mn = 0; mn < dtSno.Rows.Count; mn++)
                //{
                //    tOfferName = "";
                //    tSno = Convert.ToString(dtSno.Rows[mn]["FreeSnoGroup"].ToString());
                //    for (int ij = 0; ij < dtSnoDetail.Rows.Count; ij++)
                //    {
                //       if (tSno == Convert.ToString(dtSnoDetail.Rows[ij]["FreeSnoGroup"]))
                //        {
                //            if (string.IsNullOrEmpty(tOfferName) == true)
                //            {
                //                tOfferName = "Buy " + dtSnoDetail.Rows[ij]["SaleQtyFrom"] + " " + dtSnoDetail.Rows[ij]["Item_name"] + " Get ";
                //            }
                //            tOfferName += dtSnoDetail.Rows[ij]["Free_Qty"] + " " + dtSnoDetail.Rows[ij]["FreeItemName"] + ",";
                //        }
                //    }
                //    dtOffer.Rows.Add(tOfferName.TrimEnd(',') + " Free", dtSno.Rows[mn]["Item_no"].ToString(), dtSno.Rows[mn]["ItemImage"].ToString());
                //}
                //tRowCount = dtOffer.Rows.Count;
                ////for (int mn = 0; mn < dtOffer.Rows.Count; mn++)
                ////{
                DataTable dtSnoDetail = new DataTable();
                dtSnoDetail.Rows.Clear();

                // SqlCommand cmdOfferSnoDetail = new SqlCommand("Select FreeSno, Item_table.Item_name as FreeItemName, tempView.Item_name,Item_table.Item_no, SaleQtyFrom, SaleQtyTo, Free_Qty,Rate, Disc_amt, Disc_Per, Date, FromDate, ToDate, FreeItem_Stock, FreeItem_TempStock, FreeType,Active, FreeSnoGroup from tempView, Item_table where Item_table.Item_no=tempView.FreeItem_no and FreeSnoGroup in (Select distinct(FreeSnoGroup) as FreeSnoGroup  from FreeItem_table where  Active=1 and FreeType<>'Item Price' and FromDate<=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table)))", con);
                //// for(int il=0)
                SqlCommand cmdOfferSnoDetail = new SqlCommand("select Item_table.Item_name,Item_table.Item_code,FreeItemMaster_table.Date,FreeItemMaster_table.FromDate,FreeItemMaster_table.ToDate,FreeItemMaster_table.TotFreeQty,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.ItemImage,FreeItemMaster_table.SaleQty,FreeItemMaster_table.TotFreeQty,FreeItemMaster_table.TotSaleQty from FreeItemMaster_table join Item_table on Item_table.Item_no=freeitemMaster_table.item_no where Active=1 and ItemType='Single' and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) ", con);
                SqlDataAdapter adpOfferSnoDetail = new SqlDataAdapter(cmdOfferSnoDetail);
                adpOfferSnoDetail.Fill(dtSnoDetail);

                string tOfferName = "";
                string tSno = "";
                for (int ij = 0; ij < dtSnoDetail.Rows.Count; ij++)
                {
                    tOfferName = "Buy " + dtSnoDetail.Rows[ij]["TotSaleQty"] + " " + dtSnoDetail.Rows[ij]["Item_name"] + " Get ";
                    tOfferName += dtSnoDetail.Rows[ij]["TotFreeQty"] + " " + ",";
                    tSno = dtSnoDetail.Rows[ij]["FreeSnoGroup"].ToString();
                    SqlCommand cmd_free = new SqlCommand("Select item_table.item_name from freeItemDetail_table join item_table on freeItemDetail_table.FreeItem_no=item_table.item_no where FreeSno='" + tSno.ToString() + "'", con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd_free);
                    DataTable dtFreeItem = new DataTable();
                    dtFreeItem.Rows.Clear();
                    adp.Fill(dtFreeItem);
                    if (dtFreeItem.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtFreeItem.Rows.Count; i++)
                        {
                            tOfferName += dtFreeItem.Rows[i]["item_name"] + ",";
                        }
                    }
                    dtOffer.Rows.Add(tOfferName.TrimEnd(',') + " Free", "", dtSnoDetail.Rows[ij]["ItemImage"].ToString());
                }
                tRowCount = dtOffer.Rows.Count;
            }
            else
            {
                MessageBox.Show("Current LCD Display state is Disabled", "Warning");
                this.Close();
            }
            tbmarquee.Content = "Today Offers";
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -tbmarquee.ActualWidth;
            doubleAnimation.To = canMain.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:10"));
            tbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);
            //}
            Page1 newPage = new Page1();
            pageTransitionControl.ShowPage(newPage);

            //DataTable dtLCD = new DataTable();
            //dtLCD.Rows.Clear();

            //SqlCommand cmdLcd = new SqlCommand("Select * from LCDDisplay_table", con);
            //SqlDataAdapter adpLCD = new SqlDataAdapter(cmdLcd);
            //adpLCD.Fill(dtLCD);
            //if (dtLCD.Rows.Count > 0)
            //{
            //    tLCDLogoType = Convert.ToString(dtLCD.Rows[0]["LogoName"]);
            //    tLCDEnable=Convert.ToString( dtLCD.Rows[0]["Enable"]);
            //  //  tLCDLogoLocation=Convert.ToString( dtLCD.Rows[0]["imgLocation"]);
            //    tLCDLogoLocation = System.Windows.Forms.Application.StartupPath + Convert.ToString(dtLCD.Rows[0]["imgLocation"]);
            //    tLCDAddress1=Convert.ToString( dtLCD.Rows[0]["AddrLine1"]);
            //    tLCDAddress2 = Convert.ToString(dtLCD.Rows[0]["AddrLine2"]);
            //    tLCFCounterNo = Convert.ToString(dtLCD.Rows[0]["Counter"]);
            //    tLCDEnableRate = Convert.ToString(dtLCD.Rows[0]["EnableRate"]);
            //    tLCDEnableAmt = Convert.ToString(dtLCD.Rows[0]["EnableAmount"]);
            //    tLCDName = Convert.ToString(dtLCD.Rows[0]["Name"]);

            //    if (tLCDLogoLocation != "")
            //    {
            //        if (System.IO.File.Exists(tLCDLogoLocation))
            //        {
            //            imgCustomerLogo.Source = new BitmapImage(new Uri(tLCDLogoLocation)); ;
            //        }
            //    }
            //    else
            //    {
            //        lblCustomerName.Content = tLCDName;
            //    }
            //}
            //if (tLCDEnable == "Yes")
            //{
            //    tempTimer.Interval = 1000;
            //    tempTimer.Enabled = false;
            //    tempTimer.Tick += new EventHandler(timer1_Tick);
            //    tTimerCount = 0;
            //    tempTimer.Start();

            //    imgCustomerLogo.Visibility = Visibility.Collapsed;
            //    lblCustomerName.Visibility = Visibility.Visible;
            //    lblAddress1.Visibility = Visibility.Visible;
            //    lblAddress2.Visibility = Visibility.Visible;
            //    if (!string.IsNullOrEmpty(tLCDLogoType) && tLCDLogoType == "Logo")
            //    {
            //        imgCustomerLogo.Visibility = Visibility.Visible;
            //        lblCustomerName.Visibility = Visibility.Collapsed;
            //        // lblAddress1.Content = tLCDAddress1;
            //    }
            //    else
            //    {
            //        lblCustomerName.Content = tLCDName;
            //    }
            //    if (!string.IsNullOrEmpty(tLCDAddress1))
            //    {
            //        lblAddress1.Content = tLCDAddress1;
            //    }
            //    if (!string.IsNullOrEmpty(tLCDAddress2))
            //    {
            //        lblAddress2.Content = tLCDAddress2;
            //    }
            //    funPreviousBill();
            //    tPreviousBillNo = tBillNo;
            //    funPreviousBillAmount();
            //    if (dtOffer.Columns.Count == 0)
            //    {
            //        dtOffer.Columns.Add("OfferList", typeof(string));
            //        dtOffer.Columns.Add("Item_no", typeof(string));
            //        dtOffer.Columns.Add("ItemImage", typeof(string));
            //    }
            //    dtOffer.Rows.Clear();
            //    SqlCommand cmdOffer = new SqlCommand("Select 'Buy '+CONVERT(varchar,SaleQtyFrom)+' '+Item_table.Item_name+' $'+CONVERT(varchar,convert(numeric(18,2), Rate)) as OfferList,FreeItem_table.Item_no,ItemImage from FreeItem_table, Item_table where item_table.Item_no = FreeItem_table.Item_no and FreeType='Item Price' and Active=1 and FromDate>=(Select CONVERT(date,EndOfDay,103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table))", con);
            //    SqlDataAdapter adpOffer = new SqlDataAdapter(cmdOffer);
            //    adpOffer.Fill(dtOffer);

            //    DataTable dtSno = new DataTable();
            //    dtSno.Rows.Clear();
            //    //SqlCommand cmdOfferSno = new SqlCommand("Select distinct(FreeSnoGroup) as FreeSnoGroup,Item_table.item_no  from tempView, Item_table where Item_table.Item_no=tempView.FreeItem_no and Active=1 and FreeType<>'Item Price'", con);
            //    SqlCommand cmdOfferSno = new SqlCommand("Select FreeSnoGroup,Item_table.Item_no, ItemImage from FreeItem_table,Item_table where Item_table.Item_no=FreeItem_table.Item_no and FreeSno in (Select distinct(FreeSnoGroup) as FreeSnoGroup  from FreeItem_table where  Active=1 and FreeType<>'Item Price' and FromDate<=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table)))", con);
            //    SqlDataAdapter adpOfferSno = new SqlDataAdapter(cmdOfferSno);
            //    adpOfferSno.Fill(dtSno);
            //    DataTable dtSnoDetail = new DataTable();
            //    dtSnoDetail.Rows.Clear();
            //    SqlCommand cmdOfferSnoDetail = new SqlCommand("Select FreeSno, Item_table.Item_name as FreeItemName, tempView.Item_name,Item_table.Item_no, SaleQtyFrom, SaleQtyTo, Free_Qty,Rate, Disc_amt, Disc_Per, Date, FromDate, ToDate, FreeItem_Stock, FreeItem_TempStock, FreeType,Active, FreeSnoGroup from tempView, Item_table where Item_table.Item_no=tempView.FreeItem_no and FreeSnoGroup in (Select distinct(FreeSnoGroup) as FreeSnoGroup  from FreeItem_table where  Active=1 and FreeType<>'Item Price' and FromDate<=(Select CONVERT(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where id=(select MAX(id) from EndOfDay_Table)))", con);
            //    SqlDataAdapter adpOfferSnoDetail = new SqlDataAdapter(cmdOfferSnoDetail);
            //    adpOfferSnoDetail.Fill(dtSnoDetail);
            //    string tOfferName = "";
            //    string tSno = "";
            //    for (int mn = 0; mn < dtSno.Rows.Count; mn++)
            //    {
            //        tOfferName = "";
            //        tSno = Convert.ToString(dtSno.Rows[mn]["FreeSnoGroup"].ToString());
            //        for (int ij = 0; ij < dtSnoDetail.Rows.Count; ij++)
            //        {
            //            if (tSno == Convert.ToString(dtSnoDetail.Rows[ij]["FreeSnoGroup"]))
            //            {
            //                if (string.IsNullOrEmpty(tOfferName) == true)
            //                {
            //                    tOfferName = "Buy " + dtSnoDetail.Rows[ij]["SaleQtyFrom"] + " " + dtSnoDetail.Rows[ij]["Item_name"] + " Get ";
            //                }
            //                tOfferName += dtSnoDetail.Rows[ij]["Free_Qty"] + " " + dtSnoDetail.Rows[ij]["FreeItemName"] + ",";
            //            }
            //        }
            //        dtOffer.Rows.Add(tOfferName.TrimEnd(',') + " Free", dtSno.Rows[mn]["Item_no"].ToString(), dtSno.Rows[mn]["ItemImage"].ToString());
            //    }
            //    tRowCount = dtOffer.Rows.Count;
            //    //for (int mn = 0; mn < dtOffer.Rows.Count; mn++)
            //    //{

            //}
            //else
            //{
            //    MessageBox.Show("Current LCD Display state is Disabled","Warning");
            //    this.Close();
            //}
            //    tbmarquee.Content = "Today Offers";
            //    DoubleAnimation doubleAnimation = new DoubleAnimation();
            //    doubleAnimation.From = -tbmarquee.ActualWidth;
            //    doubleAnimation.To = canMain.ActualWidth;
            //    doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            //    doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:10"));
            //    tbmarquee.BeginAnimation(Canvas.RightProperty, doubleAnimation);
            //    //}
            //    Page1 newPage = new Page1();
            //    pageTransitionControl.ShowPage(newPage);
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
