using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOvers;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOvers;

public class GetCostOversQueryHandler : IQueryHandler<GetsCostOversQuery, DataResult<List<GetsCostOversModel>>>
{
    private readonly ICostOverRepository _repository;
    private readonly ILogger<GetCostOversQueryHandler> _logger;

    public GetCostOversQueryHandler(
        ILogger<GetCostOversQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsCostOversModel>>?>> Handle(GetsCostOversQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostOvers(
                request.Ids,
                request.FilterData,
                request.Code,
                request.Name,
                request.IsActive,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsCostOversModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsCostOversModel>>>(CostOverErrors.CostOverChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsCostOversModel>>>(SharedErrors.UnknownError);
        }
    }
}