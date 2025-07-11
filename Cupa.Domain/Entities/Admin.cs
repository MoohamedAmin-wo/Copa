namespace Cupa.Domain.Entities;
public sealed class Admin
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int ClubId { get; set; }
    public Club Club { get; set; }
    public DateTime JoinedAsAdminOn { get; set; } = DateTime.UtcNow;
}
