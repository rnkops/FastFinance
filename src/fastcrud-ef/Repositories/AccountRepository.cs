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

    public virtual Task AddAsync(TModel account, CancellationToken cancellationToken = default)
    {
        var entity = new TEntity();
        entity.FromModel(account);
        return base.AddAsync(entity, cancellationToken);
    }

    public virtual Task<bool> AnyAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IGetAccountsQuery
    {
        if (query is not GetAccountsQuery<TEntity> getAccountsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        return base.ExistsAsync(getAccountsQuery, cancellationToken);
    }

    public virtual async Task<TModel?> FindAsync(string externalId, CancellationToken cancellationToken = default)
    {
        var account = await FirstOrDefaultAsync(x => x.ExternalId == externalId && x.DeletedAt == null, cancellationToken);
        return account?.ToModel<TModel>();
    }

    public virtual async Task<TModel[]> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var accounts = await GetFilteredAsync(x => ids.Contains(x.Id) && x.DeletedAt == null, 0, int.MaxValue, cancellationToken);
        return accounts.Select(x => x.ToModel<TModel>()).ToArray();
    }

    public virtual async Task<string> GetMaxNumberAsync(Guid? parentId, CancellationToken cancellationToken = default)
    {
        var res = await GetFilteredAndOrderedDescendinglyAsync(x => x.Number, x => x.ParentId == parentId && x.DeletedAt == null, x => x.Serial, 0, 1, cancellationToken);
        return res.FirstOrDefault() ?? string.Empty;
    }

    Task<int> IAccountRepository<TModel>.SaveChangesAsync(CancellationToken cancellationToken)
        => Context.SaveChangesAsync(cancellationToken);

    public virtual Task UpdateAsync(TModel account, CancellationToken cancellationToken = default)
    {
        var entity = new TEntity();
        entity.FromModel(account);
        return base.UpdateAsync(entity, cancellationToken);
    }

    public virtual Task UpdateAsync(IEnumerable<TModel> accounts, CancellationToken cancellationToken = default)
    {
        var entities = accounts.Select(x =>
        {
            var entity = new TEntity();
            entity.FromModel(x);
            return entity;
        });
        return base.UpdateAsync(entities, cancellationToken);
    }

    async Task<TModel?> IAccountRepository<TModel>.FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await base.FindAsync(id, cancellationToken);
        return account?.ToModel<TModel>();
    }

    async Task<TModel[]> IAccountRepository<TModel>.GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken)
    {
        if (query is not GetAccountsQuery<TEntity> getAccountsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        var accounts = await base.GetAsync(getAccountsQuery, cancellationToken);
        return accounts.Select(x => x.ToModel<TModel>()).ToArray();
    }
}

public class AccountRepository : AccountRepository<Models.Account, Entities.Account>, IAccountRepository
{
    public AccountRepository(BaseDbContext context) : base(context)
    {
    }
}
