using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;

public class GetProjectByIdNoIncludingQueryHandler : IQueryHandler<GetProjectByIdNoIncludingQuery, Project>
{
    private readonly ILogger<GetProjectByIdNoIncludingQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectByIdNoIncludingQueryHandler(
        ILogger<GetProjectByIdNoIncludingQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByIdNoIncludingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectByIdNoIncluding(
                request.Id,
                ct);

            return result ?? Result.Failure<Project>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}
