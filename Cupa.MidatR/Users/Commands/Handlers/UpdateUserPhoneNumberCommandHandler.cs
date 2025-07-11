namespace Cupa.MidatR.Users.Commands.Handlers;
internal sealed class UpdateUserPhoneNumberCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUserPhoneNumberCommand, GlobalResponseDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GlobalResponseDTO> Handle(UpdateUserPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return new GlobalResponseDTO { Message = ErrorMessages.UserNotFound };

        if (user.PhoneNumber != null && user.PhoneNumber.Equals(request.Phonenumber))
            return new GlobalResponseDTO { Message = "new phone number is the same old one !" };

        user.PhoneNumber = request.Phonenumber;
        user.PhoneNumberConfirmed = true;
        user.UpdatedBy = user.Id;
        user.UpdatedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
        return new GlobalResponseDTO { IsSuccess = true, Message = "Updated successfully " };
    }
}
