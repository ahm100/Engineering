using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatement;

public record CreateEmployerStatusStatementCommand(
    EmployerStatusStatement Entity
    ) : ICommand<EmployerStatusStatement>;
