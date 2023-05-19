using AppData.Entities;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Snippets.Font;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AppData.Converter
{
    public static class PdfConverter
    {
        static PdfConverter()
        {
            GlobalFontSettings.FontResolver = new FailsafeFontResolver();
        }
        public static void CretePdfFile(string folderPath, ProductOrder productOrder)
        {          
            var document = new PdfDocument();

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            var width = page.Width; //595
            var height = page.Height; //842

            //DrawImage(gfx, "A:\\C# Projects\\SalesServiceApp\\SalesServiceApplication\\AppData\\Storage\\Pictures\\ic_Title.png", 5, 10, 470, 60);
            //DrawImage(gfx, "A:\\C# Projects\\SalesServiceApp\\SalesServiceApplication\\AppData\\Storage\\Pictures\\ic_qrcode.png", 550, 10, 50, 50);
            
            var font1 = new XFont("Cascadia Code", 28, XFontStyleEx.Bold);
            var font2 = new XFont("Cascadia Code", 22, XFontStyleEx.Regular);

            folderPath += $"ProdctInvoice{productOrder.Id}.pdf";

            gfx.DrawString("СЧЁТ ЗА ПРИОБРЕТЕНИЕ ПРОДУКТА", font1, XBrushes.Black,
                new XRect(0, -100, page.Width, page.Height), XStringFormats.Center);

            gfx.DrawRectangle(XPens.Black, new XRect(width/12, height / 2 +100 , width-(width/12*2), 200));

            document.Save(folderPath);
            Process.Start(new ProcessStartInfo(folderPath) { UseShellExecute = true });
        }
        private static void DrawImage(XGraphics gfx, string picturePath, int x, int y, int width, int height)
        {
            XImage imageMu = XImage.FromFile(picturePath);
            gfx.DrawImage(imageMu, x, y, width, height);
        }
    }
}
