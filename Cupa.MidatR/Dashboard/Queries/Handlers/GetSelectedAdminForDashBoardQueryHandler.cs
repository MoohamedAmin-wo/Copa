using Humanizer;

namespace Cupa.MidatR.Dashboard.Queries.Handlers;
internal sealed class GetSelectedAdminForDashBoardQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : IRequestHandler<GetSelectedAdminForDashBoardQuery, AdminViewForManagerModelDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<AdminViewForManagerModelDTO> Handle(GetSelectedAdminForDashBoardQuery request, CancellationToken cancellationToken)
    {
        var CurrentUser = await _userManager.FindByNameAsync(request.UserId);
        if (CurrentUser is null)
            return null!;

        if (!await _userManager.IsInRoleAsync(CurrentUser, CupaRoles.Manager))
            return null!;

        var admin = await _unitOfWork.admins.FindSingleAsync(x => x.Id.Equals(request.AdminId));
        if (admin is null)
            return null!;

        var CurrentAdmin = await _userManager.FindByIdAsync(admin.UserId);
        if (CurrentAdmin is null)
            return null!;

        var model = new AdminViewForManagerModelDTO
        {
            Fullname = string.Concat(CurrentAdmin.FirstName, " ", CurrentAdmin.LastName),
            Username = CurrentAdmin.UserName,
            Age = CurrentAdmin.Age,
            Email = CurrentAdmin.Email,
            Phonenumber = CurrentAdmin.PhoneNumber ?? "Phone not set ",
            JoinedAsAdminFrom = admin.JoinedAsAdminOn.Humanize(),
            pictureUrl = CurrentAdmin.ProfilePictureUrl,
            pictureThumbnialUrl = CurrentAdmin.ProfilePictureThumbnailUrl,
            Status = CurrentAdmin.IsDeleted ? "Deleted " : "Active",
            DateOfBirth = CurrentAdmin.BirthDate.ToString() ?? "Date of Birth not set",
        };

        var adminAddress = await _unitOfWork.address.FindSingleAsync(x => x.UserId.Equals(CurrentAdmin.Id));
        if (adminAddress is null)
        {
            model.Address = "address not set !";
        }
        else
        {
            model.Address = adminAddress.ToString();
        }

        return model;
    }
}
