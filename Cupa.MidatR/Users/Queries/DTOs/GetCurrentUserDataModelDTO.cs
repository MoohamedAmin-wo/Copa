namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record GetCurrentUserDataModelDTO
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PictureUrl { get; set; }
    public string PictureThumbnailUrl { get; set; }
    public List<string> UserRoles { get; set; }
}
