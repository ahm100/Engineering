using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationProgress;

namespace Engineering.Application.Services.Dashboard.Queries.GetProjectDashboard;

public class GetProjectDashboardQueryHandler : IQueryHandler<GetProjectDashboardQuery, GetProjectDashboardResponse?>
{
    private readonly ILogger<GetProjectDashboardQueryHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IProjectOperationRepository _projectOperationRepo;

    public GetProjectDashboardQueryHandler(
        ILogger<GetProjectDashboardQueryHandler> logger,
        IProjectOperationRepository projectOperationRepo,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _projectOperationRepo = projectOperationRepo;
    }

    public async Task<Result<GetProjectDashboardResponse?>> Handle(GetProjectDashboardQuery request, CT ct)
    {
        try
        {
            var pOs = await _projectOperationRepo.GetByProjectId(request.ProjectId, ct);

            if (!pOs.Any())
                return Result.Success<GetProjectDashboardResponse?>(null);

            decimal completedPercent = 0;

            int? totalDelayed = await _projectOperationRepo.GetDelayed(request.ProjectId, ct);
            int? totalCritical = await _projectOperationRepo.GetCriticalPO(request.ProjectId, ct);
            DateTime? lastPODate = await _projectOperationRepo.GetLastPO(request.ProjectId, ct);
            DateTime? firstPODate = await _projectOperationRepo.GetFirstPO(request.ProjectId, ct);

            foreach (var item in pOs)
            {
                var pOProgress = await _mediator.Send(
                    new GetProjectOperationProgressQuery(item.Id), ct);

                if (pOProgress.IsBad())
                    continue;

                completedPercent += pOProgress.Value!.ActualProgressPercent ?? 0;
            }

            decimal finishedPercent = completedPercent / pOs.Count;

            DateTime? estimatedFinishDate = null;

            if (firstPODate.HasValue && finishedPercent > 0)
            {
                var elapsedDays = (DateTime.Now - firstPODate.Value).TotalDays;

                var estimatedTotalDays = elapsedDays / ((double)finishedPercent / 100);

                estimatedFinishDate = firstPODate.Value.AddDays(estimatedTotalDays);
            }

            var response = new GetProjectDashboardResponse
            {
                DelayedPOS = totalDelayed,
                NearEndPOS = totalCritical,
                EstimatedFinishDate = estimatedFinishDate,
                PlannedFinishDate = lastPODate,
                FinishedPercent = finishedPercent
            };

            return Result.Success<GetProjectDashboardResponse?>(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectDashboardResponse?>(SharedErrors.UnknownError);
        }
    }
}