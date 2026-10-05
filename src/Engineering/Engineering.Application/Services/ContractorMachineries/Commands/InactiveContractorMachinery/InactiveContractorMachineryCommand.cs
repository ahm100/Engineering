using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.InactiveContractorMachinery;

public record InactiveContractorMachineryCommand(
    long Id
    ) : ICommand<ContractorMachinery>;