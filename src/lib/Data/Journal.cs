using FastFinance.Dtos;
using FastFinance.Models;
using FastFinance.Repositories;

namespace FastFinance.Data;

public class Journal
{
    protected readonly ITransactionRepositoryFactory _transactionRepositoryFactory;
    protected readonly ChartOfAccount _chartOfAccount;

    public Journal(ITransactionRepositoryFactory transactionRepositoryFactory, ChartOfAccount chartOfAccount)
    {
        _transactionRepositoryFactory = transactionRepositoryFactory;
        _chartOfAccount = chartOfAccount;
    }

    public virtual async Task AppendAsync<TTransaction>(TTransaction transaction) where TTransaction : Transaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        await transactionRepository.AddAsync(transaction);
        var accountIds = transaction.Entries.Select(e => e.AccountId).Distinct().ToArray();
        var accounts = (await _chartOfAccount.GetAsync(accountIds)).ToDictionary(x => x.Id);
        if (accounts.Count != accountIds.Length)
            throw new Exception("Account not found");
        foreach (var entry in transaction.Entries)
        {
            var account = accounts[entry.AccountId];
            _ = account.TryAppend(entry);
        }
        await _chartOfAccount.UpdateAsync(accounts.Values.ToArray());
        await _chartOfAccount.SaveChangesAsync();
    }

    public virtual Task<TTransaction?> FindAsync<TTransaction>(Guid id) where TTransaction : Transaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.FindAsync(id);
    }

    public virtual Task<TTransaction?> FindAsync<TTransaction>(long serial) where TTransaction : Transaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.FindAsync(serial);
    }

    public virtual Task<TTransaction?> FindAsync<TTransaction>(string externalId) where TTransaction : Transaction
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.FindAsync(externalId);
    }

    public virtual Task<TTransaction[]> GetAsync<TTransaction, TQuery>(TQuery query) where TTransaction : Transaction where TQuery : IGetTransactionsQuery<TTransaction>
    {
        var transactionRepository = _transactionRepositoryFactory.Create<TTransaction>();
        return transactionRepository.GetAsync(query);
    }

    public virtual Task<int> SaveChangesAsync()
    {
        return _chartOfAccount.SaveChangesAsync();
    }
}
