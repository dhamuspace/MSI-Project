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
    /// Interaction logic for frmVisual.xaml
    /// </summary>
    /// 
    public delegate void UCItemUpdateEvent();
    public partial class UCItemUpdateQtyNRate : UserControl
    {
        public UCItemUpdateQtyNRate()
        {
            InitializeComponent();
        }
        public event UCItemUpdateEvent UCItemUpdateEventRemoveItemClick;
        public event UCItemUpdateEvent UCItemUpdateEventMiscClick;
        public event UCItemUpdateEvent UCItemUpdateEventPriceChangeClick;
        public event UCItemUpdateEvent UCItemUpdateEventFinishClick;
        public event UCItemUpdateEvent UCItemUpdateEventShowModifierClick;
        public string  tUCNewQtyMain
        {
            get
            {
                return Convert.ToString(txtNewQuantity.Text);
            }
            set
            {
                txtNewQuantity.Text = value;
            }
        }

        public string tUCOriginalQtyMain
        {
            get
            {
                return Convert.ToString(txtOriginalQuantity.Text);            }
            set
            {
                txtOriginalQuantity.Text = value;
            }
        }

        public string tUCUpdateItemNameMain
        {
            get
            {
                return Convert.ToString(UCUpdatelblItemName.Content);
            }
            set
            {
                UCUpdatelblItemName.Content = value;
            }
        }

        public string  UCUpdateSelectedItemNoMain
        {
            get
            {
                return Convert.ToString(UCUpdateSelectedItemNumber.Content);
            }
            set
            {
              UCUpdateSelectedItemNumber.Content = value;
            }
        }

        public string UCLblItemRateMain
        {
            get
            {
                return Convert.ToString(UCLblItemRate.Content);
            }
            set
            {
                UCLblItemRate.Content = value;
            }
        }

        double temp1=0, temp2=0;
        private void btnAddOne_Click(object sender, RoutedEventArgs e)
        {            
            temp1 = Convert.ToDouble(txtNewQuantity.Text);
            temp2 = temp1 + 1;
            txtNewQuantity.Text = Convert.ToString(temp2);
        }

        private void btnAddFive_Click(object sender, RoutedEventArgs e)
        {
            temp1 = Convert.ToDouble(txtNewQuantity.Text);
            temp2 = temp1 + 5;
            txtNewQuantity.Text = Convert.ToString(temp2);
        }

        private void btnAddTen_Click(object sender, RoutedEventArgs e)
        {
            temp1 = Convert.ToDouble(txtNewQuantity.Text);
            temp2 = temp1 + 10;
            txtNewQuantity.Text = Convert.ToString(temp2);
        }
        private void btnSubone_Click(object sender, RoutedEventArgs e)
        {
            temp1 = Convert.ToDouble(txtNewQuantity.Text);
            if (temp1 > 1)
            {
                temp2 = temp1 - 1;
                txtNewQuantity.Text = Convert.ToString(temp2);
            }
        }
        private void btnSubFive_Click(object sender, RoutedEventArgs e)
        {
            temp1 = Convert.ToDouble(txtNewQuantity.Text);
            if (temp1 > 5)
            {
                temp2 = temp1 - 5;
                txtNewQuantity.Text = Convert.ToString(temp2);
            }
        }

        private void btnSubTen_Click(object sender, RoutedEventArgs e)
        {
            temp1 = Convert.ToDouble(txtNewQuantity.Text);
            if (temp1 > 10)
            {
                temp2 = temp1 - 10;
                txtNewQuantity.Text = Convert.ToString(temp2);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //txtNewQuantity.Text = "1";
           // UCFrmItemDiscount1.UCFRMDiscountEventCloseClick += new UCFRMDiscountEvent(UCFrmItemDiscount1_UCFRMDiscountEventCloseClick);
           // UCFrmItemDiscount1.UCFRMDiscountEventEnterClick += new UCFRMDiscountEvent(UCFrmItemDiscount1_UCFRMDiscountEventEnterClick);
              
        }

        private void btnUndoChanges_Click(object sender, RoutedEventArgs e)
        {
            txtNewQuantity.Text = txtOriginalQuantity.Text;
        }

        private void UCbtnFinished_Click(object sender, RoutedEventArgs e)
        {
            _Class.clsVariables.itemQty = txtNewQuantity.Text;
            if (UCItemUpdateEventFinishClick != null)
            {
                UCItemUpdateEventFinishClick();
            }
        }

        private void UCbtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (UCItemUpdateEventRemoveItemClick != null)
            {
                UCItemUpdateEventRemoveItemClick();
            }
        }

        private void UCbtnShowModifiers_Click(object sender, RoutedEventArgs e)
        {
            if (UCItemUpdateEventShowModifierClick != null)
            {
                UCItemUpdateEventShowModifierClick();
            }

        }

        private void UCbtnMiscChange_Click(object sender, RoutedEventArgs e)
        {
            if (UCItemUpdateEventMiscClick != null)
            {
                UCItemUpdateEventMiscClick();
            }
        }

        private void UCbtnPriceChange_Click(object sender, RoutedEventArgs e)
        {
            if (UCItemUpdateEventPriceChangeClick != null)
            {
                UCItemUpdateEventPriceChangeClick();
            }
        }
    }
}
