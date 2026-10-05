using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleGetsByCostCenterId;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRole;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.DeleteAuthorizedRole;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles;

public interface IAuthorizedRoleLogic
{
    Task<Result<CreateAuthorizedRoleResponse?>> CreateAuthorizedRole(
        CreateAuthorizedRoleRequest request, CT ct);

    Task<Result<CreateAuthorizedRolesResponse?>> CreateAuthorizedRoles(
        CreateAuthorizedRolesRequest request, CT ct);

    Task<Result<AuthorizedRoleGetsByCostCenterIdResponse?>> AuthorizedRoleGetsByCostCenterId(
        AuthorizedRoleGetsByCostCenterIdRequest request, CT ct);

    Task<Result<DeleteAuthorizedRoleResponse?>> DeleteAuthorizedRole(
        DeleteAuthorizedRoleRequest request, CT ct);

}