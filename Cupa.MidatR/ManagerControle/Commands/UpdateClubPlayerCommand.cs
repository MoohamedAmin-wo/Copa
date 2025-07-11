using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands;
public sealed class UpdateClubPlayerCommand(string userid, int playerid, UpdateClubPlayerModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public int PlayerId { get; } = playerid;
    public UpdateClubPlayerModelDTO Model { get; } = model;
}
