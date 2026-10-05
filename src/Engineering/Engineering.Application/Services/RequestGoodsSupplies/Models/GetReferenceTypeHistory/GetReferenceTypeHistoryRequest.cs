using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;

public record GetReferenceTypeHistoryRequest(long ReferenceId,
    List<RGSTypeStatus>? Statuses,
    SupplyType SupplyType,
    int PageIndex,
    int PageSize) : IHttpRequest;