using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;

public record GetReferenceItemsRequest(
    string? FaFilter,
    string? EnFilter,
    List<SupplyType> SupplyTypes,
    int PageIndex,
    int PageSize) : IHttpRequest;