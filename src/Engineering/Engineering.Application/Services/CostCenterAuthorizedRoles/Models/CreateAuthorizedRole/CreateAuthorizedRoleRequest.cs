namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRole;

public record CreateAuthorizedRoleRequest(
    long AuthorizedRoleId,
    long CostCenterId
     ) : IHttpRequest;
