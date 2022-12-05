namespace FastFinance.Dtos;

public interface IGetAccountsQuery
{
    public Guid[]? Ids { get; set; }
    public string[]? ExternalIds { get; set; }
    public string[]? Numbers { get; set; }
    public string? Query { get; set; }
    public long? SerialGte { get; set; }
    public long? SerialLte { get; set; }
    public DateTimeOffset? CreatedAtGte { get; set; }
    public DateTimeOffset? CreatedAtLte { get; set; }
}
