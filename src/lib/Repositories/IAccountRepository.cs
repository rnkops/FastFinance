using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Repositories;

public interface IAccountRepository
{
    Task<Account?> FindAsync(Guid id);
    Task<Account?> FindAsync(string externalId);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task UpdateAsync(IEnumerable<Account> accounts);
    Task<string> GetMaxNumberAsync(Guid? parentId);
    Task<Account[]> GetAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery;
    Task<Account[]> GetAsync(IEnumerable<Guid> ids);
    Task<bool> AnyAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery;
    Task<int> SaveChangesAsync();
}
