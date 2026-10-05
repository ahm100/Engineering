using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFilteredRequestGoodsSuppliesReports;

public record GetFilteredRequestGoodsSuppliesReportsQuery(
    List<long>? Ids,
    List<long>? DetailIds,
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
    long? CompanyId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupply>>>;
