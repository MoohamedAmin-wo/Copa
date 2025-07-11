using Cupa.Domain.Enums;
using Cupa.MidatR.Dashboard.Queries;

namespace Cupa.API.Controllers;

[Authorize(Roles = CupaRoles.Manager)]
[Route("/api/[controller]")]
[ApiController]
public class ManagersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("create-club")]
    public async Task<IActionResult> CreateClubAsync([FromForm] ClubModelDTO model)
    {
        var result = await _mediator.Send(new CreateClubCommand(User.GetUserId(), model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("update-club")]
    public async Task<IActionResult> UpdateClubAsync(UpdateClubDetailsModelDTO model)
    {
        var response = await _mediator.Send(new UpdateClubDetailsCommand(User.GetUserId(), model));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut("Update-Logo")]
    public async Task<IActionResult> UpdateClubLogoAsync(UpdateClubFilesModelDTO model)
    {
        var response = await _mediator.Send(new UpdateClubFilesCommand(User.GetUserId(), model, ClubFileType.Logo));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut("Update-MainShirt")]
    public async Task<IActionResult> UpdateMainShirtAsync(UpdateClubFilesModelDTO model)
    {
        var response = await _mediator.Send(new UpdateClubFilesCommand(User.GetUserId(), model, ClubFileType.MainShirt));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut("Update-ClubPicture")]
    public async Task<IActionResult> UpdateClubPictureAsync(UpdateClubFilesModelDTO model)
    {
        var response = await _mediator.Send(new UpdateClubFilesCommand(User.GetUserId(), model, ClubFileType.ClubPicture));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("asign-admin")]
    public async Task<IActionResult> AsssignAdminAsync(AdminModelDTO model)
    {
        var result = await _mediator.Send(new AssignAdminCommand(User.GetUserId(), model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("toggle-admin")]
    public async Task<IActionResult> ToggleAdminAsync([FromBody] int Id)
    {
        var result = await _mediator.Send(new ToggleAdminCommand(Id, User.GetUserId()));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }


    [HttpGet("get-Active-Admins")]
    public async Task<IActionResult> GetActiveAdminsAsync()
    {
        var response = await _mediator.Send(new GetAdminsForManagerDashBoardViewQuery(User.GetUserId(), StatusType.Active));
        return Ok(response);
    }

    [HttpGet("get-Banned-Admins")]
    public async Task<IActionResult> GetBanneddAdminsAsync()
    {
        var response = await _mediator.Send(new GetAdminsForManagerDashBoardViewQuery(User.GetUserId(), StatusType.Banned));
        return Ok(response);
    }

    [HttpGet("get-Deleted-Admins")]
    public async Task<IActionResult> GetDeletedAdminsAsync()
    {
        var response = await _mediator.Send(new GetAdminsForManagerDashBoardViewQuery(User.GetUserId(), StatusType.Deleted));
        return Ok(response);
    }

    [HttpGet("get-Select-Admin{id}")]
    public async Task<IActionResult> GetSelectedAdminAsync(int id)
    {
        var response = await _mediator.Send(new GetSelectedAdminForDashBoardQuery(User.GetUserId(), id));
        return Ok(response);
    }

    [HttpDelete("remove-player{id}")]
    public async Task<IActionResult> RemovePlayerAsync(int id)
    {
        var result = await _mediator.Send(new RemovePlayerCommand(id, User.GetUserId()));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("remove-admin{id}")]
    public async Task<IActionResult> RemoveAdminAsync(int id)
    {
        var result = await _mediator.Send(new RemoveAdminCommand(User.GetUserId(), id));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
