using System;
using FluentValidation;

namespace SVoting.Application.Features.PollingSpaces.Commands.CreateAPollingSpace;

public class CreateAPollingSpaceCommandValidator : AbstractValidator<CreateAPollingSpaceCommand>
{
	public CreateAPollingSpaceCommandValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("{PropertyName} is required")
			.NotNull();

		RuleFor(c => c.Industry)
			.NotEmpty().WithMessage("{PropertyName} is required")
			.NotNull();

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();
    }
}

