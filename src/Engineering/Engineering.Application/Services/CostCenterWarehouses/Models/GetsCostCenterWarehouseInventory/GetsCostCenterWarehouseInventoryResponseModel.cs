
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;

public record GetsCostCenterWarehouseInventoryResponseModel
{
    public long? WarehouseId { get; set; }
    public string? WarehouseCode { get; set; } = string.Empty;
    public string? WarehouseName { get; set; } = string.Empty;
    public double? InStockCount { get; set; }
    public double? RequestQuantity { get; set; }
    public bool IsDefault { get; set; } = false;
}