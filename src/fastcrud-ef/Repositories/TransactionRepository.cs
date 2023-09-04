using FastCrud.Kernel.Entities;
using FastCrud.Persistence.EFCore.Data;
using FastCrud.Persistence.EFCore.Repositories;
using FastFinance.FastCrud.EFCore.Dtos;
using FastFinance.Models;
using FastFinance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastFinance.FastCrud.EFCore.Repositories;

public abstract class TransactionRepository<TTransaction, TEntity> : CRRepository<TEntity, Guid>, ITransactionRepository<TTransaction> where TTransaction : Transaction where TEntity : Entities.Transaction<TEntity>, IEntity<Guid>, IHasSerial, IHasCreatedAt
{
    protected TransactionRepository(BaseDbContext context) : base(context)
    {
    }

    public virtual async Task AddAsync(TTransaction transaction, CancellationToken cancellationToken = default)
    {
        var entity = FromModel(transaction);
        var entries = GetEntryEntities(transaction);
        await base.AddAsync(entity, cancellationToken);
        await Context.Set<Entities.Entry>().AddRangeAsync(entries);
    }

    public virtual async Task<TTransaction?> FindAsync(long serial, CancellationToken cancellationToken = default)
    {
        var t = await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Serial == serial, cancellationToken);
        if (t == null)
            return null;
        var entries = await Context.Set<Entities.Entry>().Where(x => x.TransactionId == t.Id).ToArrayAsync(cancellationToken);
        return ToModel(t, entries);
    }

    public virtual async Task<TTransaction?> FindAsync(string externalId, CancellationToken cancellationToken = default)
    {
        var t = await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);
        if (t == null)
            return null;
        var entries = await Context.Set<Entities.Entry>().Where(x => x.TransactionId == t.Id).ToArrayAsync(cancellationToken);
        return ToModel(t, entries);
    }

    public virtual Task UpdateAsync(TTransaction transaction, CancellationToken cancellationToken = default)
    {
        var entity = FromModel(transaction);
        Context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    async Task<TTransaction?> ITransactionRepository<TTransaction>.FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await base.FindAsync(id, cancellationToken);
        if (entity == null)
            return null;
        var entries = await Context.Set<Entities.Entry>().Where(e => e.TransactionId == id).ToListAsync(cancellationToken);
        return ToModel(entity, entries);
    }

    async Task<TTransaction[]> ITransactionRepository<TTransaction>.GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken)
    {
        if (query is not BaseGetTransactionsQuery<TTransaction, TEntity> getTransactionsQuery)
            throw new ArgumentException($"Invalid query type: {query.GetType().FullName}", nameof(query));
        var t = await GetAsync(getTransactionsQuery, cancellationToken);
        if (t.Length < 1)
            return Array.Empty<TTransaction>();
        var entries = (await Context.Set<Entities.Entry>().Where(e => t.Select(x => x.Id).Contains(e.TransactionId)).ToArrayAsync(cancellationToken))
            .GroupBy(e => e.TransactionId)
            .ToDictionary(g => g.Key, g => g.ToArray());
        return t.Select(x => ToModel(x, entries[x.Id])).ToArray();
    }

    Task<int> ITransactionRepository<TTransaction>.SaveChangesAsync(CancellationToken cancellationToken)
        => Context.SaveChangesAsync(cancellationToken);

    protected virtual Entities.Entry[] GetEntryEntities(TTransaction transaction)
        => transaction.Entries.Select(Entities.Entry.FromModel).ToArray();

    protected abstract TTransaction ToModel(TEntity entity, IEnumerable<Entities.Entry> entries);
    protected abstract TEntity FromModel(TTransaction transaction);
}

public class TransactionRepository : TransactionRepository<Transaction, Entities.Transaction>
{
    public TransactionRepository(BaseDbContext context) : base(context)
    {
    }

    protected override Entities.Transaction FromModel(Transaction transaction)
        => Entities.Transaction.FromModel(transaction);

    protected override Transaction ToModel(Entities.Transaction entity, IEnumerable<Entities.Entry> entries)
        => entity.ToModel(entries);
}
