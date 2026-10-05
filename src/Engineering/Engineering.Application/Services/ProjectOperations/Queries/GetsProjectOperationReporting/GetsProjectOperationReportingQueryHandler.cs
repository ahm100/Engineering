using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationReporting;

public class GetsProjectOperationReportingQueryHandler : IQueryHandler<GetsProjectOperationReportingQuery, DataResult<List<GetsProjectOperationReportingModel>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsProjectOperationReportingQueryHandler> _logger;

    public GetsProjectOperationReportingQueryHandler(ILogger<GetsProjectOperationReportingQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectOperationReportingModel>>?>> Handle(GetsProjectOperationReportingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationReporting(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.Statuses,
                request.StartDate,
                request.EndDate,
                request.Description,
                request.DailyDescription,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectOperationReportingModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectOperationReportingModel>>>(ProjectOperationErrors.DataNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsProjectOperationReportingModel>>>(SharedErrors.UnknownError);
        }
    }
}