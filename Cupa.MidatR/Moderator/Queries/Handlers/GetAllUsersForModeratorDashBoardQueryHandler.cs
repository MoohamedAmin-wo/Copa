using Humanizer;
namespace Cupa.MidatR.Moderator.Queries.Handlers;
internal sealed class GetAllUsersForModeratorDashBoardQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersForModeratorDashBoardQuery, IReadOnlyCollection<UserDataForModeratorDashBoardViewModelDTO>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IReadOnlyCollection<UserDataForModeratorDashBoardViewModelDTO>> Handle(GetAllUsersForModeratorDashBoardQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return null!;

        IReadOnlyCollection<ApplicationUser> Users = [];
        if (request.Type.Equals(StatusType.Active))
        {
            Users = await _unitOfWork.users.GetAllAsync(skip: 0, take: 0, stopTracking: true, predicate: x => !x.IsBlocked);
        }
        else
        {
            Users = await _unitOfWork.users.GetAllAsync(skip: 0, take: 0, stopTracking: true, predicate: x => x.IsBlocked);
        }

        var model = Users.Select(x => new UserDataForModeratorDashBoardViewModelDTO
        {
            Id = x.Id,
            Fullname = string.Concat(x.FirstName, " ", x.LastName),
            Email = x.Email,
            Phone = x.PhoneNumber,
            Username = x.UserName,
            EmailConfirmed = x.EmailConfirmed ? "Confirmed" : "Not Confirmed",
            Status = x.IsDeleted ? "Deleted" : "Active",
            CreateOn = x.CreatedOn.ToShortDateString(),
            UpdateOn = x.UpdatedOn.Humanize(),
            DateOfBirth = x.BirthDate.ToShortDateString() ?? "Birth Date not set",
        });

        return [.. model];
    }
}