using Engineering.Application.Services.CostCenters.Models.CostCenterModels;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels.CostCenterWarehouseModels;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels.Responses;

namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterById;

public record GetCostCenterByIdResponse(
    long Id,
    long CostCenterTypeId,
    string CostCenterTypeTitle,
    string CostCenterCode,
    string CostCenterName,
    string? CostCenterEnName,
    int? NoOperationDays,
    long? CityId,
    string? CityName,
    string? CityCode,
    string Address,
    string? PostalCode,
    decimal Latitude,
    decimal Longitude,
    string? Description,
    string? DescriptionEn,
    bool? WeatherState,
    bool IsActive,
    bool IsDefault,
    List<CostCenterWarehouseResponse>? CostCenterWarehouses,
    List<InformedUserDataModel>? InformedUsers,
    List<AuthorizedRoleDataModel>? AuthorizedRoles,
    List<AuthorizedUserDataModel>? AuthorizedUsers,
    List<CostCenterProjectModel>? Projects,
    CostCenterTypeModel CostCenterType,
    CostCenterCityModel City,
    long? CompanyId,
    string? CompanyNameFa,
    Guid PreferentialReferenceCode
    );

public record CostCenterProjectModel(
    long Id,
    string? ProjectName,
    string? ProjectCode
    );