using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.IdentityServices.Users.Models.GetsUserById;
using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Queries.GetsUserById;

public class GetsUserByIdQueryHandler : IQueryHandler<GetsUserByIdQuery, DataResult<List<UserModel>>>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<GetsUserByIdQueryHandler> _logger;

    public GetsUserByIdQueryHandler(ILogger<GetsUserByIdQueryHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Result<DataResult<List<UserModel>>?>> Handle(GetsUserByIdQuery request, CT ct)
    {
        try
        {
            var result = await _identityService.GetsUserById(request.Adapt<GetsUserByIdRequest>(), ct);

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