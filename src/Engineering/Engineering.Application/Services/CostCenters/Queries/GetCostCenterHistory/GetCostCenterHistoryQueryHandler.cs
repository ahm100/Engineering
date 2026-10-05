using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;

namespace Engineering.Application.Services.CostCenters.Queries.GetCostCenterHistory;

public class GetCostCenterHistoriesQueryHandler : IQueryHandler<GetCostCenterHistoriesQuery, GetCostCenterHistoriesResponse?>
{
    private readonly ILogger<GetCostCenterHistoriesQueryHandler> _logger;
    private readonly ICostCenterHistoryRepository _repository;

    public GetCostCenterHistoriesQueryHandler(ILogger<GetCostCenterHistoriesQueryHandler> logger, ICostCenterHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostCenterHistoriesResponse?>> Handle(GetCostCenterHistoriesQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetCostCenterHistories(request.Id, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);
            if (response is null)
                return Result.Failure<GetCostCenterHistoriesResponse>(CostCenterErrors.CostCenterWithIdNotFound)!;
            var result = new GetCostCenterHistoriesResponse(response, response.Count);
            return result ?? Result.Failure<GetCostCenterHistoriesResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostCenterHistoriesResponse>(SharedErrors.UnknownError)!;
        }
    }
}
