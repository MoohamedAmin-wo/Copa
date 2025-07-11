using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands
{
    public class AssignAdminCommand(string userid, AdminModelDTO model) : IRequest<GlobalResponseDTO>
    {
        public AdminModelDTO Model { get; } = model;
        public string UserId { get; } = userid;
    }
}
