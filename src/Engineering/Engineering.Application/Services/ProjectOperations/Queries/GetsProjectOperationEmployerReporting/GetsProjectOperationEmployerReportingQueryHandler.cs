using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsProjectOperationEmployerReporting;

public class GetsProjectOperationEmployerReportingQueryHandler : IQueryHandler<GetsProjectOperationEmployerReportingQuery, DataResult<List<GetsProjectOperationEmployerReportingModel>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsProjectOperationEmployerReportingQueryHandler> _logger;

    public GetsProjectOperationEmployerReportingQueryHandler(ILogger<GetsProjectOperationEmployerReportingQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectOperationEmployerReportingModel>>?>> Handle(GetsProjectOperationEmployerReportingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationEmployerReporting(
                request.Ids,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ContractorIds,
                request.EmployerIds,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectOperationEmployerReportingModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectOperationEmployerReportingModel>>>(ProjectOperationErrors.DataNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            _logger.LogInformation("Mediator Error");
            return Result.Failure<DataResult<List<GetsProjectOperationEmployerReportingModel>>>(SharedErrors.UnknownError);
        }
    }
}