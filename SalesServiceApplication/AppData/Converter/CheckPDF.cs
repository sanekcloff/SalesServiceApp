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
            document.LastSection.AddParagraph($"Информация о приобретении программного продукта".ToUpper(), "Heading1");


            var paragrap1 = document.LastSection.AddParagraph($"Информация о заказе:", "Heading2");
            paragrap1.Format.Alignment = ParagraphAlignment.Left;
            paragrap1.Format.SpaceBefore = "1.5cm";
            document.LastSection.AddParagraph($"Организация: {productOrder.Client.Organization}", "Heading3");
            document.LastSection.AddParagraph($"ФИО: {productOrder.Client.FullName}", "Heading3");
            document.LastSection.AddParagraph($"Номер заказа: {productOrder.Id}", "Heading3");
            document.LastSection.AddParagraph($"Дата заказа: {productOrder.DateOfAdd}", "Heading3");
            document.LastSection.AddParagraph($"Дата завершение: {productOrder.DateOfComplete}", "Heading3");

            var paragraph2 = document.LastSection.AddParagraph($"Информация об организации:", "Heading2");
            paragraph2.Format.SpaceBefore = "1.5cm";
            paragraph2.Format.Alignment = ParagraphAlignment.Left;
            document.LastSection.AddParagraph($"ИНН: 6143070536б", "Heading3");
            document.LastSection.AddParagraph($"КПП: 614301001б", "Heading3");
            document.LastSection.AddParagraph($"ОГРН: 1086174000109", "Heading3");
            document.LastSection.AddParagraph($"Юридический адрес: г. Волгодонск, 347375, Ростовская Область, г. Волгодонск, пр-кт Курчатова, д. 45.", "Heading3");
            document.LastSection.AddParagraph($"Заказ выполнил: {productOrder.Employee.FullName}", "Heading3");

            var paragraph3 = document.LastSection.AddParagraph($"Программный продукт:", "Heading2");
            paragraph3.Format.SpaceBefore = "1.5cm";
            paragraph3.Format.Alignment = ParagraphAlignment.Left;
            document.LastSection.AddParagraph($"Наименомвание: {productOrder.Product.Title}", "Heading3");
            document.LastSection.AddParagraph($"Стоимость: {productOrder.PaymentAmount} руб.", "Heading3");

        }
        public static void ServiceOrderCreate(Document document, ServiceOrder serviceOrder)
        {
            document.LastSection.AddParagraph($"Информация о приобретении услуги".ToUpper(), "Heading1");


            var paragrap1 = document.LastSection.AddParagraph($"Информация о заказе:", "Heading2");
            paragrap1.Format.Alignment = ParagraphAlignment.Left;
            paragrap1.Format.SpaceBefore = "1.5cm";
            document.LastSection.AddParagraph($"Организация: {serviceOrder.Client.Organization}", "Heading3");
            document.LastSection.AddParagraph($"ФИО: {serviceOrder.Client.FullName}", "Heading3");
            document.LastSection.AddParagraph($"Номер заказа: {serviceOrder.Id}", "Heading3");
            document.LastSection.AddParagraph($"Дата заказа: {serviceOrder.DateOfAdd}", "Heading3");
            document.LastSection.AddParagraph($"Дата завершение: {serviceOrder.DateOfComplete}", "Heading3");

            var paragraph2 = document.LastSection.AddParagraph($"Информация об организации:", "Heading2");
            paragraph2.Format.SpaceBefore = "1.5cm";
            paragraph2.Format.Alignment = ParagraphAlignment.Left;
            document.LastSection.AddParagraph($"ИНН: 6143070536б", "Heading3");
            document.LastSection.AddParagraph($"КПП: 614301001б", "Heading3");
            document.LastSection.AddParagraph($"ОГРН: 1086174000109", "Heading3");
            document.LastSection.AddParagraph($"Юридический адрес: г. Волгодонск, 347375, Ростовская Область, г. Волгодонск, пр-кт Курчатова, д. 45.", "Heading3");
            document.LastSection.AddParagraph($"Заказ выполнил: {serviceOrder.Employee.FullName}", "Heading3");

            var paragraph3 = document.LastSection.AddParagraph($"Услуга:", "Heading2");
            paragraph3.Format.SpaceBefore = "1.5cm";
            paragraph3.Format.Alignment = ParagraphAlignment.Left;
            document.LastSection.AddParagraph($"Наименомвание: {serviceOrder.Service.Title}", "Heading3");
            document.LastSection.AddParagraph($"Стоимость: {serviceOrder.PaymentAmount} руб.", "Heading3");

        }
    }
}
