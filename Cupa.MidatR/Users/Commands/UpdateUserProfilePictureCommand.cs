using Microsoft.AspNetCore.Http;
namespace Cupa.MidatR.Users.Commands;
public sealed class UpdateUserProfilePictureCommand(string userId, IFormFile picture) : IRequest<GlobalResponseDTO>
{
    public string UserId { get; } = userId;
    public IFormFile Picture { get; } = picture;
}
