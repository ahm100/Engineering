using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdOperationInfoInclude;

public class GetProjectOperationByIdOperationInfoIncludeQueryHandler : IQueryHandler<GetProjectOperationByIdOperationInfoIncludeQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationByIdOperationInfoIncludeQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationByIdOperationInfoIncludeQueryHandler(ILogger<GetProjectOperationByIdOperationInfoIncludeQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationByIdOperationInfoIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationByIdOperationInfoInclude(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}