namespace Cupa.Domain.Entities;
public sealed class ClubPlayer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public int Number { get; set; }
    public double Price { get; set; }
    public string? ShirtPictureUrl { get; set; }
    public bool HasCurrentContract { get; set; }
    public string? ContractPictureUrl { get; set; }
    public string? ContractDuration { get; set; }
    public DateOnly? JoinedClubOn { get; set; }
    public bool IsAvaliableForSale { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    public int ClubId { get; set; }
    public Club Club { get; set; }
}