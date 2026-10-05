using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdIncludelessNew;

public class GetProjectOperationByIdIncludelessNewQueryHandler : IQueryHandler<GetProjectOperationByIdIncludelessNewQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationByIdIncludelessNewQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationByIdIncludelessNewQueryHandler(ILogger<GetProjectOperationByIdIncludelessNewQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationByIdIncludelessNewQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationByIdIncludelessNew(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
