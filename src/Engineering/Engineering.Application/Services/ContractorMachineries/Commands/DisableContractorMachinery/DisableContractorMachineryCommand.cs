using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.DisableContractorMachinery;

public record DisableContractorMachineryCommand(
    long Id
    ) : ICommand<ContractorMachinery>;