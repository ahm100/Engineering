using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailCostOver;

public record DeleteContractorContractDetailCostOverCommand(
    long Id
    ) : ICommand<ContractorContractDetailCostOver>;
