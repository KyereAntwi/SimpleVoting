using System;
using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Categories.Queries.GetCategoriesByUser;

public class GetCategoriesByUserQuery : IRequest<List<CategoryDto>>
{
    public string UserName { get; set; } = string.Empty;
}

public class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesByUserQuery, List<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesByUserQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesByUserQuery request, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetCategoriesByUser(request.UserName);

        List<CategoryDto> dtos = new List<CategoryDto>();

        dtos = _mapper.Map<List<CategoryDto>>(results);

        return dtos;
    }
}

