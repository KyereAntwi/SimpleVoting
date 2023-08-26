using System;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.Polls.Commands.ActivatePoll;

public class ActivatePollCommand : IRequest
{
	public Guid PollId { get; set; }
}

public class ActivatePollCommandHandler : IRequestHandler<ActivatePollCommand>
{
    private readonly IPollRepository _pollRepository;

    public ActivatePollCommandHandler(IPollRepository pollRepository)
    {
        _pollRepository = pollRepository;
    }

    public async Task<Unit> Handle(ActivatePollCommand request, CancellationToken cancellationToken)
    {
        var poll = await _pollRepository.GetByIdAsync(request.PollId);

        if (poll is null)
            throw new NotFoundException("Specified poll does not exist", nameof(Poll));

        await _pollRepository.ActivatePoll(poll.Id);

        return Unit.Value;
    }
}

