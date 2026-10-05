using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdLessInclude;

public class GetProjectOperationByIdLessIncludeQueryHandler : IQueryHandler<GetProjectOperationByIdLessIncludeQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationByIdLessIncludeQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationByIdLessIncludeQueryHandler(ILogger<GetProjectOperationByIdLessIncludeQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationByIdLessIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationByIdLessInclude(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}