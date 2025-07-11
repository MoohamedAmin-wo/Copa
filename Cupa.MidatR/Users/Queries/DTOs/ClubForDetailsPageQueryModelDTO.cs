namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record ClubForDetailsPageQueryModelDTO
{
    public string ClubName { get; set; }
    public string? Story { get; set; }
    public string? LogoUrl { get; set; }
    public string? MainShirtUrl { get; set; }
    public string? ClubPictureUrl { get; set; }
    public int PlayersCount { get; set; }
}
