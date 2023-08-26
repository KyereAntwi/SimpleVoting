using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Shared;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Polls.Queries.GetAllPolls;

public class GetAllPollsQuery : IRequest<PaggedListVm<PollDto>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, PaggedListVm<PollDto>>
{
    private readonly IAsyncRepository<PollingSpace> _asyncRepository;
    private readonly IMapper _mapper;

    public GetAllPollsQueryHandler(IAsyncRepository<PollingSpace> asyncReopsitory, IMapper mapper)
    {
        _asyncRepository = asyncReopsitory;
        _mapper = mapper;
    }

    public async Task<PaggedListVm<PollDto>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
    {
        var list = await _asyncRepository.GetPagedReponseAsync(request.Page, request.Size);
        var polls = _mapper.Map<List<PollDto>>(list);

        var allList = await _asyncRepository.Count();

        return new PaggedListVm<PollDto>() { Count = allList, ListItems = polls, Page = request.Page, Size = request.Size };
    }
}

