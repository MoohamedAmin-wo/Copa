namespace Cupa.MidatR.Users.Commands.DTOs;
public sealed record UserAddressModelDTO
{
    public string City { get; set; }
    public string Regoin { get; set; }
    public string Street { get; set; }
    public string Country { get; set; }
    public string Governrate { get; set; }
}