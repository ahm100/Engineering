using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyStatus;

public record GetRequestGoodsSupplyStatusRequest(
    List<GoodsSupplyStatus>? Statuses
    ) : IHttpRequest;
