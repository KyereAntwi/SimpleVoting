using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository;

public class CategoryRepository : BaseRepository<PollingCategory>, ICategoryRepository
{
    private readonly SVotingDbContext _dbContext;

    public CategoryRepository(SVotingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PollingCategory>> GetCategoriesByPoll(Guid pollingId)
    {
        List<PollingCategory> categories = new List<PollingCategory>();

        var list = await _dbContext.Polls
            .Include(p => p.PollCategories)
            .FirstOrDefaultAsync(p => p.Id == pollingId);

        if (list != null)
        {
            foreach (var polCategory in list.PollCategories)
            {
                var item = await _dbContext.PollingCategories.FindAsync(polCategory.CategoryId);
                
                if(item != null)
                {
                    categories.Add(item);
                }
            }
        }

        return categories;
    }

    public async Task<List<PollingCategory>> GetCategoriesByUser(string username)
    {
        List<PollingCategory> categories = new List<PollingCategory>();

        var list = await _dbContext.PollingCategories.Where(c => c.UserName == username).ToListAsync();

        if (list.Count > 0 && list!= null)
        {
            categories.AddRange(list);
        }

        return categories;
    }
}
