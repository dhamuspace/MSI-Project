using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Configuration;
using System.Text.RegularExpressions;


namespace MSPOSBACKOFFICE
{
    public partial class ItemCreations : Form
    {
        DataTable dt = new DataTable();
        StringBuilder str = new StringBuilder();
        string listActionType;
        bool IsBound = false;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        DataTable dt2 = new DataTable();
        string id_num;
        public int openingstock = 0;
        public int aloopstart = 0;
        public int aloopend = 0;

        public ItemCreations(string number_tabel)
        {
            InitializeComponent();
            foreach (DataGridViewColumn col in myDataGrid1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                this.myDataGrid1.DefaultCellStyle.ForeColor = Color.Black;
            }
            id_num = number_tabel;
            pnl_SerialNo.Visible = false;

        }
        void conv_photo()
        {
            //converting photo to binary data
            ms = new MemoryStream();
            picbox.Image.Save(ms, ImageFormat.Jpeg);
            byte[] photo_aray = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(photo_aray, 0, photo_aray.Length);
            cmd_photo.Parameters.AddWithValue("@sphoto", photo_aray);
        }
        //string items_code;
        string split3 = "";
        string split33 = "";
        string split333 = "";
        string chekitemcode_exit = "";

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbarcode.Text.Trim() != "" && txtQty.Text.Trim() != "" && txtRate.Text != "")
                { }
                else
                {
                    txtbarcode.Text = "";
                    txtQty.Text = "";
                    txtRate.Text = "";
                }
                if (txtBarcode1.Text.Trim() != "" && txtQty1.Text.Trim() != "" && txtRate1.Text != "")
                { }
                else
                {
                    txtBarcode1.Text = "";
                    txtQty1.Text = "";
                    txtRate1.Text = "";
                }
                if (txtBarcode2.Text.Trim() != "" && txtQty2.Text.Trim() != "" && txtRate2.Text.Trim() != "")
                {
                }
                else
                {
                    txtBarcode2.Text = "";
                    txtQty2.Text = "";
                    txtRate2.Text = "";
                }
                if (txtCode.Text.Trim() != txtName.Text.Trim())
                {
                    save_thins();
                }
                else
                {
                    MyMessageBox.Showbox("Same ItemCode", "Warning");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string tuenmae;
        string ratenumber;

        string imagaefielname = "";
        string ItemOPenqty = "";

        public static Boolean IsFileLocked(FileInfo path)
        {
            FileStream stream = null;
            try
            { //Don't change FileAccess to ReadWrite,
                //because if a file is in readOnly, it fails.
                stream = path.Open(FileMode.Open, FileAccess.Read, FileShare.None);
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
        string IMAGELOCATIONDELETE = "";
        string RetuMehod = "";
        int strStockType;

        public void save_thins()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Regex regex = new Regex("[^a0-z9A0-Z9]");
                string RgxItemCode = regex.Replace(txtCode.Text, "");
                string RgxItemName = regex.Replace(txtName.Text, "");
                string upper = (RgxItemName + RgxItemCode).ToString();
                string upper1 = (RgxItemCode + RgxItemName).ToString();
                //btn_save.BackColor = Color.SkyBlue;
                if (Pnl_Back.Visible == true)
                {
                    Pnl_Back.Visible = false;
                }
                RetuMehod = "";

                // lOOP JUMBPING HERE

                if (id_num != "")
                {
                    Updatecarryon();
                    if (txtName.Text != "" && txtModel.Text != "" && txtGroup.Text != "" && txtUnit.Text != "")
                    {
                        if (txtstopatqty.Text == "Yes")
                        {
                            tuenmae = "True";
                        }
                        if (txtstopatqty.Text != "Yes")
                        {
                            tuenmae = "False";
                        }
                        if (txtstopatRate.Text == "Yes")
                        {
                            ratenumber = "True";
                        }
                        if (txtstopatRate.Text != "Yes")
                        {
                            ratenumber = "False";
                        }
                        if (txtopneItem.Text.Trim() == "Yes")
                        {
                            ItemOPenqty = "True";
                        }
                        else
                        {
                            ItemOPenqty = "False";
                        }
                        string itemnumbergetagain = "";
                        itemnumbergetagain = id_num.ToString();
                        imagaefielname = "";

                        SqlCommand cmdGettingid1 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where item_no='" + id_num + "'", con);

                        SqlDataAdapter adpGettin1 = new SqlDataAdapter(cmdGettingid1);
                        DataTable dtgettingid1 = new DataTable();
                        dtgettingid1.Rows.Clear();
                        adpGettin1.Fill(dtgettingid1);
                        if (dtgettingid1.Rows.Count > 0)
                        {
                            if (dtgettingid1.Rows[0]["ItemPicture"].ToString() != "" && dtgettingid1.Rows[0]["ItemPicture"].ToString() != null)
                            {
                                imagaefielname = dtgettingid1.Rows[0]["ItemPicture"].ToString();
                            }
                        }
                        SqlCommand cmd_itemsetable = new SqlCommand("Sp_Item_Seltable", con);
                        cmd_itemsetable.CommandType = CommandType.StoredProcedure;
                        cmd_itemsetable.Parameters.AddWithValue("@Item_no", id_num);
                        cmd_itemsetable.Parameters.AddWithValue("@item_code", txtCode.Text.Trim());

                        Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                        Encoding Utf8 = Encoding.UTF8;
                        byte[] utf8Bytes = Utf8.GetBytes(txtName.Text.Trim()); // Unicode -> UTF-8
                        string ItemNameDecoded = Windows1252.GetString(utf8Bytes); // Mis-decode as Latin1
                        //MessageBox.Show(ItemNameDecoded, "Mis-decoded");  // Shows your garbage string.

                        cmd_itemsetable.Parameters.AddWithValue("@Item_name", ItemNameDecoded);
                        cmd_itemsetable.Parameters.AddWithValue("@Printer_name", txtPrinterName.Text.Trim());

                        //cmd_itemsetable.Parameters.AddWithValue("@Item_name", txtName.Text.Trim());
                        //cmd_itemsetable.Parameters.AddWithValue("@Printer_name", txtPrinterName.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@unit_name", txtUnit.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@nt_openqty", txtNtOpen.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@stkopenqty", tuenmae.ToString().Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@stopatRate", ratenumber.ToString().Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@groupName", txtGroup.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@ModelName", txtModel.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@UnitName", txtUnit.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@BrandName", txtBrand.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@Item_Cost", txtCost.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@Item_Price", txtPrice.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@ItemSpecial1", txtSpecial_1.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@ItemSpecial2", txtSpecial_2.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@ItemSpecial3", txtSpecial_3.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@Reorder", txtReorder.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@MiniStock ", txtMinistck.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@MaxStock ", txt_Maxstck.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@PricePerRate", txtPRate.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@StopatQty", tuenmae.ToString().Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@TaxType", (txtTaxType.Text.Trim() == null || txtTaxType.Text.Trim() == "") ? "0" : txtTaxType.Text.Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@Item_selname", upper.ToString().Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@Item_mtselname", upper1.ToString().Trim());
                        cmd_itemsetable.Parameters.AddWithValue("@IetmOpenQty", ItemOPenqty);
                        cmd_itemsetable.Parameters.AddWithValue("@ItemPossition", txtitem_possition.Text == "" ? "1" : txtitem_possition.Text.Trim());

                        //Added Newly One values here:
                        cmd_itemsetable.Parameters.AddWithValue("@Active", txtActive.Text.Trim() == "ACTIVE" ? "True" : "False");

                        //update serial
                        cmd_itemsetable.Parameters.AddWithValue("@ItemSerial", (strSerialNo.ToString() == null || strSerialNo.ToString() == "") ? "0" : strSerialNo);
                        if (txtStockType.Text == "Normal")
                        {
                            strStockType = 0;
                        }
                        else if (txtStockType.Text == "Serial")
                        {
                            strStockType = 1;
                        }
                        cmd_itemsetable.Parameters.AddWithValue("@StockType", strStockType);

                        DataTable dt_gridload = new DataTable();
                        dt_gridload.Rows.Clear();
                        dt_gridload.Columns.Add("Item_no");
                        dt_gridload.Columns.Add("Barcode");
                        dt_gridload.Columns.Add("MTBarcode");
                        dt_gridload.Columns.Add("barcodevalues");
                        dt_gridload.Columns.Add("rate");
                        dt_gridload.Columns.Add("qty");
                        if (myDataGrid1.Rows.Count > 0)
                        {
                            myDataGrid1.Rows.Add();
                            for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                            {
                                string find_barcode_specieal_char = Convert.ToString(myDataGrid1.Rows[i].Cells["Column"].Value).ToString();
                                int check = find_barcode_specieal_char.IndexOf('*');
                                //MessageBox.Show(check.ToString());
                                if (check != -1)
                                {
                                    string items_name;
                                    items_name = Convert.ToString(myDataGrid1.Rows[i].Cells["Column"].Value).ToString();
                                    string textbox_values = txtbarcode_entry.Text;
                                    string[] words = items_name.Split('*');
                                    string name5 = words[0].ToString();
                                    string name2 = words[1].ToString();
                                    string add = name5 + name2;
                                    //SqlCommand cmd4 = new SqlCommand("insert into barcode_Table(item_no,Barcode,MTBarcode,barcodevalues)values('" + id_num + "','" + items_name + "','" + add + "','0')", con);
                                    //cmd4.ExecuteNonQuery();
                                    dt_gridload.Rows.Add(id_num, items_name, add, 0, 0, 0);
                                }
                                else
                                {
                                    if (myDataGrid1.Rows[i].Cells[0].Value != null && myDataGrid1.Rows[i].Cells[0].Value != "")
                                    {
                                        dt_gridload.Rows.Add(id_num, myDataGrid1.Rows[i].Cells[0].Value, 0, 0, 0, 0);
                                    }
                                }

                            }
                            if (dt_gridload.Rows.Count > 0)
                            {
                                cmd_itemsetable.Parameters.AddWithValue("@dt_gridload", dt_gridload);
                            }
                        }
                        SqlParameter retu1 = new SqlParameter("@jak", SqlDbType.VarChar, 50);
                        retu1.Direction = ParameterDirection.Output;
                        cmd_itemsetable.Parameters.Add(retu1);
                        //  resu=Convert.ToString(cmd_itemsetable.Parameters.Add(retu1)).ToString();
                        cmd_itemsetable.ExecuteNonQuery();

                        SqlCommand cmddelete = new SqlCommand("delete from serialno_transtbl where pur_sal_ref_no = 0 and inout = 1 and  barcodeno = '" + txtCode.Text.ToString() + "'", con);
                        cmddelete.ExecuteNonQuery();

                        for (int f = 0; f < myDataGridopstock.Rows.Count - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); f++)
                        {
                            //if (DgPurchase.Rows[i].Cells["itemcode"].Value.ToString() == myDataGrid12.Rows[f].Cells["Serialitemcode12"].Value.ToString())
                            //{
                            SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values( 0 ,'" + myDataGridopstock.Rows[f].Cells["SerialNoopstock"].Value.ToString() + "','" + txtCode.Text + "','1')", con);
                            cmdserial.ExecuteNonQuery();
                            //}
                        }



                        MyMessageBox.ShowBox("Item updated Successfully", "Message");

                        if (retu1.Value.ToString() == "2")
                        {

                        }
                        if (txtbarcode.Text.Trim() != "")
                        {
                            barcodemethod(id_num, txtbarcode.Text, split3, txtQty.Text, txtRate.Text);
                        }
                        if (txtBarcode1.Text.Trim() != "")
                        {
                            barcodemethod(id_num, txtBarcode1.Text, split33, txtQty1.Text, txtRate1.Text);
                        }
                        if (txtBarcode2.Text.Trim() != "")
                        {
                            barcodemethod(id_num, txtBarcode2.Text, split333, txtQty2.Text, txtRate2.Text);
                        }
                        if (picbox.Image != null)
                        {
                            cmd_photo = new SqlCommand("insert into additionalinfo(Item_No,items_color,font_color,remarks) values( '" + id_num + "','" + cmbitemColors.Text + "','" + cmb_fontColor.Text + "','" + txt_remarks.Text + "')", con);
                            // conv_photo();
                            cmd_photo.ExecuteNonQuery();
                        }
                        else
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            if (cmbitemColors.Text.Trim() != "" || cmbitemColors.Text.Trim() != "" || txt_remarks.Text.Trim() != "")
                            {
                                cmd_photo = new SqlCommand("insert into additionalinfo(Item_No,items_color,font_color,remarks) values( '" + id_num + "','" + cmbitemColors.Text + "','" + cmb_fontColor.Text + "','" + txt_remarks.Text + "')", con);
                                cmd_photo.ExecuteNonQuery();
                            }
                        }
                        string query = "";
                        #region
                        if (picbox.Image != null)
                        {
                            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\ItemImage"))
                            {
                                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\ItemImage");
                            }
                            string tPath = System.Windows.Forms.Application.StartupPath + "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg";
                            IMAGELOCATIONDELETE = tPath;
                            if (!File.Exists(tPath))
                            {

                                //System.IO.File.Delete(tPath);
                                //  System.IO.File.Copy(FileName, tPath);

                                // System.IO.File.Delete(tPath);
                                // System.IO.File.Copy(FileName, tPath);

                                Image thumbNail = picbox.Image.GetThumbnailImage(800, 600, null, new IntPtr());
                                System.IO.File.Copy(FileName, tPath);
                                thumbNail.Save(tPath, ImageFormat.Jpeg);


                                SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                                cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                                cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                                cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text);
                                cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg");
                                cmdUpdateimage.ExecuteNonQuery();
                            }
                            else
                            {
                                if (FileName != tPath)
                                {
                                    try
                                    {
                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                        GC.Collect();
                                        System.IO.File.Delete(tPath);
                                        System.IO.File.Copy(FileName, tPath);
                                        SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                                        cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                                        cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                                        cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text);
                                        //cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg");
                                        txtName.Text = "eggs";
                                        cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg");
                                        cmdUpdateimage.ExecuteNonQuery();

                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                else
                                {
                                    SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                                    cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                                    cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                                    cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text);
                                    cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg");
                                    cmdUpdateimage.ExecuteNonQuery();
                                }
                            }
                            //else
                            //{
                            //    try
                            //    {
                            //        int j = 1;
                            //        string tPath1 = System.Windows.Forms.Application.StartupPath + "\\ITemImage\\" + txtName.Text.Trim() + ".jpeg";
                            //    End1:
                            //        if (System.IO.File.Exists(tPath1))
                            //        {
                            //            j = ++j;
                            //            tPath1 = System.Windows.Forms.Application.StartupPath + "\\ITemImage\\" + txtName.Text.Trim() + j + ".jpeg";
                            //            goto End1;
                            //        }
                            //        else
                            //        {
                            //            //  System.IO.File.Delete(tPath1);
                            //            System.IO.File.Copy(FileName, tPath1);
                            //            SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                            //            cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                            //            cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                            //            string itemimgaechk = "\\ItemImage\\" + txtName.Text.Trim() + j + ".jpeg";
                            //            cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text.Trim());
                            //            cmdUpdateimage.Parameters.AddWithValue("@ItemCode", itemimgaechk.ToString().Trim());
                            //            cmdUpdateimage.ExecuteNonQuery();
                            //            query = "1";
                            //        }

                            //    }
                            //    catch (Exception)
                            //    {
                            //    }
                            //}
                        }
                        else
                        {
                            if (picbox.Image == null)
                            {
                                SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                                cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                                cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                                cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text);
                                cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "");
                                cmdUpdateimage.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        id_num = "";
                        clear();
                        if (query == "1")
                        {
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            //GC.Collect();
                            //System.IO.File.Delete(IMAGELOCATIONDELETE);
                        }
                        this.Close();
                    }
                    else if (txtName.Text == "")
                    {
                        MyMessageBox.ShowBox("Type the Item Name ", "Warning");
                        txtName.Focus();
                    }
                    else if (txtUnit.Text == "")
                    {
                        MyMessageBox.ShowBox("Unit Name Empty", "Warning");
                    }
                    else if (txtGroup.Text == "")
                    {
                        MyMessageBox.ShowBox("Group Name Empty", "Warning");
                        txtModel.Focus();
                    }
                    else if (txtModel.Text == "")
                    {
                        MyMessageBox.ShowBox("Model Name Empty", "Warning");
                        txtModel.Focus();
                    }
                    else if (txtBrand.Text == "")
                    {
                        MyMessageBox.ShowBox("Brand Name Empty", "Warning");
                        txtBrand.Focus();
                    }
                }


                else

                // New Item Insertion Method
                {
                    chekitemcode_exit = "Exit";
                    if (txtCode.Text != "")
                    {
                        ITCode = "1";
                        Check_Item_Code();
                    }
                    if (txtName.Text.Trim() != "" && chekitemcode_exit.ToString().Trim() != "Exit")
                    {
                        ITCode = "2";
                        Check_Item_Code();
                    }
                    else
                    {
                        if (chekitemcode_exit.ToString().ToUpper().Trim() == "EXIT" || chekitemcode_exit.ToString().ToUpper().Trim() == "")
                        {
                            if (txtCode.Text != "")
                            {
                                ITCode = "1";
                                Check_Item_Code();
                            }
                            if (txtName.Text != "")
                            {
                                ITCode = "2";
                                Check_Item_Code();
                            }
                        }
                    }
                    if (RetuMehod.ToString().Trim() == "")
                    {
                        if (txtName.Text != "" && txtModel.Text != "" && txtGroup.Text != "" && txtUnit.Text != "")
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            if (txtstopatqty.Text == "Yes")
                            {
                                tuenmae = "True";
                            }
                            if (txtstopatqty.Text != "Yes")
                            {
                                tuenmae = "False";
                            }
                            if (txtstopatRate.Text == "Yes")
                            {
                                ratenumber = "True";
                            }
                            if (txtstopatRate.Text != "Yes")
                            {
                                ratenumber = "False";
                            }
                            if (txtopneItem.Text.Trim() == "Yes")
                            {
                                ItemOPenqty = "True";
                            }
                            else
                            {
                                ItemOPenqty = "False";
                            }
                            if (txtbarcode.Text.Trim() != "")
                            {
                                barcodemethod(id_num, txtbarcode.Text, split3, txtQty.Text, txtRate.Text);
                            }
                            if (txtBarcode1.Text.Trim() != "")
                            {
                                barcodemethod(id_num, txtBarcode1.Text, split33, txtQty1.Text, txtRate1.Text);
                            }
                            if (txtBarcode2.Text.Trim() != "")
                            {
                                barcodemethod(id_num, txtBarcode2.Text, split333, txtQty2.Text, txtRate2.Text);
                            }

                            SqlCommand cmd = new SqlCommand("SP_ItemTable", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@item_code", txtCode.Text.Trim());

                            Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                            Encoding Utf8 = Encoding.UTF8;
                            byte[] utf8Bytes = Utf8.GetBytes(txtName.Text.Trim()); // Unicode -> UTF-8
                            string ItemNameDecoded = Windows1252.GetString(utf8Bytes); // Mis-decode as Latin1
                            //MessageBox.Show(ItemNameDecoded, "Mis-decoded");  // Shows your garbage string.

                            cmd.Parameters.AddWithValue("@Item_name", ItemNameDecoded);
                            //cmd.Parameters.AddWithValue("@Printer_name", ItemNameDecoded);

                            //cmd.Parameters.AddWithValue("@Item_name", txtName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Printer_name", txtPrinterName.Text.Trim());

                            cmd.Parameters.AddWithValue("@unit_name", txtUnit.Text.Trim());
                            cmd.Parameters.AddWithValue("@nt_openqty", txtNtOpen.Text.Trim());
                            cmd.Parameters.AddWithValue("@stkopenqty", tuenmae.ToString().Trim());
                            cmd.Parameters.AddWithValue("@stopatRate", ratenumber.ToString().Trim());
                            cmd.Parameters.AddWithValue("@groupName", txtGroup.Text.Trim());
                            cmd.Parameters.AddWithValue("@ModelName", txtModel.Text.Trim());
                            cmd.Parameters.AddWithValue("@UnitName", txtUnit.Text.Trim());
                            cmd.Parameters.AddWithValue("@BrandName", txtBrand.Text.Trim());
                            cmd.Parameters.AddWithValue("@Item_Cost", txtCost.Text.Trim());
                            cmd.Parameters.AddWithValue("@Item_Price", txtPrice.Text.Trim());
                            cmd.Parameters.AddWithValue("@ItemSpecial1", txtSpecial_1.Text.Trim());
                            cmd.Parameters.AddWithValue("@ItemSpecial2", txtSpecial_2.Text.Trim());
                            cmd.Parameters.AddWithValue("@ItemSpecial3", txtSpecial_3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Reorder", txtReorder.Text.Trim());
                            cmd.Parameters.AddWithValue("@MiniStock ", txtMinistck.Text.Trim());
                            cmd.Parameters.AddWithValue("@MaxStock ", txt_Maxstck.Text.Trim());
                            cmd.Parameters.AddWithValue("@PricePerRate", txtPRate.Text.Trim());

                            cmd.Parameters.AddWithValue("@StopatQty", tuenmae.ToString().Trim());
                            cmd.Parameters.AddWithValue("@TaxType", (txtTaxType.Text.Trim() == null || txtTaxType.Text.Trim() == "") ? "0" : txtTaxType.Text.Trim());

                            cmd.Parameters.AddWithValue("@Item_selname", upper.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Item_mtselname", upper1.ToString().Trim());
                            cmd.Parameters.AddWithValue("@IetmOpenQty", ItemOPenqty);
                            cmd.Parameters.AddWithValue("@ItemPossition", txtitem_possition.Text == "" ? "1" : txtitem_possition.Text.Trim());
                            //Added Newly One values here:
                            cmd.Parameters.AddWithValue("@Active", txtActive.Text.Trim() == "ACTIVE" ? "True" : "False");
                            //Added Itemserialnumber to Item_MtRemarks1 column
                            cmd.Parameters.AddWithValue("@ItemSerial", (strSerialNo.ToString() == null || strSerialNo.ToString() == "") ? "0" : strSerialNo);
                            if (txtStockType.Text == "Normal")
                            {
                                strStockType = 0;
                            }
                            else if (txtStockType.Text == "Serial")
                            {
                                strStockType = 1;
                            }
                            cmd.Parameters.AddWithValue("@StockType", strStockType);

                            DataTable dt_gridload = new DataTable();
                            dt_gridload.Rows.Clear();
                            dt_gridload.Columns.Add("Item_no");
                            dt_gridload.Columns.Add("Barcode");
                            dt_gridload.Columns.Add("MTBarcode");
                            dt_gridload.Columns.Add("barcodevalues");
                            dt_gridload.Columns.Add("rate");
                            dt_gridload.Columns.Add("qty");
                            if (myDataGrid1.Rows.Count > 0)
                            {
                                myDataGrid1.Rows.Add();
                                for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                                {
                                    //  if (myDataGrid1.Rows[i].Cells[0].Value.ToString() != null && myDataGrid1.Rows[i].Cells[0].Value.ToString() != "")
                                    {
                                        string find_barcode_specieal_char = Convert.ToString(myDataGrid1.Rows[i].Cells["Column"].Value).ToString();
                                        int check = find_barcode_specieal_char.IndexOf('*');
                                        //MessageBox.Show(check.ToString());
                                        if (check != -1)
                                        {
                                            string items_name;
                                            items_name = Convert.ToString(myDataGrid1.Rows[i].Cells["Column"].Value).ToString();
                                            string textbox_values = txtbarcode_entry.Text;
                                            string[] words = items_name.Split('*');
                                            string name5 = words[0].ToString();
                                            string name2 = words[1].ToString();
                                            string add = name5 + name2;
                                            dt_gridload.Rows.Add(id_num, items_name, add, 0, 0, 0);
                                        }
                                        else
                                        {
                                            if (myDataGrid1.Rows[i].Cells[0].Value != null && myDataGrid1.Rows[i].Cells[0].Value != "")
                                            {
                                                dt_gridload.Rows.Add(id_num, myDataGrid1.Rows[i].Cells[0].Value, 0, 0, 0, 0);
                                            }
                                        }
                                    }
                                }
                                if (dt_gridload.Rows.Count > 0)
                                {
                                    cmd.Parameters.AddWithValue("@dt_gridload", dt_gridload);
                                }
                            }
                            SqlParameter retu1 = new SqlParameter("@jak", SqlDbType.VarChar, 50);
                            retu1.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(retu1);
                            cmd.ExecuteNonQuery();



                            for (int f = 0; f < myDataGridopstock.Rows.Count - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); f++)
                            {
                                //if (DgPurchase.Rows[i].Cells["itemcode"].Value.ToString() == myDataGrid12.Rows[f].Cells["Serialitemcode12"].Value.ToString())
                                //{
                                SqlCommand cmdserial = new SqlCommand("insert into serialno_transtbl (PUR_SAL_REF_NO,ITEM_NO,BARCODENO,INOUT) values( 0 ,'" + myDataGridopstock.Rows[f].Cells["SerialNoopstock"].Value.ToString() + "','" + txtCode.Text + "','1')", con);
                                cmdserial.ExecuteNonQuery();
                                //}
                            }

                            int mydatagridopstockrowscount = myDataGridopstock.Rows.Count;
                            for (int p = mydatagridopstockrowscount - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                            {
                                myDataGridopstock.Rows.RemoveAt(p - 1);
                            }

                            cmd = new SqlCommand("update item_grouptable set startingnumber =(select max(startingnumber)+1 from item_grouptable where item_groupname Like '%" + txtGroup.Text + "%') where item_groupname Like '%" + txtGroup.Text + "%' ", con);
                            cmd.ExecuteNonQuery();

                            cbocategory.Text = "";
                            txtGroup.Text = "";
                            MyMessageBox.ShowBox("Item Saved Successfully", "Message");

                            aloopstart = 0;
                            aloopend = 0;

                            if (retu1.Value.ToString() == "1")
                            {

                            }
                            SqlCommand cmdGettingid = new SqlCommand("select * from item_table with (index(IndexItem_table)) where item_name=@ItemName", con);
                            cmdGettingid.Parameters.AddWithValue("@ItemName", txtName.Text);
                            SqlDataAdapter adpGettin = new SqlDataAdapter(cmdGettingid);
                            DataTable dtgettingid = new DataTable();
                            dtgettingid.Rows.Clear();
                            adpGettin.Fill(dtgettingid);
                            if (dtgettingid.Rows.Count > 0)
                            {
                                id_num = dtgettingid.Rows[0]["item_no"].ToString();
                            }
                            if (picbox.Image != null)
                            {
                                cmd_photo = new SqlCommand("insert into additionalinfo(Item_No,items_color,font_color,remarks) values( '" + id_num + "','" + cmbitemColors.Text + "','" + cmb_fontColor.Text + "','" + txt_remarks.Text + "')", con);
                                // conv_photo();
                                cmd_photo.ExecuteNonQuery();
                            }
                            else
                            {
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Open();
                                }
                                if (cmbitemColors.Text.Trim() != "" || cmbitemColors.Text.Trim() != "" || txt_remarks.Text.Trim() != "")
                                {
                                    cmd_photo = new SqlCommand("insert into additionalinfo(Item_No,items_color,font_color,remarks) values( '" + id_num + "','" + cmbitemColors.Text + "','" + cmb_fontColor.Text + "','" + txt_remarks.Text + "')", con);
                                    cmd_photo.ExecuteNonQuery();
                                }
                            }
                            #region
                            if (picbox.Image != null)
                            {
                                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\ItemImage"))
                                {
                                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\ItemImage");
                                }
                                string tPath = System.Windows.Forms.Application.StartupPath + "\\ITemImage\\" + txtName.Text.Trim() + ".jpeg";
                                if (!File.Exists(tPath))
                                {
                                    System.IO.File.Delete(tPath);
                                    // System.IO.File.Copy(FileName, tPath);

                                    Image thumbNail = picbox.Image.GetThumbnailImage(800, 600, null, new IntPtr());
                                    System.IO.File.Copy(FileName, tPath);
                                    thumbNail.Save(tPath, ImageFormat.Jpeg);


                                    SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                                    cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                                    cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                                    cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text);
                                    cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg");
                                    cmdUpdateimage.ExecuteNonQuery();
                                }
                                else
                                {
                                    try
                                    {
                                        GC.Collect();
                                        System.IO.File.Delete(tPath);
                                        GC.Collect();
                                        // System.IO.File.Copy(FileName, tPath);

                                        Image thumbNail = picbox.Image.GetThumbnailImage(800, 600, null, new IntPtr());
                                        System.IO.File.Copy(FileName, tPath);
                                        thumbNail.Save(tPath, ImageFormat.Jpeg);

                                        SqlCommand cmdUpdateimage = new SqlCommand("SP_SelectQuery", con);
                                        cmdUpdateimage.CommandType = CommandType.StoredProcedure;
                                        cmdUpdateimage.Parameters.AddWithValue("@ActionType", "UpdateImageItem");
                                        cmdUpdateimage.Parameters.AddWithValue("@ItemName", txtName.Text);
                                        cmdUpdateimage.Parameters.AddWithValue("@ItemCode", "\\ItemImage\\" + txtName.Text.Trim() + ".jpeg");
                                        cmdUpdateimage.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                            #endregion
                            //Number table set values:
                            Updatecarryon();
                            //autonumner();
                            if (ChkCarryOn.Checked != true)
                            {
                                clear();
                            }
                            else
                            {
                                txtPrinterName.Text = "";
                            }
                            id_num = "";
                            //MyMessageBox1.ShowBox("Addedd Sucessfully","Success");
                        }
                        else if (txtName.Text == "")
                        {
                            MyMessageBox.ShowBox("Type the Item Name ", "Warning");
                            txtName.Focus();
                        }
                        else if (txtUnit.Text == "")
                        {
                            MyMessageBox.ShowBox("Unit Name Empty", "Warning");
                        }
                        else if (txtGroup.Text == "")
                        {
                            MyMessageBox.ShowBox("Group Name Empty", "Warning");
                            txtModel.Focus();
                        }
                        else if (txtModel.Text == "")
                        {
                            MyMessageBox.ShowBox("Model Name Empty", "Warning");
                            txtModel.Focus();
                        }
                        else if (txtBrand.Text == "")
                        {
                            MyMessageBox.ShowBox("Brand Name Empty", "Warning");
                            txtBrand.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string ITCode = "";
        public void Check_Item_Code()
        {
            chekitemcode_exit = "";
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = null;
            if (txtCode.Text.Trim() != "" && ITCode == "1")
            {
                cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where item_code='" + txtCode.Text.Trim() + "'", con);
            }
            if (txtName.Text.Trim() != "" && ITCode == "2")
            {
                cmd = new SqlCommand("select * from item_table with (index(IndexItem_table)) where  item_name=@ITemName", con);
                cmd.Parameters.AddWithValue("@ITemName", txtName.Text.Trim());
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dtFilee = new DataTable();
            dtFilee.Rows.Clear();
            adp.Fill(dtFilee);
            if (dtFilee.Rows.Count > 0)
            {
                if (ITCode == "1")
                {
                    MyMessageBox1.ShowBox("Already Exit This ItemCode", "Warning");
                    txtCode.Focus();
                    RetuMehod = "1";
                    chekitemcode_exit = "Exit";
                }
                if (ITCode == "2")
                {
                    MyMessageBox1.ShowBox("Already Exit This  Item Name", "Warning");
                    txtName.Focus();
                    RetuMehod = "2";
                }
            }
        }
        public void barcodemethod(string item_no, string barcode, string split, string qty, string rate)
        {
            try
            {
                SqlCommand cmd_qty = new SqlCommand("BarcodeEntry", con);
                cmd_qty.CommandType = CommandType.StoredProcedure;
                cmd_qty.Parameters.AddWithValue("@Item_no", item_no);
                cmd_qty.Parameters.AddWithValue("@Barcode", barcode);
                cmd_qty.Parameters.AddWithValue("@MTBarcode", split);
                cmd_qty.Parameters.AddWithValue("@qty", qty);
                cmd_qty.Parameters.AddWithValue("@rate", rate);
                cmd_qty.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string Vlclear = "";
        public void clear()
        {
            try
            {
                CarryLoadEvnt();
                if (Vlclear == "")
                {
                    txtCode.Text = string.Empty;
                    txtName.Text = string.Empty;
                    // unitvalueschnage = "0";
                    txtUnit.Text = string.Empty;
                    txtGroup.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtBrand.Text = string.Empty;
                    //txtStockType.Text = string.Empty;
                    txtNtOpen.Text = string.Empty;
                    txtCost.Text = string.Empty;
                    txtSpecial_1.Text = string.Empty;
                    txtSpecial_2.Text = string.Empty;
                    txtSpecial_3.Text = string.Empty;
                    txtReorder.Text = string.Empty;
                    txtMinistck.Text = string.Empty;
                    txt_Maxstck.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    txtPrinterName.Text = string.Empty;
                    txtTaxType.Text = string.Empty;
                    txtPRate.Text = string.Empty;
                    txtitem_possition.Text = string.Empty;
                    txtbarcode.Text = "";
                    txtBarcode1.Text = "";
                    txtBarcode2.Text = "";
                    txtRate.Text = "";
                    txtRate1.Text = "";
                    txtRate2.Text = "";
                    txtQty.Text = "";
                    txtQty1.Text = "";
                    txtQty2.Text = "";

                    picbox.Image = null;
                    txtStockType.Text = "Normal";
                    txtopneItem.Text = "No";
                    dt2.Rows.Clear();
                    //dataGridView1.Rows.Clear();
                    txtCode.Focus();
                    cmbitemColors.Text = "";
                    txtstopatqty.Text = "No";
                    txtstopatRate.Text = "No";
                    panel3.BackColor = Color.Transparent;
                    panel2.BackColor = Color.Transparent;
                    cmb_fontColor.Text = "";
                    panel4.BackColor = Color.Transparent;
                    btn_ColorButton.BackColor = Color.White;
                    btn_ColorButton.ForeColor = Color.Black;
                    myDataGrid1.Rows.Clear();
                    if (myDataGrid1.Rows.Count > 0)
                    {
                        for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                        {
                            myDataGrid1.Rows.RemoveAt(i);
                        }
                    }
                    id_num = "";
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string emptychkVa = "";
        public void CarryLoadEvnt()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            DataTable dtNotChange = new DataTable();

            //if (ChkCarryOn.Checked == true)
            {
                //Updatecarryon();
                SqlCommand cmd = new SqlCommand("select distinct Item_table.Item_code,Item_table.Item_name,item_table.OpenItem,Item_Printname,unit_table.unit_name,Item_Grouptable.Item_groupname,Model_table.Model_name,Brand_table.Brand_name,item_table.nt_opnqty,Tax_table.Tax_name,Item_table.nt_purqty,Item_table.StopatQty,Item_table.StopatRate,item_table.Item_ndp,Item_table.Item_cost,item_table.Item_mrsp,Item_table.Item_special1,item_table.Item_special2,Item_table.Item_special3,Item_table.Item_minstock,Item_table.Item_maxstock,item_table.Item_reorder,Item_table.Item_possition from item_table,unit_table,Brand_table,Item_Grouptable,model_table,Tax_table  where Item_table.Brand_no=Brand_table.Brand_no  and Item_table.Model_no=Model_table.Model_no and Item_table.Unit_no=unit_table.unit_no and Item_table.Tax_no=Tax_table.Tax_no and Item_table.item_Groupno=Item_Grouptable.Item_groupno and item_no=(select (CarryOn) from NumberTable where CarryOn>0)", con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dtNotChange.Rows.Clear();
                adp.Fill(dtNotChange);
                if (dtNotChange.Rows.Count > 0)
                {
                    ChkCarryOn.Checked = true;
                    // if (dtNotChange.Rows[0]["CarryOn"].ToString() != "0")
                    {
                        emptychkVa = "1";
                        txtCode.Text = dtNotChange.Rows[0]["Item_code"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_code"].ToString();
                        txtName.Text = dtNotChange.Rows[0]["Item_name"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_name"].ToString();
                        //  unitvalueschnage = "0";
                        txtUnit.Text = dtNotChange.Rows[0]["unit_name"].ToString() == "" ? "" : dtNotChange.Rows[0]["unit_name"].ToString();
                        txtGroup.Text = dtNotChange.Rows[0]["Item_groupname"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_groupname"].ToString();
                        txtModel.Text = dtNotChange.Rows[0]["Model_name"].ToString() == "" ? "" : dtNotChange.Rows[0]["Model_name"].ToString();
                        txtBrand.Text = dtNotChange.Rows[0]["Brand_name"].ToString() == "" ? "" : dtNotChange.Rows[0]["Brand_name"].ToString();
                        //txtStockType.Text = string.Empty;
                        //txtNtOpen.Text = dtNotChange.Rows[0]["nt_opnqty"].ToString() == "" ? "0.00" : Convert.ToDouble(dtNotChange.Rows[0]["nt_opnqty"].ToString()).ToString("0.00");
                        //txtPrinterName.Text = dtNotChange.Rows[0]["Item_Printname"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_Printname"].ToString();
                        txtNtOpen.Text = "0";
                        txtPrinterName.Text = "";
                        txtCost.Text = dtNotChange.Rows[0]["Item_cost"].ToString() == "" ? "0.00" : Convert.ToDouble(dtNotChange.Rows[0]["Item_cost"].ToString()).ToString("0.00");
                        txtSpecial_1.Text = dtNotChange.Rows[0]["Item_special1"].ToString() == "" ? "0.00" : Convert.ToDouble(dtNotChange.Rows[0]["Item_special1"].ToString()).ToString("0.00");
                        txtSpecial_2.Text = dtNotChange.Rows[0]["Item_special2"].ToString() == "" ? "0.00" : Convert.ToDouble(dtNotChange.Rows[0]["Item_special2"].ToString()).ToString("0.00");
                        txtSpecial_3.Text = dtNotChange.Rows[0]["Item_special3"].ToString() == "" ? "0.00" : Convert.ToDouble(dtNotChange.Rows[0]["Item_special3"].ToString()).ToString("0.00");
                        txtReorder.Text = dtNotChange.Rows[0]["Item_reorder"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_reorder"].ToString();
                        txtMinistck.Text = dtNotChange.Rows[0]["Item_minstock"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_minstock"].ToString();
                        txt_Maxstck.Text = dtNotChange.Rows[0]["Item_maxstock"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_maxstock"].ToString();
                        txtPrice.Text = dtNotChange.Rows[0]["Item_mrsp"].ToString() == "" ? "" : Convert.ToDouble(dtNotChange.Rows[0]["Item_mrsp"].ToString()).ToString("0.00");

                        txtTaxType.Text = dtNotChange.Rows[0]["Tax_name"].ToString() == "" ? "" : dtNotChange.Rows[0]["Tax_name"].ToString();
                        txtPRate.Text = dtNotChange.Rows[0]["Item_ndp"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_ndp"].ToString();
                        txtitem_possition.Text = dtNotChange.Rows[0]["Item_possition"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_possition"].ToString();
                        txtstopatRate.Text = dtNotChange.Rows[0]["StopatQty"].ToString().Trim() == "0" ? "No" : "Yes";
                        txtstopatqty.Text = dtNotChange.Rows[0]["StopatRate"].ToString().Trim() == "0" ? "No" : "Yes";
                        txtitem_possition.Text = dtNotChange.Rows[0]["Item_possition"].ToString() == "" ? "" : dtNotChange.Rows[0]["Item_possition"].ToString();
                        txtbarcode.Text = "";
                        txtBarcode1.Text = "";
                        txtBarcode2.Text = "";
                        txtRate.Text = "";
                        txtRate1.Text = "";
                        txtRate2.Text = "";
                        txtQty.Text = "";
                        txtQty1.Text = "";
                        txtQty2.Text = "";

                        picbox.Image = null;
                        //txtStockType.Text = dtNotChange.Rows[0][""].ToString() == "" ? "" : dtNotChange.Rows[0][""].ToString();
                        txtopneItem.Text = dtNotChange.Rows[0]["OpenItem"].ToString() == "False" ? "No" : "Yes";
                        dt2.Rows.Clear();
                        txtStockType.Text = "Normal";
                        //dataGridView1.Rows.Clear();
                        txtCode.Focus();
                        cmbitemColors.Text = "";
                        panel3.BackColor = Color.Transparent;
                        panel2.BackColor = Color.Transparent;
                        cmb_fontColor.Text = "";
                        panel4.BackColor = Color.Transparent;
                        btn_ColorButton.BackColor = Color.White;
                        btn_ColorButton.ForeColor = Color.Black;
                        myDataGrid1.Rows.Clear();
                        if (myDataGrid1.Rows.Count > 0)
                        {
                            for (int i = 0; i < myDataGrid1.Rows.Count - 1; i++)
                            {
                                myDataGrid1.Rows.RemoveAt(i);
                            }
                        }
                        Vlclear = "1";
                    }
                }
                else
                {
                    Vlclear = "";
                }

            }

        }


        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
        string unitstringname;

        string taxnumber;
        SqlCommand cmd_photo;
        MemoryStream ms;
        DataTable dt3 = new DataTable();

        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collection1 = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collection2 = new AutoCompleteStringCollection();
        AutoCompleteStringCollection collection3 = new AutoCompleteStringCollection();
        string itemcodechek = "", ITEMNAME_PATH = "";
        string _Lad = "";
        private void ItemCreations_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd_new1 = new SqlCommand("select item_groupname from Item_Grouptable with (index(IndexItem_grouptable)) where Item_Groupname Like '%" + txtGroup.Text + "%' Order by Item_groupname ASC", con);
                con.Open();
                //cmd_new1.Parameters.AddWithValue("@GroupName", txtGroup.Text + '%');
                SqlDataReader reader = cmd_new1.ExecuteReader();
                while (reader.Read())
                {
                    cbocategory.Items.Add(reader[0].ToString());

                }

                reader.Close();

                System.Windows.Forms.Cursor.Position = PointToScreen(new Point(txtCode.Location.X + 5, txtCode.Location.Y + 5));
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                txtopneItem.Text = "No";
                // save_thins();
                if (id_num != "")
                {
                    panel4.Visible = false;
                    panel5.Visible = false;
                    Pnl_Back.Visible = false;
                    lblListView_controls.Visible = false;
                    lvDetailsListViews.Visible = false;

                    DataTable dt_number_table1 = new DataTable();
                    SqlCommand cmd45 = new SqlCommand("select * from item_table with (index(IndexItem_table)) where item_no='" + id_num + "'", con);
                    SqlDataAdapter adp45 = new SqlDataAdapter(cmd45);
                    dt_number_table1.Rows.Clear();
                    adp45.Fill(dt_number_table1);
                    if (dt_number_table1.Rows.Count > 0)
                    {
                        txtCode.Text = dt_number_table1.Rows[0]["item_code"].ToString();
                        itemcodechek = dt_number_table1.Rows[0]["item_code"].ToString();
                        // Converting Language for ItemName //
                        Encoding Windows1252 = Encoding.GetEncoding("Windows-1252");
                        Encoding Utf8 = Encoding.UTF8;
                        byte[] originalBytes = Windows1252.GetBytes(dt_number_table1.Rows[0]["Item_Name"].ToString());
                        string goodDecode = "";
                        goodDecode = Utf8.GetString(originalBytes);
                        //MessageBox.Show(goodDecode, "Re-decoded");
                        // Converting Language for ItemName //
                        txtName.Text = goodDecode;
                        //txtPrinterName.Text = goodDecode;
                        //txtName.Text = dt_number_table1.Rows[0]["item_name"].ToString();
                        txtPrinterName.Text = dt_number_table1.Rows[0]["Item_Printname"].ToString();
                        string group_number_nu = dt_number_table1.Rows[0]["item_Groupno"].ToString();
                        string unitnumbervalues = dt_number_table1.Rows[0]["Unit_no"].ToString();
                        if (dt_number_table1.Rows[0]["Item_Active"].ToString().Trim() == "True")
                        {
                            txtActive.Text = "ACTIVE";
                        }
                        else
                        {
                            txtActive.Text = "INACTIVE";
                        }
                        //getting unit number to name:
                        DataTable dtunitname = new DataTable();
                        SqlCommand cmdunitname = new SqlCommand("Select * from unit_table where unit_no='" + unitnumbervalues + "'", con);
                        SqlDataAdapter adpunitname = new SqlDataAdapter(cmdunitname);
                        dtunitname.Rows.Clear();
                        adpunitname.Fill(dtunitname);
                        if (dtunitname.Rows.Count > 0)
                        {
                            unitstringname = dtunitname.Rows[0]["unit_name"].ToString();
                        }
                        // unitvalueschnage = "1";
                        txtUnit.Text = unitstringname.ToString();
                        SqlCommand cmd12 = new SqlCommand("select  * from Item_Grouptable where Item_groupno='" + group_number_nu.ToString() + "'", con);
                        DataTable dt_group_nu = new DataTable();
                        SqlDataAdapter adp_grou = new SqlDataAdapter(cmd12);
                        //group number name:
                        dt_group_nu.Rows.Clear();
                        adp_grou.Fill(dt_group_nu);
                        if (dt_group_nu.Rows.Count > 0)
                        {
                            txtGroup.Text = dt_group_nu.Rows[0]["item_groupname"].ToString();
                        }
                        string Modeltext = Convert.ToString(dt_number_table1.Rows[0]["model_no"].ToString());
                        DataTable dt_model_nu = new DataTable();
                        SqlCommand cmd_num = new SqlCommand("select Model_name  from Model_table with (index(IndexModel_table)) where Model_no ='" + Modeltext + "'", con);
                        SqlDataAdapter adp_num = new SqlDataAdapter(cmd_num);
                        dt_model_nu.Rows.Clear();
                        adp_num.Fill(dt_model_nu);
                        if (dt_model_nu.Rows.Count > 0)
                        {
                            txtModel.Text = dt_model_nu.Rows[0]["model_name"].ToString();
                        }
                        string txtbrand_nu = dt_number_table1.Rows[0]["brand_no"].ToString();
                        DataTable dt_brand_table = new DataTable();
                        SqlDataAdapter adp_brna = new SqlDataAdapter("select  Brand_name from Brand_table with (index(IndexBrand_table)) where Brand_no ='" + txtbrand_nu + "'", con);
                        dt_brand_table.Rows.Clear();
                        adp_brna.Fill(dt_brand_table);
                        if (dt_brand_table.Rows.Count > 0)
                        {
                            txtBrand.Text = dt_brand_table.Rows[0]["Brand_name"].ToString();
                        }

                        txtNtOpen.Text = dt_number_table1.Rows[0]["nt_opnqty"].ToString() == "" ? "0" : Convert.ToDouble(dt_number_table1.Rows[0]["nt_opnqty"].ToString()).ToString("0");
                        txtPRate.Text = dt_number_table1.Rows[0]["Item_ndp"].ToString() == "" ? "0" : Convert.ToDouble(dt_number_table1.Rows[0]["Item_ndp"].ToString()).ToString("0.00");
                        if (dt_number_table1.Rows[0]["StopatQty"].ToString() == "True")
                        {
                            txtstopatqty.Text = "Yes";
                        }
                        if (dt_number_table1.Rows[0]["StopatQty"].ToString() == "False")
                        {
                            txtstopatqty.Text = "No";
                        }
                        if (dt_number_table1.Rows[0]["StopatRate"].ToString() == "True")
                        {
                            txtstopatRate.Text = "Yes";
                        }
                        if (dt_number_table1.Rows[0]["StopatRate"].ToString() == "False")
                        {
                            txtstopatRate.Text = "No";
                        }

                        taxnumber = dt_number_table1.Rows[0]["Tax_no"].ToString();
                        DataTable dt_Tax_table = new DataTable();
                        SqlDataAdapter adp_Tax = new SqlDataAdapter("select  Tax_Name from Tax_table with (index(IndexTax_table)) where Tax_no ='" + taxnumber + "'", con);
                        dt_Tax_table.Rows.Clear();
                        adp_Tax.Fill(dt_Tax_table);
                        if (dt_Tax_table.Rows.Count > 0)
                        {
                            txtTaxType.Text = dt_Tax_table.Rows[0]["Tax_Name"].ToString();
                        }
                        txtCost.Text = "0";
                        txtPrice.Text = dt_number_table1.Rows[0]["item_mrsp"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_mrsp"].ToString()).ToString("0.00");
                        txtSpecial_1.Text = dt_number_table1.Rows[0]["item_special1"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_special1"].ToString()).ToString("0.00");
                        txtSpecial_2.Text = dt_number_table1.Rows[0]["item_special2"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_special2"].ToString()).ToString("0.00");
                        txtSpecial_3.Text = dt_number_table1.Rows[0]["item_special3"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_special3"].ToString()).ToString("0.00");
                        txtMinistck.Text = dt_number_table1.Rows[0]["item_minstock"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_minstock"].ToString()).ToString("0.00");
                        txt_Maxstck.Text = dt_number_table1.Rows[0]["item_maxstock"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_maxstock"].ToString()).ToString("0.00");
                        txtReorder.Text = dt_number_table1.Rows[0]["Item_reorder"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["Item_reorder"].ToString()).ToString("0.00");
                        //taxnumber = dt_number_table1.Rows[0]["Tax_no"].ToString();
                        txtopneItem.Text = dt_number_table1.Rows[0]["OpenItem"].ToString().Trim() == "True" ? "Yes" : "No";
                        txtitem_possition.Text = dt_number_table1.Rows[0]["Item_possition"].ToString().Trim();
                        txtCost.Text = dt_number_table1.Rows[0]["item_cost"].ToString() == "" ? "0.00" : Convert.ToDouble(dt_number_table1.Rows[0]["item_cost"].ToString()).ToString("0.00");
                        string tFileName = (dt_number_table1.Rows[0]["ItemPicture"].ToString().Trim() == "" ? "" : System.Windows.Forms.Application.StartupPath + dt_number_table1.Rows[0]["ItemPicture"].ToString());

                        //Parthi Code Start
                        picbox.Image = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        DataTable dtImagePath = new DataTable();
                        dtImagePath.Rows.Clear();
                        string tImageQuery = "Select '" + Application.StartupPath + "'+ItemPicture as ItemPicture from Item_table where ItemPicture<>''";
                        SqlCommand cmdImagePath = new SqlCommand(tImageQuery, con);
                        SqlDataAdapter adpImagePath = new SqlDataAdapter(cmdImagePath);
                        adpImagePath.Fill(dtImagePath);

                        String searchFolder = Application.StartupPath + "\\ItemImage";
                        var filters = new String[] { "jpg", "jpeg", "png", "gif", "bmp" };
                        string[] tImagePath = GetFilesFrom(searchFolder, filters, false);

                        for (int mn = 0; mn < tImagePath.Length; mn++)
                        {

                            bool isChkNew = false;
                            for (int ij = 0; ij < dtImagePath.Rows.Count; ij++)
                            {
                                isChkNew = false;
                                if (tImagePath[mn].ToString().Trim() == dtImagePath.Rows[ij]["ItemPicture"].ToString().Trim())
                                {
                                    isChkNew = true;
                                    break;
                                }
                            }
                            if (isChkNew == false)
                            {
                                try
                                {
                                    picbox.Image = null;
                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();
                                    GC.Collect();

                                    File.Delete(tImagePath[mn].ToString().Trim());
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        //Parthi Code End

                        if (tFileName != "")
                        {

                            if (File.Exists(tFileName))
                            {
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                GC.Collect();
                                picbox.Image = null;
                                string tTempImagePath = Application.StartupPath + "\\ItemImage\\" + txtName.Text.Trim() + "1.jpeg";
                                File.Copy(tFileName, tTempImagePath);
                                picbox.Image = Bitmap.FromFile(tTempImagePath);
                                picbox.SizeMode = PictureBoxSizeMode.StretchImage;
                                picbox.Refresh();
                                FileName = tFileName.ToString();
                                ITEMNAME_PATH = tFileName.ToString();
                            }
                        }
                        //coschking = "1";
                        SqlCommand cmdtaxno = new SqlCommand(@"SELECT  distinct   dbo.Tax_table.Tax_name
                        FROM dbo.Item_table INNER JOIN dbo.Tax_table ON dbo.Item_table.Tax_no = dbo.Tax_table.Tax_no where dbo.Tax_table.Tax_no='" + taxnumber.ToString() + "'", con);
                        DataTable dttaxno = new DataTable();
                        SqlDataAdapter adptaxno = new SqlDataAdapter(cmdtaxno);
                        dttaxno.Rows.Clear();
                        adptaxno.Fill(dttaxno);
                        if (dttaxno.Rows.Count > 0)
                        {
                            txtTaxType.Text = dttaxno.Rows[0]["Tax_name"].ToString();
                        }
                        //color_changed:
                        DataTable dradditonal_info = new DataTable();
                        SqlCommand cmdadditioninfo = new SqlCommand("select * from additionalinfo where Item_No='" + id_num + "'", con);
                        SqlDataAdapter adp_addtion_info = new SqlDataAdapter(cmdadditioninfo);
                        dradditonal_info.Rows.Clear();
                        //dr.Dispose();
                        adp_addtion_info.Fill(dradditonal_info);
                        if (dradditonal_info.Rows.Count > 0)
                        {
                            string back_color = dradditonal_info.Rows[0]["items_color"].ToString();
                            string font_color = dradditonal_info.Rows[0]["font_color"].ToString();
                            //panel3.BackColor = Color.FromName(back_color);
                            this.panel3.BackColor = Color.FromName(back_color);
                            this.panel2.BackColor = Color.FromName(font_color);
                            if (back_color != null || back_color != "" && font_color != null || font_color != "")
                            {
                                cmbitemColors.Text = back_color.ToString();
                                cmb_fontColor.Text = font_color.ToString();

                                btn_ColorButton.BackColor = Color.FromName(back_color);
                                btn_ColorButton.ForeColor = Color.FromName(font_color);
                                btn_ColorButton.Visible = true;
                            }
                        }
                        DataTable dtbarcode_table = new DataTable();
                        SqlCommand cmdbarcode = new SqlCommand("Select * from BarCode_table where Item_No='" + id_num + "'", con);
                        SqlDataAdapter adpbarcode_table = new SqlDataAdapter(cmdbarcode);
                        adpbarcode_table.Fill(dtbarcode_table);
                        if (dtbarcode_table.Rows.Count > 0)
                        {
                            myDataGrid1.AutoGenerateColumns = false;
                            int k = 0;

                            for (int i = 0; i < dtbarcode_table.Rows.Count; i++)
                            {
                                string id = dtbarcode_table.Rows[i]["Item_no"].ToString();
                                string barcode = dtbarcode_table.Rows[i]["Barcode"].ToString();
                                string rate = dtbarcode_table.Rows[i]["rate"].ToString();
                                string amount_barcode = dtbarcode_table.Rows[i]["rate"].ToString();
                                string id_autonumber = dtbarcode_table.Rows[i]["id"].ToString();
                                if (rate == "" || rate == "0" && amount_barcode == "0" || amount_barcode == "")
                                {
                                    myDataGrid1.Rows.Add();
                                    for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
                                    {
                                        myDataGrid1.Rows[k].Cells["Column"].Value = dtbarcode_table.Rows[i]["BarCode"].ToString();
                                        k++;
                                        break;
                                        // dataGridView1.Rows.Add();
                                    }
                                }
                                else
                                {
                                    // if (i == 0)
                                    {
                                        if (txtbarcode.Text == "")
                                        {
                                            if (rate != "" && amount_barcode != "")
                                            {
                                                lbl_barcode1.Text = dtbarcode_table.Rows[i]["id"].ToString();
                                                txtbarcode.Text = dtbarcode_table.Rows[i]["BarCode"].ToString();
                                                txtRate.Text = dtbarcode_table.Rows[i]["rate"].ToString();
                                                txtQty.Text = dtbarcode_table.Rows[i]["qty"].ToString();
                                                goto End2;
                                            }
                                        }
                                    }
                                    // if (i == 1)
                                    {
                                        if (txtBarcode1.Text == "")
                                        {
                                            lbl_barcode2.Text = dtbarcode_table.Rows[i]["id"].ToString();
                                            txtBarcode1.Text = dtbarcode_table.Rows[i]["BarCode"].ToString();
                                            txtRate1.Text = dtbarcode_table.Rows[i]["rate"].ToString();
                                            txtQty1.Text = dtbarcode_table.Rows[i]["qty"].ToString();
                                            goto End2;
                                        }
                                    }
                                    //if (i == 2)
                                    {
                                        if (txtBarcode2.Text == "")
                                        {
                                            lbl_barcode3.Text = dtbarcode_table.Rows[i]["id"].ToString();
                                            txtBarcode2.Text = dtbarcode_table.Rows[i]["BarCode"].ToString();
                                            txtRate2.Text = dtbarcode_table.Rows[i]["rate"].ToString();
                                            txtQty2.Text = dtbarcode_table.Rows[i]["qty"].ToString();
                                            goto End2;
                                        }
                                    }
                                }
                            End2:
                                int jp = 0;
                            }
                        }
                        color_check();

                        //munies code show serial no

                        string strSerial = dt_number_table1.Rows[0]["stock_type"].ToString();
                        string strSrialnumbers = dt_number_table1.Rows[0]["Item_MtRemarks1"].ToString();
                        if (strSerial == "1")
                        {
                            txtStockType.Text = "Serial";
                            //pnl_SerialNo.Visible = true;
                            //txtItemSerial.Text = strSrialnumbers;
                        }
                        else
                        {
                            txtStockType.Text = "Normal";
                            //pnl_SerialNo.Visible = false;
                        }
                        //pnl_SerialNo.Visible = false;
                        txtCode.Select();
                    }
                }
                else
                {
                    _Lad = "";
                    CarryLoadEvnt();

                    txtCode.Focus();
                    txtCode.BackColor = Color.LightBlue;
                    IsBound = true;
                    panel4.Visible = false;
                    panel5.Visible = false;
                    Pnl_Back.Visible = false;

                    lblListView_controls.Visible = false;
                    lvDetailsListViews.Visible = false;
                    if (Vlclear.ToString().Trim() == "")
                    {
                        txtstopatqty.Text = "No";
                        txtstopatRate.Text = "No";
                        color_check();
                        txtUnit.Text = "";
                        txtCode.Select();
                    }
                    else
                    {

                    }
                    //pnl_SerialNo.Visible = false;
                }

                //For Color settings
                _Class.clsVariables.Sheight_Width();
                this.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Back.BackColor = Color.FromName(_Class.clsVariables.fColor);
                // Pnl_Back2.BackColor = Color.FromName(_Class.clsVariables.fColor);
                Pnl_Header.BackColor = Color.FromName(_Class.clsVariables.fPUpColor);
                Pnl_Footer.BackColor = Color.FromName(_Class.clsVariables.fPDownColor);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void color_check()
        {
            try
            {
                dt2.Columns.Add("Barcode", typeof(string));
                Type colorType = typeof(System.Drawing.Color);
                PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
                foreach (PropertyInfo c in propInfoList)
                {
                    this.cmbitemColors.Items.Add(c.Name);
                    this.cmb_fontColor.Items.Add(c.Name);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtName_Leave(object sender, EventArgs e)
        {

        }
        private void txtCost_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCost.Text == "")
                {
                    txtCost.Text = "0.00";
                }
                else
                {
                    double cost_ = 0.00;
                    cost_ = Convert.ToDouble(txtCost.Text);
                    txtCost.Text = cost_.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtSpecial_1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSpecial_1.Text == "")
                {
                    txtSpecial_1.Text = "0.00";
                }
                else
                {
                    double cost_1 = 0.00;
                    cost_1 = Convert.ToDouble(txtSpecial_1.Text);
                    txtSpecial_1.Text = cost_1.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtSpecial_2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSpecial_2.Text == "")
                {
                    txtSpecial_2.Text = "0.00";
                }
                else
                {
                    double cost_2 = 0.00;
                    cost_2 = Convert.ToDouble(txtSpecial_2.Text);
                    txtSpecial_2.Text = cost_2.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtSpecial_3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSpecial_3.Text == "")
                {
                    txtSpecial_3.Text = "0.00";
                }
                else
                {
                    double cost_3 = 0.00;
                    cost_3 = Convert.ToDouble(txtSpecial_3.Text);
                    txtSpecial_3.Text = cost_3.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtNtOpen_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtNtOpen.Text == "")
                {
                    txtNtOpen.Text = "0";
                }
                else
                {
                    double cost_4 = 0.00;
                    cost_4 = Convert.ToDouble(txtNtOpen.Text);
                    //txtNtOpen.Text = cost_4.ToString("0.00");                    
                    txtNtOpen.Text = cost_4.ToString("0");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtCode_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (emptychkVa != "1")
            //    {
            //        if (txtCode.Text != "")
            //        {
            //            Load_Code();
            //        }
            //    }
            //    emptychkVa = "";
            //}
            //catch (Exception ex)
            //{
            //    MyMessageBox.ShowBox(ex.ToString(), "Warning");
            //}
        }
        DataTable dt1_Check = new DataTable();
        DataTable dt2_Check = new DataTable();
        string codechk = "";

        public void Load_Code()
        {
            try
            {
                codechk = "";
                SqlDataAdapter adp = null;
                string code = txtCode.Text.Trim();
                dt1_Check.Rows.Clear();
                if (code.IndexOf("'") != -1)
                {
                    code = code.Replace("'", "''");
                }
                else
                {
                    code = txtCode.Text.Trim();
                }
                if (id_num == "")
                {
                    adp = new SqlDataAdapter("select Item_code from Item_Table with (index(IndexItem_table)) where Item_code='" + code + "'", con);
                    adp.Fill(dt1_Check);
                }
                else
                    if (id_num != "")
                    {
                        if (txtCode.Text.ToUpper().ToString().Trim() != itemcodechek.ToUpper().ToString().Trim())
                        {
                            adp = new SqlDataAdapter("select Item_code from Item_Table with (index(IndexItem_table)) where Item_code='" + code + "'", con);
                            adp.Fill(dt1_Check);
                        }
                    }
                if (dt1_Check.Rows.Count > 0)
                {
                    if (dt1_Check.Rows[0][0].ToString().Trim() != "")
                    {
                        MyMessageBox.ShowBox("This Code Already Exists", "Warning");
                        dt1_Check.Rows.Clear();
                        txtCode.Focus();
                        txtCode.SelectAll();
                        codechk = "chk";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }


        public void dbcheckforserial()
        {
            try
            {
                SqlDataAdapter adp = null;
                adp = new SqlDataAdapter("select Item_no from serialno_transtbl  with (index(Index_serialno)) where Item_no='" + t1 + "' and inout = 1 ", con);
                adp.Fill(dt2_Check);

                if (dt2_Check.Rows.Count > 0)
                {
                    if (dt2_Check.Rows[0][0].ToString().Trim() != "")
                    {
                        myDataGridopstock.Rows[myDataGridopstock.CurrentCell.RowIndex].Cells["SerialNoopstock"].Value = "";
                        MyMessageBox.ShowBox("This Serial No Already Exists in database ", "Warning");
                        dt2_Check.Rows.Clear();
                    }
                }

                //SqlDataAdapter adpbillno = null;
                //adpbillno = new SqlDataAdapter("select Item_no from serialno_transtbl  with (index(Index_serialno)) where Item_no='" + t1 + "' and inout = 0 ", con);
                //adp.Fill(dt2_Check);

                //if (dt2_Check.Rows.Count > 0)
                //{
                //    if (dt2_Check.Rows[0][0].ToString().Trim() != "")
                //    {
                //        textBox1.Text = dt2_Check.Rows[0][0].ToString().Trim();
                //        myDataGridopstock.Rows[myDataGridopstock.CurrentCell.RowIndex].Cells["SerialNoopstock"].Value = "";                        
                //        MyMessageBox1.ShowBox("The Bill No is " + textBox1.Text);                        
                //        dt2_Check.Rows.Clear();
                //    }
                //}


            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }






        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtCode.Text != "")
                    {
                        Load_Code();
                        if (codechk == "")
                        {
                            txtName.Focus();
                            txtCode.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        txtCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode != Keys.Delete || e.KeyCode == Keys.Back)
                //{

                //}
                if (e.KeyCode == (Keys.Enter))
                {
                    if (txtName.Text != "")
                    {
                        // txtPrinterName.Focus();
                        if (id_num == "")
                        {
                            if (txtName.Text != "")
                            {
                                if (txtName.Text.Trim() != txtCode.Text.Trim())
                                {
                                    SqlDataAdapter adp = null;
                                    string name = txtName.Text;
                                    if (name.IndexOf("'") != -1)
                                    {
                                        name = name.Replace("'", "''");
                                        adp = new SqlDataAdapter("select Item_name from Item_Table with (index(IndexItem_table)) where Item_name='" + name + "'", con);
                                    }
                                    else
                                    {
                                        adp = new SqlDataAdapter("select Item_name from Item_Table with (index(IndexItem_table)) where Item_name='" + name + "'", con);
                                    }
                                    DataTable dt_itemname = new DataTable();
                                    dt_itemname.Rows.Clear();
                                    adp.Fill(dt_itemname);
                                    if (dt_itemname.Rows.Count > 0)
                                    {
                                        MyMessageBox.ShowBox("Item Name Already Exit", "Warning");
                                        txtName.Focus();
                                    }
                                    else
                                    {
                                        txtPrinterName.Text = txtName.Text.ToString().Trim();
                                        txtPrinterName.Focus();
                                    }
                                }
                                else
                                {
                                    MyMessageBox.ShowBox("Same ItemCode", "MSPOS");
                                    txtName.Focus();
                                }
                            }
                        }
                        else
                        {
                            txtPrinterName.SelectAll();
                            txtPrinterName.Focus();
                        }
                    }
                    else
                    {
                        MyMessageBox.ShowBox("Type the Item Name ", "Warning");
                        txtName.Focus();
                    }
                }
                if (e.KeyCode == Keys.Tab)
                {
                    if (txtName.Text != "")
                    {
                        txtPrinterName.Focus();
                    }
                    else
                    {
                        txtName.Focus();
                    }
                }
                if (e.KeyCode == Keys.Tab && e.Shift)
                {
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        void RefreshCustomerList(object sender, EventArgs e)
        {
            try
            {
                unit_creation();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void RefreshCustomerList1(object sender, EventArgs e)
        {
            try
            {
                brnad_creation();
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtStockType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtNtOpen.Focus();
                txtTaxType.Focus();
            }
            if (e.KeyCode != Keys.Delete || e.KeyCode == Keys.Back)
            {
                TextBox txt = (TextBox)sender;
                txt.Text = UppercaseWords(txt.Text);
                txt.Select(txt.Text.Length, 0);
            }
            if (e.KeyCode == Keys.Space)
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text == "Normal")
                {
                    txt.Text = "Serial";
                }
                else
                {
                    txt.Text = "Normal";
                }
            }
        }
        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPrice.Focus();
            }

        }
        private void txtNtOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtStockType.Text == "Serial")
                {
                    if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                    {
                        panel5.Visible = false;
                        lvDetailsListViews.Visible = false;
                        pnl_SerialNo.Visible = true;
                        //txtItemSerial.Focus();
                    }
                    else
                    {
                        // txtTaxType.Focus();
                    }
                }
                else
                {
                    //pnl_SerialNo.Visible = false;
                    //txtTaxType.Focus();
                    txtStockType.Focus();
                }
            }

        }
        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSpecial_1.Focus();
            }

        }
        private void txtSpecial_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSpecial_2.Focus();
            }

        }
        private void txtSpecial_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSpecial_3.Focus();
            }

        }
        private void txtSpecial_3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMinistck.Focus();
            }

        }
        private void txtMinistck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Maxstck.Focus();
            }

        }
        private void txt_Maxstck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtReorder.Focus();
            }

        }
        private void txtReorder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // btn_save.BackColor = Color.Coral;
                txtitem_possition.Focus();
            }

        }
        private void txtPrinterName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chk = "1";
                txtUnit.SelectAll();
                txtUnit.Focus();
            }
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                txtName.Focus();
            }

        }
        private void txtNtOpen_Enter(object sender, EventArgs e)
        {
            if (txtNtOpen.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.LightBlue;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtNtOpen.Text == "0" || txtNtOpen.Text == "0.00")
            {
                txtNtOpen.Text = "";
            }
        }
        private void txtCost_Enter(object sender, EventArgs e)
        {
            if (txtCost.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.LightBlue;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtCost.Text == "0.00")
            {
                txtCost.Text = "";
            }
        }
        private void txtPrice_Enter(object sender, EventArgs e)
        {
            if (txtPrice.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.LightBlue;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtPrice.Text == "0.00")
            {
                txtPrice.Text = "";
            }
        }
        private void txtSpecial_1_Enter(object sender, EventArgs e)
        {
            if (txtSpecial_1.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.LightBlue;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtSpecial_1.Text == "0.00")
            {
                txtSpecial_1.Text = "";
            }
        }
        private void txtSpecial_2_Enter(object sender, EventArgs e)
        {
            if (txtSpecial_2.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.LightBlue;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;

            if (txtSpecial_2.Text == "0.00")
            {
                txtSpecial_2.Text = "";
            }
        }
        private void txtSpecial_3_Enter(object sender, EventArgs e)
        {
            if (txtSpecial_3.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.LightBlue;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtSpecial_3.Text == "0.00")
            {
                txtSpecial_3.Text = "";
            }
        }
        private void txtMinistck_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtMinistck.Text == "")
                {
                    txtMinistck.Text = "0";
                }
                else
                {
                    double mini = 0.00;
                    mini = Convert.ToDouble(txtMinistck.Text);
                    txtMinistck.Text = mini.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txt_Maxstck_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_Maxstck.Text == "")
                {
                    txt_Maxstck.Text = "0";
                }
                else
                {
                    double mini1 = 0.00;
                    mini1 = Convert.ToDouble(txt_Maxstck.Text);
                    txt_Maxstck.Text = mini1.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtReorder_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtReorder.Text == "")
                {
                    txtReorder.Text = "0";
                }
                else
                {
                    double mini1 = 0.00;
                    mini1 = Convert.ToDouble(txtReorder.Text);
                    txtReorder.Text = mini1.ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtMinistck_Enter(object sender, EventArgs e)
        {
            if (txtMinistck.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.LightBlue;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtMinistck.Text == "0" || txtMinistck.Text == "0.00")
            {
                txtMinistck.Text = "";
            }
        }
        private void txt_Maxstck_Enter(object sender, EventArgs e)
        {
            if (txt_Maxstck.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.LightBlue;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txt_Maxstck.Text == "0" || txt_Maxstck.Text == "0.00")
            {
                txt_Maxstck.Text = "";
            }
        }
        private void txtReorder_Enter(object sender, EventArgs e)
        {
            if (txtReorder.Focus() == true)
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.LightBlue;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            lblListView_controls.Visible = false;
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtReorder.Text == "0" || txtReorder.Text == "0.00")
            {
                txtReorder.Text = "";
            }
            txtUnit.Name = "1";
        }
        string accetion_type;
        private void txtUnit_Enter(object sender, EventArgs e)
        {
            try
            {
                // panel5.Visible = true;
                accetion_type = "Unit_Name";
                listActionType = "Unit";
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.LightBlue;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;

                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }

                lvDetailsListViews.Items.Clear();
                label32.Text = "List Units";
                DataTable dt_unittable = new DataTable();
                SqlCommand cmd = new SqlCommand("select unit_name  from unit_table order by unit_name ASC", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt_unittable.Rows.Clear();
                adp.Fill(dt_unittable);
                if (dt_unittable.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_unittable.Rows.Count; i++)
                    {
                        lvDetailsListViews.Items.Add(dt_unittable.Rows[i]["unit_name"].ToString());
                        if (txtUnit.Text.Trim() == "")
                        {
                            lvDetailsListViews.SetSelected(0, true);
                        }
                        else
                        {
                            string strUnit = dt_unittable.Rows[i]["unit_name"].ToString();
                            if (txtUnit.Text.ToUpper() == strUnit.ToUpper()) ;
                            //if (txtUnit.Text.Trim() == dt_unittable.Rows[i]["unit_name"].ToString())
                            {
                                lvDetailsListViews.SetSelected(i, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        DataTable dt_unit_table = new DataTable();
        public void unit_creation()
        {
            try
            {

                Lbl_itemcreation.Visible = true;
                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtBrand_Enter(object sender, EventArgs e)
        {
            try
            {
                listActionType = "Brand";
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.LightBlue;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;

                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }
                lvDetailsListViews.Items.Clear();
                label32.Text = "List Brand";
                DataTable dt_brand = new DataTable();
                SqlCommand cmd = new SqlCommand("select  Brand_name  from Brand_table with (index(IndexBrand_table)) order by Brand_name ASC", con);
                SqlDataAdapter adp_brand = new SqlDataAdapter(cmd);
                adp_brand.Fill(dt_brand);
                if (dt_brand.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_brand.Rows.Count; i++)
                    {
                        if (pnl_SerialNo.Visible == false)
                        {
                            lvDetailsListViews.Visible = true;
                        }

                        lblListView_controls.Text = "List Brand";
                        lvDetailsListViews.Items.Add(dt_brand.Rows[i]["Brand_name"].ToString());
                        if (txtBrand.Text == "")
                        {
                            lvDetailsListViews.SetSelected(0, true);
                        }
                        else
                        {
                            if (txtBrand.Text.Trim() == dt_brand.Rows[i]["Brand_name"].ToString())
                            {
                                lvDetailsListViews.SetSelected(i, true);
                            }
                        }
                    }
                }
                accetion_type = "Brand_name";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void brnad_creation()
        {
            try
            {
                lblListView_controls.Visible = true;

                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select  Brand_name  from Brand_table with (index(IndexBrand_table)) order by Brand_name ASC", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                lvDetailsListViews.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblListView_controls.Text = "List Brands";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lvDetailsListViews.Items.Add(dt.Rows[i]["Brand_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtModel_Enter(object sender, EventArgs e)
        {
            try
            {
                listActionType = "Model";

                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.LightBlue;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
                //panel5.Visible = true;
                lvDetailsListViews.Items.Clear();
                DataTable dt_selectlimodel = new DataTable();
                SqlDataAdapter cmd = new SqlDataAdapter("select  Model_name  from Model_table with (index(IndexModel_table)) order by Model_name ASC", con);
                label32.Text = "List Model";
                dt_selectlimodel.Rows.Clear();
                cmd.Fill(dt_selectlimodel);
                if (dt_selectlimodel.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_selectlimodel.Rows.Count; i++)
                    {
                        // lvDetailsListViews.Visible = true;
                        lblListView_controls.Text = "List Model";

                        lvDetailsListViews.Items.Add(dt_selectlimodel.Rows[i]["Model_name"].ToString());
                        if (txtModel.Text.Trim() == "")
                        {
                            lvDetailsListViews.SetSelected(0, true);
                        }
                        else
                        {
                            if (txtModel.Text == dt_selectlimodel.Rows[i]["Model_name"].ToString())
                            {
                                lvDetailsListViews.SetSelected(i, true);
                            }
                        }
                    }
                }
                txtModel.Name = "3";
                accetion_type = "Model_name";
                if (pnl_SerialNo.Visible == false)
                {
                    lvDetailsListViews.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void model_creation()
        {
            try
            {
                lblListView_controls.Visible = true;

                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select  Model_name  from Model_table  with (index(IndexModel_table)) order by Model_name ASC", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                lvDetailsListViews.Items.Clear();
                adp.Fill(dt);
                label32.Text = "List Models";
                if (dt.Rows.Count > 0)
                {
                    lblListView_controls.Text = "List Models";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lvDetailsListViews.Items.Add(dt.Rows[i]["model_name"].ToString());
                    }
                }

                listActionType = "Model";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtGroup_Enter(object sender, EventArgs e)
        {
            try
            {

                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }

                listActionType = "Group";
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.LightBlue;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
                if (pnl_SerialNo.Visible == false)
                {
                    lvDetailsListViews.Visible = true;
                }
                lvDetailsListViews.Items.Clear();
                SqlDataAdapter cmd = new SqlDataAdapter("select Item_groupname from Item_Grouptable with (index(IndexItem_grouptable)) order by Item_groupname ASC", con);
                DataTable dt_select = new DataTable();
                dt_select.Rows.Clear();
                cmd.Fill(dt_select);
                label32.Text = "List Groups";
                lvDetailsListViews.Items.Clear();
                if (dt_select.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_select.Rows.Count; i++)
                    {
                        lblListView_controls.Text = "List Groups";
                        lvDetailsListViews.Items.Add(dt_select.Rows[i]["Item_groupname"].ToString());
                        if (txtGroup.Text == "")
                        {
                            lvDetailsListViews.SetSelected(0, true);
                        }
                        else
                        {
                            string strGroup = dt_select.Rows[i]["Item_groupname"].ToString();
                            if (txtGroup.Text.ToUpper() == strGroup.ToUpper()) ;
                            //if (txtGroup.Text.Trim() == dt_select.Rows[i]["Item_groupname"].ToString())
                            {
                                lvDetailsListViews.SetSelected(i, true);
                                // break;
                            }
                        }
                    }
                }
                accetion_type = "Group_name";
                if (pnl_SerialNo.Visible == false)
                {
                    lvDetailsListViews.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void group_creation()
        {
            try
            {
                lblListView_controls.Visible = true;

                if (pnl_SerialNo.Visible == false)
                {
                    panel5.Visible = true;
                    lvDetailsListViews.Visible = true;
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select item_groupname from Item_Groupname with (index(IndexItem_grouptable)) order by item_groupname ASC", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                dt.Rows.Clear();
                lvDetailsListViews.Items.Clear();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblListView_controls.Text = "List Groups";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lvDetailsListViews.Items.Add(dt.Rows[i]["item_groupname"].ToString());
                    }
                }
                listActionType = "Group";
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtCode_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Focus() == true)
                {
                    txtCode.BackColor = Color.LightBlue;
                    txtName.BackColor = Color.White;
                    txtPrinterName.BackColor = Color.White;
                    txtUnit.BackColor = Color.White;
                    txtGroup.BackColor = Color.White;
                    txtModel.BackColor = Color.White;
                    txtBrand.BackColor = Color.White;
                    txtStockType.BackColor = Color.White;
                    txtNtOpen.BackColor = Color.White;
                    txtCost.BackColor = Color.White;
                    txtPrice.BackColor = Color.White;
                    txtSpecial_1.BackColor = Color.White;
                    txtSpecial_2.BackColor = Color.White;
                    txtSpecial_3.BackColor = Color.White;
                    txtMinistck.BackColor = Color.White;
                    txt_Maxstck.BackColor = Color.White;
                    txtReorder.BackColor = Color.White;
                    txtTaxType.BackColor = Color.White;
                    txtstopatqty.BackColor = Color.White;
                    txtstopatRate.BackColor = Color.White;
                    txtPRate.BackColor = Color.White;
                    txtActive.BackColor = Color.White;
                    txtopneItem.BackColor = Color.White;
                    txtitem_possition.BackColor = Color.White;
                    txtCode.SelectAll();

                }
                lblListView_controls.Visible = false;
                panel5.Visible = false;

                lvDetailsListViews.Visible = false;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtModel_Leave(object sender, EventArgs e)
        {
            if (txtModel.Text == "")
            {
                //MessageBox.Show("Model no Empty");
                //txtModel.Focus();
            }
        }
        private void txtBrand_Leave(object sender, EventArgs e)
        {
            if (txtModel.Text == "")
            {
                //MessageBox.Show("Brand No Empty");
            }
        }

        private void lvDetailsListViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listActionType == "Group" && lvDetailsListViews.SelectedItem != null)
            {
            }
            if (listActionType == "Model" && lvDetailsListViews.SelectedItem != null)
            {
            }
            if (listActionType == "Brand" && lvDetailsListViews.SelectedItem != null)
            {
            }
            if (listActionType == "Rack" && lvDetailsListViews.SelectedItem != null)
            {
            }
            if (listActionType == "Unit" && lvDetailsListViews.SelectedItem != null)
            {
                txtUnit.Text = lvDetailsListViews.SelectedItem.ToString();
                txtGroup.Select();
            }
            if (listActionType == "Tax" && lvDetailsListViews.SelectedItem != null)
            {
            }
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int curPOS = txt.SelectionStart;
            txt.Text = UppercaseWords(txt.Text);
            txt.Select(curPOS, 0);
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Updatecarryon();
            this.Close();
        }
        public void Updatecarryon()
        {

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd = null;
            if (_Lad != "CheckExit")
            {
                if (ChkCarryOn.Checked == true)
                {
                    if (txtName.Text.Trim() != "")
                    {
                        cmd = new SqlCommand("update numbertable set CarryOn=(select Max(item_no) from item_table where item_name=@itemname)", con);
                        cmd.Parameters.AddWithValue("@itemname", txtName.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("update NumberTable set CarryOn=(select max(item_no) from item_table)", con);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    cmd = new SqlCommand("update numbertable set CarryOn='0'", con);
                    cmd.ExecuteNonQuery();
                }

                //SqlCommand cmddelete = new SqlCommand("delete from serialno_transtbl where pur_sal_ref_no = " + lblBillNo.Text.ToString(), con);


            }
        }
        private void btnAdditional_info_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pnl_Back.Visible != true)
                {
                    myDataGrid1.AutoGenerateColumns = false;
                    if (myDataGrid1.Rows.Count == 0)
                    {
                        myDataGrid1.Rows.Add();
                    }
                    // btnAdditional_info.BackColor = Color.LightBlue;
                    Pnl_Back.Visible = true;
                    cmbitemColors.Focus();
                }
                else
                {
                    // btnAdditional_info.BackColor = Color.SkyBlue;
                    Pnl_Back.Visible = false;
                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void ItemCreations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.Alt && e.KeyCode == Keys.I)
            {
                Pnl_Back.Visible = true;
                cmbitemColors.Focus();
            }
            if (e.Alt && e.KeyCode == Keys.S)
            {
                save_thins();
            }
            if (e.Alt && e.KeyCode == Keys.E)
            {
                this.Close();
            }
        }
        private void cmbitemColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string color = this.cmbitemColors.SelectedItem.ToString();
                this.panel3.BackColor = Color.FromName(color);
                btn_ColorButton.Visible = true;
                btn_ColorButton.BackColor = Color.FromName(color);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void cmb_fontColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string color = this.cmb_fontColor.SelectedItem.ToString();
                this.panel2.BackColor = Color.FromName(color);
                btn_ColorButton.ForeColor = Color.FromName(color);
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void cmbitemColors_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }
        private void cmb_fontColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }
        string filename1 = null, FileName;
        private void picbox_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);      
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    FileName = openfile.FileName;
                    picbox.Image = null;
                    Image img = new Bitmap(openfile.FileName);
                    picbox.Image = img.GetThumbnailImage(340, 125, null, new IntPtr());
                    openfile.RestoreDirectory = true;
                    filename1 = openfile.FileName;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            picbox.Image = null;
        }
        private void txtbarcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtbarcode.Text != "")
                {
                    if ((txtbarcode.Text == txtBarcode1.Text) || (txtbarcode.Text == txtBarcode2.Text))
                    {
                        MessageBox.Show("Already Exits This Barcode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtbarcode.Focus();
                        txtbarcode.Text = "";
                    }
                    else
                    {
                        if (txtbarcode.Text != "")
                        {
                            txtbarcodeEntry = txtbarcode.Text;
                            grid_dublicate_();
                            if (codevalues == "2")
                            {
                                txtbarcode.Focus();
                                txtbarcode.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtBarcode1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode1.Text != "")
                {
                    if ((txtBarcode1.Text == txtbarcode.Text) || (txtBarcode2.Text == txtBarcode1.Text))
                    {
                        MessageBox.Show("Already Exits This Barcode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBarcode1.Focus();
                        txtBarcode1.Text = "";
                    }
                    else
                    {
                        if (txtBarcode1.Text != "")
                        {
                            txtbarcodeEntry = txtBarcode1.Text;
                            grid_dublicate_();
                            if (codevalues == "2")
                            {
                                txtBarcode1.Focus();
                                txtBarcode1.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtBarcode2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode1.Text != "")
                {
                    if ((txtBarcode2.Text == txtbarcode.Text) || (txtBarcode2.Text == txtBarcode1.Text))
                    {
                        MessageBox.Show("Already Exits This Barcode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBarcode2.Focus();
                        txtBarcode2.Text = "";
                    }
                    else
                    {
                        if (txtBarcode2.Text != "")
                        {
                            txtbarcodeEntry = txtBarcode2.Text;
                            grid_dublicate_();
                            if (codevalues == "2")
                            {
                                txtBarcode2.Focus();
                                txtBarcode2.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void btn_save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btn_save.Focus() == true)
                {
                    save_thins();
                }
                else
                { }
            }
        }
        string chk;
        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }
        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool isChk = false;
                if (listActionType != "Over" && listActionType != null)
                {
                    if (txtUnit.Text.Trim() != null && txtUnit.Text.Trim() != "")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        DataTable dt_unitTable = new DataTable();
                        dt_unit_table.Rows.Clear();
                        SqlCommand cmd = new SqlCommand("Select * from unit_table where unit_name like @UnitName Order by unit_name ASC", con);
                        cmd.Parameters.AddWithValue("@UnitName", txtUnit.Text.Trim() + '%');
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt_unit_table);
                        if (dt_unit_table.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_unit_table.Rows[0]["unit_name"].ToString();
                            for (int k = 0; k < lvDetailsListViews.Items.Count; k++)
                            {
                                if (tempstr == lvDetailsListViews.Items[k].ToString())
                                {
                                    lvDetailsListViews.SetSelected(k, true);
                                    txtUnit.Select();
                                    chk = "1";
                                    txtUnit.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "2";
                            if (txtUnit.Text != "")
                            {
                                string name = txtUnit.Text.Remove(txtUnit.Text.Length - 1);
                                txtUnit.Text = name.ToString();
                                txtUnit.Select(txtUnit.Text.Length, 0);
                            }
                            txtUnit.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            chk = "1";
                            txtUnit.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        }
                        else
                        {
                            chk = "1";
                        }
                        txtName_TextChanged(sender, e);
                    }

                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (lvDetailsListViews.SelectedIndex < lvDetailsListViews.Items.Count - 1)
                    {
                        lvDetailsListViews.SetSelected(lvDetailsListViews.SelectedIndex + 1, true);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (lvDetailsListViews.SelectedIndex > 0)
                    {
                        lvDetailsListViews.SetSelected(lvDetailsListViews.SelectedIndex - 1, true);
                    }
                }
                if (e.Alt && e.KeyCode == Keys.A)
                {
                    if (accetion_type == "Unit_Name")
                    {
                        Unit frm = new Unit();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                    else if (accetion_type == "Group_name")
                    {
                        frmGroupCreation frm = new frmGroupCreation();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                    else if (accetion_type == "Model_name")
                    {

                        Model frm = new Model();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                    else if (accetion_type == "Brand_name")
                    {
                        Brand frm = new Brand();
                        frm.MdiParent = this.ParentForm;
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.WindowState = FormWindowState.Normal;
                        frm.Location = new Point(0, 80);
                        frm.Show();
                    }
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (listActionType == "Unit")
                    {
                        if (lvDetailsListViews.Items.Count > 0)
                        {
                            lvDetailsListViews.Visible = false;
                            txtUnit.Text = lvDetailsListViews.SelectedItem.ToString();
                        }
                        txtGroup.Select();
                    }
                    else if (listActionType == "Group")
                    {
                        if (lvDetailsListViews.Items.Count > 0)
                        {
                            lvDetailsListViews.Visible = false;
                            txtGroup.Text = lvDetailsListViews.SelectedItem.ToString();
                        }
                        txtModel.Select();
                    }
                    else if (listActionType == "Model")
                    {
                        if (lvDetailsListViews.Items.Count > 0)
                        {
                            lvDetailsListViews.Visible = false;
                            txtModel.Text = lvDetailsListViews.SelectedItem.ToString();
                        }
                        txtBrand.Select();
                    }
                    else if (listActionType == "Brand")
                    {
                        if (lvDetailsListViews.Items.Count > 0)
                        {
                            lvDetailsListViews.Visible = false;
                            txtBrand.Text = lvDetailsListViews.SelectedItem.ToString();
                        }
                        txtNtOpen.Select();
                    }
                    else if (listActionType == "Tax")
                    {
                        if (lvDetailsListViews.Items.Count > 0)
                        {
                            lvDetailsListViews.Visible = false;
                            txtTaxType.Text = lvDetailsListViews.SelectedItem.ToString();
                        }
                        txtopneItem.Select();
                    }
                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void textBox2_press_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (chk == "2")
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }
        private void txtSpecial_2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtName_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Focus() == true)
                {
                    txtCode.BackColor = Color.White;
                    txtName.BackColor = Color.LightBlue;
                    txtPrinterName.BackColor = Color.White;
                    txtUnit.BackColor = Color.White;
                    txtGroup.BackColor = Color.White;
                    txtModel.BackColor = Color.White;
                    txtBrand.BackColor = Color.White;
                    txtStockType.BackColor = Color.White;
                    txtNtOpen.BackColor = Color.White;
                    txtCost.BackColor = Color.White;
                    txtPrice.BackColor = Color.White;
                    txtSpecial_1.BackColor = Color.White;
                    txtSpecial_2.BackColor = Color.White;
                    txtSpecial_3.BackColor = Color.White;
                    txtMinistck.BackColor = Color.White;
                    txt_Maxstck.BackColor = Color.White;
                    txtReorder.BackColor = Color.White;
                    txtPRate.BackColor = Color.White;
                    txtTaxType.BackColor = Color.White;
                    txtstopatRate.BackColor = Color.White;
                    txtstopatqty.BackColor = Color.White;
                    txtActive.BackColor = Color.White;
                    txtopneItem.BackColor = Color.White;
                    txtitem_possition.BackColor = Color.White;
                }
                lblListView_controls.Visible = false;
                panel5.Visible = false;
                lvDetailsListViews.Visible = false;
                string itemnameCheck = "";

                if (txtName.Text != "")
                {
                    itemnameCheck = txtName.Text.Trim();
                    txtName.Text = "";
                }
                if (txtName.Text == "")
                {
                    if (itemnameCheck.ToString().Trim() != "")
                    {
                        txtName.Text = itemnameCheck;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtPrinterName_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtPrinterName.Focus() == true)
                {
                    txtCost.BackColor = Color.White;
                    txtCode.BackColor = Color.White;
                    txtName.BackColor = Color.White;
                    txtPrinterName.BackColor = Color.LightBlue;
                    txtUnit.BackColor = Color.White;
                    txtGroup.BackColor = Color.White;
                    txtModel.BackColor = Color.White;
                    txtBrand.BackColor = Color.White;
                    txtStockType.BackColor = Color.White;
                    txtNtOpen.BackColor = Color.White;
                    txtCost.BackColor = Color.White;
                    txtPrice.BackColor = Color.White;
                    txtSpecial_1.BackColor = Color.White;
                    txtSpecial_2.BackColor = Color.White;
                    txtSpecial_3.BackColor = Color.White;
                    txtMinistck.BackColor = Color.White;
                    txt_Maxstck.BackColor = Color.White;
                    txtReorder.BackColor = Color.White;
                    txtPRate.BackColor = Color.White;
                    txtTaxType.BackColor = Color.White;
                    txtstopatRate.BackColor = Color.White;
                    txtstopatqty.BackColor = Color.White;
                    txtActive.BackColor = Color.White;
                    txtopneItem.BackColor = Color.White;
                    txtitem_possition.BackColor = Color.White;
                }
                lblListView_controls.Visible = false;
                panel5.Visible = false;
                lvDetailsListViews.Visible = false;
                if (txtCost.Text == "0.00")
                {
                    txtCost.Text = "";
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void btn_exit_Enter(object sender, EventArgs e)
        {
            // btn_exit.BackColor = Color.Coral;
        }
        private void btn_save_Enter(object sender, EventArgs e)
        {
            // btn_save.BackColor = Color.Coral;
        }

        private void txtbarcode_entry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                //barcode_entry();
            }
        }
        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyCode.ToString(), "\\d+"))
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtQty.Focus();
            }
        }
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtRate.Focus();
            }
        }
        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtBarcode1.Focus();
            }
        }
        private void txtBarcode1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtQty1.Focus();
            }
        }
        private void txtQty1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Enter)
            {
                txtRate1.Focus();
            }
        }
        private void txtRate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtBarcode2.Focus();
            }
        }
        private void txtBarcode2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtQty2.Focus();
            }
        }
        private void txtQty2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRate2.Focus();
            }
        }
        private void txtRate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //txtbarcode_entry.Focus();
                txt_remarks.Focus();

            }
        }
        private void cmbitemColors_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                cmb_fontColor.Focus();
            }
        }
        private void cmb_fontColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtbarcode.Focus();
            }
        }

        private void txtbarcode_entry_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (Char)Keys.Tab || e.KeyChar == (Char)Keys.Enter)
            {
                txtbarcode_entry.Focus();
            }
        }
        private void txtbarcode_Enter(object sender, EventArgs e)
        {
            if (txtbarcode.Focus() == true)
            {
                txtbarcode.BackColor = Color.LightBlue;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txtbarcode_entry.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            if (txtQty.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.LightBlue;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txtbarcode_entry.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }
        private void txtRate_Enter(object sender, EventArgs e)
        {
            if (txtRate.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.LightBlue;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;

                txt_remarks.BackColor = Color.White;
            }
        }
        bool isChk = false;
        private void txtBrand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (listActionType != "Over" && listActionType != null)
                {
                    if (txtBrand.Text.Trim() != null && txtBrand.Text.Trim() != "")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlCommand cmd1 = new SqlCommand("select Brand_name from Brand_table with (index(IndexBrand_table)) where Brand_name Like @BrandName order by Brand_name Asc", con);
                        cmd1.Parameters.AddWithValue("@BrandName", txtBrand.Text + '%');
                        SqlDataAdapter cmd = new SqlDataAdapter(cmd1);
                        DataTable dt_brand = new DataTable();
                        dt_brand.Rows.Clear();
                        cmd.Fill(dt_brand);
                        isChk = false;
                        if (dt_brand.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_brand.Rows[0]["Brand_name"].ToString();
                            for (int k = 0; k < lvDetailsListViews.Items.Count; k++)
                            {
                                if (tempstr == lvDetailsListViews.Items[k].ToString())
                                {
                                    lvDetailsListViews.SetSelected(k, true);
                                    txtBrand.Select();
                                    chk = "1";
                                    txtBrand.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtBrand.Text != "")
                        {
                            string name = txtBrand.Text.Remove(txtBrand.Text.Length - 1);
                            txtBrand.Text = name.ToString();
                            txtBrand.Select(txtBrand.Text.Length, 0);
                            chk = "1";
                        }
                        txtBrand.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);

                    }
                    else
                    {
                        chk = "1";
                    }
                    txtName_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int c = dataGridView1.CurrentCell.RowIndex;
                if (dataGridView1.CurrentCell == dataGridView1.Rows[c].Cells["BarCode"].Value != null)
                {
                    dataGridView1.Rows.Add();
                }
            }
        }
        // SqlDataReader dr1 = null;
        private void txtGroup_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (listActionType != "Over" && listActionType != null)
                {
                    if (txtGroup.Text.Trim() != null && txtGroup.Text.Trim() != "")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        SqlCommand cmd_new1 = new SqlCommand("select * from Item_Grouptable with (index(IndexItem_grouptable)) where Item_Groupname Like @GroupName Order by Item_groupname ASC", con);
                        cmd_new1.Parameters.AddWithValue("@GroupName", txtGroup.Text + '%');
                        SqlDataAdapter cmd = new SqlDataAdapter(cmd_new1);
                        DataTable dt_group = new DataTable();
                        dt_group.Rows.Clear();
                        cmd.Fill(dt_group);
                        isChk = false;
                        if (dt_group.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_group.Rows[0]["Item_Groupname"].ToString();
                            for (int k = 0; k < lvDetailsListViews.Items.Count; k++)
                            {
                                if (tempstr == lvDetailsListViews.Items[k].ToString())
                                {
                                    lvDetailsListViews.SetSelected(k, true);
                                    txtGroup.Select();
                                    chk = "1";
                                    txtGroup.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {

                            chk = "2";
                            if (txtGroup.Text != "")
                            {
                                string name = txtGroup.Text.Remove(txtGroup.Text.Length - 1);
                                txtGroup.Text = name.ToString();
                                txtGroup.Select(txtGroup.Text.Length, 0);
                            }
                            txtGroup.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            chk = "1";
                            txtGroup.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        }
                        else
                        {
                            chk = "1";
                        }
                        txtName_TextChanged(sender, e);
                    }

                }

            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (listActionType != "Over" && listActionType != null)
                {
                    if (txtModel.Text.Trim() != null && txtModel.Text.Trim() != "")
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        SqlCommand cmd = new SqlCommand("select * from model_table with (index(IndexModel_table)) where Model_name Like  @ModelName order by Model_name Asc", con);
                        cmd.Parameters.AddWithValue("@ModelName", txtModel.Text + '%');
                        isChk = false;
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dt_modle = new DataTable();
                        dt_modle.Rows.Clear();
                        adp.Fill(dt_modle);
                        if (dt_modle.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_modle.Rows[0]["Model_name"].ToString();
                            for (int k = 0; k < lvDetailsListViews.Items.Count; k++)
                            {
                                if (tempstr == lvDetailsListViews.Items[k].ToString())
                                {
                                    lvDetailsListViews.SetSelected(k, true);
                                    txtModel.Select();
                                    chk = "1";
                                    txtModel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                    }
                    if (isChk == false)
                    {
                        chk = "2";
                        if (txtModel.Text != "")
                        {
                            string name = txtModel.Text.Remove(txtModel.Text.Length - 1);
                            txtModel.Text = name.ToString();
                            txtModel.Select(txtModel.Text.Length, 0);
                        }
                        txtModel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                        chk = "1";
                        txtModel.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                    }
                    else
                    {
                        chk = "1";
                    }
                    txtName_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void lvDetailsListViews_Click(object sender, EventArgs e)
        {
            try
            {
                if (listActionType == "Group" && lvDetailsListViews.SelectedItem != null)
                {
                    listActionType = "Over";
                    if (pnl_SerialNo.Visible == false)
                    {
                        lvDetailsListViews.Visible = true;//change
                        panel5.Visible = true;
                    }

                    txtGroup.Text = lvDetailsListViews.SelectedItem.ToString();
                    txtModel.Select();
                    // txtGroup.Focus();
                }
                else if (listActionType == "Model" && lvDetailsListViews.SelectedItem != null)
                {
                    listActionType = "Over";
                    if (pnl_SerialNo.Visible == false)
                    {
                        lvDetailsListViews.Visible = true;//change
                        panel5.Visible = true;
                    }

                    txtModel.Text = lvDetailsListViews.SelectedItem.ToString();
                    txtBrand.Select();
                    //  txtModel.Focus();
                }
                else if (listActionType == "Brand" && lvDetailsListViews.SelectedItem != null)
                {
                    listActionType = "Over";
                    if (pnl_SerialNo.Visible == false)
                    {
                        lvDetailsListViews.Visible = true;//change
                        panel5.Visible = true;
                    }

                    txtBrand.Text = lvDetailsListViews.SelectedItem.ToString();
                    txtTaxType.Select();
                    // txtBrand.Focus();
                }
                else if (listActionType == "Rack" && lvDetailsListViews.SelectedItem != null)
                {
                }
                else if (listActionType == "Unit" && lvDetailsListViews.SelectedItem != null)
                {
                    listActionType = "Over";
                    if (pnl_SerialNo.Visible == false)
                    {
                        lvDetailsListViews.Visible = true;//change
                        panel5.Visible = true;
                    }

                    txtUnit.Text = lvDetailsListViews.SelectedItem.ToString();
                    txtGroup.Select();
                    txtGroup.Enter += new EventHandler(txtGroup_Enter);
                    if (pnl_SerialNo.Visible == false)
                    {
                        lvDetailsListViews.Visible = true;
                    }
                    // txtUnit.Focus();
                }
                else if (listActionType == "Tax" && lvDetailsListViews.SelectedItem != null)
                {
                    listActionType = "Over";
                    if (pnl_SerialNo.Visible == false)
                    {
                        lvDetailsListViews.Visible = true;//change
                        panel5.Visible = true;
                    }

                    txtTaxType.Text = lvDetailsListViews.SelectedItem.ToString();
                    txtstopatqty.Select();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtTaxType_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtTaxType.Focus() == true)
                {
                    txtbarcode.BackColor = Color.White;
                    txtBarcode1.BackColor = Color.White;
                    txtBarcode2.BackColor = Color.White;
                    txtRate.BackColor = Color.White;
                    txtRate1.BackColor = Color.White;
                    txtRate2.BackColor = Color.White;
                    txtQty.BackColor = Color.White;
                    txtQty1.BackColor = Color.White;
                    txtQty2.BackColor = Color.White;
                    txtbarcode_entry.BackColor = Color.White;
                    txtCode.BackColor = Color.White;
                    txtName.BackColor = Color.White;
                    txtPrinterName.BackColor = Color.White;
                    txtUnit.BackColor = Color.White;
                    txtGroup.BackColor = Color.White;
                    txtModel.BackColor = Color.White;
                    txtBrand.BackColor = Color.White;
                    txtStockType.BackColor = Color.White;
                    txtNtOpen.BackColor = Color.White;
                    txtCost.BackColor = Color.White;
                    txtPrice.BackColor = Color.White;
                    txtSpecial_1.BackColor = Color.White;
                    txtSpecial_2.BackColor = Color.White;
                    txtSpecial_3.BackColor = Color.White;
                    txtMinistck.BackColor = Color.White;
                    txt_Maxstck.BackColor = Color.White;
                    txtReorder.BackColor = Color.White;
                    txtTaxType.BackColor = Color.LightBlue;
                    txtstopatqty.BackColor = Color.White;
                    txtstopatRate.BackColor = Color.White;
                    txtopneItem.BackColor = Color.White;
                    txtPRate.BackColor = Color.White;
                    txtActive.BackColor = Color.White;
                    txtitem_possition.BackColor = Color.White;
                    listActionType = "Tax";

                    DataTable dt_tax = new DataTable();
                    SqlDataAdapter cmd = new SqlDataAdapter("select distinct *  from Tax_Table with (index(IndexTax_table)) order by Tax_Name ASC", con);
                    dt_tax.Rows.Clear();
                    lvDetailsListViews.Items.Clear();
                    cmd.Fill(dt_tax);
                    if (dt_tax.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_tax.Rows.Count; i++)
                        {
                            if (pnl_SerialNo.Visible == false)
                            {
                                lvDetailsListViews.Visible = true;//change
                                panel5.Visible = true;
                            }

                            lvDetailsListViews.Items.Add(dt_tax.Rows[i]["Tax_Name"].ToString());
                            if (txtTaxType.Text.Trim() == "")
                            {
                                lvDetailsListViews.SetSelected(0, true);
                            }
                            else
                            {
                                if (txtTaxType.Text.Trim() == dt_tax.Rows[i]["Tax_Name"].ToString())
                                {
                                    lvDetailsListViews.SetSelected(i, true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtstopatqty_Enter(object sender, EventArgs e)
        {
            panel5.Visible = false;

            lvDetailsListViews.Visible = false;

            if (txtstopatqty.Focus() == true)
            {
                lvDetailsListViews.Visible = false;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.LightBlue;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
        }
        private void txtstopatRate_Enter(object sender, EventArgs e)
        {
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtstopatRate.Focus() == true)
            {
                lvDetailsListViews.Visible = false;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.LightBlue;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
        }
        private void txtTaxType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (listActionType != "Over" && listActionType != null)
                {
                    if (txtTaxType.Text.Trim() != null && txtTaxType.Text.Trim() != "")
                    {
                        DataTable dt_taxtype = new DataTable();
                        SqlCommand cmd2 = new SqlCommand("select * from Tax_table with (index(IndexTax_table)) where Tax_name Like @TaxName order by Tax_name", con);
                        cmd2.Parameters.AddWithValue("@TaxName", txtTaxType.Text + '%');
                        SqlDataAdapter cmd = new SqlDataAdapter(cmd2);
                        isChk = false;
                        dt_taxtype.Rows.Clear();
                        cmd.Fill(dt_taxtype);
                        if (dt_taxtype.Rows.Count > 0)
                        {
                            isChk = true;
                            string tempstr = dt_taxtype.Rows[0]["Tax_name"].ToString();
                            for (int k = 0; k < lvDetailsListViews.Items.Count; k++)
                            {
                                if (tempstr == lvDetailsListViews.Items[k].ToString())
                                {
                                    lvDetailsListViews.SetSelected(k, true);
                                    txtTaxType.Select();
                                    chk = "1";
                                    txtTaxType.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                                    break;
                                }
                            }
                        }
                        if (isChk == false)
                        {
                            chk = "2";
                            if (txtTaxType.Text != "")
                            {
                                string name = txtTaxType.Text.Remove(txtTaxType.Text.Length - 1);
                                txtTaxType.Text = name.ToString();
                                txtTaxType.Select(txtTaxType.Text.Length, 0);
                            }
                            txtTaxType.KeyPress += new KeyPressEventHandler(txtUnit_KeyPress);
                            chk = "1";
                        }
                        else
                        {
                            chk = "1";
                        }
                        txtName_TextChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        private void txtstopatRate_DoubleClick(object sender, EventArgs e)
        {
            if (txtstopatRate.Text == "Yes")
            {
                txtstopatRate.Text = "No";
            }
            else
            {
                txtstopatRate.Text = "Yes";
            }
        }
        private void txtstopatqty_DoubleClick(object sender, EventArgs e)
        {
            if (txtstopatqty.Text == "Yes")
            {
                txtstopatqty.Text = "No";
            }
            else
            {
                txtstopatqty.Text = "Yes";
            }
        }
        private void txtstopatqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtstopatRate.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (txtstopatqty.Text.Trim() == "Yes")
                {
                    txtstopatqty.Text = "No";
                }
                else
                {
                    txtstopatqty.Text = "Yes";
                }
            }
        }
        private void txtstopatRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPRate.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                if (txtstopatRate.Text.Trim() == "Yes")
                {
                    txtstopatRate.Text = "No";
                }
                else
                {
                    txtstopatRate.Text = "Yes";
                }
            }
        }
        private void txtPRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCost.Focus();
            }
        }
        private void txtPRate_Enter(object sender, EventArgs e)
        {
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            txtCode.BackColor = Color.White;
            txtName.BackColor = Color.White;
            txtPrinterName.BackColor = Color.White;
            txtUnit.BackColor = Color.White;
            txtGroup.BackColor = Color.White;
            txtModel.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            txtStockType.BackColor = Color.White;
            txtNtOpen.BackColor = Color.White;
            txtCost.BackColor = Color.White;
            txtPrice.BackColor = Color.White;
            txtSpecial_1.BackColor = Color.White;
            txtSpecial_2.BackColor = Color.White;
            txtSpecial_3.BackColor = Color.White;
            txtMinistck.BackColor = Color.White;
            txt_Maxstck.BackColor = Color.White;
            txtReorder.BackColor = Color.White;
            txtTaxType.BackColor = Color.White;
            txtstopatqty.BackColor = Color.White;
            txtstopatRate.BackColor = Color.White;
            txtPRate.BackColor = Color.LightBlue;
            txtActive.BackColor = Color.White;
            txtopneItem.BackColor = Color.White;
            txtitem_possition.BackColor = Color.White;
            if (txtPRate.Text == "0.00")
            {
                txtPRate.Text = "";
            }
        }
        private void txtPRate_Leave(object sender, EventArgs e)
        {
            if (txtPRate.Text == "")
            {
                txtPRate.Text = "0.00";
            }
            else
            {
                double price1 = 0.00;
                price1 = Convert.ToDouble(txtPRate.Text);
                txtPRate.Text = price1.ToString("0.00");
            }
        }
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (txtPrice.Text == "")
            {
                txtPrice.Text = "0.00";
            }
            else
            {
                double price = 0.00;
                price = Convert.ToDouble(txtPrice.Text);
                txtPrice.Text = price.ToString("0.00");
            }
        }
        string barcodechk = "";
        private void myDataGrid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (myDataGrid1.CurrentCell.ColumnIndex == 2)
            try
            {
                barcodechk = "";
                {
                    if (myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Column"].Value != null)
                    {
                        barcodechk = "1";
                        string t1 = myDataGrid1.Rows[myDataGrid1.CurrentRow.Index].Cells["Column"].Value.ToString();
                        int t2 = myDataGrid1.CurrentRow.Index;
                        for (int j = 0; j < myDataGrid1.Rows.Count - 1; j++)
                        {
                            if (t1 == txtCode.Text || t1 == txtbarcode.Text.Trim() || t1 == txtBarcode1.Text.Trim() || t1 == txtBarcode2.Text.Trim())
                            {
                                MessageBox.Show("This Item Already Exits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                int count = myDataGrid1.Rows.Count;
                                myDataGrid1.Rows.Remove(myDataGrid1.CurrentRow);
                                break;
                            }
                            if (t2 != j)
                            {
                                if (t1.ToLower() == myDataGrid1.Rows[j].Cells["Column"].Value.ToString().ToLower() || t1 == txtCode.Text || t1 == txtbarcode.Text.Trim() || t1 == txtBarcode1.Text.Trim() || t1 == txtBarcode2.Text.Trim())
                                {
                                    MessageBox.Show("This Item Already Exits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    int count = myDataGrid1.Rows.Count;
                                    myDataGrid1.Rows.Remove(myDataGrid1.CurrentRow);
                                    break;
                                }
                                else
                                {
                                    barcodechk = "2";
                                    DataTable dt_barcode = new DataTable();
                                    //SqlCommand cmd_barcodechk = new SqlCommand("select * from BarCode_table where BarCode=@Barcodeentry", con);
                                    SqlCommand cmd_barcodechk = new SqlCommand("SELECT (Select Count(*) From Barcode_table where Barcode=@Barcodeentry) As Barcode ,(select Count(*) From Item_table where item_code=@Barcodeentry) As ItemCode", con);
                                    cmd_barcodechk.Parameters.AddWithValue("@Barcodeentry", t1.ToString());
                                    SqlDataAdapter adp = new SqlDataAdapter(cmd_barcodechk);
                                    dt_barcode.Rows.Clear();
                                    adp.Fill(dt_barcode);
                                    if (dt_barcode.Rows.Count > 0 && (dt_barcode.Rows[0]["Barcode"].ToString() != "0" || dt_barcode.Rows[0]["ItemCode"].ToString() != "0"))
                                    {
                                        MessageBox.Show("This Item Already Exits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        int count = myDataGrid1.Rows.Count;
                                        myDataGrid1.Rows.Remove(myDataGrid1.CurrentRow);
                                        goto End;

                                    }
                                }
                            }

                            if (barcodechk == "1")
                            {
                                DataTable dt_barcode = new DataTable();
                                // SqlCommand cmd_barcodechk = new SqlCommand("select * from BarCode_table where BarCode=@Barcodeentry", con);
                                SqlCommand cmd_barcodechk = new SqlCommand("SELECT (Select Count(*) From Barcode_table where Barcode=@Barcodeentry) As Barcode ,(select Count(*) From Item_table where item_code=@Barcodeentry) As ItemCode", con);
                                cmd_barcodechk.Parameters.AddWithValue("@Barcodeentry", t1.ToString());
                                SqlDataAdapter adp = new SqlDataAdapter(cmd_barcodechk);
                                dt_barcode.Rows.Clear();
                                adp.Fill(dt_barcode);
                                if (dt_barcode.Rows.Count > 0 && (dt_barcode.Rows[0]["Barcode"].ToString() != "0" || Convert.ToString(dt_barcode.Rows[0]["ItemCode"].ToString()) != "0"))
                                {
                                    MessageBox.Show("This Item Already Exits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    int count = myDataGrid1.Rows.Count;
                                    myDataGrid1.Rows.Remove(myDataGrid1.CurrentRow);
                                    goto End;

                                }
                            }

                        }
                    }
                End:
                    int oo = 0;
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        string txtbarcodeEntry = "", codevalues = "1";

        public void grid_dublicate_()
        {
            try
            {
                if (myDataGrid1.Rows.Count > 0)
                {
                    int counts = myDataGrid1.Rows.Count;
                    for (int i = 0; i < counts; i++)
                    {
                        codevalues = "1";
                        if (myDataGrid1.Rows[i].Cells["Column"].Value != null)
                        {
                            if (txtbarcodeEntry == myDataGrid1.Rows[i].Cells["Column"].Value.ToString())
                            {
                                MessageBox.Show("This Item Already Exits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                codevalues = "2";
                                break;
                            }
                            else
                            {
                                dbchekdublicate();
                                if (codevalues == "2")
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            dbchekdublicate();
                        }
                    }
                }
                else
                {
                    dbchekdublicate();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }
        public void dbchekdublicate()
        {
            try
            {
                if (txtbarcodeEntry != "")
                {
                    codevalues = "";
                    DataTable dt_barcode = new DataTable();
                    SqlCommand cmd_chkprocess = new SqlCommand("select * from BarCode_table where BarCode=@BarcodeEntry", con);
                    cmd_chkprocess.Parameters.AddWithValue("@BarcodeEntry", txtbarcodeEntry.ToString());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd_chkprocess);
                    dt_barcode.Rows.Clear();
                    adp.Fill(dt_barcode);
                    if (dt_barcode.Rows.Count > 0)
                    {
                        MessageBox.Show("This Item Already Exits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        codevalues = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }


        private void txtStockType_Enter(object sender, EventArgs e)
        {
            if (txtStockType.Focus() == true)
            {
                //btn_save.BackColor = Color.LightSkyBlue;
                //btn_exit.BackColor = Color.LightSkyBlue;
                //btnAdditional_info.BackColor = Color.LightSkyBlue;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.LightBlue;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
        }
        private void txtActive_Enter(object sender, EventArgs e)
        {
            try
            {
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.LightBlue;
                txtopneItem.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MyMessageBox.ShowBox(ex.ToString(), "Warning");
            }
        }

        private void txtPRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNtOpen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtopneItem_Enter(object sender, EventArgs e)
        {
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtopneItem.Focus() == true)
            {
                lvDetailsListViews.Visible = false;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtopneItem.BackColor = Color.LightBlue;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtitem_possition.BackColor = Color.White;

            }
        }

        private void txtopneItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtopneItem.Text.Trim() == "Yes")
                {
                    txtopneItem.Text = "No";
                }
                else
                {
                    txtopneItem.Text = "Yes";
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtstopatqty.Focus();
            }

        }

        private void txtitem_possition_Enter(object sender, EventArgs e)
        {
            panel5.Visible = false;
            lvDetailsListViews.Visible = false;
            if (txtitem_possition.Focus() == true)
            {
                lvDetailsListViews.Visible = false;
                txtCode.BackColor = Color.White;
                txtName.BackColor = Color.White;
                txtPrinterName.BackColor = Color.White;
                txtUnit.BackColor = Color.White;
                txtGroup.BackColor = Color.White;
                txtModel.BackColor = Color.White;
                txtBrand.BackColor = Color.White;
                txtStockType.BackColor = Color.White;
                txtNtOpen.BackColor = Color.White;
                txtCost.BackColor = Color.White;
                txtPrice.BackColor = Color.White;
                txtSpecial_1.BackColor = Color.White;
                txtSpecial_2.BackColor = Color.White;
                txtSpecial_3.BackColor = Color.White;
                txtMinistck.BackColor = Color.White;
                txt_Maxstck.BackColor = Color.White;
                txtReorder.BackColor = Color.White;
                txtTaxType.BackColor = Color.White;
                txtstopatqty.BackColor = Color.White;
                txtopneItem.BackColor = Color.White;
                txtstopatRate.BackColor = Color.White;
                txtPRate.BackColor = Color.White;
                txtActive.BackColor = Color.White;
                txtitem_possition.BackColor = Color.LightBlue;
            }
        }

        private void txtitem_possition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // btn_save.BackColor = Color.Coral;
                btn_save.Focus();
            }
        }
        private void txtitem_possition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (txtActive.Text.Trim() == "ACTIVE")
                {
                    txtActive.Text = "INACTIVE";
                }
                else
                {
                    txtActive.Text = "ACTIVE";
                }
            }
        }

        private void deleteRowValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Visible = true;
            if (myDataGrid1.Rows.Count > 1)
            {
                string M1 = (MyMessageBox.ShowBox("Are You Sure Want To Delete", "Warning"));
                {
                    if (M1 == "1")
                    {
                        int i = 0;
                        if (myDataGrid1.Rows.Count > 1)
                        {
                            i = Convert.ToInt16(myDataGrid1.CurrentCell.RowIndex);
                            myDataGrid1.Rows.RemoveAt(i);
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        private void txtBarcode1_Enter(object sender, EventArgs e)
        {
            if (txtBarcode1.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.LightBlue;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txtbarcode_entry.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }

        private void txtBarcode2_Enter(object sender, EventArgs e)
        {
            if (txtBarcode2.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.LightBlue;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txtbarcode_entry.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }

        private void txtQty1_Enter(object sender, EventArgs e)
        {
            if (txtQty1.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.LightBlue;
                txtQty2.BackColor = Color.White;
                txtbarcode_entry.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }

        private void txtQty2_Enter(object sender, EventArgs e)
        {
            if (txtQty2.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.LightBlue;
                txtbarcode_entry.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }
        private void txtRate1_Enter(object sender, EventArgs e)
        {
            if (txtRate1.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.LightBlue;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }

        private void txtRate2_Enter(object sender, EventArgs e)
        {
            if (txtRate2.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.LightBlue;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txt_remarks.BackColor = Color.White;
            }
        }

        private void txt_remarks_Enter(object sender, EventArgs e)
        {
            if (txt_remarks.Focus() == true)
            {
                txtbarcode.BackColor = Color.White;
                txtBarcode1.BackColor = Color.White;
                txtBarcode2.BackColor = Color.White;
                txtRate.BackColor = Color.White;
                txtRate1.BackColor = Color.White;
                txtRate2.BackColor = Color.White;
                txtQty.BackColor = Color.White;
                txtQty1.BackColor = Color.White;
                txtQty2.BackColor = Color.White;
                txt_remarks.BackColor = Color.LightBlue;
            }
        }

        private void txtStockType_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "Normal")
            {
                txt.Text = "Serial";
                panel5.Visible = false;
                lvDetailsListViews.Visible = false;
                pnl_SerialNo.Visible = true;
                myDataGridopstock.AllowUserToAddRows = false;
                this.myDataGridopstock.DefaultCellStyle.ForeColor = Color.Black;

                if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                {
                    openingstock = Convert.ToInt32(txtNtOpen.Text);
                }

                aloopstart = aloopend;
                if (aloopstart != 0)
                {
                    aloopstart = aloopend;

                    if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                    {
                        openingstock = Convert.ToInt32(txtNtOpen.Text);
                        aloopend = openingstock;
                    }


                    if (aloopend < aloopstart)
                    {
                        aloopstart = 0;
                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                        int mydatagridopstockrowscount = myDataGridopstock.Rows.Count;
                        for (int p = mydatagridopstockrowscount - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGridopstock.Rows.RemoveAt(p - 1);
                        }
                    }
                }
                else
                {
                    if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                    {
                        openingstock = Convert.ToInt32(txtNtOpen.Text);
                        aloopend = openingstock;
                    }
                }


                for (int Z = aloopstart; Z < aloopend; Z++)
                {
                    myDataGridopstock.Rows.Add();
                    myDataGridopstock.Rows[Z].Cells[1].Value = txtCode.Text;
                }

                DataTable datatableserial = new DataTable();
                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + txtCode.Text.ToString() + "' and pur_sal_ref_no = 0 ", con);
                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                datatableserial.Rows.Clear();
                adpumas.Fill(datatableserial);

                if (aloopend >= datatableserial.Rows.Count)
                {
                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                    {
                        myDataGridopstock.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                    }
                }
            }
            else
            {
                if (txt.Text == "Serial")
                {
                    txt.Text = "Normal";
                    pnl_SerialNo.Visible = false;
                    int mydatagridopstockrowscount = myDataGridopstock.Rows.Count;
                    for (int p = mydatagridopstockrowscount - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                    {
                        myDataGridopstock.Rows.RemoveAt(p - 1);
                    }
                    aloopstart = 0;
                    aloopend = 0;
                }

            }
        }
        string strSerialNo = "";

        private void btnOk_Click(object sender, EventArgs e)
        {
            for (int f = 0; f < myDataGridopstock.Rows.Count - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); f++)
            {
                if ((String)myDataGridopstock.Rows[f].Cells["SerialNoopstock"].Value == null)
                {
                    MessageBox.Show(" cell is empty");
                    return;
                }
            }

            txtTaxType.Focus();
            pnl_SerialNo.Visible = false;

        }

        private void myDataGridopstock_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (DgPurchase.Rows[DgPurchase.CurrentCell.RowIndex].Cells["ItemNames"].Value != null)

        }
        string t1 = "";
        private void myDataGridopstock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (myDataGridopstock.Rows[myDataGridopstock.CurrentCell.RowIndex].Cells["SerialNoopstock"].Value != null)
            {
                t1 = myDataGridopstock.Rows[myDataGridopstock.CurrentRow.Index].Cells["SerialNoopstock"].Value.ToString();
                int t2 = myDataGridopstock.CurrentRow.Index;

                for (int j = 0; j < myDataGridopstock.Rows.Count; j++)
                {
                    if (t2 != j)
                    {
                        if (myDataGridopstock.Rows[j].Cells["SerialNoopstock"].Value != null)
                        {
                            if (t1.ToLower() == myDataGridopstock.Rows[j].Cells["SerialNoopstock"].Value.ToString().ToLower())
                            {
                                myDataGridopstock.Rows[myDataGridopstock.CurrentCell.RowIndex].Cells["SerialNoopstock"].Value = "";
                                MyMessageBox1.ShowBox("Serial No /IMEI No is already Entered", "Warning");
                                // int nextindex = Math.Min(this.DgPurchase.Columns.Count - 1, this.DgPurchase.CurrentCell.ColumnIndex);
                                // SetColumnIndex method = new SetColumnIndex(Mymethod);
                                // this.DgPurchase.BeginInvoke(method, 5);

                                break;
                            }

                        }
                    }
                }

                dbcheckforserial();

            }


        }

        private void txtNtOpen_TextChanged(object sender, EventArgs e)
        {

            if (txtStockType.Text == "Serial")
            {
                //txt.Text = "Serial";
                pnl_SerialNo.Visible = true;
                myDataGridopstock.AllowUserToAddRows = false;
                this.myDataGridopstock.DefaultCellStyle.ForeColor = Color.Black;

                if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                {
                    openingstock = Convert.ToInt32(txtNtOpen.Text);
                }
                else
                {
                    pnl_SerialNo.Visible = false;
                    aloopstart = 0;
                    aloopend = 0;
                    int mydatagridopstockrowscount = myDataGridopstock.Rows.Count;
                    for (int p = mydatagridopstockrowscount - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                    {
                        myDataGridopstock.Rows.RemoveAt(p - 1);
                    }
                    return;
                }


                aloopstart = aloopend;

                if (aloopstart != 0)
                {
                    aloopstart = aloopend;

                    if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                    {
                        openingstock = Convert.ToInt32(txtNtOpen.Text);
                        aloopend = openingstock;
                    }


                    if (aloopend < aloopstart)
                    {
                        aloopstart = 0;
                        //int mydatagrid1rowscount = myDataGrid1.Rows.Count;
                        int mydatagridopstockrowscount = myDataGridopstock.Rows.Count;
                        for (int p = mydatagridopstockrowscount - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                        {
                            myDataGridopstock.Rows.RemoveAt(p - 1);
                        }
                    }
                }
                else
                {
                    if (txtNtOpen.Text != "0" && !string.IsNullOrEmpty(txtNtOpen.Text.Trim()))
                    {
                        openingstock = Convert.ToInt32(txtNtOpen.Text);
                        aloopend = openingstock;
                    }
                }


                for (int Z = aloopstart; Z < aloopend; Z++)
                {
                    myDataGridopstock.Rows.Add();
                    myDataGridopstock.Rows[Z].Cells[1].Value = txtCode.Text;
                }

                DataTable datatableserial = new DataTable();
                SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl where inout = 1 and  barcodeno='" + txtCode.Text.ToString() + "' and pur_sal_ref_no = 0 ", con);
                SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
                datatableserial.Rows.Clear();
                adpumas.Fill(datatableserial);

                if (aloopend >= datatableserial.Rows.Count)
                {
                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                    {
                        myDataGridopstock.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                    }
                }
            }


            else
            {
                txtStockType.Text = "Normal";
                pnl_SerialNo.Visible = false;
                int mydatagridopstockrowscount = myDataGridopstock.Rows.Count;
                for (int p = mydatagridopstockrowscount - (myDataGridopstock.AllowUserToAddRows ? 1 : 0); p > 0; p--)
                {
                    myDataGridopstock.Rows.RemoveAt(p - 1);
                }
            }
        }

        private void txtNtOpen_DoubleClick(object sender, EventArgs e)
        {
            DataTable datatableserial = new DataTable();
            SqlCommand cmdserial = new SqlCommand("select item_no from serialno_transtbl  where inout = 1 and  barcodeno='" + txtCode.Text + "' and pur_sal_ref_no = 0 order by item_no ", con);
            SqlDataAdapter adpumas = new SqlDataAdapter(cmdserial);
            datatableserial.Rows.Clear();
            adpumas.Fill(datatableserial);
            aloopend = Convert.ToInt32(txtNtOpen.Text);
            this.myDataGridopstock.DefaultCellStyle.ForeColor = Color.Black;
            if (datatableserial.Rows.Count > 0)
            {
                myDataGridopstock.AllowUserToAddRows = false;

                if (myDataGridopstock.Rows.Count != aloopend)
                {
                    for (int i = 0; i < datatableserial.Rows.Count; i++)
                    {
                        myDataGridopstock.Rows.Add();
                        myDataGridopstock.Rows[i].Cells[0].Value = datatableserial.Rows[i]["item_no"].ToString();
                        myDataGridopstock.Rows[i].Cells[1].Value = txtCode.Text;
                    }
                }
            }
            // Ending serial number selection from database
            if (myDataGridopstock.Rows.Count > 0)
            {
                pnl_SerialNo.Visible = true;
                myDataGridopstock.Visible = true;
            }

        }

        private void cbocategory_TextChanged(object sender, EventArgs e)
        {
            txtGroup.Text = cbocategory.Text.ToString();
            // loop beginning
            if (txtGroup.Text != "")
            {
                SqlCommand cmd = new SqlCommand(" select * from Item_GroupTable where Item_groupname=@tName", con);
                cmd.Parameters.AddWithValue("@tName", txtGroup.Text);
                con.Close();
                con.Open();
                dt.Rows.Clear();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                string mnumberprefix;
                string mcodenumber;
                mnumberprefix = dt.Rows[0]["numberprefix"].ToString();
                mcodenumber = dt.Rows[0]["startingnumber"].ToString();
                if (mnumberprefix != "NULL" && mnumberprefix != "")
                {
                    txtCode.Text = mnumberprefix + mcodenumber;
                }
                else
                {

                }
            }
            //loop ending
        }
    }
}
