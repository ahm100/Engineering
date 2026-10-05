using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPORequirements;

public class GetPORequirementsQueryHandler : IQueryHandler<GetPORequirementsQuery, GetPORequirementsResponse?>
{
    private readonly ILogger<GetPORequirementsQueryHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;

    public GetPORequirementsQueryHandler(ILogger<GetPORequirementsQueryHandler> logger, IProjectOperationDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetPORequirementsResponse?>> Handle(GetPORequirementsQuery request, CT ct)
    {
        try
        {
            var predecessors = await _repository.GetPOPredecessorRequirements(request.ProjectOperationId, ct);
            var successors = await _repository.GetPOSuccessorRequirements(request.ProjectOperationId, ct);

            return successors.HasAny() || predecessors.HasAny() ?
                new GetPORequirementsResponse(predecessors, successors) :
                Result.Failure<GetPORequirementsResponse?>(ProjectOperationDetailErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPORequirementsResponse?>(SharedErrors.UnknownError);
        }
    }
}