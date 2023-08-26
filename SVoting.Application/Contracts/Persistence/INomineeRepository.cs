using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface INomineeRepository : IAsyncRepository<Nominee>
{
    Task<List<Nominee>> GetNomineesByPollingCategory(Guid pollingCategoryId);
    Task<Nominee?> GetNomineeDetails(Guid nomineeId);
}

