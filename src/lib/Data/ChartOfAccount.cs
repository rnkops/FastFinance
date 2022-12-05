using FastFinance.Dtos;
using FastFinance.Models;
using FastFinance.Repositories;

namespace FastFinance.Data;

public class ChartOfAccount
{
    protected readonly IAccountRepository _accountRepository;

    public ChartOfAccount(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public virtual Task<Account?> FindAsync(Guid id)
        => _accountRepository.FindAsync(id);

    public virtual Task<Account?> FindAsync(string externalId)
        => _accountRepository.FindAsync(externalId);

    public virtual async Task<Account> AddAsync<TRequest>(TRequest request) where TRequest : class, IAddAccountRequest
    {
        var number = request.Number ?? await _accountRepository.GetMaxNumberAsync(request.ParentId);
        var account = new Account
        {
            Id = request.Id ?? Guid.NewGuid(),
            Name = request.Name,
            Number = number,
            ExternalId = request.ExternalId,
            ParentId = request.ParentId,
            CreatedAt = DateTimeOffset.UtcNow,
            Description = request.Description,
        };
        await _accountRepository.AddAsync(account);
        return account;
    }

    public virtual Task UpdateAsync(Account account)
        => _accountRepository.UpdateAsync(account);

    public virtual Task UpdateAsync(IEnumerable<Account> accounts)
        => _accountRepository.UpdateAsync(accounts);

    public virtual Task<Account[]> GetAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery
        => _accountRepository.GetAsync(query);

    public virtual Task<Account[]> GetAsync(IEnumerable<Guid> ids)
        => _accountRepository.GetAsync(ids);

    public virtual Task<int> SaveChangesAsync()
        => _accountRepository.SaveChangesAsync();
}
