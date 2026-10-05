namespace Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUsers;

public record CreateAuthorizedUsersRequest(
    long CostCenterId,
    List<long>? UserIds,
    List<long>? RoleIds
     ) : IHttpRequest;
