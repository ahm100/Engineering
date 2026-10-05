using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetsInspectionReport;

public class GetsInspectionReportQueryHandler : IQueryHandler<GetsInspectionReportQuery, DataResult<List<GetsInspectionReportModel>>>
{
    private readonly ILogger<GetsInspectionReportQueryHandler> _logger;
    private readonly IProjectOperationDetailInspectionRepository _repository;

    public GetsInspectionReportQueryHandler(ILogger<GetsInspectionReportQueryHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsInspectionReportModel>>?>> Handle(GetsInspectionReportQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetsInspectioReport(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.OperationInfoIds,
                request.OperationLocationIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.CreatorIds,
                request.FromDate,
                request.ToDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return entities.Data.Any()
                    ? new DataResult<List<GetsInspectionReportModel>>
                    {
                        Data = entities.Data,
                        RowCount = entities.RowCount
                    } : Result.Failure<DataResult<List<GetsInspectionReportModel>>>(ProjectOperationDetailInspectionErrors.FilteredInspectionNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetsInspectionReportModel>>>(SharedErrors.UnknownError);
        }
    }
}
