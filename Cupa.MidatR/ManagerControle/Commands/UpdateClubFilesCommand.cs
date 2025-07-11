using Cupa.MidatR.ManagerControle.Commands.DTOs;
namespace Cupa.MidatR.ManagerControle.Commands;
public sealed class UpdateClubFilesCommand(string userid, UpdateClubFilesModelDTO model, ClubFileType fileType) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public UpdateClubFilesModelDTO Model { get; } = model;
    public ClubFileType FileType { get; } = fileType;
}
