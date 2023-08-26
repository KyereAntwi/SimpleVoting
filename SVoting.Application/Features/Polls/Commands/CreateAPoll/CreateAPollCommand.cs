using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Polls.Commands.CreateAPoll;

public class CreateAPollCommand : IRequest<CreatePollResonse>
{
    public string Title { get; set; } = string.Empty;
    public Guid PollingSpaceId { get; set; }
}

public class CreateAPollCommandHandler : IRequestHandler<CreateAPollCommand, CreatePollResonse>
{
    private readonly IAsyncRepository<PollingSpace> _asyncRepository;
    private readonly IMapper _mapper;
    private readonly IPollRepository _pollRepository;

    public CreateAPollCommandHandler(IAsyncRepository<PollingSpace> asyncRepository, IPollRepository pollRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
        _pollRepository = pollRepository;
    }

    public async Task<CreatePollResonse> Handle(CreateAPollCommand request, CancellationToken cancellationToken)
    {
        var response = new CreatePollResonse();

        var validator = new CreateAPollCommandValidator(_asyncRepository);
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
            var poll = new Poll()
            {
                Title = request.Title,
                PollingSpaceId = request.PollingSpaceId
            };
            poll = await _pollRepository.AddAsync(poll);
            response.PollDto = _mapper.Map<PollDto>(poll);
        }

        return response;
    }
}

