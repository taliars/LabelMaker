using System.Diagnostics;
using iText.Layout;
using iText.Layout.Element;
using LabelMaker.Core;
using LabelMaker.Services.Contract;

namespace LalelMaker.Services.Implementation
{
    public class PdfService: IPdfService
    {
        public bool CreateDocument(string path,
            AppSettings appSettings,
            string[] labelContents)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false ;
            }

            var document = PdfHelper.CreateDocumentTemplate(path, appSettings?.FontSize ?? DefaultSettings.FontSize);
            var rowTableParagraph = new Paragraph();

            foreach (var labelContent in labelContents)
            {
                var table = PdfHelper.CreateTable(appSettings.Company, labelContent);
                rowTableParagraph.Add(table);
            }

            document.Add(rowTableParagraph);
            document.Close();

            return true;
        }
    }
}
