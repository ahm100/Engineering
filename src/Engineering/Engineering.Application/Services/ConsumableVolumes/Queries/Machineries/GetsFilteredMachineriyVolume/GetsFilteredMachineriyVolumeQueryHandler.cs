using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetsFilteredMachineriyVolume;

public class GetsFilteredMachineriyVolumeQueryHandler : IQueryHandler<GetsFilteredMachineriyVolumeQuery, DataResult<List<ConsumableVolumeMachinery>>>
{
    private readonly IConsumableVolumeMachineryRepository _repository;
    private readonly ILogger<GetsFilteredMachineriyVolumeQueryHandler> _logger;

    public GetsFilteredMachineriyVolumeQueryHandler(ILogger<GetsFilteredMachineriyVolumeQueryHandler> logger, IConsumableVolumeMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumableVolumeMachinery>>?>> Handle(GetsFilteredMachineriyVolumeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredMachineriyVolume(request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, request.MachineryGroupId, request.MachineryId,
                request.FilterData, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumableVolumeMachinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumableVolumeMachinery>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumableVolumeMachinery>>>(SharedErrors.UnknownError);
        }
    }
}