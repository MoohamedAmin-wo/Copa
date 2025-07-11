using Cupa.Domain.Common;
namespace Cupa.Domain.Entities;
public class Picture : BaseEntity
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public string PictureUid { get; set; }
    public int? PlayerId { get; set; }
    public Player Player { get; set; }
}