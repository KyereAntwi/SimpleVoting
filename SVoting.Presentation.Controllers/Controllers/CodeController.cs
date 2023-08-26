using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Codes.Commands;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/polls/{pollId}/codes/generate")]
[ApiController]
[Authorize]
public class CodeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CodeController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult> GenerateCode([FromRoute] Guid pollId)
	{
		var result = await _mediator.Send(new CreateCodeCommand()
		{
			PollId = pollId
		});

		return Accepted(new {code = result});
	}
}