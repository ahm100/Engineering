using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.ReviewReports;

public interface IReviewReportRepository
{
    Task<(List<GetReferenceItemsModel>? Data, int RowCount)> GetReferenceItemReport(
       string? faFilterData,
       string? enFilterData,
       List<SupplyType> supplyTypes,
       long companyId,
       int pageIndex,
       int pageSize,
       CT ct);
}