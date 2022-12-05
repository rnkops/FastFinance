using FastCrud.Persistence.EFCore.Data;

namespace FastFinance.FastCrud.EFCore.Repositories;

public class TransactionRepository : BaseTransactionRepository<Models.Transaction, Entities.Transaction>
{
    public TransactionRepository(BaseDbContext context) : base(context)
    {
    }

    protected override Entities.Transaction FromModel(Models.Transaction transaction)
        => Entities.Transaction.FromModel(transaction);

    protected override Models.Transaction ToModel(Entities.Transaction entity, IEnumerable<Entities.Entry> entries)
        => entity.ToModel(entries);
}
