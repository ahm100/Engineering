using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementByIdForDocs;

public record GetEmployerStatusStatementByIdForDocsQuery(
    long Id
    ) : IQuery<EmployerStatusStatement>;