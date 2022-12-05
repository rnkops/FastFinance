using FastFinance.Models;

namespace FastFinance.FastCrud.EFCore.Dtos;

public class AddTransactionRequest : BaseAddTransactionRequest<Transaction>
{
    public override Transaction GetTransaction()
    {
        var transaction = new Transaction
        {
            ExternalId = ExternalId,
            Timestamp = Timestamp == default ? DateTimeOffset.UtcNow : Timestamp,
            Note = Note,
            Id = Guid.NewGuid(),
        };
        foreach (var entry in Entries)
        {
            transaction.Append(new Entry
            {
                AccountId = entry.AccountId,
                Amount = entry.Amount,
                Note = entry.Note,
                Type = entry.Type,
                TransactionId = transaction.Id,
                Currency = entry.Currency,
                ExternalId = entry.ExternalId,
                Id = Guid.NewGuid(),
            });
        }
        return transaction;
    }
}
