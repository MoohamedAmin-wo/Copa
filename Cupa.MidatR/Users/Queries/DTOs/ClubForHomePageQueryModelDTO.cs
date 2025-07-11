namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record ClubForHomePageQueryModelDTO
{
    public int Id { get; set; }
    public string ClubName { get; set; }
    public string About { get; set; }
    public string LogoUrl { get; set; }
    public string? ClubPictureUrl { get; set; }
}