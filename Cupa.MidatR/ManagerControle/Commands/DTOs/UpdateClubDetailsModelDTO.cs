namespace Cupa.MidatR.ManagerControle.Commands.DTOs;
public sealed record UpdateClubDetailsModelDTO
{
    public string ClubName { get; set; }
    public string? Story { get; set; }
    public string? About { get; set; }
}