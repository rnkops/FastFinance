using FastFinance.Dtos;
using FastFinance.Models;

namespace FastFinance.FastCrud.EFCore.Dtos;

public class AddAccountRequest : IAddAccountRequest
{
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Number { get; set; }
    public string? Description { get; set; }
    public string? ExternalId { get; set; }

    public virtual TAccount GetAccount<TAccount>() where TAccount : IAccount, new()
    {
        var account = new TAccount
        {
            Id = Id ?? Guid.NewGuid(),
            ParentId = ParentId,
            Name = Name,
            Description = Description,
            ExternalId = ExternalId,
        };
        return account;
    }
}
