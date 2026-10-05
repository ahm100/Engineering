using Engineering.Domain.Entities.ProjectOperationDetails;
using ConsumableVolumeExpert = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeExpert;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.CreateExpertConsumableVolume;

public record CreateConsumableVolumeExpertCommand(
    ProjectOperationDetail ProjectOperationDetail,
    long ExpertId,
    decimal? Number,
    decimal? UnusedPercentage,
    bool IsStandard,
    long? StandardValue,
    long FinalValue
    ) : ICommand<ConsumableVolumeExpert>;