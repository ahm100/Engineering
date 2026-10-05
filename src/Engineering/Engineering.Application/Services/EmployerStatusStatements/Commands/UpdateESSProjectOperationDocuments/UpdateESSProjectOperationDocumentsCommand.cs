using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDocuments;

public record UpdateESSProjectOperationDocumentsCommand(
    EmployerStatusStatementProjectOperation Entity,
    List<string>? Urls
    ) : ICommand<EmployerStatusStatementProjectOperation>;
