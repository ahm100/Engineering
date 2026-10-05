using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;
using MachineryEntity = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.UpdateMachinery;

public record UpdateMachineryCommand(
    long Id,
    MachineryEntity Machinery,
    int MachineryNumber,
    long TimeSpant,
    decimal? UnusedPercentage
    ) : ICommand<ConsumptionStandardMachinery>;