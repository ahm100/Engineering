using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetSummarizedProjectById;

public class GetSummarizedProjectByIdQueryHandler : IQueryHandler<GetSummarizedProjectByIdQuery, Project>
{
    private readonly ILogger<GetSummarizedProjectByIdQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetSummarizedProjectByIdQueryHandler(
        ILogger<GetSummarizedProjectByIdQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(GetSummarizedProjectByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetSummarizedProjectById(
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
