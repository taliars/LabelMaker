using System.Diagnostics;

using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace LabelMaker.Helpers
{
    public class PdfHelper
    {
        private const string DEJAVUSerif = "./src/ttf/DejaVuSerif.ttf";

        public PdfHelper(string path, string company, string order, string[] labelContents)
        {
            // Must have write permissions to the path folder
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.SetFont(CreateFont(DEJAVUSerif));

            foreach (var labelContent in labelContents)
            {
                var table = CreateTable(company, labelContent);
                document.Add(table);
            }
            document.Close();

            var p = new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }


        private PdfFont CreateFont(string font) =>
            PdfFontFactory.CreateFont(font, PdfEncodings.IDENTITY_H);

        private Table CreateTable(string company, string labelContent)
        {
            var cellValues = new string[] {
                company,
                "",
                "Проба №",
                labelContent,
                "Проба",
                "почвы (грунта)"
            };


            Table table = new Table(2, false);

            foreach (var cellValue in cellValues)
            {
                table.AddCell(CreateCell(cellValue));
            }

            table.SetMarginBottom(12.5f);
            table.SetMarginRight(12.5f);
            return table;
        }

        private Cell CreateCell(string content)
        {
            return new Cell(1, 1)
                .SetTextAlignment(TextAlignment.RIGHT)
                .Add(new Paragraph(content));

        }
    }
}
