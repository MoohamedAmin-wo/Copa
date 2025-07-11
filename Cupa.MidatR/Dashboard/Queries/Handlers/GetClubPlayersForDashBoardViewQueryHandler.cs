using Cupa.Application.Services.ClubServices;
using Humanizer;

namespace Cupa.MidatR.Dashboard.Queries.Handlers;
internal sealed class GetClubPlayersForDashBoardViewQueryHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IClubManagmentServices clubManagmentServices) : IRequestHandler<GetClubPlayersForDashBoardViewQuery, ICollection<ClubPlayerForDashBoardViewModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IClubManagmentServices _clubManagmentServices = clubManagmentServices;
    public async Task<ICollection<ClubPlayerForDashBoardViewModelDTO>> Handle(GetClubPlayersForDashBoardViewQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user is null)
            return null!;

        var club = await _clubManagmentServices.GetCurrentClubAsync(user);
        if (club is null)
            return null!;

        IReadOnlyCollection<ClubPlayer> players = [];

        switch (request.Status)
        {
            case StatusType.Active:
                players = await _unitOfWork.clubPlayer.GetAllAsync(
                    includes: i => i.Include(p => p.Player).Include(o => o.Player.Position).Include(u => u.Player.User),
                    predicate: x => !x.Player.IsDeleted && !x.Player.IsFree && x.ClubId.Equals(club.Id),
                    stopTracking: true,
                    take: 0,
                    skip: 0
                    );
                break;
            case StatusType.Deleted:
                players = await _unitOfWork.clubPlayer.GetAllAsync(
                   includes: i => i.Include(p => p.Player).Include(o => o.Player.Position).Include(u => u.Player.User),
                   predicate: x => x.Player.IsDeleted && !x.Player.IsFree && x.ClubId.Equals(club.Id),
                   stopTracking: true,
                   take: 0,
                   skip: 0
                   );
                break;
            case StatusType.Banned:
                players = await _unitOfWork.clubPlayer.GetAllAsync(
                   includes: i => i.Include(p => p.Player).Include(o => o.Player.Position).Include(u => u.Player.User),
                   predicate: x => x.Player.User.IsBlocked && !x.Player.IsFree && x.ClubId.Equals(club.Id),
                   stopTracking: true,
                   take: 0,
                   skip: 0
                   );
                break;
        }

        var returnedModel = players.Select(m => new ClubPlayerForDashBoardViewModelDTO
        {
            Id = m.PlayerId,
            Fullname = string.Concat(m.Player.User.FirstName, " ", m.Player.User.LastName),
            Email = m.Player.User.Email,
            Phone = m.Player.User.PhoneNumber,
            Number = m.Number,
            ProfilePictureUrl = m.Player.ProfilePictureUrl,
            Position = m.Player.Position.PositionAPPR,
            ContratctDuration = m.ContractDuration.Humanize(),
            Status = m.Player.IsDeleted ? "Deleted" : "Active",
            Age = m.Player.User.Age
        });

        return [.. returnedModel];
    }
}
