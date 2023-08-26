using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;

namespace SVoting.Application.Features.Codes.Commands
{
    public class CreateCodeCommand : IRequest<string>
    {
        public Guid PollId { get; set; }
    }

    public class CreateCodeCommandHandler : IRequestHandler<CreateCodeCommand, string>
    {
        private readonly ICodesRepository _asyncRepository;
        private readonly IPollRepository _pollRepository;

        public CreateCodeCommandHandler(ICodesRepository asyncRepository, IPollRepository pollRepository)
        {
            _asyncRepository = asyncRepository;
            _pollRepository = pollRepository;
        }

        public async Task<string> Handle(CreateCodeCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.PollId);
            if (poll == null)
            {
                throw new NotFoundException("Poll id specified does not exist", nameof(Poll));
            }

            GenerateRandomText generateRandom = new GenerateRandomText();

            var randomText = generateRandom.Generate(5);
            var exists = await _asyncRepository.GetByIdentifier(randomText);

            while (exists != null) 
            {
                randomText = generateRandom.Generate(5);
                exists = await _asyncRepository.GetByIdentifier(randomText);
            }

            await _asyncRepository.AddAsync(new Code() { Identifier = randomText, PollId = request.PollId });

            return randomText;
        }
    }
}
