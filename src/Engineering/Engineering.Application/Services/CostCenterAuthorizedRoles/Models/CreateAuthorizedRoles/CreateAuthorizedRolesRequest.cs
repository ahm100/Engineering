namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRoles;

public record CreateAuthorizedRolesRequest(
    long CostCenterId,
    List<long>? RoleIds
     ) : IHttpRequest;
