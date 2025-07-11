using Cupa.Domain.Enums;
using Cupa.MidatR.Common.RequestDTO;
using Cupa.MidatR.Players.Queries;
namespace Cupa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = CupaRoles.Player)]
    public class PlayerController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("Get-Player-Data")]
        public async Task<IActionResult> GetCurrentPlayerDataAsync()
            => Ok(await _mediator.Send(new GetFreePlayerDataForProfileViewQuery(User.GetUserId()))) ;

        [HttpPost("Upload-picture")]
        public async Task<IActionResult> UploadPictureToPlayerCollectionAsync(IFormFile file)
        {
            var response = await _mediator.Send(new AddNewPictureToPlayerProfilePicturesCollectionCommand(User.GetUserId(), file));
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Update-Player-Data")]
        public async Task<IActionResult> UpdateFreePlayerDataAsync(FreePlayerModelDTO model)
        {
            var response = await _mediator.Send(new UpdateFreePlayerDataFromProfileCommand(User.GetUserId(), model));
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Update-Player-Video")]
        public async Task<IActionResult> UpdateFreePlayerVideoAsync(IFormFile file)
        {
            var response = await _mediator.Send(new UpdateFreePlayerFilesFromProfileCommand(User.GetUserId(), file, FileType.video));
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Update-Player-Picture")]
        public async Task<IActionResult> UpdateFreePlayerPictureAsync(IFormFile file)
        {
            var response = await _mediator.Send(new UpdateFreePlayerFilesFromProfileCommand(User.GetUserId(), file, FileType.picture));
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
