using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;

public class GetProjectByIdIncludelessQueryHandler : IQueryHandler<GetProjectByIdIncludelessQuery, Project>
{
    private readonly ILogger<GetProjectByIdIncludelessQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectByIdIncludelessQueryHandler(
        ILogger<GetProjectByIdIncludelessQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByIdIncludelessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectByIdIncludeLess(request.Id, ct);

            return result ?? Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}