using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailPrice;

public record DeleteContractorContractDetailPriceCommand(long Id) : ICommand<ContractorContractDetailPrice>;
