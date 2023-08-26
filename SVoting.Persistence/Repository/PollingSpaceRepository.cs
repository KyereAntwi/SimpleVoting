using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository;

public class PollingSpaceRepository : BaseRepository<PollingSpace>, IPollingSpaceRepository
{
    private readonly SVotingDbContext _dbContext;
    public PollingSpaceRepository(SVotingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PollingSpace?> GetPollingSpaceByUserId(string userId) => await _dbContext.PollingSpaces.Include(p => p.Polls).FirstOrDefaultAsync(s => s.UserId == userId);

    public async Task TogglePollingSpaceDisability(Guid pollingSpaceId)
    {
        var space = await _dbContext.PollingSpaces.FindAsync(pollingSpaceId);
        space!.Active = !space.Active;
    }
}
