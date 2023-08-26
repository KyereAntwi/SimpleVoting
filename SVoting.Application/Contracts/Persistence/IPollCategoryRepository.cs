using System;
using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface IPollCategoryRepository : IAsyncRepository<PollCategory>
{
    Task AddNomineeToPollCategory(NomineeCategory nomineeCategory);
    Task<PollCategory?> GetPollCategoryFullDetail(Guid id);
    Task<PollCategory?> GetPollCategoryByPollCategory(Guid pollId, Guid categoryId);
}

