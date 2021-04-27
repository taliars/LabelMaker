using Microsoft.AspNetCore.Mvc;
using LabelMaker.Services.Contract;
using System.Threading.Tasks;
using LabelMaker.Core;
using LabelMaker.API.Entites;

namespace LabelMaker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IPdfService pdfService;

        public DocumentController(IPdfService pdfService)
        {
            this.pdfService = pdfService;
        }

        [HttpPost]
        public async Task<FileStreamResult> Get(DocumentQuery query)
        {
            var stream = await pdfService.CreateDocument(query.AppSettings, query.LabelContents);
            
            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"{query.Order}.pdf"
            };
        }
    }
}
