using System.Collections.Generic;
using System.Diagnostics;

using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using LabelMaker.Configuration;

namespace LabelMaker.Helpers
{
    public static class PdfHelper
    {
        private const string DEJAVUSerif = "./src/ttf/DejaVuSerif.ttf";
        private const int DefaultFontSize = 14;
        private const int DefaultRowSpan = 1;
        private const int DefaultColumnSpan = 1;
        private const int DefaultColumnCount = 1;

        public static void CreateDocument(string path,
            AppSettings appSettings,
            string[] labelContents)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var document = CreateDocumentTemplate(path, appSettings?.FontSize ?? DefaultFontSize);
            var rowTableParagraph = new Paragraph();

            foreach (var labelContent in labelContents)
            {
                var table = CreateTable(appSettings.Company, labelContent);
                rowTableParagraph.Add(table);
            }

            document.Add(rowTableParagraph);
            document.Close();

            OpenDocumentWithDefaultViewer(path);
        }

        private static void OpenDocumentWithDefaultViewer(string path)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                }
            };

            p.Start();
        }

        // TODO: Margins to settings
        private static Document CreateDocumentTemplate(string path, int fontSize)
        {
            // Must have write permissions to the path folder
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.SetFont(CreateFont(DEJAVUSerif));
            document.SetFontSize(fontSize);
            document.SetLeftMargin(10f);
            document.SetRightMargin(10f);

            return document;
        }

        private static Table CreateTable(string company, string labelContent)
        {
            var cellDescriptions = new CellDescription[]
            {
                new CellDescription(company, CellPosition.Top),
                new CellDescription($"Проба № {labelContent}", CellPosition.Central),
                new CellDescription("Проба почвы (грунта)", CellPosition.Bottom),
            };

            var table = new Table(DefaultColumnCount, false)
                .SetKeepTogether(true);

            foreach (var cellDescription in cellDescriptions)
            {
                var cell = CreateCell(cellDescription);
                table.AddCell(cell);
            }

            table.SetMarginBottom(2.5f);
            table.SetMarginRight(2.5f);

            return table;
        }

        private static PdfFont CreateFont(string font) =>
            PdfFontFactory.CreateFont(font, PdfEncodings.IDENTITY_H);

        private static Cell CreateCell(CellDescription cellDescription)
        {
            var cell = new Cell(DefaultRowSpan, DefaultColumnSpan)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(cellDescription.Content));

            SetBorder(cell, cellDescription.CellPosition);
            return cell;
        }

        private static Cell SetBorder(Cell cell, CellPosition cellPosition)
        {
            cell.SetBorder(Border.NO_BORDER);
            var border = new SolidBorder(1);

            return cellPosition switch
            {
                CellPosition.Top => cell.SetBorderTop(border).SetBorderLeft(border).SetBorderRight(border),
                CellPosition.Central => cell.SetBorderLeft(border).SetBorderRight(border),
                CellPosition.Bottom => cell.SetBorderBottom(border).SetBorderLeft(border).SetBorderRight(border),
                _ => throw new KeyNotFoundException(),
            };
        }
    }

}
