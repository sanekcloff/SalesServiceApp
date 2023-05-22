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
using Azure;
using PdfSharp.Charting;

namespace AppData.Converter
{
    public static class PdfConverter
    {
        static PdfConverter()
        {
            GlobalFontSettings.FontResolver = new FailsafeFontResolver();
        }
        public static void CretePdfFile(ProductOrder productOrder)
        {
            var document = new PdfDocument();

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            var width = page.Width; //595
            var height = page.Height; //842

            var font1 = new XFont("Cascadia Code", 28, XFontStyleEx.Bold);
            var font2 = new XFont("Cascadia Code", 16, XFontStyleEx.Regular);
            var font3 = new XFont("Cascadia Code", 11, XFontStyleEx.Regular);

            var folderPath = $@"ProdctInvoice.pdf";

            gfx.DrawLine(XPens.Black, width / 12, 20, width - (width / 12), 20);
            gfx.DrawString("СЧЁТ ЗА ПРИОБРЕТЕНИЕ ПРОДУКТА", font1, XBrushes.Black,
                new XRect(0, 30, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawLine(XPens.Black, width / 12, 80, width - (width / 12), 80);

            gfx.DrawString($"ИНФОРМАЦИЯ О ЗАКАЗЕ", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 220, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, width / 12, height / 2 - 200, width - (width / 12), height / 2 - 200);
            gfx.DrawString($"Номер заказа: {productOrder.Id}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 180, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Дата заказа: {productOrder.DateOfAdd.ToString("d")}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 160, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Дата завершения: {productOrder.DateOfComplete.Value.ToString("d")}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 140, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString($"Организация: ИНН 6143070536б КПП 614301001б, г. Волгодонск,", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 100, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"347375, Ростовская Область, г. Волгодонск, пр-кт Курчатова, д. 45", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 80, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Выполнил заказ: {productOrder.Employee.FullName}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 60, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString($"Заказчик: {productOrder.Client.Organization}, {productOrder.Client.FullName}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 20, page.Width, page.Height), XStringFormats.TopLeft);

            var startHeight = height / 2 +20;
            var startWidth = width / 12 + 5;
            var step = 25.0;

            WriteProductTextPart(startWidth, startHeight, step, productOrder, page, font2, gfx);

            gfx.DrawLine(XPens.Black, width / 12, height - 50, 120, height - 50);
            gfx.DrawString($"(подпись)", font3, XBrushes.Black,
                new XRect(width / 12 + 10, height - 50, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawLine(XPens.Black, width / 12 + 105, height - 50, width / 12 + 210, height - 50);
            gfx.DrawString($"(расшифровка)", font3, XBrushes.Black,
                new XRect(width / 12 + 120, height - 50, page.Width, page.Height), XStringFormats.TopLeft);

            document.Save(folderPath);
            Process.Start(new ProcessStartInfo(folderPath) { UseShellExecute = true });
        }

        private static void WriteProductTextPart(double width, double height, double step, 
            ProductOrder productOrder, PdfPage page, XFont font, XGraphics gfx)
        {
            //100
            //89
            //59-60
            var parts = new List<string>();
            var title = productOrder.Product.Title;
            
            
            if (title.Length>59)
            {
                gfx.DrawString($"Программный продукт:", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += step;
                parts.Add(title.Substring(0,59).Trim());
                title = title.Substring(59);
                parts.Add(title.Trim());

                foreach (var titlePart in parts)
                {
                    gfx.DrawString($"{titlePart}", font, XBrushes.Black,
                    new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
                    height += step;

                }
                height += step;
            }
            else
            {
                gfx.DrawString($"Программный продукт: {productOrder.Product.Title}", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += step;
            }

            gfx.DrawString($"Стоимость (без скидки): {productOrder.Product.Cost} руб.", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
            height += step;
            gfx.DrawString($"К оплате: {productOrder.PaymentAmount} руб.", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
        }

        private static void WriteServiceTextPart(double width, double height, double step,
            ServiceOrder serviceOrder, PdfPage page, XFont font, XGraphics gfx)
        {
            //100
            //89
            //59-60
            var parts = new List<string>();
            var title = serviceOrder.Service.Title;


            if (title.Length > 59)
            {
                gfx.DrawString($"Программный продукт:", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += step;
                parts.Add(title.Substring(0, 59).Trim());
                title = title.Substring(59);
                parts.Add(title.Trim());

                foreach (var titlePart in parts)
                {
                    gfx.DrawString($"{titlePart}", font, XBrushes.Black,
                    new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
                    height += step;

                }
                height += step;
            }
            else
            {
                gfx.DrawString($"Программный продукт: {serviceOrder.Service.Title}", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
                height += step;
            }
            if (serviceOrder.Service.CostPerHour!=0)
                gfx.DrawString($"Стоимость за час: {serviceOrder.Service.CostPerHour} руб.", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
            else
                gfx.DrawString($"Стоимость за час: договорная", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);

            height += step;
            gfx.DrawString($"К оплате: {serviceOrder.PaymentAmount} руб.", font, XBrushes.Black,
                new XRect(width, height, page.Width, page.Height), XStringFormats.TopLeft);
        }
        public static void CretePdfFile(ServiceOrder serviceOrder)
        {
            var document = new PdfDocument();

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            var width = page.Width; //595
            var height = page.Height; //842

            var font1 = new XFont("Cascadia Code", 28, XFontStyleEx.Bold);
            var font2 = new XFont("Cascadia Code", 16, XFontStyleEx.Regular);
            var font3 = new XFont("Cascadia Code", 11, XFontStyleEx.Regular);

            var folderPath = $@"ServiceInvoice.pdf";

            gfx.DrawLine(XPens.Black, width / 12, 20, width - (width / 12), 20);
            gfx.DrawString("СЧЁТ ЗА ПРИОБРЕТЕНИЕ УСЛУГИ", font1, XBrushes.Black,
                new XRect(0, 30, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawLine(XPens.Black, width / 12, 80, width - (width / 12), 80);

            gfx.DrawString($"ИНФОРМАЦИЯ О ЗАКАЗЕ", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 220, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, width / 12, height / 2 - 200, width - (width / 12), height / 2 - 200);
            gfx.DrawString($"Номер заказа: {serviceOrder.Id}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 180, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Дата заказа: {serviceOrder.DateOfAdd.ToString("d")}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 160, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Дата завершения: {serviceOrder.DateOfComplete.Value.ToString("d")}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 140, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString($"Организация: ИНН 6143070536б КПП 614301001б, г. Волгодонск,", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 100, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"347375, Ростовская Область, г. Волгодонск, пр-кт Курчатова, д. 45", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 80, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Выполнил заказ: {serviceOrder.Employee.FullName}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 60, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString($"Заказчик: {serviceOrder.Client.Organization}, {serviceOrder.Client.FullName}", font2, XBrushes.Black,
                new XRect(width / 12 + 5, height / 2 - 20, page.Width, page.Height), XStringFormats.TopLeft);

            var startHeight = height / 2 + 20;
            var startWidth = width / 12 + 5;
            var step = 25.0;

            WriteServiceTextPart(startWidth, startHeight, step, serviceOrder, page, font2, gfx);

            gfx.DrawLine(XPens.Black, width / 12, height - 50, 120, height - 50);
            gfx.DrawString($"(подпись)", font3, XBrushes.Black,
                new XRect(width / 12 + 10, height - 50, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawLine(XPens.Black, width / 12 + 105, height - 50, width / 12 + 210, height - 50);
            gfx.DrawString($"(расшифровка)", font3, XBrushes.Black,
                new XRect(width / 12 + 120, height - 50, page.Width, page.Height), XStringFormats.TopLeft);

            document.Save(folderPath);
            Process.Start(new ProcessStartInfo(folderPath) { UseShellExecute = true });
        }
    }
}
