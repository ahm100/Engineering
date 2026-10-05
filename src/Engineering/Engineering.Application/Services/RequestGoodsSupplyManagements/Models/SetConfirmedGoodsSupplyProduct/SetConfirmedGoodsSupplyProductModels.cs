
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;

public record SetConfirmedGoodsSupplyProductModel
{
    public long? AlternateId { get; set; }
    public List<GoodsSupplyProductModelInStock>? InStocks { get; set; }
    public List<GoodsSupplyProductModelBetweenStock>? BetweenStocks { get; set; }
    public GoodsSupplyProductModelCommerce? Commerce { get; set; }
};

public record GoodsSupplyProductModelInStock
{
    public long WarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public string? Description { get; set; }
};

public record GoodsSupplyProductModelBetweenStock
{
    public long WarehouseId { get; set; }
    public long DestinationWarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public string? Description { get; set; }
};

public record GoodsSupplyProductModelCommerce
{
    public long DestinationWarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public string? CommerceDescription { get; set; } = string.Empty;
};
