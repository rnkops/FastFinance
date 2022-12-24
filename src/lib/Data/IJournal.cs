using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.Data;

public interface IJournal
{
    Task AppendAsync<TTransaction>(TTransaction transaction) where TTransaction : ITransaction;
    Task<TTransaction?> FindAsync<TTransaction>(Guid id) where TTransaction : ITransaction;
    Task<TTransaction?> FindAsync<TTransaction>(long serial) where TTransaction : ITransaction;
    Task<TTransaction?> FindAsync<TTransaction>(string externalId) where TTransaction : ITransaction;
    Task<TTransaction[]> GetAsync<TTransaction, TQuery>(TQuery query)
        where TTransaction : Transaction
        where TQuery : IGetTransactionsQuery<TTransaction>;
    Task<int> SaveChangesAsync<TTransaction>() where TTransaction : ITransaction;
}
