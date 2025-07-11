namespace Cupa.MidatR.Moderator.Queries.DTOs;

public sealed record ClubPlayerForModeratorDashBoardViewModelDTO : FreePlayerForModeratorDashBoardViewModelDTO
{
    public string Club { get; set; }
}