using Cupa.MidatR.Players.Commands.DTOs;

namespace Cupa.MidatR.Players.Commands;
public sealed class JoinAsPlayerCommand(string userid, JoinAsPlayerModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public JoinAsPlayerModelDTO Model { get; } = model;
}