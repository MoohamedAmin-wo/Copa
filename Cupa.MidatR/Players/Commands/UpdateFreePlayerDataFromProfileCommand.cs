using Cupa.MidatR.Players.Commands.DTOs;
namespace Cupa.MidatR.Players.Commands;
public sealed class UpdateFreePlayerDataFromProfileCommand(string userid, FreePlayerModelDTO model) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public FreePlayerModelDTO Model { get; } = model;
}