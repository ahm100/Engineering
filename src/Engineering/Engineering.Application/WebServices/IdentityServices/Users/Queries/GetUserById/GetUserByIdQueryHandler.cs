using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.IdentityServices.Users.Models.GetUserById;
using UserModel = Engineering.Application.IdentityServices.Users.Models.User;

namespace Engineering.Application.IdentityServices.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserModel?>
{
    private readonly ILogger<GetUserByIdQueryHandler> _logger;
    private readonly IIdentityService _identityService;

    public GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Result<UserModel?>> Handle(GetUserByIdQuery request, CT ct)
    {
        try
        {
            var result = await _identityService.GetUserById(request.Adapt<GetUserByIdRequest>(), ct);

            return result.Value ?? Result.Failure<UserModel?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UserModel?>(SharedErrors.UnknownError);
        }
    }
}