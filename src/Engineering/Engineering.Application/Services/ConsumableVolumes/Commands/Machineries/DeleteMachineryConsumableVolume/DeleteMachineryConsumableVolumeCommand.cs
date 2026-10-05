using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.DeleteMachineryConsumableVolume;

public record DeleteConsumableVolumeMachineryCommand(
    long Id
    ) : ICommand<ConsumableVolumeMachinery>;