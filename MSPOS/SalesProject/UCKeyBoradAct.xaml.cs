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
using System.Runtime.InteropServices;


namespace SalesProject
{
    /// <summary>
    /// Interaction logic for UCKeyBoradAct.xaml
    /// </summary>
    /// 

    public delegate void  UCKeyBoradActEvent();
    public partial class UCKeyBoradAct : UserControl
    {
        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        //static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        //const int KEYEVENTF_EXTENDEDKEY = 0x1;
        //const int KEYEVENTF_KEYUP = 0x2;
        public UCKeyBoradAct()
        {
            InitializeComponent();
        }
        string temp = null;
        public event UCKeyBoradActEvent UCKeyBoradActEvent_CancelClick;
        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtEnterValue.Focus();
                System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
                if (txtEnterValue.Password != "")
                {
                    temp = txtEnterValue.Password;
                    txtEnterValue.Password = "";
                    txtEnterValue.Password = temp + " ";
                }
                if (txtEnterValue.Password == "")
                {
                    txtEnterValue.Password = " ";
                }
                //txtEnterValue.Select(txtEnterValue.Password.Length, 0);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Visibility = Visibility.Hidden;
                _Class.clsVariables.tVoidValue = "";
                if (UCKeyBoradActEvent_CancelClick != null)
                {
                    UCKeyBoradActEvent_CancelClick();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEnterValue.Password.Length > 0)
                {
                    temp = txtEnterValue.Password;
                    txtEnterValue.Password = temp.Remove(temp.Length - 1);
                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnCaps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnCaps.Content.ToString() == "Caps")
                {
                    btnCaps.Content = "CAPS";
                    btnA.Content = btnA.Content.ToString().ToLower();
                    btnB.Content = btnB.Content.ToString().ToLower();
                    btnC.Content = btnC.Content.ToString().ToLower();
                    btnD.Content = btnD.Content.ToString().ToLower();
                    btnE.Content = btnE.Content.ToString().ToLower();
                    btnF.Content = btnF.Content.ToString().ToLower();
                    btnG.Content = btnG.Content.ToString().ToLower();
                    btnH.Content = btnH.Content.ToString().ToLower();
                    btnI.Content = btnI.Content.ToString().ToLower();
                    btnJ.Content = btnJ.Content.ToString().ToLower();
                    btnK.Content = btnK.Content.ToString().ToLower();
                    btnL.Content = btnL.Content.ToString().ToLower();
                    btnM.Content = btnM.Content.ToString().ToLower();
                    btnN.Content = btnN.Content.ToString().ToLower();
                    btnO.Content = btnO.Content.ToString().ToLower();
                    btnP.Content = btnP.Content.ToString().ToLower();
                    btnQ.Content = btnQ.Content.ToString().ToLower();
                    btnR.Content = btnR.Content.ToString().ToLower();
                    btnS.Content = btnS.Content.ToString().ToLower();
                    btnT.Content = btnT.Content.ToString().ToLower();
                    btnU.Content = btnU.Content.ToString().ToLower();
                    btnV.Content = btnV.Content.ToString().ToLower();
                    btnW.Content = btnW.Content.ToString().ToLower();
                    btnX.Content = btnX.Content.ToString().ToLower();
                    btnY.Content = btnY.Content.ToString().ToLower();
                    btnZ.Content = btnZ.Content.ToString().ToLower();
                }
                else
                {
                    btnCaps.Content = "Caps";
                    btnA.Content = btnA.Content.ToString().ToUpper();
                    btnB.Content = btnB.Content.ToString().ToUpper();
                    btnC.Content = btnC.Content.ToString().ToUpper();
                    btnD.Content = btnD.Content.ToString().ToUpper();
                    btnE.Content = btnE.Content.ToString().ToUpper();
                    btnF.Content = btnF.Content.ToString().ToUpper();
                    btnG.Content = btnG.Content.ToString().ToUpper();
                    btnH.Content = btnH.Content.ToString().ToUpper();
                    btnI.Content = btnI.Content.ToString().ToUpper();
                    btnJ.Content = btnJ.Content.ToString().ToUpper();
                    btnK.Content = btnK.Content.ToString().ToUpper();
                    btnL.Content = btnL.Content.ToString().ToUpper();
                    btnM.Content = btnM.Content.ToString().ToUpper();
                    btnN.Content = btnN.Content.ToString().ToUpper();
                    btnO.Content = btnO.Content.ToString().ToUpper();
                    btnP.Content = btnP.Content.ToString().ToUpper();
                    btnQ.Content = btnQ.Content.ToString().ToUpper();
                    btnR.Content = btnR.Content.ToString().ToUpper();
                    btnS.Content = btnS.Content.ToString().ToUpper();
                    btnT.Content = btnT.Content.ToString().ToUpper();
                    btnU.Content = btnU.Content.ToString().ToUpper();
                    btnV.Content = btnV.Content.ToString().ToUpper();
                    btnW.Content = btnW.Content.ToString().ToUpper();
                    btnX.Content = btnX.Content.ToString().ToUpper();
                    btnY.Content = btnY.Content.ToString().ToUpper();
                    btnZ.Content = btnZ.Content.ToString().ToUpper();
                }
                txtEnterValue.Focus();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtEnterValue.Focus();
                System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
                if (txtEnterValue.Password != "")
                {
                    temp = txtEnterValue.Password;
                    txtEnterValue.Password = "";
                    txtEnterValue.Password = temp + btn.Content.ToString();
                }
                if (txtEnterValue.Password == "")
                {
                    txtEnterValue.Password = btn.Content.ToString();
                }

            }
            //txtEnterValue.Select(txtEnterValue.Password.Length, 0);
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
        public event System.EventHandler SalesCreationEventHandlerNew;

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _Class.clsVariables.tVoidValue = txtEnterValue.Password.Trim();
                if (_Class.clsVariables.tVoidActionType == "BILLNO")
                {
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "ITEMCODE")
                {
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "SALESITEMCODE")
                {
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "PASSWORD")
                {
                    // this.Close();
                    this.Visibility = Visibility.Hidden;
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "LOGINNAME")
                {
                    this.Visibility = Visibility.Hidden;
                    if (UCKeyBoradActEvent_CancelClick != null)
                    {
                        UCKeyBoradActEvent_CancelClick();
                    }
                }
                else if (_Class.clsVariables.tVoidActionType == "REMARK")
                {
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "PAYMENTIN")
                {
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "PAYMENTOUT")
                {
                    SalesCreationEventHandlerNew(sender, e);
                }
                else if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                {
                    this.Visibility = Visibility.Hidden;
                    if (UCKeyBoradActEvent_CancelClick != null)
                    {
                        UCKeyBoradActEvent_CancelClick();
                    }
                }
                else if (_Class.clsVariables.tVoidActionType == "KeyPassword")
                {
                    this.Visibility = Visibility.Hidden;
                    if (UCKeyBoradActEvent_CancelClick != null)
                    {
                        UCKeyBoradActEvent_CancelClick();
                    }
                }
                //  this.Close();
                //this.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void txtEnterValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    _Class.clsVariables.tVoidValue = txtEnterValue.Password.Trim();
                    if (_Class.clsVariables.tVoidActionType == "BILLNO")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "ITEMCODE")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "SALESITEMCODE")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "PASSWORD")
                    {
                        //  this.Close();
                        this.Visibility = Visibility.Hidden;
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "LOGINNAME")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "PAYMENTIN")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "PAYMENTOUT")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    else if (_Class.clsVariables.tVoidActionType == "ACTIVATE")
                    {
                        SalesCreationEventHandlerNew(sender, e);
                    }
                    //  this.Close();
                    this.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
    }
}
