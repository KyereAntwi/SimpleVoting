using Microsoft.EntityFrameworkCore;
using SVoting.Application.Contracts.Persistence;
using SVoting.Domain.Entities;
using SVoting.Persistence.Data;

namespace SVoting.Persistence.Repository;

public class CodeRepository : BaseRepository<Code>, ICodesRepository
{
    private readonly SVotingDbContext _dbContext;

    public CodeRepository(SVotingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CodeExistAndBelongToAPoll(string identifier, Guid pollId)
    {
        var code = await _dbContext.Codes.FirstOrDefaultAsync(c => c.Identifier == identifier);

        if (code == null)
            return false;

        if (!Guid.Equals(code.PollId, pollId)) return false;

        return true;
    }

    public async Task<Code?> GetByIdentifier(string identifier) => await _dbContext.Codes.FirstOrDefaultAsync(c => c.Identifier == identifier);
}
