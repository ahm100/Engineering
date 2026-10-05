using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperationDetail;

public record CreateEmployerStatusStatementProjectOperationDetailCommand(
    EmployerStatusStatementProjectOperation StatementProjectOperation,
    ProjectOperationDetail ProjectOperationDetail,
    string? Description
    ) : ICommand<EmployerStatusStatementProjectOperationDetail>;
