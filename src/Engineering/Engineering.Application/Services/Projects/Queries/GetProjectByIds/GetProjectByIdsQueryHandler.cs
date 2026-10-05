using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByIds;

public class GetProjectByIdsQueryHandler : IQueryHandler<GetProjectByIdsQuery, List<Project>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetProjectByIdsQueryHandler> _logger;

    public GetProjectByIdsQueryHandler(
        ILogger<GetProjectByIdsQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Project>?>> Handle(GetProjectByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectByIds(
                request.Ids,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                ct);
            if (result == null || result.Count != request.Ids.Count)
                return Result.Failure<List<Project>>(ProjectErrors.FilteredProjectNotFound);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Project>>(SharedErrors.UnknownError);
        }
    }
}
