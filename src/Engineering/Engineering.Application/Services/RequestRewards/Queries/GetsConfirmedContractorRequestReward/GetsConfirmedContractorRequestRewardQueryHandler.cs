using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetsConfirmedContractorRequestReward;

public class GetsConfirmedContractorRequestRewardQueryHandler : IQueryHandler<GetsConfirmedContractorRequestRewardQuery, DataResult<List<RequestReward>>>
{
    private readonly ILogger<GetsConfirmedContractorRequestRewardQueryHandler> _logger;
    private readonly IRequestRewardRepository _repository;

    public GetsConfirmedContractorRequestRewardQueryHandler(
        ILogger<GetsConfirmedContractorRequestRewardQueryHandler> logger,
        IRequestRewardRepository requestRewardRepository)
    {
        _repository = requestRewardRepository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<RequestReward>>?>> Handle(GetsConfirmedContractorRequestRewardQuery request, CT ct)
    {
        try
        {
            var requestRewards = await _repository.GetsConfirmedContractorRequestReward(
                request.ProjectId,
                request.ContractorId,
                request.FromDate,
                request.ToDate,
                request.PageIndex,
                request.PageSize, ct);

            return requestRewards.Data.Any()
                ? new DataResult<List<RequestReward>>
                {
                    Data = requestRewards.Data,
                    RowCount = requestRewards.RowCount
                } : Result.Failure<DataResult<List<RequestReward>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestReward>>>(SharedErrors.UnknownError);
        }
    }
}
