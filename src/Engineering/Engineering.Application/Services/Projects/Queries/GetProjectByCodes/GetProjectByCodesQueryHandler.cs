using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByCodes;

public class GetProjectByCodesQueryHandler : IQueryHandler<GetProjectByCodesQuery, List<Project>?>
{
    private readonly ILogger<GetProjectByCodesQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectByCodesQueryHandler(
        ILogger<GetProjectByCodesQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Project>?>> Handle(GetProjectByCodesQuery request, CT ct)
    {
        try
        {
            var ProjectsResponse = await _repository.GetByCodes(
                request.ProjectCodes,
                request.CompanyId,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                request.Statuses,
                ct);

            return ProjectsResponse ?? Result.Failure<List<Project>>(ProjectErrors.ProjectWithCodesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Project>>(SharedErrors.UnknownError);
        }
    }
}