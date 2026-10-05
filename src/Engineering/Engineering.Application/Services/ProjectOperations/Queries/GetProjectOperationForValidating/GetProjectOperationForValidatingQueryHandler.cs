using Engineering.Application.Abstractions.Data.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForValidating;

public class GetProjectOperationForValidatingQueryHandler : IQueryHandler<GetProjectOperationForValidatingQuery, bool>
{
    private readonly ILogger<GetProjectOperationForValidatingQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationForValidatingQueryHandler(ILogger<GetProjectOperationForValidatingQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetProjectOperationForValidatingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationForValidating(request.OprationInfoId, request.ProjectId, request.UnitOfMeasurementId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}