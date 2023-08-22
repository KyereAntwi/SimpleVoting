using System;
using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface IPollRepository : IAsyncRepository<Poll>
{
    Task<List<Poll>> GetPollsByPollingSpace(Guid spaceId);
}

