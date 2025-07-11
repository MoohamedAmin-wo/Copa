namespace Cupa.Domain.Entities
{
    public sealed class SuccessStories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? PictureUrl { get; set; }
        public string? Position { get; set; }
        public string Age => (DateOnly.FromDateTime(DateTime.Now).Year - DateOfBirth.Year).ToString();
    }
}
