using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateEmployerStatusStatementDocuments;

public record UpdateEmployerStatusStatementDocumentsCommand(
    EmployerStatusStatement Entity,
    List<string>? Urls
    ) : ICommand<EmployerStatusStatement>;
