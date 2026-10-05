namespace Engineering.Application.Services.CostCenters.Models.GetCostCenters;

public record GetCostCentersRequest(
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
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
