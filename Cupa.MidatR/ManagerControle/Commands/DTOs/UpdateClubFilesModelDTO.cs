using Microsoft.AspNetCore.Http;

namespace Cupa.MidatR.ManagerControle.Commands.DTOs;
public sealed record UpdateClubFilesModelDTO
{
    public IFormFile File { get; set; }
}
