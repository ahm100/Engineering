using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyDocuments;

public record UpdateESSProjectOperationDetailDailyDocumentsCommand(
    EmployerStatusStatementProjectOperationDetailDaily Entity,
    List<string>? Urls
    ) : ICommand<EmployerStatusStatementProjectOperationDetailDaily>;
