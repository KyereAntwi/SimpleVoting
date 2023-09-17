using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nomnees.Querries.GetNomineesByPollCategory;

public class GetNomineesByPollCategoryQuery : IRequest<List<NomineeDto>>
{
	public Guid PollId { get; set; }
	public Guid CategoryId { get; set; }
}

public class GetNomineesByPollCategoryQueryHandler : IRequestHandler<GetNomineesByPollCategoryQuery, List<NomineeDto>>
{
    private readonly IPollCategoryRepository _pollCategoryRepository;
    private readonly INomineeRepository _nomineeRepository;
    private readonly IMapper _mapper;

    public GetNomineesByPollCategoryQueryHandler(IPollCategoryRepository pollCategoryRepository, INomineeRepository nomineeRepository, IMapper mapper)
    {
        _pollCategoryRepository = pollCategoryRepository;
        _nomineeRepository = nomineeRepository;
        _mapper = mapper;
    }

    public async Task<List<NomineeDto>> Handle(GetNomineesByPollCategoryQuery request, CancellationToken cancellationToken)
    {
        List<NomineeDto> nomineeDtos = new List<NomineeDto>();

        var result = await _pollCategoryRepository.GetPollCategoryByPollCategory(request.PollId, request.CategoryId);

        if (result != null)
        {
            if (result.NomineeCategories.Count > 0)
            {
                foreach (var nominee in result.NomineeCategories)
                {
                    var nomineeResponse = await _nomineeRepository.GetByIdAsync(nominee.NomineeId);
                    nomineeDtos.Add(_mapper.Map<NomineeDto>(nomineeResponse));
                }
            }
        }

        return nomineeDtos;
    }
}

