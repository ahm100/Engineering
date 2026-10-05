using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetsTotalProjectOperationReporting;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsTotalProjectOperationReporting;

public class GetsTotalProjectOperationReportingQueryHandler : IQueryHandler<GetsTotalProjectOperationReportingQuery, DataResult<List<GetsTotalProjectOperationReportingResponseModel>>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetsTotalProjectOperationReportingQueryHandler> _logger;

    public GetsTotalProjectOperationReportingQueryHandler(ILogger<GetsTotalProjectOperationReportingQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsTotalProjectOperationReportingResponseModel>>?>> Handle(GetsTotalProjectOperationReportingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsTotalProjectOperationReporting(
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
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsTotalProjectOperationReportingResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsTotalProjectOperationReportingResponseModel>>>(ProjectOperationErrors.DataNotFoundWithFilters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsTotalProjectOperationReportingResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}