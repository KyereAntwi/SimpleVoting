using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Categories.Commands.AddNomineeToCategory;
using SVoting.Application.Features.Categories.Commands.CreateACategory;
using SVoting.Application.Features.Categories.Queries.GetCategoriesByUser;
using SVoting.Application.Features.Nomnees.Querries.GetNomineesByPollCategory;

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
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		command.Username = userId;

        var result = await _mediator.Send(command);
		return Accepted(result);
	}

	[HttpPost("{categoryId}/AddNominees")]
	public async Task<ActionResult> AddNominneeToCategory([FromRoute]Guid categoryId, [FromBody] AddNomineeToCategoryCommand command)
	{
		var result = await _mediator.Send(command);
		return Accepted(result);
	}

	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCategoriesByUser()
	{
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var result = await _mediator.Send(new GetCategoriesByUserQuery() { UserName = userId });

		return Ok(result);
    }

	[AllowAnonymous]
	[HttpGet("{PollId}/{CategoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetPollCategoryNominees([FromRoute] Guid PollId, [FromRoute] Guid CategoryId)
	{
		var result = await _mediator.Send(new GetNomineesByPollCategoryQuery() { PollId = PollId, CategoryId = CategoryId });
		return Ok(result);
	}

}

