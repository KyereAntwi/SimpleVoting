using System;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace SVoting.Application.Features.Nomnees.Querries.GetPhotogrph;

public class GetNomineePhotographQuery : IRequest<byte[]>
{
	public Guid NomineeId { get; set; }
}

public class GetNomineePhotographQueryhandler : IRequestHandler<GetNomineePhotographQuery, byte[]>
{
    private readonly IAsyncRepository<Nominee> _asyncRepository;

    public GetNomineePhotographQueryhandler(IAsyncRepository<Nominee> asyncRepository)
    {
        _asyncRepository = asyncRepository;
    }

    public async Task<byte[]> Handle(GetNomineePhotographQuery request, CancellationToken cancellationToken)
    {
        var nominee = await _asyncRepository.GetByIdAsync(request.NomineeId);

        if (nominee == null)
        {
            throw new NotFoundException("Spcified nominee does not exist", nameof(Nominee));
        }

        return nominee.Photograph;
    }
}

