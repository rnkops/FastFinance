namespace FastFinance.Models;

public interface ITransaction
{
    Guid Id { get; set; }
    long Serial { get; set; }
    string? ExternalId { get; set; }
    DateTimeOffset Timestamp { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    string? Note { get; set; }
    IReadOnlyList<Entry> Entries { get; }

    void Append(Entry entry);
    void Clear();
    bool IsValid();
    void Remove(Entry entry);
}
