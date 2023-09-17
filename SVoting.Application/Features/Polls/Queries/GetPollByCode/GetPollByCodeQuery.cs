using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Polls.Queries.GetPollByCode
{
    public record GetPollByCodeQuery(string Code) : IRequest<PollDto>;

    public class GetPollByCodeQueryHandler : IRequestHandler<GetPollByCodeQuery, PollDto> 
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;

        public GetPollByCodeQueryHandler(IPollRepository pollRepository, IMapper mapper)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
        }

        public async Task<PollDto> Handle(GetPollByCodeQuery request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetPollByVoterCode(request.Code);

            if (poll == null)
            {
                throw new NotFoundException("Poll was not found", typeof(PollDto));
            }

            return _mapper.Map<PollDto>(poll);
        }
    }
}
