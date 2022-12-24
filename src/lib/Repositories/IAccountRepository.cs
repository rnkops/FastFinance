using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Repositories;

public interface IAccountRepository<TModel> where TModel : IAccount
{
    Task<TModel?> FindAsync(Guid id);
    Task<TModel?> FindAsync(string externalId);
    Task AddAsync(TModel account);
    Task UpdateAsync(TModel account);
    Task UpdateAsync(IEnumerable<TModel> accounts);
    Task<string> GetMaxNumberAsync(Guid? parentId);
    Task<TModel[]> GetAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery;
    Task<TModel[]> GetAsync(IEnumerable<Guid> ids);
    Task<bool> AnyAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery;
    Task<int> SaveChangesAsync();
}

public interface IAccountRepository : IAccountRepository<Account>
{
}
