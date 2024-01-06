using FilesToPdfA.Application.Dtos;
using FilesToPdfA.Application.Helpers;
using FilesToPdfA.Application.Interfaces;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Font;
using iText.Pdfa;
using Microsoft.Extensions.Hosting;

namespace FilesToPdfA.Application.Services
{
    public class HtmlToPdfAService : IHtmlToPdfAService
    {
        private readonly string _dest;

        public HtmlToPdfAService(IHostEnvironment hostEnvironment)
        {
            string rootPath = hostEnvironment.ContentRootPath;
            _dest = $"{rootPath}/Files/{Guid.NewGuid()}.pdf";

            FileInfo file = new(_dest);
            file.Directory?.Create();
        }

        public void Convert(FileRequestDto request)
        {
            try
            {
                if (request.FormFile != null && request.FormFile.Length > 0)
                {
                    string textContent = FormFileHelper.ConvertToString(request.FormFile);

                    using MemoryStream stream = new();
                    using PdfWriter pdfWriter = new(stream);

                    Stream fileStream = new FileStream("Files/ICM/sRGB_CS_profile.icm", FileMode.Open, FileAccess.Read);

                    PdfADocument pdfDoc = new(
                        pdfWriter,
                        PdfAConformanceLevel.PDF_A_3B,
                        new PdfOutputIntent("Custom", "", null, "sRGB IEC61966-2.1", fileStream)
                    );

                    Document document = new(pdfDoc, PageSize.A4.Rotate());
                    PdfDictionary parameters = new();
                    parameters.Put(PdfName.ModDate, new PdfDate().GetPdfObject());

                    PdfDocumentInfo info = pdfDoc.GetDocumentInfo();
                    info
                        .SetTitle("Title")
                        .SetAuthor("Author")
                        .SetSubject("Subject")
                        .SetCreator("Creator")
                        .SetKeywords("Metadata, iText, PDF")
                        .SetCreator("My program using iText")
                        .AddCreationDate();

                    FontProvider fontProvider = new("Arial");
                    fontProvider.AddStandardPdfFonts();
                    fontProvider.AddSystemFonts();

                    ConverterProperties properties = new();
                    properties.SetFontProvider(fontProvider);

                    Document doc = new(pdfDoc);
                    HtmlConverter.ConvertToPdf(textContent, pdfDoc, properties);
                    File.WriteAllBytes(_dest, stream.ToArray());
                    document.Close();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
