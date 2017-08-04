using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;

namespace MSPOSBACKOFFICE
{
    static class Program
    {

       private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
      // System.Threading.Mutex mutex = new System.Threading.Mutex(false, appGuid);
        static void Main()
        {

            ////System.Windows.Forms.Application.EnableVisualStyles();
            ////System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            ////{
            ////    System.Threading.Mutex mutex = new System.Threading.Mutex(false, appGuid); 
            ////    if (!mutex.WaitOne(0, false))
            ////    {
            ////        System.Windows.Forms.MessageBox.Show("MSPOS BACKOFFICE Already Running", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////        return;
            ////    }

               // System.Windows.Forms.Application.Run(new frmBackOffice1());
                CustomApplication app = new CustomApplication();
                app.Run();
            ////}
            ////System.Threading.Mutex mutex1 = new System.Threading.Mutex(false, appGuid); 
            ////if (!mutex1.WaitOne(0, false))
            ////{
            ////    System.Windows.Forms.MessageBox.Show("MSPOS BACKOFFICE Already Running","Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    return;
            ////}
            ////GC.Collect();
            // //CustomApplication app = new CustomApplication();
            ////app.Run();
           // Application.Run(new frmBackOffice1());
          //Application.Run(StartupUri = "MainWindow.xaml");
        }
    }
    public class CustomApplication : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            FrmLogin frm = new FrmLogin();
            frm.Show();
        }
    }
}