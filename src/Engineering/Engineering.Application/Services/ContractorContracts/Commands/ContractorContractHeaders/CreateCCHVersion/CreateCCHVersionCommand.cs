using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateCCHVersion;

public record CreateCCHVersionCommand(
    ContractorStatusStatement CSS,
    List<ContractorContractHeader> CCHs
    ) : ICommand<List<ContractorContractHeaderVersion>>;
