using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.StateChangerContractorMachineries;

public record StateChangerContractorMachineriesCommand(
    List<ContractorMachinery> Items,
    bool State
    ) : ICommand<bool?>;
