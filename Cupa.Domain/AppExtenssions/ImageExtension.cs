namespace Cupa.Domain.AppExtenssions;
public static class ImageExtension
{
    public static bool IsPictureSizeValid(this IFormFile picture) =>
           picture.Length <= ImageSettings.MaxSizeForPictures ? true : false;

    public static bool IsPictureExtensionValid(this IFormFile picture) =>
       FileSettings.AllowedExtenssionsForPictures.Contains(Path.GetExtension(picture.FileName.ToLowerInvariant())) ? true : false;

    public static bool IsAllowedToUploadMorePicturesForPlayer(this Player player, int count)
        => count >= ImageSettings.MaxAllowedPicturesCountForPlayers ? false : true;
}