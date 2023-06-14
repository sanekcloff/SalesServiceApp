using AppData.Entities;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Converter
{
    class CheckPDF
    {
        public static void ProductOrderCreate(Document document, ProductOrder productOrder)
        {
            document.LastSection.AddParagraph($"Счёт за приобретение программного продукта".ToUpper(), "Heading1");

            var paragraph = document.LastSection.AddParagraph($"ИНФОРМАЦИЯ О ЗАКАЗЕ".ToUpper(), "Heading2");
            paragraph.Format.SpaceAfter = "1.5cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            document.LastSection.AddParagraph($"Заказчик: {productOrder.Client.Organization}, {productOrder.Client.FullName}", "Heading3");
            document.LastSection.AddParagraph($"Номер заказа: {productOrder.Id}", "Heading3");
            document.LastSection.AddParagraph($"Дата заказа: {productOrder.DateOfAdd}", "Heading3");
            var paragraph1 = document.LastSection.AddParagraph($"Дата завершение: {productOrder.DateOfComplete}", "Heading3");
            paragraph1.Format.SpaceAfter = "1.5cm";

            document.LastSection.AddParagraph($"Организация: ИНН 6143070536б КПП 614301001б, г. Волгодонск, 347375, Ростовская Область, г. Волгодонск, пр-кт Курчатова, д. 45.", "Heading3");
            var paragraph2 = document.LastSection.AddParagraph($"Выполнил заказ: {productOrder.Employee.FullName}", "Heading3");
            paragraph2.Format.SpaceAfter = "1.5cm";

            var paragrap3 = document.LastSection.AddParagraph($"Программный продукт:", "Heading2");
            paragrap3.Format.Alignment = ParagraphAlignment.Left;
            document.LastSection.AddParagraph($"Наименомвание: {productOrder.Product.Title}", "Heading3");
            document.LastSection.AddParagraph($"Стоимость: {productOrder.PaymentAmount} руб.", "Heading3");
        }
    }
}
