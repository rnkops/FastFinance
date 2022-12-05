using FastFinance.Models;

namespace FastFinance.Repositories;

public interface IEntryRepository
{
    Task<Entry[]> GetOfTransactionAsync(Guid transactionId);
    Task<Dictionary<Guid, Entry[]>> GetOfTransactionsAsync(IEnumerable<Guid> transactionIds);
    Task AddAsync(Entry entry);
    Task AddAsync(IEnumerable<Entry> entries);
}
