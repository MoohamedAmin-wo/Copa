using Microsoft.AspNetCore.Http;

namespace Cupa.MidatR.ManagerControle.Commands.DTOs
{
    public record ClubModelDTO
    {
        public string clubName { get; set; }
        public string? story { get; set; }
        public string? about { get; set; }
        public IFormFile logo { get; set; }
        public IFormFile MainShirt { get; set; }
        public IFormFile ClubPicture { get; set; }

    }
}
