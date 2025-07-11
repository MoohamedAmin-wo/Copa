namespace Cupa.MidatR.Moderator.Queries.DTOs;
public record FreePlayerForModeratorDashBoardViewModelDTO
{
    public int Id { get; set; }
    public int Age { get; set; }
    public string Fullname { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public string CreateOn { get; set; }
    public string UpdatedOn { get; set; }
    public string Position { get; set; }
    public string ProfilePictureUrl { get; set; }
}
