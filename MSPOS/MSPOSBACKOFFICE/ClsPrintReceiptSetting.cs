using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace MSPOSBACKOFFICE
{
    class ClsPrintReceiptSetting
    {
        public static string tempGEnableThisDevice=string.Empty;
        public static string tempGPrinterName = string.Empty;
        public static string tempGPrinterType = string.Empty;
        public static string tempGPrintCopies = string.Empty;
        public static string tempGCharactersPerLine = string.Empty;
        public static string tempGFontsize = string.Empty;

        public static string tempGFontName = string.Empty;
        public static string tempGRound = string.Empty;
        public static string tempGPrintTime = string.Empty;
        public static string tempGPrintDate = string.Empty;
        public static string tempGPrintQunatityandRate = string.Empty;
        public static string tempGCutPaper = string.Empty;
        public static string tempGAutoPrint = string.Empty;

        public static string tempGPrintBottomLine1 = string.Empty;
        public static string tempGPrintBottomLine2 = string.Empty;
        public static string tempGPrintBottomLine3 = string.Empty;
        public static string tempGPrintBottomLine4 = string.Empty;
        public static string tempGPrintBottomLine5 = string.Empty;
        public static string tempGPrintBottomTime = string.Empty;
        public static string tempGPrintHeader = string.Empty;
        public static string tempGPrintSubtotal = string.Empty;
        public static string tempGPrintTopLine1 = string.Empty;
        public static string tempGPrintTopLine2 = string.Empty;
        public static string tempGPrintTopLine3 = string.Empty;

        public static string tempGPrintTopLine4 = string.Empty;
        public static string tempGPrintTopLine5 = string.Empty;
        public static string tempGPrintLineBelowLogo = string.Empty;
        public static string tempGPrintLineBelowTopText = string.Empty;
        public static string tempGPrintLineBelowHeader = string.Empty;
        public static string tempGPrintlineAboveTotal = string.Empty;
        public static string tempGPrintLineBelowTotal = string.Empty;
        public static string tempGPrintLineAboveBottomText = string.Empty;
        public static string tempGPrintLineBelowBottomText = string.Empty;
        public static string tempGPrintTax = string.Empty;
        public static string tempGDisplayTaxType = string.Empty;
        public static string tempGPrintCounterName = string.Empty;
        public static string tempGPrintUserName = string.Empty;
        public static string tempGPrintBillType = string.Empty;
        public static string tempGPrintLogo = string.Empty;

        public static string tempGReceiptHeaderLeftAlign = string.Empty;
        public static string tempGPrintPaymentMode = string.Empty;
        public static string tempGReceiptNumber = string.Empty;
        public static string tempGDeliveryChargeText = string.Empty;
        public static string tempGAmountTendered = string.Empty;
        public static string tempGBottomLine1 = string.Empty;
        public static string tempGBottomLine2 = string.Empty;
        public static string tempGBottomLine3 = string.Empty;
        public static string tempGBottomLine4 = string.Empty;
        public static string tempGBottomLine5 = string.Empty;
        public static string tempGChangeDue = string.Empty;
        public static string tempGCustomerCopy = string.Empty;
        public static string tempGCustomerInformation = string.Empty;
        public static string tempGItemCount = string.Empty;
        public static string tempGOrderNumber = string.Empty;
        public static string tempGPayThisAmount = string.Empty;
        public static string tempGSubtotal = string.Empty;
        public static string tempGTopLine1 = string.Empty;
        public static string tempGTopLine2 = string.Empty;
        public static string tempGTopLine3 = string.Empty;
        public static string tempGTopLine4 = string.Empty;
        public static string tempGTopLine5 = string.Empty;
        public static string tempGTotalDue = string.Empty;
        public static  SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());
        public static void assignvalues()
        {
            try
            {
                DataTable dtPrint = new DataTable();
                DataTable dtCompany = new DataTable();
                dtCompany.Rows.Clear();

                dtPrint.Columns.Add("Describ", typeof(string));
                dtPrint.Columns.Add("Property", typeof(string));
                dtPrint.Rows.Clear();
                SqlCommand cmd = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tActionType", "GSET");
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtPrint);

                SqlCommand cmd13 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@tActionType", "RPTSET");
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd13);
                adp1.Fill(dtPrint);

                SqlCommand cmd2 = new SqlCommand("sp_SalesCreationSelectAll", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@tActionType", "CUSTOMTEXT");
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                adp2.Fill(dtPrint);

                DataTable dtPrinter = new DataTable();
                dtPrinter.Rows.Clear();
                SqlCommand cmdPrinter = new SqlCommand("Select * from ReceiptPrintSettings_table where Counter=@tCounter", con);
                cmdPrinter.Parameters.AddWithValue("@tCounter", _Class.clsVariables.tCounter);
                SqlDataAdapter adpPrinter = new SqlDataAdapter(cmdPrinter);
                adpPrinter.Fill(dtPrinter);
                if (dtPrint.Rows.Count > 0)
                {
                    tempGEnableThisDevice = dtPrint.Rows[0][0].ToString();
                    tempGPrinterName = dtPrint.Rows[1][0].ToString();
                    tempGPrinterType = dtPrint.Rows[2][0].ToString();
                    tempGPrintCopies = dtPrint.Rows[3][0].ToString();
                    tempGCharactersPerLine = dtPrint.Rows[4][0].ToString();
                    tempGFontsize = dtPrint.Rows[5][0].ToString();

                    tempGFontName = dtPrint.Rows[6][0].ToString();
                    tempGRound = dtPrint.Rows[7][0].ToString();
                    tempGPrintTime = dtPrint.Rows[8][0].ToString();
                    tempGPrintDate = dtPrint.Rows[9][0].ToString();
                    tempGPrintQunatityandRate = dtPrint.Rows[10][0].ToString();
                    tempGCutPaper = dtPrint.Rows[11][0].ToString();
                    tempGAutoPrint = dtPrint.Rows[12][0].ToString();

                    tempGPrintBottomLine1 = dtPrint.Rows[13][0].ToString();
                    tempGPrintBottomLine2 = dtPrint.Rows[14][0].ToString();
                    tempGPrintBottomLine3 = dtPrint.Rows[15][0].ToString();
                    tempGPrintBottomLine4 = dtPrint.Rows[16][0].ToString();

                    tempGPrintBottomLine5 = dtPrint.Rows[17][0].ToString();
                    tempGPrintBottomTime = dtPrint.Rows[18][0].ToString();
                    tempGPrintHeader = dtPrint.Rows[19][0].ToString();
                    tempGPrintSubtotal = dtPrint.Rows[20][0].ToString();
                    tempGPrintTopLine1 = dtPrint.Rows[21][0].ToString();
                    tempGPrintTopLine2 = dtPrint.Rows[22][0].ToString();
                    tempGPrintTopLine3 = dtPrint.Rows[23][0].ToString();

                    tempGPrintTopLine4 = dtPrint.Rows[24][0].ToString();
                    tempGPrintTopLine5 = dtPrint.Rows[25][0].ToString();
                    tempGPrintLineBelowLogo = dtPrint.Rows[26][0].ToString();
                    tempGPrintLineBelowTopText = dtPrint.Rows[27][0].ToString();
                    tempGPrintLineBelowHeader = dtPrint.Rows[28][0].ToString();
                    tempGPrintlineAboveTotal = dtPrint.Rows[29][0].ToString();
                    tempGPrintLineBelowTotal = dtPrint.Rows[30][0].ToString();
                    tempGPrintLineAboveBottomText = dtPrint.Rows[31][0].ToString();
                    tempGPrintLineBelowBottomText = dtPrint.Rows[32][0].ToString();
                    tempGPrintTax = dtPrint.Rows[33][0].ToString();
                    tempGDisplayTaxType = dtPrint.Rows[34][0].ToString();
                    tempGPrintCounterName = dtPrint.Rows[35][0].ToString();
                    tempGPrintUserName = dtPrint.Rows[36][0].ToString();
                    tempGPrintBillType = dtPrint.Rows[37][0].ToString();
                    tempGPrintLogo = dtPrint.Rows[38][0].ToString();

                    tempGReceiptHeaderLeftAlign = dtPrint.Rows[39][0].ToString();
                    tempGPrintPaymentMode = dtPrint.Rows[40][0].ToString();
                    tempGReceiptNumber = dtPrint.Rows[41][0].ToString();
                    tempGDeliveryChargeText = dtPrint.Rows[42][0].ToString();
                    tempGAmountTendered = dtPrint.Rows[43][0].ToString();
                    tempGBottomLine1 = dtPrint.Rows[44][0].ToString();
                    tempGBottomLine2 = dtPrint.Rows[45][0].ToString();
                    tempGBottomLine3 = dtPrint.Rows[46][0].ToString();
                    tempGBottomLine4 = dtPrint.Rows[47][0].ToString();
                    tempGBottomLine5 = dtPrint.Rows[48][0].ToString();
                    tempGChangeDue = dtPrint.Rows[49][0].ToString();
                    tempGCustomerCopy = dtPrint.Rows[50][0].ToString();
                    tempGCustomerInformation = dtPrint.Rows[51][0].ToString();
                    tempGItemCount = dtPrint.Rows[52][0].ToString();
                    tempGOrderNumber = dtPrint.Rows[53][0].ToString();
                    tempGPayThisAmount = dtPrint.Rows[54][0].ToString();
                    tempGSubtotal = dtPrint.Rows[55][0].ToString();
                    tempGTopLine1 = dtPrint.Rows[56][0].ToString();
                    tempGTopLine2 = dtPrint.Rows[57][0].ToString();
                    tempGTopLine3 = dtPrint.Rows[58][0].ToString();
                    tempGTopLine4 = dtPrint.Rows[59][0].ToString();
                    tempGTopLine5 = dtPrint.Rows[60][0].ToString();
                    tempGTotalDue = dtPrint.Rows[61][0].ToString();
                }
            }
            catch(Exception ex)
            {
                MyMessageBox1.ShowBox(ex.ToString(), "Warning");
            }
        }
    }
}
