using AppData.Entities;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppData.Converter
{
    public static class PDF
    {
        static PDF()
        {
            if (PdfSharp.Capabilities.Build.IsCoreBuild)
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();
        }
        public static void CreateProductOrderCheck(ProductOrder productOrder)
        {
            // Create a MigraDoc document.
            var document = Documents.CreateDocument(productOrder);

            // Write MigraDoc DDL into a string.
            var ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);

            // Write MigraDoc DDL to a file.
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

            var renderer = new PdfDocumentRenderer()
            {
                Document = document
            };

            renderer.RenderDocument();

            var filename = "PdfFile.pdf";
            // Save the document...
            renderer.PdfDocument.Save(filename);

            // ...and start a viewer.
            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        }
    }
}
