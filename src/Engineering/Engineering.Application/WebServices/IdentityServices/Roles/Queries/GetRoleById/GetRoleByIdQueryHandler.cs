using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.IdentityServices.Roles.Models.GetRoleById;
using RoleModel = Engineering.Application.IdentityServices.Roles.Models.Role;

namespace Engineering.Application.IdentityServices.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleModel?>
{
    private readonly ILogger<GetRoleByIdQueryHandler> _logger;
    private readonly IIdentityService _identityService;

    public GetRoleByIdQueryHandler(ILogger<GetRoleByIdQueryHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Result<RoleModel?>> Handle(GetRoleByIdQuery request, CT ct)
    {
        try
        {
            var result = await _identityService.GetRoleById(request.Adapt<GetRoleByIdRequest>(), ct);
            return result.Value ?? Result.Failure<RoleModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RoleModel?>(SharedErrors.UnknownError);
        }
    }
}