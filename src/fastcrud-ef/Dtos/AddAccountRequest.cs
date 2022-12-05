using FastFinance.Dtos;

namespace FastFinance.FastCrud.EFCore.Dtos;

public class AddAccountRequest : IAddAccountRequest
{
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Number { get; set; }
    public string? Description { get; set; }
    public string? ExternalId { get; set; }
}
