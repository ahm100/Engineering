using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Project>
{
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectByIdQueryHandler(
        ILogger<GetProjectByIdQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}