using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Polls.Commands.ActivatePoll;
using SVoting.Application.Features.Polls.Commands.AddCategoryToPoll;
using SVoting.Application.Features.Polls.Commands.CreateAPoll;
using SVoting.Application.Features.Polls.Commands.DeletePoll;
using SVoting.Application.Features.Polls.Queries.GetAllPolls;
using SVoting.Application.Features.Polls.Queries.GetPollDetails;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/polls")]
[ApiController]
[Authorize]
public class PollController : ControllerBase
{
    private readonly IMediator _mediator;

    public PollController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllPolls([FromQuery] int page = 1, [FromQuery] int size = 20)
	{
		var results = await _mediator.Send(new GetAllPollsQuery() { Page = page, Size = size });
		return Ok(results);
	}

	[HttpGet("{pollId}")]
	public async Task<ActionResult> GetPollDetails([FromRoute] Guid pollId)
	{
		var results = await _mediator.Send(new GetPollDetailsQuery() { Id = pollId });
		return Ok(results);
	}

	[HttpPost]
	public async Task<ActionResult> CreateAPoll([FromBody] CreateAPollCommand command)
	{
		var result = await _mediator.Send(command);
		return AcceptedAtAction(nameof(GetPollDetails), new { pollId = result.PollDto!.Id }, result);
	}

	[HttpPost("{pollId}/AddCategory")]
	public async Task<ActionResult> AddCategoryToPoll([FromRoute] Guid pollId, AddCategoryToPollCommand command)
	{
		var result = await _mediator.Send(command);
		return Accepted(result);
	}

	[HttpDelete("{pollId}")]
	public async Task<ActionResult> DeletePoll([FromRoute] Guid pollId)
	{
		await _mediator.Send(new PollDeleteCommand() { PollId = pollId });
		return Ok();
	}

	[HttpPut("{pollId}")]
	public async Task<ActionResult> ActivatePoll([FromRoute] Guid pollId)
	{
		await _mediator.Send(new ActivatePollCommand()
		{
			PollId = pollId
		});

		return Ok();
	}
}

