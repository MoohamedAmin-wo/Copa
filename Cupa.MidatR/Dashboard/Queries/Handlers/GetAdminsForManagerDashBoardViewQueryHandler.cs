using Humanizer;

namespace Cupa.MidatR.Dashboard.Queries.Handlers;
internal sealed class GetAdminsForManagerDashBoardViewQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetAdminsForManagerDashBoardViewQuery, ICollection<AdminForDashBoardViewModelDTO>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICollection<AdminForDashBoardViewModelDTO>> Handle(GetAdminsForManagerDashBoardViewQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return null!;

        var manager = await _unitOfWork.managers.FindSingleAsync(x => x.UserId.Equals(user.Id));
        if (manager is null)
            return null!;

        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.ManagerId.Equals(manager.Id));
        if (club is null)
            return null!;

        IReadOnlyCollection<Admin> admins = [];

        switch (request.Status)
        {
            case StatusType.Active:
                admins = await _unitOfWork.admins.GetAllAsync(
                    includes: i => i.Include(u => u.User),
                    predicate: x => !x.User.IsBlocked && !x.User.IsDeleted && x.ClubId.Equals(club.Id),
                    stopTracking: true,
                    take: 0);
                break;
            case StatusType.Deleted:
                admins = await _unitOfWork.admins.GetAllAsync(
                   includes: i => i.Include(u => u.User),
                   predicate: x => x.User.IsDeleted && x.ClubId.Equals(club.Id),
                   stopTracking: true,
                   take: 0);
                break;
            case StatusType.Banned:
                admins = await _unitOfWork.admins.GetAllAsync(
                   includes: i => i.Include(u => u.User),
                   predicate: x => x.User.IsBlocked && x.ClubId.Equals(club.Id),
                   stopTracking: true,
                   take: 0);
                break;
        }

        var returnedModel = admins.Select(x => new AdminForDashBoardViewModelDTO
        {
            Id = x.Id,
            Fullname = string.Concat(x.User.FirstName, " ", x.User.LastName),
            Username = x.User.UserName,
            Email = x.User.Email,
            Age = x.User.Age,
            Phonenumber = x.User.PhoneNumber ?? "Phone not set",
            JoinedAsAdminFrom = x.JoinedAsAdminOn.Humanize(),
            Status = x.User.IsDeleted ? "Deleted" : "Active",
        });

        return [.. returnedModel];
    }
}
