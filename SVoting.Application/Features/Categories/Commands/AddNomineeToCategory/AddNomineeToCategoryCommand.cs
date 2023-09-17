using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Categories.Commands.AddNomineeToCategory;

public class AddNomineeToCategoryCommand : IRequest<AddNomineeToCategoryResponse>
{
    public Guid NomineeId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid PollId { get; set; }
}

public class AddNomineeToCategoryCommandHandler : IRequestHandler<AddNomineeToCategoryCommand, AddNomineeToCategoryResponse>
{
    private readonly IPollCategoryRepository _pollCategoryRepository;
    private readonly INomineeRepository _nomineeRepository;
    private readonly IMapper _mapper;

    public AddNomineeToCategoryCommandHandler(IPollCategoryRepository pollCategoryRepository, INomineeRepository nomieeRepository, IMapper mapper)
    {
        _pollCategoryRepository = pollCategoryRepository;
        _nomineeRepository = nomieeRepository;
        _mapper = mapper;
    }

    public async Task<AddNomineeToCategoryResponse> Handle(AddNomineeToCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new AddNomineeToCategoryResponse();

        var validator = new AddNomineeToCategoryCommandValidator(_nomineeRepository, _pollCategoryRepository);
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
            var pollCategory = await _pollCategoryRepository.GetPollCategoryByPollCategory(request.PollId, request.CategoryId);

            await _pollCategoryRepository.AddNomineeToPollCategory(new NomineeCategory()
            {
                NomineeId = request.NomineeId,
                PollCategoryId = pollCategory!.Id
            });
        }

        return response;
    }
}

