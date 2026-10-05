using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.SetCCTotalAmount;

public record SetCCTotalAmountCommand(
    ContractorContract Entity
    ) : ICommand<ContractorContract>;
