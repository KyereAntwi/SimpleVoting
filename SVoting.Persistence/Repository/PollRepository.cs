using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository
{
    public class PollRepository : BaseRepository<Poll>, IPollRepository
    {
        private readonly SVotingDbContext _dbContext;
        public PollRepository(SVotingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ActivatePoll(Guid pollId)
        {
            var poll = await _dbContext.Polls.FindAsync(pollId);
            poll!.Published = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Poll?> GetPollByVoterCode(string voterCode)
        {
            var code  = await _dbContext.Codes.Include(c => c.Poll).FirstOrDefaultAsync(c => c.Identifier == voterCode);

            if (code == null)
            {
                return null;
            }

            return code.Poll;
        }

        public async Task<List<Poll>> GetPollsByPollingSpace(Guid spaceId)
        {
            List<Poll> polls = new List<Poll>();

            var space = await _dbContext.PollingSpaces.Include(s => s.Polls).FirstOrDefaultAsync(s => s.Id.Equals(spaceId));

            polls.AddRange(space?.Polls ?? Enumerable.Empty<Poll>());

            return polls;
        }

        public async Task<Poll?> GetPollsWithCategories(Guid pollId)
        {
            var poll = await _dbContext.Polls.Include(p => p.PollCategories).FirstOrDefaultAsync(p => p.Id.Equals(pollId));
            return poll;
        }
    }
}
