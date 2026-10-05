using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetail;

public record DeleteContractorContractDetailCommand(
    long Id
    ) : ICommand<ContractorContractDetail>;
