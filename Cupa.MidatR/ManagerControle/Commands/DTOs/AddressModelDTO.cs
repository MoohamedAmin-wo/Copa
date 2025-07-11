namespace Cupa.MidatR.ManagerControle.Commands.DTOs
{
    public record AddressModelDTO
    {
        public string Country { get; set; } = null!;
        public string Governrate { get; set; } = null!;
        public string? City { get; set; }
        public string? Regoin { get; set; }
        public string? Street { get; set; }

    }
}
