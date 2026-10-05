using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOversByIds;

public class GetsCostOversByIdsQueryHandler : IQueryHandler<GetsCostOversByIdsQuery, DataResult<List<CostOver>>>
{
    private readonly ICostOverRepository _repository;
    private readonly ILogger<GetsCostOversByIdsQueryHandler> _logger;

    public GetsCostOversByIdsQueryHandler(
        ILogger<GetsCostOversByIdsQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<CostOver>>?>> Handle(GetsCostOversByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostOversByIds(request.Ids, ct);
            return result.Data.Any() ?
                new DataResult<List<CostOver>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<CostOver>>>(CostOverErrors.CostOverChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<CostOver>>>(SharedErrors.UnknownError);
        }
    }
}