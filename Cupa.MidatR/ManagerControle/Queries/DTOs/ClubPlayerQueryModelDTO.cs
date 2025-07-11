namespace Cupa.MidatR.ManagerControle.Queries.DTOs;
public record ClubPlayerQueryModelDTO
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PictureUrl { get; set; }
    public string Position { get; set; }
    public string Status { get; set; }
    public int Age { get; set; }
}