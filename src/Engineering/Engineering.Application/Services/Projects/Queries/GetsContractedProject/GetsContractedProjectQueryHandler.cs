using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsContractedProject;

public class GetsContractedProjectQueryHandler : IQueryHandler<GetsContractedProjectQuery, DataResult<List<Project>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetsContractedProjectQueryHandler> _logger;

    public GetsContractedProjectQueryHandler(ILogger<GetsContractedProjectQueryHandler> logger, IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Project>>?>> Handle(GetsContractedProjectQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractedProject(
                request.CostCenterIds,
                request.Status,
                request.FilterData,
                request.EmployerId,
                request.ProjectTypeId,
                request.CategoryId,
                request.IsActive,
                request.OrderBy,
                request.Statuses,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                request.CompanyId,
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