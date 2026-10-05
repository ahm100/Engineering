using Engineering.Domain.Entities.OperationInfos;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;
using MachineryEntity = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.CreateMachinery;

public record CreateMachineryCommand(
    OperationInfo OperationInfo,
    MachineryEntity Machinery,
    int MachineryNumber,
    long TimeSpant,
    decimal? UnusedPercentage
    ) : ICommand<ConsumptionStandardMachinery>;