using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetDetailByRGSId;

public record GetDetailByRGSIdQuery(
    long Id,
    int PageIndex,
    int PageSize) : IQuery<GetDetailByRGSIdResponse?>;