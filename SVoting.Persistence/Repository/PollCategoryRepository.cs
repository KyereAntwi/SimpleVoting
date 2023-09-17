using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository;

public class PollCategoryRepository : BaseRepository<PollCategory>, IPollCategoryRepository
{
    private readonly SVotingDbContext _dbContext;

    public PollCategoryRepository(SVotingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddNomineeToPollCategory(NomineeCategory nomineeCategory)
    {
        _dbContext.NomineeCategories.Add(nomineeCategory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PollCategory?> GetPollCategoryByPollCategory(Guid pollId, Guid categoryId) => await 
        _dbContext.PollCategories
        .Include(c => c.NomineeCategories).
        FirstOrDefaultAsync(x => x.PollId == pollId && x.CategoryId == categoryId);

    public async Task<PollCategory?> GetPollCategoryFullDetail(Guid id) => await _dbContext.PollCategories
        .Include(p => p.Poll)
        .Include(p => p.NomineeCategories)
        .Include(p => p.Category)
        .Include(p => p.Votes)
        .FirstOrDefaultAsync();
}
                                                                