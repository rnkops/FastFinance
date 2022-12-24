namespace FastFinance.Models;

public interface IEntry
{
    Guid Id { get; set; }
    long Serial { get; set; }
    string? ExternalId { get; set; }
    Guid TransactionId { get; set; }
    Guid AccountId { get; set; }
    string Currency { get; set; }
    decimal Amount { get; set; }
    EntryType Type { get; set; }
    string? Note { get; set; }
}

public enum EntryType
{
    Debit = -1,
    Credit = 1
}
