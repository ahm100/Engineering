using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetPODate;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetPODate;

public class GetPODateQueryHandler : IQueryHandler<GetPODateQuery, GetPODateResponse?>
{
    private readonly ILogger<GetPODateQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetPODateQueryHandler(ILogger<GetPODateQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetPODateResponse?>> Handle(GetPODateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetPODate(request.ProjectOperationId, ct);
            if (result == null)
                return Result.Failure<GetPODateResponse>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            if (result.PlannedStartDate != null && result.PlannedFinishDate != null)
                result.PlannedDuration = (result.PlannedFinishDate.Value - result.PlannedStartDate.Value).Days;

            if (result.ActualStartDate != null && result.ActualFinishDate != null)
                result.ActualDuration = (result.ActualFinishDate.Value - result.ActualStartDate.Value).Days;

            if (result.BaselineStartDate != null && result.BaselineFinishDate != null)
                result.BaseLineDuration = (result.BaselineFinishDate.Value - result.BaselineStartDate.Value).Days;

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetPODateResponse>(SharedErrors.UnknownError);
        }
    }
}