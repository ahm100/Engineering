using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetReferenceTypeHistory;

public record GetReferenceTypeHistoryQuery(
    long ReferenceId,
    List<RGSTypeStatus>? Statuses,
    SupplyType SupplyType,
    int PageIndex,
    int PageSize) : IQuery<GetReferenceTypeHistoryResponse?>;
