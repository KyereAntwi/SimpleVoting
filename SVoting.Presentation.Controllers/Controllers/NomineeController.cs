﻿using System.Net;
using System.Net.Http;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVoting.Application.Features.Nominees.Querries.GetAllNominees;
using SVoting.Application.Features.Nominees.Querries.GetNomineeDetails;
using SVoting.Application.Features.Nomnees.Commands.CreateANominee;
using SVoting.Application.Features.Nomnees.Commands.DeleteNominee;
using SVoting.Application.Features.Nomnees.Commands.UpdateNominee;
using SVoting.Application.Features.Nomnees.Querries.GetNomineeCategories;
using SVoting.Application.Features.Nomnees.Querries.GetNomineesByUser;
using SVoting.Application.Features.Nomnees.Querries.GetPhotogrph;

namespace SVoting.Presentation.Controllers.Controllers;

[Route("api/nominees")]
[ApiController]
[Authorize]
public class NomineeController : ControllerBase
{
    private readonly IMediator _mediator;

    public NomineeController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllNominees([FromQuery] int page = 1, [FromQuery] int size = 20)
	{
		var result = await _mediator.Send(new GetAllNomineesQuery()
		{
			Page = page,
			Size =size
		});

		return Ok(result);
	}

	[HttpGet("{nomineeId}/Categories")]
	public async Task<ActionResult> GetNomineeCategoreis([FromRoute] Guid nomineeId)
	{
		var result = await _mediator.Send(new GetNomineeCategoriesQuery() { NomineeId = nomineeId });
		return Ok(result);
	}

	[HttpGet("{nomineeId}")]
	public async Task<ActionResult> GetNomineeDetials([FromRoute] Guid nomineeId)
	{
		var result = await _mediator.Send(new GetNomineeDetailsQuery() { NomineeId = nomineeId });
		return Ok(result);
	}

	[AllowAnonymous]
	[HttpGet("{nomineeId}/photograph")]
	public async Task<ActionResult> GetNomineePhotographUrl([FromRoute] Guid nomineeId)
	{
		var result = await _mediator.Send(new GetNomineePhotographQuery() { NomineeId = nomineeId });
		return Ok(result);
	}

	[HttpGet("all/byuser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllNomineesByUser()
	{
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		var result = await _mediator.Send(new GetNomineesByUserQuery() { UserName = userId });
		return Ok(result);
    }

	[HttpPost]
	public async Task<ActionResult> CreateANominee([FromBody] CreateANomineeCommand command)
	{
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        command.UserName = userId;
        var result = await _mediator.Send(command);

		return AcceptedAtAction(nameof(GetNomineeDetials), new
		{
			nomineeId = result.NomineeDto?.Id
		}, result);
	}

	[HttpDelete("{nomineeId}")]
	public async Task<ActionResult> DeleteNominee([FromRoute] Guid nomineeId)
	{
		var result = await _mediator.Send(new DeleteNomineeCommand()
		{
			NomineeId = nomineeId
		});

		return Ok(result);
	}

	[HttpPut("{nomineeId}")]
	public async Task<ActionResult> UpdateNominee([FromRoute] Guid nomineeId, [FromBody] UpdateNomineeCommand command)
	{
		var result = await _mediator.Send(command);
		return Accepted(result);
	}
}

