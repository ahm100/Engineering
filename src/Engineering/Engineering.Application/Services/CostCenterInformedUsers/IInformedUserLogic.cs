using Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUsers;
using Engineering.Application.Services.CostCenterInformedUsers.Models.DeleteInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserGetsByCostCenterId;

namespace Engineering.Application.Services.CostCenterInformedUsers;

public interface IInformedUserLogic
{
    Task<Result<CreateInformedUserResponse?>> CreateInformedUser(
        CreateInformedUserRequest request, CT ct);

    Task<Result<CreateInformedUsersResponse?>> CreateInformedUsers(
        CreateInformedUsersRequest request, CT ct);

    Task<Result<InformedUserGetsByCostCenterIdResponse?>> InformedUserGetsByCostCenterId(
        InformedUserGetsByCostCenterIdRequest request, CT ct);

    Task<Result<DeleteInformedUserResponse?>> DeleteInformedUser(
        DeleteInformedUserRequest request, CT ct);

}