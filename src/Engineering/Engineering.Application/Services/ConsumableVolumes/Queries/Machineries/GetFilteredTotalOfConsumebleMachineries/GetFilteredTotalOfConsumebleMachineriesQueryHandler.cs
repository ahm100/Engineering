using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetFilteredTotalOfConsumebleMachineries;

public class GetFilteredTotalOfConsumebleMachineriesQueryHandler : IQueryHandler<GetFilteredTotalOfConsumebleMachineriesQuery, List<ConsumableVolumeMachinery>>
{
    private readonly IConsumableVolumeMachineryRepository _repository;
    private readonly ILogger<GetFilteredTotalOfConsumebleMachineriesQueryHandler> _logger;

    public GetFilteredTotalOfConsumebleMachineriesQueryHandler(ILogger<GetFilteredTotalOfConsumebleMachineriesQueryHandler> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ConsumableVolumeMachinery>?>> Handle(GetFilteredTotalOfConsumebleMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredTotalOfConsumebleMachineries(request.MachineryId, request.ProjectId, request.CostCenterId,
                request.ProjectOperationIds, request.ProjectOperationDetailIds, ct);

            return result ?? Result.Failure<List<ConsumableVolumeMachinery>>(ConsumableVolumeMachineryErrors.DataNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ConsumableVolumeMachinery>>(SharedErrors.UnknownError);
        }
    }
}