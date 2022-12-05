using FastFinance.Models;

namespace FastFinance.Dtos;

public interface IGetTransactionsQuery<TTransaction> where TTransaction : Transaction
{
    public Guid[]? Ids { get; set; }
    public string[]? ExternalIds { get; set; }
    public long[]? Serials { get; set; }
    public long? SerialGte { get; set; }
    public long? SerialLte { get; set; }
    public DateTimeOffset? TimestampGte { get; set; }
    public DateTimeOffset? TimestampLte { get; set; }
    public DateTimeOffset? CreatedAtGte { get; set; }
    public DateTimeOffset? CreatedAtLte { get; set; }
    public string? Note { get; set; }
}
