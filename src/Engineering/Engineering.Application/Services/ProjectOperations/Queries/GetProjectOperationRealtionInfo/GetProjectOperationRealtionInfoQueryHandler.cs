using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationRealtionInfo;

public class GetProjectOperationRealtionInfoQueryHandler : IQueryHandler<GetProjectOperationRealtionInfoQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationRealtionInfoQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationRealtionInfoQueryHandler(ILogger<GetProjectOperationRealtionInfoQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationRealtionInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationRealtionInfo(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
