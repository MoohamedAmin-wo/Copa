using Microsoft.AspNetCore.Http;
namespace Cupa.MidatR.Players.Commands;
public sealed class AddNewPictureToPlayerProfilePicturesCollectionCommand(string userid, IFormFile file) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userid;
    public IFormFile File { get; } = file;
}