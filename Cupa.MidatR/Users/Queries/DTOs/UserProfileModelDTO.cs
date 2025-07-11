namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record UserProfileModelDTO
{
    public int Age { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public string JoinedUsOn { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string ProfilePictureUrl { get; set; }
    public DateTime? LastUpdateForPassword { get; set; }
    public string ProfilePictureThumbnailUrl { get; set; }

}
