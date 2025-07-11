namespace Cupa.MidatR.Users.Commands.DTOs;
public sealed record UpdatePasswordModelDTO
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmationPassword { get; set; } = null!;
}