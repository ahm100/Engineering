using Engineering.Application.Services.CostCenters.Models.CostCenterModels;

namespace Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedRoleId;

public record GetsByAuthorizedRoleIdResponse(
    List<CostCentersByAuthorizedRoleIdModel> Data,
    int RowCount);
