using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetsProjectTypeByIds;

public class GetsProjectTypeByIdsQueryHandler : IQueryHandler<GetsProjectTypeByIdsQuery, List<ProjectType>>
{
    private readonly IProjectTypeRepository _repository;
    private readonly ILogger<GetsProjectTypeByIdsQuery> _logger;

    public GetsProjectTypeByIdsQueryHandler(ILogger<GetsProjectTypeByIdsQuery> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectType>?>> Handle(GetsProjectTypeByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectTypeByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectType>>(SharedErrors.UnknownError);
        }
    }
}
