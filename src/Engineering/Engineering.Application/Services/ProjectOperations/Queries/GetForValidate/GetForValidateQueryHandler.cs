using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetForValidate;

public class GetProjectOperationForValidateQueryHandler : IQueryHandler<GetProjectOperationForValidateQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationForValidateQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationForValidateQueryHandler(ILogger<GetProjectOperationForValidateQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationForValidateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetForValidate(request.ProjectId, request.OperationInfoId, request.EmployerContractId, request.UnitOfMeasurementId, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
