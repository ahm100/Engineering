using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailService;

public record DeleteContractorContractDetailServiceCommand(
    long Id
    ) : ICommand<ContractorContractDetailService>;
