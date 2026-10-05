using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPODependencies;

public class GetPODependenciesQueryHandler : IQueryHandler<GetPODependenciesQuery, GetPODependenciesResponse?>
{
    private readonly ILogger<GetPODependenciesQueryHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;

    public GetPODependenciesQueryHandler(ILogger<GetPODependenciesQueryHandler> logger, IProjectOperationDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetPODependenciesResponse?>> Handle(GetPODependenciesQuery request, CT ct)
    {
        try
        {
            var predecessors = await _repository.GetPOPredecessorFullRequirements(request.ProjectOperationId, ct);
            var successors = await _repository.GetPOSuccessorFullRequirements(request.ProjectOperationId, ct);

            return successors.HasAny() || predecessors.HasAny() ?
                new GetPODependenciesResponse(predecessors, successors) :
                Result.Failure<GetPODependenciesResponse?>(ProjectOperationDetailErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPODependenciesResponse?>(SharedErrors.UnknownError);
        }
    }
}