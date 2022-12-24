using FastCrud.Kernel.Entities;

namespace FastFinance.FastCrud.EFCore.Entities;

public class Transaction<TEntity> : IEntity<Guid>, IHasCreatedAt, IHasSerial where TEntity : Transaction<TEntity>
{
    public Guid Id { get; set; }
    public long Serial { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public virtual void FromModel<TTransaction>(TTransaction transaction) where TTransaction : Models.Transaction
    {
        Id = transaction.Id;
        Serial = transaction.Serial;
        ExternalId = transaction.ExternalId;
        Timestamp = transaction.Timestamp;
        Note = transaction.Note;
    }

    public virtual TTransaction ToModel<TTransaction>(IEnumerable<Entry> entries) where TTransaction : Models.Transaction, new()
    {
        var transaction = new TTransaction
        {
            Id = Id,
            Serial = Serial,
            ExternalId = ExternalId,
            Timestamp = Timestamp,
            Note = Note,
        };
        foreach (var entry in entries)
            transaction.Append(entry.ToModel());
        return transaction;
    }
}

public class Transaction : Transaction<Transaction>, IHasCreatedAt, IHasSerial
{
    public static Transaction FromModel(Models.Transaction transaction)
        => new()
        {
            Id = transaction.Id,
            Serial = transaction.Serial,
            ExternalId = transaction.ExternalId,
            Timestamp = transaction.Timestamp,
            Note = transaction.Note,
        };

    public Models.Transaction ToModel(IEnumerable<Entry> entries)
        => base.ToModel<Models.Transaction>(entries);
}
