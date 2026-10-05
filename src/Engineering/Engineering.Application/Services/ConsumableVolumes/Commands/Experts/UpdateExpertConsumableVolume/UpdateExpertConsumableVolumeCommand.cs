using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.UpdateExpertConsumableVolume;

public record UpdateConsumableVolumeExpertCommand(
    ConsumableVolumeExpert ConsumableVolumeExpert,
    long ExpertId,
    decimal? Number,
    decimal? UnusedPercentage,
    bool IsStandard,
    long? StandardValue,
    long FinalValue
    ) : ICommand<ConsumableVolumeExpert>;