using Microsoft.AspNetCore.Http;
namespace Cupa.MidatR.Players.Commands;
public sealed class UpdateFreePlayerFilesFromProfileCommand(string userid, IFormFile file, FileType type) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public IFormFile File { get; } = file;
    public FileType Type { get; } = type;
}
