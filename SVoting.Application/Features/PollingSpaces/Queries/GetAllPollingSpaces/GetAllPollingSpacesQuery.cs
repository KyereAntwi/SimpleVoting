using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.PollingSpaces.Queries.GetAllPollingSpaces;

public class GetAllPollingSpacesQuery : IRequest<PaggedListVm<PollingSpaceDto>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetAllPollingSpacesQueryHandler : IRequestHandler<GetAllPollingSpacesQuery, PaggedListVm<PollingSpaceDto>>
{
    private readonly IAsyncRepository<PollingSpace> _asyncRepository;
    private readonly IMapper _mapper;

    public GetAllPollingSpacesQueryHandler(IAsyncRepository<PollingSpace> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }

    public async Task<PaggedListVm<PollingSpaceDto>> Handle(GetAllPollingSpacesQuery request, CancellationToken cancellationToken)
    {
        var list = await _asyncRepository.GetPagedReponseAsync(request.Page = 1, request.Size = 20);
        var spaces = _mapper.Map<List<PollingSpaceDto>>(list);

        var allList = await _asyncRepository.ListAllAsync();

        return new PaggedListVm<PollingSpaceDto>() { Count = allList.Count, ListItems = spaces, Page = request.Page, Size = request.Size };
    }
}

