using Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;
using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;
using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenters.Commands.CreateCostCenter;

public record CreateCostCenterCommand(
    CostCenterType CostCenterType,
    string CostCenterCode,
    string CostCenterName,
    string? CostCenterEnName,
    List<CostCenterWarehouseRequest?>? CostCenterWarehouses,
    List<long?>? InformedUsers,
    List<long?>? AuthorizedRoles,
    List<long?>? AuthorizedUsers,
    List<long>? ProjectIds,
    int? NoOperationDays,
    long CityId,
    string Address,
    string? PostalCode,
    decimal? Latitude,
    decimal? Longitude,
    string? Description,
    string? DescriptionEn,
    bool? WeatherState,
    bool IsActive,
    bool? IsDefault,
    long? CompanyId,
    ProjectCostCenterRequest? PCR
    ) : ICommand<CostCenter>;