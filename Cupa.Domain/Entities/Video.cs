using Cupa.Domain.Common;
namespace Cupa.Domain.Entities;
public class Video : BaseEntity
{
    public int Id { get; set; }
    public string Url { get; set; } = null!;
    public string VideoUid { get; set; }
    public int? PlayerId { get; set; }
    public Player Player { get; set; }
}