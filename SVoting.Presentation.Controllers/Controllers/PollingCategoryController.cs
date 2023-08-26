using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Categories.Commands.AddNomineeToCategory;
using SVoting.Application.Features.Categories.Commands.CreateACategory;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/categories")]
[ApiController]
[Authorize]
public class PollingCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PollingCategoryController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult> CreateACategory([FromBody] CreateACategoryCommand command)
	{
		var result = await _mediator.Send(command);
		return Accepted(result);
	}

	[HttpPost("{categoryId}/AddNominees")]
	public async Task<ActionResult> AddNominneeToCategory([FromRoute]Guid categoryId, [FromBody] AddNomineeToCategoryCommand command)
	{
		var result = await _mediator.Send(command);
		return Accepted(result);
	}

}

