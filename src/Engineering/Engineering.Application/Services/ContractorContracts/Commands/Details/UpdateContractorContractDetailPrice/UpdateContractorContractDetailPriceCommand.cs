using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailPrice;

public record UpdateContractorContractDetailPriceCommand(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    long CurrencyId,
    bool IsActive
    ) : ICommand<ContractorContractDetailPrice>;
