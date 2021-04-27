using LabelMaker.Core;
using System.IO;

namespace LabelMaker.Services.Contract
{
    public interface IPdfService
    {
        MemoryStream CreateDocument(AppSettings appSettings, string[] labelContents);
    }
}
