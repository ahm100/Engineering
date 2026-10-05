using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.IdentityServices.Roles.Models.GetRoleById;
using Engineering.Application.IdentityServices.Roles.Models.GetsRoleById;
using Engineering.Application.IdentityServices.Users.Models.GetsUserById;
using Engineering.Application.IdentityServices.Users.Models.GetUserById;
using Engineering.Application.IdentityServices.Users.Models.GetUsersByRoleIds;
using Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

namespace Engineering.Infra.Providers.Identity;

public class IdentityService : IIdentityService
{
    private readonly IIdentityProvider _identityProvider;

    public IdentityService(IIdentityProvider identityProvider)
    {
        _identityProvider = identityProvider;
    }

    public async Task<GetRoleByIdResponse> GetRoleById(GetRoleByIdRequest request, CT ct)
    {
        return await _identityProvider.GetRoleById(request.Id, ct);
    }

    public async Task<GetsRoleByIdResponse> GetsRoleById(GetsRoleByIdRequest request, CT ct)
    {
        return await _identityProvider.GetsRoleByIds(request, ct);
    }

    public async Task<GetUsersByRoleIdsResponse> GetUsersByRoleIds(GetUsersByRoleIdsRequest request, CT ct)
    {
        return await _identityProvider.GetUsersByRoleIds(request, ct);
    }

    public async Task<GetUserByIdResponse> GetUserById(GetUserByIdRequest request, CT ct)
    {
        return await _identityProvider.GetUserById(request.Id, ct);
    }

    public async Task<GetsUserByIdResponse> GetsUserById(GetsUserByIdRequest request, CT ct)
    {
        return await _identityProvider.GetsUserByIds(request, ct);
    }

    public async Task<GetUsersByActionIdResponse> GetUsersByActionId(
        GetUsersByActionIdRequest request, CT ct)
    {
        return await _identityProvider.GetUsersByActionId(request, ct);
    }

}