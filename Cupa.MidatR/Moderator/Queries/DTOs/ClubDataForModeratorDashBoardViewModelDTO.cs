namespace Cupa.MidatR.Moderator.Queries.DTOs
{
    public sealed record ClubDataForModeratorDashBoardViewModelDTO
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string? LogoUrl { get; set; }
        public string CreatedOn { get; set; }
        public int PlayersCount { get; set; }
        public int AdminsCount { get; set; }
        public string? LastUpdatedOn { get; set; }
        public string Manager { get; set; }
        public string Status { get; set; }
    }
}
