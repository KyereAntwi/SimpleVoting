using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nominees.Querries.GetNomineeDetails;

public class GetNomineeDetailsQuery : IRequest<NomineeDto>
{
	public Guid NomineeId { get; set; }
}

public class GetNomineeDetailsQueryHandler : IRequestHandler<GetNomineeDetailsQuery, NomineeDto>
{
    private readonly IAsyncRepository<Nominee> _asyncRepository;
    private readonly IMapper _mapper;

    public GetNomineeDetailsQueryHandler(IAsyncRepository<Nominee> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<NomineeDto> Handle(GetNomineeDetailsQuery request, CancellationToken cancellationToken)
    {
        var nominee = await _asyncRepository.GetByIdAsync(request.NomineeId);
        return _mapper.Map<NomineeDto>(nominee);
    }
}

