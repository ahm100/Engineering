using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;

public record GetFilteredRequestGoodsSuppliesReportsRequest(
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    List<GoodsSupplyStatus>? Statuses,
    List<long>? ProductIds,
    GoodsSupplyManagementType? Type,
    VolumeProductType? ProductType,
    long? ProductGroupId,
    DateTime? FromDate,
    DateTime? ToDate,
    long? CreatorId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
