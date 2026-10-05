
using EmployerStatusStatement = Engineering.Domain.Entities.EmployerStatusStatements.EmployerStatusStatement;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsEmployerStatusStatementExcelExporter;

public record GetsEmployerStatusStatementExcelExporterQuery(
    List<long>? Ids,
    long CostCenterId,
    long ProjectId,
    long? EmployerId,
    long EmployerContractId,
    string? StatusStatementCode,
    List<long>? ProjectOperationIds,
    DateTime? StartDate,
    DateTime? EndDate,
    Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus? Status,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<EmployerStatusStatement>>>;