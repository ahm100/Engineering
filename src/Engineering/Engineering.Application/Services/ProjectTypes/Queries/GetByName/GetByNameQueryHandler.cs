using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetByName;

public class GetProjectTypeByNameQueryHandler : IQueryHandler<GetProjectTypeByNameQuery, ProjectType?>
{
    private readonly ILogger<GetProjectTypeByNameQueryHandler> _logger;
    private readonly IProjectTypeRepository _repository;

    public GetProjectTypeByNameQueryHandler(ILogger<GetProjectTypeByNameQueryHandler> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(GetProjectTypeByNameQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.FindByName(request.ProjectTypeName, request.CompanyId, ct);
            return entity ?? Result.Failure<ProjectType>(ProjectTypeErrors.ProjectTypeWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectType>(SharedErrors.UnknownError);
        }
    }
}
