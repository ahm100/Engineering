using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSTypeByRGSId;

public record GetRGSTypeByRGSIdQuery(
    long Id,
    int PageIndex,
    int PageSize) : IQuery<GetRGSTypeByRGSIdResponse?>;