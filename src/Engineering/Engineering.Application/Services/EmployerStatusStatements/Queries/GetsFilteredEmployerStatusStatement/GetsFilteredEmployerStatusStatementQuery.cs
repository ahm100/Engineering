
using EmployerStatusStatement = Engineering.Domain.Entities.EmployerStatusStatements.EmployerStatusStatement;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetsFilteredEmployerStatusStatement;

public record GetsFilteredEmployerStatusStatementQuery(
    long EmployerId,
    long CostCenterId,
    long ProjectId,
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