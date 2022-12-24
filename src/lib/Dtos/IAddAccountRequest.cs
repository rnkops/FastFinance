using FastFinance.Models;

namespace FastFinance.Dtos;

public interface IAddAccountRequest
{
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public string? Number { get; set; }
    public string? ExternalId { get; set; }
    public string? Description { get; set; }

    public TAccount GetAccount<TAccount>() where TAccount : IAccount, new();
}
