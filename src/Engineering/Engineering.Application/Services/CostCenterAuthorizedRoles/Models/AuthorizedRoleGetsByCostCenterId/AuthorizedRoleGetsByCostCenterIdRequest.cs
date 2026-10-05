namespace Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleGetsByCostCenterId;

public record AuthorizedRoleGetsByCostCenterIdRequest(
    long CostCenterId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
