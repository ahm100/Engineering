
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.EmployerStatusStatementCodeCreator;

public record EmployerStatusStatementCodeCreatorCommand(
    long? CompanyId
    ) : ICommand<string?>;