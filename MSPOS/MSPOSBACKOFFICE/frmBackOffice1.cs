using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;



namespace MSPOSBACKOFFICE
{
    public partial class frmBackOffice1 : Form
    {
        public frmBackOffice1()
        {
            InitializeComponent();
        }

        private void rbtnItemCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is ItemCreations)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                ItemCreations frm = new ItemCreations("");
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
                if (frm is frmGroupCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmGroupCreation frm = new frmGroupCreation();
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

           // frmGroupCreation brandform = new frmGroupCreation();
           // pnlMasterDisplay.Controls.Clear();
           //// ItemCreations brandform = new ItemCreations();
           // brandform.TopLevel = false;
           // brandform.AutoScroll = true;
           // pnlMasterDisplay.Controls.Add(brandform);
           // brandform.Show();

         //   ElementHost elhost = new ElementHost();
         //  elhost.Size = new Size(1200, 620);
         //  elhost.Location = new Point(0,0);

         ////   GroupItemSetting uc1 = new GroupItemSetting();
         //   Window2 uc2 = new Window2();
         //   elhost.Child = uc2;
         //   pnlMasterDisplay.Controls.Add(elhost);
          //  this.Controls.Add(elhost);
            //pnlMasterDisplay.Controls.Clear();
            //pnlMasterDisplay.Controls.Add(host);
            //uc1 = new GroupItemSetting();
            //uc1.InitializeComponent();
            //host.Child = uc1;
           
           // var wpfwindow = new SalesProject.GroupItemSetting();
           // System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(wpfwindow);
           // wpfwindow.ShowDialog();

            

          //  System.Windows.Forms.Integration.ElementHost host = new System.Windows.Forms.Integration.ElementHost();
          ////  GroupItemSetting uc1 = new GroupItemSetting();
          //  host.Controls.Add((wind)uc1);
          //  host.Controls.Add(uc1);
          //  host.Dock = DockStyle.Fill;
          //  this.panel1.Controls.Add(host);

        }

        private void rbtnBrandCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is Brand)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Brand frm = new Brand();
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
                if (frm is Model)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Model frm = new Model();
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
                if (frm is Unit )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Unit frm = new Unit();
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
                if (frm is Rack)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Rack frm = new Rack();
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
                if (frm is PurchaseEntry1)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                PurchaseEntry1 frm = new PurchaseEntry1("0");
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
                if (frm is StockAdjustCreate )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                StockAdjustCreate frm = new StockAdjustCreate();
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
                if (frm is ListOfPurchase)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                ListOfPurchase frm = new ListOfPurchase();
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
                if (frm is StckAdjDisplay )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                StckAdjDisplay frm = new StckAdjDisplay();
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
                if (frm is frmDiscount_set )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmDiscount_set frm = new frmDiscount_set();
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
                if (frm is ItemFilter )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                passingvalues.chckvalues = "0";
                passingvalues.gridcalculation = "2";
                ItemFilter frm = new ItemFilter();
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
                if (frm is frmItemView )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
               frmItemView frm = new frmItemView();
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
                if (frm is Itemalteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Itemalteration frm = new Itemalteration();
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
            FrmLogin frm = new FrmLogin();
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
                if (frm is frmSalesSummary )//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmSalesSummary frm = new frmSalesSummary();
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
                if (frm is frmItemWiseSalesSummary)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmItemWiseSalesSummary frm = new frmItemWiseSalesSummary();
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
                if (frm is ItemLedger)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                ItemLedger frm = new ItemLedger();
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
                if (frm is frmTaxCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmTaxCreation frm = new frmTaxCreation();
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
                if (frm is UserCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                UserCreation frm = new UserCreation();               
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
                if (frm is UserAlteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                UserAlteration frm = new UserAlteration();
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
                if (frm is SalesBOM)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                SalesBOM frm = new SalesBOM();
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
                if (frm is CounterCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                CounterCreation frm = new CounterCreation();
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
                if (frm is SalesBomIssueDisplay)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                SalesBomIssueDisplay frm = new SalesBomIssueDisplay();
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
                if (frm is SalesBOMIssueCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                SalesBOMIssueCreation frm = new SalesBOMIssueCreation();
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
                if (frm is SalesBOMAlterion)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                SalesBOMAlterion frm = new SalesBOMAlterion();
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
                if (frm is Reorganize)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Reorganize frm = new Reorganize();
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
                if (frm is SalesBOMMasterAltertion)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                SalesBOMMasterAltertion frm = new SalesBOMMasterAltertion();
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
                if (frm is frmRemoveitemdetailsSummary)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmRemoveitemdetailsSummary frm = new frmRemoveitemdetailsSummary();
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
                if (frm is frmBarcodeSettings)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                frmBarcodeSettings frm = new frmBarcodeSettings();
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
                if (frm is FrmBarcodePrinter)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                FrmBarcodePrinter frm = new FrmBarcodePrinter();
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
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is FrmCreditCardCreation)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                FrmCreditCardCreation frm = new FrmCreditCardCreation();
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
            FrmLogin frm = new FrmLogin();
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

        private void linkOfferAlter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkOfferCreate_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is Promotion)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Promotion frm = new Promotion();
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

        private void picOfferAlter_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is Promotionalteration)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                Promotionalteration frm = new Promotionalteration();
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

        private void picSettings_Click(object sender, EventArgs e)
        {
            Form currentForm = null;//declaring a variable to hold form.
            foreach (Form frm in this.MdiChildren)//loop in all child forms in mdi
            {
                if (frm is ControlSettings)//if any of the forms type is frmSub
                {
                    currentForm = frm;//set that form to currentForm variable
                    break;
                }
            }
            if (currentForm == null)//if form not found
            {
                ControlSettings frm = new ControlSettings();
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

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            BranchTransferCreation btc = new BranchTransferCreation();
            btc.Show();
        }     
        

    }
}
