using Microsoft.AspNetCore.Http;

namespace Cupa.MidatR.Players.Commands.DTOs;
public record JoinAsPlayerModelDTO
{
    public string NickName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string StoryAbout { get; set; } = null!;
    public string PreefAbout { get; set; } = null!;
    public string PositionId { get; set; }
    public IFormFile? Picture { get; set; }
    public IFormFile? Video { get; set; }
}
