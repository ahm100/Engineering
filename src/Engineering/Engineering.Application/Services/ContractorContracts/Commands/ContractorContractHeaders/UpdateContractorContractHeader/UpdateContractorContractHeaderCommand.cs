using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.UpdateContractorContractHeader;

public record UpdateContractorContractHeaderCommand(
    ContractorContractHeader Entity,
    long CurrencyId,
    string? Description,
    List<string>? Urls
    ) : ICommand<ContractorContractHeader>;
