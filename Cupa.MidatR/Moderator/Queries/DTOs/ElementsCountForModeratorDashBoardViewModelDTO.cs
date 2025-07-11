namespace Cupa.MidatR.Moderator.Queries.DTOs;
public sealed record ElementsCountForModeratorDashBoardViewModelDTO
{
    public int UsersCount { get; set; }
    public int AdminsCount { get; set; }
    public int ManagersCount { get; set; }
    public int ClubPlayersCount { get; set; }
    public int FreePlayersCount { get; set; }
}