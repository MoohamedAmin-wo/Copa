namespace Cupa.MidatR.Users.Queries;
public sealed class GetClubPlayersForClubPageViewQuery(int clubid) : IRequest<ICollection<ClubPlayerForHomePageModelDTO>> { public int ClubId { get; } = clubid; }