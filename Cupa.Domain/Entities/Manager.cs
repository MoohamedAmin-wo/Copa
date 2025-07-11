namespace Cupa.Domain.Entities;
public sealed class Manager
{
    public int Id { get; set; }
    public string? StoryAbout { get; set; }
    public string? StoryWithClub { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public DateOnly ContractEndsOn { get; set; }
    public Tuple<int, int> AppointmentDuration => new(AppointmentDate.Year - ContractEndsOn.Year, AppointmentDate.Month - ContractEndsOn.Month);

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
