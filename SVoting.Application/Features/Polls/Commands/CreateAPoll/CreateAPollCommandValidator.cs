using System;
using FluentValidation;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.Polls.Commands.CreateAPoll;

public class CreateAPollCommandValidator : AbstractValidator<CreateAPollCommand>
{
    private readonly IAsyncRepository<PollingSpace> _asyncRepository;

    public CreateAPollCommandValidator(IAsyncRepository<PollingSpace> asyncRepository)
	{
		_asyncRepository = asyncRepository;

		RuleFor(e => e.Title)
			.NotEmpty().WithMessage("{PropertyName} field is required")
			.NotNull();

		RuleFor(e => e)
			.MustAsync(PollingSpaceExist).WithMessage("Polling space is specified does not exist");
	}

	private async Task<bool> PollingSpaceExist(CreateAPollCommand command, CancellationToken token)
	{
		var space = await _asyncRepository.GetByIdAsync(command.PollingSpaceId);

		return space is null ? false : true;
	}
}

