namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.DeleteAuthorizedUser;

public record DeleteAuthorizedUserRequest(
    long UserId,
    long CostCenterId
     ) : IHttpRequest;
