namespace Cupa.MidatR.Dashboard.Queries.DTOs
{
    public sealed record PlayerViewForManagerModelDTO : ClubPlayerForDashBoardViewModelDTO
    {
        public string Price { get; set; }
        public string? ShirtPictureUrl { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ContractPictureUrl { get; set; }
        public DateOnly? JoinedClubOn { get; set; }
        public string IsAvaliableForSale { get; set; }
        public string Address { get; set; }
        public List<string> PlayerPictures { get; set; }
    }
}
