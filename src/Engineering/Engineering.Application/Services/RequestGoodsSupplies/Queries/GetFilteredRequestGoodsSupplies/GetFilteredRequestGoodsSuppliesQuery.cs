using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFilteredRequestGoodsSupplies;

public record GetFilteredRequestGoodsSuppliesQuery(
    List<long>? Ids,
    List<long>? DetailIds,
    List<long>? ManagementIds,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? CityId,
    long? ProjectManagerId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    string? FilterData,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterDescription,
    string? FilterPublicName,
    string? FilterOperationInfoName,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupply>>>;
