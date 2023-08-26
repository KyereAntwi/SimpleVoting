using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository;

public class VoteRepository : BaseRepository<Vote>, IVoteRepository
{
    private readonly SVotingDbContext _dbContext;
    public VoteRepository(SVotingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Vote>> GetVotesByNominee(Guid nomineeId)
    {
        List<Vote> votes = new List<Vote>();

        var list = await _dbContext.Votes.Where(v => v.NomineeId.Equals(nomineeId)).ToListAsync();

        votes.AddRange(list);

        return votes;
    }

    public async Task<List<Vote>> GetVotesByPollingCategory(Guid pollingCategory)
    {
        throw new NotImplementedException();

        // TODO - Implement this
    }
}
