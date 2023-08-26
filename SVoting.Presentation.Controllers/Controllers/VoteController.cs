using MediatR;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Votes.Commands.Vote;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/votes")]
public class VoteController : ControllerBase
{
    private readonly IMediator _mediator;

    public VoteController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult> PerformAVote([FromBody] VoteCommand command)
	{
		var result = await _mediator.Send(command);
		return Accepted(result);
	}
}

