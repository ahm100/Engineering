namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;

public record GetProjectWarehouseInventoryModel
{
    public long? WarehouseId { get; set; }
    public string? WarehouseCode { get; set; }
    public string? WarehouseName { get; set; }
    public double? InStockCount { get; set; }
    public double? RequestQuantity { get; set; }
    public bool IsDefault { get; set; }
}
