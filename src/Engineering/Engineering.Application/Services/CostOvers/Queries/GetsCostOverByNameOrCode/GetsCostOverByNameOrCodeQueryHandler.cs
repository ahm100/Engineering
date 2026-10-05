using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverByNameOrCode;

namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNameOrCode;

public class GetsCostOverByNameOrCodeQueryHandler : IQueryHandler<GetsCostOverByNameOrCodeQuery, DataResult<List<GetsCostOverByNameOrCodeModel>>>
{
    private readonly ICostOverRepository _repository;
    private readonly ILogger<GetsCostOverByNameOrCodeQueryHandler> _logger;

    public GetsCostOverByNameOrCodeQueryHandler(
        ILogger<GetsCostOverByNameOrCodeQueryHandler> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsCostOverByNameOrCodeModel>>?>> Handle(GetsCostOverByNameOrCodeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCostOverByNameOrCode(
                request.FilterData,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsCostOverByNameOrCodeModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsCostOverByNameOrCodeModel>>>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsCostOverByNameOrCodeModel>>>(SharedErrors.UnknownError);
        }
    }
}