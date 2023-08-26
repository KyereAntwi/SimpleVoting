using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nomnees.Commands.DeleteNominee;

public class DeleteNomineeCommand : IRequest<DeleteNomineeResponse>
{
    public Guid NomineeId { get; set; }
}

public class DeleteNomineeCommandHandler : IRequestHandler<DeleteNomineeCommand, DeleteNomineeResponse>
{
    private readonly IAsyncRepository<Nominee> _asyncRepository;

    public DeleteNomineeCommandHandler(IAsyncRepository<Nominee> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<DeleteNomineeResponse> Handle(DeleteNomineeCommand request, CancellationToken cancellationToken)
    {
        var nominee = await _asyncRepository.GetByIdAsync(request.NomineeId);

        if (nominee == null)
        {
            throw new NotFoundException("Provided nominee id does not exist", nameof(Nominee));
        }

        await _asyncRepository.DeleteAsync(nominee);

        return new DeleteNomineeResponse() { Success = true, NomineeId = request.NomineeId };
    }
}
