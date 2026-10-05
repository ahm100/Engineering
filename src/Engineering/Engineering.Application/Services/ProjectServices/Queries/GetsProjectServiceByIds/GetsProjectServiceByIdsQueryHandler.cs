using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceByIds;

public class GetsProjectServiceByIdsQueryHandler : IQueryHandler<GetsProjectServiceByIdsQuery, List<ProjectService>>
{
    private readonly IProjectServiceRepository _repository;
    private readonly ILogger<GetsProjectServiceByIdsQuery> _logger;

    public GetsProjectServiceByIdsQueryHandler(ILogger<GetsProjectServiceByIdsQuery> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectService>?>> Handle(GetsProjectServiceByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectServiceByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectService>>(SharedErrors.UnknownError);
        }
    }
}
