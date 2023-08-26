using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.Polls.Commands.DeletePoll;

public class PollDeleteCommand : IRequest
{
	public Guid PollId { get; set; }
}

public class PollDeleteCommandHandler : IRequestHandler<PollDeleteCommand>
{
    private readonly IAsyncRepository<Poll> _asyncRepository;

    public PollDeleteCommandHandler(IAsyncRepository<Poll> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<Unit> Handle(PollDeleteCommand request, CancellationToken cancellationToken)
    {
        var poll = await _asyncRepository.GetByIdAsync(request.PollId);

        if (poll is null)
        {
            throw new NotFoundException("Poll does not exist", nameof(Poll));
        }

        await _asyncRepository.DeleteAsync(poll);

        return Unit.Value;
    }
}

