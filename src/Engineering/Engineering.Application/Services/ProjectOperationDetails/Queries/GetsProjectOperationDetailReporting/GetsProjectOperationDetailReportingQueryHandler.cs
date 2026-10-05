using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailReporting;

public class GetsProjectOperationDetailReportingQueryHandler : IQueryHandler<GetsProjectOperationDetailReportingQuery, DataResult<List<GetsProjectOperationDetailReportingModel>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsProjectOperationDetailReportingQueryHandler> _logger;

    public GetsProjectOperationDetailReportingQueryHandler(ILogger<GetsProjectOperationDetailReportingQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectOperationDetailReportingModel>>?>> Handle(GetsProjectOperationDetailReportingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailReporting(
                request.Ids,
                request.StartDate,
                request.EndDate,
                request.CreateFrom,
                request.CreateTo,
                request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ProjectOperationIds,
                request.ContractorIds,
                request.Statuses,
                request.LocationFilterData,
                request.DescriptionFilterData,
                request.DailyDescription,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectOperationDetailReportingModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectOperationDetailReportingModel>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsProjectOperationDetailReportingModel>>>(SharedErrors.UnknownError);
        }
    }
}
