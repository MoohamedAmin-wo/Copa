namespace Cupa.MidatR.Users.Queries;
public sealed class GetManagerDetailsForClubPageQuery(int clubid) : IRequest<ManagerDetailsForClubPageQueryModelDTO> { public int ClubId { get; } = clubid; }