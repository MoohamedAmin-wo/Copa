namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetCurrentUserDataQueryHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<GetCurrentUserDataQuery, GetCurrentUserDataModelDTO>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<GetCurrentUserDataModelDTO> Handle(GetCurrentUserDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return null!;

        var userroles = await _userManager.GetRolesAsync(user);
        if (userroles is null)
            return null!;


        return new GetCurrentUserDataModelDTO
        {
            Id = user.Id,
            Firstname = user.FirstName,
            Lastname = user.LastName,
            Username = user.UserName,
            Email = user.Email,
            PictureThumbnailUrl = user.ProfilePictureThumbnailUrl,
            PictureUrl = user.ProfilePictureUrl,
            UserRoles = userroles.ToList()
        };
    }
}