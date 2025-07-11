namespace Cupa.Domain.AppExtenssions;
public static class PdfExtension
{
    public static bool IsPdfFileSizeValid(this IFormFile pdf) =>
          pdf.Length <= PdfSettings.MaxSizeForPdfFiles ? true : false;

    public static bool IsPdfFileExtensionValid(this IFormFile pdf) =>
       FileSettings.AllowedExtenssionForPdfFiles.Contains(Path.GetExtension(pdf.FileName)) ? true : false;
}