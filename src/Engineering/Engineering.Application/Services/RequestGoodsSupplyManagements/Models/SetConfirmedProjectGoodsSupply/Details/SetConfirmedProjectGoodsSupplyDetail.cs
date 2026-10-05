
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

public record SetConfirmedProjectGoodsSupplyDetail
{
    public long RequestGoodsSupplyProductId { get; set; }
    public long? AlternateId { get; set; }
    public List<GoodsSupplyManagementModelInStock>? InStocks { get; set; }
    public List<GoodsSupplyManagementModelBetweenStock>? BetweenStocks { get; set; }
    public GoodsSupplyManagementModelCommerce? Commerce { get; set; }
};
