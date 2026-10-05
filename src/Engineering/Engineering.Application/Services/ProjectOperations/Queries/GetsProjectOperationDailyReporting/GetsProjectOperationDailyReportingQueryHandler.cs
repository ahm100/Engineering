using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReporting;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationDailyReporting;

public class GetsProjectOperationDailyReportingQueryHandler : IQueryHandler<GetsProjectOperationDailyReportingQuery, DataResult<List<GetsProjectOperationDailyReportingModel>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsProjectOperationDailyReportingQueryHandler> _logger;

    public GetsProjectOperationDailyReportingQueryHandler(
        ILogger<GetsProjectOperationDailyReportingQueryHandler> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectOperationDailyReportingModel>>?>> Handle(
        GetsProjectOperationDailyReportingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDailyReporting(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectOperationDailyReportingModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectOperationDailyReportingModel>>>(ProjectOperationErrors.DataNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsProjectOperationDailyReportingModel>>>(SharedErrors.UnknownError);
        }
    }
}