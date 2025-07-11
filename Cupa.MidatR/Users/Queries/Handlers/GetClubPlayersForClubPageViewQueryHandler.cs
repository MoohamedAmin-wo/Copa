namespace Cupa.MidatR.Users.Queries.Handlers;
internal sealed class GetClubPlayersForClubPageViewQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetClubPlayersForClubPageViewQuery, ICollection<ClubPlayerForHomePageModelDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICollection<ClubPlayerForHomePageModelDTO>> Handle(GetClubPlayersForClubPageViewQuery request, CancellationToken cancellationToken)
    {
        var club = await _unitOfWork.clubs.FindSingleAsync(x => x.Id.Equals(request.ClubId));
        if (club is null)
            return null!;

        var players = await _unitOfWork.clubPlayer.FindMultipleAsync(
            predicate: x => x.ClubId.Equals(club.Id),
            includes: i => i.Include(p => p.Player)
            .ThenInclude(u => u.User).Include(m => m.Player).ThenInclude(t => t.Position));

        if (players is null)
            return null!;

        var clubplayers = players.Select(s => new ClubPlayerForHomePageModelDTO
        {
            Id = s.Id.ToString(),
            Age = s.Player.User.Age,
            Nickname = s.Player.NickName,
            Fullname = string.Concat(s.Player.User.FirstName, " ", s.Player.User.LastName),
            ProfilePictureUrl = s.Player.ProfilePictureUrl,
            Position = s.Player.Position.PositionAPPR,
            Views = s.Player.Views
        });

        return [.. clubplayers];
    }
}
