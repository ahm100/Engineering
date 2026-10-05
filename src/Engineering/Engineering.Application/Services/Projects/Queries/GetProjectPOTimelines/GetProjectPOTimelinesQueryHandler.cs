using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;

namespace Engineering.Application.Services.Projects.Queries.GetProjectPOTimelines;

public class GetProjectPOTimelinesQueryHandler : IQueryHandler<GetProjectPOTimelinesQuery, GetProjectPOTimelinesResponse?>
{
    private readonly ILogger<GetProjectPOTimelinesQueryHandler> _logger;
    private readonly IProjectOperationRepository _pORepo;

    public GetProjectPOTimelinesQueryHandler(
        ILogger<GetProjectPOTimelinesQueryHandler> logger,
        IProjectOperationRepository pORepo)
    {
        _logger = logger;
        _pORepo = pORepo;
    }

    public async Task<Result<GetProjectPOTimelinesResponse?>> Handle(GetProjectPOTimelinesQuery request, CT ct)
    {
        try
        {
            var result = await _pORepo.GetProjectPOTimelines(request.Id, ct);
            if (result.Count < 1 || result.Data == null)
                return Result.Failure<GetProjectPOTimelinesResponse>(ProjectOperationErrors.OperationInfoNotFoundWithFilters);

            var data = result.Data!;
            var totalWorkload = data.Sum(x => x.WorkLoad);

            var plannedStartProject =
                WeightedAvg(data.Select(x =>
                (x.PlannedStartProgressPercent, x.WorkLoad)));

            var plannedEndProject =
                WeightedAvg(data.Select(x =>
                (x.PlannedEndProgressPercent, x.WorkLoad)));

            var actualStartProject =
                WeightedAvg(data.Select(x =>
                (x.ActualStartProgressPercent, x.WorkLoad)));

            var actualEndProject =
                WeightedAvg(data.Select(x =>
                (x.ActualEndProgressPercent, x.WorkLoad)));

            foreach (var item in data)
            {
                item.PlannedStartProjectProgressPercent = plannedStartProject;
                item.PlannedEndProjectProgressPercent = plannedEndProject;

                item.ActualStartProjectProgressPercent = actualStartProject;
                item.ActualEndProjectProgressPercent = actualEndProject;
            }

            return new GetProjectPOTimelinesResponse(result.Data, result.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectPOTimelinesResponse>(SharedErrors.UnknownError);
        }
    }

    decimal WeightedAvg(IEnumerable<(decimal value, decimal weight)> items)
    {
        var weightedSum = items.Sum(x => x.value * x.weight);
        var weightSum = items.Sum(x => x.weight);

        return weightSum == 0 ? 0 : weightedSum / weightSum;
    }
}