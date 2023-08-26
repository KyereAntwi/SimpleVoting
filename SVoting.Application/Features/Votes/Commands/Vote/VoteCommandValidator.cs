using FluentValidation;
using SVoting.Application.Contracts.Persistence;

namespace SVoting.Application.Features.Votes.Commands.Vote
{
    public class VoteCommandValidator : AbstractValidator<VoteCommand>
    {
        private readonly IPollRepository _poll;
        private readonly ICategoryRepository _cat;
        private readonly INomineeRepository _nominee;
        private readonly ICodesRepository _codeRepository;

        public VoteCommandValidator(IPollRepository pollRepository, ICategoryRepository categoryRepository, INomineeRepository nomineeRepository, ICodesRepository codesRepository)
        {
            _poll = pollRepository; 
            _cat = categoryRepository;
            _nominee = nomineeRepository;
            _codeRepository = codesRepository;

            RuleFor(e => e.VotingCode)
                .NotEmpty().WithMessage("{PropertyName} filed is required")
                .NotNull();

            RuleFor(e => e)
                .MustAsync(PollExists).WithMessage("Specified poll does not exist");

            RuleFor(e => e)
                .MustAsync(CategoryExists).WithMessage("Specified category does not exist");

            RuleFor(e => e)
                .MustAsync(NomineeExits).WithMessage("Specified nominee does not exist");

            RuleFor(e => e)
                .MustAsync(CodeExistAndBelongToPoll).WithMessage("Voting code specified failed validation");
        }

        private async Task<bool> PollExists(VoteCommand command, CancellationToken token)
        {
            var poll = await _poll.GetByIdAsync(command.PollId);
            return poll != null;
        }

        private async Task<bool> CategoryExists(VoteCommand command, CancellationToken token)
        {
            var category = await _cat.GetByIdAsync(command.CategoryId);
            return category != null;
        }

        private async Task<bool> NomineeExits(VoteCommand command, CancellationToken token)
        {
            var nominee = await _nominee.GetByIdAsync(command.NomineeId);
            return nominee != null;
        }

        private async Task<bool> CodeExistAndBelongToPoll(VoteCommand command, CancellationToken token)
        {
            var result = await _codeRepository.CodeExistAndBelongToAPoll(command.VotingCode, command.PollId);
            return result;
        }
    }
}
