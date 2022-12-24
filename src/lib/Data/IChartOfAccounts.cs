using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Data;

public interface IChartOfAccounts
{
    Task<TAccount> AddAsync<TAccount, TRequest>(TRequest request)
        where TAccount : IAccount, new()
        where TRequest : class, IAddAccountRequest<TAccount>;
    Task<TAccount?> FindAsync<TAccount>(Guid id) where TAccount : IAccount;
    Task<TAccount?> FindAsync<TAccount>(string externalId) where TAccount : IAccount;
    Task<TAccount[]> GetAsync<TAccount, TQuery>(TQuery query)
        where TAccount : IAccount
        where TQuery : IGetAccountsQuery;
    Task<TAccount[]> GetAsync<TAccount>(IEnumerable<Guid> ids) where TAccount : IAccount;
    Task UpdateAsync<TAccount>(TAccount account) where TAccount : IAccount;
    Task UpdateAsync<TAccount>(IEnumerable<TAccount> accounts) where TAccount : IAccount;
    Task<int> SaveChangesAsync<TAccount>() where TAccount : IAccount;
}

