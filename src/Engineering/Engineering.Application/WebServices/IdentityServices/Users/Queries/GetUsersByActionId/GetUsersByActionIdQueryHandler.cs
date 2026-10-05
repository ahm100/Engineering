using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.IdentityServices.Users.Models.GetUsersByActionId;

namespace Engineering.Application.WebServices.IdentityServices.Users.Queries.GetUsersByActionId;

public class GetUsersByActionIdQueryHandler : IQueryHandler<GetUsersByActionIdQuery, GetUsersByActionIdResponse?>
{
    private readonly ILogger<GetUsersByActionIdQueryHandler> _logger;
    private readonly IIdentityService _identityService;

    public GetUsersByActionIdQueryHandler(ILogger<GetUsersByActionIdQueryHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Result<GetUsersByActionIdResponse?>> Handle(GetUsersByActionIdQuery request, CT ct)
    {
        try
        {
            var result = await _identityService.GetUsersByActionId(request.Adapt<GetUsersByActionIdRequest>(), ct);

            return result ?? Result.Failure<GetUsersByActionIdResponse?>(SharedErrors.ProviderError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetUsersByActionIdResponse?>(SharedErrors.UnknownError);
        }
    }
}