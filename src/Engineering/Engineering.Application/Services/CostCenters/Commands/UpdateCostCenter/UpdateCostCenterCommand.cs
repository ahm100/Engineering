using Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;
using Engineering.Domain.Entities.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Commands.UpdateCostCenter;

public record UpdateCostCenterCommand(
    long Id,
    CostCenterType CostCenterType,
    string CostCenterCode,
    string CostCenterName,
    string? CostCenterEnName,
    List<long?>? InformedUsers,
    List<long?>? AuthorizedRoles,
    List<long?>? AuthorizedUsers,
    List<long>? ProjectIds,
    List<CostCenterWarehouseResponse?>? CostCenterWarehouses,
    int? NoOperationDays,
    long CityId,
    string Address,
    string? PostalCode,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? DescriptionEn,
    bool WeatherState,
    bool IsActive,
    bool? IsDefault,
    long? CompanyId
    ) : ICommand<CostCenter>;