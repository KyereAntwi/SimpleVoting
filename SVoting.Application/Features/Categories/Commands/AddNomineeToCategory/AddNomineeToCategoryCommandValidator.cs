using System;
using FluentValidation;
using SVoting.Application.Contracts.Persistence;

namespace SVoting.Application.Features.Categories.Commands.AddNomineeToCategory;

public class AddNomineeToCategoryCommandValidator : AbstractValidator<AddNomineeToCategoryCommand>
{
    private readonly INomineeRepository _nomineeRepository;
    private readonly IPollCategoryRepository _pollCategoryRepository;

    public AddNomineeToCategoryCommandValidator(INomineeRepository nomineeRepository, IPollCategoryRepository pollCategoryRepository)
	{
		_nomineeRepository = nomineeRepository;
		_pollCategoryRepository = pollCategoryRepository;

		RuleFor(e => e)
			.MustAsync(NomineeExists);

        RuleFor(e => e)
            .MustAsync(PollCategoryExists);

    }

	private async Task<bool> NomineeExists(AddNomineeToCategoryCommand command, CancellationToken token)
	{
		var nominee = await _nomineeRepository.GetByIdAsync(command.NomineeId);
		return nominee is null ? false : true;
	}

    private async Task<bool> PollCategoryExists(AddNomineeToCategoryCommand command, CancellationToken token)
    {
        var nominee = await _pollCategoryRepository.GetByIdAsync(command.PollCategoryId);
        return nominee is null ? false : true;
    }
}

