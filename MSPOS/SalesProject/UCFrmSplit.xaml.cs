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

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for frmSplit.xaml
    /// </summary>
    /// 

    public delegate void UCFrmSplitEvent();
    public delegate void UCFrmSplitEvent1(object sender,RoutedEventArgs e);
    public partial class UCFrmSplit : UserControl
    {
        public UCFrmSplit()
        {
            InitializeComponent();
           
        }
        public void funFrmSplitLoad()
        {
            pnlItemQty.Visibility = Visibility.Visible;
            UCPnlSplit.Visibility = Visibility.Visible;
            UCFrmItemDiscount1.Visibility = Visibility.Hidden;
        }

        

        private void btnCancelEven_Click(object sender, RoutedEventArgs e)
        {
            lblSplit.Content = "SELECTED ITEM";
            pnlItemQty.Visibility = Visibility.Visible;
            UCFrmItemDiscount1.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Hidden;
            if (UCFrmSplitEventItemCancelClick != null)
            {
                UCFrmSplitEventItemCancelClick();
            }
        }
        double temp1 = 0,temp2=0;       
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                temp1 = Convert.ToDouble(txtEven.Text);
                // temp1 = Convert.ToDouble(lblSplit.Content);
                if (temp1 > 0)
                {
                    temp2 = temp1 - 1;
                    txtEven.Text = Convert.ToString(temp2);
                    //lblSplit.Content = Convert.ToString(temp2);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                temp1 = Convert.ToDouble(txtEven.Text);
                // temp1 = Convert.ToDouble(lblSplit.Content);
               // if (temp1 < 20)
                {
                    temp2 = temp1 + 1;
                    txtEven.Text = Convert.ToString(temp2);
                    //lblSplit.Content = Convert.ToString(temp2);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public event UCFrmSplitEvent UCFrmSplitEventRemoveItem;
        public event UCFrmSplitEvent UCFrmSplitEventSubmitItem;
        public event UCFrmSplitEvent UCFrmSplitEventSubmitGuest;
        public event UCFrmSplitEvent UCFrmSplitEventItemClick;
        public event UCFrmSplitEvent UCFrmSplitEventItemCancelClick;
                    
        private void UCBtnSubmitEven_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            _Class.clsVariables.itemQty = txtEven.Text.Trim();
            if (UCFrmSplitEventSubmitItem != null)
            {
                UCFrmSplitEventSubmitItem();
            }
        }

      
       

        private void UCBtnItemDiscount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (_Class.clsVariables.tMainDiscountType == "Individual")
                {
                    UCFrmItemDiscount1.Visibility = Visibility.Visible;
                    pnlItemQty.Visibility = Visibility.Hidden;
                   
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
           
        }

        private void UCBtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pnlItemQty.Visibility = Visibility.Visible;
                UCFrmItemDiscount1.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                if (UCFrmSplitEventRemoveItem != null)
                {
                    UCFrmSplitEventRemoveItem();
                }
            }
            catch ( Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UCFrmItemDiscount1.UCFRMDiscountEventCloseClick += new UCFRMDiscountEvent(UCFrmItemDiscount1_UCFRMDiscountEventCloseClick);
                UCFrmItemDiscount1.UCFRMDiscountEventEnterClick += new UCFRMDiscountEvent(UCFrmItemDiscount1_UCFRMDiscountEventEnterClick);
                funFrmSplitLoad();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public event UCFrmSplitEvent1 UCItemDicount_CloseClick;
        public event UCFrmSplitEvent1 UCItemDicount_EnterClick;
        private void UCFrmItemDiscount1_UCFRMDiscountEventCloseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                pnlItemQty.Visibility = Visibility.Visible;
                UCFrmItemDiscount1.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                if (UCItemDicount_CloseClick != null)
                {
                    UCItemDicount_CloseClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        private void UCFrmItemDiscount1_UCFRMDiscountEventEnterClick(object sender, RoutedEventArgs e)
        {
            try
            {
                pnlItemQty.Visibility = Visibility.Visible;
                UCFrmItemDiscount1.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                if (UCItemDicount_EnterClick != null)
                {
                    UCItemDicount_EnterClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}
