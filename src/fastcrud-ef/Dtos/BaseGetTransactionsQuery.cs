using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Entities;
using FastCrud.Kernel.Extensions;
using FastFinance.Dtos;
using FastFinance.FastCrud.EFCore.Entities;

namespace FastFinance.FastCrud.EFCore.Dtos;

public abstract class BaseGetTransactionsQuery<TTransaction, TEntity> : BaseGetQuery<TEntity, Guid>, IGetTransactionsQuery<TTransaction> where TTransaction : Models.Transaction where TEntity : Transaction<TEntity>
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

    public override IQueryable<TEntity> GetFiltered(IQueryable<TEntity> queryable)
    {
        var trimmedNote = Note?.Trim().ToLower();
        return queryable
            .ConditionalWhere(Ids?.Length > 0, x => Ids!.Contains(x.Id))
            .ConditionalWhere(ExternalIds?.Length > 0, x => ExternalIds!.Contains(x.ExternalId))
            .ConditionalWhere(Serials?.Length > 0, x => Serials!.Contains(x.Serial))
            .ConditionalWhere(SerialGte.HasValue, x => x.Serial >= SerialGte)
            .ConditionalWhere(SerialLte.HasValue, x => x.Serial <= SerialLte)
            .ConditionalWhere(TimestampGte.HasValue, x => x.Timestamp >= TimestampGte)
            .ConditionalWhere(TimestampLte.HasValue, x => x.Timestamp <= TimestampLte)
            .ConditionalWhere(CreatedAtGte.HasValue, x => x.CreatedAt >= CreatedAtGte)
            .ConditionalWhere(CreatedAtLte.HasValue, x => x.CreatedAt <= CreatedAtLte)
            .ConditionalWhere(!string.IsNullOrEmpty(trimmedNote), x => x.Note != null && x.Note.ToLower().Contains(trimmedNote!));
    }
}
