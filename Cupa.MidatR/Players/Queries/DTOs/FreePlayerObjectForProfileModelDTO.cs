namespace Cupa.MidatR.Players.Queries.DTOs;
public sealed record FreePlayerObjectForProfileModelDTO
{
    public int Views { get; set; }
    public string NickName { get; set; }
    public string Position { get; set; }
    public string PreefAbout { get; set; }
    public string StoryAbout { get; set; }
    public string PictureUrl { get; set; }
    public string VideoUrl { get; set; }
    public List<string> Pictures { get; set; }
}
