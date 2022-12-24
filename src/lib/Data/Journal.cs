using FastFinance.Dtos;
using FastFinance.Models;
using FastFinance.Repositories;

namespace FastFinance.Data;

public abstract class Journal : IJournal
{
    protected readonly ITransactionRepositoryFactory _transactionRepositoryFactory;

    protected Journal(ITransactionRepositoryFactory transactionRepositoryFactory)
    {
        _transactionRepositoryFactory = transactionRepositoryFactory;
    }

    public virtual async Task AppendAsync<TTransaction>(TTransaction transaction) where TTransaction : ITransaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        await transactionRepository.AddAsync(transaction);
        await UpdateAccountsAsync(transaction);
        await transactionRepository.SaveChangesAsync();
    }

    public virtual Task<TTransaction?> FindAsync<TTransaction>(Guid id) where TTransaction : ITransaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.FindAsync(id);
    }

    public virtual Task<TTransaction?> FindAsync<TTransaction>(long serial) where TTransaction : ITransaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.FindAsync(serial);
    }

    public virtual Task<TTransaction?> FindAsync<TTransaction>(string externalId) where TTransaction : ITransaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.FindAsync(externalId);
    }

    public virtual Task<TTransaction[]> GetAsync<TTransaction, TQuery>(TQuery query) where TTransaction : Transaction where TQuery : IGetTransactionsQuery<TTransaction>
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.GetAsync(query);
    }

    public virtual Task<int> SaveChangesAsync<TTransaction>() where TTransaction : ITransaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.SaveChangesAsync();
    }

    protected abstract Task UpdateAccountsAsync<TTransaction>(TTransaction transaction) where TTransaction : ITransaction;
}
