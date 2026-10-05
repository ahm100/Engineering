using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.ContractorContractHeaderStatusChanger;


public record ContractorContractHeaderStatusChangerCommand(
    ContractorContractHeader Entity,
    ContractorContractStatus Status,
    string? Description,
    List<string>? Urls
    ) : ICommand<ContractorContractHeader>;
