using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetConsumableVolumeMachineryById;

public record GetConsumableVolumeMachineryByIdQuery(
    long Id
    ) : IQuery<ConsumableVolumeMachinery>;