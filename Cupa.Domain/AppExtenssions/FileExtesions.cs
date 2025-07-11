using Cupa.Domain.Enums;
namespace Cupa.Domain.AppExtenssions;
public static class FileExtesions
{
    public static string IsValidPdf(this IFormFile file)
    {
        if (!file.IsPdfFileExtensionValid())
            return ErrorMessages.NotValidPdfFileExtension;

        if (!file.IsPdfFileSizeValid())
            return ErrorMessages.NotValidPdfFileSize;

        return string.Empty;
    }

    public static string IsValidImage(this IFormFile file)
    {
        if (!file.IsPictureExtensionValid())
            return ErrorMessages.NotValidPictureExtensions;

        if (!file.IsPictureSizeValid())
            return ErrorMessages.NotValidPictureSize;

        return string.Empty;
    }

    public static FileType GetFileType(this IFormFile file)
    {
        if (file.IsPdfFileExtensionValid())
            return FileType.pdf;

        if (file.IsPictureExtensionValid())
            return FileType.picture;

        if (file.IsVideoExtensionValid())
            return FileType.video;

        return FileType.invalid;
    }

    public static async Task<string> IsValidVideo(this IFormFile file)
    {
        if (!file.IsVideoExtensionValid())
            return ErrorMessages.NotValidVideoExtensions;

        if (!file.IsVideoSizeValid())
            return ErrorMessages.NotValidVideoSize;

        //if (!await file.IsValidVideoDuration())
        //    return ErrorMessages.NotValidVideoDuration;

        return string.Empty;
    }
}