using Cupa.MidatR.Players.Queries.DTOs;
namespace Cupa.MidatR.Players.Queries;
public sealed class GetFreePlayerDataForProfileViewQuery(string userid) : IRequest<FreePlayerObjectForProfileModelDTO>
{
    public string UserId { get; } = userid;
}