namespace Cupa.Domain.Consts;
public static class VideoSetttings
{
    public static int MaxSizeForVideos = 15 * 1024 * 1024; // 15MB
    public static int MaxAllowedVideoCountForPlayers = 2;
    public static TimeSpan MaxDurationForvideos = TimeSpan.FromMinutes(3); // 3 minutes
}