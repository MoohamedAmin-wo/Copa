namespace Cupa.MidatR.Auth.Commands.DTOs;
public sealed record ContactUsModelDTO
{
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Subject { get; set; }
}
