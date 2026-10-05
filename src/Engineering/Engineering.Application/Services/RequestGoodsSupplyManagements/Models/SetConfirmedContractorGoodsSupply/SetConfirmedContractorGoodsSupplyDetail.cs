
namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedContractorGoodsSupply;

public record SetConfirmedContractorGoodsSupplyDetail
{
    public long RequestGoodsSupplyDetailId { get; set; }
    public long DestinationWarehouseId { get; set; }
    public string? CommerceDescription { get; set; } = string.Empty;
};
