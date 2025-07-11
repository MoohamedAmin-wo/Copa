namespace Cupa.MidatR.ManagerControle.Commands.DTOs
{
    public record ManagerModelDTO
    {
        public string StoryAbout { get; set; }
        public string StoryWithClub { get; set; }
        public DateOnly AppoitmentDate { get; set; }
        public DateOnly contractEndsOn { get; set; }
    }
}
