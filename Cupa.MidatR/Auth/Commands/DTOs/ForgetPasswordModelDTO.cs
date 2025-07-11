namespace Cupa.MidatR.Auth.Commands.DTOs;

public record ForgetPasswordModelDTO(string email, string newPassword, string confirmationPassword);