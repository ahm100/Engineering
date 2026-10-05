using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementExcelExporter;

public record GetEmployerStatusStatementExcelExporterQuery(
    long Id
    ) : IQuery<EmployerStatusStatement>;