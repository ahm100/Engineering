namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetsDestinationWarehouse;

public record GetsDestinationWarehouseModel
{
    public long? WarehouseId { get; set; }
    public string? WarehouseCode { get; set; } = string.Empty;
    public string? WarehouseName { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public decimal? RequestedCount { get; set; }
    public double? InStockCount { get; set; }
    public double? RequestQuantity { get; set; }
    public bool IsDefault { get; set; } = false;
}
