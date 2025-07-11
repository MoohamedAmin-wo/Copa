using Humanizer;
namespace Cupa.MidatR.Moderator.Queries.Handlers;
internal sealed class GetFreePlayersForModeratorDashboardQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IRequestHandler<GetFreePlayersForModeratorDashboardQuery, IReadOnlyCollection<FreePlayerForModeratorDashBoardViewModelDTO>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyCollection<FreePlayerForModeratorDashBoardViewModelDTO>> Handle(GetFreePlayersForModeratorDashboardQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(request.UserId);
        if (currentUser is null)
            return null!;


        if (!await _userManager.IsInRoleAsync(currentUser, CupaRoles.Moderator))
            return null!;

        IReadOnlyCollection<Player> players = [];

        switch (request.Status)
        {
            case StatusType.Active:
                players = await _unitOfWork.player.GetAllAsync(
                    includes: i => i.Include(p => p.User).Include(o => o.Position),
                    predicate: x => !x.IsDeleted && !x.IsBinned && x.IsFree,
                    stopTracking: true,
                    take: 0,
                    skip: 0
                    );
                break;
            case StatusType.Deleted:
                players = await _unitOfWork.player.GetAllAsync(
                   includes: i => i.Include(p => p.User).Include(o => o.Position),
                   predicate: x => x.IsDeleted && x.IsFree,
                   stopTracking: true,
                   take: 0,
                   skip: 0
                   );
                break;
            case StatusType.Binned:
                players = await _unitOfWork.player.GetAllAsync(
                   includes: i => i.Include(p => p.User).Include(o => o.Position),
                   predicate: x => x.IsBinned && x.IsFree,
                   stopTracking: true,
                   take: 0,
                   skip: 0
                   );
                break;
        }

        var model = players.Select(x => new FreePlayerForModeratorDashBoardViewModelDTO
        {
            Id = x.Id,
            Age = x.User.Age,
            Fullname = string.Concat(x.User.FirstName, " ", x.User.LastName),
            Nickname = x.NickName,
            Email = x.User.Email,
            Phone = x.User.PhoneNumber,
            Position = x.Position.PositionAPPR,
            CreateOn = x.CreatedOn.ToShortDateString(),
            UpdatedOn = x.UpdatedOn.Humanize(),
            ProfilePictureUrl = x.ProfilePictureUrl,
            Status = x.IsDeleted ? "Deleted" : "Active",
        });

        return [.. model];
    }
}
