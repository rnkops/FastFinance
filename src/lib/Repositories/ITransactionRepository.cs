using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Repositories;

public interface ITransactionRepository<TTransaction> where TTransaction : Transaction
{
    Task<TTransaction?> FindAsync(Guid id);
    Task<TTransaction?> FindAsync(long serial);
    Task<TTransaction?> FindAsync(string externalId);
    Task AddAsync(TTransaction transaction);
    Task UpdateAsync(TTransaction transaction);
    Task<TTransaction[]> GetAsync<TQuery>(TQuery query) where TQuery : IGetTransactionsQuery<TTransaction>;
    Task<int> SaveChangesAsync();
}
