using Microsoft.AspNetCore.Http;
using System.Text;

namespace FilesToPdfA.Application.Helpers
{
    public static class FormFileHelper
    {
        public static byte[] ConvertToBytes(IFormFile formFile)
        {
            using MemoryStream stream = new();
            formFile.CopyTo(stream);
            return stream.ToArray();
        }

        public static string ConvertToString(IFormFile formFile)
        {
            byte[] bytes = ConvertToBytes(formFile);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
