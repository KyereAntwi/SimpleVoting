using System;
using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface ICategoryRepository : IAsyncRepository<PollingCategory>
{
    Task<List<PollingCategory>> GetCategoriesByPoll(Guid pollingId);
    Task<List<PollingCategory>> GetCategoriesByUser(string username);
}

