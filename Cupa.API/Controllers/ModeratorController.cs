using Cupa.Domain.Enums;
using Cupa.MidatR.Moderator.Commands;
using Cupa.MidatR.Moderator.Queries;

namespace Cupa.API.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = CupaRoles.Moderator)]
[ApiController]
public class ModeratorController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("Block-User{id}")]
    public async Task<IActionResult> BlockAnyApplicationUserAsync(string id)
        => Ok(await _mediator.Send(new ToggleAnyApplicationUserStatusCommand(User.GetUserId(), id, ToggleRequestType.Block)));

    [HttpPost("Toggle-User{id}")]
    public async Task<IActionResult> ToggleAnyApplicationUserStatusAsync(string id)
        => Ok(await _mediator.Send(new ToggleAnyApplicationUserStatusCommand(User.GetUserId(), id, ToggleRequestType.Deleted)));

    [HttpPost("Accept-Player{id}")]
    public async Task<IActionResult> AcceptPlayerRequestAsync(int id)
        => Ok(await _mediator.Send(new AcceptFreePlayerRequestCommand(id, User.GetUserId(), ApprovalType.Accept)));

    [HttpPost("Decline-Player{id}")]
    public async Task<IActionResult> DeclinePlayerRequestAsync(int id)
        => Ok(await _mediator.Send(new AcceptFreePlayerRequestCommand(id, User.GetUserId(), ApprovalType.Decline)));

    [HttpPost("Reset-Password{id}")]
    public async Task<IActionResult> ResetPasswordForAnyApplicationUserAsync(string id)
        => Ok(await _mediator.Send(new ResetPasswordForAnyApplicationUserCommand(id, User.GetUserId())));

    [HttpGet("Get-Count")]
    public async Task<IActionResult> GetAllDataCountAsync()
        => Ok(await _mediator.Send(new GetElementsCountForModeratorDashBoardCommand(User.GetUserId())));

    [HttpGet("Get-All-Clubs")]
    public async Task<IActionResult> GetAllClubsAsync()
        => Ok(await _mediator.Send(new GetAllClubsForModeratorDashBoardQuery(User.GetUserId())));

    [HttpGet("Get-All-Users")]
    public async Task<IActionResult> GetAllUsersAsync()
        => Ok(await _mediator.Send(new GetAllUsersForModeratorDashBoardQuery(User.GetUserId(), StatusType.Active)));

    [HttpGet("Get-All-Admins")]
    public async Task<IActionResult> GetAllAdminsAsync()
        => Ok(await _mediator.Send(new GetAllAdminsForModeratorDashBoardQuery(User.GetUserId())));

    [HttpGet("Get-All-Managers")]
    public async Task<IActionResult> GetAllManagersAsync()
        => Ok(await _mediator.Send(new GetAllManagersForModeratorDashBoardQuery(User.GetUserId())));

    [HttpGet("Get-Blocked-Users")]
    public async Task<IActionResult> GetBlockedUsersAsync()
        => Ok(await _mediator.Send(new GetAllUsersForModeratorDashBoardQuery(User.GetUserId(), StatusType.Banned)));

    [HttpGet("Get-Active-FreePlayers")]
    public async Task<IActionResult> GetActiveFreePlayersAsync()
        => Ok(await _mediator.Send(new GetFreePlayersForModeratorDashboardQuery(User.GetUserId(), StatusType.Active)));

    [HttpGet("Get-Deleted-FreePlayers")]
    public async Task<IActionResult> GetDeletedFreePlayersAsync()
        => Ok(await _mediator.Send(new GetFreePlayersForModeratorDashboardQuery(User.GetUserId(), StatusType.Deleted)));

    [HttpGet("Get-Binned-FreePlayers")]
    public async Task<IActionResult> GetBinnedFreePlayersAsync()
        => Ok(await _mediator.Send(new GetFreePlayersForModeratorDashboardQuery(User.GetUserId(), StatusType.Binned)));

    [HttpGet("Get-Selected-Binned-FreePlayer{id}")]
    public async Task<IActionResult> GetSelectedBinnedFreePlayerAsync(int id)
        => Ok(await _mediator.Send(new GetSelectedBinnedPlayerRequestForMedratorOverViewQuery(User.GetUserId() , id)));

}