using FastCrud.Kernel.Dtos;
using FastCrud.Kernel.Extensions;
using FastFinance.Dtos;
using FastFinance.FastCrud.EFCore.Entities;

namespace FastFinance.FastCrud.EFCore.Dtos;

public class GetAccountsQuery<TEntity> : BaseGetQuery<TEntity, Guid>, IGetAccountsQuery where TEntity : Account<TEntity>, new()
{
    public Guid[]? Ids { get; set; }
    public string[]? ExternalIds { get; set; }
    public string[]? Numbers { get; set; }
    public long? SerialGte { get; set; }
    public long? SerialLte { get; set; }
    public DateTimeOffset? CreatedAtGte { get; set; }
    public DateTimeOffset? CreatedAtLte { get; set; }

    public override IQueryable<TEntity> GetFiltered(IQueryable<TEntity> queryable)
    {
        var trimmedQuery = Query?.Trim().ToLower();
        return queryable
            .ConditionalWhere(Ids?.Length > 0, x => Ids!.Contains(x.Id))
            .ConditionalWhere(ExternalIds?.Length > 0, x => ExternalIds!.Contains(x.ExternalId))
            .ConditionalWhere(Numbers?.Length > 0, x => Numbers!.Contains(x.Number))
            .ConditionalWhere(!string.IsNullOrEmpty(trimmedQuery), x => x.Name.ToLower().Contains(trimmedQuery!))
            .ConditionalWhere(SerialGte.HasValue, x => x.Serial >= SerialGte)
            .ConditionalWhere(SerialLte.HasValue, x => x.Serial <= SerialLte);
    }
}

public class GetAccountsQuery : GetAccountsQuery<Account>, IGetAccountsQuery
{
}
