using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatement;

public record CreateContractorStatusStatementCommand(
    ContractorStatusStatement Entity
    ) : ICommand<ContractorStatusStatement>;
