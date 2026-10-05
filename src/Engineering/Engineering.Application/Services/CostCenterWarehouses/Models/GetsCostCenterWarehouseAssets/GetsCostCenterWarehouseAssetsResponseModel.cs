
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;

public record GetsCostCenterWarehouseAssetsResponseModel
{
    public long? WarehouseId { get; set; }
    public string? WarehouseCode { get; set; } = string.Empty;
    public string? WarehouseName { get; set; } = string.Empty;
    public double? InStockCount { get; set; }
    public double? RequestQuantity { get; set; }
    public bool IsDefault { get; set; } = false;
}
