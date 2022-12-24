using FastFinance.Data;
using FastFinance.Models;
using FastFinance.Repositories;

namespace ExpenseTracker.Data;

public class Journal : FastFinance.Data.Journal
{
    private readonly IChartOfAccounts _chartOfAccounts;

    public Journal(ITransactionRepositoryFactory transactionRepositoryFactory, IChartOfAccounts chartOfAccounts) : base(transactionRepositoryFactory)
    {
        _chartOfAccounts = chartOfAccounts;
    }

    protected override async Task UpdateAccountsAsync<TTransaction>(TTransaction transaction)
    {
        var accountIds = transaction.Entries.Select(x => x.AccountId).Distinct().ToArray();
        var accounts = (await _chartOfAccounts.GetAsync<Account>(accountIds)).ToDictionary(x => x.Id);
        foreach (var entry in transaction.Entries)
        {
            var account = accounts[entry.AccountId];
            _ = account.TryAppend(entry);
        }
        await _chartOfAccounts.UpdateAsync(accounts.Values);
    }
}
