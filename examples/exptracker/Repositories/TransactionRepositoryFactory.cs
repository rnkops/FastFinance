using FastFinance.FastCrud.EFCore.Repositories;
using FastFinance.Models;
using FastFinance.Repositories;

namespace ExpenseTracker.Repositories;

public class TransactionRepositoryFactory : ITransactionRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TransactionRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITransactionRepository<TTransaction> Create<TTransaction>() where TTransaction : Transaction
    {
        var type = typeof(TTransaction);
        if (type == typeof(Transaction))
            return (_serviceProvider.GetRequiredService<TransactionRepository>() as ITransactionRepository<TTransaction>)!;
        throw new NotImplementedException();
    }
}
