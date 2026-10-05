using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetsActiveCostOvers;

public class GetsActiveCostOversQueryHandler : IQueryHandler<GetsActiveCostOversQuery, DataResult<List<GetsActiveCostOversModel>>>
{
    private readonly ICostOverRepository _repository;
    private readonly ILogger<GetsActiveCostOversQuery> _logger;

    public GetsActiveCostOversQueryHandler(
        ILogger<GetsActiveCostOversQuery> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveCostOversModel>>?>> Handle(GetsActiveCostOversQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveCostOvers(
                request.FilterData,
                request.Code,
                request.Name,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsActiveCostOversModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsActiveCostOversModel>>>(CostOverErrors.CostOverChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveCostOversModel>>>(SharedErrors.UnknownError);
        }
    }
}