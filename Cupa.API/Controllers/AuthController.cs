using Cupa.Domain.Enums;

namespace Cupa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequestModelDTO model)
    {
        var result = await _mediator.Send(new RegisterRequestCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("get-token")]
    public async Task<IActionResult> GetTokenAsync(TokenRequestModelDTO model)
    {
        var result = await _mediator.Send(new TokenRequestCommand(model, Response));
        return result.IsAuthenticated ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Contact-Us")]
    public async Task<IActionResult> ContactUsAsync(ContactUsModelDTO model) =>
         Ok(await _mediator.Send(new ContactUsFromUnAuthentacatedUsersCommand(model)));

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmCodeModelDTO model)
    {
        var result = await _mediator.Send(new ConfirmEmailCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> FrogetPasswordAsync(ForgetPasswordModelDTO model)
    {
        var result = await _mediator.Send(new ResetPasswordAfterForgetCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("request-new-code")]
    public async Task<IActionResult> RequestCodeAsync(RequestCodeModelDTO model)
    {
        var result = await _mediator.Send(new RequestCodeCommand(model, CodeType.confirm));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("send-forgetpassword-code")]
    public async Task<IActionResult> SendForgetPasswordCodeAsync(RequestCodeModelDTO model)
    {
        var result = await _mediator.Send(new RequestCodeCommand(model, CodeType.forget));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("confirm-ForgetPassword-code")]
    public async Task<IActionResult> ConfirmForgetPasswordCodeAsync(ConfirmCodeModelDTO model)
    {
        var result = await _mediator.Send(new ConfirmForgetPasswordCodeCommand(model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeTokenAsync(RevokeTokenModelDTO model)
    {
        var result = await _mediator.Send(new RevokeTokenRequestCommand(model, Request));
        return !result ? BadRequest("Invalid token !") : Ok();
    }

    [HttpGet("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var result = await _mediator.Send(new RefreshTokenRequestCommand(Request, Response));
        return !result.IsAuthenticated ? BadRequest(result.Message) : Ok(result);
    }
}
