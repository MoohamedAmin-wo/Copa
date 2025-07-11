using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands
{
    public sealed class CreatePlayerCommand(string userid, PlayerModelDTO model) : IRequest<GlobalResponseDTO>
    {
        public string UserId { get; } = userid;
        public PlayerModelDTO Model { get; } = model;
    }
}
