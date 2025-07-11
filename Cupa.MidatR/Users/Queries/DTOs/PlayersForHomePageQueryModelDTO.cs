namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record PlayersForHomePageQueryModelDTO
{
    public int Id { get; set; }
    public int Age { get; set; }
    public string Fullname { get; set; }
    public string Nickname { get; set; }
    public string Position { get; set; }
    public string ProfilePictureUrl { get; set; }
    public int Views { get; set; }
}
