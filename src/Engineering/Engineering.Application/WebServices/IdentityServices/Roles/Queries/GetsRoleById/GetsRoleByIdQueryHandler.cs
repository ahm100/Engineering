using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.IdentityServices.Roles.Models.GetsRoleById;
using RoleModel = Engineering.Application.IdentityServices.Roles.Models.Role;

namespace Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;

public class GetsRoleByIdQueryHandler : IQueryHandler<GetsRoleByIdQuery, DataResult<List<RoleModel>>>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<GetsRoleByIdQueryHandler> _logger;

    public GetsRoleByIdQueryHandler(ILogger<GetsRoleByIdQueryHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Result<DataResult<List<RoleModel>>?>> Handle(GetsRoleByIdQuery request, CT ct)
    {
        try
        {
            var result = await _identityService.GetsRoleById(request.Adapt<GetsRoleByIdRequest>(), ct);

            return (result.Value?.Data?.Any()) ?? false ?
                new DataResult<List<RoleModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<RoleModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<RoleModel>>>(SharedErrors.UnknownError);
        }
    }
}