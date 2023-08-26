using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository;

public class NomineeRepository : BaseRepository<Nominee>, INomineeRepository
{
    private readonly SVotingDbContext _dbContext;
    public NomineeRepository(SVotingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Nominee?> GetNomineeDetails(Guid nomineeId) => await _dbContext.Nominees
        .Include(n => n.NomineeCategories)
        .Include(n => n.Votes)
        .FirstOrDefaultAsync(n => n.Id.Equals(nomineeId));

    public async Task<List<Nominee>> GetNomineesByPollingCategory(Guid pollingCategoryId)
    {
        List<Nominee> nominees = new List<Nominee> ();

        var pollCategory = await _dbContext.PollCategories.FindAsync(pollingCategoryId);

        if (pollCategory == null)
        {
            return nominees;
        }

        var nomineeList = _dbContext.Nominees.Include(n => n.NomineeCategories);

        foreach ( var nominee in nomineeList) 
        {
            var nomineeCategories = nominee.NomineeCategories;

            if (nomineeCategories != null && nomineeCategories.Any())
            {
                foreach ( var category in nomineeCategories)
                {
                    if (category.PollCategoryId.Equals(pollCategory.Id)) 
                    {
                        nominees.Add(nominee);
                        continue;
                    }
                }
            }
        }

        return nominees;
    }
}
