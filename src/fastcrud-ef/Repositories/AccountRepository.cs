using FastCrud.Persistence.EFCore.Data;
using FastCrud.Persistence.EFCore.Repositories;
using FastFinance.Dtos;
using FastFinance.FastCrud.EFCore.Dtos;
using FastFinance.Repositories;

namespace FastFinance.FastCrud.EFCore.Repositories;

public class AccountRepository<TModel, TEntity> : CRURepository<TEntity, Guid>, IAccountRepository<TModel> where TModel : Models.Account, new() where TEntity : Entities.Account<TEntity>, new()
{
    public AccountRepository(BaseDbContext context) : base(context)
    {
    }

    public virtual Task AddAsync(TModel account)
    {
        var entity = new TEntity();
        entity.FromModel(account);
        return base.AddAsync(entity);
    }

    public virtual Task<bool> AnyAsync<TQuery>(TQuery query) where TQuery : IGetAccountsQuery
    {
        if (query is not GetAccountsQuery<TEntity> getAccountsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        return base.ExistsAsync(getAccountsQuery);
    }

    public virtual async Task<TModel?> FindAsync(string externalId)
    {
        var account = await FirstOrDefaultAsync(x => x.ExternalId == externalId && x.DeletedAt == null);
        return account?.ToModel<TModel>();
    }

    public virtual async Task<TModel[]> GetAsync(IEnumerable<Guid> ids)
    {
        var accounts = await GetFilteredAsync(x => ids.Contains(x.Id) && x.DeletedAt == null, 0, int.MaxValue);
        return accounts.Select(x => x.ToModel<TModel>()).ToArray();
    }

    public virtual async Task<string> GetMaxNumberAsync(Guid? parentId)
    {
        var res = await GetFilteredAndOrderedDescendinglyAsync(x => x.Number, x => x.ParentId == parentId && x.DeletedAt == null, x => x.Serial, 0, 1);
        return res.FirstOrDefault() ?? string.Empty;
    }

    public virtual Task<int> SaveChangesAsync()
        => Context.SaveChangesAsync();

    public virtual Task UpdateAsync(TModel account)
    {
        var entity = new TEntity();
        entity.FromModel(account);
        return base.UpdateAsync(entity);
    }

    public virtual Task UpdateAsync(IEnumerable<TModel> accounts)
    {
        var entities = accounts.Select(x =>
        {
            var entity = new TEntity();
            entity.FromModel(x);
            return entity;
        });
        return base.UpdateAsync(entities);
    }

    async Task<TModel?> IAccountRepository<TModel>.FindAsync(Guid id)
    {
        var account = await base.FindAsync(id);
        return account?.ToModel<TModel>();
    }

    async Task<TModel[]> IAccountRepository<TModel>.GetAsync<TQuery>(TQuery query)
    {
        if (query is not GetAccountsQuery<TEntity> getAccountsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        var accounts = await base.GetAsync(getAccountsQuery);
        return accounts.Select(x => x.ToModel<TModel>()).ToArray();
    }
}

public class AccountRepository : AccountRepository<Models.Account, Entities.Account>, IAccountRepository
{
    public AccountRepository(BaseDbContext context) : base(context)
    {
    }
}
