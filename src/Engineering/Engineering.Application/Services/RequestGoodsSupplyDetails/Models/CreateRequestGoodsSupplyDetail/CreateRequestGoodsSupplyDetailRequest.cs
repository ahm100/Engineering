using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;

public record CreateRequestGoodsSupplyDetailRequest(
    long? RequestGoodsSupplyId,
    List<CreateRequestGoodsSupplyDetailModel> Details
    ) : IHttpRequest;

public record CreateRequestGoodsSupplyDetailModelRequest(
    long? RequestGoodsSupplyId,
    RequestGoodsSupply? RequestGoodsSupply,
    List<CreateRequestGoodsSupplyDetailModel> Details
    );
