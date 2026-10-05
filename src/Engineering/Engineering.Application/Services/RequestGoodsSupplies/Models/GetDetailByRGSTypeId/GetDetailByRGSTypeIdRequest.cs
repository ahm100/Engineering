using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;

public record GetDetailByRGSTypeIdRequest(
    long Id,
    List<RGSTypeStatus>? Statuses,
    int PageIndex,
    int PageSize) : IHttpRequest;