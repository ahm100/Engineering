using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;

public class GetProjectOperationByIdNoIncludingQueryHandler : IQueryHandler<GetProjectOperationByIdNoIncludingQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationByIdNoIncludingQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationByIdNoIncludingQueryHandler(ILogger<GetProjectOperationByIdNoIncludingQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationByIdNoIncludingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationByIdNoIncluding(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}