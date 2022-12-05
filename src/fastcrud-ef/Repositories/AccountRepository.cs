using FastCrud.Persistence.EFCore.Data;
using FastCrud.Persistence.EFCore.Repositories;
using FastFinance.Dtos;
using FastFinance.FastCrud.EFCore.Dtos;
using FastFinance.FastCrud.EFCore.Entities;
using FastFinance.Repositories;

namespace FastFinance.FastCrud.EFCore.Repositories;

public class AccountRepository : CRURepository<Account, Guid>, IAccountRepository
{
    public AccountRepository(BaseDbContext context) : base(context)
    {
    }

    public Task AddAsync(Models.Account account)
        => base.AddAsync(Account.FromModel(account));

    public Task<bool> AnyAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery
    {
        if (query is not GetAccountsQuery getAccountsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        return base.ExistsAsync(getAccountsQuery);
    }

    public async Task<Models.Account?> FindAsync(string externalId)
    {
        var account = await FirstOrDefaultAsync(x => x.ExternalId == externalId && x.DeletedAt == null);
        return account?.ToModel();
    }

    public async Task<Models.Account[]> GetAsync(IEnumerable<Guid> ids)
    {
        var accounts = await GetFilteredAsync(x => ids.Contains(x.Id) && x.DeletedAt == null, 0, int.MaxValue);
        return accounts.Select(x => x.ToModel()).ToArray();
    }

    public async Task<string> GetMaxNumberAsync(Guid? parentId)
    {
        var res = await GetFilteredAndOrderedDescendinglyAsync(x => x.Number, x => x.ParentId == parentId && x.DeletedAt == null, x => x.Serial, 0, 1);
        return res.FirstOrDefault() ?? string.Empty;
    }

    public Task<int> SaveChangesAsync()
        => Context.SaveChangesAsync();

    public Task UpdateAsync(Models.Account account)
    {
        var entity = Account.FromModel(account);
        return base.UpdateAsync(entity);
    }

    public Task UpdateAsync(IEnumerable<Models.Account> accounts)
    {
        var entities = accounts.Select(Account.FromModel);
        return base.UpdateAsync(entities);
    }

    async Task<Models.Account?> IAccountRepository.FindAsync(Guid id)
    {
        var account = await base.FindAsync(id);
        return account?.ToModel();
    }

    async Task<Models.Account[]> IAccountRepository.GetAsync<TQuery>(TQuery query)
    {
        if (query is not GetAccountsQuery getAccountsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        var accounts = await base.GetAsync(getAccountsQuery);
        return accounts.Select(x => x.ToModel()).ToArray();
    }
}
