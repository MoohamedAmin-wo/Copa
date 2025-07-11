namespace Cupa.Domain.Entities;

public class Address
{
    public int Id { get; set; }
    public string? City { get; set; }
    public string? Regoin { get; set; }
    public string? Street { get; set; }
    public string Country { get; set; }
    public string Governrate { get; set; }

    public override string ToString()
    {
        return $"{Country}/{Governrate}/{City}/{Regoin}/{Street}";
    }

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; }
}
