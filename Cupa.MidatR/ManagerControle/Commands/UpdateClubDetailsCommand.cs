using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands;
public sealed class UpdateClubDetailsCommand(string userid, UpdateClubDetailsModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public UpdateClubDetailsModelDTO Model { get; } = model;
}
