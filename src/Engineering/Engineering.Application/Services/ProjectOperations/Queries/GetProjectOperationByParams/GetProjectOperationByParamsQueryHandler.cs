using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByParams;

public class GetProjectOperationByParamsQueryHandler : IQueryHandler<GetProjectOperationByParamsQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationByParamsQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationByParamsQueryHandler(ILogger<GetProjectOperationByParamsQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationByParamsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationByParams(request.ProjectId, request.OperationInfoId, request.EmployerContractId, request.MeasurementId, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}