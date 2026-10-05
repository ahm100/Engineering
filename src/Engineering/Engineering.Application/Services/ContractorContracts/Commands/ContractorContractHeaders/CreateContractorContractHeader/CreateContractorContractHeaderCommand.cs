using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateContractorContractHeader;

public record CreateContractorContractHeaderCommand(
    CostCenter CostCenter,
    long ContractorId,
    long CurrencyId,
    string? Description,
    List<string>? Urls,
    long? CompanyId
    ) : ICommand<ContractorContractHeader>;
