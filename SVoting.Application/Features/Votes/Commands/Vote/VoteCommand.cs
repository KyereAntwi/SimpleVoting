using AutoMapper;
using MediatR;
using SVoting.Application.Contracts.Persistence;
using SVoting.Application.Exceptions;
using SVoting.Domain.Entities;
using SVoting.Shared.Models;

namespace SVoting.Application.Features.Votes.Commands.Vote
{
    public class VoteCommand : IRequest<VoteResponse>
    {
        public string VotingCode { get; set; } = string.Empty;
        public Guid PollId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid NomineeId { get; set; }
    }

    public class VoteCommandHandler : IRequestHandler<VoteCommand, VoteResponse>
    {
        private readonly IAsyncRepository<Domain.Entities.Vote> _asyncRepository;
        private readonly IPollRepository _pollRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly INomineeRepository _nomineeRepository;
        private readonly IPollCategoryRepository _pollCategoryRepository;
        private readonly ICodesRepository _codeRepository;

        public VoteCommandHandler(IVoteRepository asyncRepository, 
                                    IPollRepository pollRepository, 
                                    ICategoryRepository categoryRepository, 
                                    INomineeRepository nomineeRepository, 
                                    IPollCategoryRepository pollCategoryRepository, 
                                    ICodesRepository codesRepository) 
        {
            _asyncRepository = asyncRepository;
            _pollRepository = pollRepository;
            _categoryRepository = categoryRepository;
            _nomineeRepository = nomineeRepository;
            _pollCategoryRepository = pollCategoryRepository;
            _codeRepository = codesRepository;
        }

        public async Task<VoteResponse> Handle(VoteCommand request, CancellationToken cancellationToken)
        {
            var response = new VoteResponse();

            var validator = new VoteCommandValidator(_pollRepository, _categoryRepository, _nomineeRepository, _codeRepository);
            var validationErrors = await validator.ValidateAsync(request);

            if (validationErrors.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationErrors.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var pollCategory = await _pollCategoryRepository.GetPollCategoryByPollCategory(request.PollId, request.CategoryId);
                
                if (pollCategory == null)
                {
                    throw new NotFoundException("Poll Category specified was not found", nameof(PollCategory));
                }

                await _asyncRepository.AddAsync(new Domain.Entities.Vote()
                {
                    VotingCode = request.VotingCode,
                    PollCategoryId = pollCategory.Id,
                    NomineeId = request.NomineeId
                });
            }

            return response;
        }
    }
}
