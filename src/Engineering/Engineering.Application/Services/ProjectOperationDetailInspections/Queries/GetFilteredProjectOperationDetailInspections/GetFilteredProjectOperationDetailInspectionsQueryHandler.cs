using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetFilteredProjectOperationDetailInspections;

public class GetFilteredProjectOperationDetailInspectionsQueryHandler : IQueryHandler<GetFilteredProjectOperationDetailInspectionsQuery, DataResult<List<ProjectOperationDetailInspection>>>
{
    private readonly ILogger<GetFilteredProjectOperationDetailInspectionsQueryHandler> _logger;
    private readonly IProjectOperationDetailInspectionRepository _repository;

    public GetFilteredProjectOperationDetailInspectionsQueryHandler(ILogger<GetFilteredProjectOperationDetailInspectionsQueryHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailInspection>>?>> Handle(GetFilteredProjectOperationDetailInspectionsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetProjectOperationDetailInspections(request.CostCenterId, request.ProjectId, request.OperationInfoId,
                request.OperationLocationId, request.ProjectOperationId, request.ProjectOperationdetailId, request.FromDate, request.ToDate,
                request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                    ? new DataResult<List<ProjectOperationDetailInspection>>
                    {
                        Data = entities.Data,
                        RowCount = entities.RowCount
                    } : Result.Failure<DataResult<List<ProjectOperationDetailInspection>>>(ProjectOperationDetailInspectionErrors.FilteredInspectionNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailInspection>>>(SharedErrors.UnknownError);
        }
    }
}
