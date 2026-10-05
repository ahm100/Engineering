
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public record GoodsSupplyManagementModelBetweenStock
{
    public long WarehouseId { get; set; }
    public long DestinationWarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public string? Description { get; set; }
};
