using FluentValidation;

namespace SVoting.Application.Features.Nomnees.Commands.CreateANominee;

public class CreateANomineeCommandValidator : AbstractValidator<CreateANomineeCommand>
{
    public CreateANomineeCommandValidator()
    {
            RuleFor(e => e.Fullname)
                .NotEmpty().WithMessage("{PropertyName} field is required")
                .NotNull();
    }
}
