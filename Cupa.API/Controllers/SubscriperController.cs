namespace Cupa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubscriperController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriperController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Join-As-Manager")]
    public async Task<IActionResult> JoinAsManagerAsync(ManagerModelDTO model)
    {
        var result = await _mediator.Send(new CreateManagerCommand(User.GetUserId(), model));
        return result.IsAuthenticated ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Join-as-player")]
    public async Task<IActionResult> JoinAsPlayerAsync([FromForm] JoinAsPlayerModelDTO model)
    {
        var result = await _mediator.Send(new JoinAsPlayerCommand(User.GetUserId(), model));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
