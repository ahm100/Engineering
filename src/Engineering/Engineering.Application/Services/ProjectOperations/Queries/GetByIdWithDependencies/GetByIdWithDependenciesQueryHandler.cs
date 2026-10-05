using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetByIdWithDependencies;

public class GetByIdWithDependenciesQueryHandler : IQueryHandler<GetByIdWithDependenciesQuery, ProjectOperation>
{
    private readonly ILogger<GetByIdWithDependenciesQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetByIdWithDependenciesQueryHandler(ILogger<GetByIdWithDependenciesQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetByIdWithDependenciesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByIdWithDependencies(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}