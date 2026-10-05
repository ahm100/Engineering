using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationDocuments;

public class GetProjectOperationDocumentsQueryHandler : IQueryHandler<GetProjectOperationDocumentsQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationDocumentsQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationDocumentsQueryHandler(
        ILogger<GetProjectOperationDocumentsQueryHandler> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationDocumentsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDocuments(
                request.Id,
                ct);

            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}