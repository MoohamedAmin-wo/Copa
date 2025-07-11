namespace Cupa.MidatR.Users.Queries.DTOs;
public sealed record SelectedPlayerObjectForAuthointicatedUsersModelDTO
{
    public int Views { get; set; }
    public string Fullname { get; set; }
    public string? NickName { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? Age { get; set; }
    public string? BirthDate { get; set; }
    public string? PreefAbout { get; set; }
    public string? StoryAbout { get; set; }
    public string? Address { get; set; }
    public string? Video { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public List<string>? Pictures { get; set; }
    public string? Club { get; set; }
    public string? Price { get; set; }
    public string? Number { get; set; }
    public string? ContractEndsOn { get; set; }
    public string? ShirtPictureUrl { get; set; }
    public bool IsAvaliableForSale { get; set; }
    public string? JoinedClubOn { get; set; }
}