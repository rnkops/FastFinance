using FastCrud.Kernel.Entities;
using FastFinance.Models;

namespace FastFinance.FastCrud.EFCore.Entities;

public class Entry : IEntity<Guid>, IHasCreatedAt, IHasSerial
{
    public Guid Id { get; set; }
    public long Serial { get; set; }
    public string? ExternalId { get; set; }
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public EntryType Type { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public static Entry FromModel(Models.Entry entry)
        => new()
        {
            Id = entry.Id,
            AccountId = entry.AccountId,
            Amount = entry.Amount,
            Currency = entry.Currency,
            Note = entry.Note,
            TransactionId = entry.TransactionId,
            Type = entry.Type,
            ExternalId = entry.ExternalId,
            Serial = entry.Serial,
        };

    public Models.Entry ToModel()
        => new()
        {
            Id = Id,
            AccountId = AccountId,
            Amount = Amount,
            Currency = Currency,
            Note = Note,
            TransactionId = TransactionId,
            Type = Type,
            ExternalId = ExternalId,
            Serial = Serial,
        };
}
