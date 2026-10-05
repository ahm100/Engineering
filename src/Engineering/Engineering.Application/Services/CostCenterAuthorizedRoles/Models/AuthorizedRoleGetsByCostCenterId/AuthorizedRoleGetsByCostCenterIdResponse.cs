using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleModels;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleGetsByCostCenterId;

public record AuthorizedRoleGetsByCostCenterIdResponse(
    List<AuthorizedRoleGetsByCostCenterIdModel> Data,
    int RowCount);
