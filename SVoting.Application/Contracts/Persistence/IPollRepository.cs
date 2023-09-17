using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface IPollRepository : IAsyncRepository<Poll>
{
    Task<List<Poll>> GetPollsByPollingSpace(Guid spaceId);
    Task<Poll?> GetPollsWithCategories(Guid pollId);
    Task<Poll?> GetPollByVoterCode(string voterCode);
    Task ActivatePoll(Guid pollId);
}

