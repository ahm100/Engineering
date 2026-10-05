using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDetailedDailyOperationTotals;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDetailedDailyOperationTotals;

public class GetDetailedDailyOperationTotalsQueryHandler : IQueryHandler<GetDetailedDailyOperationTotalsQuery, List<GetDetailedDailyProjectOperationTotalsModel>>
{
    private readonly ILogger<GetDetailedDailyOperationTotalsQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetDetailedDailyOperationTotalsQueryHandler(ILogger<GetDetailedDailyOperationTotalsQueryHandler> logger,
                                                       IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetDetailedDailyProjectOperationTotalsModel>?>> Handle(GetDetailedDailyOperationTotalsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDetailedDailyProjectOperationTotals(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.ServiceInfoIds,
                request.CreatorIds,
                request.StartDate,
                request.EndDate,
                request.FromDate,
                request.ToDate,
                request.Status,
                request.ProjectOperationDetailStatus,
                request.FilterData,
                ct);

            return result ?? Result.Failure<List<GetDetailedDailyProjectOperationTotalsModel>>(DailyProjectOperationErrors.DailyProjectOperationFilteredNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetDetailedDailyProjectOperationTotalsModel>>(SharedErrors.UnknownError);
        }
    }
}
