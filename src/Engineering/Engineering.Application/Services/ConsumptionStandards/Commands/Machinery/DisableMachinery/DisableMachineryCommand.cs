using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.DisableMachinery;

public record DisableMachineryCommand(
    long Id
    ) : ICommand<ConsumptionStandardMachinery>;