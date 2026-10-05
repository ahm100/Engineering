
namespace Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementCodeCreator;

public record ContractorStatusStatementCodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;