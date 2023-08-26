using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nomnees.Querries.GetNomineeCategories;

public class GetNomineeCategoriesQuery : IRequest<List<CategoryDto>>
{
    public Guid NomineeId { get; set; }
}

public class GetNomineeCategoryQueryHandler : IRequestHandler<GetNomineeCategoriesQuery, List<CategoryDto>>
{
    private readonly INomineeRepository _nomineeRepository;
    private readonly IPollCategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetNomineeCategoryQueryHandler(INomineeRepository nomineeRepository, IPollCategoryRepository categoryRepository, IMapper mapper)
    {
        _nomineeRepository = nomineeRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetNomineeCategoriesQuery request, CancellationToken cancellationToken)
    {
        var nominee = await _nomineeRepository.GetNomineeDetails(request.NomineeId);

        if (nominee is null)
        {
            throw new NotFoundException("Nominee specified does not exist", nameof(Nominee));
        }

        List<CategoryDto> categoryDtos = new List<CategoryDto>();

        if (nominee.NomineeCategories.Count > 0)
        {
            foreach (var item in nominee.NomineeCategories)
            {
                var category = await _categoryRepository.GetPollCategoryFullDetail(item.PollCategoryId);
                categoryDtos.Add(_mapper.Map<CategoryDto>(category!.Category));
            }
        }

        return categoryDtos;
    }
}

