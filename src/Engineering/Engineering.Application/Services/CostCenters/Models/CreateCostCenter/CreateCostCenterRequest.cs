using Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;

namespace Engineering.Application.Services.CostCenters.Models.CreateCostCenter;

public record CreateCostCenterRequest(
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
    List<CostCenterWarehouseRequest?>? CostCenterWarehouses,
    string? PostalCode,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? DescriptionEn,
    bool WeatherState,
    bool IsActive,
    bool? IsDefault,
    long? ProjectCostCenterRequestId = null
     ) : IHttpRequest;
