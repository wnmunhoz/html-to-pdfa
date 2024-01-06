using FilesToPdfA.Application.Dtos;
using FilesToPdfA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilesToPdfA.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlToPdfAController : ControllerBase
    {
        private readonly IHtmlToPdfAService _htmlToPdfAService;

        public HtmlToPdfAController(IHtmlToPdfAService htmlToPdfAService)
        {
            _htmlToPdfAService = htmlToPdfAService;
        }

        [HttpPost]
        public IActionResult Convert([FromForm] FileRequestDto request)
        {
            try
            {
                _htmlToPdfAService.Convert(request);
                return Ok();
            }
            catch
            {
                throw;
            }
        }
    }
}