namespace Cupa.MidatR.Moderator.Queries.DTOs;
public sealed record UserDataForModeratorDashBoardViewModelDTO
{
    public string Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public string EmailConfirmed { get; set; }
    public string DateOfBirth { get; set; }
    public string CreateOn { get; set; }
    public string UpdateOn { get; set; }
}