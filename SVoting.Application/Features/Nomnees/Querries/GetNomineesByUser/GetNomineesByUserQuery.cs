using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Nomnees.Querries.GetNomineesByUser;

public class GetNomineesByUserQuery : IRequest<List<NomineeDto>>
{
	public string UserName { get; set; } = string.Empty;
}

public class GetNomineesByUserQueryHandler : IRequestHandler<GetNomineesByUserQuery, List<NomineeDto>>
{
    private readonly IMapper _mapper;
    private readonly INomineeRepository _nomineeRepository;

    public GetNomineesByUserQueryHandler(IMapper mapper, INomineeRepository nomineeRepository)
    {
        _mapper = mapper;
        _nomineeRepository = nomineeRepository;
    }

    public async Task<List<NomineeDto>> Handle(GetNomineesByUserQuery request, CancellationToken cancellationToken)
    {
        var list = await _nomineeRepository.GetNomineesByUser(request.UserName);
        return _mapper.Map<List<NomineeDto>>(list);
    }
}

