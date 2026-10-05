using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDetail;

public record CreateContractorStatusStatementDetailCommand(
    ContractorStatusStatementDetail Entity
    ) : ICommand<ContractorStatusStatementDetail>;
