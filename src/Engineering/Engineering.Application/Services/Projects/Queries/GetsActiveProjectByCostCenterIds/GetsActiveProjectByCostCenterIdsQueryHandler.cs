using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsActiveProjectByCostCenterIds;

public class GetsActiveProjectByCostCenterIdsQueryHandler : IQueryHandler<GetsActiveProjectByCostCenterIdsQuery, DataResult<List<Project>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetsActiveProjectByCostCenterIdsQuery> _logger;

    public GetsActiveProjectByCostCenterIdsQueryHandler(
        ILogger<GetsActiveProjectByCostCenterIdsQuery> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Project>>?>> Handle(GetsActiveProjectByCostCenterIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveProjectByCostCenterIds(
                request.FilterData,
                request.EmployerId,
                request.CostCenterIds,
                request.ProjectTypeId,
                request.CategoryId,
                request.ProjectManagerId,
                request.PlanningAssistantId,
                request.SupervisorEngineerId,
                request.AdvisorId,
                request.ImplementationAssistantId,
                request.TechnicalAssistantId,
                request.CompanyId,
                request.Contractual,
                request.Statuses,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<Project>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Project>>>(ProjectErrors.FilteredProjectNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Project>>>(SharedErrors.UnknownError);
        }
    }
}