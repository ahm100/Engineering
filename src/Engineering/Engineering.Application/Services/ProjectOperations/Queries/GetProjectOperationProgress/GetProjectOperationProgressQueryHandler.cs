using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationProgress;

public class GetProjectOperationProgressQueryHandler : IQueryHandler<GetProjectOperationProgressQuery, GetProjectOperationProgressResponse?>
{
    private readonly ILogger<GetProjectOperationProgressQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationProgressQueryHandler(ILogger<GetProjectOperationProgressQueryHandler> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetProjectOperationProgressResponse?>> Handle(GetProjectOperationProgressQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationProgress(request.Id, ct);

            if (result is null)
                return Result.Failure<GetProjectOperationProgressResponse>(
                    ProjectOperationErrors.ProjectOperationDoesNotHaveEstimate);

            var plannedDays = (result.PlannedFinishDate - result.PlannedStartDate).Days;
            var now = (DateTime.UtcNow - result.PlannedStartDate).Days;

            if (plannedDays <= 0)
                result.PlannedProgressPercent = 100;
            else
                result.PlannedProgressPercent = Math.Clamp(
                    Math.Round((decimal)now * 100m / plannedDays, 2),
                    0m,
                    100m);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectOperationProgressResponse>(SharedErrors.UnknownError);
        }
    }
}
