namespace Cupa.MidatR.Dashboard.Queries.DTOs;
public record AdminForDashBoardViewModelDTO
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phonenumber { get; set; }
    public string Username { get; set; }
    public string Status { get; set; }
    public int Age { get; set; }
    public string JoinedAsAdminFrom { get; set; }
}