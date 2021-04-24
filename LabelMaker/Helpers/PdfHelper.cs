using System.Collections.Generic;
using System.Diagnostics;

using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace LabelMaker.Helpers
{

    public class PdfHelper
    {
        private const string DEJAVUSerif = "./src/ttf/DejaVuSerif.ttf";

        public PdfHelper(string path, string company, string[] labelContents)
        {
            var document = CreateDocument(path);

            var paragraph = new Paragraph();

            foreach (var labelContent in labelContents)
            {
                var table = CreateTable(company, labelContent);
                paragraph.Add(table);
            }

            document.Add(paragraph);

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

        private Document CreateDocument(string path)
        {
            // Must have write permissions to the path folder
            var writer = new PdfWriter(path);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.SetFont(CreateFont(DEJAVUSerif));
            document.SetFontSize(14f);
            document.SetLeftMargin(10f);
            document.SetRightMargin(10f);

            return document;
        }

        private Table CreateTable(string company, string labelContent)
        {
            var cellDescriptions = new CellDescription[]
            {
                new CellDescription
                {
                    Content = company,
                    TextAlignment = TextAlignment.CENTER,
                    CellPosition = CellPosition.Top,
                    ColumnSpan = 2
                },
                new CellDescription
                {
                    Content = "Проба №",
                    TextAlignment = TextAlignment.RIGHT,
                    CellPosition = CellPosition.CentralLeft
                },
                new CellDescription
                {
                    Content = labelContent,
                    TextAlignment = TextAlignment.LEFT,
                    CellPosition = CellPosition.CentralRight
                },
                new CellDescription
                {
                    Content ="Проба",
                    TextAlignment = TextAlignment.RIGHT,
                    CellPosition = CellPosition.BottomLeft
                },
                new CellDescription
                {
                    Content ="почвы (грунта)",
                    TextAlignment = TextAlignment.RIGHT,
                    CellPosition = CellPosition.BottomRight
                },
            };

            var table = new Table(2, false);
            table.SetKeepTogether(true);

            foreach (var cellDescription in cellDescriptions)
            {
                var cell = CreateCell(cellDescription);
                table.AddCell(cell);
            }

            table.SetMarginBottom(2.5f);
            table.SetMarginRight(2.5f);

            return table;
        }

        private PdfFont CreateFont(string font) =>
            PdfFontFactory.CreateFont(font, PdfEncodings.IDENTITY_H);

        private Cell CreateCell(CellDescription cellDescription)
        {
            var cell = new Cell(1, cellDescription.ColumnSpan)
                .SetTextAlignment(cellDescription.TextAlignment)
                .Add(new Paragraph(cellDescription.Content));

            SetBorder(cell, cellDescription.CellPosition);
            return cell;
        }

        private static Cell SetBorder(Cell cell, CellPosition cellPosition)
        {
            cell.SetBorder(Border.NO_BORDER);
            var border = new SolidBorder(1);

            switch (cellPosition)
            {
                case CellPosition.Top:
                    cell.SetBorderTop(border).SetBorderLeft(border).SetBorderRight(border);
                    break;
                case CellPosition.TopLeft:
                    cell.SetBorderTop(border).SetBorderLeft(border);
                    break;
                case CellPosition.TopRight:
                    cell.SetBorderTop(border).SetBorderRight(border);
                    break;
                case CellPosition.CentralLeft:
                    cell.SetBorderLeft(border);
                    break;
                case CellPosition.CentralRight:
                    cell.SetBorderRight(border);
                    break;
                case CellPosition.BottomLeft:
                    cell.SetBorderBottom(border).SetBorderLeft(border);
                    break;
                case CellPosition.BottomRight:
                    cell.SetBorderBottom(border).SetBorderRight(border);
                    break;
                default:
                    throw new KeyNotFoundException();
            }

            return cell;
        }
    }

}
