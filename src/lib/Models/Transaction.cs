namespace FastFinance.Models;

public class Transaction
{
    private readonly List<Entry> _entries = new();
    public Guid Id { get; set; }
    public long Serial { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? Note { get; set; }
    public IReadOnlyList<Entry> Entries => _entries;

    public virtual void Append(Entry entry)
    {
        _entries.Add(entry);
    }

    public virtual void Remove(Entry entry)
    {
        _entries.Remove(entry);
    }

    public virtual void Clear()
    {
        _entries.Clear();
    }

    public virtual bool IsValid()
    {
        var diffByCurrency = new Dictionary<string, decimal>();
        foreach (var entry in _entries)
        {
            if (!diffByCurrency.ContainsKey(entry.Currency))
                diffByCurrency[entry.Currency] = 0;
            diffByCurrency[entry.Currency] += entry.Amount * (int)entry.Type;
        }
        return diffByCurrency.Values.All(diff => diff == 0);
    }
}
