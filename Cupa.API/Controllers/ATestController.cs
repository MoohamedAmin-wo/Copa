using Cupa.Application.Services.Auth;
using Cupa.Domain.Entities;
using Cupa.MidatR.Dashboard.Queries;
using Microsoft.AspNetCore.Identity;

namespace Cupa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ATestController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ATestController(IMediator mediator, UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _authService = authService;
    }

    [HttpGet("Get-Positions-As-SelectListItem")]
    public async Task<IActionResult> GetPositionsAsync()
    {
        return Ok(await _mediator.Send(new GetPositionsAsSelectListItemsQuery()));
    }

    [HttpPost("confirmEmailsForModerators")]
    public async Task<IActionResult> ConfirmEmailsFroModerators(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return NotFound("User not found !");

        if (user.EmailConfirmed)
            return BadRequest("this email is already confirmed !");

        user.EmailConfirmed = true;
        var result = await _userManager.AddToRoleAsync(user, CupaRoles.User);

        if (!result.Succeeded)
            return BadRequest("Faild to assign Role !");

        return Ok("Email confirmed Successfully !");
    }

    [HttpPost("Get-Moderator-Role")]
    public async Task<IActionResult> GetModeratorRole(string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest("enter a valid email !");

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return NotFound("no user found !");

        if (await _userManager.IsInRoleAsync(user, CupaRoles.Moderator))
            return BadRequest("User already has this role ");

        if (await _authService.CheckUserRolesAsync(user, [CupaRoles.Player , CupaRoles.Admin , CupaRoles.Manager]))
            return BadRequest("this user can't own this role !");

        var addRoleResult = await _userManager.AddToRoleAsync(user , CupaRoles.Moderator);
        if (!addRoleResult.Succeeded)
            return BadRequest(string.Join(" " , addRoleResult.Errors.Select(x => x.Description)));

        return Ok("Role assigned successfully ");
    }

    //[HttpPost("Get-userRole")]
    //public async Task<IActionResult> GetUserRole(string email )
    //{
    //    if (string.IsNullOrEmpty(email))
    //        return BadRequest("enter a valid email !");

    //    var user = await _userManager.FindByEmailAsync(email);
    //    if (user is null)
    //        return NotFound("no user found !");

    //    if (await _userManager.IsInRoleAsync(user, CupaRoles.Moderator))
    //        return BadRequest("User already has this role ");

    //    var addRoleResult = await _userManager.AddToRoleAsync(user, CupaRoles.User);
    //    if (!addRoleResult.Succeeded)
    //        return BadRequest(string.Join(" ", addRoleResult.Errors.Select(x => x.Description)));

    //    return Ok("Role assigned successfully ");
    //}


    //[HttpPost("Get-PlayerRole")]
    //public async Task<IActionResult> GetPlayerRole(string email)
    //{
    //    if (string.IsNullOrEmpty(email))
    //        return BadRequest("enter a valid email !");

    //    var user = await _userManager.FindByEmailAsync(email);
    //    if (user is null)
    //        return NotFound("no user found !");

    //    if (await _userManager.IsInRoleAsync(user, CupaRoles.Moderator))
    //        return BadRequest("User already has this role ");

    //    if (await _authService.CheckUserRolesAsync(user, [CupaRoles.Moderator, CupaRoles.Admin, CupaRoles.Manager]))
    //        return BadRequest("this user can't own this role !");

    //    var addRoleResult = await _userManager.AddToRoleAsync(user, CupaRoles.Player);
    //    if (!addRoleResult.Succeeded)
    //        return BadRequest(string.Join(" ", addRoleResult.Errors.Select(x => x.Description)));

    //    return Ok("Role assigned successfully ");
    //}
}
