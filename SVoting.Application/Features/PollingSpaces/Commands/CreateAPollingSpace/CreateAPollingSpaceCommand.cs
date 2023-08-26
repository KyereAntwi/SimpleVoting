using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.PollingSpaces.Commands.CreateAPollingSpace;

public class CreateAPollingSpaceCommand : IRequest<CreatePollingSpaceResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string? UserId { get; set; }
}

public class CreateAPollingSpaceCommandHandler : IRequestHandler<CreateAPollingSpaceCommand, CreatePollingSpaceResponse>
{
    private readonly IAsyncRepository<PollingSpace> _asyncRepository;
    private readonly IMapper _mapper;

    public CreateAPollingSpaceCommandHandler(IAsyncRepository<PollingSpace> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<CreatePollingSpaceResponse> Handle(CreateAPollingSpaceCommand request, CancellationToken cancellationToken)
    {
        var response = new CreatePollingSpaceResponse();

        var validator = new CreateAPollingSpaceCommandValidator();
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
            var space = new PollingSpace()
            {
                UserId = request.UserId,
                Name = request.Name,
                Industry = request.Industry
            };

            space = await _asyncRepository.AddAsync(space);
            response.PollingSpace = _mapper.Map<PollingSpaceDto>(space);
        }

        return response;
    }
}

