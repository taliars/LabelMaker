using System.Collections.Generic;
using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using LabelMaker.Core;

namespace LalelMaker.Services.Implementation
{
    public static class PdfHelper
    {
        // TODO: Margins to settings
        public static Document CreateDocumentTemplate(MemoryStream memoryStream, int fontSize)
        {
            // Must have write permissions to the path folder
            var writer = new PdfWriter(memoryStream);
            writer.SetCloseStream(false);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var font = CreateFont(DefaultSettings.FontFamily);
            document.SetFont(font);
            document.SetFontSize(fontSize);
            document.SetLeftMargin(10f);
            document.SetRightMargin(10f);

            return document;
        }

        public static Table CreateTable(string company, string labelContent)
        {
            var cellDescriptions = new CellDescription[]
            {
                new CellDescription(company, CellPosition.Top),
                new CellDescription($"Проба № {labelContent}", CellPosition.Central),
                new CellDescription("Проба почвы (грунта)", CellPosition.Bottom),
            };

            var table = new Table(DefaultSettings.ColumnCount, false)
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
            var cell = new Cell(DefaultSettings.RowSpan, DefaultSettings.ColumnSpan)
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
