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
using Microsoft.Win32;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for FrmActivation.xaml
    /// </summary>
    /// 
   public delegate void UCHideEvent();
    public partial class FrmActivation : UserControl
    {
        public FrmActivation()
        {
            InitializeComponent();
        }
        public event UCHideEvent UCHideEvent_Click;
        public event UCHideEvent UCHideEvent_KeyBoardClick;
       // public event UCHideEvent UCKeyHideEvent_Click;

        private void deletereg()
        {
            //try
            //{
            //    RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"Software\Tanmay\Protection3", true);
            //    // regkey.DeleteValue("Black", true);
            //    regkey.SetValue("Black", "False");
            //    DateTime dt = DateTime.Now;
            //    string onlyDate = dt.ToShortDateString(); // get only date not time            
            //    regkey.SetValue("Use", onlyDate);
            //    regkey.Close();
            //}
            try
            {
                // get present date from system
                DateTime dt = DateTime.Now;
                string today = dt.ToShortDateString();
                DateTime presentDate = Convert.ToDateTime(today);

                // get instalation date
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                string Br = (string)regkey.GetValue("Install");
                DateTime installationDate = Convert.ToDateTime(Br);               

                TimeSpan diff = presentDate.Subtract(installationDate); //first.Subtract(second);            
                int totaldays = (int)diff.TotalDays;
                // special check if user chenge date in system               

                if (totaldays >= 0)
                {
                    string onlydate = presentDate.ToShortDateString();
                    regkey.SetValue("Use", onlydate);
                    regkey.SetValue("Black", "False");
                    regkey.Close();
                }
                else
                {
                    string Br1 = (string)regkey.GetValue("Install");
                    DateTime installationDate1 = Convert.ToDateTime(Br1);
                    string Insdate = installationDate1.ToShortDateString();
                    regkey.SetValue("Use", Insdate);
                    regkey.SetValue("Black", "False");
                    regkey.Close();

                }
               
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }

        private void SecondTime()
        {
            try
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                DateTime dt = DateTime.Now;
                string onlyDate = dt.ToShortDateString(); // get only date not time
                regkey.SetValue("Install", onlyDate); //Value Name,Value Data
                regkey.SetValue("Use", onlyDate); //Value Name,Value Data
                regkey.SetValue("Days", "45"); //Value Name,Value Data
            }

            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }  

        private void firstTime()
        {
            try
            {
                RegistryKey regkey = Registry.CurrentUser;
                regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                DateTime dt = DateTime.Now;
                string onlyDate = dt.ToShortDateString(); // get only date not time
                regkey.SetValue("Install", onlyDate); //Value Name,Value Data
                regkey.SetValue("Use", onlyDate); //Value Name,Value Data
                regkey.SetValue("Days", "365"); //Value Name,Value Data
            }
            
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }  
      
        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtActiveCode.Password.Trim() != "")                
                {
                    string strDelete = _Class.clsVariables.tstr1;

                    if (strDelete == "function3" && txtActiveCode.Password.Trim() == "Admin@123")
                    {
                        deletereg();
                        this.Visibility = Visibility.Hidden;
                        UserControl uct1 = new UCKeyBoradAct();
                        uct1.Visibility = Visibility.Hidden;
                        if (UCHideEvent_Click != null)
                        {
                            UCHideEvent_Click();
                        }
                    }
                    else if (txtActiveCode.Password.Trim() == "AdminActivate@123")
                    {                      
                        RegistryKey regkey = Registry.CurrentUser;
                        regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                        regkey.SetValue("Black", "False");
                        firstTime();
                        this.Visibility = Visibility.Hidden;
                        UserControl uct1 = new UCKeyBoradAct();
                        uct1.Visibility = Visibility.Hidden;
                        if (UCHideEvent_Click != null)
                        {
                            UCHideEvent_Click();
                        }                 
                    }
                    else if (txtActiveCode.Password.Trim() == "Activate@123")
                    {

                        RegistryKey regkey = Registry.CurrentUser;
                        regkey = regkey.CreateSubKey(@"Software\Tanmay\Protection3"); //path
                        regkey.SetValue("Black", "False");
                        SecondTime();
                        this.Visibility = Visibility.Hidden;
                        UserControl uct1 = new UCKeyBoradAct();
                        uct1.Visibility = Visibility.Hidden;
                        if (UCHideEvent_Click != null)
                        {
                            UCHideEvent_Click();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Enter Valid Activation Code","Warning");                        
                        //  frm.tActivationStatus = "NonActive";
                        //this.Close();                       
                        txtActiveCode.Password = "";
                        txtActiveCode.Focus();
                    }
                }
                else
                {
                    MyMessageBox.ShowBox("Enter Activation Code","Warning");            
                }            
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        //string strnew="function2";
        private void btnCanel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //FrmLogin frml = new FrmLogin();
                string strnew = _Class.clsVariables.tstr1;

                if (strnew == "function1")
                {
                    this.Visibility = Visibility.Hidden;
                    UserControl uct1 = new UCKeyBoradAct();
                    uct1.Visibility = Visibility.Hidden;
                    if (UCHideEvent_Click != null)
                    {
                        UCHideEvent_Click();
                    }
                }
                if (strnew == "function2")
                {
                    if (_Class.clsVariables.tCustomerDisplayEnable == "Yes")
                    {
                        byte[] bytesToSend1 = new byte[1] { 0x0C };
                        _Class.clsVariables.spCustomerDis.Write(bytesToSend1, 0, 1);
                        _Class.clsVariables.spCustomerDis.Close();
                        _Class.clsVariables.spCustomerDis.Dispose();
                        _Class.clsVariables.spCustomerDis = null;
                    }
                    Application.Current.Shutdown();
                    foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())//GETS PROCESSES
                    {
                        if (pr.ProcessName == "SalesProject")//KILLS FIREFOX.....REMOVE FIREFOX.....CONNECT SAVED SQL PROCESSES IN HERE MAYBE??
                        {
                            pr.Kill(); //KILLS THE PROCESSES
                        }
                    }
                }
                if (strnew == "function3")
                {
                    Application.Current.Shutdown();
                }
            }            
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }

        private void btnKeypad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //UserControl uct = new UserControls.UCKeyBoard();
                //uct.Visibility = Visibility.Visible;  
                _Class.clsVariables.tVoidValue = "";
                if (UCHideEvent_KeyBoardClick != null)
                {
                    UCHideEvent_KeyBoardClick();
                }
                UserControl uct = new UCKeyBoradAct();
                uct.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Warning");
            }
        }
    }
}
