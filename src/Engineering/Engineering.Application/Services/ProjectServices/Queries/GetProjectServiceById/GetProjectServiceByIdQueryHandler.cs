using Engineering.Application.Abstractions.Data.Projects;
using ProjectService = Engineering.Domain.Entities.Projects.ProjectService;

namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServiceById;

public class GetProjectServiceByIdQueryHandler : IQueryHandler<GetProjectServiceByIdQuery, ProjectService>
{
    private readonly ILogger<GetProjectServiceByIdQueryHandler> _logger;
    private readonly IProjectServiceRepository _repository;

    public GetProjectServiceByIdQueryHandler(
        ILogger<GetProjectServiceByIdQueryHandler> logger,
        IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectService?>> Handle(GetProjectServiceByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectServiceById(request.Id, ct);

            return result ?? Result.Failure<ProjectService>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectService>(SharedErrors.UnknownError);
        }
    }
}