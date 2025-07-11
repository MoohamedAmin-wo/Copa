namespace Cupa.MidatR.Dashboard.Queries.DTOs
{
    public sealed record AdminViewForManagerModelDTO : AdminForDashBoardViewModelDTO
    {
        public string pictureUrl { get; set; }
        public string pictureThumbnialUrl { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
    }
}
