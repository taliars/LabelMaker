using LabelMaker.Core;

using System.IO;
using System.Threading.Tasks;

namespace LabelMaker.Services.Contract
{
    public interface IPdfService
    {
        Task<MemoryStream> CreateDocument(AppSettings appSettings, string[] labelContents);
    }
}
