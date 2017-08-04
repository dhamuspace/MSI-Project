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
using System.Windows.Forms;
using System.Configuration;

namespace SalesProject
{
    /// <summary>
    /// Interaction logic for FrmHold.xaml
    /// </summary>
    public partial class FrmHold : System.Windows.Controls.UserControl
    {
        public FrmHold()
        {
            InitializeComponent();
        }
      //  SqlConnection con = new SqlConnection(@"Data Source=admin-pc\sqlexpress;Initial Catalog=MSPOS;Persist Security Info=True;User ID=sa;password=!Password123");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        // SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=MSPOS;Persist Security Info=True;User ID=sa;password=ms@123");
        SqlDataReader dr = null;
        //  SalesCreation frm = new SalesCreation();
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            SalesCreationEventHandlerNew(sender, e);
        }
        public DataTable dt1 = new DataTable();
        public event System.EventHandler SalesCreationEventHandler;
        public event System.EventHandler SalesCreationEventHandlerNew;
        public event System.EventHandler HoldInsertEventHandler;
        private void btnHoldOne_Click(object sender, RoutedEventArgs e)
        {
            if (btnHoldOne.Content.ToString() == "")
            {              
                    btnHoldOne.Content = "1";
                    HoldInsertEventHandler(sender, e);    
            }
          
            else 
            {
                if (SalesCreationEventHandler != null)
                {
                    SalesCreationEventHandler(sender, e);
                }
                SalesCreationEventHandlerNew(sender, e);  
            }
        }
        public string tButtonCaption;
        public string tLoadCaption;
        public DataTable dt = new DataTable();
        int count = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SalesProject._Class.clsVariables.holded = "";
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("Qty", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("Amt", typeof(string));
                dt.Columns.Add("Disc", typeof(string));
            }
           // SqlCommand cmd = new SqlCommand("Select Distinct(Hold_No) from SalHold_Table", con);
            //anbucode:
            SqlCommand cmd = new SqlCommand("Select Distinct(Hold_No) from SalHold_Table where ctr_no = '"+_Class.clsVariables.tCounter+"'", con);
            con.Close();
            //con.Open();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            dr = cmd.ExecuteReader();
            count=0;
            while (dr.Read())
            {
                count += 1;
                if (dr["Hold_No"].ToString()== "1")
                {
                    btnHoldOne.Content = dr["Hold_no"].ToString();
                }
                if (dr["Hold_No"].ToString() == "2")
                {
                    btnHoldTwo.Content = dr["Hold_no"].ToString();
                }
                if (dr["Hold_No"].ToString() == "3")
                {
                    btnHoldThree.Content = dr["Hold_no"].ToString();
                }
                if (dr["Hold_No"].ToString() == "4")
                {
                    btnHoldFour.Content = dr["Hold_no"].ToString();
                }
                if (dr["Hold_No"].ToString() == "5")
                {
                    btnHoldFive.Content = dr["Hold_no"].ToString();
                }
                if (count > 5)
                {
                    break;
                }

            }

        }

        private void btnHoldTwo_Click(object sender, RoutedEventArgs e)
        {
            if (btnHoldTwo.Content.ToString() == "")
            {
                btnHoldTwo.Content = "2";
                HoldInsertEventHandler(sender, e);
            }
           
            else
            {
                if (SalesCreationEventHandler != null)
                {
                    SalesCreationEventHandler(sender, e);
                }
                SalesCreationEventHandlerNew(sender, e);
            }
        }

        private void btnHoldThree_Click(object sender, RoutedEventArgs e)
        {
           
            if (btnHoldThree.Content.ToString() == "")
            {
                btnHoldThree.Content = "3";
                HoldInsertEventHandler(sender, e);
            }
            
            else
            {
                if (SalesCreationEventHandler != null)
                {
                    SalesCreationEventHandler(sender, e);
                }
                SalesCreationEventHandlerNew(sender, e);
            }
        }

        private void btnHoldFour_Click(object sender, RoutedEventArgs e)
        {
            
            if (btnHoldFour.Content.ToString() == "")
            {
                btnHoldFour.Content = "4";
                HoldInsertEventHandler(sender, e);
            }
            
            else
            {
                if (SalesCreationEventHandler != null)
                {
                    SalesCreationEventHandler(sender, e);
                }
                SalesCreationEventHandlerNew(sender, e);
            }
        }

        private void btnHoldFive_Click(object sender, RoutedEventArgs e)
        {
            
            if (btnHoldFive.Content.ToString() == "")
            {
                btnHoldFive.Content = "5";
                HoldInsertEventHandler(sender, e);
            }
            else
            {
                if (SalesCreationEventHandler != null)
                {
                    SalesCreationEventHandler(sender, e);
                }
                SalesCreationEventHandlerNew(sender, e);
            }
        }

      
    }
}
