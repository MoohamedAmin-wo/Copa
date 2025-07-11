namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record SuccessStoriesForHomePageViewModelDTO
{
    public string Fullname { get; set; }
    public string VideoUrl { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Age { get; set; }
    public string PictureUrl { get; set; }
    public string Position { get; set; }
}
