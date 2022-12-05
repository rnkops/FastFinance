using FastFinance.Models;

namespace FastFinance.Repositories;

public interface ITransactionRepositoryFactory
{
    ITransactionRepository<TTransaction> Create<TTransaction>() where TTransaction : Transaction;
}
