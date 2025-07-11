namespace Cupa.Domain.AppExtenssions;
public static class VideoExtenstion
{
    public static TimeSpan GetVideoDurationAsync(string video)
    {
        var inputFile = new MediaFile { Filename = video };
        using (var engine = new Engine())
        {
            engine.GetMetadata(inputFile);
        }

        return inputFile.Metadata.Duration;
    }

    public static async Task<bool> IsValidVideoDuration(this IFormFile video)
    {

        var tempFilePath = Path.GetTempFileName();
        using (var stream = new FileStream(tempFilePath, FileMode.Create))
        {
            await video.CopyToAsync(stream);
        }
        try
        {
            // Extract video duration
            var duration = GetVideoDurationAsync(tempFilePath);

            // Validate video duration
            if (duration > VideoSetttings.MaxDurationForvideos)
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsVideoSizeValid(this IFormFile video) =>
            video.Length <= VideoSetttings.MaxSizeForVideos ? true : false;

    public static bool IsVideoExtensionValid(this IFormFile video) =>
         FileSettings.AllowedExtenssionsForVideos.Contains(Path.GetExtension(video.FileName.ToLowerInvariant())) ? true : false;

    public static bool IsAllowedToUploadMoreVideosForPlayer(this Player player, int count) =>
           count >= VideoSetttings.MaxAllowedVideoCountForPlayers ? false : true;
}