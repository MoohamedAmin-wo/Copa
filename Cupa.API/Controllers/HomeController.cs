namespace Cupa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get-clubs-home")]
    public async Task<IActionResult> GetClubsForHomePageViewAsync()
    {
        var result = await _mediator.Send(new GetClubForHomePageViewQuery(true));
        return Ok(result);
    }

    [HttpGet("get-clubs-page")]
    public async Task<IActionResult> GetClubsPageForHomePageViewAsync()
    {
        var result = await _mediator.Send(new GetClubForHomePageViewQuery(false));
        return Ok(result);
    }


    [HttpGet("get-club-players-home")]
    public async Task<IActionResult> GetClubPlayersForHomePageViewAsync()
    {
        var result = await _mediator.Send(new GetTopRatedClubPlayersForHomePageViewQuery());
        return Ok(result);
    }
    [HttpGet("get-club-Players-Page")]
    public async Task<IActionResult> GetAllClubPlayersForUserViewPageAsync()
    {
        var response = await _mediator.Send(new GetTopRatedClubPlayersForHomePageViewQuery(false));
        return Ok(response);
    }

    [HttpGet("get-free-players-home")]
    public async Task<IActionResult> GetFreePlayersForHomePageViewAsync()
    {
        var result = await _mediator.Send(new GetTopRatedFreePlayersForHomePageViewQuery());
        return Ok(result);
    }

    [HttpGet("get-Free-Players-Page")]
    public async Task<IActionResult> GetAllFreePlayersForUserViewPageAsync()
    {
        var response = await _mediator.Send(new GetTopRatedFreePlayersForHomePageViewQuery(false));
        return Ok(response);
    }

    [HttpGet("get-successStories")]
    public async Task<IActionResult> GetSuccessStoriesForHomePageViewAsync()
    {
        var result = await _mediator.Send(new GetSuccessStoriesPageViewQuery());
        return Ok(result);
    }
}
