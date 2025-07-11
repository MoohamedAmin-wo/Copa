namespace Cupa.MidatR.Moderator.Queries;
public sealed class GetSelectedBinnedPlayerRequestForMedratorOverViewQuery(string userid , int playerid) : IRequest<BinnedFreePlayerRequestForModeratorViewModelDTO>
{
    public string UserId { get; } = userid;
    public int PlayerId { get; } = playerid;
}
