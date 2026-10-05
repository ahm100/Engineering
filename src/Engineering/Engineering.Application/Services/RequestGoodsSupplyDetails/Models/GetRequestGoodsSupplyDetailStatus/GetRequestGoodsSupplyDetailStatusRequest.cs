using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailStatus;

public record GetRequestGoodsSupplyDetailStatusRequest(
    List<GoodsSupplyDetailStatus>? Statuses
    ) : IHttpRequest;
