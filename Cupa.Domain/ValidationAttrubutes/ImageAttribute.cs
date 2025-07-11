namespace Cupa.Domain.ValidationAttrubutes;
public class ImageAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var image = (IFormFile?)value;

        if (image!.Length > ImageSettings.MaxSizeForPictures)
            return new ValidationResult(ErrorMessages.MaxSize);

        if (!FileSettings.AllowedExtenssionsForPictures.Contains(Path.GetExtension(image.FileName)))
            return new ValidationResult(ErrorMessages.NotAllowedExtension);

        return ValidationResult.Success;
    }
}