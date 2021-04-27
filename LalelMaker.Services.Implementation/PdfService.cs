using iText.Layout.Element;
using LabelMaker.Core;
using LabelMaker.Services.Contract;
using System.IO;

namespace LalelMaker.Services.Implementation
{
    public class PdfService : IPdfService
    {
        public MemoryStream CreateDocument(AppSettings appSettings, string[] labelContents)
        {
            var memoryStream = new MemoryStream();

            var fontSize = appSettings?.FontSize ?? DefaultSettings.FontSize;
            var document = PdfHelper.CreateDocumentTemplate(memoryStream, fontSize);
            var rowTableParagraph = new Paragraph();

            foreach (var labelContent in labelContents)
            {
                var table = PdfHelper.CreateTable(appSettings.Company, labelContent);
                rowTableParagraph.Add(table);
            }

            document.Add(rowTableParagraph);
            document.Close();
            memoryStream.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
