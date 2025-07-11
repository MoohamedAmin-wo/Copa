using Cupa.Domain.Common;
namespace Cupa.Domain.Entities;
public sealed class Player : BaseEntity
{
    public int Id { get; set; }
    public string? StoryAbout { get; set; } = null!;
    public string? PreefAbout { get; set; } = null!;
    public string? NickName { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; } = null!;
    public bool IsFree { get; set; }
    public bool IsBinned { get; set; }
    public int Rate { get; set; } = 0;
    public int Views { get; set; } = 0;

    public int PositionId { get; set; }
    public Position Position { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public List<Video> Videos { get; set; } = new List<Video>();
    public List<Picture> Pictures { get; set; } = new List<Picture>();
}