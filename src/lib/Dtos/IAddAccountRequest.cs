namespace FastFinance.Dtos;

public interface IAddAccountRequest
{
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public string? Number { get; set; }
    public string? ExternalId { get; set; }
    public string? Description { get; set; }
}
