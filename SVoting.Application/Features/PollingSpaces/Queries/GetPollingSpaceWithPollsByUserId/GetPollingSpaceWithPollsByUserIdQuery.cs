using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.PollingSpaces.Queries.GetPollingSpaceWithPollsByUserId
{
    public class GetPollingSpaceWithPollsByUserIdQuery : IRequest<PollingSpaceDto>
	{
		public string? UserId { get; set; }
	}

    public class GetPollingSpaceWithPollsByUserIdQueryHander : IRequestHandler<GetPollingSpaceWithPollsByUserIdQuery, PollingSpaceDto>
    {
        private readonly IPollingSpaceRepository _pollingSpaceRepository;
        private readonly IMapper _mapper;

        public GetPollingSpaceWithPollsByUserIdQueryHander(IPollingSpaceRepository pollingSpaceRepository, IMapper mapper)
        {
            _pollingSpaceRepository = pollingSpaceRepository;
            _mapper = mapper;
        }

        public async Task<PollingSpaceDto> Handle(GetPollingSpaceWithPollsByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId is null)
                throw new BadReuestException("User id was not provided");

            var space = await _pollingSpaceRepository.GetPollingSpaceByUserId(request.UserId);

            var spaceDto = _mapper.Map<PollingSpaceDto>(space);

            spaceDto.Polls = new List<PollDto>();

            if (space!.Polls.Count > 0)
                spaceDto.Polls = _mapper.Map<List<PollDto>>(space.Polls);

            return spaceDto;
        }
    }
}

