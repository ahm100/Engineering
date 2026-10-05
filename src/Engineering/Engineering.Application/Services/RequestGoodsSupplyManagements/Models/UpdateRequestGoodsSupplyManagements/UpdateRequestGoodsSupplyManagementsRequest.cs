using Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagement;

namespace Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagements;

public record UpdateRequestGoodsSupplyManagementsRequest : IHttpRequest
{
    public required List<UpdateRequestGoodsSupplyManagementRequest> RequestGoodsSupplyManagements { get; set; }
};
