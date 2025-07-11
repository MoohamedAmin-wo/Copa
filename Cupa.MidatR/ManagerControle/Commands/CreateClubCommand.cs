using Cupa.MidatR.ManagerControle.Commands.DTOs;

namespace Cupa.MidatR.ManagerControle.Commands
{
    public class CreateClubCommand(string userid, ClubModelDTO model) : IRequest<GlobalResponseDTO>
    {
        public string UserId { get; } = userid;
        public ClubModelDTO Model { get; } = model;
    }
}
