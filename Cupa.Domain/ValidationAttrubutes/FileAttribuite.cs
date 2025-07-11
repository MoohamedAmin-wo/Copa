namespace Cupa.Domain.ValidationAttrubutes;

public class FileAttribuite : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = (IFormFile?)value;
        if (file is null)
            return new ValidationResult("file is required !");

        if (file.Length > PdfSettings.MaxSizeForPdfFiles)
            return new ValidationResult($"max allowed size is 5 MB !");

        if (!FileSettings.AllowedExtenssionForPdfFiles.Contains(Path.GetExtension(file.FileName)))
            return new ValidationResult("only allwoed Pdf files !");

        return ValidationResult.Success;
    }
}
