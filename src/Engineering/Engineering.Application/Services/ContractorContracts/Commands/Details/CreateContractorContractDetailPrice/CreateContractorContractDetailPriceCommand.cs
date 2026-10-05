using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailPrice;

public record CreateContractorContractDetailPriceCommand(
    ContractorContractDetail ContractorContractDetail,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    long CurrencyId,
    bool IsActive
    ) : ICommand<ContractorContractDetailPrice>;
