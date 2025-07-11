using Cupa.MidatR.Users.Commands.DTOs;
namespace Cupa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("Update-password")]
    public async Task<IActionResult> UpdateUserPasswordAsync(UpdatePasswordModelDTO model)
    {
        var response = await _mediator.Send(new UpdateUserPasswordCommand(User.GetUserId(), model));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("Update-Picture")]
    public async Task<IActionResult> UpdateProfilePictureAsync(IFormFile picture)
    {
        var result = await _mediator.Send(new UpdateUserProfilePictureCommand(User.GetUserId(), picture));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Update-Phone")]
    public async Task<IActionResult> UpdatePhoneNumberAsync([FromBody] string Phonenumber)
    {
        var response = await _mediator.Send(new UpdateUserPhoneNumberCommand(User.GetUserId(), Phonenumber));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("Update-Name")]
    public async Task<IActionResult> UpdateFullnameAsync(UpdateNameModelDTO model)
    {
        var response = await _mediator.Send(new UpdateUserFullnameCommand(User.GetUserId(), model));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("Modify-address")]
    public async Task<IActionResult> ModifyUserAddressAsync(UserAddressModelDTO model)
    {
        var response = await _mediator.Send(new UpdateUserAddressCommand(User.GetUserId(), model));
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("Get-User-Data")]
    public async Task<IActionResult> GetCurrentUserDataAsync()
    {
        var result = await _mediator.Send(new GetCurrentUserDataQuery(User.GetUserId()));
        return Ok(result);
    }

    [HttpGet("View-User-profile")]
    public async Task<IActionResult> ViewUserProfileAsync()
    {
        var result = await _mediator.Send(new UserProfileViewQuery(User.GetUserId()));
        return Ok(result);
    }

    [HttpGet("get-player{id}")]
    public async Task<IActionResult> GetSelectedPlayerAsync(int id)
    {
        var response = await _mediator.Send(new GetSelectedPlayerFromUserQuery(User.GetUserId(), id));
        return Ok(response);
    }

    [HttpGet("get-Club-details{id}")]
    public async Task<IActionResult> GetClubDetailsAsync(int id)
    {
        var result = await _mediator.Send(new GetClubDetailsForPageViewQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("get-Manager-For-Clubdetails{id}")]
    public async Task<IActionResult> GetManagersForClubDetailsAsync(int id)
    {
        var result = await _mediator.Send(new GetManagerDetailsForClubPageQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("get-ClubPlayers-For-Clubdetails{id}")]
    public async Task<IActionResult> GetClubPlayersForClubDetailsAsync(int id)
    {
        var result = await _mediator.Send(new GetClubPlayersForClubPageViewQuery(id));
        return result is null ? NotFound() : Ok(result);
    }
}