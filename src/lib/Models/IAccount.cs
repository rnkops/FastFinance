namespace FastFinance.Models;

public interface IAccount
{
    Guid Id { get; set; }
    long Serial { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
    string Number { get; set; }
    Guid? ParentId { get; set; }
    string? ExternalId { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    Dictionary<string, decimal> Credits { get; }
    Dictionary<string, decimal> Debits { get; }
    Dictionary<string, decimal> Balances { get; }

    bool TryAppend<TEntry>(TEntry entry) where TEntry : IEntry;
}
