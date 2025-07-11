namespace Cupa.Application.Common.Response;
public sealed record UploadResponse
{
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Uid { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}
