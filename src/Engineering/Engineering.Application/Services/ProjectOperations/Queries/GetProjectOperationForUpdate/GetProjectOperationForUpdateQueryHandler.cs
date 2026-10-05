using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForUpdate;

public class GetProjectOperationForUpdateQueryHandler : IQueryHandler<GetProjectOperationForUpdateQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationForUpdateQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationForUpdateQueryHandler(ILogger<GetProjectOperationForUpdateQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationForUpdateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationForUpdate(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
