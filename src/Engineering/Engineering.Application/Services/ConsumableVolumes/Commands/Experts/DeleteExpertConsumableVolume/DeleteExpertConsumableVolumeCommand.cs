using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.DeleteExpertConsumableVolume;

public record DeleteConsumableVolumeExpertCommand(
    long Id
    ) : ICommand<ConsumableVolumeExpert>;