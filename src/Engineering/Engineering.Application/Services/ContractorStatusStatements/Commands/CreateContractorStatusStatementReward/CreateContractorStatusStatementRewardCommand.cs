using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementReward;

public record CreateContractorStatusStatementRewardCommand(
    ContractorStatusStatementReward Entity
    ) : ICommand<ContractorStatusStatementReward>;
