using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementCostOver;

public record CreateContractorStatusStatementCostOverCommand(
    ContractorStatusStatementCostOver Entity
    ) : ICommand<ContractorStatusStatementCostOver>;
