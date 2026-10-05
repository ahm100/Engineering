using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetReferenceTypeHistory;

public record GetReferenceTypeHistoryExcelRequest(long ReferenceId,
    List<GoodsSupplyDetailStatus>? Statuses,
    List<ReferenceTypeHistoryEnum>? ExcelFilters,
    SupplyType SupplyType,
    int PageIndex,
    int PageSize) : IHttpRequest;