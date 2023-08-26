using FluentValidation;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.Nomnees.Commands.UpdateNominee;

public class UpdateNomineeCommandValidator : AbstractValidator<UpdateNomineeCommand>
{
    private readonly IAsyncRepository<Nominee> _asyncRepository;

    public UpdateNomineeCommandValidator(IAsyncRepository<Nominee> asyncRepository)
    {
        _asyncRepository = asyncRepository;

        RuleFor(e => e.Fullname)
            .NotNull() .NotEmpty().WithMessage("{PropertyName} field is required");

        RuleFor(e => e)
            .MustAsync(NomineeExists).WithMessage("Specified nominee with id does not exist");
    }

    private async Task<bool> NomineeExists(UpdateNomineeCommand command, CancellationToken token)
    {
        var nominee = await _asyncRepository.GetByIdAsync(command.Id);
        return nominee is null ? false : true;
    }
}
