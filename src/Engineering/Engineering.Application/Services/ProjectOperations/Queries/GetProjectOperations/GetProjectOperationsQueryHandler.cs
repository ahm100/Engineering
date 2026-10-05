using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperations;

public class GetProjectOperationsQueryHandler : IQueryHandler<GetProjectOperationsQuery, List<ProjectOperation>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetProjectOperationsQueryHandler> _logger;

    public GetProjectOperationsQueryHandler(
        ILogger<GetProjectOperationsQueryHandler> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperation>?>> Handle(GetProjectOperationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperations(request.Ids, ct);
            if (result == null || result.Count != request.Ids.Count)
                return Result.Failure<List<ProjectOperation>>(ProjectOperationErrors.NotFound);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}