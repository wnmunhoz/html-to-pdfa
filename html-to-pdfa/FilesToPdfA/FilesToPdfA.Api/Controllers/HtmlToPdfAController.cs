using FilesToPdfA.Application.Dtos;
using FilesToPdfA.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerResponse(200, "success")]
        [SwaggerResponse(400, "bad request")]
        [SwaggerResponse(500, "some failure")]
        public IActionResult Convert([FromForm] FileRequestDto request)
        {
            try
            {
                if (request.FormFile != null && request.FormFile.Length > 0)
                {
                    _htmlToPdfAService.Convert(request);
                    return Ok();
                }
                else
                {
                    return BadRequest("You need to send the html file");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}