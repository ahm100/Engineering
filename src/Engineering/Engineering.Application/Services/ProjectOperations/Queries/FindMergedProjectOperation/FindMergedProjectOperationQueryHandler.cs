using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.FindMergedProjectOperation;

public class FindMergedProjectOperationQueryHandler : IQueryHandler<FindMergedProjectOperationQuery, ProjectOperation>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<FindMergedProjectOperationQueryHandler> _logger;

    public FindMergedProjectOperationQueryHandler(ILogger<FindMergedProjectOperationQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(FindMergedProjectOperationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindMergedProjectOperation(
                request.ProjectOperationId, request.ProjectId, request.UnitOfMeasurementId, request.OperationInfoId, ct);

            return result ?? Result.Failure<ProjectOperation?>(SharedErrors.UnknownError)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError)!;
        }
    }
}