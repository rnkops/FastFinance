using FastFinance.Dtos;
using FastFinance.Models;
using FastFinance.Repositories;

namespace FastFinance.Data;

public class ChartOfAccounts : IChartOfAccounts
{
    protected readonly IAccountRepositoryFactory _accountRepositoryFactory;

    public ChartOfAccounts(IAccountRepositoryFactory accountRepositoryFactory)
    {
        _accountRepositoryFactory = accountRepositoryFactory;
    }

    public virtual Task<TAccount?> FindAsync<TAccount>(Guid id) where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.FindAsync(id);
    }

    public virtual Task<TAccount?> FindAsync<TAccount>(string externalId) where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.FindAsync(externalId);
    }

    public virtual async Task<TAccount> AddAsync<TAccount, TRequest>(TRequest request) where TRequest : class, IAddAccountRequest where TAccount : IAccount, new()
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        var number = request.Number ?? await accountRepository.GetMaxNumberAsync(request.ParentId);
        var account = request.GetAccount<TAccount>();
        account.Id = request.Id ?? Guid.NewGuid();
        account.Number = number;
        if (account.CreatedAt == default || account.CreatedAt == DateTimeOffset.MinValue || account.CreatedAt == DateTimeOffset.MaxValue)
        {
            account.CreatedAt = DateTimeOffset.UtcNow;
        }
        await accountRepository.AddAsync(account);
        return account;
    }

    public virtual Task UpdateAsync<TAccount>(TAccount account) where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.UpdateAsync(account);
    }

    public virtual Task UpdateAsync<TAccount>(IEnumerable<TAccount> accounts) where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.UpdateAsync(accounts);
    }

    public virtual Task<TAccount[]> GetAsync<TAccount, TQuery>(TQuery query) where TQuery : IGetAccountsQuery where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.GetAsync(query);
    }

    public virtual Task<TAccount[]> GetAsync<TAccount>(IEnumerable<Guid> ids) where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.GetAsync(ids);
    }

    public virtual Task<int> SaveChangesAsync<TAccount>() where TAccount : IAccount
    {
        var accountRepository = _accountRepositoryFactory.Create<TAccount>();
        return accountRepository.SaveChangesAsync();
    }
}

