using Microsoft.AspNetCore.Http;

namespace Cupa.MidatR.Common.RequestDTO;

public record FileModelDTO { public IFormFile File { get; set; } }

