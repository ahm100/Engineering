using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetsRequestRewardDocument;

public class GetsRequestRewardDocumentByIdsQueryHandler : IQueryHandler<GetsRequestRewardDocumentByIdsQuery, DataResult<List<RequestRewardDocument>>>
{
    private readonly ILogger<GetsRequestRewardDocumentByIdsQueryHandler> _logger;
    private readonly IRequestRewardDocumentRepository _repository;

    public GetsRequestRewardDocumentByIdsQueryHandler(ILogger<GetsRequestRewardDocumentByIdsQueryHandler> logger, IRequestRewardDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestRewardDocument>>?>> Handle(GetsRequestRewardDocumentByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestRewardDocumentsByIds(request.Ids, ct);
            return result.Data.Any() ?
                new DataResult<List<RequestRewardDocument>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<RequestRewardDocument>>>(RequestRewardDocumentErrors.RequestRewardDocumentNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestRewardDocument>>>(SharedErrors.UnknownError);
        }
    }
}