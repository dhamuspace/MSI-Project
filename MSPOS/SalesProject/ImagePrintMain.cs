using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace SalesProject
{
     
    public class ImagePrintMain
    {
        //private const string PrinterName = @"Generic / Text Only";
        private class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
         private static string PrinterName = _Class.clsVariables.tPrinterName;
         private static string ImageLocation;
         public static void funImagePrintMain()
         {

             string tImagePath = System.Windows.Forms.Application.StartupPath + "\\Logo";

             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POS"].ConnectionString.ToString());

             if (!Directory.Exists(tImagePath))
             {
                 Directory.CreateDirectory(tImagePath);
                 ImageLocation = "";
             }
             else
             {
                 if (File.Exists(tImagePath + "\\logo.png"))
                 {
                     ImageLocation = tImagePath + "\\logo.png";
                 }
                 else if (File.Exists(tImagePath + "\\logo.jpg"))
                 {
                     ImageLocation = tImagePath + "\\logo.jpg";
                 }
                 else if (File.Exists(tImagePath + "\\logo.jpeg"))
                 {
                     ImageLocation = tImagePath + "\\logo.jpeg";
                 }
                 else if (File.Exists(tImagePath + "\\logo.gif"))
                 {
                     ImageLocation = tImagePath + "\\logo.gif";
                 }
                 else if (File.Exists(tImagePath + "\\logo.bmp"))
                 {
                     ImageLocation = tImagePath + "\\logo.bmp";
                 }
                 else
                 {
                     ImageLocation = "";
                 }
             }
             if (ImageLocation != "")
             {
                 Print(PrinterName, GetDocument());
             }
         }

        private static BitmapData GetBitmapData(string bmpFileName)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                var dimensions = bitmap.Width * bitmap.Height;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < bitmap.Height; y++)
                {
                    for (var x = 0; x < bitmap.Width; x++)
                    {
                        var color = bitmap.GetPixel(x, y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                    {
                        Dots = dots,
                        Height = bitmap.Height,
                        Width = bitmap.Width
                    };
            }
        }

        private static byte[] GetDocument()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                // Reset the printer bws (NV images are not cleared)
                bw.Write(AsciiControlChars.Escape);
                bw.Write('@');
                // Render the logo
                RenderLogo(bw);
                // Feed 3 vertical motion units and cut the paper with a 1 point cut
                //bw.Write(AsciiControlChars.GroupSeparator);
                //bw.Write('V');
                //bw.Write((byte)66);
                //bw.Write((byte)3);
                bw.Flush();
                return ms.ToArray();
            }
        }

        private static void RenderLogo(BinaryWriter bw)
        {
            //var data = GetBitmapData("123.png");
            var data = GetBitmapData(ImageLocation);
            var dots = data.Dots;
            var width = BitConverter.GetBytes(data.Width);

            //bw.Write(AsciiControlChars.Newline);
            //bw.Write("CASE 1");
            //bw.Write(AsciiControlChars.Newline);

            //bw.Write(AsciiControlChars.Escape);
            //bw.Write('*');         // bit-image mode
            //bw.Write((byte)0);     // 8-dot single-density
            //bw.Write((byte)5);     // width low byte
            //bw.Write((byte)0);     // width high byte
            //bw.Write((byte)128);
            //bw.Write((byte)64);
            //bw.Write((byte)32);
            //bw.Write((byte)16);
            //bw.Write((byte)8);

            //bw.Write(AsciiControlChars.Newline);
            //bw.Write("CASE 2");
            //bw.Write(AsciiControlChars.Newline);

            //bw.Write(AsciiControlChars.Escape);
            //bw.Write('*');         // bit-image mode
            //bw.Write((byte)0);     // 8-dot single-density
            //bw.Write((byte)5);     // width low byte
            //bw.Write((byte)0);     // width high byte
            //bw.Write((byte)1);
            //bw.Write((byte)2);
            //bw.Write((byte)4);
            //bw.Write((byte)8);
            //bw.Write((byte)16);

            //bw.Write(AsciiControlChars.Newline);

            
            //bw.Write(AsciiControlChars.Newline);
            //bw.Write("CASE 3");
            //bw.Write(AsciiControlChars.Newline);

            // So we have our bitmap data sitting in a bit array called "dots."
            // This is one long array of 1s (black) and 0s (white) pixels arranged
            // as if we had scanned the bitmap from top to bottom, left to right.
            // The printer wants to see these arranged in bytes stacked three high.
            // So, essentially, we need to read 24 bits for x = 0, generate those
            // bytes, and send them to the printer, then keep increasing x. If our
            // image is more than 24 dots high, we have to send a second bit image
            // command.

            // Set the line spacing to 24 dots, the height of each "stripe" of the
            // image that we're drawing.
            bw.Write(AsciiControlChars.Escape);
            bw.Write('3');
            bw.Write((byte)24);

            // OK. So, starting from x = 0, read 24 bits down and send that data
            // to the printer.
            int offset = 0;

            while (offset < data.Height)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write('*');         // bit-image mode
                bw.Write((byte)33);    // 24-dot double-density
                bw.Write(width[0]);  // width low byte
                bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;

                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;

                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }

                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }

                        bw.Write(slice);
                    }
                }

                offset += 24;
                bw.Write(AsciiControlChars.Newline);
            }

            // Restore the line spacing to the default of 30 dots.
            bw.Write(AsciiControlChars.Escape);
            bw.Write('3');
            bw.Write((byte)30);
        }

        private static void Print(string printerName, byte[] document)
        {
            NativeMethods.DOC_INFO_1 documentInfo;
            IntPtr printerHandle;

            documentInfo = new NativeMethods.DOC_INFO_1();
            documentInfo.pDataType = "RAW";
            documentInfo.pDocName = "Bit Image Test";

            printerHandle = new IntPtr(0);

            if (NativeMethods.OpenPrinter(printerName.Normalize(), out printerHandle, IntPtr.Zero))
            {
                if (NativeMethods.StartDocPrinter(printerHandle, 1, documentInfo))
                {
                    int bytesWritten;
                    byte[] managedData;
                    IntPtr unmanagedData;

                    managedData = document;
                    unmanagedData = Marshal.AllocCoTaskMem(managedData.Length);
                    Marshal.Copy(managedData, 0, unmanagedData, managedData.Length);

                    if (NativeMethods.StartPagePrinter(printerHandle))
                    {
                        NativeMethods.WritePrinter(
                            printerHandle,
                            unmanagedData,
                            managedData.Length,
                            out bytesWritten);
                        NativeMethods.EndPagePrinter(printerHandle);
                    }
                    else
                    {
                        throw new Win32Exception();
                    }

                    Marshal.FreeCoTaskMem(unmanagedData);

                    NativeMethods.EndDocPrinter(printerHandle);
                }
                else
                {
                    throw new Win32Exception();
                }

                NativeMethods.ClosePrinter(printerHandle);
            }
            else
            {
                throw new Win32Exception();
            }
        }
    }
}
