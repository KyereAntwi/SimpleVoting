using System;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Polls.Commands.AddCategoryToPoll;

public class AddCategoryToPollCommand : IRequest<AddCategoryToPollResponse>
{
	public Guid PollId { get; set; }
	public Guid CategoryId { get; set; }	
}

public class AddCategoryToPollCommandHandler : IRequestHandler<AddCategoryToPollCommand, AddCategoryToPollResponse>
{
    private readonly IAsyncRepository<PollCategory> _asyncRepository;
    private readonly IPollRepository _pollRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AddCategoryToPollCommandHandler(IAsyncRepository<PollCategory> asyncRepository, IPollRepository pollRepository, ICategoryRepository categoryRepository)
    {
        _asyncRepository = asyncRepository;
        _pollRepository = pollRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<AddCategoryToPollResponse> Handle(AddCategoryToPollCommand request, CancellationToken cancellationToken)
    {
        var response = new AddCategoryToPollResponse();

        var validator = new AddCategoryToPollCommandValidator(_pollRepository, _categoryRepository);
        var validationErrors = await validator.ValidateAsync(request);

        if (validationErrors.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationErrors.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (response.Success)
        {
            await _asyncRepository.AddAsync(
                new PollCategory()
                {
                    CategoryId = request.CategoryId,
                    PollId = request.PollId
                }
            );
        }

        return response;
    }
}

