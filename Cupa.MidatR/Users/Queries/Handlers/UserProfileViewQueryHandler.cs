namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class UserProfileViewQueryHandler(IUnitOfWork uintOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<UserProfileViewQuery, UserProfileModelDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = uintOfWork;
    public async Task<UserProfileModelDTO> Handle(UserProfileViewQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return null!;

        var userAddress = await _unitOfWork.address.FindSingleAsync(x => x.UserId.Equals(user.Id));

        var model = new UserProfileModelDTO
        {
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Age = user.Age,
            DateOfBirth = user.BirthDate,
            Username = user.UserName!,
            Email = user.Email!,
            JoinedUsOn = user.CreatedOn.ToShortDateString(),
            ProfilePictureUrl = user.ProfilePictureUrl!,
            ProfilePictureThumbnailUrl = user.ProfilePictureThumbnailUrl ?? CupaDefaults.DefaultProfilePicture,
            PhoneNumber = user.PhoneNumber ?? "Phone number not set "
        };

        if (userAddress is null)
        {
            model.Address = "Address not set ";
        }
        else
        {
            model.Address = userAddress.ToString();
        }
        user.LastUpdateForPassword ??= model.LastUpdateForPassword;

        return model;
    }
}