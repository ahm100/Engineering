namespace Engineering.Application.Services.ProjectWarehouses.Contracts;

public record ProjectWarehouseModel
{
    public long ProjectWarehouseId { get; init; }
    public long WarehouseId { get; init; }
    public long? WarehouseTypeId { get; set; }
    public long? ManagerId { get; set; }
    public string? ManagerContact { get; set; }
    public string? ManagerFullName { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public bool IsDefault { get; init; }
}
