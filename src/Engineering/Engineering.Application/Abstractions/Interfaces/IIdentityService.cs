using Engineering.Application.IdentityServices.Roles.Models.GetRoleById;
using Engineering.Application.IdentityServices.Roles.Models.GetsRoleById;
using Engineering.Application.IdentityServices.Users.Models.GetsUserById;
using Engineering.Application.IdentityServices.Users.Models.GetUserById;
using Engineering.Application.IdentityServices.Users.Models.GetUsersByRoleIds;
using Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

namespace Engineering.Application.Abstractions.Interfaces;

public interface IIdentityService
{
    Task<GetRoleByIdResponse> GetRoleById(GetRoleByIdRequest request, CT ct);
    Task<GetsRoleByIdResponse> GetsRoleById(GetsRoleByIdRequest request, CT ct);

    Task<GetUserByIdResponse> GetUserById(GetUserByIdRequest request, CT ct);
    Task<GetsUserByIdResponse> GetsUserById(GetsUserByIdRequest request, CT ct);
    Task<GetUsersByRoleIdsResponse> GetUsersByRoleIds(GetUsersByRoleIdsRequest request, CT ct);
    Task<GetUsersByActionIdResponse> GetUsersByActionId(
        GetUsersByActionIdRequest request, CT ct);

}