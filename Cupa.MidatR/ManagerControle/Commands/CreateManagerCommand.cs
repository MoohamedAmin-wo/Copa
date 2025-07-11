using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands
{
    public class CreateManagerCommand(string userid, ManagerModelDTO model) : IRequest<AuthResponse>
    {
        public ManagerModelDTO Model { get; } = model;
        public string UserId { get; } = userid;
    }
}
