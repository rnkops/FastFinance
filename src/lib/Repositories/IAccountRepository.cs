using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Repositories;

public interface IAccountRepository<TModel> where TModel : IAccount
{
    Task<TModel?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TModel?> FindAsync(string externalId, CancellationToken cancellationToken = default);
    Task AddAsync(TModel account, CancellationToken cancellationToken = default);
    Task UpdateAsync(TModel account, CancellationToken cancellationToken = default);
    Task UpdateAsync(IEnumerable<TModel> accounts, CancellationToken cancellationToken = default);
    Task<string> GetMaxNumberAsync(Guid? parentId, CancellationToken cancellationToken = default);
    Task<TModel[]> GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IGetAccountsQuery;
    Task<TModel[]> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IGetAccountsQuery;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IAccountRepository : IAccountRepository<Account>
{
}
