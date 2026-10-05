using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementById;

public record GetEmployerStatusStatementByIdQuery(
    long Id
    ) : IQuery<EmployerStatusStatement>;