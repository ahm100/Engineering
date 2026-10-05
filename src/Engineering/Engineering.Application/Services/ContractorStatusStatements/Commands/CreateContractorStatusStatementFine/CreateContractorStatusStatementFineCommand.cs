using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementFine;

public record CreateContractorStatusStatementFineCommand(
    ContractorStatusStatementFine Entity
    ) : ICommand<ContractorStatusStatementFine>;
