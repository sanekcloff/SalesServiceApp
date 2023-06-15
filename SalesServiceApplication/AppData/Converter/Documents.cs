using AppData.Entities;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppData.Converter
{
    class Documents
    {
        public static Document CreateDocument(ProductOrder productOrder)
        {
            Document document;
            // Create a new MigraDoc document.
            document = new Document
            {
                Info = { Title = $"Счёт за покупку {productOrder.Product.Title}." }
            };

            Styles.DefineStyles(document);

            CheckPDF.ProductOrderCreate(document, productOrder);

            return document;
        }
        public static Document CreateDocument(ServiceOrder serviceOrder)
        {
            Document document;
            // Create a new MigraDoc document.
            document = new Document
            {
                Info = { Title = $"Счёт за покупку {serviceOrder.Service.Title}." }
            };

            Styles.DefineStyles(document);

            CheckPDF.ServiceOrderCreate(document, serviceOrder);

            return document;
        }
    }
}
