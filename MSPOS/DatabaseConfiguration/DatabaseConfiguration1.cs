using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.SqlServer;
using System.Xml;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;
using Microsoft.Win32;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseConfiguration
{
    public partial class DatabaseConfiguration1 : Form
    {
        public DatabaseConfiguration1()
        {
            InitializeComponent();
        }

        private void DatabaseConfiguration1_Load(object sender, EventArgs e)
        {
           // DataTable dt = Microsoft.SqlServer.Management.Smo.SmoApplication.EnumAvailableSqlServers();
            //cmbServerName.DataSource = dt;
            System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            System.Data.DataTable dataTable = instance.GetDataSources();
            cmbServerName.Items.Clear();
            cmbBackupServerName.Items.Clear();
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                cmbServerName.Items.Add(dataTable.Rows[j]["ServerName"]);
                cmbBackupServerName.Items.Add(dataTable.Rows[j]["ServerName"]);
            }
            cmbTimeOut.Items.Clear();
            for(int i=1;i<=240;i++)
            {
                cmbTimeOut.Items.Add(i);
            }
            cmbTimeOut.SelectedIndex=4;
            radioWindowsAuthentication.Checked = true;
            txtLogin.Text = "";
            txtPassword.Text = "";
            txtLogin.Enabled = false;
            txtPassword.Enabled = false;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkCustomConnectionString.Checked == false)
                {
                    StringBuilder Con = new StringBuilder("Data Source=");
                    if (radioServerAuthentication.Checked)
                    {
                        //Constructing connection string from the inputs
                        // StringBuilder Con = new StringBuilder("Data Source=");
                        Con.Append(cmbServerName.Text);
                        Con.Append(";Initial Catalog=");
                        Con.Append(txtDatabaseName.Text);
                        Con.Append(";Persist Security Info=True;Connect Timeout=");
                        //  Con.Append("Connect Timeout=");
                        Con.Append(cmbTimeOut.Text + ";");
                        Con.Append("User ID=");
                        Con.Append(txtLogin.Text);
                        Con.Append(";Password=");
                        Con.Append(txtPassword.Text + ";");

                    }
                    if (radioWindowsAuthentication.Checked)
                    {
                        //Constructing connection string from the inputs

                        Con.Append(cmbServerName.Text);
                        Con.Append(";Initial Catalog=");
                        Con.Append(txtDatabaseName.Text);
                        Con.Append(";Integrated Security=True;");
                        Con.Append("Connect Timeout=");
                        Con.Append(cmbTimeOut.SelectedItem.ToString() + ";");

                    }
                    //Persist Security Info=True;User ID=sa;Password=!Password123"
                    txtCustomConnectionString.Text = Con.ToString();
                }
               // updateConfigFile(strCon);
                //Create new sql connection
                SqlConnection Db = new SqlConnection();
                //to refresh connection string each time else it will use             previous connection string
                ConfigurationManager.RefreshSection("connectionStrings");
                //Db.ConnectionString = ConfigurationManager.ConnectionStrings["POS"].ToString();
                Db.ConnectionString = txtCustomConnectionString.Text;
                try
                {
                    Db.Open();
                    if (Db.State == ConnectionState.Open)
                    {
                        MyMessageBox.ShowBox("Test Connection Success", "Success");
                    }
                    Db.Close();
                }
                catch (Exception)
                {
                    MyMessageBox.ShowBox("Test Connection Failure!", "Warning");
                }
               // MessageBox.Show(ConfigurationManager.ConnectionStrings["POS"].ToString(), "Connection Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }         
        }
       public void updateConfigFile(string ptcon)
       {
           try
           {

               // Create a new DirectoryInfo object.
               System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(Application.StartupPath);

               // Get a DirectorySecurity object that represents the
               // current security settings.
               DirectorySecurity dSecurity = dInfo.GetAccessControl();

               // Add the FileSystemAccessRule to the security settings.
               //dSecurity.AddAccessRule(
               //    new FileSystemAccessRule(
               //        new System.Security.Principal.NTAccount("UserGroupGoesHere"),
               //        FileSystemRights.DeleteSubdirectoriesAndFiles,
               //        AccessControlType.Allow
               //    )
               //);

               // Set the new access settings.
               dInfo.SetAccessControl(dSecurity);
               foreach (string file in System.IO.Directory.EnumerateFiles(Application.StartupPath, "*.config"))
               {
                   //string contents = File.ReadAllText(file);
                   //DirectoryInfo dInfo1 = new DirectoryInfo(Application.StartupPath);
                   //DirectorySecurity dSecurity1 = dInfo.GetAccessControl();
                   //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                   ////dSecurity.AddAccessRule(new FileSystemAccessRule((WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow))); // For testing, I don't know if this line is right or has an effect
                   //dInfo.SetAccessControl(dSecurity1);
                  // throw new ApplicationException("Folder Permission is Not Creation");

                   if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\ItemImage"))
                   {
                       Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\ItemImage");
                   }
                   if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Logo"))
                   {
                       Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Logo");
                   }
                   if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\GroupImage"))
                   {
                       Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\GroupImage");
                   }
                   if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\OfferImage"))
                   {
                       Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\OfferImage");
                   }
                   if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\ItemModifiersImage"))
                   {
                       Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\ItemModifiersImage");
                   }
                   if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\ItemSubModifiersImage"))
                   {
                       Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\ItemSubModifiersImage");
                   }
                   //updating config file
                   XmlDocument XmlDoc = new XmlDocument();
                   //Loading the Config file
                   // XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                   XmlDoc.Load(file);
                   //  XmlDoc.Load(System.Windows.Forms.Application.ExecutablePath);
                   foreach (XmlElement xElement in XmlDoc.DocumentElement)
                   {
                       if (xElement.Name == "connectionStrings")
                       {
                           //setting the coonection string
                           xElement.FirstChild.Attributes[1].Value = ptcon;
                       }
                   }
                   //writing the connection string in config file
                   XmlDoc.Save(file);
                   //txt_customConstring.Show();
                   // txt_conn_string.Text = ptcon;
               }
               ConfigurationManager.RefreshSection("appSettings");
           }
           catch (ApplicationException ex)
           {
               MyMessageBox.ShowBox(ex.ToString(), "Warning");
           }
        }

       private void radioWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
       {
           if (radioWindowsAuthentication.Checked == true)
           {
               txtLogin.Enabled = false;
               txtPassword.Enabled = false;
           }
           else
           {
               txtLogin.Enabled = true;
               txtPassword.Enabled = true;
           }
       }

       private void radioServerAuthentication_CheckedChanged(object sender, EventArgs e)
       {
           if (radioWindowsAuthentication.Checked == true)
           {
               txtLogin.Text = "";
               txtPassword.Text = "";
               txtLogin.Enabled = false;
               txtPassword.Enabled = false;
           }
           else
           {
               txtLogin.Text = "";
               txtPassword.Text="";
               txtLogin.Enabled = true;
               txtPassword.Enabled = true;
           }
       }

       private void btnSaveSettings_Click(object sender, EventArgs e)
       {
           try
           {
               if (chkCustomConnectionString.Checked == false)
               {
                   StringBuilder Con = new StringBuilder("Data Source=");
                   if (radioServerAuthentication.Checked)
                   {
                       //Constructing connection string from the inputs
                       // StringBuilder Con = new StringBuilder("Data Source=");
                       Con.Append(cmbServerName.Text);
                       Con.Append(";Initial Catalog=");
                       Con.Append(txtDatabaseName.Text);
                       Con.Append(";Persist Security Info=True;");
                       if (cmbTimeOut.Text.Trim() != "0")
                       {
                           Con.Append("Connect Timeout=");
                           Con.Append(cmbTimeOut.Text + ";");
                       }
                       Con.Append("User ID=");
                       Con.Append(txtLogin.Text);
                       Con.Append(";Password=");
                       Con.Append(txtPassword.Text + ";");
                   }
                   if (radioWindowsAuthentication.Checked)
                   {
                       //Constructing connection string from the inputs

                       Con.Append(cmbServerName.Text);
                       Con.Append(";Initial Catalog=");
                       Con.Append(txtDatabaseName.Text);
                       Con.Append(";Integrated Security=True;");
                       if (cmbTimeOut.Text.Trim() != "0")   
                       {
                           Con.Append("Connect Timeout=");
                           Con.Append(cmbTimeOut.SelectedItem.ToString() + ";");
                       }
                   }
                   //Persist Security Info=True;User ID=sa;Password=!Password123"
                   txtCustomConnectionString.Text = Con.ToString();
               }               
               updateConfigFile(txtCustomConnectionString.Text);
             
           }
           catch (Exception ex)
           {
               MyMessageBox.ShowBox(ex.Message, "Warning");
           }
       }

       private void chkCustomConnectionString_CheckedChanged(object sender, EventArgs e)
       {
           try
           {
               if (chkCustomConnectionString.Checked == true)
               {
                   txtCustomConnectionString.Visible = true;
                   txtCustomConnectionString.Select();
               }
               else
               {
                   txtCustomConnectionString.Visible = false;
                   txtDatabaseName.Select();
               }
           }
           catch (Exception ex)
           {
               MyMessageBox.ShowBox(ex.Message,"Warning");
           }
       }

       private void btnLoadSettings_Click(object sender, EventArgs e)
       {
           try
           {
               foreach (string file in System.IO.Directory.EnumerateFiles(Application.StartupPath, "*.config"))
               {
                   //   string contents = File.ReadAllText(file);

                   //updating config file
                   XmlDocument XmlDoc = new XmlDocument();
                   //Loading the Config file
                   // XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                   XmlDoc.Load(file);
                   //  XmlDoc.Load(System.Windows.Forms.Application.ExecutablePath);
                   foreach (XmlElement xElement in XmlDoc.DocumentElement)
                   {
                       if (xElement.Name == "connectionStrings")
                       {
                           chkCustomConnectionString.Checked = false;
                           //setting the coonection string
                          string last= xElement.FirstChild.Attributes[1].Value;
                            string lastMain= xElement.FirstChild.Attributes[1].Value;
                          txtCustomConnectionString.Text = last;
                          string testStr = "Data Source=";
                          int Chk = last.IndexOf(testStr);
                          //int indexLen = Convert.ToString();
                          if (Chk != -1)
                          {
                              last = last.Substring(Chk + testStr.Length, last.Length - (Chk + testStr.Length));
                              testStr = ";Initial Catalog=";
                              Chk = last.IndexOf(testStr);

                              if (Chk > 0)
                              {
                                  try
                                  {
                                      cmbServerName.Text = last.Substring(0, Chk);
                                      last = last.Substring(Chk + testStr.Length, last.Length - (Chk + testStr.Length));
                                      testStr = ";Persist Security Info=True;Connect Timeout=";
                                      Chk = last.IndexOf(testStr);

                                      if (Chk > 0)
                                      {
                                          radioServerAuthentication.Checked = true;
                                          txtDatabaseName.Text = last.Substring(0, Chk);
                                          last = last.Substring(Chk + testStr.Length, last.Length - (Chk + testStr.Length));
                                          testStr = ";User ID=";
                                          Chk = last.IndexOf(testStr);
                                          if (Chk > 0)
                                          {
                                              cmbTimeOut.Text = last.Substring(0, Chk);
                                              last = last.Substring(Chk + testStr.Length, last.Length - (Chk + testStr.Length));
                                              testStr = ";Password=";
                                              Chk = last.IndexOf(testStr);
                                              txtLogin.Text = last.Substring(0, Chk);
                                          }
                                      }
                                      else
                                      {
                                          radioWindowsAuthentication.Checked = true;
                                          testStr = ";Integrated Security=True;Connect Timeout=";
                                          Chk = last.IndexOf(testStr);
                                          txtDatabaseName.Text = last.Substring(0, Chk);
                                          last = last.Substring(Chk + testStr.Length, last.Length - (Chk + testStr.Length));
                                          cmbTimeOut.Text = last.TrimEnd(';');

                                      }
                                  }
                                  catch (Exception)
                                  {
                                      cmbServerName.Text = "";
                                      chkCustomConnectionString.Checked = true;
                                      txtCustomConnectionString.Text = lastMain;
                                  }
                              }
                              else
                              {
                                  cmbServerName.Text = "";
                                  chkCustomConnectionString.Checked = true;
                                  txtCustomConnectionString.Text = lastMain;
                              }
                          }
                          else
                          {
                              cmbServerName.Text = "";
                              chkCustomConnectionString.Checked = true;
                              txtCustomConnectionString.Text = lastMain;
                          }
                          break;
                       }
                   }
                   //writing the connection string in config file
                //   XmlDoc.Save(file);
                   //txt_customConstring.Show();
                   // txt_conn_string.Text = ptcon;
               }
           }
           catch (Exception ex)
           {
               MyMessageBox.ShowBox(ex.Message,"Warning");
           }
       }
       private void linkBtnConfiguration_Click(object sender, EventArgs e)
       {
           TabStripDBConfiguration.SelectedIndex = 0;
       }

       private void linkBtnBackup_Click(object sender, EventArgs e)
       {
           
           string fileUNQ = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
           txtBackUpFileName.Text = fileUNQ+".bak";
           TabStripDBConfiguration.SelectedIndex = 1;
       }

       private void btnFolderLocation_Click(object sender, EventArgs e)
       {
           FolderBrowserDialog folderDlg = new FolderBrowserDialog();
           folderDlg.ShowNewFolderButton = true;
           // Show the FolderBrowserDialog.
           DialogResult result = folderDlg.ShowDialog();
           if (result == DialogResult.OK)
           {
               txtFolderLocation.Text = folderDlg.SelectedPath;
               Environment.SpecialFolder root = folderDlg.RootFolder;
           }
       }

       private void TabStripDBConfiguration_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (TabStripDBConfiguration.SelectedIndex == 1)
           {
               linkBtnBackup_Click(sender, e);
           }
       }

       private void btnBackup_Click(object sender, EventArgs e)
       {
           try
           {
               if (cmbBackupServerName.Text.Trim() != "")
               {
                   if (txtBackupDatabaseName.Text.Trim() != "")
                   {
                       if (txtFolderLocation.Text.Trim() != "")
                       {
                           if (txtBackUpFileName.Text.Trim() != "")
                           {
                               if (txtBackUpFileName.Text.IndexOf(".bak") != -1)
                               {
                                   if (System.IO.Directory.Exists(txtFolderLocation.Text))
                                   {
                                       BackupDatabase(txtFolderLocation.Text, txtBackUpFileName.Text, txtBackupDatabaseName.Text, cmbBackupServerName.Text);
                                   }
                                   else
                                   {
                                       MyMessageBox.ShowBox("Select Valid Folder Location", "Warning");
                                   }
                               }
                               else
                               {
                                   MyMessageBox.ShowBox("Backup Filename should end with .bak","Warning");
                                   txtBackUpFileName.Select();
                               }
                           }
                           else
                           {
                               MyMessageBox.ShowBox("Enter Backup file name", "Warning");
                               txtBackUpFileName.Select();
                           }
                       }
                       else
                       {
                           MyMessageBox.ShowBox("Select Backup Folder Location", "Warning");
                           btnFolderLocation.Select();
                       }
                   }
                   else
                   {
                       MyMessageBox.ShowBox("Should Enter Database Name", "Warning");
                       txtBackupDatabaseName.Select();

                   }
               }
               else
               {
                   MyMessageBox.ShowBox("Select Server Name", "Warning");
                   cmbBackupServerName.Select();
               }
           }
           catch (Exception ex)
           {
               MyMessageBox.ShowBox(ex.Message, "Warning");
           }
       }
       public void BackupDatabase(string BackUpLocation, string BackUpFileName, string DatabaseName, string ServerName)
       {
           DatabaseName = "[" + DatabaseName + "]";
           string fileUNQ = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();

          // BackUpFileName = BackUpFileName + fileUNQ + ".bak";
           string SQLBackUp = @"BACKUP DATABASE " + DatabaseName + " TO DISK = N'" + BackUpLocation + @"\" + BackUpFileName + @"'";

           string svr = "Server=" + ServerName + ";Database=master;Integrated Security=True";

           SqlConnection cnBk = new SqlConnection(svr);
           SqlCommand cmdBkUp = new SqlCommand(SQLBackUp, cnBk);
           try
           {
               cnBk.Open();
               cmdBkUp.ExecuteNonQuery();
               MyMessageBox.ShowBox(BackUpFileName + " Backup Created Successfully");
            //   Label1.Text = "Done";
            //   Label2.Text = SQLBackUp + " ######## Server name " + ServerName + " Database " + DatabaseName + " successfully backed up to " + BackUpLocation + @"\" + BackUpFileName + "\n Back Up Date : " + DateTime.Now.ToString();
           }
           catch (Exception ex)
           {
               MyMessageBox.ShowBox(ex.Message, "Warning");
             //  Label1.Text = ex.ToString();
              // Label2.Text = SQLBackUp + " ######## Server name " + ServerName + " Database " + DatabaseName + " successfully backed up to " + BackUpLocation + @"\" + BackUpFileName + "\n Back Up Date : " + DateTime.Now.ToString();
           }
           finally
           {
               if (cnBk.State == ConnectionState.Open)
               {
                   cnBk.Close();
               }
           }
       }
       private void btnMakeNewDB_Click(object sender, EventArgs e)
       {
           Cursor.Current = Cursors.WaitCursor;
           try
           {
               if (!string.IsNullOrEmpty(cmbBackupServerName.Text) && !string.IsNullOrEmpty(txtNewDBName.Text) && !string.IsNullOrEmpty(txtSqlLogin.Text) && !string.IsNullOrEmpty(txtSqlPassWord.Text))
               {
                   SqlConnection connect;
                   string con = "Data Source =" + cmbBackupServerName.Text + "; Initial Catalog=master ;Integrated Security = True;";
                   connect = new SqlConnection(con);
                   string filename1 = Application.StartupPath + "\\" + "SqlQuery" + "\\" + "TablesCreation.txt";
                   SqlCommand cmd = new SqlCommand("select * from master.dbo.sysdatabases where name='" + txtNewDBName.Text.Trim() + "'", connect);
                   SqlDataAdapter adp = new SqlDataAdapter(cmd);
                   DataTable dt = new DataTable();
                   dt.Rows.Clear();
                   adp.Fill(dt);
                   if (dt.Rows.Count > 0)//Check Db Already Exits or not 
                   {
                       // MessageBox.Show("Already Exists this Databasename, Please Change The Name ", "Warning");
                       if (connect.State != ConnectionState.Open)
                       {
                           connect.Open();
                       }
                       SqlConnection connew = new SqlConnection("Data Source =" + cmbBackupServerName.Text + "; Initial Catalog=" + txtNewDBName.Text.Trim() + " ;Integrated Security = True");
                       if (connew.State != ConnectionState.Open)
                       {
                           connew.Open();
                       }
                       //Delete View:
                       SqlCommand cmddeleteview = new SqlCommand("select * FROM sys.views where schema_id='1'", connew);
                       SqlDataAdapter adpdeleteview = new SqlDataAdapter(cmddeleteview);
                       DataTable dtdeleteview = new DataTable();
                       dtdeleteview.Rows.Clear();
                       adpdeleteview.Fill(dtdeleteview);
                       if (dtdeleteview.Rows.Count > 0)//Drop Every View,Type And Proceudre:
                       {

                           using (SqlCommand cmd1 = new SqlCommand(@"Declare @viewName varchar(500) 
                                                                    Declare cur Cursor For Select [name] From sys.objects where type = 'v' 
                                                                    Open cur 
                                                                    Fetch Next From cur Into @viewName 
                                                                    While @@fetch_status = 0 
                                                                    Begin 
                                                                        Exec('drop view ' + @viewName) 
                                                                        Fetch Next From cur Into @viewName 
                                                                    End
                                                                    Close cur 
                                                                    Deallocate cur ", connew))//Drop View
                               cmd1.ExecuteNonQuery();

                           using (SqlCommand cmd1 = new SqlCommand(@"Declare @trgName varchar(500) 
                                                                    Declare cur Cursor For Select [name] From sys.objects where type = 'tr' 
                                                                    Open cur 
                                                                    Fetch Next From cur Into @trgName 
                                                                    While @@fetch_status = 0 
                                                                    Begin 
                                                                    Exec('drop trigger ' + @trgName) 
                                                                    Fetch Next From cur Into @trgName 
                                                                    End
                                                                    Close cur 
                                                                    Deallocate cur ", connew))//Drop Trigger
                               cmd1.ExecuteNonQuery();

                           using (SqlCommand cmd1 = new SqlCommand(@"Declare @procName varchar(500) 
                                                                    Declare cur Cursor For Select [name] From sys.objects where type = 'p' 
                                                                    Open cur 
                                                                    Fetch Next From cur Into @procName 
                                                                    While @@fetch_status = 0 
                                                                    Begin 
                                                                    Exec('drop procedure ' + @procName) 
                                                                    Fetch Next From cur Into @procName 
                                                                    End
                                                                    Close cur 
                                                                    Deallocate cur  ", connew))//Drop Procedure
                               cmd1.ExecuteNonQuery();
                                                                                               //Type Drop 
                           using (SqlCommand cmd1 = new SqlCommand("drop Type DgDiscountTable", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type dtSingleFreeSales", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Sp_BarCodeTable", connew))
                               cmd1.ExecuteNonQuery();
                           //using (SqlCommand cmd1 = new SqlCommand("drop Type ModifiersItem_Type", connew))
                           //    cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type SP_BomCreation", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Sp_BomMasterissue", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type sp_funBtnDolorAlterTable", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Sp_issuecreation", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type SP_PurAlterType", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type SP_PurchaseGridEntry", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type SP_PurchaseItemAlter", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type SP_PurchaseTypeAlter", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_DOSale", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_DOSales", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Type_FreeItem1", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Type_FreeItem2", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Type_gridValue", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type Type_gridValue5", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_SalesAlter", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_SalesAlteration", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_SalesCreate", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_SalesCreation", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type type_StockAdjCreate", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type TypeFreeOferItem", connew))
                               cmd1.ExecuteNonQuery();
                           using (SqlCommand cmd1 = new SqlCommand("drop Type TypeOfferItem", connew))
                               cmd1.ExecuteNonQuery();
                       }
                       
                       ExecuteView(); //Create View 
                       filename1 = Application.StartupPath + "\\" + "SqlQuery" + "\\" + "TypeCreation.txt";    //Type Creation:
                       ExecuteSqpProcedure(filename1); //Type Creation:
                       string script = "";
                       DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\" + "SqlQuery");//Script Scration
                       FileInfo[] rgFiles = di.GetFiles("OrginalScriptFile.sql");
                       foreach (FileInfo fi in rgFiles)
                       {
                           FileInfo fileInfo = new FileInfo(fi.FullName);
                           script = fileInfo.OpenText().ReadToEnd();
                           //Code Delete All Proceudre :
                               //declare @procName varchar(500)
                               //declare cur cursor 
                               //for select [name] from sys.objects where type = 'p'
                               //open cur
                               //fetch next from cur into @procName
                               //while @@fetch_status = 0
                               //begin
                               //    exec('drop procedure ' + @procName)
                               //    fetch next from cur into @procName
                               //end
                               //close cur
                               //deallocate cur
                       }

                       //Creating Index in sp:
                       IndexCreation();

                       //Script File execute location and ScriptName
                       ExecuteScriptfiles(filename1, script);
                       txtSqlPassWord.Text = "";
                       txtSqlLogin.Text = "";
                       txtNewDBName.Text = "";
                   }
                   else
                   {
                       //Make New database Code on local System only
                       string paths = Application.StartupPath + txtNewDBName.Text.ToString().Trim() + ".mdf";
                       string pathlog = Application.StartupPath + txtNewDBName.Text.ToString().Trim() + ".ldf";
                       string query = "CREATE DATABASE " + txtNewDBName.Text.Trim() +
                       " ON PRIMARY" +
                       " (NAME = " + txtNewDBName.Text.Trim() + "_data," +

                       " FILENAME = '" + paths.ToString() + "'," +
                       " SIZE = 3MB," +
                       " MAXSIZE = 10MB," +
                       " FILEGROWTH = 10%)" +
                       " LOG ON" +
                       " (NAME = " + txtNewDBName.Text.Trim() + "_log," +
                       " FILENAME = '" + pathlog.ToString().Trim() + "'," +
                       " SIZE = 1MB," +
                       " MAXSIZE = 10MB," +
                       " FILEGROWTH = 10%)" +
                       ";";
                       SqlCommand cmdCreateDB = new SqlCommand(query, connect);
                       if (connect.State != ConnectionState.Open)
                       {
                           connect.Open();
                       }
                       cmdCreateDB.ExecuteNonQuery();
                       con.Clone();
                       CreationAll();
                       MyMessageBox.ShowBox("Database is created successfully", "Success");
                   }
               }
               else
               {
                   if (string.IsNullOrEmpty(cmbBackupServerName.Text))
                   {
                       MyMessageBox.ShowBox("!Please Enter The Server Name", "Warning");
                   }
                   else if (string.IsNullOrEmpty(txtNewDBName.Text))
                   {
                       MyMessageBox.ShowBox("!Please Enter The Database Name", "Warning");
                   }
                   else if (string.IsNullOrEmpty(txtSqlLogin.Text))
                   {
                       MyMessageBox.ShowBox("!Please Enter The Server Login Name", "Warning");
                   }
                   else if (string.IsNullOrEmpty(txtSqlPassWord.Text))
                   {
                       MyMessageBox.ShowBox("!Please Enter The Server Password Name", "Warning");
                   }
               }
           }
           catch (Exception exp)
           {
               MyMessageBox.ShowBox(exp.Message, "Warning");
           }
           //finally
           //{ }
       }

       public void CreationAll()
       {
           try {

               string filename1 = Application.StartupPath + "\\" + "SqlQuery" + "\\" + "TablesCreation.txt";
               ExecuteSqpProcedure(filename1);                                                         //Table Creation:
               ExecuteView();                                                                          //View Creation:
               filename1 = Application.StartupPath + "\\" + "SqlQuery" + "\\" + "TypeCreation.txt";    //Type Creation:
               ExecuteSqpProcedure(filename1);
               string script = "";                                                                     //Script Creation:
               DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\" + "SqlQuery");
               FileInfo[] rgFiles = di.GetFiles("OrginalScriptFile.sql");
               foreach (FileInfo fi in rgFiles)
               {
                   FileInfo fileInfo = new FileInfo(fi.FullName);
                   script = fileInfo.OpenText().ReadToEnd();
               }
               ExecuteScriptfiles(filename1, script);
               //Index Creation Place:
               IndexCreation();
           }
           catch
           { }
           //finally { }
       }
       public void ExecuteScriptfiles(string fileLocation, string script)
       {
           try
           {
               SqlConnection connew = new SqlConnection("Data Source =" + cmbBackupServerName.Text + "; Initial Catalog=" + txtNewDBName.Text.Trim() + " ;Integrated Security = True");
               if (connew.State != ConnectionState.Open)
               {
                   connew.Open();
               }
               //get the script location
               string scriptText = fileLocation;
               //split the script on "GO" commands
               string[] splitter = new string[] { "\r\nGO\r\n" };
               string[] commandTexts = script.Split(splitter,
                 StringSplitOptions.RemoveEmptyEntries);
               foreach (string commandText in commandTexts)
               {
                   Server server = new Server(new ServerConnection(connew));
                   server.ConnectionContext.ExecuteNonQuery(commandText);
               }
           }
           catch
           { }
           //finally
           //{ }
       }
       public void ExecuteView()
       {
           try
           {
               SqlConnection connew = new SqlConnection("Data Source =" + cmbBackupServerName.Text + "; Initial Catalog=" + txtNewDBName.Text.Trim() + " ;Integrated Security = True");
               if (connew.State != ConnectionState.Open)
               {
                   connew.Open();
               }
               using (SqlCommand command = new SqlCommand("CREATE View [FreeItemMasterDetailView] As Select Item_table.Item_no,Item_table.Item_name,Item_table.Item_code,FreeItemDetail_table.FreeRate,FreeItemDetail_table.FreeQty,FreeItemDetail_table.FreeSno  from Item_table join FreeItemDetail_table on FreeItemDetail_table.FreeItem_no=Item_table.Item_no where Active=1;", connew))
                   command.ExecuteNonQuery();

               //using (SqlCommand command = new SqlCommand("CREATE VIEW [dbo].[FreeItemMasterDifferentView] As select Item_table.Item_no,item_table.Item_name,Item_table.Item_Code,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.SaleQty,FreeItemMaster_table.ItemImage,FreeItemMaster_table.FromDate,FreeItemMaster_table.todate from item_table join FreeItemMaster_table  on FreeItemMaster_table.Item_no=Item_table.Item_no where Active=1 and ItemType='Different' or ItemType='Single';", connew))
               using (SqlCommand command = new SqlCommand("CREATE VIEW [dbo].[FreeItemMasterDifferentView] As select Item_table.Item_no,item_table.Item_name,Item_table.Item_Code,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.SaleQty,FreeItemMaster_table.ItemImage,FreeItemMaster_table.FromDate,FreeItemMaster_table.todate,FreeItemMaster_table.Active from item_table join FreeItemMaster_table  on FreeItemMaster_table.Item_no=Item_table.Item_no where  ItemType='Different' or ItemType='Single';", connew))
                   command.ExecuteNonQuery();

              // using (SqlCommand command = new SqlCommand("CREATE VIEW [dbo].[FreeItemMasterSingleDifferentView] As select Item_table.Item_no,item_table.Item_name,Item_table.Item_Code,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.SaleQty,FreeItemMaster_table.ItemImage,FreeItemMaster_table.FromDate,FreeItemMaster_table.todate from item_table join FreeItemMaster_table  on FreeItemMaster_table.Item_no=Item_table.Item_no where Active=1 and ItemType='Different' or ItemType='Single' or ItemType='Same Free' or Itemtype='Different Free';", connew))
               using (SqlCommand command = new SqlCommand("CREATE VIEW [dbo].[FreeItemMasterSingleDifferentView] As select Item_table.Item_no,item_table.Item_name,Item_table.Item_Code,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.SaleQty,FreeItemMaster_table.ItemImage,FreeItemMaster_table.FromDate,FreeItemMaster_table.todate,FreeItemMaster_table.Active from item_table join FreeItemMaster_table  on FreeItemMaster_table.Item_no=Item_table.Item_no where Active=1 and ItemType='Different' or ItemType='Single' or ItemType='Same Free' or Itemtype='Different Free';", connew))
                   command.ExecuteNonQuery();

               using (SqlCommand command = new SqlCommand("CREATE VIEW [dbo].[FreeItemMasterView] As select Item_table.Item_no,item_table.Item_name,Item_table.Item_Code,FreeItemMaster_table.FreeSnoGroup,FreeItemMaster_table.FreeType,FreeItemMaster_table.TotSaleQty,FreeItemMaster_table.SaleQty,FreeItemMaster_table.ItemImage,FreeItemMaster_table.FromDate,FreeItemMaster_table.todate from item_table join FreeItemMaster_table  on FreeItemMaster_table.Item_no=Item_table.Item_no where Active=1 and ItemType='Single' or ItemType='Different';", connew))
                   command.ExecuteNonQuery();

               using (SqlCommand command = new SqlCommand("Create View [dbo].[tempView] as Select FreeSno, FreeItem_no, Item_table.Item_name, SaleQtyFrom, SaleQtyTo, Free_Qty,Rate, Disc_amt, Disc_Per, Date, FromDate, ToDate, FreeItem_Stock, FreeItem_TempStock, FreeType,Active, FreeSnoGroup from FreeItem_table, Item_table Where Item_table.Item_no=FreeItem_table.Item_no and FreeItem_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table));", connew))
                   command.ExecuteNonQuery();

               using (SqlCommand command = new SqlCommand("CREATE  View [dbo].[viewDifferentFree] as Select Item_table.Item_no as FreeItem_no,Item_table.Item_name as FreeItem_name, FreeItemDetail_table.FreeQty,FreeItemDetail_table.FreeSno,FreeItemMaster_table.OfferName,FreeItemMaster_table.TotSaleQty, FreeItemMaster_table.Item_no,FreeItemMaster_table.Sunday,FreeItemMaster_table.monday,FreeItemMaster_table.Tuesday,FreeItemMaster_table.Wednesday,FreeItemMaster_table.Thursday,FreeItemMaster_table.Friday,FreeItemMaster_table.Sturday from FreeItemMaster_table,FreeItemDetail_table, Item_table where FreeItemDetail_table.FreeItem_no=Item_table.Item_no and FreeItemDetail_table.FreeSno=FreeItemMaster_table.FreeSnoGroup and FreeItemMaster_table.ItemType='Single' and FreeType='Free Different' and FreeItemMaster_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table));", connew))
                   command.ExecuteNonQuery();

               using (SqlCommand command = new SqlCommand("CREATE View [dbo].[viewSameFree] as Select Item_table.Item_no as FreeItem_no,Item_table.Item_name as FreeItem_name, FreeItemDetail_table.FreeQty,FreeItemDetail_table.FreeSno,FreeItemMaster_table.OfferName,FreeItemMaster_table.TotSaleQty, FreeItemMaster_table.Item_no,FreeItemMaster_table.Sunday,FreeItemMaster_table.monday,FreeItemMaster_table.Tuesday,FreeItemMaster_table.Wednesday,FreeItemMaster_table.Thursday,FreeItemMaster_table.Friday,FreeItemMaster_table.Sturday from FreeItemMaster_table,FreeItemDetail_table, Item_table where FreeItemDetail_table.FreeItem_no=Item_table.Item_no and FreeItemDetail_table.FreeSno=FreeItemMaster_table.FreeSnoGroup and FreeItemMaster_table.ItemType='Single' and FreeType='Same Free' and FreeItemMaster_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table));", connew))
                   command.ExecuteNonQuery();

               using (SqlCommand command = new SqlCommand("CREATE View [dbo].[viewSingleFree] as Select Item_table.Item_no as FreeItem_no,Item_table.Item_name as FreeItem_name, FreeItemDetail_table.FreeQty,FreeItemDetail_table.FreeSno,FreeItemMaster_table.OfferName,FreeItemMaster_table.TotSaleQty, FreeItemMaster_table.Item_no,FreeItemMaster_table.Sunday,FreeItemMaster_table.monday, FreeItemMaster_table.Tuesday,FreeItemMaster_table.Wednesday,FreeItemMaster_table.Thursday,FreeItemMaster_table.Friday,FreeItemMaster_table.Sturday from FreeItemMaster_table,FreeItemDetail_table, Item_table where FreeItemDetail_table.FreeItem_no=Item_table.Item_no and FreeItemDetail_table.FreeSno=FreeItemMaster_table.FreeSnoGroup and FreeItemMaster_table.ItemType='Single' and FreeType='Different Free' and FreeItemMaster_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table));", connew))
                   command.ExecuteNonQuery();

               using (SqlCommand command = new SqlCommand("CREATE View [dbo].[viewDiffFree] as Select FreeSno, FreeSnoGroup,OfferName, Item_table.Item_name, TotSaleQty, TotSalePrice, FromDate, ToDate, ItemType,FreeType,Active from FreeItemMaster_table, Item_table Where  Item_table.Item_no=FreeItemMaster_table.Item_no and FreeType='Price' and ItemType='Different' and FreeItemMaster_table.Active=1 and FromDate<=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table)) and ToDate>=(Select convert(date,DATEADD(DAY,1,EndOfDay),103) from EndOfDay_Table where Id=(Select MAX(id) from EndOfDay_Table));", connew))
                   command.ExecuteNonQuery();
           }
           catch
           { }
           //finally
           //{ }
       }
       public void ExecuteSqpProcedure(string FileName)
       {
           try
           {
               //Execute SQL Stored Procedure Directly 
               string createQuery1 = "";
               SqlConnection conSql = new SqlConnection("Data Source=" + cmbBackupServerName.Text.Trim() + ";Initial Catalog=" + txtNewDBName.Text.Trim() + ";User ID=" + txtSqlLogin.Text.Trim() + ";Password=" + txtSqlPassWord.Text.Trim() + ";Integrated Security=True");
               if (System.IO.File.Exists(FileName) == true)
               {
                   System.IO.
                   StreamReader objreader;
                   objreader =
                   new System.IO.StreamReader(FileName);
                   createQuery1 = objreader.ReadToEnd();
                   objreader.Close();

                   SqlCommand myCommand = new SqlCommand(createQuery1, conSql);
                   if (conSql.State != ConnectionState.Open)
                   {
                       conSql.Open();
                   }
                   myCommand.ExecuteNonQuery();
                   //con.Close();
               }
               else
               {
                   MyMessageBox.ShowBox("File Name Not Found", "Warning");
               }
           }
           catch
           { }
           //finally
           //{ }
       }
       public void IndexCreation()
       {

           //Run Index:
           SqlConnection connew = new SqlConnection("Data Source =" + cmbBackupServerName.Text + "; Initial Catalog=" + txtNewDBName.Text.Trim() + " ;Integrated Security = True");
           if (connew.State != ConnectionState.Open)
           {
               connew.Open();
           }
           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexSalMas_table' AND object_id = OBJECT_ID('SalMas_table'))
                            BEGIN
                                Create Index IndexSalMas_table on SalMas_table (smas_no, smas_billno)
                                ALTER INDEX ALL ON [dbo].[SALMAS_TABLE] REBUILD 
                                END
                                else
                                begin
                                ALTER INDEX ALL ON [dbo].[SALMAS_TABLE] REBUILD 
                                end", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexBeginCashDrawer_table' AND object_id = OBJECT_ID('BeginCashDrawer_table'))
                            BEGIN
                            CREATE INDEX IndexBeginCashDrawer_table  on BeginCashDrawer_table (Id)
                            ALTER INDEX ALL ON [dbo].[BeginCashDrawer_table] REBUILD 
                            END
                            else
                            begin
                            ALTER INDEX ALL ON [dbo].[BeginCashDrawer_table] REBUILD 
                            end", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexBrand_table' AND object_id = OBJECT_ID('Brand_table'))
BEGIN
CREATE INDEX IndexBrand_table on Brand_table(Brand_no,Brand_name)
ALTER INDEX ALL ON [dbo].[Brand_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Brand_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexItem_table' AND object_id = OBJECT_ID('item_table'))
BEGIN
CREATE INDEX IndexItem_table on item_table (Item_no,item_name)
ALTER INDEX ALL ON [dbo].[item_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[item_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexItem_grouptable' AND object_id = OBJECT_ID('Item_groupTable'))
BEGIN
CREATE INDEX IndexItem_grouptable on Item_groupTable (Item_groupno,Item_groupname)
ALTER INDEX ALL ON [dbo].[Item_groupTable] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Item_groupTable] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexItem_seltable' AND object_id = OBJECT_ID('Item_seltable'))
BEGIN
CREATE INDEX IndexItem_seltable on Item_seltable (item_no,item_selName)
ALTER INDEX ALL ON [dbo].[Item_seltable] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Item_seltable] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexLedger_table' AND object_id = OBJECT_ID('Ledger_table'))
BEGIN
CREATE INDEX IndexLedger_table on Ledger_table (Ledger_no,Ledger_Name)
ALTER INDEX ALL ON [dbo].[Ledger_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Ledger_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexModel_table' AND object_id = OBJECT_ID('Model_table'))
BEGIN
CREATE INDEX IndexModel_table on Model_table (Model_no,Model_name)
ALTER INDEX ALL ON [dbo].[Model_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Model_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexPurMas_table' AND object_id = OBJECT_ID('PurMas_table'))
BEGIN
CREATE INDEX IndexPurMas_table on PurMas_table (pmas_sno)
ALTER INDEX ALL ON [dbo].[PurMas_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[PurMas_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexRack_table' AND object_id = OBJECT_ID('Rack_table'))
BEGIN
CREATE INDEX IndexRack_table on Rack_table (Rack_no, Rack_name)
ALTER INDEX ALL ON [dbo].[Rack_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Rack_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexSalRecv_table' AND object_id = OBJECT_ID('SalRecv_table'))
BEGIN
CREATE INDEX IndexSalRecv_table on SalRecv_table (SalRecv_sno, SalRecv_salno)
ALTER INDEX ALL ON [dbo].[SalRecv_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[SalRecv_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='Indexstktrn_table' AND object_id = OBJECT_ID('stktrn_table'))
BEGIN
CREATE INDEX Indexstktrn_table on stktrn_table(strn_sno, strn_no)
ALTER INDEX ALL ON [dbo].[stktrn_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[stktrn_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"
if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexTax_table' AND object_id = OBJECT_ID('Tax_table'))
BEGIN
CREATE INDEX IndexTax_table on Tax_table (Tax_no, Tax_name)
ALTER INDEX ALL ON [dbo].[Tax_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Tax_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"if NOT exists (SELECT * FROM sys.indexes WHERE name='Indexunit_table' AND object_id = OBJECT_ID('unit_table'))
BEGIN
CREATE INDEX Indexunit_table on unit_table (Unit_no, Unit_name)
ALTER INDEX ALL ON [dbo].[unit_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[unit_table] REBUILD 
end
", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"
if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexUser_table' AND object_id = OBJECT_ID('User_table'))
BEGIN
CREATE INDEX IndexUser_table on User_table(User_no,User_name)
ALTER INDEX ALL ON [dbo].[User_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[User_table] REBUILD 
end", connew))
               cmdIndex.ExecuteNonQuery();

           using (SqlCommand cmdIndex = new SqlCommand(@"
if NOT exists (SELECT * FROM sys.indexes WHERE name='IndexVch_table' AND object_id = OBJECT_ID('Vch_table'))
BEGIN
CREATE INDEX IndexVch_table on Vch_table (sno)
ALTER INDEX ALL ON [dbo].[Vch_table] REBUILD 
END
else
begin
ALTER INDEX ALL ON [dbo].[Vch_table] REBUILD 
end
END", connew))
               cmdIndex.ExecuteNonQuery();

                       
       }
       private void cmbDatabaseType_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
             cmbServerName.Select();
           }
       }
       private void cmbServerName_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
             chkCustomConnectionString.Select();
           }
       }
       private void chkCustomConnectionString_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
               txtDatabaseName.Select();
           }
       }
       private void txtDatabaseName_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
             radioWindowsAuthentication.Select();
           }
       }
       private void radioWindowsAuthentication_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
              txtLogin.Select();
           }
       }

       private void txtLogin_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
              txtPassword.Select();
           }
       }
       private void txtPassword_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
             cmbTimeOut.Select();
           }
       }

       private void cmbTimeOut_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab)
           {
             btnTestConnection.Select();
           }
       }

       private void cmbBackupServerName_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
              txtBackupDatabaseName.Select();
           }
       }

       private void txtFolderLocation_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
              txtBackUpFileName.Select();
           }
       }

       private void txtBackupDatabaseName_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
                txtFolderLocation.Select();
           }
       }

       private void txtBackUpFileName_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
             btnBackup.Select();
           }
       }

       private void txtNewDBName_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
           {
             btnMakeNewDB.Select();
           }
       }

       private void btnCreateNew_Click(object sender, EventArgs e)
       {

       }
      
    }
}
