using FastFinance.Models;

namespace FastFinance.Repositories;

public interface IEntryRepository
{
    Task<Entry[]> GetOfTransactionAsync(Guid transactionId, CancellationToken cancellationToken = default);
    Task<Dictionary<Guid, Entry[]>> GetOfTransactionsAsync(IEnumerable<Guid> transactionIds, CancellationToken cancellationToken = default);
    Task AddAsync(Entry entry, CancellationToken cancellationToken = default);
    Task AddAsync(IEnumerable<Entry> entries, CancellationToken cancellationToken = default);
}
