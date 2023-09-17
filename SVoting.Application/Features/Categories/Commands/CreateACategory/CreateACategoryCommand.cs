using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Categories.Commands.CreateACategory;

public class CreateACategoryCommand : IRequest<CreateACategoryResponse>
{
    public string Identifier { get; set; } = string.Empty;
    public string? Username { get; set; }
}

public class CreateACategoryCommandHandler : IRequestHandler<CreateACategoryCommand, CreateACategoryResponse>
{
    private readonly IAsyncRepository<PollingCategory> _asyncRepository;
    private readonly IMapper _mapper;

    public CreateACategoryCommandHandler(IAsyncRepository<PollingCategory> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<CreateACategoryResponse> Handle(CreateACategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateACategoryResponse();

        var validator = new CreateACategoryCommandValidator();
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
            var category = await _asyncRepository.AddAsync(new PollingCategory() { Identifyer = request.Identifier, UserName = request.Username! });
            response.CategoryDto = _mapper.Map<CategoryDto>(category);
        }

        return response;
    }
}

