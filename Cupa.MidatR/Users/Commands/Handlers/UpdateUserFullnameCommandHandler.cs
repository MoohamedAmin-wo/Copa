namespace Cupa.MidatR.Users.Commands.Handlers;
internal sealed class UpdateUserFullnameCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUserFullnameCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(UpdateUserFullnameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        // validate the model 

        user.FirstName = request.Model.Firstname;
        user.LastName = request.Model.Lastname;
        user.UpdatedBy = user.Id;
        user.UpdatedOn = DateTime.UtcNow;

        try
        {
            await _userManager.UpdateAsync(user);
        }
        catch (Exception)
        {
            return new GlobalResponseDTO { Message = "unable to apply updates !" };
        }

        return new GlobalResponseDTO { IsSuccess = true, Message = "Update compelete successfully" };
    }
}