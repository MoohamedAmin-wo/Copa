namespace Cupa.MidatR.Dashboard.Queries.DTOs;
public sealed record ClubDataForDashBoardViewModelDTO
{
    public string ClubName { get; set; }
    public string? Story { get; set; }
    public string? About { get; set; }
    public string? LogoUrl { get; set; }
    public string? MainShirtUrl { get; set; }
    public string? ClubPictureUrl { get; set; }
    public string CreatedOn { get; set; }
    public int PlayersCount { get; set; } = 0;
    public int AdminsCount { get; set; } = 0;
    public string? LastUpdatedOn { get; set; }
}
