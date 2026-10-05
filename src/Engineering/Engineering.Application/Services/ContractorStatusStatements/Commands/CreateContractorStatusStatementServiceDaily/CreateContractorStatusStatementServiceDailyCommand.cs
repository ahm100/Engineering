using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementServiceDaily;

public record CreateContractorStatusStatementServiceDailyCommand(
    ContractorStatusStatementServiceDaily Entity
    ) : ICommand<ContractorStatusStatementServiceDaily>;
