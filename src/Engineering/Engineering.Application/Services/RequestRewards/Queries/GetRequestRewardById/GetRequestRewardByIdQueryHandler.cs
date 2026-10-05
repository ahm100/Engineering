using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardById;

public class GetRequestRewardByIdQueryHandler : IQueryHandler<GetRequestRewardByIdQuery, RequestReward>
{
    private readonly ILogger<GetRequestRewardByIdQueryHandler> _logger;
    private readonly IRequestRewardRepository _repository;

    public GetRequestRewardByIdQueryHandler(ILogger<GetRequestRewardByIdQueryHandler> logger, IRequestRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestReward?>> Handle(GetRequestRewardByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);
            return result ?? Result.Failure<RequestReward>(RequestRewardErrors.RequestRewardWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestReward>(SharedErrors.UnknownError);
        }
    }
}
