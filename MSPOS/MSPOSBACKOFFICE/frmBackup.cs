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


namespace MSPOSBACKOFFICE
{
    public partial class frmBackup : Form
    {
        public frmBackup()
        {
            InitializeComponent();
        }

        private void btnDBbackup_Click(object sender, EventArgs e)
        {

        }
        public void Loadfun()
        {
            System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            System.Data.DataTable dataTable = instance.GetDataSources();
            //cmbServerName.Items.Clear();
            cmbBackupServerName.Items.Clear();
            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                //  cmbServerName.Items.Add(dataTable.Rows[j]["ServerName"]);
                cmbBackupServerName.Items.Add(dataTable.Rows[j]["ServerName"].ToString() + @"\" + dataTable.Rows[j]["InstanceName"].ToString());
            }


            string fileUNQ = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
            txtBackUpFileName.Text = fileUNQ + ".bak";

            _Class.clsVariables.Sheight_Width();
            this.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
            //Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
            Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
            Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Loadfun();
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
            }        
            
        }

        private void btnFolderLocation_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Message");
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBackupServerName.Text.Trim() != "")
                {
                    if (cmbBackupDatabaseName.Text.Trim() != "")
                    {
                        if (txtFolderLocation.Text.Trim() != "")
                        {
                            if (txtBackUpFileName.Text.Trim() != "")
                            {
                                if (txtBackUpFileName.Text.IndexOf(".bak") != -1)
                                {
                                    if (System.IO.Directory.Exists(txtFolderLocation.Text))
                                    {
                                        BackupDatabase(txtFolderLocation.Text, txtBackUpFileName.Text, cmbBackupDatabaseName.Text, cmbBackupServerName.Text);
                                    }
                                    else
                                    {
                                        MyMessageBox1.ShowBox("Select Valid Folder Location", "Warning");
                                    }
                                }
                                else
                                {
                                    MyMessageBox1.ShowBox("Backup Filename should end with .bak", "Warning");                                    
                                    txtBackUpFileName.Select();
                                }
                            }
                            else
                            {
                                MyMessageBox1.ShowBox("Enter Backup file name", "Warning");
                                txtBackUpFileName.Select();
                            }
                        }
                        else
                        {
                            MyMessageBox1.ShowBox("Select Backup Folder Location", "Warning");
                            btnFolderLocation.Select();
                        }
                    }
                    else
                    {
                        MyMessageBox1.ShowBox("Should Enter Database Name", "Warning");
                        cmbBackupDatabaseName.Select();

                    }
                }
                else
                {
                    MyMessageBox1.ShowBox("Select Server Name", "Warning");
                    cmbBackupServerName.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox1.ShowBox(ex.Message, "Warning");
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
                MessageBox.Show(BackUpFileName + " Backup Created Successfully");
                //   Label1.Text = "Done";
                //   Label2.Text = SQLBackUp + " ######## Server name " + ServerName + " Database " + DatabaseName + " successfully backed up to " + BackUpLocation + @"\" + BackUpFileName + "\n Back Up Date : " + DateTime.Now.ToString();
                funclr();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
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

        private void cmbBackupDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbBackupServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBackupServerName.Text != "")
                {
                    List<String> databases = new List<String>();

                    SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder();

                    connection.DataSource = cmbBackupServerName.Text;
                    // enter credentials if you want
                    connection.UserID = "sa";
                    connection.Password = "!Password123";
                    connection.IntegratedSecurity = true;

                    String strConn = connection.ToString();

                    //create connection
                    SqlConnection sqlConn = new SqlConnection(strConn);

                    //open connection
                    sqlConn.Open();

                    //get databases
                    DataTable tblDatabases = sqlConn.GetSchema("Databases");

                    //close connection
                    sqlConn.Close();

                    //add to list
                    foreach (DataRow row in tblDatabases.Rows)
                    {
                        String strDatabaseName = row["database_name"].ToString();

                        databases.Add(strDatabaseName);


                    }

                    cmbBackupDatabaseName.DataSource = databases;

                }
                
            }
            catch (WarningException)
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.Showbox(ex.Message, "Warning");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                funclr();
            }
            catch (Exception ex)
            {
                MyMessageBox.Showbox(ex.Message, "Warning");
            }
        }
        public void funclr()
        {
            cmbBackupServerName.Text = string.Empty;
            cmbBackupDatabaseName.Text = string.Empty;
            txtFolderLocation.Text = string.Empty;
            txtBackUpFileName.Text = string.Empty;
        }
    }
}
