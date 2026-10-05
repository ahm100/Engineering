namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUser;

public record CreateAuthorizedUserRequest(
    long CostCenterId,
    long AuthorizedUserId
     ) : IHttpRequest;
