using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Codes.Commands;
using SVoting.Application.Features.Codes.Queries.VerifyCode;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/codes")]
[ApiController]
[Authorize]
public class CodeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CodeController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[AllowAnonymous]
	[HttpGet("{code}")]
	public async Task<ActionResult> VerifyCode([FromRoute] string code)
	{
		await _mediator.Send(new VerifyCodeQuery(code));
		return Ok(new {Code = code});
	}

	[HttpPost("{pollId}")]
	public async Task<ActionResult> GenerateCode([FromRoute] Guid pollId)
	{
		var result = await _mediator.Send(new CreateCodeCommand()
		{
			PollId = pollId
		});

		return Accepted(new {code = result});
	}
}