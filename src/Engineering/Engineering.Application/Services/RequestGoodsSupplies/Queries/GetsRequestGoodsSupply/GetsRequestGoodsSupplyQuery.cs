using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetsRequestGoodsSupply;

public record GetsRequestGoodsSupplyQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? CityId,
    long? ProjectManagerId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    string? FilterData,
    long? CreatorId,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupply>>>;
