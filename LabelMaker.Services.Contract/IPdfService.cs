using LabelMaker.Core;

namespace LabelMaker.Services.Contract
{
    public interface IPdfService
    {
        bool CreateDocument(string path, AppSettings appSettings, string[] labelContents);
    }
}
