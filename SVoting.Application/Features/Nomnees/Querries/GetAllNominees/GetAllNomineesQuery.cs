using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nominees.Querries.GetAllNominees;

public class GetAllNomineesQuery : IRequest<PaggedListVm<NomineeDto>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetAllNomineesQueryHandler : IRequestHandler<GetAllNomineesQuery, PaggedListVm<NomineeDto>>
{
    private readonly IAsyncRepository<Nominee> _asyncRepository;
    private readonly IMapper _mapper;

    public GetAllNomineesQueryHandler(IAsyncRepository<Nominee> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<PaggedListVm<NomineeDto>> Handle(GetAllNomineesQuery request, CancellationToken cancellationToken)
    {
        var list = await _asyncRepository.GetPagedReponseAsync(request.Page, request.Size);
        var nominees = _mapper.Map<List<NomineeDto>>(list);

        var @count = await _asyncRepository.Count();

        return new PaggedListVm<NomineeDto>() { Count = @count, ListItems = nominees, Page = request.Page, Size = request.Size };
    }
}

