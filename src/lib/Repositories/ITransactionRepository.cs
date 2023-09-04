using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Repositories;

public interface ITransactionRepository<TTransaction> where TTransaction : ITransaction
{
    Task<TTransaction?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TTransaction?> FindAsync(long serial, CancellationToken cancellationToken = default);
    Task<TTransaction?> FindAsync(string externalId, CancellationToken cancellationToken = default);
    Task AddAsync(TTransaction transaction, CancellationToken cancellationToken = default);
    Task UpdateAsync(TTransaction transaction, CancellationToken cancellationToken = default);
    Task<TTransaction[]> GetAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IGetTransactionsQuery<TTransaction>;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
