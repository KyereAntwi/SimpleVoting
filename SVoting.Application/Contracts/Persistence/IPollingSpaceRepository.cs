using System;
using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence;

public interface IPollingSpaceRepository : IAsyncRepository<PollingSpace>
{
    Task<PollingSpace> GetPollingSpaceByUserId(string userId);
}

