using Microsoft.AspNetCore.Mvc;
using LabelMaker.Services.Contract;
using System.IO;

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

        [HttpGet]
        public IActionResult Get()
        {
            var appSettings = new LabelMaker.Core.AppSettings
            {
                Company = "Рога и копыта",
            };

            var labelContents = new string[] { "asdasd", "1-1-1", "1-2-1", "1-1-1m" };

            var ms = pdfService.CreateDocument(appSettings, labelContents);

            return File(ms, "application/pdf", "result.pdf");
        }
    }
}
