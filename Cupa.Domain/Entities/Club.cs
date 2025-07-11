using Cupa.Domain.Common;
namespace Cupa.Domain.Entities;
public class Club : BaseEntity
{
    public int Id { get; set; }
    public string ClubName { get; set; }
    public string? Story { get; set; }
    public string? About { get; set; }
    public string? LogoUrl { get; set; }
    public string? MainShirtUrl { get; set; }
    public string? ClubPictureUrl { get; set; }
    public int PlayersCount { get; set; } = 0;
    public int AdminsCount { get; set; } = 0;
    public int ManagerId { get; set; }
    public Manager Manager { get; set; } = null!;
    public List<Admin> Admins { get; set; } = new List<Admin>();
    public List<ClubPlayer> ClubPlayers { get; set; } = new List<ClubPlayer>();
}
