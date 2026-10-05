using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;

namespace Engineering.Application.Services.SessionRecords.Queries.GetUserSessionRecordAction;

public class GetUserSessionRecordActionQueryHandler : IQueryHandler<GetUserSessionRecordActionQuery, GetUserSessionRecordActionResponse?>
{
    private readonly ILogger<GetUserSessionRecordActionQueryHandler> _logger;
    private readonly ISessionRecordActionRepository _actionRepository;

    public GetUserSessionRecordActionQueryHandler(
        ILogger<GetUserSessionRecordActionQueryHandler> logger,
        ISessionRecordActionRepository actionRepository)
    {
        _logger = logger;
        _actionRepository = actionRepository;
    }

    public async Task<Result<GetUserSessionRecordActionResponse?>> Handle(
        GetUserSessionRecordActionQuery request, CT ct)
    {
        try
        {
            var (data, rowCount) = await _actionRepository.GetUserSessionRecordAction(
                request.UserId,
                request.Description,
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetUserSessionRecordActionResponse(data, rowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error GetUserSessionRecordAction Query Handler");
            return Result.Failure<GetUserSessionRecordActionResponse>(SharedErrors.UnknownError)!;
        }
    }
}