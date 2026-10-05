namespace Engineering.Application.Services.CostCenters.Models.UpdateCostCenter;

public record UpdateCostCenterRequest(
    long Id,
    long CostCenterTypeId,
    string CostCenterCode,
    string CostCenterName,
    string? CostCenterEnName,
    int? NoOperationDays,
    long CityId,
    string Address,
    List<long?>? InformedUsers,
    List<long?>? AuthorizedRoles,
    List<long?>? AuthorizedUsers,
    List<long>? ProjectIds,
    List<UpdateCostCenterWarehouseModel?>? CostCenterWarehouses,
    string? PostalCode,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? DescriptionEn,
    bool WeatherState,
    bool IsActive,
    bool? IsDefault
     ) : IHttpRequest;
