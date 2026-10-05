using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;

public record GetRequestGoodsSupplyProductRequest(
    long Id,
    GoodsSupplyManagementType? Type,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;