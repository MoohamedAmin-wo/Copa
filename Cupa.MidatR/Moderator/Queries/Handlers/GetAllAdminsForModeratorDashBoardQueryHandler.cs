using Humanizer;

namespace Cupa.MidatR.Moderator.Queries.Handlers;

internal sealed class GetAllAdminsForModeratorDashBoardQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetAllAdminsForModeratorDashBoardQuery, ICollection<UserDataForModeratorDashBoardViewModelDTO>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICollection<UserDataForModeratorDashBoardViewModelDTO>> Handle(GetAllAdminsForModeratorDashBoardQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return null!;

        var adminsQuery = await _unitOfWork.admins.GetAllAsync(
        predicate: x => x.UserId != null,
        includes: x => x.Include(u => u.User),
        stopTracking: true,
        skip: 0,
        take: 0);

        var model = adminsQuery.Select(x => new UserDataForModeratorDashBoardViewModelDTO
        {
            Id = x.Id.ToString(),
            Fullname = string.Concat(x.User.FirstName, " ", x.User.LastName),
            Email = x.User.Email!,
            Phone = x.User.PhoneNumber ?? "Phone not set",
            DateOfBirth = x.User.BirthDate.ToShortDateString(),
            Username = x.User.UserName!,
            EmailConfirmed = x.User.EmailConfirmed ? "Confirmed" : "Not confirmed",
            CreateOn = x.User.CreatedOn.ToShortDateString(),
            Status = x.User.IsDeleted ? "Deleted" : "Active",
            UpdateOn = x.User.UpdatedOn.Humanize()
        });

        return [.. model];
    }
}