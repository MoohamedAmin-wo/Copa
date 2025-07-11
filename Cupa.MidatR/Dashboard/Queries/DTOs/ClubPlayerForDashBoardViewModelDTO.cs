namespace Cupa.MidatR.Dashboard.Queries.DTOs;
public record ClubPlayerForDashBoardViewModelDTO
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Number { get; set; }
    public string Status { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
    public string ContratctDuration { get; set; }
    public string ProfilePictureUrl { get; set; }
}