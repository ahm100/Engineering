using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementProduct;

public record CreateContractorStatusStatementProductCommand(
    ContractorStatusStatementProduct Entity
    ) : ICommand<ContractorStatusStatementProduct>;
