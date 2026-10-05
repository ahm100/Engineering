using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByName;

public class GetProjectByNameQueryHandler : IQueryHandler<GetProjectByNameQuery, Project>
{
    private readonly ILogger<GetProjectByNameQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectByNameQueryHandler(
        ILogger<GetProjectByNameQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByNameQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByName(
                request.ProjectName,
                request.CostCenterId,
                request.CompanyId,
                ct);

            return result ?? Result.Failure<Project>(ProjectErrors.ProjectWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}