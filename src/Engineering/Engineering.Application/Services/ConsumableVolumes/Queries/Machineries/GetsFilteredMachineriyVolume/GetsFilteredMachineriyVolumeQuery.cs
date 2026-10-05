using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetsFilteredMachineriyVolume;

public record GetsFilteredMachineriyVolumeQuery(
    long ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    long? MachineryGroupId,
    long? MachineryId,
    string? FilterData
    ) : IQuery<DataResult<List<ConsumableVolumeMachinery>>>;
