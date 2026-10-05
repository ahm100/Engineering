using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.UpdateConsumableVolumes;

public record UpdateConsumableVolumesCommand(
    List<ConsumableVolumeExpert>? Experts,
    List<ConsumableVolumeMachinery>? Machineries,
    List<ConsumableVolumeProduct>? Products
    ) : ICommand<bool>;