using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.ProjectOperationWorkloadManagement;

public class ProjectOperationWorkloadManagementQueryHandler : IQueryHandler<ProjectOperationWorkloadManagementQuery, ProjectOperation>
{
    private readonly ILogger<ProjectOperationWorkloadManagementQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public ProjectOperationWorkloadManagementQueryHandler(ILogger<ProjectOperationWorkloadManagementQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(ProjectOperationWorkloadManagementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationById(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}