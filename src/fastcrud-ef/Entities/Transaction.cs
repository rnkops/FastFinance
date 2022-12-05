using FastCrud.Kernel.Entities;

namespace FastFinance.FastCrud.EFCore.Entities;

public class Transaction : IEntity<Guid>, IHasCreatedAt, IHasSerial
{
    public Guid Id { get; set; }
    public long Serial { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

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
    {
        var transaction = new Models.Transaction
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
