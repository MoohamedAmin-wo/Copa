namespace Cupa.MidatR.ManagerControle.Commands.Handlers;
internal sealed class AssignAdminHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IEmailSender emailSender) : IRequestHandler<AssignAdminCommand, GlobalResponseDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(AssignAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (!await _userManager.IsInRoleAsync(user, CupaRoles.Manager))
            return new GlobalResponseDTO { Message = ErrorMessages.UnAuthorizedUser };

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null)
            return new GlobalResponseDTO { Message = ErrorMessages.ErrorFindExistClub };

        var isExistUser = await _userManager.FindByEmailAsync(request.Model.AdminEmail);
        if (isExistUser is null)
            return new GlobalResponseDTO { Message = ErrorMessages.ErrorFindExistUser };

        if (!isExistUser.EmailConfirmed)
            return new GlobalResponseDTO { Message = ErrorMessages.EmailNotconfirmed };

        if (isExistUser.Age < 22)
            return new GlobalResponseDTO { Message = "Can't assign admin role for less than 22 years old user !" };

        if (!await CheckUserRolesAsync(isExistUser))
            return new GlobalResponseDTO { Message = "can't assign admin role to this user !" };

        if (await _userManager.IsInRoleAsync(isExistUser, CupaRoles.Admin))
            return new GlobalResponseDTO { Message = ErrorMessages.AdminRoleAssigned };

        var newAdmin = new Admin
        {
            UserId = isExistUser.Id,
            ClubId = club.Id
        };

        // create new admin entity.
        try
        {
            await _unitOfWork.admins.CreateAsync(newAdmin);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = ErrorMessages.UnHandledServerError };
        }

        // add new admin to Admin role.
        try
        {
            await _userManager.AddToRoleAsync(isExistUser, CupaRoles.Admin);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = "can't assign admin role to this user !" };
        }

        club.AdminsCount++;
        await _unitOfWork.CommitAsync();


        // send email to this admin to notify him about his new role.
        await _emailSender.SendEmailAsync(isExistUser.Email!, "Notification about your new role ",
        $"<h2>Congratulations {isExistUser.FirstName} <h3>you are add as Admin to this club {club.ClubName}<p>you are free to use your admin panal </p></h3></h2>");

        return new GlobalResponseDTO { IsSuccess = true, Message = "Admin Created Successfully" };
    }

    private async Task<bool> CheckUserRolesAsync(ApplicationUser user)
    {
        if (await _userManager.IsInRoleAsync(user, CupaRoles.Player) ||
            await _userManager.IsInRoleAsync(user, CupaRoles.Manager) ||
            await _userManager.IsInRoleAsync(user, CupaRoles.Moderator))
            return false;

        return true;
    }
}