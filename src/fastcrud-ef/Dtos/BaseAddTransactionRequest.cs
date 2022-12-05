using FastFinance.Models;

namespace FastFinance.FastCrud.EFCore.Dtos;

public abstract class BaseAddTransactionRequest<TTransaction> where TTransaction : Models.Transaction
{
    public string? ExternalId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string? Note { get; set; }
    public IEnumerable<AddEntryRequest> Entries { get; set; } = Array.Empty<AddEntryRequest>();

    public abstract TTransaction GetTransaction();
}

public class AddEntryRequest
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string? Note { get; set; }
    public string? ExternalId { get; set; }
    public EntryType Type { get; set; }
}

