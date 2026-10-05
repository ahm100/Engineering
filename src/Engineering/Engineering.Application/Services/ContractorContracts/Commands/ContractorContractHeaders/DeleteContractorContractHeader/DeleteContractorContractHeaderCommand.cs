using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.DeleteContractorContractHeader;

public record DeleteContractorContractHeaderCommand(
    long Id
    ) : ICommand<ContractorContractHeader>;