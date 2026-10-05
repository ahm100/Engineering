using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.ActiveContractorMachinery;

public record ActiveContractorMachineryCommand(
    long Id
    ) : ICommand<ContractorMachinery>;