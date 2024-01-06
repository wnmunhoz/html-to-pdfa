using FilesToPdfA.Application.Dtos;

namespace FilesToPdfA.Application.Interfaces
{
    public interface IHtmlToPdfAService
    {
        void Convert(FileRequestDto request);
    }
}
