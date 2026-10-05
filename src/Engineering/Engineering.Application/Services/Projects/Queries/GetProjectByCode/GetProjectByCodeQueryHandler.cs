using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByCode;

public class GetProjectByCodeQueryHandler : IQueryHandler<GetProjectByCodeQuery, Project>
{
    private readonly ILogger<GetProjectByCodeQueryHandler> _logger;
    private readonly IProjectRepository _repository;

    public GetProjectByCodeQueryHandler(
        ILogger<GetProjectByCodeQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByCodeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByCode(
                request.ProjectCode,
                request.CostCenterId,
                request.CompanyId,
                ct);

            return result ?? Result.Failure<Project>(ProjectErrors.ProjectWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}