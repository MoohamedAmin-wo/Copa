using Humanizer;
namespace Cupa.MidatR.Moderator.Queries.Handlers;
internal sealed class GetAllClubsForModeratorDashBoardQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetAllClubsForModeratorDashBoardQuery, IReadOnlyCollection<ClubDataForModeratorDashBoardViewModelDTO>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<ClubDataForModeratorDashBoardViewModelDTO>> Handle(GetAllClubsForModeratorDashBoardQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;

        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return null!;

        var clubs = await _unitOfWork.clubs.GetAllAsync(
            includes: x => x.Include(i => i.Manager).ThenInclude(u => u.User)
            , skip: 0, take: 0);

        var model = clubs.Select(x => new ClubDataForModeratorDashBoardViewModelDTO
        {
            Id = x.Id,
            ClubName = x.ClubName,
            LogoUrl = x.LogoUrl,
            Manager = string.Concat(x.Manager.User.FirstName, " ", x.Manager.User.LastName),
            Status = x.IsDeleted ? "Deleted" : "Active",
            AdminsCount = x.AdminsCount,
            PlayersCount = x.PlayersCount,
            CreatedOn = x.CreatedOn.ToShortDateString(),
            LastUpdatedOn = x.UpdatedOn.Humanize(),
        });

        return [.. model];
    }
}
