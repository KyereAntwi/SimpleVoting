using System;
using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface IVoteRepository : IAsyncRepository<Vote>
{
	Task<List<Vote>> GetVotesByPollingCategory(Guid pollingCategory);
	Task<List<Vote>> GetVotesByNominee(Guid nomineeId);
}

