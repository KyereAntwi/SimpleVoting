using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.PollingSpaces.Commands.CreateAPollingSpace;
using SVoting.Application.Features.PollingSpaces.Commands.DisableSpace;
using SVoting.Application.Features.PollingSpaces.Queries.GetAllPollingSpaces;
using SVoting.Application.Features.PollingSpaces.Queries.GetPollingSpaceWithPollsByUserId;
using System.Security.Claims;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/spaces")]
[ApiController]
[Authorize]
public class PollingSpaceController : ControllerBase
{
    private readonly IMediator _mediator;

    public PollingSpaceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<ActionResult> GetPollingSpace()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var pollingSpaceDto = await _mediator.Send(new GetPollingSpaceWithPollsByUserIdQuery()
        {
            UserId = userId
        });

        return Ok(pollingSpaceDto);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetASpaceByUserId([FromRoute] string userId)
    {
        var response = await _mediator.Send(new GetPollingSpaceWithPollsByUserIdQuery()
        {
            UserId = userId
        });

        return Ok(response);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllPollingSpaces([FromQuery] int size = 20, [FromQuery] int page = 1)
    {
        var result = await _mediator.Send(new GetAllPollingSpacesQuery() { Page = page, Size = size});
        return Ok(result);
    }


    [HttpPost()]
    public async Task<ActionResult> CreateASpace([FromBody] CreateAPollingSpaceCommand command)
    {
        var response = await _mediator.Send(command);

        return AcceptedAtAction(nameof(GetASpaceByUserId), 
            new
            {
                userId = response.PollingSpace.UserId
            }, 
            response);
    }


    [HttpPut("{spaceId}")]
    public async Task<ActionResult> DisableAPollingSpace([FromRoute] Guid spaceId)
    {
        var response = await _mediator.Send(new DisableSpaceCommand() { PollingSpaceId = spaceId });
        return Accepted(response);
    }
}
