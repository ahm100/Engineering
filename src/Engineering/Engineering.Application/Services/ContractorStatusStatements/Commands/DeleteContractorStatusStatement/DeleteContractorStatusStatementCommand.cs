using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatement;

public record DeleteContractorStatusStatementCommand(
    ContractorStatusStatement Entity
    ) : ICommand<ContractorStatusStatement>;
