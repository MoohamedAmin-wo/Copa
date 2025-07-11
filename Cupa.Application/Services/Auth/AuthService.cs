namespace Cupa.Application.Services.Auth;
public class AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IEmailSender _emailSender = emailSender;
    public async Task<bool> CheckUserExistByUsernameAsync(string username) =>
        await _userManager.FindByNameAsync(username) is null ? false : true;
    public async Task<bool> CheckUserExistByUserIdAsync(string useremail) =>
        await _userManager.FindByEmailAsync(useremail) is null ? false : true;
    public async Task<bool> CheckUserExistByUseremailAsync(string userid) =>
        await _userManager.FindByIdAsync(userid) is null ? false : true;
    public async Task<bool> IsUserAuthenticatedAsync(ApplicationUser user)
        => await _userManager.IsInRoleAsync(user, CupaRoles.User);
    public async Task<bool> IsUserEmailConfirmedAsync(ApplicationUser user)
        => await Task.FromResult(user.EmailConfirmed);
    public async Task<bool> ConfirmUserEmailAsync(ApplicationUser user, string code)
    {
        var confirmationResult = await _userManager.ConfirmEmailAsync(user, code);
        if (!confirmationResult.Succeeded)
            return false;

        return true;
    }
    public async Task<bool> LogCurrentUserIntoSystem(ApplicationUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password);
    public async Task<bool> CheckUserPasswordsMatchAsync(string password, string confirmation)
        => await Task.FromResult(password.Equals(confirmation));

    public async Task<string> UpdateCurrentUserAsync(ApplicationUser user)
    {
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return string.Join(",", updateResult.Errors.Select(x => x.Description));

        return string.Empty;
    }
    public async Task<string> GenerateNewConfirmationCodeAsync(ApplicationUser user)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user);
    public async Task<string> CreateNewUserAsync(ApplicationUser user, string password)
    {
        var creationResult = await _userManager.CreateAsync(user, password);
        if (!creationResult.Succeeded)
            return string.Join(",", creationResult.Errors.Select(x => x.Description));

        return string.Empty;
    }

    public async Task<bool> IsRoleExistAsync(string role)
       => await _roleManager.RoleExistsAsync(role);
    public async Task<bool> CheckUserRoleAsync(ApplicationUser user, string role)
        => await _userManager.IsInRoleAsync(user, role);
    public async Task<bool> CheckUserRolesAsync(ApplicationUser user, List<string> roles)
    {
        foreach (var role in roles)
        {
            if (await _userManager.IsInRoleAsync(user, role))
                return true;
        }
        return false;
    }

    public async Task<string> AddUserToRoleAsync(ApplicationUser user, string role)
    {
        if (user is null || !await IsRoleExistAsync(role))
            return ErrorMessages.UnExistedRoleOrUser;

        if (await CheckUserRoleAsync(user, role))
            return "User already has this role !";

        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
            return string.Join(",", result.Errors.Select(x => x.Description));

        return string.Empty;
    }
    public async Task<string> RemoveUserFromRoleAsync(ApplicationUser user, string role)
    {
        if (user is null || !await IsRoleExistAsync(role))
            return ErrorMessages.UnExistedRoleOrUser;

        if (!await CheckUserRoleAsync(user, role))
            return "User hasn't the role !";

        if (role.Equals(CupaRoles.User))
            return ErrorMessages.CannotRemoveUserRole;

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        if (!result.Succeeded)
            return string.Join(",", result.Errors.Select(x => x.Description));

        return string.Empty;
    }
    public async Task<string> UpdateUserPasswordAsync(ApplicationUser user, string password)
    {
        try
        {
            var removePasswrodResult = await _userManager.RemovePasswordAsync(user);
            try
            {
                var addPasswordResult = await _userManager.AddPasswordAsync(user, password);
                if (!addPasswordResult.Succeeded)
                    return string.Join(",", addPasswordResult.Errors.Select(x => x.Description));

                user.UpdatedOn = DateTime.Now;
            }
            catch (Exception)
            {
                return "Faild to add new password !";
            }
        }
        catch (Exception)
        {
            return "Faild to remove old password !";
        }

        return string.Empty;
    }
    public async Task<string> SendEmailToUserAsync(string email, string messageSubject, string messagebody)
    {
        try
        {
            await _emailSender.SendEmailAsync(email, messageSubject, messagebody);
        }
        catch (Exception)
        {
            return "your internet connection is week , please try again later !";
        }

        return string.Empty;
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        => await _userManager.FindByIdAsync(id);
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);
    public async Task<ApplicationUser?> GetUserByUsernameAsync(string username)
        => await _userManager.FindByNameAsync(username);
}