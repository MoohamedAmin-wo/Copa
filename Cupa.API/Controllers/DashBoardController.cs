using Cupa.Domain.Enums;
using Cupa.MidatR.Dashboard.Queries;

namespace Cupa.API.Controllers
{
    [Authorize(Roles = $"{CupaRoles.Manager},{CupaRoles.Admin}")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create-player")]
        public async Task<IActionResult> CreatePlayerAsync([FromForm] PlayerModelDTO model)
        {
            var result = await _mediator.Send(new CreatePlayerCommand(User.GetUserId(), model));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("toggle-player")]
        public async Task<IActionResult> TogglePlayerAsync([FromBody] int Id)
        {
            var result = await _mediator.Send(new TogglePlayerCommand(User.GetUserId(), Id));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("assign-player")]
        public async Task<IActionResult> AssignPlayerAsync([FromBody] int Id)
        {
            var result = await _mediator.Send(new AssignPlayerCommand(Id, User.GetUserId()));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("Update-Player{id}")]
        public async Task<IActionResult> UpdateClubPlayerAsync(int id, UpdateClubPlayerModelDTO model)
        {
            var response = await _mediator.Send(new UpdateClubPlayerCommand(User.GetUserId(), id, model));
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-Club-Details")]
        public async Task<IActionResult> GetClubDetailsAsync()
        {
            var response = await _mediator.Send(new GetClubDetailsForManagerDashBoardViewQuery(User.GetUserId()));
            return Ok(response);
        }

        [HttpGet("get-Select-player{id}")]
        public async Task<IActionResult> GetSelectedPlayerAsync(int id)
        {
            var response = await _mediator.Send(new GetSelectedPlayerForDashBoardQuery(User.GetUserId(), id));
            return Ok(response);
        }

        [HttpGet("get-Active-Players")]
        public async Task<IActionResult> GetActiveClubPlayersAsync()
        {
            var response = await _mediator.Send(new GetClubPlayersForDashBoardViewQuery(User.GetUserId(), StatusType.Active));
            return Ok(response);
        }

        [HttpGet("get-Deleted-Players")]
        public async Task<IActionResult> GetDeletedClubPlayersAsync()
        {
            var response = await _mediator.Send(new GetClubPlayersForDashBoardViewQuery(User.GetUserId(), StatusType.Deleted));
            return Ok(response);
        }

        [HttpGet("get-Banned-Players")]
        public async Task<IActionResult> GetBannedClubPlayersAsync()
        {
            var response = await _mediator.Send(new GetClubPlayersForDashBoardViewQuery(User.GetUserId(), StatusType.Banned));
            return Ok(response);
        }
    }
}
