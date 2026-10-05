namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public record GoodsSupplyManagementModelCommerce
{
    public long? RequestGoodsSupplyProductId { get; set; }
    public long DestinationWarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public string? CommerceDescription { get; set; } = string.Empty;
};
