using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagement;

public record UpdateRequestGoodsSupplyManagementRequest : IHttpRequest
{
    public long RequestGoodsSupplyManagementId { get; set; }
    public string? Description { get; set; }
    public long? WarehouseId { get; set; }
    public long? DestinationWarehouseId { get; set; }
    public decimal RequestedCount { get; set; }
    public GoodsSupplyManagementType Type { get; set; }
};
