using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetByCode;

public class GetProjectTypeByCodeQueryHandler : IQueryHandler<GetProjectTypeByCodeQuery, ProjectType?>
{
    private readonly ILogger<GetProjectTypeByCodeQueryHandler> _logger;
    private readonly IProjectTypeRepository _repository;

    public GetProjectTypeByCodeQueryHandler(ILogger<GetProjectTypeByCodeQueryHandler> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(GetProjectTypeByCodeQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.FindByCode(request.ProjectTypeCode, request.CompanyId, ct);
            return entity ?? Result.Failure<ProjectType>(ProjectTypeErrors.ProjectTypeWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectType>(SharedErrors.UnknownError);
        }
    }
}