using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenters;

public record GetCostCentersQuery(
    List<long>? Ids,
    string? FilterData,
    string? CostCenterName,
    string? CostCenterCode,
    long? CostCenterTypeId,
    long? InformedUserId,
    long? AuthorizedRoleId,
    long? AuthorizedUserId,
    long? WarehouseId,
    long? CityId,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetCostCentersModel>>>;