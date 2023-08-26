using System;
using FluentValidation;
using SVoting.Application.Contracts.Persistence;

namespace SVoting.Application.Features.Polls.Commands.AddCategoryToPoll;

public class AddCategoryToPollCommandValidator : AbstractValidator<AddCategoryToPollCommand>
{
    private readonly IPollRepository _pollRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryToPollCommandValidator(IPollRepository pollRepository, ICategoryRepository categoryRepository)
	{
		_pollRepository = pollRepository;
		_categoryRepository = categoryRepository;

		RuleFor(e => e)
			.MustAsync(PollExists)
			.WithMessage("Poll specified does not exist");

		RuleFor(e => e)
			.MustAsync(CategoryExists)
			.WithMessage("Category specified does not exist");
	}

	private async Task<bool> PollExists(AddCategoryToPollCommand command, CancellationToken token)
	{
		var poll = await _pollRepository.GetByIdAsync(command.PollId);
		return poll is null ? false : true;
	}

	private async Task<bool> CategoryExists(AddCategoryToPollCommand command, CancellationToken token)
	{
		var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
		return category is null ? false : true;
	}
}

