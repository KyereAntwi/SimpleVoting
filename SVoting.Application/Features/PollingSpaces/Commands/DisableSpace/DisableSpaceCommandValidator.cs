using System;
using FluentValidation;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.PollingSpaces.Commands.DisableSpace;

public class DisableSpaceCommandValidator : AbstractValidator<DisableSpaceCommand>
{
    private readonly IPollingSpaceRepository _asyncRepository;

    public DisableSpaceCommandValidator(IPollingSpaceRepository asyncRepository)
	{
		_asyncRepository = asyncRepository;

		RuleFor(e => e)
			.MustAsync(SpaceExist)
			.WithMessage("A poll with spcecified is does not exist");
	}

	private async Task<bool> SpaceExist(DisableSpaceCommand e, CancellationToken token)
	{
		var space = await _asyncRepository.GetByIdAsync(e.PollingSpaceId);

		return space is null ? false : true;
	}
}

