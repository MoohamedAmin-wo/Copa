namespace Cupa.Application.Services.Auth;
public interface IAuthService
{
    Task<bool> CheckUserExistByUserIdAsync(string userid);
    Task<bool> CheckUserExistByUsernameAsync(string username);
    Task<bool> CheckUserExistByUseremailAsync(string useremail);
    Task<bool> CheckUserPasswordsMatchAsync(string password, string confirmation);

    Task<bool> IsRoleExistAsync(string role);
    Task<bool> IsUserAuthenticatedAsync(ApplicationUser user);
    Task<bool> CheckUserRoleAsync(ApplicationUser user, string role);
    Task<bool> CheckUserRolesAsync(ApplicationUser user, List<string> roles);
    Task<string> AddUserToRoleAsync(ApplicationUser user, string role);
    Task<string> RemoveUserFromRoleAsync(ApplicationUser user, string role);
    Task<bool> IsUserEmailConfirmedAsync(ApplicationUser user);
    Task<bool> ConfirmUserEmailAsync(ApplicationUser user, string code);
    Task<string> GenerateNewConfirmationCodeAsync(ApplicationUser user);

    Task<string> UpdateCurrentUserAsync(ApplicationUser user);
    Task<string> CreateNewUserAsync(ApplicationUser user, string password);
    Task<bool> LogCurrentUserIntoSystem(ApplicationUser user, string password);
    Task<string> UpdateUserPasswordAsync(ApplicationUser user, string password);

    Task<string> SendEmailToUserAsync(string email, string messageSubject, string messagebody);

    Task<ApplicationUser?> GetUserByIdAsync(string id);
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<ApplicationUser?> GetUserByUsernameAsync(string username);
}