using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using ConsumableVolumeMachinery = Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes.ConsumableVolumeMachinery;

namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.UpdateMachineryConsumableVolume;

public record UpdateConsumableVolumeMachineryCommand(
    ConsumableVolumeMachinery ConsumableVolumeMachinery,
    Machinery Machinery,
    decimal? Number,
    decimal? UnusedPercentage,
    bool IsStandard,
    long? StandardValue,
    decimal FinalValue,
    RequestMachineryUnit? Unit
    ) : ICommand<ConsumableVolumeMachinery>;