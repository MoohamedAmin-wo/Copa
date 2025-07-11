namespace Cupa.Domain.ValidationAttrubutes;
public class VideoAttribuite : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = (IFormFile?)value;
        if (file is null)
            return new ValidationResult("file is required !");

        if (file.Length > VideoSetttings.MaxSizeForVideos)
            return new ValidationResult($"max allowed size is 5 MB !");

        if (!FileSettings.AllowedExtenssionsForVideos.Contains(Path.GetExtension(file.FileName)))
            return new ValidationResult("only allwoed Mp4 , Mov  files !");

        return ValidationResult.Success;
    }
}