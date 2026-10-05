using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByIds;

public class GetsCostOverByIdsQueryHandler : IQueryHandler<GetsCostOverByIdsQuery, List<CostOver>>
{
    private readonly ICostOverRepository _repository;
    private readonly ILogger<GetsCostOverByIdsQuery> _logger;

    public GetsCostOverByIdsQueryHandler(
        ILogger<GetsCostOverByIdsQuery> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CostOver>?>> Handle(GetsCostOverByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostOverByIds(request.Items, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CostOver>>(SharedErrors.UnknownError);
        }
    }
}