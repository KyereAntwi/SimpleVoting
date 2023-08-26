using SVoting.Domain.Entities;

namespace SVoting.Application.Contracts.Persistence
{
    public interface ICodesRepository : IAsyncRepository<Code>
    {
        Task<Code?> GetByIdentifier(string identifier);
        Task<bool> CodeExistAndBelongToAPoll(string identifier, Guid pollId);
    }
}
