using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;

public record GetsActiveAuthorizedCostCenterModel(
    long Id,
    string CostCenterCode,
    string CostCenterName,
    List<long>? Ids,
    CostCenterCityModel? City,
    string Address,
    long? CompanyId,
    string? CompanyNameFa
    );
