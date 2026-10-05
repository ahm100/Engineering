using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetProjectTypeById;

public class GetProjectTypeByIdQueryHandler : IQueryHandler<GetProjectTypeByIdQuery, ProjectType>
{
    private readonly ILogger<GetProjectTypeByIdQueryHandler> _logger;
    private readonly IProjectTypeRepository _repository;

    public GetProjectTypeByIdQueryHandler(ILogger<GetProjectTypeByIdQueryHandler> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectType?>> Handle(GetProjectTypeByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.Id, ct);

            return result ?? Result.Failure<ProjectType>(ProjectErrors.ProjectWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectType>(SharedErrors.UnknownError);
        }
    }
}