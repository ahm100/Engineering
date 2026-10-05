using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetDetailByRGSTypeId;

public record GetDetailByRGSTypeIdQuery(
    long Id,
    List<RGSTypeStatus>? Statuses,
    int PageIndex,
    int PageSize) : IQuery<GetDetailByRGSTypeIdResponse?>;