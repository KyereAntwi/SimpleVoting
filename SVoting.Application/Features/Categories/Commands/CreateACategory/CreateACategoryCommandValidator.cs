using System;
using FluentValidation;

namespace SVoting.Application.Features.Categories.Commands.CreateACategory
{
	public class CreateACategoryCommandValidator : AbstractValidator<CreateACategoryCommand>
	{
		public CreateACategoryCommandValidator()
		{
			RuleFor(e => e.Identifier)
				.NotEmpty().WithMessage("{PropertyName} field is required")
				.NotNull();
		}
	}
}

