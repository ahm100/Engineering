namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public record GoodsSupplyManagementModelInStock
{
    public long WarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public string? Description { get; set; }
};
