using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.Projects.Models.ProjectModels;

namespace Engineering.Application.Services.Projects.Queries.GetProjects;

public class GetProjectsQueryHandler : IQueryHandler<GetProjectsQuery, DataResult<List<GetProjectsModel>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetProjectsQueryHandler> _logger;

    public GetProjectsQueryHandler(
        ILogger<GetProjectsQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetProjectsModel>>?>> Handle(GetProjectsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjects(
                request.Ids,
                request.FilterData,
                request.Status,
                request.CategoryIds,
                request.CostCenterId,
                request.AdvisorId,
                request.ProjectManagerId,
                request.PlanningAssistantId,
                request.ThirdPartyId,
                request.SupervisorEngineerId,
                request.ImplementationAssistantId,
                request.TechnicalAssistantId,
                request.EmployerId,
                request.ProjectTypeId,
                request.IsActive,
                request.CompanyId,
                request.OrderBy,
                request.Statuses,
                request.CheckThirdParty,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetProjectsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetProjectsModel>>>(ProjectErrors.FilteredProjectNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetProjectsModel>>>(SharedErrors.UnknownError);
        }
    }
}