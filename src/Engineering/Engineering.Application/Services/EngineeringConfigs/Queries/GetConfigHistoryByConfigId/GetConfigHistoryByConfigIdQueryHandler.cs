using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Queries.GetConfigHistoryByConfigId;

public class GetConfigHistoryByConfigIdQueryHandler : IQueryHandler<GetConfigHistoryByConfigIdQuery, GetConfigHistoryByConfigIdResponse?>
{
    private readonly IUserInfoService _userInfoService;
    private readonly IMediator _mediator;
    private readonly IEngineeringConfigHistoryRepository _repository;
    private readonly ILogger<GetConfigHistoryByConfigIdQueryHandler> _logger;

    public GetConfigHistoryByConfigIdQueryHandler(ILogger<GetConfigHistoryByConfigIdQueryHandler> logger,
        IEngineeringConfigHistoryRepository repository,
        IMediator mediator,
        IUserInfoService userInfoService)
    {
        _logger = logger;
        _repository = repository;
        _userInfoService = userInfoService;
        _mediator = mediator;
    }

    public async Task<Result<GetConfigHistoryByConfigIdResponse?>> Handle(GetConfigHistoryByConfigIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetConfigHistoryByConfigId(request.ConfigId, request.PageIndex, request.PageSize, ct);

            if (result.Count < 1 || result.Data == null)
                return Result.Failure<GetConfigHistoryByConfigIdResponse?>(EngineeringConfigErrors.ActiveConfigNotFound);

            return new GetConfigHistoryByConfigIdResponse(result.Data,
                result.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetConfigHistoryByConfigIdResponse>(SharedErrors.UnknownError);
        }
    }
}