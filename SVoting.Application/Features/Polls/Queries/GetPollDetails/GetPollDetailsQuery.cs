using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Polls.Queries.GetPollDetails;

public class GetPollDetailsQuery : IRequest<PollDto>
{
	public Guid Id { get; set; }
}

public class GetPollDetailsQueryHandler : IRequestHandler<GetPollDetailsQuery, PollDto>
{
    private readonly IPollRepository _pollRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetPollDetailsQueryHandler(IPollRepository pollRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _pollRepository = pollRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<PollDto> Handle(GetPollDetailsQuery request, CancellationToken cancellationToken)
    {
        var existingPoll = await _pollRepository.GetPollsWithCategories(request.Id);

        var pollDto = _mapper.Map<PollDto>(existingPoll);

        if(existingPoll!.PollCategories.Count > 0)
        {
            foreach(var item in existingPoll.PollCategories)
            {
                var catDto = await _categoryRepository.GetByIdAsync(item.CategoryId);
                pollDto.Categories?.Add(_mapper.Map<CategoryDto>(catDto));
            }
        }

        return pollDto;
    }
}

