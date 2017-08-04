using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Data.SqlClient;
using System.Configuration;


namespace SalesProject
{
    public partial class frmBackOffice1 : Form
    {
        public frmBackOffice1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        private void rbtnItemCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.ItemCreations)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.ItemCreations frm = new MSPOSBACKOFFICE.ItemCreations("");
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
            //pnlMasterDisplay.Controls.Clear();
            //ItemCreations brandform = new ItemCreations();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlMasterDisplay.Controls.Add(brandform);
            //brandform.Show();
        }
     //   System.Windows.Forms.Integration.ElementHost host;
      //  GroupItemSetting uc1;
        private void rbtnGroupCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmGroupCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmGroupCreation frm = new MSPOSBACKOFFICE.frmGroupCreation();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }          

        }

        private void rbtnBrandCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Brand)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Brand frm = new MSPOSBACKOFFICE.Brand();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
            //pnlMasterDisplay.Controls.Clear();
            //Brand brandform = new Brand();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlMasterDisplay.Controls.Add(brandform);
            //brandform.Show();
        }

        private void rbtnModelCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Model)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Model frm = new MSPOSBACKOFFICE.Model();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }

            //pnlMasterDisplay.Controls.Clear();
            //Model modelform = new Model();
            //modelform.TopLevel = false;
            //modelform.AutoScroll = true;
            //pnlMasterDisplay.Controls.Add(modelform);
            //modelform.Show();
        }

        private void rbtnUnitCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Unit )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Unit frm = new MSPOSBACKOFFICE.Unit();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }

            //pnlMasterDisplay.Controls.Clear();
            //Unit brandform = new Unit();
            ////pnl_Mdi_Child.Visible = false;
            ////panel1.Visible = false;
            //brandform.TopLevel = false;
            ////brandform.AutoScroll = true;
            //pnlMasterDisplay.Controls.Add(brandform);
            //brandform.Show();
        }

        private void rbtnRackCreate_Click(object sender, EventArgs e)
        {
           //pnlMasterDisplay.Controls.Clear();
           // Rack rackform = new Rack();
           // rackform.TopLevel = false;
           // rackform.AutoScroll = true;
           //pnlMasterDisplay.Controls.Add(rackform);
           // rackform.Show();
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Rack)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Rack frm = new MSPOSBACKOFFICE.Rack();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rbtnPurcaseEntry_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.PurchaseEntry1)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.PurchaseEntry1 frm = new MSPOSBACKOFFICE.PurchaseEntry1("0");
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
                PurchaseEntry1 frm = new PurchaseEntry1("0");
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }

            //pnlEntryDisplay.Controls.Clear();
            //PurchaseEntry brandform = new PurchaseEntry("");
            ////pnl_Mdi_Child.Visible = false;
            ////panel1.Visible = false;
            //brandform.SendToBack();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlEntryDisplay.Controls.Add(brandform);
            //brandform.Show();
        }

        private void rbtnStockEntry_Click(object sender, EventArgs e)
        {
           // StckAdjDisplay frm1 = new StckAdjDisplay();
           // frm1.Close();
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.StockAdjustCreate)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.StockAdjustCreate frm = new MSPOSBACKOFFICE.StockAdjustCreate();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
               //frm.fromForm = "Create";
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }

            //pnlEntryDisplay.Controls.Clear();
            //StockAdjustCreate brandform = new StockAdjustCreate();
            //brandform.SendToBack();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlEntryDisplay.Controls.Add(brandform);
            //brandform.Show();
        }

        private void rbtnPurcaseDisplay_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.ListOfPurchase)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.ListOfPurchase frm = new MSPOSBACKOFFICE.ListOfPurchase();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
            //pnlEntryDisplay.Controls.Clear();
            //ListOfPurchase brandform = new ListOfPurchase();
            ////panel1.SendToBack();
            ////pnl_Mdi_Child.SendToBack();
            ////brandform.BringToFront();
            ////brandform.BringToFront();
            ////brandform.SendToBack();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlEntryDisplay.Controls.Add(brandform);
            //brandform.Show(); 
        }

        private void rbtnStockDisplay_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.StckAdjDisplay )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.StckAdjDisplay frm = new MSPOSBACKOFFICE.StckAdjDisplay();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
            //pnlEntryDisplay.Controls.Clear();
            //StckAdjDisplay brandform = new StckAdjDisplay();
            //brandform.SendToBack();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlEntryDisplay.Controls.Add(brandform);
            //brandform.Show(); 
        }

        private void rbtnItemSetting_Click(object sender, EventArgs e)
        {

        }

        private void rbtnDiscountSetting_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmDiscount_set )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmDiscount_set frm = new MSPOSBACKOFFICE.frmDiscount_set();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
            //pnlUtilsDisplay.Controls.Clear();
            //frmDiscount_set brandform = new frmDiscount_set();
            //brandform.SendToBack();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlUtilsDisplay.Controls.Add(brandform);
            //brandform.Show(); 
        }

        private void rbtnPrinterSetting_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is Receipt)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Receipt frm = new Receipt();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
            //pnlUtilsDisplay.Controls.Clear();
            //Receipt brandform = new Receipt();
            //brandform.SendToBack();
            //brandform.TopLevel = false;
            //brandform.AutoScroll = true;
            //pnlUtilsDisplay.Controls.Add(brandform);
            //brandform.Show(); 
        }
      //  frmBackOffice1 mdi = new frmBackOffice1();
        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            
           
            
            
        }

        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.ItemFilter )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                passingvalues.chckvalues = "0";
                passingvalues.gridcalculation = "2";
                MSPOSBACKOFFICE.ItemFilter frm = new MSPOSBACKOFFICE.ItemFilter();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmItemView)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmItemView frm = new MSPOSBACKOFFICE.frmItemView();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Itemalteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Itemalteration frm = new MSPOSBACKOFFICE.Itemalteration();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }

        }

        private void frmBackOffice1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void frmBackOffice1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmdUpdate = new SqlCommand("Update User_table set Active='False' where ctr_no=(select ctr_no from User_table where User_no=@tUsername)", con);
            cmdUpdate.Parameters.AddWithValue("@tUsername", SalesProject._Class.clsVariables.tUserNo);
            // cmdUpdate.Parameters.AddWithValue("@tPassword", tPassword);
            cmdUpdate.ExecuteNonQuery();
            UCSalesCreation frm = new UCSalesCreation();             
            this.Hide();
            frm.Show();
        }

        private void frmBackOffice1_Load(object sender, EventArgs e)
        {

        }

        private void rBtnSalesSummary_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmSalesSummary)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmSalesSummary frm = new MSPOSBACKOFFICE.frmSalesSummary();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnSalesItemwise_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmItemWiseSalesSummary)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmItemWiseSalesSummary frm = new MSPOSBACKOFFICE.frmItemWiseSalesSummary();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }
        private void rbtnStockLedger_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.ItemFilter)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.ItemLedger frm = new MSPOSBACKOFFICE.ItemLedger();
                frm.MdiParent = this;
                passingvalues.numbervaluestoledger = "1";
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
 
        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is ItemFilter)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                ItemFilter frm = new ItemFilter();
                passingvalues.chckvalues = "1";
                passingvalues.gridcalculation = "2";
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rbtnTax_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmTaxCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmTaxCreation frm = new MSPOSBACKOFFICE.frmTaxCreation();
                passingvalues.chckvalues = "1";
                passingvalues.gridcalculation = "2";
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnLedger_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is frmLedgerCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmLedgerCreation frm = new frmLedgerCreation();
                passingvalues.chckvalues = "1";
                passingvalues.gridcalculation = "2";
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnScale_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is frmPeripheralSettings)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmPeripheralSettings frm = new frmPeripheralSettings();
                passingvalues.chckvalues = "1";
                passingvalues.gridcalculation = "2";
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }

        }

        private void rBtnUser_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.UserCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.UserCreation frm = new MSPOSBACKOFFICE.UserCreation();               
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnUserAlter_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.UserAlteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.UserAlteration frm = new MSPOSBACKOFFICE.UserAlteration();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnBOM_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.SalesBOM)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.SalesBOM frm = new MSPOSBACKOFFICE.SalesBOM();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnCounter_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.CounterCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.CounterCreation frm = new MSPOSBACKOFFICE.CounterCreation();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnBOMMasterDisplay_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.SalesBomIssueDisplay)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.SalesBomIssueDisplay frm = new MSPOSBACKOFFICE.SalesBomIssueDisplay();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnBOMIssueCreation_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.SalesBOMIssueCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.SalesBOMIssueCreation frm = new MSPOSBACKOFFICE.SalesBOMIssueCreation();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnBOMAlteration_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.SalesBOMAlterion)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.SalesBOMAlterion frm = new MSPOSBACKOFFICE.SalesBOMAlterion();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnOrganizing_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Reorganize)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Reorganize frm = new MSPOSBACKOFFICE.Reorganize();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnMasterDisplayNew_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.SalesBOMMasterAltertion)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.SalesBOMMasterAltertion frm = new MSPOSBACKOFFICE.SalesBOMMasterAltertion();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void tabPanelEntry_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbtnRemovedItemDetail_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmRemoveitemdetailsSummary)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmRemoveitemdetailsSummary frm = new MSPOSBACKOFFICE.frmRemoveitemdetailsSummary();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void tab1_Click(object sender, EventArgs e)
        {

        }

        private void linkRemovedItemDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void rBtnBarcodeSetting_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmBarcodeSettings)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmBarcodeSettings frm = new MSPOSBACKOFFICE.frmBarcodeSettings();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnBarcodePrint_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.FrmBarcodePrinter)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.FrmBarcodePrinter frm = new MSPOSBACKOFFICE.FrmBarcodePrinter();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void rBtnCreditCard_Click(object sender, EventArgs e)
        {
            //Form currentForm = null;//declaring a variable to hold form.
            //foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            //{
            //    if (frm is FrmCreditCardCreation)//if any of the forms type is frmSub
            //    {
            //        currentForm = frm;//set that form to currentForm variable
            //        break;
            //    }
            //}
            //if (currentForm == null)//if form not found
            //{
            //    FrmCreditCardCreation frm = new FrmCreditCardCreation();
            //    frm.MdiParent = this;
            //    frm.StartPosition = FormStartPosition.Manual;
            //    frm.WindowState = FormWindowState.Normal;
            //    frm.Location = new Point(0, 80);
            //    frm.Show();
            //}
            //else//if form is already in child forms
            //{
            //    currentForm.BringToFront();
            //}
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                PictureBox picBox = (PictureBox)sender;
                picBox.BackgroundImage = Properties.Resources.B_on;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
            PictureBox picBox = (PictureBox)sender;
            picBox.BackgroundImage = null;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message);
            }
        }

        private void linkLedgerAlter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLedgerAlter_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is FrmLedgerAlteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                FrmLedgerAlteration frm = new FrmLedgerAlteration();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmdUpdate = new SqlCommand("Update User_table set Active='False' where ctr_no=(select ctr_no from User_table where User_no=@tUsername)", con);
            cmdUpdate.Parameters.AddWithValue("@tUsername", SalesProject._Class.clsVariables.tUserNo);
            // cmdUpdate.Parameters.AddWithValue("@tPassword", tPassword);
            cmdUpdate.ExecuteNonQuery();
            UCSalesCreation frm = new UCSalesCreation();
            this.Hide();
            frm.Show();
        }

        private void linkPurchaseVsSales_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmItemStock)
                //if (frm is MSPOSBACKOFFICE.FrmPurchaseSalesprofit)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                //MSPOSBACKOFFICE.FrmPurchaseSalesprofit frm = new MSPOSBACKOFFICE.FrmPurchaseSalesprofit();
                MSPOSBACKOFFICE.frmItemStock1 frm = new MSPOSBACKOFFICE.frmItemStock1();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void linkOfferCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Promotion)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Promotion frm = new MSPOSBACKOFFICE.Promotion();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void linkOfferAlter_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.Promotionalteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.Promotionalteration frm = new MSPOSBACKOFFICE.Promotionalteration();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void linkOfferCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void picSettings_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.ControlSettings)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.ControlSettings frm = new MSPOSBACKOFFICE.ControlSettings();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void lblColorSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is frmFormColor)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmFormColor frm = new frmFormColor();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void linkReOrganize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkPrinter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkDeliveryRecord_Click(object sender, EventArgs e)
        {
            //Form currentForm = null;//declaring a variable to hold form.
            //foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            //{
            //    if (frm is MSPOSBACKOFFICE.FrmDeliveryRecord)//if any of the forms type is frmSub
            //    {
            //        currentForm = frm;//set that form to currentForm variable
            //        break;
            //    }
            //}
            //if (currentForm == null)//if form not found
            //{
            //    MSPOSBACKOFFICE.FrmDeliveryRecord frm = new MSPOSBACKOFFICE.FrmDeliveryRecord();
            //    frm.MdiParent = this;
            //    frm.StartPosition = FormStartPosition.Manual;
            //    frm.WindowState = FormWindowState.Normal;
            //    frm.Location = new Point(0, 80);
            //    frm.Show();
            //}
            //else//if form is already in child forms
            //{
            //    currentForm.BringToFront();
            //}
        }

        private void linkDailySalesForSalesMan_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.FrmDailySalesForSalesManItems)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.FrmDailySalesForSalesManItems frm = new MSPOSBACKOFFICE.FrmDailySalesForSalesManItems();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void linkDailySalesForSalesmanAmount_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.FrmDailySalesForSalesMan)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.FrmDailySalesForSalesMan frm = new MSPOSBACKOFFICE.FrmDailySalesForSalesMan();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void linkVoucherEntry_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Form currentForm = null;//declaring a variable to hold form.
            //foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            //{
            //    if (frm is MSPOSBACKOFFICE.FrmVoucherDisplay)//if any of the forms type is frmSub
            //    {
            //        currentForm = frm;//set that form to currentForm variable
            //        break;
            //    }
            //}
            //if (currentForm == null)//if form not found
            //{
            //    MSPOSBACKOFFICE.FrmVoucherDisplay frm = new MSPOSBACKOFFICE.FrmVoucherDisplay();
            //    frm.MdiParent = this;
            //    frm.StartPosition = FormStartPosition.Manual;
            //    frm.WindowState = FormWindowState.Normal;
            //    frm.Location = new Point(0, 80);
            //    frm.Show();
            //}
            //else//if form is already in child forms
            //{
            //    currentForm.BringToFront();
            //}
        }

        private void linkDaybook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Form currentForm = null;//declaring a variable to hold form.
            //foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            //{
            //    if (frm is MSPOSBACKOFFICE.frmDayBook)//if any of the forms type is frmSub
            //    {
            //        currentForm = frm;//set that form to currentForm variable
            //        break;
            //    }
            //}
            //if (currentForm == null)//if form not found
            //{
            //    MSPOSBACKOFFICE.frmDayBook frm = new MSPOSBACKOFFICE.frmDayBook();
            //    frm.MdiParent = this;
            //    frm.StartPosition = FormStartPosition.Manual;
            //    frm.WindowState = FormWindowState.Normal;
            //    frm.Location = new Point(0, 80);
            //    frm.Show();
            //}
            //else//if form is already in child forms
            //{
            //    currentForm.BringToFront();
            //}
        }

        private void linkDevice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void tab2_Click(object sender, EventArgs e)
        {

        }

        private void tabPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lnkLedgerReport_Click(object sender, EventArgs e)
        {
             Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.frmLedgerReport)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.frmLedgerReport frm = new MSPOSBACKOFFICE.frmLedgerReport();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void lblItemstockReport_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                //if (frm is MSPOSBACKOFFICE.frmSalesEdit)//if any of the forms type is frmSub
                if (frm is MSPOSBACKOFFICE.frmItemStock)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                //MSPOSBACKOFFICE.frmItemStock frm = new MSPOSBACKOFFICE.frmItemStock();
                MSPOSBACKOFFICE.frmItemStock frm = new MSPOSBACKOFFICE.frmItemStock();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void pictureBox43_Click(object sender, EventArgs e)
        {

        }

        

        private void pictureBox44_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.BranchTransferCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.BranchTransferCreation frm = new MSPOSBACKOFFICE.BranchTransferCreation();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        }

        private void btnBranch_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is MSPOSBACKOFFICE.BranchCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                MSPOSBACKOFFICE.BranchCreation frm = new MSPOSBACKOFFICE.BranchCreation();
                frm.MdiParent = this;
                frm.StartPosition = FormStartPosition.Manual;
                frm.WindowState = FormWindowState.Normal;
                frm.Location = new Point(0, 80);
                frm.Show();
            }
            else//if form is already in child forms
            {
                currentForm.BringToFront();
            }
        } 
            

    }
}
