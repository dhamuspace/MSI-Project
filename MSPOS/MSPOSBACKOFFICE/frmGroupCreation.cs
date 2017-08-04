using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Drawing.Drawing2D;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO.IsolatedStorage;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;


namespace MSPOSBACKOFFICE
{
    public partial class frmGroupCreation : Form
    {

        public frmGroupCreation()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ToString());
        SqlDataReader dr = null;
        string colornsNumber = "0",colorFont="0";
        DataTable dt = new DataTable();
        public string tempGroupNo;
        public int tempCount;
        int groupNo;
        string FileName;
        byte[] imgByteArr=new byte[0];
        private void frmGroupCreation_Load(object sender, EventArgs e)
        {
            try
            {
                btnColorImage.Visible = true;
                TxtUnder.Text = "PRIMARY";
                //if (Load1 == "Color")
                //{
                //    TxtUnder.Text = ColorName.ToString();
                //    Load1 = "";
                //}
                loadGroup();
                color_check();
                pictureBox1 = null;
                TxtVisibility.SelectedIndex = 0;
                cmbImageVisibility.SelectedIndex = 0;
                TxtGroupName.Select();

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void color_check()
        {
           
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                this.cmb_GroupColor.Items.Add(c.Name);
                this.cmb_fontColor.Items.Add(c.Name);
            }
        }

        private void newBtnGroupItem_Click(object sender, EventArgs e)
        {
            try
            {
                Button ClickedButton = (Button)sender;

                TxtGroupName.Text = ClickedButton.Text.ToString();
                TxtGroupName_Click(sender, e);
                btnDelete.Enabled = true;
                // btn_unit_save.Enabled = false;
                if (TxtGroupName.Text != "")
                {
                    txtupdateModel = ClickedButton.Text.ToString();


                }
                btnColorImage.Text = ClickedButton.Text.ToString();
                // button9.Visibility = Visibility.Visible;
                if (TxtGroupName.Text != "")
                {
                    SqlCommand cmd = new SqlCommand(" select * from Item_GroupTable where Item_groupname=@tName", con);
                    cmd.Parameters.AddWithValue("@tName", TxtGroupName.Text);
                    con.Close();
                    con.Open();
                    dt.Rows.Clear();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    TxtDisAmt.Text = dt.Rows[0]["DisPerAmt"].ToString();
                    TxtPosition.Text = dt.Rows[0]["GroupPos"].ToString();
                    TxtUnder.Text = dt.Rows[0]["Item_groupmtname"].ToString();
                    string visual;
                    visual = dt.Rows[0]["Group_visibility"].ToString();
                    if (visual == "True")
                    {
                        TxtVisibility.Text = "True";
                    }
                    if (visual == "False")
                    {
                        TxtVisibility.Text = "False";
                    }
                    if (dt.Rows[0]["ImageVisibility"].ToString() == "True")
                    {
                        cmbImageVisibility.Text = "True";
                    }
                    else
                    {
                        cmbImageVisibility.Text = "False";
                    }

                    var bc = new System.Windows.Media.BrushConverter();
                    colornsNumber = dt.Rows[0]["Group_Color"].ToString();
                    colorFont = dt.Rows[0]["Font_Color"].ToString();
                    txtCommission.Text = dt.Rows[0]["Commission"].ToString();

                    // Color temp = (Color)dt.Rows[0]["Group_Color"].ToString();
                    if (colornsNumber != "" && colornsNumber != "0")
                    {
                        //  btnColorImage.BackColor = (System.Drawing.Brush)bc.ConvertFrom(Convert.ToString(colornsNumber));
                        cmb_GroupColor.Text = colornsNumber.ToString();
                        cmb_fontColor.Text = colorFont.ToString();
                        btnColorImage.BackColor = Color.FromName(colornsNumber);
                        btnColorImage.ForeColor = Color.FromName(colorFont);
                        btnColorImage.Visible = true;
                    }
                    btn_unit_save.Text = "Update";

                    //photo_aray = (byte[])dt.Rows[0]["Items_Image"];
                    //MemoryStream mStream = new MemoryStream(photo_aray);
                    //Image returnImage = Image.FromStream(mStream);
                    //btnColorImage.Image = returnImage;
                }
                TxtGroupName.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        public void loadGroup()
        {
            try
            {
                pnlGroupItem.Controls.Clear();
                SqlCommand cmd = new SqlCommand(" select * from Item_GroupTable order by GroupPos ASC", con);
                con.Close();
                con.Open();
                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {

                    i += 1;
                    Button newBtn = new Button();
                    newBtn.Text = dr["Item_groupname"].ToString();
                    newBtn.Name = "GroupItem" + i;
                    newBtn.Width = 170;
                    newBtn.Height = 30;
                    newBtn.ForeColor = Color.White;
                    newBtn.BackColor = Color.FromArgb(96, 155, 173);
                    //  newBtn.Font.Size.Equals(18);
                    // newBtn.Font.Style.Equals(FontStyle.Bold);
                    // newBtn.BackColor = Color.Transparent;                    
                    newBtn.Location = new System.Drawing.Point(5, i * 40 - 40);
                    newBtn.Click += new EventHandler(newBtnGroupItem_Click);
                    pnlGroupItem.Controls.Add(newBtn);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message,"Warning");
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_rack_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newBtnGroupItem1_Click(object sender, EventArgs e)
        {
            Button ClickedButton = (Button)sender;
            TxtUnder.Text = ClickedButton.Text.ToString();

        }
        //OpenFileDialog dlg = new OpenFileDialog();
        void funSelectImage()
        {
          //  var brush = new  ImageBrush();
          
        }
       // string imageName;
        //string filename1 = null, filename2 = null;
        public string check = null ;
        private void BtnImage_Click(object sender, EventArgs e)
        {
            btnColorImage.Enabled = true;
           // openFileDialog1.FileName=
            //openFileDialog1.DefaultExt = ".jpg";
           // openFileDialog1.Filter = "Image (.Jpg)|(*.Jpg)";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\GroupImage"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\GroupImage");
                }              
                check = "Image";
                // pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                FileName = openFileDialog1.FileName;
                btnColorImage.BackgroundImage = Image.FromFile(FileName);
                btnColorImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }


          //System.Windows.Forms .OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
          //  openfile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp";
          //  if (openfile.ShowDialog() == DialogResult.OK)
          //  {
          //      Image img = new Bitmap(openfile.FileName);
          //      pictureBox1.Image = img.GetThumbnailImage(340, 125, null, new IntPtr());
          //      openfile.RestoreDirectory = true;
          //      filename1 = openfile.FileName;
          //  }
        }

        private void cmb_fontColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = this.cmb_fontColor.SelectedItem.ToString();          
            btnColorImage.ForeColor = Color.FromName(color);
        }

        private void cmb_GroupColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = this.cmb_GroupColor.SelectedItem.ToString();          
            btnColorImage.Visible = true;
            btnColorImage.BackColor = Color.FromName(color);
        }

        private void btn_unit_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_unit_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            TxtGroupName.Text = "";
            TxtPosition.Text = "";
            TxtUnder.Text = "PRIMARY";
            TxtVisibility.Text = "True";
            cmb_fontColor.Text = "";
            cmb_GroupColor.Text = "";
            btnColorImage.BackColor = Color.LightGray;
            dt.Rows.Clear();
            pictureBox1 = null;
            btnColorImage.BackgroundImage = null;
            btnColorImage.ForeColor = Color.Black;
            btnColorImage.Text = "";
            btnColorImage.Text = "Sample";
            btnDelete.Enabled = false;
            btn_unit_save.Enabled = true;
          //  TxtVisibility.SelectedIndex = 0;
            btn_unit_save.Text = "Save";
            TxtDisAmt.Text = "0";
            CmbDisType.Text = "Percentage";
            txtCommission.Text = "0";
        }
      //  MemoryStream ms;     
        byte[] photo_aray=new byte[0];
      //  SqlCommand sp_cmd = null;
        void conv_photo()
        {
            //DataTable dt=new DataTable();
            //SqlDataAdapter adp = new SqlDataAdapter("Select Items_Image from Item_Grouptable", con);
            //adp.Fill(dt);
            //photo_aray =(byte[])dt.Rows[0]["Items_Image"];
            //converting photo to binary data
          //  ms = new MemoryStream();
          ////  pictureBox1.Image.Save(ms,ImageFormat.Jpeg);
          //  byte[] photo_aray = new byte[ms.Length];
          //  ms.Position = 0;
          //  ms.Read(photo_aray, 0, photo_aray.Length);

            FileStream fs = new FileStream(@FileName, FileMode.Open, FileAccess.Read);
            //Initialize a byte array with size of stream
            imgByteArr = new byte[fs.Length];
            //Read data from the file stream and put into the byte array
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            //cmd_photo.Parameters.AddWithValue("@sphoto", imgByteArr);
        }

        //void conv_photo()
        //{

        //    //converting photo to binary data

        //    ms = new MemoryStream();

        // pictureBox1.Image.Save(ms, ImageFormat.Jpeg);

        //    byte[] photo_aray = new byte[ms.Length];

        //    ms.Position = 0;
        //    ms.Read(photo_aray, 0, photo_aray.Length);
        //    cmd_photo.Parameters.AddWithValue("@sphoto", photo_aray);

        //}
        SqlCommand cmd_photo;
        string tMaxGroupNo;
        string visibility = null;
        DataTable dt3=new System.Data.DataTable();
        private void btn_unit_save_Click(object sender, EventArgs e)
        {
            #region
            funRecordSave();
            #endregion
        }
        string strItemId;
        public static Boolean IsFileLocked(FileInfo tpath)
        {
            FileStream stream = null;
            try
            { //Don't change FileAccess to ReadWrite,
                //because if a file is in readOnly, it fails.
                stream = tpath.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }
        void funRecordSave()
        {
            try
            {
                //FileStream fs = new FileStream(@FileName, FileMode.Open, FileAccess.Read);
                ////Initialize a byte array with size of stream
                //byte[] imgByteArr = new byte[fs.Length];
                ////Read data from the file stream and put into the byte array
                //fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                //fs.Close();

                if (TxtGroupName.Text != "")
                {
                    string discountType = "";
                    if (CmbDisType.Text == "None")
                    {
                        discountType = "0";
                    }
                    else if (CmbDisType.Text.Trim() == "Percentage")
                    {
                        discountType = "1";
                    }
                    else if (CmbDisType.Text.Trim() == "Amount")
                    {
                        discountType = "2";
                    }
                    else
                    {
                        discountType = "0";
                    }
                    if (TxtVisibility.Text == "True")
                    {
                        visibility = "True";
                    }
                    if (TxtVisibility.Text == "False")
                    {
                        visibility = "False";
                    }
                    dt3.Rows.Clear();


                    if (check == null)
                    {
                        //Getting Maximum Group Number from NumberTable:
                        SqlDataAdapter cmd = new SqlDataAdapter("Select Max(item_groupno) as GroupNo from Numbertable", con);
                        cmd.Fill(dt3);
                        tMaxGroupNo = "0";
                        if (dt3.Rows.Count > 0)
                        {
                            tMaxGroupNo = dt3.Rows[0]["GroupNo"].ToString();
                        }
                        tMaxGroupNo = (double.Parse(tMaxGroupNo) + 1).ToString();
                        //Getting Group Number for Updating Again:
                        SqlCommand cmd1 = new SqlCommand("select * from Item_Grouptable where Item_groupno=@tGroupNo", con);
                        cmd1.Parameters.AddWithValue("@tGroupNo", groupNo);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        adp.Fill(dt);
                        int i = 0;
                        if (dt.Rows.Count > 0)
                        {
                            //Updating Group Names and everythings:
                            i = 1;
                            SqlCommand sp_cmd = new SqlCommand("sp_Group_Update", con);
                            sp_cmd.CommandType = CommandType.StoredProcedure;
                            sp_cmd.Parameters.AddWithValue("@GroupPos", TxtPosition.Text);
                            sp_cmd.Parameters.AddWithValue("@Group_Color", cmb_GroupColor.Text);
                            sp_cmd.Parameters.AddWithValue("@Group_visibility", visibility);
                            sp_cmd.Parameters.AddWithValue("@Font_Color", cmb_fontColor.Text);
                            sp_cmd.Parameters.AddWithValue("@item_groupno", groupNo);
                            sp_cmd.Parameters.AddWithValue("@item_groupname", TxtGroupName.Text);
                            sp_cmd.Parameters.AddWithValue("@imageVisibility", cmbImageVisibility.Text.Trim());

                            //cmd_photo = new SqlCommand("Update item_GroupTable set GroupPos='" + TxtPosition.Text + "',Group_Color='" + cmb_GroupColor.Text + "',Group_visibility='" + visibility + "',Font_Color='" + cmb_fontColor.Text + "' where item_groupname='" + TxtGroupName.Text + "'", con);
                            //conv_photo();
                            con.Close();
                            con.Open();
                            sp_cmd.ExecuteNonQuery();
                            

                            //Alter Column:
                            //Update Discount Group Wise, settings used to save the values:
                            SqlCommand cmdDiscountType = new SqlCommand("Update Item_Grouptable set DisPerAmtType=@DiscountType,DisPerAmt=@PerAmtType where Item_groupno=@tGroupNo", con);
                            cmdDiscountType.Parameters.AddWithValue("@tGroupNo", groupNo);
                            cmdDiscountType.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(CmbDisType.Text) ? "" : discountType.ToString());
                            cmdDiscountType.Parameters.AddWithValue("@PerAmtType", string.IsNullOrEmpty(TxtDisAmt.Text) ? "0" : TxtDisAmt.Text);
                            cmdDiscountType.ExecuteNonQuery();
                            //MyMessageBox.ShowBox("Groupname Updated Successfully","Message");
                            // throw new ApplicationException("Please Verify Your Table Column Name!");
                            con.Close();
                            btn_unit_save.Text = "Save";
                            groupNo = 0;
                        }
                        if (i == 0)
                        {
                            //ItemGroup Name save the Every Time:
                            SqlCommand sp_cmd = new SqlCommand("sp_Group_Insert", con);
                            sp_cmd.CommandType = CommandType.StoredProcedure;
                            sp_cmd.Parameters.AddWithValue("@Item_groupno", tMaxGroupNo);
                            sp_cmd.Parameters.AddWithValue("@Item_groupname", TxtGroupName.Text);
                            sp_cmd.Parameters.AddWithValue("@Item_groupmtname", (TxtGroupName.Text).ToUpper());
                            sp_cmd.Parameters.AddWithValue("@Item_grouplevel", '0');
                            sp_cmd.Parameters.AddWithValue("@Item_groupunder", '0');
                            sp_cmd.Parameters.AddWithValue("@Item_Commodity", "");
                            sp_cmd.Parameters.AddWithValue("@Item_groupgno", '0');
                            sp_cmd.Parameters.AddWithValue("@Item_groupflag", '0');
                            sp_cmd.Parameters.AddWithValue("@Std_Group", '1');
                            sp_cmd.Parameters.AddWithValue("@GroupPos ", TxtPosition.Text);
                            sp_cmd.Parameters.AddWithValue("@Group_Color ", cmb_GroupColor.Text);
                            sp_cmd.Parameters.AddWithValue("@Group_visibility ", visibility);
                            sp_cmd.Parameters.AddWithValue("@Font_Color ", cmb_fontColor.Text);
                            sp_cmd.Parameters.AddWithValue("@imageVisibility", cmbImageVisibility.Text.Trim());
                            //cmd_photo = new SqlCommand("insert into Item_Grouptable(Item_groupno,Item_groupname,Item_groupmtname,Item_grouplevel,Item_groupunder,Item_Commodity,Item_groupgno,Item_groupflag,Std_Group,GroupPos,Group_Color,Group_visibility,Font_Color) Values ('" + tMaxGroupNo + "','" + TxtGroupName.Text + "','PRIMARY','0','0','','0','0','1','" + TxtPosition.Text + "','" + cmb_GroupColor.Text + "','" + visibility + "','" + cmb_fontColor.Text + "') ", con);
                            // conv_photo();
                            con.Close();
                            con.Open();
                            sp_cmd.ExecuteNonQuery();
                            MyMessageBox.ShowBox("Groupname Saved Successfully", "Message");
                           
                            //Alter Column:
                            //Update Discount Group Wise, settings used to save the values: At the Record Save Time:
                            SqlCommand cmdDiscountType = new SqlCommand("Update Item_Grouptable set DisPerAmtType=@DiscountType,DisPerAmt=@PerAmtType where item_groupname=@GroupName", con);
                            cmdDiscountType.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(CmbDisType.Text) ? "" : discountType.ToString());
                            cmdDiscountType.Parameters.AddWithValue("@PerAmtType", string.IsNullOrEmpty(TxtDisAmt.Text) ? "0" : TxtDisAmt.Text);
                            cmdDiscountType.Parameters.AddWithValue("@GroupName", TxtGroupName.Text.Trim());
                            cmdDiscountType.ExecuteNonQuery();
                           
                            con.Close();
                        }
                        //Update the number table after save the Group Name For Again Increment The Group Number
                        SqlCommand cmd11 = new SqlCommand("update NumberTable set item_groupNo=Item_groupNo+1", con);
                        con.Close();
                        con.Open();
                        cmd11.ExecuteNonQuery();
                        //MyMessageBox.ShowBox("GroupName Updated Successfully", "Message");
                        
                        
                        SqlCommand cmdUpdateCommission = new SqlCommand("Update item_Grouptable set Commission=@Commission Where item_GroupName=@GroupName", con);
                        cmdUpdateCommission.Parameters.AddWithValue("@GroupName", TxtGroupName.Text);
                        cmdUpdateCommission.Parameters.AddWithValue("@Commission", string.IsNullOrEmpty(txtCommission.Text) ? 0 : Convert.ToDouble(txtCommission.Text));
                        cmdUpdateCommission.ExecuteNonQuery();
                        //MyMessageBox.ShowBox("Groupname Updated Successfully", "Message");
                       // throw new ApplicationException("Please Verifiy Your Table Column Name!");
                        con.Close();
                    }
                    else
                    {
                        SqlDataAdapter cmd = new SqlDataAdapter("Select Max(item_groupno) as GroupNo from NumberTable", con);
                        cmd.Fill(dt3);
                        tMaxGroupNo = "0";
                        if (dt3.Rows.Count > 0)
                        {
                            tMaxGroupNo = dt3.Rows[0]["GroupNo"].ToString();
                        }
                        tMaxGroupNo = (double.Parse(tMaxGroupNo) + 1).ToString();
                        //else
                        //{
                        //    tMaxGroupNo = "1";
                        //}
                        con.Close();
                        con.Open();
                        //Select Group Name for Update Image Of That Process
                        SqlCommand cmd1 = new SqlCommand("select * from Item_Grouptable where Item_groupname=@tName", con);
                        cmd1.Parameters.AddWithValue("@tName", TxtGroupName.Text);
                        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        dt.Rows.Clear();
                        adp.Fill(dt);
                        int i = 0;
                        for (int j = 0; j < dt.Rows.Count; )
                        {
                            i = 1;
                            conv_photo();
                            //byte[] img = null;
                            //FileStream fsn = new FileStream(@FileName, FileMode.Open, FileAccess.Read);
                            //BinaryReader br = new BinaryReader(fsn);
                            //img = br.ReadBytes((int)fsn.Length);
                            //imgByteArr = img;

                            //sqlcommand sqlcmd1 = new sqlcommand("select Item_groupno from MSPOS.dbo.item_GroupTable where Item_groupname=TxtGroupName.Text.Trim()", con);

                            SqlCommand sp_cmd = new SqlCommand("UPDATE item_GroupTable SET GroupPos=@GroupPos,Items_Image=@Items_Image,Group_Color= @Group_Color,Group_visibility=@Group_visibility,Font_Color=@Font_Color,item_groupname=@item_groupname,ImageLocation=@itemLocation,imagevisibility=@imageVisibility  WHERE Item_groupno=(select Item_groupno from item_GroupTable where Item_groupname='"+TxtGroupName.Text.Trim()+"')", con);  

                            //SqlCommand sp_cmd = new SqlCommand("sp_GroupImg_Update", con);
                            //sp_cmd.CommandType = CommandType.StoredProcedure;
                            sp_cmd.Parameters.AddWithValue("@GroupPos", TxtPosition.Text);                           

                            //SqlParameter imageParameter = new SqlParameter("@Items_Image", SqlDbType.Image);
                            //imageParameter.Value = DBNull.Value;
                            //sp_cmd.Parameters.Add(imageParameter);                           

                            sp_cmd.Parameters.AddWithValue("@Items_Image", imgByteArr);
                            sp_cmd.Parameters.AddWithValue("@Group_Color", cmb_GroupColor.Text);
                            sp_cmd.Parameters.AddWithValue("@Group_visibility", visibility);
                            sp_cmd.Parameters.AddWithValue("@Font_Color", cmb_fontColor.Text);
                            sp_cmd.Parameters.AddWithValue("@item_groupname", TxtGroupName.Text);
                            sp_cmd.Parameters.AddWithValue("@item_groupno", groupNo);

                            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\GroupImage"))
                            {
                                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\GroupImage");
                            }
                            string tPath = System.Windows.Forms.Application.StartupPath + "\\GroupImage\\" + TxtGroupName.Text.Trim() + ".jpeg";
                            //string tPath = "\\GroupImage\\" + TxtGroupName.Text.Trim() + ".jpeg";
                            string chkpath = "1";

                            if (!File.Exists(tPath))
                            {
                                System.IO.File.Copy(FileName, tPath);
                                chkpath = "2";
                            }
                            else
                            {
                                //try
                                //{
                                //    //GC.Collect();
                                //    //System.IO.File.Delete(tPath);                                    

                                //    FileInfo file = new FileInfo(tPath);
                                //    if (file.Exists)
                                //    {

                                //        if (!file.IsReadOnly)
                                //        {
                                //            //// file.Delete();                                           

                                //            //FileStream stream = null;
                                //            //stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                                //            //if (stream != null)
                                //            //stream.Close(); 
                                //            //System.IO.FileStream fs;  
                                //            //fs = new FileStream(tPath, FileMode.Open, FileAccess.Read);
                                //            //if (fs != null)
                                //            //{
                                //            //    fs.Close();
                                //            //}
                                //            GC.Collect();
                                //            GC.WaitForPendingFinalizers();
                                //            GC.Collect();
                                //            if (!IsFileLocked(file))
                                //            {
                                //                file.Delete();
                                //            }
                                //            else
                                //            {
                                //                FileStream stream = null;
                                //                try
                                //                { //Don't change FileAccess to ReadWrite,
                                //                    //because if a file is in readOnly, it fails.
                                //                    stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                                //                }
                                //                catch (IOException)
                                //                { //the file is unavailable because it is:
                                //                    //still being written to or being processed by another thread
                                //                    //or does not exist (has already been processed)
                                //                    //return true;
                                //                }
                                //                finally
                                //                {
                                //                    if (stream == null)
                                //                        stream.Close();
                                //                    file.Delete();
                                //                }
                                //            }
                                //            //System.IO.File.Delete(tPath);
                                //            System.IO.File.Copy(FileName, tPath);
                                //            file.Delete();
                                //        }
                                //        else
                                //        {
                                //            FileStream stream = null;
                                //            try
                                //            { //Don't change FileAccess to ReadWrite,
                                //                //because if a file is in readOnly, it fails.
                                //                //FileStream stream = null;
                                //                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                                //            }
                                //            catch (IOException)
                                //            { //the file is unavailable because it is:
                                //                //still being written to or being processed by another thread
                                //                //or does not exist (has already been processed)
                                //                //return true;
                                //            }
                                //            finally
                                //            {
                                //                if (stream == null)
                                //                    stream.Close();
                                //                file.Delete();
                                //            }
                                //        }

                                //    }
                                //    //file.Delete();
                                //    //GC.Collect();
                                //    //System.IO.File.Copy(FileName, tPath);                                   
                                //}
                                //catch (Exception)
                                //{
                                //}                               
                            }




                            //if (!File.Exists(tPath))
                            //{
                            //    System.IO.File.Copy(FileName, tPath);
                            //    chkpath = "2";
                            //}                            
                            //else
                            //{
                            //    try
                            //    {
                            //        //GC.Collect();
                            //        //System.IO.File.Delete(tPath);                                    

                            //        FileInfo file = new FileInfo(tPath);
                            //        if (file.Exists)
                            //        {
                            //            file.Delete();
                            //            //file.Replace(FileName, tPath);
                            //        }

                            //        GC.Collect();
                            //        //System.IO.File.Copy(FileName, tPath);                                   
                            //    }
                            //    catch (Exception)
                            //    {
                            //    }
                            //}
                            string str = FileName;

                            string[] strFl = str.Split('\\');
                            string pathname = "";
                            foreach (string word in strFl)
                            {
                                word.ToString();
                                strItemId = word.ToString();
                            }
                            string str1 = "";
                            if (strItemId.Contains('.'))
                            {
                                int index = strItemId.IndexOf('.');
                                str1 = strItemId.Substring(0, index);
                            }
                            if (chkpath == "1")
                            {
                                sp_cmd.Parameters.AddWithValue("@itemLocation", "\\GroupImage\\" + str1 + ".jpeg");
                                pathname = System.Windows.Forms.Application.StartupPath + "\\GroupImage\\" + str1 + ".jpeg";
                            }
                            else
                            {
                                sp_cmd.Parameters.AddWithValue("@itemLocation", "\\GroupImage\\" + TxtGroupName.Text + ".jpeg");
                            }                            
                            sp_cmd.Parameters.AddWithValue("@imageVisibility", cmbImageVisibility.Text.Trim());
                            //cmd_photo = new SqlCommand("Update item_GroupTable set GroupPos='" + TxtPosition.Text + "',Items_Image=@sphoto,Group_Color='" + cmb_GroupColor.Text + "',Group_visibility='" + visibility + "',Font_Color='" + cmb_fontColor.Text + "' where item_groupname='" + TxtGroupName.Text + "'", con);
                            
                            sp_cmd.ExecuteNonQuery();

                            if (chkpath == "1")
                            {
                                FileInfo files = new FileInfo(tPath);
                                if (files.Exists)
                                {
                                    //files.CopyTo(FileName,pathname);
                                    System.IO.File.Copy(FileName, pathname);
                                    //files.Delete();
                                }
                            }
                            con.Close();
                            btn_unit_save.Text = "Save";
                            break;
                        }
                        if (i == 0)
                        {
                            //SqlCommand sp_cmd = new SqlCommand("sp_Group_Insert2", con);
                            //sp_cmd.CommandType = CommandType.StoredProcedure;
                            //sp_cmd.Parameters.AddWithValue("@Item_groupno", tMaxGroupNo);
                            //sp_cmd.Parameters.AddWithValue("@Item_groupname", TxtGroupName.Text);
                            //sp_cmd.Parameters.AddWithValue("@Item_groupmtname", "PRIMARY");
                            //sp_cmd.Parameters.AddWithValue("@Item_grouplevel", '0');
                            //sp_cmd.Parameters.AddWithValue("@Item_groupunder", '0');
                            //sp_cmd.Parameters.AddWithValue("@Item_Commodity", "");
                            //sp_cmd.Parameters.AddWithValue("@Item_groupgno", '0');
                            //sp_cmd.Parameters.AddWithValue("@Item_groupflag", '0');
                            //sp_cmd.Parameters.AddWithValue("@Std_Group", '1');
                            //sp_cmd.Parameters.AddWithValue("@GroupPos ", TxtPosition.Text);
                            ////sp_cmd.Parameters.AddWithValue("@Items_Image ", "@sphoto");
                            //sp_cmd.Parameters.AddWithValue("@Group_Color ", cmb_GroupColor.Text);
                            //sp_cmd.Parameters.AddWithValue("@Group_visibility ", visibility);
                            //sp_cmd.Parameters.AddWithValue("@Font_Color ", cmb_fontColor.Text);

                            conv_photo();
                            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\GroupImage\\" + TxtGroupName.Text.Trim() +".jpeg"))
                            {
                                System.IO.File.Copy(FileName, System.Windows.Forms.Application.StartupPath + "\\GroupImage\\" + TxtGroupName.Text.Trim() + ".jpeg");
                            }
                            string tPath = System.Windows.Forms.Application.StartupPath + "\\GroupImage\\" + TxtGroupName.Text.Trim() + ".jpeg";
                            cmd_photo = new SqlCommand(@"insert into Item_Grouptable(Item_groupno,Item_groupname,Item_groupmtname,Item_grouplevel,Item_groupunder,Item_Commodity,Item_groupgno,Item_groupflag,Std_Group,GroupPos,Items_Image,Group_Color,Group_visibility,Font_Color,ImageLocation,ImageVisibility) Values 
                                (@Item_groupno,@Item_groupname,'PRIMARY','0','0','','0','0','1',@GroupPos,'" + imgByteArr + "',@Group_Color,@Group_visibility,@Font_Color,@Items_Image,'" + cmbImageVisibility.Text.Trim() + "')", con);
                            cmd_photo.Parameters.AddWithValue("@Item_groupno", tMaxGroupNo);
                            cmd_photo.Parameters.AddWithValue("@Item_groupname", TxtGroupName.Text);
                            cmd_photo.Parameters.AddWithValue("@GroupPos", TxtPosition.Text);
                            cmd_photo.Parameters.AddWithValue("@Items_Image", "\\GroupImage\\" + TxtGroupName.Text.Trim() + ".jpeg");
                            cmd_photo.Parameters.AddWithValue("@Group_Color", cmb_GroupColor.Text);
                            cmd_photo.Parameters.AddWithValue("@Group_visibility", visibility);
                            cmd_photo.Parameters.AddWithValue("@Font_Color", cmb_fontColor.Text);
                            con.Close();
                            con.Open();
                            cmd_photo.ExecuteNonQuery();

                            //Alter Column:
                            //Update Discount Group Wise, settings used to save the values: At the Record Save Time:
                            SqlCommand cmdDiscountType = new SqlCommand("Update Item_Grouptable set DisPerAmtType=@DiscountType,DisPerAmt=@PerAmtType where item_groupname=@GroupName", con);
                            cmdDiscountType.Parameters.AddWithValue("@DiscountType", string.IsNullOrEmpty(CmbDisType.Text) ? "0" : discountType.ToString());
                            cmdDiscountType.Parameters.AddWithValue("@PerAmtType", string.IsNullOrEmpty(TxtDisAmt.Text) ? "0" : TxtDisAmt.Text);
                            cmdDiscountType.Parameters.AddWithValue("@GroupName", TxtGroupName.Text.Trim());
                            cmdDiscountType.ExecuteNonQuery();

                            SqlCommand cmdUpdateCommission = new SqlCommand("Update item_Grouptable set Commission=@Commission Where item_GroupName=@GroupName", con);
                            cmdUpdateCommission.Parameters.AddWithValue("@GroupName", TxtGroupName.Text);
                            cmdUpdateCommission.Parameters.AddWithValue("@Commission", string.IsNullOrEmpty(txtCommission.Text) ? 0 : Convert.ToDouble(txtCommission.Text));
                            cmdUpdateCommission.ExecuteNonQuery();


                            con.Close();
                            SqlCommand cmd11 = new SqlCommand("update NumberTable set item_groupNo=Item_groupNo+1", con);
                            con.Close();
                            con.Open();
                            cmd11.ExecuteNonQuery();                           
                            con.Close();
                        }
                        check = null;
                    }
                    clear();
                    loadGroup();
                }
                else
                {
                    MyMessageBox.ShowBox("Please Enter Group Name", "Warning");
                    TxtGroupName.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void TxtGroupName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode ==Keys.Enter || e.KeyCode == Keys.Tab )
            {
                TxtUnder.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //   btn_unit_Exit.Select();
            //}
        }

        private void TxtUnder_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TxtPosition.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //    TxtGroupName.Select();
            //}
        }

        private void TxtPosition_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               TxtVisibility.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //   TxtUnder.Select();
            //}
        }

        private void TxtVisibility_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               cmb_GroupColor.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            // TxtPosition.Select();
            //}
        }

        private void cmb_GroupColor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              BtnImage.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //   TxtVisibility.Select();
            //}
        }

        private void BtnImage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                funSelectImage();

                cmb_fontColor.Select();
            }
            if (e.KeyCode == Keys.Tab)
            {
                cmb_fontColor.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //   cmb_GroupColor.Select();
            //}
        }

        private void cmb_fontColor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              cmbImageVisibility.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //    BtnImage.Select();
            //}
        }

        private void btn_unit_save_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               
                btn_unit_Clear.Select();
                funRecordSave();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //   cmb_fontColor.Select();
            //}
        }

        private void btn_unit_Clear_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               btn_unit_Exit.Select();
               clear();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //  btn_unit_save.Select();
            //}
        }

        private void btn_unit_Exit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              TxtGroupName.Select();
            }
            //if (e.KeyCode == Keys.Back)
            //{
            //   btn_unit_Clear.Select();
            //}
        }
        string txtupdateModel = null;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtGroupName.Text.Trim() == "Primary")
                {
                    MyMessageBox.ShowBox("Cannot Delete Standard Group", "Warning");
                }
                else
                {
                    con.Close();
                    con.Open();
                    string BrandNoqry = "select Item_groupno from Item_Grouptable where Item_groupname=@tName";
                    SqlCommand cmdBrand= new SqlCommand(BrandNoqry,con);
                    cmdBrand.Parameters.AddWithValue("@tName", txtupdateModel);
                    string BrandNO=cmdBrand.ExecuteScalar().ToString();
                    con.Close();
                    con.Open();
                    string GetchkBrandCode = "select * from Item_table where item_Groupno=@tNumber";
                    SqlCommand cmdGetChkBrandCode = new SqlCommand(GetchkBrandCode, con);
                    cmdGetChkBrandCode.Parameters.AddWithValue("@tNumber", BrandNO);
                    var UsedBrandNo = cmdGetChkBrandCode.ExecuteScalar();
                    con.Close();
                    if (UsedBrandNo == null)
                    {
                    string result = MyMessageBox1.ShowBox("Do you want delete this Group?", "Delete");
                        if (result.Equals("1"))
                        {
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("delete from item_Grouptable Where item_Groupname=@tName", con);
                            cmd.Parameters.AddWithValue("@tName", txtupdateModel);
                            cmd.ExecuteNonQuery();
                            con.Close();
                           TxtGroupName.Clear();

                        }
                        if (result.Equals("2"))
                        {
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Sorry ! " + txtupdateModel + " Group is currently in Use", "Warning");
                    }
                }
               btn_unit_save.Enabled = true;
              TxtGroupName.Text = string.Empty;
              btnDelete.Enabled = false;
              //  btn_M_Update.Enabled = false;
              loadGroup();
              clear();
              TxtGroupName.Select();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.Message, "Error");
            }

        }

        private void btnColorImage_Click(object sender, EventArgs e)
        {

        }

        private void TxtGroupName_Click(object sender, EventArgs e)
        {
            string groupName = TxtGroupName.Text.ToString();
            SqlCommand cmd = new SqlCommand("Select Item_groupno from Item_Grouptable where Item_groupname=@tName", con);
            cmd.Parameters.AddWithValue("@tName", groupName);
            SqlDataAdapter adp=new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            adp.Fill(dt);
            if(dt.Rows.Count>0)
            {
                groupNo =Convert.ToInt16(dt.Rows[0]["Item_groupno"].ToString());
            }
        }

        private void TxtPosition_Leave(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmbImageVisibility_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              btn_unit_save.Select();
            }
        }

        private void TxtDisAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtCommission_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

       
    }
}
