using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardsByIds;

public class GetRequestRewardsByIdsQueryHandler : IQueryHandler<GetRequestRewardsByIdsQuery, DataResult<List<RequestReward>>>
{
    private readonly ILogger<GetRequestRewardsByIdsQueryHandler> _logger;
    private readonly IRequestRewardRepository _repository;

    public GetRequestRewardsByIdsQueryHandler(ILogger<GetRequestRewardsByIdsQueryHandler> logger, IRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestReward>>?>> Handle(GetRequestRewardsByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetRequestRewardsByIds(request.Ids, ct);

            return result.Data.Any()
                ? new DataResult<List<RequestReward>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<RequestReward>>>(RequestRewardErrors.RequestRewardWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestReward>>>(SharedErrors.UnknownError);
        }
    }
}
