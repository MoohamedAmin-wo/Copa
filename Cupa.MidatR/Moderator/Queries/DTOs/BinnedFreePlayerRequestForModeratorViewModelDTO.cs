namespace Cupa.MidatR.Moderator.Queries.DTOs;

public sealed record BinnedFreePlayerRequestForModeratorViewModelDTO : FreePlayerForModeratorDashBoardViewModelDTO
{
    public string VideoUrl { get; set; }
    public string StoryAbout { get; set; }
    public string PreefAbout { get; set; }
}