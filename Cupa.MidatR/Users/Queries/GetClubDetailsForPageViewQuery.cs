namespace Cupa.MidatR.Users.Queries;
public sealed class GetClubDetailsForPageViewQuery(int clubid) : IRequest<ClubForDetailsPageQueryModelDTO> { public int CludId { get; } = clubid; }