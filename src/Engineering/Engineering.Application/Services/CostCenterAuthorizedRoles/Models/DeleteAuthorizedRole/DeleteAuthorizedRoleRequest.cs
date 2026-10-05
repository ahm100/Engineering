namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.DeleteAuthorizedRole;

public record DeleteAuthorizedRoleRequest(
    long AuthorizedRoleId,
    long CostCenterId
     ) : IHttpRequest;
