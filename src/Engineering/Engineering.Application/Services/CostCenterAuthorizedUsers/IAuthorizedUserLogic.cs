using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserGetsByCostCenterId;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUser;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUsers;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.DeleteAuthorizedUser;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers;

public interface IAuthorizedUserLogic
{
    Task<Result<CreateAuthorizedUserResponse?>> CreateAuthorizedUser(
        CreateAuthorizedUserRequest request, CT ct);

    Task<Result<CreateAuthorizedUsersResponse?>> CreateAuthorizedUsers(
        CreateAuthorizedUsersRequest request, CT ct);

    Task<Result<AuthorizedUserGetsByCostCenterIdResponse?>> AuthorizedUserGetsByCostCenterId(
        AuthorizedUserGetsByCostCenterIdRequest request, CT ct);

    Task<Result<DeleteAuthorizedUserResponse?>> DeleteAuthorizedUser(
        DeleteAuthorizedUserRequest request, CT ct);

}