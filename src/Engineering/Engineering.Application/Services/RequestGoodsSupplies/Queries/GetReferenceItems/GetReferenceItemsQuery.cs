using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetReferenceItems;

public record GetReferenceItemsQuery(
    string? FaFilter,
    string? EnFilter,
    List<SupplyType> SupplyTypes,
    int PageIndex,
    int PageSize) : IQuery<GetReferenceItemsResponse?>;