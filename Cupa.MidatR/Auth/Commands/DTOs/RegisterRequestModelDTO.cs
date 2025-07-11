namespace Cupa.MidatR.Auth.Commands.DTOs;

public record RegisterRequestModelDTO(string FirstName, string LastName, string Username, string email, string password, DateOnly BirthDate);