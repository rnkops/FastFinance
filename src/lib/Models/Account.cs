namespace FastFinance.Models;

public class Account : IAccount
{
    public Guid Id { get; set; }
    public long Serial { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Number { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Dictionary<string, decimal> Credits { get; } = new Dictionary<string, decimal>();
    public Dictionary<string, decimal> Debits { get; } = new Dictionary<string, decimal>();
    public Dictionary<string, decimal> Balances
    {
        get
        {
            var balances = new Dictionary<string, decimal>();
            foreach (var credit in Credits)
            {
                if (!balances.ContainsKey(credit.Key))
                    balances[credit.Key] = 0;
                balances[credit.Key] += credit.Value;
            }
            foreach (var debit in Debits)
            {
                if (!balances.ContainsKey(debit.Key))
                    balances[debit.Key] = 0;
                balances[debit.Key] -= debit.Value;
            }
            return balances;
        }
    }

    public virtual bool TryAppend<TEntry>(TEntry entry) where TEntry : IEntry
    {
        if (entry.AccountId != Id)
            return false;
        if (entry.Type == EntryType.Credit)
        {
            if (!Credits.ContainsKey(entry.Currency))
                Credits[entry.Currency] = 0;
            Credits[entry.Currency] += entry.Amount;
        }
        else
        {
            if (!Debits.ContainsKey(entry.Currency))
                Debits[entry.Currency] = 0;
            Debits[entry.Currency] += entry.Amount;
        }
        return true;
    }
}
