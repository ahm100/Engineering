using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.IdentityServices.Users.Models.GetUsersByRoleIds;
using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Queries.GetUsersByRoleIds;

public class GetUsersByRoleIdsQueryHandler : IQueryHandler<GetUsersByRoleIdsQuery, DataResult<List<UserModel>>>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<GetUsersByRoleIdsQueryHandler> _logger;

    public GetUsersByRoleIdsQueryHandler(ILogger<GetUsersByRoleIdsQueryHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Result<DataResult<List<UserModel>>?>> Handle(GetUsersByRoleIdsQuery request, CT ct)
    {
        try
        {
            var result = await _identityService.GetUsersByRoleIds(request.Adapt<GetUsersByRoleIdsRequest>(), ct);

            return (result.Value?.Data?.Any()) ?? false ?
                new DataResult<List<UserModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<UserModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<UserModel>>>(SharedErrors.UnknownError);
        }
    }
}