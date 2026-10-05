using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsAuthorizedCostCenter;

public record GetsAuthorizedCostCenterResponse(
    List<GetsAuthorizedCostCenterModel> Data,
    int RowCount);

public record GetsAuthorizedCostCenterModel(
    long Id,
    string CostCenterCode,
    string CostCenterName,
    List<long>? Ids,
    CostCenterCityModel? City,
    string Address,
    long? CompanyId,
    string? CompanyNameFa
    );
