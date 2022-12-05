using FastCrud.Kernel.Entities;

namespace FastFinance.FastCrud.EFCore.Entities;

public class Account : IDeletableEntity<Guid>, IHasCreatedAt, IHasSerial
{
    public Guid Id { get; set; }
    public long Serial { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Dictionary<string, decimal> Credits { get; set; } = new();
    public Dictionary<string, decimal> Debits { get; set; } = new();

    public static Account FromModel(Models.Account account)
        => new()
        {
            Id = account.Id,
            ParentId = account.ParentId,
            Name = account.Name,
            Number = account.Number,
            ExternalId = account.ExternalId,
            Serial = account.Serial,
            Credits = account.Credits,
            Debits = account.Debits,
            CreatedAt = account.CreatedAt,
            DeletedAt = account.DeletedAt,
            Description = account.Description,
        };

    public Models.Account ToModel()
    {
        var model = new Models.Account
        {
            Id = Id,
            ParentId = ParentId,
            Name = Name,
            Number = Number,
            ExternalId = ExternalId,
            Serial = Serial,
            DeletedAt = DeletedAt,
            CreatedAt = CreatedAt,
            Description = Description,
        };
        foreach (var credit in Credits)
            model.Credits[credit.Key] = credit.Value;
        foreach (var debit in Debits)
            model.Debits[debit.Key] = debit.Value;
        return model;
    }
}
