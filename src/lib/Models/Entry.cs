namespace FastFinance.Models;

public class Entry : IEntry
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
}
