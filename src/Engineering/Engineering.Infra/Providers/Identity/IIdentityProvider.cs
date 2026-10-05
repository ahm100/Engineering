using Engineering.Application.IdentityServices.Roles.Models.GetRoleById;
using Engineering.Application.IdentityServices.Roles.Models.GetsRoleById;
using Engineering.Application.IdentityServices.Users.Models.GetsUserById;
using Engineering.Application.IdentityServices.Users.Models.GetUserById;
using Engineering.Application.IdentityServices.Users.Models.GetUsersByRoleIds;
using Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

namespace Engineering.Infra.Providers.Identity;

public interface IIdentityProvider
{
    // دریافت اطلاعات نقش 
    [Get("/role/v1/getById")]
    Task<GetRoleByIdResponse> GetRoleById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات نقش ها 
    [Post("/role/v1/getRolesByIds")]
    Task<GetsRoleByIdResponse> GetsRoleByIds(
        [Body] GetsRoleByIdRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/user/v1/GetUsersByRoleIds")]
    Task<GetUsersByRoleIdsResponse> GetUsersByRoleIds(
        [Body] GetUsersByRoleIdsRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Get("/user/v1/getUserById")]
    Task<GetUserByIdResponse> GetUserById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات کاربران ها 
    [Post("/user/v1/getUsersByIds")]
    Task<GetsUserByIdResponse> GetsUserByIds(
        [Body] GetsUserByIdRequest request, CT ct);

    // دریافت اطلاعات کاربران ها 
    [Post("/UserRole/v1/GetUsersByActionId")]
    Task<GetUsersByActionIdResponse> GetUsersByActionId(
        [Body] GetUsersByActionIdRequest request, CT ct);

}