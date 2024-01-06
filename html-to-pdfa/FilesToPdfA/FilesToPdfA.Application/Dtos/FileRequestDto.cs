using Microsoft.AspNetCore.Http;

namespace FilesToPdfA.Application.Dtos
{
    public class FileRequestDto
    {
        public IFormFile? FormFile { get; set; }
    }
}
