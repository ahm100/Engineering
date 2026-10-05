using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementService;

public record CreateContractorStatusStatementServiceCommand(
    ContractorStatusStatementService Entity
    ) : ICommand<ContractorStatusStatementService>;
